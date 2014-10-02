// ************************************************ 
// NAME                 : JSLocationProvider.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/08/2012
// DESCRIPTION          : Provides method to access location data in database and process the alias data
//                        defined in csv file
// ************************************************* 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using TransportDirect.Common.Logging;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Data.SqlClient;
using System.Collections;

namespace TransportDirect.UserPortal.LocationJsGenerator
{
    /// <summary>
    /// Provides method to access location data in database and process the alias data
    /// defined in csv file
    /// </summary>
    class JSLocationProvider
    {
        #region Private Fields

        private Dictionary<char, List<JSLocation>> locationsGroups = new Dictionary<char, List<JSLocation>>();

        // Database
        private SqlHelper sqlHelper = new SqlHelper();

        // Columns
        //private int ordinalID = sqlReader.GetOrdinal("ID");
        private int ordinalDATASETID = 0;
        private int ordinalParentID = 0;
        private int ordinalName = 0;
        private int ordinalDisplayName = 0;
        private int ordinalType = 0;
        private int ordinalNaptan = 0;
        private int ordinalLocality = 0;
        // Commented out incase they are added in the future
        //private int ordinalEasting = 0;
        //private int ordinalNorthing = 0;
        //private int ordinalNearestTOID = 0;
        //private int ordinalNearestPointE = 0;
        //private int ordinalNearestPointN = 0;
        //private int ordinalAdminAreaID = 0;
        //private int ordinalDistrictID = 0;

        #endregion

        #region Public Methods

        /// <summary>
        /// Processes alias data and creates a list of location data grouped by first letter of location
        /// </summary>
        /// <returns>Dictionary of lists of locations where key is first letter of location</returns>
        public Dictionary<char, List<JSLocation>> GetJsLocationData(JSGeneratorMode mode)
        {
            LoadLocationData(mode);

            LoadAliasData(mode);

            return locationsGroups;
        }

        #endregion

        #region Private Methods

        #region Locations data (from database)

        /// <summary>
        /// Loads location data from the database and stores in dictionary 
        /// keyed on alphabet characters (a to z), grouping locations by the first 
        /// character in their display name
        /// </summary>
        private void LoadLocationData(JSGeneratorMode mode)
        {
            Trace.Write(
                      new OperationalEvent(
                          TDEventCategory.Database,
                          TDTraceLevel.Verbose,
                          string.Format("Loading locations data for[{0}]",mode)));

            SqlDataReader sqlReader = null;
            try
            {
                Trace.Write(
                      new OperationalEvent(
                          TDEventCategory.Database,
                          TDTraceLevel.Verbose,
                          string.Format("Opening database connection to [{0}] and executing stored procedure[{1}]",
                            JSGeneratorSettings.LocationsDatabase, JSGeneratorSettings.LocationsStoredProcedure)));

                sqlHelper.ConnOpen(JSGeneratorSettings.LocationsDatabase);

                // Call stored procedure
                sqlReader = sqlHelper.GetReader(JSGeneratorSettings.LocationsStoredProcedure, new Hashtable());

                // Assign the column ordinals
                SetColumnOrdinals(sqlReader);
                
                JSLocation location = null;
                char charKey = 'a';

                int locationCount = 0; // For logging

                while (sqlReader.Read())
                {
                    // Read the database values returned
                    location = ReadLocation(sqlReader, string.Empty);
                    
                    if (LocationValid(location, mode))
                    {
                        locationCount++;

                        // Add to the character grouped dictionary
                        charKey = location.DisplayName.ToLower()[0];

                        if (!locationsGroups.ContainsKey(charKey))
                        {
                            locationsGroups.Add(charKey, new List<JSLocation>());
                        }

                        locationsGroups[charKey].Add(location);
                    }
                }

                Trace.Write(
                      new OperationalEvent(
                          TDEventCategory.Business,
                          TDTraceLevel.Info,
                          string.Format("Locations data loaded for[{0}] count[{1}] grouped in parts[{2}]",
                            mode, locationCount, locationsGroups.Keys.Count)));
            }
            catch (Exception ex)
            {
                string message = string.Format("Error occurred attempting to load locations from the database[{0}] storeproc[{1}], Exception Message[{2}] StackTrace[{3}]",
                                    JSGeneratorSettings.LocationsDatabase, JSGeneratorSettings.LocationsStoredProcedure, ex.Message, ex.StackTrace);

                Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message));

                throw new TDException(message, true, TDExceptionIdentifier.LJSGenLocatonDataLoadFailed);
            }
            finally
            {
                //close the database connections
                if (sqlReader != null)
                    sqlReader.Close();

                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
        }

        #endregion

        #region Alias data (from file)

        /// <summary>
        /// Loads alias data from the alias file defined
        /// </summary>
        private void LoadAliasData(JSGeneratorMode mode)
        {
            Trace.Write(
                      new OperationalEvent(
                          TDEventCategory.Database,
                          TDTraceLevel.Verbose,
                          string.Format("Loading alias data for[{0}]", mode)));
            try
            {
                if (!string.IsNullOrEmpty(JSGeneratorSettings.LocationsAliasFile))
                {
                    List<string> allLines = new List<string>(File.ReadAllLines(JSGeneratorSettings.LocationsAliasFile));

                    // skip the heading row
                    if (allLines.Count > 1)
                    {
                        allLines.RemoveAt(0);
                    }

                    string[] aliasEntry;

                    // Loop through each line and add an alias location
                    foreach (string line in allLines)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            aliasEntry = line.Split(',');

                            if (aliasEntry.Length == 3)
                            {
                                AddAliasLocation(aliasEntry[0], aliasEntry[1], aliasEntry[2], mode);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Error loading alias data: {0}", ex.StackTrace);

                Trace.Write(
                  new OperationalEvent(
                      TDEventCategory.Business,
                      TDTraceLevel.Error,
                      message));

                throw new TDException(message, true, TDExceptionIdentifier.LJSGenAliasDataLoadFailed);
            }
        }

        /// <summary>
        /// Adds alias location to the private location data dictionary object
        /// </summary>
        /// <param name="dataType">Type of Location data i.e. R = Rail</param>
        /// <param name="alias">Alias for the location</param>
        private void AddAliasLocation(string dataType, string dataId, string alias, JSGeneratorMode mode)
        {
            try
            {
                if (!string.IsNullOrEmpty(dataId))
                    AddAlias(dataType, dataId, alias, mode);
            }
            catch (Exception ex)
            {
                string message = string.Format("Error adding alias location with datatype: {0}, dataId: {1} and alias: {2} - {3}",
                    dataType,
                    dataId,
                    alias,
                    ex.StackTrace);

                Trace.Write(
                  new OperationalEvent(
                      TDEventCategory.Business,
                      TDTraceLevel.Error,
                      message));

                throw new TDException(message, true, TDExceptionIdentifier.LJSGenAliasLocatonAddFailed);
            }
        }

        /// <summary>
        /// Adds alias location identified by NaPTAN, locality or datasetid
        /// </summary>
        /// <param name="dataType">Type of data i.e. R= Rail </param>
        /// <param name="dataId">Data identifier i.e. NaPTAN, Locality</param>
        /// <param name="alias">Alias for the location</param>
        private void AddAlias(string dataType, string dataId, string alias, JSGeneratorMode mode)
        {
            // Check if alias location exists in the database table before adding
            SqlDataReader sqlReader = null;
            try
            {
                sqlHelper.ConnOpen(JSGeneratorSettings.LocationsDatabase);

                // Parameters
                Hashtable parameters = new Hashtable();
                parameters.Add("@id", dataId);

                // Call stored procedure
                sqlReader = sqlHelper.GetReader(JSGeneratorSettings.LocationStoredProcedure, parameters);

                // Assign the column ordinals
                SetColumnOrdinals(sqlReader);

                JSLocation location = null;
                char charKey = 'a';
                bool found = false;

                while (sqlReader.Read())
                {
                    found = true;

                    // Read the database values returned
                    location = ReadLocation(sqlReader, alias);

                    if (LocationValid(location, mode))
                    {
                        // Add to the character grouped dictionary
                        charKey = alias.ToLower()[0];

                        if (!locationsGroups.ContainsKey(charKey))
                        {
                            locationsGroups.Add(charKey, new List<JSLocation>());
                        }

                        locationsGroups[charKey].Add(location);

                        Trace.Write( new OperationalEvent(
                          TDEventCategory.Database,
                          TDTraceLevel.Verbose,
                          string.Format("Found alias[{0}] in locations database table and added to [{1}] collection", alias, charKey)));
                    }
                }

                if (!found)
                {
                    Trace.Write(
                      new OperationalEvent(
                          TDEventCategory.Database,
                          TDTraceLevel.Verbose,
                          string.Format("Not found alias[{0}] in locations database table", alias)));
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Error occurred attempting to load locations from the database[{0}] storeproc[{1}], Exception Message[{2}] StackTrace[{3}]",
                                    JSGeneratorSettings.LocationsDatabase, JSGeneratorSettings.LocationStoredProcedure, ex.Message, ex.StackTrace);

                Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, message));

                throw new TDException(message, true, TDExceptionIdentifier.LJSGenLocatonDataLoadFailed);
            }
            finally
            {
                //close the database connections
                if (sqlReader != null)
                    sqlReader.Close();

                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
        }

        #endregion

        /// <summary>
        /// Sets the column ordinals
        /// </summary>
        /// <param name="sqlReader"></param>
        private void SetColumnOrdinals(SqlDataReader sqlReader)
        {
            //ordinalID = sqlReader.GetOrdinal("ID");
            ordinalDATASETID = sqlReader.GetOrdinal("DATASETID");
            ordinalParentID = sqlReader.GetOrdinal("ParentID");
            ordinalName = sqlReader.GetOrdinal("Name");
            ordinalDisplayName = sqlReader.GetOrdinal("DisplayName");
            ordinalType = sqlReader.GetOrdinal("Type");
            ordinalNaptan = sqlReader.GetOrdinal("Naptan");
            ordinalLocality = sqlReader.GetOrdinal("LocalityID");
            //ordinalEasting = sqlReader.GetOrdinal("Easting");
            //ordinalNorthing = sqlReader.GetOrdinal("Northing");
            //ordinalNearestTOID = sqlReader.GetOrdinal("NearestTOID");
            //ordinalNearestPointE = sqlReader.GetOrdinal("NearestPointE");
            //ordinalNearestPointN = sqlReader.GetOrdinal("NearestPointN");
            //ordinalAdminAreaID = sqlReader.GetOrdinal("AdminAreaID");
            //ordinalDistrictID = sqlReader.GetOrdinal("DistrictID");
        }

        /// <summary>
        /// Reads a database location
        /// </summary>
        /// <param name="sqlReader"></param>
        private JSLocation ReadLocation(SqlDataReader sqlReader, string alias)
        {
            string datasetID = GetString(sqlReader, ordinalDATASETID);
            string parentID = GetString(sqlReader, ordinalParentID);
            string name = GetString(sqlReader, ordinalName);
            string displayName = GetString(sqlReader, ordinalDisplayName).Trim();
            string type = GetString(sqlReader, ordinalType);
            string naptan = GetString(sqlReader, ordinalNaptan);
            string locality = GetString(sqlReader, ordinalLocality);

            // Create location
            JSLocation location = new JSLocation(datasetID, parentID, name, displayName, alias, type, naptan, locality);

            return location;
        }

        /// <summary>
        /// Returns true if the location is valid for the supplied application mode
        /// </summary>
        public bool LocationValid(JSLocation location, JSGeneratorMode mode)
        {
            if (location != null && !string.IsNullOrEmpty(location.DisplayName))
            {
                switch (mode)
                {
                    case JSGeneratorMode.TDPNoGroupsNoLocalitiesNoPOIs: // Do not show locality locations in the js dropdown
                            if ((location.LocationType != JSLocationType.Locality) 
                                && (location.LocationType != JSLocationType.Group)
                                && (location.LocationType != JSLocationType.POI))
                            {
                                return true;
                            }
                        break;
                    case JSGeneratorMode.TDPNoLocalitiesNoPOIs:
                        if ((location.LocationType != JSLocationType.Locality)
                            && (location.LocationType != JSLocationType.POI))
                        {
                            return true;
                        }
                        break;
                    case JSGeneratorMode.TDPNoLocalities:
                        if (location.LocationType != JSLocationType.Locality)
                        {
                            return true;
                        }
                        break;
                    case JSGeneratorMode.TDPNoGroups:
                        if (location.LocationType != JSLocationType.Group)
                        {
                            return true;
                        }
                        break;
                    case JSGeneratorMode.TDPDefault:
                    default:
                        return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Does a null check for the data reader column value and returns empty string if found null 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public string GetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        #endregion
    }
}