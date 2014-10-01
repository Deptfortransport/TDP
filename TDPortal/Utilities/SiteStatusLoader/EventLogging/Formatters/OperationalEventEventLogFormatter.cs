// *********************************************** 
// NAME                 : OperationalEventEventLogFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Event log formatter for Operational Event
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Formatters/OperationalEventEventLogFormatter.cs-arc  $
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
    /// Formats operational events for publishing by Event Log.
    /// </summary>
    public class OperationalEventEventLogFormatter : IEventFormatter
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public OperationalEventEventLogFormatter()
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

            if (logEvent is OperationalEvent)
            {
                OperationalEvent operationalEvent = (OperationalEvent)logEvent;

                // NOTE: first string MUST be category for TNG to parse. TNG does not parse any of the remaining text.
                output = operationalEvent.Category + Environment.NewLine +
                         "SS OPERATIONAL EVENT" + Environment.NewLine +
                         "Time: " + operationalEvent.Time.ToString("yyyy-MM-ddTHH:mm:ss.fff") + Environment.NewLine +
                         "Category: " + operationalEvent.Category + Environment.NewLine +
                         "Level: " + operationalEvent.Level + Environment.NewLine +
                         "Message: " + operationalEvent.Message + Environment.NewLine +
                         "Machine: " + operationalEvent.MachineName + Environment.NewLine +
                         "Class logged: " + operationalEvent.TypeName + Environment.NewLine +
                         "Method logged: " + operationalEvent.MethodName + Environment.NewLine +
                         "Assembly logged: " + operationalEvent.AssemblyName + Environment.NewLine;

                if (operationalEvent.Target != null)
                {
                    output += "Target: " + operationalEvent.Target.ToString() + Environment.NewLine;
                }
            }

            return output;
        }
    }
}
