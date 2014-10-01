// *********************************************** 
// NAME             : PageNotFound.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 19 Mar 2012
// DESCRIPTION  	: PageNotFound page for resources not found
// ************************************************

using System;
using TDP.Common;
using TDP.Common.Web;
using TDP.UserPortal.ScreenFlow;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// PageNotFound page for resources not found
    /// </summary>
    public partial class PageNotFound : TDPPageMobile
    {
        #region Private members

        // Urls shown to user
        private string URL_Homepage = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PageNotFound()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobilePageNotFound;
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
            SetupURLs();

            SetupResources();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets the urls
        /// </summary>
        private void SetupURLs()
        {
            // Hide the back
            ((TDPMobile)Master).DisplayBack = false;

            PageTransferDetail ptd = GetPageTransferDetail(PageId.MobileDefault);

            if (ptd != null)
            {
                URL_Homepage = ResolveClientUrl(ptd.PageUrl);
            }
        }

        /// <summary>
        ///  Loads resources for page
        /// </summary>
        private void SetupResources()
        {
            titleMessage.InnerHtml = GetResourceMobile("PageNotFound.HeadingTitle.Text");
            lblMessage.Text = string.Format(GetResourceMobile("PageNotFound.Message.Text"), URL_Homepage);
        }

        #endregion
    }
}