// *********************************************** 
// NAME                 : EBCJourneyMap.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 15/09/2008
// DESCRIPTION          : Journey map page for Enivronmental Benefits Calculator (EBC) planner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/EBCJourneyMap.aspx.cs-arc  $
//
//   Rev 1.1   Oct 12 2009 09:11:52   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 12 2009 08:40:10   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Sep 21 2009 15:01:40   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.SuggestionLinkService;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Journey map page for find EBC
    /// </summary>
    public partial class EBCJourneyMap : TDPage
    {
        #region Private members
        // Session variables
        private ITDSessionManager sessionManager;
        private TDJourneyParametersMulti journeyParameters;
        private InputPageState inputPageState;
        private FindEBCPageState findEBCPageState;

        private bool outwardExists = false;


        #endregion

        #region Constructor
        /// <summary>
		/// Default Constructor
		/// </summary>
		public EBCJourneyMap()
		{
            pageId = PageId.EBCJourneyMap;
        }
        #endregion

        #region Page_Init, Page_Load, Page_PreRender
        /// <summary>
        /// Performs page initialisation including event wiring.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            ebcMapControlOutward.ButtonShowDetails.Click += new EventHandler(ButtonShowDetails_Click);
        }

                
        /// <summary>
        /// Page Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the session values needed by this page
            sessionManager = TDSessionManager.Current;
            findEBCPageState = (FindEBCPageState)sessionManager.FindPageState;
            journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = sessionManager.InputPageState;

            LoadHelp();

            LoadResources();

            InitialiseControls();

            SetControlVisibility();

            LoadLeftHandNavigation();

            SetupSkipLinksAndScreenReaderText();
        }

        /// <summary>
        /// Page PreRender event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetPrintableControl();
        }

        #endregion

        #region  Page Control Event Handlers
        private void ButtonShowDetails_Click(object sender, EventArgs e)
        {
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.EBCJourneyDetails; 
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Sets the visibility of the controls on the page
        /// </summary>
        private void SetControlVisibility()
        {
            DisplayOutwardComponents(outwardExists);
        }

        /// <summary>
        /// Sets the visibilities of the "Outward" components.
        /// </summary>
        private void DisplayOutwardComponents(bool visible)
        {
            outwardPanel.Visible = visible;
            ebcMapControlOutward.Visible = visible;
        }

        /// <summary>
        /// Establish whether we have any results
        /// </summary>
        private void DetermineStateOfResults()
        {
            //check for road journey result
            ITDJourneyResult result = sessionManager.JourneyResult;
            if (result != null)
            {
                outwardExists = ((result.OutwardRoadJourneyCount) > 0) && result.IsValid;
            }
        }

        /// <summary>
        /// Sets up the Help text for the page
        /// </summary>
        private void LoadHelp()
        {
            journeyChangeSearchControl.HelpUrl = GetResource("EBCJourneyDetails.HelpPageUrl");
        }


        /// <summary>
        /// Initialises controls on page with page and journey details
        /// </summary>
        private void InitialiseControls()
        {
            DetermineStateOfResults();

            journeysSearchedControl.UseRouteFoundForHeading = true;
            
            // Setup the EBC details control
            if (outwardExists)
            {
                ebcMapControlOutward.Initialise(sessionManager.JourneyResult.OutwardRoadJourney(), true, journeyParameters,
                    sessionManager.JourneyViewState, findEBCPageState.EnvironmentalBenefits);
                ebcMapControlOutward.RoadUnits = inputPageState.Units;
            }
            

        }

        /// <summary>
        /// Loads the left hand navigation on the page
        /// </summary>
        private void LoadLeftHandNavigation()
        {
            // Navigation links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);

            // Related links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindEBCInput);
            expandableMenuControl.AddExpandedCategory("Related links");            
        }

        /// <summary>
        /// Loads text and images on the page
        /// </summary>
        private void LoadResources()
        {
            PageTitle = GetResource("EBCPlanner.JourneyMaps.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            ebcMapControlOutward.ButtonShowDetails.Text = GetResource("ShowDetails.Text");
        }

        /// <summary>
        /// Sets the text for the skip to links (for screenreader browsers).
        /// </summary>
        private void SetupSkipLinksAndScreenReaderText()
        {
            // Setup gif resource for images (1 invisible image for all skip links)
            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");

            imageMapSkipLink.ImageUrl = skipLinkImageUrl;
            imageMapSkipLink.AlternateText = GetResource("EBCPlanner.JourneyMaps.imageJourneySkipLink.AlternateText");
        }

        /// <summary>
        /// Sets up the printable control with the querystring params needed
        /// </summary>
        private void SetPrintableControl()
        {
            string printerUnits = inputPageState.Units.ToString();

            if (printerUnits == "kms")
                journeyChangeSearchControl.PrinterFriendlyPageButton.UrlParams += "&units=kms";
            else
                journeyChangeSearchControl.PrinterFriendlyPageButton.UrlParams += "&units=miles";
        }

        #endregion
    }
}
