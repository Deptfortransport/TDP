// *********************************************** 
// NAME                 : JourneyOverview.aspx.cs
// AUTHOR               : Dan Gath
// DATE CREATED         : 20/01/2008
// DESCRIPTION			: Journey Overview Page
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyOverview.aspx.cs-arc  $
//
//   Rev 1.11   Jul 28 2011 16:20:30   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.10   Mar 12 2010 13:17:46   mmodi
//Show specific table text for International
//Resolution for 5451: TD Extra - Updates following testing by DfT
//
//   Rev 1.9   Feb 25 2010 11:02:58   mmodi
//Set accessability modes to show on Accessabilty page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 23 2010 13:29:08   mmodi
//Updated to pass FindAMode value into the overview control
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 22 2010 08:24:46   rbroddle
//Updated to initialise overview control with new flag for TDExtra
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 17 2010 15:13:54   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Feb 16 2010 11:16:12   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Jan 12 2009 14:50:34   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 04 2008 15:39:20   mmodi
//Added extra note
//Resolution for 4829: Del 10 - City to city shows "Unable to find journeys" error message
//
//   Rev 1.2   Mar 31 2008 13:24:54   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements
//Initial revision

//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;

using JC = TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.Web.Templates
{
    public partial class JourneyOverview : TDPage
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

        #region Controls

        protected ErrorDisplayControl errorDisplayControl;
        protected Panel errorMessagePanel;
        protected AmendSaveSendControl amendSaveSendControl;
        protected ResultsTableTitleControl resultsTableTitleControlOutward;
        protected ResultsTableTitleControl resultsTableTitleControlReturn;
        protected FindOverviewResultControl findOverviewResultTableControlOutward;
        protected FindOverviewResultControl findOverviewResultTableControlReturn;

        protected Image imageMainContentSkipLink1;
        protected Image imageJourneyButtonsSkipLink1;
        protected Image imageOutwardJourneysSkipLink1;
        protected Image imageReturnJourneysSkipLink1;

        protected Label labelJourneyOptionsTableDescription;

        protected Panel panelOutwardJourneysSkipLink1;

        

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
		public JourneyOverview()
		{
			this.pageId = PageId.JourneyOverview;
        }
        #endregion

        #region Page_Load
        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            tdSessionManager = TDSessionManager.Current;

            SetupControls();

            // Set the title
            this.PageTitle = GetResource("JourneyOverview.PageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            #region Result Errors

            errorMessagePanel.Visible = false;
            errorDisplayControl.Visible = false;

            if (!showItinerary)
            {
                ITDJourneyResult res = tdSessionManager.JourneyResult;
                if (res != null)
                {
                    ArrayList errorsList = new ArrayList();

                    foreach (CJPMessage mess in res.CJPMessages)
                    {
                        if (mess.Type == ErrorsType.Warning)
                        {
                            errorDisplayControl.Type = ErrorsDisplayType.Warning;
                        }

                        string text = mess.MessageText;
                        if (text == null || text.Length == 0)
                        {
                            text = GetResource(mess.MessageResourceId);
                        }
                        
                        #warning City to city - Future journeys
                        //Occasionally requests are randonly made to the ITP instead of CJP, this returns a 
                        //future date journeys error.
                        //This is incorrect as the date submitted is correct (i.e. < 3 months), 
                        //therefore supress this message.
                        //This check must be removed once the ITP - CJP problem has been removed
                        if (mess.MessageResourceId != JourneyControlConstants.JourneyWebTooFarAhead)
                        {
                            errorsList.Add(text);
                        }
                    }

                    errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

                    errorDisplayControl.ReferenceNumber = tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString();

                    if (errorDisplayControl.ErrorStrings.Length > 0)
                    {
                        errorMessagePanel.Visible = true;
                        errorDisplayControl.Visible = true;

                    }

                    // Clear the error messages in the result
                    res.ClearMessages();

                }
            }
            #endregion

            //added for white labelling
            ConfigureLeftMenu("JourneyOverview.clientLink.BookmarkTitle", "JourneyOverview.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyOverview);
            expandableMenuControl.AddExpandedCategory("Related links");
        }

        /// <summary>
        /// Page Prerender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupText();

            SetupSkipLinksAndScreenReaderText();

            // Prerender setup for the AmendSaveSend control and its child controls
            PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

            plannerOutputAdapter.AmendSaveSendControlPreRender(amendSaveSendControl);

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
                findOverviewResultTableControlOutward.Initialise(false, true, outwardArriveBefore, tdSessionManager.FindAMode);
            if (returnExists)
                findOverviewResultTableControlReturn.Initialise(false, false, returnArriveBefore, tdSessionManager.FindAMode);
            

            // Set up correct mode for footnotes control
            if (tdSessionManager.IsFindAMode)
            {
                if (FindFareInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
                {
                    footnotesControl.CostBasedResults = true;
                }
                footnotesControl.Mode = tdSessionManager.FindAMode;
                footnotesControl.controlPageID = this.PageId;

                // Specify the accessability modes to be displayed
                if (tdSessionManager.FindAMode == FindAMode.International)
                {
                    footnotesControl.AccessabilityModeTypes = GetModesUsed();
                }
            }
            
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
        /// Displays Outward and Return panels based on journeys exist
        /// </summary>
        private void SetControlVisibility()
        {
            outwardPanel.Visible = outwardExists;
            returnPanel.Visible = returnExists;

            findOverviewResultsTableNotes.Visible = (outwardExists || returnExists);

            amendSaveSendControl.Visible = tdSessionManager.FindAMode != FindAMode.International;

            if (tdSessionManager.FindAMode == FindAMode.International)
            {
                theJourneyChangeSearchControl.PrinterFriendlyPageButton.Visible = (outwardExists || returnExists);

                findOverviewResultsTableNotes.Visible = (outwardExists || returnExists);

                footnotesControl.Visible = (outwardExists || returnExists);

                JourneyPlannerOutputTitleControl1.Visible = (outwardExists || returnExists);

                labelInstructions.Visible = (outwardExists || returnExists);

                
            }
        }

        /// <summary>
        /// Sets the label text values
        /// </summary>
        private void SetupText()
        {
            labelInstructions.Text = GetResource("JourneyOverview.labelInstructions.Text");

            if (tdSessionManager.FindAMode == FindAMode.International)
            {
                // Specific text for international
                findOverviewResultsTableNotes.Text = GetResource("JourneyOverview.labelResultsTableNotes.International.Text");
            }
            else
            {
                findOverviewResultsTableNotes.Text = GetResource("JourneyOverview.labelResultsTableNotes.Text");
            }
        }

        /// <summary>
        /// Screen reader labels and images text
        /// </summary>
        private void SetupSkipLinksAndScreenReaderText()
        {
            // Setup gif resource for images (1 invisible image for all skip links)
            string SkipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
            imageMainContentSkipLink1.ImageUrl = SkipLinkImageUrl;
            imageJourneyButtonsSkipLink1.ImageUrl = SkipLinkImageUrl;
            imageOutwardJourneysSkipLink1.ImageUrl = SkipLinkImageUrl;
            imageReturnJourneysSkipLink1.ImageUrl = SkipLinkImageUrl;

            imageMainContentSkipLink1.AlternateText =
                GetResource("JourneySummary.imageMainContentSkipLink.AlternateText");

            string journeyButtonsSkipLinkText = GetResource("JourneySummary.imageJourneyButtonsSkipLink.AlternateText");

            imageJourneyButtonsSkipLink1.AlternateText = journeyButtonsSkipLinkText;

            labelJourneyOptionsTableDescription.Text = GetResource("JourneySummary.labelJourneyOptionsTableDescription.Text");


            // Only show the link to outward journeys portion of the screen if we have outward journeys on screen
            if (outwardExists)
            {
                panelOutwardJourneysSkipLink1.Visible = true;
                imageOutwardJourneysSkipLink1.AlternateText =
                    GetResource("JourneySummary.imageOutwardJourneysSkipLink1.AlternateText");
            }

            // Only show the link to return journeys portion of the screen if we have return journeys on screen
            if (returnExists)
            {
                panelReturnJourneysSkipLink1.Visible = true;
                imageReturnJourneysSkipLink1.AlternateText =
                    GetResource("JourneySummary.imageReturnJourneysSkipLink1.AlternateText");

            }
        }

        /// <summary>
        /// Method which identifies the modes used for the journeys currently displayed
        /// </summary>
        /// <returns></returns>
        private ModeType[] GetModesUsed()
        {
            List<ModeType> modeTypes = new List<ModeType>();

            ITDJourneyResult result = TDItineraryManager.Current.JourneyResult;

            if (result != null)
            {
                #region Outward journeys
                if (result.OutwardPublicJourneyCount > 0)
                {
                    foreach (JC.PublicJourney pj in result.OutwardPublicJourneys)
                    {
                        if (pj != null)
                        {
                            ModeType[] modes = pj.GetUsedModes();

                            foreach (ModeType mt in modes)
                            {
                                if (!modeTypes.Contains(mt))
                                {
                                    modeTypes.Add(mt);
                                }
                            }
                        }
                    }
                }

                if (result.OutwardRoadJourneyCount > 0)
                {
                    if (!modeTypes.Contains(ModeType.Car))
                    {
                        modeTypes.Add(ModeType.Car);
                    }
                }

                #endregion

                #region Return journeys
                if (result.ReturnPublicJourneyCount > 0)
                {
                    foreach (JC.PublicJourney pj in result.ReturnPublicJourneys)
                    {
                        if (pj != null)
                        {
                            ModeType[] modes = pj.GetUsedModes();

                            foreach (ModeType mt in modes)
                            {
                                if (!modeTypes.Contains(mt))
                                {
                                    modeTypes.Add(mt);
                                }
                            }
                        }
                    }
                }

                if (result.ReturnRoadJourneyCount > 0)
                {
                    if (!modeTypes.Contains(ModeType.Car))
                    {
                        modeTypes.Add(ModeType.Car);
                    }
                }

                #endregion

            }
            return modeTypes.ToArray();
        }

        #endregion
    }
}
