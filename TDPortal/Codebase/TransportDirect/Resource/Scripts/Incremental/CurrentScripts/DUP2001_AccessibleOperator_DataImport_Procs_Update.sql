-- ***********************************************
-- NAME           : DUP2001_AccessibleOperator_DataImport_Procs_Update.sql
-- DESCRIPTION    : Script to update accessible operator data import procedures, increasing varchar column sizes
-- AUTHOR         : Mitesh Modi
-- DATE           : 11 Mar 2013
-- ***********************************************

-- ************************************************************************************************
-- THIS SCRIPT MUST BE AMENDED TO ASSIGN PERMISSIONS TO THE CORRECT USER.
-- SEE BOTTOM OF SCRIPT.
-- ************************************************************************************************

USE [TransientPortal]
GO

-- *****************************************
-- Create stored proc if needed
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ImportAccessibleOperatorData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE ImportAccessibleOperatorData
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- Update stored proc
ALTER PROCEDURE [dbo].[ImportAccessibleOperatorData]
(
	@XML text
)
AS
BEGIN
    -- ImportAccessibleLocations strored proc to insert accessible locations data
	SET NOCOUNT ON

	DECLARE @DocID int, @XMLPathData varchar(100)

	-- Loading xml document 
	EXEC sp_xml_preparedocument @DocID OUTPUT, @XML
	-- setting the node 
	SET @XMLPathData =  '/AccessibleOperatorData/AccessibleOperator'

	-- starting trasaction 
	BEGIN TRANSACTION

		-- Delete from accessible locations table
		DELETE FROM [dbo].[AccessibleOperators]

		-- Insert accessible locations data
		INSERT INTO [dbo].[AccessibleOperators](
				 [OperatorCode]
				,[ServiceNumber]
				,[Mode]
				,[Region]
				,[WEFDate]
				,[WEUDate]
				,[WheelchairBooking]
				,[AssistanceBooking]
				,[BookingUrl]
				,[BookingNumber])
		SELECT
				X.[TravelineOperatorCode]
			   ,X.[LegServiceNumber] 
			   ,X.[LegMode]
			   ,X.[TravelineRegions]
			   ,CAST( ISNULL(NULLIF(LTRIM(RTRIM(X.[WEFDate])), ''), '19000101') AS DATE) -- Set a date if null
			   ,CAST( ISNULL(NULLIF(LTRIM(RTRIM(X.[WEUDate])), ''), '20501231') AS DATE) -- Set a date if null
			   ,CASE WHEN X.[WheelchairBooking] = 'TRUE' THEN 1 ELSE 0 END
			   ,CASE WHEN X.[AssistanceServiceBooking] = 'TRUE' THEN 1 ELSE 0 END
			   ,X.[BookingLineURL]
			   ,X.[BookingPhoneNumber]
		FROM
		OPENXML (@DocID, @XMLPathData, 2)
		WITH
		(
			   [TravelineOperatorCode] [varchar](50),
			   [LegServiceNumber] [varchar](150),
			   [LegMode] [nvarchar](20),
			   [TravelineRegions] [varchar](30),
			   [WEFDate] [varchar](20),
			   [WEUDate] [varchar](20),
			   [WheelchairBooking] [varchar](5),
			   [AssistanceServiceBooking] [varchar](5),
			   [BookingLineURL] [varchar](400),
			   [BookingPhoneNumber] [varchar](100)
		) X 
		
	-- Removing xml doc from memorry
	EXEC sp_xml_removedocument @DocID
	
	IF @@ERROR<>0
		ROLLBACK TRANSACTION
	ELSE
	BEGIN
		COMMIT TRANSACTION		
		
		UPDATE ChangeNotification
		SET Version = Version + 1
		WHERE [Table] = 'AccessibleOperatorCatalogue'
	END
	
END
GO

	
-- *****************************************
-- Grant permissions - will only work for relevant users as only they
-- will exist
GRANT  EXECUTE  ON [dbo].[ImportAccessibleOperatorData]  TO [BBPTDPSIS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[ImportAccessibleOperatorData]  TO [BBPTDPS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[ImportAccessibleOperatorData]  TO [ACPTDPS\ASPUSER_S]

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2001
SET @ScriptDesc = 'Script to update accessible operator data import procedures, increasing varchar column sizes'

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