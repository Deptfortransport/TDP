// *********************************************** 
// NAME                 : EventLogPublisher.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Publishes events to the event log
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Publishers/EventLogPublisher.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:30:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using AO.Common;

namespace AO.EventLogging
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
            get { return identifier; }
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
            catch (ArgumentException ae)
            {
                throw new SSException(String.Format(Messages.ELEventLogPublisherConstructor, ae.Message, name, source, machine), ae, false, SSExceptionIdentifier.ELSEventLogConstructor);
            }
            catch (Exception e)
            {
                throw new SSException(String.Format(Messages.ELEventLogPublisherConstructor, e.Message, name, source, machine), e, false, SSExceptionIdentifier.ELSEventLogConstructor);
            }
        }

        /// <summary>
        /// Writes the given log event to the EventLog.
        /// </summary>
        /// <param name="logEvent"><c>LogEvent</c> to write the log entry for.</param>
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
                        switch (operationalEvent.Level)
                        {
                            case SSTraceLevel.Error:
                                entryType = EventLogEntryType.Error;
                                break;
                            case SSTraceLevel.Warning:
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
                catch (ArgumentException ae)
                {
                    throw new SSException(String.Format(Messages.ELEventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), ae, false, SSExceptionIdentifier.ELSEventLogPublisherWritingEvent);
                }
                catch (InvalidOperationException ioe)
                {
                    throw new SSException(String.Format(Messages.ELEventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), ioe, false, SSExceptionIdentifier.ELSEventLogPublisherWritingEvent);
                }
                catch (System.ComponentModel.Win32Exception we)
                {
                    throw new SSException(String.Format(Messages.ELEventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), we, false, SSExceptionIdentifier.ELSEventLogPublisherWritingEvent);
                }
                catch (SystemException se)
                {
                    throw new SSException(String.Format(Messages.ELEventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), se, false, SSExceptionIdentifier.ELSEventLogPublisherWritingEvent);
                }
                catch (Exception e)
                {
                    throw new SSException(String.Format(Messages.ELEventLogPublisherWriteEvent, this.eventLog.Log, this.eventLog.Source, this.eventLog.MachineName), e, false, SSExceptionIdentifier.ELSEventLogPublisherWritingEvent);
                }
            }
        }
    }
}
