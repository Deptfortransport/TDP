// *********************************************** 
// NAME                 : RangeType.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents whether a journey plan is required that returns a 
// specific number of journeys or for a time interval
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/RangeType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:42   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:52   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// Enumerator that represents whether a journey plan is required that returns a specific number of
    /// journeys or for a time interval
    /// </summary>
    [Serializable]
    public enum RangeType
    {
        Sequence, // Return up to the specified number of journey plans.
        Interval  // Return all journeys within the given interval period.
    }
 
}
