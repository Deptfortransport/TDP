// *********************************************** 
// NAME                 : LegType.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents the type of journey leg
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/LegType.cs-arc  $
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
    /// Enumerator that represents the type of journey leg
    /// </summary>
	public enum LegType
	{
        TimedLeg,     // This is for legs that run to timetabled times.
        FrequencyLeg, // This is for legs that run to frequencies.
        ContinuousLeg // This is for legs that can be performed at any time within a given window of opportunity.
	}

}
