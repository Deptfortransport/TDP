// *********************************************** 
// NAME                 : FindInternationalInput.aspx.cs 
// AUTHOR               : Amit Patel 
// DATE CREATED         : 11/02/2010 
// DESCRIPTION			: Input page for international journey planner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindInternationalInput.aspx.cs-arc  $
//
//   Rev 1.13   Jul 28 2011 16:20:06   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.12   May 13 2010 13:05:24   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.11   Apr 23 2010 16:34:18   mmodi
//Update location control drop down list with cities to show in them, this is to restrict selection if on an ambiguity page and a location has been resolved.
//Resolution for 5521: TD Extra - Drop Down List Change - CCN0575
//
//   Rev 1.10   Mar 04 2010 17:32:42   rbroddle
//Added event handler for calendar control
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Feb 26 2010 15:51:16   mmodi
//Help button shouldnt be shown
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 26 2010 11:12:58   mmodi
//Setup the Help button
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 25 2010 14:33:22   mmodi
//Inject "Central " into the location description names for displaying on the results pages
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 25 2010 11:59:52   apatel
//Added Related Links
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Feb 24 2010 13:24:44   mmodi
//Reset Emissions page state to ensure emissions and distances are correctly displayed when going to journey details page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Feb 21 2010 23:23:06   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 18 2010 17:14:58   mmodi
//Retain modes selected when amending journey
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 16 2010 17:53:36   mmodi
//Updates
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 16 2010 11:16:08   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.InternationalPlanner;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Input page for find international journey
    /// </summary>
    public partial class FindInternationalInput : TDPage
    {
        #region Private Fields

        private InputPageState inputPageState;

        /// <summary>
        /// Holds user's current page state for this page
        /// </summary>
        private FindInternationalPageState pageState;

        /// <summary>
        /// Hold user's current journey parameters for trunk journey search
        /// </summary>
        private TDJourneyParametersMulti journeyParams;

        /// <summary>
        /// Helper class responsible for common methods to Find A pages
        /// </summary>
        private FindInternationalInputAdapter findInputAdapter; 

        #endregion Private Fields

        #region Constructor

        /// <summary>
		/// Constructor.
		/// </summary>
		public FindInternationalInput()
		{
			this.pageId = PageId.FindInternationalInput;
		}

        #endregion Constructor

		#region Page Event Handlers

        /// <summary>
        /// Performs page initialisation including event wiring.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            locationsControl.NewLocationFrom += new EventHandler(locationsControl_NewLocationFrom);
            locationsControl.NewLocationTo += new EventHandler(locationsControl_NewLocationTo);

            pageOptionsControl.Submit += new EventHandler(pageOptions_Submit);
            pageOptionsControl.Back += new EventHandler(pageOptions_Back);
            pageOptionsControl.Clear += new EventHandler(pageOptions_Clear);

            modeSelectControl.TravelModeChanged +=new EventHandler(modeSelectControl_TravelModeChanged);
            dateControl.LeaveDateControl.DateChanged += new EventHandler(dateControl_DateChanged);
        }

        /// <summary>
        /// Page Load method
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            if (Page.IsPostBack)
            {
                UpdateOtherControls();
            }
            else
            {
                InitialRequestSetup();
            }

            SetupControls();

            findInputAdapter.InitialiseControls(locationsControl);

            LoadHelpText();
        }

        

		/// <summary>
		/// Page PreRender method
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
            #region Display validation errors
            // Check if there are any validation errors in the session and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.ValidationError);

            findInputAdapter.UpdateErrorMessages(
                panelErrorMessage, labelErrorMessages, TDSessionManager.Current.ValidationError);
            #endregion

            #region Restrict input city dropdown values if necessary

            InternationalPlannerHelper helper = new InternationalPlannerHelper();
            
            // Apply restrictions to the location dropdown control if necessary, 
            // e.g. when on ambiguity page and one of the locations is resolved, then want to only show
            // the valid locations in the other location dropdown
            if ((journeyParams.OriginLocation.Status == TDLocationStatus.Valid)
                && (journeyParams.DestinationLocation.Status != TDLocationStatus.Valid))
            {
                InternationalCity[] cities = helper.GetValidRouteCitiesForInternationCity(journeyParams.OriginLocation.CityId);

                ArrayList cityIds = new ArrayList();

                foreach(InternationalCity city in cities)
                {
                    cityIds.Add(city.CityID);
                }

                locationsControl.SetScriptableDropdownList(null, cityIds);
            }
            else if ((journeyParams.DestinationLocation.Status == TDLocationStatus.Valid)
                && (journeyParams.OriginLocation.Status != TDLocationStatus.Valid))
            {
                InternationalCity[] cities = helper.GetValidRouteCitiesForInternationCity(journeyParams.DestinationLocation.CityId);

                ArrayList cityIds = new ArrayList();

                foreach (InternationalCity city in cities)
                {
                    cityIds.Add(city.CityID);
                }

                locationsControl.SetScriptableDropdownList(cityIds, null);
            }

            #endregion

            #region Set locations to be amendable
            // Set dropdowns to amendable
            if ((!Page.IsPostBack) && (pageState.AmendMode))
            {
                locationsControl.SetLocationsAmendable();
            }
            #endregion

            // Update the date control
            // don't want to update the date when not postback...
            // otherwise double initial population
            if (Page.IsPostBack)
            {
                findInputAdapter.UpdateDateControl(dateControl);
            }
            
            LoadResources();

            LoadLeftHandNavigation();

            SetupSkipLinksAndScreenReaderText();
		}
		
        #endregion Page Event Handlers

        #region Page Control Event Handlers
        /// <summary>
        /// Event handler called when new location button is clicked for the "from" location.
        /// The journey parameters are updated with the "from" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void locationsControl_NewLocationFrom(object sender, EventArgs e)
        {
            journeyParams.OriginLocation = locationsControl.FromLocationControl.TheLocation;
            journeyParams.Origin = locationsControl.FromLocationControl.TheSearch;
            journeyParams.Origin.SearchType = SearchType.Locality;
            journeyParams.Origin.DisableGisQuery();
            journeyParams.OriginType = locationsControl.FromLocationControl.LocationControlType;
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "to" location.
        /// The journey parameters are updated with the "to" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void locationsControl_NewLocationTo(object sender, EventArgs e)
        {
            journeyParams.DestinationLocation = locationsControl.ToLocationControl.TheLocation;
            journeyParams.Destination = locationsControl.ToLocationControl.TheSearch;
            journeyParams.Destination.SearchType = SearchType.Locality;
            journeyParams.Destination.DisableGisQuery();
            journeyParams.DestinationType = locationsControl.ToLocationControl.LocationControlType;
        }

        /// <summary>
        /// Event handler called when back button clicked in ambiguous mode. Decrements the level of
        /// hierarchical location searches (origin, destination and public via). If all locations are at highest 
        /// level then page is reverted to input mode and original input parameters reinstated.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void pageOptions_Back(object sender, EventArgs e)
        {
            dateControl.CalendarClose();

            TDSessionManager.Current.ValidationError = null;
            if (findInputAdapter.IsAtHighestLevel())
            {
                //Check to see if there is a previous page waiting on the stack
                if (inputPageState.JourneyInputReturnStack.Count != 0)
                {
                    TransitionEvent lastPage = (TransitionEvent)inputPageState.JourneyInputReturnStack.Pop();
                    //If the user is returing to the previous journey results, re-validate them
                    if (lastPage == TransitionEvent.FindAInputRedirectToResults)
                    {
                        TDSessionManager.Current.JourneyResult.IsValid = true;
                    }
                    TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = lastPage;
                    return;
                }

                pageState.AmbiguityMode = false;
                pageState.ReinstateJourneyParameters(journeyParams);
                dateControl.LeaveDateControl.AmbiguityMode = false;
                dateControl.LeaveDateControl.DateErrors = null;
               
                findInputAdapter.InitLocationsControl(locationsControl);

                //if (locationsControl.FromLocationControl.TheLocation.Status != TDLocationStatus.Valid)
                //{
                //    locationsControl.FromLocationControl.SetLocationUnspecified();
                //}
                //if (locationsControl.ToLocationControl.TheLocation.Status != TDLocationStatus.Valid)
                //{
                //    locationsControl.ToLocationControl.SetLocationUnspecified();
                //}
            }
            else
            {
                // Decrement the drilldown level of any ambiguous locations
                // that are not yet at their highest level
                journeyParams.Origin.DecrementLevel();
                journeyParams.Destination.DecrementLevel();
            }
        }

        /// <summary>
        /// Event handler called when clear page button is clicked. Journey parameters are reset
        /// to initial values, page controls updated and the page set to input mode.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void pageOptions_Clear(object sender, EventArgs e)
        {
            findInputAdapter.InitJourneyParameters();
            pageState.AmbiguityMode = false;
            findInputAdapter.InitialiseControls(locationsControl);
            dateControl.LeaveDateControl.DateErrors = null;
            dateControl.LeaveDateControl.AmbiguityMode = false;
            TDSessionManager.Current.ValidationError = null;
            TDPage.CloseAllSingleWindows(Page);

            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindInternationalInputDefault;
            TDSessionManager.Current.FormShift[SessionKey.ForceRedirect] = true;
            
        }

        /// <summary>
        /// Event handler called when next button clicked. The journey plan runner component validates the 
        /// current journey parameters. If invalid then the page is put into ambiguity mode otherwise 
        /// journey planning commences. User preferences are saved before commencing journey planning.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void pageOptions_Submit(object sender, EventArgs e)
        {
            SubmitRequest();
        }

        /// <summary>
        /// Event handler for ModeSelectControl's travel mode changed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modeSelectControl_TravelModeChanged(object sender, EventArgs e)
        {
            List<ModeType> publicModes = new List<ModeType>();

            if (modeSelectControl.CoachSelected)
            {
                publicModes.Add(ModeType.Coach);
            }

            if (modeSelectControl.RailSelected)
            {
                publicModes.Add(ModeType.Rail);
            }

            if (modeSelectControl.AirSelected)
            {
                publicModes.Add(ModeType.Air);
            }

            TDSessionManager.Current.JourneyParameters.PublicModes = publicModes.ToArray();
        }

        #endregion

        /// <summary>
        /// Event handler called when date selected from calendar control. The journey parameters for the outward
        /// date are updated with the calendar date selection.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void dateControl_DateChanged(object sender, EventArgs e)
        {
            journeyParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
            journeyParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
        }


        #region Private Methods

        /// <summary>
        /// Loads text and images on the page
        /// </summary>
        private void LoadResources()
        {
            labelFindPageTitle.Text = GetResource("FindInternationalInput.labelFindPageTitle");

            imageFindInternational.ImageUrl = GetResource("HomeDefault.imageFindInternationalInput.ImageUrl");
            imageFindInternational.AlternateText = " ";

            labelFromToTitle.Text = GetResource("FindInternationalInput.labelFromToTitle");
            labelFromToTitle.Visible = !pageState.AmbiguityMode;

            PageTitle = GetResource("FindInternationalInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
        }

        /// <summary>
        /// Loads the left hand navigation on the page
        /// </summary>
        private void LoadLeftHandNavigation()
        {
            // Navigation links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            // Related links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindInternationalInput);
            expandableMenuControl.AddExpandedCategory("Related links");

            // Client link
            ConfigureLeftMenu("FindInternationalInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
        }

        /// <summary>
        /// Loads the Help button text
        /// </summary>
        private void LoadHelpText()
        {
            // Same help text for ambiguity as non-ambiguity
            if (inputPageState.AmbiguityMode)
            {
                Helpbuttoncontrol1.HelpUrl = GetResource("FindInternationalInput.HelpPageUrl");
            }
            else
            {
                Helpbuttoncontrol1.HelpUrl = GetResource("FindInternationalInput.HelpPageUrl");
            }

            // No help button should currently be shown
            Helpbuttoncontrol1.Visible = false; 
        }

        /// <summary>
        /// Method to setup the controls
        /// </summary>
        private void SetupControls()
        {
            dateControl.LeaveDateControl.DateControl.TimeControlsVisible = false;
            dateControl.ReturnDateControl.Visible = false;
            dateControl.ShowPlanningTip = false;

            if (!IsPostBack)
            {
                modeSelectControl.AirSelected = false;
                modeSelectControl.CoachSelected = false;
                modeSelectControl.RailSelected = false;

                // Set modes selected from journey parameters
                foreach (ModeType modeType in journeyParams.PublicModes)
                {
                    switch (modeType)
                    {
                        case ModeType.Air:
                            modeSelectControl.AirSelected = true;
                            break;
                        case ModeType.Coach:
                            modeSelectControl.CoachSelected = true;
                            break;
                        case ModeType.Rail:
                            modeSelectControl.RailSelected = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Updates session data with control values for those controls which do not 
        /// raise events to signal that user has changed values (i.e. the date controls)
        /// </summary>
        private void UpdateOtherControls()
        {
            pageState = (FindInternationalPageState)TDSessionManager.Current.FindPageState;
            journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = TDSessionManager.Current.InputPageState;
            findInputAdapter = new FindInternationalInputAdapter(journeyParams, pageState, inputPageState);
            findInputAdapter.UpdateJourneyDates(dateControl);
        }

        /// <summary>
        /// Performs initialisation when page is loaded for the first time. This includes delegating to
        /// session manager by calling InitialiseJourneyParametersPageStates to create a new session data
        /// if necesssary (i.e. journey parameters and page state objects). If session data contains
        /// journey results then the user is redirected to the journey summary page.
        /// </summary>
        private void InitialRequestSetup()
        {
            bool resetDone;
                        
            ITDSessionManager sessionManager = TDSessionManager.Current;
            TDItineraryManager itineraryManager = TDItineraryManager.Current;

            #region Clear cache of journey data
            // if an extension is in progress, cancel it
            sessionManager.ItineraryManager.CancelExtension();

            ClearCacheHelper helper = new ClearCacheHelper();

            // Force clear of any printable information if added by the journey result page
            helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

            // Fix for IR5481 Session issue when going from between different planners using the left hand menu
            if (sessionManager.FindAMode != FindAMode.International)
            {
                // We have come directly from another planner so clear results from session.
                helper.ClearJourneyResultCache();
            }
            #endregion

            // Next, Initialise JourneyParameters and PageStates if needed. 
            // Will return true, if reset has been performed.
            resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.International);

            // if in already in trunk mode but in station mode
            // And QueryString "ClassicMode" present, means user
            // clicked on one of the links... So reset page
            if (sessionManager.FindAMode == FindAMode.International
                && Request.QueryString["ClassicMode"] != null)
            {
                resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.International);
            }

            pageState = (FindInternationalPageState)TDSessionManager.Current.FindPageState;
            inputPageState = TDSessionManager.Current.InputPageState;
            journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            findInputAdapter = new FindInternationalInputAdapter(journeyParams, pageState, inputPageState);

            // Reset the journey emissions page state, in case coming from a previous planner where it was setup
            if (sessionManager.JourneyEmissionsPageState == null)
            {
                sessionManager.JourneyEmissionsPageState = new JourneyEmissionsPageState();
            }
            sessionManager.JourneyEmissionsPageState.Initialise();

            if (resetDone)
            {
                // if page state and journey parameters were re-instantiated, initialise to correct state
                // for this find A mode.
                findInputAdapter.InitJourneyParameters();
            }
            else
            {
                // Previously request might have injected "Central " to the start of the location description, remove it
                // to allow the location drop down to be correctly set
                string central = GetResource("JourneyDetailControl.location.city.prefix");
                if ((journeyParams.OriginLocation != null) && (journeyParams.OriginLocation.Description.StartsWith(central)))
                {
                    journeyParams.OriginLocation.Description = journeyParams.OriginLocation.Description.Replace(central, string.Empty);
                }
                if ((journeyParams.DestinationLocation != null) && (journeyParams.DestinationLocation.Description.StartsWith(central)))
                {
                    journeyParams.DestinationLocation.Description = journeyParams.DestinationLocation.Description.Replace(central, string.Empty);
                }
            }

            // initialise the date control from session (required as displaying help forces redirect)
            findInputAdapter.InitDateControl(dateControl);
        }

        /// <summary>
        /// Sets the text for the skip to links (for screenreader browsers).
        /// </summary>
        private void SetupSkipLinksAndScreenReaderText()
        {
            // Setup gif resource for images (1 invisible image for all skip links)
            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");

            imageInputFormSkipLink.ImageUrl = skipLinkImageUrl;
            imageInputFormSkipLink.AlternateText = GetResource("FindInternationalInput.imageInputFormSkipLink.AlternateText");
        }

        /// <summary>
        /// Method to submit the journey request
        /// </summary>
        private void SubmitRequest()
        {
            dateControl.CalendarClose();

            if (!pageState.AmbiguityMode)
            {
                pageState.SaveJourneyParameters(journeyParams);
                findInputAdapter.AmbiguitySearch(locationsControl);
            }
            else
            {
                locationsControl.FromLocationControl.Search();
                locationsControl.ToLocationControl.Search();
            }

            #region Update location description

            // Inject "Central " to the start of the location description
            string central = GetResource("JourneyDetailControl.location.city.prefix");
            if ((journeyParams.OriginLocation != null) && (!journeyParams.OriginLocation.Description.StartsWith(central)))
            {
                journeyParams.OriginLocation.Description = central + journeyParams.OriginLocation.Description;
            }
            if ((journeyParams.DestinationLocation != null) && (!journeyParams.DestinationLocation.Description.StartsWith(central)))
            {
                journeyParams.DestinationLocation.Description = central + journeyParams.DestinationLocation.Description;
            }

            #endregion

            findInputAdapter.InitialiseAsyncCallState();

            // Validate the JourneyParameters
            InternationalJourneyPlanRunner runner = new InternationalJourneyPlanRunner(Global.tdResourceManager);

            pageState.AmbiguityMode = !runner.ValidateAndRun(
                TDSessionManager.Current,
                TDSessionManager.Current.JourneyParameters,
                GetChannelLanguage(TDPage.SessionChannelName),
                true);
                        
            if (pageState.AmbiguityMode)
            {
                dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                dateControl.LeaveDateControl.AmbiguityMode = true;
               
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
        }	

        #endregion Private Methods
    }
}
