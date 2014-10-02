-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : Districts data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

BEGIN TRANSACTION
DELETE FROM dbo.[Districts]

-- Data used for Accessible stops page (when spatial querying not being used)
-- Commented out until required in future implementation

-- BULK INSERT dbo.[Districts] FROM '\\fs\d$\SJPGazData\Districts.csv' WITH
-- (FIELDTERMINATOR = ',' ,
-- FIRSTROW = 2,
-- FormatFile = '\\fs\d$\SJPGazData\Districts.fmt') -- Using Header row

COMMIT TRANSACTION

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'Districts data'
