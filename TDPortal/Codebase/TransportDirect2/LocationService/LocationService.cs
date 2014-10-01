// *********************************************** 
// NAME             : LocationService.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: LocationService class to cache and return locations
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using TDP.Common.EventLogging;
using TDP.Common.LocationService.GIS;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Reporting.Events;
using TransportDirect.Presentation.InteractiveMapping;
using Logger = System.Diagnostics.Trace;

namespace TDP.Common.LocationService
{
    /// <summary>
    /// LocationService class to cache and return locations
    /// </summary>
    public class LocationService
    {
        #region Private members

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LocationService()
        {
            LoadData();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns the version of the locations cache. This value is used with the 
        /// locations javascript files, to ensure the cache and js are from the same 
        /// dataset
        /// </summary>
        public string LocationVersion()
        {
            return TDPLocationCache.GetVersion();
        }

        /// <summary>
        /// Resolves a location for the search using the location cache/gazetteer
        /// </summary>
        /// <param name="search">Search object containing search details. 
        /// The search object is updated with ambiguous locations if location not found</param>
        /// <returns>Null if no location found</returns>
        public TDPLocation ResolveLocation(ref LocationSearch search, bool logGazEvent, string sessionId)
        {
            TDPLocation location = null;

            // Perform cache/gazetteer search if 
            // - Search text exists (javascript or non-javascript entered location)
            // - Search id, search type, and js flag (javascript or landing page location)
            // If neither of the above, then no point resolving - it will become unspecified
            if (!string.IsNullOrEmpty(search.SearchText)
                || (!string.IsNullOrEmpty(search.SearchId) && search.SearchType != TDPLocationType.Unknown && search.JavascriptEnabled))
            {
                DateTime submitted = DateTime.Now;
                GazetteerEventCategory gazEventCategory = LocationServiceHelper.GetGazetteerEventCategory(search.SearchType);

                switch (search.SearchType)
                {
                    case TDPLocationType.Venue:
                        {
                            // Find the venue location from  the cache
                            location = GetTDPLocation(search.SearchId, search.SearchType);
                        }
                        break;

                    case TDPLocationType.Station:
                    case TDPLocationType.StationAirport:
                    case TDPLocationType.StationCoach:
                    case TDPLocationType.StationFerry:
                    case TDPLocationType.StationRail:
                    case TDPLocationType.StationTMU:
                        {
                            // Naptan location, resolved using Naptan cache
                            location = LocationServiceHelper.PopulateNaPTANLocation(search);
                        }
                        break;
                    case TDPLocationType.StationGroup:
                        {
                            // Group location, no gaz query needs to be done, can populate location object
                            // just using the group naptan(s)

                            // Find the group location from the cache, which will contain the naptans for the group
                            TDPLocation groupLocation = GetTDPLocation(search.SearchId, search.SearchType);

                            location = LocationServiceHelper.PopulateGroupLocation(search, groupLocation);
                        }
                        break;
                    case TDPLocationType.Locality:
                        {
                            // Locality location, resolved using Gaz search

                            // Find the locality location from the cache, this will contain the locality name 
                            // which may be needed if attempting to resolve locality location with locality id
                            // only (e.g. for a page landing location)
                            TDPLocation localityLocation = GetTDPLocation(search.SearchId, search.SearchType);

                            location = LocationServiceHelper.PopulateLocalityLocation(ref search, localityLocation, sessionId);

                            logGazEvent = false; // Gazetteer will log event
                        }
                        break;
                    case TDPLocationType.Postcode:
                    case TDPLocationType.Address:
                        {
                            // Postcode/Address location, resolved using Gaz search
                            location = LocationServiceHelper.PopulateAddressPostcodeLocation(ref search, sessionId);

                            logGazEvent = false; // Gazetteer will log event
                        }
                        break;
                    case TDPLocationType.POI:
                        {
                            TDPLocation poiLocation = GetTDPLocation(search.SearchId, search.SearchType);

                            location = LocationServiceHelper.PopulateCoordinateLocation(search, poiLocation);
                        }
                        break;
                    case TDPLocationType.CoordinateEN:
                    case TDPLocationType.CoordinateLL:
                        {
                            location = LocationServiceHelper.PopulateCoordinateLocation(search.SearchText, search.GridReference);
                        }
                        break;
                    case TDPLocationType.Unknown:
                    default:
                        {
                            #region Amibiguity cache search

                            // No searchType (and searchId), hence not selected from the auto-suggest dropdown,
                            // so perform an unknown location search (which matches on location description/display name)
                            // and then an ambiguity search if no exact location found using the location cache
                            TDPLocation foundLocation = GetTDPLocation(search.SearchText, TDPLocationType.Unknown);

                            if (foundLocation != null)
                            {
                                List<TDPLocationType> allowedTypes = new List<TDPLocationType>();
                                allowedTypes.Add(TDPLocationType.StationAirport);
                                allowedTypes.Add(TDPLocationType.StationCoach);
                                allowedTypes.Add(TDPLocationType.StationFerry);
                                allowedTypes.Add(TDPLocationType.StationRail);
                                allowedTypes.Add(TDPLocationType.StationTMU);
                                allowedTypes.Add(TDPLocationType.StationGroup);
                                allowedTypes.Add(TDPLocationType.Locality);
                                allowedTypes.Add(TDPLocationType.POI);
                                
                                // If location type is a station/group/locality, then recurse to resolve it,
                                // otherwise fall into ambiguity search
                                if (allowedTypes.Contains(foundLocation.TypeOfLocationActual))
                                {
                                    // Update search for this location
                                    search.SearchText = foundLocation.DisplayName;
                                    search.SearchId = foundLocation.ID;
                                    search.SearchType = foundLocation.TypeOfLocationActual;

                                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                                        string.Format("Found unknown location in cache, resolving location with Search[{0}]", search.ToString())));

                                    location = ResolveLocation(ref search, false, sessionId);
                                }
                                else
                                {
                                    // Reset location so ambiguity search is initiated below
                                    foundLocation = null;
                                }
                            }

                            // Ambiguity search
                            if (foundLocation == null)
                            {
                                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, 
                                    string.Format("No valid locations found in the location cache for[{0}], performing ambiguity search.", search.SearchText)));

                                List<TDPLocation> locations = GetAlternateLocations(search.SearchText);

                                // Update search with location objects
                                search.LocationCacheResults.AddRange(locations);
                            }

                            #endregion
                        }
                        break;
                }

                if (logGazEvent)
                    LocationServiceHelper.LogGazetteerEvent(gazEventCategory, submitted, sessionId);
            }

            return location;
        }

        /// <summary>
        /// Resolves a stop location by adding stop code information to the location
        /// </summary>
        /// <param name="location">Location object to resolve into a TDPStopLocation</param>
        /// <returns>Null if location cannot be resolved into a TDPStopLocation</returns>
        public TDPStopLocation ResolveStopLocation(TDPLocation location)
        {
            TDPStopLocation stopLocation = null;

            if (location != null)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                        string.Format("Updating location[{0}] as stop location", location.ID)));

                // Stop location will only populate code for certain types, update switch statement below as required

                // Copy the base location into the child TDPStopLocation to allow populating of code types
                stopLocation = (TDPStopLocation)location.Clone(stopLocation);

                #region Identify stop type using GIS

                string stopType = string.Empty;

                try
                {
                    // Determine the stop type to allow the CRS/SMS/IATA code to be populated 
                    GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                    QuerySchema gisResult = gisQuery.FindStopsInfoForStops(new string[] { location.Naptan[0] });

                    for (int i = 0; i < gisResult.Stops.Rows.Count; i++)
                    {
                        QuerySchema.StopsRow row = (QuerySchema.StopsRow)gisResult.Stops.Rows[i];

                        stopType = row.stoptype;
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error performing GISQuery call to FindStopsInfoForStops for naptan[{0}] when attempting to populate TDPStopLocation. Message: {1}", location.Naptan[0], ex.Message);

                    throw new TDPException(message, false, TDPExceptionIdentifier.LSGISQueryCallError, ex);
                }

                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose,
                    string.Format("Location[{0}] has stop type[{1}]", location.ID, stopType)));

                #endregion

                #region Populate stop code based on stop type

                switch (stopType)
                {
                    case "AIR":	// Air
                    case "GAT":
                        // Populate IATA code
                        stopLocation.CodeIATA = LocationServiceHelper.FindIATACode(location.Naptan);
                        stopLocation.CodeType = TDPCodeType.IATA;
                        break;

                    case "BCE":	// coach
                    case "BST":
                        // Populate SMS code
                        stopLocation.CodeSMS = LocationServiceHelper.FindSMSCode(location.Naptan);
                        stopLocation.CodeType = TDPCodeType.SMS;
                        break;

                    case "BCQ":
                    case "BCT":	// Bus
                    case "BCS":
                        // Populate SMS code
                        stopLocation.CodeSMS = LocationServiceHelper.FindSMSCode(location.Naptan);
                        stopLocation.CodeType = TDPCodeType.SMS;
                        break;

                    case "RLY":	// Rail
                    case "RPL":
                    case "RSE":
                        // Populate CRS code
                        stopLocation.CodeCRS = LocationServiceHelper.FindCRSCode(location.Naptan);
                        stopLocation.CodeType = TDPCodeType.CRS;
                        break;

                    case "MET":	// Light/rail
                    case "PLT":
                    case "TMU":

                    case "TXR":	// Taxi
                    case "STR":

                    case "FER":	// Ferry
                    case "FTD":

                    default:

                        // No code to populate, update the code type
                        switch (location.TypeOfLocationActual)
                        {
                            case TDPLocationType.Postcode:
                                stopLocation.CodeType = TDPCodeType.Postcode;
                                break;
                            case TDPLocationType.Station:
                            case TDPLocationType.StationAirport:
                            case TDPLocationType.StationCoach:
                            case TDPLocationType.StationFerry:
                            case TDPLocationType.StationRail:
                            case TDPLocationType.StationTMU:
                                stopLocation.CodeType = TDPCodeType.NAPTAN;
                                break;
                            default:
                                stopLocation.CodeType = TDPCodeType.Unknown;
                            break;
                        }
                        break;
                }

                #endregion
            }

            return stopLocation;
        }

        #region TDPLocationCache - Private methods

        /// <summary>
        /// Searches for a TDPLocation in the cache by matching the supplied searchString.
        /// If location type is Venue, it restricts the search to Venues.
        /// </summary>
        /// <returns>TDPLocation or null if no match found</returns>
        private TDPLocation GetTDPLocation(string searchString, TDPLocationType locType)
        {
            TDPLocation location = null;
            switch (locType)
            {
                case TDPLocationType.Venue:
                    location = TDPVenueLocationCache.GetVenueLocation(searchString);
                    break;
                case TDPLocationType.Station:
                case TDPLocationType.StationAirport:
                case TDPLocationType.StationCoach:
                case TDPLocationType.StationFerry:
                case TDPLocationType.StationRail:
                case TDPLocationType.StationTMU:
                    location = TDPLocationCache.GetNaptanLocation(searchString);
                    break;
                case TDPLocationType.StationGroup:
                    location = TDPLocationCache.GetGroupLocation(searchString);
                    break;
                case TDPLocationType.Locality:
                    location = TDPLocationCache.GetLocalityLocation(searchString);
                    break;
                case TDPLocationType.Postcode:
                    location = TDPLocationCache.GetPostcodeLocation(searchString);
                    break;
                case TDPLocationType.POI:
                    location = TDPLocationCache.GetPOILocation(searchString);
                    break;
                case TDPLocationType.Unknown:
                    location = TDPLocationCache.GetUnknownLocation(searchString);
                    break;
            }
            return location;
        }

        /// <summary>
        /// Searches for TDLocations in the cache that are a close match to the supplied search string.
        /// Venue locations are not returned
        /// </summary>
        /// <returns>List<TDPLocation></returns>
        private List<TDPLocation> GetAlternateLocations(string searchString)
        {
            List<TDPLocation> alternateLocations = TDPLocationCache.GetAlternativeTDPLocations(searchString);
            return alternateLocations;
        }

        #endregion

        #region TDPVenueLocationCache

        /// <summary>
        /// Returns the complete list of Olympic Venues as a List of Locations containing Displayable Name, Type and NaPTAN.
        /// </summary>
        /// <returns>List<TDPLocation></returns>
        public List<TDPLocation> GetTDPVenueLocations()
        {
            List<TDPLocation> venueLocations = TDPVenueLocationCache.GetVenuesLocations();
            return venueLocations;
        }

        /// <summary>
        /// Returns the Cycle Park for the cycle park Id
        /// </summary>
        /// <param name="cycleParkId">Cycle park Id</param>
        /// <returns></returns>
        public TDPVenueCyclePark GetTDPVenueCyclePark(string cycleParkId)
        {
            return TDPVenueLocationCache.GetTDPVenueCyclePark(cycleParkId);
        }

        /// <summary>
        /// Returns all the Cycle Parks associated with the specified venue
        /// </summary>
        /// <param name="venueNaPTAN">venue NaPTAN</param>
        /// <returns></returns>
        public List<TDPVenueCyclePark> GetTDPVenueCycleParks(List<string> venueNaPTANs)
        {
            return TDPVenueLocationCache.GetVenueCycleParks(venueNaPTANs);
        }

        /// <summary>
        /// Overloaded. Returns all the Cycle Parks associated with the specified venue
        /// </summary>
        /// <param name="venueNaPTANs"></param>
        /// <param name="outwardDate"></param>
        /// <param name="returnDate"></param>
        /// <returns></returns>
        public List<TDPVenueCyclePark> GetTDPVenueCycleParks(List<string> venueNaPTANs, DateTime outwardDate, DateTime returnDate)
        {
            List<TDPVenueCyclePark> cycleParks = GetTDPVenueCycleParks(venueNaPTANs);
            
            return FilterCycleParks(cycleParks, outwardDate, returnDate);
        }

        /// <summary>
        /// Returns the Car Park for the car park Id
        /// </summary>
        /// <param name="carParkId">Car park Id</param>
        /// <returns></returns>
        public TDPVenueCarPark GetTDPVenueCarPark(string carParkId)
        {
            return TDPVenueLocationCache.GetTDPVenueCarPark(carParkId);
        }

        /// <summary>
        /// Returns all the Car Parks associated with the specified venue
        /// </summary>
        /// <param name="venueNaPTAN">venue NaPTAN(s)</param>
        /// <returns></returns>
        public List<TDPVenueCarPark> GetTDPVenueCarParks(List<string> venueNaPTANs)
        {
            List<TDPVenueCarPark> carParks = TDPVenueLocationCache.GetVenueCarParks(venueNaPTANs);

            if (carParks != null)
            {
                // Keep only car parks which have spaces
                carParks = carParks.Where(acp => acp.CarSpaces > 0).ToList();
            }

            return carParks;
        }
        
        /// <summary>
        /// Returns all the Car Parks associated with the specified venue valid for the dates provided
        /// </summary>
        /// <param name="venueNaPTAN">venue NaPTAN</param>
        /// <returns></returns>
        public List<TDPVenueCarPark> GetTDPVenueCarParks(List<string> venueNaPTANs, DateTime outwardDate, DateTime returnDate)
        {
            // Get all car parks for naptans
            List<TDPVenueCarPark> carParks = GetTDPVenueCarParks(venueNaPTANs);

            // Remove any car parks not valid on dates provided and which don't have any spaces
            return FilterCarParks(carParks, outwardDate, returnDate);
        }

        /// <summary>
        /// Returns all the Blue Badge Car Parks associated with the specified venue
        /// </summary>
        /// <param name="venueNaPTAN">venue NaPTAN</param>
        /// <returns></returns>
        public List<TDPVenueCarPark> GetTDPVenueBlueBadgeCarParks(List<string> venueNaPTANs)
        {
            List<TDPVenueCarPark> carParks = TDPVenueLocationCache.GetVenueCarParks(venueNaPTANs);

            if (carParks != null)
            {
                // Keep only car parks which have BlueBadge or Disabled spaces
                carParks = carParks.Where(acp => (acp.BlueBadgeSpaces + acp.DisabledSpaces) > 0).ToList();
            }

            return carParks;
        }

        /// <summary>
        /// Returns all the Blue Badge Car Parks associated with the specified venue valid for the dates provided
        /// </summary>
        /// <param name="venueNaPTAN">venue NaPTAN</param>
        /// <returns></returns>
        public List<TDPVenueCarPark> GetTDPVenueBlueBadgeCarParks(List<string> venueNaPTANs, DateTime outwardDate, DateTime returnDate)
        {
            // Get all car parks for naptans
            List<TDPVenueCarPark> carParks = GetTDPVenueBlueBadgeCarParks(venueNaPTANs);

            if (carParks != null)
            {
                // Remove any car parks not valid on dates provided
                carParks = FilterCarParks(carParks, outwardDate, returnDate);
            }

            return carParks;
        }

        /// <summary>
        /// Returns all the river services associated with the specified venue
        /// </summary>
        /// <param name="venueNaPTANs"></param>
        /// <returns></returns>
        public List<TDPVenueRiverService> GetTDPVenueRiverServices(List<string> venueNaPTANs)
        {
            // Get all river services for naptans
            List<TDPVenueRiverService> riverServices = TDPVenueLocationCache.GetVenueRiverServices(venueNaPTANs.FirstOrDefault());
            
            return riverServices;
        }

        /// <summary>
        /// Returns all the pier navigation paths associated with the specified venue
        /// </summary>
        /// <param name="venueNaPTANs"></param>
        /// <returns></returns>
        public List<TDPPierVenueNavigationPath> GetTDPVenuePierNavigationPaths(List<string> venueNaPTANs)
        {
            // Get all pier navigation paths for naptans
            List<TDPPierVenueNavigationPath> pierNavigationPaths = TDPVenueLocationCache.GetVenuePierNavigationPaths(venueNaPTANs.FirstOrDefault());

            return pierNavigationPaths;
        }

        /// <summary>
        /// Returns a pier navigation path associated with the specified venue and venue pier
        /// </summary>
        /// <param name="venueNaPTANs"></param>
        /// <returns></returns>
        public TDPPierVenueNavigationPath GetTDPVenuePierNavigationPaths(List<string> venueNaPTANs, string venuePierNaPTAN, bool isToVenue)
        {
            TDPPierVenueNavigationPath navigationPath = null;

            if ((venueNaPTANs != null) && (!string.IsNullOrEmpty(venuePierNaPTAN)))
            {
                // Get the navigation paths for venue
                List<TDPPierVenueNavigationPath> navigationPaths = GetTDPVenuePierNavigationPaths(venueNaPTANs);

                if (navigationPaths != null)
                {
                    // Find the required navigation path,
                    // check by looking at the path From or To value
                    foreach (TDPPierVenueNavigationPath np in navigationPaths)
                    {
                        // Going to the venue, find the path from the venue's pier
                        if (isToVenue
                            && np.FromNaPTAN.Equals(venuePierNaPTAN))
                        {
                            navigationPath = np;
                            break;
                        }
                        // Coming from the venue, find the path to the venue's pier
                        else if (!isToVenue
                            && np.ToNaPTAN.Equals(venuePierNaPTAN))
                        {
                            navigationPath = np;
                            break;
                        }
                    }
                }
            }

            return navigationPath;
        }

        /// <summary>
        /// Returns the Venue Gate for the supplied venue gate NaPTAN ID
        /// </summary>
        /// <param name="venueGateNaPTAN">Venue Gate NaPTAN</param>
        /// <returns></returns>
        public TDPVenueGate GetTDPVenueGate(string venueGateNaPTAN)
        {
            return TDPVenueLocationCache.GetTDPVenueGate(venueGateNaPTAN);
        }

        /// <summary>
        /// Returns all the Check Constraints associated with the specified venue gate
        /// </summary>
        /// <param name="venueGateNaPTANs">venue gate NaPTANs</param>
        /// <returns></returns>
        public List<TDPVenueGateCheckConstraint> GetTDPVenueGateCheckConstraints(List<string> venueGateNaPTANs)
        {
            List<TDPVenueGateCheckConstraint> checkConstraints = TDPVenueLocationCache.GetVenueGateCheckConstraints(venueGateNaPTANs.FirstOrDefault());
            return checkConstraints;
        }

        /// <summary>
        /// Returns a Check Constraint associated with the specified venue gate
        /// </summary>
        /// <param name="venueGate">venue gate</param>
        /// <param name="isVenueEntry">Is check constraint for entry or exit of venue</param>
        /// <returns></returns>
        public TDPVenueGateCheckConstraint GetTDPVenueGateCheckConstraints(TDPVenueGate venueGate, bool isVenueEntry)
        {
            TDPVenueGateCheckConstraint venueGateCheckConstraint = null;

            if (venueGate != null)
            {
                List<string> gateNaptans = new List<string>();
                gateNaptans.Add(venueGate.GateNaPTAN);

                List<TDPVenueGateCheckConstraint> vgccs = GetTDPVenueGateCheckConstraints(gateNaptans);

                // Return the correct gate check constraint
                if (vgccs != null)
                {
                    foreach (TDPVenueGateCheckConstraint vgcc in vgccs)
                    {
                        // Assumes there is only one "IsEntry" check constraint for a gate
                        if (vgcc.IsEntry == isVenueEntry)
                        {
                            venueGateCheckConstraint = vgcc;
                            break;
                        }
                    }
                }
            }

            return venueGateCheckConstraint;
        }
                
        /// <summary>
        /// Returns all the Navigation Paths associated with the specified venue gate
        /// </summary>
        /// <param name="venueGateNaPTANs">venue gate NaPTANs</param>
        /// <returns></returns>
        public List<TDPVenueGateNavigationPath> GetTDPVenueGateNavigationPaths(List<string> venueGateNaPTANs)
        {
            List<TDPVenueGateNavigationPath> navigationPaths = TDPVenueLocationCache.GetVenueGateNavigationPaths(venueGateNaPTANs.FirstOrDefault());
            return navigationPaths;
        }

        /// <summary>
        /// Returns a Navigation Path associated with the specified venue and venue gate
        /// </summary>
        /// <returns></returns>
        public TDPVenueGateNavigationPath GetTDPVenueGateNavigationPaths(TDPLocation venue, TDPVenueGate venueGate, bool isToVenue)
        {
            TDPVenueGateNavigationPath venueGateNavigationPath = null;

            if ((venue is TDPVenueLocation) && (venueGate != null))
            {
                TDPVenueLocation venueLocation = (TDPVenueLocation)venue;

                List<string> gateNaptans = new List<string>();
                gateNaptans.Add(venueGate.GateNaPTAN);

                List<TDPVenueGateNavigationPath> vgnps = GetTDPVenueGateNavigationPaths(gateNaptans);

                // Return the correct gate navigation path
                if (vgnps != null)
                {
                    string fromNaptan = isToVenue ? venueGate.GateNaPTAN : venueLocation.Naptan[0];
                    string toNaptan = isToVenue ? venueLocation.Naptan[0] : venueGate.GateNaPTAN;

                    foreach (TDPVenueGateNavigationPath vgnp in vgnps)
                    {
                        // Assumes there is only one navigation path for the from/to venue gate naptan
                        if ((vgnp.FromNaPTAN.Equals(fromNaptan)) &&
                            (vgnp.ToNaPTAN.Equals(toNaptan)))
                        {
                            venueGateNavigationPath = vgnp;
                            break;
                        }
                    }
                }
            }

            return venueGateNavigationPath;
        }

        /// <summary>
        /// Returns the list of TDPVenueAccess containing stations to use for the specified venue naptan and datetime
        /// </summary>
        public List<TDPVenueAccess> GetTDPVenueAccessData(List<string> venueNaPTANs, DateTime datetime)
        {
            List<TDPVenueAccess> venueAccessData = TDPVenueLocationCache.GetVenueAccessData(venueNaPTANs.FirstOrDefault());

            return FilterVenueAccessData(venueAccessData, datetime);
        }

        #endregion

        #region TDPGNATLocationCache

        /// <summary>
        /// Returns the complete list of GNAT stations as a List of TDPGNATLocations containing Displayable Name, Type, NaPTAN and accesibilitty attributes.
        /// </summary>
        /// <returns>Dictionary<Name,NaPTAN></returns>
        public List<TDPGNATLocation> GetGNATLocations()
        {
            List<TDPGNATLocation> venueLocations = TDPGNATLocationCache.GetGNATList();
            return venueLocations;
        }
        
        /// <summary>
        /// Checks whether a Naptan is usable with the accesibility options supplied
        /// </summary>
        /// <returns>bool<Name,NaPTAN></returns>
        public bool IsGNAT(string naptan, bool stepFree, bool assistanceRequired)
        {
            bool isValid = TDPGNATLocationCache.IsGNAT(naptan,stepFree,assistanceRequired);
            return isValid;
        }

        /// <summary>
        /// Checks whether the admin area and district code are found in the GNAT admin areas list 
        /// and has the required GNAT attributes
        /// </summary>
        /// <param name="adminAreaCode">Admin Area code</param>
        /// <param name="districtCode">District code</param>
        /// <param name="stepFreeAccess">Check step free access available in admin area/district</param>
        /// <param name="assistanceAvailable">Check assistance available in admin area/district</param>
        public bool IsGNATAdminArea(int adminAreaCode, int districtCode, bool stepFreeAccess, bool assistanceAvailable)
        {
            return TDPGNATLocationCache.IsGNATAdminArea(adminAreaCode, districtCode, stepFreeAccess, assistanceAvailable);
        }

        #endregion

        #region Accessible queries

        /// <summary>
        /// Performs a GIS query to check if the location coordinate is in an accessible area
        /// </summary>
        /// <param name="location"></param>
        /// <param name="requireWheelchair"></param>
        /// <param name="requireAssistance"></param>
        /// <returns></returns>
        public static bool IsAccessibleLocation(TDPLocation location,
            bool requireWheelchair, bool requireAssistance)
        {
            try
            {
                int searchDistanceMetresStops = 10000;
                int searchDistanceMetresLocalities = 10000;
                bool isWheelchair = false;
                bool isAssistance = false;

                Int32.TryParse(Properties.Current["AccessibleOptions.IsPointAccessible.Stops.SearchDistance.Metres"], out searchDistanceMetresStops);
                Int32.TryParse(Properties.Current["AccessibleOptions.IsPointAccessible.Localities.SearchDistance.Metres"], out searchDistanceMetresLocalities);


                GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                gisQuery.IsPointInAccessibleLocation(location.GridRef.Easting, location.GridRef.Northing,
                    (location.TypeOfLocation == TDPLocationType.Locality) ? searchDistanceMetresLocalities : searchDistanceMetresStops,
                    out isWheelchair, out isAssistance);

                // Return value
                if (requireWheelchair && requireAssistance)
                    return isWheelchair && isAssistance;
                else if (requireWheelchair)
                    return isWheelchair;
                else if (requireAssistance)
                    return isAssistance;

            }
            catch (Exception ex)
            {
                // GIS query error
                string message = string.Format("Error doing GIS query IsPointInAccessibleLocation for location[{0} {1} {2} {3}]. Exception: {4}. {5}",
                    location.ID, location.Name, location.GridRef.Easting, location.GridRef.Northing,
                    ex.Message, ex.StackTrace);
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message));
            }

            return false;
        }

        /// <summary>
        /// Performs a GIS query to FindNearestAccessibleStops
        /// </summary>
        public List<TDPLocation> FindNearestAccessibleStops(float easting, float northing, int searchDistance,
                                        int maxStops, DateTime outwardDateTime, bool requiresStepFreeAccess, 
                                        bool requiresSpecialAssistance, string[] stopTypes)
        {
            try
            {
                GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                AccessibleStopInfo[] accessibleStops = gisQuery.FindNearestAccessibleStops(easting, northing,
                                            searchDistance, maxStops, outwardDateTime, requiresStepFreeAccess,
                                            requiresSpecialAssistance, stopTypes);

                // Create results uing data from the gisQuery
                if (accessibleStops != null)
                {
                    List<TDPLocation> results = new List<TDPLocation>();
                    TDPLocation loc = null;

                    foreach (AccessibleStopInfo asi in accessibleStops)
                    {
                        loc = new TDPLocation();
                        loc.Name = asi.StopName;
                        loc.Accessible = true;
                        loc.TypeOfLocation = TDPLocationTypeHelper.GetTDPLocationTypeForNaPTAN(asi.Atcocode);
                        loc.Naptan = new List<string>();
                        loc.Naptan.Add(asi.Atcocode);
                        loc.DistanceFromSearchOSGR = asi.Distance;

                        results.Add(loc);
                    }

                    return results;
                }
            }
            catch (Exception ex)
            {
                // GIS query error
                string message = string.Format("Error doing GIS query FindNearestAccessibleStops for grid ref[{0} {1}]. Exception: {2}. {3}",
                                            easting, northing, ex.Message, ex.StackTrace);
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message));
            }

            return null;
        }

        /// <summary>
        /// Performs a GIS query to FindNearestAccessibleLocalities
        /// </summary>
        public List<TDPLocation> FindNearestAccessibleLocalities(float easting, float northing, int searchDistanceMetres,
                                        int maxResults, bool requiresStepFreeAccess, bool requiresSpecialAssistance)
        {
            try
            {
                GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                AccessibleLocationInfo[] accessibleLocalities = gisQuery.FindNearestAccessibleLocalities(easting, northing,
                                            searchDistanceMetres, maxResults, requiresStepFreeAccess, requiresSpecialAssistance);

                // Create results uing data from the gisQuery
                if (accessibleLocalities != null)
                {
                    List<TDPLocation> results = new List<TDPLocation>();
                    TDPLocation loc = null;

                    foreach (AccessibleLocationInfo ali in accessibleLocalities)
                    {
                        loc = new TDPLocation();
                        loc.Naptan = new List<string>();
                        loc.Name = ali.LocalityName;
                        loc.Accessible = true;
                        loc.TypeOfLocation = TDPLocationType.Locality;
                        loc.Locality = ali.NationalGazetteerID;
                        loc.DistanceFromSearchOSGR = ali.Distance;

                        results.Add(loc);
                    }

                    return results;
                }
            }
            catch (Exception ex)
            {
                // GIS query error
                string message = string.Format("Error doing GIS query FindNearestAccessibleStops for grid ref[{0} {1}]. Exception: {2}. {3}",
                                            easting, northing, ex.Message, ex.StackTrace);
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message));
            }

            return null;
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Method which loads the location data
        /// </summary>
        private void LoadData()
        {
            // Use static classes to load data
            TDPVenueLocationCache.LoadVenues();
            TDPGNATLocationCache.LoadGNATStations();
            TDPLocationCache.LoadLocations();
            TDPLocationCache.LoadPostcodes();
        }

        #region Car/Cycle park filtering

        /// <summary>
        /// Filters the TDPVenueCarParks to only retain those which are valid for the dates specified 
        /// and those which have spaces
        /// </summary>
        /// <param name="carParks"></param>
        /// <param name="outwardDate"></param>
        /// <param name="returnDate"></param>
        /// <returns></returns>
        private List<TDPVenueCarPark> FilterCarParks(List<TDPVenueCarPark> carParks, DateTime outwardDate, DateTime returnDate)
        {
            if (carParks != null)
            {
                List<TDPVenueCarPark> filteredCarParks = new List<TDPVenueCarPark>();

                bool checkOutwardDate = (outwardDate != DateTime.MinValue);
                bool checkReturnDate = (returnDate != DateTime.MinValue);

                // Temp flag for car park validity
                bool valid = false;

                foreach (TDPVenueCarPark carPark in carParks)
                {
                    #region Validate dates

                    if (carPark.Availability != null)
                    {
                        foreach (TDPParkAvailability availability in carPark.Availability)
                        {
                            // If there is at least one Availability where the requested date is ok, then car park is valid
                            List<DayOfWeek> daysValid = availability.GetDaysOfWeek();

                            valid = IsDateValid(daysValid, availability.FromDate, availability.ToDate, outwardDate, returnDate, checkOutwardDate, checkReturnDate);
                            
                            if (valid)
                                break;
                        }
                    }
                    else
                    {
                        // No avilability details, assume its valid
                        valid = true;
                    }

                    #endregion

                    #region Validate spaces

                    // Check for spaces
                    if ((carPark.CarSpaces 
                        + carPark.BlueBadgeSpaces 
                        + carPark.DisabledSpaces 
                        + carPark.CoachSpaces) <= 0)
                    {
                        valid = false;
                    }

                    #endregion

                    if (valid)
                    {
                        filteredCarParks.Add(carPark);
                    }

                    // Reset flag
                    valid = false;
                }

                if (filteredCarParks.Count > 0)
                {
                    return filteredCarParks;
                }
            }

            return null;
        }

        /// <summary>
        /// Filters the TDPVenueCyclePark to only retain those which are valid for the dates specified
        /// and those which have spaces
        /// </summary>
        /// <param name="cycleParks"></param>
        /// <param name="outwardDate"></param>
        /// <param name="returnDate"></param>
        /// <returns></returns>
        private List<TDPVenueCyclePark> FilterCycleParks(List<TDPVenueCyclePark> cycleParks, DateTime outwardDate, DateTime returnDate)
        {
            if (cycleParks != null)
            {
                List<TDPVenueCyclePark> filteredCycleParks = new List<TDPVenueCyclePark>();

                bool checkOutwardDate = (outwardDate != DateTime.MinValue);
                bool checkReturnDate = (returnDate != DateTime.MinValue);

                // Temp flag for park validity
                bool valid = false;

                foreach (TDPVenueCyclePark cyclePark in cycleParks)
                {
                    #region Validate dates

                    if (cyclePark.Availability != null)
                    {
                        foreach (TDPParkAvailability availability in cyclePark.Availability)
                        {
                            // If there is at least one Availability where the requested date is ok, then park is valid
                            List<DayOfWeek> daysValid = availability.GetDaysOfWeek();
                            
                            TimeSpan overnight = new TimeSpan(3, 0, 0);
                            bool isOvernight = false;

                            if (availability.DailyClosingTime < overnight)
                            {
                                // Journey uses a cycle park that is open into the next morning so subtract a day for 
                                // validation purposes if journey is in the early hours
                                isOvernight = true;
                                if (outwardDate != DateTime.MinValue)
                                {
                                    if (outwardDate.TimeOfDay < overnight)
                                    {
                                        outwardDate.Subtract(new TimeSpan(24, 0, 0));
                                    }
                                }
                                if (returnDate != DateTime.MinValue)
                                {
                                    if (returnDate.TimeOfDay < overnight)
                                    {
                                        returnDate.Subtract(new TimeSpan(24, 0, 0));
                                    }
                                }
                            }
                            
                            valid = IsDateValid(daysValid, availability.FromDate, availability.ToDate, outwardDate, returnDate, checkOutwardDate, checkReturnDate);
                            if (valid)
                            {
                                valid = IsTimeValid(availability.DailyOpeningTime, availability.DailyClosingTime, outwardDate, returnDate, checkOutwardDate, checkReturnDate, isOvernight);
                            }
                                                        
                            if (valid)
                                break;
                        }
                    }
                    else
                    {
                        // No avilability details, assume its valid
                        valid = true;
                    }

                    #endregion

                    #region Validate spaces

                    // Check for spaces
                    if (cyclePark.NumberOfSpaces <= 0)
                    {
                        valid = false;
                    }

                    #endregion

                    if (valid)
                    {
                        filteredCycleParks.Add(cyclePark);
                    }

                    // Reset flag
                    valid = false;
                }

                if (filteredCycleParks.Count > 0)
                {
                    return filteredCycleParks;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the date(s) supplied are valid for From and To date range and DaysOfWeek list
        /// </summary>
        private bool IsDateValid(List<DayOfWeek> daysValid, DateTime fromDate, DateTime toDate,
            DateTime outwardDate, DateTime returnDate, bool checkOutwardDate, bool checkReturnDate)
        {
            bool valid = false;

            // Outward and return journey check
            if (checkOutwardDate && checkReturnDate &&
                fromDate.Date <= outwardDate.Date &&
                toDate.Date >= returnDate.Date)
            {
                if (daysValid.Contains(outwardDate.DayOfWeek) &&
                    daysValid.Contains(returnDate.DayOfWeek))
                {
                    valid = true;
                }
            }
            // Outward only journey check
            else if (checkOutwardDate && !checkReturnDate && fromDate.Date <= outwardDate.Date && toDate.Date >= outwardDate.Date)
            {
                if (daysValid.Contains(outwardDate.DayOfWeek))
                {
                    valid = true;
                }
            }
            // Return only journey check (shouldn't ever reach here, as you can't do return only journey!)
            else if (!checkOutwardDate && checkReturnDate && toDate.Date >= returnDate.Date && fromDate.Date <= returnDate.Date)
            {
                if (daysValid.Contains(returnDate.DayOfWeek))
                {
                    valid = true;
                }
            }
            else if (!checkOutwardDate && !checkReturnDate)
            {
                // Dates supplied are invalid, so assume park is valid
                valid = true;
            }

            return valid;
        }

        /// <summary>
        /// Checks if the time(s) supplied are valid against the opening and closing times
        /// </summary>
        private bool IsTimeValid(TimeSpan openingTime, TimeSpan closingTime, DateTime outwardDate, 
            DateTime returnDate, bool checkOutwardDate, bool checkReturnDate, bool overnight)
        {
            bool outwardOK = false;
            bool returnOK = false;
            TimeSpan overnightRollover = new TimeSpan(3, 0, 0);

            if (checkOutwardDate)
            {
                if ((openingTime <= outwardDate.TimeOfDay && closingTime >= outwardDate.TimeOfDay)
                    || (overnight && outwardDate.TimeOfDay <= overnightRollover)
                    || (overnight && outwardDate.TimeOfDay >= openingTime))
                {
                    outwardOK = true;
                }
            }
            else outwardOK = true;

            if (checkReturnDate)
            {
                if (openingTime <= returnDate.TimeOfDay && closingTime >= returnDate.TimeOfDay
                    || (overnight && returnDate.TimeOfDay <= overnightRollover)
                    || (overnight && returnDate.TimeOfDay >= openingTime))
                {
                    returnOK = true;
                }
            }
            else returnOK = true;

            if (outwardOK && returnOK) return true;
            else return false;
        }

        #endregion

        #region Venue Access filtering

        /// <summary>
        /// Filters the TDPVenueAccess to only retain those which are valid for the datetime specified 
        /// </summary>
        /// <returns></returns>
        private List<TDPVenueAccess> FilterVenueAccessData(List<TDPVenueAccess> venueAccessData, DateTime datetime)
        {
            if (venueAccessData != null)
            {
                List<TDPVenueAccess> filteredVenueAccess = new List<TDPVenueAccess>();

                bool checkOutwardDate = (datetime != DateTime.MinValue);

                // Temp flag for validity
                bool valid = false;

                foreach (TDPVenueAccess va in venueAccessData)
                {
                    #region Validate dates

                    if (va.AccessFrom <= datetime && va.AccessTo >= datetime)
                    {
                        valid = true;
                    }
                    
                    #endregion
                    
                    if (valid)
                    {
                        filteredVenueAccess.Add(va);
                    }

                    // Reset flag
                    valid = false;
                }

                if (filteredVenueAccess.Count > 0)
                {
                    return filteredVenueAccess;
                }
            }

            return null;
        }


        #endregion

        #endregion
    }
}
