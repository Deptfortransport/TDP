// *********************************************** 
// NAME                 : RetailUnit.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 16/02/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/RetailUnit.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:52   mturner
//Initial revision.
//
//   Rev 1.3   May 16 2005 15:45:34   jgeorge
//Added RandomiseRetailerLists method
//Resolution for 2519: Order of retailers list should be random
//
//   Rev 1.2   Mar 18 2005 14:02:26   jgeorge
//Removed commented out methods
//
//   Rev 1.1   Feb 22 2005 17:51:34   jgeorge
//Added commenting
//
//   Rev 1.0   Feb 22 2005 10:52:54   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Holds one or more tickets along with the retailers that can sell that specific combination of tickets
	/// </summary>
	[CLSCompliant(false), Serializable]
	public class RetailUnit
	{
		#region Members

		private ArrayList tickets;

		private ArrayList onlineRetailers;
		private ArrayList offlineRetailers;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor. Only a single retailer can be added initially, but further 
		/// retailers can be added with the AddRetailer method.
		/// <seealso cref="AddRetailer"/>
		/// </summary>
		/// <param name="tickets">The ticket combination that can be sold by the retailers in this RetailUnit</param>
		/// <param name="retailer">The initial retailer for the unit. Further retailers can be added using the AddRetailer method</param>
		public RetailUnit( SelectedTicket[] tickets, Retailer retailer )
		{
			this.tickets = new ArrayList( tickets );

			onlineRetailers = new ArrayList( );
			offlineRetailers = new ArrayList( );

			if ( retailer.isHandoffSupported )
				onlineRetailers.Add( retailer );
			else
				offlineRetailers.Add( retailer );
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the combination of tickets that can be sold by the retailers in this RetailerUnit
		/// </summary>
		public SelectedTicket[] Tickets
		{
			get { return (SelectedTicket[])tickets.ToArray(typeof(SelectedTicket)); }
		}

		/// <summary>
		/// Returns the retailers that support handoff.
		/// </summary>
		public Retailer[] OnlineRetailers
		{
			get { return (Retailer[])onlineRetailers.ToArray(typeof(Retailer)); }
		}

		/// <summary>
		/// Returns the retailers that do not support handoff
		/// </summary>
		public Retailer[] OfflineRetailers
		{
			get { return (Retailer[])offlineRetailers.ToArray(typeof(Retailer)); }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a retailer to the retail unit
		/// </summary>
		/// <param name="r"></param>
		public void AddRetailer(Retailer r)
		{
			if (r.isHandoffSupported && !onlineRetailers.Contains(r))
				onlineRetailers.Add(r);
			else if (!r.isHandoffSupported && !offlineRetailers.Contains(r))
				offlineRetailers.Add(r);
		}

		/// <summary>
		/// Used to check whether the RetailUnit contains a specific ticket
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public bool ContainsTicket(SelectedTicket t)
		{
			return tickets.Contains(t);
		}

		/// <summary>
		/// Used to check whether the tickets contained in this RetailUnit exactly matches
		/// a specified array of SelectedTicket
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public bool MatchesTickets(SelectedTicket[] t)
		{
			if (tickets.Count == t.Length)
			{
				foreach (SelectedTicket current in t)
				{
					if (!tickets.Contains(current))
						return false;
				}
				return true;
			}
			else
				return false;
		}

		/// <summary>
		/// Used to check whether the retail unit contains a specific Retailer
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		public bool ContainsRetailer(Retailer r)
		{
			return ( offlineRetailers.Contains(r) || onlineRetailers.Contains(r) );
		}

		/// <summary>
		/// Uses the RetailerSortHelper class to randomly sort the retailer lists
		/// </summary>
		public void RandomiseRetailerLists()
		{
			// Now sort the lists of retailers
			onlineRetailers = new ArrayList(RetailerSortHelper.SortList( onlineRetailers ));
			offlineRetailers = new ArrayList(RetailerSortHelper.SortList( offlineRetailers ));
		}

		#endregion

	}
}
