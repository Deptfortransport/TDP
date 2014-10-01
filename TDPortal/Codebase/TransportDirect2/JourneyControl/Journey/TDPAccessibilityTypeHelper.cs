// *********************************************** 
// NAME             : TDPAccessibilityTypeHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 21 Apr 2011
// DESCRIPTION  	: TDPAccessibilityTypeHelper class providing helper methods
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICJP = TransportDirect.JourneyPlanning.CJPInterface;
using TDP.Common;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// TDPAccessibilityTypeHelper class providing helper methods
    /// </summary>
    public class TDPAccessibilityTypeHelper
    {
        /// <summary>
        /// Parses a CJP AssistanceServiceType into an TDPAccessibilityType
        /// </summary>
        public static TDPAccessibilityType GetTDPAccessibilityType(ICJP.AssistanceServiceType cjpAssistanceServiceType)
        {
            switch (cjpAssistanceServiceType)
            {
                case ICJP.AssistanceServiceType.boarding:
                    return TDPAccessibilityType.ServiceAssistanceBoarding;
                case ICJP.AssistanceServiceType.porterage:
                    return TDPAccessibilityType.ServiceAssistancePorterage;
                case ICJP.AssistanceServiceType.wheelchair:
                    return TDPAccessibilityType.ServiceAssistanceWheelchair;
                case ICJP.AssistanceServiceType.wheelchairBooked:
                    return TDPAccessibilityType.ServiceAssistanceWheelchairBooked;
                case ICJP.AssistanceServiceType.other:
                    return TDPAccessibilityType.ServiceAssistanceOther;
                default:
                    throw new TDPException(
                        string.Format("Error parsing CJP AssistanceServiceType into an TDPAccessibilityType, unrecognised value[{0}]", cjpAssistanceServiceType.ToString()),
                        false, TDPExceptionIdentifier.JCErrorParsingCJPAssistanceServiceType);
            }
        }

        /// <summary>
        /// Parses a CJP AccessSummary into an TDPAccessibilityType
        /// </summary>
        public static TDPAccessibilityType GetTDPAccessibilityType(ICJP.AccessSummary cjpAccessSummary)
        {
            ICJP.AccessFeature feature = cjpAccessSummary.accessFeature;
            ICJP.Transition transition = cjpAccessSummary.transition;

            switch (feature)
            {
                case ICJP.AccessFeature.barrier:
                    return TDPAccessibilityType.AccessBarrier;

                case ICJP.AccessFeature.confinedSpace:
                    return TDPAccessibilityType.AccessConfinedSpace;

                case ICJP.AccessFeature.escalator:
                    if (transition == ICJP.Transition.down)
                        return TDPAccessibilityType.AccessEscalatorDown;
                    else if (transition == ICJP.Transition.up)
                        return TDPAccessibilityType.AccessEscalatorUp;
                    break;

                case ICJP.AccessFeature.footpath:
                    return TDPAccessibilityType.AccessFootpath;

                case ICJP.AccessFeature.lift:
                    if (transition == ICJP.Transition.down)
                        return TDPAccessibilityType.AccessLiftDown;
                    else if (transition == ICJP.Transition.up)
                        return TDPAccessibilityType.AccessLiftUp;
                    break;

                case ICJP.AccessFeature.narrowEntrance:
                    return TDPAccessibilityType.AccessNarrowEntrance;

                case ICJP.AccessFeature.none:
                    return TDPAccessibilityType.AccessNone;

                case ICJP.AccessFeature.openSpace:
                    return TDPAccessibilityType.AccessOpenSpace;

                case ICJP.AccessFeature.other:
                    return TDPAccessibilityType.AccessOther;

                case ICJP.AccessFeature.pavement:
                    return TDPAccessibilityType.AccessPavement;

                case ICJP.AccessFeature.queueManagement:
                    return TDPAccessibilityType.AccessQueueManagement;

                case ICJP.AccessFeature.ramp:
                    if (transition == ICJP.Transition.down)
                        return TDPAccessibilityType.AccessRampDown;
                    else if (transition == ICJP.Transition.up)
                        return TDPAccessibilityType.AccessRampUp;
                    break;

                case ICJP.AccessFeature.seriesOfStairs:
                    return TDPAccessibilityType.AccessSeriesOfStairs;

                case ICJP.AccessFeature.shuttle:
                    return TDPAccessibilityType.AccessShuttle;

                case ICJP.AccessFeature.stairs:
                    if (transition == ICJP.Transition.down)
                        return TDPAccessibilityType.AccessStairsDown;
                    else if (transition == ICJP.Transition.up)
                        return TDPAccessibilityType.AccessStairsUp;
                    break;

                case ICJP.AccessFeature.street:
                    return TDPAccessibilityType.AccessStreet;

                case ICJP.AccessFeature.travelator:
                    return TDPAccessibilityType.AccessTravelator;

                case ICJP.AccessFeature.passage:
                    return TDPAccessibilityType.AccessPassage;

                case ICJP.AccessFeature.unknown:
                    return TDPAccessibilityType.AccessUnknown;

                default:
                    throw new TDPException(
                        string.Format("Error parsing CJP AccessSummary into an TDPAccessibilityType, unrecognised value[{0}]", cjpAccessSummary.accessFeature.ToString()),
                        false, TDPExceptionIdentifier.JCErrorParsingCJPAccessSummary);
            }

            // Default to returning an unknown type
            return TDPAccessibilityType.Unknown;
        }
    }
}
