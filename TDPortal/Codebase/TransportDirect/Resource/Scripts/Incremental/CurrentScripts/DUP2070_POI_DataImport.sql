-- ***********************************************
-- NAME 		: DUP2070_POI_DataImport.sql
-- DESCRIPTION 	: Add POI data import configuration
-- AUTHOR		: Mitesh Modi
-- DATE			: 20 Aug 2013
-- ************************************************

USE [PermanentPortal]
GO

DECLARE @dataFeedId varchar(10)
DECLARE @gatewayPath varchar(10)

SET @gatewayPath = 'D:/Gateway'

SET @dataFeedId = 'dtl669'
-- Data Import
IF EXISTS (SELECT * FROM [IMPORT_CONFIGURATION] WHERE [DATA_FEED] = @dataFeedId)
BEGIN
	DELETE FROM [IMPORT_CONFIGURATION] WHERE [DATA_FEED] = @dataFeedId
END
IF EXISTS (SELECT * FROM [FTP_CONFIGURATION] WHERE [DATA_FEED] = @dataFeedId)
BEGIN
	DELETE FROM [FTP_CONFIGURATION] WHERE [DATA_FEED] = @dataFeedId
END


INSERT INTO [IMPORT_CONFIGURATION] VALUES (@dataFeedId, 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/PointsOfInterestData.bat', '', '', @gatewayPath + '/dat/Processing/' + @dataFeedId);
INSERT INTO [FTP_CONFIGURATION] VALUES (1, @dataFeedId, 'LocalHost', 'TDP28Nov', 'sI1732#3-', @gatewayPath + '/dat/Incoming/' + @dataFeedId, './' + @dataFeedId, '*.zip', 0, 1, '2012-01-01', '', 1);

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2070, 'Add POI data import configuration'