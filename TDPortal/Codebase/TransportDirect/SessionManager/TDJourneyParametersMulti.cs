#region Heading and version history
// ***********************************************
// NAME 		: TDJourneyParametersMulti.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 21/05/2004
// DESCRIPTION 	: Journey parameters for multimodal planner flight functions
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDJourneyParametersMulti.cs-arc  $
//
//   Rev 1.11   Jan 20 2013 16:26:28   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.10   Dec 05 2012 13:59:22   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.9   Nov 16 2012 14:00:48   DLane
//Setting walk parameters in the journey request
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.8   Nov 13 2012 13:15:34   mmodi
//Added TDAccessiblePreferences
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Sep 06 2011 11:20:38   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.6   Sep 01 2011 10:43:54   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.5   Mar 14 2011 15:11:56   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.4   Dec 06 2010 12:54:56   apatel
//Code updated to implement show all show 10 feature for journey results and to remove anytime option from the input page.
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.3   Oct 12 2009 09:11:08   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Oct 12 2009 08:39:48   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 20 2008 11:11:52   mmodi
//Updated to allow override location coordinates to be used for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Oct 13 2008 16:46:38   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.4   Oct 10 2008 15:50:34   mmodi
//Updated to have avoid time based check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.3   Aug 22 2008 10:18:42   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.2   Jul 28 2008 13:15:54   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.1   Jul 18 2008 14:02:04   mmodi
//Updates for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.0   Jun 20 2008 14:48:54   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:48:40   mturner
//Initial revision.
//
//   Rev 1.29   Jun 02 2006 15:03:44   rphilpott
//Initialise PublicAlgorithm to Default, not Fastest.
//Resolution for 4104: Wrong PublicAlgorithm setting from Find Nearest
//
//   Rev 1.28   Apr 05 2006 15:42:42   build
//Automatically merged from branch for stream0030
//
//   Rev 1.27.1.2   Apr 05 2006 11:02:12   esevern
//added walk to public modes created for Find a Bus
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.27.1.1   Mar 29 2006 12:45:20   mdambrine
//fixed the problem with the date control not showing the time now
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.27.1.0   Mar 24 2006 11:52:16   esevern
//amended InitialiseGeneric() so that 'Anytime' is not included in the time options when on Find a Bus.
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.27   Dec 05 2005 17:14:18   jgeorge
//Made IsOpenReturn property read only and derived from ReturnMonthYear.
//Resolution for 3313: Door-to-door: selecting open return on input page does not display return fares on results page
//
//   Rev 1.26   Oct 31 2005 12:06:56   tmollart
//Merge with stream 2638
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.25   Oct 11 2005 10:07:26   MTurner
//Fix for IR 2804 - Vantive 3947408.  Journey details do not match dropdown menus for car journeys.
//
//   Rev 1.24   Sep 01 2005 17:49:36   asinclair
//Fix for IR 2659 and code tidy up
//Resolution for 2659: DN062 Not all non-default journey options returned in feedback
//
//   Rev 1.23   Aug 26 2005 10:18:58   asinclair
//Fix for IRs2717 and 2659
//Resolution for 2717: DN062 - 'Arriving' misspelt  in last journey details section on the Feedback page
//
//   Rev 1.22   Aug 19 2005 10:40:28   asinclair
//Merge for Stream2572 and changes made following Code Review
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.21   Aug 09 2005 14:09:32   tmollart
//Updated to add comments to properties.
//
//   Rev 1.20.1.1   Aug 09 2005 16:35:58   asinclair
//Fixed errors in Journey details displayed
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.20.1.0   Jul 27 2005 18:11:36   asinclair
//Check in to fix build errors.  Work in progress
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.20   Apr 20 2005 16:44:18   jmorrissey
//Default fuelConsumptionValid and fuelCostValid to true
//
//   Rev 1.19   Apr 13 2005 12:22:08   Ralavi
//Added new properties for fuel consumption and fuel cost validations
//
//   Rev 1.18   Apr 01 2005 13:34:50   Ralavi
//Changes for passing correct fuel consumption to CJP
//
//   Rev 1.17   Mar 09 2005 09:58:32   RAlavi
//Added VehicleType
//
//   Rev 1.16   Mar 08 2005 09:33:36   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.15   Mar 02 2005 15:27:16   RAlavi
//Changes for ambiguity
//
//   Rev 1.14   Feb 24 2005 11:37:32   PNorell
//Updated for favourite details.
//
//   Rev 1.13   Feb 21 2005 13:02:04   esevern
//changed roadlist from arraylist to TDRoad array
//
//   Rev 1.12   Feb 18 2005 16:54:16   esevern
//Car costing - added arraylists for avoid/use roads
//
//   Rev 1.11   Feb 01 2005 10:25:28   rgreenwood
//Removed fuelConsumptionUnit at request of Peter Norell
//
//   Rev 1.10   Jan 28 2005 11:30:00   rgreenwood
//Minor corrections to variable names, and added reference to TransportDirect.Common
//
//   Rev 1.9   Jan 28 2005 10:54:10   ralavi
//Updated version for car costing
//
//   Rev 1.8   Aug 17 2004 15:45:54   passuied
//Don't blank Hour and Minute if in car mode
//Resolution for 1349: Find a car "Any time" should not be an option
//
//   Rev 1.7   Aug 03 2004 11:50:20   COwczarek
//Use new IsFindAMode property
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.6   Jul 23 2004 18:34:42   RPhilpott
//Changes for DEL 6.1 Trunk Planning
//
//   Rev 1.5   Jun 28 2004 20:49:10   JHaydock
//JourneyPlannerInput clear page and back buttons for extend journey
//
//   Rev 1.4   Jun 23 2004 16:38:46   COwczarek
//Add methods to detect whether stop over times have been entered by user
//Resolution for 1044: Add date validation to extend journey functionality
//
//   Rev 1.3   Jun 15 2004 15:47:02   RPhilpott
//Move walking speed, etc, from ...Multi to base class so that we can use the default values more easily for ...Flight.
//
//   Rev 1.2   Jun 04 2004 13:35:12   RHopkins
//Added properties for Amend Stopover Control
//
//   Rev 1.1   Jun 04 2004 09:58:26   RPhilpott
//Use DataServices to get default location search types instead of random hard-coding. 
//
//   Rev 1.0   May 26 2004 08:54:52   jgeorge
//Initial revision.
#endregion

using System;
using System.Collections;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using System.Text;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TDJourneyParametersMultiModal.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class TDJourneyParametersMulti : TDJourneyParameters
    {
        #region Private variables

        private bool privateRequired;
		private bool publicRequired;
        private bool cycleRequired;
		private int drivingSpeed;
		private bool avoidMotorWays;
        private bool banUnknownLimitedAccess;
		private string avoidRoads;
		private bool alternateLocationsFrom;
		private TDLocation publicViaLocation;
		private TDLocation privateViaLocation;
        private TDLocation cycleViaLocation;
		private PrivateAlgorithmType privateAlgorithmType;
		private TDLocation[] alternateLocations;
		private LocationSearch publicVia;
		private LocationSearch privateVia;
        private LocationSearch cycleVia;
		private LocationSearch[] alternates;
		private int outwardStopoverDays;
		private int outwardStopoverHours;
		private int outwardStopoverMinutes;
		private int returnStopoverDays;
		private int returnStopoverHours;
		private int returnStopoverMinutes;

		// LocationSelectControl control type
		private LocationSelectControlType typePublicVia;
		private LocationSelectControlType typePrivateVia;
        private LocationSelectControlType typeCycleVia;

		// Car costing changes
		private bool avoidTolls;
		private bool avoidFerries;
		private bool fuelCostOption;
		private string fuelCostEntered;
		private bool fuelConsumptionOption;
		private string fuelConsumptionEntered;
		private int fuelConsumptionUnit;
		private string carSize;
		private string carFuelType;
		private bool doNotUseMotorways;
		private int fuelUseUnitChange;
		private VehicleType vehicleType;
		private bool fuelConsumptionValid;
		private bool fuelCostValid;

		private TDRoad[] avoidRoadsList;
        private string[] avoidToidsListOutward;
        private string[] avoidToidsListReturn;
		private TDRoad[] useRoadsList;
        
        // Cycle journey preferences
        private string cycleJourneyType;
        private string cycleSpeedMax; // in metres per hour
        private string cycleSpeedText; // value entered by user
        private string cycleSpeedUnit;
        private bool cycleSpeedIsDefault;
        private bool cycleSpeedValid;
        private bool cycleAvoidSteepClimbs;
        private bool cycleAvoidUnlitRoads;
        private bool cycleAvoidWalkingYourBike;
        private bool cycleAvoidTimeBased;
        private string cyclePenaltyFunction;
        private string cyclePenaltyFunctionOverride;
        private bool cycleLocationsIsDefault;
        private OSGridReference cycleLocationOriginOverride;
        private OSGridReference cycleLocationDestinationOverride;

        // Environment benefits calculator preferences
        private bool ignoreCongestion;
        private int congestionValue;

        // Accessible journey preferences
        private bool doNotUseUnderground = false;
        private bool requireSpecialAssistance = false;
        private bool requireStepFreeAccess = false;
        private bool requireFewerInterchanges = false;
        private int accessibleWalkSpeed = 0;
        private int accessibleWalkDistance = 0;

        #endregion

        #region Constructor

        public TDJourneyParametersMulti() : base()
		{
        }

        #endregion

        #region Public properties
        public bool PrivateRequired
		{
			get { return privateRequired; }
			set { privateRequired = value; }
		}

		public bool PublicRequired
		{
			get { return publicRequired; }
			set { publicRequired = value; }
		}

        public bool CycleRequired
        {
            get { return cycleRequired; }
            set { cycleRequired = value; }
        }

		public bool IsOpenReturn
		{
			get 
			{ 
				return ReturnMonthYear == ReturnType.OpenReturn.ToString(); 
			}
		}

		public int DrivingSpeed
		{
			get { return drivingSpeed; }
			set { drivingSpeed = value; }
		}

        public bool AvoidMotorWays
        {
            get { return avoidMotorWays; }
            set { avoidMotorWays = value; }
        }

        public bool BanUnknownLimitedAccess
        {
            get { return banUnknownLimitedAccess; }
            set { banUnknownLimitedAccess = value; }
        }

		public string AvoidRoads
		{
			get { return avoidRoads; }
			set { avoidRoads = value; }
		}

		public bool AlternateLocationsFrom
		{
			get { return alternateLocationsFrom; }
			set { alternateLocationsFrom = value; }
		}

		public TDLocation PublicViaLocation
		{
			get { return publicViaLocation; }
			set { publicViaLocation = value; }
		}

		public TDLocation PrivateViaLocation
		{
			get { return privateViaLocation; }
			set { privateViaLocation = value; }
		}

        public TDLocation CycleViaLocation
        {
            get { return cycleViaLocation; }
            set { cycleViaLocation = value; }
        }

		public PrivateAlgorithmType PrivateAlgorithmType
		{
			get { return privateAlgorithmType; }
			set { privateAlgorithmType = value; }
		}

		public TDLocation[] AlternateLocations
		{
			get { return alternateLocations; }
			set { alternateLocations = value; }
		}

		public LocationSearch PublicVia
		{
			get { return publicVia; }
			set { publicVia = value; }
		}

		public LocationSearch PrivateVia
		{
			get { return privateVia; }
			set { privateVia = value; }
		}

        public LocationSearch CycleVia
        {
            get { return cycleVia; }
            set { cycleVia = value; }
        }

		public LocationSearch[] Alternates
		{
			get { return alternates; }
			set { alternates = value; }
		}

		/// <summary>
		///  Get/Set property. Control type for public via control
		/// </summary>
		public LocationSelectControlType PublicViaType
		{
			get { return typePublicVia; }
			set { typePublicVia = value; }
		}


		/// <summary>
		///  Get/Set property. Control type for private via control
		/// </summary>
		public LocationSelectControlType PrivateViaType
		{
			get { return typePrivateVia; }
			set { typePrivateVia = value; }
		}


		/// <summary>
		/// Get/Set property. Number of days to stopover on outward trip between existing Itinerary and new extension
		/// </summary>
		public int OutwardStopoverDays
		{
			get { return outwardStopoverDays; }
			set { outwardStopoverDays = value; }
		}


		/// <summary>
		/// Get/Set property. Number of hours to stopover on outward trip between existing Itinerary and new extension
		/// </summary>
		public int OutwardStopoverHours
		{
			get { return outwardStopoverHours; }
			set { outwardStopoverHours = value; }
		}


		/// <summary>
		/// Get/Set property. Number of minutes to stopover on outward trip between existing Itinerary and new extension
		/// </summary>
		public int OutwardStopoverMinutes
		{
			get { return outwardStopoverMinutes; }
			set { outwardStopoverMinutes = value; }
		}

		/// <summary>
		/// Readonly property that return true if the outward stop over time has been modified from 
		/// the initial state, or if all values are zero.
		/// </summary>
		public bool OutwardStopOverSet
		{
			get 
			{
				bool notChangedFromInitial = outwardStopoverDays == 0 && outwardStopoverHours == 0 && outwardStopoverMinutes == 0;
				bool notUsed = outwardStopoverDays == -1 && outwardStopoverHours == -1 && outwardStopoverMinutes == -1;
				return !(notChangedFromInitial || notUsed);
			}
		}                                                                                                

		/// <summary>
		/// Get/Set property. Number of days to stopover on return trip between existing Itinerary and new extension
		/// </summary>
		public int ReturnStopoverDays
		{
			get { return returnStopoverDays; }
			set { returnStopoverDays = value; }
		}


		/// <summary>
		/// Get/Set property. Number of hours to stopover on return trip between existing Itinerary and new extension
		/// </summary>
		public int ReturnStopoverHours
		{
			get { return returnStopoverHours; }
			set { returnStopoverHours = value; }
		}


		/// <summary>
		/// Get/Set property. Number of minutes to stopover on return trip between existing Itinerary and new extension
		/// </summary>
		public int ReturnStopoverMinutes
		{
			get { return returnStopoverMinutes; }
			set { returnStopoverMinutes = value; }
		}

		/// <summary>
		/// Readonly property that return true if the return stop over time has been modified from 
		/// the initial state, or if all values are zero.
		/// </summary>
		public bool ReturnStopOverSet
		{
			get 
			{
				bool notChangedFromInitial = returnStopoverDays == 0 && returnStopoverHours == 0 && returnStopoverMinutes == 0;
				bool notUsed = returnStopoverDays == -1 && returnStopoverHours == -1 && returnStopoverMinutes == -1;
				return !(notChangedFromInitial || notUsed);
			}
        }

        #region Car properties
        // Car Costing
		/// <summary>
		/// Read/Write. Status of the avoid tolls check box.
		/// </summary>
		public bool AvoidTolls
		{
			get { return avoidTolls; }
			set { avoidTolls = value; }
		}  
     
		/// <summary>
		/// Read/Write. Status of the avoid ferries check box.
		/// </summary>
		public bool AvoidFerries
		{
			get { return avoidFerries; }
			set { avoidFerries = value; }
		}       
		
		/// <summary>
		/// Read/Write. The status of the fuel cost option. Has the user accepted
		/// the default fuel price or entered their own. True indicates the users
		/// own value has been entered.
		/// </summary>
		public bool FuelCostOption
		{
			get { return fuelCostOption; }
			set { fuelCostOption = value; }
		}

		/// <summary>
		/// Read/Write. Entered by the user and indicates the cost of the fuel.
		/// </summary>
		public string FuelCostEntered
		{
			get { return fuelCostEntered; }
			set { fuelCostEntered = value; }
		}
		
		/// <summary>
		/// Read/Write. The status of the fuel consumption check box. Has the user
		/// accepted the default fuel consumption (dependant on car/fuel type selected)
		/// or entered their own value for fuel consumption. True indicates user
		/// has specified a value.
		/// </summary>
		public bool FuelConsumptionOption
		{
			get { return fuelConsumptionOption; }
			set { fuelConsumptionOption = value; }
		}

		/// <summary>
		/// Read/Write. The entered fuel consumption value. Unit of measurement
		/// depends on unit of measurement selected by user. Default unit is MPG.
		/// </summary>
		public string FuelConsumptionEntered
		{
			get { return fuelConsumptionEntered; }
			set { fuelConsumptionEntered = value; }
		}

		/// <summary>
		/// Read/Write. Is the fuel consumption entered by the user valid.
		/// False indicates that an invalid entry e.g. "abc" has been entered .
		/// </summary>
		public bool FuelConsumptionValid
		{
			get { return fuelConsumptionValid; }
			set { fuelConsumptionValid = value; }
		}

		/// <summary>
		/// Read/Write. Is the fuel cost entered by the user valid. False 
		/// indicates an invalid cost such as "abc" has been entered.
		/// </summary>
		public bool FuelCostValid
		{
			get { return fuelCostValid; }
			set { fuelCostValid = value; }
		}

		/// <summary>
		/// Read/Write. The selected fuel consumption unit. Numerical respresentation
		/// of either MPG or Ltr/KM etc. Specified in database.
		/// </summary>
		public int FuelConsumptionUnit
		{
			get { return fuelConsumptionUnit; }
			set { fuelConsumptionUnit = value; }
		}

		/// <summary>
		/// Read/Write. Selected car size e.g. "small", "medium" or "large".
		/// </summary>
		public string CarSize
		{
			get { return carSize; }
			set { carSize = value; }
		}

		/// <summary>
		/// Read/Write. Selected fuel type e.g. "petrol" or "diesel".
		/// </summary>
		public string CarFuelType
		{
			get { return carFuelType; }
			set { carFuelType = value; }
		}

		/// <summary>
		/// Read/Write. Fuel Use Unit Change - possibly not used (TCM).
		/// </summary>
		public int FuelUseUnitChange
		{
			get { return fuelUseUnitChange; }
			set { fuelUseUnitChange = value; }
		}

		/// <summary>
		/// Read/Write. Status of the do not use motorways check box.
		/// </summary>
		public bool DoNotUseMotorways
		{
			get { return doNotUseMotorways; }
			set { doNotUseMotorways = value; }
		}

		/// <summary>
		/// Read/write property.  TDRoad-list containing roads to 
		/// avoid when performing car journey planning 
		/// </summary>
		public TDRoad[] AvoidRoadsList
		{
			get { return avoidRoadsList; }
			set { avoidRoadsList = value; }
		}

        /// <summary>
        /// Read/write property. List containing Toids to 
        /// avoid when performing outward car journey planning
        /// </summary>
        public string[] AvoidToidsListOutward
        {
            get { return avoidToidsListOutward; }
            set { avoidToidsListOutward = value; }
        }

        /// <summary>
        /// Read/write property. List containing Toids to 
        /// avoid when performing return car journey planning
        /// </summary>
        public string[] AvoidToidsListReturn
        {
            get { return avoidToidsListReturn; }
            set { avoidToidsListReturn = value; }
        }

		/// <summary>
		/// Read/write property.  TDRoad-list containing roads to 
		/// use when performing car journey planning 
		/// </summary>
		public TDRoad[] UseRoadsList
		{
			get { return useRoadsList; }
			set { useRoadsList = value; }
		}

		public VehicleType VehicleType
		{
			get { return vehicleType; }
			set { vehicleType = value; }
        }

        #endregion

        #region Cycle planner properties

        /// <summary>
        /// Read/write property for Cycle journey type
        /// </summary>
        public string CycleJourneyType
        {
            get { return cycleJourneyType; }
            set { cycleJourneyType = value; }
        }

        /// <summary>
        /// Read/write property for Cycle journey speed max, (in metres per hour)
        /// </summary>
        public string CycleSpeedMax
        {
            get { return cycleSpeedMax; }
            set { cycleSpeedMax = value; }
        }

        /// <summary>
        /// Read/write property for Cycle journey speed text entered
        /// </summary>
        public string CycleSpeedText
        {
            get { return cycleSpeedText; }
            set { cycleSpeedText = value; }
        }

        /// <summary>
        /// Read/write property for Cycle journey speed unit
        /// </summary>
        public string CycleSpeedUnit
        {
            get { return cycleSpeedUnit; }
            set { cycleSpeedUnit = value; }
        }

        /// <summary>
        /// Read/write property for Cycle speed valid
        /// </summary>
        public bool CycleSpeedValid
        {
            get { return cycleSpeedValid; }
            set { cycleSpeedValid = value; }
        }

        /// <summary>
        /// Read/write property used to track if cycle speed used if default
        /// </summary>
        public bool CycleSpeedIsDefault
        {
            get { return cycleSpeedIsDefault; }
            set { cycleSpeedIsDefault = value; }
        }

        /// <summary>
        /// Read/write. Cycle avoid steep climbs preference
        /// </summary>
        public bool CycleAvoidSteepClimbs
        {
            get { return cycleAvoidSteepClimbs; }
            set { cycleAvoidSteepClimbs = value; }
        }

        /// <summary>
        /// Read/write. Cycle avoid unlit roads preference
        /// </summary>
        public bool CycleAvoidUnlitRoads
        {
            get { return cycleAvoidUnlitRoads; }
            set { cycleAvoidUnlitRoads = value; }
        }

        /// <summary>
        /// Read/write. Cycle avoid walking your bike preference
        /// </summary>
        public bool CycleAvoidWalkingYourBike
        {
            get { return cycleAvoidWalkingYourBike; }
            set { cycleAvoidWalkingYourBike = value; }
        }

        /// <summary>
        /// Read/write. Cycle avoid time based restrictions preference
        /// </summary>
        public bool CycleAvoidTimeBased
        {
            get { return cycleAvoidTimeBased; }
            set { cycleAvoidTimeBased = value; }
        }

        /// <summary>
        /// Read/write property for the Penalty Function to use.
        /// This value is populated by the TDJourneyParametersCycleConverter and is made
        /// using the CycleJourneyType or CyclePenaltyFunctionOverride.
        /// </summary>
        public string CyclePenaltyFunction
        {
            get { return cyclePenaltyFunction; }
            set { cyclePenaltyFunction = value; }
        }

        /// <summary>
        /// Read/write property for a Penalty Function Override value to use
        /// </summary>
        public string CyclePenaltyFunctionOverride
        {
            get { return cyclePenaltyFunctionOverride; }
            set { cyclePenaltyFunctionOverride = value; }
        }

        /// <summary>
        /// Read/write. Cycle property indicating is locations to use are the default. 
        /// False indicates the location override values should be used
        /// </summary>
        public bool CycleLocationsIsDefault
        {
            get { return cycleLocationsIsDefault; }
            set { cycleLocationsIsDefault = value; }
        }

        /// <summary>
        /// Read/write. Cycle property for the Origin Location Override value to use
        /// </summary>
        public OSGridReference CycleLocationOriginOverride
        {
            get { return cycleLocationOriginOverride; }
            set { cycleLocationOriginOverride = value; }
        }

        /// <summary>
        /// Read/write. Cycle property for the Destination Location Override value to use
        /// </summary>
        public OSGridReference CycleLocationDestinationOverride
        {
            get { return cycleLocationDestinationOverride; }
            set { cycleLocationDestinationOverride = value; }
        }

        /// <summary>
        ///  Read/write property. Control type for cycle via control
        /// </summary>
        public LocationSelectControlType CycleViaType
        {
            get { return typeCycleVia; }
            set { typeCycleVia = value; }
        }

        #endregion

        #region Environment benefits calculator properties
        /// <summary>
        /// Read/Write propety indicating is congestion to be ignored 
        /// when doing road journey for environment benefits calculator 
        /// </summary>
        public bool IgnoreCongestion
        {
            get { return ignoreCongestion; }
            set { ignoreCongestion = value; }
        }

        /// <summary>
        /// Read/Write propety indicating the congestion value of the route
        /// </summary>
        public int CongestionValue
        {
            get { return congestionValue; }
            set { congestionValue = value; }
        }
        #endregion

        #region Accessible journey preferences

        /// <summary>
        /// Read/Write. DoNotUseUnderground
        /// </summary>
        public bool DoNotUseUnderground
        {
            get { return doNotUseUnderground; }
            set { doNotUseUnderground = value; }
        }

        /// <summary>
        /// Read/Write. RequireSpecialAssistance
        /// </summary>
        public bool RequireSpecialAssistance
        {
            get { return requireSpecialAssistance; }
            set { requireSpecialAssistance = value; }
        }

        /// <summary>
        /// Read/Write. RequireStepFreeAccess
        /// </summary>
        public bool RequireStepFreeAccess
        {
            get { return requireStepFreeAccess; }
            set { requireStepFreeAccess = value; }
        }

        /// <summary>
        /// Read/Write. RequireFewerInterchanges
        /// </summary>
        public bool RequireFewerInterchanges
        {
            get { return requireFewerInterchanges; }
            set { requireFewerInterchanges = value; }
        }

        /// <summary>
        /// Read/Write. AccessibleWalkSpeed
        /// </summary>
        public int AccessibleWalkSpeed
        {
            get { return accessibleWalkSpeed; }
            set { accessibleWalkSpeed = value; }
        }

        /// <summary>
        /// Read/Write. AccessibleWalkDistance
        /// </summary>
        public int AccessibleWalkDistance
        {
            get { return accessibleWalkDistance; }
            set { accessibleWalkDistance = value; }
        }

        #endregion

        #endregion

        public override void Initialise()
		{
			base.Initialise();

			AlternateLocationsFrom = true;

			InitialiseGeneric();
		}

        /// <summary>
        /// Returns string containing details of journey
        /// </summary>
		public override string InputSummary()
		{

			StringBuilder summary = new StringBuilder();

            #region Locations

            summary.Append("\nFrom: ");
			summary.Append(OriginLocation.Description);
			summary.Append(" ");
			summary.Append(OriginLocation.SearchType.ToString());
			summary.Append("\r\n");

			summary.Append("To: ");
			summary.Append(DestinationLocation.Description);
			summary.Append(" ");
			summary.Append(DestinationLocation.SearchType.ToString());
			summary.Append("\r\n");

            #endregion

            #region Date/time

            if (OutwardArriveBefore)
			{
				summary.Append("Outward journey: Arriving by ");
			}
			else
			{
				summary.Append("Outward journey: Leaving after ");
			}

			if (OutwardHour != "Any")
			{
				summary.Append(OutwardHour);
				summary.Append(":");
				summary.Append(OutwardMinute);
				summary.Append(" ");
			}
			else
			{
				summary.Append("Anytime");
				summary.Append(" ");
			}

			summary.Append(OutwardDayOfMonth);
			summary.Append("/");
			summary.Append(OutwardMonthYear);
			summary.Append("\r\n");
			
			if (IsReturnRequired)
			{
	
				if (ReturnArriveBefore)
				{
					summary.Append("Return journey: Arriving by ");
				}
				else
				{
					summary.Append("Return journey: Leaving after ");
				}

				if (ReturnHour != "Any")
				{
					summary.Append(ReturnHour);
					summary.Append(":");
					summary.Append(ReturnMinute);
					summary.Append(" ");
				}
				else
				{
					summary.Append("Anytime");
					summary.Append(" ");
				}

				summary.Append(ReturnDayOfMonth);
				summary.Append("/");
				summary.Append(ReturnMonthYear);
				summary.Append("\r\n");
			}
			else
			{
				summary.Append("Return journey: none");
                summary.Append("\r\n");
            }

            #endregion

            #region Modes

            summary.Append("Using:");

			foreach (ModeType mymode in publicModes)
			{
                summary.Append(" ");
                summary.Append(mymode.ToString());
			}

			if(PrivateRequired)
			{
                summary.Append(" ");
                summary.Append("Car");
			}

            summary.AppendLine("\r\n");

            #endregion
                        
            DataServices.IDataServices populator = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            #region Public transport specific

            // Via
            if (PublicViaLocation.Description.Length > 0)
            {
                summary.Append("Public transport via: ");
                summary.Append(PublicViaLocation.Description);
                summary.Append("\r\n");
            }

            if ((publicModes.Length >0) && (!cycleRequired))
			{
                // Accessible options
                if (requireSpecialAssistance || requireStepFreeAccess || requireFewerInterchanges || doNotUseUnderground)
                {
                    summary.Append("Accessible options: ");
                    
                    if (requireStepFreeAccess)
                        summary.Append("StepFree ");
                    if (requireSpecialAssistance)
                        summary.Append("Assistance ");
                    if (requireFewerInterchanges)
                        summary.Append("FewestChanges ");
                    if (doNotUseUnderground)
                        summary.Append("DoNotUseUnderground");
                    
                    summary.Append("\r\n");
                }

				//Type of journey (number of changes)
				if(publicAlgorithmType != PublicAlgorithmType.Default)
				{
					string itemvalue = populator.GetResourceId(DataServiceType.ChangesFindDrop, publicAlgorithmType.ToString());
					summary.Append("Number of changes: ");
					summary.Append(itemvalue);
					summary.Append("\r\n");
				}

				//Speed for making changes
				if (!InterchangeSpeed.Equals(Convert.ToInt32 (populator.GetDefaultListControlValue (DataServiceType.ChangesSpeedDrop) ) ))
				{
					summary.Append("Speed for making changes: ");
					string changespeed = populator.GetResourceId(DataServiceType.ChangesSpeedDrop, InterchangeSpeed.ToString()).ToString();
					summary.Append(changespeed);
					summary.Append("\r\n");
				}

				//Walking speed
				if(!WalkingSpeed.Equals(Convert.ToInt32 (populator.GetDefaultListControlValue( DataServiceType.WalkingSpeedDrop) )))
				{
					summary.Append("Walking speed: ");
					string walkspeed = populator.GetResourceId(DataServiceType.WalkingSpeedDrop, WalkingSpeed.ToString()).ToString();
					summary.Append(walkspeed);
					summary.Append("\r\n");
				}

				//Walking time
				if(!MaxWalkingTime.Equals(Convert.ToInt32 (populator.GetDefaultListControlValue (DataServiceType.WalkingMaxTimeDrop))))
				{
					summary.Append("Maximum walking time: ");
					summary.Append(MaxWalkingTime.ToString());
					summary.Append(" mins");
					summary.Append("\r\n");
				}
            }
            #endregion

            #region Car specific
            //Car Journey Details
			//Type of journey
			if(privateAlgorithmType != PrivateAlgorithmType.Fastest)
			{
				summary.Append("Type of journey: ");

				// 3947408 - Change to append a string consistent with the option
				// selected by the user on the journey planning screen rather than simply
				// the value of the PrivateAlgorithmType property.
				switch (PrivateAlgorithmType)
				{
					case PrivateAlgorithmType.Cheapest:
						summary.Append("Cheapest Overall");
						break;
					case PrivateAlgorithmType.MostEconomical:
						summary.Append("Most Fuel Economic");
						break;
					case PrivateAlgorithmType.Shortest:
						summary.Append("Shortest");
						break;
					default:
						summary.Append(privateAlgorithmType.ToString());
						break;
				}
				// End of new code for 3947408

				summary.Append("\r\n");
			}

			//Driving speed
			if(!drivingSpeed.Equals(Convert.ToInt32 (populator.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop))))
			{
				summary.Append("Maximum car speed: ");
				string speed = populator.GetResourceId(DataServiceType.DrivingMaxSpeedDrop, drivingSpeed.ToString()).ToString();
				summary.Append(speed);
				summary.Append(" mph");
				summary.Append("\r\n");
			}

			//Do not use motorways box
			if(doNotUseMotorways)
			{
				summary.Append("Do not use Motorways");
				summary.Append("\r\n");
			}

			//Car size
			if(!carSize.Equals(populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop)))
			{
				summary.Append("Car size: ");
				summary.Append(carSize.ToString());
				summary.Append("\r\n");
			}

			//Car engine type
			if(!carFuelType.Equals(populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop)))
			{
				summary.Append("Fuel type: ");
				summary.Append(carFuelType.ToString());
				summary.Append("\r\n");
			}

			//Fuel consumption values changed from defaults
			if(!fuelConsumptionOption)
			{
				summary.Append("Fuel consumption :");
				summary.Append(fuelConsumptionEntered);
				summary.Append(" ");
				if (fuelConsumptionUnit == 1)
				{
					summary.Append("Miles/gallon");
				}
				else
				{
					summary.Append("Litres/100 km");
				}
				summary.Append("\r\n");
			}

			//Fuel cost options changes from defaults
			if(!fuelCostOption)
			{
				summary.Append("Fuel cost: ");
				summary.Append(fuelCostEntered);
				summary.Append(" pence/litre");
				summary.Append("\r\n");

			}

			if(avoidTolls)
			{
				summary.Append("Avoid Tolls");
				summary.Append("\r\n");
			}

			if(avoidFerries)
			{
				summary.Append("Avoid Ferries");
				summary.Append("\r\n");
			}
		
			if(AvoidMotorWays)
			{
				summary.Append("Avoid Motorways");
				summary.Append("\r\n");
			}
	
			//List of roads to use
			if(useRoadsList.Length >0)
			{
				summary.Append("Using roads: ");

				foreach (TDRoad road in useRoadsList)
				{		
					summary.Append(road.RoadName.ToString());
					summary.Append(" ");
				}

				summary.Append("\r\n");
			}

			//Roads to avoid
			if(avoidRoadsList.Length >0)
			{
				summary.Append("Avoiding roads: ");

				foreach (TDRoad road in avoidRoadsList)
				{		
					summary.Append(road.RoadName.ToString());
					summary.Append(" ");
				}

				summary.Append("\r\n");
			}

			//Via Locations
			if(PrivateViaLocation.Description.Length > 0)
			{
				summary.Append("Car via: ");
				summary.Append(privateViaLocation.Description);
				summary.Append("\r\n");

            }

            #endregion

            #region Cycle specific

            if (cycleViaLocation.Description.Length > 0)
            {
                summary.Append("Cycle via: ");
                summary.Append(cycleViaLocation.Description);
                summary.Append("\r\n");
            }

            if (!cycleSpeedIsDefault)
            {
                summary.Append("Cycle speed: ");
                summary.Append(cycleSpeedMax);
                summary.Append("metres per hour");
                summary.Append("\r\n");
            }

            if (!cycleJourneyType.Equals(populator.GetDefaultListControlValue(DataServiceType.CycleJourneyType)))
            {
                summary.Append("Cycle journey type: ");
                summary.Append(cycleJourneyType);
                summary.Append("\r\n");
            }

            if (cycleAvoidSteepClimbs)
            {
                summary.Append("Avoid steep climbs");
                summary.Append("\r\n");
            }

            if (cycleAvoidUnlitRoads)
            {
                summary.Append("Avoid unlit roads");
                summary.Append("\r\n");
            }

            if (cycleAvoidWalkingYourBike)
            {
                summary.Append("Avoid walking your bike");
                summary.Append("\r\n");
            }

            if (cycleAvoidTimeBased)
            {
                summary.Append("Avoid time based restrictions");
                summary.Append("\r\n");
            }

            #endregion

            return summary.ToString();
        }


        #region Public methods - Initialise
        /// <summary>
		/// Initialisation for outward or return extend journey information only
		/// </summary>
		/// <param name="originReset">Whether to initialise origin or destination information</param>
		public override void Initialise(bool originReset)
		{
			base.Initialise(originReset);

			InitialiseGeneric();
		}


		/// <summary>
		/// Initialisation common to all other initialisation methods
		/// </summary>
		private void InitialiseGeneric()
		{
			// Default is from the data services
			DataServices.IDataServices populator = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			
			if (TDSessionManager.Current.FindAMode == FindAMode.Bus)
			{
				publicModes = new ModeType[]
				{
					ModeType.Bus,
					ModeType.Tram,
					ModeType.Ferry,
					ModeType.Walk
				};
			}
			else 
			{
			
				publicModes = new ModeType[]
				{
					ModeType.Air,
					ModeType.Bus,
					ModeType.Car,
					ModeType.Coach,
					ModeType.Ferry,
					ModeType.Metro,
					ModeType.Rail,
                    ModeType.Telecabine,
					ModeType.Tram,
					ModeType.Underground,
					ModeType.Walk
				};
			}

			drivingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop), CultureInfo.CurrentCulture);
			carSize = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
			carFuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
			// Fuel consumption option and cost option are always set to true, because default setting is always used primarily
			fuelConsumptionOption = true; 
			fuelCostOption = true;
			fuelConsumptionValid = true;
			fuelCostValid = true;
			fuelUseUnitChange = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.UnitsDrop), CultureInfo.CurrentCulture);
			avoidMotorWays = false;
			avoidRoads = string.Empty;
			avoidTolls = false;
			avoidFerries = false;
			doNotUseMotorways = false;
			vehicleType = VehicleType.Car;


			// LocationSearch set to empty locationSearch objects
			publicVia = new LocationSearch();
			publicVia.SearchType = GetDefaultSearchType(DataServiceType.PTViaDrop);
			privateVia = new LocationSearch();
			privateVia.SearchType = GetDefaultSearchType(DataServiceType.CarViaDrop);
			alternates = new LocationSearch[] { new LocationSearch(), new LocationSearch() };
			alternates[0].SearchType = GetDefaultSearchType(DataServiceType.AltFromToDrop);
			alternates[1].SearchType = GetDefaultSearchType(DataServiceType.AltFromToDrop);

			PublicViaType = new LocationSelectControlType(ControlType.NoMatch);
			PrivateViaType = new LocationSelectControlType(ControlType.NoMatch);

			// TDLocations set to empty unspecified locations
			publicViaLocation = new TDLocation();
			privateViaLocation = new TDLocation();
			alternateLocations = new TDLocation[] { new TDLocation(), new TDLocation() };

			privateRequired = true;
			publicRequired = true;

			privateAlgorithmType = PrivateAlgorithmType.Fastest;

			outwardStopoverDays = 0;
			outwardStopoverHours = 0;
			outwardStopoverMinutes = 0;
			returnStopoverDays = 0;
			returnStopoverHours = 0;
			returnStopoverMinutes = 0;

			// empty hour and minutes if in FindAMode. Don't do it for car as AnyTime is not allowed!
			if	(TDSessionManager.Current.IsFindAMode && 
				(TDSessionManager.Current.FindAMode != FindAMode.Car) 
                && (TDSessionManager.Current.FindAMode != FindAMode.Cycle)
                && (TDSessionManager.Current.FindAMode != FindAMode.Bus)
                && (TDSessionManager.Current.FindAMode != FindAMode.Train)
                && (TDSessionManager.Current.FindAMode != FindAMode.Trunk)
                && (TDSessionManager.Current.FindAMode != FindAMode.Coach)
                && (TDSessionManager.Current.FindAMode != FindAMode.Flight))
			{
				OutwardAnyTime = true;
				ReturnAnyTime = false;
				OutwardHour = string.Empty;
				OutwardMinute = string.Empty;
			}

			// If these are changed in size, remember to change them in favourite journeys as well and
			// ensure that it is backwards compatible.
			useRoadsList = new TDRoad[0]; 
			avoidRoadsList = new TDRoad[0];

            // Initialise outward and return list of toids to avoid
            avoidToidsListOutward = new string[0];
            avoidToidsListReturn = new string[0];

            // Cycle planner values
            cycleRequired = false;
            cycleVia = new LocationSearch();
            cycleVia.SearchType = GetDefaultSearchType(DataServiceType.PTViaDrop);
            cycleViaLocation = new TDLocation();

            typeCycleVia = new LocationSelectControlType(ControlType.NoMatch);

            cycleAvoidSteepClimbs = false;
            cycleAvoidUnlitRoads = false;
            cycleAvoidWalkingYourBike = false;
            cycleAvoidTimeBased = false;
            cycleJourneyType = populator.GetDefaultListControlValue(DataServiceType.CycleJourneyType);
            cycleSpeedMax = "19312";
            cycleSpeedText = string.Empty;
            cycleSpeedUnit = populator.GetDefaultListControlValue(DataServiceType.UnitsSpeedDrop);
            cycleSpeedValid = true;
            cycleSpeedIsDefault = true;
            cyclePenaltyFunction = string.Empty;
            cyclePenaltyFunctionOverride = string.Empty;
            cycleLocationOriginOverride = new OSGridReference(0, 0);
            cycleLocationDestinationOverride = new OSGridReference(0, 0);

            // Accessible journey preferences
            doNotUseUnderground = false;
            requireSpecialAssistance = false;
            requireStepFreeAccess = false;
            requireFewerInterchanges = false;
            accessibleWalkDistance = 0;
            accessibleWalkSpeed = 0;
        }

        #endregion

        #region Public Methods - Reset Toid List to avoid
        /// <summary>
        /// Resets the list of outward/return toids to avoid when planning the outward or return journey respectively
        /// </summary>
        /// <param name="outward"></param>
        public void ResetToidListToAvoid(bool outward)
        {
            if (outward)
            {
                avoidToidsListOutward = new string[0];
            }
            else
            {
                avoidToidsListReturn = new string[0];
            }
        }
        #endregion
    }
}
