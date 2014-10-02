// *********************************************** 
// NAME                 : TestMockAirDataProvider.cs
// AUTHOR				: Richard Philpott
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestMockAirDataProvider.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:46   mturner
//Initial revision.
//
//   Rev 1.2   Mar 16 2007 10:00:54   build
//Automatically merged from branch for stream4362
//
//   Rev 1.1.1.0   Mar 15 2007 18:13:38   dsawe
//added methods for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.1   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures

using System;
using System.Collections;

using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Implementation of IAirDataProvider for testing purposes
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
