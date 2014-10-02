// *********************************************** 
// NAME                 : CycleJourneyDetailsTableControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 15/06/2008
// DESCRIPTION          : Control to display the details of a cycle journey in a table format
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CycleJourneyDetailsTableControl.ascx.cs-arc  $ 
//
//   Rev 1.10   Nov 08 2010 08:48:24   apatel
//Updated to remove CJP additional information for Trunk Exchange Point and interchange time
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.9   Oct 26 2010 14:30:32   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.8   Mar 29 2010 11:53:58   mmodi
//Added an anchor link to allow Tabbing to the div in IE
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.7   Dec 10 2009 11:06:50   mmodi
//Updated to display zoom to cycle map direction number links in table
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Nov 20 2009 09:26:10   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Oct 20 2008 14:23:10   mmodi
//Pass in CJP user flag
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Oct 15 2008 17:09:52   mmodi
//Added null check to fix problem for Car journey maps
//
//   Rev 1.3   Oct 10 2008 15:37:06   mmodi
//Updated for cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:27:10   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 06 2008 14:50:52   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 13:26:52   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class CycleJourneyDetailsTableControl : TDUserControl
    {
        #region Private members

        private bool printable;
        private bool outward = false;
        private bool javaScriptSupported = false;

        private TDJourneyViewState viewState;

        // journey to display
        private CycleJourney cycleJourney = null;

        // currently selected roadunits
        private RoadUnitsEnum roadUnits;

        // Array of column titles for details table
        private IList headerDetail;

        // Indexs used to display a subset of the instructions
        private int directionStartIndex = -1;
        private int directionEndIndex = -1;

        // Variables needed to add javascript to the direction link click
        private bool addMapJavascript = false;
        private string mapId = "map";
        private string mapJourneyDisplayDetailsDropDownId = "mapdropdown";
        private string scrollToControlId = "mapControl";
        private string sessionId = "session";
        private string journeyType = "Cycle";
        private MapHelper mapHelper = new MapHelper();

        #endregion

        #region Initialise
        /// <summary>
        /// Initialises this control with a specific cycle journey
        /// </summary>
        public void Initialise(CycleJourney cycleJourney, bool outward, TDJourneyViewState viewState, TDJourneyParametersMulti journeyParameters)
        {
            this.outward = outward;
            this.cycleJourney = cycleJourney;
            this.viewState = viewState;
                        
        }

        /// <summary>
        /// Initialises this control with a specific cycle journey
        /// </summary>
        public void Initialise(CycleJourney cycleJourney, bool outward, TDJourneyViewState viewState, int directionStartIndex, int directionEndIndex, TDJourneyParametersMulti journeyParameters)
        {
            this.outward = outward;
            this.cycleJourney = cycleJourney;
            this.viewState = viewState;
            this.directionStartIndex = directionStartIndex;
            this.directionEndIndex = directionEndIndex;
            
        }

        /// <summary>
        /// Method which sets the values needed to add map javascript to the direction links
        /// </summary>
        public void SetMapProperties(bool addMapJavascript, string mapId, string mapJourneyDisplayDetailsDropDownId,
            string scrollToControlId, string sessionId)
        {
            this.addMapJavascript = addMapJavascript;
            this.mapId = mapId;
            this.mapJourneyDisplayDetailsDropDownId = mapJourneyDisplayDetailsDropDownId;
            this.scrollToControlId = scrollToControlId;
            this.sessionId = sessionId;

        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender
        /// <summary>
        /// Page_Init
        /// </summary>
        protected void Page_Init(object sender, EventArgs e)
        {
            cycleDetailsRepeater.ItemDataBound += new RepeaterItemEventHandler(cycleDetailsRepeater_ItemDataBound);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Only want to do this if not on the nonprintable page.
            if (!printable)
            {
                EnableScriptableObjects();
            }

            AlignServerWithClient();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            LoadResources();

            // Load the cycle journey details in to the display table
            DisplayData();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to load text for all static labels 
        /// </summary>
        private void LoadResources()
        {
            labelCycleJourneyDetailsTableControlTitle.Text = GetResource("CyclePlanner.CycleJourneyDetailsTableControl.labelCycleJourneyDetailsTableControlTitle.Text");
            
            // Don't show the title div when printing
            divCycleJourneyDetailsTableTile.Visible = !printable;

            buttonShowMore.Value = GetResource("CyclePlanner.CycleJourneyDetailsTableControl.buttonShowMore");
            buttonShowLess.Value = GetResource("CyclePlanner.CycleJourneyDetailsTableControl.buttonShowLess");

            // Setup the name to allow a page skiplink to navigate to
            cycleDetailsTableAnchor.Name = "cycleDetailTable" + (outward ? "Outward" : "Return");
        }

        /// <summary>
        /// Method which displays the Cycle journey details in the table.
        /// Calls the CycleJourneyDetailFormatter and binds this to the data table
        /// </summary>
        private void DisplayData()
        {
            if (cycleJourney != null)
            {
                FindCyclePageState findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;

                bool showAllDetails = (outward) ? findCyclePageState.ShowAllDetailsOutward : findCyclePageState.ShowAllDetailsReturn;

                CycleJourneyDetailFormatter detailFormatter = new DefaultCycleJourneyDetailFormatter(
                        cycleJourney,
                        viewState,
                        outward,
                        roadUnits,
                        printable,
                        showAllDetails,
                        IsCJPUser());

                headerDetail = new ArrayList(detailFormatter.GetDetailHeadings());

                // check if we need to pass in a start and end index for the journey directions
                if ((directionStartIndex >= 0) && (directionEndIndex >= 0))
                {
                    cycleDetailsRepeater.DataSource = detailFormatter.GetJourneyDetails(directionStartIndex, directionEndIndex);
                }
                else
                {
                    cycleDetailsRepeater.DataSource = detailFormatter.GetJourneyDetails();
                }

                // Ensures controls on ascx are evaluated, i.e. the <# #>,
                // including the cycleDetailsRepeater
                this.DataBind();
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

        ///<summary>
        /// The EnableClientScript property of a scriptable control is set so that they
        /// output an action attribute when appropriate.
        /// If JavaScript is enabled then appropriate script blocks are added to the page.
        ///</summary>
        protected void EnableScriptableObjects()
        {
            javaScriptSupported = bool.Parse((string)Session[((TDPage)Page).Javascript_Support]);
            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            if (javaScriptSupported)
            {
                buttonShowMore.Visible = true;
                buttonShowLess.Visible = true;
                                
                ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                Page.ClientScript.RegisterStartupScript(typeof(CycleJourneyDetailsTableControl), "DetailsSwitchCycle", scriptRepository.GetScript("DetailsSwitchCycle", javaScriptDom));

                string PageName = this.PageId.ToString();

                buttonShowMore.Attributes.Add( "onclick" ,"ShowDetails('" + GetHiddenInputId + "', this)");
                buttonShowLess.Attributes.Add("onclick", "ShowDetails('" + GetHiddenInputId + "', this)");
            }
            else
            {
                buttonShowMore.Visible = false;
                buttonShowLess.Visible = false;
                divShowMore.Visible = false;
                divShowLess.Visible = false;

                if (TDSessionManager.Current.FindPageState is FindCyclePageState)
                {
                    // Ensure all details are shown
                    FindCyclePageState findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;
                    findCyclePageState.ShowAllDetailsOutward = true;
                    findCyclePageState.ShowAllDetailsReturn = true;
                }
            }
        }

        ///<summary>
        /// The client may have changed things through JavaScript so need to update server state.  
        /// Used to set the show/hide details for cycle journeys.
        ///</summary>
        private void AlignServerWithClient()
        {
            FindCyclePageState findCyclePageState = TDSessionManager.Current.FindPageState as FindCyclePageState;

            if (findCyclePageState != null)
            {
                string hiddenUnitsField = GetHiddenInputId;

                bool showDetails = false;

                // Get the value
                if (Request.Params[hiddenUnitsField] != null)
                {
                    showDetails = (Request.Params[hiddenUnitsField].ToLower() == "show");
                }
                else
                {
                    // Assume this is first time page is loaded so get the value from session
                    if (outward)
                        showDetails = findCyclePageState.ShowAllDetailsOutward;
                    else
                        showDetails = findCyclePageState.ShowAllDetailsReturn;
                }

                // Save back to session
                if (outward)
                    findCyclePageState.ShowAllDetailsOutward = showDetails;
                else
                    findCyclePageState.ShowAllDetailsReturn = showDetails;
            }
        }

        #endregion

        #region Event handlers

        protected void cycleDetailsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                string cycleRoute = (string)((object[])e.Item.DataItem)[3];
                string cycleInstruction = (string)((object[])e.Item.DataItem)[5];
                string cycleAttributeDetail = (string)((object[])e.Item.DataItem)[6];

                int rowspanCount = 1;

                // Set the visibility of the rows
                HtmlTableRow rowCycleRoute = (HtmlTableRow)e.Item.FindControl("rowCycleRoute");
                if (rowCycleRoute != null)
                {
                    if (!string.IsNullOrEmpty(cycleRoute))
                    {
                        rowCycleRoute.Visible = true;
                        rowspanCount++;
                    }
                }

                HtmlTableRow rowCycleInstruction = (HtmlTableRow)e.Item.FindControl("rowCycleInstruction");
                if (rowCycleInstruction != null)
                {
                    if (!string.IsNullOrEmpty(cycleInstruction))
                    {
                        rowCycleInstruction.Visible = true;
                        rowspanCount++;
                    }
                }

                HtmlTableRow rowCycleAttribute = (HtmlTableRow)e.Item.FindControl("rowCycleAttribute");
                if (rowCycleAttribute != null)
                {
                    if (!string.IsNullOrEmpty(cycleAttributeDetail))
                    {
                        rowCycleAttribute.Visible = true;
                        rowspanCount++;
                    }
                }

                // Update the appropriate cell rowspans accordingly
                if (rowspanCount > 1) // At least one of the above rows is visible
                {
                    HtmlTableCell cell = null; 
                    
                    cell = (HtmlTableCell)e.Item.FindControl("cellStepNumber");
                    if (cell != null)
                        cell.RowSpan = rowspanCount;

                    cell = (HtmlTableCell)e.Item.FindControl("cellDistance");
                    if (cell != null)
                        cell.RowSpan = rowspanCount;

                    cell = (HtmlTableCell)e.Item.FindControl("cellTime");
                    if (cell != null)
                        cell.RowSpan = rowspanCount;
                }

            }

        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. Printable mode of control
        /// </summary>
        public bool Printable
        {
            get { return printable; }
            set { printable = value; }
        }

        /// <summary>
        /// Read/write. Road units to display the instructions in
        /// </summary>
        public RoadUnitsEnum RoadUnits
        {
            get { return roadUnits; }
            set { roadUnits = value; }
        }

        /// <summary>
        /// Array of column titles for details table
        /// </summary>
        public IList HeaderDetail
        {
            get
            {
                return headerDetail;
            }
        }

        #endregion

        #region Public properties used by control
        /// <summary>
        /// Returns the id of the hidden control
        /// </summary>
        public string GetHiddenInputId
        {
            get
            {
                return this.ClientID + "_hdnShowDetailsState";
            }
        }

        /// <summary>
        /// Returns the current show details value in the session
        /// </summary>
        public string ShowDetailsState
        {
            get
            {
                FindCyclePageState findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;

                string show = "show";
                string hide = "hide";

                if (outward)
                    return (findCyclePageState.ShowAllDetailsOutward) ? show : hide;
                else
                    return (findCyclePageState.ShowAllDetailsReturn) ? show : hide;
            }
        }

        /// <summary>
        /// Returns css class for the ShowMore div
        /// </summary>
        public string GetCSSClassShowMore
        {
            get
            {
                if (javaScriptSupported)
                {
                    FindCyclePageState findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;

                    if (outward)
                        return (findCyclePageState.ShowAllDetailsOutward) ? GetClassHide : GetClassShow;
                    else
                        return (findCyclePageState.ShowAllDetailsReturn) ? GetClassHide : GetClassShow;
                }
                else
                {
                    return GetClassHide;
                }
            }
        }

        /// <summary>
        /// Returns css class for the ShowLess div
        /// </summary>
        public string GetCSSClassShowLess
        {
            get
            {
                if (javaScriptSupported)
                {
                    FindCyclePageState findCyclePageState = (FindCyclePageState)TDSessionManager.Current.FindPageState;

                    if (outward)
                        return (findCyclePageState.ShowAllDetailsOutward) ? GetClassShow : GetClassHide;
                    else
                        return (findCyclePageState.ShowAllDetailsReturn) ? GetClassShow : GetClassHide;
                }
                else
                {
                    return GetClassHide;
                }
            }
        }

        /// <summary>
        /// Returns the css class for Show
        /// </summary>
        public string GetClassShow
        {
            get
            {
                return outward ? "cycleJourneyDetailsAttributeShowOut nopadding" : "cycleJourneyDetailsAttributeShowRet nopadding";
            }
        }

        /// <summary>
        /// Returns the css class for Hide
        /// </summary>
        public string GetClassHide
        {
            get
            {
                return outward ? "cycleJourneyDetailsAttributeHideOut nopadding" : "cycleJourneyDetailsAttributeHideRet nopadding";
            }
        }

        /// <summary>
        /// Read Only.
        /// Determines if the cjp info summary div should be visible
        /// </summary>
        public bool IsCJPInfoSummaryAvailable
        {
            get
            {
                return CJPUserInfoHelper.IsCJPInformationAvailableForType(CJPInfoType.TrunkExchangePoint);
            }
        }

        #endregion

        #region Maps

        /// <summary>
        /// Generates and return a script to associate with a link to show journey direction on map
        /// </summary>
        /// <param name="row">data row object</param>
        /// <returns>javascript string</returns>
        public string GetShowOnMapScript(string directionNumber)
        {
            string linkScript = string.Empty;

            if (IsRowMapLinkVisible && !string.IsNullOrEmpty(mapId)
                && (cycleJourney != null))
            {
                // Ensure map area is visible
                StringBuilder showOnMapScript = new StringBuilder(
                    string.Format("scrollToElement('{0}');", scrollToControlId));

                // The script function called sets the selected index in the journey map directions drop down,
                // and calls the same function to zoom map as the drop down
                showOnMapScript.AppendFormat("zoomCycleJourneyDetailMap('{0}','{1}','{2}','{3}','{4}',{5},'{6}');",
                    mapId,
                    sessionId,
                    cycleJourney.RouteNum,
                    journeyType,
                    mapJourneyDisplayDetailsDropDownId,
                    directionNumber,
                    scrollToControlId);

                showOnMapScript.Append(" return false;");

                linkScript = showOnMapScript.ToString();
            }

            return linkScript;
        }

        /// <summary>
        /// Read/Write property to determine if the row index should show as link to zoom to the Map
        /// </summary>
        public bool IsRowMapLinkVisible
        {
            get
            {
                return addMapJavascript && javaScriptSupported && !printable;
            }
            set
            {
                addMapJavascript = value;
            }
        }

        #endregion
    }
}