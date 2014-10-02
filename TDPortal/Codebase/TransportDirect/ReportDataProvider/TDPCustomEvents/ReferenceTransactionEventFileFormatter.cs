// **************************************************************** 
// NAME                 : ReferenceTransactionEventFileFormatter.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 6/11/2003 
// DESCRIPTION  : Formats event data for file publisher.
// ****************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/ReferenceTransactionEventFileFormatter.cs-arc  $
//
//   Rev 1.1   Jun 28 2010 14:07:48   PScott
//SCR 5561 - write MachineName to reference transactions
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.0   Nov 08 2007 12:39:30   mturner
//Initial revision.
//
//   Rev 1.3   Feb 16 2004 17:34:26   geaton
//Incident 643. Changed format to allow data to be imported into database easily. This change negates the need for a separate Transaction Injector File Publisher.
//
//   Rev 1.2   Dec 02 2003 20:07:08   geaton
//Updates following addition of Successful column on ReferenceTransactionEvent table.
//
//   Rev 1.1   Nov 10 2003 12:30:16   geaton
//Change to support category becoming type string.
//
//   Rev 1.0   Nov 06 2003 12:18:02   geaton
//Initial Revision
 

using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Formats reference transaction events for publishing by file.
	/// </summary>
	public class ReferenceTransactionEventFileFormatter : IEventFormatter
	{	
		private const string TimeFormat = "yyyy-MM-dd HH:mm:ss.fff000000";
        private const string RecordFormat = "1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}'";

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ReferenceTransactionEventFileFormatter()
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

			if (logEvent is ReferenceTransactionEvent)
			{
				ReferenceTransactionEvent rte = (ReferenceTransactionEvent)logEvent;

				output = String.Format(RecordFormat, 
									   rte.EventType,
									   rte.ServiceLevelAgreement.ToString(),
									   rte.Submitted.ToString(TimeFormat),
									   rte.SessionId,
									   rte.Time.ToString(TimeFormat),
									   rte.Successful.ToString(),
                                       rte.MachineName);	
			}

			return output;
		}
	}
}



