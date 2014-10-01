// ************************************************************** 
// NAME			: TestMockRoutePriceSupplier.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 02/03/2005 
// DESCRIPTION	: Mock implemention of IRoutePriceSupplier
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestMockRoutePriceSupplier.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 08:50:38   mmodi
//Updated ticket objects to include additional parameters for use by the RBO MR call
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 16:21:56   mturner
//Initial revision.
//
//   Rev 1.4   Jun 02 2006 14:32:38   rphilpott
//Fix NUnit test.
//
//   Rev 1.3   Nov 24 2005 18:23:04   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.2   Nov 03 2005 19:22:12   RPhilpott
//Merge undiscounted and discounted CoachFareData into one.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Nov 02 2005 09:31:14   RWilby
//Updated Unit Tests
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 15:26:54   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using System.Collections;


using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// TestMockRailRoutePriceSupplier
	/// </summary>
	public class TestMockRailRoutePriceSupplier :TestMockRoutePriceSupplierBase
	{
		protected override TravelDate[] GetOutwardTravelDates()
		{
			//mock versions of rail tickets
			CostSearchTicket[] singleRailTickets;
				
			CostSearchTicket costSearchTicket1 = new CostSearchTicket("Apex Single /any rail",Flexibility.FullyFlexible,"Apex",27.10f,15.00f, float.NaN, float.NaN, 40.00f,5.00f,0,16,Probability.Medium);
            costSearchTicket1.TicketRailFareData = new RailFareData("shortTicketCode", "routeCode", "restrictionCode", "originNic", "destinationNic", "railcardCode", false, null, null, string.Empty);
			CostSearchTicket costSearchTicket2 = new CostSearchTicket("7 day advance",Flexibility.NoFlexibility,"7day",20.00f,10.00f, float.NaN, float.NaN, 5.00f,5.00f,0,16,Probability.Medium);
            costSearchTicket2.TicketRailFareData = new RailFareData("shortTicketCode", "routeCode", "restrictionCode", "originNic", "destinationNic", "railcardCode", false, null, null, string.Empty);
			CostSearchTicket costSearchTicket3 = new CostSearchTicket("Cheap day single /any rail",Flexibility.LimitedFlexibility,"CDS",5.00f,1.00f, float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium);
            costSearchTicket3.TicketRailFareData = new RailFareData("shortTicketCode", "routeCode", "restrictionCode", "originNic", "destinationNic", "railcardCode", false, null, null, string.Empty);
			CostSearchTicket costSearchTicket4 = new CostSearchTicket("Standard first /any rail",Flexibility.FullyFlexible,"STF",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16,Probability.High);
            costSearchTicket4.TicketRailFareData = new RailFareData("shortTicketCode", "routeCode", "restrictionCode", "originNic", "destinationNic", "railcardCode", false, null, null, string.Empty);


			singleRailTickets = new CostSearchTicket[]
			{		
				costSearchTicket1,
				costSearchTicket2,
				costSearchTicket3,
				costSearchTicket4
			};		


			CostSearchTicket costSearchTicket5 = new CostSearchTicket("Midland Mainline Apex Single",Flexibility.NoFlexibility,"MMApex",27.10f,15.00f, float.NaN, float.NaN, 40.00f,5.00f,0,16,Probability.Medium);
            costSearchTicket5.TicketRailFareData = new RailFareData("shortTicketCode", "routeCode", "restrictionCode", "originNic", "destinationNic", "railcardCode", false, null, null, string.Empty);
			CostSearchTicket costSearchTicket6 = new CostSearchTicket("Midland Mainline 7 day advance",Flexibility.NoFlexibility,"MM7day",20.00f,10.00f, float.NaN, float.NaN, 5.00f,5.00f,0,16,Probability.Medium);
            costSearchTicket6.TicketRailFareData = new RailFareData("shortTicketCode", "routeCode", "restrictionCode", "originNic", "destinationNic", "railcardCode", false, null, null, string.Empty);
			CostSearchTicket costSearchTicket7 = new CostSearchTicket("Midland Mainline Cheap day single",Flexibility.LimitedFlexibility,"MMCDS",5.00f,1.00f, float.NaN, float.NaN, 2.00f,1.00f,0,16,Probability.Medium);
            costSearchTicket7.TicketRailFareData = new RailFareData("shortTicketCode", "routeCode", "restrictionCode", "originNic", "destinationNic", "railcardCode", false, null, null, string.Empty);
			CostSearchTicket costSearchTicket8 = new CostSearchTicket("Midland Mainline Standard first",Flexibility.FullyFlexible,"MMSF",40.00f,20.00f, float.NaN, float.NaN, 30.00f,10.00f,0,16,Probability.High);
            costSearchTicket8.TicketRailFareData = new RailFareData("shortTicketCode", "routeCode", "restrictionCode", "originNic", "destinationNic", "railcardCode", false, null, null, string.Empty);

			CostSearchTicket[] singleRailTickets2;
			singleRailTickets2 = new CostSearchTicket[]
			{
				costSearchTicket5,
				costSearchTicket6,
				costSearchTicket7,
				costSearchTicket8
			};	
			
			//TDDateTime eighthJune = new TDDateTime(DateTime.Now);

			TDDateTime eighthJune = new TDDateTime(2006,6,08);	

			//mock rail travel dates
			TravelDate railDate1 = new TravelDate(101, eighthJune,TicketTravelMode.Rail,10.00f,50.00f,true);
			railDate1.TicketType = TicketType.OpenReturn;
			railDate1.OutwardTickets = singleRailTickets;	
			
			TravelDate railDate2 = new TravelDate(102, eighthJune.AddDays(1),TicketTravelMode.Rail,10.00f,50.00f,true);
			railDate2.TicketType = TicketType.Single;
			railDate2.OutwardTickets = singleRailTickets2;	

			//assign mock travel dates to results set
			TravelDate[] travelDates = new TravelDate[]
			{railDate1, railDate2};	

			//add bidirectional assocation between ticket and travel date
			AddTicketAndTravelDateAssociation(travelDates);

			return travelDates;
		}

	}

	/// <summary>
	/// TestMockCoachRoutePriceSupplier
	/// </summary>
	public class TestMockCoachRoutePriceSupplier :TestMockRoutePriceSupplierBase
	{
		protected override TravelDate[] GetOutwardTravelDates()
		{
			const string LondonNaptan = "900057366";
			const string CambridgeNaptan = "900052074";
			const string LutonNaptan = "900051114";
			

			//Setup dump fare data for london to cambridge
			CoachFareData	coachFareData = new CoachFareData();
			coachFareData.OriginNaptan = LondonNaptan;
			coachFareData.DestinationNaptan = CambridgeNaptan;
			coachFareData.ChangeNaptans = new string[]{LutonNaptan};
			
	
			//setup ticket 1 for travel date 1
			CostSearchTicket costSearchTicket1 = new CostSearchTicket( "Nx Coach Ticket",Flexibility.FullyFlexible,"NXCoachTicket",27.10f,15.00f, float.NaN, float.NaN, 20.00f,5.00f,0,16,Probability.Medium);
			costSearchTicket1.CombinedTicketIndex = 0;
			costSearchTicket1.LegNumber = 0;
			costSearchTicket1.TicketCoachFareData = coachFareData;
			CostSearchTicket[] CoachTickets1 = new CostSearchTicket[]{costSearchTicket1};

			
			//Setup dump fare data for london to luton
			CoachFareData	coachFareData2 = new CoachFareData();
			coachFareData2.OriginNaptan = LondonNaptan;
			coachFareData2.DestinationNaptan = LutonNaptan;
			

			//Setup dump fare data for luton to cambridge
			CoachFareData	coachFareData3 = new CoachFareData();
			coachFareData3.OriginNaptan = LutonNaptan;
			coachFareData3.DestinationNaptan = CambridgeNaptan;
			

			//setup ticket 1 for travel date 2
			CostSearchTicket costSearchTicket2 = new CostSearchTicket( "Nx Coach Ticket",Flexibility.FullyFlexible,"NXCoachTicket",27.10f,15.00f, float.NaN, float.NaN, 20.00f,5.00f,0,16,Probability.Medium);
			costSearchTicket2.CombinedTicketIndex = 1;
			costSearchTicket2.LegNumber = 0;
			costSearchTicket2.TicketCoachFareData = coachFareData2;

			//setup ticket 2 for travel date 2
			CostSearchTicket costSearchTicket3 = new CostSearchTicket( "SCL Coach Ticket",Flexibility.FullyFlexible,"SCLCoachTicket",27.10f,15.00f, float.NaN, float.NaN, 20.00f,5.00f,0,16,Probability.Medium);
			costSearchTicket3.CombinedTicketIndex = 1;
			costSearchTicket3.LegNumber = 1;
			costSearchTicket3.TicketCoachFareData = coachFareData3;

			CostSearchTicket[] CoachTickets2 = new CostSearchTicket[]{costSearchTicket2,costSearchTicket3};

			TDDateTime eighthJune = new TDDateTime(2006,6,08);	
			//mock coahc travel dates
			TravelDate coachDate1 = new TravelDate( 101, eighthJune,TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate1.TicketType = TicketType.OpenReturn;
			coachDate1.OutwardTickets = CoachTickets1;	
			coachDate1.ReturnTickets = CoachTickets1;
			
			TravelDate coachDate2 = new TravelDate(102, eighthJune.AddDays(1),TicketTravelMode.Coach,10.00f,50.00f,true);
			coachDate2.TicketType = TicketType.Single;
			coachDate2.OutwardTickets = CoachTickets2;	

			//assign mock travel dates to results set
			TravelDate[] travelDates = new TravelDate[]
			{coachDate1, coachDate2};	

			//add bidirectional assocation between ticket and travel date
			AddTicketAndTravelDateAssociation(travelDates);

			return travelDates;
		}

	}
	/// <summary>
	/// Base class for Mock RoutePriceSupplier classes.
	/// </summary>
	public abstract class TestMockRoutePriceSupplierBase : IRoutePriceSupplier
	{
		public TestMockRoutePriceSupplierBase()
		{
		}

		/// <summary>
		/// Calculate the available fares for the dates and route specified
		/// Use these to create CostSearchTickets, which are added to TravelDates
		/// </summary>
		/// <param name="dates"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="dicounts"></param>
		/// <param name="journeyStore"></param></param>
		/// <returns>string array of resource ids for error messages</returns>
		public string[] PriceRoute(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts, 
			CJPSessionInfo sessionInfo, string operatorCode, int combinedTickets,int legNumber, QuotaFareList quotaFares)
		{
			string[] errors =  null;
			TravelDate[] tdArray = GetOutwardTravelDates();
			dates.Clear();

			foreach (TravelDate td in tdArray)
			{
				dates.Add(td);	
			}

			
			return errors;
		}	

		/// <summary>
		/// Creates mock data for tests
		/// </summary>
		/// <returns></returns>
		protected abstract TravelDate[] GetOutwardTravelDates();
		
		/// <summary>
		/// Adds an association between each ticket and its travel date
		/// </summary>
		/// <param name="travelDate"></param>
		protected void AddTicketAndTravelDateAssociation(TravelDate[] allTravelDates)
		{

			//step through all travel dates
			foreach (TravelDate thisTravelDate in allTravelDates)
			{
				//link all outward tickets
				if (thisTravelDate.OutwardTickets != null)
				{
					foreach (CostSearchTicket ticket in thisTravelDate.OutwardTickets)
					{
						ticket.TravelDateForTicket = thisTravelDate;
					}
				}

				//link all outward tickets
				if (thisTravelDate.InwardTickets != null)
				{
					foreach (CostSearchTicket ticket in thisTravelDate.InwardTickets)
					{
						ticket.TravelDateForTicket = thisTravelDate;
					}
				}


				//link all outward tickets
				if (thisTravelDate.ReturnTickets != null)
				{
					foreach (CostSearchTicket ticket in thisTravelDate.ReturnTickets)
					{
						ticket.TravelDateForTicket = thisTravelDate;
					}
				}
			}
		}

	}
}
