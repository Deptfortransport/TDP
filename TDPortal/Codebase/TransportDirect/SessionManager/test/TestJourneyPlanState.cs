// *********************************************** 
// NAME         : TestJourneyPlanState.cs
// AUTHOR       : Jonathan George
// DATE CREATED : 19/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestJourneyPlanState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:49:02   mturner
//Initial revision.
//
//   Rev 1.0   Oct 21 2005 18:32:34   jgeorge
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Tests JourneyPlanState.
	/// </summary>
	[TestFixture]
	public class TestJourneyPlanState
	{
		/// <summary>
		/// Default constructor. Does nothing.
		/// </summary>
		public TestJourneyPlanState()
		{
		}

		/// <summary>
		/// Sets up service discovery for the tests.
		/// </summary>
		[TestFixtureSetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestAsyncCallStateInitialisation());
		}

		/// <summary>
		/// Clears down service discovery to avoid interference with subsequent tests.
		/// </summary>
		[TestFixtureTearDown]
		public void TearDown()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		/// <summary>
		/// Tests the processing when the status is CompletedOk.
		/// The ProcessState method should return the destination page, and the one use key 
		/// SessionKey.FirstViewingOfResults should be set to "yes".
		/// </summary>
		[Test]
		public void TestCompletedOk()
		{
			AsyncCallState state = new JourneyPlanState(PageId.JourneyPlannerAmbiguity, PageId.JourneySummary, PageId.JourneyPlannerInput, 0, 0);
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ ServiceDiscoveryKey.SessionManager ];
			state.Status = AsyncCallStatus.CompletedOK;

			PageId result = state.ProcessState();

			Assert.AreEqual(PageId.JourneySummary, result, "Result of ProcessState not as expected when Status = CompletedOK"); 
			
			string key = sessionManager.GetOneUseKey(SessionKey.FirstViewingOfResults);
			Assert.AreEqual("yes", key, "OneUseKey SessionKey.FirstViewingOfResults was not set correctly when Status = CompletedOK");
		}

		/// <summary>
		/// Tests the processing when the status is TimedOut.
		/// The ProcessState method should return the error page.
		/// The JourneyResult property of session manager should be set to a new TDJourneyResult
		/// There should be a message with resource id of JourneyControlConstants.CJPInternalError
		/// and major code of JourneyControlConstants.CjpCallError
		/// There should be a new JourneyViewState value with the OriginalJourneyRequest property
		/// set to the same as the session manager JourneyRequest property
		/// </summary>
		[Test]
		public void TestTimeout()
		{
			AsyncCallState state = new JourneyPlanState(PageId.JourneyPlannerAmbiguity, PageId.JourneySummary, PageId.JourneyPlannerInput, 0, 0);
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ ServiceDiscoveryKey.SessionManager ];
			state.Status = AsyncCallStatus.TimedOut;

			sessionManager.JourneyResult = null;
			ITDJourneyRequest theRequest = new TDJourneyRequest();
			sessionManager.JourneyRequest = theRequest;
			sessionManager.JourneyViewState = null;

			PageId result = state.ProcessState();

			Assert.AreEqual(PageId.JourneyPlannerInput, result, "Result of ProcessState not as expected when Status = TimedOut"); 

			ITDJourneyResult theResult = sessionManager.JourneyResult;

			Assert.IsNotNull(theResult, "JourneyResult property of session manager returned null after processing when Status = TimedOut");
			Assert.AreEqual(1, theResult.CJPMessages.Length, "Number of CJP Messages not as expected on JourneyResult after processing when Status = TimedOut");
			Assert.AreEqual(JourneyControlConstants.CJPInternalError, theResult.CJPMessages[0].MessageResourceId, "CJP Message not as expected on JourneyResult after processing when Status = TimedOut");

			TDJourneyViewState theViewState = sessionManager.JourneyViewState;
			Assert.IsNotNull(theViewState, "JourneyViewState property of session manager returned null after processing when Status = TimedOut");
			Assert.AreSame(theRequest, theViewState.OriginalJourneyRequest, "OriginalJourneyRequest property of JourneyViewState not as expected after processing when Status = TimedOut");
		}

		/// <summary>
		/// Tests the processing when the status is NoResults.
		/// The ProcessState method should return the error page.
		/// </summary>
		[Test]
		public void TestNoResults()
		{
			AsyncCallState state = new JourneyPlanState(PageId.JourneyPlannerAmbiguity, PageId.JourneySummary, PageId.JourneyPlannerInput, 0, 0);
			state.Status = AsyncCallStatus.NoResults;

			PageId result = state.ProcessState();

			Assert.AreEqual(PageId.JourneyPlannerInput, result, "Result of ProcessState not as expected when Status = NoResults"); 
		}

		/// <summary>
		/// Tests the processing when the status is InProgress.
		/// The ProcessState method should return the wait page.
		/// </summary>
		[Test]
		public void TestInProgress()
		{
			AsyncCallState state = new JourneyPlanState(PageId.JourneyPlannerAmbiguity, PageId.JourneySummary, PageId.JourneyPlannerInput, 0, 0);
			state.Status = AsyncCallStatus.InProgress;

			PageId result = state.ProcessState();

			Assert.AreEqual(PageId.WaitPage, result, "Result of ProcessState not as expected when Status = InProgress"); 
		}

		/// <summary>
		/// Tests the processing when the status is ValidationError.
		/// The ProcessState method should return the wait page (ValidationError will never happen
		/// when planning a time based journey).
		/// </summary>
		[Test]
		public void TestValidationError()
		{
			AsyncCallState state = new JourneyPlanState(PageId.JourneyPlannerAmbiguity, PageId.JourneySummary, PageId.JourneyPlannerInput, 0, 0);
			state.Status = AsyncCallStatus.ValidationError;

			PageId result = state.ProcessState();

			Assert.AreEqual(PageId.WaitPage, result, "Result of ProcessState not as expected when Status = ValidationError"); 
		}

	


	}
}
