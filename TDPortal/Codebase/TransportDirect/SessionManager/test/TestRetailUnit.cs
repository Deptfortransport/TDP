// *********************************************** 
// NAME			: TestRetailUnit.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 18/03/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestRetailUnit.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:04   mturner
//Initial revision.
//
//   Rev 1.4   Oct 16 2007 13:57:34   mmodi
//Amended to pass a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.3   Nov 09 2005 12:31:52   build
//Automatically merged from branch for stream2818
//
//   Rev 1.2.1.1   Nov 02 2005 16:43:56   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2.1.0   Oct 29 2005 14:16:52   RPhilpott
//Updates to pricing methods.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Mar 31 2005 16:28:26   jgeorge
//Removal of TicketType from SelectedTicket
//
//   Rev 1.1   Mar 30 2005 15:10:26   jgeorge
//Integration and changes to retailer handoff
//
//   Rev 1.0   Mar 18 2005 14:01:12   jgeorge
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
	/// Test harness for the RetailUnit class
	/// </summary>
	[TestFixture]
	public class TestRetailUnit
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
		/// Verifies that if we create a RetailUnit with an online retailer, the
		/// properties return the expected values.
		/// </summary>
		[Test]
		public void TestCreationWithOnlineRetailer()
		{
			Itinerary itinerary = GetItinerary();

			// Create a SelectedTicket from the first ticket of the first pricing unit
			SelectedTicket selectedTicket = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], (Ticket)((PricingUnit)itinerary.OutwardUnits[0]).SingleFares.Tickets[0] );
			SelectedTicket[] selectedTickets = new SelectedTicket[] { selectedTicket };
			// Create an online retailer
            Retailer retailer = new Retailer("TEST", "TEST RETAILER", string.Empty, "www.madeuphandoffurl.com", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);

			// Create the RetailUnit
			RetailUnit unit = new RetailUnit(selectedTickets, retailer);

			// Check the properties
			Assert.AreEqual( selectedTickets, unit.Tickets, "SelectedTicket lists don't match" );
			Assert.AreEqual( 1, unit.OnlineRetailers.Length, "Online retailers list does not contain expected number of Retailers");
			Assert.AreEqual( retailer, unit.OnlineRetailers[0], "Online retailers list does not contain expected Retailer");
			Assert.AreEqual( 0, unit.OfflineRetailers.Length, "Offline retailers list is not empty");
		}

		/// <summary>
		/// Verifies that if we create a RetailUnit with an offline retailer, the
		/// properties return the expected values.
		/// </summary>
		[Test]
		public void TestCreationWithOfflineRetailer()
		{
			Itinerary itinerary = GetItinerary();

			// Create a SelectedTicket from the first ticket of the first pricing unit
			SelectedTicket selectedTicket = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], (Ticket)((PricingUnit)itinerary.OutwardUnits[0]).SingleFares.Tickets[0] );
			SelectedTicket[] selectedTickets = new SelectedTicket[] { selectedTicket };

			// Create an offline retailer
            Retailer retailer = new Retailer("TEST", "TEST RETAILER", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);

			// Create the RetailUnit
			RetailUnit unit = new RetailUnit(selectedTickets, retailer);

			// Check the properties
			Assert.AreEqual( selectedTickets, unit.Tickets, "SelectedTicket lists don't match" );
			Assert.AreEqual( 1, unit.OfflineRetailers.Length, "Offline retailers list does not contain expected number of Retailers");
			Assert.AreEqual( retailer, unit.OfflineRetailers[0], "Offline retailers list does not contain expected Retailer");
			Assert.AreEqual( 0, unit.OnlineRetailers.Length, "Online retailers list is not empty");
		}

		/// <summary>
		/// Verifies that if we create a RetailUnit with multiple SelectedTicket objects, the
		/// properties return the expected values.
		/// </summary>
		[Test]
		public void TestCreationWithMultipleTickets()
		{
			Itinerary itinerary = GetItinerary();

			// Create a SelectedTicket from the first ticket of the first pricing unit
			SelectedTicket[] selectedTickets = new SelectedTicket[3];
			selectedTickets[0] = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], (Ticket)((PricingUnit)itinerary.OutwardUnits[0]).SingleFares.Tickets[0] );
			selectedTickets[1] = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], (Ticket)((PricingUnit)itinerary.OutwardUnits[0]).SingleFares.Tickets[0] );
			selectedTickets[2] = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], (Ticket)((PricingUnit)itinerary.OutwardUnits[0]).SingleFares.Tickets[0] );

			// Create an online retailer
            Retailer retailer = new Retailer("TEST", "TEST RETAILER", string.Empty, "www.madeuphandoffurl.com", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);

			// Create the RetailUnit
			RetailUnit unit = new RetailUnit(selectedTickets, retailer);

			// Check the properties
			Assert.AreEqual( selectedTickets, unit.Tickets, "SelectedTicket lists don't match" );
			Assert.AreEqual( 1, unit.OnlineRetailers.Length, "Online retailers list does not contain expected number of Retailers");
			Assert.AreEqual( retailer, unit.OnlineRetailers[0], "Online retailers list does not contain expected Retailer");
			Assert.AreEqual( 0, unit.OfflineRetailers.Length, "Offline retailers list is not empty");
		}

		/// <summary>
		/// Verifies that the AddRetailers method works correctly
		/// </summary>
		[Test]
		public void TestAddRetailers()
		{
            Retailer online1 = new Retailer("online1", "online1", string.Empty, "www.madeuphandoffurl.com", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            Retailer online2 = new Retailer("online2", "online2", string.Empty, "www.madeuphandoffurl.com", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            Retailer online3 = new Retailer("online3", "online3", string.Empty, "www.madeuphandoffurl.com", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);

            Retailer offline1 = new Retailer("offline1", "online1", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            Retailer offline2 = new Retailer("offline2", "offline2", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            Retailer offline3 = new Retailer("offline3", "offline3", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
		
			Itinerary itinerary = GetItinerary();

			// Create a SelectedTicket from the first ticket of the first pricing unit
			SelectedTicket selectedTicket = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], (Ticket)((PricingUnit)itinerary.OutwardUnits[0]).SingleFares.Tickets[0] );
			SelectedTicket[] selectedTickets = new SelectedTicket[] { selectedTicket };

			// Create the RetailUnit
			RetailUnit unit = new RetailUnit(selectedTickets, online1);

			ArrayList expectedOnlineList = new ArrayList(3);
			ArrayList expectedOfflineList = new ArrayList(3);

			expectedOnlineList.Add( online1 );
			Assert.AreEqual( ArrayListToRetailerArray(expectedOnlineList), unit.OnlineRetailers, "Online retailers don't match expected");
			Assert.AreEqual( ArrayListToRetailerArray(expectedOfflineList), unit.OfflineRetailers, "Offline retailers don't match expected");

			unit.AddRetailer( offline1 );
			expectedOfflineList.Add( offline1 );
			Assert.AreEqual( ArrayListToRetailerArray(expectedOnlineList), unit.OnlineRetailers, "Online retailers don't match expected");
			Assert.AreEqual( ArrayListToRetailerArray(expectedOfflineList), unit.OfflineRetailers, "Offline retailers don't match expected");

			unit.AddRetailer( offline2 );
			expectedOfflineList.Add( offline2 );
			Assert.AreEqual( ArrayListToRetailerArray(expectedOnlineList), unit.OnlineRetailers, "Online retailers don't match expected");
			Assert.AreEqual( ArrayListToRetailerArray(expectedOfflineList), unit.OfflineRetailers, "Offline retailers don't match expected");

			unit.AddRetailer( online2 );
			expectedOnlineList.Add( online2 );
			Assert.AreEqual( ArrayListToRetailerArray(expectedOnlineList), unit.OnlineRetailers, "Online retailers don't match expected");
			Assert.AreEqual( ArrayListToRetailerArray(expectedOfflineList), unit.OfflineRetailers, "Offline retailers don't match expected");

			unit.AddRetailer( online3 );
			expectedOnlineList.Add( online3 );
			Assert.AreEqual( ArrayListToRetailerArray(expectedOnlineList), unit.OnlineRetailers, "Online retailers don't match expected");
			Assert.AreEqual( ArrayListToRetailerArray(expectedOfflineList), unit.OfflineRetailers, "Offline retailers don't match expected");

			unit.AddRetailer( offline3 );
			expectedOfflineList.Add( offline3 );
			Assert.AreEqual( ArrayListToRetailerArray(expectedOnlineList), unit.OnlineRetailers, "Online retailers don't match expected");
			Assert.AreEqual( ArrayListToRetailerArray(expectedOfflineList), unit.OfflineRetailers, "Offline retailers don't match expected");

		}

		/// <summary>
		/// Verifies that the MatchesTickets works correctly
		/// </summary>
		[Test]
		public void TestMatchesTickets()
		{
			Itinerary itinerary = GetItinerary();

			Ticket ticket1 = new Ticket("TX1", Flexibility.FullyFlexible, "TX1", (float)10, (float)6, (float)6, (float)4);
			Ticket ticket2 = new Ticket("TX2", Flexibility.FullyFlexible, "TX2", (float)10, (float)6, (float)6, (float)4);
			Ticket ticket3 = new Ticket("TX3", Flexibility.FullyFlexible, "TX3", (float)10, (float)6, (float)6, (float)4);
			Ticket ticket4 = new Ticket("TX4", Flexibility.FullyFlexible, "TX4", (float)10, (float)6, (float)6, (float)4);

			SelectedTicket selectedTicket1 = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], ticket1 );
			SelectedTicket selectedTicket2 = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], ticket2 );
			SelectedTicket selectedTicket3 = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], ticket3 );
			SelectedTicket selectedTicket4 = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], ticket4 );

			SelectedTicket[] selectedTicketsSupplied = new SelectedTicket[] { selectedTicket2, selectedTicket4, selectedTicket1, selectedTicket3 };
			SelectedTicket[] selectedTicketsExpected = new SelectedTicket[] { selectedTicket1, selectedTicket2, selectedTicket3, selectedTicket4 };
			SelectedTicket[] selectedTicketsIncomplete = new SelectedTicket[] { selectedTicket3, selectedTicket4 };
			SelectedTicket[] selectedTicketsEmpty = new SelectedTicket[0];
			SelectedTicket[] selectedTicketsDuplicates = new SelectedTicket[] { selectedTicket3, selectedTicket4, selectedTicket1, selectedTicket2, selectedTicket3, selectedTicket4 };

            Retailer retailer = new Retailer("TEST", "TEST", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);

			RetailUnit unit = new RetailUnit(selectedTicketsSupplied, retailer);

			Assert.IsTrue( unit.MatchesTickets( selectedTicketsExpected ) );
			Assert.IsFalse( unit.MatchesTickets( selectedTicketsIncomplete ) );
			Assert.IsFalse( unit.MatchesTickets( selectedTicketsEmpty ) );
			Assert.IsFalse( unit.MatchesTickets( selectedTicketsDuplicates ) );
		}

		/// <summary>
		/// Verifies that the ContainsTicket and ContainsRetailer methods work correctly
		/// </summary>
		[Test]
		public void TestContainsMethods()
		{
			Itinerary itinerary = GetItinerary();

			Ticket ticket1 = new Ticket("TX1", Flexibility.FullyFlexible, "TX1", (float)10, (float)6, (float)6, (float)4);
			Ticket ticket2 = new Ticket("TX2", Flexibility.FullyFlexible, "TX2", (float)10, (float)6, (float)6, (float)4);
			Ticket ticket3 = new Ticket("TX3", Flexibility.FullyFlexible, "TX3", (float)10, (float)6, (float)6, (float)4);
			Ticket ticket4 = new Ticket("TX4", Flexibility.FullyFlexible, "TX4", (float)10, (float)6, (float)6, (float)4);

			SelectedTicket selectedTicket1 = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], ticket1 );
			SelectedTicket selectedTicket2 = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], ticket2 );
			SelectedTicket selectedTicket3 = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], ticket3 );
			SelectedTicket selectedTicket4 = new SelectedTicket((PricingUnit)itinerary.OutwardUnits[0], ticket4 );

			SelectedTicket[] selectedTickets = new SelectedTicket[] { selectedTicket2, selectedTicket1, selectedTicket3 };

            Retailer online1 = new Retailer("online1", "online1", string.Empty, "www.madeuphandoffurl.com", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            Retailer online2 = new Retailer("online2", "online2", string.Empty, "www.madeuphandoffurl.com", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            Retailer online3 = new Retailer("online3", "online3", string.Empty, "www.madeuphandoffurl.com", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            Retailer offline1 = new Retailer("offline1", "online1", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            Retailer offline2 = new Retailer("offline2", "offline2", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);
            Retailer offline3 = new Retailer("offline3", "offline3", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty);

			RetailUnit unit = new RetailUnit(selectedTickets, online1);
			unit.AddRetailer( online2 );
			unit.AddRetailer( offline1 );
			unit.AddRetailer( offline2 );

			Assert.IsTrue( unit.ContainsTicket( selectedTicket1 ) );
			Assert.IsTrue( unit.ContainsTicket( selectedTicket2 ) );
			Assert.IsTrue( unit.ContainsTicket( selectedTicket3 ) );
			Assert.IsFalse( unit.ContainsTicket( selectedTicket4 ) );
			Assert.IsFalse( unit.ContainsTicket( null ) );

			Assert.IsTrue( unit.ContainsRetailer( online1 ) );
			Assert.IsTrue( unit.ContainsRetailer( online2 ) );
			Assert.IsTrue( unit.ContainsRetailer( offline1 ) );
			Assert.IsTrue( unit.ContainsRetailer( offline2 ) );

			Assert.IsFalse( unit.ContainsRetailer( online3 ) );
			Assert.IsFalse( unit.ContainsRetailer( offline3 ) );
			Assert.IsFalse( unit.ContainsRetailer( null ) );

		}

		#endregion

		#region Helper methods

		private Retailer[] ArrayListToRetailerArray(ArrayList list)
		{
			return (Retailer[])list.ToArray(typeof(Retailer));
		}


		/// <summary>
		/// Returns an Itinerary with Farees and Retailers initialised
		/// </summary>
		/// <returns></returns>
		private Itinerary GetItinerary()
		{
			// Create an itinerary
			Itinerary itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov);

			if (!itinerary.FaresInitialised)
			{
				ITimeBasedFareSupplierFactory factory = (ITimeBasedFareSupplierFactory) TDServiceDiscovery.Current[ServiceDiscoveryKey.TimeBasedFareSupplier];

				Discounts discounts = new Discounts(string.Empty, string.Empty, TicketClass.All);

				foreach (PricingUnit unit in itinerary.OutwardUnits)
				{
					ITimeBasedFareSupplier fareSupplier = factory.GetSupplier(unit.Mode);

					if	(fareSupplier != null)
					{
						fareSupplier.PricePricingUnit(unit, discounts, null, string.Empty);
					}
				}

				foreach (PricingUnit unit in itinerary.ReturnUnits)
				{
					ITimeBasedFareSupplier fareSupplier = factory.GetSupplier(unit.Mode);

					if	(fareSupplier != null)
					{
						fareSupplier.PricePricingUnit(unit, discounts, null, string.Empty);
					}
				}
			}

			if (!itinerary.RetailersInitialised)
			{
				itinerary.FindRetailers();
			}

			return itinerary;
		}

		#endregion
	}
}
