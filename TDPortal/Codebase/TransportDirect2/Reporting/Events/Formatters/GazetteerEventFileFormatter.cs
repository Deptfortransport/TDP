// *********************************************** 
// NAME             : GazetteerEventFileFormatter.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: File formatter for the GazetteerEvent
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
    /// File formatter for the GazetteerEvent
    /// </summary>
    public class GazetteerEventFileFormatter : IEventFormatter
    {
        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
        private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        /// <summary>
        /// default constructor
        /// </summary>
        public GazetteerEventFileFormatter()
        {

        }

        /// <summary>
        /// Formats the given log event.
        /// </summary>
        /// <param name="logEvent">Log Event to format.</param>
        /// <returns>A formatted string representing the log event.</returns>
        public string AsString(LogEvent logEvent)
        {
            string output = String.Empty;

            if (logEvent is GazetteerEvent)
            {
                GazetteerEvent ge = (GazetteerEvent)logEvent;

                output =
                    "TD-GE\t" +
                    ge.Time.ToString(dateTimeFormat) + "\t" +
                    ge.EventCategory + "\t" +
                    ge.UserLoggedOn;



                if (ge.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output += ("\t" + ge.SessionId);
                }
            }
            return output;
        }
    }
}