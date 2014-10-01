// *********************************************** 
// NAME             : GISQueryType.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Defines types for GIS Query events
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Reporting.Events
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
