-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : TDPWeb EventLogging properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- 'TDPWeb' EventLogging properties
------------------------------------------------

DECLARE @AID varchar(50) = 'TDPWeb'
DECLARE @GID varchar(50) = 'TDPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- Publishers
EXEC AddUpdateProperty 'Logging.Publisher.Default',	'FILE1', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Publisher.Console', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Custom', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Email', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.EventLog', 'EVENTLOG1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.File', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Queue', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID

-- Publisher Configurations
EXEC AddUpdateProperty 'Logging.Publisher.EventLog.EVENTLOG1.Name', 'Application', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.EventLog.EVENTLOG1.Source', @AID, @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.EventLog.EVENTLOG1.Machine', '.', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Publisher.File.FILE1.Directory', 'D:\TDPortal', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.File.FILE1.Rotation', '20000', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Publisher.Queue.QUEUE1.Path', '.\Private$\TDSecondaryQueue', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Queue.QUEUE1.Delivery', 'Recoverable', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Queue.QUEUE1.Priority', 'Normal', @AID, @GID, @PartnerID, @ThemeID

-- Event Logging Level and Publisher mapping
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel', 'Warning', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Verbose.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Info.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Warning.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Error.Publishers', 'FILE1 EVENTLOG1', @AID, @GID, @PartnerID, @ThemeID

-- Custom Events
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom', 'PAGE JOURNEYREQUEST JOURNEYRESULTS CYCLEREQUEST CYCLERESULT REPEATVISITOR RETAIL LANDING GATEWAY GAZ GIS REFTRANS WORKLOAD STOPEVENT NORESULTS', @AID, @GID, @PartnerID, @ThemeID

-- Custom Events Configuration
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Name', 'PageEntryEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.PAGE.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYREQUEST.Name', 'JourneyPlanRequestEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYREQUEST.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYREQUEST.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYREQUEST.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYRESULTS.Name', 'JourneyPlanResultsEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYRESULTS.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYRESULTS.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.JOURNEYRESULTS.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLEREQUEST.Name', 'CyclePlannerRequestEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLEREQUEST.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLEREQUEST.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLEREQUEST.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLERESULT.Name', 'CyclePlannerResultEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLERESULT.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLERESULT.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.CYCLERESULT.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.REPEATVISITOR.Name', 'RepeatVisitorEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REPEATVISITOR.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REPEATVISITOR.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REPEATVISITOR.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.RETAIL.Name', 'RetailerHandoffEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RETAIL.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RETAIL.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RETAIL.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Name', 'LandingPageEntryEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.LANDING.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GATEWAY.Name', 'DataGatewayEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GATEWAY.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GATEWAY.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GATEWAY.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Name', 'GazetteerEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GAZ.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Name', 'GISQueryEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.GIS.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.REFTRANS.Name', 'ReferenceTransactionEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REFTRANS.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REFTRANS.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.REFTRANS.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.WORKLOAD.Name', 'WorkloadEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.WORKLOAD.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.WORKLOAD.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.WORKLOAD.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.STOPEVENT.Name', 'StopEventRequestEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.STOPEVENT.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.STOPEVENT.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.STOPEVENT.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Event.Custom.NORESULTS.Name', 'NoResultsEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.NORESULTS.Assembly', 'tdp.reporting.events', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.NORESULTS.Publishers', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.NORESULTS.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'TDPWeb EventLogging properties data'