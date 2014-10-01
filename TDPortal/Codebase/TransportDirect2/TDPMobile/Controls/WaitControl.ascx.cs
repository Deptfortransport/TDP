// *********************************************** 
// NAME             : WaitControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Apr 2012
// DESCRIPTION  	: Wait control displaying the wait image and text
// ************************************************
// 
                
using System;
using System.Web.UI;
using TDP.Common.Web;
using System.Web.UI.WebControls;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// Wait control displaying the wait image and text
    /// </summary>
    public partial class WaitControl : System.Web.UI.UserControl
    {
        #region Public properties

        /// <summary>
        /// Read/Write Loading message label
        /// </summary>
        public Label LoadingMessageLabel
        {
            get { return loadingMessage; }
            set { loadingMessage = value; }
        }

        #endregion


        #region Page_Load, Page_PreRender

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
            SetupResources();

            SetupControls();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads resource strings for labels/controls
        /// </summary>
        private void SetupResources()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            loadingImage.ImageUrl = page.ImagePath + page.GetResourceMobile("JourneySummary.LoadingImage.Imageurl");
            loadingImage.AlternateText = Server.HtmlDecode(page.GetResourceMobile("JourneySummary.LoadingImage.AlternateText"));
            loadingImage.ToolTip = Server.HtmlDecode(page.GetResourceMobile("JourneySummary.LoadingImage.ToolTip"));

            if (string.IsNullOrEmpty(loadingMessage.Text))
            {
                loadingMessage.Text = Server.HtmlDecode(page.GetResourceMobile("JourneySummary.LoadingMessage.Text"));
            }

            // Default message used by javascript to display on wait control
            loadingMessage.Attributes["data-defaultmessage"] = loadingMessage.Text;

            longWaitMessage.Text = Server.HtmlDecode(page.GetResourceMobile("JourneySummary.LongWaitMessage.Text"));
            longWaitMessageLink.Text = Server.HtmlDecode(page.GetResourceMobile("JourneySummary.LongWaitMessageLink.Text"));
            longWaitMessageLink.ToolTip = Server.HtmlDecode(page.GetResourceMobile("JourneySummary.LongWaitMessageLink.ToolTip"));
        }

        /// <summary>
        /// Setsup controls
        /// </summary>
        private void SetupControls()
        {
            // For non-js users to manually refresh page
            longWaitMessageLink.NavigateUrl = Page.Request.Url.ToString();
        }

        #endregion
    }
}