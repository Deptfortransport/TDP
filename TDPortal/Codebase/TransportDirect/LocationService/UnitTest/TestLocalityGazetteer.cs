// *********************************************** 
// NAME                 : TestLocalityGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 03/09/2003 
// DESCRIPTION  : Test for LocalityGazetteer class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestLocalityGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:44   mturner
//Initial revision.
//
//   Rev 1.14   Apr 20 2006 15:40:16   tmollart
//Updated methods so that calls to GetLocationDetails no longer pass in a Locaility Required argument.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.13   Mar 28 2006 15:52:40   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup/GazopsWebTest.asmx
//
//   Rev 1.12   Feb 07 2006 09:59:28   mtillett
//Fix unit tests broken by changes to the LocalityGazetteer.cs v1.26
//
//   Rev 1.11   Apr 13 2005 08:58:02   rscott
//Changes made to some test methods as a consequence of IR1978 enhancement to reject single word addresses.
//
//   Rev 1.10   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.9   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.8   Jan 19 2005 12:07:38   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.7   Jul 23 2004 16:31:30   RPhilpott
//NUnit refactoring -- use TestMockGisQuery, eliminating need for stub version of td.interactivemapping.dll.
//
//   Rev 1.6   Jul 09 2004 13:09:20   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.5   May 14 2004 16:22:24   passuied
//change for interface change of GetLocationDetails
//
//   Rev 1.4   Apr 01 2004 12:09:44   geaton
//Unit test refactoring exercise.
//
//   Rev 1.3   Dec 03 2003 12:21:36   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.2   Sep 25 2003 12:38:20   passuied
//Added property and allowed locality to get location details when the location has children
//
//   Rev 1.1   Sep 20 2003 16:59:56   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.0   Sep 05 2003 15:30:10   passuied
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
	/// Summary description for TestLocalityGazetteer.
	/// </summary>
	[TestFixture]
	[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
	public class TestLocalityGazetteer
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
		public void TestGetLocationDetails()
		{
			LocalityGazetteer gaz = new LocalityGazetteer("sessionID", false);

			LocationQueryResult queryResult =  gaz.FindLocation("euston", false);

			queryResult = gaz.DrillDown("euston", false, queryResult.PickListUsed,
				queryResult.QueryReference, (LocationChoice)queryResult.LocationChoiceList[0]);

			LocationChoice choice = (LocationChoice)queryResult.LocationChoiceList[0];
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
			
			// Check info after Drill3
			//<GAZOPS_DATA_REQUEST operation="ADDRESS_MATCH">
			// ...			<ADDRESS_MATCH>
			// ...			<ADDLINE>Euston: A1088 C634</ADDLINE><ADDLINE>Barnham Turn</ADDLINE><ADDLINE>Barnham Turn</ADDLINE><ADDLINE>A1088 C634</ADDLINE><ADDLINE>Euston, Thetford, St. Edmundsbury, Suffolk</ADDLINE></V></F>
			// ...			<N>NATIONAL_GAZETTEER_ID</N><V>E0051520</V><V>E0051520</V></F>
			
			// ...			<F><N>GAZOPS_EASTING</N><V>589561</V><V>589561</V></F><F><N>GAZOPS_NORTHING</N><V>278888</V><V>278888</V></F>
			// ...

			Assert.AreEqual("Euston: A1088 C634,Barnham Turn,Barnham Turn,A1088 C634,Euston, Thetford, St. Edmundsbury, Suffolk",location.Description);
			Assert.AreEqual("E0051520", location.Locality);
			Assert.AreEqual(RequestPlaceType.Locality, location.RequestPlaceType);
			Assert.AreEqual(SearchType.Locality, location.SearchType);
			Assert.AreEqual(TDLocationStatus.Valid, location.Status);

			Assert.AreEqual(589561, location.GridReference.Easting);
			Assert.AreEqual(278888, location.GridReference.Northing);

			Assert.AreEqual(location.Toid.Length, 3);
			Assert.AreEqual(location.Toid[0], "4000000013194143");
			Assert.AreEqual(location.Toid[1], "4000000013194317");
			Assert.AreEqual(location.Toid[2], "4000000013194231");
		
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
