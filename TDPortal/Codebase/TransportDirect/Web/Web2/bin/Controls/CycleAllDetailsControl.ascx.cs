// *********************************************** 
// NAME                 : CycleAllDetailsControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 15/06/2008
// DESCRIPTION          : Wrapper control for the Cycle details, Cycle graph, and GPX tracking items
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CycleAllDetailsControl.ascx.cs-arc  $ 
//
//   Rev 1.8   Oct 26 2010 14:30:32   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.7   Dec 10 2009 11:06:46   mmodi
//Updated to display zoom to cycle map direction number links in table
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Nov 18 2009 15:14:14   mmodi
//Add opens new window text to hyperlinks
//Resolution for 5337: Cycle Planner - Opens new window text shown against links
//
//   Rev 1.5   Sep 25 2009 11:36:58   apatel
//code updated so in km units mode when switch to the table view from gradients view all of the values show in correct units(kms)  USD UK5647417
//Resolution for 5327: Miles and Km get muddled in Cycle Planner
//
//   Rev 1.4   Sep 25 2008 11:26:32   mmodi
//Updated for table version of gradient profile
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 22 2008 10:27:10   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 06 2008 14:50:50   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 01 2008 16:35:54   mmodi
//Do not show gradient profile if turned off
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 13:26:50   mmodi
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
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Control to wrap the cycle journey items
    /// </summary>
    public partial class CycleAllDetailsControl : TDUserControl
    {
        #region Private members

        private bool printable;
        private bool outward;

        #endregion

        #region Initialise
        /// <summary>
        /// Initialises this control with a specific cycle journey
        /// </summary>
        public void Initialise(CycleJourney cycleJourney, bool outward, TDJourneyParametersMulti journeyParameters, TDJourneyViewState viewState)
        {
            this.outward = outward;

            // Initialise the controls 
            this.cycleSummaryControl.Initialise(cycleJourney, outward, journeyParameters);
            this.cycleJourneyDetailsTableControl.Initialise(cycleJourney, outward, viewState, journeyParameters);
            this.cycleJourneyGPXControl.Initialise(cycleJourney, outward);
            this.cycleJourneyGraphControl.Initialise(cycleJourney, outward);
        }

        /// <summary>
        /// Method which sets the values needed to add map javascript to the direction links
        /// </summary>
        public void SetMapProperties(bool addMapJavascript, string mapId, string mapJourneyDisplayDetailsDropDownId,
            string scrollToControlId, string sessionId)
        {
            cycleJourneyDetailsTableControl.SetMapProperties(addMapJavascript, mapId,
                mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
        }

        #endregion

        #region Page_Load, Page_PreRender
        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Disable the gradient profiler if it should be turned off
            if (!FindCycleInputAdapter.GradientProfilerAvailable)
            {
                this.cycleJourneyGraphControl.Visible = false;
            }
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            //Set up road units
            this.cycleJourneyDetailsTableControl.RoadUnits = TDSessionManager.Current.InputPageState.Units;    

            // Set controls printable values
            this.cycleSummaryControl.Printable = printable;
            this.cycleJourneyDetailsTableControl.Printable = printable;
            this.cycleJourneyGraphControl.Printable = printable;
            this.cycleJourneyGPXControl.Printable = printable;

            LoadResources();

            PopulateFooterLinks();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to load the text and image resources for the page
        /// </summary>
        private void LoadResources()
        {
            labelCycleAllDetailsControlTitle.Text = (outward ?
                GetResource("CyclePlanner.labelCycleAllDetailsControlTitle.Text.OutwardJourney") :
                GetResource("CyclePlanner.labelCycleAllDetailsControlTitle.Text.ReturnJourney"));
        }

        /// <summary>
        /// Populates the links shown at the footer of this control
        /// </summary>
        private void PopulateFooterLinks()
        {
            if (!printable)
            {
                imageCycleEngland.ImageUrl = GetResource("CyclePlanner.CycleAllDetailsControl.imageCycleEngland.URL");
                imageCycleEngland.AlternateText = GetResource("CyclePlanner.CycleAllDetailsControl.imageCycleEngland.AltText");

                hyperlinkCycleEngland.Text = GetResource("CyclePlanner.CycleAllDetailsControl.hyperlinkCycleEngland.Text");
                hyperlinkCycleEngland.Target = "_blank";

                // Add on the opens new window resource
                if (!string.IsNullOrEmpty(GetResource("ExternalLinks.OpensNewWindowImage")))
                {
                    hyperlinkCycleEngland.Text += " " + GetResource("ExternalLinks.OpensNewWindowImage");
                }

                ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current["CycleEngland"];
                if (link != null)
                {
                    hyperlinkCycleEngland.NavigateUrl = link.Url;
                }
                else
                {
                    HideFooterLinks();
                }
            }
            else
            {
                HideFooterLinks();
            }
        }

        /// <summary>
        /// Hides all of the footer links
        /// </summary>
        private void HideFooterLinks()
        {
            imageCycleEngland.Visible = false;
            hyperlinkCycleEngland.Visible = false;
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

        /// <summary>
        /// Read only. Expose Show map button to allow users to attach to event
        /// </summary>
        public TDButton ButtonShowMap
        {
            get { return buttonShowMap; }
        }

        /// <summary>
        /// Read only. Exposes the CycleJourneyDetailsTableControl
        /// </summary>
        public CycleJourneyDetailsTableControl CycleJourneyDetailsTableControl
        {
            get { return cycleJourneyDetailsTableControl; }
        }

        #endregion
    }
}