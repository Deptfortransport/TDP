//********************************************************************************
//NAME         : TicketUniqueKey.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 24/11/2003
//DESCRIPTION  : Implementation of TicketUniqueKey class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RBOGateway/TicketUniqueKey.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:12   mturner
//Initial revision.
//
//   Rev 1.0   Nov 24 2003 16:30:08   acaunt
//Initial Revision
using System;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Defines a compound unique key for the ticket information returned by the RBOs.
	/// This allows us to group discounted and undiscounted fares for a particular ticket
	/// </summary>
	public class TicketUniqueKey
	{
		private string ticketCode;
		private string routeCode;

		public TicketUniqueKey(string ticketCode, string routeCode)
		{
			this.ticketCode = ticketCode;
			this.routeCode = routeCode;
		}

		public string TicketCode
		{
			get { return ticketCode;}
		}

		public string RouteCode
		{
			get {return routeCode;}
		}

		// Provides a hashcode for the class so that it can be used as a key in a hashtable
		public override int GetHashCode()
		{
			return ticketCode.GetHashCode() ^ routeCode.GetHashCode();
		}

		// Override Equals as well for consistency
		public override bool Equals(Object o)
		{
			// Attempt to cast the object as a TicketUniqueKey
			TicketUniqueKey that = o as TicketUniqueKey;
			if (that == null) 
			{
				return false;
			}
			return (this.ticketCode.Equals(that.ticketCode) && this.routeCode.Equals(that.routeCode));
		}
	}
}
