// ***********************************************
// NAME 		: TestExtendSegmentStore.cs
// AUTHOR 		: Paul Cross
// DATE CREATED : 16/09/2005
// DESCRIPTION 	: NUnit test for ExtendSegmentStore class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestExtendSegmentStore.cs-arc  $
//
//   Rev 1.2   Dec 05 2012 13:57:26   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Oct 12 2009 09:11:08   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 12 2009 08:39:50   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Nov 08 2007 12:49:00   mturner
//Initial revision.
//
//   Rev 1.6   Mar 22 2006 20:27:48   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 15 2006 14:19:22   rhopkins
//Corrections to unit tests
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 14 2006 14:40:58   rhopkins
//Merge stream3353.
//Also fix unit tests
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2.2.0   Mar 02 2006 17:45:32   NMoorhouse
//extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 10 2006 15:04:34   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.1   Dec 22 2005 10:13:44   tmollart
//General updates.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2.1.0   Dec 12 2005 17:09:54   tmollart
//Removed references to OldFindAMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Sep 27 2005 08:53:46   pcross
//Minor updates. Mainly associated with PublicJourney class update.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 21 2005 16:23:08   pcross
//Finished test class compatible with current code.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 19 2005 15:41:30   pcross
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;


namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// NUnit test for ExtendSegmentStore class.
	/// </summary>
	/// <TestPlan>
	/// </TestPlan>
	[TestFixture]
	[CLSCompliant(false)]
	public class TestExtendSegmentStore
	{
		ITDSessionManager sessionManager;
		private DateTime timeNow;
		private int journeyNum;

		public TestExtendSegmentStore()
		{
		}


		#region Setup / teardown

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation() );
			sessionManager = TDSessionManager.Current;

			sessionManager.JourneyViewState = new TDJourneyViewState();
			sessionManager.ItineraryMode = ItineraryManagerMode.ExtendJourney;
			sessionManager.ItineraryManager.ResetItinerary();
			timeNow = TDDateTime.Now.GetDateTime();
			journeyNum = 1;
		}

		[TearDown] 
		public void Dispose()
		{ 
		}

		#endregion

		#region Tests
		
		/// <summary>
		/// Test management of the segments (ie moving in and out of segment store)
		/// </summary>
		[Test]
		public void TestManageSegments()
		{
			// Set up a journey result in session manager

			// Create an initial journey request
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDDateTime outwardDateTime = (sessionManager.JourneyRequest.OutwardDateTime != null) ? sessionManager.JourneyRequest.OutwardDateTime[0] : TDDateTime.Now;
			TDDateTime returnDateTime = (sessionManager.JourneyRequest.ReturnDateTime != null) ? sessionManager.JourneyRequest.ReturnDateTime[0] : null;

			// Create an initial journey result
			JourneyResult cjpResult = CreateCJPResult(false, true, "InitialJourney", 2 );	// Contains 2 journeys
			TDJourneyResult initialResult = new TDJourneyResult(1234, 0, outwardDateTime, returnDateTime, initialRequest.OutwardArriveBefore, initialRequest.ReturnArriveBefore, false);

			initialResult.AddResult(cjpResult, true, null, null, initialRequest.OriginLocation, initialRequest.DestinationLocation, "ssss", false, -1);		// Added cjp result set as outward journeys with session ID "ssss"
			initialResult.JourneyReferenceNumber = 99;					// Add journey reference number so we can identify this journey result later on
			sessionManager.JourneyResult = initialResult;		// Assign to the session manager
			sessionManager.JourneyViewState.SelectedOutwardJourneyID = 1;  // Select non-default journey

			sessionManager.FindPageState = FindPageState.CreateInstance(FindAMode.Station);	


			// Test copying journey result from session to ExtendSegmentStore

			// Create a segment store
			ExtendSegmentStore extendSegmentStore = new ExtendSegmentStore();

			// Copy journey from session
			extendSegmentStore.CopyJourneyFromSession();

			// Test to check if segment store populated correctly
			Assert.AreEqual(99, extendSegmentStore.JourneyResult.JourneyReferenceNumber, "Journey result not copied from Session Manager to Segment Store as expected");
			Assert.IsTrue( (Array.IndexOf(extendSegmentStore.JourneyRequest.AvoidRoads, "A1") != -1), "JourneyRequest not copied from Session Manager to Segment Store as expected");
			Assert.AreEqual(1, extendSegmentStore.JourneyState.SelectedOutwardJourneyID, "SelectedOutwardJourneyID not copied from Session Manager to Segment Store as expected");
			Assert.AreEqual(FindAMode.Station, extendSegmentStore.FindPageState.Mode, "FindPageState not copied from Session Manager to Segment Store as expected");


			// Test copying journey result from segment store to session
			
			// Clear session
			sessionManager.JourneyResult = new TDJourneyResult();
			sessionManager.JourneyRequest = new TDJourneyRequest();
			sessionManager.JourneyViewState = new TDJourneyViewState();
			sessionManager.InitialiseJourneyParameters(FindAMode.Station);

			// Repopulate the session from the segment store
			extendSegmentStore.CopyJourneyToSession();

			// Test to check if segment store populated correctly
			Assert.AreEqual(99, sessionManager.JourneyResult.JourneyReferenceNumber, "Journey result not copied from Segment Store to Session Manager as expected");
			Assert.IsTrue( (Array.IndexOf(sessionManager.JourneyRequest.AvoidRoads, "A1") != -1), "JourneyRequest not copied from Segment Store to Session Manager as expected");
			Assert.AreEqual(1, sessionManager.JourneyViewState.SelectedOutwardJourneyID, "SelectedOutwardJourneyID not copied from Segment Store to Session Manager as expected");
			Assert.AreEqual(FindAMode.Station, sessionManager.FindPageState.Mode, "FindPageState not copied from Segment Store to Session Manager as expected");


			// Test to check that we can retrieve the selected outward public journey
			// (to do this we need to have the segment held within itinerary manager and the index
			// of the selected journey set in itinerary manager)

			// Set itinerary mode. Required now that we have different subclasses of itinerary manager so need to know which one to create
			sessionManager.ItineraryMode = ItineraryManagerMode.ExtendJourney;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			// Set up itinerary manager (Extend version)
			itineraryManager.CreateItinerary();		// Creates itinerary based on journey data in session

			JourneyControl.PublicJourney publicJourney;

			// Set the selected outward journey to be the first in the segment
			// This involves setting these 3 vars at once! Chris O to look at as this could be construed as a fault
			itineraryManager.SelectedOutwardJourneyIndex = 0;
			itineraryManager.SelectedOutwardJourneyID = ((JourneyControl.PublicJourney)itineraryManager.JourneyResult.OutwardPublicJourneys[0]).JourneyIndex;
			itineraryManager.SelectedOutwardJourneyType = ((JourneyControl.PublicJourney)itineraryManager.JourneyResult.OutwardPublicJourneys[0]).Type;

			// Get the selected public journey
			publicJourney = extendSegmentStore.OutwardPublicJourney();
			Assert.IsTrue( (publicJourney.JourneyLegs[0].LegStart.Location.Description.IndexOf("InitialJourney0") >= 0), "CreateItinerary has not copied the 1st journey in the itinerary as expected");

			// Do similar test for second journey
			itineraryManager.SelectedOutwardJourneyIndex = 1;
			itineraryManager.SelectedOutwardJourneyID = ((JourneyControl.PublicJourney)itineraryManager.JourneyResult.OutwardPublicJourneys[1]).JourneyIndex;
			itineraryManager.SelectedOutwardJourneyType = ((JourneyControl.PublicJourney)itineraryManager.JourneyResult.OutwardPublicJourneys[1]).Type;

			publicJourney = extendSegmentStore.OutwardPublicJourney();
			Assert.IsTrue( (publicJourney.JourneyLegs[0].LegStart.Location.Description.IndexOf("InitialJourney1") >= 0), "CreateItinerary has not copied the 2nd journey in the itinerary as expected");

		}
		

		/// <summary>
		/// Test returns from the queries provided by the segment store
		/// </summary>
		[Test]
		public void TestSegmentQueries()
		{
			// Set up a journey result in session manager

			// Create an initial journey request
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDDateTime outwardDateTime = (sessionManager.JourneyRequest.OutwardDateTime != null) ? sessionManager.JourneyRequest.OutwardDateTime[0] : TDDateTime.Now;
			TDDateTime returnDateTime = (sessionManager.JourneyRequest.ReturnDateTime != null) ? sessionManager.JourneyRequest.ReturnDateTime[0] : null;

			// Create an initial journey result, 
			JourneyResult cjpResult = CreateCJPResult(false, true, "InitialJourney", 2 );	// Contains 2 journeys
			TDJourneyResult initialResult = new TDJourneyResult(1234, 0, outwardDateTime, returnDateTime, initialRequest.OutwardArriveBefore, initialRequest.ReturnArriveBefore, false);
			initialResult.AddResult(cjpResult, true, null, null, initialRequest.OriginLocation, initialRequest.DestinationLocation, "ssss", false, -1);		// Added cjp result set as outward journeys with session ID "ssss"

			// Add a private journey
			cjpResult = CreateCJPResult(false, false, "PrivateJourney", 1 );	// Contains 1 journey
			initialResult.AddResult(cjpResult, true, null, null, initialRequest.OriginLocation, initialRequest.DestinationLocation, "ssss", false, -1);
			initialResult.JourneyReferenceNumber = 99;					// Add journey reference number so we can identify this journey result later on
			sessionManager.JourneyResult = initialResult;		// Assign to the session manager

			// Copy journey result from session to ExtendSegmentStore

			// Create a segment store
			ExtendSegmentStore extendSegmentStore = new ExtendSegmentStore();

			// Copy journey from session
			extendSegmentStore.CopyJourneyFromSession();

			// Get the selected public journey
			JourneyControl.PublicJourney publicJourney = extendSegmentStore.OutwardPublicJourney();
			Assert.IsTrue( (publicJourney.JourneyLegs[0].LegStart.Location.Description.IndexOf("InitialJourney0") >= 0), "CreateItinerary has not copied the 1st journey in the itinerary as expected");

			// See if the selected outward journey is public
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			Assert.IsTrue(extendSegmentStore.SelectedOutwardJourneyIsPublic, "Selected outward journey should be public");

			// Do similar test for the road journey
			itineraryManager.SelectedOutwardJourneyIndex = 2;
			itineraryManager.SelectedOutwardJourneyID = itineraryManager.JourneyResult.OutwardRoadJourney().JourneyIndex;
			itineraryManager.SelectedOutwardJourneyType = itineraryManager.JourneyResult.OutwardRoadJourney().Type;
			Assert.IsFalse(extendSegmentStore.SelectedOutwardJourneyIsPublic, "Selected outward journey should not be public");

			// Test Journey querying on the ExtendSegmentStore
			Assert.IsTrue( (extendSegmentStore.OutwardRoadJourney().Details[0].RoadNumber.IndexOf("PrivateJourney0") >= 0), "Road number not as expected");
		}

		/// <summary>
		/// Test returns from the queries provided by the segment store
		/// </summary>
		[Test]
		public void TestSegmentQueriesForReturnJourneys()
		{
			// Set up a journey result in session manager

			// Create an initial journey request
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDDateTime outwardDateTime = (sessionManager.JourneyRequest.OutwardDateTime != null) ? sessionManager.JourneyRequest.OutwardDateTime[0] : TDDateTime.Now;
			TDDateTime returnDateTime = (sessionManager.JourneyRequest.ReturnDateTime != null) ? sessionManager.JourneyRequest.ReturnDateTime[0] : null;

			// Create an initial journey result
			JourneyResult cjpResult = CreateCJPResult(true, true, "InitialJourney", 2 );	// Contains 2 journeys
			TDJourneyResult initialResult = new TDJourneyResult(1234, 0, outwardDateTime, returnDateTime, initialRequest.OutwardArriveBefore, initialRequest.ReturnArriveBefore, false);
			initialResult.AddResult(cjpResult, false, null, null, initialRequest.OriginLocation, initialRequest.DestinationLocation, "ssss", false, -1);		// Added cjp result set as return journeys with session ID "ssss"

			// Add a private journey
			cjpResult = CreateCJPResult(true, false, "PrivateJourney", 1 );	// Contains 1 journey
			initialResult.AddResult(cjpResult, false, null, null, initialRequest.OriginLocation, initialRequest.DestinationLocation, "ssss", false, -1);
			initialResult.JourneyReferenceNumber = 99;					// Add journey reference number so we can identify this journey result later on
			sessionManager.JourneyResult = initialResult;		// Assign to the session manager
		

			// Copy journey result from session to ExtendSegmentStore

			// Create a segment store
			ExtendSegmentStore extendSegmentStore = new ExtendSegmentStore();

			// Copy journey from session
			extendSegmentStore.CopyJourneyFromSession();

			// Get the selected public journey
			JourneyControl.PublicJourney publicJourney = extendSegmentStore.ReturnPublicJourney();
			Assert.IsTrue( (publicJourney.JourneyLegs[0].LegStart.Location.Description.IndexOf("InitialJourney0") >= 0), "CreateItinerary has not copied the 1st journey in the itinerary as expected");

			// See if the selected return journey is public
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			Assert.IsTrue(extendSegmentStore.SelectedReturnJourneyIsPublic, "Selected return journey should be public");

			// Do similar test for the road journey
			itineraryManager.SelectedReturnJourneyIndex = 2;
			itineraryManager.SelectedReturnJourneyID = ((JourneyControl.RoadJourney)itineraryManager.JourneyResult.ReturnRoadJourney()).JourneyIndex;
			itineraryManager.SelectedReturnJourneyType = ((JourneyControl.RoadJourney)itineraryManager.JourneyResult.ReturnRoadJourney()).Type;
			Assert.IsFalse(extendSegmentStore.SelectedReturnJourneyIsPublic, "Selected Return journey should not be public");


			// Test Journey querying on the ExtendSegmentStore
			Assert.IsTrue( (extendSegmentStore.ReturnRoadJourney().Details[0].RoadNumber.IndexOf("PrivateJourney0") >= 0), "Road number not as expected");

		}

		#endregion


		/// <summary>
		/// Create a CJPResult containing either private or public journeys
		/// </summary>
		/// <param name="Public">bool Public - true for a Public journey, false for a private journey</param>
		/// <param name="requestID">String used to enable Request/Results to be identified in Assertions</param>
		/// <param name="journeyCount">Number of result options to be generated</param>
		/// <returns>JourneyResult</returns>
		private JourneyResult CreateCJPResult( bool doReturn, bool Public, string requestID, int journeyCount )
		{
			JourneyResult result = new JourneyResult();

			if( Public )
			{
				result.publicJourneys = new JourneyPlanning.CJPInterface.PublicJourney[journeyCount];
				for (int i=0; i<journeyCount; i++, journeyNum++)
				{
					result.publicJourneys[i] = CreatePublicJourney(doReturn, requestID + i.ToString());
				}
			}
			else
			{
				result.privateJourneys = new PrivateJourney[journeyCount];
				for (int i=0; i<journeyCount; i++, journeyNum++)
				{
					result.privateJourneys[i] = CreatePrivateJourney(doReturn, requestID + i.ToString());
				}
			}
			return result;
		}

		/// <summary>
		/// Create a private journey.
		/// It will have a start, finish and drive section 
		/// </summary>
		/// <param name="requestID">String used to enable Request/Results to be identified in Assertions</param>
		/// <returns>PrivateJourney</returns>
		private PrivateJourney CreatePrivateJourney(bool doReturn, string requestID)
		{
			PrivateJourney result = new PrivateJourney();
			result.start = new StopoverSection();
			result.start = CreateStopoverSection( "Start", requestID);
			result.finish = new StopoverSection();
			result.finish = CreateStopoverSection( "Finish", requestID);
			result.sections = new Section[1];
			result.sections[0] = CreateDriveSection(doReturn, requestID, 25);
			result.congestion = true;

			return result;
		}

		/// <summary>
		/// Create a public journey
		/// It will have a board and alight leg
		/// </summary>
		/// <param name="requestID">String used to enable Request/Results to be identified in Assertions</param>
		/// <returns>PublicJourney</returns>
		private JourneyPlanning.CJPInterface.PublicJourney CreatePublicJourney(bool doReturn, string requestID)
		{
			JourneyPlanning.CJPInterface.PublicJourney result = new JourneyPlanning.CJPInterface.PublicJourney();

			int returnMinutes;
			string serviceStartPoint = String.Empty;
			string serviceEndPoint = String.Empty;
			string startPoint = String.Empty;
			string endPoint = String.Empty;

			if (doReturn)
			{
				returnMinutes = 180;
				startPoint = "MMMM";
				endPoint = "AAAA";
			}
			else
			{
				returnMinutes = 0;
				startPoint = "AAAA";
				endPoint = "MMMM";
			}

			result.legs = new Leg[1];

			result.legs[0] = new TimedLeg();
			result.legs[0].description = requestID;

			result.legs[0].mode = ModeType.Tram;
			result.legs[0].validated = true;

			result.legs[0].board = new Event();
			result.legs[0].board.activity = ActivityType.Depart;
			result.legs[0].board.departTime = timeNow.AddMinutes(returnMinutes + (30 * journeyNum));
			//			result.legs[0].board.pass = false;
			result.legs[0].board.stop = new Stop();
			result.legs[0].board.stop.NaPTANID = startPoint + " NAPTANID " + requestID + journeyNum;
			result.legs[0].board.stop.name = startPoint + " name " + requestID + journeyNum;

			result.legs[0].alight = new Event();
			result.legs[0].alight.activity = ActivityType.Arrive;
			result.legs[0].alight.arriveTime = timeNow.AddMinutes(returnMinutes + (20 + (30 * journeyNum)));
			//			result.legs[0].alight.pass = false;
			result.legs[0].alight.stop = new Stop();
			result.legs[0].alight.stop.NaPTANID = endPoint + " NAPTANID " + requestID + journeyNum;
			result.legs[0].alight.stop.name = endPoint + " name " + requestID + journeyNum;

			result.legs[0].destination = new Event();
			result.legs[0].destination.activity = ActivityType.Arrive;
			result.legs[0].destination.arriveTime = timeNow.AddMinutes(returnMinutes + (45 + (30 * journeyNum)));
			//			result.legs[0].destination.pass = false;
			result.legs[0].destination.stop = new Stop();
			result.legs[0].destination.stop.NaPTANID = "ZZZZ NAPTANID " + requestID + journeyNum;
			result.legs[0].destination.stop.name = "ZZZZ name " + requestID + journeyNum;			
			return result;
		}

		/// <summary>
		/// Creates a dummy Journey Request for an Initial Journey
		/// </summary>
		/// <param name="requestID">String used to enable Request to be identified in Assertions</param>
		/// <returns>Dummy Request</returns>
		public ITDJourneyRequest CreateJourneyRequest(string requestID)
		{
			ITDJourneyRequest request = new TDJourneyRequest();
			sessionManager.JourneyRequest = request;

			CompleteJourneyRequest(requestID);

			return request;
		}

		/// <summary>
		/// Populates the attributes in the Working Journey Request.
		/// If the Origin or Destination locations have already been populated then their values will not be overwritten.
		/// </summary>
		/// <param name="requestID">String used to enable Request to be identified in Assertions</param>
		private void CompleteJourneyRequest(string requestID)
		{
			timeNow = DateTime.Now;

			sessionManager.JourneyRequest.IsReturnRequired = true;
			sessionManager.JourneyRequest.OutwardArriveBefore = false;
			sessionManager.JourneyRequest.ReturnArriveBefore = true;

			sessionManager.JourneyRequest.OutwardDateTime = new TDDateTime[1];
			sessionManager.JourneyRequest.OutwardDateTime[0] = new TDDateTime( timeNow );

			sessionManager.JourneyRequest.ReturnDateTime = new TDDateTime[1];
			sessionManager.JourneyRequest.ReturnDateTime[0] = new TDDateTime( timeNow.AddMinutes(180) );
			

			sessionManager.JourneyRequest.InterchangeSpeed = 1;
			sessionManager.JourneyRequest.WalkingSpeed = 2;
			sessionManager.JourneyRequest.MaxWalkingTime = 3;
			sessionManager.JourneyRequest.DrivingSpeed = 4;
			sessionManager.JourneyRequest.AvoidMotorways = false;
			sessionManager.JourneyRequest.PublicViaLocations = new TDLocation[1];
			sessionManager.JourneyRequest.PrivateViaLocation = new TDLocation();
			sessionManager.JourneyRequest.AvoidRoads = new string[] {"A1", "A6"};
			sessionManager.JourneyRequest.AlternateLocations = new TDLocation[2];
			sessionManager.JourneyRequest.AlternateLocations[0] = new TDLocation();
			sessionManager.JourneyRequest.AlternateLocations[1] = new TDLocation();
			sessionManager.JourneyRequest.AlternateLocationsFrom = true;
			sessionManager.JourneyRequest.PrivateAlgorithm = PrivateAlgorithmType.MostEconomical;
			sessionManager.JourneyRequest.PublicAlgorithm = PublicAlgorithmType.Fastest;

			if (sessionManager.JourneyRequest.OriginLocation == null)
			{
				sessionManager.JourneyRequest.OriginLocation = new TDLocation();
				sessionManager.JourneyRequest.OriginLocation.Description = "AAAA name " + requestID + journeyNum;
			}

			if (sessionManager.JourneyRequest.DestinationLocation == null)
			{
				sessionManager.JourneyRequest.DestinationLocation = new TDLocation();
				sessionManager.JourneyRequest.DestinationLocation.Description = "MMMM name " + requestID + journeyNum;
			}
		}

		/// <summary>
		/// Creates a stop over section with a given name
		/// For use with the private journey
		/// </summary>
		/// <param name="name">string - the name of the section</param>
		/// <param name="requestID">String used to enable Request/Results to be identified in Assertions</param>
		/// <returns>StopoverSection</returns>
		private StopoverSection CreateStopoverSection( string name, string requestID)
		{
			StopoverSection stopoverSection = new StopoverSection();

			stopoverSection.name = name + " Section Name " + requestID + journeyNum;
			stopoverSection.node = new ITNNode();
			stopoverSection.node.TOID = name + " TOID " + requestID + journeyNum;

			return stopoverSection;
		}

		/// <summary>
		/// Create a drive section
		/// For use with the private journey
		/// </summary>
		/// <param name="requestID">String used to enable Request/Results to be identified in Assertions</param>
		/// <returns>DriveSection</returns>
		private DriveSection CreateDriveSection(bool doReturn, string requestID, int addMinutes)
		{
			string directionName = doReturn ? "Return " : "Outward ";

			DriveSection driveSection = new DriveSection();
			driveSection.time = new DateTime(1,1,1).AddMinutes(addMinutes);
			driveSection.name = directionName + "Drive Section Name " + requestID + journeyNum;
			driveSection.number = "Drive Section Number " + requestID + journeyNum;
			driveSection.distance = 123;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.MiniRoundaboutContinue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = true;
			driveSection.cost = 30;
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID " + requestID + journeyNum;
			driveSection.links[0].congestion = 1;
			return driveSection;
		}

	}
}

