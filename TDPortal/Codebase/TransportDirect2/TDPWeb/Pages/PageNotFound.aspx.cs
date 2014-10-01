﻿// *********************************************** 
// NAME             : PageNotFound.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 15 Apr 2011
// DESCRIPTION  	: PageNotFound page for resources not found
// ************************************************
// 

using System;
using TDP.Common;
using TDP.Common.Web;
using TDP.UserPortal.ScreenFlow;

namespace TDP.UserPortal.TDPWeb.Pages
{
    /// <summary>
    /// PageNotFound page
    /// </summary>
    public partial class PageNotFound : TDPPage
    {
       #region Private members

        // Urls shown to user
        private string URL_Homepage = string.Empty;
        private string URL_Sitemap = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PageNotFound()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.PageNotFound;
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
            DisplayControls();
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
            PageTransferDetail ptd = GetPageTransferDetail(PageId.Homepage);

            // London2012 homepage
            if (ptd != null)
            {
                URL_Homepage = ResolveClientUrl(ptd.PageUrl);
            }

            ptd = GetPageTransferDetail(PageId.Sitemap);

            // London2012 sitemap
            if (ptd != null)
            {
                URL_Sitemap = ResolveClientUrl(ptd.PageUrl);
            }
        }

        /// <summary>
        ///  Loads resources for page
        /// </summary>
        private void SetupResources()
        {
            lblMessage1.Text = GetResource("PageNotFound.Message1.Text");
            lblMessage2.Text = string.Format(GetResource("PageNotFound.Message2.Text"), URL_Homepage, URL_Sitemap);
            lblMessage3.Text = GetResource("PageNotFound.Message3.Text");
        }

        /// <summary>
        /// Sets the visibility of controls on the page
        /// </summary>
        private void DisplayControls()
        {
            // Don't display the sidebars
            ((TDPWeb)this.Master).DisplaySideBarLeft = false;
            ((TDPWeb)this.Master).DisplaySideBarRight = false;
        }

        #endregion
    }
}