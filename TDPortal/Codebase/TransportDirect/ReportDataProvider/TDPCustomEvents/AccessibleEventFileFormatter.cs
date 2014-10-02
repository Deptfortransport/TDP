// *********************************************** 
// NAME                 : AccessibleEventFileFormatter.cs
// AUTHOR               : David Lane
// DATE CREATED         : 14/01/2013
// DESCRIPTION          : File formatter for the AccessibleEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/AccessibleEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Jan 24 2013 13:00:56   DLane
//Initial revision.
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// File formatter for the AccessibleEvent
    /// </summary>
    public class AccessibleEventFileFormatter : IEventFormatter
    {
        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
        private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        /// <summary>
        /// Default constructor
        /// </summary>
        public AccessibleEventFileFormatter()
        {

        }

        /// <summary>
        /// Formats the given log event.
        /// </summary>
        /// <param name="logEvent">Log Event to format.</param>
        /// <returns>A formatted string representing the log event.</returns>
        public string AsString(LogEvent logEvent)
        {
            StringBuilder output = new StringBuilder();

            if (logEvent is AccessibleEvent)
            {
                AccessibleEvent ae = (AccessibleEvent)logEvent;

                string tab = "\t";

                output.Append("TD-AE" + tab);
                output.Append(ae.Time.ToString(dateTimeFormat) + tab);
                output.Append(ae.AccessibleEventType.ToString() + tab);

                if (ae.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output.Append(tab);
                    output.Append("SessionId: " + tab);
                    output.Append(ae.SessionId);
                }
            }
            return output.ToString();
        }
    }
}