// *************************************************************************** 
// NAME                 : RTTILoadLevel.cs
// AUTHOR               : Tolu Olomolaiye
// DATE CREATED         : 26 Jan 2006 
// DESCRIPTION  		: Event class for extra RTTI logging information
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RTTIInternalEvent.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:39:32   mturner
//Initial revision.
//
//   Rev 1.2   Apr 08 2006 13:00:10   rwilby
//Added required Serializable attribute to class.
//Resolution for 3630: Regr: RTTI Logging:  Event Logging - Run Report Data Importer fails when run
//
//   Rev 1.1   Apr 07 2006 10:21:32   rwilby
//Updated to override IEventFormatter mehtod on base class.
//Resolution for 3630: Regr: RTTI Logging:  Event Logging - Run Report Data Importer fails when run
//
//   Rev 1.0   Feb 20 2006 16:45:28   tolomolaiye
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// RTTI extra logging event class
	/// </summary>
	[Serializable]
	public class RTTIInternalEvent : TDPCustomEvent
	{
		private DateTime startDateTime;
		private DateTime endDateTime;
		private int numberOfRetries;
		private bool loggingSuccessful;
		private static RTTIInternalEventFileFormatter  fileFormatter = new RTTIInternalEventFileFormatter();

		/// <summary>
		/// Constructor for the RTTI event logging
		/// </summary>
		/// <param name="startDateTime">Time logging started</param>
		/// <param name="endDateTime">Time logging ended</param>
		/// <param name="numberOfRetries">Number of retries for the connection</param>
		/// <param name="loggingSuccessful">boolean to indicate whether the connection was successful</param>
		public RTTIInternalEvent(DateTime startDateTime, DateTime endDateTime, int numberOfRetries, bool loggingSuccessful): base(string.Empty, false)
		{
			this.startDateTime = startDateTime;
			this.endDateTime = endDateTime;
			this.numberOfRetries = numberOfRetries;
			this.loggingSuccessful = loggingSuccessful;
		}

		/// <summary>
		/// Get the start time
		/// </summary>
		public DateTime StartDateTime
		{
			get{return startDateTime;}
		}

		/// <summary>
		/// Get the end time
		/// </summary>
		public DateTime EndDateTime
		{
			get {return endDateTime;}
		}

		/// <summary>
		/// Get the number of retries
		/// </summary>
		public int NumberOfRetries
		{
			get {return numberOfRetries;}
		}

		/// <summary>
		/// Was logging successful?
		/// </summary>
		public bool LoggingSuccessful
		{
			get {return loggingSuccessful;}
		}

		/// <summary>
		/// Read-Only property provides an event formatter for publishing to files.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}
	}
}