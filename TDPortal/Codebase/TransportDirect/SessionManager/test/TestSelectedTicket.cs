// *********************************************** 
// NAME			: TestSelectedTicket.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 18/03/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestSelectedTicket.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:06   mturner
//Initial revision.
//
//   Rev 1.5   Oct 16 2007 13:58:10   mmodi
//Amended to pass a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.4   Nov 09 2005 12:31:52   build
//Automatically merged from branch for stream2818
//
//   Rev 1.3.1.2   Nov 02 2005 16:44:16   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3.1.1   Oct 29 2005 14:17:14   RPhilpott
//Updates to pricing methods.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3.1.0   Oct 14 2005 15:38:56   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Apr 28 2005 14:18:26   jgeorge
//Removal of unneeded test
//Resolution for 2309: PT - Train - Destination wrong on rail ticket description.
//
//   Rev 1.2   Mar 31 2005 16:28:26   jgeorge
//Removal of TicketType from SelectedTicket
//
//   Rev 1.1   Mar 30 2005 15:10:24   jgeorge
//Integration and changes to retailer handoff
//
//   Rev 1.0   Mar 18 2005 11:24:04   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Test harness for the SelectedTicket class
	/// </summary>
	[TestFixture]
	public class TestSelectedTicket
	{
		#region Setup/teardown

		/// <summary>
		/// Initialises Service Discovery for the tests
		/// </summary>
		[TestFixtureSetUp]
		public void Setup()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation());
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.RetailerCatalogue, new RetailerCatalogueFactory());
		}

		/// <summary>
		/// Clears down Service Discovery so that other tests aren't affected
		/// </summary>
		[TestFixtureTearDown]
		public void TearDown()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		#endregion

		#region Tests

		/// <summary>
		/// Verifies that the SelectedTicket is correctly created from a PricingUnit
		/// on which the Price and FindRetailers methods have been called.
		/// </summary>
		[Test]
		public void TestCreateFromPricingUnitWithRetailers()
		{
			// Create an itinerary
			Itinerary itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov);
			
			if (!itinerary.FaresInitialised)
			{
				ITimeBasedFareSupplierFactory factory = (ITimeBasedFareSupplierFactory) TDServiceDiscovery.Current[ServiceDiscoveryKey.TimeBasedFareSupplier];

				Discounts discounts = new Discounts(string.Empty, string.Empty, TicketClass.All);

				foreach (PricingUnit pu in itinerary.OutwardUnits)
				{
					ITimeBasedFareSupplier fareSupplier = factory.GetSupplier(pu.Mode);

					if	(fareSupplier != null)
					{
						fareSupplier.PricePricingUnit(pu, discounts, null, string.Empty);
					}
				}

				foreach (PricingUnit pu in itinerary.ReturnUnits)
				{
					ITimeBasedFareSupplier fareSupplier = factory.GetSupplier(pu.Mode);

					if	(fareSupplier != null)
					{
						fareSupplier.PricePricingUnit(pu, discounts, null, string.Empty);
					}
				}
			}

			if (!itinerary.RetailersInitialised)
			{
				itinerary.FindRetailers();
			}

			// Obtain the pricing unit and ticket we are going to use (first outbound pricing unit, first single ticket)
			PricingUnit unit = itinerary.OutwardUnits[0] as PricingUnit;
			Ticket ticket = unit.SingleFares.Tickets[0] as Ticket;

			SelectedTicket selectedTicket = new SelectedTicket( unit, ticket );

			// Check the properties of the SelectedTicket
			TDLocation expectedOrigin = ((PublicJourneyDetail)unit.OutboundLegs[0]).LegStart.Location;
			TDLocation expectedDestination = ((PublicJourneyDetail)unit.OutboundLegs[unit.OutboundLegs.Count - 1]).LegEnd.Location;

			PublicJourneyDetail[] expectedOutboundLegs = (PublicJourneyDetail[])((ArrayList)unit.OutboundLegs).ToArray(typeof(PublicJourneyDetail));
			PublicJourneyDetail[] expectedInboundLegs = (PublicJourneyDetail[])((ArrayList)unit.InboundLegs).ToArray(typeof(PublicJourneyDetail));

			Assert.AreEqual(expectedOrigin, selectedTicket.OriginLocation, "Origin location not as expected");
			Assert.AreEqual(expectedDestination, selectedTicket.DestinationLocation, "Destination location not as expected");
			Assert.AreEqual(expectedOutboundLegs, selectedTicket.OutboundLegs, "Outbound journey details do not match");
			Assert.AreEqual(expectedInboundLegs, (IList)selectedTicket.InboundLegs, "Inbound journey details do not match");
			Assert.AreEqual(unit.Mode, selectedTicket.Mode, "Mode does not match");
			Assert.AreEqual(ticket, selectedTicket.Ticket, "Tickets do not match");

			Assert.AreSame( unit.Retailers, selectedTicket.Retailers, "Retailers lists do not match" );
		}

		/// <summary>
		/// Verifies that the SelectedTicket is correctly created from a PricingUnit
		/// on which the Price method has been called but the FindRetailers method hasn't.
		/// The SelectedTicket should find the correct retailers when it is created, and 
		/// these should be the same as the ones the itinerary would have found.
		/// </summary>
		[Test]
		public void TestCreateFromPricingUnitWithoutRetailers()
		{
			// Create an itinerary
			Itinerary itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov);
			
			if (!itinerary.FaresInitialised)
			{
				ITimeBasedFareSupplierFactory factory = (ITimeBasedFareSupplierFactory) TDServiceDiscovery.Current[ServiceDiscoveryKey.TimeBasedFareSupplier];

				Discounts discounts = new Discounts(string.Empty, string.Empty, TicketClass.All);

				foreach (PricingUnit pu in itinerary.OutwardUnits)
				{
					ITimeBasedFareSupplier fareSupplier = factory.GetSupplier(pu.Mode);

					if	(fareSupplier != null)
					{
						fareSupplier.PricePricingUnit(pu, discounts, null, string.Empty);
					}
				}

				foreach (PricingUnit pu in itinerary.ReturnUnits)
				{
					ITimeBasedFareSupplier fareSupplier = factory.GetSupplier(pu.Mode);

					if	(fareSupplier != null)
					{
						fareSupplier.PricePricingUnit(pu, discounts, null, string.Empty);
					}
				}
			}

			// The point of this test is to verify that the retailers will be populated in the 
			// SelectedTicket constructor if they are not already present in the pricing unit,
			// so there's no point continuing if the Retailers have been initialised
			Assert.IsFalse( itinerary.RetailersInitialised );

			// Obtain the pricing unit and ticket we are going to use (first outbound pricing unit, first single ticket)
			PricingUnit unit = itinerary.OutwardUnits[0] as PricingUnit;
			Ticket ticket = unit.SingleFares.Tickets[0] as Ticket;

			SelectedTicket selectedTicket = new SelectedTicket( unit, ticket );

			// Check the properties of the SelectedTicket
			TDLocation expectedOrigin = ((PublicJourneyDetail)unit.OutboundLegs[0]).LegStart.Location;
			TDLocation expectedDestination = ((PublicJourneyDetail)unit.OutboundLegs[unit.OutboundLegs.Count - 1]).LegEnd.Location;

			PublicJourneyDetail[] expectedOutboundLegs = (PublicJourneyDetail[])((ArrayList)unit.OutboundLegs).ToArray(typeof(PublicJourneyDetail));
			PublicJourneyDetail[] expectedInboundLegs = (PublicJourneyDetail[])((ArrayList)unit.InboundLegs).ToArray(typeof(PublicJourneyDetail));

			Assert.AreEqual(expectedOrigin, selectedTicket.OriginLocation, "Origin location not as expected");
			Assert.AreEqual(expectedDestination, selectedTicket.DestinationLocation, "Destination location not as expected");
			Assert.AreEqual(expectedOutboundLegs, selectedTicket.OutboundLegs, "Outbound journey details do not match");
			Assert.AreEqual(expectedInboundLegs, selectedTicket.InboundLegs, "Inbound journey details do not match");
			Assert.AreEqual(unit.Mode, selectedTicket.Mode, "Mode does not match");
			Assert.AreEqual(ticket, selectedTicket.Ticket, "Tickets do not match");

			// Now initialise the retailers on the itinerary and make sure those for the
			// pricing unit match those that the SelectedTicket class has found
			itinerary.FindRetailers();

			Assert.AreSame( unit.Retailers, selectedTicket.Retailers, "Retailers lists do not match" );
		}

		#endregion

	}
}
