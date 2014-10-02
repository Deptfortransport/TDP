// *********************************************** 
// NAME			: TestReportDataArchiverController.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 25/09/2003 
// DESCRIPTION	: Implementation of the TestReportDataArchiverController class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportStagingDataArchiver/TestReportDataArchiverController.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:14   mturner
//Initial revision.
//
//   Rev 1.11   Feb 10 2006 09:16:02   kjosling
//Turned off failing unit tests
//
//   Rev 1.10   May 20 2005 13:22:48   NMoorhouse
//Post Del7 NUnit Updates - Automate Data Loading
//
//   Rev 1.9   Feb 08 2005 09:05:18   RScott
//Assertion changed to Assert
//
//   Rev 1.8   Apr 01 2004 17:14:16   geaton
//Updates following unit test refactoring exercise.
//
//   Rev 1.7   Dec 01 2003 19:26:08   geaton
//Updated test following refactoring of importer factory.
//
//   Rev 1.6   Nov 19 2003 19:48:46   geaton
//Added extra assertions.
//
//   Rev 1.5   Nov 19 2003 11:38:56   geaton
//Refactored.

using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Globalization;
using System.Threading;

using TransportDirect.Common.ServiceDiscovery;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.ReportStagingDataArchiver
{
	/// <summary>
	/// Tests that the TestReportDataArchiverController class functions correctly.	
	/// </summary>
	[TestFixture]
	public class TestReportDataArchiverController
	{
		public TestReportDataArchiverController()
		{
			TDServiceDiscovery.Init( new ReportDataArchiverInitialisation() );

		}

		/// <remarks>
		/// Manual verification:
		/// Manual check must be made in staging tables to check that staging data has been removed.
		/// Manual check must be made on report data audit table to check that datetime has been updated for each staging table.
		/// </remarks>
		[Test]
		[Ignore("Manual verification required")]
		public void ManualVerification()
		{}

		
		/// <remarks>
		/// Manual setup:
		///
		/// The following batch files must be run in the order below prior to 
		/// running this test:
		/// 1. Resource\Dev\Data\2_PopulatePortalDatabases.bat  (this will set default values for the audit table)
		/// 2. TestPopulateReportStagingDataTables.bat  (this provides test report staging data)
		/// These batch files must be run before EACH test in this suite.
		///
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}

		/// <summary>
		/// Checks the number of rows in the tables to be processed,
		/// then calls the stored procedure to delete the rows,
		/// and finally gets the new count of rows in the tables.
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk")]
		public void ArchiveWhereAllDataHistoric()
		{
			// Insert Test Report Staging Data
			bool SqlLoadSuccessful = ExecuteSetupScript("ReportStagingDataArchiver/TestReportStagingData.sql");
			Assert.AreEqual(true, SqlLoadSuccessful, "Loading of Test Report Staging Data failed.");

			ArrayList errors = new ArrayList();	

			SqlConnection  sqlConn  = null;

			try
			{
				
				// Determine total number of rows in staging tables.
				string connstr = Properties.Current[ SqlHelperDatabase.ReportStagingDB.ToString() ];
				sqlConn = new SqlConnection( connstr );
				sqlConn.Open();	
				ArrayList tablecounts = GetTablesInfoList( sqlConn );
			
				int totalRowsBefore = 0;
				foreach( TableInfo ti in tablecounts )				
					totalRowsBefore += ti.numRows;				

				Assert.IsTrue(totalRowsBefore > 0,
					"There is no staging data in the database to test archiving.");

				// Perfom archive.
				string[] args = { };
				int returnCode = ReportDataArchiverMain.Main( args );

				Console.WriteLine( "Return Code = " + returnCode );
				Assert.IsTrue(returnCode == 0,
					"Return code on import not zero.");

				// Check that all tables have been cleared.
				tablecounts.Clear();
				tablecounts = GetTablesInfoList( sqlConn );
				int totalRowsAfter = 0;
				foreach( TableInfo ti in tablecounts )
					totalRowsAfter += ti.numRows;				
				Assert.IsTrue( totalRowsAfter == 0 );
				
			}
			finally
			{
				if( sqlConn != null )
				{
					sqlConn.Close();			
				}
				
			}				
		}
		
		/// <summary>
		/// Creates one new record in the LoginEvent table, 
		/// and then executes the stored procedure to archive the tables.
		/// Only one record should be left.
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk")]
		public void ArchiveWhereSomeDataCurrent()
		{
			// Insert Test Report Staging Data
			bool SqlLoadSuccessful = ExecuteSetupScript("ReportStagingDataArchiver/TestReportStagingData.sql");
			Assert.AreEqual(true, SqlLoadSuccessful, "Loading of Test Report Staging Data failed.");

			ArrayList errors = new ArrayList();	

			SqlConnection  sqlConn  = null;

			try
			{
				// Determine total number of rows in staging tables.
				string connstr = Properties.Current[ SqlHelperDatabase.ReportStagingDB.ToString() ];
				sqlConn = new SqlConnection( connstr );
				sqlConn.Open();	
				ArrayList tablecounts = GetTablesInfoList( sqlConn );
			
				int totalRowsBefore = 0;
				foreach( TableInfo ti in tablecounts )				
					totalRowsBefore += ti.numRows;				
				sqlConn.Close();
				sqlConn = null;	

				Assert.IsTrue(totalRowsBefore > 0,
					"There is no staging data in the database to test archiving.");

				// Insert some new data to test that it is not archived
				sqlConn = new SqlConnection( connstr );
				sqlConn.Open();	
				DateTime dt = DateTime.Now;				
				StringBuilder sSql = new StringBuilder("INSERT INTO LoginEvent (SessionId, UserLoggedon, TimeLogged) ");
				sSql.Append( "SELECT '0G', 1, '" + dt.ToString("MM/dd/yyyy hh:mm:ss") + "'");
				SqlCommand sqlCmd = new SqlCommand( sSql.ToString(), sqlConn );				
				sqlCmd.ExecuteNonQuery();
				tablecounts = GetTablesInfoList( sqlConn );
				int totalRowsBeforePlusNew = 0;
				foreach( TableInfo ti in tablecounts )				
					totalRowsBeforePlusNew += ti.numRows;				
				sqlConn.Close();
				sqlConn = null;
				Assert.IsTrue(totalRowsBeforePlusNew == (totalRowsBefore + 1),
					"Failed to add new rows.");


				// Perfom archive.
				string[] args = { };
				int returnCode = ReportDataArchiverMain.Main( args );
				Console.WriteLine( "Return Code = " + returnCode );
				Assert.IsTrue(returnCode == 0,
					"Return code on import not zero.");

				// Check that two rows are left
				sqlConn = new SqlConnection( connstr );
				sqlConn.Open();	
				ArrayList tablecountsAfter = GetTablesInfoList( sqlConn );
				int rowsAfter = 0;
				foreach( TableInfo ti in tablecountsAfter )				
					rowsAfter += ti.numRows;				
				Assert.IsTrue(rowsAfter == 1,
					"Newly added rows have been archived!");
			}					
			finally
			{
				if( sqlConn != null )
				{
					sqlConn.Close();			
				}				
			}				
		}

		/// <summary>
		/// Returns an array list of TableInfo objects,
		/// which contain the table name and the number of rows in the table.
		/// </summary>
		/// <param name="sqlConn">SQL Connection object.</param>
		/// <returns>ArrayList of TableInfo objects.</returns>
		private ArrayList GetTablesInfoList( SqlConnection sqlConn )
		{
			ArrayList tablecounts = new ArrayList();
			tablecounts.Add( new TableInfo("GazetteerEvent") );								
			tablecounts.Add( new TableInfo("JourneyPlanRequestEvent") );								
			tablecounts.Add( new TableInfo("JourneyPlanResultsEvent") );								
			tablecounts.Add( new TableInfo("JourneyWebRequestEvent") );								
			tablecounts.Add( new TableInfo("LocationRequestEvent") );								
			tablecounts.Add( new TableInfo("LoginEvent") );								
			tablecounts.Add( new TableInfo("MapEvent") );								
			tablecounts.Add( new TableInfo("OperationalEvent") );								
			tablecounts.Add( new TableInfo("PageEntryEvent") );								
			tablecounts.Add( new TableInfo("ReferenceTransactionEvent") );								
			tablecounts.Add( new TableInfo("RetailerHandoffEvent") );								
			tablecounts.Add( new TableInfo("WorkloadEvent") );								
			tablecounts.Add( new TableInfo("DataGatewayEvent") );								
			tablecounts.Add( new TableInfo("UserPreferenceSaveEvent") );								
			tablecounts.Add( new TableInfo("JourneyPlanRequestVerboseEvent") );								
			tablecounts.Add( new TableInfo("JourneyPlanResultsVerboseEvent") );	

			for( int i=0; i < tablecounts.Count; i++ )
			{					
				DataSet ds = new DataSet();
				SqlDataAdapter sqlDA = new SqlDataAdapter( "SELECT COUNT(*) FROM " + ((TableInfo)tablecounts[i]).tableName, sqlConn );				
				
				sqlDA.Fill( ds );

				string s = ds.Tables[0].Rows[0][0].ToString();

				((TableInfo)tablecounts[i]).numRows = int.Parse( ds.Tables[0].Rows[0][0].ToString() );
			}

			return tablecounts;
		}

		private static bool ExecuteSetupScript(string scriptName)
		{
			Process sql = new Process();
			sql.StartInfo.FileName = "osql.exe";
			sql.StartInfo.Arguments = "-E -i \"" + System.IO.Directory.GetCurrentDirectory() + "\\" + scriptName + "\"";
			sql.StartInfo.WorkingDirectory = System.IO.Directory.GetCurrentDirectory();
			sql.Start();

			// Wait for it to finish
			while (!sql.HasExited)
				Thread.Sleep(1000);

			return (sql.ExitCode == 0);
		}

		/// <summary>
		/// Stores table name and count of rows in the table.
		/// </summary>
		private class TableInfo
		{
			public TableInfo( string tableName )
			{
				this.tableName = tableName;
			}

			public string tableName = null;
			public int    numRows   = 0;
		}

	}
}
















