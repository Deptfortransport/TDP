// *********************************************** 
// NAME                 : DefaultFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: DefaultFormatter class
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Formatters/DefaultFormatter.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:29:22   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;
using AO.Common;

namespace AO.EventLogging
{
    /// <summary>
    /// A class which formats log events for publishing.
    /// Used when a more specific formatter is not available
    /// for a given <c>LogEvent</c> type.
    /// </summary>
    public class DefaultFormatter : IEventFormatter
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultFormatter()
        { }

        /// <summary>
        /// Formats log event data into a string. The data included in the
        /// format string will exist for all <c>LogEvent</c> types.
        /// </summary>
        /// <param name="logEvent">The <c>LogEvent</c> to format.</param>
        /// <returns>A formatted string containing event data common across all event types.</returns>
        public string AsString(LogEvent logEvent)
        {
            string output = String.Format(Messages.SSTTDefaultFormatterOutput, logEvent.Time, logEvent.ClassName);
            return output;
        }
    }
}
