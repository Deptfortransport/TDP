-- ***********************************************
-- NAME 		: DUP1997_BatchCoordinateDefence.sql
-- DESCRIPTION 	: Update Batch with bad coordinate defence - correct MaxEasting
--				  and MaxNorthing values.
-- AUTHOR		: Rich Broddle
-- DATE			: 05 March 2013
-- ************************************************

USE [BatchJourneyPlanner]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[AddBatchRequest]
(
	@EmailAddress nvarchar(250),
	@IncludeJourneyStatistics bit,
	@IncludeJourneyDetails bit,
	@IncludePublicTransport bit,
	@IncludeCar bit,
	@IncludeCycle bit,
	@ConvertToRtf bit,
	@BatchDetails xml
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Limits for eastings and northings
	DECLARE @MaxEasting INT
	SET @MaxEasting = 800000
	DECLARE @MaxNorthing INT
	SET @MaxNorthing = 1350000

	-- Get the user id
	DECLARE @UserId int
	SELECT @UserId = UserId
	FROM RegisteredUser
	WHERE @EmailAddress = EmailAddress
	
	-- Add a record to batch request
	DECLARE @Date datetime
	SELECT @Date = GETDATE()
	
	INSERT INTO [BatchJourneyPlanner].[dbo].[BatchRequestSummary]
           ([UserId]
           ,[QueuedDateTime]
           ,[BatchStatusId]
           ,[ProcessorId]
           ,[ReportParameters.IncludeJourneyStatistics]
           ,[ReportParameters.IncludeJourneyDetails]
           ,[ReportParameters.IncludePublicTransport]
           ,[ReportParameters.IncludeCar]
           ,[ReportParameters.IncludeCycle]
           ,[ReportParameters.ConvertToRtf])
     VALUES
           (@UserId
           ,@Date
           ,1 -- Pending
           ,'Inserting' -- To avoid being picked up during the insert
           ,@IncludeJourneyStatistics
           ,@IncludeJourneyDetails
           ,@IncludePublicTransport
           ,@IncludeCar
           ,@IncludeCycle
           ,@ConvertToRtf)	
	
	DECLARE @BatchId int
	SELECT @BatchId = @@IDENTITY
	
	-- Add the batch detail records
	DECLARE @userSuppliedUniqueId nvarchar(100)
	DECLARE @originType char(1)
	DECLARE @origin nvarchar(100)
	DECLARE @destinationType char(1)
	DECLARE @destination nvarchar(100)
	DECLARE @outwardDate nvarchar(6)
	DECLARE @outwardTimeAsString nvarchar(5)
	DECLARE @outwardArrDep char(1)
	DECLARE @returnDate nvarchar(6)
	DECLARE @returnTimeAsString nvarchar(5)
	DECLARE @returnArrDep char(1)
	DECLARE @detailStatus char(1)
	
	DECLARE Cursor_Details CURSOR STATIC FORWARD_ONLY READ_ONLY FOR 
	SELECT   
		tab.col.value('UserSuppliedUniqueId[1]','NVARCHAR(100)'),
		tab.col.value('OriginType[1]','CHAR(1)'),
		tab.col.value('Origin[1]','NVARCHAR(100)'),
		tab.col.value('DestinationType[1]','CHAR(1)'),
		tab.col.value('Destination[1]','NVARCHAR(100)'),
		tab.col.value('OutwardDate[1]','NVARCHAR(6)'),
		tab.col.value('OutwardTime[1]','NVARCHAR(5)'),
		tab.col.value('OutwardArrDep[1]','CHAR(1)'),
		tab.col.value('ReturnDate[1]','NVARCHAR(6)'),
		tab.col.value('ReturnTime[1]','NVARCHAR(5)'),
		tab.col.value('ReturnArrDep[1]','CHAR(1)')
	FROM @BatchDetails.nodes('//DetailRecord') tab(col) 
	
	OPEN Cursor_Details
	FETCH NEXT FROM Cursor_Details INTO
		@userSuppliedUniqueId,
		@originType,
		@origin,
		@destinationType,
		@destination,
		@outwardDate,
		@outwardTimeAsString,
		@outwardArrDep,
		@returnDate,
		@returnTimeAsString,
		@returnArrDep
		
	DECLARE @lines int
	SELECT @lines = 0
	DECLARE @invalidLines int
	SELECT @invalidLines = 0
	DECLARE @badRows int
	SELECT @badRows = 0
	
	WHILE 0 = @@FETCH_STATUS
	BEGIN
		-- Insert the detail row
		SELECT @lines = @lines + 1
		
		-- Check for dodgy coords
		DECLARE @goodCoords BIT
		SET @goodCoords = 1

		BEGIN TRY
			DECLARE @Separator INT
			DECLARE @Easting INT
			DECLARE @Northing INT

			IF (@originType = 'c')
			BEGIN
				SET @Separator = CHARINDEX('|', @Origin)
				SET @Easting = SUBSTRING(@Origin, 0, @Separator)
				SET @Northing = SUBSTRING(@Origin, @Separator + 1, 1000)

				IF (@Easting > @MaxEasting)
				BEGIN
					SET @goodCoords = 0
				END

				IF (@Northing > @MaxNorthing)
				BEGIN
					SET @goodCoords = 0
				END
			END

			IF (@destinationType = 'c')
			BEGIN
				SET @Separator = CHARINDEX('|', @destination)
				SET @Easting = SUBSTRING(@destination, 0, @Separator)
				SET @Northing = SUBSTRING(@destination, @Separator + 1, 1000)

				IF (@Easting > @MaxEasting)
				BEGIN
					SET @goodCoords = 0
				END

				IF (@Northing > @MaxNorthing)
				BEGIN
					SET @goodCoords = 0
				END
			END
		END TRY
		BEGIN CATCH
			-- co-ord string is duff in some other way
			SET @goodCoords = 0
		END CATCH
		
		IF (@goodCoords = 0)
		BEGIN
			INSERT INTO BatchRequestDetail
			   ([BatchId]
			   ,[BatchDetailStatusId]
			   ,[UserSuppliedUniqueId])
			VALUES
			   (@BatchId
			   ,4 -- validation error
			   ,@userSuppliedUniqueId)
		END
		ELSE
		BEGIN
			-- if necessary prepend a zero to the time
			IF (LEN(@outwardTimeAsString) = 3)
				SET @outwardTimeAsString = '0' + @outwardTimeAsString
				
			-- Convert outward time 
			SET @outwardTimeAsString = LEFT(@outwardTimeAsString, 2) + ':' + RIGHT(@outwardTimeAsString, 2)
			
			-- Catch errors
			BEGIN TRY		
				-- check for return journey
				IF (LEN(@returnDate) > 0 AND LEN(@returnTimeAsString) > 0 AND LEN(@returnArrDep) > 0)
				BEGIN
					-- if necessary prepend a zero to the time
					IF (LEN(@returnTimeAsString) = 3)
						SET @returnTimeAsString = '0' + @returnTimeAsString

					-- Convert return time 
					SET @returnTimeAsString = LEFT(@returnTimeAsString, 2) + ':' + RIGHT(@returnTimeAsString, 2)
					
					INSERT INTO BatchRequestDetail
					   ([BatchId]
					   ,[BatchDetailStatusId]
					   ,[UserSuppliedUniqueId]
					   ,[JourneyParameters.OriginType]
					   ,[JourneyParameters.Origin]
					   ,[JourneyParameters.DestinationType]
					   ,[JourneyParameters.Destination]
					   ,[JourneyParameters.OutwardDate]
					   ,[JourneyParameters.OutwardTime]
					   ,[JourneyParameters.OutwardArrDep]
					   ,[JourneyParameters.ReturnDate]
					   ,[JourneyParameters.ReturnTime]
					   ,[JourneyParameters.ReturnArrDep]
					   ,[SubmittedDateTime])
					VALUES
					   (@BatchId
					   ,@detailStatus
					   ,@userSuppliedUniqueId
					   ,@originType
					   ,@origin
					   ,@destinationType
					   ,@destination
					   ,@outwardDate
					   ,@outwardTimeAsString
					   ,@outwardArrDep
					   ,@returnDate
					   ,@returnTimeAsString
					   ,@returnArrDep
					   ,@Date)
				END
				ELSE
				BEGIN
					INSERT INTO BatchRequestDetail
					   ([BatchId]
					   ,[BatchDetailStatusId]
					   ,[UserSuppliedUniqueId]
					   ,[JourneyParameters.OriginType]
					   ,[JourneyParameters.Origin]
					   ,[JourneyParameters.DestinationType]
					   ,[JourneyParameters.Destination]
					   ,[JourneyParameters.OutwardDate]
					   ,[JourneyParameters.OutwardTime]
					   ,[JourneyParameters.OutwardArrDep]
					   ,[JourneyParameters.ReturnDate]
					   ,[JourneyParameters.ReturnTime]
					   ,[JourneyParameters.ReturnArrDep]
					   ,[SubmittedDateTime])
					VALUES
					   (@BatchId
					   ,NULL
					   ,@userSuppliedUniqueId
					   ,@originType
					   ,@origin
					   ,@destinationType
					   ,@destination
					   ,@outwardDate
					   ,@outwardTimeAsString
					   ,@outwardArrDep
					   ,null
					   ,null
					   ,null
					   ,@Date)
				END
			END TRY
			BEGIN CATCH
				SELECT @badRows = @badRows + 1
				
				-- Insert a batch detail record
				INSERT INTO BatchRequestDetail
					   ([BatchId]
					   ,[BatchDetailStatusId]
					   ,[UserSuppliedUniqueId]
					   ,[ErrorMessages])
				 VALUES
					   (@BatchId
					   ,4 -- validation error
					   ,@userSuppliedUniqueId
					   ,'Journey details could not be processed')
			END CATCH
		END

		FETCH NEXT FROM Cursor_Details INTO
			@userSuppliedUniqueId,
			@originType,
			@origin,
			@destinationType,
			@destination,
			@outwardDate,
			@outwardTimeAsString,
			@outwardArrDep,
			@returnDate,
			@returnTimeAsString,
			@returnArrDep
	END
	
	CLOSE Cursor_Details
	DEALLOCATE Cursor_Details

	-- Update the summary with the number of lines.
	UPDATE BatchRequestSummary
		SET NumberOfRequests = @lines,
			NumberOfInvalidRequests = @badRows
		WHERE BatchId = @BatchId
		
	-- return the batch id and queued position
	DECLARE @PaddedBatchId nvarchar(50)
	SELECT @PaddedBatchId = BatchJourneyPlanner.dbo.PadBatchId(@BatchId)
	
	DECLARE @Position int
	SELECT @Position = COUNT(*) + 1
		FROM BatchRequestSummary
		WHERE BatchId < @BatchId
		AND BatchStatusId < 3 -- ie queued or in progress
		
	DECLARE @ReturnPosition nvarchar(20)
	SELECT @ReturnPosition = CONVERT(nvarchar(20), @Position)
	SELECT @ReturnPosition = @ReturnPosition + 
		CASE
			WHEN LEN(@ReturnPosition) > 1 AND (LEFT(RIGHT(@ReturnPosition, 2), 1) = '1') THEN 'th'
			WHEN RIGHT(@Position, 1) = '1' THEN 'st'
			WHEN RIGHT(@Position, 1) = '2' THEN 'nd'
			WHEN RIGHT(@Position, 1) = '3' THEN 'rd'
			ELSE  'th'
		END
	
	-- Set the batch as available
	UPDATE BatchRequestSummary
	SET ProcessorId = NULL
	WHERE BatchId = @BatchId
	
	SELECT @PaddedBatchId, @ReturnPosition
	RETURN 0
END


GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1997
SET @ScriptDesc = 'BatchCoordinateDefence_ProdValues'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO
