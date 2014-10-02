// *********************************************** 
// NAME				 : CarJourneyDetailsTableControl.ascx.cs 
// AUTHOR			   : Kenny Cheung
// DATE CREATED		 : 21/08/2003 
// DESCRIPTION			: A custom user control to
// display the details of a car journey in
// a tabular format.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CarJourneyDetailsTableControl.ascx.cs-arc  $
//
//   Rev 1.11   Sep 08 2011 13:10:22   apatel
//Updated to resolve the issues with printer friendly, padding for daily end date and daily end date adjustment issues
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.10   Sep 01 2011 10:44:42   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.9   Nov 08 2010 08:48:22   apatel
//Updated to remove CJP additional information for Trunk Exchange Point and interchange time
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.8   Oct 26 2010 14:30:34   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.7   Dec 02 2009 12:17:20   mmodi
//Updated to display map direction number link on Car journey details table
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Oct 16 2009 17:04:24   mmodi
//Pass is CJP user flag to EBC formatter
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.5   Oct 15 2009 14:25:52   mmodi
//Hide header and footer table if there are no rows visible within it (xhtml compliance). And general tidy up code regions.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.4   Sep 21 2009 14:57:08   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   May 08 2008 11:40:52   mmodi
//Changes made to multiple files to fix the find a car park feature of the city-to-city trunk mode. Testing performed to ensure that the trunk car park features has not broken the drive to car park mode.
//Resolution for 4954: Include 'Drive to Car Park' functionality in City to City
//
//   Rev 1.2   Mar 31 2008 13:19:32   mturner
//Drop3 from Dev Factory
//
//  Rev devfactory Feb 20 2008 14:00:00   mmodi
//Update to display the Find car parks link only when in Car mode
//
//  Rev devfactory Feb 12 2008 12:30:00   mmodi
//Update to not display the Find car parks link when in Door to door mode
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.0   Nov 08 2007 13:12:34   mturner
//Initial revision.
//
//   Rev 1.56   Dec 07 2006 13:43:46   mturner
//Merge for stream4240
//
//   Rev 1.54.1.1   Nov 29 2006 12:33:32   mmodi
//Code to intialise the JourneyEmissions page state
//Resolution for 4240: CO2 Emissions
//Resolution for 4278: CO2: Selecting Plan a journey tab retains previous Emissions values
//
//   Rev 1.54.1.0   Nov 25 2006 16:52:56   dsawe
//Removed footer & header template & added hyperlink control for fuel cost logo
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.54   Nov 09 2006 15:54:30   mmodi
//Removed Car Park opening times note
//Resolution for 4248: Del 9.1: Remove Opening time note on Car Park Information page
//
//   Rev 1.53   Nov 08 2006 10:48:02   mmodi
//Improved PrinterFriendly code for Car Park detail line
//Resolution for 4212: Car Parking: Printer friendly view of journey details displays car park hyperlink
//
//   Rev 1.52   Oct 30 2006 17:50:02   mmodi
//Added code to hide hyperlink for CarPark detail line when in PrinterFriendly mode
//Resolution for 4212: Car Parking: Printer friendly view of journey details displays car park hyperlink
//
//   Rev 1.51   Oct 06 2006 14:22:28   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.50.1.1   Sep 26 2006 12:23:18   esevern
//Added car parking opening times note
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4164: Car Parking: Car park note is not displayed in Details view
//
//   Rev 1.50.1.0   Sep 20 2006 16:59:14   esevern
//Added HyperlinkPostbackControl and event handler to provide link to CarParkInformation page from car park location data in results table
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4163: Car Parking: Car park is not shown as a link in Details view
//
//   Rev 1.50   Feb 23 2006 19:16:26   build
//Automatically merged from branch for stream3129
//
//   Rev 1.49.1.0   Jan 10 2006 15:23:46   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.49   Nov 14 2005 18:50:52   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.48   Nov 03 2005 17:08:42   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.47.1.0   Oct 19 2005 11:03:48   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.47   Aug 02 2005 12:48:36   jmorrissey
//Removed DisplayData(); from PageLoad(). Also being done in Pre-Render.
//
//   Rev 1.46   May 12 2005 17:36:30   asinclair
//Changed constructor for CarFormatter
//
//   Rev 1.45   Apr 23 2005 19:28:00   asinclair
//Fix for IR1983
//
//   Rev 1.44   Apr 12 2005 10:57:58   bflenk
//Work in Progress - IR 1986
//
//   Rev 1.43   Apr 06 2005 11:32:14   asinclair
//Added MapShow method to set visability of Map buttons on Print page.
//
//   Rev 1.42   Mar 18 2005 16:22:14   asinclair
//Added code for Map button click
//
//   Rev 1.41   Mar 01 2005 16:26:46   asinclair
//Checked in to fix error causing build fail
//
//   Rev 1.40   Mar 01 2005 15:54:36   asinclair
//Updated for Del 7 car costing - work in progress
//
//   Rev 1.39   Jan 20 2005 10:40:22   asinclair
//Work in progress - Del 7 Car costing
//
//   Rev 1.38   Jul 21 2004 18:25:38   rgreenwood
//IR1117
//
//   Rev 1.37   Jun 16 2004 20:23:52   JHaydock
//Update for JourneyDetailsTableControl to use ItineraryManager
//
//   Rev 1.36   Jun 03 2004 10:23:28   jbroome
//Added support for ItineraryManager - Extend Journey
//
//   Rev 1.35   Apr 02 2004 17:06:42   AWindley
//DEL 5.2 QA Changes: Resolution for 692
//
//   Rev 1.34   Feb 02 2004 13:43:04   COwczarek
//Work in progress
//Resolution for 613: Refactoring of code that displays car journey details
//
//   Rev 1.33   Jan 27 2004 17:48:20   COwczarek
//Work in progress
//Resolution for 613: Refactoring of code that displays car journey details
//
//   Rev 1.32   Jan 23 2004 16:17:36   COwczarek
//Work in progress
//Resolution for 613: Refactoring of code that displays car journey details
//
//   Rev 1.31   Jan 23 2004 11:56:50   COwczarek
//Work in progress
//Resolution for 613: Refactoring of code that displays car journey details
//
//   Rev 1.30   Jan 13 2004 12:38:32   RPhilpott
//Make compatible with use by "email to a friend" facility.
//Resolution for 549: Send to a Friend email eight various problems
//Resolution for 589: Mileage/road description mismatch
//
//   Rev 1.29   Jan 09 2004 19:05:28   RPhilpott
//Line up distances with road names correctly, and associated changes to initial and final legs.
//Resolution for 589: Mileage/road description mismatch
//
//   Rev 1.28   Dec 02 2003 14:58:20   kcheung
//Reverted so that Arrival Time is always displayed.
//Resolution for 466: Durations but not journey times on unadjusted journey
//
//   Rev 1.27   Dec 02 2003 10:07:00   kcheung
//Added logic to not display the "Arrival Time" column if the current car journey being rendered is for Unadjusted Route.
//Resolution for 466: Durations but not journey times on unadjusted journey
//
//   Rev 1.26   Nov 26 2003 16:46:54   kcheung
//Fixed time rounding errors.
//Previously using Math.Round - this always rounds to the  nearest EVEN number when the value to round is a point 5.  This means if a duration is 1:30, you get 2 minutes, and when it is 2:30 you also get 2 minutes - which creates inconsistency.
//Custom round method has been added to remove this problem.
//
//   Rev 1.25   Nov 26 2003 14:19:30   kcheung
//Corrected rounding error
//
//   Rev 1.24   Nov 17 2003 15:33:38   kcheung
//Added lang=en, removed redundant method
//
//   Rev 1.23   Nov 17 2003 13:47:18   kcheung
//Added miles label to the header.
//Resolution for 71: Car Journey Plan Distances
//
//   Rev 1.22   Nov 13 2003 11:06:18   RPhilpott
//No change.
//Resolution for 177: Errors in road directions
//
//   Rev 1.21   Nov 12 2003 19:27:12   RPhilpott
//Correct alignment of instructions with corresponding road names.
//
//   Rev 1.20   Nov 10 2003 17:36:22   RPhilpott
//Use correct location description on last leg of return journeys.
//
//   Rev 1.19   Nov 06 2003 15:29:06   kcheung
//Changed property of directions from default false to default true as all places that it is currently used at requires it to be true.
//
//   Rev 1.18   Nov 04 2003 13:08:00   kcheung
//Fixed numeric bug for less than 30 seconds check.
//
//   Rev 1.17   Oct 29 2003 09:43:38   kcheung
//Fixed Heading and and added "Directions" label as requested in QA.
//
//   Rev 1.16   Oct 27 2003 12:14:08   kcheung
//Fixed so that < 30 seconds is generated instead of 0 hours 0 mins
//
//   Rev 1.15   Oct 27 2003 11:29:44   kcheung
//Updated so that if the next road is empty then the correct route text is generated
//
//   Rev 1.14   Oct 22 2003 11:34:58   kcheung
//Fixed for FXCOP
//
//   Rev 1.13   Oct 21 2003 15:55:26   kcheung
//FXCOP fixes
//
//   Rev 1.12   Oct 20 2003 10:26:58   kcheung
//Minor corrections for FXCOP
//
//   Rev 1.11   Oct 16 2003 13:41:48   kcheung
//Fixed so that the duration of a leg is rounded to the nearest minute.
//
//   Rev 1.10   Oct 15 2003 13:30:02   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.9   Oct 08 2003 10:37:02   PNorell
//Removed exceptions and errors when no journey results where found.
//
//   Rev 1.8   Sep 29 2003 16:48:52   kcheung
//Update to read from description and not locality
//
//   Rev 1.7   Sep 25 2003 16:39:12   kcheung
//Integrated stylesheet stuff
//
//   Rev 1.6   Sep 25 2003 13:05:16   kcheung
//Integrated HTML for Car Details Control
//
//   Rev 1.5   Sep 15 2003 16:08:04   kcheung
//Updated
//
//   Rev 1.4   Sep 04 2003 15:14:34   kcheung
//Updated bear strings
//
//   Rev 1.3   Sep 04 2003 11:42:20   kcheung
//Updated route text strings to match Design Note "Driving Instructions."
//
//   Rev 1.2   Sep 03 2003 16:09:34   kcheung
//Good working version after integration.
//
//   Rev 1.1   Aug 22 2003 11:50:40   kcheung
//Updated - working
//
//   Rev 1.0   Aug 22 2003 10:26:16   kcheung
//Initial Revision

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///	A custom user control to display the details of a car journey in
	/// a tabular format.
	/// </summary>
	public partial  class CarJourneyDetailsTableControl : TDUserControl
    {
        #region Private members

        protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl directionsHyperlink;
		protected System.Web.UI.WebControls.Label directionsLabel;
		protected TransportDirect.UserPortal.Web.Controls.TDButton buttonMap;
		protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl fuelEmissionLinkControlFooter;
		protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl fuelEmissionLinkControlHeader;

        private PageId belongingPageId = PageId.Empty;
		
		// Counter for number of rows output by detailsRepeater
		protected int repeaterRowCount;
		
		// Indicates if control is rending outward or return journey details
		private bool outward = false;
		private bool initialised = false;
		private RoadJourney roadJourney = null;
		private TDJourneyViewState viewState = null;

		//Added for DEL 7 
		private string buttonMapText = String.Empty;
		private bool nonprintable;

		// Array of column titles for details table
		private IList headerDetail;

		private IList footerDetail;

		private int journeyItineraryIndex = -1;

		private RoadUnitsEnum roadUnits;

		private string firsttoid;
		private string lasttoid;
		private bool mapButtonVisible;
        public int journeyLength;

        // Variables needed to add javascript to the direction link click
        private bool addMapJavascript = false;
        private string mapId = "map";
        private string mapJourneyDisplayDetailsDropDownId = "mapdropdown";
        private string scrollToControlId = "mapControl";
        private string sessionId = "session";
        private string journeyType = "Road";
        private MapHelper mapHelper = new MapHelper();

        private bool showDirectionTime = true;  // Default to show Time and not Distance
        private bool showDirectionDistance = false;
        private bool showTravelNewsIncidents = true; // Default to show matching Travel News incidents

        private CarJourneyDetailsTableFormatter formatter = CarJourneyDetailsTableFormatter.Default;

		// Del 9 car parking constants
		public const char SEPARATOR = '$';
		public const int ARRIVE_AT = 0;
		public const int CARPARK_DESCRIPTION = 1; 
		public const int CARPARK_NAME = 2;
		public const int CARPARK_REF = 3;
		public const int CARPARK_LENGTH = 5;
		public const int DIRECTIONS_INDEX = 2;

		private bool carParkVisible = false;
		private bool carParkLabelNoteVisible = false;
		protected System.Web.UI.WebControls.Label openingTimesLabel;

        #endregion

        #region Public enum

        /// <summary>
        /// Enum specifying the formatter to use on this control
        /// </summary>
        public enum CarJourneyDetailsTableFormatter
        {
            Default,
            EnvironmentalBenefits
        }

        #endregion

        #region Public properties

        /// <summary>
		/// Event raised when the user clicks a "Map" button.
		/// </summary>
		public event SectionMapSelectedEventHandler SectionMapSelected;

		/// <summary>
		/// Event raised when the user clicks the first or the last "Map" button.
		/// </summary>
		public event FirstLastMapSelectedEventHandler FirstLastMapSelected;

		/// <summary>
		/// Array of column titles for details table
		/// </summary>
		public IList HeaderDetail
		{
			get { return headerDetail; }
		}

		/// <summary>
		/// Array of footer titles for details table
		/// </summary>
		public IList FooterDetail
		{
            get { return footerDetail; }
		}

		/// <summary>
		/// Get/Set property - get or sets the page Id. This should be the page Id
		/// of the page that contains this control.
		/// </summary>
		public PageId MyPageId
		{
			get { return belongingPageId; }
            set { belongingPageId = value; }
		}

		public RoadUnitsEnum RoadUnits
		{
			get { return roadUnits; }
			set { roadUnits = value; }
		}

		/// <summary>
		/// Set and get property if this component is in printable mode or not.
		/// </summary>
		public bool NonPrintable 
		{
			get {return nonprintable;}
			set {nonprintable = value;}
        }

        /// <summary>
        /// Read/write property to show the direction time.
        /// If direction time is set to true (default is true), 
        /// then direction distance column is not shown
        /// </summary>
        public bool ShowDirectionTime
        {
            get { return showDirectionTime; }
            set
            {
                showDirectionTime = value;
                showDirectionDistance = !showDirectionTime;
            }
        }

        /// <summary>
        /// Read/write property to show the direction distance.
        /// If direction distance is set to true (default is false), 
        /// then direction time column is not shown
        /// </summary>
        public bool ShowDirectionDistance
        {
            get { return showDirectionDistance; }
            set 
            { 
                showDirectionDistance = value;
                showDirectionTime = !showDirectionDistance;
            }
        }

        public bool ShowTravelNewsIncidents
        {
            get { return showTravelNewsIncidents; }
            set
            {
                showTravelNewsIncidents = value;
            }
        }

        /// <summary>
        /// Read/write property to set the formatter to use in generating the details.
        /// Default is to use the DefaultCarJourneyDetail formatter
        /// </summary>
        public CarJourneyDetailsTableFormatter Formatter
        {
            get { return formatter; }
            set { formatter = value; }
        }

        /// <summary>
        /// Read/write property returning car park display visibility
        /// </summary>
        public bool CarParkVisible
        {
            get { return carParkVisible; }
            set { carParkVisible = value; }
        }

        /// <summary>
        /// Read/write property returning the car park opening times label note visibility
        /// </summary>
        public bool CarParkLabelNoteVisible
        {
            get { return carParkLabelNoteVisible; }
            set { carParkLabelNoteVisible = value; }
        }

        /// <summary>
        /// Get/Set property to get/set visibility of the directions label.
        /// </summary>
        public bool DirectionsLabelVisible
        {
            get { return labelDirections.Visible; }
            set { labelDirections.Visible = value; }
        }

        /// <summary>
        /// Read Only.
        /// Determines if the cjp info summary div should be visible
        /// </summary>
        public bool IsCJPInfoSummaryAvailable
        {
            get
            {
                return CJPUserInfoHelper.IsCJPInformationAvailableForType(CJPInfoType.TrunkExchangePoint);
            }
        }

        public string HighTrafficSymbolUrl
        {
            get
            {
                return Global.tdResourceManager.GetString(
                "CarJourneyDetailsControl.highTrafficSymbol.ImageUrl", TDCultureInfo.CurrentUICulture);
            }
        }

        public string HighTrafficSymbolToolTip
        {
            get
            {
                string tooltip = string.Empty;

                TDPage page = (TDPage)Page;
                
                tooltip = Global.tdResourceManager.GetString(
                    "CarJourneyDetailsControl.highTrafficSymbol.ToolTip", TDCultureInfo.CurrentUICulture);
            

                return tooltip;
               
            }
        }

        public string HighTrafficSymbolPopupTitle
        {
            get
            {
                return Global.tdResourceManager.GetString(
                "CarJourneyDetailsControl.highTrafficSymbol.PopupTitle", TDCultureInfo.CurrentUICulture);
            }
        }

        public string HighTrafficSymbolPopupContent
        {
            get
            {
                return Global.tdResourceManager.GetString(
                "CarJourneyDetailsControl.highTrafficSymbol.PopupContent", TDCultureInfo.CurrentUICulture);
            }
        }
        #endregion

        #region Page Load, OnPreRender

        /// <summary>
		/// Page Load method
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			// Directions label is visible by default
			labelDirections.Visible = true;

			buttonMapText = Global.tdResourceManager.GetString(
				"CarJourneyDetailsControl.buttonMap.Text", TDCultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Sets-up event handlers 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // Create a formatter to generate formatted journey instructions
            DisplayData();
            AddEventHandlers();
            base.OnLoad(e);
        }

        /// <summary>
        /// OnPreRender method.
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            // IR4270: Congestion Charge - Same day return journey shows charge of £0
            // DisplayData is called twice, OnLoad and OnPreRender. The first call 
            // sets CongestionChargeAdded flag to true, and on the second call it sees 
            // the flag as congestion charge has been added and therefore shows £0. 
            // We need to reset this flag to ensure the flag is in its initial state 
            // prior to the second call.
            // This is not a nice solution and when the Car Details code is refactored, 
            // this should be looked at.
            if (outward)
            {
                TDSessionManager.Current.JourneyViewState.CongestionChargeAdded = false;
            }

            // Create a formatter to generate formatted journey instructions
            DisplayData();
            AddEventHandlers();

            RegisterPopupJavaScript();

            DisplayTables();

            base.OnPreRender(e);
        }

        #endregion

        #region Initialise

        /// <summary>
		/// Initialises the control - this method must be called.
		/// </summary>
		/// <param name="outward">Indiciates if outward or return road journey should be rendered.</param>
		public void Initialise(bool outward)
		{
			this.outward = outward;
			this.initialised = true;
		}

        /// <summary>
        /// Initialises the control - this method must be called.
        /// </summary>
        /// <param name="outward">Indiciates if outward or return road journey should be rendered.</param>
        /// <param name="journeyParams">Journey parameters</param>
        public void Initialise(bool outward, TDJourneyParametersMulti journeyParams)
        {
            this.outward = outward;
            this.initialised = true;

           
        }

		/// <summary>
		/// Initialises the control - this method must be called.
		/// </summary>
		/// <param name="roadJourney">The specific road journey to display</param>
		/// <param name="journeyViewState">The related journey view state</param>
		/// <param name="outward">Whether the journey is an outward one</param>
		public void Initialise(RoadJourney roadJourney, TDJourneyViewState viewState, bool outward)
		{
			this.roadJourney = roadJourney;
			this.viewState = viewState;
			this.outward = outward;
			this.initialised = true;

		}

        /// <summary>
        /// Method which sets the values needed to add map javascript to the direction links
        /// </summary>
        public void SetMapProperties(bool addMapJavascript, string mapId, string mapJourneyDisplayDetailsDropDownId,
            string scrollToControlId, string sessionId)
        {
            this.addMapJavascript = addMapJavascript;
            this.mapId = mapId;
            this.mapJourneyDisplayDetailsDropDownId = mapJourneyDisplayDetailsDropDownId;
            this.scrollToControlId = scrollToControlId;
            this.sessionId = sessionId;
        }

        #endregion

        #region Private methods

        private void DisplayData()
		{
			if (initialised)
			{
				// Only display the car park link if switched on, and for Car, Door to door, 
                // City-to-City and not modified
                bool showCarParkLink =
                    (FindCarParkHelper.CarParkingAvailable)
                        &&
                    (this.PageId != PageId.CarDetails)
                        &&
                    ((TDSessionManager.Current.FindAMode == FindAMode.Car)
                            || (TDSessionManager.Current.FindAMode == FindAMode.None)
                            || (TDSessionManager.Current.FindAMode == FindAMode.Trunk));

                // Set up the detail formatter
                if (formatter == CarJourneyDetailsTableFormatter.Default)
                {
                    #region Default formatter

                    CarJourneyDetailFormatter defaultDetailFormatter;

                    if (roadJourney == null || viewState == null)
                    {
                        defaultDetailFormatter = new DefaultCarJourneyDetailFormatter(
                            TDItineraryManager.Current.JourneyResult,
                            TDItineraryManager.Current.JourneyViewState,
                            outward,
                            TDCultureInfo.CurrentUICulture,
                            RoadUnits, nonprintable
                            );

                        // Save road journey for use
                        if (outward)
                        {
                            roadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();
                        }
                        else
                        {
                            roadJourney = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();
                        }
                    }
                    else
                    {
                        defaultDetailFormatter = new DefaultCarJourneyDetailFormatter(
                            roadJourney,
                            viewState,
                            outward,
                            TDCultureInfo.CurrentUICulture,
                            RoadUnits, nonprintable
                            );
                    }

                    detailsRepeater.DataSource = defaultDetailFormatter.GetJourneyDetails();

                    defaultDetailFormatter.ShowFindNearestCarParksLink = showCarParkLink;

                    headerDetail = new ArrayList(defaultDetailFormatter.GetDetailHeadings());
                    footerDetail = new ArrayList(defaultDetailFormatter.GetFooterHeadings());

                    #endregion
                }
                else
                {
                    #region EBC formatter

                    // EBC formatter
                    EBCCarJourneyDetailFormatter ebcDetailFormatter;

                    ebcDetailFormatter = new EBCCarJourneyDetailFormatter(
                            roadJourney,
                            viewState,
                            outward,
                            TDCultureInfo.CurrentUICulture,
                            RoadUnits, nonprintable, IsCJPUser()
                            );

                    detailsRepeater.DataSource = ebcDetailFormatter.GetJourneyDetails();

                    ebcDetailFormatter.ShowFindNearestCarParksLink = showCarParkLink;

                    headerDetail = new ArrayList(ebcDetailFormatter.GetDetailHeadings());
                    footerDetail = new ArrayList(ebcDetailFormatter.GetFooterHeadings());

                    #endregion
                }

                // Bind the tables and repeater
				detailsRepeater.DataBind();
				RouteDirections.DataBind();
				RouteDirections2.DataBind();

//              //if destination location is a carpark then hide the find nearest car parks controls
//              footerDetail[11] = Convert.ToBoolean((this.roadJourney.DestinationLocation.CarParking != null));
			}
        }

        /// <summary>
        /// Method to add the event handlers to add dynamically generated buttons
        /// </summary>
        private void AddEventHandlers()
        {

            // Add handler if appropriate
            for (int i = 0; i < detailsRepeater.Items.Count; i++)
            {
                if ((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("directionsHyperlink") != null)
                {
                    HyperlinkPostbackControl link = (HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("directionsHyperlink");
                    if (link.Visible)
                    {
                        link.link_Clicked += new System.EventHandler(this.CarParkInformationLinkClick);
                    }
                }

                if ((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("directionsHyperlink") != null)
                {
                    HyperlinkPostbackControl link = (HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("directionsHyperlink");
                    if (link.Visible)
                    {
                        link.link_Clicked += new System.EventHandler(this.CarParkInformationLinkClick);
                    }
                }
            }
        }

        /// <summary>
        /// Event handler called when binding detailsRepeater to data source.
        /// Resets the row count output by repeater.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">The event parameters</param>
        private void dataBinding(object sender, System.EventArgs e)
        {
            repeaterRowCount = 0;

            ArrayList details = (ArrayList)detailsRepeater.DataSource;
            foreach (object[] objects in details)
            {
                if (objects.Length == 5)
                {
                    RoadJourneyDetailMapInfo mapInfo = (RoadJourneyDetailMapInfo)objects[4];
                    if (mapInfo.firstToid == null)
                    {
                        mapButtonVisible = false;

                    }
                    else
                    {
                        string first = mapInfo.firstToid;
                        string last = mapInfo.lastToid;

                        mapButtonVisible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Method which sets the visibility of the header and footer tables dependent on 
        /// if any rows are visible
        /// </summary>
        private void DisplayTables()
        {
            bool showHeaderTable = false;
            bool showFooterTable = false;

            // Header table
            foreach (TableRow row in RouteDirections.Rows)
            {
                if (row.Visible)
                {
                    showHeaderTable = true;
                    break;
                }
            }

            // Footer table
            foreach(TableRow row in RouteDirections2.Rows)
            {
                if (row.Visible)
                {
                    showFooterTable = true;
                    break;
                }
            }
            
            RouteDirections.Visible = showHeaderTable;
            RouteDirections2.Visible = showFooterTable;
        }

        /// <summary>
        /// Method which returns true if user is a higher-level (e.g. CJP) user 
        /// </summary>
        private bool IsCJPUser()
        {
            bool userIsLoggedOn = TDSessionManager.Current.Authenticated;

            // Get the user's type
            int userType = userIsLoggedOn ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;

            return (userType > 0);
        }

        /// <summary>
        /// Register Javascript
        /// Sets up the popup script
        /// </summary>
        private void RegisterPopupJavaScript()
        {
            TDPage page = (TDPage)this.Page;


            if (page.IsJavascriptEnabled)
            {

                // Determine if javascript is support and determine the JavascriptDom value
                ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                string scriptName = "TDInfoWindow";

                string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

                // Register the javascript file
                page.ClientScript.RegisterStartupScript(this.GetType(), "", scriptRepository.GetScript(scriptName, javaScriptDom));

            }

        }
                
        #endregion

        #region Public methods

        /// <summary>
		/// Returns the initial text for the journey directions 
		/// ('Leave from' or 'Arrive at')
		/// </summary>
		/// <param name="data">string data row</param>
		/// <returns>string directions text</returns>
		public string SetLabelContent(string data)
		{
			string[] values = data.Split(new char[]{SEPARATOR});
			return (string)values[ARRIVE_AT];
		}

		/// <summary>
		/// Checks length of row data to determine if the car 
		/// park hyperlink should be visible
		/// </summary>
		/// <param name="data">string row data</param>
		/// <returns>bool</returns>
		public bool IsHyperlinkVisible(string data)
		{
			string[] values = data.Split(new char[]{SEPARATOR});
			// if we have car parking info

			if(values.Length > 1)
			{
				// IR4248 - remove the Opening times note
				carParkLabelNoteVisible = false;

				carParkVisible = true;
				return carParkVisible;
			}
			else 
			{
				carParkLabelNoteVisible = false;
				carParkVisible = false;
				return carParkVisible;
			}
		}

		/// <summary>
		/// Sets the PrinterFriendly status of the hyperlink
		/// </summary>
		/// <param name="data">string row data</param>
		/// <returns>bool</returns>
		public bool IsPrintable(string data)
		{
			if (nonprintable)
				return false;
			else
				return true;
		}

		/// <summary>
		/// Returns true if a break should be created between rows output
		/// by detailsRepeater based on the number of rows created by the
		/// control.
		/// </summary>
		/// <param name="frequency">The number of rows to output before a break is required</param>
		/// <returns>true if a break should be created, false otherwise</returns>
		public bool TableBreakRequired(int frequency) 
		{
			return (++repeaterRowCount % frequency == 0);
		}	
        
		#endregion

		#region ViewState Code
		/// <summary>
		/// Loads the ViewState.
		/// </summary>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					outward = (bool)myState[1];
			}
		}
	
		/// <summary>
		/// Overrides the base SaveViewState to customise viestate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
		
			object[] allStates = new object[2];
			allStates[0] = baseState;
			allStates[1] = outward;

			return allStates;
		}

		/// <summary>
		/// Method handles the click event from the hyperlinkJourneyEmission
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hyperlinkJourneyEmission_link_Clicked(object sender, EventArgs e)
		{
			// Set page id in stack so we know where to come back to
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId );

			// Reset the journey emissions page state, to clear it of any previous values
			TDSessionManager.Current.JourneyEmissionsPageState.Initialise();

			// Navigate to the Journey Accessibility page
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissions;
		}

		/// <summary>
		/// Event Handler for the information hyperlink.
		/// </summary>
		public void CarParkInformationLinkClick(object sender, EventArgs e)
		{
			// User has clicked the information hyperlink for a the carpark.
			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;
			string carParkRef =  link.CommandArgument;
			TDSessionManager.Current.InputPageState.CarParkReference = carParkRef;

			// This is how we 'return'
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);

			// Show the information page for the selected location.
			// Write the Transition Event
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.FindCarParkResultsInfo;
		}


		/// <summary>
		/// Returns the reference number of the car park as a string
		/// </summary>
		/// <param name="data">string row data</param>
		/// <returns>string car park reference</returns>
		public string GetCarParkRef(string data)
		{
			if (CarParkVisible)		
			{
				string[] values = data.Split(new char[]{SEPARATOR});
				return values[CARPARK_REF];
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns the initial direction text 'arrive at' or 'leave from'
		/// </summary>
		/// <param name="data">string row data</param>
		/// <returns>string direction text</returns>
		public string GetArriveAt(string data)
		{
			if(CarParkVisible)
			{			
				string[] values = data.Split(new char[]{SEPARATOR});
				return values[ARRIVE_AT];
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns the location description text
		/// </summary>
		/// <param name="data">string row data</param>
		/// <returns>string location description</returns>
		public string GetDescription(string data)
		{
			if(CarParkVisible)
			{
				string[] values = data.Split(new char[]{SEPARATOR});
				return values[CARPARK_DESCRIPTION];
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns the car park name as a string
		/// </summary>
		/// <param name="data">string row data</param>
		/// <returns>string car park name</returns>
		public string GetCarParkName(string data)
		{
			if(CarParkVisible)
			{
				string[] values = data.Split(new char[]{SEPARATOR});
				return values[CARPARK_NAME];
			}
			else
			{
				return string.Empty;
			}
		}

        /// <summary>
        /// Sets the script to display high traffic level message in information window
        /// </summary>
        /// <returns></returns>
        public string GetShowPopupScript()
        {
            if (!nonprintable)
            {
                return string.Empty;
            }
            else
            {
                string popupTitle = Global.tdResourceManager.GetString(
                   "CarJourneyDetailsControl.highTrafficSymbol.PopupTitle", TDCultureInfo.CurrentUICulture);
                string popupContent = Global.tdResourceManager.GetString(
                   "CarJourneyDetailsControl.highTrafficSymbol.PopupContent", TDCultureInfo.CurrentUICulture);

                return string.Format("TravelNewsInfoWindow.showRoadQueuePopup(event,'{0}','{1}');", popupTitle, popupContent);
            }
        }
		
		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// On Init Method
		/// </summary>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			detailsRepeater.DataBinding += new System.EventHandler(this.dataBinding);

			this.fuelEmissionLinkControlHeader.link_Clicked += new System.EventHandler(this.hyperlinkJourneyEmission_link_Clicked);
			this.fuelEmissionLinkControlFooter.link_Clicked += new System.EventHandler(this.hyperlinkJourneyEmission_link_Clicked);

            this.findNearestCarParkLinkControlHeader.link_Clicked += new EventHandler(this.findNearestCarParkLinkControlFooter_link_Clicked);
            this.findNearestCarParkLinkControlFooter.link_Clicked += new EventHandler(this.findNearestCarParkLinkControlFooter_link_Clicked);
			
			// Add handlers if appropriate
			for(int i=0; i<detailsRepeater.Items.Count; i++)
			{
				if((HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("directionsHyperlink") != null)
				{
					HyperlinkPostbackControl link = (HyperlinkPostbackControl)detailsRepeater.Items[i].FindControl("directionsHyperlink");
					if(link.Visible)
					{
						link.link_Clicked += new System.EventHandler(this.CarParkInformationLinkClick);
					}
				}
			} 

            base.OnInit(e);
			
		}

        void findNearestCarParkLinkControlFooter_link_Clicked(object sender, EventArgs e)
        {
            //FindCarParkPageState.FindCarParkMode.DriveTo
            TDJourneyParametersMulti journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            TDSessionManager.Current.FindCarParkPageState.LocationTo = journeyParams.DestinationLocation;
            TDSessionManager.Current.FindCarParkPageState.SearchTo = journeyParams.Destination;
            TDSessionManager.Current.FindCarParkPageState.CurrentSearch = journeyParams.Destination;
            TDSessionManager.Current.FindCarParkPageState.CurrentLocation = journeyParams.DestinationLocation;
            
            // Set the mode for FindCarPark page
            TDSessionManager.Current.FindCarParkPageState.CarParkFindMode = FindCarParkPageState.FindCarParkMode.DriveTo;

            // Set the values to allow return to Journey Details page in correct mode
            //TDSessionManager.Current.FindCarParkPageState..FindAMode = TDSessionManager.Current.FindAMode;
            TDSessionManager.Current.FindCarParkPageState.IsFromDoorToDoor = (TDSessionManager.Current.FindAMode == FindAMode.None);
            TDSessionManager.Current.FindCarParkPageState.IsFromCityToCity = (TDSessionManager.Current.FindAMode == FindAMode.Trunk);

            // Go to the FindCarPark page
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputDefault;
        }
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.detailsRepeater.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.LegMapButtonClick);

		}
		#endregion
			
		#region Maps

        /// <summary>
        /// Generates and return a script to associate with a link to show journey direction on map
        /// </summary>
        /// <param name="row">data row object</param>
        /// <returns>javascript string</returns>
        public string GetShowOnMapScript(string directionNumber)
        {
            string linkScript = string.Empty;

            if (IsRowMapLinkVisible && !string.IsNullOrEmpty(mapId)
                && (roadJourney != null))
            {
                // Ensure map area is visible
                StringBuilder showOnMapScript = new StringBuilder(
                    string.Format("scrollToElement('{0}');", scrollToControlId));

                // The script function called sets the selected index in the journey map directions drop down,
                // and calls the same function to zoom map as the drop down
                showOnMapScript.AppendFormat("zoomRoadJourneyDetailMap('{0}','{1}','{2}','{3}','{4}',{5},'{6}');",
                    mapId,
                    sessionId,
                    roadJourney.RouteNum,
                    journeyType,
                    mapJourneyDisplayDetailsDropDownId,
                    directionNumber,
                    scrollToControlId);
                    
                showOnMapScript.Append(" return false;");

                linkScript = showOnMapScript.ToString();
            }

            return linkScript;
        }

        /// <summary>
        /// Read/Write property to determine if the row index should show as link to zoom to the Map
        /// </summary>
        public bool IsRowMapLinkVisible
        {
            get
            {
                TDPage page = (TDPage)this.Page;
                return addMapJavascript && nonprintable && page.IsJavascriptEnabled;
            }
            set {  addMapJavascript = value; }
        }

        /// <summary>
		/// Get property MapButtonText.
		/// The text value for the map button.
		/// </summary>
		public string MapButtonText
		{
			get { return buttonMapText; }
		}

		/// <summary>
		/// Get property OpenTimeText.
		/// The text value for the opening times label.
		/// </summary>
		public string OpenTimeText
		{
			get 
			{
				return GetResource("CarParkInformationControl.informationNote"); 
			}
		}

	

		/// <summary>
		/// Get property MapButtonVisible.
		/// Indicates if the map button should be rendered
		/// </summary>
		public bool MapButtonVisible
		{
			get 
			{ 
				return mapButtonVisible; 
			}
			
			set 
			{ 
				mapButtonVisible = value;
			}
		}

		public void MapButtonClick(object sender, EventArgs e)
		{
			TDButton button = (TDButton)sender;
			int selectedJourneyLeg = Convert.ToInt32(button.CommandName, TDCultureInfo.CurrentCulture.NumberFormat);

			// Write the selected journey leg to TDViewState
			TDJourneyViewState journeyViewState = TDItineraryManager.Current.JourneyViewState;
			journeyViewState.SelectedJourneyLeg = selectedJourneyLeg;
			journeyViewState.SelectedIntermediateItinerarySegment = journeyItineraryIndex;
			journeyViewState.CallingPageID = belongingPageId;

			TDSessionManager.Current.Session[ SessionKey.JourneyMapOutward ] = outward;
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyDetailMap;
		}

		public void LegMapButtonClick(object sender, RepeaterCommandEventArgs e)
		{
			object[] selectedLeg = (object[])((ArrayList)detailsRepeater.DataSource)[e.Item.ItemIndex];
	
			int end = detailsRepeater.Items.Count-1;

			if (e.Item.ItemIndex == 0 | e.Item.ItemIndex == 1)
			{
				//it is the start.  Show the start of the journey
				TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
				ITDJourneyRequest request = viewState.OriginalJourneyRequest;

				int easting = 0;
				int northing = 0;

				easting = request.OriginLocation.GridReference.Easting;
				northing = request.OriginLocation.GridReference.Northing;

				string SelectedLeg = (e.Item.ItemIndex).ToString();
				OnFirstLastMapSelected( new FirstLastMapSelectedEventArgs (northing, easting, SelectedLeg));
			}


			else if (e.Item.ItemIndex == end)//it is the end
			{
				TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
				ITDJourneyRequest request = viewState.OriginalJourneyRequest;
				
				int easting = 0;
				int northing = 0;
				
				easting = request.DestinationLocation.GridReference.Easting;
				
				northing = request.DestinationLocation.GridReference.Northing;
				
				string SelectedLeg = (e.Item.ItemIndex).ToString();
				
				
				OnFirstLastMapSelected( new FirstLastMapSelectedEventArgs (northing, easting, SelectedLeg));

			}

			else if (selectedLeg.Length > 4)
			{	
				object item = selectedLeg[4];

				//Gets us the first and last toid.
				RoadJourneyDetailMapInfo mapInfo = (RoadJourneyDetailMapInfo)item;
				firsttoid = mapInfo.firstToid;
				lasttoid = mapInfo.lastToid;

				JourneyMapState legMapState = TDSessionManager.Current.JourneyMapState;

				string SelectedLeg = (e.Item.ItemIndex).ToString();

				OnMapSelected( new SectionMapSelectedEventArgs (firsttoid, lasttoid, SelectedLeg));
			}
		
		}


		/// <summary>
		/// Raises the FirstLastMapSelected event
		/// </summary>
		/// <param name="e"></param>
		private void OnFirstLastMapSelected(FirstLastMapSelectedEventArgs e)
		{
			FirstLastMapSelectedEventHandler theDelegate = FirstLastMapSelected;
			if (theDelegate != null)		
				theDelegate(this, e);
		}

		/// <summary>
		/// EventArgs class used if either the first or the last car section is selected
		/// </summary>
		public class FirstLastMapSelectedEventArgs : EventArgs
		{
			private readonly int northing;
			private readonly int easting;
			private readonly string SelectedIndex;
			
			/// <summary>
			/// Constructor. 
			/// </summary>
			/// <param name=" "></param>
			public FirstLastMapSelectedEventArgs(int Northing, int Easting, string selectedIndex)
			{
				this.easting = Easting;
				this.northing = Northing;
				this.SelectedIndex = selectedIndex;
				
			}

			/// <summary>
			/// Read only property allowing access to the Northing
			/// </summary>
			public int Northing
			{
				get { return northing; }
			}

			/// <summary>
			/// Read only property allowing access to the Easting
			/// </summary>
			public int Easting
			{
				get { return easting; }
			}
	
			/// <summary>
			/// Read only property allowing access to the SelectedJourneyIndex
			/// </summary>
			public string SelectedJourneyIndex
			{
				get { return SelectedIndex; }
			}

		}

		/// <summary>
		/// Do not display Map buttons on Printer Friendly page. On normal
		/// page use defaultVisabilty, which is the default setting.
		/// </summary>
		/// <param name="defaultVisablity"></param>
		public bool MapShow(bool defaultVisablity)
		{
			
			if(nonprintable)
			{
				return defaultVisablity;

			}
			else
			{
				return false;

			}
		}


		/// <summary>
		/// Raises the SectionMapSelected event
		/// </summary>
		/// <param name="e"></param>
		private void OnMapSelected(SectionMapSelectedEventArgs e)
		{
			SectionMapSelectedEventHandler theDelegate = SectionMapSelected;
			if (theDelegate != null)
				theDelegate(this, e);
		}



		/// <summary>
		/// EventArgs class used for Map Drive Section Selection
		/// </summary>
		public class SectionMapSelectedEventArgs : EventArgs
		{
			private readonly string last;
			private readonly string first;
			private readonly string SelectedIndex;

			/// <summary>
			/// Constructor. The retailer that was selected must be specified
			/// </summary>
			/// <param name="selectedRetailer"></param>
			public SectionMapSelectedEventArgs(string firsttoid, string lasttoid, string selectedIndex)
			{
				this.first = firsttoid;
				this.last = lasttoid;
				this.SelectedIndex = selectedIndex;
			}

			/// <summary>
			/// Read only property allowing access to the first toid
			/// </summary>
			public string LastSectionToid
			{
				get { return last; }
			}

			/// <summary>
			/// Read only property allowing access to the last toid
			/// </summary>
			public string FirstSectionToid
			{
				get { return first; }
			}

			/// <summary>
			/// Read only property allowing access
			/// </summary>
			public string SelectedJourneyIndex
			{
				get { return SelectedIndex; }
			}


		}

		/// <summary>
		/// Delegate type for events raised by clicking on a map button in a car instruction
		/// </summary>
		public delegate void SectionMapSelectedEventHandler(object sender, SectionMapSelectedEventArgs e);

		/// <summary>
		/// Delegate type for events raised by clicking on the First or the last Map button in a car instruction
		/// </summary>
		public delegate void FirstLastMapSelectedEventHandler(object sender, FirstLastMapSelectedEventArgs e);
		
        #endregion
	
	}

}
