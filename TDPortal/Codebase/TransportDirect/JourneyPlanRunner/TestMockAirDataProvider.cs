using System;
using System.Collections;

using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for TestMockAirDataProvider.
	/// </summary>
	public class TestMockAirDataProvider : IAirDataProvider, IServiceFactory
	{
		
		ArrayList operatorList1;	
		ArrayList operatorList2;	
		
		public object Get()
		{
			return this;
		}
		
		public TestMockAirDataProvider()
		{
			operatorList1 = new ArrayList(4);

			AirOperator ao = new AirOperator("BA", "BA");
			operatorList1.Add(ao);
			ao = new AirOperator("RA", "RA");
			operatorList1.Add(ao);
			ao = new AirOperator("LH", "LH");
			operatorList1.Add(ao);
			ao = new AirOperator("SA", "SA");
			operatorList1.Add(ao);

			operatorList2 = new ArrayList(1);
			ao = new AirOperator("BA", "BA");
			operatorList2.Add(ao);

		}

		public ArrayList GetAirports()
		{
			return new ArrayList(0);
		} 
		
		public ArrayList GetRegions() 
		{
			return new ArrayList(0);
		} 
		
		public ArrayList GetRoutes() 
		{
			return new ArrayList(0);
		} 
		
		public ArrayList GetOperators()
		{
			return new ArrayList(0);
		} 

		//gets the Airport operators for local zonal services
		public ArrayList GetLocalZonalAirportOperators(string AirportNaptan)
		{
			return new ArrayList(0);
		}
		
		public Airport GetAirport(string airportCode)
		{
			return new Airport("LHR", "Heathrow", 4); 
		}

		public AirRegion GetRegion(int regionCode)
		{
			return new AirRegion(123, "London");
		}

		public AirOperator GetOperator(string operatorCode)
		{
			switch (operatorCode)
			{
				case "BA": return (AirOperator) operatorList1[0];
				case "RA": return (AirOperator) operatorList1[1];
				case "LH": return (AirOperator) operatorList1[2];
				case "SA": return (AirOperator) operatorList1[3];
				case "XX": return new AirOperator("XX", "XX");
				case "YY": return new AirOperator("YY", "YY");
				case "ZZ": return new AirOperator("ZZ", "ZZ");
			}

			return null;
		}

		public ArrayList GetRegionAirports(int regionCode)
		{
			return new ArrayList(0);
		}

		public ArrayList GetAirportRegions(string airportCode)
		{
			return new ArrayList(0);
		}

		public ArrayList GetAirportsFromNaptans(string[] naptans)
		{
			return new ArrayList(0);
		}
		
		public Airport GetAirportFromNaptan(string naptan)
		{
			
			if	(naptan == "9200LHR")
			{
				return new Airport("LHR", "Heathrow", 4); 
			}
			else if (naptan == "9200EDB")
			{
				return new Airport("EDB", "Edinburgh", 1); 
			}
			else if (naptan == "9200NCL")
			{
				return new Airport("NCL", "Newcastle", 1);
			}
			else
			{
				return null;
			}
		}

		public ArrayList GetValidDestinationAirports(AirRegion originRegion)
		{
			return new ArrayList(0);
		}

		public ArrayList GetValidDestinationAirports(Airport[] originAirports)
		{
			return new ArrayList(0);
		}

		public ArrayList GetValidDestinationRegions(AirRegion originRegion)
		{
			return new ArrayList(0);
		}

		public ArrayList GetValidDestinationRegions(Airport[] originAirports)
		{
			return new ArrayList(0);
		}

		public ArrayList GetValidOriginAirports(AirRegion destinationRegion)
		{
			return new ArrayList(0);
		}

		public ArrayList GetValidOriginAirports(Airport[] destinationAirports)
		{
			return new ArrayList(0);
		}

		public ArrayList GetValidOriginRegions(AirRegion destinationAirports)
		{
			return new ArrayList(0);
		}

		public ArrayList GetValidOriginRegions(Airport[] destinationAirports)
		{
			return new ArrayList(0);
		}

		public ArrayList GetAirportOperators(AirRegion targetRegion)
		{
			return new ArrayList(0);
		}

		public ArrayList GetAirportOperators(Airport[] targetAirports)
		{
			return new ArrayList(0);
		}

		public ArrayList GetRouteOperators(Airport[] originAirports, Airport[] destinationAirports)
		{
			return new ArrayList(0);
		}

		public ArrayList GetRouteOperators(AirRoute[] routes)
		{
			
			ArrayList list = new ArrayList();

			if	(routes != null || routes.Length > 0) 
			{
				if	(routes[0].OriginAirport == "LHR")
				{
					return operatorList1;
				}
				
				if	(routes[0].OriginAirport == "EDB")
				{
					return operatorList2;
				}
			}
			
			return new ArrayList();
		}


		public ArrayList GetValidRoutes(Airport[] originAirports, Airport[] destinationAirports)
		{
			ArrayList list = new ArrayList();

			if	(originAirports[0].IATACode == "LHR")
			{
				AirRoute ar = new AirRoute("LHR", "EDB"); 
				list.Add(ar);
				ar = new AirRoute("LHR", "LGW"); 
				list.Add(ar);
				ar = new AirRoute("LHR", "GLG"); 
				list.Add(ar);
				return list;			
			}

			if	(originAirports[0].IATACode == "EDB")
			{
				AirRoute ar = new AirRoute("EDB", "LHR"); 
				list.Add(ar);
				return list;			
			}

			return list;			// return empty list
		}

		public ArrayList GetValidRoutes(AirRegion originRegion, AirRegion destinationRegion)
		{
			return new ArrayList(0);
		}

		public ArrayList GetValidRoutes(AirRegion originRegion, Airport[] destinationAirports)
		{
			return new ArrayList(0);
		}

		public ArrayList GetValidRoutes(Airport[] originAirports, AirRegion destinationRegion)
		{
			return new ArrayList(0);
		}

		public bool ValidRouteExists(string[] originAirportCodes, string[] destinationAirportCodes)
		{
			return true;
		}	

		public bool ValidRouteExists(int originRegionCode, int destinationRegionCode)
		{
			return true;
		}

		public bool ValidRouteExists(int originRegionCode, string[] destinationAirportCodes)
		{
			return true;
		}

		public bool ValidRouteExists(string[] originAirportsCode, int destinationRegionCode)		
		{
			return true;
		}

		public bool ScriptGenerated
		{
			get { return true; }
		}


	}
}
