// ************************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/11/2003 
// DESCRIPTION			: Container for messages
// used by classes in the ReportDataImporter project.
// ************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportDataImporter/Messages.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:00   mturner
//Initial revision.
//
//   Rev 1.11   Nov 26 2003 11:03:18   geaton
//Removed messages that have been migrated to ImporterFactory component.
//
//   Rev 1.10   Nov 25 2003 19:00:26   geaton
//Added new messages following code refactoring.
//
//   Rev 1.9   Nov 24 2003 13:22:16   geaton
//Added warning message if no dates to process.
//
//   Rev 1.8   Nov 23 2003 19:58:48   geaton
//Added new message.
//
//   Rev 1.7   Nov 23 2003 14:41:58   geaton
//Added timeout support.
//
//   Rev 1.6   Nov 21 2003 11:29:42   geaton
//Added message for completion.
//
//   Rev 1.5   Nov 20 2003 22:43:54   geaton
//Added new messages.
//
//   Rev 1.4   Nov 19 2003 09:00:32   geaton
//Corrected typo.
//
//   Rev 1.3   Nov 19 2003 08:52:34   geaton
//Corrected usage message.
//
//   Rev 1.2   Nov 19 2003 08:32:32   geaton
//Added new message.
//
//   Rev 1.1   Nov 18 2003 21:44:50   geaton
//Added informational.
//
//   Rev 1.0   Nov 18 2003 21:27:12   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.ReportDataImporter
{
	public class Messages
	{
		// Initialisation Messages
		public const string Init_DotNETTraceListenerFailed = "Failed to initialise a default .NET trace listener. Reason:[{0}].";
		public const string Init_InitialisationStarted = "Initialisation of Report Data Importer started.";
		public const string Init_Completed = "Initialisation of Report Data Importer completed successfully.";
		public const string Init_TDServiceAddFailed = "Failed to add a TD service to the cache: [{0}].";
		public const string Init_TDTraceListenerFailed = "Failed to initialise the TD Trace Listener class: {0}";
        public const string Init_Usage = "Usage: td.reportdataprovider.reportdataimporter.exe [days_offset|/help|/test] " + 
            "\r\n" +
            "\r\n   days_offset: Number of days before current date that should be " + 
            "\r\n                included in the import. E.g. If 1 then include data " +
            "\r\n                from yesterday and earlier (which has not already been " +
            "\r\n                processed in a previous Import request)";
		public const string Init_UnknownPropertyKey = "Attempt to validate unknown property key: [{0}].";
		public const string Init_BadProperties = "One or more errors found in Report Data Importer properties: [{0}]";
		
		// ReportDataImporter Messages
        public const string ReportDataImporter_DaysOffset = "Report Data Importer days offset value set to [{0}]";
        public const string ReportDataImporter_Failed = "Report Data Importer failed. Reason:[{0}] Id:[{1}]";
		public const string ReportDataImporter_TestSucceeded = "Report Data Importer was run in test mode and succeeded.";
		public const string ReportDataImporter_InvalidArg = "Invalid argument/s passed to Report Data Importer.";
		public const string ReportDataImporter_Started = "Report Data Importer initialised successfully and is importing data to the report database using a days offset value of [{0}].";
		public const string ReportDataImporter_Completed = "Report Data Importer has completed successfully using a days offset value of [{0}].";

		// Controller Messages
        public const string Controller_DBTransferLink = "Initialising the link between the staging database and the reporting database.";
        public const string Controller_DBTransferLinkFail = "Failure when initialising the link between the staging database and the reporting database. Reason:[{0}]";
        public const string Controller_DBRemoveLink = "Removing link between staging database and the report database.";
        public const string Controller_DBRemoveLinkFailed = "Failure when removing link between staging database and the report database. Reason:[{0}]";
		
		// Validate Messages
		public const string Validation_BadWebRequestWindow = "Web Request Window [{0}] specified in property key [{1}] is invalid. The value must be a whole number.";
		public const string Validation_BadTimeout = "Timeout duration [{0}] specified in property key [{1}] is invalid. The value must be a whole number. A value of zero indicates no limit and should be avoided.";
		public const string Validation_UnableToConnectToReportingDB = "Unable to connect to reporting database [{0}] specified in property key [{1}]. Reason: [{2}].";
		
	}
}


