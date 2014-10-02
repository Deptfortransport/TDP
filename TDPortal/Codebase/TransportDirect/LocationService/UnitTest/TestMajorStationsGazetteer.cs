// *********************************************** 
// NAME                 : TestMajorStationsGazetteer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 03/09/2003 
// DESCRIPTION  : Test for MajorStationsGazetteer class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestMajorStationsGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:46   mturner
//Initial revision.
//
//   Rev 1.9   Mar 28 2006 15:54:52   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup/GazopsWebTest.asmx
//
//   Rev 1.8   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.7   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.6   Jan 19 2005 12:07:48   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.5   Jul 09 2004 13:09:22   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.4   Apr 01 2004 12:09:48   geaton
//Unit test refactoring exercise.
//
//   Rev 1.3   Mar 31 2004 20:25:28   geaton
//Added TestFixture attribute so that included in tests.
//
//   Rev 1.2   Dec 03 2003 12:21:38   passuied
//Changed GetLocationDetails interface to use reference
//
//   Rev 1.1   Sep 20 2003 16:59:58   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.0   Sep 05 2003 15:30:14   passuied
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
	/// Test for MajorStationsGazetteer class
	/// </summary>
	[TestFixture]
	public class TestMajorStationsGazetteer
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
		/// Test that the Major Stations gazetteer returns the expected 
		/// location choices when using a locality search term.
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
		[Test]
		[Ignore("NEWKIRK: Requires access to GazopsWebTest.asmx page")]
		public void TestFindLocation()
		{
			MajorStationsGazetteer gaz = new MajorStationsGazetteer("sessionID", false, StationType.Undetermined);

			LocationQueryResult queryResult = gaz.FindLocation("euston", false);


			// Check choice count
			Assert.AreEqual(12, queryResult.LocationChoiceList.Count, "Test Failed");

			
			// Get 4th location choice
			LocationChoice choice = (LocationChoice)queryResult.LocationChoiceList[3];
			
			// Note that the spaces in criterias and value are retained!!!
			Assert.AreEqual(" LOCALITY", choice.PicklistCriteria, "Test Failed");
			Assert.AreEqual(" LOCALITY:E0043105", choice.PicklistValue, "Test Failed");
			Assert.AreEqual(96.6666666666667, choice.Score, "Test Failed");
			Assert.AreEqual("Easton, West Berkshire", choice.Description, "Test Failed");
			Assert.AreEqual(true, choice.HasChilden, "Test Failed");


		}

		/// <remarks>
		/// Manual setup:
		/// 1) Build the GazopsWeb web service stub: (This will be used instead of the Gazetteer Web service provided on the development server: http://tri-tdp-iis/GazopsWeb/GazopsWeb.asmx
		/// - create a blank solution (called GazopsWeb) in \TDPortal\DEL5\TransportDirect\stubs
		/// - get the latest version of the project TDPortal\DEL5\TransportDirect\Stubs\GazopsWebTest
		/// - add the GazopsWebTest project to the solution created
		/// - build the solution
		/// - enter the following URL into the browser and verify that web methods are displayed:
		/// http://localhost/GazopsWebTest/GazopsWeb.asmx
		/// 
		/// 2) Build the Map class stub: (This will be used instead of the ESRI Map class.)
		/// - get the latest version of the solution TDPortal\DEL5\TransportDirect\stubs\td.interactivemapping\td.interactivemapping.sln
		/// - build the solution
		/// - Note that the mock Map class defines GIS Query data that is returned when clients call the mock GIS Query methods of the class.
		/// 
		/// 3) Change the value of the property with key name locationservice.gazopsweburl
		/// from: http://tri-tdp-iis/GazopsWeb/GazopsWeb.asmx
		/// to: http://localhost/GazopsWebTest/GazopsWeb.asmx
		/// 
		/// 4) Put the dll td.interactivemapping.dll built in step 2) into the directory:
		/// C:\TDPortal\DEL5\build\externals\
		/// 
		/// 5) Ensure that the test XML data files reside in the following location: (These data files are used by the mock GazopsWeb service to provide output data. ie the data files contain mock output data.)
		/// C:\Inetpub\wwwroot\GazopsWebTest\Xml\
		/// Within each xml file directory, the file called lookup.xml is used to map requests to the appropriate xml data file that should be used to service that request.
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}

	}
}
