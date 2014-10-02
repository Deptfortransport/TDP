// *********************************************** 
// NAME                 : FindCoachInput.aspx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 03.08.04
// DESCRIPTION			: Input page for finding coach journeys.
// Allows user to specify details for a coach only journey and
// resolve any ambiguities before proceeding to journey planning
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindCoachInput.aspx.cs-arc  $
//
//   Rev 1.12   Mar 22 2013 10:49:10   DLane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.11   Jul 28 2011 16:19:52   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.10   Oct 28 2010 15:49:02   rbroddle
//Removed explicit wire up to Page_PreRender & Page_Init as AutoEventWireUp=true for this control so they were firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.9   May 13 2010 13:05:20   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.8   Mar 26 2010 11:57:38   MTurner
//Added code to clear journey results from session if you are coming directly from another planner.
//
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.7   Jan 29 2010 14:45:26   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.6   Jan 30 2009 10:44:14   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.5   May 22 2008 16:42:38   mmodi
//Updated to make locations editable
//Resolution for 4998: "Find Nearest" functionality on Find Train / Find Coach input not working
//Resolution for 5000: Find Train - when ambiguity page is diplayed, resolved locations still editable
//Resolution for 5002: Amend Find a train cost does not use the new location
//
//   Rev 1.4   May 01 2008 17:24:02   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.3   Apr 08 2008 15:55:54   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
// rev devfactory 8 apr sbarker
// Advanced text hidden when advanced pressed, or in ambiguity mode
//
//   Rev 1.2   Mar 31 2008 13:24:26   mturner
//Drop3 from Dev Factory
// 
//  Rev Devfactory Jan 30 2008 08:37:00 apatel
//  Modified to set PageOptionsControls inside the blue boxes. New PageOptionsControl added to page which
//  will be display/hide when hide/advance button will be clicked.
//
//   Rev 1.0   Nov 08 2007 13:29:26   mturner
//Initial revision.
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//   Rev 1.37   Sep 03 2007 15:24:52   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.36   Apr 25 2006 11:28:14   COwczarek
//Change logic in submitRequest method to set ambiguity mode
//to false if auto plan landing request
//Resolution for 3943: DN077 Landing Page: Dates not displayed on Ambiguity screen
//
//   Rev 1.35   Apr 24 2006 10:34:08   pscott
//I
//
//   Rev 1.34   Apr 13 2006 11:07:02   COwczarek
//Call ResetLandingPageSessionParameters in Unload event
//handler rather than PreRender event handler. This ensures
//parameters are reset even if redirect occurs due to autoplan
//being set.
//Resolution for 3902: Landing Page: Using Find A Car with autoplan set then clicking amend throws exception
//
//   Rev 1.33   Apr 12 2006 12:43:24   COwczarek
//Rearrange logic in Page_PreRender so that UpdateDateControl is not called on initial load if request is a landing page request.
//Resolution for 3773: Landing Page: Return date error when no return date or time specified
//
//   Rev 1.32   Mar 30 2006 10:37:28   halkatib
//Made page check if coming from landing page before initialising the journey paramters. Initialisation is not required on the page since the landing page does this already. When this happens twice in a landing page call the journeyparametes set by the landing are changed.
//
//   Rev 1.31   Mar 27 2006 10:20:38   kjosling
//Merged stream 0023 - Journey Results
//
//   Rev 1.30   Mar 22 2006 17:30:08   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.29   Mar 10 2006 12:42:08   pscott
//SCR3510
//Close Calendar Control when going to Ambiguity page
//
//   Rev 1.28   Feb 23 2006 19:14:04   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.27   Feb 10 2006 11:07:42   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.26   Jan 12 2006 18:16:06   RPhilpott
//Reset TDItineraryManager to default (mode "None") in page initialisation to allow for case where we are coming from VisitPlanner.
//Resolution for 3450: DEL 8: Server error when returning to Quickplanner results from Visit Planner input
//
//   Rev 1.25   Nov 25 2005 13:47:08   NMoorhouse
//Moved the setting on HelpUrl into the Page_Load (as they were being reset to null on post back)
//Resolution for 2990: UEE Post Build Enhancement: Add Help Pages to Input Pages
//
//   Rev 1.24   Nov 17 2005 17:11:16   NMoorhouse
//Update Input Page Help Page URLs
//Resolution for 2990: UEE Post Build Enhancement: Add Help Pages to Input Pages
//
//   Rev 1.23   Nov 10 2005 14:32:44   NMoorhouse
//TD093 UEE Input Pages - Soft Content
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.22   Nov 09 2005 17:16:40   RPhilpott
//Merge of stream2818
//
//   Rev 1.21   Nov 07 2005 12:05:30   ralonso
//labelFromToTitle added
//
//   Rev 1.20   Nov 04 2005 10:57:22   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.19   Nov 02 2005 14:47:24   jgeorge
//Undid previous change, as this was not complete and caused an additional bug. Fix for the original IR was made in FindLocationControl and FindViaLocationControl.
//Resolution for 2935: Del 7.2: Expanding Train Preferences prevents user planning journey
//
//   Rev 1.18   Oct 11 2005 17:05:32   kjosling
//Fixed problem switching to preferences. 
//Resolution for 2842: DN79 UEE Stage 1:  "No Options found" message displayed when opening the Journey Details section after pressing Back button on ambiguity page
//
//   Rev 1.17.2.3   Nov 02 2005 18:15:14   NMoorhouse
//TD93 - UEE Input Pages - Update format following analyst screen walk-through
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.17.2.2   Oct 28 2005 18:36:26   AViitanen
//TD093 Input page - Page formatting
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.17.2.1   Oct 06 2005 14:07:30   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.17.2.0   Oct 04 2005 16:33:58   mtillett
//Update the pages that use the FindToFromLocationControl
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.17   Sep 29 2005 12:50:26   build
//Automatically merged from branch for stream2673
//
//   Rev 1.16.1.0   Sep 09 2005 14:16:14   Schand
//DN079 UEE Enter Key.
//Updates for UEE.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.16   Apr 27 2005 10:20:34   COwczarek
//Fix compiler warnings
//
//   Rev 1.15   Apr 15 2005 12:48:10   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.14   Mar 08 2005 16:26:16   bflenk
//TimeOut functionality implemented in TDPage.cs, removed from this file - IR1720
//
//   Rev 1.13   Feb 23 2005 17:25:48   RAlavi
//This was temporarily changed but it is now checked in as version 1.11
//
//   Rev 1.12   Jan 28 2005 18:46:20   ralavi
//Updated for car costing
//
//   Rev 1.11   Nov 19 2004 11:31:58   asinclair
//Fix for IR 1720 Vantive 3482658
//
//   Rev 1.10   Oct 15 2004 12:39:06   jgeorge
//Changed to take account of new JourneyPlanStateData and changes to existing JourneyPlanControlData.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.9   Sep 23 2004 10:40:50   jmorrissey
//Added // $Log$

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.CommonWeb.Helpers;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
    /// Input page for finding coach journeys.
    /// Allows user to specify details for a coach only journey and
    /// resolve any ambiguities before proceeding to journey planning
	/// </summary>
    public partial class FindCoachInput : TDPage
	{

        /// <summary>
        /// Holds user's current page state for this page
        /// </summary>
        private FindCoachPageState pageState;

        /// <summary>
        /// Hold user's current journey parameters for coach only journey
        /// </summary>
        private TDJourneyParametersMulti journeyParams;

		/// <summary>
		/// Holds user's map current page state.
		/// </summary>
		private InputPageState inputPageState;

        /// <summary>
        /// Helper class responsible for common methods to Find A pages
        /// </summary>
        private FindCoachTrainInputAdapter findInputAdapter;

		/// <summary>
		/// Helper class for Landing Page functionality
		/// </summary>
		private LandingPageHelper landingPageHelper = new LandingPageHelper();


        protected TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl locationsControl;
        protected TransportDirect.UserPortal.Web.Controls.FindCoachTrainPreferencesControl preferencesControl;
        protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;
		protected HeaderControl headerControl;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FindCoachInput()
        {
            this.pageId = PageId.FindCoachInput;
        }

        #region Private Methods


        /// <summary>
        /// Updates session data with control values for those controls that do not raise events
        /// to signal that user has changed values (i.e. the date controls)
        /// </summary>
        private void updateOtherControls() 
        {
            pageState = (FindCoachPageState)TDSessionManager.Current.FindPageState;
            journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;            
            findInputAdapter = new FindCoachInputAdapter(journeyParams,pageState, inputPageState);
			inputPageState = TDSessionManager.Current.InputPageState;

            findInputAdapter.UpdateJourneyDates(dateControl);

        }

        /// <summary>
        /// Performs initialisation when page is loaded for the first time. This includes delegating to
        /// session manager by calling InitialiseJourneyParametersPageStates to create a new session data
        /// if necesssary (i.e. journey parameters and page state objects). If session data contains
        /// journey results then the user is redirected to the journey summary page.
        /// </summary>
        private void initialRequestSetup() 
        {

            ITDSessionManager sessionManager = TDSessionManager.Current;
			sessionManager.ItineraryMode = ItineraryManagerMode.None;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

            #region Clear cache of journey data
            // if an extension is in progress, cancel it
            sessionManager.ItineraryManager.CancelExtension();

            ClearCacheHelper helper = new ClearCacheHelper();

            // Force clear of any printable information if added by the journey result page
            helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

            // Fix for IR5481 Session issue when going from between different planners using the left hand menu
            if (sessionManager.FindAMode != FindAMode.Coach)
            {
                // We have come directly from another planner so clear results from session.
                helper.ClearJourneyResultCache();
            }
            #endregion

            // Initialise page state and journey parameter objects in session data

            bool resetDone;

			//No reset is required if coming from landing page, since the journey parameters 
			//have already been initialised.
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ])
			{
				resetDone = false;
			}
			else
			{
				resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.Coach);
			}

            pageState = (FindCoachPageState)TDSessionManager.Current.FindPageState;
            journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;  
			inputPageState = TDSessionManager.Current.InputPageState;

            findInputAdapter = new FindCoachInputAdapter(journeyParams,pageState, inputPageState);
            
            if (resetDone)            
            {
                // if page state and journey parameters were re-instantiated, initialise to correct state
                // for this find A mode.
                findInputAdapter.InitJourneyParameters();
            }

			// initialise the date control from session (required as displaying help forces redirect)
			findInputAdapter.InitDateControl(dateControl);
        }

        /// <summary>
        /// Sets the From and To locations into editable mode if user is Amending their journey
        /// </summary>
        private void SetLocationsAmendable()
        {
            // Set locations to be amendable
            if (
                ((!Page.IsPostBack) && (pageState.AmendMode))
                ||
                ((!pageState.AmbiguityMode) && (pageState.AmendMode)))
            {
                // Don't want the location to be amendable if it was a FindNearest
                if ((journeyParams.OriginLocation != null) && (journeyParams.OriginLocation.SearchType != SearchType.FindNearest))
                    locationsControl.FromLocationControl.AmendMode = pageState.AmendMode;

                if ((journeyParams.DestinationLocation != null) && (journeyParams.DestinationLocation.SearchType != SearchType.FindNearest))
                    locationsControl.ToLocationControl.AmendMode = pageState.AmendMode;
            }
        }

        /// <summary>
        /// Sets the From and To locations into Resolved mode if they are valid
        /// Only done in Submit request because in case we go to ambiguity page, need to ensure we show a resolved location
        /// </summary>
        private void SetLocationsResolved()
        {
            if ((journeyParams.OriginLocation != null) && (journeyParams.OriginLocation.Status == TDLocationStatus.Valid))
            {
                locationsControl.FromLocationControl.AmendMode = false;
            }

            if ((journeyParams.DestinationLocation != null) && (journeyParams.DestinationLocation.Status == TDLocationStatus.Valid))
            {
                locationsControl.ToLocationControl.AmendMode = false;
            }
        }

        #endregion Private Methods

        #region Event Handlers

        /// <summary>
        /// Event handler for page PreRender event fired by page
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
			// don't want to update the date when not postback...
			// otherwise double initial population
			if (Page.IsPostBack)
			{
				findInputAdapter.UpdateDateControl(dateControl);
			}

            findInputAdapter.UpdateErrorMessages(
                panelErrorMessage, labelErrorMessages, TDSessionManager.Current.ValidationError);

            // White Labeling - this section sets options for pageOptionsControltop.
            bool showPreferences;

            if (preferencesControl.AmbiguityMode)
            {
                showPreferences = (preferencesControl.ViaLocationControl.Visible);
            }
            else
            {
                showPreferences = preferencesControl.PreferencesVisible;
            }

            
            pageOptionsControltop.AllowBack = preferencesControl.AmbiguityMode;
            pageOptionsControltop.AllowShowAdvancedOptions = !preferencesControl.AmbiguityMode && !preferencesControl.PreferencesVisible;


            if (showPreferences)
            {
                pageOptionsControltop.AllowHideAdvancedOptions = !preferencesControl.AmbiguityMode;

            }

            if (preferencesControl.AmbiguityMode)
            {
                pageOptionsControltop.Visible = true;
            }

            
			panelBackTop.Visible = pageState.AmbiguityMode;
			panelSubHeading.Visible = !pageState.AmbiguityMode;

            pageOptionsControltop.AllowBack = false;

            if (preferencesControl.PreferencesVisible || pageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }
        }
        
        /// <summary>
        /// Event handler for Load event fired by page
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            labelFindPageTitle.Text = GetResource("FindCoachInput.labelFindCoachTitle");
            labelFromToTitle.Text = GetResource("FindCoachInput.labelFromToTitle");
            PageTitle = GetResource("FindCoachInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            imageFindACoach.ImageUrl = GetResource("HomeDefault.imageFindCoach.ImageUrl");
            imageFindACoach.AlternateText = " ";

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            if (Page.IsPostBack)
            {
				updateOtherControls();
            }
            else
            {
                initialRequestSetup();
            }

            // Make the locations editable
            SetLocationsAmendable();

            findInputAdapter.InitialiseControls(preferencesControl, locationsControl);

			// Adding client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

			if (TDSessionManager.Current.FindPageState.AmbiguityMode)
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindCoachInput.HelpAmbiguityUrl");
			}
			else
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindCoachInput.HelpPageUrl");
            }

            //Added for white labelling:
            ConfigureLeftMenu("FindCoachInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);


            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindCoachInput);
            expandableMenuControl.AddExpandedCategory("Related links");
            #region Landing Page Functionality
            //Check if we need to initiate an automatic search due to Landing Page Autoplan Mode			
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageAutoPlan ])
			{
				//if required then call the submit request method. 				
				SubmitRequest();
			}			
			#endregion

            commandBack.Text = GetResource("FindCoachInput.CommandBack.Text");
        }

		/// <summary>
		/// Page Unload event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//reset landing page session parameters
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ])
			{
				landingPageHelper.ResetLandingPageSessionParameters();
			}
		}

		/// <summary>
        /// Event handler called when "changes" type option changed. Updates journey parameters with new value.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_ChangesOptionChanged(object sender, EventArgs e)
        {
            journeyParams.PublicAlgorithmType = preferencesControl.Changes;
        }

        /// <summary>
        /// Event handler called when "changes" speed option changed. Updates journey parameters with new value.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_ChangeSpeedOptionChanged(object sender, EventArgs e)
        {
            journeyParams.InterchangeSpeed = preferencesControl.ChangesSpeed;
        }

        /// <summary>
        /// Event handler called when back button clicked in ambiguous mode. Decrements the level of
        /// hierarchical location searches (origin, destination and public via). If all locations are at highest 
        /// level then page is reverted to input mode and original input parameters reinstated.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Back(object sender, EventArgs e)
        {
			dateControl.CalendarClose();
			
            if (findInputAdapter.IsAtHighestLevel()) 
            {
				//Check to see if there is a previous page waiting on the stack
				if(inputPageState.JourneyInputReturnStack.Count != 0)
				{
					TransitionEvent lastPage = (TransitionEvent)inputPageState.JourneyInputReturnStack.Pop();
					//If the user is returing to the previous journey results, re-validate them
					if(lastPage == TransitionEvent.FindAInputRedirectToResults)
					{
						TDSessionManager.Current.JourneyResult.IsValid = true;
					}
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = lastPage;
					return;
				}

                pageState.AmbiguityMode = false;
                pageState.ReinstateJourneyParameters(journeyParams);
                preferencesControl.ChangesDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal;
                dateControl.LeaveDateControl.AmbiguityMode = false;
                dateControl.ReturnDateControl.AmbiguityMode = false;
                dateControl.LeaveDateControl.DateErrors = null;
                dateControl.ReturnDateControl.DateErrors = null;
                TDSessionManager.Current.ValidationError = null;

                findInputAdapter.InitLocationsControl(locationsControl);
                findInputAdapter.InitViaLocationsControl(preferencesControl);

                if (locationsControl.FromLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
                {
                    locationsControl.FromLocationControl.SetLocationUnspecified();
                }
                if (locationsControl.ToLocationControl.TheLocation.Status != TDLocationStatus.Valid) 
                {
                    locationsControl.ToLocationControl.SetLocationUnspecified();
                }
                if (preferencesControl.ViaLocationControl.TheLocation.Status != TDLocationStatus.Valid)
                {
                    preferencesControl.ViaLocationControl.SetLocationUnspecified();
                }
            } 
            else
            {
                // Decrement the drilldown level of any ambiguous locations
                // that are not yet at their highest level
                journeyParams.Origin.DecrementLevel();
                journeyParams.Destination.DecrementLevel();
                journeyParams.PublicVia.DecrementLevel();
            }


            // CCN 0427 Setting visibility of the page options control at top
            pageOptionsControltop.Visible = !preferencesControl.PreferencesVisible;

        }

        /// <summary>
        /// Event handler called when visibility of preferences changed. Updates page state with new value.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_PreferencesVisibleChanged(object sender, EventArgs e) 
        {
            pageState.TravelDetailsVisible = preferencesControl.PreferencesVisible;
            if (!preferencesControl.PreferencesVisible)
                pageOptionsControltop.Visible = true;
        }

        /// <summary>
        /// Event handler called when next button clicked. The journey plan runner component validates the 
        /// current journey parameters. If invalid then the page is put into ambiguity mode otherwise 
        /// journey planning commences. User preferences are saved before commencing journey planning.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Submit(object sender, EventArgs e)
		{
				SubmitRequest();
		}
		
		/// <summary>
		/// Method containing the preferencesControl_Submit method functionality so that it
		/// can be called by the Landing page when AutoPlanning is required. 
		/// </summary>
		private void SubmitRequest()
		{
            // Must be done here to ensure if user was Amending journey -> changed a location ->
            // and it is ambiguous, then make the valid location resolved
            SetLocationsResolved();

			dateControl.CalendarClose();
			
			if (preferencesControl.PreferencesOptionsControl.SavePreferences) 
			{
				findInputAdapter.SaveTravelDetails();
			}

			if (!pageState.AmbiguityMode) 
			{
				pageState.SaveJourneyParameters(journeyParams);
				findInputAdapter.AmbiguitySearch(locationsControl, preferencesControl);
			} 
			else 
			{
				locationsControl.FromLocationControl.Search();
				locationsControl.ToLocationControl.Search();
				preferencesControl.ViaLocationControl.Search();
			}

			findInputAdapter.InitialiseAsyncCallState();

			// Validate the JourneyParameters
			JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

			pageState.AmbiguityMode = !runner.ValidateAndRun(
				TDSessionManager.Current, 
				TDSessionManager.Current.JourneyParameters, 
				GetChannelLanguage(TDPage.SessionChannelName),
				true);

            if (pageState.AmbiguityMode ) 
            {
                if (!TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan]) 
                {
                    preferencesControl.ChangesDisplayMode = GenericDisplayMode.ReadOnly;
                    preferencesControl.ChangesSpeedDisplayMode = GenericDisplayMode.ReadOnly;
                    dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                    dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                    dateControl.LeaveDateControl.AmbiguityMode = true;
                    dateControl.ReturnDateControl.AmbiguityMode = true;
                } 
                else 
                {
                    pageState.AmbiguityMode = false;
                }
            }
            else 
            {

                // Extending journeys using a Find A is not currently possible so clear down the itinerary
                TDItineraryManager itineraryManager = TDItineraryManager.Current;
                if (itineraryManager.Length >= 1 ) 
                {
                    itineraryManager.ResetItinerary();
                }

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindAInputOk;
            }
        }

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
            journeyParams.DestinationType = locationsControl.ToLocationControl.LocationControlType;
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "via" location.
        /// The journey parameters are updated with the "via" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_NewLocation(object sender, EventArgs e) 
        {
            journeyParams.PublicVia = preferencesControl.ViaLocationControl.TheSearch;
            journeyParams.PublicViaLocation = preferencesControl.ViaLocationControl.TheLocation;
            journeyParams.PublicViaType = preferencesControl.ViaLocationControl.LocationControlType;
        }

        /// <summary>
        /// Event handler called when clear page button is clicked. Journey parameters are reset
        /// to initial values, page controls updated and the page set to input mode.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Clear(object sender, EventArgs e) 
        {
            findInputAdapter.InitJourneyParameters();
            journeyParams.PublicModes = new ModeType[] {ModeType.Coach};
            pageState.AmbiguityMode = false;
            findInputAdapter.InitialiseControls(preferencesControl, locationsControl);
            dateControl.LeaveDateControl.DateErrors = null;
            dateControl.LeaveDateControl.AmbiguityMode = false;
            dateControl.ReturnDateControl.DateErrors = null;
            dateControl.ReturnDateControl.AmbiguityMode = false;
            TDSessionManager.Current.ValidationError = null;
            TDPage.CloseAllSingleWindows(Page);
        }

        /// <summary>
        /// Event handler called when "find nearest station" button for the "to" location is clicked. 
        /// The user is redirected to the find station page.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void locationsControlToLocationControl_FindNearestClick(object sender, EventArgs e) 
        {
            TDSessionManager.Current.FindStationPageState.LocationType = FindStationPageState.CurrentLocationType.To;
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
        }

        /// <summary>
        /// Event handler called when "find nearest station" button for the "from" location is clicked. 
        /// The user is redirected to the find station page.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void locationsControlFromLocationControl_FindNearestClick(object sender, EventArgs e) 
        {
            TDSessionManager.Current.FindStationPageState.LocationType = FindStationPageState.CurrentLocationType.From;
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
        }

        /// <summary>
        /// Event handler called when date selected from calendar control. The journey parameters for the outward
        /// date are updated with the calendar date selection.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e)
        {
            journeyParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
            journeyParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
        }

        /// <summary>
        /// Event handler called when date selected from calendar control. The journey parameters for the return
        /// date are updated with the calendar date selection.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void dateControlReturnDateControl_DateChanged(object sender, EventArgs e) 
        {
            journeyParams.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
            journeyParams.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
        }

		


        /// <summary>
        /// Performs page initialisation including event wiring.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            preferencesControl.ChangesOptionChanged += new EventHandler(preferencesControl_ChangesOptionChanged);
            preferencesControl.ChangeSpeedOptionChanged += new EventHandler(preferencesControl_ChangeSpeedOptionChanged);
            preferencesControl.PageOptionsControl.Submit += new EventHandler(preferencesControl_Submit);
            preferencesControl.PageOptionsControl.Back += new EventHandler(preferencesControl_Back);
            preferencesControl.PreferencesVisibleChanged += new EventHandler(preferencesControl_PreferencesVisibleChanged);
            locationsControl.NewLocationFrom += new EventHandler(locationsControl_NewLocationFrom);
            locationsControl.NewLocationTo += new EventHandler(locationsControl_NewLocationTo);
            preferencesControl.ViaLocationControl.NewLocation += new EventHandler(preferencesControl_NewLocation);
            preferencesControl.PageOptionsControl.Clear += new EventHandler(preferencesControl_Clear);
            // added for the top PageOptionsControl
            pageOptionsControltop.Submit += new EventHandler(preferencesControl_Submit);
            pageOptionsControltop.Back += new EventHandler(preferencesControl_Back);
            pageOptionsControltop.Clear += new EventHandler(preferencesControl_Clear);
            pageOptionsControltop.ShowAdvancedOptions += new EventHandler(pageOptionsControltop_ShowAdvancedOptions);
            pageOptionsControltop.HideAdvancedOptions += new EventHandler(pageOptionsControltop_HideAdvancedOptions);
            locationsControl.ToLocationControl.FindNearestClick += 
                new EventHandler(locationsControlToLocationControl_FindNearestClick);
            locationsControl.FromLocationControl.FindNearestClick += 
                new EventHandler(locationsControlFromLocationControl_FindNearestClick);
            dateControl.LeaveDateControl.DateChanged +=
                new EventHandler(dateControlLeaveDateControl_DateChanged);
            dateControl.ReturnDateControl.DateChanged +=
                new EventHandler(dateControlReturnDateControl_DateChanged);

			// Event Handler for default action button
			headerControl.DefaultActionEvent +=  new EventHandler(preferencesControl_Submit);

            commandBack.Click += new EventHandler(preferencesControl_Back);
        }

        /// <summary>
        /// White Labeling - Added event for pageOptionsControltop which raises when user hides AdvanceOptions.
        /// this will hide advance options. 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageOptionsControltop_HideAdvancedOptions(object sender, EventArgs e)
        {
            preferencesControl.PreferencesVisible = false;
        }

        /// <summary>
        /// White Labeling - Added event for pageOptionsControltop which raises when user clicks AdvanceOptions.
        /// this will show advance options and hide pageOptionsControltop control. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageOptionsControltop_ShowAdvancedOptions(object sender, EventArgs e)
        {
            // Showing Advance options
            preferencesControl.PreferencesVisible = true;
            // making page options control invisible
            pageOptionsControltop.Visible = false;
        }
        #endregion Event Handlers

		#region properties

		/// <summary>
		/// IR1619 - IsFindNearest returns true if the 'Find Nearest' functionality has been used to choose the 
		/// 'From' or 'To' location
		/// </summary>
		private bool IsFindNearest
		{
			get 
			{				
				return ((TDSessionManager.Current.JourneyParameters.OriginLocation.SearchType == SearchType.FindNearest) ||
					(TDSessionManager.Current.JourneyParameters.DestinationLocation.SearchType == SearchType.FindNearest));
			}
		}

		#endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }
		
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {    

		}
		#endregion


	}
}
