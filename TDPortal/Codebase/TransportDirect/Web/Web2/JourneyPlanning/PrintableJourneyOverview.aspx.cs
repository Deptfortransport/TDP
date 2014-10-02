// *********************************************** 
// NAME                 : PrintableJourneyOverview.aspx.cs
// AUTHOR               : Dan Gath
// DATE CREATED         : 20/01/2008
// DESCRIPTION			: Printable Journey Overview page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableJourneyOverview.aspx.cs-arc  $
//
//   Rev 1.4   Feb 23 2010 13:29:10   mmodi
//Updated to pass FindAMode value into the overview control
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Jan 12 2009 14:50:34   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:26   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements
//Initial revision

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
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
    public partial class PrintableJourneyOverview : TDPrintablePage, INewWindowPage
    {
        #region Private members

        private ITDSessionManager tdSessionManager;

        /// <summary>
        /// True if the Itinerary exists and there are no extensions in the process of being planned
        /// </summary>
        private bool showItinerary = false;

        private bool outwardExists = false;
        private bool returnExists = false;

        private bool outwardArriveBefore = false;
        private bool returnArriveBefore = false;

        #endregion

        #region Constructor, PageLoad

        /// <summary>
		/// Constructor.
		/// </summary>
		public PrintableJourneyOverview() : base()
		{
			pageId = PageId.PrintableJourneyOverview;
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            tdSessionManager = TDSessionManager.Current;

            SetupControls();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupText();

            // Prerender setup the title
            PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

            plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);

        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises/displays controls on the page based on the journey results
        /// </summary>
        private void SetupControls()
        {
            DetermineStateOfResults();

            // Set up overview controls
            if (outwardExists)
                findOverviewResultTableControlOutward.Initialise(true, true, outwardArriveBefore, tdSessionManager.FindAMode);

            if (returnExists)
                findOverviewResultTableControlReturn.Initialise(true, false, returnArriveBefore, tdSessionManager.FindAMode);

            SetControlVisibility();
        }

        /// <summary>
        /// Determines the state of the journey results for use by SetupControls
        /// </summary>
        private void DetermineStateOfResults()
        {
            ITDJourneyResult result = tdSessionManager.JourneyResult;

            if (result != null)
            {
                //check for normal result
                outwardExists = ((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid;
                returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;

                // Get time types for journey.
                outwardArriveBefore = tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
                returnArriveBefore = tdSessionManager.JourneyViewState.JourneyReturningTimeSearchType;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetControlVisibility()
        {
            outwardPanel.Visible = outwardExists;
            returnPanel.Visible = returnExists;
        }

        private void SetupText()
        {
            labelInstructions.Text = GetResource("JourneyOverview.labelInstructions.Text");

            labelJourneyOptionsTableDescription.Text = GetResource("JourneySummary.labelJourneyOptionsTableDescription.Text");

            // Initialise static labels, hypertext text and image button Urls 
            // from Resourcing Mangager.
            #region Printer page specific text
            // Initialise static labels text
            LabelFootnote.Text = GetResource(
                "JourneySummary.labelFootnote.Text");

            labelPrinterFriendly.Text = GetResource(
                "StaticPrinterFriendly.labelPrinterFriendly");

            labelInstructions.Text = GetResource(
                "StaticPrinterFriendly.labelInstructions");

            labelDate.Text = TDDateTime.Now.ToString("G");

            labelUsername.Visible = tdSessionManager.Authenticated;
            labelUsernameTitle.Visible = tdSessionManager.Authenticated;
            if (tdSessionManager.Authenticated)
            {
                labelUsername.Text = tdSessionManager.CurrentUser.Username;
            }

            if (showItinerary)
            {
                labelReferenceNumberTitle.Visible = false;
            }
            else
            {
                // Set the journey reference number from the result
                labelReferenceNumberTitle.Visible = true;
                labelJourneyReferenceNumber.Text =
                    tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
            }

            #endregion
           
        }

        #endregion
    }
}
