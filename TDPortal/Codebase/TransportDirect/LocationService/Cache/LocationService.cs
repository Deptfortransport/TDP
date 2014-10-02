// *********************************************** 
// NAME                 : LocationService.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 20/08/2012 
// DESCRIPTION          : LocationService class to cache and return locations
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/Cache/LocationService.cs-arc  $ 
//
//   Rev 1.9   Jan 04 2013 15:33:36   mmodi
//Return no accessible locations for invalid stop types list
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.8   Dec 06 2012 09:10:14   mmodi
//Updated to not use static accessible locations cache class
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Dec 05 2012 13:34:30   mmodi
//Updated to remove static location cache and use an instance 
//Resolution for 5877: IIS Recycle issue
//
//   Rev 1.6   Oct 04 2012 14:22:26   mmodi
//Added error handling around gis calls
//Resolution for 5857: Gaz - Code review updates
//
//   Rev 1.5   Oct 04 2012 13:52:04   mmodi
//Tidied up method summary comments
//Resolution for 5857: Gaz - Code review updates
//
//   Rev 1.4   Oct 03 2012 11:20:44   mmodi
//Removed check for child localities when resolving a locality location
//Resolution for 5855: Gaz - Locality auto-suggest location displays ambiguity page
//
//   Rev 1.3   Sep 03 2012 11:11:24   mmodi
//Removed commented out code
//Resolution for 5832: CCN0668 Gazetteer Enhancements - Auto-Suggest drop downs
//
//   Rev 1.2   Aug 31 2012 14:23:58   mmodi
//Allow searching for a location using an enterned naptan or locality
//Resolution for 5832: CCN0668 Gazetteer Enhancements - Auto-Suggest drop downs
//
//   Rev 1.1   Aug 29 2012 12:11:48   mmodi
//Corrected use of short List notation because csc.exe complained during build
//Resolution for 5832: CCN0668 Gazetteer Enhancements - Auto-Suggest drop downs
//
//   Rev 1.0   Aug 28 2012 10:45:28   mmodi
//Initial revision.
//Resolution for 5832: CCN Gaz

using System;
using System.Collections.Generic;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using Logger = System.Diagnostics.Trace;
using System.Collections.ObjectModel;

namespace TransportDirect.UserPortal.LocationService.Cache
{
    /// <summary>
    /// LocationService class to cache and return locations
    /// </summary>
    public class LocationService
    {
        #region Private members

        private TDLocationCache tdLocationCache = null;
        private TDLocationAccessibleCache tdLocationAccessibleCache = null;

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
        /// Returns the version of the locations cache. This value is used to with the 
        /// locations javascript files, to ensure the cache and js are from the same 
        /// dataset
        /// </summary>
        public string LocationVersion()
        {
            return tdLocationCache.GetVersion();
        }

        /// <summary>
        /// Resolves a location returned from the location cache into the ref location object
        /// </summary>
        public void ResolveLocation(ref LocationSearch search, ref TDLocation location,
            string locationId, TDStopType locationType, StationType stationType,
            int maxWalkingTime, int walkingSpeed, 
            bool logGazEvent, string sessionId, bool loggedOn)
        {
            if (location == null)
                location = new TDLocation();

            // If no search text entered, then no point resolving - it will be unspecified
            if (!string.IsNullOrEmpty(search.InputText))
            {
                if (logGazEvent)
                    LogGazetteerEvent(locationType, sessionId, loggedOn);

                switch (locationType)
                {
                    case TDStopType.Air:
                    case TDStopType.Bus:
                    case TDStopType.Coach:
                    case TDStopType.Ferry:
                    case TDStopType.LightRail:
                    case TDStopType.Rail:
                        {
                            // Naptan location, no gaz query needs to be done, can populate location object
                            // just using the naptan(s);
                            PopulateNaPTANLocation(ref search, ref location, locationId, locationType, stationType);
                        }
                        break;
                    case TDStopType.Group:
                        {
                            // Group location, no gaz query needs to be done, can populate location object
                            // just using the group naptan(s)
                            PopulateGroupLocation(ref search, ref location, locationId, locationType, stationType);
                        }
                        break;
                    case TDStopType.Locality:
                        {
                            // Locality location, resolved using Gaz search
                            PopulateLocalityLocation(ref search, ref location, locationId, locationType, stationType,
                                maxWalkingTime, walkingSpeed, sessionId, loggedOn);
                        }
                        break;
                    case TDStopType.POI:
                        {
                            // Coordinate location type
                            TDLocation foundLocation = GetTDLocation(search.InputText, TDStopType.POI);
                            PopulateCoordinateLocation(ref search, ref location, locationId, locationType, stationType, foundLocation);
                        }
                        break;
                    case TDStopType.Unknown:
                    default:
                        {
                            // No stoptype/locationId, hence not selected from the auto-suggest dropdown,
                            // so perform an unknown location search (which matches on location description/display name)
                            // and then an ambiguity search if no location found using the location cache
                            TDLocation foundLocation = GetTDLocation(search.InputText, TDStopType.Unknown);

                            if (foundLocation != null)
                            {
                                List<TDStopType> allowedTypes = new List<TDStopType>();
                                allowedTypes.Add(TDStopType.Air);
                                allowedTypes.Add(TDStopType.Bus);
                                allowedTypes.Add(TDStopType.Coach);
                                allowedTypes.Add(TDStopType.Ferry);
                                allowedTypes.Add(TDStopType.Group);
                                allowedTypes.Add(TDStopType.LightRail);
                                allowedTypes.Add(TDStopType.Rail);
                                allowedTypes.Add(TDStopType.Locality);
                                allowedTypes.Add(TDStopType.POI);

                                // If location type is a station/group/locality, then recurse to resolve it,
                                // otherwise fall into ambiguity search
                                if (allowedTypes.Contains(foundLocation.StopType))
                                {
                                    // Update search and location
                                    search.ClearSearch();
                                    search.SearchType = SearchTypeHelper.GetSearchType(foundLocation.StopType);

                                    // Ensure search text is now this location
                                    search.InputText = foundLocation.Description;

                                    location.ClearAll();

                                    ResolveLocation(ref search, ref location, foundLocation.ID, foundLocation.StopType, stationType,
                                        maxWalkingTime, walkingSpeed, false, sessionId, loggedOn);
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
                                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, string.Format("No valid locations found in the location cache for[{0}], performing ambiguity search.", search.InputText)));

                                List<TDLocation> locations = GetAlternateLocations(search.InputText);

                                // Update search and location objects
                                search.ClearSearch();
                                search.AddAmbiguitySearchResult(locations);
                                
                                location.ClearAll();
                                location.Status = TDLocationStatus.Ambiguous;
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Returns a read only collection of Accessible locations
        /// </summary>
        /// <param name="stopTypes">TDStopType to filter locations on</param>
        /// <returns></returns>
        public ReadOnlyCollection<TDLocationAccessible> GetAccessibleLocations(List<TDStopType> stopTypes)
        {
            List<TDLocationAccessible> accessibleLocations = new List<TDLocationAccessible>();

            if (stopTypes == null || stopTypes.Count == 0)
            {
                // Return none
            }
            else
            {
                // Filter on stop type
                accessibleLocations = tdLocationAccessibleCache.GetAccessibleList().FindAll(
                        delegate(TDLocationAccessible loc)
                        {
                            return stopTypes.Contains(loc.StopType);
                        });
            }

            return accessibleLocations.AsReadOnly();
        }

        /// <summary>
        /// Checks whether a location is usable with the accesibility options supplied
        /// </summary>
        /// <returns>bool<Name,NaPTAN></returns>
        public bool IsAccessibleLocation(string naptan, bool requireStepFreeAccess, bool requireSpecialAssistance)
        {
            return tdLocationAccessibleCache.IsAccessible(naptan, requireStepFreeAccess, requireSpecialAssistance);
        }

        /// <summary>
        /// Checks whether the admin area and district code are found in the Accessible admin areas list 
        /// and has the required accessible attributes
        /// </summary>
        /// <param name="adminAreaCode">Admin Area code</param>
        /// <param name="districtCode">District code</param>
        /// <param name="requireStepFreeAccess">Check step free access available in admin area/district</param>
        /// <param name="requireSpecialAssistance">Check assistance available in admin area/district</param>
        public bool IsAccessibleAdminArea(string adminAreaCode, string districtCode, bool requireStepFreeAccess, bool requireSpecialAssistance)
        {
            return tdLocationAccessibleCache.IsAccessibleAdminArea(adminAreaCode, districtCode, requireStepFreeAccess, requireSpecialAssistance);
        }

        #endregion

        #region Private methods

        #region TDLocationCache

        /// <summary>
        /// Method which loads the location data
        /// </summary>
        private void LoadData()
        {
            // Create new location cache
            tdLocationCache = new TDLocationCache();
            tdLocationAccessibleCache = new TDLocationAccessibleCache();
        }

        /// <summary>
        /// Searches for an TDLocation by matching the supplied searchString.
        /// </summary>
        /// <returns>TDLocation or null if no match found</returns>
        private TDLocation GetTDLocation(string searchString, TDStopType locType)
        {
            TDLocation location = null;
            switch (locType)
            {
                case TDStopType.Group:
                    location = tdLocationCache.GetGroupLocation(searchString);
                    break;
                case TDStopType.Unknown:
                    location = tdLocationCache.GetUnknownLocation(searchString);
                    break;
                case TDStopType.POI:
                    location = tdLocationCache.GetPOILocation(searchString);
                    break;
            }
            return location;
        }

        /// <summary>
        /// Returns a list of TDLocations for an abiguity search on the string supplied.
        /// </summary>
        private List<TDLocation> GetAlternateLocations(string searchString)
        {
            List<TDLocation> alternateLocations = tdLocationCache.GetAlternativeTDLocations(searchString);
            return alternateLocations;
        }

        #endregion

        #region Location "auto-suggest" methods

        #region Populate

        /// <summary>
        /// Populates a naptan location selected from the auto-suggest list
        /// </summary>
        private void PopulateNaPTANLocation(ref LocationSearch search, ref TDLocation location,
            string locationId, TDStopType locationType, StationType stationType)
        {
            if (!string.IsNullOrEmpty(locationId) && !string.IsNullOrEmpty(search.InputText))
            {
                try
                {
                    location.NaPTANs = PopulateNaptans(locationId.Split(new char[] { ',' }));

                    // Update search and location text to be the auto-suggest picked text
                    location.Description = search.InputText;
                }
                catch (Exception ex)
                {
                    // Unable to populate the Naptans log the information and reset the location and serch
                    string message = string.Format("PopulateNaPTANLocation method failed to populate naptans for {0}. Error: {1} Stack: {2}", locationId, ex.Message, ex.StackTrace);
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, message));

                    location.ClearAll();
                }

                if (location.NaPTANs.Length > 0)
                {
                    OSGridReference osgr = location.NaPTANs[0].GridReference;

                    location.Locality = PopulateLocality(location.NaPTANs, osgr);
                    location.Toid = PopulateToids(osgr, stationType); // Will only be done for "Undetermined" station type
                    location.GridReference = osgr;
                    location.SearchType = SearchType.MainStationAirport;
                    location.RequestPlaceType = RequestPlaceType.NaPTAN;
                    location.StopType = locationType;
                    location.Status = TDLocationStatus.Valid;
                }
            }
        }

        /// <summary>
        /// Populates a coordinate location, point of interest
        /// </summary>
        private void PopulateCoordinateLocation(ref LocationSearch search, ref TDLocation location, 
            string locationId, TDStopType locationType, StationType stationType, TDLocation coordLocation)
        {
            if (!string.IsNullOrEmpty(locationId) && !string.IsNullOrEmpty(search.InputText))
            {
                // Update search and location text to be the auto-suggest picked text
                location.Description = search.InputText;
                location.GridReference = coordLocation.GridReference;
                location.Locality = PopulateLocality(location.NaPTANs, coordLocation.GridReference);
                location.Toid = PopulateToids(coordLocation.GridReference, stationType); // Will only be done for "Undetermined" station type
                location.SearchType = SearchType.POI;
                location.RequestPlaceType = RequestPlaceType.Coordinate;
                location.StopType = locationType;
                location.Status = TDLocationStatus.Valid;
            }
        }

        /// <summary>
        /// Populates a group location selected from the auto-suggest list
        /// </summary>
        private void PopulateGroupLocation(ref LocationSearch search, ref TDLocation location,
            string locationId, TDStopType locationType, StationType stationType)
        {
            if (!string.IsNullOrEmpty(locationId) && !string.IsNullOrEmpty(search.InputText))
            {
                // Find the group location from the cache
                TDLocation groupLocation = GetTDLocation(locationId, TDStopType.Group);

                try
                {
                    // Build the group location
                    if (groupLocation != null)
                    {
                        List<TDNaptan> tdNaptans = new List<TDNaptan>();

                        // Check each group naptan in turn, because one group naptan may no longer exist,
                        // but the others may be valid - so shouldnt fail journey planning
                        foreach (TDNaptan n in groupLocation.NaPTANs)
                        {
                            try
                            {
                                tdNaptans.AddRange(PopulateNaptans(new string[1] { n.Naptan }));
                            }
                            catch (TDException tdEx)
                            {
                                // Any exceptions thrown for invalid naptan, ignore and carry on to next naptan,
                                // otherwise throw and exit
                                if (tdEx.Identifier != TDExceptionIdentifier.LSNaptanCacheLookupNaptanInvalid)
                                {
                                    throw;
                                }
                            }
                        }

                        if (tdNaptans.Count == 0)
                        {
                            // Throw exception, this shouldnt happen as the auto suggest should only contain
                            // group location ids which exist
                            throw new Exception();
                        }

                        location.NaPTANs = tdNaptans.ToArray();

                        // Update search and location text to be the auto-suggest picked text
                        location.Description = search.InputText;
                    }
                }
                catch (Exception ex)
                {
                    // Unable to populate the Naptans log the information and reset the location and serch
                    string message = string.Format("PopulateGroupLocation method failed to populate for group id {0}. Error: {1} Stack: {2}", locationId, ex.Message, ex.StackTrace);
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, message));

                    location.ClearAll();
                }

                if (location.NaPTANs.Length > 0)
                {
                    OSGridReference osgr = new OSGridReference(groupLocation.GridReference.Easting, groupLocation.GridReference.Northing);
                    if (!osgr.IsValid)
                    {
                        string message = string.Format("Group location for id {0} contains an invalid osgr[{1},{2}], instead using osgr of first group naptan[{3}]",
                            locationId, osgr.Easting, osgr.Northing, location.NaPTANs[0].Naptan);
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, message));

                        osgr = location.NaPTANs[0].GridReference;
                    }

                    location.Locality = PopulateLocality(null, osgr);
                    location.Toid = PopulateToids(osgr, stationType); // Will only be done for "Undetermined" station type
                    location.GridReference = osgr;
                    location.SearchType = SearchType.MainStationAirport;
                    location.RequestPlaceType = RequestPlaceType.NaPTAN;
                    location.StopType = locationType;
                    location.Status = TDLocationStatus.Valid;
                }
            }
        }

        /// <summary>
        /// Populates a locality location selected from the auto-suggest list
        /// </summary>
        private void PopulateLocalityLocation(ref LocationSearch search, ref TDLocation location,
            string locationId, TDStopType locationType, StationType stationType,
            int maxWalkingTime, int maxWalkingSpeed, string sessionId, bool loggedOn)
        {
            LocationChoiceList result =
                    search.StartSearch(
                        search.InputText,
                        search.SearchType,
                        false,
                        maxWalkingSpeed * maxWalkingTime,
                        sessionId,
                        loggedOn,
                        stationType);

            if (result.Count == 1)
            {
                LocationChoice lc = (LocationChoice)result[0];

                // Populate the location details
                search.GetLocationDetails(ref location, lc);
                search.InputText = location.Description;

                location.StopType = locationType;
            }
            else if (result.Count > 1)
            {
                // Try to resolve the location by matching the localityId (from the auto-suggest)
                location.Status = TDLocationStatus.Ambiguous;

                #region Try to resolve location by finding locality and/or drilling down

                foreach (LocationChoice lc in result)
                {
                    if (lc.PicklistValue.ToLower().Contains(locationId.ToLower()))
                    {
                        search.GetLocationDetails(ref location, lc);
                        search.InputText = location.Description;

                        location.StopType = locationType;
                    }
                }

                #endregion
            }
            else
            {
                // Shouldnt reach here as the auto-suggest locality should exist
                location.Status = TDLocationStatus.Unspecified;

                // Vague flag and Single word address flag
                if (result.IsVague == true)
                {
                    search.VagueSearch = true;
                }
                else if (result.IsSingleWordAddress == true)
                {
                    search.SingleWord = true;
                }
                else
                {
                    search.VagueSearch = false;
                    search.SingleWord = false;
                }
            }
        }

        #endregion

        #region Populate helpers - Naptans, Locality, Toids

        /// <summary>
        /// Finds the Naptans that are closest to the provided naptan input
        /// </summary>
        private static TDNaptan[] PopulateNaptans(string[] naptan)
        {
            TDNaptan[] naptans = new TDNaptan[naptan.Length];
            NaptanCacheEntry nce = null;
            int i = 0;
            try
            {
                foreach (string tempNaptan in naptan)
                {
                    nce = NaptanLookup.Get(tempNaptan.Trim(), "Naptan");

                    if (nce.Found)
                    {
                        naptans[i] = new TDNaptan();
                        naptans[i].Naptan = nce.Naptan;
                        naptans[i].GridReference = nce.OSGR;
                        naptans[i].Locality = nce.Locality;
                        naptans[i].Name = nce.Description;
                        i++;
                    }
                    else
                    {
                        // If any Naptans are not found, this shouldnt happen because the auto-suggest
                        // list should only contain valid naptans
                        throw new TDException(string.Empty, false, TDExceptionIdentifier.LSNaptanCacheLookupNaptanInvalid);
                    }
                }
            }
            catch (Exception ex) // Catch's any errors from NaptanLookup.Get, e.g. where "ABC" is the naptan submitted
            {
                string message = string.Format("Naptan code not found or invalid Naptan code used [{0}]",
                                (nce != null) ? nce.Naptan : "null");

                // Append exception if exists, because exception could just be a naptan not found from above
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    message = string.Format("{0}. Exception message[{1}], stack[{2}]", message, ex.Message, ex.StackTrace);

                    // Naptan cache handles and logs all "Not found" exceptions, but any other exceptions should 
                    // cascade up to this try catch, so log and throw
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));

                    throw (new TDException(message, true, TDExceptionIdentifier.LSNaptanCacheLookupError));
                }
                else
                {
                    throw (new TDException(message, true, TDExceptionIdentifier.LSNaptanCacheLookupNaptanInvalid));
                }
            }

            return naptans;
        }

        /// <summary>
        /// Populate locality data
        /// </summary>
        private static string PopulateLocality(TDNaptan[] naptans, OSGridReference osgr)
        {
            string locality = string.Empty;

            // Use locality of naptan if availble
            if (naptans != null && naptans.Length > 0)
            {
                foreach (TDNaptan naptan in naptans)
                {
                    if (!string.IsNullOrEmpty(naptan.Locality))
                    {
                        locality = naptan.Locality;
                        break;
                    }
                }
            }

            // Otherwise, use locality from coordinate
            if (string.IsNullOrEmpty(locality))
            {
                try
                {
                    IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                    locality = gisQuery.FindNearestLocality(osgr.Easting, osgr.Northing);
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error performing GISQuery call to FindNearestLocation for coordinate[{0},{1}]. Message: {2}", osgr.Easting, osgr.Northing, ex.Message);

                    throw new TDException(message, false, TDExceptionIdentifier.LSGISQueryCallError, ex);
                }
            }

            return locality;
        }

        /// <summary>
        /// Populate toids data
        /// </summary>
        private static string[] PopulateToids(OSGridReference osgr, StationType stationType)
        {
            string[] toids = new string[0];

            // Only set toids for StationType.Undetermined, as other types
            // will not plan car journeys (for performance)
            if (stationType == StationType.Undetermined || stationType == StationType.UndeterminedNoGroup)
            {
                try
                {
                    IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                    QuerySchema gisResult = gisQuery.FindNearestITN(osgr.Easting, osgr.Northing, string.Empty, true);

                    toids = new string[gisResult.ITN.Rows.Count];

                    for (int i = 0; i < gisResult.ITN.Rows.Count; i++)
                    {
                        QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
                        toids[i] = row.toid;
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error performing GISQuery call to FindNearestITN for coordinate[{0},{1}]. Message: {2}", osgr.Easting, osgr.Northing, ex.Message);

                    throw new TDException(message, false, TDExceptionIdentifier.LSGISQueryCallError, ex);
                }
            }

            return toids;
        }

        #endregion

        #endregion

        #region Log MIS

        /// <summary>
        /// Log location gaz event for MIS
        /// </summary>
        private void LogGazetteerEvent(TDStopType locationType, string sessionId, bool loggedOn)
        {
            // Log a Gaz auto-suggest event
            GazetteerEvent ge = new GazetteerEvent(GetGazetteerEventCategory(locationType),
                DateTime.Now, sessionId, loggedOn);
            Logger.Write(ge);
        }

        /// <summary>
        /// Parses an auto-suggest location type string value into a GazetteerEventCategory
        /// </summary>
        private GazetteerEventCategory GetGazetteerEventCategory(TDStopType locationType)
        {
            switch (locationType)
            {
                case (TDStopType.Air): return GazetteerEventCategory.GazetteerAutoSuggestAirport;
                case (TDStopType.Coach): return GazetteerEventCategory.GazetteerAutoSuggestCoach;
                case (TDStopType.Group): return GazetteerEventCategory.GazetteerAutoSuggestGroup;
                case (TDStopType.Ferry): return GazetteerEventCategory.GazetteerAutoSuggestFerry;
                case (TDStopType.Locality): return GazetteerEventCategory.GazetteerAutoSuggestLocality;
                case (TDStopType.Rail): return GazetteerEventCategory.GazetteerAutoSuggestRail;
                case (TDStopType.LightRail): return GazetteerEventCategory.GazetteerAutoSuggestTMU;
                case (TDStopType.POI): return GazetteerEventCategory.GazetteerAutoSuggestPointOfInterest;
                // Any other strings are ignored and the default returned
            }

            return GazetteerEventCategory.GazetteerAutoSuggestOther;
        }

        #endregion

        #endregion
    }
}