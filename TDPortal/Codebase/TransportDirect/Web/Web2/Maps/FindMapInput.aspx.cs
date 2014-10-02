// *********************************************** 
// NAME                 : FindMapInput.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/10/2009
// DESCRIPTION          : Input Page for Find a map
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Maps/FindMapInput.aspx.cs-arc  $ 
//
//   Rev 1.10   Mar 22 2013 10:49:26   dlane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.9   Jul 28 2011 16:21:10   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.8   Dec 15 2009 08:46:52   apatel
//Resolved issue with amend journey not changing when location is selected using findonmapinput page and issue with back button working wrong.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Dec 14 2009 14:03:02   apatel
//updated to resove the issue with visitplanner locations not get set up in session as TdJourneyParametersMulti was overriding it
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Dec 11 2009 09:39:38   apatel
//ExtendedJourneyInput mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Dec 04 2009 08:49:00   apatel
//input page mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Dec 02 2009 11:54:16   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 30 2009 09:58:34   apatel
//input page find on map workflow changed
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Nov 18 2009 11:20:44   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 17 2009 11:32:56   mmodi
//Handle arriving from Home page Find a place control
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 02 2009 17:52:30   mmodi
//Initial revision.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.SuggestionLinkService;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.CommonWeb.Helpers;

using Logger = System.Diagnostics.Trace;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Input Page for Find a map
    /// </summary>
    public partial class FindMapInput : TDPage
    {
        #region Variables and controls

        // Session variables
        private ITDSessionManager sessionManager;
        private TDJourneyParametersMulti journeyParameters;
        private InputPageState inputPageState;

        #region Map Location Search Variables

        // Declaration of search/location object members
        private LocationSearch mapSearch;
        private TDLocation mapLocation;
        private LocationSelectControlType mapLocationControlType;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
		/// Default Constructor
		/// </summary>
		public FindMapInput()
		{
            pageId = PageId.FindMapInput;
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
		/// Performs page initialisation including event wiring.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            pageOptionsControltop.Submit += new EventHandler(pageOptionsControltop_Submit);
            commandBack.Click += new EventHandler(this.commandBack_Click);
            findLocationControl.NewLocation += new EventHandler(findLocationControl_NewLocation);
        }

        /// <summary>
        /// Page Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            #region Populate controls
            if (Page.IsPostBack || (TDSessionManager.Current.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) != null))
            {
                // Set up the session variables
                LoadSessionVariables();
                
                // Get the map location objects back from session
                LoadSessionLocationVariables();
            }
            else
            {
                InitialRequestSetup();
            }
            #endregion

            // Inject map location objects if needed
            // Added for interacting with the Homepage FindAPlaceControl
            if ((sessionManager.GetOneUseKey(SessionKey.FindALocationFromHomePage) != null)
                ||
                (sessionManager.Session[SessionKey.LandingPageCheck])
                || (sessionManager.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) != null))
            {
                LoadSessionLocationVariables();
            }

            // Initialise the Find location control with the map location objects
            InitLocationsControl();

            // Come from Homepage or LandingPage, so automatically submit the request to resolve locations
            // and transition to ambiguity or results
            if ((sessionManager.GetOneUseKey(SessionKey.FindALocationFromHomePage) != null)
                ||
                (sessionManager.Session[SessionKey.LandingPageCheck]))
            {
                // Force a refresh of the locations control (to set up its internal parameters),
                // then call SubmitRequest to perform the location search
                findLocationControl.RefreshTristateControl(false);

                SubmitRequest();

                // Reset the transferred flag to allow transition to results page if needed
                TDSessionManager.Current.Session[SessionKey.Transferred] = false;
            }

            // Initialise the Next button
            InitPageOptionsControl();

            LoadHelpText();
        }

        /// <summary>
		/// Page PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            LoadResources();

            LoadLeftHandNavigation();

            SetupSkipLinksAndScreenReaderText();

            SetBackButtonVisibility();

            SaveSessionLocationVariables();
        }

        /// <summary>
        /// Page Unload event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (TDSessionManager.Current.Session[SessionKey.LandingPageCheck])
            {
                LandingPageHelper helper = new LandingPageHelper();
                helper.ResetLandingPageSessionParameters();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads text and images on the page
        /// </summary>
        private void LoadResources()
        {
            PageTitle = GetResource("FindMapInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            imageFindMap.ImageUrl = GetResource("HomeFindAPlace.imageFindAPlace.ImageUrl");
            imageFindMap.AlternateText = " ";

            labelFindPageTitle.Text = labelFindPageTitle.Text = GetResource("HomeFindAPlace.lblFindAPlace");

            labelFromToTitle.Text = GetResource("FindMapInput.labelFromToTitle");

            commandBack.Text = GetResource("FindMapInput.CommandBack.Text");
        }

        /// <summary>
        /// Loads the left hand navigation on the page
        /// </summary>
        private void LoadLeftHandNavigation()
        {
            // Navigation links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);

            // Related links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyPlannerLocationMap);
            expandableMenuControl.AddExpandedCategory("Related links");

            // Client link
            ConfigureLeftMenu("JourneyPlannerLocationMap.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);
        }

        /// <summary>
        /// Sets the text for the skip to links (for screenreader browsers).
        /// </summary>
        private void SetupSkipLinksAndScreenReaderText()
        {
            // Setup gif resource for images (1 invisible image for all skip links)
            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");

            imageInputFormSkipLink.ImageUrl = skipLinkImageUrl;
            imageInputFormSkipLink.AlternateText = GetResource("FindMapInput.imageInputFormSkipLink.AlternateText");
        }

        /// <summary>
        /// Loads the Help button text
        /// </summary>
        private void LoadHelpText()
        {
            if (inputPageState.AmbiguityMode)
            {
                Helpbuttoncontrol1.HelpUrl = GetResource("FindMapInput.HelpAmbiguityUrl");
            }
            else
            {
                Helpbuttoncontrol1.HelpUrl = GetResource("FindMapInput.HelpPageUrl");
            }
        }

        /// <summary>
        /// Sets the visibility of the Back button.
        /// </summary>
        private void SetBackButtonVisibility()
        {
            if ((inputPageState.JourneyInputReturnStack.Count > 0)
                ||
                ((inputPageState.MapLocation != null) && (inputPageState.MapLocation.Status == TDLocationStatus.Ambiguous))
               )
            {
                commandBack.Visible = true;
            }
            else
            {
                commandBack.Visible = false;
            }
        }

        /// <summary>
        /// Performs initialisation when page is loaded for the first time. 
        /// </summary>
        private void InitialRequestSetup()
        {
            sessionManager = TDSessionManager.Current;

            bool resetDone = true;

            //No reset is required if coming from landing page, since the journey parameters 
            //have already been initialised.
            if (sessionManager.Session[SessionKey.LandingPageCheck])
            {
                resetDone = false;
            }
            else
            {
                resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.None);
            }

            // Get the session values needed by this page
            LoadSessionVariables();

            // Set new instances of the page map location variables
            InitPageLocationVariables();
        }

        /// <summary>
        /// Sets the page session variables
        /// </summary>
        private void LoadSessionVariables()
        {
            // Get the session values needed by this page
            sessionManager = TDSessionManager.Current;
            journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = sessionManager.InputPageState;
        }

        /// <summary>
        /// Loads location variables from the session into page location variables
        /// </summary>
        private void LoadSessionLocationVariables()
        {
            mapSearch = inputPageState.MapLocationSearch;
            mapLocation = inputPageState.MapLocation;
            mapLocationControlType = inputPageState.MapLocationControlType;
        }

        /// <summary>
        /// Saves location variables to the session from the page location variables
        /// </summary>
        private void SaveSessionLocationVariables()
        {
            inputPageState.MapLocationSearch = mapSearch;
            inputPageState.MapLocation = mapLocation;
            inputPageState.MapLocationControlType = mapLocationControlType;
        }

        /// <summary>
        /// Initialises the map location variables on page
        /// </summary>
        private void InitPageLocationVariables()
        {
            // Reset the page variables for map location
            mapLocation = new TDLocation();
            mapSearch = new LocationSearch();
            mapLocationControlType = new LocationSelectControlType(ControlType.Default);

            mapSearch.SearchType = SearchType.MainStationAirport;
            mapLocation.SearchType = SearchType.MainStationAirport;
        }

        /// <summary>
        /// Initialises the findLocationControl with the page map location variables
        /// </summary>
        private void InitLocationsControl()
        {
            findLocationControl.TheLocation = mapLocation;
            findLocationControl.TheSearch = mapSearch;
            findLocationControl.LocationControlType = mapLocationControlType;

            findLocationControl.StationTypesCheckListVisible = false;
            findLocationControl.TriLocationControl.LocationAmbiguousControl.NewLocationVisible = false;
        }

        /// <summary>
        /// Initialises the PageOptionsControl (i.e. only show Next button)
        /// </summary>
        private void InitPageOptionsControl()
        {
            pageOptionsControltop.AllowBack = false; // Only ever want to show the Back button at top of page
            pageOptionsControltop.AllowNext = true;
            pageOptionsControltop.AllowHideAdvancedOptions = false; 
            pageOptionsControltop.AllowShowAdvancedOptions = false;
            pageOptionsControltop.AllowClear = false;
        }

        #region Location helpers

        /// <summary>
        /// Returns true if the location search is currently set to their highest level of hierarchy.
        /// </summary>
        /// <returns>True if at highest level, false otherwise</returns>
        private bool IsAtHighestLevel()
        {
            return !(mapLocation.Status == TDLocationStatus.Ambiguous && mapSearch.CurrentLevel > 0);
        }

        #endregion

        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler called when next button clicked. 
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void pageOptionsControltop_Submit(object sender, EventArgs e)
        {
            SubmitRequest();
        }

        /// <summary>
        /// Event handler called when back button clicked in ambiguous mode. Decrements the level of
        /// hierarchical location searches. If locations are at highest 
        /// level then page is reverted to input mode and original input parameters reinstated.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void commandBack_Click(object sender, EventArgs e)
        {
            if (IsAtHighestLevel())
            {
                //Check to see if there is a previous page waiting on the stack
                if (inputPageState.JourneyInputReturnStack.Count != 0)
                {
                    TransitionEvent lastPage = TransitionEvent.FindMapInputDefault;

                    object returnObj = TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Pop();

                    if (returnObj is PageId)
                    {
                        PageId returnPage = (PageId)returnObj;


                        switch (returnPage)
                        {
                            case PageId.JourneyPlannerAmbiguity:
                                lastPage = TransitionEvent.JourneyPlannerAmbiguityDefault;
                                break;
                            case PageId.JourneyPlannerInput:
                                lastPage = TransitionEvent.JourneyPlannerInputDefault;
                                break;
                            case PageId.FindCarInput:
                                lastPage = TransitionEvent.FindCarInputDefault;
                                break;
                            case PageId.FindBusInput:
                                lastPage = TransitionEvent.FindBusInputDefault;
                                break;
                            case PageId.ParkAndRideInput:
                                lastPage = TransitionEvent.ParkAndRideInput;
                                break;
                            case PageId.VisitPlannerInput:
                                lastPage = TransitionEvent.VisitPlannerInputBack;
                                break;
                            case PageId.FindCycleInput:
                                lastPage = TransitionEvent.FindCycleInputDefault;
                                break;
                            case PageId.ExtendJourneyInput:
                                lastPage = TransitionEvent.ExtendJourneyInput;
                                break;
                            default:
                                lastPage = TransitionEvent.FindMapInputDefault;
                                break;
                        }
                    }
                    else if (returnObj is TransitionEvent)
                    {
                        lastPage = (TransitionEvent)returnObj;
                    }

                    //If the user is returing to the previous journey results, re-validate them
                    if (lastPage == TransitionEvent.FindAInputRedirectToResults)
                    {
                        sessionManager.JourneyResult.IsValid = true;
                    }
                    sessionManager.FormShift[SessionKey.TransitionEvent] = lastPage;
                    return;
                }

                if (sessionManager.ValidationError != null)
                {
                    sessionManager.ValidationError.Initialise();
                }

                inputPageState.AmbiguityMode = false;

                SaveSessionLocationVariables();

                InitLocationsControl();

                if (findLocationControl.TheLocation.Status != TDLocationStatus.Valid)
                {
                    findLocationControl.SetLocationUnspecified();
                }
            }
            else
            {
                // Decrement the drilldown level of any ambiguous locations
                // that are not yet at their highest level
                mapSearch.DecrementLevel();
            }

            InitPageOptionsControl();
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the location.
        /// The journey parameters are updated with the "from" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void findLocationControl_NewLocation(object sender, EventArgs e)
        {
            // Reset the locations in the findLocationControl
            findLocationControl.TheLocation = new TDLocation();
            findLocationControl.TheSearch = new LocationSearch();
            findLocationControl.LocationControlType = new LocationSelectControlType(ControlType.Default);
            
            InitPageLocationVariables();

            InitLocationsControl();

            SaveSessionLocationVariables();
        }

        #endregion

        #region SubmitRequest
        /// <summary>
        /// Method containing the Submit functionality so that it
        /// can be called by the Landing page when AutoPlanning is required. 
        /// </summary>
        private void SubmitRequest()
        {
            // Call search
            findLocationControl.Search();

            inputPageState.AmbiguityMode = (findLocationControl.TheLocation.Status != TDLocationStatus.Valid);
            
            // Ensure the page map location objects are updated
            mapLocation = findLocationControl.TheLocation;
            mapSearch = findLocationControl.TheSearch;
            mapLocationControlType = findLocationControl.LocationControlType;

            // And save back to session
            SaveSessionLocationVariables();

            if (inputPageState.AmbiguityMode)
            {
                if (!TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan])
                {
                    // Nothing to do here, page will refresh itself showing the ambiguous location
                }
                else
                {
                    inputPageState.AmbiguityMode = false;
                }
            }
            else
            {
                // Extending journeys using a Find A is not currently possible so clear down the itinerary
                TDItineraryManager itineraryManager = TDItineraryManager.Current;
                if (itineraryManager.Length >= 1 && !itineraryManager.ExtendEndOfItinerary && !itineraryManager.ExtendToItineraryStartPoint())
                {
                    itineraryManager.ResetItinerary();
                }

                switch (inputPageState.MapType)
                {
                    case CurrentLocationType.From:
                        UpdateFromLocation();
                        break;

                    case CurrentLocationType.To:
                        UpdateToLocation();
                        break;

                    case CurrentLocationType.VisitPlannerOrigin:
                        UpdateVisitOriginLocation();
                        break;

                    case CurrentLocationType.VisitPlannerVisitPlace1:
                        UpdateVisitLocation1();
                        break;

                    case CurrentLocationType.VisitPlannerVisitPlace2:
                        UpdateVisitLocation2();
                        break;

                    case CurrentLocationType.PrivateVia:
                    case CurrentLocationType.PublicVia:
                    case CurrentLocationType.CycleVia:
                        UpdateViaLocation();
                        break;
                }

                TransitionEvent te = TransitionEvent.FindMapResult;

                if (TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count > 0)
                {
                    PageId returnPage = PageId.FindMapResult;

                    object returnObj = TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Pop();

                    if (returnObj is PageId)
                    {
                        returnPage = (PageId)returnObj;
                    

                        switch (returnPage)
                        {
                            case PageId.JourneyPlannerAmbiguity:
                                te = TransitionEvent.JourneyPlannerAmbiguityDefault;
                                break;
                            case PageId.JourneyPlannerInput:
                                te = TransitionEvent.JourneyPlannerInputDefault;
                                break;
                            case PageId.FindCarInput:
                                te = TransitionEvent.FindCarInputDefault;
                                break;
                            case PageId.FindBusInput:
                                te = TransitionEvent.FindBusInputDefault;
                                break;
                            case PageId.ParkAndRideInput:
                                te = TransitionEvent.ParkAndRideInput;
                                break;
                            case PageId.VisitPlannerInput:
                                te = TransitionEvent.VisitPlannerInputBack;
                                break;
                            case PageId.FindCycleInput:
                                te = TransitionEvent.FindCycleInputDefault;
                                break;
                            case PageId.ExtendJourneyInput:
                                te = TransitionEvent.ExtendJourneyInput;
                                break;
                            default:
                                te = TransitionEvent.FindMapResult;
                                break;
                        }
                    }
                    else if (returnObj is TransitionEvent)
                    {
                        te = (TransitionEvent)returnObj;
                    }
                }

                
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = te;
                
            }
        }

        /// <summary>
        /// Updates the From Location of the Journey Parameters with the selected location.
        /// </summary>
        private void UpdateFromLocation()
        {
                        
            journeyParameters.Origin = inputPageState.MapLocationSearch;
            journeyParameters.Origin.SearchType = SearchType.Map;
            journeyParameters.OriginLocation = inputPageState.MapLocation;
            journeyParameters.OriginLocation.SearchType = SearchType.Map;
            
            // Update the input text in the location search
            journeyParameters.Origin.InputText = journeyParameters.OriginLocation.Description;
            TDSessionManager.Current.JourneyParameters = journeyParameters;
        }

        /// <summary>
        /// Updates the To Location of the Journey Parameters with the selected location.
        /// </summary>
        private void UpdateToLocation()
        {
            journeyParameters.Destination = inputPageState.MapLocationSearch;
            journeyParameters.Destination.SearchType = SearchType.Map;
            // Get the location from InputPageState
            journeyParameters.DestinationLocation = inputPageState.MapLocation;
            journeyParameters.DestinationLocation.SearchType = SearchType.Map;
            // Update the input text in the location search
            journeyParameters.Destination.InputText = journeyParameters.DestinationLocation.Description;
            TDSessionManager.Current.JourneyParameters = journeyParameters;
        }

        /// <summary>
        /// Updates the Via Location in TDJourneyParameters
        /// </summary>
        private void UpdateViaLocation()
        {
            // This only applies to Multi Modal journeys

            // Create the location
            if (inputPageState.MapType == CurrentLocationType.PrivateVia)
            {
                journeyParameters.PrivateVia = inputPageState.MapLocationSearch;
                journeyParameters.PrivateViaLocation = inputPageState.MapLocation;
                journeyParameters.PrivateVia.InputText = journeyParameters.PrivateViaLocation.Description;
                journeyParameters.PrivateVia.SearchType = SearchType.Map;
                journeyParameters.PrivateViaLocation.SearchType = SearchType.Map;
            
            }
            else if (inputPageState.MapType == CurrentLocationType.CycleVia)
            {
                journeyParameters.CycleVia = inputPageState.MapLocationSearch;
                journeyParameters.CycleViaLocation = inputPageState.MapLocation;
                journeyParameters.CycleVia.InputText = journeyParameters.CycleViaLocation.Description;
                journeyParameters.CycleVia.SearchType = SearchType.Map;
                journeyParameters.CycleViaLocation.SearchType = SearchType.Map;
            }
            else
            {
                journeyParameters.PublicVia = inputPageState.MapLocationSearch;
                journeyParameters.PublicViaLocation = inputPageState.MapLocation;
                journeyParameters.PublicVia.InputText = journeyParameters.PublicViaLocation.Description;
                journeyParameters.PublicVia.SearchType = SearchType.Map;
                journeyParameters.PublicViaLocation.SearchType = SearchType.Map;
            }
            TDSessionManager.Current.JourneyParameters = journeyParameters;
        }

        /// <summary>
        /// Updates the VisitPlannerOrigin Location Journey Parameters with the selected location.
        /// </summary>
        private void UpdateVisitOriginLocation()
        {
            //private TDJourneyParametersVisitPlan parameters;
            TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

            //parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;
            //TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters;
            InputPageState pageState = TDSessionManager.Current.InputPageState;

            parameters.SetLocationSearch(0, pageState.MapLocationSearch);
            parameters.SetLocation(0, pageState.MapLocation);
            //parameters.GetLocationSearch(0).l

            // Update the input text in the location search
            //parameters.Origin.InputText = parameters.OriginLocation.Description;

            //parameters.GetLocation(1).Description  
            parameters.GetLocationSearch(0).InputText = parameters.GetLocation(0).Description;
            
        }

        /// <summary>
        /// Updates the VisitPlannerLocation1  Journey Parameters with the selected location.
        /// </summary>
        private void UpdateVisitLocation1()
        {
            //private TDJourneyParametersVisitPlan parameters;
            TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

            //parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;
            //TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters;
            InputPageState pageState = TDSessionManager.Current.InputPageState;

            parameters.SetLocationSearch(1, pageState.MapLocationSearch);
            parameters.SetLocation(1, pageState.MapLocation);
            //parameters.GetLocationSearch(0).l

            // Update the input text in the location search
            //parameters.Origin.InputText = parameters.OriginLocation.Description;

            //parameters.GetLocation(1).Description  
            parameters.GetLocationSearch(1).InputText = parameters.GetLocation(1).Description;

        }

        /// <summary>
        /// Updates the VisitPlannerLocation2 Location Journey Parameters with the selected location.
        /// </summary>
        private void UpdateVisitLocation2()
        {
            //private TDJourneyParametersVisitPlan parameters;
            TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

            //parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;
            //TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters;
            InputPageState pageState = TDSessionManager.Current.InputPageState;

            parameters.SetLocationSearch(2, pageState.MapLocationSearch);
            parameters.SetLocation(2, pageState.MapLocation);
            //parameters.GetLocationSearch(0).l

            // Update the input text in the location search
            //parameters.Origin.InputText = parameters.OriginLocation.Description;

            //parameters.GetLocation(1).Description  
            parameters.GetLocationSearch(2).InputText = parameters.GetLocation(2).Description;

        }
        #endregion
    }
}
