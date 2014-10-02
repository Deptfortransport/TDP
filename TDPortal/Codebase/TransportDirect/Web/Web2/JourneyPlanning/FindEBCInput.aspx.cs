// *********************************************** 
// NAME                 : FindEBCInput.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 14/09/2008
// DESCRIPTION          : Input Page for Find Enivronmental Benefits Calculator (EBC) planner 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindEBCInput.aspx.cs-arc  $
//
//   Rev 1.7   Jul 28 2011 16:19:56   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.6   May 13 2010 13:05:22   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.5   Jan 29 2010 14:45:28   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.4   Oct 21 2009 13:49:08   mmodi
//Updated images displayed in title area
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Oct 15 2009 13:42:18   apatel
//EBC printer friendly page changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 16:25:58   mmodi
//Updated to set EBC unavailable error
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 09 2009 10:28:00   mmodi
//Added information panel and label
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Sep 21 2009 15:01:44   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.SuggestionLinkService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;

using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Input page for find EBC
    /// </summary>
    public partial class FindEBCInput : TDPage
    {
        #region Private members

        // Session variables
        private ITDSessionManager sessionManager;
        private TDJourneyParametersMulti journeyParameters;
        private InputPageState inputPageState;
        private FindEBCPageState findEBCPageState;

        // Helpers
        private FindEBCInputAdapter findEBCInputAdapter;

        // Tracks if the via NewLocation button was clicked, to allow visibility of control to be set
        private bool viaNewLocationClicked = false;
        #endregion

        #region Constructor
        /// <summary>
		/// Default Constructor
		/// </summary>
		public FindEBCInput()
		{
            pageId = PageId.FindEBCInput;
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
            pageOptionsControltop.Clear += new EventHandler(pageOptionsControltop_Clear);
            pageOptionsControltop.ShowAdvancedOptions += new EventHandler(pageOptionsControltop_ShowAdvancedOptions);
            pageOptionsControltop.HideAdvancedOptions += new EventHandler(pageOptionsControltop_HideAdvancedOptions);

            commandBack.Click += new EventHandler(pageOptionsControltop_Back);

            locationsControl.NewLocationFrom += new EventHandler(locationsControl_NewLocationFrom);
            locationsControl.NewLocationTo += new EventHandler(locationsControl_NewLocationTo);
            locationsControl.FromLocationControl.TriLocationControl.MapClick += new EventHandler(locationsControl_MapFromClick);
            locationsControl.ToLocationControl.TriLocationControl.MapClick += new EventHandler(locationsControl_MapToClick);

            viaFindLocationControl.NewLocation += new EventHandler(viaFindLocationControl_NewLocation);
            viaFindLocationControl.TriLocationControl.MapClick += new EventHandler(viaFindLocationControl_MapClick);

            // Event Handler for default action button
            headerControl.DefaultActionEvent += new EventHandler(pageOptionsControltop_Submit);
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
            if (Page.IsPostBack)
            {
                UpdateControls();
            }
            else
            {
                InitialRequestSetup();
            }
            #endregion

            #region Check for Amend mode
            //check if we are in amend mode and if true, set the location controls to editable
            locationsControl.FromLocationControl.AmendMode = findEBCPageState.AmendMode;
            locationsControl.ToLocationControl.AmendMode = findEBCPageState.AmendMode;
            viaFindLocationControl.AmendMode = findEBCPageState.AmendMode;
            if (findEBCPageState.AmendMode)
            {
                journeyParameters.DestinationType.Type = ControlType.Default;
                journeyParameters.OriginType.Type = ControlType.Default;
                journeyParameters.PrivateViaType.Type = ControlType.Default;
            }
            #endregion

            findEBCInputAdapter.InitialiseControls(locationsControl, viaFindLocationControl);
                        
            // Adding client side script for user navigation (when user hit enter, it should take the default action)
            UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

            LoadHelpText();

            ClearMapViewStates();
        }

        /// <summary>
		/// Page PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            #region Display validation errors
            // Check if there are any validation errors in the session and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.ValidationError);

            findEBCInputAdapter.UpdateErrorMessages(
                panelErrorMessage, labelErrorMessages, TDSessionManager.Current.ValidationError);
            #endregion

            #region Display/Show controls

            commandBack.Visible = findEBCPageState.AmbiguityMode;

            pageOptionsControltop.AllowBack = false; // Only ever want to show the Back button at top of page
            pageOptionsControltop.AllowHideAdvancedOptions = false; // Only ever want to show the Hide button at the bottom of preferences control

            // Set visibility of buttons dependent on preferences visibility
            pageOptionsControltop.AllowShowAdvancedOptions = false;
            pageOptionsControltop.AllowNext = true;
            pageOptionsControltop.AllowClear = true;

            // When in page ambiguity mode
            if (findEBCPageState.AmbiguityMode)
            {
                // Don't allow user to show advanced options
                pageOptionsControltop.AllowShowAdvancedOptions = false;

                // Set visibility of Via location dependent on text entered and if new location was clicked
                if ((string.IsNullOrEmpty(viaFindLocationControl.TheSearch.InputText)) &&
                    !viaNewLocationClicked)
                {
                    // No search text entered, so hide via location control
                    viaFindLocationControl.Visible = false;
                }
            }

            #endregion

            #region Hide controls when in ambiguity mode
            // hide from to title in ambiguity mode!
            labelFromToTitle.Visible = !findEBCPageState.AmbiguityMode;

            // hide information text
            panelInformation.Visible = !findEBCPageState.AmbiguityMode;

            //Hide the advanced text if required:
            if (findEBCPageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }
            #endregion

            LoadResources();

            LoadLeftHandNavigation();

            SetupSkipLinksAndScreenReaderText();
        }

        #endregion

        #region Private methods
        
        /// <summary>
        /// Loads text and images on the page
        /// </summary>
        private void LoadResources()
        {
            PageTitle = GetResource("EBCPlanner.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            imageFindEBC1.ImageUrl = GetResource("HomeTipsTools.imageFindEBC1.ImageUrl");
            imageFindEBC1.AlternateText = " ";

            imageFindEBC2.ImageUrl = GetResource("HomeTipsTools.imageFindEBC2.ImageUrl");
            imageFindEBC2.AlternateText = " ";

            imageFindEBC3.ImageUrl = GetResource("HomeTipsTools.imageFindEBC3.ImageUrl");
            imageFindEBC3.AlternateText = " ";

            labelFindPageTitle.Text = GetResource("EBCPlanner.labelFindPageTitle.Text");

            labelFromToTitle.Text = GetResource("EBCPlanner.FindEBCInput.labelFromToTitle");

            labelInformation.Text = GetResource("EBCPlanner.FindEBCInput.Information.Text");

            commandBack.Text = GetResource("EBCPlanner.FindEBCInput.CommandBack.Text");
        }

        /// <summary>
        /// Loads the left hand navigation on the page
        /// </summary>
        private void LoadLeftHandNavigation()
        {
            // Navigation links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);

            // Related links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindEBCInput);
            expandableMenuControl.AddExpandedCategory("Related links");

            // Client link
            ConfigureLeftMenu("FindEBCInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
        }

        /// <summary>
        /// Loads the Help button text
        /// </summary>
        private void LoadHelpText()
        {
            if (sessionManager.FindPageState.AmbiguityMode)
            {
                Helpbuttoncontrol1.HelpUrl = GetResource("FindEBCInput.HelpAmbiguityUrl");
            }
            else
            {
                Helpbuttoncontrol1.HelpUrl = GetResource("FindEBCInput.HelpPageUrl");
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
            imageInputFormSkipLink.AlternateText = GetResource("EBCPlanner.FindEBCInput.imageInputFormSkipLink.AlternateText");
        }

        

        /// <summary>
        /// Performs initialisation when page is loaded for the first time. 
        /// </summary>
        private void InitialRequestSetup()
        {
            sessionManager = TDSessionManager.Current;

            #region Clear cache of journey data
            ClearCacheHelper helper = new ClearCacheHelper();

            // Force clear of any printable information if added by the journey result page
            helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

            // Fix for IR5481 Session issue when going from between different planners using the left hand menu
            if (sessionManager.FindAMode != FindAMode.EnvironmentalBenefits)
            {
                // We have come directly from another planner so clear results from session.
                helper.ClearJourneyResultCache();
            }
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
                resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.EnvironmentalBenefits);
            }

            // Get the session values needed by this page
            findEBCPageState = (FindEBCPageState)sessionManager.FindPageState;
            journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = sessionManager.InputPageState;
            
            findEBCInputAdapter = new FindEBCInputAdapter(journeyParameters, findEBCPageState, inputPageState);

            if (resetDone)
            {
                // Reset the journey parameters for EBC mode
                findEBCInputAdapter.InitJourneyParameters();
            }
            else if (!resetDone && sessionManager.Authenticated && !findEBCPageState.AmendMode)
            {
                // Ensure travel preferences are still loaded when we didn't reset the journey parameters.
                // This fixes scenario where user goes to input page -> logon -> input page
                findEBCInputAdapter.LoadTravelDetails();
            }
        }

        /// <summary>
        /// Updates session data with control values for those controls that do not raise events
        /// to signal that user has changed values (i.e. the date controls)
        /// </summary>
        private void UpdateControls()
        {
            // Get the session values needed by this page
            sessionManager = TDSessionManager.Current;
            findEBCPageState = (FindEBCPageState)sessionManager.FindPageState;
            journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = sessionManager.InputPageState;

            findEBCInputAdapter = new FindEBCInputAdapter(journeyParameters, findEBCPageState, inputPageState);
            
            // No journey parameters to update
                       
            // No date controls to update
        }

        /// <summary>
        /// Clears Map view states
        /// </summary>
        private void ClearMapViewStates()
        {
            TDSessionManager.Current.StoredMapViewState[TDSessionManager.OUTWARDMAP] = null;
            TDSessionManager.Current.StoredMapViewState[TDSessionManager.RETURNMAP] = null;

        }

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
        /// Event handler called when clear page button is clicked. Journey parameters are reset
        /// to initial values, page controls updated and the page set to input mode.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void pageOptionsControltop_Clear(object sender, EventArgs e)
        {
            // Reset all the journey parameters, and initialise for cycle planner
            findEBCInputAdapter.InitJourneyParameters();

            findEBCPageState.AmbiguityMode = false;

            // Because we've reset the journey parameters, update the controls on this 
            // page with these new values
            findEBCInputAdapter.InitialiseControls(locationsControl, viaFindLocationControl);
            
            // Clear out any validation errors
            sessionManager.ValidationError = null;

            TDPage.CloseAllSingleWindows(Page);
        }

        /// <summary>
        /// Added event for pageOptionsControltop which raises when user clicks AdvanceOptions.
        /// this will show advance options and hide pageOptionsControltop control. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageOptionsControltop_ShowAdvancedOptions(object sender, EventArgs e)
        {
            // Advanced options currently unsupported on this input page
        }

        /// <summary>
        /// Added event for pageOptionsControltop which raises when user hides AdvanceOptions.
        /// this will hide advance options. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageOptionsControltop_HideAdvancedOptions(object sender, EventArgs e)
        {
            // Advanced options currently unsuppported on this input page
        }

        /// <summary>
        /// Event handler called when back button clicked in ambiguous mode. Decrements the level of
        /// hierarchical location searches (origin, destination and via). If all locations are at highest 
        /// level then page is reverted to input mode and original input parameters reinstated.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void pageOptionsControltop_Back(object sender, EventArgs e)
        {
            if (findEBCInputAdapter.IsAtHighestLevel())
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

                TDSessionManager.Current.ValidationError.Initialise();

                findEBCPageState.AmbiguityMode = false;
                findEBCPageState.ReinstateJourneyParameters(journeyParameters);

                
                findEBCInputAdapter.InitLocationsControl(locationsControl);

                if (locationsControl.FromLocationControl.TheLocation.Status != TDLocationStatus.Valid)
                {
                    locationsControl.FromLocationControl.SetLocationUnspecified();
                }
                if (locationsControl.ToLocationControl.TheLocation.Status != TDLocationStatus.Valid)
                {
                    locationsControl.ToLocationControl.SetLocationUnspecified();
                }
                if (viaFindLocationControl.TheLocation.Status != TDLocationStatus.Valid)
                {
                    viaFindLocationControl.SetLocationUnspecified();
                }
            }
            else
            {
                // Decrement the drilldown level of any ambiguous locations
                // that are not yet at their highest level
                journeyParameters.Origin.DecrementLevel();
                journeyParameters.Destination.DecrementLevel();
                journeyParameters.PrivateVia.DecrementLevel();
            }

            pageOptionsControltop.AllowBack = false;
            pageOptionsControltop.AllowClear = true;
            pageOptionsControltop.AllowNext = true;
            pageOptionsControltop.AllowHideAdvancedOptions = false;
            pageOptionsControltop.AllowShowAdvancedOptions = false;
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "from" location.
        /// The journey parameters are updated with the "from" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void locationsControl_NewLocationFrom(object sender, EventArgs e)
        {
            journeyParameters.OriginLocation = locationsControl.FromLocationControl.TheLocation;
            journeyParameters.Origin = locationsControl.FromLocationControl.TheSearch;
            journeyParameters.Origin.SearchType = SearchType.Locality;
            journeyParameters.OriginType = locationsControl.FromLocationControl.LocationControlType;
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "to" location.
        /// The journey parameters are updated with the "to" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void locationsControl_NewLocationTo(object sender, EventArgs e)
        {
            journeyParameters.DestinationLocation = locationsControl.ToLocationControl.TheLocation;
            journeyParameters.Destination = locationsControl.ToLocationControl.TheSearch;
            journeyParameters.Destination.SearchType = SearchType.Locality;
            journeyParameters.DestinationType = locationsControl.ToLocationControl.LocationControlType;
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "via" location.
        /// The journey parameters are updated with the "via" location control's new values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viaFindLocationControl_NewLocation(object sender, EventArgs e)
        {
            // Set the flag to allow display of via control in page_prerender
            viaNewLocationClicked = true;

            // Clear out the existing locations and search as the control does not do this
            viaFindLocationControl.TheLocation = new TDLocation();
            viaFindLocationControl.TheSearch = new LocationSearch();
            viaFindLocationControl.LocationControlType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);

            journeyParameters.PrivateViaLocation = viaFindLocationControl.TheLocation;
            journeyParameters.PrivateVia = viaFindLocationControl.TheSearch;
            journeyParameters.PrivateVia.SearchType = SearchType.Locality;
            journeyParameters.PrivateViaType = viaFindLocationControl.LocationControlType;
        }

        /// <summary>
        /// Event handler for when Find on map is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void locationsControl_MapFromClick(object sender, EventArgs e)
        {
            // Map button currently unsupported on this input page
        }

        /// <summary>
        /// Event handler for when Find on map is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void locationsControl_MapToClick(object sender, EventArgs e)
        {
            // Map button currently unsupported on this input page
        }

        /// <summary>
        /// Event handler for whe Find on map is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viaFindLocationControl_MapClick(object sender, EventArgs e)
        {
            // Map button currently unsupported on this input page
        }

        /// <summary>
        /// Event handler when Outward date calendar used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e)
        {
            // Date control not available on page
        }

        /// <summary>
        /// Event handler when Return data calendar used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateControlReturnDateControl_DateChanged(object sender, EventArgs e)
        {
            // Date control not available on page
        }

        #endregion

        #region SubmitRequest
        /// <summary>
        /// Method containing the Submit functionality so that it
        /// can be called by the Landing page when AutoPlanning is required. 
        /// </summary>
        private void SubmitRequest()
        {
            if (!findEBCPageState.AmbiguityMode)
            {
                // Initial search
                findEBCPageState.SaveJourneyParameters(journeyParameters);
                findEBCInputAdapter.AmbiguitySearch(locationsControl, viaFindLocationControl);
            }
            else
            {
                // Ambiguouse location search
                locationsControl.FromLocationControl.Search();
                locationsControl.ToLocationControl.Search();
                viaFindLocationControl.Search();
            }

            // Call method to validate preferences, and save back to the journey parameters
            UpdateControls();

            // Set up the JourneyPlanControlData
            findEBCInputAdapter.InitialiseAsyncCallState();

            if (FindEBCInputAdapter.EBCPlannerAvailable)
            {
                // Validate the JourneyParameters
                JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

                findEBCPageState.AmbiguityMode = !runner.ValidateAndRun(
                     TDSessionManager.Current,
                     TDSessionManager.Current.JourneyParameters,
                     GetChannelLanguage(TDPage.SessionChannelName),
                     true);
            }
            else // Planner not available
            {
                // Set validation message indicating this planner is currently not available
                sessionManager.ValidationError = new ValidationError();
                sessionManager.ValidationError.ErrorIDs = new ValidationErrorID[1] {ValidationErrorID.EnvironmentalBenefitsCalculatorUnavailable};
                sessionManager.ValidationError.MessageIDs = new Hashtable();
                sessionManager.ValidationError.MessageIDs.Add(ValidationErrorID.EnvironmentalBenefitsCalculatorUnavailable, "EBCPlanner.EnvironmentalBenefitsCalculatorUnavailable");
                
                findEBCPageState.AmbiguityMode = true;
            }

            if (findEBCPageState.AmbiguityMode)
            {
                if (!TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan])
                {
                    // Nothing to do here
                }
                else
                {
                    findEBCPageState.AmbiguityMode = false;
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
            if (findEBCPageState.AmendMode)
            {
                //Disable AmendMode when leaving the page
                findEBCPageState.AmendMode = false;

                // Refresh location control after amend mode changed to make sure the location
                // is up to date 
                locationsControl.FromLocationControl.AmendMode = findEBCPageState.AmendMode;
                locationsControl.ToLocationControl.AmendMode = findEBCPageState.AmendMode;
                viaFindLocationControl.AmendMode = findEBCPageState.AmendMode;

                locationsControl.FromLocationControl.RefreshTristateControl(false);
                locationsControl.ToLocationControl.RefreshTristateControl(false);
                viaFindLocationControl.TriLocationControl.ForceRefresh();
            }
            
        }
        #endregion
    }
}
