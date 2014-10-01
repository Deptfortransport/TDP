// *********************************************** 
// NAME                 : PublicAlgorithmType.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents the type of journey planning required
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/PublicAlgorithmType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:40   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:50   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// Enumerator that represents the type of journey planning required
    /// </summary>
    [Serializable]
    public enum PublicAlgorithmType
    {
        Default,     // TBS ...
        Fastest,     // Fastest journeys irrespective of number of changes.
        NoChanges,   // Journeys with no changes only.
        Max1Change,  // Journeys with 1 change only.
        Max2Changes, // Journeys with 2 change only.
        Max3Changes  // Journeys with 3 change only.
    }
 
}