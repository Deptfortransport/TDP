// *********************************************** 
// NAME             : TDPAccessibilityType.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 21 Apr 2011
// DESCRIPTION  	: TDPAccessibilityType enumeration
// ************************************************
// 
                
using System;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// TDPAccessibilityType enumeration
    /// </summary>
    [Serializable()]
    public enum TDPAccessibilityType
    {
        // Used for returning an accessibility type which is not parsed into any of the others, 
        // should not be displayed in UI output
        Unknown,

        // Service assistance (e.g. train service offers assistance with boarding)
        ServiceAssistanceBoarding,
        ServiceAssistanceWheelchair,
        ServiceAssistanceWheelchairBooked,
        ServiceAssistancePorterage,
        ServiceAssistanceOther,

        // Service specific (e.g. bus service has low floor)
        ServiceLowFloor,
        ServiceLowFloorTram,
        ServiceMobilityImpairedAccess,
        ServiceMobilityImpairedAccessBus,
        ServiceMobilityImpairedAccessService,
        ServiceWheelchairBookingRequired,

        // Accessibility
        EscalatorFreeAccess,
        LiftFreeAccess,
        MobilityImpairedAccess,
        StepFreeAccess,
        WheelchairAccess,
        
        // Access features
        AccessLiftUp,
        AccessLiftDown,
        AccessStairsUp,
        AccessStairsDown,
        AccessSeriesOfStairs,
        AccessEscalatorUp,
        AccessEscalatorDown,
        AccessTravelator,
        AccessRampUp,
        AccessRampDown,
        AccessShuttle,
        AccessBarrier,
        AccessNarrowEntrance,
        AccessConfinedSpace,
        AccessQueueManagement,
        AccessNone,
        AccessUnknown,
        AccessOther,
        AccessOpenSpace,
        AccessStreet,
        AccessPavement,
        AccessFootpath,
        AccessPassage,
    }
}
