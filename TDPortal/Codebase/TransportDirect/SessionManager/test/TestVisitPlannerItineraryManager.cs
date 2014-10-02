// ***********************************************
// NAME 		: TestVisitPlannerItineraryManager.cs
// AUTHOR 		: Paul Cross / Richard Hopkins (based on TestExtendItineraryManager class)
// DATE CREATED : 21/09/2005
// DESCRIPTION 	: NUnit test for VisitPlannerItineraryManager class.
// NOTES		: Adapted from TestExtendItineraryManager class.
//				  VisitPlanner is such that there are no return segments. There are either 2 or 3 'outward' segments ie:
//				  A - B - C (2 segments)
//				  A - B - C - A (3 segments - circular journey)
//				  As the data here is spoofed there is no natural link between objects ie the result object is not
//				  actually created from the request object therefore all similarities are hardcoded.
//				  Much of the testing uses a FillItinerary method that creates a 2 or 3 segment set of journeys in the
//				  itinerary manager. The basic journey set is:
//				  Request:
//					AAAA VisitPlanner1 - MMMM VisitPlanner1 - ZZZZ VisitPlanner1 - AAAA VisitPlanner1
//				  Result:
//					Same as request, when querying Origin.Location or Destination.Location.
//					However, querying LegStart and LegEnd retrieves location values 
//					AAAA name VisitPlannerJourney1 - MMMM name VisitPlannerJourney1
//					AAAA name FirstExtension3 - MMMM name FirstExtension3
//					AAAA name ReturnToOrigin5 - MMMM name ReturnToOrigin5   as these aren't updated to be related to the request
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestVisitPlannerItineraryManager.cs-arc  $
//
//   Rev 1.1   Oct 12 2009 09:11:10   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 12 2009 08:39:52   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Nov 08 2007 12:49:06   mturner
//Initial revision.
//
//   Rev 1.10   Mar 22 2006 20:27:48   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.9   Mar 14 2006 11:26:10   tmollart
//Manual merge of stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Feb 14 2006 16:12:18   tolomolaiye
//Fix unit test
//
//   Rev 1.7   Feb 10 2006 17:53:08   kjosling
//Fixed
//
//   Rev 1.6   Feb 10 2006 12:22:16   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.5   Nov 24 2005 16:33:26   tmollart
//Updated to reflect changes made in main class.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 29 2005 14:17:54   tmollart
//Code Review: Knock on effect meant update to a method name.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 14 2005 09:35:48   jbroome
//Updated tests to use SpecificJourneyRequest/Result as FullItinerarySegment now always returns true for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 11 2005 10:43:16   tmollart
//Changes so that AddJourneysToSegment method now takes a journey result object.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 27 2005 09:02:26   pcross
//Tests for VisitPlannerItineraryManager
//Resolution for 2638: DEL 8 Stream: Visit Planner
//

using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.PropertyService.Properties;

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
	public class TestVisitPlannerItineraryManager
	{
		private DateTime timeNow;
		private int journeyNum;

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation() );
			TDSessionManager.Current.ItineraryMode = ItineraryManagerMode.VisitPlanner;
			timeNow = TDDateTime.Now.GetDateTime();
			journeyNum = 1;
			TDItineraryManager.Current.ResetItinerary();
			TDItineraryManager.Current.JourneyViewState = new TDJourneyViewState();
		}

		[TearDown] 
		public void Dispose()
		{ 
		}

		#region Tests

		/// <summary>
		/// Tests that a Journey Result can be added to the end of the Itinerary
		/// </summary>
		[Test]
		public void TestAddToItinerary()
		{
			// Create a parameters object to represent data specified by user
			TDJourneyParametersVisitPlan visitPlannerParameters = CreateJourneyParameters("VisitPlannerJourney", true);

			// Create a request object
			ITDJourneyRequest visitPlannerRequest = CreateJourneyRequest("VisitPlannerJourney", null);

			// Test that the request has the info expected
			Assert.AreEqual("AAAA name VisitPlannerJourney1", visitPlannerRequest.OriginLocation.Description, "Origin location incorrect in the journey request");
			Assert.AreEqual("MMMM name VisitPlannerJourney1", visitPlannerRequest.DestinationLocation.Description, "Destination location incorrect in the journey request");
			Assert.AreEqual(timeNow.AddDays(1).ToString("dd/MM/yyyy hh:mm"), visitPlannerRequest.OutwardDateTime[0].GetDateTime().ToString("dd/MM/yyyy hh:mm"), "Outward Date incorrect in the journey request");
			Assert.AreEqual(2, visitPlannerRequest.WalkingSpeed, "Walking speed incorrect in the 1st journey request");

			// Create a results object
			TDJourneyResult visitPlannerResult = CreateJourneyResult("VisitPlannerJourney", false, true, 2, visitPlannerRequest);
			
			// Create a journey state object
			TDJourneyState visitPlannerJourneyState = new TDJourneyState();
			
			// Add journey result into itinerary
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest, visitPlannerResult, visitPlannerJourneyState);


			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, TDItineraryManager.Current.Length, "Itinerary is of incorrect length.");

			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.IsTrue(TDItineraryManager.Current.SpecificJourneyRequest(0) == visitPlannerRequest, "First Itinerary Segment Request does not match original Request.");
			Assert.IsTrue(TDItineraryManager.Current.SpecificJourneyResult(0) == visitPlannerResult, "First Itinerary Segment Result does not match Original Result");


			// Add a second result into the itinerary

			// Create a request object
			ITDJourneyRequest visitPlannerRequest2 = CreateJourneyRequest("FirstExtension", visitPlannerRequest);

			// Create a results object
			ITDJourneyResult visitPlannerResult2 = CreateJourneyResult("FirstExtension", false, true, 2, visitPlannerRequest2);
			
			// Add journey result into itinerary
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest2, visitPlannerResult2, visitPlannerJourneyState);


			// Check length of Itinerary and values of all elements
			Assert.AreEqual(2, TDItineraryManager.Current.Length, "Itinerary is of incorrect length.");

			Assert.AreEqual(visitPlannerRequest.DestinationLocation.Description, visitPlannerRequest2.OriginLocation.Description, "Origin of Second Itinerary Segment Request does not match Destination of Original Request");
			Assert.IsTrue(TDItineraryManager.Current.SpecificJourneyResult(1) == visitPlannerResult2, "Second Itinerary Segment does not match First Extension");
			
		}

		/// <summary>
		/// Tests ability to build up a journey request from collected parameters
		/// </summary>
		[Test]
		public void TestBuildJourneyRequest()
		{
			// Create a parameters object to represent data specified by user
			TDJourneyParametersVisitPlan visitPlannerParameters = CreateJourneyParameters("VisitPlannerJourney", true);

			// Test BuildNextRequest
			ITDJourneyRequest visitPlannerRequest = ((VisitPlannerItineraryManager)TDItineraryManager.Current).BuildNextRequest(visitPlannerParameters);

			// Test that the request has the info expected
			Assert.AreEqual("AAAA name VisitPlannerJourney1", visitPlannerRequest.OriginLocation.Description, "Origin location incorrect in the 1st journey request");
			Assert.AreEqual("MMMM name VisitPlannerJourney1", visitPlannerRequest.DestinationLocation.Description, "Destination location incorrect in the 1st journey request");
			Assert.AreEqual(timeNow.AddDays(1).ToString("dd/MM/yyyy hh:mm"), visitPlannerRequest.OutwardDateTime[0].GetDateTime().ToString("dd/MM/yyyy hh:mm"), "Outward Date incorrect in the 1st journey request");
			Assert.AreEqual(ModeType.Rail, visitPlannerRequest.Modes[0], "Mode type incorrect in the 1st journey request");
			Assert.AreEqual(2, visitPlannerRequest.WalkingSpeed, "Walking speed incorrect in the 1st journey request");


			// At this point the request would be sent to CJP and results returned (stored in segment store)
			// Need to simulate this, as to work out next segment journey start times we need to know results of prev segment
			
			// Create a journey result
			TDJourneyResult visitPlannerResult = CreateJourneyResult("VisitPlannerJourney", false, true, 2, visitPlannerRequest);
			TDJourneyState visitPlannerJourneyState = new TDJourneyState();
			
			// Add journey result into itinerary
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest, visitPlannerResult, visitPlannerJourneyState);

			// Build the request for the second segment
			ITDJourneyRequest visitPlannerRequest2 = ((VisitPlannerItineraryManager)TDItineraryManager.Current).BuildNextRequest(visitPlannerParameters);

			// Test that the request has the info expected
			Assert.AreEqual("MMMM name VisitPlannerJourney1", visitPlannerRequest2.OriginLocation.Description, "Origin location incorrect in the 2nd journey request");
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", visitPlannerRequest2.DestinationLocation.Description, "Destination location incorrect in the 2nd journey request");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(110).ToString("dd/MM/yyyy hh:mm"), visitPlannerRequest2.OutwardDateTime[0].GetDateTime().ToString("dd/MM/yyyy hh:mm"), "Outward Time incorrect in the 2nd journey request");
			Assert.AreEqual(ModeType.Rail, visitPlannerRequest2.Modes[0], "Mode type incorrect in the 2nd journey request");
			Assert.AreEqual(2, visitPlannerRequest2.WalkingSpeed, "Walking speed incorrect in the 2nd journey request");


			// At this point the request would be sent to CJP and results returned (stored in segment store)
			// Need to simulate this, as to work out next segment journey start times we need to know results of prev segment
			
			// Create a journey result
			ITDJourneyResult visitPlannerResult2 = CreateJourneyResult("FirstExtension", false, true, 2, visitPlannerRequest2);
			
			// Add journey result into itinerary
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest2, visitPlannerResult2, visitPlannerJourneyState);

			// Build the next request (this is a circular journey so we have a request to return to original location)
			ITDJourneyRequest visitPlannerRequest3 = ((VisitPlannerItineraryManager)TDItineraryManager.Current).BuildNextRequest(visitPlannerParameters);

			// Test that the request has the info expected
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", visitPlannerRequest3.OriginLocation.Description, "Origin location incorrect in the ReturnToOrigin journey request");
			Assert.AreEqual("AAAA name VisitPlannerJourney1", visitPlannerRequest3.DestinationLocation.Description, "Destination location incorrect in the ReturnToOrigin journey request");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(230).ToString("dd/MM/yyyy hh:mm"), visitPlannerRequest3.OutwardDateTime[0].GetDateTime().ToString("dd/MM/yyyy hh:mm"), "Outward Time incorrect in the ReturnToOrigin journey request");
			Assert.AreEqual(ModeType.Rail, visitPlannerRequest3.Modes[0], "Mode type incorrect in the ReturnToOrigin journey request");
			Assert.AreEqual(2, visitPlannerRequest3.WalkingSpeed, "Walking speed incorrect in the ReturnToOrigin journey request");

		}

		[Test]
		public void TestItineraryResults()
		{

			// Create a parameters object to represent data specified by user
			TDJourneyParametersVisitPlan visitPlannerParameters = CreateJourneyParameters("VisitPlannerJourney", true);

			// Build request
			ITDJourneyRequest visitPlannerRequest = ((VisitPlannerItineraryManager)TDItineraryManager.Current).BuildNextRequest(visitPlannerParameters);
			
			// Create a journey result
			TDJourneyResult visitPlannerResult = CreateJourneyResult("VisitPlannerJourney", false, true, 2, visitPlannerRequest);
			TDJourneyState visitPlannerJourneyState = new TDJourneyState();
			
			// Add journey result into itinerary
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest, visitPlannerResult, visitPlannerJourneyState);

			// Build the request for the second segment
			ITDJourneyRequest visitPlannerRequest2 = ((VisitPlannerItineraryManager)TDItineraryManager.Current).BuildNextRequest(visitPlannerParameters);

			// At this point the request would be sent to CJP and results returned (stored in segment store)
			// Need to simulate this, as to work out next segment journey start times we need to know results of prev segment
			
			// Create a journey result
			ITDJourneyResult visitPlannerResult2 = CreateJourneyResult("FirstExtension", false, true, 2, visitPlannerRequest2);
			
			// Add journey result into itinerary
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest2, visitPlannerResult2, visitPlannerJourneyState);


			// Build the next request (this is a circular journey so we have a request to return to original location)
			ITDJourneyRequest visitPlannerRequest3 = ((VisitPlannerItineraryManager)TDItineraryManager.Current).BuildNextRequest(visitPlannerParameters);
		
			// Create a journey result
			ITDJourneyResult visitPlannerResult3 = CreateJourneyResult("ReturnToOrigin", false, true, 2, visitPlannerRequest3);
			
			// Add journey result into itinerary
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest3, visitPlannerResult3, visitPlannerJourneyState);


			// Test there are maximum number of segments (3)
			Assert.AreEqual(3, TDItineraryManager.Current.Length, "3 segments expected for a full circular itinerary");

			// Check results
			Assert.IsTrue(TDItineraryManager.Current.SpecificJourneyRequest(0) == visitPlannerRequest,
				"First Itinerary Segment Request does not match Original Request.");
			Assert.IsTrue(TDItineraryManager.Current.SpecificJourneyResult(0) == visitPlannerResult,
				"First Itinerary Segment Result does not match Original Result");

			TDLocation _prevLocation;
			_prevLocation = TDItineraryManager.Current.SpecificJourneyRequest(0).DestinationLocation;
			Assert.AreEqual(_prevLocation.Description, TDItineraryManager.Current.SpecificJourneyRequest(1).OriginLocation.Description,
				"Origin of First Extension Request does not match Destination of Original Request");
			Assert.IsTrue(TDItineraryManager.Current.SpecificJourneyResult(1) == visitPlannerResult2,
				"Second Itinerary Segment does not match First Extension");

			_prevLocation = TDItineraryManager.Current.SpecificJourneyRequest(1).DestinationLocation;
			Assert.AreEqual(_prevLocation.Description, TDItineraryManager.Current.SpecificJourneyRequest(2).OriginLocation.Description,
				"Origin of Second Extension Request does not match Destination of First Extension Request");
			Assert.IsTrue(TDItineraryManager.Current.SpecificJourneyResult(2) == visitPlannerResult3,
				"Return Itinerary Segment does not match Second Extension");

		}

		/// <summary>
		/// Tests that the Itinerary can be cleared down
		/// </summary>
		[Test]
		public void TestNewSearch()
		{
			// Create a full itinerary
			FillItinerary(true, false, true, 2);

			// Check length of Itinerary before Clear
			Assert.AreEqual(3, TDItineraryManager.Current.Length,
				"Itinerary is of incorrect length.");

			// Check itinerary properties before clear
			string testPropertyValue = String.Empty;


			testPropertyValue = ((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourney(0)).Details[0].Destination.Location.Description;
			Assert.IsTrue(testPropertyValue.IndexOf("VisitPlannerJourney") >=0, "First journey different to expected");

			testPropertyValue = ((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(1).OutwardPublicJourney(0)).Details[0].Origin.Location.Description;
			Assert.IsTrue(testPropertyValue.IndexOf("VisitPlannerJourney") >=0, "Second journey different to expected");

			testPropertyValue = ((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(2).OutwardPublicJourney(0)).Details[0].Origin.Location.Description;
			Assert.IsTrue(testPropertyValue.IndexOf("VisitPlannerJourney") >=0, "Third journey different to expected");

			
			TDItineraryManager.Current.ResetItinerary();

			// Check length of Itinerary after Clear
			Assert.AreEqual(0, TDItineraryManager.Current.Length,
				"Itinerary is of incorrect length.");

			Assert.IsTrue(TDItineraryManager.Current.ExtendInProgress == false,
				"ItineraryManager is still in 'ExtendInProgress' mode.");

			// Check length of Itinerary after Clear
			Assert.AreEqual(0, TDItineraryManager.Current.Length,
				"Itinerary is of incorrect length.");

			Assert.IsTrue(TDItineraryManager.Current.ExtendInProgress == false,
				"ItineraryManager is still in 'ExtendInProgress' mode.");


			// To test if empty, try reading a value (like we did above) - should error
			try
			{
				testPropertyValue = ((JourneyControl.PublicJourney)TDItineraryManager.Current.JourneyResult.OutwardPublicJourney(0)).Details[0].Destination.Location.Description;
				// No error - value still available - therefore array not emptied
				Assert.Fail("Itinerary manager not reset");
			}
			catch
			{
				// Error as expected
			}

		}


		/// <summary>
		/// Tests that the Origin and Destination details for the Outward Itinerary can be retrieved
		/// </summary>
		[Test]
		public void TestGetOutwardItineraryOriginAndDestination()
		{

			// Create a full itinerary
			FillItinerary(true, false, true, 2);

			TDLocation _origin = TDItineraryManager.Current.OutwardOriginLocation();
			TDLocation _destination = TDItineraryManager.Current.OutwardDestinationLocation();

			Assert.AreEqual("AAAA name VisitPlannerJourney1", _origin.Description,
				"Origin location is not correct (circular journey).");
			Assert.AreEqual("AAAA name VisitPlannerJourney1", _destination.Description,
				"Destination location is not correct (circular journey).");


			// Repeat the test for a non-circular journey

			// Create a non-return itinerary
			VisitPlannerItineraryManager.Current.ResetItinerary();
			journeyNum = 1 ;
			FillItinerary(false, false, true, 2);

			TDLocation _origin2 = TDItineraryManager.Current.OutwardOriginLocation();
			TDLocation _destination2 = TDItineraryManager.Current.OutwardDestinationLocation();

			Assert.AreEqual("AAAA name VisitPlannerJourney1", _origin2.Description,
				"Origin location is not correct (non-return journey).");
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", _destination2.Description,
				"Destination location is not correct (non-return journey).");

		}

		/// <summary>
		/// Tests that the Journeys in the Itinerary can be returned as a Journey array for a full, circular journey
		/// </summary>
		[Test]
		public void TestFullOutwardJourneyItinerary()
		{
			
			// Create a full itinerary
			FillItinerary(true, false, true, 2);
			
			Journey[] itineraryOutwardJourneys = TDItineraryManager.Current.OutwardJourneyItinerary;
			Journey[] itineraryReturnJourneys = TDItineraryManager.Current.ReturnJourneyItinerary;

			// Test the results
			
			Assert.AreEqual(3, itineraryOutwardJourneys.Length, "Outward journey array is of incorrect length.");
			Assert.AreEqual(0, itineraryReturnJourneys.Length, "Return journey array is of incorrect length.");

			
			int _journeyNumFirstSeg = 1;	// Result 1 of 1st segment
			int _journeyNumSecondSeg = 3;	// Result 1 of second segment
			int _journeyNumLastSeg = 5;		// Result 1 of return

			Assert.AreEqual("AAAA name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[0]).Details[0]).Origin.Location.Description,
				"Full Itinerary Origin location is not correct.");
			Assert.AreEqual("AAAA name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[itineraryOutwardJourneys.Length-1]).Details[0]).Destination.Location.Description,
				"Full Itinerary Destination location is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(30 * _journeyNumFirstSeg).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[0]).Details[0]).LegStart.DepartureDateTime.GetDateTime().ToString(),
				"Full Itinerary Depart time is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(20 + (30 * _journeyNumLastSeg)).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[itineraryOutwardJourneys.Length-1]).Details[0]).LegEnd.ArrivalDateTime.GetDateTime().ToString(),
				"Full Itinerary Arrive time is not correct.");

			Assert.AreEqual("MMMM name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[1]).Details[0]).Origin.Location.Description,
				"2nd Segment Origin location is not correct.");
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[1]).Details[0]).Destination.Location.Description,
				"2nd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(30 * _journeyNumSecondSeg).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[1]).Details[0]).LegStart.DepartureDateTime.GetDateTime().ToString(),
				"2nd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(20 + (30 * _journeyNumSecondSeg)).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[1]).Details[0]).LegEnd.ArrivalDateTime.GetDateTime().ToString(),
				"2nd Segment Arrive time is not correct.");

			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[2]).Details[0]).Origin.Location.Description,
				"Return Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[2]).Details[0]).Destination.Location.Description,
				"Return Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(30 * _journeyNumLastSeg).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[2]).Details[0]).LegStart.DepartureDateTime.GetDateTime().ToString(),
				"Return Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(20 + (30 * _journeyNumLastSeg)).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[2]).Details[0]).LegEnd.ArrivalDateTime.GetDateTime().ToString(),
				"Return Segment Arrive time is not correct.");
		}

		/// <summary>
		/// Tests that the Journeys in the Itinerary can be returned as a Journey array for a non-return journey
		/// </summary>
		[Test]
		public void TestNonReturnOutwardJourneyItinerary()
		{
			
			// Create a full itinerary
			FillItinerary(false, false, true, 2);
			
			Journey[] itineraryOutwardJourneys = TDItineraryManager.Current.OutwardJourneyItinerary;
			Journey[] itineraryReturnJourneys = TDItineraryManager.Current.ReturnJourneyItinerary;

			// Test the results
			
			Assert.AreEqual(2, itineraryOutwardJourneys.Length, "Outward journey array is of incorrect length.");
			Assert.AreEqual(0, itineraryReturnJourneys.Length, "Return journey array is of incorrect length.");

			
			int _journeyNumFirstSeg = 1;	// Result 1 of 1st segment
			int _journeyNumSecondSeg = 3;	// Result 1 of second segment

			Assert.AreEqual("AAAA name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[0]).Details[0]).Origin.Location.Description,
				"Full Itinerary Origin location is not correct.");
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[itineraryOutwardJourneys.Length-1]).Details[0]).Destination.Location.Description,
				"Full Itinerary Destination location is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(30 * _journeyNumFirstSeg).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[0]).Details[0]).LegStart.DepartureDateTime.GetDateTime().ToString(),
				"Full Itinerary Depart time is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(20 + (30 * _journeyNumSecondSeg)).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[itineraryOutwardJourneys.Length-1]).Details[0]).LegEnd.ArrivalDateTime.GetDateTime().ToString(),
				"Full Itinerary Arrive time is not correct.");

			Assert.AreEqual("MMMM name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[1]).Details[0]).Origin.Location.Description,
				"2nd Segment Origin location is not correct.");
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[1]).Details[0]).Destination.Location.Description,
				"2nd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(30 * _journeyNumSecondSeg).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[1]).Details[0]).LegStart.DepartureDateTime.GetDateTime().ToString(),
				"2nd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(20 + (30 * _journeyNumSecondSeg)).ToString(), ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)itineraryOutwardJourneys[1]).Details[0]).LegEnd.ArrivalDateTime.GetDateTime().ToString(),
				"2nd Segment Arrive time is not correct.");
		}


		/// <summary>
		/// Tests that Journey requests in the Itinerary can be returned using SpecificJourneyRequest method
		/// </summary>
		[Test]
		public void TestSpecificJourneyRequest()
		{
			
			// Create a full itinerary
			FillItinerary(true, false, true, 2);
			
			// Get the initial journey request
			ITDJourneyRequest journeyRequest = (ITDJourneyRequest)TDItineraryManager.Current.SpecificJourneyRequest(0);

			// Check that the data is as expected
			Assert.AreEqual("AAAA name VisitPlannerJourney1", journeyRequest.OriginLocation.Description, "Origin of initial journey request is not as expected.");
			Assert.AreEqual("MMMM name VisitPlannerJourney1", journeyRequest.DestinationLocation.Description, "Destination of initial journey request is not as expected.");


			// Get the second journey request
			ITDJourneyRequest journeyRequest2 = (ITDJourneyRequest)TDItineraryManager.Current.SpecificJourneyRequest(1);

			// Check that the data is as expected
			Assert.AreEqual("MMMM name VisitPlannerJourney1", journeyRequest2.OriginLocation.Description, "Origin of second journey request is not as expected.");
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", journeyRequest2.DestinationLocation.Description, "Destination of second journey request is not as expected.");


			// Get the return journey request
			ITDJourneyRequest journeyRequest3 = (ITDJourneyRequest)TDItineraryManager.Current.SpecificJourneyRequest(2);

			// Check that the data is as expected
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", journeyRequest3.OriginLocation.Description, "Origin of return journey request is not as expected.");
			Assert.AreEqual("AAAA name VisitPlannerJourney1", journeyRequest3.DestinationLocation.Description, "Destination of return journey request is not as expected.");
		}

		/// <summary>
		/// Tests that Journey results in the Itinerary can be returned using SpecificJourneyResult method
		/// </summary>
		[Test]
		public void TestSpecificJourneyResult()
		{
			
			// Create a full itinerary
			FillItinerary(true, false, true, 2);
			
			// Get the initial journey request
			ITDJourneyResult journeyResult = (ITDJourneyResult)TDItineraryManager.Current.SpecificJourneyResult(0);

			// Check that the data is as expected
			Assert.AreEqual("AAAA name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)journeyResult.OutwardPublicJourneys[0]).Details[0]).Origin.Location.Description, "Origin of initial journey result is not as expected.");
			Assert.AreEqual("MMMM name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)journeyResult.OutwardPublicJourneys[0]).Details[0]).Destination.Location.Description, "Destination of initial journey result is not as expected.");


			// Get the second journey request
			ITDJourneyResult journeyResult2 = (ITDJourneyResult)TDItineraryManager.Current.SpecificJourneyResult(1);

			// Check that the data is as expected
			Assert.AreEqual("MMMM name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)journeyResult2.OutwardPublicJourneys[0]).Details[0]).Origin.Location.Description, "Origin of second journey result is not as expected.");
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)journeyResult2.OutwardPublicJourneys[0]).Details[0]).Destination.Location.Description, "Destination of second journey result is not as expected.");


			// Get the return journey request
			ITDJourneyResult journeyResult3 = (ITDJourneyResult)TDItineraryManager.Current.SpecificJourneyResult(2);

			// Check that the data is as expected
			Assert.AreEqual("ZZZZ name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)journeyResult3.OutwardPublicJourneys[0]).Details[0]).Origin.Location.Description, "Origin of return journey result is not as expected.");
			Assert.AreEqual("AAAA name VisitPlannerJourney1", ((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)journeyResult3.OutwardPublicJourneys[0]).Details[0]).Destination.Location.Description, "Destination of return journey result is not as expected.");
		}

		/// <summary>
		/// Tests that a segment of the itinerary can be selected and within that, a journey
		/// </summary>
		[Test]
		public void TestSelectedItinerarySegment()
		{
			
			// Create a full itinerary
			FillItinerary(true, false, true, 3);
			
			// Select 2nd journey on 1st segment
			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			TDItineraryManager.Current.SelectedOutwardJourneyIndex = 1;		// array index of second journey
			TDItineraryManager.Current.SelectedOutwardJourneyID = 1;	// id of second journey
			TDItineraryManager.Current.SelectedOutwardJourneyType = TDJourneyType.PublicOriginal;

			// Get the journey result associated with the selected segment
			ITDJourneyResult journeyResult = TDItineraryManager.Current.SpecificJourneyResult(0);
			// Get the journey that has been selected
			JourneyControl.PublicJourney publicJourney = journeyResult.OutwardPublicJourney(1);

			
			int _journeyNumFirstSeg = 2;	// Result 2 of 1st segment

			// Use the locations and times to check the particular journey (2nd journey in result departs and arrives later than 1st)
			Assert.AreEqual("AAAA name VisitPlannerJourney1", publicJourney.Details[0].Origin.Location.Description, "Selected journey origin in 1st segment not as expected");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(30 * _journeyNumFirstSeg).ToString(), ((JourneyControl.PublicJourneyDetail)(publicJourney).Details[0]).LegStart.DepartureDateTime.GetDateTime().ToString(),
				"Selected journey depart time in 1st segment is not correct.");

            // Reset the selected segment. The selected journey will then be the second journey of the second segment
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			TDItineraryManager.Current.SelectedOutwardJourneyIndex = 1;		// array index of second journey
			TDItineraryManager.Current.SelectedOutwardJourneyID = 1;	// id of second journey
			TDItineraryManager.Current.SelectedOutwardJourneyType = TDJourneyType.PublicOriginal;

			// Get the journey result associated with the selected segment
			ITDJourneyResult journeyResult2 = TDItineraryManager.Current.SpecificJourneyResult(1);
			// Get the journey that has been selected
			JourneyControl.PublicJourney publicJourney2 = journeyResult2.OutwardPublicJourney(1);

			// Use the locations and times to check the particular journey (2nd journey in result departs and arrives later than 1st)
			Assert.AreEqual("MMMM name VisitPlannerJourney1", publicJourney2.Details[0].Origin.Location.Description, "Selected journey origin in 2nd segment not as expected");
			Assert.AreEqual(timeNow.AddDays(1).AddMinutes(150).ToString(), ((JourneyControl.PublicJourneyDetail)(publicJourney2).Details[0]).LegStart.DepartureDateTime.GetDateTime().ToString(),
				"Selected journey depart time in 2nd segment is not correct.");
		}


		/// <summary>
		/// Tests the validation of the journey times of the selected journeys across the segments
		/// </summary>
		[Test]
		public void TestValidateSelectedJourneyTimes()
		{
			
			bool isValid;
			
			// Create a full itinerary with times that don't overlap
			FillItinerary(true, false, true, 2);
			
			//				Segment 1					Segment 2					Segment 3
			// Journey1		dep = +30	arr = +80		dep = +90	arr = +140		dep = +150	arr = +200 (mins)

			isValid = ((VisitPlannerItineraryManager)TDItineraryManager.Current).ValidateSelectedJourneyTimes();

			Assert.IsTrue(isValid, "ValidateSelectedJourneyTimes has suggested an overlap where one is not expected");

			
			// Create a full itinerary with overlapping times
			VisitPlannerItineraryManager.Current.ResetItinerary();
			journeyNum = 1 ;
			FillItinerary(true, true, true, 2);
			
			//				Segment 1					Segment 2					Segment 3
			// Journey1		dep = +30	arr = +100		dep = +90	arr = +160		dep = +150	arr = +220

			isValid = ((VisitPlannerItineraryManager)TDItineraryManager.Current).ValidateSelectedJourneyTimes();

			Assert.IsFalse(isValid, "ValidateSelectedJourneyTimes has not identified an overlap where one is expected");

		}


		/// <summary>
		/// Tests the ability to add extra journeys to a segment earlier than the current set
		/// </summary>
		[Test]
		public void TestAddEarlierJourneys()
		{
			
			// Create a full itinerary
			FillItinerary(true, false, true, 3);
			
			// Add journeys with an earlier departure time to the 1st segment
			// Call ExtendJourneyResultEarlier to create a request with an earlier start time to current earliest
			bool madeRequest;
			madeRequest = ((VisitPlannerItineraryManager)TDItineraryManager.Current).ExtendJourneyResultEarlier(0);

			Assert.IsTrue(madeRequest, "The new request has not been created");

			// Check that the request that has been added to the session manager has an arrival time of 1 minute before the
			// alight time of the last leg of the 1st journey in the selected segment
			int legCount = ((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneys[0]).Details.Length;
			Assert.AreEqual(((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneys[0]).Details[legCount - 1]).LegEnd.ArrivalDateTime.AddMinutes(-1), 
				TDSessionManager.Current.JourneyRequest.OutwardDateTime[0], "The new request should have an arrival time of 1 minute before the alight time of the last leg of the 1st journey in the selected segment");

			// Add the journeys to the segment (we have to spoof 2 more journey results so they won't be particularly related to the request)
			// Create a journey result and add to the session
			TDJourneyResult newJourneys = CreateJourneyResult("NewJourneys", false, true, 3, TDSessionManager.Current.JourneyRequest);
			//TDSessionManager.Current.JourneyResult = newJourneys;

			// Add the journeys to the segment
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddJourneysToSegment(newJourneys);

			// Check that we have an extra 2 journeys in the segment
			Assert.AreEqual(4, TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneyCount, "4 journeys expected in the segment");

			// Check that they have been added to the beginning
			Assert.IsTrue(((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneys[3]).Details[0]).LegStart.Location.Description.IndexOf("VisitPlannerJourney") > 0);
			Assert.IsTrue(((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneys[0]).Details[0]).LegStart.Location.Description.IndexOf("NewJourneys") > 0);

		}

		/// <summary>
		/// Tests the ability to add extra journeys to a segment later than the current set
		/// </summary>
		[Test]
		public void TestAddLaterJourneys()
		{
			
			// Create a full itinerary
			FillItinerary(true, false, true, 3);
			
			// Add journeys with an earlier departure time to the 1st segment
			// Call ExtendJourneyResultEarlier to create a request with an earlier start time to current earliest
			bool madeRequest;
			madeRequest = ((VisitPlannerItineraryManager)TDItineraryManager.Current).ExtendJourneyResultLater(0);

			Assert.IsTrue(madeRequest, "The new request has not been created");

			// Check that the request that has been added to the session manager has a departure time of 1 minute after the
			// board time of the first leg of the last journey in the selected segment
			int journeyCount = TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneys.Count;
			Assert.AreEqual(((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneys[journeyCount - 1]).Details[0]).LegStart.DepartureDateTime.AddMinutes(+1), 
				TDSessionManager.Current.JourneyRequest.OutwardDateTime[0], "The new request should have a departure time of 1 minute after the board time of the first leg of the last journey in the selected segment");

			// Add the journeys to the segment (we have to spoof 2 more journey results so they won't be particularly related to the request)
			// Create a journey result and add to the session
			TDJourneyResult newJourneys = CreateJourneyResult("NewJourneys", false, true, 3, TDSessionManager.Current.JourneyRequest);
			//TDSessionManager.Current.JourneyResult = newJourneys;

			// Add the journeys to the segment
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddJourneysToSegment(newJourneys);

			// Check that we have an extra 2 journeys in the segment
			Assert.AreEqual(4, TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneyCount, "4 journeys expected in the segment");

			// Check that they have been added to the end
			Assert.IsTrue(((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneys[0]).Details[0]).LegStart.Location.Description.IndexOf("VisitPlannerJourney") > 0);
			Assert.IsTrue(((JourneyControl.PublicJourneyDetail)((JourneyControl.PublicJourney)TDItineraryManager.Current.SpecificJourneyResult(0).OutwardPublicJourneys[3]).Details[0]).LegStart.Location.Description.IndexOf("NewJourneys") > 0);

		}

		/// <summary>
		/// Tests the ability to stop adding journeys when max number is reached
		/// </summary>
		[Test]
		public void TestMaxAddJourneys()
		{
			
			// Create a full itinerary with 10 journeys one of which will be removed by the 
			// force coach function (which leaves 1 poss slot for a new journey)
			FillItinerary(true, false, true, 10);

			// ExtendSegmentPermitted should be set to true as we don't yet have max journeys in segment
			Assert.IsTrue(((VisitPlannerItineraryManager)TDItineraryManager.Current).ExtendSegmentPermitted(1), "Extend segment should be permitted");

			// Add journeys with an earlier departure time to the 2nd segment
			// Call ExtendJourneyResultEarlier to create a request with an earlier start time to current earliest
			bool madeRequest;
			madeRequest = ((VisitPlannerItineraryManager)TDItineraryManager.Current).ExtendJourneyResultLater(1);

			Assert.IsTrue(madeRequest, "The new request has not been created");


			// Add the journeys (actually only 1 should be added) to the segment (we have to spoof 2 more journey results so they won't be particularly related to the request)
			// Create a journey result and add to the session
			TDJourneyResult newJourneys = CreateJourneyResult("NewJourneys", false, true, 2, TDSessionManager.Current.JourneyRequest);
			//TDSessionManager.Current.JourneyResult = newJourneys;

			// Add the journeys to the segment (only 1 should be added)
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddJourneysToSegment(newJourneys);

			// Check that we have an extra journey in the segment
			Assert.AreEqual(10, TDItineraryManager.Current.SpecificJourneyResult(1).OutwardPublicJourneyCount, "10 journeys expected in the segment");

			// Try to add again
			TDJourneyResult tooManyJourneys = CreateJourneyResult("TooManyJourneys", false, true, 1, TDSessionManager.Current.JourneyRequest);
			//TDSessionManager.Current.JourneyResult = tooManyJourneys;

			// Attempt to add the journeys to the segment
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddJourneysToSegment(tooManyJourneys);

			// Check that we still have just 10 journeys in the segment
			Assert.AreEqual(10, TDItineraryManager.Current.SpecificJourneyResult(1).OutwardPublicJourneyCount, "10 journeys expected in the segment");

			// ExtendSegmentPermitted should be set to false as we have max journeys in segment
			Assert.IsFalse(((VisitPlannerItineraryManager)TDItineraryManager.Current).ExtendSegmentPermitted(1), "Extend segment should not be permitted");

			// ExtendSegmentPermitted should be set to true on a different segment (eg 1st) as we still only have 9 journeys there
			Assert.IsTrue(((VisitPlannerItineraryManager)TDItineraryManager.Current).ExtendSegmentPermitted(0), "Extend segment should be permitted on 1st segment");

		}

		/// <summary>
		/// Tests the ability to read properties set in the constructor
		/// </summary>
		[Test]
		public void TestProperties()
		{
			
			// Load properties from the properties service that are also exposed in VisitPlannerItineraryManager class
			int expectedinitialRequestSize        = int.Parse(Properties.Current["VisitPlan.InitialRequestSize"]);
			int expectedinitialRequestDiscardSize = int.Parse(Properties.Current["VisitPlan.InitialRequestDiscardSize"]);

			Assert.AreEqual(expectedinitialRequestSize, ((VisitPlannerItineraryManager)TDItineraryManager.Current).InitialRequestSize,
				"initialRequestSize not as expected");

			Assert.AreEqual(expectedinitialRequestDiscardSize, ((VisitPlannerItineraryManager)TDItineraryManager.Current).InitialRequestDiscardSize,
				"initialRequestDiscardSize not as expected");

		}

		#endregion

		/// <summary>
		/// Creates a dummy Journey Request for an Initial Journey
		/// </summary>
		/// <remarks>BuildJourneyRequest will normally do this but avoiding this call to keep the test simple</remarks>
		/// <param name="requestID">String used to enable Request to be identified in Assertions</param>
		/// <returns>Dummy Request</returns>
		public ITDJourneyRequest CreateJourneyRequest(string requestID, ITDJourneyRequest previousRequest)
		{
			ITDJourneyRequest request = new TDJourneyRequest();

			request.IsReturnRequired = true;
			request.OutwardArriveBefore = false;
			request.ReturnArriveBefore = true;

			request.OutwardDateTime = new TDDateTime[1];
			request.OutwardDateTime[0] = new TDDateTime(timeNow).AddDays(1);

			request.ReturnDateTime = new TDDateTime[1];
			request.ReturnDateTime[0] = new TDDateTime(timeNow.AddDays(1).AddMinutes(180));
			

			request.InterchangeSpeed = 1;
			request.WalkingSpeed = 2;
			request.MaxWalkingTime = 3;
			request.DrivingSpeed = 4;
			request.AvoidMotorways = false;
			request.PublicViaLocations = new TDLocation[1];
			request.PrivateViaLocation = new TDLocation();
			request.AvoidRoads = new string[] {"A1", "A6"};
			request.AlternateLocations = new TDLocation[2];
			request.AlternateLocations[0] = new TDLocation();
			request.AlternateLocations[1] = new TDLocation();
			request.AlternateLocationsFrom = true;
			request.PrivateAlgorithm = PrivateAlgorithmType.MostEconomical;
			request.PublicAlgorithm = PublicAlgorithmType.Fastest;

			// Get the origin from the previous destination
			if (previousRequest != null)
			{
				request.OriginLocation = previousRequest.DestinationLocation;
			}
			else
			{
				request.OriginLocation = new TDLocation();
				request.OriginLocation.Description = "AAAA name " + requestID + journeyNum;
			}

			request.DestinationLocation = new TDLocation();
			request.DestinationLocation.Description = "MMMM name " + requestID + journeyNum;

			return request;
		}



		/// <summary>
		/// Creates a dummy Journey Parameters object for an Initial Journey
		/// </summary>
		/// <param name="requestID">String used to enable object to be identified in Assertions</param>
		/// <returns>Dummy Parameters object</returns>
		public TDJourneyParametersVisitPlan CreateJourneyParameters(string requestID, bool makeCircular)
		{
			TDJourneyParametersVisitPlan journeyParameters = new TDJourneyParametersVisitPlan();

			// Make the travel date tomorrow
			TDDateTime outwardTravelDate = new TDDateTime(timeNow).AddDays(1);
			
			journeyParameters.OutwardDayOfMonth = outwardTravelDate.ToString("dd");
			journeyParameters.OutwardMonthYear = outwardTravelDate.ToString("MM") + "/" + outwardTravelDate.ToString("yyyy");
			journeyParameters.OutwardHour = outwardTravelDate.ToString("hh");
			journeyParameters.OutwardMinute = outwardTravelDate.ToString("mm");
			journeyParameters.PublicModes = new ModeType[1] {ModeType.Rail};
			journeyParameters.InterchangeSpeed = 10;
			journeyParameters.WalkingSpeed = 2;
			journeyParameters.PublicAlgorithmType = PublicAlgorithmType.Fastest;

			// Multi segment parameters
			journeyParameters.SetStayDuration(0, 60);

			if (makeCircular)
				journeyParameters.SetStayDuration(1, 120);
			
			TDLocation location1 = new TDLocation();
			location1.Description = "AAAA name " + requestID + journeyNum;
			journeyParameters.SetLocation(0, location1);
			TDLocation location2 = new TDLocation();
			location2.Description = "MMMM name " + requestID + journeyNum;
			journeyParameters.SetLocation(1, location2);
			TDLocation location3 = new TDLocation();
			location3.Description = "ZZZZ name " + requestID + journeyNum;
			journeyParameters.SetLocation(2, location3);

			return journeyParameters;
		}
		
		/// <summary>
		/// Returns a dummy journey result. Unlike Extend, doesn't get assigned to the session.
		/// </summary>
		/// <param name="journeyID">Text to identify journey</param>
		/// <param name="includeReturn">True if return journey is to be included</param>
		/// <param name="publicMode">True if results are to use public transport</param>
		/// <param name="resultCount">Number of result options to generate</param>
		/// <returns>Journey results</returns>
		private TDJourneyResult CreateJourneyResult(string journeyID, bool overlapTimes, bool publicMode, int resultCount, ITDJourneyRequest journeyRequest)
		{
			// Create journey result
			TDJourneyResult thisResult = new TDJourneyResult(1234);
			JourneyResult cjpResult = CreateCJPResult(false, overlapTimes, publicMode, journeyID, resultCount );
			thisResult.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			
			// The request and parameters objects will have been updated with the correct locations
			// (ie orig = last dest when extending from itinerary end, etc)
			// We need to make the same update to the journey result so the locations match.
			if (journeyRequest.OriginLocation != null && journeyRequest.OriginLocation.Description.Length >= 0)
			{
				if (publicMode)
				{
					foreach (JourneyControl.PublicJourney publicJourney in thisResult.OutwardPublicJourneys)
					{
                        publicJourney.Details[0].Origin.Location = journeyRequest.OriginLocation;
					}
					
				}
				else
				{
					// No need to do anything for road journey as the origin and destination don't change, so they are only
					// stored on the request object
				}
			}

			if (journeyRequest.DestinationLocation != null && journeyRequest.DestinationLocation.Description.Length >= 0)
			{
				if (publicMode)
				{
					foreach (JourneyControl.PublicJourney publicJourney in thisResult.OutwardPublicJourneys)
					{
						publicJourney.Details[0].Destination.Location = journeyRequest.DestinationLocation;
					}
					
				}
			}


			if (publicMode)
			{
				TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyType = TDJourneyType.PublicOriginal;
			}
			else
			{
				TDSessionManager.Current.JourneyViewState.SelectedOutwardJourneyType = TDJourneyType.RoadCongested;
			}

			return thisResult;
		}

		/// <summary>
		/// Create a CJPResult containing either private or public journeys
		/// </summary>
		/// <param name="Public">bool Public - true for a Public journey, false for a private journey</param>
		/// <param name="requestID">String used to enable Request/Results to be identified in Assertions</param>
		/// <param name="journeyCount">Number of result options to be generated</param>
		/// <returns>JourneyResult</returns>
		private JourneyResult CreateCJPResult( bool doReturn, bool overlapTimes, bool Public, string requestID, int journeyCount )
		{
			JourneyResult result = new JourneyResult();

			if( Public )
			{
				result.publicJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[journeyCount];
				for (int i=0; i<journeyCount; i++, journeyNum++)
				{
					result.publicJourneys[i] = CreatePublicJourney(doReturn, overlapTimes, requestID);
				}
			}
			else
			{
				result.privateJourneys = new PrivateJourney[journeyCount];
				for (int i=0; i<journeyCount; i++, journeyNum++)
				{
					result.privateJourneys[i] = CreatePrivateJourney(doReturn, requestID);
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
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney CreatePublicJourney(bool doReturn, bool overlapTimes, string requestID)
		{
			TransportDirect.JourneyPlanning.CJPInterface.PublicJourney result = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();

			int returnMinutes;
			string serviceStartPoint = String.Empty;
			string serviceEndPoint = String.Empty;
			string startPoint = String.Empty;
			string endPoint = String.Empty;

			int journeyDuration;
			if (overlapTimes)
				journeyDuration = 80;
			else
				journeyDuration = 20;

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

			result.legs[0].mode = ModeType.Tram;
			result.legs[0].validated = true;

			result.legs[0].board = new Event();
			result.legs[0].board.activity = ActivityType.Depart;
			result.legs[0].board.departTime = timeNow.AddDays(1).AddMinutes(returnMinutes + (30 * journeyNum));
			result.legs[0].board.stop = new Stop();
			result.legs[0].board.stop.NaPTANID = startPoint + " NAPTANID " + requestID + journeyNum;
			result.legs[0].board.stop.name = startPoint + " name " + requestID + journeyNum;

			result.legs[0].alight = new Event();
			result.legs[0].alight.activity = ActivityType.Arrive;
			result.legs[0].alight.arriveTime = timeNow.AddDays(1).AddMinutes(returnMinutes + (journeyDuration + (30 * journeyNum)));
			result.legs[0].alight.stop = new Stop();
			result.legs[0].alight.stop.NaPTANID = endPoint + " NAPTANID " + requestID + journeyNum;
			result.legs[0].alight.stop.name = endPoint + " name " + requestID + journeyNum;

			result.legs[0].destination = new Event();
			result.legs[0].destination.activity = ActivityType.Arrive;
			result.legs[0].destination.arriveTime = timeNow.AddDays(1).AddMinutes(returnMinutes + (45 + (30 * journeyNum)));
			result.legs[0].destination.stop = new Stop();
			result.legs[0].destination.stop.NaPTANID = "ZZZZ NAPTANID " + requestID + journeyNum;
			result.legs[0].destination.stop.name = "ZZZZ name " + requestID + journeyNum;			
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


		/// <summary>
		/// Returns a complete visit planner itinerary
		/// </summary>
		public void FillItinerary(bool makeCircular, bool overlapTimes, bool publicMode, int resultCount)
		{
			// Create a parameters object to represent data specified by user
			TDJourneyParametersVisitPlan visitPlannerParameters = CreateJourneyParameters("VisitPlannerJourney", makeCircular);

			// Build request
			ITDJourneyRequest visitPlannerRequest = ((VisitPlannerItineraryManager)TDItineraryManager.Current).BuildNextRequest(visitPlannerParameters);
			
			// Create a journey result
			TDJourneyResult visitPlannerResult = CreateJourneyResult("VisitPlannerJourney", overlapTimes, publicMode, resultCount, visitPlannerRequest);
			TDJourneyState visitPlannerJourneyState = new TDJourneyState();
			
			// Add journey result into itinerary
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest, visitPlannerResult, visitPlannerJourneyState);

			// Build the request for the second segment
			ITDJourneyRequest visitPlannerRequest2 = ((VisitPlannerItineraryManager)TDItineraryManager.Current).BuildNextRequest(visitPlannerParameters);



			// At this point the request would be sent to CJP and results returned (stored in segment store)
			// Need to simulate this, as to work out next segment journey start times we need to know results of prev segment
			
			// Create a journey result
			ITDJourneyResult visitPlannerResult2 = CreateJourneyResult("FirstExtension", overlapTimes, publicMode, resultCount, visitPlannerRequest2);
			
			// Add journey result into itinerary
			((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest2, visitPlannerResult2, visitPlannerJourneyState);


			if (makeCircular)
			{
				// Build the next request (this is a circular journey so we have a request to return to original location)
				ITDJourneyRequest visitPlannerRequest3 = ((VisitPlannerItineraryManager)TDItineraryManager.Current).BuildNextRequest(visitPlannerParameters);


				// Create a journey result
				ITDJourneyResult visitPlannerResult3 = CreateJourneyResult("ReturnToOrigin", overlapTimes, publicMode, resultCount, visitPlannerRequest3);
				
				// Add journey result into itinerary
				((VisitPlannerItineraryManager)TDItineraryManager.Current).AddSegmentToItinerary(visitPlannerParameters, visitPlannerRequest3, visitPlannerResult3, visitPlannerJourneyState);

			}

		}

	}
}
