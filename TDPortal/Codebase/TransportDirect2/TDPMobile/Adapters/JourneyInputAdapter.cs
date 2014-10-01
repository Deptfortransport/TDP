// *********************************************** 
// NAME             : JourneyInputAdapter.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Feb 2012
// DESCRIPTION  	: Journey Input page adapter responsible for populating inptut controls, journey request in session,
//                    validating the controls
// ************************************************
// 

using System;
using System.Collections.Generic;
using TDP.Common.LocationService;
using TDP.Common.Extenders;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.TDPMobile.Controls;
using TDP.Common.PropertyManager;
using TDP.Common;
using TDP.UserPortal.SessionManager;

namespace TDP.UserPortal.TDPMobile.Adapters
{
    /// <summary>
    /// Journey Input page adapter responsible for populating inptut controls, journey request in session,
    /// validating the controls
    /// </summary>
    public class JourneyInputAdapter
    {
        #region Private Fields

        private JourneyInputControl journeyInputControl;
        private AccessibleStopsControl accessibleStopsControl;

        private SessionHelper sessionHelper;
        private CookieHelper cookieHelper;
        private LocationHelper locationHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public JourneyInputAdapter()
        {
            sessionHelper = new SessionHelper();
            cookieHelper = new CookieHelper();
            locationHelper = new LocationHelper();
        }

        /// <summary>
        /// Constructor - Initialises the adapter with page controls necessary for journey
        /// </summary>
        public JourneyInputAdapter(JourneyInputControl journeyInputControl)
            : this()
        {
            this.journeyInputControl = journeyInputControl;
        }

        /// <summary>
        /// Constructor - Initialises the adapter with page controls necessary for accessible stop
        /// </summary>
        public JourneyInputAdapter(AccessibleStopsControl accessibleStopsControl)
            : this()
        {
            this.accessibleStopsControl = accessibleStopsControl;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Populates journey input page controls from session
        /// </summary>
        public void PopulateInputControls(bool isLandingPage)
        {
            ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();
            InputPageState pageState = TDPSessionManager.Current.PageState;

            if (journeyInputControl != null)
            {
                journeyInputControl.Reset();
            }

            if (tdpJourneyRequest != null)
            {
                if (journeyInputControl != null)
                {
                    // Set locations,
                    // Set the location search (this will either be null or populated from a postback when ambiguous)
                    journeyInputControl.Initialise(
                        tdpJourneyRequest.Origin, pageState.OriginSearch, 
                        tdpJourneyRequest.Destination, pageState.DestinationSearch, 
                        isLandingPage);
                    
                    // If landing page, then do not want to resolve location as it would have already
                    // been done (or if null/invalid then the control will say when Validate is called)
                    journeyInputControl.ResolveLocationFrom = !isLandingPage;
                    journeyInputControl.ResolveLocationTo = !isLandingPage;

                    #region Ensure location inputs are in a valid input scenario

                    // If landing page, then ensure location controls type of location is valid
                    if (isLandingPage)
                    {
                        // logic moved to JourneyInputControl.LocationInputMode
                    }

                    #endregion

                    LocationInputMode locationInputMode = journeyInputControl.LocationInputMode;

                    // If venue locations are not available, then remove "at least one venue" check
                    if (!locationHelper.VenueLocationsAvailable())
                    {
                        locationInputMode = LocationInputMode.NoVenue;
                    }
                    
                    switch (locationInputMode)
                    {
                        case LocationInputMode.VenueToVenue:
                            // Retain existing location as venue only, giving preference to the desitnation input
                            if (journeyInputControl.LocationToVenueOnly)
                            {
                                journeyInputControl.LocationFromVenueOnly = false;
                                journeyInputControl.LocationToVenueOnly = true;
                            }
                            else if (journeyInputControl.LocationFromVenueOnly)
                            {
                                journeyInputControl.LocationFromVenueOnly = true;
                                journeyInputControl.LocationToVenueOnly = false;
                            }
                            break;
                        case LocationInputMode.FromVenue:
                            journeyInputControl.LocationFromVenueOnly = true;
                            journeyInputControl.LocationToVenueOnly = false;
                            break;
                        case LocationInputMode.ToVenue:
                            journeyInputControl.LocationFromVenueOnly = false;
                            journeyInputControl.LocationToVenueOnly = true;
                            break;
                        case LocationInputMode.NoVenue:
                        default:
                            journeyInputControl.LocationFromVenueOnly = false;
                            journeyInputControl.LocationToVenueOnly = false;
                            break;
                    }

                    // If landing page, then want to ensure the dates are validated and updated if necessary
                    journeyInputControl.OutwardDateTimeForceUpdate = true; // isLandingPage;
                    journeyInputControl.OutwardDateTime = tdpJourneyRequest.OutwardDateTime;
                    journeyInputControl.OutwardDateTimeArriveBy = tdpJourneyRequest.OutwardArriveBefore;

                    journeyInputControl.PlannerMode = tdpJourneyRequest.PlannerMode;
                    journeyInputControl.Modes = tdpJourneyRequest.Modes;

                    // Set the selected value to be the specified value (allows previous choice to be retained when returning
                    // to the locations page)
                    journeyInputControl.SelectedCycleJourneyType = tdpJourneyRequest.CycleAlgorithm;


                    // Populate options
                    if (tdpJourneyRequest.AccessiblePreferences != null)
                    {
                        if (journeyInputControl.AccessibleOptions != null)
                        {
                            TDPAccessiblePreferences accessPref = tdpJourneyRequest.AccessiblePreferences;

                            journeyInputControl.AccessibleOptions.ExcludeUnderGround = accessPref.DoNotUseUnderground;
                            journeyInputControl.AccessibleOptions.FewestChanges = accessPref.RequireFewerInterchanges;

                            if (tdpJourneyRequest.AccessiblePreferences.RequireSpecialAssistance
                                && tdpJourneyRequest.AccessiblePreferences.RequireStepFreeAccess)
                            {
                                journeyInputControl.AccessibleOptions.StepFreeAndAssistance = true;
                            }
                            else if (tdpJourneyRequest.AccessiblePreferences.RequireSpecialAssistance)
                            {
                                journeyInputControl.AccessibleOptions.Assistance = accessPref.RequireSpecialAssistance;
                            }
                            else if (tdpJourneyRequest.AccessiblePreferences.RequireStepFreeAccess)
                            {
                                journeyInputControl.AccessibleOptions.StepFree = accessPref.RequireStepFreeAccess;
                            }
                        }
                    }
                } // journeyInputControl != null
            }
        }

        /// <summary>
        /// Updates journey input controls from session for a page postback
        /// </summary>
        public void UpdateInputControls()
        {
            ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();
            InputPageState pageState = TDPSessionManager.Current.PageState;

            if (tdpJourneyRequest != null)
            {
                if (journeyInputControl != null)
                {
                    // Set locations and search
                    journeyInputControl.Initialise(
                        tdpJourneyRequest.Origin, pageState.OriginSearch,
                        tdpJourneyRequest.Destination, pageState.DestinationSearch,
                        false);

                    // Set planner mode
                    journeyInputControl.PlannerMode = tdpJourneyRequest.PlannerMode;
                }
            }
            else
            {
                // No journey request yet
                // Do not reset the input control, just initialise - the control should deal with null location/search
                // and values entered by user
                journeyInputControl.Initialise(null, null, null, null, false);
            }
        }

        #region Validate and Build TDPJourneyRequest

        /// <summary>
        /// Validates the input page controls and populates a TDPJourneyRequest object using input page controls,
        /// and adds to the session
        /// </summary>
        /// <returns>True if the input page controls are in valid state</returns>
        public bool ValidateAndBuildTDPRequest(TDPJourneyPlannerMode plannerMode, bool ignoreInvalid)
        {
            bool valid = false;

            if (journeyInputControl != null)
            {
                // Validate inputs
                valid = journeyInputControl.Validate();

                // Locations and dates are valid, construct an TDPJourneyRequest,
                // If ignoreInvalid set, then caller requires a request in session built with the valid items,
                // e.g. if moving temporarily away from the input page to map and requires the selected items to be captured
                if (valid || ignoreInvalid)
                {
                    JourneyRequestHelper jrh = new JourneyRequestHelper();

                    ITDPJourneyRequest tdpJourneyRequest = jrh.BuildTDPJourneyRequest(
                        journeyInputControl.IsValidLocationFrom ? journeyInputControl.LocationFrom : null,
                        journeyInputControl.IsValidLocationTo ? journeyInputControl.LocationTo : null,
                        journeyInputControl.IsValidDate ? journeyInputControl.OutwardDateTime : DateTime.MinValue,
                        DateTime.MinValue, // TDPMobile does not do return journeys
                        journeyInputControl.OutwardDateTimeArriveBy,
                        false,
                        true,
                        false, // TDPMobile does not do return journeys
                        false,
                        plannerMode,
                        journeyInputControl.Modes,
                        GetAccessiblePreferences(plannerMode));

                    // Commit journey request to session
                    sessionHelper.UpdateSession(tdpJourneyRequest);

                    // Persist the journey request details in the cookie.
                    // This aids in recovery following session timeout, or when user 
                    // returns to site after a period of time (new session)
                    cookieHelper.UpdateJourneyRequestToCookie(tdpJourneyRequest);
                }

                if (!valid || ignoreInvalid)
                {
                    // Persist the location search values if invalid, to allow location input page
                    // to display/resolve ambiguous if necessary
                    InputPageState pageState = TDPSessionManager.Current.PageState;

                    pageState.OriginSearch = journeyInputControl.LocationSearchFrom;
                    pageState.DestinationSearch = journeyInputControl.LocationSearchTo;
                }
            }
            else if (accessibleStopsControl != null)
            {
                // Use the existing journey request but update origin / destination with GNAT stop
                JourneyRequestHelper jrh = new JourneyRequestHelper();

                // Get the request from the session
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();

                // Update origin / destination
                ValidateAndUpdateTDPRequestForAccessiblePublicTransport(accessibleStopsControl);

                // Commit journey request to session
                sessionHelper.UpdateSession(tdpJourneyRequest);

                // Persist the journey request details in the cookie.
                // This aids in recovery following session timeout, or when user 
                // returns to site after a period of time (new session)
                cookieHelper.UpdateJourneyRequestToCookie(tdpJourneyRequest);

                // Should alway be valid
                valid = true;
            }

            return valid;
        }

        /// <summary>
        /// Populates a new TDPJourneyRequest object using the supplied TDPJourneyRequest, 
        /// with the supplied parameters, and adds to the session
        /// </summary>
        /// <returns></returns>
        public bool ValidateAndBuildTDPRequestForReplan(ITDPJourneyRequest tdpJourneyRequest,
            DateTime replanOutwardDateTime,
            bool replanOutwardArriveBefore,
            List<Journey> outwardJourneys,
            bool retainOutwardJourneys,
            bool retainOutwardJourneysWhenNoResults
            )
        {
            bool valid = false;

            if (tdpJourneyRequest != null)
            {
                JourneyRequestHelper jrh = new JourneyRequestHelper();

                // Create a new request. 
                // Must create a new request as it's hash will be different, and should allow the 
                // "browser back" to still function, and support multi-tabbing (assumption!)
                ITDPJourneyRequest tdpJourneyRequestReplan = jrh.BuildTDPJourneyRequestForReplan(
                    tdpJourneyRequest,
                    true, false, // TDPMobile only supports outward journeys so cannot replan the return 
                    replanOutwardDateTime, DateTime.MinValue,
                    replanOutwardArriveBefore, false,
                    outwardJourneys, null,
                    retainOutwardJourneys, false,
                    retainOutwardJourneysWhenNoResults, false
                    );

                // Update the Journey request location with the earlier or later journey request hash
                tdpJourneyRequestReplan = jrh.UpdateTDPJourneyRequestEarlierLater(
                    tdpJourneyRequestReplan, !replanOutwardArriveBefore, tdpJourneyRequest.JourneyRequestHash);

                // Commit journey request to session
                sessionHelper.UpdateSession(tdpJourneyRequestReplan);

                // Don't persist the journey request details in the cookie, as this is a replan
                // and the original request should still be used (when user leaves site, or goes back
                // to input page)

                valid = true;
            }

            return valid;
        }

        #endregion

        #region Validate and Update Request

        /// <summary>
        /// Updates the journey request (if exists) and cookie with the planner mode and accessible options
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public void ValidateAndUpdateTDPRequestForPlannerMode(TDPJourneyPlannerMode plannerMode)
        {
            JourneyRequestHelper journeyRequestHelper = new JourneyRequestHelper();
            
            // Get journey request from session (if it exists)
            ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();

            if (tdpJourneyRequest != null)
            {
                journeyRequestHelper.UpdateTDPJourneyRequestPlannerMode(tdpJourneyRequest, plannerMode);

                // Commit journey request to session
                sessionHelper.UpdateSession(tdpJourneyRequest);

                // Persist the journey request details in the cookie.
                // This aids in recovery following session timeout, or when user 
                // returns to site after a period of time (new session)
                cookieHelper.UpdateJourneyRequestToCookie(tdpJourneyRequest);
            }
            else
            {
                // Build an empty request with planner mode  and accessible options specified
                tdpJourneyRequest = journeyRequestHelper.BuildTDPJourneyRequest(
                        null,
                        null,
                        DateTime.MinValue,
                        DateTime.MinValue, // TDPMobile does not do return journeys
                        false,
                        false,
                        true,
                        false, // TDPMobile does not do return journeys
                        false,
                        plannerMode,
                        null,
                        GetAccessiblePreferences(plannerMode));

                // Commit journey request to session
                sessionHelper.UpdateSession(tdpJourneyRequest);

                // Persist the journey request details in the cookie.
                // This aids in recovery following session timeout, or when user 
                // returns to site after a period of time (new session)
                cookieHelper.UpdateJourneyRequestToCookie(tdpJourneyRequest);
            }
        }

        /// <summary>
        /// Validates the venue for car/cycle parks and updates the journey request stored in session
        /// </summary>
        /// <param name="tdpPark">car or cycle park</param>
        public bool ValidateAndUpdateTDPRequestForTDPPark(TDPPark tdpPark,
            DateTime outwardDateTime, string cycleRouteType, TDPJourneyPlannerMode plannerMode)
        {
            if (tdpPark != null)
            {
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();

                // Update the journey request location with the Park and datetimes 
                JourneyRequestHelper journeyRequestHelper = new JourneyRequestHelper();
                tdpJourneyRequest = journeyRequestHelper.UpdateTDPJourneyRequestVenue(
                    tdpJourneyRequest, 
                    tdpPark, 
                    outwardDateTime,
                    DateTime.MinValue);

                if (!string.IsNullOrEmpty(cycleRouteType))
                {
                    // Update the selected cycle route penalty function algorithm
                    tdpJourneyRequest = journeyRequestHelper.UpdateTDPJourneyRequestCycle(tdpJourneyRequest, cycleRouteType);
                }

                // Commit journey request to session
                sessionHelper.UpdateSession(tdpJourneyRequest);

                // Persist the journey request details in the cookie.
                // This aids in recovery following session timeout, or when user 
                // returns to site after a period of time (new session)
                cookieHelper.UpdateJourneyRequestToCookie(tdpJourneyRequest);

                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Validates the location and updates the journey request stored in session
        /// </summary>
        /// <param name="selectedLocation"></param>
        /// <returns></returns>
        public bool ValidateAndUpdateTDPRequestForAccessiblePublicTransport(AccessibleStopsControl accessibleStopsControl)
        {
            if (accessibleStopsControl != null)
            {
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();


                // Update the Journey request location with the accessible location
                JourneyRequestHelper journeyRequestHelper = new JourneyRequestHelper();
                tdpJourneyRequest = journeyRequestHelper.UpdateTDPJourneyRequestForAccessiblePublicTransport(
                    tdpJourneyRequest, accessibleStopsControl.OriginLocation, accessibleStopsControl.DestinationLocation);

                // Commit journey request to session
                sessionHelper.UpdateSession(tdpJourneyRequest);

                // Persist the journey request details in the cookie.
                // This aids in recovery following session timeout, or when user 
                // returns to site after a period of time (new session)
                cookieHelper.UpdateJourneyRequestToCookie(tdpJourneyRequest);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates the location and updates the journey request's destination or origin (stored in session)
        /// </summary>
        /// <param name="location">New location</param>
        /// <param name="updateOrigin">true for origin, false for destination</param>
        /// <returns></returns>
        public bool ValidateAndUpdateTDPRequestOriginOrDestination(TDPLocation location, bool updateOrigin)
        {
            if (location != null)
            {
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();

                // Update the Journey request location with the accessible location
                JourneyRequestHelper journeyRequestHelper = new JourneyRequestHelper();
                tdpJourneyRequest = journeyRequestHelper.UpdateTDPJourneyRequestOriginOrDestination(
                    tdpJourneyRequest, location, updateOrigin);

                // Commit journey request to session
                sessionHelper.UpdateSession(tdpJourneyRequest);

                // Do not persist in cookie as this method is only used by the maps page and therefore
                // a journey request isn't being submitted 
                
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates the replan journey request hash exists and updates the two journey requests so they are linked 
        /// as an earlier - later chain
        /// </summary>
        /// <param name="journeyRequestHash"></param>
        /// <param name="isEarlier"></param>
        /// <param name="replanJourneyRequestHash"></param>
        /// <returns></returns>
        public bool ValidateAndUpdateTDPRequestEarlierLater(string journeyRequestHash, bool isEarlier, string replanJourneyRequestHash)
        {
            bool valid = false;

            if (!string.IsNullOrEmpty(journeyRequestHash) && !string.IsNullOrEmpty(replanJourneyRequestHash))
            {
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest(journeyRequestHash);
                ITDPJourneyRequest tdpJourneyRequestReplan = sessionHelper.GetTDPJourneyRequest(replanJourneyRequestHash);

                // Ensure both requests exist, otherwise don't update
                if (tdpJourneyRequest != null && tdpJourneyRequestReplan != null)
                {
                    // Update the Journey request location with the earlier or later journey request hash
                    JourneyRequestHelper journeyRequestHelper = new JourneyRequestHelper();
                    
                    tdpJourneyRequest = journeyRequestHelper.UpdateTDPJourneyRequestEarlierLater(
                        tdpJourneyRequest, isEarlier, replanJourneyRequestHash);

                    tdpJourneyRequestReplan = journeyRequestHelper.UpdateTDPJourneyRequestEarlierLater(
                        tdpJourneyRequestReplan, !isEarlier, journeyRequestHash);

                    // Commit journey request to session
                    sessionHelper.UpdateSession(tdpJourneyRequest);

                    valid = true;
                }
            }

            return valid;
        }

        #endregion

        #region Submit

        /// <summary>
        /// Submits the request. If successful, then sets the page to transfer to, otherwise the current
        /// page processing continues
        /// </summary>
        /// <param name="plannerMode"></param>
        public bool SubmitRequest(TDPJourneyPlannerMode plannerMode, ref List<TDPMessage> messages, TDPPage page)
        {
            bool submitValid = false;

            // Check locations/dates are valid and setup the journey request.
            // Any errors and the curret page will be displayed again, with errors shown
            if (ValidateAndBuildTDPRequest(plannerMode, false))
            {
                LocationHelper locationHelper = new LocationHelper();

                // Assume locations are accessible
                bool validAccessibleLocations = true;

                #region Validate accessible locations (if required)

                bool checkAccessibleLocations = Properties.Current["JourneyPlannerInput.CheckForGNATStation.Switch"].Parse(true);

                // Only check for accessible locations if the planner mode is public transport, and switch turned on
                if (plannerMode == TDPJourneyPlannerMode.PublicTransport && checkAccessibleLocations)
                {
                    // If origin/destination are venues and not accessible, then error
                    if (locationHelper.CheckAccessibleLocationForVenue(true) && locationHelper.CheckAccessibleLocationForVenue(false))
                    {
                        // Check accessible location for Origin
                        validAccessibleLocations = locationHelper.CheckAccessibleLocation(true);

                        // Check accessible location for Destination
                        bool validDest = locationHelper.CheckAccessibleLocation(false);
                        validAccessibleLocations = validAccessibleLocations && validDest;
                    }
                }

                #endregion

                #region Check for The Mall venue

                // Check for The Mall venue
                string mallNaptan = Properties.Current["JourneyPlannerInput.TheMallNaptan"];
                if (journeyInputControl != null)
                {
                    if (journeyInputControl.LocationFrom is TDPVenueLocation)
                    {
                        if (journeyInputControl.LocationFrom.Naptan.Contains(mallNaptan))
                        {
                            messages.Add(new TDPMessage("JourneyPlannerInput.MallMessage.Text", TDPMessageType.Error));
                            return false;
                        }
                    }

                    if (journeyInputControl.LocationTo is TDPVenueLocation)
                    {
                        if (journeyInputControl.LocationTo.Naptan.Contains(mallNaptan))
                        {
                            messages.Add(new TDPMessage("JourneyPlannerInput.MallMessage.Text", TDPMessageType.Error));
                            return false;
                        }
                    }
                }

                #endregion

                #region Submit the request

                JourneyPlannerHelper journeyPlannerHelper = new JourneyPlannerHelper();

                switch (plannerMode)
                {
                    case TDPJourneyPlannerMode.PublicTransport:
                        if (validAccessibleLocations)
                        {
                            // Attempt to submit the request and plan the journey
                            if (journeyPlannerHelper.SubmitRequest(plannerMode, true))
                            {
                                submitValid = true;
                            }
                        }
                        else
                        {
                            // Validate page details before moving on
                            if (journeyPlannerHelper.SubmitRequest(plannerMode, false))
                            {
                                // Transfer to further accessibility page
                                page.SetPageTransfer(PageId.AccessibilityOptions);

                                // Add the query string values to allow JourneyLocations page 
                                // to load the details for the correct request
                                page.AddQueryStringForPage(PageId.AccessibilityOptions);
                            }
                        }
                        break;

                    case TDPJourneyPlannerMode.Cycle:
                        if (journeyInputControl == null)
                        {
                            submitValid = false;
                        }
                        else
                        {
                            bool valid = ValidateAndUpdateTDPRequestForTDPPark(
                                journeyInputControl.SelectedCyclePark,
                                journeyInputControl.SelectedCycleParkDateTime,
                                journeyInputControl.SelectedCycleJourneyType,
                                journeyInputControl.PlannerMode);

                            if (valid && journeyPlannerHelper.SubmitRequest(plannerMode, true))
                            {
                                submitValid = true;
                            }
                        }
                        break;
                }

                #endregion
            }
            else
            {
                // Add any control specific messages
                if (journeyInputControl != null)
                {
                    List<string> validationMessages = journeyInputControl.ValidationMessages;

                    foreach (string message in validationMessages)
                    {
                        messages.Add(new TDPMessage(message, string.Empty, 0, 0, TDPMessageType.Error));
                    }
                }

                // Add default message if needed
                if (messages.Count == 0)
                {
                    messages.Add(new TDPMessage("JourneyPlannerInput.ValidationError.Text", TDPMessageType.Error));
                }
            }

            if (journeyInputControl != null)
            {
                // Persist planner mode if the Submit validation fails and this page is displayed again
                journeyInputControl.PlannerMode = plannerMode;
            }

            return submitValid;
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Checks the active journey options tab and builds accessible preferences
        /// </summary>
        /// <returns></returns>
        private TDPAccessiblePreferences GetAccessiblePreferences(TDPJourneyPlannerMode plannerMode)
        {
            TDPAccessiblePreferences accessiblePreferences = new TDPAccessiblePreferences();

            if (plannerMode == TDPJourneyPlannerMode.PublicTransport)
            {
                if (journeyInputControl != null && journeyInputControl.AccessibleOptions != null)
                {
                    accessiblePreferences.DoNotUseUnderground = journeyInputControl.AccessibleOptions.ExcludeUnderGround;
                    accessiblePreferences.RequireFewerInterchanges = journeyInputControl.AccessibleOptions.FewestChanges;
                    accessiblePreferences.RequireSpecialAssistance = journeyInputControl.AccessibleOptions.Assistance;
                    accessiblePreferences.RequireStepFreeAccess = journeyInputControl.AccessibleOptions.StepFree;

                    if (journeyInputControl.AccessibleOptions.StepFreeAndAssistance)
                    {
                        accessiblePreferences.RequireSpecialAssistance = true;
                        accessiblePreferences.RequireStepFreeAccess = true;
                    }
                }
            }

            return accessiblePreferences;
        }

        #endregion
    }
}
