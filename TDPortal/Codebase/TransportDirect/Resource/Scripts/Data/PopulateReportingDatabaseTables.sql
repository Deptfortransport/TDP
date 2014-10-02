-- ***********************************************
-- NAME 	: PopulateReportingDatabaseTables.sql
-- DESCRIPTION 	: Populates Reporting database tables, sets up reference data types
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/Data/PopulateReportingDatabaseTables.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:41:12   mturner
--Initial revision.

Use [Reporting]
GO

--------------------------------------------------------
-- Some location details were produced with the       --
-- aid of InstallReportDataDatabaseLocationHelper.xls --
--------------------------------------------------------

TRUNCATE TABLE CapacityBands
TRUNCATE TABLE WeekDays
TRUNCATE TABLE ResponseServiceCreditBands

DELETE FROM ReferenceTransactionType
DELETE FROM MapDisplayType
DELETE FROM MapCommandType
DELETE FROM JourneyPrepositionType
DELETE FROM JourneyPlanResponseType
DELETE FROM JourneyModeType
DELETE FROM JourneyAdminAreaType
DELETE FROM JourneyRegionType
DELETE FROM GazetteerType
DELETE FROM PageEntryType
DELETE FROM UserPreferenceType
DELETE FROM RetailerHandoffType

--------------------------------------------------

Insert Into CapacityBands
(CBNumber, CBMinRequests, CBMaxRequests, CBSurgePercentage)
Select
1, 0, 120000, 1.66667
Union
Select
2, 120000, 660000, 1.66667
Union
Select
3, 660000, 2000000, 1.66667
Union
Select
4, 2000000, 4000000, 1.66667
Union
Select
5, 4000000, 6000000, 1.66667
Union
Select
6, 6000000, 8000000, 1.66667
Union
Select
7, 8000000, 10000000, 1.66667

--------------------------------------------------

Insert Into PageEntryType
(PETID, PETCode, PETDescription)
Select
1, 'InitialPage', 'Initial Page'
Union
Select
2, 'JourneyPlannerInput', 'Journey Planner Input'
Union
Select
3, 'JourneyPlannerAmbiguity', 'Journey Planner Ambiguity'
Union
Select
4, 'JourneySummary', 'Journey Summary'
Union
Select
5, 'JourneyPlannerLocationMap', 'Journey Planner Location Map'
Union
Select
6, 'JourneyDetails', 'Journey Details'
Union
Select
7, 'JourneyMap', 'Journey Map'
Union
Select
8, 'JourneyAdjust', 'Journey Adjust'
Union
Select
9, 'JourneyFares', 'Journey Fares'
Union
Select
10, 'CompareAdjustedJourney', 'Compare Adjusted Journey'
Union
Select
11, 'DetailedLegMap', 'Detailed Leg Map'
Union
Select
12, 'WaitPage', 'Wait Page'
Union
Select
13, 'PrintableJourneySummary', 'Printable Journey Summary'
Union
Select
14, 'PrintableJourneyDetails', 'Printable Journey Details'
Union
Select
15, 'PrintableJourneyMaps', 'Printable Journey Maps'
Union
Select
16, 'PrintableJourneyMapInput', 'Printable Journey Map Input'
Union
Select
17, 'PrintableCompareAdjustedJourney', 'Printable Compare Adjusted Journey'
Union
Select
18, 'PrintableTicketRetailers', 'Printable Ticket Retailers'
Union
Select
19, 'PrintableJourneyFares', 'Printable Journey Fares'
Union
Select
20, 'FeedbackInitialPage', 'Feedback Initial Page'
Union
Select
21, 'ContactUsPage', 'Contact Us Page'
Union
Select
22, 'ClaimsInputPage', 'Claims Input Page'
Union
Select
23, 'ClaimsErrorsPage', 'Claims Errors Page'
Union
Select
24, 'ClaimPrintPage', 'Claim Print Page'
Union
Select
25, 'Feedback', 'Feedback'
Union
Select
26, 'Links', 'Links'
Union
Select
27, 'Help', 'Help'
Union
Select
28, 'GeneralMaps', 'General Maps'
Union
Select
29, 'SiteMap', 'Site Map'
Union
Select
30, 'NetworkMaps', 'Network Maps'
Union
Select
31, 'LocationInformation', 'Location Information'
Union
Select
32, 'Map', 'Map'
Union
Select
33, 'Home', 'Home'
Union
Select
34, 'TravelNews', 'TravelNews'
Union
Select
35, 'PrintableTravelNews', 'Printable Travel News'
Union
Select
36, 'DepartureBoards', 'Departure Boards'
Union
Select
37, 'TrafficMap', 'Traffic Map'
Union
Select
38, 'PrintableTrafficMap', 'Printable Traffic Map'
Union
Select
39, 'TicketRetailers', 'Ticket Retailers'
Union
Select
40, 'TicketRetailersHandOff', 'Ticket Retailers HandOff'
Union
Select
41, 'HelpFullJP', 'Help Full JP'
Union
Select
42, 'Error', 'Error'
Union
Select
43, 'ClaimsPolicy', 'Claims Policy'
Union
Select
44, 'Maintenance', 'Maintenance'

--------------------------------------------------

Insert Into GazetteerType
(GTID, GTCode, GTDescription)
Select
1, 'GazetteerAddress', 'Address'
Union
Select
2, 'GazetteerPostCode', 'Post Code'
Union
Select
3, 'GazetteerPointOfInterest', 'Point Of Interest'
Union
Select
4, 'GazetteerMajorStations', 'Major Stations'
Union
Select
5, 'GazetteerAllStations', 'All Stations'
Union
Select
6, 'GazetteerLocality', 'Locality'

--------------------------------------------------

Insert Into JourneyRegionType
(JRTID, JRTCode, JRTDescription, JRTMaxMsDuration)
Select
1, 'EA', 'East Anglia', 1000
Union
Select
2, 'EM', 'East Midlands', 1000
Union
Select
3, 'L', 'London', 1000
Union
Select
4, 'NE', 'North East', 1000
Union
Select
5, 'NW', 'North West', 1000
Union
Select
6, 'S', 'Scotland', 1000
Union
Select
7, 'SE', 'South East', 1000
Union
Select
8, 'SW', 'South West', 1000
Union
Select
9, 'W', 'Wales', 1000
Union
Select
10, 'WM', 'West Midlands', 1000
Union
Select
11, 'Y', 'Yorkshire', 1000
Union
Select
12, 'NX', 'National Express', 1000

--------------------------------------------------

Insert Into JourneyAdminAreaType(JAATID, JAATJRTID, JAATCode, JAATDescription)
Select
1, 8, '001', 'Bath and North East Somerset'
Union
Select
2, 5, '002', 'Blackburn with Darwen'
Union
Select
3, 5, '003', 'Blackpool'
Union
Select
4, 9, '004', 'Blaenau Gwent'
Union
Select
5, 8, '005', 'Bournemouth'
Union
Select
6, 7, '006', 'Bracknell Forest'
Union
Select
7, 9, '007', 'Bridgend'
Union
Select
8, 7, '008', 'Brighton and Hove'
Union
Select
9, 8, '009', 'Bristol, City of'
Union
Select
10, 9, '010', 'Caerphilly'
Union
Select
11, 9, '011', 'Cardiff'
Union
Select
12, 9, '012', 'Carmarthenshire'
Union
Select
13, 9, '013', 'Ceredigion'
Union
Select
14, 9, '014', 'Conwy'
Union
Select
15, 4, '015', 'Darlington'
Union
Select
16, 9, '016', 'Denbighshire'
Union
Select
17, 2, '017', 'Derby'
Union
Select
18, 11, '018', 'East Riding of Yorkshire'
Union
Select
19, 9, '019', 'Flintshire'
Union
Select
20, 9, '020', 'Gwynedd'
Union
Select
21, 5, '021', 'Halton'
Union
Select
22, 4, '022', 'Hartlepool'
Union
Select
23, 10, '023', 'Herefordshire, County of'
Union
Select
24, 9, '024', 'Isle of Anglesey'
Union
Select
25, 7, '025', 'Isle of Wight'
Union
Select
26, 11, '026', 'Kingston upon Hull, City of'
Union
Select
27, 2, '027', 'Leicester'
Union
Select
28, 7, '028', 'Luton'
Union
Select
29, 7, '029', 'Medway'
Union
Select
30, 9, '030', 'Merthyr Tydfil'
Union
Select
31, 4, '031', 'Middlesbrough'
Union
Select
32, 7, '032', 'Milton Keynes'
Union
Select
33, 9, '033', 'Monmouthshire'
Union
Select
34, 9, '034', 'Neath Port Talbot'
Union
Select
35, 9, '035', 'Newport'
Union
Select
36, 2, '036', 'North East Lincolnshire'
Union
Select
37, 2, '037', 'North Lincolnshire'
Union
Select
38, 8, '038', 'North Somerset'
Union
Select
39, 2, '039', 'Nottingham'
Union
Select
40, 9, '040', 'Pembrokeshire'
Union
Select
41, 2, '041', 'Peterborough'
Union
Select
42, 8, '042', 'Plymouth'
Union
Select
43, 8, '043', 'Poole'
Union
Select
44, 8, '044', 'Portsmouth'
Union
Select
45, 9, '045', 'Powys'
Union
Select
46, 7, '046', 'Reading'
Union
Select
47, 4, '047', 'Redcar and Cleveland'
Union
Select
48, 9, '048', 'Rhondda, Cynon, Taff'
Union
Select
49, 2, '049', 'Rutland'
Union
Select
50, 7, '050', 'Slough'
Union
Select
51, 8, '051', 'South Gloucestershire'
Union
Select
52, 8, '052', 'Southampton'
Union
Select
53, 7, '053', 'Southend-on-Sea'
Union
Select
54, 4, '054', 'Stockton-on-Tees'
Union
Select
55, 10, '055', 'Stoke-on-Trent'
Union
Select
56, 9, '056', 'Swansea'
Union
Select
57, 8, '057', 'Swindon'
Union
Select
58, 10, '058', 'Telford and Wrekin'
Union
Select
59, 7, '059', 'Thurrock'
Union
Select
60, 8, '060', 'Torbay'
Union
Select
61, 9, '061', 'Torfaen'
Union
Select
62, 9, '062', 'Vale of Glamorgan, The'
Union
Select
63, 5, '063', 'Warrington'
Union
Select
64, 7, '064', 'West Berkshire'
Union
Select
65, 7, '065', 'Windsor and Maidenhead'
Union
Select
66, 7, '066', 'Wokingham'
Union
Select
67, 9, '067', 'Wrexham'
Union
Select
68, 11, '068', 'York'
Union
Select
69, 7, '069', 'Bedfordshire'
Union
Select
70, 7, '070', 'Buckinghamshire'
Union
Select
71, 1, '071', 'Cambridgeshire'
Union
Select
72, 5, '072', 'Cheshire'
Union
Select
73, 8, '073', 'Cornwall and Isles of Scilly'
Union
Select
74, 5, '074', 'Cumbria'
Union
Select
75, 2, '075', 'Derbyshire'
Union
Select
76, 8, '076', 'Devon'
Union
Select
77, 8, '077', 'Dorset'
Union
Select
78, 4, '078', 'Durham'
Union
Select
79, 7, '079', 'East Sussex'
Union
Select
80, 7, '080', 'Essex'
Union
Select
81, 8, '081', 'Gloucestershire'
Union
Select
82, 3, '082', 'Greater London'
Union
Select
83, 5, '083', 'Greater Manchester'
Union
Select
84, 8, '084', 'Hampshire'
Union
Select
85, 7, '085', 'Hertfordshire'
Union
Select
86, 7, '086', 'Kent'
Union
Select
87, 5, '087', 'Lancashire'
Union
Select
88, 2, '088', 'Leicestershire'
Union
Select
89, 2, '089', 'Lincolnshire'
Union
Select
90, 5, '090', 'Merseyside'
Union
Select
91, 1, '091', 'Norfolk'
Union
Select
92, 11, '092', 'North Yorkshire'
Union
Select
93, 2, '093', 'Northamptonshire'
Union
Select
94, 4, '094', 'Northumberland'
Union
Select
95, 2, '095', 'Nottinghamshire'
Union
Select
96, 7, '096', 'Oxfordshire'
Union
Select
97, 10, '097', 'Shropshire'
Union
Select
98, 8, '098', 'Somerset'
Union
Select
99, 11, '099', 'South Yorkshire'
Union
Select
100, 10, '100', 'Staffordshire'
Union
Select
101, 1, '101', 'Suffolk'
Union
Select
102, 7, '102', 'Surrey'
Union
Select
103, 4, '103', 'Tyne and Wear'
Union
Select
104, 10, '104', 'Warwickshire'
Union
Select
105, 10, '105', 'West Midlands'
Union
Select
106, 7, '106', 'West Sussex'
Union
Select
107, 11, '107', 'West Yorkshire'
Union
Select
108, 8, '108', 'Wiltshire'
Union
Select
109, 10, '109', 'Worcestershire'
Union
Select
110, 6, '110', 'National - Railtrack'
Union
Select
111, 6, '111', 'Aberdeen City'
Union
Select
112, 6, '112', 'Aberdeenshire'
Union
Select
113, 6, '113', 'Angus'
Union
Select
114, 6, '114', 'Argyll & Bute'
Union
Select
115, 6, '115', 'Scottish Borders'
Union
Select
116, 6, '116', 'Clackmannanshire'
Union
Select
117, 6, '117', 'West Dunbartonshire'
Union
Select
118, 6, '118', 'Dumfries & Galloway'
Union
Select
119, 6, '119', 'Dundee City'
Union
Select
120, 6, '120', 'East Ayrshire'
Union
Select
121, 6, '121', 'East Dunbartonshire'
Union
Select
122, 6, '122', 'East Lothian'
Union
Select
123, 6, '123', 'East Renfrewshire'
Union
Select
124, 6, '124', 'City of Edinburgh'
Union
Select
125, 6, '125', 'Falkirk'
Union
Select
126, 6, '126', 'Fife'
Union
Select
127, 6, '127', 'Glasgow City'
Union
Select
128, 6, '128', 'Highland'
Union
Select
129, 6, '129', 'Inverclyde'
Union
Select
130, 6, '130', 'Midlothian'
Union
Select
131, 6, '131', 'Moray'
Union
Select
132, 6, '132', 'North Ayrshire'
Union
Select
133, 6, '133', 'North Lanarkshire'
Union
Select
134, 6, '134', 'Orkney Islands'
Union
Select
135, 6, '135', 'Perth & Kinross'
Union
Select
136, 6, '136', 'Renfrewshire'
Union
Select
137, 6, '137', 'Shetland Islands'
Union
Select
138, 6, '138', 'South Ayrshire'
Union
Select
139, 6, '139', 'South Lanarkshire'
Union
Select
140, 6, '140', 'Stirling'
Union
Select
141, 6, '141', 'West Lothian'
Union
Select
142, 6, '142', 'Western Isles'
Union
Select
143, 5, '143', 'National - National Express'
Union
Select
144, 5, '144', 'National - Railtrack - WSA'

--------------------------------------------------

Insert Into JourneyModeType
(JMTID, JMTCode, JMTDescription)
Select
1, 'Air', 'Air'
Union
Select
2, 'Bus', 'Bus'
Union
Select
3, 'Car', 'Car'
Union
Select
4, 'Coach', 'Coach'
Union
Select
5, 'Cycle', 'Cycle'
Union
Select
6, 'Drt', 'DRT'
Union
Select
7, 'Ferry', 'Ferry'
Union
Select
8, 'Metro', 'Metro'
Union
Select
9, 'Rail', 'Rail'
Union
Select
10, 'Taxi', 'Taxi'
Union
Select
11, 'Tram', 'Tram'
Union
Select
12, 'Underground', 'Underground'
Union
Select
13, 'Walk', 'Walk'

--------------------------------------------------

Insert Into JourneyPlanResponseType
(JPRTID, JPRTCode, JPRTDescription)
Select
1, 'Failure', 'Failure'
Union
Select
2, 'ZeroResults', '0 Results'
Union
Select
3, 'Results', '1+ Results'

--------------------------------------------------

Insert Into JourneyPrepositionType
(JPTID, JPTCode, JPTDescription)
Select
1, 'From', 'From'
Union
Select
2, 'To', 'To'
Union
Select
3, 'Via', 'Via'

--------------------------------------------------

Insert Into MapCommandType
(MCTID, MCTCode, MCTDescription)
Select
1, 'MapInitialDisplay', 'Initial Display'
Union
Select
2, 'MapZoom', 'Zoom'
Union
Select
3, 'MapPan', 'Pan'
Union
Select
4, 'MapOverlay', 'Overlay'

--------------------------------------------------

Insert Into MapDisplayType
(MDTID, MDTCode, MDTDescription)
Select
1, 'OSStreetView', 'OS Street View'
Union
Select
2, 'ScaleColourRaster50', '1:50,000 Scale Colour Raster'
Union
Select
3, 'ScaleColourRaster250', '1:250,000 Scale Colour Raster'
Union
Select
4, 'MiniScale', 'MiniScale'
Union
Select
5, 'Strategi', 'Strategi'

--------------------------------------------------

Insert Into ReferenceTransactionType
(RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
Select
1, 'SimpleJourneyRequest1', 'Simple Query 1', 1, 1, 2000, 99.3, 98
Union
Select
2, 'SimpleJourneyRequest2', 'Simple Query 2', 1, 1, 2000, 99.3, 98
Union
Select
3, 'ComplexJourneyRequest', 'Complex Query 1', 0, 1, 5000, 99.3, 98
Union
Select
4, 'PricingRequest1', 'Pricing Request 1', 0, 0, 5000, 99.3, 98
Union
Select
5, 'PricingRequest2', 'Pricing Request 2', 0, 0, 5000, 99.3, 98
Union
Select
6, 'StationInfo1', 'Station Information 1', 0, 0, 5000, 99.3, 98
Union
Select
7, 'SoftContent1', 'Soft Content 1', 0, 0, 5000, 99.3, 98

--------------------------------------------------

Insert Into ResponseServiceCreditBands
(RSCBStart, RSCBEnd, RSCBPoints)
Select
0, 97.3, 50
Union
Select
97.3, 97.8, 40
Union
Select
97.8, 98.3, 30
Union
Select
98.3, 98.8, 20
Union
Select
98.8, 99.3, 10

--------------------------------------------------

Insert Into RetailerHandoffType
(RHTID, RHTCode, RHTDescription)
Select
1, 'Trainline', 'Trainline'
Union
Select
2, 'QJump', 'QJump'
Union
Select
3, 'NationalExpress', 'National Express'
Union
Select
4, 'ScottishCityLink', 'Scottish City Link'

--------------------------------------------------

Insert Into UserPreferenceType
(UPTID, UPTCode, UPTDescription)
Select
1, 'JourneyPlanningOptions', 'Journey Planning Options'
Union
Select
2, 'FareOptions', 'Fare Options'
Union
Select
3, 'News', 'News'

--------------------------------------------------

Insert Into WeekDays
(WDID, WDShortName, WDFullName)
Select
1, 'Mon', 'Monday'
Union
Select
2, 'Tue', 'Tuesday'
Union
Select
3, 'Wed', 'Wednesday'
Union
Select
4, 'Thu', 'Thursday'
Union
Select
5, 'Fri', 'Friday'
Union
Select
6, 'Sat', 'Saturday'
Union
Select
7, 'Sun', 'Sunday'
GO