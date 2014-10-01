// *********************************************** 
// NAME			: IAirDataProvider.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 12/05/2004
// DESCRIPTION	: Interface for providing airport/air region/operator data to clients
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/IAirDataProvider.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:24   mturner
//Initial revision.
//
//   Rev 1.6   Mar 16 2007 10:00:52   build
//Automatically merged from branch for stream4362
//
//   Rev 1.5.1.0   Mar 15 2007 18:09:46   dsawe
//added methods for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.5   Jul 08 2004 14:11:32   jgeorge
//Actioned review comments
//
//   Rev 1.4   May 26 2004 10:42:46   jgeorge
//Work in progress
//
//   Rev 1.3   May 26 2004 08:53:20   jgeorge
//Work in progress
//
//   Rev 1.2   May 13 2004 11:31:36   jgeorge
//Updates for Naptans and commenting
//
//   Rev 1.1   May 13 2004 09:28:16   jgeorge
//Modified namespace to TransportDirect.UserPortal.AirDataProvider
//
//   Rev 1.0   May 12 2004 15:59:54   jgeorge
//Initial revision.

using System;
using System.Collections;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Summary description for IAirDataProvider.
	/// </summary>
	public interface IAirDataProvider
	{
		// Methods for retrieving lists of all data
		ArrayList GetAirports(); // ArrayList of Airport
		ArrayList GetRegions(); // ArrayList of AirRegion
		ArrayList GetRoutes(); // ArrayList of AirRoute
		ArrayList GetOperators(); // ArrayList of AirOperator

		// Methods for retrieving an object from its code
		Airport GetAirport(string airportCode);
		AirRegion GetRegion(int regionCode);
		AirOperator GetOperator(string operatorCode);
		
		//gets the Airport operators for local zonal services
		ArrayList GetLocalZonalAirportOperators(string AirportNaptan);

		// Retrieve all airports in a given region
		ArrayList GetRegionAirports(int regionCode); // ArrayList of Airport
		ArrayList GetAirportRegions(string airportCode); // ArrayList of AirRegion

		// Retrieve airports from a list of naptans
		ArrayList GetAirportsFromNaptans(string[] naptans);
		Airport GetAirportFromNaptan(string naptan);

		// The following methods are for retrieving data for and validating direct
		// flights only. None of the below apply to indirect flights, as the routes
		// are worked out by the CJP.
				
		// Retrieve valid destination airports from given origin region(s) or airport(s)
		ArrayList GetValidDestinationAirports(AirRegion originRegion); // ArrayList of Airport
		ArrayList GetValidDestinationAirports(Airport[] originAirports); // ArrayList of Airport

		// Retrieve valid destination regions from givin origin region(s) or aiports(s)
		ArrayList GetValidDestinationRegions(AirRegion originRegion); // ArrayList of AirRegion
		ArrayList GetValidDestinationRegions(Airport[] originAirports); // ArrayList of AirRegion

		// Retrieve valid origin airports from given destination region(s) or airport(s)
		ArrayList GetValidOriginAirports(AirRegion destinationRegion); // ArrayList of Airport
		ArrayList GetValidOriginAirports(Airport[] destinationAirports); // ArrayList of Airport

		// Retrieve valid origin regions from given destination region(s) or airport(s)
		ArrayList GetValidOriginRegions(AirRegion destinationAirports); // ArrayList of AirRegion
		ArrayList GetValidOriginRegions(Airport[] destinationAirports); // ArrayList of AirRegion

		// Retrieve valid operators for an airport code. There is no need to distinguish between
		// origin and destination airports/regions as it is assumed all routes work in both directions.
		ArrayList GetAirportOperators(AirRegion targetRegion); // ArrayList of AirOperator
		ArrayList GetAirportOperators(Airport[] targetAirports); // ArrayList of AirOperator

		// Retrieve valid operators for routes between given origin(s) and destination(s)
		ArrayList GetRouteOperators(Airport[] originAirports, Airport[] destinationAirports); 
		ArrayList GetRouteOperators(AirRoute[] routes); 
		// ArrayList of AirOperator

		// Return valid routes between given origin(s)/destination(s)
		ArrayList GetValidRoutes(Airport[] originAirports, Airport[] destinationAirports);
		ArrayList GetValidRoutes(AirRegion originRegion, AirRegion destinationRegion);
		ArrayList GetValidRoutes(AirRegion originRegion, Airport[] destinationAirports);
		ArrayList GetValidRoutes(Airport[] originAirports, AirRegion destinationRegion);

		// Check that given origin(s)/destination(s) constitute valid route(s)
		bool ValidRouteExists(string[] originAirportCodes, string[] destinationAirportCodes);
		bool ValidRouteExists(int originRegionCode, int destinationRegionCode);
		bool ValidRouteExists(int originRegionCode, string[] destinationAirportCodes);
		bool ValidRouteExists(string[] originAirportsCode, int destinationRegionCode);

		// Returns true if the JavaScript declarations file has been generated and registed
		// with the ScriptRepository
		bool ScriptGenerated { get; }
	}
}
