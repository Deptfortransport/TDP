// *********************************************** 
// NAME                 : OperationalEventEmailPublisher.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 07/07/2003 
// DESCRIPTION  : A formatter that formats
// operational events for e-mail publishing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/OperationalEventEmailFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:08   mturner
//Initial revision.
//
//   Rev 1.5   Jul 29 2003 20:45:22   geaton
//Put formatting of machine name in correct position. Used ToString() to format target.
//
//   Rev 1.4   Jul 29 2003 17:31:34   geaton
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
	/// Formats operational events for publishing by email.
	/// </summary>
	public class OperationalEventEmailFormatter : IEventFormatter
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public OperationalEventEmailFormatter()
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
				OperationalEvent oe = (OperationalEvent)logEvent;

				output =
					"TD Operational Event\n\n" +
					"Time: " + oe.Time + "\n" +
					"Message: " + oe.Message + "\n" +
					"Category: " + oe.Category + "\n" +
					"Level: " + oe.Level + "\n";

				output += "Machine Name: ";
				if(oe.MachineName.Length > 0)
				{
					output += (oe.MachineName + "\n");
				}
				else
				{
					output += ("N/A\n");
				}

				output += "Type Name: ";
				if(oe.TypeName.Length > 0)
				{
					output += (oe.TypeName + "\n");
				}
				else
				{
					output += ("N/A\n");
				}

				output += "Method Name: ";
				if(oe.MethodName.Length > 0)
				{
					output += (oe.MethodName + "\n");
				}
				else
				{
					output += ("N/A\n");
				}

				output += "Assembly Name: ";
				if(oe.AssemblyName.Length > 0)
				{
					output += (oe.AssemblyName + "\n");
				}
				else
				{
					output += ("N/A\n");
				}

				output += "Target: ";
				if(oe.Target != null)
				{
					output += (oe.Target.ToString() + "\n");
				}
				else
				{
					output += ("N/A\n");
				}

				output += "Session: ";
				if(oe.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += oe.SessionId;
				}
				else
				{
					output += ("N/A\n");
				}
			}

			return output;
		}
	}
}
