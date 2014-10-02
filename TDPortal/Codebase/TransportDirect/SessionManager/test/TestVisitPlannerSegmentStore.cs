// ***********************************************
// NAME 		: TestVisitPlannerSegmentStore.cs
// AUTHOR 		: Paul Cross
// DATE CREATED : 15/09/2005
// DESCRIPTION 	: NUnit test for VisitPlannerSegmentStore class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestVisitPlannerSegmentStore.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:08   mturner
//Initial revision.
//
//   Rev 1.3   Mar 14 2006 08:41:46   build
//Automatically merged from branch for stream3353
//
//   Rev 1.2.1.0   Dec 20 2005 19:50:14   rhopkins
//Removed test for obsolete methods, which no longer exist.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Nov 24 2005 16:33:38   tmollart
//Updated to reflect changes made in main class.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 27 2005 08:53:46   pcross
//Minor updates. Mainly associated with PublicJourney class update.
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
	/// NUnit test for VisitPlannerSegmentStore class.
	/// </summary>
	/// <TestPlan>
	/// </TestPlan>
	[TestFixture]
	[CLSCompliant(false)]
	public class TestVisitPlannerSegmentStore
	{

		private DateTime timeNow;
        
        public TestVisitPlannerSegmentStore()
		{
		}


		#region Setup / teardown

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation() );
			TDSessionManager.Current.JourneyViewState = new TDJourneyViewState();
			timeNow = TDDateTime.Now.GetDateTime();
		}

		[TearDown] 
		public void Dispose()
		{ 
		}

		#endregion

		#region Tests
		
		/// <summary>
		/// Tests the ExtendEndOfSegment property.
		/// </summary>
		[Test]
		public void TestExtendEndOfSegment()
		{
			bool testValue = true;

			//Create a new segment store.
			VisitPlannerSegmentStore segment = new VisitPlannerSegmentStore();

			segment.ExtendEndOfSegment = testValue;

			Assert.AreEqual(testValue, segment.ExtendEndOfSegment,"Test value and property are not the same.");
		}

		#endregion
	}
}

