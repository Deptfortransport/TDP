// *********************************************** 
// NAME                 : SelectedTicket.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 16/02/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/SelectedTicket.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:52   mturner
//Initial revision.
//
//   Rev 1.6   Nov 09 2005 12:31:50   build
//Automatically merged from branch for stream2818
//
//   Rev 1.5.2.0   Oct 29 2005 11:08:10   RPhilpott
//Get rid of compiler warnings.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Apr 28 2005 14:17:54   jgeorge
//Removed unneeded functionality
//Resolution for 2309: PT - Train - Destination wrong on rail ticket description.
//
//   Rev 1.4   Mar 31 2005 16:26:54   jgeorge
//Bug fix
//
//   Rev 1.3   Mar 30 2005 15:09:10   jgeorge
//Change to retailer handoff and integration with cost based searching
//
//   Rev 1.2   Mar 18 2005 11:26:44   jgeorge
//Updated commenting
//
//   Rev 1.1   Feb 22 2005 17:51:00   jgeorge
//Add Origin and Destination helper properties
//
//   Rev 1.0   Feb 22 2005 10:53:04   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Holds details of a selected ticket for one or more journey legs, and the retailers that sell that ticket.
	/// </summary>
	[CLSCompliant(false), Serializable]
	public class SelectedTicket
	{
		#region Private members

		/// <summary>
		/// Legs for the outward journey
		/// </summary>
		private PublicJourneyDetail[] outwardLegs;

		/// <summary>
		/// Legs for the inward journey, or null if no inward journey
		/// </summary>
		private PublicJourneyDetail[] inwardLegs;

		/// <summary>
		/// The underlying Ticket object for this SelectedTicket
		/// </summary>
		private Ticket ticket;

		/// <summary>
		/// Will contain objects of type Retailer
		/// </summary>
		private IList retailers;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor used to create the object from a pricing unit
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <param name="selectedTicket"></param>
		public SelectedTicket( PricingUnit pricingUnit, Ticket selectedTicket )
		{
			this.outwardLegs = (PublicJourneyDetail[])((ArrayList)pricingUnit.OutboundLegs).ToArray(typeof(PublicJourneyDetail));
			if (pricingUnit.InboundLegs != null)
				this.inwardLegs = (PublicJourneyDetail[])((ArrayList)pricingUnit.InboundLegs).ToArray(typeof(PublicJourneyDetail));

			this.ticket = selectedTicket;
			
			if ( (pricingUnit.Retailers == null) || (pricingUnit.Retailers.Count == 0) )
				pricingUnit.FindRetailers( (RetailerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.RetailerCatalogue] );

			this.retailers = pricingUnit.Retailers;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Read only. Returns an the outward legs that this ticket applies to
		/// </summary>
		public PublicJourneyDetail[] OutboundLegs
		{
			get { return outwardLegs; }
		}

		/// <summary>
		/// Read only. Returns an the inward legs that this ticket applies to. Will be null
		/// if the ticket type is not return
		/// </summary>
		public PublicJourneyDetail[] InboundLegs
		{
			get { return inwardLegs; }
		}

		/// <summary>
		/// Read only. The ticket that has been selected for the journey legs
		/// </summary>
		public Ticket Ticket
		{
			get { return ticket; }
		}

		/// <summary>
		/// Read only. Returns an IList containing objects of type Retailer
		/// </summary>
		public IList Retailers
		{
			get { return retailers; }
		}

		/// <summary>
		/// Read only. Returns the mode for the Ticket. This is the mode of the first leg
		/// of the journey details held.
		/// </summary>
		public TransportDirect.JourneyPlanning.CJPInterface.ModeType Mode
		{
			get { return outwardLegs[0].Mode; }
		}

		/// <summary>
		/// Read only. Returns the start location from the journey details
		/// </summary>
		public TDLocation OriginLocation
		{
			get { return outwardLegs[0].LegStart.Location; }
		}

		/// <summary>
		/// Read only. Returns the end location from the journey details
		/// </summary>
		public TDLocation DestinationLocation
		{
			get { return outwardLegs[ outwardLegs.Length - 1 ].LegEnd.Location; } 
		}


		#endregion

	}
}
