-- =============================================
-- Script Template
-- =============================================


USE TDPContent
Go


EXEC AddChangeNotificationTable 'Content'
EXEC AddChangeNotificationTable 'ContentGroup'
EXEC AddChangeNotificationTable 'ContentOverride'

GO