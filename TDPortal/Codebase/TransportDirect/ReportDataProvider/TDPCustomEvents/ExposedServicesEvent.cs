// *********************************************** 
// NAME                 : ExposedServicesEvent.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 07/02/2005
// DESCRIPTION  : Logging event for Exposed services
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/ExposedServicesEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:20   mturner
//Initial revision.
//
//   Rev 1.1   Feb 09 2005 18:12:12   passuied
//made the objects serializable
//
//   Rev 1.0   Feb 08 2005 11:05:46   passuied
//Initial revision.

using System;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Logging event for Exposed services
	/// </summary>
	[Serializable]
	public class ExposedServicesEvent : CustomEvent
	{
		private string sToken = string.Empty;
		private DateTime dtSubmitted;
		private ExposedServicesCategory esCategory;
		private bool isSuccess = false;

		/// <summary>
		/// Defines the formatter class that formats event data for use by a file publisher.
		/// Used by all instances of the JourneyWebRequestEvent class.
		/// </summary>
		private static IEventFormatter fileFormatter = new ExposedServicesEventFileFormatter();
		
		/// <summary>
		/// Standard formatter for use where specialised formatters are not required.
		/// </summary>
		private static DefaultFormatter defaultFormatter = new DefaultFormatter();

		public ExposedServicesEvent(string token, DateTime submitted, ExposedServicesCategory category, bool success)
		{
			sToken  = token;
			dtSubmitted = submitted;
			esCategory = category;
			isSuccess = success;
		}

		/// <summary>
		/// Read-only property. authentication token
		/// </summary>
		public string Token
		{
			get{ return sToken;}
		}

		/// <summary>
		/// Read-only property. date and time event was submitted
		/// </summary>
		public DateTime Submitted
		{
			get{ return dtSubmitted;}
		}

		/// <summary>
		/// Read-only property. category of exposedServices this event is associated with
		/// </summary>
		public ExposedServicesCategory Category
		{
			get{ return esCategory;} 
		}

		/// <summary>
		/// Read-only property. Was the call to the exposed services successful.
		/// </summary>
		public bool Successful
		{
			get { return isSuccess;}
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
