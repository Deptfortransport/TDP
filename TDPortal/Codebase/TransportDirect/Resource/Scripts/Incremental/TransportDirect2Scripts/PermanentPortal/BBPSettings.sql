-- ***********************************************
-- NAME           : BBPSettings.sql
-- DESCRIPTION    : BBP DRTest properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

-- =============================================
-- SCRIPT TO UPDATE BBP DRTEST SPECIFIC SETTINGS - OVERWRITES PRODUCTION SETTINGS
-- =============================================
DECLARE @AID_DEFAULT varchar(50) = '<DEFAULT>'
DECLARE @GID_TDP varchar(50) = 'TDPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- 'TDPMobile', @GID_TDP
EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.ScriptPath', 'http://testmaps.transportdirect.info/output/javascript/', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'ScreenFlow.PageTransfer.Homepage.Host','test.transportdirect.info', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID

GO
