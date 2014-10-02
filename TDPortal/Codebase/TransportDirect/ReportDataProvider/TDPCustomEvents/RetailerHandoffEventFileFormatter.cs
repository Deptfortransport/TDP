// *********************************************** 
// NAME                 : RetailerHandoffEventFileFormatter
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 28/06/2004 
// DESCRIPTION  : File formatter for the RetailerHandoffEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RetailerHandoffEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:30   mturner
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
	/// Summary description for RetailerHandoffEventFileFormatter.
	/// </summary>
	public class RetailerHandoffEventFileFormatter : IEventFormatter
	{
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// default constructor
		/// </summary>
		public RetailerHandoffEventFileFormatter()
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

			if(logEvent is RetailerHandoffEvent)
			{
				RetailerHandoffEvent rhe = (RetailerHandoffEvent)logEvent;

				output =
					"TD-RHE\t" + 
					rhe.Time.ToString(dateTimeFormat) + "\t" +
					rhe.RetailerId + "\t" +
					rhe.UserLoggedOn;

				

				if(rhe.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + rhe.SessionId);
				}
			}
			return output;
		}
	}
}
