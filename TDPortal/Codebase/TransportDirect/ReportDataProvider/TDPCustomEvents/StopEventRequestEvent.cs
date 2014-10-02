// *********************************************** 
// NAME                 : StopEventRequestEvent.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 02/02/2005
// DESCRIPTION  : Defines a	custom event class for capturing StopEventRequest event data in CJP
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/StopEventRequestEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:32   mturner
//Initial revision.
//
//   Rev 1.1   Feb 09 2005 18:12:08   passuied
//made the objects serializable
//
//   Rev 1.0   Feb 02 2005 18:34:04   passuied
//Initial revision.

using System;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Defines a	custom event class for capturing StopEventRequest event data in CJP
	/// </summary>
	[Serializable]
	public class StopEventRequestEvent : CustomEvent
	{
		private string sRequestId = string.Empty;
		private DateTime dtSubmitted;
		private StopEventRequestType rtRequestType = StopEventRequestType.Time;
		private bool isSuccess = false;

		/// <summary>
		/// Defines the formatter class that formats event data for use by a file publisher.
		/// Used by all instances of the JourneyWebRequestEvent class.
		/// </summary>
		private static IEventFormatter fileFormatter = new StopEventRequestEventFileFormatter();
		
		/// <summary>
		/// Standard formatter for use where specialised formatters are not required.
		/// </summary>
		private static DefaultFormatter defaultFormatter = new DefaultFormatter();
		

		public StopEventRequestEvent(
			string requestId,
			DateTime submitted,
			StopEventRequestType requestType,
			bool success)
		{
			sRequestId = requestId;
			dtSubmitted = submitted;
			rtRequestType = requestType;
			isSuccess = success;
		}

		/// <summary>
		/// Read only property. Holds the Request Id info.
		/// </summary>
		public string RequestId
		{
			get{ return sRequestId;}
		}

		/// <summary>
		/// Read only property. Holds the submitted date and Time for the event.
		/// </summary>
		public DateTime Submitted
		{
			get{ return dtSubmitted; }
		}

		/// <summary>
		/// Read only property. Holds the type of request (First, Last, Time)
		/// </summary>
		public StopEventRequestType RequestType
		{
			get{ return rtRequestType;}
		}

		

		/// <summary>
		/// Read only property. Holds if request was successful.
		/// </summary>
		public bool Success
		{
			get{ return isSuccess;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}

		/// <summary>
		/// Provides an event formatting for publishing to email.
		/// </summary>
		override public IEventFormatter EmailFormatter
		{
			get {return defaultFormatter;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to event logs
		/// </summary>
		override public IEventFormatter EventLogFormatter
		{
			get {return defaultFormatter;}
		}

		/// <summary>
		/// Provides an event formatter for publishing to console.
		/// </summary>
		override public IEventFormatter ConsoleFormatter
		{
			get {return defaultFormatter;}
		}

	}
}
