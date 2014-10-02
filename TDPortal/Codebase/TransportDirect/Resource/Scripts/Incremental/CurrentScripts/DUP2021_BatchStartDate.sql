-- ***********************************************
-- NAME 		: DUP2021_BatchStartDate.sql
-- DESCRIPTION 	: Add start date for batches
-- AUTHOR		: David Lane
-- DATE			: 10 Apr 2013
-- ************************************************

USE BatchJourneyPlanner
GO

-- Start date
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BatchRequestSummary' AND COLUMN_NAME = 'StartDateTime')
BEGIN
ALTER TABLE BatchRequestSummary
ADD StartDateTime DATETIME NULL
END
GO

-- GetBatchRequestDetailsForProcessing proc
ALTER PROCEDURE [dbo].[GetBatchRequestDetailsForProcessing]
(
	@ProcessorId nvarchar(50),
	@NumberRequests int,
	@BatchMaxLineLimit int,
	@BatchMinLineLimit int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Check for a batch this processor is already doing
	DECLARE @BatchId int
	DECLARE @Date datetime
	SET @Date = GETDATE()
	
	IF (NOT EXISTS (SELECT BatchId FROM BatchRequestSummary WHERE ProcessorId = @ProcessorId))
	BEGIN
		-- Get a new batch if there is one available
		IF (EXISTS (SELECT BatchId FROM BatchRequestSummary WHERE ProcessorId IS NULL AND BatchStatusId = 1 AND NumberOfRequests <= @BatchMaxLineLimit AND NumberOfRequests >= @BatchMinLineLimit))
		BEGIN
			SELECT @BatchId = BatchId 
				FROM BatchRequestSummary 
				WHERE ProcessorId IS NULL 
				AND BatchStatusId = 1
				AND NumberOfRequests <= @BatchMaxLineLimit
				AND NumberOfRequests >= @BatchMinLineLimit
				ORDER BY QueuedDateTime DESC
				
			UPDATE BatchRequestSummary
				SET ProcessorId = @ProcessorId,
				BatchStatusId = 2,
				PercentageComplete = 0,
				StartDateTime = GETDATE()
				WHERE BatchId = @BatchId
		END
		ELSE
		BEGIN
			-- No requests available
			RETURN 0
		END
	END
	ELSE
	BEGIN
		SELECT @BatchId = BatchId
		FROM BatchRequestSummary
		WHERE ProcessorId = @ProcessorId
	END

	-- See if the batch is complete
	DECLARE @Count int
	SELECT @Count = COUNT(*)
	FROM BatchRequestDetail
	WHERE (BatchDetailStatusId IS NULL -- Pending
	OR BatchDetailStatusId = 1) -- in progress
	AND BatchId = @BatchId
		
	IF (@Count = 0)
	BEGIN
		-- Allow service to generate zip
		RETURN @BatchId
	END
	ELSE
	BEGIN
		-- Check if batch has stalled
		SELECT @Count = COUNT(*)
		FROM BatchRequestDetail
		WHERE BatchDetailStatusId IS NULL
		AND BatchId = @BatchId
		
		IF (@Count = 0)
		BEGIN
			-- resset the stalled records
			UPDATE BatchRequestDetail
			SET BatchDetailStatusId = NULL
			WHERE BatchDetailStatusId = 1
			AND BatchId = @BatchId
		END
	END

	-- Get detail records and preferences
	SELECT TOP (@NumberRequests) 
		bd.RequestId,									--0
		bs.[ReportParameters.IncludeJourneyStatistics],	--1
		bs.[ReportParameters.IncludeJourneyDetails],	--2
		bs.[ReportParameters.IncludePublicTransport],	--3
		bs.[ReportParameters.IncludeCar],				--4
		bs.[ReportParameters.IncludeCycle],				--5
		bs.[ReportParameters.ConvertToRtf],				--6
		bd.UserSuppliedUniqueId,						--7
		bd.[JourneyParameters.OriginType],				--8
		bd.[JourneyParameters.Origin],					--9
		bd.[JourneyParameters.DestinationType],			--10
		bd.[JourneyParameters.Destination],				--11
		bd.[JourneyParameters.OutwardDate],				--12
		bd.[JourneyParameters.OutwardTime],				--13
		bd.[JourneyParameters.OutwardArrDep],			--14
		bd.[JourneyParameters.ReturnDate],				--15
		bd.[JourneyParameters.ReturnTime],				--16
		bd.[JourneyParameters.ReturnArrDep]				--17
	FROM BatchRequestSummary bs
	INNER JOIN BatchRequestDetail bd
		ON bs.BatchId = bd.BatchId
	WHERE bd.BatchId = @BatchId
	AND bd.BatchDetailStatusId IS NULL -- Pending
	ORDER BY bd.RequestId ASC
	
	-- Update the status and submitted date
	UPDATE BatchRequestDetail
		SET BatchDetailStatusId = 1, -- Submitted
			SubmittedDateTime = @Date
	WHERE RequestId IN (SELECT TOP (@NumberRequests) RequestId
						FROM BatchRequestDetail bd
						WHERE bd.BatchId = @BatchId
						AND bd.BatchDetailStatusId IS NULL
						ORDER BY bd.RequestId ASC)
	
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

SET @ScriptNumber = 2021
SET @ScriptDesc = 'Batch start date'

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
-------------------------------------------
