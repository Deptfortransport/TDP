// *********************************************** 
// NAME			: ReportDataArchiverController.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 25/09/2003 
// DESCRIPTION	: Implementation of the ReportDataArchiverController class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportStagingDataArchiver/ReportDataArchiverController.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:10   mturner
//Initial revision.
//
//   Rev 1.10   Nov 26 2003 11:05:12   geaton
//Use ImporterFactory component.
//
//   Rev 1.9   Nov 25 2003 20:05:18   geaton
//Renamed run method. (work in progress to use ImporterFactory dll)
//
//   Rev 1.8   Nov 21 2003 12:16:14   geaton
//Added exception handling for rollbacks.

using System;
using System.Diagnostics;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.ImporterFactory;

namespace  TransportDirect.ReportDataProvider.ReportStagingDataArchiver
{	
	/// <summary>
	/// Responsible for deleting records in a given set of tables based on a date.
	/// Class calls the stored procedure "DeleteReportStagingData",
	/// which takes a table name and based on the criteria in the ReportStagingDataAudit table
	/// retrieves an event date, and then deletes records from the given table
	/// that are before or on the event date.
	/// </summary>
	class ReportDataArchiverController
	{
		/// <summary>
		/// Timeout in seconds, to use when performing archive operations.
		/// </summary>
		private static int timeout = 30;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ReportDataArchiverController()
		{}
	
		/// <summary>
		/// Archives report staging data tables.
		/// </summary>
		/// <returns>Returns zero on success, otherwise an error code.</returns>
		public static int Run()
		{
			int returnCode = 0;

			TransportDirect.ReportDataProvider.ImporterFactory.ImporterFactory importFactory = new TransportDirect.ReportDataProvider.ImporterFactory.ImporterFactory();
			Importer[] importers = null;

			// Create the Importers (these provide an archive service).
			try
			{
				importers = importFactory.CreateImporters(DateTime.Now, timeout, 0);  // Final parameter (CJP Request window) not used by archive service so pass zero.
			}
			catch (TDException tdException)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, String.Format(Messages.Controller_FailedCreatingImporters, tdException.Message)));
				returnCode = (int)tdException.Identifier;
			}
			
			// Check that that Latest Imported Date is same for all importers.
			// This is mandatory prior to archive, since some staging tables are shared 
			// by different importers.
			// (Alternatively could check that shared tables all have the same Latest
			// Imported date, but this is considered overkill.)
			bool latestImportedDatesMatch = false;
			if (importers != null)
			{		
				DateTime latestImported = importers[0].LatestImported;
				latestImportedDatesMatch = true;
			
				for (int i = 1; i < importers.Length; i++)
				{
					// Only check dates match when importer configured to perform archive AND import 
					// ie where an Importer does not perform imports, then safe to archive (if configured to do so)
					if ((importers[i].ArchiveEnabled) && (importers[i].ImportEnabled))
					{
						if (importers[i].LatestImported != latestImported)
							latestImportedDatesMatch = false;
					}
				}

				if (!latestImportedDatesMatch)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, Messages.Controller_LatestImportedMismatch));
					returnCode = (int)TDExceptionIdentifier.RDPStagingArchiverLatestImportedMismatch;
				}
			}

			// Perform the archives.
			if (latestImportedDatesMatch)
			{
				try
				{
					for (int i = 0; i < importers.Length; i++)
					{
						if (importers[i].ArchiveEnabled)
							importers[i].Archive();
					}					
				}
				catch (TDException tdException)
				{
					if (!tdException.Logged)
						Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, tdException.Message));
					
					returnCode = (int)tdException.Identifier;
				}
			}

			return returnCode;
		}
		
	}
}









