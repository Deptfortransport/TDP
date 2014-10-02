// *********************************************** 
// NAME                 : EBCJourneyDetails.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 15/09/2008
// DESCRIPTION          : Journey details page for Enivronmental Benefits Calculator (EBC) planner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/EBCJourneyDetails.aspx.cs-arc  $
//
//   Rev 1.7   Dec 11 2009 14:54:38   apatel
//EBC map slider bar css 
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Dec 02 2009 15:58:42   mmodi
//Added code to show map view when coming back from car park/stop information page
//
//   Rev 1.5   Nov 23 2009 13:01:52   apatel
//Updated to log page entry events
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Nov 16 2009 17:07:06   apatel
//Updated for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Oct 12 2009 14:20:02   mmodi
//Hide printer friendly button if journey fails
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 09:11:50   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 12 2009 08:40:08   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 06 2009 14:19:20   mmodi
//Updated for EBC
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Sep 21 2009 15:01:38   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Journey details page for find EBC
    /// </summary>
    public partial class EBCJourneyDetails : TDPage
    {
        #region Private members

        private const string scriptCommonAPI = "Common";

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
		public EBCJourneyDetails()
		{
            pageId = PageId.EBCJourneyDetails;
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
            this.ebcAllDetailsControlOutward.ButtonShowMap.Click += new EventHandler(buttonShowMapOutward_Click);
            this.ebcMapControlOutward.ButtonShowDetails.Click += new EventHandler(ButtonShowDetails_Click);
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

            DisplayJourneyErrorMessages();

            InitialiseControls();

            SetControlVisibility();

            LoadHelp();
        }

        /// <summary>
        /// Page PreRender event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            

            LoadResources();

            LoadLeftHandNavigation();

            SetupSkipLinksAndScreenReaderText();

            RegisterJavascript();

            SetPrintableControl();
        }

        
        #endregion

        #region Private methods

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
            PageTitle = GetResource("EBCPlanner.JourneyDetails.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            ebcAllDetailsControlOutward.ButtonShowMap.Text = GetResource("ShowMap.Text");
            ebcMapControlOutward.ButtonShowDetails.Text = GetResource("ShowDetails.Text");
        }

        /// <summary>
        /// Sets up the Help text for the page
        /// </summary>
        private void LoadHelp()
        {
            journeyChangeSearchControl.HelpUrl = GetResource("EBCJourneyDetails.HelpPageUrl");
        }

        /// <summary>
        /// Sets the text for the skip to links (for screenreader browsers).
        /// </summary>
        private void SetupSkipLinksAndScreenReaderText()
        {
            // Setup gif resource for images (1 invisible image for all skip links)
            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");

            imageJourneySkipLink.ImageUrl = skipLinkImageUrl;
            imageJourneySkipLink.AlternateText = GetResource("EBCPlanner.EBCJourneyDetails.imageJourneySkipLink.AlternateText");
        }

        /// <summary>
        /// Sets up the printable control with the querystring params needed
        /// </summary>
        private void SetPrintableControl()
        {
            string printerUnits = inputPageState.Units.ToString();

            // Initialise only for return map
            PrintableButtonHelper printHelper = new PrintableButtonHelper(
                ebcMapControlOutward.MapControl.MapId,
                ebcMapControlOutward.MapControl.MapSymbolsSelectId);
                        

            if (printerUnits == "kms")
                journeyChangeSearchControl.PrinterFriendlyPageButton.UrlParams += "&units=kms";
            else
                journeyChangeSearchControl.PrinterFriendlyPageButton.UrlParams += "&units=miles";

            // Don't show the printer button if there is no journ
            if (outwardExists)
            {
                journeyChangeSearchControl.PrinterFriendlyPageButton.Visible = true;
                if (printHelper != null)
                {
                    // Only attach if maps are visible
                    journeyChangeSearchControl.PrinterFriendlyPageButton.PrintButton.OnClientClick = printHelper.GetClientScript();
                    
                }
            }
            else
            {
                journeyChangeSearchControl.PrinterFriendlyPageButton.Visible = false;
            }
        }

        /// <summary>
        /// Sets the visibility of the controls on the page
        /// </summary>
        private void SetControlVisibility()
        {
            DisplayOutwardComponents(outwardExists);
        }

        /// <summary>
        /// Displays any error messages returned following a journey plan
        /// </summary>
        private void DisplayJourneyErrorMessages()
        {
            errorMessagePanel.Visible = false;
            errorDisplayControl.Visible = false;
            
            ITDJourneyResult tdJourneyResult = sessionManager.JourneyResult;

            if (tdJourneyResult != null)
            {
                ArrayList errorsList = new ArrayList();

                foreach (CJPMessage message in tdJourneyResult.CJPMessages)
                {
                    if (message.Type == ErrorsType.Warning)
                    {
                        errorDisplayControl.Type = ErrorsDisplayType.Warning;
                    }

                    string text = message.MessageText;

                    if (string.IsNullOrEmpty(text))
                    {
                        text = GetResource(message.MessageResourceId);
                    }
                    
                    errorsList.Add(text);
                }

                errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

                errorDisplayControl.ReferenceNumber = tdJourneyResult.JourneyReferenceNumber.ToString();

                if (errorDisplayControl.ErrorStrings.Length > 0)
                {
                    errorMessagePanel.Visible = true;
                    errorDisplayControl.Visible = true;
                }

                // Clear the error messages in the result
                tdJourneyResult.ClearMessages();
            }
        }

        /// <summary>
        /// Sets the visibilities of the "Outward" components.
        /// </summary>
        private void DisplayOutwardComponents(bool visible)
        {
            outwardPanel.Visible = visible;
            ebcAllDetailsControlOutward.Visible = visible;
            outwardMapPanel.Visible = visible;
            ebcMapControlOutward.Visible = visible;

            // Set the initial display to be map if indicated by session key, should only be set
            // if coming back from the car park information
            if (TDSessionManager.Current.GetOneUseKey(SessionKey.MapView) != null)
            {
                outwardPanel.Style[HtmlTextWriterStyle.Display] = "none";
                outwardMapPanel.Style[HtmlTextWriterStyle.Display] = string.Empty;
            }
            else
            {
                outwardPanel.Style[HtmlTextWriterStyle.Display] = string.Empty;
                outwardMapPanel.Style[HtmlTextWriterStyle.Display] = "none";
            }
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
        /// Initialises controls on page with page and journey details
        /// </summary>
        private void InitialiseControls()
        {
            DetermineStateOfResults();

            journeysSearchedControl.UseRouteFoundForHeading = true;

            // Setup the EBC details control
            if (outwardExists)
            {
                ebcAllDetailsControlOutward.Initialise(sessionManager.JourneyResult.OutwardRoadJourney(), true, journeyParameters,
                    sessionManager.JourneyViewState, findEBCPageState.EnvironmentalBenefits);
                ebcAllDetailsControlOutward.RoadUnits = inputPageState.Units;

                ebcMapControlOutward.Initialise(sessionManager.JourneyResult.OutwardRoadJourney(), true, journeyParameters,
                    sessionManager.JourneyViewState, findEBCPageState.EnvironmentalBenefits);
                ebcMapControlOutward.RoadUnits = inputPageState.Units;

            }

        }

        /// <summary>
        /// Registers page and page controls javascript
        /// </summary>
        private void RegisterJavascript()
        {
            // Register the scripts needed only if user has Javascript enabled
            TDPage thePage = this.Page as TDPage;

            if (thePage != null && thePage.IsJavascriptEnabled)
            {
                // Get the global script repository
                ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                string toggleVisibiltyScript = "setSliderBox();toggleVisibility('" + outwardPanel.ClientID + "'); toggleVisibility('" + outwardMapPanel.ClientID + "'); return false;";

                ebcMapControlOutward.ButtonShowDetails.OnClientClick = string.Format("ESRIUKTDPAPI.logPageEntryEvent('{0}');{1}", PageId.EBCJourneyMap ,toggleVisibiltyScript);

                ebcAllDetailsControlOutward.ButtonShowMap.OnClientClick = string.Format("ESRIUKTDPAPI.logPageEntryEvent('{0}');{1}", PageId.EBCJourneyDetails, toggleVisibiltyScript);

                // Register the mapping api call script
                thePage.ClientScript.RegisterClientScriptBlock(this.GetType(), scriptCommonAPI, repository.GetScript(scriptCommonAPI, thePage.JavascriptDom));
            }
            
        }
        #endregion

        #region Event handlers

        /// <summary>
        /// Event handler for the button show map click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowMapOutward_Click(object sender, EventArgs e)
        {
           // TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.EBCJourneyMap; 

            if (!this.IsJavascriptEnabled)
            {
                errorDisplayControl.Type = ErrorsDisplayType.Custom;
                errorDisplayControl.ErrorsDisplayTypeText = GetResource("MapControl.JavaScriptDisabled.Heading.Text");

                List<string> errors = new List<string>();

                errors.Add(GetResource("MapControl.JavaScriptDisabled.Description.Text"));
                errorDisplayControl.ErrorStrings = errors.ToArray();

                errorMessagePanel.Visible = true;
                errorDisplayControl.Visible = true;
                ebcMapControlOutward.Visible = true;
                outwardMapPanel.Style[HtmlTextWriterStyle.Display] = string.Empty;
                outwardPanel.Visible = false;

                // logging the page entry event for EBCJourneyMap page as we not going on the ebcjourneymap page anymore
                PageEntryEvent logPage = new PageEntryEvent(PageId.EBCJourneyMap, Session.SessionID, TDSessionManager.Current.Authenticated);
                Logger.Write(logPage);
            }
            
        }

        /// <summary>
        /// Event handler for the button show detail click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShowDetails_Click(object sender, EventArgs e)
        {
            outwardPanel.Visible = true;
            outwardMapPanel.Visible = false;

            // As we not coming from EBCJourneyMap page and we just doing a post back,
            // logging pageevent for EBCJourneyDetail page
            PageEntryEvent logPage = new PageEntryEvent(pageId, Session.SessionID, TDSessionManager.Current.Authenticated);
            Logger.Write(logPage);
        }

        #endregion
    }
}
