using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Summary description for TDMobilePageEntryEventFileFormatter.
	/// </summary>
	public class TDMobilePageEntryEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// default constructor
		/// </summary>
		public TDMobilePageEntryEventFileFormatter()
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

			if(logEvent is TDMobilePageEntryEvent)
			{
				TDMobilePageEntryEvent mobilePageEntryEvent = (TDMobilePageEntryEvent)logEvent;

				output =
					"TD-PEE\t" + 
					mobilePageEntryEvent.Time.ToString(dateTimeFormat) + "\t" +
					mobilePageEntryEvent.PageName.ToString()  + "\t" +
					mobilePageEntryEvent.UserLoggedOn;

				

				if(mobilePageEntryEvent.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + mobilePageEntryEvent.SessionId);
				}
			}
			return output;
		}
	}
}
