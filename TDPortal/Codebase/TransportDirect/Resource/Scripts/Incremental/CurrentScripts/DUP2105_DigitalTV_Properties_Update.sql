-- ***********************************************
-- NAME           : DUP2105_DigitalTV_Properties_Update
-- DESCRIPTION    : Digital TV properties update to hide icon on Tips Tools page
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 June 2014
-- ***********************************************
USE [PermanentPortal]
GO


DECLARE @AID varchar(50) = '<DEFAULT>'
DECLARE @GID varchar(50) = '<DEFAULT>'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- Hide Digital TV icon
EXEC AddUpdateProperty 'DigitalTVInfoAvailable', 'False', @AID, @GID, @PartnerID, @ThemeID

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2105, 'Digital TV properties update to hide icon on Tips Tools page'