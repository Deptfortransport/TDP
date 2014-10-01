// *********************************************** 
// NAME			: GradientProfileEventFileFormatter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 17/01/2009
// DESCRIPTION	: Class which defines a custom event for logging a Gradient profile event to a file
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/GradientProfileEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Jan 19 2009 11:09:48   mmodi
//Initial revision.
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public class GradientProfileEventFileFormatter : IEventFormatter
    {
        #region Private members

        // Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        #endregion

        #region Constructor
        /// <summary>
		/// Constructor
		/// </summary>
        public GradientProfileEventFileFormatter()
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
            string output = string.Empty;

			if(logEvent is GradientProfileEvent)
			{
                GradientProfileEvent gpe = (GradientProfileEvent)logEvent;

                string outputFormat = "TD-GPE \t{0}\tTimeSubmitted:[{1}]\tDisplayCat:[{2}]\tUserLoggedOn:[{3}]\tSessionID:[{4}]";

                output = String.Format(outputFormat, gpe.Time.ToString(dateTimeFormat), gpe.Submitted.ToString(dateTimeFormat), gpe.DisplayCategory.ToString(), gpe.UserLoggedOn.ToString(), gpe.SessionId.ToString());
			}
			return output;
        }

        #endregion

    }
}
