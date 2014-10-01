// *********************************************** 
// NAME                 : OperationalEventEventLogFormatter.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 04/07/2003 
// DESCRIPTION  : A formatter that formats
// operational events for Event Log publishing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/OperationalEventEventLogFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:08   mturner
//Initial revision.
//
//   Rev 1.7   Nov 07 2003 09:11:58   geaton
//Added category at start of description field - for TNG to parse.
//
//   Rev 1.6   Nov 03 2003 15:45:34   geaton
//Improved layout of event string.
//
//   Rev 1.5   Jul 29 2003 20:45:34   geaton
//Used ToString() to format target.
//
//   Rev 1.4   Jul 29 2003 17:31:36   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.3   Jul 25 2003 14:14:38   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.2   Jul 24 2003 18:27:46   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
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

			if(logEvent is OperationalEvent)
			{
				OperationalEvent operationalEvent = (OperationalEvent)logEvent;
				
				// NOTE: first string MUST be category for TNG to parse. TNG does not parse any of the remaining text.
				output = operationalEvent.Category + Environment.NewLine +
						 "TDP OPERATIONAL EVENT" + Environment.NewLine +
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

				if(operationalEvent.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += "Session Id: " + operationalEvent.SessionId + Environment.NewLine;
				}
			}

			return output;
		}
	}
}
