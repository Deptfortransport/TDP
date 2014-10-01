// *********************************************** 
// NAME             : Default.aspx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Feb 2012
// DESCRIPTION  	: Default Mobile page. Transfers user to actual "Mobile Home" page
// ************************************************
// 

using System;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.TDPMobile.Adapters;

namespace TDP.UserPortal.TDPMobile
{
    /// <summary>
    /// Default page
    /// </summary>
    public partial class _Default : TDPPageMobile
    {
        #region Private members

        private JourneyInputAdapter journeyInputAdapter;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public _Default()
            : base(Global.TDPResourceManager)
        {
            pageId = PageId.MobileDefault;
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
            journeyInputAdapter = new JourneyInputAdapter();

            SetupResources();

            SetupControls();
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Publit Transport mode button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void publicTransportModeBtn_Click(object sender, EventArgs e)
        {
            UpdateJourneyPlannerMode(TDPJourneyPlannerMode.PublicTransport);

            SetPageTransfer(PageId.MobileInput);
        }

        /// <summary>
        /// Cycle mode button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cycleModeBtn_Click(object sender, EventArgs e)
        {
            UpdateJourneyPlannerMode(TDPJourneyPlannerMode.Cycle);

            SetPageTransfer(PageId.MobileInput);
        }

        /// <summary>
        /// Travel news button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void travelNewsBtn_Click(object sender, EventArgs e)
        {
            SetPageTransfer(PageId.MobileTravelNews);
        }
                
        #endregion

        #region Private methods

        /// <summary>
        /// Setups resources labels and controls
        /// </summary>
        private void SetupResources()
        {
            publicTransportLogoBtn.Text = string.Empty;
            publicTransportLogoBtn.ToolTip = GetResourceMobile("Default.PublicTransportModeButton.ToolTip");

            publicTransportModeBtn.Text = GetResourceMobile("Default.PublicTransportModeButton.Text");
            publicTransportModeBtn.ToolTip = GetResourceMobile("Default.PublicTransportModeButton.ToolTip");

            cycleModeBtn.Text = GetResourceMobile("Default.CycleModeButton.Text");
            cycleModeBtn.ToolTip = GetResourceMobile("Default.CycleModeButton.ToolTip");

            travelNewsBtn.Text = GetResourceMobile("Default.TravelNewsButton.Text");
            travelNewsBtn.ToolTip = GetResourceMobile("Default.TravelNewsButton.ToolTip");

        }

        /// <summary>
        /// Setup the controls
        /// </summary>
        private void SetupControls()
        {
            // Hide back next navigation buttons
            ((TDPMobile)Master).DisplayNavigation = false;

            // Cycle Planner functionality turned off
            if (!Properties.Current["CyclePlanner.Enabled.Switch"].Parse(true))
            {
                cycleModeBtn.Visible = false;
                divCycleMode.Visible = false;
            }

            // Travel News button turned off
            if (!Properties.Current["TravelNews.DefaultPage.Enabled.Switch"].Parse(true))
            {
                travelNewsBtn.Visible = false;
                divTravelNews.Visible = false;
            }
        }

        /// <summary>
        /// Updates the journey request (if exists) and cookie with the planner mode
        /// </summary>
        /// <param name="mode"></param>
        private void UpdateJourneyPlannerMode(TDPJourneyPlannerMode mode)
        {
            // Journey request will only exist in session if it is a returning user 
            // (i.e. has cookie, or page landed), so update that with planner mode,
            // or create an empty journey request with the mode
            journeyInputAdapter.ValidateAndUpdateTDPRequestForPlannerMode(mode);
        }

        #endregion
    }
}