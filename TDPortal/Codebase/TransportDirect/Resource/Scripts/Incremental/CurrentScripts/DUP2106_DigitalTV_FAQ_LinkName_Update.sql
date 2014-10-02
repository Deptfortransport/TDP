-- ***********************************************
-- NAME           : DUP2106_DigitalTV_FAQ_LinkName_Update
-- DESCRIPTION    : Update to FAQ link to remove Digital TV name
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 June 2014
-- ***********************************************
USE [TransientPortal]
GO

-- Hide Digital TV icon
EXEC [UpdateSuggestionLinkDisplayText] 'FAQ.MobileDeviceService', 
'Using Transport Direct services on your PDA/Mobile',
'Defnyddio gwasanaethau teithio byw Transport Direct ar PDA / ffôn symudol'

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2106, 'Update to FAQ link to remove Digital TV name'