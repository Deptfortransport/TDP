-- ***********************************************
-- NAME 	: PopulatePermanentPortalDataBaseTables.sql
-- DESCRIPTION 	: Populates PermanentPortal database tables
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/Data/PopulatePermanentPortalDataBaseTables.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:41:12   mturner
--Initial revision.

USE [PermanentPortal]
GO

--------------------------------------------------------------------
-- TRUNCATE TABLES BEING REPOPULATED
--------------------------------------------------------------------

-- Data Services
TRUNCATE TABLE DropDownLists
TRUNCATE TABLE BankHolidays
TRUNCATE TABLE Lists
TRUNCATE TABLE CategorisedHashes
TRUNCATE TABLE RetailerLookup
DELETE FROM Retailers

-- Properties
TRUNCATE TABLE properties

-- ReportDataStaging
TRUNCATE TABLE ReportStagingDataAudit
TRUNCATE TABLE ReportStagingDataAuditType
TRUNCATE TABLE ReportStagingDataType
TRUNCATE TABLE GazetteerEvent
TRUNCATE TABLE JourneyPlanRequestEvent
TRUNCATE TABLE JourneyWebRequestEvent
TRUNCATE TABLE LocationRequestEvent
TRUNCATE TABLE MapEvent
TRUNCATE TABLE PageEntryEvent
TRUNCATE TABLE ReferenceTransactionEvent
TRUNCATE TABLE WorkloadEvent
TRUNCATE TABLE LoginEvent
TRUNCATE TABLE RetailerHandoffEvent
TRUNCATE TABLE DataGatewayEvent
TRUNCATE TABLE UserPreferenceSaveEvent
TRUNCATE TABLE OperationalEvent
TRUNCATE TABLE JourneyPlanRequestVerboseEvent
TRUNCATE TABLE JourneyPlanResultsVerboseEvent
GO


--------------------------------------------------------------------
-- START DATASERVICES
--------------------------------------------------------------------

-- Insert the default and only row into ReferenceNum
 INSERT INTO ReferenceNum (RefID) VALUES (1)
GO

-- Create FromTo dropdown list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('FromToDrop','Address','AddressPostCode',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('FromToDrop','Stations','MainStationAirport',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('FromToDrop','AllStops','AllStationStops',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('FromToDrop','City','Locality',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('FromToDrop','Attraction','POI',0,5)
GO

-- Create LocationType dropdown list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('LocationTypeDrop','Address','AddressPostCode',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('LocationTypeDrop','Stations','MainStationAirport',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('LocationTypeDrop','AllStops','AllStationStops',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('LocationTypeDrop','City','Locality',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('LocationTypeDrop','Attraction','POI',0,5)
GO

-- Create Changes dropdown list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ChangesFindDrop','AllJourneys','Default',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ChangesFindDrop','LimitedChanges','Max2Changes',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ChangesFindDrop','NoChanges','NoChanges',0,3)
GO

-- Create ChangesSpeedDrop list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ChangesSpeedDrop','Average','0',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ChangesSpeedDrop','Slow','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ChangesSpeedDrop','Fast','3',0,3)
GO

-- Create WalkingSpeedDrop,
INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('WalkingSpeedDrop','Average','80',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('WalkingSpeedDrop','Slow','40',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('WalkingSpeedDrop','Fast','120',0,3)
GO

-- Create WalkingMaxTimeDrop,
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('WalkingMaxTimeDrop','5','5',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('WalkingMaxTimeDrop','10','10',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('WalkingMaxTimeDrop','15','15',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('WalkingMaxTimeDrop','20','20',1,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('WalkingMaxTimeDrop','25','25',0,5)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('WalkingMaxTimeDrop','30','30',0,6)
GO

-- Create DrivingFindDrop,
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DrivingFindDrop','Quickest','Fastest',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DrivingFindDrop','Shortest','Shortest',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DrivingFindDrop','Economic','MostEconomical',0,3)
GO

-- Create DrivingMaxSpeedDrop,
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DrivingMaxSpeedDrop','None','111',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DrivingMaxSpeedDrop','Max','112',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DrivingMaxSpeedDrop','60','96',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DrivingMaxSpeedDrop','50','80',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DrivingMaxSpeedDrop','40','64',0,5)
GO

-- Create CarVia dropdown list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('CarViaDrop','Address','AddressPostCode',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('CarViaDrop','Stations','MainStationAirport',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('CarViaDrop','AllStops','AllStationStops',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('CarViaDrop','City','Locality',1,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('CarViaDrop','Attraction','POI',0,5)
GO


-- Create PTVia dropdown list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('PTViaDrop','Stations','MainStationAirport',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('PTViaDrop','AllStops','AllStationStops',0,2)


-- Create AltFromTo dropdown list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AltFromToDrop','Address','AddressPostCode',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AltFromToDrop','Stations','MainStationAirport',1,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AltFromToDrop','AllStops','AllStationStops',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AltFromToDrop','City','Locality',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AltFromToDrop','Attraction','POI',0,5)
GO

-- Create MapTransport Category list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapTransport','Bus','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapTransport','Rail','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapTransport','AirFerry','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapTransport','Taxi','4',0,4)
GO

-- Create MapAccomodation Category list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapAccomodation','BB','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapAccomodation','Hotel','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapAccomodation','Camping','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapAccomodation','Hostels','4',0,4)
GO

-- Create MapSport Category list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapSport','Outdoor','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapSport','Complexes','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapSport','Venues','3',0,3)
GO

-- Create MapAttractions Category list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapAttractions','ZooGardens','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapAttractions','Historical','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapAttractions','Recreational','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapAttractions','TouristAttraction','4',0,4)
GO

-- Create MapHealth Category list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapHealth','Surgeries','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapHealth','Clinics','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapHealth','Hospitals','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapHealth','Nursing','4',0,4)
GO

-- Create MapEducation Category list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapEducation','Primary','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapEducation','Secondary','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapEducation','Colleges','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapEducation','Recreational','4',0,4)
GO

-- Create MapPublicBuildings Category list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapPublicBuildings','PoliceCourts','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapPublicBuildings','LocalServices','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapPublicBuildings','OtherGov','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapPublicBuildings','Facilities','4',0,4)
GO

-- Create MapsForThisJourneyDrop list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapsForThisJourneyDrop','FullJourney','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapsForThisJourneyDrop','Begining','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('MapsForThisJourneyDrop','End','3',0,3)
GO

-- Create MapsForThisJourneyDrop list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('SingleReturnDrop','Single','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('SingleReturnDrop','Return','2',0,2)
GO

-- Create MapsForThisJourneyDrop list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountClassDrop','Std','Second',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountClassDrop','All','All',0,2)
GO

-- Create DiscountRailCardDrop list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountRailCardDrop','None','',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountRailCardDrop', 'Disabled Adult Railcard','DIS',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountRailCardDrop', 'Disabled Child Railcard','DIC',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountRailCardDrop', 'Family Railcard','FAM',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountRailCardDrop', 'HM Forces Railcard','HMF',0,5)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountRailCardDrop', 'Network Railcard','NEW',0,6)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountRailCardDrop', 'Senior Railcard','SNR',0,7)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountRailCardDrop', 'Young Persons Railcard','YNG',0,8)
GO

-- Create DiscountCoachCardDrop list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountCoachCardDrop','None','',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountCoachCardDrop','National Express Student Coachcard','National Express Student Coachcard',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountCoachCardDrop','National Express Advantage50 Coachcard','National Express Advantage50 Coachcard',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountCoachCardDrop','National Express Young Person Coachcard','National Express Young Person Coachcard',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountCoachCardDrop','Family Saver Coachcard','Family Saver Coachcard',0,5)
  INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountCoachCardDrop','Citylink 50+ Discount Card','Citylink 50+ Discount Card',0,6)
  INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountCoachCardDrop','Citylink Student Card','Citylink Student Card',0,7)
  INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountCoachCardDrop','Young Scot Cardholder','Young Scot Cardholder',0,8)
  INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('DiscountCoachCardDrop','Citylink Young Persons Discount Card','Citylink Young Persons Discount Card',0,9)
GO

-- Create LeaveArriveDrop list items
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('LeaveArriveDrop','Leave','false',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('LeaveArriveDrop','Arrive','true',0,2)
GO

-- Create NewsTransportDrop
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsTransportDrop','Public Transport','0',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsTransportDrop','Road','1',0,2)
GO

-- Create NewsRegionDrop
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','All','0',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','South West','1',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','South East','2',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','London','3',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','East','4',0,5)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','East Midlands','5',0,6)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','West Midlands','6',0,7)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','Yorkshire and Humber','7',0,8)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','North West','8',0,9)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','North East','9',0,10)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','Scotland','10',0,11)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsRegionDrop','Wales','11',0,12)
GO

-- Create NewsShowTypeDrop
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsShowTypeDrop','Very Severe','0',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsShowTypeDrop','All','1',0,2)
GO

-- Create NewsShowDetailDrop
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsShowDetailDrop','Summary','0',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('NewsShowDetailDrop','Full','1',0,2)
GO

-- Create FeedbackTypeDrop
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('FeedbackTypeDrop','General','1',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('FeedbackTypeDrop','Journey','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('FeedbackTypeDrop','Question','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('FeedbackTypeDrop','Suggestion','4',0,4)
GO

-- Create ProblemTypeDrop
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemTypeDrop','Searching for a journey','1',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemTypeDrop','Looking at details for a journey','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemTypeDrop','Using a map','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemTypeDrop','Using the fare page','4',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemTypeDrop','Buying a ticket','1',1,5)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemTypeDrop','Using Live Travel','2',0,6)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemTypeDrop','Regarding accessibility','3',0,7)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemTypeDrop','Printing','4',0,8)
GO

-- Create PublicTransportsCheck
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('PublicTransportsCheck','Train','Rail',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('PublicTransportsCheck','Bus','Bus',1,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('PublicTransportsCheck','Underground','Underground',1,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('PublicTransportsCheck','Tram','Tram',1,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('PublicTransportsCheck','Ferry','Ferry',1,5)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('PublicTransportsCheck','Plane','Air',1,6)
GO

-- Create AltOptionsRadio
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AltOptionsRadio','From','true',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AltOptionsRadio','To','false',0,2)
GO

-- Create ReturnMonthYearDrop
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ReturnMonthYearDrop','NoReturn','NoReturn',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ReturnMonthYearDrop','OpenReturn','OpenReturn',0,2)
GO

-- Create ProblemOccuredDropDownList
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemOccuredDropDownList','Start of journey','1',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemOccuredDropDownList','End of journey','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemOccuredDropDownList','Other','3',0,3)
GO

-- Create ComplaintRegardingDrop
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ComplaintRegardingDrop','Past','1',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ComplaintRegardingDrop','current','2',0,2)
GO

-- Create AdjustedRouteDrop
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AdjustedRouteDrop','Adjusted','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AdjustedRouteDrop','Unadjusted','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AdjustedRouteDrop','Both','3',0,3)
GO

-- Create AdjustedUnadjustedRouteDrop (excludes both)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AdjustedUnadjustedDrop','Adjusted','1',0,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('AdjustedUnadjustedDrop','Unadjusted','2',0,2)
GO

-- Create ProblemDropDownList
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemDropDownList','Searching for a journey','1',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemDropDownList','Looking at details for a journey','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemDropDownList','Using a map','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemDropDownList','Using the fares page','4',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemDropDownList','Buying a ticket','5',0,5)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemDropDownList','Finding relevant Live travel info','6',0,6)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemDropDownList','Regarding accessibility','7',0,7)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('ProblemDropDownList','Using the print outs of my journey','8',0,8)

-- Create InformationDropDownList
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('InformationDropDownList','Rail','1',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('InformationDropDownList','Underground/Metro/Light rail/Tram','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('InformationDropDownList','Bus/coach','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('InformationDropDownList','Plane','4',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('InformationDropDownList','Ferry','5',0,5)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('InformationDropDownList','Road(eg. car, van, motorbike)','6',0,6)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('InformationDropDownList','Other','7',0,7)

-- Create SuggestionDropDownList
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('SuggestionDropDownList','Getting around the site (site navigation)','1',1,1)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('SuggestionDropDownList','Information on the site','2',0,2)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('SuggestionDropDownList','Services offered on the site','3',0,3)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('SuggestionDropDownList','Design/Look and feel','4',0,4)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('SuggestionDropDownList','Accessibility','5',0,5)
 INSERT INTO DropDownLists (DataSet,ResourceID,ItemValue,IsSelected,SortOrder)
 VALUES ('SuggestionDropDownList','Other','6',0,6)

-- Create bank holiday data
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '25/12/2003',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '26/12/2003',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '01/01/2004',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '02/01/2004',103),2)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '09/04/2004',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '12/04/2004',103),1)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '03/05/2004',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '31/05/2004',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '02/08/2004',103),2)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '30/08/2004',103),1)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '25/12/2004',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '26/12/2004',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '27/12/2004',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '28/12/2004',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '01/01/2005',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '02/01/2005',103),2)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '03/01/2005',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '04/01/2005',103),2)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '25/03/2005',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '28/03/2005',103),1)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '02/05/2005',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '30/05/2005',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '01/08/2005',103),2)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '29/08/2005',103),1)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '25/12/2005',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '26/12/2005',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '27/12/2005',103),3)
 INSERT INTO BankHolidays (holiday, country)
 VALUES (convert(datetime, '01/01/2006',103),3)
GO


-- Create NatEx operator code list
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','AB')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','AL')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','CL')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','FK')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','FL')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','GD')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','HH')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','JL')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','NB')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','NG')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','NX')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','RA')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','SH')
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('NatExCodes','SP')
 GO

-- Create SCL operator code list
 INSERT INTO Lists (DataSet, ListItem)
 VALUES ('SCLCodes','SCL')
GO

-- Create displayable rail ticket list
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SOS','Standard Open Single','FullyFlexible')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SOR','Standard Open Return','FullyFlexible')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','FOS','First Class Open Single','FullyFlexible')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','FOR','First Class Open Return','FullyFlexible')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SDS','Standard Day Single','FullyFlexible')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SDR','Standard Day Return','FullyFlexible')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','FDS','First Class Day Single','FullyFlexible')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','FDR','First Class Day Return','FullyFlexible')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','CDS','Cheap Day Single','LimitedFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','CDR','Cheap Day Return','LimitedFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SVS','Saver Single','LimitedFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SVR','Saver Return','LimitedFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SSS','Supersaver Single','LimitedFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SSR','Supersaver Return','LimitedFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SAS','SuperAdvance Single','NoFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','SAR','SuperAdvance Return','NoFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','XXX','First SuperAdvance Single','NoFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','XXY','First SuperAdvance Return','NoFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','AXS','APEX Single','NoFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','AXR','APEX Return','NoFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','FXS','APEX First Single','NoFlexibility')
 INSERT INTO CategorisedHashes (DataSet, KeyName, Value, Category)
 VALUES ('DisplayableRailTickets','FXR','APEX First Return','NoFlexibility')
GO

-- ** Create Retailer TEST DATA **


 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('NEX','National Express','http://www.nationalexpress.com',null,'08705 80 80 80','www.nationalexpress.com','/Web/images/gifs/SoftContent/NationalExpress.gif')
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('QJUMP','QJump','http://www.qjump.co.uk',null,'0870 0007 245','www.qjump.co.uk','/Web/images/gifs/SoftContent/QJump.gif')
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('TRAINLINE','The Trainline.com','http://www.thetrainline.com',null,'N/A','www.thetrainline.com',null)
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('SCL','The Trainline.com','http://www.citylink.co.uk',null,'08705 50 50 50','www.citylink.co.uk',null)
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('ANGLIA','Anglia Railways Telesales',null,null,'08700 40 90 90',null,null)
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('FGW','First Great Western',null,null,'08457 000 125',null,null)
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('GNER','GNER',null,null,'08457 225 225',null,null)
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('MML','Midland Mainline',null,null,'08457 125 678',null,null)
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('MML-B','Midland Mainline Business Travel',null,null,'08457 366 125',null,null)
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('SCOTRAIL','ScotRail',null,null,'08457 55 00 33',null,null)
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('VIRGIN','Virgin Trains Call Centre',null,null,'08457 222 333',null,null)
 INSERT INTO Retailers (RetailerId,Name,WebsiteURL,HandoffURL,PhoneNumber,DisplayURL,IconURL)
 VALUES('VIRGIN-B','Virgin Trains Business Express',null,null,'0845 600 61 62',null,null)

GO
-- ** End Retailer TEST DATA **

-- Create RetailerLookup list
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('AB','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('AL','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('CL','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('FK','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('FL','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('GD','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('HH','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('JL','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NB','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NG','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NX','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('RA','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('SH','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('SP','Coach','NEX')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('SCL','Coach','SCL')

 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','QJUMP')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','TRAINLINE')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','ANGLIA')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','FGW')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','GNER')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','MML')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','MML-B')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','SCOTRAIL')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','VIRGIN')
 INSERT INTO RetailerLookup (OperatorCode,Mode,RetailerId)
 VALUES ('NONE','Rail','VIRGIN-B')

GO
--------------------------------------------------------------------
-- END DATASERVICES
--------------------------------------------------------------------


--------------------------------------------------------------------
-- START FTP SERVER CONFIG
--------------------------------------------------------------------
-- None at present
--------------------------------------------------------------------
-- END FTP SERVER CONFIG
--------------------------------------------------------------------


--------------------------------------------------------------------
-- START PROPERTY SERVICES
--------------------------------------------------------------------
INSERT INTO properties
VALUES ('propertyservice.version', '1', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('propertyservice.refreshrate', '60000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('propertyservice.lockedproperties', 'crypt@zocppj2cDhkl00/8nBRONLRt1f9MJTczO2sys305q7qk7EXNV5j5PGcX0edoh1e+/ayMPkS7Fsr5GDH/VrsqrOIdggxka9tGDhPR1qlqKiU=', '<DEFAULT>', '<DEFAULT>');

-- Event Logging Service Properties : Core Publishers ------
INSERT INTO properties
 VALUES ('Logging.Publisher.File', 'FILE1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.Email', '', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.EventLog', 'EventLog1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.Console', '', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.Queue', 'Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.File.FILE1.Directory', 'D:\TDPortal', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.File.FILE1.Rotation', '1000', 'Web', 'UserPortal');
INSERT INTO Properties
 VALUES('Logging.Publisher.Queue.Queue1.Path', '.\Private$\TDPrimaryQueue', 'Web', 'UserPortal');
INSERT INTO Properties
 VALUES('Logging.Publisher.Queue.Queue1.Delivery', 'Express', 'Web', 'UserPortal');
INSERT INTO Properties
 VALUES('Logging.Publisher.Queue.Queue1.Priority', 'Normal', 'Web', 'UserPortal');
INSERT INTO Properties
 VALUES('Logging.Publisher.EventLog.EventLog1.Name', 'Application', 'Web', 'UserPortal');
INSERT INTO Properties
 VALUES('Logging.Publisher.EventLog.EventLog1.Source', 'TDPortalWeb', 'Web', 'UserPortal');
INSERT INTO Properties
 VALUES('Logging.Publisher.EventLog.EventLog1.Machine', '.', 'Web', 'UserPortal');


-- Event Logging Service Properties : Operational Event Publisher Assignment --
INSERT INTO properties
 VALUES ('Logging.Event.Operational.Info.Publishers', 'FILE1 Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Operational.Verbose.Publishers', 'FILE1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Operational.Warning.Publishers', 'FILE1 Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Operational.Error.Publishers', 'FILE1 Queue1 EventLog1', 'Web', 'UserPortal');

-- Event Logging Service Properties : Default Publisher Assignment --
INSERT INTO properties
 VALUES ('Logging.Publisher.Default', 'FILE1', 'Web', 'UserPortal');

-- Event Logging Service Properties : Global Trace Levels --
INSERT INTO properties
 VALUES ('Logging.Event.Custom.Trace', 'On', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Operational.TraceLevel', 'Info', 'Web', 'UserPortal');

-- Event Logging Service Properties : Custom Publishers --
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom', 'EMAIL', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom.EMAIL.Name', 'CustomEmailPublisher', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom.EMAIL.WorkingDir', 'D:\Temp', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom.EMAIL.Sender', 'tdp@slb.com', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Publisher.Custom.EMAIL.SMTPServer', 'mail', 'Web', 'UserPortal');

-- Event Logging Service Properties : Custom Events --
INSERT INTO properties
 VALUES ('Logging.Event.Custom', 'EMAIL GAZ LOGIN MAP PAGE RETAIL PREF JOURNEYREQUEST JOURNEYREQUESTVERBOSE JOURNEYRESULTS JOURNEYRESULTSVERBOSE GATE', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.EMAIL.Name', 'CustomEmailEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.EMAIL.Assembly', 'td.common.logging', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.EMAIL.Publishers', 'EMAIL', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.EMAIL.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.GAZ.Name', 'GazetteerEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GAZ.Assembly', 'td.reportdataprovider.tdpcustomevents', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GAZ.Publishers', 'Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GAZ.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOGIN.Name', 'LoginEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOGIN.Assembly', 'td.reportdataprovider.tdpcustomevents', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOGIN.Publishers', 'Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.LOGIN.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.MAP.Name', 'MapEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.MAP.Assembly', 'td.reportdataprovider.tdpcustomevents', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.MAP.Publishers', 'Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.MAP.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.PAGE.Name', 'PageEntryEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PAGE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PAGE.Publishers', 'Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PAGE.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.RETAIL.Name', 'RetailerHandoffEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.RETAIL.Assembly', 'td.reportdataprovider.tdpcustomevents', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.RETAIL.Publishers', 'Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.RETAIL.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.PREF.Name', 'UserPreferenceSaveEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PREF.Assembly', 'td.reportdataprovider.tdpcustomevents', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PREF.Publishers', 'Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.PREF.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Name', 'JourneyPlanRequestEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Assembly', 'td.userportal.journeycontrol', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Publishers', 'Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUEST.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Name', 'JourneyPlanRequestVerboseEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Assembly', 'td.userportal.journeycontrol', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Publishers', 'FILE1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYREQUESTVERBOSE.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Name', 'JourneyPlanResultsEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Assembly', 'td.userportal.journeycontrol', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Publishers', 'Queue1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTS.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Name', 'JourneyPlanResultsVerboseEvent', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Assembly', 'td.userportal.journeycontrol', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Publishers', 'FILE1', 'Web', 'UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.JOURNEYRESULTSVERBOSE.Trace', 'On', 'Web', 'UserPortal');

INSERT INTO properties
 VALUES ('Logging.Event.Custom.GATE.Name', 'DataGatewayEvent', 'Web','UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GATE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'Web','UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GATE.Publishers', 'Queue1', 'Web','UserPortal');
INSERT INTO properties
 VALUES ('Logging.Event.Custom.GATE.Trace', 'On', 'Web','UserPortal');



-- End Event Logging Service Properties ------
GO

-- ScreenFlow Properties --
INSERT INTO properties
VALUES ('ScreenFlow.PageTransferDetails.Path', '/Web/PageTransferDetails.xml', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransitionEvent.Path', '/Web/PageTransitionEvent.xml', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransferDetails.Xml.Node.Root', 'PageTransferDetails', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransferDetails.Xml.Node.Page', 'page', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransferDetails.Xml.Attribute.PageId', 'PageId', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransferDetails.Xml.Attribute.PageURL', 'PageURL', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransferDetails.Xml.Attribute.BookmarkRedirect', 'BookmarkRedirect', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransferDetails.Xml.Attribute.SpecificStateClass', 'SpecificStateClass', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransitionEvent.Xml.Node.Root', 'PageTransitionEvents', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransitionEvent.Xml.Node.Page', 'page', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransitionEvent.Xml.Attribute.TransitionEvent', 'TransitionEvent', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('ScreenFlow.PageTransitionEvent.Xml.Attribute.PageId', 'PageId', '<DEFAULT>', '<DEFAULT>');
-- End ScreenFlow Properties --
GO

-- Properties used by Web and web controls
INSERT INTO properties
VALUES ('Web.Controls.MileageConverter', '1609', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.CarJourneyDetailsControl.ImmediateTurnDistance', '100', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.CarJourneyDetailsControl.UncountedTurnValue', '4', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelOne', '2000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelTwo', '4000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelThree', '8000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelFour', '20000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelFive', '40000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelSix', '80000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelSeven', '180000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelEight', '360000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelNine', '720000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelTen', '1200000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelEleven', '2400000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelTwelve', '4800000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.MappingComponent.ZoomLevelThirteen', '9000000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelOne', '100000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelTwo', '200000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelThree', '300000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelFour', '400000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelFive', '500000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelSix', '600000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelSeven', '700000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelEight', '800000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelNine', '900000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelTen', '1000000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelEleven', '4000000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelTwelve', '7500000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('Web.SimpleMappingComponent.ZoomLevelThirteen', '12090656', '<DEFAULT>', '<DEFAULT>');

-- JourneyControl properties
INSERT INTO properties
VALUES ('JourneyControl.NumberOfPublicJourneys','5', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.CJPTimeoutMillisecs','235000', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.WaitPageTimeoutSeconds','240', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.WaitPageRefreshSeconds','6', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.LogJourneyWebFailures','Y', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.LogTTBOFailures','Y', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.LogCJPFailures','Y', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.LogRoadEngineFailures','Y', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.LogDisplayableMessages','Y', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.LogAllResponses','Y', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.LogAllRequests','Y', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('JourneyControl.NaptanCacheTimeoutSeconds','1200', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES('EsriDB','Server=' + @@SERVERNAME + ';Initial Catalog=MASTERMAP;Trusted_Connection=true;', '<DEFAULT>', '<DEFAULT>');


-- LocationService properties
DECLARE @GISServerName nvarchar(100)
SELECT @GISServerName=GISServerName from master.dbo.Environment

INSERT INTO properties
VALUES ('locationservice.gazopsweburl','http://' + @GISServerName + '/GazopsWeb/GazopsWeb.asmx', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.maxreturnedrecords','60', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.addressgazetteerid','ADDP1', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.postcodegazetteerid','PCODE1', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.localitygazetteerid','DRILL1', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.majorstationsgazetteerid','NPTG1', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.allstationsgazetteerid','NAPTAN1', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.attractionsgazetteerid','PX1', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.servername', @GISServerName, 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.servicename','tdp_del5', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.addresspostcode.minscore','20', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.locality.minscore','20', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.majorstations.minscore','20', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.allstations.minscore','20', 'LocationService', 'UserPortal');
INSERT INTO properties
VALUES ('locationservice.attractions.minscore','20', 'LocationService', 'UserPortal');
-- End LocationService properties

-- JourneyPlanRunner properties
INSERT INTO properties
VALUES ('journeyplanrunner.cjpwaittimeouthours','0', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('journeyplanrunner.cjpwaittimeoutminutes','1', '<DEFAULT>', '<DEFAULT>');
INSERT INTO properties
VALUES ('journeyplanrunner.cjpwaittimeoutseconds','0', '<DEFAULT>', '<DEFAULT>');
-- End JourneyPlanRunner properties


-- DataServices Properties

DECLARE @MISServerName nvarchar(100)
SELECT @MISServerName=MISServerName from master.dbo.Environment

INSERT INTO Properties
 VALUES ('ReportDataDB','Server=' + @MISServerName + ';DRIVER={SQL Server};Initial Catalog=Reporting;Trusted_Connection=true;', '<DEFAULT>', '<DEFAULT>');
INSERT INTO Properties
 VALUES ('DefaultDB','Server=' + @@SERVERNAME + ';Initial Catalog=PermanentPortal;Trusted_Connection=true;', '<DEFAULT>', '<DEFAULT>');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.List.type','1','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.List.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.List.query','SELECT CategoryName FROM Categories ORDER BY CategoryName','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.Hash.type','2','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.Hash.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.Hash.query','SELECT CategoryName, Description FROM Categories ORDER BY CategoryName','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.FromToDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.FromToDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.FromToDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''FromToDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.LocationTypeDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.LocationTypeDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.LocationTypeDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''LocationTypeDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ChangesFindDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ChangesFindDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ChangesFindDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''ChangesFindDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ChangesSpeedDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ChangesSpeedDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ChangesSpeedDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''ChangesSpeedDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.WalkingSpeedDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.WalkingSpeedDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.WalkingSpeedDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''WalkingSpeedDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.WalkingMaxTimeDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.WalkingMaxTimeDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.WalkingMaxTimeDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''WalkingMaxTimeDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DrivingFindDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DrivingFindDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DrivingFindDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''DrivingFindDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DrivingMaxSpeedDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DrivingMaxSpeedDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DrivingMaxSpeedDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''DrivingMaxSpeedDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.CarViaDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.CarViaDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.CarViaDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''CarViaDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.PTViaDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.PTViaDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.PTViaDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''PTViaDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AltFromToDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AltFromToDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AltFromToDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''AltFromToDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapTransport.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapTransport.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapTransport.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''MapTransport'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapAccomodation.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapAccomodation.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapAccomodation.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''MapAccomodation'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapSport.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapSport.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapSport.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''MapSport'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapAttractions.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapAttractions.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapAttractions.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''MapAttractions'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapHealth.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapHealth.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapHealth.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''MapHealth'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapEducation.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapEducation.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapEducation.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''MapEducation'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapPublicBuildings.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapPublicBuildings.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapPublicBuildings.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''MapPublicBuildings'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapsForThisJourneyDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapsForThisJourneyDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.MapsForThisJourneyDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''MapsForThisJourneyDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.SingleReturnDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.SingleReturnDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.SingleReturnDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''SingleReturnDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountClassDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountClassDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountClassDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''DiscountClassDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountRailCardDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountRailCardDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountRailCardDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''DiscountRailCardDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountCoachCardDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountCoachCardDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountCoachCardDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''DiscountCoachCardDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.LeaveArriveDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.LeaveArriveDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.LeaveArriveDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''LeaveArriveDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsTransportDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsTransportDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsTransportDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''NewsTransportDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsRegionDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsRegionDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsRegionDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''NewsRegionDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsShowTypeDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsShowTypeDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsShowTypeDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''NewsShowtypeDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsShowDetailDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsShowDetailDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NewsShowDetailDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''NewsShowDetailDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.FeedbackTypeDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.FeedbackTypeDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.FeedbackTypeDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''FeedbackTypeDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ComplaintRegardingDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ComplaintRegardingDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ComplaintRegardingDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''ComplaintRegardingDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AdjustedRouteDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AdjustedRouteDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AdjustedRouteDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''AdjustedRouteDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.PublicTransportsCheck.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.PublicTransportsCheck.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.PublicTransportsCheck.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''PublicTransportsCheck'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AltOptionsRadio.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AltOptionsRadio.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AltOptionsRadio.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''AltOptionsRadio'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ProblemTypeDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ProblemTypeDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ProblemTypeDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''ProblemTypeDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ReturnMonthYearDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ReturnMonthYearDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ReturnMonthYearDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''ReturnMonthYearDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.BankHolidays.type','4','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.BankHolidays.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.BankHolidays.query','SELECT holiday, country FROM BankHolidays ORDER BY holiday','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AdjustedUnadjustedDrop.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AdjustedUnadjustedDrop.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.AdjustedUnadjustedDrop.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''AdjustedUnadjustedDrop'' ORDER BY SortOrder','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NatExCodes.type','1','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NatExCodes.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.NatExCodes.query','SELECT listitem FROM Lists WHERE dataset = ''NatExCodes'' ','DataServices','UserPortal')
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.SCLCodes.type','1','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.SCLCodes.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.SCLCodes.query','SELECT listitem FROM Lists WHERE dataset = ''SCLCodes'' ','DataServices','UserPortal')

INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ProblemOccuredDropDownList.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ProblemOccuredDropDownList.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ProblemOccuredDropDownList.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''ProblemOccuredDropDownList'' ORDER BY SortOrder','DataServices','UserPortal');

INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ProblemDropDownList.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ProblemDropDownList.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.ProblemDropDownList.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''ProblemDropDownList'' ORDER BY SortOrder','DataServices','UserPortal');

INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.InformationDropDownList.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.InformationDropDownList.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.InformationDropDownList.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''InformationDropDownList'' ORDER BY SortOrder','DataServices','UserPortal');

INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.SuggestionDropDownList.type','3','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.SuggestionDropDownList.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.SuggestionDropDownList.query','SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''SuggestionDropDownList'' ORDER BY SortOrder','DataServices','UserPortal');

INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountCoachCards.type','1','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountCoachCards.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DiscountCoachCards.query','SELECT ItemValue FROM DropDownLists WHERE DataSet = ''DiscountCoachCardDrop'' ORDER BY SortOrder','DataServices','UserPortal');

INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DisplayableRailTickets.type','5','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DisplayableRailTickets.db','DefaultDB','DataServices','UserPortal');
INSERT INTO Properties
 VALUES('TransportDirect.UserPortal.DataServices.DisplayableRailTickets.query','SELECT KeyName, Value, Category FROM CategorisedHashes WHERE DataSet = ''DisplayableRailTickets''','DataServices','UserPortal');

-- END DataServices Properties
GO

-- Calendar control properties
INSERT INTO Properties VALUES('controls.numberofmonths','6','Web','UserPortal');
-- End Calendar control properties
GO

-- ASPState properties
INSERT INTO properties
VALUES ('ASPStateDB','Server=' + @@SERVERNAME + ';Initial Catalog=ASPState;Trusted_Connection=true;', '<DEFAULT>', '<DEFAULT>');
-- End ASPState properties
GO

-- Journey Parameters Properties
INSERT INTO Properties VALUES('journeyparameters.eveningtime','21:00','Web','UserPortal');
INSERT INTO Properties VALUES('journeyparameters.morningtime','7:00','Web','UserPortal');
INSERT INTO Properties VALUES('journeyparameters.minutestoadd','15','Web','UserPortal');
-- End Journey Parameter Properties
GO

-- Location Map Properties
INSERT INTO Properties VALUES('LocationMap.SelectSmallestScale','40000','Web','UserPortal');
INSERT INTO Properties VALUES('LocationMap.PointXSmallestScale','40000','Web','UserPortal');
INSERT INTO Properties VALUES('LocationMap.NaPTANSmallestScale','40000','Web','UserPortal');
-- End Location Map Properties
GO

-- Gazetteer Default Scale Properties
INSERT INTO Properties VALUES('GazetteerDefaultScale.AddressPostCode','4000','Web','UserPortal');
INSERT INTO Properties VALUES('GazetteerDefaultScale.Locality','80000','Web','UserPortal');
INSERT INTO Properties VALUES('GazetteerDefaultScale.MajorStations','8000','Web','UserPortal');
INSERT INTO Properties VALUES('GazetteerDefaultScale.AllStations','8000','Web','UserPortal');
INSERT INTO Properties VALUES('GazetteerDefaultScale.AttractionsFacilities','8000','Web','UserPortal')
-- End Gazetteer Default Scale Properties
GO

-- ESRI Map Component Properties
DECLARE @GISServerName nvarchar(100)
SELECT @GISServerName=GISServerName from master.dbo.Environment

INSERT INTO Properties VALUES('InteractiveMapping.Map.ServiceName','tdp_del5', 'Web', 'UserPortal');
INSERT INTO Properties VALUES('InteractiveMapping.Map.ServerName', @GISServerName, 'Web', 'UserPortal');
INSERT INTO Properties VALUES('InteractiveMapping.Map.RoadScaleCutOff','80000', 'Web', 'UserPortal');
INSERT INTO Properties VALUES('InteractiveMapping.SimpleMap.ServiceName','tdp_congestion', 'Web', 'UserPortal');
INSERT INTO Properties VALUES('InteractiveMapping.SimpleMap.ServerName', @GISServerName, 'Web', 'UserPortal');

-- End ESRI Map Component
GO

-- TransientPortalDB enumerator property
INSERT INTO Properties
VALUES ('TransientPortalDB','Server=' + @@SERVERNAME + ';Initial Catalog=TransientPortal;Trusted_Connection=true;', '<DEFAULT>', '<DEFAULT>');
-- End TransientPortalDB enumerator property
GO

-- JourneyPlanner Properties
INSERT INTO Properties VALUES('journeyplanner.minimumlegsforpagebreak', '2', 'Web', 'UserPortal');
INSERT INTO Properties VALUES('journeyplanner.monthyearseparator', '/', 'Web', 'UserPortal');
-- End journeyPlanner
GO

-- AdditionalDataModule Properties
INSERT INTO Properties
 VALUES ('AdditionalDataDB','Server=' + @@SERVERNAME + ';Initial Catalog=AdditionalData;Trusted_Connection=true;', 'AdditionalDataModule', 'UserPortal');
GO
-- End AdditionalDataModule

-- Retail Business Objects
-- Core Publishers
INSERT INTO properties VALUES ('Logging.Publisher.Queue', '', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.Email','',  'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.EventLog','',  'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.Console','',  'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.Custom','',  'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.Default', 'File1', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.File', 'File1', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.File.File1.Directory', 'D:\TDPortal', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.File.File1.Rotation', '1024', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.Console.Console1.Stream', 'Out', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.EventLog.EventLog1.Name', 'ELName1', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.EventLog.EventLog1.Source', 'Pricing-RBO', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Publisher.EventLog.EventLog1.Machine', 'localhost', 'Pricing', 'RBO')
-- END Core Publishers
-- Operational Event Publisher assignment
INSERT INTO properties VALUES ('Logging.Event.Operational.TraceLevel', 'Verbose', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Event.Operational.Info.Publishers', 'File1', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Event.Operational.Verbose.Publishers', 'File1', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Event.Operational.Warning.Publishers', 'File1', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Event.Operational.Error.Publishers', 'File1', 'Pricing', 'RBO')
-- END Operational Event Publisher assignment
-- Custom Events
INSERT INTO properties VALUES ('Logging.Event.Custom.Trace', 'On', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Event.Custom', 'DataGateway', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Event.Custom.DataGateway.Name', 'DataGatewayEvent', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Event.Custom.DataGateway.Assembly', 'td.ReportDataProvider.TDPCustomEvents', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Event.Custom.DataGateway.Publishers', 'File1', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('Logging.Event.Custom.DataGateway.Trace', 'On', 'Pricing', 'RBO')
-- END Custom Events
-- FBO PROPERTIES
INSERT INTO properties VALUES ('RetailBusinessObjects.FBO.InterfaceVersion', '0101', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.FBO.PoolSize', '5', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.FBO.ObjectId', 'FA', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.FBO.TimeoutDuration', '3000', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.FBO.TimeoutCheckFrequency', '1000', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.FBO.TimeoutChecking', 'On', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.FBO.MinimumPoolSize', '5', 'Pricing', 'RBO')
-- END FBO PROPERTIES
-- RBO PROPERTIES
INSERT INTO properties VALUES ('RetailBusinessObjects.RBO.InterfaceVersion', '0101', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.RBO.PoolSize', '1', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.RBO.ObjectId', 'RE', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.RBO.TimeoutDuration', '5000', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.RBO.TimeoutCheckFrequency', '1000', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.RBO.TimeoutChecking', 'On', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.RBO.MinimumPoolSize', '1', 'Pricing', 'RBO')
-- END RBO PROPERTIES
-- LBO PROPERTIES
INSERT INTO properties VALUES ('RetailBusinessObjects.LBO.InterfaceVersion', '0100', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.LBO.PoolSize', '5', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.LBO.ObjectId', 'LU', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.LBO.TimeoutDuration', '3000', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.LBO.TimeoutCheckFrequency', '1000', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.LBO.TimeoutChecking', 'On', 'Pricing', 'RBO')
INSERT INTO properties VALUES ('RetailBusinessObjects.LBO.MinimumPoolSize', '5', 'Pricing', 'RBO')
-- END LBO PROPERTIES
-- Pool Properties
INSERT INTO properties VALUES ('RetailBusinessObjects.HousekeepingCheckFrequency', '3000', 'Pricing', 'RBO')
-- END Pool Properties
GO
-- END Retail Business Objects

-- PricingRetail.RetailXmlDocument Properties
INSERT INTO Properties VALUES('PricingRetail.RetailXmlDocument.SchemaFilename', 'D:\inetpub\wwwroot\web\030829 Journey_Interface v2.xsd', '<DEFAULT>', '<DEFAULT>' );
INSERT INTO Properties VALUES('PricingRetail.RetailXmlDocument.Version', '1.0', '<DEFAULT>', '<DEFAULT>' );
-- End PricingRetail.RetailXmlDocument
GO

-- Usersupport and Favourite Journeys Properties
INSERT INTO Properties
 VALUES('favourites.maxnumberoffavouritejourneys', '6', 'Web', 'UserPortal');
INSERT INTO Properties
 VALUES('usersupport.encryptionkey','crypt@ddUw+p6uPXhcJuGvk9fNZWe5TujhpirA7r8tNnoSrm+6Zk6G1dyB5y44+Pxa1t3lgTtl7phTyK86nOb7MrYD375GEp+6Q8AAlTVWlTVgDBIlKJgUPzuEKXrWnYaVSZcC', 'Web', 'UserPortal' );
INSERT INTO Properties
 VALUES('usersupport.encryptioniv','DcqVHaQY/LFYo61QKxGefQ==', 'Web', 'UserPortal' );
-- End FavouriteJourneys
GO

-- LocationInformation Properties
INSERT INTO Properties
 VALUES('locationinformation.departureboardurl', 'http://www.livedepartureboards.co.uk/ldb/summary.aspx?T={0}', 'Web', 'UserPortal');
INSERT INTO Properties
 VALUES('locationinformation.furtherdetailsurl', 'http://www.serco-online.com/html/station_info/crs_atoc.asp?crs_code={0}', 'Web', 'UserPortal');

-- End LocationInformation
GO

----- REPORT DATA PROVIDER COMPONENT PROPERTIES -------


-- TransactionWebService Properties

INSERT INTO Properties VALUES('TransactionWebService.CJPConfigFilepath', 'D:\Inetpub\wwwroot\TDPWebServices\TransactionWebService\cjp.client.config', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.Queue', 'Queue1', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.Email', '', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog', 'EventLog1', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.Console', '', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.File', 'File1', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.Custom', '', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Path', '.\Private$\TDPrimaryQueue', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Delivery', 'Express', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Priority', 'Normal', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Name', 'Application', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Source', 'TransactionWebService', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Machine', '.', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Directory', 'D:\TDPortal', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Rotation', '1000', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Publisher.Default', 'File1', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Event.Operational.Verbose.Publishers', 'File1', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Event.Operational.Info.Publishers', 'Queue1 File1', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Event.Operational.Warning.Publishers', 'Queue1 File1', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Event.Operational.Error.Publishers', 'Queue1 File1 EventLog1', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Event.Operational.TraceLevel', 'Verbose', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Event.Custom', '', 'TransactionWebService', 'UserPortal');
INSERT INTO Properties VALUES('Logging.Event.Custom.Trace', 'Off', 'TransactionWebService', 'UserPortal');

-- End TransactionWebService
GO


-- Web Log Reader Properties
INSERT INTO Properties VALUES('Logging.Publisher.Queue', 'Queue1', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Email', '', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog', '', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Console', '', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File', 'File1', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom', '', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Path', '.\Private$\TDPrimaryQueue', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Delivery', 'Express', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Priority', 'Normal', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Directory', 'D:\TDPortal', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Rotation', '1000', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Default', 'File1', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Verbose.Publishers', 'File1', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Info.Publishers', 'Queue1 File1', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Warning.Publishers', 'Queue1 File1', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Error.Publishers', 'Queue1 File1', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.TraceLevel', 'Verbose', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom.Trace', 'On', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom', 'WebLogData', 'WebLogReader', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Event.Custom.WebLogData.Name', 'WorkloadEvent', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom.WebLogData.Assembly', 'td.ReportDataProvider.TDPCustomEvents', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom.WebLogData.Publishers', 'Queue1 File1', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom.WebLogData.Trace', 'On', 'WebLogReader', 'ReportDataProvider');

INSERT INTO Properties VALUES('WebLogReader.ArchiveDirectory', 'D:\Info\Logs\w3svc1\Archive', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('WebLogReader.LogDirectory', 'D:\Info\Logs\w3svc1', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('WebLogReader.NonPageMinimumBytes', '5000000', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('WebLogReader.WebPageExtensions', 'asp aspx htm html pdf [none]', 'WebLogReader', 'ReportDataProvider');
INSERT INTO Properties VALUES('WebLogReader.ClientIPExcludes', '10.93.108.195 10.93.108.194', 'WebLogReader', 'ReportDataProvider');


-- End Web Log Reader
GO


-- EventReceiver Properties

INSERT INTO Properties VALUES('Receiver.Queue', 'SourceQueue1', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Receiver.Queue.SourceQueue1.Path', '.\Private$\TDPrimaryQueue', 'EventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.Queue', '', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Email', '', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog', 'EventLog1', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Console', '', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File', 'File1', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom', 'TDPDB CJPDB OPDB', 'EventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Name', 'Application', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Source', 'EventReceiver', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Machine', '.', 'EventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Directory', 'D:\TDPortal', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Rotation', '1000', 'EventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.Custom.TDPDB.Name', 'TDPCustomEventPublisher', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom.CJPDB.Name', 'CJPCustomEventPublisher', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom.OPDB.Name', 'OperationalEventPublisher', 'EventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.TDPCustomEventPublisher.JourneyPlanRequestID', '', 'EventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.Default', 'File1', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Verbose.Publishers', 'File1 EventLog1', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Info.Publishers', 'File1', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Warning.Publishers', 'File1', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Error.Publishers', 'File1 EventLog1', 'EventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Event.Custom', 'LOC GAZ LOGIN MAP PAGE RETAIL PREF JOURNEYREQUEST JOURNEYREQUESTVERBOSE JOURNEYRESULTS JOURNEYRESULTSVERBOSE JOURNEYWEBREQUEST GATE REFERENCETRANSACTION WORKLOAD ROE', 'EventReceiver', 'ReportDataProvider');

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
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Name', 'WorkloadEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.WORKLOAD.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Name', 'ReferenceTransactionEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Publishers', 'TDPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.REFERENCETRANSACTION.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO properties VALUES ('Logging.Event.Custom.ROE.Name', 'ReceivedOperationalEvent', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.ROE.Assembly', 'td.reportdataprovider.tdpcustomevents', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.ROE.Publishers', 'OPDB', 'EventReceiver','ReportDataProvider');
INSERT INTO properties VALUES ('Logging.Event.Custom.ROE.Trace', 'On', 'EventReceiver','ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Event.Operational.TraceLevel', 'Verbose', 'EventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom.Trace', 'On', 'EventReceiver', 'ReportDataProvider');


-- End EventReceiver
GO


-- InjectorEventReceiver Properties. Use for EventReceiver on ONE of Web Servers
-- This will receive messages from the MSMQ on the Web Server AND from the Injector MSMQ.

INSERT INTO Properties VALUES('Receiver.Queue', 'SourceQueue1 SourceQueue2', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Receiver.Queue.SourceQueue1.Path', '.\Private$\TDPrimaryQueue', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Receiver.Queue.SourceQueue2.Path', 'INJBOX\Private$\TDPrimaryQueue', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.Queue', '', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Email', '', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog', 'EventLog1', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Console', '', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File', 'File1', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom', 'TDPDB CJPDB OPDB', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Name', 'Application', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Source', 'InjectorEventReceiver', 'InjectorEventReceiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog.EventLog1.Machine', '.', 'InjectorEventReceiver', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Directory', 'D:\TDPortal', 'InjectorEventReceiver', 'ReportDataProvider');
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


-- Report Data Importer Properties

INSERT INTO Properties VALUES('ReportDataImporter.CJPWebRequestWindow', '1', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('ReportDataImporter.TimeoutDuration', '180', 'ReportDataImporter', 'ReportDataProvider');

INSERT INTO Properties VALUES('Logging.Publisher.Queue', 'Queue1', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Email', '', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog', '', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Console', '', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File', 'File1', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom', '', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Path', '.\Private$\TDPrimaryQueue', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Delivery', 'Express', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Priority', 'Normal', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Directory', 'D:\TDPortal', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Rotation', '1000', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Default', 'File1', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Verbose.Publishers', 'File1', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Info.Publishers', 'Queue1 File1', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Warning.Publishers', 'Queue1 File1', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Error.Publishers', 'Queue1 File1', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.TraceLevel', 'Verbose', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom.Trace', 'Off', 'ReportDataImporter', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom', '', 'ReportDataImporter', 'ReportDataProvider');

-- End Report Data Importer
GO


-- Report Staging Data Archiver Properties

INSERT INTO Properties VALUES('Logging.Publisher.Queue', 'Queue1', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Email', '', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.EventLog', '', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Console', '', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File', 'File1', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Custom', '', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Path', '.\Private$\TDPrimaryQueue', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Delivery', 'Express', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Queue.Queue1.Priority', 'Normal', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Directory', 'D:\TDPortal', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.File.File1.Rotation', '1000', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Publisher.Default', 'File1', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Verbose.Publishers', 'File1', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Info.Publishers', 'Queue1 File1', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Warning.Publishers', 'Queue1 File1', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.Error.Publishers', 'Queue1 File1', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Operational.TraceLevel', 'Verbose', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom.Trace', 'Off', 'ReportStagingDataArchiver', 'ReportDataProvider');
INSERT INTO Properties VALUES('Logging.Event.Custom', '', 'ReportStagingDataArchiver', 'ReportDataProvider');

-- End Report Staging Data Archiver
GO

----- END REPORT DATA PROVIDER COMPONENT PROPERTIES -------



-- Properties for FeedbackInitialPage
INSERT INTO Properties VALUES('feedback.emailaddress.general', 'tdp@slb.com', 'Web', 'UserPortal');
INSERT INTO Properties VALUES('feedback.emailaddress.information', 'tdp@slb.com', 'Web', 'UserPortal');
INSERT INTO Properties VALUES('feedback.emailaddress.suggestion', 'tdp@slb.com', 'Web', 'UserPortal');
INSERT INTO Properties VALUES('feedback.emailaddress.claim', 'tdp@slb.com', 'Web', 'UserPortal');
GO
-- End of FeedbackInitialPage

-- Insert Data Gateway Properties

INSERT INTO properties VALUES ('Gateway.ReceptionPath', 'D:/Gateway/dat/Reception/', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.IncomingPath', 'D:/Gateway/dat/Incoming/', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.HoldingPath', 'D:/Gateway/dat/Holding/', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.ProcessingPath', 'D:/Gateway/dat/Processing/', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.BackupPath', 'D:/Gateway/dat/Backup/', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.PathToPuTTY', 'D:/Gateway/bin/PuTTY/', '', 'DataGateway')

INSERT INTO properties VALUES ('Gateway.Export.ServerID', 'BBP', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.Export.BBP.ftp', '127.0.0.1', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.Export.BBP.uid', 'TDP28Nov', '', 'DataGateway')
INSERT INTO properties VALUES ('Gateway.Export.BBP.pwd', 'sI1732#3', '', 'DataGateway')

INSERT INTO properties VALUES ('datagateway.zipprocessor.schemalocation', 'D:/Gateway/bin/TDPZip/bin/TDPZip.xsd', '', 'DataGateway')
INSERT INTO properties VALUES ('datagateway.zipprocessor.xmlnamespace', 'http://www.transportdirect.info/datafeedinfo', '', 'DataGateway');
INSERT INTO properties VALUES ('datagateway.zipprocessor.headerxmlfilename', 'TDheader.xml', '', 'DataGateway');
INSERT INTO properties VALUES ('datagateway.zipprocessor.timepreparednodename', 'TimePrepared', '', 'DataGateway')
INSERT INTO properties VALUES ('datagateway.zipprocessor.supplieridnodename', 'SupplierID', '', 'DataGateway')
INSERT INTO properties VALUES ('datagateway.zipprocessor.filenamenodename', 'Filename', '', 'DataGateway')
INSERT INTO properties VALUES ('datagateway.zipprocessor.ispresentattributename', 'isPresent', '', 'DataGateway')
INSERT INTO properties VALUES ('datagateway.zipprocessor.ispresentindicator', 'Y', '', 'DataGateway')
INSERT INTO properties VALUES ('datagateway.zipprocessor.pkunzipexecutablepath', 'D:/Program Files/PKWARE/PKZIPC/pkzipc', '', 'DataGateway')
INSERT INTO properties VALUES ('datagateway.zipprocessor.pkunziparguments', '-Extract -Overwrite', '', 'DataGateway')

INSERT INTO properties VALUES ('datagateway.travelnews.schemalocation', 'D:/Gateway/bin/xml/TravelNews.xsd', '', 'DataGateway')
INSERT INTO properties VALUES ('datagateway.travelnews.xmlnamespace', 'http://www.transportdirect.info/travelnews', '', 'DataGateway');
INSERT INTO properties VALUES ('datagateway.retailbusinessobjects.fbofeedname', 'kfq970', '', 'DataGateway');
INSERT INTO properties VALUES ('datagateway.retailbusinessobjects.rbofeedname', 'pgc328', '', 'DataGateway');
INSERT INTO properties VALUES ('datagateway.retailbusinessobjects.lbofeedname', 'kiq827', '', 'DataGateway');

INSERT INTO Properties VALUES ('Logging.Publisher.Queue', 'Queue1', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.Email', '', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.EventLog', '', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.Console', '', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.File', 'File1', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.Custom', '', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.Queue.Queue1.Path', '.\Private$\TDPrimaryQueue', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.Queue.Queue1.Delivery', 'Express', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.Queue.Queue1.Priority', 'Normal', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.File.File1.Directory', 'D:\TDPortal', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.File.File1.Rotation', '1000', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Publisher.Default', 'File1', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Event.Operational.Verbose.Publishers', 'File1', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Event.Operational.Info.Publishers', 'Queue1 File1', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Event.Operational.Warning.Publishers', 'Queue1 File1', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Event.Operational.Error.Publishers', 'Queue1 File1', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Event.Operational.TraceLevel', 'Verbose', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Event.Custom', 'GATE', '', 'DataGateway');
INSERT INTO Properties VALUES ('Logging.Event.Custom.Trace', 'On', '', 'DataGateway');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Name', 'DataGatewayEvent', '','DataGateway');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Assembly', 'td.reportdataprovider.tdpcustomevents', '','DataGateway');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Publishers', 'Queue1 File1', '','DataGateway');
INSERT INTO properties VALUES ('Logging.Event.Custom.GATE.Trace', 'On', '','DataGateway');

-- Data Gateway FTP Configuration

INSERT INTO FTP_CONFIGURATION VALUES (1, 'testPull', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/testPull', './testPull', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'testPush', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/testPush', './testPush', '*.zip', 0, 1, '2003-01-01', '', 1);

INSERT INTO FTP_CONFIGURATION VALUES (1, 'afc743', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/afc743', './afc743', '*.zip', 0, 8, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'qxj544', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/qxj544', './qxj544', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'yvs938', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/yvs938', './yvs938', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'kiq827', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/kiq827', './kiq827', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'kfq970', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/kfq970', './kfq970', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'ynx354', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/ynx354', './ynx354', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'map466', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/map466', './map466', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'vfd300', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/vfd300', './vfd300', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'pgc328', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/pgc328', './pgc328', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'nbv921', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/nbv921', './nbv921', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'pyc726', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/pyc726', './pyc726', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'arh321', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/arh321', './arh321', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'far265', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/far265', './far265', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'ijm369', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/ijm369', './ijm369', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'xpy492', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/xpy492', './xpy492', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'tyd263', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/tyd263', './tyd263', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'wnq457', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/wnq457', './wnq457', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'bmc368', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/bmc368', './bmc368', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'jzr690', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/jzr690', './jzr690', '*.zip', 0, 1, '2003-01-01', '', 1);
INSERT INTO FTP_CONFIGURATION VALUES (1, 'gut906', 'LocalHost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/gut906', './gut906', '*.zip', 0, 1, '2003-01-01', '', 1);
GO

-- Gateway server name
DECLARE @GAServerName nvarchar(100)
SELECT @GAServerName=GAServerName from master.dbo.Environment

-- Data Gateway Import Configuration
INSERT INTO IMPORT_CONFIGURATION VALUES ('testPull', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/testPull');
INSERT INTO IMPORT_CONFIGURATION VALUES ('testPush', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/testPush');

INSERT INTO IMPORT_CONFIGURATION VALUES ('afc743', 'TransportDirect.UserPortal.TravelNews.TravelNewsImport', 'D:/Gateway/bin/td.userportal.travelnews.dll', '', '', '', 'D:/Gateway/dat/Processing/afc743');
INSERT INTO IMPORT_CONFIGURATION VALUES ('qxj544', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/qxj544');
INSERT INTO IMPORT_CONFIGURATION VALUES ('yvs938', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/TrainTaxi.bat', '', '', 'D:/Gateway/dat/Processing/yvs938');
INSERT INTO IMPORT_CONFIGURATION VALUES ('kiq827', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/kiq827');
INSERT INTO IMPORT_CONFIGURATION VALUES ('kfq970', 'TransportDirect.UserPortal.RetailBusinessObjects.RBOImportTask', 'D:/Gateway/bin/td.userportal.retailbusiness.dll', '', 'D:\RBOData\dlfa.dat http://' + @GAServerName + ':80/RetailBusinessObjects/RetailBusinessObjectsFacade.rem', '', 'D:/Gateway/dat/Processing/kfq970');
INSERT INTO IMPORT_CONFIGURATION VALUES ('ynx354', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/NPTG.bat', '', '', 'D:/Gateway/dat/Processing/ynx354');
INSERT INTO IMPORT_CONFIGURATION VALUES ('map466', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/map466');
INSERT INTO IMPORT_CONFIGURATION VALUES ('vfd300', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/NaPTANxml.bat', '', '', 'D:/Gateway/dat/Processing/vfd300');
INSERT INTO IMPORT_CONFIGURATION VALUES ('pgc328', 'TransportDirect.UserPortal.RetailBusinessObjects.RBOImportTask', 'D:/Gateway/bin/td.userportal.retailbusiness.dll', '', 'D:\RBOData\dlre.dat http://' + @GAServerName + ':80/RetailBusinessObjects/RetailBusinessObjectsFacade.rem', '', 'D:/Gateway/dat/Processing/pgc328');
INSERT INTO IMPORT_CONFIGURATION VALUES ('nbv921', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/nbv921');
INSERT INTO IMPORT_CONFIGURATION VALUES ('pyc726', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/pyc726');
INSERT INTO IMPORT_CONFIGURATION VALUES ('arh321', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/arh321');
INSERT INTO IMPORT_CONFIGURATION VALUES ('far265', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/far265');
INSERT INTO IMPORT_CONFIGURATION VALUES ('ijm369', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/ijm369');
INSERT INTO IMPORT_CONFIGURATION VALUES ('xpy492', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/xpy492');
INSERT INTO IMPORT_CONFIGURATION VALUES ('tyd263', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/tyd263');
INSERT INTO IMPORT_CONFIGURATION VALUES ('wnq457', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/wnq457');
INSERT INTO IMPORT_CONFIGURATION VALUES ('bmc368', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/bmc368');
INSERT INTO IMPORT_CONFIGURATION VALUES ('jzr690', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/jzr690');
INSERT INTO IMPORT_CONFIGURATION VALUES ('gut906', 'TransportDirect.Datagateway.Framework.CommandLineImporter', 'D:/Gateway/bin/td.datagateway.framework.dll', 'D:/Gateway/bat/test.bat', '', '', 'D:/Gateway/dat/Processing/gut906');
GO
-- End of Data Gateway Properties

--Base URL for MCMS Template StaticNoPrint
INSERT INTO properties VALUES ('StaticNoPrint.baseurl', '<base href="http://192.12.43.32:80/Web/Templates/">', 'Web', 'UserPortal');

--Base URL for MCMS Template HelpFullJP
INSERT INTO properties VALUES ('HelpFullJP.baseurl', '<base href="http://192.12.43.32:80/Web/Templates/">', 'Web', 'UserPortal');

--------------------------------------------------------------------
-- END PROPERTY SERVICES
--------------------------------------------------------------------

--------------------------------------------------------------------
-- START REPORT STAGING DATA - REFERENCE DATA AND DEFAULT VALUES
--------------------------------------------------------------------

-- Define audit types

Insert Into ReportStagingDataAuditType
(RSDATID,   RSDATName)
Select
1, 'LastImport'
Union
Select
2, 'LatestImported'
Union
Select
3, 'LastArchive'


-- Define names of data type names to use in audit entries.
-- (The names of the stored procedures used to import report staging data are used
--  since an audit is performed for calls to each of them.)

Insert Into ReportStagingDataType
(RSDTID, RSDTName)
Select
1, 'TransferGazetteerEvents'
Union
Select
2, 'TransferJourneyPlanLocationEvents'
Union
Select
3, 'TransferJourneyPlanModeEvents'
Union
Select
4, 'TransferJourneyProcessingEvents'
Union
Select
5, 'TransferJourneyWebRequestEvents'
Union
Select
6, 'TransferLoginEvents'
Union
Select
7, 'TransferMapEvents'
Union
Select
8, 'TransferOperationalEvents'
Union
Select
9, 'TransferPageEntryEvents'
Union
Select
10, 'TransferReferenceTransactionEvents'
Union
Select
11, 'TransferRetailerHandoffEvents'
Union
Select
12, 'TransferWorkloadEvents'
Union
Select
13, 'TransferDataGatewayEvents'
Union
Select
14, 'TransferUserPreferenceSaveEvents'
Union
Select
15, 'TransferRoadPlanEvents'
Union
Select
16, 'DummyNameJourneyPlanRequestVerboseEvents'
Union
Select
17, 'DummyNameJourneyPlanResultsVerboseEvents'
Union
Select
18, 'TransferSessionEvents'

-- Provide default date values for audit dates. This can be any date in the past.
Insert Into ReportStagingDataAudit
(RSDTID, RSDATID, Event)
Select
1, 1, '11/20/2003'
Union
Select
1, 2, '11/20/2003'
Union
Select
1, 3, '11/20/2003'
Union
Select
2, 1, '11/20/2003'
Union
Select
2, 2, '11/20/2003'
Union
Select
2, 3, '11/20/2003'
Union
Select
3, 1, '11/20/2003'
Union
Select
3, 2, '11/20/2003'
Union
Select
3, 3, '11/20/2003'
Union
Select
4, 1, '11/20/2003'
Union
Select
4, 2, '11/20/2003'
Union
Select
4, 3, '11/20/2003'
Union
Select
5, 1, '11/20/2003'
Union
Select
5, 2, '11/20/2003'
Union
Select
5, 3, '11/20/2003'
Union
Select
6, 1, '11/20/2003'
Union
Select
6, 2, '11/20/2003'
Union
Select
6, 3, '11/20/2003'
Union
Select
7, 1, '11/20/2003'
Union
Select
7, 2, '11/20/2003'
Union
Select
7, 3, '11/20/2003'
Union
Select
8, 1, '11/20/2003'
Union
Select
8, 2, '11/20/2003'
Union
Select
8, 3, '11/20/2003'
Union
Select
9, 1, '11/20/2003'
Union
Select
9, 2, '11/20/2003'
Union
Select
9, 3, '11/20/2003'
Union
Select
10, 1, '11/20/2003'
Union
Select
10, 2, '11/20/2003'
Union
Select
10, 3, '11/20/2003'
Union
Select
11, 1, '11/20/2003'
Union
Select
11, 2, '11/20/2003'
Union
Select
11, 3, '11/20/2003'
Union
Select
12, 1, '11/20/2003'
Union
Select
12, 2, '11/20/2003'
Union
Select
12, 3, '11/20/2003'
Union
Select
13, 1, '11/20/2003'
Union
Select
13, 2, '11/20/2003'
Union
Select
13, 3, '11/20/2003'
Union
Select
14, 1, '11/20/2003'
Union
Select
14, 2, '11/20/2003'
Union
Select
14, 3, '11/20/2003'
Union
Select
15, 1, '11/20/2003'
Union
Select
15, 2, '11/20/2003'
Union
Select
15, 3, '11/20/2003'
Union
Select
16, 1, '11/20/2003'
Union
Select
16, 2, '11/20/2003'
Union
Select
16, 3, '11/20/2003'
Union
Select
17, 1, '11/20/2003'
Union
Select
17, 2, '11/20/2003'
Union
Select
17, 3, '11/20/2003'
Union
Select
18, 1, '11/20/2003'
Union
Select
18, 2, '11/20/2003'
Union
Select
18, 3, '11/20/2003'

--------------------------------------------------------------------
-- END REPORT STAGING DATA
--------------------------------------------------------------------
