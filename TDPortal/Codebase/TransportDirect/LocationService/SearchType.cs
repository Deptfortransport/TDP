// *********************************************** 
// NAME			: SearchType.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the SearchType enumeration
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/SearchType.cs-arc  $
//
//   Rev 1.2   Aug 28 2012 10:19:56   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.1   Feb 10 2010 16:57:42   RBroddle
//Added International to the enum
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Nov 08 2007 12:25:20   mturner
//Initial revision.
//
//   Rev 1.5   Oct 06 2006 12:49:36   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.4.1.0   Aug 07 2006 10:49:04   esevern
//added car park search type
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Mar 23 2006 17:58:32   build
//Automatically merged from branch for stream0025
//
//   Rev 1.3.1.2   Mar 14 2006 10:30:52   halkatib
//Changes made for park and ride phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.3.1.1   Mar 10 2006 16:46:26   halkatib
//changes for park and ride phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.3.1.0   Mar 07 2006 11:12:06   halkatib
//Changes made by ParkandRide phase 2
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.3   Nov 01 2004 15:40:46   jgeorge
//Added "City" search type (Del 6.3)
//
//   Rev 1.2   Jul 09 2004 13:09:16   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.1   Mar 09 2004 15:51:28   CHosegood
//Changes for "Select from map"
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.0   Sep 05 2003 15:30:38   passuied
//Initial Revision
//
//   Rev 1.1   Aug 20 2003 17:55:52   AToner
//Work in progress
using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for SearchType.
	/// </summary>
	public enum SearchType
	{
        AddressPostCode,
		MainStationAirport,
		AllStationStops,
		Locality,
		POI,
        Map,
		FindNearest,
		City,
		ParkAndRide,
		CarPark,
        International
	};

    /// <summary>
    /// Static helper class for SearchType
    /// </summary>
    public static class SearchTypeHelper
    {
        /// <summary>
        /// Method to return the SearchType for a TDStopType
        /// </summary>
        /// <param name="type">TDStopType</param>
        /// <returns>SearchType enum value</returns>
        public static SearchType GetSearchType(TDStopType type)
        {
            switch (type)
            {
                case (TDStopType.Air): return SearchType.MainStationAirport;
                case (TDStopType.Coach): return SearchType.MainStationAirport;
                case (TDStopType.Group): return SearchType.MainStationAirport; // As there is no Group search type
                case (TDStopType.Ferry): return SearchType.MainStationAirport;
                case (TDStopType.Locality): return SearchType.Locality;
                case (TDStopType.Rail): return SearchType.MainStationAirport;
                case (TDStopType.LightRail): return SearchType.MainStationAirport;
                case (TDStopType.POI): return SearchType.POI;
                // Any other strings are ignored and the default returned
            }

            return SearchType.MainStationAirport;
        }
    }

}
