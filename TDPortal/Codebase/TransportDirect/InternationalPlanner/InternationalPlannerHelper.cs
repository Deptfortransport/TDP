// *********************************************** 
// NAME			: InternationalPlannerHelper.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Helper Class which contains logic to call the international database to retrieve data
//              : required for international journey planning
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalPlannerHelper.cs-arc  $
//
//   Rev 1.21   Sep 14 2010 18:04:02   rbroddle
//fixed problem with effective dates
//Resolution for 5551: TDExtra journeys cache sometimes returning invalid results
//
//   Rev 1.19   Jun 18 2010 09:30:12   RBroddle
//Corrected comment for UpdateValidForDateJourney method.
//
//   Rev 1.18   Jun 11 2010 16:42:18   rbroddle
//Updated to filter on effective dates when retrieving journeys from the cache.
//Resolution for 5551: TDExtra journeys cache sometimes returning invalid results
//
//   Rev 1.17   Apr 23 2010 15:56:10   mmodi
//Added method to return valid route destination cities for the specified city.
//Resolution for 5521: TD Extra - Drop Down List Change - CCN0575
//
//   Rev 1.16   Mar 16 2010 14:42:50   mmodi
//Updated to state distance is in metres
//Resolution for 5461: TD Extra - Code review changes
//
//   Rev 1.15   Mar 04 2010 16:38:02   mmodi
//Perform a MinDate check before adding/subtracting a timespan
//Resolution for 5436: TD Extra - Error when subtracting a timezpan from a Zero datatime
//
//   Rev 1.14   Mar 01 2010 11:51:06   mmodi
//Corrected getting coach journeys from the data cache
//Resolution for 5424: TD Extra - Reset data cache needed
//
//   Rev 1.13   Feb 24 2010 16:42:50   mmodi
//Correctly add on the arrival day
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.12   Feb 24 2010 14:48:06   mmodi
//Populate the Service Features property of the detail
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.11   Feb 22 2010 17:33:36   mmodi
//Updated call to get Air journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Feb 21 2010 12:35:24   mmodi
//Corrected calling points clear for coach and rail journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Feb 19 2010 15:57:50   mmodi
//Updated to add distances to journey details
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 18 2010 16:37:50   mmodi
//Updated following database tables change
//
//   Rev 1.7   Feb 18 2010 15:53:06   mmodi
//Corrected setting of stop naptans for rail and coach journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 17 2010 16:50:40   mmodi
//Corrected saving is permitted journey data
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Feb 17 2010 12:24:00   mmodi
//Updated to correctly return coach journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Feb 16 2010 17:44:36   mmodi
//Added is permitted journeys and restructured creating intermediate legs for journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 12 2010 09:40:48   mmodi
//Updated to plan train journeys and save journeys to cache
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 09 2010 09:53:04   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 04 2010 10:26:10   mmodi
//Updates as part of development
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:38   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Helper Class for the International planner
    /// </summary>
    public class InternationalPlannerHelper
    {
        #region Private members 

        private SqlHelper sqlHelper = new SqlHelper();
        private const SqlHelperDatabase database = SqlHelperDatabase.InternationalDataDB;

        /// <summary>
        /// Stored procedures
        /// </summary>
        private const string SP_GetInternationalStop = "GetInternationalStops";
        private const string SP_GetInternationalCity = "GetInternationalCity";
        private const string SP_GetInternationalJourneyAir = "GetInternationalJourneysAir";
        private const string SP_GetInternationalJourneyCoach = "GetInternationalJourneysCoach";
        private const string SP_GetInternationalJourneyRail = "GetInternationalJourneysRail";
        private const string SP_GetInternationalJourneyCar = "GetInternationalJourneysCar";
        private const string SP_GetInterchangeTime = "GetInterchangeTime";
        private const string SP_GetInternationalCityTransfer = "GetInternationalCityTransfer";
        private const string SP_GetInternationalStopDistance = "GetInternationalStopDistances";
        private const string SP_GetPermittedInternationalJourney = "IsPermittedInternationalJourney";
        

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalPlannerHelper()
		{

		}

		#endregion

        #region Public methods

        /// <summary>
        /// Method returns true if the Origin to Destination country code is permitted for an international
        /// journey plan
        /// </summary>
        public bool IsPermittedInternationalJourney(string originCountryCode, string destinationCountryCode)
        {
            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Check if permitted country data has been setup
            if (!intlData.IsPermittedCountriesDataSet())
            {
                #region Get permitted country data

                SqlDataReader sqlReader = null;
                try
                {
                    Hashtable parameters = new Hashtable();

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetPermittedInternationalJourney));
                    Logger.Write(oe);

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetPermittedInternationalJourney, parameters);

                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalDepartureCountryCode = sqlReader.GetOrdinal("DepartureCountryCode");
                    int ordinalArrivalCountryCode = sqlReader.GetOrdinal("ArrivalCountryCode");
                    #endregion

                    // Any values returned, then its a permitted international journey country combination
                    while (sqlReader.Read())
                    {
                        #region Read data
                        // Read the database values returned
                        string departureCountryCode = sqlReader.GetString(ordinalDepartureCountryCode);
                        string arrivalCountryCode = sqlReader.GetString(ordinalArrivalCountryCode);

                        #endregion

                        #region Update data dictionary cache

                        // Update the data for future access
                        intlData.AddPermittedCountry(departureCountryCode, arrivalCountryCode, true);

                        #endregion
                    }

                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetPermittedInternationalJourney, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve permitted international country combinations data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingPermittedInternationalJourneys);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve permitted international country combinations data from the database[{0}], TDException Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingPermittedInternationalJourneys);
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #endregion
            }

            return intlData.GetPermittedCountry(originCountryCode, destinationCountryCode);
        }

        /// <summary>
        /// Method which returns all the valid cities the specified city id can travel to
        /// </summary>
        public InternationalCity[] GetValidRouteCitiesForInternationCity(string cityId)
        {
            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Check if city route matrix data has been setup
            if (!intlData.IsInternationalCityRouteMatrixDataSet())
            {
                // Force data dictionary to re-load data
                intlData.LoadValidRoutesForInternationalCities();
            }

            return intlData.GetInternationalCityRouteMatrix(cityId);
        }

        /// <summary>
        /// Method which retrieves InternationalStops from the InternationalData database for 
        /// the specified list of NaPTANs
        /// </summary>
        /// <param name="naptans"></param>
        /// <returns></returns>
        public InternationalStop[] GetInternationalStop(string[] stopNaptans, bool logResults)
        {
            // Return value
            List<InternationalStop> internationalStops = new List<InternationalStop>();

            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Temp list to retrieve stops from database if not in the data cache
            List<string> stopNaptansToFind = new List<string>();

            // Check data cache fist
            foreach (string stopNaptan in stopNaptans)
            {
                if (!string.IsNullOrEmpty(stopNaptan))
                {
                    InternationalStop intlStop = intlData.GetInternationalStop(stopNaptan.Trim());

                    if (intlStop != null)
                    {
                        internationalStops.Add(intlStop);
                    }
                    else
                    {
                        // Stop is not in the data cache yet, so flag as needed
                        stopNaptansToFind.Add(stopNaptan.Trim());

                        // and add a temporary default value to data cache to prevent future database calls
                        intlStop = CreateInternationalStop(
                            stopNaptan, stopNaptan, InternationalModeType.None, string.Empty, 0, 0, 
                            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                            string.Empty, null);
                        
                        intlData.AddInternationalStop(intlStop);
                    }
                }
            }
            
            #region Get stop data from database

            SqlDataReader sqlReader = null;
            try
            {
                #region Build up naptans database query list
                // Build up the sql request
                StringBuilder sbStopNaptans = new StringBuilder();

                foreach (string stopNaptan in stopNaptansToFind)
                {
                    // Stored procedure accepts a list of naptans in format ''9200ABC'',''...
                    if (!string.IsNullOrEmpty(stopNaptan))
                    {
                        sbStopNaptans.Append("'");
                        sbStopNaptans.Append(stopNaptan);
                        sbStopNaptans.Append("',");
                    }
                }

                // Trim off the last comma
                string sqlParameterNaptans = sbStopNaptans.ToString().TrimEnd(',');

                #endregion

                if (!string.IsNullOrEmpty(sqlParameterNaptans))
                {
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@StopNaPTANs", sqlParameterNaptans);

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInternationalStop));
                    Logger.Write(oe);

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInternationalStop, parameters);


                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalStopCode = sqlReader.GetOrdinal("StopCode");
                    int ordinalStopNaptan = sqlReader.GetOrdinal("StopNaPTAN");
                    int ordinalStopType = sqlReader.GetOrdinal("StopType");
                    int ordinalStopName = sqlReader.GetOrdinal("StopName");
                    int ordinalStopOSGREasting = sqlReader.GetOrdinal("StopOSGREasting");
                    int ordinalStopOSGRNorthing = sqlReader.GetOrdinal("StopOSGRNorthing");
                    int ordinalStopTerminal = sqlReader.GetOrdinal("StopTerminalNumber");
                    int ordinalStopInformationURL = sqlReader.GetOrdinal("StopInformationURL");
                    int ordinalStopInformationDesc = sqlReader.GetOrdinal("StopInformationDescription");
                    int ordinalStopDeparturesURL = sqlReader.GetOrdinal("StopDeparturesURL");
                    int ordinalStopDeparturesDesc = sqlReader.GetOrdinal("StopDeparturesDescription");
                    int ordinalStopAccessabilityURL = sqlReader.GetOrdinal("StopAccessabilityURL");
                    int ordinalStopAccessabilityDesc = sqlReader.GetOrdinal("StopAccessabilityDescription");

                    int ordinalCountryCode = sqlReader.GetOrdinal("CountryCode");
                    int ordinalCountryCodeIANA = sqlReader.GetOrdinal("CountryCodeIANA");
                    int ordinalCountryAdminCodeUIC = sqlReader.GetOrdinal("CountryAdminCodeUIC");
                    int ordinalCountryTimeZoneHours = sqlReader.GetOrdinal("CountryTimeZoneHours");
                    #endregion

                    while (sqlReader.Read())
                    {
                        #region Read data
                        // Read the database values returned
                        string stopCode = sqlReader.GetString(ordinalStopCode);
                        string stopNaptan = sqlReader.GetString(ordinalStopNaptan);
                        string stopType = sqlReader.GetString(ordinalStopType);
                        string stopName = sqlReader.GetString(ordinalStopName);
                        int stopOSGREasting = sqlReader.GetInt32(ordinalStopOSGREasting);
                        int stopOSGRNorthing = sqlReader.GetInt32(ordinalStopOSGRNorthing);
                        string stopTerminal = sqlReader.GetString(ordinalStopTerminal);
                        string stopInfoURL = sqlReader.GetString(ordinalStopInformationURL);
                        string stopInfoDesc = sqlReader.GetString(ordinalStopInformationDesc);
                        string stopDeptURL = sqlReader.GetString(ordinalStopDeparturesURL);
                        string stopDeptDesc = sqlReader.GetString(ordinalStopDeparturesDesc);
                        string stopAccessURL = sqlReader.GetString(ordinalStopAccessabilityURL);
                        string stopAccessDesc = sqlReader.GetString(ordinalStopAccessabilityDesc);

                        string countryCode = sqlReader.GetString(ordinalCountryCode);
                        string countryCodeIANA = sqlReader.GetString(ordinalCountryCodeIANA);
                        string countryAdminCodeUIC = sqlReader.GetString(ordinalCountryAdminCodeUIC);
                        int countryTimeZone = sqlReader.GetInt16(ordinalCountryTimeZoneHours);
                        #endregion

                        // Parse the stop mode type
                        InternationalModeType stopModeType = GetModeTypeForStopType(stopType, stopCode);

                        // Build the InternationalCountry
                        InternationalCountry intlCountry = CreateInternationalCountry(countryCode, countryCodeIANA, countryAdminCodeUIC, countryTimeZone);

                        // Build the InternationalStop
                        InternationalStop intlStop = CreateInternationalStop(
                            stopCode, stopNaptan, stopModeType, stopName, stopOSGREasting, stopOSGRNorthing,
                            stopTerminal, stopInfoURL, stopInfoDesc, stopDeptURL, stopDeptDesc, stopAccessURL,
                            stopAccessDesc, intlCountry);

                        // Add to the return result list
                        internationalStops.Add(intlStop);

                        #region Update data dictionary cache

                        // Update the data for future access
                        intlData.AddInternationalStop(intlStop);
                        
                        #endregion

                        #region Log data result
                        if (logResults)
                        {
                            oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format(
                                    "International stop - ( stopCode[{0}] stopNaptan[{1}] stopModeType[{2}] stopName[{3}] " +
                                    "countryCode[{4}] stopOSGREasting[{5}] stopOSGRNorthing[{6}] stopTerminal[{7}] ). ",
                                    stopCode.PadRight(4),
                                    stopNaptan.PadRight(8),
                                    stopModeType.ToString().PadRight(5),
                                    stopName.PadRight(16),
                                    countryCode.PadRight(3),
                                    stopOSGREasting, stopOSGRNorthing,
                                    stopTerminal.PadRight(4)));
                            Logger.Write(oe);
                        }
                        #endregion
                    }
                }
                // else no naptan string created, this will therefore return an empty stops array. 
                // Let caller deal with it.
            }
            #region Error handling
            catch (SqlException sqlEx)
            {
                // SQLHelper does not catch SqlException so catch here
                throw new TDException(string.Format(
                    "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                    SP_GetInternationalStop, sqlEx.Message), 
                    sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
            }
            catch (TDException tdEx)
            {
                throw new TDException(string.Format(
                    "Error occurred attempting to retrieve InternationalStop data from the database[{0}], TDException Message[{1}].",
                    database,
                    tdEx.Message),
                    tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInternationalStop);
            }
            catch (Exception ex)
            {
                throw new TDException(string.Format(
                    "Error occurred attempting to retrieve InternationalStop data from the database[{0}], Exception Message[{1}].",
                    database,
                    ex.Message),
                    ex, false, TDExceptionIdentifier.IPErrorRetrievingInternationalStop);
            }
            finally
            {
                //close the database connections
                if (sqlReader != null)
                    sqlReader.Close();

                if (sqlHelper.ConnIsOpen)
                    sqlHelper.ConnClose();
            }
            #endregion

            #endregion

            return internationalStops.ToArray();
        }
                
        /// <summary>
        /// Method which retrieves international city data from the InternationalData database for 
        /// the specified City ID and parses the result into an InternationalCity object
        /// </summary>
        /// <param name="naptans"></param>
        /// <returns></returns>
        public InternationalCity GetInternationalCity(string cityID, bool logResults)
        {
            // Return value
            InternationalCity intlCity = null;

            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Check data cache fist
            intlCity = intlData.GetInternationalCity(cityID);

            if (intlCity == null)
            {
                // City not in data cache yet

                #region Get city data from database

                SqlDataReader sqlReader = null;
                try
                {
                    // Build up the sql request
                    if (!string.IsNullOrEmpty(cityID))
                    {
                        Hashtable parameters = new Hashtable();
                        parameters.Add("@CityId", cityID);

                        // Log
                        OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                    string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                        database, SP_GetInternationalCity));
                        Logger.Write(oe);

                        sqlHelper.ConnOpen(database);

                        // Call stored procedure
                        sqlReader = sqlHelper.GetReader(SP_GetInternationalCity, parameters);

                        #region Column ordinals
                        // Assign the column ordinals
                        int ordinalCityId = sqlReader.GetOrdinal("CityId");
                        int ordinalCityName = sqlReader.GetOrdinal("CityName");
                        int ordinalCityOSGREasting = sqlReader.GetOrdinal("CityOSGREasting");
                        int ordinalCityOSGRNorthing = sqlReader.GetOrdinal("CityOSGRNorthing");
                        int ordinalCityURL = sqlReader.GetOrdinal("CityURL");

                        int ordinalCountryCode = sqlReader.GetOrdinal("CountryCode");
                        int ordinalCountryCodeIANA = sqlReader.GetOrdinal("CountryCodeIANA");
                        int ordinalCountryAdminCodeUIC = sqlReader.GetOrdinal("CountryAdminCodeUIC");
                        int ordinalCountryTimeZoneHours = sqlReader.GetOrdinal("CountryTimeZoneHours");
                        #endregion

                        // Assume only 1 record is found
                        while (sqlReader.Read())
                        {
                            #region Read data
                            // Read the database values returned
                            string cityId = Convert.ToString(sqlReader.GetInt32(ordinalCityId));
                            string cityName = sqlReader.GetString(ordinalCityName);
                            int cityOSGREasting = sqlReader.GetInt32(ordinalCityOSGREasting);
                            int cityOSGRNorthing = sqlReader.GetInt32(ordinalCityOSGRNorthing);
                            string cityURL = sqlReader.GetString(ordinalCityURL);

                            string countryCode = sqlReader.GetString(ordinalCountryCode);
                            string countryCodeIANA = sqlReader.GetString(ordinalCountryCodeIANA);
                            string countryAdminCodeUIC = sqlReader.GetString(ordinalCountryAdminCodeUIC);
                            int countryTimeZone = sqlReader.GetInt16(ordinalCountryTimeZoneHours);
                            #endregion

                            // Build the InternationalCountry
                            InternationalCountry intlCountry = CreateInternationalCountry(countryCode, countryCodeIANA, countryAdminCodeUIC, countryTimeZone);

                            // Build the InternationalCity
                            intlCity = CreateInternationalCity(
                                cityID, cityName, cityOSGREasting, cityOSGRNorthing, cityURL, intlCountry);

                            #region Update data dictionary cache

                            // Update the dictionary for future access
                            intlData.AddInternationalCity(intlCity);

                            #endregion

                            #region Log data result
                            if (logResults)
                            {
                                oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                    string.Format(
                                        "International city - ( cityId[{0}] cityName[{1}] " +
                                        "countryCode[{2}] cityOSGREasting[{3}] cityOSGRNorthing[{4}] ). ",
                                        cityId.PadRight(4),
                                        cityName.PadRight(16),
                                        countryCode.PadRight(3),
                                        cityOSGREasting, cityOSGRNorthing));
                                Logger.Write(oe);
                            }
                            #endregion
                        }
                    }
                    // else no city id provided, this will therefore return a null city.
                    // Let caller deal with it.
                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalCity, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve InternationalCity data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInternationalCity);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve InternationalCity data from the database[{0}], Exception Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingInternationalCity);
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #endregion
            }

            return intlCity;
        }

        /// <summary>
        /// Method which retrieves Air schedule journeys from the database and creates 
        /// InternationalJourneys using the data
        /// </summary>
        /// <returns></returns>
        public InternationalJourney[] GetInternationalJourneyAir(InternationalStop originStop, InternationalStop destinationStop, string originCityID, string destinationCityID, DateTime outwardDateTime, bool logResults)
        {

            Logger.Write(
                new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("International journey - Planning {0} Journey From[City:{1} Naptan:{2}]  To[City:{3} Naptan:{4}]",
                            InternationalModeType.Air,
                            originCityID, originStop.StopNaptan, destinationCityID, destinationStop.StopNaptan)));

            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Check data cache fist
            InternationalJourney[] intlJourneys = intlData.GetInternationalJourneys(originCityID, destinationCityID, originStop.StopNaptan, destinationStop.StopNaptan, InternationalModeType.Air);

            if (intlJourneys == null)
            {
                // No journeys exist for this origin and destination city/stop combination in cache, call database

                #region Get journey data from database

                // Temp array to pass planned journeys to the data cache for future access
                List<InternationalJourney> tmpIntlJourneys = new List<InternationalJourney>();

                SqlDataReader sqlReader = null;
                try
                {
                    // Build up the sql request
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@OriginStopCode", originStop.StopCode);
                    parameters.Add("@DestinationStopCode", destinationStop.StopCode);
                    parameters.Add("@TerminalFrom", originStop.StopTerminal);
                    parameters.Add("@TerminalTo", destinationStop.StopTerminal);
                    parameters.Add("@OutwardDateTime", outwardDateTime);

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInternationalJourneyAir));
                    Logger.Write(oe);

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInternationalJourneyAir, parameters);

                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalDepartureStopNaptan = sqlReader.GetOrdinal("DepartureStopNaptan");
                    int ordinalArrivalStopNaptan = sqlReader.GetOrdinal("ArrivalStopNaptan");
                    int ordinalDepartureTime = sqlReader.GetOrdinal("DepartureTime");
                    int ordinalArrivalTime = sqlReader.GetOrdinal("ArrivalTime");
                    int ordinalArrivalDay = sqlReader.GetOrdinal("ArrivalDay");
                    int ordinalDaysOfOperation = sqlReader.GetOrdinal("DaysOfOperation");
                    int ordinalCarrierCode = sqlReader.GetOrdinal("CarrierCode");
                    int ordinalFlightNumber = sqlReader.GetOrdinal("FlightNumber");
                    int ordinalAircraftTypeCode = sqlReader.GetOrdinal("AircraftTypeCode");
                    int ordinalTerminalNumberFrom = sqlReader.GetOrdinal("TerminalNumberFrom");
                    int ordinalTerminalNumberTo = sqlReader.GetOrdinal("TerminalNumberTo");
                    int ordinalEffectiveFromDate = sqlReader.GetOrdinal("EffectiveFromDate");
                    int ordinalEffectiveToDate = sqlReader.GetOrdinal("EffectiveToDate");
                    #endregion

                    #region Variable to hold values
                    string departureStopNaptan = string.Empty;
                    string arrivalStopNaptan = string.Empty;
                    TimeSpan departureTimespan = new TimeSpan();
                    TimeSpan arrivalTimespan = new TimeSpan();
                    int arrivalDay = 0;
                    string daysOfOperation = string.Empty;
                    string carrierCode = string.Empty;
                    string flightNumber = string.Empty;
                    string aircraftTypeCode = string.Empty;
                    string terminalNumberFrom = string.Empty;
                    string terminalNumberTo = string.Empty;
                    DateTime effectiveFromDate = new DateTime();
                    DateTime effectiveToDate = new DateTime();

                    // Used for logging number of journey
                    int index = 0;
                    #endregion

                    while (sqlReader.Read())
                    {
                        #region Read data
                        // Read the database values returned, check for nulls as all columns may not
                        // have a value
                        departureStopNaptan = sqlReader.GetString(ordinalDepartureStopNaptan);
                        arrivalStopNaptan = sqlReader.GetString(ordinalArrivalStopNaptan);
                        departureTimespan = sqlReader.GetTimeSpan(ordinalDepartureTime);
                        arrivalTimespan = sqlReader.GetTimeSpan(ordinalArrivalTime);
                        arrivalDay = sqlReader.GetInt32(ordinalArrivalDay);

                        if (!sqlReader.IsDBNull(ordinalDaysOfOperation))
                        {
                            daysOfOperation = sqlReader.GetString(ordinalDaysOfOperation);
                        }
                        else
                        {
                            daysOfOperation = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalCarrierCode))
                        {
                            carrierCode = sqlReader.GetString(ordinalCarrierCode);
                        }
                        else
                        {
                            carrierCode = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalFlightNumber))
                        {
                            flightNumber = sqlReader.GetString(ordinalFlightNumber);
                        }
                        else
                        {
                            flightNumber = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalAircraftTypeCode))
                        {
                            aircraftTypeCode = sqlReader.GetString(ordinalAircraftTypeCode);
                        }
                        else
                        {
                            aircraftTypeCode = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalTerminalNumberFrom))
                        {
                            terminalNumberFrom = sqlReader.GetString(ordinalTerminalNumberFrom);
                        }
                        else
                        {
                            terminalNumberFrom = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalTerminalNumberTo))
                        {
                            terminalNumberTo = sqlReader.GetString(ordinalTerminalNumberTo);
                        }
                        else
                        {
                            terminalNumberTo = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalEffectiveFromDate))
                        {
                            effectiveFromDate = sqlReader.GetDateTime(ordinalEffectiveFromDate);
                        }
                        else
                        {
                            effectiveFromDate = DateTime.MinValue;
                        }

                        if (!sqlReader.IsDBNull(ordinalEffectiveToDate))
                        {
                            effectiveToDate = sqlReader.GetDateTime(ordinalEffectiveToDate);
                        }
                        else
                        {
                            effectiveToDate = DateTime.MaxValue;
                        }

                        #endregion

                        // Parse the Days of operation
                        DayOfWeek[] listDaysOfOperation = GetDaysOfOperation(daysOfOperation, departureStopNaptan, arrivalStopNaptan);

                        // Update the journey dates and times
                        DateTime departureDateTime;
                        DateTime arrivalDateTime;
                        GetJourneyDateTime(outwardDateTime, departureTimespan, 0, arrivalTimespan, arrivalDay,
                            out departureDateTime, out arrivalDateTime);

                        // Air journey has no intermediate stops so there is just one journey detail leg
                        InternationalJourneyDetail[] journeyDetails = new InternationalJourneyDetail[1];
                        journeyDetails[0] = CreateInternationalJourneyDetail(
                            InternationalJourneyDetailType.TimedAir,
                            departureStopNaptan, arrivalStopNaptan, departureDateTime, arrivalDateTime,
                            carrierCode, flightNumber, aircraftTypeCode,
                            terminalNumberFrom, terminalNumberTo);

                        // Build the InternationalJourney
                        InternationalJourney intlJourney = CreateInternationalJourney(
                            originCityID, destinationCityID,
                            departureStopNaptan, arrivalStopNaptan,
                            departureDateTime, arrivalDateTime, listDaysOfOperation,
                            InternationalModeType.Air, journeyDetails);

                        //Add effective date and time
                        intlJourney.EffectiveFromDate = effectiveFromDate;
                        intlJourney.EffectiveToDate = effectiveToDate;

                        // Add to the temp array to be added to the data cache
                        tmpIntlJourneys.Add(intlJourney);

                        #region Log data result
                        if (logResults)
                        {
                            index++;

                            oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format(
                                    "International journey - {0}( mode[{1}] departureStopNaptan[{2}] arrivalStopNaptan[{3}] departTime[{4}] " +
                                    "arrivalTime[{5}] daysOfOperation[{6}] carrierCode[{7}] aircraftTypeCode[{8}] flightNumber[{9}] terminalNumberFrom[{10}] terminalNumberTo[{11}] ). ",
                                    index.ToString().PadRight(3),
                                    InternationalModeType.Air,
                                    departureStopNaptan.PadRight(8),
                                    arrivalStopNaptan.PadRight(8),
                                    departureTimespan.ToString(),
                                    arrivalTimespan.ToString(),
                                    daysOfOperation.PadRight(7),
                                    carrierCode.PadRight(4),
                                    aircraftTypeCode.PadRight(4),
                                    flightNumber.PadRight(4),
                                    terminalNumberFrom.PadRight(3),
                                    terminalNumberTo.PadRight(3)));
                            Logger.Write(oe);
                        }
                        #endregion

                    }
                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalJourneyAir, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve International Air schedule data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInternationalJourneyAir);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve International Air schedule data from the database[{0}], Exception Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingInternationalJourneyAir);
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #endregion

                #region Update Interchange (check in/out times)

                tmpIntlJourneys = UpdateJourneyInterchangeTime(tmpIntlJourneys, logResults);

                #endregion

                #region Update City Transfer

                tmpIntlJourneys = UpdateJourneyCityTransfer(tmpIntlJourneys, originCityID, destinationCityID, logResults);

                #endregion

                #region Update International Stop information objects

                tmpIntlJourneys = UpdateJourneyInternationalStopInformation(tmpIntlJourneys, logResults);

                #endregion

                #region Update Distances

                tmpIntlJourneys = UpdateJourneyDistances(tmpIntlJourneys);

                #endregion

                #region Update Duration times

                tmpIntlJourneys = UpdateJourneyDurationTimes(tmpIntlJourneys);

                #endregion

                // Update data cache with the planned journeys
                intlData.AddInternationalJourneys(originCityID, destinationCityID, originStop.StopNaptan, destinationStop.StopNaptan, InternationalModeType.Air, tmpIntlJourneys.ToArray());

                #region Filter journeys

                // Filter journeys to keep only those valid for the requested date and day of the week
                intlJourneys = UpdateValidForDayJourney(tmpIntlJourneys, outwardDateTime.DayOfWeek).ToArray();
                intlJourneys = UpdateValidForDateJourney(new List<InternationalJourney>(intlJourneys), outwardDateTime).ToArray();

                #endregion
            }
            else
            {
                // Journeys were retrieved from the cache

                #region Filter journeys

                // Filter journeys to keep only those valid for the requested date and day of the week
                intlJourneys = UpdateValidForDayJourney(new List<InternationalJourney>(intlJourneys), outwardDateTime.DayOfWeek).ToArray();
                intlJourneys = UpdateValidForDateJourney(new List<InternationalJourney>(intlJourneys), outwardDateTime).ToArray();

                #endregion

                #region Update Dates for journeys

                // Because the journeys were added to the cache by a previous request, the journey dates may be wrong
                intlJourneys = UpdateJourneyDates(intlJourneys, outwardDateTime);

                #endregion
            }

            return intlJourneys;
        }

        /// <summary>
        /// Method which retrieves Coach schedule journeys from the database and creates 
        /// InternationalJourneys using the data
        /// </summary>
        /// <returns></returns>
        public InternationalJourney[] GetInternationalJourneyCoach(InternationalStop originStop, InternationalStop destinationStop, string originCityID, string destinationCityID, DateTime outwardDateTime, bool logResults)
        {

            Logger.Write(
                new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("International journey - Planning {0} Journey From[City:{1} Naptan:{2}]  To[City:{3} Naptan:{4}]",
                            InternationalModeType.Coach,
                            originCityID, originStop.StopNaptan, destinationCityID, destinationStop.StopNaptan)));

            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Check data cache fist
            InternationalJourney[] intlJourneys = intlData.GetInternationalJourneys(originCityID, destinationCityID, originStop.StopNaptan, destinationStop.StopNaptan, InternationalModeType.Coach);

            if (intlJourneys == null)
            {
                // No journeys exist for this origin and destination city/stop combination in cache, call database

                #region Get journey data from database

                // Temp array to pass planned journeys to the data cache for future access
                List<InternationalJourney> tmpIntlJourneys = new List<InternationalJourney>();

                // Temp array used to collect all the different journeys and their calling points,
                // to allow correct allocation to before, during, and after service calling point arrays 
                // for the Journey detail
                Dictionary<int, InternationalJourney> tempJourneys = new Dictionary<int, InternationalJourney>();
                Dictionary<int, List<InternationalJourneyCallingPoint>> tempJourneyCallingPoints = new Dictionary<int, List<InternationalJourneyCallingPoint>>();
                
                SqlDataReader sqlReader = null;
                try
                {
                    // Build up the sql request
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@OriginStopCode", originStop.StopCode);
                    parameters.Add("@DestinationStopCode", destinationStop.StopCode);
                    parameters.Add("@OutwardDateTime", outwardDateTime);

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInternationalJourneyCoach));
                    Logger.Write(oe);

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInternationalJourneyCoach, parameters);

                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalId = sqlReader.GetOrdinal("Id");
                    int ordinalCoachNumber = sqlReader.GetOrdinal("CoachNumber");
                    int ordinalOperatorCode = sqlReader.GetOrdinal("OperatorCode");
                    int ordinalDaysOfOperation = sqlReader.GetOrdinal("DaysOfOperation");
                    int ordinalServiceFacilities = sqlReader.GetOrdinal("ServiceFacilities");
                    int ordinalLegNum = sqlReader.GetOrdinal("LegNum");
                    int ordinalDepartureStopNaptan = sqlReader.GetOrdinal("DepartureStopNaptan");
                    int ordinalArrivalStopNaptan = sqlReader.GetOrdinal("ArrivalStopNaptan");
                    int ordinalDepartureTime = sqlReader.GetOrdinal("DepartureTime");
                    int ordinalDepartureDay = sqlReader.GetOrdinal("DepartureDay");
                    int ordinalArrivalTime = sqlReader.GetOrdinal("ArrivalTime");
                    int ordinalArrivalDay = sqlReader.GetOrdinal("ArrivalDay");
                    int ordinalEffectiveFromDate = sqlReader.GetOrdinal("EffectiveFromDate");
                    int ordinalEffectiveToDate = sqlReader.GetOrdinal("EffectiveToDate");
                    #endregion

                    #region Variables to hold values
                    int currentJourneyId = 0;
                    string coachNumber = string.Empty;
                    string operatorCode = string.Empty;
                    string daysOfOperation = string.Empty;
                    string serviceFacilities = string.Empty;
                    int legNum = 0;
                    string departureStopNaptan = string.Empty;
                    string arrivalStopNaptan = string.Empty;
                    TimeSpan departureTimespan = new TimeSpan(0, 0, 0);
                    int departureDay = 0;
                    TimeSpan arrivalTimespan = new TimeSpan(0, 0, 0);
                    int arrivalDay = 0;
                    DateTime effectiveFromDate = new DateTime();
                    DateTime effectiveToDate = new DateTime();

                    // Temp values to build up the journey and details
                    InternationalJourney intlJourney = null;
                    InternationalJourneyDetail intlJourneyDetail = null;
                    InternationalJourneyCallingPoint destinationCallingPoint = null;
                    DateTime previousLegArrivalDateTime = DateTime.MinValue;
                    int previousJourneyId = -1;

                    // Used to add to the temp journey and calling point dictionaries
                    int index = 0;
                    List<InternationalJourneyCallingPoint> tempCallingPoints = new List<InternationalJourneyCallingPoint>();

                    #endregion

                    while (sqlReader.Read())
                    {
                        // The stored procedure returns all legs for a journey, with each being treated as a
                        // stand-alone journey leg departing and arriving at a stop. The journey id value groups legs 
                        // together, thus indicating they are part of the same journey.

                        // The following code identifies different journeys, builds a single journey detail leg,
                        // and converts all legs in to calling points.

                        // The calling points are then updated with correct type (e.g. Board, Alight...) once all 
                        // data rows have been processed

                        // Assumes journey ids are sorted
                        // Assumes legs are sorted

                        #region Read data
                        // Read the database values returned, check for nulls as all columns may not
                        // have a value
                        currentJourneyId = sqlReader.GetInt32(ordinalId);
                        coachNumber = sqlReader.GetString(ordinalCoachNumber);
                        legNum = sqlReader.GetInt32(ordinalLegNum);
                        departureStopNaptan = sqlReader.GetString(ordinalDepartureStopNaptan);
                        arrivalStopNaptan = sqlReader.GetString(ordinalArrivalStopNaptan);
                        departureTimespan = sqlReader.GetTimeSpan(ordinalDepartureTime);
                        departureDay = sqlReader.GetInt32(ordinalDepartureDay);
                        arrivalTimespan = sqlReader.GetTimeSpan(ordinalArrivalTime);
                        arrivalDay = sqlReader.GetInt32(ordinalArrivalDay);

                        if (!sqlReader.IsDBNull(ordinalDaysOfOperation))
                        {
                            daysOfOperation = sqlReader.GetString(ordinalDaysOfOperation);
                        }
                        else
                        {
                            daysOfOperation = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalServiceFacilities))
                        {
                            serviceFacilities = sqlReader.GetString(ordinalServiceFacilities);
                        }
                        else
                        {
                            serviceFacilities = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalOperatorCode))
                        {
                            operatorCode = sqlReader.GetString(ordinalOperatorCode);
                        }
                        else
                        {
                            operatorCode = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalEffectiveFromDate))
                        {
                            effectiveFromDate = sqlReader.GetDateTime(ordinalEffectiveFromDate);
                        }
                        else
                        {
                            effectiveFromDate = DateTime.MinValue;
                        }

                        if (!sqlReader.IsDBNull(ordinalEffectiveToDate))
                        {
                            effectiveToDate = sqlReader.GetDateTime(ordinalEffectiveToDate);
                        }
                        else
                        {
                            effectiveToDate = DateTime.MaxValue;
                        }


                        #endregion

                        // New journey detected
                        if (previousJourneyId != currentJourneyId)
                        {
                            #region Save previous journey to result array

                            // Save the previous journey to the journey results array
                            if (intlJourney != null)
                            {
                                // Add the last detected destination calling point
                                tempCallingPoints.Add(destinationCallingPoint);

                                // Update the journey with the detail leg
                                intlJourney.JourneyDetails = new InternationalJourneyDetail[1] { intlJourneyDetail };

                                // Add to the temp arrays
                                tempJourneys.Add(index, intlJourney);
                                tempJourneyCallingPoints.Add(index, tempCallingPoints);

                                // Increment ready for next save
                                index++;
                            }

                            #endregion

                            // Reset values
                            tempCallingPoints = new List<InternationalJourneyCallingPoint>();

                            #region Create new journey

                            // Parse the Days of operation
                            DayOfWeek[] listDaysOfOperation = GetDaysOfOperation(daysOfOperation, departureStopNaptan, arrivalStopNaptan);

                            // Create new journey (this is updated later)
                            intlJourney = new InternationalJourney();
                            intlJourney.DepartureCityID = originCityID;
                            intlJourney.ArrivalCityID = destinationCityID;
                            intlJourney.DepartureStopNaptan = originStop.StopNaptan;
                            intlJourney.ArrivalStopNaptan = destinationStop.StopNaptan;
                            intlJourney.DaysOfOperation = listDaysOfOperation;
                            intlJourney.ModeType = InternationalModeType.Coach;

                            //Add effective date and time
                            intlJourney.EffectiveFromDate = effectiveFromDate;
                            intlJourney.EffectiveToDate = effectiveToDate;

                            // Create a temp detail leg (this is updated later)
                            intlJourneyDetail = new InternationalJourneyDetail();
                            intlJourneyDetail.DetailType = InternationalJourneyDetailType.TimedCoach;
                            intlJourneyDetail.OperatorCode = operatorCode;
                            intlJourneyDetail.ServiceNumber = coachNumber;
                            intlJourneyDetail.ServiceFacilities = GetServiceFacilities(serviceFacilities);

                            #endregion

                            previousJourneyId = currentJourneyId;
                        }

                        #region Create calling point

                        // Get the dates and times
                        DateTime legDepartureDateTime;
                        DateTime legArrivalDateTime;
                        GetJourneyDateTime(outwardDateTime, departureTimespan, departureDay, arrivalTimespan, arrivalDay, out legDepartureDateTime, out legArrivalDateTime);

                        // Create a new calling point for the departure, all are given a type of CallingPoint, which
                        // is updated later
                        InternationalJourneyCallingPoint callingPoint = new InternationalJourneyCallingPoint(
                                departureStopNaptan, previousLegArrivalDateTime, legDepartureDateTime, CallingPointType.CallingPoint);

                        // Store it
                        tempCallingPoints.Add(callingPoint);

                        // Create the final calling point (added when a new journey is detected)
                        destinationCallingPoint = new InternationalJourneyCallingPoint(
                                arrivalStopNaptan, legArrivalDateTime, DateTime.MinValue, CallingPointType.CallingPoint);

                        // Track arrival time for next calling point
                        previousLegArrivalDateTime = legArrivalDateTime;

                        #endregion

                        #region Log data result
                        if (logResults)
                        {
                            oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format(
                                    "International journey - scheduleId[{0}] ( mode[{1}] departureStopNaptan[{2}] arrivalStopNaptan[{3}] departTime[{4}] " +
                                    "arrivalTime[{5}] daysOfOperation[{6}] operatorCode[{7}] coachNumber[{8}] ). ",
                                    currentJourneyId.ToString().PadRight(3),
                                    InternationalModeType.Coach,
                                    departureStopNaptan.PadRight(8),
                                    arrivalStopNaptan.PadRight(8),
                                    departureTimespan.ToString(),
                                    arrivalTimespan.ToString(),
                                    daysOfOperation.PadRight(7),
                                    operatorCode.PadRight(4),
                                    coachNumber.PadRight(6)
                                    ));
                            Logger.Write(oe);
                        }
                        #endregion

                    }

                    // Wrap up. Add the last journey being worked with to the results array, because loop will have 
                    // exited before it was done
                    if (intlJourney != null)
                    {
                        #region Save journey to result array

                        // Add the last detected destination calling point
                        tempCallingPoints.Add(destinationCallingPoint);

                        // Update the journey with the detail leg
                        intlJourney.JourneyDetails = new InternationalJourneyDetail[1] { intlJourneyDetail };

                        // Add to the temp arrays
                        tempJourneys.Add(index, intlJourney);
                        tempJourneyCallingPoints.Add(index, tempCallingPoints);

                        #endregion
                    }
                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalJourneyCoach, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve International Coach schedule data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInternationalJourneyCoach);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve International Coach schedule data from the database[{0}], Exception Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingInternationalJourneyCoach);
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #region Update Intermediate calling points

                // All journeys from database processed. Now update the journey with all the calling points found for its service
                tmpIntlJourneys = UpdateJourneyCallingPoints(tempJourneys, tempJourneyCallingPoints);

                #endregion

                #endregion

                #region Update Interchange (check in/out times)

                tmpIntlJourneys = UpdateJourneyInterchangeTime(tmpIntlJourneys, logResults);

                #endregion

                #region Update City Transfer

                tmpIntlJourneys = UpdateJourneyCityTransfer(tmpIntlJourneys, originCityID, destinationCityID, logResults);

                #endregion

                #region Update International Stop information objects

                tmpIntlJourneys = UpdateJourneyInternationalStopInformation(tmpIntlJourneys, logResults);

                #endregion

                #region Update Distances

                tmpIntlJourneys = UpdateJourneyDistances(tmpIntlJourneys);

                #endregion

                #region Update Duration times

                tmpIntlJourneys = UpdateJourneyDurationTimes(tmpIntlJourneys);

                #endregion

                // Update data cache with the planned journeys
                intlData.AddInternationalJourneys(originCityID, destinationCityID, originStop.StopNaptan, destinationStop.StopNaptan, InternationalModeType.Coach, tmpIntlJourneys.ToArray());

                #region Filter journeys

                // Filter journeys to keep only those valid for the requested date and day of the week
                intlJourneys = UpdateValidForDayJourney(tmpIntlJourneys, outwardDateTime.DayOfWeek).ToArray();
                intlJourneys = UpdateValidForDateJourney(new List<InternationalJourney>(intlJourneys), outwardDateTime).ToArray();

                #endregion
            }
            else
            {
                // Journeys were retrieved from the cache

                #region Filter journeys

                // Filter journeys to keep only those valid for the requested date and day of the week
                intlJourneys = UpdateValidForDayJourney(new List<InternationalJourney>(intlJourneys), outwardDateTime.DayOfWeek).ToArray();
                intlJourneys = UpdateValidForDateJourney(new List<InternationalJourney>(intlJourneys), outwardDateTime).ToArray();

                #endregion

                #region Update Dates for journeys

                // Because the journeys were added to the cache by a previous request, the journey dates may be wrong
                intlJourneys = UpdateJourneyDates(intlJourneys, outwardDateTime);

                #endregion
            }

            return intlJourneys;
        }

        /// <summary>
        /// Method which retrieves Rail schedule journeys from the database and creates 
        /// InternationalJourneys using the data
        /// </summary>
        /// <returns></returns>
        public InternationalJourney[] GetInternationalJourneyRail(InternationalStop originStop, InternationalStop destinationStop, string originCityID, string destinationCityID, DateTime outwardDateTime, bool logResults)
        {

            Logger.Write(
                new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("International journey - Planning {0} Journey From[City:{1} Naptan:{2}]  To[City:{3} Naptan:{4}]",
                            InternationalModeType.Rail,
                            originCityID, originStop.StopNaptan, destinationCityID, destinationStop.StopNaptan)));

            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Check data cache fist
            InternationalJourney[] intlJourneys = intlData.GetInternationalJourneys(originCityID, destinationCityID, originStop.StopNaptan, destinationStop.StopNaptan, InternationalModeType.Rail);

            if (intlJourneys == null)
            {
                // No journeys exist for this origin and destination city/stop combination in cache, call database

                #region Get journey data from database

                // Temp array to pass planned journeys to the data cache for future access
                List<InternationalJourney> tmpIntlJourneys = new List<InternationalJourney>();
                
                // Temp array used to collect all the different journeys and their calling points,
                // to allow correct allocation to before, during, and after service calling point arrays 
                // for the Journey detail
                Dictionary<int, InternationalJourney> tempJourneys = new Dictionary<int, InternationalJourney>();
                Dictionary<int, List<InternationalJourneyCallingPoint>> tempJourneyCallingPoints = new Dictionary<int, List<InternationalJourneyCallingPoint>>();
                    

                SqlDataReader sqlReader = null;
                try
                {
                    // Build up the sql request
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@OriginStopCode", originStop.StopCode);
                    parameters.Add("@DestinationStopCode", destinationStop.StopCode);
                    parameters.Add("@OutwardDateTime", outwardDateTime);

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInternationalJourneyRail));
                    Logger.Write(oe);

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInternationalJourneyRail, parameters);

                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalId = sqlReader.GetOrdinal("Id");
                    int ordinalTrainNumber = sqlReader.GetOrdinal("TrainNumber");
                    int ordinalOperatorCode = sqlReader.GetOrdinal("OperatorCode");
                    int ordinalDaysOfOperation = sqlReader.GetOrdinal("DaysOfOperation");
                    int ordinalServiceFacilities = sqlReader.GetOrdinal("ServiceFacilities");
                    int ordinalLegNum = sqlReader.GetOrdinal("LegNum");
                    int ordinalDepartureStopNaptan = sqlReader.GetOrdinal("DepartureStopNaptan");
                    int ordinalArrivalStopNaptan = sqlReader.GetOrdinal("ArrivalStopNaptan");
                    int ordinalDepartureTime = sqlReader.GetOrdinal("DepartureTime");
                    int ordinalDepartureDay = sqlReader.GetOrdinal("DepartureDay");
                    int ordinalArrivalTime = sqlReader.GetOrdinal("ArrivalTime");
                    int ordinalArrivalDay = sqlReader.GetOrdinal("ArrivalDay");
                    int ordinalEffectiveFromDate = sqlReader.GetOrdinal("EffectiveFromDate");
                    int ordinalEffectiveToDate = sqlReader.GetOrdinal("EffectiveToDate");
                    #endregion

                    #region Variables to hold values
                    int currentJourneyId = 0;
                    string trainNumber = string.Empty;
                    string operatorCode = string.Empty;
                    string daysOfOperation = string.Empty;
                    string serviceFacilities = string.Empty;
                    int legNum = 0;
                    string departureStopNaptan = string.Empty;
                    string arrivalStopNaptan = string.Empty;
                    TimeSpan departureTimespan = new TimeSpan(0, 0, 0);
                    int departureDay = 0;
                    TimeSpan arrivalTimespan = new TimeSpan(0, 0, 0);
                    int arrivalDay = 0;
                    DateTime effectiveFromDate = new DateTime();
                    DateTime effectiveToDate = new DateTime();

                    // Temp values to build up the journey and details
                    InternationalJourney intlJourney = null;
                    InternationalJourneyDetail intlJourneyDetail = null;
                    InternationalJourneyCallingPoint destinationCallingPoint = null;
                    DateTime previousLegArrivalDateTime = DateTime.MinValue;
                    int previousJourneyId = -1;
                    
                    // Used to add to the temp journey and calling point dictionaries
                    int index = 0;
                    List<InternationalJourneyCallingPoint> tempCallingPoints = new List<InternationalJourneyCallingPoint>();

                    #endregion

                    while (sqlReader.Read())
                    {
                        // The stored procedure returns all legs for a journey, with each being treated as a
                        // stand-alone journey leg departing and arriving at a stop. The journey id value groups legs 
                        // together, thus indicating they are part of the same journey.

                        // The following code identifies different journeys, builds a single journey detail leg,
                        // and converts all legs in to calling points.

                        // The calling points are then updated with correct type (e.g. Board, Alight...) once all 
                        // data rows have been processed

                        // Assumes journey ids are sorted
                        // Assumes legs are sorted

                        #region Read data
                        // Read the database values returned, check for nulls as all columns may not
                        // have a value
                        currentJourneyId = sqlReader.GetInt32(ordinalId);
                        trainNumber = sqlReader.GetString(ordinalTrainNumber);
                        legNum = sqlReader.GetInt32(ordinalLegNum);
                        departureStopNaptan = sqlReader.GetString(ordinalDepartureStopNaptan);
                        arrivalStopNaptan = sqlReader.GetString(ordinalArrivalStopNaptan);
                        departureTimespan = sqlReader.GetTimeSpan(ordinalDepartureTime);
                        departureDay = sqlReader.GetInt32(ordinalDepartureDay);
                        arrivalTimespan = sqlReader.GetTimeSpan(ordinalArrivalTime);
                        arrivalDay = sqlReader.GetInt32(ordinalArrivalDay);

                        if (!sqlReader.IsDBNull(ordinalDaysOfOperation))
                        {
                            daysOfOperation = sqlReader.GetString(ordinalDaysOfOperation);
                        }
                        else
                        {
                            daysOfOperation = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalServiceFacilities))
                        {
                            serviceFacilities = sqlReader.GetString(ordinalServiceFacilities);
                        }
                        else
                        {
                            serviceFacilities = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalOperatorCode))
                        {
                            operatorCode = sqlReader.GetString(ordinalOperatorCode);
                        }
                        else
                        {
                            operatorCode = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalEffectiveFromDate))
                        {
                            effectiveFromDate = sqlReader.GetDateTime(ordinalEffectiveFromDate);
                        }
                        else
                        {
                            effectiveFromDate = DateTime.MinValue;
                        }

                        if (!sqlReader.IsDBNull(ordinalEffectiveToDate))
                        {
                            effectiveToDate = sqlReader.GetDateTime(ordinalEffectiveToDate);
                        }
                        else
                        {
                            effectiveToDate = DateTime.MaxValue;
                        }


                        #endregion

                        // New journey detected
                        if (previousJourneyId != currentJourneyId)
                        {
                            #region Save previous journey to result array

                            // Save the previous journey to the journey results array
                            if (intlJourney != null)
                            {
                                // Add the last detected destination calling point
                                tempCallingPoints.Add(destinationCallingPoint);

                                // Update the journey with the detail leg
                                intlJourney.JourneyDetails = new InternationalJourneyDetail[1] { intlJourneyDetail };

                                // Add to the temp arrays
                                tempJourneys.Add(index, intlJourney);
                                tempJourneyCallingPoints.Add(index, tempCallingPoints);

                                // Increment ready for next save
                                index++;
                            }

                            #endregion

                            // Reset values
                            tempCallingPoints = new List<InternationalJourneyCallingPoint>();
                            
                            #region Create new journey

                            // Parse the Days of operation
                            DayOfWeek[] listDaysOfOperation = GetDaysOfOperation(daysOfOperation, departureStopNaptan, arrivalStopNaptan);

                            // Create new journey (this is updated later)
                            intlJourney = new InternationalJourney();
                            intlJourney.DepartureCityID = originCityID;
                            intlJourney.ArrivalCityID = destinationCityID;
                            intlJourney.DepartureStopNaptan = originStop.StopNaptan;
                            intlJourney.ArrivalStopNaptan = destinationStop.StopNaptan;
                            intlJourney.DaysOfOperation = listDaysOfOperation;
                            intlJourney.ModeType = InternationalModeType.Rail;

                            //Add effective date and time
                            intlJourney.EffectiveFromDate = effectiveFromDate;
                            intlJourney.EffectiveToDate = effectiveToDate;

                            // Create a temp detail leg (this is updated later)
                            intlJourneyDetail = new InternationalJourneyDetail();
                            intlJourneyDetail.DetailType = InternationalJourneyDetailType.TimedRail;
                            intlJourneyDetail.OperatorCode = operatorCode;
                            intlJourneyDetail.ServiceNumber = trainNumber;
                            intlJourneyDetail.ServiceFacilities = GetServiceFacilities(serviceFacilities);
                            #endregion

                            previousJourneyId = currentJourneyId;
                        }

                        #region Create calling point

                        // Get the dates and times
                        DateTime legDepartureDateTime;
                        DateTime legArrivalDateTime;
                        GetJourneyDateTime(outwardDateTime, departureTimespan, departureDay, arrivalTimespan, arrivalDay, out legDepartureDateTime, out legArrivalDateTime);

                        // Create a new calling point for the departure, all are given a type of CallingPoint, which
                        // is updated later
                        InternationalJourneyCallingPoint callingPoint = new InternationalJourneyCallingPoint(
                                departureStopNaptan, previousLegArrivalDateTime, legDepartureDateTime, CallingPointType.CallingPoint);

                        // Store it
                        tempCallingPoints.Add(callingPoint);

                        // Create the final calling point (added when a new journey is detected)
                        destinationCallingPoint = new InternationalJourneyCallingPoint(
                                arrivalStopNaptan, legArrivalDateTime, DateTime.MinValue, CallingPointType.CallingPoint);

                        // Track arrival time for next calling point
                        previousLegArrivalDateTime = legArrivalDateTime;

                        #endregion
                        
                        #region Log data result
                        if (logResults)
                        {
                            oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format(
                                    "International journey - scheduleId[{0}] ( mode[{1}] departureStopNaptan[{2}] arrivalStopNaptan[{3}] departTime[{4}] " +
                                    "arrivalTime[{5}] daysOfOperation[{6}] operatorCode[{7}] trainNumber[{8}] ). ",
                                    currentJourneyId.ToString().PadRight(3),
                                    InternationalModeType.Rail,
                                    departureStopNaptan.PadRight(8),
                                    arrivalStopNaptan.PadRight(8),
                                    departureTimespan.ToString(),
                                    arrivalTimespan.ToString(),
                                    daysOfOperation.PadRight(7),
                                    operatorCode.PadRight(4),
                                    trainNumber.PadRight(6)
                                    ));
                            Logger.Write(oe);
                        }
                        #endregion

                    }

                    // Wrap up. Add the last journey being worked with to the results array, because loop will have 
                    // exited before it was done
                    if (intlJourney != null)
                    {
                        #region Save journey to result array

                        // Add the last detected destination calling point
                        tempCallingPoints.Add(destinationCallingPoint);

                        // Update the journey with the detail leg
                        intlJourney.JourneyDetails = new InternationalJourneyDetail[1] { intlJourneyDetail };

                        // Add to the temp arrays
                        tempJourneys.Add(index, intlJourney);
                        tempJourneyCallingPoints.Add(index, tempCallingPoints);

                        #endregion
                    }
                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalJourneyRail, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve International Rail schedule data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInternationalJourneyRail);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve International Rail schedule data from the database[{0}], Exception Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingInternationalJourneyRail);
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #region Update Intermediate calling points

                // All journeys from database processed. Now update the journey with all the calling points found for its service
                tmpIntlJourneys = UpdateJourneyCallingPoints(tempJourneys, tempJourneyCallingPoints);

                #endregion

                #endregion

                #region Update Interchange (check in/out times)

                tmpIntlJourneys = UpdateJourneyInterchangeTime(tmpIntlJourneys, logResults);

                #endregion

                #region Update City Transfer

                tmpIntlJourneys = UpdateJourneyCityTransfer(tmpIntlJourneys, originCityID, destinationCityID, logResults);

                #endregion

                #region Update International Stop information objects

                tmpIntlJourneys = UpdateJourneyInternationalStopInformation(tmpIntlJourneys, logResults);

                #endregion

                #region Update Distances

                tmpIntlJourneys = UpdateJourneyDistances(tmpIntlJourneys);

                #endregion

                #region Update Duration times

                tmpIntlJourneys = UpdateJourneyDurationTimes(tmpIntlJourneys);

                #endregion

                // Update data cache with the planned journeys
                intlData.AddInternationalJourneys(originCityID, destinationCityID, originStop.StopNaptan, destinationStop.StopNaptan, InternationalModeType.Rail, tmpIntlJourneys.ToArray());

                #region Filter journeys

                // Filter journeys to keep only those valid for the requested date and day of the week
                intlJourneys = UpdateValidForDayJourney(tmpIntlJourneys, outwardDateTime.DayOfWeek).ToArray();
                intlJourneys = UpdateValidForDateJourney(new List<InternationalJourney>(intlJourneys), outwardDateTime).ToArray();

                #endregion
            }
            else
            {
                // Journeys were retrieved from the cache

                #region Filter journeys

                // Filter journeys to keep only those valid for the requested date and day of the week
                intlJourneys = UpdateValidForDayJourney(new List<InternationalJourney>(intlJourneys), outwardDateTime.DayOfWeek).ToArray();
                intlJourneys = UpdateValidForDateJourney(new List<InternationalJourney>(intlJourneys), outwardDateTime).ToArray();

                #endregion

                #region Update Dates for journeys

                // Because the journeys were added to the cache by a previous request, the journey dates may be wrong
                intlJourneys = UpdateJourneyDates(intlJourneys, outwardDateTime);

                #endregion
            }

            return intlJourneys;
        }

        /// <summary>
        /// Method which retrieves Car journeys from the database and creates 
        /// InternationalJourneys using the data
        /// </summary>
        /// <returns></returns>
        public InternationalJourney[] GetInternationalJourneyCar( string originCityID, string destinationCityID, DateTime outwardDateTime, bool logResults)
        {
            Logger.Write(
                new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("International journey - Planning {0} Journey From[City:{1}]  To[City:{2}]",
                            InternationalModeType.Car,
                            originCityID, destinationCityID)));

            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Check data cache fist
            string carNaptan = "CAR"; // Dummy naptan for getting/adding car journey to cache
            InternationalJourney[] intlJourneys = intlData.GetInternationalJourneys(originCityID, destinationCityID, carNaptan, carNaptan, InternationalModeType.Car);

            if (intlJourneys == null)
            {
                // No journeys exist for this origin and destination city combination in cache, call database

                #region Get journey data from database

                // Temp array to pass planned journeys to the data cache for future access
                List<InternationalJourney> tmpIntlJourneys = new List<InternationalJourney>();

                SqlDataReader sqlReader = null;
                try
                {
                    // Build up the sql request
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@DepartureCityId", originCityID);
                    parameters.Add("@ArrivalCityId", destinationCityID);

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInternationalJourneyCar));
                    Logger.Write(oe);

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInternationalJourneyCar, parameters);

                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalDepartureCityId = sqlReader.GetOrdinal("DepartureCityId");
                    int ordinalArrivalCityId = sqlReader.GetOrdinal("ArrivalCityId");
                    int ordinalDepartureTime = sqlReader.GetOrdinal("DepartureTime");
                    int ordinalArrivalTime = sqlReader.GetOrdinal("ArrivalTime");
                    int ordinalArrivalDay = sqlReader.GetOrdinal("ArrivalDay");
                    int ordinalEmissionsGrammes = sqlReader.GetOrdinal("EmissionsGrammes");
                    int ordinalJourneyURL = sqlReader.GetOrdinal("JourneyURL");
                    int ordinalJourneyInformation = sqlReader.GetOrdinal("JourneyInformation");
                    #endregion

                    #region Variable to hold values
                    string departureCityId = string.Empty;
                    string arrivalCityId = string.Empty;
                    TimeSpan departureTimespan = new TimeSpan();
                    TimeSpan arrivalTimespan = new TimeSpan();
                    int arrivalDay = 0;
                    double emissionsGrammes = 0;
                    string journeyURL = string.Empty;
                    string journeyInformation = string.Empty;

                    // Used for logging number of journey
                    int index = 0;
                    #endregion

                    while (sqlReader.Read())
                    {
                        #region Read data
                        // Read the database values returned, check for nulls as all columns may not
                        // have a value
                        departureCityId = Convert.ToString(sqlReader.GetInt32(ordinalDepartureCityId));
                        arrivalCityId = Convert.ToString(sqlReader.GetInt32(ordinalArrivalCityId));
                        departureTimespan = sqlReader.GetTimeSpan(ordinalDepartureTime);
                        arrivalTimespan = sqlReader.GetTimeSpan(ordinalArrivalTime);
                        arrivalDay = sqlReader.GetInt32(ordinalArrivalDay);

                        if (!sqlReader.IsDBNull(ordinalEmissionsGrammes))
                        {
                            emissionsGrammes = Convert.ToDouble(sqlReader.GetDecimal(ordinalEmissionsGrammes));
                        }
                        else
                        {
                            emissionsGrammes = 0;
                        }

                        if (!sqlReader.IsDBNull(ordinalJourneyURL))
                        {
                            journeyURL = sqlReader.GetString(ordinalJourneyURL);
                        }
                        else
                        {
                            journeyURL = string.Empty;
                        }

                        if (!sqlReader.IsDBNull(ordinalJourneyInformation))
                        {
                            journeyInformation = sqlReader.GetString(ordinalJourneyInformation);
                        }
                        else
                        {
                            journeyInformation = string.Empty;
                        }
                        #endregion


                        // Update the journey dates and times
                        DateTime departureDateTime;
                        DateTime arrivalDateTime;
                        GetJourneyDateTime(outwardDateTime, departureTimespan, 0, arrivalTimespan, arrivalDay,
                            out departureDateTime, out arrivalDateTime);

                        // Car journey has no intermediate stops so there is just one journey detail leg
                        InternationalJourneyDetail[] journeyDetails = new InternationalJourneyDetail[1];
                        journeyDetails[0] = CreateInternationalJourneyDetail(
                            InternationalJourneyDetailType.TimedCar,
                            string.Empty, string.Empty, departureDateTime, arrivalDateTime,
                            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                        // Build the InternationalJourney
                        InternationalJourney intlJourney = CreateInternationalJourney(
                            originCityID, destinationCityID,
                            string.Empty, string.Empty,
                            departureDateTime, arrivalDateTime,
                            new DayOfWeek[7] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                            InternationalModeType.Car, journeyDetails);

                        // Update the emissions
                        intlJourney.Emissions = emissionsGrammes;

                        // Add to the temp array to be added to the data cache
                        tmpIntlJourneys.Add(intlJourney);

                        #region Log data result
                        if (logResults)
                        {
                            index++;

                            oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format(
                                    "International journey - {0}( mode[{1}] originCityID[{2}] destinationCityID[{3}] departTime[{4}] " +
                                    "arrivalTime[{5}] ). ",
                                    index.ToString().PadRight(3),
                                    InternationalModeType.Car,
                                    originCityID.PadRight(4),
                                    destinationCityID.PadRight(4),
                                    departureTimespan.ToString(),
                                    arrivalTimespan.ToString()));
                            Logger.Write(oe);
                        }
                        #endregion
                    }
                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalJourneyCar, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve International Car journey data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInternationalJourneyCar);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve International Car journey data from the database[{0}], Exception Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingInternationalJourneyCar);
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #endregion

                #region Update Duration times

                tmpIntlJourneys = UpdateJourneyDurationTimes(tmpIntlJourneys);

                #endregion

                #region Add City objects to detail

                // Car journey will only have one Detail object, and no Transfer detail objects added
                foreach (InternationalJourney ij in tmpIntlJourneys)
                {
                    if ((ij.JourneyDetails != null) && (ij.JourneyDetails.Length > 0))
                    {
                        // Assign the City object to the detail if it exists
                        ij.JourneyDetails[0].DepartureCity = GetInternationalCity(originCityID, logResults);
                        ij.JourneyDetails[0].ArrivalCity = GetInternationalCity(destinationCityID, logResults);
                    }
                }

                #endregion

                // Update data cache with the planned journeys
                intlData.AddInternationalJourneys(originCityID, destinationCityID, carNaptan, carNaptan, InternationalModeType.Car, tmpIntlJourneys.ToArray());

                // Add to the result array
                intlJourneys = tmpIntlJourneys.ToArray();
            }
            else
            {
                // Journeys were retrieved from the cache

                #region Update Dates for journeys

                // Because the journeys were added to the cache by a previous request, the journey dates may be wrong
                intlJourneys = UpdateJourneyDates(intlJourneys, outwardDateTime);

                #endregion
            }

            return intlJourneys;
        }

        #endregion

        #region Private methods

        #region Update
        
        /// <summary>
        /// Method which takes journeys and the calling points, and sorts them in points called at 
        /// before, during, and after the depart and arrive at stop naptan spcified in the journey.
        /// Assumes each journey has only 1 journey detail leg
        /// </summary>
        private List<InternationalJourney> UpdateJourneyCallingPoints(Dictionary<int, InternationalJourney> tempJourneys, Dictionary<int, List<InternationalJourneyCallingPoint>> tempJourneyCallingPoints)
        {
            // Return object
            List<InternationalJourney> intlJourneys = new List<InternationalJourney>();

            // Temp arrays
            List<InternationalJourneyCallingPoint> intermediatesBefore = null;
            List<InternationalJourneyCallingPoint> intermediatesLeg = null;
            List<InternationalJourneyCallingPoint> intermediatesAfter = null;

            // Used to track which array to add the calling point
            bool boardAdded = false;
            bool alightAdded = false;

            #region Update journey calling points

            // Work with each journey and calling points in turn
            foreach (KeyValuePair<int, InternationalJourney> kvp in tempJourneys)
            {
                InternationalJourney intlJourney = tempJourneys[kvp.Key];

                List<InternationalJourneyCallingPoint> callingPoints = tempJourneyCallingPoints[kvp.Key];

                string departureNaptan = intlJourney.DepartureStopNaptan;
                string arrivalNaptan = intlJourney.ArrivalStopNaptan;

                // Reset temp values
                intermediatesBefore = new List<InternationalJourneyCallingPoint>();
                intermediatesLeg = new List<InternationalJourneyCallingPoint>();
                intermediatesAfter = new List<InternationalJourneyCallingPoint>();
                boardAdded = false;
                alightAdded = false;

                // Update each calling point and assign to the correct array
                for (int i = 0; i < callingPoints.Count; i++)
                {
                    InternationalJourneyCallingPoint callingPoint = callingPoints[i];

                    // Board and Alight calling points are added to the IntermediateLegs array

                    if (callingPoint.StopNaptan == departureNaptan)
                    {
                        #region Board (and Origin)

                        if (i == 0)
                        {
                            // First calling point is the origin and board
                            callingPoint.Type = CallingPointType.OriginAndBoard;
                            intermediatesLeg.Add(callingPoint);
                        }
                        else
                        {
                            // Its not first point in the service, so boarding
                            callingPoint.Type = CallingPointType.Board;
                            intermediatesLeg.Add(callingPoint);
                        }

                        // Update the journey detail values
                        intlJourney.JourneyDetails[0].DepartureStopNaptan = callingPoint.StopNaptan;
                        intlJourney.JourneyDetails[0].DepartureDateTime = callingPoint.DepartureDateTime;
                        intlJourney.DepartureDateTime = callingPoint.DepartureDateTime;

                        boardAdded = true;

                        #endregion
                    }
                    else if (callingPoint.StopNaptan == arrivalNaptan)
                    {
                        #region Alight (and Destination)

                        if (i == callingPoints.Count - 1)
                        {
                            // Last calling point is the destination and alight
                            callingPoint.Type = CallingPointType.DestinationAndAlight;
                            intermediatesLeg.Add(callingPoint);
                        }
                        else
                        {
                            // Its not the last point in the service, so alight
                            callingPoint.Type = CallingPointType.Alight;
                            intermediatesLeg.Add(callingPoint);
                        }

                        // Update the journey detail values
                        intlJourney.JourneyDetails[0].ArrivalStopNaptan = callingPoint.StopNaptan;
                        intlJourney.JourneyDetails[0].ArrivalDateTime = callingPoint.ArrivalDateTime;
                        intlJourney.ArrivalDateTime = callingPoint.ArrivalDateTime;

                        alightAdded = true;

                        #endregion
                    }
                    else
                    {
                        #region Other Calling Points (incl Origin and Destination)

                        // Its for another stop and hence just an Origin, Destination, or Calling point 
                        if (i == 0)
                        {
                            callingPoint.Type = CallingPointType.Origin;
                            intermediatesBefore.Add(callingPoint);
                        }
                        else if (i == callingPoints.Count - 1)
                        {
                            callingPoint.Type = CallingPointType.Destination;
                            intermediatesAfter.Add(callingPoint);
                        }
                        else
                        {
                            callingPoint.Type = CallingPointType.CallingPoint;

                            if (alightAdded)
                            {
                                intermediatesAfter.Add(callingPoint);
                            }
                            else if (boardAdded)
                            {
                                intermediatesLeg.Add(callingPoint);
                            }
                            else
                            {
                                intermediatesBefore.Add(callingPoint);
                            }
                        }

                        #endregion
                    }
                } // Finished updating calling points

                // Assign arrays to the Journey Detail
                intlJourney.JourneyDetails[0].IntermediatesBefore = intermediatesBefore.ToArray();
                intlJourney.JourneyDetails[0].IntermediatesLeg = intermediatesLeg.ToArray();
                intlJourney.JourneyDetails[0].IntermediatesAfter = intermediatesAfter.ToArray();

                // Add to the result array
                intlJourneys.Add(intlJourney);
            }

            #endregion

            return intlJourneys;
        }


        /// <summary>
        /// Method which adds international stop information data to the international journey detail legs
        /// </summary>
        private List<InternationalJourney> UpdateJourneyInternationalStopInformation(List<InternationalJourney> internationalJourneys, bool logResults)
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Updating international stop information for journeys.");
            Logger.Write(oe);

            // Update stop information details for each journey
            foreach (InternationalJourney ij in internationalJourneys)
            {
                foreach (InternationalJourneyDetail ijd in ij.JourneyDetails)
                {
                    // Departure
                    InternationalStop[] intlStops = GetInternationalStop(new string[1] { ijd.DepartureStopNaptan }, logResults);

                    if (intlStops.Length > 0)
                    {
                        // Assign to journey detail
                        ijd.DepartureStop = intlStops[0];
                    }

                    
                    // Arrival
                    intlStops = GetInternationalStop(new string[1] { ijd.ArrivalStopNaptan }, logResults);

                    if (intlStops.Length > 0)
                    {
                        // Assign to journey detail
                        ijd.ArrivalStop = intlStops[0];
                    }
                    
                    
                    // Intermediate stops
                    foreach (InternationalJourneyCallingPoint ijcp in ijd.IntermediatesBefore)
                    {
                        intlStops = GetInternationalStop(new string[1] { ijcp.StopNaptan }, logResults);

                        if (intlStops.Length > 0)
                        {
                            // Assign to intermediate stop
                            ijcp.Stop = intlStops[0];
                        }
                    }
                    foreach (InternationalJourneyCallingPoint ijcp in ijd.IntermediatesLeg)
                    {
                        intlStops = GetInternationalStop(new string[1] { ijcp.StopNaptan }, logResults);

                        if (intlStops.Length > 0)
                        {
                            // Assign to intermediate stop
                            ijcp.Stop = intlStops[0];
                        }
                    }
                    foreach (InternationalJourneyCallingPoint ijcp in ijd.IntermediatesAfter)
                    {
                        intlStops = GetInternationalStop(new string[1] { ijcp.StopNaptan }, logResults);

                        if (intlStops.Length > 0)
                        {
                            // Assign to intermediate stop
                            ijcp.Stop = intlStops[0];
                        }
                    }

                }
            }

            return internationalJourneys;
        }

        /// <summary>
        /// Method which adds interchange (check in/out) times to the start and end of the 
        /// international journey detail leg
        /// </summary>
        private List<InternationalJourney> UpdateJourneyInterchangeTime(List<InternationalJourney> internationalJourneys, bool logResults)
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Updating interchange time information for journeys.");
            Logger.Write(oe);

            // Update interchange times for each journey
            foreach (InternationalJourney ij in internationalJourneys)
            {
                if (ij.JourneyDetails.Length > 0)
                {
                    // Get the first and last journey details (assume the journey only contains the actual
                    // travelling parts at this point, i.e. just the flight or the rail legs)
                    InternationalJourneyDetail departureDetail = ij.JourneyDetails[0];
                    InternationalJourneyDetail arriveDetail = ij.JourneyDetails[ij.JourneyDetails.Length - 1];

                    #region Get interchange times

                    // Call GetInterchangeTime method to get the time.
                    // The method will ensure the times are added to the cache,  it will only add to 
                    // cache if they're not already loaded.
                    // If no time exists for the specified Operator code, then a default time is returned
                    // for the Naptan
                    InterchangeTimeValue checkInValue = GetInterchangeTime(departureDetail.DepartureStopNaptan, departureDetail.OperatorCode, true, logResults);
                    InterchangeTimeValue checkOutValue = GetInterchangeTime(arriveDetail.ArrivalStopNaptan, arriveDetail.OperatorCode, false, logResults);

                    #endregion

                    #region Calculate interchange date times

                    DateTime checkInDateTime = departureDetail.DepartureDateTime.Subtract(
                        checkInValue.timeSpan);

                    DateTime checkOutDateTime = arriveDetail.ArrivalDateTime.Add(
                        checkOutValue.timeSpan);

                    #endregion

                    // Update the international journey detail
                    departureDetail.CheckInDateTime = checkInDateTime;
                    arriveDetail.CheckOutDateTime = checkOutDateTime;
                }
            }

            return internationalJourneys;
        }

        /// <summary>
        /// Method which adds the city transfer international journey detail leg to the start and end of the journey
        /// </summary>
        /// <param name="internationalJourneys"></param>
        /// <returns></returns>
        private List<InternationalJourney> UpdateJourneyCityTransfer(List<InternationalJourney> internationalJourneys,
            string originCityID, string destinationCityID, bool logResults)
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Adding city transfer time journey detail legs to journeys.");
            Logger.Write(oe);

            foreach (InternationalJourney ij in internationalJourneys)
            {
                if (ij.JourneyDetails.Length > 0)
                {
                    // Get the first and last journey details (assume the journey only contains the actual
                    // travelling parts at this point, i.e. just the flight or the rail legs)
                    InternationalJourneyDetail departureDetail = ij.JourneyDetails[0];
                    InternationalJourneyDetail arriveDetail = ij.JourneyDetails[ij.JourneyDetails.Length - 1];

                    #region Get city transfer times

                    // GetTransferTime updates the dictionary
                    TransferTimeKey transferFromCityKey = new TransferTimeKey(originCityID, departureDetail.DepartureStopNaptan);
                    TransferTimeValue transferFromCityValue = GetInternationalTransfer(transferFromCityKey, logResults);

                    
                    TransferTimeKey transferToCityKey = new TransferTimeKey(destinationCityID, arriveDetail.ArrivalStopNaptan);
                    TransferTimeValue transferToCityValue = GetInternationalTransfer(transferToCityKey, logResults);
                    
                    #endregion

                    // The new international journey details list
                    List<InternationalJourneyDetail> newJourneyDetails = new List<InternationalJourneyDetail>();

                    #region Transfer from city journey detail

                    // Check the transfer value
                    if (transferFromCityValue.transferTimeMinutes >= 0)
                    {
                        // Calculate the transfer date times
                        DateTime arrivalDateTime = departureDetail.DepartureDateTime;
                        if ((departureDetail.CheckInDateTime != DateTime.MinValue)
                            && (departureDetail.CheckInDateTime < departureDetail.DepartureDateTime))
                        {
                            // If a check in date time specified then use that
                            arrivalDateTime = departureDetail.CheckInDateTime;
                        }

                        DateTime departureDateTime = arrivalDateTime.Subtract(new TimeSpan(0, transferFromCityValue.transferTimeMinutes, 0));

                        // Build up the journey detail
                        InternationalJourneyDetail transferFromJourneyDetail = CreateInternationalJourneyDetail(
                            InternationalJourneyDetailType.Transfer,
                            string.Empty, transferFromCityKey.stopNaptan, departureDateTime, arrivalDateTime,
                            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                        // Assign the transfer information
                        transferFromJourneyDetail.TransferInfo = transferFromCityValue.transferText;


                        // Assign the City object to the transfer detail if it exists
                        transferFromJourneyDetail.DepartureCity = GetInternationalCity(originCityID, logResults);
                        
                        // Stop objects are assigned to the detail by the GetInternationalJourney... as a final update

                        // Add the transfer detail to the details array
                        newJourneyDetails.Add(transferFromJourneyDetail);

                        // Update the Journey Depart time to be the same as the transfer as this is now the first leg
                        ij.DepartureDateTime = transferFromJourneyDetail.DepartureDateTime;
                    }
                    
                    #endregion

                    // Copy all the existing journey details in to the new list
                    newJourneyDetails.AddRange(ij.JourneyDetails);

                    #region Transfer to city journey detail

                    // Check the transfer value
                    if (transferToCityValue.transferTimeMinutes >= 0)
                    {
                        // Calculate the transfer date times
                        DateTime departureDateTime = arriveDetail.ArrivalDateTime;
                        if ((arriveDetail.CheckOutDateTime != DateTime.MinValue)
                            && (arriveDetail.CheckOutDateTime > arriveDetail.ArrivalDateTime))
                        {
                            // If a check out date time specified then use that
                            departureDateTime = arriveDetail.CheckOutDateTime;
                        }

                        DateTime arrivalDateTime = departureDateTime.Add(new TimeSpan(0, transferToCityValue.transferTimeMinutes, 0));

                        // Build up the journey detail
                        InternationalJourneyDetail transferToJourneyDetail = CreateInternationalJourneyDetail(
                            InternationalJourneyDetailType.Transfer,
                            transferToCityKey.stopNaptan, string.Empty,
                            departureDateTime, arrivalDateTime,
                            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                        // Assign the transfer information
                        transferToJourneyDetail.TransferInfo = transferToCityValue.transferText;

                        
                        // Assign the City object to the transfer detail if it exists
                        transferToJourneyDetail.ArrivalCity = GetInternationalCity(destinationCityID, logResults);
                        
                        // Stop objects are assigned to the detail by the GetInternationalJourney... as a final update

                        // Add the transfer detail to the details array
                        newJourneyDetails.Add(transferToJourneyDetail);

                        // Update the Journey Arrive time to be the same as the transfer as this is now the last leg
                        ij.ArrivalDateTime = transferToJourneyDetail.ArrivalDateTime;
                    }

                    #endregion

                    // Finally update the journey with the new journey details
                    ij.JourneyDetails = newJourneyDetails.ToArray();
                }
            }

            return internationalJourneys;
        }

        /// <summary>
        /// Method which updates the distances between stops for each journey detail leg
        /// </summary>
        private List<InternationalJourney> UpdateJourneyDistances(List<InternationalJourney> internationalJourneys)
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Updating journey distance information for journeys.");
            Logger.Write(oe);

            // Distances are in metres
            foreach (InternationalJourney ij in internationalJourneys)
            {
                foreach (InternationalJourneyDetail ijd in ij.JourneyDetails)
                {
                    // Don't add a distance for a Transfer
                    if (ijd.DetailType != InternationalJourneyDetailType.Transfer)
                    {
                        if ((ijd.DepartureStop != null) && (ijd.ArrivalStop != null))
                        {
                            ijd.Distance = GetStopDistance(ijd.DepartureStop.StopCode, ijd.ArrivalStop.StopCode);
                        }
                        else
                        {
                            ijd.Distance = 0;
                        }
                    }
                    else
                    {
                        ijd.Distance = 0;
                    }

                }
            }

            return internationalJourneys;
        }

        /// <summary>
        /// Method which calculates the journey and journey detail leg duration times, taking in to consideration
        /// the timezone the detail depart/arrive stops sit in
        /// </summary>
        private List<InternationalJourney> UpdateJourneyDurationTimes(List<InternationalJourney> internationalJourneys)
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Updating journey duration time information for journeys.");
            Logger.Write(oe);

            // Durations are calculated in minutes
            foreach (InternationalJourney ij in internationalJourneys)
            {
                double journeyDuration = 0;           // Overall total duration for journey
                
                foreach (InternationalJourneyDetail ijd in ij.JourneyDetails)
                {
                    // Get the travel times (which will be in the Coutry local time),
                    // adjusting the time to be for the timezone it is in
                    DateTime departureDateTime = ijd.DepartureDateTime;
                    DateTime arrivalDateTime = ijd.ArrivalDateTime;

                    #region Update times with timezone
 
                    // Assume time zone value is in hours
                    double departureTimeZoneHours = 0;
                    double arrivalTimeZoneHours = 0;

                    // Get departure timezone for this detail
                    if ((ijd.DepartureCity != null) && (ijd.DepartureCity.CityCountry != null))
                    {
                        departureTimeZoneHours = ijd.DepartureCity.CityCountry.TimeZone;
                    }
                    else if ((ijd.DepartureStop != null) && (ijd.DepartureStop.StopCountry != null))
                    {
                        departureTimeZoneHours = ijd.DepartureStop.StopCountry.TimeZone;
                    }
                    
                    double departureTimeZoneMinutes = Math.Round(departureTimeZoneHours * 60, 0);
                    TimeSpan departureTimeZone = new TimeSpan(0, Convert.ToInt32(departureTimeZoneMinutes), 0);
                    
                    // Get arrival timezone for this detail
                    if ((ijd.ArrivalCity != null) && (ijd.ArrivalCity.CityCountry != null))
                    {
                        arrivalTimeZoneHours = ijd.ArrivalCity.CityCountry.TimeZone;
                    }
                    else if ((ijd.ArrivalStop != null) && (ijd.ArrivalStop.StopCountry != null))
                    {
                        arrivalTimeZoneHours = ijd.ArrivalStop.StopCountry.TimeZone;
                    }

                    double arrivalTimeZoneMinutes = Math.Round(arrivalTimeZoneHours * 60, 0);
                    TimeSpan arrivalTimeZone = new TimeSpan(0, Convert.ToInt32(arrivalTimeZoneMinutes), 0);

                    // Bring the depart and arrive times to UTC times based on the timezone
                    DateTimeOffset departureOffsetTime = new DateTimeOffset(departureDateTime, departureTimeZone);
                    DateTimeOffset arrivalOffsetTime = new DateTimeOffset(arrivalDateTime, arrivalTimeZone);
                    
                    #endregion

                    // Get duration using the Local UTC time
                    TimeSpan ts = new TimeSpan(0, 0, 0);

                    if (departureOffsetTime.LocalDateTime < arrivalOffsetTime.LocalDateTime)
                    {
                        ts = arrivalOffsetTime.Subtract(departureOffsetTime);
                    }
                    else
                    {
                        ts = departureOffsetTime.Subtract(arrivalOffsetTime);
                    }
                    

                    // detail duration is set to value not including check in/out times
                    ijd.DurationMinutes = ts.TotalMinutes;
                    journeyDuration += ts.TotalMinutes;

                    #region Add on check in/out times to the total journey duration
                    // For a timed detail leg, add on the check in/out time
                    if ((ijd.DetailType == InternationalJourneyDetailType.TimedAir)
                        ||(ijd.DetailType == InternationalJourneyDetailType.TimedCoach)
                        || (ijd.DetailType == InternationalJourneyDetailType.TimedRail))
                    {
                        if (ijd.CheckInDateTime != DateTime.MinValue)
                        {
                            // Get checkin time duration, and add to overall journey duration
                            ts = ijd.DepartureDateTime.Subtract(ijd.CheckInDateTime);

                            journeyDuration += ts.TotalMinutes;
                        }

                        if (ijd.CheckOutDateTime != DateTime.MinValue)
                        {
                            // Get checkout time duration, and add to overall journey duration
                            ts = ijd.CheckOutDateTime.Subtract(ijd.ArrivalDateTime);

                            journeyDuration += ts.TotalMinutes;
                        }
                    }
                    #endregion
                }

                // Assign the duration to the journey
                ij.DurationMinutes = journeyDuration;
            }

            return internationalJourneys;
        }

        /// <summary>
        /// Method which filters a list of international journeys and only keeps those valid for the requested day
        /// </summary>
        private List<InternationalJourney> UpdateValidForDayJourney(List<InternationalJourney> internationalJourneys, DayOfWeek requestedDay)
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Removing journeys not valid for requested day.");
            Logger.Write(oe);

            List<InternationalJourney> validJourneys = new List<InternationalJourney>();

            foreach (InternationalJourney ij in internationalJourneys)
            {
                DayOfWeek[] listDaysOfOperation = ij.DaysOfOperation;

                // Is journey valid on the specified date?, if so then add it to the valid array
                if (IsJourneyValidForDay(listDaysOfOperation, requestedDay))
                {
                    validJourneys.Add(ij);
                }   
                else
                {
                    string daysOfOp = string.Empty;
                    foreach (DayOfWeek dw in ij.DaysOfOperation) {daysOfOp += dw.ToString();}

                    OperationalEvent oeFiltered = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                    string.Format(
                    "International [{1}] journey - serviceNumber[{0}] (departureStopNaptan[{2}] arrivalStopNaptan[{3}] departTime[{4}] " +
                    "arrivalTime[{5}] daysOfOperation[{6}] effFrom[{7}] effTo[{8}] filtered out on effective days mismatch ). ",
                    ij.JourneyDetails[1].ServiceNumber.ToString().PadRight(3),
                    ij.ModeType.ToString(),
                    ij.DepartureStopNaptan.PadRight(8),
                    ij.ArrivalStopNaptan.PadRight(8),
                    ij.JourneyDetails[1].DepartureDateTime.ToString(),
                    ij.JourneyDetails[1].ArrivalDateTime.ToString(),
                    daysOfOp,
                    ij.EffectiveFromDate.ToString(),
                    ij.EffectiveToDate.ToString()
                    ));
                    Logger.Write(oeFiltered);
                }

            }

            oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        string.Format("International planner - [{0}] journeys were removed out of [{1}] journeys because they were not valid for the requested day."
                        , internationalJourneys.Count - validJourneys.Count
                        , internationalJourneys.Count));
            Logger.Write(oe);

            return validJourneys;
        }


        /// <summary>
        /// Method which filters a list of international journeys and only keeps those effective on the requested date
        /// </summary>
        private List<InternationalJourney> UpdateValidForDateJourney(List<InternationalJourney> internationalJourneys, DateTime requestedDate)
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Removing journeys not effective on requested date.");
            Logger.Write(oe);

            List<InternationalJourney> validJourneys = new List<InternationalJourney>();

            foreach (InternationalJourney ij in internationalJourneys)
            {

                // Is journey valid on the specified date?, if so then add it to the valid array
                if ((ij.EffectiveFromDate <= requestedDate) && (ij.EffectiveToDate >= requestedDate))
                {
                    validJourneys.Add(ij);
                }
                else
                {
                    string daysOfOp = string.Empty;
                    foreach (DayOfWeek dw in ij.DaysOfOperation) {daysOfOp += dw.ToString();}

                    OperationalEvent oeFiltered = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                    string.Format(
                    "International [{1}] journey - serviceNumber[{0}] (departureStopNaptan[{2}] arrivalStopNaptan[{3}] departTime[{4}] " +
                    "arrivalTime[{5}] daysOfOperation[{6}] effFrom[{7}] effTo[{8}] filtered out on effective dates mismatch ). ",
                    ij.JourneyDetails[1].ServiceNumber.ToString().PadRight(3),
                    ij.ModeType.ToString(),
                    ij.DepartureStopNaptan.PadRight(8),
                    ij.ArrivalStopNaptan.PadRight(8),
                    ij.JourneyDetails[1].DepartureDateTime.ToString(),
                    ij.JourneyDetails[1].ArrivalDateTime.ToString(),
                    daysOfOp,
                    ij.EffectiveFromDate.ToString(),
                    ij.EffectiveToDate.ToString()
                    ));
                    Logger.Write(oeFiltered);
                }

            }

            oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        string.Format("International planner - [{0}] journeys were removed out of [{1}] journeys because they were not effective on requested date."
                        , internationalJourneys.Count - validJourneys.Count
                        , internationalJourneys.Count));
            Logger.Write(oe);

            return validJourneys;
        }

        /// <summary>
        /// Method which updates the journey dates for an already planned international journey
        /// </summary>
        private InternationalJourney[] UpdateJourneyDates(InternationalJourney[] internationalJourneys, DateTime requestedDay)
        {
            // Log
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                        "International planner - Updating journey dates for journeys retrieved from the data cache.");
            Logger.Write(oe);

            // When updating the dates, need to take into account the journey may arrive 0 or more days after
            // the departure day. Therefore, use a timespan value to add the correct number of days to get 
            // to the requested date. Times will remain the same so only need to add on the days
            TimeSpan targetDayTimeSpan = new TimeSpan(0,0,0);
            DateTime targetDate = new DateTime(requestedDay.Year, requestedDay.Month, requestedDay.Day);

            foreach (InternationalJourney ij in internationalJourneys)
            {
                // Work out the new date as a timespan difference (ignoring the time). This should be 
                // the same for all journeys but not guranteed
                DateTime currentJourneyDate = new DateTime(ij.DepartureDateTime.Year, ij.DepartureDateTime.Month, ij.DepartureDateTime.Day);

                targetDayTimeSpan = targetDate.Subtract(currentJourneyDate);

                // Now update all dates in the journey with the new date
                ij.DepartureDateTime = ij.DepartureDateTime.Add(targetDayTimeSpan);
                ij.ArrivalDateTime = ij.ArrivalDateTime.Add(targetDayTimeSpan);
 
                foreach (InternationalJourneyDetail ijd in ij.JourneyDetails)
                {
                    // Journey details
                    ijd.DepartureDateTime = ijd.DepartureDateTime.Add(targetDayTimeSpan);
                    ijd.ArrivalDateTime = ijd.ArrivalDateTime.Add(targetDayTimeSpan);

                    if (ijd.CheckInDateTime != DateTime.MinValue)
                    {
                        ijd.CheckInDateTime = ijd.CheckInDateTime.Add(targetDayTimeSpan);
                    }

                    if (ijd.CheckOutDateTime != DateTime.MinValue)
                    {
                        ijd.CheckOutDateTime = ijd.CheckOutDateTime.Add(targetDayTimeSpan);
                    }

                    // Calling points
                    foreach (InternationalJourneyCallingPoint ijcp in ijd.IntermediatesBefore)
                    {
                        if (ijcp.DepartureDateTime != DateTime.MinValue)
                        {
                            ijcp.DepartureDateTime = ijcp.DepartureDateTime.Add(targetDayTimeSpan);
                        }

                        if (ijcp.ArrivalDateTime != DateTime.MinValue)
                        {
                            ijcp.ArrivalDateTime = ijcp.ArrivalDateTime.Add(targetDayTimeSpan);
                        }
                    }

                    foreach (InternationalJourneyCallingPoint ijcp in ijd.IntermediatesLeg)
                    {
                        if (ijcp.DepartureDateTime != DateTime.MinValue)
                        {
                            ijcp.DepartureDateTime = ijcp.DepartureDateTime.Add(targetDayTimeSpan);
                        }

                        if (ijcp.ArrivalDateTime != DateTime.MinValue)
                        {
                            ijcp.ArrivalDateTime = ijcp.ArrivalDateTime.Add(targetDayTimeSpan);
                        }
                    }

                    foreach (InternationalJourneyCallingPoint ijcp in ijd.IntermediatesAfter)
                    {
                        if (ijcp.DepartureDateTime != DateTime.MinValue)
                        {
                            ijcp.DepartureDateTime = ijcp.DepartureDateTime.Add(targetDayTimeSpan);
                        }

                        if (ijcp.ArrivalDateTime != DateTime.MinValue)
                        {
                            ijcp.ArrivalDateTime = ijcp.ArrivalDateTime.Add(targetDayTimeSpan);
                        }
                    }
                }
            }

            return internationalJourneys;
        }

        #endregion

        #region Database

        /// <summary>
        /// Method which retrieves Interchange times (check in/out) for the specified Naptan. 
        /// Returns a dictionary of the interchange times found for the naptans
        /// </summary>
        /// <returns></returns>
        private InterchangeTimeValue GetInterchangeTime(string naptan, string operatorCode, bool checkIn, bool logResults)
        {
            // Return value, default to timespan of 0
            InterchangeTimeValue interchangeValue = new InterchangeTimeValue(naptan, new TimeSpan(0, 0, 0));

            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Check if the key exists in the data dictionary, if not add a default value to prevent 
            // repeated calls to the database for a Non-existent key
            InterchangeTimeKey tmpKey = new InterchangeTimeKey(naptan, string.Empty);

            // Look in cache
            InterchangeTimeValue tmpValue = intlData.GetInterchangeTime(tmpKey, checkIn);

            if (tmpValue == null)
            {
                // Value currently doesnt exist and hasnt been asked for before, add a default value
                intlData.AddInterchangeTime(tmpKey, interchangeValue, checkIn);

                #region Get interchange time data from database

                SqlDataReader sqlReader = null;
                try
                {
                    string fromNaptan = string.Empty;
                    string toNaptan = string.Empty;

                    // Determine the From and To naptan codes. 
                    // To get the checkin time, e.g. for Air, then the From will be 9200ABC and the To is 920FABC,
                    // with the F indicating this is the flight end. Refer to IF067.

                    if ((!string.IsNullOrEmpty(naptan)) && (naptan.Length >= 3)) 
                    {
                        // Assume International data also uses F for all airports and international rail/coach stations
                        string flightNaptan = naptan.Substring(0, 3) + "F" + naptan.Substring(4, naptan.Length - 4);

                        fromNaptan = checkIn ? naptan : flightNaptan;
                        toNaptan = checkIn ? flightNaptan : naptan;
                    }
                    // Don't pass the operator code, get all times for this naptan (prevents doing multiple database calls)

                    // Build up the sql request
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@FromNaPTAN", fromNaptan);
                    parameters.Add("@ToNaPTAN", toNaptan);

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInterchangeTime));
                    Logger.Write(oe);

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInterchangeTime, parameters);

                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalFromNaPTAN = sqlReader.GetOrdinal("FromNaPTAN");
                    int ordinalToNaPTAN = sqlReader.GetOrdinal("ToNaPTAN");
                    int ordinalFromOperator = sqlReader.GetOrdinal("FromOperator");
                    int ordinalToOperator = sqlReader.GetOrdinal("ToOperator");
                    int ordinalInterchangeTimeMinutes = sqlReader.GetOrdinal("InterchangeTimeMinutes");

                    #endregion

                    while (sqlReader.Read())
                    {
                        #region Read data

                        // Read the database values returned
                        string fromNaPTAN = sqlReader.GetString(ordinalFromNaPTAN);
                        string toNaPTAN = sqlReader.GetString(ordinalToNaPTAN);
                        string fromOperator = sqlReader.GetString(ordinalFromOperator);
                        string toOperator = sqlReader.GetString(ordinalToOperator); // To operator is not used
                        int interchangeTimeMinutes = sqlReader.GetInt32(ordinalInterchangeTimeMinutes);

                        #endregion

                        TimeSpan interchangeTimespan = new TimeSpan(0, interchangeTimeMinutes, 0);
                        InterchangeTimeKey interchangeTimeKey = new InterchangeTimeKey(string.Empty, string.Empty);
                        InterchangeTimeValue interchangeTimeValue = new InterchangeTimeValue(string.Empty, new TimeSpan(0, 0, 0));

                        if (checkIn)
                        {
                            interchangeTimeKey = new InterchangeTimeKey(fromNaptan.Trim(), fromOperator.Trim());
                            interchangeTimeValue = new InterchangeTimeValue(fromNaptan.Trim(), interchangeTimespan);
                        }
                        else
                        {
                            interchangeTimeKey = new InterchangeTimeKey(toNaptan.Trim(), fromOperator.Trim());
                            interchangeTimeValue = new InterchangeTimeValue(toNaptan.Trim(), interchangeTimespan);
                        }

                        // Add to the data dictionary cache
                        intlData.AddInterchangeTime(interchangeTimeKey, interchangeTimeValue, checkIn);

                        #region Log data result
                        if (logResults)
                        {
                            oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format(
                                    "International interchange time - ( fromNaptan[{0}] toNaptan[{1}] fromOperator[{2}] toOperator[{3}] interchangeTime[{4}] ). ",
                                    fromNaptan.PadRight(8),
                                    toNaptan.PadRight(8),
                                    fromOperator.PadRight(4),
                                    toOperator.PadRight(4),
                                    interchangeTimeMinutes.ToString().PadRight(3)));
                            Logger.Write(oe);
                        }
                        #endregion
                    }

                    #region Log

                    // Indicate no results were found
                    if ((!sqlReader.HasRows) && (logResults))
                    {
                        oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format(
                                    "International interchange time - ( fromNaptan[{0}] toNaptan[{1}] interchangeTime[{2}] ). ",
                                    fromNaptan.PadRight(8),
                                    toNaptan.PadRight(8),
                                    string.Empty.PadRight(3)));
                        Logger.Write(oe);
                    }
                    #endregion

                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalStop, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve InterchangeTime data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInterchangeTime);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve InterchangeTime data from the database[{0}], Exception Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingInterchangeTime);
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #endregion
            }
            
            // Cache should now be populated

            // Check for the naptan key with operator first
            InterchangeTimeKey key = new InterchangeTimeKey(naptan, operatorCode);
            interchangeValue = intlData.GetInterchangeTime(key, checkIn);

            if (interchangeValue == null)
            {
                // Transfer time not found, therefore get value without operator
                // ie. the default for this naptan which is added always at start of this method
                key = new InterchangeTimeKey(naptan, string.Empty);
                interchangeValue = intlData.GetInterchangeTime(key, checkIn);
            }
            
            return interchangeValue;
        }

        /// <summary>
        /// Method which retrieves the Transfer values for the specified TransferTimeKey containing the CityID and
        /// international Stop Naptan
        /// </summary>
        /// <returns></returns>
        private TransferTimeValue GetInternationalTransfer(TransferTimeKey transferKey, bool logResults)
        {
            // Return value, default to time of -1, thus indicating there is no transfer time set
            TransferTimeValue transferValue = new TransferTimeValue(-1, string.Empty);
            
            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];
            
            // Check if the key exists in the data dictionary, if not add a default value to prevent 
            // repeated calls to the database for a Non-existent key

            // Look in cache
            TransferTimeValue tmpValue = intlData.GetTransferTime(transferKey);

            if (tmpValue == null)
            {
                // Value currently doesnt exist and hasnt been asked for before, add a default value
                intlData.AddTransferTime(transferKey, transferValue);

                #region Get transfer time data from database

                SqlDataReader sqlReader = null;
                try
                {
                    // Build up the sql request
                    Hashtable parameters = new Hashtable();
                    parameters.Add("@CityId", transferKey.cityID);
                    parameters.Add("@StopNaPTAN", transferKey.stopNaptan);

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInternationalCityTransfer));
                    Logger.Write(oe);

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInternationalCityTransfer, parameters);

                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalTransferTimeMinutes = sqlReader.GetOrdinal("TransferTimeMinutes");
                    int ordinalTransferInformation = sqlReader.GetOrdinal("TransferInformation");

                    #endregion

                    int transferTimeMinutes = 0;
                    string transferInformation = string.Empty;

                    // Assume there is only one record that should be returned
                    while (sqlReader.Read())
                    {
                        #region Read data

                        // Read the database values returned
                        transferTimeMinutes = sqlReader.GetInt32(ordinalTransferTimeMinutes);
                        transferInformation = sqlReader.GetString(ordinalTransferInformation);

                        #endregion

                        #region Log data result
                        if (logResults)
                        {
                            oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format(
                                    "International transfer time - ( cityId[{0}] stopNaptan[{1}] transferTimeMinutes[{2}] transferInformation[{3}] ). ",
                                    transferKey.cityID.PadRight(4),
                                    transferKey.stopNaptan.PadRight(8),
                                    transferTimeMinutes.ToString().PadRight(3),
                                    transferInformation));
                            Logger.Write(oe);
                        }
                        #endregion
                    }

                    #region Log

                    // Indicate no results were found
                    if ((!sqlReader.HasRows) && (logResults))
                    {
                        oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format(
                                    "International transfer time - ( cityId[{0}] stopNaptan[{1}] transferTimeMinutes[{2}] transferInformation[{3}] ). ",
                                    transferKey.cityID.PadRight(4),
                                    transferKey.stopNaptan.PadRight(8),
                                    string.Empty.ToString().PadRight(3),
                                    string.Empty));
                        Logger.Write(oe);
                    }
                    #endregion

                    // Set up the return object
                    transferValue = new TransferTimeValue(transferTimeMinutes, transferInformation);

                    // Update the data dictionary cache for future access
                    intlData.AddTransferTime(transferKey, transferValue);
                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalCityTransfer, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve InternationalCityTransferTime data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingTransferTime);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve InternationalCityTransferTime data from the database[{0}], Exception Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingTransferTime);
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #endregion
            }

            // Cache should now be populated, with a default or database read value
            transferValue = intlData.GetTransferTime(transferKey);

            return transferValue;
        }

        /// <summary>
        /// Method returns the distance between the Origin and Destination stop code
        /// </summary>
        public int GetStopDistance(string originStopCode, string destinationStopCode)
        {
            // Data dictionay cache
            InternationalPlannerData intlData = (InternationalPlannerData)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerDataFactory];

            // Check if distance data has been setup
            if (!intlData.IsStopDistancesDataSet())
            {
                #region Get all stop distance data

                SqlDataReader sqlReader = null;
                try
                {
                    Hashtable parameters = new Hashtable();

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInternationalStopDistance));
                    Logger.Write(oe);

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInternationalStopDistance, parameters);

                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalOriginStopCode = sqlReader.GetOrdinal("OriginStopCode");
                    int ordinalDestinationStopCode = sqlReader.GetOrdinal("DestinationStopCode");
                    int ordinalDistance = sqlReader.GetOrdinal("Distance");
                    #endregion
                                        
                    while (sqlReader.Read())
                    {
                        #region Read data
                        // Read the database values returned
                        string originStop = sqlReader.GetString(ordinalOriginStopCode);
                        string destinationStop = sqlReader.GetString(ordinalDestinationStopCode);
                        int distanceMetres = sqlReader.GetInt32(ordinalDistance);
                        #endregion

                        #region Update data dictionary cache

                        // Update the data for future access
                        intlData.AddStopDistance(originStop, destinationStop, distanceMetres);

                        #endregion
                    }

                    if (!sqlReader.HasRows)
                    {
                        // Avoid calling the database again if no rows have been setup. Adding a dummy
                        // value sets the flag indicating distances have been loaded
                        intlData.AddStopDistance("default", "default", 0);
                    }

                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalStopDistance, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve international stop distances data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInternationalStop);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve international stop distances data from the database[{0}], TDException Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingInternationalStop);
                }
                finally
                {
                    //close the database connections
                    if (sqlReader != null)
                        sqlReader.Close();

                    if (sqlHelper.ConnIsOpen)
                        sqlHelper.ConnClose();
                }
                #endregion

                #endregion
            }

            return intlData.GetStopDistance(originStopCode, destinationStopCode);
        }

        #endregion

        #region Create

        /// <summary>
        /// Method which creates an international country
        /// </summary>
        /// <returns></returns>
        public InternationalCountry CreateInternationalCountry(string countryCode, string countryCodeIANA, 
            string countryAdminCodeUIC, int countryTimeZone)
        {
            InternationalCountry internationalCountry = new InternationalCountry();

            internationalCountry.CountryCode = countryCode.Trim().ToUpper();
            internationalCountry.CountryCodeIANA = countryCodeIANA.Trim().ToUpper();
            internationalCountry.AdminCodeUIC = countryAdminCodeUIC.Trim().ToUpper();
            internationalCountry.TimeZone = countryTimeZone;

            return internationalCountry;
        }

        /// <summary>
        /// Method which creates an international stop
        /// </summary>
        /// <returns></returns>
        private InternationalStop CreateInternationalStop(
            string stopCode, string stopNaptan, InternationalModeType stopType, string stopName,
            int stopOSGREasting, int stopOSGRNorthing, string stopTerminal, 
            string stopInfoURL, string stopInfoDesc, string stopDeptURL,
            string stopDeptDesc, string stopAccessURL, string stopAccessDesc,
            InternationalCountry stopCountry)
        {
            InternationalStop internationalStop = new InternationalStop();

            internationalStop.StopCode = stopCode;
            internationalStop.StopNaptan = stopNaptan;
            internationalStop.StopType = stopType;
            internationalStop.StopName = stopName;
            internationalStop.StopOSGREasting = stopOSGREasting;
            internationalStop.StopOSGRNorthing = stopOSGRNorthing;
            internationalStop.StopTerminal = stopTerminal;
            internationalStop.StopInfoURL = stopInfoURL; 
            internationalStop.StopInfoDesc =stopInfoDesc;
            internationalStop.StopDeptURL = stopDeptURL;
            internationalStop.StopDeptDesc = stopDeptDesc;
            internationalStop.StopAccessURL = stopAccessURL;
            internationalStop.StopAccessDesc = stopAccessDesc;
            internationalStop.StopCountry = stopCountry;

            return internationalStop;
        }

        /// <summary>
        /// Method which creates an international city
        /// </summary>
        /// <returns></returns>
        public InternationalCity CreateInternationalCity(
            string cityID, string cityName,
            int cityOSGREasting, int cityOSGRNorthing, 
            string cityInfoURL, InternationalCountry cityCountry)
        {
            InternationalCity internationalCity = new InternationalCity();

            internationalCity.CityID = cityID;
            internationalCity.CityName = cityName;
            internationalCity.CityOSGREasting = cityOSGREasting;
            internationalCity.CityOSGRNorthing = cityOSGRNorthing;
            internationalCity.CityInfoURL = cityInfoURL;
            internationalCity.CityCountry = cityCountry;

            return internationalCity;
        }

        /// <summary>
        /// Method which creates an international journey
        /// </summary>
        /// <returns></returns>
        private InternationalJourney CreateInternationalJourney(
            string departureCityID, string arrivalCityID, 
            string departureStopNaptan, string arrivalStopNaptan, 
            DateTime departureDateTime, DateTime arrivalDateTime, DayOfWeek[] daysOfOperation,
            InternationalModeType modeType, InternationalJourneyDetail[] journeyDetails)
        {
            InternationalJourney internationalJourney = new InternationalJourney();

            // Set up the journey
            internationalJourney.DepartureCityID = departureCityID;
            internationalJourney.ArrivalCityID = arrivalCityID;
            internationalJourney.DepartureStopNaptan = departureStopNaptan;
            internationalJourney.ArrivalStopNaptan = arrivalStopNaptan;
            internationalJourney.DepartureDateTime = departureDateTime;
            internationalJourney.ArrivalDateTime = arrivalDateTime;
            internationalJourney.DaysOfOperation = daysOfOperation;
            
            internationalJourney.ModeType = modeType;
            internationalJourney.JourneyDetails = journeyDetails;

            // Default values - these values will be updated later if needed by the relevant methods
            internationalJourney.Emissions = 0;

            return internationalJourney;
        }

        /// <summary>
        /// Method which creates an international journey detail leg
        /// </summary>
        /// <returns></returns>
        private InternationalJourneyDetail CreateInternationalJourneyDetail(
            InternationalJourneyDetailType detailType,
            string departureStopNaptan, string arrivalStopNaptan,
            DateTime departureDateTime, DateTime arrivalDateTime,
            string operatorCode, string serviceNumber,
            string aircraftTypeCode, string terminalNumberFrom, string terminalNumberTo)
        {
            InternationalJourneyDetail internationalJourneyDetail = new InternationalJourneyDetail();

            // Set the type
            internationalJourneyDetail.DetailType = detailType;

            // Set up the journey detail leg
            internationalJourneyDetail.DepartureStopNaptan = departureStopNaptan;
            internationalJourneyDetail.ArrivalStopNaptan = arrivalStopNaptan;
            internationalJourneyDetail.DepartureDateTime = departureDateTime;
            internationalJourneyDetail.ArrivalDateTime = arrivalDateTime;

            // Update the detail values
            internationalJourneyDetail.OperatorCode = operatorCode;
            internationalJourneyDetail.ServiceNumber = serviceNumber;
            internationalJourneyDetail.AircraftTypeCode = aircraftTypeCode;
            internationalJourneyDetail.TerminalNumberFrom = terminalNumberFrom;
            internationalJourneyDetail.TerminalNumberTo = terminalNumberTo;

            return internationalJourneyDetail;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Method which parses a stop type value from the database into an InternationalModeType
        /// </summary>
        /// <param name="stopType"></param>
        /// <returns></returns>
        private InternationalModeType GetModeTypeForStopType(string stopType, string stopCode)
        {
            try{

                switch (stopType.Trim().ToUpper())
                {
                    case "A":
                        return InternationalModeType.Air;
                    case "C":
                        return InternationalModeType.Coach;
                    case "R":
                        return InternationalModeType.Rail;
                }

                // If not parsed then need to throw an exception as unsupported stop supplied
                throw new TDException();
            }
            catch
            {
                if (string.IsNullOrEmpty(stopType))
                {
                    stopType = "";
                }

                // Invalid/unrecognised mode
                throw new TDException(
                    string.Format("Error parsing the international StopType[{0}] for StopCode[{1}]", stopType, stopCode),
                    false,
                    TDExceptionIdentifier.IPUnrecognisedInternationalStopModeType);
            }
        }

        /// <summary>
        /// Method which parses a the days of operationa value from the database into a DayOfWeek enum list.
        /// Assumes the days are supplied as a string "MoTuWeTh..."
        /// </summary>
        /// <param name="stopType"></param>
        /// <returns></returns>
        private DayOfWeek[] GetDaysOfOperation(string daysOfOperation, string departureStopCode, string arrivalStopCode)
        {
            try
            {
                List<DayOfWeek> listDaysOfOperation = new List<DayOfWeek>();

                // having no days could be a valid value
                if ((string.IsNullOrEmpty(daysOfOperation))
                    ||
                    (string.IsNullOrEmpty(daysOfOperation.Trim())))
                {
                    return listDaysOfOperation.ToArray();
                }

                // Remove whitespace
                daysOfOperation = daysOfOperation.Trim();
                
                // Determine if working with interger representation or char representation of days
                bool parseAsChars = true;
                string tmpDay = daysOfOperation.Substring(0, 1);
                int tmpDayInt;
                if (Int32.TryParse(tmpDay, out tmpDayInt))
                {
                    // Parsed into a number, so need to parse as Int
                    parseAsChars = false;
                }

                

                int count = 0;
                while (count < daysOfOperation.Length)
                {
                    string day = string.Empty;

                    // Check valid number of characters for day
                    if (parseAsChars && ((count + 2) <= daysOfOperation.Length))
                    {
                        day = daysOfOperation.Substring(count, 2);
                        day = day.Trim().ToUpper();
                    }
                    else if (!parseAsChars && ((count + 1) <= daysOfOperation.Length))
                    {
                        day = daysOfOperation.Substring(count, 1);
                        day = day.Trim().ToUpper();
                    }
                    else
                    {
                        // Unexpected string length detected
                        throw new TDException();
                    }

                    switch (day)
                    {
                        case "MO":
                        case "1":
                            listDaysOfOperation.Add(DayOfWeek.Monday);
                            break;
                        case "TU":
                        case "2":
                            listDaysOfOperation.Add(DayOfWeek.Tuesday);
                            break;
                        case "WE":
                        case "3":
                            listDaysOfOperation.Add(DayOfWeek.Wednesday);
                            break;
                        case "TH":
                        case "4":
                            listDaysOfOperation.Add(DayOfWeek.Thursday);
                            break;
                        case "FR":
                        case "5":
                            listDaysOfOperation.Add(DayOfWeek.Friday);
                            break;
                        case "SA":
                        case "6":
                            listDaysOfOperation.Add(DayOfWeek.Saturday);
                            break;
                        case "SU":
                        case "7":
                            listDaysOfOperation.Add(DayOfWeek.Sunday);
                            break;
                        default:
                            // Any other value is an unrecognised day
                            throw new TDException();
                    }

                    // Move on to the next day in the string
                    count += parseAsChars ? 2 : 1;
                }

                return listDaysOfOperation.ToArray();
            }
            catch
            {
                // Invalid/unrecognised daysOfOperation value
                throw new TDException(
                    string.Format("Error parsing the international journey value DaysOfOperation[{0}] for DepartureStopCode[{1}] ArrivalStopCode[{2}]", daysOfOperation, departureStopCode, arrivalStopCode),
                    false,
                    TDExceptionIdentifier.IPUnrecognisedDaysOfOperation);
            }
        }

        /// <summary>
        /// Method which parses a string of service facility numbers and converts in to an array of int.
        /// Assumes the numbers in the string are seperated by a "|"
        /// </summary>
        private int[] GetServiceFacilities(string serviceFacilities)
        {
            List<int> intServiceFacilities = new List<int>();

            if (!string.IsNullOrEmpty(serviceFacilities))
            {
                string[] strServiceFacilities = serviceFacilities.Split('|');

                int sfId = -1;

                foreach (string sf in strServiceFacilities)
                {
                    if (Int32.TryParse(sf, out sfId))
                    {
                        intServiceFacilities.Add(sfId);
                    }
                }
            }

            return intServiceFacilities.ToArray();
        }

        /// <summary>
        /// Method which updates the journey dates and times using the planned journey date and the times
        /// returned in the journey schedules data
        /// </summary>
        /// <param name="stopType"></param>
        /// <returns></returns>
        private void GetJourneyDateTime(DateTime journeyDateTime, TimeSpan departureTimespan, int departureDay, 
            TimeSpan arrivalTimespan, int arrivalDay, out DateTime departureDateTime, out DateTime arrivalDateTime)
        {
            // It is assumed the schedule times read in from the schedule data are in the country's local time, 
            // e.g. Depart UK at 07:00, Arrive France at 07:30 - which would by 08:30+01:00 UTC time.
            // As the Portal UI must display journeys in the country local time, no special handling is currently
            // needed for creating the departure and arrival datetimes. 
            // However, the UpdateJourneyDurationTime method calculates the journey durations taking in to account
            // the country timezone.

            // The departure date time returned
            departureDateTime = new DateTime(
                journeyDateTime.Year, journeyDateTime.Month, journeyDateTime.Day,
                departureTimespan.Hours, departureTimespan.Minutes, departureTimespan.Seconds);

            // The arrival date time returned
            arrivalDateTime = new DateTime(
                journeyDateTime.Year, journeyDateTime.Month, journeyDateTime.Day,
                arrivalTimespan.Hours, arrivalTimespan.Minutes, arrivalTimespan.Seconds);

            // Update the departure date based on the departure days
            if (departureDay > 0)
            {
                departureDateTime = departureDateTime.AddDays(departureDay);
            }

            // Update the arrival date based on the arrival days
            if (arrivalDay > 0)
            {
                arrivalDateTime = arrivalDateTime.AddDays(arrivalDay);
            }
        }

        /// <summary>
        /// Method which checks if the requested day is in the days valid DayOfWeek array
        /// </summary>
        /// <param name="intlJourneys"></param>
        /// <returns></returns>
        private bool IsJourneyValidForDay(DayOfWeek[] days, DayOfWeek requestedDay)
        {
            List<DayOfWeek> validDays = new List<DayOfWeek>(days);

            // Is the journey valid for the requested day
            if (validDays.Contains(requestedDay))
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        #endregion
    }
}
