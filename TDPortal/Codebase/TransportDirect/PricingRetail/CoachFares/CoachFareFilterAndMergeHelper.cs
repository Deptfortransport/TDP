//********************************************************************************
//NAME         : CoachFareFilterAndMergeHelper.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 21/10/2005
//DESCRIPTION  : Implementation of CoachFareFilterAndMergeHelper.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/CoachFareFilterAndMergeHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:32   mturner
//Initial revision.
//
//   Rev 1.7   May 25 2007 16:22:12   build
//Automatically merged from branch for stream4401
//
//   Rev 1.6.1.4   May 22 2007 13:14:56   mmodi
//Updated logicto combine return fares following NX confirmation on rules
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.6.1.3   May 17 2007 15:20:50   mmodi
//Corrected issue with adding two fares together for a Return discounted ticket
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.6.1.2   May 17 2007 14:26:44   mmodi
//Updated Combing retur tickets logic
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.6.1.1   May 11 2007 14:39:36   mmodi
//Updated to use Ticket.ShortCode when using the CoachFaresLookup
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.6.1.0   May 10 2007 16:47:48   mmodi
//Added code for New NX fares combining return tickets
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.6   Mar 06 2007 13:43:44   build
//Automatically merged from branch for stream4358
//
//   Rev 1.5.1.0   Mar 02 2007 11:03:48   asinclair
//Pass extra parameter into new PricingResult
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.5   Jan 18 2006 18:16:28   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.4   Dec 01 2005 15:28:40   mguney
//AreTicketsMatching method changed to handle combining discount tickets.
//Resolution for 3262: DN040: SBP - Discounted Fares are not displayed properly
//
//   Rev 1.3   Nov 30 2005 14:12:24   mguney
//AreTicketsMatching method changed to return true when not dealing with the same ticket type.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.2   Nov 30 2005 09:40:08   RPhilpott
//Correct handling of OpenReturn/Return tickets when we have more than one return date with the same outward date. 
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.1   Oct 22 2005 16:20:18   mguney
//Associate IR
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 21 2005 11:58:16   mguney
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// CoachFareFilterAndMergeHelper.
	/// </summary>
	public class CoachFareFilterAndMergeHelper
	{
		/// <summary>
		/// Determines whether two Ticket objects 'match'.
		/// A coach Ticket will only ever have one fare value 
		/// populated: Adult, Child, DiscountedAdult, DiscountedChild.
		/// If two tickets are deemed to match, then the two fare values
		/// can be combined onto a single Ticket object. E.g. Standard Single 
		/// with Adult, Child and DiscountedAdult fares values.
		/// Internal visibility for NUnit testing purposes only.
		/// </summary>
		/// <param name="ticket1">Ticket</param>
		/// <param name="ticket2">Ticket</param>
		/// <returns>true if tickets match</returns>

		internal bool AreTicketsMatching(Ticket ticket1, Ticket ticket2)
		{
			bool result = false;
			// Ticket codes must match e.g. 'Standard Single'
			if (ticket1.Code == ticket2.Code)
			{
				// Set IsAdult and IsDiscounted flags
				bool ticket2IsDiscounted = ((!ticket2.DiscountedAdultFare.Equals(float.NaN)) || (!ticket2.DiscountedChildFare.Equals(float.NaN)));
				bool ticket1IsAdult = ((!ticket1.AdultFare.Equals(float.NaN)) || (!ticket1.DiscountedAdultFare.Equals(float.NaN)));
				bool ticket2IsAdult = ((!ticket2.AdultFare.Equals(float.NaN)) || (!ticket2.DiscountedAdultFare.Equals(float.NaN)));
				
				// If dealing with same type of ticket, fares only match if fares are the same.
				if (ticket1IsAdult == ticket2IsAdult)
				{
					if (ticket2IsDiscounted)
					{
						if (ticket2IsAdult)
						{
							result = ticket1.DiscountedAdultFare.Equals(float.NaN) ||
								(ticket1.DiscountedAdultFare == ticket2.DiscountedAdultFare);
						}
						else
						{
							result = ticket1.DiscountedChildFare.Equals(float.NaN) ||
								(ticket1.DiscountedChildFare == ticket2.DiscountedChildFare);
						}
					}
					else
					{
						if (ticket2IsAdult)
						{
							result = ticket1.AdultFare.Equals(float.NaN) || 
								(ticket1.AdultFare == ticket2.AdultFare);
						}
						else
						{
							result = ticket1.ChildFare.Equals(float.NaN) ||
								(ticket1.ChildFare == ticket2.ChildFare);
						}
					}
				}
					// Dealing with different types of ticket e.g. Adult and Child, so can combine
				else
				{
					result = true;
				}
			}
			return result;
		}


		/// <summary>
		/// Determines whether two Ticket objects 'match', e.g. they are both Adult tickets, 
		/// DiscountedAdultFare tickets. Method does NOT match if the Fare values are the same
		/// </summary>
		/// <param name="ticket1">Ticket</param>
		/// <param name="ticket2">Ticket</param>
		/// <param name="ignoreTicketCode">Ignores the Ticket code when checking for matching tickets</param>
		/// <returns>true if tickets match</returns>
		public bool AreTicketsMatchingFareType(Ticket ticket1, Ticket ticket2, bool ignoreTicketCode)
		{
			bool result = false;
			// Ticket codes must match e.g. 'Standard Single'
			// or for the new NX fares, we can add together two non matching codes
			if ((ticket1.Code == ticket2.Code) || (ignoreTicketCode))
			{
				// Set the type of ticket flags
				bool ticketIsDiscountedAdultFare = ((!ticket1.DiscountedAdultFare.Equals(float.NaN)) && (!ticket2.DiscountedAdultFare.Equals(float.NaN)));
				bool ticketIsDiscountedChildFare = ((!ticket1.DiscountedChildFare.Equals(float.NaN)) && (!ticket2.DiscountedChildFare.Equals(float.NaN)));
				bool ticketIsAdult = ((!ticket1.AdultFare.Equals(float.NaN)) && (!ticket2.AdultFare.Equals(float.NaN)));
				bool ticketIsChild = ((!ticket1.ChildFare.Equals(float.NaN)) && (!ticket2.ChildFare.Equals(float.NaN)));

				// We can only combine if the tickets are of the same type, i.e. Both adult;
				if ((ticketIsDiscountedAdultFare) || (ticketIsDiscountedChildFare) || (ticketIsAdult) || (ticketIsChild))
					result = true;				
			}

			return result;
		}

		/// <summary>
		/// Method loops through Tickets collection of a PricingUnit and sees if
		/// any of the tickets can be combined. A Coach ticket will only ever have
		/// one fare value set, e.g. Adult, Child, DiscountedAdult, DiscountedChild.
		/// Matching Tickets will be combined together to create Tickets of the same
		/// type with multiple fare values set e.g. a Standard Single ticket with
		/// values for Adult, Child and DiscountedAdult fares.
		/// Internal visibility for NUnit testing purposes only.
		/// </summary>
		/// <param name="pricingResult">PricingResult to process</param>
		/// <returns>Processed PricingResult</returns>
		internal PricingResult CombinePricingResultTickets (PricingResult pricingResult)
		{
			IList origTickets = new ArrayList();
			ArrayList combinedTickets = new ArrayList();
			// Set reference to the original tickets collection
			origTickets = pricingResult.Tickets;
			
			// For each original Ticket
			foreach (Ticket origTicket in origTickets)
			{ 
				int ticketToCombine = -1;
				// See if it can be combined with another
				for (int i=0; i<combinedTickets.Count; i++)
				{
					if (AreTicketsMatching((Ticket)combinedTickets[i], origTicket))
					{
						ticketToCombine = i;
						break;					
					}
				}
				// Have found a ticket with which to combine so update combineTickets collection
				if (ticketToCombine > -1)
				{
					combinedTickets[ticketToCombine] = CombineTickets((Ticket)combinedTickets[ticketToCombine], origTicket);
				}
					// Haven't found a ticket with which we can combine
				else
				{
					combinedTickets.Add(origTicket);
				}
			}
			// Update Tickets collection of PricingResult to new 'combined' collection
			pricingResult.Tickets = combinedTickets;

			return pricingResult;
		}

		/// <summary>
		/// Method 'combines' two tickets, updating the relevant fare
		/// field of the first ticket with the value from the second.
		/// </summary>
		/// <param name="origTicket">Ticket</param>
		/// <param name="newTicket">Ticket to combine</param>
		/// <returns>Combined Ticket</returns>
		private Ticket CombineTickets(Ticket origTicket, Ticket newTicket)
		{
			if (!newTicket.AdultFare.Equals(float.NaN))
				origTicket.AdultFare = newTicket.AdultFare;
			else if (!newTicket.ChildFare.Equals(float.NaN))
				origTicket.ChildFare = newTicket.ChildFare;
			else if (!newTicket.DiscountedAdultFare.Equals(float.NaN))
				origTicket.DiscountedAdultFare = newTicket.DiscountedAdultFare;
			else if (!newTicket.DiscountedChildFare.Equals(float.NaN))
				origTicket.DiscountedChildFare = newTicket.DiscountedChildFare;

			return origTicket;
		}

		/// <summary>
		/// The complete set of fares associated with a journey may be larger than the ones that we are able to display.
		/// For example, certain return fares are only valid if the user has selected both and outward and return journey.
		/// This method filters invalid fares before they are associated with a PricingUnit.
		/// This method doesn't need unit testing as the logic used in this method(CombinePricingResultTickets) is already tested.
		/// </summary>
		/// <param name="existingResult"></param>
		/// <returns></returns>
		public PricingResult FilterFares(PricingResult existingResult)
		{
			PricingResult filteredResult;
			// It may be that the are no results at all (this would happen if no fares information was provided in the first place
			// If this is the case then we create an empty PricingResult.
			// Otherwise we assumed that the filtered results will look like the unfiltered results
			if (existingResult != null) 			
				filteredResult = (PricingResult)existingResult.Clone();											 
			else 			
				filteredResult = new PricingResult(0,0, false, false, false);			

			// Combine matching Tickets e.g. Standard Single Adult and Standard Single Child
			return CombinePricingResultTickets(filteredResult);
		}

		/// <summary>
		/// Combines the Coach Return component fares
		/// </summary>
		/// <param name="outwardPricingUnit"></param>
		/// <param name="returnPricingUnit"></param>
		/// <returns>PricingResult of the returnFares</returns>
		public PricingResult CombineReturnCoachFares(PricingUnit outwardPricingUnit, PricingUnit returnPricingUnit)
		{
			PricingResult outwardPR = outwardPricingUnit.ReturnFares;
			PricingResult returnPR = returnPricingUnit.ReturnFares;
			PricingResult combinedPR = new PricingResult(outwardPR.MinChildAge, outwardPR.MaxChildAge, outwardPR.NoPlacesAvailableForSingles, outwardPR.NoPlacesAvailableForReturns, outwardPR.NoThroughFaresAvailable);

			IList outwardTickets = new ArrayList();
			IList returnTickets = new ArrayList();
			ArrayList combinedTickets = new ArrayList(); // Array which contains all the combinations of return tickets
			
			// Set reference to the original tickets collection
			outwardTickets = outwardPR.Tickets;
			returnTickets = returnPR.Tickets;
			
			#region Combine tickets

			foreach (Ticket outwardTicket in outwardTickets)
			{
				foreach (Ticket returnTicket in returnTickets)
				{
					if (AreTicketsMatchingFareType(outwardTicket, returnTicket, false))
					{
						combinedTickets.Add(AddTickets(outwardTicket, returnTicket));
					}
				}				
			}
		
			#endregion         

			// Update Tickets collection of PricingResult to new 'combined' collection
			combinedPR.Tickets = combinedTickets;

			return combinedPR;
		}

		/// <summary>
		/// Method 'adds' two tickets, adding the relevant fare
		/// field of the first ticket with the value from the second.
		/// </summary>
		/// <param name="origTicket">Ticket to add to</param>
		/// <param name="newTicket">Ticket to add from</param>
		/// <returns>Combined Ticket</returns>
		private Ticket AddTickets(Ticket ticket1, Ticket ticket2)
		{
			Ticket newTicket = (Ticket)ticket1.Clone();
			
			// Only add the fares if both tickets have a value, else set to null
			if ((!ticket1.AdultFare.Equals(float.NaN)) && (!ticket2.AdultFare.Equals(float.NaN)))
				newTicket.AdultFare = ticket1.AdultFare + ticket2.AdultFare;
			else
				newTicket.AdultFare = float.NaN;
			
			if ((!ticket1.ChildFare.Equals(float.NaN)) && (!ticket2.ChildFare.Equals(float.NaN)))
				newTicket.ChildFare = ticket1.ChildFare + ticket2.ChildFare;
			else 
				newTicket.ChildFare = float.NaN;
			
			if ((!ticket1.DiscountedAdultFare.Equals(float.NaN)) && (!ticket2.DiscountedAdultFare.Equals(float.NaN)))
				newTicket.DiscountedAdultFare = ticket1.DiscountedAdultFare + ticket2.DiscountedAdultFare;
			else
				newTicket.DiscountedAdultFare = float.NaN;
			
			if ((!ticket1.DiscountedChildFare.Equals(float.NaN)) && (!ticket2.DiscountedChildFare.Equals(float.NaN)))
				newTicket.DiscountedChildFare = ticket1.DiscountedChildFare + ticket2.DiscountedChildFare;
			else
				newTicket.DiscountedChildFare = float.NaN;

			return newTicket;
		}
	}
}
