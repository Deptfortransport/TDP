// *********************************************** 
// NAME             : GISQueryEventFileFormatter.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: File formatter for the GISQueryEvent
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.EventLogging;

namespace TDP.Reporting.Events.Formatters
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