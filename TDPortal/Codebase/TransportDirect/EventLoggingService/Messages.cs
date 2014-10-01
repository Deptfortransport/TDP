// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Contains messages that are
// used in the Event Logging Service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/Messages.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:06   mturner
//Initial revision.
//
//   Rev 1.26   Feb 23 2006 19:15:38   build
//Automatically merged from branch for stream3129
//
//   Rev 1.25.1.0   Nov 21 2005 13:19:20   rgreenwood
//TD016 Enhanced Exposed Web Services Framework: Added log messages for Start and Finish events
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.25   Feb 07 2005 14:37:52   passuied
//added code + storedproc + tables for ExposedServices Event
//
//   Rev 1.24   Feb 02 2005 18:50:58   passuied
//changes for StopEventRequestEvent
//
//   Rev 1.23   Jan 24 2005 14:02:00   jgeorge
//Del 7 changes to CJP custom events
//
//   Rev 1.22   Jun 30 2004 17:02:54   jgeorge
//Changes for "force logging" functionality
//
//   Rev 1.21   May 26 2004 17:08:02   COwczarek
//Change CustomEmailPublisherStreamSaveFailed to CustomEmailPublisherSaveFailed and modify text
//Resolution for 726: Server error message when sending email to friend (Maps) (DEL 6.0)
//
//   Rev 1.20   Nov 21 2003 09:44:24   geaton
//Added AID and GID to exception message.
//
//   Rev 1.19   Nov 14 2003 11:09:10   geaton
//Added additional message info when configured publisher fails.
//
//   Rev 1.18   Nov 13 2003 18:18:14   geaton
//Updated event log constructor exception message.
//
//   Rev 1.17   Oct 21 2003 11:36:24   geaton
//Updated Event Log publisher messages following Event Log Publisher changes.
//
//   Rev 1.16   Oct 21 2003 08:53:54   geaton
//Added message for  Event Log Source property.
//
//   Rev 1.15   Oct 09 2003 13:11:36   geaton
//Support for error handling should an unknown event class be logged.
//
//   Rev 1.14   Sep 24 2003 18:06:12   geaton
//Provided additional help within message.
//
//   Rev 1.13   Sep 22 2003 17:08:00   geaton
//Provide more meaningful message should configured and default publishers fail.
//
//   Rev 1.12   Sep 18 2003 15:52:02   geaton
//Changed ambiguous message.
//
//   Rev 1.11   Sep 18 2003 12:12:56   geaton
//Message to support TDTraceListener property change notification.
//
//   Rev 1.10   Sep 18 2003 11:31:06   geaton
//Added messages to support CustomEmailPublisher and CustomEmailEvent classes.
//
//   Rev 1.9   Sep 12 2003 11:36:22   geaton
//Messages for custom events added. Maintained in this class so format of the custom events logged by Atkins can be changed without need to change the dll defining the events.
//
//   Rev 1.8   Sep 04 2003 16:21:36   geaton
//Added new message.
//
//   Rev 1.7   Aug 26 2003 15:30:58   geaton
//Added validation messages.
//
//   Rev 1.6   Aug 22 2003 15:00:24   geaton
//messages for emailattachment publisher
//
//   Rev 1.5   Aug 22 2003 11:05:34   geaton
//Added message for default formatter.
//
//   Rev 1.4   Aug 22 2003 10:18:50   geaton
//New messages to support other code changes
//
//   Rev 1.3   Jul 30 2003 11:49:34   geaton
//Added PVCS logging.

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Summary description for Messages.
	/// </summary>
	public class Messages
	{
		// **** Property validation messages ***
		public const string CustomPublisherNameMismatch = "Custom Publisher Class [{0}] has an invalid class name or invalid identifier. According to the given class identifier the class name should be specified in property key [{1}].";
		public const string CustomPublisherInvalidId = "Custom Publisher Class [{0}] does not have a valid identifier assigned to it. Current identifier value is [{1}].";
		public const string UndefinedPublisher = "A Publisher specified for an event [{0}] has an id [{1}] that does not match any of the publishers available."; 
		public const string FilePublisherDirectoryBad = "Directory [{0}] of file publisher specified in property [{1}] does not exist.";
		public const string FilePublisherRotationBad = "Rotation value [{0}] of file publisher specified in property [{1}] must be an integer and greater than zero.";
		public const string QueuePublisherMSMQBad = "MSMQ [{0}] of queue publisher specified in property [{1}] cannot be written to. MSMQ may not exist or path may be invalid.";
		public const string QueuePublisherDeliveryBad = "Delivery method [{0}] of queue publisher specified in property [{1}] is invalid. Must take value Express|Recoverable.";
		public const string EmailPublisherAddressBad = "Email address [{0}] of email publisher specified in property [{1}] does not follow RFC 822 standard.";
		public const string LoggingPropertyValidatorKeyBad = "LoggingPropertyValidators unable to validate key [{0}]";
		public const string EventLogPublisherEventLogBad = "Event Log [{0}] on machine [{1)] belonging to event log publisher does not exist. Values were specified in properties [{2}] and [{3}].";
		public const string EventLogPublisherEventLogNotFound = "Event Log belonging to event log publisher cannot be found. Reason: [{0}]. Values were specified in properties [{1}] and [{2}]."; 		
		public const string CustomPublisherArrayBad = "Custom Publisher array cannot be null. Number of elements must be zero or more.";
		public const string EventHasZeroPublishers = "One or more publishers have not been defined for the event specified in key [{0}]. It is mandatory to specify one or more publishers for this event.";		
		public const string IncorrectClassType = "Class [{0}] specified in property key [{1}] does not derive from class [{2}].";
		public const string UnknownCustomEventClassName = "The name of the Custom Event class [{0}] is unknown. Ensure: (1) the correct Custom Event class 'name' is passed to the CustomEventLevelSwitch. (2) The TDTraceListener has been added to the trace listener collection. (3) The Custom Event class has been defined in the logging service configuration properties.";
		public const string CustomEventSwitchNotInitialised = "The CustomEventSwitch class instance has not been initialised. Ensure that the TDTraceListener has been created first.";
		
		public const string OperationalEventLevelInvalid = "An Operational Event has been created using an invalid TDTraceLevel.";

		// *** Standard Publisher messages ***
		public const string ConsolePublisherWriteEvent =
			"Exception occured when Console Publisher attempted to write log event to the console.";
		public const string EmailPublisherWriteEvent =
			"Exception occurred while attempting to send the log event using Email Publisher.  The value(s) that may have caused the exception are:{0}";
		public const string EventLogPublisherConstructor =
			"Exception [{0}] occured when constructing an Event Log Publisher:  Event Log Name:[{1}] Event Log Source:[{2}] Event Log Machine:[{3}]";
		public const string EventLogPublisherWriteEvent =
			"Exception occured when publishing the event using Event Log Publisher:- Event Log Name:[{0}] Event Log Source:[{1}] Event Log Machine:[{2}]";
		public const string FilePublisherConstructor =
			"Exception occured while attempting to set-up the File Publisher.  The value(s) that may have caused the exception are:{0}";	
		public const string FilePublisherWriteEvent =
			"Exception occured while attempting to write the log event [{0}] to the file [{1}].";
		public const string QueuePublisherConstructor =
			"Exception occured while attempting to set-up the Message Queue.  The value(s) that may have caused the exception are:{0}";
		public const string QueuePublisherWriteEvent =
			"Exception occured while attempting to send the logEvent to the Message Queue. The value(s) that may have caused the exception are:{0}";

		// Custom Email Publisher messages
		public const string CustomEmailPublisherSaveFailed = "CustomEmailPublisher failed to save attachment as a file to the filepath [{0}]";
		public const string CustomEmailPublisherFileDeleteFailed = "CustomEmailPublisher failed to delete file attachment from filepath [{0}]. Note that this error may have been triggered by the CustomEmailPublisher failing to publish the event - ie the directory being deleted may be locked.";
		public const string CustomEmailPublisherConstructor = "One or more errors occurred during the construction of CustomEmailPublisher.";
		public const string CustomEmailPublisherPublisherFromAddressBad = "Sendee email address [{0}] does not follow RFC 822 standard.";
		public const string CustomEmailPublisherPublisherSmtpServerBad = "Email SMTP Server [{0}] is invalid.";
		public const string CustomEmailPublisherWorkingDirMissing = "Working directory [{0}] is missing or inaccessible.";
		public const string CustomEmailPublisherUnsupportedEventType = "Unsupported LogEvent type was passed for publishing to CustomEmailPublisher";
		public const string CustomEmailPublisherWriteEventFailed =
			"Exception occurred while attempting to send the log event using CustomEmailPublisher (Identifier:[{0}]).  The CustomEmailEvent being published had the following values:To:[{1}], From:[{2}] Subject:[{3}] Priority:[{4}] SmtpServer:[{5}] MessageBody:[{6}]";
		
		
		// TDTraceListener messages
		public const string TDTraceListenerUnsupportedPrototype = "Unsupported TDTraceListener write prototype called with parameter(s): [{0}]. Change this call to use Write(object) prototype!";
		public const string TDTraceSwitchNotInitialised = "The TDTraceSwitch class instance has not been initialised. Ensure that the TDTraceListener has been created first.";
		public const string TDTraceListenerWriteUnimplemented = "A prototype of TDTraceListener.Write has been used but is not supported - use void Write(object) instead.";
		public const string TDTraceListenerConstructor = "One or more errors occurred during the construction of TDTraceListener for component with property Application Id [{0}] and Group Id [{1}].";
		public const string TDTraceListenerDefaultPublisherFailure = "Failed to write an event using the default publisher. The default publisher was used because another publisher failed to write the event - check inner exception for details.";
		public const string ConfiguredPublisherFailed = "Failed to write a custom event of type [{0}] using it's configured publisher [{1}]. Reason: [{2}]";
		public const string DefaultFormatterOutput = "TD-EVENT\t{0}\tEventClassName:[{1}]\tWARNING:This event data was formatted using DefaultFormatter and may have resulted in data loss."; 
		public const string TDTraceListenerSubscriberNotificationFailed = "TDTraceListener failed to notify subscriber/s with property values.";
		public const string TDTraceListenerPropertyChange = "TDTraceListener received notification from property provider that properties have changed. The properties that changed may or may not be relevent to the Event Logging Service.";
		public const string TDTraceListenerDefaultPublisherUsed = "A custom event of type [{0}] was published using the default publisher.";
		public const string TDTraceListenerUnknownClass = "An attempt to log an object that is not of type LogEvent occurred.";

		// Messages for custom events
		public const string InternalRequestEventInvalidFunctionType = "The function type [{0}] is invalid. Function types must be exactly two characters long.";

		// formatter class messages for custom events
		public const string JourneyWebRequestEventFileFormat = "JourneyWebRequestEvent\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}"; 
		public const string InternalRequestEventFileFormat = "InternalRequestEvent\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}"; 
		public const string LocationRequestEventFileFormat = "LocationRequestEvent\t{0}\t{1}\t{2}\t{3}\t{4}"; 
		public const string StopEventRequestEventFileFormat = "StopEventRequestEvent\t{0}\t{1}\t{2}\t{3}\t{4}";
		public const string ExposedServicesEventFileFormat = "ExposedServicesEvent\t{0}\t{1}\t{2}\t{3}\t{4}";

		public const string EnhancedExposedServicesStartEventFileFormat = "EnhancedExposedServicesStartEvent\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}";
		
		public const string EnhancedExposedServicesFinishEventFileFormat = "EnhancedExposedServicesFinishEvent\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}";

		static Messages()
		{}
	}
}
