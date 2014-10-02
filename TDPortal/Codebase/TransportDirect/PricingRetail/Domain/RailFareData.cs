// ******************************************************** 
// NAME			: RailFareData.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 21/02/2005
// DESCRIPTION	: Implementation of the RailFareData class
// ******************************************************* 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/RailFareData.cs-arc  $
//
//   Rev 1.2   Jun 03 2010 09:08:46   mmodi
//Additional parameters to allow validate of services and fares using the RBO MR call
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.1   Feb 16 2009 11:48:58   rbroddle
//Added origin and dest NLCActual properties 
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.0   Nov 08 2007 12:36:54   mturner
//Initial revision.
//
//   Rev 1.7   Apr 26 2006 12:13:58   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.6.1.0   Apr 05 2006 17:10:02   RPhilpott
//Add fare location names.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.6   Jan 18 2006 18:16:34   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.5   Nov 24 2005 18:23:02   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.4   Nov 23 2005 15:49:40   RPhilpott
//Add retailId for logging, remove redundant quotaControlled.
//Resolution for 3038: DN040: Double reporting/logging of NRS requests
//
//   Rev 1.3   Mar 22 2005 16:08:56   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.2   Mar 02 2005 18:10:36   RPhilpott
//Change RestrictionCodes to RestrictionCode
//
//   Rev 1.1   Mar 01 2005 18:43:14   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//
//   Rev 1.0   Feb 21 2005 11:43:14   jmorrissey
//Initial revision.

using System;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for RailFareData.
	/// </summary>
	[Serializable]
	public class RailFareData
	{
		private string shortTicketCode;
		private string routeCode;
		private string restrictionCode;
		private string railcardCode;

		private TDLocation origin;
		private TDLocation destination;

		private LocationDto[] origins;
		private LocationDto[] destinations;

		private string originNlc;
		private string destinationNlc;

        private string originNlcActual;       
        private string destinationNlcActual; 
		
		private string originName;
		private string destinationName;

        private string rawFareString;

		private bool isReturn;

		public RailFareData(string shortTicketCode, string routeCode, string restrictionCode, 
			string originNlc, string destinationNlc, string railcardCode, bool isReturn,
            LocationDto[] origins, LocationDto[] destinations, string rawFareString)
		{
			this.shortTicketCode	= shortTicketCode;
			this.routeCode			= routeCode;
			this.restrictionCode	= restrictionCode;
			this.railcardCode		= railcardCode;
			this.isReturn			= isReturn;
			this.originNlc			= originNlc;
			this.destinationNlc		= destinationNlc;
			this.origins			= origins;
			this.destinations		= destinations;
            this.rawFareString      = rawFareString;
		}

		public RailFareData(TicketDto ticket)
		{
			this.shortTicketCode	= ticket.TicketCode;
			this.routeCode			= ticket.RouteCode;
			this.restrictionCode	= ticket.RestrictionCode;
			this.railcardCode		= ticket.Railcard;
			this.isReturn			= (ticket.JourneyType == JourneyType.Return);
			this.originNlc			= ticket.OriginNlc;
			this.destinationNlc		= ticket.DestinationNlc;
            this.originNlcActual = ticket.FareOriginNlcActual;
            this.destinationNlcActual = ticket.FareDestinationNlcActual;
			this.origins			= ticket.Origins;
			this.destinations		= ticket.Destinations;
			this.origin				= ticket.OriginLocation;
			this.destination		= ticket.DestinationLocation;
			this.originName			= ticket.OriginName;
			this.destinationName	= ticket.DestinationName;
            this.rawFareString      = ticket.RawFareString;
		}

		/// <summary>
		/// read/write property for ShortTicketCode
		/// </summary>
		public string ShortTicketCode
		{
			get
			{
				return shortTicketCode;
			}
			set
			{
				shortTicketCode = value;
			}
		}

		/// <summary>
		/// read/write property for RouteCode
		/// </summary>
		public string RouteCode
		{
			get
			{
				return routeCode;
			}
			set
			{
				routeCode = value;
			}
		}

		/// <summary>
		/// read/write property for RestrictionCode
		/// </summary>
		public string RestrictionCode
		{
			get
			{
				return restrictionCode;
			}
			set
			{
				restrictionCode = value;
			}
		}
		
		/// <summary>
		/// read/write property for RailcardCode
		/// </summary>
		public string RailcardCode
		{
			get
			{
				return railcardCode;
			}
			set
			{
				railcardCode = value;
			}
		}

		/// <summary>
		/// read/write property for IsReturn 
		/// </summary>
		public bool IsReturn
		{
			get
			{
				return isReturn;
			}
			set
			{
				isReturn = value;
			}
		}

		/// <summary>
		/// read/write property for DestinationNlc 
		/// </summary>
		public string DestinationNlc
		{
			get
			{
				return destinationNlc;
			}
			set
			{
				destinationNlc = value;
			}
		}

		/// <summary>
		/// read/write property for OriginNlc
		/// </summary>
		public string OriginNlc
		{
			get
			{
				return originNlc;
			}
			set
			{
				originNlc = value;
			}
		}

        /// <summary>
        /// NLC code of fare origin - may be code for an individual station or a group/zone (r/o).
        /// This is set only by a ZPBO call. 
        /// This value represents the actual origin NLC this fare is valid for
        /// and can be different to the FareOriginNLC but is still valid for this journey.
        /// </summary>
        public string OriginNlcActual
        {
            get { return originNlcActual; }
        }

        /// <summary>
        /// NLC code of fare destination - may be code for an individual station or a group/zone (r/o).
        /// This is set only by a ZPBO call. 
        /// This value represents the actual destination NLC this fare is valid for
        /// and can be different to the FareDestinationNLC but is still valid for this journey.
        /// </summary>
        public string DestinationNlcActual
        {
            get { return destinationNlcActual; }
        }

		/// <summary>
		/// read/write property for DestinationName
		/// </summary>
		public string DestinationName
		{
			get
			{
				return destinationName;
			}
			set
			{
				destinationName = value;
			}
		}

		/// <summary>
		/// read/write property for OriginName
		/// </summary>
		public string OriginName
		{
			get
			{
				return originName;
			}
			set
			{
				originName = value;
			}
		}


		/// <summary>
		/// read/write property for Destination 
		/// </summary>
		public TDLocation Destination
		{
			get
			{
				return destination;
			}
			set
			{
				destination = value;
			}
		}

		/// <summary>
		/// read/write property for Origin
		/// </summary>
		public TDLocation Origin
		{
			get
			{
				return origin;
			}
			set
			{
				origin = value;
			}
		}


		/// <summary>
		/// read/write property for Destinations 
		/// </summary>
		public LocationDto[] Destinations
		{
			get
			{
				return destinations;
			}
			set
			{
				destinations = value;
			}
		}

		/// <summary>
		/// read/write property for Origins
		/// </summary>
		public LocationDto[] Origins
		{
			get
			{
				return origins;
			}
			set
			{
				origins = value;
			}
		}

        /// <summary>
        /// Read/write. The raw fare string used to create the original Fare (RBO) object this RailFateData
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
	}
}
