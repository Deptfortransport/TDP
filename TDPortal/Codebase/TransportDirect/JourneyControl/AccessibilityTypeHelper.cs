// *********************************************** 
// NAME         : AccessibilityTypeHelper.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 20/11/2012
// DESCRIPTION  : AccessibilityTypeHelper class providing helper methods
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/AccessibilityTypeHelper.cs-arc  $
//
//   Rev 1.0   Dec 05 2012 14:29:20   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;

using ICJP = TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// AccessibilityTypeHelper class providing helper methods
    /// </summary>
    public class AccessibilityTypeHelper
    {
        /// <summary>
        /// Parses a CJP AssistanceServiceType into an AccessibilityType
        /// </summary>
        public static AccessibilityType GetAccessibilityType(ICJP.AssistanceServiceType cjpAssistanceServiceType)
        {
            switch (cjpAssistanceServiceType)
            {
                case ICJP.AssistanceServiceType.boarding:
                    return AccessibilityType.ServiceAssistanceBoarding;
                case ICJP.AssistanceServiceType.porterage:
                    return AccessibilityType.ServiceAssistancePorterage;
                case ICJP.AssistanceServiceType.wheelchair:
                    return AccessibilityType.ServiceAssistanceWheelchair;
                case ICJP.AssistanceServiceType.wheelchairBooked:
                    return AccessibilityType.ServiceAssistanceWheelchairBooked;
                case ICJP.AssistanceServiceType.other:
                    return AccessibilityType.ServiceAssistanceOther;
                default:
                    throw new TDException(
                        string.Format("Error parsing CJP AssistanceServiceType into an AccessibilityType, unrecognised value[{0}]", cjpAssistanceServiceType.ToString()),
                        false, TDExceptionIdentifier.AJErrorParsingCJPAssistanceServiceType);
            }
        }

        /// <summary>
        /// Parses a CJP AccessSummary into an AccessibilityType
        /// </summary>
        public static AccessibilityType GetAccessibilityType(ICJP.AccessSummary cjpAccessSummary)
        {
            ICJP.AccessFeature feature = cjpAccessSummary.accessFeature;
            ICJP.Transition transition = cjpAccessSummary.transition;

            switch (feature)
            {
                case ICJP.AccessFeature.barrier:
                    return AccessibilityType.AccessBarrier;

                case ICJP.AccessFeature.confinedSpace:
                    return AccessibilityType.AccessConfinedSpace;

                case ICJP.AccessFeature.escalator:
                    if (transition == ICJP.Transition.down)
                        return AccessibilityType.AccessEscalatorDown;
                    else if (transition == ICJP.Transition.up)
                        return AccessibilityType.AccessEscalatorUp;
                    break;

                case ICJP.AccessFeature.footpath:
                    return AccessibilityType.AccessFootpath;

                case ICJP.AccessFeature.lift:
                    if (transition == ICJP.Transition.down)
                        return AccessibilityType.AccessLiftDown;
                    else if (transition == ICJP.Transition.up)
                        return AccessibilityType.AccessLiftUp;
                    break;

                case ICJP.AccessFeature.narrowEntrance:
                    return AccessibilityType.AccessNarrowEntrance;

                case ICJP.AccessFeature.none:
                    return AccessibilityType.AccessNone;

                case ICJP.AccessFeature.openSpace:
                    return AccessibilityType.AccessOpenSpace;

                case ICJP.AccessFeature.other:
                    return AccessibilityType.AccessOther;

                case ICJP.AccessFeature.pavement:
                    return AccessibilityType.AccessPavement;

                case ICJP.AccessFeature.queueManagement:
                    return AccessibilityType.AccessQueueManagement;

                case ICJP.AccessFeature.ramp:
                    if (transition == ICJP.Transition.down)
                        return AccessibilityType.AccessRampDown;
                    else if (transition == ICJP.Transition.up)
                        return AccessibilityType.AccessRampUp;
                    break;

                case ICJP.AccessFeature.seriesOfStairs:
                    return AccessibilityType.AccessSeriesOfStairs;

                case ICJP.AccessFeature.shuttle:
                    return AccessibilityType.AccessShuttle;

                case ICJP.AccessFeature.stairs:
                    if (transition == ICJP.Transition.down)
                        return AccessibilityType.AccessStairsDown;
                    else if (transition == ICJP.Transition.up)
                        return AccessibilityType.AccessStairsUp;
                    break;

                case ICJP.AccessFeature.street:
                    return AccessibilityType.AccessStreet;

                case ICJP.AccessFeature.travelator:
                    return AccessibilityType.AccessTravelator;

                case ICJP.AccessFeature.passage:
                    return AccessibilityType.AccessPassage;

                case ICJP.AccessFeature.unknown:
                    return AccessibilityType.AccessUnknown;

                default:
                    throw new TDException(
                        string.Format("Error parsing CJP AccessSummary into an AccessibilityType, unrecognised value[{0}]", cjpAccessSummary.accessFeature.ToString()),
                        false, TDExceptionIdentifier.AJErrorParsingCJPAccessSummary);
            }

            // Default to returning an unknown type
            return AccessibilityType.Unknown;
        }
    }
}