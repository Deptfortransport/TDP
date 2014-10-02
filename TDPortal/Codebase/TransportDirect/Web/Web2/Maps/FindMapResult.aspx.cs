// *********************************************** 
// NAME                 : FindMapResult.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 28/10/2009
// DESCRIPTION          : Result Page for Find a map
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Maps/FindMapResult.aspx.cs-arc  $ 
//
//   Rev 1.6   Dec 10 2009 15:50:10   mmodi
//Hide printer friendly button when javascript disabled.
//
//   Rev 1.5   Nov 20 2009 09:28:34   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Nov 18 2009 11:20:44   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Nov 11 2009 16:54:06   apatel
//Added back button and its click event handler
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Nov 11 2009 11:50:20   apatel
//updated for printer friendly control setup changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 05 2009 14:56:26   apatel
//mapping enhancement code changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 02 2009 17:52:32   mmodi
//Initial revision.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.SuggestionLinkService;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Result Page for Find a map
    /// </summary>
    public partial class FindMapResult : TDPage
    {
        #region Variables and controls

        // Session variables
        private ITDSessionManager sessionManager;
        private TDJourneyParametersMulti journeyParameters;
        private InputPageState inputPageState;

        #region Map Location Search Variables

        // Declaration of search/location object members
        private LocationSearch mapSearch;
        private TDLocation mapLocation;
        private LocationSelectControlType mapLocationControlType;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
		/// Default Constructor
		/// </summary>
		public FindMapResult()
		{
            pageId = PageId.FindMapResult;
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
            buttonNewSearch.Click += new EventHandler(buttonNewSearch_Click);

            commandBack.Click += new EventHandler(commandBack_Click);
        }
   
        /// <summary>
        /// Page Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Set up the session variables
            LoadSessionVariables();

            LoadSessionLocationVariables();

            InitialiseControls();

            LoadHelp();
        }

        /// <summary>
        /// Page PreRender event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetControlVisibility();

            LoadResources();

            LoadLeftHandNavigation();

            SetupSkipLinksAndScreenReaderText();

            SetPrintableControl();

            CheckJavascriptEnabled();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the left hand navigation on the page
        /// </summary>
        private void LoadLeftHandNavigation()
        {
            // Navigation links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);

            // Related links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyPlannerLocationMap);
            expandableMenuControl.AddExpandedCategory("Related links");
        }

        /// <summary>
        /// Loads text and images on the page
        /// </summary>
        private void LoadResources()
        {
            PageTitle = GetResource("FindMapInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            buttonNewSearch.Text = GetResource("FindMapResult.buttonNewSearch.Text");

            commandBack.Text = GetResource("FindMapResult.commandBack.Text");

            labelMap.Text = GetResource("FindMapResult.labelMap.Text");
            labelSelectedLocation.Text = mapLocation.Description;
        }

        /// <summary>
        /// Sets up the Help text for the page
        /// </summary>
        private void LoadHelp()
        {
            pageHelpButton.HelpUrl = GetResource("FindMapResult.HelpPageUrl");
        }

        /// <summary>
        /// Sets the text for the skip to links (for screenreader browsers).
        /// </summary>
        private void SetupSkipLinksAndScreenReaderText()
        {
            // Setup gif resource for images (1 invisible image for all skip links)
            string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");

            imageMapSkipLink.ImageUrl = skipLinkImageUrl;
            imageMapSkipLink.AlternateText = GetResource("FindMapResult.imageMapSkipLink.AlternateText");
        }

        /// <summary>
        /// Sets up the printable control with the querystring params needed
        /// </summary>
        private void SetPrintableControl()
        {
            PrintableButtonHelper printHelper = new PrintableButtonHelper(mapFindControl.MapID,mapFindControl.MapSymbolsSelectID);
            printerFriendlyPageButton.PrintButton.OnClientClick = printHelper.GetClientScript();
        }

        /// <summary>
        /// Sets the visibility of the controls on the page
        /// </summary>
        private void SetControlVisibility()
        {
            // Only display printer friendly button if location is valid
            printerFriendlyPageButton.Visible = mapLocation.Status == TDLocationStatus.Valid;
        }

        /// <summary>
        /// Initialises controls on page with page and journey details
        /// </summary>
        private void InitialiseControls()
        {
            // Check for a map location and initialise the map control
            if ((mapLocation != null) && (mapLocation.Status == TDLocationStatus.Valid))
            {
                mapFindControl.Initialise(mapLocation, mapSearch, GetMapLocationModes());
            }

            if (TDSessionManager.Current.IsStopInformationMode)
            {
                commandBack.Visible = true;
            }
        }

        /// <summary>
        /// Returns MapLocationMode array to initialise map with start, end and via modes
        /// Returns Start and End modes by default
        /// </summary>
        /// <returns>MapLocationMode array</returns>
        private MapLocationMode[] GetMapLocationModes()
        {
            MapLocationMode[] mapLocationModes = new MapLocationMode[2] {MapLocationMode.Start, MapLocationMode.End};
            if (inputPageState.MapType == CurrentLocationType.PrivateVia
                || inputPageState.MapType == CurrentLocationType.PublicVia
                || inputPageState.MapType == CurrentLocationType.CycleVia)
            {
                mapLocationModes = new MapLocationMode[3] {MapLocationMode.Start, MapLocationMode.Via, MapLocationMode.End};
            }

            if (inputPageState.JourneyInputReturnStack.Count > 0)
            {
                if (inputPageState.JourneyInputReturnStack.Contains(PageId.ParkAndRideInput))
                {
                    mapLocationModes = new MapLocationMode[1] { MapLocationMode.Start};
                }
            }

            return mapLocationModes;
        }

        /// <summary>
        /// Sets the page session variables
        /// </summary>
        private void LoadSessionVariables()
        {
            // Get the session values needed by this page
            sessionManager = TDSessionManager.Current;
            journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = sessionManager.InputPageState;
        }

        /// <summary>
        /// Loads location variables from the session into page location variables
        /// </summary>
        private void LoadSessionLocationVariables()
        {
            mapSearch = inputPageState.MapLocationSearch;
            mapLocation = inputPageState.MapLocation;
            mapLocationControlType = inputPageState.MapLocationControlType;
        }

        /// <summary>
        /// Checks if javascript is diabled and shows info message if disabled for map
        /// </summary>
        private void CheckJavascriptEnabled()
        {
            if (!this.IsJavascriptEnabled)
            {
                errorDisplayControl.Type = ErrorsDisplayType.Custom;
                errorDisplayControl.ErrorsDisplayTypeText = GetResource("MapControl.JavaScriptDisabled.Heading.Text");

                List<string> errors = new List<string>();

                errors.Add(GetResource("MapControl.JavaScriptDisabled.Description.Text"));
                errors.Add(GetResource("MapControl.JavaScriptDisabled.FindMapResult.Text"));
                errorDisplayControl.ErrorStrings = errors.ToArray();
                
                panelErrorDisplayControl.Visible = true;
                errorDisplayControl.Visible = true;
                mapFindControl.Visible = false;

                // Hide printer friendly button
                printerFriendlyPageButton.Visible = false;
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// NewSearch button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNewSearch_Click(object sender, EventArgs e)
        {
            // Reset the map location objects and save to session
            mapLocation = new TDLocation();
            mapSearch = new LocationSearch();
            mapLocationControlType = new LocationSelectControlType(ControlType.NewLocation);
            
            mapSearch.SearchType = SearchType.MainStationAirport;
            mapLocation.SearchType = SearchType.MainStationAirport;
                        
            inputPageState.MapLocationSearch = mapSearch;
            inputPageState.MapLocation = mapLocation;
            inputPageState.MapLocationControlType = mapLocationControlType;

            // Return back to the input page
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindMapInputDefault;
        }

        /// <summary>
        /// CommandBack button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandBack_Click(object sender, EventArgs e)
        {
            TransitionEvent te = TransitionEvent.FindMapInputDefault;

            if (TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count > 0)
            {
                PageId returnPage = (PageId)TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Pop();

                switch (returnPage)
                {
                    case PageId.StopInformation:
                        te = TransitionEvent.StopInformation;
                        break;
                    
                    default:
                        te = TransitionEvent.FindMapInputDefault;
                        break;
                }
            }

            
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = te;
            
        }

        #endregion
    }
}
