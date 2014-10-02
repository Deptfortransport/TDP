-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : AdminAreas data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

BEGIN TRANSACTION
DELETE FROM dbo.[AdminAreas]

-- Data used for Accessible stops page (when spatial querying not being used)
-- Commented out until required in future implementation

-- BULK INSERT dbo.[AdminAreas] FROM '\\fs\d$\SJPGazData\AdminAreas.csv' WITH
-- (FIELDTERMINATOR = ',' ,
-- FIRSTROW = 2,
-- FormatFile = '\\fs\d$\SJPGazData\AdminAreas.fmt') -- Using Header row

COMMIT TRANSACTION

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'AdminAreas data'
