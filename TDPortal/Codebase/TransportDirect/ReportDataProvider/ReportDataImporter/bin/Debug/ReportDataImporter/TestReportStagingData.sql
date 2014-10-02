-- Drop existing reporting data

delete from ReportServer.Reporting.dbo.GazetteerEvents
delete from ReportServer.Reporting.dbo.JourneyPlanLocationEvents
delete from ReportServer.Reporting.dbo.JourneyPlanModeEvents
delete from ReportServer.Reporting.dbo.JourneyProcessingEvents
delete from ReportServer.Reporting.dbo.JourneyWebRequestEvents
delete from ReportServer.Reporting.dbo.LoginEvents
delete from ReportServer.Reporting.dbo.MapEvents
delete from ReportServer.Reporting.dbo.OperationalEvents
delete from ReportServer.Reporting.dbo.PageEntryEvents
delete from ReportServer.Reporting.dbo.ReferenceTransactionEvents
delete from ReportServer.Reporting.dbo.RetailerHandoffEvents
delete from ReportServer.Reporting.dbo.RoadPlanEvents
delete from ReportServer.Reporting.dbo.WorkloadEvents
delete from ReportServer.Reporting.dbo.DataGatewayEvents
delete from ReportServer.Reporting.dbo.UserPreferenceSaveEvents
delete from ReportServer.Reporting.dbo.SessionEvents
delete from ReportServer.Reporting.dbo.RTTIEvents

USE ReportStagingDB

-- Drop existing staging data
Delete From GazetteerEvent
Delete From JourneyPlanRequestEvent
Delete From JourneyPlanResultsEvent
Delete From JourneyWebRequestEvent
Delete From LocationRequestEvent
Delete From LoginEvent
Delete From MapEvent
Delete From OperationalEvent
Delete From PageEntryEvent
Delete From ReferenceTransactionEvent
Delete From RetailerHandoffEvent
Delete From WorkloadEvent
Delete From DataGatewayEvent
Delete From UserPreferenceSaveEvent
Delete From JourneyPlanRequestVerboseEvent
Delete From JourneyPlanResultsVerboseEvent
Delete from RttiEvent

-- Set dates in audit table which are earlier than those used in the test data (otherwise importer will be prevented from importing!)
update ReportStagingDataAudit
set event = convert(datetime, '2005-02-21 17:05:06', 121)
where rsdatid = 1

update ReportStagingDataAudit
set event = convert(datetime, '2005-02-21', 121)
where rsdatid = 2

--------------------------------------------

Insert Into JourneyPlanRequestVerboseEvent
(JourneyPlanRequestId, JourneyRequestData, SessionId, UserLoggedOn, TimeLogged)
Select
'0000-0000-0000-0000-0001', 'Request data for Journey Plan Request 01...', 'SessionXYZ', 0, '2005-03-22 14:01:14'

Insert Into JourneyPlanResultsVerboseEvent
(JourneyPlanRequestId, JourneyResultsData, SessionId, UserLoggedOn, TimeLogged)
Select
'0000-0000-0000-0000-0001', 'Results data for Journey Plan Request 01...', 'SessionXYZ', 0, '2005-03-22 14:01:18' 

Insert Into JourneyPlanResultsEvent
(JourneyPlanRequestId, ResponseCategory, SessionId, UserLoggedOn, TimeLogged)
Select
'0000-0000-0000-0000-0001', 'Results', '1A', 1, '2005-03-22 00:00:40'
Union
Select
'0000-0000-0000-0000-0002', 'ZeroResults', '1B', 1, '2005-03-22 01:00:41'
Union
Select
'0000-0000-0000-0000-0003', 'Failure', '1C', 0, '2005-03-22 01:01:30'
Union
Select
'0000-0000-0000-0000-0004', 'Results', '1D', 1, '2005-03-22 00:00:50'
Union
Select
'0000-0000-0000-0000-0005', 'ZeroResults', '1E', 0, '2005-03-22 00:01:00'
Union
Select
'0000-0000-0000-0000-0006', 'Failure', '1A', 1, '2005-03-22 00:01:00'
Union
Select
'0000-0000-0000-0000-0007', 'Results', '1B', 1, '2005-03-22 00:00:35'
Union
Select
'0000-0000-0000-0000-0008', 'ZeroResults', '1C', 0, '2005-03-22 01:02:30'
Union
Select
'0000-0000-0000-0000-0009', 'Failure', '1D', 1, '2005-03-22 01:03:00'
Union
Select
'0000-0000-0000-0000-0010', 'Results', '1E', 0, '2005-03-22 01:03:30'

--------------------------------------------

Insert Into OperationalEvent
(SessionId, Message, MachineName, AssemblyName, MethodName, TypeName, Level, Category, Target, TimeLogged)
Select
'123', 'An operational event message.', 'MYMachine', 'MyAssemblyName', 'MyMethodName', 'MyTypeName', 'Warning', 'Database', 'MyTarget', '2005-03-22 14:00:00'
--------------------------------------------


Insert Into DataGatewayEvent
(FeedId, SessionId, FileName, TimeStarted, TimeFinished, SuccessFlag, ErrorCode, UserLoggedOn, TimeLogged)
Select
'AFC743', '345', 'MyImportFilename', '2005-03-22 14:00:14', '2005-03-22 14:08:00', 1, 0, 1, '2005-03-22 14:08:14'
--------------------------------------------


Insert Into UserPreferenceSaveEvent
(EventCategory, SessionId, TimeLogged)
Select
'FareOptions', '111', '2005-03-22 14:08:00'
Union
Select
'FareOptions', '112', '2005-03-22 14:09:00'
Union
Select
'FareOptions', '113', '2005-03-22 14:11:00'
Union
Select
'FareOptions', '114', '2005-03-22 14:17:00'
Union
Select
'JourneyPlanningOptions', '121', '2005-03-22 14:08:00'
Union
Select
'JourneyPlanningOptions', '122', '2005-03-22 14:19:00'
Union
Select
'JourneyPlanningOptions', '123', '2005-03-22 14:35:00'
Union
Select
'JourneyPlanningOptions', '124', '2005-03-22 14:53:00'
Union
Select
'News', '311', '2005-03-22 14:08:00'
Union
Select
'News', '312', '2005-03-23 14:08:00'
Union
Select
'News', '313', '2005-03-25 14:08:00'

--------------------------------------------


Insert Into GazetteerEvent
(EventCategory, SessionId, UserLoggedOn, TimeLogged)
Select
'GazetteerLocality', '01', 1, '2005-03-22 14:47:14'
Union
Select
'GazetteerLocality', '02', 0, '2005-03-22 14:47:14'
Union
Select
'GazetteerLocality', '03', 1, '2005-03-22 14:48:14'
Union
Select
'GazetteerLocality', '04', 0, '2005-03-22 14:49:14'
Union
Select
'GazetteerLocality', '10', 1, '2005-03-22 15:08:14'
Union
Select
'GazetteerLocality', '11', 1, '2005-03-22 15:23:14'
Union
Select
'GazetteerLocality', '12', 1, '2005-03-22 15:39:36'
Union
Select
'GazetteerLocality', '13', 1, '2005-03-22 15:48:23'
Union
Select
'GazetteerLocality', '20', 0, '2005-03-23 15:35:56'
Union
Select
'GazetteerLocality', '21', 0, '2005-03-25 16:56:34'
Union
Select
'GazetteerAddress', '31', 0, '2005-03-22 14:47:14'
Union
Select
'GazetteerPostCode', '32', 1,  '2005-03-22 14:47:14'
Union
Select
'GazetteerPointOfInterest', '33', 0, '2005-03-22 14:47:14'
Union
Select
'GazetteerMajorStations', '34', 1,  '2005-03-22 14:47:14'
Union
Select
'GazetteerAllStations', '35', 0, '2005-03-22 14:47:14'
--------------------------------------------

Insert Into JourneyPlanRequestEvent
(JourneyPlanRequestId, Air, Bus, Car, Coach, Cycle, Drt, Ferry, Metro, Rail, Taxi, Tram, Underground, Walk, SessionId, UserLoggedOn, TimeLogged)
Select
'0000-0000-0000-0000-0001', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, '1A', 1, '2005-03-22 00:00:00'
Union
Select
'0000-0000-0000-0000-0002', 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1B', 1, '2005-03-22 01:00:08'
Union
Select
'0000-0000-0000-0000-0003', 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1C', 1, '2005-03-22 01:00:19'
Union
Select
'0000-0000-0000-0000-0004', 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1D', 1, '2005-03-22 00:00:20'
Union
Select
'0000-0000-0000-0000-0005', 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1E', 1, '2005-03-22 00:00:30'
Union
Select
'0000-0000-0000-0000-0006', 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1A', 1, '2005-03-22 00:00:20'
Union
Select
'0000-0000-0000-0000-0007', 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1B', 1, '2005-03-22 00:00:05'
Union
Select
'0000-0000-0000-0000-0008', 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1C', 1, '2005-03-22 01:02:00'
Union
Select
'0000-0000-0000-0000-0009', 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1D', 1, '2005-03-22 01:02:30'
Union
Select
'0000-0000-0000-0000-0010', 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '1E', 0, '2005-03-22 01:03:00'

------------------------------------------------------------

Insert Into LocationRequestEvent
(JourneyPlanRequestId, PrepositionCategory, AdminAreaCode, RegionCode, TimeLogged)
Select
'0000-0000-0000-0000-0001', 'From', '001', 'SW', '2005-03-22 14:01:14'
Union
Select
'0000-0000-0000-0000-0001', 'To', '002', 'NW', '2005-03-22 14:01:14'
Union
Select
'0000-0000-0000-0000-0001', 'Via', '083', 'NW', '2005-03-22 14:01:14'
Union
Select
'0000-0000-0000-0000-0002', 'From', '001', 'SW', '2005-03-22 14:47:14'
Union
Select
'0000-0000-0000-0000-0003', 'From', '002', 'NW', '2005-03-22 14:47:14'
Union
Select
'0000-0000-0000-0000-0004', 'From', '083', 'NW', '2005-03-22 14:47:14'
Union
Select
'0000-0000-0000-0000-0005', 'From', '001', 'SW', '2005-03-22 14:47:14'
Union
Select
'0000-0000-0000-0000-0006', 'From', '002', 'NW', '2005-03-22 14:47:14'
Union
Select
'0000-0000-0000-0000-0007', 'From', '083', 'NW', '2005-03-22 14:47:14'
Union
Select
'0000-0000-0000-0000-0008', 'From', '001', 'SW', '2005-03-22 14:47:14'
Union
Select
'0000-0000-0000-0000-0009', 'From', '002', 'NW', '2005-03-22 15:22:22'
Union
Select
'0000-0000-0000-0000-0010', 'From', '083', 'NW', '2005-03-22 15:22:22'
----------------------------------------------------------

Insert Into MapEvent
(CommandCategory, Submitted, DisplayCategory, SessionId, UserLoggedOn, TimeLogged)
Select
'MapInitialDisplay', '2005-03-22 00:01:00', 'OSStreetView', '01', 0, '2005-03-22 00:01:02'
Union
Select
'MapInitialDisplay', '2005-03-22 00:07:00', 'OSStreetView', '02', 0, '2005-03-22 00:07:03'
Union
Select
'MapInitialDisplay', '2005-03-22 00:11:00', 'OSStreetView', '03', 0, '2005-03-22 00:11:01'
Union
Select
'MapInitialDisplay', '2005-03-22 00:14:00', 'OSStreetView', '04', 0, '2005-03-22 00:14:02'
Union
Select
'MapZoom', '2005-03-22 14:11:12', 'ScaleColourRaster250', '21', 1, '2005-03-22 14:11:14'
Union
Select
'MapZoom', '2005-03-22 14:26:12', 'ScaleColourRaster250', '22', 1, '2005-03-22 14:26:14'
Union
Select
'MapZoom', '2005-03-22 14:38:12', 'ScaleColourRaster250', '25', 1, '2005-03-22 14:38:14'
Union
Select
'MapZoom', '2005-03-22 14:57:12', 'ScaleColourRaster250', '24', 1, '2005-03-22 14:57:14'
Union
Select
'MapOverlay', '2005-03-23 14:47:12', 'MiniScale', '31', 1, '2005-03-23 14:47:14'
Union
Select
'MapOverlay', '2005-03-25 14:47:10', 'Strategi', '32', 0, '2005-03-25 14:47:14'


--------------------------------------------------------------

Insert Into PageEntryEvent
(Page, SessionId, UserLoggedOn, TimeLogged)
Select
'JourneyPlannerAmbiguity', '1', 1, '2005-03-22 14:47:14'
Union
Select
'JourneyPlannerAmbiguity', '2', 0, '2005-03-22 14:48:14'
Union
Select
'JourneyPlannerAmbiguity', '3', 1, '2005-03-22 14:49:14'
Union
Select
'JourneyPlannerAmbiguity', '4', 0, '2005-03-22 14:50:14'
Union
Select
'JourneyPlannerAmbiguity', '5', 1, '2005-03-22 14:51:14'
Union
Select
'JourneyPlannerAmbiguity', '6', 0, '2005-03-22 14:52:14'
Union
Select
'JourneyPlannerAmbiguity', '7', 1, '2005-03-22 14:53:14'
Union
Select
'JourneyPlannerAmbiguity', '8', 0, '2005-03-22 14:54:14'
Union
Select
'JourneyPlannerInput', '1', 0, '2005-03-22 15:08:14'
Union
Select
'JourneyPlannerInput', '1', 0, '2005-03-22 15:23:14'
Union
Select
'JourneyPlannerInput', '2', 0, '2005-03-22 15:41:14'
Union
Select
'JourneyPlannerInput', '2', 0, '2005-03-22 15:56:14'
Union
Select
'JourneyMap', '1', 0, '2005-03-23 14:47:14'
Union
Select
'JourneyMap', '1', 0, '2005-03-25 14:47:14'
-----------------------------------------------------

Insert Into ReferenceTransactionEvent
(EventType, ServiceLevelAgreement, Submitted, SessionId, TimeLogged, Successful)
Select
'ComplexJourneyRequest', 1, '2005-03-22 01:00:00.000', '1B', '2005-03-22 01:00:45.000', 1
Union
Select
'SimpleJourneyRequest1', 1, '2005-03-22 01:00:00.000', '1C', '2005-03-22 01:00:32.000', 0

-----------------------------------------------------------------------------------

Insert Into JourneyWebRequestEvent
(JourneyWebRequestId, SessionId, Submitted, RegionCode, Success, RefTransaction, TimeLogged, RequestType)
Select
'0000-0000-0000-0000-00011', '1A', '2005-03-22 00:00:00', 'NW', 1, 0, '2005-03-22 00:00:20', 'Journey'
Union
Select
'0000-0000-0000-0000-00012', '1A', '2005-03-22 00:00:01', 'SW', 1, 0, '2005-03-22 00:00:10', 'Journey'
Union
Select
'0000-0000-0000-0000-00013', '1A', '2005-03-22 00:00:12', 'L', 1, 0, '2005-03-22 00:00:20', 'Journey'
Union
Select
'0000-0000-0000-0000-00014', '1A', '2005-03-22 00:00:20', 'WM', 1, 0, '2005-03-22 00:00:30', 'Journey'
Union
Select
'0000-0000-0000-0000-00015', '1A', '2005-03-22 00:00:22', 'EA', 1, 0, '2005-03-22 00:00:33', 'Journey'
Union
Select
'0000-0000-0000-0000-00021', '1B', '2005-03-22 01:00:10', 'NW', 0, 1, '2005-03-22 01:00:12', 'Journey'
Union
Select
'0000-0000-0000-0000-00022', '1B', '2005-03-22 01:00:21', 'SW', 1, 1, '2005-03-22 01:00:28', 'Journey'
Union
Select
'0000-0000-0000-0000-00023', '1B', '2005-03-22 01:00:25', 'L', 1, 1, '2005-03-22 01:00:40', 'Journey'
Union
Select
'0000-0000-0000-0000-00024', '1B', '2005-03-22 01:00:33', 'WM', 1, 1, '2005-03-22 01:00:39', 'Journey'
Union
Select
'0000-0000-0000-0000-00031', '1C', '2005-03-22 01:00:20', 'NW', 0, 1, '2005-03-22 01:00:40', 'Journey'
Union
Select
'0000-0000-0000-0000-00032', '1C', '2005-03-22 01:00:25', 'NW', 1, 1, '2005-03-22 01:00:39', 'Journey'
Union
Select
'0000-0000-0000-0000-00033', '1C', '2005-03-22 01:00:30', 'EA', 1, 1, '2005-03-22 01:00:41', 'Journey'
Union
Select
'0000-0000-0000-0000-00034', '1C', '2005-03-22 01:00:35', 'EM', 0, 1, '2005-03-22 01:00:40', 'Journey'
Union
Select
'0000-0000-0000-0000-00035', '1C', '2005-03-22 01:00:40', 'L', 1, 1, '2005-03-22 01:00:45', 'Journey'
Union
Select
'0000-0000-0000-0000-00036', '1C', '2005-03-22 01:00:45', 'NE', 0, 1, '2005-03-22 01:00:47', 'Journey'
Union
Select
'0000-0000-0000-0000-00037', '1C', '2005-03-22 01:00:56', 'NW', 1, 1, '2005-03-22 01:01:01', 'Journey'
Union
Select
'0000-0000-0000-0000-00038', '1C', '2005-03-22 01:01:03', 'S', 1, 1, '2005-03-22 01:01:20', 'Journey'
-----------------------------------------------------------------------------------

Insert Into WorkloadEvent
(Requested, TimeLogged, NumberRequested)
Select
'2005-03-22 14:00:00', '2005-03-22 14:15:04', 2
Union
Select
'2005-03-22 14:00:00', '2005-03-22 14:20:02', 3
Union
Select
'2005-03-22 14:00:00', '2005-03-22 14:21:03', 4
Union
Select
'2005-03-22 14:01:00', '2005-03-22 14:22:02', 22
Union
Select
'2005-03-22 14:01:00', '2005-03-22 14:23:02', 33
Union
Select
'2005-03-22 14:01:00', '2005-03-22 14:24:02', 44
Union
Select
'2005-03-22 14:02:00', '2005-03-22 14:03:04', 222
Union
Select
'2005-03-22 14:59:00', '2005-03-22 15:00:23', 333
Union
Select
'2005-03-22 15:00:00', '2005-03-22 15:00:24', 444

-----------------------------------------------------------

Insert Into RetailerHandoffEvent
(RetailerId, SessionId, UserLoggedOn, TimeLogged)
Select
'Trainline', '01', 0, '2005-03-22 14:47:14.000'
Union
Select
'Trainline', '02', 0, '2005-03-22 14:49:14.000'
Union
Select
'Trainline', '03', 0, '2005-03-22 14:51:14.000'
Union
Select
'Trainline', '04', 0, '2005-03-22 14:53:14.000'
Union
Select
'QJump', '21', 1, '2005-03-22 15:07:14.000'
Union
Select
'QJump', '22', 1, '2005-03-22 15:23:14.000'
Union
Select
'QJump', '23', 1, '2005-03-22 15:47:14.000'
Union
Select
'QJump', '24', 1, '2005-03-22 15:56:14.000'
Union
Select
'VIRGIN', '0C', 0, '2005-03-23 13:47:14.000'
Union
Select
'GNER', '0D', 0, '2005-03-25 12:27:14.000'


-----------------------------------------------------------

INSERT INTO LoginEvent
( SessionId, TimeLogged )
SELECT
'0A', '2005-03-22 12:16:00'
UNION
SELECT
'01', '2005-03-22 12:17:00'
UNION
SELECT
'02', '2005-03-22 12:18:00'
UNION
SELECT
'0B', '2005-03-22 14:06:00'
UNION
SELECT
'0C', '2005-03-22 14:19:00'
UNION
SELECT
'0D', '2005-03-22 14:33:00'
UNION
SELECT
'0E', '2005-03-22 13:20:00'
UNION
SELECT
'0F', '2005-03-22 14:59:00'
UNION
SELECT
'0G', '2005-03-22 13:10:00'
UNION
SELECT
'1A', '2005-03-23 12:16:00'
UNION
SELECT
'11', '2005-03-23 12:17:00'
UNION
SELECT
'12', '2005-03-23 12:18:00'
UNION
SELECT
'1B', '2005-03-23 14:06:00'
UNION
SELECT
'1C', '2005-03-23 14:19:00'
UNION
SELECT
'1D', '2005-03-23 14:33:00'
UNION
SELECT
'1E', '2005-03-23 13:20:00'
UNION
SELECT
'1F', '2005-03-23 14:59:00'
UNION
SELECT
'1G', '2005-03-23 13:10:00'
UNION
SELECT
'2F', '2005-03-25 14:59:00'

-------------------------------------------------------------------
-- Inserting entries in RTTIEvent
INSERT INTO RTTIEvent (StartTime, FinishTime, DataRecievedSucessfully, TimeLogged)
VALUES     (CONVERT(DATETIME, '2005-03-23 00:00:00', 102), CONVERT(DATETIME, '2005-03-23 00:00:01', 102), 1, CONVERT(DATETIME, '2005-03-23 00:00:01', 102))

INSERT INTO RTTIEvent(StartTime, FinishTime, DataRecievedSucessfully, TimeLogged)
VALUES     (CONVERT(DATETIME, '2005-03-23 01:00:00', 102), CONVERT(DATETIME, '2005-03-23 01:00:02', 102), 1, CONVERT(DATETIME, '2005-03-23 01:00:01', 102))

INSERT INTO RTTIEvent(StartTime, FinishTime, DataRecievedSucessfully, TimeLogged)
VALUES     (CONVERT(DATETIME, '2005-03-23 02:00:00', 102), CONVERT(DATETIME, '2005-03-23 02:00:01', 102), 1, CONVERT(DATETIME, '2005-03-23 02:00:01', 102))

INSERT INTO RTTIEvent(StartTime, FinishTime, DataRecievedSucessfully, TimeLogged)
VALUES     (CONVERT(DATETIME, '2005-03-23 03:00:00', 102), CONVERT(DATETIME, '2005-03-23 03:00:01', 102), 1, CONVERT(DATETIME, '2005-03-23 03:00:01', 102))

INSERT INTO RTTIEvent(StartTime, FinishTime, DataRecievedSucessfully, TimeLogged)
VALUES     (CONVERT(DATETIME, '2005-03-23 04:00:00', 102), CONVERT(DATETIME, '2005-03-23 04:00:01', 102), 1, CONVERT(DATETIME, '2005-03-23 04:00:01', 102))

INSERT INTO RTTIEvent(StartTime, FinishTime, DataRecievedSucessfully, TimeLogged)
VALUES     (CONVERT(DATETIME, '2005-03-23 05:00:00', 102), CONVERT(DATETIME, '2005-03-23 05:00:01', 102), 1, CONVERT(DATETIME, '2005-03-23 05:00:01', 102))



----------------------------------------------------------------------------------------------
-- Adding test data for EnahncedExposedServices
INSERT INTO [ReportStagingDB].[dbo].[EnhancedExposedServiceEvent]
([PartnerId], [InternalTransactionId], [ExternalTransactionId], [ServiceType], [EventTime], 
[IsStartEvent], [CallSuccessful])
VALUES(
1, 
Convert(varchar(50), getdate()), 
'112131241243124', 'Unknown', 
getdate(), 1, 1)

INSERT INTO [ReportStagingDB].[dbo].[EnhancedExposedServiceEvent]
([PartnerId], [InternalTransactionId], [ExternalTransactionId], [ServiceType], [EventTime], 
[IsStartEvent], [CallSuccessful])
VALUES(
1, 
Convert(varchar(50), getdate()), 
'112131241243124', 'Unknown', 
getdate(), 0, 1)

-------------------------------------------------------------------------------------------------------

