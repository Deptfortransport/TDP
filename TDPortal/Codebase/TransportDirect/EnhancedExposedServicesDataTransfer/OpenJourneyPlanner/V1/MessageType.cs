// *********************************************** 
// NAME                 : MessageType.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents the source of the journey result message
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/MessageType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:40   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:48   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1
{

    /// <summary>
    /// Enumerator that represents the source of the journey result message
    /// </summary>
	public enum MessageType
	{
        JourneyPlannerMessage, // Message from journey planner
        RailEngineMessage,     // Message from rail engine
        JourneyWebMessage      // Message from JourneyWeb (Traveline)
	}

}
