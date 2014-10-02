// ***********************************************
// NAME 		: ProfileKeys.cs
// AUTHOR 		: C.M. Owczarek 
// DATE CREATED : 02.08.04
// DESCRIPTION 	: Defines key values used for saving and retrieving 
// user profile data held by a TDProperties object
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ProfileKeys.cs-arc  $
//
//   Rev 1.5   Jan 30 2013 10:35:28   mmodi
//Updated to save Telecabine in favourite journey
//Resolution for 5884: CCN:677 - Telecabine modetype
//
//   Rev 1.4   Nov 16 2012 14:00:48   DLane
//Setting walk parameters in the journey request
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Nov 15 2012 14:06:20   DLane
//Addition of accessibility options to journey plan input and user profile
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Oct 29 2010 09:08:06   apatel
//updated to enable logged in user to have feature of extended session time out
//Resolution for 5625: Users not able to extend their session timeout
//
//   Rev 1.1   Oct 13 2008 16:46:38   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.1   Oct 10 2008 15:49:14   mmodi
//Updated to have avoid time based check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0.1.0   Sep 02 2008 11:40:10   mmodi
//Added cycle journey keys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:48:34   mturner
//Initial revision.
//
//   Rev 1.3   Apr 18 2005 11:45:26   tmollart
//Added profile keys for saving of Find Fare user preferences.
//Resolution for 2159: Find a fare save preferences check-box
//
//   Rev 1.2   Mar 08 2005 09:33:58   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.1   Feb 24 2005 11:37:30   PNorell
//Updated for favourite details.
//
//   Rev 1.0   Aug 02 2004 13:29:24   COwczarek
//Initial revision.
//Resolution for 1202: Implement FindTrainInput page
//

namespace TransportDirect.UserPortal.SessionManager
{	
	/// <summary>
    /// Defines key values used for saving and retrieving 
    /// user profile data held by a TDProperties object
	/// </summary>
    public class ProfileKeys
    {
        /// <summary>
        /// String pattern for extracting locality from the profile store.
        /// </summary>
        public const string LOCALITY = "GeneralInfo.{0}Locality";
        /// <summary>
        /// String pattern for extracting easting from the profile store.
        /// </summary>
        public const string EASTING = "GeneralInfo.{0}Easting";
        /// <summary>
        /// String pattern for extracting northing from the profile store.
        /// </summary>
        public const string NORTHING = "GeneralInfo.{0}Northing";
        /// <summary>
        /// String pattern for extracting naptans from the profile store.
        /// </summary>
        public const string NAPTAN = "GeneralInfo.{0}Naptan";
        /// <summary>
        /// String pattern for extracting naptan eastings from the profile store.
        /// </summary>
        public const string NAPTAN_EASTING = "GeneralInfo.{0}NaptanEasting";
        /// <summary>
        /// String pattern for extracting naptan northings from the profile store.
        /// </summary>
        public const string NAPTAN_NORTHING = "GeneralInfo.{0}NaptanNorthing";

        public const string ORIGIN_LOCATION_NAME = "GeneralInfo.OriginLocationName";
        public const string DESTINATION_LOCATION_NAME = "GeneralInfo.DestinationLocationName";
        public const string DESTINATION_TYPE = "GeneralInfo.DestinationType";
        public const string ORIGIN_LOCATION_TYPE = "GeneralInfo.OriginLocationType";
        public const string JOURNEY_NAME = "GeneralInfo.JourneyName";
        public const string RETURN_JOURNEY = "GeneralInfo.ReturnJourney";
        public const string FIND_CAR = "GeneralInfo.FindCar";
        public const string FIND_PT = "GeneralInfo.FindPT";
        public const string FIND_CYCLE = "GeneralInfo.FindCycle";
        public const string MODE_PLANE = "GeneralInfo.ModePlane";
        public const string MODE_BUS_COACH = "GeneralInfo.ModeBusCoach";	
        public const string MODE_FERRY = "GeneralInfo.ModeFerry";
        public const string MODE_TRAIN = "GeneralInfo.ModeTrain";
        public const string MODE_TELECABINE = "GeneralInfo.ModeTelecabine";	
        public const string MODE_TRAM_LR = "GeneralInfo.ModeTramLR";	
        public const string MODE_UG_METRO = "GeneralInfo.ModeUGMetro";
        public const string MODE_CYCLE = "GeneralInfo.ModeCycle";
        public const string AVOID_ROAD = "GeneralInfo.AvoidRoad";
		public const string USE_ROAD = "GeneralInfo.UseRoad";
        public const string VIA_LOCATION_CAR = "GeneralInfo.ViaLocationCar";
        public const string VIA_LOCATION_TYPE_CAR = "GeneralInfo.ViaLocationTypeCar";
        public const string VIA_LOCATION_PT = "GeneralInfo.ViaLocationPT";
        public const string VIA_LOCATION_TYPE_PT = "GeneralInfo.ViaLocationTypePT";
        public const string VIA_LOCATION_CYCLE = "GeneralInfo.ViaLocationCycle";
        public const string VIA_LOCATION_TYPE_CYCLE = "GeneralInfo.ViaLocationTypeCycle";

        public const string DISCOUNT_CARD_RAIL = "GeneralInfo.DiscountCardRail";
        public const string DISCOUNT_CARD_COACH = "GeneralInfo.DiscountCardCoach";
        public const string ADULT_CHILD = "GeneralInfo.Adultchild";
		public const string TRAVEL_CLASS = "GeneralInfo.TravelClass";

		// Preferences
        public const string PREFERENCES = "GeneralInfo.Preferences";
		public const string CAR_PREFERENCES_SET = "GeneralInfo.CarPreferencesSet";
		public const string PT_PREFERENCES_SET = "GeneralInfo.PtPreferencesSet";
        public const string CYCLE_PREFERENCES_SET = "GeneralInfo.CyclePreferencesSet";
        public const string ACCESSIBILITY_PREFERENCES_SET = "GeneralInfo.AccessibilityPreferencesSet";
        public const string WALKING_SPEED = "GeneralInfo.WalkingSpeed";
        public const string WALKING_TIME = "GeneralInfo.WalkingTime";
        public const string USER_SECURITY_PASSWORD = "GeneralInfo.user_security_password";
        public const string INTERCHANGE_SPEED = "GeneralInfo.InterchangeSpeed";
        public const string INTERCHANGE_LIMIT = "GeneralInfo.InterchangeLimit";
        public const string DRIVING_SPEED = "GeneralInfo.DrivingSpeed";
        public const string DRIVING_TYPE = "GeneralInfo.DrivingType";
		public const string DONOTUSE_MOTORWAYS = "GeneralInfo.DoNotUseMotorways";
		public const string CAR_ALGORITHM = "GeneralInfo.CarAlgorithm";
		public const string CAR_SIZE = "GeneralInfo.CarSize";
		public const string CAR_FUELTYPE = "GeneralInfo.CarFuelType";
		public const string CAR_DEFAULT_FUEL_CONSUMPTION = "GeneralInfo.CarDefaultFuelConsumption";
		public const string CAR_FUEL_CONSUMPTION = "GeneralInfo.CarFuelConsumption";
		public const string CAR_FUEL_CONSUMPTION_TYPE = "GeneralInfo.CarFuelConsumptionType";
		public const string CAR_FUEL_COST = "GeneralInfo.CarFuelCost";
		public const string CAR_DEFAULT_FUEL_COST = "GeneralInfo.CarDefaultFuelCost";
        public const string ACCESSIBILITY_REQUIRE_SPECIAL_ASSISTANCE = "GenerailInfo.AccessiblityRequireSpecialAssistance";
        public const string ACCESSIBILITY_REQUIRE_STEP_FREE_ACCESS = "GenerailInfo.AccessiblityRequireStepFreeAccess";
        public const string ACCESSIBILITY_REQUIRE_FEWER_CHANGES = "GenerailInfo.AccessiblityRequireFewerChanges";
        public const string ACCESSIBILITY_WALK_SPEED = "GenerailInfo.AccessiblityWalkSpeed";
        public const string ACCESSIBILITY_WALK_DISTANCE = "GenerailInfo.AccessiblityWalkDistance";
        public const string CYCLE_SPEED_TEXT = "GeneralInfo.CycleSpeedText";
        public const string CYCLE_SPEED_UNIT = "GeneralInfo.CycleSpeedUnit";
        public const string CYCLE_JOURNEY_TYPE = "GeneralInfo.CycleJourneyType";
        public const string CYCLE_AVOID_UNLITROADS = "GeneralInfo.CycleAvoidUnlitRoads";
        public const string CYCLE_AVOID_WALKINGBIKE = "GeneralInfo.CycleAvoidWalkingBike";
        public const string CYCLE_AVOID_STEEPCLIMBS = "GeneralInfo.CycleAvoidSteepClimbs";
        public const string CYCLE_AVOID_TIMEBASED = "GeneralInfo.CycleAvoidTimeBased";

        public const string AVOID_MOTORWAY = "GeneralInfo.AvoidMotorway";
		public const string AVOID_FERRIES = "GeneralInfo.AvoidFerries";
		public const string AVOID_TOLLS = "GeneralInfo.AvoidTolls";

        public const string TRAVEL_NEWS_AREA = "GeneralInfo.TravelNewsArea";  
        public const string TRAVEL_NEWS_MODE = "GeneralInfo.TravelNewsMode"; 
        public const string TRAVEL_NEWS_SEVERITY = "GeneralInfo.TravelNewsSeverity";  
        public const string TRAVEL_NEWS_LEVEL = "GeneralInfo.TravelNewsLevel"; 

        public const string FAVOURITE = "GeneralInfo.Favourite{0}";

        /// <summary>
        /// The logon name key
        /// </summary>
        public const string LOGON = "GeneralInfo.logon_name";

        /// <summary>
        /// The last login key
        /// </summary>
        public const string LASTLOGIN = "GeneralInfo.d_date_last_login";

        /// <summary>
        /// The user id key
        /// </summary>
        public const string USERID = "GeneralInfo.user_id";

        /// <summary>
        /// The type of user
        /// </summary>
        public const string USERTYPE = "GeneralInfo.user_type";


        public const string EXTENDED_SESSION = "GeneralInfo.user_extended_session";

    }		



}


