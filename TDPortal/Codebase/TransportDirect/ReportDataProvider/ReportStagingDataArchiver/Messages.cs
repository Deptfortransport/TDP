// ************************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 19/11/2003 
// DESCRIPTION			: Container for messages
// used by classes in the ReportStagingDataArchiver.
// ************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportStagingDataArchiver/Messages.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:10   mturner
//Initial revision.
//
//   Rev 1.4   Nov 26 2003 11:05:52   geaton
//Removed redundant messages following use of ImporterFactory component.
//
//   Rev 1.3   Nov 22 2003 17:22:06   geaton
//Added test mode.
//
//   Rev 1.2   Nov 21 2003 12:15:58   geaton
//Added completed successfully message.
//
//   Rev 1.1   Nov 19 2003 19:48:56   geaton
//Added new messages.
//
//   Rev 1.0   Nov 19 2003 11:37:50   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.ReportStagingDataArchiver
{
	public class Messages
	{
		// Initialisation Messages
		public const string Init_DotNETTraceListenerFailed = "Failed to initialise a default .NET trace listener. Reason:[{0}].";
		public const string Init_InitialisationStarted = "Initialisation of Report Staging Data Archiver started.";
		public const string Init_Completed = "Initialisation of Report Staging Data Archiver completed successfully.";
		public const string Init_TDServiceAddFailed = "Failed to add a TD service to the cache: [{0}].";
		public const string Init_TDTraceListenerFailed = "Failed to initialise the TD Trace Listener class: {0}";
		public const string Init_UnknownPropertyKey = "Attempt to validate unknown property key: [{0}].";
		public const string Init_BadProperties = "One or more errors found in Report Staging Data Archiver properties: [{0}]";		
		public const string Init_Usage = "Usage: Td.ReportDataProvider.ReportStagingDataArchiver.exe [/help|/test]";
		
		// ReportStagingDataArchiver Messages
		public const string ReportStagingDataArchiver_Failed = "Report Staging Data Archiver failed. Reason:[{0}] Id:[{1}]";
		public const string ReportStagingDataArchiver_Started = "Report Staging Data Archiver initialised successfully and is archiving report staging data.";
		public const string ReportStagingDataArchiver_InvalidArg = "Invalid argument/s passed to Report Staging Data Archiver.";
		public const string ReportStagingDataArchiver_Completed = "Report Staging Data Archiver completed successfully.";
		public const string ReportStagingDataArchiver_TestSucceeded = "Report Staging Data Importer was run in test mode and succeeded.";

		// Controller Messages
		public const string Controller_FailedCreatingImporters = "Failed to create Importers using ImporterFactory. Reason:[{0}]";
		public const string Controller_LatestImportedMismatch = "The Latest Imported dates (held in the ReportStagingDataAudit table) are not the same for all import data. Since some staging tables are used by more than one import stored procedure, an archive cannot be performed until Latest Imported dates all match.";
		
	}
}

