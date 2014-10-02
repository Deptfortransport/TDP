// *************************************************
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 5/11/2003 
// DESCRIPTION			: Container for messages
// used by classes in Event Receiver component.
// ************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/Messages.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:36   mturner
//Initial revision.
//
//   Rev 1.3   Jul 05 2004 11:20:48   passuied
//changes for EventReceiver Recovery
//
//   Rev 1.2   Jan 09 2004 15:03:16   geaton
//Updated messages to support exception handling changes.
//
//   Rev 1.1   Nov 07 2003 08:57:18   geaton
//Added message to support cryto failure.
//
//   Rev 1.0   Nov 06 2003 19:53:46   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.EventReceiver
{
	public class Messages
	{
		// Initialisation Messages
		public const string Init_TDTraceListenerFailed = "Failed to initialise the TD Trace Listener class. Exception ID:[{0}]. Reason[{1}].";
		public const string Init_UnknownPropertyKey = "Attempt to validate unknown property key: [{0}].";
		public const string Init_InvalidPropertyKeys = "Missing or invalid Event Receiver property keys found on initialisation: [{0}].";
		public const string Init_Completed = "Initialisation of Event Receiver completed successfully.";
		public const string Init_Failed = "Infrastructure\n{0}"; // OperationalEvent category prefixed for TNG to parse Event Log Description field.
		public const string Init_PropertyServiceFailed = "Failed to initialise the TD Property Service: [{0}]";
		public const string Init_EventConfigMissing = "Event Receiver is not configured to publish events of type [ReceivedOperationalEvent]. This configuration is mandatory in order to publish received Operational Events.";
		public const string Init_CryptographicServiceFailed = "Failed to initialise the TD Cryptographic Service: [{0}]";

		
		// Service Messages
		public const string Service_Starting = "Starting Event Receiver Service.";
		public const string Service_Stopping = "Stopping Event Receiver Service.";
		public const string Service_StartedTestMode = "Event Receiver Service started successfully in test mode. No further processing is in progress.";
		public const string Service_BadParam = @"Unknown parameter passed when starting service. Valid parameters are: /test";
		public const string Service_FailedRunning = "Failure when running the event receiver class: [{0}]";
		public const string Service_EstablishedQueues = "Established list of [{0}] queues to service.";
		public const string Service_SettingQueueHandlers = "Setting up event handlers for queues.";
		public const string Service_FailedSettingHandler = "Failed to set up a handling for the queue. Reason: [{0}]";
		public const string Service_UnknownEventReceived = "Object not of type LogEvent received from queue. Object has NOT been logged by any publisher!";
		public const string Service_EventPublishFail = "Failed to log event received from queue. Event has NOT been logged by any publisher! Reason: [{0}]";
		public const string Service_FailureReceivingMessage = "Failure when receiving message from queue. Reason: [{0}]";
		public const string Service_FailedExtractingMessageBody = "Failed to read content of message received from queue. Reason: [{0}]";
		public const string Service_FailedPollingQueue = "Failed to poll the queue. Reason: [{0}]";
		public const string Service_FailureWhenProcessingMessage = "Failure occurred when processing message received from queue. Reason: [{0}]";
		public const string Service_FailureWhenPublishingEvent = "Failure occurred when publishing event in one of the main publishers. Event of type[{0}] .The service will stop processing messages.";
		
		public const string Service_FailedRetrievingProperty = "Failed retrieving 'EventReceiver.TimeBeforeRecovery' property.";
				
		// Validation Messages
		public const string Validation_NoQueues = "Queues [{0}] specified in property key [{1}] is invalid. Value must contain 1 or more queue identifiers.";
		public const string Validation_QueueMissing = "Queue [{0}] specified in property key [{1}] does not exist.";
		public const string Validation_QueueNotReadable = "Queue [{0}] specified in property key [{1}] cannot be read.";
		public const string Validation_TestQueueNotReadable = "Queue [{0}] specified in property key [{1}] cannot be read when testing it. Reason: [{2}]";

		// Recovery message
		public const string Service_RecoveryMessage = "The service will try to recover in {0} ms.";
	}
}

