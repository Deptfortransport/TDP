USE ReportStagingDB
GO

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


--------------------------------------------

Insert Into JourneyPlanResultsVerboseEvent
(JourneyPlanRequestId, JourneyResultsData, SessionId, UserLoggedOn, TimeLogged)
Select
'123', 'My journey results data...', '777', 1, '2005-03-15 14:08:32'
--------------------------------------------

Insert Into JourneyPlanRequestVerboseEvent
(JourneyPlanRequestId, JourneyRequestData, SessionId, UserLoggedOn, TimeLogged)
Select
'123', 'My journey request data...', '777', 1, '2005-03-15 14:08:14'

--------------------------------------------

--------------------------------------------

Insert Into JourneyPlanResultsEvent
(JourneyPlanRequestId, ResponseCategory, SessionId, UserLoggedOn, TimeLogged)
Select
'123', 'Results', '777', 1, '2005-03-15 14:08:14'
--------------------------------------------


Insert Into OperationalEvent
(SessionId, Message, MachineName, AssemblyName, MethodName, TypeName, Level, Category, Target, TimeLogged)
Select
'123', 'An operational event message.', 'MYMachine', 'MyAssemblyName', 'MyMethodName', 'MyTypeName', 'Warning', 'Database', 'MyTarget', '2005-03-15 14:00:00'
--------------------------------------------


Insert Into DataGatewayEvent
(FeedId, SessionId, FileName, TimeStarted, TimeFinished, SuccessFlag, ErrorCode, UserLoggedOn, TimeLogged)
Select
'AFC743', '345', 'MyImportFilename', '2005-03-15 14:00:14', '2005-03-15 14:08:00', 1, 0, 1, '2005-03-15 14:08:14'
--------------------------------------------


Insert Into UserPreferenceSaveEvent
(EventCategory, SessionId, TimeLogged)
Select
'FareOptions', '777', '2005-03-15 14:08:00'

--------------------------------------------


Insert Into GazetteerEvent
(EventCategory, SessionId, UserLoggedOn, TimeLogged)
Select
'GazetteerAddress', '0A', 0, '2005-03-15 14:47:14'
Union
Select
'GazetteerPostCode', '1A', 1,  '2005-03-15 14:47:14'
Union
Select
'GazetteerPointOfInterest', '2A', 0, '2005-03-15 14:47:14'
Union
Select
'GazetteerMajorStations', '3A', 1,  '2005-03-15 14:47:14'
Union
Select
'GazetteerAllStations', '4A', 0, '2005-03-15 14:47:14'
Union
Select
'GazetteerLocality', '5A', 1,  '2005-03-15 14:47:14'
Union
Select
'GazetteerLocality', '6A', 0, '2005-03-15 14:47:14'
Union
Select
'GazetteerLocality', '7A', 1,  '2005-03-15 14:47:14'
Union
Select
'GazetteerLocality', '8A', 0, '2005-03-15 14:47:14'
Union
Select
'GazetteerLocality', '9A', 1,  '2005-03-15 14:47:14'
Union
Select
'GazetteerLocality', '0B', 0, '2005-03-15 14:47:14'
Union
Select
'GazetteerLocality', '1B', 1,  '2005-03-15 14:47:14'
Union
Select
'GazetteerLocality', '2B', 0, '2005-03-15 14:53:27'
Union
Select
'GazetteerLocality', '3B', 0, '2005-03-15 14:57:36'
Union
Select
'GazetteerLocality', '4B', 0, '2005-03-15 13:13:23'
Union
Select
'GazetteerLocality', '5B', 0, '2005-03-15 15:35:56'
Union
Select
'GazetteerLocality', '6B', 0, '2005-03-15 16:56:34'
Union
Select
'GazetteerLocality', '7B', 0, '2005-03-15 12:39:12'
--------------------------------------------

Insert Into JourneyPlanRequestEvent
(JourneyPlanRequestId, Air, Bus, Car, Coach, Cycle, Drt, Ferry, Metro, Rail, Taxi, Tram, Underground, Walk, SessionId, UserLoggedOn, TimeLogged)
Select
'Request1', 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, '0A', 0, '2005-03-15 14:47:14'
Union
Select
'Request2', 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, '1A', 1, '2005-03-15 14:47:14'
Union
Select
'Request3', 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, '2A', 0, '2005-03-15 14:47:14'
Union
Select
'Request4', 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, '3A', 1, '2005-03-15 14:47:14'
Union
Select
'Request5', 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, '4A', 0, '2005-03-15 14:47:14'
Union
Select
'Request6', 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, '5A', 1, '2005-03-15 14:47:14'
Union
Select
'Request7', 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '6A', 0, '2005-03-15 14:47:14'
Union
Select
'Request8', 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '7A', 1, '2005-03-15 14:47:14'
Union
Select
'Request9', 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, '8A', 0, '2005-03-15 14:47:14'
Union
Select
'Request10', 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, '9A', 1, '2005-03-15 14:47:14'
Union
Select
'Request11', 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, '0A', 0, '2005-03-15 15:22:22'
Union
Select
'Request12', 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, '1A', 1, '2005-03-15 15:22:22'
Union
Select
'Request13', 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, '2A', 0, '2005-03-15 15:22:22'
Union
Select
'Request14', 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, '3A', 1, '2005-03-15 15:22:22'
Union
Select
'Request15', 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, '4A', 0, '2005-03-15 15:22:22'
Union
Select
'Request16', 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, '5A', 1, '2005-03-15 15:22:22'
Union
Select
'Request17', 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '6A', 0, '2005-03-15 15:22:22'
Union
Select
'Request18', 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '7A', 1, '2005-03-15 15:22:22'
Union
Select
'Request19', 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, '8A', 0, '2005-03-15 15:22:22'
Union
Select
'Request20', 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, '9A', 1, '2005-03-15 15:22:22'
-----------------------------------------------------------------------------------

Insert Into JourneyWebRequestEvent
(JourneyWebRequestId, SessionId, Submitted, RegionCode, Success, RefTransaction, TimeLogged, RequestType)
Select
'Request1', '0A', '2005-03-15 00:00:01', 'NW', 0, 0, '2005-03-15 15:22:22', 'Request1'
Union
Select
'Request2', '0B', '2005-03-15 00:05:12', 'WM', 0, 1, '2005-03-15 15:22:22', 'Request2'
Union
Select
'Request3', '0C', '2005-03-15 00:12:27', 'NW', 1, 0, '2005-03-15 15:20:22', 'Request3'
Union
Select
'Request4', '0D', '2005-03-15 00:15:04', 'L', 1, 1, '2005-03-15 12:12:22', 'Request5'
Union
Select
'Request5', '0E', '2005-03-15 00:24:32', 'SW', 0, 1, '2005-03-15 13:29:22', 'Request6'
Union
Select
'Request6', '0F', '2005-03-15 01:02:45', 'NW', 1, 0, '2005-03-15 15:00:22', 'Request7'
Union
Select
'Request7', '0G', '2005-03-15 01:13:56', 'NW', 0, 0, '2005-03-15 15:59:22', 'Request8'

------------------------------------------------------------

Insert Into LocationRequestEvent
(JourneyPlanRequestId, PrepositionCategory, AdminAreaCode, RegionCode, TimeLogged)
Select
'Request1', 'From', '001', 'SW', '2005-03-15 14:47:14'
Union
Select
'Request2', 'To', '002', 'NW', '2005-03-15 14:47:14'
Union
Select
'Request3', 'Via', '083', 'NW', '2005-03-15 14:47:14'
Union
Select
'Request4', 'From', '001', 'SW', '2005-03-15 14:47:14'
Union
Select
'Request5', 'From', '002', 'NW', '2005-03-15 14:47:14'
Union
Select
'Request6', 'From', '083', 'NW', '2005-03-15 14:47:14'
Union
Select
'Request7', 'From', '001', 'SW', '2005-03-15 15:22:22'
Union
Select
'Request8', 'From', '002', 'NW', '2005-03-15 15:22:22'
Union
Select
'Request9', 'From', '083', 'NW', '2005-03-15 15:22:22'
Union
Select
'Request10', 'From', '001', 'SW', '2005-03-15 15:22:22'
Union
Select
'Request11', 'From', '002', 'NW', '2005-03-15 15:22:22'
Union
Select
'Request12', 'From', '083', 'NW', '2005-03-15 15:22:22'
----------------------------------------------------------

Insert Into MapEvent
(CommandCategory, Submitted, DisplayCategory, SessionId, UserLoggedOn, TimeLogged)
Select
'MapInitialDisplay', '2003-11-01 00:00:01', 'OSStreetView', '0A', 0, '2005-03-15 14:47:14'
Union
Select
'MapPan', '2003-11-01 00:00:01', 'ScaleColourRaster50', '1A', 1, '2005-03-15 14:47:14'
Union
Select
'MapZoom', '2003-11-01 00:05:12', 'ScaleColourRaster250', '2A', 0, '2005-03-15 14:57:14'
Union
Select
'MapOverlay', '2003-11-01 00:05:12', 'MiniScale', '3A', 1, '2005-03-15 14:47:14'
Union
Select
'MapOverlay', '2003-11-01 00:12:27', 'Strategi', '4A', 0, '2005-03-15 14:47:14'


--------------------------------------------------------------

Insert Into PageEntryEvent
(Page, SessionId, UserLoggedOn, TimeLogged)
Select
'JourneyPlannerInput', '0A', 0, '2005-03-15 14:47:14'
Union
Select
'JourneyPlannerAmbiguity', '1A', 1, '2005-03-15 14:47:14'
Union
Select
'JourneySummary', '2A', 0, '2005-03-15 14:47:14'
Union
Select
'JourneyPlannerLocationMap', '3A', 1, '2005-03-15 14:47:14'
Union
Select
'JourneyDetails', '4A', 0, '2005-03-15 14:47:14'
Union
Select
'JourneyMap', '5A', 1, '2005-03-15 14:47:14'
Union
Select
'JourneyAdjust', '6A', 0, '2005-03-15 14:47:14'
Union
Select
'JourneyFares', '7A', 1, '2005-03-15 14:47:14'
Union
Select
'CompareAdjustedJourney', '8A', 0, '2005-03-15 14:47:14'
Union
Select
'DetailedLegMap', '9A', 1, '2005-03-15 14:47:14'
Union
Select
'WaitPage', '0B', 0, '2005-03-15 14:47:14'
Union
Select
'PrintableJourneySummary', '1B', 1, '2005-03-15 14:47:14'
Union
Select
'PrintableJourneyDetails', '2B', 0, '2005-03-15 14:47:14'
Union
Select
'PrintableJourneyMaps', '3B', 1, '2005-03-15 14:47:14'
Union
Select
'ClaimsInputPage', '4B', 0, '2005-03-15 14:47:14'
Union
Select
'ClaimPrintPage', '5B', 1, '2005-03-15 14:47:14'
-----------------------------------------------------

Insert Into ReferenceTransactionEvent
(EventType, ServiceLevelAgreement, Submitted, SessionId, TimeLogged)
Select
'SimpleJourneyRequest1', 0, '2005-03-15 00:00:00.000', '0A', '2005-03-15 00:00:01.000'
Union
Select
'SimpleJourneyRequest1', 0, '2005-03-15 00:01:00.000', '1A', '2005-03-15 00:01:01.000'
Union
Select
'SimpleJourneyRequest1', 0, '2005-03-15 00:02:00.000', '2A', '2005-03-15 00:02:01.000'
Union
Select
'SimpleJourneyRequest1', 0, '2005-03-15 00:03:00.000', '3A', '2005-03-15 00:03:01.000'
Union
Select
'SimpleJourneyRequest1', 0, '2005-03-15 00:04:00.000', '4A', '2005-03-15 00:04:01.000'
Union
Select
'SimpleJourneyRequest1', 0, '2005-03-15 00:05:00.000', '5A', '2005-03-15 00:05:01.000'
Union
Select
'SimpleJourneyRequest1', 0, '2005-03-15 00:06:00.000', '6A', '2005-03-15 00:06:01.000'
Union
Select
'SimpleJourneyRequest1', 1, '2005-03-15 00:07:00.000', '7A', '2005-03-15 00:07:01.000'
Union
Select
'SimpleJourneyRequest1', 1, '2005-03-15 00:08:00.000', '8A', '2005-03-15 00:08:01.000'
Union
Select
'SimpleJourneyRequest1', 1, '2005-03-15 00:11:00.000', '9A', '2005-03-15 00:11:01.000'
Union
Select
'SimpleJourneyRequest1', 1, '2005-03-15 00:10:00.000', '0B', '2005-03-15 00:10:01.000'
Union
Select
'SimpleJourneyRequest1', 1, '2005-03-15 00:11:00.000', '1B', '2005-03-15 00:11:01.000'
Union
Select
'SimpleJourneyRequest1', 1, '2005-03-15 00:12:00.000', '2B', '2005-03-15 00:12:01.000'
Union
Select
'SimpleJourneyRequest1', 1, '2005-03-15 00:13:00.000', '3B', '2005-03-15 00:13:01.000'
Union
Select
'SimpleJourneyRequest1', 0, '2005-03-15 00:14:00.000', '4B', '2005-03-15 00:14:01.000'
-----------------------------------------------------------

Insert Into WorkloadEvent
(Requested, TimeLogged, NumberRequested)
Select
'2003-01-01 00:00:01', '2005-03-15 14:47:14', 1
Union
Select
'2003-01-01 00:01:12', '2005-03-15 14:47:14', 1
Union
Select
'2003-01-01 00:05:00', '2005-03-15 14:47:14', 1
Union
Select
'2003-01-01 00:12:00', '2005-03-15 14:47:14', 1
Union
Select
'2003-01-01 00:15:00', '2005-03-15 14:47:14', 1
Union
Select
'2003-01-01 00:17:25', '2005-03-15 14:47:14', 1
Union
Select
'2003-01-01 01:00:00', '2005-03-15 14:47:14', 1
Union
Select
'2003-01-01 01:12:56', '2005-03-15 14:47:14', 1
Union
Select
'2003-01-01 00:27:00', '2005-03-15 14:47:14', 1

-----------------------------------------------------------

Insert Into RetailerHandoffEvent
(RetailerId, SessionId, UserLoggedOn, TimeLogged)
Select
'Trainline', '0A', 0, '2005-03-15 14:47:14.000'
Union
Select
'QJump', '0B', 0, '2005-03-15 13:07:14.000'
Union
Select
'NationalExpress', '0C', 0, '2005-03-15 13:47:14.000'
Union
Select
'ScottishCityLink', '0D', 0, '2005-03-15 12:27:14.000'
Union
Select
'ScottishCityLink', '0E', 0, '2005-03-15 13:59:14.000'
Union
Select
'ScottishCityLink', '0F', 0, '2005-03-15 14:45:14.000'
Union
Select
'ScottishCityLink', '0G', 0, '2005-03-15 14:40:14.000'


-----------------------------------------------------------

INSERT INTO LoginEvent
( SessionId, UserLoggedOn, TimeLogged )
SELECT
'0A', 1, '2005-03-15 14:19:00'
UNION
SELECT
'0B', 0, '2005-03-15 12:00:00'
UNION
SELECT
'0C', 0, '2005-03-15 14:00:00'
UNION
SELECT
'0D', 0, '2005-03-15 14:27:00'
UNION
SELECT
'0E', 1, '2005-03-15 13:20:00'
UNION
SELECT
'0F', 1, '2005-03-15 14:59:00'
UNION
SELECT
'0G', 1, '2005-03-15 13:10:00'