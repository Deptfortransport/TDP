// *********************************************** 
// NAME			: TestAvailabilityEstimator.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Test implementation of the AvailabilityEstimator class
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/AvailabilityEstimator/TestAvailabilityEstimator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:14   mturner
//Initial revision.
//
//   Rev 1.5   Apr 28 2005 16:36:22   jbroome
//UnavailableProducts now stored by Outward and Return dates
//Resolution for 2302: PT - Product availability does not handle return products adequately.
//
//   Rev 1.4   Apr 23 2005 11:16:36   jbroome
//Updated after changes to AvailabilityResult class
//
//   Rev 1.3   Mar 18 2005 14:47:04   jbroome
//Added missing class documentation comments (Code Review)
//
//   Rev 1.2   Mar 09 2005 14:08:26   jbroome
//Replaced AvailabilityEstimate enum with existing Probability enum
//Resolution for 1923: DEV Code Review : Availability Estimator
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

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Test class which tests the creation and public methods
	/// of each implementation of IAvailabilityEstimator
	/// </summary>
	[TestFixture]
	public class TestAvailabilityEstimator
	{
		
		# region Constructor and Initialisation

		/// <summary>
		/// Constructor
		/// </summary>
		public TestAvailabilityEstimator()
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
		/// <returns>boolean value of success</returns>
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
		/// Test method the AvailabilityEstimatorFactory class
		/// </summary>
		[Test]
		public void TestAvailabilityEstimatorFactory()
		{
			AvailabilityEstimatorFactory factory = new AvailabilityEstimatorFactory();

			//Test creation of RailAvailabilityEstimator with enum input
			IAvailabilityEstimator railEstimator = factory.GetAvailabilityEstimator(TicketTravelMode.Rail);
			Assert.AreEqual(typeof(RailAvailabilityEstimator), railEstimator.GetType(), "Failed in creating RailAvailabilityEstimator");

			//Test creation of CoachAvailabilityEstimator with enum input
			IAvailabilityEstimator coachEstimator = factory.GetAvailabilityEstimator(TicketTravelMode.Coach);
			Assert.AreEqual(typeof(CoachAvailabilityEstimator), coachEstimator.GetType(), "Failed in creating CoachAvailabilityEstimator");

			//Test creation of AirAvailabilityEstimator with enum input
			IAvailabilityEstimator airEstimator = factory.GetAvailabilityEstimator(TicketTravelMode.Air);
			Assert.AreEqual(typeof(AirAvailabilityEstimator), airEstimator.GetType(), "Failed in creating AirAvailabilityEstimator");
		}

		/// <summary>
		/// Test method for the RailAvailabilityEstimator class
		/// </summary>
		[Test]
		public void TestRailAvailabilityEstimator()
		{
			
			#region Setup
			AvailabilityRequest request1, request2, request3, request4, request5, request6;
			AvailabilityResult result1, result2, result3, result4, result5, result6;
			AvailabilityEstimatorFactory factory = new AvailabilityEstimatorFactory();
			IAvailabilityEstimator railEstimator = factory.GetAvailabilityEstimator(TicketTravelMode.Rail);
			TDDateTime outTravelDatetime = new TDDateTime(2010, 01, 01, 12, 00, 00);
			TDDateTime retTravelDatetime = new TDDateTime(2010, 01, 01, 13, 00, 00);

			// Create array of AvailabilityResultServices 
			AvailabilityResultService[] services = new AvailabilityResultService[1];
			services[0] = new AvailabilityResultService("TEST1", "TEST1", outTravelDatetime, true);
			#endregion

			#region Test 1
			// ********** Test #1 ******************
			// Get estimate: Expected value = High
			// Update result: Valid input, outward only, available = false
			// Get estimate: Product now known to be unavailable, so expected value = Low
			// *************************************

			request1 = new AvailabilityRequest(TicketTravelMode.Rail, "TESTGROUP1", "TESTGROUP1", "TEST1", outTravelDatetime);
			Assert.AreEqual(Probability.High, railEstimator.GetAvailabilityEstimate(request1), "Error in Test #1 with GetAvailabilityEstimate()");

			result1 = new AvailabilityResult(TicketTravelMode.Rail, "TESTGROUP1", "TESTGROUP1", "TEST1", outTravelDatetime, null, false);	
			result1.AddJourneyServices(services);
			try
			{
				// Void method so can only test that it does not fall over. SQL errors handled by
				// AvailabilityEstimatorDBHelper. 
				railEstimator.UpdateAvailabilityEstimate(result1);
			}
			catch
			{
				Assert.Fail("Error in test #1 with UpdateAvailabilityEstimate()");
			}

			Assert.AreEqual(Probability.Low, railEstimator.GetAvailabilityEstimate(request1), "Error in test #1 with GetAvailabilityEstimate()");
			
			// ************ End test #1 ************
			#endregion

			#region Test 2
			// ********** Test #2 ******************
			// Get estimate: Expected value = Medium
			// Update result: Valid input, outward only available = true
			// Get estimate: Expected value = Medium
			// *************************************

			request2 = new AvailabilityRequest(TicketTravelMode.Rail, "TESTGROUP2", "TESTGROUP2", "TEST1", outTravelDatetime);
			Assert.AreEqual(Probability.Medium, railEstimator.GetAvailabilityEstimate(request2), "Error in Test #2 with GetAvailabilityEstimate()");

			result2 = new AvailabilityResult(TicketTravelMode.Rail, "TESTGROUP2", "TESTGROUP2", "TEST1", outTravelDatetime, null, true);	
			result2.AddJourneyServices(services);
			try
			{
				// Void method so can only test that it does not fall over. SQL errors handled by
				// AvailabilityEstimatorDBHelper. 
				railEstimator.UpdateAvailabilityEstimate(result2);
			}
			catch
			{
				Assert.Fail("Error in test #2 with UpdateAvailabilityEstimate()");
			}

			Assert.AreEqual(Probability.Medium, railEstimator.GetAvailabilityEstimate(request2), "Error in test #2 with GetAvailabilityEstimate()");

			// ************ End test #2 ************
			#endregion

			#region Test 3
			// ********** Test #3 ******************
			// Get estimate: Expected value = High
			// Update result: Valid input, outward and return, available = false
			// Get estimate: Product now known to be unavailable, so expected value = Low
			// *************************************

			request3 = new AvailabilityRequest(TicketTravelMode.Rail, "TESTGROUP3", "TESTGROUP3", "TEST1", outTravelDatetime, retTravelDatetime);
			Assert.AreEqual(Probability.High, railEstimator.GetAvailabilityEstimate(request3), "Error in Test #3 with GetAvailabilityEstimate()");

			result3 = new AvailabilityResult(TicketTravelMode.Rail, "TESTGROUP3", "TESTGROUP3", "TEST1", outTravelDatetime, retTravelDatetime, false);	
			result3.AddJourneyServices(services);

			try
			{
				// Void method so can only test that it does not fall over. SQL errors handled by
				// AvailabilityEstimatorDBHelper. 
				railEstimator.UpdateAvailabilityEstimate(result3);
			}
			catch
			{
				Assert.Fail("Error in test #3 with UpdateAvailabilityEstimate()");
			}

			Assert.AreEqual(Probability.Low, railEstimator.GetAvailabilityEstimate(request3), "Error in test #3 with GetAvailabilityEstimate()");

			// ************ End test #3 ************
			#endregion

			#region Test 4
			// ********** Test #4 ******************
			// Get estimate: Expected value = Medium
			// Update result: Valid input, outward and return , available = true
			// Get estimate: Expected value = Medium
			// *************************************

			request4 = new AvailabilityRequest(TicketTravelMode.Rail, "TESTGROUP4", "TESTGROUP4", "TEST1", outTravelDatetime, retTravelDatetime);
			Assert.AreEqual(Probability.Medium, railEstimator.GetAvailabilityEstimate(request4), "Error in Test #4 with GetAvailabilityEstimate()");

			result4 = new AvailabilityResult(TicketTravelMode.Rail, "TESTGROUP4", "TESTGROUP4", "TEST1", outTravelDatetime, retTravelDatetime, true);	
			result4.AddJourneyServices(services);
			try
			{
				// Void method so can only test that it does not fall over. SQL errors handled by
				// AvailabilityEstimatorDBHelper. 
				railEstimator.UpdateAvailabilityEstimate(result4);
			}
			catch
			{
				Assert.Fail("Error in test #4 with UpdateAvailabilityEstimate()");
			}

			Assert.AreEqual(Probability.Medium, railEstimator.GetAvailabilityEstimate(request4), "Error in test #4 with GetAvailabilityEstimate()");

			// ************ End test #4 ************
			#endregion

			#region Test 5
			// ********** Test 5# ******************
			// Get estimate: Expected value = Low
			// Update result: Invalid input, ticket code invalid - should not raise error
			// Get estimate: Expected value = Low
			// *************************************

			request5 = new AvailabilityRequest(TicketTravelMode.Rail, "TESTGROUP5", "TESTGROUP5", "TEST1", outTravelDatetime);
			Assert.AreEqual(Probability.Low, railEstimator.GetAvailabilityEstimate(request5), "Error in Test #5 with GetAvailabilityEstimate()");

			result5 = new AvailabilityResult(TicketTravelMode.Rail, "TESTGROUP5", "TESTGROUP5", "INVALID", outTravelDatetime, null, true);	
			result5.AddJourneyServices(services);
			try
			{
				// Void method so can only test that it does not fall over. SQL errors handled by
				// AvailabilityEstimatorDBHelper. 
				railEstimator.UpdateAvailabilityEstimate(result5);
			}
			catch
			{
				Assert.Fail("Error in test #5 with UpdateAvailabilityEstimate()");
			}

			Assert.AreEqual(Probability.Low, railEstimator.GetAvailabilityEstimate(request5), "Error in Test #5 with GetAvailabilityEstimate()");

			// ************ End test #5 ************
			#endregion

			#region Test 6
			// ********** Test 6# ******************
			// Get estimate: No profile found - Expected value = None
			// Update result: Invalid input, no services have been added to result
			// *************************************

			request6 = new AvailabilityRequest(TicketTravelMode.Coach, "TESTGROUP6", "TESTGROUP6", "TEST1", outTravelDatetime);
			Assert.AreEqual(Probability.None, railEstimator.GetAvailabilityEstimate(request6), "Error in test #6 with GetAvailabilityEstimate()");

			result6 = new AvailabilityResult(TicketTravelMode.Rail, "TESTGROUP6", "TESTGROUP6", "INVALID", outTravelDatetime, null, true);	
			try
			{
				// Void method so can only test that it does not fall over. SQL errors handled by
				// AvailabilityEstimatorDBHelper. 
				railEstimator.UpdateAvailabilityEstimate(result6);
			}
			catch
			{
				Assert.Fail("Error in test #6 with UpdateAvailabilityEstimate()");
			}

			// ************ End test #6 ************
			#endregion

		}

		/// <summary>
		/// Test method for the CoachAvailabilityEstimator class
		/// </summary>
		[Test]
		public void TestCoachAvailabilityEstimator()
		{
			AvailabilityRequest request;
			AvailabilityEstimatorFactory factory = new AvailabilityEstimatorFactory();
			IAvailabilityEstimator coachEstimator = factory.GetAvailabilityEstimator(TicketTravelMode.Coach);
		
			// Test #1 GetAvailabilityEstimate 
			// Always returns Probability.High - valid input
			request = new AvailabilityRequest(TicketTravelMode.Coach, "900076112", "900029044", "SOS", new TDDateTime());
			Assert.AreEqual(Probability.High, coachEstimator.GetAvailabilityEstimate(request), "Error in Test #1 with GetAvailabilityEstimate()");
		
			// Test #2 GetAvailabilityEstimate 
			// Always returns Probability.High - invalid input
			request = new AvailabilityRequest(TicketTravelMode.Coach, "", "", "", new TDDateTime());
			Assert.AreEqual(Probability.High, coachEstimator.GetAvailabilityEstimate(request), "Error in test # 2 with GetAvailabilityEstimate()");

			// Test #3 UpdateAvailabilityEstimate 
			// Throws exception
			AvailabilityResult result = new AvailabilityResult (TicketTravelMode.Coach, "TEST", "TEST", "SOS", new TDDateTime(), null, true);
			try 
			{
                coachEstimator.UpdateAvailabilityEstimate(result);
				Assert.Fail("Error in test # 3. Calling UpdateAvailabilityEstimate on CoachAvailabilityEstimator did not throw exception");
			}
			catch
			{
				// expected - do nothing
			}
		}
		
		/// <summary>
		/// Test method for the AirAvailabilityEstimator class
		/// </summary>
		[Test]
		public void TestAirAvailabilityEstimator()
		{
			AvailabilityRequest request;
			AvailabilityEstimatorFactory factory = new AvailabilityEstimatorFactory();
			IAvailabilityEstimator airEstimator = factory.GetAvailabilityEstimator(TicketTravelMode.Air);
		
			// Test #1 GetAvailabilityEstimate 
			// Always returns Probability.None - valid input
			request = new AvailabilityRequest(TicketTravelMode.Air, "TEST", "TEST", "TEST", new TDDateTime());
			Assert.AreEqual(Probability.None, airEstimator.GetAvailabilityEstimate(request), "Error in test #1 with GetAvailabilityEstimate()");
		
			// Test # 2 GetAvailabilityEstimate 
			// Always returns Probability.None - invalid input
			request = new AvailabilityRequest(TicketTravelMode.Air, "", "", "", new TDDateTime());
			Assert.AreEqual(Probability.None, airEstimator.GetAvailabilityEstimate(request), "Error in test #2 with GetAvailabilityEstimate()");

			// Test test #3 UpdateAvailabilityEstimate 
			// Throws exception
			AvailabilityResult result = new AvailabilityResult (TicketTravelMode.Air, "TEST", "TEST", "SOS", new TDDateTime(), null, true);
			try 
			{
				airEstimator.UpdateAvailabilityEstimate(result);
				Assert.Fail("Error in test #3. Calling UpdateAvailabilityEstimate on AirAvailabilityEstimator did not throw exception");
			}
			catch
			{
				// expected - do nothing
			}
		}
		#endregion
	
	}
}
