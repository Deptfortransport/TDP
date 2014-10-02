//********************************************************************************
//NAME         : TestNatExFaresSupplierPriceRoute.cs
//AUTHOR       : James Broome
//DATE CREATED : 02/03/2005
//DESCRIPTION  : Implementation of TestNatExFaresSupplierPriceRoute class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestNatExFaresSupplierPriceRoute.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:32   mturner
//Initial revision.
//
//   Rev 1.11   Apr 29 2005 12:03:10   jbroome
//Updated tests after removing FirstLegIndex and LastLegIndex properties from CoachPricingUnitFare class.
//
//   Rev 1.10   Apr 28 2005 13:59:40   jbroome
//Updated after changes in NatExFaresSupplier and CostSearchTicket classes
//Resolution for 2309: PT - Train - Destination wrong on rail ticket description.
//
//   Rev 1.9   Apr 25 2005 13:36:58   jbroome
//Updated after changes to PublicJourneyStore class
//
//   Rev 1.8   Apr 25 2005 10:11:10   jbroome
//Updated tests after changes to PriceRoute method
//
//   Rev 1.7   Apr 21 2005 13:41:40   jbroome
//Updated resource ids for error messages.
//
//   Rev 1.6   Apr 13 2005 14:15:30   jbroome
//Added TestLegsNotCovereredbyPricingUnit test method
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.5   Apr 08 2005 16:35:26   jbroome
//Change to CombinedTicketIndex property, added inward journeys to return CostSearchTickets
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.4   Apr 06 2005 18:00:34   jbroome
//Correct grouping of CostSearchTickets.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.3   Apr 05 2005 14:46:18   jbroome
//Fares now filtered by Discount Card.
//Updated test fixtures to reflect this change.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.2   Mar 30 2005 10:39:20   jbroome
//Setting / Testing TicketTypes on TravelDates
//
//   Rev 1.1   Mar 23 2005 15:48:52   jbroome
//Updated due to removal of TravelDates public property on NatExFaresSupplier.
//
//   Rev 1.0   Mar 23 2005 09:36:56   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;
using System.Collections;
using System.Configuration;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;

using NUnit.Framework;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// This class is concerned with testing the PriceRoute functionality
	/// of the NatExFaresSupplier class. This function performs several 
	/// complex operations which are entirely dependent on CJP data
	/// and therefore it was deemed necessary to seperate the testing of this 
	/// functionality from the main TestNatExFareSupplier test fixture.
	/// </summary>
	[TestFixture]
	public class TestNatExFaresSupplierPriceRoute
	{

		#region Private members

		NatExFaresSupplier supplier;
		CJPSessionInfo sessionInfo = new CJPSessionInfo();
		private const string propertiesFile1 = @"NatExFares\PriceRoute_Setup1_Properties.xml";
		private const string propertiesFile2 = @"NatExFares\PriceRoute_Setup2_Properties.xml";
		private const string propertiesFile3 = @"NatExFares\PriceRoute_Setup3_Properties.xml";
		private const string propertiesFile4 = @"NatExFares\PriceRoute_Setup4_Properties.xml";
		private const string propertiesFile5 = @"NatExFares\PriceRoute_Setup5_Properties.xml";
		private const string propertiesFile6 = @"NatExFares\PriceRoute_Setup6_Properties.xml";
		private const string propertiesFile7 = @"NatExFares\PriceRoute_Setup7_Properties.xml";
		private const string propertiesFile8 = @"NatExFares\PriceRoute_Setup8_Properties.xml";
		private const string propertiesFile9 = @"NatExFares\PriceRoute_Setup9_Properties.xml";

		private ArrayList travelDates1
		{
			get 
			{
				ArrayList travelDates = new ArrayList();
				travelDates.Add(new TravelDate(0, new TDDateTime(2005, 01, 01), new TDDateTime(2005, 01, 01), TicketTravelMode.Coach, 0, 0, true));
				travelDates.Add(new TravelDate(1, new TDDateTime(2005, 01, 01), new TDDateTime(2005, 01, 02), TicketTravelMode.Coach, 0, 0, true));
				travelDates.Add(new TravelDate(2, new TDDateTime(2005, 01, 02), new TDDateTime(2005, 01, 01), TicketTravelMode.Coach, 0, 0, true));
				travelDates.Add(new TravelDate(3, new TDDateTime(2005, 01, 02), new TDDateTime(2005, 01, 02), TicketTravelMode.Coach, 0, 0, true));
				foreach (TravelDate travelDate in travelDates)
				{
					travelDate.TicketType = TicketType.Return;
				}
				return travelDates;
			}
		}
		
		private ArrayList travelDates2
		{
			get 
			{
				ArrayList travelDates = new ArrayList();
				travelDates.Add(new TravelDate(0, new TDDateTime(2005, 01, 01), new TDDateTime(2005, 01, 01), TicketTravelMode.Coach, 0, 0, true));
				foreach (TravelDate travelDate in travelDates)
				{
					travelDate.TicketType = TicketType.Return;
				}
				return travelDates;
			}
		}

		private ArrayList travelDates3
		{
			get 
			{
				ArrayList travelDates = new ArrayList();
				travelDates.Add(new TravelDate(0, new TDDateTime(2005, 01, 01), TicketTravelMode.Coach, 0, 0, true));
				foreach (TravelDate travelDate in travelDates)
				{
					travelDate.TicketType = TicketType.OpenReturn;
				}
				return travelDates;
			}
		}

		// Langstring resource IDs for error messages
		private const string PriceRouteNoResults = "CostSearchError.NoFaresResults";
		private const string PriceRouteNoFares = "CostSearchError.FaresInternalError";
	
		#endregion

		#region Constructor and initialisation

		/// <summary>
		/// Constructor
		/// </summary>
		public TestNatExFaresSupplierPriceRoute()
		{
			
		}

		/// <summary>
		/// Deliberately NOT marked with [SetUp] as it needs to be called
		/// explicitly with property file name parameter to set up CJP stub
		/// </summary>
		public bool SetUp(string fileName)
		{
			try
			{
				TDServiceDiscovery.ResetServiceDiscoveryForTest();
				// Initialise property service etc. 
				// And Mock CJP service, based on properties in fileName
				TDServiceDiscovery.Init(new TestNatExFaresInitialisation(fileName));
				supplier = new NatExFaresSupplier();
				sessionInfo.IsLoggedOn = false;
				sessionInfo.Language = "en-GB";
				sessionInfo.SessionId = "SESSIONID";
				sessionInfo.UserType = 2;				
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region TestOutwardFareDates

		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class and tests
		/// that the correct CoachJourneyFares are added to the OutwardFareDates, 
		/// grouped into the correct CoachJourneyFareSummary collections.
		/// </summary>
		[Test]
		public void TestOutwardFareDates()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile1), "SetUp failed for TestOutwardFareDates()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			// All other discount card fares will be ignored
			discounts.CoachDiscount = "Young Person";

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates1, origin, destination, discounts, sessionInfo, out journeyStore);
			// Check that no errors were returned
			Assert.AreEqual (0, errors.Length, "PriceRoute call returned errors in TestFareDateProperties()");
	
			// OutwardFareDates
			// Check that only distinct dates have been used
			Assert.AreEqual(2, supplier.OutwardFareDates.Length, "Error with OutwardFareDates property in TestFareDateProperties()");	
			Assert.AreEqual(new TDDateTime(2005, 01, 01), supplier.OutwardFareDates[0].Date, "Error with Date property of first OutwardFareDate");	
			Assert.AreEqual(new TDDateTime(2005, 01, 02), supplier.OutwardFareDates[1].Date, "Error with Date property of second OutwardFareDate");	
			Assert.AreEqual(12, supplier.OutwardFareDates[0].FareSummary.Length, "Error with FareSummary collection of first OutwardFareDate");	
			Assert.AreEqual(1, supplier.OutwardFareDates[1].FareSummary.Length, "Error with FareSummary collection of second OutwardFareDate");	
		
			// Test public properties of CoachJourneyFareSummary objects
			for (int i=0; i<supplier.OutwardFareDates[0].FareSummary.Length; i++)
			{
				Assert.AreEqual("900057366", supplier.OutwardFareDates[0].FareSummary[i].OriginNaptan, string.Format("OriginNaptan property not set correctly. FareSummary[{0}]", i));
				Assert.AreEqual("900066177", supplier.OutwardFareDates[0].FareSummary[i].DestinationNaptan, string.Format("DestinationNaptan property not set correctly. FareSummary[{0}]", i));
				if (i<7 || i==11)
					Assert.AreEqual(1, supplier.OutwardFareDates[0].FareSummary[i].JourneyFares.Length, string.Format("JourneyFares.Length property incorrect. FareSummary[{0}]", i));
				else 
					Assert.AreEqual(2, supplier.OutwardFareDates[0].FareSummary[i].JourneyFares.Length, string.Format("JourneyFares.Length property incorrect. FareSummary[{0}]", i));

				if (i==9 || i==10)
					Assert.AreEqual(2, supplier.OutwardFareDates[0].FareSummary[i].Journeys.Length, string.Format("Journeys.Length property incorrect. FareSummary[{0}]", i));
				else
					Assert.AreEqual(1, supplier.OutwardFareDates[0].FareSummary[i].Journeys.Length, string.Format("Journeys.Length property incorrect. FareSummary[{0}]", i));
			}
            
			CoachJourneyFareDate fareDate = supplier.OutwardFareDates[0];

			// Test the 18 CoachJourneyFares
			Assert.AreEqual(true, fareDate.FareSummary[0].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 1");
			Assert.AreEqual(true, fareDate.FareSummary[0].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 1");
			Assert.AreEqual(false, fareDate.FareSummary[0].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 1");
			Assert.AreEqual(45, fareDate.FareSummary[0].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 1");

			Assert.AreEqual(true, fareDate.FareSummary[1].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 2");
			Assert.AreEqual(true, fareDate.FareSummary[1].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 2");
			Assert.AreEqual(false, fareDate.FareSummary[1].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 2");
			Assert.AreEqual(65, fareDate.FareSummary[1].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 2");

			Assert.AreEqual(false, fareDate.FareSummary[2].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 3");
			Assert.AreEqual(true, fareDate.FareSummary[2].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 3");
			Assert.AreEqual(false, fareDate.FareSummary[2].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 3");
			Assert.AreEqual(38, fareDate.FareSummary[2].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 3");

			Assert.AreEqual(true, fareDate.FareSummary[3].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 4");
			Assert.AreEqual(false, fareDate.FareSummary[3].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 4");
			Assert.AreEqual(false, fareDate.FareSummary[3].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 4");
			Assert.AreEqual(75, fareDate.FareSummary[3].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 4");
			
			Assert.AreEqual(true, fareDate.FareSummary[4].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 5");
			Assert.AreEqual(true, fareDate.FareSummary[4].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 5");
			Assert.AreEqual(false, fareDate.FareSummary[4].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 5");
			Assert.AreEqual(46, fareDate.FareSummary[4].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 5");

			Assert.AreEqual(true, fareDate.FareSummary[5].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 6");
			Assert.AreEqual(true, fareDate.FareSummary[5].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 6");
			Assert.AreEqual(true, fareDate.FareSummary[5].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 6");
			Assert.AreEqual(42, fareDate.FareSummary[5].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 6");

			Assert.AreEqual(true, fareDate.FareSummary[6].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 7");
			Assert.AreEqual(true, fareDate.FareSummary[6].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 7");
			Assert.AreEqual(false, fareDate.FareSummary[6].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 7");
			Assert.AreEqual(51, fareDate.FareSummary[6].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 7");

			Assert.AreEqual(true, fareDate.FareSummary[7].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 8");
			Assert.AreEqual(true, fareDate.FareSummary[7].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 8");
			Assert.AreEqual(false, fareDate.FareSummary[7].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 8");
			Assert.AreEqual(45, fareDate.FareSummary[7].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 8");

			Assert.AreEqual(false, fareDate.FareSummary[7].JourneyFares[1].IsAdult, "IsAdult: CoachJourneyFare 9");
			Assert.AreEqual(true, fareDate.FareSummary[7].JourneyFares[1].IsSingle, "IsSingle: CoachJourneyFare 9");
			Assert.AreEqual(false, fareDate.FareSummary[7].JourneyFares[1].IsDiscounted, "IsDiscounted: CoachJourneyFare 9");
			Assert.AreEqual(33, fareDate.FareSummary[7].JourneyFares[1].TotalAmount, "TotalAmount: CoachJourneyFare 9");

			Assert.AreEqual(true, fareDate.FareSummary[8].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 10");
			Assert.AreEqual(true, fareDate.FareSummary[8].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 10");
			Assert.AreEqual(false, fareDate.FareSummary[8].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 10");
			Assert.AreEqual(55, fareDate.FareSummary[8].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 10");

			Assert.AreEqual(false, fareDate.FareSummary[8].JourneyFares[1].IsAdult, "IsAdult: CoachJourneyFare 11");
			Assert.AreEqual(true, fareDate.FareSummary[8].JourneyFares[1].IsSingle, "IsSingle: CoachJourneyFare 11");
			Assert.AreEqual(false, fareDate.FareSummary[8].JourneyFares[1].IsDiscounted, "IsDiscounted: CoachJourneyFare 11");
			Assert.AreEqual(38, fareDate.FareSummary[8].JourneyFares[1].TotalAmount, "TotalAmount: CoachJourneyFare 11");

			Assert.AreEqual(true, fareDate.FareSummary[9].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 12");
			Assert.AreEqual(false, fareDate.FareSummary[9].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 12");
			Assert.AreEqual(false, fareDate.FareSummary[9].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 12");
			Assert.AreEqual(60, fareDate.FareSummary[9].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 12");

			Assert.AreEqual(false, fareDate.FareSummary[9].JourneyFares[1].IsAdult, "IsAdult: CoachJourneyFare 13");
			Assert.AreEqual(false, fareDate.FareSummary[9].JourneyFares[1].IsSingle, "IsSingle: CoachJourneyFare 13");
			Assert.AreEqual(false, fareDate.FareSummary[9].JourneyFares[1].IsDiscounted, "IsDiscounted: CoachJourneyFare 13");
			Assert.AreEqual(47, fareDate.FareSummary[9].JourneyFares[1].TotalAmount, "TotalAmount: CoachJourneyFare 13");

			Assert.AreEqual(true, fareDate.FareSummary[10].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 14");
			Assert.AreEqual(false, fareDate.FareSummary[10].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 14");
			Assert.AreEqual(false, fareDate.FareSummary[10].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 14");
			Assert.AreEqual(68, fareDate.FareSummary[10].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 14");

			Assert.AreEqual(false, fareDate.FareSummary[10].JourneyFares[1].IsAdult, "IsAdult: CoachJourneyFare 15");
			Assert.AreEqual(false, fareDate.FareSummary[10].JourneyFares[1].IsSingle, "IsSingle: CoachJourneyFare 15");
			Assert.AreEqual(false, fareDate.FareSummary[10].JourneyFares[1].IsDiscounted, "IsDiscounted: CoachJourneyFare 15");
			Assert.AreEqual(52, fareDate.FareSummary[10].JourneyFares[1].TotalAmount, "TotalAmount: CoachJourneyFare 15");

			Assert.AreEqual(true, fareDate.FareSummary[11].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 16");
			Assert.AreEqual(true, fareDate.FareSummary[11].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 16");
			Assert.AreEqual(false, fareDate.FareSummary[11].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 16");
			Assert.AreEqual(65, fareDate.FareSummary[11].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 16");

			// Test selection of the CoachFarePricingUnits...

			Assert.AreEqual("900057366", fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].StartLocationNaptan, "StartLocationNaptan property");
			Assert.AreEqual("900069092", fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].EndLocationNaptan, "EndLocationNaptan property");
			Assert.AreEqual(string.Empty, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].DiscountCardType, "DiscountCardType property");
			Assert.AreEqual(45, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].FareAmount, "FareAmount property");
			Assert.AreEqual("Standard Single", fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].FareType, "FareType property");
			Assert.AreEqual("Unknown", fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].FareTypeConditions, "FareTypeConditions property");
			Assert.AreEqual(Flexibility.FullyFlexible, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].Flexibility, "Flexibility property");
			Assert.AreEqual(15, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].MaxChildFare, "MaxChildFare property");
			Assert.AreEqual(3, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].MinChildFare, "MinChildFare property");
			Assert.AreEqual(0, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[0].PricingUnitIndex, "PricingUnitIndex property");

			Assert.AreEqual("900057366", fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].StartLocationNaptan, "StartLocationNaptan property");
			Assert.AreEqual("900076052", fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].EndLocationNaptan, "EndLocationNaptan property");
			Assert.AreEqual("Young Person", fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].DiscountCardType, "DiscountCardType property");
			Assert.AreEqual(15, fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].FareAmount, "FareAmount property");
			Assert.AreEqual("Young Person Single", fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].FareType, "FareType property");
			Assert.AreEqual("Unknown", fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].FareTypeConditions, "FareTypeConditions property");
			Assert.AreEqual(Flexibility.FullyFlexible, fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].Flexibility, "Flexibility property");
			Assert.AreEqual(15, fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].MaxChildFare, "MaxChildFare property");
			Assert.AreEqual(3, fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].MinChildFare, "MinChildFare property");
			Assert.AreEqual(0, fareDate.FareSummary[5].JourneyFares[0].PricingUnitFares[0].PricingUnitIndex, "PricingUnitIndex property");

			Assert.AreEqual("900057366", fareDate.FareSummary[9].JourneyFares[1].PricingUnitFares[0].StartLocationNaptan, "StartLocationNaptan property");
			Assert.AreEqual("900066177", fareDate.FareSummary[9].JourneyFares[1].PricingUnitFares[0].EndLocationNaptan, "EndLocationNaptan property");
			Assert.AreEqual(string.Empty, fareDate.FareSummary[9].JourneyFares[1].PricingUnitFares[0].DiscountCardType, "DiscountCardType property");
			Assert.AreEqual(47, fareDate.FareSummary[9].JourneyFares[1].PricingUnitFares[0].FareAmount, "FareAmount property");
			Assert.AreEqual("Economy Return", fareDate.FareSummary[9].JourneyFares[1].PricingUnitFares[0].FareType, "FareType property");
			Assert.AreEqual("Unknown", fareDate.FareSummary[9].JourneyFares[1].PricingUnitFares[0].FareTypeConditions, "FareTypeConditions property");
			Assert.AreEqual(Flexibility.NoFlexibility, fareDate.FareSummary[9].JourneyFares[0].PricingUnitFares[0].Flexibility, "Flexibility property");
			Assert.AreEqual(15, fareDate.FareSummary[9].JourneyFares[1].PricingUnitFares[0].MaxChildFare, "MaxChildFare property");
			Assert.AreEqual(3, fareDate.FareSummary[9].JourneyFares[1].PricingUnitFares[0].MinChildFare, "MinChildFare property");
			Assert.AreEqual(0, fareDate.FareSummary[9].JourneyFares[1].PricingUnitFares[0].PricingUnitIndex, "PricingUnitIndex property");

			Assert.AreEqual("900069092", fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].StartLocationNaptan, "StartLocationNaptan property");
			Assert.AreEqual("900066177", fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].EndLocationNaptan, "EndLocationNaptan property");
			Assert.AreEqual(string.Empty, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].DiscountCardType, "DiscountCardType property");
			Assert.AreEqual(18, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].FareAmount, "FareAmount property");
			Assert.AreEqual("Advance Single", fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].FareType, "FareType property");
			Assert.AreEqual("Unknown", fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].FareTypeConditions, "FareTypeConditions property");
			Assert.AreEqual(Flexibility.LimitedFlexibility, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].Flexibility, "Flexibility property");
			Assert.AreEqual(15, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].MaxChildFare, "MaxChildFare property");
			Assert.AreEqual(4, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].MinChildFare, "MinChildFare property");
			Assert.AreEqual(1, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[1].PricingUnitIndex, "PricingUnitIndex property");
		
		}

		#endregion

		#region TestInwardFareDates

		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class and tests
		/// that the correct CoachJourneyFares are added to the InwardFareDates, 
		/// grouped into the correct CoachJourneyFareSummary collections.
		/// </summary>
		[Test]
		public void TestInwardFareDates()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile1), "SetUp failed for TestInwardFareDates()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = "Young Person";

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates1, origin, destination, discounts, sessionInfo, out journeyStore);
			// Check that no errors were returned
			Assert.AreEqual (0, errors.Length, "PriceRoute call returned errors in TestInwardFareDates()");
	
			// InwardFareDates
			// Check that only distinct dates have been used
			Assert.AreEqual(2, supplier.InwardFareDates.Length, "Error with InwardFareDates property in TestInwardFareDates()");	
			Assert.AreEqual(new TDDateTime(2005, 01, 01), supplier.InwardFareDates[0].Date, "Error with Date property of first InwardFareDate");	
			Assert.AreEqual(new TDDateTime(2005, 01, 02), supplier.InwardFareDates[1].Date, "Error with Date property of second InwardFareDate");	
			Assert.AreEqual(5, supplier.InwardFareDates[0].FareSummary.Length, "Error with FareSummary collection of first InwardFareDate");	
			Assert.AreEqual(0, supplier.InwardFareDates[1].FareSummary.Length, "Error with FareSummary collection of second InwardFareDate");	

			// Test public properties of CoachJourneyFareSummary objects
			for (int i=0; i<supplier.InwardFareDates[0].FareSummary.Length; i++)
			{
				Assert.AreEqual("900066177", supplier.InwardFareDates[0].FareSummary[i].OriginNaptan, string.Format("OriginNaptan property not set correctly. FareSummary[{0}]", i));
				Assert.AreEqual("900057366", supplier.InwardFareDates[0].FareSummary[i].DestinationNaptan, string.Format("DestinationNaptan property not set correctly. FareSummary[{0}]", i));
				if (i<3)
					Assert.AreEqual(1, supplier.InwardFareDates[0].FareSummary[i].JourneyFares.Length, string.Format("JourneyFares.Length property incorrect. FareSummary[{0}]", i));
				else 
					Assert.AreEqual(2, supplier.InwardFareDates[0].FareSummary[i].JourneyFares.Length, string.Format("JourneyFares.Length property incorrect. FareSummary[{0}]", i));
				
				Assert.AreEqual(1, supplier.InwardFareDates[0].FareSummary[i].Journeys.Length, string.Format("Journeys.Length property incorrect. FareSummary[{0}]", i));
			}
            
			CoachJourneyFareDate fareDate = supplier.InwardFareDates[0];

			// Test the 7 CoachJourneyFares
			Assert.AreEqual(true, fareDate.FareSummary[0].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 1");
			Assert.AreEqual(true, fareDate.FareSummary[0].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 1");
			Assert.AreEqual(false, fareDate.FareSummary[0].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 1");
			Assert.AreEqual(46, fareDate.FareSummary[0].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 1");

			Assert.AreEqual(true, fareDate.FareSummary[1].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 2");
			Assert.AreEqual(true, fareDate.FareSummary[1].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 2");
			Assert.AreEqual(false, fareDate.FareSummary[1].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 2");
			Assert.AreEqual(51, fareDate.FareSummary[1].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 2");

			Assert.AreEqual(true, fareDate.FareSummary[2].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 3");
			Assert.AreEqual(true, fareDate.FareSummary[2].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 3");
			Assert.AreEqual(true, fareDate.FareSummary[2].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 3");
			Assert.AreEqual(42, fareDate.FareSummary[2].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 3");

			Assert.AreEqual(true, fareDate.FareSummary[3].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 4");
			Assert.AreEqual(true, fareDate.FareSummary[3].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 4");
			Assert.AreEqual(false, fareDate.FareSummary[3].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 4");
			Assert.AreEqual(45, fareDate.FareSummary[3].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 4");

			Assert.AreEqual(false, fareDate.FareSummary[3].JourneyFares[1].IsAdult, "IsAdult: CoachJourneyFare 5");
			Assert.AreEqual(true, fareDate.FareSummary[3].JourneyFares[1].IsSingle, "IsSingle: CoachJourneyFare 5");
			Assert.AreEqual(false, fareDate.FareSummary[3].JourneyFares[1].IsDiscounted, "IsDiscounted: CoachJourneyFare 5");
			Assert.AreEqual(33, fareDate.FareSummary[3].JourneyFares[1].TotalAmount, "TotalAmount: CoachJourneyFare 5");

			Assert.AreEqual(true, fareDate.FareSummary[4].JourneyFares[0].IsAdult, "IsAdult: CoachJourneyFare 6");
			Assert.AreEqual(true, fareDate.FareSummary[4].JourneyFares[0].IsSingle, "IsSingle: CoachJourneyFare 6");
			Assert.AreEqual(false, fareDate.FareSummary[4].JourneyFares[0].IsDiscounted, "IsDiscounted: CoachJourneyFare 6");
			Assert.AreEqual(55, fareDate.FareSummary[4].JourneyFares[0].TotalAmount, "TotalAmount: CoachJourneyFare 6");

			Assert.AreEqual(false, fareDate.FareSummary[4].JourneyFares[1].IsAdult, "IsAdult: CoachJourneyFare 7");
			Assert.AreEqual(true, fareDate.FareSummary[4].JourneyFares[1].IsSingle, "IsSingle: CoachJourneyFare 7");
			Assert.AreEqual(false, fareDate.FareSummary[4].JourneyFares[1].IsDiscounted, "IsDiscounted: CoachJourneyFare 7");
			Assert.AreEqual(38, fareDate.FareSummary[4].JourneyFares[1].TotalAmount, "TotalAmount: CoachJourneyFare 7");

			// Test selection of the CoachFarePricingUnits...

			Assert.AreEqual("900069092", fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].StartLocationNaptan, "StartLocationNaptan property");
			Assert.AreEqual("900076052", fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].EndLocationNaptan, "EndLocationNaptan property");
			Assert.AreEqual(string.Empty, fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].DiscountCardType, "DiscountCardType property");
			Assert.AreEqual(5, fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].FareAmount, "FareAmount property");
			Assert.AreEqual("Open Single", fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].FareType, "FareType property");
			Assert.AreEqual("Unknown", fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].FareTypeConditions, "FareTypeConditions property");
			Assert.AreEqual(Flexibility.FullyFlexible, fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].Flexibility, "Flexibility property");
			Assert.AreEqual(16, fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].MaxChildFare, "MaxChildFare property");
			Assert.AreEqual(5, fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].MinChildFare, "MinChildFare property");
			Assert.AreEqual(1, fareDate.FareSummary[0].JourneyFares[0].PricingUnitFares[1].PricingUnitIndex, "PricingUnitIndex property");

			Assert.AreEqual("900069092", fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].StartLocationNaptan, "StartLocationNaptan property");
			Assert.AreEqual("900076052", fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].EndLocationNaptan, "EndLocationNaptan property");
			Assert.AreEqual(string.Empty, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].DiscountCardType, "DiscountCardType property");
			Assert.AreEqual(10, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].FareAmount, "FareAmount property");
			Assert.AreEqual("First Class Single", fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].FareType, "FareType property");
			Assert.AreEqual("Unknown", fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].FareTypeConditions, "FareTypeConditions property");
			Assert.AreEqual(Flexibility.FullyFlexible, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].Flexibility, "Flexibility property");
			Assert.AreEqual(16, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].MaxChildFare, "MaxChildFare property");
			Assert.AreEqual(5, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].MinChildFare, "MinChildFare property");
			Assert.AreEqual(1, fareDate.FareSummary[1].JourneyFares[0].PricingUnitFares[1].PricingUnitIndex, "PricingUnitIndex property");

			Assert.AreEqual("900076052", fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].StartLocationNaptan, "StartLocationNaptan property");
			Assert.AreEqual("900057366", fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].EndLocationNaptan, "EndLocationNaptan property");
			Assert.AreEqual("Young Person", fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].DiscountCardType, "DiscountCardType property");
			Assert.AreEqual(15, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].FareAmount, "FareAmount property");
			Assert.AreEqual("Young Person Single", fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].FareType, "FareType property");
			Assert.AreEqual("Unknown", fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].FareTypeConditions, "FareTypeConditions property");
			Assert.AreEqual(Flexibility.FullyFlexible, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].Flexibility, "Flexibility property");
			Assert.AreEqual(15, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].MaxChildFare, "MaxChildFare property");
			Assert.AreEqual(3, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].MinChildFare, "MinChildFare property");
			Assert.AreEqual(2, fareDate.FareSummary[2].JourneyFares[0].PricingUnitFares[2].PricingUnitIndex, "PricingUnitIndex property");

			Assert.AreEqual("900066177", fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].StartLocationNaptan, "StartLocationNaptan property");
			Assert.AreEqual("900057366", fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].EndLocationNaptan, "EndLocationNaptan property");
			Assert.AreEqual(string.Empty, fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].DiscountCardType, "DiscountCardType property");
			Assert.AreEqual(33, fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].FareAmount, "FareAmount property");
			Assert.AreEqual("Economy Single", fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].FareType, "FareType property");
			Assert.AreEqual("Unknown", fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].FareTypeConditions, "FareTypeConditions property");
			Assert.AreEqual(Flexibility.LimitedFlexibility, fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].Flexibility, "Flexibility property");
			Assert.AreEqual(15, fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].MaxChildFare, "MaxChildFare property");
			Assert.AreEqual(3, fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].MinChildFare, "MinChildFare property");
			Assert.AreEqual(0, fareDate.FareSummary[3].JourneyFares[1].PricingUnitFares[0].PricingUnitIndex, "PricingUnitIndex property");

			Assert.AreEqual("900066177", fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].StartLocationNaptan, "StartLocationNaptan property");
			Assert.AreEqual("900057366", fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].EndLocationNaptan, "EndLocationNaptan property");
			Assert.AreEqual(string.Empty, fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].DiscountCardType, "DiscountCardType property");
			Assert.AreEqual(55, fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].FareAmount, "FareAmount property");
			Assert.AreEqual("Standard Single", fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].FareType, "FareType property");
			Assert.AreEqual("Unknown", fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].FareTypeConditions, "FareTypeConditions property");
			Assert.AreEqual(Flexibility.FullyFlexible, fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].Flexibility, "Flexibility property");
			Assert.AreEqual(15, fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].MaxChildFare, "MaxChildFare property");
			Assert.AreEqual(3, fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].MinChildFare, "MinChildFare property");
			Assert.AreEqual(0, fareDate.FareSummary[4].JourneyFares[0].PricingUnitFares[0].PricingUnitIndex, "PricingUnitIndex property");

		}


		#endregion
		
		#region TestTravelDates

		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class and tests
		/// that the public properties of the TravelDates have been set correctly.
		/// </summary>
		[Test]
		public void TestTravelDates() 
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile1), "SetUp failed for TestTravelDateProperties()");

			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates  = travelDates1;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
			// Check that no errors were returned
			Assert.AreEqual (0, errors.Length, "PriceRoute call returned errors in TestTravelDateProperties()");

			// Check travelDates array list - should have increased in size as duplicates 
			// are created for TicketType Single and Singles
			Assert.AreEqual (8, travelDates.Count, "PriceRoute call has not increased travelDates arrray list");

			// Test properties of first TravelDate
			Assert.AreEqual(TicketType.Return, ((TravelDate)travelDates[0]).TicketType, "TicketType not set correctly for first TravelDate");
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).Index, "Index fare not set correctly for first TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[0]).MinAdultFare, "Min Adult fare set incorrectly for first TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[0]).MaxAdultFare, "Max Adult fare set incorrectly for first TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[0]).MinChildFare, "Min Child fare set incorrectly for first TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[0]).MaxChildFare, "Max Child fare set incorrectly for first TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,01), ((TravelDate)travelDates[0]).OutwardDate, "OutwardDate not set correctly for first TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,01), ((TravelDate)travelDates[0]).ReturnDate, "ReturnDate not set correctly for first TravelDate");

			// Test properties of second TravelDate
			Assert.AreEqual(TicketType.Return, ((TravelDate)travelDates[1]).TicketType, "TicketType not set correctly for second TravelDate");
			Assert.AreEqual(1, ((TravelDate)travelDates[1]).Index, "Index fare not set correctly for second TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[1]).MinAdultFare, "Min Adult fare set incorrectly for second TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[1]).MaxAdultFare, "Max Adult fare set incorrectly for second TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[1]).MinChildFare, "Min Child fare set incorrectly for second TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[1]).MaxChildFare, "Max Child fare set incorrectly for second TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,01), ((TravelDate)travelDates[1]).OutwardDate, "OutwardDate not set correctly for second TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,02), ((TravelDate)travelDates[1]).ReturnDate, "ReturnDate not set correctly for second TravelDate");

			// Test properties of third TravelDate
			Assert.AreEqual(TicketType.Return, ((TravelDate)travelDates[2]).TicketType, "TicketType not set correctly for third TravelDate");
			Assert.AreEqual(2, ((TravelDate)travelDates[2]).Index, "Index fare not set correctly for third TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[2]).MinAdultFare, "Min Adult fare set incorrectly for third TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[2]).MaxAdultFare, "Max Adult fare set incorrectly for third TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[2]).MinChildFare, "Min Child fare set incorrectly for third TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[2]).MaxChildFare, "Max Child fare set incorrectly for third TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,02), ((TravelDate)travelDates[2]).OutwardDate, "OutwardDate not set correctly for third TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,01), ((TravelDate)travelDates[2]).ReturnDate, "ReturnDate not set correctly for third TravelDate");

			// Test properties of fourth TravelDate
			Assert.AreEqual(TicketType.Return, ((TravelDate)travelDates[3]).TicketType, "TicketType not set correctly for fourth TravelDate");
			Assert.AreEqual(3, ((TravelDate)travelDates[3]).Index, "Index fare not set correctly for fourth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[3]).MinAdultFare, "Min Adult fare set incorrectly for fourth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[3]).MaxAdultFare, "Max Adult fare set incorrectly for fourth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[3]).MinChildFare, "Min Child fare set incorrectly for fourth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[3]).MaxChildFare, "Max Child fare set incorrectly for fourth TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,02), ((TravelDate)travelDates[3]).OutwardDate, "OutwardDate not set correctly for fourth TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,02), ((TravelDate)travelDates[3]).ReturnDate, "ReturnDate not set correctly for fourth TravelDate");

			// Test properties of fith TravelDate
			Assert.AreEqual(TicketType.Singles, ((TravelDate)travelDates[4]).TicketType, "TicketType not set correctly for fifth TravelDate");
			Assert.AreEqual(4, ((TravelDate)travelDates[4]).Index, "Index fare not set correctly for fifth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[4]).MinAdultFare, "Min Adult fare set incorrectly for fifth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[4]).MaxAdultFare, "Max Adult fare set incorrectly for fifth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[4]).MinChildFare, "Min Child fare set incorrectly for fifth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[4]).MaxChildFare, "Max Child fare set incorrectly for fifth TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,01), ((TravelDate)travelDates[4]).OutwardDate, "OutwardDate not set correctly for fifth TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,01), ((TravelDate)travelDates[4]).ReturnDate, "ReturnDate not set correctly for fifth TravelDate");

			// Test properties of sixth TravelDate
			Assert.AreEqual(TicketType.Singles, ((TravelDate)travelDates[5]).TicketType, "TicketType not set correctly for sixth TravelDate");
			Assert.AreEqual(5, ((TravelDate)travelDates[5]).Index, "Index fare not set correctly for sixth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[5]).MinAdultFare, "Min Adult fare set incorrectly for sixth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[5]).MaxAdultFare, "Max Adult fare set incorrectly for sixth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[5]).MinChildFare, "Min Child fare set incorrectly for sixth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[5]).MaxChildFare, "Max Child fare set incorrectly for sixth TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,01), ((TravelDate)travelDates[5]).OutwardDate, "OutwardDate not set correctly for sixth TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,02), ((TravelDate)travelDates[5]).ReturnDate, "ReturnDate not set correctly for sixth TravelDate");

			// Test properties of seventh TravelDate
			Assert.AreEqual(TicketType.Singles, ((TravelDate)travelDates[6]).TicketType, "TicketType not set correctly for seventh TravelDate");
			Assert.AreEqual(6, ((TravelDate)travelDates[6]).Index, "Index fare not set correctly for seventh TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[6]).MinAdultFare, "Min Adult fare set incorrectly for seventh TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[6]).MaxAdultFare, "Max Adult fare set incorrectly for seventh TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[6]).MinChildFare, "Min Child fare set incorrectly for seventh TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[6]).MaxChildFare, "Max Child fare set incorrectly for seventh TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,02), ((TravelDate)travelDates[6]).OutwardDate, "OutwardDate not set correctly for seventh TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,01), ((TravelDate)travelDates[6]).ReturnDate, "ReturnDate not set correctly for seventh TravelDate");

			// Test properties of eighth TravelDate
			Assert.AreEqual(TicketType.Singles, ((TravelDate)travelDates[7]).TicketType, "TicketType not set correctly for eighth TravelDate");
			Assert.AreEqual(7, ((TravelDate)travelDates[7]).Index, "Index fare not set correctly for eighth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[7]).MinAdultFare, "Min Adult fare set incorrectly for eighth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[7]).MaxAdultFare, "Max Adult fare set incorrectly for eighth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[7]).MinChildFare, "Min Child fare set incorrectly for eighth TravelDate");
			Assert.AreEqual(float.NaN, ((TravelDate)travelDates[7]).MaxChildFare, "Max Child fare set incorrectly for eighth TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,02), ((TravelDate)travelDates[7]).OutwardDate, "OutwardDate not set correctly for eighth TravelDate");
			Assert.AreEqual(new TDDateTime(2005,01,02), ((TravelDate)travelDates[7]).ReturnDate, "ReturnDate not set correctly for eighth TravelDate");

		}

		#endregion

		#region TestTravelDateOutwardTickets

		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class and tests
		/// that the correct CostSearchTickets are added to the OutwardTickets
		/// collection of a TravelDate. Also checks that the properties of
		/// each CostSearchTicket have been set correctly.
		/// </summary>
		[Test]
		public void TestTravelDateOutwardTickets()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile1), "SetUp failed for TestTravelDateOutwardTickets()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			// Discounted fares from other discount cards will be ignored			
			discounts.CoachDiscount = "Frequent Traveller";
			ArrayList travelDates = travelDates1;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
			// Check that no errors were returned
			Assert.AreEqual (0, errors.Length, "PriceRoute call returned errors in TestTravelDateOutwardTickets()");

			// Test that CostSearchTicket collections have been created successfully for fourth TravelDate
			Assert.AreEqual(19, ((TravelDate)travelDates[4]).OutwardTickets.Length, "Outward tickets collection not set correctly");
			
			for (int i=0; i<19; i++) 
			{
				// The TravelDateForTicket property will always be the same
				Assert.AreEqual(((TravelDate)travelDates[4]), ((TravelDate)travelDates[4]).OutwardTickets[i].TravelDateForTicket, string.Format("TravelDateForTicket property not set correctly. CostSearchTicket {0}.", i));
			}
			
			// Test the CostSearchTickets		
			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[0].MinChildAge, "MinChildAge property. CostSearchTicket 1");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[0].MaxChildAge, "MaxChildAge property. CostSearchTicket 1");
			Assert.AreEqual(1, ((TravelDate)travelDates[4]).OutwardTickets[0].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 1");
			Assert.AreEqual(30, ((TravelDate)travelDates[4]).OutwardTickets[0].AdultFare, "AdultFare property. CostSearchTicket 1");
			Assert.AreEqual(Flexibility.LimitedFlexibility, ((TravelDate)travelDates[4]).OutwardTickets[0].Flexibility, "Flexibility property. CostSearchTicket 1");

			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[1].MinChildAge, "MinChildAge property. CostSearchTicket 2");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[1].MaxChildAge, "MaxChildAge property. CostSearchTicket 2");
			Assert.AreEqual(1, ((TravelDate)travelDates[4]).OutwardTickets[1].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 2");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[1].AdultFare, "AdultFare property. CostSearchTicket 2");
			Assert.AreEqual(Flexibility.LimitedFlexibility, ((TravelDate)travelDates[4]).OutwardTickets[1].Flexibility, "Flexibility property. CostSearchTicket 2");
			
			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[2].MinChildAge, "MinChildAge property. CostSearchTicket 3");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[2].MaxChildAge, "MaxChildAge property. CostSearchTicket 3");
			Assert.AreEqual(2, ((TravelDate)travelDates[4]).OutwardTickets[2].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 3");
			Assert.AreEqual(45, ((TravelDate)travelDates[4]).OutwardTickets[2].AdultFare, "AdultFare property. CostSearchTicket 3");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[2].Flexibility, "Flexibility property. CostSearchTicket 3");

			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[3].MinChildAge, "MinChildAge property. CostSearchTicket 4");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[3].MaxChildAge, "MaxChildAge property. CostSearchTicket 4");
			Assert.AreEqual(2, ((TravelDate)travelDates[4]).OutwardTickets[3].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 4");
			Assert.AreEqual(20, ((TravelDate)travelDates[4]).OutwardTickets[3].AdultFare, "AdultFare property. CostSearchTicket 4");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[3].Flexibility, "Flexibility property. CostSearchTicket 4");
			
			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[4].MinChildAge, "MinChildAge property. CostSearchTicket 5");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[4].MaxChildAge, "MaxChildAge property. CostSearchTicket 5");
			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[4].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 5");
			Assert.AreEqual(20, ((TravelDate)travelDates[4]).OutwardTickets[4].ChildFare, "ChildFare property. CostSearchTicket 5");
			Assert.AreEqual(Flexibility.LimitedFlexibility, ((TravelDate)travelDates[4]).OutwardTickets[4].Flexibility, "Flexibility property. CostSearchTicket 5");

			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[5].MinChildAge, "MinChildAge property. CostSearchTicket 6");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[5].MaxChildAge, "MaxChildAge property. CostSearchTicket 6");
			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[5].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 6");
			Assert.AreEqual(18, ((TravelDate)travelDates[4]).OutwardTickets[5].ChildFare, "ChildFare property. CostSearchTicket 6");
			Assert.AreEqual(Flexibility.LimitedFlexibility, ((TravelDate)travelDates[4]).OutwardTickets[5].Flexibility, "Flexibility property. CostSearchTicket 6");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[6].MinChildAge, "MinChildAge property. CostSearchTicket 7");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[6].MaxChildAge, "MaxChildAge property. CostSearchTicket 7");
			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[6].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 7");
			Assert.AreEqual(19, ((TravelDate)travelDates[4]).OutwardTickets[6].AdultFare, "AdultFare property. CostSearchTicket 7");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[6].Flexibility, "Flexibility property. CostSearchTicket 7");

			Assert.AreEqual(5, ((TravelDate)travelDates[4]).OutwardTickets[7].MinChildAge, "MinChildAge property. CostSearchTicket 8");
			Assert.AreEqual(16, ((TravelDate)travelDates[4]).OutwardTickets[7].MaxChildAge, "MaxChildAge property. CostSearchTicket 8");
			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[7].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 8");
			Assert.AreEqual(5, ((TravelDate)travelDates[4]).OutwardTickets[7].AdultFare, "AdultFare property. CostSearchTicket 8");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[7].Flexibility, "Flexibility property. CostSearchTicket 8");

			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[8].MinChildAge, "MinChildAge property. CostSearchTicket 9");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[8].MaxChildAge, "MaxChildAge property. CostSearchTicket 9");
			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[8].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 9");
			Assert.AreEqual(22, ((TravelDate)travelDates[4]).OutwardTickets[8].AdultFare, "AdultFare property. CostSearchTicket 9");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[8].Flexibility, "Flexibility property. CostSearchTicket 9");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[9].MinChildAge, "MinChildAge property. CostSearchTicket 10");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[9].MaxChildAge, "MaxChildAge property. CostSearchTicket 10");
			Assert.AreEqual(5, ((TravelDate)travelDates[4]).OutwardTickets[9].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 10");
			Assert.AreEqual(19, ((TravelDate)travelDates[4]).OutwardTickets[9].AdultFare, "AdultFare property. CostSearchTicket 10");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[9].Flexibility, "Flexibility property. CostSearchTicket 10");

			Assert.AreEqual(5, ((TravelDate)travelDates[4]).OutwardTickets[10].MinChildAge, "MinChildAge property. CostSearchTicket 11");
			Assert.AreEqual(16, ((TravelDate)travelDates[4]).OutwardTickets[10].MaxChildAge, "MaxChildAge property. CostSearchTicket 11");
			Assert.AreEqual(5, ((TravelDate)travelDates[4]).OutwardTickets[10].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 11");
			Assert.AreEqual(10, ((TravelDate)travelDates[4]).OutwardTickets[10].AdultFare, "AdultFare property. CostSearchTicket 11");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[10].Flexibility, "Flexibility property. CostSearchTicket 11");

			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[11].MinChildAge, "MinChildAge property. CostSearchTicket 12");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[11].MaxChildAge, "MaxChildAge property. CostSearchTicket 12");
			Assert.AreEqual(5, ((TravelDate)travelDates[4]).OutwardTickets[11].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 12");
			Assert.AreEqual(22, ((TravelDate)travelDates[4]).OutwardTickets[11].AdultFare, "AdultFare property. CostSearchTicket 12");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[11].Flexibility, "Flexibility property. CostSearchTicket 12");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[12].MinChildAge, "MinChildAge property. CostSearchTicket 13");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[12].MaxChildAge, "MaxChildAge property. CostSearchTicket 13");
			Assert.AreEqual(6, ((TravelDate)travelDates[4]).OutwardTickets[12].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 13");
			Assert.AreEqual(19, ((TravelDate)travelDates[4]).OutwardTickets[12].DiscountedAdultFare, "DiscountedAdultFare property. CostSearchTicket 13");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[12].Flexibility, "Flexibility property. CostSearchTicket 13");

			Assert.AreEqual(5, ((TravelDate)travelDates[4]).OutwardTickets[13].MinChildAge, "MinChildAge property. CostSearchTicket 14");
			Assert.AreEqual(16, ((TravelDate)travelDates[4]).OutwardTickets[13].MaxChildAge, "MaxChildAge property. CostSearchTicket 14");
			Assert.AreEqual(6, ((TravelDate)travelDates[4]).OutwardTickets[13].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 14");
			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[13].DiscountedAdultFare, "DiscountedAdultFare property. CostSearchTicket 14");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[13].Flexibility, "Flexibility property. CostSearchTicket 14");

			Assert.AreEqual(4, ((TravelDate)travelDates[4]).OutwardTickets[14].MinChildAge, "MinChildAge property. CostSearchTicket 15");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[14].MaxChildAge, "MaxChildAge property. CostSearchTicket 15");
			Assert.AreEqual(6, ((TravelDate)travelDates[4]).OutwardTickets[14].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 15");
			Assert.AreEqual(22, ((TravelDate)travelDates[4]).OutwardTickets[14].DiscountedAdultFare, "DiscountedAdultFare property. CostSearchTicket 15");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[14].Flexibility, "Flexibility property. CostSearchTicket 15");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[15].MinChildAge, "MinChildAge property. CostSearchTicket 16");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[15].MaxChildAge, "MaxChildAge property. CostSearchTicket 16");
			Assert.AreEqual(0, ((TravelDate)travelDates[4]).OutwardTickets[15].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 16");
			Assert.AreEqual(45, ((TravelDate)travelDates[4]).OutwardTickets[15].AdultFare, "AdultFare property. CostSearchTicket 16");
			Assert.AreEqual(33, ((TravelDate)travelDates[4]).OutwardTickets[15].ChildFare, "ChildFare property. CostSearchTicket 16");
			Assert.AreEqual(Flexibility.LimitedFlexibility, ((TravelDate)travelDates[4]).OutwardTickets[15].Flexibility, "Flexibility property. CostSearchTicket 16");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[16].MinChildAge, "MinChildAge property. CostSearchTicket 17");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[16].MaxChildAge, "MaxChildAge property. CostSearchTicket 17");
			Assert.AreEqual(0, ((TravelDate)travelDates[4]).OutwardTickets[16].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 17");
			Assert.AreEqual(55, ((TravelDate)travelDates[4]).OutwardTickets[16].AdultFare, "AdultFare property. CostSearchTicket 17");
			Assert.AreEqual(38, ((TravelDate)travelDates[4]).OutwardTickets[16].ChildFare, "ChildFare property. CostSearchTicket 17");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[16].Flexibility, "Flexibility property. CostSearchTicket 17");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[17].MinChildAge, "MinChildAge property. CostSearchTicket 18");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[17].MaxChildAge, "MaxChildAge property. CostSearchTicket 18");
			Assert.AreEqual(7, ((TravelDate)travelDates[4]).OutwardTickets[17].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 18");
			Assert.AreEqual(50, ((TravelDate)travelDates[4]).OutwardTickets[17].AdultFare, "AdultFare property. CostSearchTicket 18");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[17].Flexibility, "Flexibility property. CostSearchTicket 18");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).OutwardTickets[18].MinChildAge, "MinChildAge property. CostSearchTicket 19");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[18].MaxChildAge, "MaxChildAge property. CostSearchTicket 19");
			Assert.AreEqual(7, ((TravelDate)travelDates[4]).OutwardTickets[18].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 19");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).OutwardTickets[18].AdultFare, "AdultFare property. CostSearchTicket 19");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).OutwardTickets[18].Flexibility, "Flexibility property. CostSearchTicket 19");

		}

		#endregion
        
		#region TestTravelDateReturnTickets

		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class and tests
		/// that the correct CostSearchTickets are added to the ReturnTickets
		/// collection of a TravelDate. Also checks that the properties of
		/// each CostSearchTicket have been set correctly.
		/// </summary>
		[Test]
		public void TestTravelDateReturnTickets()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile1), "SetUp failed for TestTravelDateReturnTickets()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates = travelDates1;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
			// Check that no errors were returned
			Assert.AreEqual (0, errors.Length, "PriceRoute call returned errors in TestTravelDateReturnTickets()");

			// Test that CostSearchTicket collections have been created successfully for first TravelDate
			Assert.AreEqual(4, ((TravelDate)travelDates[0]).ReturnTickets.Length, "Return tickets collection not set correctly");
			
			for (int i=0; i<4; i++)
			{
				// The TravelDateForTicket property will always be the same
				Assert.AreEqual(((TravelDate)travelDates[0]), ((TravelDate)travelDates[0]).ReturnTickets[i].TravelDateForTicket, string.Format("TravelDateForTicket property not set correctly. CostSearchTicket {0}.", i));
			}
			
			// Test the CostSearchTickets		
			Assert.AreEqual(3, ((TravelDate)travelDates[0]).ReturnTickets[0].MinChildAge, "MinChildAge property. CostSearchTicket 1");
			Assert.AreEqual(15, ((TravelDate)travelDates[0]).ReturnTickets[0].MaxChildAge, "MaxChildAge property. CostSearchTicket 1");
			Assert.AreEqual(1, ((TravelDate)travelDates[0]).ReturnTickets[0].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 1");
			Assert.AreEqual(50, ((TravelDate)travelDates[0]).ReturnTickets[0].AdultFare, "AdultFare property. CostSearchTicket 1");
			Assert.AreEqual(Flexibility.LimitedFlexibility, ((TravelDate)travelDates[0]).ReturnTickets[0].Flexibility, "Flexibility property. CostSearchTicket 1");

			Assert.AreEqual(4, ((TravelDate)travelDates[0]).ReturnTickets[1].MinChildAge, "MinChildAge property. CostSearchTicket 2");
			Assert.AreEqual(15, ((TravelDate)travelDates[0]).ReturnTickets[1].MaxChildAge, "MaxChildAge property. CostSearchTicket 2");
			Assert.AreEqual(1, ((TravelDate)travelDates[0]).ReturnTickets[1].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 2");
			Assert.AreEqual(25, ((TravelDate)travelDates[0]).ReturnTickets[1].AdultFare, "AdultFare property. CostSearchTicket 2");
			Assert.AreEqual(Flexibility.LimitedFlexibility, ((TravelDate)travelDates[0]).ReturnTickets[1].Flexibility, "Flexibility property. CostSearchTicket 2");

			Assert.AreEqual(3, ((TravelDate)travelDates[0]).ReturnTickets[2].MinChildAge, "MinChildAge property. CostSearchTicket 3");
			Assert.AreEqual(15, ((TravelDate)travelDates[0]).ReturnTickets[2].MaxChildAge, "MaxChildAge property. CostSearchTicket 3");
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).ReturnTickets[2].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 3");
			Assert.AreEqual(60, ((TravelDate)travelDates[0]).ReturnTickets[2].AdultFare, "AdultFare property. CostSearchTicket 3");
			Assert.AreEqual(47, ((TravelDate)travelDates[0]).ReturnTickets[2].ChildFare, "ChildFare property. CostSearchTicket 3");
			Assert.AreEqual(Flexibility.NoFlexibility, ((TravelDate)travelDates[0]).ReturnTickets[2].Flexibility, "Flexibility property. CostSearchTicket 3");

			Assert.AreEqual(3, ((TravelDate)travelDates[0]).ReturnTickets[3].MinChildAge, "MinChildAge property. CostSearchTicket 4");
			Assert.AreEqual(15, ((TravelDate)travelDates[0]).ReturnTickets[3].MaxChildAge, "MaxChildAge property. CostSearchTicket 4");
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).ReturnTickets[3].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 4");
			Assert.AreEqual(68, ((TravelDate)travelDates[0]).ReturnTickets[3].AdultFare, "AdultFare property. CostSearchTicket 4");
			Assert.AreEqual(52, ((TravelDate)travelDates[0]).ReturnTickets[3].ChildFare, "ChildFare property. CostSearchTicket 4");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[0]).ReturnTickets[3].Flexibility, "Flexibility property. CostSearchTicket 4");
		}

		#endregion

		#region TestTravelDateInwardTickets

		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class and tests
		/// that the correct CostSearchTickets are added to the InwardTickets
		/// collection of a TravelDate. Also checks that the properties of
		/// each CostSearchTicket have been set correctly.
		/// </summary>
		[Test]
		public void TestTravelDateInwardTickets()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile1), "SetUp failed for TestTravelDateInwardTickets()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates = travelDates1;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
			// Check that no errors were returned
			Assert.AreEqual (0, errors.Length, "PriceRoute call returned errors in TestTravelDateInwardTickets()");

			// Test that CostSearchTicket collections have been created successfully for first TravelDate
			Assert.AreEqual(8, ((TravelDate)travelDates[4]).InwardTickets.Length, "Inward tickets collection not set correctly");
			
			for (int i=0; i<8; i++)
			{
				// The TravelDateForTicket property will always be the same
				Assert.AreEqual(((TravelDate)travelDates[4]), ((TravelDate)travelDates[4]).InwardTickets[i].TravelDateForTicket, string.Format("TravelDateForTicket property not set correctly. CostSearchTicket {0}.", i));
			}
			
			// Test the CostSearchTickets		
			Assert.AreEqual(4, ((TravelDate)travelDates[4]).InwardTickets[0].MinChildAge, "MinChildAge property. CostSearchTicket 1");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).InwardTickets[0].MaxChildAge, "MaxChildAge property. CostSearchTicket 1");
			Assert.AreEqual(1, ((TravelDate)travelDates[4]).InwardTickets[0].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 1");
			Assert.AreEqual(22, ((TravelDate)travelDates[4]).InwardTickets[0].AdultFare, "AdultFare property. CostSearchTicket 1");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).InwardTickets[0].Flexibility, "Flexibility property. CostSearchTicket 1");

			Assert.AreEqual(5, ((TravelDate)travelDates[4]).InwardTickets[1].MinChildAge, "MinChildAge property. CostSearchTicket 2");
			Assert.AreEqual(16, ((TravelDate)travelDates[4]).InwardTickets[1].MaxChildAge, "MaxChildAge property. CostSearchTicket 2");
			Assert.AreEqual(1, ((TravelDate)travelDates[4]).InwardTickets[1].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 2");
			Assert.AreEqual(5, ((TravelDate)travelDates[4]).InwardTickets[1].AdultFare, "AdultFare property. CostSearchTicket 2");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).InwardTickets[1].Flexibility, "Flexibility property. CostSearchTicket 2");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).InwardTickets[2].MinChildAge, "MinChildAge property. CostSearchTicket 3");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).InwardTickets[2].MaxChildAge, "MaxChildAge property. CostSearchTicket 3");
			Assert.AreEqual(1, ((TravelDate)travelDates[4]).InwardTickets[2].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 3");
			Assert.AreEqual(19, ((TravelDate)travelDates[4]).InwardTickets[2].AdultFare, "AdultFare property. CostSearchTicket 3");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).InwardTickets[2].Flexibility, "Flexibility property. CostSearchTicket 3");

			Assert.AreEqual(4, ((TravelDate)travelDates[4]).InwardTickets[3].MinChildAge, "MinChildAge property. CostSearchTicket 4");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).InwardTickets[3].MaxChildAge, "MaxChildAge property. CostSearchTicket 4");
			Assert.AreEqual(2, ((TravelDate)travelDates[4]).InwardTickets[3].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 4");
			Assert.AreEqual(22, ((TravelDate)travelDates[4]).InwardTickets[3].AdultFare, "AdultFare property. CostSearchTicket 4");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).InwardTickets[3].Flexibility, "Flexibility property. CostSearchTicket 4");

			Assert.AreEqual(5, ((TravelDate)travelDates[4]).InwardTickets[4].MinChildAge, "MinChildAge property. CostSearchTicket 5");
			Assert.AreEqual(16, ((TravelDate)travelDates[4]).InwardTickets[4].MaxChildAge, "MaxChildAge property. CostSearchTicket 5");
			Assert.AreEqual(2, ((TravelDate)travelDates[4]).InwardTickets[4].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 5");
			Assert.AreEqual(10, ((TravelDate)travelDates[4]).InwardTickets[4].AdultFare, "AdultFare property. CostSearchTicket 5");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).InwardTickets[4].Flexibility, "Flexibility property. CostSearchTicket 5");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).InwardTickets[5].MinChildAge, "MinChildAge property. CostSearchTicket 6");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).InwardTickets[5].MaxChildAge, "MaxChildAge property. CostSearchTicket 6");
			Assert.AreEqual(2, ((TravelDate)travelDates[4]).InwardTickets[5].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 6");
			Assert.AreEqual(19, ((TravelDate)travelDates[4]).InwardTickets[5].AdultFare, "AdultFare property. CostSearchTicket 6");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).InwardTickets[5].Flexibility, "Flexibility property. CostSearchTicket 6");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).InwardTickets[6].MinChildAge, "MinChildAge property. CostSearchTicket 7");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).InwardTickets[6].MaxChildAge, "MaxChildAge property. CostSearchTicket 7");
			Assert.AreEqual(0, ((TravelDate)travelDates[4]).InwardTickets[6].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 7");
			Assert.AreEqual(45, ((TravelDate)travelDates[4]).InwardTickets[6].AdultFare, "AdultFare property. CostSearchTicket 7");
			Assert.AreEqual(33, ((TravelDate)travelDates[4]).InwardTickets[6].ChildFare, "ChildFare property. CostSearchTicket 7");
			Assert.AreEqual(Flexibility.LimitedFlexibility, ((TravelDate)travelDates[4]).InwardTickets[6].Flexibility, "Flexibility property. CostSearchTicket 7");

			Assert.AreEqual(3, ((TravelDate)travelDates[4]).InwardTickets[7].MinChildAge, "MinChildAge property. CostSearchTicket 8");
			Assert.AreEqual(15, ((TravelDate)travelDates[4]).InwardTickets[7].MaxChildAge, "MaxChildAge property. CostSearchTicket 8");
			Assert.AreEqual(0, ((TravelDate)travelDates[4]).InwardTickets[7].CombinedTicketIndex, "CombinedTicketIndex property. CostSearchTicket 8");
			Assert.AreEqual(55, ((TravelDate)travelDates[4]).InwardTickets[7].AdultFare, "AdultFare property. CostSearchTicket 8");
			Assert.AreEqual(38, ((TravelDate)travelDates[4]).InwardTickets[7].ChildFare, "ChildFare property. CostSearchTicket 8");
			Assert.AreEqual(Flexibility.FullyFlexible, ((TravelDate)travelDates[4]).InwardTickets[7].Flexibility, "Flexibility property. CostSearchTicket 8");
		}

		#endregion

		#region TestPublicJourneys

		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class and tests
		/// that the correct PublicJourneys have been associated with 
		/// relevant CostBasedJourneys and CoachJourneyFareSummaries
		/// 
		/// TDJourneyResult contains 5 Outward journeys and 3 Inward journeys,
		/// of which 1 is ignored is it contains only Inward Return fares.
		/// </summary>
		[Test]
		public void TestPublicJourneys()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile1), "SetUp failed for TestPublicJourneys()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			// All discounted fares will be ignored
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates = travelDates1;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
			// Check that no errors were returned
			Assert.AreEqual (0, errors.Length, "PriceRoute call returned errors in TestPublicJourneys()");

			// Create PublicJourney objects
			PublicJourney journey1 = (journeyStore.GetOutwardJourneysForDate(0))[0];
			PublicJourney journey2 = (journeyStore.GetOutwardJourneysForDate(0))[1];
			PublicJourney journey3 = (journeyStore.GetOutwardJourneysForDate(0))[2];
			PublicJourney journey4 = (journeyStore.GetOutwardJourneysForDate(0))[3];
			PublicJourney journey5 = (journeyStore.GetOutwardJourneysForDate(0))[4];
			PublicJourney journey6 = (journeyStore.GetOutwardJourneysForDate(1))[0];
			PublicJourney journey7 = (journeyStore.GetReturnJourneysForDate(0))[0];
			PublicJourney journey8 = (journeyStore.GetReturnJourneysForDate(0))[1];
			PublicJourney journey9 = (journeyStore.GetReturnJourneysForDate(0))[2];
			PublicJourney journey10 = (journeyStore.GetReturnJourneysForDate(1))[0];

			// Test journeyStore returned from method
			Assert.AreEqual(5, journeyStore.GetOutwardJourneysForDate(0).Length, "JourneyStore should contain 5 outward journeys for first date");
			Assert.AreEqual(1, journeyStore.GetOutwardJourneysForDate(1).Length, "JourneyStore should contain 1 outward journey for second date");
			Assert.AreEqual(3, journeyStore.GetReturnJourneysForDate(0).Length, "JourneyStore should contain 3 return journeys for first date");
			Assert.AreEqual(1, journeyStore.GetReturnJourneysForDate(1).Length, "JourneyStore should contain 1 return journey for second date");

			// Journey 1
			// Test CoachJourneyFareSummaries
			for (int i=0; i<4; i++)
			{
				Assert.AreEqual (journey1, supplier.OutwardFareDates[0].FareSummary[i].Journeys[0], string.Format("FareSummary[{0}].Journeys[0] should match journey1.", i));
			}
			// Test Outward CostBasedJourneys
			for (int i=0; i<6; i++)
			{
				Assert.AreEqual (0, ((TravelDate)travelDates[4]).OutwardTickets[i].JourneysForTicket.OutwardJourneyIndexes[0], string.Format("OutwardTickets[{0}].JourneysForTicket.OutwardJourneyIndexes[0] incorrect.", i)); 
				Assert.AreEqual (0, ((TravelDate)travelDates[4]).OutwardTickets[i].JourneysForTicket.OutwardDateIndex, string.Format("OutwardTickets[{0}].JourneysForTicket.OutwardDateIndex incorrect.", i)); 
			}
			Assert.AreEqual (0, ((TravelDate)travelDates[0]).ReturnTickets[0].JourneysForTicket.OutwardJourneyIndexes[0], "ReturnTickets[0].JourneysForTicket.OutwardJourneyIndexes[0] incorrect."); 
			Assert.AreEqual (0, ((TravelDate)travelDates[0]).ReturnTickets[0].JourneysForTicket.OutwardDateIndex, "ReturnTickets[0].JourneysForTicket.OutwardDateIndex incorrect."); 

			Assert.AreEqual (0, ((TravelDate)travelDates[0]).ReturnTickets[1].JourneysForTicket.OutwardJourneyIndexes[0], "ReturnTickets[1].JourneysForTicket.OutwardJourneyIndexes[0] incorrect."); 
			Assert.AreEqual (0, ((TravelDate)travelDates[0]).ReturnTickets[1].JourneysForTicket.OutwardDateIndex, "ReturnTickets[1].JourneysForTicket.OutwardDateIndex incorrect."); 

			// Journey 2
			// Test CoachJourneyFareSummaries
			for (int i=4; i<6; i++)
			{
				Assert.AreEqual (journey2, supplier.OutwardFareDates[0].FareSummary[i].Journeys[0], string.Format("FareSummary[{0}].Journeys[0] should match journey2.", i));
			}
			// Test Outward CostBasedJourneys
			for (int i=6; i<12; i++)
			{
				Assert.AreEqual (1, ((TravelDate)travelDates[4]).OutwardTickets[i].JourneysForTicket.OutwardJourneyIndexes[0], string.Format("OutwardTickets[{0}].JourneysForTicket.OutwardJourneyIndexes[0] incorrect.", i)); 
				Assert.AreEqual (0, ((TravelDate)travelDates[4]).OutwardTickets[i].JourneysForTicket.OutwardDateIndex, string.Format("OutwardTickets[{0}].JourneysForTicket.OutwardDateIndex incorrect.", i)); 
			}

			// Journey 3
			// Test CoachJourneyFareSummaries
			for (int i=6; i<10; i++)
			{
				Assert.AreEqual (journey3, supplier.OutwardFareDates[0].FareSummary[i].Journeys[0], string.Format("FareSummary[{0}].Journeys[0] should match journey3.", i));
			}
			// Test Outward CostBasedJourneys
			for (int i=12; i<14; i++)
			{
				Assert.AreEqual (2, ((TravelDate)travelDates[4]).OutwardTickets[i].JourneysForTicket.OutwardJourneyIndexes[0], string.Format("OutwardTickets[{0}].JourneysForTicket.OutwardJourneyIndexes[0] incorrect.", i)); 
				Assert.AreEqual (0, ((TravelDate)travelDates[4]).OutwardTickets[i].JourneysForTicket.OutwardDateIndex, string.Format("OutwardTickets[{0}].JourneysForTicket.OutwardDateIndex incorrect.", i)); 
			}
			// Test Return CostBasedJourneys
			for (int i=2; i<4; i++)
			{
				Assert.AreEqual (2, ((TravelDate)travelDates[0]).ReturnTickets[i].JourneysForTicket.OutwardJourneyIndexes[0], string.Format("ReturnTickets[{0}].JourneysForTicket.OutwardJourneyIndexes[0] incorrect.", i)); 
				Assert.AreEqual (0, ((TravelDate)travelDates[0]).ReturnTickets[i].JourneysForTicket.OutwardDateIndex, string.Format("ReturnTickets[{0}].JourneysForTicket.OutwardDateIndex incorrect.", i)); 
			}

			// Journey 4
			// Test CoachJourneyFareSummaries
			Assert.AreEqual (journey4, supplier.OutwardFareDates[0].FareSummary[10].Journeys[0], "FareSummary[10].Journeys[0] should match journey4.");
			// Test CostBasedJourneys
			Assert.AreEqual (3, ((TravelDate)travelDates[4]).OutwardTickets[14].JourneysForTicket.OutwardJourneyIndexes[0], "OutwardTickets[16].JourneysForTicket.OutwardJourneyIndexes[1] incorrect."); 
			Assert.AreEqual (0, ((TravelDate)travelDates[4]).OutwardTickets[14].JourneysForTicket.OutwardDateIndex, "OutwardTickets[16].JourneysForTicket.OutwardDateIndex incorrect."); 
			
			Assert.AreEqual (3, ((TravelDate)travelDates[4]).OutwardTickets[15].JourneysForTicket.OutwardJourneyIndexes[0], "OutwardTickets[17].JourneysForTicket.OutwardJourneyIndexes[1] incorrect."); 
			Assert.AreEqual (0, ((TravelDate)travelDates[4]).OutwardTickets[15].JourneysForTicket.OutwardDateIndex, "OutwardTickets[17].JourneysForTicket.OutwardDateIndex incorrect."); 

			// Journey 5
			// Test CoachJourneyFareSummaries
			Assert.AreEqual (journey5, supplier.OutwardFareDates[0].FareSummary[8].Journeys[1], "FareSummary[7].Journeys[0] should match journey5.");
			Assert.AreEqual (journey5, supplier.OutwardFareDates[0].FareSummary[9].Journeys[1], "FareSummary[8].Journeys[0] should match journey5.");
			// Test Return CostBasedJourneys
			for (int i=2; i<4; i++)
			{
				Assert.AreEqual (4, ((TravelDate)travelDates[0]).ReturnTickets[i].JourneysForTicket.OutwardJourneyIndexes[1], string.Format("ReturnTickets[{0}].JourneysForTicket.OutwardJourneyIndexes[0] incorrect.", i)); 
				Assert.AreEqual (0, ((TravelDate)travelDates[0]).ReturnTickets[i].JourneysForTicket.OutwardDateIndex, string.Format("ReturnTickets[{0}].JourneysForTicket.OutwardDateIndex incorrect.", i)); 
			}

			// Journey 6 
			// Should not be attached to any tickets			

			// Journey 7
			// Test CoachJourneyFareSummaries
			for (int i=0; i<2; i++)
			{
				Assert.AreEqual (journey7, supplier.InwardFareDates[0].FareSummary[i].Journeys[0], string.Format("FareSummary[{0}].Journeys[0] should match journey7.", i));
			}
			// Test Inward CostBasedJourneys
			for (int i=0; i<6; i++)
			{
				Assert.AreEqual (0, ((TravelDate)travelDates[4]).InwardTickets[i].JourneysForTicket.InwardJourneyIndexes[0], string.Format("InwardTickets[{0}].JourneysForTicket.InwardJourneyIndexes[0] incorrect.", i)); 
				Assert.AreEqual (0, ((TravelDate)travelDates[4]).InwardTickets[i].JourneysForTicket.InwardDateIndex, string.Format("InwardTickets[{0}].JourneysForTicket.InwardDateIndex incorrect.", i)); 
			}

			// Journey 8
			// Test CoachJourneyFareSummaries
			for (int i=2; i<4; i++)
			{
				Assert.AreEqual (journey8, supplier.InwardFareDates[0].FareSummary[i].Journeys[0], string.Format("FareSummary[{0}].Journeys[0] should match journey8.", i));
			}
			// Test Inward CostBasedJourneys
			for (int i=6; i<8; i++)
			{
				Assert.AreEqual (1, ((TravelDate)travelDates[4]).InwardTickets[i].JourneysForTicket.InwardJourneyIndexes[0], string.Format("InwardTickets[{0}].JourneysForTicket.InwardJourneyIndexes incorrect.", i)); 
				Assert.AreEqual (0, ((TravelDate)travelDates[4]).InwardTickets[i].JourneysForTicket.InwardDateIndex, string.Format("InwardTickets[{0}].JourneysForTicket.InwardDateIndex incorrect.", i)); 
			}

			// Journey 9 

			// Journey 10

			// Test Inward journey collections for valid inward journeys for Return travel date
			Assert.AreEqual(2, ((TravelDate)travelDates[0]).ReturnTickets[2].JourneysForTicket.InwardJourneyIndexes.Count, "InwardJourneyIndexes should be size 2 for first travel dates third return ticket");
			Assert.AreEqual(2, ((TravelDate)travelDates[0]).ReturnTickets[3].JourneysForTicket.InwardJourneyIndexes.Count, "InwardJourneyIndexes should be size 2 for first travel dates fourth return ticket");

			Assert.AreEqual (1, ((TravelDate)travelDates[0]).ReturnTickets[2].JourneysForTicket.InwardJourneyIndexes[0], "InwardJourneyIndexes[0] incorrect for first travel dates third return ticket");
			Assert.AreEqual (2, ((TravelDate)travelDates[0]).ReturnTickets[2].JourneysForTicket.InwardJourneyIndexes[1], "InwardJourneyIndexes[1] incorrect for first travel dates third return ticket");
			Assert.AreEqual (1, ((TravelDate)travelDates[0]).ReturnTickets[3].JourneysForTicket.InwardJourneyIndexes[0], "InwardJourneyIndexes[0] incorrect for first travel dates fourth return ticket");
			Assert.AreEqual (2, ((TravelDate)travelDates[0]).ReturnTickets[3].JourneysForTicket.InwardJourneyIndexes[1], "InwardJourneyIndexes[2] incorrect for first travel dates fourth return ticket");
		}

		#endregion
					
		#region TestPriceRouteNoJourneys

		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class
		/// Tests the correct error handling when CJP
		/// returns no journey results
		/// </summary>
		[Test]
		public void TestPriceRouteNoJourneys()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile2), "SetUp failed for TestPriceRouteNoJourneys()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates1, origin, destination, discounts, sessionInfo, out journeyStore);
			Assert.AreEqual(1, errors.Length, "PriceRoute did not return errors for TestPriceRouteNoJourneys()");
			Assert.AreEqual(PriceRouteNoResults, errors[0], "PriceRoute did not return correct error message for TestPriceRouteNoJourneys");
				
		}

		#endregion

		#region TestPriceRouteNoFares

		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class
		/// Tests the correct error handling when no CJP
		/// returns journey results, but no fares
		/// </summary>
		[Test]
		public void TestPriceRouteNoFares()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile3), "SetUp failed for TestPriceRouteNoFares()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;

			// Call the PriceRoute function with outward and return date (return required)
			string[] errors = supplier.PriceRoute(travelDates2, origin, destination, discounts, sessionInfo, out journeyStore);
			Assert.AreEqual(1, errors.Length, "PriceRoute did not return errors for TestPriceRouteNoFares() with no outward or return fares");
			Assert.AreEqual(PriceRouteNoFares, errors[0], "PriceRoute did not correct error message for TestPriceRouteNoFares");

			// Call the PriceRoute function for with outward date only
			errors = supplier.PriceRoute(travelDates3, origin, destination, discounts, sessionInfo, out journeyStore);
			Assert.AreEqual(1, errors.Length, "PriceRoute did not return errors for TestPriceRouteNoFares() with no outward fares");
			Assert.AreEqual(PriceRouteNoFares, errors[0], "PriceRoute did not correct error message for TestPriceRouteNoFares");
		}

		#endregion

		#region TestPriceRouteNoSingleFares
		/// <summary>
		/// Calls the PriceRoute method of the NatExFaresSupplier class
		/// Tests the correct error handling when user
		/// specifies "No Return", but CJP returns no single fares
		/// </summary>
		[Test]
		public void TestPriceRouteNoSingleFares()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile4), "SetUp failed for TestPriceRouteNoSingleFares()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;

			// Call the PriceRoute function with outward and return date (return required)
			string[] errors = supplier.PriceRoute(travelDates3, origin, destination, discounts, sessionInfo, out journeyStore);
			Assert.AreEqual(1, errors.Length, "PriceRoute did not return errors for TestPriceRouteNoSingleFares()");
			Assert.AreEqual(PriceRouteNoResults, errors[0], "PriceRoute did not return correct error message for TestPriceRouteNoSingleFares");
		}

		#endregion

		#region TestPriceRouteNoReturnRequired

		/// <summary>
		/// Calls the PriceRoute method with only Outward travel dates 
		/// Outward Single and Return journeys are returned
		/// </summary>
		public void TestPriceRouteNoReturnRequired()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile1), "SetUp failed for TestPriceRouteNoReturnRequired()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates = travelDates3;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
			Assert.AreEqual(0, errors.Length, "PriceRoute return errors for TestPriceRouteNoReturnRequired()");

			// Check that return fares have been added to the OpenReturn travel date
			Assert.AreEqual(TicketType.OpenReturn, ((TravelDate)travelDates[0]).TicketType, "TicketType property incorrectly set for first travel date");
			Assert.AreEqual(3, ((TravelDate)travelDates[0]).OutwardTickets.Length, "Return fares not added to the OutwardTickets collection of the first travel date");

			// Check that the single fares have been added to the Single travel date
			Assert.AreEqual(TicketType.Single, ((TravelDate)travelDates[1]).TicketType, "TicketType property incorrectly set for second travel date");
			Assert.AreEqual(16, ((TravelDate)travelDates[1]).OutwardTickets.Length, "Single fares no added to the OutwardTickets collection of the second travel date");
			
			// Test that no tickets have been added to the TravelDate.InwardTickets collection
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).InwardTickets.Length, "Tickets have been incorrectly added to InwardTickets collection");
			// Check that no InwardFareDates have been created
			Assert.AreEqual(0, supplier.InwardFareDates.Length, "InwardFareDates have incorrectly been added to collection");
		}

		#endregion

		#region TestPriceRouteOutwardSingleOnly

		/// <summary>
		/// Calls the PriceRoute method with Outward and Return travel dates 
		/// Only Outward Single journeys are returned
		/// </summary>
		public void TestPriceRouteOutwardSingleOnly()
		{
		
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile6), "SetUp failed for TestPriceRouteOutwardSingleOnly()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates = travelDates2;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
					
			Assert.AreEqual(0, errors.Length, "PriceRoute call returned errors in TestPriceRouteOutwardSingleOnly()");

			// Test that no tickets have been added to the TravelDate.ReturnTickets collection for first travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).ReturnTickets.Length, "Tickets have incorrectly been added to the ReturnTickets collection for first travel date");
			// Test that no tickets have been added to the TravelDate.InwardTickets collection for first travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).InwardTickets.Length, "Tickets have been incorrectly added to InwardTickets collection for first travel date");
			// Test that no tickets have been added to the TravelDate.OutwardTickets collection for first travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).OutwardTickets.Length, "Tickets have been incorrectly added to OutwardTickets collection for first travel date");
			
			// Test that no tickets have been added to the TravelDate.ReturnTickets collection for second travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[1]).ReturnTickets.Length, "Tickets have incorrectly been added to the ReturnTickets collection for second travel date");
			// Test that no tickets have been added to the TravelDate.InwardTickets collection for second travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[1]).InwardTickets.Length, "Tickets have been incorrectly added to InwardTickets collection for second travel date");
			// Test that tickets have been added to the TravelDate.OutwardTickets collection for second travel date
			Assert.AreEqual(1, ((TravelDate)travelDates[1]).OutwardTickets.Length, "Tickets have not been added to OutwardTickets collection for second travel date");
			
			// Check that no FareSummarys have been added to the InwardFareDates collection
			Assert.AreEqual(0, supplier.InwardFareDates[0].FareSummary.Length, "FareSummarys have incorrectly been added to the InwardFareDates collection");

			// Test that TravelDates indicate that there are no results for first travel date
			Assert.AreEqual(TicketType.Return, ((TravelDate)travelDates[0]).TicketType, "TicketType property of first TravelDate incorrectly set");
			Assert.AreEqual(false, ((TravelDate)travelDates[0]).ErrorForInward, "ErrorForInward property of first TravelDate incorrectly set");
			Assert.AreEqual(false, ((TravelDate)travelDates[0]).ErrorForOutward, "ErrorForOutward property of first TravelDate incorrectly set");

			// Test that TravelDates indicate that results are 'Partial' for second travel date
			Assert.AreEqual(TicketType.Singles, ((TravelDate)travelDates[1]).TicketType, "TicketType property of second TravelDate incorrectly set");
			Assert.AreEqual(true, ((TravelDate)travelDates[1]).ErrorForInward, "ErrorForInward property of TravelDate incorrectly set for second travel date");
			Assert.AreEqual(false, ((TravelDate)travelDates[1]).ErrorForOutward, "ErrorForOutward property of TravelDate incorrectly set for second travel date");

		}

		#endregion
		
		#region TestPriceRouteOutwardReturnOnly

		/// <summary>
		/// Calls the PriceRoute method with Outward and Return travel dates 
		/// Only Outward Return journeys are returned
		/// </summary>
		public void TestPriceRouteOutwardReturnOnly()
		{
		
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile7), "SetUp failed for TestPriceRouteOutwardReturnOnly()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates = travelDates2;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
			
			
			Assert.AreEqual(0, errors.Length, "PriceRoute call returned errors in TestPriceRouteOutwardReturnOnly()");

			// Test that no tickets have been added to the TravelDate.OutwardTickets collection
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).OutwardTickets.Length, "Tickets have incorrectly been added to the OutwardTickets collection");
			
			// Test that no tickets have been added to the TravelDate.InwardTickets collection
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).InwardTickets.Length, "Tickets have been incorrectly added to InwardTickets collection");
			// Check that no FareSummarys have been added to the InwardFareDates collection
			Assert.AreEqual(0, supplier.InwardFareDates[0].FareSummary.Length, "FareSummarys have incorrectly been added to the InwardFareDates collection");

		}

		#endregion

		#region TestPriceRouteInwardSingleOnly

		/// <summary>
		/// Calls the PriceRoute method with Outward and Return travel dates 
		/// Only Inward Single journeys are returned
		/// </summary>
		public void TestPriceRouteInwardSingleOnly()
		{
		
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile8), "SetUp failed for TestPriceRouteInwardSingleOnly()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates = travelDates2;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
						
			Assert.AreEqual(0, errors.Length, "PriceRoute call returned errors in TestPriceRouteOutwardSingleOnly()");

			// Test that no tickets have been added to the TravelDate.OutwardTickets collection for first travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).OutwardTickets.Length, "Tickets have been incorrectly added to OutwardTickets collection for first travel date");
			// Test that no tickets have been added to the TravelDate.ReturnTickets collection for first travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).ReturnTickets.Length, "Tickets have incorrectly been added to the ReturnTickets collection for first travel date");
			// Test that no tickets have been added to the TravelDate.InwardTickets collection for first travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[0]).InwardTickets.Length, "Tickets have incorrectly been added to the InwardTickets collection for first travel date");

			// Test that no tickets have been added to the TravelDate.OutwardTickets collection for second travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[1]).OutwardTickets.Length, "Tickets have been incorrectly added to OutwardTickets collection for second travel date");
			// Test that no tickets have been added to the TravelDate.ReturnTickets collection for second travel date
			Assert.AreEqual(0, ((TravelDate)travelDates[1]).ReturnTickets.Length, "Tickets have incorrectly been added to the ReturnTickets collection for second travel date");
			// Test that tickets have been added to the TravelDate.InwardTickets collection for second travel date
			Assert.AreEqual(1, ((TravelDate)travelDates[1]).InwardTickets.Length, "Tickets have not been added to the InwardTickets collection for second travel date");

			// Check that no FareSummarys have been added to the OutwardFareDates collection for first travel date
			Assert.AreEqual(0, supplier.OutwardFareDates[0].FareSummary.Length, "FareSummarys have incorrectly been added to the OutwardFareDates collection for first travel date");

			// Test that TravelDates indicate that there are no results for first travel date
			Assert.AreEqual(TicketType.Return, ((TravelDate)travelDates[0]).TicketType, "TicketType property of TravelDate incorrectly set for first travel date");
			Assert.AreEqual(false, ((TravelDate)travelDates[0]).ErrorForInward, "ErrorForInward property of TravelDate incorrectly set for first travel date");
			Assert.AreEqual(false, ((TravelDate)travelDates[0]).ErrorForOutward, "ErrorForOutward property of TravelDate incorrectly set for first travel date");

			// Test that TravelDates indicate that results are 'Partial' for second travel date
			Assert.AreEqual(TicketType.Singles, ((TravelDate)travelDates[1]).TicketType, "TicketType property of TravelDate incorrectly set for second travel date");
			Assert.AreEqual(false, ((TravelDate)travelDates[1]).ErrorForInward, "ErrorForInward property of TravelDate incorrectly set for second travel date");
			Assert.AreEqual(true, ((TravelDate)travelDates[1]).ErrorForOutward, "ErrorForOutward property of TravelDate incorrectly set for second travel date");

		}

		#endregion

		#region TestCJPManagerIntegration

		/// <summary>
		/// Test method tests that the PriceRoute method can operate 
		/// successfully using live CJPManager calls, with a dummy CJP results file. 
		/// </summary>
		[Test]
		public void TestCJPManagerIntegration()
		{
			// Manually call SetUp with live CJP Manager, but dummy CJP is used
			Assert.IsTrue(SetUp("LIVE"), "SetUp failed for TestCJPManagerIntegration()");
			
			// Create origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "London: Victoria Coach Station (Coach)";
			origin.Locality = "E0034943";
			TDNaptan[] naptans = new TDNaptan[1];
			naptans[0] = new TDNaptan();
			naptans[0].Naptan = "900057366";
			origin.NaPTANs = naptans;
			origin.GridReference = new OSGridReference(528629, 178693);

			TDLocation destination = new TDLocation();
			destination.Description = "Leeds: Bus &amp; Coach Stn, Dyer St (Coach)";
			destination.Locality = "E0034943";
			naptans[0] = new TDNaptan();
			naptans[0].Naptan = "900076052";
			destination.NaPTANs = naptans;
			destination.GridReference = new OSGridReference(430621, 4335338);

			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates = travelDates2;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
						
			Assert.AreEqual(0, errors.Length, "PriceRoute call returned errors in TestCJPManagerIntegration()");

		}

		#endregion
        
		#region TestCJPManagerIntegration

		/// <summary>
		/// Test method tests that PublicJourneys that contain 
		/// Legs not covered by PricingUnits are ignored.
		/// </summary>
		[Test]
		public void TestLegsNotCoveredByPricingUnit()
		{
			// Manually call SetUp, with Cjp stub properties filename
			Assert.IsTrue(SetUp(propertiesFile9), "SetUp failed for TestLegsNotCoveredByPricingUnit()");
			
			// Create dummy origin and desination location objects
			PublicJourneyStore journeyStore;
			TDLocation origin = new TDLocation();
			origin.Description = "Origin";
			TDLocation destination = new TDLocation();
			destination.Description = "Destination";
			Discounts discounts = new Discounts();
			discounts.CoachDiscount = string.Empty;
			ArrayList travelDates = travelDates3;

			// Call the PriceRoute function 
			string[] errors = supplier.PriceRoute(travelDates, origin, destination, discounts, sessionInfo, out journeyStore);
						
			Assert.AreEqual(0, errors.Length, "PriceRoute call returned errors in TestLegsNotCoveredByPricingUnit()");

            // Test that only one ticket has been processed as all other 
			// public journeys were not covered entireley by pricing units
			Assert.AreEqual(false, ((TravelDate)travelDates[0]).HasOutwardTickets, "First pricing unit should not have any tickets in the OutwardTickets collection");
			Assert.AreEqual(false, ((TravelDate)travelDates[0]).HasReturnTickets, "First pricing unit should not have any tickets in the ReturnTickets collection");
			Assert.AreEqual(false, ((TravelDate)travelDates[0]).HasInwardTickets, "First pricing unit should not have any tickets in the InwardTickets collection");

			Assert.AreEqual(false, ((TravelDate)travelDates[1]).HasReturnTickets, "Second pricing unit should not have any tickets in the ReturnTickets collection");
			Assert.AreEqual(false, ((TravelDate)travelDates[1]).HasInwardTickets, "Second pricing unit should not have any tickets in the InwardTickets collection");
			Assert.AreEqual(true, ((TravelDate)travelDates[1]).HasOutwardTickets, "Second pricing unit should have tickets in the OutwardTickets collection");
			Assert.AreEqual(1, ((TravelDate)travelDates[1]).OutwardTickets.Length, "Second pricing unit should have only one ticket in the OutwardTickets collection");

		}

		#endregion
	}
}
