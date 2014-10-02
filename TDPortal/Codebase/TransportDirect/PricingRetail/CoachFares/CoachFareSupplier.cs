// ***********************************************
// NAME			: CoachFareSupplier.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-10-27
// DESCRIPTION	: Implementation of the CoachFareSupplier class.
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/CoachFareSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:32   mturner
//Initial revision.
//
//   Rev 1.12   May 25 2007 16:22:12   build
//Automatically merged from branch for stream4401
//
//   Rev 1.11.1.0   May 10 2007 16:27:20   asinclair
//Pass in extra bool to AddUndiscountedFare and AddDiscountedFare
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.11   Jan 18 2006 18:16:30   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.10   Dec 08 2005 09:55:10   mguney
//Changes made for code review.
//Resolution for 3345: Coach Fare Code Review - CR026_IR_2818 Coach Fares.doc
//
//   Rev 1.9   Dec 06 2005 11:23:20   mguney
//Filtering exceptions and probability settings changed.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.8   Nov 30 2005 09:40:16   RPhilpott
//Correct handling of OpenReturn/Return tickets when we have more than one return date with the same outward date. 
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.7   Nov 29 2005 20:26:56   mguney
//Changed for Exceptional Fares.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.6   Nov 29 2005 20:26:22   mguney
//Changed for Exceptional Fares.
//
//   Rev 1.5   Nov 29 2005 14:42:10   mguney
//In GetPricingResults, adding discounted and undiscounted fares are done according to the day return and exclude conditions. (ExceptionalFares)
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.4   Nov 07 2005 20:48:36   RPhilpott
//Add Open Return to Outwards collections
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Nov 07 2005 18:21:30   RPhilpott
//Allow for pre-populated TravelDates
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Nov 06 2005 19:36:52   RPhilpott
//NUnit fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Nov 02 2005 19:04:14   RPhilpott
//Work in progress
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 18:55:28   RPhilpott
//Initial revision.
//

using System;
using System.Collections;
using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for CoachFareSupplier.
	/// </summary>
	public abstract class CoachFareSupplier : IRoutePriceSupplier
	{
		private static string NO_DISCOUNT = string.Empty;		
		private FareRequest fareRequest; 
		
		/// <summary>
		/// Calculate the available fares for the dates and route specified
		/// Use these to create CostSearchTickets, which are added to TravelDates
		/// </summary>
		/// <returns>string array of resource ids for error messages</returns>
		public abstract string[] PriceRoute(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts, 
			CJPSessionInfo sessionInfo, string operatorCode, int combinedTickets,int legNumber, QuotaFareList quotaFares);

		/// <summary>
		/// Property to be implemented by the child classes.
		/// True: Exceptional Fares Lookup object is going to be used by the PricingResultsBuilder to get the
		/// exceptional fares, day return etc. information.
		/// False: IF098 coach fare properties (from CoachFareForRoute object) are going to be used for this.
		/// </summary>
		protected abstract bool UseExceptionalFaresLookup
		{
			get;			
		}

		/// <summary>
		/// Combines an array of coach fares into consistent sets 
		/// </summary>
		/// <param name="fares">Array of coach fares to be processed</param>
		/// <param name="discounts">Discount cards selected by user</param>
		/// <param name="operatorCode"></param>
		/// <returns>Array of precisely two PricingResults, for single and returns (in that order)</returns>
		protected internal PricingResult[] CreatePricingResults(CoachFare[] fares, Discounts discounts, 
			string operatorCode, bool isDayReturn)
		{
			Hashtable singleResults = GetPricingResults(fares, discounts, true,  operatorCode, isDayReturn); 
			Hashtable returnResults = GetPricingResults(fares, discounts, false, operatorCode, isDayReturn); 

			string discountCard = (discounts.CoachDiscount != null && discounts.CoachDiscount.Length != 0) 
				? discounts.CoachDiscount : NO_DISCOUNT;
			
			string singleCard = singleResults.ContainsKey(discountCard) ? discountCard : NO_DISCOUNT;
			string returnCard = returnResults.ContainsKey(discountCard) ? discountCard : NO_DISCOUNT;

			PricingResult unfilteredSingleResults = (PricingResult)singleResults[singleCard];
			PricingResult unfilteredReturnResults = (PricingResult)returnResults[returnCard];
			
			CoachFareFilterAndMergeHelper filterHelper = new CoachFareFilterAndMergeHelper();
			
			PricingResult filteredSingleResult = filterHelper.FilterFares(unfilteredSingleResults);
			PricingResult filteredReturnResult = filterHelper.FilterFares(unfilteredReturnResults);
			
			return new PricingResult[] {filteredSingleResult, filteredReturnResult};  
		}


		/// <summary>
		/// Uses a PricingResultsBuilder to create a hashtable of PricingResults 
		/// </summary>
		/// <param name="fares">Array of coach fares</param>
		/// <param name="discounts">Discount cards selected by user</param>
		/// <param name="isSingle">True if processing single fares, false for return</param>
		/// <param name="operatorCode"></param>
		/// <returns>Hashtable of PricingResults</returns>
		private Hashtable GetPricingResults(CoachFare[] fares, Discounts discounts, bool isSingle, 
			string operatorCode, bool isDayReturn)
		{
			PricingResultsBuilder builder = new PricingResultsBuilder(isDayReturn,UseExceptionalFaresLookup);			
			
			foreach (CoachFare coachFare in fares)
			{				
				if	((coachFare.IsSingle && isSingle) || (!coachFare.IsSingle && !isSingle))
				{
					if	(coachFare.DiscountCardType == null || coachFare.DiscountCardType.Length == 0)
					{						
						builder.AddUndiscountedFare(coachFare, operatorCode, false);														  
					}
				}
			}

			if	(discounts.CoachDiscount != null && discounts.CoachDiscount.Length > 0)
			{
				builder.AddDiscountCard(discounts.CoachDiscount);
	
				foreach (CoachFare coachFare in fares)
				{
					if	((coachFare.IsSingle && isSingle) || (!coachFare.IsSingle && !isSingle))
					{
						if	(coachFare.DiscountCardType != null || coachFare.DiscountCardType.Length > 0)
						{							
							builder.AddDiscountedFare(coachFare, operatorCode, false);
						}
					}
				}
			}

			return builder.GetPricingResults();
		}

		/// <summary>
		/// Uses the appropriate fares interface for the operator to get fares for a specific date.
		/// </summary>
		/// <param name="dateTime">Date to get the fare for (including a time, for Journey interface)</param>
		/// <param name="type">ForRoute or ForJourney</param>
		/// <param name="origin">TDLocation containing precisely one Naptan, for origin</param>
		/// <param name="destination">TDLocation containing precisely one Naptan, for destination</param>
		/// <param name="sessionInfo">Session info</param>
		/// <returns>Array of CoachFares</returns>
		protected internal CoachFare[] GetFaresForSingleDate(TDDateTime dateTime, CoachFaresInterfaceType type, 
			string operatorCode, TDLocation origin, TDLocation destination, CJPSessionInfo sessionInfo)
		{

			ICoachFaresInterfaceFactory factory = (ICoachFaresInterfaceFactory)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachFaresInterface];
			IFaresInterface faresInterface = factory.GetFaresInterface(type);

			TDNaptan originNaptan = origin.NaPTANs[0];
			TDNaptan destinationNaptan = destination.NaPTANs[0];

			TDDateTime startDateTime = dateTime;
			TDDateTime endDateTime = (type == CoachFaresInterfaceType.ForRoute ? dateTime : null);

			fareRequest = new FareRequest(operatorCode, originNaptan, destinationNaptan, 
													startDateTime, endDateTime, sessionInfo); 

			FareResult result = faresInterface.GetCoachFares(fareRequest);

			if	(result.ErrorStatus == FareErrorStatus.Error)
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
									"Unable to price " + operatorCode + " route from " + originNaptan.Naptan + " to " + destinationNaptan.Naptan + " on " + startDateTime.ToString()));
				return new CoachFare[0];
			}

			return result.Fares;
		}
	

		/// <summary>
		/// Gets the unique outward dates from the given array list.
		/// </summary>
		/// <param name="dates"></param>
		/// <returns></returns>
		protected internal Hashtable GetUniqueOutwardDates(ArrayList dates)
		{
			Hashtable uniqueDates = new Hashtable(dates.Count);

			foreach (TravelDate td in dates)
			{
				if	(!uniqueDates.Contains(td.OutwardDate))
				{
					uniqueDates.Add(td.OutwardDate, null);
				}
			}
			
			return uniqueDates;
		}


		/// <summary>
		/// Method creates a copy of a TravelDate, setting any existing
		/// properties. The TicketType property is set according to the 
		/// TicketType property of the TravelDate being cloned.
		/// </summary>
		/// <param name="travelDate">TravelDate to clone</param>
		/// <param name="openReturn">bool openReturn</param>
		/// <param name="size">int size of original collection</param>
		/// <returns>cloned TravelDate</returns>
		protected TravelDate GetSingleTravelDate(TravelDate travelDate, int size)
		{
			TravelDate singleTravelDate = new TravelDate();
			
			singleTravelDate.Index = travelDate.Index + size;
			singleTravelDate.OutwardDate = travelDate.OutwardDate;
			singleTravelDate.ReturnDate = travelDate.ReturnDate;
			singleTravelDate.TravelMode = travelDate.TravelMode;
			singleTravelDate.TicketType = TicketType.Single;
			
			return singleTravelDate;
		}

		/// <summary>
		/// Method creates a CostSearchTicket to be attached to a specific 
		/// TravelDate, based on an existing Ticket.
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="childAgeMin"></param>
		/// <param name="childAgeMax"></param>
		/// <param name="operatorCode"></param>
		/// <param name="index"></param>
		/// <param name="legNo"></param>
		/// <returns>A new CostSearchTicket</returns>
		protected CostSearchTicket CreateCostSearchTicket(Ticket ticket, uint childAgeMin, uint childAgeMax, 
			int index, int legNo)
		{
			CostSearchTicket cst = new CostSearchTicket(ticket.Code,
				ticket.Flexibility,
				ticket.ShortCode,
				ticket.AdultFare,
				ticket.ChildFare,
				float.NaN,
				float.NaN,
				ticket.DiscountedAdultFare,
				ticket.DiscountedChildFare,
				childAgeMin,
				childAgeMax,
				ticket.TicketCoachFareData.Probability);				

			cst.TicketCoachFareData = ticket.TicketCoachFareData;
			cst.LegNumber = legNo;
			cst.CombinedTicketIndex = index;

			return cst;
		}

		
		/// <summary>
		/// read-only property to expose the fare request, for NUnit testing only
		/// </summary>
		protected internal FareRequest FareRequest
		{
			get { return fareRequest; }
		}

		/// <summary>
		/// Adds tickets from the given result set to the given travel date.
		/// </summary>
		/// <param name="td"></param>
		/// <param name="result"></param>
		/// <param name="combinedTickets"></param>
		/// <param name="legNumber"></param>
		protected void AddTicketsToTravelDate(TravelDate td, PricingResult result, int combinedTickets, int legNumber)
		{
			uint childAgeMin = (uint)(result.MinChildAge);
			uint childAgeMax = (uint)(result.MaxChildAge);

			foreach (Ticket ticket in result.Tickets)
			{
				CostSearchTicket cst = CreateCostSearchTicket(ticket, childAgeMin, childAgeMax, combinedTickets, legNumber); 					
				cst.TravelDateForTicket = td;
				
				if	(td.TicketType == TicketType.Return)
				{
					td.AddReturnTicket(cst);
					td.AddOutwardTicket(cst);
				}
				else
				{
					if	(td.TicketType != TicketType.OpenReturn || ticket.Flexibility != Flexibility.NoFlexibility)
					{
						td.AddOutwardTicket(cst);
					}
				}
			}
		}
	}
}

