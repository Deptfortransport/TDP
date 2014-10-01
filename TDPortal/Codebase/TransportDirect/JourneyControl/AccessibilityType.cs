// *********************************************** 
// NAME         : AccessibilityType.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 20/11/2012
// DESCRIPTION  : AccessibilityType enumeration
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/AccessibilityType.cs-arc  $
//
//   Rev 1.2   Mar 19 2013 12:03:20   mmodi
//Updates to accessible icons logic
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.1   Jan 30 2013 15:45:10   mmodi
//Updated accessible service icon for Tram mode
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.0   Dec 05 2012 14:29:18   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// AccessibilityType enumeration
    /// </summary>
    [Serializable()]
    public enum AccessibilityType
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