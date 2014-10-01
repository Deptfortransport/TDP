// *********************************************** 
// NAME                 : RequestServiceType.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents the type of service in a request
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/RequestServiceType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:44   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:54   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// Enumerator that represents the type of service in a request
    /// </summary>
    [Serializable]
    public enum RequestServiceType
    {
        RequestServicePrivate, // Filter by PrivateID only. For rail only journeys.
        RequestServiceNumber   // Filter by ServiceNumber.
    }
 
}
