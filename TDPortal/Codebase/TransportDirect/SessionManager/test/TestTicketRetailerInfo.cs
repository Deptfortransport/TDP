// *********************************************** 
// NAME			: TestTicketRetailerInfo.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 21/12/04
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestTicketRetailerInfo.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:06   mturner
//Initial revision.
//
//   Rev 1.6   Oct 16 2007 13:59:02   mmodi
//Amended to pass a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.5   Nov 15 2005 08:57:58   jgeorge
//Updated to take into account changes in classes being tested.
//Resolution for 2995: DN040: Incorrect retailer and ticket information after using Back button
//Resolution for 2996: DN040: spurious Wait Page when selecting "Amend" in SBT
//Resolution for 3030: DN040: Switching between options on results page invokes wait page
//
//   Rev 1.4   Nov 09 2005 12:31:54   build
//Automatically merged from branch for stream2818
//
//   Rev 1.3.1.3   Nov 02 2005 16:44:44   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3.1.2   Oct 29 2005 16:18:04   RPhilpott
//Updated requestId
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3.1.1   Oct 29 2005 14:18:02   RPhilpott
//Updates to pricing methods.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3.1.0   Oct 14 2005 15:38:56   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Mar 31 2005 16:28:26   jgeorge
//Removal of TicketType from SelectedTicket
//
//   Rev 1.2   Mar 30 2005 15:10:20   jgeorge
//Integration and changes to retailer handoff
//
//   Rev 1.1   Mar 21 2005 10:05:44   jgeorge
//Updated commenting
//
//   Rev 1.0   Mar 21 2005 10:04:46   jgeorge
//Initial revision.

using System;
using System.Diagnostics;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using NUnit.Framework;
using System.Collections;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Test harness for TestTicketRetailerInfo class
	/// </summary>
	[TestFixture]
	public class TestTicketRetailerInfo
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
		/// Tests creation of the object for a single journey with a single pricing unit
		/// </summary>
		[Test, Ignore("This test is incomplete - the test data needs to be amended to provide a suitable journey")]
		public void TestCreateSingleJourneySingleUnit()
		{
			// Create an itinerary
			PublicJourney outwardJourney = (PublicJourney)TestSampleJourneyData.TrainDovNot;
			Itinerary itinerary = new Itinerary(outwardJourney, null);
			
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
	
			itinerary.FindRetailers();

			// Select the first ticket
			PricingUnit unit = (PricingUnit)itinerary.OutwardUnits[0];
			SelectedTicket[] tickets = new SelectedTicket[] { new SelectedTicket( unit, (Ticket)unit.SingleFares.Tickets[0]  ) };
			TicketRetailerInfo info = new TicketRetailerInfo(outwardJourney.Details[0].LegStart.DepartureDateTime, ItineraryType.Single, false, tickets, 0);

			// Check the properties first
			Assert.IsFalse( info.IsForReturn, "IsForReturn not as expected" );
			Assert.AreEqual( ItineraryType.Single, info.ItineraryType, "ItineraryType not as expected" );
			Assert.AreEqual( outwardJourney.Details[0].LegStart.DepartureDateTime, info.JourneyDate, "JourneyDate not as expected");

			Assert.AreEqual( tickets, info.SelectedTickets, "SelectedTickets arrays do not match" );

			// Now check the retail units. We expect one unit containing all the retailers
			Assert.AreEqual( 1, info.RetailUnits.Length, "Number of retailer units was not as expected" );
			
			RetailUnit retailUnit = info.RetailUnits[0];
			
			// Need to work out what we're expected for the retailer lists
			ArrayList expectedOnlineRetailers = new ArrayList();
			ArrayList expectedOfflineRetailers = new ArrayList();

			foreach (Retailer r in unit.Retailers)
			{
				if (r.isHandoffSupported)
					expectedOnlineRetailers.Add( r );
				else
					expectedOfflineRetailers.Add( r );
			}

			// Verify the tickets and retailers collections
			Assert.AreEqual( tickets, retailUnit.Tickets, "Tickets collection of retail unit not as expected" );
			Assert.AreEqual( ArrayListToRetailerArray(expectedOfflineRetailers), retailUnit.OfflineRetailers, "Offline retailers collection of retail unit not as expected" );
			Assert.AreEqual( ArrayListToRetailerArray(expectedOnlineRetailers), retailUnit.OnlineRetailers, "Online retailers collection of retail unit not as expected" );
		}

		/// <summary>
		/// Tests creation of the object for a single journey with two pricing units
		/// </summary>
		[Test, Ignore("This test is incomplete - the test data needs to be amended to provide fare details for the SCL leg of the journey")]
		public void TestCreateReturnJourneyMultipleUnit()
		{
			// Create an itinerary
			PublicJourney outwardJourney = (PublicJourney)TestSampleJourneyData.TrainDovNot;
			PublicJourney inwardJourney = (PublicJourney)TestSampleJourneyData.TrainNotDov;
			Itinerary itinerary = new Itinerary(outwardJourney, inwardJourney);

			Assert.AreEqual(2, itinerary.OutwardUnits.Count, "For test to work as expected, the itinerary needs two outward pricing units");
			
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
			
			itinerary.FindRetailers();
			Assert.IsTrue( ((PricingUnit)itinerary.OutwardUnits[0]).SingleFares.Tickets.Count > 0, "For the test two work, both pricing units must have at least one single fare" );
			Assert.IsTrue( ((PricingUnit)itinerary.OutwardUnits[1]).SingleFares.Tickets.Count > 0, "For the test two work, both pricing units must have at least one single fare" );


			// Get the units and use the first ticket from each to build SelectedTicket objects
			
			PricingUnit unit1 = (PricingUnit)itinerary.OutwardUnits[0];
			PricingUnit unit2 = (PricingUnit)itinerary.OutwardUnits[1];
			SelectedTicket[] tickets = new SelectedTicket[2];
			tickets[0] = new SelectedTicket( unit1, (Ticket)unit1.SingleFares.Tickets[0]  );
			tickets[1] = new SelectedTicket( unit2, (Ticket)unit2.SingleFares.Tickets[0]  );

			TicketRetailerInfo info = new TicketRetailerInfo(outwardJourney.Details[0].LegStart.DepartureDateTime, ItineraryType.Single, false, tickets, 0);

			// No need to check the properties, as that is done in TestCreateSingleJourneySingleUnit.
			// Just need to verify that the retail units are correct

			Assert.AreEqual( 2, info.RetailUnits.Length, "Incorrect number of retail units found" );

			// Build the list of expected retailers for the first unit
		}

		/// <summary>
		/// Tests generation of the object when there are three units with two of them being
		/// able to be sold by a single retailer (multiple ticket handoff)
		/// </summary>
		[Test, Ignore("This test is incomplete - the test data needs to be amended to provide a suitable journey")]
		public void TestCreateSingleJourneyMultipleUnitWithMTH()
		{
		}

		#endregion

		#region Helper methods

		private Retailer[] ArrayListToRetailerArray(ArrayList list)
		{
			return (Retailer[])list.ToArray(typeof(Retailer));
		}

		#endregion
	}

}
