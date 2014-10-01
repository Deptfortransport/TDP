// *********************************************** 
// NAME             : EventParser.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 18 June 2011
// DESCRIPTION  	: Event parser class to convert an external event into an internal LogEvent
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;
using TDP.Reporting.Events;
using TDPEL = TransportDirect.Common.Logging;
using TDPCJPE = TransportDirect.ReportDataProvider.CJPCustomEvents;

namespace TDP.Reporting.EventReceiver
{
    /// <summary>
    /// Event parser class to convert an external event into an internal LogEvent
    /// </summary>
    public static class EventParser
    {
        /// <summary>
        /// Parses a TDP CJPCustomEvent (TDP LogEvent) into an CJPCustomEvent (LogEvent)
        /// </summary>
        /// <param name="tdpLogEvent"></param>
        /// <returns></returns>
        public static LogEvent ParseTDPCJPLogEvent(TDPEL.LogEvent tdpLogEvent)
        {
            if (tdpLogEvent != null)
            {
                if (tdpLogEvent is TDPCJPE.JourneyWebRequestEvent)
                {
                    TDPCJPE.JourneyWebRequestEvent jwre = (TDPCJPE.JourneyWebRequestEvent)tdpLogEvent;

                    JourneyWebRequestType jwrt = (JourneyWebRequestType)Enum.Parse(typeof(JourneyWebRequestType), jwre.RequestType.ToString());

                    JourneyWebRequestEvent logEvent = new JourneyWebRequestEvent(
                        jwre.SessionId, jwre.JourneyWebRequestId, jwre.Submitted,
                        jwrt, jwre.RegionCode, jwre.Success, jwre.RefTransaction);

                    logEvent.Time = jwre.Time;

                    return logEvent;
                }
                else if (tdpLogEvent is TDPCJPE.LocationRequestEvent)
                {
                    TDPCJPE.LocationRequestEvent lre = (TDPCJPE.LocationRequestEvent)tdpLogEvent;

                    JourneyPrepositionCategory jpc = (JourneyPrepositionCategory)Enum.Parse(typeof(JourneyPrepositionCategory), lre.PrepositionCategory.ToString());

                    LocationRequestEvent logEvent = new LocationRequestEvent(
                        lre.JourneyPlanRequestId, jpc, lre.AdminAreaCode, lre.RegionCode);

                    logEvent.Time = lre.Time;

                    return logEvent;
                }
                else if (tdpLogEvent is TDPCJPE.InternalRequestEvent)
                {
                    TDPCJPE.InternalRequestEvent ire = (TDPCJPE.InternalRequestEvent)tdpLogEvent;

                    InternalRequestType irt = (InternalRequestType)Enum.Parse(typeof(InternalRequestType), ire.RequestType.ToString());

                    InternalRequestEvent logEvent = new InternalRequestEvent(
                        ire.SessionId, ire.InternalRequestId, ire.Submitted, irt, ire.FunctionType,
                        ire.Success, ire.RefTransaction
                        );

                    logEvent.Time = ire.Time;

                    return logEvent;
                }
                else if (tdpLogEvent is TDPEL.OperationalEvent)
                {
                    try
                    {
                        TDPEL.OperationalEvent tdpOE = (TDPEL.OperationalEvent)tdpLogEvent;

                        // Parse the external types (assuming app namespace values are the same)
                        TDPTraceLevel traceLevel = (TDPTraceLevel)Enum.Parse(typeof(TDPTraceLevel), tdpOE.Level.ToString());
                        TDPEventCategory eventCategory = (TDPEventCategory)Enum.Parse(typeof(TDPEventCategory), tdpOE.Category.ToString());

                        OperationalEvent logEvent = new OperationalEvent(
                            eventCategory, tdpOE.SessionId, traceLevel, tdpOE.Message);
                        logEvent.Time = tdpOE.Time;
                        logEvent.UpdateContextProperties(tdpOE.MethodName, tdpOE.TypeName, tdpOE.AssemblyName, tdpOE.MethodName);

                        return logEvent;
                    }
                    catch (Exception ex)
                    {
                        Trace.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, String.Format("{0} Exception: {1} StackTrace: {2}",
                                Messages.Service_UnknownEventReceived,
                                ex.Message,
                                ex.StackTrace)));
                        // Return null
                    }
                }
            }

            return null;
        }
    }
}
