-- ***********************************************
-- NAME 		: DUP2006_JourneyNoteFilter_DataImport.sql
-- DESCRIPTION 	: Add journey note filter data import configuration for operator data
-- AUTHOR		: Mitesh Modi
-- DATE			: 19 Mar 2013
-- ************************************************

-- ************************************************************************************************
-- IMPORTANT - UPDATE THE @gatewayPath VALUE TO BE 'D:/Gateway' WHEN 
-- THIS SCRIPT IS RUN ON THE PRODUCTION SERVERS
-- ************************************************************************************************

USE [PermanentPortal]
GO

DECLARE @dataFeedId varchar(10)
DECLARE @gatewayPath varchar(10)

SET @gatewayPath = 'C:/Gateway'

SET @dataFeedId = 'loe147'
-- Data Import - journey note filter data
IF EXISTS (SELECT * FROM [IMPORT_CONFIGURATION] WHERE [DATA_FEED] = @dataFeedId)
BEGIN
	DELETE FROM [IMPORT_CONFIGURATION] WHERE [DATA_FEED] = @dataFeedId
END
IF EXISTS (SELECT * FROM [FTP_CONFIGURATION] WHERE [DATA_FEED] = @dataFeedId)
BEGIN
	DELETE FROM [FTP_CONFIGURATION] WHERE [DATA_FEED] = @dataFeedId
END


INSERT INTO [IMPORT_CONFIGURATION] VALUES (@dataFeedId, 'TransportDirect.UserPortal.JourneyControl.JourneyNoteFilterDataImporter', 'td.userportal.journeycontrol.dll', '', '', '', @gatewayPath + '/dat/Processing/' + @dataFeedId);
INSERT INTO [FTP_CONFIGURATION] VALUES (1, @dataFeedId, 'LocalHost', 'TDP28Nov', 'sI1732#3-', @gatewayPath + '/dat/Incoming/' + @dataFeedId, './' + @dataFeedId, '*.zip', 0, 1, '2012-01-01', '', 1);


GO



----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2006
SET @ScriptDesc = 'Add journey note filter data import configuration'

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