// *********************************************** 
// NAME             : JourneyPlannerInputAdapter.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 04 Apr 2011
// DESCRIPTION  	: JourneyPlanner Input page adapter responsible for populating inptut controls, journey request in session,
//                    validating the controls
// ************************************************


using System;
using System.Collections.Generic;
using TDP.Common.LocationService;
using TDP.Common.Web;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.TDPWeb.Controls;
using TDP.UserPortal.SessionManager;

namespace TDP.UserPortal.TDPWeb.Adapters
{
    /// <summary>
    /// JourneyPlanner Input page adapter responsible for populating inptut controls, journey request in session,
    /// validating the controls and initiating a journey request
    /// </summary>
    public class JourneyPlannerInputAdapter
    {
        #region Private Fields

        private LocationControl locationFrom;
        private LocationControl locationTo;
        private EventDateControl eventControl;
        private JourneyOptionTabContainer journeyOptions;
        
        private SessionHelper sessionHelper;
        private CookieHelper cookieHelper;
        private LocationHelper locationHelper;

        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public JourneyPlannerInputAdapter()
        {
            sessionHelper = new SessionHelper();
            cookieHelper = new CookieHelper();
            locationHelper = new LocationHelper();
        }

        /// <summary>
        /// Constructor - Initialises the adapter with page controls necessary for journey
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="eventControl"></param>
        /// <param name="journeyOptions"></param>
        public JourneyPlannerInputAdapter(LocationControl locationFrom,
            LocationControl locationTo, EventDateControl eventControl,
            JourneyOptionTabContainer journeyOptions) : this()
        {
            this.locationFrom = locationFrom;
            this.locationTo = locationTo;
            this.eventControl = eventControl;
            this.journeyOptions = journeyOptions;
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

            // Reset inputs
            locationFrom.Initialise(null, null, false, false);
            locationTo.Initialise(null, null, true, false);

            locationFrom.Reset(TDPLocationType.Unknown);
            locationTo.Reset(TDPLocationType.Unknown);

            locationFrom.ResolveLocation = true;
            locationTo.ResolveLocation = true;

            eventControl.OutwardDateTime = DateTime.MinValue;
            eventControl.ReturnDateTime = DateTime.MinValue;
            eventControl.ForceUpdate = true;


            if (tdpJourneyRequest != null)
            {
                // Set locations,
                // Set the location search (this will either be null or populated from a postback when ambiguous)
                locationFrom.Initialise(tdpJourneyRequest.Origin, pageState.OriginSearch, false, isLandingPage);
                locationTo.Initialise(tdpJourneyRequest.Destination, pageState.DestinationSearch, true, isLandingPage);

                // If landing page, then do not want to resolve location as it would have already
                // been done (or if null/invalid then the control will say when Validate is called)
                locationFrom.ResolveLocation = !isLandingPage;
                locationTo.ResolveLocation = !isLandingPage;
                
                #region Ensure location inputs are in a valid input scenario

                // If landing page, then ensure location controls type of location is valid
                if (isLandingPage)
                {
                    // No longer done here, see below for logic
                }
                
                LocationInputMode locationInputMode = LocationInputMode.NoVenue;

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
                
                // Set location input mode
                // If venue locations are not available, then remove "at least one venue" check
                if (!locationHelper.VenueLocationsAvailable())
                {
                    locationInputMode = LocationInputMode.NoVenue;
                }

                switch (locationInputMode)
                {
                    case LocationInputMode.VenueToVenue:
                        // Retain existing location as venue only, giving preference to the desitnation input
                        if (locationTo.IsVenueOnly)
                        {
                            locationFrom.IsVenueOnly = false;
                            locationTo.IsVenueOnly = true;
                        }
                        else if (locationFrom.IsVenueOnly)
                        {
                            locationFrom.IsVenueOnly = true;
                            locationTo.IsVenueOnly = false;
                        }
                        break;
                    case LocationInputMode.FromVenue:
                        locationFrom.IsVenueOnly = true;
                        locationTo.IsVenueOnly = false;
                        break;
                    case LocationInputMode.ToVenue:
                        locationFrom.IsVenueOnly = false;
                        locationTo.IsVenueOnly = true;
                        break;
                    case LocationInputMode.NoVenue:
                    default:
                        locationFrom.IsVenueOnly = false;
                        locationTo.IsVenueOnly = false;
                        break;
                }

                #endregion

                // If landing page, then want to ensure the dates are validated and updated if necessary
                eventControl.ForceUpdate = true; // isLandingPage;
                eventControl.OutwardDateTime = tdpJourneyRequest.OutwardDateTime;
                eventControl.ReturnDateTime = tdpJourneyRequest.ReturnDateTime;

                eventControl.IsOutwardRequired = tdpJourneyRequest.IsOutwardRequired;
                eventControl.IsReturnRequired = tdpJourneyRequest.IsReturnRequired;

                // If outward journey not required, then hide the outward date control
                if (!tdpJourneyRequest.IsOutwardRequired)
                {
                    eventControl.ShowReturnDateOnly(true);
                }

                journeyOptions.PlannerMode = tdpJourneyRequest.PlannerMode;
                //journeyOptions.Modes = tdpJourneyRequest.Modes;

                // Populate options within the tabs
                if (tdpJourneyRequest.AccessiblePreferences != null)
                {
                    TDPAccessiblePreferences accessPref = tdpJourneyRequest.AccessiblePreferences;

                    journeyOptions.PublicJourneyTab.ExcludeUnderGround = accessPref.DoNotUseUnderground;
                    journeyOptions.PublicJourneyTab.FewerInterchanges = accessPref.RequireFewerInterchanges;

                    if (tdpJourneyRequest.AccessiblePreferences.RequireSpecialAssistance
                        && tdpJourneyRequest.AccessiblePreferences.RequireStepFreeAccess)
                    {
                        journeyOptions.PublicJourneyTab.StepFreeAndAssistance = true;
                    }
                    else if (tdpJourneyRequest.AccessiblePreferences.RequireSpecialAssistance)
                    {
                        journeyOptions.PublicJourneyTab.Assistance = accessPref.RequireSpecialAssistance;
                    }
                    else if (tdpJourneyRequest.AccessiblePreferences.RequireStepFreeAccess)
                    {
                        journeyOptions.PublicJourneyTab.StepFree = accessPref.RequireStepFreeAccess;
                    }
                }
            }
        }

        #region Validate and Build TDPJourneyRequest

        /// <summary>
        /// Validates the input page controls and populates a TDPJourneyRequest object using input page controls,
        /// and adds to the session
        /// </summary>
        /// <returns>True if the input page controls are in valid state</returns>
        public bool ValidateAndBuildTDPRequest(TDPJourneyPlannerMode plannerMode)
        {
            bool valid = false;

            if (locationFrom != null && locationTo != null && eventControl != null)
            {
                bool validFrom = locationFrom.Validate();
                bool validTo = locationTo.Validate();
                bool validDate = eventControl.Validate();

                valid = validFrom && validTo && validDate;

                // Locations and dates are valid, construct an TDPJourneyRequest
                if (valid)
                {
                    JourneyRequestHelper jrh = new JourneyRequestHelper();

                    // Check if this should be treated as a return only journey request,
                    // by the abscene of the outward required in the event date control
                    bool isReturnOnly = (!eventControl.IsOutwardRequired && eventControl.IsReturnRequired);

                    ITDPJourneyRequest tdpJourneyRequest = jrh.BuildTDPJourneyRequest(
                        locationFrom.Location,
                        locationTo.Location,
                        eventControl.OutwardDateTime,
                        eventControl.ReturnDateTime,
                        eventControl.IsOutwardRequired,
                        eventControl.IsReturnRequired,
                        isReturnOnly,
                        plannerMode,
                        GetAccessiblePreferences());

                    // Commit journey request to session
                    sessionHelper.UpdateSession(tdpJourneyRequest);

                    // Persist the journey request details in the cookie.
                    // This aids in recovery following session timeout, or when user 
                    // returns to site after a period of time (new session)
                    cookieHelper.UpdateJourneyRequestToCookie(tdpJourneyRequest);
                }
            }

            return valid;
        }

        /// <summary>
        /// Populates a new TDPJourneyRequest object using the supplied TDPJourneyRequest, 
        /// with the supplied parameters, and adds to the session
        /// </summary>
        /// <returns></returns>
        public bool ValidateAndBuildTDPRequestForReplan(ITDPJourneyRequest tdpJourneyRequest,
            bool replanOutwardRequired, bool replanReturnRequired,
            DateTime replanOutwardDateTime, DateTime replanReturnDateTime,
            bool replanOutwardArriveBefore, bool replanReturnArriveBefore,
            List<Journey> outwardJourneys, List<Journey> returnJourneys,
            bool retainOutwardJourneys, bool retainReturnJourneys,
            bool retainOutwardJourneysWhenNoResults, bool retainReturnJourneysWhenNoResults)
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
                    replanOutwardRequired, replanReturnRequired,
                    replanOutwardDateTime, replanReturnDateTime,
                    replanOutwardArriveBefore, replanReturnArriveBefore,
                    outwardJourneys, returnJourneys,
                    retainOutwardJourneys, retainReturnJourneys,
                    retainOutwardJourneysWhenNoResults, retainReturnJourneysWhenNoResults
                    );
                                
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
        /// Validates the venue for car/cycle parks and updates the journey request stored in session
        /// </summary>
        /// <param name="park">car or cycle park</param>
        public bool ValidateAndUpdateTDPRequestForTDPPark(TDPPark park, 
            DateTime outwardDateTime, DateTime returnDateTime, string cycleRouteType)
        {
            if (park != null)
            {
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();

                // Update the journey request location with the Park and datetimes 
                JourneyRequestHelper journeyRequestHelper = new JourneyRequestHelper();
                tdpJourneyRequest = journeyRequestHelper.UpdateTDPJourneyRequestVenue(
                    tdpJourneyRequest, park, outwardDateTime, returnDateTime);

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
        /// Validates the venue for river serivces and updates the journey request stored in session
        /// </summary>
        /// <param name="outwardStopEvent">Selected outward river serivce </param>
        /// <param name="outwardDateTime">Outward river service date and time</param>
        /// <param name="returnStopEvent">Selected return river serivce </param>
        /// <param name="returnDateTime">Return river service date and time</param>
        /// <returns></returns>
        public bool ValidateAndUpdateTDPRequestForTDPRiverServices(Journey outwardStopEventJourney, DateTime outwardDateTime, 
            Journey returnStopEventJourney, DateTime returnDateTime)
        {
            if ((outwardStopEventJourney != null) || (returnStopEventJourney != null))
            {
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();
                                
                // Update the journey request location with the river service details
                JourneyRequestHelper journeyRequestHelper = new JourneyRequestHelper();
                tdpJourneyRequest = journeyRequestHelper.UpdateTDPJourneyRequestRiverServices(tdpJourneyRequest,
                    outwardStopEventJourney, returnStopEventJourney, outwardDateTime, returnDateTime);
                
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
        /// Validates the GNAT location and updates the journey request stored in session
        /// </summary>
        /// <param name="selectedLocation"></param>
        /// <returns></returns>
        public bool ValidateAndUpdateTDPRequestForAccessiblePublicTransport(TDPGNATLocation gnatStop)
        {
            if (gnatStop != null)
            {
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();


                // Update the Journey request location with the accessible location
                JourneyRequestHelper journeyRequestHelper = new JourneyRequestHelper();

                // TODO - using dummy locations as method being called has changed
                TDPLocation origin = new TDPLocation();
                TDPLocation destination = new TDPLocation();
                tdpJourneyRequest = journeyRequestHelper.UpdateTDPJourneyRequestForAccessiblePublicTransport(
                    tdpJourneyRequest, origin, destination);
                
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

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Checks the active journey options tab and builds accessible preferences
        /// </summary>
        /// <returns></returns>
        private TDPAccessiblePreferences GetAccessiblePreferences()
        {
            TDPAccessiblePreferences accessiblePreferences = new TDPAccessiblePreferences();

            if (journeyOptions.ActiveTab is PublicJourneyOptionsTab)
            {
                accessiblePreferences.DoNotUseUnderground = journeyOptions.PublicJourneyTab.ExcludeUnderGround;
                accessiblePreferences.RequireFewerInterchanges = journeyOptions.PublicJourneyTab.FewerInterchanges;
                accessiblePreferences.RequireSpecialAssistance = journeyOptions.PublicJourneyTab.Assistance;
                accessiblePreferences.RequireStepFreeAccess = journeyOptions.PublicJourneyTab.StepFree;

                if (journeyOptions.PublicJourneyTab.StepFreeAndAssistance)
                {
                    accessiblePreferences.RequireSpecialAssistance = true;
                    accessiblePreferences.RequireStepFreeAccess = true;
                }
            }
            else if (journeyOptions.ActiveTab is BlueBadgeOptionsTab)
            {
                accessiblePreferences.BlueBadge = true;
            }

            return accessiblePreferences;
        }
        
        #endregion
    }
}
