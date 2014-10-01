﻿// *********************************************** 
// NAME             : PageEntryEventFileFormatter.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 20 Apr 2011
// DESCRIPTION  	: File formatter for the PageEntryEvent
// ************************************************
// 

using System.Text;
using TDP.Common.EventLogging;

namespace TDP.Reporting.Events.Formatters
{
    /// <summary>
    /// File formatter for the PageEntryEvent
    /// </summary>
    public class PageEntryEventFileFormatter : IEventFormatter
    {
        #region Private members

        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
        private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        #endregion

        #region Constructor

        /// <summary>
        /// default constructor
        /// </summary>
        public PageEntryEventFileFormatter()
        {

        }

        #endregion

        #region Public methods
        
        /// <summary>
        /// Formats the given log event.
        /// </summary>
        /// <param name="logEvent">Log Event to format.</param>
        /// <returns>A formatted string representing the log event.</returns>
        public string AsString(LogEvent logEvent)
        {
            StringBuilder output = new StringBuilder();

            if (logEvent is PageEntryEvent)
            {
                PageEntryEvent pee = (PageEntryEvent)logEvent;

                string tab = "\t";

                output.Append("PEE" + tab);
                output.Append(pee.Time.ToString(dateTimeFormat) + tab);
                output.Append(pee.Page.ToString() + tab);
                output.Append("UserLoggedOn: ");
                output.Append(pee.UserLoggedOn.ToString() + tab);
                output.Append("ThemeId: ");
                output.Append(pee.ThemeId.ToString() + tab);

                if (pee.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output.Append(tab);
                    output.Append(pee.SessionId);
                }
            }
            return output.ToString();
        }

        #endregion
    }
}
