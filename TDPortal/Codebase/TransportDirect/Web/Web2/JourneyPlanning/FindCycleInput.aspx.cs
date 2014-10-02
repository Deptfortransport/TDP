// *********************************************** 
// NAME                 : FindCycleInput.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 05/06/2008
// DESCRIPTION          : Input Page for Find cycle
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindCycleInput.aspx.cs-arc  $ 
//
//   Rev 1.30   Mar 22 2013 10:49:12   dlane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.29   Oct 30 2012 11:13:28   mmodi
//Hide advanced options when favourite journey loaded with no via location
//Resolution for 5852: Gaz - Favourite journeys are not displayed
//
//   Rev 1.28   Oct 08 2012 13:45:42   mmodi
//Re-initialise via location when back selected
//Resolution for 5858: Cycle ambiguous via location is not reset when back button selected
//
//   Rev 1.27   Sep 27 2012 14:47:00   mmodi
//Display favourite journey in new location suggest control
//Resolution for 5852: Gaz - Favourite journeys are not displayed
//
//   Rev 1.26   Sep 04 2012 11:17:00   mmodi
//Updated to handle landing page auto plan with the new auto-suggest location control
//Resolution for 5837: Gaz - Page landing autoplan links fail on Cycle input page
//
//   Rev 1.25   Aug 28 2012 10:21:36   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.24   Jul 28 2011 16:19:54   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.23   May 13 2010 13:05:20   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.22   Jan 19 2010 13:21:00   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.21   Dec 14 2009 11:06:12   apatel
//stop the map showing when new location button click after amend
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.20   Dec 09 2009 11:33:58   mmodi
//When Clear button is clicked, reset the map
//
//   Rev 1.19   Dec 03 2009 16:00:56   apatel
//input page mapping enhancement related changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.18   Dec 02 2009 11:51:20   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.17   Nov 30 2009 10:19:42   apatel
//mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.16   Nov 30 2009 09:58:08   apatel
//input page find on map workflow changed
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.15   Nov 18 2009 11:42:10   apatel
//Added oneusekey for findonmap button click to move on to findmapinput page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.13   Nov 10 2009 11:30:14   apatel
//Find Input pages mapping enhancement changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.12   Oct 01 2009 10:54:02   pghumra
//Applied changes for cycle planner landing page, latitude longitude coordinates in landing page and find nearest car park functionality
//Resolution for 5316: CCN537 Cycle Planning Page Landing
//Resolution for 5317: CCNxxx Lat Long Coordinates in Page Landing
//
//   Rev 1.11   Oct 28 2008 10:34:26   mmodi
//Added screen reader labels
//
//   Rev 1.10   Oct 27 2008 14:13:50   mmodi
//Set default cycle speed when user has not entered a value in Advanced Options
//Resolution for 5153: Cycle Planner - Incorrect data as "Average cycling speed is 20mph" is displayed on 'Cycle Journey Details' page
//
//   Rev 1.9   Oct 20 2008 11:11:20   mmodi
//Updated to allow override location coordinates to be used for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.8   Oct 10 2008 15:52:24   mmodi
//Updated to have avoid time based check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.7   Oct 07 2008 15:43:30   mmodi
//Updated validation of preferences
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//Resolution for 5129: Cycle Planner - There is no limit on Cycling Speed entered by user on 'Cycle Journey Options' page
//
//   Rev 1.6   Sep 09 2008 13:18:40   mmodi
//Updated to load saved preferences
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Sep 02 2008 11:25:18   mmodi
//Added Favourite journeys control
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Aug 22 2008 10:35:12   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 01 2008 16:34:22   mmodi
//Updated help text
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Jul 28 2008 13:05:54   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 13:32:00   mmodi
//Updates to controls on page
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:13:28   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Web.UI;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using TransportDirect.CommonWeb.Helpers;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Input page for find cycle
    /// </summary>
    public partial class FindCycleInput : TDPage
    {
        #region Variables and controls

        /// <summary>
        /// Helper class responsible for methods for Find a pages
        /// </summary>
        private FindCycleInputAdapter findCycleInputAdapter;

        /// <summary>
        /// Holds user's Cycle planner details
        /// </summary>
        private FindCyclePageState findCyclePageState;

        /// <summary>
        /// Hold user's current journey parameters for cycle journey
        /// </summary>
        private TDJourneyParametersMulti journeyParams;

        /// <summary>
        /// Holds user's current page state.
        /// </summary>
        private InputPageState inputPageState;

        #endregion

        #region Constructor
        /// <summary>
		/// Default Constructor
		/// </summary>
		public FindCycleInput()
		{
            pageId = PageId.FindCycleInput;
        }
        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (TDSessionManager.Current.Session[SessionKey.LandingPageCheck])
            {
                LandingPageHelper helper = new LandingPageHelper();
                helper.ResetLandingPageSessionParameters();
            }
        }

        /// <summary>
		/// Performs page initialisation including event wiring.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            pageOptionsControltop.Submit += new EventHandler(preferencesControl_Submit);
            pageOptionsControltop.Clear += new EventHandler(preferencesControl_Clear);
            pageOptionsControltop.ShowAdvancedOptions += new EventHandler(pageOptionsControltop_ShowAdvancedOptions);
            pageOptionsControltop.HideAdvancedOptions += new EventHandler(pageOptionsControltop_HideAdvancedOptions);

            preferencesControl.PreferencesVisibleChanged += new EventHandler(preferencesControl_PreferencesVisibleChanged);

            preferencesControl.PageOptionsControl.Clear += new EventHandler(preferencesControl_Clear);
            preferencesControl.PageOptionsControl.Submit += new EventHandler(preferencesControl_Submit);
            preferencesControl.PageOptionsControl.HideAdvancedOptions += new EventHandler(pageOptionsControltop_HideAdvancedOptions);
            preferencesControl.PageOptionsControl.ShowAdvancedOptions += new EventHandler(pageOptionsControltop_ShowAdvancedOptions);

            commandBack.Click += new EventHandler(preferencesControl_Back);

            // Locations
            originLocationControl.MapLocationClick += new EventHandler(MapFromClick);
            originLocationControl.NewLocationClick += new EventHandler(NewLocationFromClick);

            destinationLocationControl.MapLocationClick += new EventHandler(MapToClick);
            destinationLocationControl.NewLocationClick += new EventHandler(NewLocationToClick);

            preferencesControl.LocationControl.MapLocationClick += new EventHandler(MapViaCycleClick);
            preferencesControl.LocationControl.NewLocationClick += new EventHandler(NewLocationCycleViaClick);

            dateControl.LeaveDateControl.DateChanged += new EventHandler(dateControlLeaveDateControl_DateChanged);
            dateControl.ReturnDateControl.DateChanged += new EventHandler(dateControlReturnDateControl_DateChanged);

            // Event Handler for default action button
            headerControl.DefaultActionEvent += new EventHandler(preferencesControl_Submit);

            // Event handler for loading a favourite journey
            favouriteLoadOptions.FavouriteLoggedIn.LoadFavourite += new FavouriteLoggedInControl.LoadFavouriteEventHandler(LoadFavourite);
        }

        /// <summary>
        /// Page Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadResources();

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            #region Populate controls
            if (Page.IsPostBack)
            {
                UpdateControls();
            }
            else if (TDSessionManager.Current.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) == null)
            {
                InitialRequestSetup();
            }
            else
            {
                 ITDSessionManager sessionManager = TDSessionManager.Current;
            
                // Clear out the JourneyRequest/Journey Result in case user came from another planner,
                // this planner uses CycleRequest/Result 
                sessionManager.JourneyRequest = null;
                sessionManager.JourneyResult = null;
              

                // Get the session values needed by this page
                findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;
                journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
                inputPageState = TDSessionManager.Current.InputPageState;
                findCycleInputAdapter = new FindCycleInputAdapter(journeyParams, findCyclePageState, inputPageState);

                
                findCycleInputAdapter.InitDateControl(dateControl);
                findCycleInputAdapter.UpdatePreferencesControl(preferencesControl);
            }
            #endregion

            #region Check for Amend mode
            if (findCyclePageState.AmendMode)
            {
                journeyParams.DestinationType.Type = ControlType.Default;
                journeyParams.OriginType.Type = ControlType.Default;
                journeyParams.CycleViaType.Type = ControlType.Default;
            }
            #endregion

            findCycleInputAdapter.InitialiseControls(originLocationControl, destinationLocationControl, preferencesControl, cycleJourneyTypeControl);

            LoadLeftHandNavigation();

            LoadHelpText();

            if (TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan])
            {
                // Update location control resolve flag as this is a landing page request so don't want to validate,
                // the landing page will have validated
                if (journeyParams.OriginLocation != null && journeyParams.OriginLocation.Status == TDLocationStatus.Valid)
                {
                    originLocationControl.ResolveLocation = false;
                }
                if (journeyParams.DestinationLocation != null && journeyParams.DestinationLocation.Status == TDLocationStatus.Valid)
                {
                    destinationLocationControl.ResolveLocation = false;
                }

                SubmitRequest();
            }
        }

        /// <summary>
		/// Page PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            #region Check for Amend mode
            // If user is in Amend mode, need to ensure their original entered preference values are selected
            // Need to do it on the prerender to ensure controls have populated lists
            if (!Page.IsPostBack && findCyclePageState.AmendMode)
            {
                findCycleInputAdapter.UpdatePreferencesControl(preferencesControl);
                findCycleInputAdapter.InitJourneyTypeControl(cycleJourneyTypeControl);
            }
            #endregion

            #region Display dates
            // Ensures the dates are displayed if we're in ambiguity mode
            if (Page.IsPostBack)
            {
                findCycleInputAdapter.UpdateDateControl(dateControl);
            }
            #endregion

            #region Display validation errors
            // Check if there are any validation errors in the session and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.ValidationError);

            findCycleInputAdapter.UpdateErrorMessages(
                panelErrorMessage, labelErrorMessages, TDSessionManager.Current.ValidationError);
            #endregion

            #region Display/Show preferences
            // Set up hide button and display of preferences
            
            commandBack.Visible = findCyclePageState.AmbiguityMode;
            
            pageOptionsControltop.AllowBack = false; // Only ever want to show the Back button at top of page
            pageOptionsControltop.AllowHideAdvancedOptions = false; // Only ever want to show the Hide button at the bottom of preferences control

            // When in ambiguity mode, we only want to show the preferences control if user has 
            // entered advanced options
            if ((findCyclePageState.AmbiguityMode) && (preferencesControl.PreferencesDefault))
            {
                preferencesControl.PreferencesVisible = false;
            }

            // Set visibility of buttons dependent on preferences visibility
            pageOptionsControltop.AllowShowAdvancedOptions = !preferencesControl.PreferencesVisible;
            pageOptionsControltop.AllowNext = !preferencesControl.PreferencesVisible;
            pageOptionsControltop.AllowClear = !preferencesControl.PreferencesVisible;

            // When in page ambiguity mode
            if (findCyclePageState.AmbiguityMode)
            {
                // Don't allow user to show advanced options
                pageOptionsControltop.AllowShowAdvancedOptions = false;
            }

            LoadFavouriteJourneysControl();
                        
            #endregion

            #region Hide controls when in ambiguity mode
            // hide from to title in ambiguity mode!
            labelFromToTitle.Visible = !findCyclePageState.AmbiguityMode;

            //Hide the advanced text if required:
            if (preferencesControl.PreferencesVisible || findCyclePageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }
            #endregion

            SetupMap();

            SetupSkipLinksAndScreenReaderText();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads text and images on the page
        /// </summary>
        private void LoadResources()
        {
            PageTitle = GetResource("CyclePlanner.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            imageFindACycle.ImageUrl = GetResource("HomeDefault.imageFindCycle.ImageUrl");
            imageFindACycle.AlternateText = " ";

            labelFindPageTitle.Text = GetResource("CyclePlanner.labelFindPageTitle.Text");

            labelFromToTitle.Text = GetResource("CyclePlanner.labelFromToTitle");

            commandBack.Text = GetResource("CyclePlanner.CommandBack.Text");

            labelOriginTitle.Text =GetResource("originSelect.labelLocationTitle");
            labelDestinationTitle.Text = GetResource("destinationSelect.labelLocationTitle");
        }

        /// <summary>
        /// Loads the left hand navigation on the page
        /// </summary>
        private void LoadLeftHandNavigation()
        {
            // Navigation links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            // Related links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindCycleInput);
            expandableMenuControl.AddExpandedCategory("Related links");

            // Client link
            ConfigureLeftMenu("FindCycleInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
        }

        /// <summary>
        /// Loads the Help button text
        /// </summary>
        private void LoadHelpText()
        {
            if (TDSessionManager.Current.FindPageState.AmbiguityMode)
            {
                Helpbuttoncontrol1.HelpUrl = GetResource("FindCycleInput.HelpAmbiguityUrl");
            }
            else
            {
                Helpbuttoncontrol1.HelpUrl = GetResource("FindCycleInput.HelpPageUrl");
            }
        }

        /// <summary>
        /// Displays the favourite journeys dropdown when user is logged on
        /// </summary>
        private void LoadFavouriteJourneysControl()
        {
            // check if user logged on 
            if (TDSessionManager.Current.Authenticated)
            {
                //show logged in controls when page refreshed/reloaded after ambiguity etc.
                favouriteLoadOptions.LoggedInDisplay();
            }
        }

        /// <summary>
        /// Sets the text for the skip to links (for screenreader browsers).
        /// </summary>
        private void SetupSkipLinksAndScreenReaderText()
        {
            // Setup gif resource for images (1 invisible image for all skip links)
            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");

            imageInputFormSkipLink.ImageUrl = skipLinkImageUrl;
            imageInputFormSkipLink.AlternateText = GetResource("CyclePlanner.FindCycleInput.imageInputFormSkipLink.AlternateText");
        }

        /// <summary>
        /// Performs initialisation when page is loaded for the first time. 
        /// </summary>
        private void InitialRequestSetup()
        {
            ITDSessionManager sessionManager = TDSessionManager.Current;

            #region Clear cache of journey data
            // Clear out the JourneyRequest/Journey Result in case user came from another planner,
            // this planner uses CycleRequest/Result 
            sessionManager.JourneyRequest = null;
            sessionManager.JourneyResult = null;

            ClearCacheHelper helper = new ClearCacheHelper();

            // Force clear of any printable information if added by the journey result page
            helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

            #endregion

            bool resetDone = true;

            //No reset is required if coming from landing page, since the journey parameters 
            //have already been initialised.
            if (sessionManager.Session[SessionKey.LandingPageCheck])
            {
                resetDone = false;
            }
            else
            {
                resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.Cycle);
            }

            // Get the session values needed by this page
            findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;
            journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = TDSessionManager.Current.InputPageState;
            findCycleInputAdapter = new FindCycleInputAdapter(journeyParams, findCyclePageState, inputPageState);

            if (resetDone)
            {   
                // Reset the journey parameters for Cycle mode
                findCycleInputAdapter.InitJourneyParameters();
            }
            else if (!resetDone && sessionManager.Authenticated && !findCyclePageState.AmendMode)
            {
                // Ensure travel preferences are still loaded when we didn't reset the journey parameters.
                // This fixes scenario where user goes to input page -> logon -> input page
                findCycleInputAdapter.LoadTravelDetails();
            }

            findCycleInputAdapter.InitDateControl(dateControl);
            findCycleInputAdapter.UpdatePreferencesControl(preferencesControl);
        }

        /// <summary>
        /// Updates session data with control values for those controls that do not raise events
        /// to signal that user has changed values (i.e. the date controls)
        /// </summary>
        private void UpdateControls()
        {
            // Get session values
            findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;
            journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = TDSessionManager.Current.InputPageState;
            findCycleInputAdapter = new FindCycleInputAdapter(journeyParams, findCyclePageState, inputPageState);

            // Update journey parameters
            journeyParams.CycleSpeedText = preferencesControl.SpeedText;
            journeyParams.CycleSpeedUnit = preferencesControl.SpeedUnit;
            journeyParams.CycleSpeedIsDefault = preferencesControl.SpeedIsDefault;
            journeyParams.CycleSpeedValid = preferencesControl.SpeedTextValid;
            journeyParams.CycleAvoidSteepClimbs = preferencesControl.AvoidSteepClimbs;
            journeyParams.CycleAvoidUnlitRoads = preferencesControl.AvoidUnlitRoads;
            journeyParams.CycleAvoidWalkingYourBike = preferencesControl.AvoidWalkingYourBike;
            journeyParams.CycleAvoidTimeBased = preferencesControl.AvoidTimeBased;
            journeyParams.CyclePenaltyFunctionOverride = preferencesControl.PenaltyFunctionOverride;
            journeyParams.CycleLocationsIsDefault = preferencesControl.LocationsIsDefault;
            journeyParams.CycleLocationOriginOverride = preferencesControl.LocationOriginOverride;
            journeyParams.CycleLocationDestinationOverride = preferencesControl.LocationDestinationOverride;

            journeyParams.CycleJourneyType = cycleJourneyTypeControl.JourneyType;
                        
            // If user has entered their own speed, then get the value in metres and overwrite the 
            // default value in the session
            if (!preferencesControl.SpeedIsDefault)
            {
                journeyParams.CycleSpeedMax = preferencesControl.SpeedMetresPerHour;
            }
            else
            {
                journeyParams.CycleSpeedMax = findCycleInputAdapter.GetDefaultCycleSpeed();
            }

            // Update dates
            findCycleInputAdapter.UpdateJourneyDates(dateControl);

        }

        /// <summary>
        /// Sets up a map for the location search and type of map location mode
        /// If map location mode is via map gets initialised with start, via and end mode.
        /// By default map gets initialised with start and end mode.
        /// </summary>
        private void SetupMap()
        {
            MapLocationPoint[] locationsToShow = GetMapLocationPoints();

            LocationSearch locationSearch = inputPageState.MapLocationSearch;
            TDLocation location = inputPageState.MapLocation;

            SearchType searchType = SearchType.Map;


            if (locationSearch != null)
            {
                searchType = locationSearch.SearchType;
            }

            if (location != null)
            {
                if (location.GridReference.IsValid)
                {
                    mapInputControl.MapCenter = location.GridReference;
                }
            }

            if (preferencesControl.PreferencesVisible)
            {
                MapLocationMode[] mapModes = new MapLocationMode[3] { MapLocationMode.Start, MapLocationMode.Via, MapLocationMode.End };
                mapInputControl.Initialise(searchType, locationsToShow, mapModes);
            }
            else
            {
                mapInputControl.Initialise(searchType, locationsToShow);
            }

            if (!mapInputControl.Visible && TDSessionManager.Current.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) != null)
            {
                if (!inputPageState.AmbiguityMode)
                {
                    mapInputControl.Visible = true;
                }
            }


        }

        /// <summary>
        /// Returns the location points to show on map
        /// </summary>
        /// <returns></returns>
        private MapLocationPoint[] GetMapLocationPoints()
        {
            MapHelper mapHelper = new MapHelper();

            List<MapLocationPoint> mapLocationPoints = new List<MapLocationPoint>();

            MapLocationPoint origin = mapHelper.GetMapLocationPoint(journeyParams.OriginLocation, MapLocationSymbolType.Start, true, false);

            MapLocationPoint destination = mapHelper.GetMapLocationPoint(journeyParams.DestinationLocation, MapLocationSymbolType.End, true, false);

            MapLocationPoint publicVia = mapHelper.GetMapLocationPoint(journeyParams.PublicViaLocation, MapLocationSymbolType.Via, true, false);

            MapLocationPoint privateVia = mapHelper.GetMapLocationPoint(journeyParams.PrivateViaLocation, MapLocationSymbolType.Via, true, false);

            if (origin.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(origin);
            }

            if (destination.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(destination);
            }

            if (publicVia.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(publicVia);
            }

            if (privateVia.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(privateVia);
            }

            return mapLocationPoints.ToArray();
        }

        
        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler called when visibility of preferences changed. Updates page state with new value.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_PreferencesVisibleChanged(object sender, EventArgs e)
        {
            findCyclePageState.TravelDetailsVisible = preferencesControl.PreferencesVisible;
            if (!preferencesControl.PreferencesVisible)
                pageOptionsControltop.Visible = true;
        }
                
        /// <summary>
        /// Event handler called when next button clicked. 
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Submit(object sender, EventArgs e)
        {
            SubmitRequest();
        }

        /// <summary>
        /// Event handler called when clear page button is clicked. Journey parameters are reset
        /// to initial values, page controls updated and the page set to input mode.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Clear(object sender, EventArgs e)
        {
            // Reset all the journey parameters, and initialise for cycle planner
            findCycleInputAdapter.InitJourneyParameters();
            
            findCyclePageState.AmbiguityMode = false;

            // Hide advanced options 
            preferencesControl.PreferencesVisible = false;

            // Hide map
            mapInputControl.Visible = false;

            // Reset location controls
            originLocationControl.Reset(SearchType.AddressPostCode);
            destinationLocationControl.Reset(SearchType.AddressPostCode);
            preferencesControl.LocationControl.Reset(SearchType.AddressPostCode);

            // Because we've reset the journey parameters, update the controls on this 
            // page with these new values
            findCycleInputAdapter.InitialiseControls(originLocationControl, destinationLocationControl, preferencesControl, cycleJourneyTypeControl);
            findCycleInputAdapter.UpdatePreferencesControl(preferencesControl);
            findCycleInputAdapter.UpdatePreferencesDisplayMode(preferencesControl, GenericDisplayMode.Normal);
            findCycleInputAdapter.UpdateJourneyTypeDisplayMode(cycleJourneyTypeControl, GenericDisplayMode.Normal);
            
            dateControl.LeaveDateControl.DateErrors = null;
            dateControl.LeaveDateControl.AmbiguityMode = false;
            dateControl.ReturnDateControl.DateErrors = null;
            dateControl.ReturnDateControl.AmbiguityMode = false;

            // Reset the Map locations to show on the map (use Origin as it should have been reset by now)
            inputPageState.MapLocation = journeyParams.OriginLocation;
            inputPageState.MapLocationSearch = journeyParams.Origin;

            TDSessionManager.Current.ValidationError = null;

            TDPage.CloseAllSingleWindows(Page);
        }

        /// <summary>
        /// Added event for pageOptionsControltop which raises when user clicks AdvanceOptions.
        /// this will show advance options and hide pageOptionsControltop control. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageOptionsControltop_ShowAdvancedOptions(object sender, EventArgs e)
        {
            // Showing Advance options
            preferencesControl.PreferencesVisible = true;
            // making page options control invisible
            pageOptionsControltop.AllowBack = false;
            pageOptionsControltop.AllowClear = false;
            pageOptionsControltop.AllowNext = true;
            pageOptionsControltop.AllowHideAdvancedOptions = false;
        }

        /// <summary>
        /// Added event for pageOptionsControltop which raises when user hides AdvanceOptions.
        /// this will hide advance options. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageOptionsControltop_HideAdvancedOptions(object sender, EventArgs e)
        {
            preferencesControl.PreferencesVisible = false;
        }

        /// <summary>
        /// Event handler called when back button clicked in ambiguous mode. Decrements the level of
        /// hierarchical location searches (origin, destination and private via). If all locations are at highest 
        /// level then page is reverted to input mode and original input parameters reinstated.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Back(object sender, EventArgs e)
        {
            dateControl.CalendarClose();

            if (findCycleInputAdapter.IsAtHighestLevel()
                &&
                !(journeyParams.CycleViaLocation.Status == TDLocationStatus.Ambiguous &&
                journeyParams.CycleVia.CurrentLevel > 0))
            {
                //Check to see if there is a previous page waiting on the stack
                if (inputPageState.JourneyInputReturnStack.Count != 0)
                {
                    TransitionEvent lastPage = (TransitionEvent)inputPageState.JourneyInputReturnStack.Pop();
                    //If the user is returing to the previous journey results, re-validate them
                    if (lastPage == TransitionEvent.FindAInputRedirectToResults)
                    {
                        TDSessionManager.Current.CycleResult.IsValid = true;
                    }
                    TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = lastPage;
                    return;
                }

                TDSessionManager.Current.ValidationError.Initialise();

                findCyclePageState.AmbiguityMode = false;
                findCyclePageState.ReinstateJourneyParameters(journeyParams);

                // Populate preferences controls with values already entered by user
                findCycleInputAdapter.UpdatePreferencesControl(preferencesControl);
                findCycleInputAdapter.UpdatePreferencesDisplayMode(preferencesControl, GenericDisplayMode.Normal);
                findCycleInputAdapter.InitJourneyTypeControl(cycleJourneyTypeControl);
                findCycleInputAdapter.UpdateJourneyTypeDisplayMode(cycleJourneyTypeControl, GenericDisplayMode.Normal);
                
                dateControl.LeaveDateControl.AmbiguityMode = false;
                dateControl.ReturnDateControl.AmbiguityMode = false;
                dateControl.LeaveDateControl.DateErrors = null;
                dateControl.ReturnDateControl.DateErrors = null;

                findCycleInputAdapter.InitLocationsControl(originLocationControl, destinationLocationControl);
                findCycleInputAdapter.InitPreferencesControl(preferencesControl);
            }
            else
            {
                // Decrement the drilldown level of any ambiguous locations
                // that are not yet at their highest level
                journeyParams.Origin.DecrementLevel();
                journeyParams.Destination.DecrementLevel();
                journeyParams.CycleVia.DecrementLevel();
            }

            if (preferencesControl.PreferencesVisible)
            {
                pageOptionsControltop.AllowNext = true;
                pageOptionsControltop.AllowBack = false;
                pageOptionsControltop.AllowClear = false;
                pageOptionsControltop.AllowHideAdvancedOptions = false;
            }
            else
            {
                pageOptionsControltop.AllowBack = false;
                pageOptionsControltop.AllowClear = true;
                pageOptionsControltop.AllowNext = true;
                pageOptionsControltop.AllowHideAdvancedOptions = false;
            }
        }

        #region Map Event Handlers

        /// <summary>
        /// Event handler for when Find on map is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapFromClick(object sender, EventArgs e)
        {
            bool shiftForm = false;
            inputPageState.MapType = CurrentLocationType.From;
            inputPageState.MapMode = CurrentMapMode.FromFindAInput;

            #region Resolve location for map

            // Reset map search and location
            inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
            inputPageState.MapLocationSearch.ClearAll();
            
            // Validate selected location and save to parameters ready for input/map use
            originLocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.Origin = originLocationControl.Search;
            journeyParams.OriginLocation = originLocationControl.Location;
            inputPageState.MapLocationSearch = originLocationControl.Search;
            inputPageState.MapLocation = originLocationControl.Location;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                journeyParams.Origin.SearchType = SearchType.Map;
            }
            // Otherwise, ambiguous location, send to next page to resolve
            else
            {
                inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                shiftForm = true;
            }

            #endregion

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                mapInputControl.Visible = true;
            }
        }

        /// <summary>
        /// Event handler for when Find on map is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapToClick(object sender, EventArgs e)
        {
            bool shiftForm = false;
            inputPageState.MapType = CurrentLocationType.To;
            inputPageState.MapMode = CurrentMapMode.FromFindAInput;

            #region Resolve location for map

            // Reset map search and location
            inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
            inputPageState.MapLocationSearch.ClearAll();

            // Validate selected location and save to parameters ready for input/map use
            destinationLocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.Destination = destinationLocationControl.Search;
            journeyParams.DestinationLocation = destinationLocationControl.Location;
            inputPageState.MapLocationSearch = destinationLocationControl.Search;
            inputPageState.MapLocation = destinationLocationControl.Location;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                journeyParams.Destination.SearchType = SearchType.Map;
            }
            // Otherwise, ambiguous location, send to next page to resolve
            else
            {
                inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                shiftForm = true;
            }

            #endregion

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                mapInputControl.Visible = true;
            }
        }

        /// <summary>
        /// Event handler for whe Find on map is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapViaCycleClick(object sender, EventArgs e)
        {
            bool shiftForm = false;
            inputPageState.MapType = CurrentLocationType.CycleVia;
            inputPageState.MapMode = CurrentMapMode.FromFindAInput;

            #region Resolve location for map

            // Reset map search and location
            inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
            inputPageState.MapLocationSearch.ClearAll();

            // Validate selected location and save to parameters ready for input/map use
            preferencesControl.LocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.CycleVia = preferencesControl.LocationControl.Search;
            journeyParams.CycleViaLocation = preferencesControl.LocationControl.Location;
            inputPageState.MapLocationSearch = preferencesControl.LocationControl.Search;
            inputPageState.MapLocation = preferencesControl.LocationControl.Location;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                journeyParams.CycleVia.SearchType = SearchType.Map;
            }
            // Otherwise, ambiguous location, send to next page to resolve
            else
            {
                inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                shiftForm = true;
            }

            #endregion

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                mapInputControl.Visible = true;
            }
        }

        #endregion

        /// <summary>
        /// Event handler when Outward date calendar used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e)
        {
            journeyParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
            journeyParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
        }

        /// <summary>
        /// Event handler when Return data calendar used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateControlReturnDateControl_DateChanged(object sender, EventArgs e)
        {
            journeyParams.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
            journeyParams.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
        }

        /// <summary>
        /// Retrieves the User's selected FavouriteJourney using the Favourite GUID held in the
        /// list of users journeys.  JourneyParameter data is populated from the selected 
        /// Favourite journey.  This will be used to populate page elements when page is refreshed
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Controls.FavouriteLoggedInControl.LoadFavouriteEventArgs</param>
        public void LoadFavourite(object sender, Controls.FavouriteLoggedInControl.LoadFavouriteEventArgs e)
        {
            FavouriteJourney favouriteJourney = e.FavouriteJourney;
            FavouriteJourneyHelper.LoadFavouriteJourney(favouriteJourney);

            // Ensure all variables on page are updated
            findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;
            journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = TDSessionManager.Current.InputPageState;
            findCycleInputAdapter = new FindCycleInputAdapter(journeyParams, findCyclePageState, inputPageState);

            findCycleInputAdapter.InitialiseControls(originLocationControl, destinationLocationControl, preferencesControl, cycleJourneyTypeControl);
            findCycleInputAdapter.UpdatePreferencesControl(preferencesControl);

            // Display the advanced options if a via location exists for the favourite journey
            if (journeyParams.CycleViaLocation != null && journeyParams.CycleViaLocation.Status == TDLocationStatus.Valid)
            {
                pageOptionsControltop_ShowAdvancedOptions(this, null);
            }
            else
            {
                pageOptionsControltop_HideAdvancedOptions(this, null);
            }
        }

        #region Location events

        /// <summary>
        /// Event handler called when new location button is clicked for the "from" location.
        /// The journey parameters are updated with the "from" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void NewLocationFromClick(object sender, EventArgs e)
        {           
            // Set search and location to be the location controls values
            journeyParams.Origin = originLocationControl.Search;
            journeyParams.OriginLocation = originLocationControl.Location;

            // If the map control is visible, set it to focus in on the destination, if that is valid
            if (mapInputControl.Visible)
            {
                if (journeyParams.DestinationLocation.Status == TDLocationStatus.Valid)
                {
                    inputPageState.MapLocationSearch = journeyParams.Destination;
                    inputPageState.MapLocation = journeyParams.DestinationLocation;
                }
            }
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "to" location.
        /// The journey parameters are updated with the "to" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void NewLocationToClick(object sender, EventArgs e)
        {
            // Set search and location to be the location controls values
            journeyParams.Destination = destinationLocationControl.Search;
            journeyParams.DestinationLocation = destinationLocationControl.Location;

            // If the map control is visible, set it to focus in on the origin, if that is valid
            if (mapInputControl.Visible)
            {
                if (journeyParams.OriginLocation.Status == TDLocationStatus.Valid)
                {
                    inputPageState.MapLocationSearch = journeyParams.Origin;
                    inputPageState.MapLocation = journeyParams.OriginLocation;
                }
            }
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "via" location.
        /// The journey parameters are updated with the "via" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void NewLocationCycleViaClick(object sender, EventArgs e)
        {
            journeyParams.CycleVia = preferencesControl.LocationControl.Search;
            journeyParams.CycleViaLocation = preferencesControl.LocationControl.Location;

            // ensure via location input is displayed on preferences control
            preferencesControl.ViaLocationDisplayMode = GenericDisplayMode.Normal;
        }
        #endregion

        #endregion

        #region SubmitRequest

        /// <summary>
        /// Method containing the Submit functionality so that it
        /// can be called by the Landing page when AutoPlanning is required. 
        /// </summary>
        private void SubmitRequest()
        {
            if (preferencesControl.SavePreferences)
            {
                findCycleInputAdapter.SaveTravelDetails();
            }
            
            dateControl.CalendarClose();

            if (!findCyclePageState.AmbiguityMode)
            {
                // Initial search
                findCyclePageState.SaveJourneyParameters(journeyParams);
            }

            #region Origin and Destination locations

            originLocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.Origin = originLocationControl.Search;
            journeyParams.OriginLocation = originLocationControl.Location;

            destinationLocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.Destination = destinationLocationControl.Search;
            journeyParams.DestinationLocation = destinationLocationControl.Location;

            #endregion

            #region Via location

            preferencesControl.LocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.CycleVia = preferencesControl.LocationControl.Search;
            journeyParams.CycleViaLocation = preferencesControl.LocationControl.Location;

            #endregion

            // Call method to populate the specific cycle location parameters needed by the Cycle planner
            findCycleInputAdapter.PopulateCycleLocations(originLocationControl, destinationLocationControl, preferencesControl);
            
            // Call method to validate preferences, and save back to the journey parameters
            preferencesControl.ValidatePreferences();
            UpdateControls();

            // Set up the JourneyPlanControlData
            findCycleInputAdapter.InitialiseAsyncCallState();

            // Validate the JourneyParameters
            JourneyPlanRunner.CycleJourneyPlanRunner runner = new JourneyPlanRunner.CycleJourneyPlanRunner(Global.tdResourceManager);

            findCyclePageState.AmbiguityMode = !runner.ValidateAndRun(
                 TDSessionManager.Current,
                 TDSessionManager.Current.JourneyParameters,
                 GetChannelLanguage(TDPage.SessionChannelName),
                 true);

            if (findCyclePageState.AmbiguityMode)
            {
                if (!TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan])
                {
                    // Indicate the locations when valid/resolved should be shown as fixed when page is in ambiguity 
                    originLocationControl.ShowFixedLocation = true;
                    destinationLocationControl.ShowFixedLocation = true;
                    preferencesControl.LocationControl.ShowFixedLocation = true;

                    // Set all preferance control options to readonly
                    findCycleInputAdapter.UpdatePreferencesDisplayMode(preferencesControl, GenericDisplayMode.ReadOnly);
                    findCycleInputAdapter.UpdateJourneyTypeDisplayMode(cycleJourneyTypeControl, GenericDisplayMode.ReadOnly);
                    
                    dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                    dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                    dateControl.LeaveDateControl.AmbiguityMode = true;
                    dateControl.ReturnDateControl.AmbiguityMode = true;
                }
                else
                {
                    findCyclePageState.AmbiguityMode = false;
                }
            }
            else
            {
                // Extending journeys using a Find A is not currently possible so clear down the itinerary
                TDItineraryManager itineraryManager = TDItineraryManager.Current;
                if (itineraryManager.Length >= 1)
                {
                    itineraryManager.ResetItinerary();
                }

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindAInputOk;
            }

            // Adding code to refresh the controls otherwise, the amendMode
            // change is not taken into account until next postback
            if (findCyclePageState.AmendMode)
            {
                //Disable AmendMode when leaving the page
                findCyclePageState.AmendMode = false;
            }

        }
        #endregion
    }
}
