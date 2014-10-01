// *********************************************** 
// NAME			: OperatorCatalogue.cs
// AUTHOR		: Paul Cross
// DATE CREATED	: 15/07/2005
// DESCRIPTION	: Implementation for the OperatorCatalogue class
//				  which wraps access to service operator information.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/OperatorCatalogue.cs-arc  $
//
//   Rev 1.5   Mar 21 2013 15:56:26   rbroddle
//Amended to translate operators for MDV region prefixes
//
//   Rev 1.4   Jan 29 2013 13:20:10   mmodi
//Ignore case for accessible opertators match
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.3   Dec 10 2012 12:13:18   mmodi
//Updated accessible operator to include WEF and WEU datetimes
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Dec 05 2012 14:14:50   mmodi
//Updated for accessible operators
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Mar 20 2008 10:11:46   mturner
//Del10 patch1 from Dev factory
//
//   Rev 1.0   Nov 08 2007 12:23:54   mturner
//Initial revision.
//
//   Rev 1.4   Apr 03 2007 10:17:24   dsawe
//updated for local zonal services phase 2 & 3
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.3   Mar 16 2007 10:00:56   build
//Automatically merged from branch for stream4362
//
//   Rev 1.2.1.1   Mar 14 2007 18:42:58   dsawe
//returning  string.empty instead of null
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.2.1.0   Mar 12 2007 16:02:02   dsawe
//added code for operator links
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.2   Jul 25 2005 21:00:14   pcross
//FxCop updates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 19 2005 10:47:50   pcross
//Minor updates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 18 2005 16:16:42   pcross
//Initial revision.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// OperatorCatalogue class which wraps access to service operator information
    /// </summary>
	public class OperatorCatalogue : IOperatorCatalogue
	{
	
		#region Private Members
		
		private enum FieldIndices {MODE, OPERATOR, OPERATOR_NAME, URL};
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
		/// Structure used to define hashkey for accessing data cached in operators hashtable
		/// </summary>
		private struct OperatorKey
		{
            public string mode, operatorCode, regionId, serviceNumber;

			public OperatorKey(string mode, string operatorCode, string regionId, string serviceNumber)
			{
				this.mode = mode;
				this.operatorCode = operatorCode;
                this.regionId = regionId;
                this.serviceNumber = serviceNumber;
			}
		}

        /// <summary>
        /// Structure used to define a key for accessing data cached
        /// </summary>
        private struct OperatorKey2
        {
            public string operatorCode, serviceNumber;

            public OperatorKey2(string operatorCode, string serviceNumber)
            {
                this.operatorCode = operatorCode;
                this.serviceNumber = serviceNumber;
            }
        }

		/// <summary> Hashtable for storing operators </summary>
		private Hashtable operatorsCache = new Hashtable();

        /// <summary>
        /// Dictionary for storing accessible operators
        /// </summary>
        private Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>> operatorsAccessibleCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>>();

        /// <summary>
		/// For storing zonal operator links from ZonalOperatorLinks tables
		/// </summary>
        private Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>> zonalOperatorLinksCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>>();
        private Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>> zonalOperatorFaresLinksCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>>();
		        
        /// <summary>
        /// for storing operator code translations from ServiceDetailRequestOperatorCodes Table in Transient Portal
        /// </summary>
        private Hashtable hashServiceDetailRequestOperatorCodes = new Hashtable();

		#endregion

		#region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
		public OperatorCatalogue()
		{
			// Call the Load method to load the operator info into a hashtable object
			Load();
		}

		#endregion

		#region IOperatorCatalogue interface methods

        /// <summary>
        /// Given an operator checks the hashServiceDetailRequestOperatorCodes hashtable.
        /// If no match is found then operatorCode passed in is returned unaltered.
        /// If a match is found then translated operatorCode value is returned.
        /// </summary>
        /// <param name="operatorCode"></param>
        /// <returns></returns>
        public String TranslateOperator(string operatorCode)
        {
            string operatorTranslated = operatorCode;

            // Look up the cached operator object for the given key
            if ((hashServiceDetailRequestOperatorCodes.ContainsKey(operatorCode)) && (bool.Parse(Properties.Current["StopInformation.ShowServices.FilterOperatorCodes"])))
                {
                operatorTranslated = (string)hashServiceDetailRequestOperatorCodes[operatorCode];
                }
            return operatorTranslated;
        }

		/// <summary>
		/// Given an operator and transport mode, gets the matching URL for operators web page (if exists).
		/// If no match is found then null is returned.
		/// Additionally, a match may be found but there may be no URL or operator name present.
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		public ServiceOperator GetOperator(string operatorCode, ModeType mode)
		{
			bool noOperatorMatch = false;

			// Get the matching url from the hashtable
			ServiceOperator operatorInfo = new ServiceOperator();

			// Create the key in the right format
            OperatorKey keyToMatch1 = new OperatorKey(mode.ToString(), operatorCode, string.Empty, string.Empty);
			
			// Look up the cached operator object for the given key
			if (operatorsCache.ContainsKey(keyToMatch1))
				operatorInfo = (ServiceOperator)operatorsCache[keyToMatch1];
			else
			{
				// Exact match not found - try with blank mode
                OperatorKey keyToMatch2 = new OperatorKey(string.Empty, operatorCode, string.Empty, string.Empty);
				if (operatorsCache.ContainsKey(keyToMatch2))
					operatorInfo = (ServiceOperator)operatorsCache[keyToMatch2];
				else
					noOperatorMatch = true;
			}

			if (noOperatorMatch)
				return null;
			else
			{
				// (returns empty url if no match or if there's a match but no available URL for that mode & operator)
				if (String.Compare(operatorInfo.Url, CONST_NOLINK, true, CultureInfo.InvariantCulture) == 0)
					operatorInfo.Url = string.Empty;

				return operatorInfo;
			}
		}

        /// <summary>
        /// Returns Operator details from the accessible operator bookings data.
        /// If no match is found then null is returned.
        /// </summary>
        public ServiceOperator GetAccessibleOperator(string operatorCode, string serviceNumber,
            string region, ModeType mode, TDDateTime dateTime)
        {
            // Return value
            ServiceOperator serviceOperator = null;

            // Found values
            Dictionary<OperatorKey2, List<ServiceOperator>> serviceOperators = null;
            List<ServiceOperator> serviceOperatorsList = null;

            // Ignore operator code case
            if (!string.IsNullOrEmpty(operatorCode))
                operatorCode = operatorCode.ToUpper();

            // Create the key in the right format
            RegionModeKey rmKey = new RegionModeKey(region, mode.ToString());
            OperatorKey2 opKey = new OperatorKey2(operatorCode, serviceNumber);
            
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
                    opKey = new OperatorKey2(operatorCode, string.Empty);

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

        /// <summary>
        /// Returns Zonal Operator details from the zonal operators data.
        /// If no match is found then null is returned.
        /// </summary>
		public ServiceOperator GetZonalOperatorLinks(string modeId, string operatorCode, string regionId)
		{
            // Return value
			ServiceOperator zonalOperatorInfo = null;

            // Found values
            Dictionary<OperatorKey2, List<ServiceOperator>> serviceOperators = null;
            List<ServiceOperator> serviceOperatorsList = null;

            // Create the key in the right format
            RegionModeKey rmKey = new RegionModeKey(regionId.ToLower(), modeId);
            OperatorKey2 opKey = new OperatorKey2(operatorCode, string.Empty);


			// Look up the cached operator object for the given key
            if (zonalOperatorLinksCache.ContainsKey(rmKey))
            {
                serviceOperators = zonalOperatorLinksCache[rmKey];

                if (serviceOperators.ContainsKey(opKey))
                {
                    serviceOperatorsList = serviceOperators[opKey];
                }
            }

            // Assume there will only be one matching
            if (serviceOperatorsList != null && serviceOperatorsList.Count > 0)
            {
                zonalOperatorInfo = serviceOperatorsList[0];
            }
                        
            return zonalOperatorInfo;
		}

        /// <summary>
        /// Returns Zonal Fares Operator url from the zonal fares operator data.
        /// If no match is found then string empty is returned.
        /// </summary>
		public string GetZonalOperatorFaresLinks(string modeId, string operatorCode, string regionId)
		{
            // Found values
            Dictionary<OperatorKey2, List<ServiceOperator>> serviceOperators = null;
            List<ServiceOperator> serviceOperatorsList = null;

            // Create the key in the right format
            RegionModeKey rmKey = new RegionModeKey(regionId.ToLower(), modeId);
            OperatorKey2 opKey = new OperatorKey2(operatorCode, string.Empty);

            // Look up the cached operator object for the given key
            if (zonalOperatorFaresLinksCache.ContainsKey(rmKey))
            {
                serviceOperators = zonalOperatorFaresLinksCache[rmKey];

                if (serviceOperators.ContainsKey(opKey))
                {
                    serviceOperatorsList = serviceOperators[opKey];
                }
            }

            // Assume there will only be one matching
            if (serviceOperatorsList != null && serviceOperatorsList.Count > 0)
            {
                ServiceOperator zonalOperatorFaresInfo = serviceOperatorsList[0];

                return zonalOperatorFaresInfo.Url;
            }
                        
            return string.Empty;
		}

		#endregion

		#region Public Methods
		
		/// <summary>
		/// Read only property. Implementation of IOperatorCatalogue Current method
		/// </summary>
		public static IOperatorCatalogue Current
		{
			get
			{
				return (OperatorCatalogue)TDServiceDiscovery.Current[ ServiceDiscoveryKey.OperatorsService ];				
			}
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
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Loading Operator data started"));
                    }

                    // Temp data cache
                    Hashtable localLinksCache = new Hashtable();

                    // Get Operator data
                    reader = helper.GetReader("GetOperatorLinks");

                    // Set up a new hashtable with a struct (of mode & operator) as a key and operator object as value
                    // This will be kept in cache for future reference
                    while (reader.Read())
                    {
                        // Set up and assign the variables for each row
                        string operatorCode = string.Empty;
                        string operatorName = string.Empty;
                        string mode = string.Empty;
                        string url = string.Empty;

                        // Mode is blank unless it has a non-default value in the database
                        if (reader.GetString(Convert.ToInt32(FieldIndices.MODE, CultureInfo.InvariantCulture)) != CONST_DEFAULT)
                        {
                            mode = reader.GetString(Convert.ToInt32(FieldIndices.MODE, CultureInfo.InvariantCulture));
                        }
                        operatorCode = reader.GetString(Convert.ToInt32(FieldIndices.OPERATOR, CultureInfo.InvariantCulture));
                        operatorName = reader.GetString(Convert.ToInt32(FieldIndices.OPERATOR_NAME, CultureInfo.InvariantCulture));
                        url = reader.GetString(Convert.ToInt32(FieldIndices.URL, CultureInfo.InvariantCulture));

                        // Create an Operator object
                        ServiceOperator operatorInfo = new ServiceOperator();
                        if (mode.Length == 0)
                            operatorInfo = new ServiceOperator(operatorCode, operatorName, url);
                        else
                            operatorInfo = new ServiceOperator((ModeType)Enum.Parse(typeof(ModeType), mode, true), operatorCode, operatorName, url);

                        // Create a unique key
                        OperatorKey hashTableKey = new OperatorKey(mode, operatorCode, string.Empty, string.Empty);

                        // Add the key and operator object as a key-value pair in a local hashtable
                        localLinksCache.Add(hashTableKey, operatorInfo);

                        operatorsCount++;
                    }

                    reader.Close();

                    Hashtable oldTable = new Hashtable();

                    //Create reference to the existing operatorsCache hashtable prior to replacing it				
                    //This is for multi-threading purposes to ensure there is always data to reference
                    if (operatorsCache != null)
                    {
                        oldTable = operatorsCache;
                    }

                    //replace the linksCache hashtable
                    operatorsCache = localLinksCache;

                    //clear the old data storage hashtable
                    oldTable.Clear();

                    // Log that data has been loaded
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, string.Format("Loading Operator data completed, count[{0}]", operatorsCount)));
                    }
                    #endregion

                    #region Load Accessible Operator Links

                    // Get Accessible Operator data
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Loading Accessible Operator data started"));
                    }

                    // Temp data cache
                    Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>> tmpOperatorsAccessibleCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>>();
                    operatorsCount = 0;

                    // Get Accessible Operator data
                    reader = helper.GetReader("GetAccessibleOperatorLinks");

                    // Set up and assign the variables for each row
                    ServiceOperator accOperator = null;
                    string accOperatorCode = string.Empty;
                    string accServiceNumber = string.Empty;
                    string accMode = string.Empty;
                    ModeType accModeType = ModeType.Air;
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
                            accModeType = (ModeType)Enum.Parse(typeof(ModeType), accMode, true);
                        else
                            accModeType = ModeType.Air;

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
                            OperatorKey2 opKey = new OperatorKey2(accOperatorCode, accServiceNumber);

                            // Add
                            if (!tmpOperatorsAccessibleCache.ContainsKey(rmKey))
                                tmpOperatorsAccessibleCache.Add(rmKey, new Dictionary<OperatorKey2, List<ServiceOperator>>());

                            if (!tmpOperatorsAccessibleCache[rmKey].ContainsKey(opKey))
                                tmpOperatorsAccessibleCache[rmKey].Add(opKey, new List<ServiceOperator>());

                            tmpOperatorsAccessibleCache[rmKey][opKey].Add(accOperator);

                            operatorsCount++;
                        }
                    }

                    reader.Close();

                    Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>> oldOperatorsAccessibleCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>>();

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
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, string.Format("Loading Accessible Operator data completed, count[{0}]", operatorsCount)));
                    }

                    #endregion

                    #region Load Zonal Operator Links

                    // Get Operator data
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Loading Zonal Operator links data started"));
                    }

                    // Temp data cache
                    Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>> tmpZonalOperatorsCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>>();
                    operatorsCount = 0;

                    // Get Zonal Operator data
                    reader = helper.GetReader("GetZonalOperatorLinks", new Hashtable(), new Hashtable());

                    ServiceOperator zonalOperator = null;

                    int RegionIdColumnOrdinal = reader.GetOrdinal("RegionId");
                    int ModeIdColumnOrdinal = reader.GetOrdinal("ModeId");
                    int OperatorCodeColumnOrdinal = reader.GetOrdinal("OperatorCode");
                    int URLColumnOrdinal = reader.GetOrdinal("URL");

                    while (reader.Read())
                    {
                        string regionId = reader.GetString(RegionIdColumnOrdinal);
                        string modeId = reader.GetString(ModeIdColumnOrdinal);
                        string operatorCode = reader.GetString(OperatorCodeColumnOrdinal);
                        string url = reader.GetString(URLColumnOrdinal);
                        ModeType modeType = ModeType.Air;

                        if (!string.IsNullOrEmpty(modeId))
                            modeType = (ModeType)Enum.Parse(typeof(ModeType), modeId, true);

                        // Region case insensitive
                        if (!string.IsNullOrEmpty(regionId))
                            regionId = regionId.ToLower();

                        // Update url if its a "no link" placeholder text 
                        if (url.Equals(CONST_NOLINK, StringComparison.InvariantCultureIgnoreCase))
                            url = string.Empty;

                        // Create an Operator object
                        zonalOperator = new ServiceOperator(modeType, operatorCode, regionId, url);

                        // Create a unique key
                        RegionModeKey rmKey = new RegionModeKey(regionId, modeId);
                        OperatorKey2 opKey = new OperatorKey2(operatorCode, string.Empty);

                        // Add
                        if (!tmpZonalOperatorsCache.ContainsKey(rmKey))
                            tmpZonalOperatorsCache.Add(rmKey, new Dictionary<OperatorKey2, List<ServiceOperator>>());

                        if (!tmpZonalOperatorsCache[rmKey].ContainsKey(opKey))
                            tmpZonalOperatorsCache[rmKey].Add(opKey, new List<ServiceOperator>());

                        tmpZonalOperatorsCache[rmKey][opKey].Add(zonalOperator);

                        operatorsCount++;
                    }

                    reader.Close();

                    Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>> oldZonalOperatorsCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>>();

                    // Create reference to the existing dictionary prior to replacing it.
                    // This is for multi-threading purposes to ensure there is always data to reference
                    if (zonalOperatorLinksCache != null)
                    {
                        oldZonalOperatorsCache = zonalOperatorLinksCache;
                    }

                    // Replace the current
                    zonalOperatorLinksCache = tmpZonalOperatorsCache;

                    // Clear the old
                    oldZonalOperatorsCache.Clear();

                    // Log that data has been loaded
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, string.Format("Loading Zonal Operator links data completed, count[{0}]", operatorsCount)));
                    }
                    #endregion

                    #region Load Zonal Operator Fares Links
                    // Get Operator Fares Links 
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Loading Zonal Operator Fares links data started"));
                    }

                    // Temp data cache
                    Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>> tmpZonalOperatorFaresCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>>();
                    operatorsCount = 0;

                    // Get Zonal Fares Operator data
                    reader = helper.GetReader("GetZonalOperatorFaresLinks", new Hashtable(), new Hashtable());

                    ServiceOperator zonalFareOperator = null;

                    int faresRegionIdColumnOrdinal = reader.GetOrdinal("RegionId");
                    int faresModeIdColumnOrdinal = reader.GetOrdinal("ModeId");
                    int faresOperatorCodeColumnOrdinal = reader.GetOrdinal("OperatorCode");
                    int faresURLColumnOrdinal = reader.GetOrdinal("URL");

                    while (reader.Read())
                    {
                        string faresRegionId = reader.GetString(faresRegionIdColumnOrdinal);
                        string faresModeId = reader.GetString(faresModeIdColumnOrdinal);
                        string faresOperatorCode = reader.GetString(faresOperatorCodeColumnOrdinal);
                        string faresURL = reader.GetString(faresURLColumnOrdinal);

                        ModeType modeType = ModeType.Air;

                        if (!string.IsNullOrEmpty(faresModeId))
                            modeType = (ModeType)Enum.Parse(typeof(ModeType), faresModeId, true);

                        // Region case insensitive
                        if (!string.IsNullOrEmpty(faresRegionId))
                            faresRegionId = faresRegionId.ToLower();

                        // Update url if its a "no link" placeholder text 
                        if (faresURL.Equals(CONST_NOLINK, StringComparison.InvariantCultureIgnoreCase))
                            faresURL = string.Empty;

                        // Create an Operator object
                        zonalFareOperator = new ServiceOperator(modeType, faresOperatorCode, faresRegionId, faresURL);

                        // Create a unique key
                        RegionModeKey rmKey = new RegionModeKey(faresRegionId, faresModeId);
                        OperatorKey2 opKey = new OperatorKey2(faresOperatorCode, string.Empty);

                        // Add
                        if (!tmpZonalOperatorFaresCache.ContainsKey(rmKey))
                            tmpZonalOperatorFaresCache.Add(rmKey, new Dictionary<OperatorKey2, List<ServiceOperator>>());

                        if (!tmpZonalOperatorFaresCache[rmKey].ContainsKey(opKey))
                            tmpZonalOperatorFaresCache[rmKey].Add(opKey, new List<ServiceOperator>());

                        tmpZonalOperatorFaresCache[rmKey][opKey].Add(zonalFareOperator);

                        operatorsCount++;
                    }

                    reader.Close();

                    Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>> oldZonalOperatorFaresCache = new Dictionary<RegionModeKey, Dictionary<OperatorKey2, List<ServiceOperator>>>();

                    // Create reference to the existing dictionary prior to replacing it.
                    // This is for multi-threading purposes to ensure there is always data to reference
                    if (zonalOperatorFaresLinksCache != null)
                    {
                        oldZonalOperatorFaresCache = zonalOperatorFaresLinksCache;
                    }

                    // Replace the current
                    zonalOperatorFaresLinksCache = tmpZonalOperatorFaresCache;

                    // Clear the old
                    oldZonalOperatorFaresCache.Clear();

                    // Log that data has been loaded
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, string.Format("Loading Zonal Operator Fares links data completed, count[{0}]", operatorsCount)));
                    }
                    #endregion

                    #region Load Translated MDV Operator Codes for stop event requests
                    // Loading Translated MDV Operator Codes for stop event requests - to remove regional
                    //prefixes from service info requests
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, "Loading Translated MDV Operator Codes for service info stop event requests started"));
                    }

                    operatorsCount = 0;

                    // Get data
                    reader = helper.GetReader("GetServiceDetailRequestOperatorCodes", new Hashtable(), new Hashtable());

                    int OriginalOperatorCodeColumnOrdinal = reader.GetOrdinal("OperatorCode");
                    int TranslatedOperatorCodeColumnOrdinal = reader.GetOrdinal("RequestOperatorCode");

                    while (reader.Read())
                    {
                        string OriginalOperatorCode = reader.GetString(OriginalOperatorCodeColumnOrdinal);
                        string TranslatedOperatorCode = reader.GetString(TranslatedOperatorCodeColumnOrdinal);

                        hashServiceDetailRequestOperatorCodes.Add(OriginalOperatorCode, TranslatedOperatorCode);

                        operatorsCount++;
                    }
                    reader.Close();

                    // Log that data has been loaded
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business,
                            TDTraceLevel.Verbose, string.Format("Loading Translated MDV Operator Codes for service info stop event requests completed, count[{0}]", operatorsCount)));
                    }
                    #endregion

                }
                // As there is no serious drawback to this data being missing, just log that there was an execption.
                catch (SqlException sqle)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, "An SQL exception occurred whilst attempting to load the Operator Catalogue data.", sqle));
                }
                catch (Exception ex)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, "An exception occurred whilst attempting to load the Operator Catalogue data.", ex));
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
