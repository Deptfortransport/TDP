// *********************************************** 
// NAME             : Summary.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Feb 2012
// DESCRIPTION  	: Journey Summary results options page
// ************************************************
// 

using System;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;
using TDP.Common.PropertyManager;
using TDP.Reporting.Events;
using TDP.UserPortal.SessionManager;
using Logger = System.Diagnostics.Trace;
using TDP.UserPortal.TDPMobile.Adapters;
using System.Collections.Specialized;
using System.Collections.Generic;
using TDP.UserPortal.ScreenFlow;
using TDP.UserPortal.TDPMobile.Controls;
using TDP.Common.ResourceManager;

namespace TDP.UserPortal.TDPMobile
{
    public partial class Summary : TDPPageMobile
    {
        #region Variables

        private const string NO_RESULT_INFO = "JourneySummary.NoResultsFound.UserInfo";
        private const string NO_RESULT_ERROR = "JourneySummary.NoResultsFound.Timeout.Error";

        // Used to log page action events
        private bool logPageActionEvent = false;
        private bool journeyResultAvailable = false;

        private JourneyInputAdapter journeyInputAdapter ;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Summary()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobileSummary;
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            // Attach to the plan journey event
            journeyInputControl.OnPlanJourney += new PlanJourney(journeyInputControl_OnPlanJourney);

            // Attach to the toggle locations event
            journeyInputControl.OnToggleLocation += new PlanJourney(journeyInputControl_OnToggleLocation);

            // Attach to the show journey event
            outwardSummaryControl.ShowJourneyHandler += new OnShowJourney(ShowOutwardJourneyEvent);

            // Attach to the replan journey event
            outwardSummaryControl.ReplanJourneyHandler += new OnReplanJourney(ReplanOutwardJourneyEvent);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            journeyInputAdapter = new JourneyInputAdapter(journeyInputControl);

            // Enable the page auto refresh to allow checking for journeys
            if (!IsPostBack)
            {
                SetupTimer();

                // Populate the journey input control

                // Assume not landing page because should have come from input page therefore inputs
                // will have been validated already
                journeyInputAdapter.PopulateInputControls(true);
            }
            else
            {
                if (((TDPMobile)Master).PageScriptManager.IsInAsyncPostBack)
                {
                    logPageEntry = false;
                }

                // Ensure the input control continues to be in the correct planner mode
                journeyInputAdapter.UpdateInputControls();
            }
            
            #region Check for results

            JourneyResultHelper resultHelper = new JourneyResultHelper();

            // Check for results
            if (waitTimer.Enabled || IsPostBack)
            {
                int waitCount = UpdateWaitCount();

                // Tracks if a page action event should be logged in the PreRender event of page
                logPageActionEvent = true;

                // Base page shouldn't log the page entry because the page action event is logged 
                // by this page (in posbacks only)
                if (IsPostBack)
                {
                    logPageEntry = false;
                }

                journeyResultAvailable = resultHelper.IsJourneyResultAvailable;
                
                if (journeyResultAvailable)
                {
                    #region Bind results

                    // Journeys have returned, display them
                    ITDPJourneyResult journeyResult = resultHelper.JourneyResult;
                    
                    if (journeyResult.Messages.Count > 0)
                    {
                        bool showResultMessage = false;

                        // Contain page redirect url if no journeys (for javascript use) 
                        // to return user to input
                        Common.PageId redirectPage = GetNoResultsRedirectPage(journeyResult.OutwardJourneys.Count);
                        
                        TDPMessage m = null;
                        // Journey Result returned with error messages
                        foreach (TDPMessage message in journeyResult.Messages)
                        {
                            if (message.Type != TDPMessageType.Info)
                                showResultMessage = true;

                            // Clone so the result object is not altered
                            m = message.Clone();

                            // Make all messages in mobile an error type
                            m.Type = TDPMessageType.Error;

                            DisplayMessage(m, redirectPage);
                        }

                        if (showResultMessage)
                        {
                            // Journey Result returned with error message... Add instruction for user
                            DisplayMessage(new TDPMessage(NO_RESULT_INFO, TDPMessageType.Error), redirectPage);
                        }
                    }

                    // Display journeys if they exist
                    if (journeyResult.OutwardJourneys.Count > 0)
                    {
                        // Journey result returned without errors, Bind journey result to journey result controls
                        BindJourneyResult(resultHelper.JourneyRequest, journeyResult);

                        // Display the return journey button
                        planReturnJourneyBtn.Visible = true;
                    }

                    // Stop refresh
                    //waitControl.Visible = false;
                    if (!waitControlDiv.Attributes["class"].Contains("hide"))
                    {
                        waitControlDiv.Attributes["class"] = string.Format("{0} hide", waitControlDiv.Attributes["class"]);
                    }
                    waitTimer.Enabled = false;

                    #endregion
                }
                else
                {
                    // If results not available and have exceeded the wait count, then display error
                    int maxWaitCount = Properties.Current["JourneySummary.Wait.RefreshCount.Max"].Parse(12);

                    if (waitCount > maxWaitCount)
                    {
                        DisplayMessage(new TDPMessage(NO_RESULT_ERROR, TDPMessageType.Error), GetNoResultsRedirectPage(0));
                        DisplayMessage(new TDPMessage(NO_RESULT_INFO, TDPMessageType.Error), GetNoResultsRedirectPage(0));

                        // Stop refresh
                        //waitControl.Visible = false;
                        if (!waitControlDiv.Attributes["class"].Contains("hide"))
                        {
                            waitControlDiv.Attributes["class"] = string.Format("{0} hide", waitControlDiv.Attributes["class"]);
                        }
                        waitTimer.Enabled = false;
                    }
                }
            }

            #endregion

            // Add javascripts specific for this page
            AddJavascript("Input.js");
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();

            SetupControlVisibility();

            #region Log page action event for postbacks

            if (logPageActionEvent)
            {
                // Log in postbacks only because Results could have been available on first render of this page
                // and therefore don't need to log this page action event
                if (IsPostBack && !journeyResultAvailable)
                {
                    // Log an event because user is still waiting for journey results
                    PageEntryEvent logPage = new PageEntryEvent(PageId.MobileSummaryWait, TDPSessionManager.Current.Session.SessionID, false);
                    Logger.Write(logPage);
                }
                else if (IsPostBack && journeyResultAvailable)
                {
                    // Log an event because journey results are now available to user
                    PageEntryEvent logPage = new PageEntryEvent(PageId.MobileSummaryResult, TDPSessionManager.Current.Session.SessionID, false);
                    Logger.Write(logPage);
                }
            }
            else if (((TDPMobile)Master).PageScriptManager.IsInAsyncPostBack)
            {
                // Log an event as a result of partial page update
                PageEntryEvent logPage = new PageEntryEvent(PageId.MobileSummaryPartialUpdate, TDPSessionManager.Current.Session.SessionID, false);
                Logger.Write(logPage);
            }

            #endregion
        }

        #endregion

        #region Control Event Handlers
        
        /// <summary>
        /// Tick event handler for the ajax wait timer
        /// WaitTimer simulates the wait page scenario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WaitTimer_Tick(object sender, EventArgs e)
        {
        }

        #endregion

        #region Event handlers
        
        #region Show journey events

        /// <summary>
        /// Shows the selected outward journey
        /// </summary>
        protected void ShowOutwardJourneyEvent(object sender, JourneyEventArgs e)
        {
            if (e != null)
            {
                // Persist selected journey to session
                JourneyHelper journeyHelper = new JourneyHelper();

                journeyHelper.SetJourneySelected(true, e.JourneyId);

                // Transfer to the Details page
                SetPageTransfer(PageId.MobileDetail);

                AddQueryStringForPage(PageId.MobileDetail);
            }
        }
        
        #endregion

        #region Plan journey events

        /// <summary>
        /// Plan journey event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void journeyInputControl_OnPlanJourney(object sender, PlanJourneyEventArgs e)
        {
            if (e != null)
            {
                SubmitRequest(e.PlannerMode);
            }

            journeyHeadingControl.Visible = false;
            outwardSummaryControl.Visible = false;
            planReturnJourneyBtn.Visible = false;
        }

        /// <summary>
        /// Toggle locations event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void journeyInputControl_OnToggleLocation(object sender, EventArgs e)
        {
            // Toggle locations selected, hide the current journey results
            journeyHeadingControl.Visible = false;
            outwardSummaryControl.Visible = false;
            planReturnJourneyBtn.Visible = false;
        }

        /// <summary>
        /// Event handler for plan journey button click event. 
        /// Initiates the toggle locations on the journey input control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void planReturnJourneyBtn_Click(object sender, EventArgs e)
        {
            journeyInputControl.ToggleLocations();
        }
        
        /// <summary>
        /// Replans the outward journey for the current request
        /// </summary>
        protected void ReplanOutwardJourneyEvent(object sender, ReplanJourneyEventArgs e)
        {
            if (e != null)
            {
                ReplanJourney(true, e.IsEarlier);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads resource strings for labels/controls
        /// </summary>
        private void SetupResources()
        {
            planReturnJourneyBtn.Text = string.Format("<span>{0}</span>", GetResourceMobile("JourneySummary.PlanReturnJourney.Text"));
            planReturnJourneyBtn.ToolTip = GetResourceMobile("JourneySummary.PlanReturnJourney.ToolTip");
            planReturnJourneyBtnNonJS.Text = GetResourceMobile("JourneySummary.PlanReturnJourney.Text");
            planReturnJourneyBtnNonJS.ToolTip = GetResourceMobile("JourneySummary.PlanReturnJourney.ToolTip");
        }

        /// <summary>
        /// Sets the visibility of the controls
        /// </summary>
        private void SetupControlVisibility()
        {
            SetupJourneyInputControlVisibility();

            SetupMapLink();

            SetupPlanReturnJourneyButton();
        }

        /// <summary>
        /// Sets up the visibility of the journey input control
        /// </summary>
        private void SetupJourneyInputControlVisibility()
        {
            if (!Properties.Current["JourneySummary.JourneyInputControl.Visible.Switch"].Parse(false))
            {
                journeyInputControl.Visible = false;
                journeyInputUpdate.Visible = false;
            }
        }

        /// <summary>
        /// Sets up the display of the map link, click logic is in master page
        /// </summary>
        private void SetupMapLink()
        {
            if (Properties.Current["Map.Summary.Enabled.Switch"].Parse(true))
            {
                ((TDPMobile)Master).DisplayNext = true;
            }
        }

        /// <summary>
        /// Sets up the plan return journey button
        /// </summary>
        private void SetupPlanReturnJourneyButton()
        {
            if (!Properties.Current["JourneySummary.PlanReturnJourney.Visible.Switch"].Parse(false))
            {
                planReturnJourneyBtn.Visible = false;
                planReturnJourneyBtnNonJS.Visible = false;
                divReturnJourney.Visible = false;
            }
        }

        /// <summary>
        /// Initialises the ajax timer control
        /// </summary>
        private void SetupTimer()
        {
            int refreshSecs = Properties.Current["JourneySummary.Wait.RefreshTime.Seconds"].Parse(5);

            waitTimer.Interval = refreshSecs * 1000; // In millisecs
        }

        /// <summary>
        /// Updates the count of page wait refresh
        /// </summary>
        private int UpdateWaitCount()
        {
            // Read count from hidden field
            int count = waitCount.Value.Parse(0);

            // Increment
            count = count + 1;

            // Persist in hidden field
            waitCount.Value = count.ToString();

            return count;
        }

        /// <summary>
        /// Binds journey result to outward and return detail summary result controls
        /// </summary>
        /// <param name="journeyRequest">ITDPJourneyRequest object</param>
        /// <param name="journeyResult">ITDPJourneyResult object</param>
        private void BindJourneyResult(ITDPJourneyRequest journeyRequest, ITDPJourneyResult journeyResult)
        {
            if (journeyResult != null)
            {
                if (journeyResult.OutwardJourneys != null)
                {
                    if (journeyResult.OutwardJourneys.Count > 0)
                    {
                        journeyHeadingControl.Initialise(journeyRequest, null, true, false);
                        journeyHeadingControl.Visible = true;

                        outwardSummaryControl.Initialise(journeyRequest, journeyResult);
                        outwardSummaryControl.Visible = true;
                    }
                }
            }
        }
        
        /// <summary>
        /// Returns the page id to redirect to for no journey results
        /// </summary>
        /// <param name="journeyCount"></param>
        /// <returns></returns>
        private Common.PageId GetNoResultsRedirectPage(int journeyCount)
        {
            if (Properties.Current["JourneySummary.JourneyInputControl.Visible.Switch"].Parse(false))
            {
                // If input control displayed then no need to redirect back to input
                return Common.PageId.Empty;
            }

            if (journeyCount == 0)
            {
                return Common.PageId.MobileInput;
            }
            
            return Common.PageId.Empty;
        }


        #region Submit/Replan methods

        /// <summary>
        /// Submits the journey request
        /// </summary>
        /// <param name="plannerMode"></param>
        private void SubmitRequest(TDPJourneyPlannerMode plannerMode)
        {
            List<TDPMessage> messages = new List<TDPMessage>();

            if (journeyInputAdapter.SubmitRequest(plannerMode, ref messages, (TDPPage)Page))
            {
                string transferUrl;

                switch (plannerMode)
                {
                    case TDPJourneyPlannerMode.PublicTransport:
                        // And redirect to the summary page
                        transferUrl = GetPlanJourneyTransferURL(PageId.MobileSummary);

                        // Do the redirect here.
                        // Can't use the TDPPage SetPageTransfer functionality as already on the Summary
                        // page, and would require complicating the code logic on the page transfer and to load 
                        // the required result
                        Response.Redirect(transferUrl);
                        break;

                    case TDPJourneyPlannerMode.Cycle:
                        // And redirect to the summary page
                        transferUrl = GetPlanJourneyTransferURL(PageId.MobileSummary);

                        // Do the redirect here.
                        // Can't use the TDPPage SetPageTransfer functionality as already on the Summary
                        // page, and would require complicating the code logic on the page transfer and to load 
                        // the required result
                        Response.Redirect(transferUrl);
                        break;
                }
            }
            else
            {
                // Show any submit validation error messages
                foreach (TDPMessage message in messages)
                {
                    DisplayMessage(message, Common.PageId.Empty);
                }
            }
        }

        /// <summary>
        /// Method to replan the journey (for earlier or later) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplanJourney(bool isOutward, bool isEarlier)
        {
            #region Build Replan request

            JourneyInputAdapter journeyInputAdapter = new JourneyInputAdapter();
            JourneyHelper journeyHelper = new JourneyHelper();
            SessionHelper sessionHelper = new SessionHelper();

            bool requestExists = false;

            // Get the request hash
            string jrh = journeyHelper.GetJourneyRequestHash();
            string jrhEarlier = string.Empty;
            string jrhLater = string.Empty;

            // Get the request and result to be replanned
            ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest(jrh);
            ITDPJourneyResult tdpJourneyResult = sessionHelper.GetTDPJourneyResult(jrh);

            // Check if an earlier or later has previously been done,
            // and if it has display that rather than submitting a request to the cjp
            jrhEarlier = tdpJourneyRequest.ReplanJourneyRequestHashEarlier;
            jrhLater = tdpJourneyRequest.ReplanJourneyRequestHashLater;

            requestExists = isEarlier ? !string.IsNullOrEmpty(jrhEarlier) : !string.IsNullOrEmpty(jrhLater);

            if (requestExists)
            {
                // This request has previously done an earlier/later.

                // Ensure the current requests is aware of the replan requests (if it exists) so 
                // for any further replans the chain of earlier/later journey requests persists, 
                // e.g.  earlier - earlier - current - later - later ... etc
                requestExists = journeyInputAdapter.ValidateAndUpdateTDPRequestEarlierLater(tdpJourneyRequest.JourneyRequestHash,
                    isEarlier, isEarlier ? jrhEarlier : jrhLater);

                if (requestExists)
                {
                    // Ensure session is set to the earlier/later journey request to be displayed on the page redirect
                    sessionHelper.UpdateSession(isEarlier ? jrhEarlier : jrhLater);
                }
            }

            if (!requestExists)
            {
                // Request earlier/later doesnt exist for this request, create one

                #region Set the replan values

                DateTime replanOutwardDateTime = DateTime.MinValue;
                bool replanOutwardArriveBefore = false;

                int earlierIntervalMins = Properties.Current["JourneyOptions.Replan.Earlier.Interval.Minutes"].Parse(1);
                int laterIntervalMins = Properties.Current["JourneyOptions.Replan.Later.Interval.Minutes"].Parse(1);

                bool retainPreviousJourneys = Properties.Current["JourneySummary.Replan.RetainPreviousJourneys.Switch"].Parse(false);
                bool retainPreviousJourneysWhenNoResults = Properties.Current["JourneySummary.Replan.RetainPreviousJourneysWhenNoResults.Switch"].Parse(false);

                // Only replan the selected journey direction (should only ever be Outward in TDPMobile)
                if (isOutward)
                {
                    DateTime dt = GetJourneyDateTime(tdpJourneyResult.OutwardJourneys, isEarlier);

                    // If Earlier, then date time is 1 minute before earliest "arrive time" in all the journeys
                    if (isEarlier)
                    {
                        replanOutwardDateTime = dt.Subtract(new TimeSpan(0, earlierIntervalMins, 0));
                        replanOutwardArriveBefore = true;
                    }
                    // If Later, then date time is 1 minute after latest "leave time" in all the journeys
                    else
                    {
                        replanOutwardDateTime = dt.Add(new TimeSpan(0, laterIntervalMins, 0));
                        replanOutwardArriveBefore = false;
                    }
                }

                #endregion

                // Create the replan request
                journeyInputAdapter.ValidateAndBuildTDPRequestForReplan(tdpJourneyRequest,
                    replanOutwardDateTime,
                    replanOutwardArriveBefore,
                    tdpJourneyResult.OutwardJourneys,
                    retainPreviousJourneys,
                    retainPreviousJourneysWhenNoResults // Ensure existing journeys are reshown if replan fails
                    );
            }

            #endregion

            #region Submit

            // Submit the request if needed to
            JourneyPlannerHelper journeyPlannerHelper = new JourneyPlannerHelper();

            if (requestExists ||
                journeyPlannerHelper.SubmitRequest(TDPJourneyPlannerMode.PublicTransport, true))
            {
                // And redirect to the summary page
                string transferUrl = GetPlanJourneyTransferURL(PageId.MobileSummary);

                // Do the redirect here.
                // Can't use the TDPPage SetPageTransfer functionality as already on the Summary
                // page, and would require complicating the code logic on the page transfer and to load 
                // the required result
                Response.Redirect(transferUrl);
            }

            #endregion
        }

        /// <summary>
        /// Returns the earliest/latest datetime for the journeys
        /// If isEarlier, then the Arrive times are searched and the earliest arrive time is returned
        /// Else, then the latest Leave times are searched and the latest leave time is returned
        /// </summary>
        /// <param name="journeys"></param>
        /// <param name="isEarlier"></param>
        /// <returns></returns>
        private DateTime GetJourneyDateTime(List<Journey> journeys, bool isEarlier)
        {
            DateTime dt = DateTime.MinValue;

            if (journeys != null)
            {
                if (isEarlier)
                {
                    journeys.Sort(JourneyComparer.SortJourneyArriveBy);

                    // Last journey should have the earliest arrive by datetime
                    dt = journeys[journeys.Count - 1].EndTime;
                }
                else
                {
                    journeys.Sort(JourneyComparer.SortJourneyLeaveAfter);

                    // Last journey should have the latest leave after datetime
                    dt = journeys[journeys.Count - 1].StartTime;
                }
            }

            return dt;
        }
        
        /// <summary>
        /// Method to return a reponse redirect transfer url for the supplied page id, 
        /// and which contains the journey request hash in session with landing page parameters for that journey request
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private string GetPlanJourneyTransferURL(PageId pageId)
        {
            SessionHelper sessionHelper = new SessionHelper();
            LandingPageHelper landingHelper = new LandingPageHelper();
            URLHelper urlHelper = new URLHelper();

            #region Query string

            // Have to duplicate the build querystring code (from TDPPage) as need to 
            // specify the selected journey request hash (and not the current one in the Query string)
            NameValueCollection redirectQueryString = new NameValueCollection();

            // Only add query string for results page
            redirectQueryString[QueryStringKey.JourneyRequestHash] = sessionHelper.GetTDPJourneyRequest().JourneyRequestHash;

            // Landing page querystring values
            Dictionary<string, string> dictLandingPageJO = landingHelper.BuildJourneyRequestForQueryString(
                    sessionHelper.GetTDPJourneyRequest());

            foreach (KeyValuePair<string, string> kvp in dictLandingPageJO)
            {
                redirectQueryString[kvp.Key] = kvp.Value;
            }

            #endregion

            // Get page transfer details
            PageTransferDetail transferDetail = GetPageTransferDetail(pageId);
            string transferUrl = transferDetail.PageUrl;

            // Append query string values set
            transferUrl = urlHelper.AddQueryStringParts(transferUrl, redirectQueryString);

            return transferUrl;
        }

        #endregion

        /// <summary>
        /// Displays a message
        /// </summary>
        private void DisplayMessage(TDPMessage tdpMessage, PageId redirectPageId )
        {
            ((TDPMobile)this.Master).DisplayMessage(tdpMessage, redirectPageId);
        }

        #endregion
    }
}
