﻿// *********************************************** 
// NAME             : ErrorPage.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 21 Mar 2011
// DESCRIPTION  	: Error page for application errors
// ************************************************
// 

using System;
using TDP.Common;
using TDP.Common.Web;
using TDP.UserPortal.ScreenFlow;

namespace TDP.UserPortal.TDPWeb.Pages
{
    /// <summary>
    /// Error page
    /// </summary>
    public partial class ErrorPage : TDPPage
    {
        #region Private members

        // Urls shown to user
        private string URL_JourneyPlannerInput = string.Empty;
        private string URL_Contact = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ErrorPage()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.Error;
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
            PageTransferDetail ptd = GetPageTransferDetail(PageId.JourneyPlannerInput);

            // homepage
            if (ptd != null)
            {
                URL_JourneyPlannerInput = ResolveClientUrl(ptd.PageUrl);
            }

            ptd = GetPageTransferDetail(PageId.Contact);

            // London2012 contact us
            if (ptd != null)
            {
                URL_Contact = ResolveClientUrl(ptd.PageUrl);
            }
        }

        /// <summary>
        ///  Loads resources for page
        /// </summary>
        private void SetupResources()
        {
            lblErrorMessage1.Text = string.Format(GetResource("Error.Message1.Text"));
            lblErrorMessage2.Text = string.Format(GetResource("Error.Message2.Text"), URL_JourneyPlannerInput);
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