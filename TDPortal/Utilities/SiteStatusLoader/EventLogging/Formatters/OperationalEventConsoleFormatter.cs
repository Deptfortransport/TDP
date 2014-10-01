// *********************************************** 
// NAME                 : OperationalEventConsoleFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Console formatter for Operational Events
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Formatters/OperationalEventConsoleFormatter.cs-arc  $
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
    /// Formats operational events for publishing by console.
    /// </summary>
    public class OperationalEventConsoleFormatter : IEventFormatter
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public OperationalEventConsoleFormatter()
        {
        }

        /// <summary>
        /// Formats the given log event.
        /// </summary>
        /// <param name="logEvent">Log event to format.</param>
        /// <returns>A formatted string representing the log event.</returns>
        public string AsString(LogEvent logEvent)
        {
            string header = "SS-OP";
            string output = String.Empty;

            if (logEvent is OperationalEvent)
            {
                OperationalEvent operationalEvent = (OperationalEvent)logEvent;

                output =
                    header + " " +
                    operationalEvent.Time + " " +
                    operationalEvent.Message + " " +
                    operationalEvent.Category + " " +
                    operationalEvent.Level + " " +
                    operationalEvent.MachineName + " " +
                    operationalEvent.TypeName + " " +
                    operationalEvent.MethodName + " " +
                    operationalEvent.AssemblyName;

                if (operationalEvent.Target != null)
                {
                    output += " " + operationalEvent.Target.ToString();
                }
            }

            return output;
        }
    }
}
