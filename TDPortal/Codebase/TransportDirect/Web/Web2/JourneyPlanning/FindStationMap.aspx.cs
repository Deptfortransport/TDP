// *********************************************** 
// NAME                 : FindStationMap.aspx 
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 11/05/2004 
// DESCRIPTION  : Page displaying results of found stations with a MAP
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindStationMap.aspx.cs-arc  $ 
//
//   Rev 1.13   Jun 09 2010 09:05:14   apatel
//Updated to remove javascript eschape characters "'" and "/"
//Resolution for 5550: FindNearest and FindCarPark break with "'" in the content
//
//   Rev 1.12   Jun 08 2010 15:37:52   apatel
//Updated to remove Javascript escape characters from map location point content.
//Resolution for 5550: FindNearest and FindCarPark break with "'" in the content
//
//   Rev 1.11   Mar 29 2010 16:39:26   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.10   Mar 11 2010 12:37:02   mmodi
//Pass in a content string to the map location symbols, to allow popup to be shown at any zoom level and with overlays turned off
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.9   Jan 19 2010 13:21:00   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.8   Dec 07 2009 11:21:48   mmodi
//Corrected scroll to map issue in IE
//
//   Rev 1.7   Nov 20 2009 09:27:46   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Nov 19 2009 12:08:52   mmodi
//Added Map stylesheet
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Nov 18 2009 12:30:12   mmodi
//Updated for new mapping
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Nov 16 2009 17:07:08   apatel
//Updated for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Dec 17 2008 15:52:02   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:24:36   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:36   mturner
//Initial revision.
//
//   Rev 1.35   May 11 2007 14:54:22   nrankin
//Code change to remove Gazetteer postfixes from location name on maps
//Resolution for 4406: Gaz post fixes should not be displayed on maps
//
//   Rev 1.34   Jan 08 2007 15:49:36   jfrank
//Any <modes> have been changed to Main <modes>, this will break previous pseudo naptans CCN's.  This change means this will not occur.
//Resolution for 4333: Gaz Improvement Workshop - Actions 6, 9, 17 - Gaz Config and Naptan Config Changes
//
//   Rev 1.33   Dec 28 2006 10:53:04   mturner
//Resolution for IR 4326 :- Pseudo locations still being displayed for some journeys
//
//   Rev 1.32   Oct 06 2006 16:39:08   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.31.1.1   Sep 29 2006 15:39:52   esevern
//Added check for car parking functionality switched on before setting toggling of car parking visibility and refreshing map
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4191: Car Parking: Configurable switch should be available to display/hide car parking functionality
//
//   Rev 1.31.1.0   Sep 19 2006 18:04:18   mmodi
//Added code to display car park icons when selected
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4170: Car Parking: Car park symbol not shown on PT journey map
//
//   Rev 1.31   Jun 01 2006 08:42:16   mmodi
//IR4105: Added code to repopulate Select New Location dropdown list (when user returns back from Help page)
//Resolution for 4105: Del 8.2 - Select new location map feature dropdown values are lost
//
//   Rev 1.30   Apr 04 2006 14:14:00   kjosling
//Merged for stream 0034
//
//   Rev 1.29.1.0   Mar 29 2006 17:49:46   RWilby
//Fixed issue identified during the Map symbol update work.
//Resolution for 3715: Map Symbols
//
//   Rev 1.29   Feb 23 2006 19:30:30   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.28   Feb 21 2006 11:42:28   aviitanen
//Merge from stream0009 
//
//   Rev 1.27   Feb 10 2006 15:09:10   build
//Automatically merged from branch for stream3180
//
//   Rev 1.26.2.0   Nov 29 2005 18:43:52   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.26   Nov 08 2005 19:23:34   ralonso
//Automatically merged for stream 2816
//
//   Rev 1.25.1.0   Oct 11 2005 12:11:22   RGriffith
//TD089 ES020 Image Button Replacement
//
//   Rev 1.27   Oct 06 2005 16:43:28   rGriffith
//Replaced printer button hyperlink/image to use the PrinterFriendlyPageButton control
//
//   Rev 1.26   Oct 06 2005 13:43:28   rGriffith
//Updated imagebuttons to become TDButton controls
//
//   Rev 1.25   Sep 10 2004 16:43:28   passuied
//Set the Map layer "Motorways labels" visibility to false for better visibility
//Resolution for 1285: Find A Flight Airport numbers are obscurred
//
//   Rev 1.24   Sep 01 2004 18:16:22   passuied
//changed the url pointed to by the printer link to be the PrintableFindStationMap page
//Resolution for 1450: Find nearest station/aiport printer friendly page no map
//
//   Rev 1.23   Aug 18 2004 14:19:24   passuied
//Use of non duplicated code to Get transitionevent from FindAMode mode
//
//   Rev 1.22   Aug 13 2004 14:29:10   passuied
//Changes for FindA station distinct error message
//
//   Rev 1.21   Aug 02 2004 15:36:00   jbroome
//IR 1252 - Tick All Check box retains value after postback.
//
//   Rev 1.20   Jul 29 2004 15:43:06   passuied
//Updated back button redirection for Del6.1
//
//   Rev 1.19   Jul 27 2004 14:03:16   passuied
//FindStation Del6.1 :Finalised version 
//
//   Rev 1.18   Jul 26 2004 20:23:58   passuied
//Changes to implement AmendSeach Functionality. Created and Amend mode in the tristate to enable the display of a valid location inside the locationUnspecified control.
//We send this mode when a one use session key has been set by a click on AmendSearch button.
//Also tweak in toFromLocationControl to display the to and from location/station controls correctly
//
//   Rev 1.17   Jul 23 2004 17:42:00   passuied
//FindStation 6.1. Labels and text updates
//
//   Rev 1.16   Jul 23 2004 11:48:36   passuied
//Changes to add GetResource Method in TDPage and TDUserControl to ease access to resources. Also removal of local GetResouce in controls and pages
//
//   Rev 1.15   Jul 21 2004 10:51:38   passuied
//Re work for integration with FindStation del6.1. Working. Needs work on resources
//
//   Rev 1.14   Jul 14 2004 13:00:28   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.13   Jul 12 2004 14:13:48   passuied
//use of new property Mode of FindPageState base class
//
//   Rev 1.12   Jun 30 2004 15:43:04   passuied
//Cleaning up
//
//   Rev 1.11   Jun 23 2004 11:23:08   passuied
//addition of help for findStation pages
//
//   Rev 1.10   Jun 10 2004 10:19:04   jgeorge
//Updated for changes to FindFlightPageState and TDJourneyParametersFlight
//
//   Rev 1.9   Jun 09 2004 16:31:02   passuied
//changes to display correct instructions in FindStationMap
//
//   Rev 1.8   Jun 08 2004 10:43:44   passuied
//changed help labels
//
//   Rev 1.7   Jun 04 2004 11:17:16   passuied
//fix for Map Icon after More of Find Info
//
//   Rev 1.6   Jun 03 2004 11:41:46   passuied
//changed display of airports icons on map
//
//   Rev 1.5   Jun 02 2004 16:40:22   passuied
//working version
//
//   Rev 1.4   May 28 2004 17:51:20   passuied
//update as part of FindStation development
//
//   Rev 1.3   May 24 2004 12:12:38   passuied
//checked in to comply with control changes
//
//   Rev 1.2   May 21 2004 15:49:58   passuied
//partly working Find station pages and controls. Check in for backup
//
//   Rev 1.1   May 12 2004 17:47:30   passuied
//compiling check in for FindStation pages and related


using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Page displaying results of found stations with a MAP
	/// </summary>
	public partial class FindStationMap : TDPage
	{
		#region Custom web user controls

		protected TransportDirect.UserPortal.Web.Controls.FindStationResultsTable stationResultsControl;

		#endregion

		#region Page variables (stored in internal viewstate)

		// Indicates if the session data for input has been resetted.
		private bool resetted = false;

        FindStationPageState stationPageState;
		FindPageState pageState;

        #endregion

		#region Resource keys declaration

		private const string RES_TITLE_NOTSTATIONMODE		= "FindStationResult.labelTitle.NotStationMode";
		private const string RES_TITLE_STATIONMODE_NOVALID  = "FindStationResult.labelTitle.StationMode.Valid";
		private const string RES_TITLE_STATIONMODE_VALID	= "FindStationResult.labelTitle.StationMode.Valid"; // same as valid		
		private const string RES_NOTE_NOTSTATIONMODE		= "FindStationResult.labelNote.NotStationMode";
		private const string RES_NOTE_STATIONMODE_NOVALID	= "FindStationResult.labelNote.StationMode.Valid";
		private const string RES_NOTE_STATIONMODE_VALID		= "FindStationResult.labelNote.NotStationMode";
        public const string RES_ERRORMESSAGE_STATIONMODE	= "FindStation.ErrorMessage.StationMode";
		
		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public FindStationMap()
		{
			pageId = PageId.FindStationMap;
		}

		#endregion

		#region Private methods
		/// <summary>
		/// Get property - Returns the TD Map Location currently in the session
		/// </summary>
		private TDLocation GetLocation
		{	
			get
			{
				TDLocation location = null;

				if(TDSessionManager.Current.InputPageState.MapLocation != null)
					location = TDSessionManager.Current.InputPageState.MapLocation ;
		
				return location;
			}
		}

        /// <summary>
        /// Sets up map with the locations showing on the map
        /// </summary>
        private void SetupMap()
        {
            // Find the "relevant location" to determine the initial zoom level.
            TDLocation location = GetLocation;

            // PAS : define minX,Y and max X,Y to zoom to
            OSGridReference minGridReference, maxGridReference;
            minGridReference = new OSGridReference();
            maxGridReference = new OSGridReference();

            bool result = FindStationHelper.DetermineZoomEnvelope(
                stationPageState.StationResultsTable,
                location,
                ref minGridReference,
                ref maxGridReference);


            mapNearestControl.Initialise(minGridReference, maxGridReference, GetMapLocationPoints(),
                stationPageState.CurrentLocation.Description, true);

            stationResultsControl.MapClientID = mapNearestControl.MapId;
            stationResultsControl.MapScrollToID = mapNearestControl.FirstElementId;
            stationResultsControl.IsRowSelectLinkVisible = true;


        }

        /// <summary>
        /// Returns the location points to show on map
        /// </summary>
        /// <returns></returns>
        private MapLocationPoint[] GetMapLocationPoints()
        {
            MapHelper mapHelper = new MapHelper();

            List<MapLocationPoint> mapLocationPoints = new List<MapLocationPoint>();

            string name = TDSessionManager.Current.InputPageState.MapLocation.Description;
            
            // Strip out any sub strings (read from properties DB) denoting pseudo locations 
            IPropertyProvider properties = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

            string railPostFix = properties["Gazetteerpostfix.rail"];
            string coachPostFix = properties["Gazetteerpostfix.coach"];
            string railcoachPostFix = properties["Gazetteerpostfix.railcoach"];

            System.Text.StringBuilder strName = new System.Text.StringBuilder(name);
            strName.Replace(railPostFix, "");
            strName.Replace(coachPostFix, "");
            strName.Replace(railcoachPostFix, "");
            string shortname = strName.ToString();

            mapLocationPoints.Add(new MapLocationPoint(TDSessionManager.Current.InputPageState.MapLocation.GridReference, MapLocationSymbolType.Circle, shortname, false, false));

            // Add station symbols
            int index = 1;
            foreach (TDNaptan naptan in stationPageState.CurrentLocation.NaPTANs)
            {
                // fetch naptan of stations from TDLocation using index in rows
                string iconName = "CIRCLE" + index.ToString(TDCultureInfo.InvariantCulture.NumberFormat);

                // Get the stop name and stop information link to be shown in the info popup for the location shown on map
                // Removed "'" and "\" from stop name as they causing problems when rendered in the esri map api
                string content = "<b>" + naptan.Name.Replace("\\", "").Replace("\'", "\\\'") + "</b><br />" + mapHelper.GetStopInformationLink(naptan.Naptan);

                if (index == 1)
                {
                    // Should show the popup for the first location but currently mapping javascript behaviour isn't quite correct,
                    // i.e. when using the Results table to zoom to the station, this info popup doesnt close.
                    mapLocationPoints.Add(new MapLocationPoint(naptan.GridReference, MapLocationSymbolType.Custom, iconName, " ", true, false, content));
                }
                else
                {
                    mapLocationPoints.Add(new MapLocationPoint(naptan.GridReference, MapLocationSymbolType.Custom, iconName, " ", true, false, content));
                }
                
                index++;
            }

            return mapLocationPoints.ToArray();
        }

		/// <summary>
		/// Set visibility of controls in page
		/// </summary>
		private void SetControlVisibility()
		{
			switch (pageState.Mode)
			{
				case FindAMode.Station:
					// New search and Amend button are displayed when in Station mode only!
					commandNewSearch.Visible = true;
					commandAmendSearch.Visible = true;
					// if no location defined, show travel from / Travel to
					if (stationPageState.LocationFrom.Status != TDLocationStatus.Valid
						&& stationPageState.LocationTo.Status != TDLocationStatus.Valid)
					{
						commandTravelFrom.Visible = true;
						commandTravelTo.Visible = true;
						
						commandNext.Visible = false;
						commandNext2.Visible = false;
						
					}
					else
					{
						commandTravelFrom.Visible = false;
						commandTravelTo.Visible = false;
						
						commandNext.Visible = true;
						commandNext2.Visible = true;
						
					}

					// display error message if not all station types found	
					labelMessage.Visible = stationPageState.NotAllStationTypesFound;
					

					break;
				default:
					commandTravelFrom.Visible = false;
					commandTravelTo.Visible = false;
					
					commandNext.Visible = true;
					commandNext2.Visible = true;
					
					commandNewSearch.Visible = false;
					commandAmendSearch.Visible = false;
					break;
			}	
		}
        		
		/// <summary>
		/// Load page resources
		/// </summary>
		private void LoadResources()
		{
			SetText();

            SetHelpLabel();
		}

		/// <summary>
		/// Set Text for controls in page
		/// </summary>
		private void SetText()
		{
            this.PageTitle = GetResource("FindStationMap.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			#region Static Setting
			
			commandBack.Text = 
				GetResource("FindStationMap.commandBack.Text");
			commandNext.Text = 
				GetResource("FindStationMap.commandNext.Text");
			commandNext2.Text = 
				GetResource("FindStationMap.commandNext2.Text");
			
			commandTravelFrom.Text = 
				GetResource("FindStationMap.commandTravelFrom.Text");
			commandTravelTo.Text = 
				GetResource("FindStationMap.commandTravelTo.Text");
			commandNewSearch.Text = 
				GetResource("FindStationMap.commandNewSearch.Text");
			commandAmendSearch.Text = 
				GetResource("FindStationMap.commandAmendSearch.Text");

			#endregion

			if (pageState.Mode != FindAMode.Station)
			{
				SetTextNotStationMode();
			}
			else
			{
				SetTextStationMode();
			}
		}

		private void SetHelpLabel()
		{
            helpIconSelect.HelpLabelControl = stationResultsLocationControl.HelpLabel;
		}

		private void SetTextNotStationMode()
		{
			string sStationType = FindStationHelper.GetStationTypeString();
			string sDirection = FindStationHelper.GetDirectionString();

			// Set Text for TITLE
			labelResultsTableTitle.Text = string.Format(
				GetResource(RES_TITLE_NOTSTATIONMODE), sStationType, sDirection);

			// Set Text for Note
			labelNote.Text = string.Format(
				GetResource(RES_NOTE_NOTSTATIONMODE), sStationType, sDirection);
		}

		private void SetTextStationMode()
		{
			string sStationType = GetResource(FindStationHelper.RES_STATION)
				+"/"
				+ GetResource(FindStationHelper.RES_AIRPORT);

			// different if no location are valid or if one is valid

			// if one of them is valid
			if (	stationPageState.LocationFrom.Status == TDLocationStatus.Valid
				||	stationPageState.LocationTo.Status == TDLocationStatus.Valid)
			{
				// get the direction string
				string sDirection = FindStationHelper.GetDirectionString();

				labelResultsTableTitle.Text = string.Format(
					GetResource(RES_TITLE_STATIONMODE_VALID), sStationType, sDirection);
				labelNote.Text = string.Format(
					GetResource(RES_NOTE_STATIONMODE_VALID), sStationType, sDirection);

			}
			else
			{
				labelResultsTableTitle.Text = string.Format(
					GetResource(RES_TITLE_STATIONMODE_NOVALID), sStationType);
				labelNote.Text = GetResource(RES_NOTE_STATIONMODE_NOVALID);
			}

			labelMessage.Text = GetResource(RES_ERRORMESSAGE_STATIONMODE);
		}


		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraWiringEvents()
		{
			// Setting up the Button Event handlers
			this.commandNewSearch.Click += new EventHandler(this.CommandNewSearchClick);
			this.commandAmendSearch.Click += new EventHandler(this.CommandAmendSearchClick);
			this.commandBack.Click += new EventHandler(this.CommandBackClick);
			this.commandNext.Click += new EventHandler(this.CommandNextClick);
			this.commandNext2.Click += new EventHandler(this.CommandNextClick);
			this.commandTravelFrom.Click += new EventHandler(this.CommandTravelFromClick);
			this.commandTravelTo.Click += new EventHandler(this.CommandTravelToClick);

            this.mapNearestControl.HideMapButton.Click += new EventHandler(this.CommandHideMapClick);
		}

        /// <summary>
        /// Sets up the printable control with the querystring params needed
        /// </summary>
        private void SetPrintableControl()
        {
            PrintableButtonHelper printHelper = new PrintableButtonHelper(mapNearestControl.MapId, mapNearestControl.MapSymbolsControl.ClientID);
            printerFriendlyPageButton.PrintButton.OnClientClick = printHelper.GetClientScript();
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
                errors.Add(GetResource("MapControl.JavaScriptDisabled.FindNearest.Text"));
                errorDisplayControl.ErrorStrings = errors.ToArray();

                panelErrorDisplayControl.Visible = true;
                errorDisplayControl.Visible = true;
                mapNearestControl.Visible = false;
            }
        }

		#endregion

		#region ViewState Code

		/// <summary>
		/// Loads the internal viewstate for this page.
		/// </summary>
		/// <param name="savedState">Object containing the saved state.</param>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					resetted = (bool)myState[1];
				
			}
		}
	
		/// <summary>
		/// Overrides the base SaveViewState to customise viestate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
		
			object[] allStates = new object[2];
			allStates[0] = baseState;
			allStates[1] = resetted;
			

			return allStates;
		}

		#endregion

		#region OnPreRender and Page_Load methods

		/// <summary>
		/// Overrides base OnPreRender. Updates the Map Tools Control
		/// and calls base OnPreRender.
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
            //Setting visiblity for the error box
            errormsg.Visible = labelMessage.Visible;

			// Special case for FindStationMap page :
			// The page shall not display the icons on the map.
			// therefore, the transport section is open but no icon is selected.
			if (!Page.IsPostBack && TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack) == null)
			{
                mapNearestControl.MapSymbolsControl.UncheckTransportSectionSymbols();
			}
			else if(TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack) != null)
			{
                mapNearestControl.MapSymbolsControl.UncheckTransportSectionSymbols();
			}

            SetPrintableControl();

            CheckJavascriptEnabled();
				
			base.OnPreRender(e);
		}			

		/// <summary>
		/// Page_Load event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			stationPageState = TDSessionManager.Current.FindStationPageState;
			pageState = TDSessionManager.Current.FindPageState;
			
			InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            //CCN 0427 Set up the help button at top right 
            helpIconSelect.HelpLabelControl = stationResultsLocationControl.HelpLabel;

            inputPageState.MapLocation = stationPageState.CurrentLocation;

			//If this isn't a post back and the previous page has ensured that data has not 
			//been saved and can be reloaded.
			if(!Page.IsPostBack && TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack) == null )
			{
				//Re-initialise the map state(s)
				TDSessionManager.Current.JourneyMapState.Initialise();
				TDSessionManager.Current.ReturnJourneyMapState.Initialise();
			}
			
            SetupMap();

            // Load resources
            LoadResources();
            SetControlVisibility();

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindStationMap);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraWiringEvents();
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		#region Event Handler methods

		/// <summary>
		/// Click event for the HideMap button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandHideMapClick(object sender, EventArgs e)
		{
			stationPageState.IsShowingHidingMap = true;
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationMapHideMap;
		}

		/// <summary>
		/// Click event for the Next button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandNextClick(object sender, EventArgs e)
		{
			FindStationHelper.ResultsNext();
		}

		/// <summary>
		/// Click event for the TravelFrom button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandTravelFromClick(object sender, EventArgs e)
		{
			FindStationHelper.ResultsTravelFrom();
		}

		/// <summary>
		/// Click event for the TravelTo button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandTravelToClick(object sender, EventArgs e)
		{
			FindStationHelper.ResultsTravelTo();
		}

		/// <summary>
		/// Click event for the back button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandBackClick(object sender, EventArgs e)
		{
			if (pageState.Mode == FindAMode.Station)
				stationPageState.InstateAmendMode();

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(pageState.Mode);
		}

		/// <summary>
		/// Click event for the NewSearch button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandNewSearchClick(object sender, EventArgs e)
		{	
			stationPageState.Initialise();
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
		}
		
		/// <summary>
		/// Click event for the AmendSearch button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandAmendSearchClick(object sender, EventArgs e)
		{
			// Clear the searches and locations in session then set the AmendKey in session
			// to indicate to the FindStationInput page not to initialise it
			stationPageState.InstateAmendMode();

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
		}

		#endregion
	}
}
