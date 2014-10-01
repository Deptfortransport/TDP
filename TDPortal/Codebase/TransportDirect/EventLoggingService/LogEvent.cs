// *********************************************** 
// NAME                 : LogEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Abstract class for a log event.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/LogEvent.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:06   mturner
//Initial revision.
//
//   Rev 1.5   Aug 22 2003 10:17:44   geaton
//Moved ClassName property from CustomEvent into this class since common to all event types.
//
//   Rev 1.4   Jul 29 2003 17:31:30   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.3   Jul 25 2003 14:14:34   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.2   Jul 25 2003 10:05:30   geaton
//Made CLS compliancy changes
//
//   Rev 1.1   Jul 24 2003 18:27:40   geaton
//Added/updated comments

using System;
using System.Text;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Class for log events.
	/// Has serializable attribute to allow events to be published on MSMQs
	/// </summary>
	[Serializable]
	public abstract class LogEvent
	{
		/// <summary>
		/// Gets the File Formatter.
		/// </summary>
		abstract public IEventFormatter FileFormatter {get;}

		/// <summary>
		/// Gets the Event Log Formatter.
		/// </summary>
		abstract public IEventFormatter EventLogFormatter {get;}

		/// <summary>
		/// Gets the Email Formatter.
		/// </summary>
		abstract public IEventFormatter EmailFormatter {get;}

		/// <summary>
		/// Gets the Console Formatter.
		/// </summary>
		abstract public IEventFormatter ConsoleFormatter {get;}

		/// <summary>
		/// Gets the Filter.
		/// </summary>
		abstract public IEventFilter Filter {get;}


		/// <summary>
		/// Gets and sets the string containing the list of publisher class types that
		/// have published the event.
		/// </summary>
		public string PublishedBy
		{
			get {return publishedBy.ToString();}

			set {publishedBy.Append(value + " ");}
		}

		private readonly DateTime time;
		
		private StringBuilder publishedBy;

		private bool auditPublishersOff;

		/// <summary>
		/// Gets and sets audit publisher indicator.
		/// Has value <c>true</c> if auditing is performed on event, otherwise <c>false</c>.
		/// </summary>
		public bool AuditPublishersOff
		{
			get {return auditPublishersOff;}
			set {auditPublishersOff = value;}
		}

		/// <summary>
		/// Gets the time at which the event was logged.
		/// </summary>
		public DateTime Time
		{
			get {return time;}
		}

		private string className;

		/// <summary>
		/// Gets and sets the class name. The class name does not include the full namespace path.
		/// The class name is used as a mechanism for associating events with publishers in TDTraceListener.
		/// </summary>
		public string ClassName
		{
			get {return className;}
			set {className = value;}
		}

		/// <summary>
		/// Constructor used to create a log event.
		/// </summary>
		protected LogEvent()
		{
			this.time = DateTime.Now;
		
			this.publishedBy = new StringBuilder();

			this.AuditPublishersOff = true; // turn auditing off by default as it incurs runtime hit
		}

	}
}
