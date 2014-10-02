// *********************************************** 
// NAME                 : DummyGisQuery.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 13/10/03
// DESCRIPTION			: Implementation of the DummyGisQuery class
// ************************************************ 

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;

namespace  TransportDirect.UserPortal.PricingRetail.Domain
{

	/// <summary>
	/// Stub implementation of IGisQuery. Provides a modicum of fake functionality for testing purposes.
	/// </summary>
	[Serializable()]
	public class TestStubGisQuery : IGisQuery, IServiceFactory
	{

		class DummyQuerySchema : QuerySchema
		{
			private Query query;

			public DummyQuerySchema(string[] naptans)
			{
				foreach(string naptan in naptans)
				{
					StopsRow dr = Stops.NewStopsRow();
					dr.atcocode = naptan;
					dr.natgazid = "Somewhere";
					dr.X = 1;
					dr.Y = 1;
					Stops.Rows.Add(dr);
				}
				
				// get ServiceName and ServerName properties from PropertyService
				string serviceName  = Properties.Current["locationservice.servicename"];
				string serverName	= Properties.Current["locationservice.servername"];

				if (serviceName == string.Empty || serverName == string.Empty)
				{
					throw new TDException ("Unable to access the GisQuery Properties", false, TDExceptionIdentifier.LSGizQueryPropertyInvalid);
				}
				query = new Query(serverName, serviceName);	
			}
		}


		class DummyExchangePointSchema : ExchangePointSchema
		{
			public DummyExchangePointSchema(string[] naptans, string[] modes)
			{
				for (int i = 0; i < naptans.Length; i++)
				{
					string naptan	= naptans[i];
					string mode		= modes[i];
					this.ExchangePoints.AddExchangePointsRow(naptan, naptan + " name", 111111, 222222, mode, "E00009999");
				}				
			}
		}


		// Dummy NaPTAN groups. Completely arbitary values
		private string[] naptans1 = {"9100VICTRIC", "9100VICTRIE"};
		private string[] naptans2 = {"9100STPX"};
		private string[] naptans3 = {"9100NTNG"};
		private string[] naptans4 = {"9100DOVERP"};
		private string[] naptans5 = {"9100MNCRPIC"};

		private string[] naptanAir  = {"9200LHR"};
		private string[] modeAir	= {"Air"};

		private ArrayList group1 = new ArrayList();
		private ArrayList group2 = new ArrayList();
		private ArrayList group3 = new ArrayList();
		private ArrayList group4 = new ArrayList();
		private ArrayList group5 = new ArrayList();

		private QuerySchema qs1;
		private QuerySchema qs2;
		private QuerySchema qs3;
		private QuerySchema qs4;
		private QuerySchema qs5;

		public TestStubGisQuery()
		{	
			// Set up our DummyQuerySchemas as search groups of NaPTANs
			qs1 = new DummyQuerySchema(naptans1);
			qs2 = new DummyQuerySchema(naptans2);
			qs3 = new DummyQuerySchema(naptans3);
			qs4 = new DummyQuerySchema(naptans4);
			qs5 = new DummyQuerySchema(naptans5);

			group1.Add(naptans1[0]);
			group1.Add(naptans1[1]);
			group2.Add(naptans2[0]);
			group3.Add(naptans3[0]);
			group4.Add(naptans4[0]);
			group5.Add(naptans5[0]);
		}



		/// <summary>
		/// Wrapper for the Gis Query FindNearestStops
		/// </summary>
		/// <param name="x">x coordonate</param>
		/// <param name="y">y coordonate</param>
		/// <param name="maxDistance">max Walking distance</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestStops(double x, double y, int maxDistance)
		{
			return new QuerySchema();
		
		}

		public QuerySchema FindNearestCarParks(double easting, double northing, 
			int initialRadius, int maxRadius, int maxNoCarParks)
		{
			return new QuerySchema();
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
			
			return new DummyExchangePointSchema(naptanAir, modeAir);
		}

		/// <summary>
		/// Wrapper for the Gis Query FindNearestITNs
		/// </summary>
		/// <param name="x">x coordonate</param>
		/// <param name="y">y coordonate</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestITNs(double x, double y)
		{
			return new QuerySchema();
		}

		/// <summary>
		/// Wrapper for the Gis Query FindNearestStopsAndITNs
		/// </summary>
		/// <param name="x">x coordonate</param>
		/// <param name="y">y coordonate</param>
		/// <param name="maxDistance">max Walking distance</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindNearestStopsAndITNs(double x, double y, int maxDistance)
		{
			return new QuerySchema();
		}

		/// <summary>
		/// Wrapper for the Gis Query FindStopsInGroupForStops
		/// </summary>
		/// <param name="naptanIDs">naptan IDs</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindStopsInGroupForStops(string[] naptanIDs)
		{
			if (group1.Contains(naptanIDs[0]))
			{
				return qs1;
			} 
			else if (group2.Contains(naptanIDs[0]))
			{
				return qs2;
			}
			else if (group3.Contains(naptanIDs[0]))
			{
				return qs3;
			}
			else if (group4.Contains(naptanIDs[0]))
			{
				return qs4;
			}
			else 
			{
				return qs5;
			}
		}
		
		/// <summary>
		/// Wrapper for the Gis Query FindStopsInGroupForStops
		/// </summary>
		/// <param name="schema">An existing QuerySchema to be augmented with Naptan Groups</param>
		/// <returns>returns a QuerySchema representing the GIS query result</returns>
		public QuerySchema FindStopsInGroupForStops(QuerySchema schema)
		{
			return new QuerySchema();
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
			return new QuerySchema();
		}

		/// <summary>
		/// Wrapper for the Gis Query FindStopsInRadius
		/// </summary>
		/// <param name="x">easting</param>
		/// <param name="y">northing</param>
		/// <param name="searchDistance">radius of circle to be searched</param>
		/// <returns>returns a QuerySchema containing all Naptans within the specified
		/// radius of the given point, and any naptans in the same groups as those found</returns>
		public QuerySchema FindStopsInRadius(double x, double y, int searchDistance, string[] stopTypes)
		{
			return new QuerySchema();
		}


		/// <summary>
		/// Wrapper for the Gis Query FindStopsInfoForStops
		/// </summary>
		/// <param name="naptanIDs">naptan IDs</param>
		public QuerySchema FindStopsInfoForStops(string[] naptanIDs)
		{
			return new QuerySchema();
		}

		/// <summary>
		/// Wrapper for the Gis Query FindExchangePointsInRadius
		/// </summary>
		/// <param name="naptanIDs">naptan IDs</param>
		public ExchangePointSchema FindExchangePointsInRadius(int x, int y, int radius, string mode, int maximum)
		{
			return new ExchangePointSchema();
		}

		/// <summary>
		/// Wrapper for the Gis Query FindNearestLocality
		/// </summary>
		/// <param name="x">easting</param>
		/// <param name="x">northing</param>
		/// <returns>returns a QuerySchema containing stop info for the supplied naptans</returns>
		public string FindNearestLocality(double x, double y)
		{
			return string.Empty;
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
			return new QuerySchema();
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
        /// <returns></returns>
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
		///  Method used by ServiceDiscovery to get an
		///  instance of the TestStubGisQuery class.
		/// </summary>
		/// <returns>A new instance of a TestStubGisQuery.</returns>
		public Object Get()
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
