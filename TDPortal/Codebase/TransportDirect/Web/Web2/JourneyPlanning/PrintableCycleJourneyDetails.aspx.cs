// *********************************************** 
// NAME                 : PrintableCycleJourneyDetails.aspx.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/08/2008
// DESCRIPTION			: The printable cycle journey details.
//                      : This page currently only shows CycleJourneys
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableCycleJourneyDetails.aspx.cs-arc  $
//
//   Rev 1.8   Feb 02 2010 16:37:58   pghumra
//Fixed multiple page refresh issues for printer friendly maps.
//Resolution for 5395: CODE FIX - INITIAL - DEL 10.x - Del 10.9.1 Bug printer friendly
//
//   Rev 1.7   Jan 18 2010 12:43:22   mmodi
//Add an auto refresh page if map image url not detected in session
//Resolution for 5375: Maps - Printer friendly map page refresh change
//
//   Rev 1.6   Dec 10 2009 12:34:12   mmodi
//Added hidden units state field needed by Gradient profile chart to render
//
//   Rev 1.5   Jan 19 2009 11:18:32   mmodi
//Updated to use new Cycle helper, passing in false to not log a gradient profile event
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//
//   Rev 1.4   Jan 16 2009 09:54:38   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Oct 10 2008 15:55:12   mmodi
//Updated for cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Sep 25 2008 11:29:10   mmodi
//Updated for table version of gradient profile
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Sep 17 2008 16:24:58   mmodi
//Updated for printer friendly chart 
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Aug 22 2008 10:51:50   mmodi
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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;

namespace TransportDirect.UserPortal.Web.Templates
{
    public partial class PrintableCycleJourneyDetails : TDPrintablePage, INewWindowPage
    {
        #region Private members

        private ITDSessionManager sessionManager;
        private TDItineraryManager itineraryManager;
        private TDJourneyViewState viewState;
        private ITDCyclePlannerResult cycleResult;

        #region State of results
        /// <summary>
        ///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
        /// </summary>
        private bool outwardExists = false;

        /// <summary>
        ///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
        /// </summary>
        private bool returnExists = false;

        /// <summary>
        /// True if the Itinerary exists, containing the Initial journey and zero or more extensions
        /// </summary>
        private bool itineraryExists = false;

        /// <summary>
        /// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
        /// </summary>
        private bool extendInProgress = false;

        /// <summary>
        /// True if the Itinerary exists and there are no extensions in the process of being planned
        /// </summary>
        private bool showItinerary = false;

        /// <summary>
        /// True if the results have been planned using FindA
        /// </summary>
        private bool showFindA = false;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor - sets the Page Id.
        /// </summary>
        public PrintableCycleJourneyDetails()
        {
            pageId = PageId.PrintableCycleJourneyDetails;
        }

        #endregion

        #region Page_Load

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // check if the User Survey form should be displayed
            ShowSurvey();

            // load session items
            sessionManager = TDSessionManager.Current;
            itineraryManager = TDItineraryManager.Current;
            viewState = sessionManager.JourneyViewState;
            cycleResult = sessionManager.CycleResult;

            DetermineStateOfResults();

            SetupUnitsToDisplay();

            SetupDetailsToDisplay();

            PopulateFooterControls();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            PopulateControls();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Displays a user survey
        /// </summary>
        private void ShowSurvey()
        {
            //check if user survey should be displayed			
            bool showSurvey = UserSurveyHelper.ShowUserSurvey();

            //if user survey should be displayed...
            if (showSurvey)
            {
                //check if JavaScript is supported by the browser
                if (bool.Parse((string)Session[((TDPage)Page).Javascript_Support]))
                {
                    //add javascript block to this page that will open a user survey window when this page closes
                    string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];
                    ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
                    Page.ClientScript.RegisterClientScriptBlock(typeof(PrintableJourneyMapTile), "UserSurvey", scriptRepository.GetScript("UserSurvey", javaScriptDom));
                }
            }
        }

        /// <summary>
        /// Establish what mode the Itinerary Manager is in and whether we have any Return results
        /// </summary>
        private void DetermineStateOfResults()
        {
            itineraryExists = (itineraryManager.Length > 0);
            extendInProgress = itineraryManager.ExtendInProgress;
            showItinerary = (itineraryExists && !extendInProgress);
            showFindA = (!showItinerary && (sessionManager.IsFindAMode));

            if (showItinerary)
            {
                outwardExists = (itineraryManager.OutwardLength > 0);
                returnExists = (itineraryManager.ReturnLength > 0);
            }
            else
            {
                //check for cycle result
                PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(sessionManager);
                outwardExists = plannerOutputAdapter.CycleExists(true);
                returnExists = plannerOutputAdapter.CycleExists(false);

                //check for normal result
                ITDJourneyResult result = sessionManager.JourneyResult;
                if (result != null)
                {
                    outwardExists = (((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid) || outwardExists;
                    returnExists = (((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid) || returnExists;
                }
            }
        }

        /// <summary>
        /// Checks querystring to determine which distance units to use
        /// </summary>
        private void SetupUnitsToDisplay()
        {
            string urlQueryString = string.Empty;

            // The Query params is set using javascript on the non-printable page
            urlQueryString = Request.Params["units"];

            // If there is no query string, take the value from the session
            if (string.IsNullOrEmpty(urlQueryString))
            {
                mapTileOutward.RoadUnits = sessionManager.InputPageState.Units;
                mapTileReturn.RoadUnits = sessionManager.InputPageState.Units;
            }
            else if (urlQueryString == "kms")
            {
                mapTileOutward.RoadUnits = RoadUnitsEnum.Kms;
                mapTileReturn.RoadUnits = RoadUnitsEnum.Kms;
            }
            else // default
            {
                mapTileOutward.RoadUnits = RoadUnitsEnum.Miles;
                mapTileReturn.RoadUnits = RoadUnitsEnum.Miles;
            }

            // Add a hidden field to page, this is used by the Gradient chart to determine which units to 
            // use on the graph.
            hdnUnitsState.Value = mapTileOutward.RoadUnits.ToString();
            sessionManager.InputPageState.Units = mapTileOutward.RoadUnits;
        }

        /// <summary>
        /// Checks query string to determine if all journey details should be displayed
        /// </summary>
        private void SetupDetailsToDisplay()
        {
            FindCyclePageState findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;

            string queryStringValue = Request.Params["details"];

            if (string.IsNullOrEmpty(queryStringValue))
            {
                findCyclePageState.ShowAllDetailsOutward = true;
                findCyclePageState.ShowAllDetailsReturn = true;
            }
            else if (queryStringValue == "hide")
            {
                findCyclePageState.ShowAllDetailsOutward = false;
                findCyclePageState.ShowAllDetailsReturn = false;
            }
            else // default
            {
                findCyclePageState.ShowAllDetailsOutward = true;
                findCyclePageState.ShowAllDetailsReturn = true;
            }
        }


        /// <summary>
        /// Populates the controls on the page with the journey results
        /// </summary>
        private void PopulateControls()
        {
            MapHelper mapHelper = new MapHelper();

            // Map is visible only if results exist.
            if (outwardExists)
            {
                panelMapOutward.Visible = true;

                // Show Directions, and only show Maps with Directions below it if user was looking at Maps
                mapTileOutward.ShowDirectionsEntireJourney = true;
                mapTileOutward.ShowDirectionsTile = viewState.OutwardShowMap;
                mapTileOutward.ShowMapEntireJourney = false; // Do not show Map of entire journey
                mapTileOutward.ShowMapTile = viewState.OutwardShowMap; ;
                
                mapTileOutward.Populate(
                    true,
                    mapHelper.FindRelevantJourney(true),
                    sessionManager,
                    itineraryManager,
                    viewState);

                cycleJourneyGraphControlOutward.Initialise((CycleJourney)mapHelper.FindRelevantJourney(true), true);
                cycleJourneyGraphControlOutward.Printable = true;
            }

            if (returnExists)
            {
                literalNewPage.Visible = true; // Ensure a page break is forced between outward/return

                panelMapReturn.Visible = true;

                // Show Directions, and only show Maps with Directions below it if user was looking at Maps
                mapTileReturn.ShowDirectionsEntireJourney = true;
                mapTileReturn.ShowDirectionsTile = viewState.ReturnShowMap;
                mapTileReturn.ShowMapEntireJourney = false; // Do not show Map of entire journey
                mapTileReturn.ShowMapTile = viewState.ReturnShowMap;

                mapTileReturn.Populate(false,
                    mapHelper.FindRelevantJourney(false),
                    sessionManager,
                    itineraryManager,
                    viewState);

                cycleJourneyGraphControlReturn.Initialise((CycleJourney)mapHelper.FindRelevantJourney(false), false);
                cycleJourneyGraphControlReturn.Printable = true;
            }
        }

        /// <summary>
        /// Populates the labels shown at the bottom of the printable page
        /// </summary>
        private void PopulateFooterControls()
        {
            labelDateTimeTitle.Text = GetResource("PrintableJourneyMap.labelDateTimeTitle");
            labelDateTime.Text = TDDateTime.Now.ToString("G");

            if (sessionManager.Authenticated)
            {
                labelUsername.Text = sessionManager.CurrentUser.Username;
                labelUsername.Visible = true;

                labelUsernameTitle.Text = GetResource("PrintableJourneyMap.labelUsernameTitle");
                labelUsernameTitle.Visible = true;
            }

            if (cycleResult != null)
            {
                labelReference.Text = cycleResult.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
                labelReference.Visible = true;

                labelReferenceTitle.Text = GetResource("PrintableJourneyMap.labelReferenceTitle");
                labelReferenceTitle.Visible = true;
            }
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

            CyclePlannerHelper cycleHelper = new CyclePlannerHelper(sessionManager, false);

            return cycleHelper.GetChartData(outward);
        }

        #endregion
    }
}
