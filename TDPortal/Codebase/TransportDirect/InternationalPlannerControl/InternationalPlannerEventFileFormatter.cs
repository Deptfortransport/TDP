// *********************************************** 
// NAME			: InternationalPlannerEventFileFormatter.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 29/01/2010
// DESCRIPTION	: Enumeration defining type of the International planner for which to log event
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalPlannerEventFileFormatter.cs-arc  $
//
//   Rev 1.1   Feb 18 2010 15:49:22   mmodi
//Corrected file formatter
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 02 2010 10:04:10   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// File formatter class for international planner events
    /// </summary>
    class InternationalPlannerEventFileFormatter: IEventFormatter
    {
        #region Private members

        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        #endregion

        #region Constructor
        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalPlannerEventFileFormatter()
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

			if(logEvent is InternationalPlannerEvent)
			{
                InternationalPlannerEvent ipe = (InternationalPlannerEvent)logEvent;

                output.Append("TD-IPE\t");
                output.Append(ipe.Time.ToString(dateTimeFormat) + "\t");
                output.Append("InternationalPlannerEventType[" + ipe.InternationalPlanner.ToString() + "]\t");
                output.Append("LoggedOn[" + ipe.UserLoggedOn + "]");

                if (ipe.SessionId != OperationalEvent.SessionIdUnassigned)
                {
                    output.Append("\tSessionID[" + ipe.SessionId + "]");
                }
			}
			return output.ToString();
        }

        #endregion
    }
}
