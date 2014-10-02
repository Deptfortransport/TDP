// *********************************************** 
// NAME                 : TDLocationCache.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 20/08/2012 
// DESCRIPTION          : Helper class to provide methods to obtain 
//                      : Location Information for TDLocations
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/Cache/TDLocationCache.cs-arc  $ 
//
//   Rev 1.4   Dec 05 2012 13:34:32   mmodi
//Updated to remove static location cache and use an instance 
//Resolution for 5877: IIS Recycle issue
//
//   Rev 1.3   Oct 10 2012 14:29:12   mmodi
//Updated trace logging level
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.2   Oct 04 2012 14:22:52   mmodi
//Corrected method summary comments
//Resolution for 5857: Gaz - Code review updates
//
//   Rev 1.1   Aug 31 2012 14:23:56   mmodi
//Allow searching for a location using an enterned naptan or locality
//Resolution for 5832: CCN0668 Gazetteer Enhancements - Auto-Suggest drop downs
//
//   Rev 1.0   Aug 28 2012 10:45:30   mmodi
//Initial revision.
//Resolution for 5832: CCN Gaz

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService.Cache
{
    /// <summary>
    /// TDLocationCache helper class to provide methods to obtain 
    //  Location Information for TDLocations
    /// </summary>
    internal class TDLocationCache
    {
        #region Private members

        // Stored procs - All must return the same column names as a common ReadLocation method is used
        private SqlHelperDatabase DB_LocationsDatabase = SqlHelperDatabase.DefaultDB;

        private string SP_GetLocationVersion = "GetLocationsVersion";

        private string SP_GetLocations = "GetLocations";
        private string SP_GetLocation = "GetLocation";
        private string SP_GetUnknownLocation = "GetUnknownLocation";
        private string SP_GetPoiLocation = "GetPoiLocation";
        private string SP_GetAlternativeSuggestionList = "GetAlternativeSuggestionList";

        // Used for load
        private readonly object dataInitialisedLock = new object();
        private bool dataLocationsInitialised = false;

        // Load/Use flags
        private bool useLocationCache = false;

        // Location version
        private string version = string.Empty;

        // Location caches
        private List<TDLocation> locations = new List<TDLocation>();
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        internal TDLocationCache()
        {
            LoadLocations();
        }

        #endregion

        #region Private methods

        #region Version

        /// <summary>
        /// Populates the Locations version
        /// </summary>
        private void PopulateVersion()
        {
            // Build Locations List
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, "Loading Location data version"));

                    string tmpVersion = string.Empty;

                    Hashtable paramList = new Hashtable();

                    helper.ConnOpen(DB_LocationsDatabase);

                    // Read and populate the detailed venue location array
                    using (SqlDataReader dr = helper.GetReader(SP_GetLocationVersion, paramList))
                    {
                        while (dr.Read())
                        {
                            tmpVersion = (dr["Version"] != DBNull.Value) ? dr["Version"].ToString() : string.Empty;
                        }
                    }

                    // Assign
                    version = tmpVersion;

                    if (helper.ConnIsOpen)
                        helper.ConnClose();

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, string.Format("Location data version is[{0}]", version)));
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load Location data version: {0}", ex.Message);

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message, ex));
                }
            }
        }

        #endregion

        #region Locations

        /// <summary>
        /// Populates the Locations cache by retrieveing the source data from the database.
        /// </summary>
        private void PopulateLocationsData()
        {
            // Build Locations List
            using (SqlHelper helper = new SqlHelper())
            {
                // Temp lists before assigning to the class lists
                List<TDLocation> tmpLocations = new List<TDLocation>();

                try
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, "Loading Locations data"));

                    #region Load locations

                    TDLocation tdLocation = null;

                    Hashtable paramList = new Hashtable();

                    helper.ConnOpen(DB_LocationsDatabase);

                    // Read and populate the detailed venue location array
                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetLocations, paramList))
                    {
                        while (locationsDR.Read())
                        {
                            tdLocation = ReadLocation(locationsDR);

                            tmpLocations.Add(tdLocation);
                        }
                    }

                    // Assign to class lists
                    locations = tmpLocations;

                    if (helper.ConnIsOpen)
                        helper.ConnClose();

                    #endregion

                    // Log warning if no locations cached
                    if (useLocationCache && locations.Count == 0)
                        Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Warning, string.Format("Locations in cache [{0}]", locations.Count)));
                    else
                        Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, string.Format("Locations in cache [{0}]", locations.Count)));

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, "Loading Locations data completed"));
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load Locations data: {0}", ex.Message);

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message, ex));
                }
            }
        }

        #endregion

        #region Group

        /// <summary>
        /// Populates a single Group location (not cached) by retrieving the source data from the database.
        /// </summary>
        /// <returns></returns>
        private TDLocation PopulateGroupData(string groupId)
        {
            // Load group location from database
            TDLocation tdLocation = null;
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    #region Load group
                    
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@groupID", groupId);
                    
                    helper.ConnOpen(DB_LocationsDatabase);

                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetLocation, parameters))
                    {
                        if (locationsDR.HasRows)
                        {
                            locationsDR.Read();

                            tdLocation = ReadLocation(locationsDR);

                            Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                    string.Format("Read Group location data for group[{0}]", groupId)));

                        }
                    }

                    if (helper.ConnIsOpen)
                        helper.ConnClose();

                    #endregion
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load GroupId[{0}] location data: {1}", groupId, ex.Message);

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message, ex));
                }
            }

            return tdLocation;
        }

        #endregion

        #region Unknown

        /// <summary>
        /// Populates a single Unknown location (not cached) by retrieving the source data from the database.
        /// </summary>
        /// <returns></returns>
        private TDLocation PopulateUnknownData(string location)
        {
            // Load Unknown location from database
            TDLocation tdLocation = null;

            if (!string.IsNullOrEmpty(location))
            {
                using (SqlHelper helper = new SqlHelper())
                {
                    try
                    {
                        #region Load unknown

                        Hashtable parameters = new Hashtable();
                        parameters.Add("@searchstring", location);

                        helper.ConnOpen(DB_LocationsDatabase);
                                                
                        using (SqlDataReader locationsDR = helper.GetReader(SP_GetUnknownLocation, parameters))
                        {
                            if (locationsDR.HasRows)
                            {
                                locationsDR.Read();

                                tdLocation = ReadLocation(locationsDR);

                                Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                        string.Format("Read Unknown location data for location searchstring[{0}]", location)));

                            }
                        }

                        if (helper.ConnIsOpen)
                            helper.ConnClose();

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format("Error occurred attempting to load Unknown location[{0}] location data: {1}", location, ex.Message);

                        Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message, ex));
                    }
                }
            }

            return tdLocation;
        }

        #endregion

        #region POI

        /// <summary>
        /// Populates a single POI location (not cached) by retrieving the source data from the database.
        /// </summary>
        /// <returns></returns>
        private TDLocation PopulatePoiData(string location)
        {
            // Load Unknown location from database
            TDLocation tdLocation = null;

            if (!string.IsNullOrEmpty(location))
            {
                using (SqlHelper helper = new SqlHelper())
                {
                    try
                    {
                        #region Load POI

                        Hashtable parameters = new Hashtable();
                        parameters.Add("@poi", location);

                        helper.ConnOpen(DB_LocationsDatabase);

                        using (SqlDataReader locationsDR = helper.GetReader(SP_GetPoiLocation, parameters))
                        {
                            if (locationsDR.HasRows)
                            {
                                locationsDR.Read();

                                tdLocation = ReadLocation(locationsDR);

                                Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                        string.Format("Read POI location data for location searchstring[{0}]", location)));

                            }
                        }

                        if (helper.ConnIsOpen)
                            helper.ConnClose();

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format("Error occurred attempting to load POI location[{0}] location data: {1}", location, ex.Message);

                        Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message, ex));
                    }
                }
            }

            return tdLocation;
        }

        #endregion

        /// <summary>
        /// Uses the SqlDataReader to build an TDLocation (does not advance the reader)
        /// </summary>
        private TDLocation ReadLocation(SqlDataReader locationsDR)
        {
            // Read Values
            string dataSetID = (locationsDR["DATASETID"] != DBNull.Value) ? locationsDR["DATASETID"].ToString() : string.Empty;
            string displayName = (locationsDR["DisplayName"] != DBNull.Value) ? locationsDR["DisplayName"].ToString() : string.Empty;
            string locality = (locationsDR["LocalityID"] != DBNull.Value) ? locationsDR["LocalityID"].ToString() : string.Empty;
            string parentId = (locationsDR["ParentID"] != DBNull.Value) ? locationsDR["ParentID"].ToString() : string.Empty;
            
            int easting = Convert.ToInt32(locationsDR["Easting"].ToString());
            int northing = Convert.ToInt32(locationsDR["Northing"].ToString());
            OSGridReference osgr = new OSGridReference(easting, northing);
                        
            string naptan = (locationsDR["Naptan"] != DBNull.Value) ? locationsDR["Naptan"].ToString() : string.Empty;
            List<TDNaptan> tdNaptans = new List<TDNaptan>();
            TDNaptan tdNaptan;
            if (!string.IsNullOrEmpty(naptan))
            {
                foreach (string n in (naptan.Split(new char[] { ',' })))
                {
                    tdNaptan = new TDNaptan();
                    tdNaptan.Naptan = n;
                    tdNaptans.Add(tdNaptan);
                }
            }

            string type = (locationsDR["Type"] != DBNull.Value) ? locationsDR["Type"].ToString() : string.Empty;
            TDStopType stopType = TDStopTypeHelper.GetTDStopType(type);

            // Commented out in case added for future
            //string name = (locationsDR["Name"] != DBNull.Value) ? locationsDR["Name"].ToString() : string.Empty;
            //float nearestEasting = (float)Convert.ToDouble((locationsDR["NearestPointE"] != DBNull.Value) ? locationsDR["NearestPointE"].ToString() : easting.ToString());
            //float nearestNorthing = (float)Convert.ToDouble((locationsDR["NearestPointN"] != DBNull.Value) ? locationsDR["NearestPointN"].ToString() : northing.ToString());
            //OSGridReference nearestOsgr = new OSGridReference(nearestEasting, nearestNorthing);

            //int adminAreaID = (int)Convert.ToInt32((locationsDR["AdminAreaID"] != DBNull.Value) ? locationsDR["AdminAreaID"].ToString() : "0");
            //int districtID = (int)Convert.ToInt32((locationsDR["DistrictID"] != DBNull.Value) ? locationsDR["DistrictID"].ToString() : "0");
            
            //string toid = (locationsDR["NearestTOID"] != DBNull.Value) ? locationsDR["NearestTOID"].ToString() : string.Empty;
            //List<string> toids = new List<string>();
            //if (!string.IsNullOrEmpty(toid))
            //    toids.Add(toid);

            // Create location
            TDLocation tdLocation = new TDLocation();

            // Reset (constructor doesnt seem to set everything)
            tdLocation.ClearAll();

            tdLocation.DataSetID = dataSetID;
            tdLocation.ParentID = parentId;
            tdLocation.Description = displayName;
            tdLocation.Locality = locality;
            tdLocation.NaPTANs = tdNaptans.ToArray();
            tdLocation.GridReference = osgr;
            tdLocation.StopType = stopType;

            return tdLocation;
        }

        #endregion

        #region Query methods

        /// <summary>
        /// Returns the version of this locations cache
        /// </summary>
        /// <returns></returns>
        internal string GetVersion()
        {
            return version;
        }

        /// <summary>
        /// Searches for a Station Group where the Group ID is an exact match with the supplied search string. 
        /// </summary>
        /// <param name="groupID">The ID of the group to search for</param>
        /// <returns>TDLocation or null if no match found</returns>
        internal TDLocation GetGroupLocation(string groupID)
        {
            if (useLocationCache)
            {
                // Find group location in cache
                TDLocation result = locations.Find(delegate(TDLocation loc) { return loc.DataSetID == groupID; });
                return result;
            }
            else
            {
                // Load group location from database
                TDLocation result = PopulateGroupData(groupID);
                return result;
            }
        }

        /// <summary>
        /// Searches for a POI location with an exact match with the supplied search string. 
        /// </summary>
        /// <param name="poiName">Name of the POI</param>
        /// <returns>TDLocation or null if no match found</returns>
        internal TDLocation GetPOILocation(string poiName)
        {
            if (useLocationCache)
            {
                // Find group location in cache
                string poiUpper = poiName.ToUpper();
                TDLocation result = locations.Find(delegate(TDLocation loc) { return loc.Description.ToUpper() == poiUpper; });
                return result;
            }
            else
            {
                // Load group location from database
                TDLocation result = PopulatePoiData(poiName);
                return result;
            }
        }

        /// <summary>
        /// Searches for any location where the DisplayName is an exact match to the supplied search string. 
        /// </summary>
        /// <param name="location">The location name</param>
        /// <returns>TDLocation or null if no match found</returns>
        internal TDLocation GetUnknownLocation(string location)
        {
            if (useLocationCache)
            {
                // Find unknown location in cache (search on display name)
                string locationUpper = location.ToUpper();

                TDLocation result = locations.Find(delegate(TDLocation loc) { return loc.Description.ToUpper() == locationUpper; });

                if (result == null)
                {
                    // Try searching for id (this allows resolution using a naptan/locality/group code)
                    result = locations.Find(delegate(TDLocation loc) { return loc.ID.ToUpper() == locationUpper; });
                }

                return result;
            }
            else
            {
                // Load unknown location from database
                TDLocation result = PopulateUnknownData(location);
                return result;
            }
        }

        /// <summary>
        /// Searches for locations that are a close match to the supplied search string. 
        /// </summary>
        /// <param name="searchString">The name to search for</param>
        /// <returns>TDLocation[], contains suitable alternatives</returns>
        internal List<TDLocation> GetAlternativeTDLocations(string searchString)
        {
            // Enforce a limit to search for, e.g. a single letter "a" search could return 1000's of records
            int searchLimit;
            if (!Int32.TryParse(Properties.Current[LSKeys.Max_SearchLocationsLimit], out searchLimit))
                searchLimit = 1000;

            // Number of locations to return
            int searchShowLimit;
            if (!Int32.TryParse(Properties.Current[LSKeys.Max_SearchLocationsShow], out searchShowLimit))
                searchShowLimit = 20;

            // Use "in cache" ambiguity search logic,
            // this is different to the database stored proc ambiguity search, and better
            bool searchInCache;
            if (!Boolean.TryParse(Properties.Current[LSKeys.Search_Cache_Locations], out searchInCache))
                searchInCache = false;

            List<TDLocation> altLocations = new List<TDLocation>();
            if (useLocationCache && searchInCache)
            {
                altLocations.AddRange(GetAlternativeTDLocationsFromCache(searchString, searchLimit));
            }
            else
            {
                altLocations.AddRange(GetAlternativeTDLocationsFromDB(searchString, searchLimit));
            }

            // Sort/limit the results
            return SortAndFilterAlternativeTDLocations(altLocations, searchShowLimit);
        }

        #region Alternative Locations helpers

        /// <summary>
        /// Searches for locations that are a close match to the supplied search string from cached location store
        /// </summary>
        /// <param name="searchString">The name to search for</param>
        /// <param name="searchLimit">Maximum number of search results to return</param>
        /// <returns>TDLocation[], contains suitable alternatives</returns>
        private List<TDLocation> GetAlternativeTDLocationsFromCache(string searchString, int searchLimit)
        {
            Levenstein levenstein = new Levenstein();

            #region Varibles

            double simindexlimit_NoCommonWords;
            if (!Double.TryParse(Properties.Current[LSKeys.SimilarityIndex_NoCommonWords], out simindexlimit_NoCommonWords))
                simindexlimit_NoCommonWords = 0.5;

            double simindexlimit_NoCommonWordsAndSpace;
            if (!Double.TryParse(Properties.Current[LSKeys.SimilarityIndex_NoCommonWordsAndSpace], out simindexlimit_NoCommonWordsAndSpace))
                simindexlimit_NoCommonWordsAndSpace = 0.5;

            double simindexlimit_IndividualWords;
            if (!Double.TryParse(Properties.Current[LSKeys.SimilarityIndex_IndividualWords], out simindexlimit_IndividualWords))
                simindexlimit_IndividualWords = 0.65;

            bool simIndex_ChildLocalityAtEnd; 
            if (!Boolean.TryParse(Properties.Current[LSKeys.SimilarityIndex_ChildLocalityAtEnd], out simIndex_ChildLocalityAtEnd))
                simIndex_ChildLocalityAtEnd = true;
            
            // contains the matches with the whole search string matched
            Dictionary<TDLocation, double> levensteinMatchWhole = new Dictionary<TDLocation, double>();

            // contains the matches when matches  done using part of the search string words matched
            Dictionary<TDLocation, double> levensteinMatchPart = new Dictionary<TDLocation, double>();

            // contains the matches when matches  done using part of the search string words matched
            Dictionary<TDLocation, double> levensteinMatchChildLocality = new Dictionary<TDLocation, double>();

            // Get the common words from properties
            List<string> commonWords = new List<string>(Properties.Current[LSKeys.CommonWords].Split(new char[] { ',' }));

            // Remove the common symbols
            commonWords.AddRange(new string[] { ",", ".", "and", "&", "-", "(", ")", "'" });

            #endregion

            // normalise the spaces between word by removing extra spaces
            searchString = System.Text.RegularExpressions.Regex.Replace(searchString, @"\s+", " ");

            // Strip the common words first from both search string and location display name
            string search = StripCommonWords(searchString.ToLower(), commonWords);

            foreach (TDLocation location in locations)
            {
                bool childLocality = false;

                if (location.StopType == TDStopType.Locality  && !string.IsNullOrEmpty(location.ParentID))
                {
                    childLocality = simIndex_ChildLocalityAtEnd;
                }

                // Strip the common words first from both search string and location display name
                string toMatch = StripCommonWords(location.Description.ToLower(), commonWords);

                #region Perform similarity check and add

                // Get the similarity index with the common words stripped
                double simInd = levenstein.GetSimilarity(search, toMatch);

                // Get the similarity index with the common words stripped and the spaced between words
                double simIndNoSpace = levenstein.GetSimilarity(search.Replace(" ", ""), toMatch.Replace(" ", ""));

                // if the location name starts with the search string specified add the location with similarity index as 1
                if (toMatch.StartsWith(search))
                {
                    if (childLocality)
                    {
                        // if child locality put the locations in a separate dictionary
                        if (!levensteinMatchChildLocality.ContainsKey(location))
                        {
                            levensteinMatchChildLocality.Add(location, 0);
                        }
                    }
                    else
                    {
                        if (!levensteinMatchWhole.ContainsKey(location))
                        {
                            levensteinMatchWhole.Add(location, 1);
                        }
                    }
                }

                // if similarity index with only common words stripped is more than 0.5 add the location as match
                else if (simInd > simindexlimit_NoCommonWords)
                {
                    if (childLocality)
                    {
                        // if child locality put the locations in a separate dictionary
                        if (!levensteinMatchChildLocality.ContainsKey(location))
                        {
                            levensteinMatchChildLocality.Add(location, 0);
                        }
                    }
                    else
                    {
                        if (!levensteinMatchWhole.ContainsKey(location))
                        {
                            levensteinMatchWhole.Add(location, simInd);
                        }
                    }
                }
                // We had similarity index less than 0.5 with only common words stripped
                // Lets test if the similarity index is greater than 0.5 with common words and spaces stripped
                else if (simIndNoSpace > simindexlimit_NoCommonWordsAndSpace)
                {
                    if (childLocality)
                    {
                        // if child locality put the locations in a separate dictionary
                        if (!levensteinMatchChildLocality.ContainsKey(location))
                        {
                            levensteinMatchChildLocality.Add(location, 0);
                        }
                    }
                    else
                    {
                        if (!levensteinMatchWhole.ContainsKey(location))
                        {
                            levensteinMatchWhole.Add(location, simInd);
                        }
                    }
                }
                else // this is painful as we have to break the word in tokens and look for the similarity index
                {
                    string[] toMatchTokens = toMatch.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] searchTokens = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string st in searchTokens)
                    {
                        foreach (string token in toMatchTokens)
                        {
                            simInd = levenstein.GetSimilarity(st, token);

                            // for individual words matching we have to have similarity index higher.
                            // Set it to 65% 
                            if (simInd > simindexlimit_IndividualWords)
                            {
                                if (childLocality)
                                {
                                    // if child locality put the locations in a separate dictionary
                                    if (!levensteinMatchChildLocality.ContainsKey(location))
                                    {
                                        levensteinMatchChildLocality.Add(location, 0);
                                    }
                                }
                                else
                                {
                                    if (!levensteinMatchPart.ContainsKey(location))
                                    {
                                        levensteinMatchPart.Add(location, simInd);
                                    }
                                }
                            }

                        }
                    }
                }

                #endregion
            }

            // Sort the dictionaries and get the first specific number of locations defined by search limit
            Dictionary<TDLocation, double> filtered =
                Take(
                    Concat(
                        Concat(OrderByValue(levensteinMatchWhole, true), 
                               OrderByValue(levensteinMatchPart, true)), 
                        OrderByKey(levensteinMatchChildLocality, false)), // show locations of type locality with parent locality at very end
                    searchLimit);

            return new List<TDLocation>(filtered.Keys);
        }

        /// <summary>
        /// Searches for TDLocations that are a close match to the supplied search string in database
        /// </summary>
        /// <param name="searchString">The name to search for</param>
        /// <param name="searchLimit">Maximum number of search results to return</param>
        /// <returns>TDLocation[], contains suitable alternatives</returns>
        private List<TDLocation> GetAlternativeTDLocationsFromDB(string searchString, int searchLimit)
        {
            List<TDLocation> altLocations = new List<TDLocation>();
            Hashtable parameters = new Hashtable();
            parameters.Add("@searchstring", searchString);
            parameters.Add("@maxRecords", searchLimit);

            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    helper.ConnOpen(DB_LocationsDatabase);
                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetAlternativeSuggestionList, parameters))
                    {
                        while (locationsDR.Read())
                        {
                            TDLocation tempLocation = ReadLocation(locationsDR);
                            altLocations.Add(tempLocation);
                        }
                    }

                    if (helper.ConnIsOpen)
                        helper.ConnClose();
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to read Alternative suggestions locations data: {0}", ex.Message);

                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message, ex));
                }
            }

            return altLocations;
        }

        /// <summary>
        /// Sorts and limits the number of the provided TDLocations
        /// </summary>
        private List<TDLocation> SortAndFilterAlternativeTDLocations(List<TDLocation> altLocations, int searchShowLimit)
        {
            List<TDLocation> sortedLocations = new List<TDLocation>();

            // Limits to apply for location types if more than max show limit exceeded
            int searchShowLimitGroup;
            if (!Int32.TryParse(Properties.Current[LSKeys.Max_SearchLocationsShowLimit_GroupStations], out searchShowLimitGroup))
                searchShowLimitGroup = 100;

            int searchShowLimitRail;
            if (!Int32.TryParse(Properties.Current[LSKeys.Max_SearchLocationsShowLimit_RailStations], out searchShowLimitRail))
                searchShowLimitRail = 5;

            int searchShowLimitPOI;
            if (!Int32.TryParse(Properties.Current[LSKeys.Max_SearchLocationsShowLimit_POIs], out searchShowLimitPOI))
                searchShowLimitPOI = 5;

            int searchShowLimitCoach;
            if (!Int32.TryParse(Properties.Current[LSKeys.Max_SearchLocationsShowLimit_CoachStations], out searchShowLimitCoach))
                searchShowLimitCoach = 2;

            int searchShowLimitTram;
            if (!Int32.TryParse(Properties.Current[LSKeys.Max_SearchLocationsShowLimit_TramStations], out searchShowLimitTram))
                searchShowLimitTram = 5;

            int searchShowLimitFerry;
            if (!Int32.TryParse(Properties.Current[LSKeys.Max_SearchLocationsShowLimit_FerryStations], out searchShowLimitFerry))
                searchShowLimitFerry = 100;

            int searchShowLimitAirport;
            if (!Int32.TryParse(Properties.Current[LSKeys.Max_SearchLocationsShowLimit_AirportStations], out searchShowLimitAirport))
                searchShowLimitAirport = 100;

            if ((altLocations != null) && (altLocations.Count > 0))
            {
                // Only apply the loc type limits if more than show limit number of locations
                bool applySortLimits = (altLocations.Count > searchShowLimit);

                // Sort order is  as follows:
                // 1. Exchange groups (group stations)
                // 2. Rail stations
                // 3. Localities
                // 4. POIs
                // 5. Coach stations
                // 6. TMU (tram, metros, underground)
                // 7. Ferry
                // 8. Airport
                // 9. Other (all other types)

                // Assume the locations provided have already been placed in an initial order, 
                // e.g. the "closer" matches are first
                // Therefore we want to preserve the locations order, but grouped as above

                #region Filter the location types

                // Extract the locations for each group type
                List<TDLocation> locStationGroups = altLocations.FindAll(delegate(TDLocation loc) { return loc.StopType == TDStopType.Group; });
                List<TDLocation> locStationsRail = altLocations.FindAll(delegate(TDLocation loc) { return loc.StopType == TDStopType.Rail; });
                List<TDLocation> locLocalities = altLocations.FindAll(delegate(TDLocation loc) { return loc.StopType == TDStopType.Locality; });
                List<TDLocation> locPOIs = altLocations.FindAll(delegate(TDLocation loc) { return loc.StopType == TDStopType.POI; });
                List<TDLocation> locStationsCoach = altLocations.FindAll(delegate(TDLocation loc) { return loc.StopType == TDStopType.Coach; });
                List<TDLocation> locStationsTMU = altLocations.FindAll(delegate(TDLocation loc) { return loc.StopType == TDStopType.LightRail; });
                List<TDLocation> locStationsFerry = altLocations.FindAll(delegate(TDLocation loc) { return loc.StopType == TDStopType.Ferry; });
                List<TDLocation> locStationsAirport = altLocations.FindAll(delegate(TDLocation loc) { return loc.StopType == TDStopType.Air; });
                List<TDLocation> locOther = altLocations.FindAll(delegate(TDLocation loc)
                {
                    return (loc.StopType == TDStopType.Unknown)
                           || (loc.StopType == TDStopType.Taxi)
                           || (loc.StopType == TDStopType.Bus);
                });
                #endregion

                #region Add the locations to the sorted list

                // - Exchange Groups
                if (applySortLimits && (locStationGroups.Count > searchShowLimitGroup))
                {
                    sortedLocations.AddRange(Take(locStationGroups, searchShowLimitGroup));
                }
                else
                {
                    sortedLocations.AddRange(locStationGroups);
                }

                // - Rail Stations
                if (applySortLimits && (locStationsRail.Count > searchShowLimitRail))
                {
                    sortedLocations.AddRange(Take(locStationsRail, searchShowLimitRail));
                }
                else
                {
                    sortedLocations.AddRange(locStationsRail);
                }

                // - Localities 
                // These are used to fill out the list if needed, therefore skip over for now and insert last
                int localitiesIndex = sortedLocations.Count;

                // POIs
                if (applySortLimits && (locPOIs.Count > searchShowLimitPOI))
                {
                    sortedLocations.AddRange(Take(locPOIs, searchShowLimitPOI));
                }
                else
                {
                    sortedLocations.AddRange(locPOIs);
                }

                // - Coach stations
                if (applySortLimits && (locStationsCoach.Count > searchShowLimitCoach))
                {
                    sortedLocations.AddRange(Take(locStationsCoach, searchShowLimitCoach));
                }
                else
                {
                    sortedLocations.AddRange(locStationsCoach);
                }

                // - TMU
                if (applySortLimits && (locStationsTMU.Count > searchShowLimitTram))
                {
                    sortedLocations.AddRange(Take(locStationsTMU, searchShowLimitTram));
                }
                else
                {
                    sortedLocations.AddRange(locStationsTMU);
                }

                // - Ferry
                if (applySortLimits && (locStationsFerry.Count > searchShowLimitFerry))
                {
                    sortedLocations.AddRange(Take(locStationsFerry, searchShowLimitFerry));
                }
                else
                {
                    sortedLocations.AddRange(locStationsFerry);
                }

                // - Airport
                if (applySortLimits && (locStationsAirport.Count > searchShowLimitAirport))
                {
                    sortedLocations.AddRange(Take(locStationsAirport, searchShowLimitAirport));
                }
                else
                {
                    sortedLocations.AddRange(locStationsAirport);
                }

                // - Other
                sortedLocations.AddRange(locOther);

                // Insert the localities, upto the maximum number
                if ((locLocalities.Count > 0) && (sortedLocations.Count < searchShowLimit))
                {
                    if (locLocalities.Count > (searchShowLimit - sortedLocations.Count))
                    {
                        sortedLocations.InsertRange(localitiesIndex, Take(locLocalities, searchShowLimit - sortedLocations.Count));
                    }
                    else
                    {
                        sortedLocations.InsertRange(localitiesIndex, locLocalities);
                    }
                }

                #endregion
            }

            // Return the sorted locations, using the limit in case the above logic added too many
            return Take(sortedLocations, searchShowLimit);
        }

        /// <summary>
        /// Strips out common occurring words from the target string
        /// </summary>
        /// <param name="targetString">Target string from which common words need removing</param>
        /// <param name="commonWords">List of common words to be removed from the target string</param>
        /// <returns>Target string with all the common words removed i.e coach, station, rail, etc..</returns>
        private string StripCommonWords(string targetString, List<string> commonWords)
        {
            foreach (string word in commonWords)
            {
                targetString = targetString.ToLower().Replace(word.ToLower(), "").Trim();
            }

            return targetString;
        }

        #endregion

        #endregion

        #region Load

        /// <summary>
        /// Loads all Locations data
        /// </summary>
        internal void LoadLocations()
        {
            // Make load threadsafe
            if (!dataLocationsInitialised)
            {
                lock (dataInitialisedLock)
                {
                    // Check if locations should be cached, potentially a very large dataset so
                    // make it switchable
                    if (!Boolean.TryParse(Properties.Current[LSKeys.Cache_LoadLocations], out useLocationCache))
                        useLocationCache = false;

                    if (!dataLocationsInitialised)
                    {
                        // Load properties
                        DB_LocationsDatabase = (SqlHelperDatabase)Enum.Parse(typeof(SqlHelperDatabase), Properties.Current[LSKeys.Data_LocationsDatabase]);

                        SP_GetLocationVersion = Properties.Current[LSKeys.Data_LocationsVersionStoredProcedure]; //GetLocationsVersion

                        SP_GetLocations = Properties.Current[LSKeys.Data_LocationsStoredProcedure]; //"GetLocations";
                        SP_GetLocation = Properties.Current[LSKeys.Data_LocationStoredProcedure]; //"GetLocation";
                        SP_GetUnknownLocation = Properties.Current[LSKeys.Data_LocationUnknownStoredProcedure]; //"GetUnknownLocation";
                        SP_GetAlternativeSuggestionList = Properties.Current[LSKeys.Data_LocationsAlternateStoredProcedure]; //"GetAlternativeSuggestionList";

                        // Load version
                        PopulateVersion();

                        // Load data if use cache flag has been set
                        if (useLocationCache)
                        {
                            PopulateLocationsData();
                        }
                        else
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, "LocationServiceCache is set to not cache locations data"));
                        }

                        // Set to true here (prevents repeated attempts to load if it fails)
                        dataLocationsInitialised = true;
                    }
                }
            }
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Retrieves a sub list from this instance. 
        /// The list starts at position 0 and retrieves upto the specified length
        /// </summary>
        private List<TDLocation> Take(List<TDLocation> list, int length)
        {
            if ((list == null) || (list.Count == 0) || (length <= 0))
            {
                return new List<TDLocation>();
            }

            if (list.Count <= length)
            {
                return list;
            }
            else
            {
                return list.GetRange(0, length);
            }
        }

        /// <summary>
        /// Retrieves a sub list from this instance. 
        /// The list starts at position 0 and retrieves upto the specified length
        /// </summary>
        private Dictionary<TDLocation, double> Take(Dictionary<TDLocation, double> dict, int length)
        {
            if ((dict == null) || (dict.Count == 0) || (length <= 0))
            {
                return new Dictionary<TDLocation, double>();
            }

            if (dict.Count <= length)
            {
                return dict;
            }
            else
            {
                int count = 0;

                Dictionary<TDLocation, double> result = new Dictionary<TDLocation, double>();

                foreach (KeyValuePair<TDLocation, double> kvp in dict)
                {
                    result.Add(kvp.Key, kvp.Value);
                    count++;

                    if (count == length)
                        break;
                }

                return result;
            }
        }

        /// <summary>
        /// Orders a Dictionary<TDLocation, double> by value
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        private Dictionary<TDLocation, double> OrderByValue(Dictionary<TDLocation, double> dict, bool descending)
        {
            if ((dict == null) || (dict.Count == 0))
            {
                return new Dictionary<TDLocation,double>();
            }

            if (dict.Count == 1)
            {
                return dict;
            }
            else
            {
                // As we're in .NET 2.0, forced to do this the slow way
                List<KeyValuePair<TDLocation, double>> list = new List<KeyValuePair<TDLocation, double>>();

                list.AddRange(dict);

                list.Sort(
                    delegate(
                        KeyValuePair<TDLocation, double> firstPair,
                        KeyValuePair<TDLocation, double> nextPair)
                    {
                        if (descending)
                        {
                            return nextPair.Value.CompareTo(firstPair.Value);
                        }
                        else
                        {
                            return firstPair.Value.CompareTo(nextPair.Value);
                        }
                    }
                );

                Dictionary<TDLocation, double> result = new Dictionary<TDLocation, double>();

                foreach (KeyValuePair<TDLocation, double> kvp in list)
                {
                    result.Add(kvp.Key, kvp.Value);
                }

                return result;
            }
        }

        /// <summary>
        /// Orders a Dictionary<TDLocation, double> on the Key.Description value
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        private Dictionary<TDLocation, double> OrderByKey(Dictionary<TDLocation, double> dict, bool descending)
        {
            if ((dict == null) || (dict.Count == 0))
            {
                return new Dictionary<TDLocation, double>();
            }

            if (dict.Count == 1)
            {
                return dict;
            }
            else
            {
                // As we're in .NET 2.0, forced to do this the slow way
                List<KeyValuePair<TDLocation, double>> list = new List<KeyValuePair<TDLocation, double>>();

                list.AddRange(dict);

                list.Sort(
                    delegate(
                        KeyValuePair<TDLocation, double> firstPair,
                        KeyValuePair<TDLocation, double> nextPair)
                    {
                        if (descending)
                        {
                            return nextPair.Key.Description.CompareTo(firstPair.Key.Description);
                        }
                        else
                        {
                            return firstPair.Key.Description.CompareTo(nextPair.Key.Description);
                        }
                    }
                );

                Dictionary<TDLocation, double> result = new Dictionary<TDLocation, double>();

                foreach (KeyValuePair<TDLocation, double> kvp in list)
                {
                    result.Add(kvp.Key, kvp.Value);
                }

                return result;
            }
        }

        /// <summary>
        /// Joins together two Dictionary<TDLocation, double> 
        /// </summary>
        /// <param name="dict1"></param>
        /// <param name="dict2"></param>
        /// <returns></returns>
        private Dictionary<TDLocation, double> Concat(Dictionary<TDLocation, double> dict1, Dictionary<TDLocation, double> dict2)
        {
            if ((dict1 == null) || (dict2 == null))
            {
                return new Dictionary<TDLocation, double>();
            }
            else if ((dict1 == null) && (dict2 != null))
            {
                return dict2;
            }
            else if ((dict1 != null) && (dict2 == null))
            {
                return dict1;
            }

            Dictionary<TDLocation, double> result = new Dictionary<TDLocation, double>();

            foreach (KeyValuePair<TDLocation, double> kvp in dict1)
            {
                result.Add(kvp.Key, kvp.Value);
            }

            foreach (KeyValuePair<TDLocation, double> kvp in dict2)
            {
                result.Add(kvp.Key, kvp.Value);
            }

            return result;
        }

        #endregion
    }
}
