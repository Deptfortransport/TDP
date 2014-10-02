// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 14/11/2003 
// DESCRIPTION			: Container for messages
// used by classes in the WebLogReader project.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/WebLogReader/Messages.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:54   mturner
//Initial revision.
//
//   Rev 1.9   Nov 11 2004 17:48:18   passuied
//Part of changes to enable WebLogReaders to read from multiple folders.
//
//   Rev 1.8   Jun 23 2004 10:26:34   jgeorge
//IR1043
//
//   Rev 1.7   Apr 19 2004 20:35:10   geaton
//IR785.
//
//   Rev 1.6   Apr 16 2004 13:38:56   geaton
//IR785 - changes to cope with hourly rotated web logs.
//
//   Rev 1.5   Dec 16 2003 12:31:38   geaton
//Added additional error information when reader fails to read web log entry.
//
//   Rev 1.4   Dec 15 2003 17:30:24   geaton
//Added support for filtering out log entries based on client IP address/es.
//
//   Rev 1.3   Nov 28 2003 10:39:48   geaton
//Added additional informational and warning messages.
//
//   Rev 1.2   Nov 21 2003 12:32:24   geaton
//Added success message.
//
//   Rev 1.1   Nov 17 2003 20:15:30   geaton
//Refactored.
//
//   Rev 1.0   Nov 14 2003 17:36:42   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.WebLogReader
{
	public class Messages
	{
		// Initialisation Messages
		public const string Init_DotNETTraceListenerFailed = "Failed to initialise a default .NET trace listener. Reason:[{0}].";
		public const string Init_InitialisationStarted = "Initialisation of Web Log Reader started.";
		public const string Init_Completed = "Initialisation of Web Log Reader completed successfully.";
		public const string Init_TDServiceAddFailed = "Failed to add a TD service to the cache: [{0}].";
		public const string Init_TDTraceListenerFailed = "Failed to initialise the TD Trace Listener class: {0}";
		public const string Init_Usage = "Usage: WebLogReader [/help|/test]";
		public const string Init_UnknownPropertyKey = "Attempt to validate unknown property key: [{0}].";
		public const string Init_ReaderProperties = "One or more errors found in Web Log Reader properties: [{0}]";

		// Reader Messages
		public const string Reader_Failed = "Web Log Reader failed. Reason:[{0}] Id:[{1}]";
		public const string Reader_TestSucceeded = "Web Log Reader was run in test mode and succeeded.";
		public const string Reader_InvalidArg = "Invalid argument/s passed to web log reader.";
		public const string Reader_Started = "Web Log Reader initialised successfully and is processing web log/s.";
		public const string Reader_Completed = "Web Log Reader completed successfully.";

		// Controller Messages
		public const string Controller_NoWebLogs = "Web Log Reader determined that there were no web log files to process.";
		public const string Controller_FailedProcessingWebLog = "Failed to process web log [{0}]. Reason: [{1}]. Prior to this failure, [{2}] web logs were processed successfully.";
		public const string Controller_FailedArchivingFile = "Failed to archive a processed web log file. Reason: [{0}]";
		public const string Controller_NumLogs = "Web Log Reader determined that there is/are [{0}] web log/s ready to process.";
		public const string Controller_NumLogsProcessed = "Web Log Reader successfully processed [{0}] web log/s.";
		public const string Controller_NumWorkloadEvents = "Web Log Reader logged [{0}] workload events for web log file [{1}].";
		public const string Controller_ZeroWorkloadEvents = "Web Log Reader logged zero workload events for web log file [{0}].";

		// Validate Messages

		public const string Validation_MissingWebLogsFolders = "Web log folders property specified in key [{0} does not exist or is empty.";
		public const string Validation_BadArchiveDir = "Web Log archive directory [{0}] specified in key [{1}] does not exist.";
		public const string Validation_BadLogDir = "Web Log log directory [{0}] specified in key [{1}] does not exist.";
		public const string Validation_NonPageMinimumBytesInvalid = "Non page minimum bytes value [{0}] specified in key [{1}] is invalid. Value must be zero or greater.";
		public const string Validation_TimeZoneInvalid = "Timezone setting of machine on which web logs reside is invalid. Time zone must be set to GMT.";
		public const string Validation_IPAddressInvalid = "Client IP Address Exclude [{0}] specified in key [{1}] is not in the correct format.";

		// W3C Reader messages
		public const string W3CReader_MissingFields = "Mandatory field [{0}] missing from web log file. File must contain the following fields in each entry: [{1}]";
		public const string W3CReader_NoFieldTokens = "No field name tokens have been included in web log file. These are used to determine which data maps to which fields.";
		public const string W3CReader_FailureStoringData = "Web Log Reader failure when storing workload event data for a web log file. Reason:[{0}]";
		public const string W3CReader_FailureReadingWebLogFile = "Web Log Reader failure when reading a web log file entry [{0}]. Reason:[{1}]";
		public const string W3CReader_RolloverDaily = "Web Log Reader determined active web logs using Daily rollover.";
		public const string W3CReader_RolloverHourly = "Web Log Reader determined active web logs using Hourly rollover.";
		public const string W3CReader_LocalTime = "Web Log Reader determined active web logs using local machine time.";
		public const string W3CReader_UtcTime = "Web Log Reader determined active web logs using GMT.";
		public const string W3CReader_FailedAllocatingMemoryForData = "Web Log Reader failed to allocate memory to store the web log data. Reason:[{1}]";
		public const string W3CReader_InvalidUseLocalTimeValue = "The property value UseLocalTime ({0}) could not be converted to a boolean value. The WebLogReader will default to using GMT.";
	}
}

