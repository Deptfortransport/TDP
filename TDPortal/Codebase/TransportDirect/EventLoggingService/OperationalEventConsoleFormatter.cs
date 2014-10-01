// *********************************************** 
// NAME                 : OperationalEventConsoleFormatter.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 04/07/2003 
// DESCRIPTION  : A formatter that formats
// operational events for file publishing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/OperationalEventConsoleFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:06   mturner
//Initial revision.
//
//   Rev 1.5   Jul 29 2003 20:45:36   geaton
//Used ToString() to format target.
//
//   Rev 1.4   Jul 29 2003 17:31:34   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.3   Jul 25 2003 14:14:36   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.2   Jul 24 2003 18:27:44   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Formats operational events for publishing by console.
	/// </summary>
	public class OperationalEventConsoleFormatter : IEventFormatter
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OperationalEventConsoleFormatter()
		{
		}

		/// <summary>
		/// Formats the given log event.
		/// </summary>
		/// <param name="logEvent">Log event to format.</param>
		/// <returns>A formatted string representing the log event.</returns>
		public string AsString(LogEvent logEvent)
		{
			string header = "TD-OP";
			string output = String.Empty;

			if(logEvent is OperationalEvent)
			{
				OperationalEvent operationalEvent = (OperationalEvent)logEvent;
				
				output =
					header + " " + 
					operationalEvent.Time + " " +
					operationalEvent.Message + " " +
					operationalEvent.Category + " " +
					operationalEvent.Level + " " +
					operationalEvent.MachineName + " " +
					operationalEvent.TypeName + " " +
					operationalEvent.MethodName + " " +
					operationalEvent.AssemblyName;

				if (operationalEvent.Target != null)
				{
					output += " " + operationalEvent.Target.ToString();
				}

				if(operationalEvent.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += " " + operationalEvent.SessionId;
				}
			}

			return output;
		}
	}
}
