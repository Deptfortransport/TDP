// ************************************************************** 
// NAME			: CostSearchRequest.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Implementation of the CostSearchRequest class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/CostSearchRequest.cs-arc  $
//
//   Rev 1.1   Feb 02 2009 16:20:56   mmodi
//Include Routing Guide properties
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:19:18   mturner
//Initial revision.
//
//   Rev 1.7   May 06 2005 16:22:42   jmorrissey
//Actioned code review comments
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.6   May 06 2005 15:13:38   jmorrissey
//Actioned code review comments
//Resolution for 1930: DEV Code Review: Cost Search Facade
//
//   Rev 1.5   Mar 10 2005 17:11:56   jmorrissey
//Added 'using TransportDirect.UserPortal.JourneyControl' so that it can use the CJPSessionInfo which has moved to this project
//
//   Rev 1.4   Feb 22 2005 16:50:10   jmorrissey
//Added sessioInfo and requestId
//
//   Rev 1.3   Jan 26 2005 10:33:26   jmorrissey
//Added properties to hold the start and end dates of searches
//
//   Rev 1.2   Jan 17 2005 15:52:22   tmollart
//Modified [Serializable] directive.
//
//   Rev 1.1   Jan 12 2005 13:52:30   jmorrissey
//Latest versions. Still in development.
//
//   Rev 1.0   Dec 22 2004 11:59:48   jmorrissey
//Initial revision.

using System;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// CostSearchRequest class holds user input information needed to run a cost based search
	/// </summary>
	[Serializable]
	public class CostSearchRequest : ICostSearchRequest
	{
		/// <summary>
		/// default empty constructor
		/// </summary>
		public CostSearchRequest()
		{
			
		}

		private TDLocation originLocation;
		private TDLocation destinationLocation;
		private TDLocation returnOriginLocation;
		private TDLocation returnDestinationLocation;		
		private TDDateTime outwardDateTime;
		private TDDateTime returnDateTime;
		private TDDateTime searchOutwardStartDate;
		private TDDateTime searchOutwardEndDate;
		private TDDateTime searchReturnStartDate;
		private TDDateTime searchReturnEndDate;
		private int outwardFlexibilityDays;
		private int inwardFlexibilityDays;
		private TicketTravelMode [] travelModes;
		private string railDiscountedCard;
		private string coachDiscountedCard;
        private bool routingGuideInfluenced;
        private bool routingGuideCompliantJourneysOnly;
	
		//SessionInfo for this request
		private CJPSessionInfo sessionInfo;

		//The request ID of this cost search		
		private Guid requestId;

		#region public properties

		/// <summary>
		/// Read/Write Property. Gets/Sets originLocation
		/// </summary>
		public TDLocation OriginLocation
		{
			get 
			{
				return originLocation;
			}
			set
			{
				originLocation = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets destinationLocation
		/// </summary>	
		public TDLocation DestinationLocation 
		{
			get 
			{
				return destinationLocation;
			}
			set
			{
				destinationLocation = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets returnOriginLocation
		/// </summary>	
		public TDLocation ReturnOriginLocation 
		{
			get 
			{
				return returnOriginLocation;
			}
			set
			{
				returnOriginLocation = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets returnDestinationLocation
		/// </summary>
		public TDLocation ReturnDestinationLocation 
		{
			get 
			{
				return returnDestinationLocation;
			}
			set
			{
				returnDestinationLocation = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets returnDestinationLocation
		/// </summary>
		public TDDateTime OutwardDateTime 
		{
			get 
			{
				return outwardDateTime;
			}
			set
			{
				outwardDateTime = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets returnDateTime
		/// </summary>
		public TDDateTime ReturnDateTime 
		{
			get 
			{
				return returnDateTime;
			}
			set
			{
				returnDateTime = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets outwardFlexibilityDays
		/// </summary>
		public int OutwardFlexibilityDays
		{
			get 
			{
				return outwardFlexibilityDays;
			}
			set
			{
				outwardFlexibilityDays = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets inwardFlexibilityDays
		/// </summary>
		public int InwardFlexibilityDays 
		{
			get 
			{
				return inwardFlexibilityDays;
			}
			set
			{
				inwardFlexibilityDays = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets travelModes
		/// </summary>
		public TicketTravelMode [] TravelModes 
		{
			get 
			{
				return travelModes;
			}
			set
			{
				travelModes = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets railDiscountedCard
		/// </summary>
		public string RailDiscountedCard 
		{
			get 
			{
				return railDiscountedCard;
			}
			set
			{
				railDiscountedCard = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets coachDiscountedCard
		/// </summary>
		public string CoachDiscountedCard 
		{
			get 
			{
				return coachDiscountedCard;
			}
			set
			{
				coachDiscountedCard = value;
			}
		}		

		/// <summary>
		/// Read/Write Property. Gets/Sets searchOutwardStartDate
		/// </summary>
		public TDDateTime SearchOutwardStartDate 
		{
			get 
			{
				return searchOutwardStartDate;
			}
			set
			{
				searchOutwardStartDate = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets searchOutwardEndDate
		/// </summary>
		public TDDateTime SearchOutwardEndDate 
		{
			get 
			{
				return searchOutwardEndDate;
			}
			set
			{
				searchOutwardEndDate = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets searchReturnStartDate
		/// </summary>
		public TDDateTime SearchReturnStartDate 
		{
			get 
			{
				return searchReturnStartDate;
			}
			set
			{
				searchReturnStartDate = value;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets searchReturnEndDate
		/// </summary>
		public TDDateTime SearchReturnEndDate 
		{
			get 
			{
				return searchReturnEndDate;
			}
			set
			{
				searchReturnEndDate = value;
			}
		}

        #region Routing guide

        /// <summary>
        /// Read/write. If this is true, then the journey request will attempt to return journeys 
        /// that comply with the routing guide
        /// </summary>
        public bool RoutingGuideInfluenced
        {
            get { return routingGuideInfluenced; }
            set { routingGuideInfluenced = value; }
        }

        /// <summary>
        /// Read/write. If this is true, then the journeys returned are routing guide compliant.
        /// </summary>
        public bool RoutingGuideCompliantJourneysOnly
        {
            get { return routingGuideCompliantJourneysOnly; }
            set { routingGuideCompliantJourneysOnly = value; }
        }

        #endregion

        /// <summary>
		/// Read/Write Property. Gets/Sets sessionInfo
		/// </summary>
		public CJPSessionInfo SessionInfo
		{
			get
			{

				return sessionInfo;
			}
			set
			{
				sessionInfo = value;

			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets requestId
		/// </summary>
		public Guid RequestId
		{
			get
			{

				return requestId;	
			}
			set
			{
				requestId = value;

			}
		}
	}

	#endregion
	
}
