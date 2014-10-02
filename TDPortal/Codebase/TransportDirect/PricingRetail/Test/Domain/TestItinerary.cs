// *********************************************** 
// NAME			: TestItinerary.cs
// AUTHOR		: 
// DATE CREATED	: 14/10/03
// ************************************************ 

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using CJPInterface = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Test harness for Itinerary
	/// </summary>
	[TestFixture]
	public class TestItinerary
	{
		private Itinerary itinerary;	

		/// <summary>
		/// Initialises Service Discovery for the tests
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{								   
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());
		}

		/// <summary>
		/// Clears down Service Discovery after the tests have been run
		/// </summary>
		[TestFixtureTearDown]
		public void CleanUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		/// <summary>
		/// Test that PricingUnits and attributes are correctly set on creation for a matching return journey
		/// The journey should only have one PricingUnit in each direction
		/// </summary>
		[Test]
		public void TestCreationMatchingReturn()
		{
			itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov);
			// Check the default ItineraryType
			Assert.IsTrue(itinerary.Type==ItineraryType.Return, "ItineraryType incorrect for Matching Return");
			// Check that we have the correct number of outward and return PricingUnits
			Assert.AreEqual(1, itinerary.OutwardUnits.Count, "Incorrect number of outward PricingUnits");
			Assert.AreEqual(1, itinerary.ReturnUnits.Count, "Incorrect number of return PricingUnits");

		}

		/// <summary>
		/// Test that PricingUnits and attributes are correctly set on creation for a non-matching return journey
		/// The journey should have two outward PricingUnits and one return
		/// </summary>
		[Test]
		public void TestCreationNonMatchingReturn()
		{
			itinerary = new Itinerary(TestSampleJourneyData.MixedDovNot, TestSampleJourneyData.MixedNotDov);
			// Check the default ItineraryType
			Assert.IsTrue(itinerary.Type==ItineraryType.Single, "ItineraryType incorrect for Non-Matching Return");
			// Check that we have the correct number of outward and return PricingUnits
			Assert.AreEqual(2, itinerary.OutwardUnits.Count, "Incorrect number of outward PricingUnits");
			Assert.AreEqual(1, itinerary.ReturnUnits.Count, "Incorrect number of return PricingUnits");
		}

		/// <summary>
		/// Test that PricingUnits and attributes are correctly set on creation for a single journey
		/// The journey should have one outward PricingUnit
		/// </summary>
		[Test]
		public void TestCreationSingle()
		{
			itinerary = new Itinerary(TestSampleJourneyData.NatExDovNot, null);
			// Check the default ItineraryType
			Assert.IsTrue(itinerary.Type==ItineraryType.Single, "ItineraryType incorrect for Single");
			// Check that we have the correct number of outward and return PricingUnits
			Assert.AreEqual(1, itinerary.OutwardUnits.Count, "Incorrect number of outward PricingUnits");
			Assert.AreEqual(0, itinerary.ReturnUnits.Count, "Incorrect number of return PricingUnits");
		}
	
	}
}
