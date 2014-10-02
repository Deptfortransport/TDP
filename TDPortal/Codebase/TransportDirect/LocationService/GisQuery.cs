// *********************************************** 
// NAME                 : GisQuery.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 03/09/2003 
// DESCRIPTION  : Class which provides a very thin wrapper around the ArcIMS Query object provided by ESRI.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/GisQuery.cs-arc  $ 
//
//   Rev 1.8   Feb 11 2013 10:46:34   mmodi
//Properties to control logging of GISQueryEvent types
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.7   Jan 14 2013 14:41:54   mmodi
//Added GISQueryEvent
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.6   Jan 09 2013 11:40:24   mmodi
//GIS query updates for accessible searches
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Nov 11 2009 16:42:42   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.4   Sep 25 2009 14:16:34   mmodi
//Updated with new ESRI method GetDistancesForTOIDs
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Sep 10 2009 11:18:36   MTurner
//Added support for new interface member GetLocalityInfoForNatGazID.
//
//   Rev 1.2   Oct 13 2008 16:46:14   build
//Automatically merged from branch for stream5014
//
//   Rev 1.1.1.0   Jun 20 2008 14:56:26   mmodi
//Updated for new cycle planner methods
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Mar 10 2008 15:18:48   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:25:06   mturner
//Initial revision.
//
//   Rev 1.17   Mar 16 2007 10:00:54   build
//Automatically merged from branch for stream4362
//
//   Rev 1.16.1.0   Mar 09 2007 09:33:30   dsawe
//added GetNPTGInfoForNaPTAN method
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.16   Oct 06 2006 11:51:04   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.15.1.0   Aug 03 2006 13:59:38   esevern
//Added FindNearestCarParks
//
//   Rev 1.15   Mar 31 2006 18:44:40   RPhilpott
//Add support for new "GetStreetsForPostcode()" method.
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.14   Mar 21 2006 17:35:18   jmcallister
//Added new method GetExchangeInfoForNaptan. This uses new feature on td.interactivemapping.dll and allows us to resolve coach naptans.
//
//   Rev 1.13   Mar 20 2006 18:02:40   RWilby
//Merged stream0027: Start/End TOIDs changes.
//
//   Rev 1.12.1.0   Mar 14 2006 12:01:54   RWilby
//Added FindNearestITN method
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.12   Sep 10 2004 15:35:44   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.11   Jul 23 2004 16:47:32   RPhilpott
//Add support for FindStopsInfoForStops().
//
//   Rev 1.10   May 28 2004 17:52:18   passuied
//update as part of FindStation development
//
//   Rev 1.9   Dec 12 2003 16:55:58   PNorell
//Updated for ESRI Del 1.5
//
//   Rev 1.8   Dec 11 2003 17:52:42   RPhilpott
//Make use of new FindStopsInRadius() method in GISQuery.
//Resolution for 281: Postcode journey doesn't return public transport journeys
//
//   Rev 1.7   Oct 03 2003 13:38:34   PNorell
//Updated the new exception identifier.
//
//   Rev 1.6   Oct 02 2003 17:44:12   COwczarek
//id parameter passed in TDException constructor set to -1 to enable compilation after introduction of new TDException constructor which takes an enum type for id. This is a temporary fix - the constructor taking an
//id of type long will be removed.
//
//   Rev 1.5   Sep 22 2003 17:31:18   passuied
//made all objects serializable
//
//   Rev 1.4   Sep 22 2003 13:34:40   RPhilpott
//"to do" comment no longer needed
//
//   Rev 1.3   Sep 21 2003 18:43:36   kcheung
//Uncommented FindStopsInGroupForStops
//
//   Rev 1.2   Sep 20 2003 16:59:46   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.1   Sep 09 2003 17:23:46   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:24   passuied
//Initial Revision


using System;
using TransportDirect.Common;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.PropertyService.Properties;

using Logger = System.Diagnostics.Trace;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Class which provides a very thin wrapper around the ArcIMS Query object provided by ESRI.
	/// </summary>
	[Serializable()]
	public class GisQuery : IGisQuery
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

        public GisQuery()
		{
			// get ServiceName and ServerName properties from PropertyService
			string serviceName  = Properties.Current["locationservice.servicename"];
			string serverName = Properties.Current["locationservice.servername"];

			if (serviceName == string.Empty || serverName == string.Empty)
				throw new TDException ("Unable to access the GisQuery Properties", false, TDExceptionIdentifier.LSGizQueryPropertyInvalid);
			query = new Query(serverName, serviceName);	
			
            // Read property flags for logging
            LoadLoggingProperties();
        }

        #endregion

        /// <summary>
		/// Wrapper for the Gis Query GetExchangeInfoForNaptan
		/// </summary>
		/// <param name="naptan" > A TD Naptan instance</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public ExchangePointSchema GetExchangeInfoForNaptan(TDNaptan naptan)
		{
            DateTime logStart = DateTime.Now;

			string[] naptanID = new string[1];

			naptanID[0] = naptan.Naptan;
			
			ExchangePointSchema result = query.GetExchangeInfoForNaptan(naptanID, naptan.TransportExchangeType);

            LogEvent(logGetExchangeInfoForNaptan, GISQueryType.GetExchangeInfoForNaptan, logStart);

            return result;
		}

		/// <summary>
		/// Wrapper for the Gis Query FindNearestStops
		/// </summary>
		/// <param name="x">easting</param>
		/// <param name="y">northing</param>
		/// <param name="maxDistance">max Walking distance</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestStops(double x, double y, int maxDistance)
		{
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindNearestStops(x, y, maxDistance);

            LogEvent(logFindNearestStops, GISQueryType.FindNearestStops, logStart);

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
		/// Wrapper for the Gis Query FindNearestStopsAndITNs
		/// </summary>
		/// <param name="x">easting</param>
		/// <param name="y">northing</param>
		/// <param name="maxDistance">max Walking distance</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestStopsAndITNs(double x, double y, int maxDistance)
		{
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindNearestStopsAndITNs(x, y, maxDistance);

            LogEvent(logFindNearestStopsAndITNs, GISQueryType.FindNearestStopsAndITNs, logStart);

            return result;
        }

	
		/// <summary>
		/// Wrapper for the Gis Query FindStopsInGroupForStops
		/// </summary>
		/// <param name="naptanIDs">naptan IDs</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindStopsInGroupForStops(string[] naptanIDs)
		{
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindStopsInGroupForStops(naptanIDs);

            LogEvent(logFindStopsInGroupForStops, GISQueryType.FindStopsInGroupForStops, logStart);

            return result;
        }

		
		/// <summary>
		/// Wrapper for the Gis Query FindStopsInGroupForStops
		/// </summary>
		/// <param name="schema">An existing QuerySchema to be augmented with Naptan Groups</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindStopsInGroupForStops(QuerySchema schema)
		{
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindStopsInGroupForStops(schema);
            
            LogEvent(logFindStopsInGroupForStops, GISQueryType.FindStopsInGroupForStops, logStart);

            return result;
        }


		/// <summary>
		/// Wrapper for the Gis Query FindStopsInRadius
		/// </summary>
		/// <param name="x">easting</param>
		/// <param name="y">northing</param>
		/// <param name="searchDistance">radius of circle to be searched</param>
		/// <returns>returns a QuerySchema containing all Naptans within the specified
		/// radius of the given point, and any naptans in the same groups as those found</returns>
		public QuerySchema FindStopsInRadius(double x, double y, int searchDistance)
		{
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindStopsInRadius(x, y, searchDistance);

            LogEvent(logFindStopsInRadius, GISQueryType.FindStopsInRadius, logStart);

            return result;
        }


		/// <summary>
		/// Wrapper for the Gis Query FindStopsInRadius
		/// </summary>
		/// <param name="x">easting</param>
		/// <param name="y">northing</param>
		/// <param name="searchDistance">radius of circle to be searched</param>
		/// <param name="stopTypes">stop types to search</param>
		/// <returns>returns a QuerySchema containing all Naptans within the specified
		/// radius of the given point, and any naptans in the same groups as those found</returns>
		public QuerySchema FindStopsInRadius(double x, double y, int searchDistance, string[] stopTypes)
		{
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindStopsInRadius(x, y, searchDistance, stopTypes);

            LogEvent(logFindStopsInRadius, GISQueryType.FindStopsInRadius, logStart);

            return result;
        }

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
		/// <returns>returns a QuerySchema containing stop info for the supplied naptans</returns>
		public string FindNearestLocality(double x, double y)
		{
            DateTime logStart = DateTime.Now;
            string result = query.FindNearestLocality(x, y);

            LogEvent(logFindNearestLocality, GISQueryType.FindNearestLocality, logStart);

            return result;
        }

		/// <summary>
		/// Wrapper for the Gis Query FindExchangePointsInRadius
		/// </summary>
		/// <param name="x">easting</param>
		/// <param name="y">northing</param>
		/// <param name="radius">radius of circle to be searched</param>
		/// <param name="mode">Air, Rail or Coach</param>
		/// <param name="maximum">max no of points required</param>
		/// <returns>returns an ExchangePointSchema containing the found points</returns>
		public ExchangePointSchema FindExchangePointsInRadius(int x, int y, int radius, string mode, int maximum)
		{
            DateTime logStart = DateTime.Now;
            ExchangePointSchema result = query.FindExchangePointsInRadius(x, y, radius, mode, maximum);

            LogEvent(logFindExchangePointsInRadius, GISQueryType.FindExchangePointsInRadius, logStart);

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
		public QuerySchema FindNearestITN(double x, double y,string address,bool ingoreMotorways)
		{
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindNearestITN(x, y, address, ingoreMotorways);

            LogEvent(logFindNearestITN, GISQueryType.FindNearestITN, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the GIS Query FindNearestPointOnTOID
        /// </summary>
        /// <param name="x">Easting</param>
        /// <param name="y">Northing</param>
        /// <param name="toid">Toid on which to find the closest point to the coordinates supplied</param>
        /// <returns>a Point</returns>
        public Point FindNearestPointOnTOID(double x, double y, string toid)
        {
            DateTime logStart = DateTime.Now;
            Point result = query.FindNearestPointOnTOID(x, y, toid);

            LogEvent(logFindNearestPointOnTOID, GISQueryType.FindNearestPointOnTOID, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the GIS Query IsPointsInCycleDataArea
        /// </summary>
        /// <param name="point">Array of points to test</param>
        /// <param name="sameAreaOnly">True/false flag to test if the points are in the same area</param>
        /// <returns></returns>
        public bool IsPointsInCycleDataArea(Point[] point, bool sameAreaOnly)
        {
            DateTime logStart = DateTime.Now;
            bool result = query.IsPointsInCycleDataArea(point, sameAreaOnly);

            LogEvent(logIsPointsInCycleDataArea, GISQueryType.IsPointsInCycleDataArea, logStart);

            return result;
        }

        /// <summary>
        /// Wrapper for the GIS Query IsPointsInWalkDataArea
        /// </summary>
        /// <param name="point">Array of points to test</param>
        /// <param name="sameAreaOnly">True/False flag to test if the points are in the same area</param>
        /// <param name="walkitID">WalkIt ID</param>
        /// <param name="city">Name of WalkIt area or city</param>
        /// <returns>Tru if specified points are in WalkIt data area, false otherwise</returns>
        public bool IsPointsInWalkDataArea(Point[] point, bool sameAreaOnly, out int walkitID, out string city)
        {
            DateTime logStart = DateTime.Now;
            walkitID = 0;
            city = string.Empty;
            bool result = query.IsPointsInWalkDataArea(point, sameAreaOnly, out walkitID, out city);

            LogEvent(logIsPointsInWalkDataArea, GISQueryType.IsPointsInWalkDataArea, logStart);

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
		/// Wrapper for the GIS Query FindNearesetCarParks
		/// </summary>
		/// <param name="easting">Easting</param>
		/// <param name="northing">Northing</param>
		/// <param name="initalRadius">Minimum search radius</param>
		/// <param name="maxRadius">Maximum search radius</param>
		/// <param name="maxNoCarParks">Maximum number of car parks to be returned</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestCarParks(double easting, double northing,	int initialRadius, 
			int maxRadius, int maxNoCarParks)
		{
            DateTime logStart = DateTime.Now;
            QuerySchema result = query.FindNearestCarParks(easting, northing, initialRadius, maxRadius, maxNoCarParks);

            LogEvent(logFindNearestCarParks, GISQueryType.FindNearestCarParks, logStart);

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

		///<summary>
		/// Wrapper for the GIS Query GetNPTGInfoForNaPTAN
		///</summary>
		///
		public NPTGInfo GetNPTGInfoForNaPTAN(string naptanID)
		{
            DateTime logStart = DateTime.Now;
            NPTGInfo result = query.GetNPTGInfoForNaPTAN(naptanID);

            LogEvent(logGetNPTGInfoForNaPTAN, GISQueryType.GetNPTGInfoForNaPTAN, logStart);

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
        /// Wrapper for the GIS Query GetDistancesForTOIDs
        /// </summary>
        /// <returns></returns>
        public CountryDistances[] GetDistancesForTOIDs(string[] toids)
        {
            DateTime logStart = DateTime.Now;
            CountryDistances[] result = query.GetDistancesForTOIDs(toids);

            LogEvent(logGetDistancesForTOIDs, GISQueryType.GetDistancesForTOIDs, logStart);

            return result;
        }

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
            catch (TDException tdEx)
            {
                // Log error to allow configuration to be setup
                if (!tdEx.Logged)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
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
