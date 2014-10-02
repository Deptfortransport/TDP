//********************************************************************************
//NAME         : PricingResultBuilder.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 19/10/2003
//DESCRIPTION  : Implementation of PricingResultBuilder class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RBOGateway/PricingResultBuilder.cs-arc  $
//
//   Rev 1.3   Jul 12 2010 11:36:04   mmodi
//Corrected logic in keeping the cheapest fare found (for a Ticket code and Route code). To resolve problem when multiple fare results contained the same fare with different prices and the cheapest wasn't always retained.
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.2   Mar 26 2010 11:55:42   mmodi
//Updated to allow "duplicate" fares to be shown (but code commented out until approved for release)
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.1   Feb 10 2009 15:26:38   mmodi
//Added check for a cheaper fare
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//Resolution for 5243: Routeing Guide - London terminal journeys all go to Blackfriars
//
//   Rev 1.0   Nov 08 2007 12:37:12   mturner
//Initial revision.
//
//   Rev 1.24   Mar 06 2007 13:43:48   build
//Automatically merged from branch for stream4358
//
//   Rev 1.23.1.0   Mar 02 2007 11:13:30   asinclair
//Pass in NoThroughFares bool when creating a new PricingResult
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.23   Jan 18 2006 18:16:42   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.22   Dec 15 2005 20:43:44   RPhilpott
//Include tickets with adult undiscounted fare of NaN.
//Resolution for 3373: Incorrect display of availablility with railcards
//
//   Rev 1.21   Nov 24 2005 18:22:56   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.20   Nov 23 2005 15:53:08   RPhilpott
//Fix incorrect availability reporting of inward single and return fares by excluding irrelevant fares from all processing (returns) and from NRS queries (inward singles on outward legs). 
//Resolution for 3101: DN039 - NRS - Single Fares in Return Journeys
//
//   Rev 1.19   Nov 15 2005 19:44:08   RPhilpott
//Remove check for displayable tickets (now done in RetailBusinessObjects).
//Resolution for 3037: DN040: User responsiveness of SBP fare requests
//
//   Rev 1.18   May 09 2005 12:30:58   RPhilpott
//Remove "duplicate" tickets if adult and child fares and codes are the same and route codes differ, retaining those with route code "Any Permitted".
//Resolution for 2457: PT - Find a Fare - Train fares displayed twice on Select a Fare page
//
//   Rev 1.17   Apr 28 2005 18:05:32   RPhilpott
//Split noPlacesAvaialble flag into singles and returns.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.16   Apr 28 2005 16:03:54   RPhilpott
//Add "NoPlacesAvailable" property to PricingResult to indicate that valid fares have been found, but with no seat availability.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.15   Apr 27 2005 16:45:52   rgeraghty
//CreateTicket method updated
//Resolution for 2340: PT - UI issue with Door-to-door and discount cards
//
//   Rev 1.14   Apr 22 2005 11:46:04   RPhilpott
//Handling of "partial errors" returned by RBO's. 
//Resolution for 2247: PT - Error handling by Retail Business Objects
//
//   Rev 1.13   Apr 12 2005 10:04:48   RPhilpott
//Set minimum fares correctlly.
//Resolution for 2071: PT: Minimum train fares not flagged
//
//   Rev 1.12   Apr 11 2005 16:08:34   RPhilpott
//Make handling of missing discounts more sophisticated  
// - resetting discounted fare to NaN if it is the same as the undiscounted value 
//    (for example, a child fare with an adult-only railcard).
//Resolution for 2065: PT: Find Fare Ticket Selection always displays discount fares
//
//   Rev 1.11   Apr 11 2005 14:44:12   RPhilpott
//Leave discounted fare as default value of "NaN" if no discount requested or no discounted fare.
//Resolution for 2065: PT: Find Fare Ticket Selection always displays discount fares
//
//   Rev 1.10   Mar 30 2005 16:54:50   RPhilpott
//Add upgrade information to time-based tickets.
//
//   Rev 1.9   Mar 22 2005 16:08:56   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.8   Jun 15 2004 11:50:00   jgeorge
//Changed call to new Ticket constructor to use ShortCode. 
//
//   Rev 1.7   Nov 24 2003 16:28:40   acaunt
//Tickets are handled by the key of TicketCode + RouteCode, not just TicketCode.
//
//Also tickets ordered before being returned.
//
//   Rev 1.6   Nov 04 2003 16:07:18   acaunt
//Now handles NaNs correctly
//
//   Rev 1.5   Oct 27 2003 16:15:36   acaunt
//Ticket codes are now mapped to ticket name and appropriate flexibility using DataServices values
//
//   Rev 1.4   Oct 26 2003 15:52:08   acaunt
//defaults discounted values to non-discounted values
//
//   Rev 1.3   Oct 23 2003 10:26:10   acaunt
//TrainDto.TicketName -> TrainDto.TicketCode
//
//   Rev 1.2   Oct 20 2003 21:41:56   acaunt
//corrected isDiscount problem
//
//   Rev 1.1   Oct 20 2003 16:46:50   acaunt
//Bug fixes
//
//   Rev 1.0   Oct 20 2003 10:19:08   acaunt
//Initial Revision

using System;
using System.Collections;
using System.Data;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Class to encapsulate the construction of a PricingRequest object
	/// </summary>
	public class PricingResultBuilder
	{
		private static IDataServices dataServices = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

		private const string ANY_PERMITTED_ROUTE = "00000";

		private PricingResult pricingResult;
		// We store the tickets in a HashMap so that we can select individual tickets based on their names.
		private Hashtable tickets;

		public PricingResultBuilder()
		{
		}

		/// <summary>
		/// Create the outline of a PricingResult from a PricingResultDto
		/// </summary>
		/// <param name="result"></param>
		public void CreatePricingResult(PricingResultDto result)
		{
			pricingResult = new PricingResult(result.MinChildAge, result.MaxChildAge, result.NoPlacesAvailableForSingles, result.NoPlacesAvailableForReturns, result.NoThroughFaresAvailable);
			tickets = new Hashtable();
		}

		/// <summary>
		/// Fares are identified as adult undiscounted, adult discounted, child undiscounted, child discounted
		/// If a particular fare isn't available (it wasn't returned by the FBO or was returned with a value of NaN) we substitute it with the most
		/// appropriate fare that it there as follows:
		/// 
		/// child undiscounted	-> adult undiscounted
		/// adult discounted	-> adult undiscounted
		/// child discounted	-> child undiscounted (-> adult undiscounted)
		/// 
		/// If a ticket hasn't got an undiscounted adult fare then it can't be shown
		/// </summary>
		/// <param name="ticketDto">DTO wrapping the fare details</param>
		public void AddTicketDto(TicketDto ticketDto)
		{
			Ticket ticket = null; // the ticket we will be creating or updating

			// Retrieve the ticket code and route code so that we can use a key
            
            // Added to allow fares which apply to different NLCs to be shown, as they are not 
            // really duplicates - UK:4782040
            // MM - Commented out as this has not been approved yet.
			//TicketUniqueKey key = new TicketUniqueKey(ticketDto.TicketCode + 
            //    ticketDto.OriginNlc + ticketDto.FareOriginNlcActual + 
            //    ticketDto.DestinationNlc + ticketDto.FareDestinationNlcActual, ticketDto.RouteCode);
            TicketUniqueKey key = new TicketUniqueKey(ticketDto.TicketCode, ticketDto.RouteCode);

			// The absence of a railcard indicates that this is an undiscounted ticket
			bool isDiscounted = !(ticketDto.Railcard.Trim().Length == 0);

			// See if we already have this ticket
			bool match = tickets.Contains(key);

            #region Create and add ticket to collection

            // If we haven't create it and add it to our collection of tickets
			if (match) 
			{
				ticket = (Ticket)tickets[key];

                // If the ticket has already been created, and the new one is cheaper,
                // then replace with this new one. This is likely to occur in Search by price 
                // where a group station is selected
                if (ticket.AdultFare > ticketDto.AdultFare && !isDiscounted)
                {
                    Ticket newTicket = CreateTicket(ticketDto);

                    // Persist the discounted fares from the matched ticket
                    newTicket.DiscountedAdultFare = ticket.DiscountedAdultFare;
                    newTicket.DiscountedChildFare = ticket.DiscountedChildFare;

                    if (ticketDto.IsFromCostSearch)
                    {
                        ((CostSearchTicket)newTicket).MinimumAdultFare = ((CostSearchTicket)ticket).MinimumAdultFare;
                        ((CostSearchTicket)newTicket).MinimumChildFare = ((CostSearchTicket)ticket).MinimumChildFare;
                    }

                    // Place newTicket into the hashtable
                    tickets[key] = newTicket;

                    // And assign the newTicket to our current working ticket
                    ticket = newTicket;
                }
			}
			else
			{
				ticket = CreateTicket(ticketDto);
				tickets.Add(key, ticket);
            }

            #endregion

            #region Set fare values for the ticket

            // Set the appropriate fares (discounted or undiscounted)
            if (isDiscounted)
            {
                //only update the discounted fares if they are not NaN
                if (!ticketDto.AdultFare.Equals(float.NaN))
                {
                    ticket.DiscountedAdultFare = ticketDto.AdultFare;
                }

                if (!ticketDto.ChildFare.Equals(float.NaN))
                {
                    ticket.DiscountedChildFare = ticketDto.ChildFare;
                }

                if (ticketDto.IsFromCostSearch)
                {
                    ((CostSearchTicket)ticket).MinimumAdultFare = ticketDto.MinimumAdultFare;
                    ((CostSearchTicket)ticket).MinimumChildFare = ticketDto.MinimumChildFare;
                }
            }
            else
            {
                // CreateTicket sets the fare values to NaN - so use the current
                // ticketDto to populate the fare.
                //
                // If ticket is a "match", then the ticket should already have had its fare
                // values set.  So only set the fare value if this current ticketDto is cheaper.
                // If "match" and a new ticket was created (because it was cheaper), then fare value
                // is NaN, so ok to set the new fare using the current ticketDto.
                //
                // This additional logic is required because SearchByPrice can have multiple PricingResultDto
                // in the GatewayTransform caller, where the "same" ticket can exist but with different fare 
                // values. And depending on the order they arrive into this logic, we don't want to 
                // overwrite a "matched" ticket cheaper fare value with a more expensive one (as was being 
                // done before adding this if check).
                if ((ticket.AdultFare.Equals(float.NaN)) || (ticket.AdultFare > ticketDto.AdultFare))
                {
                    ticket.AdultFare = ticketDto.AdultFare;
                }

                // If child fare hasn't been set, then use ticketDto to populate
                if (ticket.ChildFare.Equals(float.NaN))
                {
                    // if the child fare isn't available, set it to the adult fare
                    if (!ticketDto.ChildFare.Equals(float.NaN))
                    {
                        ticket.ChildFare = ticketDto.ChildFare;
                    }
                    else
                    {
                        ticket.ChildFare = ticketDto.AdultFare;
                    }
                }
                    // Else child fare has already been populated, only update if current ticketDto
                    // being used is cheaper
                else if ((!ticketDto.ChildFare.Equals(float.NaN)) && (ticket.ChildFare > ticketDto.ChildFare))
                {
                    ticket.ChildFare = ticketDto.ChildFare;
                }

            }

            #endregion

            // If we have updated fare information for a matching ticket, make sure that the undiscounted value is less than the discounted value
			// (this may not happen as the discounted value has been set to the minimum fare value, which could be larger than the undiscounted fare (no comments please))
			
			if (match) 
			{
				if	(!float.IsNaN(ticket.AdultFare))
				{
					ticket.DiscountedAdultFare = Math.Min(ticket.AdultFare, ticket.DiscountedAdultFare);
				}

				if	(!float.IsNaN(ticket.ChildFare))
				{
					ticket.DiscountedChildFare = Math.Min(ticket.ChildFare, ticket.DiscountedChildFare);
				}

				if	(ticket.DiscountedAdultFare == ticket.AdultFare)
				{
					ticket.DiscountedAdultFare = float.NaN;
				}

				if	(ticket.DiscountedChildFare == ticket.ChildFare)
				{
					ticket.DiscountedChildFare = float.NaN;
				}
			
			}

			// if any displayable supplements have been found for this fare,
			//  add an upgrade object for each to the ticket

			foreach (SupplementDto supplement in ticketDto.Supplements)
			{
				ticket.AddUpgrade(new Upgrade(supplement.Code, supplement.Description, supplement.Cost));
			}

		}

		/// <summary>
		/// Put the PricingRequest together and return to the client
		/// </summary>
		/// <returns></returns>
		public PricingResult GetPricingResult()
		{
			// We filter the tickets to ensure there is an adult undiscounted price for all the fares we can show.
			// (Undiscounted adult fares are the one type of fare that are always shown).
			
			ArrayList filteredTickets = new ArrayList(tickets.Values.Count);
			
			foreach (Ticket ticket in tickets.Values)
			{
				if (!ticket.AdultFare.Equals(float.NaN) || !ticket.DiscountedAdultFare.Equals(float.NaN))
				{
					filteredTickets.Add(ticket);
				}
			}
			// Sort the list of filtered tickets by adult fare, child fare, ticket code and route. 
			
			filteredTickets.Sort(new TicketOrderingComparer());
			
			// Now refilter the sorted list to remove any with identical fares and ticket types
			//  but different route codes where one of the routes is "Any Permitted".  

			// Note that since route AP is "00000", any tickets with this code 
			//  will be appear before any otherwise identical tickets with other routes.

			ArrayList reFilteredTickets = new ArrayList(filteredTickets.Count);

			Ticket previousTicket = null;
			
			foreach (Ticket ticket in filteredTickets)
			{
				if	(previousTicket != null)
				{
					if	(ticket.AdultFare == previousTicket.AdultFare
						&& ticket.ChildFare == previousTicket.ChildFare
						&& ticket.ShortCode == previousTicket.ShortCode)
					{
                        // Added to allow fares which apply to different NLCs to be shown, as they are not 
                        // really duplicates - UK:4782040
                        // MM - Commented out as this has not been approved yet.
                        //if (ticket.TicketRailFareData.OriginNlc == previousTicket.TicketRailFareData.OriginNlc
                        //    && ticket.TicketRailFareData.OriginNlcActual == previousTicket.TicketRailFareData.OriginNlcActual
                        //    && ticket.TicketRailFareData.DestinationNlc == previousTicket.TicketRailFareData.DestinationNlc
                        //    && ticket.TicketRailFareData.DestinationNlcActual == previousTicket.TicketRailFareData.DestinationNlcActual)
                        {

                            if (previousTicket.TicketRailFareData.RouteCode.Equals(ANY_PERMITTED_ROUTE))
                            {
                                continue;
                            }
                        }
					}
				}

				previousTicket = ticket;
					
				reFilteredTickets.Add(ticket);
			}
		
			pricingResult.Tickets = reFilteredTickets;
			
			return pricingResult;
		}

		/// <summary>
		/// Create new Ticket object
		/// </summary>
		/// <param name="ticketDto"></param>
		/// <returns></returns>
		private Ticket CreateTicket(TicketDto ticketDto)
		{
			// Find the details object corresponding to the ticket code
			CategorisedHashData info = dataServices.FindCategorisedHash(DataServiceType.DisplayableRailTickets, ticketDto.TicketCode);

			// Extract the ticket name and map the ticket flexibility			
			if	(ticketDto.IsFromCostSearch)
			{
				// we are in CostSearch mode and need to return a CostSearchTicket ...				
				CostSearchTicket csTicket = new CostSearchTicket(info.Value, (Flexibility) Enum.Parse(typeof(Flexibility), info.Category), ticketDto.TicketCode); 
				csTicket.TicketRailFareData = new RailFareData(ticketDto);
				return csTicket;	
			}
			else
			{
				// we are in time-based mode and the less specialised Ticket class is needed ... 
				Ticket ticket = new Ticket(info.Value, (Flexibility) Enum.Parse(typeof(Flexibility), info.Category), ticketDto.TicketCode);
				ticket.TicketRailFareData = new RailFareData(ticketDto);				
				return ticket;
			}
		}
	}
}
