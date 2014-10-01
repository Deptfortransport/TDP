// *********************************************** 
// NAME             : JourneyPageControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 14 Feb 2012
// DESCRIPTION  	: JourneyPage control to allow paging between the journeys
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.UserPortal.JourneyControl;
using TDP.Common.Extenders;
using TDP.Common.Web;
using TDP.Common.ResourceManager;
using System.Collections.Specialized;
using TDP.UserPortal.ScreenFlow;
using TDP.Common.LocationService;
using System.Web.UI.HtmlControls;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// JourneyPage control to allow paging between the journeys
    /// </summary>
    public partial class JourneyPageControl : System.Web.UI.UserControl
    {
        #region Public Events

        // Show journey event declaration
        public event OnShowJourney ShowJourneyHandler;
        
        #endregion

        #region Private Fields

        private ITDPJourneyRequest journeyRequest = null;
        private ITDPJourneyResult journeyResult = null;
        private Journey journey;

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetupControls();
            }
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// JourneyNav repeater data bound event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void JourneyNav_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.SelectedItem)
            {
                Journey j = e.Item.DataItem as Journey;

                Button btnJourneyNav = e.Item.FindControlRecursive<Button>("btnJourneyNav");
                HtmlGenericControl journeyNavDiv = e.Item.FindControlRecursive<HtmlGenericControl>("journeyNavDiv");

                TDPPageMobile page = (TDPPageMobile)Page;

                // Set argument for the journey to display when journey nav button clicked
                btnJourneyNav.CommandArgument = j.JourneyId.ToString();
                
                // Set display text, e.g. "Journey 1"
                btnJourneyNav.Text = string.Format("{0} {1}",
                    page.GetResourceMobile("JourneyDetail.JourneyPaging.Heading.Text"),
                    (e.Item.ItemIndex + 1));
                btnJourneyNav.ToolTip = string.Format("{0} {1}",
                    page.GetResourceMobile("JourneyDetail.JourneyPaging.Heading.ToolTip"),
                    (e.Item.ItemIndex + 1));

                // Apply selected style if required
                string style = journeyNavDiv.Attributes["class"];

                if (j.JourneyId == journey.JourneyId)
                {
                    if (!style.Contains("journeyNavSelected"))
                    {
                        style = string.Format("{0} journeyNavSelected", style.Trim());
                    }
                }
                else if (style.Contains("journeyNavSelected"))
                {
                    style = style.Replace("journeyNavSelected", string.Empty);
                }

                // Apply last item style
                if (e.Item.ItemIndex >= journeyResult.OutwardJourneys.Count - 1)
                {
                    style = string.Format("{0} journeyNavLast", style.Trim());
                }

                journeyNavDiv.Attributes["class"] = style;
            }
        }

        /// <summary>
        /// Handler for the previous and next journey button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnJourneyNav_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                int journeyId = ((Button)sender).CommandArgument.Parse(1);

                // Raise event to tell subscribers to that show journey button has been selected
                if (ShowJourneyHandler != null)
                {
                    ShowJourneyHandler(sender, new JourneyEventArgs(journeyId));
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(ITDPJourneyRequest journeyRequest, ITDPJourneyResult journeyResult, Journey journey)
        {
            this.journeyRequest = journeyRequest;
            this.journeyResult = journeyResult;
            this.journey = journey;
        }

        /// <summary>
        /// Refresh method to re-populate the controls, e.g. if initialise called after Page_Load
        /// </summary>
        public void Refresh()
        {
            SetupControls();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method which setup the controls
        /// </summary>
        private void SetupControls()
        {
            if (journeyRequest != null && journeyResult != null && journey != null)
            {
                if (journeyResult.OutwardJourneys.Count <= 1)
                {
                    // Only 1 journey in the result, no need to do paging
                    journeyNavContainer.Visible = false;
                }
                else
                {
                    // Determine which journey (index) is being displayed and set the nav buttons accordingly
                    int journeyId = journey.JourneyId;

                    List<Journey> journeys = journeyResult.OutwardJourneys;

                    // Sort the journeys to be same as displayed on the summary page:
                    // (If the sort is changed here, ensure DetailSummaryControl.ascx sort is updated as well)
                    if (journeyRequest.Destination is TDPVenueLocation)
                    {
                        journeys.Sort(JourneyComparer.SortJourneyArriveBy);
                    }
                    else if (journeyRequest.Origin is TDPVenueLocation)
                    {
                        journeys.Sort(JourneyComparer.SortJourneyLeaveAfter);
                    }
                    else
                    {
                        // Otherwise use the arrive by time sort
                        if (journeyRequest.OutwardArriveBefore)
                        {
                            journeys.Sort(JourneyComparer.SortJourneyArriveBy);
                        }
                        else
                        {
                            journeys.Sort(JourneyComparer.SortJourneyLeaveAfter);
                        }
                    }

                    rptJourneyNav.DataSource = journeys;
                    rptJourneyNav.DataBind();
                }
            }
        }

        #endregion
    }
}