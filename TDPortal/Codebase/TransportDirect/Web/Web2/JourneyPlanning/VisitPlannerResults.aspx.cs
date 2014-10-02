// *********************************************** 
// NAME				 : VisitPlannerResults.aspx.cs
// AUTHOR			 : Tolu Olomolaiye
// DATE CREATED		 : 29 Sep 2005
// DESCRIPTION		 : Journey Results Page for VisitPlanner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/VisitPlannerResults.aspx.cs-arc  $
//
//   Rev 1.16   Aug 02 2011 13:25:00   apatel
//Added additional intellitracker tags to track day trip planner
//Resolution for 5713: Intelitracker tags not added for Cycle detail page map/detail toggle button
//
//   Rev 1.15   Apr 30 2010 09:30:50   apatel
//Updated to remove "/" from intellitracker parameter values
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.14   Mar 05 2010 10:00:40   apatel
//Updated for customised intellitracker tags
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.13   Feb 16 2010 11:15:30   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.12   Feb 12 2010 11:14:08   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.11   Dec 02 2009 15:59:20   mmodi
//Added code to show map view when returning from car park/stop information page
//
//   Rev 1.10   Nov 29 2009 12:47:24   mmodi
//Updated map initialise to hide the show journey buttons
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 23 2009 10:36:30   mmodi
//Updated for mapping
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 11 2009 18:41:38   mmodi
//Updated to use new MapJourneyControl
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Feb 27 2009 09:36:46   rbroddle
//Changed to set "isTripPlannerJourney" on footnotes control
//Resolution for 5258: Visit planner results footnotes include the note about planning a return using "amend date and time" tab
//
//   Rev 1.6   Dec 17 2008 11:27:58   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.5   Oct 13 2008 16:44:34   build
//Automatically merged from branch for stream5014
//
//   Rev 1.4.1.0   Sep 22 2008 11:26:36   mmodi
//Hide controls when there is an error with the results
//Resolution for 5116: Inappropriate Details Text and Header bar when no results
//
//   Rev 1.4   Apr 07 2008 11:36:40   scraddock
//Steve B: Fixed a problem with the journey parameters variable being null when switching between results and planners
//
// Rev DevFactory Apr 7 sbarker
// Fixed the journey results being nulls when flicking from another planner
//
//   Rev 1.3   Apr 03 2008 15:28:54   apatel
//set the journe detail to show as diagram by default
//
//   Rev 1.2   Mar 31 2008 13:25:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:32:02   mturner
//Initial revision.
//
//   Rev 1.28   Mar 06 2007 12:30:00   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.27.1.0   Feb 26 2007 11:59:46   mmodi
//Added Compare CO2 emissions button - currently visibility set to false
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.27   Apr 26 2006 13:36:18   halkatib
//Fix for IR3991: Added extra error handling to detect whether there is an error in the VisitPlanner journey result and set the IsError flag on the journeyMap control accordingly. 
//
//   Rev 1.26   Feb 24 2006 12:52:40   RWilby
//Fix for merge stream3129. Added using reference to TransportDirect.Common.ResourceManager namespace.
//
//   Rev 1.25   Feb 21 2006 18:34:20   aviitanen
//TD114 PDF Print bundles - high resolution map changes
//
//   Rev 1.24   Feb 15 2006 15:25:56   aviitanen
//Added a page title
//
//   Rev 1.23   Feb 10 2006 11:21:30   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.22   Jan 27 2006 18:22:10   jbroome
//Ensure map is started when switching from details to map view 
//Resolution for 3522: Visit Planner: Server error when selecting map button in results table
//
//   Rev 1.21   Jan 18 2006 18:58:24   jbroome
//Updated map button clicked event to ensure that map symbols are added to map
//
//   Rev 1.20   Jan 04 2006 10:06:24   tolomolaiye
//Updates folllowing Visit Planner code review
//
//   Rev 1.19   Dec 14 2005 11:36:50   jbroome
//Updated page title strings
//Resolution for 3315: VisitPlanner: Wrong Header Title
//
//   Rev 1.18   Dec 06 2005 17:19:04   pcross
//Changed browser title to be retrieved from usual resource string
//Resolution for 3315: VisitPlanner: Wrong Header Title
//
//   Rev 1.17   Dec 06 2005 14:29:54   pcross
//Updated the property that handles whether the map location control is interactive or not. Before it could be invisible, now it is always visible but you can show just text or text and interactive controls 
//Resolution for 3278: Visit Planner: The maps panel on the journey results page is missing 'select new location' and 'i' buttons
//
//   Rev 1.16   Nov 30 2005 17:58:18   rhopkins
//Corrections to the button alignments
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//Resolution for 3242: UEE: Netscape - buttons overlap with journey summary on results page
//
//   Rev 1.15   Nov 25 2005 16:12:36   jbroome
//Updated Amend journey event due to continued issues
//
//   Rev 1.14   Nov 24 2005 16:37:48   jbroome
//Fixed problems with Amending journey
//Resolution for 2954: Visit Planner (CG): Resolved locations wrongly need to be re-resolvedx after amend journey
//Resolution for 3166: Visit Planner - Journey details not found again when amend function used.  Using back button causes server error
//Resolution for 3194: Visit Planner - Server error during journey amend
//
//   Rev 1.13   Nov 24 2005 10:17:56   tolomolaiye
//Fixes for IRs 3174 & 3157
//Resolution for 3157: Visit planner: Message states overlapping jourenys when jourenys no longer overlap
//Resolution for 3174: Visit Planner - Amend button doesn't allow Length of Stay to be edited
//
//   Rev 1.12   Nov 17 2005 09:10:32   asinclair
//Made changes to the Amend_CommandEventHandler
//Resolution for 2954: Visit Planner (CG): Resolved locations wrongly need to be re-resolvedx after amend journey
//
//   Rev 1.11   Nov 16 2005 09:29:42   tolomolaiye
//Fixes for IR 3009 & 3068
//Resolution for 3009: Visit Planner: Clicking 'Back' on Help from VP Results Map view causes crash
//Resolution for 3068: Visit Planner - Journey results page font and borders
//
//   Rev 1.10   Nov 11 2005 16:22:56   tolomolaiye
//Added text for help label
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.9   Nov 10 2005 14:08:00   tolomolaiye
//Updated view state and table summaries
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.8   Nov 07 2005 09:58:36   tolomolaiye
//Modifications for Visit Planner
//
//   Rev 1.7   Oct 29 2005 16:08:06   jbroome
//Added scroll to click, sorted out layout issues, ensure correct journey index used.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.6   Oct 28 2005 14:49:42   tolomolaiye
//Changes from code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Oct 26 2005 11:37:38   tmollart
//Modified load order so AmendSaveSendControl is always initalised with the PageID if we are displaying results or not.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 24 2005 16:43:50   tolomolaiye
//Updated Map results
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 18 2005 15:12:00   tolomolaiye
//Updated references to the resource file
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 11 2005 17:48:56   tolomolaiye
//Work in progress
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Oct 07 2005 16:54:48   tolomolaiye
//Work in progress
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 30 2005 14:19:54   tolomolaiye
//Initial revision.
//

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web.Support;
using TransportDirect.Common.Logging;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.LocationService;
using Logger = System.Diagnostics.Trace;
using System.Globalization;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for VisitPlannerResults.
	/// </summary>
	public partial class VisitPlannerResults : TDPage
	{
		protected ErrorDisplayControl  errorDisplayControl;
		protected VisitPlannerRequestDetailsControl visitPlannerJourneyDetailsControl;
		protected VisitPlannerJourneyOptionsControl visitPlannerJourneyOptionsControl;
		protected JourneyDetailsControl journeyDetailsVisitResults;
		protected JourneyDetailsTableControl journeyDetailsTableVisitResults;
		protected AmendSaveSendControl visitAmendSaveSendControl;
		protected ResultsFootnotesControl footnotesControl;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyChangeSearchControl journeyChangeSearchVisitResults;

		private ResultsAdapter resultAdapter = new ResultsAdapter();
		private InputAdapter visitInputAdapter = new InputAdapter();
		private VisitPlannerAdapter visitPlanner = new VisitPlannerAdapter();

		// Flag used to ensure some controls are not displayed if we have no results
        private bool errorWithResults = false;

        private TrackingControlHelper trackingHelper;

		/// <summary>
		/// Constructor - sets the page id and local resource manager
		/// </summary>
		public VisitPlannerResults(): base()
		{
			pageId = PageId.VisitPlannerResults;
			this.LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
		}

		#region Page Load and Initialisation Methods

		/// <summary>
		/// Page_Load method populates all controls on the page
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            trackingHelper = new TrackingControlHelper();
			//get the title of the page
			PageTitle = GetResource("VisitPlanner.DefaultPageTitle");

			//the request details control should always be visible
			visitPlannerJourneyDetailsControl.Visible = true;

            //hide all output controls
			HideControls();

			// Initialise page here.
			visitAmendSaveSendControl.Initialise(this.pageId);
			
			//check if there are any errors. If there are display the error control
			if (PopulateAndSetError())
			{
				//there are errors - show the error control
				errorDisplayControl.Visible = true;
                				
                //used to ensure controls are not rendered
                errorWithResults = true;
			}
			else 
			{
				//there are no errors - populate and display the controls
				visitPlannerJourneyOptionsControl.Visible = true;
				visitAmendSaveSendControl.Visible = true;
				
				labelDetailsTitle.Text = GetResource("VisitPlannerResults.labelDetailsTitle.Text");
				labelMapsTitle.Text = GetResource("VisitPlannerResults.labelMapsTitle.Text");
				PopulateControls();

				// Only display the CompareCO2 buttons if switch is turned on.
				// We're not displaying the button because functionality on Journey Emissions Compare and 
				// subsequent functionality (e.g. selecting Door-to-door link on left hand navigation)
				// has adverse effects
				buttonCompareEmissions.Visible = false; //JourneyEmissionsHelper.JourneyEmissionsPTAvailable;
				buttonCompareEmissions.Text = GetResource("VisitPlannerResults.CompareEmissions.Text");
			}

            // Set the initial result mode to show, should only be set
            // if coming back from the car park information
            if (TDSessionManager.Current.GetOneUseKey(SessionKey.MapView) != null)
            {
                TDSessionManager.Current.ResultsPageState.ResultsMode = ResultsModes.MapView;
            }
            else if (!Page.IsPostBack)
            {
                TDSessionManager.Current.ResultsPageState.ResultsMode = ResultsModes.SchematicDetailsView;
            }

            //Track user planned dates for journey
            TDJourneyParametersVisitPlan journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

            if (journeyParams != null && !IsPostBack)
            {
                try
                {
                    
                    // Add Tracking Parameter
                    trackingHelper.AddTrackingParameter(this.pageId.ToString(), "OutwardDate", string.Format("{0}{1}", journeyParams.OutwardDayOfMonth, journeyParams.OutwardMonthYear.Replace("/", "")));
                    if (journeyParams.OutwardAnyTime)
                    {
                        trackingHelper.AddTrackingParameter(this.pageId.ToString(), "OutwardTime", "AnyTime");
                    }
                    else
                    {
                        trackingHelper.AddTrackingParameter(this.pageId.ToString(), "OutwardTime", string.Format("{0}{1}", journeyParams.OutwardHour, journeyParams.OutwardMinute));
                    }

                    AddDaysToDepartTrackingParam(journeyParams.OutwardDayOfMonth, journeyParams.OutwardMonthYear);
                    
                }
                catch (Exception ex)
                {
                    string message = "TrackingControlHelper Exception: " + ex.StackTrace;
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                                TDTraceLevel.Error, message);
                    Logger.Write(oe);
                }

            }

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextVisitPlannerResults);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Handles the page prerender event. This performs any last-minute updates to the controls
		/// that are displayed to the user
		/// </summary>
		protected void OnPreRender(object sender, System.EventArgs e)
		{
            // Only show the Details, Table, Map control if we had results
            if (!errorWithResults)
            {
                ShowModeControls();

                SetPrintableControl();
            }

			//if page is set to summary view show the full details of the requested journey
			visitPlannerJourneyDetailsControl.FullDisplay = 
				(TDSessionManager.Current.ResultsPageState.CurrentViewSelection == 0) ? true : false;
            
            // If there were no results, we still want to show the Requested journey header
            if (errorWithResults)
            {
                visitPlannerJourneyDetailsControl.FullDisplay = true;
            }
            
            // CCN 0427 made amend button in visitplannerrequestdetailscontrol to hide.
            visitPlannerJourneyDetailsControl.AmendButtonVisible = false;
			visitPlannerJourneyDetailsControl.ShowJourneyDetails();
            
		}

        /// <summary>
        /// Adds a tracking parameter to pass outward days to depart or return days to depart for intellitracker
        /// </summary>
        /// <param name="day"></param>
        /// <param name="monthYear"></param>
        /// <param name="isReturn"></param>
        private void AddDaysToDepartTrackingParam(string day, string monthYear)
        {
            try
            {
                string trackingParamKey = "OutwardDaysToDepart";

                string dateString = string.Format("{0}/{1}", day, monthYear);


                DateTime journeyDate = DateTime.MinValue;

                DateTime.TryParseExact(dateString, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out journeyDate);

                if (journeyDate != DateTime.MinValue)
                {

                    int daysToDepart = journeyDate.Subtract(DateTime.Now.Date).Days;

                    trackingHelper.AddTrackingParameter(this.pageId.ToString(), trackingParamKey, daysToDepart.ToString());
                }
            }
            catch (Exception ex)
            {
                string message = "TrackingControlHelper Exception: " + ex.StackTrace;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                            TDTraceLevel.Error, message);
                Logger.Write(oe);
            }

        }

		/// <summary>
		/// Sets up the necessary event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
			this.visitPlannerJourneyOptionsControl.SelectionControl1.LaterCommand += new CommandEventHandler(this.Later_CommandEventHandler);
			this.visitPlannerJourneyOptionsControl.SelectionControl1.EarlierCommand += new CommandEventHandler(this.Earlier_CommandEventHandler);
			this.visitPlannerJourneyOptionsControl.SelectionControl1.SelectionChangedEvent += new CommandEventHandler(this.SelectionChanged_CommandEventHandler);

			this.visitPlannerJourneyOptionsControl.SelectionControl2.LaterCommand += new CommandEventHandler(this.Later_CommandEventHandler);
			this.visitPlannerJourneyOptionsControl.SelectionControl2.EarlierCommand += new CommandEventHandler(this.Earlier_CommandEventHandler);
			this.visitPlannerJourneyOptionsControl.SelectionControl2.SelectionChangedEvent += new CommandEventHandler(this.SelectionChanged_CommandEventHandler);
			
			this.visitPlannerJourneyOptionsControl.SelectionControl3.LaterCommand += new CommandEventHandler(this.Later_CommandEventHandler);
			this.visitPlannerJourneyOptionsControl.SelectionControl3.EarlierCommand += new CommandEventHandler(this.Earlier_CommandEventHandler);
			this.visitPlannerJourneyOptionsControl.SelectionControl3.SelectionChangedEvent += new CommandEventHandler(this.SelectionChanged_CommandEventHandler);

            journeyChangeSearchVisitResults.AmendButton.Click += new EventHandler(Amend_EventHandler);
			this.visitPlannerJourneyDetailsControl.AmendCommand += new CommandEventHandler(this.Amend_CommandEventHandler);
			this.journeyChangeSearchVisitResults.HelpButton.HelpEvent +=new EventHandler(helpControl_HelpEvent);

			this.visitPlannerJourneyOptionsControl.OKCommand += new CommandEventHandler(this.OK_CommandEventHandler);	
			
			this.journeyDetailsVisitResults.MapButtonClicked += new MapButtonClickEventHandler(this.MapButtonClicked);
			this.journeyDetailsTableVisitResults.MapButtonClicked += new MapButtonClickEventHandler(this.MapButtonClicked);
		
			this.buttonCompareEmissions.Click += new EventHandler(this.buttonCompareEmissions_Click);
		}

		/// <summary>
		/// Hide all the output controls on the page except Visit Planner Request details control
		/// </summary>
		private void HideControls()
		{
			journeyDetailsVisitResults.Visible = false;
			journeyDetailsVisitResults.EnableViewState = false;

			journeyDetailsTableVisitResults.Visible = false;
			journeyDetailsTableVisitResults.EnableViewState = false;

			journeyDetailsPanel.Visible = false;
			journeyDetailsPanel.EnableViewState = false;

			journeyMapPanel.Visible = false;

			footnotesControl.Visible = false;
			footnotesControl.EnableViewState = false;

			visitPlannerJourneyOptionsControl.Visible = false;

			visitAmendSaveSendControl.Visible = false;

			errorDisplayControl.Visible = false;
			errorDisplayControl.EnableViewState = false;
		}

		/// <summary>
		/// Show the appropriate mode control based on the the mode of the page (ResultsMode)
		/// </summary>
		private void ShowModeControls()
		{
			switch(TDSessionManager.Current.ResultsPageState.ResultsMode)
			{
				case ResultsModes.SchematicDetailsView: 
					ShowAndPopulateJourneyDetailsControl();
					break;   
               
				case ResultsModes.TabularDetailsView:    
					ShowAndPopulateJourneyDetailsTableControl();
					break;   

				case ResultsModes.MapView: 
					//map view
					ShowAndPopulateJourneyMapControl();
					break; 
			}

			// Results mode and current view selection can be out of sync if user has clicked map button on details controls
			if (TDSessionManager.Current.ResultsPageState.CurrentViewSelection != Convert.ToInt32(TDSessionManager.Current.ResultsPageState.ResultsMode, TDCultureInfo.CurrentUICulture.NumberFormat))
			{
				TDSessionManager.Current.ResultsPageState.CurrentViewSelection = Convert.ToInt32(TDSessionManager.Current.ResultsPageState.ResultsMode, TDCultureInfo.CurrentUICulture.NumberFormat);
			}

			//set the index for the selection drop down list
			visitPlannerJourneyOptionsControl.ShowSelection.SelectedIndex = TDSessionManager.Current.ResultsPageState.CurrentViewSelection; 
		}

		#endregion

		#region Control Population methods
		/// <summary>
		/// Populates the static text controls on the pages and 
		/// determines if there are any errors in displaying data
		/// </summary>
		/// <returns>true if there are no errors, false if there are errors</returns>
		private bool PopulateAndSetError()
		{
			//get the current session and itinerary objects
			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			ITDJourneyResult journeyResult = sessionManager.JourneyResult;
			
			//populate static controls from the resource file
			labelVisitResultsTitle.Text = GetResource("VisitPlannerResults.labelVisitResultsTitle");
			journeyChangeSearchVisitResults.HelpUrl = GetResource("VisitPlannerResults.helpControl.HelpUrl");

			//populate the requestdetailscontrol
			TDJourneyParametersVisitPlan parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;

            if (parameters == null)
            {
                parameters = new TDJourneyParametersVisitPlan();
                sessionManager.JourneyParameters = parameters;
            }

			visitPlanner.PopulateVisitPlannerRequestDetailsControl(visitPlannerJourneyDetailsControl, parameters);

			//Populate the ErrorDisplayControl and return true or false based on whther or not ther are errors
			//check both the itinerary manage and the journeyresult
			if (resultAdapter.PopulateErrorDisplayControl(errorDisplayControl, itineraryManager))
				return true;
			else if (resultAdapter.PopulateErrorDisplayControl(errorDisplayControl, journeyResult))
				return true;
			else
				return false;			
		}

		/// <summary>
		/// Populate all the visit planner related controls on the page
		/// </summary>
		private void PopulateControls()
		{
			footnotesControl.Visible = true;

			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDJourneyParametersVisitPlan parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;
			
			errorDisplayControl.Visible = visitInputAdapter.PopulateErrorDisplayControl(errorDisplayControl, TDSessionManager.Current.ValidationError);
			
			//populate journey line control
			visitPlanner.PopulateJourneyLineControl(visitPlannerJourneyOptionsControl.JourneyLine, parameters);
		
			//populate routeselection coontrols
			VisitPlannerItineraryManager visitItineraryManager = (VisitPlannerItineraryManager)TDSessionManager.Current.ItineraryManager;
			visitPlanner.PopulateRouteSelectionControl(visitPlannerJourneyOptionsControl.SelectionControl1, visitItineraryManager, 0);
			visitPlanner.PopulateRouteSelectionControl(visitPlannerJourneyOptionsControl.SelectionControl2, visitItineraryManager, 1);
			
			if (parameters.ReturnToOrigin)
			{
				visitPlanner.PopulateRouteSelectionControl(visitPlannerJourneyOptionsControl.SelectionControl3, visitItineraryManager, 2);
			}
			else
			{
				visitPlannerJourneyOptionsControl.SelectionControl3.Visible = false;
			}

			// Intialise controls
            mapJourneyControlOutward.Initialise(true, false, false);
			journeyDetailsVisitResults.Initialise(true, false, false,true,sessionManager.FindAMode);
			journeyDetailsVisitResults.MyPageId = pageId;
			journeyDetailsTableVisitResults.Initialise(true, sessionManager.FindAMode);
			journeyDetailsTableVisitResults.MyPageId = pageId;

            footnotesControl.IsTripPlanner = true;

		}

		/// <summary>
		/// Show and populate the journey details control
		/// </summary>
		private void ShowAndPopulateJourneyDetailsControl()
		{
			journeyDetailsVisitResults.Initialise(true, false, false,true,TDSessionManager.Current.FindAMode);
			journeyDetailsVisitResults.MyPageId = pageId;
			journeyDetailsPanel.Visible = true;
			journeyDetailsVisitResults.Visible = true;
			journeyDetailsVisitResults.EnableViewState = true;

		}

		/// <summary>
		/// Show and populate the JourneyDetailsTableControl object
		/// </summary>
		private void ShowAndPopulateJourneyDetailsTableControl()
		{
            journeyDetailsTableVisitResults.Initialise(true, TDSessionManager.Current.FindAMode);
			journeyDetailsTableVisitResults.MyPageId = pageId;
			journeyDetailsPanel.Visible = true;
			journeyDetailsTableVisitResults.Visible = true;
			journeyDetailsTableVisitResults.EnableViewState = true;
		}

		/// <summary>
		/// Show and populate the JourneyMapControl object
		/// </summary>
		private void ShowAndPopulateJourneyMapControl()
		{
			journeyMapPanel.Visible = true;
			ShowMapControl();
		}

        /// <summary>
        /// Sets up the printable control with the querystring params needed
        /// </summary>
        private void SetPrintableControl()
        {
            // Add the javascript to set the map viewstate on client side
            PrintableButtonHelper printHelper = new PrintableButtonHelper(mapJourneyControlOutward.MapId,
                    mapJourneyControlOutward.MapSymbolsSelectId);

            // Only attach if maps are visible
            if (mapJourneyControlOutward.Visible)
            {
                journeyChangeSearchVisitResults.PrinterFriendlyPageButton.PrintButton.OnClientClick = printHelper.GetClientScript();
            }
        }
        

		#endregion

		#region Command Event Handlers

        private void Amend_EventHandler(object sender, EventArgs e)
        {
            ITDSessionManager sessionManager = TDSessionManager.Current;

            //set ambiguity mode to false
            sessionManager.InputPageState.AmbiguityMode = false;

            //get the existing journey parameters
            TDJourneyParametersVisitPlan parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;

            parameters.GetLocation(0).Status = TDLocationStatus.Valid;
            parameters.GetLocation(1).Status = TDLocationStatus.Valid;
            parameters.GetLocation(2).Status = TDLocationStatus.Valid;

            // Update Page states
            sessionManager.InputPageState.VisitAmendMode = true;
            sessionManager.ResultsPageState = new ResultsPageState();

            // Call new search on Itinerary Manager			
            TDItineraryManager.Current.ResetItinerary();

            //invoke the appropriate transition
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerAmend;		
        }

		/// <summary>
		/// Event hander for the Amend journey button
		/// </summary>
		private void Amend_CommandEventHandler(object sender, CommandEventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

			//set ambiguity mode to false
			sessionManager.InputPageState.AmbiguityMode = false;

			//get the existing journey parameters
			TDJourneyParametersVisitPlan parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;
			
			parameters.GetLocation(0).Status = TDLocationStatus.Valid;
			parameters.GetLocation(1).Status = TDLocationStatus.Valid;
			parameters.GetLocation(2).Status = TDLocationStatus.Valid;

			// Update Page states
			sessionManager.InputPageState.VisitAmendMode = true;
			sessionManager.ResultsPageState = new ResultsPageState();

			// Call new search on Itinerary Manager			
			TDItineraryManager.Current.ResetItinerary();
			
			//invoke the appropriate transition
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerAmend;					
		}


		/// <summary>
		/// Event handler for the earlier button command on the routeselection control
		/// </summary>
		private void Earlier_CommandEventHandler(object sender, CommandEventArgs e)
		{
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerResultsMore;
			
			int segmentIndex = Convert.ToInt32(e.CommandArgument, TDCultureInfo.CurrentUICulture.NumberFormat);
			VisitPlannerItineraryManager visitItineraryManager = (VisitPlannerItineraryManager)TDSessionManager.Current.ItineraryManager;			
			visitPlanner.RouteSelectionEarlier(visitItineraryManager, segmentIndex);
		}

		/// <summary>
		/// Event handler for the later button command on the routeselection control
		/// </summary>
		private void Later_CommandEventHandler(object sender, CommandEventArgs e)
		{
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerResultsMore;

			int segmentIndex = Convert.ToInt32(e.CommandArgument, TDCultureInfo.CurrentUICulture.NumberFormat);
			VisitPlannerItineraryManager visitItineraryManager = (VisitPlannerItineraryManager)TDSessionManager.Current.ItineraryManager;			
			visitPlanner.RouteSelectionLater(visitItineraryManager, segmentIndex);
		}

		/// <summary>
		/// Event handler for the OK button on the VisitPlannerRequestDetailsControl
		/// </summary>
		private void OK_CommandEventHandler(object sender, CommandEventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			VisitPlannerItineraryManager visitItineraryManager = (VisitPlannerItineraryManager)TDSessionManager.Current.ItineraryManager;			

			//check if the selection change event has been invoked
			if (visitPlannerJourneyOptionsControl.SelectionControl1.NewSelection || 
				visitPlannerJourneyOptionsControl.SelectionControl2.NewSelection ||
				visitPlannerJourneyOptionsControl.SelectionControl3.NewSelection)
			{
				if (!visitItineraryManager.ValidateSelectedJourneyTimes())
				{
					errorDisplayControl.Visible = visitInputAdapter.PopulateErrorDisplayControl(errorDisplayControl, TDSessionManager.Current.ValidationError);
					//clear the warnings object
					TDSessionManager.Current.ValidationError.Initialise();
				}
			}
			
			//set the currentviewselection to the selected index and determine the page transition
			sessionManager.ResultsPageState.CurrentViewSelection = visitPlannerJourneyOptionsControl.ShowSelection.SelectedIndex;

            // Track the view change for intellitracker
            string viewChangeEvent = "SummaryView";

			switch(sessionManager.ResultsPageState.CurrentViewSelection)
			{
				case 1: 
					//Schematic view
					sessionManager.ResultsPageState.ResultsMode = ResultsModes.SchematicDetailsView;  
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerResultsSchematicView;
                    viewChangeEvent = "DiagramView";
                    break;

				case 2:    
					//tabular view
					sessionManager.ResultsPageState.ResultsMode = ResultsModes.TabularDetailsView;
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerResultsTableView;
                    viewChangeEvent = "TableView";
                    break;   

				case 3: 
					//map view
					sessionManager.ResultsPageState.ResultsMode = ResultsModes.MapView;
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerResultsMapView;
                    viewChangeEvent = "MapView";
                    break; 

				default:
					sessionManager.ResultsPageState.ResultsMode = ResultsModes.Summary;
                    viewChangeEvent = "SummaryView";
                    break;
			}

            try
            {
                trackingHelper.AddTrackingParameter(this.pageId.ToString(), viewChangeEvent, TrackingControlHelper.CLICK);
            }
            catch (Exception ex)
            {
                string message = "TrackingControlHelper Exception: " + ex.StackTrace;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                            TDTraceLevel.Error, message);
                Logger.Write(oe);

            }
				
			
			// Ensure page scrolls with OK button at top of page
			if (sessionManager.ResultsPageState.ResultsMode != ResultsModes.Summary)
				this.ScrollManager.RestPageAtElement(visitPlannerJourneyOptionsControl.OkButton.ClientID);
			else
				// Except if viewing summary, then scroll to top
				this.ScrollManager.RestPageAtElement(headerControl.ClientID);
		}

		/// <summary>
		/// Event handler for the selection changed event on the route selection control
		/// </summary>
		private void SelectionChanged_CommandEventHandler(object sender, CommandEventArgs e)
		{
			TDItineraryManager itineraryManager = TDSessionManager.Current.ItineraryManager;

			int selectedIndex = 0;
			int selectedJourneyIndex = 0;
			int segmentIndex = Convert.ToInt32(e.CommandArgument, TDCultureInfo.CurrentUICulture.NumberFormat);
			
			switch(segmentIndex)
			{
				case 0:
					selectedIndex = visitPlannerJourneyOptionsControl.SelectionControl1.SelectedItemIndex;
					selectedJourneyIndex = visitPlannerJourneyOptionsControl.SelectionControl1.SelectedItemJourneyIndex;
					break;

				case 1:
					selectedIndex = visitPlannerJourneyOptionsControl.SelectionControl2.SelectedItemIndex;
					selectedJourneyIndex = visitPlannerJourneyOptionsControl.SelectionControl2.SelectedItemJourneyIndex;
					break;

				case 2:
					selectedIndex = visitPlannerJourneyOptionsControl.SelectionControl3.SelectedItemIndex;
					selectedJourneyIndex = visitPlannerJourneyOptionsControl.SelectionControl3.SelectedItemJourneyIndex;
					break;
			}
			resultAdapter.SetSelectedOutwardJourneyIndex(itineraryManager, segmentIndex, selectedIndex, selectedJourneyIndex);
		}

		/// <summary>
		/// Event handler that fires when any "Map" button on the page is clicked
		/// </summary>
		private void MapButtonClicked(object sender, MapButtonClickEventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Need to make sure map is visible before preRender, so that symbols etc can be added 
			ShowAndPopulateJourneyMapControl();

            // Now show the selected leg
            int selectedLeg = e.LegIndex;
            mapJourneyControlOutward.ShowSelectedLeg = selectedLeg;

			sessionManager.ResultsPageState.ResultsMode = ResultsModes.MapView;
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerResultsMapView;
		}

		/// <summary>
		/// Handle the event when the "Help" button is clicked
		/// </summary>
		private void helpControl_HelpEvent(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Handler for the Compare emissions button.
		/// Navigates to the Compare emissions page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCompareEmissions_Click(object sender, EventArgs e)
		{
			// Set page id in stack so we know where to come back to
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId );

			// Reset the journey emissions page state, to clear it of any previous values
			TDSessionManager.Current.JourneyEmissionsPageState.Initialise();

			// Navigate to the Journey Emissions Compare Journey page
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyEmissionsCompareJourney;
		}

		#endregion

		/// <summary>
		/// Shows the map control 
		/// </summary>
		private void ShowMapControl()
		{
            mapJourneyControlOutward.Initialise(true, false, false);
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Initialisation Event
		/// </summary>
		override protected void OnInit(EventArgs e)
		{
			ExtraWiringEvents();
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
			this.PreRender += new System.EventHandler(this.OnPreRender);

		}
		#endregion

	}
}
