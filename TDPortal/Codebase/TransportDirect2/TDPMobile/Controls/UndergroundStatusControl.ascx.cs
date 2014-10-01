// *********************************************** 
// NAME             : UndergroundStatusControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 08 Mar 2012
// DESCRIPTION  	: Control to display Underground status items
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.UserPortal.UndergroundNews;
using TDP.Common.Extenders;
using TDP.Common.Web;
using TDP.Common.ServiceDiscovery;
using System.Web.UI.HtmlControls;
using TDP.Common.ResourceManager;
using TDP.Common;
using TDP.Common.PropertyManager;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// Control to display Underground status items
    /// </summary>
    public partial class UndergroundStatusControl : System.Web.UI.UserControl
    {
        #region Private members

        // Resource manager
        private TDPResourceManager RM = Global.TDPResourceManager;

        private bool show = true;
        private bool forceUnavailable = false;

        private const string dateTimeFormat = "dd/MM/yyyy HH:mm";

        private DateTime expiredDateTime = DateTime.MinValue;

        #endregion

        #region Page_Load

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            BindUndergroundStatusData();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(bool show, bool forceUnavailable)
        {
            this.show = show;
            this.forceUnavailable = forceUnavailable;
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Underground status repeater ItemDataBound event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void undergroundStatusRptr_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.SelectedItem)
            {
                UndergroundStatusItem usi = e.Item.DataItem as UndergroundStatusItem;

                // Build an id based on name to allow javascript to search on id,
                string usiId = string.Format("{0}",
                     usi.LineName.Replace(" ", "_").ToLower());

                #region Summary view

                HiddenField statusLineId = e.Item.FindControlRecursive<HiddenField>("statusLineId");
                statusLineId.Value = usiId;

                System.Web.UI.WebControls.LinkButton showDetailsBtn = e.Item.FindControlRecursive<System.Web.UI.WebControls.LinkButton>("showDetailsBtn");
                HtmlGenericControl statusColorDiv = e.Item.FindControlRecursive<HtmlGenericControl>("statusColorDiv");
                Label statusLineLbl = e.Item.FindControlRecursive<Label>("statusLineLbl");
                Label statusDescriptionLbl = e.Item.FindControlRecursive<Label>("statusDescriptionLbl");

                // Set the line colour div
                string undergroundLineClass = usi.LineName.ToLower().Replace("&", "and").Replace(" ", "");
                if (!statusColorDiv.Attributes["class"].Contains(undergroundLineClass))
                {
                    statusColorDiv.Attributes["class"] += string.Format(" " + undergroundLineClass);
                }

                // Set the status labels
                statusLineLbl.Text = usi.LineName;

                // Check if news item has expired
                if (usi.LastUpdated > expiredDateTime)
                {
                    // Set the status description and css if there are details
                    statusDescriptionLbl.Text = usi.StatusDescription;

                    if (!string.IsNullOrEmpty(usi.LineStatusDetails))
                    {
                        if (!statusDescriptionLbl.CssClass.Contains("highlight"))
                        {
                            statusDescriptionLbl.CssClass += string.Format(" highlight");
                        }
                    }

                    // If its not "good service", then include the details page
                    if (usi.StatusId != Properties.Current["UndergroundNews.Status.GoodService.Id"])
                    {
                        #region Detail view

                        // Creates a new undergroundDetails 'page' and adds the control to a list of details pages
                        UndergroundStatusDetailsControl undergroundStatusDetailsControl = (UndergroundStatusDetailsControl)Page.LoadControl("./Controls/UndergroundStatusDetailsControl.ascx");

                        // Set the control id to be the status item id to allow javascript to find it
                        undergroundStatusDetailsControl.ID = usiId;

                        undergroundStatusDetailsControl.Initialise(usi);

                        // Add the detail control to the details page placeholder
                        undergroundDetails.Controls.Add(undergroundStatusDetailsControl);

                        #endregion

                        #region Show Details button non-js

                        Button showDetailsBtnNonJS = e.Item.FindControlRecursive<Button>("showDetailsBtnNonJS");

                        showDetailsBtnNonJS.Text = "View";
                        showDetailsBtnNonJS.ToolTip = "View";
                        showDetailsBtnNonJS.CommandArgument = usiId;

                        #endregion
                    }
                    else
                    {
                        showDetailsBtn.CssClass += " nodetail";
                    }
                }
                else
                {
                    // This item has expired, set an unknown value
                    statusDescriptionLbl.Text = RM.GetString(CurrentLanguage.Value, "UndergroundNews.Status.Expired.Text");
                    showDetailsBtn.CssClass += " nodetail";
                }

                #endregion
            }
        }

        /// <summary>
        /// Show Details button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void showDetailsBtnNonJS_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                string newsId = ((Button)sender).CommandArgument;

                TDPPageMobile page = (TDPPageMobile)Page;

                page.SetPageTransfer(PageId.MobileTravelNewsDetail);

                page.AddQueryString(QueryStringKey.NewsId, newsId);
                page.AddQueryString(QueryStringKey.NewsMode, TravelNewsHelper.NewsViewMode_LUL);
            }
        }
        
        #endregion

        #region Private methods

        /// <summary>
        /// Gets the travel news data using current travel news state and binds it to travel news repeater
        /// </summary>
        private void BindUndergroundStatusData()
        {
            if (show)
            {
                // Get latest underground status items
                IUndergroundNewsHandler undergroundNewsHandler = TDPServiceDiscovery.Current.Get<IUndergroundNewsHandler>(ServiceDiscoveryKey.UndergroundNews);

                List<UndergroundStatusItem> items = undergroundNewsHandler.GetUndergroundStatusItems();

                // Clear the popups
                undergroundDetails.Controls.Clear();

                // If items exist, and they are not expired
                if ((items != null && items.Count > 0)
                    && !forceUnavailable
                    && !UndergroundNewsExpired(items, undergroundNewsHandler.UndergroundStatusLastLoaded))
                {  
                
                    // Underground status available
                    undergroundStatusUnavailableDiv.Visible = false;
                    undergroundStatusDiv.Visible = true;

                    undergroundStatusRptr.DataSource = items;
                    undergroundStatusRptr.DataBind();

                    // Set the underground status updated date time
                    string lastUpdated = RM.GetString(CurrentLanguage.Value, "UndergroundNews.LastUpdated.Text");

                    undergroundStatusLastUpdatedLbl.Text = string.Format("{0}: {1}",
                        lastUpdated,
                        undergroundNewsHandler.UndergroundStatusLastLoaded.ToString(dateTimeFormat));
                }
                else
                {
                    // Underground status unavailable
                    undergroundStatusUnavailableDiv.Visible = true;
                    undergroundStatusDiv.Visible = false;

                    undergroundStatusUnavailableLbl.Text = RM.GetString(CurrentLanguage.Value, "UndergroundNews.Unavailable.Text");
                }
            }
            else
            {
                undergroundStatusUnavailableDiv.Visible = false;
                undergroundStatusDiv.Visible = false;
            }
        }

        /// <summary>
        /// Returns true if the underground news has expired
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private bool UndergroundNewsExpired(List<UndergroundStatusItem> items, DateTime lastLoaded)
        {
            bool expired = false;

            SetExpiredDateTime();

            if (expiredDateTime > DateTime.MinValue)
            {
                // If the underground data was loaded too far in the past
                if (lastLoaded <= expiredDateTime)
                {
                    expired = true;
                }
                else
                {
                    // If all the individual items are in the past, then expired
                    bool allExpired = true;

                    foreach (UndergroundStatusItem usi in items)
                    {
                        if (usi.LastUpdated > expiredDateTime)
                        {
                            // This item was last updated "recently", so its ok
                            allExpired = false;
                            break;
                        }
                    }

                    // All items are expired
                    if (allExpired)
                        expired = true;
                }
            }

            return expired;
        }

        /// <summary>
        /// Sets the expired datetime value used to compare against news items
        /// </summary>
        private void SetExpiredDateTime()
        {
            int expiryMinutes = Properties.Current["UndergroundNews.ExpiryTime.Minutes"].Parse(0);

            if (expiryMinutes > 0)
            {
                // Check if Underground items are too old
                expiredDateTime = DateTime.Now.Subtract(new TimeSpan(0, expiryMinutes, 0));
            }
        }

        #endregion
    }
}