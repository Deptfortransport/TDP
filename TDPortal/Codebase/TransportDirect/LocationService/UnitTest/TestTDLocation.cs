// *********************************************** 
// NAME                 : TestTDLocation.cs
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 02/06/04
// DESCRIPTION  : Test for TDLocation clas
// ***********************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestTDLocation.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:50   mturner
//Initial revision.
//
//   Rev 1.4   Mar 30 2006 12:17:14   build
//Automatically merged from branch for stream0018
//
//   Rev 1.3.1.0   Feb 16 2006 16:55:46   mguney
//TestToRequestPlace included.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.3   Mar 23 2005 11:55:32   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.2   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.1   Jan 19 2005 12:08:16   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.0   Jun 02 2004 17:35:04   acaunt
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Test fixture for TDLocation
	/// </summary>
	[TestFixture]
	public class TestTDLocation
	{

		private TDLocation matchingCoachLocation1;
		private TDLocation matchingCoachLocation2;
		private TDLocation matchingCoachLocationWithBays1;
		private TDLocation matchingCoachLocationWithBays2;
		private TDLocation unmatchingCoachLocation;
		private TDLocation matchingTiplocLocation1;
		private TDLocation matchingTiplocLocation2;
		private TDLocation unmatchingTiplocLocation;

		private TDLocation aberyswith;
		private TDLocation aberdeen;
		private TDLocation noNaptan;


		[TestFixtureSetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialisation());

			matchingCoachLocation1 = CreateLocation(new string[]{"9000123", "9000456"});
			matchingCoachLocation2 = CreateLocation(new string[]{"9000689", "9000456"});
			matchingCoachLocationWithBays1 = CreateLocation(new string[]{"9000321A", "9000654A"});
			matchingCoachLocationWithBays2 = CreateLocation(new string[]{"9000321B", "9000654B"});
			unmatchingCoachLocation = CreateLocation(new string[]{"9000111", "9000222"});
			matchingTiplocLocation1 = CreateLocation(new string[]{"91000ABC", "9100DEF"});
			matchingTiplocLocation2 = CreateLocation(new string[]{"9100DEF", "9100GHI"});
			unmatchingTiplocLocation = CreateLocation(new string[]{"9100TVU", "9100XYZ"});

			aberyswith = CreateLocation(new string[]{"900012183", "9100ABRYSTH", "9200CWL", "9200MAN"});
			aberdeen = CreateLocation(new string[]{"900090017", "9100ABRDEEN", "9100COVNTRY", "9200ABZ", "9200XXX0"});
			noNaptan = CreateLocation(new string[]{});

		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		/// <summary>
		/// Check that matching tiplocnaptans are recognised as being in the same group
		/// </summary>
		[Test]
		public void TestMatchingTiplocNaptans()
		{
			Assert.IsTrue(matchingTiplocLocation1.IsMatchingNaPTANGroup(matchingTiplocLocation2), "Matching tiploc locations not recognised as being in same group ");
		}

		/// <summary>
		/// Check that unmatching tiplocnaptans are not  recognised as being in the same group
		/// </summary>
		[Test]
		public void TestMatchingUnmatchingTiplocNaptans()
		{
			Assert.IsTrue(!matchingTiplocLocation1.IsMatchingNaPTANGroup(unmatchingTiplocLocation), "Unmatching tiploc locations not recognised as being in different group ");
		}


		/// <summary>
		/// Check that matching tiplocnaptans are recognised as being in the same group
		/// </summary>
		[Test]
		public void TestMatchingCoachNaptans()
		{
			Assert.IsTrue(matchingCoachLocation1.IsMatchingNaPTANGroup(matchingCoachLocation2), "Matching coach locations not recognised as being in same group ");
		}

		/// <summary>
		/// Check that matching tiplocnaptans are recognised as being in the same group despite the presence of different bay codes
		/// </summary>
		[Test]
		public void TestMatchingWithBaysCoachNaptans()
		{
			Assert.IsTrue(matchingCoachLocationWithBays1.IsMatchingNaPTANGroup(matchingCoachLocationWithBays2), "Matching coach locations with bays not recognised as being in same group ");
		}

		/// <summary>
		/// Check that unmatching tiplocnaptans are not  recognised as being in the same group
		/// </summary>
		[Test]
		public void TestUnmatchingCoachNaptans()
		{
			Assert.IsTrue(!matchingCoachLocation1.IsMatchingNaPTANGroup(unmatchingCoachLocation), "Unmatching coach locations not recognised as being in different group ");
		}

		/// <summary>
		/// Tests ToRequestPlace method.
		/// </summary>
		[Test]
		public void TestToRequestPlace()
		{
			//Test airport
			RequestPlace place = aberyswith.ToRequestPlace(new TDDateTime(DateTime.Now),StationType.Airport,true,false,false);
			Assert.AreEqual(2,place.stops.Length,"The number of stops don't match. Aberyswith-Airport");
			Assert.AreEqual("9200CWL",place.stops[0].NaPTANID,"The first naptan is wrong. Aberyswith-Airport");
			Assert.AreEqual("9200MAN",place.stops[1].NaPTANID,"The second naptan is wrong. Aberyswith-Airport");
			//Test airport + rail
			StationType[] stationTypes = new StationType[2];
			stationTypes[0] = StationType.Airport;
			stationTypes[1] = StationType.Rail;
			place = aberyswith.ToRequestPlace(new TDDateTime(DateTime.Now),stationTypes,true,false,false);
			Assert.AreEqual(3,place.stops.Length,"The number of stops don't match. Aberyswith-Airport-Rail");
			Assert.AreEqual("9100ABRYSTH",place.stops[0].NaPTANID,"The first naptan is wrong. Aberyswith-Airport-Rail");
			Assert.AreEqual("9200CWL",place.stops[1].NaPTANID,"The second naptan is wrong. Aberyswith-Airport-Rail");
			Assert.AreEqual("9200MAN",place.stops[2].NaPTANID,"The third naptan is wrong. Aberyswith-Airport-Rail");
			//Test undetermined
			place = aberyswith.ToRequestPlace(new TDDateTime(DateTime.Now),StationType.Undetermined,true,false,false);
			Assert.AreEqual(10,place.stops.Length,"The number of stops don't match. Aberyswith-Undetermined");
			Assert.AreEqual("9200LHR4",place.stops[9].NaPTANID,"The last naptan is wrong. Aberyswith-Undetermined");			

			//Aberdeen is in the TestLocationServicePlaceData.xml as well so that the locality can be picked up.
			place = aberdeen.ToRequestPlace(new TDDateTime(DateTime.Now),StationType.Undetermined,true,false,false);
			Assert.AreEqual(11,place.stops.Length,"The number of stops don't match. Aberdeen-Undetermined");
			Assert.AreEqual("900090017",place.stops[0].NaPTANID,"The first naptan is wrong. Aberdeen-Undetermined");			
			Assert.AreEqual("9200LHR4",place.stops[6].NaPTANID,"The seventh(last) naptan is wrong. Aberdeen-Undetermined");			
			Assert.AreEqual("ES000011",place.locality,"The locality is wrong. Aberdeen-Undetermined");

			//Private journey toid test
			aberdeen.Toid = new string[] {"1","2","3"};
			aberdeen.Toid = new string[] {"1","2","3"};
			place = aberdeen.ToRequestPlace(new TDDateTime(DateTime.Now),StationType.Undetermined,true,true,false);
			Assert.AreEqual(11,place.stops.Length,"The number of stops don't match. Aberdeen-Undetermined");
			Assert.AreEqual("900090017",place.stops[0].NaPTANID,"The first naptan is wrong. Aberdeen-Undetermined");			
			Assert.AreEqual("9200LHR4",place.stops[6].NaPTANID,"The seventh naptan is wrong. Aberdeen-Undetermined");			
			Assert.AreEqual("ES000011",place.locality,"The locality is wrong. Aberdeen-Undetermined");
			Assert.AreEqual(3,place.roadPoints.Length,"The roadpoints count wrong. Aberdeen-Undetermined");
			Assert.AreEqual("1",place.roadPoints[0].TOID,"The toid is wrong. Aberdeen-Undetermined");

			//no naptan
			place = noNaptan.ToRequestPlace(new TDDateTime(DateTime.Now),StationType.Undetermined,true,true,false);
			Assert.AreEqual(1,place.stops.Length,"The number of stops don't match. no naptan");
			
		}

		/// <summary>
		/// Helper method to create TDNaptan objects
		/// </summary>
		/// <param name="ids"></param>
		private TDLocation CreateLocation(string[] ids)
		{
			TDNaptan[] naptans = new TDNaptan[ids.Length];
			for (int i=0; i<naptans.Length;++i)
			{
				naptans[i] = new TDNaptan();
				naptans[i].Naptan = ids[i];
			}
			TDLocation location = new TDLocation();
			location.NaPTANs = naptans;
			return location;
		}
	}
}
