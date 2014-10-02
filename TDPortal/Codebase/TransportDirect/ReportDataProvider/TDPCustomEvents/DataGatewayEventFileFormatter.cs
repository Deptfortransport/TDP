// ********************************************************* 
// NAME                 : DataGatewayEventFileFormatter.cs
// AUTHOR               : Gary Eaton
// DATE CREATED         : 29/10/2003 
// DESCRIPTION  : File formatter class for datagateway custom
// events.
// ********************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/DataGatewayEventFileFormatter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:18   mturner
//Initial revision.
//
//   Rev 1.0   Oct 29 2003 19:58:24   geaton
//Initial Revision

using System;
using System.Globalization;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Formats data gateway events for publishing by file.
	/// </summary>
	public class DataGatewayEventFileFormatter : IEventFormatter
	{	
		/// <summary>
		/// Default constructor.
		/// </summary>
		public DataGatewayEventFileFormatter()
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

			if(logEvent is DataGatewayEvent)
			{
				DataGatewayEvent dge = (DataGatewayEvent)logEvent;
				string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

				string outputFormat = "TD-DGE	FeedId:[{0}]	Filepath:[{1}]	TimeStarted:[{2}]	TimeFinished:[{3}]	SuccessFlag:[{4}]	ErrorCode:[{5}]";
				
				output = String.Format(outputFormat, dge.FeedId, dge.FileName, dge.TimeStarted.ToString(dateTimeFormat), dge.TimeFinished.ToString(dateTimeFormat), dge.SuccessFlag.ToString(), dge.ErrorCode);				
			}
			return output;
		}
	}
}



