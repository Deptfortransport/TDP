// *********************************************** 
// NAME                 : MockGisQuerys.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 03/09/2003 
// DESCRIPTION  : Class which provides a very thin wrapper around the ArcIMS Query object provided by ESRI.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/MockGisQuery.cs-arc  $ 
//
//   Rev 1.5   Jan 09 2013 11:40:24   mmodi
//GIS query updates for accessible searches
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Nov 11 2009 16:42:44   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.3   Sep 25 2009 14:16:36   mmodi
//Updated with new ESRI method GetDistancesForTOIDs
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Sep 10 2009 11:17:18   mturner
//Added support for new interface member GetLocalityInfoForNatGazID.
//
//   Rev 1.1   Oct 13 2008 16:46:16   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 14:56:24   mmodi
//Updated for new cycle planner methods
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:25:14   mturner
//Initial revision.
//
//   Rev 1.6   Mar 16 2007 10:00:54   build
//Automatically merged from branch for stream4362
//
//   Rev 1.5.1.0   Mar 09 2007 09:36:14   dsawe
//added method GetNPTGInfoForNaPTAN
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.5   Oct 06 2006 12:48:14   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.4.1.0   Aug 03 2006 13:59:38   esevern
//Added FindNearestCarParks
//
//   Rev 1.4   Mar 31 2006 18:44:40   RPhilpott
//Add support for new "GetStreetsForPostcode()" method.
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.3   Mar 21 2006 17:52:06   jmcallister
//Amended in line with changed IGisQuery interface.
//
//   Rev 1.2   Mar 20 2006 18:03:46   RWilby
//Merged stream0027: Start/End TOIDs changes.
//
//   Rev 1.1.1.0   Mar 14 2006 12:03:44   RWilby
//Added FindNearestITN method
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.1   Aug 16 2005 10:53:38   RPhilpott
//Get rid of exception warning
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Aug 09 2005 18:23:40   rgreenwood
//Initial revision.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results



using System;
using System.Data;
using TransportDirect.Common;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Class which provides a very thin wrapper around the ArcIMS Query object provided by ESRI.
	/// </summary>
	[Serializable()]
	public class MockGisQuery : IGisQuery
	{
		private Query query;
		public MockGisQuery()
		{
			// get ServiceName and ServerName properties from PropertyService
			string serviceName  = Properties.Current["locationservice.servicename"];
			string serverName = Properties.Current["locationservice.servername"];

			if (serviceName == string.Empty || serverName == string.Empty)
				throw new TDException ("Unable to access the GisQuery Properties", false, TDExceptionIdentifier.LSGizQueryPropertyInvalid);
			query = new Query(serverName, serviceName);	
			

		}
		
		/// <summary>
		/// Wrapper for the Gis Query GetExchangeInfoForNaptan
		/// </summary>
		/// <param name="naptan" > A TD Naptan instance</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public ExchangePointSchema GetExchangeInfoForNaptan(TDNaptan naptan)
		{
			string[] naptanID = new string[1];

			naptanID[0] = naptan.Naptan;
			
			return query.GetExchangeInfoForNaptan(naptanID, naptan.TransportExchangeType);
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
			return query.FindNearestStops(x, y, maxDistance);
		}

		
		/// <summary>
		/// Wrapper for the Gis Query FindNearestITNs
		/// </summary>
		/// <param name="x">easting</param>
		/// <param name="y">northing</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestITNs(double x, double y)
		{
			return query.FindNearestITNs(x, y);
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
			return query.FindNearestStopsAndITNs(x, y, maxDistance);
		}

	
		/// <summary>
		/// Wrapper for the Gis Query FindStopsInGroupForStops
		/// </summary>
		/// <param name="naptanIDs">naptan IDs</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindStopsInGroupForStops(string[] naptanIDs)
		{
			return query.FindStopsInGroupForStops(naptanIDs);
		}

		
		/// <summary>
		/// Wrapper for the Gis Query FindStopsInGroupForStops
		/// </summary>
		/// <param name="schema">An existing QuerySchema to be augmented with Naptan Groups</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindStopsInGroupForStops(QuerySchema schema)
		{
			return query.FindStopsInGroupForStops(schema);
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
			return query.FindStopsInRadius(x, y, searchDistance);
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
			return query.FindStopsInRadius(x, y, searchDistance, stopTypes);
		}

		/// <summary>
		/// Wrapper for the Gis Query FindStopsInfoForStops
		/// </summary>
		/// <param name="searchDistance">radius of circle to be searched</param>
		/// <param name="naptanIDs">naptan IDs</param>
		/// <returns>returns a QuerySchema containing stop info for the supplied naptans</returns>
		public QuerySchema FindStopsInfoForStops(string[] naptanIDs)
		{

			QuerySchema qs = new QuerySchema();

			bool fileFound = false;

			if	(naptanIDs != null && naptanIDs.Length > 0 && naptanIDs[0]!=  null && naptanIDs[0].Length > 0)
			{
				string fileName = "C:\\TDPortal\\CodeBase\\TransportDirect\\LocationService\\bin\\Debug\\FindStopsInGroupForStopsMapDetails" + naptanIDs[0] + ".xml";

				try 
				{
					qs.ReadXml(fileName, XmlReadMode.Auto);
					fileFound = true;
				}
				catch 
				{
					// assume file not found 
				}
			}

			if	(!fileFound)
			{
				return query.FindStopsInfoForStops(naptanIDs);
			}
			else
			{
				return qs;
			}

			
		}

		/// <summary>
		/// Wrapper for the Gis Query FindNearestLocality
		/// </summary>
		/// <param name="x">easting</param>
		/// <param name="x">northing</param>
		/// <returns>returns a QuerySchema containing stop info for the supplied naptans</returns>
		public string FindNearestLocality(double x, double y)
		{
			return query.FindNearestLocality(x, y);
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
			return query.FindExchangePointsInRadius(x, y, radius, mode, maximum);
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
			return query.FindNearestITN(x,y,address,ingoreMotorways);
			
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
            return query.FindNearestPointOnTOID(x, y, toid);
        }

        /// <summary>
        /// Wrapper for the GIS Query IsPointsInCycleDataArea
        /// </summary>
        /// <param name="point">Array of points to test</param>
        /// <param name="sameAreaOnly">True/false flag to test if the points are in the same area</param>
        /// <returns></returns>
        public bool IsPointsInCycleDataArea(Point[] point, bool sameAreaOnly)
        {
            return query.IsPointsInCycleDataArea(point, sameAreaOnly);
        }

        /// <summary>
        /// Wrapper for the GIS Query IsPointsInWalkDataArea
        /// </summary>
        /// <param name="point">Array of points ot test</param>
        /// <param name="sameAreaOnly">True/False flag to test if the points are in the same area</param>
        /// <param name="walkitID">WalkIt IDs of points</param>
        /// <param name="city">Name of WalkIt data area or city</param>
        /// <returns>True if specified points are within a WalkIt data area</returns>
        public bool IsPointsInWalkDataArea(Point[] point, bool sameAreaOnly, out int walkitID, out string city)
        {
            walkitID = 0;
            city = string.Empty;
            bool result = query.IsPointsInWalkDataArea(point, sameAreaOnly, out walkitID, out city);
            return result;
        }
		/// <summary>
		/// Wrapper for the GIS Query GetStreetsFromPostCode
		/// </summary>
		/// <param name="postCode">postcode</param>
		/// <returns>an array of the streets that match the supplied postcode</returns>
		public string[] GetStreetsFromPostCode(string postCode)
		{
			return query.GetStreetsFromPostcode(postCode);
		}

		/// <summary>
		/// Wrapper for the Gis Query FindNearestCarParks
		/// </summary>
		/// <param name="easting">Easting</param>
		/// <param name="northing">Northing</param>
		/// <param name="initalRadius">Minimum search radius</param>
		/// <param name="maxRadius">Maximum search radius</param>
		/// <param name="maxNoCarParks">Maximum number of car parks to be returned</param>		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestCarParks(double easting, double northing,	int initialRadius, 
			int maxRadius, int maxNoCarParks)		
		{
			return query.FindNearestCarParks(easting, northing,	initialRadius, maxRadius, maxNoCarParks);
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
            return query.FindNearestAccessibleStops(x, y, searchDistance, maxResults, date, wheelChair, assistance, stopTypes);
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
            return query.FindNearestAccessibleLocalities(x, y, searchDistance, maxResults, wheelChair, assistance);
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
            query.IsPointInAccessibleLocation(x, y, searchDistance, out wheelChair, out assistance);
        }
		
		///<summary>
		/// Wrapper for the GIS Query GetNPTGInfoForNaPTAN
		///</summary>
		///
		public NPTGInfo GetNPTGInfoForNaPTAN(string naptanID)
		{
			return query.GetNPTGInfoForNaPTAN(naptanID);
		}

        ///<summary>
        /// Wrapper for the GIS Query GetLocalityInfoForNatGazID
        ///</summary>
        ///
        public LocalityNameInfo GetLocalityInfoForNatGazID(string localityID)
        {
            LocalityNameInfo lni = query.GetLocalityInfoForNatGazID(localityID);
            return lni;
        }

        /// <summary>
        /// Wrapper for the GIS Query GetDistancesForTOIDs
        /// </summary>
        /// <returns></returns>
        public CountryDistances[] GetDistancesForTOIDs(string[] toids)
        {
            return query.GetDistancesForTOIDs(toids);
        }
	}
}
