// *********************************************** 
// NAME                 : IGisQuery.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 03/09/2003 
// DESCRIPTION  : Interface for the GisQuery class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/IGisQuery.cs-arc  $ 
//
//   Rev 1.5   Jan 09 2013 11:40:24   mmodi
//GIS query updates for accessible searches
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.4   Nov 11 2009 16:42:44   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.3   Sep 25 2009 14:16:34   mmodi
//Updated with new ESRI method GetDistancesForTOIDs
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Sep 10 2009 11:17:58   mturner
//Added new interface member GetLocalityInfoForNatGazID.
//
//   Rev 1.1   Oct 13 2008 16:46:16   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 14:56:24   mmodi
//Updated for new cycle planner methods
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:25:06   mturner
//Initial revision.
//
//   Rev 1.12   Mar 16 2007 10:00:56   build
//Automatically merged from branch for stream4362
//
//   Rev 1.11.1.0   Mar 09 2007 09:35:04   dsawe
//added method GetNPTGInfoForNaPTAN
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.11   Oct 06 2006 11:58:44   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.10.1.0   Aug 03 2006 13:59:38   esevern
//Added FindNearestCarParks
//
//   Rev 1.10   Mar 31 2006 18:44:40   RPhilpott
//Add support for new "GetStreetsForPostcode()" method.
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.9   Mar 21 2006 17:35:18   jmcallister
//Added new method GetExchangeInfoForNaptan. This uses new feature on td.interactivemapping.dll and allows us to resolve coach naptans.
//
//   Rev 1.8   Mar 20 2006 18:02:56   RWilby
//Merged stream0027: Start/End TOIDs changes.
//
//   Rev 1.7.1.0   Mar 14 2006 12:01:02   RWilby
//Added signature for FindNearestITN method
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.7   Sep 10 2004 15:35:44   RPhilpott
//Rewrite of FindNearestStation logic and various Gazetteer methods to make use of new ESRI query methods.
//Resolution for 1308: Find a coach - Can not find nearest coach stations
//Resolution for 1326: Find a variety of transport does not return any results
//Resolution for 1458: Train station names
//Resolution for 1534: Find a Coach - Wick to Inverness clicking NEXT does not try to get a journey
//Resolution for 1558: "Group", not terminal airport naptans used for multimodal plans.
//
//   Rev 1.6   Jul 23 2004 16:47:28   RPhilpott
//Add support for FindStopsInfoForStops().
//
//   Rev 1.5   May 28 2004 17:52:18   passuied
//update as part of FindStation development
//
//   Rev 1.4   Dec 11 2003 17:52:44   RPhilpott
//Make use of new FindStopsInRadius() method in GISQuery.
//Resolution for 281: Postcode journey doesn't return public transport journeys
//
//   Rev 1.3   Sep 22 2003 13:35:18   RPhilpott
//tidy
//
//   Rev 1.2   Sep 21 2003 18:43:44   kcheung
//Uncommented FindStopsInGroupForStops
//
//   Rev 1.1   Sep 20 2003 16:59:48   RPhilpott
//1) Add OSGR's to NaPTAN definitions and populate them
//2) Extra call to FindStopsInGroupForStops(schema) to gazetteers
//
//   Rev 1.0   Sep 05 2003 15:30:24   passuied
//Initial Revision


using System;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	///Interface for the GisQuery class
	/// </summary>
	public interface IGisQuery
	{
		QuerySchema FindNearestCarParks(double easting, double northing, int initialRadius, int maxRadius, int maxNoCarParks);
		QuerySchema FindNearestStops(double x, double y, int maxDistance);
		QuerySchema FindNearestITNs(double x, double y);
		QuerySchema FindNearestStopsAndITNs(double x, double y, int maxDistance);
        QuerySchema FindStopsInGroupForStops(string[] naptanIDs);
		QuerySchema FindStopsInGroupForStops(QuerySchema schema);
		QuerySchema FindStopsInRadius(double x, double y, int searchDistance);
		QuerySchema FindStopsInRadius(double x, double y, int searchDistance, string[] stopTypes);
		QuerySchema FindStopsInfoForStops(string[] naptanIDs);
		ExchangePointSchema GetExchangeInfoForNaptan(TDNaptan naptan);

		string[] GetStreetsFromPostCode(string postCode);

		string FindNearestLocality(double x, double y);
		
		ExchangePointSchema FindExchangePointsInRadius(int x, int y, int radius, string mode, int maximum);

		QuerySchema FindNearestITN(double x, double y,string address,bool ingoreMotorways);

        Point FindNearestPointOnTOID(double x, double y, string toid);
        bool IsPointsInCycleDataArea(Point[] point, bool sameAreaOnly);
        bool IsPointsInWalkDataArea(Point[] point, bool sameAreaOnly, out int walkitID, out string city);

        AccessibleStopInfo[] FindNearestAccessibleStops(double x, double y, int searchDistance,
            int maxResults, DateTime date, bool wheelChair, bool assistance, string[] stopTypes);
        AccessibleLocationInfo[] FindNearestAccessibleLocalities(double x, double y, int searchDistance,
            int maxResults, bool wheelChair, bool assistance);
        void IsPointInAccessibleLocation(double x, double y, int searchDistance, out bool wheelChair, out bool assistance);

		NPTGInfo GetNPTGInfoForNaPTAN(string naptanID);

        LocalityNameInfo GetLocalityInfoForNatGazID(string localityID);

        CountryDistances[] GetDistancesForTOIDs(string[] toids);
	}
}
