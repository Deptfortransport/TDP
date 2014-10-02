// *************************************************************************** 
// NAME                 : RTTILoadLevelEventFileFormatter.cs
// AUTHOR               : Tolu Olomolaiye
// DATE CREATED         : 26 Jan 2006 
// DESCRIPTION  		: The file formatter for RTTILoadlevelEvent
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RTTIInternalEventFileFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:32   mturner
//Initial revision.
//
//   Rev 1.1   Apr 07 2006 10:18:12   rwilby
//Corrected to implement  IEventFormatter interface
//Resolution for 3630: Regr: RTTI Logging:  Event Logging - Run Report Data Importer fails when run
//
//   Rev 1.0   Feb 20 2006 16:45:28   tolomolaiye
//Initial revision.

using System;
using System.Text;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Summary description for RTTILoadLevelEventFileFormatter.
	/// </summary>
	public class RTTIInternalEventFileFormatter : IEventFormatter
	{
		//specify format for the datetime variables
		private const string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// Default constructor for the class
		/// </summary>
		public RTTIInternalEventFileFormatter()
		{
		}

		/// <summary>
		/// Converts a Boolean value to an integer value of either 0 (false) or 1 (true) 
		/// </summary>
		/// <param name="booleanValue"></param>
		/// <returns>0 or 1</returns>
		private int BooleanToDisplayText(bool booleanValue)
		{
			return (booleanValue ? 1 : 0);
		}

		/// <summary>
		/// Formats the given log event.
		/// </summary>
		/// <param name="logEvent">Log Event to format.</param>
		/// <returns>A formatted string representing the log event.</returns>
		public string AsString(LogEvent logEvent)
		{
			string eventValue = String.Empty;

			if(logEvent is RTTIInternalEvent)
			{
				RTTIInternalEvent  loadEvent = (RTTIInternalEvent)logEvent;
				StringBuilder outputString = new StringBuilder();

				outputString.Append("TD-RTTIInternalEvent\t");
				outputString.Append("Started at: "); 
				outputString.Append(loadEvent.StartDateTime.ToString(dateTimeFormat));
				outputString.Append("\t");
				outputString.Append("Finished at: "); 
				outputString.Append(loadEvent.EndDateTime.ToString(dateTimeFormat));
				outputString.Append("\t");
				outputString.Append("NumberOfRetries: "); 
				outputString.Append(loadEvent.NumberOfRetries.ToString());
				outputString.Append("\t");
				outputString.Append("LoggingSuccessful: "); 
				outputString.Append(BooleanToDisplayText(loadEvent.LoggingSuccessful));
				outputString.Append("\t");
				
				eventValue = outputString.ToString();
			}

			return eventValue;
		}
	}
}