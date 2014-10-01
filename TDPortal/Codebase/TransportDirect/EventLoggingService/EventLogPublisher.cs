// *********************************************** 
// NAME                 : EventLogPublisher.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 04/07/2003 
// DESCRIPTION  : A publisher that publishes
// events to the Event Log.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/EventLogPublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:02   mturner
//Initial revision.
//
//   Rev 1.8   Nov 13 2003 18:18:30   geaton
//Updated event log constructor exception message so that valid variables are used in it.
//
//   Rev 1.7   Nov 03 2003 15:44:22   geaton
//Provided an event id when publishing - to distinguish between operational and custom events logged.
//
//   Rev 1.6   Oct 29 2003 20:30:18   geaton
//Use custom event formatter without testing for null.
//
//   Rev 1.5   Oct 21 2003 15:14:30   geaton
//Changes resulting from removal of Event Log Entry Type from properties. (This is now derived from TDTraceLevel of event being logged.)
//
//   Rev 1.4   Oct 21 2003 11:36:00   geaton
//Added thread-safety code and tidied up exception messages. Removed redundant member variables.
//
//   Rev 1.3   Oct 07 2003 13:40:40   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.2   Jul 25 2003 14:14:30   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.1   Jul 24 2003 18:27:32   geaton
//Added/updated comments following intial version by Kenny

using System;
using System.Diagnostics;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Publishes events to the Event Log.
	/// </summary>
	public class EventLogPublisher : IEventPublisher
	{
		private string identifier;
		private EventLog eventLog;
		private const int OperationalEventId = 1;
		private const int CustomEventId = 2;
	
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		public string Identifier
		{
			get {return identifier;}
		}

		/// <summary>
		/// Create a publisher that writes event details to the Event Log.
		/// It is assumed that all parameters have been pre-validated.
		/// This constructor will additionally create an Event Source for the
		/// Event Log if one does not already exist.
		/// </summary>
		/// <param name="identifier">Identifier of event log.</param>
		/// <param name="name">Name of the event log to publish to.</param>
		/// <param name="source">Name of the source for the event log.</param>
		/// <param name="machine">Name of the machine on which the event log resides.</param>
		/// <exception cref="TransportDirect.Common.TDException">Error creating the class.</exception>
		public EventLogPublisher(string identifier, string name, string source, string machine)
		{
			try
			{
				this.identifier = identifier;

				// Create a source for the event log if it does not already have one.
                if (!EventLog.SourceExists(source, machine))
                {
                    EventSourceCreationData sourceData = new EventSourceCreationData(source, name);
                    EventLog.CreateEventSource(sourceData);
                }

				// Associate EventLog instance with publisher properties.
				eventLog = new EventLog(name, machine, source);
				
			}
			catch(ArgumentException ae)
			{	
				throw new TDException(String.Format(Messages.EventLogPublisherConstructor, ae.Message, name, source, machine), ae, false, TDExceptionIdentifier.ELSEventLogConstructor);
			}
			catch(Exception e)
			{
				throw new TDException(String.Format(Messages.EventLogPublisherConstructor, e.Message, name, source, machine), e, false, TDExceptionIdentifier.ELSEventLogConstructor);
			}
		}
		
		/// <summary>
		/// Writes the given log event to the EventLog.
		/// </summary>
		/// <param name="logEvent"><c>LogEvent</c> to write the log entry for.</param>
		/// <exception cref="TransportDirect.Common.TDException">Log Event was not successfully written to the event log.</exception>
		public void WriteEvent(LogEvent logEvent)
		{
			lock (eventLog)
			{
				try
				{
					if (logEvent is OperationalEvent)
					{
						OperationalEvent operationalEvent = (OperationalEvent)logEvent;
			
						// Derive entry type based on event level.
						EventLogEntryType entryType;
						switch(operationalEvent.Level)
						{
							case TDTraceLevel.Error:
								entryType = EventLogEntryType.Error;
								break;
							case TDTraceLevel.Warning:
								entryType = EventLogEntryType.Warning;
								break;
							default:
								entryType = EventLogEntryType.Information;
								break;
						}

						eventLog.WriteEntry(operationalEvent.EventLogFormatter.AsString(operationalEvent), entryType, OperationalEventId);

					}
					else
					{
						// All custom events are logged as Information entry type.
						eventLog.WriteEntry(logEvent.EventLogFormatter.AsString(logEvent), EventLogEntryType.Information, CustomEventId);
						
					}
				
				}
				catch(ArgumentException ae)
				{
					throw new TDException(String.Format(Messages.EventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), ae, false, TDExceptionIdentifier.ELSEventLogPublisherWritingEvent);
				}
				catch(InvalidOperationException ioe)
				{
					throw new TDException(String.Format(Messages.EventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), ioe, false, TDExceptionIdentifier.ELSEventLogPublisherWritingEvent);
				}
				catch(System.ComponentModel.Win32Exception we)
				{
					throw new TDException(String.Format(Messages.EventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), we, false, TDExceptionIdentifier.ELSEventLogPublisherWritingEvent);
				}
				catch(SystemException se)
				{
					throw new TDException(String.Format(Messages.EventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), se, false, TDExceptionIdentifier.ELSEventLogPublisherWritingEvent);
				}
				catch(Exception e)
				{
					throw new TDException(String.Format(Messages.EventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), e, false, TDExceptionIdentifier.ELSEventLogPublisherWritingEvent);
				}
			}
		}
	}
}
