// *********************************************** 
// NAME             : JourneyRequestHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 13 Apr 2011
// DESCRIPTION  	: JourneyRequestHelper class to provide helper methods for journey requests
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.JourneyControl;
using JC = TDP.UserPortal.JourneyControl;
using Logger = System.Diagnostics.Trace;
using LS = TDP.Common.LocationService;

namespace TDP.Common.Web
{
    /// <summary>
    /// JourneyRequestHelper class to provide helper methods for journey requests
    /// </summary>
    public class JourneyRequestHelper
    {
        #region Private Fields

        private TDPLocation originLocation;
        private TDPLocation destinationLocation;
        private DateTime outwardDateTime;
        private DateTime returnDateTime;
        private bool outwardArriveBy = false; // Outward journeys (as at 19/07/2013) always leave at (also see Replan method)
        private bool returnArriveBy = false; // Return journeys always leave at (also see Replan method)
        private bool outwardRequired;
        private bool returnRequired;
        private bool returnOnly;
        private TDPJourneyPlannerMode plannerMode;
        private List<TDPModeType> modes;
        private TDPAccessiblePreferences accessiblePreferences;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public JourneyRequestHelper()
        {
        }

        #endregion

        #region Public Methods

        #region Build new request

        /// <summary>
        /// Populates an TDPJourneyRequest object using parameters provided,
        /// overloaded to specify an outward and return arrive by flag
        /// </summary>
        public ITDPJourneyRequest BuildTDPJourneyRequest(TDPLocation originLocation,
            TDPLocation destinationLocation,
            DateTime outwardDateTime,
            DateTime returnDateTime,
            bool outwardArriveBy,
            bool returnArriveBy,
            bool outwardRequired,
            bool returnRequired,
            bool returnOnly,
            TDPJourneyPlannerMode plannerMode,
            List<TDPModeType> modes,
            TDPAccessiblePreferences accessiblePreferences)
        {
            this.originLocation = originLocation;
            this.destinationLocation = destinationLocation;
            this.outwardDateTime = outwardDateTime;
            this.returnDateTime = returnDateTime;
            this.outwardArriveBy = outwardArriveBy;
            this.returnArriveBy = returnArriveBy;
            this.outwardRequired = outwardRequired;
            this.returnRequired = returnRequired;
            this.returnOnly = returnOnly;   // Treat the origin location as the ReturnOrigin, and destination location as the ReturnDestination (assumes no outward journey is required)
            this.plannerMode = plannerMode;
            this.modes = modes;
            this.accessiblePreferences = accessiblePreferences;

            ITDPJourneyRequest tdpJourneyRequest = BuildTDPRequest();

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Populates an TDPJourneyRequest object using parameters provided
        /// </summary>
        public ITDPJourneyRequest BuildTDPJourneyRequest(TDPLocation originLocation,
            TDPLocation destinationLocation,
            DateTime outwardDateTime,
            DateTime returnDateTime,
            bool outwardRequired,
            bool returnRequired,
            bool returnOnly,
            TDPJourneyPlannerMode plannerMode,
            TDPAccessiblePreferences accessiblePreferences)
        {
            return this.BuildTDPJourneyRequest(
                originLocation,
                destinationLocation,
                outwardDateTime,
                returnDateTime,
                outwardArriveBy,
                returnArriveBy,
                outwardRequired,
                returnRequired,
                returnOnly,
                plannerMode,
                modes,
                accessiblePreferences);
        }

        /// <summary>
        /// Populates an TDPJourneyRequest object using parameters provided. 
        /// Each string parameter is parsed into the correct type. 
        /// If any parameter fails parsing, then an TDPException is thrown or a null object is returned
        /// </summary>
        public ITDPJourneyRequest BuildTDPJourneyRequest(
            string originId,
            string originType,
            string originName,
            string destinationId,
            string destinationType,
            string destinationName,
            DateTime outwardDateTime,
            DateTime returnDateTime,
            bool outwardArriveBy,
            bool returnArriveBy,
            bool outwardRequired,
            bool returnRequired,
            bool returnOnly,
            string accessibleOption,
            bool fewerInterchanges,
            string plannerMode,
            List<TDPModeType> modes)
        {
            try
            {
                LocationHelper locationHelper = new LocationHelper();
                LocationSearch search = new LocationSearch();

                TDPLocationType originLocationType = TDPLocationType.Unknown;
                TDPLocationType destinationLocationType = TDPLocationType.Unknown;
                                
                // Origin location
                originLocationType = (TDPLocationType)Enum.Parse(typeof(TDPLocationType), originType, true);
                this.originLocation = locationHelper.GetLocation(
                    new LocationSearch(originName, originId, originLocationType, true));

                // Destination location
                destinationLocationType = (TDPLocationType)Enum.Parse(typeof(TDPLocationType), destinationType, true);
                this.destinationLocation = locationHelper.GetLocation(
                    new LocationSearch(destinationName, destinationId, destinationLocationType, true));
                
                // Outward date - No need to validate here as the Date control, or the JourneyPlanRunner will check
                this.outwardDateTime = outwardDateTime;
                this.outwardArriveBy = outwardArriveBy;

                // Return date - No need to validate here as the Date control, or the JourneyPlanRunner will check
                this.returnDateTime = returnDateTime;
                this.returnArriveBy = returnArriveBy;

                // Outward/Return required
                this.outwardRequired = outwardRequired;
                this.returnRequired = returnRequired;
                this.returnOnly = returnOnly;   // Treat the origin location as the ReturnOrigin, and destination location as the ReturnDestination (assumes no outward journey is required)

                // Planner mode
                this.plannerMode = (TDPJourneyPlannerMode)Enum.Parse(typeof(TDPJourneyPlannerMode), plannerMode, true);

                // Modes
                this.modes = modes;
                
                // Accessible preferences
                this.accessiblePreferences = new TDPAccessiblePreferences();
                this.accessiblePreferences.PopulateAccessiblePreference(accessibleOption);
                this.accessiblePreferences.RequireFewerInterchanges = fewerInterchanges;
                
                // For Mobile, outward only journey From venue scenario
                if (outwardRequired && !returnRequired && !returnOnly)
                {
                    if ((originLocation != null && originLocation is TDPVenueLocation)
                        && (destinationLocation != null && !(destinationLocation is TDPVenueLocation)))
                    {
                        this.outwardArriveBy = false;
                    }
                }

                // Build the TDPJourneyRequest
                ITDPJourneyRequest tdpJourneyRequest = BuildTDPRequest();

                return tdpJourneyRequest;
            }
            catch (Exception ex)
            {
                throw new TDPException(
                    string.Format("Error building an TDPJourneyRequest from string values: {0}", ex.Message), 
                    false, TDPExceptionIdentifier.SWErrorBuildingJourneyRequestFromString, ex);
            }
        }

        #endregion

        #region Build replan request

        /// <summary>
        /// Populates an TDPJourneyRequest object using parameters provided
        /// </summary>
        public ITDPJourneyRequest BuildTDPJourneyRequestForReplan(ITDPJourneyRequest tdpJourneyRequest,
            bool replanOutwardRequired,
            bool replanReturnRequired,
            DateTime replanOutwardDateTime,
            DateTime replanReturnDateTime,
            bool replanOutwardArriveBefore,
            bool replanReturnArriveBefore,
            List<Journey> outwardJourneys, 
            List<Journey> returnJourneys,
            bool retainOutwardJourneys,
            bool retainReturnJourneys,
            bool retainOutwardJourneysWhenNoResults,
            bool retainReturnJourneysWhenNoResults)
        {
            this.originLocation = (TDPLocation)tdpJourneyRequest.Origin.Clone();
            this.destinationLocation = (TDPLocation)tdpJourneyRequest.Destination.Clone();
            this.outwardDateTime = tdpJourneyRequest.OutwardDateTime;
            this.returnDateTime = tdpJourneyRequest.ReturnDateTime;
            this.outwardArriveBy = tdpJourneyRequest.OutwardArriveBefore;
            this.returnArriveBy = tdpJourneyRequest.ReturnArriveBefore;
            this.outwardRequired = tdpJourneyRequest.IsOutwardRequired;
            this.returnRequired = tdpJourneyRequest.IsReturnRequired;
            this.returnOnly = tdpJourneyRequest.IsReturnOnly;
            this.plannerMode = tdpJourneyRequest.PlannerMode;
            this.modes = tdpJourneyRequest.Modes;
            this.accessiblePreferences = tdpJourneyRequest.AccessiblePreferences.Clone();

            // Create the replan request based on the original
            ITDPJourneyRequest tdpJourneyRequestReplan = BuildTDPRequest();

            // Add the replan values
            tdpJourneyRequestReplan.IsReplan = true;
            tdpJourneyRequestReplan.ReplanIsOutwardRequired = replanOutwardRequired;
            tdpJourneyRequestReplan.ReplanIsReturnRequired = replanReturnRequired;
            tdpJourneyRequestReplan.ReplanOutwardDateTime = replanOutwardDateTime;
            tdpJourneyRequestReplan.ReplanReturnDateTime = replanReturnDateTime;
            tdpJourneyRequestReplan.ReplanOutwardArriveBefore = replanOutwardArriveBefore;
            tdpJourneyRequestReplan.ReplanReturnArriveBefore = replanReturnArriveBefore;
            tdpJourneyRequestReplan.ReplanOutwardJourneys = outwardJourneys;
            tdpJourneyRequestReplan.ReplanReturnJourneys = returnJourneys;
            tdpJourneyRequestReplan.ReplanRetainOutwardJourneys = retainOutwardJourneys;
            tdpJourneyRequestReplan.ReplanRetainReturnJourneys = retainReturnJourneys;
            tdpJourneyRequestReplan.ReplanRetainOutwardJourneysWhenNoResults = retainOutwardJourneysWhenNoResults;
            tdpJourneyRequestReplan.ReplanRetainReturnJourneysWhenNoResults = retainReturnJourneysWhenNoResults;
            
            // Update the journey request hash because replan values have been added
            tdpJourneyRequestReplan.JourneyRequestHash = tdpJourneyRequestReplan.GetTDPHashCode().ToString();

            return tdpJourneyRequestReplan;
        }

        #endregion

        #region Update existing request

        /// <summary>
        /// Updates an TDPJourneyRequest object with the planner mode parameter provided
        /// </summary>
        /// <param name="tdpJourneyRequest">Request to update</param>
        public ITDPJourneyRequest UpdateTDPJourneyRequestPlannerMode(ITDPJourneyRequest tdpJourneyRequest, TDPJourneyPlannerMode plannerMode)
        {
            if (tdpJourneyRequest != null)
            {
                if (tdpJourneyRequest.PlannerMode != plannerMode)
                {
                    tdpJourneyRequest.PlannerMode = plannerMode;
                    tdpJourneyRequest.Modes = PopulateModes(plannerMode, tdpJourneyRequest.AccessiblePreferences);
                }

                // Update the journey request hash because the planner mode has changed.
                tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            }

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Updates an TDPJourneyRequest object with the accessible options provided
        /// </summary>
        /// <param name="tdpJourneyRequest">Request to update</param>
        public ITDPJourneyRequest UpdateTDPJourneyRequestAccessiblePreferences(ITDPJourneyRequest tdpJourneyRequest, TDPAccessiblePreferences accessiblePreferences)
        {
            if (tdpJourneyRequest != null)
            {
                // Update accessible preference in the existing request
                tdpJourneyRequest.AccessiblePreferences = accessiblePreferences;

                // And update the request properties which are dependent on the accessible preferences
                UpdateAccessibilePreferences((TDPJourneyRequest)tdpJourneyRequest, accessiblePreferences);

                UpdateAccessiblePublicParameters((TDPJourneyRequest)tdpJourneyRequest);

                // Update the journey request hash because the planner mode has changed.
                tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            }

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Updates an TDPJourneyRequest object using the Additional Venue parameters provided.
        /// The destination location is updated (if a venue), otherwise the origin (if a venue)
        /// </summary>
        /// <param name="tdpJourneyRequest">Request to update</param>
        public ITDPJourneyRequest UpdateTDPJourneyRequestVenue(ITDPJourneyRequest tdpJourneyRequest,
            TDPPark tdpPark, DateTime outwardDateTime, DateTime returnDateTime)
        {
            TDPVenueLocation venueLocation = null;
            bool isOriginLocation = false;

            if (tdpJourneyRequest.Destination is TDPVenueLocation)
            {
                venueLocation = tdpJourneyRequest.Destination as TDPVenueLocation;
                isOriginLocation = false;
            }
            else if (tdpJourneyRequest.Origin is TDPVenueLocation)
            {
                venueLocation = tdpJourneyRequest.Origin as TDPVenueLocation;
                isOriginLocation = true;
            }
            
            if (venueLocation != null)
            {
                // Set the park Id for the journey planner to use
                venueLocation.SelectedTDPParkID = tdpPark.ID;

                // Set the datetimes to use instead of the input page journey request datetimes
                venueLocation.SelectedOutwardDateTime = outwardDateTime;
                venueLocation.SelectedReturnDateTime = returnDateTime;

                // Clear any previous venue accessible details
                venueLocation.AccessibleNaptans = new List<string>();
                venueLocation.SelectedName = string.Empty;

                // Update request
                if (isOriginLocation)
                {
                    tdpJourneyRequest.Origin = venueLocation;
                }
                else
                {
                    tdpJourneyRequest.Destination = venueLocation;
                }

                // Update the journey request hash because the location has changed.
                tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            }

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Updates an TDPJourneyRequest object to use the specified Cycle route type penalty algorithm
        /// </summary>
        /// <param name="tdpJourneyRequest">Request to update</param>
        /// <param name="penaltyFunctionAlgortihm">Penalty function algorithm</param>
        /// <returns></returns>
        public ITDPJourneyRequest UpdateTDPJourneyRequestCycle(ITDPJourneyRequest tdpJourneyRequest,
            string penaltyFunctionAlgortihm)
        {
            if (!string.IsNullOrEmpty(penaltyFunctionAlgortihm))
            {
                // Set the correct cycle algorithm to use
                tdpJourneyRequest.CycleAlgorithm = penaltyFunctionAlgortihm;
                tdpJourneyRequest.PenaltyFunction = GetCycleAlgorithm(penaltyFunctionAlgortihm);

                // Clear any previous accessible preferences
                tdpJourneyRequest.AccessiblePreferences = new TDPAccessiblePreferences();

                // Update the journey request hash because the penalty function has changed.
                tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            }

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Updates an TDPJourneyRequest object using the Additional Venue parameters provided for river services
        /// </summary>
        public ITDPJourneyRequest UpdateTDPJourneyRequestRiverServices(ITDPJourneyRequest tdpJourneyRequest,
            Journey outwardStopEventJourney, Journey returnStopEventJourney, DateTime outwardDateTime, DateTime returnDateTime)
        {
            TDPVenueLocation venueLocation = null;
            TDPVenueLocation returnVenueLocation = null;

            bool updateRequest = false;

            #region Outward venue location

            if (outwardStopEventJourney != null)
            {
                venueLocation = tdpJourneyRequest.Destination as TDPVenueLocation;
                
                if (venueLocation != null)
                {
                    if (outwardStopEventJourney.JourneyLegs.Count > 0)
                    {
                        // Set the Pier NaPTAN and name for the journey planner to use
                        JourneyCallingPoint venuePier = outwardStopEventJourney.JourneyLegs[0].LegStart;
                        if (venuePier != null && venuePier.Location != null)
                        {
                            venueLocation.SelectedPierNaptan = venuePier.Location.Naptan.FirstOrDefault();
                            venueLocation.SelectedName = venuePier.Location.DisplayName;
                            venueLocation.SelectedGridReference = venuePier.Location.GridRef;
                        }
                    }

                    // Set the datetimes to use instead of the input page journey request datetimes
                    venueLocation.SelectedOutwardDateTime = outwardDateTime;
                    venueLocation.SelectedReturnDateTime = returnDateTime;

                    // Clear any previous venue accessible details
                    venueLocation.AccessibleNaptans =  new List<string>();
                    
                    // Update request
                    tdpJourneyRequest.Destination = venueLocation;
                    
                    // Add the stop event journey, it's legs will be appended to the journey planned
                    tdpJourneyRequest.OutwardJourneyPart = outwardStopEventJourney;

                    updateRequest = true;
                }
            }

            #endregion

            #region Return venue location

            if (returnStopEventJourney != null)
            {
                returnVenueLocation = tdpJourneyRequest.ReturnOrigin as TDPVenueLocation;
                
                if (returnVenueLocation != null)
                {
                    if (returnStopEventJourney.JourneyLegs.Count > 0)
                    {
                        // Set the Pier NaPTAN for the journey planner to use
                        JourneyCallingPoint venuePier = returnStopEventJourney.JourneyLegs[0].LegEnd;
                        if (venuePier != null && venuePier.Location != null)
                        {
                            returnVenueLocation.SelectedPierNaptan = venuePier.Location.Naptan.FirstOrDefault();
                            returnVenueLocation.SelectedName = venuePier.Location.DisplayName;
                            returnVenueLocation.SelectedGridReference = venuePier.Location.GridRef;
                        }
                    }

                    // Set the datetimes to use instead of the input page journey request datetimes
                    returnVenueLocation.SelectedOutwardDateTime = outwardDateTime;
                    returnVenueLocation.SelectedReturnDateTime = returnDateTime;

                    // Clear any previous venue accessible details
                    returnVenueLocation.AccessibleNaptans = new List<string>();

                    // Update request
                    tdpJourneyRequest.ReturnOrigin = returnVenueLocation;
                    
                    // Add the stop event journey, it's legs will be appended to the journey planned
                    tdpJourneyRequest.ReturnJourneyPart = returnStopEventJourney;

                    updateRequest = true;
                }
            }

            #endregion

            if (updateRequest)
            {
                IPropertyProvider pp = Properties.Current;

                // Update the number of PT journeys required
                tdpJourneyRequest.Sequence = pp[JC.Keys.JourneyRequest_Sequence_RiverServicePlannerMode].Parse(3);

                // Travel demand should be turned off for this journey request to pier
                tdpJourneyRequest.TravelDemandPlanOutward = pp[JC.Keys.JourneyRequest_TravelDemandPlanOff];
                tdpJourneyRequest.TravelDemandPlanReturn = pp[JC.Keys.JourneyRequest_TravelDemandPlanOff];

                // Clear any previous accessible preferences
                tdpJourneyRequest.AccessiblePreferences = new TDPAccessiblePreferences();

                // Update the journey request hash because the location has changed.
                tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            }
            
            return tdpJourneyRequest;
        }


        /// <summary>
        /// Updates an TDPJourneyRequest object origin location using the user selected accessible GNAT stop location
        /// </summary>
        /// <param name="tdpJourneyRequest"></param>
        /// <param name="gnatStop"></param>
        /// <returns></returns>
        public ITDPJourneyRequest UpdateTDPJourneyRequestForAccessiblePublicTransport(ITDPJourneyRequest tdpJourneyRequest, TDPLocation origin, TDPLocation destination)
        {
            if (origin != null && destination != null)
            {
                // Incoming locations are fully populated 
                // Update the locations 
                tdpJourneyRequest.Origin = origin;
                tdpJourneyRequest.Destination = destination;
                
                // Update the dont force coach rule because origin/destination has changed
                UpdateDontForceCoach((TDPJourneyRequest)tdpJourneyRequest);

                // Update the journey request hash because the origin/destination has changed.
                tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            }

            return tdpJourneyRequest;
        }


        /// <summary>
        /// Updates an TDPJourneyRequest object origin or destination location 
        /// </summary>
        /// <param name="tdpJourneyRequest"></param>
        /// <param name="location">the new location</param>
        /// <param name="updateOrigin">true for origin, false for destination</param>
        /// <returns></returns>
        public ITDPJourneyRequest UpdateTDPJourneyRequestOriginOrDestination(ITDPJourneyRequest tdpJourneyRequest, TDPLocation location, bool updateOrigin)
        {
            if (location != null)
            {
                if (updateOrigin)
                {
                    tdpJourneyRequest.Origin = location;
                }
                else
                {
                    tdpJourneyRequest.Destination = location;
                }

                // Update the journey request hash because the origin/destination has changed.
                tdpJourneyRequest.JourneyRequestHash = tdpJourneyRequest.GetTDPHashCode().ToString();
            }

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Updates an TDPJourneyRequest object with the replan journey request hash
        /// </summary>
        /// <param name="tdpJourneyRequest"></param>
        /// <param name="isEarlier"></param>
        /// <param name="replanJourneyRequestHash"></param>
        /// <returns></returns>
        public ITDPJourneyRequest UpdateTDPJourneyRequestEarlierLater(ITDPJourneyRequest tdpJourneyRequest, bool isEarlier, string replanJourneyRequestHash)
        {
            if (tdpJourneyRequest != null)
            {
                // Update the replan journey request hash for this request
                if (isEarlier)
                {
                    tdpJourneyRequest.ReplanJourneyRequestHashEarlier = replanJourneyRequestHash;
                }
                else
                {
                    tdpJourneyRequest.ReplanJourneyRequestHashLater = replanJourneyRequestHash;
                }

                // Do not update the journey request hash code as the replan journey request hash
                // has no bearing on this journey request parameters, it is only a pointer to the 
                // replan journey request
            }

            return tdpJourneyRequest;
        }
        
        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Constructs an journey request
        /// </summary>
        private ITDPJourneyRequest BuildTDPRequest()
        {
            TDPJourneyRequest request = new TDPJourneyRequest();

            #region User entered parameters

            // Locations
            request.Origin = originLocation;
            request.Destination = destinationLocation;

            // Ensures the hash code is different when the only difference is origin and destination
            // has been swapped around
            request.LocationInputMode = GetLocationInputMode();

            // Date/times
            request.OutwardDateTime = outwardDateTime;
            request.OutwardArriveBefore = outwardArriveBy;
            
            request.ReturnDateTime = returnDateTime;
            request.ReturnArriveBefore = returnArriveBy;

            request.IsOutwardRequired = outwardRequired;
            request.IsReturnRequired = returnRequired;
            request.IsReturnOnly = returnOnly; // Treat the origin location as the ReturnOrigin, and destination location as the ReturnDestination (assumes no outward journey is required)
                        
            // Modes
            request.PlannerMode = plannerMode;
            if (modes != null && modes.Count > 0)
                request.Modes = modes;
            else 
                request.Modes = PopulateModes(plannerMode, accessiblePreferences);


            #region Accessible preferences

            UpdateAccessibilePreferences(request, accessiblePreferences);

            #endregion

            #region Dont force coach

            UpdateDontForceCoach(request);

            #endregion
            
            #endregion

            #region Common parameters from properties

            // Populate request parameters from the properties service.
            // Populate values for all journey request types regardless of requested type. 
            // The journey planner managers will use only those values it needs

            IPropertyProvider pp = Properties.Current;

            #region Public

            // Public specific
            request.PublicAlgorithm = GetPublicAlgorithm(pp[JC.Keys.JourneyRequest_AlgorithmPublic]);

            request.Sequence = pp[JC.Keys.JourneyRequest_Sequence].Parse(3);
            request.InterchangeSpeed = pp[JC.Keys.JourneyRequest_InterchangeSpeed].Parse(0);
            request.WalkingSpeed = pp[JC.Keys.JourneyRequest_WalkingSpeed].Parse(80);
            request.MaxWalkingTime = pp[JC.Keys.JourneyRequest_MaxWalkingTime].Parse(30);
            request.RoutingGuideInfluenced = pp[JC.Keys.JourneyRequest_RoutingGuideInfluenced].Parse(false);
            request.RoutingGuideCompliantJourneysOnly = pp[JC.Keys.JourneyRequest_RoutingGuideCompliantJourneysOnly].Parse(false);
            request.RouteCodes = pp[JC.Keys.JourneyRequest_RouteCodes];
            
            // TDM rules
            if (pp[JC.Keys.JourneyRequest_TravelDemandPlanSwitch].Parse(true))
            {
                // Handle venue to non-venue for an outward journey request
                if ((request.IsOutwardRequired && !request.IsReturnRequired && !request.IsReturnOnly)
                    && ((request.Origin != null) && (request.Origin is TDPVenueLocation)
                         && (request.Destination != null) && !(request.Destination is TDPVenueLocation))
                   )
                {
                    // Then this a leaving venue, so use TDM for return journey
                    request.TravelDemandPlanOutward = pp[JC.Keys.JourneyRequest_TravelDemandPlanReturn];
                    request.TravelDemandPlanReturn = pp[JC.Keys.JourneyRequest_TravelDemandPlanReturn];
                }
                else
                {
                    request.TravelDemandPlanOutward = pp[JC.Keys.JourneyRequest_TravelDemandPlanOutward];
                    request.TravelDemandPlanReturn = pp[JC.Keys.JourneyRequest_TravelDemandPlanReturn];
                }
            }
            else
            {
                request.TravelDemandPlanOutward = pp[JC.Keys.JourneyRequest_TravelDemandPlanOff];
                request.TravelDemandPlanReturn = pp[JC.Keys.JourneyRequest_TravelDemandPlanOff];
            }

            // Override for accessible journeys
            UpdateAccessiblePublicParameters(request);

            #endregion

            #region Car

            // Car specific
            request.PrivateAlgorithm = GetPrivateAlgorithm(pp[JC.Keys.JourneyRequest_AlgorithmPrivate]);

            request.AvoidMotorways = pp[JC.Keys.JourneyRequest_AvoidMotorways].Parse(false);
            request.AvoidFerries = pp[JC.Keys.JourneyRequest_AvoidFerries].Parse(false);
            request.AvoidTolls = pp[JC.Keys.JourneyRequest_AvoidTolls].Parse(false);
            request.AvoidRoads = new List<string>();
            request.IncludeRoads = new List<string>();
            request.DrivingSpeed = pp[JC.Keys.JourneyRequest_DrivingSpeed].Parse(112);
            request.DoNotUseMotorways = pp[JC.Keys.JourneyRequest_DoNotUseMotorways].Parse(false);
            request.FuelConsumption = pp[JC.Keys.JourneyRequest_FuelConsumption];
            request.FuelPrice = pp[JC.Keys.JourneyRequest_FuelPrice];

            #endregion

            #region Cycle

            // Cycle specific

            #region Penalty function

            request.CycleAlgorithm = string.Empty;
            request.PenaltyFunction = GetCycleAlgorithm(string.Empty);

            #endregion

            #region User preferences

            // The ID of each user preference must match the IDs specified in the cycle planner configuration file.
            List<TDPUserPreference> userPreferences = new List<TDPUserPreference>();

            TDPUserPreference tdpUserPreference = new TDPUserPreference();
            
            // A property that denotes the size of the array of user preferences expected by the Atkins CTP
            int numOfProperties = Convert.ToInt32(pp[JC.Keys.JourneyRequest_UserPreferences_Count]);

            // Build the actual array of user preferences from properties
            // these are used in the request sent to the Atkins CTP.
            for (int i = 0; i < numOfProperties; i++)
            {
                // Override any preferences by User entered/chosen values
                switch (i)
                {
                    //case 5: // Max Speed
                    //    break;
                    //case 6:  // Avoid Time Based Restrictions
                    //    break;
                    //case 12: // Avoid Steep Climbs
                    //    break;
                    //case 13: // Avoid Unlit Roads
                    //    break;
                    //case 14: // Avoid Walking your bike
                    //    break;
                    default:
                        tdpUserPreference = new TDPUserPreference(i.ToString(),
                            pp[string.Format(JC.Keys.JourneyRequest_UserPreferences_Index, i.ToString())]);
                        break;
                }
                userPreferences.Add(tdpUserPreference);
            }

            request.UserPreferences = userPreferences;

            #endregion

            #endregion

            #endregion
            
            // All request values have been set, now update the journey request hash.
            // This determines the uniqueness of this journey request for this users session
            request.JourneyRequestHash = request.GetTDPHashCode().ToString();

            return request;
        }

        /// <summary>
        /// Used the TDPJourneyPlannerMode to return an ModeTypes array corresponding to that mode 
        /// </summary>
        public static List<TDPModeType> PopulateModes(TDPJourneyPlannerMode plannerMode, TDPAccessiblePreferences accessiblePreferences)
        {
            List<TDPModeType> modes = new List<TDPModeType>();

            switch (plannerMode)
            {
                case TDPJourneyPlannerMode.Cycle: // Cycle only journey
                    modes.Add(TDPModeType.Cycle);
                    break;
                case TDPJourneyPlannerMode.BlueBadge: // Car only journey
                    modes.Add(TDPModeType.Car);
                    break;
                case TDPJourneyPlannerMode.ParkAndRide: // Car only journey
                    modes.Add(TDPModeType.Car);
                    break;
                case TDPJourneyPlannerMode.RiverServices: // River PT journey
                    modes.Add(TDPModeType.Air);
                    modes.Add(TDPModeType.Bus);
                    modes.Add(TDPModeType.Coach);
                    modes.Add(TDPModeType.Ferry);
                    modes.Add(TDPModeType.Metro);
                    modes.Add(TDPModeType.Rail);
                    modes.Add(TDPModeType.Telecabine);
                    modes.Add(TDPModeType.Tram);
                    modes.Add(TDPModeType.Underground);
                    break;
                case TDPJourneyPlannerMode.PublicTransport: // Public Transport journey
                default: 
                    modes.Add(TDPModeType.Air);
                    modes.Add(TDPModeType.Bus);
                    modes.Add(TDPModeType.Coach);
                    modes.Add(TDPModeType.Ferry);
                    modes.Add(TDPModeType.Metro);
                    modes.Add(TDPModeType.Rail);
                    modes.Add(TDPModeType.Telecabine);
                    modes.Add(TDPModeType.Tram);

                    // Do not include underground if accessible preference underground flag set
                    if ((accessiblePreferences == null) || (!accessiblePreferences.DoNotUseUnderground))
                    {
                        modes.Add(TDPModeType.Underground);
                    }
                    break;
            }

            return modes;
        }

        /// <summary>
        /// Converts a string into a TDPPublicAlgorithmType. If unable to parse, Default is returned 
        /// and a warning logged
        /// </summary>
        private TDPPublicAlgorithmType GetPublicAlgorithm(string algorithm)
        {
            TDPPublicAlgorithmType algorithmType = TDPPublicAlgorithmType.Default;
            try
            {
                algorithmType = (TDPPublicAlgorithmType)Enum.Parse(typeof(TDPPublicAlgorithmType), algorithm, true);
            }
            catch
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning,
                    string.Format("Failed to parse algorithm string[{0}] into an TDPPublicAlgorithmType, check property[{1}] contains a valid value for this algorithm type.",
                                   algorithm,
                                   JC.Keys.JourneyRequest_AlgorithmPublic)));
            }

            return algorithmType;
        }

        /// <summary>
        /// Converts a string into a TDPPrivateAlgorithmType. If unable to parse, Fastest is returned 
        /// and a warning logged
        /// </summary>
        private TDPPrivateAlgorithmType GetPrivateAlgorithm(string algorithm)
        {
            TDPPrivateAlgorithmType algorithmType = TDPPrivateAlgorithmType.Fastest;
            try
            {
                algorithmType = (TDPPrivateAlgorithmType)Enum.Parse(typeof(TDPPrivateAlgorithmType), algorithm, true);
            }
            catch
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning,
                    string.Format("Failed to parse algorithm string[{0}] into an TDPPrivateAlgorithmType, check property[{1}] contains a valid value for this algorithm type.",
                                   algorithm,
                                   JC.Keys.JourneyRequest_AlgorithmPrivate)));
            }

            return algorithmType;
        }

        /// <summary>
        /// Uses the provided cycle penalty function algorithm to build up a penalty function algorithm
        /// to use for cycle planning. Values are read from properties for the specified algorithm
        /// </summary>
        /// <param name="algorithm">Algorithm name corresponding to properties value, empty will use default algorithm</param>
        /// <returns></returns>
        private string GetCycleAlgorithm(string algorithm)
        {
            IPropertyProvider pp = Properties.Current;

            // penalty function must be formatted as 
            // "Call <location of penalty function assembly file>,<penalty function type name>"
            // e.g. "Call C:\CyclePlannerService\Services\RoadInterfaceHostingService\atk.cp.PenaltyFunctions.dll,
            // AtkinsGlobal.JourneyPlanning.PenaltyFunctions.Fastest"

            string algorithmToUse = algorithm;

            // Construct penalty function using the properties
            if (string.IsNullOrEmpty(algorithmToUse))
            {
                algorithmToUse = pp[JC.Keys.JourneyRequest_PenaltyFunction_Algorithm];
            }

            string dllPath = pp[string.Format(JC.Keys.JourneyRequest_PenaltyFunction_DLLPath, algorithmToUse)];
            string dll = pp[string.Format(JC.Keys.JourneyRequest_PenaltyFunction_DLL, algorithmToUse)];
            string prefix = pp[string.Format(JC.Keys.JourneyRequest_PenaltyFunction_Prefix, algorithmToUse)];
            string suffix = pp[string.Format(JC.Keys.JourneyRequest_PenaltyFunction_Suffix, algorithmToUse)];

            #region Validate

            // Validate penalty function values
            if (string.IsNullOrEmpty(dllPath) ||
                string.IsNullOrEmpty(dll) ||
                string.IsNullOrEmpty(prefix) ||
                string.IsNullOrEmpty(suffix))
            {
                throw new TDPException(
                    string.Format("Cycle planner penalty function property values for algorithm[{0}] were missing or invalid, check properties[{1}, {2}, {3}, and {4}] are available.",
                        algorithmToUse,
                        string.Format(JC.Keys.JourneyRequest_PenaltyFunction_DLLPath, algorithmToUse),
                        string.Format(JC.Keys.JourneyRequest_PenaltyFunction_DLL, algorithmToUse),
                        string.Format(JC.Keys.JourneyRequest_PenaltyFunction_Prefix, algorithmToUse),
                        string.Format(JC.Keys.JourneyRequest_PenaltyFunction_Suffix, algorithmToUse)),
                    false,
                    TDPExceptionIdentifier.PSMissingProperty);
            }

            if (!dllPath.EndsWith("\\"))
            {
                dllPath = dllPath + "\\";
            }

            if (!prefix.EndsWith("."))
            {
                prefix = prefix + ".";
            }

            #endregion

            string penaltyFunction = string.Format("Call {0}{1}, {2}{3}", dllPath, dll, prefix, suffix);

            return penaltyFunction;
        }

        /// <summary>
        /// Returns the current LocationInputMode based on the state of the From and To location
        /// </summary>
        /// <returns></returns>
        private string GetLocationInputMode()
        {
            if ((originLocation != null) && (destinationLocation != null))
            {
                LocationInputMode locationInputMode = LocationInputMode.NoVenue;

                if (originLocation.TypeOfLocation == TDPLocationType.Venue
                    && destinationLocation.TypeOfLocation == TDPLocationType.Venue)
                {
                    locationInputMode = LocationInputMode.VenueToVenue;
                }
                else if (originLocation.TypeOfLocation == TDPLocationType.Venue
                        && destinationLocation.TypeOfLocation != TDPLocationType.Venue)
                {
                    locationInputMode = LocationInputMode.FromVenue;
                }
                else if (originLocation.TypeOfLocation != TDPLocationType.Venue
                        && destinationLocation.TypeOfLocation == TDPLocationType.Venue)
                {
                    locationInputMode = LocationInputMode.ToVenue;
                }

                return locationInputMode.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Updates the accessible options based on the accessible preferences provided
        /// </summary>
        private void UpdateAccessibilePreferences(TDPJourneyRequest request, TDPAccessiblePreferences accessiblePreferences)
        {
            // Accessible preferences
            request.AccessiblePreferences = accessiblePreferences;
            request.FilteringStrict = accessiblePreferences.Accessible;

            // Set the venue accessible naptans
            if (accessiblePreferences.Accessible)
            {
                TDPVenueLocation originVenueLocation = null;
                TDPVenueLocation destinationVenueLocation = null;

                if (request.Origin != null && request.Origin is TDPVenueLocation)
                {
                    originVenueLocation = request.Origin as TDPVenueLocation;
                }

                if (request.Destination != null && request.Destination is TDPVenueLocation)
                {
                    destinationVenueLocation = request.Destination as TDPVenueLocation;
                }

                // Set the accessible naptans
                UpdateAccessibleLocations(originVenueLocation, destinationVenueLocation);

                // Assign updated locations back to request
                if (originVenueLocation != null)
                {
                    request.Origin = originVenueLocation;
                }

                if (destinationVenueLocation != null)
                {
                    request.Destination = destinationVenueLocation;
                }
            }
        }

        /// <summary>
        /// Updates the accessible naptans in the venue location for the naptans specified, valid for a datetime. List can be empty
        /// </summary>
        /// <returns></returns>
        private void UpdateAccessibleLocations(TDPVenueLocation originVenueLocation, TDPVenueLocation destinationVenueLocation)
        {
            LS.LocationService locationService = TDPServiceDiscovery.Current.Get<LS.LocationService>(ServiceDiscoveryKey.LocationService);

            #region Origin location

            // Origin location (in case it is a venue to venue accessible journey
            if (originVenueLocation != null)
            {
                List<TDPVenueAccess> venueAccessList = locationService.GetTDPVenueAccessData(originVenueLocation.Naptan, outwardDateTime);

                List<string> accessibleNaptans = new List<string>();
                string accessibleName = string.Empty;

                if ((venueAccessList != null) && (venueAccessList.Count > 0))
                {
                    foreach (TDPVenueAccess va in venueAccessList)
                    {
                        if (va.Stations != null)
                        {
                            foreach (TDPVenueAccessStation vas in va.Stations)
                            {
                                if (!accessibleNaptans.Contains(vas.StationNaPTAN))
                                {
                                    // Set station name (use first one found)
                                    if (string.IsNullOrEmpty(accessibleName))
                                    {
                                        accessibleName = vas.StationName;
                                    }

                                    // Add to the list of station accessible naptans to use
                                    accessibleNaptans.Add(vas.StationNaPTAN);
                                }
                            }
                        }
                    }
                }

                originVenueLocation.AccessibleNaptans = accessibleNaptans;
                originVenueLocation.SelectedName = accessibleName;
            }

            #endregion

            #region Destination location

            if (destinationVenueLocation != null)
            {
                List<TDPVenueAccess> venueAccessList = locationService.GetTDPVenueAccessData(destinationVenueLocation.Naptan, outwardDateTime);

                List<string> accessibleNaptans = new List<string>();
                string accessibleName = string.Empty;

                if ((venueAccessList != null) && (venueAccessList.Count > 0))
                {
                    foreach (TDPVenueAccess va in venueAccessList)
                    {
                        if (va.Stations != null)
                        {
                            foreach (TDPVenueAccessStation vas in va.Stations)
                            {
                                if (!accessibleNaptans.Contains(vas.StationNaPTAN))
                                {
                                    // Set station name (use first one found)
                                    if (string.IsNullOrEmpty(accessibleName))
                                    {
                                        accessibleName = vas.StationName;
                                    }
                                    
                                    // Add to the list of station accessible naptans to use
                                    accessibleNaptans.Add(vas.StationNaPTAN);
                                }
                            }
                        }
                    }

                    // Adjust the date time to take into account the transfer time
                    if ((outwardDateTime != DateTime.MinValue) && (outwardDateTime != DateTime.MaxValue))
                    {
                        destinationVenueLocation.SelectedOutwardDateTime = outwardDateTime.Subtract(venueAccessList[0].AccessToVenueDuration);
                    }
                    if ((returnDateTime != DateTime.MinValue) && (returnDateTime != DateTime.MaxValue))
                    {
                        destinationVenueLocation.SelectedReturnDateTime = returnDateTime.Add(venueAccessList[0].AccessToVenueDuration);
                    }
                }

                destinationVenueLocation.AccessibleNaptans = accessibleNaptans;
                destinationVenueLocation.SelectedName = accessibleName;
            }

            #endregion
        }

        /// <summary>
        /// Updates the public journey planning parameters based on the accessible preferences
        /// </summary>
        /// <param name="request"></param>
        private void UpdateAccessiblePublicParameters(TDPJourneyRequest request)
        {
            IPropertyProvider pp = Properties.Current;

            // Only update is accessible journey required
            if (request.AccessiblePreferences.Accessible)
            {
                // Accessible request (CJP flag to perform single region journey planning)
                request.OlympicRequest = pp[JC.Keys.JourneyRequest_OlympicRequest].Parse(true);

                // Remove awkward overnight journeys flag
                request.RemoveAwkwardOvernight = pp[JC.Keys.JourneyRequest_RemoveAwkwardOvernight].Parse(false);
                
                // Walk speed, distance
                if (request.AccessiblePreferences.RequireSpecialAssistance && request.AccessiblePreferences.RequireStepFreeAccess)
                {
                    request.WalkingSpeed = pp[JC.Keys.JourneyRequest_WalkingSpeed_StepFreeAssistance].Parse(80);
                    request.MaxWalkingDistance = pp[JC.Keys.JourneyRequest_MaxWalkingDistance_StepFreeAssistance].Parse(3000);
                }
                else if (request.AccessiblePreferences.RequireSpecialAssistance)
                {
                    request.WalkingSpeed = pp[JC.Keys.JourneyRequest_WalkingSpeed_Assistance].Parse(80);
                    request.MaxWalkingDistance = pp[JC.Keys.JourneyRequest_MaxWalkingDistance_Assistance].Parse(3000);
                }
                else if (request.AccessiblePreferences.RequireStepFreeAccess)
                {
                    request.WalkingSpeed = pp[JC.Keys.JourneyRequest_WalkingSpeed_StepFree].Parse(80);
                    request.MaxWalkingDistance = pp[JC.Keys.JourneyRequest_MaxWalkingDistance_StepFree].Parse(3000);
                }

                // Override algorithm
                if (request.AccessiblePreferences.RequireFewerInterchanges)
                {
                    request.PublicAlgorithm = GetPublicAlgorithm(pp[JC.Keys.JourneyRequest_AlgorithmPublic_MinChanges]);
                }
                
                // TDM rules
                if (pp[JC.Keys.JourneyRequest_TravelDemandPlanSwitch].Parse(true))
                {
                    // If accessible preference set with only Do not use underground, then we still want TDM rule applied
                    if (request.AccessiblePreferences.DoNotUseUnderground
                        && !request.AccessiblePreferences.RequireSpecialAssistance
                        && !request.AccessiblePreferences.RequireStepFreeAccess)
                    {
                        request.TravelDemandPlanOutward = pp[JC.Keys.JourneyRequest_TravelDemandPlanOutward_Accessible_DoNotUseUnderground];
                        request.TravelDemandPlanReturn = pp[JC.Keys.JourneyRequest_TravelDemandPlanReturn_Accessible_DoNotUseUnderground];
                    }
                    else
                    {
                        request.TravelDemandPlanOutward = pp[JC.Keys.JourneyRequest_TravelDemandPlanOff];
                        request.TravelDemandPlanReturn = pp[JC.Keys.JourneyRequest_TravelDemandPlanOff];
                    }
                }
            }
        }

        /// <summary>
        /// Updates the dont force coach flag in the journey request
        /// </summary>
        /// <param name="request"></param>
        private void UpdateDontForceCoach(TDPJourneyRequest request)
        {
            IPropertyProvider pp = Properties.Current;

            #region Dont Force Coach

            // Dont force coach is set using Property switches, and for the following scenarios only

            // Dont force coach rule, this overrides the force coach CJP config,
            // i.e. if true, then even if force coach is on in CJP settings do not perform force coach.
            // This is only set for accessible journeys where locations are in london and not coach stops
                
            // Default false to let CJP determine how to apply force coach rule
            request.DontForceCoach = false;

            // If PT journey...
            if (request.PlannerMode == TDPJourneyPlannerMode.PublicTransport)
            {
                int londonAdminAreaCode = pp["AccessibilityOptions.DistrictList.AdminAreaCode.London"].Parse(82);
                bool propertyDontForceCoach_OriginDestinationLondon = pp[JC.Keys.JourneyRequest_DontForceCoach_OriginDestinationLondon].Parse(false);
                bool propertyDontForceCoach_Accessible_OriginDestinationLondon = pp[JC.Keys.JourneyRequest_DontForceCoach_Accessible_OriginDestinationLondon].Parse(false);
                bool propertyDontForceCoach_Accessible_FewerChanges = pp[JC.Keys.JourneyRequest_DontForceCoach_Accessible_FewerChanges].Parse(false);

                // ... and both origin and destination are in london (adminarea = 82) and not accessible request, or
                if ((request.Origin != null && request.Origin.AdminAreaCode == londonAdminAreaCode)
                    && (request.Destination != null && request.Destination.AdminAreaCode == londonAdminAreaCode)
                    && !request.AccessiblePreferences.Accessible
                    && propertyDontForceCoach_OriginDestinationLondon)
                {
                    request.DontForceCoach = true;
                }
                // ... and both origin and destination are in london (adminarea = 82) and is accessible request (step free/assistance only), or
                else if ((request.Origin != null && request.Origin.AdminAreaCode == londonAdminAreaCode)
                    && (request.Destination != null && request.Destination.AdminAreaCode == londonAdminAreaCode)
                    && (request.AccessiblePreferences.RequireStepFreeAccess || request.AccessiblePreferences.RequireSpecialAssistance)
                    && propertyDontForceCoach_Accessible_OriginDestinationLondon)
                {
                    request.DontForceCoach = true;
                }
                // ... and fewer changes required
                else if (request.AccessiblePreferences.RequireFewerInterchanges
                    && propertyDontForceCoach_Accessible_FewerChanges)
                {
                    request.DontForceCoach = true;
                }
            }

            #endregion
        }

        #endregion
    }
}
