// ***********************************************
// NAME 		: TestReplanItineraryManager.cs
// AUTHOR 		: Richard Hopkins
// DATE CREATED : 11/03/2006
// DESCRIPTION 	: NUnit test for ReplanItineraryManager class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestReplanItineraryManager.cs-arc  $
//
//   Rev 1.2   Dec 05 2012 13:57:26   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Oct 12 2009 09:11:10   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 12 2009 08:39:50   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Nov 08 2007 12:49:04   mturner
//Initial revision.
//
//   Rev 1.4   May 03 2006 16:59:08   kjosling
//Updated unit tests in response to changes in replanning. Replan journeys are no longer based on original journey request
//
//   Rev 1.5   May 03 2006 16:57:56   kjosling
//Rolled back road updates, which are in fact based on original journey request
//
//   Rev 1.4   May 03 2006 16:47:40   kjosling
//Updated unit tests since replanning is no longer done on original journey request.
//
//   Rev 1.3   Mar 29 2006 09:45:00   tmollart
//Updated unit tests to increase coverage.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Mar 14 2006 17:17:04   tmollart
//Post merge fixes. Stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Mar 13 2006 20:07:36   rhopkins
//Correction to unit tests
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Mar 13 2006 18:15:42   rhopkins
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// NUnit test for TDSessionManager class.
	/// </summary>
	/// <TestPlan>
	/// Test the stored procedures used to interact with ASPState..DeferredData table.
	/// </TestPlan>
	[TestFixture]
	[CLSCompliant(false)]
	public class TestReplanItineraryManager
	{
		private DateTime timeNow;
		private int journeyNum;
		private ITDJourneyRequest initialRequest;
		private JourneyResult cjpResult;
		private TDJourneyResult initialResult;
		private TDJourneyResult replanResult;

		private ReplanItineraryManager itineraryManager;
		private ITDSessionManager sessionManager;

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation(true) );

			sessionManager = TDSessionManager.Current;
			sessionManager.ItineraryMode = ItineraryManagerMode.Replan;

			itineraryManager = TDItineraryManager.Current as ReplanItineraryManager;

			timeNow = TDDateTime.Now.GetDateTime();
			journeyNum = 1;
		}

		[TearDown] 
		public void Dispose()
		{
		}

		#region Tests

		/// <summary>
		/// Tests that the initial Journey Result can be used to create an Itinerary
		/// Outward = public
		/// Return = none
		/// </summary>
		[Test]
		public void TestCreateItineraryPublicNoReturn()
		{
			CreateItinerary(true, true, 5, false, false, 0);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.Length, "Itinerary is of incorrect length.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"Returned JourneyResult does not match original JourneyResult");
		}

		/// <summary>
		/// Tests that the initial Journey Result can be used to create an Itinerary
		/// Outward = road
		/// Return = none
		/// </summary>
		[Test]
		public void TestCreateItineraryRoadNoReturn()
		{
			CreateItinerary(true, false, 5, false, false, 0);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.Length, "Itinerary is of incorrect length.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"Returned JourneyResult does not match original JourneyResult");
		}

		/// <summary>
		/// Tests that the initial Journey Result can be used to create an Itinerary
		/// Outward = public
		/// Return = public
		/// </summary>
		[Test]
		public void TestCreateItineraryPublicWithReturn()
		{
			CreateItinerary(false, true, 5, true, true, 5);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.Length, "Itinerary is of incorrect length.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"Returned JourneyResult does not match original JourneyResult");
		}

		/// <summary>
		/// Tests that the initial Journey Result can be used to create an Itinerary
		/// Outward = road
		/// Return = road
		/// </summary>
		[Test]
		public void TestCreateItineraryRoadWithReturn()
		{
			CreateItinerary(false, true, 5, true, false, 5);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.Length, "Itinerary is of incorrect length.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"Returned JourneyResult does not match original JourneyResult");
		}


		/// <summary>
		/// Tests that it is possible to replan an Outward Public Journey at the start
		/// Outward = public 5 legs
		/// Return = none
		/// </summary>
		[Test]
		public void TestReplanOutwardPublicStart()
		{
			int startIndex = 0;
			int endIndex = 2;

			CreateItinerary(true, true, 5, false, false, 0);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, true);

			JourneyControl.PublicJourney resultJourney = initialResult.OutwardPublicJourney(sessionManager.JourneyViewState.SelectedOutwardJourneyID);

			// Check that locations are correct
			Assert.AreEqual(resultJourney.JourneyLegs[startIndex].LegStart.Location.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(resultJourney.JourneyLegs[endIndex+1].LegStart.Location.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan destination location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(1, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.AreEqual(ModeType.Car, sessionManager.JourneyRequest.Modes[0], "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(1);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, true);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(2, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(0, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.OutwardJourneyItinerary[0].Type, "First outward journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.OutwardJourneyItinerary[1].Type, "Second outward journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan an Outward Public Journey in the middle
		/// Outward = public 5 legs
		/// Return = none
		/// </summary>
		[Test]
		public void TestReplanOutwardPublicMiddle()
		{
			int startIndex = 2;
			int endIndex = 3;

			CreateItinerary(true, true, 5, false, false, 0);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, true);

			JourneyControl.PublicJourney resultJourney = initialResult.OutwardPublicJourney(sessionManager.JourneyViewState.SelectedOutwardJourneyID);

			// Check that locations are correct
			Assert.AreEqual(resultJourney.JourneyLegs[startIndex-1].LegEnd.Location.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(resultJourney.JourneyLegs[endIndex+1].LegStart.Location.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan destination location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(1, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.AreEqual(ModeType.Car, sessionManager.JourneyRequest.Modes[0], "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(1);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, true);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(3, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(0, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.OutwardJourneyItinerary[0].Type, "First outward journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.OutwardJourneyItinerary[1].Type, "Second outward journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.OutwardJourneyItinerary[2].Type, "Third outward journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan an Outward Public Journey at the end
		/// Outward = public 5 legs
		/// Return = none
		/// </summary>
		[Test]
		public void TestReplanOutwardPublicEnd()
		{
			int startIndex = 3;
			int endIndex = 4;

			CreateItinerary(true, true, 5, false, false, 0);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, true);

			JourneyControl.PublicJourney resultJourney = initialResult.OutwardPublicJourney(sessionManager.JourneyViewState.SelectedOutwardJourneyID);

			// Check that locations are correct
			Assert.AreEqual(resultJourney.JourneyLegs[startIndex-1].LegEnd.Location.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(resultJourney.JourneyLegs[endIndex].LegEnd.Location.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan origin location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(1, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.AreEqual(ModeType.Car, sessionManager.JourneyRequest.Modes[0], "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(1);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, true);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(2, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(0, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.OutwardJourneyItinerary[0].Type, "First outward journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.OutwardJourneyItinerary[1].Type, "Second outward journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan an Outward Public Journey that only has one leg
		/// Outward = public 1 leg
		/// Return = none
		/// </summary>
		[Test]
		public void TestReplanOutwardPublicOneLeg()
		{
			int startIndex = 0;
			int endIndex = 0;

			CreateItinerary(true, true, 1, false, false, 0);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, true);

			JourneyControl.PublicJourney resultJourney = initialResult.OutwardPublicJourney(sessionManager.JourneyViewState.SelectedOutwardJourneyID);

			// Check that locations are correct
			Assert.AreEqual(resultJourney.JourneyLegs[startIndex].LegStart.Location.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(resultJourney.JourneyLegs[endIndex].LegEnd.Location.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan origin location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(1, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.AreEqual(ModeType.Car, sessionManager.JourneyRequest.Modes[0], "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(1);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, true);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(0, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");


			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.OutwardJourneyItinerary[0].Type, "First outward journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan an Outward Road Journey
		/// Outward = road
		/// Return = none
		/// </summary>
		[Test]
		public void TestReplanOutwardRoad()
		{
			int startIndex = 0;
			int endIndex = 0;

			CreateItinerary(true, false, 1, false, false, 0);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, true);

			// Check that locations are correct
			Assert.AreEqual(initialRequest.OriginLocation.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(initialRequest.DestinationLocation.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan origin location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(8, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.IsTrue( (Array.IndexOf(sessionManager.JourneyRequest.Modes, ModeType.Car) == -1), "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(3);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, true);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(0, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.OutwardJourneyItinerary[0].Type, "First outward journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan a Return Public Journey at the start
		/// Outward = none
		/// Return = public 5 legs
		/// </summary>
		[Test]
		public void TestReplanReturnPublicStart()
		{
			int startIndex = 0;
			int endIndex = 2;

			CreateItinerary(false, true, 5, true, true, 5);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, false);

			JourneyControl.PublicJourney resultJourney = initialResult.ReturnPublicJourney(sessionManager.JourneyViewState.SelectedReturnJourneyID);

			// Check that locations are correct (original request locations will be reversed)
			Assert.AreEqual(resultJourney.JourneyLegs[startIndex].LegStart.Location.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(resultJourney.JourneyLegs[endIndex+1].LegStart.Location.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan destination location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(1, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.AreEqual(ModeType.Car, sessionManager.JourneyRequest.Modes[0], "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(1);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, false);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(2, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.ReturnJourneyItinerary[0].Type, "First return journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.ReturnJourneyItinerary[1].Type, "Second return journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan an Outward Public Journey in the middle
		/// Outward = public 5 legs
		/// Return = none
		/// </summary>
		[Test]
		public void TestReplanReturnPublicMiddle()
		{
			int startIndex = 2;
			int endIndex = 3;

			CreateItinerary(false, true, 5, true, true, 5);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, false);

			JourneyControl.PublicJourney resultJourney = initialResult.ReturnPublicJourney(sessionManager.JourneyViewState.SelectedReturnJourneyID);

			// Check that locations are correct
			Assert.AreEqual(resultJourney.JourneyLegs[startIndex-1].LegEnd.Location.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(resultJourney.JourneyLegs[endIndex+1].LegStart.Location.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan destination location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(1, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.AreEqual(ModeType.Car, sessionManager.JourneyRequest.Modes[0], "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(1);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, false);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(3, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.ReturnJourneyItinerary[0].Type, "First return journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.ReturnJourneyItinerary[1].Type, "Second return journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.ReturnJourneyItinerary[2].Type, "Third return journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan an Outward Public Journey at the end
		/// Outward = public 5 legs
		/// Return = none
		/// </summary>
		[Test]
		public void TestReplanReturnPublicEnd()
		{
			int startIndex = 3;
			int endIndex = 4;

			CreateItinerary(false, true, 5, true, true, 5);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, false);

			JourneyControl.PublicJourney resultJourney = initialResult.ReturnPublicJourney(sessionManager.JourneyViewState.SelectedReturnJourneyID);

			// Check that locations are correct
			Assert.AreEqual(resultJourney.JourneyLegs[startIndex-1].LegEnd.Location.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(resultJourney.JourneyLegs[endIndex].LegEnd.Location.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan origin location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(1, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.AreEqual(ModeType.Car, sessionManager.JourneyRequest.Modes[0], "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(1);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, false);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(2, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.ReturnJourneyItinerary[0].Type, "First return journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.ReturnJourneyItinerary[1].Type, "Second return journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan a Return Public Journey that only has one leg
		/// Outward = none
		/// Return = public 1 leg
		/// </summary>
		[Test]
		public void TestReplanReturnPublicOneLeg()
		{
			int startIndex = 0;
			int endIndex = 0;

			CreateItinerary(false, true, 1, true, true, 1);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, false);

			JourneyControl.PublicJourney resultJourney = initialResult.ReturnPublicJourney(sessionManager.JourneyViewState.SelectedReturnJourneyID);

			// Check that locations are correct
			Assert.AreEqual(resultJourney.JourneyLegs[startIndex].LegStart.Location.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(resultJourney.JourneyLegs[endIndex].LegEnd.Location.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan origin location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(1, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.AreEqual(ModeType.Car, sessionManager.JourneyRequest.Modes[0], "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(1);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, false);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(1, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.ReturnJourneyItinerary[0].Type, "First return journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan a Return Road Journey
		/// Outward = none
		/// Return = road
		/// </summary>
		[Test]
		public void TestReplanReturnRoad()
		{
			int startIndex = 0;
			int endIndex = 0;

			CreateItinerary(false, true, 1, true, false, 1);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, false);

			// Check that locations are correct
			Assert.AreEqual(initialRequest.DestinationLocation.Description,
				sessionManager.JourneyRequest.OriginLocation.Description, "Replan origin location is incorrect.");
			Assert.AreEqual(initialRequest.OriginLocation.Description,
				sessionManager.JourneyRequest.DestinationLocation.Description, "Replan origin location is incorrect.");

			// Check that transport mode is correct
			Assert.AreEqual(8, sessionManager.JourneyRequest.Modes.Length, "Replan mode is of incorrect length.");
			Assert.IsTrue( (Array.IndexOf(sessionManager.JourneyRequest.Modes, ModeType.Car) == -1), "Replan mode is incorrect.");


			// Create Replan result
			DoReplan(3);

			// Add Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, false);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(1, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.ReturnJourneyItinerary[0].Type, "First return journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan the Outward then Return directions of a Journey
		/// Outward = public 5 legs
		/// Return = public 5 legs
		/// </summary>
		[Test]
		public void TestReplanOutwardThenReturn()
		{
			int startIndex = 2;
			int endIndex = 3;

			// Do replan for Outward
			CreateItinerary(true, true, 5, true, true, 5);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, true);

			// Create Outward Replan result
			DoReplan(1);

			// Add Outward Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, true);

			// Do replan for Return
			itineraryManager.CreateItinerary(sessionManager, false);
			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, false);

			// Create Return Replan result
			DoReplan(1);

			// Add Return Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, false);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(3, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(3, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Outward Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.OutwardJourneyItinerary[0].Type, "First outward journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.OutwardJourneyItinerary[1].Type, "Second outward journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.OutwardJourneyItinerary[2].Type, "Third outward journey is of incorrect JourneyType.");

			// Check Return Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.ReturnJourneyItinerary[0].Type, "First return journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.ReturnJourneyItinerary[1].Type, "Second return journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.ReturnJourneyItinerary[2].Type, "Third return journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that it is possible to replan the Return then Outward directions of a Journey
		/// Outward = public 5 legs
		/// Return = public 5 legs
		/// </summary>
		[Test]
		public void TestReplanReturnThenOutward()
		{
			int startIndex = 2;
			int endIndex = 3;

			// Do replan for Return
			CreateItinerary(false, true, 5, true, true, 5);

			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, false);

			// Create Return Replan result
			DoReplan(1);

			// Add Return Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, false);

			// Do replan for Outward
			itineraryManager.CreateItinerary(sessionManager, true);
			sessionManager.JourneyRequest = itineraryManager.BuildReplanJourneyRequest(((ReplanPageState)sessionManager.InputPageState).OriginalRequest,
				startIndex, endIndex, true);

			// Create Outward Replan result
			DoReplan(1);

			// Add Outward Replan result to Itinerary
			itineraryManager.InsertReplan(sessionManager.JourneyResult, startIndex, endIndex, true);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(3, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(3, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");

			// Check Outward Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.OutwardJourneyItinerary[0].Type, "First outward journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.OutwardJourneyItinerary[1].Type, "Second outward journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.OutwardJourneyItinerary[2].Type, "Third outward journey is of incorrect JourneyType.");

			// Check Return Itinerary segments are of correct journey type
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.ReturnJourneyItinerary[0].Type, "First return journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.RoadCongested, itineraryManager.ReturnJourneyItinerary[1].Type, "Second return journey is of incorrect JourneyType.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, itineraryManager.ReturnJourneyItinerary[2].Type, "Third return journey is of incorrect JourneyType.");
		}


		/// <summary>
		/// Tests that the Itinerary can be cleared down
		/// </summary>
		[Test]
		public void TestNewSearch()
		{
			sessionManager.InitialiseJourneyParameters(FindAMode.Train);

			// Create an initial journey in the Itinerary
			initialRequest = CreateJourneyRequest("InitialJourney", false);
			CreateItinerary(true, true, 5, false, false, 0);

			itineraryManager.NewSearch();

			// Check length of Itinerary after Clear
			Assert.AreEqual(0, itineraryManager.Length,
				"Itinerary is of incorrect length.");
			Assert.AreEqual(0, itineraryManager.OutwardLength,
				"Outward Itinerary is of incorrect length.");
			Assert.AreEqual(0, itineraryManager.ReturnLength,
				"Return Itinerary is of incorrect length.");
		}

		/// <summary>
		/// Tests that the Itinerary can be cleared down and the Working Results be reset to the Initial Journey Request/Results
		/// </summary>
		[Test]
		public void TestResetToInitialJourney()
		{
			// Create an initial journey in the Itinerary
			initialRequest = CreateJourneyRequest("InitialJourney", false);
			CreateItinerary(true, true, 5, false, false, 0);

			itineraryManager.ResetToInitialJourney();

			// Check length of Itinerary after Reset
			Assert.AreEqual(0, itineraryManager.Length, "Itinerary is of incorrect length.");

			// Check SessionManager is restored
			Assert.IsTrue(TDSessionManager.Current.JourneyRequest == initialRequest, "Request in working area does not match Original Request");
			Assert.IsTrue(TDSessionManager.Current.JourneyResult == initialResult, "Result in working area does not match Original Result");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestAddExtensionToItinerary()
		{
			try
			{
				itineraryManager.AddExtensionToItinerary();
				Assert.Fail("Exception should have been raised.");
			}

			catch (NotImplementedException)
			{
				// Do nothing - this is expected behaviour.
			}

		}


		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestFullItinerarySummary()
		{
			initialRequest = CreateJourneyRequest("InitialJourney", false);
			CreateItinerary(true, true, 5, true, true, 5);

			JourneySummaryLine[] test = itineraryManager.FullItinerarySummary();

			Assert.IsTrue(test.Length == 2, "Expected 2 Journey summary lines got " + test.Length.ToString());

		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestGenerateJourneySummaryLine()
		{
			initialRequest = CreateJourneyRequest("InitialJourney", false);
			CreateItinerary(true, true, 5, true, false, 5);

			ArrayList journeys = new ArrayList(itineraryManager.OutwardJourneyItinerary);
			JourneySummaryLine line = itineraryManager.GenerateJourneySummaryLine(journeys);

			Assert.IsNotNull(line.JourneyIndex, "journeyIndex is null.");
			Assert.IsNotNull(line.Type, "Journey type is null.");
			Assert.IsTrue(line.Modes.Length > 0, "Modes array is empty.");
			Assert.IsTrue(line.InterchangeCount > 0, "Interchange count is zero.");
			Assert.IsNotNull(line.DepartureDateTime, "Departure date time is null.");
			Assert.IsNotNull(line.ArrivalDateTime, "Arrival date time is null.");
			Assert.IsTrue(line.RoadMiles == 0, "Road miles is not zero."); //Outward joureny was public.
			Assert.IsNotNull(line.DisplayNumber, "Display number is null.");

			// Check return road journey has road miles
			journeys = new ArrayList(itineraryManager.ReturnJourneyItinerary);
			line = itineraryManager.GenerateJourneySummaryLine(journeys);

			Assert.IsTrue(line.RoadMiles > 0, "Road miles is zero.");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestOutwardDepartDateTime()
		{
			CreateItinerary(true, true, 5, true, false, 5);
			Assert.IsNotNull(itineraryManager.OutwardDepartDateTime(), "Outward depart date/time is null.");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestOutwardDepartLocation()
		{
			CreateItinerary(true, true, 5, true, false, 5);
			Assert.IsNotNull(itineraryManager.OutwardDepartLocation(), "Outward depart location is null.");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestReturnDepartDateTime()
		{
			CreateItinerary(true, true, 5, true, false, 5);
			Assert.IsNotNull(itineraryManager.ReturnDepartDateTime(), "Return depart date/time is null.");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestReturnDepartLocation()
		{
			CreateItinerary(true, true, 5, true, false, 5);
			Assert.IsNotNull(itineraryManager.ReturnDepartLocation(), "Return depart location is null.");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestGetOutwardJourney()
		{

			int journeyCount = 1;

			CreateItinerary(true, true, journeyCount , true, true, journeyCount);

			for (int i=0; i < journeyCount; i++)
			{
				Assert.IsNotNull(itineraryManager.GetOutwardJourney(i), "Outward journey " + i.ToString() + " is null.");
			}
		

		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestGetReturnJourney()
		{
			int journeyCount = 1;

			CreateItinerary(true, true, journeyCount , true, true, journeyCount);

			for (int i=0; i < journeyCount; i++)
			{
				Assert.IsNotNull(itineraryManager.GetReturnJourney(i), "Outward journey " + i.ToString() + " is null.");
			}
		}

		/// <summary>
		/// 
		/// </summary>
//		[Test]
//		public void TestSetGetItineraryPricing()
//		{
//			int legs = 2;
//
//			CreateItinerary(true, true, legs / 2 , true, true, legs / 2);
//
//			PricingRetailOptionsState[] pros = new PricingRetailOptionsState[legs];
//
//			for (int i = 0; i < legs; i ++)
//			{
//				pros[i] = new PricingRetailOptionsState();
//				pros[i].JourneyItinerary = new PricingRetail.Domain.Itinerary(new JourneyControl.PublicJourney(), new JourneyControl.PublicJourney());
//				pros[i].JourneyItinerary.FaresInitialised = true;
//				pros[i].ItinerarySegment = i + 1; // make it so its 1,2,3.. rather than 0,1,2,
//			}
//
//			itineraryManager.SetItineraryPricing(pros, true);
//
//
//			// Retrieve and test pricing information
//			PricingRetailOptionsState[] newPros = itineraryManager.GetItineraryPricing();
//
//			for (int i = 0; i < legs; i ++)
//			{
//				Assert.IsTrue(pros[i].ItinerarySegment == newPros[i].ItinerarySegment, "Pricing retail object " + i.ToString() + " does not match originally SET object.");
//			}
//		}



		#endregion


		#region Non-test methods

		private void CreateItinerary(bool replanOutward, bool outwardPublic, int outwardLegCount, bool returnRequired, bool returnPublic, int returnLegCount)
		{
			itineraryManager.NewSearch();
			sessionManager.JourneyViewState = new TDJourneyViewState();

			// Create an initial journey request
			initialRequest = CreateJourneyRequest("InitialJourney", returnRequired);

			TDDateTime outwardDateTime = (sessionManager.JourneyRequest.OutwardDateTime != null) ? sessionManager.JourneyRequest.OutwardDateTime[0] : null;
			TDDateTime returnDateTime = (sessionManager.JourneyRequest.ReturnDateTime != null) ? sessionManager.JourneyRequest.ReturnDateTime[0] : null;

			initialResult = new TDJourneyResult(1234, 0, outwardDateTime, returnDateTime, initialRequest.OutwardArriveBefore, initialRequest.ReturnArriveBefore, false);

			// Create an initial journey result - outward
			cjpResult = CreateCJPResult(false, outwardPublic, "InitialJourney", 2, outwardLegCount );
			initialResult.AddResult(cjpResult, true, null, null, initialRequest.OriginLocation, initialRequest.DestinationLocation, "ssss", false, -1);
			sessionManager.JourneyViewState.SelectedOutwardJourneyType = (outwardPublic) ? TDJourneyType.PublicOriginal : TDJourneyType.RoadCongested;

			if (returnRequired)
			{
				// Create an initial journey result - return
				cjpResult = CreateCJPResult(true, returnPublic, "InitialJourney", 2, returnLegCount );
				initialResult.AddResult(cjpResult, false, null, null, initialRequest.OriginLocation, initialRequest.DestinationLocation, "ssss", false, -1);
				sessionManager.JourneyViewState.SelectedReturnJourneyType = (returnPublic) ? TDJourneyType.PublicOriginal : TDJourneyType.RoadCongested;
			}

			sessionManager.JourneyResult = initialResult;

			itineraryManager.CreateItinerary(sessionManager, replanOutward);
		}

		private void DoReplan(int legCount)
		{
			TDDateTime dateTime = (sessionManager.JourneyRequest.OutwardDateTime != null) ? sessionManager.JourneyRequest.OutwardDateTime[0] : null;

			// Create Replan result
			replanResult = new TDJourneyResult(1234, 0, dateTime, null,
				sessionManager.JourneyRequest.OutwardArriveBefore, false, false);

			// Create an initial journey result - outward
			cjpResult = CreateCJPResult(false, (sessionManager.JourneyRequest.Modes[0] != ModeType.Car), "InitialJourney", 2, legCount );
			replanResult.AddResult(cjpResult, true, null, null, sessionManager.JourneyRequest.OriginLocation, sessionManager.JourneyRequest.DestinationLocation, "ssss", false, -1);

			sessionManager.JourneyResult = replanResult;
		}


		/// <summary>
		/// Creates a dummy Journey Request for an Initial Journey
		/// </summary>
		/// <param name="requestID">String used to enable Request to be identified in Assertions</param>
		/// <returns>Dummy Request</returns>
		public ITDJourneyRequest CreateJourneyRequest(string requestID, bool returnRequired)
		{
			ITDJourneyRequest request = new TDJourneyRequest();
			request.IsReturnRequired = returnRequired;
			TDSessionManager.Current.JourneyRequest = request;

			CompleteJourneyRequest(requestID);

			return request;
		}

		/// <summary>
		/// Populates the attributes in the Working Journey Request.
		/// If the Origin or Destination locations have already been populated then their values will not be overwritten.
		/// </summary>
		/// <remarks>References JourneyParameters to find existing origin, dest locations.</remarks>
		/// <param name="requestID">String used to enable Request to be identified in Assertions</param>
		private void CompleteJourneyRequest(string requestID)
		{
			TDSessionManager.Current.JourneyRequest.OutwardArriveBefore = false;
			TDSessionManager.Current.JourneyRequest.ReturnArriveBefore = true;

			TDSessionManager.Current.JourneyRequest.OutwardDateTime = new TDDateTime[1];
			TDSessionManager.Current.JourneyRequest.OutwardDateTime[0] = new TDDateTime( timeNow );

			TDSessionManager.Current.JourneyRequest.ReturnDateTime = new TDDateTime[1];
			TDSessionManager.Current.JourneyRequest.ReturnDateTime[0] = new TDDateTime( timeNow.AddMinutes(180) );
			

			TDSessionManager.Current.JourneyRequest.InterchangeSpeed = 1;
			TDSessionManager.Current.JourneyRequest.WalkingSpeed = 2;
			TDSessionManager.Current.JourneyRequest.MaxWalkingTime = 3;
			TDSessionManager.Current.JourneyRequest.DrivingSpeed = 4;
			TDSessionManager.Current.JourneyRequest.AvoidMotorways = false;
			TDSessionManager.Current.JourneyRequest.PublicViaLocations = new TDLocation[1];
			TDSessionManager.Current.JourneyRequest.PrivateViaLocation = new TDLocation();
			TDSessionManager.Current.JourneyRequest.AvoidRoads = new string[] {"A1", "A6"};
			TDSessionManager.Current.JourneyRequest.AlternateLocations = new TDLocation[2];
			TDSessionManager.Current.JourneyRequest.AlternateLocations[0] = new TDLocation();
			TDSessionManager.Current.JourneyRequest.AlternateLocations[1] = new TDLocation();
			TDSessionManager.Current.JourneyRequest.AlternateLocationsFrom = true;
			TDSessionManager.Current.JourneyRequest.PrivateAlgorithm = PrivateAlgorithmType.MostEconomical;
			TDSessionManager.Current.JourneyRequest.PublicAlgorithm = PublicAlgorithmType.Fastest;

			if (TDSessionManager.Current.JourneyRequest.OriginLocation == null)
			{
				if (TDSessionManager.Current.JourneyParameters.OriginLocation == null || TDSessionManager.Current.JourneyParameters.OriginLocation.Description.Length==0)
				{
					TDSessionManager.Current.JourneyRequest.OriginLocation = new TDLocation();
					TDSessionManager.Current.JourneyRequest.OriginLocation.Description = "Location A";
				}
				else
				{
					TDSessionManager.Current.JourneyRequest.OriginLocation = TDSessionManager.Current.JourneyParameters.OriginLocation;
				}
			}

			if (TDSessionManager.Current.JourneyRequest.DestinationLocation == null)
			{
				if (TDSessionManager.Current.JourneyParameters.DestinationLocation == null || TDSessionManager.Current.JourneyParameters.DestinationLocation.Description.Length==0)
				{
					TDSessionManager.Current.JourneyRequest.DestinationLocation = new TDLocation();
					TDSessionManager.Current.JourneyRequest.DestinationLocation.Description = "Location Z";
				}
				else
				{
					TDSessionManager.Current.JourneyRequest.DestinationLocation = TDSessionManager.Current.JourneyParameters.DestinationLocation;
				}
			}
		}


		/// <summary>
		/// Create a CJPResult containing either private or public journeys
		/// </summary>
		/// <param name="Public">bool Public - true for a Public journey, false for a private journey</param>
		/// <param name="requestID">String used to enable Request/Results to be identified in Assertions</param>
		/// <param name="journeyCount">Number of result options to be generated</param>
		/// <returns>JourneyResult</returns>
		private JourneyResult CreateCJPResult( bool doReturn, bool isPublic, string requestID, int journeyCount, int legCount )
		{
			JourneyResult result = new JourneyResult();

			if( isPublic )
			{
				result.publicJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[journeyCount];
				for (int i=0; i<journeyCount; i++, journeyNum++)
				{
					result.publicJourneys[i] = CreatePublicJourney(doReturn, requestID, legCount);
				}
			}
			else
			{
				result.privateJourneys = new PrivateJourney[1];

				result.privateJourneys[0] = CreatePrivateJourney(doReturn, requestID);

				journeyNum++;
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
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney CreatePublicJourney(bool doReturn, string requestID, int legCount)
		{
			TransportDirect.JourneyPlanning.CJPInterface.PublicJourney result = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();

			int returnMinutes;
			int locationNumber;
			int locationChange;

			if (doReturn)
			{
				locationNumber = legCount;
				locationChange = -1;
				returnMinutes = 180;
			}
			else
			{
				locationNumber = 0;
				locationChange = 1;
				returnMinutes = 0;
			}

			result.legs = new Leg[legCount];

			for (int i=0; i<legCount; i++)
			{
				result.legs[i] = new TimedLeg();

				result.legs[i].mode = ModeType.Tram;
				result.legs[i].validated = true;

				result.legs[i].board = new Event();
				result.legs[i].board.activity = ActivityType.Depart;
				result.legs[i].board.departTime = timeNow.AddMinutes(returnMinutes + (30 * journeyNum));
				result.legs[i].board.stop = new Stop();
				result.legs[i].board.stop.NaPTANID = (1000 + locationNumber).ToString();
				result.legs[i].board.stop.name = "Location " + locationNumber;

				result.legs[i].alight = new Event();
				result.legs[i].alight.activity = ActivityType.Arrive;
				result.legs[i].alight.arriveTime = timeNow.AddMinutes(returnMinutes + (20 + (30 * journeyNum)));
				result.legs[i].alight.stop = new Stop();
				result.legs[i].alight.stop.NaPTANID = (1000 + locationNumber + locationChange).ToString();
				result.legs[i].alight.stop.name = "Location " + (locationNumber + locationChange);

				result.legs[i].destination = new Event();
				result.legs[i].destination.activity = ActivityType.Arrive;
				result.legs[i].destination.arriveTime = timeNow.AddMinutes(returnMinutes + (45 + (30 * journeyNum)));
				result.legs[i].destination.stop = new Stop();
				result.legs[i].destination.stop.NaPTANID = (2000 + locationNumber).ToString();
				result.legs[i].destination.stop.name = "Destination " + locationNumber;

				locationNumber += locationChange;
			}

			return result;
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


	#endregion
}
