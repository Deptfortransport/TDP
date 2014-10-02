// *********************************************** 
// NAME                 : FindTDANControl.ascx
// AUTHOR               : David Lane
// DATE CREATED         : 28/11/2012 
// DESCRIPTION          : Find TDAN control. Enables user to find the nearest accessible stop
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindTDANControl.ascx.cs-arc  $ 
//
//   Rev 1.7   Jan 29 2013 13:02:24   mmodi
//Display select this stop link in the accessible stops map 
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.6   Jan 29 2013 11:30:42   dlane
//Removing localities from via locations
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.5   Jan 15 2013 10:35:28   mmodi
//Specify a search distance override for find accessible stop
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Jan 09 2013 16:10:22   mmodi
//Error message display updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Jan 09 2013 11:43:28   mmodi
//Find accessible location updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Jan 04 2013 15:40:46   mmodi
//Updates for Find nearest accessible stops page display and logic
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Dec 18 2012 16:54:58   DLane
//Accessible JP updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Dec 07 2012 15:59:06   DLane
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using Logger = System.Diagnostics.Trace;
using LSC = TransportDirect.UserPortal.LocationService.Cache;
using System.Globalization;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Location control
    /// </summary>
    public partial class FindTDANControl : TDUserControl
    {
        #region Private members

        private TDJourneyParametersMulti journeyParameters;
        private TDLocation locationToSearch = null;
        private TDLocation location = null;
        private List<TDLocationAccessible> locationChoices = null;

        private bool locationIsAccessible = false;
        private List<TDStopType> accessibleStopTypes = null;

        private bool showMapButton = true;

        private int searchDistanceOverride = -1;
        private bool via = false;
                
        #endregion

        #region Public Events
        
        public event EventHandler MapLocationClick;

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        protected FindTDANControl()
        {
        }

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        /// <summary>
        /// Page_Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupControls();
        }
        
        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupControlVisibility();

            RegisterJavascripts();

            SetupResources();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Map button event handler, raises event for parent controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void findOnMapBtn_Click(object sender, EventArgs e)
        {
            if (MapLocationClick != null)
                MapLocationClick(sender, e);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get/Sets the TDLocation represented by the location control
        /// </summary>
        public TDLocation Location
        {
            get 
            {
                if (locationIsAccessible)
                {
                    // The Location this control was initialised with is accessible, so return that
                    return locationToSearch;
                }

                // If nothing selected, don't do anything
                // get the location from the list
                if (locationDrop.SelectedIndex <= 0)
                {
                    return null;
                }
                else
                {
                    LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];

                    TDLocation location = new TDLocation();
                    LocationSearch search = new LocationSearch();

                    string locationId = string.Empty, locationName = string.Empty;
                    TDStopType locationType = TDStopType.Unknown;
                    StationType stationType = StationType.Undetermined;

                    GetAccessibleDropDownValues(ref locationId, ref locationType, ref locationName);

                    search.InputText = locationName;
                    // Update search type for locality stop, as a LocalityGazetteer call will be done,
                    // if it is a stop naptan, then a NaptanCache lookup is done so no need to update searchtype
                    if (locationType == TDStopType.Locality)
                        search.SearchType = SearchType.Locality;
                                        
                    // Use the LocationService to resolve the location - the dropdown will contain the location id and type
                    locationService.ResolveLocation(ref search, ref location,
                        locationId, locationType, stationType,
                        journeyParameters.MaxWalkingTime, journeyParameters.WalkingSpeed,
                        false, TDSessionManager.Current.Session.SessionID, TDSessionManager.Current.Authenticated);

                                        
                    location.Status = TDLocationStatus.Valid;
                    location.Accessible = true;

                    return location;
                }
            }
            set { location = value; }
        }

        /// <summary>
        /// Gets the Accessible location choices displayed in this control
        /// </summary>
        public List<TDLocationAccessible> LocationChoices
        {
            get
            {
                // Return from the populated accessible location choices,
                if (locationChoices != null && locationChoices.Count > 0)
                {
                    return locationChoices;
                }
                else
                {
                    // Otherwise, no accessible locations exist
                    return new List<TDLocationAccessible>();
                }
            }
        }

        /// <summary>
        /// Gets the location choices dropdown list
        /// </summary>
        public DropDownList LocationDrop
        {
            get { return locationDrop; }
        }

        /// <summary>
        /// Gets if Accessible location choices were found. Returns false only if 
        /// the location is not accessible and no location choices found.
        /// </summary>
        public bool LocationChoicesFound
        {
            get
            {
                if (!locationIsAccessible
                    && (locationChoices == null || locationChoices.Count == 0))
                {
                    return false ;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets/Sets the Show Map button visibility
        /// </summary>
        public bool ShowMapButton
        {
            get { return showMapButton; }
            set { showMapButton = value; }
        }

        /// <summary>
        /// Get/Set. Search distance override to search accessible locations on
        /// </summary>
        public int SearchDistanceOverride
        {
            get { return searchDistanceOverride; }
            set { searchDistanceOverride = value; }
        }

        #endregion
       
        #region Public Methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(TDLocation locationToSearch, List<TDStopType> accessibleStopTypes, 
            TDJourneyParametersMulti journeyParameters, TDLocationAccessible[] locationAccessibleChoices, bool via)
        {
            this.locationToSearch = locationToSearch;
            this.accessibleStopTypes = accessibleStopTypes;
            this.journeyParameters = journeyParameters;
            this.via = via;

            if (locationAccessibleChoices != null)
                this.locationChoices = new List<TDLocationAccessible>(locationAccessibleChoices);

            if (locationToSearch != null)
            {
                if (locationToSearch.Accessible)
                {
                    locationIsAccessible = true;
                }
                else
                {
                    locationIsAccessible = false;
                }
            }
        }

        /// <summary>
        /// Forces a refresh of this control, repopulates the accessible locations dropdown
        /// </summary>
        public void Refresh()
        {
            SetupAccessibleLocationsDropDown(via);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Registers the javascripts required to show auto suggest functionality
        /// </summary>
        private void RegisterJavascripts()
        {
            TDPage currentPage = (TDPage)Page;

            string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];

            ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

            // Javascript files to provide location suggest
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Common", scriptRepository.GetScript("Common", javaScriptDom));
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "JQuery", scriptRepository.GetScript("JQuery", javaScriptDom));
        }

        /// <summary>
        /// Sets up resource string from content database
        /// </summary>
        private void SetupResources()
        {
            findOnMapBtn.Text = GetResource("FindTDANControl.MapButton.Text");
        }

        /// <summary>
        /// Sets up the controls
        /// </summary>
        private void SetupControls()
        {
            // Display text
            if (locationToSearch != null)
            {
                locationName.Text = locationToSearch.Description;
            }

            // Display accessible locations in the drop down
            if (!Page.IsPostBack)
            {
                SetupAccessibleLocationsDropDown(via);
            }
        }

        /// <summary>
        /// Sets the control visibilities
        /// </summary>
        private void SetupControlVisibility()
        {
            // Visibilities
            // If the locationSearch is accessible then only display the location and 
            // hide the drop down list and map button
            if (locationIsAccessible)
            {
                locationDropDownRow.Visible = false;
                findOnMapBtn.Visible = false;
            }
            else
            {
                // No accessible locations found, display an error
                if (locationChoices == null || locationChoices.Count == 0)
                {
                    locationDropDownRow.Visible = false;
                    findOnMapBtn.Visible = false;

                    locationErrorRow.Visible = true;
                    locationError.Text = GetResource("FindTDANControl.AccessibleLocationsNotFound.Error");
                }
                else
                {
                    // Accessible locations exist
                    locationDropDownRow.Visible = true;
                    findOnMapBtn.Visible = true && showMapButton;
                }
            }
        }

        /// <summary>
        /// Sets list of nearby accessible locations
        /// </summary>
        private void SetupAccessibleLocationsDropDown(bool via)
        {
            if (!locationIsAccessible && locationToSearch != null)
            {
                if (via && accessibleStopTypes.Contains(TDStopType.Locality))
                {
                    accessibleStopTypes.Remove(TDStopType.Locality);
                }

                // Call service to find the accessible locations
                locationChoices = LocationSearchHelper.FindAccessibleLocations(
                            ref locationToSearch, 
                            journeyParameters.RequireStepFreeAccess, 
                            journeyParameters.RequireSpecialAssistance,
                            accessibleStopTypes,
                            TDDateTime.Parse(string.Format("{0} {1} {2}:{3}",
                                journeyParameters.OutwardDayOfMonth,
                                journeyParameters.OutwardMonthYear,
                                journeyParameters.OutwardHour,
                                journeyParameters.OutwardMinute), CultureInfo.CurrentCulture),
                            searchDistanceOverride);
                
                // If location choices exist, populate dropdown
                if (locationChoices != null && locationChoices.Count > 0)
                {
                    // Display accessible locations
                    PopulateAccessibleDropDown(locationChoices, locationDrop.SelectedIndex);
                }
            }
        }

        /// <summary>
        /// Sets up the TDAN dropdownlist 
        /// </summary>
        /// <param name="choices">LocationChoiceList</param>
        /// <param name="index">the option to set as selected</param>
        private void PopulateAccessibleDropDown(List<TDLocationAccessible> locationChoices, int selectedIndex)
        {
            // Clear existing list
            locationDrop.Items.Clear();

            // Show list
            locationDrop.Visible = true;

            ListItem item;

            string itemPleaseSelect = GetResource("FindTDANControl.itemPleaseSelect");

            // The first item in the drop down list should be "please select"
            item = new ListItem(itemPleaseSelect, LocationSearchHelper.DEFAULT_ITEM);
            locationDrop.Items.Add(item);

            int i = 0;

            foreach (TDLocationAccessible location in locationChoices)
            {
                i++;

                // Display text is set to location name (upto 50 chars), with number and distance values 
                // to allow user to view on map and provide context from their chosen location
                item = new ListItem(
                    string.Format("{0}. {1} ({2} {3})", 
                        i, 
                        (location.Description.Length <= 50) ? location.Description : location.Description.Substring(0, 50), 
                        GetMilesDistance(location.DistanceFromSearchOSGR),
                        GetResource("RouteText.Miles")),
                    string.Format("{0}|{1}|{2}", location.ID, location.StopType.ToString(), location.Description )
                    );
                locationDrop.Items.Add(item);
            }

            if (selectedIndex < locationDrop.Items.Count)
                locationDrop.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// Retrieves the selected location values from the Accessible location dropdown
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="locationType"></param>
        /// <param name="locationName"></param>
        private void GetAccessibleDropDownValues(ref string locationId, ref TDStopType locationType, ref string locationName)
        {
            if (locationDrop.SelectedIndex > 0)
            {
                try
                {
                    string[] values = locationDrop.SelectedItem.Value.Split('|');

                    locationId = values[0];
                    locationType = (TDStopType)Enum.Parse(typeof(TDStopType), values[1]);
                    locationName = values[2];
                }
                catch
                {
                    // Ignore exception, this is a server controlled list, and any tampering should be rejected!
                }
            }
        }

        /// <summary>
        /// Returns the miles distance as string
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        private string GetMilesDistance(double distance)
        {
            double miles = MeasurementConversion.ConvertValue(distance, ConversionType.MetresToMileage);

            string milesRounded = Math.Round(miles, 1, MidpointRounding.AwayFromZero).ToString();

            return milesRounded;
        }

        #endregion
    }
}