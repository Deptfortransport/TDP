// *********************************************** 
// NAME                 : ActivityType.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: Enumerator that represents an activity type.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/ActivityType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:36   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:20:28   COwczarek
//Initial revision.
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1
{

    /// <summary>
    /// Enumerator that represents an activity type.
    /// </summary>
    [Serializable]
    public enum ActivityType
    {
        Arrive,       // Activity is an arrival.
        Depart,       // Activity is a departure.
        ArriveDepart, // Activity is both an arrival and then a departure.
        Frequency,    // Activity arrives and departs but is not timetabled.
        Request,      // Activity is a request stop (arrives and departs).
        Pass          // Activity arrives and departs but does not stop.
    }

}