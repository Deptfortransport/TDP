// *********************************************** 
// NAME                 : RefineHelper.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 01/03/2006
// DESCRIPTION			: Provides helper methods for use by the Adjust Journey functionality.
// ************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/RefineHelper.cs-arc  $
//
//   Rev 1.4   Dec 11 2012 11:59:54   mmodi
//Hide adjust options for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Oct 13 2008 16:41:28   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Jul 30 2008 11:10:08   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 12:59:12   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 15 2007 17:00:00   mmodi
//Handle page transition back to ParkAndRideInput
//
//   Rev 1.0   Nov 08 2007 13:11:28   mturner
//Initial revision.
//
//   Rev 1.7   Oct 06 2006 14:08:28   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.6.1.0   Oct 03 2006 10:33:52   mmodi
//Check if we originally came from NearestCarParks input when New search selected
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4208: Car Parking: New Search a car park journey goes to Car route input
//
//   Rev 1.6   May 04 2006 10:57:18   mtillett
//Move park and ride code to FindInputAdapter
//Resolution for 4072: DN058 Park and Ride Phase 2: Destination field is empty on ambiguity page after entering invalid date in the Amend Tool
//
//   Rev 1.5   May 03 2006 15:41:20   asinclair
//Added a check to see if the user has come from the Park and Ride Input page
//Resolution for 4060: DN058 Park & Ride Phase 2: After Extend/Replan etc - New Search button displays Find a Car
//
//   Rev 1.4   Apr 26 2006 12:16:10   RPhilpott
//Correct destination of "New Search" if in cost-based
//Resolution for 3940: DD075: Find Cheaper: New Search displays Find a Fare instead of Find a Train
//Resolution for 3994: DD075: New Search from Search by Price does not clear details of existing journey
//
//   Rev 1.3   Apr 18 2006 11:10:46   mtillett
//Use the BaseFindAMode instead of the FindAMode on the session manager, as this is reset for extend etc
//Resolution for 3911: DN068: New Search displays City-to-City instead of Find Nearest input screen
//
//   Rev 1.2   Mar 09 2006 19:08:44   pcross
//Updated the judgement of whether we can apply adjust or not to exclude all non schedule-based services (not just walks)
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Mar 03 2006 11:25:34   NMoorhouse
//Added (common) NewSearch onto the helper class
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Mar 02 2006 14:56:10   pcross
//Initial revision.
//

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.Web.Adapters
{

	/// <summary>
	/// This class provides helper/adapter methods for use by Adjust Journey functionality.
	/// </summary>
	public class RefineHelper : TDWebAdapter
	{

		/// <summary>
		/// Empty constructor to allow initialisation
		/// </summary>
		public RefineHelper()
		{
		}

		/// <summary>
		/// Decides if the outward or return journey currently selected in journey result in session is
		/// ok to perform adjust operations on.
		/// OK to adjust if:
		/// Journey available (may be no return journey); not road journey; PT journey is multi-leg (excluding walks).
		/// </summary>
		/// <param name="outward">Specifies whether considering outward or return journey</param>
		/// <returns></returns>
		public bool IsAdjustAvailable(bool outward)
		{

			// Look at the selected outward and return journeys. If there is only a single leg (excluding walk legs)
			// in a journey then we won't offer adjust (since we may as well just re-start from beginning as that is
			// essentially what would happen on adjust)
			ITDSessionManager sessionManager = TDSessionManager.Current;
			ResultsAdapter helper = new ResultsAdapter();

			Journey journey = helper.GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, !outward);

			// Check for journey existence
			if (journey == null)
				return false;

			// Check for road journey
			if (journey.Type == TDJourneyType.RoadCongested)	// This is the type for a road journey
				return false;

			// Check for multi-leg PT
			if (!IsMultiPTLeg(journey as JourneyControl.PublicJourney))
				return false;

            // Check for accessible journey
            if (IsAccessibleJourney(journey))
                return false;

			// If here, all tests passed
			return true;

		}

		/// <summary>
		/// Returns true if the journey is multi-leg, excluding walks and frequency based services.
		/// </summary>
		/// <param name="journey">Public journey to examine</param>
		/// <returns>True if the journey is multi-leg, excluding walks</returns>
		private bool IsMultiPTLeg(JourneyControl.PublicJourney journey)
		{
			int modeCheck = 0;
			bool multiLeg = false;

			foreach (JourneyControl.JourneyLeg leg in journey.JourneyLegs)
			{
				if (leg is PublicJourneyTimedDetail)
					modeCheck++;

				if (modeCheck > 1)
				{
					multiLeg = true;
					break;
				}
			}

			return multiLeg;
		}

        /// <summary>
        /// Returns true if the journey is a public accessible journey
        /// </summary>
        /// <param name="journey">Journey to examine</param>
        public bool IsAccessibleJourney(JourneyControl.Journey journey)
        {
            if (journey != null && journey is JourneyControl.PublicJourney)
            {
                JourneyControl.PublicJourney publicJourney = (JourneyControl.PublicJourney)journey;

                if (publicJourney.AccessibleJourney)
                {
                    return true;
                }
            }

            return false;
        }

		/// <summary>
		/// Handles new search button click event.
		/// This code is designed to return to the input page the results were derived from and reset 
		/// it to a blank input value ready for a new search.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void NewJourney()
		{
			// If we are in the CostBased partition, switch back to TimeBased 
			//  so that the user is taken back to their original (time based) 
			//  journey input page rather than the Find Fare input page.

			if	(TDSessionManager.Current.Partition == TDSessionPartition.CostBased)
			{
				// clear down any existing cost-based results first ...
				TDItineraryManager costBasedItineraryManager = TDItineraryManager.Current;
				costBasedItineraryManager.NewSearch();
				
				TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;
			}

            // The code for this is based on the "new search" button functionality in the 
			// JourneyChangeSearchControl.

			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			// Redirect user to the input page appropriate for the mode used to plan the initial journey.
			// Determining the transition event must happen before resetting itinerary manager in order for
			// BaseJourneyFindAMode to return correct value.

			FindAMode baseFindAMode = itineraryManager.BaseJourneyFindAMode;
			if (baseFindAMode == FindAMode.TrunkStation)
			{
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
				sessionManager.SetOneUseKey(SessionKey.NotFindAMode, "true");
			}
			else 
			{
				if (baseFindAMode == FindAMode.None) 
				{
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
				} 
				else 
				{
					sessionManager.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(baseFindAMode);
				}
			}

			// If we are in visit planner we need to redirect to a new place.
			if (sessionManager.ItineraryMode == ItineraryManagerMode.VisitPlanner)
			{
				sessionManager.JourneyParameters = new TDJourneyParametersVisitPlan();
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.VisitPlannerNewClear;

				VisitPlannerAdapter adaptor = new VisitPlannerAdapter();
				adaptor.ClearDownRequestAndResults(sessionManager.ItineraryManager, sessionManager.ResultsPageState);
			}

            // If we are in park and ride we need to redirect to the park and ride page
            if ((itineraryManager.JourneyParameters.DestinationLocation.ParkAndRideScheme != null) 
                || (baseFindAMode == FindAMode.ParkAndRide))
            {
                sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ParkAndRideInput;
            }
  
			// If we are in Find nearest car park mode, then we need to redirect to Find car park input
			if (sessionManager.IsFromNearestCarParks)
			{
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindCarParkInputDefault;
			}

			// Reset the itinerary manager
			itineraryManager.NewSearch();

			// Flag new search button being clicked so that redirect page can perform any necessary initialisation
			sessionManager.SetOneUseKey(SessionKey.NewSearch,string.Empty);

			// invalidate the current journey result. Set the mode for which the results pertain to as being
			// none so that clicking the Find A tab will then redirect to the default find A input page.
			if (sessionManager.JourneyResult != null)
			{
				sessionManager.JourneyResult.IsValid = false;
			}

			//set the ItineraryMode to none
			sessionManager.ItineraryMode = ItineraryManagerMode.None;
		}

	}

}
