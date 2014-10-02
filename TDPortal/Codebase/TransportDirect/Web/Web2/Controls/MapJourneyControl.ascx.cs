// *********************************************** 
// NAME                 : MapJourneyControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 04/11/2009
// DESCRIPTION          : Map control used on journey result Map page. The control will access the session
//                      : and find the journey(s) to display on the map.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapJourneyControl.ascx.cs-arc  $ 
//
//   Rev 1.23   Sep 14 2010 09:49:20   apatel
//Updated to correct the walkit link for return journey
//Resolution for 5603: Return Journey - Walkit Links Wrong
//
//   Rev 1.22   Jul 12 2010 12:08:54   apatel
//Updated to resolve the issue with fullmap showing instead of leg map after new fix TDP 10.17 released by ESRI
//Resolution for 5559: Map issue with full map showing instead of leg map
//
//   Rev 1.21   Jun 22 2010 11:52:28   mmodi
//Correctly set up via location info popup for a PT journey
//Resolution for 5557: Maps - Journey with Via location does not display
//
//   Rev 1.20   Apr 29 2010 10:34:04   mmodi
//Updated to show Info Circle symbol for car/cycle journey directions on the map
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.19   Apr 22 2010 11:27:38   apatel
//Removed client side script to add walkit link for overlays following update from ESRI
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.18   Apr 09 2010 15:31:08   mmodi
//Only show stop infomation link in the popup if the journey location has one naptan
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.17   Apr 07 2010 13:31:58   pghumra
//Various updates for accessibility purposes
//Resolution for 5491: RS69801 CCN0551 Accessibility - Spaces TfL Map Note London Bus
//
//   Rev 1.16   Mar 22 2010 09:42:30   apatel
//Updated to register javascript file for walkit link to inject walkit link in existing  Map information window showing overlay information (i.e. station link)
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.15   Mar 18 2010 15:29:12   mmodi
//Added code to allow popups to be displayed for the Start/End/Via locations in the journey
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.14   Mar 12 2010 09:13:36   apatel
//updated to show Walkit link  on map
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.13   Mar 10 2010 15:19:14   apatel
//Updated to show Walkit links on map information popup window when user clicks on start location of walk leg
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.12   Jan 19 2010 13:20:24   mmodi
//Updates for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.11   Jan 07 2010 14:21:04   mmodi
//Display Via location point on journey map
//Resolution for 5358: Maps - Via location icons not shown on journey map
//
//   Rev 1.10   Dec 03 2009 14:02:08   mmodi
//Updated to display Cycle journey direction symbols
//
//   Rev 1.9   Dec 02 2009 15:57:00   mmodi
//Corrected Cycle via location point for map
//
//   Rev 1.8   Dec 02 2009 12:17:18   mmodi
//Updated to display map direction number link on Car journey details table
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Nov 29 2009 12:39:12   mmodi
//Updated to show outward or return journey toggle, and updated road journey direction symbols
//
//   Rev 1.6   Nov 28 2009 11:26:50   apatel
//Travel News map enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Nov 27 2009 13:19:30   mmodi
//Updated to initialise with journey and zoom to the journey, and added circle symbols for a road journey directions
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Nov 23 2009 10:31:34   mmodi
//Updated for modified journeys
//
//   Rev 1.3   Nov 15 2009 11:07:22   mmodi
//Updated to display map at selected leg
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Nov 11 2009 18:32:08   mmodi
//Updated
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 09 2009 15:45:08   mmodi
//Updated
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 04 2009 14:11:54   mmodi
//Initial revision.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Map control used on journey result Map page.
    /// The control will access the session and find the journey(s) to display on the map.
    /// </summary>
    public partial class MapJourneyControl : TDUserControl
    {
        #region Private members

        #region Constants

        // Constants used when adding location points to the map
        private const int STARTPOINT = 1;
        private const int ENDPOINT = 2;
        private const int VIAPOINT = 3;

        private const string INTERMEDIATE_NODE_IMAGE_PREFIX = "CIRCLE";
        private const string ROAD_DIRECTION_NODE_IMAGE_PREFIX = "INFOCIRCLE";
        private const string CYCLE_DIRECTION_NODE_IMAGE_PREFIX = "INFOCIRCLE";

        private const string JS_SEPERATOR = ":::";

        // Constants to indicate what type of road journey symbol to add in the javascript
        private const string JOURNEY_ADDSYMBOL_JUNCTION = "J";
        private const string JOURNEY_ADDSYMBOL_NODE = "N";
        private const string JOURNEY_ADDSYMBOL_POINT = "P";

        private const string MAP_ALT_TEXT_ORIGIN_PATTERN = "{ORIGIN}";
        private const string MAP_ALT_TEXT_DESTINATION_PATTERN = "{DESTINATION}";

        #endregion

        private MapHelper mapHelper = new MapHelper();

        private ITDSessionManager sessionManager;
        private TDItineraryManager itineraryManager;

        // Indicates if the map should render for outward journey or return journey
		private bool outward = true;
        private bool showMapTitleArea = true;

        // Indicates if the show map for outward or return journey panel should be shown
        private bool showJourneySelectPanel = false;

        // Variables to specify custom map height and width. 
        // These should only be set where the default size from the map javascript config file 
        // are no good
        private int mapHeight = -1;
        private int mapWidth = -1;

        // Flag used in determining where to obtain journey info - 
        // has user extended journey?
        private bool usingItinerary = false;
        private bool itinerarySegmentSelected = false;
        
        // Remove text from the location name displayed on the map
        private string railPostFix = string.Empty;
        private string coachPostFix = string.Empty;
        private string railcoachPostFix = string.Empty;

        // Used for setting ferry/toll icon ITNNode on the map
        private string toidPrefix = string.Empty;

        // Used for obtaining a journey based on mode types (only used by city to city)
        ModeType[] modeTypes = null;
        
        // Leg to be shown on the initial map
        private int selectedLeg = -1;

        #endregion

        #region Initialise

        /// <summary>
        /// Initialises the map journey control with the properties specified
        /// </summary>
        /// <param name="outward">If true, map for outward journey is rendered
		/// otherwise map for return journey is rendered.</param>
        /// <param name="showMapTitle">Sets if the map title area should be shown, this area includes 
        /// the journey directions drop down list</param>
        /// <param name="showJourneySelectButtons">Indicates if the show outward or return journey buttons
        /// should be displayed</param>
        public void Initialise(bool outward, bool showMapTitle, bool showJourneySelectButtons)
        {
			this.outward = outward;
            this.showMapTitleArea = showMapTitle;
            this.showJourneySelectPanel = showJourneySelectButtons;
		}

        /// <summary>
        /// Initialises the map journey control with the properties specified
        /// </summary>
        /// <param name="outward">If true, map for outward journey is rendered
        /// otherwise map for return journey is rendered.</param>
        /// <param name="showMapTitle">Sets if the map title area should be shown, this area includes 
        /// the journey directions drop down list</param>
        /// <param name="mapHeight">IMPORTANT - this should only be set where the default size from 
        /// the map javascript config must be overidden</param>
        /// <param name="mapWidth">IMPORTANT - this should only be set where the default size from 
        /// the map javascript config must be overidden</param>
        /// <param name="showJourneySelectButtons">Indicates if the show outward or return journey buttons
        /// should be displayed</param>
        public void Initialise(bool outward, bool showMapTitle, bool showJourneySelectButtons, int mapHeight, int mapWidth)
        {
            Initialise(outward, showMapTitle, showJourneySelectButtons);

            this.mapHeight = mapHeight;
            this.mapWidth = mapWidth;
        }

		#endregion
        
        #region Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the session variables
            sessionManager = TDSessionManager.Current;
            itineraryManager = TDItineraryManager.Current;

            SetControlVariables();
            
            LoadProperties();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            LoadResources();

            InitialiseControls();

            SetJourneyDisplayNumber();

            SetControlVisibility();

            ResetPrintableSessionValues();

            RegisterMapAltTextJavaScript();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads text and image resources
        /// </summary>
        private void LoadResources()
        {
            labelMaps.Text = GetResource("JourneyMapControl.labelMaps.Text");
            labelMapsCar.Text = "(" + GetResource("JourneyMapControl.labelCar") + ")";

            if (outward)
                labelJourney.Text = GetResource("JourneyMapControl.labelOutwardJourney");
            else
                labelJourney.Text = GetResource("JourneyMapControl.labelReturnJourney");

            labelOptions.Text = GetResource("MapLocationControl.labelOptions.Text.OutputPublic");

            labelShowOutwardJourney.Text = GetResource("JourneyMapControl.labelOutwardJourney");
            labelShowReturnJourney.Text = GetResource("JourneyMapControl.labelReturnJourney");
            buttonShowOutwardJourney.Text = GetResource("JourneyMapControl.labelOutwardJourney");
            buttonShowReturnJourney.Text = GetResource("JourneyMapControl.labelReturnJourney");
        }

        /// <summary>
        /// Loads values from properties needed by the control
        /// </summary>
        private void LoadProperties()
        {
            railPostFix = Properties.Current["Gazetteerpostfix.rail"];
            coachPostFix = Properties.Current["Gazetteerpostfix.coach"];
            railcoachPostFix = Properties.Current["Gazetteerpostfix.railcoach"];

            toidPrefix = Properties.Current["JourneyControl.ToidPrefix"];
        }

        /// <summary>
        /// Sets variables needed by the control
        /// </summary>
        private void SetControlVariables()
        {
            SetUsingItineraryFlag();

            // Set the modes used in the display to retrieve journeys from result object, 
            // set by city to city otherwise will be null
            if (sessionManager.FindPageState != null)
                modeTypes = sessionManager.FindPageState.ModeType;
        }

        /// <summary>
        /// Sets the controls usingItinerary flags
        /// </summary>
        private void SetUsingItineraryFlag()
        {
            usingItinerary = ((itineraryManager.Length > 0) && (!itineraryManager.ExtendInProgress));
            itinerarySegmentSelected = ((usingItinerary) && (!itineraryManager.FullItinerarySelected));
        }

        private void SetMapAltText(string origin, string destination)
        {
            string mapAltText = GetResource("MapJourneyControl.MapAltText");
            if (!string.IsNullOrEmpty(mapAltText))
            {
                mapAltText = mapAltText.Replace(MAP_ALT_TEXT_ORIGIN_PATTERN, origin);
                mapAltText = mapAltText.Replace(MAP_ALT_TEXT_DESTINATION_PATTERN, destination);
                mapAltText = mapAltText.Replace("'", @"\'");
                mapControl.MapAltText = mapAltText;
            }
            else
            {
                mapControl.MapAltText = string.Empty;
            }
        }

        /// <summary>
        /// Method which sets the visibility of controls. Dependent on type of journey, and journey
        /// planner mode
        /// </summary>
        private void SetControlVisibility()
        {
            // Journey display number in heading/title area
            if ((sessionManager.FindAMode == FindAMode.Car) ||
                (sessionManager.FindAMode == FindAMode.Cycle))
            {
                labelDisplayNumber.Visible = false;
            }
            else
            {
                labelDisplayNumber.Visible = true;
            }

            // Car journey label is only shown if car journey is selected
            if (sessionManager.FindAMode == FindAMode.Car)
            {
                labelMapsCar.Visible = false;
            }
            else
            {
                bool showCarLabel = (outward && mapHelper.PrivateOutwardJourney)
                                    || (!outward && mapHelper.PrivateReturnJourney);

                labelMapsCar.Visible = showCarLabel;
            }

            // Set the show the map title area 
            mapTitleArea.Visible = showMapTitleArea;

            // Set the select outward/return journey visibility
            if (showJourneySelectPanel)
            {
                panelJourneySelectButtons.Visible = true;

                buttonShowOutwardJourney.Visible = !outward;
                buttonShowReturnJourney.Visible = outward;
                labelShowOutwardJourney.Visible = outward;
                labelShowReturnJourney.Visible = !outward;
            }
            else
            {
                panelJourneySelectButtons.Visible = false;
            }

        }

        /// <summary>
        /// Method which sets the journey display number label
        /// </summary>
        private void SetJourneyDisplayNumber()
        {
            string displayNumber = "-1";

            if (usingItinerary)
            {
                displayNumber = outward ? itineraryManager.OutwardDisplayNumber : itineraryManager.ReturnDisplayNumber;
            }
            else
			{
                ITDJourneyResult result = itineraryManager.JourneyResult;
                ITDCyclePlannerResult cycleResult = sessionManager.CycleResult;
                TDJourneyViewState viewState = itineraryManager.JourneyViewState;
                JourneySummaryLine summaryLine;

				// Get the selected public journey
				if(outward)
				{
					int selectedIndex = viewState.SelectedOutwardJourney;
					bool arriveBefore = viewState.JourneyLeavingTimeSearchType;
                    summaryLine = (sessionManager.FindAMode == FindAMode.Cycle) ?
                        cycleResult.OutwardJourneySummary(arriveBefore, modeTypes)[selectedIndex] :
                        result.OutwardJourneySummary(arriveBefore, modeTypes)[selectedIndex];
				}
				else
				{
					int selectedIndex = viewState.SelectedReturnJourney;
					bool arriveBefore = viewState.JourneyReturningTimeSearchType;
                    summaryLine = (sessionManager.FindAMode == FindAMode.Cycle) ?
                        cycleResult.ReturnJourneySummary(arriveBefore, modeTypes)[selectedIndex] :
                        result.ReturnJourneySummary(arriveBefore, modeTypes)[selectedIndex];
				}

				// Set journey display number text
				if(sessionManager.FindAMode != FindAMode.None)
				{
                    displayNumber = string.Empty;
				}
				else 
				{
                    displayNumber = summaryLine.DisplayNumber;
				}
			}

            if (displayNumber != "-1")
            {
                labelDisplayNumber.Text = displayNumber;
            }
        }

        /// <summary>
        /// If user has extended the journey then the journeys that make up the 
        /// Itinerary will be returned. If not then the single journey from 
        /// the SesssionManager will be returned.
        /// </summary>
        /// <returns>Array of Journey objects</returns>
        private Journey[] GetCurrentJourneys()
        {
            itineraryManager = TDItineraryManager.Current;
            sessionManager = TDSessionManager.Current;

            if (sessionManager.ItineraryMode != ItineraryManagerMode.None)
            {
                // Check if the map is for a journey where the extend is currently in progress
                if ((!itineraryManager.FullItinerarySelected) && (itineraryManager.ExtendInProgress))
                {
                    #region Get an extend in progress journey

                    // If so, get from the normal journey result
                    ITDJourneyResult journeyResult = itineraryManager.JourneyResult;
                    TDJourneyViewState viewState = itineraryManager.JourneyViewState;
                    Journey journey = null;

                    if (outward)
                    {
                        if (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal)
                        {
                            // the original journey has been selected
                            journey = journeyResult.OutwardPublicJourney(viewState.SelectedOutwardJourneyID);
                        }
                        else if (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended)
                        {
                            // the amended journey has been selected
                            journey = journeyResult.AmendedOutwardPublicJourney;
                        }
                        else if (viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested)
                        {
                            //private journey has been selected
                            journey = journeyResult.OutwardRoadJourney();
                        }
                    }
                    else
                    {
                        if (viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal)
                        {
                            // the original journey has been selected
                            journey = journeyResult.ReturnPublicJourney(viewState.SelectedReturnJourneyID);
                        }
                        else if (viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended)
                        {
                            // the amended journey has been selected
                            journey = journeyResult.AmendedReturnPublicJourney;
                        }
                        else if (viewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested)
                        {
                            //private journey has been selected
                            journey = journeyResult.ReturnRoadJourney();
                        }
                    }

                    if (journey != null)
                    {
                        return new Journey[] { journey };
                    }

                    #endregion
                }
                else
                {
                    // Full itinerary journey to be displayed
                    if (outward)
                    {
                        return itineraryManager.OutwardJourneyItinerary;
                    }
                    else
                    {
                        return itineraryManager.ReturnJourneyItinerary;
                    }
                }
            }
            else
            {
                TDJourneyViewState viewState = sessionManager.JourneyViewState;
                ITDJourneyResult result = sessionManager.JourneyResult;
                ITDCyclePlannerResult cycleResult = sessionManager.CycleResult;
                Journey journey = null;

                #region Determine the selected journey

                bool arriveBefore = outward ? viewState.JourneyLeavingTimeSearchType : viewState.JourneyReturningTimeSearchType;
                int selectedJourney = outward ? viewState.SelectedOutwardJourney : viewState.SelectedReturnJourney;

                JourneySummaryLine[] summaryLine;

                // get the summary line from the session Cycle result or Journey result
                if (sessionManager.FindAMode == FindAMode.Cycle)
                {
                    summaryLine = outward ? cycleResult.OutwardJourneySummary(arriveBefore, modeTypes) : cycleResult.ReturnJourneySummary(arriveBefore, modeTypes);
                }
                else
                {
                    summaryLine = outward ? result.OutwardJourneySummary(arriveBefore, modeTypes) : result.ReturnJourneySummary(arriveBefore, modeTypes);
                }

                if ((sessionManager.FindAMode == FindAMode.Trunk || sessionManager.FindAMode == FindAMode.TrunkStation) && sessionManager.FindPageState.ITPJourney)
                {
                    List<JourneySummaryLine> itpjourneys = new List<JourneySummaryLine>();

                    foreach (JourneySummaryLine jsl in summaryLine)
                    {
                        ArrayList journeyModes = new ArrayList();
                        journeyModes.AddRange(jsl.Modes);

                        bool coachmode = ((journeyModes.Contains(ModeType.Coach)) || (journeyModes.Contains(ModeType.Bus)));
                        bool trainmode = ((journeyModes.Contains(ModeType.Rail))
                                   ||
                                 (journeyModes.Contains(ModeType.RailReplacementBus))
                                   ||
                                 (journeyModes.Contains(ModeType.Metro))
                                   ||
                                 (journeyModes.Contains(ModeType.Tram))
                                   ||
                                 (journeyModes.Contains(ModeType.Underground)));

                        bool airmode = journeyModes.Contains(ModeType.Air);

                        if (airmode && (coachmode || trainmode))
                            itpjourneys.Add(jsl);
                        if (coachmode && trainmode && !airmode)
                            itpjourneys.Add(jsl);
                    }

                    summaryLine = itpjourneys.ToArray();
                }

                JourneySummaryLine summary = summaryLine[selectedJourney];

                #endregion

                // Use the summary lines list to get the correct journey
                if (outward)
                {
                    if (summary.Type == TDJourneyType.PublicOriginal)
                        journey = result.OutwardPublicJourney(summary.JourneyIndex);
                    else if (summary.Type == TDJourneyType.PublicAmended)
                        journey = result.AmendedOutwardPublicJourney;
                    else if (summary.Type == TDJourneyType.Cycle)
                        journey = cycleResult.OutwardCycleJourney(summary.JourneyIndex);
                    else
                        journey = result.OutwardRoadJourney();
                }
                else
                {
                    if (summary.Type == TDJourneyType.PublicOriginal)
                        journey = result.ReturnPublicJourney(summary.JourneyIndex);
                    else if (summary.Type == TDJourneyType.PublicAmended)
                        journey = result.AmendedReturnPublicJourney;
                    else if (summary.Type == TDJourneyType.Cycle)
                        journey = cycleResult.ReturnCycleJourney(summary.JourneyIndex);
                    else
                        journey = result.ReturnRoadJourney();
                }

                return new Journey[] { journey };
            }

            // If reached here, then no journeys foun
            return new Journey[0];
        }

        #region Initialise Map Controls

        /// <summary>
        /// Initialises the controls
        /// </summary>
        private void InitialiseControls()
        {
            // The journeys to show on the map
            Journey[] journeys = GetCurrentJourneys();

            // Initialise map with the journey
            InitialiseMap(journeys);

            // Initialise map symbols
            InitialiseMapSymbolsControl(journeys);

            // Initialise map key control
            InitialiseMapKeyControl();

            // Initialise journey drop down details control
            InitialiseMapJourneyDetailsDropDown();
        }

        /// <summary>
        /// Initialises the map control
        /// </summary>
        private void InitialiseMap(Journey[] journeys)
        {
            #region Variables needed to populate map

            sessionManager = TDSessionManager.Current;
            itineraryManager = TDItineraryManager.Current;
            ITDJourneyRequest request = null;
            JourneyLeg[] journeyLegs;
            JourneyControl.PublicJourney publicJourney;
            JourneyControl.RoadJourney roadJourney;
            CyclePlannerControl.CycleJourney cycleJourney;

            int noOfJourneys = 0;
            TDLocation startLocation = null;
            TDLocation endLocation = null;
            OSGridReference startOSGR = new OSGridReference();
            OSGridReference endOSGR = new OSGridReference();
            OSGridReference viaOSGR = new OSGridReference();
            string startDescription = string.Empty;
            string endDescription = string.Empty;
            int startType = 0;
            int endType = 0;
            string symbols = string.Empty;
            StringBuilder roadJourneyDirectionSymbols = new StringBuilder();
            StringBuilder cycleJourneyDirectionSymbols = new StringBuilder();
            
            bool journeyZoomToEnvelope = false;

            // Parameters to initialise the map with
            MapParameters mapParameters = InitialiseMapParameters();
            
            // Array to hold the location points to add on the map
            ArrayList mapLocationPoints = new ArrayList();

            // Journeys to show on map
            ArrayList mapJourneys = new ArrayList();

            // Set the number of journeys
            if (journeys != null)
            {
                noOfJourneys = journeys.Length;
            }

            #endregion

            try
            {
                for (int i = 0; i < noOfJourneys; i++)
                {
                    #region Determine start/end coordinates and description
                    // Clear locations so previously set one isnt re-used
                    startLocation = null;
                    endLocation = null;

                    journeyLegs = journeys[i].JourneyLegs;

                    if (journeyLegs[0].LegStart.Location.GridReference != null
                        && journeyLegs[0].LegStart.Location.GridReference.Easting > 0
                        && journeyLegs[0].LegStart.Location.GridReference.Northing > 0)
                    {
                        startLocation = journeyLegs[0].LegStart.Location;
                        startOSGR = journeyLegs[0].LegStart.Location.GridReference;
                        startDescription = journeyLegs[0].LegStart.Location.Description;

                        request = sessionManager.JourneyRequest;
                    }
                    else
                    {
                        if (sessionManager.ItineraryMode == ItineraryManagerMode.Replan)
                        {
                            if (i == 0)
                            {
                                if (outward)
                                {
                                    startLocation = ((ReplanItineraryManager)itineraryManager).OriginalRequest.OriginLocation;
                                    startOSGR = ((ReplanItineraryManager)itineraryManager).OriginalRequest.OriginLocation.GridReference;
                                    startDescription = ((ReplanItineraryManager)itineraryManager).OriginalRequest.OriginLocation.Description;
                                }
                                else
                                {
                                    startLocation = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnOriginLocation;
                                    startOSGR = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnOriginLocation.GridReference;
                                    startDescription = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnOriginLocation.Description;
                                }
                            }
                            else
                            {
                                startLocation = journeyLegs[0].LegStart.Location;
                                startOSGR = new OSGridReference();
                                startDescription = journeyLegs[0].LegStart.Location.Description;
                            }
                        }
                        else
                        {
                            request = itineraryManager.JourneyRequest;

                            if (outward)
                            {
                                startLocation = request.OriginLocation;
                                startOSGR = request.OriginLocation.GridReference;
                                startDescription = request.OriginLocation.Description;
                            }
                            else
                            {
                                startLocation = request.ReturnOriginLocation;
                                startOSGR = request.ReturnOriginLocation.GridReference;
                                startDescription = request.ReturnOriginLocation.Description;
                            }
                        }
                    }

                    if (journeyLegs[journeyLegs.Length - 1].LegEnd.Location.GridReference != null
                        && journeyLegs[journeyLegs.Length - 1].LegEnd.Location.GridReference.Easting > 0
                        && journeyLegs[journeyLegs.Length - 1].LegEnd.Location.GridReference.Northing > 0)
                    {
                        endLocation = journeyLegs[journeyLegs.Length - 1].LegEnd.Location;
                        endOSGR = journeyLegs[journeyLegs.Length - 1].LegEnd.Location.GridReference;
                        endDescription = journeyLegs[journeyLegs.Length - 1].LegEnd.Location.Description;

                        request = sessionManager.JourneyRequest;
                    }
                    else
                    {
                        if (sessionManager.ItineraryMode == ItineraryManagerMode.Replan)
                        {
                            if (i == (noOfJourneys - 1))
                            {
                                if (outward)
                                {
                                    endLocation = ((ReplanItineraryManager)itineraryManager).OriginalRequest.DestinationLocation;
                                    endOSGR = ((ReplanItineraryManager)itineraryManager).OriginalRequest.DestinationLocation.GridReference;
                                    endDescription = ((ReplanItineraryManager)itineraryManager).OriginalRequest.DestinationLocation.Description;
                                }
                                else
                                {
                                    endLocation = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnDestinationLocation;
                                    endOSGR = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnDestinationLocation.GridReference;
                                    endDescription = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnDestinationLocation.Description;
                                }
                            }
                            else
                            {
                                endLocation = journeyLegs[journeyLegs.Length - 1].LegEnd.Location;
                                endOSGR = new OSGridReference();
                                endDescription = journeyLegs[journeyLegs.Length - 1].LegEnd.Location.Description;
                            }
                        }
                        else
                        {
                            request = itineraryManager.JourneyRequest;

                            if (outward)
                            {
                                endLocation = request.DestinationLocation;
                                endOSGR = request.DestinationLocation.GridReference;
                                endDescription = request.DestinationLocation.Description;
                            }
                            else
                            {
                                endLocation = request.ReturnDestinationLocation;
                                endOSGR = request.ReturnDestinationLocation.GridReference;
                                endDescription = request.ReturnDestinationLocation.Description;
                            }
                        }
                    }
                    #endregion

                    if (journeys[i] is JourneyControl.PublicJourney)
                    {
                        #region Add PT Journey to map
                        publicJourney = (JourneyControl.PublicJourney)journeys[i];

                        // Add the route to the map.
                        UpdateJourneyRoute(ref mapJourneys, MapJourneyType.PublicTransport, Session.SessionID, publicJourney.RouteNum);

                        // Add location points to map.

                        JourneyLeg journeyLegFirst = publicJourney.JourneyLegs[0];
                        JourneyLeg journeyLegLast = publicJourney.JourneyLegs[publicJourney.JourneyLegs.Length - 1];

                        //Car Park outward and return journey
                        if ((journeyLegLast.LegEnd.Location.CarParking != null)
                            || (journeyLegFirst.LegStart.Location.CarParking != null))
                        {
                            #region Handling for car park locations

                            // Car Park journey can use Map, Entrance, or Exit coordinates to 
                            // plan a journey From/To. We always want to display the Map coordinate 
                            // as the Start/End point on the map

                            // Add location points to map, for start or via
                            startType = (i == 0) ? STARTPOINT : VIAPOINT;

                            // Scenario where user has planned From a Car Park
                            if (journeyLegFirst.LegStart.Location.CarParking != null)
                            {
                                CarPark carPark = journeyLegFirst.LegStart.Location.CarParking;
                                startOSGR = carPark.GetMapGridReference();
                                startLocation = journeyLegFirst.LegStart.Location;
                            }
                            
                            MapLocationPoint mapLocationPointStart = CreateStartEndViaPoint(startLocation, startOSGR, startDescription, startType);

                            if (mapLocationPointStart != null)
                            {
                                mapLocationPoints.Add(mapLocationPointStart);
                            }

                            //Add location points to map, for end or via
                            endType = (i == (noOfJourneys - 1)) ? ENDPOINT : VIAPOINT;

                            //Scenario where user has planned To a Car Park
                            if (journeyLegLast.LegEnd.Location.CarParking != null)
                            {
                                CarPark carPark = journeyLegLast.LegEnd.Location.CarParking;
                                endOSGR = carPark.GetMapGridReference();
                                endLocation = journeyLegLast.LegEnd.Location;
                            }
                            
                            MapLocationPoint mapLocationPointEnd = CreateStartEndViaPoint(endLocation, endOSGR, endDescription, endType);

                            if (mapLocationPointEnd != null)
                            {
                                mapLocationPoints.Add(mapLocationPointEnd);
                            }

                            #endregion
                        }
                        else
                        {
                            startType = (i == 0) ? STARTPOINT : VIAPOINT;

                            MapLocationPoint mapLocationPointStart = CreateStartEndViaPoint(startLocation, startOSGR, startDescription, startType);

                            if (mapLocationPointStart != null)
                            {
                                mapLocationPoints.Add(mapLocationPointStart);
                            }

                            endType = (i == (noOfJourneys - 1)) ? ENDPOINT : VIAPOINT;

                            MapLocationPoint mapLocationPointEnd = CreateStartEndViaPoint(endLocation, endOSGR, endDescription, endType);

                            if (mapLocationPointEnd != null)
                            {
                                mapLocationPoints.Add(mapLocationPointEnd);
                            }
                        }

                        // Check if any of the details are for a via location, and add point
                        foreach (PublicJourneyDetail pjd in publicJourney.Details)
                        {
                            if ((pjd.IncludesVia) && (pjd.ViaLocationOSGR != null) && (pjd.ViaLocationOSGR.IsValid))
                            {
                                MapLocationPoint mapLocationPointVia = null;
                                TDLocation viaLocation = null;
                                string description = string.Empty;

                                #region Get via location and description

                                if ((request != null) && (request.PublicViaLocations != null) 
                                    && (request.PublicViaLocations.Length == 1))
                                {
                                    // Identify the Via Location Naptan
                                    viaLocation = request.PublicViaLocations[0];

                                    string viaNaptan = string.Empty;
                                                                        
                                    if ((viaLocation.NaPTANs != null) && (viaLocation.NaPTANs.Length > 0))
                                    {
                                        viaNaptan = viaLocation.NaPTANs[0].Naptan;
                                    }

                                    bool found = false;

                                    // Find the display name from the journey legs returned by the CJP
                                    if (!string.IsNullOrEmpty(viaNaptan))
                                    {
                                        // Check the journey detail to see if it ends at the Via location
                                        if ((pjd.LegEnd.Location != null) && (pjd.LegEnd.Location.NaPTANs != null))
                                        {
                                            foreach (TDNaptan naptan in pjd.LegEnd.Location.NaPTANs)
                                            {
                                                if (naptan.Naptan == viaNaptan)
                                                {
                                                    description = pjd.LegEnd.Location.Description;
                                                    found = true;
                                                    break;
                                                }
                                            }
                                        }

                                        // Otherwise look through the intermediate calling points,
                                        // as this could be a single leg journey where the via location
                                        // is a calling point
                                        if (!found)
                                        {
                                            foreach (PublicJourneyCallingPoint pjcp in pjd.IntermediatesLeg)
                                            {
                                                if (pjcp.Location != null)
                                                {
                                                    foreach (TDNaptan naptan in pjcp.Location.NaPTANs)
                                                    {
                                                        if (naptan.Naptan == viaNaptan)
                                                        {
                                                            description = pjcp.Location.Description;
                                                            found = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        if (!found)
                                        {
                                            foreach (PublicJourneyCallingPoint pjcp in pjd.IntermediatesAfter)
                                            {
                                                if (pjcp.Location != null)
                                                {
                                                    foreach (TDNaptan naptan in pjcp.Location.NaPTANs)
                                                    {
                                                        if (naptan.Naptan == viaNaptan)
                                                        {
                                                            description = pjcp.Location.Description;
                                                            found = true;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                #endregion

                                if (viaLocation != null)
                                {
                                    // Pass location to allow info popup to be shown
                                    mapLocationPointVia = CreateStartEndViaPoint(viaLocation, pjd.ViaLocationOSGR,
                                        description,
                                        VIAPOINT);
                                }
                                
                                if (mapLocationPointVia != null)
                                {
                                    mapLocationPoints.Add(mapLocationPointVia);
                                }
                                break;
                            }
                        }

                        if (noOfJourneys == 1)
                        {
                            UpdateMapForPublicJourney(publicJourney, ref journeyZoomToEnvelope, ref mapParameters);
                        }

                        #endregion
                    }
                    else if (journeys[i] is CyclePlannerControl.CycleJourney)
                    {
                        #region Add Cycle Journey to map

                        cycleJourney = (CyclePlannerControl.CycleJourney)journeys[i];

                        // Update the map to display the selected cycle journey
                        UpdateJourneyRoute(ref mapJourneys, MapJourneyType.Cycle, Session.SessionID, cycleJourney.RouteNum);

                        // Add the start and end point text
                        startType = (i == 0) ? STARTPOINT : VIAPOINT;

                        MapLocationPoint mapLocationPointStart = CreateStartEndViaPoint(startLocation, startOSGR, startDescription, startType);

                        if (mapLocationPointStart != null)
                        {
                            mapLocationPoints.Add(mapLocationPointStart);
                        }

                        endType = (i == (noOfJourneys - 1)) ? ENDPOINT : VIAPOINT;

                        MapLocationPoint mapLocationPointEnd = CreateStartEndViaPoint(endLocation, endOSGR, endDescription, endType);

                        if (mapLocationPointEnd != null)
                        {
                            mapLocationPoints.Add(mapLocationPointEnd);
                        }

                        // Add the via location point if there is one
                        if ((cycleJourney.RequestedViaLocation != null) && (cycleJourney.RequestedViaLocation.Status == TDLocationStatus.Valid))
                        {
                            TDLocation viaLocation = cycleJourney.RequestedViaLocation;

                            MapLocationPoint mapLocationPointVia = CreateStartEndViaPoint(viaLocation, viaLocation.GridReference, viaLocation.Description, VIAPOINT);

                            if (mapLocationPointVia != null)
                            {
                                mapLocationPoints.Add(mapLocationPointVia);
                            }
                        }

                        #region Add Ferrys and Toll icons
                        
                        CycleJourneyDetail currentCycleDetail;

                        //This displays the Toll and Ferry Icons on the map
                        for (int a = 0; a < cycleJourney.Details.Length; a++)
                        {
                            currentCycleDetail = cycleJourney.Details[a];

                            //Only display if FerryExit/Entry or Toll Entry
                            if (currentCycleDetail.DisplayFerryIcon || currentCycleDetail.DisplayTollIcon)
                            {
                                string ITNToid = currentCycleDetail.NodeToid;

                                if (!string.IsNullOrEmpty(ITNToid))
                                {
                                    if (toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
                                    {
                                        ITNToid = ITNToid.Substring(toidPrefix.Length);
                                    }

                                    // Ferry or Toll icon
                                    string image = (currentCycleDetail.DisplayFerryIcon) ? "FERRY" : "TOLL";

                                    // Append the image type and toid to the symbols string
                                    if (symbols.Length > 0)
                                    {
                                        symbols += JS_SEPERATOR;
                                    }
                                    else
                                    {
                                        symbols += mapControl.MapID + JS_SEPERATOR;
                                    }

                                    symbols += JOURNEY_ADDSYMBOL_NODE + JS_SEPERATOR + image + JS_SEPERATOR + ITNToid;
                                }
                            }
                        }
                        
                        #endregion

                        #region Direction Icons

                        // Only show the direction icons if there is one journey, prevents scenario
                        // for extended journeys where the Intermediate legs (PT + Car) also show
                        // circle symbols with number - this would lead to incorrect symbol numbering.
                        if (journeys.Length == 1)
                        {
                            CycleJourneyDetail previousCycleDetail = null;
                            string directionText = string.Empty;
                            string addSymbolString = string.Empty;
                            TDJourneyViewState viewState = itineraryManager.JourneyViewState;

                            // Get the directions text, use email formatter as it contains no html
                            EmailCycleJourneyDetailFormatter defaultDetailFormatter = new EmailCycleJourneyDetailFormatter(
                                 cycleJourney,
                                 viewState,
                                 outward,
                                 RoadUnitsEnum.Miles, true, false);

                            // Direction instruction should be at index 2 in the list
                            IList cycleJourneyDetails = defaultDetailFormatter.GetJourneyDetails();

                            #region First direction symbol

                            // Add first direction symbol
                            currentCycleDetail = cycleJourney.Details[0];
                            
                            object[] details = (object[])cycleJourneyDetails[1];
                            directionText = (string)details[2];
                            directionText = directionText.Replace("<br />", ".");
                            directionText = Server.HtmlEncode(directionText);

                            addSymbolString = JOURNEY_ADDSYMBOL_POINT + JS_SEPERATOR
                                                + CYCLE_DIRECTION_NODE_IMAGE_PREFIX + JS_SEPERATOR
                                                //+ "CIRCLE2" + JS_SEPERATOR  // Commented out in case we need to reinstate numbered circles
                                                + currentCycleDetail.StartOSGR.Easting + JS_SEPERATOR
                                                + currentCycleDetail.StartOSGR.Northing + JS_SEPERATOR + directionText;

                            // Append the mapControl id to the start only, and update symbols string
                            cycleJourneyDirectionSymbols.Append(mapControl.MapID + JS_SEPERATOR);
                            cycleJourneyDirectionSymbols.Append(addSymbolString);

                            addSymbolString = string.Empty;

                            #endregion

                            // This displays the Cycle directions and symbols on the map.
                            // Start at the second detail because first detail is populated prior to 
                            // loop and we need previous and current details to work out correct coordinate
                            for (int a = 1; a < cycleJourney.Details.Length; a++)
                            {
                                currentCycleDetail = cycleJourney.Details[a];

                                if (a > 0)
                                {
                                    previousCycleDetail = cycleJourney.Details[a - 1];
                                }

                                // Get the direction text to show, should be at index 2
                                // First item in list is the "Start at ..." which we dont show here.
                                // Some directions can have a <br /> in it, remove this, and HTMLEncode for sanity 
                                // to remove any other html elements - which there shouldnt be
                                details = (object[])cycleJourneyDetails[a + 1];
                                directionText = (string)details[2];
                                directionText = directionText.Replace("<br />", ".");
                                directionText = Server.HtmlEncode(directionText);

                                #region Get coordinates

                                if (previousCycleDetail.StopoverSection && !currentCycleDetail.StopoverSection)
                                {
                                    // If the previous section was a StopoverSection, but the current one isn't
                                    // then it is the first drive section after a Stopover, however as a toll/ferry
                                    // symbol could be shown, only add the Circle direction if its a U-Turn

                                    // Allow Zoom to UTurn instruction
                                    if (string.IsNullOrEmpty(previousCycleDetail.NodeToid))
                                    {
                                        // This is a U-Turn as we don't have a Node
                                        // Use the OSGR of the section start instead.
                                        addSymbolString = JOURNEY_ADDSYMBOL_POINT + JS_SEPERATOR
                                                + CYCLE_DIRECTION_NODE_IMAGE_PREFIX + JS_SEPERATOR
                                                //+ "CIRCLE" + (a + 2) + JS_SEPERATOR  // Commented out in case we need to reinstate numbered circles
                                                + currentCycleDetail.StartOSGR.Easting + JS_SEPERATOR
                                                + currentCycleDetail.StartOSGR.Northing + JS_SEPERATOR + directionText;
                                    }
                                }
                                // If the current CycleJourneyDetail is not a StopoverSection
                                else if (!currentCycleDetail.StopoverSection)
                                {
                                    // If this detail is not a StopoverSection, then we need to zoom to 
                                    // the intersection coordinate of where the previous detail joins the current
                                    // detail

                                    // Variables to hold OSGR found
                                    OSGridReference intersectionOSGR = null;
                                    OSGridReference firstOSGR = null;

                                    mapHelper.GetIntersectionCoordinate(previousCycleDetail.Geometry, currentCycleDetail.Geometry,
                                        out intersectionOSGR, out firstOSGR);

                                    if (intersectionOSGR != null)
                                    {
                                        addSymbolString = JOURNEY_ADDSYMBOL_POINT + JS_SEPERATOR
                                                + CYCLE_DIRECTION_NODE_IMAGE_PREFIX + JS_SEPERATOR
                                                //+ "CIRCLE" + (a + 2) + JS_SEPERATOR  // Commented out in case we need to reinstate numbered circles
                                                + intersectionOSGR.Easting + JS_SEPERATOR
                                                + intersectionOSGR.Northing + JS_SEPERATOR + directionText;
                                    }
                                    else
                                    {
                                        addSymbolString = JOURNEY_ADDSYMBOL_POINT + JS_SEPERATOR
                                                + CYCLE_DIRECTION_NODE_IMAGE_PREFIX + JS_SEPERATOR
                                                //+ "CIRCLE" + (a + 2) + JS_SEPERATOR  // Commented out in case we need to reinstate numbered circles
                                                + firstOSGR.Easting + JS_SEPERATOR
                                                + firstOSGR.Northing + JS_SEPERATOR + directionText;
                                    }
                                }
                                // The current CycleJourneyDetail is a StopoverSection
                                else
                                {
                                    // Do not show direction symbol for any stop over sections, as a toll/ferry symbol could be shown

                                    // If the current section is a StopoverSection, then zoom to 
                                    // the ITNNode

                                    string ITNToid = string.Empty;
                                    if (string.IsNullOrEmpty(currentCycleDetail.NodeToid))
                                    {
                                        // We don't have a Node as this is a Point on a link being used as a via.
                                        //
                                        // Instead try to find the last point of the previous section as this should 
                                        // be the same Point as this Via. This is found in the last value in the array 
                                        // of geometry objects.
                                        //
                                        // We can then zoom to that Point.
                                        OSGridReference previousEndPoint = (OSGridReference)previousCycleDetail.Geometry[previousCycleDetail.Geometry.Count - 1].GetValue(previousCycleDetail.Geometry[previousCycleDetail.Geometry.Count - 1].Length - 1);

                                        addSymbolString = JOURNEY_ADDSYMBOL_POINT + JS_SEPERATOR
                                                + CYCLE_DIRECTION_NODE_IMAGE_PREFIX + JS_SEPERATOR
                                                //+ "CIRCLE" + (a + 2) + JS_SEPERATOR  // Commented out in case we need to reinstate numbered circles
                                                + previousEndPoint.Easting + JS_SEPERATOR
                                                + previousEndPoint.Northing + JS_SEPERATOR + directionText;
                                    }
                                    
                                }

                                #endregion

                                if (!string.IsNullOrEmpty(addSymbolString))
                                {
                                    cycleJourneyDirectionSymbols.Append(JS_SEPERATOR);
                                    cycleJourneyDirectionSymbols.Append(addSymbolString);

                                    // reset string
                                    addSymbolString = string.Empty;
                                }
                            }
                        }

                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region Add Road Journey to map
                        roadJourney = (JourneyControl.RoadJourney)journeys[i];

                        // Expand route for use
                        mapHelper.ExpandJourneyRouteInDatabase(Session.SessionID, roadJourney.RouteNum);
                        
                        // Update the map to show display the selected road journey
                        UpdateJourneyRoute(ref mapJourneys, MapJourneyType.Road, Session.SessionID, roadJourney.RouteNum);

                        JourneyLeg journeyLeg = roadJourney.JourneyLegs[0];

                        //Park and Ride outward journey
                        if (journeyLeg.LegEnd.Location.ParkAndRideScheme != null)
                        {
                            #region Park and Ride outward journey

                            // Add location points to map, for start or via
                            startType = (i == 0) ? STARTPOINT : VIAPOINT;

                            MapLocationPoint mapLocationPointStart = CreateStartEndViaPoint(startLocation, startOSGR, startDescription, startType);

                            if (mapLocationPointStart != null)
                            {
                                mapLocationPoints.Add(mapLocationPointStart);
                            }

                            //get CarPark details for journey TOID
                            ParkAndRideInfo parkAndRideInfo = journeyLeg.LegEnd.Location.ParkAndRideScheme;
                            journeyLeg.LegEnd.Location.CarPark = parkAndRideInfo.MatchCarPark(roadJourney.Details[roadJourney.Details.Length - 1].Toid);

                            if (journeyLeg.LegEnd.Location.CarPark != null)
                            {
                                //Add location points to map, for end or via
                                endType = (i == (noOfJourneys - 1)) ? ENDPOINT : VIAPOINT;

                                endLocation = journeyLeg.LegEnd.Location;

                                MapLocationPoint mapLocationPoint = CreateStartEndViaPoint(endLocation, journeyLeg.LegEnd.Location.CarPark.GridReference, journeyLeg.LegEnd.Location.CarPark.CarParkName, endType);

                                if (mapLocationPoint != null)
                                {
                                    mapLocationPoints.Add(mapLocationPoint);
                                }
                            }

                            UpdateMapForParkAndRideJourney(journeyLeg.LegStart.Location, journeyLeg.LegEnd.Location, roadJourney.RequestedViaLocation, 
                                ref journeyZoomToEnvelope, ref mapParameters, ref mapLocationPoints);

                            #endregion
                        }
                        //Park and Ride return journey
                        else if (journeyLeg.LegStart.Location.ParkAndRideScheme != null)
                        {
                            #region Park and Ride return journey

                            //get CarPark details for journey TOID
                            ParkAndRideInfo parkAndRideInfo = journeyLeg.LegStart.Location.ParkAndRideScheme;
                            journeyLeg.LegStart.Location.CarPark = parkAndRideInfo.MatchCarPark(roadJourney.Details[0].Toid);

                            // Add location points to map, for start or via
                            startType = (i == 0) ? STARTPOINT : VIAPOINT;

                            startLocation = journeyLeg.LegStart.Location;

                            MapLocationPoint mapLocationPointStart = CreateStartEndViaPoint(startLocation, journeyLeg.LegStart.Location.CarPark.GridReference, journeyLeg.LegStart.Location.CarPark.CarParkName, startType);

                            if (mapLocationPointStart != null)
                            {
                                mapLocationPoints.Add(mapLocationPointStart);
                            }

                            if (journeyLeg.LegStart.Location.CarPark != null)
                            {
                                //Add location points to map, for end or via
                                endType = (i == (noOfJourneys - 1)) ? ENDPOINT : VIAPOINT;

                                endLocation = journeyLeg.LegEnd.Location;

                                MapLocationPoint mapLocationPoint = CreateStartEndViaPoint(endLocation, journeyLeg.LegEnd.Location.GridReference, journeyLeg.LegEnd.Location.Description, endType);

                                if (mapLocationPoint != null)
                                {
                                    mapLocationPoints.Add(mapLocationPoint);
                                }
                            }

                            UpdateMapForParkAndRideJourney(journeyLeg.LegStart.Location, journeyLeg.LegEnd.Location, roadJourney.RequestedViaLocation,
                                ref journeyZoomToEnvelope, ref mapParameters, ref mapLocationPoints);

                            #endregion
                        }
                        //Car Park outward or return journey
                        else if ((journeyLeg.LegEnd.Location.CarParking != null)
                            || (journeyLeg.LegStart.Location.CarParking != null))
                        {
                            #region Handling for car park locations

                            // Car Park journey can use Map, Entrance, or Exit coordinates to 
                            // plan a journey From/To. We always want to display the Map coordinate 
                            // as the Start/End point on the map

                            // Add location points to map, for start or via
                            startType = (i == 0) ? STARTPOINT : VIAPOINT;

                            //Scenario where user has planned From a Car Park
                            if (journeyLeg.LegStart.Location.CarParking != null)
                            {
                                CarPark carPark = journeyLeg.LegStart.Location.CarParking;
                                startOSGR = carPark.GetMapGridReference();
                                startDescription = FindCarParkHelper.GetCarParkName(carPark);
                                startLocation = journeyLeg.LegStart.Location;
                            }

                            MapLocationPoint mapLocationPointStart = CreateStartEndViaPoint(startLocation, startOSGR, startDescription, startType);

                            if (mapLocationPointStart != null)
                            {
                                mapLocationPoints.Add(mapLocationPointStart);
                            }

                            //Add location points to map, for end or via
                            endType = (i == (noOfJourneys - 1)) ? ENDPOINT : VIAPOINT;

                            //Scenario where user has planned To a Car Park
                            if (journeyLeg.LegEnd.Location.CarParking != null)
                            {
                                CarPark carPark = journeyLeg.LegEnd.Location.CarParking;
                                endOSGR = carPark.GetMapGridReference();
                                endDescription = FindCarParkHelper.GetCarParkName(carPark);
                                endLocation = journeyLeg.LegEnd.Location;
                            }

                            MapLocationPoint mapLocationPointEnd = CreateStartEndViaPoint(endLocation, endOSGR, endDescription, endType);

                            if (mapLocationPointEnd != null)
                            {
                                mapLocationPoints.Add(mapLocationPointEnd);
                            }
                            
                            #endregion
                        }
                        else
                        {
                            // Add location points to map.
                            startType = (i == 0) ? STARTPOINT : VIAPOINT;

                            MapLocationPoint mapLocationPointStart = CreateStartEndViaPoint(startLocation, startOSGR, startDescription, startType);

                            if (mapLocationPointStart != null)
                            {
                                mapLocationPoints.Add(mapLocationPointStart);
                            }

                            endType = (i == (noOfJourneys - 1)) ? ENDPOINT : VIAPOINT;

                            MapLocationPoint mapLocationPointEnd = CreateStartEndViaPoint(endLocation, endOSGR, endDescription, endType);

                            if (mapLocationPointEnd != null)
                            {
                                mapLocationPoints.Add(mapLocationPointEnd);
                            }
                        }

                        // Via location point
                        if ((roadJourney.RequestedViaLocation != null) && (roadJourney.RequestedViaLocation.Status == TDLocationStatus.Valid))
                        {
                            TDLocation viaLocation = roadJourney.RequestedViaLocation;

                            MapLocationPoint mapLocationPointVia = CreateStartEndViaPoint(viaLocation, viaLocation.GridReference, viaLocation.Description, VIAPOINT);

                            if (mapLocationPointVia != null)
                            {
                                mapLocationPoints.Add(mapLocationPointVia);
                            }
                        }
                        
                        #region Add Ferrys and Toll, and RoadJourneyDirection icons

                        #region Ferrys/Tolls

                        RoadJourneyDetail currentRoadDetail = null;
                        
                        //This displays the Toll and Ferry Icons on the map
                        for (int a = 0; a < roadJourney.Details.Length; a++)
                        {
                            currentRoadDetail = roadJourney.Details[a];
                                                        
                            string itnNodeToid = currentRoadDetail.nodeToid;
                            
                            if (!string.IsNullOrEmpty(itnNodeToid))
                            {
                                if (toidPrefix.Length > 0 && itnNodeToid.StartsWith(toidPrefix))
                                {
                                    itnNodeToid = itnNodeToid.Substring(toidPrefix.Length);
                                }
                                
                                //Only display if FerryExit/Entry or Toll Entry
                                if (currentRoadDetail.displayFerryIcon || currentRoadDetail.displayTollIcon)
                                {

                                    // Ferry or Toll icon
                                    string image = (currentRoadDetail.displayFerryIcon) ? "FERRY" : "TOLL";

                                    // Append the image type and toid to the symbols string
                                    if (symbols.Length > 0)
                                    {
                                        symbols += JS_SEPERATOR;
                                    }
                                    else
                                    {
                                        symbols += mapControl.MapID + JS_SEPERATOR;
                                    }

                                    symbols += JOURNEY_ADDSYMBOL_NODE + JS_SEPERATOR + image + JS_SEPERATOR + itnNodeToid;
                                }
                            }
                        }

                        #endregion

                        #region Direction Icons

                        // Only show the direction icons if there is one journey, prevents scenario
                        // for extended journeys where the Intermediate legs (PT + Car) also show
                        // circle symbols with number - this would lead to incorrect symbol numbering.
                        if (journeys.Length == 1)
                        {
                            RoadJourneyDetail previousRoadDetail = null;
                            string firstITNToid = string.Empty;
                            string lastITNToid = string.Empty;
                            string directionText = string.Empty;
                            string addSymbolString = string.Empty;
                            TDJourneyViewState viewState = itineraryManager.JourneyViewState;

                            // Get the directions text, use email formatter as it contains no html
                            EmailCarJourneyDetailFormatter defaultDetailFormatter = new EmailCarJourneyDetailFormatter(
                                roadJourney,
                                viewState,
                                outward,
                                TDCultureInfo.CurrentUICulture,
                                RoadUnitsEnum.Miles, true);

                            // Direction instruction should be at index 1 in the list
                            IList carJourneyDetails = defaultDetailFormatter.GetJourneyDetails();

                            //This displays the Road directions and symbols on the map
                            for (int a = 0; a < roadJourney.Details.Length; a++)
                            {
                                currentRoadDetail = roadJourney.Details[a];

                                if (a > 0)
                                {
                                    previousRoadDetail = roadJourney.Details[a - 1];
                                }

                                // Get the direction text to show, should be at index 1.
                                // First item in list is the "Start at ..." which we dont show here
                                object[] details = (object[])carJourneyDetails[a + 1];
                                directionText = (string)details[1];

                                #region Get toids
                                if (a == 0)
                                {
                                    // This is the first direction, so add icon to the toid closest to the start of the journey
                                    firstITNToid = currentRoadDetail.RoadJourneyDetailMapInfo.firstToid;

                                    if (!string.IsNullOrEmpty(firstITNToid))
                                    {
                                        if (toidPrefix.Length > 0 && firstITNToid.StartsWith(toidPrefix))
                                        {
                                            firstITNToid = firstITNToid.Substring(toidPrefix.Length);
                                        }

                                        // Perform GIS query to get tehe closest point on the toid
                                        IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                                        Point pointForToid = gisQuery.FindNearestPointOnTOID(startOSGR.Easting, startOSGR.Northing, firstITNToid);

                                        // Create the add symbol string for this point
                                        addSymbolString = JOURNEY_ADDSYMBOL_POINT + JS_SEPERATOR
                                                + ROAD_DIRECTION_NODE_IMAGE_PREFIX + JS_SEPERATOR
                                                //+ "CIRCLE" + (a + 2) + JS_SEPERATOR // Commented out in case we need to reinstate displaying numbered circles
                                                + Convert.ToInt32(pointForToid.X) + JS_SEPERATOR
                                                + Convert.ToInt32(pointForToid.Y) + JS_SEPERATOR + directionText;
                                    }

                                }
                                // Do not show direction symbol for any stop over sections, as a toll/ferry symbol is shown
                                else if ((previousRoadDetail != null) && (!previousRoadDetail.IsStopOver) && (!currentRoadDetail.IsStopOver))
                                {
                                    firstITNToid = previousRoadDetail.RoadJourneyDetailMapInfo.lastToid;
                                    lastITNToid = currentRoadDetail.RoadJourneyDetailMapInfo.firstToid;

                                    if (!string.IsNullOrEmpty(firstITNToid) && !string.IsNullOrEmpty(lastITNToid))
                                    {
                                        if (toidPrefix.Length > 0 && firstITNToid.StartsWith(toidPrefix))
                                        {
                                            firstITNToid = firstITNToid.Substring(toidPrefix.Length);
                                        }

                                        if (toidPrefix.Length > 0 && lastITNToid.StartsWith(toidPrefix))
                                        {
                                            lastITNToid = lastITNToid.Substring(toidPrefix.Length);
                                        }

                                        // Create the add symbol string for these itns
                                        addSymbolString = JOURNEY_ADDSYMBOL_JUNCTION + JS_SEPERATOR
                                            + ROAD_DIRECTION_NODE_IMAGE_PREFIX + JS_SEPERATOR
                                            //+ "CIRCLE" + (a + 2) + JS_SEPERATOR  // Commented out in case we need to reinstate displaying numbered circles
                                            + firstITNToid + JS_SEPERATOR 
                                            + lastITNToid + JS_SEPERATOR + directionText;    
                                   }
                                }

                                #endregion

                                if (!string.IsNullOrEmpty(addSymbolString))
                                {
                                    // Append the mapControl id to the start only, and update symbols string
                                    if (roadJourneyDirectionSymbols.Length > 0)
                                    {
                                        roadJourneyDirectionSymbols.Append(JS_SEPERATOR);
                                    }
                                    else
                                    {
                                        roadJourneyDirectionSymbols.Append(mapControl.MapID + JS_SEPERATOR);
                                    }

                                    roadJourneyDirectionSymbols.Append(addSymbolString);

                                    // reset string
                                    addSymbolString = string.Empty;
                                }
                            }
                        }

                        #endregion

                        #endregion

                        #endregion
                    }

                } //foreach journey...

                #region Initialise map

                // Show intermediate nodes if journey is a public journey or an extended journey with full itinerary selected
                if ((outward && mapHelper.PublicOutwardJourney) || (!outward && mapHelper.PublicReturnJourney) || (usingItinerary && !itinerarySegmentSelected))
                {
                    mapLocationPoints.AddRange(ShowIntermediateNodesOnMap(outward));
                }

                // Set any icons which need to be added by the map javascript
                ShowFerryAndTollSymbolsOnMap(symbols);
                ShowRoadJourneyDirectionsOnMap(roadJourneyDirectionSymbols.ToString());
                ShowCycleJourneyDirectionsOnMap(cycleJourneyDirectionSymbols.ToString());

                // Update map location points to show walk it url at start location of walk legs
                if (sessionManager.ItineraryMode == ItineraryManagerMode.Replan)
                {
                    UpdateMapLocationsForWalkit(journeys, ((ReplanItineraryManager)itineraryManager).OriginalRequest, ref mapLocationPoints);
                }
                else
                {
                    UpdateMapLocationsForWalkit(journeys, itineraryManager.JourneyRequest, ref mapLocationPoints);
                }

                // Ensure map is zoomed to the journey(s) or the selected leg if set
                UpdateZoomLevel(journeys, journeyZoomToEnvelope, ref mapParameters);

                // Initialise the map with the parameters, journey, and location points to display
                mapControl.Initialise(
                    mapParameters, 
                    (MapLocationPoint[])mapLocationPoints.ToArray(typeof(MapLocationPoint)),
                    (MapJourney[])mapJourneys.ToArray(typeof(MapJourney)));

                #endregion

            }
            catch (Exception ex)
            {
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "Exception intialising map. " + ex.Message);

                Logger.Write(operationalEvent);

                // Initialise a default map
                mapControl.Initialise();
            }
            SetMapAltText(startDescription, endDescription);
        }

       
        /// <summary>
        /// Initialises the map symbols control
        /// </summary>
        private void InitialiseMapSymbolsControl(Journey[] journeys)
        {
            // Initialise map symbols
            mapSymbolsSelectControl.MapId = mapControl.MapID;

            mapSymbolsSelectControl.ShowTravelNews = true;

            if (journeys.Length > 0)
            {
                if (journeys[0].JourneyLegs.Length > 0)
                {
                    mapSymbolsSelectControl.TravelNewsDate = journeys[0].JourneyLegs[0].StartTime;
                }
            }

            // Set symbols selected by default for journey type
            if (outward)
            {
                // Check for PT first because this shows all symbols
                if (mapHelper.PublicOutwardJourney)
                {
                    mapSymbolsSelectControl.CheckTransportSectionSymbols();
                }
                else if (mapHelper.PrivateOutwardJourney)
                {
                    mapSymbolsSelectControl.CheckTransportSectionSymbolsForCar();
                }
                else // Default is to show all
                {
                    mapSymbolsSelectControl.CheckTransportSectionSymbols();
                }
            }
            else
            {
                // Check for PT first because this shows all symbols
                if (mapHelper.PublicReturnJourney)
                {
                    mapSymbolsSelectControl.CheckTransportSectionSymbols();
                }
                else if (mapHelper.PrivateReturnJourney)
                {
                    mapSymbolsSelectControl.CheckTransportSectionSymbolsForCar();
                }
                else // Default is to show all
                {
                    mapSymbolsSelectControl.CheckTransportSectionSymbols();
                }
            }
        }

        /// <summary>
        /// Initialises the map key control
        /// </summary>
        private void InitialiseMapKeyControl()
        {
            if (itineraryManager.FullItinerarySelected)
            {
                mapJourneyDisplayDetailsControl.UsingItinerary = true;

                // Check if we are dealing with Visit Planner results - has own key
                if (sessionManager.ItineraryMode == ItineraryManagerMode.VisitPlanner)
                {
                    mapKeyControl.InitialiseVisitPlan(false, mapHelper.HasJourneyGreyedOutMode(outward));
                }
                else
                {
                    mapKeyControl.InitialiseMixed(false, mapHelper.HasJourneyGreyedOutMode(outward));
                }
            }
            else
            {
                mapJourneyDisplayDetailsControl.UsingItinerary = usingItinerary;

                if (outward)
                {
                    #region Outward journey

                    //This is the outward journey so set the state of the journey details drop down control
                    mapJourneyDisplayDetailsControl.PublicJourney = mapHelper.PublicOutwardJourney;

                    if (!sessionManager.IsFindAMode)
                    {
                        if (mapHelper.PublicOutwardJourney)
                        {
                            mapKeyControl.InitialisePublic(false, mapHelper.HasJourneyGreyedOutMode(outward));
                        }
                        else
                        {
                            mapKeyControl.InitialisePrivate(false, false);
                        }
                    }
                    else
                    {
                        if (sessionManager.FindPageState.Mode == FindAMode.Car)
                        {
                            mapKeyControl.InitialisePrivate(false, false);
                        }
                        else if (sessionManager.FindPageState.Mode == FindAMode.Cycle)
                        {
                            mapKeyControl.InitialiseCycle(false);
                        }
                        else if (sessionManager.FindPageState.Mode == FindAMode.EnvironmentalBenefits)
                        {
                            mapKeyControl.InitialiseEBC(false);
                        }
                        else
                        {
                            mapKeyControl.InitialiseSpecificModes(mapHelper.FindUsedModes(true, usingItinerary), false, mapHelper.HasJourneyGreyedOutMode(outward));
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Return journey

                    //This is the return journey so set the state of the journey details control
                    mapJourneyDisplayDetailsControl.PublicJourney = mapHelper.PublicReturnJourney;

                    if (!sessionManager.IsFindAMode)
                    {
                        if (mapHelper.PublicReturnJourney)
                        {
                            mapKeyControl.InitialisePublic(false, mapHelper.HasJourneyGreyedOutMode(outward));
                        }
                        else
                        {
                            mapKeyControl.InitialisePrivate(false, false);
                        }
                    }
                    else
                    {
                        if (sessionManager.FindPageState.Mode == FindAMode.Car)
                        {
                            mapKeyControl.InitialisePrivate(false, false);
                        }
                        else if (sessionManager.FindPageState.Mode == FindAMode.Cycle)
                        {
                            mapKeyControl.InitialiseCycle(false);
                        }
                        else
                        {
                            mapKeyControl.InitialiseSpecificModes(mapHelper.FindUsedModes(false, usingItinerary), false, mapHelper.HasJourneyGreyedOutMode(outward));
                        }
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// Initialises the control which displays a drop down list of the journey detail segments
        /// </summary>
        private void InitialiseMapJourneyDetailsDropDown()
        {
            // Only do this if displaying the map title area
            if (showMapTitleArea)
            {
                // Set the propeties needed by the javascript used on the control
                mapJourneyDisplayDetailsControl.EnableJavascript = true;
                mapJourneyDisplayDetailsControl.MapId = mapControl.MapID;
                mapJourneyDisplayDetailsControl.SessionId = Session.SessionID;

                // Add the journey segments
                mapJourneyDisplayDetailsControl.ClearSelection();

                if (usingItinerary && !itinerarySegmentSelected)
                {
                    mapJourneyDisplayDetailsControl.PopulateJourneySegments(outward, usingItinerary, itineraryManager.Length);
                }
                else
                {
                    mapJourneyDisplayDetailsControl.PopulateJourneySegments(outward, usingItinerary, 1);
                }

                // Set the selected leg after dropdownlist is populated, this will likely only be set 
                // if on the journey details page, and the user has selected the map icon/button for a leg.
                // So need to ensure the dropdownlist shows the user selected map leg
                if (selectedLeg >= 0)
                {
                    // Add 1 because first item in drop down list is "map of entire journey"
                    mapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex = selectedLeg + 1;
                }
            }
        }

        #endregion

        #region Map helper methods

        /// <summary>
        /// Initialises a new MapParameters object to be used for a journey map
        /// </summary>
        /// <returns></returns>
        private MapParameters InitialiseMapParameters()
        {
            MapParameters mapParameters = new MapParameters();

            // Show no toolbars other than pan and zoom
            mapParameters.MapToolbarTools = new MapToolbarTool[2] { MapToolbarTool.Pan, MapToolbarTool.Zoom };

            // Show no "plan a journey" links on the info popup
            mapParameters.MapLocationModes = new MapLocationMode[1] { MapLocationMode.None };

            // No "plan a journey" text needed
            mapParameters.MapInfoDisplayText = string.Empty;

            // Set the size if provided
            if ((mapHeight > 0) && (mapWidth > 0))
            {
                mapParameters.MapHeight = mapHeight;
                mapParameters.MapWidth = mapWidth;
            }

            // Map scale and envelope are automatically set when journey is added

            return mapParameters;
        }

        /// <summary>
        /// Adds the journey to the MapJourney array to be added to the Map control
        /// </summary>
        private void UpdateJourneyRoute(ref ArrayList mapJourneys, MapJourneyType mapJourneyType, string sessionId, int routeNumber)
        {
            // Set the journey route number on the map details drop down control - to allow zooming on a route
            mapJourneyDisplayDetailsControl.JourneyRouteNumber = routeNumber;

            // Set the journey type on the map details drop down - to allow zooming on the route
            mapJourneyDisplayDetailsControl.JourneyType = mapControl.GetMapJourneyTypeString(mapJourneyType);

            // Create the journey to be shown on the map
            MapJourney mapJourney = new MapJourney(sessionId, routeNumber, mapJourneyType);

            mapJourneys.Add(mapJourney);
        }

        /// <summary>
        /// Update the map to the correct zoom level
        /// </summary>
        private void UpdateZoomLevel(Journey[] journeys, bool zoomToEnvelope, ref MapParameters mapParameters)
        {
            if (journeys.Length > 1)
            {
                if (selectedLeg >= 0)
                {
                    UpdateMapForItineraryJourney(journeys, ref mapParameters);
                }
                else
                {
                    // Zoom to all added journeys.
                    // This is automatically done by the map
                    //RegisterJourneyMapZoomToAllRoutes();
                }
            }
            else if (!zoomToEnvelope)
            {
                // Only zoom to journey if an envelope hasnt been set.
                // This is automatically done by the map
            }
            else
            {
                // Zooming to envelope.
                // This is done when calling the relevant UpdateMapFor... method
            }
        }

        /// <summary>
        /// Creates a location point (for start/end/via points) to add to the map for given coordinates.
        /// Returns null if osgr is invalid.
        /// </summary>
        /// <param name="osgr">OSGR of location</param>
        /// <param name="description">Description of location</param>
        /// <param name="type">Type of location</param>
        private MapLocationPoint CreateStartEndViaPoint(TDLocation location, OSGridReference osgr, string description, int type)
        {
            // Strip out any sub strings denoting pseudo locations
            StringBuilder strName = new StringBuilder(description);
            strName.Replace(railPostFix, "");
            strName.Replace(coachPostFix, "");
            strName.Replace(railcoachPostFix, "");
            description = strName.ToString();

            // Only return a location point if valid
            if ((osgr != null) && (osgr.IsValid))
            {
                string content = string.Empty;

                // Set up the links to show, only if the location has a single naptan, otherwise the popup
                // can be too cluttered with links, e.g. London
                if ((location != null) && (location.NaPTANs != null) && (location.NaPTANs.Length == 1))
                {
                    content = mapHelper.GetStopInformationLinks(location);
                }
                
                bool allowPopup = !string.IsNullOrEmpty(content);

                switch (type)
                {
                    case STARTPOINT:
                        return new MapLocationPoint(osgr, MapLocationSymbolType.Start, null, description, allowPopup, false, content);
                    case ENDPOINT:
                        return new MapLocationPoint(osgr, MapLocationSymbolType.End, null, description, allowPopup, false, content);
                    case VIAPOINT:
                        return new MapLocationPoint(osgr, MapLocationSymbolType.Via, null, description, allowPopup, false, content);
                }
            }

            return null;
        }

        /// <summary>
        /// Creates location points to be shown as icons on the map that number the legs of the journey
        /// </summary>
        /// <param name="outward"></param>
        /// <param name="usingItinerary"></param>
        private MapLocationPoint[] ShowIntermediateNodesOnMap(bool outward)
        {
            ArrayList mapLocationPoints = new ArrayList();

            // Add a symbol at each intermediate node
            OSGridReference[] journeyIntermediateNodesGridReferencesArray = mapHelper.FindIntermediateNodesGridReferences(outward);

            int nodeCount = journeyIntermediateNodesGridReferencesArray.Length;
            if (nodeCount == 0)
            {
                // No nodes to display - probably a road map
            }
            else
            {
                int index = 0;

                foreach (OSGridReference osGridReference in journeyIntermediateNodesGridReferencesArray)
                {
                    if (osGridReference.IsValid)
                    {
                        if (journeyIntermediateNodesGridReferencesArray[index] != null)
                        {                           
                            int iconNumber = index + 1;
                            string iconName = INTERMEDIATE_NODE_IMAGE_PREFIX + iconNumber.ToString(TDCultureInfo.InvariantCulture.NumberFormat);

                            // Create location point to add to the map
                            mapLocationPoints.Add(new MapLocationPoint(osGridReference, MapLocationSymbolType.Custom, iconName, string.Empty, false, false));

                            index++;
                        }
                        else
                        {
                            // No more grid references
                            break;
                        }
                    }
                    else
                    {
                        // Skip the addition of this icon
                        index++;
                        continue;
                    }
                } // End foreach
            }

            return (MapLocationPoint[])mapLocationPoints.ToArray(typeof(MapLocationPoint));
        }

        /// <summary>
        /// Adds a hidden field to the control containing a list of symbols to be displayed.
        /// The symbols hidden field is then used by a javascript function addFerryAndTollSymbols() 
        /// to parse and add the symbols to the map
        /// </summary>
        private void ShowFerryAndTollSymbolsOnMap(string symbols)
        {
            if (!string.IsNullOrEmpty(symbols))
            {
                HiddenField hdnSymbols = new HiddenField();

                hdnSymbols.ID = "FerryAndTollSymbols";
                hdnSymbols.Value = symbols;

                this.Controls.Add(hdnSymbols);

                // Add the javascript which renders the Ferry and Toll symbols
                RegisterFerryAndTollJavaScript();
            }
        }

        /// <summary>
        /// Adds a hidden field to the control containing a list of symbols to be displayed.
        /// The symbols hidden field is then used by a javascript function addRoadJourneyDirectionSymbols() 
        /// to parse and add the symbols to the map
        /// </summary>
        private void ShowRoadJourneyDirectionsOnMap(string symbols)
        {
            if (!string.IsNullOrEmpty(symbols))
            {
                HiddenField hdnSymbols = new HiddenField();

                hdnSymbols.ID = "RoadJourneyDirectionSymbols";
                hdnSymbols.Value = symbols;

                this.Controls.Add(hdnSymbols);

                // Add the javascript which renders the road journey directions
                RegisterRoadJourneyJavaScript();
            }
        }

        /// <summary>
        /// Adds a hidden field to the control containing a list of symbols to be displayed.
        /// The symbols hidden field is then used by a javascript function addCycleJourneyDirectionSymbols() 
        /// to parse and add the symbols to the map
        /// </summary>
        private void ShowCycleJourneyDirectionsOnMap(string symbols)
        {
            if (!string.IsNullOrEmpty(symbols))
            {
                HiddenField hdnSymbols = new HiddenField();

                hdnSymbols.ID = "CycleJourneyDirectionSymbols";
                hdnSymbols.Value = symbols;

                this.Controls.Add(hdnSymbols);

                // Add the javascript which renders the road journey directions
                RegisterCycleJourneyJavaScript();
            }
        }

        #region Update map for journey type

        /// <summary>
        /// Method to update the map parameters to show the public journey at a selected leg, setting the 
        /// map parameters to zoom the journey to a map envelope. 
        /// </summary>
        /// <param name="publicJourney"></param>
        /// <param name="journeyZoomToEnvelope"></param>
        /// <param name="mapParameters"></param>
        private void UpdateMapForPublicJourney(JourneyControl.PublicJourney publicJourney,
            ref bool journeyZoomToEnvelope, ref MapParameters mapParameters)
        {
            // Only need to set the zoom enevelope if a specific leg is to be shown
            if (selectedLeg >= 0)
            {
                if ((publicJourney.Details.Length - 1) >= selectedLeg)
                {
                    PublicJourneyDetail pjd = publicJourney.Details[selectedLeg];

                    if (!pjd.HasInvalidCoordinates)
                    {
                        OSGridReference[] osgr = pjd.Geometry;
                        if (osgr.Length > 0)
                        {
                            // Create the map envelope using the osgrids for the public journey detail
                            OSGridReference[] osgrEnvelope = mapHelper.CreateMapEnvelope(osgr);

                            // Add map envelope for map to zoom to leg
                            mapParameters.MapEnvelopeMin = osgrEnvelope[0];
                            mapParameters.MapEnvelopeMax = osgrEnvelope[1];
                            
                            // Set flag to zoom to the envelope of the selected leg
                            journeyZoomToEnvelope = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to update the map parameters with a symbol represtenting the Park and Ride destination and to 
        /// Create a map envelope around the entire journey. 
        /// </summary>
        /// <param name="originLocation">used to provide the grid reference of the origin</param>
        /// <param name="destinationLocation">used to provide the grid reference of the destination</param>
        /// <param name="privateViaLocation">used to provide the grid reference of the via point</param>
        private void UpdateMapForParkAndRideJourney(TDLocation originLocation, TDLocation destinationLocation, TDLocation privateViaLocation,
            ref bool journeyZoomToEnvelope, ref MapParameters mapParameters, ref ArrayList mapLocationPoints)
        {
            if (outward)
            {
                //Origin journey: create the symbol for the destination of the park and ride scheme. 
                MapLocationPoint mapLocationPoint = new MapLocationPoint(destinationLocation.ParkAndRideScheme.SchemeGridReference,
                    MapLocationSymbolType.Circle, destinationLocation.ParkAndRideScheme.Location.ToString(), false, false);

                // Add to the array of location points to draw on the map
                mapLocationPoints.Add(mapLocationPoint);

                OSGridReference[] osGridArray = {originLocation.GridReference,
													 destinationLocation.CarPark.GridReference,
													 destinationLocation.ParkAndRideScheme.SchemeGridReference,
													 ((privateViaLocation !=null) ? privateViaLocation.GridReference : null)
												 };
                if (osGridArray.Length == 0)
                    return;

                // Create the map envelope using the osgrids that are provided. 
                OSGridReference[] osgrEnvelope = mapHelper.CreateMapEnvelope(osGridArray);

                // Add map envelope for map to zoom to leg
                mapParameters.MapEnvelopeMin = osgrEnvelope[0];
                mapParameters.MapEnvelopeMax = osgrEnvelope[1];

            }
            else
            {
                //Return journey: create the symbol for the origin of the park and ride scheme. 
                MapLocationPoint mapLocationPoint = new MapLocationPoint(originLocation.ParkAndRideScheme.SchemeGridReference,
                    MapLocationSymbolType.Circle, originLocation.ParkAndRideScheme.Location.ToString(), false, false);

                // Add to the array of location points to draw on the map
                mapLocationPoints.Add(mapLocationPoint);

                OSGridReference[] osGridArray = {destinationLocation.GridReference,
													 originLocation.CarPark.GridReference,
													 originLocation.ParkAndRideScheme.SchemeGridReference,
													 ((privateViaLocation !=null) ? privateViaLocation.GridReference : null)
												 };
                if (osGridArray.Length == 0)
                    return;
                
                // Create the map envelope using the osgrids that are provided. 
                OSGridReference[] osgrEnvelope = mapHelper.CreateMapEnvelope(osGridArray);

                // Add map envelope for map to zoom to leg
                mapParameters.MapEnvelopeMin = osgrEnvelope[0];
                mapParameters.MapEnvelopeMax = osgrEnvelope[1];

            }

            // Set flag because for Park and Ride we want to zoom to our defined envelope
            journeyZoomToEnvelope = true;
        }

        /// <summary>
        /// Method to update the map parameters to show the itinerary journey at a selected leg, setting the 
        /// map parameters to zoom the journey to a map envelope. 
        /// </summary>
        /// <param name="publicJourney"></param>
        /// <param name="journeyZoomToEnvelope"></param>
        /// <param name="mapParameters"></param>
        private void UpdateMapForItineraryJourney(Journey[] journeys, ref MapParameters mapParameters)
        {
            // Only need to set the zoom enevelope if a specific leg is to be shown,
            // otherwise the map will be zoomed to show all the journey(s) added to the map
            if (selectedLeg >= 0)
            {
                // Variable to hold the coordinates to work out the envelope
                OSGridReference[] osgrs = new OSGridReference[0];

                // The selected leg index will have been set to the logical index i.e. when all journeys and 
                // their legs are joined together. So need to move through the legs until we hit 
                // the selected leg.
                int legsCount = 0;
                int previousLegsCount = 0;
                for (int i = 0; i < journeys.Length; i++)
                {
                    Journey journey = journeys[i];

                    legsCount += journey.JourneyLegs.Length;

                    // This journey covers the leg to display
                    if (legsCount > selectedLeg)
                    {
                        int actualLeg = selectedLeg - previousLegsCount;

                        if (journey is JourneyControl.PublicJourney)
                        {
                            // Its a public journey, so set the initial display to a zoom envelope
                            JourneyControl.PublicJourney pj = journey as JourneyControl.PublicJourney;
                            JourneyControl.PublicJourneyDetail pjd = pj.Details[actualLeg];

                            if (!pjd.HasInvalidCoordinates)
                            {
                                osgrs = pjd.Geometry;
                            }
                        }
                        else if (journey is JourneyControl.RoadJourney)
                        {
                            // Its a road journey, so zoom the map to the whole of the road journey
                            JourneyControl.RoadJourney rj = journey as JourneyControl.RoadJourney;

                            OSGridReference legStartOSGR = rj.JourneyLegs[0].LegStart.Location.GridReference;
                            OSGridReference legEndOSGR = rj.JourneyLegs[0].LegEnd.Location.GridReference;

                            osgrs = new OSGridReference[2] { legStartOSGR, legEndOSGR };
                        }

                        break;
                    }

                    previousLegsCount += journey.JourneyLegs.Length;
                }

                if (osgrs.Length > 0)
                {
                    //create the map envelope using the osgrids for the journey detail to show
                    OSGridReference[] osgrEnvelope = mapHelper.CreateMapEnvelope(osgrs);

                    // Add map envelope for map to zoom to leg
                    mapParameters.MapEnvelopeMin = osgrEnvelope[0];
                    mapParameters.MapEnvelopeMax = osgrEnvelope[1];

                }
            }
        }

        #endregion

        #region Update map location points for walk journey legs
        /// <summary>
        /// Updates map location points to pass walkit journey link in information window popup for the location symbols
        /// </summary>
        /// <param name="journeys"></param>
        /// <param name="request"></param>
        /// <param name="mapLocationPoints"></param>
        private void UpdateMapLocationsForWalkit(Journey[] journeys, ITDJourneyRequest request, ref ArrayList mapLocationPoints)
        {
            if (journeys == null)
                return;
            
            //iterate though all the journey to find walk journey legs
            foreach (Journey journey in journeys)
            {
                if (IsJourneyContainsWalk(journey))
                {
                    int legIndex = 0;
                    foreach (JourneyLeg leg in journey.JourneyLegs)
                    {
                        if (leg.Mode == ModeType.Walk)
                        {
                            // if journey leg is a walk load and add walkit link
                            WalkitURLHandoffHelper walkitHelper = new WalkitURLHandoffHelper(journey, leg, legIndex, request, outward);

                            string walkitURL = walkitHelper.GetWalkitHandoffURL();

                            string walkitLink = mapHelper.GetWalkitLink(walkitURL);

                            if (!string.IsNullOrEmpty(walkitLink))
                            {
                                string stopInformationLink = mapHelper.GetStopInformationLinks(leg.LegStart.Location);
                                
                                string walkitPopupLink = string.Format("<div>{0}</div>", walkitLink);

                                if (!string.IsNullOrEmpty(stopInformationLink))
                                {
                                    walkitPopupLink = string.Format("<div>{0}<br/>{1}</div>", stopInformationLink, walkitLink);
                                }

                                int locationId = 0;
                                foreach (Object location in mapLocationPoints)
                                {
                                    MapLocationPoint mapLocation = location as MapLocationPoint;

                                    OSGridReference legGridRef = leg.LegStart.Location.GridReference;

                                    if (mapLocation.MapLocationOSGR.Easting == legGridRef.Easting
                                        && mapLocation.MapLocationOSGR.Northing == legGridRef.Northing)
                                    {
                                        mapLocation.Content = walkitPopupLink;
                                        if (string.IsNullOrEmpty(mapLocation.MapLocationDescription))
                                        {
                                            mapLocation.MapLocationDescription = leg.LegStart.Location.Description;
                                        }

                                        mapLocation.MapInfoPopupRequired = true;
                                        mapLocationPoints.RemoveAt(locationId);
                                        mapLocationPoints.Insert(locationId, mapLocation);
                                        break;
                                    }
                                    locationId++;
                                }
                            }
                            legIndex++;
                        }
                    }
                }
            }

        }
        
        /// <summary>
        /// Checks if the journey got any walk leg
        /// Returns true if walk journey leg found in the journey
        /// </summary>
        /// <param name="journey"></param>
        /// <returns>True if walk leg found in journey</returns>
        private bool IsJourneyContainsWalk(Journey journey)
        {
            bool hasWalk = false;

            if (journey != null)
            {
                foreach (ModeType mode in journey.GetUsedModes())
                {
                    if (mode == ModeType.Walk)
                    {
                        hasWalk = true;
                    }
                }
            }

            return hasWalk;
        }


        #endregion

        /// <summary>
        /// Resets the session variables used by the printable version of map, used by 
        /// the Printer Friendly page.
        /// </summary>
        private void ResetPrintableSessionValues()
        {
            if (!Page.IsPostBack)
            {
                string journeyTextToShow = GetResource("DataServices.MapsForThisJourneyDrop.FullJourney");

                mapHelper.ResetPrintableMapSessionValues(true, journeyTextToShow);
                mapHelper.ResetPrintableMapSessionValues(false, journeyTextToShow);
            }
        }

        #region Register Javascript dojo event methods

        private void RegisterMapAltTextJavaScript()
        {
            string mapAltText = this.mapControl.MapAltText;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MapAltText", "InitMapAltText('" + mapAltText + "');", true);
        }

        /// <summary>
        /// Method which registers any javascript files needed a road journey map
        /// </summary>
        private void RegisterFerryAndTollJavaScript()
        {
            string dojoFerryAndTollScript = "dojo.subscribe(\"Map\",function(mapArgs){ addFerryAndTollSymbols(); });";

            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "dojoFerryAndTollScript", dojoFerryAndTollScript, true);
        }

        /// <summary>
        /// Method which registers any javascript files needed a road journey map
        /// </summary>
        private void RegisterRoadJourneyJavaScript()
        {
            string dojoRoadJourneyScript = "dojo.subscribe(\"Map\",function(mapArgs){ addRoadJourneyDirectionSymbols('" + mapControl.MapID + "', mapArgs); });";

            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "dojoRoadJourneyScript", dojoRoadJourneyScript, true);
        }

        /// <summary>
        /// Method which registers any javascript files needed a cycle journey map
        /// </summary>
        private void RegisterCycleJourneyJavaScript()
        {
            string dojoCycleJourneyScript = "dojo.subscribe(\"Map\",function(mapArgs){ addCycleJourneyDirectionSymbols('" + mapControl.MapID + "', mapArgs); });";

            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "dojoCycleJourneyScript", dojoCycleJourneyScript, true);
        }
        
        /// <summary>
        /// Method which registers javascript to fire once the map has initialised, to zoom the journey to an OSGR envelope
        /// </summary>
        private void RegisterJourneyMapZoomToEnvelopeJavaScript(OSGridReference[] osgrEnvelope)
        {
            StringBuilder dojoJourneyMapZoomScript = new StringBuilder();

            dojoJourneyMapZoomScript.Append("dojo.subscribe(\"mapInitialised\",function(mapArgs){ ");
            dojoJourneyMapZoomScript.Append("eventZoomJourneyToEnvelope(mapArgs,");
            dojoJourneyMapZoomScript.Append(osgrEnvelope[0].Easting + ",");
            dojoJourneyMapZoomScript.Append(osgrEnvelope[0].Northing + ",");
            dojoJourneyMapZoomScript.Append(osgrEnvelope[1].Easting + ",");
            dojoJourneyMapZoomScript.Append(osgrEnvelope[1].Northing);
            dojoJourneyMapZoomScript.Append("); });");
            
            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "dojoJourneyMapZoomScript", dojoJourneyMapZoomScript.ToString(), true);
        }

        /// <summary>
        /// Method which registers javascript to fire once the map has initialised, to zoom the journey to all addedd routes
        /// </summary>
        private void RegisterJourneyMapZoomToAllRoutes()
        {
            StringBuilder dojoJourneyMapZoomToAllScript = new StringBuilder();

            dojoJourneyMapZoomToAllScript.Append("dojo.subscribe(\"mapInitialised\",function(map){ ");
            dojoJourneyMapZoomToAllScript.Append("eventZoomJourneyToAllAdded(map); });");

            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "dojoJourneyMapZoomToAllScript", dojoJourneyMapZoomToAllScript.ToString(), true);
        }

        #endregion

        #endregion

        #endregion

        #region Public properties

        /// <summary>
        /// Read only. Returns the ClientID of the Map control
        /// </summary>
        public string MapId
        {
            get { return mapControl.MapID; }
        }

        /// <summary>
        /// Read only. Returns ClientID generated for Map symbols select control
        /// </summary>
        public string MapSymbolsSelectId
        {
            get { return mapSymbolsSelectControl.ClientID; }
        }

        /// <summary>
        /// Read only. Returns the ClientID of the Map journey display details drop down list control
        /// </summary>
        public string MapJourneyDisplayDetailsDropDownId
        {
            get { return mapJourneyDisplayDetailsControl.DropDownListJourneySegment.ClientID; }
        }

        /// <summary>
        /// Read only. Returns the first element Id on this control (to allow scrolling to on page refresh)
        /// </summary>
        public string FirstElementId
        {
            get { return scrollToMap.ClientID; }
        }

        /// <summary>
        /// Read/write. Sets the selected leg to be shown on the initial map displayed
        /// </summary>
        public int ShowSelectedLeg
        {
            get { return selectedLeg; }
            set { selectedLeg = value; }
        }

        /// <summary>
        /// Read only. Returns the show outward journey button, to allow attaching to the click event
        /// </summary>
        public TDButton ShowOutwardJourneyButton
        {
            get { return buttonShowOutwardJourney; }
        }

        /// <summary>
        /// Read only. Returns the show return journey button, to allow attaching to the click event
        /// </summary>
        public TDButton ShowReturnJourneyButton
        {
            get { return buttonShowReturnJourney; }
        }

        #endregion
    }
}