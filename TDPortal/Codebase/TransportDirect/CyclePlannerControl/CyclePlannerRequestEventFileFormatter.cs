// *********************************************** 
// NAME			: CyclePlannerRequestEventFileFormatter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 20/08/2008
// DESCRIPTION	: Class which defines a custom event for logging a Cycle planner request to a file
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CyclePlannerRequestEventFileFormatter.cs-arc  $
//
//   Rev 1.1   Jan 22 2009 10:37:54   mmodi
//Added titles to output
//
//   Rev 1.0   Aug 22 2008 10:46:36   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public class CyclePlannerRequestEventFileFormatter : IEventFormatter
    {
        #region Private members

        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        #endregion

        #region Constructor
        /// <summary>
		/// Constructor
		/// </summary>
        public CyclePlannerRequestEventFileFormatter()
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

			if(logEvent is CyclePlannerRequestEvent)
			{
                CyclePlannerRequestEvent cpre = (CyclePlannerRequestEvent)logEvent;

                output.Append("TD-CPReqE\t");
                output.Append(cpre.Time.ToString(dateTimeFormat) + "\t");
                output.Append("RequestID[" + cpre.CyclePlannerRequestId + "]\t");
                output.Append("LoggedOn[" + cpre.UserLoggedOn + "]");

                if (cpre.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output.Append("\tSessionID[" + cpre.SessionId + "]");
                }
			}
			return output.ToString();
        }

        #endregion
    }
}
