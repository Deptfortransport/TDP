﻿// *********************************************** 
// NAME			: InternationalPlannerRequestEventFileFormatter.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 25/01/2010
// DESCRIPTION	: Class which defines a custom event for logging International planner requests to a file
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalPlannerRequestEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Jan 25 2010 15:14:18   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// File formatter class for international planner request events
    /// </summary>
    public class InternationalPlannerRequestEventFileFormatter: IEventFormatter
    {
        #region Private members

        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        #endregion

        #region Constructor
        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalPlannerRequestEventFileFormatter()
		{

        }

        #endregion

        #region IEventFormatter Members
        /// <summary>
		/// Formats the given log event.
		/// </summary>
		/// <param name="logEvent">Log Event to format.</param>
		/// <returns>A formatted string representing the log event.</returns>
		public string AsString(LogEvent logEvent)
		{
            StringBuilder output = new StringBuilder();

			if(logEvent is InternationalPlannerRequestEvent)
			{
                InternationalPlannerRequestEvent ipre = (InternationalPlannerRequestEvent)logEvent;

                output.Append("TD-IPReqE\t");
                output.Append(ipre.Time.ToString(dateTimeFormat) + "\t");
                output.Append("RequestID[" + ipre.InternationalPlannerRequestId + "]\t");
                output.Append("LoggedOn[" + ipre.UserLoggedOn + "]");

                if (ipre.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output.Append("\tSessionID[" + ipre.SessionId + "]");
                }
			}
			return output.ToString();
        }

        #endregion
    }
}
