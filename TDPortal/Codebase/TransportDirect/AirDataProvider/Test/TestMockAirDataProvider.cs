using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Summary description for TestMockAirDataProvider.
	/// </summary>
	public class TestMockAirDataProvider : IAirDataProvider, IServiceFactory
	{
		private string dataFile = string.Empty;

		#region Constructor

		public TestMockAirDataProvider(string dataFile)
		{
			this.dataFile = dataFile;
			LoadData();
			current = this;
		}

		#endregion

		#region Implementation of IServiceFactory

		// Current instance of airdataprovider
		IAirDataProvider current;

		public object Get()
		{
			return current;
		}

		#endregion

		#region Public constants

		// Name to use when generating the JavaScript file
		public const string ScriptName = "AirDataDeclarations";

		#endregion

		#region Private variables

		private const string DataChangeNotificationGroup = "Air";

		private ArrayList airports;
		private ArrayList regions;
		private ArrayList routes;
		private ArrayList operators;
		private Hashtable regionAirports;
		private Hashtable airportRegions;
		private Hashtable routeMatrix;
		private Hashtable regionMatrix;
		private Hashtable airportOperators;
		private Hashtable routeOperators;

		/// <summary>
		/// Hastable storing operator codes used in local zonal services
		/// </summary>
		private ArrayList arrayZonalOperatorCodes = new ArrayList();

		#endregion

		#region Private methods

		/// <summary>
		/// Loads the data and performs pre processing
		/// </summary>
		private void LoadData()
		{
			lock(typeof(AirDataProvider))
			{
				// Temporary variables to hold the new data
				ArrayList newAirports;
				ArrayList newRegions;
				ArrayList newRoutes;
				ArrayList newOperators;
				Hashtable newRegionAirports;
				Hashtable newAirportRegions;
				Hashtable newRouteMatrix;
				Hashtable newRegionMatrix;
				Hashtable newAirportOperators;
				Hashtable newRouteOperators;

				// Initialise the arraylists and hashtables we need at this point.
				// The rest are left until later, as we can use data that is loaded
				// in the first stage to initialise them more appropriately.
				newAirports = new ArrayList();
				newRegions = new ArrayList();
				newRegionAirports = new Hashtable();
				newOperators = new ArrayList();
				newRoutes = new ArrayList();
				newRouteOperators = new Hashtable();

				// Initialise temporary variables
				AirRegion currRegion = null;
				//ArrayList currRegionAirports = null;
				AirRoute currRoute = null;
				AirRoute tempRoute = null;
				ArrayList currRouteOperators = null;
				ArrayList temp = null;
				AirOperator tempOperator = null;

				XmlDocument data = new XmlDocument();
				data.Load(this.dataFile);
				#region Load airports, regions, operators and routes

				foreach (XmlNode node in data.GetElementsByTagName("airport"))
				{
					Airport curr = new Airport(node.Attributes["code"].Value.Substring(4, 3), node.Attributes["name"].Value, Convert.ToInt32(node.Attributes["terminals"].Value));
					newAirports.Add(curr);
				}

				foreach (XmlNode node in data.GetElementsByTagName("airregion"))
				{
					AirRegion curr = new AirRegion(Convert.ToInt32(node.Attributes["code"].Value), node.Attributes["name"].Value);
					newRegions.Add(curr);
					newRegionAirports.Add(curr.Code, new ArrayList());
				}

				foreach (XmlNode node in data.GetElementsByTagName("regionairport"))
				{
					currRegion = this.GetRegion(Convert.ToInt32(node.Attributes["airregioncode"].Value), newRegions);
					Airport currAirport = this.GetAirport(node.Attributes["airportcode"].Value.Substring(4, 3), newAirports);
					((ArrayList)newRegionAirports[currRegion.Code]).Add(currAirport);
				}

				foreach (XmlNode node in data.GetElementsByTagName("operator"))
					newOperators.Add(new AirOperator(node.Attributes["code"].Value, node.Attributes["name"].Value));

				foreach (XmlNode node in data.GetElementsByTagName("flightroute"))
				{
					tempRoute = new AirRoute(node.Attributes["origincode"].Value.Substring(4, 3), node.Attributes["destinationcode"].Value.Substring(4, 3));
					if ( (currRoute == null) || !(currRoute.Equals(tempRoute)) )
					{
						// New route
						currRoute = tempRoute;
						// Ensure routes are added only once (remember A - B is same route as B - A)
						if (!newRoutes.Contains(currRoute))
							newRoutes.Add(currRoute);

						if (!newRouteOperators.ContainsKey(currRoute))
							newRouteOperators.Add(currRoute, new ArrayList());

						currRouteOperators = (ArrayList)newRouteOperators[currRoute];
					}
					tempOperator = GetOperator(node.Attributes["operatorcode"].Value, newOperators);
					if (!currRouteOperators.Contains(tempOperator))
						currRouteOperators.Add(tempOperator);
				}

				#endregion
					
				#region Initialise remaining hashtables

				newAirportRegions = new Hashtable(newAirports.Count);
				newRouteMatrix = new Hashtable(newAirports.Count);
				newRegionMatrix = new Hashtable(newAirports.Count);
				newAirportOperators = new Hashtable(newAirports.Count);

				foreach (Airport a in newAirports)
				{
					newAirportRegions.Add(a.IATACode, new ArrayList());
					newRouteMatrix.Add(a.IATACode, new ArrayList());
					newAirportOperators.Add(a.IATACode, new ArrayList());
				}

				foreach (AirRegion r in newRegions)
					newRegionMatrix.Add(r.Code, new ArrayList());

				#endregion

				#region Populate airportRegions hashtable

				foreach (AirRegion r in newRegions)
					foreach (Airport a in (ArrayList)newRegionAirports[r.Code])
						((ArrayList)newAirportRegions[a.IATACode]).Add(r);

				#endregion

				#region Populate routeMatrix and regionMatrix hashtables

				// routeMatrix and regionMatrix hashtables
				ArrayList tempOrigin, tempDestination;
				ArrayList matrixEntryCurr;
				foreach (AirRoute r in newRoutes)
				{
					((ArrayList)newRouteMatrix[r.OriginAirport]).Add(GetAirport(r.DestinationAirport, newAirports));
					((ArrayList)newRouteMatrix[r.DestinationAirport]).Add(GetAirport(r.OriginAirport, newAirports));
					
					tempOrigin = (ArrayList)newAirportRegions[r.OriginAirport];
					tempDestination = (ArrayList)newAirportRegions[r.DestinationAirport];
					foreach (AirRegion r1 in tempOrigin)
						foreach (AirRegion r2 in tempDestination)
						{
							matrixEntryCurr = (ArrayList)newRegionMatrix[r1.Code];
							if (!matrixEntryCurr.Contains(r2))
								matrixEntryCurr.Add(r2);
							matrixEntryCurr = (ArrayList)newRegionMatrix[r2.Code];
							if (!matrixEntryCurr.Contains(r1))
								matrixEntryCurr.Add(r1);
						}
				}					

				#endregion

				#region Populate airportOperators hashtable

				foreach (AirRoute r in newRoutes)
					foreach (AirOperator o in (ArrayList)newRouteOperators[r])
					{
						temp = (ArrayList)newAirportOperators[r.OriginAirport];
						if (!temp.Contains(o))
							temp.Add(o);

						temp = (ArrayList)newAirportOperators[r.DestinationAirport];
						if (!temp.Contains(o))
							temp.Add(o);
					}

				#endregion			
				
				this.airports = newAirports;
				this.regions = newRegions;
				this.routes = newRoutes;
				this.operators = newOperators;
				this.regionAirports = newRegionAirports;
				this.airportRegions = newAirportRegions;
				this.routeMatrix = newRouteMatrix;
				this.regionMatrix = newRegionMatrix;
				this.airportOperators = newAirportOperators;
				this.routeOperators = newRouteOperators;

			}
		}

		/// <summary>
		/// Given an array of ArrayLists, returns an ArrayList contain all the elements in 
		/// all the ArrayLists. No duplicate items are added.
		/// </summary>
		/// <param name="arrays"></param>
		/// <returns></returns>
		private ArrayList ArrayListUnion(ArrayList[] arrays)
		{
			ArrayList results;
			if ((arrays.Length == 0) || (arrays[0] == null))
				results = new ArrayList();
			else
				results = arrays[0];

			for (int index = 1; index < arrays.Length; index++)
				if (arrays[index] != null)
					foreach (object o in arrays[index])
						if (!results.Contains(o))
							results.Add(o);

			return results;
		}

		/// <summary>
		/// Converts an array of airports into a string array of their
		/// IATACode properties.
		/// </summary>
		/// <param name="airports"></param>
		/// <returns></returns>
		private string[] AirportArrayToIATACodeArray(Airport[] airports)
		{
			string[] results = new string[airports.Length];
			for (int index = 0; index < airports.Length; index++)
				results[index] = airports[index].IATACode;
			return results;
		}

		/// <summary>
		/// Builds an ArrayList containing route objects representing all potential routes between
		/// the origin and destination airports. These can then be used to check against the list
		/// of valid routes.
		/// </summary>
		/// <param name="originAirports"></param>
		/// <param name="destinationAirports"></param>
		/// <returns></returns>
		private ArrayList BuildPotentialRouteList(Airport[] originAirports, Airport[] destinationAirports)
		{
			ArrayList results = new ArrayList(originAirports.Length * destinationAirports.Length);
			AirRoute route;
			foreach (Airport origin in originAirports)
				foreach (Airport destination in destinationAirports)
				{
					route = new AirRoute(origin.IATACode, destination.IATACode);
					if (!results.Contains(route))
						results.Add(route);
				}
			return results;
		}

		/// <summary>
		/// Builds an ArrayList containing route objects representing all potential routes between
		/// the origin and destination airports. These can then be used to check against the list
		/// of valid routes.
		/// </summary>
		/// <param name="originAirportCodes">Airport IATA Codes</param>
		/// <param name="destinationAirportCodes">Airport IATA Codes</param>
		/// <returns></returns>
		private ArrayList BuildPotentialRouteList(string[] originAirportCodes, string[] destinationAirportCodes)
		{
			ArrayList results = new ArrayList(originAirportCodes.Length * destinationAirportCodes.Length);
			AirRoute route;
			foreach (string origin in originAirportCodes)
				foreach (string destination in destinationAirportCodes)
				{
					route = new AirRoute(origin, destination);
					if (!results.Contains(route))
						results.Add(route);
				}
			return results;
		}

		#endregion

		#region Implementation of IAirDataProvider

		/// <summary>
		/// Returns all Airports
		/// </summary>
		/// <returns></returns>
		public ArrayList GetAirports()
		{
			return (ArrayList)airports.Clone();
		}

		/// <summary>
		/// Returns all AirRegions
		/// </summary>
		/// <returns></returns>
		public ArrayList GetRegions()
		{
			return (ArrayList)regions.Clone();
		}

		/// <summary>
		/// Returns all AirRoutes
		/// </summary>
		/// <returns></returns>
		public ArrayList GetRoutes()
		{
			return (ArrayList)routes.Clone();
		}

		/// <summary>
		/// Returns all AirOperators
		/// </summary>
		/// <returns></returns>
		public ArrayList GetOperators()
		{
			return (ArrayList)operators.Clone();
		}

		/// <summary>
		/// Retrieve an airport object given its IATA code.
		/// Null is returned if the airport is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		public Airport GetAirport(string airportCode)
		{
			return GetAirport(airportCode, this.airports);
		}

		/// <summary>
		/// Retrieve an airport object from the supplied Arraylist 
		/// given its IATA code.
		/// Null is returned if the airport is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		private Airport GetAirport(string airportCode, ArrayList airportsToSearch)
		{
			foreach (Airport a in airportsToSearch)
				if (a.IATACode == airportCode)
					return a;

			return null;
		}

		/// <summary>
		/// Retrieve a region object given its code.
		/// Null is returned if the region is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		public AirRegion GetRegion(int regionCode)
		{
			return GetRegion(regionCode, this.regions);
		}

		/// <summary>
		/// Retrieve a region object from the supplied ArrayList
		/// given its region code.
		/// Null is returned if the region is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		private AirRegion GetRegion(int regionCode, ArrayList regionsToSearch)
		{
			foreach (AirRegion r in regionsToSearch)
				if (r.Code == regionCode)
					return r;

			return null;
		}

		/// <summary>
		/// Method used to get Airport Operator Codes for Local Zonal services
		/// </summary>
		/// <param name="AirportNaptan">Naptan Code for Airport</param>
		/// <returns></returns>
		public ArrayList GetLocalZonalAirportOperators(string AirportNaptan)
		{
			return arrayZonalOperatorCodes;
		}

		/// <summary>
		/// Retrieve an airoperator object given its IATA code.
		/// Null is returned if the operator is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		public AirOperator GetOperator(string operatorCode)
		{
			return GetOperator(operatorCode, this.operators);
		}

		/// <summary>
		/// Retrieve an airoperator object from the supplied ArrayList
		/// given its IATA code.
		/// Null is returned if the operator is not found.
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		private AirOperator GetOperator(string operatorCode, ArrayList operatorsToSearch)
		{
			foreach (AirOperator o in operatorsToSearch)
				if (o.IATACode == operatorCode)
					return o;

			return null;
		}

		/// <summary>
		/// Retrieve all airports in a given region
		/// </summary>
		/// <param name="regionCode"></param>
		/// <returns></returns>
		public ArrayList GetRegionAirports(int regionCode)
		{
			return (ArrayList)((ArrayList)regionAirports[regionCode]).Clone();
		}

		/// <summary>
		/// Retrieve all regions for an airport
		/// </summary>
		/// <param name="airportCode"></param>
		/// <returns></returns>
		public ArrayList GetAirportRegions(string airportCode)
		{
			return (ArrayList)((ArrayList)airportRegions[airportCode]).Clone();
		}

		/// <summary>
		/// Retrieve airports from a list of naptans 
		/// </summary>
		/// <param name="naptans"></param>
		/// <returns></returns>
		public ArrayList GetAirportsFromNaptans(string[] naptans)
		{
			Airport current;
			ArrayList results = new ArrayList();
			foreach (string s in naptans)
			{
				current = GetAirportFromNaptan(s);
				if ( (current != null) && (!results.Contains(current)) )
					results.Add(current);
			}
			return results;
		}

		/// <summary>
		/// Retrieve a single airport from a naptan
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public Airport GetAirportFromNaptan(string naptan)
		{
			// Verify that the naptan is the expected length
			if (naptan.Length < Airport.NaptanPrefix.Length + 3)
				return null;
			else
				return GetAirport(naptan.Substring(Airport.NaptanPrefix.Length, 3));
		}

		/* The following methods are for retrieving data for and validating direct
		 * flights only. None of the below apply to indirect flights, as the routes
		 * are worked out by the CJP.
		 */

		/// <summary>
		///  Retrieve valid destination airports from given origin region
		/// </summary>
		/// <param name="originRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidDestinationAirports(AirRegion originRegion)
		{
			ArrayList airports = GetRegionAirports(originRegion.Code);
			return GetValidDestinationAirports( (Airport[]) airports.ToArray(typeof(Airport)) );
		}

		/// <summary>
		///  Retrieve valid destination airports from given origin airports
		/// </summary>
		/// <param name="originRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidDestinationAirports(Airport[] originAirport)
		{
			ArrayList[] currDests = new ArrayList[originAirport.Length];
			for (int index = 0; index < originAirport.Length; index++)
				currDests[index] = (ArrayList)routeMatrix[originAirport[index].IATACode];

			return ArrayListUnion(currDests);
		}

		/// <summary>
		/// Retrieve valid destination regions from given origin region
		/// </summary>
		/// <param name="originRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidDestinationRegions(AirRegion originRegion)
		{
			ArrayList airports = GetRegionAirports(originRegion.Code);
			return GetValidDestinationRegions( (Airport[]) airports.ToArray(typeof(Airport)) );
		}

		/// <summary>
		/// Retrieve valid destination regions from given origin airports
		/// </summary>
		/// <param name="originAirport"></param>
		/// <returns></returns>
		public ArrayList GetValidDestinationRegions(Airport[] originAirport)
		{
			ArrayList validDestinationAirports = GetValidDestinationAirports(originAirport);
			ArrayList[] currDestRegions = new ArrayList[validDestinationAirports.Count];
			for (int index = 0; index < validDestinationAirports.Count; index++)
				currDestRegions[index] = (ArrayList)airportRegions[((Airport)validDestinationAirports[index]).IATACode];
			return ArrayListUnion(currDestRegions);
		}


		/// <summary>
		/// Retrieve valid origin airports from given destination region(s) or airport(s)
		/// </summary>
		/// <param name="destinationRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidOriginAirports(AirRegion destinationRegion)
		{
			return GetValidDestinationAirports(destinationRegion);
		}

		/// <summary>
		/// Retrieve valid origin airports from given destination region(s) or airport(s)
		/// </summary>
		/// <param name="destinationAirports"></param>
		/// <returns></returns>
		public ArrayList GetValidOriginAirports(Airport[] destinationAirports)
		{
			return GetValidDestinationAirports(destinationAirports);
		}

		/// <summary>
		/// Retrieve valid origin airports from given destination region(s) or airport(s)
		/// </summary>
		/// <param name="destinationRegion"></param>
		/// <returns></returns>
		public ArrayList GetValidOriginRegions(AirRegion destinationRegion)
		{
			return GetValidDestinationRegions(destinationRegion);
		}

		/// <summary>
		/// Retrieve valid origin airports from given destination region(s) or airport(s)
		/// </summary>
		/// <param name="destinationAirports"></param>
		/// <returns></returns>
		public ArrayList GetValidOriginRegions(Airport[] destinationAirports)
		{
			return GetValidDestinationRegions(destinationAirports);
		}


		/// <summary>
		/// Retrieve valid operators for an air region.
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		public ArrayList GetAirportOperators(AirRegion region)
		{
			return GetAirportOperators( (Airport[])((ArrayList)regionAirports[region.Code]).ToArray(typeof(Airport)) );
		}

		/// <summary>
		/// Retrieve valid operators for a list of airports.
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		public ArrayList GetAirportOperators(Airport[] airport)
		{
			ArrayList[] results = new ArrayList[airport.Length];
			for (int index = 0; index < airport.Length; index++)
				results[index] = (ArrayList)airportOperators[airport[index].IATACode];

			return ArrayListUnion(results);
		}

		/// <summary>
		/// Retrieve valid operators for routes between given origin(s) and destination(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetRouteOperators(Airport[] originAirportCodes, Airport[] destinationAirportCodes)
		{
			ArrayList validRoutes = GetValidRoutes(originAirportCodes, destinationAirportCodes);
			return GetRouteOperators( (AirRoute[])validRoutes.ToArray(typeof(AirRoute)) );
		}
		
		/// <summary>
		/// Retrieve valid operators for routes between given origin(s) and destination(s)
		/// </summary>
		/// <param name="routes"></param>
		/// <returns></returns>
		public ArrayList GetRouteOperators(AirRoute[] routes)
		{
			ArrayList[] results = new ArrayList[routes.Length];
			for (int index = 0; index < routes.Length; index++)
				results[index] = (ArrayList)routeOperators[routes[index]];
			return ArrayListUnion(results);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetValidRoutes(Airport[] originAirports, Airport[] destinationAirports)
		{
			// Build a list of all possible routes
			ArrayList possibleRoutes = BuildPotentialRouteList(originAirports, destinationAirports);
			ArrayList validRoutes = new ArrayList(possibleRoutes.Count);
			foreach (AirRoute r in possibleRoutes)
				if (!validRoutes.Contains(r) &&  routes.Contains(r))
					validRoutes.Add(r);

			return validRoutes;
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetValidRoutes(AirRegion originRegion, AirRegion destinationRegion)
		{
			return GetValidRoutes( 
				(Airport[])GetRegionAirports(originRegion.Code).ToArray(typeof(Airport)), 
				(Airport[])GetRegionAirports(destinationRegion.Code).ToArray(typeof(Airport))
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetValidRoutes(AirRegion originRegion, Airport[] destinationAirports)
		{
			return GetValidRoutes( 
				(Airport[])GetRegionAirports(originRegion.Code).ToArray(typeof(Airport)), 
				destinationAirports
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public ArrayList GetValidRoutes(Airport[] originAirports, AirRegion destinationRegion)
		{
			return GetValidRoutes( 
				originAirports, 
				(Airport[])GetRegionAirports(destinationRegion.Code).ToArray(typeof(Airport))
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportCodes"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public bool ValidRouteExists(string[] originAirportCodes, string[] destinationAirportCodes)
		{
			// Build a list of all possible routes
			ArrayList possibleRoutes = BuildPotentialRouteList(originAirportCodes, destinationAirportCodes);
			foreach (AirRoute r in possibleRoutes)
				if (routes.Contains(r))
					return true;

			return false;
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originRegionCode"></param>
		/// <param name="destinationRegionCode"></param>
		/// <returns></returns>
		public bool ValidRouteExists(int originRegionCode, int destinationRegionCode)
		{
			return ValidRouteExists( 
				AirportArrayToIATACodeArray((Airport[])GetRegionAirports(originRegionCode).ToArray(typeof(Airport))), 
				AirportArrayToIATACodeArray((Airport[])GetRegionAirports(destinationRegionCode).ToArray(typeof(Airport)))
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originRegionCode"></param>
		/// <param name="destinationAirportCodes"></param>
		/// <returns></returns>
		public bool ValidRouteExists(int originRegionCode, string[] destinationAirportCodes)
		{
			return ValidRouteExists( 
				AirportArrayToIATACodeArray((Airport[])GetRegionAirports(originRegionCode).ToArray(typeof(Airport))), 
				destinationAirportCodes
				);
		}

		/// <summary>
		/// Check that given origin(s)/destination(s) constitute valid route(s)
		/// </summary>
		/// <param name="originAirportsCode"></param>
		/// <param name="destinationRegionCode"></param>
		/// <returns></returns>
		public bool ValidRouteExists(string[] originAirportsCode, int destinationRegionCode)
		{
			return ValidRouteExists( 
				originAirportsCode, 
				AirportArrayToIATACodeArray((Airport[])GetRegionAirports(destinationRegionCode).ToArray(typeof(Airport)))
				);
		}

		/// <summary>
		/// Returns true if a JavaScript declarations file has been generated for the current
		/// data set.
		/// </summary>
		public bool ScriptGenerated
		{
			get { return false; }
		}

		#endregion

	}
}
