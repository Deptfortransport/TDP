// *********************************************** 
// NAME			: TestAvailabilityEstimatorDBHelper.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Test implementation of the AvailabilityEstimatorDBHelper class
//				  This class handles all the necessary database calls					
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/AvailabilityEstimator/TestAvailabilityEstimatorDBHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:16   mturner
//Initial revision.
//
//   Rev 1.5   May 17 2006 15:01:56   rphilpott
//Add RouteCode to UnavailableProducts table, associated SP's and all classes that use them.
//Resolution for 4084: DD075: Unavailable products - ticket and route codes
//
//   Rev 1.4   Apr 28 2005 16:36:22   jbroome
//UnavailableProducts now stored by Outward and Return dates
//Resolution for 2302: PT - Product availability does not handle return products adequately.
//
//   Rev 1.3   Apr 15 2005 13:55:52   jbroome
//Removed foreign key between AvailabilityHistory and TicketCategory tables.
//
//   Rev 1.2   Mar 18 2005 14:47:04   jbroome
//Added missing class documentation comments (Code Review)
//
//   Rev 1.1   Feb 17 2005 14:43:40   jbroome
//Updated test fixtures after changes to real classes
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 09:56:16   jbroome
//Initial revision.

using System;
using System.Diagnostics;
using System.Threading;
using System.Data.SqlClient;
using System.Collections;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;


namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Test class which tests the public methods of the AvailabilityEstimatorDBHelper class
	/// </summary>
	[TestFixture]
	public class TestAvailabilityEstimatorDBHelper
	{
		
		#region Constructor and Initialisation
		
		/// <summary>
		/// Constructor
		/// </summary>
		public TestAvailabilityEstimatorDBHelper()
		{

		}

		/// <summary>
		/// Initialisation sets up DB for tests to run
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{			
			// Initialise property service etc.
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());

			// Enure database has been set up sucessfully, or do not continue
			bool InitSuccessful = ExecuteSetupScript("SetUp.sql");
			Assert.AreEqual(true, InitSuccessful, "Database setup failed during initialisation. Unable to continue with tests.");
		}

		/// <summary>
		/// Clean Up undoes DB changes performed during tests
		/// </summary>
		[TestFixtureTearDown]
		public void CleanUp()
		{
			// Remove test data from Database
			bool CleanUpSuccessful = ExecuteSetupScript("CleanUp.sql");
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
			sql.StartInfo.Arguments = "-E -i \"" + System.IO.Directory.GetCurrentDirectory() + "\\AvailabilityEstimator\\" + scriptName + "\"";
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
		/// Tests the GetProductProfile stored procedure
		/// </summary>
		[Test]
		public void TestGetProductProfile()
		{
			AvailabilityEstimatorDBHelper helper = new AvailabilityEstimatorDBHelper();
			string probability = string.Empty; // To hold return values from GetProductProfile

			TDDateTime travelDate = new TDDateTime(2010, 01, 01);

			// Test #1 GetProductProfile 
			// Return profile matching on all values
			probability = helper.GetProductProfile("TEST1", "TEST", "TEST", "TEST1", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #1 with GetProductProfile()");

			// Test #2 GetProductProfile 
			// Return profile using default DayType
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST2", "TEST", "TEST", "TEST1", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #2 with GetProductProfile()");

			// Test #3 GetProductProfile 
			// Return profile using default Category
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST3", "TEST", "TEST", "TEST1", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #3 with GetProductProfile()");

			// Test #4 GetProductProfile 
			// Return profile using default DayType, default Category
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST4", "TEST", "TEST", "TEST1", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #4 with GetProductProfile()");

			// Test #5 GetProductProfile 
			// Return profile using default Route
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST5", "TEST", "TEST", "TEST1", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #5 with GetProductProfile()");

			// Test #6 GetProductProfile 
			// Return profile using default Route, default DayType
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST6", "TEST", "TEST", "TEST1", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #6 with GetProductProfile()");

			// Test #7 GetProductProfile 
			// Return profile using default Route, default Category
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST7", "TEST", "TEST", "TEST1", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #7 with GetProductProfile()");

			
			// Test #8 GetProductProfile 
			// Return profile using default Route, default DayType, default Category
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST8", "TEST", "TEST", "TEST1", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #8 with GetProductProfile()");


			// Test #9 GetProductProfile 
			// Specify unknown Route values
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST9", "UNKNOWN", "UNKNOWN", "TEST1", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #9 with GetProductProfile()");

			// Test #10 GetProductProfile 
			// Specify unknown TicketCode
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST10", "TEST", "TEST", "UNKNOWN", travelDate);
			Assert.AreEqual("High", probability, "Error in Test #10 with GetProductProfile()");

			// Test #11 GetProductProfile 
			// Specify unknown Date
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST11", "TEST", "TEST", "TEST1", new TDDateTime(2011, 01, 01));
			Assert.AreEqual("High", probability, "Error in Test #11 with GetProductProfile()");

			// Test #12 GetProductProfile 
			// Test correct date range value selected
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST12", "TEST", "TEST", "TEST1", new TDDateTime(2010, 01, 01));
			Assert.AreEqual("High", probability, "Error in Test #12 with GetProductProfile()");

			// Test #13 GetProductProfile 
			// Test correct date range null value selected
			probability = string.Empty;
			probability = helper.GetProductProfile("TEST13", "TEST", "TEST", "TEST1", new TDDateTime(2010, 01, 01));
			Assert.AreEqual("High", probability, "Error in Test #13 with GetProductProfile()");

			// Test #14 GetProductProfile 
			// Test profile with invalid input (Mode = XXXXX)
			probability = string.Empty;
			probability = helper.GetProductProfile("XXXXX", "TEST", "TEST", "TEST1", travelDate);
			Assert.AreEqual(string.Empty, probability, "Error in Test #14 with GetProductProfile()");
            
		}
		
		/// <summary>
		/// Tests the 
		///		- AddUnavailableProducts,
		///		- CheckUnavailableProducts 
		///	stored procedures.
		///	Have to package these in one method as 
		///	order of calls is important.
		/// </summary>
		[Test]
		public void TestAddCheckUnavailableProducts()
		{

			AvailabilityEstimatorDBHelper helper = new AvailabilityEstimatorDBHelper();

			TDDateTime outTravelDate = new TDDateTime(2010, 01, 01);
			TDDateTime retTravelDate = new TDDateTime(2010, 01, 02);

			TDDateTime outTravelDate2 = new TDDateTime(2010, 01, 02);
			TDDateTime retTravelDate2 = new TDDateTime(2010, 01, 03);

			// Test #1 CheckUnavailableProducts 
			// We know product is not unavailable
			Assert.AreEqual(false, helper.CheckUnavailableProducts("TEST1", "TEST", "TEST", outTravelDate, retTravelDate, "TEST1", string.Empty), "Error in Test #1 with CheckUnavailableProducts() using Outward and Return dates.");
			Assert.AreEqual(false, helper.CheckUnavailableProducts("TEST1", "TEST", "TEST", outTravelDate, null, "TEST1", string.Empty), "Error in Test #1 with CheckUnavailableProducts() using Outward date only.");

			// Test #2 AddUnavailableProduct successfully
			Assert.AreEqual(true, helper.AddUnavailableProduct("TEST1", "TEST", "TEST", outTravelDate, retTravelDate, "TEST1", string.Empty), "Error in Test #2 with AddUnavailableProduct() using Outward and Return dates.");
			Assert.AreEqual(true, helper.AddUnavailableProduct("TEST1", "TEST", "TEST", outTravelDate, null, "TEST1", string.Empty), "Error in Test #2 with AddUnavailableProduct() using Outward date only.");
			Assert.AreEqual(true, helper.AddUnavailableProduct("TEST1", "TEST", "TEST", outTravelDate2, retTravelDate2, "TEST1", string.Empty), "Error in Test #2 with AddUnavailableProduct() using Outward date 2 and Return date.");

			// Test #3 AddUnavailableProduct with duplicate product
			// Does not insert record and does not throw exception
			Assert.AreEqual(false, helper.AddUnavailableProduct("TEST1", "TEST", "TEST", outTravelDate, retTravelDate, "TEST1", string.Empty), "Error in Test #3 with AddUnavailableProduct() using Outward and Return dates.");
			Assert.AreEqual(false, helper.AddUnavailableProduct("TEST1", "TEST", "TEST", outTravelDate, null, "TEST1", string.Empty), "Error in Test #3 with AddUnavailableProduct() using Outward date only.");
			
			// Test #4 AddUnavailableProduct with invalid input
			// Force error to be raised, but handled smoothly
			// Violate foreign key constraint on TicketCode
			Assert.AreEqual(false, helper.AddUnavailableProduct("TEST1", "TEST", "TEST", outTravelDate, retTravelDate, "XXXX", string.Empty), "Error in Test #4 with AddUnavailableProduct() using Outward and Return dates.");
			Assert.AreEqual(false, helper.AddUnavailableProduct("TEST1", "TEST", "TEST", outTravelDate, null, "XXXX", string.Empty), "Error in Test #4 with AddUnavailableProduct() using Outward date only.");
		
			// Test #5 CheckUnavailableProducts
			// We know product is unavailable
			Assert.AreEqual(true, helper.CheckUnavailableProducts("TEST1", "TEST", "TEST", outTravelDate, retTravelDate, "TEST1", string.Empty), "Error in Test #5 with CheckUnavailableProducts() using Outward and Return dates.");
			Assert.AreEqual(true, helper.CheckUnavailableProducts("TEST1", "TEST", "TEST", outTravelDate, null, "TEST1", string.Empty), "Error in Test #5 with CheckUnavailableProducts() using Outward date only.");

			// Test #6 CheckUnavailableProducts
			// Should not match any records - products not known to be unavailable
			Assert.AreEqual(false, helper.CheckUnavailableProducts("TEST1", "TEST", "TEST", outTravelDate, retTravelDate2, "TEST1", string.Empty), "Error in Test #6 with CheckUnavailableProducts() using Outward and Return date.");
			Assert.AreEqual(false, helper.CheckUnavailableProducts("TEST1", "TEST", "TEST", outTravelDate2, null, "TEST1", string.Empty), "Error in Test #6 with CheckUnavailableProducts() using Outward date only.");

			// Test #7 CheckUnavailableProducts with invalid input
			// Should be handled smoothly and return 0
			Assert.AreEqual(false, helper.CheckUnavailableProducts("TEST1", "TEST", "TEST", outTravelDate, retTravelDate, "XXXXX", string.Empty), "Error in Test #7 with CheckUnavailableProducts() using Outward and Return dates.");
			Assert.AreEqual(false, helper.CheckUnavailableProducts("TEST1", "TEST", "TEST", outTravelDate, null, "XXXXX", string.Empty), "Error in Test #7 with CheckUnavailableProducts() using Outward date only.");

		}

		/// <summary>
		/// Tests the AddAvailabilityHistory stored procedure
		/// </summary>
		[Test]
		public void TestAddAvailabilityHistory()
		{
			bool success;
			TDDateTime travelDate = new TDDateTime(2010, 01, 01, 12, 00, 00);
			AvailabilityEstimatorDBHelper helper = new AvailabilityEstimatorDBHelper();

			// Test #1 AddAvailabilityHistory 
			// Sucessful insertion of record
			success = helper.AddAvailabilityHistory("Rail", "TEST", "TEST", "TEST1", travelDate, true);
			Assert.AreEqual(true, success, "Error in Test #1 with AddAvailabilityHistory()");

		}

		#endregion 
	
	}
}
