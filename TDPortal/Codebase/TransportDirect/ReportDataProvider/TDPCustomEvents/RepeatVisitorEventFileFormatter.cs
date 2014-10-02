// *********************************************** 
// NAME                 : RepeatVisitorEventFileFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 12/05/2008
// DESCRIPTION          : File formatter for the RepeatVisitorEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RepeatVisitorEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   May 14 2008 15:34:32   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// File formatter for RepeatVisitorEvent
    /// </summary>
    public class RepeatVisitorEventFileFormatter : IEventFormatter
    {
        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
        private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        /// <summary>
        /// Default constructor
        /// </summary>
        public RepeatVisitorEventFileFormatter()
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

            if (logEvent is RepeatVisitorEvent)
            {
                RepeatVisitorEvent rve = (RepeatVisitorEvent)logEvent;

                string tab = "\t";

                output.Append("TD-RVE" + tab);
                output.Append(rve.Time.ToString(dateTimeFormat) + tab);
                output.Append(rve.RepeatVisitorType.ToString() + tab);
                output.Append("ThemeId: ");
                output.Append(rve.ThemeId.ToString() + tab);
                output.Append("Domain: ");
                output.Append(rve.Domain + tab);
                output.Append("LastPageVisited: ");
                output.Append(rve.LastPageVisted + tab);
                output.Append("LastVisitedDateTime: ");
                output.Append(rve.LastVisitedDateTime.ToString(dateTimeFormat) + tab);
                output.Append("UserAgent: ");
                output.Append(rve.UserAgent + tab);
                output.Append("SessionIdOld: ");
                output.Append(rve.SessionIdOld);
                                
                if (rve.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output.Append(tab);
                    output.Append("SessionIdNew: " + tab);
                    output.Append(rve.SessionId);
                }
            }
            return output.ToString();
        }
    }
}
