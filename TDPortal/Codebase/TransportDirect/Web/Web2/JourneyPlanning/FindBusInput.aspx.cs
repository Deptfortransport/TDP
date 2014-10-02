// *********************************************** 
// NAME                 : FindBusInput
// AUTHOR               : Esther Severn
// DATE CREATED         : 20/03/2006
// DESCRIPTION  : Input page for Find Bus journey planning
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindBusInput.aspx.cs-arc  $ 
//
//   Rev 1.21   Jul 28 2011 16:19:42   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.20   Oct 28 2010 14:42:04   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.19   May 13 2010 13:05:14   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.18   Mar 26 2010 11:08:56   MTurner
//Added code to clear journey results from session if you are coming directly from another planner.
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.17   Jan 29 2010 14:45:22   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.16   Jan 19 2010 13:20:56   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.15   Dec 14 2009 11:06:10   apatel
//stop the map showing when new location button click after amend
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.14   Dec 09 2009 11:33:50   mmodi
//When Clear button is clicked, reset the map
//
//   Rev 1.13   Dec 03 2009 16:00:54   apatel
//input page mapping enhancement related changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.12   Dec 02 2009 11:51:16   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.11   Nov 30 2009 10:19:40   apatel
//mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.10   Nov 30 2009 09:58:06   apatel
//input page find on map workflow changed
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 18 2009 11:42:12   apatel
//Added oneusekey for findonmap button click to move on to findmapinput page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 18 2009 11:20:38   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Nov 10 2009 11:30:00   apatel
//Find Input pages mapping enhancement changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Jan 30 2009 10:44:08   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.5   May 06 2008 16:25:36   apatel
//move preference control out of transportpanel to make it work in ambiguity mode
//Resolution for 4936: Via on find a bus does not work properly
//
//   Rev 1.4   May 01 2008 17:24:18   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.3   Apr 08 2008 16:11:32   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
// rev devfactory 8 apr sbarker
// Advanced text hidden when advanced pressed, or in ambiguity mode
//
//   Rev 1.2   Mar 31 2008 13:24:20   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Mar 28 2008 16:45 dgath
//  Commented out both references to commandSubmit, and removed this control from page, as this 
//  page already had Next buttons on both the main options and Advanced / Public Transport 
//  Journey Details options
//
//  Rev Devfactory Mar 18 2008 11:24:00 apatel
//  Page_Load and Page_Init event modified to show advance options and to show pageoptionscontrol accordingly. 
//
//   Rev 1.0   Nov 08 2007 13:29:20   mturner
//Initial revision.
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//   Rev 1.16   Sep 03 2007 15:24:30   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.15   Jun 07 2007 15:16:10   mmodi
//Added a new Next submit button
//
//   Rev 1.14   Apr 27 2006 11:03:10   mtillett
//Ensure calender button shown on ambiguity page
//Resolution for 3927: DN062 Amend Tool: Calendar buttons ignored on Ambiguity page
//
//   Rev 1.13   Apr 12 2006 17:45:38   mdambrine
//extra code in the back button event handler to redirect to the page it was coming from
//Resolution for 3899: DN093 Find a Bus: Back button links to wrong page when using Amend Tool with Find A Bus
//
//   Rev 1.12   Apr 11 2006 12:11:40   esevern
//Reset the AdvancedOptionsVisible bool in InputPageState to false when user clicks 'Clear' - the advanced travel options should be hidden when the input fields are cleared.
//Resolution for 3871: DN093 Find a Bus: Clear page does not hide the Advanced Options
//
//   Rev 1.11   Apr 05 2006 11:10:58   esevern
//code review changes - added default action event wiring and updating of users previous selections for use when amending a planned journey
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.10   Apr 03 2006 14:58:22   mdambrine
//code review fixes
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.9   Apr 03 2006 10:42:28   esevern
//corrected error on via location when amending journey
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.8   Mar 30 2006 17:30:30   mdambrine
//Fxcop fixes
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.7   Mar 30 2006 14:25:30   esevern
//added commenting and updated public via location map click event.
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.6   Mar 29 2006 17:09:36   esevern
//added loading of saved user preferences when logged in
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.5   Mar 29 2006 15:36:36   mdambrine
//added clear page event handler
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.4   Mar 29 2006 12:45:24   mdambrine
//fixed the problem with the date control not showing the time now
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.3   Mar 28 2006 15:07:20   mdambrine
//updated html and handling of the preferences
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.2   Mar 24 2006 16:25:00   mdambrine
//temporary checkin
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.1   Mar 22 2006 11:59:24   esevern
//interim checkin for handover
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.0   Mar 21 2006 16:53:04   esevern
//Initial revision.
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)

#region References
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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SuggestionLinkService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Web.Support;
using TransportDirect.JourneyPlanning.CJPInterface;

using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using System.Collections.Generic;
#endregion

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Input page for find A bus. Allows user to plan a bus only journey.
	/// The input page is also responsible for resolution of any input data
	/// ambiguity resolution.
	/// </summary>
	public partial class FindBusInput : TDPage
	{		

		#region Declarations


		//protected FindToFromLocationsControl locationsControl;
		//protected FindLeaveReturnDatesControl dateControl;			

		//protected HeaderControl headerControl;
			
		//protected TransportDirect.UserPortal.Web.Controls.TransportTypesControl transportTypesControl;
		//protected TransportDirect.UserPortal.Web.Controls.PtPreferencesControl preferencesControl;
		//protected TransportDirect.UserPortal.Web.Controls.FindPageOptionsControl findPageOptionsControl;		

		private ControlPopulator populator;

		/// <summary>
		/// Holds user's current page state for this page
		/// </summary>
		private FindBusPageState pageState;

		/// <summary>
		/// Hold user's current journey parameters for train only journey
		/// </summary>
		private TDJourneyParametersMulti journeyParams;

		/// <summary>
		/// Holds user's map current page state.
		/// </summary>
		private InputPageState inputPageState;

		/// <summary>
		/// Helper class responsible for common methods to Find A pages
		/// </summary>
		private FindBusInputAdapter findInputAdapter;

		private const string RES_FROMTOTITLE = "FindBusInput.labelFindBusNote";
		protected System.Web.UI.WebControls.Panel transportTypesPanel;
		private const string RES_PAGETITLE = "FindBusInput.labelFindBusTitle";	
		
		#endregion

		#region Constructor, Page Load, Init, PreRender
		/// <summary>
		/// FindBusInput page contructor
		/// </summary>
		public FindBusInput()
		{
			this.pageId = PageId.FindBusInput;				
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];		
		}
		
		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{			

			LoadResources();

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            if (Page.IsPostBack)
			{
				// The date controls don't raise events to say they have updated, so the params 
				// for these are always update here
				updateOtherControls();

				WriteToSession();
				
			}
            else if (TDSessionManager.Current.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) != null)
            {
                pageState = (FindBusPageState)TDSessionManager.Current.FindPageState;
                journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
                inputPageState = TDSessionManager.Current.InputPageState;
                findInputAdapter = new FindBusInputAdapter(journeyParams, pageState, inputPageState);

                // initialise the date control from session (required as displaying help forces redirect)
                findInputAdapter.InitDateControl(dateControl);
                          

                // Populate Transport Types checkBoxes
                populator.LoadListControl(DataServiceType.FindABusCheck, transportTypesControl.ModesPublicTransport);
               
            }
            else
            {
                initialRequestSetup();
            }

            // CCN 0427 showing preferencescontrol's bottom page options controls visible.
            preferencesControl.ViaLocationControl.PageOptionsControl.AllowHideAdvancedOptions = true;
            preferencesControl.WalkingSpeedOptionsControl.PageOptionsControl.AllowClear = false;
            preferencesControl.WalkingSpeedOptionsControl.PageOptionsControl.AllowNext = false;

            findPageOptionsControl.AllowHideAdvancedOptions = false;
            
            
            //check if we are in amend mode and if true, set the location controls to editable			
            preferencesControl.ViaLocationControl.AmendMode = pageState.AmendMode;
            locationsControl.FromLocationControl.AmendMode = pageState.AmendMode && (journeyParams.OriginLocation.Status == TDLocationStatus.Valid);
            locationsControl.ToLocationControl.AmendMode = pageState.AmendMode && (journeyParams.DestinationLocation.Status == TDLocationStatus.Valid);
            

			if (pageState.AmendMode)
			{
				journeyParams.DestinationType.Type = ControlType.Default;
				journeyParams.OriginType.Type = ControlType.Default;
				journeyParams.PublicViaType.Type = ControlType.Default;			
			}											
				
			findInputAdapter.InitialiseControls(preferencesControl, locationsControl);

			// Adding client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);
			
			if (TDSessionManager.Current.FindPageState.AmbiguityMode)
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindBusInput.HelpAmbiguityUrl");
			}
			else
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindBusInput.HelpPageUrl");
			}

            //Added for white labelling:
            ConfigureLeftMenu("FindBusInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindBusInput);
            expandableMenuControl.AddExpandedCategory("Related links");

            commandBack.Text = GetResource("FindBusInput.CommandBack.Text");
		}

		/// <summary>
		/// Performs page initialisation including event wiring.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Init(object sender, EventArgs e)
		{		
				
			//Locations to and from
			locationsControl.NewLocationFrom += new EventHandler(locationsControl_NewLocationFrom);
			locationsControl.NewLocationTo += new EventHandler(locationsControl_NewLocationTo);								
			locationsControl.FromLocationControl.TriLocationControl.MapClick += new EventHandler(locationsControl_MapFromClick);
			locationsControl.ToLocationControl.TriLocationControl.MapClick += new EventHandler(locationsControl_MapToClick);

			//DateControls single and return
			dateControl.LeaveDateControl.DateChanged += new EventHandler(dateControlLeaveDateControl_DateChanged);
			dateControl.ReturnDateControl.DateChanged += new EventHandler(dateControlReturnDateControl_DateChanged);									

			//Preferences control
			preferencesControl.ViaLocationControl.MapClick += new EventHandler(viaLocationControl_MapClick);	
			preferencesControl.ViaLocationControl.NewLocation += new EventHandler(viaLocationControl_NewLocation);
			preferencesControl.JourneyChangesOptionsControl.ChangesOptionChanged += new EventHandler(preferencesControl_PtJourneyChanges);
			preferencesControl.JourneyChangesOptionsControl.ChangeSpeedOptionChanged += new EventHandler(preferencesControl_PtChangesSpeed);
			preferencesControl.WalkingSpeedOptionsControl.WalkingDurationOptionChanged += new EventHandler(preferencesControl_PtWalkDuration);
			preferencesControl.WalkingSpeedOptionsControl.WalkingSpeedOptionChanged += new EventHandler(preferencesControl_PtWalkSpeed);
			preferencesControl.PreferencesVisibleChanged += new EventHandler(preferencesControl_PreferencesVisibleChanged);
			preferencesControl.PreferencesOptionsControl.Submit += new EventHandler(findPageOptionsControl_NextEventHandler);

			//back, next, hide, clear page, preferences buttons
			findPageOptionsControl.ShowAdvancedOptions += new EventHandler(findPageOptionsControl_DisplayEventHandler);
			//findPageOptionsControl.HideAdvancedOptions += new EventHandler(findPageOptionsControl_DisplayEventHandler);
			findPageOptionsControl.Submit += new EventHandler(findPageOptionsControl_NextEventHandler);
			findPageOptionsControl.Clear += new EventHandler(findPageOptionsControl_NextClearEventHandler);
			findPageOptionsControl.Back += new EventHandler(findPageOptionsControl_BackEventHandler);	
			
		    // CCN 0427 Added event handler for the ptpreference control
            preferencesControl.ViaLocationControl.PageOptionsControl.HideAdvancedOptions += new EventHandler(findPageOptionsControl_DisplayEventHandler);
            preferencesControl.ViaLocationControl.PageOptionsControl.Submit += new EventHandler(findPageOptionsControl_NextEventHandler);
			// Submit button
			//commandSubmit.Click += new EventHandler(findPageOptionsControl_NextEventHandler);

			// Event Handler for default action button			
			headerControl.DefaultActionEvent += new EventHandler(findPageOptionsControl_NextEventHandler);

            commandBack.Click += new EventHandler(findPageOptionsControl_BackEventHandler);
		}

        
		/// <summary>
		/// Handles display of the input controls and error messages
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
			else 
			{
				findInputAdapter.UpdateLocationsControls(locationsControl, preferencesControl);
				findInputAdapter.UpdatePreferencesControls(preferencesControl);
				findInputAdapter.UpdateTransportTypes(transportTypesControl);
			}
		
			//Determine which populate methods are called, and therefore controls displayed
			//If the page is not in Ambiguity Mode
			if (!pageState.AmbiguityMode)
			{
				ShowAndPopulateInputControl();
				
				// Load travel preferences
				LoadUserPreferences();
			}
			else //The page is in Ambiguity Mode
			{
				ShowAndPopulateAmbiguityControl();
			}

			findInputAdapter.UpdateErrorMessages(
				panelErrorMessage, labelErrorMessages, TDSessionManager.Current.ValidationError);
			// hide from to title in ambiguity mode!
			labelFromToTitle.Visible = !pageState.AmbiguityMode;

            findPageOptionsControl.AllowBack = false;
            commandBack.Visible = preferencesControl.AmbiguityMode;

            //Hide advanced text:
            if (preferencesControl.PreferencesVisible || pageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }

            SetupMap();
		}

		#endregion

		#region Private Methods
		

		/// <summary>
		/// Updates session data with control values for those controls that do not raise events
		/// to signal that user has changed values (i.e. the date controls)
		/// </summary>
		private void updateOtherControls() 
		{
			pageState = (FindBusPageState)TDSessionManager.Current.FindPageState;
			journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;            
			inputPageState = TDSessionManager.Current.InputPageState;
			findInputAdapter = new FindBusInputAdapter(journeyParams,pageState, inputPageState);			
			
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

            #region Clear cache of journey data
            // if an extension is in progress, cancel it
			sessionManager.ItineraryManager.CancelExtension();

            ClearCacheHelper helper = new ClearCacheHelper();

            // Force clear of any printable information if added by the journey result page
            helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

            // Fix for IR5481 Session issue when going from between different planners using the left hand menu
            if (sessionManager.FindAMode != FindAMode.Bus)
            {
                // We have come directly from another planner so clear results from session.
                helper.ClearJourneyResultCache();
            }
            #endregion

            // Next, Initialise JourneyParameters and PageStates if needed. 
			// Will return true, if reset has been performed.
			bool resetDone;
						
			resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.Bus);			
			pageState = (FindBusPageState)TDSessionManager.Current.FindPageState;				
			journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;            							
			inputPageState = TDSessionManager.Current.InputPageState;
			findInputAdapter = new FindBusInputAdapter(journeyParams,pageState, inputPageState);			
            
			if (resetDone)            
			{
				findInputAdapter.InitJourneyParameters();
			}


			// initialise the date control from session (required as displaying help forces redirect)
			findInputAdapter.InitDateControl(dateControl);				

			// Populate Transport Types checkBoxes
			populator.LoadListControl(DataServiceType.FindABusCheck, transportTypesControl.ModesPublicTransport);
		}
		#endregion

		#region Event Handlers			
	
		#region locations controls

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
			journeyParams.Origin.SearchType = SearchType.MainStationAirport;
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
			journeyParams.Destination.SearchType = SearchType.MainStationAirport;
			journeyParams.DestinationType = locationsControl.ToLocationControl.LocationControlType;
		}

		/// <summary>
		/// Handler for the 'From' location 'Find on Map' button click event. Calls MapSearch 
		/// to setup location parameters before redirecting to JourneyLocationLocationMap page.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void locationsControl_MapFromClick(object sender, EventArgs e)
		{
            bool shiftForm = false;

			inputPageState.MapType = CurrentLocationType.From;
			inputPageState.MapMode = CurrentMapMode.FromFindAInput;
			
			if (journeyParams.OriginLocation.Status == TDLocationStatus.Unspecified )
			{
				findInputAdapter.MapSearch(journeyParams.Origin.InputText, journeyParams.Origin.SearchType, journeyParams.Origin.FuzzySearch);
                if (inputPageState.MapLocationSearch.InputText.Length == 0)
                {
                    inputPageState.MapLocationControlType.Type = ControlType.Default;
                }
                else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    journeyParams.Origin = inputPageState.MapLocationSearch;
                    journeyParams.OriginLocation = inputPageState.MapLocation;
                    journeyParams.OriginLocation.Status = TDLocationStatus.Valid;
                    locationsControl.FromLocationControl.TheLocation = inputPageState.MapLocation;
                    locationsControl.FromLocationControl.RefreshTristateControl(false);
                    locationsControl.FromLocationControl.AmendMode = false;
                }
                else
                {
                    inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
			} 
			else 
			{
				inputPageState.MapLocationSearch = journeyParams.Origin;
				inputPageState.MapLocation = journeyParams.OriginLocation;
			}

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
		/// Handler for the 'From' location 'Find on Map' button click event. Calls MapSearch 
		/// to setup location parameters before redirecting to JourneyLocationLocationMap page.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void locationsControl_MapToClick(object sender, EventArgs e)
		{
            bool shiftForm = false;
			inputPageState.MapType = CurrentLocationType.To;
			inputPageState.MapMode = CurrentMapMode.FromFindAInput;
			
			if (journeyParams.DestinationLocation.Status == TDLocationStatus.Unspecified )
			{
				findInputAdapter.MapSearch(journeyParams.Destination.InputText, 
					journeyParams.Destination.SearchType, journeyParams.Destination.FuzzySearch);

                if (inputPageState.MapLocationSearch.InputText.Length == 0)
                {
                    inputPageState.MapLocationControlType.Type = ControlType.Default;
                }
                else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    journeyParams.Destination = inputPageState.MapLocationSearch;
                    journeyParams.DestinationLocation = inputPageState.MapLocation;
                    locationsControl.ToLocationControl.TheLocation = inputPageState.MapLocation;
                    locationsControl.ToLocationControl.AmendMode = false;
                    
                }
                else
                {
                    inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
			} 
			else 
			{
				inputPageState.MapLocationSearch = journeyParams.Destination;
				inputPageState.MapLocation = journeyParams.DestinationLocation;
			}

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

        #region Map Setup 
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
                if (!pageState.AmbiguityMode)
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

        #region Date controls
        /// <summary>
		/// Handler for the return date changed event. Updates outward day and month year
		/// values in journey parameters
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">EventArgs</param>
		private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e) 
		{
			journeyParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
			journeyParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
		}

		/// <summary>
		/// Handler for the return date changed event. Updates return day and month year
		/// values in journey parameters
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">EventArgs</param>
		private void dateControlReturnDateControl_DateChanged(object sender, EventArgs e) 
		{
			journeyParams.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
			journeyParams.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
		}
		#endregion		


		#region Preferences control
		
		/// <summary>
		/// Handler for the Public Transport Via location 'Find on Map' button click event. 
		/// Calls MapSearch to setup location parameters before redirecting to 
		/// JourneyLocationLocationMap page.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">EventArgs</param>
		private void viaLocationControl_MapClick(object sender, EventArgs e)
		{
            bool shiftForm = false;
			inputPageState.MapType = CurrentLocationType.PublicVia;
			inputPageState.MapMode = CurrentMapMode.FromFindAInput;
			
			if (journeyParams.PublicViaLocation.Status == TDLocationStatus.Unspecified )
			{
				findInputAdapter.MapSearch(journeyParams.PublicVia.InputText, 
					journeyParams.PublicVia.SearchType, journeyParams.PublicVia.FuzzySearch);

                if (inputPageState.MapLocationSearch.InputText.Length == 0)
                {
                    inputPageState.MapLocationControlType.Type = ControlType.Default;
                }
                else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    journeyParams.PublicVia = inputPageState.MapLocationSearch;
                    journeyParams.PublicViaLocation = inputPageState.MapLocation;
                    preferencesControl.ViaLocationControl.Search();
                }
                else
                {
                    inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
			} 
			else 
			{
				inputPageState.MapLocationSearch = journeyParams.PublicVia;
				inputPageState.MapLocation = journeyParams.PublicViaLocation;
			}

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
		/// Event handler called when new location button is clicked for the "via" location.
		/// The journey parameters are updated with the "via" location control's new values.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void viaLocationControl_NewLocation(object sender, EventArgs e) 
		{
			journeyParams.PublicVia = preferencesControl.ViaLocationControl.TheSearch;
			journeyParams.PublicViaLocation =  preferencesControl.ViaLocationControl.TheLocation;
			journeyParams.PublicVia.SearchType = SearchType.MainStationAirport;
			journeyParams.PublicViaType =  preferencesControl.ViaLocationControl.LocationControlType;
		}

		/// <summary>
		/// Handler for the public transport interchanges change event.  Updates journey parameters
		/// with the newly selected value.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_PtJourneyChanges(object sender, EventArgs e)
		{
			journeyParams.PublicAlgorithmType = preferencesControl.JourneyChangesOptionsControl.Changes;
		}

		/// <summary>
		/// Handler for the public transport interchange speed change event.  Updates journey 
		/// parameters with the newly selected value.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_PtChangesSpeed(object sender, EventArgs e)
		{
			journeyParams.InterchangeSpeed = preferencesControl.JourneyChangesOptionsControl.ChangesSpeed;
		}

		/// <summary>
		/// Handler for the public transport walking duration change event.  Updates journey parameters
		/// with the newly selected value.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_PtWalkDuration(object sender, EventArgs e)
		{
			journeyParams.MaxWalkingTime = preferencesControl.WalkingSpeedOptionsControl.WalkingDuration ;
		}
		
		/// <summary>
		/// Handler for the public transport walking speed change event.  Updates journey parameters
		/// with the newly selected value. 
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_PtWalkSpeed(object sender, EventArgs e)
		{
			journeyParams.WalkingSpeed = preferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;
		}
		
		/// <summary>
		/// Event handler called when visibility of preferences changed. Updates page state with 
		/// new value.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_PreferencesVisibleChanged(object sender, EventArgs e) 
		{
			pageState.TravelDetailsVisible = preferencesControl.PreferencesVisible;
		}

		#endregion

		#region Find Page options control
		/// <summary>
		/// Handles the ShowPreferences event of the FindPageOptionsControl. Updates the input page 
		/// state AdvancedOptionsVisible value.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void findPageOptionsControl_DisplayEventHandler(object sender, EventArgs e)
		{
			inputPageState.AdvancedOptionsVisible = !inputPageState.AdvancedOptionsVisible;
		}

		/// <summary>
		/// Handles the Next button click. Saves the user travel details if save preferences is 
		/// specified. Performs location searches and calls ValidateAndRun before forwarding to 
		/// next appropriate page.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void findPageOptionsControl_NextEventHandler(object sender, EventArgs e)
		{	
			dateControl.CalendarClose();
		
			if (preferencesControl.SavePreferences)
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

			// Set up the JourneyPlanControlData
			findInputAdapter.InitialiseAsyncCallState();

			// Validate the JourneyParameters
			JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

			pageState.AmbiguityMode = !runner.ValidateAndRun(
				TDSessionManager.Current, 
				TDSessionManager.Current.JourneyParameters, 
				GetChannelLanguage(TDPage.SessionChannelName),
				true);

			if (pageState.AmbiguityMode) 
			{			
				dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
				dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
				dateControl.LeaveDateControl.AmbiguityMode = true;
				dateControl.ReturnDateControl.AmbiguityMode = true;
				preferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.ReadOnly;
				preferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.ReadOnly;
				preferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.ReadOnly;
				preferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.ReadOnly;										
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

			// refresh the controls otherwise, the amendMode change is not taken into account until 
			// next postback
			if (pageState.AmendMode)
			{
				//Disable AmendMode when leaving the page
				pageState.AmendMode = false;

				// Refresh location controls to ensure the location is up to date 
				preferencesControl.ViaLocationControl.AmendMode = pageState.AmendMode;
				locationsControl.FromLocationControl.AmendMode = pageState.AmendMode;	
				locationsControl.ToLocationControl.AmendMode = pageState.AmendMode;
			}
		}

		
		/// <summary>
		/// Handles the Clear and New search event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void findPageOptionsControl_NextClearEventHandler(object sender, EventArgs e)
		{			
			findInputAdapter.InitJourneyParameters();
			pageState.AmbiguityMode = false;

			//repopulate the data
			findInputAdapter.InitialiseControls(preferencesControl, locationsControl);			
			findInputAdapter.InitViaLocationControl(preferencesControl);
			populator.LoadListControl(DataServiceType.FindABusCheck, transportTypesControl.ModesPublicTransport);

			// Advanced travel options should be hidden on 'Clear'
			inputPageState.AdvancedOptionsVisible = false;

			dateControl.LeaveDateControl.DateErrors = null;
			dateControl.LeaveDateControl.AmbiguityMode = false;
			dateControl.ReturnDateControl.DateErrors = null;
			dateControl.ReturnDateControl.AmbiguityMode = false;
			TDSessionManager.Current.ValidationError = null;
			TDPage.CloseAllSingleWindows(Page);			
			
			preferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.Normal;
			preferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal;
			preferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.Normal;
			preferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.Normal;

            // Reset the Map locations to show on the map (use Origin as it should have been reset by now)
            inputPageState.MapLocation = journeyParams.OriginLocation;
            inputPageState.MapLocationSearch = journeyParams.Origin;
		}

		/// <summary>
		/// Handles the ShowPreferences event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void findPageOptionsControl_BackEventHandler(object sender, EventArgs e)
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
				preferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.Normal;
				preferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.Normal;	
				dateControl.LeaveDateControl.AmbiguityMode = false;
				dateControl.ReturnDateControl.AmbiguityMode = false;
				dateControl.LeaveDateControl.DateErrors = null;
				dateControl.ReturnDateControl.DateErrors = null;
				TDSessionManager.Current.ValidationError = null;

				findInputAdapter.InitLocationsControl(locationsControl);
				findInputAdapter.InitPreferencesControl(preferencesControl);

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
		}
		
		#endregion		
		
		#endregion

		#region Load/Save
		/// <summary>
		/// This method will save data to the journey parameters session object. This method is needed
		/// as this functionality is not encapsulated within the PTJourneyPreferences control.
		/// </summary>
		private void WriteToSession()
		{															
			journeyParams.PublicModes =			transportTypesControl.PublicModes;
			
			journeyParams.WalkingSpeed =		preferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;				
			journeyParams.MaxWalkingTime =		preferencesControl.WalkingSpeedOptionsControl.WalkingDuration;
			journeyParams.InterchangeSpeed =	preferencesControl.JourneyChangesOptionsControl.ChangesSpeed;
			journeyParams.PublicAlgorithmType = preferencesControl.JourneyChangesOptionsControl.Changes;		

		}	
		#endregion

		#region DisplayState
		/// <summary>
		/// Populate controls on page in Ambiguity mode
		/// </summary>
		private void ShowAndPopulateAmbiguityControl()
		{			
			findPageOptionsControl.AllowClear = false;
		
			//Sets the visibility of the transportTypesControl and the preferencesControl
			panelTransportTypes.Visible = false;			

			//Displays the back button
			findPageOptionsControl.AllowBack = true;

            preferencesControl.ViaLocationControl.PageOptionsControl.AllowClear = false;
            preferencesControl.ViaLocationControl.PageOptionsControl.AllowNext = false;
            preferencesControl.ViaLocationControl.PageOptionsControl.AllowHideAdvancedOptions = false;
            preferencesControl.ViaLocationControl.PageOptionsControl.AllowShowAdvancedOptions = false;
            preferencesControl.WalkingSpeedOptionsControl.PageOptionsControl.Visible = false;
		}

		/// <summary>
		/// Populate the controls when page is in Input mode (i.e. AmbiguityMode is false)
		/// </summary>
		private void ShowAndPopulateInputControl()
		{								 					
			//Sets the visability of the controls on the preferencesControl as not all are used by VP
			preferencesControl.JourneyChangesOptionsControl.Visible = true;
			preferencesControl.WalkingSpeedOptionsControl.Visible = true;
			preferencesControl.ViaLocationControl.Visible = true;
			preferencesControl.PreferencesVisible = true;

			//Sets the visability of the transportTypesControl and the preferencesControl
			findPageOptionsControl.AllowShowAdvancedOptions = !inputPageState.AdvancedOptionsVisible;
			labelAdvanced.Visible = inputPageState.AdvancedOptionsVisible;
			panelTransportTypes.Visible = inputPageState.AdvancedOptionsVisible;
			preferencesControl.PreferencesVisible = inputPageState.AdvancedOptionsVisible;
            findPageOptionsControl.AllowHideAdvancedOptions = false;			

		}

		/// <summary>
		/// Sets static label text for the page, including page title.
		/// </summary>
		private void LoadResources()
		{
			PageTitle = GetResource("FindBusInput.AppendPageTitle")+GetResource("JourneyPlanner.DefaultPageTitle");	
			labelFindPageTitle.Text = GetResource(RES_PAGETITLE);
			labelFromToTitle.Text = GetResource(RES_FROMTOTITLE);
			labelAdvanced.Text = GetResource (TDResourceManager.VISIT_PLANNER_RM, "VisitPlannerInput.AdvancedOptions");
			//commandSubmit.Text = GetResource("JourneyPlannerInput.commandSubmit.AlternateText");
            imageFindBus.ImageUrl = GetResource("HomePlanAJourney.imageFindBus.ImageUrl");
            imageFindBus.AlternateText = " ";
		}

		/// <summary>
		/// If the user is currently logged in, calls UserPreferencesHelper to load any 
		/// previously saved travel preferences (interchange and walking options).
		/// Sets the obtained values in the walking and interchange controls from the 
		/// updated journey parameters.
		/// </summary>
		private void LoadUserPreferences()
		{
			if(TDSessionManager.Current.Authenticated)
			{
				bool ptPrefs = UserPreferencesHelper.LoadPublicTransportPreferences( journeyParams );

				if( ptPrefs )
				{
					findInputAdapter.UpdatePreferencesControls(preferencesControl);
				}
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
