// *********************************************** 
// NAME                 : Importer.cs 
// AUTHOR               : Andy Lole
// DATE CREATED         : 12/09/2003 
// DESCRIPTION  : A Report Data Importer.
// Used to perform imports and archives.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ImporterFactory/Importer.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:46   mturner
//Initial revision.
//
//   Rev 1.6   Apr 21 2006 14:48:34   rwilby
//Changed RecordImportVolumes method's SQL select statement to "SELECT COUNT(*) FROM " from "SELECT COUNT(ID) FROM ".
//Resolution for 3707: Regr: Problem running Report Data Importer
//
//   Rev 1.5   Dec 20 2004 17:49:16   jgeorge
//Updated to use ReportStagingDB instead of DefaultDB
//
//   Rev 1.4   Feb 11 2004 11:42:34   geaton
//Incident 640 - Importer Timeout Value does not take effect.
//
//   Rev 1.3   Dec 04 2003 15:21:02   geaton
//Corrected operational event logging level.
//
//   Rev 1.2   Dec 01 2003 19:30:40   geaton
//Pass latestImported date when archiving 'import enabled' staging tables.
//
//   Rev 1.1   Nov 26 2003 12:50:52   geaton
//Corrected verbose event report for the WorkloadEvent data imported.
//
//   Rev 1.0   Nov 26 2003 11:00:34   geaton
//Initial Revision

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.ImporterFactory
{
	/// <summary>
	/// Report Data Importer class - used to perform imports and archives of 
	/// report staging data.
	/// </summary>
	public class Importer : IImporter
	{
		
		/// <summary>
		/// Name of stored procedure that updates the audit event values in the 
		/// ReportStagingDataAudit table.
		/// </summary>
		private const string UpdateAuditEventStoredProcedureName = "UpdateAuditEvent";

		/// <summary>
		/// Name of stored procedure that gets the latest imported date
		/// from the ReportStagingDataAudit table.
		/// </summary>
		private const string GetLatestImportedStoredProcedureName = "GetLatestImported";

		/// <summary>
		/// Name of stored procedure that deletes staging data that
		/// has previously been imported.
		/// </summary>
		private const string DeleteImportedStagingDataStoredProcedureName = "DeleteImportedStagingData";

		/// <summary>
		/// Name of stored procedure that deletes staging data.
		/// </summary>
		private const string DeleteStagingDataStoredProcedureName = "DeleteStagingData";

		/// <summary>
		/// Gets one or more staging table names used as the source for performing imports.
		/// </summary>
		private string[] stagingTableName = null;
		public string[] StagingTableName
		{
			get { return stagingTableName; }
		}
		
		/// <summary>
		/// Gets the name of stored procedure used to import the data from the staging tables.
		/// This is also used as the RSDTName in the ReportStagingDataType table.
		/// </summary>
		private string storedProcedure = string.Empty;
		public string StoredProcedureName
		{
			get { return storedProcedure; }
		}

		/// <summary>
		/// Delegate used to add parameters to the stored procedure.
		/// </summary>
		private AddParameters addParameters = null;

		/// <summary>
		/// Timeout duration in seconds for performing SQL commands.
		/// </summary>
		private int timeout = 30;

		/// <summary>
		/// Time at which Importer service was requested.
		/// </summary>
		private DateTime requestDate;

		/// <summary>
		/// Duration of CJP web request window - required by some Importers when performing Imports ONLY.
		/// </summary>
		private int CJPWebRequestWindow;
		
		/// <summary>
		/// Gets the date of the latest imported staging data.
		/// </summary>
		private DateTime latestImported;
		public DateTime LatestImported
		{
			get { return latestImported; }
		}

		/// <summary>
		/// Gets and sets the Import Enabled flag.
		/// </summary>
		private bool importEnabled;
		public bool ImportEnabled
		{
			get {return importEnabled;}
			set {importEnabled = value;}
		}

		/// <summary>
		/// Gets and sets the Archive Enabled flag.
		/// </summary>
		private bool archiveEnabled;
		public bool ArchiveEnabled
		{
			get {return archiveEnabled;}
			set {archiveEnabled = value;}
		}

		/// <summary>
		/// Determines the latest imported date for this Importer.
		/// </summary>
		/// <returns>Latest Imported Date.</returns>
		/// <exception cref="TDException">
		/// Fails to determine latest imported date.
		/// </exception>
		private DateTime GetLatestImportedDate()
		{
			SqlHelper sqlHelper = new SqlHelper();
			
			// Initialise with day before current Date
			DateTime latestImportedDate = DateTime.Now.Subtract(new TimeSpan(1,0,0,0,0));

			try
			{
				sqlHelper.ConnOpen(SqlHelperDatabase.ReportStagingDB);
				Hashtable parameters = new Hashtable(1);
				parameters.Add("@DataName", this.storedProcedure);
				object result = sqlHelper.GetScalar(GetLatestImportedStoredProcedureName, parameters);
				latestImportedDate = (DateTime)result;
			}
			catch (Exception exception) // catch all since no documentation for sqlhelper
			{
				throw new TDException(String.Format(Messages.Importer_StoredProcedureFailed, GetLatestImportedStoredProcedureName, exception.Message), false, TDExceptionIdentifier.RDPDataImporterStoredProcedureFailed);
			}
			finally
			{
				if (sqlHelper.ConnIsOpen)
				{
					sqlHelper.ConnClose();
				}
			}

			return latestImportedDate;
		}

		/// <summary>
		/// Default Constructor.
		/// </summary>
		/// <param name="requestDate">
		/// Date and time at which import was requested to perform service.
		/// </param>
		/// <param name="storedProcedure">
		/// Name of stored procedure for importing data to the report database.
		/// </param>
		/// <param name="stagingTable">
		/// Names of one or more staging table names containing the staging data.
		/// </param>
		/// <param name="addParameters">
		/// Delegate to use to get parameters for import stored procedure.
		/// </param>
		/// <param name="timeout">
		/// Sql command timeout duration in seconds.
		/// </param>
		/// <param name="importEnabled">
		/// True if importing using this Importer is enabled.
		/// </param>
		/// <param name="archiveEnabled">
		/// True if archiving using this Importer is enabled.
		/// </param>
		/// <param name="CJPWebRequestWindow">
		/// Duration of CJP web request window - required by some Importers when performing Imports ONLY.
		/// </param>
		/// <exception cref="TDException">
		/// Thrown if constructor fails to determine the Latest Imported Date 
		/// of the staging data.
		/// </exception>
		public Importer(DateTime requestDate,
						string storedProcedure,
						string[] stagingTable,
						AddParameters addParameters,
						int timeout,
						bool importEnabled,
						bool archiveEnabled,
						int CJPWebRequestWindow)
		{
			this.requestDate = requestDate;
			this.stagingTableName = stagingTable;
			this.storedProcedure = storedProcedure;
			this.addParameters = addParameters;
			this.importEnabled = importEnabled;
			this.archiveEnabled = archiveEnabled;
			this.CJPWebRequestWindow = CJPWebRequestWindow;
			this.timeout = timeout;
			
			if (TDTraceSwitch.TraceVerbose)
                Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, String.Format("Import timeout is configured to [{0}] seconds for [{1}]", this.timeout.ToString(), storedProcedure)));

			if ((timeout == 0) && (TDTraceSwitch.TraceWarning))
				Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Warning, Messages.Importer_TimeoutInfinite));

			try
			{
				this.latestImported = GetLatestImportedDate();
			}
			catch (TDException tdException)
			{
				throw new TDException(String.Format(Messages.Importer_FailedToGetLatestImported, this.storedProcedure, tdException.Message), false, TDExceptionIdentifier.RDPDataImporterFailedToGetLatestImportedDate);
			}
		}

		/// <summary>
		/// Performs archive of the staging data used by the Importer to perform imports.
		/// Only staging data that has been imported will be archived.
		/// Also performs necessary auditing.
		/// </summary>
		/// <exception cref="TDException">
		/// Thrown if error occurs during operation.
		/// </exception>
		public void Archive()
		{
			if (!this.archiveEnabled)
				throw new TDException(String.Format(Messages.Importer_ArchiveDisabled, this.storedProcedure), false, TDExceptionIdentifier.RDPDataImporterArchiveDisabled);

			SqlConnection sqlConnection = null;

			// Create connection to staging database.
			try
			{
				sqlConnection = new SqlConnection(); 
				sqlConnection.ConnectionString = Properties.Current[SqlHelperDatabase.ReportStagingDB.ToString()];
				sqlConnection.Open();
			}
			catch (Exception exception)
			{
				throw new TDException(String.Format(Messages.Importer_StagingDBConnectionFailure, exception.Message), false, TDExceptionIdentifier.RDPDataImporterConnectionFailure); 
			}

			SqlTransaction sqlTransaction = null;

			try
			{
				sqlTransaction = sqlConnection.BeginTransaction();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				sqlCommand.Transaction = sqlTransaction;

				// Archive all staging tables.
				for (int i = 0; i < this.stagingTableName.Length; i++)	
					ArchiveTable(sqlCommand, this.stagingTableName[i]);

				// Commit changes to staging tables.
				sqlTransaction.Commit();

				// Report on tables archived.
				if (TDTraceSwitch.TraceVerbose)
				{
					StringBuilder tableNames = new StringBuilder(50);
					for (int table = 0; table < stagingTableName.Length; table++)
						tableNames.Append(stagingTableName[table] + " ");

					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, String.Format(Messages.Importer_ArchivedTables, tableNames.ToString(), this.storedProcedure)));
				}

				// Audit the archive request.
				AuditRequest(sqlConnection, AuditType.LastArchive);
				
			}
			catch (TDException tdException)
			{
				try
				{
					sqlTransaction.Rollback();
				}
				catch (InvalidOperationException invalidOperationException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.Importer_RollbackFailedOnArchive, this.storedProcedure, invalidOperationException.Message)));
				}
				catch (ArgumentException argumentException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.Importer_RollbackFailedOnArchive, this.storedProcedure, argumentException.Message)));
				}
				catch (Exception rollbackException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.Importer_RollbackPossiblyFailedOnArchive, this.storedProcedure, rollbackException.Message)));
				}

				StringBuilder tableNames = new StringBuilder(50);
				for (int table = 0; table < stagingTableName.Length; table++)
					tableNames.Append(stagingTableName[table] + " ");
					
				throw new TDException(String.Format(Messages.Importer_ArchiveFailed, tableNames.ToString(), tdException.Message), tdException, false, TDExceptionIdentifier.RDPDataImporterFailureDuringArchive);
			}
			finally
			{
				sqlConnection.Close();
			}

		}

		/// <summary>
		/// Archives (deletes) all data in the table <code>tableName</code>
		/// that has already been imported to the reporting database.
		/// </summary>
		/// <param name="sqlCommand">Command to use to perform archive.</param>
		/// <param name="tableName">Name of staging table to archive.</param>
		/// <exception cref="TDException">
		/// If archive fails on given table.
		/// </exception>
		private void ArchiveTable(SqlCommand sqlCommand, string tableName)
		{
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandTimeout = this.timeout;
			sqlCommand.Parameters.Clear();

			if (this.importEnabled)
			{
				// Archive must take account of latest imported date, if import is enabled.
				sqlCommand.CommandText = DeleteImportedStagingDataStoredProcedureName;
                (sqlCommand.Parameters.AddWithValue("@StagingTableName", tableName)).Direction = ParameterDirection.Input;
                (sqlCommand.Parameters.AddWithValue("@LatestImported", this.latestImported)).Direction = ParameterDirection.Input;
			}
			else
			{
				// Where import is disable, no need to take account of latest imported date.
				sqlCommand.CommandText = DeleteStagingDataStoredProcedureName;
                (sqlCommand.Parameters.AddWithValue("@StagingTableName", tableName)).Direction = ParameterDirection.Input;
			}
			
			// Perform archive on table.
			try
			{
				sqlCommand.ExecuteNonQuery();
			}
			catch (Exception exception)
			{
                throw new TDException(String.Format(Messages.Importer_StoredProcedureFailed, sqlCommand.CommandText, exception.Message), false, TDExceptionIdentifier.RDPDataImporterStoredProcedureFailed);
			}
					
		}

		/// <summary>
		/// Performs import of staging data to the reporting database.
		/// Also performs necessary auditing.
		/// </summary>
		/// <param name="toDate">
		/// Date up to which staging data should be imported.
		/// </param>
		/// <exception cref="TDException">
		/// Thrown if error occurs during operation or if import service is disabled.
		/// </exception>
		public void Import(DateTime toDate)
		{
			if (!this.importEnabled)
				throw new TDException(String.Format(Messages.Importer_ImportDisabled, toDate.ToShortDateString(), this.storedProcedure), false, TDExceptionIdentifier.RDPDataImporterImportDisabled);

			// Add 1 day to 'latest imported' get the start date of Import Period.
			DateTime fromDate = latestImported.AddDays(1);

			// Calculate import duration
			TimeSpan importDuration = (toDate - fromDate) + new TimeSpan(1,0,0,0,0);  // Add one day to make duration inclusive of to and from date.

			if (importDuration.TotalDays <= 0)
			{	
				// No days to import - give warning.
				if (TDTraceSwitch.TraceWarning)
					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Warning, String.Format(Messages.Importer_NoDatesToProcess, this.storedProcedure)));
			}
			else
			{
				SqlConnection sqlConnection = null;

				// Create connection to staging database.
				try
				{
					sqlConnection = new SqlConnection(); 
					sqlConnection.ConnectionString = Properties.Current[SqlHelperDatabase.ReportStagingDB.ToString()];
					sqlConnection.Open();
				}
				catch (Exception exception)
				{
					throw new TDException(String.Format(Messages.Importer_StagingDBConnectionFailure, exception.Message), false, TDExceptionIdentifier.RDPDataImporterConnectionFailure); 
				}
					
				try
				{
					try
					{
						// Import all staging data between the period defined by from and to dates.
						ImportPeriod(sqlConnection, fromDate, toDate);
					}
					catch (TDException tdException)
					{
						throw tdException;
					}
					
					try
					{
						// Audit the entire import request.
						AuditRequest(sqlConnection, AuditType.LastImport);	
					}
					catch (TDException tdException)
					{
						throw new TDException(String.Format(Messages.Importer_RequestAuditFailed, this.storedProcedure, tdException.Message), false, TDExceptionIdentifier.RDPDataImporterRequestAuditUpdateFailed); 
					}
				}
				catch (TDException tdException)
				{
					throw tdException;
				}
				finally
				{
					sqlConnection.Close();
				}
			}		
		}

		
		/// <summary>
		/// Imports staging data for the period defined by <code>fromDate</code> and <code>toDate</code>
		/// </summary>
		/// <param name="sqlConnection">Connection to staging database.</param>
		/// <param name="fromDate">Start date of import period.</param>
		/// <param name="toDate">End date of import period.</param>
		/// <exception cref="TDException">
		/// Thrown if import fails.
		/// </exception>
		private void ImportPeriod(SqlConnection sqlConnection,
								  DateTime fromDate,
								  DateTime toDate)
		{
			TimeSpan importDuration = toDate - fromDate + new TimeSpan(1,0,0,0,0); // Add one day to make duration inclusive of to and from date.
			DateTime fromDay = fromDate;
			DateTime currentDay = fromDay;
			SqlTransaction sqlTransaction = null;
			DateTime processDayStart;

			try
			{
				for (int i = 0; i < importDuration.TotalDays; i++)
				{
					processDayStart = DateTime.Now;
					
					currentDay = fromDay.AddDays(i);

					sqlTransaction = sqlConnection.BeginTransaction();
					SqlCommand sqlCommand = new SqlCommand();
					sqlCommand.Connection = sqlConnection;
					sqlCommand.Transaction = sqlTransaction;

					// Import data for the day.
					ImportDay(sqlCommand, currentDay);

					// Audit the day's imported data.
					AuditDay(sqlCommand, currentDay);

					// Commit transaction ONLY after successfully importing AND auditing.
					sqlTransaction.Commit();
					
					this.latestImported = currentDay;

					// Record start time for informational logging.
					if (TDTraceSwitch.TraceVerbose)
						RecordImportVolumes(currentDay, processDayStart);
				}

			}
			catch (Exception exception)
			{
				try
				{
					sqlTransaction.Rollback();
				}
				catch (InvalidOperationException invalidOperationException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.Importer_RollbackFailed, this.storedProcedure, currentDay.ToShortDateString(), invalidOperationException.Message)));
				}
				catch (ArgumentException argumentException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.Importer_RollbackFailed, this.storedProcedure, currentDay.ToShortDateString(), argumentException.Message)));
				}
				catch (Exception rollbackException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.Importer_RollbackPossiblyFailed, this.storedProcedure, currentDay.ToShortDateString(), rollbackException.Message)));
				}

				throw new TDException(String.Format(Messages.Importer_ImportFailed, this.storedProcedure, currentDay.ToShortDateString(), exception.Message), exception, false, TDExceptionIdentifier.RDPDataImporterFailureDuringImport );
			}

		}

		/// <summary>
		/// Records volume of staging data imported for performance monitoring.
		/// An error is logged if this method fails.
		/// </summary>
		/// <param name="importDay">Date of staging data imported.</param>
		/// <param name="startTime">Time at which import started.</param>
		private void RecordImportVolumes(DateTime importDay, DateTime startTime)
		{
			TimeSpan timeTaken = DateTime.Now - startTime;
			SqlHelper sqlHelper = new SqlHelper();

			try
			{
				// Convert import date into format required by SQL Server.
				string month = importDay.Month.ToString();
				string day   = importDay.Day.ToString();
			
				if ( month.Length == 1 )
				{
					month = "0" + month;
				}
				if ( day.Length == 1 )
				{
					day = "0" + day;
				}
			
				string importDateString = importDay.Year + "-" + month + "-" + day;

				sqlHelper.ConnOpen(SqlHelperDatabase.ReportStagingDB);

				StringBuilder stagingTables = new StringBuilder(50);
				StringBuilder rowCounts = new StringBuilder(50);
			
				for (int table = 0; table < stagingTableName.Length; table++)
				{			
					string command = String.Empty;

					// Construct select command.
					// NB: Use Requested column for WorkloadEvent table since the TimeLogged refers to the time that the Web Log Reader logged the event, as opposed to when the workload event occurred.
					if (stagingTableName[table].Equals("WorkloadEvent"))
						command = "SELECT COUNT(*) FROM " + stagingTableName[table] + " WHERE CONVERT(varchar(10), Requested, 121) = '" + importDateString + "\'";
					else
						command = "SELECT COUNT(*) FROM " + stagingTableName[table] + " WHERE CONVERT(varchar(10), TimeLogged, 121) = '" + importDateString + "\'";

					object result = sqlHelper.GetScalar(command);
					int rows = (int)result;

					if (result != null)
						rowCounts.Append(rows.ToString() + " ");

					stagingTables.Append(stagingTableName[table] + " ");
				}

				Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, String.Format(Messages.Importer_RowCount, rowCounts.ToString(), stagingTables.ToString(), timeTaken.TotalSeconds, this.storedProcedure, importDay.ToShortDateString())));

			}
			catch (Exception exception)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.Importer_FailedToLogRowCount, exception.Message)));
			}
			finally
			{
				sqlHelper.ConnClose();
			}

		}

		
		/// <summary>
		/// Imports staging data that was logged on day <code>importDay</code>
		/// </summary>
		/// <param name="sqlCommand">Command to use to perform import.</param>
		/// <param name="importDay">Date of staging data to import. </param>
		/// <exception cref="TDException">
		/// Thrown if import fails.
		/// </exception>
		private void ImportDay(SqlCommand sqlCommand, DateTime importDay)
		{
			// Convert import date into format required by SQL Server.
			string month = importDay.Month.ToString();
			string day   = importDay.Day.ToString();
			
			if ( month.Length == 1 )
			{
				month = "0" + month;
			}
			if ( day.Length == 1 )
			{
				day = "0" + day;
			}
			
			string importDateString = importDay.Year + "-" + month + "-" + day;

			sqlCommand.CommandText = this.storedProcedure;
			sqlCommand.CommandType = CommandType.StoredProcedure;
			sqlCommand.CommandTimeout = this.timeout;
			sqlCommand.Parameters.Clear();

			// Get parameters using delegate method.
			Hashtable parameters = this.addParameters(importDateString, this.CJPWebRequestWindow);

			// Add parameters to command.
			foreach (string keyName in parameters.Keys)
                sqlCommand.Parameters.AddWithValue(keyName, parameters[keyName]);

			// Perform import.
			try
			{	
				sqlCommand.ExecuteNonQuery();
			}
			catch (Exception exception)
			{
				throw new TDException(String.Format(Messages.Importer_StoredProcedureFailed, this.storedProcedure, exception.Message), false, TDExceptionIdentifier.RDPDataImporterStoredProcedureFailed);
			}

		}

		/// <summary>
		/// Audits the import performed for the day <code>auditDay</code> 
		/// </summary>
		/// <param name="sqlCommand">Command to use to perform import.</param>
		/// <param name="importDay">Date that audit should be performed for.</param>
		/// <exception cref="TDException">
		/// Thrown if audit fails.
		/// </exception>
		private void AuditDay(SqlCommand sqlCommand, DateTime auditDay)
		{
			
			try
			{
				sqlCommand.CommandText = UpdateAuditEventStoredProcedureName;
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.Clear();
				sqlCommand.CommandTimeout = this.timeout;
                sqlCommand.Parameters.AddWithValue("@DataName", this.storedProcedure);
                sqlCommand.Parameters.AddWithValue("@Date", auditDay);
                sqlCommand.Parameters.AddWithValue("@AuditType", AuditType.LatestImported.ToString());
							
				sqlCommand.ExecuteNonQuery();	
			}
			catch (Exception exception)
			{
				throw new TDException(String.Format(Messages.Importer_StoredProcedureFailed, UpdateAuditEventStoredProcedureName, exception.Message), false, TDExceptionIdentifier.RDPDataImporterStoredProcedureFailed);
			}		
		}

		/// <summary>
		/// Audits a request specified by the audit type <code>auditType</code>
		/// </summary>
		/// <param name="sqlCommand">Connection to staging database.</param>
		/// <param name="auditType">Type of audit to perform.</param>
		/// <exception cref="TDException">
		/// Thrown if audit fails.
		/// </exception>
		private void AuditRequest(SqlConnection sqlConnection , AuditType auditType)
		{
			
			try
			{
				SqlCommand sqlCommand = new SqlCommand();
				
				sqlCommand.Connection = sqlConnection;
				sqlCommand.CommandText = UpdateAuditEventStoredProcedureName;
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Parameters.Clear();
				sqlCommand.CommandTimeout = this.timeout;
                sqlCommand.Parameters.AddWithValue("@DataName", this.storedProcedure);
                sqlCommand.Parameters.AddWithValue("@Date", this.requestDate);
                sqlCommand.Parameters.AddWithValue("@AuditType", auditType.ToString());
							
				sqlCommand.ExecuteNonQuery();	
			}
			catch (Exception exception)
			{
				throw new TDException(String.Format(Messages.Importer_StoredProcedureFailed, UpdateAuditEventStoredProcedureName, exception.Message), false, TDExceptionIdentifier.RDPDataImporterStoredProcedureFailed);
			}
			
		}
	}
}
