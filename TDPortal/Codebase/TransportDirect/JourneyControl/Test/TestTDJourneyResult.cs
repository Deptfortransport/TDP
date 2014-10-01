// *********************************************** 
// NAME			: TestTDJourneyResult.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestTDJourneyResult class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestTDJourneyResult.cs-arc  $
//
//   Rev 1.2   Dec 05 2012 14:12:50   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Oct 12 2009 09:11:00   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 12 2009 08:39:46   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Nov 08 2007 12:24:20   mturner
//Initial revision.
//
//   Rev 1.28   Mar 30 2006 15:58:42   mguney
//TDJourneyResult construction changed in  TestOutwardRoadJourney and TestReturnRoadJourney.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.27   Mar 30 2006 13:31:58   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.26   Mar 14 2006 10:41:10   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.25   Feb 09 2006 17:49:02   jmcallister
//Project Newkirk
//
//   Rev 1.24.1.1   Mar 02 2006 17:40:44   NMoorhouse
//Extra param to hold private via location
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24.1.0   Jan 26 2006 20:15:28   rhopkins
//Pass new attributes in constructors for TDJourneyResult
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24   Aug 24 2005 16:06:54   RPhilpott
//Make tests consistent with changed TDJourneyResult.AddResult() and PublicJourney ctor. 
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.23   May 11 2005 14:37:30   rscott
//Changes for code review
//
//   Rev 1.22   Mar 23 2005 15:22:22   rhopkins
//Fixed FxCop "warnings"
//
//   Rev 1.21   Mar 01 2005 16:56:28   rscott
//Del 7 Cost Based Searcg Incremental Design Changes
//
//   Rev 1.20   Feb 23 2005 16:41:36   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.19   Jan 26 2005 15:53:00   PNorell
//Support for partitioning the session information.
//
//   Rev 1.18   Jan 18 2005 15:56:32   rhopkins
//Removed dependancy on other tests so that this test will work when TestCarCostCalculator is present.
//Also changed Assertion to Assert.
//
//   Rev 1.17   Nov 26 2004 13:52:00   jbroome
//DEL6.3.1. Motorway Junctions enhancements
//
//   Rev 1.16   Oct 27 2004 11:16:28   jbroome
//Removed references to Toll property of CJP DriveSection class for DEL 7 CJP interface.
//
//   Rev 1.15   Sep 17 2004 15:13:06   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.14   Jul 28 2004 10:54:08   CHosegood
//Updated to compile against CJP 6.0.0.0
//NOT TESTED!!!
//
//   Rev 1.13   Jun 09 2004 15:09:48   JHaydock
//Update/fix for TDJourneyResult and unit test regarding GetOperatorNames method
//
//   Rev 1.12   Nov 06 2003 16:27:24   PNorell
//Ensured test work properly.
//
//   Rev 1.11   Oct 15 2003 21:56:18   acaunt
//Destinations added to the leg data.
//
//Call to TestJourneyInitialiser added
//
//   Rev 1.10   Oct 15 2003 13:30:14   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.9   Sep 25 2003 11:44:42   RPhilpott
//Map handoff and MI logging changes
//
//   Rev 1.8   Sep 23 2003 14:06:30   RPhilpott
//Changes to TDJourneyResult (ctor and referenceNumber)
//
//   Rev 1.7   Sep 10 2003 11:13:58   RPhilpott
//Changes to CJPMessage handling.
//
//   Rev 1.6   Sep 02 2003 12:43:36   kcheung
//Updated becase TDJourneyResult interface Road methods were updated.  Updated tests
//
//   Rev 1.5   Sep 01 2003 16:28:46   jcotton
//Updated: RouteNum
//
//   Rev 1.4   Aug 29 2003 11:46:06   kcheung
//Added Tests for: Outward/Return PublicJourney methods and Outward/Return RoadJourney methods.
//
//   Rev 1.3   Aug 28 2003 17:59:14   kcheung
//Updated because TDJourneyResult does not have property to return the Journey[] anymore
//
//   Rev 1.2   Aug 27 2003 11:06:06   kcheung
//Added Tests for the Journey Summary Lines.
//
//   Rev 1.1   Aug 26 2003 17:36:44   kcheung
//Updated tests to include summary line - working version.
//
//   Rev 1.0   Aug 20 2003 17:55:32   AToner
//Initial Revision
using System;
using NUnit.Framework;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestTDJourneyResult.
	/// </summary>
	[TestFixture]
	public class TestTDJourneyResult
	{
		public TestTDJourneyResult()
		{
		}

		[SetUp]
		public void SetUp()
		{
			// Initialise services
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init( new TestJourneyInitialisation() );
		}

		[Test]
		public void TestPublicJourney()
		{
			
			// Populate a dummy CJP request
			JourneyResult cjpResult = CreateCJPResult( true );
			//
			// Pass this as the outward journey (The property does not return an array but an actual Journey)
			TDJourneyResult result = new TDJourneyResult(1234);
			result.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			
			// Pass the same result as the return journey
			result.AddResult(cjpResult, false, null, null, null, null, "ssss", false, -1);
			
			//Assert.IsNotNull( result.ReturnPublicJourney, "ReturnJourney" );

			//Assert.AreEqual( 2, result.OutwardPublicJourney.Length, "OutwardPublicJourney count" );
			//Assert.AreEqual( 2, result.ReturnPublicJourney.Length, "ReturnPublicJourney count" );

			
		}

		[Test]
		public void TestPrivateJourney()
		{
			//
			// Populate a dummy CJP request
			JourneyResult cjpResult = CreateCJPResult( false );
			//
			// Pass this as the outward journey
			TDJourneyResult result = new TDJourneyResult(1234, 0, new TDDateTime(2003, 6, 5, 3, 4, 0), new TDDateTime(2003, 6, 5, 3, 4, 0), false, false, false);
			result.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			
			//Assert.IsNull( result.ReturnRoadJourney, "NoReturnJourney" );
			//
			// Pass the same result as the return journey
			result.AddResult(cjpResult, false, null, null, null, null, "ssss", false, -1);
			
			//Assert.IsNotNull( result.ReturnRoadJourney, "ReturnJourney" );

			//Assert.AreEqual( 2, result.OutwardRoadJourney.Length, "OutwardRoadJourney count" );
			//Assert.AreEqual( 2, result.ReturnRoadJourney.Length, "ReturnRoadJourney count" );
		}

		/// <summary>
		/// Test that the correct outward journey summary lines are returned.
		/// </summary>
		[Test]		
		public void TestOutwardJourneySummaryResult()
		{
			// Populate a dummy CJP request
			JourneyResult cjpResult = CreateCJPResult2();

			// Start time Outward
			TDDateTime startO = new DateTime(2003, 7, 2, 16, 5, 12);
			// Start time Return
			TDDateTime startR = new DateTime(2003, 7, 2, 16, 5, 12);

			// Pass this as the outward journey
			TDJourneyResult result = new TDJourneyResult(1234, 0, startO , startR, false, false, false);

			result.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);

            result.AmendedOutwardPublicJourney = new PublicJourney(1, CreatePublicJourney2(), null, null, null, TDJourneyType.PublicAmended, false, 0);

			// Get the outward Journey Summary Line for the result
			JourneySummaryLine[] summaries = result.OutwardJourneySummary(true);

			// Perform Assertions

			// Expect 3 public journeys, 1 amended public journey and 1 private
			Assert.AreEqual(5, summaries.Length);

			// Test the sort order - "Arrive Before" flag is set to TRUE so expect journeys
			// to be sorted by reverse order of departure time (latest departure first)

			JourneySummaryLine summary = summaries[0];

			Assert.AreEqual(new TDDateTime(2003, 9, 16, 13, 12, 09), summary.DepartureDateTime);
			Assert.AreEqual(new TDDateTime(2003, 9, 16, 15, 18, 25), summary.ArrivalDateTime);
			Assert.AreEqual("1", summary.DisplayNumber);
			
			// Get the modes that were used in this journey.
			ModeType[] modesUsed = summary.Modes;

			// Expect 3 modes to be used
			Assert.AreEqual(3, modesUsed.Length);

			ArrayList modesUsedList = new ArrayList();
			modesUsedList.Add(modesUsed[0]);
			modesUsedList.Add(modesUsed[1]);
			modesUsedList.Add(modesUsed[2]);

			// Check that the modes that we expect in the leg exist in the array list
			Assert.IsTrue(modesUsedList.Contains(ModeType.Walk));
			Assert.IsTrue(modesUsedList.Contains(ModeType.Metro));
			Assert.IsTrue(modesUsedList.Contains(ModeType.Underground));

			// Expect only 1 interchange for this leg (Metro to Underground)
			Assert.AreEqual(1, summary.InterchangeCount);

			Assert.AreEqual(TDJourneyType.PublicOriginal, summary.Type);
			Assert.AreEqual(0, summary.RoadMiles);

			// Test the next summary line
			summary = summaries[1];

			// Expect the next journey to be the amended journey
			Assert.AreEqual(TDJourneyType.PublicAmended, summary.Type);

			Assert.AreEqual(new TDDateTime(2003, 6, 5, 3, 4, 0), summary.DepartureDateTime);
			Assert.AreEqual(new TDDateTime(2003, 6, 5, 3, 14, 0), summary.ArrivalDateTime);
			Assert.IsTrue(summary.Modes.Length == 1);
			
			ArrayList amendModesUsed = new ArrayList();
			amendModesUsed.Add(summary.Modes[0]);
			Assert.IsTrue(amendModesUsed.Contains(ModeType.Tram));
			Assert.AreEqual("1a", summary.DisplayNumber);
			Assert.AreEqual(0, summary.RoadMiles);

			// Expect no interchanges since there is only 1 leg
			Assert.AreEqual(0, summary.InterchangeCount);

			// Test the next summary
			summary = summaries[2];

			// If the sort has correctly executed then
			// expect the next public journey to depart on 29th August 2003.
			
			Assert.AreEqual(new TDDateTime(2003, 8, 29, 13, 12, 9), summary.DepartureDateTime);
			Assert.AreEqual(new TDDateTime(2003, 8, 29, 13, 42, 19), summary.ArrivalDateTime);
			
			// Expect this journey to have only 1 mode - Ferry
			Assert.AreEqual(1, summary.Modes.Length);
			Assert.AreEqual(ModeType.Ferry, summary.Modes[0]);
			Assert.AreEqual(0, summary.InterchangeCount);
			Assert.AreEqual("2", summary.DisplayNumber);

			Assert.AreEqual(0, summary.RoadMiles);

			Assert.AreEqual(TDJourneyType.PublicOriginal, summary.Type);

			// ----

			// Test the next public journey

			summary = summaries[3];

			// expect the next public journey to depart on 29th August 2003.
			
			Assert.AreEqual(new TDDateTime(2003, 8, 17, 13, 12, 9), summary.DepartureDateTime);
			Assert.AreEqual(new TDDateTime(2003, 8, 17, 14, 48, 25), summary.ArrivalDateTime);
			
			// Expect this journey to have only 2 modes - Tram and Rail
			Assert.AreEqual(2, summary.Modes.Length);

			ArrayList temp = new ArrayList();
			temp.Add(summary.Modes[0]);
			temp.Add(summary.Modes[1]);

			// Test the modes
			Assert.IsTrue(temp.Contains(ModeType.Tram));
			Assert.IsTrue(temp.Contains(ModeType.Rail));

			Assert.AreEqual(1, summary.InterchangeCount);
			Assert.AreEqual("3", summary.DisplayNumber);

			Assert.AreEqual(0, summary.RoadMiles);

			Assert.AreEqual(TDJourneyType.PublicOriginal, summary.Type);

			// ---------

			// Test the private journey

			summary = summaries[4];

			Assert.AreEqual(TDJourneyType.RoadCongested, summary.Type);

			// Since it is arrive before, the starting time should be 20 minutes in advance of arrival
			TDDateTime carStart = startO.Add( new TimeSpan(0,0,-20,0) );
			Assert.AreEqual(carStart, summary.DepartureDateTime);
			Assert.AreEqual(startO , summary.ArrivalDateTime );

			Assert.AreEqual(123, summary.RoadMiles);
			
			Assert.AreEqual(1, summary.Modes.Length);
			Assert.AreEqual(ModeType.Car, summary.Modes[0]);
			Assert.AreEqual(0, summary.InterchangeCount);

			Assert.AreEqual("4", summary.DisplayNumber);


			// Test the sort by passing false as the parameter for "Arrive Before"
			JourneyResult cjpResult2 = CreateCJPResult2();

			// Pass this as the outward journey
			TDJourneyResult result2 = new TDJourneyResult(1234, 0, new TDDateTime(2003, 6, 5, 3, 4, 0), new TDDateTime(2003, 6, 5, 3, 4, 0), false, false, false);
			result2.AddResult(cjpResult2, true, null, null, null, null, "ssss", false, -1);
			
			// Get the outward Journey Summary Line for the result
			JourneySummaryLine[] summaries2 = result2.OutwardJourneySummary(false);
			
			// Expect length 4 since there was no amended journey this time.
			Assert.AreEqual(4, summaries2.Length);

			// Expect the public journeys to be ordered by arrival time.
			Assert.AreEqual(new TDDateTime(2003, 8, 17, 14, 48, 25), summaries2[0].ArrivalDateTime);
			Assert.AreEqual(new TDDateTime(2003, 8, 29, 13, 42, 19), summaries2[1].ArrivalDateTime);
			Assert.AreEqual(new TDDateTime(2003, 9, 16, 15, 18, 25), summaries2[2].ArrivalDateTime);

		}

		/// <summary>
		/// Test that the correct Return Journey Summary Lines are correctly returned.
		/// </summary>
		
		[Test]		
		public void TestReturnJourneySummaryResult()
		{
			// Populate a dummy CJP request
			JourneyResult cjpResult = CreateCJPResult2();

			// Start time Outward
			TDDateTime startO = new TDDateTime(2003, 6, 5, 3, 4, 0);
			// Start time Return
			TDDateTime startR = new TDDateTime(2003, 6, 5, 3, 4, 0);

			// Pass this as the return journey
			TDJourneyResult result = new TDJourneyResult(1234, 0, startO , startR, false, false, false);
			result.AddResult(cjpResult, false, null, null, null, null, "ssss", false, -1);
            result.AmendedReturnPublicJourney = new PublicJourney(1, CreatePublicJourney2(), null, null, null, TDJourneyType.PublicAmended, false, 0);

			// Get the outward Journey Summary Line for the result
			JourneySummaryLine[] summaries = result.ReturnJourneySummary(true);

			// Perform Assertions

			// Expect 3 public journeys, 1 amended public journey and 1 private
			Assert.AreEqual(5, summaries.Length);

			// Test the sort order - "Arrive Before" flag is set to TRUE so expect journeys
			// to be sorted by reverse order of departure time (latest departure first)

			JourneySummaryLine summary = summaries[0];

			Assert.AreEqual(new TDDateTime(2003, 9, 16, 13, 12, 09), summary.DepartureDateTime);
			Assert.AreEqual(new TDDateTime(2003, 9, 16, 15, 18, 25), summary.ArrivalDateTime);
			Assert.AreEqual("1", summary.DisplayNumber);
			
			// Get the modes that were used in this journey.
			ModeType[] modesUsed = summary.Modes;

			// Expect 3 modes to be used
			Assert.AreEqual(3, modesUsed.Length);

			ArrayList modesUsedList = new ArrayList();
			modesUsedList.Add(modesUsed[0]);
			modesUsedList.Add(modesUsed[1]);
			modesUsedList.Add(modesUsed[2]);

			// Check that the modes that we expect in the leg exist in the array list
			Assert.IsTrue(modesUsedList.Contains(ModeType.Walk));
			Assert.IsTrue(modesUsedList.Contains(ModeType.Metro));
			Assert.IsTrue(modesUsedList.Contains(ModeType.Underground));

			// Expect only 1 interchange for this leg (Metro to Underground)
			Assert.AreEqual(1, summary.InterchangeCount);

			Assert.AreEqual(TDJourneyType.PublicOriginal, summary.Type);
			Assert.AreEqual(0, summary.RoadMiles);

			// Test the next summary line
			summary = summaries[1];

			// Expect the next journey to be the amended journey
			Assert.AreEqual(TDJourneyType.PublicAmended, summary.Type);

			Assert.AreEqual(new TDDateTime(2003, 6, 5, 3, 4, 0), summary.DepartureDateTime);
			Assert.AreEqual(new TDDateTime(2003, 6, 5, 3, 14, 0), summary.ArrivalDateTime);
			Assert.IsTrue(summary.Modes.Length == 1);
			
			ArrayList amendModesUsed = new ArrayList();
			amendModesUsed.Add(summary.Modes[0]);
			Assert.IsTrue(amendModesUsed.Contains(ModeType.Tram));
			Assert.AreEqual("1a", summary.DisplayNumber);
			Assert.AreEqual(0, summary.RoadMiles);

			// Expect no interchanges since there is only 1 leg
			Assert.AreEqual(0, summary.InterchangeCount);

			// Test the next summary
			summary = summaries[2];

			// If the sort has correctly executed then
			// expect the next public journey to depart on 29th August 2003.
			
			Assert.AreEqual(new TDDateTime(2003, 8, 29, 13, 12, 9), summary.DepartureDateTime);
			Assert.AreEqual(new TDDateTime(2003, 8, 29, 13, 42, 19), summary.ArrivalDateTime);
			
			// Expect this journey to have only 1 mode - Ferry
			Assert.AreEqual(1, summary.Modes.Length);
			Assert.AreEqual(ModeType.Ferry, summary.Modes[0]);
			Assert.AreEqual(0, summary.InterchangeCount);
			Assert.AreEqual("2", summary.DisplayNumber);

			Assert.AreEqual(0, summary.RoadMiles);

			Assert.AreEqual(TDJourneyType.PublicOriginal, summary.Type);

			// ----

			// Test the next public journey

			summary = summaries[3];

			// expect the next public journey to depart on 29th August 2003.
			
			Assert.AreEqual(new TDDateTime(2003, 8, 17, 13, 12, 9), summary.DepartureDateTime);
			Assert.AreEqual(new TDDateTime(2003, 8, 17, 14, 48, 25), summary.ArrivalDateTime);
			
			// Expect this journey to have only 2 modes - Tram and Rail
			Assert.AreEqual(2, summary.Modes.Length);

			ArrayList temp = new ArrayList();
			temp.Add(summary.Modes[0]);
			temp.Add(summary.Modes[1]);

			// Test the modes
			Assert.IsTrue(temp.Contains(ModeType.Tram));
			Assert.IsTrue(temp.Contains(ModeType.Rail));

			Assert.AreEqual(1, summary.InterchangeCount);
			Assert.AreEqual("3", summary.DisplayNumber);

			Assert.AreEqual(0, summary.RoadMiles);

			Assert.AreEqual(TDJourneyType.PublicOriginal, summary.Type);

			// ---------

			// Test the private journey

			summary = summaries[4];

			Assert.AreEqual(TDJourneyType.RoadCongested, summary.Type);

			TDDateTime carStart = startR.Add( new TimeSpan(0,0,-20,0) );

			Assert.AreEqual(carStart, summary.DepartureDateTime);
			Assert.AreEqual( startR, summary.ArrivalDateTime);

			Assert.AreEqual(123, summary.RoadMiles);
			
			Assert.AreEqual(1, summary.Modes.Length);
			Assert.AreEqual(ModeType.Car, summary.Modes[0]);
			Assert.AreEqual(0, summary.InterchangeCount);

			Assert.AreEqual("4", summary.DisplayNumber);


			// Test the sort by passing false as the parameter for "Arrive Before"
			JourneyResult cjpResult2 = CreateCJPResult2();

			// Pass this as the outward journey
			TDJourneyResult result2 = new TDJourneyResult(1234, 0, startO , startR, false, false, false);
			result2.AddResult(cjpResult2, false, null, null, null, null, "ssss", false, -1);
			
			// Get the outward Journey Summary Line for the result
			JourneySummaryLine[] summaries2 = result2.ReturnJourneySummary(false);
			
			// Expect length 4 since there was no amended journey this time.
			Assert.AreEqual(4, summaries2.Length);

			// Expect the public journeys to be ordered by arrival time.

			Assert.AreEqual(new TDDateTime(2003, 8, 17, 14, 48, 25), summaries2[0].ArrivalDateTime);
			Assert.AreEqual(new TDDateTime(2003, 8, 29, 13, 42, 19), summaries2[1].ArrivalDateTime);
			Assert.AreEqual(new TDDateTime(2003, 9, 16, 15, 18, 25), summaries2[2].ArrivalDateTime);
		}

		/// <summary>
		/// Test that the Outward Public Journey method returns the correct PublicJourney
		/// </summary>
		[Test]
		public void TestOutwardPublicJourney()
		{
			// Create a new TDJourneyResult
			// Populate a dummy CJP request
			JourneyResult cjpResult = CreateCJPResult( true );
			//
			// Pass this as the outward journey
			TDJourneyResult result = new TDJourneyResult(1234);
			result.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);

			// Get the Journey Summary Lines from the result
			JourneySummaryLine[] summaryLines = result.OutwardJourneySummary(true);

			// Create an array of all the journey indexes
			int[] journeyIndexes = new int[summaryLines.Length];

			// populate the journey index array
			for(int i=0; i<journeyIndexes.Length; i++)
			{
				journeyIndexes[i] = summaryLines[i].JourneyIndex;
			}

			// Test the OutwardPublicJourney method to ensure that
			// the correct Public Journey is returned given a journey index
			// (from the array)

			foreach(int journeyIndex in journeyIndexes)
			{
				// Get the PublicJourney
				PublicJourney journey = result.OutwardPublicJourney(journeyIndex);

				// Test the journey index in journey matches the expected journey index
				Assert.AreEqual(journeyIndex, journey.JourneyIndex);
			}

		}

		/// <summary>
		/// Test that the Return Public Journey method returns the correct PublicJourney
		/// </summary>
		[Test]
		public void TestReturnPublicJourney()
		{
			// Create a new TDJourneyResult
			// Populate a dummy CJP request
			JourneyResult cjpResult = CreateCJPResult( true );
			//
			// Pass this as the return journey
			TDJourneyResult result = new TDJourneyResult(1234);
			result.AddResult(cjpResult, false, null, null, null, null, "ssss", false, -1);

			// Get the Journey Summary Lines from the result
			JourneySummaryLine[] summaryLines = result.ReturnJourneySummary(true);

			// Create an array of all the journey indexes
			int[] journeyIndexes = new int[summaryLines.Length];

			// populate the journey index array
			for(int i=0; i<journeyIndexes.Length; i++)
			{
				journeyIndexes[i] = summaryLines[i].JourneyIndex;
			}

			// Test the ReturnPublicJourney method to ensure that
			// the correct Public Journey is returned given a journey index
			// (from the array)

			foreach(int journeyIndex in journeyIndexes)
			{
				// Get the PublicJourney
				PublicJourney journey = result.ReturnPublicJourney(journeyIndex);

				// Test the journey index in journey matches the expected journey index
				Assert.AreEqual(journeyIndex, journey.JourneyIndex);
			}

		}

		/// <summary>
		/// Test that the Outward Road Journey method returns the correct Road Journey
		/// </summary>
		[Test]		
		public void TestOutwardRoadJourney()
		{
			TDJourneyResult result2 = new TDJourneyResult();
			Assert.IsNull(result2.OutwardRoadJourney());

			// Create a new TDJourneyResult
			// Populate a dummy CJP request
			JourneyResult cjpResult = CreateCJPResult2();

			// Pass this as the outward journey
			//TDJourneyResult result = new TDJourneyResult(1234);
            TDJourneyResult result = new TDJourneyResult(1234, 10, new TDDateTime(5, 5, 5), new TDDateTime(5, 5, 6), true, false, false);
			result.AddResult(cjpResult, true, null, null, null, null, "ssss",false, -1);

			RoadJourney roadJourney = result.OutwardRoadJourney();

			// the congested road journey should be returned.
			Assert.AreEqual(TDJourneyType.RoadCongested, roadJourney.Type);

			// test the start time to ensure that we have the correct road journey.
			// Assert.AreEqual(new TDDateTime(2003, 7, 2, 14, 1, 2), roadJourney.StartDateTime);

			// Get the freeflow road journey
			// roadJourney = result.OutwardRoadJourney(false);
			// Assert.AreEqual(TDJourneyType.RoadFreeFlow, roadJourney.Type);
			// Assert.AreEqual(new TDDateTime(2003, 9, 1, 13, 1, 2), roadJourney.StartDateTime);
		}

		/// <summary>
		/// Test that the Return Road Journey method returns the correct Road Journey
		/// </summary>
		[Test]
		public void TestReturnRoadJourney()
		{
			// Create a new TDJourneyResult
			// Populate a dummy CJP request
			
			// Tests that nulls are returned.
			JourneyResult cjpResult = CreateCJPResult2();

			TDJourneyResult result2 = new TDJourneyResult();
			Assert.IsNull(result2.ReturnRoadJourney());
			
			// Pass this as the outward journey
			//TDJourneyResult result = new TDJourneyResult(1234);
            TDJourneyResult result = new TDJourneyResult(1234, 10, new TDDateTime(5, 5, 5), new TDDateTime(5, 5, 6), true, false, false);
			result.AddResult(cjpResult, false, null, null, null, null, "ssss",false, -1);

			RoadJourney roadJourney = result.ReturnRoadJourney();

			// the congested road journey should be returned.
			Assert.AreEqual(TDJourneyType.RoadCongested, roadJourney.Type);


			// test the start time to ensure that we have the correct road journey.
			// Assert.AreEqual(new TDDateTime(2003, 7, 2, 14, 1, 2), roadJourney.StartDateTime);

			// Get the freeflow road journey
			// roadJourney = result.ReturnRoadJourney(false);
			// Assert.AreEqual(TDJourneyType.RoadFreeFlow, roadJourney.Type);
			// Assert.AreEqual(new TDDateTime(2003, 9, 1, 13, 1, 2), roadJourney.StartDateTime);

		}

		/// <summary>
		/// Tests that the TDJourneyResults conforms to the specifications of ITDSessionAware.
		/// </summary>
		[Test]
		public void TestITDSessionAware()
		{
			//
			// Populate a dummy CJP request
			JourneyResult cjpResult = CreateCJPResult( false );
			//
			// Pass this as the outward journey
            TDJourneyResult result = new TDJourneyResult(1234, 0, new TDDateTime(2003, 6, 5, 3, 4, 0), new TDDateTime(2003, 6, 5, 3, 4, 0), false, false, false);
			Assert.IsTrue( result.IsDirty , "Initial TDJourneyResult is not marked as dirty");
			result.IsDirty = false;
			Assert.IsFalse( result.IsDirty , "TDJourneyResult is marked as dirty after being marked !dirty");

			
			result.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			Assert.IsTrue( result.IsDirty , "Initial TDJourneyResult is not marked as dirty after data has been changed");
		}


		/// <summary>
		/// Test the static method GetOperatorNames
		/// </summary>
		[Test]
		public void TestGetOperatorNames()
		{
			string[] output;
			TDJourneyResult result;
			JourneyResult cjpResult;

			//result = new TDJourneyResult();
			output = TDJourneyResult.GetOperatorNames(null);
			Assert.AreEqual(0, output.Length);

			// Create a new TDJourneyResult
			// Populate a dummy CJP request
			cjpResult = CreateCJPResult2();
			// Pass this as the outward journey			
            result = new TDJourneyResult(1234, 10, new TDDateTime(5, 5, 5), new TDDateTime(5, 5, 6), true, false, false);
			result.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			output = TDJourneyResult.GetOperatorNames(result.OutwardPublicJourney(0));
			Assert.AreEqual("OperatorA", output[0]);
			Assert.AreEqual("OperatorB", output[1]);
			Assert.AreEqual("OperatorC", output[2]);
		}

		/// <summary>
		/// Test the AddPublicJourney and RemovePublicJourney methods
		/// </summary>
		[Test]
		public void TestAddRemovePublicJourney()
		{
			
			// Populate a dummy CJP request
			JourneyResult cjpResult = CreateCJPResult( true );
			//
			// Pass this as the outward journey (The property does not return an array but an actual Journey)
			TDJourneyResult result1 = new TDJourneyResult(1234);
			result1.AddResult(cjpResult, true, null, null, null, null, "ssss", false, -1);
			result1.AddResult(cjpResult, false, null, null, null, null, "ssss", false, -1);

			//create a PublicJourney to add
			PublicJourney opj = new PublicJourney();
			opj = result1.OutwardPublicJourney(0);	
	
			PublicJourney rpj = new PublicJourney();
			rpj = result1.ReturnPublicJourney(0);	

			//get the initial greatest index values for the tests
			int iMaxIndexOutward = (result1.OutwardPublicJourneyCount-1);
			int iMaxIndexReturn = (result1.ReturnPublicJourneyCount-1);

			int originalPublicJourneyArraySize = result1.OutwardPublicJourneyCount;

			//test PublicJourney Added to outwardPublicJourney arraylist
			result1.AddPublicJourney(opj,true);
			
			// not added because journey already exists in result 
			Assert.AreEqual(result1.OutwardPublicJourneyCount, originalPublicJourneyArraySize);

			// change mode and try again 
			
			opj = new PublicJourney();
			opj.Details = new PublicJourneyDetail[] { new PublicJourneyTimedDetail() };
			opj.Details[0].Mode = ModeType.Rail;
			opj.Details[0].LegStart = new PublicJourneyCallingPoint();
			opj.Details[0].LegStart.Location = new TDLocation();
			opj.Details[0].LegStart.Location.GridReference = new OSGridReference(111111, 222222);
			opj.Details[0].LegEnd = new PublicJourneyCallingPoint();
			opj.Details[0].LegEnd.Location = new TDLocation();
			opj.Details[0].LegEnd.Location.GridReference = new OSGridReference(111111, 222222);

			result1.AddPublicJourney(opj, true);
			
			Assert.AreEqual(result1.OutwardPublicJourneyCount, originalPublicJourneyArraySize + 1);
	
			originalPublicJourneyArraySize = result1.ReturnPublicJourneyCount;

			//test PublicJourneyAdded to returnPublicJourney arraylist
			result1.AddPublicJourney(rpj, false);
			
			// not added because journey already exists in result 
			Assert.AreEqual(result1.ReturnPublicJourneyCount, originalPublicJourneyArraySize);

			rpj = new PublicJourney();
			rpj.Details = new PublicJourneyDetail[] { new PublicJourneyTimedDetail() };
			rpj.Details[0].Mode = ModeType.Rail;
			rpj.Details[0].LegStart = new PublicJourneyCallingPoint();
			rpj.Details[0].LegStart.Location = new TDLocation();
			rpj.Details[0].LegStart.Location.GridReference = new OSGridReference(111111, 222222);
			rpj.Details[0].LegEnd = new PublicJourneyCallingPoint();
			rpj.Details[0].LegEnd.Location = new TDLocation();
			rpj.Details[0].LegEnd.Location.GridReference = new OSGridReference(111111, 222222);

			result1.AddPublicJourney(rpj,false);

			Assert.AreEqual(result1.ReturnPublicJourneyCount, originalPublicJourneyArraySize + 1);
			
			//check indexes
			PublicJourney topj = new PublicJourney();
			topj = result1.OutwardPublicJourney(result1.OutwardPublicJourneyCount-1);
			PublicJourney trpj = new PublicJourney();
			trpj = result1.ReturnPublicJourney(result1.ReturnPublicJourneyCount-1);

			Assert.AreEqual(topj.JourneyIndex,(iMaxIndexOutward+1),"outward public journey index wrong");
			Assert.AreEqual(trpj.JourneyIndex,(iMaxIndexReturn+1),"return public journey index wrong");

			//Test Removal of Public journey and reindexing
			if (result1.OutwardPublicJourneyCount > 0 && result1.ReturnPublicJourneyCount > 0)
			{
				result1.RemovePublicJourney(result1.OutwardPublicJourney(0), true);
				result1.RemovePublicJourney(result1.ReturnPublicJourney(0), false);
			}
			else
			{
				Assert.IsTrue(false,"Cannot remove Public Journey as no Public Journeys in array list");
			}

			//Check joureyindex is sequential
			//OutwardPublicJourneys
			int iJourneyIndex = 1;
			foreach (PublicJourney t1pj in result1.OutwardPublicJourneys)
			{
				Assert.AreEqual(t1pj.JourneyIndex,iJourneyIndex,"Incorrect indexing in OutwardPublicJourney");
				iJourneyIndex++;
			}
			//ReturnPublicJourneys
			iJourneyIndex = 1;
			foreach (PublicJourney t2pj in result1.ReturnPublicJourneys)
			{
				Assert.AreEqual(t2pj.JourneyIndex,iJourneyIndex,"Incorrect indexing in OutwardPublicJourney");
				iJourneyIndex++;
			}
		}

		#region "Helper Methods"

		/// <summary>
		/// Create CJP result containing public and private journeys. (Used for summary line testing)
		/// </summary>
		/// <returns></returns>
		private JourneyResult CreateCJPResult2()
		{
			JourneyResult result = new JourneyResult();

			result.publicJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[3];
			
			result.publicJourneys[0] = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			result.publicJourneys[0].legs = new Leg[2];

			result.publicJourneys[0].legs[0] = new TimedLeg();
			result.publicJourneys[0].legs[0].mode = ModeType.Tram;
			result.publicJourneys[0].legs[0].validated = true;

			result.publicJourneys[0].legs[0].board = new Event();
			result.publicJourneys[0].legs[0].board.activity = ActivityType.Depart;
			result.publicJourneys[0].legs[0].board.departTime = new DateTime(2003, 8, 17, 13, 12, 9);
			result.publicJourneys[0].legs[0].board.stop = new Stop();
			result.publicJourneys[0].legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[0].legs[0].board.stop.name = "Board name";

			result.publicJourneys[0].legs[0].alight = new Event();
			result.publicJourneys[0].legs[0].alight.activity = ActivityType.Arrive;
			result.publicJourneys[0].legs[0].alight.arriveTime = new DateTime (2003, 8, 17, 13, 42, 19);
			result.publicJourneys[0].legs[0].alight.stop = new Stop();
			result.publicJourneys[0].legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[0].legs[0].alight.stop.name = "Alight name";
			
			result.publicJourneys[0].legs[0].destination = new Event();
			result.publicJourneys[0].legs[0].destination.activity = ActivityType.Arrive;
			result.publicJourneys[0].legs[0].destination.arriveTime = new DateTime (2003, 8, 17, 13, 43, 19);
			result.publicJourneys[0].legs[0].destination.stop = new Stop();
			result.publicJourneys[0].legs[0].destination.stop.NaPTANID = "Destination NAPTANID";
			result.publicJourneys[0].legs[0].destination.stop.name = "Destination name";

			result.publicJourneys[0].legs[0].services = new Service[3];
			result.publicJourneys[0].legs[0].services[0] = new Service();
			result.publicJourneys[0].legs[0].services[1] = new Service();
			result.publicJourneys[0].legs[0].services[1].operatorName = null;
			result.publicJourneys[0].legs[0].services[2] = new Service();
			result.publicJourneys[0].legs[0].services[2].operatorName = "OperatorA";

			result.publicJourneys[0].legs[1] = new TimedLeg();
			result.publicJourneys[0].legs[1].mode = ModeType.Rail;
			result.publicJourneys[0].legs[1].validated = true;

			result.publicJourneys[0].legs[1].board = new Event();
			result.publicJourneys[0].legs[1].board.activity = ActivityType.Depart;
			result.publicJourneys[0].legs[1].board.departTime = new DateTime(2003, 8, 17, 13, 45, 19);
			result.publicJourneys[0].legs[1].board.stop = new Stop();
			result.publicJourneys[0].legs[1].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[0].legs[1].board.stop.name = "Board name";

			result.publicJourneys[0].legs[1].alight = new Event();
			result.publicJourneys[0].legs[1].alight.activity = ActivityType.Arrive;
			result.publicJourneys[0].legs[1].alight.arriveTime = new DateTime (2003, 8, 17, 14, 48, 25);
			result.publicJourneys[0].legs[1].alight.stop = new Stop();
			result.publicJourneys[0].legs[1].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[0].legs[1].alight.stop.name = "Alight name";

			result.publicJourneys[0].legs[1].destination = new Event();
			result.publicJourneys[0].legs[1].destination.activity = ActivityType.Arrive;
			result.publicJourneys[0].legs[1].destination.arriveTime = new DateTime (2003, 8, 17, 14, 51, 25);
			result.publicJourneys[0].legs[1].destination.stop = new Stop();
			result.publicJourneys[0].legs[1].destination.stop.NaPTANID = "Destination NAPTANID";
			result.publicJourneys[0].legs[1].destination.stop.name = "Destination name";

			result.publicJourneys[0].legs[1].services = new Service[3];
			result.publicJourneys[0].legs[1].services[0] = new Service();
			result.publicJourneys[0].legs[1].services[0].operatorName = "OperatorB";
			result.publicJourneys[0].legs[1].services[1] = new Service();
			result.publicJourneys[0].legs[1].services[2] = new Service();
			result.publicJourneys[0].legs[1].services[2].operatorName = "OperatorC";
			// ----

			result.publicJourneys[1] = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			result.publicJourneys[1].legs = new Leg[3];

			result.publicJourneys[1].legs[0] = new TimedLeg();
			result.publicJourneys[1].legs[0].mode = ModeType.Walk;
			result.publicJourneys[1].legs[0].validated = true;

			result.publicJourneys[1].legs[0].board = new Event();
			result.publicJourneys[1].legs[0].board.activity = ActivityType.Depart;
			result.publicJourneys[1].legs[0].board.departTime = new DateTime(2003, 9, 16, 13, 12, 9);
			result.publicJourneys[1].legs[0].board.stop = new Stop();
			result.publicJourneys[1].legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[1].legs[0].board.stop.name = "Board name";

			result.publicJourneys[1].legs[0].alight = new Event();
			result.publicJourneys[1].legs[0].alight.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[0].alight.arriveTime = new DateTime (2003, 9, 16, 13, 42, 19);
			result.publicJourneys[1].legs[0].alight.stop = new Stop();
			result.publicJourneys[1].legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[1].legs[0].alight.stop.name = "Alight name";

			result.publicJourneys[1].legs[0].destination = new Event();
			result.publicJourneys[1].legs[0].destination.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[0].destination.arriveTime = new DateTime (2003, 9, 16, 13, 52, 19);
			result.publicJourneys[1].legs[0].destination.stop = new Stop();
			result.publicJourneys[1].legs[0].destination.stop.NaPTANID = "Destination NAPTANID";
			result.publicJourneys[1].legs[0].destination.stop.name = "Destination name";

			result.publicJourneys[1].legs[1] = new TimedLeg();
			result.publicJourneys[1].legs[1].mode = ModeType.Metro;
			result.publicJourneys[1].legs[1].validated = true;

			result.publicJourneys[1].legs[1].board = new Event();
			result.publicJourneys[1].legs[1].board.activity = ActivityType.Depart;
			result.publicJourneys[1].legs[1].board.departTime = new DateTime(2003, 9, 16, 13, 45, 19);
			result.publicJourneys[1].legs[1].board.stop = new Stop();
			result.publicJourneys[1].legs[1].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[1].legs[1].board.stop.name = "Board name";

			result.publicJourneys[1].legs[1].alight = new Event();
			result.publicJourneys[1].legs[1].alight.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[1].alight.arriveTime = new DateTime (2003, 9, 16, 14, 48, 25);
			result.publicJourneys[1].legs[1].alight.stop = new Stop();
			result.publicJourneys[1].legs[1].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[1].legs[1].alight.stop.name = "Alight name";

			result.publicJourneys[1].legs[1].destination = new Event();
			result.publicJourneys[1].legs[1].destination.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[1].destination.arriveTime = new DateTime (2003, 9, 16, 14, 53, 25);
			result.publicJourneys[1].legs[1].destination.stop = new Stop();
			result.publicJourneys[1].legs[1].destination.stop.NaPTANID = "Destination NAPTANID";
			result.publicJourneys[1].legs[1].destination.stop.name = "Destination name";
			//-- 

			result.publicJourneys[1].legs[2] = new TimedLeg();
			result.publicJourneys[1].legs[2].mode = ModeType.Underground;
			result.publicJourneys[1].legs[2].validated = true;

			result.publicJourneys[1].legs[2].board = new Event();
			result.publicJourneys[1].legs[2].board.activity = ActivityType.Depart;
			result.publicJourneys[1].legs[2].board.departTime = new DateTime(2003, 9, 16, 14, 55, 25);
			result.publicJourneys[1].legs[2].board.stop = new Stop();
			result.publicJourneys[1].legs[2].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[1].legs[2].board.stop.name = "Board name";

			result.publicJourneys[1].legs[2].alight = new Event();
			result.publicJourneys[1].legs[2].alight.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[2].alight.arriveTime = new DateTime (2003, 9, 16, 15, 18, 25);
			result.publicJourneys[1].legs[2].alight.stop = new Stop();
			result.publicJourneys[1].legs[2].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[1].legs[2].alight.stop.name = "Alight name";

			result.publicJourneys[1].legs[2].destination = new Event();
			result.publicJourneys[1].legs[2].destination.activity = ActivityType.Arrive;
			result.publicJourneys[1].legs[2].destination.arriveTime = new DateTime (2003, 9, 16, 15, 22, 25);
			result.publicJourneys[1].legs[2].destination.stop = new Stop();
			result.publicJourneys[1].legs[2].destination.stop.NaPTANID = "Destination NAPTANID";
			result.publicJourneys[1].legs[2].destination.stop.name = "Destination name";
			// ----------

			result.publicJourneys[2] = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			result.publicJourneys[2].legs = new Leg[1];
			result.publicJourneys[2].legs[0] = new TimedLeg();
			result.publicJourneys[2].legs[0].mode = ModeType.Ferry;
			result.publicJourneys[2].legs[0].validated = true;

			result.publicJourneys[2].legs[0].board = new Event();
			result.publicJourneys[2].legs[0].board.activity = ActivityType.Depart;
			result.publicJourneys[2].legs[0].board.departTime = new DateTime(2003, 8, 29, 13, 12, 9);
			result.publicJourneys[2].legs[0].board.stop = new Stop();
			result.publicJourneys[2].legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.publicJourneys[2].legs[0].board.stop.name = "Board name";

			result.publicJourneys[2].legs[0].alight = new Event();
			result.publicJourneys[2].legs[0].alight.activity = ActivityType.Arrive;
			result.publicJourneys[2].legs[0].alight.arriveTime = new DateTime (2003, 8, 29, 13, 42, 19);
			result.publicJourneys[2].legs[0].alight.stop = new Stop();
			result.publicJourneys[2].legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.publicJourneys[2].legs[0].alight.stop.name = "Alight name";

			result.publicJourneys[2].legs[0].destination = new Event();
			result.publicJourneys[2].legs[0].destination.activity = ActivityType.Arrive;
			result.publicJourneys[2].legs[0].destination.arriveTime = new DateTime (2003, 8, 29, 13, 45, 19);
			result.publicJourneys[2].legs[0].destination.stop = new Stop();
			result.publicJourneys[2].legs[0].destination.stop.NaPTANID = "Destination NAPTANID";
			result.publicJourneys[2].legs[0].destination.stop.name = "Destination name";
			// create a private journey
			result.privateJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PrivateJourney[1];
			result.privateJourneys[0] = new TransportDirect.JourneyPlanning.CJPInterface.PrivateJourney();
			result.privateJourneys[0].congestion = true;

			string name = "name";

			result.privateJourneys[0].startTime = new DateTime(1, 1, 1, 0, 0, 0);
			result.privateJourneys[0].congestion = true;
			result.privateJourneys[0].start = new StopoverSection();
			result.privateJourneys[0].start.time = new DateTime(1, 1, 1, 0, 10, 0); 
			result.privateJourneys[0].start.name = name + " Section Name";
			result.privateJourneys[0].start.node = new ITNNode();
			result.privateJourneys[0].start.node.TOID = name + " TOID";

			result.privateJourneys[0].finish = new StopoverSection();
			result.privateJourneys[0].finish.time = new DateTime(1, 1, 1, 0, 10, 0);
			result.privateJourneys[0].finish.name = name + " Section Name";
			result.privateJourneys[0].finish.node = new ITNNode();
			result.privateJourneys[0].finish.node.TOID = name + " TOID";

			DriveSection driveSection = new DriveSection();

			driveSection.time =  new DateTime(1, 1, 1, 0, 20, 0); // new DateTime(2003, 7, 2, 14, 1, 1); //  // 
			driveSection.name = "Drive Section Name";
			driveSection.number = "Drive Section Number";
			driveSection.distance = 123;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.MiniRoundaboutContinue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = true;
			driveSection.cost = 30;
			driveSection.heading = "Drive Section Heading";
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;

			result.privateJourneys[0].sections = new DriveSection[1];
			result.privateJourneys[0].sections[0] = driveSection;

			return result;

		}

		/// <summary>
		/// Create a CJPResult containing either private or public journeys
		/// </summary>
		/// <param name="Public">bool Public - true for a Public journey, false for a private journey</param>
		/// <returns>JourneyResult</returns>
		public JourneyResult CreateCJPResult( bool publicJourney )
		{
			JourneyResult result = new JourneyResult();

			if( publicJourney )
			{
				result.publicJourneys = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney[2];
				result.publicJourneys[0] = CreatePublicJourney();
				result.publicJourneys[1] = CreatePublicJourney();
			}
			else
			{
				result.privateJourneys = new PrivateJourney[2];
				result.privateJourneys[0] = CreatePrivateJourney();
				result.privateJourneys[1] = CreatePrivateJourney();
			}
			return result;
		}
		/// <summary>
		/// Create a private journey.
		/// It will have a start, finish and drive section 
		/// </summary>
		/// <returns>PrivateJourney</returns>
		private PrivateJourney CreatePrivateJourney()
		{
			PrivateJourney result = new PrivateJourney();
			result.start = new StopoverSection();
			result.start = CreateStopoverSection( "Start" );
			result.finish = new StopoverSection();
			result.finish = CreateStopoverSection( "Finish" );
			result.sections = new Section[1];
			result.sections[0] = CreateDriveSection();

			return result;
		}
		/// <summary>
		/// Create a public journey
		/// It will have a board and alight leg
		/// </summary>
		/// <returns>PublicJourney</returns>
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney CreatePublicJourney()
		{
			TransportDirect.JourneyPlanning.CJPInterface.PublicJourney result = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();
			DateTime timeNow = DateTime.Now;

			result.legs = new Leg[1];

			result.legs[0] = new TimedLeg();

			result.legs[0].mode = ModeType.Tram;
			result.legs[0].validated = true;

			result.legs[0].board = new Event();
			result.legs[0].board.activity = ActivityType.Depart;
			result.legs[0].board.departTime = timeNow;
//			result.legs[0].board.pass = false;
			result.legs[0].board.stop = new Stop();
			result.legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.legs[0].board.stop.name = "Board name";

			result.legs[0].alight = new Event();
			result.legs[0].alight.activity = ActivityType.Arrive;
			result.legs[0].alight.arriveTime = timeNow.AddMinutes(30);
//			result.legs[0].alight.pass = false;
			result.legs[0].alight.stop = new Stop();
			result.legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.legs[0].alight.stop.name = "Alight name";

			result.legs[0].destination = new Event();
			result.legs[0].destination.activity = ActivityType.Arrive;
			result.legs[0].destination.arriveTime = timeNow.AddMinutes(59);
//			result.legs[0].destination.pass = false;
			result.legs[0].destination.stop = new Stop();
			result.legs[0].destination.stop.NaPTANID = "Destination NAPTANID";
			result.legs[0].destination.stop.name = "Destination name";			
			return result;
		}


		/// <summary>
		/// Create a single CJPResult public journey. (Used for summary line testing)
		/// </summary>
		/// <returns></returns>
		/// 
		private TransportDirect.JourneyPlanning.CJPInterface.PublicJourney CreatePublicJourney2()
		{
			TransportDirect.JourneyPlanning.CJPInterface.PublicJourney result = new TransportDirect.JourneyPlanning.CJPInterface.PublicJourney();

			result.legs = new Leg[1];

			result.legs[0] = new TimedLeg();

			result.legs[0].mode = ModeType.Tram;
			result.legs[0].validated = true;

			result.legs[0].board = new Event();
			result.legs[0].board.activity = ActivityType.Depart;
			result.legs[0].board.departTime = new DateTime(2003, 6, 5, 3, 4, 0);
			//			result.legs[0].board.pass = false;
			result.legs[0].board.stop = new Stop();
			result.legs[0].board.stop.NaPTANID = "Board NAPTANID";
			result.legs[0].board.stop.name = "Board name";

			result.legs[0].alight = new Event();
			result.legs[0].alight.activity = ActivityType.Arrive;
			result.legs[0].alight.arriveTime = new DateTime(2003, 6, 5, 3, 14, 0);
			//			result.legs[0].alight.pass = false;
			result.legs[0].alight.stop = new Stop();
			result.legs[0].alight.stop.NaPTANID = "Alight NAPTANID";
			result.legs[0].alight.stop.name = "Alight name";

			result.legs[0].destination = new Event();
			result.legs[0].destination.activity = ActivityType.Arrive;
			result.legs[0].destination.arriveTime = new DateTime(2003, 6, 5, 3, 15, 0);
			//			result.legs[0].destination.pass = false;
			result.legs[0].destination.stop = new Stop();
			result.legs[0].destination.stop.NaPTANID = "Destination NAPTANID";
			result.legs[0].destination.stop.name = "Destination name";			
			return result;
		}

		/// <summary>
		/// Creates a stop over section with a given name
		/// For use with the private journey
		/// </summary>
		/// <param name="name">string - the name of the section</param>
		/// <returns>StopoverSection</returns>
		private static StopoverSection CreateStopoverSection( string name )
		{
			DateTime timeNow = DateTime.Now;
			StopoverSection stopoverSection = new StopoverSection();

			stopoverSection.time = timeNow;
			stopoverSection.name = name + " Section Name";
			stopoverSection.node = new ITNNode();
			stopoverSection.node.TOID = name + " TOID";

			return stopoverSection;
		}
		/// <summary>
		/// Create a drive section
		/// For use with the private journey
		/// </summary>
		/// <returns>DriveSection</returns>
		private DriveSection CreateDriveSection()
		{
			DateTime timeNow = DateTime.Now;
			DriveSection driveSection = new DriveSection();

			driveSection.time = timeNow;
			driveSection.name = "Drive Section Name";
			driveSection.number = "Drive Section Number";
			driveSection.distance = 123;
			driveSection.turnCount = 3;
			driveSection.turnDirection = TurnDirection.MiniRoundaboutContinue;
			driveSection.turnAngle = TurnAngle.Continue;
			driveSection.roundabout = true;
			driveSection.throughRoute = true;
			driveSection.cost = 30;
			driveSection.heading = "Drive Section Heading";
			driveSection.links = new ITNLink[1];
			driveSection.links[0] = new ITNLink();
			driveSection.links[0].TOID = "Drive TOID";
			driveSection.links[0].congestion = 1;
			return driveSection;
		}

		#endregion

	}
}
