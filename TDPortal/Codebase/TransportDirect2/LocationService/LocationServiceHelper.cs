// *********************************************** 
// NAME             : LocationServiceHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: LocationServiceHelper class for helper methods
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Text;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.LocationService.Gazetteer;
using TDP.Common.LocationService.GIS;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Reporting.Events;
using TDP.UserPortal.AdditionalDataModule;
using TransportDirect.Presentation.InteractiveMapping;
using Logger = System.Diagnostics.Trace;

namespace TDP.Common.LocationService
{
    internal static class LocationServiceHelper
    {
        #region Public methods - Populate location

        /// <summary>
        /// Populates a naptan location selected from the auto-suggest list
        /// </summary>
        public static TDPLocation PopulateNaPTANLocation(LocationSearch search)
        {
            if (!string.IsNullOrEmpty(search.SearchId))
            {
                try
                {
                    // Find naptans
                    List<string> naptans = FindNaptans(search.SearchId.Split(new char[] { ',' }));

                    if (naptans.Count > 0)
                    {
                        NaptanCacheEntry nce = TDPNaptanCache.Get(naptans[0], "Naptan");

                        string locality = FindLocality(nce.OSGR, naptans);
                        List<string> toids = FindToids(nce.OSGR, string.Empty, true);

                        // Dertermine actual location type (not just TDPLocationType.Station)
                        TDPLocationType locationTypeActual = TDPLocationTypeHelper.GetTDPLocationTypeForNaPTAN(naptans[0]);
                        if (locationTypeActual == TDPLocationType.Unknown)
                            locationTypeActual = search.SearchType; // Otherwise default back to the value in the search

                        // Create the location to return
                        TDPLocation location = new TDPLocation(
                            nce.Name,
                            (!string.IsNullOrEmpty(search.SearchText)) ? search.SearchText : nce.Name,
                            locality,
                            toids,
                            naptans,
                            string.Empty,
                            search.SearchType,
                            locationTypeActual,
                            nce.OSGR,
                            nce.OSGR,
                            false,
                            false,
                            0,
                            0,
                            nce.Naptan);

                        return location;
                    }
                }
                catch (TDPException tdEx)
                {
                    if (!tdEx.Logged)
                    {
                        // Unable to populate the Naptans, log the information
                        string message = string.Format("PopulateNaPTANLocation method failed to populate location for {0}. Error: {1} Stack: {2}", search.SearchId, tdEx.Message, tdEx.StackTrace);
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning, message));
                    }
                }
                catch (Exception ex)
                {
                    // Unable to populate the Naptans, log the information
                    string message = string.Format("PopulateNaPTANLocation method failed to populate location for {0}. Error: {1} Stack: {2}", search.SearchId, ex.Message, ex.StackTrace);
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning, message));
                }
            }

            // Location could not be retrieved for a naptan, return null and let caller handle
            return null;
        }

        /// <summary>
        /// Populates a group location selected from the auto-suggest list
        /// </summary>
        public static TDPLocation PopulateGroupLocation(LocationSearch search, TDPLocation groupLocation)
        {
            if (!string.IsNullOrEmpty(search.SearchId))
            {
                try
                {
                    // Build the group location
                    if (groupLocation != null)
                    {
                        #region Validate group naptans

                        List<string> naptans = new List<string>();

                        // Check each group naptan in turn, because one group naptan may no longer exist,
                        // but the others may be valid - so shouldnt fail journey planning
                        foreach (string n in groupLocation.Naptan)
                        {
                            try
                            {
                                naptans.AddRange(FindNaptans(new string[1] { n }));
                            }
                            catch (TDPException tdEx)
                            {
                                // Any exceptions thrown for invalid naptan, ignore and carry on to next naptan,
                                // otherwise throw and exit
                                if (tdEx.Identifier != TDPExceptionIdentifier.LSNaptanCacheLookupNaptanInvalid)
                                {
                                    throw;
                                }
                            }
                        }

                        if (naptans.Count == 0)
                        {
                            // Throw exception, this shouldnt happen as the auto suggest should only contain
                            // group location ids which exist
                            throw new Exception();
                        }

                        #endregion

                        if (naptans.Count > 0)
                        {
                            OSGridReference osgr = new OSGridReference(groupLocation.GridRef.Easting, groupLocation.GridRef.Northing);
                            if (!osgr.IsValid)
                            {
                                string message = string.Format("Group location for id {0} contains an invalid osgr[{1}], instead using osgr of first group naptan[{2}]",
                                    search.SearchId, osgr.ToString(), naptans[0]);
                                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning, message));

                                NaptanCacheEntry nce = TDPNaptanCache.Get(naptans[0], "Naptan");
                                osgr = nce.OSGR;
                            }

                            string locality = FindLocality(osgr, naptans);
                            List<string> toids = FindToids(osgr, string.Empty, true);

                            // Create the location to return
                            TDPLocation location = new TDPLocation(
                                groupLocation.Name,
                                (!string.IsNullOrEmpty(search.SearchText)) ? search.SearchText : groupLocation.DisplayName,
                                locality,
                                toids,
                                naptans,
                                string.Empty,
                                search.SearchType,
                                search.SearchType,
                                osgr,
                                osgr,
                                false,
                                false,
                                0,
                                0,
                                search.SearchId);
                            
                            return location;
                        }
                    }
                }
                catch (TDPException tdEx)
                {
                    if (!tdEx.Logged)
                    {
                        // Unable to populate the Naptans, log the information
                        string message = string.Format("PopulateGroupLocation method failed to populate location for {0}. Error: {1} Stack: {2}", search.SearchId, tdEx.Message, tdEx.StackTrace);
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning, message));
                    }
                }
                catch (Exception ex)
                {
                    // Unable to populate the Naptans, log the information and reset the location and serch
                    string message = string.Format("PopulateGroupLocation method failed to populate for group id {0}. Error: {1} Stack: {2}", search.SearchId, ex.Message, ex.StackTrace);
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning, message));
                }   
            }

            // Location could not be retrieved for a naptan, return null and let caller handle
            return null;
        }

        /// <summary>
        /// Populates a coordinate location (POI point of interest) selected from the auto-suggest list
        /// </summary>
        public static TDPLocation PopulateCoordinateLocation(LocationSearch search, TDPLocation coordLocation)
        {
            if (!string.IsNullOrEmpty(search.SearchId))
            {
                try
                {
                    if (coordLocation == null)
                    {
                        // If no coord location then method has been called incorrectly, 
                        // use the other PopulateCoordinateLocation(string name, OSGridReference gridRef)
                        throw new Exception();
                    }

                    string locality = FindLocality(coordLocation.GridRef, null);
                    List<string> toids = FindToids(coordLocation.GridRef, string.Empty, false);

                    // Create the location to return
                    TDPLocation location = new TDPLocation(
                        coordLocation.Name,
                        (!string.IsNullOrEmpty(search.SearchText)) ? search.SearchText : coordLocation.DisplayName,
                        locality,
                        toids,
                        new List<string>(),
                        string.Empty,
                        search.SearchType,
                        search.SearchType,
                        coordLocation.GridRef,
                        coordLocation.GridRef,
                        false,
                        false,
                        0,
                        0,
                        coordLocation.ID);

                    return location;
                }
                catch (TDPException tdEx)
                {
                    if (!tdEx.Logged)
                    {
                        // Unable to populate, log the information
                        string message = string.Format("PopulateCoordinateLocation method failed to populate location for {0}. Error: {1} Stack: {2}", search.SearchId, tdEx.Message, tdEx.StackTrace);
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning, message));
                    }
                }
                catch (Exception ex)
                {
                    // Unable to populate, log the information and reset the location and serch
                    string message = string.Format("PopulateCoordinateLocation method failed to populate for poi id {0}. Error: {1} Stack: {2}", search.SearchId, ex.Message, ex.StackTrace);
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning, message));
                }   
            }

            return null;
        }

        /// <summary>
        /// Returns an TDPLocation for the supplied name and grid reference with type TDPLocationType.CoordinateEN
        /// The location is built by finding the closest Locality to the coordinate, and using that
        /// as the basis for the Coordinate TDPLocation
        /// </summary>
        /// <param name="gridRef"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TDPLocation PopulateCoordinateLocation(string name, OSGridReference gridRef)
        {
            TDPLocation location = null;

            if (gridRef != null && gridRef.IsValid)
            {
                // Find the locality (coordinate location must have a locality for passing into the journey planner)
                string locality = FindLocality(gridRef, null);

                if (!string.IsNullOrEmpty(locality))
                {
                    #region Set display name

                    GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                    LocalityNameInfo lni = gisQuery.GetLocalityInfoForNatGazID(locality);

                    bool appendLocalityName = Properties.Current[Keys.CoordinateLocation_AppendLocalityName].Parse(false);

                    // Set name/displayname 
                    // (if missing use the locality location, otherwise append locality to name provided)
                    string displayName = string.Empty;

                    if (string.IsNullOrEmpty(name))
                    {
                        // No name supplied, used the locality name
                        name = lni.LocalityName;
                        displayName = lni.LocalityName;
                    }
                    else if (appendLocalityName && (!name.Contains(lni.LocalityName)))
                    {
                        // Append the locality name (if flag set)
                        displayName = string.Format("{0} ({1})", name, lni.LocalityName);
                    }
                    else
                    {
                        // Name supplied (and/or locality name already appended to it), use what was supplied
                        displayName = name;
                        name = displayName.SubstringFirst('(').Trim();
                    }

                    #endregion

                    List<string> toids = FindToids(gridRef, string.Empty, false);

                    // Create location using the locality properties where needed
                    location = new TDPLocation(
                        name,
                        displayName,
                        locality,
                        toids,
                        new List<string>(),
                        string.Empty,
                        TDPLocationType.CoordinateEN,
                        TDPLocationType.CoordinateEN,
                        gridRef,
                        gridRef,
                        false,
                        false,
                        0,
                        0,
                        string.Empty);
                }
            }

            return location;
        }

        /// <summary>
        /// Populates a locality location using the Locality gazetteer
        /// </summary>
        public static TDPLocation PopulateLocalityLocation(ref LocationSearch search, TDPLocation localityLocation, string sessionId)
        {
            using (TDPGazetteer gazetteer = new LocalityGazetteer(sessionId, false))
            {
                LocationQueryResult locationQueryResult = null; // Gaz result
                LocationChoiceList locationChoiceList = null; // Location choices from Gaz result
                LocationChoice locationChoice = null; // Location choice to get location for
                TDPLocation location = null; // Location result

                // Check for null search text, as it might be a page landing resolution using the locality id only
                if (string.IsNullOrEmpty(search.SearchText) && localityLocation != null)
                {
                    search.SearchText = localityLocation.DisplayName;
                }

                bool performFindLocation = true;
                bool performDrillDown = false;

                #region Ambiguous resolution logic

                // Check if this is to resolve an ambiguous location selected from the gaz
                if (search.LocationChoiceSelected != null)
                {
                    performFindLocation = false;

                    if (search.LocationChoiceDrillDown && search.LocationChoiceSelected.HasChilden)
                    {
                        performDrillDown = true;
                    }

                    // Set the location query result and choice (to get the location for)
                    locationQueryResult = search.GetLocationQueryResult(search.CurrentLevel());
                    locationChoice = search.LocationChoiceSelected;
                }

                #endregion
                
                if (performFindLocation)
                {
                    // Perform gazetteer search
                    locationQueryResult = GazFindLocation(gazetteer, ref search);

                    // No query results, so return null location
                    if (locationQueryResult == null)
                        return null;
                }

                if (performDrillDown)
                {
                    // Perform gazetteer drill down
                    locationQueryResult = GazDrillDown(gazetteer, ref search, locationQueryResult, locationChoice);
                }
                                
                locationChoiceList = locationQueryResult.LocationChoiceList;

                bool ignoreChildren = false;

                if (locationChoice == null) // If not null, then ambiguity resolution is probably happening
                {
                    #region Identify location choice (if possible)

                    // Single location found
                    if (locationChoiceList.Count == 1 && !locationChoiceList[0].HasChilden)
                        locationChoice = locationChoiceList[0];

                    // Otherwise, if search id was supplied (e.g. it was selected from auto-suggest),
                    // then try to resolve the location by matching it with the localityId
                    else if (!string.IsNullOrEmpty(search.SearchId))
                    {
                        #region Try to resolve location by finding locality

                        foreach (LocationChoice lc in locationChoiceList)
                        {
                            // PicklistValue is the locality id
                            if (lc.PicklistValue.ToLower().Contains(search.SearchId.ToLower()))
                            {
                                // Found
                                locationChoice = lc;

                                // Indicate to resolve location even though it has children
                                if (lc.HasChilden)
                                    ignoreChildren = true;

                                break;
                            }
                        }

                        #endregion
                    }

                    #endregion
                }
                
                location = GazGetLocation(gazetteer, ref search, locationQueryResult, locationChoice, ignoreChildren);

                if (location != null)
                    return location;

                GazUpdateSearchLocationChoice(gazetteer, ref search, locationQueryResult, locationChoiceList, locationChoice);
            }

            return null;
        }

        /// <summary>
        /// Populates an address postcode location using the AddressPostcode gazetteer
        /// </summary>
        public static TDPLocation PopulateAddressPostcodeLocation(ref LocationSearch search, string sessionId)
        {
            using (TDPGazetteer gazetteer = new AddressPostcodeGazetteer(sessionId, false))
            {
                LocationQueryResult locationQueryResult = null; // Gaz result
                LocationChoiceList locationChoiceList = null; // Location choices from Gaz result
                LocationChoice locationChoice = null; // Location choice to get location for
                TDPLocation location = null; // Location result

                // Check for null search text, as it might be a page landing resolution using the postcode id value
                if (string.IsNullOrEmpty(search.SearchText) && !(string.IsNullOrEmpty(search.SearchId)))
                {
                    search.SearchText = search.SearchId;
                }

                bool performFindLocation = true;
                bool performDrillDown = false;

                #region Ambiguous resolution logic

                // Check if this is to resolve an ambiguous location selected from the gaz
                if (search.LocationChoiceSelected != null)
                {
                    performFindLocation = false;

                    if (search.LocationChoiceDrillDown && search.LocationChoiceSelected.HasChilden)
                    {
                        performDrillDown = true;
                    }

                    // Set the location query result and choice (to get the location for)
                    locationQueryResult = search.GetLocationQueryResult(search.CurrentLevel());
                    locationChoice = search.LocationChoiceSelected;
                }

                #endregion

                if (performFindLocation)
                {
                    // Perform gazetteer search
                    locationQueryResult = GazFindLocation(gazetteer, ref search);

                    // No query results, so return null location
                    if (locationQueryResult == null)
                        return null;
                }

                if (performDrillDown)
                {
                    // Perform gazetteer drill down
                    locationQueryResult = GazDrillDown(gazetteer, ref search, locationQueryResult, locationChoice);
                }

                locationChoiceList = locationQueryResult.LocationChoiceList;

                bool ignoreChildren = false;

                if (locationChoice == null) // If not null, then ambiguity resolution is probably happening
                {
                    #region Identify location choice (if possible)

                    // Single location found
                    if (locationChoiceList.Count == 1 && !locationChoiceList[0].HasChilden)
                        locationChoice = locationChoiceList[0];

                    // Otherwise, if search id was supplied (e.g. it was selected from auto-suggest or landing page),
                    // then try to resolve the location by matching it
                    else if (!string.IsNullOrEmpty(search.SearchId))
                    {
                        #region Try to resolve location by finding exact address

                        foreach (LocationChoice lc in locationChoiceList)
                        {
                            // PicklistValue is the locality id
                            if (lc.Description.ToLower().Equals(search.SearchId.ToLower()))
                            {
                                // Found
                                locationChoice = lc;

                                // Indicate to resolve location even though it has children
                                if (lc.HasChilden)
                                    ignoreChildren = true;

                                break;
                            }
                        }

                        #endregion
                    }
                                        
                    #endregion
                }

                location = GazGetLocation(gazetteer, ref search, locationQueryResult, locationChoice, ignoreChildren);

                if (location != null)
                    return location;

                GazUpdateSearchLocationChoice(gazetteer, ref search, locationQueryResult, locationChoiceList, locationChoice);
            }

            return null;
        }

        #endregion

        #region Public methods - Populate helpers - Naptans, Locality, Toids, CRS, SMS, IATA

        /// <summary>
        /// Finds the Naptans that are closest to the provided naptan input
        /// </summary>
        public static List<string> FindNaptans(string[] naptan)
        {
            List<string> naptans = new List<string>();
            NaptanCacheEntry nce = null;
            int i = 0;
            
            try
            {
                foreach (string tempNaptan in naptan)
                {
                    nce = TDPNaptanCache.Get(tempNaptan.Trim(), "Naptan");

                    if (nce.Found)
                    {
                        naptans.Add(nce.Naptan);
                        i++;
                    }
                    else
                    {
                        // If any Naptans are not found, this shouldnt happen because the auto-suggest
                        // list should only contain valid naptans
                        throw new TDPException(string.Empty, false, TDPExceptionIdentifier.LSNaptanCacheLookupNaptanInvalid);
                    }
                }
            }
            catch (Exception ex) // Catch's any errors from NaptanCache Get, e.g. where "ABC" is the naptan submitted
            {
                string message = string.Format("Naptan code not found or invalid Naptan code used [{0}]",
                                (nce != null) ? nce.Naptan : "null");

                // Append exception if exists, because exception could just be a naptan not found from above
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    message = string.Format("{0}. Exception message[{1}], stack[{2}]", message, ex.Message, ex.StackTrace);

                    // Naptan cache handles and logs all "Not found" exceptions, but any other exceptions should 
                    // cascade up to this try catch, so log and throw
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message));

                    throw (new TDPException(message, true, TDPExceptionIdentifier.LSNaptanCacheLookupError));
                }
                else
                {
                    throw (new TDPException(message, false, TDPExceptionIdentifier.LSNaptanCacheLookupNaptanInvalid));
                }
            }

            return naptans;
        }

        /// <summary>
        /// Populate locality data
        /// </summary>
        public static string FindLocality(OSGridReference osgr, List<string> naptans)
        {
            string locality = string.Empty;

            // Use locality of naptan if availble
            if (naptans != null && naptans.Count > 0)
            {
                NaptanCacheEntry nce = null;

                foreach (string naptan in naptans)
                {
                    nce = TDPNaptanCache.Get(naptan, "Naptan");

                    if (nce.Found)
                    {
                        if (!string.IsNullOrEmpty(nce.Locality))
                        {
                            locality = nce.Locality;
                            break;
                        }
                    }
                }
            }

            // Otherwise, get locality from coordinate
            if (string.IsNullOrEmpty(locality))
            {
                if (osgr != null && osgr.IsValid)
                {
                    try
                    {
                        GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                        locality = gisQuery.FindNearestLocality(osgr.Easting, osgr.Northing);

                        if (TDPTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                                string.Format("GISQuery - FindNearestLocality result[{0}] for coordinate[{1}]", locality, osgr.ToString())));
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format("Error performing GISQuery call to FindNearestLocality for coordinate[{0}]. Message: {1}", osgr.ToString(), ex.Message);

                        throw new TDPException(message, false, TDPExceptionIdentifier.LSGISQueryCallError, ex);
                    }
                }
            }

            return locality;
        }

        /// <summary>
        /// Populate toids data
        /// </summary>
        public static List<string> FindToids(OSGridReference osgr, string address, bool useAddress)
        {
            List<string> toids = new List<string>();

            if (osgr != null && osgr.IsValid)
            {
                try
                {
                    GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                    QuerySchema gisResult = (useAddress) ?
                        gisQuery.FindNearestITN(osgr.Easting, osgr.Northing, address, true) :
                        gisQuery.FindNearestITNs(osgr.Easting, osgr.Northing);

                    StringBuilder msg = new StringBuilder();

                    // Get the toids from the result
                    for (int i = 0; i < gisResult.ITN.Rows.Count; i++)
                    {
                        QuerySchema.ITNRow row = (QuerySchema.ITNRow)gisResult.ITN.Rows[i];
                        toids.Add(row.toid);

                        msg.Append(string.Format("{0}({1}) ", row.toid, row.name));
                    }

                    if (TDPTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                            string.Format("GISQuery - FindNearestITN result[{0}] for coordinate[{1}] address[{2}]", msg.ToString().Trim(), osgr.ToString(), address)));
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error performing GISQuery call to FindNearestITN for coordinate[{0}]. Message: {1}", osgr.ToString(), ex.Message);

                    throw new TDPException(message, false, TDPExceptionIdentifier.LSGISQueryCallError, ex);
                }
            }

            return toids;
        }

        /// <summary>
        /// Populate CRS code for a naptan
        /// </summary>
        /// <param name="naptans"></param>
        /// <returns></returns>
        public static string FindCRSCode(List<string> naptans)
        {
            string crsCode = string.Empty;

            if (naptans != null && naptans.Count > 0)
            {
                try
                {
                    // Use AdditionalData to find CRS code
                    AdditionalData addData = TDPServiceDiscovery.Current.Get<AdditionalData>(ServiceDiscoveryKey.AdditionalData);

                    crsCode = addData.LookupCrsForNaptan(naptans[0]);
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error performing AdditionalData call to LookupCrsForNaptan for naptan[{0}]. Message: {1}", naptans[0], ex.Message);
                    
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message));
                    
                    // Do not throw exception as not critical a code was not found
                }
            }

            return crsCode;
        }

        /// <summary>
        /// Populate SMS code for a naptan
        /// </summary>
        public static string FindSMSCode(List<string> naptans)
        {
            string smsCode = string.Empty;

            if (naptans != null && naptans.Count > 0)
            {
                try
                {
                    // Use GIS to find SMS code
                    GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                    QuerySchema gisResult = gisQuery.FindStopsInfoForStops(naptans.ToArray());


                    for (int i = 0; i < gisResult.Stops.Rows.Count; i++)
                    {
                        QuerySchema.StopsRow row = (QuerySchema.StopsRow)gisResult.Stops.Rows[i];

                        if (!string.IsNullOrEmpty(row.smsnumber))
                        {
                            smsCode = row.smsnumber;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error performing GISQuery call to FindStopsInfoForStops for naptan[{0}]. Message: {1}", naptans[0], ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message));

                    // Do not throw exception as not critical a code was not found
                }
            }

            return smsCode;
        }

        /// <summary>
        /// Populate IATA code for a naptan
        /// </summary>
        /// <param name="naptans"></param>
        /// <returns></returns>
        public static string FindIATACode(List<string> naptans)
        {
            string iataCode = string.Empty;

            if (naptans != null && naptans.Count > 0)
            {
                try
                {
                    // IATA code is normally the 3 character code at the end of the naptan
                    string airportPrefix = Properties.Current[Keys.NaptanPrefix_Airport].Parse("9200");

                    if (naptans[0].StartsWith(airportPrefix))
                    {
                        iataCode = naptans[0].Substring(airportPrefix.Length, 3);
                    }
                }
                catch
                {
                    // Do not throw exception as not critical a code was not found
                }
            }

            return iataCode;
        }
        
        #endregion

        #region Private methods - Gaz helpers

        /// <summary>
        /// Returns LocationQueryResult for the gaz FindLocation call, null if search is too vague
        /// </summary>
        private static LocationQueryResult GazFindLocation(ITDPGazetteer gazetteer, ref LocationSearch search)
        {
            #region Find location gaz call

            // Perform gazetteer search
            LocationQueryResult locationQueryResult = gazetteer.FindLocation(search.SearchText, false);

            // Update the hierarchic flag to indicate to caller that hierarchic drill down can be done
            search.SupportHierarchic = gazetteer.SupportHierarchicSearch;

            // If the response from the gazetteer is vague then update search object to allow caller to handle
            search.VagueSearch = locationQueryResult.IsVague;
            search.SingleWord = locationQueryResult.IsSingleWordAddress;

            if (search.VagueSearch || search.SingleWord)
            {
                // No query results, so return null location
                return null;
            }

            #endregion

            return locationQueryResult;
        }

        /// <summary>
        /// Returns LocationQueryResult for the gaz DrillDown call
        /// </summary>
        private static LocationQueryResult GazDrillDown(ITDPGazetteer gazetteer, ref LocationSearch search,
            LocationQueryResult locationQueryResult, LocationChoice locationChoice)
        {
            #region Drill down gaz call

            LocationQueryResult lqr = gazetteer.DrillDown(
                search.SearchText, false,
                locationQueryResult.PickListUsed,
                locationQueryResult.QueryReference,
                locationChoice);

            #endregion

            return lqr;
        }

        /// <summary>
        /// Returns Location from the gaz GetLocation call
        /// </summary>
        private static TDPLocation GazGetLocation(ITDPGazetteer gazetteer, ref LocationSearch search,
            LocationQueryResult locationQueryResult, LocationChoice locationChoice, bool ignoreChildren)
        {
            // Location choice provided! But if children exist, then its ambiguous
            if (locationChoice != null && (ignoreChildren || !locationChoice.HasChilden))
            {
                #region Get location gaz call

                // Populate the location details for the choice
                TDPLocation location = gazetteer.GetLocationDetails(
                        search.SearchText, false,
                        locationQueryResult.PickListUsed,
                        locationQueryResult.QueryReference,
                        locationChoice);

                search.SearchText = location.DisplayName;

                return location;

                #endregion
            }

            return null;
        }

        /// <summary>
        /// Update search with query results
        /// </summary>
        private static void GazUpdateSearchLocationChoice(ITDPGazetteer gazetteer, ref LocationSearch search,
            LocationQueryResult locationQueryResult, LocationChoiceList locationChoiceList, LocationChoice locationChoice)
        {
            // Multiple locations found - ambiguous
            if (locationChoiceList.Count >= 1)
            {
                #region Update search with query results

                // Otherwise, update search with ambiguous location choices

                // If the search is hierarchical, add the query result to the
                // collection so that navigation of the hierarchy is possible
                if (gazetteer.SupportHierarchicSearch)
                {
                    if (locationChoice != null)
                        locationQueryResult.ParentChoice = locationChoice;
                }
                else
                {
                    // A non-hierarchical search will only ever contain one query result
                    // since navigation is not supported in this case
                    search.LocationQueryResults.Clear();
                }

                search.LocationQueryResults.Add(locationQueryResult);

                #endregion
            }
        }

        #endregion

        #region Public methods - Log MIS

        /// <summary>
        /// Log location gaz event for MIS
        /// </summary>
        public static void LogGazetteerEvent(GazetteerEventCategory gazetteerEventCategory, DateTime submitted, string sessionId)
        {
            GazetteerEvent ge = new GazetteerEvent(
                gazetteerEventCategory, submitted, sessionId, false);
            Logger.Write(ge);
        }

        /// <summary>
        /// Parses an auto-suggest location type string value into a GazetteerEventCategory
        /// </summary>
        public static GazetteerEventCategory GetGazetteerEventCategory(TDPLocationType locationType)
        {
            switch (locationType)
            {
                case TDPLocationType.StationAirport: return GazetteerEventCategory.GazetteerAutoSuggestAirport;
                case TDPLocationType.StationCoach: return GazetteerEventCategory.GazetteerAutoSuggestCoach;
                case TDPLocationType.StationFerry: return GazetteerEventCategory.GazetteerAutoSuggestFerry;
                case TDPLocationType.StationRail: return GazetteerEventCategory.GazetteerAutoSuggestRail;
                case TDPLocationType.StationTMU: return GazetteerEventCategory.GazetteerAutoSuggestTMU;
                case TDPLocationType.StationGroup: return GazetteerEventCategory.GazetteerAutoSuggestGroup;
                case TDPLocationType.Locality: return GazetteerEventCategory.GazetteerAutoSuggestLocality;
                case TDPLocationType.POI: return GazetteerEventCategory.GazetteerAutoSuggestPointOfInterest;

                case TDPLocationType.Postcode: return GazetteerEventCategory.GazetteerPostCode;
                case TDPLocationType.Address: return GazetteerEventCategory.GazetteerAddress;

                case TDPLocationType.CoordinateEN: return GazetteerEventCategory.GazetteerCoordinate;
                case TDPLocationType.CoordinateLL: return GazetteerEventCategory.GazetteerCoordinate;

                // Any other strings are ignored and the default returned
            }

            return GazetteerEventCategory.GazetteerUnknown;
        }

        #endregion
    }
}
