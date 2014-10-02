// *********************************************** 
// NAME                 : TestAddressPostcodeGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 03/09/2003 
// DESCRIPTION  : Test for AddressPostcodeGazetteer class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestAddressPostcodeGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:42   mturner
//Initial revision.
//
//   Rev 1.14   Apr 20 2006 15:39:28   tmollart
//Updated methods so that calls to GetLocationDetails no longer pass in a Locaility Required argument.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.13   Mar 28 2006 15:51:14   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup
//
//   Rev 1.12   Apr 13 2005 08:56:44   rscott
//New test method added TestFindLocationUsingSingleWordAddress( ) for IR1978 changes.
//
//   Rev 1.11   Mar 23 2005 11:55:26   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.10   Feb 07 2005 14:03:28   RScott
//Assertion changed to Assert
//
//   Rev 1.9   Sep 11 2004 16:54:58   RPhilpott
//Updates for introduction of FindNearestLocality().
//
//   Rev 1.8   Jul 23 2004 16:31:26   RPhilpott
//NUnit refactoring -- use TestMockGisQuery, eliminating need for stub version of td.interactivemapping.dll.
//
//   Rev 1.7   Jul 09 2004 13:09:24   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.6   May 14 2004 16:22:26   passuied
//change for interface change of GetLocationDetails
//
//   Rev 1.5   Apr 27 2004 13:39:50   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.4   Apr 01 2004 12:09:50   geaton
//Unit test refactoring exercise.
//
//   Rev 1.3   Mar 30 2004 19:17:42   geaton
//Refactored and fixed unit tests according to latest nunit guidelines.
//
//   Rev 1.2   Dec 03 2003 12:21:40   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.1   Sep 20 2003 17:00:02   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.0   Sep 05 2003 15:30:04   passuied
//Initial Revision

using System;
using NUnit.Framework;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;


namespace TransportDirect.UserPortal.LocationService
{

	/// <summary>
	/// Summary description for TestTDGazetteer.
	/// </summary>
	[TestFixture]
	[Ignore("NEWKIRK: Manual setup required.")]
	public class TestAddressPostcodeGazetteer
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
		/// Test that the address/postcode gazetteer returns the expected 
		/// location choices when using an ADDRESS search term.
		/// The data file used in this test to support the mock GazopsWeb service is:
		/// \DrillDownAddressQuery\3bethelstreet.xml
		/// </summary>
		[Test]
		public void TestFindLocationUsingAddress()
		{
			// This test address should return a single location choice.
			string testAddress = "3 bethel street";
			string dummySessionId = "sessionID";

			AddressPostcodeGazetteer gaz = 
				new AddressPostcodeGazetteer(dummySessionId, false);
			LocationQueryResult queryResult = gaz.FindLocation(testAddress, true);

			// Check only 1 choice was returned.
			Assert.AreEqual(1, queryResult.LocationChoiceList.Count, "Incorrect number of location choices returned.");

			// Check for that location choice returned includes
			// following data:
			// ...
			//  <ADDLINE>3 Bethel Street</ADDLINE> 
			//  <ADDLINE>Neath</ADDLINE> 
			//  <ADDLINE>West Glamorgan</ADDLINE> 
			//  <ADDLINE>SA11 2HQ</ADDLINE> 
			//  <N>GAZOPS_EASTING</N> 
			//  <V>274007</V> 
			//  <V>274007</V> 
			//  </F>
			// ...
			//- <F>
			//  <N>GAZOPS_NORTHING</N> 
			//  <V>194007</V> 
			//  <V>194007</V> 
			//  </F>
			//- ...
			
			LocationChoice choice = (LocationChoice)queryResult.LocationChoiceList[0];
			Assert.AreEqual("3 Bethel Street,Neath,West Glamorgan,SA11 2HQ", choice.Description, "Incorrect description in location choice.");
			Assert.AreEqual(274007, choice.OSGridReference.Easting, "Incorrect easting in location choice.");
			Assert.AreEqual(194007, choice.OSGridReference.Northing, "Incorrect northing in location choice.");
		}

		/// <summary>
		/// Test that the address/postcode is not called and the string is rejected
		/// s it is a single word address
		/// </summary>
		[Test]
		public void TestFindLocationUsingSingleWordAddress()
		{
			// This test address should return a single location choice.
			string testAddress = "bethel";
			string dummySessionId = "sessionID";

			AddressPostcodeGazetteer gaz = 
				new AddressPostcodeGazetteer(dummySessionId, false);
			LocationQueryResult queryResult = gaz.FindLocation(testAddress, true);

			// Check only 1 choice was returned.
			Assert.AreEqual(true, queryResult.IsSingleWordAddress, "Single Word Address not captured");
		}

		/// <summary>
		/// Test that the address/postcode gazetteer returns the expected 
		/// location choices when using a POSTCODE search term.
		/// The data file used in this test to support the mock GazopsWeb service is:
		/// \PostCodeMatch\sa112nf.xml
		/// </summary>
		[Test]
		public void TestFindLocationUsingPostcode()
		{
			// This test postcode should return a single location choice.
			string testPostcode = "sa11 2nf";
			string dummySessionId = "sessionID";

			AddressPostcodeGazetteer gaz = 
				new AddressPostcodeGazetteer(dummySessionId, false);
			LocationQueryResult queryResult = gaz.FindLocation(testPostcode, true);

			Assert.AreEqual(1, queryResult.LocationChoiceList.Count,"Incorrect number of location choices returned.");
			
			// Get the (first and only) location choice returned in query result.
			LocationChoice choice = (LocationChoice)queryResult.LocationChoiceList[0];

			// Check that easting and northing of the query result.
			Assert.AreEqual(195733, choice.OSGridReference.Easting, "Incorrect easting in location choice.");
			Assert.AreEqual(139059, choice.OSGridReference.Northing, "Incorrect northing in location choice.");
		}

		/// <summary>
		/// Test that the address/postcode gazetteer returns the expected 
		/// location choices when using a PARTIAL POSTCODE search term.
		/// The data file used in this test to support the mock GazopsWeb service is:
		/// \PostCodeMatch\sa11.xml
		/// </summary>
		[Test]
		public void TestFindLocationUsingPartPostcode()
		{
			// This test postcode should return a single location choice.
			string testPostcode = "sa11";
			string dummySessionId = "sessionID";

			AddressPostcodeGazetteer gaz = 
				new AddressPostcodeGazetteer(dummySessionId, false);
			LocationQueryResult queryResult = gaz.FindLocation(testPostcode, true);

			Assert.AreEqual(1, queryResult.LocationChoiceList.Count, "Incorrect number of location choices returned.");
			
			// Get the (first and only) location choice returned in query result.
			LocationChoice choice = (LocationChoice)queryResult.LocationChoiceList[0];

			// Check the easting, northing and coordinates of the query result.
			Assert.AreEqual(527535, choice.OSGridReference.Easting, "Incorrect easting in location choice.");
			Assert.AreEqual(175587, choice.OSGridReference.Northing, "Incorrect northing in location choice.");
			Assert.AreEqual(528763, choice.PartPostcodeMaxX, "Incorrect Max X coord in location choice.");
			Assert.AreEqual(177369, choice.PartPostcodeMaxY, "Incorrect Max Y coord in location choice.");
			Assert.AreEqual(526308, choice.PartPostcodeMinX, "Incorrect Min X coord in location choice.");
			Assert.AreEqual(173805, choice.PartPostcodeMinY, "Incorrect Min Y coord in location choice.");
		}

		/// <summary>
		/// Test that the address/postcode gazetteer returns the expected 
		/// location choices when using a locality search term.
		/// The data file used in this test to support the mock GazopsWeb service is:
		/// \DrillDownAddressQuery\Euston.xml
		/// RS - 13/04/2005 Changed this test to look for "euston," 
		/// as "euston is rejected as a single word address.
		/// </summary>
		[Test]
		public void TestFindLocationUsingLocality()
		{		
			// This test locality should return a number of location choices.
			string testLocality = "euston,";
			
			string dummySessionId = "sessionID";

			AddressPostcodeGazetteer gaz = 
				new AddressPostcodeGazetteer(dummySessionId, false);

			LocationQueryResult queryResult =  gaz.FindLocation(testLocality, false);



//			Assert.AreEqual(true, queryResult.IsSingleWordAddress, "Is not a single word address");

			// Check that the correct query reference has been returned (this applies to all location choices in the returned query result)
			// (The query reference identifies how the result set has been cached.)
			Assert.AreEqual("GZ38", queryResult.QueryReference, "Query reference incorrect.");
			
			// Check that correct number of location choices have been returned.
			Assert.AreEqual(12, queryResult.LocationChoiceList.Count, "Incorrect number of location choices returned.");

			//Check that the 4th element in the location choices includes the following data:
			//	<PICK_LIST_ENTRY id="3">
			//  <MATCH_COUNT>1</MATCH_COUNT> 
			//  <CRITERIA_FIELD> LOCALITY</CRITERIA_FIELD> 
			//  <IDENTITY> LOCALITY:E0043105</IDENTITY> 
			//  <SCORE>96.6666666666667</SCORE> 
			//  <DESCRIPTION>Easton, West Berkshire</DESCRIPTION> 
			//  <FINAL>False</FINAL> 
			//  </PICK_LIST_ENTRY>

			
			// Get 4th location choice
			LocationChoice choice = (LocationChoice)queryResult.LocationChoiceList[3];
			
			// Note that the spaces in criterias and value are retained!!!
			Assert.AreEqual(" LOCALITY", choice.PicklistCriteria, "Test Failed");
			Assert.AreEqual(" LOCALITY:E0043105", choice.PicklistValue, "Test Failed");
			Assert.AreEqual(96.6666666666667, choice.Score, "Test Failed");
			Assert.AreEqual("Easton, West Berkshire", choice.Description, "Test Failed");
			Assert.AreEqual(true, choice.HasChilden, "Test Failed");
		}


		/// <summary>
		/// Tests the drill down functionality of the gazetteer using a
		/// query result based on an initial query using a locality.
		/// The following data files are used in this test to support the mock GazopsWeb service is:
		/// \DrillDownAddressQuery\Euston.xml (for initial call to find location)
		/// \DrillDownAddressQuery\euston_drill2.xml (for drilldown call)
		/// </summary>
		[Test]
		public void TestDrillDownUsingLocality()
		{
			string testLocality = "euston,";

			AddressPostcodeGazetteer gaz = new AddressPostcodeGazetteer("sessionID", false);

			// This request will result in a picklist of more than one location choice.
			LocationQueryResult queryResult =  gaz.FindLocation(testLocality, false);

			string testDrillDownText = "euston";

			// Drill down based on the first location choice returned above
			LocationQueryResult drilledQueryResult =
				gaz.DrillDown(testDrillDownText,
							  false,
							  queryResult.PickListUsed,
							  queryResult.QueryReference,
							  (LocationChoice)queryResult.LocationChoiceList[0]);


			

			// Check that drilled query result includes two location choices
			Assert.AreEqual(2, drilledQueryResult.LocationChoiceList.Count, "Incorrect location choices returned in drilled results.");

			// Check that the drilled query result includes results that follow this XML:
			//	<GAZOPS_DATA_REQUEST operation="ADDRESS_MATCH"><OPERATION_RESULT>success</OPERATION_RESULT>
			//	<PICK_LIST>
			//	<CRITERIA><FIELD>LOCALITY</FIELD><VALUE>LOCALITY:E0051520</VALUE></CRITERIA><PICK_LIST_TYPE /><FIELD_DEFINTION><NAME>% Match</NAME><FIELD_WIDTH>10</FIELD_WIDTH></FIELD_DEFINTION><FIELD_DEFINTION><NAME>Value</NAME><FIELD_WIDTH>500</FIELD_WIDTH></FIELD_DEFINTION>
			//	<PICK_LIST_ENTRY id="0"><MATCH_COUNT>1</MATCH_COUNT><CRITERIA_FIELD>STOP</CRITERIA_FIELD><IDENTITY>STOP:390050032</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Euston: A1088 C634, Barnham Turn, Barnham Turn, A1088 C634</DESCRIPTION><FINAL>True</FINAL></PICK_LIST_ENTRY>
			//	<PICK_LIST_ENTRY id="1"><MATCH_COUNT>1</MATCH_COUNT><CRITERIA_FIELD>STOP</CRITERIA_FIELD><IDENTITY>STOP:390050033</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Euston: A1088 C634, opp Barnham Turn, opp Barnham Turn, A1088 C634</DESCRIPTION><FINAL>True</FINAL></PICK_LIST_ENTRY>
			//  </PICK_LIST>
			//  <CACHE_ID>GZ43</CACHE_ID>
			//  </GAZOPS_DATA_REQUEST>

			// Check against the second location choice returned
			LocationChoice choice = (LocationChoice)drilledQueryResult.LocationChoiceList[1]; // check 2nd choice in list
			Assert.AreEqual("STOP" ,choice.PicklistCriteria, "Incorrect picklist criteria.");
			Assert.AreEqual("STOP:390050033", choice.PicklistValue, "Incorrect picklist value.");
			Assert.AreEqual("Euston: A1088 C634, opp Barnham Turn, opp Barnham Turn, A1088 C634", choice.Description, "Incorrect choice description.");
			Assert.AreEqual(100, choice.Score, "Incorrect choice score.");
			Assert.AreEqual(false, choice.HasChilden, "Incorrect choice children indicator.");

			// Attempt another drilldown against the first location choice returned above,
			// and using same search text as previous drilldown
			bool thrown = false;
			try
			{
				gaz.DrillDown(testDrillDownText,
							  false,
							  drilledQueryResult.PickListUsed,
							  drilledQueryResult.QueryReference,
							  (LocationChoice)drilledQueryResult.LocationChoiceList[0]);

			}
			catch (TDException)
			{
				// Correctly throws exception (because the location choice passed does not have children)
				thrown = true;
			}
 
			Assert.AreEqual(true, thrown, "Exception was not thrown on attempt to drill down on a location choice that has no children.");
			
		}


		/// <summary>
		/// Tests that the gazetteer is able to return location details for a
		/// location choice returned through a call using a locality.
		/// The following data files are used in this test to support the mock GazopsWeb service is:
		/// \DrillDownAddressQuery\Euston.xml (for initial call to find location)
		/// \DrillDownAddressQuery\euston_drill2.xml (for drilldown call)
		/// \DrillDownAddressQuery\euston_drill3.xml (for drilldown call resulting from call to getlocationdetails)
		/// Also relies on data defined in the mock class Map defined in the mock dll td.interactivemapping.dll
		/// </summary>
		[Test]
		public void TestGetLocationDetails()
		{
			// This test locality should return a number of location choices.
			string testLocality = "euston,";
			
			string dummySessionId = "sessionID";

			AddressPostcodeGazetteer gaz = 
				new AddressPostcodeGazetteer(dummySessionId, false);

			LocationQueryResult queryResult =  gaz.FindLocation(testLocality, false);

			TDLocation location= new TDLocation();
			
			// Check exception is thrown if attempt to get location details
			// for a location choice that has children.
			bool thrown = false;
			try
			{
				// Attempt to get location details for first location choice in list.
				gaz.GetLocationDetails(
					ref location,
					"euston",
					false,
					queryResult.PickListUsed,
					queryResult.QueryReference,
					(LocationChoice)queryResult.LocationChoiceList[0],
					20,
					false);
			}
			catch (TDException)
			{
				thrown = true;
			}

			Assert.IsTrue(thrown == true, "Exception was not thrown when attempting to get location details for a location choice that has children");

			string testDrillDownText = "euston";

			// Drill down based on the first location choice returned above
			LocationQueryResult drilledQueryResult =
				gaz.DrillDown(testDrillDownText,
							  false,
							  queryResult.PickListUsed,
							  queryResult.QueryReference,
							  (LocationChoice)queryResult.LocationChoiceList[0]);


			// Get location details for first choice in drilled down results.
			LocationChoice choice = (LocationChoice)drilledQueryResult.LocationChoiceList[0];
			
			try
			{
				// The following call will result in a final drill down since the location choice passed does not include a easting or northing.
				gaz.GetLocationDetails( ref location,
					"euston", 
					false, 
					queryResult.PickListUsed, 
					queryResult.QueryReference,
					choice, 
					20,
					false);
			}
			catch (TDException e)
			{
				Assert.IsTrue(false, String.Format("Failed to get location details: {0}", e.Message)); 
			}

			Assert.AreEqual("Euston: A1088 C634,Barnham Turn,Barnham Turn,A1088 C634,Euston, Thetford, St. Edmundsbury, Suffolk",location.Description, "Expected location details incorrect.");

			Assert.AreEqual("E0001659",location.Locality, "Expected locality incorrect");
			
			Assert.AreEqual(RequestPlaceType.Coordinate,location.RequestPlaceType, "Expeceted request place type incorrect.");
			Assert.AreEqual(SearchType.AddressPostCode,location.SearchType, "Expected search type incorrect");
			Assert.AreEqual(TDLocationStatus.Valid,location.Status, "Expected location status incorrect");
			Assert.AreEqual(58956, location.GridReference.Easting, "Expected easting incorrect");
			Assert.AreEqual(27888, location.GridReference.Northing, "Expected northing incorrect");

			Assert.AreEqual(location.Toid.Length, 3);
			Assert.AreEqual(location.Toid[0], "4000000013194143");
			Assert.AreEqual(location.Toid[1], "4000000013194317");
			Assert.AreEqual(location.Toid[2], "4000000013194231");
		
			Assert.AreEqual(location.NaPTANs.Length, 0);

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
