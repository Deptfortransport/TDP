// *************************************************
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 5/11/2003 
// DESCRIPTION			: Container for messages
// used by classes in Transaction Injector component.
// ************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/Messages.cs-arc  $
//
//   Rev 1.2   Mar 16 2009 12:24:04   build
//Automatically merged from branch for stream5215
//
//   Rev 1.1.1.2   Jan 21 2009 10:11:28   mturner
//Modified messages so that alerts are now Amber or Red rather than just all being the same.
//
//   Rev 1.1.1.1   Jan 14 2009 18:02:32   mturner
//Added new travel news messages
//
//   Rev 1.1.1.0   Jan 13 2009 14:42:52   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.1   Oct 13 2008 16:46:34   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Aug 04 2008 16:32:36   mturner
//Added new message for CP injected transaction failure
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:39:58   mturner
//Initial revision.
//
//   Rev 1.19   Apr 09 2005 15:17:06   schand
//Added messages for RTTI exceptions
//
//   Rev 1.18   Nov 17 2004 11:18:18   passuied
//Addition for TravelinecheckerTransaction
//
//   Rev 1.17   Oct 20 2004 17:38:02   rhopkins
//Additional message added for setting injection frequency on individual TI Services
//
//   Rev 1.16   Jun 22 2004 15:37:50   passuied
//Enhancements for TI
//
//   Rev 1.15   Jun 21 2004 15:25:14   passuied
//Changes for del6-del5.4.1
//
//   Rev 1.14   Jun 10 2004 17:10:42   passuied
//Added parameters for RequestJourneySleep
//
//   Rev 1.13   May 14 2004 17:00:38   GEaton
//IR882 - tidy message format as part of IR
//
//   Rev 1.12   May 14 2004 16:52:54   GEaton
//IR882
//
//   Rev 1.11   May 12 2004 17:55:38   GEaton
//IR866 - clean up after time outs
//
//   Rev 1.10   Apr 23 2004 17:22:04   geaton
//IR827 - Allow timeout specification for transactions.
//
//   Rev 1.9   Feb 16 2004 17:32:30   geaton
//Incident 643.
//
//   Rev 1.8   Feb 12 2004 09:31:40   geaton
//Incident 642 - clarify message.
//
//   Rev 1.7   Jan 09 2004 12:41:12   PNorell
//Updated transactions.
//
//   Rev 1.6   Dec 02 2003 20:08:46   geaton
//Added messages to support informationals.
//
//   Rev 1.5   Nov 13 2003 12:28:12   geaton
//Added messages to support new transaction types.
//
//   Rev 1.4   Nov 11 2003 19:16:16   geaton
//Added pricing support.
//
//   Rev 1.3   Nov 11 2003 15:49:42   geaton
//Removed redundant messages.
//
//   Rev 1.2   Nov 07 2003 15:45:34   geaton
//Added message
//
//   Rev 1.1   Nov 06 2003 18:10:02   geaton
//Removed redundant message.
//
//   Rev 1.0   Nov 06 2003 17:19:08   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	public class Messages
	{
		// Initialisation Messages
		public const string Init_TDTraceListenerFailed = "Failed to initialise the TD Trace Listener class. Exception ID: [{0}]. Reason: [{1}].";
		public const string Init_TDPropertiesServiceFailed = "Failed to initialise the TD Properties Service: [{0}]";
		public const string Init_UnknownPropertyKey = "Attempt to validate unknown property key: [{0}].";
		public const string Init_InvalidPropertyKeys = "Missing or invalid Transaction Injector property keys found on initialisation: [{0}].";
		public const string Init_Completed = "Initialisation of Transaction Injector completed successfully.";
		public const string Init_Failed = "Infrastructure\n{0}"; // OperationalEvent category prefixed for TNG to parse Event Log Description field.
		public const string Init_CustomPublisherCreateFailed = "Failed to create the Transaction Injector Custom Publisher. Reason: [{0}]";

		// Service Messages
		public const string Service_Starting = "Starting Transaction Injector Service.";
		public const string Service_Stopping = "Stopping Transaction Injector Service.";
		public const string Service_StartedTestMode = "Transaction Injector Service started successfully in test mode. No further processing is in progress.";
		public const string Service_BadParam = @"Unknown parameter passed when starting service. Valid parameters are: /test or /generatetemplate";
		public const string Service_StartedGenerateMode = "Transaction Injector Service started successfully in generate templates mode. Following template generation no further processing will be performed.";
		public const string Service_UnknownTransactionClass = "Unknown transaction class found when creating transaction class instances: [{0}]";
		public const string Service_FailedCreatingJourneyRequest = "Failed to create a journey request transaction for file [{0}] with error [{1}].";
		public const string Service_FailedCreatingTravelineChecker = "Failed to create a traveline checker transaction for file [{0}] with error [{1}].";
		public const string Service_FailedCreatingTransactionInstance = "Failed to create a transaction injector to inject transactions: [{0}].";
		public const string Service_JourneyRequestException = "Exception when executing journey request transaction [{0}]: Exception: [{1}].";
        public const string Service_CycleRequestException = "Exception when executing cycle journey request transaction [{0}]: Exception: [{1}].";
		public const string Service_TravelineCheckerException = "Exception when executing traveline checker transaction [{0}]: Exception: [{1}].";
		public const string Service_JourneyRequestGazException = "Exception when executing journey request using gazetteer transaction [{0}]: Exception: [{1}].";
        public const string Service_TransactionFailed = "{0} RED ALERT - [{1}] did not produce the expected results or failed with an exception. Results data: [{2}]";
		public const string Service_TransactionSucceeded = "Reference Transaction [{0}] succeeded. Results data: [{1}]";
		public const string Service_FailedCalculatingJourneyTime = "Error when calculating actual journey date and time: [{0}].";
		public const string Service_JourneyDateTimeInvalid = "Both the absolute date-time and the time spans have not been given a value. The current date-time is being used until this is resolved.";
		public const string Service_TransactionExceededAmberThreshold = "{0} AMBER ALERT - [{1}] exceeded threshold of [{2}] milliseconds by [{3}] milliseconds.";
        public const string Service_TransactionExceededRedThreshold = "{0} RED ALERT - [{1}] exceeded threshold of [{2}] milliseconds by [{3}] milliseconds.";
		public const string Service_XMLFormatIncorrect = "XML error: [{0}]";

        public const string Service_EESRequestException = "Exception when executing EES Request info transaction: [{0}].";
		public const string Service_FailedCreatingEESRequest = "Failed to create a EES Request transaction for file [{0}] with error [{1}].";

		public const string Service_MapException = "Exception when executing map transaction: [{0}].";
		public const string Service_FailedCreatingMap = "Failed to create a map transaction for file [{0}] with error [{1}].";

		public const string Service_GazetteerException = "Exception when executing gazetteer transaction: [{0}].";
		public const string Service_FailedCreatingGazetteer = "Failed to create a gazetteer transaction for file [{0}] with error [{1}].";

        public const string Service_TravelNewsException = "Exception when executing travel news transaction: [{0}].";
        public const string Service_FailedCreatingTravelNews = "Failed to create a travel news transaction for file [{0}] with error [{1}].";


		// Validation Messages
		public const string Validation_InvalidTransactions = "Transactions [{0}] specified in property key [{1}] is invalid. Value must contain 1 or more transaction identifiers.";
		public const string Validation_InvalidTemplateDir = "Directory [{0}] specified in property key [{1}] is invalid. Directory must exist.";
		public const string Validation_WebServiceUnavailable = "Configured web service is unavailable: [{0}]";

		// Template Messages
		public const string Template_GenerationFailed = "Template generation failed: [{0}]";

		// Injector Messages
		public const string Injector_InjectedTransaction = "Injected Reference Transaction of category [{0}]";
		public const string Injector_InitialisedTransaction = "Initialised Reference Transaction of category [{0}]. Repeat frequency configured to [{1}] seconds. Timeout configured to [{2}] seconds.";
		public const string Injector_TypeGapConfigured = "Transaction Injector has been configured to stagger injection of transaction 'types' using a gap of [{0}] seconds.";
		public const string Injector_InvalidTypeGapConfigured = "Transaction Injector was configured with an invalid transaction 'type' injection gap property value of [{0}]. Value must be numeric and greater than zero. A default of zero seconds will be used instead.";
		public const string Injector_InvalidServiceFrequencyConfigured = "Transaction Injector was configured with an invalid transaction service frequency property value of [{0}] for transaction service [{1}]. Value must be numeric and greater than zero. The global frequency will be used instead.";
		public const string Injector_InvalidTypeFrequencyConfigured = "Transaction Injector was configured with an invalid transaction type frequency property value of [{0}] for transaction type [{1}]. Value must be numeric and greater than zero. The global frequency will be used instead.";
		public const string Injector_FailedInsertingTypeGap = "Transaction Injector failed to insert a gap between injection of transaction types. Reason: [{0}] Injection will continue without gaps.";
		public const string Injector_FailedCreatingTimer = "Failed to create a timer for reference transaction of category [{0}]. Reason: [{1}]";
		public const string Injector_TransactionGapConfigured = "Transaction Injector has been configured to stagger injection of transactions using a gap of [{0}] seconds.";
		public const string Injector_InvalidTransactionGapConfigured = "Transaction Injector was configured with an invalid transaction injection gap property value of [{0}]. Value must be numeric and greater than zero. A default of zero seconds will be used instead.";
		public const string Injector_FailedInsertingTransactionGap = "Transaction Injector failed to insert a gap between injection of transactions. Reason: [{0}] Injection will continue without gaps.";
		public const string Injector_InvalidTimeoutConfigured = "Transaction Injector was configured with an invalid timeout property value of [{0}]. Value must be numeric and greater than zero. Default value of [{1}] seconds will be used instead.";
		public const string Injector_InvalidTypeTimeoutConfigured = "Transaction Injector was configured with an invalid transaction type timeout property value of [{0}] for transaction type [{1}]. Value must be numeric and greater than zero. The global timeout value will be used instead.";
		public const string Injector_TransactionTimedOut = "Transaction request has exceeded the configured timeout duration. Exception: [{0}]";
		public const string Injector_AbortFailure = "Failure occurred when aborting a transaction of category [{0}] after it had timed out. Reason: [{1}]";
		public const string Injector_NoResults = "Results are never returned to the injector for transactions of this category.";
	}
}
