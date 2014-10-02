// *********************************************** 
// NAME                 : UserPreferenceSaveEventFileFormatter
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 28/06/2004 
// DESCRIPTION  : File formatter for the UserPreferenceSaveEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/UserPreferenceSaveEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:38   mturner
//Initial revision.
//
//   Rev 1.0   Jun 28 2004 15:41:16   passuied
//Initial Revision

using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Summary description for UserPreferenceSaveEventFileFormatter.
	/// </summary>
	public class UserPreferenceSaveEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// default constructor
		/// </summary>
		public UserPreferenceSaveEventFileFormatter()
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

			if(logEvent is UserPreferenceSaveEvent)
			{
				UserPreferenceSaveEvent upse = (UserPreferenceSaveEvent)logEvent;

				output =
					"TD-UPSE\t" + 
					upse.Time.ToString(dateTimeFormat) + "\t" +
					upse.EventCategory + "\t" +
					upse.UserLoggedOn;

				

				if(upse.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + upse.SessionId);
				}
			}
			return output;
		}
	}
}
