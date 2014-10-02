// *********************************************** 
// NAME                 : GazetteerEventFileFormatter
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 28/06/2004 
// DESCRIPTION  : File formatter for the GazetteerEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/GazetteerEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:22   mturner
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
	/// File formatter for the GazetteerEvent
	/// </summary>
	public class GazetteerEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// default constructor
		/// </summary>
		public GazetteerEventFileFormatter()
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

			if(logEvent is GazetteerEvent)
			{
				GazetteerEvent ge = (GazetteerEvent)logEvent;

				output =
					"TD-GE\t" + 
					ge.Time.ToString(dateTimeFormat) + "\t" +
					ge.EventCategory + "\t" +
					ge.UserLoggedOn;

				

				if(ge.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + ge.SessionId);
				}
			}
			return output;
		}
	}
}
