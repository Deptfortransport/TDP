// *********************************************** 
// NAME			: TDJourneyRequest.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TDJourneyRequest class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDJourneyRequest.cs-arc  $
//
//   Rev 1.8   Dec 05 2012 14:13:58   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Nov 13 2012 13:13:30   mmodi
//Added TDAccessiblePreferences
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.6   Sep 01 2011 10:43:22   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.5   Mar 14 2011 15:11:50   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.4   Dec 21 2010 14:05:04   apatel
//Code updated to request services for the day of travel starting from 01:00 on the current day to 01:00 the following day for Find a train, Find a flight and City to City requests
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.3   Nov 24 2010 10:38:38   apatel
//Resolve the issue with SBP planner when planning for today get services from 22:00 yesterday.  For all other dates the period is 00:00 onwards. Make the SBP planner to return journeys planned 00:00 onwards for all days requested
//Resolution for 5644: SBP planner when planning for today you get services from 22:00 yesterday
//
//   Rev 1.2   Oct 12 2009 09:10:58   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:42:52   apatel
//EBC Map and Printer Friendly pages related chages
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:39:44   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Feb 02 2009 16:42:52   mmodi
//Added Routing Guide properties
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:24:00   mturner
//Initial revision.
//
//   Rev 1.19   Nov 01 2005 15:12:56   build
//Automatically merged from branch for stream2638
//
//   Rev 1.18.1.2   Oct 29 2005 14:03:42   tmollart
//Code Review: Modifications to property usage.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.18.1.1   Oct 10 2005 18:00:56   tmollart
//Added Clone method to enable class cloning for VisitPlanner.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.18.1.0   Sep 13 2005 11:56:02   tmollart
//Added property for sequence.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.18   Jul 25 2005 17:41:30   jbroome
//Made Session Aware to avoid data being overwritten when planning journeys
//Resolution for 2619: Dates in the table header displayed incorrectly when Amend Date/Time control used
//
//   Rev 1.17   Mar 15 2005 15:56:12   RAlavi
//Change fuel consumption and fuel cost types to string
//
//   Rev 1.16   Mar 14 2005 11:30:16   esevern
//Added FuelType and CarSize properties
//
//   Rev 1.15   Mar 09 2005 09:38:46   RAlavi
//Added code for car costing
//
//   Rev 1.14   Feb 23 2005 16:40:34   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.13   Feb 23 2005 15:07:48   rscott
//DEL 7 Update - New Properties Added
//
//   Rev 1.12   Jan 27 2005 17:25:16   ralavi
//Added car costing properties
//
//   Rev 1.11   Jan 26 2005 15:52:34   PNorell
//Support for partitioning the session information.
//
//   Rev 1.10   Jan 19 2005 14:45:22   RScott
//DEL 7 - PublicViaLocation removed and PublicViaLocations[ ], PublicSoftViaLocations[ ], PublicNotViaLocations[] added.
//
//   Rev 1.9   Sep 13 2004 16:15:38   RHopkins
//IR1484 Add new attributes to the JourneyRequest for ReturnOriginLocation and ReturnDestinationLocation to allow Extensions to be made to/from Return locations that differ from the corresponding Outward location.
//
//   Rev 1.8   Jul 28 2004 10:59:14   RPhilpott
//Remove now-redundant FlightPlan flag (replaced by IsTrunkRequest for 6.1).
//
//   Rev 1.7   Jul 23 2004 18:13:10   RPhilpott
//Add isTrunkRequest flag
//
//   Rev 1.6   Jun 16 2004 10:13:06   RPhilpott
//More Find-A-Flight additions
//
//   Rev 1.5   Jun 15 2004 13:25:08   RPhilpott
//Find-A-Flight additions
//
//   Rev 1.4   Sep 11 2003 16:34:14   jcotton
//Made Class Serializable
//
//   Rev 1.3   Sep 05 2003 15:29:08   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.2   Aug 20 2003 17:55:54   AToner
//Work in progress
using System;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TDJourneyRequest.
	/// </summary>
	[Serializable()]
	public class TDJourneyRequest : ITDJourneyRequest, ITDSessionAware, ICloneable
    {
        #region Private members

        private ModeType[] modes;
        
        // Outward is true by default
        private bool isOutwardRequired = true;
		private bool isReturnRequired;
		
        private bool outwardArriveBefore;
		private bool returnArriveBefore;
		
		private TDDateTime[] outwardDateTime;
		private TDDateTime[] returnDateTime;

		private int interchangeSpeed;
		private int walkingSpeed;
		private int maxWalkingTime;
        private int maxWalkingDistance = -1;
		private int drivingSpeed;
		private bool avoidMotorways;
        private bool banUnknownLimitedAccess;
		private TDLocation originLocation;
		private TDLocation destinationLocation;
		private TDLocation returnOriginLocation;
		private TDLocation returnDestinationLocation;
		private bool doNotUseMotorways;

		private string[] trainUidFilter;
		private bool trainUidFilterIsInclude;
		private TDLocation[] publicViaLocations;
		private TDLocation[] publicSoftViaLocations;
		private TDLocation[] publicNotViaLocations;

		private string[] routingPointNaptans;

        private bool routingGuideInfluenced;
        private bool routingGuideCompliantJourneysOnly;
        private string routeCodes = string.Empty;

		private TDLocation privateViaLocation;
		private string[] avoidRoads;
        private string[] avoidToidsOutward;
        private string[] avoidToidsReturn;
		private TDLocation[] alternateLocations;
		private bool alternateLocationsFrom;
		private PrivateAlgorithmType privateAlgorithm;
		private PublicAlgorithmType publicAlgorithm;
		bool useOnlySpecifiedOperators;
		string[] selectedOperators;
		private VehicleType vehicleType;

		private bool isTrunkRequest;

		// following only used for Find-A 
		//  (isTrunkRequest == true) ...
		bool outwardAnyTime;
		bool returnAnyTime;

		// following only used for Find-A-Flight 
		//  (isTrunkRequest == true, mode == Air) ...
		private TDDateTime viaLocationOutwardStopoverTime;
		private TDDateTime viaLocationReturnStopoverTime;
		private TDDateTime extraCheckinTime;
		bool directFlightsOnly;

		//following is used for car costing
		private bool avoidTolls;
		private string fuelPrice;
		private bool avoidFerries;
		private string fuelConsumption;
		private string[] includeRoads;
		private string fuelType;
		private string carSize;

		//Added for visit planner
		private int sequence;

        //Added for environment benefits calculator
        private bool ignoreCongestion;
        private int congestionValue;

        //By default allow the time to be adjusted
        private bool adjustTimeWithIntervalBefore = true;

        //Added to determine find a planner mode for the request
        //By default set to none
        private FindAPlannerMode findAMode = FindAPlannerMode.None;

        // Added for accessible journey planner
        private TDAccessiblePreferences accessiblePreferences = null;
        private bool accessibleRequest;
        private bool dontForceCoach;
        private bool removeAwkwardOvernight;

		private bool isDirty = true;

        #endregion

        #region Constructor

        public TDJourneyRequest()
		{
        }

        #endregion

        #region Public properties
       
        public ModeType[] Modes
		{
			get { return modes; }
			set 
			{
				isDirty = true;
				modes = value; 
			}
		}

		public bool IsReturnRequired
		{
			get { return isReturnRequired; }
			set 
			{ 
				isDirty = true;
				isReturnRequired = value; 
			}
		}

        /// <summary>
        /// Determines if outward journey should be planned
        /// </summary>
        public bool IsOutwardRequired
        {
            get { return isOutwardRequired; }
            set
            {
                isDirty = true;
                isOutwardRequired = value;
            }
        }

		public bool OutwardArriveBefore
		{
			get { return outwardArriveBefore; }
			set 
			{ 
				isDirty = true;
				outwardArriveBefore = value; 
			}
		}

		public bool ReturnArriveBefore
		{
			get { return returnArriveBefore; }
			set 
			{ 
				isDirty = true;
				returnArriveBefore = value; 
			}
		}

		public TDDateTime[] OutwardDateTime
		{
			get { return outwardDateTime; }
			set 
			{ 
				isDirty = true;
				outwardDateTime = value; 
			}
		}

		public TDDateTime[] ReturnDateTime
		{
			get { return returnDateTime; }
			set 
			{ 
				isDirty = true;
				returnDateTime = value; 
			}
		}

		public int InterchangeSpeed
		{
			get { return interchangeSpeed; }
			set 
			{ 
				isDirty = true;
				interchangeSpeed = value; 
			}
		}

		public int WalkingSpeed
		{
			get { return walkingSpeed; }
			set 
			{ 
				isDirty = true;
				walkingSpeed = value; 
			}
		}

		public int MaxWalkingTime
		{
			get { return maxWalkingTime; }
			set 
			{ 
				isDirty = true;
				maxWalkingTime = value; 
			}
		}

        /// <summary>
        /// Read/Write property. Sets the maximum walking distance. 
        /// If less than 0, then distance is calculated using WalkingSpeed and MaxWalkingTime
        /// </summary>
        public int MaxWalkingDistance 
        {
            get { return maxWalkingDistance; }
            set { maxWalkingDistance = value; }
        }

		public int DrivingSpeed
		{
			get { return drivingSpeed; }
			set 
			{ 
				isDirty = true;
				drivingSpeed = value; 
			}
		}

        public bool AvoidMotorways
        {
            get { return avoidMotorways; }
            set
            {
                isDirty = true;
                avoidMotorways = value;
            }
        }

        public bool BanUnknownLimitedAccess
        {
            get { return banUnknownLimitedAccess; }
            set
            {
                isDirty = true;
                banUnknownLimitedAccess = value;
            }
        }

		public bool DoNotUseMotorways
		{
			get { return doNotUseMotorways; }
			set 
			{ 
				isDirty = true;
				doNotUseMotorways = value; 
			}
		}

		public TDLocation OriginLocation
		{
			get { return originLocation; }
			set 
			{ 
				isDirty = true;
				originLocation = value; 
			}
		}

		public TDLocation DestinationLocation
		{
			get { return destinationLocation; }
			set 
			{ 
				isDirty = true;
				destinationLocation = value; 
			}
		}

		public TDLocation ReturnOriginLocation
		{
			get 
			{
				if (returnOriginLocation != null)
				{
					return returnOriginLocation;
				}
				else
				{
					return destinationLocation;
				}
			}
			set 
			{ 
				isDirty = true;
				returnOriginLocation = value; 
			}
		}

		public TDLocation ReturnDestinationLocation
		{
			get 
			{
				if (returnDestinationLocation != null)
				{
					return returnDestinationLocation;
				}
				else
				{
					return originLocation;
				}
			}
			set 
			{
				isDirty = true;
				returnDestinationLocation = value; 
			}
		}

		public TDLocation[] PublicViaLocations
		{
			get { return publicViaLocations; }
			set 
			{ 
				isDirty = true;
				publicViaLocations = value; 
			}
		}

		public TDLocation[] PublicSoftViaLocations
		{
			get { return publicSoftViaLocations; }
			set 
			{ 
				isDirty = true;
				publicSoftViaLocations = value; 
			}
		}

		public TDLocation[] PublicNotViaLocations
		{
			get { return publicNotViaLocations; }
			set 
			{ 
				isDirty = true;
				publicNotViaLocations = value;
			}
		}


		public bool TrainUidFilterIsInclude
		{
			get { return trainUidFilterIsInclude; }
			set 
			{ 
				isDirty = true;
				trainUidFilterIsInclude = value; 
			}
		}

		public string[] TrainUidFilter
		{
			get { return trainUidFilter; }
			set 
			{ 
				isDirty = true;
				trainUidFilter = value; 
			}
		}

		public TDLocation PrivateViaLocation
		{
			get { return privateViaLocation; }
			set 
			{ 
				isDirty = true;
				privateViaLocation = value; 
			}
		}

		public string[] AvoidRoads
		{
			get { return avoidRoads; }
			set 
			{ 
				isDirty = true;
				avoidRoads = value;
			}
		}

        /// <summary>
        /// Array of Toids which should be avoided while planning the journey
        /// Use to plan an outward road journey avoiding closures/breakage matched with Travel News
        /// </summary>
        public string[] AvoidToidsOutward
        {
            get { return avoidToidsOutward; }
            set
            {
                isDirty = true;
                avoidToidsOutward = value;
            }
        }

        /// <summary>
        /// Array of Toids which should be avoided while planning the journey
        /// Use to plan a return road journey avoiding closures/breakage matched with Travel News
        /// </summary>
        public string[] AvoidToidsReturn
        {
            get { return avoidToidsReturn; }
            set
            {
                isDirty = true;
                avoidToidsReturn = value;
            }
        }
        

		public TDLocation[] AlternateLocations
		{
			get { return alternateLocations; }
			set 
			{ 
				isDirty = true;
				alternateLocations = value;
			}
		}

		public bool AlternateLocationsFrom
		{
			get { return alternateLocationsFrom; }
			set 
			{ 
				isDirty = true;
				alternateLocationsFrom = value; 
			}
		}

		public PrivateAlgorithmType PrivateAlgorithm
		{
			get { return privateAlgorithm; }
			set 
			{ 
				isDirty = true;
				privateAlgorithm = value; 
			} 
		}

		public PublicAlgorithmType PublicAlgorithm
		{
			get { return publicAlgorithm; }
			set 
			{ 
				isDirty = true;
				publicAlgorithm = value; 
			} 
		}

		public TDDateTime ViaLocationOutwardStopoverTime
		{
			get { return viaLocationOutwardStopoverTime; }
			set 
			{ 
				isDirty = true;
				viaLocationOutwardStopoverTime = value; 
			} 
		}

		public TDDateTime ViaLocationReturnStopoverTime
		{
			get { return viaLocationReturnStopoverTime; }
			set 
			{ 
				isDirty = true;
				viaLocationReturnStopoverTime = value; 
			} 
		}

		public TDDateTime ExtraCheckinTime
		{
			get { return extraCheckinTime; }
			set 
			{ 
				isDirty = true;
				extraCheckinTime = value; 
			} 
		}

		public bool IsTrunkRequest
		{
			get { return isTrunkRequest; }
			set 
			{ 
				isDirty = true;
				isTrunkRequest = value; 
			} 
		}

		public bool UseOnlySpecifiedOperators
		{
			get { return useOnlySpecifiedOperators; }
			set 
			{ 
				isDirty = true;
				useOnlySpecifiedOperators = value; 
			} 
		}

		public string[] SelectedOperators
		{
			get { return selectedOperators; }
			set 
			{ 
				isDirty = true;
				selectedOperators = value; 
			}
		}

		public bool DirectFlightsOnly
		{
			get { return directFlightsOnly; }
			set 
			{ 
				isDirty = true;
				directFlightsOnly = value; 
			}
		}

		public bool OutwardAnyTime
		{
			get { return outwardAnyTime; }
			set 
			{ 
				isDirty = true;
				outwardAnyTime = value; 
			}
		}

		public bool ReturnAnyTime
		{
			get { return returnAnyTime; }
			set 
			{ 
				isDirty = true;
				returnAnyTime = value; 
			}
		}

		public string[] RoutingPointNaptans
		{
			get { return routingPointNaptans; }
			set 
			{ 
				isDirty = true;
				routingPointNaptans = value; 
			}
		}

        // Used for routing guide

        /// <summary>
        /// Read/write. If this is true, then the CJP will adjust its TTBO search parameters 
        /// and attempt to return journeys that comply with the routing guide.
        /// </summary>
        public bool RoutingGuideInfluenced 
        {
            get { return routingGuideInfluenced; }
            set { routingGuideInfluenced = value; }
        }

        /// <summary>
        /// Read/write. If this is true, then the CJP will request that the TTBO reject 
        /// any services that are not routing guide compliant.
        /// </summary>
        public bool RoutingGuideCompliantJourneysOnly 
        {
            get { return routingGuideCompliantJourneysOnly; }
            set { routingGuideCompliantJourneysOnly = value; }
        }

        /// <summary>
        /// Read/write. Used to specify a route code to be used to restrict journeys.  
        /// Route code can be obtained from a fare code.
        /// </summary>
        public string RouteCodes 
        {
            get { return routeCodes; }
            set { routeCodes = value; }
        }

		// Used for car costing functionality

		public bool AvoidTolls
		{
			get { return avoidTolls; }
			set 
			{ 
				isDirty = true;
				avoidTolls = value; 
			}
		}

		public string FuelPrice
		{
			get { return fuelPrice; }
			set 
			{ 
				isDirty = true;
				fuelPrice = value; 
			}
		}

		public bool AvoidFerries
		{
			get { return avoidFerries; }
			set 
			{ 
				isDirty = true;
				avoidFerries = value; 
			}
		}

		public string FuelConsumption 
		{
			get { return fuelConsumption; }
			set 
			{ 
				isDirty = true;
				fuelConsumption = value; 
			}
		}

		public string[] IncludeRoads
		{
			get { return includeRoads; }
			set 
			{ 
				isDirty = true;
				includeRoads = value; 
			}
		}

		public VehicleType VehicleType
		{
			get { return vehicleType; }
			set 
			{ 
				isDirty = true;
				vehicleType = value; 
			}
		}

		public string CarSize
		{
			get { return carSize; }
			set 
			{ 
				isDirty = true;
				carSize = value; 
			}
		}

		public string FuelType
		{
			get { return fuelType; }
			set 
			{ 
				isDirty = true;
				fuelType = value; 
			}
		}

		/// <summary>
		/// Read/Write. Sequence number determines how many journeys
		/// are returned from the CJP for this request.
		/// </summary>
		public int Sequence
		{
			get { return sequence; }
			set
			{
				isDirty = true;
				sequence = value;
			}
		}

        /// <summary>
        /// Read/Write property to determine if congestion to be ignored
        /// </summary>
        public bool IgnoreCongestion
        {
            get { return ignoreCongestion; }
            set { ignoreCongestion = value; }
        }

        /// <summary>
        /// Read/Write property to determine congestion value of route
        /// This property should only be used when IgnoreCongestion is true
        /// </summary>
        public int CongestionValue
        {
            get { return congestionValue; }
            set { congestionValue = value; }
        }

        /// <summary>
        /// Read/Write property to determin if the journey time adjusted while populating the CJP request
        /// </summary>
        public bool AdjustTimeWithIntervalBefore
        {
            get { return adjustTimeWithIntervalBefore; }
            set { adjustTimeWithIntervalBefore = value; }
        }

        /// <summary>
        /// Read/Write property. Determines the find a planner mode i.e. Rail, Flight, City to City etc.
        /// </summary>
        public FindAPlannerMode FindAMode
        {
            get { return findAMode; }
            set { findAMode = value; }
        }

        /// <summary>
        /// Read/Write property. Accessible preferences for the journey request
        /// </summary>
        public TDAccessiblePreferences AccessiblePreferences
        {
            get { return accessiblePreferences; }
            set
            {
                isDirty = true;
                accessiblePreferences = value;
            }
        }

        /// <summary>
        /// Read/Write property. Flag for the CJP to perform an "olympic" request which will ensure the CJP
        /// - uses the single traveline region for journeys
        /// - not use the TTBO/Coach planner for air/rail/coach only journeys
        /// - not validate the results 
        /// - add TTBO service information where available
        /// </summary>
        public bool AccessibleRequest 
        { 
            get { return accessibleRequest;}
            set
            {
                isDirty = true;
                accessibleRequest = value; 
            }
        }

        /// <summary>
        /// Read/Write property. Don't force coach.
        /// Flag to override the CJP don't force coach rules. 
        /// Default is false to allow CJP to determine how to apply force coach rule. 
        /// True will prevent CJP from applying the force coach rules
        /// </summary>
        public bool DontForceCoach
        {
            get { return dontForceCoach; }
            set
            {
                isDirty = true;
                dontForceCoach = value;
            }
        }

        /// <summary>
        /// Read/Write property. Flag to remove awkward overnight journeys. 
        /// This removes journeys that breach overnight journey rules, in particular for journeys that arrive for
        /// early morning having long waits in the middle of the night.
        /// </summary>
        public bool RemoveAwkwardOvernight
        {
            get { return removeAwkwardOvernight; }
            set
            {
                isDirty = true;
                removeAwkwardOvernight = value;
            }
        }

        #endregion
        
        #region ITDSessionAware Members

        /// <summary>
		/// Read/write property indicating whether or not the object has changed since
		/// it was last saved. Note that changes to properties which return objects or arrays
		/// (e.g. PrivateViaLocation, AlternateLocations) do not cause this flag to be set to
		/// true, so it must be set manually.
		/// </summary>
		public bool IsDirty
		{
			get { return isDirty; }
			set { isDirty = value; }
		}

		#endregion
        
		#region ICloneable Members
		
		/// <summary>
		/// Performs a memberwise clone of this object. 
		/// </summary>
		/// <returns>A copy of this object</returns>
		public object Clone()
		{
			return this.MemberwiseClone();
		}
		
		#endregion
	}
}
