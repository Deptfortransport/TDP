// ***********************************************
// NAME 		: TestClearCacheHelper.cs
// AUTHOR 		: Tim Mollart
// DATE CREATED : 19/20/2005
// DESCRIPTION 	: NUnit test for ClearCacheHelper class.
// NOTES		: 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestClearCacheHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:00   mturner
//Initial revision.
//
//   Rev 1.0   Dec 28 2005 11:48:44   NMoorhouse
//Initial revision.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2

using System;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TestClearCacheHelper.
	/// </summary>
	[TestFixture]
	[CLSCompliant(false)]
	public class TestClearCacheHelper
	{

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation() );
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.SessionManager, new TestMockSimpleSessionManager());

			TDSessionManager.Current.JourneyRequest = new TDJourneyRequest();
			TDSessionManager.Current.JourneyMapState = new JourneyMapState();
			TDSessionManager.Current.ReturnJourneyMapState = new JourneyMapState();
			TDSessionManager.Current.JourneyViewState = new TDJourneyViewState();
			TDSessionManager.Current.InputPageState = new InputPageState();
			TDSessionManager.Current.FindStationPageState = new FindStationPageState();
			
			// Objects that do not create a new instance when required.
			TDSessionManager.Current.JourneyResult = new TDJourneyResult();
			TDSessionManager.Current.AmendedJourneyResult = new TDJourneyResult();
			TDSessionManager.Current.CurrentAdjustState = new TDCurrentAdjustState(new TDJourneyRequest());
			TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();
			TDSessionManager.Current.FindPageState = new FindTrainPageState();
			TDSessionManager.Current.AmbiguityResolution = new AmbiguityResolutionState();

			TDSessionManager.Current.ItineraryMode = ItineraryManagerMode.VisitPlanner;
		}

		[TearDown] 
		public void Dispose()
		{ 
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestClearCache()
		{
			ClearCacheHelper helper = new ClearCacheHelper();

			helper.ClearCache();

			ITDSessionManager sm = TDSessionManager.Current;

			// Check that objects are now null/not null as required
			Assert.IsNotNull(sm.JourneyRequest, "JourneyRequest is null.");
			Assert.IsNotNull(sm.JourneyMapState, "JourneyMapState is null.");
			Assert.IsNotNull(sm.ReturnJourneyMapState, "ReturnJourneyMapState is null.");
			Assert.IsNotNull(sm.JourneyViewState, "JourneyViewState is null.");
			Assert.IsNotNull(sm.InputPageState, "InputPageState is null.");
			Assert.IsNull(sm.FindStationPageState, "FindStationPageState is not null.");
			Assert.IsNull(sm.FindStationPageState, "FindStationPageState is not null.");
			Assert.IsNull(sm.StoredMapViewState, "StoredMapViewState is not null.");
			Assert.IsNotNull(sm.JourneyResult, "JourneyResult is null.");
			Assert.IsNull(sm.AmendedJourneyResult, "AmendedJourneyResult is not null.");
			Assert.IsNull(sm.CurrentAdjustState, "CurrentAdjustState is not null.");
			Assert.IsNotNull(sm.JourneyParameters, "JourneyParameters is null.");
			Assert.IsNotNull(sm.JourneyParameters, "JourneyParameters is null.");
			Assert.IsNull(sm.FindPageState, "FindPageState is not null.");
			Assert.IsNull(sm.AmbiguityResolution, "AmbiguityResolution is not null.");
			Assert.IsNull(sm.TravelNewsState, "TravelNewsState is not null.");
			Assert.IsNotNull(sm.AsyncCallState, "AsyncCallState is null.");
			Assert.IsNotNull(sm.ItineraryManager, "ItineraryManager is  null.");
		}
	}
}
