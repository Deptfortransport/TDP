using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;
using System.Diagnostics;
using System.Threading;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.ReportDataProvider.ReportDataImporter
{

	/// <summary>
	/// Class to initialise services that are used by the tests.
	/// </summary>
	public class TestInitialization : IServiceInitialisation
	{

		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			// Enable logging service.
			ArrayList errors = new ArrayList();
			try
			{    
				IEventPublisher[] customPublishers = new IEventPublisher[0];

				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException tdEx)
			{
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw tdEx;
			}

			// Insert Test Report Staging Data
			bool SqlLoadSuccessful = ExecuteSetupScript("ReportDataImporter/TestReportStagingData.sql");
			Assert.AreEqual(true, SqlLoadSuccessful, "Loading of Test Report Staging Data failed.");
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
	}

	/// <summary>
	/// Summary description for CheckTestData.
	/// </summary>
	[TestFixture]
	public class TestReportDataImporterController
	{
		
		//initialisation in setup method called before every test method
		[SetUp]
		public void Init()
		{	
			TDServiceDiscovery.Init( new TestInitialization() );
		}

		//finalisation in TearDown method called after every test method
		[TearDown]
		public void CleanUp()
		{
		} 

		/// <summary>
		/// Performs end-to-end test of an import.
		/// </summary>
		/// <remarks>
		[Test]
		[Ignore( "Completely Broken. Awaiting merger of Enhanced Exposed Services (stream 3129) before fix can be applied" )]
		public void TestImport()
		{		

			string[] spNames =  { "TransferEnhancedExposedServicesEvents",
									"TransferGazetteerEvents",
									"TransferJourneyPlanLocationEvents",
									"TransferJourneyPlanModeEvents",
									"TransferJourneyProcessingEvents",
									"TransferJourneyWebRequestEvents",
									"TransferLoginEvents",
									"TransferMapEvents",
									"TransferOperationalEvents",
									"TransferPageEntryEvents",
									"TransferReferenceTransactionEvents",
									"TransferRetailerHandoffEvents",
									"TransferWorkloadEvents",
									"TransferDataGatewayEvents",
									"TransferUserPreferenceSaveEvents",
									"TransferRoadPlanEvents",
									"TransferSessionEvents", 
									"TransferRTTIEvent"	}; 

			string[] tableName =	{   "EnhancedExposedServiceEvents",
										"GazetteerEvents",
										"JourneyPlanLocationEvents",
										"JourneyPlanModeEvents",
										"JourneyProcessingEvents",
										"JourneyWebRequestEvents",
										"LoginEvents",
										"MapEvents",
										"OperationalEvents",
										"PageEntryEvents",
										"ReferenceTransactionEvents",
										"RetailerHandoffEvents",
										"RoadPlanEvents",
										"WorkloadEvents",
										"DataGatewayEvents",
										"UserPreferenceSaveEvents",
										"SessionEvents",
										"RTTIEvents" };

			int numOfReportingTables = tableName.Length;

			string[] sqlQueryEvents =	{  "Select count(*) from EnhancedExposedServiceEvents",
											"Select count(*) from GazetteerEvents",
											"Select count(*) from JourneyPlanLocationEvents",
											"Select count(*) from JourneyPlanModeEvents",
											"Select count(*) from JourneyProcessingEvents",
											"Select count(*) from JourneyWebRequestEvents",
											"Select count(*) from LoginEvents",
											"Select count(*) from MapEvents",
											"Select count(*) from OperationalEvents",
											"Select count(*) from PageEntryEvents",
											"Select count(*) from ReferenceTransactionEvents",
											"Select count(*) from RetailerHandoffEvents",
											"Select count(*) from RoadPlanEvents",
											"Select count(*) from WorkloadEvents",
											"Select count(*) from DataGatewayEvents",
											"Select count(*) from UserPreferenceSaveEvents",
											"Select count(*) from SessionEvents", 
											"Select count(*) from RTTIEvents"};

			// The following counts are taken from the expected results provided in the unit test plan.
			int[] expectedNewRows = {	1,		//EnhancedExposedServices
										13,	// GazetteerEvents
										9,		// JourneyPlanLocationEvents
										15,	// JourneyPlanModeEvents
										2,		// JourneyProcessingEvents
										14,	// JourneyWebRequestEvents
										15,	// LoginEvents
										7,		// MapEvents
										1,		// OperationalEvents
										8,		// PageEntryEvents
										2,	    // ReferenceTransactionEvents
										6,		// RetailerHandoffEvents
										4,		// RoadPlanEvents
										5,		// WorkloadEvents
										1,		// DataGatewayEvents
										9, 	// UserPreferenceSaveEvents
										3,    // SessionEvents
										6 }; // RTTIEvent
			int[] eventCountBefore = new int[numOfReportingTables];
			for ( int i = 0; i < numOfReportingTables; i++ )
			{
				ArrayList tmpEventsCountBefore = CallSqlReader( sqlQueryEvents[i] );
				object[] countValuesBefore = ( object[] ) tmpEventsCountBefore[0];
				eventCountBefore[i] = (int) countValuesBefore[0];
				Assert.IsTrue(eventCountBefore[i] == 0,
					"Data already exists in the reporting tables - remove this before running the test.");
			}

			// Perform import
			string[] args = { "1" }; // import data from yesterday and before
			int returnCode = ReportDataImporterMain.Main( args );
			Console.WriteLine( "Return Code = " + returnCode );
			Assert.IsTrue(returnCode == 0, "Return code on import not zero.");

			// Check that correct number of rows have been inserted.
			// NOTE that a manual check must be performed to ensure that the correct data has been inserted.
		
			int[] eventCountAfter = new int[numOfReportingTables];
			for ( int i = 0; i < numOfReportingTables; i++ )
			{
				ArrayList tmpEventsCountAfter = CallSqlReader( sqlQueryEvents[i] );
				object[] countValuesAfter = ( object[] ) tmpEventsCountAfter[0];
				eventCountAfter[i] = (int) countValuesAfter[0];
			}

			bool eventDataErrors = false;
			for ( int i = 0; i < numOfReportingTables; i++ )
			{
				int added = (eventCountAfter[i] - eventCountBefore[i]);
				if (eventCountAfter[i] != (eventCountBefore[i] +  expectedNewRows[i]))
				{
					eventDataErrors = true;
					string errorString = "Rows on table " + tableName[i] + " incorrect. (ExpectedNewRows:" + expectedNewRows[i].ToString() + " NewRowsAdded:" + added.ToString() + " ";
					Console.WriteLine(errorString);
				}
			}

			if (eventDataErrors)
			{
				Assert.IsTrue(false, "Incorrect number of rows after import - see Console output for details.");
			}

			// Check that latest imported value has been updated to 'yesterday's date' (since a value of 1 was passed to the importer)
			DateTime yesterday = DateTime.Now.Subtract(new TimeSpan(1,0,0,0,0));

			for ( int i = 0; i < spNames.Length; i++ )
			{
				Assert.IsTrue(GetLatestImportedDate(spNames[i]).ToShortDateString() == yesterday.ToShortDateString(),
					"Latest Imported Date has not been updated correctly for table" + spNames[i]);
			}

		}

		/// <summary>
		/// Helper method to determine the latest imported date for a given data set.
		/// </summary>
		/// <param name="dataName">Name of Transfer SP.</param>
		/// <returns>Latest Imported date.</returns>
		private DateTime GetLatestImportedDate(string dataName)
		{
			SqlHelper sqlHelper = new SqlHelper();
			
			// Initialise with day before current Date
			DateTime latestImportedDate = DateTime.Now.Subtract(new TimeSpan(1,0,0,0,0));

			try
			{
				sqlHelper.ConnOpen(SqlHelperDatabase.ReportStagingDB);
				Hashtable parameters = new Hashtable(1);
				parameters.Add("@DataName", dataName);
				object result = sqlHelper.GetScalar("GetLatestImported", parameters);
				latestImportedDate = (DateTime)result;
			}
			catch (Exception) // catch all since no documentation for sqlhelper
			{
				throw new TDException("Failure when getting latest imported date", false, TDExceptionIdentifier.RDPDataImporterStoredProcedureFailed);
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
		/// Helper method to read data from DB using given query
		/// </summary>
		/// <param name="sqlQuery">SQL query to run.</param>
		/// <returns>ArrayList - one entry per row in the results set</returns>
		private ArrayList CallSqlReader( string sqlQuery )
		{
			SqlHelper sqlHelper = new SqlHelper();
			SqlDataReader dataReader = null;
			ArrayList rowList = null;
			SqlConnection sqlConn = new SqlConnection();

			try
			{
				IPropertyProvider currProps = Properties.Current;
				string linkedConnectionString = currProps[Keys.ReportDatabase];

				// Remove the driver option from the connection string.
				string connectionString = linkedConnectionString.Replace("DRIVER={SQL Server};", "");

				sqlConn.ConnectionString = connectionString;

				sqlConn.Open();

				rowList = new ArrayList();
				
				SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);
				sqlCmd.CommandType = CommandType.Text;
				dataReader = sqlCmd.ExecuteReader(CommandBehavior.SingleResult);

				while (dataReader.Read()) 
				{
					object[] values = new object[dataReader.FieldCount];
					dataReader.GetValues( values );
					rowList.Add( values );
				}

			}
			catch( SqlException sqlEx )
			{
				throw new TDException( "SqlException caught : "+ sqlEx.Message, sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure );
			}
			catch( TDException sqlEx )
			{
				string message = "Error Calling SqlQuery : " + sqlQuery + " : " + sqlEx.Message;
				throw new TDException( message, sqlEx, false, TDExceptionIdentifier.RDPSQLHelperStoredProcedureFailure );
			}
			finally
			{
				// Always close the DB connection
				if ( dataReader != null )
				{
					dataReader.Close();
				}

				if ( sqlConn != null )
				{
					sqlConn.Close();
				}
			}

			return rowList;
		} 

		/// <remarks>
		/// Manual verification:
		/// Manual check must be made in reporting tables (in reporting database) to check that report data has been imported.
		/// Manual check must be made on report data audit table (in permanent portal database) to check that datetime has been updated for each staging table.
		/// </remarks>
		[Test]
		[Ignore("Manual verification required")]
		public void ManualVerification()
		{}

		
		/// <remarks>
		/// Manual setup:
		/// 
		/// Due to linked database restrictions it is necessary to use two separate
		/// machines to perform this test. The databases on these machines must be 
		/// specified as follows:
		/// A) Change the location of the database hosting the staging tables. This is specified in the property ReportStagingDB in ReportDataImporterProperties.xml. 
		/// B) Change the location of the database hosting the reporting tables.This is specified in the property ReportDataDB in ReportDataImporterProperties.xml. 
		/// C) Ensure a Linked server ('ReportServer') existing between local sql server instance (hosting the ReportStagingDB) and the second sql server instance (hosting Reporting DB).
		/// D) Ensure DTC (Distributed Transaction Coordinator) Component Service is enabled on both servers
		///
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}

	}
}
