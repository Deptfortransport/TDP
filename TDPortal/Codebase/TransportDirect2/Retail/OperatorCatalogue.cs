// *********************************************** 
// NAME			: OperatorCatalogue.cs
// AUTHOR		: David Lane
// DATE CREATED	: 06/08/2013
// DESCRIPTION	: OperatorCatalogue class which wraps access to service operator 
//                  information. Currently only accessible operators
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TDP.Common;
using TDP.Common.Extenders;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.EventLogging;
using Logger = System.Diagnostics.Trace;

namespace TDP.UserPortal.Retail
{
    /// <summary>
    /// OperatorCatalogue class which wraps access to service operator information
    /// </summary>
    public class OperatorCatalogue
    {
        #region Private Members

        private enum FieldIndices { MODE, OPERATOR, OPERATOR_NAME, URL };
        private const string CONST_DEFAULT = "DE";
        private const string CONST_NOLINK = "NOLINK";

        /// <summary>
        /// Structure used to define a key for accessing data cached
        /// </summary>
        private struct RegionModeKey
        {
            public string regionId, mode;

            public RegionModeKey(string regionId, string mode)
            {
                this.regionId = regionId;
                this.mode = mode;
            }
        }

        /// <summary>
        /// Structure used to define a key for accessing data cached
        /// </summary>
        private struct OperatorKey
        {
            public string operatorCode, serviceNumber;

            public OperatorKey(string operatorCode, string serviceNumber)
            {
                this.operatorCode = operatorCode;
                this.serviceNumber = serviceNumber;
            }
        }

        /// <summary>
        /// Dictionary for storing operators
        /// </summary>
        private Dictionary<OperatorKey, ServiceOperator> operatorsCache = new Dictionary<OperatorKey, ServiceOperator>();

        /// <summary>
        /// Dictionary for storing accessible operators
        /// </summary>
        private Dictionary<RegionModeKey, Dictionary<OperatorKey, List<ServiceOperator>>> operatorsAccessibleCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey, List<ServiceOperator>>>();
        
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public OperatorCatalogue()
        {
            // Load the operator info
            Load();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns operator details from the operators cache
        /// If no match is found then null is returned.
        /// </summary>
        public ServiceOperator GetOperator(string operatorCode)
        {
            // Return value
            ServiceOperator serviceOperator = null;

            if (!string.IsNullOrEmpty(operatorCode))
            {
                // Create the key in the right format
                OperatorKey keyToMatch1 = new OperatorKey(operatorCode.Trim().ToUpper(), string.Empty);

                // Look up the cached operator object for the given key
                if (operatorsCache.ContainsKey(keyToMatch1))
                    serviceOperator = operatorsCache[keyToMatch1];
            }

            return serviceOperator;
        }

        /// <summary>
        /// Returns Operator details from the accessible operator bookings data.
        /// If no match is found then null is returned.
        /// </summary>
        public ServiceOperator GetAccessibleOperator(string operatorCode, string serviceNumber,
            string region, TDPModeType mode, DateTime dateTime)
        {
            // Return value
            ServiceOperator serviceOperator = null;

            // Found values
            Dictionary<OperatorKey, List<ServiceOperator>> serviceOperators = null;
            List<ServiceOperator> serviceOperatorsList = null;

            // Ignore operator code case
            if (!string.IsNullOrEmpty(operatorCode))
                operatorCode = operatorCode.ToUpper();

            // Create the key in the right format
            RegionModeKey rmKey = new RegionModeKey(region, mode.ToString());
            OperatorKey opKey = new OperatorKey(operatorCode, serviceNumber);

            // Look up the cached operator object for the given key
            if (operatorsAccessibleCache.ContainsKey(rmKey))
            {
                serviceOperators = operatorsAccessibleCache[rmKey];

                if (serviceOperators.ContainsKey(opKey))
                {
                    serviceOperatorsList = serviceOperators[opKey];
                }
                else
                {
                    // Exact match not found - try with no service number
                    opKey = new OperatorKey(operatorCode, string.Empty);

                    if (serviceOperators.ContainsKey(opKey))
                        serviceOperatorsList = serviceOperators[opKey];
                }
            }

            // Check if operator is valid for the date
            if (serviceOperatorsList != null && serviceOperatorsList.Count > 0)
            {
                List<ServiceOperator> filteredOperators = serviceOperatorsList.FindAll(
                        delegate(ServiceOperator op)
                        {
                            return op.WEFDate <= dateTime && dateTime <= op.WEUDate;
                        });

                if (filteredOperators != null && filteredOperators.Count > 0)
                {
                    // Assume there will only be one matching for the datetime
                    serviceOperator = filteredOperators[0];
                }
            }

            return serviceOperator;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads all the available operator links data from the database into a hashtable
        /// </summary>
        private void Load()
        {
            using (SqlHelper helper = new SqlHelper())
            {
                SqlDataReader reader;

                // Declare database to open
                SqlHelperDatabase sourceDB = SqlHelperDatabase.TransientPortalDB;

                // Load all operator links
                try
                {
                    // Open connection
                    helper.ConnOpen(sourceDB);
                    int operatorsCount = 0;

                    #region Load Operator Links

                    // Get Operator data
                    if (TDPTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business,
                            TDPTraceLevel.Verbose, "Loading Operator data started"));
                    }

                    // Temp data cache
                    Dictionary<OperatorKey, ServiceOperator> tmpOperatorsCache = new Dictionary<OperatorKey, ServiceOperator>();

                    // Get Operator data
                    reader = helper.GetReader("GetOperatorLinks");

                    // Set up and assign the variables for each row
                    ServiceOperator serviceOperator = null;
                    OperatorKey operatorKey;

                    string opCode = string.Empty;
                    string opName = string.Empty;
                    string opMode = string.Empty;
                    string opURL = string.Empty;

                    TDPModeType opModeType = TDPModeType.Unknown;

                    // Set up column ordinals
                    int columnOperatorCode = reader.GetOrdinal("OperatorCode");
                    int columnOperatorName = reader.GetOrdinal("OperatorName");
                    int columnOperatorMode = reader.GetOrdinal("ModeId");
                    int columnOperatorURL = reader.GetOrdinal("URL");

                    // Read operator values and create operator key for caching
                    while (reader.Read())
                    {
                        opCode = reader.IsDBNull(columnOperatorCode) ? string.Empty : reader.GetString(columnOperatorCode);
                        opName = reader.IsDBNull(columnOperatorName) ? string.Empty : reader.GetString(columnOperatorName);
                        opMode = reader.IsDBNull(columnOperatorMode) ? string.Empty : reader.GetString(columnOperatorMode);
                        opURL = reader.IsDBNull(columnOperatorURL) ? string.Empty : reader.GetString(columnOperatorURL);
                                                
                        // Parse into type
                        if (!string.IsNullOrEmpty(opMode))
                            opModeType = opMode.Parse(TDPModeType.Unknown);
                        else
                            opModeType = TDPModeType.Unknown;
                        
                        // Create an Operator object
                        serviceOperator = new ServiceOperator(opModeType, opCode, opName, opURL);
                        
                        // Create a unique key
                        operatorKey = new OperatorKey(opCode.Trim().ToUpper(), string.Empty);

                        // Add the key and operator object as a key-value pair in a local hashtable
                        if (!tmpOperatorsCache.ContainsKey(operatorKey))
                            tmpOperatorsCache.Add(operatorKey, serviceOperator);

                        operatorsCount++;
                    }

                    reader.Close();

                    Dictionary<OperatorKey, ServiceOperator> oldOperatorsCache = new Dictionary<OperatorKey, ServiceOperator>();

                    // Create reference to the existing dictionary prior to replacing it.
                    // This is for multi-threading purposes to ensure there is always data to reference
                    if (operatorsCache != null)
                    {
                        oldOperatorsCache = operatorsCache;
                    }

                    // Replace the current
                    operatorsCache = tmpOperatorsCache;

                    // Clear the old
                    oldOperatorsCache.Clear();
                    
                    // Log that data has been loaded
                    if (TDPTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business,
                            TDPTraceLevel.Verbose, string.Format("Loading Operator data completed, count[{0}]", operatorsCount)));
                    }

                    #endregion

                    #region Load Accessible Operator Links

                    // Get Accessible Operator data
                    if (TDPTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business,
                            TDPTraceLevel.Verbose, "Loading Accessible Operator data started"));
                    }

                    // Temp data cache
                    Dictionary<RegionModeKey, Dictionary<OperatorKey, List<ServiceOperator>>> tmpOperatorsAccessibleCache = new Dictionary<RegionModeKey,Dictionary<OperatorKey,List<ServiceOperator>>>();
                    operatorsCount = 0;

                    // Get Accessible Operator data
                    reader = helper.GetReader("GetAccessibleOperatorLinks");

                    // Set up and assign the variables for each row
                    ServiceOperator accOperator = null;
                    string accOperatorCode = string.Empty;
                    string accServiceNumber = string.Empty;
                    string accMode = string.Empty;
                    TDPModeType accModeType = TDPModeType.Air;
                    string accRegion = string.Empty;
                    DateTime accWEFDate = DateTime.MinValue;
                    DateTime accWEUDate = DateTime.MaxValue;
                    bool accWheelchairBooking = false;
                    bool accAssistanceBooking = false;
                    string accBookingUrl = string.Empty;
                    string accBookingNumber = string.Empty;

                    // Set up column ordinals
                    int accColumnOperatorCode = reader.GetOrdinal("OperatorCode");
                    int accColumnServiceNumber = reader.GetOrdinal("ServiceNumber");
                    int accColumnMode = reader.GetOrdinal("Mode");
                    int accColumnRegion = reader.GetOrdinal("Region");
                    int accColumnWEFDate = reader.GetOrdinal("WEFDate");
                    int accColumnWEUDate = reader.GetOrdinal("WEUDate");
                    int accColumnWheelchairBooking = reader.GetOrdinal("WheelchairBooking");
                    int accColumnAssistanceBooking = reader.GetOrdinal("AssistanceBooking");
                    int accColumnBookingUrl = reader.GetOrdinal("BookingUrl");
                    int accColumnBookingNumber = reader.GetOrdinal("BookingNumber");

                    // Read operator values and create operator key for caching
                    while (reader.Read())
                    {
                        accOperatorCode = reader.IsDBNull(accColumnOperatorCode) ? string.Empty : reader.GetString(accColumnOperatorCode);
                        accServiceNumber = reader.IsDBNull(accColumnServiceNumber) ? string.Empty : reader.GetString(accColumnServiceNumber);
                        accMode = reader.IsDBNull(accColumnMode) ? string.Empty : reader.GetString(accColumnMode);
                        accRegion = reader.IsDBNull(accColumnRegion) ? string.Empty : reader.GetString(accColumnRegion);
                        accWEFDate = reader.IsDBNull(accColumnWEFDate) ? DateTime.MinValue : reader.GetDateTime(accColumnWEFDate);
                        accWEUDate = reader.IsDBNull(accColumnWEUDate) ? DateTime.MinValue : reader.GetDateTime(accColumnWEUDate);
                        accWheelchairBooking = reader.GetBoolean(accColumnWheelchairBooking);
                        accAssistanceBooking = reader.GetBoolean(accColumnAssistanceBooking);
                        accBookingUrl = reader.IsDBNull(accColumnBookingUrl) ? string.Empty : reader.GetString(accColumnBookingUrl);
                        accBookingNumber = reader.IsDBNull(accColumnBookingNumber) ? string.Empty : reader.GetString(accColumnBookingNumber);

                        if (accMode == CONST_DEFAULT)
                            accMode = string.Empty;

                        if (!string.IsNullOrEmpty(accMode))
                            accModeType = (TDPModeType)Enum.Parse(typeof(TDPModeType), accMode, true);
                        else
                            accModeType = TDPModeType.Air;

                        // Ignore operator code case
                        if (!string.IsNullOrEmpty(accOperatorCode))
                            accOperatorCode = accOperatorCode.ToUpper();

                        // Update url if its a "no link" placeholder text 
                        if (accBookingUrl.Equals(CONST_NOLINK, StringComparison.InvariantCultureIgnoreCase))
                            accBookingUrl = string.Empty;

                        // Region could be a list, so split and create a ServiceOperator for each
                        List<string> regions = new List<string>(accRegion.Split('|'));

                        foreach (string region in regions)
                        {
                            // Create an Operator object
                            accOperator = new ServiceOperator(accModeType, accOperatorCode, accServiceNumber, string.Empty,
                                region, accWEFDate, accWEUDate, string.Empty, accWheelchairBooking, accAssistanceBooking,
                                accBookingUrl, accBookingNumber);

                            // Create a unique key
                            RegionModeKey rmKey = new RegionModeKey(region, accMode);
                            OperatorKey opKey = new OperatorKey(accOperatorCode, accServiceNumber);

                            // Add
                            if (!tmpOperatorsAccessibleCache.ContainsKey(rmKey))
                                tmpOperatorsAccessibleCache.Add(rmKey, new Dictionary<OperatorKey, List<ServiceOperator>>());

                            if (!tmpOperatorsAccessibleCache[rmKey].ContainsKey(opKey))
                                tmpOperatorsAccessibleCache[rmKey].Add(opKey, new List<ServiceOperator>());

                            tmpOperatorsAccessibleCache[rmKey][opKey].Add(accOperator);

                            operatorsCount++;
                        }
                    }

                    reader.Close();

                    Dictionary<RegionModeKey, Dictionary<OperatorKey, List<ServiceOperator>>> oldOperatorsAccessibleCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey, List<ServiceOperator>>>();

                    // Create reference to the existing dictionary prior to replacing it.
                    // This is for multi-threading purposes to ensure there is always data to reference
                    if (operatorsAccessibleCache != null)
                    {
                        oldOperatorsAccessibleCache = operatorsAccessibleCache;
                    }

                    // Replace the current
                    operatorsAccessibleCache = tmpOperatorsAccessibleCache;

                    // Clear the old
                    oldOperatorsAccessibleCache.Clear();

                    // Log that data has been loaded
                    if (TDPTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business,
                            TDPTraceLevel.Verbose, string.Format("Loading Accessible Operator data completed, count[{0}]", operatorsCount)));
                    }

                    #endregion
                }

                // As there is no serious drawback to this data being missing, just log that there was an execption.
                catch (SqlException sqle)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, "An SQL exception occurred whilst attempting to load the Operator Catalogue data.", sqle));
                }
                catch (Exception ex)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Database, TDPTraceLevel.Error, "An exception occurred whilst attempting to load the Operator Catalogue data.", ex));
                }
                finally
                {
                    if (helper.ConnIsOpen)
                        helper.ConnClose();
                }
            }
        }

        #endregion
    }
}
