// *********************************************** 
// NAME                 : FindCarParkMap.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 01/08/2006 
// DESCRIPTION			: Map results page for nearest
//						  car parks found for a 
//						  specified location.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindCarParkMap.aspx.cs-arc  $ 
//
//   Rev 1.16   Jun 09 2010 09:05:12   apatel
//Updated to remove javascript eschape characters "'" and "/"
//Resolution for 5550: FindNearest and FindCarPark break with "'" in the content
//
//   Rev 1.15   Jun 08 2010 15:37:52   apatel
//Updated to remove Javascript escape characters from map location point content.
//Resolution for 5550: FindNearest and FindCarPark break with "'" in the content
//
//   Rev 1.14   Mar 29 2010 16:39:24   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.13   Mar 11 2010 12:37:02   mmodi
//Pass in a content string to the map location symbols, to allow popup to be shown at any zoom level and with overlays turned off
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.12   Jan 19 2010 13:20:58   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.11   Dec 07 2009 11:21:48   mmodi
//Corrected scroll to map issue in IE
//
//   Rev 1.10   Nov 20 2009 09:27:40   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 17 2009 18:02:38   mmodi
//Updated
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 12 2009 13:40:58   mmodi
//Updated for printable map page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Nov 12 2009 08:47:54   mmodi
//Updated and removed redundant code
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Nov 10 2009 16:49:18   apatel
//Mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Feb 09 2009 10:56:32   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.4   May 08 2008 11:41:14   mmodi
//Changes made to multiple files to fix the find a car park feature of the city-to-city trunk mode. Testing performed to ensure that the trunk car park features has not broken the drive to car park mode.
//Resolution for 4954: Include 'Drive to Car Park' functionality in City to City
//
//   Rev 1.3   Apr 03 2008 15:27:50   apatel
//updated to show different text in drive to mode.
//
//   Rev 1.2   Mar 31 2008 13:24:24   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 05 2008 09:48:00 apatel
//  Removed the zoom controls and select location control. Removed the driveTo and drive from buttons from top. Change layout of the controls.
//  Move the hide button, back button, search and amend buttons.
//  Moved the help button and the title. labelResultsTableTitle label removed.
//  Removed hide button from the bottom.
//
//   Rev DevFactory   Feb 01 2008 17:00:00   mmodi
//Additional logic for New Search, Amend, and Back buttons for Car Park Usability changes
//
//   Rev DevFactory   Jan 31 2008 18:00:00   mmodi
//Corrected error when Null grid reference being used for Symbol point in method AddAdditionalIconsOnMap()
//
//   Rev 1.0   Nov 08 2007 13:29:24   mturner
//Initial revision.
//
//   Rev 1.17   Sep 28 2007 17:06:18   mmodi
//Updated to use new ESRI ZoomToEnvelope method
//Resolution for 4503: Del 9.8 - Car park map shown at maximum scale for 1 car park result
//
//   Rev 1.16   May 11 2007 14:54:58   nrankin
//Code change to remove Gazetteer postfixes from location name on maps
//Resolution for 4406: Gaz post fixes should not be displayed on maps
//
//   Rev 1.15   Jan 08 2007 15:50:14   jfrank
//Any <modes> have been changed to Main <modes>, this will break previous pseudo naptans CCN's.  This change means this will not occur.
//Resolution for 4333: Gaz Improvement Workshop - Actions 6, 9, 17 - Gaz Config and Naptan Config Changes
//
//   Rev 1.14   Oct 19 2006 10:52:00   mmodi
//Code to display Car park input when Back selected, and corrected display name on map for "Any Rail" change
//Resolution for 4231: Car Parking: Back button navigation issue on results page
//
//   Rev 1.13   Oct 19 2006 10:08:34   mmodi
//Added code to populate overlay map on a postback
//Resolution for 4230: Car Parking: Overlay map lost when different car park selected
//
//   Rev 1.12   Sep 28 2006 17:09:26   mmodi
//Corrected issue where button text is not displayed
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.11   Sep 27 2006 16:47:34   mmodi
//Changed skip link alt text reference
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4185: Car Parking: Screen reader
//
//   Rev 1.10   Sep 19 2006 18:03:04   mmodi
//Added code to display car park icons when selected
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4170: Car Parking: Car park symbol not shown on PT journey map
//
//   Rev 1.9   Sep 19 2006 15:39:10   tmollart
//Modifications for skip links.
//Resolution for 4185: Car Parking: Screen reader
//
//   Rev 1.8   Sep 05 2006 11:32:40   mmodi
//Added transition to Findcarinput
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.7   Aug 31 2006 17:04:52   MModi
//Corrected map display error
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.6   Aug 24 2006 16:52:50   esevern
//Added check for car parks visibility changed before refreshing map
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.5   Aug 23 2006 15:18:58   mmodi
//Added skip link
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Aug 22 2006 14:57:54   esevern
//Removed 'next' button references - not required for FindNearestCarPark
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.3   Aug 14 2006 11:17:42   esevern
//Interim check-in for developer build. Added FindCarParkResultsTable and Location controls
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.2   Aug 04 2006 14:31:18   mmodi
//Added labels and controls
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 03 2006 11:31:04   esevern
//Added PageId to default constructor
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 02 2006 10:36:42   esevern
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2


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
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for FindCarParkMap.
	/// </summary>
	public partial class FindCarParkMap : TDPage
	{
		#region Resource keys declaration

		private const string RES_TITLE_CARPARKMODE_VALID	= "FindCarParkResult.labelTitle.CarParkMode.Valid";
		private const string RES_NOTE_CARPARKMODE_VALID		= "FindCarParkResult.labelNote.CarParkMode.Valid";
		public const string RES_ERRORMESSAGE_CARPARKMODE	= "FindCarParkResult.ErrorMessage.CarParkMode";
        public const string RES_MAP_SYMBOLS = "panelMapLocationSelect.labelMapSymbols";
        private const string RES_NOTE_CARPARKDRIVETOMODE_VALID = "FindCarParkResult.labelNote.CarParkDriveToMode.Valid";

		#endregion
        		
		FindCarParkPageState carParkPageState;
		FindPageState pageState;

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public FindCarParkMap()
		{
			pageId = PageId.FindCarParkMap;
		}

		#endregion

		#region Page event methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
			carParkPageState = TDSessionManager.Current.FindCarParkPageState;
			pageState = TDSessionManager.Current.FindPageState;
			InputPageState inputPageState = TDSessionManager.Current.InputPageState;
			
			// Skip link
			imageMainContentSkipLink1.ImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.AlternateText = GetResource("FindCarParkResults.SkipLink_CarPark.AlternateText");

            inputPageState.MapLocation = carParkPageState.CurrentLocation;

            //If this isn't a post back and the previous page has ensured that data has not 
			//been saved and can be reloaded.
			if(!Page.IsPostBack && TDSessionManager.Current.GetOneUseKey( 
				SessionKey.IndirectLocationPostBack) == null )
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
            #region Determine context based on car parks mode
            TransportDirect.UserPortal.SuggestionLinkService.Context context;

            if (carParkPageState.CurrentFindMode == FindCarParkPageState.FindCarParkMode.Default)
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace;
            else
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney;
            #endregion

            ConfigureLeftMenu("ClientLinks.DoorToDoor.LinkText", "", clientLink, expandableMenuControl, context);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindCarParkMap);
            expandableMenuControl.AddExpandedCategory("Related links");
		
		}

        /// <summary>
        /// Page PreRender even handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            CheckJavascriptEnabled();
        }


		#endregion

		#region Private methods

		/// <summary>
		/// Load page resources
		/// </summary>
		private void LoadResources()
		{
            helpIconSelect.HelpLabelControl = resultsLocationControl.HelpLabel;

			SetText();
		}

		/// <summary>
		/// Set Text for controls in page
		/// </summary>
		private void SetText()
		{
            this.PageTitle = GetResource("FindCarParkMap.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			#region Static Setting

			commandBack.Text		= GetResource("FindCarParkMap.commandBack.Text");
			commandDriveFrom.Text	= GetResource("FindCarParkMap.commandDriveFrom.Text");
			commandDriveTo.Text		= GetResource("FindCarParkMap.commandDriveTo.Text");
			commandNewSearch.Text	= GetResource("FindCarParkMap.commandNewSearch.Text");
			commandAmendSearch.Text = GetResource("FindCarParkMap.commandAmendSearch.Text");

			// CCN 0427 - moved help label to top right corner
            helpIconSelect.AlternateText = GetResource("FindCarParkResults.AlternateText");
			#endregion

			labelResultsTableTitle.Text = GetResource(RES_TITLE_CARPARKMODE_VALID);
			labelNote.Text = GetResource(RES_NOTE_CARPARKMODE_VALID);           
		}

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
        /// Sets up map with the car park locations showing on the map
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
                carParkPageState.ResultsTable,
                location,
                ref minGridReference,
                ref maxGridReference);

            mapNearestControl.Initialise(minGridReference, maxGridReference, GetMapLocationPoints(),
                carParkPageState.CurrentLocation.Description, false);

            resultsTableControl.MapClientID = mapNearestControl.MapId;
            resultsTableControl.MapScrollToID = mapNearestControl.FirstElementId;
            resultsTableControl.IsRowSelectLinkVisible = true;
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

            // Add carpark symbols
            foreach (DataRow dataRow in carParkPageState.ResultsTable.Rows)
            {
                try
                {
                    OSGridReference gridRef = (OSGridReference)dataRow[FindCarParkHelper.columnGridRef];

                    if ((gridRef != null) && (gridRef.IsValid))
                    {
                        string iconName = "CIRCLE" + ((int)dataRow[FindCarParkHelper.columnIndex] + 1).ToString(TDCultureInfo.InvariantCulture.NumberFormat);

                        // Get the stop name and stop information link to be shown in the info popup for the location shown on map
                        // Removed "'" and "\" from car park name as they causing problems when rendered in the esri map api
                        string content = "<b>" + ((string)dataRow[FindCarParkHelper.columnCarParkName]).Replace("\\", "").Replace("\'", "\\\'") + "</b><br />"
                            + mapHelper.GetCarParkInformationLink((string)dataRow[FindCarParkHelper.columnCarParkRef]);

                        mapLocationPoints.Add(new MapLocationPoint(gridRef, MapLocationSymbolType.Custom, iconName, " ", true, false, content));
                    }
                }
                catch
                {
                    // ignore the error, shouldnt happen but may do if the OSGR is null
                }
            }

            return mapLocationPoints.ToArray();
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
			this.commandDriveFrom.Click += new EventHandler(this.CommandTravelFromClick);
			this.commandDriveTo.Click += new EventHandler(this.CommandTravelToClick);

            this.mapNearestControl.HideMapButton.Click += new EventHandler(this.CommandHideMapClick);
		}
        
        /// <summary>
        /// Overrides base OnPreRender. Updates the Map Tools Control
        /// and calls base OnPreRender.
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            // Special case for FindCarParkMap page - The page should not display 
            // the icons on the map, so the transport section is open but no icon is selected.
            if (!Page.IsPostBack && TDSessionManager.Current.GetOneUseKey(SessionKey.IndirectLocationPostBack) == null)
            {
                mapNearestControl.MapSymbolsControl.UncheckTransportSectionSymbols();
            }
            else if (TDSessionManager.Current.GetOneUseKey(SessionKey.IndirectLocationPostBack) != null)
            {
                mapNearestControl.MapSymbolsControl.UncheckTransportSectionSymbols();
               
            }

            SetPrintableControl();

            base.OnPreRender(e);
        }

		/// <summary>
		/// Set visibility of controls in page
		/// </summary>
		private void SetControlVisibility()
		{
            bool showDriveFromButtons = TDSessionManager.Current.FindCarParkPageState.CurrentFindMode != FindCarParkPageState.FindCarParkMode.DriveTo;
            commandDriveFrom.Visible = showDriveFromButtons;
			commandDriveTo.Visible = true;
            commandNewSearch.Visible = true;
			commandAmendSearch.Visible = true;

            if (!showDriveFromButtons)
            {
                labelNote.Text = GetResource(RES_NOTE_CARPARKDRIVETOMODE_VALID);
            }

            //Note: we always want to hide the New Location button, since this functionality
            //has now been replaced using the New Search button.
            resultsLocationControl.SetNewLocationButtonVisibility(false);
		}

        /// <summary>
        /// Sets the session journey parameters ready for a new search
        /// </summary>
        private void SetupForJourneyNewSearch(ITDSessionManager sessionManager, TDItineraryManager itineraryManager)
        {
            //Reset all journey related
            itineraryManager.NewSearch();

            // Flag new search button being clicked so that redirect page can perform any necessary initialisation
            sessionManager.SetOneUseKey(SessionKey.NewSearch, string.Empty);

            // invalidate the current journey result. Set the mode for which the results pertain to as being
            // none so that clicking the Find A tab will then redirect to the default find A input page.
            if (sessionManager.JourneyResult != null)
            {
                sessionManager.JourneyResult.IsValid = false;
            }
        }

        /// <summary>
        /// Sets the session journey parameters ready for an amend
        /// </summary>
        /// <param name="sessionManager"></param>
        private void SetupForJourneyAmend(ITDSessionManager sessionManager)
        {
            // If the results have been added to the Itinerary then we need to get them back out again
            ExtendItineraryManager itineraryManager = TDItineraryManager.Current as ExtendItineraryManager;

            if ((itineraryManager != null) && (itineraryManager.Length > 0) && !itineraryManager.ExtendInProgress)
            {
                if (itineraryManager.Length == 1)
                {
                    // The Initial journey is the only journey in the Itinerary
                    itineraryManager.ResetToInitialJourney();
                }
                else
                {
                    itineraryManager.ResetLastExtension();
                }
            }

            // invalidate the current journey result. Set the mode for which the results pertain to as being
            // none so that clicking the Find A tab will then redirect to the default find A input page.
            if (sessionManager.JourneyResult != null)
            {
                sessionManager.JourneyResult.IsValid = false;
            }
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

		#region Event Handler methods

		/// <summary>
		/// Click event for the HideMap button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandHideMapClick(object sender, EventArgs e)
		{
			carParkPageState.IsShowingHidingMap = true;
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkMapHideMap;
		}

		/// <summary>
		/// Click event for the TravelFrom button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandTravelFromClick(object sender, EventArgs e)
		{
			FindCarParkHelper.ResultsTravelFrom();
			
			// Set the Return Stack so Find Car route can check its come from this page
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarInputDefault;
		}

		/// <summary>
		/// Click event for the TravelTo button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandTravelToClick(object sender, EventArgs e)
		{
            FindCarParkHelper.ResultsTravelTo();

            // Set the Return Stack so Find Car route/Door to door input can check its come from this page
            TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);

            if (carParkPageState.IsFromDoorToDoor)
            {
                FindCarParkHelper.SetupCarParkLocationForDoorToDoor(false);

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
            }
            else
            {
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarInputDefault;
            }
		}

		/// <summary>
		/// Click event for the back button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandBackClick(object sender, EventArgs e)
		{
            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

            // Determine mode of page and send to appropriate page
            if (sessionManager.FindCarParkPageState.CurrentFindMode == FindCarParkPageState.FindCarParkMode.DriveTo)
            {
                #region Door to door, Find car route, city to city
                // User has come from the Car Journey Details page if in DriveTo mode. So return them back there
                #region Set for going to Details page
                // Reset car park state
                carParkPageState.Initialise();

                // Reset for going to Details page
                sessionManager.JourneyMapState.Initialise();
                sessionManager.ReturnJourneyMapState.Initialise();

                //When entering the Details page, set the Road Units to Miles
                sessionManager.InputPageState.Units = RoadUnitsEnum.Miles;

                // Specific handling for Door to door
                if (carParkPageState.IsFromDoorToDoor)
                {
                    carParkPageState.IsFromDoorToDoor = false;
                    sessionManager.FindPageState = null;
                }
                else if (carParkPageState.IsFromCityToCity)
                {
                    carParkPageState.IsFromCityToCity = false;
                }
                #endregion

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyDetails;
                #endregion
            }
            else
            {
                #region Car park input
                if ((pageState.Mode == FindAMode.CarPark) || (pageState.Mode == FindAMode.Car))
                    carParkPageState.InstateAmendMode();

                // Set the mode the find car park input page should be displayed in
                carParkPageState.CarParkFindMode = carParkPageState.CurrentFindMode;

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(FindAMode.CarPark);
                #endregion
            }
		}

		/// <summary>
		/// Click event for the NewSearch button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandNewSearchClick(object sender, EventArgs e)
		{
            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
            TDItineraryManager itineraryManager = TDItineraryManager.Current;

            // Reset car park state
            carParkPageState.Initialise();

            // Determine where we've come from and send to appropriate input page
            if (carParkPageState.IsFromDoorToDoor)
            {
                #region Door to door
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;

                carParkPageState.IsFromDoorToDoor = false;
                sessionManager.FindPageState = null;

                SetupForJourneyNewSearch(sessionManager, itineraryManager);
                #endregion
            }
            else if (carParkPageState.IsFromCityToCity)
            {
                #region City to city
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;

                carParkPageState.IsFromCityToCity = false;

                SetupForJourneyNewSearch(sessionManager, itineraryManager);
                #endregion
            }
            else if (sessionManager.FindPageState.Mode == FindAMode.Car)
            {
                #region Find car route
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarInputDefault;

                SetupForJourneyNewSearch(sessionManager, itineraryManager);
                #endregion
            }
            else
            {
                #region Car park input
                // Set the mode the find car park input page should be displayed in
                carParkPageState.CarParkFindMode = carParkPageState.CurrentFindMode;

                // User started in Nearest car park mode, send back to Find car park input
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputDefault;
                #endregion
            }
		}
		
		/// <summary>
		/// Click event for the AmendSearch button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandAmendSearchClick(object sender, EventArgs e)
		{
            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

            // Determine where we've come from and send to appropriate input page
            if (carParkPageState.IsFromDoorToDoor)
            {
                #region Door to door
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;

                carParkPageState.IsFromDoorToDoor = false;
                sessionManager.FindPageState = null;

                SetupForJourneyAmend(sessionManager);
                #endregion
            }
            else if (carParkPageState.IsFromCityToCity)
            {
                #region City to city
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;

                carParkPageState.IsFromCityToCity = false;

                SetupForJourneyAmend(sessionManager);
                #endregion
            }
            else if (sessionManager.FindPageState.Mode == FindAMode.Car)
            {
                #region Find car route
                sessionManager.FindPageState.PrepareForAmendJourney();

                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarInputDefault;

                SetupForJourneyAmend(sessionManager);
                #endregion
            }
            else
            {
                #region Car park input
                // Clear the searches and locations in session then set the AmendKey in session
                // to indicate to the input page not to initialise it
                carParkPageState.InstateAmendMode();

                // Set the mode the find car park input page should be displayed in
                carParkPageState.CarParkFindMode = carParkPageState.CurrentFindMode;

                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputDefault;
                #endregion
            }
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
	}
}
