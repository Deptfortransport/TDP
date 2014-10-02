-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : Content Group data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
Go

-- If declaring a new Group, also add definition in the code file ResourceManager.ResourceManager.cs

EXEC AddGroup 'TDPGeneral'
EXEC AddGroup 'TDPSitemap'
EXEC AddGroup 'TDPHeaderFooter'
EXEC AddGroup 'TDPJourneyOutput'
EXEC AddGroup 'TDPAnalytics'
EXEC AddGroup 'TDPMobile'

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'Content Group data'

GO