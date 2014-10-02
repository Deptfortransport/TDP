// *********************************************** 
// NAME                 : TestTDGazetteerFactory.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 1/09/2003 
// DESCRIPTION  : Test for TDGazettee factory class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestTDGazetteerFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:50   mturner
//Initial revision.
//
//   Rev 1.9   Mar 23 2005 11:55:32   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.8   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.7   Jan 19 2005 12:08:14   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.6   Nov 03 2004 10:13:18   jgeorge
//Changed DatabaseGazetteer to ImportantPlaceGazetteer
//
//   Rev 1.5   Nov 01 2004 15:47:20   jgeorge
//Added code to create DatabaseGazetteer
//
//   Rev 1.4   Jul 23 2004 16:31:26   RPhilpott
//NUnit refactoring -- use TestMockGisQuery, eliminating need for stub version of td.interactivemapping.dll.
//
//   Rev 1.3   Jul 22 2004 11:02:22   RPhilpott
//Belated update following IR1089 changes to GazetteerFactory.
//
//   Rev 1.2   Jul 09 2004 13:09:22   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.1   Apr 27 2004 13:41:12   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.0   Sep 05 2003 15:30:18   passuied
//Initial Revision


using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Test for TDGazetteer factory class
	/// </summary>
	[TestFixture]
	public class TestTDGazetteerFactory
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
		public void TestGazetteer()
		{
			SearchType type;
			TDGazetteerFactory factory = new TDGazetteerFactory();
			string message = "Test failed";
			
			type = SearchType.AddressPostCode;
			ITDGazetteer gaz = factory.Gazetteer(type, "sessionID", false, StationType.Undetermined);
			Assert.IsTrue(gaz is AddressPostcodeGazetteer, message);

			type = SearchType.AllStationStops;
			gaz = factory.Gazetteer(type, "sessionID", false, StationType.Undetermined);
			Assert.IsTrue(gaz is AllStationsGazetteer, message);

			type = SearchType.Locality;
			gaz = factory.Gazetteer(type, "sessionID", false, StationType.Undetermined);
			Assert.IsTrue(gaz is LocalityGazetteer, message);

			type = SearchType.MainStationAirport;
			gaz = factory.Gazetteer(type, "sessionID", false, StationType.Undetermined);
			Assert.IsTrue(gaz is MajorStationsGazetteer, message);

			type = SearchType.MainStationAirport;
			gaz = factory.Gazetteer(type, "sessionID", false, StationType.Rail);
			Assert.IsTrue(gaz is MajorStationsGazetteer, message);

			type = SearchType.MainStationAirport;
			gaz = factory.Gazetteer(type, "sessionID", false, StationType.Coach);
			Assert.IsTrue(gaz is MajorStationsGazetteer, message);

			type = SearchType.POI;
			gaz = factory.Gazetteer(type, "sessionID", false, StationType.Undetermined);
			Assert.IsTrue(gaz is AttractionsGazetteer, message);

			type = SearchType.City;
			gaz = factory.Gazetteer(type, "sessionID", false, StationType.Undetermined);
			Assert.IsTrue(gaz is ImportantPlaceGazetteer, message);
		}
	}
}
