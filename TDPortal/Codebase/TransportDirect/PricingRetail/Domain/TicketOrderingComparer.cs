//********************************************************************************
//NAME         : TicketOrderingComparer.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 28/10/2003
//DESCRIPTION  : Implementation of TicketOrderingComparer class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/TicketOrderingComparer.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:58   mturner
//Initial revision.
//
//   Rev 1.3   Dec 15 2005 20:42:52   RPhilpott
//Handle case where adult undiscounted fare is NaN.
//Resolution for 3373: Incorrect display of availablility with railcards
//
//   Rev 1.2   May 09 2005 12:29:02   RPhilpott
//Include route code in sort criteria if ticket has rail data.
//Resolution for 2457: PT - Find a Fare - Train fares displayed twice on Select a Fare page
//
//   Rev 1.1   Nov 24 2003 16:26:38   acaunt
//Bug fix when comparing child fares
//
//   Rev 1.0   Oct 28 2003 12:07:34   acaunt
//Initial Revision
using System;
using System.Collections;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Implementation of IComparer for ordering Tickets
	/// </summary>
	public class TicketOrderingComparer : IComparer
	{
		/// <summary>
		/// Do nothing constructor
		/// </summary>
		public TicketOrderingComparer()
		{
		}

		/// <summary>
		/// Implementation of IComparer.Compare
		/// The two tickets are compared by (in order) adult fare value, child fare value, ticket name
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

			// Compare the Tickets first by adult fare

			// treat NaN as the highest possible fare ...
			if	(float.IsNaN(ticketX.AdultFare) && !float.IsNaN(ticketY.AdultFare))
			{
				comparison = 1;
			}
			else if (!float.IsNaN(ticketX.AdultFare) && float.IsNaN(ticketY.AdultFare))
			{
				comparison = -1;	
			}

			if	(comparison == 0)
			{
				comparison = (ticketX.AdultFare < ticketY.AdultFare) ? -1 : ((ticketX.AdultFare > ticketY.AdultFare) ? 1 : 0);
			}

			// if they are the same (comparison == 0) then compare the child fares
			if  (comparison == 0) 
			{
				comparison = (ticketX.ChildFare < ticketY.ChildFare) ? -1 : ((ticketX.ChildFare > ticketY.ChildFare) ? 1 : 0);
			}

			// if they are still the same then compare the ticket names
			if  (comparison == 0)
			{
				comparison = ticketX.Code.CompareTo(ticketY.Code);
			}

			// if they are still the same, and both include rail fare data, then compare the route codes
			if  (comparison == 0 && ticketX.TicketRailFareData != null && ticketY.TicketRailFareData != null)
			{
				comparison = ticketX.TicketRailFareData.RouteCode.CompareTo(ticketY.TicketRailFareData.RouteCode);	
			}

			return comparison;
		}
	}
}
