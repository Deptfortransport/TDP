-- ***********************************************
-- NAME 		: DUP2089_AccessibleLinks_Content_Update.sql
-- DESCRIPTION 	: Accessible Mode links page content update for Telecabine
-- AUTHOR		: Mitesh Modi
-- DATE			: 02 Jan 2014
-- ************************************************

USE [Content]
GO
------------------------------------------------

EXEC AddtblContent 1, 1, 'langStrings', 'JourneyAccessibilityLinksControl.ImagePath.Telecabine',
'/Web2/App_Themes/TransportDirect/images/gifs/journeyPlanning/en/telecabine.png',
'/Web2/App_Themes/TransportDirect/images/gifs/journeyPlanning/en/telecabine.png'

EXEC AddtblContent 1, 1, 'langStrings', 'JourneyAccessibilityLinksControl.ModeName.Telecabine',
'Cable car',
'Car cebl'

EXEC AddtblContent 1, 1, 'langStrings', 'JourneyAccessibilityLinksControl.TitleText.Telecabine',
'Cable car',
'Car cebl'


GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2089, 'Accessible Mode links page content update for Telecabine'

GO