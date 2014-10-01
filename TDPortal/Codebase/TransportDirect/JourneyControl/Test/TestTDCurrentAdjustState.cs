// ***********************************************
// NAME 		: TestTDCurrentAdjustState.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 21/08/2003
// DESCRIPTION 	: NUnit test class for testing the TDCurrentAdjustState.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestTDCurrentAdjustState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:20   mturner
//Initial revision.
//
//   Rev 1.4   Feb 07 2005 11:22:28   RScott
//Assertion changed to Assert
//
//   Rev 1.3   Sep 01 2003 16:28:46   jcotton
//Updated: RouteNum
//
//   Rev 1.2   Aug 29 2003 10:44:12   kcheung
//Updated made after TDTimeSearchType was replaced by a boolean.
//
//   Rev 1.1   Aug 27 2003 11:54:42   PNorell
//Amended file header.
using System;
using NUnit.Framework;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// NUnit test class for testing the TDCurrentAdjustState.
	/// </summary>
	[TestFixture]
	public class TestTDCurrentAdjustState
	{
		/// <summary>
		/// Verifies that changes of state take effect.
		/// </summary>
		[Test]
		public void consistencyCheck()
		{
			// Create the original journey request
			ITDJourneyRequest orgRequest = new TDJourneyRequest();

			// Create TDCurrentAdjustState
			TDCurrentAdjustState adjustState = new TDCurrentAdjustState(orgRequest);

			// See MockJourneyResult in TestAdjustRoute
			PublicJourneyDetail[] pd = new PublicJourneyDetail[0];
			PublicJourney amendedJourney = new PublicJourney(0,pd, TDJourneyType.PublicAmended, 0);
			adjustState.AmendedJourney = amendedJourney;

			// Create the amended journey request
			ITDJourneyRequest amendedJourneyRequest = new TDJourneyRequest();
			adjustState.AmendedJourneyRequest = amendedJourneyRequest;

            adjustState.CurrentAmendmentType = TDAmendmentType.ReturnJourney;
			adjustState.JourneyReferenceSequence = 10;

			PublicJourneyDetail[] remainingRouteSegment = new PublicJourneyDetail[0];
			adjustState.RemainingRouteSegment = remainingRouteSegment;

			adjustState.RequestStatus = JourneyStatusType.RequestSent;

			adjustState.SelectedOutwardJourney = 1;
			adjustState.SelectedReturnJourney = 2;
			adjustState.SelectedRouteNode = 3;
			adjustState.SelectedRouteNodeSearchType = false;

			Assert.AreEqual(amendedJourney, adjustState.AmendedJourney, "AmendedJourney");
			Assert.AreEqual(amendedJourneyRequest, adjustState.AmendedJourneyRequest, "AmendedJourneyRequest");
			Assert.AreEqual(TDAmendmentType.ReturnJourney, adjustState.CurrentAmendmentType, "CurrentAmendmentType");
			Assert.AreEqual(10, adjustState.JourneyReferenceSequence, "JourneyReferenceSequence");
			Assert.AreEqual(orgRequest, adjustState.OriginalJourneyRequest, "OriginalJourneyRequest");
			Assert.AreEqual(remainingRouteSegment, adjustState.RemainingRouteSegment, "RemainingRouteSegment");
			Assert.AreEqual(JourneyStatusType.RequestSent, adjustState.RequestStatus, "RequestStatus");
			Assert.AreEqual(1, adjustState.SelectedOutwardJourney, "SelectedOutwardJourney");
			Assert.AreEqual(2, adjustState.SelectedReturnJourney, "SelectedReturnJourney");
			Assert.AreEqual(3, adjustState.SelectedRouteNode, "SelectedRouteNode");
			Assert.AreEqual(false, adjustState.SelectedRouteNodeSearchType, "SelectedRouteNodeSearchType");

		}
	}
}
