/* First uninstall - this section is exactly the same as uninstall.sql */
USE master
GO

/* Drop the database containing our sprocs */
IF DB_ID('Properties') IS NOT NULL BEGIN
    DROP DATABASE Properties
END
GO

/* Create and populate the Properties database */
CREATE DATABASE Properties
GO

USE Properties
GO


-- CREATE Permission access for TD Developers and for Property_Admin
exec sp_grantlogin 'TRANSPORTDIRECT\TD Developers'
exec sp_addlogin 'property_adm', 'property_adm', 'Properties'
exec sp_grantdbaccess 'property_adm'
exec sp_addrolemember 'db_owner', 'property_adm'
exec sp_addrolemember 'db_owner', 'TRANSPORTDIRECT\TD Developers'

/* Create stored procedures */

-- Getrefreshrate
CREATE PROCEDURE RefreshRate
AS
SELECT PVALUE FROM PROPERTIES
WHERE PNAME='propertyservice.refreshrate'
GO

-- GetVersion
CREATE PROCEDURE GetVersion
AS
SELECT PVALUE FROM PROPERTIES
WHERE PNAME='propertyservice.version'
GO

-- ResetAfterTest
CREATE PROCEDURE ResetAfterTest
AS
UPDATE PROPERTIES SET PVALUE='1' WHERE PNAME = 'propertyservice.version'
UPDATE PROPERTIES SET PVALUE='60000' WHERE PNAME = 'propertyservice.refreshrate'
DELETE FROM PROPERTIES WHERE PNAME='propertyservice.standard.message'
GO

-- SelectApplicationProperties
CREATE PROCEDURE SelectApplicationProperties @AID char(50) 
AS
SELECT PNAME, PVALUE 
FROM PROPERTIES P
WHERE P.AID = @AID
GO

-- SelectGlobalProperties
CREATE PROCEDURE SelectGlobalProperties 
AS
SELECT PNAME, PVALUE 
FROM PROPERTIES P
WHERE P.AID IS NULL
AND P.GID IS NULL
GO

-- SelectGroup Properties
CREATE PROCEDURE SelectGroupProperties @GID char(50) 
AS
SELECT PNAME, PVALUE 
FROM PROPERTIES P
WHERE P.GID = @GID
GO

-- TestLoad1
CREATE PROCEDURE TestLoad1
AS
UPDATE PROPERTIES SET PVALUE='1' WHERE PNAME = 'propertyservice.version'
UPDATE PROPERTIES SET PVALUE='1000' WHERE PNAME = 'propertyservice.refreshrate'
INSERT INTO PROPERTIES VALUES ('propertyservice.standard.message', 'hello global', null, null) 
GO

-- TestLoad2
CREATE PROCEDURE TestLoad2
AS
INSERT INTO PROPERTIES VALUES ('propertyservice.standard.message', 'hello group', null, '1111')
GO

-- TestLoad3
CREATE PROCEDURE TestLoad3
AS
UPDATE PROPERTIES SET PVALUE=3 WHERE PNAME = 'propertyservice.version'
GO

-- TestLoad4
CREATE PROCEDURE TestLoad4
AS
UPDATE PROPERTIES SET PVALUE=5 WHERE PNAME = 'propertyservice.version'
INSERT INTO PROPERTIES VALUES ('propertyservice.standard.message', 'hello application', '1234', '1111') 
GO

/* Create Database4Exceptions if does not exist */
IF DB_ID('Properties4Exceptions') IS NULL BEGIN
	CREATE DATABASE Properties4Exceptions
END
GO


 /* Create Properties Table */
 CREATE TABLE properties(
      pName           VARCHAR(100)        NOT NULL,
      pValue		  VARCHAR(200)	      NOT NULL,
      AID			  VARCHAR(50),
      GID             VARCHAR(50)
 )




INSERT INTO properties
VALUES ('propertyservice.version', '1', 'LinkTestGenerator','ReportDataProvider');

INSERT INTO properties
VALUES ('propertyservice.refreshrate', '60000', 'LinkTestGenerator','ReportDataProvider');

INSERT INTO properties
VALUES ('ReportDataStagingDB', 'Server=.;Initial Catalog=ReportDataStaging;Trusted_Connection=true;', NULL, NULL);

INSERT INTO properties
VALUES ('ReportDataDB', 'Server=.;Initial Catalog=ReportDataStaging;Trusted_Connection=true;', NULL, NULL);

-- Event Logging Service Properties : Core Publishers ------
INSERT INTO properties VALUES ('Logging.Publisher.File', 'FILE1', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Email', '','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.EventLog', '','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Console', '', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue', 'QUEUE1','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Directory', 'C:\\TDPortal\\DEL5\\TransportDirect\\ReportDataProvider\\LinkTestEventGenerator\\FILE1', NULL, NULL);
INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Rotation', '1000', 'LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Path', 'sg015986\private$\testqueue1$', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Delivery', 'Express','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Priority', 'Normal', 'LinkTestGenerator','ReportDataProvider');


-- Event Logging Service Properties : Operational Event Publisher Assignment --
INSERT INTO properties VALUES ('Logging.Event.Operational.Info.Publishers', 'QUEUE1', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Verbose.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Warning.Publishers', 'QUEUE1', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Error.Publishers', 'QUEUE1 FILE1','LinkTestGenerator','ReportDataProvider');


-- Event Logging Service Properties : Default Publisher Assignment --
INSERT INTO properties VALUES ('Logging.Publisher.Default', 'FILE1', 'LinkTestGenerator','ReportDataProvider');


-- Event Logging Service Properties : Global Trace Levels --
INSERT INTO properties VALUES ('Logging.Event.Custom.Trace', 'On', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.TraceLevel', 'Verbose','LinkTestGenerator','ReportDataProvider');


-- Event Logging Service Properties : Custom Publishers --
INSERT INTO properties VALUES ('Logging.Publisher.Custom', 'EMAIL', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Custom.EMAIL.Name', 'CustomEmailPublisher', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Custom.EMAIL.WorkingDir', 'C:\\Temp', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Custom.EMAIL.Sender', 'Support@slb.com', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Custom.EMAIL.SMTPServer', 'localhost', 'LinkTestGenerator','ReportDataProvider');


-- Event Logging Service Properties : Custom Events --


INSERT INTO properties VALUES ('Logging.Event.Custom', 'LOC EMAIL GAZ LOGIN MAP PAGE RETAIL PREF JOURNEYREQUEST JOURNEYREQUESTVERBOSE JOURNEYRESULTS JOURNEYRESULTSVERBOSE JOURNEYWEBREQUEST GATE', 'LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.EMAIL.Name', 'CustomEmailEvent', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.EMAIL.Assembly', 'td.common.logging', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.EMAIL.Publishers', 'EMAIL', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.EMAIL.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Name', 'GazetteerEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Publishers', 'QUEUE1', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Trace', 'On', 'LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Name', 'LoginEvent', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Publishers', 'QUEUE1', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Trace', 'On', 'LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Name', 'MapEvent', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Assembly', 'td.reportdataprovider.tdpcustomevents', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Publishers', 'QUEUE1', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Name', 'PageEntryEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Assembly', 'td.reportdataprovider.tdpcustomevents','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Trace', 'On', 'LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Name', 'RetailerHandoffEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Assembly', 'td.reportdataprovider.tdpcustomevents','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Name', 'UserPreferenceSaveEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Assembly', 'td.reportdataprovider.tdpcustomevents','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Name', 'JourneyPlanRequestEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Assembly', 'td.userportal.journeycontrol','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Name', 'JourneyPlanRequestVerboseEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Assembly', 'td.userportal.journeycontrol','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Name', 'JourneyPlanResultsEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Assembly', 'td.userportal.journeycontrol','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Name', 'JourneyPlanResultsVerboseEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Assembly', 'td.userportal.journeycontrol','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Name', 'JourneyWebRequestEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Assembly', 'td.reportdataprovider.cjpcustomevents','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Name', 'LocationRequestEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Assembly', 'td.reportdataprovider.cjpcustomevents','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Trace', 'On','LinkTestGenerator','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Name', 'DataGatewayEvent','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Assembly', 'td.reportdataprovider.tdpcustomevents','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Publishers', 'QUEUE1','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Trace', 'On', 'LinkTestGenerator','ReportDataProvider');


-- Link Test Event Generator Properties : Number of events generated --
INSERT INTO properties VALUES ('LinkTestEventGenerator.Threads', '3', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.OperationalEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.CustomEmailEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.GazetteerEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.MapEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.RetailerHandoffEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.UserPreferenceSaveEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.JourneyPlanRequestEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.JourneyPlanRequestVerboseEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.JourneyPlanResultsEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.JourneyPlanResultsVerboseEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.JourneyWebRequestEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.LocationRequestEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.WorkloadEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.PageEntryEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.LoginEvents', '3','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.DataGatewayEvents', '3','LinkTestGenerator','ReportDataProvider');


-- Link Test Event Generator Properties : Other settings --


INSERT INTO properties VALUES ('LinkTestEventGenerator.WorkloadEventURL', 'http://localhost/WebApplication1/WebForm1.aspx','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.SessionId', '1234','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.Retailer1Id', 'Trainline', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.Retailer2Id', 'QJump', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.JWRequestId1', '666', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.JWRequestId2', '777', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.RegionCode1', 'EA', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.RegionCode2', 'EM', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.AdminArea1', '001', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.AdminArea2', '002', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.JPRequestId1', '123', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.JPRequestId2', '321', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.DataGatewayFile', 'gatefile.dat','LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.DataGatewayFeed', '2', 'LinkTestGenerator','ReportDataProvider');
INSERT INTO properties VALUES ('LinkTestEventGenerator.EmailAddresses', 'jp.scott@ntlworld.com','LinkTestGenerator','ReportDataProvider');


-- these events apply to the event receiver
INSERT INTO properties VALUES ('propertyservice.property','globalproperty','EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('propertyservice.refreshrate','60000','EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('propertyservice.version','1','EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Custom', 'TDPDB CJPDB OPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Custom.TDPDB.Name', 'TDPCustomEventPublisher', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Custom.CJPDB.Name', 'CJPCustomEventPublisher', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Custom.OPDB.Name', 'OperationalEventPublisher', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.TDPCustomEventPublisher.JourneyPlanRequestID','123 321', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Receiver.Queue','1','EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Receiver.Queue.1.Path','sg015986\private$\testqueue1$','EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom', 'LOC GAZ LOGIN MAP PAGE RETAIL PREF JOURNEYREQUEST JOURNEYREQUESTVERBOSE JOURNEYRESULTS JOURNEYRESULTSVERBOSE JOURNEYWEBREQUEST GATE REFERENCETRANSACTION WORKLOAD', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Name', 'GazetteerEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GAZ.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Name', 'LoginEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOGIN.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Name', 'MapEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.MAP.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Name', 'PageEntryEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PAGE.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Name', 'RetailerHandoffEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.RETAIL.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Name', 'UserPreferenceSaveEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.PREF.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Name', 'JourneyPlanRequestEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Assembly', 'td.userportal.journeycontrol', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Name', 'JourneyPlanRequestVerboseEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Assembly', 'td.userportal.journeycontrol', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Name', 'JourneyPlanResultsEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Assembly', 'td.userportal.journeycontrol', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Name', 'JourneyPlanResultsVerboseEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Assembly', 'td.userportal.journeycontrol', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Name', 'JourneyWebRequestEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Assembly', 'td.reportdataprovider.cjpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Publishers', 'CJPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.JOURNEYWEBREQUEST.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Name', 'LocationRequestEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Assembly', 'td.reportdataprovider.cjpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Publishers', 'CJPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.LOC.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Name', 'DataGatewayEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Publishers', 'TDPDB FILE1', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Name', 'WorkloadEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Name', 'ReferenceTransactionEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Operational.Info.Publishers', 'FILE1', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Verbose.Publishers', 'FILE1','EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Warning.Publishers', 'FILE1', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Error.Publishers', 'FILE1', 'EventReceiver','ReportDataProvider');

--
-- events for the web log reader

INSERT INTO properties VALUES ('propertyservice.property','globalproperty','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('propertyservice.refreshrate','1000','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('propertyservice.version','1','WebLogReader','ReportDataProvider');

INSERT INTO properties VALUES ('WebLogReader.Machine','m1','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('WebLogReader.Machine.m1.Name','sg015986','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('WebLogReader.ArchiveDirectory','c:\winnt\system32\logfiles\w3svc1\Archive','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('WebLogReader.LogDirectory','c:\winnt\system32\logfiles\w3svc1','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('WebLogReader.NonPageMinimumBytes','5000000','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('WebLogReader.WebPageExtensions','asp aspx htm html pdf [none]','WebLogReader','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Default', 'FILE1', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File', 'FILE1', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Directory', 'C:\\TDPortal\\DEL5\\TransportDirect\\ReportDataProvider\\LinkTestEventGenerator\\FILE1', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Rotation', '1024', 'WebLogReader','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Queue', 'QUEUE1','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Path', 'sg015986\private$\testqueue1$', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Delivery', 'Express','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Priority', 'Normal', 'WebLogReader','ReportDataProvider');
	
INSERT INTO properties VALUES ('Logging.Event.Custom.Trace', 'On','WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.TraceLevel', 'Info', 'WebLogReader','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom', 'WebLogData', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WebLogData.Name', 'WorkloadEvent', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WebLogData.Assembly', 'td.reportdataprovider.tdpcustomevents', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WebLogData.Publishers', 'QUEUE1', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WebLogData.Trace', 'On', 'WebLogReader','ReportDataProvider');
	
INSERT INTO properties VALUES ('Logging.Event.Operational.Info.Publishers', 'QUEUE1', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Verbose.Publishers', 'QUEUE1 FILE1', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Warning.Publishers', 'QUEUE1 FILE1', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Error.Publishers', 'QUEUE1 FILE1', 'WebLogReader','ReportDataProvider');
	
INSERT INTO properties VALUES ('Logging.Publisher.Email', '', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.EventLog', '', 'WebLogReader','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Console', '', 'WebLogReader','ReportDataProvider');



-- properties for transaction injector
INSERT INTO properties VALUES ('propertyservice.property','globalproperty','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('propertyservice.refreshrate','1000','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('propertyservice.version','1','TransactionInjector','ReportDataProvider');

INSERT INTO properties VALUES ('TransactionInjector.Transaction','1 2','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('TransactionInjector.Frequency','5','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('TransactionInjector.WebService','http://localhost/TDPWebServices/TransactionWebService/TDTransactionService.asmx','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('TransactionInjector.Transaction.1.Class','JourneyRequestTransaction','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('TransactionInjector.Transaction.1.Path','C:\TDPortal\DEL5\TransportDirect\ReportDataProvider\TransactionInjector\JourneyData','TransactionInjector','ReportDataProvider');	
INSERT INTO properties VALUES ('TransactionInjector.Transaction.2.Class','SoftContentTransaction','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('TransactionInjector.Transaction.2.Path','C:\TDPortal\DEL5\TransportDirect\ReportDataProvider\TransactionInjector\SoftContData','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('TransactionInjector.TemplateFileDirectory','C:\TDPortal\DEL5\TransportDirect\ReportDataProvider\TransactionInjector\templates','TransactionInjector','ReportDataProvider');
	
		
INSERT INTO properties VALUES ('Logging.Publisher.Default', 'FILE1', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File', 'FILE1', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Directory', 'C:\\TDPortal\\DEL5\\TransportDirect\\ReportDataProvider\\LinkTestEventGenerator\\FILE1', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Rotation', '1024', 'TransactionInjector','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Queue', 'QUEUE1','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Path', 'sg015986\private$\testqueue1$', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Delivery', 'Express','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Priority', 'Normal', 'TransactionInjector','ReportDataProvider');


INSERT INTO properties VALUES ('Logging.Publisher.Email', '', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.EventLog', '','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Console', '', 'TransactionInjector','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.Trace', 'On','TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.TraceLevel', 'Verbose', 'TransactionInjector','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom', 'C1', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.C1.Name', 'ReferenceTransactionEvent', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.C1.Assembly', 'td.reportdataprovider.tdpcustomevents', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.C1.Publishers', 'QUEUE1', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.C1.Trace', 'On', 'TransactionInjector','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Custom', '', 'TransactionInjector','ReportDataProvider');
	
INSERT INTO properties VALUES ('Logging.Event.Operational.TraceLevel', 'Info', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Info.Publishers', 'QUEUE1', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Verbose.Publishers', 'QUEUE1 FILE1', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Warning.Publishers', 'QUEUE1 FILE1', 'TransactionInjector','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Error.Publishers', 'QUEUE1 FILE1', 'TransactionInjector','ReportDataProvider');

-- properties for report data importer
INSERT INTO properties
VALUES ('ReportDataStagingDB', 'Server=.;Initial Catalog=ReportDataStaging;Trusted_Connection=true;','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES('ReportDataDB','Server=SCHLUMBE-F9F9E3;Initial Catalog=ReportData;Trusted_Connection=true;','ReportDataImporter','ReportDataProvider');
	
INSERT INTO properties VALUES('ReportDataImporter.CJPWebRequestWindow','1','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES('ReportDataImporter.DBConnectionString','DRIVER={SQL Server};SERVER=schlumbe-f9f9e3;Trusted_Connection=Yes','ReportDataImporter','ReportDataProvider');

INSERT INTO properties VALUES('propertyservice.refreshrate','1000','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES('propertyservice.version','1','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES('propertyservice.property','globalproperty','ReportDataImporter','ReportDataProvider');
	
INSERT INTO properties VALUES ('Logging.Publisher.Default', 'FILE1', 'ReportDataImporter','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Queue', 'QUEUE1','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Path', 'sg015986\private$\testqueue1$', 'ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Delivery', 'Express','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Queue.QUEUE1.Priority', 'Normal', 'ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom', '','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.Trace', 'On','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.TraceLevel', 'Info', 'ReportDataImporter','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Operational.Info.Publishers', 'QUEUE1', 'ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Verbose.Publishers', 'QUEUE1 FILE1', 'ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Warning.Publishers', 'QUEUE1 FILE1', 'ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Error.Publishers', 'QUEUE1 FILE1', 'ReportDataImporter','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.File', 'FILE1', 'ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Directory', 'C:\\TDPortal\\DEL5\\TransportDirect\\ReportDataProvider\\LinkTestEventGenerator\\FILE1','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Rotation', '1024', 'ReportDataImporter','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Email', '', 'ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.EventLog', '','ReportDataImporter','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Console', '', 'ReportDataImporter','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Custom', '', 'ReportDataImporter','ReportDataProvider');

-- report staging archiver

INSERT INTO properties VALUES('propertyservice.refreshrate','1000','ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES('propertyservice.version','1','ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES('propertyservice.property','globalproperty','ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties
VALUES ('ReportDataStagingDB', 'Server=.;Initial Catalog=ReportDataStaging;Trusted_Connection=true;','ReportStagingDataArchiver','ReportDataProvider');
  		
INSERT INTO properties VALUES ('Logging.Publisher.Default', 'FILE1', 'ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Email', '', 'ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.EventLog', '','ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.Console', '', 'ReportStagingDataArchiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Queue', '','ReportStagingDataArchiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.Custom', '', 'ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom', '','ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.Trace', 'Off','ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.TraceLevel', 'Info', 'ReportStagingDataArchiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Operational.Info.Publishers', 'FILE1', 'ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Verbose.Publishers', 'FILE1 FILE1', 'ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Warning.Publishers', 'FILE1', 'ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Operational.Error.Publishers', 'FILE1', 'ReportStagingDataArchiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Publisher.File', 'FILE1', 'ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Directory', 'C:\\TDPortal\\DEL5\\TransportDirect\\ReportDataProvider\\LinkTestEventGenerator\\FILE1','ReportStagingDataArchiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Publisher.File.FILE1.Rotation', '1024', 'ReportStagingDataArchiver','ReportDataProvider');
