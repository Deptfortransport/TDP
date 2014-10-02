-- ***********************************************
-- NAME           : DUP2007_JourneyNoteFilter_DataImport_Procs.sql
-- DESCRIPTION    : Script to add journey note filter data import procedures
-- AUTHOR         : Mitesh Modi
-- DATE           : 19 Mar 2013
-- ***********************************************

-- ************************************************************************************************
-- THIS SCRIPT MUST BE AMENDED TO ASSIGN PERMISSIONS TO THE CORRECT USER.
-- SEE BOTTOM OF SCRIPT.
-- ************************************************************************************************

USE [TransientPortal]
GO

-- *****************************************
-- Create stored proc if needed
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ImportJourneyNoteFilterData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE ImportJourneyNoteFilterData
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- Update stored proc
ALTER PROCEDURE [dbo].[ImportJourneyNoteFilterData]
(
	@XML text
)
AS
BEGIN
    -- [ImportJourneyNoteFilterData] stored proc to insert journey note filter data
	SET NOCOUNT ON

	DECLARE @DocID int, @XMLPathData varchar(100)

	-- Loading xml document 
	EXEC sp_xml_preparedocument @DocID OUTPUT, @XML
	-- setting the node 
	SET @XMLPathData =  '/JourneyNoteFilterData/JourneyNoteFilter'

	-- starting trasaction 
	BEGIN TRANSACTION

		-- Delete from table
		DELETE FROM [dbo].[JourneyNoteFilter]

		-- Insert data
		INSERT INTO [dbo].[JourneyNoteFilter](
				 [Mode]
				,[Region]
				,[AccessibleOnly]
				,[FilterText])
		SELECT
				X.[Mode]
			   ,X.[Region] 
			   ,X.[AccessibleOnly]
			   ,X.[FilterText]
		FROM
		OPENXML (@DocID, @XMLPathData, 2)
		WITH
		(
			   [Mode] [varchar](20),
			   [Region] [varchar](100),
			   [AccessibleOnly] [bit],
			   [FilterText] [varchar](250)
		) X 
		
	-- Removing xml doc from memory
	EXEC sp_xml_removedocument @DocID
	
	IF @@ERROR<>0
		ROLLBACK TRANSACTION
	ELSE
	BEGIN
		COMMIT TRANSACTION		
		
		UPDATE ChangeNotification
		SET Version = Version + 1
		WHERE [Table] = 'JourneyNoteFilter'
	END
	
END
GO

	
-- *****************************************
-- Grant permissions - will only work for relevant users as only they
-- will exist
GRANT  EXECUTE  ON [dbo].[ImportJourneyNoteFilterData]  TO [BBPTDPSIS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[ImportJourneyNoteFilterData]  TO [BBPTDPS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[ImportJourneyNoteFilterData]  TO [ACPTDPS\ASPUSER_S]

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2007
SET @ScriptDesc = 'Script to add journey note filter data import procedures'

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