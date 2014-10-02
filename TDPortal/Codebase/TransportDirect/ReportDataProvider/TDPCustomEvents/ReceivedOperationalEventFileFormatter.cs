// *************************************************************** 
// NAME                 : ReceivedOperationalEventFileFormatter.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 14/11/2003 
// DESCRIPTION  : A formatter that formats
// "received" operational events for file publishing.
// *************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/ReceivedOperationalEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:30   mturner
//Initial revision.
//
//   Rev 1.1   Nov 14 2003 12:54:22   geaton
//Updated output string to identify type of event.
//
//   Rev 1.0   Nov 14 2003 11:46:46   geaton
//Initial Revision

using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Formats "received" operational events for publishing by file.
	/// </summary>
	public class ReceivedOperationalEventFileFormatter : IEventFormatter
	{	
		// Custom datetime pattern based on ISO 8601, to resolution of milliseconds
		private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ReceivedOperationalEventFileFormatter()
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

			if(logEvent is ReceivedOperationalEvent)
			{
				ReceivedOperationalEvent roe = (ReceivedOperationalEvent)logEvent;

				output =
					"TD-ROE\t" + 
					roe.WrappedOperationalEvent.Time.ToString(dateTimeFormat) + "\t" +
					roe.WrappedOperationalEvent.Message + "\t" +
					roe.WrappedOperationalEvent.Category + "\t" +
					roe.WrappedOperationalEvent.Level + "\t" +
					roe.WrappedOperationalEvent.MachineName + "\t" +
					roe.WrappedOperationalEvent.TypeName + "\t" +
					roe.WrappedOperationalEvent.MethodName + "\t" +
					roe.WrappedOperationalEvent.AssemblyName;

				if(roe.WrappedOperationalEvent.Target != null)
				{
					output += ("\t" + roe.WrappedOperationalEvent.Target.ToString());
				}

				if(roe.WrappedOperationalEvent.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					output += ("\t" + roe.WrappedOperationalEvent.SessionId);
				}
			}
			return output;
		}
	}
}

