// ***********************************************
// NAME 		: TestTDItineraryManager.cs
// AUTHOR 		: Richard Hopkins
// DATE CREATED : 23/04/2004
// DESCRIPTION 	: NUnit test for TDItineraryManager class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TestTDItineraryManager.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:46   mturner
//Initial revision.
//
//   Rev 1.11   Feb 08 2006 13:30:24   mtillett
//Tests will be removed in stream3180, therefore, set to Ignore
//
//   Rev 1.10   Aug 24 2005 16:06:56   RPhilpott
//Make tests consistent with changed TDJourneyResult.AddResult() and PublicJourney ctor. 
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.9   Mar 21 2005 17:11:02   jgeorge
//Removed references to TransportDirect.UserPortal.SessionManager.test namespace
//
//   Rev 1.8   Mar 01 2005 16:45:46   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.7   Feb 08 2005 11:31:58   RScott
//Assertion changed to Assert
//
//   Rev 1.6   Jan 20 2005 10:42:54   RScott
//DEL 7 Updated - PublicViaLocations substituted  for PublicVialocation
//
//   Rev 1.5   Oct 27 2004 11:57:50   jbroome
//Removed references to Toll property of CJP Drive section class for DEL 7 CJP interface.
//
//   Rev 1.4   Aug 11 2004 13:34:40   CHosegood
//Pass attribute removed from Stop in CJP 6.0.0.0 interface.
//
//   Rev 1.3   May 19 2004 14:50:50   RHopkins
//Improvements to tests for latest version of ItineraryManager
//
//   Rev 1.2   May 13 2004 17:01:10   RHopkins
//Added properties for individual Outward and Return Lengths and a new method for resetting the last extension.
//
//   Rev 1.1   May 12 2004 11:36:16   RHopkins
//Added ExtendInProgress and corrected names of summary line functions
//
//   Rev 1.0   May 10 2004 15:33:14   RHopkins
//Initial revision.

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
	[Ignore("Tests will be removed in stream3180")]
	public class TestTDItineraryManager
	{
		private DateTime timeNow;
		private int journeyNum;

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation() );
			TDSessionManager.Current.JourneyViewState = new TDJourneyViewState();
			timeNow = TDDateTime.Now.GetDateTime();
			journeyNum = 1;
			TDItineraryManager.Current.NewSearch();
		}

		[TearDown] 
		public void Dispose()
		{ 
		}

		/// <summary>
		/// Tests that the initial Journey Result can be used to create an Itinerary
		/// </summary>
		[Test]
		public void TestCreateItinerary()
		{
			// Create an initial journey request
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");

			// Create an initial journey result
			JourneyResult cjpResult = CreateCJPResult(false, true, "InitialJourney", 2 );
			TDJourneyResult initialResult = new TDJourneyResult(1234);
			initialResult.AddResult(cjpResult, true, null, null, null, "ssss");
			TDSessionManager.Current.JourneyResult = initialResult;

			TDItineraryManager.Current.CreateItinerary();

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(1, TDItineraryManager.Current.Length, "Itinerary is of incorrect length.");
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
				"Returned JourneyResult does not match original JourneyResult");
		}

		/// <summary>
		/// Tests that Journey Result can be added to the end of the Itinerary
		/// </summary>
		[Test]
		public void TestAddToEndOfItinerary()
		{
			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Indicate that extension will be added to END of itinerary
			bool _successful = TDItineraryManager.Current.ExtendFromItineraryEndPoint();

			Assert.IsTrue(TDItineraryManager.Current.ExtendInProgress,
				"ItineraryManager is not in 'ExtendInProgress' mode.");
			Assert.AreEqual(TDItineraryManager.Current.JourneyRequest.DestinationLocation.Description, TDSessionManager.Current.JourneyRequest.OriginLocation.Description,
				"Origin for new Extension is not current Destination of Itinerary.");
			Assert.AreEqual("MMMM name InitialJourney1", TDSessionManager.Current.JourneyRequest.OriginLocation.Description,
				"Origin for new Extension is not as expected.");

			// Create a journey extension
			CompleteJourneyRequest("FirstExtension");
			JourneyResult cjpResult = CreateCJPResult(false, true, "FirstExtension", 2 );
			TDJourneyResult firstExtensionResult = new TDJourneyResult(1234);
			firstExtensionResult.AddResult(cjpResult, true, null, null, null, "ssss");
			TDSessionManager.Current.JourneyResult = firstExtensionResult;

			// Add extension to Itinerary
			TDItineraryManager.Current.AddExtensionToItinerary();

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(2, TDItineraryManager.Current.Length,
				"Itinerary is of incorrect length.");

			Assert.IsTrue(TDItineraryManager.Current.ExtendInProgress == false,
				"ItineraryManager is still in 'ExtendInProgress' mode.");

			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.IsTrue(TDItineraryManager.Current.JourneyRequest == initialRequest,
				"First Itinerary Segment Request does not match original Request.");
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
				"First Itinerary Segment does not match Original Result");

			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.AreEqual(initialRequest.DestinationLocation.Description, TDItineraryManager.Current.JourneyRequest.OriginLocation.Description,
				"Origin of Second Itinerary Segment Request does not match Destination of Original Request");
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"Second Itinerary Segment does not match First Extension");
		}

		/// <summary>
		/// Tests that Journey Result can be added to the start of the Itinerary
		/// </summary>
		[Test]
		public void TestAddToStartOfItinerary()
		{
			// Create an initial journey in the Itinerary
			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
			TDJourneyResult initialResult = CreateItinerary(false, true, 2);

			// Indicate that extension will be added to START of itinerary
			bool _successful = TDItineraryManager.Current.ExtendToItineraryStartPoint();

			Assert.IsTrue(TDItineraryManager.Current.ExtendInProgress,
				"ItineraryManager is not in 'ExtendInProgress' mode.");
			Assert.AreEqual(TDItineraryManager.Current.JourneyRequest.OriginLocation.Description, TDSessionManager.Current.JourneyRequest.DestinationLocation.Description,
				"Destination for new Extension is not current Origin of Itinerary.");

			// Create a journey extension
			CompleteJourneyRequest("FirstExtension");
			JourneyResult cjpResult = CreateCJPResult(false, true, "FirstExtension", 2 );
			TDJourneyResult firstExtensionResult = new TDJourneyResult(1234);
			firstExtensionResult.AddResult(cjpResult, true, null, null, null, "ssss");
			TDSessionManager.Current.JourneyResult = firstExtensionResult;

			// Add extension to Itinerary
			TDItineraryManager.Current.AddExtensionToItinerary();

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(2, TDItineraryManager.Current.Length,
				"Itinerary is of incorrect length.");

			Assert.IsTrue(TDItineraryManager.Current.ExtendInProgress == false,
				"ItineraryManager is still in 'ExtendInProgress' mode.");

			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.AreEqual(initialRequest.OriginLocation.Description,
				TDItineraryManager.Current.JourneyRequest.DestinationLocation.Description,
				"Destination of First Itinerary Segment Request does not match Origin of Original Request");
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"First Itinerary Segment does not match First Extension");

			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.IsTrue(TDItineraryManager.Current.JourneyRequest == initialRequest,
				"Second Itinerary Segment Request does not match original Request.");
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
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
			TDJourneyResult thirdExtensionResult = GetExtend("ThirdExtension", false, true, 2, true);
			TDJourneyResult fourthExtensionResult = GetExtend("FourthExtension", false, true, 2, true);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(5, TDItineraryManager.Current.Length,
				"Itinerary is of incorrect length.");

			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.IsTrue(TDItineraryManager.Current.JourneyRequest == initialRequest,
				"First Itinerary Segment Request does not match Original Request.");
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
				"First Itinerary Segment does not match Original Result");

			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"Second Itinerary Segment does not match First Extension");
			Assert.AreEqual(_prevLocation.Description, TDItineraryManager.Current.JourneyRequest.OriginLocation.Description,
				"Origin of First Extension Request does not match Destination of Original Request");

			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 2;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == secondExtensionResult,
				"Third Itinerary Segment does not match Second Extension");
			Assert.AreEqual(_prevLocation.Description, TDItineraryManager.Current.JourneyRequest.OriginLocation.Description,
				"Origin of Second Extension Request does not match Destination of First Extension Request");

			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 3;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == thirdExtensionResult,
				"Fourth Itinerary Segment does not match Third Extension");
			Assert.AreEqual(_prevLocation.Description, TDItineraryManager.Current.JourneyRequest.OriginLocation.Description,
				"Origin of Third Extension Request does not match Destination of Second Extension Request");

			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 4;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == fourthExtensionResult,
				"Fifth Itinerary Segment does not match Fourth Extension");
			Assert.AreEqual(_prevLocation.Description, TDItineraryManager.Current.JourneyRequest.OriginLocation.Description,
				"Origin of Fourth Extension Request does not match Destination of Third Extension Request");

			// Attempt to create fifth journey extension
			_successful = TDItineraryManager.Current.ExtendFromItineraryEndPoint();
			Assert.IsTrue(_successful == false,
				"Allowed to start fifth journey extension");
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
			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", false, true, 2, false);
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, true, 2, false);
			TDJourneyResult thirdExtensionResult = GetExtend("ThirdExtension", false, true, 2, false);
			TDJourneyResult fourthExtensionResult = GetExtend("FourthExtension", false, true, 2, false);

			// Check length of Itinerary and values of all elements
			Assert.AreEqual(5, TDItineraryManager.Current.Length, "Itinerary is of incorrect length.");

			TDItineraryManager.Current.SelectedItinerarySegment = 0;

			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == fourthExtensionResult,
				"First Itinerary Segment does not match Fourth Extension");
			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.AreEqual(TDItineraryManager.Current.JourneyRequest.OriginLocation.Description, _prevLocation.Description,
				"Destination of Fourth Extension Request does not match Origin of Third Extension Request");

			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == thirdExtensionResult,
				"Second Itinerary Segment does not match Third Extension");
			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 2;
			Assert.AreEqual(TDItineraryManager.Current.JourneyRequest.OriginLocation.Description, _prevLocation.Description,
				"Destination of Third Extension Request does not match Origin of Second Extension Request");

			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == secondExtensionResult,
				"Third Itinerary Segment does not match Second Extension");
			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 3;
			Assert.AreEqual(TDItineraryManager.Current.JourneyRequest.OriginLocation.Description, _prevLocation.Description,
				"Destination of Second Extension Request does not match Origin of First Extension Request");

			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"Fourth Itinerary Segment does not match First Extension");
			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 4;
			Assert.AreEqual(TDItineraryManager.Current.JourneyRequest.OriginLocation.Description, _prevLocation.Description,
				"Destination of First Extension Request does not match Origin of Original Request");

			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
				"Fifth Itinerary Segment does not match Original Result");


			// Attempt to create fifth journey extension
			_successful = TDItineraryManager.Current.ExtendToItineraryStartPoint();
			Assert.IsTrue(_successful == false,
				"Allowed to start fifth journey extension");
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
			Assert.AreEqual(3, TDItineraryManager.Current.Length,
				"Before Delete - Itinerary is of incorrect length.");
			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
				"Before Delete - First Itinerary Segment does not match Original Result");
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"Before Delete - Second Itinerary Segment does not match First Extension");
			TDItineraryManager.Current.SelectedItinerarySegment = 2;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == secondExtensionResult,
				"Before Delete - Third Itinerary Segment does not match Second Extension");

			TDItineraryManager.Current.DeleteLastExtension();

			// Check length of Itinerary and values of all elements after to Delete
			Assert.AreEqual(2, TDItineraryManager.Current.Length,
				"After Delete - Itinerary is of incorrect length.");
			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
				"After Delete - First Itinerary Segment does not match Original Result");
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"After Delete - Second Itinerary Segment does not match First Extension");
			TDItineraryManager.Current.SelectedItinerarySegment = 2;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == null,
				"After Delete - Third Itinerary Segment still exists");


			// Create 3rd journey extension on END of 1st extension
			TDJourneyResult thirdExtensionResult = GetExtend("ThirdExtension", false, true, 2, true);


			// Check length of Itinerary and values of all elements
			Assert.AreEqual(3, TDItineraryManager.Current.Length,
				"After re-add - Itinerary is of incorrect length.");

			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.IsTrue(TDItineraryManager.Current.JourneyRequest == initialRequest,
				"After re-add - First Itinerary Segment Request does not match Original Request.");
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
				"After re-add - First Itinerary Segment does not match Original Result");

			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"After re-add - Second Itinerary Segment does not match First Extension");
			Assert.AreEqual(_prevLocation.Description, TDItineraryManager.Current.JourneyRequest.OriginLocation.Description,
				"After re-add - Origin of First Extension Request does not match Destination of Original Request");

			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 2;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == thirdExtensionResult,
				"After re-add - Third Itinerary Segment does not match Third Extension");
			Assert.AreEqual(_prevLocation.Description, TDItineraryManager.Current.JourneyRequest.OriginLocation.Description,
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
			Assert.AreEqual(3, TDItineraryManager.Current.Length,
				"Before Delete - Itinerary is of incorrect length.");
			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == secondExtensionResult,
				"Before Delete - First Itinerary Segment does not match Second Extension");
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"Before Delete - Second Itinerary Segment does not match First Extension");
			TDItineraryManager.Current.SelectedItinerarySegment = 2;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
				"Before Delete - Third Itinerary Segment does not match Original Result");

			TDItineraryManager.Current.DeleteLastExtension();

			// Check length of Itinerary and values of all elements after to Delete
			Assert.AreEqual(2, TDItineraryManager.Current.Length,
				"After Delete - Itinerary is of incorrect length.");
			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"After Delete - First Itinerary Segment does not match First Extension");
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
				"After Delete - Second Itinerary Segment does not match Original Result");
			TDItineraryManager.Current.SelectedItinerarySegment = 2;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == null,
				"After Delete - Third Itinerary Segment still exists");


			// Create 3rd journey extension on START of 1st extension
			TDJourneyResult thirdExtensionResult = GetExtend("ThirdExtension", false, true, 2, false);


			// Check length of Itinerary and values of all elements
			Assert.AreEqual(3, TDItineraryManager.Current.Length,
				"After re-add - Itinerary is of incorrect length.");

			TDItineraryManager.Current.SelectedItinerarySegment = 0;

			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == thirdExtensionResult,
				"After re-add - First Itinerary Segment does not match Third Extension");
			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.AreEqual(TDItineraryManager.Current.JourneyRequest.OriginLocation.Description, _prevLocation.Description,
				"After re-add - Destination of Third Extension Request does not match Origin of First Extension Request");

			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == firstExtensionResult,
				"After re-add - Second Itinerary Segment does not match First Extension");
			_prevLocation = TDItineraryManager.Current.JourneyRequest.DestinationLocation;
			TDItineraryManager.Current.SelectedItinerarySegment = 2;
			Assert.AreEqual(TDItineraryManager.Current.JourneyRequest.OriginLocation.Description, _prevLocation.Description,
				"After re-add - Destination of First Extension Request does not match Origin of Original Request");

			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == initialResult,
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
			Assert.AreEqual(3, TDItineraryManager.Current.Length,
				"Itinerary is of incorrect length.");

			TDItineraryManager.Current.NewSearch();

			// Check length of Itinerary after Clear
			Assert.AreEqual(0, TDItineraryManager.Current.Length,
				"Itinerary is of incorrect length.");

			Assert.IsTrue(TDItineraryManager.Current.ExtendInProgress == false,
				"ItineraryManager is still in 'ExtendInProgress' mode.");

			TDItineraryManager.Current.SelectedItinerarySegment = 0;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == null,
				"First Itinerary Segment still exists");
			TDItineraryManager.Current.SelectedItinerarySegment = 1;
			Assert.IsTrue( TDItineraryManager.Current.JourneyResult == null,
				"Second Itinerary Segment still exists");
			TDItineraryManager.Current.SelectedItinerarySegment = 2;
			Assert.IsTrue(TDItineraryManager.Current.JourneyResult == null,
				"Third Itinerary Segment still exists");
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
			Assert.AreEqual(3, TDItineraryManager.Current.Length,"Itinerary is of incorrect length.");

			TDItineraryManager.Current.ResetToInitialJourney();

			// Check length of Itinerary after Reset
			Assert.AreEqual(0, TDItineraryManager.Current.Length, "Itinerary is of incorrect length.");

			Assert.IsTrue(TDItineraryManager.Current.ExtendInProgress == false, "ItineraryManager is still in 'ExtendInProgress' mode.");

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
			Assert.AreEqual(3, TDItineraryManager.Current.Length, "Itinerary is of incorrect length.");

			TDItineraryManager.Current.ResetLastExtension();

			// Check Itinerary and Working Area after Reset
			Assert.AreEqual(2, TDItineraryManager.Current.Length, "Itinerary is of incorrect length.");
			Assert.IsTrue(TDItineraryManager.Current.ExtendInProgress, 
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

			TDLocation _origin = TDItineraryManager.Current.OutwardOriginLocation();
			TDLocation _destination = TDItineraryManager.Current.OutwardDestinationLocation();
			TDDateTime _depart = TDItineraryManager.Current.OutwardDepartDateTime();
			TDDateTime _arrive = TDItineraryManager.Current.OutwardArriveDateTime();

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

//		/// <summary>
//		/// Tests that the Origin and Destination details for the Return Itinerary can be retrieved
//		/// </summary>
//		[Test]
//		public void TestGetReturnItineraryOriginAndDestination()
//		{
//			// Create an initial journey in the Itinerary
//			ITDJourneyRequest initialRequest = CreateJourneyRequest("InitialJourney");
//			TDJourneyResult initialResult = CreateItinerary(true, true, 2);
//
//			// Create 1st extension on END of Itinerary
//			TDJourneyResult firstExtensionResult = GetExtend("FirstExtension", true, true, 2, true);
//
//			// Create 2nd extension on START of Itinerary
//			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", true, true, 2, false);
//
//			TDLocation _origin = TDItineraryManager.Current.ReturnOriginLocation();
//			TDLocation _destination = TDItineraryManager.Current.ReturnDestinationLocation();
//			TDDateTime _depart = TDItineraryManager.Current.ReturnDepartDateTime();
//			TDDateTime _arrive = TDItineraryManager.Current.ReturnArriveDateTime();
//
//			int _journeyNumFirstSeg = 5;	// Result 1 of 1st return extension
//			int _journeyNumLastSeg = 9;		// Result 1 of 2nd return extension
//
//			Assertion.AssertEquals("Origin location is not correct.", "MMMM name FirstExtension" + _journeyNumFirstSeg, _origin.Description);
//			Assertion.AssertEquals("Destination location is not correct.", "AAAA name SecondExtension" + _journeyNumLastSeg, _destination.Description);
//
//			Assertion.AssertEquals("Depart time is not correct.", timeNow.AddMinutes(180 + (30 * _journeyNumFirstSeg)).ToString(), _depart.GetDateTime().ToString());
//			Assertion.AssertEquals("Arrive time is not correct.", timeNow.AddMinutes(180).ToString(), _arrive.GetDateTime().ToString());
//		}

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
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", false, false, 2, false);

			JourneySummaryLine[] outwardItinerarySummary = TDItineraryManager.Current.OutwardItinerarySummary();
			JourneySummaryLine[] returnItinerarySummary = TDItineraryManager.Current.ReturnItinerarySummary();

			Assert.AreEqual(4, outwardItinerarySummary.Length, "Outward Summary is of incorrect length.");
			Assert.AreEqual(0, returnItinerarySummary.Length, "Return Summary is of incorrect length.");

			int _journeyNumFirstSeg = 5;	// Result 1 of 2nd extension
			int _journeyNumSecondSeg = 1;	// Result 1 of initial journey
			int _journeyNumLastSeg = 3;		// Result 1 of 1st extension

			Assert.AreEqual("AAAA name SecondExtension" + _journeyNumFirstSeg, outwardItinerarySummary[0].OriginDescription,
				"Full Itinerary Origin location is not correct.");
			Assert.AreEqual("MMMM name FirstExtension" + _journeyNumLastSeg, outwardItinerarySummary[0].DestinationDescription,
				"Full Itinerary Destination location is not correct.");
			Assert.AreEqual(timeNow.ToString(), outwardItinerarySummary[0].DepartureDateTime.GetDateTime().ToString(),
				"Full Itinerary Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _journeyNumLastSeg)).ToString(), outwardItinerarySummary[0].ArrivalDateTime.GetDateTime().ToString(),
				"Full Itinerary Arrive time is not correct.");
			Assert.AreEqual(2, outwardItinerarySummary[0].InterchangeCount,
				"Full Itinerary Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.Itinerary, outwardItinerarySummary[0].Type,
				"Full Itinerary Journey Type is not correct.");

			Assert.AreEqual("AAAA name SecondExtension" + _journeyNumFirstSeg, outwardItinerarySummary[1].OriginDescription,
				"1st Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name InitialJourney1", outwardItinerarySummary[1].DestinationDescription,
				"1st Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.ToString(), outwardItinerarySummary[1].DepartureDateTime.GetDateTime().ToString(),
				"1st Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(25).ToString(), outwardItinerarySummary[1].ArrivalDateTime.GetDateTime().ToString(),
				"1st Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardItinerarySummary[1].InterchangeCount,
				"1st Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.RoadCongested, outwardItinerarySummary[1].Type,
				"1st Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name InitialJourney" + _journeyNumSecondSeg, outwardItinerarySummary[2].OriginDescription,
				"2nd Segment Origin location is not correct.");
			Assert.AreEqual("MMMM name InitialJourney" + _journeyNumSecondSeg, outwardItinerarySummary[2].DestinationDescription,
				"2nd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(30 * _journeyNumSecondSeg).ToString(), outwardItinerarySummary[2].DepartureDateTime.GetDateTime().ToString(),
				"2nd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _journeyNumSecondSeg)).ToString(), outwardItinerarySummary[2].ArrivalDateTime.GetDateTime().ToString(),
				"2nd Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardItinerarySummary[2].InterchangeCount,
				"2nd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, outwardItinerarySummary[2].Type,
				"2nd Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name FirstExtension" + _journeyNumLastSeg, outwardItinerarySummary[3].OriginDescription,
				"3rd Segment Origin location is not correct.");
			Assert.AreEqual("MMMM name FirstExtension" + _journeyNumLastSeg, outwardItinerarySummary[3].DestinationDescription,
				"3rd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(30 * _journeyNumLastSeg).ToString(), outwardItinerarySummary[3].DepartureDateTime.GetDateTime().ToString(),
				"3rd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _journeyNumLastSeg)).ToString(), outwardItinerarySummary[3].ArrivalDateTime.GetDateTime().ToString(),
				"3rd Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardItinerarySummary[3].InterchangeCount,
				"3rd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, outwardItinerarySummary[3].Type,
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
			TDJourneyResult secondExtensionResult = GetExtend("SecondExtension", true, false, 2, false);

			JourneySummaryLine[] outwardItinerarySummary = TDItineraryManager.Current.OutwardItinerarySummary();
			JourneySummaryLine[] returnItinerarySummary = TDItineraryManager.Current.ReturnItinerarySummary();

			Assert.AreEqual(4, outwardItinerarySummary.Length, "Outward Summary is of incorrect length.");
			Assert.AreEqual(4, returnItinerarySummary.Length, "Return Summary is of incorrect length.");

			int _outwardJourneyNumFirstSeg = 9;		// Result 1 of 2nd outward extension
			int _outwardJourneyNumSecondSeg = 1;	// Result 1 of initial outward journey
			int _outwardJourneyNumLastSeg = 5;		// Result 1 of 1st outward extension
			int _returnJourneyNumFirstSeg = 7;		// Result 1 of 1st return extension
			int _returnJourneyNumSecondSeg = 3;		// Result 1 of initial return journey
			int _returnJourneyNumLastSeg = 9;		// Result 1 of 2nd return extension

			Assert.AreEqual("AAAA name SecondExtension" + _outwardJourneyNumFirstSeg, outwardItinerarySummary[0].OriginDescription,
				"Outward Full Itinerary Origin location is not correct.");
			Assert.AreEqual("MMMM name FirstExtension" + _outwardJourneyNumLastSeg, outwardItinerarySummary[0].DestinationDescription,
				"Outward Full Itinerary Destination location is not correct.");
			Assert.AreEqual(timeNow.ToString(), outwardItinerarySummary[0].DepartureDateTime.GetDateTime().ToString(),
				"Outward Full Itinerary Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _outwardJourneyNumLastSeg)).ToString(), outwardItinerarySummary[0].ArrivalDateTime.GetDateTime().ToString(),
				"Outward Full Itinerary Arrive time is not correct.");
			Assert.AreEqual(2, outwardItinerarySummary[0].InterchangeCount,
				"Outward Full Itinerary Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.Itinerary, outwardItinerarySummary[0].Type,
				"Outward Full Itinerary Journey Type is not correct.");

			Assert.AreEqual("MMMM name FirstExtension" + _returnJourneyNumFirstSeg, returnItinerarySummary[0].OriginDescription,
				"Return Full Itinerary Origin location is not correct.");
			Assert.AreEqual("AAAA name SecondExtension" + _returnJourneyNumLastSeg, returnItinerarySummary[0].DestinationDescription,
				"Return Full Itinerary Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (30 * _returnJourneyNumFirstSeg)).ToString(), returnItinerarySummary[0].DepartureDateTime.GetDateTime().ToString(),
				"Return Full Itinerary Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180).ToString(), returnItinerarySummary[0].ArrivalDateTime.GetDateTime().ToString(),
				"Return Full Itinerary Arrive time is not correct.");
			Assert.AreEqual(2, returnItinerarySummary[0].InterchangeCount,
				"Return Full Itinerary Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.Itinerary, returnItinerarySummary[0].Type,
				"Return Full Itinerary Journey Type is not correct.");

			Assert.AreEqual("AAAA name SecondExtension" + _outwardJourneyNumFirstSeg, outwardItinerarySummary[1].OriginDescription,
				"Outward 1st Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name InitialJourney1", outwardItinerarySummary[1].DestinationDescription,
				"Outward 1st Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.ToString(), outwardItinerarySummary[1].DepartureDateTime.GetDateTime().ToString(),
				"Outward 1st Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(25).ToString(), outwardItinerarySummary[1].ArrivalDateTime.GetDateTime().ToString(),
				"Outward 1st Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardItinerarySummary[1].InterchangeCount,
				"Outward 1st Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.RoadCongested, outwardItinerarySummary[1].Type,
				"Outward 1st Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name InitialJourney" + _outwardJourneyNumSecondSeg, outwardItinerarySummary[2].OriginDescription,
				"Outward 2nd Segment Origin location is not correct.");
			Assert.AreEqual("MMMM name InitialJourney" + _outwardJourneyNumSecondSeg, outwardItinerarySummary[2].DestinationDescription,
				"Outward 2nd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(30 * _outwardJourneyNumSecondSeg).ToString(), outwardItinerarySummary[2].DepartureDateTime.GetDateTime().ToString(),
				"Outward 2nd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _outwardJourneyNumSecondSeg)).ToString(), outwardItinerarySummary[2].ArrivalDateTime.GetDateTime().ToString(),
				"Outward 2nd Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardItinerarySummary[2].InterchangeCount,
				"Outward 2nd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, outwardItinerarySummary[2].Type,
				"Outward 2nd Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name FirstExtension" + _outwardJourneyNumLastSeg, outwardItinerarySummary[3].OriginDescription,
				"Outward 3rd Segment Origin location is not correct.");
			Assert.AreEqual("MMMM name FirstExtension" + _outwardJourneyNumLastSeg, outwardItinerarySummary[3].DestinationDescription,
				"Outward 3rd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(30 * _outwardJourneyNumLastSeg).ToString(), outwardItinerarySummary[3].DepartureDateTime.GetDateTime().ToString(),
				"Outward 3rd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(20 + (30 * _outwardJourneyNumLastSeg)).ToString(), outwardItinerarySummary[3].ArrivalDateTime.GetDateTime().ToString(),
				"Outward 3rd Segment Arrive time is not correct.");
			Assert.AreEqual(0, outwardItinerarySummary[3].InterchangeCount,
				"Outward 3rd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, outwardItinerarySummary[3].Type,
				"Outward 3rd Segment Journey Type is not correct.");

			Assert.AreEqual("MMMM name FirstExtension" + _returnJourneyNumFirstSeg, returnItinerarySummary[1].OriginDescription,
				"Return 1st Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name FirstExtension" + _returnJourneyNumFirstSeg, returnItinerarySummary[1].DestinationDescription,
				"Return 1st Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (30 * _returnJourneyNumFirstSeg)).ToString(), returnItinerarySummary[1].DepartureDateTime.GetDateTime().ToString(),
				"Return 1st Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (20 + (30 * _returnJourneyNumFirstSeg))).ToString(), returnItinerarySummary[1].ArrivalDateTime.GetDateTime().ToString(),
				"Return 1st Segment Arrive time is not correct.");
			Assert.AreEqual(0, returnItinerarySummary[1].InterchangeCount,
				"Return 1st Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, returnItinerarySummary[1].Type,
				"Return 1st Segment Journey Type is not correct.");

			Assert.AreEqual("MMMM name InitialJourney" + _returnJourneyNumSecondSeg, returnItinerarySummary[2].OriginDescription,
				"Return 2nd Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name InitialJourney" + _returnJourneyNumSecondSeg, returnItinerarySummary[2].DestinationDescription,
				"Return 2nd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (30 * _returnJourneyNumSecondSeg)).ToString(), returnItinerarySummary[2].DepartureDateTime.GetDateTime().ToString(),
				"Return 2nd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180 + (20 + (30 * _returnJourneyNumSecondSeg))).ToString(), returnItinerarySummary[2].ArrivalDateTime.GetDateTime().ToString(),
				"Return 2nd Segment Arrive time is not correct.");
			Assert.AreEqual(0, returnItinerarySummary[2].InterchangeCount,
				"Return 2nd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.PublicOriginal, returnItinerarySummary[2].Type,
				"Return 2nd Segment Journey Type is not correct.");

			Assert.AreEqual("AAAA name InitialJourney1", returnItinerarySummary[3].OriginDescription,
				"Return 3rd Segment Origin location is not correct.");
			Assert.AreEqual("AAAA name SecondExtension" + _returnJourneyNumLastSeg, returnItinerarySummary[3].DestinationDescription,
				"Return 3rd Segment Destination location is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180-25).ToString(), returnItinerarySummary[3].DepartureDateTime.GetDateTime().ToString(),
				"Return 3rd Segment Depart time is not correct.");
			Assert.AreEqual(timeNow.AddMinutes(180).ToString(), returnItinerarySummary[3].ArrivalDateTime.GetDateTime().ToString(),
				"Return 3rd Segment Arrive time is not correct.");
			Assert.AreEqual(0, returnItinerarySummary[3].InterchangeCount,
				"Return 3rd Segment Interchange Count is not correct.");
			Assert.AreEqual(TDJourneyType.RoadCongested, returnItinerarySummary[3].Type,
				"Return 3rd Segment Journey Type is not correct.");
		}


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
		/// <param name="requestID">String used to enable Request to be identified in Assertions</param>
		private void CompleteJourneyRequest(string requestID)
		{
			timeNow = DateTime.Now;

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
				TDSessionManager.Current.JourneyRequest.OriginLocation = new TDLocation();
				TDSessionManager.Current.JourneyRequest.OriginLocation.Description = "AAAA name " + requestID + journeyNum;
			}

			if (TDSessionManager.Current.JourneyRequest.DestinationLocation == null)
			{
				TDSessionManager.Current.JourneyRequest.DestinationLocation = new TDLocation();
				TDSessionManager.Current.JourneyRequest.DestinationLocation.Description = "MMMM name " + requestID + journeyNum;
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
			// Create journey result
			TDJourneyResult thisResult = new TDJourneyResult(1234);
			JourneyResult cjpResult = CreateCJPResult(false, publicMode, journeyID, resultCount );
			thisResult.AddResult(cjpResult, true, null, null, null, "ssss");
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
				thisResult.AddResult(cjpResult, false, null, null, null, "ssss");
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

			TDItineraryManager.Current.CreateItinerary();

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
				_successful = TDItineraryManager.Current.ExtendFromItineraryEndPoint();
			}
			else
			{
				_successful = TDItineraryManager.Current.ExtendToItineraryStartPoint();
			}

			CompleteJourneyRequest(journeyID);
			TDJourneyResult thisResult = GetResult(journeyID, includeReturn, publicMode, resultCount);

			TDItineraryManager.Current.AddExtensionToItinerary();

			return thisResult;
		}
	}
}
