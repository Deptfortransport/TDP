-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : ChangeNotification table data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
Go

EXEC AddChangeNotificationTable 'Content'
EXEC AddChangeNotificationTable 'ContentGroup'
EXEC AddChangeNotificationTable 'ContentOverride'

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'ChangeNotification table data'