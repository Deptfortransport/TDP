// *********************************************** 
// NAME			: TestPricedJourneySegment.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 21/12/04
// DESCRIPTION	: Test harness for PricedJourneySegment class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestPricedJourneySegment.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:04   mturner
//Initial revision.
//
//   Rev 1.2   Mar 14 2006 08:41:46   build
//Automatically merged from branch for stream3353
//
//   Rev 1.1.1.0   Mar 10 2006 19:09:42   rhopkins
//Removal of JourneyDetail class.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Mar 18 2005 09:21:58   jgeorge
//Added tests
//
//   Rev 1.0   Dec 23 2004 11:53:48   jgeorge
//Initial revision.

using System;
using System.Diagnostics;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Test harness for PricedJourneySegment class
	/// </summary>
	[TestFixture]
	public class TestPricedJourneySegment
	{

		/// <summary>
		/// Initialises Service Discovery for the tests
		/// </summary>
		[TestFixtureSetUp]
		public void Setup()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation());
		}

		/// <summary>
		/// Clears down Service Discovery so that other tests aren't affected
		/// </summary>
		[TestFixtureTearDown]
		public void TearDown()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		/// <summary>
		/// Ensures that the class works correctly when used with a PricingUnit that has both
		/// inbound and outbound legs.
		/// </summary>
		[Test]
		public void TestCreateFromReturnPricingUnit()
		{
			// Create an itinerary
			Itinerary itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov);
			
			PricingUnit unitToUse = (PricingUnit)itinerary.ReturnUnits[0];

			// This will create a matching return itinerary, so we will use the first return pricing unit
			PricedJourneySegment segment = new PricedJourneySegment(unitToUse, true, itinerary.ReturnUnits.Count == 1);

			Assert.AreSame(unitToUse, segment.PricingUnit, "PricingUnits do not match");
			Assert.IsTrue(segment.UnitIsPriced, "UnitIsPriced property not as expected");
			Assert.IsTrue(segment.IsFirst, "Segment should be first, but IsFirst returns false");
			if (itinerary.ReturnUnits.Count == 1)
				Assert.IsTrue(segment.IsLast, "Segment should be last, but IsLast returns false");
			else
				Assert.IsFalse(segment.IsLast, "Segment is not last, but IsLast returns true");
			
			Assert.AreSame(unitToUse.OutboundLegs, segment.OutboundLegs, "Outbound legs don't match");
			Assert.AreSame(unitToUse.InboundLegs, segment.InboundLegs, "Inbound legs don't match");
		}

		/// <summary>
		/// Ensures that the class works correctly when used with a PricingUnit that has only
		/// outbound legs.
		/// </summary>
		[Test]
		public void TestCreateFromSinglePricingUnit()
		{
			// Create an itinerary
			Itinerary itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, null);
			
			PricingUnit unitToUse = (PricingUnit)itinerary.OutwardUnits[0];

			// This will create a matching return itinerary, so we will use the first return pricing unit
			PricedJourneySegment segment = new PricedJourneySegment(unitToUse, true, itinerary.ReturnUnits.Count == 1);

			Assert.AreSame(unitToUse, segment.PricingUnit, "PricingUnits do not match");
			Assert.IsTrue(segment.UnitIsPriced, "UnitIsPriced property not as expected");
			Assert.IsTrue(segment.IsFirst, "Segment should be first, but IsFirst returns false");
			if (itinerary.ReturnUnits.Count == 1)
				Assert.IsTrue(segment.IsLast, "Segment should be last, but IsLast returns false");
			else
				Assert.IsFalse(segment.IsLast, "Segment is not last, but IsLast returns true");
			
			Assert.AreSame(unitToUse.OutboundLegs, segment.OutboundLegs, "Outbound legs don't match");
			Assert.AreSame(unitToUse.InboundLegs, segment.InboundLegs, "Inbound legs don't match");
		}

		/// <summary>
		/// Ensures that the class works correctly when used a single leg of a journey
		/// </summary>
		[Test]
		public void TestCreateFromUnpricedLeg()
		{
			PublicJourney journey = TestSampleJourneyData.TrainDovNot as PublicJourney;
			PublicJourneyDetail detail = journey.Details[0] as PublicJourneyDetail;

			PricedJourneySegment segment = new PricedJourneySegment(detail, true, true);

			// Check that the segment returns the expected values
			Assert.AreEqual( true, segment.IsFirst, "Unexpected value of IsFirst found");
			Assert.AreEqual( true, segment.IsLast, "Unexpected value of IsLast found");
			Assert.IsFalse( segment.UnitIsPriced, "Unexpected value of UnitIsPriced");
			Assert.AreEqual( 1, segment.OutboundLegs.Count, "Unexpected number of journey legs");
			Assert.AreSame( detail, segment.OutboundLegs[0], "OutboundLegs not as expected");
			Assert.AreEqual( 0, segment.InboundLegs.Count, "There should be no inbound journey legs for an unpriced leg");
		}
	}
}
