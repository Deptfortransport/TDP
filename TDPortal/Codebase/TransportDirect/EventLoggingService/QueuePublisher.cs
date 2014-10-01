// *********************************************** 
// NAME                 : QueuePublisher.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 07/07/2003 
// DESCRIPTION  : A publisher that publishes
// events to a Queue.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/QueuePublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:08   mturner
//Initial revision.
//
//   Rev 1.5   Oct 08 2003 13:07:38   geaton
//Used the LogEvent class name as the message label - this may assist during debugging.
//
//   Rev 1.4   Oct 07 2003 13:40:46   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.3   Aug 21 2003 11:02:54   geaton
//Provided a default label for messages sent to queue.
//
//   Rev 1.2   Aug 21 2003 09:07:28   geaton
//Changed WriteEvent method so that event is sent to queue without a message label or casting to a specific event class. This has been done for performance.
//
//   Rev 1.1   Jul 24 2003 18:27:50   geaton
//Added/updated comments

using System;
using System.Messaging;

namespace TransportDirect.Common.Logging
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
			get {return identifier;}
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
			catch(Exception e)
			{
				string message = String.Format(
					Messages.QueuePublisherConstructor, "priority:" + priority +
					"," + "path:" + path + "," + "recoverable:" + recoverable);

				throw new TDException(message, e, false, TDExceptionIdentifier.ELSQueuePublisherConstructor);
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
			catch(MessageQueueException mqe)
			{
				// thrown by Send if the path property has not been
				// set or an error occured when accessing a
				// Message Queuing API.
				string message = String.Format(Messages.QueuePublisherWriteEvent, "QueuePath:" + queue.Path);

				throw new TDException(message, mqe, false, TDExceptionIdentifier.ELSQueuePublisherWritingEvent);
			}
			catch(Exception e)
			{
				string message = String.Format(Messages.QueuePublisherWriteEvent, "QueuePath:" + queue.Path);

				throw new TDException(message, e, false, TDExceptionIdentifier.ELSQueuePublisherWritingEvent);
			}	
		}
	}
}
