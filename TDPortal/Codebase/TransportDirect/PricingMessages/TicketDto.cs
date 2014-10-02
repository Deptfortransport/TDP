//********************************************************************************
//NAME         : TicketDto.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Data Transfer Object for fare tickets
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/TicketDto.cs-arc  $
//
//   Rev 1.2   Jun 03 2010 08:55:50   mmodi
//Added additional parameters to allow validation of services and fares using the RBO MR call
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.1   Feb 13 2009 17:16:46   rbroddle
//Added originNlcActual and destinationNlcActual properties and amended constructor to populate them
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.0   Nov 08 2007 12:35:56   mturner
//Initial revision.
//
//   Rev 1.12   Apr 26 2006 12:12:58   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.11.1.0   Apr 05 2006 17:09:36   RPhilpott
//Add fare location names.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.11   Nov 24 2005 18:23:02   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.10   Mar 22 2005 16:08:36   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.9   Mar 01 2005 18:43:00   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//
//   Rev 1.8   Nov 18 2003 16:24:24   CHosegood
//Added route code attribute
//
//   Rev 1.7   Oct 23 2003 10:14:22   CHosegood
//Changed TicketName to TicketCode
//
//   Rev 1.6   Oct 17 2003 10:18:08   CHosegood
//Added JourneyType
//
//   Rev 1.5   Oct 16 2003 15:18:56   CHosegood
//Changed DOCO
//
//   Rev 1.4   Oct 16 2003 13:27:42   CHosegood
//Now correctly sets ticket name
//
//   Rev 1.3   Oct 15 2003 20:03:00   CHosegood
//Changed all occurences of DateTime to TDDateTime
//
//   Rev 1.2   Oct 13 2003 13:20:16   CHosegood
//Removed empty constructor
//
//   Rev 1.1   Oct 09 2003 17:38:12   CHosegood
//No change.
//
//   Rev 1.0   Oct 08 2003 11:34:24   CHosegood
//Initial Revision

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.PricingMessages
{
    /// <summary>
    /// Data Transfer Object for fare tickets.
    /// </summary>
    [Serializable]
	[CLSCompliant(false)]
	public class TicketDto
    {
        private string ticketCode = string.Empty;
		
		private float adultFare;
		private float childFare;
		private float minAdultFare;
		private float minChildFare;

        private string routeCode;
        private TicketClass ticketClass = TicketClass.Standard;
        private string railcard = string.Empty;
        private bool quotaControlled = false;
        private JourneyType journeyType = JourneyType.OutwardSingle;
		private string restrictionCode = string.Empty;
		private bool fromCostSearch = false;
		private string originNlc = string.Empty;
		private string destinationNlc = string.Empty;
		private string originName = string.Empty;
		private string destinationName = string.Empty;

        private string originNlcActual = string.Empty;          // Used by ZPBO to identify actual origin of fare (could be a group station)
        private string destinationNlcActual = string.Empty;    // Used by ZPBO to identify actual destination of fare (could be a group station)


		private LocationDto[] origins		= new LocationDto[0];
		private LocationDto[] destinations	= new LocationDto[0];

		TDLocation origin		= null;
		TDLocation destination	= null;

		private ArrayList supplements = new ArrayList();

        private string rawFareString;

        /// <summary>
        /// Identifies the type of ticket
        /// </summary>
        public string TicketCode
        {
            get { return this.ticketCode; }
            set { this.ticketCode = value; }
        }

		/// <summary>
		/// Identifies the permitted route
		/// </summary>
		public string RouteCode
		{
			get { return this.routeCode; }
			set { this.routeCode = value; }
		}

		/// <summary>
		/// Identifies the applicable restriction code
		/// </summary>
		public string RestrictionCode
		{
			get { return this.restrictionCode; }
			set { this.restrictionCode = value; }
		}

		/// <summary>
		/// The fare for one adult.  This may be Nan if there is no fare
		/// </summary>
		public float AdultFare
		{
			get { return this.adultFare; }
			set { this.adultFare = value; }
		}

		/// <summary>
		/// The fare for one child.  This may be Nan if there is no fare
		/// </summary>
		public float ChildFare
		{
			get { return this.childFare; }
			set { this.childFare = value; }
		}

		/// <summary>
		/// The minimum adult fare.  This may be Nan if there is no minimum fare
		/// </summary>
		public float MinimumAdultFare
		{
			get { return this.minAdultFare; }
			set { this.minAdultFare = value; }
		}

		/// <summary>
		/// The minimum child fare.  This may be Nan if there is no minimum fare
		/// </summary>
		public float MinimumChildFare
		{
			get { return this.minChildFare; }
			set { this.minChildFare = value; }
		}

		/// <summary>
        /// Identifies the class of the ticket
        /// </summary>
        public TicketClass TicketClass
        {
            get { return this.ticketClass; }
            set { this.ticketClass = value; }
        }

        /// <summary>
        /// Identifies the railcard used for the ticket 
        /// </summary>
        public string Railcard
        {
            get { return this.railcard; }
            set { this.railcard = value; }
        }

        /// <summary>
        /// Identifies if the ticket may be quota controlled
        /// </summary>
        public bool QuotaControlled
        {
            get { return this.quotaControlled; }
            set { this.quotaControlled = value; }
        }

		/// <summary>
		/// Origin NLC for fare (may be a fare group)
		/// </summary>
		public string OriginNlc 
		{
			get { return originNlc; }
			set { originNlc = value; }
		}

		/// <summary>
		/// Destination NLC for fare (may be a fare group)
		/// </summary>
		public string DestinationNlc 
		{
			get { return destinationNlc; }
			set { destinationNlc = value; }
		}
		
		/// <summary>
		/// Name of fare origin location (may be a fare group) (r/w) 
		/// </summary>
		public string OriginName 
		{
			get { return originName; }
			set { originName = value; }
		}

		/// <summary>
		/// Name of fare destination location (may be a fare group) (r/w) 
		/// </summary>
		public string DestinationName 
		{
			get { return destinationName; }
			set { destinationName = value; }
		}
		
		/// <summary>
		/// Fare origin as TDLocation
		/// </summary>
		public TDLocation OriginLocation 
		{
			get { return origin; }
			set { origin = value; }
		}

		/// <summary>
		/// Fare destination as TDLocation
		/// </summary>
		public TDLocation DestinationLocation 
		{
			get { return destination; }
			set { destination = value; }
		}

        /// <summary>
        /// NLC code of fare origin - may be code for an individual station or a group/zone (r/o).
        /// This is set only by a ZPBO call. 
        /// This value represents the actual origin NLC this fare is valid for
        /// and can be different to the FareOriginNLC but is still valid for this journey.
        /// </summary>
        public string FareOriginNlcActual
        {
            get { return originNlcActual; }
        }

        /// <summary>
        /// NLC code of fare destination - may be code for an individual station or a group/zone (r/o).
        /// This is set only by a ZPBO call. 
        /// This value represents the actual destination NLC this fare is valid for
        /// and can be different to the FareDestinationNLC but is still valid for this journey.
        /// </summary>
        public string FareDestinationNlcActual
        {
            get { return destinationNlcActual; }
        }

		/// <summary>
		/// Fare origins (> 1 if a group)
		/// </summary>
		public LocationDto[] Origins 
		{
			get { return origins; }
			set { origins = value; }
		}

		/// <summary>
		/// Fare destinations (> 1 if a group)
		/// </summary>
		public LocationDto[] Destinations 
		{
			get { return destinations; }
			set { destinations = value; }
		}

		/// <summary>
		/// Type of journey of this ticket
		/// </summary>
		public JourneyType JourneyType 
		{
			get { return this.journeyType; }
			set { this.journeyType = value; }
		}

		/// <summary>
		/// True is this ticket was found as 
		///  a result of a cost-based search
		/// </summary>
		public bool IsFromCostSearch 
		{
			get { return this.fromCostSearch; }
			set { this.fromCostSearch = value; }
		}

		/// <summary>
		/// A collection of supplements applicable 
		/// to this journey and fare, if any.
		/// Will only be populated for time-based
		/// search results (otherwise returns an  
		/// empty array).
		/// </summary>
		public SupplementDto[] Supplements 
		{
			get { return (SupplementDto[])(this.supplements.ToArray(typeof(SupplementDto))); } 
		}

		public void AddSupplement(SupplementDto supplement)
		{
			supplements.Add(supplement);
		}

        /// <summary>
        /// Read/write. The raw fare string used to create the original Fare (RBO) object this TicketDto
        /// was based on. 
        /// This is provided to allow rebuilding the Fare (RBO) object if further subsequent fare validation
        /// is required following the return back to the TDP Domain
        /// </summary>
        public string RawFareString
        {
            get
            {
                return (rawFareString != null ? rawFareString : string.Empty);
            }
            set
            {
                rawFareString = value;
            }
        }

		/// <summary>
		/// Data Transfer Object for fare tickets
		/// </summary>
		/// <param name="ticketCode">The code of the ticket</param>
		/// <param name="adultFare">The adult fare</param>
		/// <param name="childFare">The child fare</param>
		/// <param name="ticketClass">The class of the ticket</param>
		/// <param name="railcard">The railcard that was applied to the ticket</param>
		/// <param name="quotaControlled">Whether this ticket is quota-limited</param>
		/// <param name="restrictionCodes">Restriction code applicable to this ticket</param>
		public TicketDto( string ticketCode, string routeCode, float adultFare, float childFare,
			float minAdultFare, float minChildFare, TicketClass ticketClass, string railcard, 
			bool quotaControlled, JourneyType journeyType, string restrictionCode, string rawFareString) 
		{
			this.TicketCode = ticketCode;
			this.RouteCode = routeCode;
			this.AdultFare = adultFare;
			this.ChildFare= childFare;
			this.MinimumAdultFare = minAdultFare;
			this.MinimumChildFare = minChildFare;
			this.TicketClass= ticketClass;
			this.Railcard = railcard;
			this.QuotaControlled= quotaControlled;
			this.JourneyType = journeyType;
			this.RestrictionCode = restrictionCode;
            this.rawFareString = rawFareString;
		}

		
		/// <summary>
        /// Data Transfer Object for fare tickets
        /// </summary>
        /// <param name="ticketCode">The code of the ticket</param>
        /// <param name="adultFare">The adult fare</param>
        /// <param name="childFare">The child fare</param>
        /// <param name="ticketClass">The class of the ticket</param>
		/// <param name="railcard">The railcard that was applied to the ticket</param>
		/// <param name="quotaControlled">Whether this ticket is quota-limited</param>
		/// <param name="restrictionCodes">Restriction code applicable to this ticket</param>
		public TicketDto( string ticketCode, string routeCode, float adultFare, float childFare, float minAdultFare,
            float minChildFare, TicketClass ticketClass, string railcard, bool quotaControlled, JourneyType journeyType,
			string restrictionCode, string originNlc, string destinationNlc, LocationDto[] origins, LocationDto[] destinations,
			string originName, string destinationName,string originNlcActual, string destinationNlcActual, string rawFareString) 
        {
            this.TicketCode = ticketCode;
            this.RouteCode = routeCode;
			this.AdultFare = adultFare;
			this.ChildFare = childFare;
			this.MinimumAdultFare = minAdultFare;
			this.MinimumChildFare = minChildFare;
			this.TicketClass= ticketClass;
            this.Railcard = railcard;
            this.QuotaControlled= quotaControlled;
            this.JourneyType = journeyType;
			this.RestrictionCode = restrictionCode;
			this.originNlc = originNlc;
			this.destinationNlc = destinationNlc;
			this.origins = origins;
			this.destinations = destinations;
			this.originName = originName;
			this.destinationName = destinationName;
			this.originNlcActual = originNlcActual;
			this.destinationNlcActual = destinationNlcActual;
            this.rawFareString = rawFareString;
		}
    }
}
