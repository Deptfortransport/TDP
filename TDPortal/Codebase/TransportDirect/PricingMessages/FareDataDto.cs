// ******************************************************** 
// NAME			: FareDataDto.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-03-16
// DESCRIPTION	: Data transfer object to pass rail fare data 
//					between the Domain and RBO components (needed
//					to avoid a dependency from RBO to Domain).
// ******************************************************* 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/FareDataDto.cs-arc  $
//
//   Rev 1.1   Jun 03 2010 08:52:40   mmodi
//Added additional parameters to allow validation of services and fares using the RBO MR call
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.0   Nov 08 2007 12:35:50   mturner
//Initial revision.
//
//   Rev 1.5   Dec 05 2005 18:26:42   RPhilpott
//Changes to ensure that RE GD call is made if connecting TOC's need to be checked post-timetable call.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.4   Nov 24 2005 18:23:04   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.3   Nov 23 2005 15:49:38   RPhilpott
//Add retailId for logging, remove redundant quotaControlled.
//Resolution for 3038: DN040: Double reporting/logging of NRS requests
//
//   Rev 1.2   Apr 06 2005 17:09:44   jmorrissey
//Added [Serializable] attribute
//
//   Rev 1.1   Apr 05 2005 11:19:16   RPhilpott
//Don't return null strings.
//
//   Rev 1.0   Mar 22 2005 16:23:40   RPhilpott
//Initial revision.
//

using System;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data transfer object to pass rail fare data 
	///	 between the domain and RBO components.
	/// </summary>
	[Serializable]
	public class FareDataDto
	{
		private string shortTicketCode;
		private string routeCode;
		private string restrictionCode;
		private string railcardCode;
		private string restrictionCodesToReapply;
		private string originNlc;
		private string destinationNlc;
		private bool connectingTocCheckRequired;
        private bool crossLondonToCheck;
        private bool zonalIndicatorToCheck;
        private bool visitCRSToCheck;
        private string outputGL;

		private LocationDto[] origins;
		private LocationDto[] destinations;

        private string rawFareString;

		private bool isReturn;

		public FareDataDto(         string shortTicketCode, string routeCode, string restrictionCode, 
									string originNlc, string destinationNlc, string railcardCode, 
									bool isReturn, string restrictionCodesToReapply, 
									LocationDto[] origins, LocationDto[] destinations,
									bool connectingTocCheckRequired, string rawFareString,
                                    bool crossLondonToCheck, bool zonalIndicatorToCheck,
                                    bool visitCRSToCheck, string outputGL)
		{
            this.shortTicketCode = shortTicketCode;
			this.routeCode = routeCode;
			this.restrictionCode = restrictionCode;
			this.railcardCode = railcardCode;
			this.isReturn = isReturn;
			this.originNlc = originNlc;
			this.destinationNlc = destinationNlc;
			this.restrictionCodesToReapply = restrictionCodesToReapply;
			this.origins = origins;
			this.destinations = destinations;
			this.connectingTocCheckRequired = connectingTocCheckRequired;
            this.rawFareString = rawFareString;
            this.crossLondonToCheck = crossLondonToCheck;
            this.zonalIndicatorToCheck = zonalIndicatorToCheck;
            this.visitCRSToCheck = visitCRSToCheck;
            this.outputGL = outputGL;
		}

		/// <summary>
		/// read/write property for ShortTicketCode
		/// </summary>
		public string ShortTicketCode
		{
			get
			{
				return (shortTicketCode != null ? shortTicketCode : string.Empty);
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
				return (routeCode != null ? routeCode : string.Empty); 
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
				return (restrictionCode != null ? restrictionCode : string.Empty);
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
				return (railcardCode != null ? railcardCode : string.Empty);
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
				return (destinationNlc != null ? destinationNlc : string.Empty);
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
				return (originNlc != null ? originNlc : string.Empty);
			}
			set
			{
				originNlc = value;
			}
		}

		/// <summary>
		/// read/write property for Destinations
		/// </summary>
		public LocationDto[] Destinations
		{
			get
			{
				return (destinations != null ? destinations : new LocationDto[0]);
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
				return (origins != null ? origins : new LocationDto[0]);
			}
			set
			{
				origins = value;
			}
		}

		/// <summary>
		/// read/write property for RestrictionCodesToReapply
		/// </summary>
		public string RestrictionCodesToReapply
		{
			get
			{
				return (restrictionCodesToReapply != null ? restrictionCodesToReapply : string.Empty);
			}
			set
			{
				restrictionCodesToReapply = value;
			}
		}

		/// <summary>
		/// read/write property for ConnectingTocCheckRequired
		/// </summary>
		public bool ConnectingTocCheckRequired
		{
			get
			{
				return connectingTocCheckRequired;
			}
			set
			{
				connectingTocCheckRequired = value;
			}
		}

        /// <summary>
        /// Read/write. The raw fare string used to create the original Fare (RBO) object this FareDataDto
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
        /// read/write property for IsReturn 
        /// </summary>
        public bool CrossLondonToCheck
        {
            get
            {
                return crossLondonToCheck;
            }
            set
            {
                crossLondonToCheck = value;
            }
        }

        /// <summary>
        /// read/write property for IsReturn 
        /// </summary>
        public bool ZonalIndicatorToCheck
        {
            get
            {
                return zonalIndicatorToCheck;
            }
            set
            {
                zonalIndicatorToCheck = value;
            }
        }

        /// <summary>
        /// read/write property for IsReturn 
        /// </summary>
        public bool VisitCRSToCheck
        {
            get
            {
                return visitCRSToCheck;
            }
            set
            {
                visitCRSToCheck = value;
            }
        }

        /// <summary>
        /// Read/write. The raw fare string used to create the original Fare (RBO) object this FareDataDto
        /// was based on. 
        /// This is provided to allow rebuilding the Fare (RBO) object if further subsequent fare validation
        /// is required following the return back to the TDP Domain
        /// </summary>
        public string OutputGL
        {
            get
            {
                return (outputGL != null ? outputGL : string.Empty);
            }
            set
            {
                outputGL = value;
            }
        }

	}
}
