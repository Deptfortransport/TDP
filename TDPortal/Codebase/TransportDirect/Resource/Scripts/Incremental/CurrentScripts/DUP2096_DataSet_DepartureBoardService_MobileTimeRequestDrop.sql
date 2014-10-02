-- ***********************************************
-- NAME           : DUP2096_DataSet_DepartureBoardService_MobileTimeRequestDrop.sql
-- DESCRIPTION    : DataSet update for DepartureBoardService MobileTimeRequestDrop
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Feb 2014
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- 'MobileTimeRequestDrop' dataset
------------------------------------------------

DELETE FROM [dbo].[DropDownLists]
WHERE DataSet = 'MobileTimeRequestDrop'

INSERT INTO [dbo].[DropDownLists] VALUES ('MobileTimeRequestDrop', 'Today', 'TimeToday', 1, 1, 0, 1)

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2096, 'DataSet update for DepartureBoardService MobileTimeRequestDrop'