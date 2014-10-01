﻿// *********************************************** 
// NAME             : BlueBadgeOptionsTab.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 31 Mar 2011
// DESCRIPTION  	: User control to contain blue badge journey options tab 
// ************************************************
                
                
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TDP.Common.LocationService;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;

namespace TDP.UserPortal.TDPWeb.Controls
{
    /// <summary>
    /// User control to contain blue badge journey options tab 
    /// </summary>
    public partial class BlueBadgeOptionsTab : System.Web.UI.UserControl, IJourneyOptionsTab
    {
        #region Private Fields
        private bool disabled = false;
        private TDPLocation venue = null;
        private DateTime outwardDateTime;
        private DateTime returnDateTime;
        #endregion

        #region Events
        public event PlanJourney OnPlanJourney;
        
        #endregion

        #region Public Properties
        /// <summary>
        /// Read only property determining the planner mode represented by journey obtions tab
        /// </summary>
        public JourneyControl.TDPJourneyPlannerMode PlannerMode
        {
            get { return JourneyControl.TDPJourneyPlannerMode.BlueBadge; }
        }

        /// <summary>
        /// Read/Write property if the tab is disabled
        /// </summary>
        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }

        /// <summary>
        /// Read/Write. venue location
        /// </summary>
        public TDPLocation Venue
        {
            get { return venue; }
            set { venue = value; }
        }

        /// <summary>
        /// Read/Write outward date time selected for the journey
        /// </summary>
        public DateTime OutwardDateTime
        {
            get { return outwardDateTime; }
            set { outwardDateTime = value; }
        }

        /// <summary>
        /// Read/Write. Return date time selected for the journey
        /// </summary>
        public DateTime ReturnDateTime
        {
            get { return returnDateTime; }
            set { returnDateTime = value; }
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender
        /// <summary>
        /// Page_PreRender 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();
            SetupUpdateProgressPanel();
        }

        #endregion

        #region Control Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PlanBlueBadge(object sender, EventArgs e)
        {
            if (OnPlanJourney != null)
            {
                OnPlanJourney(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Loads resource strings for labels/controls
        /// </summary>
        private void SetupResources()
        {
            TDPPage page = (TDPPage)Page;
            if (disabled)
            {
                blueBadgeOptions.ImageUrl = page.ImagePath + page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.Disabled.ImageUrl");
                blueBadgeOptions.AlternateText = page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.Disabled.AlternateText");
                blueBadgeOptions.ToolTip = page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.Disabled.ToolTip");
            }
            else
            {
                blueBadgeOptions.ImageUrl = page.ImagePath +  page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.ImageUrl");
                blueBadgeOptions.AlternateText = page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.AlternateText");
                blueBadgeOptions.ToolTip = page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.ToolTip");
            }

            planBlueBadge.Text = Server.HtmlDecode(page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.PlanBlueBadge.Text"));
            planBlueBadge.ToolTip = Server.HtmlDecode(page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.PlanBlueBadge.ToolTip"));

            planBlueBadge.Visible = true;
            planBlueBadge.Enabled = !disabled && (venue != null);

            string URL_OpenInNewWindowImage = ResolveClientUrl(page.ImagePath + page.GetResource("OpenInNewWindow.Blue.URL"));
            string ALTTXT_OpenInNewWindow = page.GetResource("OpenInNewWindow.AlternateText");
            string TOOLTIP_OpenInNewWindow = page.GetResource("OpenInNewWindow.Text");

            string imgOpenInNewWindow = string.Format("<img src=\"{0}\" alt=\"{1}\" title=\"{2}\" />",
                URL_OpenInNewWindowImage, ALTTXT_OpenInNewWindow, TOOLTIP_OpenInNewWindow);

            if (disabled)
            {
                venueContent.InnerHtml = string.Format(
                    page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.Disable.Information"),
                    imgOpenInNewWindow);

                // Fully hide the submit button if the functionality is set to disabled
                planBlueBadge.Visible = false;
            }
            else if (venue != null)
            {
               
                LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

                // Get car parks for the venue and its parent
                List<string> naptans = new List<string>(venue.Naptan);
                if (!string.IsNullOrEmpty(venue.Parent))
                {
                    naptans.Add(venue.Parent);
                }
                List<TDPVenueCarPark> carParkList = locationService.GetTDPVenueBlueBadgeCarParks(naptans);

                if (!disabled)
                {
                    if ((carParkList != null) && (carParkList.Count > 0))
                    {
                        planBlueBadge.Enabled = true;
                        venueContent.InnerHtml = string.Format(
                            page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.Available.Information"),
                            venue.DisplayName,
                            imgOpenInNewWindow);
                    }
                    else
                    {
                        planBlueBadge.Enabled = false;
                        venueContent.InnerHtml =string.Format(
                            page.GetResource("JourneyOptionTabContainer.BlueBadgeOptions.NotAvailable.Information"),
                            venue.DisplayName,
                            imgOpenInNewWindow);

                    }
                }
            }
            else if (!disabled)
            {
                venueContent.InnerHtml = page.GetResource("JourneyOptionTabContainer.SelectVenue.Information");
            }
                        
            #region Set enabled/disabled style

            if (!planBlueBadge.Enabled)
            {
                if (!planBlueBadge.CssClass.Contains("btnDisabled"))
                {
                    planBlueBadge.CssClass = planBlueBadge.CssClass + " btnDisabled";
                }
            }
            else
            {
                if (planBlueBadge.CssClass.Contains("btnDisabled"))
                {
                    planBlueBadge.CssClass = planBlueBadge.CssClass.Replace(" btnDisabled", string.Empty);
                }
            }

            #endregion
        }

        /// <summary>
        /// Sets up update panel's progress panel
        /// </summary>
        private void SetupUpdateProgressPanel()
        {
            TDPPage page = (TDPPage)Page;
            Image loading = updateProgress.FindControlRecursive<Image>("loading");
            Label loadingMessage = updateProgress.FindControlRecursive<Label>("loadingMessage");

            if (loading != null)
            {
                loading.ImageUrl = page.ImagePath + page.GetResource("JourneyOptionTabContainer.Loading.Imageurl");
                loading.AlternateText = Server.HtmlDecode(page.GetResource("JourneyOptionTabContainer.Loading.AlternateText"));
            }

            if (loadingMessage != null)
            {
                loadingMessage.Text = Server.HtmlDecode(page.GetResource("JourneyOptionTabContainer.loadingMessage.Text"));
            }
        }

        #endregion
    }
}
