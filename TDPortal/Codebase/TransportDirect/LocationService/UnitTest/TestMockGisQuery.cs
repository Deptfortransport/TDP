// *********************************************** 
// NAME                 : TestMockGisQuery.cs
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2004-07-21 
// DESCRIPTION			: GISQuery stub for NUnit testing
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestMockGisQuery.cs-arc  $ 
//
//   Rev 1.5   Jan 09 2013 11:40:26   mmodi
//GIS query updates for accessible searches
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Nov 11 2009 16:42:46   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.3   Sep 25 2009 14:16:36   mmodi
//Updated with new ESRI method GetDistancesForTOIDs
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Sep 10 2009 11:16:30   mturner
//Added support for new interface member GetLocalityInfoForNatGazID.
//
//   Rev 1.1   Oct 13 2008 16:46:18   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 14:53:46   mmodi
//Updated for cycle journeys, query methods
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:25:48   mturner
//Initial revision.
//
//   Rev 1.13   Mar 16 2007 10:00:54   build
//Automatically merged from branch for stream4362
//
//   Rev 1.12.1.0   Mar 09 2007 09:37:30   dsawe
//added method GetNPTGInfoForNaPTAN
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.12   Oct 06 2006 12:54:12   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.11.1.0   Aug 03 2006 13:59:10   esevern
//added FindNearestCarParks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.11   Jun 02 2006 14:03:52   rphilpott
//Corrections for change in get-locality logic.
//Resolution for 4103: Find Cheaper - journeys not always returned.
//
//   Rev 1.10   Mar 31 2006 18:55:56   RPhilpott
//Added support for new GetStreetsFromPostcode() method.
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.9   Mar 21 2006 17:53:18   jmcallister
//Amended in line with changed IGisQuery interface.
//
//   Rev 1.8   Mar 21 2006 10:54:46   RWilby
//Merge stream0027: Added Test FindNearestITN implementation.
//
//   Rev 1.7   Mar 20 2006 18:04:18   RWilby
//Merged stream0027: Start/End TOIDs changes.
//
//   Rev 1.6.1.0   Mar 14 2006 12:07:16   RWilby
//Added FindNearestITN method to implement new IGISQuery interface.
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.6   Feb 23 2006 19:15:42   build
//Automatically merged from branch for stream3129
//
//   Rev 1.5.1.0   Jan 19 2006 09:39:38   RWilby
//Changed FindStopsInRadius to read FindStopsInRadiusBusStops1.xml file needed by TestLocationSearch.TestFindBusStops
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.5   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.4   Jan 19 2005 12:07:56   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.3   Sep 11 2004 17:35:02   RPhilpott
//Tests updated for addition of FindNearestLocality and FindExchangePointsInRadius.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1328: Find nearest stations/airports does not return any results
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//
//   Rev 1.2   Sep 10 2004 15:36:08   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.1   Jul 23 2004 16:47:12   RPhilpott
//Add support for FindStopsInfoForStops().
//
//   Rev 1.0   Jul 23 2004 16:27:48   RPhilpott
//Initial revision.
//

using System;
using System.Data;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Mock GISQuery
	/// </summary>
	[Serializable()]
	public class TestMockGisQuery : IGisQuery, IServiceFactory
	{
		public TestMockGisQuery()
		{
		}
	
		public QuerySchema FindNearestStops(double x, double y, int maxDistance)
		{
			QuerySchema qs = new QuerySchema();
			qs.ReadXml("FindNearestStops1.xml", XmlReadMode.Auto);
			return qs;
		}

		public ExchangePointSchema GetExchangeInfoForNaptan(TDNaptan naptan)
		{
			ExchangePointSchema eps = new ExchangePointSchema();
			eps.ReadXml("GetExchangeInfoForNaptan1.xml", XmlReadMode.Auto);
			return eps;
		}

		public QuerySchema FindNearestITNs(double x, double y)
		{
			QuerySchema qs = new QuerySchema();
			qs.ReadXml("FindNearestITNs1.xml", XmlReadMode.Auto);
			return qs;
		}

		public QuerySchema FindNearestStopsAndITNs(double x, double y, int maxDistance)
		{
			QuerySchema qs = new QuerySchema();
			qs.ReadXml("FindNearestStopsAndITNs1.xml", XmlReadMode.Auto);
			return qs;
		}

		public QuerySchema FindStopsInGroupForStops(string[] naptanIDs)
		{
			QuerySchema qs = new QuerySchema();
			qs.ReadXml("FindStopsInGroupForStops1.xml", XmlReadMode.Auto);
			return qs;
		}

		public QuerySchema FindStopsInGroupForStops(QuerySchema querySchema)
		{
			QuerySchema qs = new QuerySchema();
			qs.ReadXml("FindStopsInGroupForStops2.xml", XmlReadMode.Auto);
			return qs;
		}

		public QuerySchema FindStopsInfoForStops(string[] naptanIDs)
		{
			QuerySchema qs = new QuerySchema();
			if	(naptanIDs[0] == "9200LHR4")
			{
				qs.ReadXml("FindStopsInGroupForStops3.xml", XmlReadMode.Auto);
			}
			else
			{
				qs.ReadXml("FindStopsInGroupForStops1.xml", XmlReadMode.Auto);
			}
			return qs;
		}


		public QuerySchema FindStopsInRadius(double x, double y, int searchDistance, string[] stopTypes)
		{
			QuerySchema qs = new QuerySchema();

			foreach (string stopType in stopTypes)
			{
				switch (stopType)
				{
					case "GAT":
						qs.ReadXml("FindStopsInRadiusAir1.xml", XmlReadMode.Auto);
						break;
					
					case "RLY":
						qs.ReadXml("FindStopsInRadiusRail1.xml", XmlReadMode.Auto);

						if	(searchDistance > 20000)
						{
							qs.ReadXml("FindStopsInRadiusRail2.xml", XmlReadMode.Auto);
						}
						break;
					
					case "BCS":
						//Load data used by TestLocationSearch.TestFindBusStops
						qs.ReadXml("FindStopsInRadiusBusStops1.xml", XmlReadMode.Auto);
						break;
					default:
						qs.ReadXml("FindStopsInRadiusCoach1.xml", XmlReadMode.Auto);

						if	(searchDistance > 75000)
						{
							qs.ReadXml("FindStopsInRadiusCoach2.xml", XmlReadMode.Auto);
						}
						break;
				}
			}
			
			return qs;
		}


		public QuerySchema FindStopsInRadius(double x, double y, int searchDistance)
		{
			QuerySchema qs = new QuerySchema();
			qs.ReadXml("FindStopsInRadius1.xml", XmlReadMode.Auto);
			return qs;
		}

		public string FindNearestLocality(double x, double y)
		{
			return "E0001659";
		}


		public ExchangePointSchema FindExchangePointsInRadius(int x, int y, int radius, string mode, int maximum)
		{
			ExchangePointSchema eps = new ExchangePointSchema();

			switch (mode)
			{
				case "Air":
					eps.ReadXml("FindExchangePointsInRadiusAir1.xml", XmlReadMode.Auto);
					break;
				
				case "Rail":
					eps.ReadXml("FindExchangePointsInRadiusRail1.xml", XmlReadMode.Auto);

					if	(radius > 20000)
					{
						eps.ReadXml("FindExchangePointsInRadiusRail2.xml", XmlReadMode.Auto);
					}
					break;
				
				case "Coach":
					eps.ReadXml("FindExchangePointsInRadiusCoach1.xml", XmlReadMode.Auto);

					if	(radius > 75000)
					{
						eps.ReadXml("FindExchangePointsInRadiusCoach2.xml", XmlReadMode.Auto);
					}
					break;
			}
			
			return eps;
		}

		public QuerySchema FindNearestCarParks(double easting, double northing,	int initialRadius, 
			int maxRadius, int maxNoCarParks)
		{
			QuerySchema qs = new QuerySchema();
			qs.ReadXml("FindNearestCarParks.xml", XmlReadMode.Auto);
			return qs;
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
			QuerySchema qs = new QuerySchema();
			qs.ReadXml("FindNearestITNs1.xml", XmlReadMode.Auto);
			return qs;
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
            return new Point((double)453007, (double)336731);
        }

        /// <summary>
        /// Wrapper for the GIS Query IsPointsInCycleDataArea
        /// </summary>
        /// <param name="point">Array of points to test</param>
        /// <param name="sameAreaOnly">True/false flag to test if the points are in the same area</param>
        /// <returns></returns>
        public bool IsPointsInCycleDataArea(Point[] point, bool sameAreaOnly)
        {
            return true;
        }

        /// <summary>
        /// Wrapper for the GIS Query IsPointsInWalkDataArea
        /// </summary>
        /// <param name="point">Array of points to test</param>
        /// <param name="sameAreaOnly">True/False flag to test if the points are in the same area</param>
        /// <param name="walkitID">WalkIt IDs of points</param>
        /// <param name="city">Name of WalkIt data area or city</param>
        /// <returns>True if specified points are within a WalkIt data area, false otherwise</returns>
        public bool IsPointsInWalkDataArea(Point[] point, bool sameAreaOnly, out int walkitID, out string city)
        {
            walkitID = 0;
            city = string.Empty;
            return true;
        }

		/// <summary>
		/// Wrapper for the GIS Query GetStreetsFromPostCode
		/// </summary>
		/// <param name="postCode">postcode</param>
		/// <returns>an array of the streets that match the supplied postcode</returns>
		public string[] GetStreetsFromPostCode(string postCode)
		{
			return new string[0];
		}



		/// <summary>
		/// Factory method
		/// </summary>
		public object Get()
		{
			return this;
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
            AccessibleStopInfo[] accessibleStopInfos = new AccessibleStopInfo[1];

            AccessibleStopInfo asi = new AccessibleStopInfo("900010171", "Warrington: Bus Interchange", true, true, 5000);

            accessibleStopInfos[0] = asi;

            return accessibleStopInfos;
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
            AccessibleLocationInfo[] accessibleLocalityInfos = new AccessibleLocationInfo[1];

            AccessibleLocationInfo ali = new AccessibleLocationInfo("E0001659", "Locality", true, true, 5000);

            accessibleLocalityInfos[0] = ali;

            return accessibleLocalityInfos;
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
            wheelChair = true;
            assistance = true;
        }

		///<summary>
		/// Wrapper for the GIS Query GetNPTGInfoForNaPTAN
		///</summary>
		///
		public NPTGInfo GetNPTGInfoForNaPTAN(string naptanID)
		{
			return new NPTGInfo("1", "2");
		}

        ///<summary>
        /// Wrapper for the GIS Query GetLocalityInfoForNatGazID
        ///</summary>
        ///
        public LocalityNameInfo GetLocalityInfoForNatGazID(string localityID)
        {
            return new LocalityNameInfo("Beeston", "Nottingham", "Nottinghamshire", 512345, 512345, "123", "123");
        }

        /// <summary>
        /// Wrapper for the GIS Query GetDistancesForTOIDs
        /// </summary>
        /// <returns></returns>
        public CountryDistances[] GetDistancesForTOIDs(string[] toids)
        {
            CountryDistances[] countryDistances = new CountryDistances[1];

            countryDistances[0] = new CountryDistances(string.Empty, 1000, 1000, 1000);

            return countryDistances;
        }
	}
}
