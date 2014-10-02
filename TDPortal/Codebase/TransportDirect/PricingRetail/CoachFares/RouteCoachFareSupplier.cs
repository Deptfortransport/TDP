// ***********************************************
// NAME			: RouteCoachFareSupplier.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-10-27
// DESCRIPTION	: Implementation of the RouteCoachFareSupplier class.
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/RouteCoachFareSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:36   mturner
//Initial revision.
//
//   Rev 1.7   Dec 06 2005 11:27:02   mguney
//PriceRoute method changed to handle exceptional fares.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.6   Nov 30 2005 09:40:30   RPhilpott
//Correct handling of OpenReturn/Return tickets when we have more than one return date with the same outward date. 
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.5   Nov 29 2005 20:27:52   mguney
//Changed for Exceptional Fares.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.4   Nov 29 2005 14:45:46   mguney
//Because of the change made in the signature of CreatePricingResults, false parameter is passed.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.3   Nov 07 2005 20:48:02   RPhilpott
//Add Open Returns to Outwards collection
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Nov 07 2005 18:21:36   RPhilpott
//Allow for pre-populated TravelDates
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Nov 06 2005 19:37:00   RPhilpott
//NUnit fixes.
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
using TransportDirect.UserPortal.PricingRetail.Logging;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for CoachFareSupplier.
	/// </summary>
	public class RouteCoachFareSupplier : CoachFareSupplier
	{
		/// <summary>
		/// Exceptional fares should not be filtered.
		/// </summary>
		protected override bool UseExceptionalFaresLookup
		{
			get
			{
				return false;
			}			
		}

		/// <summary>
		/// Calculate the available fares for the dates and route specified
		/// Use these to create CostSearchTickets, which are added to TravelDates
		/// </summary>
		/// <returns>string array of resource ids for error messages</returns>
		public override string[] PriceRoute(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts, 
			CJPSessionInfo sessionInfo, string operatorCode, int combinedTickets,int legNumber, QuotaFareList quotaFares)
		{

			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					PriceRouteRequestMessage.Message(dates, origin, destination, discounts, sessionInfo, operatorCode, combinedTickets, legNumber, quotaFares)));
			}

			ArrayList errorIds = new ArrayList();
			
			Hashtable outwardDates = GetUniqueOutwardDates(dates);	

			ArrayList outwardDateList = new ArrayList(outwardDates.Keys);

			// Get a pair of PricingResults (singles/returns) for 
			//  each unique outward date in the TravelDates array ...
 
			foreach (TDDateTime outDate in outwardDateList)
			{
				//CoachFare[] fares = GetFaresForSingleDate(outDate, CoachFaresInterfaceType.ForRoute, operatorCode, origin, destination, sessionInfo);	
				//PricingResult[] pricingResults = CreatePricingResults(fares, discounts, operatorCode,false);
				//outwardDates[outDate] = pricingResults;				
				CoachFare[] fares = GetFaresForSingleDate(outDate, CoachFaresInterfaceType.ForRoute, operatorCode, origin, destination, sessionInfo);	
				outwardDates[outDate] = fares;
			}

			int dateArraySize = dates.Count;

			ArrayList additionalTravelDates = new ArrayList(dateArraySize);

			foreach (TravelDate td in dates)
			{
				//if ReturnDate = OutwardDate 
				//then include day return tickets. (see CreatePricingResults method of coachfaresupplier)
				bool isDayReturn = (td.ReturnDate != null) && 
					(td.OutwardDate.GetDateTime().Date == td.ReturnDate.GetDateTime().Date);

				//PricingResult[] results = (PricingResult[]) outwardDates[td.OutwardDate];
				PricingResult[] results = CreatePricingResults((CoachFare[])outwardDates[td.OutwardDate], 
					discounts, operatorCode, isDayReturn);
				
				if	(td.TicketType == TicketType.None)
				{
					td.TicketType = (td.ReturnDate != null ? TicketType.Return : TicketType.OpenReturn);

					AddTicketsToTravelDate(td, results[1], combinedTickets, legNumber);

					TravelDate newTd = GetSingleTravelDate(td, dateArraySize);
					newTd.TicketType = TicketType.Single;
			
					AddTicketsToTravelDate(newTd, results[0], combinedTickets, legNumber);

					additionalTravelDates.Add(newTd);
				}
				else
				{
					if	(td.TicketType == TicketType.Single)
					{
						AddTicketsToTravelDate(td, results[0], combinedTickets, legNumber);
					}
					else if (td.TicketType == TicketType.Return)
					{
						AddTicketsToTravelDate(td, results[1], combinedTickets, legNumber);
					}
				}
			}

			dates.AddRange(additionalTravelDates);

			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, PriceRouteResponseMessage.Message(dates)));
			}

			return (string[])(errorIds.ToArray(typeof(string)));
		}

	}
}

