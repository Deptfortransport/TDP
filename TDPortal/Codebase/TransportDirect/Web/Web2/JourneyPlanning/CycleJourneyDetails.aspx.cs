// *********************************************** 
// NAME                 : CycleJourneyDetails.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 10/06/2008
// DESCRIPTION          : Journey details page for Find cycle
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/CycleJourneyDetails.aspx.cs-arc  $ 
//
//   Rev 1.25   Aug 01 2011 14:51:12   apatel
//Updated to add DaysToDeparture Intellitracker tags
//Resolution for 5718: DaysToDeparture tracking tag not get sent to Intellitracker
//
//   Rev 1.24   Jul 29 2011 09:59:38   apatel
//Added tracking parameters for show/hide map/detail view buttons
//Resolution for 5713: Intelitracker tags not added for Cycle detail page map/detail toggle button
//
//   Rev 1.23   Jul 07 2011 09:28:36   apatel
//Updated to show return journey intellitracker tags 
//Resolution for 5703: Intellitracker date and time tags
//
//   Rev 1.22   Jul 06 2011 15:55:04   apatel
//Updated to correct Tracking Helper initialisation
//Resolution for 5703: Intellitracker date and time tags
//
//   Rev 1.21   Jun 09 2011 10:55:34   PScott
//IR 5703 Intellitracker date and time tags
//
//Resolution for 5703: Intellitracker date and time tags
//
//   Rev 1.20   May 18 2011 13:53:46   apatel
//updated to add intellitracker tags
//
//   Rev 1.19   Mar 30 2010 10:11:30   mmodi
//Hide the Details title if journey errors
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.18   Dec 10 2009 11:08:22   mmodi
//Fixed return cycle journey maps and set up cycle directions table links to map
//
//   Rev 1.17   Dec 03 2009 14:03:24   mmodi
//Updated to display Cycle journey direction symbols
//
//   Rev 1.16   Nov 29 2009 12:42:08   mmodi
//Updated map initialise to hide the show journey buttons, and ensure only  one map is shown
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.15   Nov 16 2009 17:07:02   apatel
//Updated for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.14   Oct 06 2009 14:38:34   apatel
//Social bookmarking changes
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.13   Sep 25 2009 11:34:12   apatel
//Updated for Cycle journey 'arrive by' journeys - USD UK5688851
//Resolution for 5328: CTP - Arrive by journey results show depart after time in summary
//
//   Rev 1.12   Feb 12 2009 12:45:28   mturner
//Fixed session management bug
//Resolution for 5245: Cycle Planner - After Amend Map page shows wrong journey
//
//   Rev 1.11   Dec 11 2008 14:10:18   mmodi
//Only populate the results adapter if there are journeys
//Resolution for 5208: Cycle Planner - Results title summary throws exception when results fail
//
//   Rev 1.10   Oct 28 2008 10:34:08   mmodi
//Added screen reader labels
//
//   Rev 1.9   Oct 15 2008 17:16:00   mmodi
//Updated help
//
//   Rev 1.8   Oct 14 2008 15:32:38   mmodi
//Updated related links
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.7   Oct 09 2008 15:57:24   mmodi
//Updated to correct show map button text for return journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//Resolution for 5142: Cycle Planner - 'Hide Map' button for 'Return Journey' is not displayed on 'Cycle Journey Details' page
//Resolution for 5144: Cycle Planner - On clicking 'Show Map' button for 'Outward Journey' on 'Cycle Journey Details' page, 'Show Map' button for 'Return Journey' is not displayed
//
//   Rev 1.6   Sep 18 2008 11:38:54   mmodi
//Do not sure Map directions button on Cycle Details page
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Sep 02 2008 10:31:20   mmodi
//Show messages for CJP user
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Aug 22 2008 10:35:08   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 08 2008 12:05:38   mmodi
//Updated as part of workstream
//
//   Rev 1.2   Aug 06 2008 14:50:54   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 13:30:06   mmodi
//Added controls and AJAX methods
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:14:52   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ResourceManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SuggestionLinkService;
using TransportDirect.UserPortal.Resource;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using System.Globalization;

namespace TransportDirect.UserPortal.Web.Templates
{
    public partial class CycleJourneyDetails : TDPage
    {
        #region Private members

        private const string scriptCommonAPI = "Common";

        private ITDSessionManager tdSessionManager;
        private TDJourneyParametersMulti journeyParameters;
        private TDJourneyViewState viewState;
        
        private bool outwardExists = false;
        private bool returnExists = false;
        private bool outwardArriveBefore = false;
        private bool returnArriveBefore = false;

        // Helper
        PlannerOutputAdapter plannerOutputAdapter;

        private TrackingControlHelper trackingHelper;

        #endregion

        #region Constructor
        /// <summary>
		/// Default Constructor
		/// </summary>
        public CycleJourneyDetails()
		{
            pageId = PageId.CycleJourneyDetails;
        }
        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_OnInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            this.cycleAllDetailsControlOutward.ButtonShowMap.Click += new EventHandler(buttonShowMapOutward_Click);
            this.cycleAllDetailsControlReturn.ButtonShowMap.Click += new EventHandler(buttonShowMapReturn_Click);
            socialBookMarkLinkControl.EmailLinkButton.Click += new EventHandler(EmailLink_Click);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the session
            tdSessionManager = TDSessionManager.Current;
            journeyParameters = tdSessionManager.JourneyParameters as TDJourneyParametersMulti;
            viewState = tdSessionManager.JourneyViewState;
            
            // Setup the helper
            plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

            DisplayJourneyErrorMessages();

            InitialiseControls();

            LoadHelp();

            LoadLeftHandNavigation();
        }

        /// <summary>
		/// Page PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetControlVisibility();

            LoadResources();

            SetupSkipLinksAndScreenReaderText();

            RegisterJavascript();

            SetPrintableControl();
            
            // Prerender setup for the AmendSaveSend control and its child controls
            plannerOutputAdapter.AmendSaveSendControlPreRender(amendSaveSendControl);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads text and images on page
        /// </summary>
        private void LoadResources()
        {
            PageTitle = GetResource("CyclePlanner.JourneyDetails.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            // Show map buttons text
            if (mapJourneyControlOutward.Visible)
            {
                cycleAllDetailsControlOutward.ButtonShowMap.Text = GetResource("HideMap.Text");
            }
            else
            {
                cycleAllDetailsControlOutward.ButtonShowMap.Text = GetResource("ShowMap.Text");
            }

            if (mapJourneyControlReturn.Visible)
            {
                cycleAllDetailsControlReturn.ButtonShowMap.Text = GetResource("HideMap.Text");
            }
            else
            {
                cycleAllDetailsControlReturn.ButtonShowMap.Text = GetResource("ShowMap.Text");
            }
        }

        /// <summary>
        /// Sets up the Help text for the page
        /// </summary>
        private void LoadHelp()
        {
            journeyChangeSearchControl.HelpUrl = GetResource("CycleJourneyDetails.HelpPageUrl");
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
            
            // Social Bookmarking Control
            socialBookMarkLinkControl.BookmarkDescription = journeysSearchedControl.ToString();
            socialBookMarkLinkControl.EmailLink.NavigateUrl = Request.Url.AbsoluteUri + "#JourneyOptions";

        }

        /// <summary>
        /// Sets the text for the skip to links (for screenreader browsers).
        /// </summary>
        private void SetupSkipLinksAndScreenReaderText()
        {
            // Setup gif resource for images (1 invisible image for all skip links)
            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");

            imageMainContentSkipLink1.ImageUrl = skipLinkImageUrl;
            imageMainContentSkipLink1.AlternateText = GetResource("CyclePlanner.JourneyDetails.imageMainContentSkipLink.AlternateText");

            imageOutwardJourneySkipLink.ImageUrl = skipLinkImageUrl;
            imageOutwardJourneySkipLink.AlternateText = GetResource("CyclePlanner.JourneyDetails.imageOutwardJourneySkipLink.AlternateText");
            imageOutwardJourneySkipLink.Visible = outwardExists;

            imageReturnJourneySkipLink.ImageUrl = skipLinkImageUrl;
            imageReturnJourneySkipLink.AlternateText = GetResource("CyclePlanner.JourneyDetails.imageReturnJourneySkipLink.AlternateText");
            imageReturnJourneySkipLink.Visible = returnExists;
        }

        /// <summary>
        /// Displays any error messages returned following a journey plan
        /// </summary>
        private void DisplayJourneyErrorMessages()
        {
            errorMessagePanel.Visible = false;
            errorDisplayControl.Visible = false;

            ITDCyclePlannerResult tdCyclePlannerResult = tdSessionManager.CycleResult;

            if (tdCyclePlannerResult != null)
            {
                ArrayList errorsList = new ArrayList();

                foreach (CyclePlannerMessage message in tdCyclePlannerResult.CyclePlannerMessages)
                {
                    if (message.Type == TransportDirect.UserPortal.CyclePlannerControl.ErrorsType.Warning)
                    {
                        errorDisplayControl.Type = ErrorsDisplayType.Warning;
                    }

                    string text = string.Empty;

                    // If the user is logged on as a CJP user then we show actual messages returned by the 
                    // cycle planner. These are added by the CyclePlannerManager using the resource id CPExternalMessage
                    // Therefore, when we detect this type of error message and the user is a CJP user,
                    // construct and display the message.
                    if ((message.MessageResourceId == CyclePlannerConstants.CPExternalMessage)
                        && (IsCJPUser()))
                    {
                        text = GetResource(message.MessageResourceId);
                        text += "<br />Code: " + message.MajorMessageNumber + ". Message: " + message.MessageText;
                    }
                    else
                    {   // all other messages
                        text = message.MessageText;
                        if (string.IsNullOrEmpty(text))
                        {
                            text = GetResource(message.MessageResourceId);
                        }
                    }
                                                           
                    errorsList.Add(text);
                }

                errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

                errorDisplayControl.ReferenceNumber = tdCyclePlannerResult.JourneyReferenceNumber.ToString();

                if (errorDisplayControl.ErrorStrings.Length > 0)
                {
                    errorMessagePanel.Visible = true;
                    errorDisplayControl.Visible = true;
                }

                // Clear the error messages in the result
                tdCyclePlannerResult.ClearMessages();
            }
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
        /// Establish whether we have any results
        /// </summary>
        private void DetermineStateOfResults()
        {
            //check for cycle result
            outwardExists = plannerOutputAdapter.CycleExists(true);
            returnExists = plannerOutputAdapter.CycleExists(false);

            // Get time types for journey.
            outwardArriveBefore = tdSessionManager.JourneyViewState.CycleJourneyLeavingTimeSearchType;
            returnArriveBefore = tdSessionManager.JourneyViewState.CycleJourneyReturningTimeSearchType;
        }

        /// <summary>
        /// Initialises controls on page with page and journey details
        /// </summary>
        private void InitialiseControls()
        {
            DetermineStateOfResults();

            outputNavigationControl.Initialise(pageId);

            resultFootnotesControl.Mode = FindAMode.Cycle;

            // Setup the AmendSaveSend control and its child controls
            plannerOutputAdapter.AmendSaveSendControlPageLoad(amendSaveSendControl, this.pageId);

            if (outwardExists || returnExists)
            {
                // Outward/return journey titles
                plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);
            }

            // Setup the cycle details control
            if (outwardExists)
            {
                findSummaryResultTableControlOutward.Initialise(false, true, outwardArriveBefore);
                cycleAllDetailsControlOutward.Initialise(tdSessionManager.CycleResult.OutwardCycleJourney(), true, journeyParameters, tdSessionManager.JourneyViewState);
                mapJourneyControlOutward.Initialise(true, true, false);

                // Set up to add the links to zoom to direction on the map
                if (viewState.OutwardShowMap)
                {
                    cycleAllDetailsControlOutward.SetMapProperties(true, mapJourneyControlOutward.MapId,
                                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                                    mapJourneyControlOutward.FirstElementId, Session.SessionID);
                }
            }

            if (returnExists)
            {
                findSummaryResultTableControlReturn.Initialise(false, false, returnArriveBefore);
                cycleAllDetailsControlReturn.Initialise(tdSessionManager.CycleResult.ReturnCycleJourney(), false, journeyParameters, tdSessionManager.JourneyViewState);
                mapJourneyControlReturn.Initialise(false, true, false);

                // Set up to add the links to zoom to direction on the map
                if (viewState.ReturnShowMap)
                {
                    cycleAllDetailsControlReturn.SetMapProperties(true, mapJourneyControlReturn.MapId,
                                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId,
                                    mapJourneyControlReturn.FirstElementId, Session.SessionID);
                }
            }

            trackingHelper = new TrackingControlHelper();

            //Track user planned dates for journey
            TDJourneyParameters journeyParams = tdSessionManager.JourneyParameters;

            if (journeyParams != null && !IsPostBack)
            {
                try
                {
                    // Add Tracking Parameter
                    trackingHelper.AddTrackingParameter(this.pageId.ToString(), "OutwardDate", string.Format("{0}{1}", journeyParams.OutwardDayOfMonth, journeyParams.OutwardMonthYear.Replace("/", "")));
                    trackingHelper.AddTrackingParameter(this.pageId.ToString(), "OutwardTime", string.Format("{0}{1}", journeyParams.OutwardHour, journeyParams.OutwardMinute));

                    AddDaysToDepartTrackingParam(journeyParams.OutwardDayOfMonth, journeyParams.OutwardMonthYear, false);

                    if (tdSessionManager.CycleRequest.IsReturnRequired)
                    {
                        // Add Tracking Parameter
                        trackingHelper.AddTrackingParameter(this.pageId.ToString(), "ReturnDate", string.Format("{0}{1}", journeyParams.ReturnDayOfMonth, journeyParams.ReturnMonthYear.Replace("/", "")));
                        trackingHelper.AddTrackingParameter(this.pageId.ToString(), "ReturnTime", string.Format("{0}{1}", journeyParams.ReturnHour, journeyParams.ReturnMinute));

                        AddDaysToDepartTrackingParam(journeyParams.ReturnDayOfMonth, journeyParams.ReturnMonthYear, true);
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


            // Outward/return journey hyperlink
            InitialiseHyperlinkImage();
        }

        /// <summary>
        /// Initialises all hyperlinks and images from the resourcing manager.
        /// </summary>
        private void InitialiseHyperlinkImage()
        {
            // Initialise hyperlink text
            hyperLinkReturnJourneys.Text = Global.tdResourceManager.GetString(
                "JourneyPlanner.hyperLinkReturnJourneys.Text", TDCultureInfo.CurrentUICulture);

            hyperLinkImageReturnJourneys.ImageUrl = Global.tdResourceManager.GetString(
                "JourneyPlanner.hyperLinkImageReturnJourneys", TDCultureInfo.CurrentUICulture);

            hyperLinkImageReturnJourneys.AlternateText = Global.tdResourceManager.GetString(
                "JourneyPlanner.hyperLinkImageReturnJourneys.AlternateText", TDCultureInfo.CurrentUICulture);


            hyperLinkOutwardJourneys.Text = Global.tdResourceManager.GetString(
                "JourneyPlanner.hyperLinkOutwardJourneys.Text", TDCultureInfo.CurrentUICulture);

            hyperLinkImageOutwardJourneys.ImageUrl = Global.tdResourceManager.GetString(
                "JourneyPlanner.hyperLinkImageOutwardJourneys", TDCultureInfo.CurrentUICulture);

            hyperLinkImageOutwardJourneys.AlternateText = Global.tdResourceManager.GetString(
                "JourneyPlanner.hyperLinkImageOutwardJourneys.AlternateText", TDCultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Sets the visibility of the controls on the page
        /// </summary>
        private void SetControlVisibility()
        {
            // Hide Title if no journey results
            if (!outwardExists && !returnExists)
            {
                journeyPlannerOutputTitleControl.Visible = false;
            }

            DisplayOutwardComponents(outwardExists);
            DisplayReturnComponents(returnExists);

            // Update the map controls
            if (viewState.OutwardShowMap && (!mapJourneyControlOutward.Visible))
            {
                ShowMapControl(mapJourneyControlOutward, true);
            }

            if (viewState.ReturnShowMap && (!mapJourneyControlReturn.Visible))
            {
                ShowMapControl(mapJourneyControlReturn, false);
            }
            
            // Only show the Details, Summary, Maps... buttons if we have journeys
            outputNavigationControl.Visible = (outwardExists || returnExists);

            // Set up to add the links to zoom to direction on the map
            if (viewState.OutwardShowMap)
            {
                cycleAllDetailsControlOutward.SetMapProperties(true, mapJourneyControlOutward.MapId,
                                mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                                mapJourneyControlOutward.FirstElementId, Session.SessionID);
            }

            // Set up to add the links to zoom to direction on the map
            if (viewState.ReturnShowMap)
            {
                cycleAllDetailsControlReturn.SetMapProperties(true, mapJourneyControlReturn.MapId,
                                mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId,
                                mapJourneyControlReturn.FirstElementId, Session.SessionID);
            }
        }

        /// <summary>
        /// Sets up the printable control with the querystring params needed
        /// </summary>
        private void SetPrintableControl()
        {
            FindCyclePageState findCyclePageState = (FindCyclePageState)tdSessionManager.FindPageState;

            if (findCyclePageState.ShowAllDetailsOutward)
                journeyChangeSearchControl.PrinterFriendlyPageButton.UrlParams = "details=show";
            else
                journeyChangeSearchControl.PrinterFriendlyPageButton.UrlParams = "details=hide";

            string printerUnits = tdSessionManager.InputPageState.Units.ToString();

            if (printerUnits == "kms")
                journeyChangeSearchControl.PrinterFriendlyPageButton.UrlParams += "&units=kms";
            else
                journeyChangeSearchControl.PrinterFriendlyPageButton.UrlParams += "&units=miles";

            // Add the javascript to set the map viewstate on client side
            PrintableButtonHelper printHelper = null;

            if ((outwardExists) && (returnExists))
            {
                // Initialise for both outward and return maps
                printHelper = new PrintableButtonHelper(
                    mapJourneyControlOutward.MapId,
                    mapJourneyControlReturn.MapId,
                    mapJourneyControlOutward.MapSymbolsSelectId,
                    mapJourneyControlReturn.MapSymbolsSelectId,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId);
            }
            else if (outwardExists)
            {
                // Initialise only for outward map
                printHelper = new PrintableButtonHelper(
                    mapJourneyControlOutward.MapId,
                    string.Empty,
                    mapJourneyControlOutward.MapSymbolsSelectId,
                    string.Empty,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                    string.Empty);
            }
            else if (returnExists)
            {
                // Initialise only for return map
                printHelper = new PrintableButtonHelper(
                    string.Empty,
                    mapJourneyControlReturn.MapId,
                    string.Empty,
                    mapJourneyControlReturn.MapSymbolsSelectId,
                    string.Empty,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId);
            }

            // This is the number of maps we want to aim for
            int targetNumberOfMaps = Convert.ToInt32(Properties.Current["CyclePlanner.InteractiveMapping.Map.NumberOfMapTilesTarget"]);

            // first, get the number of maps for our default zoom level
            double scale = Convert.ToDouble(Properties.Current["CyclePlanner.InteractiveMapping.Map.MapTilesDefaultScale"]);

            if (printHelper != null)
            {
                // Only attach if maps are visible
                if (mapJourneyControlOutward.Visible || mapJourneyControlReturn.Visible)
                {
                    journeyChangeSearchControl.PrinterFriendlyPageButton.PrintButton.OnClientClick = printHelper.GetCycleMapClientScript(scale, targetNumberOfMaps, mapJourneyControlOutward.Visible);
                }
            }
        }

        /// <summary>
        /// Sets the visibilities of the "Outward" components.
        /// </summary>
        private void DisplayOutwardComponents(bool visible)
        {
            outwardPanel.Visible = visible;
            findSummaryResultTableControlOutward.Visible = visible;
            cycleAllDetailsControlOutward.Visible = visible;
        }

        /// <summary>
        /// Sets the visibilities of the "Return" components.
        /// </summary>
        private void DisplayReturnComponents(bool visible)
        {
            returnPanel.Visible = visible;
            findSummaryResultTableControlReturn.Visible = visible;
            hyperLinkPanelReturnJourneys.Visible = visible;
            cycleAllDetailsControlReturn.Visible = visible;
        }

        /// <summary>
        /// Registers page and page controls javascript
        /// </summary>
        private void RegisterJavascript()
        {
            // Register the scripts needed only if user has Javascript enabled
            TDPage thePage = this.Page as TDPage;

            if (thePage != null && thePage.IsJavascriptEnabled)
            {
                // Get the global script repository
                ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                // Register the mapping api call script
                thePage.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptCommonAPI, repository.GetScript(scriptCommonAPI, thePage.JavascriptDom));
            }
            else
            {
                cycleAllDetailsControlOutward.ButtonShowMap.Visible = false;
                cycleAllDetailsControlReturn.ButtonShowMap.Visible = false;
            }
        }

        /// <summary>
        /// Adds a tracking parameter to pass outward days to depart or return days to depart for intellitracker
        /// </summary>
        /// <param name="day"></param>
        /// <param name="monthYear"></param>
        /// <param name="isReturn"></param>
        private void AddDaysToDepartTrackingParam(string day, string monthYear, bool isReturn)
        {
            try
            {
                string trackingParamKey = isReturn ? "ReturnDaysToDepart" : "OutwardDaysToDepart";

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
                
        #region Map methods

        /// <summary>
        /// Handler for the Show map button.
        /// Show the whole return journey in a map control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHideMap(bool outward)
        {
            MapJourneyControl journeyMapControl;
            

            // Set up a generic journey map control reference that can represent outward or return and see if it is public transport or car
            if (outward)
            {
                journeyMapControl = mapJourneyControlOutward;
            }
            else
            {
                journeyMapControl = mapJourneyControlReturn;
            }

            // If not visible then show the control and initialise it
            if (journeyMapControl.Visible != true)
            {
                // Handle the simple visual elements
                ShowMapControl(journeyMapControl, outward);
                HideMapControl(!outward);

                
            }
            else
            {
                // Control is visible so hide it
                HideMapControl(outward);
            }

            
        }

        /// <summary>
        /// Handles the visual bits associated with showing the map control - ie makes it visible and updates the buttons
        /// </summary>
        /// <param name="?"></param>
        private void ShowMapControl(MapJourneyControl journeyMapControl, bool outward)
        {
            journeyMapControl.Visible = true;
            journeyMapControl.Initialise(outward, true, false);

            if (outward)
            {
                viewState.OutwardShowMap = true;

                tdSessionManager.InputPageState.PreviousOutwardJourney = -1;
            }
            else
            {
                viewState.ReturnShowMap = true;

                tdSessionManager.InputPageState.PreviousReturnJourney = -1;
            }
        }

        /// <summary>
        /// Handles the visual bits associated with showing the map control - ie makes it visible and updates the buttons
        /// </summary>
        /// <param name="?"></param>
        private void HideMapControl(bool outward)
        {
            if (outward)
            {
                viewState.OutwardShowMap = false;

                mapJourneyControlOutward.Visible = false;

                // Force the directions map link to be hidden
                cycleAllDetailsControlOutward.CycleJourneyDetailsTableControl.IsRowMapLinkVisible = false;
            }
            else
            {
                viewState.ReturnShowMap = false;

                mapJourneyControlReturn.Visible = false;

                // Force the directions map link to be hidden
                cycleAllDetailsControlReturn.CycleJourneyDetailsTableControl.IsRowMapLinkVisible = false;
            }
        }

        /// <summary>
        /// Adds show/hide map button tracking parameters
        /// </summary>
        /// <param name="outward">true if outward</param>
        private void AddMapButtonTrackingParameter(bool outward)
        {
            MapJourneyControl journeyMapControl = outward ? mapJourneyControlOutward : mapJourneyControlReturn;
            string hideMapKey = outward ? "buttonHideMapOutward" : "buttonHideMapReturn";
            string showMapKey = outward ? "buttonShowMapOutward" : "buttonShowMapReturn";

            try
            {
                if (journeyMapControl.Visible)
                {
                    // Add Tracking Parameter
                    trackingHelper.AddTrackingParameter(this.pageId.ToString(), showMapKey, TrackingControlHelper.CLICK);

                }
                else
                {
                    // Add Tracking Parameter
                    trackingHelper.AddTrackingParameter(this.pageId.ToString(), hideMapKey, TrackingControlHelper.CLICK);
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

        #endregion

        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler for the button show map click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowMapOutward_Click(object sender, EventArgs e)
        {
            ShowHideMap(true);
            // add tracking params after the map journey control's visible state changes
            AddMapButtonTrackingParameter(true);
        }
                

        /// <summary>
        /// Event handler for the button show map click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowMapReturn_Click(object sender, EventArgs e)
        {
            ShowHideMap(false);
            // add tracking params after the map journey control's visible state changes
            AddMapButtonTrackingParameter(false);
        }

        /// <summary>
        /// Event handler that responds to the Social Link control's email link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailLink_Click(object sender, EventArgs e)
        {
            if (amendSaveSendControl.IsLoggedIn())
            {
                amendSaveSendControl.SetActiveTab(AmendPanelMode.SendEmailNormal);
            }
            else
            {
                amendSaveSendControl.SetActiveTab(AmendPanelMode.SendEmailLogin);
            }
            amendSaveSendControl.Focus();


            string amendSaveSendControlFocusScript = @"<script>  function ScrollView() { var el = document.getElementById('" + amendSaveSendControl.SendEmailTabButton.ClientID
                                              + @"'); if (el != null){ el.scrollIntoView(); el.focus();}} window.onload = ScrollView;</script>";

            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "CtrlFocus", amendSaveSendControlFocusScript);

        }
        #endregion

        #region AJAX Web method
        /// <summary>
        /// AJAX method which is called by the client to get the chart data. 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static CycleJourneyGraphControl.TDChartData GetChartData(bool outward)
        {
            // WebMethod must be in the ASPX page as it cannot be called directly from the control.
            // It must also be static to allow an AJAX call to be made to it 
            // (for explanation, see: http://encosia.com/2008/04/16/why-do-aspnet-ajax-page-methods-have-to-be-static/ )

            ITDSessionManager sessionManager = TDSessionManager.Current;
            
            CyclePlannerHelper cycleHelper = new CyclePlannerHelper(sessionManager);

            // MT - Fix for USD UK:4103828
            // Call Unload lifecycle events as otherwise in a multiserver environment session
            // state gets out of sync across servers.
            TDSessionManager.Current.OnPreUnload();
            TDSessionManager.Current.OnUnload();
            // MT - End of USD fix

            return cycleHelper.GetChartData(outward);
        }

        #endregion
    }
}
