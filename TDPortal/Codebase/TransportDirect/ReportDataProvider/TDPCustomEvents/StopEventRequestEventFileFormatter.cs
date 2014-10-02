using System;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Summary description for StopEventRequestEventFileFormatter.
	/// </summary>
	public class StopEventRequestEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		public StopEventRequestEventFileFormatter()
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

			if(logEvent is StopEventRequestEvent)
			{
				StopEventRequestEvent e = (StopEventRequestEvent)logEvent;

				output = String.Format(Messages.StopEventRequestEventFileFormat,
					e.Submitted.ToString(dateTimeFormat),				   
					e.Time.ToString(dateTimeFormat),
					e.RequestId,
					e.RequestType.ToString(),
					e.Success.ToString());
									  
			}
			return output;
		}
		
	}
}
