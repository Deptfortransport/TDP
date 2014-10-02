// ***********************************************
// NAME			: JourneyCoachFareSupplier.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-10-27
// DESCRIPTION	: Implementation of the RouteCoachFareSupplier class.
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/JourneyCoachFareSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:36   mturner
//Initial revision.
//
//   Rev 1.8   Jan 18 2006 18:16:30   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.7   Dec 06 2005 11:24:06   mguney
//Exceptional fares property name changed.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.6   Nov 30 2005 09:40:22   RPhilpott
//Correct handling of OpenReturn/Return tickets when we have more than one return date with the same outward date. 
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.5   Nov 29 2005 20:25:56   mguney
//Changed for Exceptional Fares.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.4   Nov 29 2005 14:43:54   mguney
//In PriceRoute method, isDayReturn is calculated and passed into CreatePricingResults method.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.3   Nov 07 2005 20:48:32   RPhilpott
//Add Open Return to Outwards collections
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Nov 07 2005 18:21:34   RPhilpott
//Allow for pre-populated TravelDates
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Nov 06 2005 19:37:00   RPhilpott
//NUnit fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 18:55:30   RPhilpott
//Initial revision.
//

using System;
using System.Collections;
using System.Globalization;

using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
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
	public class JourneyCoachFareSupplier : CoachFareSupplier
	{
		
		private const string START_TIME_PROPERTY = "CoachFares.JourneyInterface.SampleTime";

		/// <summary>
		/// Exceptional fares should be filtered.
		/// </summary>
		protected override bool UseExceptionalFaresLookup
		{
			get
			{
				return true;
			}			
		}

		
		/// <summary>
		/// Calculate the available fares for the dates and route specified
		/// Use these to create CostSearchTickets, which are added to TravelDates
		/// </summary>
		/// <returns>string array of resource ids for error messages</returns>
		public override string[] PriceRoute(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts, 
			CJPSessionInfo sessionInfo, string operatorCode, int combinedTickets, int legNumber, QuotaFareList quotaFares)
		{
			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					PriceRouteRequestMessage.Message(dates, origin, destination, discounts, sessionInfo, operatorCode, combinedTickets, legNumber, quotaFares)));
			}

			ArrayList errorIds = new ArrayList();
			
			Hashtable outwardDates = GetUniqueOutwardDates(dates);	

			TimeSpan timeToAdd = GetSampleStartTime(Properties.Current[START_TIME_PROPERTY]);

			// Get a pair of PricingResults (singles/returns) for 
			//  each unique outward date in the TravelDates array ...
 
			ArrayList outwardDateList = new ArrayList(outwardDates.Keys);

			foreach (TDDateTime outDate in outwardDateList)
			{
				TDDateTime outDateTime = outDate.Add(timeToAdd);
				CoachFare[] fares = GetFaresForSingleDate(outDateTime, CoachFaresInterfaceType.ForJourney, operatorCode, origin, destination, sessionInfo);	
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
																	
				PricingResult[] results = CreatePricingResults((CoachFare[])outwardDates[td.OutwardDate], 
					discounts, operatorCode, isDayReturn);
				
				if	(td.TicketType == TicketType.None)
				{
					td.TicketType = (td.ReturnDate != null ? TicketType.Return : TicketType.OpenReturn);

					AddTicketsToTravelDate(td, results[1], combinedTickets, legNumber);

					TravelDate newTd = GetSingleTravelDate(td, dateArraySize);
					newTd.TicketType = TicketType.Single;
			
					AddTicketsToTravelDate(newTd, results[0], combinedTickets, legNumber);

					foreach (QuotaFare qf in quotaFares)
					{
						CostSearchTicket quotaTicket = CreateQuotaTicket(qf, operatorCode, combinedTickets, legNumber); 					
						quotaTicket.TravelDateForTicket = newTd;
						newTd.AddOutwardTicket(quotaTicket);
					}
				
					additionalTravelDates.Add(newTd);
				}
				else
				{
					if	(td.TicketType == TicketType.Single)
					{
						AddTicketsToTravelDate(td, results[0], combinedTickets, legNumber);

						foreach (QuotaFare qf in quotaFares)
						{
							CostSearchTicket quotaTicket = CreateQuotaTicket(qf, operatorCode, combinedTickets, legNumber); 					
							quotaTicket.TravelDateForTicket = td;
							td.AddOutwardTicket(quotaTicket);
						}
					}
					else if (td.TicketType == TicketType.Return || td.TicketType == TicketType.OpenReturn)
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


		/// <summary>
		/// Calculate the offset from midnight for start of sample time, 
		/// using the time read in format hh:mm (or hh.mm) from the properties.
		/// </summary>
		/// <param name="startTimeString">String in format hh:mm or hh.mm</param>
		/// <returns>Offset from midnight</returns>
		internal TimeSpan GetSampleStartTime(string startTimeString)
		{
			int hours = 0; 
			int mins  = 0;

			if	(startTimeString != null) // assumes midnight if not found
			{
				string[] timeStrings = startTimeString.Split(new char[] { ':', '.' });
			
				try 
				{
					hours = Int32.Parse(timeStrings[0], CultureInfo.InvariantCulture);
					mins  = Int32.Parse(timeStrings[1], CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
					// if invalid, assume midnight ...
					hours = 0; 
					mins  = 0;
				}
			}
			return new TimeSpan(0, hours, mins, 0);
		}

		/// <summary>
		/// Method creates a CostSearchTicket to be attached to  
		/// a specific TravelDate, based on a quota fare.
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="operatorCode"></param>
		/// <param name="index"></param>
		/// <param name="legNo"></param>
		/// <returns>A new CostSearchTicket</returns>
		private CostSearchTicket CreateQuotaTicket(QuotaFare fare, string operatorCode, int index, int legNo)
		{
			CostSearchTicket cst = new CostSearchTicket(fare.TicketType,
				Flexibility.NoFlexibility,
				string.Empty,
				((float)fare.Fare) / 100,
				float.NaN,
				float.NaN,
				float.NaN,
				float.NaN,
				float.NaN,
				0,
				0,
				Probability.Low);

			CoachFareData fareData = new CoachFareData();
			fareData.IsQuotaFare = true;
			fareData.OriginNaptan = fare.OriginNaPTAN;
			fareData.DestinationNaptan = fare.DestinationNaPTAN;
			fareData.OperatorCode = operatorCode;
			
			cst.TicketCoachFareData = fareData;

			cst.LegNumber = legNo;
			cst.CombinedTicketIndex = index;

			return cst;
		}

	}
}