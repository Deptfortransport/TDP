// *********************************************** 
// NAME             : CallingPointsControl.ascx      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 25 Jul 2013
// DESCRIPTION  	: CallingPointsControl to display calling points for a journey leg
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.Common.ResourceManager;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;
using TDP.Common.LocationService;
using System.Web.UI.HtmlControls;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// CallingPointsControl to display calling points for a journey leg
    /// </summary>
    public partial class CallingPointsControl : System.Web.UI.UserControl
    {
        #region Private Fields

        private List<JourneyCallingPoint> intermediateLegs = new List<JourneyCallingPoint>();
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Read only. Returns true if this control contains calling points
        /// </summary>
        public bool HasCallingPoints
        {
            get { return (intermediateLegs != null && intermediateLegs.Count > 0); }
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

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
            SetupControls();
        }

        #endregion

        #region Control Event Handlers

        /// <summary>
        /// LegCallingPoints repeater data bound event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LegCallingPoints_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem
                || e.Item.ItemType == ListItemType.Item
                || e.Item.ItemType == ListItemType.SelectedItem)
            {
                TDPPageMobile page = (TDPPageMobile)Page;

                // Current calling point
                JourneyCallingPoint callingPoint = e.Item.DataItem as JourneyCallingPoint;

                Label pointDepartTime = e.Item.FindControlRecursive<Label>("pointDepartTime");
                Label pointLocation = e.Item.FindControlRecursive<Label>("pointLocation");
                Image legNodeImage = e.Item.FindControlRecursive<Image>("legNodeImage");

                pointDepartTime.Text = callingPoint.DepartureDateTime.ToString("HH:mm");
                pointLocation.Text = callingPoint.Location.DisplayName;

                legNodeImage.ImageUrl = page.ImagePath + page.GetResource(
                    TDPResourceManager.GROUP_JOURNEYOUTPUT,
                    TDPResourceManager.COLLECTION_JOURNEY,
                    "JourneyOutput.Image.CallingPoint.ImageURL");
                legNodeImage.AlternateText = page.GetResource(
                    TDPResourceManager.GROUP_JOURNEYOUTPUT,
                    TDPResourceManager.COLLECTION_JOURNEY,
                    "JourneyOutput.Image.CallingPoint.AltText");
                legNodeImage.ToolTip = legNodeImage.AlternateText;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(List<JourneyCallingPoint> callingPoints)
        {
            if (callingPoints != null)
            {
                this.intermediateLegs = callingPoints;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method which setup the controls
        /// </summary>
        private void SetupControls()
        {
            legCallingPoints.DataSource = intermediateLegs;
            legCallingPoints.DataBind();
        }

        #endregion
    }
}