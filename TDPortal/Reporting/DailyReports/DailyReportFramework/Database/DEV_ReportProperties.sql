-- ***********************************************
-- NAME 	: DEV_ReportProperties.sql
-- DESCRIPTION 	: Report Properties update for DEV
--				: This should be run after main report properties sql
-- ************************************************
--
USE [ReportProperties]
GO

-- Change email address to dev
UPDATE ReportProperties 
SET [value] = 'mitesh.modi@atos.net'
WHERE [propertykey] = 'MailRecipient'

UPDATE ReportProperties 
SET [value] = 'reports@transportdirect.info'
WHERE [propertykey] = 'MailAddress'

-- Change email server to dev
UPDATE ReportProperties 
SET [value] = 'localhost'
WHERE [propertykey] = 'smtpServer'

-- Change all report filepaths to be for dev
UPDATE ReportProperties
SET [value] = REPLACE([value], 'C:\', 'D:\TDPortal\Reporting\Reports\Daily\')
WHERE [propertykey] = 'FilePath'
AND [reportNumber] in (
		SELECT reportNumber FROM ReportProperties 
		WHERE propertykey = 'Frequency' 
		AND value = 'D')

UPDATE ReportProperties
SET [value] = REPLACE([value], 'C:\', 'D:\TDPortal\Reporting\Reports\Weekly\')
WHERE [propertykey] = 'FilePath'
AND [reportNumber] in (
		SELECT reportNumber FROM ReportProperties 
		WHERE propertykey = 'Frequency' 
		AND value = 'W')
		
UPDATE ReportProperties
SET [value] = REPLACE([value], 'C:\', 'D:\TDPortal\Reporting\Reports\Monthly\')
WHERE [propertykey] = 'FilePath'
AND [reportNumber] in (
		SELECT reportNumber FROM ReportProperties 
		WHERE propertykey = 'Frequency' 
		AND value = 'M')
		