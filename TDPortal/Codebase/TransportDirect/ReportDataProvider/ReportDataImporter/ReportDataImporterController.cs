// *********************************************** 
// NAME                 : ReportDataImporterController
// AUTHOR               : Andy Lole
// DATE CREATED         : 16/09/2003 
// DESCRIPTION			: Controller Class import data from the ReportDataStaging db into the ReportData db.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportDataImporter/ReportDataImporterController.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:02   mturner
//Initial revision.
//
//   Rev 1.16   Aug 03 2004 13:43:08   passuied
//changed database used import events from
//Resolution for 1275: Move the ReportStagingDatabase to another machine - DEL6.0
//
//   Rev 1.15   Nov 26 2003 13:56:06   geaton
//Removed need to use separate connection string (stored as property) for initialising link to Reporting database. - reuse string stored in another property.
//
//   Rev 1.14   Nov 26 2003 11:02:56   geaton
//Use ImporterFactory component.
//
//   Rev 1.13   Nov 25 2003 19:00:48   geaton
//Moved functionality into Importer class.
//
//   Rev 1.12   Nov 24 2003 13:24:02   geaton
//Added warning if no dates to process and fixed code to cope with this situation (the SP GetOldestAuditDate is no longer required following this bug fix).
//
//   Rev 1.11   Nov 23 2003 19:59:18   geaton
//Removed informational that is now logged in Importer class.


using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.ImporterFactory;

namespace TransportDirect.ReportDataProvider.ReportDataImporter
{
	/// <summary>
	/// Summary description for ReportDataImporterController.
	/// </summary>
	public class ReportDataImporterController
	{

		/// <summary>
		/// Name of stored procedure used to initialise link between staging and report database.
		/// </summary>
		private const string InitialiseLinkSPName = "TransferInitialise";

		/// <summary>
		/// Name of stored procedure used to remove link between staging and report database.
		/// </summary>
		private const string RemoveLinkSPName = "TransferComplete";


		/// <summary>
		/// Constructor.
		/// </summary>
		public ReportDataImporterController()
		{
		}

	
		/// <summary>
		/// Performs import.
		/// </summary>
		/// <param name="daysOffset">
		/// Number of days before current date that should be included
		/// in the import. 
		/// Eg If 1 then include data from yesterday and earlier 
		/// (which has not already been processed in a previous Import request).
		/// </param>
		/// <returns>
		/// Zero if successful import,
		/// else greater than zero if failure.
		/// </returns>
		public int Run(int daysOffset)
		{
			int returnCode = 0;
		
			try
			{
				// Initialise the link between Staging DB and Report DB.
				DBTransferInitialise();

				// Create the Importers.
				TransportDirect.ReportDataProvider.ImporterFactory.ImporterFactory importFactory = new TransportDirect.ReportDataProvider.ImporterFactory.ImporterFactory();
				Importer[] importers = 
					importFactory.CreateImporters(DateTime.Now,
												  int.Parse(Properties.Current[Keys.ImportTimeout]),
												  int.Parse(Properties.Current[Keys.CJPWebRequestWindow]));

				// Derive toDate based on current date and offset.
				DateTime toDate = DateTime.Now.Date.Subtract(new TimeSpan(daysOffset, 0 , 0, 0));

				// Run the Importers.
				for (int i = 0; i < importers.Length; i++)
				{
					if (importers[i].ImportEnabled)
						importers[i].Import(toDate);
				}

			}
			catch (TDException tdException)
			{
				returnCode = (int)tdException.Identifier;
				
				if (!tdException.Logged)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, tdException.Message));
				}
			}
			finally
			{
				try
				{
					// Remove database link
					DBTransferComplete();
				}
				catch (TDException tdException)
				{
					// Don't overwrite return code if already set with more important code.
					if (returnCode == 0)
						returnCode =  (int)tdException.Identifier;

					if (!tdException.Logged)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, tdException.Message));
					}
				}
			}

			return returnCode;
		}

		/// <summary>
		/// Sets up the link between the Staging DB and the Report DB.
		/// </summary>
		/// <exception cref="TDException">
		/// Thrown if initialisation fails.
		/// </exception>
		private void DBTransferInitialise()
		{
            Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, Messages.Controller_DBTransferLink));

			SqlHelper sqlHelper = new SqlHelper();
			sqlHelper.ConnOpen(SqlHelperDatabase.ReportStagingDB);

			Hashtable parameters = new Hashtable(1);

			parameters.Add("@ConnectionString", Properties.Current[SqlHelperDatabase.ReportDataDB.ToString()]);

			try
			{
				sqlHelper.Execute(InitialiseLinkSPName, parameters);
			}
			catch (Exception exception) // Catch all since no documentation for sqlHelper.
			{
				throw new TDException(String.Format(Messages.Controller_DBTransferLinkFail, exception.Message), false, TDExceptionIdentifier.RDPDataImporterLinkInitFailed);
			}
			finally
			{
				sqlHelper.ConnClose();
			}

		}

		/// <summary>
		/// Disconnects the ReportDataStaging DB and ReportData DB.
		/// </summary>
		/// /// <exception cref="TDException">
		/// Thrown if disconnect fails.
		/// </exception>
		private void DBTransferComplete()
		{
            Trace.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Verbose, Messages.Controller_DBRemoveLink));

			SqlHelper sqlHelper = new SqlHelper();
			sqlHelper.ConnOpen( SqlHelperDatabase.ReportStagingDB );

			Hashtable parameters = new Hashtable();

			try
			{
				sqlHelper.Execute(RemoveLinkSPName, parameters );
			}
			catch (Exception exception) // Catch all since no documentation for sqlHelper.
			{
				throw new TDException(String.Format(Messages.Controller_DBRemoveLinkFailed, exception.Message), false, TDExceptionIdentifier.RDPDataImporterLinkRemoveFailed);
			}
			finally
			{
				sqlHelper.ConnClose();
			}
		}

	}
}
