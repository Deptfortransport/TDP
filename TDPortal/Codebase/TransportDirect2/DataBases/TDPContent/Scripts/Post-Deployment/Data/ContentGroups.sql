-- =============================================
-- Script Template
-- =============================================

USE TDPContent
Go

-- If declaring a new Group, also add definition in the code file ResourceManager.ResourceManager.cs

EXEC AddGroup 'General'
EXEC AddGroup 'Sitemap'
EXEC AddGroup 'HeaderFooter'
EXEC AddGroup 'JourneyOutput'
EXEC AddGroup 'Analytics'
EXEC AddGroup 'Mobile'

GO