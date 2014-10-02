// ******************************************************** 
// NAME                 : UserFeedbackEventFileFormatter.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 16/07/2004 
// DESCRIPTION  : File formatter for the UserFeedbackEvent
// ******************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/UserFeedbackEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:36   mturner
//Initial revision.
//
//   Rev 1.0   Jul 20 2004 15:30:08   jmorrissey
//Initial revision.

using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Summary description for UserFeedbackEventFileFormatter.
	/// </summary>
	public class UserFeedbackEventFileFormatter : IEventFormatter
	{

		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// default constructor
		/// </summary>
		public UserFeedbackEventFileFormatter()
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

			if(logEvent is UserFeedbackEvent)
			{
				UserFeedbackEvent ufe = (UserFeedbackEvent)logEvent;			

				output =
					"TD-UFE\t" + 
					ufe.Time.ToString(dateTimeFormat) + "\t" +
					ufe.SessionId + "\t" +					
					ufe.FeedbackType + "\t" +
					ufe.SubmittedTime.ToString(dateTimeFormat) + "\t" +
					ufe.AcknowledgedTime.ToString(dateTimeFormat) + "\t" +
					ufe.AcknowledgmentSent + "\t" +		
					ufe.UserLoggedOn;		

				if(ufe.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + ufe.SessionId);
				}
			}
			return output;
		}
	}
}
