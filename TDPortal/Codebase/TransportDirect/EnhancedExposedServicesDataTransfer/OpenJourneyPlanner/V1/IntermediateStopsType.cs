// *********************************************** 
// NAME                 : IntermediateStopsType.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents the type of intermediate stop
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/IntermediateStopsType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:38   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:20:30   COwczarek
//Initial revision.
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1

{

    /// <summary>
    /// Enumerator that represents the type of intermediate stop
    /// </summary>
    [Serializable]
    public enum IntermediateStopsType
    {
        None,         // No stops required.
        Before,       // Stops between the service origin and leg board.
        BeforeAndLeg, // Stops between the service origin and leg alight.
        Leg,          // Stops between the leg board and leg alight.
        LegAndAfter,  // Stops between the leg board and service destination.
        After,        // Stops between the leg alight and service destination.
        All           // All stops between service origin and service destination.
    }
 
}