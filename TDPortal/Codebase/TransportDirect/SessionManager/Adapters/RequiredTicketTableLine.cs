// *********************************************** 
// NAME			: RequiredTicketTableLine.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 28/02/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/RequiredTicketTableLine.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:52   mturner
//Initial revision.
//
//   Rev 1.2   Mar 21 2005 17:09:44   jgeorge
//FxCop changes
//
//   Rev 1.1   Mar 18 2005 09:55:24   jgeorge
//Updated commenting
//
//   Rev 1.0   Mar 02 2005 16:32:10   jgeorge
//Initial revision.

using System;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Represents a line displayed on the ticket retailer handoff page
	/// </summary>
	[CLSCompliant(false)]
	public class RequiredTicketTableLine
	{
		private readonly string ticketName = string.Empty;
		private readonly int noPeople;
		private readonly bool discounted;
		private readonly float adultFare = float.NaN;
		private readonly float childFare = float.NaN;
		private readonly float total = float.NaN;

		#region Constructor

		/// <summary>
		/// Constructor. Object is immutable, so all values must be specified at creation
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="noPeople"></param>
		/// <param name="children"></param>
		/// <param name="discounted"></param>
		public RequiredTicketTableLine(Ticket ticket, int noPeople, bool children, bool discounted)
		{
			this.ticketName = ticket.Code;
			this.noPeople = noPeople;
			this.discounted = discounted;
			if (children)
			{
				if (discounted)
					childFare = ticket.DiscountedChildFare;
				else
					childFare = ticket.ChildFare;
				total = childFare * noPeople;
			}
			else
			{
				if (discounted)
					adultFare = ticket.DiscountedAdultFare;
				else
					adultFare = ticket.AdultFare;
				total = adultFare * noPeople;
			}
		}

		#endregion

		#region Public properties

		/// <summary>
		/// The name of the ticket
		/// </summary>
		public string TicketName
		{
			get { return ticketName; }
		}

		/// <summary>
		/// Read only property holding the number of people who will be travelling on this ticket
		/// </summary>
		public int NoPeople
		{
			get { return noPeople; }
		}

		/// <summary>
		/// Read only property indicating whether or not the ticket price is discounted
		/// </summary>
		public bool Discounted
		{
			get { return discounted; }
		}

		/// <summary>
		/// Read only value containing the cost of the adult ticket, or float.NaN if this
		/// line is for a child ticket.
		/// </summary>
		public float AdultFare
		{
			get { return adultFare; }
		}

		/// <summary>
		/// Read only value containing the cost of the child ticket, or float.NaN if this
		/// line is for an adult ticket.
		/// </summary>
		public float ChildFare
		{
			get { return childFare; }
		}

		/// <summary>
		/// Read only value containing total cost for the line (either AdultFare or ChildFare 
		/// multiplied by NoPeople
		/// </summary>
		public float Total
		{
			get { return total; }
		}

        #endregion
		
	}
}
