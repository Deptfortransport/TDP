// *********************************************** 
// NAME             : GisQuery.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Class which provides a very thin wrapper around the ArcIMS Query object provided by ESRI
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.PropertyManager;
using TDP.Common.EventLogging;
using TransportDirect.Presentation.InteractiveMapping;
using Logger = System.Diagnostics.Trace;
using TDP.Reporting.Events;

namespace TDP.Common.LocationService.GIS
{
    /// <summary>
    /// Class which provides a very thin wrapper around the ArcIMS Query object provided by ESRI
    /// </summary>
    public class GisQuery
    {
        #region Private members

        // flags to log GISQueryEvent
        private bool logFindNearestStops;
        private bool logFindNearestLocality;
        private bool logFindNearestITN;
        private bool logFindNearestITNs;
        private bool logFindNearestPointOnTOID;
        private bool logFindNearestCarParks;
        private bool logFindNearestAccessibleStops;
        private bool logFindNearestAccessibleLocalities;
        private bool logFindNearestStopsAndITNs;
        private bool logFindExchangePointsInRadius;
        private bool logFindStopsInRadius;
        private bool logFindStopsInGroupForStops;
        private bool logFindStopsInfoForStops;
        private bool logIsPointsInCycleDataArea;
        private bool logIsPointsInWalkDataArea;
        private bool logIsPointInAccessibleLocation;
        private bool logGetExchangeInfoForNaptan;
        private bool logGetNPTGInfoForNaPTAN;
        private bool logGetLocalityInfoForNatGazID;
        private bool logGetStreetsFromPostcode;
        private bool logGetDistancesForTOIDs;

        private Query query;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GisQuery()
        {
            // get ServiceName and ServerName properties from PropertyService
            string serviceName = Properties.Current["locationservice.servicename"];
            string serverName = Properties.Current["locationservice.servername"];

            if (string.IsNullOrEmpty(serviceName) || string.IsNullOrEmpty(serverName))
                throw new TDPException("Unable to access the GisQuery Properties", false, TDPExceptionIdentifier.LSGizQueryPropertyInvalid);
            
            query = new Query(serverName, serviceName);

            // Read property flags for logging
            LoadLoggingProperties();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Wrapper for the Gis Query FindStopsInfoForStops
        /// </summary>
        /// <param name="searchDistance">radius of circle to be searched</param>
        /// <param name="naptanIDs">naptan IDs</param>
        /// <returns>returns a QuerySchema containing stop info for the supplied naptans</returns>
        public QuerySchema FindStopsInfoForStops(string[] naptanIDs)
        {
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindStopsInfoForStops(naptanIDs);

            LogEvent(logFindStopsInfoForStops, GISQueryType.FindStopsInfoForStops, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the Gis Query FindNearestLocality
        /// </summary>
        /// <param name="x">easting</param>
        /// <param name="x">northing</param>
        /// <returns>returns the nearest locality as a string</returns>
        public string FindNearestLocality(double x, double y)
        {
            DateTime logStart = DateTime.Now;
            string result = query.FindNearestLocality(x, y);

            LogEvent(logFindNearestLocality, GISQueryType.FindNearestLocality, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the Gis Query FindNearestITN
        /// </summary>
        /// <param name="x">easting</param>
        /// <param name="y">northing</param>
        /// <param name="address">The address parameter can be zero length. 
        /// This method will search outwards from point x,y until one or more ITN features are found. 
        /// This initial search may find multiple ITN’s. Within these initial results, if no address has been supplied the nearest feature will be returned. 
        /// If address has been specified, then the nearest feature that matches will be returned.</param>
        /// <param name="ingoreMotorways">If ignoreMotorways is true, the search radius will be continually increased until a non-motorway ITN is found.</param>
        /// <returns>returns a QuerySchema representing the GIS query result</returns>
        public QuerySchema FindNearestITN(double x, double y, string address, bool ingoreMotorways)
        {
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindNearestITN(x, y, address, ingoreMotorways);

            LogEvent(logFindNearestITN, GISQueryType.FindNearestITN, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the Gis Query FindNearestITNs
        /// </summary>
        /// <param name="x">easting</param>
        /// <param name="y">northing</param>
        /// <returns>returns a QuerySchema representing the GIS query result</returns>
        public QuerySchema FindNearestITNs(double x, double y)
        {
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindNearestITNs(x, y);

            LogEvent(logFindNearestITNs, GISQueryType.FindNearestITNs, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the GIS Query FindNearestAccessibleStops
        /// </summary>
        /// <param name="x">Easting</param>
        /// <param name="y">Northing</param>
        /// <param name="searchDistance">Search distance</param>
        /// <param name="maxResults">Max results to return</param>
        /// <param name="date">Date to search stops available for</param>
        /// <param name="wheelChair">Wheelchair accessible required</param>
        /// <param name="assistance">Assistance service required</param>
        /// <param name="stopTypes">stopTypes are any of: "rail","coach,"ferry","air","lulmetro","dlr","lightrail"</param>
        public AccessibleStopInfo[] FindNearestAccessibleStops(double x, double y, int searchDistance,
            int maxResults, DateTime date, bool wheelChair, bool assistance, string[] stopTypes)
        {
            DateTime logStart = DateTime.Now;
            AccessibleStopInfo[] result = query.FindNearestAccessibleStops(x, y, searchDistance, maxResults, date, wheelChair, assistance, stopTypes);

            LogEvent(logFindNearestAccessibleStops, GISQueryType.FindNearestAccessibleStops, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the GIS Query FindNearestAccessibleLocalities
        /// </summary>
        /// <param name="x">Easting</param>
        /// <param name="y">Northing</param>
        /// <param name="searchDistance">Search distance</param>
        /// <param name="maxResults">Max results to return</param>
        /// <param name="wheelChair">Wheelchair accessible required</param>
        /// <param name="assistance">Assistance service required</param>
        public AccessibleLocationInfo[] FindNearestAccessibleLocalities(double x, double y, int searchDistance,
            int maxResults, bool wheelChair, bool assistance)
        {
            DateTime logStart = DateTime.Now;
            AccessibleLocationInfo[] result = query.FindNearestAccessibleLocalities(x, y, searchDistance, maxResults, wheelChair, assistance);

            LogEvent(logFindNearestAccessibleLocalities, GISQueryType.FindNearestAccessibleLocalities, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the Gis Query GetExchangeInfoForNaptan
        /// </summary>
        /// <param name="naptan" > A TD Naptan instance</param>
        /// <returns>returns a QuerySchema representing the GIS query result</returns>
        public ExchangePointSchema GetExchangeInfoForNaptan(string naptanID, string exchangeType)
        {
            DateTime logStart = DateTime.Now;
            ExchangePointSchema result = query.GetExchangeInfoForNaptan(new string[1] { naptanID }, exchangeType);

            LogEvent(logGetExchangeInfoForNaptan, GISQueryType.GetExchangeInfoForNaptan, logStart);

            return result;
        }

        ///<summary>
        /// Wrapper for the GIS Query GetLocalityInfoForNatGazID
        ///</summary>
        ///
        public LocalityNameInfo GetLocalityInfoForNatGazID(string localityID)
        {
            DateTime logStart = DateTime.Now;
            LocalityNameInfo result = query.GetLocalityInfoForNatGazID(localityID);

            LogEvent(logGetLocalityInfoForNatGazID, GISQueryType.GetLocalityInfoForNatGazID, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the GIS Query GetStreetsFromPostCode
        /// </summary>
        /// <param name="postCode">postcode</param>
        /// <returns>an array of the streets that match the supplied postcode</returns>
        public string[] GetStreetsFromPostCode(string postCode)
        {
            DateTime logStart = DateTime.Now;
            string[] result = query.GetStreetsFromPostcode(postCode);

            LogEvent(logGetStreetsFromPostcode, GISQueryType.GetStreetsFromPostcode, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the GIS Query IsPointInAccessibleLocation
        /// </summary>
        /// <param name="x">Easting</param>
        /// <param name="y">Northing</param>
        /// <param name="searchDistance">Search distance</param>
        /// <param name="wheelChair">Wheelchair accessible</param>
        /// <param name="assistance">Assistance service</param>
        public void IsPointInAccessibleLocation(double x, double y, int searchDistance,
            out bool wheelChair, out bool assistance)
        {
            DateTime logStart = DateTime.Now;
            query.IsPointInAccessibleLocation(x, y, searchDistance, out wheelChair, out assistance);

            LogEvent(logIsPointInAccessibleLocation, GISQueryType.IsPointInAccessibleLocation, logStart);
        }


        #endregion

        #region Public methods - Currently not used. Commented out until required

        ///// <summary>
        ///// Wrapper for the Gis Query FindNearestStops
        ///// </summary>
        ///// <param name="x">easting</param>
        ///// <param name="y">northing</param>
        ///// <param name="maxDistance">max Walking distance</param>
        ///// <returns>returns a QuerySchema representing the GIS query result</returns>
        //public QuerySchema FindNearestStops(double x, double y, int maxDistance)
        //{
        //    DateTime logStart = DateTime.Now;
        //    QuerySchema result = query.FindNearestStops(x, y, maxDistance);

        //    LogEvent(logFindNearestStops, GISQueryType.FindNearestStops, logStart);

        //    return result;
        //}
                
        ///// <summary>
        ///// Wrapper for the Gis Query FindNearestStopsAndITNs
        ///// </summary>
        ///// <param name="x">easting</param>
        ///// <param name="y">northing</param>
        ///// <param name="maxDistance">max Walking distance</param>
        ///// <returns>returns a QuerySchema representing the GIS query result</returns>
        //public QuerySchema FindNearestStopsAndITNs(double x, double y, int maxDistance)
        //{
        //    DateTime logStart = DateTime.Now;
        //    QuerySchema result = query.FindNearestStopsAndITNs(x, y, maxDistance);

        //    LogEvent(logFindNearestStopsAndITNs, GISQueryType.FindNearestStopsAndITNs, logStart);

        //    return result;
        //}


        ///// <summary>
        ///// Wrapper for the Gis Query FindStopsInGroupForStops
        ///// </summary>
        ///// <param name="naptanIDs">naptan IDs</param>
        ///// <returns>returns a QuerySchema representing the GIS query result</returns>
        //public QuerySchema FindStopsInGroupForStops(string[] naptanIDs)
        //{
        //    DateTime logStart = DateTime.Now;
        //    QuerySchema result = query.FindStopsInGroupForStops(naptanIDs);

        //    LogEvent(logFindStopsInGroupForStops, GISQueryType.FindStopsInGroupForStops, logStart);

        //    return result;
        //}


        ///// <summary>
        ///// Wrapper for the Gis Query FindStopsInGroupForStops
        ///// </summary>
        ///// <param name="schema">An existing QuerySchema to be augmented with Naptan Groups</param>
        ///// <returns>returns a QuerySchema representing the GIS query result</returns>
        //public QuerySchema FindStopsInGroupForStops(QuerySchema schema)
        //{
        //    DateTime logStart = DateTime.Now;
        //    QuerySchema result = query.FindStopsInGroupForStops(schema);

        //    LogEvent(logFindStopsInGroupForStops, GISQueryType.FindStopsInGroupForStops, logStart);

        //    return result;
        //}


        ///// <summary>
        ///// Wrapper for the Gis Query FindStopsInRadius
        ///// </summary>
        ///// <param name="x">easting</param>
        ///// <param name="y">northing</param>
        ///// <param name="searchDistance">radius of circle to be searched</param>
        ///// <returns>returns a QuerySchema containing all Naptans within the specified
        ///// radius of the given point, and any naptans in the same groups as those found</returns>
        //public QuerySchema FindStopsInRadius(double x, double y, int searchDistance)
        //{
        //    DateTime logStart = DateTime.Now;
        //    QuerySchema result = query.FindStopsInRadius(x, y, searchDistance);

        //    LogEvent(logFindStopsInRadius, GISQueryType.FindStopsInRadius, logStart);

        //    return result;
        //}


        ///// <summary>
        ///// Wrapper for the Gis Query FindStopsInRadius
        ///// </summary>
        ///// <param name="x">easting</param>
        ///// <param name="y">northing</param>
        ///// <param name="searchDistance">radius of circle to be searched</param>
        ///// <param name="stopTypes">stop types to search</param>
        ///// <returns>returns a QuerySchema containing all Naptans within the specified
        ///// radius of the given point, and any naptans in the same groups as those found</returns>
        //public QuerySchema FindStopsInRadius(double x, double y, int searchDistance, string[] stopTypes)
        //{
        //    DateTime logStart = DateTime.Now;
        //    QuerySchema result = query.FindStopsInRadius(x, y, searchDistance, stopTypes);

        //    LogEvent(logFindStopsInRadius, GISQueryType.FindStopsInRadius, logStart);

        //    return result;
        //}

        
        ///// <summary>
        ///// Wrapper for the Gis Query FindExchangePointsInRadius
        ///// </summary>
        ///// <param name="x">easting</param>
        ///// <param name="y">northing</param>
        ///// <param name="radius">radius of circle to be searched</param>
        ///// <param name="mode">Air, Rail or Coach</param>
        ///// <param name="maximum">max no of points required</param>
        ///// <returns>returns an ExchangePointSchema containing the found points</returns>
        //public ExchangePointSchema FindExchangePointsInRadius(int x, int y, int radius, string mode, int maximum)
        //{
        //    DateTime logStart = DateTime.Now;
        //    ExchangePointSchema result = query.FindExchangePointsInRadius(x, y, radius, mode, maximum);

        //    LogEvent(logFindExchangePointsInRadius, GISQueryType.FindExchangePointsInRadius, logStart);

        //    return result;
        //}

        
        ///// <summary>
        ///// Wrapper for the GIS Query FindNearestPointOnTOID
        ///// </summary>
        ///// <param name="x">Easting</param>
        ///// <param name="y">Northing</param>
        ///// <param name="toid">Toid on which to find the closest point to the coordinates supplied</param>
        ///// <returns>a Point</returns>
        //public Point FindNearestPointOnTOID(double x, double y, string toid)
        //{
        //    DateTime logStart = DateTime.Now;
        //    Point result = query.FindNearestPointOnTOID(x, y, toid);

        //    LogEvent(logFindNearestPointOnTOID, GISQueryType.FindNearestPointOnTOID, logStart);

        //    return result;
        //}

        ///// <summary>
        ///// Wrapper for the GIS Query IsPointsInCycleDataArea
        ///// </summary>
        ///// <param name="point">Array of points to test</param>
        ///// <param name="sameAreaOnly">True/false flag to test if the points are in the same area</param>
        ///// <returns></returns>
        //public bool IsPointsInCycleDataArea(Point[] point, bool sameAreaOnly)
        //{
        //    DateTime logStart = DateTime.Now;
        //    bool result = query.IsPointsInCycleDataArea(point, sameAreaOnly);

        //    LogEvent(logIsPointsInCycleDataArea, GISQueryType.IsPointsInCycleDataArea, logStart);

        //    return result;
        //}

        ///// <summary>
        ///// Wrapper for the GIS Query IsPointsInWalkDataArea
        ///// </summary>
        ///// <param name="point">Array of points to test</param>
        ///// <param name="sameAreaOnly">True/False flag to test if the points are in the same area</param>
        ///// <param name="walkitID">WalkIt ID</param>
        ///// <param name="city">Name of WalkIt area or city</param>
        ///// <returns>Tru if specified points are in WalkIt data area, false otherwise</returns>
        //public bool IsPointsInWalkDataArea(Point[] point, bool sameAreaOnly, out int walkitID, out string city)
        //{
        //    DateTime logStart = DateTime.Now;
        //    walkitID = 0;
        //    city = string.Empty;
        //    bool result = query.IsPointsInWalkDataArea(point, sameAreaOnly, out walkitID, out city);

        //    LogEvent(logIsPointsInWalkDataArea, GISQueryType.IsPointsInWalkDataArea, logStart);

        //    return result;
        //}

        

        ///// <summary>
        ///// Wrapper for the GIS Query FindNearesetCarParks
        ///// </summary>
        ///// <param name="easting">Easting</param>
        ///// <param name="northing">Northing</param>
        ///// <param name="initalRadius">Minimum search radius</param>
        ///// <param name="maxRadius">Maximum search radius</param>
        ///// <param name="maxNoCarParks">Maximum number of car parks to be returned</param>
        ///// <returns>returns a QuerySchema representing the GIS query result</returns>
        //public QuerySchema FindNearestCarParks(double easting, double northing, int initialRadius,
        //    int maxRadius, int maxNoCarParks)
        //{
        //    DateTime logStart = DateTime.Now;
        //    QuerySchema result = query.FindNearestCarParks(easting, northing, initialRadius, maxRadius, maxNoCarParks);

        //    LogEvent(logFindNearestCarParks, GISQueryType.FindNearestCarParks, logStart);

        //    return result;
        //}

        /////<summary>
        ///// Wrapper for the GIS Query GetNPTGInfoForNaPTAN
        /////</summary>
        /////
        //public NPTGInfo GetNPTGInfoForNaPTAN(string naptanID)
        //{
        //    DateTime logStart = DateTime.Now;
        //    NPTGInfo result = query.GetNPTGInfoForNaPTAN(naptanID);

        //    LogEvent(logGetNPTGInfoForNaPTAN, GISQueryType.GetNPTGInfoForNaPTAN, logStart);

        //    return result;
        //}

        ///// <summary>
        ///// Wrapper for the GIS Query GetDistancesForTOIDs
        ///// </summary>
        ///// <returns></returns>
        //public CountryDistances[] GetDistancesForTOIDs(string[] toids)
        //{
        //    DateTime logStart = DateTime.Now;
        //    CountryDistances[] result = query.GetDistancesForTOIDs(toids);

        //    LogEvent(logGetDistancesForTOIDs, GISQueryType.GetDistancesForTOIDs, logStart);

        //    return result;
        //}

        #endregion

        #region Private methods

        /// <summary>
        /// Logs a GISQueryEvent
        /// </summary>
        private void LogEvent(bool log, GISQueryType gisQueryType, DateTime submitted)
        {
            // Place in a try because this GISQueryEvent might not have been configured
            // for all applications that use the GISQuery and therefore do not want to break
            // these existing applications
            try
            {
                if (log)
                {
                    GISQueryEvent gqe = new GISQueryEvent(gisQueryType, submitted, OperationalEvent.SessionIdUnassigned);
                    Logger.Write(gqe);
                }
            }
            catch (TDPException tdEx)
            {
                // Log error to allow configuration to be setup
                if (!tdEx.Logged)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error,
                        string.Format("Error logging GISQueryEvent. Exception: {0}. {1}", tdEx.Message, tdEx.StackTrace)));
                }
            }
        }

        /// <summary>
        /// Loads the property flags for logging GISQueryEvents
        /// </summary>
        private void LoadLoggingProperties()
        {
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestStops"], out logFindNearestStops);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestLocality"], out logFindNearestLocality);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestITN"], out logFindNearestITN);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestITNs"], out logFindNearestITNs);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestPointOnTOID"], out logFindNearestPointOnTOID);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestCarParks"], out logFindNearestCarParks);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestAccessibleStops"], out logFindNearestAccessibleStops);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestAccessibleLocalities"], out logFindNearestAccessibleLocalities);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestStopsAndITNs"], out logFindNearestStopsAndITNs);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindExchangePointsInRadius"], out logFindExchangePointsInRadius);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindStopsInRadius"], out logFindStopsInRadius);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindStopsInGroupForStops"], out logFindStopsInGroupForStops);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindStopsInfoForStops"], out logFindStopsInfoForStops);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.IsPointsInCycleDataArea"], out logIsPointsInCycleDataArea);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.IsPointsInWalkDataArea"], out logIsPointsInWalkDataArea);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.IsPointInAccessibleLocation"], out logIsPointInAccessibleLocation);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetExchangeInfoForNaptan"], out logGetExchangeInfoForNaptan);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetNPTGInfoForNaPTAN"], out logGetNPTGInfoForNaPTAN);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetLocalityInfoForNatGazID"], out logGetLocalityInfoForNatGazID);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetStreetsFromPostcode"], out logGetStreetsFromPostcode);
            bool.TryParse(Properties.Current["Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetDistancesForTOIDs"], out logGetDistancesForTOIDs);
        }

        #endregion
    }
}
