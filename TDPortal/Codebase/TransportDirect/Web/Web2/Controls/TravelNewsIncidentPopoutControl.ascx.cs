// *********************************************** 
// NAME                 : TravelNewsIncidentPopoutControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 26/08/2011
// DESCRIPTION          : Control to display travel news incident images and detail popups for road journey details
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TravelNewsIncidentPopoutControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Sep 19 2011 11:43:10   mmodi
//Show different tool tip text when in printerfriendly mode
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.1   Sep 02 2011 10:22:10   apatel
//Real time car changes
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.0   Sep 01 2011 10:48:20   apatel
//Initial revision.
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.TravelNewsInterface;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Control to display travel news incident images and detail popups for road journey details
    /// </summary>
    public partial class TravelNewsIncidentPopoutControl : TDPrintableUserControl
    {
        #region Private Fields
        private string[] travelIncidents = new string[0];
        #endregion

        #region Public Properties
        /// <summary>
        /// Read/write. Determins the array of  travel news incident uids
        /// for which tavel news incident detail popup needs displaying
        /// </summary>
        public string[] TravelIncidents
        {
            get
            {
                return travelIncidents;
            }
            set
            {
                travelIncidents = value;
            }
        }
        #endregion

        #region Page Events
        /// <summary>
        /// Page load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RoadTravelNews.ItemDataBound += new RepeaterItemEventHandler(RoadTravelNews_ItemDataBound);
        }

        /// <summary>
        /// Page prerender event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (travelIncidents != null && travelIncidents.Length > 0)
            {
                RoadTravelNews.DataSource = travelIncidents;
                RoadTravelNews.DataBind();
                               
            }
        }
        #endregion

        #region Control event handlers
        /// <summary>
        /// RoadTravelNews data bound event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoadTravelNews_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string loadingImageUrl = GetResource("TravelNewsIncidentPopupControl.Loading.ImageUrl"); 

            if (e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.SelectedItem)
            {
                string travelNewsIncidentId = e.Item.DataItem.ToString();

                TravelNewsItem travelNewsItem = TravelNewsHelper.GetTravelNewsItem(travelNewsIncidentId);

                Image roadNewsItem = e.Item.FindControl("roadNewsItem") as Image;

                roadNewsItem.ImageUrl = GetResource(string.Format("TravelNewsIncidentPopupControl.RoadIncident.{0}.Image.ImageUrl", travelNewsItem.PlannedIncident ? "PlannedIncident" : "UnplannedIncident"));

                TDPage page = (TDPage)this.Page;

                if ((!page.IsJavascriptEnabled) || (PrinterFriendly))
                {
                    roadNewsItem.ToolTip = GetResource(string.Format("TravelNewsIncidentPopupControl.RoadIncident.{0}.Image.ToolTip", travelNewsItem.PlannedIncident ? "PlannedIncident" : "UnplannedIncident"));
                }
                else
                {
                    roadNewsItem.ToolTip = GetResource("TravelNewsIncidentPopupControl.RoadIncident.Image.ToolTip");
                }

                roadNewsItem.AlternateText = GetResource(string.Format("TravelNewsIncidentPopupControl.RoadIncident.{0}.Image.AlternateText", travelNewsItem.PlannedIncident ? "PlannedIncident" : "UnplannedIncident"));

                string title = GetResource(string.Format("TravelNewsIncidentPopupControl.RoadIncident.{0}.Title", travelNewsItem.PlannedIncident ? "PlannedIncident" : "UnplannedIncident")); 

                
                if (!PrinterFriendly)
                {
                    roadNewsItem.Attributes["onclick"] = string.Format("TravelNewsInfoWindow.showTNInfo(event,'{0}', '{1}','{2}');"
                        , travelNewsIncidentId,title, loadingImageUrl);
                }
            }
        }

        #endregion
    }
}