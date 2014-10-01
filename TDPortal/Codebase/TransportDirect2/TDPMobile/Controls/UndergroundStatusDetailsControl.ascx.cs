// *********************************************** 
// NAME             : UndergroundStatusDetailsControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 01 May 2012
// DESCRIPTION  	: A template for holding the details of underground status details
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common.ResourceManager;
using TDP.UserPortal.UndergroundNews;
using TDP.Common.Extenders;
using TDP.Common.Web;
using TDP.Common.PropertyManager;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// A template for holding the details of underground status details
    /// </summary>
    public partial class UndergroundStatusDetailsControl : System.Web.UI.UserControl
    {
        #region Variables

        private TDPResourceManager RM = Global.TDPResourceManager;

        private UndergroundStatusItem usi = null;

        private DateTime expiredDateTime = DateTime.MinValue;

        #endregion

        #region Page Load
        
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

        #region Public methods

        /// <summary>
        /// Initialise
        /// </summary>
        /// <param name="tni"></param>
        public void Initialise(UndergroundStatusItem usi)
        {
            this.usi = usi;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Setsup the controls
        /// </summary>
        public void SetupControls()
        {
            if (usi != null)
            {
                SetExpiredDateTime();

                // Add the display text
                undergroundServiceHeadlineLbl.InnerText = usi.LineName;
                

                // Set the line color
                string undergroundLineClass = usi.LineName.ToLower().Replace("&", "and").Replace(" ", "");

                if (!undergroundServiceHeadlineLbl.Attributes["class"].Contains(undergroundLineClass))
                {
                    undergroundServiceHeadlineLbl.Attributes["class"] += string.Format(" " + undergroundLineClass);
                }

                // Check if news item has expired
                if (usi.LastUpdated > expiredDateTime)
                {
                    statusDescriptionLbl.Text = usi.StatusDescription;
                    statusDetailLbl.Text = usi.LineStatusDetails;
                }
                else
                {
                    // This item has expired, set an unknown value
                    statusDescriptionLbl.Text = RM.GetString(CurrentLanguage.Value, "UndergroundNews.Status.Expired.Text");
                }
            }
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