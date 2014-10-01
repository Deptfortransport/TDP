// ***********************************************
// NAME 		: TestTDJourneyViewState.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 21/08/2003
// DESCRIPTION 	: NUnit test class for testing the TDJourneyViewState.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestTDJourneyViewState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:20   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2005 16:41:36   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.4   Feb 07 2005 11:22:36   RScott
//Assertion changed to Assert
//
//   Rev 1.3   Nov 06 2003 16:27:28   PNorell
//Ensured test work properly.
//
//   Rev 1.2   Aug 29 2003 10:44:22   kcheung
//Updated made after TDTimeSearchType was replaced by a boolean.
//
//   Rev 1.1   Aug 27 2003 11:54:42   PNorell
//Amended file header.
using System;
using NUnit.Framework;
using TransportDirect.Common;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// NUnit test class for testing the TDJourneyViewState.
	/// </summary>
	[TestFixture]
	public class TestTDJourneyViewState
	{
		/// <summary>
		/// Verifies that changes of state take effect.
		/// </summary>
		[Test]
		public void consistencyCheck()
		{
			TDJourneyViewState jvstate = new TDJourneyViewState();
			
			jvstate.CallingPageID = PageId.InitialPage;
			TDDateTime journeyLeavingDateTime  = TDDateTime.Now;
			TDDateTime journeyReturningDateTime  = TDDateTime.Now;

			// Create the original journey request
			ITDJourneyRequest orgRequest = new TDJourneyRequest();
			jvstate.OriginalJourneyRequest = orgRequest;
			orgRequest.OutwardDateTime = new TDDateTime[1];
			orgRequest.OutwardDateTime[0] = journeyLeavingDateTime;
			orgRequest.OutwardArriveBefore = false;
			orgRequest.ReturnDateTime = new TDDateTime[1];
			orgRequest.ReturnDateTime[0] = journeyReturningDateTime;
			orgRequest.ReturnArriveBefore = true;
			
			jvstate.SelectedOutwardJourney = 1;
			jvstate.SelectedReturnJourney = 2;
			jvstate.SelectedRouteJourneyType = TDJourneyType.PublicOriginal;

			Assert.AreEqual(PageId.InitialPage, jvstate.CallingPageID,"CallingPageID");
			Assert.AreEqual(journeyLeavingDateTime, jvstate.JourneyLeavingDateTime, "JourneyLeavingDateTime");
			Assert.AreEqual(false, jvstate.JourneyLeavingTimeSearchType, "JourneyLeavingTimeSearchType");
			Assert.AreEqual(journeyReturningDateTime, jvstate.JourneyReturningDateTime, "JourneyReturningDateTime");
			Assert.AreEqual(true, jvstate.JourneyReturningTimeSearchType, "JourneyReturningTimeSearchType");
			Assert.AreEqual(orgRequest, jvstate.OriginalJourneyRequest, "OriginalJourneyRequest");
			Assert.AreEqual(1, jvstate.SelectedOutwardJourney, "SelectedOutwardJourney");
			Assert.AreEqual(2, jvstate.SelectedReturnJourney, "SelectedReturnJourney");
			Assert.AreEqual(TDJourneyType.PublicOriginal, jvstate.SelectedRouteJourneyType, "SelectedRouteJourneyType");
		}

	}
}
