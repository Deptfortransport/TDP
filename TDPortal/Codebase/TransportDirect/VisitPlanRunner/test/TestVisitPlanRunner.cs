// *****************************************************
// NAME 		: TestVisitPlanRunner.cs
// AUTHOR 		: Tim Mollart
// DATE CREATED : 27/09/2005
// DESCRIPTION 	: NUnit test for VisitPlanRunner class.
// NOTES		: 
// *****************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/VisitPlanRunner/test/TestVisitPlanRunner.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:51:16   mturner
//Initial revision.
//
//   Rev 1.2   Nov 09 2005 18:57:16   RPhilpott
//Merge with stream2818

using System;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;


namespace TransportDirect.UserPortal.VisitPlanRunner
{
	/// <summary>
	/// Test classes to test Visit Plan Runner.
	/// </summary>
	[TestFixture]
	public class TestVisitPlanRunner
	{

		[SetUp]
		public void Init()
		{
			// Intialise the service cache. For this test use the MOCK VisitPlanRunenrCaller.
			TDServiceDiscovery.Init(new TestVisitPlanRunnerInitialisation());
		}							    


		[TearDown]
		public void Dispose()
		{
		}


		/// <summary>
		/// Tests a blank date being entered.
		/// </summary>
		[Test]
		public void TestDateBlank()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "";
			parameters.OutwardMonthYear = "";
			parameters.OutwardHour = "";
			parameters.OutwardMinute = "";

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);
			Assert.IsTrue((runner.ErrorMessages.Count > 0), "No error message returned");
		}


		/// <summary>
		/// Tests a date not being specified.
		/// </summary>
		[Test]
		public void TestDateNotSpecified()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "-";
			parameters.OutwardMonthYear = "-";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);
			Assert.IsTrue((runner.ErrorMessages.Count > 0), "No error message returned");
		}


		/// <summary>
		/// Tests what happens if an invalid day is entered.
		/// </summary>
		[Test]
		public void TestDateInvalidDay()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "99";
			parameters.OutwardMonthYear = "10/2010";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);
			Assert.IsTrue((runner.ErrorMessages.Count > 0), "No error message returned");		
		}

		
		/// <summary>
		/// Test an invalid month being entered.
		/// </summary>
		[Test]
		public void TestDateInvalidMonth()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "99/2005";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);
			Assert.IsTrue((runner.ErrorMessages.Count > 0), "No error message returned");
		}


		/// <summary>
		/// Tests an invalid year being specified.
		/// </summary>
		[Test]
		public void TestDateInvalidYear()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "10/ABCD";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);
			Assert.IsTrue((runner.ErrorMessages.Count > 0), "No error message returned");		
		}


		/// <summary>
		/// Tests an invalid hour being entered.
		/// </summary>
		[Test]
		public void TestDateInvalidHours()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "10/2010";
			parameters.OutwardHour = "99";
			parameters.OutwardMinute = "10";
		
			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);
			Assert.IsTrue((runner.ErrorMessages.Count > 0), "No error message returned");
		}


		/// <summary>
		/// Test invalid minutes being enetered.
		/// </summary>
		[Test]
		public void TestDateInvalidMinutes()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "10/2010";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "99";
		
			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);
			Assert.IsTrue((runner.ErrorMessages.Count > 0), "No error message returned");
		}


		/// <summary>
		/// Test what happens if a date in the past is entered.
		/// </summary>
		[Test]
		public void TestDateInPast()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "10/1999";
			parameters.OutwardMonthYear = "10";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);
			Assert.IsTrue((runner.ErrorMessages.Count > 0), "No error message returned");
		}


		/// <summary>
		/// Test what happens if a valid hour is entered but no
		/// minutes are then entered. The minutes should be populated
		/// with 00 automatically.
		/// </summary>
		[Test]
		public void TestDateValidHourNoMinutesEntered()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "10/2005";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "";
			
			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);

			// Check that the paremters object minutes has been updated to "00"
			Assert.IsTrue(parameters.OutwardMinute == "00", "Outward minutes set to: " + parameters.OutwardMinute);
		}


		/// <summary>
		/// Test a valid date being entered.
		/// </summary>
		[Test]
		public void TestDateValid()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "10/2010";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";
			
			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformDateValidations(parameters);
			Assert.IsTrue((runner.ErrorMessages.Count == 0), "Error message returned");
		}


		/// <summary>
		/// Test what happens when no modes are selected. Outcome is that mode count 
		/// should increase to contain all the modes.
		/// </summary>
		[Test]
		public void TestModeValidationNoModesSelected()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.PublicModes = new ModeType[]{};

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PeformModeValidations(parameters);

			Assert.IsTrue(parameters.PublicModes.GetLength(0) > 0, "No modes present on the parameters");
		}


		/// <summary>
		/// Test what happens when a few modes are selected. Outcome is that
		/// the mode count should not change.
		/// </summary>
		[Test]
		public void TestModeValidationSomeModesSelected()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.PublicModes = new ModeType[] { ModeType.Air, ModeType.Metro, ModeType.Underground};

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PeformModeValidations(parameters);

			Assert.IsTrue(parameters.PublicModes.GetLength(0) == 3, "Incorrect number of modes present.");
		}


		/// <summary>
		/// Test what happens when all modes are selected. The count of modes after
		/// calling validate should be the same as before. No changes should be made.
		/// </summary>
		[Test]
		public void TestModeValidationAllModesSelected()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.PublicModes = new ModeType[] { ModeType.Air, ModeType.Bus, ModeType.Coach, ModeType.Ferry, ModeType.Metro, ModeType.Rail, ModeType.Tram, ModeType.Underground};

			int originalLength = parameters.PublicModes.GetLength(0);

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PeformModeValidations(parameters);

			int currentLength = parameters.PublicModes.GetLength(0);

			//Should equal the amount specified before
			Assert.IsTrue(originalLength == currentLength, "Mode counts do not match.");
		}


		/// <summary>
		/// Tests that no error messages are reported then all
		/// locations are valid.
		/// </summary>
		[Test]
		public void TestLocationsAllValid()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();

			parameters.SetLocation(0, new TDLocation());
			parameters.SetLocation(1, new TDLocation());
			parameters.SetLocation(2, new TDLocation());

			parameters.GetLocation(0).Status = TDLocationStatus.Valid;
			parameters.GetLocation(1).Status = TDLocationStatus.Valid;
			parameters.GetLocation(2).Status = TDLocationStatus.Valid;

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformLocationValidations(parameters);

			Assert.IsTrue(runner.ErrorMessages.Count == 0, "Unexpected error messages were returned");
		}


		/// <summary>
		/// Test that error messages are reported when each location
		/// is ambiguous.
		/// </summary>
		[Test]
		public void TestLocationsAllAmbiguous()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			
			parameters.SetLocation(0, new TDLocation());
			parameters.SetLocation(1, new TDLocation());
			parameters.SetLocation(2, new TDLocation());
			
			parameters.GetLocation(0).Status = TDLocationStatus.Ambiguous;
			parameters.GetLocation(1).Status = TDLocationStatus.Ambiguous;
			parameters.GetLocation(2).Status = TDLocationStatus.Ambiguous;

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformLocationValidations(parameters);

			Assert.IsTrue(runner.ErrorMessages.Count == 3, "Unexpected error messages count was returned");
		}


		/// <summary>
		/// Test that error messages are reported when each location
		/// is invalid.
		/// </summary>
		[Test]
		public void TestLocationsAllUnspecified()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();

			parameters.SetLocation(0, new TDLocation());
			parameters.SetLocation(1, new TDLocation());
			parameters.SetLocation(2, new TDLocation());

			parameters.GetLocation(0).Status = TDLocationStatus.Unspecified;
			parameters.GetLocation(1).Status = TDLocationStatus.Unspecified;
			parameters.GetLocation(2).Status = TDLocationStatus.Unspecified;

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformLocationValidations(parameters);

			Assert.IsTrue(runner.ErrorMessages.Count == 3, "Unexpected error messages count was returned");
		}


		/// <summary>
		/// Test that error messages are reported one location is ambiguous
		/// and the rest are unspecified. This will cause a different code
		/// execution path but the same number of error messages.
		/// </summary>
		[Test]
		public void TestLocationsOneAmbiguousRestUnspecified()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();

			parameters.SetLocation(0, new TDLocation());
			parameters.SetLocation(1, new TDLocation());
			parameters.SetLocation(2, new TDLocation());

			parameters.GetLocation(0).Status = TDLocationStatus.Ambiguous;
			parameters.GetLocation(1).Status = TDLocationStatus.Unspecified;
			parameters.GetLocation(2).Status = TDLocationStatus.Unspecified;

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.PerformLocationValidations(parameters);

			Assert.IsTrue(runner.ErrorMessages.Count == 3, "Unexpected error messages count was returned");
		}


		/// <summary>
		/// Test that overlapping locations are detected.
		/// </summary>
		[Test]
		public void TestOverlappingLocations()
		{
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();

			TDLocation location1 = new TDLocation();
			TDLocation location2 = new TDLocation();
			TDLocation location3 = new TDLocation();

			TDNaptan[] naptans = new TDNaptan[3];
			naptans[0] = new TDNaptan("1000", null);
			naptans[1] = new TDNaptan("1000", null);
			naptans[2] = new TDNaptan("1000", null);

			location1.NaPTANs = naptans;
			location2.NaPTANs = naptans;
			location3.NaPTANs = naptans;

			location1.Status = TDLocationStatus.Valid;
			location2.Status = TDLocationStatus.Valid;
			location3.Status = TDLocationStatus.Valid;

			parameters.SetLocation(0, location1);
			parameters.SetLocation(1, location2);
			parameters.SetLocation(2, location3);

			parameters.GetLocation(0).Status = TDLocationStatus.Valid;
			parameters.GetLocation(1).Status = TDLocationStatus.Valid;
			parameters.GetLocation(2).Status = TDLocationStatus.Valid;

			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.CheckForLocationsOverlapping(parameters);

			Assert.IsTrue(runner.ErrorMessages.Count == 1, "Unexpected messages count was returned");

		}

		/// <summary>
		/// Tests the call to ValidateAndRunInintialIntinerary. Actually ends up planning
		/// a full itinerary (albeit) with the mock CJP as there is no way to just test
		/// the call without performing the full operation.
		/// </summary>
		[Test]
		public void TestValidateAndRunIntitialItinerary()
		{
			// Local Variables
			bool result;

			// Set up new parameters object
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();

			parameters.SetLocation(0, new TDLocation());
			parameters.SetLocation(1, new TDLocation());
			parameters.SetLocation(2, new TDLocation());

			parameters.GetLocation(0).Status = TDLocationStatus.Valid;
			parameters.GetLocation(1).Status = TDLocationStatus.Valid;
			parameters.GetLocation(2).Status = TDLocationStatus.Valid;

			parameters.PublicModes = new ModeType[] { ModeType.Air, ModeType.Bus, ModeType.Coach, ModeType.Ferry, ModeType.Metro, ModeType.Rail, ModeType.Tram, ModeType.Underground};

			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "10/2010";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";

			// Get mock session manager
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

			// Set parameters onto the session manager
			sessionManager.JourneyParameters = parameters;

			// Create a journey plan state data object. This would normally
			// be done by the calling page.
			JourneyPlanState jps = new JourneyPlanState();
			jps.RequestID = Guid.NewGuid();
			jps.Status = AsyncCallStatus.None;

			// Put the object onto the session manager
			sessionManager.AsyncCallState = jps;

			// Call runner - this will use a mock VisitPlanRunnerCaller as we dont
			// want to start using the CJP for this test.
			VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
			result = runner.ValidateAndRunInitialItinerary(sessionManager);

			Assert.IsTrue(result, "Call to ValidateAndRunInitialItinerary returned FALSE");
		}


		/// <summary>
		/// Tests the call to AddJourneys.
		/// </summary>
		[Test]
		public void TestAddJourneys()
		{
			try
			{
				// Get mock session manager
				ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			
				VisitPlanRunner.VisitPlannerRunner runner = new VisitPlannerRunner();
				runner.RunAddJourneys(sessionManager);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
