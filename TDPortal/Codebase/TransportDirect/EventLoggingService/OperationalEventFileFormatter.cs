// *********************************************** 
// NAME                 : OperationalEventFileFormatter.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 04/07/2003 
// DESCRIPTION  : A formatter that formats
// operational events for file publishing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/OperationalEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:08   mturner
//Initial revision.
//
//   Rev 1.6   Oct 15 2003 08:40:02   geaton
//Format time logged to include milliseconds. this was requested by Atkins to assist in their performance testing.
//
//   Rev 1.5   Jul 29 2003 20:45:34   geaton
//Used ToString() to format target.
//
//   Rev 1.4   Jul 29 2003 17:31:38   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.3   Jul 25 2003 14:14:40   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.2   Jul 24 2003 18:27:48   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Formats operational events for publishing by file.
	/// </summary>
	public class OperationalEventFileFormatter : IEventFormatter
	{	
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// Default constructor.
		/// </summary>
		public OperationalEventFileFormatter()
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

			if(logEvent is OperationalEvent)
			{
				OperationalEvent oe = (OperationalEvent)logEvent;

				output =
					"TD-OP\t" + 
					oe.Time.ToString(dateTimeFormat) + "\t" +
					oe.Message + "\t" +
					oe.Category + "\t" +
					oe.Level + "\t" +
					oe.MachineName + "\t" +
					oe.TypeName + "\t" +
					oe.MethodName + "\t" +
					oe.AssemblyName;

				if(oe.Target != null)
				{
					output += ("\t" + oe.Target.ToString());
				}

				if(oe.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + oe.SessionId);
				}
			}
			return output;
		}
	}
}
