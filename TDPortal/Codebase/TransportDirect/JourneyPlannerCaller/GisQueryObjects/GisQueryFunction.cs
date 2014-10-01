using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyPlannerCaller.GisQueryObjects
{
    public enum GisQueryFunction
    {
        FindNearestStops,
        FindNearestITNs,
        FindNearestStopsAndITNs,
        FindStopsInGroupForStops,
        FindStopsInRadius,
        FindStopsInfoForStops,
        FindNearestLocality,
        FindNearestPointOnTOID,
        FindExchangePointsInRadius,
        FindNearestITN,
        IsPointsInCycleDataArea,
        IsPointsInWalkDataArea,
        GetStreetsFromPostCode,
        FindNearestCarParks

    }
}
