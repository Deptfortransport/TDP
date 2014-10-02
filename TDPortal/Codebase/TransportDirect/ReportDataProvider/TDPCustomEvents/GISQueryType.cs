// *********************************************** 
// NAME                 : GISQueryType.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 14/01/2013
// DESCRIPTION          : Defines types for GIS Query events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/GISQueryType.cs-arc  $
//
//   Rev 1.0   Jan 14 2013 14:42:34   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// Enumeration containing classifiers for <c>GISQueryEvent</c>.
    /// </summary>
    public enum GISQueryType : int
    {
        Unknown = 0,
        FindNearestStops,
        FindNearestLocality,
        FindNearestITN,
        FindNearestITNs,
        FindNearestPointOnTOID,
        FindNearestCarParks,
        FindNearestAccessibleStops,
        FindNearestAccessibleLocalities,
        FindNearestStopsAndITNs,
        FindExchangePointsInRadius,
        FindStopsInRadius,
        FindStopsInGroupForStops,
        FindStopsInfoForStops,
        IsPointsInCycleDataArea,
        IsPointsInWalkDataArea,
        IsPointInAccessibleLocation,
        GetExchangeInfoForNaptan,
        GetNPTGInfoForNaPTAN,
        GetLocalityInfoForNatGazID,
        GetStreetsFromPostcode,
        GetDistancesForTOIDs
    }
}
