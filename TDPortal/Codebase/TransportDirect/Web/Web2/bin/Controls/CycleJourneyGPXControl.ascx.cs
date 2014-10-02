// *********************************************** 
// NAME                 : CycleJourneyGPXControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 15/06/2008
// DESCRIPTION          : Control to allow user to download a GPX file
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CycleJourneyGPXControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Nov 18 2009 15:14:14   mmodi
//Add opens new window text to hyperlinks
//Resolution for 5337: Cycle Planner - Opens new window text shown against links
//
//   Rev 1.1   Dec 10 2008 11:24:36   mmodi
//Updated to support return journeys
//Resolution for 5195: Cycle Planner - GPX download file is not generated for the Return journey
//
//   Rev 1.0   Jul 18 2008 13:26:56   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.ScreenFlow;


namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class CycleJourneyGPXControl : TDUserControl
    {
        #region Private members

        private bool printable = false;
        private bool outward = false;
        private CycleJourney cycleJourney = null;

        #endregion

        #region Initialise
        /// <summary>
        /// Initialises this control with a specific cycle journey
        /// </summary>
        public void Initialise(CycleJourney cycleJourney, bool outward)
        {
            this.outward = outward;
            this.cycleJourney = cycleJourney;
        }

        #endregion

        #region Page_Load, Page_PreRender
        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // No need to do anything in the page load
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            LoadResources();

            SetControlVisibility();

            SetupDownloadLink();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the control display text 
        /// </summary>
        private void LoadResources()
        {
            labelTitle.Text = GetResource("CyclePlanner.CycleJourneyGPXControl.labelTitle.Text");
            labelDownloadDescription.Text = GetResource("CyclePlanner.CycleJourneyGPXControl.labelDownloadDescription.Text");
            hyperlinkDownload.Text = GetResource("CyclePlanner.CycleJourneyGPXControl.hyperlinkDownload.Text");

            // Add on the opens new window resource
            if (!string.IsNullOrEmpty(GetResource("ExternalLinks.OpensNewWindowImage")))
            {
                hyperlinkDownload.Text += " " + GetResource("ExternalLinks.OpensNewWindowImage");
            }
        }

        /// <summary>
        /// Hides the download link if there is no cycle journey
        /// </summary>
        private void SetControlVisibility()
        {
            hyperlinkDownload.Visible = (cycleJourney != null);
        }

        /// <summary>
        /// Sets up the download hyperlink
        /// </summary>
        private void SetupDownloadLink()
        {
            // No need to set up if we're not displaying the link
            if (hyperlinkDownload.Visible)
            {
                hyperlinkDownload.Target = "_blank";

                #region Link for the download hyperlink
                
                // Get the PageController from Service Discovery
                IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];

                // Get the PageTransferDataCache from the pageController
                IPageTransferDataCache pageTransferDataCache = pageController.PageTransferDataCache;

                // Get the PageTransferDetails object to which holds the Url
                PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.CycleJourneyGPXDownload);

                string url = @"~/" + pageTransferDetails.PageUrl;

                // append the outward/return flag
                url += (url.IndexOf("?") > 0) ? "&outward=" : "?outward=";
                url += outward;
                
                hyperlinkDownload.NavigateUrl = url;
                #endregion
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. Printable mode of control
        /// </summary>
        public bool Printable
        {
            get { return printable; }
            set { printable = value; }
        }

        #endregion
    }
}