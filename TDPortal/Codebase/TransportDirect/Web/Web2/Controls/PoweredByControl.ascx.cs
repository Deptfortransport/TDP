// *********************************************** 
// NAME                 : PoweredByControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 03/11/2005 
// DESCRIPTION  		: Powered By control displayed on partner pages, loads html from content database
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PoweredByControl.ascx.cs-arc  $ 
//
//   Rev 1.2   May 06 2008 14:15:34   dgath
//Updated to always show logo in top navigation, and never in right hand navigation, on Visit Britain site.
//Resolution for 4942: Make the Provided by Transport Direct consistently in the black bar
//
//   Rev 1.1   Mar 31 2008 13:22:24   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Mar 15 2008 13:00:00   mmodi
//Initial revision.
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
using TD.ThemeInfrastructure;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.UserPortal.Web.Controls
{
    public partial class PoweredByControl : TDUserControl
    {
        public enum PoweredByMode
        {
            Default,
            LogoOnly,
            LogoHeader,
            LogoFooter
        }

        #region Private members 
        private PoweredByMode poweredByMode = PoweredByMode.Default;
        private int currentTheme = TD.ThemeInfrastructure.ThemeProvider.Instance.GetTheme().Id;
        #endregion

        #region Page Load
        /// <summary>
        /// Page Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupPoweredByControl();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Adds the html content for the control
        /// </summary>
        private void SetupPoweredByControl()
        {
            bool showPoweredBy = false;

            try
            {
                showPoweredBy = bool.Parse(Properties.Current["PoweredByControl.ShowPoweredBy"]);
            }
            catch
            {
                // don't show powered by panel
            }

            if ((showPoweredBy) && (ShowPoweredByOnPage()))
            {
                panelPoweredBy.Visible = true;

                // Add the literal control
                if (panelPoweredByContent.Controls.Count == 0)
                {
                    panelPoweredByContent.Controls.Add(new LiteralControl());
                }

                // Get the powred by html from database
                switch (poweredByMode)
                {
                    case PoweredByMode.LogoOnly:
                    case PoweredByMode.LogoHeader:
                        ((LiteralControl)panelPoweredByContent.Controls[0]).Text = GetResource("PoweredByControl.HTMLContent.LogoOnly");
                        break;
                    default:
                        ((LiteralControl)panelPoweredByContent.Controls[0]).Text = GetResource("PoweredByControl.HTMLContent");
                        break;
                }
            }
            else
            {
                panelPoweredBy.Visible = false;
            }
        }

        /// <summary>
        /// Determine whether to show based on control mode and pageId
        /// </summary>
        /// <returns></returns>
        private bool ShowPoweredByOnPage()
        {
            // Always show
            bool show = true;

            // This control may be used multiple times on a page (e.g. in header, footer, main content), 
            // however we only want to show it once. 
            // So dependent on controlmode and page it is on, we set visibility
            
            if (currentTheme == 2)//VisitBritain - only show in main nav bar, not in right hand menu
            {
                if (poweredByMode == PoweredByMode.Default)
                    show = false;
            }
            else//all other themes
            {
                if (poweredByMode == PoweredByMode.LogoHeader)
                {
                    // If page is any of the following, we don't want it displayed as the logo may be used elsewhere on page
                    switch (PageId)
                    {
                        case PageId.HomeFindAPlace:
                        case PageId.HomeTravelInfo:
                        case PageId.HomeTipsTools:
                        case PageId.HomePlanAJourney:
                        case PageId.FindTrainCostInput:
                        case PageId.ExtendJourneyInput:
                        case PageId.FindBusInput:
                        case PageId.FindCarInput:
                        case PageId.FindCoachInput:
                        case PageId.FindFlightInput:
                        case PageId.FindTrainInput:
                        case PageId.FindTrunkInput:
                        case PageId.JourneyPlannerInput:
                        case PageId.ParkAndRideInput:
                        case PageId.VisitPlannerInput:
                        case PageId.JourneyReplanInputPage:
                        case PageId.JourneyPlannerAmbiguity:
                        case PageId.FindStationInput:
                        case PageId.FindCarParkInput:
                        case PageId.WaitPage:
                            show = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            return show;
        }

        #endregion

        #region Public properties

        public PoweredByMode PoweredByControlMode
        {
            get { return poweredByMode; }
            set { poweredByMode = value; }
        }

        #endregion
    }
}