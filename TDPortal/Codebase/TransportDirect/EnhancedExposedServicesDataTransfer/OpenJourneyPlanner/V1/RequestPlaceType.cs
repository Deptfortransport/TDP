// *********************************************** 
// NAME                 : RequestPlaceType.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents the type of place in a request
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/RequestPlaceType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:44   mturner
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
    /// Enumerator that represents the type of place in a request
    /// </summary>
    [Serializable]
    public enum RequestPlaceType
    {
        NaPTAN,     // Plan journey using one of the supplied NaPTANs.
        Coordinate, // Plan journey from or to the supplied OSGR, including a walk leg if 
                    // necessary from/to the specified point to/from the most appropriate starting Naptan.
        Locality    // Plan journey from or two the most appropriate  Naptan within walking distance of the
                    // supplied OSGR,  but without including a walk leg between the Naptan and the specified point.
    }
 
}