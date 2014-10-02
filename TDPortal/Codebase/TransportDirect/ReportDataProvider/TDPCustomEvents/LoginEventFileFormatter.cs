// *********************************************** 
// NAME                 : LoginEventFileFormatter
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 28/06/2004 
// DESCRIPTION  : File formatter for the PageEntryEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/LoginEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:26   mturner
//Initial revision.
//
//   Rev 1.0   Jun 28 2004 15:41:14   passuied
//Initial Revision

using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// File formatter for the LoginEvent
	/// </summary>
	public class LoginEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// default constructor
		/// </summary>
		public LoginEventFileFormatter()
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

			if(logEvent is LoginEvent)
			{
				LoginEvent le = (LoginEvent)logEvent;

				output =
					"TD-LE\t" + 
					le.Time.ToString(dateTimeFormat) + "\t" +
					le.UserLoggedOn;

				if(le.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + le.SessionId);
				}
			}
			return output;
		}
	}
}
