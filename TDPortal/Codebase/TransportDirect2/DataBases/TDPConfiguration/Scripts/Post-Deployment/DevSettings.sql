-- =============================================
-- SCRIPT TO UPDATE DEV MACHINE SPECIFIC SETTINGS - OVERWRITES PRODUCTION SETTINGS IN DB PROJECT IF BUILD CONFIGURATION = DEBUG
-- =============================================

-- '<Default>', '<Default>'
EXEC AddUpdateProperty 'DefaultDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TDPConfiguration;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', '<Default>', '<Default>'
EXEC AddUpdateProperty 'TransientPortalDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TDPTransientPortal;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', '<Default>', '<Default>'
EXEC AddUpdateProperty 'GazetteerDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TDPGazetteer;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', '<Default>', '<Default>'
EXEC AddUpdateProperty 'ContentDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TDPContent;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', '<Default>', '<Default>'
EXEC AddUpdateProperty 'ReportStagingDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TDPReportStaging;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', '<Default>', '<Default>'
EXEC AddUpdateProperty 'CommandControlDB','Data Source=.\SQLEXPRESS;Initial Catalog=CommandAndControl;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', '<Default>', '<Default>'


-- 'TDPWeb' and 'TDPMobile', 'UserPortal'
EXEC AddUpdateProperty 'CoordinateConvertor.WebService.URL', 'http://localhost/TDPWebServices/CoordinateConvertorService/CoordinateConvertor.asmx', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'CoordinateConvertor.WebService.URL', 'http://localhost/TDPWebServices/CoordinateConvertorService/CoordinateConvertor.asmx', 'TDPMobile', 'UserPortal'

EXEC AddUpdateProperty 'CyclePlanner.WebService.URL','http://CP_JP/cycleplannerservice/service.asmx', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'CyclePlanner.WebService.URL','http://CP_JP/cycleplannerservice/service.asmx', 'TDPMobile', 'UserPortal'

EXEC AddUpdateProperty 'LocationService.Cache.LoadLocations', 'false', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'LocationService.Cache.LoadLocations', 'false', 'TDPMobile', 'UserPortal'

EXEC AddUpdateProperty 'LocationService.Cache.LoadPostcodes', 'false', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'LocationService.Cache.LoadPostcodes', 'false', 'TDPMobile', 'UserPortal'

EXEC AddUpdateProperty 'Debug.Information.Show.Switch','true', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'Debug.Information.Show.Switch','true', 'TDPMobile', 'UserPortal'

EXEC AddUpdateProperty 'TravelNews.AutoRefresh.Refresh.Seconds', '10', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'TravelNews.AutoRefresh.Refresh.Seconds', '10', 'TDPMobile', 'UserPortal'

-- 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'Retail.RetailHandoffXml.Schema.Path','D:\TDPortal\Codebase\TransportDirect2\TDPWeb\Schemas\SJPBookingSystemHandoff.xsd', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'Retail.Retailers.ShowTestRetailers.Switch', 'true', 'TDPWeb', 'UserPortal'

-- 'TDPMobile', 'UserPortal'
EXEC AddUpdateProperty 'EventDateControl.Now.Link.Switch', 'false', 'TDPMobile', 'UserPortal'

-- Logging
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace','On', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace','On', 'TDPMobile', 'UserPortal'
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'TDPMobile', 'UserPortal'
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'CCAgent', 'UserPortal'
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'CoordinateConvertorService', 'UserPortal'

EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Publishers', 'QUEUE1 FILE1', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Publishers', 'QUEUE1 FILE1', 'TDPMobile', 'UserPortal'

EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Publishers', 'QUEUE1 FILE1', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Publishers', 'QUEUE1 FILE1', 'TDPMobile', 'UserPortal'

--TravelNews & LUL locations - updates both locations to be local
EXEC AddUpdateProperty 'DataLoader.Transfer.TravelNews.Location.1', 'http://127.0.0.1/Data/TravelNews/TravelNews.xml', 'DataLoader', 'DataGateway'
EXEC AddUpdateProperty 'DataLoader.Transfer.TravelNews.Location.2', 'http://127.0.0.1/Data/TravelNews/TravelNews.xml', 'DataLoader', 'DataGateway'
EXEC AddUpdateProperty 'DataLoader.Transfer.LUL.Location.1', 'http://127.0.0.1/Data/LUL/LondonUnderground.xml', 'DataLoader', 'DataGateway'
EXEC AddUpdateProperty 'DataLoader.Transfer.LUL.Location.2', 'http://127.0.0.1/Data/LUL/LondonUnderground.xml', 'DataLoader', 'DataGateway'

-- Venue Incidents
EXEC AddUpdateProperty 'VenueIncidents.IncidentLandingPage.Location', 'http://localhost/TDPMobile/TravelNews.aspx?nv={0}&pn=0', 'VenueIncidents', 'FileCreation'

GO
