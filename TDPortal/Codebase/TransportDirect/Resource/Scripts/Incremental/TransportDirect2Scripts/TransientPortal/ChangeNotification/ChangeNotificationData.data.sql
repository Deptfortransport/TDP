-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : ChangeNotification data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

EXEC AddChangeNotificationTable 'UndergroundStatusImport', 1

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'ChangeNotification data'