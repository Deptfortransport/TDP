-- ***********************************************
-- NAME           : DevSettings.sql
-- DESCRIPTION    : Dev properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

-- =============================================
-- SCRIPT TO UPDATE DEV MACHINE SPECIFIC SETTINGS - OVERWRITES PRODUCTION SETTINGS
-- =============================================
DECLARE @AID_DEFAULT varchar(50) = '<DEFAULT>'
DECLARE @GID_TDP varchar(50) = 'TDPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- @AID_DEFAULT, @GID_TDP
EXEC AddUpdateProperty 'DefaultDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=PermanentPortal;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TransientPortalDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TransientPortal;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'GazetteerDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TDPGazetteer;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ContentDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=Content;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AirDataProviderDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=AirRouteMatrix;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1',  @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AdditionalDataDB', 'data source=DBM; initial catalog=AdditionalData; user id=steve; password=password; persist security info=False', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AtosAdditionalDataDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=AtosAdditionalData;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ReportStagingDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TDPReportStaging;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'CommandControlDB','Data Source=.\SQLEXPRESS;Initial Catalog=CommandAndControl;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID

-- @AID_DEFAULT, @GID_TDP
EXEC AddUpdateProperty 'ScreenFlow.PageTimeout.Path','D:\TDPortal\Codebase\TransportDirect2\TDPMobile\PageTimeout.xml', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScreenFlow.PageTimeout.Xsd','D:\TDPortal\Codebase\TransportDirect2\TDPMobile\PageTimeout.xsd', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'ScreenFlow.PageTransfer.Homepage.Host','localhost', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'StopInformation.Enabled.Switch','true', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.WebService.URL','http://localhost/TDPWebServices/DepartureBoardWebService/DepartureBoards.asmx', @AID_DEFAULT, @GID_TDP, @PartnerID, @ThemeID

-- 'TDPWeb' and 'TDPMobile', @GID_TDP
EXEC AddUpdateProperty 'CyclePlanner.WebService.URL','http://CP_JP/cycleplannerservice/service.asmx', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'CyclePlanner.WebService.URL','http://CP_JP/cycleplannerservice/service.asmx', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'LocationService.Cache.LoadLocations', 'false', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.Cache.LoadLocations', 'true', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'LocationService.Cache.LoadPostcodes', 'false', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'LocationService.Cache.LoadPostcodes', 'false', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Debug.Information.Show.Switch','true', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Debug.Information.Show.Switch','true', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'TravelNews.AutoRefresh.Refresh.Seconds', '10', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TravelNews.AutoRefresh.Refresh.Seconds', '10', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

-- 'TDPWeb', @GID_TDP
EXEC AddUpdateProperty 'Retail.RetailHandoffXml.Schema.Path','D:\TDPortal\Codebase\TransportDirect2\TDPWeb\Schemas\SJPBookingSystemHandoff.xsd', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Retail.Retailers.ShowTestRetailers.Switch', 'true', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Javscript.File.jquery', 'jquery-1.7.1.min.js', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.ScriptPath', '~/{0}Scripts/TempScripts/', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.ScriptPath.IncludeVersion', 'true', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID

-- 'TDPMobile', @GID_TDP
EXEC AddUpdateProperty 'EventDateControl.Now.Link.Switch', 'false', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Javscript.File.jquery', 'jquery-1.7.1.min.js', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.ScriptPath', '~/{0}Scripts/TempScripts/', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ScriptRepository.LocationSuggest.ScriptPath.IncludeVersion', 'true', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

-- Logging
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace','On', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace','On', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID
--EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'CCAgent', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Publishers', 'QUEUE1 FILE1', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Publishers', 'QUEUE1 FILE1', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Publishers', 'QUEUE1 FILE1', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Publishers', 'QUEUE1 FILE1', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Publishers', 'QUEUE1 FILE1', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Publishers', 'QUEUE1 FILE1', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Publishers', 'QUEUE1 FILE1', 'TDPWeb', @GID_TDP, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Publishers', 'QUEUE1 FILE1', 'TDPMobile', @GID_TDP, @PartnerID, @ThemeID

--LUL locations - updates both locations to be local
EXEC AddUpdateProperty 'DataLoader.Transfer.LUL.Location.1', 'http://127.0.0.1/Data/LUL/LondonUnderground.xml', 'DataLoader', 'DataGateway', @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DataLoader.Transfer.LUL.Location.2', 'http://127.0.0.1/Data/LUL/LondonUnderground.xml', 'DataLoader', 'DataGateway', @PartnerID, @ThemeID

-- Venue Incidents
--EXEC AddUpdateProperty 'VenueIncidents.IncidentLandingPage.Location', 'http://localhost/TDPMobile/TravelNews.aspx?nv={0}&pn=0', 'VenueIncidents', 'FileCreation', @PartnerID, @ThemeID

GO
