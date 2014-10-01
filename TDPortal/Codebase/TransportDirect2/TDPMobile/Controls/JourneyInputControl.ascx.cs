﻿// *********************************************** 
// NAME             : JourneyInputControl.ascx.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Feb 2012
// DESCRIPTION  	: Journey input control containing input controls for location, dates, and dropdowns
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDP.Common.Extenders;
using TDP.Common.Web;
using TDP.Common.LocationService;
using TDP.UserPortal.JourneyControl;
using TDP.Common.DataServices;
using TDP.Common.ServiceDiscovery;
using TDP.Common.PropertyManager;
using System.Web.UI.HtmlControls;
using TDP.Common;

namespace TDP.UserPortal.TDPMobile.Controls
{
    #region Public Event Definition

    /// <summary>
    /// EventsArgs class for passing journey id in OnSelectedJourneyLegChange event
    /// </summary>
    public class PlanJourneyEventArgs : EventArgs
    {
        private readonly TDPJourneyPlannerMode plannerMode = TDPJourneyPlannerMode.PublicTransport;

        /// <summary>
        /// Constructor
        /// </summary>
        public PlanJourneyEventArgs(TDPJourneyPlannerMode plannerMode)
        {
            this.plannerMode = plannerMode;
        }

        /// <summary>
        /// TDPJourneyPlannerMode
        /// </summary>
        public TDPJourneyPlannerMode PlannerMode
        {
            get { return plannerMode; }
        }
    }

    /// <summary>
    /// Delegate for Plan Journey button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PlanJourney(object sender, PlanJourneyEventArgs e);
        
    #endregion

    /// <summary>
    /// Journey input control containing input controls for location, dates, and dropdowns
    /// </summary>
    public partial class JourneyInputControl : System.Web.UI.UserControl
    {
        #region Private members

        private TDPJourneyPlannerMode plannerMode = TDPJourneyPlannerMode.PublicTransport;
        private LocationInputMode locationInputMode = LocationInputMode.NoVenue;

        private List<TDPVenueCyclePark> cycleParkList = null;
        private string cycleParksSelect = string.Empty;
        private string cycleParksDisabled = string.Empty;
        private string locationParkDrpNoParks = string.Empty;
        private string journeyType = string.Empty;

        private List<string> validationMessages = new List<string>();
        private bool validLocationFrom = false;
        private bool validLocationTo = false;
        private bool validDate = false;
        private bool validAdvancedOptions = false;
        private bool validCyclePark = false;

        #endregion

        #region Public Events

        public event PlanJourney OnPlanJourney;

        public event PlanJourney OnToggleLocation;

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. Determines the options and functionality enabled in this control
        /// </summary>
        public TDPJourneyPlannerMode PlannerMode
        {
            get { return plannerMode; }
            set
            {
                plannerMode = value;

                // Mobile only supports PT and Cycle
                switch (plannerMode)
                {
                    case TDPJourneyPlannerMode.Cycle:
                    case TDPJourneyPlannerMode.PublicTransport:
                        break;
                    default:
                        plannerMode = TDPJourneyPlannerMode.PublicTransport;
                        break;
                }

                advancedOptionsControl.Initialise(plannerMode);
            }
        }
        
        /// <summary>
        /// Read/Write. Transport Modes available for selection
        /// </summary>
        public List<TDPModeType> Modes
        {
            get { return advancedOptionsControl.Modes; }
            set { advancedOptionsControl.Modes = value; }
        }

        /// <summary>
        /// Read/Write. Sets the Location controls input mode
        /// </summary>
        public LocationInputMode LocationInputMode
        {
            get
            {
                if (locationFrom.TypeOfLocation == TDPLocationType.Venue)
                {
                    if (locationTo.TypeOfLocation == TDPLocationType.Venue)
                    {
                        locationInputMode = LocationInputMode.VenueToVenue;
                    }
                    else
                    {
                        locationInputMode = LocationInputMode.FromVenue;
                    }
                }
                else if (locationTo.TypeOfLocation == TDPLocationType.Venue)
                {
                    locationInputMode = LocationInputMode.ToVenue;
                }
                else
                {
                    locationInputMode = LocationInputMode.NoVenue;
                }

                return locationInputMode;
            }
            set
            {
                locationInputMode = value;

                SetupJourneyLocationInputControls();
            }
        }

        /// <summary>
        /// Read/Write property indicates if the location should be resolved when Validate is called.
        /// This is to allow a pre-resolved location set during a landing page to bypass resolution when
        /// autoplan is in effect (as the control will not have gone through its life cycle and therefore not 
        /// fully setup).
        /// Default is true.
        /// </summary>
        public bool ResolveLocationFrom
        {
            get { return locationFrom.ResolveLocation; }
            set { locationFrom.ResolveLocation = value; }
        }

        /// <summary>
        /// Read/Write. TDPLocation to be used as the From
        /// </summary>
        public TDPLocation LocationFrom
        {
            get { return locationFrom.Location; }
            set { locationFrom.Location = value; }
        }

        /// <summary>
        /// Read/Write. LocationSearch used for the From location 
        /// </summary>
        public LocationSearch LocationSearchFrom
        {
            get { return locationFrom.Search; }
            set { locationFrom.Search = value; }
        }

        /// <summary>
        /// Read/Write property indicates if the location should be resolved when Validate is called.
        /// This is to allow a pre-resolved location set during a landing page to bypass resolution when
        /// autoplan is in effect (as the control will not have gone through its life cycle and therefore not 
        /// fully setup).
        /// Default is true.
        /// </summary>
        public bool ResolveLocationTo
        {
            get { return locationTo.ResolveLocation; }
            set { locationTo.ResolveLocation = value; }
        }

        /// <summary>
        /// Read/Write. TDPLocation to be used as the To
        /// </summary>
        public TDPLocation LocationTo
        {
            get { return locationTo.Location; }
            set { locationTo.Location = value; }
        }

        /// <summary>
        /// Read/Write. LocationSearch used for the To location 
        /// </summary>
        public LocationSearch LocationSearchTo
        {
            get { return locationTo.Search; }
            set { locationTo.Search = value; }
        }

        /// <summary>
        /// Read/Write. Outward datetime
        /// </summary>
        public DateTime OutwardDateTime
        {
            get { return eventControl.OutwardDateTime; }
            set { eventControl.OutwardDateTime = value; }
        }

        /// <summary>
        /// Read/Write. Outward arrive by or leave at flag
        /// </summary>
        public bool OutwardDateTimeArriveBy
        {
            get { return eventControl.ArriveBy; }
            set { eventControl.ArriveBy = value; }
        }

        /// <summary>
        /// Read/Write property indicates if the datetime should be updated
        /// (e.g. if in the past)
        /// </summary>
        public bool OutwardDateTimeForceUpdate
        {
            get { return eventControl.ForceUpdate; }
            set { eventControl.ForceUpdate = value; }
        }
        
        /// <summary>
        /// Sets the IsVenueOnly property of the from location
        /// </summary>
        public bool LocationFromVenueOnly
        {
            get { return locationFrom.IsVenueOnly; }
            set { locationFrom.IsVenueOnly = value; }
        }

        /// <summary>
        /// Sets the IsVenueOnly property of the to location
        /// </summary>
        public bool LocationToVenueOnly
        {
            get { return locationTo.IsVenueOnly; }
            set { locationTo.IsVenueOnly = value; }
        }

        #region Accessible specific

        /// <summary>
        /// Accessible journey options control
        /// </summary>
        public AccessibleOptionsControl AccessibleOptions
        {
            get { return advancedOptionsControl.AccessibleOptions; }
            set { advancedOptionsControl.AccessibleOptions = value; }
        }

        #endregion

        #region Cycle specific

        /// <summary>
        /// Read only property to represent the selected TDPVenueCyclePark
        /// </summary>
        public TDPVenueCyclePark SelectedCyclePark
        {
            get
            {
                return GetSelectedCyclePark();
            }
        }

        /// <summary>
        /// Read only property to return the selected date time updated with any cycle park transit times
        /// </summary>
        public DateTime SelectedCycleParkDateTime
        {
            get
            {
                DateTime selectedOutwardDateTime = OutwardDateTime;

                // Update the time to be the outward datetime plus or minus any transit times from 
                // venue to the location park
                if (selectedOutwardDateTime != DateTime.MinValue)
                {
                    switch (GetLocationInputMode())
                    {
                        case LocationInputMode.FromVenue:
                            selectedOutwardDateTime = selectedOutwardDateTime.Add(GetCycleTransitTime(LocationFrom, true));
                            break;
                        case LocationInputMode.VenueToVenue:
                        case LocationInputMode.ToVenue:
                        default:
                            selectedOutwardDateTime = selectedOutwardDateTime.Subtract(GetCycleTransitTime(LocationTo, false));
                            break;
                    }
                }

                return selectedOutwardDateTime;
            }
        }

        /// <summary>
        /// Read only property to return the selected journey type (i.e. selected cycle route)
        /// </summary>
        public string SelectedCycleJourneyType
        {
            get
            {
                if (journeyTypeRdo != null && journeyTypeRdo.Items.Count > 0)
                {
                    journeyType = journeyTypeRdo.SelectedValue;
                }

                return journeyType;
            }
            set { journeyType = value; }
        }

        #endregion

        #region Validate properties

        /// <summary>
        /// Read property indicates if the Location From is valid. 
        /// Default is false until Validate() is called
        /// </summary>
        public bool IsValidLocationFrom
        {
            get { return validLocationFrom; }
        }

        /// <summary>
        /// Read property indicates if the Location To is valid. 
        /// Default is false until Validate() is called
        /// </summary>
        public bool IsValidLocationTo
        {
            get { return validLocationTo; }
        }

        /// <summary>
        /// Read property indicates if the Date and Time are valid. 
        /// Default is false until Validate() is called
        /// </summary>
        public bool IsValidDate
        {
            get { return validDate; }
        }

        /// <summary>
        /// Read property indicates if when in Cycle planner mode, a Cycle park has been 
        /// selected (if applicable, e.g. if venues data has been removed and cycle functionality turned on, 
        /// then journey can be planned from the entered locations) and is valid. 
        /// Default is false until Validate() is called
        /// </summary>
        public bool IsValidCyclePark
        {
            get { return validCyclePark; }
        }

        /// <summary>
        /// Read only property contains any validation messages raised within the controls itself
        /// </summary>
        public List<string> ValidationMessages
        {
            get
            {
                if (!string.IsNullOrEmpty(locationTo.ValidationMessage))
                {
                    validationMessages.Insert(0, locationTo.ValidationMessage);
                }

                if (!string.IsNullOrEmpty(locationFrom.ValidationMessage))
                {
                    validationMessages.Insert(0, locationFrom.ValidationMessage);
                }

                if (!string.IsNullOrEmpty(advancedOptionsControl.ValidationMessage))
                {
                    validationMessages.Add(advancedOptionsControl.ValidationMessage);
                }

                return validationMessages;
            }
        }

        #endregion

        #endregion

        #region Page_Init, Page_Load, Page_PreRender

        protected void Page_Init(object sender, EventArgs e)
        {
            locationFrom.OnToggleLocation += new ToggleLocation(TravelFromToToggle_Click);
            locationTo.OnToggleLocation += new ToggleLocation(TravelFromToToggle_Click);
        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // Ensure the venue location is set in the location control, 
                // to allow any controls requiring the venue (e.g. the cycle park locations)
                if (locationTo.TypeOfLocation == TDPLocationType.Venue) // To venue journey
                {
                    locationTo.Validate();
                }
                else if (locationFrom.TypeOfLocation == TDPLocationType.Venue) // From venue journey
                {
                    locationFrom.Validate();
                }
            }

            SetupResources();

            SetupLocationParkDropDown();

            if (!IsPostBack)
            {
                SetupJourneyTypeDropDown();
            }
        }

        /// <summary>
        /// Page_PreRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupControlVisibility();

            SetupControlResource();

            // Update date control with the lastest location input mode for its 
            // arrive by/now date time logic
            eventControl.IsToVenue = LocationToVenueOnly;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Initialises the control
        /// </summary>
        /// <param name="searchString">Location to be search</param>
        /// <param name="locationType">Type of location</param>
        public void Initialise(
            TDPLocation origin, LocationSearch searchOrigin,
            TDPLocation destination, LocationSearch searchDestination,
            bool isLandingPage)
        {
            locationFrom.Initialise(origin, searchOrigin, false, true, false, isLandingPage);
            locationTo.Initialise(destination, searchDestination, true, false, true, isLandingPage);
        }

        /// <summary>
        /// Method which resets this control into its default state
        /// </summary>
        public void Reset()
        {
            // Default input mode to NoVenue
            locationInputMode = LocationInputMode.NoVenue;

            locationFrom.Initialise(null, null, false, true, false, false);
            locationTo.Initialise(null, null, true, false, true, false);

            locationFrom.Reset(TDPLocationType.Unknown);
            locationTo.Reset(TDPLocationType.Unknown);
            
            locationFrom.ResolveLocation = true;
            locationTo.ResolveLocation = true;

            eventControl.OutwardDateTime = DateTime.MinValue;
            eventControl.ForceUpdate = true;

            advancedOptionsControl.Reset(plannerMode);

            SetupJourneyLocationInputControls();

            validLocationFrom = false;
            validLocationTo = false;
            validDate = false;
        }

        /// <summary>
        /// Validates all input controls, and returns the overall valid status
        /// </summary>
        public bool Validate()
        {
            validLocationFrom = locationFrom.Validate();
            validLocationTo = locationTo.Validate();
            validDate = eventControl.Validate();

            if (plannerMode == TDPJourneyPlannerMode.PublicTransport)
                validAdvancedOptions = advancedOptionsControl.Validate();
            else
                validAdvancedOptions = true;

            if (plannerMode == TDPJourneyPlannerMode.Cycle)
            {
                // See if a cycle park has been selected
                validCyclePark = (SelectedCyclePark != null);
                                
                if (!validCyclePark)
                {
                    TDPPageMobile page = (TDPPageMobile)Page;

                    if (cycleParkList != null && cycleParkList.Count > 0)
                        validationMessages.Add(page.GetResourceMobile("JourneyInput.LocationPark.ValidationError.SelectPark"));
                    else
                        // No cycle parks for venue
                        validationMessages.Add(page.GetResourceMobile("JourneyInput.LocationPark.ValidationError.NoParks"));

                }
            }
            else
                validCyclePark = true;

            return validLocationFrom && validLocationTo && validDate && validAdvancedOptions && validCyclePark;
        }

        /// <summary>
        /// Calls the travel from to toggle location event handler
        /// </summary>
        public void ToggleLocations()
        {
            TravelFromToToggle_Click(this, null);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads resource strings for labels/controls
        /// </summary>
        private void SetupResources()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            // Controls
            locationFrom.LocationDirectionLabel.Text = page.GetResourceMobile("JourneyInput.Location.From.Text");
            locationTo.LocationDirectionLabel.Text = page.GetResourceMobile("JourneyInput.Location.To.Text");
                        
            // AccessKey to allow user direct access to the location input
            locationFrom.LocationInputTextBox.AccessKey = "F";
            locationTo.LocationInputTextBox.AccessKey = "T";

            // Aria accessibility (toggle should only be shown on To location
            locationTo.TravelFromToToggle.Attributes["aria-controls"] = string.Format("{0} {1}",
                locationFrom.LocationInputTextBox.ClientID, locationTo.LocationInputTextBox.ClientID);

            cycleParksSelect = page.GetResourceMobile("JourneyInput.LocationPark.Text");
            cycleParksDisabled = page.GetResourceMobile("JourneyInput.LocationPark.Disabled.Text");
            parkselected.Text = cycleParksSelect;

            planJourneyBtn.Text = Server.HtmlDecode(page.GetResourceMobile("JourneyInput.PlanJourney.Text"));
            planJourneyBtn.ToolTip = Server.HtmlDecode(page.GetResourceMobile("JourneyInput.PlanJourney.ToolTip"));

            journeyTypeHeading.InnerText = page.GetResourceMobile("JourneyInput.TypeOfRouteHeading.Text");
            journeyTypeHeading.Attributes["title"] = page.GetResourceMobile("JourneyInput.TypeOfRouteHeading.Text");

            // Strings
            locationParkDrpNoParks = page.GetResourceMobile("JourneyInput.LocationPark.DropDown.NoParks");

            cycleParkSelectorLabel.InnerText = page.GetResourceMobile("JourneyInput.SelectCyclePark.Text");
            closeparkmap.Text = page.GetResourceMobile("JourneyInput.Back.Text");
            closeparkmap.ToolTip = page.GetResourceMobile("JourneyInput.Back.Text");
            cycleParkMapImageLabel.Text = page.GetResourceMobile("JourneyInput.LocationPark.Map.Text");
        }

        /// <summary>
        /// Loads control resource strings on page prerender
        /// </summary>
        private void SetupControlResource()
        {
            TDPPageMobile page = (TDPPageMobile)Page;

            // Update journey type heading to be the selected text
            // If mobile summary page, then show selected text,
            // Otherwise, only show it if user has manually selected
            if ((page.PageId == PageId.MobileSummary)
                || (Page.IsPostBack && !string.IsNullOrEmpty(journeyTypeSelected.Value)))
            {
                if (journeyTypeRdo != null && journeyTypeRdo.Items.Count > 0)
                {
                    journeyTypeHeading.InnerText = journeyTypeRdo.SelectedItem.Text;
                }
            }
        }

        /// <summary>
        /// Updates the control visibility depending on the planner mode
        /// </summary>
        private void SetupControlVisibility()
        {
            // Set control visibility dependent on mode
            switch (plannerMode)
            {
                case TDPJourneyPlannerMode.Cycle:

                    LocationInputMode locationInputMode = GetLocationInputMode();

                    locationParkDiv.Visible = true;
                    journeyTypeDiv.Visible = true;
                    advancedOptionsDiv.Visible = false;

                    LocationHelper locationHelper = new LocationHelper();

                    // If venues are not available, then hide cycle parks div, 
                    // allowing journeys to be planned from the locations selected
                    if (locationInputMode == LocationInputMode.NoVenue
                        && !locationHelper.VenueLocationsAvailable())
                    {
                        locationParkDiv.Visible = false;
                    }

                    parkselected.Enabled = true;
                    // Disable the cycle park selection if no venue selected
                    switch (locationInputMode)
                    {
                        case LocationInputMode.FromVenue:
                            if (LocationFrom == null)
                            {
                                parkselected.Text = cycleParksDisabled;
                                parkselected.Enabled = false;

                                SetCycleParkStyle(false);
                            }
                            break;
                        case LocationInputMode.VenueToVenue:
                        case LocationInputMode.ToVenue:
                            if (LocationTo == null)
                            {
                                parkselected.Text = cycleParksDisabled;
                                parkselected.Enabled = false;

                                SetCycleParkStyle(false);
                            }
                            break;
                        case LocationInputMode.NoVenue:
                        default:
                            parkselected.Text = cycleParksDisabled;
                            parkselected.Enabled = false;

                            SetCycleParkStyle(false);
                            break;
                    }

                    break;
                default:
                    locationParkDiv.Visible = false;
                    journeyTypeDiv.Visible = false;
                    advancedOptionsDiv.Visible = Properties.Current["PublicJourneyOptions.AdvancedOptions.Visible"].Parse(false);
                    break;
            }
        }

        /// <summary>
        /// Sets up the arrive by or leave at flag on the date control
        /// dependent on the location input mode
        /// </summary>
        private void SetupEventDateArriveBy()
        {
            if (LocationToVenueOnly)
            {
                eventControl.ArriveBy = true;
            }
            else if (LocationFromVenueOnly)
            {
                eventControl.ArriveBy = false;
            }

            // reset the time field only
            DateTime resetDateTime = eventControl.OutwardDateTime;
            eventControl.OutwardDateTime = new DateTime(resetDateTime.Year, resetDateTime.Month, resetDateTime.Day, 0, 0, 0);
            eventControl.ResetTime = true;
        }

        #region Location input

        /// <summary>
        /// Updates the location input controls to be in the correct LocationInputMode
        /// </summary>
        private void SetupJourneyLocationInputControls()
        {
            switch (LocationInputMode)
            {
                case LocationInputMode.FromVenue:
                    locationFrom.Reset(TDPLocationType.Venue);
                    locationFrom.ResolveLocation = true;
                    locationTo.Reset(TDPLocationType.Unknown);
                    locationTo.ResolveLocation = true;
                    break;

                case LocationInputMode.VenueToVenue:
                    locationFrom.Reset(TDPLocationType.Venue);
                    locationFrom.ResolveLocation = true;
                    locationTo.Reset(TDPLocationType.Venue);
                    locationTo.ResolveLocation = true;
                    break;

                case LocationInputMode.ToVenue:
                    locationFrom.Reset(TDPLocationType.Unknown);
                    locationFrom.ResolveLocation = true;
                    locationTo.Reset(TDPLocationType.Venue);
                    locationTo.ResolveLocation = true;
                    break;

                case LocationInputMode.NoVenue:
                default:
                    locationFrom.Reset(TDPLocationType.Unknown);
                    locationFrom.ResolveLocation = true;
                    locationTo.Reset(TDPLocationType.Unknown);
                    locationTo.ResolveLocation = true;
                    break;
            }
        }

        /// <summary>
        /// Returns the current LocationInputMode based on the state of the From and To location controls
        /// </summary>
        /// <returns></returns>
        private LocationInputMode GetLocationInputMode()
        {
            #region Get current location input mode

            LocationInputMode currentInputMode = LocationInputMode.NoVenue;

            if (locationFrom.TypeOfLocation == TDPLocationType.Venue
                && locationTo.TypeOfLocation == TDPLocationType.Venue)
            {
                currentInputMode = LocationInputMode.VenueToVenue;
            }
            else if (locationFrom.TypeOfLocation == TDPLocationType.Venue
                && locationTo.TypeOfLocation != TDPLocationType.Venue)
            {
                currentInputMode = LocationInputMode.FromVenue;
            }
            else if (locationFrom.TypeOfLocation != TDPLocationType.Venue
                && locationTo.TypeOfLocation == TDPLocationType.Venue)
            {
                currentInputMode = LocationInputMode.ToVenue;
            }
            
            #endregion

            return currentInputMode;
        }

        /// <summary>
        /// Toggles the From/To location controls to be Venue-to-venue, From-venue, To-venue
        /// </summary>
        private void ToggleLocationControl(LocationInputMode locationInputMode)
        {
            // Determine current mode (to allow correct reassigning of locations
            LocationInputMode currentInputMode = GetLocationInputMode();

            #region Get currently entered locations

            // Retain the selected locations, to allow inserting back in if possible
            TDPLocation from = locationFrom.Location;
            TDPLocation to = locationTo.Location;

            // Validate incase the location object hasnt been retrieved from the cache
            if (locationFrom.Location == null && locationFrom.Status == LocationStatus.Unspecified)
            {
                if (locationFrom.Validate())
                {
                    from = locationFrom.Location;
                }
                else if (locationFrom.TypeOfLocation == TDPLocationType.Venue)
                {
                    // Issue where constant switching loses the non-venue location 
                    // - this is because the validate works on locationtype, so try and validate as a non-venue
                    // because the location could have non-venue values in its hidden settings preserved from 
                    // a previous location input mode switch
                    if (locationFrom.Validate(TDPLocationType.Unknown))
                    {
                        from = locationFrom.Location;
                    }
                }
            }

            // Validate incase the location object hasnt been retrieved from the cache
            if (locationTo.Location == null && locationTo.Status == LocationStatus.Unspecified)
            {
                if (locationTo.Validate())
                {
                    to = locationTo.Location;
                }
                else if (locationTo.TypeOfLocation == TDPLocationType.Venue)
                {
                    // Issue where constant switching loses the non-venue location 
                    // - this is because the validate works on locationtype, so try and validate as a non-venue
                    // because the location could have non-venue values in its hidden settings preserved from 
                    // a previous location input mode switch
                    if (locationTo.Validate(TDPLocationType.Unknown))
                    {
                        to = locationTo.Location;
                    }
                }
            }

            #endregion

            #region Set for new location input mode

            // Set the control location input mode
            this.locationInputMode = locationInputMode;

            // And update the location input to be in the correct input state
            SetupJourneyLocationInputControls();

            #endregion

            #region Set currently entered locations for the new location input mode (where possible)

            // Reassign locations based on the current to the new location input mode.
            // The following has a lot of switches and if's because of the various permutations in the 
            // order the user can switch between the location input modes. Re-Factor at your own risk!
            switch (currentInputMode)
            {
                case LocationInputMode.VenueToVenue:

                    switch (locationInputMode)
                    {
                        // Retain the currently selected venue in the From/To venue mode
                        case LocationInputMode.ToVenue:

                            if ((to != null && to.TypeOfLocation == TDPLocationType.Venue)
                                && (from != null && from.TypeOfLocation != TDPLocationType.Venue))
                            {
                                locationTo.Location = to;
                                locationFrom.Location = from;
                            }
                            // Handle sceneario where From -> Venue -> To
                            else if ((to != null && to.TypeOfLocation != TDPLocationType.Venue)
                                && (from != null && from.TypeOfLocation == TDPLocationType.Venue))
                            {
                                locationTo.Location = from;
                                locationFrom.Location = to;
                                SwapControlTypes();
                            }
                            else if (to != null && to.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationTo.Location = to;
                            }
                            break;
                        case LocationInputMode.FromVenue:

                            if ((to != null && to.TypeOfLocation != TDPLocationType.Venue)
                                && (from != null && from.TypeOfLocation == TDPLocationType.Venue))
                            {
                                locationTo.Location = to;
                                locationFrom.Location = from;
                            }
                            // Handle sceneario where To -> Venue -> From
                            else if ((to != null && to.TypeOfLocation == TDPLocationType.Venue)
                                && (from != null && from.TypeOfLocation != TDPLocationType.Venue))
                            {
                                locationTo.Location = from;
                                locationFrom.Location = to;
                                SwapControlTypes();
                            }
                            else if (from != null && from.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationFrom.Location = from;
                            }
                            break;
                        case LocationInputMode.VenueToVenue:

                            // Switching the venue locations around
                            if (to != null && to.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationFrom.Location = to;
                            }
                            if (from != null && from.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationTo.Location = from;
                            }
                            SwapControlTypes();
                            break;
                    }
                    break;

                case LocationInputMode.FromVenue:

                    switch (locationInputMode)
                    {
                        case LocationInputMode.VenueToVenue:
                            // Retain the currently selected venue
                            if (from != null && from.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationFrom.Location = from;
                            }
                            else if (to != null && to.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationFrom.Location = to;
                            }
                            break;
                        case LocationInputMode.ToVenue:
                            // Switch the currently input locations around
                            if (from != null && from.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationTo.Location = from; // To is now the venue
                            }
                            if (to != null && to.TypeOfLocation != TDPLocationType.Venue)
                            {
                                locationFrom.Location = to; // From is now the not-venue
                            }
                            SwapControlTypes();
                            break;
                    }
                    break;

                case LocationInputMode.ToVenue:

                    switch (locationInputMode)
                    {
                        case LocationInputMode.VenueToVenue:
                            // Retain the currently selected venue
                            if (to != null && to.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationTo.Location = to;
                            }
                            else if (from != null && from.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationTo.Location = from;
                            }
                            break;
                        case LocationInputMode.FromVenue:
                            // Switch the currently input locations around
                            if (from != null && from.TypeOfLocation != TDPLocationType.Venue)
                            {
                                locationTo.Location = from; // To is now the not-venue
                            }
                            if (to != null && to.TypeOfLocation == TDPLocationType.Venue)
                            {
                                locationFrom.Location = to; // From is now the venue
                            }
                            SwapControlTypes();
                            break;
                    }
                    break;

                case LocationInputMode.NoVenue:
                default:
                    // Assume no venues
                    switch (locationInputMode)
                    {
                        case LocationInputMode.NoVenue:
                            if (from != null && from.TypeOfLocation != TDPLocationType.Venue)
                            {
                                locationTo.Location = from; // To is not the From not-venue
                            }
                            if (to != null && to.TypeOfLocation != TDPLocationType.Venue)
                            {
                                locationFrom.Location = to; // From is now the To not-venue
                            }
                            break;
                    }
                    break;
            }

            #endregion

            #region Update other controls for new location input mode

            // Ensure location parks are shown as required
            SetupLocationParkDropDown();

            #endregion
        }

        /// <summary>
        /// Swaps the "venue only" location input
        /// </summary>
        private void SwapControlTypes()
        {
            bool toControlType = locationTo.IsVenueOnly;
            locationTo.IsVenueOnly = locationFrom.IsVenueOnly;
            locationFrom.IsVenueOnly = toControlType;

            // If both are venue only, then always make the to venue only
            if (locationTo.IsVenueOnly && locationFrom.IsVenueOnly)
            {
                locationFrom.IsVenueOnly = false;
            }
        }

        #endregion

        #region Location parks

        /// <summary>
        /// Sets up the location park drop down list
        /// </summary>
        private void SetupLocationParkDropDown()
        {
            if (plannerMode == TDPJourneyPlannerMode.Cycle)
            {                
                switch (GetLocationInputMode())
                {
                    case LocationInputMode.FromVenue:
                        LoadCycleParkMap(LocationFrom);
                        break;
                    case LocationInputMode.VenueToVenue:
                    case LocationInputMode.ToVenue:
                        LoadCycleParkMap(LocationTo);
                        break;
                    case LocationInputMode.NoVenue:
                    default:
                        // No cycle parks to load
                        break;
                }

                // Set existing values
                if (SelectedCyclePark != null)
                {
                    parkselected.Text = SelectedCyclePark.Name;
                    selectedCycleParkID.Value = SelectedCyclePark.ID;
                }
            }
        }

        /// <summary>
        /// Load the appropriate cycle map and setup the cycle park
        /// </summary>
        /// <param name="location"></param>
        public void LoadCycleParkMap(TDPLocation location)
        {
            String venueId = null;
            RadioButtonList cycleParksNoMap = null;

            // Get the venue id used to load image and text from resources
            if (location != null)
            {
                venueId = location.ID;
            }

            if (!String.IsNullOrEmpty(venueId))
            {
                TDPPageMobile page = (TDPPageMobile)this.Page;

                cycleParkList = GetCycleParks(location);

                if (cycleParkList != null)
                {
                    cycleParkMapContainer.Visible = true;

                    // Try getting the image for the venue first, 
                    // if that doesnt exist, try retrieving for its parent as we show cycle parks for it's parent venue too
                    if (page.GetResourceMobile("JourneyLocations." + venueId + ".CycleParkM.Url") == null)
                    {
                        // Update with parent id if exists
                        venueId = (location.Parent != string.Empty) ? location.Parent : location.ID;
                    }

                    if (page.GetResourceMobile("JourneyLocations." + venueId + ".CycleParkM.Url") != null)
                    {
                        // make sure the map control is visible and the radio button list (if it exists) is removed
                        contentMap.Visible = true;
                        if (cycleParksNoMap != null && contentMap.FindControl(cycleParksNoMap.ID) != null)
                        {
                            contentMap.Controls.Remove(cycleParksNoMap);
                        }

                        cycleParkMapImage.ImageUrl = page.ImagePath + page.GetResourceMobile("JourneyLocations." + venueId + ".CycleParkM.Url");
                        cycleParkMapImage.AlternateText = page.GetResourceMobile("JourneyLocations." + venueId + ".CycleParkM.AlternateText");
                        cycleParkMapImage.ToolTip = page.GetResourceMobile("JourneyLocations." + venueId + ".CycleParkM.AlternateText");

                        cycleParkLinks.Controls.Clear();

                        foreach (TDPVenueCyclePark cyclePark in cycleParkList)
                        {
                            if (contentMap.FindControl(cyclePark.ID) == null)
                            {
                                try
                                {
                                    // Add cycle park radio button onto map. 
                                    // CSS with park id is used for positioning
                                    using (System.Web.UI.WebControls.RadioButton cycleMapRadio = new System.Web.UI.WebControls.RadioButton())
                                    {
                                        using (Panel optionDiv = new Panel())
                                        {
                                            optionDiv.ID = cyclePark.ID;
                                            optionDiv.CssClass = "paks";
                                            optionDiv.ToolTip = cyclePark.Name;

                                            cycleMapRadio.GroupName = "parks";
                                            cycleMapRadio.Attributes.Add("value", cyclePark.ID);
                                            cycleMapRadio.ID = "Park" + cyclePark.ID;
                                            cycleMapRadio.Text = cyclePark.Name;
                                            cycleMapRadio.ToolTip = cyclePark.Name;

                                            optionDiv.Controls.Add(cycleMapRadio);
                                            cycleParkLinks.Controls.Add(optionDiv);
                                        }
                                    }
                                }
                                catch { }
                            }

                            if (((TDPVenueLocation)location).SelectedTDPParkID == cyclePark.ID)
                            {
                                parkselected.Text = cyclePark.Name;
                                selectedCycleParkID.Value = cyclePark.ID;
                            }
                        }

                        parkselected.Enabled = true;
                        SetCycleParkStyle(true);
                    }
                    else
                    {
                        // turn off the cycle parks map field
                        contentMap.Visible = false;

                        using (cycleParksNoMap = new RadioButtonList())
                        {
                            cycleParksNoMap.ID = "cycleParksNoMap";
                            cycleParksNoMap.DataSource = cycleParkList;
                            cycleParksNoMap.DataTextField = "Name";
                            cycleParksNoMap.DataValueField = "ID";

                            cycleParkMapPage.Controls.Add(cycleParksNoMap);

                            cycleParksNoMap.DataBind();
                        }
                    }
                }
                else
                {
                    cycleParkMapContainer.Visible = false;
                    parkselected.Text = ((TDPPageMobile)Page).GetResourceMobile("JourneyInput.LocationPark.DropDown.NoParks");
                    parkselected.Enabled = false;
                    selectedCycleParkID.Value = "";

                    SetCycleParkStyle(false);
                }
            }
        }
        
        /// <summary>
        /// Gets the cycle parks for the provided venue
        /// </summary>
        private List<TDPVenueCyclePark> GetCycleParks(TDPLocation venue)
        {
            List<TDPVenueCyclePark> cycleParkList = null;

            // Venue should have been set by now
            if ((venue != null) && (venue.TypeOfLocation == TDPLocationType.Venue))
            {
                LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

                // Get cycle parks for the venue and its parent
                List<string> naptans = new List<string>(venue.Naptan);
                if (!string.IsNullOrEmpty(venue.Parent))
                {
                    naptans.Add(venue.Parent);
                }

                // Cycle parks,
                // ignoring dates because user may change the date after selecting a cycle park, 
                // submit journey request validation will flag if closed
                cycleParkList = locationService.GetTDPVenueCycleParks(naptans);
            }

            return cycleParkList;
        }

        /// <summary>
        /// Returns the selected cycle park
        /// </summary>
        /// <returns></returns>
        private TDPVenueCyclePark GetSelectedCyclePark()
        {
            TDPVenueCyclePark selectedCyclePark = null;

            #region Venues not available logic

            LocationHelper locationHelper = new LocationHelper();

            // If no venues available, then cycle park does not need to be selected, the 
            // locations are used directly to plan the journey from to, so provide 
            // an empty cycle park (may need updating in future if validation of cycle park changes)
            if (cycleParkList == null && !locationHelper.VenueLocationsAvailable())
            {
                selectedCyclePark = new TDPVenueCyclePark();

                // Set empty values where necessary
                selectedCyclePark.ID = string.Empty;
                selectedCyclePark.Name = string.Empty;
            }

            #endregion

            else if (cycleParkList != null)
            {
                // Read the hidden value for the selected cycle park
                selectedCyclePark = cycleParkList.SingleOrDefault(cp => cp.ID == selectedCycleParkID.Value);

                if (selectedCyclePark != null)
                {
                    // Handle scenario where cycle parks which have identical names but are open on different datetimes.
                    // Where this happens, update selection to be the open park, 
                    // otherwise pass in the selected park and let caller handle closed cycle parks
                    List<TDPVenueCyclePark> duplicateCycleParks = cycleParkList.FindAll(delegate(TDPVenueCyclePark cp) { return cp.Name == selectedCyclePark.Name; });

                    if (duplicateCycleParks != null && duplicateCycleParks.Count > 1)
                    {
                        // Find the cycle park which is open for the selected journey datetime. 
                        // Assumes this method is called after the user has selected datetime
                        DateTime datetime = OutwardDateTime;

                        foreach (TDPVenueCyclePark cp in duplicateCycleParks)
                        {
                            if (cp.IsOpenForDateAndTime(datetime, true))
                            {
                                selectedCyclePark = cp;
                                break;
                            }
                        }
                    }
                }

            }

            return selectedCyclePark;
        }

        /// <summary>
        /// Sets the disabled class on the select cycle park link
        /// </summary>
        private void SetCycleParkStyle(bool enabled)
        {
            if (enabled)
            {
                if (locationParkDiv.Attributes["class"].Contains("locationParkRowDisabled"))
                    locationParkDiv.Attributes["class"] = locationParkDiv.Attributes["class"].Replace(" locationParkRowDisabled", string.Empty);

                if (parkselected.CssClass.Contains("cycleParkLinkDisabled"))
                    parkselected.CssClass = parkselected.CssClass.Replace(" cycleParkLinkDisabled", string.Empty);
            }
            else
            {
                if (!locationParkDiv.Attributes["class"].Contains("locationParkRowDisabled"))
                    locationParkDiv.Attributes["class"] += " locationParkRowDisabled";

                if (!parkselected.CssClass.Contains("cycleParkLinkDisabled"))
                    parkselected.CssClass += " cycleParkLinkDisabled";
            }
        }

        #endregion

        #region Journey type

        /// <summary>
        /// Sets up and populates journey type dropdown (i.e. cycle penalty functions) using dataservices
        /// </summary>
        private void SetupJourneyTypeDropDown()
        {
            if (plannerMode == TDPJourneyPlannerMode.Cycle)
            {
                IDataServices dataServices = TDPServiceDiscovery.Current.Get<IDataServices>(ServiceDiscoveryKey.DataServices);

                dataServices.LoadListControl(DataServiceType.CycleJourneyType, journeyTypeRdo, Global.TDPResourceManager, CurrentLanguage.Value);

                // Set the selected value to be the specified value (allows previous choice to be retained when returning to page)
                if (!string.IsNullOrEmpty(journeyType))
                {
                    if (journeyTypeRdo != null && journeyTypeRdo.Items.Count > 0)
                    {
                        string selected = journeyTypeRdo.SelectedValue;
                        // Place in a try just to be safe
                        try
                        {
                            journeyTypeRdo.SelectedValue = journeyType;
                        }
                        catch
                        {
                            // Ignore exceptions and set back to its previous selected value
                            journeyTypeRdo.SelectedValue = selected;
                        }
                    }
                }

            }
        }

        #endregion

        /// <summary>
        /// Returns the total transit time between the Venue and Cycle park
        /// </summary>
        /// <returns></returns>
        private TimeSpan GetCycleTransitTime(TDPLocation venue, bool isReturn)
        {
            TimeSpan time = new TimeSpan();

            TDPVenueCyclePark cyclePark = SelectedCyclePark;

            if (cyclePark != null)
            {
                #region Cycle park to/from the venue interchange

                // Use Walk To gate duration, or Walk From gate for the return journey if it exists
                TimeSpan duration = cyclePark.WalkToGateDuration;

                if ((isReturn) && (cyclePark.WalkFromGateDuration.TotalMinutes > 0))
                {
                    duration = cyclePark.WalkFromGateDuration;
                }

                time = time.Add(duration);

                #endregion

                #region Venue gate and check constraints

                LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

                // Get the venue gate and path details for cycle park

                // Use Entrance naptan always, or Exit naptan for the return journey if it exists
                string gateNaptan = cyclePark.VenueGateEntranceNaPTAN;

                if ((isReturn) && (!string.IsNullOrEmpty(cyclePark.VenueGateExitNaPTAN)))
                {
                    gateNaptan = cyclePark.VenueGateExitNaPTAN;
                }

                TDPVenueGate gate = locationService.GetTDPVenueGate(gateNaptan);
                TDPVenueGateCheckConstraint gateCheckConstraint = locationService.GetTDPVenueGateCheckConstraints(gate, !isReturn);
                TDPVenueGateNavigationPath gateNavigationPath = locationService.GetTDPVenueGateNavigationPaths(venue, gate, !isReturn);

                if (gateCheckConstraint != null)
                {
                    time = time.Add(gateCheckConstraint.AverageDelay);
                }

                if (gateNavigationPath != null)
                {
                    time = time.Add(gateNavigationPath.TransferDuration);
                }

                #endregion
            }

            return time;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event handler for toggle locations click
        /// </summary>
        protected void TravelFromToToggle_Click(object sender, EventArgs e)
        {
            // Raise event to allow page to know toggle locations selected
            if (OnToggleLocation != null)
            {
                OnToggleLocation(this, null);
            }

            // Reset selected cycle park before toggling locations as the method will
            // update the cycle parks shown
            parkselected.Text = cycleParksSelect;
            selectedCycleParkID.Value = string.Empty;

            LocationInputMode currentInputMode = GetLocationInputMode();

            switch (currentInputMode)
            {
                case LocationInputMode.VenueToVenue:
                    ToggleLocationControl(LocationInputMode.VenueToVenue);
                    previousLocationInputMode.Value = currentInputMode.ToString();
                    break;
                case LocationInputMode.FromVenue:
                    ToggleLocationControl(LocationInputMode.ToVenue);
                    previousLocationInputMode.Value = currentInputMode.ToString();
                    break;
                case LocationInputMode.ToVenue:
                    ToggleLocationControl(LocationInputMode.FromVenue);
                    previousLocationInputMode.Value = currentInputMode.ToString();
                    break;
                case LocationInputMode.NoVenue:
                default:
                    ToggleLocationControl(LocationInputMode.NoVenue);
                    previousLocationInputMode.Value = currentInputMode.ToString();
                    break;
            }

            // Update date control to with latest location input mode
            SetupEventDateArriveBy();
        }

        /// <summary>
        /// Event handler for plan journey button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void planJourneyBtn_Click(object sender, EventArgs e)
        {
            if (OnPlanJourney != null)
            {
                OnPlanJourney(this, new PlanJourneyEventArgs(this.plannerMode));
            }
        }

        #endregion
    }
}
