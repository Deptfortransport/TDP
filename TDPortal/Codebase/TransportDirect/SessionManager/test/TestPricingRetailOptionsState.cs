// *********************************************** 
// NAME			: TestPricingRetailOptionsState.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 21/03/05
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestPricingRetailOptionsState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:04   mturner
//Initial revision.
//
//   Rev 1.9   Oct 16 2007 13:56:32   mmodi
//Amended to pass a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.8   Feb 08 2006 15:28:34   mtillett
//Turn off tests that cannot be fixed easily
//
//   Rev 1.7   Dec 06 2005 08:31:06   jgeorge
//Bug fix
//Resolution for 3313: Door-to-door: selecting open return on input page does not display return fares on results page
//
//   Rev 1.6   Dec 05 2005 17:14:54   jgeorge
//Updated after modification to IsOpenReturn property of TDJourneyParametersMulti
//Resolution for 3313: Door-to-door: selecting open return on input page does not display return fares on results page
//
//   Rev 1.5   Nov 15 2005 08:58:00   jgeorge
//Updated to take into account changes in classes being tested.
//Resolution for 2995: DN040: Incorrect retailer and ticket information after using Back button
//Resolution for 2996: DN040: spurious Wait Page when selecting "Amend" in SBT
//Resolution for 3030: DN040: Switching between options on results page invokes wait page
//
//   Rev 1.4   Nov 09 2005 12:31:52   build
//Automatically merged from branch for stream2818
//
//   Rev 1.3.1.4   Nov 02 2005 16:43:40   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//
//   Rev 1.3.1.3   Oct 29 2005 16:17:40   RPhilpott
//Updated requestId
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3.1.2   Oct 29 2005 14:16:28   RPhilpott
//Updates to pricing methods.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3.1.1   Oct 25 2005 16:46:20   RPhilpott
//Work in progress
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3.1.0   Oct 14 2005 15:38:56   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Jul 06 2005 09:21:06   MTillett
//Repair to unit test, ensuring that jorney itinerary set for PricingRetailOptionsState before testing it.
//
//   Rev 1.2   Mar 31 2005 16:28:24   jgeorge
//Removal of TicketType from SelectedTicket
//
//   Rev 1.1   Mar 30 2005 15:10:30   jgeorge
//Integration and changes to retailer handoff
//
//   Rev 1.0   Mar 21 2005 13:39:38   jgeorge
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Test harness for the PricingRetailOptionState class
	/// Does not test most of the properties are they don't do anything other than get/set
	/// internal backing fields
	/// </summary>
	[TestFixture]
	public class TestPricingRetailOptionsState
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
		/// Tests the SetProcessedJourneys method as well as the HasProcessedRetailersJourneyChanged
		/// and HasProcessedFaresJourneyChanged when the session partition is time based
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk - to be reactived when fixed")]
		public void TestProcessedJourneyMethodsForTimeBased()
		{
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			sessionManager.Partition = TDSessionPartition.TimeBased;

			// Need a TDJourneyViewState object
			TDJourneyViewState journeyViewState = new TDJourneyViewState();
			journeyViewState.SelectedOutwardJourney = 2;
			journeyViewState.SelectedReturnJourney = 1;
			sessionManager.JourneyViewState = journeyViewState;

			JourneyPlanState jps = new JourneyPlanState();
			Guid expectedGuid = Guid.NewGuid();
			jps.RequestID = expectedGuid;
			sessionManager.AsyncCallState = jps;

			// Now call the method
			PricingRetailOptionsState options = new PricingRetailOptionsState();
			options.SetProcessedJourneys();

			// Now verify that all is as expected
			Assert.AreEqual( 2, options.ProcessedOutwardJourney, "ProcessedOutwardJourney not as expected" );
			Assert.AreEqual( 1, options.ProcessedReturnJourney, "ProcessedReturnJourney not as expected" );

			// Now change the values in the state classes to test the HasProcessedRetailersJourneyChanged method

			// First change SelectedOutwardJourney
			journeyViewState.SelectedOutwardJourney = 5;
			Assert.IsTrue( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned true since SelectedOutwardJourney has changed" );
			Assert.IsTrue( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned true since SelectedOutwardJourney has changed" );
			// Change it back
			journeyViewState.SelectedOutwardJourney = 2;
			Assert.IsFalse( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned false" );
			Assert.IsFalse( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned false" );

			// First change SelectedOutwardJourney
			journeyViewState.SelectedReturnJourney = 4;
			Assert.IsTrue( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned true since SelectedOutwardJourney has changed" );
			Assert.IsTrue( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned true since SelectedOutwardJourney has changed" );
			// Change it back
			journeyViewState.SelectedReturnJourney = 1;
			Assert.IsFalse( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned false" );
			Assert.IsFalse( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned false" );

			// Set ApplyNewDiscounts to true
			options.ApplyNewDiscounts = true;
			Assert.IsFalse( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned false" );
			Assert.IsTrue( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned true since ApplyNewDiscounts is true" );

		}

		/// <summary>
		/// Tests the SetProcessedJourneys method as well as the HasProcessedRetailersJourneyChanged
		/// and HasProcessedFaresJourneyChanged when the session partition is cost based
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk - to be reactived when fixed")]
		public void TestProcessedJourneyMethodsForCostBased()
		{
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			sessionManager.Partition = TDSessionPartition.CostBased;

			// Need a TDJourneyViewState object
			TDJourneyViewState journeyViewState = new TDJourneyViewState();
			journeyViewState.SelectedOutwardJourney = 2;
			journeyViewState.SelectedReturnJourney = 1;
			sessionManager.JourneyViewState = journeyViewState;

			AsyncCallState stateData = new CostBasedFaresSearchState();
			Guid expectedGuid = Guid.NewGuid();
			stateData.RequestID = expectedGuid;
			sessionManager.AsyncCallState = stateData;

			// Now call the method
			PricingRetailOptionsState options = new PricingRetailOptionsState();
			options.SetProcessedJourneys();

			// Now verify that all is as expected
			Assert.AreEqual( 2, options.ProcessedOutwardJourney, "ProcessedOutwardJourney not as expected" );
			Assert.AreEqual( 1, options.ProcessedReturnJourney, "ProcessedReturnJourney not as expected" );

			// Now change the values in the state classes to test the HasProcessedRetailersJourneyChanged method

			// First change SelectedOutwardJourney
			journeyViewState.SelectedOutwardJourney = 5;
			Assert.IsTrue( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned true since SelectedOutwardJourney has changed" );
			Assert.IsTrue( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned true since SelectedOutwardJourney has changed" );
			// Change it back
			journeyViewState.SelectedOutwardJourney = 2;
			Assert.IsFalse( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned false" );
			Assert.IsFalse( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned false" );

			// First change SelectedOutwardJourney
			journeyViewState.SelectedReturnJourney = 4;
			Assert.IsTrue( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned true since SelectedOutwardJourney has changed" );
			Assert.IsTrue( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned true since SelectedOutwardJourney has changed" );
			// Change it back
			journeyViewState.SelectedReturnJourney = 1;
			Assert.IsFalse( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned false" );
			Assert.IsFalse( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned false" );

			// Set ApplyNewDiscounts to true
			options.ApplyNewDiscounts = true;
			Assert.IsFalse( options.HasProcessedRetailersJourneyChanged, "HasProcessedRetailersJourneyChanged should have returned false" );
			Assert.IsTrue( options.HasProcessedFaresJourneyChanged, "HasProcessedFaresJourneyChanged should have returned true since ApplyNewDiscounts is true" );

		}

		/// <summary>
		/// Tests the methods GetSelectedTicket, SetSelectedTicket and ClearSelectedTickets
		/// </summary>
		[Test]
		public void TestSelectedTicketMethod()
		{
			// Create the object - first get an itinerary
			Itinerary itinerary = GetItinerary(true);
			PricingUnit unit = (PricingUnit)itinerary.OutwardUnits[0];

			PricingRetailOptionsState options = new PricingRetailOptionsState();
			Assert.IsNull(options.GetSelectedTicket(unit), "GetSelectedTicket returned a non-null value before any calls to SetSelectedTicket");

			// Try setting the selected ticket to the first one in the unit
			options.SetSelectedTicket(unit, (Ticket)unit.SingleFares.Tickets[0]);
			Assert.AreEqual( (Ticket)unit.SingleFares.Tickets[0], options.GetSelectedTicket(unit), "Selected ticket not as expected" );

			// Set it to "No ticket selected"
			options.SetSelectedTicket( unit, PricingRetailOptionsState.NoTicket );
			Assert.AreEqual( PricingRetailOptionsState.NoTicket, options.GetSelectedTicket(unit), "Selected ticket not as expected" );

			// Set it to null
			options.SetSelectedTicket( unit, null );
			Assert.IsNull(options.GetSelectedTicket(unit), "GetSelectedTicket returned a non-null value before any calls to SetSelectedTicket");

			// Set it to the second ticket in the unit
			options.SetSelectedTicket( unit, (Ticket)unit.ReturnFares.Tickets[0]);
			Assert.AreEqual( (Ticket)unit.ReturnFares.Tickets[0], options.GetSelectedTicket(unit) );

			// Clear the selected tickets
			options.ClearSelectedTickets();
			Assert.IsNull(options.GetSelectedTicket(unit), "GetSelectedTicket returned a non-null value before any calls to SetSelectedTicket");

		}

		/// <summary>
		/// Tests the InitOverrideItineraryType method. This method intialises the OverrideItineraryType
		/// property, which will be set to the same as the itinerary's itinerary type unless the 
		/// journey parameters specify an open return. In this case, the itinerary will have been
		/// created as type single, but should be overridden to return
		/// </summary>
		[Test]
		public void TestInitOverrideItineraryType()
		{
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			TDJourneyParametersMulti journeyParameters = new TDJourneyParametersMulti();
			PricingRetailOptionsState options = new PricingRetailOptionsState();

			sessionManager.JourneyParameters = journeyParameters;

			journeyParameters.ReturnMonthYear = ReturnType.NoReturn.ToString();

			// First test with a single itinerary
			options.InitOverrideItineraryType(ItineraryType.Single);
			Assert.AreEqual( ItineraryType.Single, options.OverrideItineraryType, "OverrideItineraryType should have been initialised to Single" );

			// Now with a return itinerary
			options.InitOverrideItineraryType(ItineraryType.Return);
			Assert.AreEqual( ItineraryType.Return, options.OverrideItineraryType, "OverrideItineraryType should have been initialised to Return" );
			
			// Now try the case with the open return
			journeyParameters.ReturnMonthYear = ReturnType.OpenReturn.ToString();
			options.InitOverrideItineraryType(ItineraryType.Single);
			Assert.AreEqual( ItineraryType.Return, options.OverrideItineraryType, "OverrideItineraryType should have been initialised to Return" );
		}

		/// <summary>
		/// Tests the OutwardTicketRetailerInfo, ReturnTicketRetailerInfo and 
		/// HasProcessedTicketRetailerInfoJourneyChanged methods
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk - to be reactived when fixed")]
		public void TestTicketRetailerInfoMethods()
		{
			// Initial set up - need to be able to call SetProcessedJourneys before continuing
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

			// Need a TDJourneyViewState object
			TDJourneyViewState journeyViewState = new TDJourneyViewState();
			journeyViewState.SelectedOutwardJourney = 2;
			journeyViewState.SelectedReturnJourney = 1;
			sessionManager.JourneyViewState = journeyViewState;

			// Also need a JourneyPlanStateData
			AsyncCallState jpsd = new JourneyPlanState();
			int expectedGuid = 0;
			jpsd.RequestID = Guid.NewGuid();
			sessionManager.AsyncCallState = jpsd;

			// Now call the method
			PricingRetailOptionsState options = new PricingRetailOptionsState();

			// HasProcessedTicketRetailerInfoJourneyChanged should initially return true 
			// regardless of Itinerary Type

			options.OverrideItineraryType = ItineraryType.Single;
			Assert.IsTrue( options.HasProcessedTicketRetailerInfoJourneyChanged, "HasProcessedTicketRetailerInfoJourneyChanged should intitially return true");

			options.OverrideItineraryType = ItineraryType.Return;
			Assert.IsTrue( options.HasProcessedTicketRetailerInfoJourneyChanged, "HasProcessedTicketRetailerInfoJourneyChanged should intitially return true");

			options.SetProcessedJourneys();
			// Create an itinerary
			Itinerary itinerary = GetItinerary(true);
			options.JourneyItinerary = itinerary;
			PublicJourney outwardJourney = (PublicJourney)itinerary.OutwardJourney;
			PublicJourney inwardJourney = (PublicJourney)itinerary.ReturnJourney;

			// Generate and set the outward TicketRetailerInfo
			PricingUnit unit = (PricingUnit)itinerary.OutwardUnits[0];
			SelectedTicket[] tickets = new SelectedTicket[] { new SelectedTicket( unit, (Ticket)unit.SingleFares.Tickets[0] ) };
			TicketRetailerInfo outwardInfo = new TicketRetailerInfo(outwardJourney.Details[0].LegStart.DepartureDateTime, ItineraryType.Single, false, tickets, expectedGuid);

			// Now generate and set the inward one
			unit = (PricingUnit)itinerary.ReturnUnits[0];
			tickets = new SelectedTicket[] { new SelectedTicket( unit, (Ticket)unit.SingleFares.Tickets[0]  ) };
			TicketRetailerInfo inwardInfo = new TicketRetailerInfo(inwardJourney.Details[0].LegStart.DepartureDateTime, ItineraryType.Single, true, tickets, expectedGuid);

			// The methods shoudl return true if:
			// 1. OverrideItineraryType = Single
			//    a) they are both null; or
			//    b) if one or both is not null, their CJPRequestId should match 
			//       ProcessedCjpCallRequestID and the ticket selection must not have
			//       changed since they were set.
			// 2. OverrideItineraryType = Return
			//    a) the OutwardTicketRetailerInfo is null or ReturnTicketRetailerInfo is not null
			//    b) as b) above, (but only for OutwardTicketRetailerInfo)

			// Tests for OverrideItineraryType = Single
			options.OverrideItineraryType = ItineraryType.Single;

			// Outward one is not null
			options.OutwardTicketRetailerInfo = outwardInfo;
			Assert.IsFalse( options.HasProcessedTicketRetailerInfoJourneyChanged );

			// Return one is not null
			options.OutwardTicketRetailerInfo = null;
			options.ReturnTicketRetailerInfo = inwardInfo;
			Assert.IsFalse( options.HasProcessedTicketRetailerInfoJourneyChanged );

			// Both are not null
			options.OutwardTicketRetailerInfo = outwardInfo;
			Assert.IsFalse( options.HasProcessedTicketRetailerInfoJourneyChanged );

			// Outward not null and id doesn't match
			options.ReturnTicketRetailerInfo = null;
			Assert.IsFalse( options.HasProcessedTicketRetailerInfoJourneyChanged );

			// Return not null and id doesn't match
			options.OutwardTicketRetailerInfo = null;
			options.ReturnTicketRetailerInfo = inwardInfo;
			Assert.IsFalse( options.HasProcessedTicketRetailerInfoJourneyChanged );

			// Now test after calling SetSelectedTicket
			options.SetSelectedTicket(unit, (Ticket)unit.SingleFares.Tickets[0]);
			Assert.IsTrue( options.HasProcessedTicketRetailerInfoJourneyChanged );

			options.OutwardTicketRetailerInfo = outwardInfo;
			options.ReturnTicketRetailerInfo = null;
			Assert.IsFalse( options.HasProcessedTicketRetailerInfoJourneyChanged );
			options.ClearSelectedTickets();
			options.SetSelectedTicket(unit, (Ticket)unit.SingleFares.Tickets[0]);
			Assert.IsTrue( options.HasProcessedTicketRetailerInfoJourneyChanged );

			options.OutwardTicketRetailerInfo = outwardInfo;
			options.ReturnTicketRetailerInfo = inwardInfo;
			Assert.IsFalse( options.HasProcessedTicketRetailerInfoJourneyChanged );
			options.ClearSelectedTickets();
			options.SetSelectedTicket(unit, (Ticket)unit.SingleFares.Tickets[0]);
			Assert.IsTrue( options.HasProcessedTicketRetailerInfoJourneyChanged );

			// Tests for OverrideItineraryType = Return
			options.OverrideItineraryType = ItineraryType.Return;
			options.ClearSelectedTickets();
			options.OutwardTicketRetailerInfo = outwardInfo;
			options.ReturnTicketRetailerInfo = null;
			Assert.IsFalse( options.HasProcessedTicketRetailerInfoJourneyChanged );

			// Now test after calling SetSelectedTicket
			options.SetSelectedTicket(unit, (Ticket)unit.SingleFares.Tickets[0]);
			Assert.IsTrue( options.HasProcessedTicketRetailerInfoJourneyChanged );

			options.OutwardTicketRetailerInfo = outwardInfo;
			Assert.IsFalse( options.HasProcessedTicketRetailerInfoJourneyChanged );
		}

		#endregion

		#region Helper methods

		/// <summary>
		/// Returns an Itinerary with Farees and Retailers initialised
		/// </summary>
		/// <returns></returns>
		private Itinerary GetItinerary(bool useReturn)
		{
			// Create an itinerary
			Itinerary itinerary;
			if (useReturn)	
				itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov);
			else
				itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, null);
			
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
