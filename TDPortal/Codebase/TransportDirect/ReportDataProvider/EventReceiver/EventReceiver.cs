// *********************************************** 
// NAME                 : EventReceiver.cs 
// AUTHOR               : Jatinder S. Toor
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  :  Builds a list of MSMQs 
// and then sets up an event handler to receive 
// messages received by those queues.
// Logs the messages to relevant logger as specified
// in the properties.xml file.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/EventReceiver.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:32   mturner
//Initial revision.
//
//   Rev 1.14   Sep 23 2004 11:49:30   jgeorge
//Added code to clear ArrayList of queues as part of recovery. This prevents the memory leak/multiple logging problems.
//Resolution for 1476: Event receiver causing virtual memory shortage DEL 6
//
//   Rev 1.13   Jul 05 2004 11:20:48   passuied
//changes for EventReceiver Recovery
//
//   Rev 1.12   Jan 09 2004 15:02:34   geaton
//Improved exception handling when receiving a message.
//
//   Rev 1.11   Nov 06 2003 19:54:14   geaton
//Removed redundant key.
//
//   Rev 1.10   Oct 30 2003 12:40:36   geaton
//Removed log of a verbose message in the Receive method. This will have caused recursion if this event was logged to the same queue that was being received from.
//
//   Rev 1.9   Oct 30 2003 12:26:42   geaton
//Log any OperationalEvents received as ReceivedOperationalEvents (to allow configuration of those received and those logged to vary).
//
//   Rev 1.8   Oct 10 2003 15:22:54   geaton
//Updated error handling and validation.
//
//   Rev 1.7   Oct 10 2003 08:33:08   geaton
//Removed reference to datagateway project
//
//   Rev 1.6   Oct 09 2003 20:04:46   geaton
//Updated trace messages.
//
//   Rev 1.5   Oct 09 2003 12:33:46   geaton
//Tidied up error handling and added verbose messages to assist in debugging.
//
//   Rev 1.4   Oct 08 2003 15:50:40   JTOOR
// 
//
//   Rev 1.3   Sep 05 2003 09:49:36   jtoor
//Changes made to comply with Code Review.
//
//   Rev 1.2   Aug 29 2003 11:11:44   jtoor
// 
//
//   Rev 1.1   Aug 28 2003 09:39:12   jtoor
// 
//
//   Rev 1.0   Aug 22 2003 11:49:32   jtoor
//Initial Revision

using System;
using System.Collections;
using System.Messaging;
using System.Diagnostics;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.ReportDataProvider.DatabasePublishers;


namespace TransportDirect.ReportDataProvider.EventReceiver
{	
	/// <summary>
	/// Monitors queues it is responsible for and 
	/// logs them using the event logging service
	/// </summary>
	public class EventReceiver
	{
		private ArrayList						messageQueueList;
		private ReceiveCompletedEventHandler	receiveEventHandler;
		private bool							disposed = false;

		/// <summary>
		/// Disposes of resources. 
		/// Can be called by clients (via Dispose()) or runtime (via destructor).
		/// </summary>
		/// <param name="disposing">
		/// True when called by clients.
		/// False when called by runtime.
		/// </param>
		public void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				if (disposing)
				{
					// Dispose of any managed resources:	
					StopMessageQueues();
								
				}
             
				// Dispose of any unmanaged resources:
				
			}

			this.disposed = true;
		}

		private void StopMessageQueues()
		{
			// releases all queues
			
			// Unregister the eventhandler from all the queues in our list
			// and the remove from the list
			while (messageQueueList.Count > 0)
			{
				((MessageQueue)messageQueueList[0]).ReceiveCompleted -= receiveEventHandler;
				((MessageQueue)messageQueueList[0]).Close();
				messageQueueList.RemoveAt(0);
				
			}
			MessageQueue.ClearConnectionCache();
		}

	
		/// <summary>
		/// Disposes of pool resources.
		/// Allows clients to dispose of resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); // take off finalization queue to prevent dispose being called again.
		}

		/// <summary>
		/// Class destructor.
		/// </summary>
		~EventReceiver()      
		{
			Dispose(false);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public EventReceiver()
		{
			receiveEventHandler	= null;
		}

		/// <summary>
		/// Starts message queue polling.
		/// </summary>
		public void Run()
		{
			this.Run( Properties.Current );						
		}
        
		/// <summary>
		/// Starts message queue polling.
		/// This prototype is used for unit testing to allow mock properties to be passed in.
		/// </summary>
		/// <param name="pp">Properties.</param>
		public void Run( IPropertyProvider pp )
		{					
			InitQueueList(pp);
			SetupQueues();	// has effect of starting queue polling				
		}

		/// <summary>
		/// Initialise the internal list of queues to support.
		/// The list of queues is obtained from the properties.
		/// </summary>
		/// <param name="pp">Properties used to initialise queues.</param>
		private void InitQueueList(IPropertyProvider properties)
		{
			messageQueueList = new ArrayList();

			string[] idList = properties[Keys.ReceiverQueue].Split( ' ' );
				
			string queue = null;
			
			foreach( string id in idList )
			{
				if( id != " " )
				{							
					queue = properties[ string.Format( Keys.ReceiverQueuePath, id ) ];

					messageQueueList.Add( new MessageQueue( queue ) );
				}
			}

			if (TDTraceSwitch.TraceVerbose)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, String.Format(Messages.Service_EstablishedQueues, messageQueueList.Count)));

		}

		private bool recover = false;
		/// <summary>
		/// Closes and releases all queues. Then Re Init the queues.
		/// </summary>
		private void RecoverFromRemoteException()
		{
			// First Stop MessageQueues
			StopMessageQueues();

			// Sleep before trying to re init again.
			// This is if network not available for a long time
			// so it does not loop too many times

			int sleep = Convert.ToInt32(Properties.Current[Keys.TimeBeforeRecovery]);
			if (sleep == 0)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, Messages.Service_FailedRetrievingProperty));
				throw new TDException(Messages.Service_FailedRetrievingProperty, true, TDExceptionIdentifier.PSMissingProperty);
			}
			Thread.Sleep(sleep); 
			// Then Re Init all queues
			Run();



		}
		/// <summary>
		/// Setup the queues in our list to receive messages.
		/// </summary>
		/// <exception cref="TDException">
		/// Failure to set up a queue.
		/// </exception> 
		private void SetupQueues()
		{
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.Service_SettingQueueHandlers));

			int numQueues = messageQueueList.Count;

			receiveEventHandler = new ReceiveCompletedEventHandler( Receive );
			
			try
			{
				foreach( TraceListener listener in Trace.Listeners)
				{
					if (listener is TDTraceListener)
						((TDTraceListener)listener).DefaultPublisherCalled += new DefaultPublisherCalledEventHandler(OnDefaultPublisherCalled);

				}
			}
			catch(Exception e)
			{
				EventLog.WriteEntry("My event", string.Format("Unable to subscribe to the event. Reason:[{0}]",e.Message),EventLogEntryType.Error);
			}
			for( int i=0; i < numQueues; i++ )
			{
				try
				{
					((MessageQueue)messageQueueList[i]).Formatter = new BinaryMessageFormatter();				 				
					((MessageQueue)messageQueueList[i]).ReceiveCompleted += receiveEventHandler;
					((MessageQueue)messageQueueList[i]).BeginReceive();

					// the recover variable is set to true 
					// if the first Set up succeeds
					recover = true;
				}
				catch (MessageQueueException messageQueueException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailedSettingHandler, messageQueueException.Message)+ string.Format(Messages.Service_RecoveryMessage, Properties.Current[Keys.TimeBeforeRecovery])));
					// if first setup fails recover still set to false
					// we don't want to recover
					if (!recover)
						throw new TDException(String.Format(Messages.Service_FailedSettingHandler, messageQueueException.Message), messageQueueException, true, TDExceptionIdentifier.RDPEventReceiverQueueReceiveInitFailed);
					else
						RecoverFromRemoteException();
				}
			}
		
		}

		private void OnDefaultPublisherCalled(object sender, DefaultPublisherCalledEventArgs e)
		{
			StopMessageQueues();

			LogEvent le = e.LogEvent;
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format(Messages.Service_FailureWhenPublishingEvent, le.ClassName)));
		}
		
		/// <summary>
		/// Global event handler used for all queues.
		/// </summary>
		/// <remarks>
		/// Any exceptions caught are NOT re-thrown since component is
		/// running as a service and exceptions will not reach client.
		/// Events that do not relate to exceptions MUST NOT be logged
		/// - since recursion may result if event logged to same queue
		/// as that being serviced.
		/// </remarks>
		/// <param name="source">Source queue.</param>
		/// <param name="asyncResult">Event data.</param>
		private void Receive( Object source, ReceiveCompletedEventArgs asyncResult )
		{	
			MessageQueue mq	 = (MessageQueue)source;
			Message		 msg = null;

			try
			{
				msg = mq.EndReceive(asyncResult.AsyncResult);

				// ensure that message body is of correct class type
				LogEvent le = (LogEvent)msg.Body;

				// Publish message body using the configured publisher.
				// Wrap Operational Events received to allow Operational Events 
				// generated by Event Receiver to be configured differently to those received.
				if (le is OperationalEvent)
					Trace.Write(new ReceivedOperationalEvent((OperationalEvent)le));
				else
					Trace.Write(le);
				

				// Note: Only poll queue for next message if the previous message was processed successfully.
				// This prevents more messages being potentially lost if the problem repeats itself.
				try
				{
					mq.BeginReceive();
				}
				catch (MessageQueueException messageQueueException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailedPollingQueue, messageQueueException.Message)));
				}
			

			}
			catch (ArgumentNullException argumentNullException)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailureReceivingMessage , argumentNullException.Message)+ string.Format(Messages.Service_RecoveryMessage, Properties.Current[Keys.TimeBeforeRecovery])));
				RecoverFromRemoteException();
			}
			catch (ArgumentException argumentException)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailureReceivingMessage, argumentException.Message)+ string.Format(Messages.Service_RecoveryMessage, Properties.Current[Keys.TimeBeforeRecovery])));
				RecoverFromRemoteException();
			}
			catch (MessageQueueException messageQueueException)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailureReceivingMessage, messageQueueException.Message)+ string.Format(Messages.Service_RecoveryMessage, Properties.Current[Keys.TimeBeforeRecovery])));
				RecoverFromRemoteException();
			}
			catch (InvalidOperationException invalidOperationException)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailedExtractingMessageBody, invalidOperationException.Message)+ string.Format(Messages.Service_RecoveryMessage, Properties.Current[Keys.TimeBeforeRecovery])));
				RecoverFromRemoteException();
			}
			catch (InvalidCastException)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_UnknownEventReceived)+ string.Format(Messages.Service_RecoveryMessage, Properties.Current[Keys.TimeBeforeRecovery])));
				RecoverFromRemoteException();
			}
			catch (TDException tdException)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_EventPublishFail, tdException.Message)+ string.Format(Messages.Service_RecoveryMessage, Properties.Current[Keys.TimeBeforeRecovery])));
				RecoverFromRemoteException();
			}
			catch (Exception exception)
			{
				// Catch undocumented exceptions.
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailureWhenProcessingMessage, exception.Message)+ string.Format(Messages.Service_RecoveryMessage, Properties.Current[Keys.TimeBeforeRecovery])));
				RecoverFromRemoteException();
			}

		}
	}
}
