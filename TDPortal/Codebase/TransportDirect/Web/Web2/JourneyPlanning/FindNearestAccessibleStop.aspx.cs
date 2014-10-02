// *********************************************** 
// NAME                 : FindNearestAccessibleStop.aspx
// AUTHOR               : David Lane
// DATE CREATED         : 26/11/2012 
// DESCRIPTION  : Find Nearest Accessible Stop page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindNearestAccessibleStop.aspx.cs-arc  $ 
//
//   Rev 1.13   Jan 29 2013 13:02:22   mmodi
//Display select this stop link in the accessible stops map 
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.12   Jan 29 2013 11:31:44   DLane
//Removing localities for via locations
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.11   Jan 24 2013 15:44:42   mmodi
//Reposition Back button and hide Next when no accessible location choices
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.9   Jan 15 2013 13:54:42   mmodi
//Hide location show map button if map is showing
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.8   Jan 15 2013 13:29:44   mmodi
//Added switch to display localities transport type
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Jan 15 2013 10:30:56   mmodi
//Specify a search distance override for find accessible stop
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.6   Jan 09 2013 17:32:30   mmodi
//Updated help button
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Jan 09 2013 16:10:22   mmodi
//Error message display updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Jan 09 2013 11:44:20   mmodi
//Updated to display localities on map
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Jan 04 2013 16:46:10   mmodi
//Updated for javascript disabled
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Jan 04 2013 15:40:54   mmodi
//Updates for Find nearest accessible stops page display and logic
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Dec 18 2012 16:55:00   dlane
//Accessible JP updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Dec 07 2012 16:01:54   DLane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using System.Text;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Journey Planner Input page
	/// </summary>
	/// 
	public partial class FindNearestAccessibleStop : TDPage
	{
		#region  Instance members

        protected System.Web.UI.WebControls.CheckBoxList checklistModesPublicTransport;
		protected System.Web.UI.WebControls.Label labelPublicModesNote;
		protected System.Web.UI.WebControls.Label labelPublicModesTitle;
		protected System.Web.UI.WebControls.Panel panelRouteOptionsLabel;

        protected TransportDirect.UserPortal.Web.Controls.AccessibleTransportTypesControl accessibleTransportTypesControl;

		protected HeaderControl headerControl;

		private ControlPopulator populator;
		private TDJourneyParametersMulti journeyParameters;
		private InputPageState inputPageState;
        
		// Locations
        private TDLocation originLocation;
        private TDLocation destinationLocation;
        private TDLocation viaLocation;

        // Errors to display
        private List<string> errorResourceIds = new List<string>();

        private bool isCJPUser = false;

        #endregion	
		
		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public FindNearestAccessibleStop()
		{
			pageId = PageId.FindNearestAccessibleStop;
		}

		#endregion

        #region Page_Init, Page_Load, OnPreRender, OnUnload

        /// <summary>
        /// Performs page initialisation including event wiring.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            // Get DataServices from Service Discovery
            populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            #region Events wireup

            // Add event handlers
            originTDANControl.MapLocationClick += new EventHandler(MapFromClick);
            destinationTDANControl.MapLocationClick += new EventHandler(MapToClick);
            viaTDANControl.MapLocationClick += new EventHandler(MapViaClick);

            // Map
            mapNearestControl.HideMapButton.Click += new EventHandler(this.CommandHideMapClick);

            // Buttons
            commandSubmit.Click += new EventHandler(SubmitClick);
            commandBack.Click += new EventHandler(commandBack_Click);

            // CJP User button
            btnUpdate.Click += new EventHandler(btnUpdate_Click);

            // Event Handler for default action button			
            headerControl.DefaultActionEvent += new EventHandler(this.SubmitClick);

            // Stop tranport type click change
            accessibleTransportTypesControl.ModesAccessibleTransport.SelectedIndexChanged += new EventHandler(ModesAccessibleTransport_SelectedIndexChanged);
            accessibleTransportTypesControl.UpdateButton.Click += new EventHandler(ModesAccessibleTransport_SelectedIndexChanged);

            #endregion
        }

        /// <summary>
		/// Page_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            if (!Page.IsPostBack)
            {
                #region Clear cache of journey data
                ClearCacheHelper helper = new ClearCacheHelper();

                // Force clear of any printable information if added by the journey result page
                helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

                // Fix for IR5481 Session issue when going from FAT to D2D using the left hand menu
                if (TDSessionManager.Current.FindAMode != FindAMode.None || TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.None)
                {
                    // We have come directly from another planner so clear results from session.
                    helper.ClearJourneyResultCache();
                }
                #endregion
            }

			// This is known because FindAMode.None always gives us Multi
			journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
			inputPageState = sessionManager.InputPageState;

			// Getting search and location objects from session
			LoadSessionVariables();

            // Update CJP user flag
            isCJPUser = IsCJPUser();

            // Initialise controls
            SetupLocationControls();

            SetupAdvancedUserControls();

			//Adding client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);
            
            // Setup navigation menus
            ConfigureLeftMenu(expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyPlannerInput);
            expandableMenuControl.AddExpandedCategory("Related links");

            SetupHelp();
        }

        /// <summary>
        /// Page_PreRender
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            SetupResources();

            ShowErrors();

            SetupMap();

            SetupControlVisibility();

            SaveAccessibleLocations();
        }

        #endregion

		#region Events handlers

        /// <summary>
        /// Submit Click event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void SubmitClick(object sender, EventArgs e)
        {
            SubmitRequest();

            if (mapNearestControl.Visible)
            {
                ShowMap();
            }
        }

        /// <summary>
        /// Back button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void commandBack_Click(object sender, EventArgs e)
        {
            //Check to see if there is a previous page waiting on the stack (i.e. user did not come in
            //from JourneyPlannerInput.aspx)
            if (inputPageState.JourneyInputReturnStack.Count != 0)
            {
                PageId lastPage = (PageId)inputPageState.JourneyInputReturnStack.Pop();
                
                if (lastPage == PageId.JourneyPlannerAmbiguity)
                {
                    TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputErrors;
                    return;
                }
            }

            // Reinstate all journey parameters that may have changed during
            // on this page
            if (TDSessionManager.Current.AmbiguityResolution != null)
            {
                TDSessionManager.Current.AmbiguityResolution.ReinstateJourneyParameters();
            }

            // Default return to JourneyPlannerInput page    
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerAmbiguityBack;
        }

        /// <summary>
        /// Event handler for when the update button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Event handler for when stop tranport type is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ModesAccessibleTransport_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // If user changes the selected stop transport types, then reload 
            // the accessible location lists
            Refresh();

            // Track AJAX event
            SetPartialPostbackValues(PageId.FindNearestAccessibleStopTransportClickAJAX);
        }      

        #region Map Event Handlers

		/// <summary>
		/// Map from click event hadler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void MapFromClick(object sender, EventArgs e)
        {
            // Set map search and location
            inputPageState.MapLocationSearch = journeyParameters.Origin;
            inputPageState.MapLocation = journeyParameters.OriginLocation;

            inputPageState.MapType = CurrentLocationType.From;
            inputPageState.MapMode = CurrentMapMode.FromJourneyInput;
            inputPageState.MapLocationControlType.Type = ControlType.Default;

            SetupMap(originTDANControl);

            // Track AJAX event
            SetPartialPostbackValues(PageId.FindNearestAccessibleStopMapClickAJAX);
        }
        
		/// <summary>
		/// Map to click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapToClick(object sender, EventArgs e)
		{
            // Set map search and location
            inputPageState.MapLocationSearch = journeyParameters.Destination;
            inputPageState.MapLocation = journeyParameters.DestinationLocation;

            inputPageState.MapType = CurrentLocationType.To;
            inputPageState.MapMode = CurrentMapMode.FromJourneyInput;
            inputPageState.MapLocationControlType.Type = ControlType.Default;

            SetupMap(destinationTDANControl);

            // Track AJAX event
            SetPartialPostbackValues(PageId.FindNearestAccessibleStopMapClickAJAX);
		}

        /// <summary>
        /// Map to click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapViaClick(object sender, EventArgs e)
        {
            // Set map search and location
            inputPageState.MapLocationSearch = journeyParameters.PublicVia;
            inputPageState.MapLocation = journeyParameters.PublicViaLocation;

            inputPageState.MapType = CurrentLocationType.PublicVia;
            inputPageState.MapMode = CurrentMapMode.FromJourneyInput;
            inputPageState.MapLocationControlType.Type = ControlType.Default;

            SetupMap(viaTDANControl);

            // Track AJAX event
            SetPartialPostbackValues(PageId.FindNearestAccessibleStopMapClickAJAX);
        }

        /// <summary>
        /// Click event for the HideMap button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void CommandHideMapClick(object sender, EventArgs e)
        {
            // Update the map values to be reset
            inputPageState.MapLocationSearch = null;
            inputPageState.MapLocation = null;

            inputPageState.MapType = CurrentLocationType.None;

            mapNearestControl.Visible = false;

            // Track AJAX event
            SetPartialPostbackValues(PageId.FindNearestAccessibleStopMapHideClickAJAX);
        }

        #endregion

		#endregion

        #region Private Methods

        /// <summary>
        /// Loads page location and search objects from journey parameters
        /// </summary>
        private void LoadSessionVariables()
        {
            //This is to fix the journey planner being null.
            //Note that we don't need to check that journeyPlanner 
            //is the correct type. It always will be, else it'll be
            //null.
            if (journeyParameters == null)
            {
                journeyParameters = new TDJourneyParametersMulti();
                TDSessionManager.Current.JourneyParameters = journeyParameters;
            }

            originLocation = journeyParameters.OriginLocation;
            destinationLocation = journeyParameters.DestinationLocation;
            viaLocation = journeyParameters.PublicViaLocation;
        }

        /// <summary>
        /// Saves the accessible locations from the controls to session
        /// </summary>
        private void SaveAccessibleLocations()
        {
            inputPageState.AccessibleLocationsOrigin = originTDANControl.LocationChoices.ToArray();
            inputPageState.AccessibleLocationsDestination = destinationTDANControl.LocationChoices.ToArray();
            inputPageState.AccessibleLocationsPublicVia = viaTDANControl.LocationChoices.ToArray();
        }
        
        /// <summary>
        /// Setsup the accessible origin, destination and via location controls
        /// </summary>
        private void SetupLocationControls()
        {
            List<TDStopType> stopTypes = new List<TDStopType>(accessibleTransportTypesControl.AccessibleModes);

            // Set accessible location controls where appropriate
            originTDANControl.Initialise(originLocation, stopTypes, journeyParameters, inputPageState.AccessibleLocationsOrigin, false);
            destinationTDANControl.Initialise(destinationLocation, stopTypes, journeyParameters, inputPageState.AccessibleLocationsDestination, false);

            if (viaLocation == null || viaLocation.Status == TDLocationStatus.Unspecified)
            {
                labelViaTitle.Visible = false;
                viaTDANControl.Visible = false;
            }
            else
            {
                viaTDANControl.Initialise(viaLocation, stopTypes, journeyParameters, inputPageState.AccessibleLocationsPublicVia, true);
            }
        }

        /// <summary>
        /// Setsup the advanced user controls
        /// </summary>
        private void SetupAdvancedUserControls()
        {
            pnlCJPUser.Visible = isCJPUser;

            if (isCJPUser)
            {
                lblDistance.Text = GetResource("FindNearestAccessibleStop.SearchDistance.Text");
                btnUpdate.Text = GetResource("FindNearestAccessibleStop.Update.Text");

                if (!Page.IsPostBack)
                {
                    // Distance override
                    int searchDistanceMetresStops = 10000;

                    if (!Int32.TryParse(Properties.Current["AccessibleOptions.FindNearestLocations.Stops.SearchDistance.Metres"], out searchDistanceMetresStops))
                        searchDistanceMetresStops = 10000;

                    txtDistance.Text = searchDistanceMetresStops.ToString();
                }
            }
        }

        /// <summary>
        /// Setsup the map, dependent on the state of the input page state
        /// </summary>
        private void SetupMap()
        {
            // Check if returning from the Stop Information page, 
            // if it is and the map was previously displayed, then show the map
            string indirectLocationPostBack = TDSessionManager.Current.GetOneUseKey(SessionKey.IndirectLocationPostBack);

            if (indirectLocationPostBack != null)
            {
                // Has returned from Stop information page, and the map was previously selected
                if (inputPageState.MapType != CurrentLocationType.None)
                {
                    ShowMap();
                }
            }

            // Hide map button for the location controls if needed
            if (mapNearestControl.Visible)
            {
                switch (inputPageState.MapType)
                {
                    case CurrentLocationType.PublicVia:
                        viaTDANControl.ShowMapButton = false;
                        break;
                    case CurrentLocationType.To:
                        destinationTDANControl.ShowMapButton = false;
                        break;
                    case CurrentLocationType.From:
                    default:
                        originTDANControl.ShowMapButton = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Shows the map by firing the appropriate show map click event
        /// </summary>
        private void ShowMap()
        {
            switch (inputPageState.MapType)
            {
                case CurrentLocationType.PublicVia:
                    MapViaClick(this, null);
                    break;
                case CurrentLocationType.To:
                    MapToClick(this, null);
                    break;
                case CurrentLocationType.From:
                default:
                    MapFromClick(this, null);
                    break;
            }
        }

        /// <summary>
        /// Refresh method to update the various controls on the page
        /// </summary>
        private void Refresh()
        {
            SetupLocationControls();

            // Check if the accessible search distance override has been specified
            if (isCJPUser)
            {
                int searchDistance = -1;

                if (!Int32.TryParse(txtDistance.Text, out searchDistance))
                    searchDistance = -1;
                else if (searchDistance > 5000000)
                    searchDistance = 5000000;

                originTDANControl.SearchDistanceOverride = searchDistance;
                destinationTDANControl.SearchDistanceOverride = searchDistance;
                viaTDANControl.SearchDistanceOverride = searchDistance;
            }

            originTDANControl.Refresh();
            destinationTDANControl.Refresh();

            if (viaTDANControl.Visible)
                viaTDANControl.Refresh();

            if (mapNearestControl.Visible)
            {
                ShowMap();
            }
        }

        #region Map methods

        /// <summary>
        /// Sets up a map for the location and its accessible locations list
        /// </summary>
        private void SetupMap(FindTDANControl findTDANControl)
        {   
            // Location to center on
            TDLocation location = inputPageState.MapLocation;
            List<TDLocationAccessible> locationsAccessible = findTDANControl.LocationChoices;

            #region Points to display on the map

            // Build the locations to display
            List<TDLocation> tdLocations = new List<TDLocation>();
            TDLocation locMap = null;
            List<TDNaptan> tdNaptans = null;

            if (locationsAccessible != null)
            {
                #region Stops and Localities to display on the map
                
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                foreach (TDLocationAccessible loc in locationsAccessible)
                {
                    // Create a location containing the details required to generate map points
                    locMap = new TDLocation();
                    tdNaptans = new List<TDNaptan>();
                    
                    locMap.Description = loc.Description;
                    locMap.StopType = loc.StopType;
                    locMap.GridReference = loc.GridReference;

                    if (loc.StopType == TDStopType.Locality)
                    {
                        // Locality location
                        try
                        {
                            // Update the coordinate of the locality location
                            if (loc.GridReference != null && !loc.GridReference.IsValid)
                            {
                                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                                    string.Format("GIS query call GetLocalityInfoForNatGazID for location[{0} {1}]",
                                        loc.ID, loc.Description)));

                                LocalityNameInfo lni = gisQuery.GetLocalityInfoForNatGazID(loc.Locality);

                                if (lni != null)
                                {
                                    locMap.GridReference = new OSGridReference(lni.Easting, lni.Northing);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // GIS query error, don't add this location
                            string message = string.Format("Error doing GIS query to retreive Locality coordinates for location[{0} {1}]. Exception: {2}. {3}",
                                location.Locality, location.Description,
                                ex.Message, ex.StackTrace);
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message));
                        }
                    }
                    else
                    {
                        // TDNaptan might not be fully populated in the location so search if needed
                        foreach (TDNaptan naptan in loc.NaPTANs)
                        {
                            if (string.IsNullOrEmpty(naptan.Naptan) || string.IsNullOrEmpty(naptan.Name)
                                || !naptan.GridReference.IsValid)
                            {
                                NaptanCacheEntry nce = NaptanLookup.Get(naptan.Naptan, string.Empty);

                                if (nce.Found)
                                {
                                    tdNaptans.Add(new TDNaptan(nce.Naptan, nce.OSGR, nce.Description, false, nce.Locality));
                                }
                            }
                            else
                            {
                                // Naptan is ok, add it
                                tdNaptans.Add(naptan);
                            }
                        }
                    }

                    locMap.NaPTANs = tdNaptans.ToArray();

                    tdLocations.Add(locMap);
                }

                #endregion

            }
            #endregion

            // Define envelope to zoom to
            OSGridReference minGridReference = new OSGridReference(), maxGridReference = new OSGridReference();

            SetMapZoomEnvelope(location, tdLocations, ref minGridReference, ref maxGridReference);

            mapNearestControl.Initialise(minGridReference, maxGridReference,
                GetMapLocationPoints(location, tdLocations, findTDANControl), 
                null,
                location.Description, true, true, fnAccessibleStopMap.ClientID);

            // Show map
            mapNearestControl.Visible = true;

            // Don't display the symbols
            mapNearestControl.MapSymbolsControl.Visible = false;
        }

        /// <summary>
        /// Determines the coordinates of the zoom box to apply to the map
        /// according to the distance of the furthest accessible location
        /// </summary>
        /// <returns>returns true if successful</returns>
        private void SetMapZoomEnvelope(
            TDLocation location,
            List<TDLocation> tdLocations,
            ref OSGridReference minGridReference,
            ref OSGridReference maxGridReference)
        {
            if (location != null && tdLocations != null && tdLocations.Count > 0)
            {
                // Get all osgrs, and build an map envelope
                List<OSGridReference> osgrs = new List<OSGridReference>();

                foreach (TDLocation tdLoc in tdLocations)
                {
                    if (tdLoc.StopType == TDStopType.Locality)
                    {
                        // Localities
                        if (tdLoc.GridReference.IsValid)
                        {
                            osgrs.Add(tdLoc.GridReference);
                        }
                    }
                    else
                    {
                        // Stops
                        foreach (TDNaptan tdNaptan in tdLoc.NaPTANs)
                        {
                            if (tdNaptan.GridReference.IsValid)
                            {
                                osgrs.Add(tdNaptan.GridReference);
                            }
                        }
                    }
                }

                MapHelper mapHelper = new MapHelper();

                OSGridReference[] envelopeOsgrs = mapHelper.CreateMapEnvelope(location.GridReference, osgrs.ToArray());

                minGridReference = envelopeOsgrs[0];
                maxGridReference = envelopeOsgrs[1];
            }
            else if (location != null)
            {
                // Default radius
                double radius = 10000; // metres

                // minGridReference = X0-Radius ; Y0-Radius
                // 5% tolerance because of icons sometimes not shown entirely,
                minGridReference.Easting = location.GridReference.Easting - ((int)(radius * 1.05));
                minGridReference.Northing = location.GridReference.Northing - ((int)(radius * 1.05));

                // maxGridReference = X0+Radius ; Y0+Radius
                maxGridReference.Easting = location.GridReference.Easting + ((int)(radius * 1.05));
                maxGridReference.Northing = location.GridReference.Northing + ((int)(radius * 1.05));
            }
        }

        /// <summary>
        /// Returns the location points to show on map
        /// </summary>
        /// <returns></returns>
        private MapLocationPoint[] GetMapLocationPoints(TDLocation location, List<TDLocation> tdLocations,
            FindTDANControl findTDANControl)
        {
            MapHelper mapHelper = new MapHelper();

            List<MapLocationPoint> mapLocationPoints = new List<MapLocationPoint>();

            if (location != null)
            {
                string name = location.Description;

                // Strip out any sub strings (read from properties DB) denoting pseudo locations 
                string railPostFix = Properties.Current["Gazetteerpostfix.rail"];
                string coachPostFix = Properties.Current["Gazetteerpostfix.coach"];
                string railcoachPostFix = Properties.Current["Gazetteerpostfix.railcoach"];

                StringBuilder strName = new StringBuilder(name);
                strName.Replace(railPostFix, "");
                strName.Replace(coachPostFix, "");
                strName.Replace(railcoachPostFix, "");
                string shortname = strName.ToString();

                // Add center location
                mapLocationPoints.Add(new MapLocationPoint(location.GridReference, MapLocationSymbolType.Circle, shortname, false, false));
            }

            if (tdLocations != null)
            {
                // Add accessible location symbols
                int index = 0;
                foreach (TDLocation tdLoc in tdLocations)
                {
                    if (tdLoc.StopType == TDStopType.Locality)
                    {
                        // Localities

                        index++;

                        string iconName = "CIRCLE" + index.ToString(TDCultureInfo.InvariantCulture.NumberFormat);

                        string content = string.Format("<b>{0}</b><br />{1}<br />",
                            tdLoc.Description.Replace("\\", "").Replace("\'", "\\\'"),
                            string.Format(GetResource("MapHelper.SelectLocationInDropdownLink"), findTDANControl.LocationDrop.ClientID, index, updateInputPanel.ClientID));
                        
                        mapLocationPoints.Add(new MapLocationPoint(tdLoc.GridReference, MapLocationSymbolType.Custom, iconName,
                            " ", true, false, content));
                    }
                    else
                    {
                        // Stops

                        foreach (TDNaptan naptan in tdLoc.NaPTANs)
                        {
                            index++;

                            string iconName = "CIRCLE" + index.ToString(TDCultureInfo.InvariantCulture.NumberFormat);
                            
                            // Get the stop name and stop information link to be shown in the info popup for the location shown on map
                            // Removed "'" and "\" from stop name as they causing problems when rendered in the esri map api
                            string content = string.Format("<b>{0}</b><br />{1}<br />{2}<br />",
                                naptan.Name.Replace("\\", "").Replace("\'", "\\\'"),
                                mapHelper.GetStopInformationLink(naptan.Naptan),
                                string.Format(GetResource("MapHelper.SelectLocationInDropdownLink"), findTDANControl.LocationDrop.ClientID, index, updateInputPanel.ClientID));

                            mapLocationPoints.Add(new MapLocationPoint(naptan.GridReference, MapLocationSymbolType.Custom, iconName,
                                " ", true, false, content));
                        }
                    }
                }
            }

            return mapLocationPoints.ToArray();
        }

        /// <summary>
        /// Gets the default search type
        /// </summary>
        /// <param name="listType"></param>
        /// <returns></returns>
        private SearchType GetDefaultSearchType(DataServiceType listType)
        {
            DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            string defaultItemValue = ds.GetDefaultListControlValue(listType);
            return (SearchType)(Enum.Parse(typeof(SearchType), defaultItemValue));
        }

        #endregion

        #region Submit

        /// <summary>
        /// Validates inputs and submits the journey request
        /// </summary>
        private void SubmitRequest()
        {
            bool locationsValid = true;

            #region Origin, Destination and Via locations

            // Check selected locations, where the locations statuses are valid, 
            // then submit the request. 
            // Assume all other journey parameters were validated prior to reaching this accessible stop page

            TDLocation accessibleOrigin = originTDANControl.Location;
            TDLocation accessibleDestination = destinationTDANControl.Location;
            TDLocation accessibleVia = viaTDANControl.Location;
            
            // Origin
            if (accessibleOrigin == null || accessibleOrigin.Status != TDLocationStatus.Valid || !accessibleOrigin.Accessible)
            {
                locationsValid = false;

                // Redisplay this page, displaying an error message indicating user must select an accessible stop,
                // only if location choices exist - otherwise another ShowErrors method will display appropriate message
                if (originTDANControl.LocationChoicesFound)
                    errorResourceIds.Add("FindNearestAccessibleStop.SelectStop.Origin.Error");
                
            }
            // Destination
            else if (accessibleDestination == null || accessibleDestination.Status != TDLocationStatus.Valid || !accessibleDestination.Accessible)
            {
                locationsValid = false;

                // Redisplay this page, displaying an error message indicating user must select an accessible stop,
                // only if location choices exist - otherwise another ShowErrors method will display appropriate message
                if (destinationTDANControl.LocationChoicesFound)
                    errorResourceIds.Add("FindNearestAccessibleStop.SelectStop.Destination.Error");
            }
            // Via
            else if ((viaTDANControl.Visible)
                && (accessibleVia == null || accessibleVia.Status != TDLocationStatus.Valid || !accessibleVia.Accessible))
            {
                locationsValid = false;

                // Redisplay this page, displaying an error message indicating user must select an accessible stop,
                // only if location choices exist - otherwise another ShowErrors method will display appropriate message
                if (viaTDANControl.LocationChoicesFound)
                    errorResourceIds.Add("FindNearestAccessibleStop.SelectStop.Via.Error");
            }

            #endregion

            if (locationsValid)
            {
                // Update journey parameters with accessible locations
                journeyParameters.OriginLocation = accessibleOrigin;
                journeyParameters.DestinationLocation = accessibleDestination;

                if (viaTDANControl.Visible)
                {
                    journeyParameters.PublicViaLocation = accessibleVia;
                }

                JourneyPlannerInputAdapter journeyPlannerInputAdapter = new JourneyPlannerInputAdapter();
                journeyPlannerInputAdapter.ValidateAndSearch(true, PageId.JourneyPlannerInput);
            }
        }

        #endregion

        /// <summary>
        /// Loads text and image resources
        /// </summary>
        private void SetupResources()
        {
            PageTitle = GetResource("FindNearestAccessibleStop.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            imageJourneyPlanner.ImageUrl = GetResource("PlanAJourneyControl.imageDoorToDoor.ImageUrl");
            imageJourneyPlanner.AlternateText = " ";

            labelFindNearestAccessibleStopTitle.Text = GetResource("FindNearestAccessibleStop.Title");

            #region Information text
            
            // Information text
            string acccessibleStr = string.Empty;
            string locationStr = string.Empty;

            if (journeyParameters.RequireStepFreeAccess && journeyParameters.RequireSpecialAssistance)
                acccessibleStr = GetResource("FindNearestAccessibleStop.SubHeading.WheelchairAssistance");
            else if (journeyParameters.RequireStepFreeAccess)
                acccessibleStr = GetResource("FindNearestAccessibleStop.SubHeading.Wheelchair");
            else if (journeyParameters.RequireSpecialAssistance)
                acccessibleStr = GetResource("FindNearestAccessibleStop.SubHeading.Assistance");

            locationStr = GetLocationString();

            labelSubHeading.Text = string.Format(
                GetResource("FindNearestAccessibleStop.SubHeading"),
                acccessibleStr, locationStr, locationStr);

            #endregion

            // Location labels
            labelOriginTitle.Text = GetResource("originSelect.labelLocationTitle");
            labelDestinationTitle.Text = GetResource("destinationSelect.labelLocationTitle");
            labelViaTitle.Text = GetResource("viaSelect.labelLocationTitle");

            // Buttons
            commandSubmit.Text = GetResource("FindNearestAccessibleStop.NextButton");
            commandBack.Text = GetResource("FindNearestAccessibleStop.BackButton");
        }

        /// <summary>
        /// Loads help
        /// </summary>
        private void SetupHelp()
        {
            // Help button
            Helpbuttoncontrol1.HelpUrl = GetResource("FindNearestAccessibleStop.HelpPageUrl");
        }

        /// <summary>
        /// Returns a location string to display in the information text, e.g. "origin and destination"
        /// </summary>
        /// <returns></returns>
        private string GetLocationString()
        {
            StringBuilder sb = new StringBuilder();

            if (originLocation != null && originLocation.Status == TDLocationStatus.Valid
                && !originLocation.Accessible)
            {
                sb.Append(GetResource("FindNearestAccessibleStop.SubHeading.Origin"));
                sb.Append(", ");
            }

            if (destinationLocation != null && destinationLocation.Status == TDLocationStatus.Valid
                && !destinationLocation.Accessible)
            {
                sb.Append(GetResource("FindNearestAccessibleStop.SubHeading.Destination"));
                sb.Append(", ");
            }

            if (viaLocation != null && viaLocation.Status == TDLocationStatus.Valid
                && !viaLocation.Accessible)
            {
                sb.Append(GetResource("FindNearestAccessibleStop.SubHeading.Via"));
                sb.Append(", ");
            }

            // Remove trailing ,
            string locationString = sb.ToString().Trim().TrimEnd(',');

            // Replace last "," with "and"
            int idx = locationString.LastIndexOf(',');

            if (idx > 0)
            {
                locationString = locationString.Remove(idx, 1).Insert(idx, " " + GetResource("FindNearestAccessibleStop.SubHeading.And"));
            }

            return locationString;
        }

        /// <summary>
        /// Displays any overall errors where necessary
        /// </summary>
        private void ShowErrors()
        {
            if (!panelErrorDisplayControl.Visible)
            {
                // If no accessible stop types selected, display error
                if (accessibleTransportTypesControl.AccessibleModes.Length == 0)
                {
                    errorResourceIds.Add("FindNearestAccessibleStop.SelectStopType.Error");
                }
                // If all accessible stop types selected, and at least one location
                // has no accessible locations found, display error
                else if (
                    (accessibleTransportTypesControl.ModesAccessibleTransportAllChecked)
                    && (
                        (!originTDANControl.LocationChoicesFound)
                        || (!destinationTDANControl.LocationChoicesFound)
                        || (viaTDANControl.Visible && !viaTDANControl.LocationChoicesFound)
                        )
                    )
                {
                    errorResourceIds.Add("FindNearestAccessibleStop.NoAccessibleLocations.Error");
                }
                // If at least one accessible stop type is selected, and at least one location
                // has no accessible locations found, display error
                else if (
                    (accessibleTransportTypesControl.AccessibleModes.Length > 0)
                    && (
                        (!originTDANControl.LocationChoicesFound)
                        || (!destinationTDANControl.LocationChoicesFound)
                        || (viaTDANControl.Visible && !viaTDANControl.LocationChoicesFound)
                        )
                    )
                {
                    errorResourceIds.Add("FindNearestAccessibleStop.ChangeStopType.Error");
                }

                ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, errorResourceIds);
            }
        }

        /// <summary>
        /// Updates TDPage with the partial postback (AJAX) values to allow logging of event
        /// </summary>
        /// <param name="pageIdPostback"></param>
        private void SetPartialPostbackValues(PageId pageIdPostback)
        {
            if (scriptManager1.IsInAsyncPostBack)
            {
                this.PageIdPostback = pageIdPostback;
                this.IsPartialPostback = true;
            }
        }

        /// <summary>
        /// Sets up the control visibility
        /// </summary>
        private void SetupControlVisibility()
        {
            // If any of the locations have no accessible choices (assuming they are not accessible),
            // then do not show the next button
            commandSubmit.Visible = true; // Default

            if ((!originTDANControl.LocationChoicesFound)
                || (!destinationTDANControl.LocationChoicesFound)
                || (viaTDANControl.Visible && !viaTDANControl.LocationChoicesFound))
            {
                commandSubmit.Visible = false;
            }
        }

        /// <summary>
        /// Method which returns true if user is a higher-level (e.g. CJP) user 
        /// </summary>
        private static bool IsCJPUser()
        {
            bool userIsLoggedOn = TDSessionManager.Current.Authenticated;

            // Get the user's type
            int userType = userIsLoggedOn ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;

            return (userType > 0);
        }

		#endregion

        #region Web Form Designer generated code

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
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
