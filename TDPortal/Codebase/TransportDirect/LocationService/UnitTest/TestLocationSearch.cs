// *********************************************** 
// NAME                 : TestLocationSearch.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 04/09/2003 
// DESCRIPTION : Test for the LocationSearch class 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestLocationSearch.cs-arc  $ 
//
//   Rev 1.3   Jun 15 2010 09:30:04   apatel
//Updated 
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.2   Jun 14 2010 12:12:12   apatel
//Updated for DropDownLocationProvider unit tests
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.1   Mar 16 2010 16:42:34   mmodi
//Add International Planner Gaz
//Resolution for 5461: TD Extra - Code review changes
//
//   Rev 1.0   Nov 08 2007 12:25:46   mturner
//Initial revision.
//
//   Rev 1.24   Mar 30 2006 13:49:40   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.23   Mar 28 2006 15:52:42   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup/GazopsWebTest.asmx
//
//   Rev 1.22   Feb 23 2006 10:53:22   RWilby
//Merged stream3129
//
//   Rev 1.21   Feb 07 2006 09:59:28   mtillett
//Fix unit tests broken by changes to the LocalityGazetteer.cs v1.26
//
//   Rev 1.20   Apr 13 2005 08:58:06   rscott
//Changes made to some test methods as a consequence of IR1978 enhancement to reject single word addresses.
//
//   Rev 1.19   Apr 08 2005 13:55:16   rscott
//New test added TestSearchAddressPostcodeNoMatchVague.
//
//   Rev 1.18   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.17   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.16   Sep 11 2004 17:35:00   RPhilpott
//Tests updated for addition of FindNearestLocality and FindExchangePointsInRadius.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1328: Find nearest stations/airports does not return any results
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//
//   Rev 1.15   Sep 10 2004 15:36:06   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.14   Sep 03 2004 11:14:16   RPhilpott
//Corrected failing tests.
//Resolution for 1354: Find A Flight results - wrong origin/dest location on output
//
//   Rev 1.13   Aug 20 2004 15:02:08   passuied
//Test for new GetRemainingStationTypes() method
//Resolution for 1415: FindNearest Station/Airport Optimisation
//
//   Rev 1.12   Jul 23 2004 16:31:26   RPhilpott
//NUnit refactoring -- use TestMockGisQuery, eliminating need for stub version of td.interactivemapping.dll.
//
//   Rev 1.11   Jul 22 2004 16:13:08   RPhilpott
//FindStations changes.
//
//   Rev 1.10   Jul 09 2004 13:09:22   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.9   Apr 01 2004 12:09:46   geaton
//Unit test refactoring exercise.
//
//   Rev 1.8   Mar 31 2004 20:27:48   geaton
//Fixed TestSearchAddressPostcode test that was out of date with implementation that it was testing.
//
//   Rev 1.7   Mar 18 2004 08:48:24   geaton
//Added logging init.
//
//   Rev 1.6   Mar 03 2004 15:26:04   COwczarek
//Changes required due to modifying LocationQueryResult and LocationSearch classes for DEL 5.2 ambiguity resolution changes
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.5   Dec 03 2003 12:21:38   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.4   Nov 07 2003 11:05:16   passuied
//updated dummy initialisation class
//
//   Rev 1.3   Sep 23 2003 13:26:34   passuied
//Fixed to get GISQuery from ServiceDiscovery
//
//   Rev 1.2   Sep 20 2003 16:59:58   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.1   Sep 15 2003 10:51:28   passuied
//Design changes
//
//   Rev 1.0   Sep 05 2003 15:30:12   passuied
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties; 
using AddDataModule = TransportDirect.UserPortal.AdditionalDataModule;

using NUnit.Framework;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// This service initialisation class is used by most
	///  of the other LocationService NUnit tests ...
	/// </summary>
	public class TestInitialisation : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
		
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add (ServiceDiscoveryKey.AirDataProvider, new TestMockAirDataProvider());
			serviceCache.Add (ServiceDiscoveryKey.GisQuery, new TestMockGisQuery());
			serviceCache.Add (ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());
			serviceCache.Add( ServiceDiscoveryKey.AdditionalData ,  new AddDataModule.TestStubAdditionalData());
			serviceCache.Add (ServiceDiscoveryKey.PlaceDataProvider, new TestMockPlaceDataProvider());
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TestMockCache());
            serviceCache.Add(ServiceDiscoveryKey.InternationalPlaceGazetteer, new InternationalPlaceGazetteerFactory());
            
            // Enable Test ChangeNotification
            serviceCache.Add(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());
            serviceCache.Add(ServiceDiscoveryKey.ScriptRepository, new ScriptRepositoryFactory(Properties.Current["ScriptRepositoryRoot"], Properties.Current["ScriptRepositoryPath"]));
            
			// Create TD Logging service.
			ArrayList errors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[0];
			try
			{				
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException tdException)
			{
				throw tdException;
			}

		}
	}
	/// <summary>
	/// Test for the LocationSearch class
	/// </summary>
	[TestFixture]
	public class TestLocationSearch
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialisation());
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		/// <summary>
		///  Tests that the FindBusStops method populates the TDLocation.NaPTANS property with NaPTANs 
		///  for nearest bus stops to TDLocation.GridReference.
		/// </summary>
		[Test]
		public void TestFindBusStops()
		{
			//Test1: Test FindBusStops.
			//Create OSGridReference for EC1R 3HN postcode (AO office Farringdon Road, London) 
			TDLocation tdlocation = new TDLocation();
			tdlocation.GridReference.Easting=531261;
			tdlocation.GridReference.Northing=182231;
			
			LocationSearch.FindBusStops(ref tdlocation,10);
			Assert.AreEqual(4,tdlocation.NaPTANs.Length,"TestFindBusStops Test1 Failed");
			
			//Test2: Test FindBusStops maxReturn parameter that specifics the maximum number of results to return
			TDLocation tdlocationTest2 = new TDLocation();
			//Set maxReturn parameter to 2. We expect 2 results out of a possible 4.
			LocationSearch.FindBusStops(ref tdlocationTest2,2);
			Assert.AreEqual(2,tdlocationTest2.NaPTANs.Length,"TestFindBusStops Test2 Failed: maxReturn parameter");
		}

		/// <summary>
		///  Tests that the FindPostcode method correctly performs a postcode gazetter search for a full UK postcode
		/// </summary>
		[Test]
		[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
		public void TestFindPostcode()
		{
			//Test1: Test that correct exception is throw when the postcode is not a full uk postcode
			try
			{
				LocationSearch.FindPostcode("sa11","TestSessionID",false);
			}
			catch(TDException tdex)
			{
				Assert.AreEqual(TDExceptionIdentifier.LSInvalidFullUKPostcode,tdex.Identifier,"TestFindPostcode Test1 Failed:Incorrect exception Identifier when the postcode is not a full uk postcode");
			}
			

			//Test2: Test mehtod returns correct result
			LocationChoiceList locationChoiceList = LocationSearch.FindPostcode("sa11 2nf","TestSessionID",false);
			Assert.AreEqual(1,locationChoiceList.Count,"TestFindPostcode failed","TestFindPostcode Test2 Failed:Excepting 1 LocationChoice");
			// Get the location choice returned in query result.
			LocationChoice choice = (LocationChoice)locationChoiceList[0];
			// Check that easting and northing of the query result.
			Assert.AreEqual(195733, choice.OSGridReference.Easting, "TestFindPostcode Test2 Failed:Incorrect easting in LocationChoice.");
			Assert.AreEqual(139059, choice.OSGridReference.Northing, "TestFindPostcode Test2 Failed:Incorrect northing in LocationChoice.");
			
		}

		[Test]
		[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
		public void TestSearchAddressPostcode()
		{
			LocationSearch search = new LocationSearch();
			// 1. Test StartSearch with type AddressPostcode
			LocationChoiceList choices = search.StartSearch ("euston,", SearchType.AddressPostCode, false, 20, "sessionID", false);

			// Cf. GazopsWebTest\Xml\DrillDown\euston.xml
			Assert.AreEqual(12 , choices.Count, "Test Failed");
			
			// Check that the 4th element has the correct info
			//	<PICK_LIST_ENTRY id="3">
			//  <MATCH_COUNT>1</MATCH_COUNT> 
			//  <CRITERIA_FIELD> LOCALITY</CRITERIA_FIELD> 
			//  <IDENTITY> LOCALITY:E0043105</IDENTITY> 
			//  <SCORE>96.6666666666667</SCORE> 
			//  <DESCRIPTION>Easton, West Berkshire</DESCRIPTION> 
			//  <FINAL>False</FINAL> 
			//  </PICK_LIST_ENTRY>

			LocationChoice choice = (LocationChoice)choices[3];
			// Note that the spaces in criterias and value are retained!!!
			Assert.AreEqual(" LOCALITY", choice.PicklistCriteria, "Test Failed");
			Assert.AreEqual(" LOCALITY:E0043105", choice.PicklistValue, "Test Failed");
			Assert.AreEqual(96.6666666666667, choice.Score, "Test Failed");
			Assert.AreEqual("Easton, West Berkshire", choice.Description, "Test Failed");
			Assert.AreEqual(true, choice.HasChilden, "Test Failed");

			TDLocation location = new TDLocation();
			// 1b. Try to get Location details... Should get an exception
			bool exception_thrown = false;
			try
			{
				search.GetLocationDetails(ref location, (LocationChoice)choices[3]);
			}
			catch (TDException )
			{
				exception_thrown = true;
			}
			Assert.IsTrue(exception_thrown == true);


			// 2. Drill down from choice 0
			// <PICK_LIST_ENTRY id="0"><MATCH_COUNT>1</MATCH_COUNT>			<CRITERIA_FIELD>LOCALITY</CRITERIA_FIELD><IDENTITY>LOCALITY:E0051520</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Euston, Thetford, Suffolk, St. Edmundsbury</DESCRIPTION><FINAL>False</FINAL></PICK_LIST_ENTRY>
			LocationChoiceList choices2 = search.DrillDown(search.CurrentLevel, (LocationChoice)choices[0]);

			// We Get the XML Results as follows
			// <PICK_LIST_ENTRY id="0"><MATCH_COUNT>1</MATCH_COUNT><CRITERIA_FIELD>STOP</CRITERIA_FIELD><IDENTITY>STOP:390050032</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Euston: A1088 C634, Barnham Turn, Barnham Turn, A1088 C634</DESCRIPTION><FINAL>True</FINAL></PICK_LIST_ENTRY>
			//<PICK_LIST_ENTRY id="1"><MATCH_COUNT>1</MATCH_COUNT><CRITERIA_FIELD>STOP</CRITERIA_FIELD><IDENTITY>STOP:390050033</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Euston: A1088 C634, opp Barnham Turn, opp Barnham Turn, A1088 C634</DESCRIPTION><FINAL>True</FINAL></PICK_LIST_ENTRY>

			// Check for info returned for entry index=1
			choice = (LocationChoice)choices2[1];
			// Note that the spaces in criterias and value are retained!!!
			Assert.AreEqual("STOP", choice.PicklistCriteria, "Test Failed");
			Assert.AreEqual("STOP:390050033", choice.PicklistValue, "Test Failed");
			Assert.AreEqual(100, choice.Score, "Test Failed");
			Assert.AreEqual("Euston: A1088 C634, opp Barnham Turn, opp Barnham Turn, A1088 C634", choice.Description, "Test Failed");
			Assert.AreEqual(false, choice.HasChilden, "Test Failed");


			// 3. Try to drill down again... Should generate an exception (no children)
			bool thrown = false;
			try
			{
				search.DrillDown(search.CurrentLevel, (LocationChoice)choices2[0]);
			}
			catch (TDException)
			{
				thrown = true;
			}
			Assert.IsTrue(thrown == true);

			// 4. Get LocationDetails for choice 0;
						search.GetLocationDetails(
										ref location,
										(LocationChoice)choices2[0]);

			// We get the XML response as follows
			// <ADDRESS_MATCH><F><N>GAZOPS_FORMATTED_ADDRESS</N><V tp="xml"><ADDLINE>Euston: A1088 C634</ADDLINE><ADDLINE>Barnham Turn</ADDLINE><ADDLINE>Barnham Turn</ADDLINE><ADDLINE>A1088 C634</ADDLINE><ADDLINE>Euston, Thetford, St. Edmundsbury, Suffolk</ADDLINE></V></F><F><N>ATCO_CODE</N><V>390050032</V><V>390050032</V></F><F><N>GAZOPS_UNIQUE_ID</N><V>390050032</V><V>390050032</V></F><F><N>GAZOPS_ALIAS_ID</N><V>390050032</V><V>390050032</V></F><F><N>NATIONAL_GAZETTEER_ID</N><V>E0051520</V><V>E0051520</V></F><F><N>COMMON_NAME</N><V>Euston: A1088 C634</V><V>Euston: A1088 C634</V></F><F><N>IDENTIFIER</N><V>Barnham Turn</V><V>Barnham Turn</V></F><F><N>LANDMARK</N><V>Barnham Turn</V><V>Barnham Turn</V></F><F><N>STREET</N><V>A1088 C634</V><V>A1088 C634</V></F><F><N>GAZOPS_EASTING</N><V>589561</V><V>589561</V></F><F><N>GAZOPS_NORTHING</N><V>278888</V><V>278888</V></F><F><N>STOP_TYPE</N><V>BCT</V><V>BCT</V></F><F><N>DIRECTION</N><V>N</V><V>N</V></F><F><N>GRID_TYPE</N><V /><V /></F><F><N>BUS_STOP_TYPE</N><V>CUS</V><V>CUS</V></F><F><N>LOCALITY_CENTRE</N><V></V><V></V></F><F><N>HIERARCHY_NAME</N><V>Euston, Thetford, St. Edmundsbury, Suffolk</V><V>Euston, Thetford, St. Edmundsbury, Suffolk</V></F></ADDRESS_MATCH>

			// And default GIS Information (see GIS stub)
			Assert.AreEqual("Euston: A1088 C634,Barnham Turn,Barnham Turn,A1088 C634,Euston, Thetford, St. Edmundsbury, Suffolk" ,location.Description, "Test failed");
			Assert.AreEqual(58956, location.GridReference.Easting, "Test failed");
			Assert.AreEqual(27888,  location.GridReference.Northing, "Test failed");

			Assert.AreEqual(RequestPlaceType.Coordinate,  location.RequestPlaceType, "Test failed");
			Assert.AreEqual(SearchType.AddressPostCode,  location.SearchType, "Test failed");
			Assert.AreEqual(TDLocationStatus.Valid, location.Status, "Test failed");
			
			Assert.AreEqual(location.Toid.Length, 3);
			Assert.AreEqual(location.Toid[0], "4000000013194143");
			Assert.AreEqual(location.Toid[1], "4000000013194317");
			Assert.AreEqual(location.Toid[2], "4000000013194231");
		
			Assert.AreEqual(location.NaPTANs.Length, 0);

			// Finally, Test ClearAll and ClearSearch
			search.ClearSearch();

			Assert.AreEqual(-1, search.CurrentLevel, "Test failed"); //A value of -1 indicates no query result has yet been associated 
			Assert.AreEqual(null, search.GetCurrentChoices(0), "Test failed");

			search.ClearAll();

			Assert.AreEqual(-1, search.CurrentLevel, "Test failed");
			Assert.AreEqual(null, search.GetCurrentChoices(0), "Test failed");
			Assert.AreEqual(string.Empty, search.InputText, "Test failed");
			Assert.AreEqual(false, search.FuzzySearch, "Test failed");

		}

		/// <summary>
		/// Tests that a vague result can be returned flagged by the VagueSearch property
		/// </summary>
		[Test]
		[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
		public void TestSearchAddressPostcodeNoMatchVague()
		{
			LocationSearch search = new LocationSearch();
			// 1. Test StartSearch with type AddressPostcode
			LocationChoiceList choices = search.StartSearch ("north road", SearchType.AddressPostCode, false, 20, "sessionID", false);

//			// Cf. GazopsWebTest\Xml\DrillDownAddressQuery\vague.xml
//			Assert.AreEqual(12 , choices.Count, "Test Failed");
//			
//			<GAZOPS_DATA_REQUEST operation="ADDRESS_MATCH">
//				<OPERATION_RESULT>No Match Found</OPERATION_RESULT>
//				<GAZOPS_VAGUE />
//			</GAZOPS_DATA_REQUEST>

			Assert.AreEqual(true, choices.IsVague, "Test failed as not vague");


		}


		[Test]
		[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
		public void TestSearchLocality()
		{
			LocationSearch search = new LocationSearch();

			// 1. Test StartSearch with type Locality
			LocationChoiceList choices = search.StartSearch ("euston", SearchType.Locality,  false, 20, "sessionID", false);

			// Cf. GazopsWebTest\Xml\DrillDown\euston.xml
			Assert.AreEqual(12 , choices.Count, "Test Failed");
			
			// Check that the 4th element has the correct info
			//	<PICK_LIST_ENTRY id="3">
			//  <MATCH_COUNT>1</MATCH_COUNT> 
			//  <CRITERIA_FIELD> LOCALITY</CRITERIA_FIELD> 
			//  <IDENTITY> LOCALITY:E0043105</IDENTITY> 
			//  <SCORE>96.6666666666667</SCORE> 
			//  <DESCRIPTION>Easton, West Berkshire</DESCRIPTION> 
			//  <FINAL>False</FINAL> 
			//  </PICK_LIST_ENTRY>

			LocationChoice choice = (LocationChoice)choices[3];
			// Note that the spaces in criterias and value are retained!!!
			Assert.AreEqual(" LOCALITY", choice.PicklistCriteria, "Test Failed");
			Assert.AreEqual(" LOCALITY:E0043105", choice.PicklistValue, "Test Failed");
			Assert.AreEqual(96.6666666666667, choice.Score, "Test Failed");
			Assert.AreEqual("Easton, West Berkshire", choice.Description, "Test Failed");
			Assert.AreEqual(true, choice.HasChilden, "Test Failed");


			TDLocation location = new TDLocation();
			// 1b. Try to get Location details... Should get an exception
			bool exception_thrown = false;
			try
			{
				search.GetLocationDetails(ref location, (LocationChoice)choices[3]);
			}
			catch (TDException )
			{
				exception_thrown = true;
			}
			Assert.IsTrue(exception_thrown == true);

			// 2. Drill down from choice index 1
			// <PICK_LIST_ENTRY id="1"><MATCH_COUNT>1</MATCH_COUNT><CRITERIA_FIELD>LOCALITY</CRITERIA_FIELD><IDENTITY>LOCALITY:E0043733</IDENTITY><SCORE>96.6666666666667</SCORE><DESCRIPTION>Easton, Cambridgeshire, Huntingdonshire</DESCRIPTION><FINAL>False</FINAL></PICK_LIST_ENTRY>
			LocationChoiceList choices2 = search.DrillDown(search.CurrentLevel, (LocationChoice)choices[1]);
			// Don't check the result... Will be done in next section (just checking we can drill again from lower level here !!!

			// 3. Drill down from choice 0 at previous level (user changed his mind)
			
			// <PICK_LIST_ENTRY id="0"><MATCH_COUNT>1</MATCH_COUNT>			<CRITERIA_FIELD>LOCALITY</CRITERIA_FIELD><IDENTITY>LOCALITY:E0051520</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Euston, Thetford, Suffolk, St. Edmundsbury</DESCRIPTION><FINAL>False</FINAL></PICK_LIST_ENTRY>
			choices2 = search.DrillDown(search.CurrentLevel-1, (LocationChoice)choices[0]);

			// We Get the XML Results as follows
			// <PICK_LIST_ENTRY id="0"><MATCH_COUNT>1</MATCH_COUNT><CRITERIA_FIELD>STOP</CRITERIA_FIELD><IDENTITY>STOP:390050032</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Euston: A1088 C634, Barnham Turn, Barnham Turn, A1088 C634</DESCRIPTION><FINAL>True</FINAL></PICK_LIST_ENTRY>
			//<PICK_LIST_ENTRY id="1"><MATCH_COUNT>1</MATCH_COUNT><CRITERIA_FIELD>STOP</CRITERIA_FIELD><IDENTITY>STOP:390050033</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Euston: A1088 C634, opp Barnham Turn, opp Barnham Turn, A1088 C634</DESCRIPTION><FINAL>True</FINAL></PICK_LIST_ENTRY>

			// Check for info returned for entry index=1
			choice = (LocationChoice)choices2[1];
			// Note that the spaces in criterias and value are retained!!!
			Assert.AreEqual("STOP", choice.PicklistCriteria, "Test Failed");
			Assert.AreEqual("STOP:390050033", choice.PicklistValue, "Test Failed");
			Assert.AreEqual(100, choice.Score, "Test Failed");
			Assert.AreEqual("Euston: A1088 C634, opp Barnham Turn, opp Barnham Turn, A1088 C634", choice.Description, "Test Failed");
			Assert.AreEqual(false, choice.HasChilden, "Test Failed");


			// 4. Try to drill down again... Should generate an exception (no children)
			bool thrown = false;
			try
			{
				search.DrillDown(search.CurrentLevel, (LocationChoice)choices2[0]);
			}
			catch (TDException)
			{
				thrown = true;
			}
			Assert.IsTrue(thrown == true);

			// 5. Get LocationDetails for choice 0;
			search.GetLocationDetails(
				ref location,
				(LocationChoice)choices2[0]);

			// We get the XML response as follows
			// <ADDRESS_MATCH><F><N>GAZOPS_FORMATTED_ADDRESS</N><V tp="xml"><ADDLINE>Euston: A1088 C634</ADDLINE><ADDLINE>Barnham Turn</ADDLINE><ADDLINE>Barnham Turn</ADDLINE><ADDLINE>A1088 C634</ADDLINE><ADDLINE>Euston, Thetford, St. Edmundsbury, Suffolk</ADDLINE></V></F><F><N>ATCO_CODE</N><V>390050032</V><V>390050032</V></F><F><N>GAZOPS_UNIQUE_ID</N><V>390050032</V><V>390050032</V></F><F><N>GAZOPS_ALIAS_ID</N><V>390050032</V><V>390050032</V></F><F><N>NATIONAL_GAZETTEER_ID</N><V>E0051520</V><V>E0051520</V></F><F><N>COMMON_NAME</N><V>Euston: A1088 C634</V><V>Euston: A1088 C634</V></F><F><N>IDENTIFIER</N><V>Barnham Turn</V><V>Barnham Turn</V></F><F><N>LANDMARK</N><V>Barnham Turn</V><V>Barnham Turn</V></F><F><N>STREET</N><V>A1088 C634</V><V>A1088 C634</V></F><F><N>GAZOPS_EASTING</N><V>589561</V><V>589561</V></F><F><N>GAZOPS_NORTHING</N><V>278888</V><V>278888</V></F><F><N>STOP_TYPE</N><V>BCT</V><V>BCT</V></F><F><N>DIRECTION</N><V>N</V><V>N</V></F><F><N>GRID_TYPE</N><V /><V /></F><F><N>BUS_STOP_TYPE</N><V>CUS</V><V>CUS</V></F><F><N>LOCALITY_CENTRE</N><V></V><V></V></F><F><N>HIERARCHY_NAME</N><V>Euston, Thetford, St. Edmundsbury, Suffolk</V><V>Euston, Thetford, St. Edmundsbury, Suffolk</V></F></ADDRESS_MATCH>

			// And default GIS Information (see GIS stub)
			Assert.AreEqual("Euston: A1088 C634,Barnham Turn,Barnham Turn,A1088 C634,Euston, Thetford, St. Edmundsbury, Suffolk" ,location.Description, "Test failed");
			Assert.AreEqual(589561, location.GridReference.Easting, "Test failed");
			Assert.AreEqual(278888,  location.GridReference.Northing, "Test failed");

			Assert.AreEqual("E0051520", location.Locality, "Test failed"); 
			Assert.AreEqual(RequestPlaceType.Locality,  location.RequestPlaceType, "Test failed");
			Assert.AreEqual(SearchType.Locality,  location.SearchType, "Test failed");
			Assert.AreEqual(TDLocationStatus.Valid, location.Status, "Test failed");
			
			Assert.AreEqual(location.Toid.Length, 3);
			Assert.AreEqual(location.Toid[0], "4000000013194143");
			Assert.AreEqual(location.Toid[1], "4000000013194317");
			Assert.AreEqual(location.Toid[2], "4000000013194231");
		
		}


		[Test]
		[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
		public void TestSearchAttractions()
		{
			LocationSearch search = new LocationSearch();

			// 1. Search attraction with type Attraction
			LocationChoiceList choices = search.StartSearch("euston",
				SearchType.POI, false, 20, "sessionID", false);

			// Cf. GazopsWebTest\Xml\PlacenameMatch\euston.xml for expected result
			Assert.AreEqual(4, choices.Count, "Test failed");

			// Check for data in one of the choices (eg. index=3)
			// <PICK_LIST_ENTRY id="3"><IDENTITY>NX001441</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Thetford Coach Stop (Coach)</DESCRIPTION><GAZOPS_EASTING>587000</GAZOPS_EASTING><GAZOPS_NORTHING>283000</GAZOPS_NORTHING><HIERARCHY_NAME>Euston, Thetford, St. Edmundsbury, Suffolk</HIERARCHY_NAME><NATIONAL_GAZETTEER_ID>E0051520</NATIONAL_GAZETTEER_ID><DISTRICT_ID>189</DISTRICT_ID><ADMINAREA_ID>101</ADMINAREA_ID><EXCHANGE_POINT_ID>NX001441</EXCHANGE_POINT_ID><EXCHANGE_POINT_TYPE>C</EXCHANGE_POINT_TYPE><EXCH_NAME>Thetford Coach Stop</EXCH_NAME></PICK_LIST_ENTRY>

			LocationChoice choice = (LocationChoice)choices[3];

			Assert.AreEqual(587000, choice.OSGridReference.Easting, "Test failed");
			Assert.AreEqual(283000, choice.OSGridReference.Northing, "Test failed");
			Assert.AreEqual("Thetford Coach Stop (Coach)", choice.Description, "Test failed");
			Assert.AreEqual(100, choice.Score, "Test failed");
			

			//2. Test we cannot drill down
			bool thrown = false;
			try
			{
				search.DrillDown(search.CurrentLevel,choice);
			}
			catch (TDException)
			{
				thrown = true;
			}

			Assert.IsTrue(thrown == true);

			// Test Get location details
			TDLocation location = new TDLocation();
			search.GetLocationDetails(ref location, choice);

			Assert.AreEqual(location.Toid.Length, 3);
			Assert.AreEqual(location.Toid[0], "4000000013194143");
			Assert.AreEqual(location.Toid[1], "4000000013194317");
			Assert.AreEqual(location.Toid[2], "4000000013194231");
		
			Assert.AreEqual(location.NaPTANs.Length, 0);

			// Check other location details
			Assert.AreEqual(choice.Description, location.Description, "Test Failed");
			Assert.AreEqual("E0001659", location.Locality, "Test Failed"); // E0001659 is returned from the More Frequent value in Stops.NatgazId[]
			
			Assert.AreEqual(choice.OSGridReference.Easting, location.GridReference.Easting, "Test Failed");
			Assert.AreEqual(choice.OSGridReference.Northing, location.GridReference.Northing, "Test Failed");
			Assert.AreEqual(RequestPlaceType.Coordinate, location.RequestPlaceType, "Test Failed");
			Assert.AreEqual(SearchType.POI, location.SearchType, "Test Failed");
			Assert.AreEqual(TDLocationStatus.Valid, location.Status, "Test Failed");


		}

		/// <summary>
		/// The data file used in this test to support the mock GazopsWeb service is:
		/// \DrillDownAddressQuery\Euston.xml
		/// Note that originally the test used the following data file:
		/// \PlaceNameMatch\Euston.xml
		/// However, the Major Stations gazetteer was changed to a use a different
		/// drill down query which results in using a different data file - 
		/// the expected results have been changed accordingly. Although this data
		/// file does not include main stations, it still provides a valid 
		/// mechanism to perform the test.
		/// </summary>
		/// </summary>
		[Test]
		[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
		public void TestSearchMajorStations()
		{
			LocationSearch search = new LocationSearch();

			LocationChoiceList choices = search.StartSearch("euston",
				SearchType.MainStationAirport, false, 20, "sessionID", false);

			// Check choice count
			Assert.AreEqual(12, choices.Count, "Test Failed");

			
			// Get 4th location choice
			LocationChoice choice = (LocationChoice)choices[3];
			
			// Note that the spaces in criterias and value are retained!!!
			Assert.AreEqual(" LOCALITY", choice.PicklistCriteria, "Test Failed");
			Assert.AreEqual(" LOCALITY:E0043105", choice.PicklistValue, "Test Failed");
			Assert.AreEqual(96.6666666666667, choice.Score, "Test Failed");
			Assert.AreEqual("Easton, West Berkshire", choice.Description, "Test Failed");
			Assert.AreEqual(true, choice.HasChilden, "Test Failed");
			
		}

		[Test]
		[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
		public void TestSearchAllStations()
		{
			LocationSearch search = new LocationSearch();

			// 1. Search attraction with type Attraction
			LocationChoiceList choices = search.StartSearch("euston",
				SearchType.AllStationStops, false, 20, "sessionID", false);

			// Cf. GazopsWebTest\Xml\PlacenameMatch\euston.xml for expected result
			Assert.AreEqual(4, choices.Count, "Test failed");

			// Check for data in one of the choices (eg. index=3)
			// <PICK_LIST_ENTRY id="3"><IDENTITY>NX001441</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Thetford Coach Stop (Coach)</DESCRIPTION><GAZOPS_EASTING>587000</GAZOPS_EASTING><GAZOPS_NORTHING>283000</GAZOPS_NORTHING><HIERARCHY_NAME>Euston, Thetford, St. Edmundsbury, Suffolk</HIERARCHY_NAME><NATIONAL_GAZETTEER_ID>E0051520</NATIONAL_GAZETTEER_ID><DISTRICT_ID>189</DISTRICT_ID><ADMINAREA_ID>101</ADMINAREA_ID><EXCHANGE_POINT_ID>NX001441</EXCHANGE_POINT_ID><EXCHANGE_POINT_TYPE>C</EXCHANGE_POINT_TYPE><EXCH_NAME>Thetford Coach Stop</EXCH_NAME></PICK_LIST_ENTRY>

			LocationChoice choice = (LocationChoice)choices[3];

			Assert.AreEqual(587000, choice.OSGridReference.Easting, "Test failed");
			Assert.AreEqual(283000, choice.OSGridReference.Northing, "Test failed");
			Assert.AreEqual("Thetford Coach Stop (Coach)", choice.Description, "Test failed");
			Assert.AreEqual(100, choice.Score, "Test failed");
			Assert.AreEqual("C", choice.ExchangePointType, "Test failed");
			Assert.AreEqual("NX001441", choice.Naptan, "Test failed");
			Assert.AreEqual("E0051520", choice.Locality, "Test failed");
			

			//2. Test we cannot drill down
			bool thrown = false;
			try
			{
				search.DrillDown(search.CurrentLevel,choice);
			}
			catch (TDException)
			{
				thrown = true;
			}

			Assert.IsTrue(thrown == true);

			// Test Get location details
			TDLocation location = new TDLocation();
			search.GetLocationDetails(ref location, choice);

			Assert.AreEqual(location.Toid.Length, 3);
			Assert.AreEqual(location.Toid[0], "4000000013194143");
			Assert.AreEqual(location.Toid[1], "4000000013194317");
			Assert.AreEqual(location.Toid[2], "4000000013194231");
		
			Assert.AreEqual(location.NaPTANs.Length, 3);
			Assert.AreEqual(location.NaPTANs[0].Naptan, "0600CR145");
			Assert.AreEqual(location.NaPTANs[1].Naptan, "0600CR145A");
			Assert.AreEqual(location.NaPTANs[2].Naptan, "0600CR145B");
			
			// Check other location details
			Assert.AreEqual(choice.Description, location.Description, "Test Failed");
			Assert.AreEqual("E0051520", location.Locality, "Test Failed"); 
			
			Assert.AreEqual(choice.OSGridReference.Easting, location.GridReference.Easting, "Test Failed");
			Assert.AreEqual(choice.OSGridReference.Northing, location.GridReference.Northing, "Test Failed");
			Assert.AreEqual(RequestPlaceType.NaPTAN, location.RequestPlaceType, "Test Failed");
			Assert.AreEqual(SearchType.AllStationStops, location.SearchType, "Test Failed");
			Assert.AreEqual(TDLocationStatus.Valid, location.Status, "Test Failed");

		}

		[Test]
		public void TestFindStationsWithoutDirectFlight()
		{
			TDLocation location = new TDLocation();
			location.Description = "Home London";
			location.GridReference = new OSGridReference(524250, 186750);
			
			int naptanCount = 0;
				
			naptanCount = LocationSearch.FindStations(ref location, new StationType[] {StationType.Coach, StationType.Rail});
			Assert.AreEqual(3, naptanCount);
	
			naptanCount = LocationSearch.FindStations(ref location, new StationType[] {StationType.Coach});
			Assert.AreEqual(2, naptanCount);
	
			naptanCount = LocationSearch.FindStations(ref location, new StationType[] {StationType.Rail});
			Assert.AreEqual(1, naptanCount);
			Assert.AreEqual(location.NaPTANs[0].Naptan, "9100CRKLWD");	
		}


		[Test]
		[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
		public void TestFindStationsInRadius()
		{
			TDLocation location = new TDLocation();

			// search airports only.
			location.Description = "Home London";
			location.GridReference = new OSGridReference(524250, 186750);
			LocationSearch search = new LocationSearch();

			search.DisableGisQuery();
			search.StartSearch("nw118tj", SearchType.AddressPostCode, false, 1500, "", false);
			
			int radius = 50000;
			LocationSearch.FindStationsInRadius(location, radius, StationType.Airport, 10);
			
			foreach (TDNaptan naptan in location.NaPTANs)
			{
				Assert.IsTrue(naptan.Name.Length != 0, string.Format("The naptan with id [{0}] doesn't have any name", naptan.Naptan));
				Assert.IsTrue(naptan.StationType == StationType.Airport, string.Format("The naptan with description [{0}] is not an airport.", naptan.Name));
				Assert.IsTrue(naptan.GridReference.DistanceFrom(location.GridReference) <= radius, string.Format("The naptan with description [{0}] is further to the location than the current radius.", naptan.Name));
			}

			// search rail stations only.
			location.Description = "Home London";
			location.GridReference = new OSGridReference(524250, 186750);
			
			radius = 5000;
			LocationSearch.FindStationsInRadius(location, radius, StationType.Rail, 10);
			
			foreach (TDNaptan naptan in location.NaPTANs)
			{
				Assert.IsTrue(naptan.Name.Length != 0, string.Format("The naptan with id [{0}] doesn't have any name", naptan.Naptan));
				Assert.IsTrue(naptan.StationType == StationType.Rail, string.Format("The naptan with description [{0}] is not an rail station.", naptan.Name));
				Assert.IsTrue(naptan.GridReference.DistanceFrom(location.GridReference) <= radius, string.Format("The naptan with description [{0}] is further to the location than the current radius.", naptan.Name) );
			}

			// search coach station only.
			location.Description = "Home London";
			location.GridReference = new OSGridReference(524250, 186750);
			
			radius = 6000;
			LocationSearch.FindStationsInRadius(location, radius, StationType.Coach, 10);
			
			foreach (TDNaptan naptan in location.NaPTANs)
			{
				Assert.IsTrue(naptan.Name.Length != 0, string.Format("The naptan with id [{0}] doesn't have any name", naptan.Naptan));
				Assert.IsTrue(naptan.StationType == StationType.Coach, string.Format("The naptan with description [{0}] is not an coach station.", naptan.Name));
				Assert.IsTrue(naptan.GridReference.DistanceFrom(location.GridReference) <= radius, string.Format("The naptan with description [{0}] is further to the location than the current radius.", naptan.Name));
			}

			// search airports and rail stations.
			location = new TDLocation();
			location.Description = "Home London";
			location.GridReference = new OSGridReference(524250, 186750);

			radius = 300000;
			LocationSearch.FindStationsInRadius(location, radius, StationType.Rail, 10);

			foreach (TDNaptan naptan in location.NaPTANs)
			{
				Assert.IsTrue(naptan.Name.Length != 0, string.Format("The naptan with id [{0}] doesn't have any name", naptan.Naptan));
				Assert.IsTrue(naptan.StationType == StationType.Airport || naptan.StationType == StationType.Rail, string.Format("The naptan with description [{0}] is not an airport or a rail station.", naptan.Name));
				Assert.IsTrue(naptan.GridReference.DistanceFrom(location.GridReference) <= radius, string.Format("The naptan with description [{0}] is further to the location than the current radius.", naptan.Name));
			}

			// search airports, rail and coach stations.
			location = new TDLocation();
			location.Description = "Home London";
			location.GridReference = new OSGridReference(524250, 186750);

			LocationSearch.FindStationsInRadius(location, radius, StationType.Coach, 10);
			
			foreach (TDNaptan naptan in location.NaPTANs)
			{
				Assert.IsTrue(naptan.Name.Length != 0, string.Format("The naptan with id [{0}] doesn't have any name", naptan.Naptan));
				Assert.IsTrue(naptan.StationType == StationType.Airport || naptan.StationType == StationType.Rail || naptan.StationType == StationType.Coach, string.Format("The naptan with description [{0}] is not an airport or a rail station or a coach station.", naptan.Name));
				Assert.IsTrue(naptan.GridReference.DistanceFrom(location.GridReference) <= radius, string.Format("The naptan with description [{0}] is further to the location than the current radius.", naptan.Name));
			}
		}


		
		/// <remarks>
		///
		/// Manual setup:
		/// 1) Build the GazopsWeb web service stub: (This will be used instead of the Gazetteer Web service provided on the development server: http://tri-tdp-iis/GazopsWeb/GazopsWeb.asmx
		/// - create a blank solution (called GazopsWeb) in \TDPortal\DEL5\TransportDirect\stubs
		/// - get the latest version of the project TDPortal\DEL5\TransportDirect\Stubs\GazopsWebTest
		/// - add the GazopsWebTest project to the solution created
		/// - build the solution
		/// - enter the following URL into the browser and verify that web methods are displayed:
		/// http://localhost/GazopsWebTest/GazopsWeb.asmx
		/// 
		/// 2) Check that the value of the property with key name locationservice.gazopsweburl
		/// is: http://localhost/GazopsWebTest/GazopsWeb.asmx
		/// 
		/// 3) Ensure that the test XML data files reside in the following location: 
		/// C:\Inetpub\wwwroot\GazopsWebTest\Xml\
		/// (These data files are used by the mock GazopsWeb service to provide output data. ie the data files contain mock output data.)
		/// Within each xml file directory, the file called lookup.xml is used to map requests to the appropriate xml data file that should be used to service that request.
		/// 
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}
		
	}
}
