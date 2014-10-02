// *********************************************** 
// NAME                 : TicketRetailerInfo.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 21/12/2004
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/TicketRetailerInfo.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:52   mturner
//Initial revision.
//
//   Rev 1.7   Nov 15 2005 08:56:14   jgeorge
//Changed CJP Request id to int
//Resolution for 2995: DN040: Incorrect retailer and ticket information after using Back button
//Resolution for 2996: DN040: spurious Wait Page when selecting "Amend" in SBT
//Resolution for 3030: DN040: Switching between options on results page invokes wait page
//
//   Rev 1.6   Nov 09 2005 12:31:52   build
//Automatically merged from branch for stream2818
//
//   Rev 1.5.1.2   Oct 29 2005 16:17:00   RPhilpott
//Updated requestId.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5.1.1   Oct 25 2005 16:45:38   RPhilpott
//Work in progress
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5.1.0   Oct 14 2005 15:15:56   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.5   May 16 2005 15:46:02   jgeorge
//Added call to RetailUnit.RandomiseRetailerLists method
//Resolution for 2519: Order of retailers list should be random
//
//   Rev 1.4   Mar 30 2005 15:09:14   jgeorge
//Change to retailer handoff and integration with cost based searching
//
//   Rev 1.3   Mar 21 2005 17:10:10   jgeorge
//FxCop changes
//
//   Rev 1.2   Feb 22 2005 10:56:30   jgeorge
//Interim check-in
//
//   Rev 1.1   Jan 18 2005 13:55:16   jgeorge
//Interim check-in
//
//   Rev 1.0   Dec 23 2004 11:55:12   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Holds data used to build the matrix on the Ticket Retailers page
	/// </summary>
	[Serializable, CLSCompliant(false)]
	public class TicketRetailerInfo
	{
		#region Private fields

		private readonly bool isForReturn;
		private readonly TDDateTime journeyDate;
		private ItineraryType itineraryType;
		private SelectedTicket[] selectedTickets;

		private ArrayList retailUnits;
		private ArrayList onlineRetailers;
		private ArrayList offlineRetailers;
		private int cjpRequestId;

		#endregion

		#region Constructor

		/// <summary>
		/// Builds the object.
		/// </summary>
		/// <param name="journeyDate">The date that should be shown as the journey date</param>
		/// <param name="itineraryType">Whether the tickets in question are Single or Return</param>
		/// <param name="isForReturn">Whether this is for the outward or inward section of the journey. This should always be false when the ItineraryType is Return</param>
		/// <param name="tickets"></param>
		public TicketRetailerInfo(TDDateTime journeyDate, ItineraryType itineraryType, bool isForReturn, SelectedTicket[] selectedTickets, int cjpRequestId)
		{
			this.isForReturn = isForReturn;
			this.journeyDate = journeyDate;
			this.itineraryType = itineraryType;
			this.selectedTickets = (SelectedTicket[])selectedTickets.Clone();
			this.cjpRequestId = cjpRequestId;
			populateRetailUnits();
		}

		#endregion

		#region Public properties
		
		/// <summary>
		/// Date of the journey
		/// </summary>
		public TDDateTime JourneyDate
		{
			get { return journeyDate; }
		}

		/// <summary>
		/// Whether the tickets are single or return
		/// </summary>
		public ItineraryType ItineraryType
		{
			get { return itineraryType; }
		}

		/// <summary>
		/// Whether this object contains tickets for the outward or inward portion of the journey
		/// </summary>
		public bool IsForReturn
		{
			get { return isForReturn; }
		}

		/// <summary>
		/// The list of tickets that have been selected for the journey
		/// </summary>
		public SelectedTicket[] SelectedTickets
		{
			get { return (SelectedTicket[])selectedTickets.Clone(); }
		}

		public int CjpRequestId
		{
			get { return cjpRequestId; }
		}

		/// <summary>
		/// The retail units
		/// </summary>
		public RetailUnit[] RetailUnits
		{
			get { return (RetailUnit[])retailUnits.ToArray(typeof(RetailUnit)); }
		}

		#endregion

		#region Public methods

		#endregion

		#region Private methods

		/// <summary>
		/// Populates the list of RetailUnits, based on the supplied selectedTickets array
		/// </summary>
		private void populateRetailUnits()
		{
			compileRetailerLists();
			retailUnits = new ArrayList();
			AddUnitsForRetailers(onlineRetailers);
			AddUnitsForRetailers(offlineRetailers);
			foreach (RetailUnit r in retailUnits)
				r.RandomiseRetailerLists();
		}

		/// <summary>
		/// Adds the retail units for a set of retailers
		/// </summary>
		/// <param name="retailers"></param>
		private void AddUnitsForRetailers(ArrayList retailers)
		{
			foreach (Retailer r in retailers)
			{
				// Find the tickets that the retailer can sell
				SelectedTicket[] retailerTickets = RetailerTickets(r);

				if (r.isHandoffSupported && r.AllowsMultipleTicketHandoff)
				{
					// Online retailer supporting multiple ticket handoff
					// Single retail unit for all these tickets
					AddRetailUnit( retailerTickets, r );
				}
				else
				{
					foreach (SelectedTicket t in retailerTickets)
						AddRetailUnit( t, r );
				}
			}
		}

		/// <summary>
		/// Updates the retailer units collection for a combination of tickets and a retailer
		/// </summary>
		/// <param name="tickets"></param>
		/// <param name="r"></param>
		private void AddRetailUnit(SelectedTicket[] tickets, Retailer r)
		{
			RetailUnit unit = FindRetailUnitForTickets(tickets);
			if (unit == null)
				retailUnits.Add( new RetailUnit( tickets, r ) );
			else
				unit.AddRetailer( r );
		}

		/// <summary>
		/// Updates the retailer units collection for a ticket and a retailer
		/// </summary>
		/// <param name="ticket"></param>
		/// <param name="r"></param>
		private void AddRetailUnit(SelectedTicket ticket, Retailer r)
		{
			AddRetailUnit( new SelectedTicket[] { ticket }, r );
		}

		/// <summary>
		/// Searches for an existing RetailUnit matching the supplied selection
		/// of tickets
		/// </summary>
		/// <param name="tickets"></param>
		/// <returns></returns>
		private RetailUnit FindRetailUnitForTickets(SelectedTicket[] tickets)
		{
			foreach (RetailUnit u in retailUnits)
				if (u.MatchesTickets(tickets))
					return u;
			return null;
		}

		/// <summary>
		/// Retrieves all of the tickets that the given retailer can sell
		/// </summary>
		/// <param name="r"></param>
		/// <returns></returns>
		private SelectedTicket[] RetailerTickets(Retailer r)
		{
			ArrayList results = new ArrayList(selectedTickets.Length);
			foreach (SelectedTicket t in selectedTickets)
				if (t.Retailers.Contains(r))
					results.Add(t);
			return (SelectedTicket[])results.ToArray(typeof(SelectedTicket));
		}

		/// <summary>
		/// Populates the onlineRetailers and offlineRetailers collections
		/// </summary>
		private void compileRetailerLists()
		{
			onlineRetailers = new ArrayList();
			offlineRetailers = new ArrayList();

			foreach (SelectedTicket ticket in selectedTickets)
			{
				foreach (Retailer r in ticket.Retailers)
				{
					if ( r.isHandoffSupported && !onlineRetailers.Contains(r) )
						onlineRetailers.Add(r);
					else if ( !r.isHandoffSupported && !offlineRetailers.Contains(r) )
						offlineRetailers.Add(r);
				}
			}
		}

		#endregion
	}
}
