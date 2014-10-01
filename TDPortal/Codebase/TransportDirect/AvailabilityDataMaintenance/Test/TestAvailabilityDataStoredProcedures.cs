// *********************************************** 
// NAME			: TestAvailabilityDataStoredProcedures.cs
// AUTHOR		: James Broome
// DATE CREATED	: 26/01/2005
// DESCRIPTION	: Tests all the stored procedures used within the application
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AvailabilityDataMaintenance/Test/TestAvailabilityDataStoredProcedures.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:58   mturner
//Initial revision.
//
//   Rev 1.4   Apr 21 2005 14:39:16   jbroome
//Added error handling for initialisation
//
//   Rev 1.3   Apr 15 2005 14:32:52   jbroome
//Restructed test folders
//
//   Rev 1.2   Apr 15 2005 13:48:28   jbroome
//Updated test code as ImportProductProfiles now uses a .csv file for import.
//
//   Rev 1.1   Mar 21 2005 10:55:58   jbroome
//Minor updates after code review
//
//   Rev 1.0   Feb 08 2005 10:39:32   jbroome
//Initial revision.

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;
using System.Threading;
using System.Xml;
using System.Text;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.AvailabilityDataMaintenance
{
	/// <summary>
	/// Class used to test the stored procedure functionality 
	/// used within the application.
	/// </summary>
	[TestFixture]
	public class TestAvailabilityDataStoredProcedures
	{
		
		#region Private Members

		// Stored Procs
		private const string SPGetAvailabilityHistory =		"GetAvailabilityHistory";
		private const string SPDeleteAvailabilityHistory =	"DeleteAvailabilityHistory";
		private const string SPImportProductProfiles =		"ImportProductProfiles"; 
		private const string SPGetProductProfiles =			"GetProductProfiles";
		private const string SPDeleteUnavailableProducts =	"DeleteUnavailableProducts";

		// Params
		private const string paramXml =			"@Xml";
		private const string paramHistoryId =	"@HistoryId";
		
		#endregion

		#region Constructor and Initialisation

		/// <summary>
		/// Constructor
		/// </summary>
		public TestAvailabilityDataStoredProcedures()
		{

		}

		/// <summary>
		/// Initialisation sets up DB for tests to run
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{			
			try
			{
				// Initialise property service etc.
				TDServiceDiscovery.Init(new TestAvailabilityDataMaintenanceInitialisation());

				// Enure database has been set up sucessfully, or do not continue
				bool InitSuccessful = ExecuteSetupScript("AvailabilityDataMaintenance/SetUp.sql");
				Assert.AreEqual(true, InitSuccessful, "Database setup failed during initialisation. Unable to continue with tests.");
			}
			catch
			{
				throw new TDException("Set up failed for TestAvailabilityDataStoredProcedures.", false, TDExceptionIdentifier.AETDTraceInitFailed);	
			}
		}

		/// <summary>
		/// Clean Up undoes DB changes performed during tests
		/// </summary>
		[TestFixtureTearDown]
		public void CleanUp()
		{
			// Remove test data from Database
			bool CleanUpSuccessful = ExecuteSetupScript("AvailabilityDataMaintenance/CleanUp.sql");
			Assert.AreEqual(true, CleanUpSuccessful, "Database clean up failed during Tear Down.");
		}


		/// <summary>
		/// Enables Custom Test Setups to prepare the Database via scripts run in osql.exe
		/// </summary>
		/// <param name="scriptName"></param>
		/// <returns></returns>
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

		# endregion

		#region Test Methods
		
		/// <summary>
		/// Tests the 
		///		GetAvailabilityHistory 
		///		DeleteAvailabilityHistory 
		///	stored procedures
		///	Need to be packaged together as order of 
		///	procedure calls	is important.
		/// </summary>
		[Test]
		public void TestGetDeleteAvailabilityHistory()
		{
			SqlHelper helper = new SqlHelper();
			int returnValue = 0;
			DataSet dsResults = null;

			// Test #1 GetAvailabilityHistory
			// Returns correct number of rows
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			dsResults = helper.GetDataSet(SPGetAvailabilityHistory);
			helper.ConnClose();
			Assert.AreEqual(10, dsResults.Tables[0].Rows.Count, "Error in test #1 with GetAvailabilityHistory()");

			// Test #2 GetAvailabilityHistory
			// Returns correct number of columns
			Assert.AreEqual(8, dsResults.Tables[0].Columns.Count, "Error in test #2 with GetAvailabilityHistory()");

			// Test #3 Test first row of data
			object[] firstRow = dsResults.Tables[0].Rows[0].ItemArray; 
			int historyId = (int)firstRow[0];
			DateTime requestDate = (DateTime)firstRow[1];
			Assert.AreEqual(requestDate.ToShortDateString(), DateTime.Now.ToShortDateString(), "error1");
			Assert.AreEqual("TEST", firstRow[2].ToString(), "Error in test #3 with first data row");
			Assert.AreEqual("TEST", firstRow[3].ToString(), "Error in test #3 with first data row");
			Assert.AreEqual("TEST", firstRow[4].ToString(), "Error in test #3 with first data row");
			DateTime travelDate = (DateTime)firstRow[5];
			Assert.AreEqual(travelDate.ToShortDateString(), DateTime.Now.ToShortDateString(), "error1");
			Assert.AreEqual("TEST", firstRow[6].ToString(), "Error in test #3 with first data row");
			Assert.AreEqual("True", firstRow[7].ToString(), "Error in test #3 with first data row");
				
			// Test #4 DeleteAvailabilityHistory
			// Successful deletion of first row
			Hashtable parameters = new Hashtable();
			parameters.Add(paramHistoryId, historyId);
			Hashtable types = new Hashtable();
			types.Add(paramHistoryId, SqlDbType.Int);

			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			returnValue = (int)helper.Execute(SPDeleteAvailabilityHistory, parameters, types);
			helper.ConnClose();
			// ExecuteNonQuery returns -1 if NOCOUNT ON is set
			Assert.AreEqual(-1, returnValue, "Error in test #4 with DeleteAvailabilityHistory()");

			// Test #5 GetAvailabilityHistory
			// Returns correct number of rows, after deletion
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			dsResults = helper.GetDataSet(SPGetAvailabilityHistory);
			helper.ConnClose();
			Assert.AreEqual(9, dsResults.Tables[0].Rows.Count, "Error in test #5 with GetAvailabilityHistory()");

			// Test #6 DeleteAvailabilityHistory 
			// Successful deletion of second row
			parameters[paramHistoryId] = historyId+1;
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			returnValue = (int)helper.Execute(SPDeleteAvailabilityHistory, parameters, types);
			helper.ConnClose();
			// ExecuteNonQuery returns -1 if NOCOUNT ON is set
			Assert.AreEqual(-1, returnValue, "Error in test #6 with DeleteAvailabilityHistory()");

			// Test #7 GetAvailabilityHistory
			// Returns correct number of rows, after deletion
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			dsResults = helper.GetDataSet(SPGetAvailabilityHistory);
			helper.ConnClose();
			Assert.AreEqual(8, dsResults.Tables[0].Rows.Count, "Error in test #7 with GetAvailabilityHistory()");

			// Test #8 DeleteAvailabilityHistory
			// Does not delete when HistoryId is invalid
			parameters[paramHistoryId] = -1;
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			returnValue = (int)helper.Execute(SPDeleteAvailabilityHistory, parameters, types);
			helper.ConnClose();
			// ExecuteNonQuery returns -1 if NOCOUNT ON is set
			Assert.AreEqual(-1, returnValue, "Error in test #8 with DeleteAvailabilityHistory()");

			// Test #9 GetAvailabilityHistory
			// Returns correct number of rows, after unsuccessful deletion
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			dsResults = helper.GetDataSet(SPGetAvailabilityHistory);
			helper.ConnClose();
			Assert.AreEqual(8, dsResults.Tables[0].Rows.Count, "Error in test #9 with GetAvailabilityHistory()");

		}

		/// <summary>
		/// Tests the 
		///		ImportProductProfiles 
		///		GetProductProfiles
		///	stored procedures
		///	Need to be packaged together as order of 
		///	procedure calls	is important.
		/// </summary>
		[Test]
		public void TestImportGetProductProfiles()
		{
			int result = 0;
			string xml = string.Empty;
			SqlHelper helper = new SqlHelper();
			
			// Set up stored proc parameters
			Hashtable parameters = new Hashtable();
			parameters.Add(paramXml, xml);
			Hashtable types = new Hashtable();
			types.Add(paramXml, SqlDbType.Text);

			// Test #1 ImportProductProfiles
			// Pass in an empty XML string - error expected
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			try
			{
				result = (int)helper.Execute(SPImportProductProfiles, parameters, types);
				Assert.Fail("Error in test #1 with ImportProductProfiles(). Invalid XML string did not throw error");
			}
			catch
			{ // Expected - do nothing 
			}
			finally
			{
				helper.ConnClose();
			}


			// Test #2 ImportProductProfiles
			// Pass in an invalid XML string - error handled smoothly
			
			#region Define xml string
			xml =			"<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
								"<ProductProfiles" + // element not closed properly
									"<Profile>" + 
										"<Mode>TEST3</Mode>" + 
										"<Origin>TEST3</Origin>" + 
										"<Destination>TEST3</Destination>" + 
										"<Category>FullyFlexible</Category>" + 
										"<DayType>Weekday</DayType>" + 
										"<DayRange>0</DayRange>" + 
										"<Probability>High</Probability>" + 
									"</Profile>" +
								"</ProductProfiles>";
			#endregion

			// Update parameters
			parameters[paramXml] = xml;

			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			try
			{
				result = helper.Execute(SPImportProductProfiles, parameters, types);
				Assert.Fail("Error in test #2 with ImportProductProfiles(). Invalid XML string did not throw error");
			}
			catch
			{ // Expected - do nothing
			}
			finally
			{
				helper.ConnClose();
			}

			// Test #3 ImportProductProfiles
			// Test valid input

			#region Define xml string
			xml =			"<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
								"<ProductProfiles>" + 
									"<Profile>" + 
										"<Mode>TEST3</Mode>" + 
										"<Origin>TEST3</Origin>" + 
										"<Destination>TEST3</Destination>" + 
										"<Category>FullyFlexible</Category>" + 
										"<DayType>Weekday</DayType>" + 
										"<DayRange>0</DayRange>" + 
										"<Probability>High</Probability>" + 
									"</Profile>" +
									"<Profile>" + 
										"<Mode>TEST3</Mode>" + 
										"<Origin>TEST3</Origin>" + 
										"<Destination>TEST3</Destination>" + 
										"<Category>FullyFlexible</Category>" + 
										"<DayType></DayType>" + 
										"<DayRange>0</DayRange>" + 
										"<Probability>High</Probability>" + 
									"</Profile>" +
									"<Profile>" + 
										"<Mode>TEST3</Mode>" + 
										"<Origin>TEST3</Origin>" + 
										"<Destination>TEST3</Destination>" + 
										"<Category></Category>" + 
										"<DayType>Weekday</DayType>" + 
										"<DayRange>0</DayRange>" + 
										"<Probability>High</Probability>" + 
									"</Profile>" +
								"</ProductProfiles>";
			#endregion

			// Update parameters
			parameters[paramXml] = xml;

			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			result = helper.Execute(SPImportProductProfiles, parameters, types);
			helper.ConnClose();
			// ExecuteNonQuery returns -1 if NOCOUNT ON is set
			Assert.AreEqual(-1, result, "Error in test #3 with ImportProductProfiles()");
		
			// Testing for GetProductProfiles ....
			
			DataSet dsResults = null;

			// Test #4 GetProductProfiles
			// Returns correct number of rows
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			dsResults = helper.GetDataSet(SPGetProductProfiles);
			helper.ConnClose();
			Assert.AreEqual(3, dsResults.Tables[0].Rows.Count, "Error in test #4 with GetProductProfiles()");

			// Test #5 GetProductProfiles
			// Returns correct number of cols
			Assert.AreEqual(7, dsResults.Tables[0].Columns.Count, "Error in test #5 with GetProductProfiles()");
			
			// Test # Test first row of data
			object[] firstRow = dsResults.Tables[0].Rows[0].ItemArray; 
			Assert.AreEqual("TEST3", firstRow[0].ToString(), "Error in test #6 with first data row");
			Assert.AreEqual("TEST3", firstRow[1].ToString(), "Error in test #7 with first data row");
			Assert.AreEqual("TEST3", firstRow[2].ToString(), "Error in test #8 with first data row");
			Assert.AreEqual("FullyFlexible", firstRow[3].ToString(), "Error in test #9 with first data row");
			Assert.AreEqual("Weekday", firstRow[4].ToString(), "Error in test #10 with first data row");
			Assert.AreEqual("0", firstRow[5].ToString(), "Error in test #11 with first data row");
			Assert.AreEqual("High", firstRow[6].ToString(), "Error in test #12 with first data row");

			// Delete the test data
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			result = (int)helper.Execute("DELETE ProductProfile WHERE Mode like 'TEST%'");
			helper.ConnClose();

			// Test #13 GetProductProfiles 
			// Should now only be one row left
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			dsResults = helper.GetDataSet(SPGetProductProfiles);
			helper.ConnClose();
			Assert.AreEqual(0, dsResults.Tables[0].Rows.Count, "Error in test #13 with GetProductProfiles()");
		}

		/// <summary>
		/// Tests the DeleteUnavailableProducts stored procedure
		/// </summary>
		[Test]
		public void TestDeleteUnavailableProducts()
		{
			SqlHelper helper = new SqlHelper();
			int returnValue = 0;

			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			int numberOfRows = (int)helper.GetScalar("SELECT COUNT (*) FROM UnavailableProducts");
			helper.ConnClose();
			// Test #1 Test that there are the correct 
			// number of rows before calling stored procedure
			Assert.AreEqual(9, numberOfRows, "Error in test #1 with DeleteUnavailableProducts()");

			// Test #2 DeleteUnavailableProducts
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			returnValue = (int)helper.Execute(SPDeleteUnavailableProducts);
			helper.ConnClose();
			// ExecuteNonQuery returns -1 if NOCOUNT ON is set
			Assert.AreEqual(-1, returnValue, "Error in test #2 with DeleteUnavailableProducts()");
		
			helper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			numberOfRows = (int)helper.GetScalar("SELECT COUNT (*) FROM UnavailableProducts");
			helper.ConnClose();
			// Test #3 Test that there are the correct 
			// number of rows left after calling stored procedure
			// Only historic records are deleted (travel date is in the past)
			Assert.AreEqual(5, numberOfRows, "Error in test #3 with DeleteUnavailableProducts()");
			helper.ConnClose();
			helper.Dispose();
		}

		#endregion

	}
}
