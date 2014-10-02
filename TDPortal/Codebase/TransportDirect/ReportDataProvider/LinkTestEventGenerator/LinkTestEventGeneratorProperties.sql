-- Loads Properties and Data used by the Link Test Event Generator --

USE PermanentPortal
GO

-- Number of threads --

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.Threads', '2', 'LinkTestGenerator', 'ReportDataProvider');

-- Workload Event URL. This page must be hosted on the machine on which the generator is executed --

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.WorkloadEventURL', 'http://localhost/WebApplication1/WebForm1.aspx', 'LinkTestGenerator', 'ReportDataProvider');


-- Event Dummy Data --

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.SessionId', 'SESH', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.Retailer1Id', 'Trainline', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.Retailer2Id', 'QJump', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.RegionCode1', 'EA', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.RegionCode2', 'EM', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.AdminArea1', '001', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.AdminArea2', '002', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.JPRequestId1', '0000-0000-0001-', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.JPRequestId2', '0000-0000-0002-', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.DataGatewayFile', 'IF009_20031112010203.zip', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.DataGatewayFeed', 'SERCO', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.EmailAddresses', 'geaton@slb.com', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.JourneyWebRequestDurationMs', '500', 'LinkTestGenerator', 'ReportDataProvider');


-- Event Volumes --

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.OperationalEvents', '3', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.CustomEmailEvents', '0', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.GazetteerEvents', '3', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.MapEvents', '3', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.RetailerHandoffEvents', '3', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.UserPreferenceSaveEvents', '3', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.WebLogEntries', '3', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.PageEntryEvents', '3', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.LoginEvents', '3', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('LinkTestEventGenerator.DataGatewayEvents', '3', 'LinkTestGenerator', 'ReportDataProvider');



-- Event Logging Service Properties : Core Publishers ------

INSERT INTO properties
 VALUES ('Logging.Publisher.File', 'FILE1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.Email', '', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.EventLog', '', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.Console', '', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.Queue', 'Queue1 Queue2', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.File.FILE1.Directory', 'C:\TDPortal', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.File.FILE1.Rotation', '1000', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO Properties
 VALUES('Logging.Publisher.Queue.Queue1.Path', '.\Private$\TDPrimaryQueue', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO Properties
 VALUES('Logging.Publisher.Queue.Queue1.Delivery', 'Express', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO Properties
 VALUES('Logging.Publisher.Queue.Queue1.Priority', 'Normal', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO Properties
 VALUES('Logging.Publisher.Queue.Queue2.Path', '.\Private$\TDInjectorQueue', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO Properties
 VALUES('Logging.Publisher.Queue.Queue2.Delivery', 'Express', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO Properties
 VALUES('Logging.Publisher.Queue.Queue2.Priority', 'Normal', 'LinkTestGenerator', 'ReportDataProvider');


-- Event Logging Service Properties : Operational Event Publisher Assignment --
INSERT INTO properties
 VALUES ('Logging.Event.Operational.Info.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Operational.Verbose.Publishers', 'FILE1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Operational.Warning.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Operational.Error.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');

-- Event Logging Service Properties : Default Publisher Assignment --
INSERT INTO properties
 VALUES ('Logging.Publisher.Default', 'FILE1', 'LinkTestGenerator', 'ReportDataProvider');

-- Event Logging Service Properties : Global Trace Levels --
INSERT INTO properties
 VALUES ('Logging.Event.Custom.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Operational.TraceLevel', 'Info', 'LinkTestGenerator', 'ReportDataProvider');

-- Event Logging Service Properties : Custom Publishers --
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom', 'EMAIL', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom.EMAIL.Name', 'CustomEmailPublisher', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom.EMAIL.WorkingDir', 'C:\Temp', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom.EMAIL.Sender', 'TDLinkTestEventGenerator@slb.com', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom.EMAIL.SMTPServer', 'localhost', 'LinkTestGenerator', 'ReportDataProvider');

-- Event Logging Service Properties : Custom Events --
INSERT INTO properties
 VALUES ('Logging.Event.Custom', 'GATE EMAIL GAZ LOGIN MAP PAGE RETAIL PREF JOURNEYREQUEST JOURNEYREQUESTVERBOSE JOURNEYRESULTS JOURNEYRESULTSVERBOSE LOCATIONREQUEST JOURNEYWEBREQUEST', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.EMAIL.Name', 'CustomEmailEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.EMAIL.Assembly', 'td.common.logging', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.EMAIL.Publishers', 'EMAIL', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.EMAIL.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.GAZ.Name', 'GazetteerEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GAZ.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GAZ.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GAZ.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOGIN.Name', 'LoginEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOGIN.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOGIN.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOGIN.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.MAP.Name', 'MapEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.MAP.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.MAP.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.MAP.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.PAGE.Name', 'PageEntryEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PAGE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PAGE.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PAGE.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.RETAIL.Name', 'RetailerHandoffEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.RETAIL.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.RETAIL.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.RETAIL.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.PREF.Name', 'UserPreferenceSaveEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PREF.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PREF.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PREF.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Name', 'JourneyPlanRequestEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Assembly', 'td.userportal.journeycontrol', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Name', 'JourneyPlanRequestVerboseEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Assembly', 'td.userportal.journeycontrol', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Name', 'JourneyPlanResultsEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Assembly', 'td.userportal.journeycontrol', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Name', 'JourneyPlanResultsVerboseEvent', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Assembly', 'td.userportal.journeycontrol', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Publishers', 'Queue1', 'LinkTestGenerator', 'ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Trace', 'On', 'LinkTestGenerator', 'ReportDataProvider');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.GATE.Name', 'DataGatewayEvent', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GATE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GATE.Publishers', 'Queue1', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GATE.Trace', 'On', 'LinkTestGenerator','ReportDataProvider');


INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Name', 'JourneyWebRequestEvent', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Assembly', 'td.reportdataprovider.cjpcustomevents', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Publishers', 'Queue1', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Trace', 'On', 'LinkTestGenerator','ReportDataProvider');


INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOCATIONREQUEST.Name', 'LocationRequestEvent', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOCATIONREQUEST.Assembly', 'td.reportdataprovider.cjpcustomevents', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOCATIONREQUEST.Publishers', 'Queue1', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOCATIONREQUEST.Trace', 'On', 'LinkTestGenerator','ReportDataProvider');


-- Event Receiver properties (these differ from the dev properties in that 2 queues are serviced
-- InjectorEventReceiver Properties. Use for EventReceiver on ONE of Web Servers
-- This will receive messages from the MSMQ on the Web Server AND from the Injector MSMQ.

INSERT INTO Properties VALUES('Receiver.Queue', 'SourceQueue1 SourceQueue2', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Receiver.Queue.SourceQueue1.Path', '.\Private$\TDPrimaryQueue', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Receiver.Queue.SourceQueue2.Path', '.\Private$\TDInjectorQueue', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.Queue', '', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Email', '', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog', 'EventLog1', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Console', '', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File', 'File1', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom', 'TDPDB CJPDB OPDB', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Name', 'Application', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Source', 'InjectorEventReceiver', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Machine', '.', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Directory', 'C:\TDPortal', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Rotation', '1000', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.Custom.TDPDB.Name', 'TDPCustomEventPublisher', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom.CJPDB.Name', 'CJPCustomEventPublisher', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom.OPDB.Name', 'OperationalEventPublisher', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.TDPCustomEventPublisher.JourneyPlanRequestID', '', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.Default', 'File1', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Verbose.Publishers', 'File1 EventLog1', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Info.Publishers', 'File1', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Warning.Publishers', 'File1', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Error.Publishers', 'File1 EventLog1', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Event.Custom', 'LOC GAZ LOGIN MAP PAGE RETAIL PREF JOURNEYREQUEST JOURNEYREQUESTVERBOSE JOURNEYRESULTS JOURNEYRESULTSVERBOSE JOURNEYWEBREQUEST GATE REFERENCETRANSACTION WORKLOAD ROE', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Name', 'GazetteerEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Name', 'LoginEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Name', 'MapEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Name', 'PageEntryEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Name', 'RetailerHandoffEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Name', 'UserPreferenceSaveEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Name', 'JourneyPlanRequestEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Assembly', 'td.userportal.journeycontrol', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Name', 'JourneyPlanRequestVerboseEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Assembly', 'td.userportal.journeycontrol', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Name', 'JourneyPlanResultsEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Assembly', 'td.userportal.journeycontrol', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Name', 'JourneyPlanResultsVerboseEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Assembly', 'td.userportal.journeycontrol', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Name', 'JourneyWebRequestEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Assembly', 'td.reportdataprovider.cjpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Publishers', 'CJPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Name', 'LocationRequestEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Assembly', 'td.reportdataprovider.cjpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Publishers', 'CJPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Name', 'DataGatewayEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Name', 'WorkloadEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Name', 'ReferenceTransactionEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Publishers', 'TDPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.ROE.Name', 'ReceivedOperationalEvent', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.ROE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.ROE.Publishers', 'OPDB', 'InjectorEventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.ROE.Trace', 'On', 'InjectorEventReceiver','ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Event.Operational.TraceLevel', 'Verbose', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom.Trace', 'On', 'InjectorEventReceiver', 'ReportDataProvider');

-- End InjectorEventReceiver

GO


