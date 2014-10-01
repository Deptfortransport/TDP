// *********************************************** 
// NAME             : MultiModalJourneyRequestPopulator.cs
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 30 Mar 2011
// DESCRIPTION  	: MultiModalJourneyRequestPopulator class responsible for populating CJP JourneyRequests
// for all multi-modal and car-only journeys.
// ************************************************
// 

using System;
using System.Collections.Generic;
using TDP.Common;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using ICJP = TransportDirect.JourneyPlanning.CJPInterface;
using Logger = System.Diagnostics.Trace;
using TDP.Common.EventLogging;
using System.Linq;
using TDP.Common.DataServices.AirportData;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// MultiModalJourneyRequestPopulator class responsible for populating CJP JourneyRequests
    /// for all multi-modal and car-only journeys
    /// </summary>
    public class MultiModalJourneyRequestPopulator : JourneyRequestPopulator
    {
        #region Constructor

        /// <summary>
        /// Constructs a new MultiModalJourneyRequestPopulator
        /// </summary>
        /// <param name="request">ITDPJourneyRequest</param>
        public MultiModalJourneyRequestPopulator(ITDPJourneyRequest tdpJourneyRequest)
        {
            this.tdpJourneyRequest = tdpJourneyRequest;
            this.properties = Properties.Current;
            this.locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates the CJPRequest objects needed to call the CJP for the current 
        /// ITDPJourneyRequest, and returns them encapsulated in an array of CJPCall objects.
        /// </summary>
        public override CJPCall[] PopulateRequestsCJP(int referenceNumber, int seqNo, string sessionId, bool referenceTransaction, int userType, string language)
        {
            List<CJPCall> cjpCalls = new List<CJPCall>();

            ICJP.JourneyRequest request = null;

            // Outward journey required (or its a replan outward journey required)
            if ((tdpJourneyRequest.IsOutwardRequired && !tdpJourneyRequest.IsReplan)
                || (tdpJourneyRequest.ReplanIsOutwardRequired && tdpJourneyRequest.IsReplan))
            {
                request = PopulateSingleRequest(tdpJourneyRequest, false,
                                                    referenceNumber, seqNo++,
                                                    sessionId, referenceTransaction,
                                                    userType, language);

                cjpCalls.Add(new CJPCall(request, false, referenceNumber, sessionId));
            }

            // Return journey required (or its a reaplan return journey required)
            if ((tdpJourneyRequest.IsReturnRequired && !tdpJourneyRequest.IsReplan)
                || (tdpJourneyRequest.ReplanIsReturnRequired && tdpJourneyRequest.IsReplan))
            {
                request = PopulateSingleRequest(tdpJourneyRequest, true,
                                                referenceNumber, seqNo++,
                                                sessionId, referenceTransaction,
                                                userType, language);

                cjpCalls.Add(new CJPCall(request, true, referenceNumber, sessionId));
            }

            return cjpCalls.ToArray();
        }


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
            // MultiModalJourneyRequestPopulator does not support creating calls for the CTP
            throw new TDPException("MultiModalJourneyRequestPopulator does not support creating calls for the CTP", false, TDPExceptionIdentifier.JCUnsupportedJourneyRequestPopulator);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Create a single fully-populated CJP JourneyRequest object
        /// for a single multimodal request for a specified date.
        /// </summary>
        private ICJP.JourneyRequest PopulateSingleRequest(ITDPJourneyRequest tdpJourneyRequest,
            bool returnJourney,
            int referenceNumber,
            int seqNo,
            string sessionId,
            bool referenceTransaction,
            int userType,
            string language)
        {
            #region Initialise the request

            ICJP.JourneyRequest cjpRequest = InitialiseNewRequest(referenceNumber, seqNo,
                                                                sessionId, referenceTransaction,
                                                                userType, language);

            #endregion

            #region Set locations and date/times, plus any TOIDs

            bool privateRequired = tdpJourneyRequest.Modes.Contains(TDPModeType.Car);
            bool publicRequired = (tdpJourneyRequest.Modes.Count > 1 || tdpJourneyRequest.Modes[0] != TDPModeType.Car);

            bool isForAccessibleJourney = tdpJourneyRequest.AccessiblePreferences.Accessible;

            DateTime arriveTime = DateTime.MinValue;
            DateTime departTime = DateTime.MinValue;

            if (returnJourney)
            {
                // Return arrive before time (or its a replan arrive before time)
                if ((tdpJourneyRequest.ReturnArriveBefore && !tdpJourneyRequest.IsReplan)
                    || (tdpJourneyRequest.ReplanReturnArriveBefore && tdpJourneyRequest.IsReplan))
                {
                    arriveTime = GetRequestDateTime(tdpJourneyRequest, false);

                    cjpRequest.depart = false;
                }
                else
                {
                    departTime = GetRequestDateTime(tdpJourneyRequest, false);

                    cjpRequest.depart = true;
                }

                cjpRequest.origin = PopulateRequestPlace(tdpJourneyRequest.ReturnOrigin, departTime,
                    publicRequired, privateRequired, true, returnJourney, isForAccessibleJourney);
                cjpRequest.destination = PopulateRequestPlace(tdpJourneyRequest.ReturnDestination, arriveTime,
                    publicRequired, privateRequired, false, returnJourney, isForAccessibleJourney);
            }
            else
            {
                cjpRequest.depart = !tdpJourneyRequest.OutwardArriveBefore;

                // Outward arrive before time (or its a replan arrive before time)
                if ((tdpJourneyRequest.OutwardArriveBefore && !tdpJourneyRequest.IsReplan)
                    || (tdpJourneyRequest.ReplanOutwardArriveBefore && tdpJourneyRequest.IsReplan))
                {
                    arriveTime = GetRequestDateTime(tdpJourneyRequest, true);

                    cjpRequest.depart = false;
                }
                else
                {
                    departTime = GetRequestDateTime(tdpJourneyRequest, true);

                    cjpRequest.depart = true;
                }

                cjpRequest.origin = PopulateRequestPlace(tdpJourneyRequest.Origin, departTime,
                    publicRequired, privateRequired, true, returnJourney, isForAccessibleJourney);
                cjpRequest.destination = PopulateRequestPlace(tdpJourneyRequest.Destination, arriveTime,
                    publicRequired, privateRequired, false, returnJourney, isForAccessibleJourney);
            }

            #endregion

            cjpRequest.serviceFilter = null; // No services filter required
            cjpRequest.operatorFilter = null; // No operators filter required
            cjpRequest.parkNRide = false; // Not a park and ride request

            #region Modes

            List<TDPModeType> modes = tdpJourneyRequest.Modes;

            cjpRequest.modeFilter = new ICJP.Modes();
            cjpRequest.modeFilter.include = true;

            if (modes.Contains(TDPModeType.Bus) && !modes.Contains(TDPModeType.Drt))
            {
                bool isDrtRequired = properties[Keys.JourneyRequest_DrtIsRequired].Parse(true);

                if (isDrtRequired)
                {
                    modes.Add(TDPModeType.Drt);
                }
            }

            cjpRequest.modeFilter.modes = GetModeArray(modes);

            #endregion

            if (privateRequired)
            {
                cjpRequest.privateParameters = SetPrivateParameters(tdpJourneyRequest);
            }

            if (publicRequired)
            {
                cjpRequest.publicParameters = SetPublicParameters(tdpJourneyRequest, privateRequired, returnJourney);
            }

            return cjpRequest;
        }

        /// <summary>
        /// Instantiates a CJP JourneyRequest and populates some common attributes. 
        /// </summary>
        private ICJP.JourneyRequest InitialiseNewRequest(int referenceNumber, int seqNo, string sessionId,
                                                        bool referenceTransaction, int userType,
                                                        string language)
        {
            ICJP.JourneyRequest cjpRequest = new ICJP.JourneyRequest();

            cjpRequest.requestID = SqlHelper.FormatRef(referenceNumber) + FormatSeqNo(seqNo);
            cjpRequest.referenceTransaction = referenceTransaction;
            cjpRequest.sessionID = sessionId;
            cjpRequest.language = language;
            cjpRequest.userType = userType;

            return cjpRequest;
        }

        /// <summary>
		/// Takes an TDPLocation, and convert it into a CJP RequestPlace
		/// </summary>
		/// <returns></returns>
        private ICJP.RequestPlace PopulateRequestPlace(TDPLocation location, DateTime dateTime, 
            bool publicRequired, bool privateRequired,
            bool isOrigin, bool isForReturnJourney, bool isForAccessibleJourney)
		{
            ICJP.RequestPlace requestPlace = new ICJP.RequestPlace();

            // Changeable values based on location type
            string name = location.DisplayName;
            List<string> toids = location.Toid;
            List<string> naptans = location.Naptan;
            OSGridReference gridRef = location.GridRef;

            // If Private journey required, then should use the venue car park to populate details
            // If Public journey required, then should use the venue naptans to populate details
            SetVenueLocationDetails(location, privateRequired, isOrigin, isForReturnJourney, isForAccessibleJourney,
                ref name, ref toids, ref naptans, ref gridRef);

            // If airport location, ensure naptans include the terminal number 
            // otherwise CJP will not plan journeys - this method will take the pseudo naptan 9200XXX and 
            // append terminal numbers as required, e.g. 9200XXX1, 9200XXX2
            UpdateAirportLocationDetails(location, ref naptans);

            #region name

            requestPlace.givenName = name;

            #endregion

            #region locality

            requestPlace.locality = location.Locality;

            #endregion

            #region via

            // No via locations required
            requestPlace.userSpecifiedVia = false;

            #endregion
                                    
            #region place type

            // request place type
            switch (location.TypeOfLocation)
            {
                case TDPLocationType.Venue:
                    // Because some venues have unrecognised NaPTANs, should use the coordinate instead.
                    // This gives a better chance of finding journeys
                    if (location.UseNaPTAN)
                    {
                        requestPlace.type = ICJP.RequestPlaceType.NaPTAN;
                    }
                    else
                    {
                        requestPlace.type = ICJP.RequestPlaceType.Coordinate;

                        // If UseNaPTAN is false, but we have an accessible journey request 
                        // that contains accessible naptans, then use that
                        if (isForAccessibleJourney)
                        {
                            if (location is TDPVenueLocation)
                            {
                                TDPVenueLocation venueLocation = (TDPVenueLocation)location;

                                if ((venueLocation.AccessibleNaptans != null) && (venueLocation.AccessibleNaptans.Count > 0))
                                {
                                    requestPlace.type = ICJP.RequestPlaceType.NaPTAN;
                                }
                            }
                        }
                    }
                    break;
                case TDPLocationType.Locality:
                    requestPlace.type = ICJP.RequestPlaceType.Locality;
                    break;
                case TDPLocationType.Postcode:
                case TDPLocationType.Address:
                    requestPlace.type = ICJP.RequestPlaceType.Coordinate;
                    break;
                case TDPLocationType.POI:
                    requestPlace.type = ICJP.RequestPlaceType.Coordinate;
                    break;
                case TDPLocationType.CoordinateEN:
                    requestPlace.type = ICJP.RequestPlaceType.Coordinate;
                    break;
                default:
                    requestPlace.type = ICJP.RequestPlaceType.NaPTAN;
                    break;
            }

            #endregion

            #region coordinate

            // coordinates
            requestPlace.coordinate = new ICJP.Coordinate();
            requestPlace.coordinate.easting = Convert.ToInt32(gridRef.Easting);
            requestPlace.coordinate.northing = Convert.ToInt32(gridRef.Northing);

            #endregion

            if	(publicRequired)
            {
                #region naptans

                List<ICJP.RequestStop> requestStops = new List<ICJP.RequestStop>();

                if (naptans.Count == 0)
				{
                    // Always need at least one naptan, even if it's a dummy empty string,
                    //  so that we've got somewhere to hang the time 

                    ICJP.RequestStop requestStop = new ICJP.RequestStop();
                    					
					requestStop.NaPTANID = string.Empty;

					if	((dateTime != null) && (dateTime != DateTime.MinValue))
					{
						requestStop.timeDate = dateTime;
					}

                    requestStops.Add(requestStop);
				}
				else
				{
                    foreach (string naptan in naptans)
                    {
                        ICJP.RequestStop requestStop = new ICJP.RequestStop();

                        requestStop.NaPTANID = naptan;

                        if ((dateTime != null) && (dateTime != DateTime.MinValue))
                        {
                            requestStop.timeDate = dateTime;
                        }

                        if (gridRef.IsValid)
                        {
                            requestStop.coordinate = new ICJP.Coordinate();
                            requestStop.coordinate.easting = Convert.ToInt32(gridRef.Easting);
                            requestStop.coordinate.northing = Convert.ToInt32(gridRef.Northing);
                        }

                        requestStops.Add(requestStop);
                    }
				}

                requestPlace.stops = requestStops.ToArray();

                #endregion
            }

			if	(privateRequired)
            {
                #region road points

                List<ICJP.ITN> requestRoadPoints = new List<ICJP.ITN>();

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

				if	(toidPrefix == null) 
				{
					toidPrefix = string.Empty;
				}

                #region Add ITNs for toids

                foreach (string toid in editedToidList)
                {
                    ICJP.ITN itn = new ICJP.ITN();

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

                requestPlace.roadPoints = requestRoadPoints.ToArray();

                #endregion
            }

            return requestPlace;
		}
        
        /// <summary>
        /// Create an array of ICJP.Mode from the given array of TDPModeType
        /// Modes are used within the CJPJourneyRequest Type
        /// </summary>
        private ICJP.Mode[] GetModeArray(List<TDPModeType> modes)
        {
            if (modes != null)
            {
                modes.Sort();

                List<ICJP.Mode> modeResult = new List<ICJP.Mode>();
                
                foreach(TDPModeType tdpMode in modes)
                {
                    ICJP.Mode mode = new ICJP.Mode();
                    
                    mode.mode = TDPModeTypeHelper.GetCJPModeType(tdpMode);

                    modeResult.Add(mode);
                }

                return modeResult.ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Updates the name, toids, naptans, if criteria to use Venue specific details are met, 
        /// if not then ref values remain unaltered.
        /// e.g. if for a Car journey and the Venue location has a Car Park Id specified
        /// e.g. if for a Public journey and the Venue river pier naptan is specified
        /// </summary>
        private void SetVenueLocationDetails(TDPLocation location, bool privateRequired,
            bool isOrigin, bool isForReturnJourney, bool isForAccessibleJourney,
            ref string name, ref List<string> toids, ref List<string> naptans, ref OSGridReference gridRef)
        {
            if (location is TDPVenueLocation)
            {
                TDPVenueLocation venueLocation = (TDPVenueLocation)location;
                
                // If Private journey required, then should use the venue car park to populate details
                if ((privateRequired) && (!string.IsNullOrEmpty(venueLocation.SelectedTDPParkID)))
                {
                    #region Update for Car park location
                                        
                    TDPVenueCarPark carPark = locationService.GetTDPVenueCarPark(venueLocation.SelectedTDPParkID);

                    if (carPark != null)
                    {
                        // For outward journey, destination will be to the "entrance" TOID of car park
                        if (!isForReturnJourney && !isOrigin)
                        {
                            name = carPark.Name;

                            if (!string.IsNullOrEmpty(carPark.DriveToToid))
                            {
                                toids = new List<string>(1);
                                toids.Add(carPark.DriveToToid.Trim());
                            }
                            else
                            {
                                // Should exist but log to inform support
                                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning, 
                                    string.Format("Attempt to use CarPark[{0}][{1}] for journey planning is missing a DriveToToid", carPark.ID, carPark.Name)));
                            }
                        }
                        // For return journey, origin will be to the "exit" TOID of car park
                        else if (isForReturnJourney && isOrigin)
                        {
                            name = carPark.Name;

                            if (!string.IsNullOrEmpty(carPark.DriveFromToid))
                            {
                                toids = new List<string>(1);
                                toids.Add(carPark.DriveFromToid.Trim());
                            }
                            else
                            {
                                // Should exist but log to inform support
                                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Warning,
                                    string.Format("Attempt to use CarPark[{0}][{1}] for journey planning is missing a DriveFromToid", carPark.ID, carPark.Name)));
                            }
                        }
                    }

                    #endregion
                }
                // If Public journey required, and pier naptan, then should use the venue river service to populate details
                else if (!string.IsNullOrEmpty(venueLocation.SelectedPierNaptan))
                {
                    #region Update for River Services Pier location

                    // Use pier naptans
                    List<string> pierNaPTANs = new List<string>() { venueLocation.SelectedPierNaptan };
                    naptans = pierNaPTANs;

                    // Use name provided
                    if (!string.IsNullOrEmpty(venueLocation.SelectedName))
                    {
                        name = venueLocation.SelectedName;
                    }

                    if (venueLocation.SelectedGridReference.IsValid)
                    {
                        gridRef = venueLocation.SelectedGridReference;
                    }

                    #endregion
                }
                else if (isForAccessibleJourney)
                {
                    #region Update for Accessible venue location

                    // Use the venue accessible naptans if they exist
                    if ((venueLocation.AccessibleNaptans != null) && (venueLocation.AccessibleNaptans.Count > 0))
                    {
                        naptans = venueLocation.AccessibleNaptans;
                    }

                    // If a name provided
                    if (!string.IsNullOrEmpty(venueLocation.SelectedName))
                    {
                        if (naptans.Count > 1)
                        {
                            // Set name to empty string in the event multiple accessible naptans are added,
                            // the cjp will then output the name rather than the one set here
                            name = string.Empty;
                        }
                        else
                        {
                            name = venueLocation.SelectedName;
                        }
                    }

                    #endregion
                }
            }
        }

        /// <summary>
        /// Method will update the pseudo naptan 9200XXX and 
        /// replace/append with terminal numbers using the AirDataProvider, e.g. 9200XXX1, 9200XXX2.
        /// If location naptan already includes terminal numbers or is not an airport naptan, 
        /// then no changes are applied
        /// </summary>
        /// <param name="location"></param>
        /// <param name="naptans"></param>
        private void UpdateAirportLocationDetails(TDPLocation location, ref List<string> naptans)
        {
            List<string> updatedNaptans = new List<string>();

            // Naptan Prefix Length
            int PREFIX_LENGTH	= 4;

		    // IATA code length e.g. LHR, LGW, BHX etc
    		int IATA_LENGTH = 3;

		    // Terminal code length
		    int TERMINAL_LENGTH = 1;

            // Airport Data
            AirDataProvider airData = TDPServiceDiscovery.Current.Get<AirDataProvider>(ServiceDiscoveryKey.AirDataProvider);

            foreach (string naptan in naptans)
            {
                // Naptan to update
                string tmpNaptan = naptan;

                if (TDPLocationTypeHelper.GetTDPLocationTypeForNaPTAN(tmpNaptan) == TDPLocationType.StationAirport)
                {
                    // Strip off dummy terminal numb "0" so we can handle it like other groups 
                    if (tmpNaptan.Length == (PREFIX_LENGTH + IATA_LENGTH + TERMINAL_LENGTH)
                        && tmpNaptan.EndsWith("0"))
                    {
                        tmpNaptan = naptan.Substring(0, PREFIX_LENGTH + IATA_LENGTH);
                    }

                    if (tmpNaptan.Length == (PREFIX_LENGTH + IATA_LENGTH))
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                            string.Format("Updating airport naptan[{0}] with terminal numbers", tmpNaptan)));

                        // Find airport for iata code and add all of its naptans
                        Airport airport = airData.GetAirport(tmpNaptan.Substring(PREFIX_LENGTH, IATA_LENGTH));

                        if (airport != null)
                        {
                            updatedNaptans.AddRange(airport.Naptans);
                        }
                        else
                        {
                            // if we cannot find airport in our database table, it's because the airport's
                            //  been 'thrown away' for having no domestic flights -- in this case assume
                            //  it's a little one with just one terminal ...
                            updatedNaptans.Add(tmpNaptan + "1");
                        }
                    }
                    else
                    {
                        // Not airport length naptan, so ignore
                        updatedNaptans.Add(tmpNaptan);
                    }
                }
                else
                {
                    // Not airport naptan, so ignore
                    updatedNaptans.Add(tmpNaptan);
                }
            }

            // Update the reference naptan list
            naptans = updatedNaptans;
        }

        /// <summary>
        /// Returns the Outward/Return date time to use in the request. 
        /// If the location is a Venue location which contains a "datetime to use", then this is returned
        /// but only for the Outward journey Destination location or Return journey Origin location
        /// (as these are currently the ones only set for a Car park journey),
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
                // Currently only Park and Ride, Blue Badge, and River Service specify a Venue location datetime value,
                // and also a Public Transport Accessible journey
                if ((journeyRequest.PlannerMode == TDPJourneyPlannerMode.ParkAndRide
                    || journeyRequest.PlannerMode == TDPJourneyPlannerMode.BlueBadge
                    || journeyRequest.PlannerMode == TDPJourneyPlannerMode.RiverServices)
                    || (journeyRequest.PlannerMode == TDPJourneyPlannerMode.PublicTransport && journeyRequest.AccessiblePreferences.Accessible))
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

            #region Replan datetime

            // If its a replan, then need to use the replan datetime
            // TODO: Mitesh - update replan datetime to take into account the "venue selected datetime" (accessible journey)
            if (journeyRequest.IsReplan)
            {
                if (outward)
                {
                    dateTime = journeyRequest.ReplanOutwardDateTime;
                }
                else
                {
                    dateTime = journeyRequest.ReplanReturnDateTime;
                }
            }

            #endregion

            return dateTime;
        }

        #region Private parameters

        /// <summary>
        /// Fill the CJP PrivateParameters fields for a single multimodal request.
        /// </summary>
        private ICJP.PrivateParameters SetPrivateParameters(ITDPJourneyRequest tdpJourneyRequest)
        {
            ICJP.PrivateParameters privateParameters = new ICJP.PrivateParameters();

            // Fixed parameters
            privateParameters.flowType = ICJP.FlowType.Congestion;
            privateParameters.vehicleType = ICJP.VehicleType.Car;

            // Changeable parameters
            privateParameters.avoidMotorway = tdpJourneyRequest.AvoidMotorways;
            privateParameters.avoidFerries = tdpJourneyRequest.AvoidFerries;
            privateParameters.avoidToll = tdpJourneyRequest.AvoidTolls;
            privateParameters.avoidRoads = GetRoads(tdpJourneyRequest.AvoidRoads);
            privateParameters.useRoads = GetRoads(tdpJourneyRequest.IncludeRoads);
            privateParameters.algorithm = GetPrivateAlgorithm(tdpJourneyRequest.PrivateAlgorithm);
            privateParameters.maxSpeed = tdpJourneyRequest.DrivingSpeed;
            privateParameters.banMotorway = tdpJourneyRequest.DoNotUseMotorways;
            privateParameters.fuelConsumption = Convert.ToInt32(Convert.ToDecimal((tdpJourneyRequest.FuelConsumption)));
            privateParameters.fuelPrice = Convert.ToInt32(Convert.ToDecimal((tdpJourneyRequest.FuelPrice)));

            // No via locations required
            privateParameters.vias = new ICJP.RequestPlace[0];

            return privateParameters;
        }

        /// <summary>
        /// Create an array of Roads and populate the contents from an array of strings
        /// </summary>
        /// <param name="Roads">String array of road names</param>
        private ICJP.Road[] GetRoads(List<string> stringRoads)
        {
            List<ICJP.Road> roadResult = new List<ICJP.Road>();

            foreach(string strRoad in stringRoads)
            {
                if (!string.IsNullOrEmpty(strRoad))
                {
                    ICJP.Road road = new ICJP.Road();

                    road.roadNumber = strRoad;

                    roadResult.Add(road);
                }
            }

            return roadResult.ToArray();
        }

        /// <summary>
        /// Converts an TDPPrivateAlgorithmType into a CJP PrivateAlgorithmType
        /// </summary>
        /// <param name="tdpPrivateAlgorithm"></param>
        /// <returns></returns>
        private ICJP.PrivateAlgorithmType GetPrivateAlgorithm(TDPPrivateAlgorithmType tdpPrivateAlgorithm)
        {
            switch (tdpPrivateAlgorithm)
            {
                case TDPPrivateAlgorithmType.Cheapest:
                    return ICJP.PrivateAlgorithmType.Cheapest;
                case TDPPrivateAlgorithmType.MostEconomical:
                    return ICJP.PrivateAlgorithmType.MostEconomical;
                case TDPPrivateAlgorithmType.Shortest:
                    return ICJP.PrivateAlgorithmType.Shortest;
                default:
                    return ICJP.PrivateAlgorithmType.Fastest;
            }
        }

        #endregion

        #region Public Parameters

        /// <summary>
        /// Fill the PublicParameters fields for a single multimodal request.
        /// </summary>
        private ICJP.PublicParameters SetPublicParameters(ITDPJourneyRequest tdpJourneyRequest, bool privateRequired, bool returnJourney)
        {
            ICJP.PublicParameters parameters = new ICJP.PublicParameters();

            // Fixed parameters
            parameters.trunkPlan = false;
            parameters.intermediateStops = ICJP.IntermediateStopsType.All;
            parameters.rangeType = ICJP.RangeType.Sequence;

            // Changeable parameters
            parameters.algorithm = GetPublicAlgorithm(tdpJourneyRequest.PublicAlgorithm);
            parameters.interchangeSpeed = tdpJourneyRequest.InterchangeSpeed;
            parameters.walkSpeed = tdpJourneyRequest.WalkingSpeed;
            parameters.sequence = (privateRequired ? tdpJourneyRequest.Sequence - 1 : tdpJourneyRequest.Sequence);
            parameters.extraSequence = 0;
            parameters.extraInterval = DateTime.MinValue;
            parameters.extraCheckInTime = DateTime.MinValue;

            // Distance in metres, times in minutes, speeds in metres/min ...
            if (tdpJourneyRequest.MaxWalkingDistance > 0)
            {
                parameters.maxWalkDistance = tdpJourneyRequest.MaxWalkingDistance;
            }
            else
            {
                parameters.maxWalkDistance = tdpJourneyRequest.WalkingSpeed * tdpJourneyRequest.MaxWalkingTime;
            }

            // No via locations required
            parameters.vias = new ICJP.RequestPlace[0];
            parameters.softVias = new ICJP.RequestPlace[0];
            parameters.notVias = new ICJP.RequestPlace[0];
            
            // Set up Routing guide specific values
            parameters.routingGuideInfluenced = tdpJourneyRequest.RoutingGuideInfluenced;
            parameters.rejectNonRGCompliantJourneys = tdpJourneyRequest.RoutingGuideCompliantJourneysOnly;
            parameters.routeCodes = tdpJourneyRequest.RouteCodes;

            // Set up Awkward Overnight Journey Rules
            parameters.removeAwkwardOvernight = tdpJourneyRequest.RemoveAwkwardOvernight;

            // Games specific
            parameters.olympicRequest = tdpJourneyRequest.OlympicRequest;
            if (returnJourney)
            {
                if (!string.IsNullOrEmpty(tdpJourneyRequest.TravelDemandPlanReturn))
                    parameters.travelDemandPlan = tdpJourneyRequest.TravelDemandPlanReturn;
            }
            else
            {
                if (!string.IsNullOrEmpty(tdpJourneyRequest.TravelDemandPlanOutward))
                    parameters.travelDemandPlan = tdpJourneyRequest.TravelDemandPlanOutward;
            }
            
            // Accessible parameters
            parameters.accessibilityOptions = GetAccessibilityOptions(tdpJourneyRequest);
            parameters.filtering = (tdpJourneyRequest.FilteringStrict) ? ICJP.FilterOptionEnumeration.strict : ICJP.FilterOptionEnumeration.permissive;

            // Force coach 
            parameters.dontForceCoach = tdpJourneyRequest.DontForceCoach;

            return parameters;
        }

        /// <summary>
        /// Converts an TDPPublicAlgorithmType into a CJP PublicAlgorithmType
        /// </summary>
        /// <param name="tdpPublicAlgorithm"></param>
        /// <returns></returns>
        private ICJP.PublicAlgorithmType GetPublicAlgorithm(TDPPublicAlgorithmType tdpPublicAlgorithm)
        {
            switch (tdpPublicAlgorithm)
            {
                case TDPPublicAlgorithmType.Fastest:
                    return ICJP.PublicAlgorithmType.Fastest;
                case TDPPublicAlgorithmType.Max1Change:
                    return ICJP.PublicAlgorithmType.Max1Change;
                case TDPPublicAlgorithmType.Max2Changes:
                    return ICJP.PublicAlgorithmType.Max2Changes;
                case TDPPublicAlgorithmType.Max3Changes:
                    return ICJP.PublicAlgorithmType.Max3Changes;
                case TDPPublicAlgorithmType.NoChanges:
                    return ICJP.PublicAlgorithmType.NoChanges;
                case TDPPublicAlgorithmType.MinChanges:
                    return ICJP.PublicAlgorithmType.MinChanges;
                default:
                    return ICJP.PublicAlgorithmType.Default;
            }
        }

        /// <summary>
        /// Gets the CJP AccessibilityOptions for the request
        /// </summary>
        /// <param name="tdpJourneyRequest"></param>
        /// <returns></returns>
        private ICJP.AccessibilityOptions GetAccessibilityOptions(ITDPJourneyRequest tdpJourneyRequest)
        {
            TDPAccessiblePreferences accessiblePreferences = tdpJourneyRequest.AccessiblePreferences;

            // Accessible journey required
            if ((accessiblePreferences != null) && (accessiblePreferences.Accessible))
            {
                ICJP.AccessibilityOptions accessibilityOptions = new ICJP.AccessibilityOptions();
                                
                if (accessiblePreferences.DoNotUseUnderground)
                {
                    // Remove Underground from modes
                    // This will have already been done by the JourneyRequestHelper when setting the modes for request
                }
                if (accessiblePreferences.RequireStepFreeAccess)
                {
                    accessibilityOptions.wheelchairUse = ICJP.AccessibilityRequirement.Essential;
                }
                if (accessiblePreferences.RequireSpecialAssistance)
                {
                    accessibilityOptions.assistanceService = ICJP.AccessibilityRequirement.Essential;
                }
                                                
                return accessibilityOptions;
            }

            // Accessible journey options not required
            return null;
        }

        #endregion

        #endregion
    }
}
