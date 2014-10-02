// *********************************************** 
// NAME                 : GISQueryEventFileFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 14/01/2013
// DESCRIPTION          : File formatter for the GISQueryEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/GISQueryEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Jan 14 2013 14:42:32   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// File formatter for the GISQueryEvent
    /// </summary>
    public class GISQueryEventFileFormatter : IEventFormatter
    {
        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
        private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        /// <summary>
        /// Default constructor
        /// </summary>
        public GISQueryEventFileFormatter()
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

            if (logEvent is GISQueryEvent)
            {
                GISQueryEvent gqe = (GISQueryEvent)logEvent;

                string tab = "\t";

                output.Append("TD-GQE" + tab);
                output.Append(gqe.Time.ToString(dateTimeFormat) + tab);
                output.Append(gqe.GISQueryType.ToString() + tab);

                if (gqe.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output.Append(tab);
                    output.Append("SessionId: " + tab);
                    output.Append(gqe.SessionId);
                }
            }
            return output.ToString();
        }
    }
}