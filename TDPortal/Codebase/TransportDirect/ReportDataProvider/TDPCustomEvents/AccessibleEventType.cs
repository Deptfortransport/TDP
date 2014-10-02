// *********************************************** 
// NAME                 : AccessibleEventType.cs 
// AUTHOR               : David Lane
// DATE CREATED         : 18/01/2013
// DESCRIPTION          : Defines types for Accessible events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/AccessibleEventType.cs-arc  $
//
//   Rev 1.1   Jan 28 2013 15:27:44   DLane
//New event types
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.0   Jan 24 2013 13:00:58   dlane
//Initial revision.
//Resolution for 5873: CCN:677 - Accessible Journeys Planner

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// Enumeration containing classifiers for <c>AccessibleEvent</c>.
    /// </summary>
    public enum AccessibleEventType : int
    {
        Unknown = 0,
        StepFree,
        StepFreeWithAssistance,
        Assistance,
        StepFreeFewerChanges,
        StepFreeWithAssistanceFewerChanges,
        AssistanceFewerChanges,
    }
}
