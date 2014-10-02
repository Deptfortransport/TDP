// *********************************************** 
// NAME                 : TestAttractionsGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 03/09/2003 
// DESCRIPTION  : Test for AttractionsGazetteer class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestAttractionsGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:42   mturner
//Initial revision.
//
//   Rev 1.12   Apr 20 2006 15:39:52   tmollart
//Updated methods so that calls to GetLocationDetails no longer pass in a Locaility Required argument.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.11   Mar 28 2006 15:51:40   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup/GazopsWebTest.asmx
//
//   Rev 1.10   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.9   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.8   Jan 19 2005 12:07:24   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.7   Sep 11 2004 16:54:34   RPhilpott
//Updates for introduction of FindNearestLocality()
//
//   Rev 1.6   Jul 23 2004 16:31:30   RPhilpott
//NUnit refactoring -- use TestMockGisQuery, eliminating need for stub version of td.interactivemapping.dll.
//
//   Rev 1.5   Jul 09 2004 13:09:20   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.4   May 14 2004 16:22:22   passuied
//change for interface change of GetLocationDetails
//
//   Rev 1.3   Apr 01 2004 12:09:40   geaton
//Unit test refactoring exercise.
//
//   Rev 1.2   Dec 03 2003 12:21:34   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.1   Sep 20 2003 16:59:56   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.0   Sep 05 2003 15:30:08   passuied
//Initial Revision

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Test for AttractionsGazetteer class
	/// </summary>
	[TestFixture]
	[Ignore("NEWKIRK: Manual setup of GazopsWebTest required")]
	public class TestAttractionsGazetteer
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

		[Test]
		public void TestFindLocation()
		{
			AttractionsGazetteer gaz = new AttractionsGazetteer("sessionID", false);

			LocationQueryResult queryResult = gaz.FindLocation("euston", false);


			// Check choice count
			Assert.AreEqual(4, queryResult.LocationChoiceList.Count, "Test Failed");

			// Check info in queryResult e.g. check choice 2 (index = 1)
			// <PICK_LIST_ENTRY id="1"><IDENTITY>NX000258</IDENTITY><SCORE>100</SCORE><DESCRIPTION>Bury St Edmunds Coach Stop (Coach)</DESCRIPTION><GAZOPS_EASTING>585000</GAZOPS_EASTING><GAZOPS_NORTHING>264000</GAZOPS_NORTHING><HIERARCHY_NAME>Euston, Thetford, St. Edmundsbury, Suffolk</HIERARCHY_NAME><NATIONAL_GAZETTEER_ID>E0051520</NATIONAL_GAZETTEER_ID><DISTRICT_ID>189</DISTRICT_ID><ADMINAREA_ID>101</ADMINAREA_ID><EXCHANGE_POINT_ID>NX000258</EXCHANGE_POINT_ID><EXCHANGE_POINT_TYPE>C</EXCHANGE_POINT_TYPE><EXCH_NAME>Bury St Edmunds Coach Stop</EXCH_NAME></PICK_LIST_ENTRY>
			
			LocationChoice choice = (LocationChoice)queryResult.LocationChoiceList[1];

			Assert.AreEqual(585000, choice.OSGridReference.Easting, "Test Failed");

			Assert.AreEqual(264000, choice.OSGridReference.Northing, "Test Failed");
			Assert.AreEqual("Bury St Edmunds Coach Stop (Coach)", choice.Description, "Test Failed");
			Assert.AreEqual(100, choice.Score, "Test Failed");
			Assert.AreEqual(false, choice.HasChilden, "Test Failed");

		}

		[Test]
		public void TestDrillDown()
		{
			bool thrown = false;

			AllStationsGazetteer gaz = new AllStationsGazetteer("sessionID", false);

			try
			{
				gaz.DrillDown("euston", false, string.Empty, string.Empty, null);
			}
			catch (TDException)
			{
				thrown = true;
			}

			Assert.IsTrue(thrown == true);

		}

		[Test]
		public void TestGetLocationDetails()
		{
			AttractionsGazetteer gaz = new AttractionsGazetteer("sessionID", false);

			LocationQueryResult queryResult = gaz.FindLocation("euston", false);

			LocationChoice choice = (LocationChoice) queryResult.LocationChoiceList[0];
			TDLocation location = new TDLocation();
			gaz.GetLocationDetails(
				ref location,
				"euston", 
				false,
				queryResult.PickListUsed,
				queryResult.QueryReference,
				choice, 
				20,
				false);

			
			Assert.AreEqual("E0001659",location.Locality);
			Assert.AreEqual(RequestPlaceType.Coordinate,location.RequestPlaceType);
			Assert.AreEqual(SearchType.POI,location.SearchType);
			Assert.AreEqual(TDLocationStatus.Valid,location.Status);

			Assert.AreEqual(choice.OSGridReference.Easting, location.GridReference.Easting);
			Assert.AreEqual(choice.OSGridReference.Northing, location.GridReference.Northing);

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
