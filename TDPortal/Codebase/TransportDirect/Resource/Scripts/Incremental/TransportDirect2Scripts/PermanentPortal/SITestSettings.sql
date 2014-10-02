-- ***********************************************
-- NAME           : SITESTSettings.sql
-- DESCRIPTION    : SITest properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

-- =============================================
-- SCRIPT TO UPDATE SITEST SPECIFIC SETTINGS - OVERWRITES PRODUCTION SETTINGS
-- =============================================
DECLARE @AID_DEFAULT varchar(50) = '<DEFAULT>'
DECLARE @GID_TDP varchar(50) = 'TDPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- 'TDPMobile', @GID_TDP
EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.ScriptPath', 'http://sitestmaps.transportdirect.info/output/javascript/', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'ScreenFlow.PageTransfer.Homepage.Host','sitest.transportdirect.info', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID

-- Logging
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace','On', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace','On', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Publishers', 'QUEUE1 FILE1', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Publishers', 'QUEUE1 FILE1', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Publishers', 'QUEUE1 FILE1', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Publishers', 'QUEUE1 FILE1', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Publishers', 'QUEUE1 FILE1', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Publishers', 'QUEUE1 FILE1', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Publishers', 'QUEUE1 FILE1', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Publishers', 'QUEUE1 FILE1', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

GO
