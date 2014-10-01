// *********************************************** 
// NAME                 : OperationalEventFileFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: File log formatter for Operational Event
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Formatters/OperationalEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:29:22   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader


using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Formats operational events for publishing by file.
    /// </summary>
    public class OperationalEventFileFormatter : IEventFormatter
    {
        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
        private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        /// <summary>
        /// Default constructor.
        /// </summary>
        public OperationalEventFileFormatter()
        {
        }

        /// <summary>
        /// Formats tht given log event.
        /// </summary>
        /// <param name="logEvent">Log Event to format.</param>
        /// <returns>A formatted string representing the log event.</returns>
        public string AsString(LogEvent logEvent)
        {
            string output = String.Empty;

            if (logEvent is OperationalEvent)
            {
                OperationalEvent oe = (OperationalEvent)logEvent;

                output =
                    "SS-OP\t" +
                    oe.Time.ToString(dateTimeFormat) + "\t" +
                    oe.Message + "\t" +
                    oe.Category + "\t" +
                    oe.Level + "\t" +
                    oe.MachineName + "\t" +
                    oe.TypeName + "\t" +
                    oe.MethodName + "\t" +
                    oe.AssemblyName;

                if (oe.Target != null)
                {
                    output += ("\t" + oe.Target.ToString());
                }
            }
            return output;
        }
    }
}
