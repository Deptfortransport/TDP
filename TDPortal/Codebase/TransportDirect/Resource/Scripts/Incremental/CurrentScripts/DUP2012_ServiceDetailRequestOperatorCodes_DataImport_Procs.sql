-- ***********************************************
-- NAME           : DUP2012_ServiceDetailRequestOperatorCodes_DataImport_Procs.sql
-- DESCRIPTION    : Script to add journey note filter data import procedures
-- AUTHOR         : Rich Broddle
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
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ImportServiceDetailRequestOperatorCodesData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE ImportServiceDetailRequestOperatorCodesData
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- Update stored proc
ALTER PROCEDURE [dbo].[ImportServiceDetailRequestOperatorCodesData]
(
	@XML text
)
AS
BEGIN
    -- [ImportServiceDetailRequestOperatorCodesData] stored proc to insert journey note filter data
	SET NOCOUNT ON

	DECLARE @DocID int, @XMLPathData varchar(100)

	-- Loading xml document 
	EXEC sp_xml_preparedocument @DocID OUTPUT, @XML
	-- setting the node 
	SET @XMLPathData =  '/ServiceDetailRequestOperatorCodesData/ServiceDetailRequestOperatorCode'

	-- starting trasaction 
	BEGIN TRANSACTION

		-- Delete from table
		DELETE FROM [dbo].[ServiceDetailRequestOperatorCodes]

		-- Insert data
		INSERT INTO [dbo].[ServiceDetailRequestOperatorCodes](
	[OperatorCode],
	[RequestOperatorCode])
		SELECT
				X.[OperatorCode]
			   ,X.[RequestOperatorCode] 
		FROM
		OPENXML (@DocID, @XMLPathData, 2)
		WITH
		(
			   [OperatorCode] [varchar](10),
			   [RequestOperatorCode] [varchar](10)
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
		WHERE [Table] = 'ServiceDetailRequestOperatorCodes'
	END
	
END
GO

	
-- *****************************************
-- Grant permissions - will only work for relevant users as only they
-- will exist
GRANT  EXECUTE  ON [dbo].[ImportServiceDetailRequestOperatorCodesData]  TO [BBPTDPSIS\SiAdmin]
GRANT  EXECUTE  ON [dbo].[ImportServiceDetailRequestOperatorCodesData]  TO [BBPTDPS\SiAdmin]
GRANT  EXECUTE  ON [dbo].[ImportServiceDetailRequestOperatorCodesData]  TO [ACPTDPS\SiAdmin]
GRANT  EXECUTE  ON [dbo].[ImportServiceDetailRequestOperatorCodesData]  TO [BBPTDPSIS\-service-nsm]
GRANT  EXECUTE  ON [dbo].[ImportServiceDetailRequestOperatorCodesData]  TO [BBPTDPS\-service-nsm]
GRANT  EXECUTE  ON [dbo].[ImportServiceDetailRequestOperatorCodesData]  TO [ACPTDPS\-service-nsm]

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2012
SET @ScriptDesc = 'Script to add data import procedures for operator code translation data'

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