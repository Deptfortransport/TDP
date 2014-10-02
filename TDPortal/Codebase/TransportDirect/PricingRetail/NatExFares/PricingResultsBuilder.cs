//********************************************************************************
//NAME         : PricingResultsBuilder.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 21/10/2003
//DESCRIPTION  : Implementation of PricingResultsBuilder class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/NatExFares/PricingResultsBuilder.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:06   mturner
//Initial revision.
//
//   Rev 1.8   Apr 28 2005 18:05:30   RPhilpott
//Split noPlacesAvaialble flag into singles and returns.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.7   Apr 28 2005 16:03:54   RPhilpott
//Add "NoPlacesAvailable" property to PricingResult to indicate that valid fares have been found, but with no seat availability.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.6   Mar 23 2005 09:39:28   jbroome
//Extracted functionality into new PricingResultsHelper class
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.5   Jan 14 2005 11:16:44   jmorrissey
//Fixed warnings being generated in ConvertFlexibility method 
//
//   Rev 1.4   Jun 11 2004 15:35:40   acaunt
//Now uses TicketNameRule to generate appropriate SCL or NatEx ticket name.
//
//   Rev 1.3   May 28 2004 13:53:22   acaunt
//Updated to accomodate real NatEx and SCL data and with revised display rules.
//
//   Rev 1.2   Nov 24 2003 16:27:42   acaunt
//Coach fares are ordered before being stored
//
//   Rev 1.1   Oct 22 2003 23:08:58   acaunt
//unit testing bug fixes
//
//   Rev 1.0   Oct 22 2003 10:15:32   acaunt
//Initial Revision
using System;
using System.Collections;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Class to encapsulate the creation of the set of PricingResults
	/// corresponding to pricing information provided by the CJP
	/// To use this object:
	/// 1. Add all the undiscounted fares
	/// 2. Add all the discount cards we need to support
	/// 3. Add all the discount fare information
	/// 
	/// If discount fare information is not provided for a particular discount card then the fares associated
	/// with that card are taken to be the same as the undiscounted fares
	/// 
	/// The builder returns a Hashtable of PricingResults, keyed off discount card
	/// </summary>
	public class PricingResultsBuilder
	{
		/// <summary>
		/// Implementation of IComparer to order coach tickets
		/// </summary>
		private class CoachTicketOrderingComparer  : IComparer
		{
			/// <summary>
			/// Implementation of IComparer.Compare. Two tickets are compared based on there value of their fares and then by ticket name
			/// </summary>
			/// <param name="x"></param>
			/// <param name="y"></param>
			/// <returns></returns>
			public int Compare(Object x, Object y)
			{
				int comparison = 0; // the value we will return to indicate the comparison
				// Make sure we can cast both objects as Tickets
				Ticket ticketX = x as Ticket;
				Ticket ticketY = y as Ticket;
				// If either of the objects isn't a Ticket then throw an ArgumentException
				if (ticketX == null || ticketY == null) 
				{
					throw new ArgumentException("Unable to compare objects "+x+" and "+y+"as Tickets");
				}
				float fareX = GetFare(ticketX);
				float fareY = GetFare(ticketY);

				// Compare the fares
				comparison = (fareX < fareY) ? -1 : ((fareX > fareY) ? 1 : 0);
				// if they are still the same then compare the ticket names
				if (comparison == 0)
				{
					comparison = ticketX.Code.CompareTo(ticketY.Code);
				}
				return comparison;
			}

			/// <summary>
			/// Find the appropriate fare field
			/// </summary>
			/// <param name="ticket"></param>
			/// <returns></returns>
			private float GetFare(Ticket ticket)
			{
				if (!ticket.AdultFare.Equals(float.NaN))
				{
					return ticket.AdultFare;
				} 
				else if (!ticket.ChildFare.Equals(float.NaN))
				{
					return ticket.ChildFare;
				}
				else if (!ticket.DiscountedAdultFare.Equals(float.NaN))
				{
					return ticket.DiscountedAdultFare;
				}
				else 
				{
					return ticket.DiscountedChildFare;
				}
			}
		}
		PricingResultsHelper helper = new PricingResultsHelper();

		// Formatted string containing the min and max ages of child fares
		private string childAgeRange = string.Empty;
		// The extraced values, with defaults
		private int minChildAge;
		private int maxChildAge;

		TicketNameRule nameRule = null;

		// The list of undiscounted fares. This is used to generate a template PricingResult
		private IList undiscountedFares = new ArrayList();
		// Track whether the user should be adding discounted or undiscounted fares
		private bool undiscountedFaresComplete = false;
		// The PricingResults object for undiscounted Fares. This is used as a template for all other PricingResults
		private PricingResult templatePricingResult = null;
		// Our set of PricingResults organised by discount card
		private Hashtable pricingResults = new Hashtable();

		/// <summary>
		/// Create a PricingResultsBuilder with the appropriate nameRule (NatEx or SCL)
		/// </summary>
		/// <param name="nameRule"></param>
		public PricingResultsBuilder(TicketNameRule nameRule)
		{
			this.nameRule = nameRule;
			this.minChildAge = helper.DEFAULT_MIN;
			this.maxChildAge = helper.DEFAULT_MAX;
		}

		/// <summary>
		/// Create a new undiscounted fare
		/// </summary>
		/// <param name="fare"></param>
		public void AddUndiscountedFare(Fare fare)
		{
			if (undiscountedFaresComplete)
			{
				throw new InvalidOperationException("Unable to add an undiscounted fare after adding a discount card");
			}
			Ticket ticket = CreateTicket(fare);
			undiscountedFares.Add(ticket);
			// Capture any age range information if we haven't already
			if (!fare.childAgeRange.Equals(string.Empty) && childAgeRange.Equals(string.Empty))
			{
				childAgeRange = fare.childAgeRange;
			}
		}

		/// <summary>
		/// Add a new discount card to the set of supported discount cards
		/// </summary>
		/// <param name="card"></param>
		public void AddDiscountCard(string card)
		{
			if (!pricingResults.ContainsKey(card))
			{
				if (templatePricingResult == null)
				{
					CreateTemplatePricingResult();
				}
				pricingResults.Add(card, templatePricingResult.Clone());
			}
			
		}

		/// <summary>
		///  Create a new discounted fare associated with the matching discount card
		/// </summary>
		/// <param name="fare"></param>
		public void AddDiscountedFare(Fare fare)
		{
			if (!undiscountedFaresComplete)
			{
				throw new InvalidOperationException("Unable to add a discounted fare until discount cards have been added");
			}
			// See if we have a PricingResult with a matching discount card and if so add a new Ticket to it
			if (pricingResults.ContainsKey(fare.discountCardType))
			{
				((PricingResult)pricingResults[fare.discountCardType]).Tickets.Add(CreateTicket(fare));
			}
		}

		/// <summary>
		/// Return the set of generated PricingResults with the tickets of each PricingResult appropriate ordered
		/// </summary>
		/// <returns></returns>
		public Hashtable GetPricingResults()
		{
			// If we haven't got any results yet, then it is because no discount card has been added so the PricingResult
			// for undiscounted fares hasb't been created. So we creat it
			if (templatePricingResult == null)
			{
				CreateTemplatePricingResult();
			}
			// Sort the sets of tickets in each of the PricingResults
			IComparer comparer = new CoachTicketOrderingComparer();
			foreach (PricingResult pricingResult in pricingResults.Values)
			{
				pricingResult.Sort(comparer);
			}
			return pricingResults;
		}

		/// <summary>
		/// Once a discount card is added, assume that we now have all the undiscounted fares and create a PricingResult
		/// of undiscounted fares. This then forms a template for all the other PricingResults.
		/// </summary>
		private void CreateTemplatePricingResult()
		{
			// Once we create the template we don't want to accept any more undisconted fares
			undiscountedFaresComplete = true;
			if (!childAgeRange.Equals(string.Empty))
			{
				int[] ages = helper.GenerateChildAges(childAgeRange);
				minChildAge = ages[0];
				maxChildAge = ages[1];									
			}
			templatePricingResult = new PricingResult(minChildAge, maxChildAge, false, false);
			templatePricingResult.Tickets = undiscountedFares;
			// Add the template to the set of PricingResults
			pricingResults.Add(helper.NO_DISCOUNT, templatePricingResult);

		}

		/// <summary>
		/// Creates a the outline of a Ticket based on an Atkins Fare object
		/// </summary>
		/// <param name="fare"></param>
		/// <returns></returns>
		private Ticket CreateTicket(Fare fare)
		{
			String name = nameRule.GetName(fare);
			Flexibility flexibility = helper.ConvertFlexibility(fare);
			Ticket ticket = new Ticket(name, flexibility);
			bool discounted = ((fare.discountCardType != null) && (fare.discountCardType != helper.NO_DISCOUNT));
			helper.SetFare(ticket, fare.adult, discounted, fare.fare);
			return ticket;
		}
	}
}
