// *********************************************** 
// NAME             : CyclePlannerRequestPopulator.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 29 Mar 2011
// DESCRIPTION  	: CyclePlannerRequestPopulator class which is responsible for 
// populating Cycle planner requests for the Cycle planner service
// ************************************************
// 

using System;
using System.Collections;
using System.Collections.Generic;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using CPWS = TDP.UserPortal.CyclePlannerService.CyclePlannerWebService;
using TDP.Common;
using TDP.Common.ServiceDiscovery;
using Logger = System.Diagnostics.Trace;
using TDP.Common.EventLogging;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// CyclePlannerRequestPopulator class which is responsible for 
    /// populating Cycle planner requests for the Cycle planner service
    /// </summary>
    public class CyclePlannerRequestPopulator : JourneyRequestPopulator
    {
        #region Constructor

        /// <summary>
        /// Constructs a new CyclePlannerRequestPopulator
        /// </summary>
        /// <param name="request">ITDPJourneyRequest</param>
        public CyclePlannerRequestPopulator(ITDPJourneyRequest tdpJourneyRequest)
        {
            this.tdpJourneyRequest = tdpJourneyRequest;
            this.properties = Properties.Current;
            this.locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates the CyclePlannerRequest objects needed to call the Cycle planner service for the current 
        /// ITDPJourneyRequest, and returns them encapsulated in an array of CyclePlannerCall objects.
        /// </summary>
        public override CyclePlannerCall[] PopulateRequestsCTP(int referenceNumber,
                                                   int seqNo,
                                                   string sessionId,
                                                   bool referenceTransaction,
                                                   int userType,
                                                   string language)
        {
            List<CyclePlannerCall> cpCalls = new List<CyclePlannerCall>();

            CPWS.JourneyRequest request = null;

            if (tdpJourneyRequest.IsOutwardRequired)
            {
                request = PopulateSingleCycleRequest(tdpJourneyRequest, false,
                                                    referenceNumber, seqNo++,
                                                    sessionId, referenceTransaction,
                                                    userType, language);

                cpCalls.Add(new CyclePlannerCall(request, false, referenceNumber, sessionId));
            }

            if (tdpJourneyRequest.IsReturnRequired)
            {
                request = PopulateSingleCycleRequest(tdpJourneyRequest, true,
                                                referenceNumber, seqNo++,
                                                sessionId, referenceTransaction,
                                                userType, language);

                cpCalls.Add(new CyclePlannerCall(request, true, referenceNumber, sessionId));
            }

            return cpCalls.ToArray();
        }

        /// <summary>
        /// Creates the CJPRequest objects needed to call the CJP for the current 
        /// ITDPJourneyRequest, and returns them encapsulated in an array of CJPCall objects.
        /// </summary>
        public override CJPCall[] PopulateRequestsCJP(int referenceNumber, int seqNo, string sessionId, bool referenceTransaction, int userType, string language)
        {
            // CyclePlannerRequestPopulator does not support creating calls for the CJP
            throw new TDPException("CyclePlannerRequestPopulator does not support creating calls for the CJP", false, TDPExceptionIdentifier.JCUnsupportedJourneyRequestPopulator);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a single, fully populate CyclePlannerRequest object
        /// </summary>
        /// <returns></returns>
        private CPWS.JourneyRequest PopulateSingleCycleRequest(
            ITDPJourneyRequest tdpJourneyRequest,
            bool returnJourney,
            int referenceNumber,
            int seqNo,
            string sessionId,
            bool referenceTransaction,
            int userType,
            string language)
        {
            CPWS.JourneyRequest request = new CPWS.JourneyRequest();

            #region Initialise the request

            request.requestID = SqlHelper.FormatRef(referenceNumber) + FormatSeqNo(seqNo);
            request.referenceTransaction = referenceTransaction;
            request.sessionID = sessionId;
            request.language = language;
            request.userType = userType;

            #endregion

            #region Set locations and date/times, plus any TOIDs
            DateTime arriveTime = DateTime.MinValue;
            DateTime departTime = DateTime.MinValue;

            if (returnJourney)
            {
                request.depart = !tdpJourneyRequest.ReturnArriveBefore;

                if (tdpJourneyRequest.ReturnArriveBefore)
                {
                    arriveTime = GetRequestDateTime(tdpJourneyRequest, false);
                }
                else
                {
                    departTime = GetRequestDateTime(tdpJourneyRequest, false);
                }

                request.origin = PopulateRequestPlace(tdpJourneyRequest.ReturnOrigin, departTime, true, returnJourney);
                request.destination = PopulateRequestPlace(tdpJourneyRequest.ReturnDestination, arriveTime, false, returnJourney);
            }
            else
            {
                request.depart = !tdpJourneyRequest.OutwardArriveBefore;

                if (tdpJourneyRequest.OutwardArriveBefore)
                {
                    arriveTime = GetRequestDateTime(tdpJourneyRequest, true);
                }
                else
                {
                    departTime = GetRequestDateTime(tdpJourneyRequest, true);
                }

                request.origin = PopulateRequestPlace(tdpJourneyRequest.Origin, departTime, true, returnJourney);
                request.destination = PopulateRequestPlace(tdpJourneyRequest.Destination, arriveTime, false, returnJourney);
            }

            #endregion

            #region Set via locations and date/time

            // No via locations
            request.vias = new CPWS.RequestPlace[0];
            
            #endregion

            #region Set user preferences

            List<CPWS.UserPreference> userPreferences = new List<CPWS.UserPreference>();

            foreach (TDPUserPreference preference in tdpJourneyRequest.UserPreferences)
            {
                CPWS.UserPreference userPreference = new CPWS.UserPreference();
                userPreference.parameterID = Convert.ToInt32(preference.PreferenceKey);
                userPreference.parameterValue = preference.PreferenceValue;

                userPreferences.Add(userPreference);
            }

            request.userPreferences = userPreferences.ToArray();

            #endregion

            #region Set penalty function

            request.penaltyFunction = tdpJourneyRequest.PenaltyFunction;

            #endregion

            #region Set journey result settings

            CPWS.JourneyResultSettings resultSettings = new CPWS.JourneyResultSettings();

            // Get result setting values from properties
            resultSettings.includeToids = properties[Keys.JourneyResultSetting_IncludeToids].Parse(true);
            resultSettings.includeGeometry = properties[Keys.JourneyResultSetting_IncludeGeometry].Parse(true);
            resultSettings.includeText = properties[Keys.JourneyResultSetting_IncludeText].Parse(true);
            resultSettings.pointSeparator = GetCoordinateSeperator();
            resultSettings.eastingNorthingSeparator = GetEastingNorthingSeperator();

            request.journeyResultSettings = resultSettings;

            #endregion

            return request;
        }

        /// <summary>
        /// Takes an TDPLocation, and converts it into a CyclePlannerWebService RequestPlace
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private CPWS.RequestPlace PopulateRequestPlace(TDPLocation location, DateTime dateTime,
            bool isOrigin, bool isForReturnJourney)
        {
            CPWS.RequestPlace requestPlace = new CPWS.RequestPlace();

            // Changeable values based on location type
            string name = location.Name;
            OSGridReference osgr = location.CycleGridRef;

            // For cycle journey required, then should use the venue cycle park to populate details
            SetVenueLocationDetails(location, isOrigin, isForReturnJourney, ref name, ref osgr);

            #region name

            requestPlace.givenName = name;

            #endregion

            #region timedate

            if ((dateTime != null) && (dateTime != DateTime.MinValue))
            {
                requestPlace.timeDate = dateTime;
            }

            #endregion

            #region coordinate
            
            requestPlace.coordinate = new CPWS.Coordinate();
            requestPlace.coordinate.easting = Convert.ToInt32(osgr.Easting);
            requestPlace.coordinate.northing = Convert.ToInt32(osgr.Northing);
            
            #endregion

            #region road points

            List<CPWS.ITN> requestRoadPoints = new List<CPWS.ITN>();

            // Check if we should include Toid's in the request sent 
            // If not then cycle planner will use the coordinate passed to plan the journey from/to
            if (properties[Keys.JourneyRequest_IncludeToids].Parse(false))
            {
                #region Include toids in the request

                // check for toids
                if (location.Toid != null)
                {
                    List<string> toids = location.Toid;

                    #region Remove duplicate toids

                    // Remove any duplicate toids
                    List<string> editedToidList = new List<string>();

                    foreach (string toid in toids)
                    {
                        if (!editedToidList.Contains(toid))
                        {
                            editedToidList.Add(toid);
                        }
                    }

                    #endregion

                    string toidPrefix = properties[Keys.Toid_Prefix];

                    if (toidPrefix == null)
                    {
                        toidPrefix = string.Empty;
                    }

                    #region Add ITNs for toids

                    foreach (string toid in editedToidList)
                    {
                        CPWS.ITN itn = new CPWS.ITN();
                        
                        // toids must have the toid prefix
                        if (!toid.StartsWith(toidPrefix))
                        {
                            itn.TOID = toidPrefix + toid;
                        }
                        else
                        {
                            itn.TOID = toid;
                        }

                        itn.node = false;		// ESRI supplies us with links, not nodes

                        if ((dateTime != null) && (dateTime != DateTime.MinValue))
                        {
                            itn.timeDate = dateTime;
                        }

                        // Add the ITN
                        requestRoadPoints.Add(itn);
                    }

                    #endregion
                }

                #endregion
            }
            
            requestPlace.roadPoints = requestRoadPoints.ToArray();

            #endregion

            return requestPlace;
        }

        /// <summary>
        /// Updates the name and grid reference if criteria to use Venue specific details are met, 
        /// if not then ref values remain unaltered.
        /// e.g. if for a Cycle journey and the Venue location has a Cycle Park Id specified
        /// </summary>
        private void SetVenueLocationDetails(TDPLocation location, 
            bool isOrigin, bool isForReturnJourney,
            ref string name, ref OSGridReference osgr)
        {
            if (location is TDPVenueLocation)
            {
                TDPVenueLocation venueLocation = (TDPVenueLocation)location;

                // Use the venue cycle park to populate details
                if (!string.IsNullOrEmpty(venueLocation.SelectedTDPParkID))
                {
                    #region Update for Cycle park location

                    TDPVenueCyclePark cyclePark = locationService.GetTDPVenueCyclePark(venueLocation.SelectedTDPParkID);

                    if (cyclePark != null)
                    {
                        // For outward journey, destination will be to the "entrance" OSGR of cycle park
                        if (!isForReturnJourney && !isOrigin)
                        {
                            name = cyclePark.Name;

                            if (cyclePark.CycleToGridReference != null && cyclePark.CycleToGridReference.IsValid)
                            {
                                osgr = cyclePark.CycleToGridReference;
                            }
                            else
                            {
                                // Should exist but log to inform support
                                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning,
                                    string.Format("Attempt to use CyclePark[{0}][{1}] for journey planning is missing a valid To OSGR", cyclePark.ID, cyclePark.Name)));
                            }
                        }
                        // For return journey, origin will be from the "exit" OSGR of cycle park
                        else if ((isForReturnJourney && isOrigin)
                        // Mobile scenario:
                        // For outward journey venue to non-venue, origin will be from the "exit" OSGR of cycle park
                            || (!isForReturnJourney && isOrigin))
                        {
                            name = cyclePark.Name;

                            if (cyclePark.CycleFromGridReference != null && cyclePark.CycleFromGridReference.IsValid)
                            {
                                osgr = cyclePark.CycleFromGridReference;
                            }
                            else
                            {
                                // Should exist but log to inform support
                                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning,
                                    string.Format("Attempt to use CyclePark[{0}][{1}] for journey planning is missing a valid From OSGR", cyclePark.ID, cyclePark.Name)));
                            }
                        }
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// Returns the Outward/Return date time to use in the request. 
        /// If the location is a Venue location which contains a "datetime to use", then this is returned
        /// but only for the Outward journey Destination location or Return journey Origin location
        /// (as these are currently the ones only set for a Cycle park journey),
        /// otherwise returns the journey request date time
        /// </summary>
        private DateTime GetRequestDateTime(ITDPJourneyRequest journeyRequest, bool outward)
        {
            DateTime dateTime = DateTime.MinValue;

            TDPVenueLocation venueLocation = null;

            if (outward)
            {
                dateTime = journeyRequest.OutwardDateTime;

                // For outward journey, if destination is Venue, then use it's datetime
                if (journeyRequest.Destination is TDPVenueLocation)
                {
                    venueLocation = (TDPVenueLocation)journeyRequest.Destination;
                }
                // For Mobile scenario, is origin is Venue, then use it's datetime
                else if (journeyRequest.Origin is TDPVenueLocation)
                {
                    venueLocation = (TDPVenueLocation)journeyRequest.Origin;
                }
            }
            else
            {
                dateTime = journeyRequest.ReturnDateTime;

                // For return journey, if origin is Venue, then use it's datetime
                if (journeyRequest.ReturnOrigin is TDPVenueLocation)
                {
                    venueLocation = (TDPVenueLocation)journeyRequest.ReturnOrigin;
                }
            }

            #region Use Venue location date time

            if (venueLocation != null)
            {
                // Cycle may specify a Venue location datetime value, but not guranteed
                if (journeyRequest.PlannerMode == TDPJourneyPlannerMode.Cycle)
                {
                    if (outward)
                    {
                        if (venueLocation.SelectedOutwardDateTime != DateTime.MinValue)
                        {
                            dateTime = venueLocation.SelectedOutwardDateTime;
                        }
                    }
                    else
                    {
                        if (venueLocation.SelectedReturnDateTime != DateTime.MinValue)
                        {
                            dateTime = venueLocation.SelectedReturnDateTime;
                        }
                    }
                }
            }

            #endregion

            return dateTime;
        }

        #endregion

        #region Public Static methods

        /// <summary>
        /// Returns the seperator char used to split a number of coordinates in a string
        /// </summary>
        public static char GetCoordinateSeperator()
        {
            char coordinateSeperator;

            if (!Char.TryParse(Properties.Current[Keys.JourneyResultSetting_PointSeperator], out coordinateSeperator))
            {
                // Invalid value, ignore and set to locally
                coordinateSeperator = ' ';
            }

            return coordinateSeperator;
        }

        /// <summary>
        /// Returns the seperator charused to split the easting/northing value in a coordinate in a string
        /// </summary>
        /// <returns></returns>
        public static char GetEastingNorthingSeperator()
        {
            char eastingNorthingSeperator;

            if (!Char.TryParse(Properties.Current[Keys.JourneyResultSetting_EastingNorthingSeperator], out eastingNorthingSeperator))
            {
                // Invalid value, ignore and set locally
                eastingNorthingSeperator = ',';
            }

            return eastingNorthingSeperator;
        }

        #endregion
    }
}
