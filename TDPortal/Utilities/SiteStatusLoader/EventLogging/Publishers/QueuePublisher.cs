// *********************************************** 
// NAME                 : QueuePublisher.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Publishes events to the message queue
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Publishers/QueuePublisher.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:30:20   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Messaging;
using System.Text;
using AO.Common;

namespace AO.EventLogging
{
    /// <summary>
    /// Publishes events to a queue.
    /// </summary>
    public class QueuePublisher : IEventPublisher
    {
        private MessageQueue queue;
        private string identifier;

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Identifier
        {
            get { return identifier; }
        }

        /// <summary>
        /// Create a publisher that sends event details to a message queue.
        /// It is assumed that all parameters have been pre-validated.
        /// </summary>
        /// <param name="identifier">Identifier of publisher.</param>
        /// <param name="priority">Priority of messages published.</param>
        /// <param name="path">Path to the queue.</param>
        /// <param name="recoverable"><c>true</c> if messages should be recoverable otherwise <c>false</c>.</param>
        public QueuePublisher(string identifier, MessagePriority priority, string path, bool recoverable)
        {
            try
            {
                this.identifier = identifier;
                this.queue = new MessageQueue(path);

                this.queue.DefaultPropertiesToSend.Recoverable = recoverable;
                this.queue.DefaultPropertiesToSend.Priority = priority;
                this.queue.Formatter = new BinaryMessageFormatter();
            }
            catch (Exception e)
            {
                string message = String.Format(
                    Messages.ELQueuePublisherConstructor, "priority:" + priority +
                    "," + "path:" + path + "," + "recoverable:" + recoverable);

                throw new SSException(message, e, false, SSExceptionIdentifier.ELSQueuePublisherConstructor);
            }
        }

        /// <summary>
        /// Sends the given log event to the queue.
        /// </summary>
        /// <param name="logEvent"><c>LogEvent</c> to publish.</param>
        /// <exception cref="TransportDirect.Common.TDException">Log Event was not successfully written to the queue.</exception>
        public void WriteEvent(LogEvent logEvent)
        {
            try
            {
                queue.Send(logEvent, logEvent.ClassName);
            }
            catch (MessageQueueException mqe)
            {
                // thrown by Send if the path property has not been
                // set or an error occured when accessing a
                // Message Queuing API.
                
                string message = String.Format(Messages.ELQueuePublisherWriteEvent, "QueuePath:" + queue.Path);

                throw new SSException(message, mqe, false, SSExceptionIdentifier.ELSQueuePublisherWritingEvent);
            }
            catch (Exception e)
            {
                string message = String.Format(Messages.ELQueuePublisherWriteEvent, "QueuePath:" + queue.Path);

                throw new SSException(message, e, false, SSExceptionIdentifier.ELSQueuePublisherWritingEvent);
            }
        }
    }
}
