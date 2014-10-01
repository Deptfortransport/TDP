// *********************************************** 
// NAME             : LocationControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Feb 2012
// DESCRIPTION  	: Location User control
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Common.Web;
using TDP.Common.EventLogging;
using Logger = System.Diagnostics.Trace;
using TDP.Common.LocationService.Gazetteer;
using System.Text;

namespace TDP.UserPortal.TDPMobile.Controls
{
    #region Public Event Definition

    /// <summary>
    /// Delegate for Toggle location button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ToggleLocation(object sender, EventArgs e);

    #endregion

    /// <summary>
    /// Location user control
    /// </summary>
    public partial class LocationControl : System.Web.UI.UserControl
    {
        #region Public Events

        public event ToggleLocation OnToggleLocation;

        #endregion

        #region Private Fields

        private LocationService locationService = null;
        private LocationHelper locationHelper;

        private TDPLocationType locationType = TDPLocationType.Unknown;
        private LocationStatus status = LocationStatus.Unspecified;
        private LocationSearch search = null;
        private TDPLocation location = null;
        private bool resolveLocation = true;
        private bool? isVenueOnly = null;
        private bool isDestinationLocation = false;

        private bool showCurrentLocation = false;
        private bool showToggleLocation = false;

        private bool addInvalidStyle = false;

        private string validationMessage = string.Empty;

        // Used when toggling locations.
        // Must match string javascript toggle locations method
        private const string updatingString = "Updating...";
        private const string mylocationString = "My Location";

        #region Resource Strings

        private string locationDropDownDefaultItem = string.Empty;
        private string locationDropDownMoreItem = string.Empty;
        private string venueDropDownDefaultItem = string.Empty;

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write property for the search details for this location control
        /// </summary>
        public LocationSearch Search
        {
            get { return search; }
            set { search = value; }
        }

        /// <summary>
        /// Read/Write property indicates if the location should be resolved when Validate is called.
        /// This is to allow a pre-resolved location set during a landing page to bypass resolution when
        /// autoplan is in effect (as the control will not have gone through its life cycle and therefore not 
        /// fully setup).
        /// Default is true.
        /// </summary>
        public bool ResolveLocation
        {
            get { return resolveLocation; }
            set { resolveLocation = value; }
        }

        /// <summary>
        /// Get/Sets the TDPLocation represented by the location control
        /// </summary>
        public TDPLocation Location
        {
            get { return location; }
            set
            {
                location = value;
                if (location != null)
                {
                    status = LocationStatus.Resolved;
                    locationType = location.TypeOfLocation;
                }
            }
        }

        /// <summary>
        /// Read only property determining status fo the location control
        /// </summary>
        public LocationStatus Status
        {
            get {

                // If ambiguous and no amibiguous locations found then let controls know 
                // location is not resolved
                if (status == LocationStatus.Ambiguous && ambiguityDrop.Items.Count == 0)
                {
                    return LocationStatus.Unspecified;
                }
                else
                {
                    return status;
                }
            }
        }

        /// <summary>
        /// Read/Write property determining type of location represented by location control
        /// </summary>
        public TDPLocationType TypeOfLocation
        {
            get
            {
                // Location type could have been updated by js
                if (IsPostBack
                    && (locationInput_hdnType.Value != string.Empty)
                    && !ambiguityRow.Visible
                    && (locationType == TDPLocationType.Unknown))
                {
                    locationType = TDPLocationTypeHelper.GetTDPLocationTypeJS(locationInput_hdnType.Value);
                }
                // For non-js users, when venue dropdown is used that indicates its a venue location type
                else if (IsPostBack
                    && (!(jsEnabled.Value.Parse(true)))
                    && (venueDropdownNonJS.Items.Count > 0)
                    && (venueDropdownNonJS.Visible))
                {
                    locationType = TDPLocationType.Venue;
                }

                return locationType;
            }
            set { locationType = value; }
        }

        #region Public Control access

        /// <summary>
        /// Read/Write. Label shown against location input
        /// </summary>
        public Label LocationDirectionLabel
        {
            get { return locationDirectionLbl; }
            set { locationDirectionLbl = value; }
        }

        /// <summary>
        /// Read/Write. TextBox for location input
        /// </summary>
        public TextBox LocationInputTextBox
        {
            get { return locationInput; }
            set { locationInput = value; }
        }

        /// <summary>
        /// Read/Write. Toggle location button
        /// </summary>
        public Button TravelFromToToggle
        {
            get { return travelFromToToggle; }
            set { travelFromToToggle = value; }
        }

        #endregion

        /// <summary>
        /// Sets this control so that it only allows venues to be selected
        /// </summary>
        public bool IsVenueOnly
        {
            get 
            {
                if (!isVenueOnly.HasValue)
                {
                    isVenueOnly = venueOnly.Value.Parse(false);
                }

                return isVenueOnly.Value;
            }
            set
            {
                isVenueOnly = value;
                venueOnly.Value = value.ToString();
            }
        }

        /// <summary>
        /// Read only. Contains any error or validation message returned by the location resolution proces
        /// </summary>
        public string ValidationMessage
        {
            get { return validationMessage; }
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
            locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

            locationHelper = new LocationHelper();
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetupResources();

            RegisterJavascripts();
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            PopulateVenueDropDown();

            if (IsVenueOnly)
            {
                SetForVenueLocations();
            }
            else
            {
                SetForInputLocation();
            }

            SetupResourcesForControls();

            SetInvalidStyle();

            ShowHideControls();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Reset button event handler
        /// Resets the location in the event of reset button clicked when ambiguity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ResetLocation(object sender, EventArgs e)
        {
            Reset(locationType);
        }

        /// <summary>
        /// Event handler for the toggle locations click
        /// </summary>
        protected void TravelFromToToggle_Click(object sender, EventArgs e)
        {
            if (OnToggleLocation != null)
            {
                OnToggleLocation(this, null);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialises the location control
        /// </summary>
        /// <param name="searchString">Location to be search</param>
        /// <param name="locationType">Type of location</param>
        public void Initialise(TDPLocation location, LocationSearch search,
            bool isDestinationLocation, bool showCurrentLocation, bool showToggleLocation,
            bool isLandingPage)
        {
            this.Location = location;
            this.Search = search;

            this.isDestinationLocation = isDestinationLocation;
            this.showCurrentLocation = showCurrentLocation;
            this.showToggleLocation = showToggleLocation;

            // Update status if necessary (could be from landing page with ambiguous search)
            if (isLandingPage && location == null && search != null)
            {
                if (search.LocationQueryResultsExist || search.LocationCacheResultsExist)
                {
                    status = LocationStatus.Ambiguous;

                    UpdateValidationMessage();
                }
            }
        }

        /// <summary>
        /// Reset location to type of location specified
        /// </summary>
        /// <param name="typeOfLocation">Type of location</param>
        public void Reset(TDPLocationType typeOfLocation)
        {
            locationType = typeOfLocation;

            PopulateVenueDropDown();

            status = LocationStatus.Unspecified;
            location = null;
            search = new LocationSearch();

            locationInputDiv.Visible = true;
            locationInput.Visible = true;
            locationInput_hdnValue.Visible = true;
            locationInput_hdnType.Visible = true;

            locationInput.Text = string.Empty;
            locationInput_hdnValue.Value = string.Empty;
            locationInput_hdnType.Value = string.Empty;

            locationDescriptionDiv.Visible = true;
            venueInputPageLnk.Visible = true;
            venueSelectControl.Visible = true;
            venueDropdownNonJS.Visible = false;
            currentLocationDiv.Visible = showCurrentLocation;
            travelFromToToggleDiv.Visible = showToggleLocation; 

            ambiguityDrop.Items.Clear();
            ambiguityRow.Visible = false;
            resetDiv.Visible = false;
            resetButton.Visible = false;
            resetButtonNonJS.Visible = false;

            // If location type is venue, and venues are available, 
            // then only allow venues to be selected
            if (typeOfLocation == TDPLocationType.Venue)
            {
                IsVenueOnly = locationHelper.VenueLocationsAvailable();
            }
        }

        /// <summary>
        /// Validates the location control and determines the current status of it
        /// </summary>
        /// <returns>True if the location gets resolved</returns>
        public bool Validate()
        {
            return Validate(TypeOfLocation);
        }

        /// <summary>
        /// Validates the location control and determines the current status of it
        /// </summary>
        /// <returns>True if the location gets resolved</returns>
        public bool Validate(TDPLocationType locationType)
        {
            bool js = jsEnabled.Value.Parse(true);

            if (locationType == TDPLocationType.Venue)
            {
                #region Validate Venue location

                if (resolveLocation)
                {
                    if (locationInput_hdnValue.Value != "" && js)
                    {
                        search = new LocationSearch(string.Empty, locationInput_hdnValue.Value, TDPLocationType.Venue, js);
                        
                        location = locationHelper.GetLocation(search);
                        
                        // Assume venue location will be returned
                        status = LocationStatus.Resolved;
                    }
                    else if (venueDropdownNonJS.Items.Count > 0 && !js)
                    {
                        if (venueDropdownNonJS.SelectedIndex == 0)
                        {
                            // Status will remain unresolved/ambiguous
                        }
                        else
                        {
                            if (locationHelper != null)
                            {
                                search = new LocationSearch(string.Empty, venueDropdownNonJS.SelectedValue, TDPLocationType.Venue, js);

                                location = locationHelper.GetLocation(search);

                                // Assume venue location will be returned
                                status = LocationStatus.Resolved;
                            }
                        }
                    }
                }
                else
                {
                    // Location was supplied to control and was considered resolved, so check it is
                    if (IsValidLocation(location))
                    {
                        status = LocationStatus.Resolved;
                    }
                }

                #endregion
            }
            else
            {
                #region Validate location

                if (resolveLocation)
                {
                    try
                    {
                        #region Search should only be done if user has changed input

                        bool performLocationSearch = true;

                        // If location is resolved, only resolve the location if user has changed 
                        // the input text and/or selected another location
                        if (location != null && status == LocationStatus.Resolved
                            && !string.IsNullOrEmpty(locationInput.Text.Trim()))
                        {
                            if (location.DisplayName == locationInput.Text.Trim()
                                && !IsIgnoreSearchString(locationInput.Text.Trim()))
                            {
                                performLocationSearch = false;
                            }
                            else if (!string.IsNullOrEmpty(locationInput_hdnValue.Value.Trim())
                                && location.ID == locationInput_hdnValue.Value.Trim()
                                && js)
                            {
                                performLocationSearch = false;
                            }
                        }

                        #endregion

                        if (performLocationSearch)
                        {
                            #region Input search text, id, and search type

                            // Reset location text if it matches any ignore strings (e.g. watermark text)
                            if (IsIgnoreSearchString(locationInput.Text.Trim()))
                            {
                                locationInput.Text = string.Empty;
                            }

                            string searchString = locationInput.Text.Trim();

                            // Any errors parsing, allow application to capture as that would indicate javascript may have been tampered with
                            TDPLocationType searchType = TDPLocationTypeHelper.GetTDPLocationTypeJS(locationInput_hdnType.Value);
                            string searchId = locationInput_hdnValue.Value;

                            // Check for postcode location and update search type,
                            // this will allow location resolution to fall into the appropriate logic
                            if (searchString.IsValidPostcode() || searchString.IsValidPartPostcode())
                            {
                                searchType = TDPLocationType.Postcode;
                            }
                            else if (searchString.IsContainsPostcode() && searchString.IsNotSingleWord())
                            {
                                searchType = TDPLocationType.Address;
                            }

                            // Fix for toggle locations when My location was selected but was not resolved (page was not postback before toggle selected)
                            if ((searchType == TDPLocationType.CoordinateLL || (searchType == TDPLocationType.CoordinateEN))
                                && string.IsNullOrEmpty(searchString))
                            {
                                searchString = mylocationString;
                            }
                            
                            #endregion

                            // if location is set from javascript dropdown (or used the current location button)
                            if (!string.IsNullOrEmpty(locationInput_hdnType.Value.Trim())
                                && !string.IsNullOrEmpty(locationInput_hdnValue.Value.Trim())
                                && js)
                            {
                                #region Javascript selected location search

                                // Create a new search object contained user entered values
                                search = new LocationSearch(searchString, searchId, searchType, js);

                                location = locationHelper.GetLocation(search);

                                #endregion
                            }
                            // if location is not selected from auto suggest (javascript and non-javascript users)
                            else if (!ambiguityRow.Visible)
                            {
                                #region Non-javascript location search

                                // Override search type if js is disabled, and the search text entered isn't a postcode
                                if (!js)
                                {
                                    if (searchType != TDPLocationType.Postcode && searchType != TDPLocationType.Address)
                                    {
                                        searchType = TDPLocationType.Unknown;
                                    }
                                }

                                // Create a new search object contained user entered values
                                search = new LocationSearch(searchString, string.Empty, searchType , false);

                                location = locationHelper.GetLocation(search);

                                #endregion
                            }
                            else // if user is selecting location from ambiguity state
                            {
                                #region Ambiguous location selection

                                // LocationControl should have been populated with the 
                                // search object containing the ambiguous locations

                                location = locationHelper.GetAmbiguityLocation(ref search, ambiguityDrop, js);

                                #endregion
                            }
                        }
                    }
                    catch (TDPException tdEx)
                    {
                        if (!tdEx.Logged)
                        {
                            Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error,
                                string.Format("Error resolving location: {0}", tdEx.Message), tdEx));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error,
                                string.Format("Error resolving location: {0}", ex.Message), ex));
                    }
                }

                // Check location is valid
                if (IsValidLocation(location))
                {
                    status = LocationStatus.Resolved;
                }
                else
                {
                    status = LocationStatus.Ambiguous;

                    UpdateValidationMessage();
                }

                #endregion
            }

            // Let control know invalid css style should be applied to the input
            this.addInvalidStyle = (status != LocationStatus.Resolved);

            return (status == LocationStatus.Resolved);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets location for Input 
        /// </summary>
        private void SetForInputLocation()
        {
            if (status == LocationStatus.Ambiguous)
            {
                SetForAmbiguousLocation();

                // Ensure hidden values are cleared to allow postback to resolve the 
                // selected ambiguous location, rather than falling into the resolve js location
                locationInput_hdnValue.Value = string.Empty;
                locationInput_hdnType.Value = string.Empty;
            }
            else if (status == LocationStatus.Resolved && location != null)
            {
                locationInput.Text = location.DisplayName;
            }

            if (status != LocationStatus.Ambiguous && location != null)
            {
                // Set the hidden location type and value settings 
                // check if js enabled on postback 
                if (!IsPostBack)
                {
                    locationInput_hdnValue.Value = location.ID;
                    locationInput_hdnType.Value = location.TypeOfLocation.ToString();

                    // If the location object is not resolved, then ensure the control
                    // displays the input text (e.g. could be returning back from ambiguity page
                    // to input and therefore want to display the input text that caused
                    // the ambiguity)
                    if (string.IsNullOrEmpty(locationInput.Text.Trim()))
                    {
                        if (!string.IsNullOrEmpty(location.DisplayName))
                        {
                            locationInput.Text = location.DisplayName;
                        }
                        else if (search != null && !string.IsNullOrEmpty(search.SearchText))
                        {
                            locationInput.Text = search.SearchText;
                        }
                    }
                }
                else if (jsEnabled.Value.Parse(true) && (status == LocationStatus.Resolved))
                {
                    // Only update hidden values for resolved location, as postback could happen
                    // before user has "submitted" and therefore selected javasript location
                    // may not have been resolved
                    locationInput_hdnValue.Value = location.ID;
                    locationInput_hdnType.Value = location.TypeOfLocation.ToString();
                }
            }

            // Add location specific class and remove venue input specific classs
            SetInputStyle("locationBox", "locationBoxVenue");
        }

        #region Ambiguous location
        
        /// <summary>
        /// Sets location for ambiguity situation
        /// </summary>
        private void SetForAmbiguousLocation()
        {
            // Ambiguous locations will either be from the "auto-suggest" ambiguous search,
            // or from the standard Gaz ambiguous search (there shouldn't be both)
                        
            // If location choices exist, populate ambiguity dropdown,
            // (there could be an empty choice list if the text searched 
            // for found none or could be null for an empty text search)
            if (search != null && search.LocationQueryResultsExist)
            {
                // Get location choices found for the search 
                LocationChoiceList locationChoices = search.GetLocationChoices(search.CurrentLevel());
                                
                SetupAmbiguityGazDropDown(locationChoices, ambiguityDrop, ambiguityDrop.SelectedIndex, search.SupportHierarchic);
            }
            // If cached location exists, populate ambiguity dropdown
            else if (search != null && search.LocationCacheResultsExist)
            {
                // Else it's an "auto-suggest" ambiguous locations list (because user hadn't selected from
                // the auto suggest dropdown)
                SetupAmbiguityLocDropDown(search.GetLocationCacheResult(), ambiguityDrop, ambiguityDrop.SelectedIndex);
            }
            else
            {
                // No ambiguous location choices

                // Set input error style 
                SetInvalidStyle();

                // Ensure location input continues to show the search input text
                if (string.IsNullOrEmpty(locationInput.Text.Trim()) && search != null)
                {
                    locationInput.Text = search.SearchText;
                }
            }
        }

        /// <summary>
        /// Setsup the ambiguity dropdownlist with the description and the index of the choice in the list
        /// </summary>
        /// <param name="choices">LocationChoiceList</param>
        /// <param name="list">list to populate</param>
        /// <param name="index">the option to set as selected</param>
        /// <param name="isHierarchic">true if the drop down contains drillable options</param>
        private void SetupAmbiguityGazDropDown(LocationChoiceList choices, DropDownList list, int selectedIndex, bool isHierarchic)
        {
            // Clear existing list
            list.Items.Clear();
                        
            int i = 0;
            ListItem item;
            StringBuilder itemText = new StringBuilder(string.Empty);
            StringBuilder itemValue = new StringBuilder(string.Empty);

            // The first item in the drop down list should be "please select"
            item = new ListItem(locationDropDownDefaultItem, LocationHelper.DEFAULT_ITEM);
            list.Items.Add(item);

            foreach (LocationChoice choice in choices)
            {
                // For hierarchic searches add options for drillable locations
                if (isHierarchic)
                {
                    // If option is not an admin area, add option to select the location.
                    // A choice which is an admin area should only appear as an option to drilldown
                    // otherwise it can appear as both a location to select and a location
                    // to drilldown to
                    if (!choice.IsAdminArea)
                    {
                        item = new ListItem(choice.Description, i.ToString());
                        list.Items.Add(item);
                    }
                    // If location has children, add "More options for" item to drilldown
                    if (choice.HasChilden)
                    {
                        itemText = new StringBuilder();
                        itemText.Append(locationDropDownMoreItem);
                        itemText.Append(" ");
                        itemText.Append(choice.Description);
                        itemText.Append("...");

                        itemValue = new StringBuilder();
                        itemValue.Append("+");
                        itemValue.Append(i.ToString());

                        item = new ListItem(itemText.ToString(), itemValue.ToString());

                        list.Items.Add(item);
                    }
                }
                else
                {
                    // If location has children, add as an item to drilldown
                    itemValue = new StringBuilder();
                    if (choice.HasChilden)
                        itemValue.Append("+");
                    itemValue.Append(i.ToString());

                    item = new ListItem(choice.Description, itemValue.ToString());
                    
                    list.Items.Add(item);
                }
                i++;
            }

            if (selectedIndex < list.Items.Count)
                list.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// Setsup the ambiguity dropdownlist with the locations in the list
        /// </summary>
        /// <param name="choices">List<TDLocation></param>
        /// <param name="list">list to populate</param>
        /// <param name="index">the option to set as selected</param>
        private void SetupAmbiguityLocDropDown(IList<TDPLocation> choices, DropDownList list, int selectedIndex)
        {
            bool isDebug = DebugHelper.ShowDebug;

            // Clear existing list
            list.Items.Clear();
                        
            int i = 0;
            ListItem item;
            StringBuilder itemText = new StringBuilder(string.Empty);
            StringBuilder itemValue = new StringBuilder(string.Empty);

            // The first item in the drop down list should be "please select"
            item = new ListItem(locationDropDownDefaultItem, LocationHelper.DEFAULT_ITEM);
            list.Items.Add(item);

            string displayName = null;

            foreach (TDPLocation choice in choices)
            {
                displayName = choice.DisplayName;

                if (isDebug)
                {
                    displayName += string.Format(" - t[{0}] id[{1}]",
                        choice.TypeOfLocationActual.ToString(),
                        choice.ID);
                }

                item = new ListItem(displayName, i.ToString());
                list.Items.Add(item);

                i++;
            }
            if (selectedIndex < list.Items.Count)
                list.SelectedIndex = selectedIndex;

            if (isDebug)
            {
                validationMessage += string.Format("<br /><span class=\"debug\">Total[{0}]<br />Grp[{1}] Rail[{2}] Loc[{3}] Coach[{4}] TMU[{5}] Ferry[{6}] Air[{7}] Pcde[{8}] Other[{9}]</span>",
                    choices.Count,
                    choices.Count(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationGroup; }),
                    choices.Count(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationRail; }),
                    choices.Count(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.Locality; }),
                    choices.Count(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationCoach; }),
                    choices.Count(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationTMU; }),
                    choices.Count(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationFerry; }),
                    choices.Count(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationAirport; }),
                    choices.Count(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.Postcode || loc.TypeOfLocationActual == TDPLocationType.Address; }),
                    choices.Count(delegate(TDPLocation loc)
                    {
                        return (loc.TypeOfLocationActual == TDPLocationType.Unknown)
                               || (loc.TypeOfLocationActual == TDPLocationType.Venue)
                               || (loc.TypeOfLocationActual == TDPLocationType.Station);
                    })
                    );
            }
        }

        #endregion

        /// <summary>
        /// Sets location control to show venue dropdowns
        /// </summary>
        private void SetForVenueLocations()
        {
            if (location != null && status == LocationStatus.Resolved)
            {
                venueSelectControl.List.SelectedIndex = venueSelectControl.List.Items.IndexOf(venueSelectControl.List.Items.FindByValue(location.ID.Trim()));

                if (venueDropdownNonJS.Items.Count > 0)
                {
                    venueDropdownNonJS.SelectedIndex = venueDropdownNonJS.Items.IndexOf(venueDropdownNonJS.Items.FindByValue(location.ID.Trim()));
                }

                locationInput.Text = location.DisplayName;
                locationInput_hdnValue.Value = location.ID;
                locationInput_hdnType.Value = location.TypeOfLocation.ToString();
            }

            // Add venue specific class and remove location input specific classs
            SetInputStyle("locationBoxVenue", "locationBox");
        }

        /// <summary>
        /// Adds and removes the styling to the input control
        /// </summary>
        private void SetInputStyle(string addClass, string removeClass)
        {
            // Add location input class
            if (!locationInput.CssClass.Contains(addClass))
            {
                locationInput.CssClass += string.Format(" {0}", addClass);
            }
            // Remove location input class
            if (locationInput.CssClass.Contains(removeClass))
            {
                locationInput.CssClass = locationInput.CssClass.Replace(removeClass, string.Empty);
            }

            // If venues are available, then venue link button is displayed so update class
            if (locationHelper.VenueLocationsAvailable())
            {
                if (!locationInput.CssClass.Contains("locationVenueLink"))
                {
                    locationInput.CssClass += string.Format(" {0}", "locationVenueLink");
                }
            }
        }

        /// <summary>
        /// Adds the invalid styling to the input control(s)
        /// </summary>
        private void SetInvalidStyle()
        {
            string invalidStyle = "locationError";

            if ((addInvalidStyle) && (!IsValidLocation(Location)))
            {
                // Input text box
                if (!locationInput.CssClass.Contains(invalidStyle))
                {
                    locationInput.CssClass += " " + invalidStyle;
                }

                // Dropdown if shown for ambiguous
                if (!ambiguityDrop.CssClass.Contains(invalidStyle))
                {
                    ambiguityDrop.CssClass += " " + invalidStyle;
                }
            }
            else
            {
                // Input text box
                if (locationInput.CssClass.Contains(invalidStyle))
                {
                    locationInput.CssClass = locationInput.CssClass.Replace(invalidStyle, string.Empty);
                }

                // Dropdown if shown for ambiguous
                if (!ambiguityDrop.CssClass.Contains(invalidStyle))
                {
                    ambiguityDrop.CssClass = ambiguityDrop.CssClass.Replace(invalidStyle, string.Empty);
                }
            }
        }

        /// <summary>
        /// Populates the Venue dropdown
        /// </summary>
        private void PopulateVenueDropDown()
        {
            // Ensures venue dropdown is loaded during the page lifecycle only 
            // (e.g. this method could be called during a Reset call before control is ready) 
            venueSelectControl.Populate(locationService);
            
            if ((!jsEnabled.Value.Parse(true)) && (locationService != null))
            {
                List<TDPLocation> venues = locationService.GetTDPVenueLocations();

                venueDropdownNonJS.Items.Clear();

                if (Properties.Current["LocationControl.VenueGrouping.Switch"].Parse(true))
                {
                    Dictionary<string, List<TDPVenueLocation>> groupedVenues = new Dictionary<string, List<TDPVenueLocation>>();

                    // To group items for which there is no group specified.
                    // These items will be shown last in the list
                    string defaultGroupName = "NOGROUP";

                    groupedVenues.Add(defaultGroupName, new List<TDPVenueLocation>());

                    foreach (TDPLocation loc in venues)
                    {
                        TDPVenueLocation venue = (TDPVenueLocation)loc;

                        if (string.IsNullOrEmpty(venue.VenueGroupName) && !string.IsNullOrEmpty(venue.Parent))
                        {
                            // If the group name is empty but parent is not empty get the group name from the parent

                            TDPLocation parentVenue = venues.SingleOrDefault(v => v.ID == venue.Parent);

                            if (parentVenue != null)
                            {
                                TDPVenueLocation pvl = (TDPVenueLocation)parentVenue;
                                if (!groupedVenues.Keys.Contains(pvl.VenueGroupName))
                                {
                                    groupedVenues.Add(pvl.VenueGroupName, new List<TDPVenueLocation>());
                                }
                                groupedVenues[pvl.VenueGroupName].Add(venue);
                            }
                            else // no suitable parent found add it as no group
                            {
                                groupedVenues[defaultGroupName].Add(venue);
                            }
                        }
                        else if (!string.IsNullOrEmpty(venue.VenueGroupName))
                        {
                            // group name specified add it will group name
                            if (!groupedVenues.Keys.Contains(venue.VenueGroupName))
                            {
                                groupedVenues.Add(venue.VenueGroupName, new List<TDPVenueLocation>());
                            }
                            groupedVenues[venue.VenueGroupName].Add(venue);
                        }
                        else // No grouping specified add it as no group
                        {
                            groupedVenues[defaultGroupName].Add(venue);
                        }
                    }

                    foreach (string venueGroupName in groupedVenues.Keys)
                    {
                        foreach (TDPVenueLocation vl in groupedVenues[venueGroupName].OrderBy(v => v.DisplayName))
                        {
                            ListItem vItem = new ListItem();
                            vItem.Text = vl.DisplayName;
                            vItem.Value = vl.ID;

                            if (venueGroupName != defaultGroupName)
                                vItem.Attributes.Add("OptionGroup", venueGroupName);

                            venueDropdownNonJS.Items.Add(vItem);
                        }
                    }
                }
                else
                {
                    venueDropdownNonJS.DataSource = venues;
                    venueDropdownNonJS.DataTextField = "DisplayName";
                    venueDropdownNonJS.DataValueField = "Id";
                    venueDropdownNonJS.DataBind();
                }

                venueDropdownNonJS.Items.Insert(0, new ListItem(venueDropDownDefaultItem));
            }
        }

        /// <summary>
        /// Registers the javascripts required to show auto suggest functionality
        /// </summary>
        private void RegisterJavascripts()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            // Javascript settings

            // Get the locations js data
            string version = locationService.LocationVersion();
            string locationListType = Properties.Current["ScriptRepository.LocationSuggest.Script.Name.Mobile"];

            scriptId.Value = string.Format("{0}{1}", locationListType, version);

            // Set path of locations js files
            scriptPath.Value = page.ResolveClientUrl(page.JavascriptPathLocationsData);

            page.AddJavascript("LocationSuggest.js");
        }

        /// <summary>
        /// Sets up resource string from content database
        /// </summary>
        private void SetupResources()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            locationDropDownDefaultItem = page.GetResource("LocationControl.locationDropdown.DefaultItem.Text");
            locationDropDownMoreItem = page.GetResource("LocationControl.locationDropdown.MoreItem.Text");
            venueDropDownDefaultItem = page.GetResource("LocationControl.VenueDropdown.DefaultItem.Text");
        }

        /// <summary>
        /// Sets up controls with resources from the content database
        /// </summary>
        private void SetupResourcesForControls()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            if (IsVenueOnly)
            {
                locationInput.ToolTip = page.GetResource("LocationControl.LocationInput.Tooltip.Venue");
                locationInput_Discription.Text = page.GetResource("LocationControl.LocationInput.Discription.Text.Venue");
                locationInput.Attributes.Add("data-inputdefaultvalue", page.GetResource("LocationControl.LocationInput.Tooltip.Venue"));
            }
            else
            {
                bool venuesAvailable = locationHelper.VenueLocationsAvailable();

                locationInput.ToolTip = venuesAvailable ?
                    page.GetResource("LocationControl.LocationInput.Tooltip.All") :
                    page.GetResource("LocationControl.LocationInput.Tooltip");
                locationInput_Discription.Text = venuesAvailable ?
                    page.GetResource("LocationControl.LocationInput.Discription.Text.All") :
                    page.GetResource("LocationControl.LocationInput.Discription.Text");
                locationInput.Attributes.Add("data-inputdefaultvalue",
                    LocationDirectionLabel.Text == page.GetResourceMobile("JourneyInput.Location.From.Text") ?
                    page.GetResourceMobile("JourneyInput.Location.From.Watermark") :
                    page.GetResourceMobile("JourneyInput.Location.To.Watermark"));
            }

            venueInputPageLnk.Text = page.GetResourceMobile("JourneyInput.Location.Venues.Text");
            venueInputPageLnk.ToolTip = page.GetResourceMobile("JourneyInput.Location.Venues.ToolTip");
            resetButton.Text = page.GetResource("LocationControl.LocationInput.Reset.Clear.Text");
            resetButton.ToolTip = page.GetResource("LocationControl.LocationInput.Reset.Clear.Text");
            resetButtonNonJS.Text = resetButton.Text;
            currentLocationButton.ToolTip = page.GetResource("LocationControl.CurrentLocation.Tooltip");
            clearLocationButton.ToolTip = page.GetResource("LocationControl.ClearLocation.Tooltip");
            travelFromToToggle.Text = string.Empty;
            travelFromToToggle.ToolTip = page.GetResourceMobile("JourneyInput.TravelFromToToggle.ToolTip");
        }

        /// <summary>
        /// Updates the validation messages, should be called after Validate method
        /// </summary>
        private void UpdateValidationMessage()
        {
            if (status == LocationStatus.Ambiguous)
            {
                string messageText = string.Empty;

                TDPMessage tdpMessage = locationHelper.GetLocationValidationMessage(search, isDestinationLocation, IsVenueOnly);

                if (tdpMessage != null && !string.IsNullOrEmpty(tdpMessage.MessageResourceId))
                {
                    TDPPageMobile page = (TDPPageMobile)Page;

                    messageText = page.GetResource(tdpMessage.MessageResourceId);

                    // Check if need to format the string with args for the message
                    if (tdpMessage.MessageArgs != null && tdpMessage.MessageArgs.Count > 0)
                    {
                        messageText = string.Format(messageText, tdpMessage.MessageArgs.ToArray());
                    }
                }

                validationMessage = messageText;
            }
        }

        /// <summary>
        /// Show/Hides controls based on various state of the location control
        /// </summary>
        private void ShowHideControls()
        {
            // Default to input view
            bool showInput = true;
            bool showAmbiguity = false;
            bool showVenue = false;
            bool showVenueOnly = false;

            bool js = jsEnabled.Value.Parse(true);

            // If venues are not available, then only show input mode
            if (locationHelper.VenueLocationsAvailable())
            {
                showVenue = true;
            }

            if (IsVenueOnly)
            {
                showVenueOnly = true;
            }

            // If ambiguous and amibiguous locations found, display them,
            // otherwise continue to display the input
            if (status == LocationStatus.Ambiguous && ambiguityDrop.Items.Count > 0)
            {
                showAmbiguity = true;
                showInput = false;
                showVenue = false;
            }
      
            // Input
            locationInputDiv.Visible = showInput || showVenue;
            locationInput.Visible = showInput || showVenue;
            locationInput_hdnValue.Visible = showInput || showVenue;
            locationInput_hdnType.Visible = showInput || showVenue;

            locationDescriptionDiv.Visible = showInput || showVenue;

            currentLocationDiv.Visible = showCurrentLocation && (showInput && !showVenueOnly);
            travelFromToToggleDiv.Visible = showToggleLocation && (showInput && !showVenueOnly);

            // Ambiguity
            ambiguityRow.Visible = showAmbiguity && !showVenueOnly;
            resetDiv.Visible = showAmbiguity && !showVenueOnly;
            resetButton.Visible = showAmbiguity && !showVenueOnly;
            resetButtonNonJS.Visible = showAmbiguity && !showVenueOnly;

            // Venue
            venueInputPageLnk.Visible = showVenue && !showAmbiguity;
            venueSelectControl.Visible = showVenue && !showAmbiguity;
            venueDropdownNonJS.Visible = showVenue && (!js) && !showAmbiguity; // Only shown for non-js users
            

            locationDirectionLbl.AssociatedControlID = (ambiguityRow.Visible) ?
                ambiguityDrop.ID : locationInput.ID;

            // Set toggle visibility from properties
            if (!Properties.Current["JourneyLocations.Toggle.Enabled.Switch"].Parse(true))
            {
                if (travelFromToToggleDiv.Visible)
                    travelFromToToggleDiv.Visible = false;
            }
        }

        #region Helpers

        /// <summary>
        /// Checks for valid location attributes
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool IsValidLocation(TDPLocation location)
        {
            return (location != null
                        && !String.IsNullOrEmpty(location.DisplayName)
                        && (!string.IsNullOrEmpty(location.Locality)
                            || location.Naptan.Count > 0
                            || (location.GridRef.Easting > 0 && location.GridRef.Northing > 0)));
        }

        /// <summary>
        /// Checks if the search string is an ignore string, e.g. an updating, or a watermark
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private bool IsIgnoreSearchString(string searchText)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    if ((searchText.Equals(updatingString)) || (searchText.Equals(mylocationString)))
                    {
                        return true;
                    }
                    else
                    {
                        TDPPageMobile page = (TDPPageMobile)Page;

                        if (searchText.Equals(page.GetResourceMobile("JourneyInput.Location.From.Watermark")))
                        {
                            return true;
                        }
                        else if (searchText.Equals(page.GetResourceMobile("JourneyInput.Location.To.Watermark")))
                        {
                            return true;
                        }
                        else if (searchText.Equals(page.GetResource("LocationControl.LocationInput.Tooltip.Venue")))
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                // Ignore exceptions, only checking a string
            }

            return false;
        }

        #endregion

        #endregion
    }
}
