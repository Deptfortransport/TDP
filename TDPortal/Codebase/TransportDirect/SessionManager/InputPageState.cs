// *********************************************** 
// NAME                 : InputStatePate.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 17/09/2003 
// DESCRIPTION  : Holds variables indicating state of page, return stack for navigation etc
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/InputPageState.cs-arc  $ 
//
//   Rev 1.17   Jan 04 2013 15:34:00   mmodi
//Store accessible locations found for accessible journey 
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.16   Oct 12 2011 12:02:22   mmodi
//Updated logic for Traffic Maps page to set and persist the selected date and times
//Resolution for 5753: Traffic Levels map does not use selected date
//
//   Rev 1.15   Mar 26 2010 12:00:26   RHopkins
//Reduce number of calls made to CJP when showing Departure Boards and Stop Events
//Resolution for 5450: Stop Information pages make excessive calls to CJP
//
//   Rev 1.14   Feb 25 2010 16:20:54   pghumra
//Code fix applied to resolve issue with date not being displayed on journey details section in the door to door planner when date of travel is different to requested date
//Resolution for 5413: CODE FIX - NEW - DEL 10.x - Issue with seasonal information change from Del 10.8
//
//   Rev 1.13   Feb 25 2010 10:37:14   mmodi
//Added Accessability modetypes to be shown
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.12   Feb 17 2010 16:42:32   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.11   Feb 09 2010 13:06:10   apatel
//updated for cycle printer friendly map
//Resolution for 5399: Cycle Planner Printer Friendly page broken
//
//   Rev 1.10   Dec 08 2009 11:30:38   mmodi
//Flag to set visibility of Expand map button on Stop Information page
//
//   Rev 1.9   Dec 04 2009 14:59:30   apatel
//Departure board changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.8   Nov 22 2009 15:57:58   pghumra
//Stop Information departure board changes
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.7   Nov 16 2009 17:06:58   apatel
//Updated for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Oct 27 2009 09:53:20   apatel
//Stop Information Departure board control changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.5   Oct 15 2009 14:48:58   apatel
//Stop Information Departure Board Service code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.4   Sep 14 2009 10:31:44   apatel
//Stop Information page related changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Oct 14 2008 15:23:06   mmodi
//Manual merge for stream5014
//
//   Rev 1.2   Jul 15 2008 16:40:26   mmodi
//Updated to hold the query string
//Resolution for 5065: Log in issues - Find a map, and Help pages
//
//   Rev 1.1.1.1   Sep 05 2008 15:14:22   mmodi
//Updated from trunk to enable new style tabs to be displayed in cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1.1.0   Jun 20 2008 14:48:56   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   May 01 2008 17:25:44   mmodi
//Updated to hold session timeout errors
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.0   Nov 08 2007 12:48:28   mturner
//Initial revision.
//
//   Rev 1.35   Jan 12 2007 14:09:08   mmodi
//Added state for Feedback page
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.34   Oct 06 2006 13:33:10   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.33.1.1   Aug 04 2006 13:49:22   esevern
//added CarParkReference property
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.33.1.0   Jul 31 2006 14:28:36   MModi
//Updated for new Car Park symbol on map icons key
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.33   Jun 15 2006 10:21:10   mmodi
//IR4117 Added property to store the current page language
//Resolution for 4117: Maps - Navigation error when selecting Welsh on the map results page
//
//   Rev 1.32   Jun 01 2006 08:48:06   mmodi
//IR4105: Added property to store coordinates where user clicked on the map
//Resolution for 4105: Del 8.2 - Select new location map feature dropdown values are lost
//
//   Rev 1.31   Apr 29 2006 19:40:04   RPhilpott
//Store retailer list to avoid affects of randomisation.
//Resolution for 4036: DD075: Mismatch of retailer selected in Find Cheaper
//
//   Rev 1.30   Apr 29 2006 11:48:08   asinclair
//Added Amend mode bool - set when user returns from results to input page
//Resolution for 3984: DN068: Previously resolved extension locations are treated as ambiguous on amend
//
//   Rev 1.29   Apr 04 2006 11:39:40   build
//Automatically merged for stream 0034
//
//   Rev 1.28.1.0   Mar 29 2006 17:31:16   RWilby
//Updated  ResetIconSelectionOutward and ResetIconSelectionReturn methods for changes for the new map symbols.
//Resolution for 3715: Map Symbols
//
//   Rev 1.28   Mar 13 2006 14:37:00   NMoorhouse
//Manual merge of stream3353 -> trunk
//
//   Rev 1.27   Feb 17 2006 11:57:08   halkatib
//Added fixes for IR3573
//
//   Rev 1.26   Jan 24 2006 18:10:30   asinclair
//Added visitAmendMode = false; to Initialise()
//Resolution for 3498: Visit Planner: in certain cases, amend journey clears out journey details
//
//   Rev 1.25.1.0   Jan 19 2006 11:22:48   NMoorhouse
//New Page State properties for Journey Replan Input Page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.25   Nov 17 2005 10:02:26   asinclair
//Added bool VisitAmendMode
//Resolution for 2954: Visit Planner (CG): Resolved locations wrongly need to be re-resolvedx after amend journey
//Resolution for 3071: Visit Planner - Manchester (Any Rail / Coach) not found by journey input scren
//
//   Rev 1.24   Nov 01 2005 15:12:10   build
//Automatically merged from branch for stream2638
//
//   Rev 1.23.1.3   Oct 26 2005 18:43:28   asinclair
//Updated CurrentMapMode
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.23.1.2   Oct 07 2005 16:15:40   asinclair
//Fixed AmbiguityMode lower case error
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.23.1.1   Oct 07 2005 15:54:10   asinclair
//Added AdvancedOptionsVisible and AmbiguityMode properties
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.23.1.0   Sep 16 2005 16:15:02   tolomolaiye
//Check in for review
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.23   Mar 08 2005 09:33:48   PNorell
//new functionality for saving preferences.
//Resolution for 1958: DEV CODE Review: Preferences (Car and Public)
//
//   Rev 1.22   Mar 01 2005 09:46:40   asinclair
//Added the RoadUnitsEnum for Del 7 Car Costing
//
//   Rev 1.21   Nov 04 2004 13:00:30   passuied
//Added ability to clear Iconselection (out and ret) in Input page State when scale is too low
//Resolution for 1732: POI keys appear on printable map page although the scale is too low.
//
//   Rev 1.20   Aug 19 2004 15:39:40   passuied
//Added new type in MapMode called FromFindAInput, used by FindTrunkInput and FindCarInput when calling the map. The map page and controls behave exactly as with enum MapMode.FromJourneyInput except that it shows the FindA header in the first case and the JourneyPlanner one in the latter. 
//Also checks if MapMode.FromFindAInput and if not wipe the FindAMode (CreateInstance(FindAMode.None)).
//Resolution for 1361: Maps no longer displays all gazetteer options
//Resolution for 1390: JourneyLocationMap. Wrong header displayed in FindA mode
//
//   Rev 1.19   Mar 31 2004 09:31:30   CHosegood
//added PreviousOutwardJourney and PreviousReturnJourney properties
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.18   Mar 26 2004 10:00:32   COwczarek
//Remove redundant RouteOptionsChangedProperty
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.17   Mar 19 2004 16:28:06   COwczarek
//Remove redundant code
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.16   Feb 18 2004 10:01:14   esevern
//DEL5.2 added transport types visibility property
//
//   Rev 1.15   Jan 20 2004 11:04:34   PNorell
//Updated map according to 5.2.
//
//   Rev 1.14   Dec 15 2003 12:42:18   kcheung
//Added new property to indicate if force reload of journey planner input page or not.  Del 5.1 Update.
//
//   Rev 1.13   Dec 13 2003 14:06:24   passuied
//added properties for controltype
//
//   Rev 1.12   Dec 10 2003 10:59:24   JHaydock
//Adjustment so that Ambiguity page only displays sections that have changed
//
//   Rev 1.11   Nov 21 2003 09:38:56   alole
//Updated the CalendarDateTime porperty to be settable.
//
//   Rev 1.10   Nov 04 2003 15:11:00   passuied
//Added flags to indicate if traveldetails have changed and if route options have changed
//
//   Rev 1.9   Oct 02 2003 15:57:32   passuied
//added properties for printable map
//
//   Rev 1.8   Oct 02 2003 11:30:58   passuied
//added IconSelection properties
//
//   Rev 1.7   Sep 30 2003 10:38:46   passuied
//added properties for printable map page
//
//   Rev 1.6   Sep 25 2003 09:32:26   passuied
//added ambiguity index for the map
//
//   Rev 1.5   Sep 22 2003 12:14:06   kcheung
//Added 'FindJourneys' map mode for input map page
//
//   Rev 1.4   Sep 19 2003 10:10:06   passuied
//corrected name of properties
//
//   Rev 1.3   Sep 19 2003 10:08:48   passuied
//added Properties for Map control
//
//   Rev 1.2   Sep 18 2003 14:48:46   kcheung
//Marked as serializable as it was causing errors during load
//
//   Rev 1.1   Sep 18 2003 12:05:38   passuied
//changed to follow design + initialisation
//
//   Rev 1.0   Sep 17 2003 18:23:52   passuied
//Initial Revision


using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.SessionManager
{
    #region Enums
    /// <summary>
	/// Holds variables indicating state of page, return stack for navigation etc
	/// </summary>
	/// 
	public enum CurrentLocationType
	{
		None,
		From,
		To,
		PublicVia,
		PrivateVia,
        CycleVia,
		Alternate1,
		Alternate2,
		VisitPlannerOrigin,			//used for the VisitPlannerLocationControl
		VisitPlannerVisitPlace1,	//used for the VisitPlannerLocationControl
		VisitPlannerVisitPlace2		//used for the VisitPlannerLocationControl
	}

	public enum CurrentMapMode
	{
		FromJourneyInput,
		TravelFrom,
		TravelTo,
		TravelBoth,
		FindJourneys,
		FromFindAInput,
		VisitPlannerLocationOrigin,
		VisitPlannerLocation1,
		VisitPlannerLocation2
	}

	public enum TravelOptionsChangedEnum
	{
		PublicTransport,
		InterchangesMode,
		InterchangesSpeed,
		WalkSpeed,
		WalkTime,
		CarJourneyType,
		CarSpeed,
		CarAvoidMotorway
	}

	public enum RouteOptionsChangedEnum
	{
		CarAvoidRoad,
		CarViaLocation,
		PublicTransportViaLocation
	}

	public enum RoadUnitsEnum
	{
		Miles,
		Kms
	}

	public enum FeedbackState
	{
		Initial,
		Suggestion,
		Problem,
		SubmittedSuccess,
		SubmittedFail
	}

    public enum InputSessionError
    {
        TimeoutSorry,
        TimeoutExpires
    }
    #endregion

    [CLSCompliant(false)] 
	[Serializable()]
	public class InputPageState
    {
        #region Private variables
        // General
		private Stack stackJourneyInputReturnStack = null;
		private string stringAdditionalDataLocation = string.Empty;
		private string stringAdditionalDataDescription = string.Empty;
		private string pageLanguage = String.Empty;

        // Used to retain the query string when switching pages. 
        // Specifically used when going to Login/Register from an input page Help page
        private string stringJourneyInputQueryString = string.Empty;

		// Mapping related
		private CurrentLocationType enumMapType;
		private CurrentMapMode	enumMapMode = CurrentMapMode.TravelBoth;
		private LocationSearch searchMapLocationSearch;
		private TDLocation locationMapLocation;
		private TDJourneyParameters.LocationSelectControlType typeMapLocationControl;

		private double doubleOriginalX= 0;
		private double doubleOriginalY=0;
		private int intOriginalScale = 0;
		
		private int intMapClickPointX = 0;
		private int intMapClickPointY = 0;

		private string carParkRef = string.Empty;

		// JourneyPlanner Input page
		private TDDateTime dateCalendarDateTime = null;
        private TDDateTime dateCalendarDateTimePrintable = null;
		private bool boolCalendarIsForOutward = true;
		private bool carOptionsVisible = false;
		private bool publicTransportOptionsVisible = false;
		private bool transportTypesVisible = false;
		private ArrayList arrayTravelOptionsChanged = new ArrayList();
        

		//VisitPlannerInput page
		private bool advancedOptionsVisible = false;
		private bool ambiguityMode = false;

		private bool visitAmendMode = false;

		private bool amendMode;

		// Printable map related
		private bool[][] boolIconSelectionOutward;
		private bool[][] boolIconSelectionReturn;
		private string stringMapUrlOutward = string.Empty;
		private string stringOverviewMapUrlOutward = string.Empty;
		private int intMapScaleOutward = 0;
		private string stringMapViewTypeOutward = string.Empty;
		private string stringMapUrlReturn = string.Empty;
		private string stringOverviewMapUrlReturn = string.Empty;
		private int intMapScaleReturn = 0;
		private string stringMapViewTypeReturn = string.Empty;
		private int refineStartJourneyDetailIndex = -1;
		private int refineEndJourneyDetailIndex = -1;
        private string cycleMapTilesOutward;
        private string cycleMapTilesReturn;
        int mapTileScaleOutward = 0;
        int mapTileScaleReturn = 0;


		private RoadUnitsEnum units;

		// for TicketRetailer handoff only 
		private Retailer[] outwardOnlineRetailers;
		private Retailer[] outwardOfflineRetailers;
		private Retailer[] returnOnlineRetailers;
		private Retailer[] returnOfflineRetailers;

		// for Feedback page only
		private FeedbackState feedbackState;

        // for StopInformation page only
        private string stopCode = string.Empty;
        private TDCodeType stopCodeType = TDCodeType.NAPTAN;
        private bool showStopInformationFacilityControl= true;
        private bool showStopInformationLocalityControl = true;
        private bool showStopInformationOperatorControl = true;
        private bool showStopInformationRealTimeControl = true;
        private bool showStopInformationTaxiControl = true;
        private bool showStopInformationPlanJourneyControl = false;
        private bool showStopInformationDepartureBoardControl = true;
        private bool showStopInformationMapControl = true;
        private bool showStopInformationMapControlExpandButton = true;
        private string stopServiceDetailsStopCode = string.Empty;
        private string stopServiceDetailsOperatorCode = string.Empty;
        private string stopServiceDetailsServiceNumber = string.Empty;
        private TDStopType stopServiceDetailsStopType = TDStopType.Unknown;
        private string stopInfromationTDOnMoveUrl = string.Empty;
        private bool departureBoardShowDeparture = true;
        private TDCodeType stopServiceDetailsCodeType = TDCodeType.CRS;
        private bool stopInformationDepartArriveChecked = false;
        private bool stopInformationDepartExists = false;
        private bool stopInformationArriveExists = false;

        // for any errors to be displayed on Input pages
        private InputSessionError[] inputSessionErrors;

        // for Accessability page
        private ModeType[] accessabilityModeTypes = new ModeType[0];
        private TDLocationAccessible[] accessibleLocationsOrigin = null;
        private TDLocationAccessible[] accessibleLocationsDestination = null;
        private TDLocationAccessible[] accessibleLocationsPublicVia = null;

        private TDDateTime originalOutwardDateTime = null;
        private TDDateTime originalReturnDateTime = null;

        #endregion

        public void Initialise()
		{
			carOptionsVisible = false;
			publicTransportOptionsVisible = false;
			transportTypesVisible = false;
			arrayTravelOptionsChanged.Clear();
			visitAmendMode = false;
			feedbackState = FeedbackState.Initial;
            stringJourneyInputQueryString = string.Empty;
            accessabilityModeTypes = new ModeType[0];
            accessibleLocationsOrigin = null;
            accessibleLocationsDestination = null;
            accessibleLocationsPublicVia = null;
		}

		public InputPageState()
		{
			stackJourneyInputReturnStack = new Stack();
			searchMapLocationSearch = new LocationSearch();
			locationMapLocation = new TDLocation();
		
			typeMapLocationControl = new TDJourneyParameters.LocationSelectControlType( TDJourneyParameters.ControlType.NoMatch);

			ResetIconSelectionOutward();
			ResetIconSelectionReturn();

		}

        /// <summary>
        /// Read/write for the Input page session errors
        /// </summary>
        public InputSessionError[] InputSessionErrors
        {
            get
            {
                return inputSessionErrors;
            }
            set
            {
                inputSessionErrors = value;
            }
        }

		/// <summary>
		/// Read/Write for the car park reference number
		/// </summary>
		public string CarParkReference
		{
			get
			{
				return carParkRef;
			}
			set
			{
				carParkRef = value;
			}
		}


		/// <summary>
		/// Get/Set. JourneyInput Return stack
		/// </summary>
		public Stack JourneyInputReturnStack
		{
			get
			{
				return stackJourneyInputReturnStack;
			}
			set
			{
				stackJourneyInputReturnStack = value;
			}
		}

		/// <summary>
		/// Get/Set. Additional Data Location
		/// </summary>
		public string AdditionalDataLocation
		{
			get
			{
				return stringAdditionalDataLocation;
			}
			set
			{
				stringAdditionalDataLocation = value;
			}
		}

		/// <summary>
		/// Get/Set. Additional Data Description
		/// </summary>
		public string AdditionalDataDescription
		{
			get
			{
				return stringAdditionalDataDescription;
			}
			set
			{
				stringAdditionalDataDescription = value;
			}
		}

        /// <summary>
		/// Get/Set. Query string value which needs to be retained 
        /// when navigating to and then back from a page. e.g. Login/register
		/// </summary>
        public string JourneyInputQueryString
		{
			get
			{
                return stringJourneyInputQueryString;
			}
			set
			{
                stringJourneyInputQueryString = value;
			}
		}
        
		/// <summary>
		/// Get/Set. Page Language
		/// </summary>
		public string PageLanguage
		{
			get
			{
				return pageLanguage;
			}
			set
			{
				pageLanguage = value;
			}
		}

		/// <summary>
		/// Get/Set. Current Location Type
		/// </summary>
		public CurrentLocationType MapType
		{
			get
			{
				return enumMapType;
			}
			set
			{
				enumMapType = value;
			}
		}

		/// <summary>
		/// Get/Set. Current Map Mode
		/// </summary>
		public CurrentMapMode MapMode
		{
			get
			{
				return enumMapMode;
			}
			set
			{
				enumMapMode = value;
			}
		}

		/// <summary>
		/// Get/Set. Map LocationSearch
		/// </summary>
		public LocationSearch MapLocationSearch
		{
			get
			{
				return searchMapLocationSearch;
			}
			set
			{
				searchMapLocationSearch = value;
			}
		}

		/// <summary>
		/// Get/Set. Map Location
		/// </summary>
		public TDLocation MapLocation
		{
			get
			{
				return locationMapLocation;
			}
			set
			{
				locationMapLocation = value;
			}
		}

		/// <summary>
		/// Get/Set. Current Location Type
		/// </summary>
		public  RoadUnitsEnum Units 
		{
			get
			{
				return units;
			}
			set
			{
				units = value;
			}
		}

		/// <summary>
		///  Get/Set property. Control type for map location control
		/// </summary>
		public TDJourneyParameters.LocationSelectControlType MapLocationControlType
		{
			get
			{
				return typeMapLocationControl;
			}
			set
			{
				typeMapLocationControl = value;
			}

		}

		/// <summary>
		/// Get/Set. Original X
		/// </summary>
		public double OriginalX
		{
			get
			{
				return doubleOriginalX;
			}
			set
			{
				doubleOriginalX = value;
			}
		}

		/// <summary>
		/// Get/Set. Original Y
		/// </summary>
		public double OriginalY
		{
			get
			{
				return doubleOriginalY;
			}
			set
			{
				doubleOriginalY = value;
			}
		}

		/// <summary>
		/// Get/Set. Original scale
		/// </summary>
		public int OriginalScale
		{
			get
			{
				return intOriginalScale;
			}
			set
			{
				intOriginalScale = value;
			}
		}

		/// <summary>
		/// Get/Set. MapClickPointX
		/// </summary>
		public int MapClickPointX
		{
			get
			{
				return intMapClickPointX;
			}
			set
			{
				intMapClickPointX = value;
			}
		}

		/// <summary>
		/// Get/Set. MapClickPointY
		/// </summary>
		public int MapClickPointY
		{
			get
			{
				return intMapClickPointY;
			}
			set
			{
				intMapClickPointY = value;
			}
		}

		/// <summary>
		/// Get/Set. Calendar DateTime
		/// </summary>
		public TDDateTime CalendarDateTime
		{
			get
			{
				return dateCalendarDateTime;
			}
			set
			{
				dateCalendarDateTime = value;
			}
		}

        /// <summary>
        /// Get/Set. Calendar DateTime for printable page
        /// </summary>
        public TDDateTime CalendarDateTimePrintable
        {
            get
            {
                return dateCalendarDateTimePrintable;
            }
            set
            {
                dateCalendarDateTimePrintable = value;
            }
        }

		/// <summary>
		/// Get/Set. Calendar is For Outward journey indicator
		/// </summary>
		public bool CalendarIsForOutward
		{
			get
			{
				return boolCalendarIsForOutward;
			}
			set
			{
				boolCalendarIsForOutward = value;
			}
		}

		/// <summary>
		/// get/set. AdvancedOptions panel visible?
		/// </summary>
		public bool CarOptionsVisible
		{
			get
			{
				return carOptionsVisible;
			}
			set
			{
				carOptionsVisible = value;
			}
		}

		/// <summary>
		/// Get/Set. TravelDetails panel Visible?
		/// </summary>
		public bool PublicTransportOptionsVisible
		{
			get
			{
				return publicTransportOptionsVisible;
			}
			set
			{
				publicTransportOptionsVisible = value;
			}
		}

		/// <summary>
		/// Get/Set. Public transport modes visible ?
		/// </summary>
		public bool PublicTransportTypesVisible
		{
			get
			{
				return transportTypesVisible;
			}
			set
			{
				transportTypesVisible = value;
			}
		}

		/// <summary>
		/// Get/Set. TravelDetails changed?
		/// </summary>
		public ArrayList TravelOptionsChanged
		{
			get
			{
				return arrayTravelOptionsChanged;
			}
			set
			{
				arrayTravelOptionsChanged = value;
			}
		}

		public bool[][] IconSelectionOutward
		{
			get
			{
				return boolIconSelectionOutward;
			}
			set
			{
				boolIconSelectionOutward = value;
			}
		}
		public bool[][] IconSelectionReturn
		{
			get
			{
				return boolIconSelectionReturn;
			}
			set
			{
				boolIconSelectionReturn = value;
			}
		}

		/// <summary>
		/// Reset icon selection for outward journey
		/// </summary>
		public void ResetIconSelectionOutward()
		{
			boolIconSelectionOutward = new bool[7][];
			boolIconSelectionOutward[0] = new bool[7];
			boolIconSelectionOutward[1] = new bool[4];
			boolIconSelectionOutward[2] = new bool[4];
			boolIconSelectionOutward[3] = new bool[4];
			boolIconSelectionOutward[4] = new bool[4];
			boolIconSelectionOutward[5] = new bool[4];
			boolIconSelectionOutward[6] = new bool[4];
			
		}

		/// <summary>
		/// Reset icon selection for return journey
		/// </summary>
		public void ResetIconSelectionReturn()
		{
			boolIconSelectionReturn = new bool[7][];
			boolIconSelectionReturn[0] = new bool[7];
			boolIconSelectionReturn[1] = new bool[4];
			boolIconSelectionReturn[2] = new bool[4];
			boolIconSelectionReturn[3] = new bool[4];
			boolIconSelectionReturn[4] = new bool[4];
			boolIconSelectionReturn[5] = new bool[4];
			boolIconSelectionReturn[6] = new bool[4];

		}
		
		/// <summary>
		/// Get/Set. Url of the map to display (Outward)
		/// </summary>
		public string MapUrlOutward
		{
			get
			{
				return stringMapUrlOutward;
			}
			set
			{
				stringMapUrlOutward = value;
			}
		}

		/// <summary>
		/// Get/Set. Url of the overview map to display (Outward)
		/// </summary>
		public string OverviewMapUrlOutward
		{
			get
			{
				return stringOverviewMapUrlOutward;
			}
			set
			{
				stringOverviewMapUrlOutward = value;
			}
		}

		/// <summary>
		/// Get/Set. map scale (Outward)
		/// </summary>
		public int MapScaleOutward
		{
			get
			{
				return intMapScaleOutward;
			}
			set
			{
				intMapScaleOutward = value;
			}
		}

		/// <summary>
		/// Get/Set. Map view type (Outward)
		/// </summary>
		public string MapViewTypeOutward
		{
			get
			{
				return stringMapViewTypeOutward;
			}
			set
			{
				stringMapViewTypeOutward = value;
			}
		}
		
		/// <summary>
		/// Get/Set. Url of the map to display (Return)
		/// </summary>
		public string MapUrlReturn
		{
			get
			{
				return stringMapUrlReturn;
			}
			set
			{
				stringMapUrlReturn = value;
			}
		}

		/// <summary>
		/// Get/Set. Url of the overview map to display (Return)
		/// </summary>
		public string OverviewMapUrlReturn
		{
			get
			{
				return stringOverviewMapUrlReturn;
			}
			set
			{
				stringOverviewMapUrlReturn = value;
			}
		}

        public string CycleMapTilesOutward
        {
            get
            {
                return cycleMapTilesOutward;
            }
            set
            {
                cycleMapTilesOutward = value;
            }
        }

        public string CycleMapTilesReturn
        {
            get
            {
                return cycleMapTilesReturn;
            }
            set
            {
                cycleMapTilesReturn = value;
            }
        }

        public int MapTileScaleOutward
        {
            get
            {
                return mapTileScaleOutward;
            }
            set
            {
                mapTileScaleOutward = value;
            }
        }

        public int MapTileScaleReturn
        {
            get
            {
                return mapTileScaleReturn;
            }
            set
            {
                mapTileScaleReturn = value;
            }
        }

		/// <summary>
		/// Get/Set. map scale (Return)
		/// </summary>
		public int MapScaleReturn
		{
			get
			{
				return intMapScaleReturn;
			}
			set
			{
				intMapScaleReturn = value;
			}
		}

		/// <summary>
		/// Get/Set. Map view type (Return)
		/// </summary>
		public string MapViewTypeReturn
		{
			get
			{
				return stringMapViewTypeReturn;
			}
			set
			{
				stringMapViewTypeReturn = value;
			}
		}

		private bool forceMapReload = false;

		public bool JourneyPlannerLocationMapForceLoad
		{
			get
			{
				return forceMapReload;
			}
			set
			{
				forceMapReload = value;
			}
		}

        private int previousOutwardJourney;
        /// <summary>
        /// Gets/sets the previous outward journey index
        /// This is used on the map output pages to determine if the congestion data dropdowns need
        /// to be repopulated
        /// </summary>
        public int PreviousOutwardJourney 
        {
            get
            {
                return previousOutwardJourney;
            }
            set
            {
                previousOutwardJourney  = value;
            }
        }

        private int previousReturnJourney;
        /// <summary>
        /// Gets/sets the previous return journey index
        /// This is used on the map output pages to determine if the congestion data dropdowns need
        /// to be repopulated
        /// </summary>
        public int PreviousReturnJourney 
        {
            get
            {
                return previousReturnJourney;
            }
            set
            {
                previousReturnJourney  = value;
            }
        }

		/// <summary>
		/// Read/Write property indicating if in Ambiguity mode
		/// </summary>
		public bool AmbiguityMode
		{
			get
			{
				return ambiguityMode;
			}
			set
			{
				ambiguityMode = value;
			}
		}

		/// <summary>
		/// Read/Write property indicating if in Amend mode.  Used when returning to input page from results
		/// </summary>
		public bool AmendMode
		{
			get
			{
				return amendMode;
			}
			set
			{
				amendMode = value;
			}
		}

		/// <summary>
		/// Read/Write property indicating if the advanced options are visible on input page
		/// </summary>
		public bool AdvancedOptionsVisible
		{
			get
			{
				return advancedOptionsVisible;
			}
			set
			{
				advancedOptionsVisible = value;
			}
		}

		
		/// <summary>
		/// Read/Write property indicating if in Amend mode
		/// </summary>
		public bool VisitAmendMode
		{
			get
			{
				return visitAmendMode;
			}
			set
			{
				visitAmendMode = value;
			}
		}

		/// <summary>
		/// Read/Write property indicating the start element index of journey to be replaned/amended
		/// </summary>
		public int RefineStartJourneyDetailIndex
		{
			get
			{
				return refineStartJourneyDetailIndex;
			}
			set
			{
				refineStartJourneyDetailIndex = value;
			}
		}

		/// <summary>
		/// Read/Write property indicating the start element index of journey to be replaned/amended
		/// </summary>
		public int RefineEndJourneyDetailIndex
		{
			get
			{
				return refineEndJourneyDetailIndex;
			}
			set
			{
				refineEndJourneyDetailIndex = value;
			}
		}

		/// <summary>
		/// Read/Write property for OfflineRetailers
		/// </summary>
		public Retailer[] OutwardOfflineRetailers
		{
			get { return outwardOfflineRetailers; }
			set { outwardOfflineRetailers = value; }
		}
		/// <summary>
		/// Read/Write property for OnlineRetailers
		/// </summary>
		public Retailer[] OutwardOnlineRetailers
		{
			get { return outwardOnlineRetailers; }
			set { outwardOnlineRetailers = value; }
		}
		/// <summary>
		/// Read/Write property for OfflineRetailers
		/// </summary>
		public Retailer[] ReturnOfflineRetailers
		{
			get { return returnOfflineRetailers; }
			set { returnOfflineRetailers = value; }
		}
		/// <summary>
		/// Read/Write property for OnlineRetailers
		/// </summary>
		public Retailer[] ReturnOnlineRetailers
		{
			get { return returnOnlineRetailers; }
			set { returnOnlineRetailers = value; }
		}

		/// <summary>
		/// Read/Write property for Feedback Page state
		/// </summary>
		public FeedbackState FeedbackPageState
		{
			get { return feedbackState; }
			set { feedbackState = value; }
		}

        /// <summary>
        /// Read/Write property for stop code of Stop Information page
        /// </summary>
        public string StopCode
        {
            get
            {
                return stopCode;
            }

            set
            {
                stopCode= value;
            }
        }

        /// <summary>
        /// Read/Write property for stop code type of Stop Information page
        /// </summary>
        public TDCodeType StopCodeType
        {
            get
            {
                return stopCodeType;
            }

            set
            {
                stopCodeType = value;
            }
        }

        /// <summary>
        /// Read/Write boolean property to determine whether to show StopInformationFacilityControl or not
        /// </summary>
        public bool ShowStopInformationFacilityControl
        {
            get
            {
                return showStopInformationFacilityControl;
            }
            set
            {
                showStopInformationFacilityControl = value;
            }
        }

        /// <summary>
        /// Read/Write boolean property to determine whether to show StopInformationLocalityControl or not
        /// </summary>
        public bool ShowStopInformationLocalityControl
        {
            get
            {
                return showStopInformationLocalityControl;
            }
            set
            {
                showStopInformationLocalityControl = value;
            }
        }

        /// <summary>
        /// Read/Write boolean property to determine whether to show StopInformationOperatorControl or not
        /// </summary>
        public bool ShowStopInformationOperatorControl
        {
            get
            {
                return showStopInformationOperatorControl;
            }
            set
            {
                showStopInformationOperatorControl = value;
            }
        }

        /// <summary>
        /// Read/Write boolean property to determine whether to show StopInformationRealTimeControl or not
        /// </summary>
        public bool ShowStopInformationRealTimeControl
        {
            get
            {
                return showStopInformationRealTimeControl;
            }
            set
            {
                showStopInformationRealTimeControl = value;
            }
        }

        /// <summary>
        /// Read/Write boolean property to determine whether to show StopInformationTaxiControl or not
        /// </summary>
        public bool ShowStopInformationTaxiControl
        {
            get
            {
                return showStopInformationTaxiControl;
            }
            set
            {
                showStopInformationTaxiControl = value;
            }
        }

        /// <summary>
        /// Read/Write boolean property to determine whether to show StopInformationPlanJourneyControl or not
        /// </summary>
        public bool ShowStopInformationPlanJourneyControl
        {
            get
            {
                return showStopInformationPlanJourneyControl;
            }
            set
            {
                showStopInformationPlanJourneyControl = value;
            }
        }

        /// <summary>
        /// Read/Write boolean property to determine whether to show StopInformationDepartureBoardControl or not
        /// </summary>
        public bool ShowStopInformationDepartureBoardControl
        {
            get
            {
                return showStopInformationDepartureBoardControl;
            }
            set
            {
                showStopInformationDepartureBoardControl = value;
            }
        }

        /// <summary>
        /// Read/Write boolean property to determine whether to show StopInformationMapControl or not
        /// </summary>
        public bool ShowStopInformationMapControl
        {
            get
            {
                return showStopInformationMapControl;
            }
            set
            {
                showStopInformationMapControl = value;
            }
        }

        /// <summary>
        /// Read/Write boolean property to determine whether to show the Expand button 
        /// on the StopInformationMapControl. Default is true
        /// </summary>
        public bool ShowStopInformationMapControlExpandButton
        {
            get
            {
                return showStopInformationMapControlExpandButton;
            }
            set
            {
                showStopInformationMapControlExpandButton = value;
            }
        }

        /// <summary>
        /// Read/Write property for stop service detail page stop code
        /// </summary>
        public string StopServiceDetailsStopCode
        {
            get
            {
                return stopServiceDetailsStopCode;
            }
            set
            {
                stopServiceDetailsStopCode = value;
            }
        }

        /// <summary>
        /// Read/Write property for stop service detail page operator code
        /// </summary>
        public string StopServiceDetailsOperatorCode
        {
            get
            {
                return stopServiceDetailsOperatorCode;
            }
            set
            {
                stopServiceDetailsOperatorCode = value;
            }
        }

        /// <summary>
        /// Read/Write property for stop service detail page service number
        /// </summary>
        public string StopServiceDetailsServiceNumber
        {
            get
            {
                return stopServiceDetailsServiceNumber;
            }
            set
            {
                stopServiceDetailsServiceNumber = value;
            }
        }

        /// <summary>
        /// Read/Write property for stop service detail page Stop type
        /// </summary>
        public TDStopType StopServiceDetailsStopType
        {
            get
            {
                return stopServiceDetailsStopType;
            }
            set
            {
                stopServiceDetailsStopType = value;
            }
        }

        /// <summary>
        /// Read/Write property for stop service detail page Code type
        /// </summary>
        public TDCodeType StopServiceDetailsCodeType
        {
            get
            {
                return stopServiceDetailsCodeType;
            }
            set
            {
                stopServiceDetailsCodeType = value;
            }
        }
        
        /// <summary>
        /// Read/Write property to set TDOnMove url from stop information departure board control 
        /// </summary>
        public string StopInfromationTDOnMoveUrl
        {
            get
            {
                return stopInfromationTDOnMoveUrl;
            }
            set
            {
                stopInfromationTDOnMoveUrl = value;
            }
        }

        /// <summary>
        /// Read/Write property to set Departure board control's show departure value
        /// </summary>
        public bool DepartureBoardShowDeparture
        {
            get
            {
                return departureBoardShowDeparture;
            }
            set
            {
                departureBoardShowDeparture = value;
            }
        }

        /// <summary>
        /// Read/Write property to set whether we have checked for the presence of both Departure and Arrival data
        /// </summary>
        public bool StopInformationDepartArriveChecked
        {
            get
            {
                return stopInformationDepartArriveChecked;
            }
            set
            {
                stopInformationDepartArriveChecked = value;
            }
        }

        /// <summary>
        /// Read/Write property to set whether we have found Departure data
        /// </summary>
        public bool StopInformationDepartExists
        {
            get
            {
                return stopInformationDepartExists;
            }
            set
            {
                stopInformationDepartExists = value;
            }
        }

        /// <summary>
        /// Read/Write property to set whether we have found Arrival data
        /// </summary>
        public bool StopInformationArriveExists
        {
            get
            {
                return stopInformationArriveExists;
            }
            set
            {
                stopInformationArriveExists = value;
            }
        }

        /// <summary>
        /// Read/Write property to list the modes types to be shown on the Accessability page.
        /// When an empty array, the value has not been initialised and should be set
        /// </summary>
        public ModeType[] AccessabilityModeTypes
        {
            get { return accessabilityModeTypes; }
            set { accessabilityModeTypes = value; }
        }

        /// <summary>
        /// Read/Write. AccessibleLocationsOrigin for accessible locations displayed on Accessible stops page
        /// </summary>
        public TDLocationAccessible[] AccessibleLocationsOrigin
        {
            get { return accessibleLocationsOrigin; }
            set { accessibleLocationsOrigin = value; }
        }

        /// <summary>
        /// Read/Write. AccessibleLocationsDestination for accessible locations displayed on Accessible stops page
        /// </summary>
        public TDLocationAccessible[] AccessibleLocationsDestination
        {
            get { return accessibleLocationsDestination; }
            set { accessibleLocationsDestination = value; }
        }

        /// <summary>
        /// Read/Write. AccessibleLocationsPublicVia for accessible locations displayed on Accessible stops page
        /// </summary> 
        public TDLocationAccessible[] AccessibleLocationsPublicVia
        {
            get { return accessibleLocationsPublicVia; }
            set { accessibleLocationsPublicVia = value; }
        }

        public TDDateTime OriginalOutwardDateTime
        {
            get
            {
                return originalOutwardDateTime;
            }
            set
            {
                originalOutwardDateTime = value;
            }
        }

        public TDDateTime OriginalReturnDateTime
        {
            get
            {
                return originalReturnDateTime;
            }
            set
            {
                originalReturnDateTime = value;
            }
        }
	}
}
