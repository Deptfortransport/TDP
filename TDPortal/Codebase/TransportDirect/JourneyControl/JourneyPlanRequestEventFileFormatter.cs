// *********************************************** 
// NAME                 : JourneyPlanRequestEventFileFormatter.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 28/06/2004 
// DESCRIPTION  : File formatter for the JourneyPlanRequest event
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyPlanRequestEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:48   mturner
//Initial revision.
//
//   Rev 1.0   Jun 28 2004 15:41:10   passuied
//Initial Revision

using System;
using System.Globalization;

using TransportDirect.Common.Logging;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// File formatter for the JourneyPlanResults Event
	/// </summary>
	public class JourneyPlanRequestEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// default constructor
		/// </summary>
		public JourneyPlanRequestEventFileFormatter()
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

			if(logEvent is JourneyPlanRequestEvent)
			{
				JourneyPlanRequestEvent jpre = (JourneyPlanRequestEvent)logEvent;

				output =
					"TD-JPReqE\t" + 
					jpre.Time.ToString(dateTimeFormat) + "\t" +
					jpre.JourneyPlanRequestId  + "\t";
				foreach (ModeType mode in jpre.Modes)
				{
					output += mode.ToString() +"/";

				}
				output += "\t" +jpre.UserLoggedOn;

				if(jpre.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + jpre.SessionId);
				}
			}
			return output;
		}
	}
}
