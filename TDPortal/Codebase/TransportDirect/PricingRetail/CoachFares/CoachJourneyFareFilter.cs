// ***********************************************
// NAME			: CoachJourneyFareFilter.cs
// AUTHOR		: Murat Guney
// DATE CREATED	: 25/10/2005
// DESCRIPTION	: CoachJourneyFareFilter implementation.
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/CoachJourneyFareFilter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:34   mturner
//Initial revision.
//
//   Rev 1.7   Jan 16 2006 09:30:40   mguney
//A check to see if all the tickets are used is included in FilterJourneyList method.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.6   Dec 05 2005 14:53:40   mguney
//Restriction methods changed.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.5   Nov 29 2005 18:12:46   mguney
//OutOfRestrictedTime method changed to handle empty time restrictions array.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.4   Nov 26 2005 11:57:48   mguney
//Restricted Time method changed. It is now returning false if the leg start time is in one of the restricted times in the array.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.3   Nov 07 2005 20:36:48   RPhilpott
//Handle null CoachOperator when opertaor not found
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Nov 03 2005 19:23:22   RPhilpott
//Merge undiscounted and discounted CoachFareData into one.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 28 2005 14:51:16   mguney
//Associated IR.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 26 2005 15:53:08   mguney
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price

using System;
using System.Collections;

using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.CoachFares;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;
using TransportDirect.UserPortal.JourneyControl;
using CJP = TransportDirect.JourneyPlanning.CJPInterface;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Filters the journeys given in the tdjourneyresult according to the information in the given ticket list.
	/// </summary>
	public class CoachJourneyFareFilter : IJourneyFareFilter
	{
		
		#region Private methods

		/// <summary>
		/// Checks if there is any quota based tickets in the ticket list.
		/// </summary>
		/// <param name="tickets"></param>
		/// <returns></returns>
		private bool QuataBasedTicketsExist(CostSearchTicket[] tickets)
		{
			foreach (CostSearchTicket ticket in tickets)
			{
				if ((ticket.TicketCoachFareData != null) && (ticket.TicketCoachFareData.IsQuotaFare))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Checks if the provided time is within the restricted time range for the given ticket.
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="timeToCheck"></param>
		/// <returns></returns>
		private bool OutOfRestrictedTime(CostSearchTicket ticket,TDDateTime timeToCheck)
		{
			DateTime time = timeToCheck.GetDateTime();

			if ((ticket.TicketCoachFareData != null) && (ticket.TicketCoachFareData.TimeRestrictions != null)
				&& (ticket.TicketCoachFareData.TimeRestrictions.Length > 0))
			{
				foreach (TimeRestriction restriction in ticket.TicketCoachFareData.TimeRestrictions)
				{
					if ((time >= restriction.StartTime) && (time <= restriction.EndTime))
						return false;
				}
			}
			else return false;

			return true;
		}

		/// <summary>
		/// Checks if the provided operator code is in the restricted operator codes of the given ticket.
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		private bool IsRestrictedOperatorCode(CostSearchTicket ticket,string operatorCode)
		{
			if ((ticket.TicketCoachFareData != null) && (ticket.TicketCoachFareData.RestrictedOperatorCodes != null)
				&& (ticket.TicketCoachFareData.RestrictedOperatorCodes.Length > 0))
			{
				foreach (string restrictedOperatorCode in ticket.TicketCoachFareData.RestrictedOperatorCodes)
				{
					if (operatorCode.Equals(restrictedOperatorCode))
						return false;
				}
			}
			else return false;

			return true;
		}

		/// <summary>
		/// Checks if the provided service number is in the restricted service numbers of the given ticket.
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="serviceNumber"></param>
		/// <returns></returns>
		private bool IsRestrictedService(CostSearchTicket ticket,string serviceNumber)
		{
			if ((ticket.TicketCoachFareData != null) && (ticket.TicketCoachFareData.RestrictedServices != null)
				&& (ticket.TicketCoachFareData.RestrictedServices.Length > 0))
			{
				foreach (string restrictedService in ticket.TicketCoachFareData.RestrictedServices)
				{
					if (serviceNumber.Equals(restrictedService))
						return false;
				}
			}
			else return false;

			return true;
		}

		/// <summary>
		/// Gets the start date time for the ticket in the given public journey.
		/// Goes through the legs of the journey and tries to match the naptan of a leg with the
		/// origin or destination naptan of the ticket (depending on being outward or return).
		/// </summary>
		/// <param name="publicJourney"></param>
		/// <param name="ticket"></param>
		/// <param name="returnJourneyList"></param>
		/// <returns></returns>
		private TDDateTime GetStartDateTimeForTicket(object publicJourney,CostSearchTicket ticket,
			bool returnJourneyList)
		{	
			string legStartNaptan = (returnJourneyList) ? ticket.TicketCoachFareData.DestinationNaptan
				: ticket.TicketCoachFareData.OriginNaptan;

			foreach (PublicJourneyDetail leg in ((PublicJourney)publicJourney).Details)
			{
				if (leg.LegStart.Location.NaPTANs[0].Naptan.Equals(legStartNaptan))
					return leg.LegStart.DepartureDateTime;
			}
			//If the corresponding naptan not found, return a past datetime.
			//The coach fare interface will return a result with error in that case.
			return new TDDateTime(DateTime.Now.AddHours(-1));
		}

		/// <summary>
		/// Checks if the quota fare of the given tickets match with the fare values obtained
		/// by calling the coach fares interface method.
		/// </summary>
		/// <param name="journey"></param>
		/// <param name="tickets"></param>
		/// <param name="returnJourneyList"></param>
		/// <returns></returns>
		private bool QuotaFaresMatch(object journey,CostSearchTicket[] tickets,bool returnJourneyList)
		{			
			//Go through the tickets for each journey
			//if there is any quota based tickets, (this method is called when there is anyway)
			//double check the fare value with the fares in the result 
			//obtained by calling the fares interface GetCoachFares method.
			foreach (CostSearchTicket ticket in tickets)
			{
				CoachFareData coachFareData = ticket.TicketCoachFareData;

				if (coachFareData.IsQuotaFare)
				{
					//prepare the request
					FareRequest fareRequest = new FareRequest();
					fareRequest.OperatorCode = coachFareData.OperatorCode;
					fareRequest.OriginNaPTAN = new TDNaptan(coachFareData.OriginNaptan,null);
					fareRequest.DestinationNaPTAN = new TDNaptan(coachFareData.DestinationNaptan,null);
					fareRequest.OutwardStartDateTime = 
						GetStartDateTimeForTicket((PublicJourney)journey,ticket,returnJourneyList);		
			
					//get the fares interface from the factory
					ICoachFaresInterfaceFactory factory = (ICoachFaresInterfaceFactory)
						TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachFaresInterface];									
					IFaresInterface faresInterface = 
						(IFaresInterface)factory.GetFaresInterface(coachFareData.OperatorCode);
					//get the result
					FareResult result = faresInterface.GetCoachFares(fareRequest);
					if (result.ErrorStatus == FareErrorStatus.Error)
					{
						Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
							"Couldn't get the fare information from the CoachFareInterfaces."));
						return false;
					}

					//Go through the fares in the result.
					//If the quota fare is not found, remove the journey.
					bool quotaFareFound = false;
					foreach (CoachFare coachFare in result.Fares)
					{						
						if (((float)(coachFare.Fare/100.00) == ticket.AdultFare) &&
							(coachFare.FareType.Equals(ticket.Code)))							
						{
							quotaFareFound = true;
							break;
						}
					}
					//If the quota fare is not found, remove the journey.
					if (!quotaFareFound)
					{
						return false;							
					}
				}//if quota fare					
			}//for each ticket
			
			return true;
		}

		/// <summary>
		/// Filters the journeys according to the ticket information. When a return journey is involved,
		/// the same tickets will be used but in reverse order.
		/// </summary>
		/// <param name="journeys"></param>
		/// <param name="tickets"></param>
		/// <param name="returnJourneyList"></param>
		private void FilterJourneyList(ArrayList journeys,CostSearchTicket[] tickets,bool returnJourneyList)
		{		
			ArrayList removeJourneyList = new ArrayList();
			//get the operatorlookup from service discovery
			ICoachOperatorLookup coachOperatorLookup = (ICoachOperatorLookup)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];			
			//check no tickets condition
			if (tickets.Length == 0)
				return;
				
			//
			bool quataBasedTicketsExist = QuataBasedTicketsExist(tickets);
			//go through all the journeys in the list
			for (int i=0;i < journeys.Count;i++) 
			{
				bool journeyRemoved = false;
				//tickets will be checked from backwards if return journey
				int ticketIndex = (returnJourneyList) ? tickets.Length-1 : 0;
				//go through all the legs in the journey
				foreach (PublicJourneyDetail leg in ((PublicJourney)(journeys[i])).Details)
				{					
					if (tickets.Length > 1)
					{
						//Change the ticket index if via location is reached.
						//This will be handled differently for outward and return journey.
						//Check tickets starting from backwards if return journey.
						string viaNaptan = (returnJourneyList) 
							? tickets[ticketIndex].TicketCoachFareData.OriginNaptan
							: tickets[ticketIndex].TicketCoachFareData.DestinationNaptan;

						if (leg.LegStart.Location.NaPTANs[0].Naptan.Equals(viaNaptan))						
						{
							ticketIndex = ticketIndex + (1* ((returnJourneyList) ? -1 : +1));
						}
					}

					
					//If it is a coach leg, check for conditions to remove journeys from the journey list					
					if ((leg.Mode == CJP.ModeType.Coach) || (leg.Mode == CJP.ModeType.Bus))
					{
						//There is only 1 service for coaches,so use the first one to get the cjp operator code.
						string cjpLegOperatorCode = leg.Services[0].OperatorCode;
						//1.
						//	Check the operator for the current leg and see if it matches with the current ticket.
						//	If it doesn't match remove the whole journey from the list.												
						
						CoachOperator op = coachOperatorLookup.GetOperatorDetails(cjpLegOperatorCode);

						if	(op == null || op.OperatorCode == null || op.OperatorCode.Length == 0 
							|| (!op.OperatorCode.Equals(tickets[ticketIndex].TicketCoachFareData.OperatorCode)))		
						{
							removeJourneyList.Add(journeys[i]);							
							journeyRemoved = true;
							break;
						}

						//2.
						//	If none of the tickets is quota-based, then it is necessary to remove journeys:
						//		a. If departure time of the leg falls withing a TimeRestriction band
						//		b. If CJP-supplied operator code is in RestrictedOperatorCodes
						//		c. If service number is in RestrictedServices.
						//check if any of the tickets include quota based fares
						if(!quataBasedTicketsExist)
						{
							if (OutOfRestrictedTime(tickets[ticketIndex],leg.LegStart.DepartureDateTime) ||
								IsRestrictedOperatorCode(tickets[ticketIndex],cjpLegOperatorCode) ||
								IsRestrictedService(tickets[ticketIndex],leg.Services[0].ServiceNumber))
							{
								removeJourneyList.Add(journeys[i]);
								journeyRemoved = true;
								break;
							}														
						}						
						
					}//if coach leg
				}//for each leg

				//make sure that all the tickets are used. if not remove the journey.
				if (
					((returnJourneyList) && (ticketIndex != 0)) ||
					((!returnJourneyList) && (ticketIndex != (tickets.Length-1))))
				{					
					removeJourneyList.Add(journeys[i]);
					journeyRemoved = true;					
				}

				//3. 
				//	If any of the tickets is quota-based, it is necessary to filter the individual journeys,
				//	which don't have the quota fare assigned to the ticket.
				if( (!journeyRemoved) && quataBasedTicketsExist)
				{
					if (!QuotaFaresMatch(journeys[i],tickets,returnJourneyList))
						removeJourneyList.Add(journeys[i]);
				}

			}//for each journey		
	
			//remove the invalid journeys			
			for (int i=0;i < removeJourneyList.Count;i++)
				journeys.Remove(removeJourneyList[i]);
		}

		#endregion

		#region IJourneyFareFilter Members

		/// <summary>
		/// Filters the journeys given in the TDJourneyResult using the information in the provided tickets.
		/// 
		/// 1.
		///		Check the operators for the legs and see if they match with the current ticket.
		///		If any of them doesn't match remove the whole journey from the list.	
		///	2.
		///		If none of the tickets is quota-based, then it is necessary to remove journeys:
		///		a. If departure time of the leg falls withing a TimeRestriction band
		///		b. If CJP-supplied operator code is in RestrictedOperatorCodes
		///		c. If service number is in RestrictedServices.
		/// 3.
		///		If any of the tickets is quota-based, it is necessary to filter the individual journeys,
		///		which don't have the quota fare assigned to the ticket.
		/// </summary>
		/// <param name="originalJourney"></param>
		/// <param name="tickets"></param>
		/// <returns>The changed journey result.</returns>
		public TDJourneyResult FilterJourneys(TDJourneyResult originalJourney, CostSearchTicket[] tickets)
		{
			FilterJourneyList(originalJourney.OutwardPublicJourneys,tickets,false);
			FilterJourneyList(originalJourney.ReturnPublicJourneys,tickets,true);

			return originalJourney;
		}

		#endregion
	}
}
