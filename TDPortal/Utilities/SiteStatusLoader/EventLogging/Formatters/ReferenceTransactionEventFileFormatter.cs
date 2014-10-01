// *********************************************** 
// NAME                 : ReferenceTransactionEventFileFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: File formatter for ReferenceTransactionEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Formatters/ReferenceTransactionEventFileFormatter.cs-arc  $
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
    /// Formats reference transaction events for publishing by file.
    /// </summary>
    public class ReferenceTransactionEventFileFormatter : IEventFormatter
    {
        private const string TimeFormat = "yyyy-MM-dd HH:mm:ss.fff000000";
        private const string RecordFormat = "1,'{0}','{1}','{2}','{3}','{4}','{5}'";

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReferenceTransactionEventFileFormatter()
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

            if (logEvent is ReferenceTransactionEvent)
            {
                ReferenceTransactionEvent rte = (ReferenceTransactionEvent)logEvent;

                output = String.Format(RecordFormat,
                                       rte.EventType,
                                       rte.ServiceLevelAgreement.ToString(),
                                       rte.TimeSubmitted.ToString(TimeFormat),
                                       rte.TimeCompleted.ToString(TimeFormat),
                                       rte.SessionId,
                                       rte.Time.ToString(TimeFormat),
                                       rte.Successful.ToString());
            }

            return output;
        }
    }
}
