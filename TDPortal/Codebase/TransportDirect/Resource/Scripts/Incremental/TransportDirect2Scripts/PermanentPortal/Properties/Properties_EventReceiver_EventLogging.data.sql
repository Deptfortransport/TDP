-- ***********************************************
-- NAME           : Properties_EventReceiver_EventLogging.data.sql
-- DESCRIPTION    : TDP EventReceiver EventLogging properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 01 Aug 2013
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- 'EventReceiver' properties
------------------------------------------------

DECLARE @AID varchar(50) = 'TDPEventReceiver'
DECLARE @GID varchar(50) = 'TDReporting'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- Publishers
EXEC AddUpdateProperty 'Logging.Publisher.Default',	'FILE1', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Publisher.Console', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Custom', 'TDPDB CJPDB OPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Email', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.EventLog', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.File', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Queue', '', @AID, @GID, @PartnerID, @ThemeID

-- Publisher Configurations
EXEC AddUpdateProperty 'Logging.Publisher.File.FILE1.Directory', 'D:\TDPortal', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.File.FILE1.Rotation', '1000', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Publisher.Custom.TDPDB.Name', 'TDPCustomEventPublisher', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Custom.OPDB.Name', 'TDPOperationalEventPublisher', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Custom.CJPDB.Name', 'CJPCustomEventPublisher', @AID, @GID, @PartnerID, @ThemeID

-- Event Logging Level and Publisher mapping
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel', 'Verbose', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Verbose.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Info.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Warning.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Error.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID

-- Custom Events
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom', 'PAGE JOURNEYREQUEST JOURNEYRESULTS CYCLEREQUEST CYCLERESULT REPEATVISITOR RETAIL LANDING GATEWAY GAZ GIS REFTRANS WORKLOAD STOPEVENT ROE CJPJOURNEYWEB CJPLOCATION CJPINTERNAL NORESULTS', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Name', 'PageEntryEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Publishers', 'TDPDB FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYREQUEST.Name', 'JourneyPlanRequestEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYREQUEST.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYREQUEST.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYREQUEST.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYRESULTS.Name', 'JourneyPlanResultsEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYRESULTS.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYRESULTS.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYRESULTS.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLEREQUEST.Name', 'CyclePlannerRequestEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLEREQUEST.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLEREQUEST.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLEREQUEST.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLERESULT.Name', 'CyclePlannerResultEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLERESULT.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLERESULT.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLERESULT.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.REPEATVISITOR.Name', 'RepeatVisitorEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REPEATVISITOR.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REPEATVISITOR.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REPEATVISITOR.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.RETAIL.Name', 'RetailerHandoffEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RETAIL.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RETAIL.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RETAIL.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Name', 'LandingPageEntryEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GATEWAY.Name', 'DataGatewayEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GATEWAY.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GATEWAY.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GATEWAY.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Name', 'GazetteerEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Name', 'GISQueryEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.REFTRANS.Name', 'ReferenceTransactionEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REFTRANS.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REFTRANS.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REFTRANS.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.WORKLOAD.Name', 'WorkloadEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.WORKLOAD.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.WORKLOAD.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.WORKLOAD.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.STOPEVENT.Name', 'StopEventRequestEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.STOPEVENT.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.STOPEVENT.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.STOPEVENT.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.ROE.Name', 'ReceivedOperationalEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.ROE.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.ROE.Publishers', 'OPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.ROE.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID


-- TDP CJP Custom Events
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPJOURNEYWEB.Name', 'JourneyWebRequestEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPJOURNEYWEB.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPJOURNEYWEB.Publishers', 'CJPDB FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPJOURNEYWEB.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.CJPLOCATION.Name', 'LocationRequestEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPLOCATION.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPLOCATION.Publishers', 'CJPDB FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPLOCATION.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.CJPINTERNAL.Name', 'InternalRequestEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPINTERNAL.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPINTERNAL.Publishers', 'CJPDB FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CJPINTERNAL.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.NORESULTS.Name', 'NoResultsEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.NORESULTS.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.NORESULTS.Publishers', 'TDPDB', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.NORESULTS.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID


GO
