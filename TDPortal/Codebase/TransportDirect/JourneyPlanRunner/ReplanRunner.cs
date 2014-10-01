// *********************************************** 
// NAME                 : ReplanRunner.cs 
// AUTHOR               : Tim Mollart
// DATE CREATED         : 13/02/2006
// DESCRIPTION			: 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/ReplanRunner.cs-arc  $
//
//   Rev 1.1   Mar 10 2008 15:18:30   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//   Rev 1.0   Nov 08 2007 12:24:46   mturner
//Initial revision.
//
//   Rev 1.3   Apr 05 2006 15:42:40   build
//Automatically merged from branch for stream0030
//
//   Rev 1.2.1.0   Mar 29 2006 11:10:32   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.2   Mar 09 2006 14:04:22   RGriffith
//Fix to prevent cast problems when replanning a journey after having visited the Journey Fares page
//
//   Rev 1.1   Feb 17 2006 14:41:42   tmollart
//Removed validation method. No longer required.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 15 2006 16:13:22   tmollart
//Initial revision.

using System;
using System.Resources;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for ReplanRunner.
	/// </summary>
	public class ReplanRunner
	{
		/// <summary>
		/// Standard constructor.
		/// </summary>
		public ReplanRunner()
		{
		}


		/// <summary>
		/// Runs a replan journey. Does not perform any validation. Uses journey plan runner
		/// for ammend journeys. Result will be in AmmendedJourney property on session manager.
		/// </summary>
		/// <param name="pageState">Replan page state object to use.</param>
		/// <param name="originalResult">Original journey result to use.</param>
		/// <param name="itineraryManager">Current RePlan itinerary manager.</param>
		/// <param name="resourceManager">Resouce manager. Can be null.</param>
		/// <param name="lang">Current language. Can be null.</param>
		public void RunReplan(ReplanPageState pageState, ITDJourneyResult originalResult, ReplanItineraryManager itineraryManager, TDResourceManager resourceManager, string lang)
		{
			// Get reference/sequence number from original journey
			int referenceNumber = Convert.ToInt32(originalResult.JourneyReferenceNumber);
			int sequenceNumber = Convert.ToInt32(originalResult.LastReferenceSequence);

			// Build the journey request
			ITDJourneyRequest newRequest = itineraryManager.BuildReplanJourneyRequest(
				pageState.OriginalRequest,
				pageState.ReplanStartJourneyDetailIndex,
				pageState.ReplanEndJourneyDetailIndex,
				(pageState.CurrentAmendmentType == TDAmendmentType.OutwardJourney));

			// Journey planner runner
			JourneyPlanRunner runner = new JourneyPlanRunner( resourceManager );

			JourneyPlanState jps = TDSessionManager.Current.AsyncCallState as JourneyPlanState;

			if (jps == null)
			{
				jps = new JourneyPlanState();
			}

			// Determine refresh interval and resource string for the wait page
			jps.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.Replan"]);
			jps.WaitPageMessageResourceFile = "langStrings";
			jps.WaitPageMessageResourceId = "WaitPageMessage.Replan";
			
			jps.AmbiguityPage = PageId.JourneyPlannerAmbiguity;
			jps.DestinationPage = PageId.ReplanFullItinerarySummary;
			jps.ErrorPage = PageId.ReplanFullItinerarySummary;
			TDSessionManager.Current.AsyncCallState = jps;

			TDSessionManager.Current.JourneyResult.IsValid = false;

			runner.ValidateAndRun(referenceNumber, sequenceNumber, newRequest, TDSessionManager.Current, lang);
			
			// Transfer the user to the wait page.
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
		}
	}
}
