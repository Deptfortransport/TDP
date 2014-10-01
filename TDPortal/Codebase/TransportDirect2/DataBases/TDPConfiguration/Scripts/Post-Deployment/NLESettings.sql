-- =============================================
-- SCRIPT TO UPDATE NLE SPECIFIC SETTINGS 
-- Not run during deployment in any configuration but to be copied with release files and 
-- manually run during deployment to NLE.
-- =============================================


USE [TDPConfiguration] 
GO


-- TDPWeb - Logging
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace','On', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'TDPWeb', 'UserPortal'

-- TDPWeb - Debug information mode on
EXEC AddUpdateProperty 'Debug.Information.Show.Switch','true', 'TDPWeb', 'UserPortal'
EXEC AddUpdateProperty 'Debug.Information.Show.Switch','true', 'TDPMobile', 'UserPortal'

-- CCAgent - Logging
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'CCAgent', 'UserPortal'

-- CoordinateConvertorService - Logging
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel','Verbose', 'CoordinateConvertorService', 'UserPortal'

-- Venue Incidents
EXEC AddUpdateProperty 'VenueIncidents.IncidentLandingPage.Location', 'http://m.test-travel.london2012.com/TravelNews.aspx?nv={0}&pn=0', 'VenueIncidents', 'FileCreation'

GO