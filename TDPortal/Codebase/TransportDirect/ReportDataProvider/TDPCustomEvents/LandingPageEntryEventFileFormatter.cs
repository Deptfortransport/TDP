// *********************************************** 
// NAME                 : LandingPageEntryEventFileFormatter
// AUTHOR               : Jamie McAllister / Tim Mollart
// DATE CREATED         : 22/07/2005 
// DESCRIPTION  : File formatter for the LandingPageEntryEvent
// ************************************************ 


using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// File formatter for the LandingPageEntryEvent
	/// </summary>
	public class LandingPageEntryEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// default constructor
		/// </summary>
		public LandingPageEntryEventFileFormatter()
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

			LandingPageEntryEvent lpee = logEvent as LandingPageEntryEvent;

			if(logEvent != null)
			{
				output = "TD-LPEE\t" + 
						 lpee.Time.ToString(dateTimeFormat, CultureInfo.InvariantCulture) + "\t" +
						 "Partner ID=" + lpee.PartnerID + "\t" +
						 "Service ID=" + lpee.ServiceID;
			}
			return output;
		}
	}
}
