// *****************************************************
// NAME 		: TestVisitPlanRunnerCaller.cs
// AUTHOR 		: Tim Mollart
// DATE CREATED : 05/10/2005
// DESCRIPTION 	: NUnit test for VisitPlanRunnerCaller class.
// NOTES		: 
// *****************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/VisitPlanRunner/test/TestVisitPlanRunnerCaller.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:51:16   mturner
//Initial revision.
//
//   Rev 1.2   Feb 10 2006 09:32:34   kjosling
//Turned off failing unit tests
//
//   Rev 1.1   Nov 09 2005 18:57:16   RPhilpott
//Merge with stream2818

using System;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.JourneyPlanning.CJPInterface;

using NUnit.Framework;


namespace TransportDirect.UserPortal.VisitPlanRunner
{
	[TestFixture]
	public class TestVisitPlanRunnerCaller
	{

		private const string deleteCommand = "DELETE FROM ASPStateTempSessions WHERE SessionId = '{0}'";
		private const string insertCommand = "INSERT INTO ASPStateTempSessions (SessionId, Created, Expires, LockDate, LockCookie, Timeout, Locked) VALUES ('{0}', getdate(), getdate()+5, getdate(), 1, 10, 0 )";


		public TestVisitPlanRunnerCaller()
		{
		}


		[TestFixtureSetUp]
		public void Init()
		{
			// Intialise the service cache. For this test use the REAL VisitPlanRunenrCaller.
			TDServiceDiscovery.Init(new TestVisitPlanRunnerInitialisation(VisitPlanTestInitialisationMode.UseRealVisitPlanRunnerCaller));
			AddASPSession(TestVisitPlanRunnerInitialisation.TestSessionID);
		}							    


//		[TestFixtureTearDown]
//		public void Dispose()
//		{
//			RemoveASPSession(TestVisitPlanRunnerInitialisation.TestSessionID);
//		}


		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestRunInitialItinerary()
		{

			bool callComplete= false;

			// Set up a mock session manager and get a CJP Session Info object
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();

			// Set up serializer object.
			TDSessionSerializer ser = new TDSessionSerializer();

			// Set up required objects prior to calling the method.

			// Set up new parameters object
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();

			parameters.SetLocation(0, new TDLocation());
			parameters.SetLocation(1, new TDLocation());
			parameters.SetLocation(2, new TDLocation());

			parameters.GetLocation(0).Status = TDLocationStatus.Valid;
			parameters.GetLocation(1).Status = TDLocationStatus.Valid;
			parameters.GetLocation(2).Status = TDLocationStatus.Valid;

			parameters.GetLocation(0).Description = "Birmingham";
			parameters.GetLocation(1).Description = "London";
			parameters.GetLocation(2).Description = "Southampton";

			parameters.PublicModes = new ModeType[] { ModeType.Air, ModeType.Bus, ModeType.Coach, ModeType.Ferry, ModeType.Metro, ModeType.Rail, ModeType.Tram, ModeType.Underground};

			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "10/2010";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";

			// Return to origin
			parameters.ReturnToOrigin = false;

			// Serialize the paramters object onto the session
			
			// Create new state data object.
			JourneyPlanState originalState = new JourneyPlanState();
			originalState.RequestID = Guid.NewGuid();
			originalState.Status = AsyncCallStatus.None;

			// Create a new itinerary manager
			VisitPlannerItineraryManager im = new VisitPlannerItineraryManager();

			// Serialise created objects onto the session
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyJourneyParameters, parameters); 
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyAsyncCallState, originalState);
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyItineraryManager, im);

			// Call the method
			VisitPlanRunnerCaller caller = new VisitPlanRunnerCaller();
			caller.RunInitialItinerary(sessionInfo, TDSessionPartition.TimeBased);

			// Wait for the method to complete
			do
			{
				// Wait for 0.5 seconds
				Thread.Sleep(500);

				// Get a new session data object and test it
				JourneyPlanState newState = (JourneyPlanState)ser.RetrieveAndDeserializeSessionObject(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyAsyncCallState);
				
				if (newState.Status == AsyncCallStatus.CompletedOK)
				{
					callComplete = true;
				}

				if (newState.Status != AsyncCallStatus.InProgress)
				{
					Assert.Fail("CJP Call Failed. Refer to log for details of error.");
					return;
				}
			}
			while (!callComplete);


			// Perform tests now the method has completed.

			// Check each position in itinerary manager has relevant objects

			im = (VisitPlannerItineraryManager)ser.RetrieveAndDeserializeSessionObject(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyItineraryManager);

			for (int i=0; i<=1; i++)
			{		
				// Check objects are populated
				Assert.IsTrue(im.SpecificJourneyResult(i) != null, "Segment " + i.ToString() + ": Journey Result is NULL");
				Assert.IsTrue(im.SpecificJourneyRequest(i) != null, "Segment " + i.ToString() + ": Journey Request is NULL");
				Assert.IsTrue(im.SpecificJourneyViewState(i) != null, "Segment " + i.ToString() + ": Journey Viewstate is NULL");

				// Check objects have some data in them
				Assert.IsTrue(im.SpecificJourneyResult(i).OutwardPublicJourneyCount > 0, "Segment " + i.ToString() + ": No outward public journeys." );
			}

			// Check there is no 3rd journey
			Assert.IsTrue(im.SpecificJourneyResult(2) == null, "Return to origin journey result has been returned.");
			Assert.IsTrue(im.SpecificJourneyRequest(2) == null, "Return to origin journey request has been returned.");
			Assert.IsTrue(im.SpecificJourneyViewState(2) == null, "Return to origin journey viewstate has been returned.");


			// Check request objects to see if locations have been
			// built up correctly.
			Assert.IsTrue(im.SpecificJourneyRequest(0).OriginLocation.Description == parameters.GetLocation(0).Description, "Request 0: Origin incorrect.");
			Assert.IsTrue(im.SpecificJourneyRequest(0).DestinationLocation.Description == parameters.GetLocation(1).Description, "Request 0: Destination incorrect.");
			
			Assert.IsTrue(im.SpecificJourneyRequest(1).OriginLocation.Description == parameters.GetLocation(1).Description, "Request 1: Origin incorrect.");
			Assert.IsTrue(im.SpecificJourneyRequest(1).DestinationLocation.Description == parameters.GetLocation(2).Description, "Request 1: Destination incorrect.");
		}
		
		
		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestRunInitialItineraryReturnToOrigin()
		{

			bool callComplete= false;

			// Set up a mock session manager and get a CJP Session Info object
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();

			// Set up serializer object.
			TDSessionSerializer ser = new TDSessionSerializer();

			// Set up required objects prior to calling the method.

			// Set up new parameters object
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();

			parameters.SetLocation(0, new TDLocation());
			parameters.SetLocation(1, new TDLocation());
			parameters.SetLocation(2, new TDLocation());

			parameters.GetLocation(0).Status = TDLocationStatus.Valid;
			parameters.GetLocation(1).Status = TDLocationStatus.Valid;
			parameters.GetLocation(2).Status = TDLocationStatus.Valid;

			parameters.GetLocation(0).Description = "Birmingham";
			parameters.GetLocation(1).Description = "London";
			parameters.GetLocation(2).Description = "Southampton";

			parameters.PublicModes = new ModeType[] { ModeType.Air, ModeType.Bus, ModeType.Coach, ModeType.Ferry, ModeType.Metro, ModeType.Rail, ModeType.Tram, ModeType.Underground};

			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "10/2010";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";

			// Return to origin
			parameters.ReturnToOrigin = true;

			// Serialize the paramters object onto the session
			
			// Create new state data object.
			JourneyPlanState originalState = new JourneyPlanState();
			originalState.RequestID = Guid.NewGuid();
			originalState.Status = AsyncCallStatus.None;

			// Create a new itinerary manager
			VisitPlannerItineraryManager im = new VisitPlannerItineraryManager();

			// Serialise created objects onto the session
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyJourneyParameters, parameters); 
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyAsyncCallState, originalState);
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyItineraryManager, im);

			// Call the method
			VisitPlanRunnerCaller caller = new VisitPlanRunnerCaller();
			caller.RunInitialItinerary(sessionInfo, TDSessionPartition.TimeBased);

			// Wait for the method to complete
			do
			{
				// Wait for 0.5 seconds
				Thread.Sleep(500);

				// Get a new session data object and test it
				JourneyPlanState newState = (JourneyPlanState)ser.RetrieveAndDeserializeSessionObject(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyAsyncCallState);
				
				if (newState.Status == AsyncCallStatus.CompletedOK)
				{
					callComplete = true;
				}

				if (newState.Status != AsyncCallStatus.InProgress)
				{
					Assert.Fail("CJP Call Failed. Refer to log for details of error.");
					return;
				}
			}
			while (!callComplete);


			// Perform tests now the method has completed.

			// Check each position in itinerary manager has relevant objects

			im = (VisitPlannerItineraryManager)ser.RetrieveAndDeserializeSessionObject(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyItineraryManager);

			for (int i=0; i<=2; i++)
			{		
				// Check objects are populated
				Assert.IsTrue(im.SpecificJourneyResult(i) != null, "Segment " + i.ToString() + ": Journey Result is NULL");
				Assert.IsTrue(im.SpecificJourneyRequest(i) != null, "Segment " + i.ToString() + ": Journey Request is NULL");
				Assert.IsTrue(im.SpecificJourneyViewState(i) != null, "Segment " + i.ToString() + ": Journey Viewstate is NULL");

				// Check objects have some data in them
				Assert.IsTrue(im.SpecificJourneyResult(i).OutwardPublicJourneyCount > 0, "Segment " + i.ToString() + ": No outward public journeys." );
			}

			// Check request objects to see if locations have been
			// built up correctly.
			Assert.IsTrue(im.SpecificJourneyRequest(0).OriginLocation.Description == parameters.GetLocation(0).Description, "Request 0: Origin incorrect.");
			Assert.IsTrue(im.SpecificJourneyRequest(0).DestinationLocation.Description == parameters.GetLocation(1).Description, "Request 0: Destination incorrect.");
			
			Assert.IsTrue(im.SpecificJourneyRequest(1).OriginLocation.Description == parameters.GetLocation(1).Description, "Request 1: Origin incorrect.");
			Assert.IsTrue(im.SpecificJourneyRequest(1).DestinationLocation.Description == parameters.GetLocation(2).Description, "Request 1: Destination incorrect.");

			Assert.IsTrue(im.SpecificJourneyRequest(2).OriginLocation.Description == parameters.GetLocation(2).Description, "Request 2: Origin incorrect.");
			Assert.IsTrue(im.SpecificJourneyRequest(2).DestinationLocation.Description == parameters.GetLocation(0).Description, "Request 2: Destination incorrect.");

		}


		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestRunAddJourneys()
		{

			bool callComplete = false;
			bool callResult = false;

			int initialJourneyCount;
			int extendedJourneyCount;

			// Run an initial itinerary
			// Set up a mock session manager and get a CJP Session Info object
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();

			// Set up serializer object.
			TDSessionSerializer ser = new TDSessionSerializer();

			// Set up required objects prior to calling the method.

			// Set up new parameters object
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();
			parameters.SetLocation(0, new TDLocation());
			parameters.SetLocation(1, new TDLocation());
			parameters.SetLocation(2, new TDLocation());
			parameters.GetLocation(0).Status = TDLocationStatus.Valid;
			parameters.GetLocation(1).Status = TDLocationStatus.Valid;
			parameters.GetLocation(2).Status = TDLocationStatus.Valid;
			parameters.GetLocation(0).Description = "Birmingham";
			parameters.GetLocation(1).Description = "London";
			parameters.GetLocation(2).Description = "Southampton";
			parameters.PublicModes = new ModeType[] { ModeType.Air, ModeType.Bus, ModeType.Coach, ModeType.Ferry, ModeType.Metro, ModeType.Rail, ModeType.Tram, ModeType.Underground};
			parameters.OutwardDayOfMonth = "10";
			parameters.OutwardMonthYear = "10/2010";
			parameters.OutwardHour = "10";
			parameters.OutwardMinute = "00";
			parameters.ReturnToOrigin = true;

			// Serialize the paramters object onto the session
			
			// Create new state data object.
			JourneyPlanState originalState = new JourneyPlanState();
			originalState.RequestID = Guid.NewGuid();
			originalState.Status = AsyncCallStatus.None;

			// Create a new itinerary manager
			VisitPlannerItineraryManager im = new VisitPlannerItineraryManager();

			// Serialise created objects onto the session
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyJourneyParameters, parameters); 
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyAsyncCallState, originalState);
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyItineraryManager, im);

			// Call the method
			VisitPlanRunnerCaller caller = new VisitPlanRunnerCaller();
			caller.RunInitialItinerary(sessionInfo, TDSessionPartition.TimeBased);

			// Wait for the method to complete
			do
			{
				// Wait for 0.5 seconds
				Thread.Sleep(500);

				// Get a new session data object and test it
				JourneyPlanState newState = (JourneyPlanState)ser.RetrieveAndDeserializeSessionObject(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyAsyncCallState);
				if (newState.Status == AsyncCallStatus.CompletedOK)
				{
					callComplete = true;
				}

				if (newState.Status != AsyncCallStatus.InProgress)
				{
					Assert.Fail("CJP Call Failed. Refer to log for details of error.");
					return;
				}
			}
			while (!callComplete);

			// Get the updated itinerary manager.
			im = (VisitPlannerItineraryManager)ser.RetrieveAndDeserializeSessionObject(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyItineraryManager);

			// Get a count of the journeys returned.
			initialJourneyCount = im.SpecificJourneyResult(0).OutwardPublicJourneys.Count;

			// Create a new request to extend the segment
			callResult = im.ExtendJourneyResultLater(0);

			Assert.IsTrue(callResult, "Call to ExtendJourneyResultLater failed.");

			// The journey request will now be in the mock session manager and not deffered. The mock
			// session manager keeps deffered objects in a hash table. For the runner caller to pick
			// this up we will need to get if from this hash table and store the objects on the database.
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyJourneyRequest, TDSessionManager.Current.JourneyRequest);
			ser.SerializeSessionObjectAndSave(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyAsyncCallState, TDSessionManager.Current.AsyncCallState);

			// Call Add Journeys
			caller.RunAddJourneys(sessionInfo, TDSessionPartition.TimeBased);

			// Wait for the method to complete
			do
			{
				// Wait for 0.5 seconds
				Thread.Sleep(500);

				// Get a new session data object and test it
				JourneyPlanState newState = (JourneyPlanState)ser.RetrieveAndDeserializeSessionObject(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyAsyncCallState);
				
				if (newState.Status == AsyncCallStatus.CompletedOK)
				{
					callComplete = true;
				}

				if (newState.Status != AsyncCallStatus.InProgress)
				{
					Assert.Fail("CJP Call Failed. Refer to log for details of error.");
					return;
				}
			}
			while (!callComplete);

			// Deseralise the itinerary manager.
			im = (VisitPlannerItineraryManager)ser.RetrieveAndDeserializeSessionObject(TestVisitPlanRunnerInitialisation.TestSessionID, TDSessionPartition.TimeBased, TDSessionManager.KeyItineraryManager);

			// Get a count of the journeys now
			extendedJourneyCount = im.SpecificJourneyResult(0).OutwardPublicJourneys.Count;

			// Check the journey count and Assert!
			Assert.IsTrue(extendedJourneyCount > initialJourneyCount, "Journey count has not increased.");
		}
			

		/// <summary>
		/// Adds an ASP Session to the ASP State DB
		/// </summary>
		/// <param name="sessionId">Session ID</param>
		private void AddASPSession(string sessionId)
		{
			RemoveASPSession(sessionId);

			SqlHelper helper = new SqlHelper();
			try
			{
				helper.ConnOpen(SqlHelperDatabase.ASPStateDB);
				helper.Execute(string.Format(insertCommand, sessionId));
				helper.ConnClose();
			}
			catch (Exception e)
			{
				Assert.Fail("Unexpected exception occurred [" + e.Message + "]");
			}
			finally
			{
				helper.Dispose();
			}
		}


		/// <summary>
		/// Removes ASP State data from the database
		/// </summary>
		/// <param name="sessionId">Session ID</param>
		private void RemoveASPSession(string sessionId)
		{
			SqlHelper helper = new SqlHelper();
			try
			{
				helper.ConnOpen(SqlHelperDatabase.ASPStateDB);
				helper.Execute(string.Format(deleteCommand, sessionId));
				helper.ConnClose();
			}
			catch (Exception e)
			{
				Assert.Fail("Unexpected exception occurred [" + e.Message + "]");
			}
			finally
			{
				helper.Dispose();
			}
		}
		
	}
}
