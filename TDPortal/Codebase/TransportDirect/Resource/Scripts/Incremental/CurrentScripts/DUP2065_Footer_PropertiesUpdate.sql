-- ***********************************************
-- NAME 		: DUP2065_Footer_PropertiesUpdate.sql
-- DESCRIPTION 	: Footer properties update
-- AUTHOR		: Mitesh Modi
-- DATE			: 06 Aug 2013
-- ************************************************

USE [PermanentPortal]
GO
------------------------------------------------

DECLARE @AID varchar(50) = 'Web'
DECLARE @GID varchar(50) = 'UserPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

EXEC AddUpdateProperty 'FooterControl.MobileSiteLink.Visible', 'true', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'FooterControl.MobileSiteLink.Visible', 'false', @AID, @GID, @PartnerID, 200
EXEC AddUpdateProperty 'FooterControl.MobileSiteLink.Visible', 'false', @AID, @GID, @PartnerID, 201


-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2065, 'Footer properties update'

GO