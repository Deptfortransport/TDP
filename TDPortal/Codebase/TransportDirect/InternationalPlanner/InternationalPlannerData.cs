// *********************************************** 
// NAME			: InternationalPlannerData.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 08/02/2010
// DESCRIPTION	: Data Class to hold and cache all International Planner data needed for returning journey plans.
//              : Data is loaded and cached only when requested for the first time.
//              : Change notification is implemented, which clears out the data only and does not reload.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalPlannerData.cs-arc  $
//
//   Rev 1.11   Oct 10 2012 14:26:56   mmodi
//Updated trace logging level
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.10   May 05 2010 09:54:44   RBroddle
//Slight amendment to syntax in LoadValidRoutesForInternationalCities() as build process did not like it. (though it was ok in VS)
//
//   Rev 1.9   Apr 23 2010 15:58:34   mmodi
//Added code to load valid City routes and to generate a Javascript file using this, which is to be used by the international input page to allow the From/To dropdown lists to be filtered based on selected From/To location
//Resolution for 5521: TD Extra - Drop Down List Change - CCN0575
//
//   Rev 1.8   Mar 16 2010 14:42:52   mmodi
//Updated to state distance is in metres
//Resolution for 5461: TD Extra - Code review changes
//
//   Rev 1.7   Mar 05 2010 11:48:00   mmodi
//Updated to allow a deep clone of the journey to be returned from the data cache
//
//   Rev 1.6   Mar 01 2010 13:33:32   mmodi
//Reset stop distances flag
//Resolution for 5424: TD Extra - Reset data cache needed
//
//   Rev 1.5   Mar 01 2010 11:50:22   mmodi
//Added code to periodically clear out the data 
//Resolution for 5424: TD Extra - Reset data cache needed
//
//   Rev 1.4   Feb 25 2010 13:19:46   mmodi
//Updated change notification group name
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 19 2010 15:57:52   mmodi
//Updated to add distances to journey details
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 16 2010 17:43:16   mmodi
//Added permitted journey data
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 12 2010 09:40:42   mmodi
//Updated to plan train journeys and save journeys to cache
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 09:52:28   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Data Class to hold and cache all International Planner data needed for returning journey plans.
    /// </summary>
    public class InternationalPlannerData
    {
        #region Private members

        private const string DataChangeNotificationGroup = "InternationalPlanner";
        private bool receivingChangeNotifications;

        // Stored procedures
        private const string SP_GetInternationalCityAll = "GetAllInternationalCities";
        private const string SP_GetInternationalCityRoute = "GetInternationalCityRoute";

        // Database
        private SqlHelper sqlHelper = new SqlHelper();
        private const SqlHelperDatabase database = SqlHelperDatabase.InternationalDataDB;
        private InternationalPlannerHelper intlPlannerHelper = new InternationalPlannerHelper();

        // Name to use when generating the JavaScript file
        public const string ScriptName = "InternationalDataDeclarations";
        private bool scriptGenerated = false;

        // Variables to periodically clear the cache
        private static readonly object clearDataLock = new object();
        private DateTime nextClearDataTime = new DateTime();

        #region Dictionaries

        // Dictionaries to hold values read from database, to prevent multiple repeated calls to 
        // get the data from the database
        private Dictionary<string, InternationalStop> dictInternationalStops = new Dictionary<string, InternationalStop>();
        private Dictionary<string, InternationalCity> dictInternationalCities = new Dictionary<string, InternationalCity>();
        private bool dictInternationalCitiesSetup = false;
        private Dictionary<InterchangeTimeKey, InterchangeTimeValue> dictCheckInInterchangeTimes = new Dictionary<InterchangeTimeKey, InterchangeTimeValue>();
        private Dictionary<InterchangeTimeKey, InterchangeTimeValue> dictCheckOutInterchangeTimes = new Dictionary<InterchangeTimeKey, InterchangeTimeValue>();
        private Dictionary<TransferTimeKey, TransferTimeValue> dictTransferTimes = new Dictionary<TransferTimeKey, TransferTimeValue>();
        private Dictionary<StopStopKey, int> dictStopDistances = new Dictionary<StopStopKey, int>();
        private bool dictStopDistancesSetup = false;

        // Dictionaries to cache planned international journeys, to prevent repeated creation of journeys
        // where only the dates may be different
        private Dictionary<InternationalJourneyKey, InternationalJourney[]> dictInternationalJourneysAir = new Dictionary<InternationalJourneyKey, InternationalJourney[]>();
        private Dictionary<InternationalJourneyKey, InternationalJourney[]> dictInternationalJourneysCoach = new Dictionary<InternationalJourneyKey, InternationalJourney[]>();
        private Dictionary<InternationalJourneyKey, InternationalJourney[]> dictInternationalJourneysRail = new Dictionary<InternationalJourneyKey, InternationalJourney[]>();
        private Dictionary<InternationalJourneyKey, InternationalJourney[]> dictInternationalJourneysCar = new Dictionary<InternationalJourneyKey, InternationalJourney[]>();

        // Dictionary to define which country pairs international planner journeys are allowed
        private Dictionary<PermittedCountryKey, bool> dictPermittedCountry = new Dictionary<PermittedCountryKey, bool>();
        private bool dictPermittedCountrySetup = false;
        private Dictionary<string, InternationalCity[]> dictCityRouteMatrix = new Dictionary<string, InternationalCity[]>();
        private bool dictCityRouteMatrixSetup = false;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InternationalPlannerData()
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                "Creating international planner data cache."));

            // Introduced to allow creating javascript file containing city data during application
            // intialisation
            LoadData();

            receivingChangeNotifications = RegisterForChangeNotification();

            SetNextClearDataTime();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads data needed imediately by the data cache. 
        /// The rest is loaded by the InternationalPlannerHelper class on a need only basis
        /// </summary>
        private void LoadData()
        {
            LoadAllInternationalCities(true);
            LoadValidRoutesForInternationalCities();

            // Generate the international city data javascript file
            lock (this)
            {
                this.scriptGenerated = GenerateJavascript();
            }
        }

        /// <summary>
        /// Method which loads all international city data from the InternationalData database 
        /// and parses the result into an InternationalCity objects, adding them to the class dictionary
        /// </summary>
        /// <param name="naptans"></param>
        /// <returns></returns>
        private void LoadAllInternationalCities(bool logResults)
        {
            if (!dictInternationalCitiesSetup)
            {
                #region Get cities data from database

                SqlDataReader sqlReader = null;
                try
                {
                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInternationalCityAll));
                    Logger.Write(oe);

                    Dictionary<string, InternationalCity> newDictInternationalCities = new Dictionary<string, InternationalCity>();

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInternationalCityAll, new Hashtable());

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
                        InternationalCountry intlCountry = intlPlannerHelper.CreateInternationalCountry(countryCode, countryCodeIANA, countryAdminCodeUIC, countryTimeZone);

                        // Build the InternationalCity
                        InternationalCity intlCity = intlPlannerHelper.CreateInternationalCity(cityId, cityName, cityOSGREasting, cityOSGRNorthing, cityURL, intlCountry);

                        // Add to the new array
                        newDictInternationalCities.Add(intlCity.CityID, intlCity);

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

                    // Assign new list
                    lock (this)
                    {
                        this.dictInternationalCities = newDictInternationalCities;
                        this.dictInternationalCitiesSetup = true;
                    }
                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalCityAll, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve All InternationalCity data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInternationalCity);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve All InternationalCity data from the database[{0}], Exception Message[{1}].",
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
        }

        /// <summary>
        /// Method which returns all the valid cities the specified city id can travel to.
        /// Public to allow the InternationalPlannerHelper data to force a reload if the 
        /// data expiry time has elapsed and ResetData was called by the application
        /// </summary>
        public void LoadValidRoutesForInternationalCities()
        {
            // Check if city route matrix data has been setup
            if (!dictCityRouteMatrixSetup)
            {
                if (!dictInternationalCitiesSetup)
                {
                    // Valid routes requires city data to have been setup
                    LoadAllInternationalCities(true);
                }

                #region Get city route matrix data

                SqlDataReader sqlReader = null;

                Dictionary<string, List<string>> cityRouteMatrix = new Dictionary<string, List<string>>();

                try
                {
                    Hashtable parameters = new Hashtable();

                    // Log
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose,
                                string.Format("Opening database connection to [{0}] and executing stored procedure[{1}].",
                                    database, SP_GetInternationalCityRoute));
                    Logger.Write(oe);

                    Dictionary<string, InternationalCity[]> newDictCityRouteMatrix = new Dictionary<string, InternationalCity[]>();

                    sqlHelper.ConnOpen(database);

                    // Call stored procedure
                    sqlReader = sqlHelper.GetReader(SP_GetInternationalCityRoute, parameters);

                    #region Column ordinals
                    // Assign the column ordinals
                    int ordinalDepartureCityId = sqlReader.GetOrdinal("DeparturetCityId");
                    int ordinalArrivalCityId = sqlReader.GetOrdinal("ArrivalCityId");
                    #endregion

                    // Any values returned, then there are valid city routes
                    while (sqlReader.Read())
                    {
                        #region Read data
                        // Read the database values returned
                        string departureCityId = Convert.ToString(sqlReader.GetInt32(ordinalDepartureCityId));
                        string arrivalCityId = Convert.ToString(sqlReader.GetInt32(ordinalArrivalCityId));

                        #endregion

                        // Store in the temp dictionary
                        if (cityRouteMatrix.ContainsKey(departureCityId))
                        {
                            List<string> arrivalCities = cityRouteMatrix[departureCityId];

                            if (!arrivalCities.Contains(arrivalCityId))
                            {
                                arrivalCities.Add(arrivalCityId);
                            }

                            cityRouteMatrix[departureCityId] = arrivalCities;
                        }
                        else
                        {
                            List<string> arrivalCities = new List<string>();
                            arrivalCities.Add(arrivalCityId);

                            cityRouteMatrix[departureCityId] = arrivalCities;
                        }
                    }

                    #region Populate the International Cities and update the data dictionary cache

                    foreach (string cityIdKey in cityRouteMatrix.Keys)
                    {
                        List<string> arrivalCityIds = cityRouteMatrix[cityIdKey];

                        // Array to hold the City objects where this city id can travel to
                        List<InternationalCity> arrivalCities = new List<InternationalCity>();

                        foreach (string arrivalCityId in arrivalCityIds.ToArray())
                        {
                            InternationalCity city = dictInternationalCities[arrivalCityId];

                            arrivalCities.Add(city);
                        }

                        newDictCityRouteMatrix.Add(cityIdKey, arrivalCities.ToArray());
                    }

                    #endregion

                    // Assign new list
                    lock (this)
                    {
                        this.dictCityRouteMatrix = newDictCityRouteMatrix;
                        this.dictCityRouteMatrixSetup = true;
                    }
                }
                #region Error handling
                catch (SqlException sqlEx)
                {
                    // SQLHelper does not catch SqlException so catch here
                    throw new TDException(string.Format(
                        "SQL Helper error when excuting stored procedure [{0}], Message[{1}]",
                        SP_GetInternationalCityRoute, sqlEx.Message),
                        sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure);
                }
                catch (TDException tdEx)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve international city route matrix data from the database[{0}], TDException Message[{1}].",
                        database,
                        tdEx.Message),
                        tdEx, false, TDExceptionIdentifier.IPErrorRetrievingInternationalData);
                }
                catch (Exception ex)
                {
                    throw new TDException(string.Format(
                        "Error occurred attempting to retrieve international city route matrix data from the database[{0}], TDException Message[{1}].",
                        database,
                        ex.Message),
                        ex, false, TDExceptionIdentifier.IPErrorRetrievingInternationalData);
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
        }

        /// <summary>
        /// Generates a javascript file containing the declarations for client side code.
        /// Only works if the script repository service is present in TDServiceDiscovery.
        /// </summary>
        /// <returns></returns>
        private bool GenerateJavascript()
        {
            ScriptRepository.ScriptRepository scriptRep;
            try
            {
                scriptRep = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
            }
            catch
            {
                // Failed to get a reference to script repository, so don't create the file
                if (TDTraceSwitch.TraceWarning)
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "Script repository not present in TDServiceDiscovery, so JavaScript file will not be created."));
                return false;
            }

            try
            {
                #region Generate javascript

                // Get the international cities
                List<InternationalCity> cities = new List<InternationalCity>();
                foreach (string cityKey in dictInternationalCities.Keys)
                {
                    cities.Add(dictInternationalCities[cityKey]);
                }

                // A StringWriter is used to build the file. This maintains the text in an underlying
                // StringBuilder, which is then passed to the script repository to create the file.
                StringWriter js = new StringWriter(CultureInfo.InvariantCulture);
                DateTime now = DateTime.Now;
                js.WriteLine(String.Format("// {0} file generated at {1}/{2}/{3} {4}:{5}.{6}", ScriptName, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second));

                js.WriteLine("var cities = new Array({0});", ArrayListJoin(new ArrayList(cities), typeof(InternationalCity), "CityID", "\"{0}\"", ", "));
                js.WriteLine("var cityNames;");
                js.WriteLine("var cityRouteTable;");
                js.WriteLine();

                js.WriteLine("populateInternationalCityDataArrays();");
                js.WriteLine();

                js.WriteLine("function populateInternationalCityDataArrays()");
                js.WriteLine("{");

                js.WriteLine("cityNames = new Array();");
                js.WriteLine("cityRouteTable = new Array();");
                js.WriteLine();

                foreach (InternationalCity city in cities)
                {
                    js.WriteLine("cityNames[\"{0}\"] = \"{1}\";", city.CityID, city.CityName);

                    // Get the route matrix values for this city
                    InternationalCity[] arrivalCities =  dictCityRouteMatrix[city.CityID];

                    js.WriteLine("cityRouteTable[\"{0}\"] = new Array({1});", city.CityID, ArrayListJoin(new ArrayList(arrivalCities), typeof(InternationalCity), "CityID", "\"{0}\"", ", "));
                }

                js.WriteLine("}");

                scriptRep.AddTempScript(ScriptName, "W3C_STYLE", js.GetStringBuilder());

                js.Close();

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Temporary JavaScript file for Internation City Data generated"));

                #endregion

                return true;
            }
            catch (Exception e)
            {
                if (TDTraceSwitch.TraceWarning)
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "Unexpected error occurred creating International City Data javascript file.", e));
                return false;
            }
        }


        /// <summary>
        /// Registers an event handler with the data change notification service
        /// </summary>
        private bool RegisterForChangeNotification()
        {
            IDataChangeNotification notificationService;
            try
            {
                notificationService = (IDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
            }
            catch (TDException e)
            {
                // If the SDInvalidKey TDException is thrown, return false as the notification service
                // hasn't been initialised.
                // Otherwise, rethrow the exception that was received.
                if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising InternationalPlannerData"));
                    return false;
                }
                else
                    throw;
            }
            catch
            {
                // Non-CLS-compliant exception
                throw;
            }

            notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
            return true;
        }

        #region Reset Data methods

        /// <summary>
        /// Method which resets the International Stops data
        /// </summary>
        private void ResetInternationalStopData()
        {
            dictInternationalStops.Clear();
        }

        /// <summary>
        /// Method which resets the International City data
        /// </summary>
        private void ResetInternationalCityData()
        {
            dictInternationalCities.Clear();
            dictInternationalCitiesSetup = false;
        }

        /// <summary>
        /// Method which resets the Interchange (check in/out) data
        /// </summary>
        private void ResetInterchangeData()
        {
            dictCheckInInterchangeTimes.Clear();
            dictCheckOutInterchangeTimes.Clear();
        }

        /// <summary>
        /// Method which resets the City Transfer time data
        /// </summary>
        private void ResetCityTransferData()
        {
            dictTransferTimes.Clear();
        }

        /// <summary>
        /// Method which resets the International Journeys data
        /// </summary>
        private void ResetInternationalJourneys()
        {
            dictInternationalJourneysAir.Clear();
            dictInternationalJourneysCoach.Clear();
            dictInternationalJourneysRail.Clear();
            dictInternationalJourneysCar.Clear();
        }

        /// <summary>
        /// Method which resets the Stop to Stop distances data
        /// </summary>
        private void ResetStopDistances()
        {
            dictStopDistances.Clear();
            dictStopDistancesSetup = false;
        }

        /// <summary>
        /// Method which resets the Permitted country data
        /// </summary>
        private void ResetPermittedCountry()
        {
            dictPermittedCountry.Clear();
            dictPermittedCountrySetup = false; 
        }

        /// <summary>
        /// Method which resets the City route matrix data
        /// </summary>
        private void ResetCityRouteMatrix()
        {
            dictCityRouteMatrix.Clear();
            dictCityRouteMatrixSetup = false;
        }

        /// <summary>
        /// Method which clears all data held in the class
        /// </summary>
        private void ClearData()
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                "Clearing international planner data."));

            ResetCityTransferData();
            ResetInterchangeData();
            ResetInternationalCityData();
            ResetInternationalStopData();
            ResetInternationalJourneys();
            ResetStopDistances();
            ResetPermittedCountry();
            ResetCityRouteMatrix();
        }

        /// <summary>
        /// Method to set the clear data time
        /// </summary>
        private void SetNextClearDataTime()
        {
            try
            {
                // Default time is one day from now
                nextClearDataTime = DateTime.Now.AddDays(1);

                // Get the minutes
                int minutes = Int32.Parse(Properties.Current["InternationalPlanner.DataCache.Reset.Minutes"]);

                // Check for a sensible value (lets say 20 mins minimum)
                if (minutes >= 20)
                {
                    nextClearDataTime = DateTime.Now.AddMinutes(minutes);
                }

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                    string.Format("International planner data reset time has been set to [{0}].",
                        nextClearDataTime)));
            }
            catch (Exception ex)
            {
                // Any exceptions, log it. Already set a default date time above.
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, 
                    string.Format("Error attempting to read and parse property [InternationalPlanner.DataCache.Reset.Minutes], defaulting data reset time to be [{0}]. Exception: {1}",
                    nextClearDataTime, ex.Message),
                    ex));
            }
        }

        /// <summary>
        /// Method which checks if the data needs to be reset
        /// </summary>
        private void CheckForResetData()
        {
            DateTime now = DateTime.Now;

            if (now > nextClearDataTime)
            {
                // Add lock to prevent multiple threads clearing data
                lock (clearDataLock)
                {
                    if (now > nextClearDataTime)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("International planner data has exceeded its expiry date time [{0}].", nextClearDataTime)));

                        SetNextClearDataTime();

                        ClearData();
                    }
                }
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Helper function.
        /// Given an arraylist containing objects of a specific type, builds a string containing
        /// the value of the given property of each object, formatted and concatenated into a
        /// single string - similar the String.Join function.
        /// </summary>
        /// <param name="items">The ArrayList of items to be joined. All items in the list must be of the same type.</param>
        /// <param name="objectType">A Type object containing the type of the objects in the ArrayList</param>
        /// <param name="propertyName">Name of the property whose value will be used in the output string</param>
        /// <param name="formatString">Format string that will be applied to the property.</param>
        /// <param name="separator">String to insert between each value</param>
        /// <returns></returns>
        private string ArrayListJoin(ArrayList items, Type objectType, string propertyName, string formatString, string separator)
        {
            PropertyInfo p = objectType.GetProperty(propertyName);
            StringBuilder s = new StringBuilder();
            bool isFirst = true;
            foreach (object o in items)
            {
                if (!isFirst)
                    s.Append(separator);
                else
                    isFirst = false;

                s.AppendFormat(formatString, p.GetValue(o, null).ToString());
            }
            return s.ToString();
        }

        #endregion

        #endregion

        #region Event handler

        /// <summary>
        /// Used by the Data Change Notification service to clear the data if it is changed in the DB
        /// </summary>
        private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
        {
            if (e.GroupId == DataChangeNotificationGroup)
            {
                ClearData();
                LoadData();
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to return an InternationalStop, null if it doesnt exist
        /// </summary>
        public InternationalStop GetInternationalStop(string stopNaptan)
        {
            CheckForResetData();

            if (!string.IsNullOrEmpty(stopNaptan) && dictInternationalStops.ContainsKey(stopNaptan))
            {
                return dictInternationalStops[stopNaptan];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method to add an InternationalStop to the data dictionary
        /// </summary>
        public void AddInternationalStop(InternationalStop internationalStop)
        {
            if (internationalStop != null)
            {
                // Remove from dictionary
                if (dictInternationalStops.ContainsKey(internationalStop.StopNaptan))
                {
                    dictInternationalStops.Remove(internationalStop.StopNaptan);
                }

                // Add to dictionary
                dictInternationalStops.Add(internationalStop.StopNaptan, internationalStop);
            }
        }

        /// <summary>
        /// Method to return an InternationalCity, null if it doesnt exist
        /// </summary>
        public InternationalCity GetInternationalCity(string cityID)
        {
            CheckForResetData();

            if (!string.IsNullOrEmpty(cityID) && dictInternationalCities.ContainsKey(cityID))
            {
                return dictInternationalCities[cityID];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method to add an InternationalCity to the data dictionary
        /// </summary>
        public void AddInternationalCity(InternationalCity internationalCity)
        {
            if (internationalCity != null)
            {
                // Remove from dictionary
                if (dictInternationalCities.ContainsKey(internationalCity.CityID))
                {
                    dictInternationalCities.Remove(internationalCity.CityID);
                }

                // Add to dictionary
                dictInternationalCities.Add(internationalCity.CityID, internationalCity);
            }
        }

        /// <summary>
        /// Method to return the interchange timespan for the specified InterchangeTimeKey.
        /// Returns null if not found
        /// </summary>
        public InterchangeTimeValue GetInterchangeTime(InterchangeTimeKey key, bool checkIn)
        {
            CheckForResetData();

            if (checkIn && dictCheckInInterchangeTimes.ContainsKey(key))
            {
                return dictCheckInInterchangeTimes[key];
            }
            else if (!checkIn && dictCheckOutInterchangeTimes.ContainsKey(key))
            {
                return dictCheckOutInterchangeTimes[key];
            }

            return null;
        }

        /// <summary>
        /// Method to add an Interchange time to the data dictionary
        /// </summary>
        public void AddInterchangeTime(InterchangeTimeKey key, InterchangeTimeValue timeSpan, bool checkIn)
        {
            if (checkIn)
            {
                // Remove from dictionary
                if (dictCheckInInterchangeTimes.ContainsKey(key))
                {
                    dictCheckInInterchangeTimes.Remove(key);
                }

                // Add to dictionary
                dictCheckInInterchangeTimes.Add(key, timeSpan);
            }
            else
            {
                // Remove from dictionary
                if (dictCheckOutInterchangeTimes.ContainsKey(key))
                {
                    dictCheckOutInterchangeTimes.Remove(key);
                }

                // Add to dictionary
                dictCheckOutInterchangeTimes.Add(key, timeSpan);
            }
        }

        /// <summary>
        /// Method to return the city transfer time for the specified key
        /// Returns null if not found
        /// </summary>
        public TransferTimeValue GetTransferTime(TransferTimeKey key)
        {
            CheckForResetData();

            if (dictTransferTimes.ContainsKey(key))
            {
                return dictTransferTimes[key];
            }
            
            return null;
        }

        /// <summary>
        /// Method to add a Transfer time to the data dictionary
        /// </summary>
        public void AddTransferTime(TransferTimeKey key, TransferTimeValue value)
        {
            // Remove from dictionary
            if (dictTransferTimes.ContainsKey(key))
            {
                dictTransferTimes.Remove(key);
            }

            // Add to dictionary
            dictTransferTimes.Add(key, value);
        }

        /// <summary>
        /// Method to return the cached International Journeys for the specified 
        /// origin and destination stop naptans, and mode.
        /// Returns null if no journeys are found
        /// </summary>
        public InternationalJourney[] GetInternationalJourneys(string originCityId, string destinationCityId, string originStopNaptan, string destinationStopNaptan, InternationalModeType modeType)
        {
            CheckForResetData();

            if (!string.IsNullOrEmpty(originCityId) && !string.IsNullOrEmpty(destinationCityId)
                && !string.IsNullOrEmpty(originStopNaptan) && !string.IsNullOrEmpty(destinationStopNaptan))
            {
                InternationalJourneyKey key = new InternationalJourneyKey(originCityId, destinationCityId, originStopNaptan, destinationStopNaptan);

                List<InternationalJourney> ijs = new List<InternationalJourney>();

                switch (modeType)
                {
                    case InternationalModeType.Air:
                        if (dictInternationalJourneysAir.ContainsKey(key))
                        {
                            foreach (InternationalJourney ij in dictInternationalJourneysAir[key])
                            {
                                ijs.Add(ij.DeepClone());
                            }

                            return ijs.ToArray();
                        }
                        break;

                    case InternationalModeType.Coach:
                        if (dictInternationalJourneysCoach.ContainsKey(key))
                        {
                            foreach (InternationalJourney ij in dictInternationalJourneysCoach[key])
                            {
                                ijs.Add(ij.DeepClone());
                            }

                            return ijs.ToArray();
                        }
                        break;

                    case InternationalModeType.Rail:
                        if (dictInternationalJourneysRail.ContainsKey(key))
                        {
                            foreach (InternationalJourney ij in dictInternationalJourneysRail[key])
                            {
                                ijs.Add(ij.DeepClone());
                            }

                            return ijs.ToArray();
                        }
                        break;

                    case InternationalModeType.Car:
                        if (dictInternationalJourneysCar.ContainsKey(key))
                        {
                            foreach (InternationalJourney ij in dictInternationalJourneysCar[key])
                            {
                                ijs.Add(ij.DeepClone());
                            }

                            return ijs.ToArray();
                        }
                        break;

                }
            }

            // No journeys found or invalid stop naptans
            return null;
        }

        /// <summary>
        /// Method to add International Journeys to the dictionary data caches, if an origin and destination
        /// stop naptan are supplied
        /// </summary>
        public void AddInternationalJourneys(string originCityId, string destinationCityId, string originStopNaptan, string destinationStopNaptan, InternationalModeType modeType, InternationalJourney[] internationalJourneys)
        {
            if (!string.IsNullOrEmpty(originCityId) && !string.IsNullOrEmpty(destinationCityId) 
                && !string.IsNullOrEmpty(originStopNaptan) && !string.IsNullOrEmpty(destinationStopNaptan))
            {
                InternationalJourneyKey key = new InternationalJourneyKey(originCityId, destinationCityId, originStopNaptan, destinationStopNaptan);

                switch (modeType)
                {
                    case InternationalModeType.Air:
                        if (dictInternationalJourneysAir.ContainsKey(key))
                        {
                            // Remove existing set of journeys
                            dictInternationalJourneysAir.Remove(key);
                        }

                        // Add new set of journeys
                        dictInternationalJourneysAir.Add(key, internationalJourneys);
                        break;

                    case InternationalModeType.Coach:
                        if (dictInternationalJourneysCoach.ContainsKey(key))
                        {
                            // Remove existing set of journeys
                            dictInternationalJourneysCoach.Remove(key);
                        }

                        // Add new set of journeys
                        dictInternationalJourneysCoach.Add(key, internationalJourneys);
                        break;

                    case InternationalModeType.Rail:
                        if (dictInternationalJourneysRail.ContainsKey(key))
                        {
                            // Remove existing set of journeys
                            dictInternationalJourneysRail.Remove(key);
                        }

                        // Add new set of journeys
                        dictInternationalJourneysRail.Add(key, internationalJourneys);
                        break;

                    case InternationalModeType.Car:
                        if (dictInternationalJourneysCar.ContainsKey(key))
                        {
                            // Remove existing set of journeys
                            dictInternationalJourneysCar.Remove(key);
                        }

                        // Add new set of journeys
                        dictInternationalJourneysCar.Add(key, internationalJourneys);
                        break;
                }
            }
        }

        /// <summary>
        /// Returns if the stop distances data has been set up
        /// </summary>
        /// <returns></returns>
        public bool IsStopDistancesDataSet()
        {
            CheckForResetData();

            return dictStopDistancesSetup;
        }

        /// <summary>
        /// Method to return the distance (in metres) between the origin and desitnation stop naptan
        /// </summary>
        public int GetStopDistance(string originStopNaptan, string destinationStopNaptan)
        {
            if (!string.IsNullOrEmpty(originStopNaptan) && !string.IsNullOrEmpty(destinationStopNaptan))
            {
                originStopNaptan = originStopNaptan.Trim().ToUpper();
                destinationStopNaptan = destinationStopNaptan.Trim().ToUpper();

                StopStopKey key = new StopStopKey(originStopNaptan, destinationStopNaptan);

                if (dictStopDistances.ContainsKey(key))
                {
                    return dictStopDistances[key];
                }
            }

            return 0;
        }

        /// <summary>
        /// Method to add the distance (in metres) between a origin and destination stop naptan
        /// </summary>
        public void AddStopDistance(string originStopNaptan, string destinationStopNaptan, int distanceMetres)
        {
            if (!string.IsNullOrEmpty(originStopNaptan) && !string.IsNullOrEmpty(destinationStopNaptan))
            {
                originStopNaptan = originStopNaptan.Trim().ToUpper();
                destinationStopNaptan = destinationStopNaptan.Trim().ToUpper();

                StopStopKey key = new StopStopKey(originStopNaptan, destinationStopNaptan);

                // Remove from dictionary
                if (dictStopDistances.ContainsKey(key))
                {
                    dictStopDistances.Remove(key);
                }

                // Add to dictionary
                dictStopDistances.Add(key, distanceMetres);

                // Flag to indicate values have been added to let caller know data was setup
                dictStopDistancesSetup = true;
            }
        }

        /// <summary>
        /// Returns if the permitted country data has been set up
        /// </summary>
        /// <returns></returns>
        public bool IsPermittedCountriesDataSet()
        {
            CheckForResetData();

            return dictPermittedCountrySetup;
        }

        /// <summary>
        /// Method to return if the origin and desitnation country codes are permitted to plan an international journey
        /// </summary>
        public bool GetPermittedCountry(string originCountryCode, string destinationCountryCode)
        {
            if (!string.IsNullOrEmpty(originCountryCode) && !string.IsNullOrEmpty(destinationCountryCode))
            {
                originCountryCode = originCountryCode.Trim().ToUpper();
                destinationCountryCode = destinationCountryCode.Trim().ToUpper();

                PermittedCountryKey key = new PermittedCountryKey(originCountryCode, destinationCountryCode);

                if (dictPermittedCountry.ContainsKey(key))
                {
                    return dictPermittedCountry[key];
                }
            }
            
            return false;
        }

        /// <summary>
        /// Method to add the origin and destination country code is permitted to the data dictionary
        /// </summary>
        public void AddPermittedCountry(string originCountryCode, string destinationCountryCode, bool isPermitted)
        {
            if (!string.IsNullOrEmpty(originCountryCode) && !string.IsNullOrEmpty(destinationCountryCode))
            {
                originCountryCode = originCountryCode.Trim().ToUpper();
                destinationCountryCode = destinationCountryCode.Trim().ToUpper();

                PermittedCountryKey key = new PermittedCountryKey(originCountryCode, destinationCountryCode);
                
                // Remove from dictionary
                if (dictPermittedCountry.ContainsKey(key))
                {
                    dictPermittedCountry.Remove(key);
                }

                // Add to dictionary
                dictPermittedCountry.Add(key, isPermitted);

                // Flag to indicate values have been added to let caller know data was setup
                dictPermittedCountrySetup = true;
            }
        }
        
        /// <summary>
        /// Returns if the city route matrix data has been set up
        /// </summary>
        /// <returns></returns>
        public bool IsInternationalCityRouteMatrixDataSet()
        {
            CheckForResetData();

            return dictCityRouteMatrixSetup;
        }

        /// <summary>
        /// Returns an array of cities which can be travelled to for the specified City Id
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public InternationalCity[] GetInternationalCityRouteMatrix(string cityId)
        {
            CheckForResetData();

            if ((!string.IsNullOrEmpty(cityId)) && dictCityRouteMatrix.ContainsKey(cityId))
            {
                return dictCityRouteMatrix[cityId];
            }
            else
            {
                return new InternationalCity[0];
            }
        }

        /// <summary>
        /// Method to add the International City route matrix data
        /// </summary>
        public void AddInternationalCityRouteMatrix(string cityId, InternationalCity[] validCities)
        {
            if (!string.IsNullOrEmpty(cityId) && (validCities != null))
            {
                // Add to dictionary
                dictCityRouteMatrix[cityId] = validCities;
                
                // Flag to indicate values have been added to let caller know data was setup
                dictCityRouteMatrixSetup = true;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns true if a JavaScript declarations file has been generated for the current
        /// data set.
        /// </summary>
        public bool ScriptGenerated
        {
            get { return scriptGenerated; }
        }

        #endregion
    }

    #region Public class and struct to access data

        /// <summary>
        /// Struct to define an Interchange Time key
        /// </summary>
        public struct InterchangeTimeKey
        {
            public string naptan;
            public string operatorCode;

            public InterchangeTimeKey(string naptan, string operatorCode)
            {
                this.naptan = naptan;
                this.operatorCode = operatorCode;
            }
        }

        /// <summary>
        /// Class to define an Interchange Time value
        /// </summary>
        public class InterchangeTimeValue
        {
            public string naptan;
            public TimeSpan timeSpan;

            public InterchangeTimeValue(string naptan, TimeSpan timeSpan)
            {
                this.naptan = naptan;
                this.timeSpan = timeSpan;
            }
        }

        /// <summary>
        /// Struct to define a Transfer Time key
        /// </summary>
        public struct TransferTimeKey
        {
            public string cityID;
            public string stopNaptan;

            public TransferTimeKey(string cityID, string stopNaptan)
            {
                this.cityID = cityID;
                this.stopNaptan = stopNaptan;
            }
        }

        /// <summary>
        /// Class to define a Transfer Time value
        /// </summary>
        public class TransferTimeValue
        {
            public int transferTimeMinutes;
            public string transferText;

            public TransferTimeValue(int transferTimeMinutes, string transferText)
            {
                this.transferTimeMinutes = transferTimeMinutes;
                this.transferText = transferText;
            }
        }

        /// <summary>
        /// Struct to define an International Journey key
        /// </summary>
        public struct InternationalJourneyKey
        {
            public string departureCityId;
            public string arrivalCityId;
            public string departureStopNaptan;
            public string arrivalStopNaptan;

            public InternationalJourneyKey(string departureCityId, string arrivalCityId, string departureStopNaptan, string arrivalStopNaptan)
            {
                this.departureCityId = departureCityId;
                this.arrivalCityId = arrivalCityId;
                this.departureStopNaptan = departureStopNaptan;
                this.arrivalStopNaptan = arrivalStopNaptan;
            }
        }

        /// <summary>
        /// Struct to define a country to country key
        /// </summary>
        public struct PermittedCountryKey
        {
            public string originCountryCode;
            public string destinationCountryCode;

            public PermittedCountryKey(string originCountryCode, string destinationCountryCode)
            {
                this.originCountryCode = originCountryCode;
                this.destinationCountryCode = destinationCountryCode;
            }
        }

        /// <summary>
        /// Struct to define a stop to stop key
        /// </summary>
        public struct StopStopKey
        {
            public string originStopNaptan;
            public string destinationStopNaptan;

            public StopStopKey(string originStopNaptan, string destinationStopNaptan)
            {
                this.originStopNaptan = originStopNaptan;
                this.destinationStopNaptan = destinationStopNaptan;
            }
        }


    #endregion
}
