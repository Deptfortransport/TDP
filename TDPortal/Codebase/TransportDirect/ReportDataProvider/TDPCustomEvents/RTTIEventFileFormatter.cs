// *********************************************** 
// NAME                 : RTTIEventFileFormatter.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 25/01/2005 
// DESCRIPTION  		: The file formatter for RTTIEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RTTIEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:32   mturner
//Initial revision.
//
//   Rev 1.1   Mar 08 2005 15:24:28   schand
//Added copyright info at the top

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Summary description for RTTIEventFileFormatter.
	/// </summary>
	public class RTTIEventFileFormatter : IEventFormatter
	{	// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";
		
		/// <summary>
		/// default constructor
		/// </summary>
		public RTTIEventFileFormatter()
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

			if(logEvent is RTTIEvent)
			{
				RTTIEvent  rEvent = (RTTIEvent)logEvent;

				output =
					"TD-RTTIEvent\t" + 
					"Request started at: " + rEvent.StartTime.ToString(dateTimeFormat) + "\t"  +
					"Request finished at: " + rEvent.FinishTime.ToString(dateTimeFormat) + "\t"  
					+  "Data received: " + rEvent.DataReceived.ToString() +  "\t";
				
			}
			return output;
		}
	}
}
