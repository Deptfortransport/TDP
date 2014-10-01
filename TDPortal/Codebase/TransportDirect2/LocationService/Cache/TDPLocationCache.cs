// *********************************************** 
// NAME             : TDPLocationCache.cs      
// AUTHOR           : Mark Turner
// DATE CREATED     : 21 Feb 2011
// DESCRIPTION  	: Helper class to provide methods to obtain 
//                    Location Information for non-Olympic Venue locations
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.PropertyManager;
using Logger = System.Diagnostics.Trace;

namespace TDP.Common.LocationService
{
    /// <summary>
    /// TDPLocationCache helper class to provide methods to obtain 
    //  Location Information for non-Olympic Venue locations
    /// </summary>
    static class TDPLocationCache
    {
        #region Private members

        // Stored procs - All must return the same column names as a common ReadLocation method is used
        private static SqlHelperDatabase DB_LocationsDatabase = SqlHelperDatabase.AtosAdditionalDataDB;

        private const string SP_GetLocationVersion = "GetLocationsVersion";

        private const string SP_GetLocations = "GetLocations";
        private const string SP_GetUnknownLocation = "GetUnknownLocation";
        private const string SP_GetNaptanLocation = "GetNaptanLocation";
        private const string SP_GetGroupLocation = "GetGroupLocation";
        private const string SP_GetLocalityLocation = "GetLocalityLocation";
        private const string SP_GetLocalityLocations = "GetLocalityLocations";
        private const string SP_GetPostcodeLocation = "GetPostcodeLocation";
        private const string SP_GetPostcodeLocations = "GetPostcodeLocations";
        private const string SP_GetPoiLocation = "GetPoiLocation";
        private const string SP_GetIdLocation = "GetLocation";

        private const string SP_GetAlternativeSuggestionList = "GetAlternativeSuggestionList";

        // Used for load
        private static readonly object dataInitialisedLock = new object();
        private static bool dataLocationsInitialised = false;
        private static bool dataPostcodesInitialised = false;

        // Location version
        private static string version = string.Empty;

        // Load/Use flags
        private static bool useLocationCache = false;
        private static bool usePostcodeCache = false;

        // Location caches
        private static List<TDPLocation> locations = new List<TDPLocation>();
        private static List<TDPLocation> postcodeLocations = new List<TDPLocation>();

        #endregion

        #region Constructor

        /// <summary>
        /// Static Constructor
        /// </summary>
        static TDPLocationCache()
        {
            LoadLocations();
            LoadPostcodes();
        }

        #endregion

        #region Private methods

        #region Version

        /// <summary>
        /// Populates the Locations version
        /// </summary>
        private static void PopulateVersion()
        {
            // Build Locations List
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose, "Loading Location data version"));

                    string tmpVersion = string.Empty;

                    List<SqlParameter> paramList = new List<SqlParameter>();

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

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Info, string.Format("Location data version is[{0}]", version)));
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load Location data version: {0}", ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }
        }

        #endregion

        #region Locations

        /// <summary>
        /// Populates the Locations cache by retrieveing the source data from the database.
        /// </summary>
        private static void PopulateLocationsData()
        {
            // Build Locations List
            using (SqlHelper helper = new SqlHelper())
            {
                // Temp lists before assigning to the static lists
                List<TDPLocation> tmpLocations = new List<TDPLocation>();

                try
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Info, "Loading Locations data"));

                    #region Load locations

                    TDPLocation tdpLocation = null;

                    List<SqlParameter> paramList = new List<SqlParameter>();

                    helper.ConnOpen(DB_LocationsDatabase);

                    // Read and populate the detailed venue location array
                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetLocations, paramList))
                    {
                        while (locationsDR.Read())
                        {
                            tdpLocation = ReadLocation(locationsDR, TDPLocationType.Unknown);
                                                        
                            tmpLocations.Add(tdpLocation);
                        }
                    }

                    // Assign to static lists
                    locations = tmpLocations;

                    #endregion

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose, string.Format("Locations in cache [{0}]", locations.Count)));
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Info, "Loading Locations data completed"));
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load Locations data: {0}", ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }
        }

        #region ID

        /// <summary>
        /// Populates a single location by ID.
        /// </summary>
        /// <param name="id">ID of the location</param>
        /// <returns>The location</returns>
        private static TDPLocation PopulateIdData(string id)
        {
            // Load ID location from database
            TDPLocation tdpLocation = new TDPLocation();
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    #region Load ID location

                    List<SqlParameter> paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@id", id));

                    helper.ConnOpen(DB_LocationsDatabase);

                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetIdLocation, paramList))
                    {
                        if (locationsDR.HasRows)
                        {
                            locationsDR.Read();

                            tdpLocation = ReadLocation(locationsDR, TDPLocationType.Unknown);
                            
                            Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose,
                                    string.Format("Read ID location data for ID[{0}]", id)));

                        }
                    }
                    
                    #endregion
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load ID[{0}] location data: {1}", id, ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }

            return tdpLocation;
        }
        
        #endregion

        #region Unknown

        /// <summary>
        /// Populates a single Unknown location (not cached) by retrieving the source data from the database.
        /// </summary>
        /// <returns></returns>
        private static TDPLocation PopulateUnknownData(string location)
        {
            // Load Unknown location from database
            TDPLocation tdpLocation = new TDPLocation();

            if (!string.IsNullOrEmpty(location))
            {
                using (SqlHelper helper = new SqlHelper())
                {
                    try
                    {
                        #region Load unknown

                        // If its a postcode, then remove any spaces to allow search matching in database query
                        // as raw data includes spaces
                        if (location.IsValidPostcode())
                        {
                            location = location.Replace(" ", string.Empty);
                        }

                        List<SqlParameter> paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@searchstring", location));

                        helper.ConnOpen(DB_LocationsDatabase);

                        using (SqlDataReader locationsDR = helper.GetReader(SP_GetUnknownLocation, paramList))
                        {
                            if (locationsDR.HasRows)
                            {
                                locationsDR.Read();

                                tdpLocation = ReadLocation(locationsDR, TDPLocationType.Unknown);

                                Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose,
                                        string.Format("Read Unknown location data for location searchstring[{0}]", location)));

                            }
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format("Error occurred attempting to load Unknown location[{0}] location data: {1}", location, ex.Message);

                        Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                    }
                }
            }

            return tdpLocation;
        }
                
        #endregion

        #region NaPTAN

        /// <summary>
        /// Populates a single NaPTAN location (not cached) by retrieving the source data from the database.
        /// </summary>
        /// <returns></returns>
        private static TDPLocation PopulateNaPTANData(string naptanId)
        {
            // Load NaPTAN location from database
            TDPLocation tdpLocation = new TDPLocation();
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    #region Load naptan

                    List<SqlParameter> paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@naptan", naptanId));

                    helper.ConnOpen(DB_LocationsDatabase);

                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetNaptanLocation, paramList))
                    {
                        if (locationsDR.HasRows)
                        {
                            locationsDR.Read();

                            tdpLocation = ReadLocation(locationsDR, TDPLocationType.Station);
                            
                            Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose,
                                    string.Format("Read NaPTAN location data for naptan[{0}]", naptanId)));

                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load NaptanId[{0}] location data: {1}", naptanId, ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }

            return tdpLocation;
        }

        #endregion

        #region Group

        /// <summary>
        /// Populates a single Group location (not cached) by retrieving the source data from the database.
        /// </summary>
        /// <returns></returns>
        private static TDPLocation PopulateGroupData(string groupId)
        {
            // Load group location from database
            TDPLocation tdpLocation = new TDPLocation();
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    #region Load locality

                    List<SqlParameter> paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@groupID", groupId));

                    helper.ConnOpen(DB_LocationsDatabase);

                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetGroupLocation, paramList))
                    {
                        if (locationsDR.HasRows)
                        {
                            locationsDR.Read();

                            tdpLocation = ReadLocation(locationsDR, TDPLocationType.StationGroup);
                            
                            Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose,
                                    string.Format("Read Group location data for group[{0}]", groupId)));

                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load GroupId[{0}] location data: {1}", groupId, ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }

            return tdpLocation;
        }

        #endregion

        #region Locality

        /// <summary>
        /// Populates a single Locality location (not cached) by retrieving the source data from the database.
        /// </summary>
        /// <returns></returns>
        private static TDPLocation PopulateLocalityData(string localityId)
        {
            // Load locality location from database
            TDPLocation tdpLocation = new TDPLocation();
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    #region Load locality

                    List<SqlParameter> paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@localityID", localityId));

                    helper.ConnOpen(DB_LocationsDatabase);

                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetLocalityLocation, paramList))
                    {
                        if (locationsDR.HasRows)
                        {
                            locationsDR.Read();

                            tdpLocation = ReadLocation(locationsDR, TDPLocationType.Locality);
                            
                            Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose,
                                    string.Format("Read Locality location data for locality[{0}]", localityId)));

                        }
                    }
                    
                    #endregion
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load LocalityId[{0}] location data: {1}", localityId, ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }

            return tdpLocation;
        }

        /// <summary>
        /// Returns a list of Locality locations (not cached) by retrieving the source data from the database
        /// where localitys are within a coordinate square
        /// </summary>
        /// <param name="osgrMin"></param>
        /// <param name="osgrMax"></param>
        /// <returns></returns>
        private static List<TDPLocation> PopulateLocalityLocationsUsingCoordinate(OSGridReference osgrMin, OSGridReference osgrMax)
        {
            List<TDPLocation> locations = new List<TDPLocation>();

            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    #region Get localitys using coordinate square

                    List<SqlParameter> paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@eastingMin", osgrMin.Easting));
                    paramList.Add(new SqlParameter("@eastingMax", osgrMax.Easting));
                    paramList.Add(new SqlParameter("@northingMin", osgrMin.Northing));
                    paramList.Add(new SqlParameter("@northingMax", osgrMax.Northing));

                    helper.ConnOpen(DB_LocationsDatabase);

                    // Get locality locations using the coordinate square
                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetLocalityLocations, paramList))
                    {
                        TDPLocation tdpLocation = null;

                        if (locationsDR.HasRows)
                        {
                            while (locationsDR.Read())
                            {
                                tdpLocation = ReadLocation(locationsDR, TDPLocationType.Locality);

                                locations.Add(tdpLocation);
                            }
                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load Locality locations data for coordinate square [{0}][{1}]", osgrMin.ToString(), osgrMax.ToString());

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }


            Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose,
                    string.Format("Read Locality location data count[{0}] for localitys in cooridinate square [{1}][{2}]", locations.Count, osgrMin.ToString(), osgrMax.ToString())));

            return locations;
        }

        #endregion

        #region POI

        /// <summary>
        /// Populates a single POI location (not cached) by retrieving the source data from the database.
        /// </summary>
        /// <returns></returns>
        private static TDPLocation PopulatePoiData(string poiID)
        {
            // Load Unknown location from database
            TDPLocation tdLocation = null;

            if (!string.IsNullOrEmpty(poiID))
            {
                using (SqlHelper helper = new SqlHelper())
                {
                    try
                    {
                        #region Load POI

                        List<SqlParameter> paramList = new List<SqlParameter>();
                        paramList.Add(new SqlParameter("@poi", poiID));
                                                
                        helper.ConnOpen(DB_LocationsDatabase);

                        using (SqlDataReader locationsDR = helper.GetReader(SP_GetPoiLocation, paramList))
                        {
                            if (locationsDR.HasRows)
                            {
                                locationsDR.Read();

                                tdLocation = ReadLocation(locationsDR, TDPLocationType.POI);

                                Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose,
                                    string.Format("Read Locality location data for POI[{0}]", poiID)));

                            }
                        }

                        if (helper.ConnIsOpen)
                            helper.ConnClose();

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format("Error occurred attempting to load PoiId[{0}] location data: {1}", poiID, ex.Message);

                        Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                    }
                }
            }

            return tdLocation;
        }

        #endregion

        #endregion

        #region Postcodes

        /// <summary>
        /// Populates the Postcode locations cache by retrieveing the source data from the database.
        /// </summary>
        private static void PopulatePostcodesData()
        {
            // Build Postcode Locations List
            using (SqlHelper helper = new SqlHelper())
            {
                // Temp lists before assigning to the static lists
                List<TDPLocation> tmpPostcodeLocations = new List<TDPLocation>();

                try
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Info, "Loading Postcodes location data"));

                    #region Load postcodes

                    TDPLocation tdpLocation = null;

                    List<SqlParameter> paramList = new List<SqlParameter>();

                    helper.ConnOpen(DB_LocationsDatabase);
                    
                    // Read and populate the detailed venue location array
                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetPostcodeLocations, paramList))
                    {
                        while (locationsDR.Read())
                        {
                            tdpLocation = ReadLocation(locationsDR, TDPLocationType.Postcode);

                            // Add to locations list
                            tmpPostcodeLocations.Add(tdpLocation);
                        }
                    }

                    // Assign to static lists
                    postcodeLocations = tmpPostcodeLocations;

                    #endregion

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose, string.Format("Postcode locations in cache [{0}]", postcodeLocations.Count)));
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Info, "Loading Postcodes location data completed"));
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load Postcodes location data: {0}", ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }
        }

        /// <summary>
        /// Populates a single Postcode location (not cached) by retrieving the source data from the database.
        /// </summary>
        /// <returns></returns>
        private static TDPLocation PopulatePostcodeData(string postcode)
        {
            // Load postcode location from database
            TDPLocation tdpLocation = new TDPLocation();
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    #region Load postcode

                    // If its a postcode, then remove any spaces to allow search matching in database query
                    // as raw data includes spaces
                    if (postcode.IsValidPostcode())
                    {
                        postcode = postcode.Replace(" ", string.Empty);
                    }

                    List<SqlParameter> paramList = new List<SqlParameter>();
                    paramList.Add(new SqlParameter("@postcode", postcode));

                    helper.ConnOpen(DB_LocationsDatabase);

                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetPostcodeLocation, paramList))
                    {
                        if (locationsDR.HasRows)
                        {
                            locationsDR.Read();

                            tdpLocation = ReadLocation(locationsDR, TDPLocationType.Postcode);
                            
                            Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Verbose,
                                    string.Format("Read Postcode location data for postcode[{0}]", postcode)));
                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to load Postcode[{0}] location data: {1}", postcode, ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }

            return tdpLocation;
        }

        #endregion

        /// <summary>
        /// Uses the SqlDataReader to build an TDPLocation (does not advance the reader)
        /// </summary>
        private static TDPLocation ReadLocation(SqlDataReader locationsDR, TDPLocationType tdpLocationType)
        {
            // Return object
            TDPLocation tdpLocation = null;

            // Read Values
            string dataSetID = (locationsDR["DATASETID"] != DBNull.Value) ? locationsDR["DATASETID"].ToString() : string.Empty;
            string name = (locationsDR["Name"] != DBNull.Value) ? locationsDR["Name"].ToString() : string.Empty;
            string displayName = (locationsDR["DisplayName"] != DBNull.Value) ? locationsDR["DisplayName"].ToString() : string.Empty;
            string locality = (locationsDR["LocalityID"] != DBNull.Value) ? locationsDR["LocalityID"].ToString() : string.Empty;
            string parentId = (locationsDR["ParentID"] != DBNull.Value) ? locationsDR["ParentID"].ToString() : string.Empty;

            float easting = (float)Convert.ToDouble(locationsDR["Easting"].ToString());
            float northing = (float)Convert.ToDouble(locationsDR["Northing"].ToString());
            OSGridReference osgr = new OSGridReference(easting, northing);
            float nearestEasting = (float)Convert.ToDouble((locationsDR["NearestPointE"] != DBNull.Value) ? locationsDR["NearestPointE"].ToString() : easting.ToString());
            float nearestNorthing = (float)Convert.ToDouble((locationsDR["NearestPointN"] != DBNull.Value) ? locationsDR["NearestPointN"].ToString() : northing.ToString());
            OSGridReference nearestOsgr = new OSGridReference(nearestEasting, nearestNorthing);

            int adminAreaID = (int)Convert.ToInt32((locationsDR["AdminAreaID"] != DBNull.Value) ? locationsDR["AdminAreaID"].ToString() : "0");
            int districtID = (int)Convert.ToInt32((locationsDR["DistrictID"] != DBNull.Value) ? locationsDR["DistrictID"].ToString() : "0");

            string naptan = (locationsDR["Naptan"] != DBNull.Value) ? locationsDR["Naptan"].ToString() : string.Empty;
            List<string> naptans = new List<string>();
            if (!string.IsNullOrEmpty(naptan))
                naptans.AddRange(naptan.Split(new char[] {','}));

            string toid = (locationsDR["NearestTOID"] != DBNull.Value) ? locationsDR["NearestTOID"].ToString() : string.Empty;
            List<string> toids = new List<string>();
            if (!string.IsNullOrEmpty(toid))
                toids.Add(toid);

            TDPLocationType tdpLocationTypeActual = TDPLocationType.Unknown;

            // Only read the location if parameter value is not known
            if (tdpLocationType == TDPLocationType.Unknown)
            {
                string type = (locationsDR["Type"] != DBNull.Value) ? locationsDR["Type"].ToString() : string.Empty;
                tdpLocationType = TDPLocationTypeHelper.GetTDPLocationType(type);
                tdpLocationTypeActual = TDPLocationTypeHelper.GetTDPLocationTypeActual(type);
            }

            // Create naptan location
            tdpLocation = new TDPLocation(
                name, displayName, locality, toids, naptans, parentId,
                tdpLocationType, tdpLocationTypeActual, osgr, nearestOsgr, false, false, 
                adminAreaID, districtID, dataSetID);

            // Update GNAT flag
            tdpLocation.IsGNAT = TDPGNATLocationCache.IsGNAT(tdpLocation.ID,false,false);

            return tdpLocation;
        }

        #endregion

        #region Query methods

        /// <summary>
        /// Returns the version of this locations cache
        /// </summary>
        /// <returns></returns>
        internal static string GetVersion()
        {
            return version;
        }

        /// <summary>
        /// Searches for any location where the DisplayName is an exact match to the supplied search string. 
        /// </summary>
        /// <param name="location">The postcode to search for</param>
        /// <returns>TDPLocation or null if no match found</returns>
        internal static TDPLocation GetUnknownLocation(string location)
        {
            if (useLocationCache)
            {
                // Find unknown location in cache (search on display name)
                string locationUpper = location.ToUpper();

                TDPLocation result = locations.Find(delegate(TDPLocation loc) { return loc.DisplayName.ToUpper() == locationUpper; });
                return result;
            }
            else
            {
                // Load unknown location from database
                TDPLocation result = PopulateUnknownData(location);
                return result;
            }
        }

        /// <summary>
        /// Searches for a Station Location where the Naptan is an exact match to the supplied search string. 
        /// </summary>
        /// <param name="naptan">The naptan of the station to search for</param>
        /// <returns>TDPLocation or null if no match found</returns>
        internal static TDPLocation GetNaptanLocation(string naptan)
        {
            if (useLocationCache)
            {
                // Find naptan location in cache (each location in cache will only contain one (or none) naptan)
                TDPLocation result = locations.Find(delegate(TDPLocation loc) { return loc.Naptan.Contains(naptan); });
                return result;
            }
            else
            {
                // Load naptan location from database
                TDPLocation result = PopulateNaPTANData(naptan);
                return result;
            }
        }

        /// <summary>
        /// Searches for a Station Group where the Group ID is an exact match with the supplied search string. 
        /// </summary>
        /// <param name="groupID">The ID of the group to search for</param>
        /// <returns>TDPLocation or null if no match found</returns>
        internal static TDPLocation GetGroupLocation(string groupID)
        {
            if (useLocationCache)
            {
                // Find group location in cache
                TDPLocation result = locations.Find(delegate(TDPLocation loc) { return loc.DataSetID == groupID; });
                return result;
            }
            else
            {
                // Load group location from database
                TDPLocation result = PopulateGroupData(groupID);
                return result;
            }
        }

        /// <summary>
        /// Searches for a Locality that is an exact match to the supplied search string. 
        /// </summary>
        /// <param name="localityID">The ID of the locality to search for</param>
        /// <returns>TDPLocation or null if no match found</returns>
        internal static TDPLocation GetLocalityLocation(string localityID)
        {
            if (useLocationCache)
            {
                // Find locality location in cache
                TDPLocation result = locations.Find(delegate(TDPLocation loc) { return loc.ID == localityID; });
                return result;
            }
            else
            {
                // Load locality location from database
                TDPLocation result = PopulateLocalityData(localityID);
                return result;
            }
        }

        /// <summary>
        /// Searches for a Locality closest to the supplied coordinate
        /// </summary>
        /// <returns>TDPLocation or null if none found</returns>
        internal static TDPLocation GetLocalityLocationForCoordinate(OSGridReference osgr)
        {
            // Found locality location object
            TDPLocation result = null;

            #region Find closest locality location

            List<TDPLocation> tdpLocationsFound = new List<TDPLocation>();

            // Set an area to narrow down the localities
            int paddingEastingMetres = Properties.Current[Keys.CoordinateLocation_LocalitySearch_Padding_Easting].Parse(50000);
            int paddingNorthingMetres = Properties.Current[Keys.CoordinateLocation_LocalitySearch_Padding_Northing].Parse(50000);

            OSGridReference osgrMin = new OSGridReference(osgr.Easting - paddingEastingMetres, osgr.Northing - paddingNorthingMetres);
            OSGridReference osgrMax = new OSGridReference(osgr.Easting + paddingEastingMetres, osgr.Northing + paddingNorthingMetres);

            // Find all locality locations closest to coordinate
            if (useLocationCache)
            {
                // Load from cache
                tdpLocationsFound = locations.FindAll(delegate(TDPLocation loc) {
                    return (loc.TypeOfLocation == TDPLocationType.Locality
                        && loc.GridRef.Easting > osgrMin.Easting && loc.GridRef.Easting < osgrMax.Easting)
                        && (loc.GridRef.Northing > osgrMin.Northing && loc.GridRef.Northing < osgrMax.Northing);
                });
            }
            else
            {
                // Load from database
                tdpLocationsFound = PopulateLocalityLocationsUsingCoordinate(osgrMin, osgrMax);
            }

            // Track the shortest locality location distance from the coordinate
            int shortestDistance = Int32.MaxValue;
            int locDistance = 0;

            // Find the closest locality location to the coordinate
            foreach (TDPLocation loc in tdpLocationsFound)
            {
                locDistance = loc.GridRef.DistanceFrom(osgr);

                if (locDistance < shortestDistance)
                {
                    // Closer location found
                    shortestDistance = locDistance;
                    result = loc;
                }
            }

            Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    string.Format("Closest Locality location found for coordinate[{0}] is id[{1}] displayname[{2}] distance[{3}metres]",
                        osgr.ToString(),
                        (result != null) ? result.ID : string.Empty,
                        (result != null) ? result.DisplayName : string.Empty,
                        shortestDistance
                    )));

            #endregion

            return result;
        }

        /// <summary>
        /// Searches for a POI location with an exact match with the supplied search string. 
        /// </summary>
        /// <param name="poiID">Id of the POI</param>
        /// <returns>TDPLocation or null if no match found</returns>
        internal static TDPLocation GetPOILocation(string poiID)
        {
            if (useLocationCache)
            {
                // Find poi location in cache
                TDPLocation result = locations.Find(delegate(TDPLocation loc) { return loc.ID == poiID; });
                return result;
            }
            else
            {
                // Load poi location from database
                TDPLocation result = PopulatePoiData(poiID);
                return result;
            }
        }

        /// <summary>
        /// Searches for a Postcode that is an exact match to the supplied search string. 
        /// </summary>
        /// <param name="postcode">The postcode to search for</param>
        /// <returns>TDPLocation or null if no match found</returns>
        internal static TDPLocation GetPostcodeLocation(string postcode)
        {
            if (usePostcodeCache)
            {
                // Find postcode location in cache
                TDPLocation result = postcodeLocations.Find(delegate(TDPLocation loc)
                {
                    return loc.ID == postcode.ToUpper().Replace(" ", "");
                });
                return result;
            }
            else
            {
                // Load postcode location from database
                TDPLocation result = PopulatePostcodeData(postcode);
                return result;
            }
        }

        /// <summary>
        /// Searches for TDPLocations that are a close match to the supplied search string. 
        /// This method should only be called if a previous call to GetTDPLocation was unsuccesful.
        /// </summary>
        /// <param name="searchString">The name to search for</param>
        /// <returns>TDPLocation[], contains suitable alternatives</returns>
        internal static List<TDPLocation> GetAlternativeTDPLocations(string searchString)
        {
            // Enforce a limit to search for, e.g. a single letter "a" search could return 1000's of records
            int searchLimit = Properties.Current[Keys.Max_SearchLocationsLimit].Parse(1000);

            // Number of locations to return
            int searchShowLimit = Properties.Current[Keys.Max_SearchLocationsShow].Parse(20);

            // Use "in cache" ambiguity search logic,
            // this is different to the database stored proc ambiguity search, and better
            bool searchInCache = Properties.Current[Keys.Search_Cache_Locations].Parse(true);
                        
            List<TDPLocation> altLocations = new List<TDPLocation>();
            if (useLocationCache && searchInCache)
            {
                altLocations.AddRange(GetAlternativeTDPLocationsFromCache(searchString, searchLimit));
            }
            else
            {
                altLocations.AddRange(GetAlternativeTDPLocationsFromDB(searchString, searchLimit));
            }

            // Sort/limit the results
            return SortAndFilterAlternativeTDPLocations(altLocations, searchShowLimit);
        }

        #region Alternative Locations helpers

        /// <summary>
        /// Searches for Locations that are a close match to the supplied search string from cached TDPLocation store
        /// </summary>
        /// <param name="searchString">The name to search for</param>
        /// <param name="searchLimit">Maximum number of search results to return</param>
        /// <returns>TDPLocation[], contains suitable alternatives</returns>
        private static List<TDPLocation> GetAlternativeTDPLocationsFromCache(string searchString, int searchLimit)
        {
            Levenstein levenstein = new Levenstein();

            #region Varibles

            double simindexlimit_NoCommonWords = Properties.Current[Keys.SimilarityIndex_NoCommonWords].Parse(0.5);
            double simindexlimit_NoCommonWordsAndSpace = Properties.Current[Keys.SimilarityIndex_NoCommonWordsAndSpace].Parse(0.5);
            double simindexlimit_IndividualWords = Properties.Current[Keys.SimilarityIndex_IndividualWords].Parse(0.65);

            bool simIndex_ChildLocalityAtEnd = Properties.Current[Keys.SimilarityIndex_ChildLocalityAtEnd].Parse(true);
            
            // contains the matches with the whole search string matched
            Dictionary<TDPLocation, double> levensteinMatchWhole = new Dictionary<TDPLocation, double>();

            // contains the matches when matches  done using part of the search string words matched
            Dictionary<TDPLocation, double> levensteinMatchPart = new Dictionary<TDPLocation, double>();

            // contains the matches when matches  done using part of the search string words matched
            Dictionary<TDPLocation, double> levensteinMatchChildLocality = new Dictionary<TDPLocation, double>();

            // Get the common words from properties
            List<string> commonWords = new List<string>(Properties.Current[Keys.CommonWords].Split(new char[] { ',' }));

            // Remove the common symbols
            commonWords.AddRange(new string[] { ",", ".", "and", "&", "-", "(", ")", "'" });

            #endregion

            // normalise the spaces between word by removing extra spaces
            searchString = System.Text.RegularExpressions.Regex.Replace(searchString, @"\s+", " ");

            // Strip the common words first from both search string and location display name
            string search = StripCommonWords(searchString.ToLower(), commonWords);

            foreach (TDPLocation location in TDPLocationCache.locations)
            {
                bool childLocality = false;

                if (location.TypeOfLocation == TDPLocationType.Locality && !string.IsNullOrEmpty(location.Parent))
                {
                    childLocality = simIndex_ChildLocalityAtEnd;
                }

                // Strip the common words first from both search string and location display name
                string toMatch = StripCommonWords(location.DisplayName.ToLower(), commonWords);

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
            Dictionary<TDPLocation, double> filtered = levensteinMatchWhole.OrderByDescending(x => x.Value)
                .Concat(levensteinMatchPart.OrderByDescending(x => x.Value))
                .Concat(levensteinMatchChildLocality.OrderBy(x=>x.Key.DisplayName)) // show locations of type locality with parent locality at very end
                .Take(searchLimit)
                .ToDictionary(x => x.Key, x => x.Value);
            
            return filtered.Keys.ToList();
        }

        /// <summary>
        /// Searches for TDPLocations that are a close match to the supplied search string in database
        /// </summary>
        /// <param name="searchString">The name to search for</param>
        /// <param name="searchLimit">Maximum number of search results to return</param>
        /// <returns>TDPLocation[], contains suitable alternatives</returns>
        private static List<TDPLocation> GetAlternativeTDPLocationsFromDB(string searchString, int searchLimit)
        {
            List<TDPLocation> altLocations = new List<TDPLocation>();
            List<SqlParameter> paramList = new List<SqlParameter>();
            SqlParameter searchparam = new SqlParameter("@searchstring", (System.Data.SqlTypes.SqlString)searchString);
            SqlParameter limitparam = new SqlParameter("@maxRecords", (System.Data.SqlTypes.SqlInt32)searchLimit);
            paramList.Add(searchparam);
            paramList.Add(limitparam);
            using (SqlHelper helper = new SqlHelper())
            {
                try
                {
                    helper.ConnOpen(DB_LocationsDatabase);
                    using (SqlDataReader locationsDR = helper.GetReader(SP_GetAlternativeSuggestionList, paramList))
                    {
                        while (locationsDR.Read())
                        {
                            TDPLocation tempLocation = new TDPLocation();

                            tempLocation.DisplayName = locationsDR["DisplayName"].ToString();
                            tempLocation.TypeOfLocation = TDPLocationTypeHelper.GetTDPLocationType(locationsDR["Type"].ToString());
                            tempLocation.TypeOfLocationActual = TDPLocationTypeHelper.GetTDPLocationTypeActual(locationsDR["Type"].ToString());

                            switch (tempLocation.TypeOfLocation)
                            {
                                case TDPLocationType.Venue:
                                    tempLocation.ID = locationsDR["Naptan"].ToString();
                                    break;
                                case TDPLocationType.Station:
                                    tempLocation.ID = locationsDR["Naptan"].ToString();
                                    break;
                                case TDPLocationType.StationGroup:
                                    tempLocation.ID = locationsDR["DATASETID"].ToString();
                                    break;
                                case TDPLocationType.Locality:
                                    tempLocation.ID = locationsDR["LocalityID"].ToString();
                                    break;
                                case TDPLocationType.Postcode:
                                    tempLocation.ID = locationsDR["DisplayName"].ToString();
                                    break;
                                default:
                                    tempLocation.ID = locationsDR["DisplayName"].ToString();
                                    break;
                            }
                            altLocations.Add(tempLocation);
                        }
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format("Error occurred attempting to read Alternative suggestions locations data: {0}", ex.Message);

                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, message, ex));
                }
            }

            return altLocations;
        }

        /// <summary>
        /// Sorts and limits the number of the provided TDPLocations
        /// </summary>
        /// <param name="altLocations"></param>
        /// <returns></returns>
        private static List<TDPLocation> SortAndFilterAlternativeTDPLocations(List<TDPLocation> altLocations, int searchShowLimit)
        {
            List<TDPLocation> sortedLocations = new List<TDPLocation>();

            // Limits to apply for location types if more than max show limit exceeded
            int searchShowLimitGroup = Properties.Current[Keys.Max_SearchLocationsShowLimit_GroupStations].Parse(100);
            int searchShowLimitRail = Properties.Current[Keys.Max_SearchLocationsShowLimit_RailStations].Parse(5);
            int searchShowLimitCoach = Properties.Current[Keys.Max_SearchLocationsShowLimit_CoachStations].Parse(2);
            int searchShowLimitTram = Properties.Current[Keys.Max_SearchLocationsShowLimit_TramStations].Parse(5);
            int searchShowLimitFerry = Properties.Current[Keys.Max_SearchLocationsShowLimit_FerryStations].Parse(100);
            int searchShowLimitAirport = Properties.Current[Keys.Max_SearchLocationsShowLimit_AirportStations].Parse(100);
            int searchShowLimitPOI = Properties.Current[Keys.Max_SearchLocationsShowLimit_POIs].Parse(5);

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
                List<TDPLocation> locStationGroups = altLocations.FindAll(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationGroup; });
                List<TDPLocation> locStationsRail = altLocations.FindAll(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationRail; });
                List<TDPLocation> locLocalities = altLocations.FindAll(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.Locality; });
                List<TDPLocation> locPOIs = altLocations.FindAll(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.POI; });
                List<TDPLocation> locStationsCoach = altLocations.FindAll(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationCoach; });
                List<TDPLocation> locStationsTMU = altLocations.FindAll(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationTMU; });
                List<TDPLocation> locStationsFerry = altLocations.FindAll(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationFerry; });
                List<TDPLocation> locStationsAirport = altLocations.FindAll(delegate(TDPLocation loc) { return loc.TypeOfLocationActual == TDPLocationType.StationAirport; });
                List<TDPLocation> locOther = altLocations.FindAll(delegate(TDPLocation loc) { return (loc.TypeOfLocationActual == TDPLocationType.Unknown)
                                                                                                     || (loc.TypeOfLocationActual == TDPLocationType.Venue)
                                                                                                     || (loc.TypeOfLocationActual == TDPLocationType.Postcode)
                                                                                                     || (loc.TypeOfLocationActual == TDPLocationType.Station);
                                                                                            });
                #endregion

                #region Add the locations to the sorted list

                // - Exchange Groups
                if (applySortLimits && (locStationGroups.Count > searchShowLimitGroup))
                {
                    sortedLocations.AddRange(locStationGroups.Take(searchShowLimitGroup));
                }
                else
                {
                    sortedLocations.AddRange(locStationGroups);
                }

                // - Rail Stations
                if (applySortLimits && (locStationsRail.Count > searchShowLimitRail))
                {
                    sortedLocations.AddRange(locStationsRail.Take(searchShowLimitRail));
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
                    sortedLocations.AddRange(locPOIs.Take(searchShowLimitPOI));
                }
                else
                {
                    sortedLocations.AddRange(locPOIs);
                }

                // - Coach stations
                if (applySortLimits && (locStationsCoach.Count > searchShowLimitCoach))
                {
                    sortedLocations.AddRange(locStationsCoach.Take(searchShowLimitCoach));
                }
                else
                {
                    sortedLocations.AddRange(locStationsCoach);
                }

                // - TMU
                if (applySortLimits && (locStationsTMU.Count > searchShowLimitTram))
                {
                    sortedLocations.AddRange(locStationsTMU.Take(searchShowLimitTram));
                }
                else
                {
                    sortedLocations.AddRange(locStationsTMU);
                }

                // - Ferry
                if (applySortLimits && (locStationsFerry.Count > searchShowLimitFerry))
                {
                    sortedLocations.AddRange(locStationsFerry.Take(searchShowLimitFerry));
                }
                else
                {
                    sortedLocations.AddRange(locStationsFerry);
                }

                // - Airport
                if (applySortLimits && (locStationsAirport.Count > searchShowLimitAirport))
                {
                    sortedLocations.AddRange(locStationsAirport.Take(searchShowLimitAirport));
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
                        sortedLocations.InsertRange(localitiesIndex, locLocalities.Take(searchShowLimit - sortedLocations.Count));
                    }
                    else
                    {
                        sortedLocations.InsertRange(localitiesIndex, locLocalities);
                    }
                }

                #endregion
            }

            // Return the sorted locations, using the limit in case the above logic added too many
            return sortedLocations.Take(searchShowLimit).ToList();
        }

        /// <summary>
        /// Strips out common occurring words from the target string
        /// </summary>
        /// <param name="targetString">Target string from which common words need removing</param>
        /// <param name="commonWords">List of common words to be removed from the target string</param>
        /// <returns>Target string with all the common words removed i.e coach, station, rail, etc..</returns>
        private static string StripCommonWords(string targetString, List<string> commonWords)
        {
            foreach (string word in commonWords)
            {
                targetString = targetString.ToLower().Replace(word.ToLower(), "").Trim();
            }

            return targetString;
        }

        #endregion

        /// <summary>
        /// Loads all Locations data
        /// </summary>
        internal static void LoadLocations()
        {
            // Make load threadsafe
            if (!dataLocationsInitialised)
            {
                lock (dataInitialisedLock)
                {
                    // Check if locations should be cached, potentially a very large dataset so
                    // make it switchable
                    useLocationCache = Properties.Current[Keys.Cache_LoadLocations].Parse(false);

                    if (!dataLocationsInitialised)
                    {
                        // Load version
                        PopulateVersion();

                        // Load data if use cache flag has been set
                        if (useLocationCache)
                        {
                            PopulateLocationsData();
                        }

                        // Set to true here (prevents repeated attempts to load if it fails)
                        dataLocationsInitialised = true;
                    }
                }
            }
        }

        /// <summary>
        /// Loads all Postcode data
        /// </summary>
        internal static void LoadPostcodes()
        {
            // Make load threadsafe
            if (!dataPostcodesInitialised)
            {
                lock (dataInitialisedLock)
                {
                    // Check if postcodes shoule be cached, potentially a very large dataset so
                    // make it switchable
                    usePostcodeCache = Properties.Current[Keys.Cache_LoadPostcodes].Parse(false);

                    if (!dataPostcodesInitialised)
                    {
                        // Load data if use cache flag has been set
                        if (usePostcodeCache)
                        {
                            // MITESH MODI 28/06/2013:
                            // COMMENTED OUT UNTIL IMPLEMENTATION REQUIRED FOR TDP (DATABASE TABLES PROCS WILL NEED BE CREATED)
                        
                            //PopulatePostcodesData();
                        }

                        // Set to true here (prevents repeated attempts to load if it fails)
                        dataPostcodesInitialised = true;
                    }
                }
            }
        }
        #endregion
    }
}
