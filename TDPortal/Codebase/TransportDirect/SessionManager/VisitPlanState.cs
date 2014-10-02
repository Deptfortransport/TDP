// *********************************************** 
// NAME         : VisitPlanState.cs
// AUTHOR       : Jonathan George
// DATE CREATED : 09/11/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/VisitPlanState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:50   mturner
//Initial revision.
//
//   Rev 1.4   Apr 05 2006 15:42:46   build
//Automatically merged from branch for stream0030
//
//   Rev 1.3.1.0   Mar 29 2006 11:10:28   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.3   Nov 17 2005 16:48:54   tmollart
//Added additional processing to handle errors.
//Resolution for 2946: Visit Planner (CG): duplicate transport from locality
//
//   Rev 1.2   Nov 11 2005 13:28:20   tmollart
//Added time out processing for Visit Planner.
//
//   Rev 1.1   Nov 10 2005 11:49:48   jgeorge
//Added DoCompletedOkProcessing method
//
//   Rev 1.0   Nov 10 2005 10:15:02   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Class used to control the asynchronous call for visit planning
	/// </summary>
	[Serializable]
	public class VisitPlanState : AsyncCallState
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public VisitPlanState() : base( int.Parse(Properties.Current["MultipleJourneyInput.WaitPageTimeoutSeconds"] ) )
		{ }

		/// <summary>
		/// Implementation of the ProcessState method. Does any processing required for the current state
		/// of the object prior to redirection to the page specified in the return value.
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		public override PageId ProcessState()
		{
			switch (this.Status)
			{
				case AsyncCallStatus.CompletedOK:
					return DoCompletedOkProcessing();

				case AsyncCallStatus.TimedOut:
					return DoTimeoutProcessing();

				case AsyncCallStatus.NoResults:
					return DoNoResultsProcessing();

				default:
					return PageId.WaitPage;
			}
		}


		/// <summary>
		/// Performs the processing required when the status is AsyncCallStatus.TimedOut
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		private PageId DoTimeoutProcessing()
		{
			// Time out processing code needs to add an segment containing a
			// CJP error message to the itinerary manager. As the journey planning could be
			// in any state when a time out occurs we reset the itinerary to clear out
			// any result which may already be there and then repopulate it with one
			// segment containing an erronous result object.
			PopulateSegmentWithCJPErrorMessage();
			
			// Return the page ID for the appropriate error page.
			return this.ErrorPage;
		}


		/// <summary>
		/// Performs the processing required when no results are returned and the status is
		/// AsyncCallStatus.NoResults
		/// </summary>
		/// <returns></returns>
		private PageId DoNoResultsProcessing()
		{

			// No results processing needs to check that there are no CJP error messages
			// already present and if not a segment needs to be put on the itinerary
			// manager with a CJP error in it. As we dont know the state of processing
			// when the error has occured we just clear down the itinerary and then
			// add a new segment containing an error message.

			// Get a reference to the visit planner itinerary manager.
			VisitPlannerItineraryManager im = (VisitPlannerItineraryManager)TDItineraryManager.Current;

			if (im.CJPMessages.GetLength(0) == 0)
			{
				PopulateSegmentWithCJPErrorMessage();
			}

			return this.ErrorPage;
		}


		/// <summary>
		/// Performs the processing required when the status is AsyncCallStatus.CompletedOK
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		private PageId DoCompletedOkProcessing()
		{
			// Set flag to indicate that we are going to the destination page from
			// the wait page.
			TDSessionManager.Current.SetOneUseKey(SessionKey.FirstViewingOfResults, "yes");

			return this.DestinationPage;
		}



		/// <summary>
		/// Put a blank segment on the itinerary manager that contains a CJP
		/// error message.
		/// </summary>
		private void PopulateSegmentWithCJPErrorMessage()
		{
			// Get a reference to the visit planner itinerary manager.
			VisitPlannerItineraryManager im = (VisitPlannerItineraryManager)TDItineraryManager.Current;

			// Reset the itinerary which will make all the segments empty.
			im.ResetItinerary();
		
			// Create a result object and populate it with an error message.
			TDJourneyResult result = new TDJourneyResult();
			result.AddMessageToArray(string.Empty, 
				JourneyControlConstants.CJPInternalError, 
				JourneyControlConstants.CjpCallError,
				0);

			// Add a segment to the itinerary containing empty objects apart from the
			// result object which will contain the error message.
			im.AddSegmentToItinerary(new TDJourneyParametersVisitPlan(), new TDJourneyRequest(), result, new TDJourneyState());
		}

	}
}
