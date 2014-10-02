using System;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.SuggestionLinkService.Test
{
	/// <summary>
	/// Summary description for TestStubJourneyRequest.
	/// </summary>
	public class TestStubJourneyRequest : ITDJourneyRequest, ICloneable
	{
		private TDLocation originLocation;
		private TDLocation destinationLocation;

		public TestStubJourneyRequest()
		{
			originLocation = new TDLocation();
			originLocation.Description = "Leeds Station (Rail)";
			
			destinationLocation = new TDLocation();
			destinationLocation.Description = "Bradford Interchange (Rail)";
		}

		public TDLocation OriginLocation
		{
			get { return originLocation; }
			set { originLocation = value; }
		}

		public TDLocation DestinationLocation
		{
			get { return destinationLocation; }
			set { destinationLocation = value; }
		}

		#region Redundant ITDJourneyRequest members

		public ModeType[] Modes
		{
			get { return null;  }
			set { }
		}

        /// <summary>
        /// Determines if outward journey is requested
        /// </summary>
        public bool IsOutwardRequired
        {
            get { return true; }
            set { }
        }

		public bool IsReturnRequired
		{
			get { return true; }
			set { }
		}

		public bool OutwardArriveBefore
		{
			get { return true; }
			set { }
		}

		public bool ReturnArriveBefore
		{
			get { return true; }
			set { }
		}

		public TDDateTime[] OutwardDateTime
		{
			get { return null; }
			set { }
		}

		public TDDateTime[] ReturnDateTime
		{
			get { return null; }
			set { }
		}

		public int InterchangeSpeed
		{
			get { return 0; }
			set { }
		}

		public int WalkingSpeed
		{
			get { return 0; }
			set { }
		}

		public int MaxWalkingTime
		{
			get { return 0; }
			set { }
		}

        public int MaxWalkingDistance
        {
            get { return -1; }
            set { }
        }

		public int DrivingSpeed
		{
			get { return 0; }
			set { }
		}

        public bool AvoidMotorways
        {
            get { return true; }
            set { }
        }

        public bool BanUnknownLimitedAccess
        {
            get { return true; }
            set { }
        }

		public bool DoNotUseMotorways
		{
			get { return true; }
			set { }
		}

		public TDLocation ReturnOriginLocation
		{
			get { return null; }
			set { }
		}

		public TDLocation ReturnDestinationLocation
		{
			get { return null; }
			set { }
		}

		public TDLocation[] PublicViaLocations
		{
			get { return null; }
			set { }
		}

		public TDLocation[] PublicSoftViaLocations
		{
			get { return null; }
			set { }
		}

		public TDLocation[] PublicNotViaLocations
		{
			get { return null; }
			set { }
		}


		public bool TrainUidFilterIsInclude
		{
			get { return true; }
			set { }
		}

		public string[] TrainUidFilter
		{
			get { return null; }
			set { }
		}

		public TDLocation PrivateViaLocation
		{
			get { return null; }
			set { }
		}

		public string[] AvoidRoads
		{
			get { return null; }
			set { }
		}

        public string[] AvoidToidsOutward
        {
            get { return null; }
            set { }
        }

        public string[] AvoidToidsReturn
        {
            get { return null; }
            set { }
        }

		public TDLocation[] AlternateLocations
		{
			get { return null; }
			set { }
		}

		public bool AlternateLocationsFrom
		{
			get { return true; }
			set { }
		}

		public PrivateAlgorithmType PrivateAlgorithm
		{
			get { return PrivateAlgorithmType.Cheapest; }
			set { }
		}

		public PublicAlgorithmType PublicAlgorithm
		{
			get { return PublicAlgorithmType.Default; }
			set { } 
		}

		public TDDateTime ViaLocationOutwardStopoverTime
		{
			get { return null; }
			set { }
		}

		public TDDateTime ViaLocationReturnStopoverTime
		{
			get { return null; }
			set { }
		}

		public TDDateTime ExtraCheckinTime
		{
			get { return null; }
			set { } 
		}

		public bool IsTrunkRequest
		{
			get { return true; }
			set { }
		}

		public bool UseOnlySpecifiedOperators
		{
			get { return true; }
			set { } 
		}

		public string[] SelectedOperators
		{
			get { return null; }
			set { }
		}

		public bool DirectFlightsOnly
		{
			get { return true; }
			set { }
		}

		public bool OutwardAnyTime
		{
			get { return true; }
			set { }
		}

		public bool ReturnAnyTime
		{
			get { return true; }
			set { }
		}

		public string[] RoutingPointNaptans
		{
			get { return null; }
			set { }
		}

        public bool RoutingGuideInfluenced
        {
            get { return false; }
            set { }
        }

        public bool RoutingGuideCompliantJourneysOnly
        {
            get { return false; }
            set { }
        }

        public string RouteCodes
        {
            get { return string.Empty; }
            set { }
        }

		public bool AvoidTolls
		{
			get { return true; }
			set { }
		}

		public string FuelPrice
		{
			get { return null; }
			set { }
		}

		public bool AvoidFerries
		{
			get { return true; }
			set { }
		}

		public string FuelConsumption 
		{
			get { return null; }
			set { }
		}

		public string[] IncludeRoads
		{
			get { return null; }
			set { }
		}

		public VehicleType VehicleType
		{
			get { return VehicleType.Car; }
			set { }
		}

		public string CarSize
		{
			get { return null; }
			set { }
		}

		public string FuelType
		{
			get { return null; }
			set { }
		}

		public int Sequence
		{
			get {return 0; }
			set { }
		}

		public object Clone()
		{
			return this.MemberwiseClone();
		}

        public bool IgnoreCongestion
        {
            get { return false; }
            set { }
        }

        public int CongestionValue
        {
            get { return -1; }
            set { }
        }

        public bool AdjustTimeWithIntervalBefore
        {
            get { return true; }
            set {  }
        }

        public FindAPlannerMode FindAMode
        {
            get { return FindAPlannerMode.None; }
            set { }
        }

        public TDAccessiblePreferences AccessiblePreferences
        {
            get { return new TDAccessiblePreferences(); }
            set { }
        }

        public bool AccessibleRequest
        {
            get { return false; }
            set { }
        }

        public bool DontForceCoach
        {
            get { return false; }
            set { }
        }

        public bool RemoveAwkwardOvernight
        {
            get { return false; }
            set { }
        }

		#endregion
	}
}
