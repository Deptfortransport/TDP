// ************************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/11/2003 
// DESCRIPTION			: Container for messages
// used by classes in the ReportDataImporter project.
// ************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ImporterFactory/Messages.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:48   mturner
//Initial revision.
//
//   Rev 1.0   Nov 26 2003 11:00:36   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.ImporterFactory
{
	public class Messages
	{
		
		public const string Importer_FailedToLogRowCount = "Failed to log number of rows imported from staging table/s. This was being performed for a verbose informational only. Reason:[{0}]";
		public const string Importer_RowCount = "Imported [{0}] rows from table(s) [{1}] in [{2}] seconds using stored procedure [{3}] for date [{4}].";
		public const string Importer_RollbackFailed = "Failure when attempting to roll back data after running stored procedure [{0}] for date [{1}] - report audit data may not reflect data that may have been imported. Reason:[{2}]";
		public const string Importer_RollbackPossiblyFailed = "Failure when attempting to roll back data after running stored procedure [{0}] for date [{1}]. It is possible that the rollback failed because no rows had previously been added in the transaction - in this case exception message will be 'The ROLLBACK TRANSACTION request has no corresponding BEGIN TRANSACTION.' Reason:[{2}]";
		public const string Importer_ImportFailed = "Import process failed when importing using stored procedure [{0}] for date [{1}]. Reason:[{2}]";
		public const string Importer_FailedToGetLatestImported = "Failed to determine the Latest Imported Date prior to importing using stored procedure [{0}]. Reason:[{1}]";
		public const string Importer_StoredProcedureFailed = "Failure when calling Stored Procedure [{0}]. Reason:[{1}]";
		public const string Importer_TimeoutInfinite = "Import timeout duration has been set to zero. When performing imports, SQL commands will wait indefinitely.";
		public const string Importer_NoDatesToProcess = "According to data in the ReportStagingDataAudit table, there are no dates for which an import should be performed using stored procedure [{0}].";
		public const string Importer_StagingDBConnectionFailure = "Failure when making connection to Staging Database. Reason:[{0}]";
		public const string Importer_RequestAuditFailed = "Failure when performing audit of import request. The actual import request was successfully performed using the stored procedure [{0}]. Reason:[{1}]";
		public const string Importer_ImportDisabled = "Import request for 'to date' [{0}] was revoked for importer with stored procedure [{1}].";
		public const string Importer_ArchiveDisabled = "Archive request was revoked for importer with stored procedure [{0}].";
		public const string Importer_RollbackFailedOnArchive = "Failure when attempting to roll back data after error when archiving staging tables used by the stored procedure[{0}]. Reason:[{1}]";
		public const string Importer_RollbackPossiblyFailedOnArchive = "Failure when attempting to roll back data after error when archiving staging tables used by the stored procedure[{0}]. It is possible that the rollback failed because no rows had previously been added in the transaction - in this case exception message will be 'The ROLLBACK TRANSACTION request has no corresponding BEGIN TRANSACTION.' Reason:[{1}]";
		public const string Importer_ArchiveFailed = "Archive process failed when archiving staging table(s) [{0}]. Reason:[{1}]";
		public const string Importer_ArchivedTables = "Archive process successfully archived data from table(s) [{0}] used by stored procedure [{1}].";

		public const string ImporterFactory_InvalidGroup = "An invalid group of Importers has been created by factory. Change configuration of Importers.";

	}
}


