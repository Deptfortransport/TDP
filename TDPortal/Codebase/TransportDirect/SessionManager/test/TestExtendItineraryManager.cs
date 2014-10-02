// ***********************************************
// NAME 		: TestExtendItineraryManager.cs
// AUTHOR 		: Paul Cross
// DATE CREATED : 21/09/2005
// DESCRIPTION 	: NUnit test for ExtendItineraryManager class.
// NOTES		: Was created to test TDItineraryManager class but that has now been extended
//				  so this adaptation is to test the ExtendItineraryManager class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestExtendItineraryManager.cs-arc  $
//
//   Rev 1.2   Dec 05 2012 13:57:24   mmodi
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
//   Rev 1.4   Mar 15 2006 14:20:06   rhopkins
//Corrections to unit tests
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 14 2006 16:45:20   tmollart
//Manual merge (automatic failed) of stream 3353 onto trunk.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2.1.3   Mar 13 2006 18:25:22   rhopkins
//Corrections to various unit tests
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2.1.2   Mar 02 2006 17:45:32   NMoorhouse
//extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2.1.1   Dec 20 2005 19:47:36   rhopkins
//Changed to use new version of TDItineraryManager and ExtendItineraryManager
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2.1.0   Dec 15 2005 12:41:52   rhopkins
//Changed tests to use new TDItineraryManager methods for Extend
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Oct 29 2005 09:23:54   tmollart
//Code Review: Updated version history etc.
//Resolution for 2638: DEL 8 Stream: Visit Planner

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
	public class TestExtendItineraryManager
	{
		private DateTime timeNow;
		private int journeyNum;

		private ExtendItineraryManager itineraryManager;

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation(true) );
			TDSessionManager.Current.ItineraryMode = ItineraryManagerMode.ExtendJourney;

			itineraryManager = TDItineraryManager.Current as ExtendItineraryManager;

			timeNow = TDDateTime.Now.GetDateTime();
			journeyNum = 1;
			itineraryManager.NewSearch();
			itineraryManager.JourneyViewState = new TDJourneyViewState();
		}

		[TearDown] 
		public void Dispose()
		{ 
		}

		#region Tests

		/// <summary>
		/// Tests that the initial Journey Result can be used to create an Itinerary
		/// </summary>
		[Test]
		public void TestCreateItinerary()
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Create an initial journey request
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");

			TDDateTime outwardDateTime = (sessionManager.JourneyRequest.OutwardDateTime != null) ? sessionManager.JourneyRequest.OutwardDateTime[0] : null;
			TDDateTime returnDateTime = (sessionManager.JourneyRequest.ReturnDateTime != null) ? sessionManager.JourneyRequest.ReturnDateTime[0] : null;

			// Create an initial journey result
			JourneyResult cjpResult = CreateCJPResult(false, true, "InitialJourney", 2 );
			TDJourneyResult initialResult = new TDJourneyResult(1234, 0, outwardDateTime, returnDateTime, initialRequest.OutwardArriveBefore, initialRequest.ReturnArriveBefore, false);
			initialResult.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			TDSessionManager.Current.JourneyResult = initialResult;

			itineraryManager.CreateItinerary();

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, itineraryManager.Length, "Itinerary is of incorrect length.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"Returned JourneyResult does not match original JourneyResult");
		}

		/// <summary>
		/// Tests that Journey Result can be added to the end of the Itinerary
		/// </summary>
		[Test]
		public void TestAddToEndOfItinerary()
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Indicate that extension will be added to END of itinerary
			bool _successful = itineraryManager.ExtendFromItineraryEndPoint();

			Assert.IsTrue(itineraryManager.ExtendInProgress,
				"ItineraryManager is not in 'ExtendInProgress' mode.");

			// Test that the new journey parameters for the new journey has the same origin as the end point
			// of the existing segment in the itinerary
			Assert.AreEqual(itineraryManager.SpecificJourneyRequest(0).DestinationLocation.Description, TDSessionManager.Current.JourneyParameters.OriginLocation.Description,
				"Origin for new Extension is not current Destination of Itinerary.");
			
			Assert.AreEqual("MMMM name InitialJourney1", TDSessionManager.Current.JourneyParameters.OriginLocation.Description,
				"Origin for new Extension is not as expected.");

			// Create a journey extension
			CompleteJourneyRequest("FirstExtension");

			TDDateTime outwardDateTime = (sessionManager.JourneyRequest.OutwardDateTime != null) ? sessionManager.JourneyRequest.OutwardDateTime[0] : null;
			TDDateTime returnDateTime = (sessionManager.JourneyRequest.ReturnDateTime != null) ? sessionManager.JourneyRequest.ReturnDateTime[0] : null;

			JourneyResult cjpResult = CreateCJPResult(false, true, "FirstExtension", 2 );
			TDJourneyResult firstExtensionResult = new TDJourneyResult(1234, 0, outwardDateTime, returnDateTime, initialRequest.OutwardArriveBefore, initialRequest.ReturnArriveBefore, false);
			firstExtensionResult.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			TDSessionManager.Current.JourneyResult = firstExtensionResult;

			// Add extension to Itinerary
			itineraryManager.AddExtensionToItinerary();

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(2, itineraryManager.Length,
				"Itinerary is of incorrect length.");

			Assert.IsTrue(itineraryManager.ExtendInProgress == false,
				"ItineraryManager is still in 'ExtendInProgress' mode.");

			itineraryManager.SelectedItinerarySegment = 0;
			Assert.IsTrue(itineraryManager.JourneyRequest == initialRequest,
				"First Itinerary Segment Request does not match original Request.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"First Itinerary Segment does not match Original Result");

			itineraryManager.SelectedItinerarySegment = 1;
			Assert.AreEqual(initialRequest.DestinationLocation.Description, itineraryManager.JourneyRequest.OriginLocation.Description,
				"Origin of Second Itinerary Segment Request does not match Destination of Original Request");
			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"Second Itinerary Segment does not match First Extension");
		}

		/// <summary>
		/// Tests that Journey Result can be added to the start of the Itinerary
		/// </summary>
		[Test]
		public void TestAddToStartOfItinerary()
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Indicate that extension will be added to START of itinerary
			bool _successful = itineraryManager.ExtendToItineraryStartPoint();

			Assert.IsTrue(itineraryManager.ExtendInProgress,
				"ItineraryManager is not in 'ExtendInProgress' mode.");

			// Test that the new journey parameters for the new journey has the same destination as the start point
			// of the existing segment in the itinerary
			Assert.AreEqual(itineraryManager.SpecificJourneyRequest(0).OriginLocation.Description, TDSessionManager.Current.JourneyParameters.DestinationLocation.Description,
				"Origin for new Extension is not current Destination of Itinerary.");

			// Create a journey extension
			CompleteJourneyRequest("FirstExtension");

			TDDateTime outwardDateTime = (sessionManager.JourneyRequest.OutwardDateTime != null) ? sessionManager.JourneyRequest.OutwardDateTime[0] : null;
			TDDateTime returnDateTime = (sessionManager.JourneyRequest.ReturnDateTime != null) ? sessionManager.JourneyRequest.ReturnDateTime[0] : null;

			JourneyResult cjpResult = CreateCJPResult(false, true, "FirstExtension", 2 );
			TDJourneyResult firstExtensionResult = new TDJourneyResult(1234, 0, outwardDateTime, returnDateTime, initialRequest.OutwardArriveBefore, initialRequest.ReturnArriveBefore, false);
			firstExtensionResult.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			TDSessionManager.Current.JourneyResult = firstExtensionResult;

			// Add extension to Itinerary
			itineraryManager.AddExtensionToItinerary();

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(2, itineraryManager.Length,
				"Itinerary is of incorrect length.");

			Assert.IsTrue(itineraryManager.ExtendInProgress == false,
				"ItineraryManager is still in 'ExtendInProgress' mode.");

			itineraryManager.SelectedItinerarySegment = 0;
			Assert.AreEqual(initialRequest.OriginLocation.Description,
				itineraryManager.JourneyRequest.DestinationLocation.Description,
				"Destination of First Itinerary Segment Request does not match Origin of Original Request");
			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"First Itinerary Segment does not match First Extension");

			itineraryManager.SelectedItinerarySegment = 1;
			Assert.IsTrue(itineraryManager.JourneyRequest == initialRequest,
				"Second Itinerary Segment Request does not match original Request.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"Second Itinerary Segment does not match Original Result");
		}

		/// <summary>
		/// Tests that the END of the Itinerary cannot be extended beyond its maximum
		/// </summary>
		[Test]
		public void TestAddToMaximumEndOfItinerary()
		{
			bool _successful;
			TDLocation _prevLocation;

			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Create four journey extensions on END of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, true);
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, true, 2, true);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(3, itineraryManager.Length, "Itinerary is of incorrect length.");

			itineraryManager.SelectedItinerarySegment = 0;
			Assert.IsTrue(itineraryManager.JourneyRequest == initialRequest,
				"First Itinerary Segment Request does not match Original Request.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"First Itinerary Segment does not match Original Result");

			_prevLocation = itineraryManager.JourneyRequest.DestinationLocation;
			itineraryManager.SelectedItinerarySegment = 1;
			Assert.AreEqual(_prevLocation.Description, itineraryManager.JourneyRequest.OriginLocation.Description,
				"Origin of First Extension Request does not match Destination of Original Request");
			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"Second Itinerary Segment does not match First Extension");

			_prevLocation = itineraryManager.JourneyRequest.DestinationLocation;
			itineraryManager.SelectedItinerarySegment = 2;
			Assert.AreEqual(_prevLocation.Description, itineraryManager.JourneyRequest.OriginLocation.Description,
				"Origin of Second Extension Request does not match Destination of First Extension Request");
			Assert.IsTrue(itineraryManager.JourneyResult == secondExtensionResult,
				"Third Itinerary Segment does not match Second Extension");

			// Attempt to create second journey extension
			_successful = itineraryManager.ExtendFromItineraryEndPoint();
			Assert.IsTrue( ((_successful == false) || itineraryManager.ExtendPermitted),
				"Allowed to start second journey extension");
		}

		/// <summary>
		/// Tests that the START of the Itinerary cannot be extended beyond its maximum
		/// </summary>
		[Test]
		public void TestAddToMaximumStartOfItinerary()
		{
			bool _successful;
			TDLocation _prevLocation;

			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Create four journey extensions on START of Itinerary
			TDSessionManager.Current.JourneyParameters.ExtendEndOfItinerary = false;
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, false);
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, true, 2, false);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(3, itineraryManager.Length, "Itinerary is of incorrect length.");

			itineraryManager.SelectedItinerarySegment = 2;
			Assert.IsTrue(itineraryManager.JourneyRequest == initialRequest,
				"Third Itinerary Segment Request does not match Original Request.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"Third Itinerary Segment does not match Original Result");

			_prevLocation = itineraryManager.JourneyRequest.OriginLocation;
			itineraryManager.SelectedItinerarySegment = 1;
			Assert.AreEqual(_prevLocation.Description, itineraryManager.JourneyRequest.DestinationLocation.Description,
				"Destination of First Extension Request does not match Origin of Original Request");
			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"Second Itinerary Segment does not match First Extension");

			_prevLocation = itineraryManager.JourneyRequest.OriginLocation;
			itineraryManager.SelectedItinerarySegment = 0;
			Assert.AreEqual(_prevLocation.Description, itineraryManager.JourneyRequest.DestinationLocation.Description,
				"Destination of Second Extension Request does not match Origin of First Extension Request");
			Assert.IsTrue(itineraryManager.JourneyResult == secondExtensionResult,
				"First Itinerary Segment does not match Second Extension");

			// Attempt to create second journey extension
			_successful = itineraryManager.ExtendToItineraryStartPoint();
			Assert.IsTrue( ((_successful == false) || itineraryManager.ExtendPermitted),
				"Allowed to start second journey extension");
		}

		/// <summary>
		/// Tests that the most recent Extension can be deleted from the END of the Itinerary
		/// </summary>
		[Test]
		public void TestDeleteFromEndOfItinerary()
		{
			TDLocation _prevLocation;

			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Create two journey extensions on END of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, true);
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, true, 2, true);

			// Check length of Itinerary and values of all elements prior to Delete
			Assert.AreEqual(3, itineraryManager.Length,
				"Before Delete - Itinerary is of incorrect length.");
			itineraryManager.SelectedItinerarySegment = 0;
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"Before Delete - First Itinerary Segment does not match Original Result");
			itineraryManager.SelectedItinerarySegment = 1;
			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"Before Delete - Second Itinerary Segment does not match First Extension");
			itineraryManager.SelectedItinerarySegment = 2;
			Assert.IsTrue(itineraryManager.JourneyResult == secondExtensionResult,
				"Before Delete - Third Itinerary Segment does not match Second Extension");

			itineraryManager.DeleteLastExtension();

			// Check length of Itinerary and values of all elements after to Delete
			Assert.AreEqual(2, itineraryManager.Length,
				"After Delete - Itinerary is of incorrect length.");
			itineraryManager.SelectedItinerarySegment = 0;
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"After Delete - First Itinerary Segment does not match Original Result");
			itineraryManager.SelectedItinerarySegment = 1;
			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"After Delete - Second Itinerary Segment does not match First Extension");
			itineraryManager.SelectedItinerarySegment = 2;
			Assert.IsTrue(itineraryManager.JourneyResult == null,
				"After Delete - Third Itinerary Segment still exists");


			// Create 3rd journey extension on END of 1st extension
			TDJourneyResult thirdExtensionResult = GetExtend("ThirdExtension", false, true, 2, true);


			// Check length of Itinerary and values of all elements
			Assert.AreEqual(3, itineraryManager.Length,
				"After re-add - Itinerary is of incorrect length.");

			itineraryManager.SelectedItinerarySegment = 0;
			Assert.IsTrue(itineraryManager.JourneyRequest == initialRequest,
				"After re-add - First Itinerary Segment Request does not match Original Request.");
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"After re-add - First Itinerary Segment does not match Original Result");

			_prevLocation = itineraryManager.JourneyRequest.DestinationLocation;
			itineraryManager.SelectedItinerarySegment = 1;
			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"After re-add - Second Itinerary Segment does not match First Extension");
			Assert.AreEqual(_prevLocation.Description, itineraryManager.JourneyRequest.OriginLocation.Description,
				"After re-add - Origin of First Extension Request does not match Destination of Original Request");

			_prevLocation = itineraryManager.JourneyRequest.DestinationLocation;
			itineraryManager.SelectedItinerarySegment = 2;
			Assert.IsTrue(itineraryManager.JourneyResult == thirdExtensionResult,
				"After re-add - Third Itinerary Segment does not match Third Extension");
			Assert.AreEqual(_prevLocation.Description, itineraryManager.JourneyRequest.OriginLocation.Description,
				"After re-add - Origin of Third Extension Request does not match Destination of First Extension Request");
		}

		/// <summary>
		/// Tests that the most recent Extension can be deleted from the START of the Itinerary
		/// </summary>
		[Test]
		public void TestDeleteFromStartOfItinerary()
		{
			TDLocation _prevLocation;

			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Create two journey extensions on START of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, false);
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, true, 2, false);

			// Check length of Itinerary and values of all elements prior to Delete
			Assert.AreEqual(3, itineraryManager.Length,
				"Before Delete - Itinerary is of incorrect length.");
			itineraryManager.SelectedItinerarySegment = 0;
			Assert.IsTrue(itineraryManager.JourneyResult == secondExtensionResult,
				"Before Delete - First Itinerary Segment does not match Second Extension");
			itineraryManager.SelectedItinerarySegment = 1;
			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"Before Delete - Second Itinerary Segment does not match First Extension");
			itineraryManager.SelectedItinerarySegment = 2;
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"Before Delete - Third Itinerary Segment does not match Original Result");

			itineraryManager.DeleteLastExtension();

			// Check length of Itinerary and values of all elements after to Delete
			Assert.AreEqual(2, itineraryManager.Length,
				"After Delete - Itinerary is of incorrect length.");
			itineraryManager.SelectedItinerarySegment = 0;
			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"After Delete - First Itinerary Segment does not match First Extension");
			itineraryManager.SelectedItinerarySegment = 1;
			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"After Delete - Second Itinerary Segment does not match Original Result");
			itineraryManager.SelectedItinerarySegment = 2;
			Assert.IsTrue(itineraryManager.JourneyResult == null,
				"After Delete - Third Itinerary Segment still exists");


			// Create 3rd journey extension on START of 1st extension
			TDJourneyResult thirdExtensionResult = GetExtend("ThirdExtension", false, true, 2, false);


			// Check length of Itinerary and values of all elements
			Assert.AreEqual(3, itineraryManager.Length,
				"After re-add - Itinerary is of incorrect length.");

			itineraryManager.SelectedItinerarySegment = 0;

			Assert.IsTrue(itineraryManager.JourneyResult == thirdExtensionResult,
				"After re-add - First Itinerary Segment does not match Third Extension");
			_prevLocation = itineraryManager.JourneyRequest.DestinationLocation;
			itineraryManager.SelectedItinerarySegment = 1;
			Assert.AreEqual(itineraryManager.JourneyRequest.OriginLocation.Description, _prevLocation.Description,
				"After re-add - Destination of Third Extension Request does not match Origin of First Extension Request");

			Assert.IsTrue(itineraryManager.JourneyResult == firstExtensionResult,
				"After re-add - Second Itinerary Segment does not match First Extension");
			_prevLocation = itineraryManager.JourneyRequest.DestinationLocation;
			itineraryManager.SelectedItinerarySegment = 2;
			Assert.AreEqual(itineraryManager.JourneyRequest.OriginLocation.Description, _prevLocation.Description,
				"After re-add - Destination of First Extension Request does not match Origin of Original Request");

			Assert.IsTrue(itineraryManager.JourneyResult == initialResult,
				"After re-add - Third Itinerary Segment does not match Original Result");
		}

		/// <summary>
		/// Tests that the Itinerary can be cleared down
		/// </summary>
		[Test]
		public void TestNewSearch()
		{
			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Create 1st extension on END of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, true);

			// Create 2nd extension on START of Itinerary
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, true, 2, false);

			// Check length of Itinerary before Clear
			Assert.AreEqual(3, itineraryManager.Length,
				"Itinerary is of incorrect length.");

			// Check itinerary properties before clear
			string testPropertyValue = String.Empty;


			itineraryManager.SelectedItinerarySegment = 0;
			testPropertyValue = itineraryManager.JourneyResult.OutwardPublicJourney(0).Details[0].Destination.Location.Description;
			Assert.IsTrue(testPropertyValue.IndexOf("InitialJourney") >=0, "First journey different to expected");

			itineraryManager.SelectedItinerarySegment = 1;
			testPropertyValue = itineraryManager.JourneyResult.OutwardPublicJourney(0).Details[0].Origin.Location.Description;
			Assert.IsTrue(testPropertyValue.IndexOf("InitialJourney") >=0, "Second journey different to expected");

			itineraryManager.SelectedItinerarySegment = 2;
			testPropertyValue = itineraryManager.JourneyResult.OutwardPublicJourney(0).Details[0].Origin.Location.Description;
			Assert.IsTrue(testPropertyValue.IndexOf("InitialJourney") >=0, "Third journey different to expected");

			
			itineraryManager.NewSearch();

			// Check length of Itinerary after Clear
			Assert.AreEqual(0, itineraryManager.Length,
				"Itinerary is of incorrect length.");

			Assert.IsTrue(itineraryManager.ExtendInProgress == false,
				"ItineraryManager is still in 'ExtendInProgress' mode.");


			// To test if empty, try reading a value (like we did above) - should error
			try
			{
				testPropertyValue = ((JourneyControl.PublicJourney)itineraryManager.JourneyResult.OutwardPublicJourney(0)).Details[0].Destination.Location.Description;
				// No error - value still available - therefore array not emptied
				Assert.Fail("Itinerary manager not reset");
			}
			catch
			{
				// Error as expected
			}

		}

		/// <summary>
		/// Tests that the Itinerary can be cleared down and the Working Results be reset to the Initial Journey Request/Results
		/// </summary>
		[Test]
		public void TestResetToInitialJourney()
		{
			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Create 1st extension on END of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, true);

			// Create 2nd extension on START of Itinerary
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, true, 2, false);

			// Check length of Itinerary and values of all elements before Clear
			Assert.AreEqual(3, itineraryManager.Length,"Itinerary is of incorrect length.");

			itineraryManager.ResetToInitialJourney();

			// Check length of Itinerary after Reset
			Assert.AreEqual(0, itineraryManager.Length, "Itinerary is of incorrect length.");

			Assert.IsTrue(itineraryManager.ExtendInProgress == false, "ItineraryManager is still in 'ExtendInProgress' mode.");

			Assert.IsTrue(TDSessionManager.Current.JourneyRequest == initialRequest, "Request in working area does not match Original Request");
			Assert.IsTrue(TDSessionManager.Current.JourneyResult == initialResult, "Result in working area does not match Original Result");
		}

		/// <summary>
		/// Tests that the latest extension can be removed from the Itinerary and restored to the working area ready for a different journey selection
		/// </summary>
		[Test]
		public void TestResetLastExtension()
		{
			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Create 1st extension on END of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, true);

			// Create 2nd extension on START of Itinerary
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, true, 2, false);

			// Check length of Itinerary and values of all elements before Clear
			Assert.AreEqual(3, itineraryManager.Length, "Itinerary is of incorrect length.");

			itineraryManager.ResetLastExtension();

			// Check Itinerary and Working Area after Reset
			Assert.AreEqual(2, itineraryManager.Length, "Itinerary is of incorrect length.");
			Assert.IsTrue(itineraryManager.ExtendInProgress, 
				"ItineraryManager is not in 'ExtendInProgress' mode.");
			Assert.IsTrue(TDSessionManager.Current.JourneyResult == secondExtensionResult,
				"Result in working area does not match 2nd extension");
		}

		/// <summary>
		/// Tests that the Origin and Destination details for the Outward Itinerary can be retrieved
		/// </summary>
		[Test]
		public void TestGetOutwardItineraryOriginAndDestination()
		{
			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Create 1st extension on END of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, true);

			// Create 2nd extension on START of Itinerary
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, true, 2, false);

			TDLocation _origin = itineraryManager.OutwardOriginLocation();
			TDLocation _destination = itineraryManager.OutwardDestinationLocation();
			TDDateTime _depart = itineraryManager.OutwardDepartDateTime();
			TDDateTime _arrive = itineraryManager.OutwardArriveDateTime();

			int _journeyNumFirstSeg = 5;	// Result 1 of 2nd extension
			int _journeyNumLastSeg = 3;		// Result 1 of 1st extension

			Assert.AreEqual("AAAA name SecondExtension" + _journeyNumFirstSeg, _origin.Description,
				"Origin location is not correct.");
			Assert.AreEqual("MMMM name FirstExtension" + _journeyNumLastSeg, _destination.Description,
				"Destination location is not correct.");

			Assert.AreEqual(timeNow.AddMinutes(30 * _journeyNumFirstSeg).ToString(), _depart.GetDateTime().ToString(),
				"Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _journeyNumLastSeg)).ToString(), _arrive.GetDateTime().ToString(),
				"Arrive time is not correct.");
		}

		/// <summary>
		/// Tests that the Origin and Destination details for the Return Itinerary can be retrieved
		/// </summary>
		[Test]
		public void TestGetReturnItineraryOriginAndDestination()
		{
			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(true, true, 2);

			// Create 1st extension on END of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", true, true, 2, true);

			// Create 2nd extension on START of Itinerary
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", true, true, 2, false);

			TDLocation _origin = itineraryManager.ReturnOriginLocation();
			TDLocation _destination = itineraryManager.ReturnDestinationLocation();
			TDDateTime _depart = itineraryManager.ReturnDepartDateTime();
			TDDateTime _arrive = itineraryManager.ReturnArriveDateTime();

			int _outwardJourneyNumFirstSeg = 9;		// Result 1 of 2nd outward extension
			//int _outwardJourneyNumSecondSeg = 1;	// Result 1 of initial outward journey
			int _outwardJourneyNumLastSeg = 5;		// Result 1 of 1st outward extension
			int _returnJourneyNumFirstSeg = 7;		// Result 1 of 1st return extension
			//int _returnJourneyNumSecondSeg = 3;		// Result 1 of initial return journey
			int _returnJourneyNumLastSeg = 11;		// Result 1 of 2nd return extension


			// Names of return locations are set by the outward journey request
			Assert.AreEqual("MMMM name FirstExtension" + _outwardJourneyNumLastSeg, _origin.Description, "Origin location is not correct.");
			Assert.AreEqual("AAAA name SecondExtension" + _outwardJourneyNumFirstSeg, _destination.Description, "Destination location is not correct.");

			// Return journeys are 180 mins after outward start plus 30 mins per segment.
			// 20 mins are added for arrival to account for time of journey
			Assert.AreEqual(timeNow.AddMinutes(180 + (30 * _returnJourneyNumFirstSeg)).ToString(), _depart.GetDateTime().ToString(), "Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + 20 + (30 * _returnJourneyNumLastSeg)).ToString(), _arrive.GetDateTime().ToString(), "Arrive time is not correct.");
		}

		/// <summary>
		/// Tests that the Itinerary Summary can be retrieved without Return journeys
		/// </summary>
		[Test]
		public void TestItinerarySummaryWithoutReturns()
		{
			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Create 1st extension on END of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, true);

			// Create 2nd extension on START of Itinerary
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, false, 1, false);

			JourneySummaryLine[] fullItinerarySummary = itineraryManager.FullItinerarySummary();
			JourneySummaryLine[] outwardSegmentsSummary = itineraryManager.OutwardSegmentsSummary();
			JourneySummaryLine[] returnSegmentsSummary = itineraryManager.ReturnSegmentsSummary();

			// Only Outward journey
			Assert.AreEqual(1, fullItinerarySummary.Length, "Full Itinerary Summary is of incorrect length.");

			// 3 segments plus an overall journey summary segment 
			Assert.AreEqual(3, outwardSegmentsSummary.Length, "Outward Summary is of incorrect length.");
			Assert.AreEqual(0, returnSegmentsSummary.Length, "Return Summary is of incorrect length.");

			int _journeyNumFirstSeg = 5;	// Result 1 of 2nd extension
			int _journeyNumSecondSeg = 1;	// Result 1 of initial journey
			int _journeyNumLastSeg = 3;		// Result 1 of 1st extension

			Assert.AreEqual("AAAA name SecondExtension" + _journeyNumFirstSeg, fullItinerarySummary[0].OriginDescription,
				"Full Itinerary Origin location is not correct.");
			Assert.AreEqual("MMMM name FirstExtension" + _journeyNumLastSeg, fullItinerarySummary[0].DestinationDescription,
				"Full Itinerary Destination location is not correct.");
			Assert.AreEqual(timeNow.ToString(), fullItinerarySummary[0].DepartureDateTime.GetDateTime().ToString(),
				"Full Itinerary Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _journeyNumLastSeg)).ToString(), fullItinerarySummary[0].ArrivalDateTime.GetDateTime().ToString(),
				"Full Itinerary Arrive time is not correct.");
			Assert.AreEqual(2, fullItinerarySummary[0].InterchangeCount,
				"Full Itinerary Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.Itinerary, fullItinerarySummary[0].Type,
				"Full Itinerary Journey Type is not correct.");

			Assert.AreEqual("AAAA name SecondExtension" + _journeyNumFirstSeg, outwardSegmentsSummary[0].OriginDescription,
				"1st Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name InitialJourney1", outwardSegmentsSummary[0].DestinationDescription,
				"1st Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.ToString(), outwardSegmentsSummary[0].DepartureDateTime.GetDateTime().ToString(),
				"1st Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(25).ToString(), outwardSegmentsSummary[0].ArrivalDateTime.GetDateTime().ToString(),
				"1st Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardSegmentsSummary[0].InterchangeCount,
				"1st Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.RoadCongested, outwardSegmentsSummary[0].Type,
				"1st Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name InitialJourney" + _journeyNumSecondSeg, outwardSegmentsSummary[1].OriginDescription,
				"2nd Segment Origin location is not correct.");
			Assert.AreEqual("MMMM name InitialJourney" + _journeyNumSecondSeg, outwardSegmentsSummary[1].DestinationDescription,
				"2nd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(30 * _journeyNumSecondSeg).ToString(), outwardSegmentsSummary[1].DepartureDateTime.GetDateTime().ToString(),
				"2nd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _journeyNumSecondSeg)).ToString(), outwardSegmentsSummary[1].ArrivalDateTime.GetDateTime().ToString(),
				"2nd Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardSegmentsSummary[1].InterchangeCount,
				"2nd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, outwardSegmentsSummary[1].Type,
				"2nd Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name FirstExtension" + _journeyNumLastSeg, outwardSegmentsSummary[2].OriginDescription,
				"3rd Segment Origin location is not correct.");
			Assert.AreEqual("MMMM name FirstExtension" + _journeyNumLastSeg, outwardSegmentsSummary[2].DestinationDescription,
				"3rd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(30 * _journeyNumLastSeg).ToString(), outwardSegmentsSummary[2].DepartureDateTime.GetDateTime().ToString(),
				"3rd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _journeyNumLastSeg)).ToString(), outwardSegmentsSummary[2].ArrivalDateTime.GetDateTime().ToString(),
				"3rd Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardSegmentsSummary[2].InterchangeCount,
				"3rd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, outwardSegmentsSummary[2].Type,
				"3rd Segment Journey Type is not correct.");
		}

		/// <summary>
		/// Tests that the Itinerary Summary can be retrieved with Return journeys
		/// </summary>
		[Test]
		public void TestItinerarySummaryWithReturns()
		{
			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(true, true, 2);

			// Create 1st extension on END of Itinerary
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", true, true, 2, true);

			// Create 2nd extension on START of Itinerary
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", true, false, 1, false);

			JourneySummaryLine[] fullItinerarySummary = itineraryManager.FullItinerarySummary();
			JourneySummaryLine[] outwardSegmentsSummary = itineraryManager.OutwardSegmentsSummary();
			JourneySummaryLine[] returnSegmentsSummary = itineraryManager.ReturnSegmentsSummary();

			// Only Outward journey
			Assert.AreEqual(2, fullItinerarySummary.Length, "Full Itinerary Summary is of incorrect length.");

			// 3 segments plus an overall journey summary segment 
			Assert.AreEqual(3, outwardSegmentsSummary.Length, "Outward Summary is of incorrect length.");
			Assert.AreEqual(3, returnSegmentsSummary.Length, "Return Summary is of incorrect length.");

			int _outwardJourneyNumFirstSeg = 9;		// Result 1 of 2nd outward extension
			int _outwardJourneyNumSecondSeg = 1;	// Result 1 of initial outward journey
			int _outwardJourneyNumLastSeg = 5;		// Result 1 of 1st outward extension
			int _returnJourneyNumFirstSeg = 7;		// Result 1 of 1st return extension
			int _returnJourneyNumSecondSeg = 3;		// Result 1 of initial return journey
			int _returnJourneyNumLastSeg = 9;		// Result 1 of 2nd return extension

			Assert.AreEqual("AAAA name SecondExtension" + _outwardJourneyNumFirstSeg, fullItinerarySummary[0].OriginDescription,
				"Outward Full Itinerary Origin location is not correct.");
			Assert.AreEqual("MMMM name FirstExtension" + _outwardJourneyNumLastSeg, fullItinerarySummary[0].DestinationDescription,
				"Outward Full Itinerary Destination location is not correct.");
			Assert.AreEqual(timeNow.ToString(), fullItinerarySummary[0].DepartureDateTime.GetDateTime().ToString(),
				"Outward Full Itinerary Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _outwardJourneyNumLastSeg)).ToString(), fullItinerarySummary[0].ArrivalDateTime.GetDateTime().ToString(),
				"Outward Full Itinerary Arrive time is not correct.");
			Assert.AreEqual(2, fullItinerarySummary[0].InterchangeCount,
				"Outward Full Itinerary Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.Itinerary, fullItinerarySummary[0].Type,
				"Outward Full Itinerary Journey Type is not correct.");

			Assert.AreEqual("MMMM name FirstExtension" + _returnJourneyNumFirstSeg, fullItinerarySummary[1].OriginDescription,
				"Return Full Itinerary Origin location is not correct.");
			Assert.AreEqual("AAAA name SecondExtension" + _returnJourneyNumLastSeg, fullItinerarySummary[1].DestinationDescription,
				"Return Full Itinerary Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (30 * _returnJourneyNumFirstSeg)).ToString(), fullItinerarySummary[1].DepartureDateTime.GetDateTime().ToString(),
				"Return Full Itinerary Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180).ToString(), fullItinerarySummary[1].ArrivalDateTime.GetDateTime().ToString(),
				"Return Full Itinerary Arrive time is not correct.");
			Assert.AreEqual(2, fullItinerarySummary[1].InterchangeCount,
				"Return Full Itinerary Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.Itinerary, fullItinerarySummary[1].Type,
				"Return Full Itinerary Journey Type is not correct.");

			Assert.AreEqual("AAAA name SecondExtension" + _outwardJourneyNumFirstSeg, outwardSegmentsSummary[0].OriginDescription,
				"Outward 1st Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name InitialJourney1", outwardSegmentsSummary[0].DestinationDescription,
				"Outward 1st Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.ToString(), outwardSegmentsSummary[0].DepartureDateTime.GetDateTime().ToString(),
				"Outward 1st Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(25).ToString(), outwardSegmentsSummary[0].ArrivalDateTime.GetDateTime().ToString(),
				"Outward 1st Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardSegmentsSummary[0].InterchangeCount,
				"Outward 1st Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.RoadCongested, outwardSegmentsSummary[0].Type,
				"Outward 1st Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name InitialJourney" + _outwardJourneyNumSecondSeg, outwardSegmentsSummary[1].OriginDescription,
				"Outward 2nd Segment Origin location is not correct.");
			Assert.AreEqual("MMMM name InitialJourney" + _outwardJourneyNumSecondSeg, outwardSegmentsSummary[1].DestinationDescription,
				"Outward 2nd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(30 * _outwardJourneyNumSecondSeg).ToString(), outwardSegmentsSummary[1].DepartureDateTime.GetDateTime().ToString(),
				"Outward 2nd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _outwardJourneyNumSecondSeg)).ToString(), outwardSegmentsSummary[1].ArrivalDateTime.GetDateTime().ToString(),
				"Outward 2nd Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardSegmentsSummary[1].InterchangeCount,
				"Outward 2nd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, outwardSegmentsSummary[1].Type,
				"Outward 2nd Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name FirstExtension" + _outwardJourneyNumLastSeg, outwardSegmentsSummary[2].OriginDescription,
				"Outward 3rd Segment Origin location is not correct.");
			Assert.AreEqual("MMMM name FirstExtension" + _outwardJourneyNumLastSeg, outwardSegmentsSummary[2].DestinationDescription,
				"Outward 3rd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(30 * _outwardJourneyNumLastSeg).ToString(), outwardSegmentsSummary[2].DepartureDateTime.GetDateTime().ToString(),
				"Outward 3rd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _outwardJourneyNumLastSeg)).ToString(), outwardSegmentsSummary[2].ArrivalDateTime.GetDateTime().ToString(),
				"Outward 3rd Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardSegmentsSummary[2].InterchangeCount,
				"Outward 3rd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, outwardSegmentsSummary[2].Type,
				"Outward 3rd Segment Journey Type is not correct.");

			Assert.AreEqual("MMMM name FirstExtension" + _returnJourneyNumFirstSeg, returnSegmentsSummary[0].OriginDescription,
				"Return 1st Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name FirstExtension" + _returnJourneyNumFirstSeg, returnSegmentsSummary[0].DestinationDescription,
				"Return 1st Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (30 * _returnJourneyNumFirstSeg)).ToString(), returnSegmentsSummary[0].DepartureDateTime.GetDateTime().ToString(),
				"Return 1st Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (20 + (30 * _returnJourneyNumFirstSeg))).ToString(), returnSegmentsSummary[0].ArrivalDateTime.GetDateTime().ToString(),
				"Return 1st Segment Arrive time is not correct.");
			Assert.AreEqual(0, returnSegmentsSummary[0].InterchangeCount,
				"Return 1st Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, returnSegmentsSummary[0].Type,
				"Return 1st Segment Journey Type is not correct.");

			Assert.AreEqual("MMMM name InitialJourney" + _returnJourneyNumSecondSeg, returnSegmentsSummary[1].OriginDescription,
				"Return 2nd Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name InitialJourney" + _returnJourneyNumSecondSeg, returnSegmentsSummary[1].DestinationDescription,
				"Return 2nd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (30 * _returnJourneyNumSecondSeg)).ToString(), returnSegmentsSummary[1].DepartureDateTime.GetDateTime().ToString(),
				"Return 2nd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (20 + (30 * _returnJourneyNumSecondSeg))).ToString(), returnSegmentsSummary[1].ArrivalDateTime.GetDateTime().ToString(),
				"Return 2nd Segment Arrive time is not correct.");
			Assert.AreEqual(0, returnSegmentsSummary[1].InterchangeCount,
				"Return 2nd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, returnSegmentsSummary[1].Type,
				"Return 2nd Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name InitialJourney1", returnSegmentsSummary[2].OriginDescription,
				"Return 3rd Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name SecondExtension" + _returnJourneyNumLastSeg, returnSegmentsSummary[2].DestinationDescription,
				"Return 3rd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180-25).ToString(), returnSegmentsSummary[2].DepartureDateTime.GetDateTime().ToString(),
				"Return 3rd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180).ToString(), returnSegmentsSummary[2].ArrivalDateTime.GetDateTime().ToString(),
				"Return 3rd Segment Arrive time is not correct.");
			Assert.AreEqual(0, returnSegmentsSummary[2].InterchangeCount,
				"Return 3rd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.RoadCongested, returnSegmentsSummary[2].Type,
				"Return 3rd Segment Journey Type is not correct.");
		}

		#endregion


		#region Non-test methods

		/// <summary>
		/// Creates a dummy Journey Request for an Initial Journey
		/// </summary>
		/// <param name="requestID">String used to enable Request to be identified in Assertions</param>
		/// <returns>Dummy Request</returns>
		public ITDJourneyRequest CreateJourneyRequest(string requestID)
		{
			ITDJourneyRequest request = new TDJourneyRequest();
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
			TDSessionManager.Current.JourneyRequest.IsReturnRequired = true;
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
					TDSessionManager.Current.JourneyRequest.OriginLocation.Description = "AAAA name " + requestID + journeyNum;
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
					TDSessionManager.Current.JourneyRequest.DestinationLocation.Description = "MMMM name " + requestID + journeyNum;

				}
				else
				{
					TDSessionManager.Current.JourneyRequest.DestinationLocation = TDSessionManager.Current.JourneyParameters.DestinationLocation;
				}
			}
		}

		/// <summary>
		/// Create dummy journey result in working area
		/// </summary>
		/// <param name="journeyID">Text to identify journey</param>
		/// <param name="includeReturn">True if return journey is to be included</param>
		/// <param name="publicMode">True if results are to use public transport</param>
		/// <param name="resultCount">Number of result options to generate</param>
		/// <returns>Journey results</returns>
		private TDJourneyResult GetResult(string journeyID, bool includeReturn, bool publicMode, int resultCount)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Create journey result
			TDDateTime outwardDateTime = (sessionManager.JourneyRequest.OutwardDateTime != null) ? sessionManager.JourneyRequest.OutwardDateTime[0] : null;
			TDDateTime returnDateTime = (sessionManager.JourneyRequest.ReturnDateTime != null) ? sessionManager.JourneyRequest.ReturnDateTime[0] : null;

			TDJourneyResult thisResult = new TDJourneyResult(1234, 0, outwardDateTime, returnDateTime, sessionManager.JourneyRequest.OutwardArriveBefore, sessionManager.JourneyRequest.ReturnArriveBefore, false);
			JourneyResult cjpResult = CreateCJPResult(false, publicMode, journeyID, resultCount );
			thisResult.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			
			// If the result has been created from a call to extend journey then the request and parameters objects
			// will have been updated with the correct locations (ie orig = last dest when extending from itinerary end, etc)
			// We need to make the same update to the journey result so the locations match.
			if (TDSessionManager.Current.JourneyRequest.OriginLocation != null && TDSessionManager.Current.JourneyRequest.OriginLocation.Description.Length >= 0)
			{
				if (publicMode)
				{
					foreach (JourneyControl.PublicJourney publicJourney in thisResult.OutwardPublicJourneys)
					{
						publicJourney.Details[0].Origin.Location = TDSessionManager.Current.JourneyRequest.OriginLocation;
					}
					
					if (includeReturn)
					{
						foreach (JourneyControl.PublicJourney publicJourney in thisResult.ReturnPublicJourneys)
						{
							publicJourney.Details[0].Destination.Location = TDSessionManager.Current.JourneyRequest.OriginLocation;
						}
					}
				}
				else
				{
					// No need to do anything for road journey as the origin and destination don't change, so they are only
					// stored on the request object
				}
			}

			if (TDSessionManager.Current.JourneyRequest.DestinationLocation != null && TDSessionManager.Current.JourneyRequest.DestinationLocation.Description.Length >= 0)
			{
				if (publicMode)
				{
					foreach (JourneyControl.PublicJourney publicJourney in thisResult.OutwardPublicJourneys)
					{
						publicJourney.Details[0].Destination.Location = TDSessionManager.Current.JourneyParameters.DestinationLocation;
					}
					
					if (includeReturn)
					{
						foreach (JourneyControl.PublicJourney publicJourney in thisResult.ReturnPublicJourneys)
						{
							publicJourney.Details[0].Origin.Location = TDSessionManager.Current.JourneyParameters.DestinationLocation;
						}
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

			if (includeReturn)
			{
				cjpResult = CreateCJPResult(true, publicMode, journeyID, resultCount );
				thisResult.AddResult(cjpResult, false, null, null, null, null, "ssss", false, -1);
				if (publicMode)
				{
					TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyType  = TDJourneyType.PublicOriginal;
				}
				else
				{
					TDSessionManager.Current.JourneyViewState.SelectedReturnJourneyType  = TDJourneyType.RoadCongested;
				}
			}

			TDSessionManager.Current.JourneyResult = thisResult;

			return thisResult;
		}

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
				result.publicJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[journeyCount];
				for (int i=0; i<journeyCount; i++, journeyNum++)
				{
					result.publicJourneys[i] = CreatePublicJourney(doReturn, requestID);
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
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney CreatePublicJourney(bool doReturn, string requestID)
		{
			TransportDirect.JourneyPlanning.CJPInterface.PublicJourney result = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();

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

			result.legs[0].mode = ModeType.Tram;
			result.legs[0].validated = true;

			result.legs[0].board = new Event();
			result.legs[0].board.activity = ActivityType.Depart;
			result.legs[0].board.departTime = timeNow.AddMinutes(returnMinutes + (30 * journeyNum));
			result.legs[0].board.stop = new Stop();
			result.legs[0].board.stop.NaPTANID = startPoint + " NAPTANID " + requestID + journeyNum;
			result.legs[0].board.stop.name = startPoint + " name " + requestID + journeyNum;

			result.legs[0].alight = new Event();
			result.legs[0].alight.activity = ActivityType.Arrive;
			result.legs[0].alight.arriveTime = timeNow.AddMinutes(returnMinutes + (20 + (30 * journeyNum)));
			result.legs[0].alight.stop = new Stop();
			result.legs[0].alight.stop.NaPTANID = endPoint + " NAPTANID " + requestID + journeyNum;
			result.legs[0].alight.stop.name = endPoint + " name " + requestID + journeyNum;

			result.legs[0].destination = new Event();
			result.legs[0].destination.activity = ActivityType.Arrive;
			result.legs[0].destination.arriveTime = timeNow.AddMinutes(returnMinutes + (45 + (30 * journeyNum)));
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
		/// Create the initial journey in the Itinerary
		/// </summary>
		/// <param name="includeReturn">True if return journey is to be included</param>
		/// <param name="publicMode">True if results are to use public transport</param>
		/// <param name="resultCount">Number of result options to generate</param>
		/// <param name="extendFromEnd">True if extension to be added to end; false if extension to be added to start</param>
		/// <returns>Journey results</returns>
		private TDJourneyResult CreateItinerary(bool includeReturn, bool publicMode, int resultCount)
		{
			TDJourneyResult thisResult = GetResult("InitialJourney", includeReturn, publicMode, resultCount);

			itineraryManager.CreateItinerary();

			return thisResult;
		}

		/// <summary>
		/// Extend the current Itinerary
		/// </summary>
		/// <param name="journeyID">Text to identify journey</param>
		/// <param name="includeReturn">True if return journey is to be included</param>
		/// <param name="publicMode">True if results are to use public transport</param>
		/// <param name="resultCount">Number of result options to generate</param>
		/// <param name="extendFromEnd">True if extension to be added to end; false if extension to be added to start</param>
		/// <returns>Journey results</returns>
		private TDJourneyResult GetExtend(string journeyID, bool includeReturn, bool publicMode, int resultCount, bool extendFromEnd)
		{
			bool _successful;

			if (extendFromEnd)
			{
				_successful = itineraryManager.ExtendFromItineraryEndPoint();

				// Set journey parameters

			}
			else
			{
				_successful = itineraryManager.ExtendToItineraryStartPoint();
			}

			CompleteJourneyRequest(journeyID);
			TDJourneyResult thisResult = GetResult(journeyID, includeReturn, publicMode, resultCount);

			itineraryManager.AddExtensionToItinerary();

			return thisResult;
		}
	}

	#endregion
}
