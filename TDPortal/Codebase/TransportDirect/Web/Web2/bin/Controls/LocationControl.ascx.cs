// *********************************************** 
// NAME                 : LocationControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/07/2012 
// DESCRIPTION          : Location control. Enables user to 
//                      : select a location using the gazetteer,
//                      : select a location using auto-suggest,
//                      : select a location from ambiguous dropdown,
//                      : display a resolved location
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LocationControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Dec 05 2012 13:42:16   mmodi
//Validate locations for accessible journey request
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Oct 02 2012 11:42:24   mmodi
//Clear hidden js location values when displaying control in ambiguos state
//Resolution for 5854: Gaz - Locality location with child localities displays ambiguity screen
//
//   Rev 1.2   Sep 05 2012 16:38:30   mmodi
//Corrected issue detecting more options have been selected
//Resolution for 5839: Gaz - Homepage plan a journey submit does not show ambiguity locations
//
//   Rev 1.1   Sep 04 2012 11:16:58   mmodi
//Updated to handle landing page auto plan with the new auto-suggest location control
//Resolution for 5837: Gaz - Page landing autoplan links fail on Cycle input page
//
//   Rev 1.0   Aug 28 2012 10:23:48   mmodi
//Initial revision.
//Resolution for 5832: CCN Gaz

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

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Location control
    /// </summary>
    public partial class LocationControl : TDUserControl
    {
        #region Private members

        private bool resolveLocation = true;

        private LocationSearch search = null;
        private TDLocation location = null;

        private DataServiceType locationListType = DataServiceType.FromToDrop;
        private TDLocationStatus locationStatus = TDLocationStatus.Unspecified;
        private SearchType locationSearchType = SearchType.MainStationAirport;

        private bool allowAutoSuggest = true;
        private bool allowAmbiguity = true;
        private bool allowAmbiguityReset = true;
        private bool allowUnsureSpelling = true;
        private bool allowFindOnMap = false;
        private bool allowLocationTypesListRadio = true;
        private bool allowLocationTypesListDropDown = false;
        private bool showFixedLocation = false;
        private bool showAmbiguityInputLocation = false;
        private bool showAmbiguityTypeDrop = false;
        private bool showMoreOptionsExpanded = false;

        private DataServices.DataServices populator;
        private bool moreOptionSelected = false;
        
        #endregion

        #region Public Events
        
        public event EventHandler MapLocationClick;
        public event EventHandler NewLocationClick;

        #endregion

        #region Public Properties

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
        /// Get/Sets the LocationSearch represented by the location control
        /// </summary>
        public LocationSearch Search
        {
            get { return search; }
            set
            {
                search = value;
                if (search != null)
                {
                    locationSearchType = search.SearchType;
                }
            }
        }

        /// <summary>
        /// Get/Sets the TDLocation represented by the location control
        /// </summary>
        public TDLocation Location
        {
            get { return location; }
            set
            {
                location = value;
                if (location != null)
                {
                    locationStatus = location.Status;
                    locationSearchType = location.SearchType;
                }
            }
        }

        /// <summary>
        /// Location input textbox to allow custom tooltip to be assigned
        /// Not to be used for any other reason
        /// </summary>
        public TextBox LocationInput
        {
            get { return locationInput; }
            set { locationInput = value; }
        }

        /// <summary>
        /// Screen reader label associated with the location input
        /// </summary>
        public Label LocationInputDescription
        {
            get { return locationInputDescription; }
        }

        /// <summary>
        /// Screen reader label associated with the location type gazetteer list
        /// </summary>
        public Label LocationTypeDescription
        {
            get { return locationTypeDescription; }
        }

        /// <summary>
        /// Read/Write property to set the fixed location flag after the initialise has already been called.
        /// Allows page in ambiguity mode (after submit called) so specify the valid location should be shown as fixed
        /// </summary>
        public bool ShowFixedLocation
        {
            get { return showFixedLocation; }
            set { showFixedLocation = value; }
        }

        /// <summary>
        /// Read only property to return the selected search type
        /// </summary>
        public SearchType SearchTypeSelected
        {
            get
            {
                // If location selected using auto-suggest, return that type
                if (!string.IsNullOrEmpty(locationInput_hdnType.Value.Trim()))
                {
                    TDStopType locationType = TDStopTypeHelper.GetTDStopType(locationInput_hdnType.Value.Trim());

                    if (locationType != TDStopType.Unknown)
                    {
                        return SearchTypeHelper.GetSearchType(locationType);
                    }
                }
                
                // Otherwise use the gaz type list
                return GetSearchTypeFromList();
            }
        }

        /// <summary>
        /// Read only property to return the location input text (checks for any watermark text
        /// </summary>
        public string LocationInputText
        {
            get
            {
                if (IsIgnoreSearchString(locationInput.Text.Trim()))
                {
                    return string.Empty;
                }
                else
                {
                    return locationInput.Text.Trim();
                }
            }
        }

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        protected LocationControl()
        {
            populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
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

            LogMoreSelectedEvent();
        }
        
        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            RegisterJavascripts();

            SetupResources();

            SetForInputLocation();

            ShowHideControls();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Ambiguity Reset button event handler, resets the location ready for input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void resetBtn_Click(object sender, EventArgs e)
        {
            Reset();

            // Also tell event subscribers the location was reset (i.e. new location)
            if (NewLocationClick != null)
                NewLocationClick(sender, e);
        }

        /// <summary>
        /// New location button event handler, resets the fixed location ready for input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void newLocationBtn_Click(object sender, EventArgs e)
        {
            Reset();

            // Tell event subcribers new location was clicked
            if (NewLocationClick != null)
                NewLocationClick(sender, e);
        }

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

        /// <summary>
        /// More options button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void moreOptionsBtn_Click(object sender, EventArgs e)
        {
            // Shouldnt be clicked as this is a javascript displayed button only, 
            // intended for use by javascript to display the gazetteer radio list without the need for a postback
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialise method
        /// </summary>
        public void Initialise(
            TDLocation location,
            LocationSearch search,
            DataServiceType locationListType, 
            bool allowAutoSuggest,
            bool allowAmbiguity,
            bool allowAmbiguityReset,
            bool allowUnsureSpelling,
            bool allowFindOnMap,
            bool allowLocationTypesListRadio,
            bool allowLocationTypesListDropDown,
            bool showValidLocationFixed,
            bool showMoreOptionsExpanded)
        {
            this.Location = location;
            this.Search = search;
            this.locationListType = locationListType;
            this.allowAutoSuggest = allowAutoSuggest;
            this.allowAmbiguity = allowAmbiguity;
            this.allowAmbiguityReset = allowAmbiguityReset;
            this.allowUnsureSpelling = allowUnsureSpelling;
            this.allowFindOnMap = allowFindOnMap;
            this.allowLocationTypesListRadio = allowLocationTypesListRadio;
            this.allowLocationTypesListDropDown = allowLocationTypesListDropDown;
            this.showMoreOptionsExpanded = showMoreOptionsExpanded;

            if (!allowAmbiguity)
            {
                if (locationStatus == TDLocationStatus.Ambiguous)
                    locationStatus = TDLocationStatus.Unspecified;
            }

            // Normally allow a resolved location to be editable, but sometimes
            // want it to be fixed, e.g. if selected from a map. 
            // So prevent user from changing location if 
            // 1) search indicates its a fixed location
            // 2) its is a map location (in case the search hasn't indicated it should be fixed)
            // 3) parent of control indicates it should be fixed
            if (search != null && search.LocationFixed)
            {
                this.showFixedLocation = true;
            }
            else if (search != null && search.SearchType == SearchType.Map
                     && location != null && location.SearchType == SearchType.Map)
            {
                this.showFixedLocation = true;
            }
            else
            {
                this.showFixedLocation = showValidLocationFixed;
            }
        }

        /// <summary>
        /// Resets location control
        /// </summary>
        public void Reset()
        {
            search = new LocationSearch();
            location = new TDLocation();
            locationStatus = TDLocationStatus.Unspecified;
            locationSearchType = SearchType.MainStationAirport;

            SetSearchType(locationSearchType);

            locationInput.Text = string.Empty;
            locationInput_hdnValue.Value = string.Empty;
            locationInput_hdnType.Value = string.Empty;

            locationTypesRow.Visible = allowLocationTypesListRadio;
            locationTypes.Visible = allowLocationTypesListRadio;
            locationTypeDrop.Visible = allowLocationTypesListDropDown && !allowLocationTypesListRadio; // Only show one list, giving preference to radio
            locationListType = DataServiceType.FromToDrop;

            ambiguityRow.Visible = false;
            ambiguityText.Text = string.Empty;

            showFixedLocation = false;
            showMoreOptionsExpanded = false;
        }

        /// <summary>
        /// Resets location control
        /// </summary>
        /// <param name="searchType"></param>
        public void Reset(SearchType searchType)
        {
            Reset();

            search.SearchType = searchType;
            locationSearchType = searchType;

            SetSearchType(locationSearchType);
        }


        /// <summary>
        /// Validates the control by resolving the location
        /// </summary>
        /// <returns>True if the location is resolved</returns>
        public bool Validate(
            TDJourneyParameters journeyParameters,
            bool allowGroupLocations,
            bool acceptsPostcode,
            bool acceptsPartPostcode,
            StationType stationType)
        {
            if (resolveLocation)
            {
                try
                {
                    bool performLocationSearch = true;

                    // If location is resolved, only resolve the location if user has changed 
                    // the input text and/or selected another location
                    if (location != null && locationStatus == TDLocationStatus.Valid
                        && !string.IsNullOrEmpty(locationInput.Text.Trim()))
                    {
                        if (location.Description == locationInput.Text.Trim())
                        {
                            performLocationSearch = false;
                        }
                        else if (!string.IsNullOrEmpty(locationInput_hdnValue.Value.Trim())
                            && location.ID == locationInput_hdnValue.Value.Trim())
                        {
                            performLocationSearch = false;
                        }
                    }

                    if (performLocationSearch)
                    {
                        // Reset location text if it matches any ignore strings (e.g. watermark text)
                        if (IsIgnoreSearchString(locationInput.Text.Trim()))
                        {
                            locationInput.Text = string.Empty;
                        }

                        bool js = Parse(jsEnabled.Value, false);

                        // if location is set from auto suggest
                        if (!string.IsNullOrEmpty(locationInput_hdnType.Value.Trim())
                            && !string.IsNullOrEmpty(locationInput_hdnValue.Value.Trim())
                            && js)
                        {
                            #region Javascript selected location search

                            TDStopType locationType = TDStopTypeHelper.GetTDStopType(locationInput_hdnType.Value.Trim());

                            LocationSearchHelper.ResetSearchAndLocation(ref search, ref location,
                                locationInput.Text,
                                SearchTypeHelper.GetSearchType(locationType),
                                unsureSpellingCheck.Checked,
                                allowGroupLocations, 
                                js);

                            LogAutoSuggestEvent();
                            
                            // Resolve location using location gaz search for the selected location id and type
                            LocationSearchHelper.SearchLocation(ref search, ref location, journeyParameters,
                                acceptsPostcode, acceptsPartPostcode, stationType,
                                locationInput_hdnValue.Value.Trim(), locationType);

                            #endregion
                        }
                        // if location is not selected from auto suggest (javascript and non-javascript users)
                        else if (!ambiguityRow.Visible)
                        {
                            #region Non-javascript location search

                            // If more is selected or more options expanded, 
                            // then do not want to do an "auto-suggest" search, use the gaz search
                            LocationSearchHelper.ResetSearchAndLocation(ref search, ref location,
                                locationInput.Text,
                                GetSearchTypeFromList(),
                                unsureSpellingCheck.Checked,
                                allowGroupLocations, 
                                js && !IsMoreOptionsSelected());

                            // Resolve location using location gaz search
                            LocationSearchHelper.SearchLocation(ref search, ref location, journeyParameters,
                                acceptsPostcode, acceptsPartPostcode, stationType,
                                string.Empty, TDStopType.Unknown);

                            #endregion
                        }
                        else
                        {
                            #region Ambiguous location selection

                            // Check if ambiguity gaz type drop is displayed and the selected type has changed
                            if (ambiguityTypeDropRow.Visible 
                                && GetSearchTypeFromList() != search.SearchType)
                            {
                                // Reset selected item to ensure default value is shown if user has selected
                                // both a location and different gaz type
                                ambiguityDrop.SelectedIndex = -1;

                                // Different, reset and search again
                                LocationSearchHelper.ResetSearchAndLocation(ref search, ref location,
                                    search.InputText,
                                    GetSearchTypeFromList(),
                                    search.FuzzySearch,
                                    allowGroupLocations,
                                    false);

                                // Resolve location using location gaz search
                                LocationSearchHelper.SearchLocation(ref search, ref location, journeyParameters,
                                    acceptsPostcode, acceptsPartPostcode, stationType,
                                    string.Empty, TDStopType.Unknown);
                            }
                            else
                            {
                                LocationSearchHelper.AmbiguityLocation(ref search, ref location, journeyParameters,
                                    ambiguityDrop, acceptsPostcode, acceptsPartPostcode, stationType,
                                    js);
                            }

                            #endregion
                        }
                    }

                    // Update the location accessible flag, this will only be done if the 
                    // journey parameters has accessible preferences set and location status is valid
                    LocationSearchHelper.CheckAccessibleLocation(ref location, journeyParameters);
                }
                catch (TDException tdEx)
                {
                    if (!tdEx.Logged)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                            string.Format("Error resolving location: {0}", tdEx.Message), tdEx));
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                            string.Format("Error resolving location: {0}", ex.Message), ex));
                }
            }

            // Check location is valid
            if (IsValidLocation(location))
            {
                locationStatus = TDLocationStatus.Valid;
            }
            else
            {
                locationStatus = TDLocationStatus.Ambiguous;
            }

            return (locationStatus == TDLocationStatus.Valid);
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
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LocationInput", scriptRepository.GetScript("LocationInput", javaScriptDom));

            // Only add the autosuggest scripts if allowed
            if (allowAutoSuggest)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LocationSuggest", scriptRepository.GetScript("LocationSuggest", javaScriptDom));

                // Javascript settings - based on the location list type to show
                
                // Set path of locations js files
                scriptPath.Value = ResolveClientUrl(Properties.Current["ScriptRepository.LocationSuggest.ScriptPath"]);

                // Javascript version
                LSC.LocationService locationService = (LSC.LocationService)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationServiceCache];
                string version = locationService.LocationVersion();

                // Set js files (prefix, LocationSuggest.js completes the filename) to download for the list type,
                // e.g. "Location_NoGroups_VERSION" with "_a.js", "_b.js" etc being added within the javascript
                switch (locationListType)
                {
                    case DataServiceType.FromToDrop:
                    case DataServiceType.PTViaDrop: // PT via location should not contain localities in the auto suggest
                    case DataServiceType.FindCarLocationDrop:
                    case DataServiceType.CarViaDrop: // Car via location should not contain groups in the auto suggest
                    case DataServiceType.FindCycleLocationDrop:
                    case DataServiceType.CycleViaLocationDrop: // Cycle via location should not contain groups in the auto suggest
                        scriptId.Value = string.Format("{0}{1}",
                            Properties.Current[string.Format("ScriptRepository.LocationSuggest.Script.Name.{0}", locationListType.ToString())],
                            version);
                        break;
                    default:
                        scriptId.Value = string.Format("{0}{1}",
                            Properties.Current["ScriptRepository.LocationSuggest.ScriptId.Default"],
                            version);
                        break;
                }
            }

            // If more options are set to be expanded, then disable the location suggest by setting flag
            // to allow js to detect and remove (instead of by default attaching functionality)
            locationSuggestDisabled.Value = showMoreOptionsExpanded.ToString().ToLower();
        }

        /// <summary>
        /// Sets up resource string from content database
        /// </summary>
        private void SetupResources()
        {
            // If parent hasn't populated this text
            if (string.IsNullOrEmpty(locationInputDescription.Text))
            {
                locationInputDescription.Text = GetResource("LocationControl.LocationInputDescription.Text");
            }
            if (string.IsNullOrEmpty(locationInput.ToolTip))
            {
                locationInput.ToolTip = GetResource("LocationControl.LocationInput.ToolTip");
            }

            unsureSpellingCheck.Text = GetResource("LocationControl.UnsureSpelling.Text");
            ambiguityTypeText.Text = GetResource("LocationAmbiguousControl.labelChooseLocation.Text");
            ambiguityResetBtn.Text = GetResource("LocationControl.AmbiguityReset.Text");
            moreOptionsBtn.Text = GetResource("LocationControl.MoreOptions.Text");
            newLocationBtn.Text = GetResource("LocationControl.NewLocation.Text");
            findOnMapBtn.Text = GetResource("LocationControl.FindOnMap.Text");

            // Set the watermark text value, this is displayed by javascript if needed
            locationInput.Attributes.Add("data-defaultvalue", locationInput.ToolTip);

            // Set the screenreader label association
            locationTypeDescription.AssociatedControlID = (allowLocationTypesListRadio) ? locationTypeRadio.ID : locationTypeDrop.ID;
        }

        /// <summary>
        /// Sets up controls
        /// </summary>
        private void SetupControls()
        {
            if (!Page.IsPostBack)
            {
                // Gaz location types, shown when More options clicked (using js) and for non-js users
                populator.LoadListControl(locationListType, locationTypeRadio);

                // Should only be used for homepage "Plan a journey control"
                populator.LoadListControl(DataServiceType.FindLocationGazeteerOptions, locationTypeDrop, resourceManager);

                // Gaz location types drop, shown for Ambiguous locations when "auto-suggest" ambiguity not used
                populator.LoadListControl(locationListType, ambiguityTypeDrop, resourceManager);

                SetSearchType(locationSearchType);
            }

            // Clear any error styles (these will be reapplied where necessary)
            SetInputErrorStyle(false);
        }

        /// <summary>
        /// Sets location for Input 
        /// </summary>
        private void SetForInputLocation()
        {
            // If a post back occurs and location is resolved but becomes ambiguous
            if (!allowAmbiguity && locationStatus == TDLocationStatus.Ambiguous)
            {
                locationStatus = TDLocationStatus.Unspecified;
            }

            if (locationStatus == TDLocationStatus.Ambiguous)
            {
                SetForAmbiguousLocation();

                // Ensure hidden values are cleared to allow postback to resolve the 
                // selected ambiguous location, rather than falling into the resolve js location
                locationInput_hdnValue.Value = string.Empty;
                locationInput_hdnType.Value = string.Empty;
            }
            else if (locationStatus == TDLocationStatus.Valid && location != null)
            {
                locationInput.Text = location.Description;

                // Normally allow a resolved location to be editable, but sometimes
                // want it to be fixed, e.g. if selected from a map 
                // or if page explicitly declared it (for an ambiguity page)
                if (showFixedLocation)
                {
                    SetForFixedLocation();
                }
            }

            if (locationStatus != TDLocationStatus.Ambiguous && location != null)
            {
                #region Set hidden input values, and location input text if required

                // Set the hidden location type and value settings 
                // check if js enabled on postback 
                if (!IsPostBack)
                {
                    locationInput_hdnValue.Value = location.ID;
                    locationInput_hdnType.Value = location.StopType.ToString();

                    // If the location object is not resolved, then ensure the control
                    // displays the input text (e.g. could be returning back from ambiguity page
                    // to input and therefore want to display the input text that caused
                    // the ambiguity)
                    if (!allowAmbiguity)
                    {
                        if (string.IsNullOrEmpty(locationInput.Text.Trim()))
                        {
                            if (!string.IsNullOrEmpty(location.Description))
                                locationInput.Text = location.Description;
                            else if (search != null && !string.IsNullOrEmpty(search.InputText))
                                locationInput.Text = search.InputText;
                        }
                    }
                }
                else if ((Parse(jsEnabled.Value, false)) && (locationStatus == TDLocationStatus.Valid))
                {
                    // Only update hidden values for resolved location, as postback could happen
                    // before user has "submitted" and therefore selected javasript location
                    // may not have been resolved
                    locationInput_hdnValue.Value = location.ID;
                    locationInput_hdnType.Value = location.StopType.ToString();
                }

                #endregion
            }
        }

        /// <summary>
        /// Sets location for fixed, doesnt allow user to edit
        /// </summary>
        private void SetForFixedLocation()
        {
            // Set the location type description and location name

            // Map location
            if (location.SearchType == SearchType.Map)
            {
                fixedLocationDescription.Text = GetResource("LocationDisplayControl.LabelLocationType.Text");
                fixedLocation.Text = HttpUtility.HtmlDecode(location.Description);
            }
            // Car park location
            else if ((location.SearchType == SearchType.CarPark) || (location.CarParking != null))
            {
                // Display "Near (searched for) location"
                fixedLocationDescription.Text = string.Format("{0}  {1}", GetResource("FindLocationControl.directionLabelTravelFrom"), HttpUtility.HtmlDecode(location.Description));

                // And display car park name as the actual location
                #region Get Car park name
                
                string carLocation = string.Empty;

                // If there is no location, then this prevents car name being displayed incorrectly
                if (!string.IsNullOrEmpty(location.CarParking.Location))
                    carLocation = HttpUtility.HtmlDecode(location.CarParking.Location) + ", ";
                else
                    carLocation = string.Empty;

                string carParkName = carLocation + HttpUtility.HtmlDecode(location.CarParking.Name);

                // Add on the car park end 
                if (location.CarParking.ParkAndRideIndicator.Trim().ToLower() == "true")
                    carParkName += GetResource("ParkAndRide.Suffix") + GetResource("ParkAndRide.CarkPark.Suffix");
                else
                    carParkName += GetResource("ParkAndRide.CarkPark.Suffix");

                #endregion

                fixedLocation.Text = carParkName;
            }
            else
            {
                // Hide type for any unknown location type, 
                // it could be a landing page location which wouldnt be able to distinguish a naptan stop type
                if (location.StopType != TDStopType.Unknown)
                {
                    fixedLocationDescription.Text = populator.GetText(locationListType, Enum.GetName(typeof(SearchType), location.SearchType));
                }
                fixedLocation.Text = HttpUtility.HtmlDecode(location.Description);
            }
        }

        /// <summary>
        /// Sets location for ambiguity situation
        /// </summary>
        private void SetForAmbiguousLocation()
        {
            // Ambiguous locations will either be from the "auto-suggest" ambiguous search,
            // or from the standard Gaz ambiguous search (there shouldn't be both)

            // Check for any location choices found for the search (there could be an empty choice list
            // if the text searched for found none or could be null for an empty text search)
            LocationChoiceList locationChoices = 
                (search.CurrentLevel >= 0) ? locationChoices = search.GetCurrentChoices(search.CurrentLevel) : null;

            // If location choices exist, populate ambiguity dropdown
            if (locationChoices != null && locationChoices.Count > 0)
            {
                // Gaz ambigiuty, show ambiguity gaz drop
                showAmbiguityTypeDrop = true;

                // Set ambiguity instruction text
                string locationText = (search.CurrentLevel == 0) ?
                    search.InputText : search.GetQueryResult(search.CurrentLevel).ParentChoice.Description;

                ambiguityText.Text = string.Format(GetResource("LocationAmbiguousControl.possibleOptions"), locationText);
                
                SetupAmbiguityGazDropDown(locationChoices, ambiguityDrop, ambiguityDrop.SelectedIndex, search.SupportHierarchic);

                // Ensure ambiguity gaz type has correct search type selected
                SetSearchType(search.SearchType);
            }
            else if (search.GetAmbiguitySearchResult().Count > 0)
            {
                // Auto-suggest ambigiuty, do not display ambiguity gaz drop
                showAmbiguityTypeDrop = false;

                // Else it's an "auto-suggest" ambiguous locations list (because user hadn't selected from
                // the auto suggest dropdown)
                string locationText = search.InputText;

                ambiguityText.Text = string.Format(GetResource("LocationAmbiguousControl.possibleOptions"), locationText);

                SetupAmbiguityLocDropDown(search.GetAmbiguitySearchResult(), ambiguityDrop, ambiguityDrop.SelectedIndex);   
            }
            else
            {
                // No ambiguous location choices, so set to show ambiguity input row
                showAmbiguityInputLocation = true;

                // Set input error style 
                SetInputErrorStyle(true);

                // Set ambiguity input instruction text
                string textSearchType = populator.GetText(locationListType, Enum.GetName(typeof(SearchType), search.SearchType));

                if (search.SearchType == SearchType.AddressPostCode)
                {
                    ambiguityInputText.Text = string.Format("{0}<br />{1}",
                        string.Format(GetResource("LocationSelectControl2.labelNoMatchForAddressPostcode"), search.InputText, textSearchType),
                        GetResource("LocationSelectControl2.labelNoMatchForLocationTip"));
                }
                else
                {
                    // If more option was selected or js disabled, and they've entered text,
                    // specify message with selected gaz
                    if (!string.IsNullOrEmpty(search.InputText)
                        && (moreOptionSelected || !search.JavascriptEnabled))
                    {
                        ambiguityInputText.Text = string.Format("{0}<br />{1}",
                            string.Format(GetResource("LocationSelectControl2.labelNoMatchForLocation"), search.InputText, textSearchType),
                            GetResource("LocationSelectControl2.labelNoMatchForLocationTip"));
                    }
                    else
                    {
                        ambiguityInputText.Text = string.Format("{0}<br />{1}",
                            string.Format(GetResource("LocationControl.Ambiguity.NoMatchForLocation.Text"), search.InputText),
                            GetResource("LocationSelectControl2.labelNoMatchForLocationTip"));
                    }
                }

                // Ensure location input continues to show the search input text
                if (string.IsNullOrEmpty(locationInput.Text.Trim()))
                {
                    locationInput.Text = search.InputText;
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
            ambiguityDrop.Items.Clear();

            // Show list
            ambiguityDrop.Visible = true;

            int i = 0;
            ListItem item;
            StringBuilder itemText = new StringBuilder(string.Empty);
            StringBuilder itemValue = new StringBuilder(string.Empty);

            string itemPleaseSelect = GetResource("LocationAmbiguousControl.itemPleaseSelect");
            string moreOptions = GetResource("LocationAmbiguousControl.moreOptions");

            // The first item in the drop down list should be "please select"
            item = new ListItem(itemPleaseSelect, LocationSearchHelper.DEFAULT_ITEM);
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
                    // If location has children, add option to drilldown
                    if (choice.HasChilden)
                    {
                        itemText = new StringBuilder(string.Empty);
                        itemText.Append(moreOptions);
                        itemText.Append(" ");
                        itemText.Append(choice.Description);
                        itemText.Append("...");

                        itemValue = new StringBuilder(string.Empty);
                        itemValue.Append("+");
                        itemValue.Append(i.ToString());

                        item = new ListItem(itemText.ToString(), itemValue.ToString());

                        list.Items.Add(item);
                    }
                }
                else
                {
                    item = new ListItem(choice.Description, i.ToString());
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
        private void SetupAmbiguityLocDropDown(IList<TDLocation> choices, DropDownList list, int selectedIndex)
        {
            // Clear existing list
            ambiguityDrop.Items.Clear();

            // Show list
            ambiguityDrop.Visible = true;

            int i = 0;
            ListItem item;
            StringBuilder itemText = new StringBuilder(string.Empty);
            StringBuilder itemValue = new StringBuilder(string.Empty);

            string itemPleaseSelect = GetResource("LocationAmbiguousControl.itemPleaseSelect");

            // The first item in the drop down list should be "please select"
            item = new ListItem(itemPleaseSelect, LocationSearchHelper.DEFAULT_ITEM);
            list.Items.Add(item);

            foreach (TDLocation choice in choices)
            {
                item = new ListItem(choice.Description, i.ToString());
                list.Items.Add(item);

                i++;
            }
            if (selectedIndex < list.Items.Count)
                list.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// Show/Hides controls based on various state of the location control
        /// </summary>
        private void ShowHideControls()
        {
            // Default to input view
            bool showInput = true;
            bool showAmbiguityInput = false;
            bool showAmbiguity = false;
            bool showFixed = false;

            // Show ambiguity input row, show input
            if (locationStatus == TDLocationStatus.Ambiguous && showAmbiguityInputLocation)
            {
                showAmbiguityInput = true;
                showInput = true;
            }
            // Show ambiguity row, hide input
            else if (locationStatus == TDLocationStatus.Ambiguous)
            {
                showAmbiguity = true;
                showInput = false;
            }
            // Show fixed location row, hide input
            else if (locationStatus == TDLocationStatus.Valid && showFixedLocation)
            {
                showFixed = true;
                showInput = false;
            }

            // Input row
            locationInput.Visible = showInput;
            locationInput_hdnValue.Visible = showInput || showFixed;
            locationInput_hdnType.Visible = showInput || showFixed;
            locationInputDescription.Visible = showInput;

            // More options
            unsureSpellingRow.Visible = showInput && allowUnsureSpelling;
            locationTypesRow.Visible = showInput && (allowLocationTypesListRadio || allowLocationTypesListDropDown);
            locationTypes.Visible = allowLocationTypesListRadio;
            locationTypeDrop.Visible = allowLocationTypesListDropDown && !allowLocationTypesListRadio; // Only show one types list, preference to radio
            findOnMapBtn.Visible = showInput && allowFindOnMap;

            // Show more button only if not wanting to display options expanded, 
            // and when in input view with at least one "more option" available
            moreOptionsBtn.Visible = !showMoreOptionsExpanded && showInput && (allowUnsureSpelling || allowFindOnMap || allowLocationTypesListRadio);
            
            // Ambiguity input row
            ambiguityInputRow.Visible = showAmbiguityInput;

            // Ambiguity row
            ambiguityRow.Visible = showAmbiguity;
            ambiguityResetBtn.Visible = showAmbiguity && allowAmbiguityReset;
            ambiguityTypeDropRow.Visible = showAmbiguity && showAmbiguityTypeDrop;

            // Fixed location row
            fixedLocationRow.Visible = showFixed;
        }

        #region Log

        /// <summary>
        /// Log auto suggest selected event
        /// </summary>
        private void LogAutoSuggestEvent()
        {
            // Log selected autosuggest values
            if (TDTraceSwitch.TraceVerbose)
            {
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("AutoSuggest Type[{0}] Value[{1}]", locationInput_hdnType.Value.Trim(), locationInput_hdnValue.Value.Trim()));
                Logger.Write(oe);
            }
        }

        /// <summary>
        /// Log more button selected event
        /// </summary>
        private void LogMoreSelectedEvent()
        {
            if (!string.IsNullOrEmpty(moreSelected.Value.Trim()))
            {
                // Log an event to indicate the user selected "more options" on this location control
                if (Parse(moreSelected.Value.Trim(), false))
                {
                    PageEntryEvent pee = new PageEntryEvent(PageId.LocationMoreOptionsClicked, TDSessionManager.Current.Session.SessionID, TDSessionManager.Current.Authenticated);
                    Logger.Write(pee);

                    // Set flag for use
                    moreOptionSelected = true;
                }

                // Reset value to false so it's not logged until user selects again
                moreSelected.Value = string.Empty;
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks for valid location attributes
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private bool IsValidLocation(TDLocation location)
        {
            return (location != null
                        && !String.IsNullOrEmpty(location.Description)
                        && (!string.IsNullOrEmpty(location.Locality)
                            || location.NaPTANs.Length > 0
                            || (location.GridReference.Easting > 0 && location.GridReference.Northing > 0)));
        }

        /// <summary>
        /// Checks if the search string is an ignore string, e.g. an updating, or a watermark
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private bool IsIgnoreSearchString(string search)
        {
            try
            {
                if (!string.IsNullOrEmpty(search))
                {
                    // Location input watermark text
                    if (search.Equals(locationInput.ToolTip))
                    {
                        return true;
                    }
                    else if (search.Equals(GetResource("LocationControl.LocationInput.ToolTip")))
                    {
                        return true;
                    }
                    else if (search.Equals(GetResource("LocationControl.LocationInput.PTVia.ToolTip")))
                    {
                        return true;
                    }
                    else if (search.Equals(GetResource("LocationControl.LocationInput.PlanAJourney.ToolTip")))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // Ignore exceptions, only checking a string
            }

            return false;
        }

        /// <summary>
        /// Returns true if the more options have been selected or shown expanded.
        /// This would indicate the Gaz search should be done
        /// </summary>
        /// <returns></returns>
        private bool IsMoreOptionsSelected()
        {
            // Set by javascript if user has clicked the more options
            if (moreOptionSelected)
                return true;

            // Set by parent page (normally set only once during pageload and not on subsequent postbacks, 
            // so also check more options button visibility below for postbacks)
            if (showMoreOptionsExpanded)
                return true;

            // If more options button is hidden, then implies show more options be defauilt
            if (!moreOptionsBtn.Visible)
            {
                // However, it could be hidden because 
                // a) On homepage plan a journey control which would mean there are no more options to show
                if (!allowUnsureSpelling && !allowUnsureSpelling && !allowLocationTypesListRadio)
                {
                    return false;
                }
                // b) Previous load may have asked to showMoreOptionsExpanded which would have 
                // set the more button to be hidden, but this property is now false, so check
                // the allow more options settings. Any true would imply more options are allowed and were 
                // displayed
                else if (allowUnsureSpelling || allowFindOnMap || allowLocationTypesListRadio)
                {
                    return true;
                }
            }

            // Default more not selected
            return false;
        }

        /// <summary>
        /// Parses a string into a bool. Returns specified default if fails
        /// </summary>
        private bool Parse(string stringValue, bool defaultValue)
        {
            bool value = defaultValue;

            if (bool.TryParse(stringValue, out value))
            {
                return value;
            }

            return defaultValue;
        }

        /// <summary>
        /// Returns the selected SearchType from the location types gazetter list
        /// </summary>
        /// <returns></returns>
        private SearchType GetSearchTypeFromList()
        {
            SearchType searchType = SearchType.MainStationAirport;

            // Get the selected gazetteer search type, defaulting to main stations
            if (allowLocationTypesListRadio 
                && locationTypesRow.Visible 
                && locationTypeRadio.SelectedIndex >= 0)
            {
                string value = populator.GetValue(locationListType, locationTypeRadio.SelectedItem.Value);
                searchType = (SearchType)Enum.Parse(typeof(SearchType), value, true);
            }
            else if (allowLocationTypesListDropDown 
                && locationTypesRow.Visible 
                && locationTypeDrop.SelectedIndex >= 0)
            {
                // This drop contains values for FindLocationGazeteerOptions enum
                FindLocationGazeteerOptions findLocationGazeteerOptions = (FindLocationGazeteerOptions)Enum.Parse(typeof(FindLocationGazeteerOptions), locationTypeDrop.SelectedValue);

                switch (findLocationGazeteerOptions)
                {
                    case FindLocationGazeteerOptions.AttractionFacility:
                        searchType = SearchType.POI;
                        break;
                    case FindLocationGazeteerOptions.CityTownSuburb:
                        searchType = SearchType.Locality;
                        break;
                    case FindLocationGazeteerOptions.StationAirport:
                        searchType = SearchType.MainStationAirport;
                        break;
                    case FindLocationGazeteerOptions.AddressPostcode:
                    default:
                        searchType = SearchType.AddressPostCode;
                        break;
                }
            }
            else if (ambiguityRow.Visible
                && ambiguityTypeDropRow.Visible
                && ambiguityTypeDrop.SelectedIndex >= 0)
            {
                string value = populator.GetValue(locationListType, ambiguityTypeDrop.SelectedItem.Value);
                searchType = (SearchType)Enum.Parse(typeof(SearchType), value, true);
            }

            return searchType;
        }

        /// <summary>
        /// Sets the search type to the location type list
        /// </summary>
        /// <param name="searchType"></param>
        private void SetSearchType(SearchType searchType)
        {
            try
            {
                // Gaz radio list
                if (locationTypeRadio.Items.Count > 0)
                {
                    populator.Select(locationTypeRadio, populator.GetResourceId(locationListType, searchType.ToString()));
                }

                // Homepage drop down list
                if (locationTypeDrop.Items.Count > 0)
                {
                    FindLocationGazeteerOptions flgo = FindLocationGazeteerOptions.AddressPostcode;

                    switch (searchType)
                    {
                        case SearchType.POI:
                            flgo = FindLocationGazeteerOptions.AttractionFacility;
                            break;
                        case SearchType.Locality:
                            flgo = FindLocationGazeteerOptions.CityTownSuburb;
                            break;
                        case SearchType.MainStationAirport:
                            flgo = FindLocationGazeteerOptions.StationAirport;
                            break;
                        case SearchType.AddressPostCode:
                        default:
                            flgo = FindLocationGazeteerOptions.AddressPostcode;
                            break;
                    }

                    populator.Select(locationTypeDrop, flgo.ToString());
                }

                // Ambiguity drop down list
                if (ambiguityTypeDrop.Items.Count > 0)
                {
                    populator.Select(ambiguityTypeDrop, populator.GetResourceId(locationListType, searchType.ToString()));
                }
            }
            catch
            {
                // Ignore exception, data could be incorrectly setup. 
                // The lists will display the default value which will be ok in most scenarios
            }
        }

        /// <summary>
        /// Adds ore removes error style from the location input box
        /// </summary>
        /// <param name="addErrorStyle"></param>
        private void SetInputErrorStyle(bool addErrorStyle)
        {
            if (addErrorStyle)
            {
                if (!locationInput.CssClass.Contains("alertboxerror"))
                {
                    locationInput.CssClass = string.Format("{0} {1}", locationInput.CssClass, "alertboxerror");
                }
            }
            else
            {
                if (locationInput.CssClass.Contains("alertboxerror"))
                {
                    locationInput.CssClass = locationInput.CssClass.Replace(" alertboxerror", string.Empty);
                }
            }
        }

        #endregion

        #endregion
    }
}