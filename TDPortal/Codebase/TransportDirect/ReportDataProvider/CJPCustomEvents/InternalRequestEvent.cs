// *********************************************** 
// NAME                 : InternalRequestEvent.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 30/06/2004
// DESCRIPTION  : Defines a custom event class
// for capturing internal request event data in
// the CJP (ie calls to TTBO/road engine)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/CJPCustomEvents/InternalRequestEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:06   mturner
//Initial revision.
//
//   Rev 1.2   Jan 24 2005 14:00:52   jgeorge
//Del 7 modifications
//
//   Rev 1.1   Jul 02 2004 14:54:16   jgeorge
//Added Serializable attribute to enable queue publishing
//
//   Rev 1.0   Jul 02 2004 13:51:00   jgeorge
//Initial revision.

using System;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using LoggingMessages = TransportDirect.Common.Logging.Messages;

namespace TransportDirect.ReportDataProvider.CJPCustomEvents
{
	/// <summary>
	/// Defines a custom event class for capturing internal request 
	/// event data in the CJP (ie calls to TTBO/road engine)
	/// </summary>
	[Serializable]
	public class InternalRequestEvent : CustomEvent
	{
		private string internalRequestId;
		private string sessionId;
		private DateTime submitted;
		private InternalRequestType requestType;
		private string functionType;
		private bool success;
		private bool refTransaction;

		/// <summary>
		/// Defines the formatter class that formats event data for use by a file publisher.
		/// Used by all instances of the JourneyWebRequestEvent class.
		/// </summary>
		private static IEventFormatter fileFormatter = new InternalRequestEventFileFormatter();
		
		/// <summary>
		/// Standard formatter for use where specialised formatters are not required.
		/// </summary>
		private static DefaultFormatter defaultFormatter = new DefaultFormatter();

		/// <summary>
		/// Gets the session identifier.
		/// </summary>
		public string SessionId
		{
			get{return sessionId;}
		}

		/// <summary>
		/// Gets the journey web request identifier.
		/// </summary>
		public string InternalRequestId
		{
			get{return internalRequestId;}
		}

		/// <summary>
		/// Gets the time that the journey web request was submitted.
		/// </summary>
		public DateTime Submitted
		{
			get{return submitted;}
		}
		
		/// <summary>
		/// Gets the region code.
		/// </summary>
		public InternalRequestType RequestType
		{
			get{return requestType;}
		}

		/// <summary>
		/// Gets the function type code
		/// </summary>
		public string FunctionType
		{
			get{return functionType;}
		}
		
		/// <summary>
		/// Gets the success indicator.
		/// A value of true indicates that the journey web request was successful.
		/// </summary>
		public bool Success
		{
			get{return success;}
		}

		/// <summary>
		/// Gets the reference transaction indicator.
		/// A value of true indicates that a reference transaction 
		/// was used to trigger the journey web request.
		/// </summary>
		public bool RefTransaction
		{
			get{return refTransaction;}
		}

		
		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="sessionId">Identifier to uniquely identify the session under which this journey web request was made.</param>
		/// <param name="internalRequestId">Identifier to uniquely identify this request.</param>
		/// <param name="submitted">Time that journey web request was submitted.</param>
		/// <param name="requestType">The type of request</param>
		/// <param name="functionType">The function code. Maximum two characters</param>
		/// <param name="success">True if request was successful, otherwise false.</param>
		/// <param name="refTransaction">True if request was triggered by a reference transaction, otherwise false.</param>
		public InternalRequestEvent(string sessionId,
			string internalRequestId,
			DateTime submitted,
			InternalRequestType requestType,
			string functionType,
			bool success,
			bool refTransaction) : base()
		{
			this.sessionId = sessionId;
			this.internalRequestId = internalRequestId;
			this.submitted = submitted;

			if (functionType.Length != 2)
			{
				// Throw/log an error
				TDException e = new TDException( String.Format(LoggingMessages.InternalRequestEventInvalidFunctionType, functionType), true, TDExceptionIdentifier.ELSInvalidFunctionType );
				Trace.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message) );
				throw e;				
			}

			this.functionType = functionType;
			this.success = success;
			this.requestType = requestType;
			this.refTransaction = refTransaction;
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

