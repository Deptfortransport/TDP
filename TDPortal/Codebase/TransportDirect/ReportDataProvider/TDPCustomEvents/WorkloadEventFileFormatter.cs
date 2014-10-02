using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Formats operational events for publishing by file.
	/// </summary>
	public class WorkloadEventFileFormatter : IEventFormatter
	{	
		// Provide formats that will allow easy import into database if necessary.
		private const string TimeFormat = "yyyy-MM-dd HH:mm:ss.fff000000";
		private const string RecordFormat = "WorkloadEvent,'{0}','{1}'";

		/// <summary>
		/// Default constructor.
		/// </summary>
		public WorkloadEventFileFormatter()
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

			if(logEvent is WorkloadEvent)
			{
				WorkloadEvent we = (WorkloadEvent)logEvent;
				
				output = String.Format(RecordFormat, 
									   we.Requested.ToString(TimeFormat),
									   we.NumberRequested.ToString());
			}
			return output;
		}
	}
}


