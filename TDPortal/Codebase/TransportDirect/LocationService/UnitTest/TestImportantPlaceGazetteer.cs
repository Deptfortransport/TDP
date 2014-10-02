// *********************************************** 
// NAME                 : TestImportantPlaceGazetteer
// AUTHOR               : Jonathan George
// DATE CREATED         : 29/10/2004
// DESCRIPTION  : Unit test for the DatabaseGazetter class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestImportantPlaceGazetteer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:44   mturner
//Initial revision.
//
//   Rev 1.5   Apr 20 2006 15:40:06   tmollart
//Updated methods so that calls to GetLocationDetails no longer pass in a Locaility Required argument.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.4   Mar 30 2006 12:17:12   build
//Automatically merged from branch for stream0018
//
//   Rev 1.3.1.0   Feb 16 2006 16:33:42   mguney
//TestFindLocation changed because of the change in the data file.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.3   Mar 23 2005 11:57:18   jgeorge
//Updates for new UseForFareEnquiries field on TDNaptan
//
//   Rev 1.2   Feb 08 2005 15:50:04   RScott
//assertions changed to asserts
//
//   Rev 1.1   Jan 19 2005 12:07:34   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.0   Nov 03 2004 09:51:06   jgeorge
//Initial revision.
//
//   Rev 1.0   Nov 01 2004 15:47:04   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using NUnit.Framework;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for TestDatabaseGazetteer.
	/// </summary>
	[TestFixture]
	public class TestImportantPlaceGazetteer
	{
		#region Constructor, setup and teardown

		[TestFixtureSetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialisation());
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PlaceDataProvider, new TestPlaceDataProviderFactory());
		}

		[TestFixtureTearDown]
		public void ClearUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		#endregion

		#region Tests

		/// <summary>
		/// Tests that a call to find location returns all the entries in the provider
		/// </summary>
		[Test]
		public void TestFindLocation()
		{
			ITDGazetteer gazetteer = new ImportantPlaceGazetteer(string.Empty, false, PlaceType.City);

			LocationQueryResult result = gazetteer.FindLocation(string.Empty, false);
		
			// The LocationChoiceList should have two entries
			LocationChoiceList choiceList = result.LocationChoiceList;

			Assert.AreEqual(3, choiceList.Count,
				"The result.LocationChoiceList did not have the expected number of results");
		}
		
		/// <summary>
		/// Tests that location details can be retrieved correctly for a specific location 
		/// </summary>
		[Test]
		public void TestGetLocationDetails()
		{
			ITDGazetteer gazetteer = new ImportantPlaceGazetteer(string.Empty, false, PlaceType.City);

			LocationQueryResult result = gazetteer.FindLocation(string.Empty, false);

			TDLocation location = new TDLocation();

			gazetteer.GetLocationDetails(ref location, string.Empty, false, string.Empty, string.Empty, (LocationChoice)result.LocationChoiceList[0], 0, true);

			Assert.AreEqual("Coventry, West Midlands", location.Description,
				"Name of location 1 not as expected");
			Assert.AreEqual(2, location.NaPTANs.Length,
				"Number of naptans of location 1 not as expected");
			Assert.AreEqual("9100COVNTRY", location.NaPTANs[0].Naptan,
				"Location 1, Naptan 0, Naptan not as expected");
			Assert.AreEqual(433200, location.NaPTANs[0].GridReference.Easting,
				"Location 1, Naptan 0, OSGR.Easting not as expected");
			Assert.AreEqual(278200, location.NaPTANs[0].GridReference.Northing,
				"Location 1, Naptan 0, OSGR.Northing not as expected");
			Assert.IsTrue( location.NaPTANs[0].UseForFareEnquiries,
				"Location 1, Naptan 0, UseForFareEnquiries not as expected");

			Assert.AreEqual("9200BHX", location.NaPTANs[1].Naptan,
				"Location 1, Naptan 1, Naptan not as expected");
			Assert.AreEqual(417950, location.NaPTANs[1].GridReference.Easting,
				"Location 1, Naptan 1, OSGR.Easting not as expected");
			Assert.AreEqual(283950, location.NaPTANs[1].GridReference.Northing,
				"Location 1, Naptan 1, OSGR.Northing not as expected");
			Assert.IsFalse( location.NaPTANs[1].UseForFareEnquiries,
				"Location 1, Naptan 1, UseForFareEnquiries not as expected");

		}


		#endregion

	}

	public class TestPlaceDataProviderFactory : IServiceFactory
	{
		private IPlaceDataProvider current;

		/// <summary>
		/// Constructor
		/// </summary>
		public TestPlaceDataProviderFactory()
		{
			current = new PlaceDataProvider(new FilePlaceDataLoader(@".\TestLocationServicePlaceData.xml"));
		}
		
		/// <summary>
		/// Returns the current PlaceDataProvider object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}
	}

}

