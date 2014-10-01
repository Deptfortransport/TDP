// *********************************************** 
// NAME             : JourneyHeadingControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 17 Jul 2013
// DESCRIPTION  	: JourneyHeading control to display a summary of the journey requested
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common.Web;
using TDP.Common.ResourceManager;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.ScreenFlow;
using TDP.Common;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// JourneyHeading control to display a summary of the journey requested
    /// </summary>
    public partial class JourneyHeadingControl : System.Web.UI.UserControl
    {
        #region Private Fields

        private ITDPJourneyRequest journeyRequest = null;
        private Journey journey = null;
        private bool showJourneyHeadingSummary = false;
        private bool showJourneyDurationSummary = false;

        private SummaryInstructionAdapter adapter = null;

        private static string RG = TDPResourceManager.GROUP_JOURNEYOUTPUT;
        private static string RC = TDPResourceManager.COLLECTION_JOURNEY;

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
            adapter = new SummaryInstructionAdapter(Global.TDPResourceManager, true);

            SetupJourneyHeading();

            SetupJourneyDuration();

            // Set overall visibility
            journeyHeadingContainer.Visible = journeyHeadingDiv.Visible
                                              || journeyDurationDiv.Visible;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(ITDPJourneyRequest journeyRequest, Journey journey, bool showJourneyHeadingSummary, bool showJourneyDurationSummary)
        {
            this.journeyRequest = journeyRequest;
            this.journey = journey;
            this.showJourneyHeadingSummary = showJourneyHeadingSummary;
            this.showJourneyDurationSummary = showJourneyDurationSummary;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets up the journey heading text
        /// </summary>
        private void SetupJourneyHeading()
        {
            if (showJourneyHeadingSummary && (journeyRequest != null))
            {
                TDPPageMobile page = (TDPPageMobile)Page;

                string TXT_JourneyHeaderOutward = page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.Outward.Text");

                // Journey locations
                string headingJourney = string.Format(TXT_JourneyHeaderOutward,
                    journeyRequest.Origin.DisplayName,
                    journeyRequest.Destination.DisplayName);

                lblHeadingJourney.Text = headingJourney;

                // Journey date
                if (journeyRequest.OutwardDateTime.Date == DateTime.Now.Date)
                {
                    string TXT_JourneyArriveByToday = page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.ArriveByToday.Text");
                    string TXT_JourneyLeavingAtToday = page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.LeavingAtToday.Text");

                    // Today
                    lblHeadingDate.Text = string.Format(
                        journeyRequest.OutwardArriveBefore ? TXT_JourneyArriveByToday : TXT_JourneyLeavingAtToday,
                        journeyRequest.OutwardDateTime.ToString("HH:mm"));
                }
                else
                {
                    string TXT_JourneyArriveBy = page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.ArriveDateByTime.Text");
                    string TXT_JourneyLeavingAt = page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.LeavingDateAtTime.Text");

                    // Future
                    lblHeadingDate.Text = string.Format(
                        journeyRequest.OutwardArriveBefore ? TXT_JourneyArriveBy : TXT_JourneyLeavingAt,
                            journeyRequest.OutwardDateTime.ToString("dd-MMM-yyyy"),
                            journeyRequest.OutwardDateTime.ToString("HH:mm"));
                }

                // Link back to input
                PageTransferDetail transferDetail = page.GetPageTransferDetail(PageId.MobileInput);

                journeyHeadingLnk.ToolTip = string.Format(page.GetResource(RG, RC, "JourneyOutput.JourneyHeader.AmendJourney.ToolTip"), headingJourney);
                journeyHeadingLnk.NavigateUrl = transferDetail.PageUrl;
            }
            else
            {
                journeyHeadingDiv.Visible = false;
            }
        }

        /// <summary>
        /// Sets up the journey duration text
        /// </summary>
        private void SetupJourneyDuration()
        {
            if (showJourneyDurationSummary && (journeyRequest != null) && (journey != null))
            {
                TDPPageMobile page = (TDPPageMobile)Page;

                changes.Text = string.Format("{0} {1}",
                    journey.InterchangeCount.ToString(),
                    page.GetResource(RG, RC, "JourneyOutput.HeaderRow.Changes.Text"));

                duration.Text = string.Empty;
                durationSR.Text = page.GetResource(RG, RC, "JourneyOutput.HeaderRow.Duration.Text");
                durationTime.Text = adapter.GetJourneyTime(journey.StartTime, journey.EndTime, true);

                leave.Text = page.GetResource(RG, RC, "JourneyOutput.HeaderRow.Leave.Text");
                leaveTime.Text = adapter.GetTime(journey.StartTime, journeyRequest.OutwardDateTime);

                arrive.Text = page.GetResource(RG, RC, "JourneyOutput.HeaderRow.Arrive.Text");
                arriveTime.Text = adapter.GetTime(journey.EndTime, journeyRequest.OutwardDateTime);

                // Apply style to indicate this is being displayed for duration mode
                if (!journeyHeadingContainer.Attributes["class"].Contains("journeyHeadingContainerBottom"))
                    journeyHeadingContainer.Attributes["class"] += " journeyHeadingContainerBottom";
            }
            else
            {
                journeyDurationDiv.Visible = false;
            }
        }

        #endregion
    }
}