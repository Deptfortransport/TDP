// *********************************************** 
// NAME             : ErrorPage.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 19 Mar 2012
// DESCRIPTION  	: Error page for application errors
// ************************************************

using System;
using TDP.Common;
using TDP.Common.Web;
using TDP.UserPortal.ScreenFlow;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// Error page for application errors
    /// </summary>
    public partial class TermsPage : TDPPageMobile
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TermsPage()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobileTerms;
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
            // Use the browser back in the header
            ((TDPMobile)Master).DisplayBrowserBack = true;
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();
        }

        #endregion

        #region Private methods

        /// <summary>
        ///  Loads resources for page
        /// </summary>
        private void SetupResources()
        {
            titleMessage.InnerHtml = GetResourceMobile("Terms.HeadingTitle.Text");
            bodyTextDiv.InnerHtml = GetResourceMobile("Terms.ContentDiv.Html");
        }

        #endregion
    }
}