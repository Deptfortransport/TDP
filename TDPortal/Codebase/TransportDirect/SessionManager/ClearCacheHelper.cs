// ************************************************************
// NAME         : ClearCacheHelper
// AUTHOR       : Tim Mollart
// DATE CREATED : 25/11/2004
// DESCRIPTION  : Helper class for clearing down cached journey
//				  results.
// *************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ClearCacheHelper.cs-arc  $
//
//   Rev 1.3   May 13 2010 11:08:34   mmodi
//Added method to clear the printable session information
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.2   Mar 26 2010 11:20:14   MTurner
//Added code to remove only the result objects from a session, leaving everything else intact.
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.1   Oct 13 2008 16:46:36   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Aug 22 2008 10:17:48   mmodi
//Updated to clear cycle objects
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:48:18   mturner
//Initial revision.
//
//   Rev 1.5   Mar 06 2007 12:28:22   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.4.1.0   Feb 20 2007 15:29:54   mmodi
//Cleared the JourneyEmissionsPageState
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.4   Oct 06 2006 13:30:52   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.3.1.0   Oct 03 2006 10:31:54   mmodi
//Reset the IsFromNearestCarParks property
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4208: Car Parking: New Search a car park journey goes to Car route input
//
//   Rev 1.3   May 18 2006 14:43:10   rphilpott
//Delete existing PricingRetailOptionsState when starting a new trunk journey enquiry.
//Resolution for 4026: Homepage Phase 2: Fare details not reset if plan new journey using tab at top
//
//   Rev 1.2   Feb 17 2006 16:18:30   tolomolaiye
//Fix for IR 3572. Fixed error that occured when planning a Door-to-Door journey immediately after planning a Vist Planner (Day Trip Planner) journey.
//Resolution for 3572: Del 8.1 - H2 Merge - Problems with JourneySummary, Journey Details, JourneyMaps, and JourneyTickets pages.
//
//   Rev 1.1   Dec 22 2005 09:12:12   tmollart
//Removed reference to OldJourneyParameters.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.0   Dec 12 2005 17:21:28   tmollart
//Initial revision.

using System;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Clear cache helper class.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class ClearCacheHelper
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ClearCacheHelper()
		{
		}

		/// <summary>
		/// Clears down the cache of journeys.
		/// </summary>
		public void ClearCache()
		{
			ITDSessionManager sm = TDSessionManager.Current;

			// Clear down cost based data first, then time based leaving time based
			// as the active partition.
			sm.Partition = TDSessionPartition.CostBased;
			ResetSessionObjects();

			sm.Partition = TDSessionPartition.TimeBased;
			ResetSessionObjects();
		}

        /// <summary>
        /// Clears down the cache of journey results leaving request objects intact.
        /// </summary>
        public void ClearJourneyResultCache()
        {
            ITDSessionManager sm = TDSessionManager.Current;

            // Clear down cost based data first, then time based leaving time based
            // as the active partition.
            sm.Partition = TDSessionPartition.CostBased;
            ResetSessionResultObjects();

            sm.Partition = TDSessionPartition.TimeBased;
            ResetSessionResultObjects();
        }

        /// <summary>
        /// Clears down the cache of printable results information
        /// </summary>
        /// <param name="partition">The partition to leave session pointing at</param>
        public void ClearPrintableResultCache(TDSessionPartition partition)
        {
            ITDSessionManager sm = TDSessionManager.Current;

            // Partition is specified to allow calling page to populate page control
            // using data retained in the session parition (e.g. for Find cheaper rail 
            // should leave in cost based)

            if (partition == TDSessionPartition.TimeBased)
            {
                // Clear down cost based data first, then time based leaving time based
                // as the active partition.
                sm.Partition = TDSessionPartition.CostBased;
                ResetSessionPrintableObjects();

                sm.Partition = TDSessionPartition.TimeBased;
                ResetSessionPrintableObjects();
            }
            else
            {
                // Clear down time based data first, then cost based leaving cost based
                // as the active partition.
                sm.Partition = TDSessionPartition.TimeBased;
                ResetSessionPrintableObjects();

                sm.Partition = TDSessionPartition.CostBased;
                ResetSessionPrintableObjects();
            }
        }

		/// <summary>
		/// Resets required session objects.
		/// </summary>
		private void ResetSessionObjects()
		{
			ITDSessionManager sm = TDSessionManager.Current;

			// Objects that create a new instance as required.
			sm.JourneyRequest = null;
			sm.JourneyMapState = null;
			sm.ReturnJourneyMapState = null;
			sm.JourneyViewState = null;
			sm.InputPageState = null;
			sm.FindStationPageState = null;
			sm.StoredMapViewState = null;
            sm.CycleRequest = null;
			
			// Objects that do not create a new instance when required.
			sm.JourneyResult = null;
			sm.AmendedJourneyResult = null;
			sm.CurrentAdjustState = null;
			sm.JourneyParameters = new TDJourneyParametersMulti();
			sm.FindPageState = null;
			sm.AmbiguityResolution = null;
			sm.TravelNewsState = null;
			sm.AsyncCallState = null;
			sm.IsFromNearestCarParks = false;
			sm.JourneyEmissionsPageState = null;
            sm.CycleResult = null;

			// Reset Itinerary
			sm.ItineraryMode = ItineraryManagerMode.None;
			sm.ItineraryManager.NewSearch();

			if	(!(sm.FindAMode == FindAMode.Fare) && !(sm.FindAMode == FindAMode.TrunkCostBased))
			{
				sm.ItineraryManager.PricingRetailOptions = null; 
			}
		}

        /// <summary>
        /// Resets required session objects.
        /// </summary>
        private void ResetSessionResultObjects()
        {
            ITDSessionManager sm = TDSessionManager.Current;

            // Objects that do not create a new instance when required.
            sm.JourneyResult = null;
            sm.AmendedJourneyResult = null;
            sm.CurrentAdjustState = null;
            sm.CycleResult = null;

            // Reset Itinerary
            sm.ItineraryMode = ItineraryManagerMode.None;
            sm.ItineraryManager.NewSearch();

            if (!(sm.FindAMode == FindAMode.Fare) && !(sm.FindAMode == FindAMode.TrunkCostBased))
            {
                sm.ItineraryManager.PricingRetailOptions = null;
            }
        }

        /// <summary>
        /// Resets required printable session objects.
        /// </summary>
        private void ResetSessionPrintableObjects()
        {
            ITDSessionManager sm = TDSessionManager.Current;

            // As the Input page state can contain other information needed by the input pages,
            // only clear out the printable elements within init manually

            //Clear the outward state
            sm.InputPageState.MapUrlOutward = string.Empty;
            sm.InputPageState.MapViewTypeOutward = string.Empty;
            sm.InputPageState.MapTileScaleOutward = 0;
            sm.InputPageState.CycleMapTilesOutward = string.Empty;

            // Reset selected map symbols/icons
            if (sm.InputPageState.IconSelectionOutward != null)
            {
                // Clear the selected icons in the session
                for (int i = 0; i < sm.InputPageState.IconSelectionOutward.Length; i++)
                {
                    for (int j = 0; j < sm.InputPageState.IconSelectionOutward[i].Length; j++)
                    {
                        sm.InputPageState.IconSelectionOutward[i][j] = false;
                    }
                }
            }

            //Clear the return state
            sm.InputPageState.MapUrlReturn = string.Empty;
            sm.InputPageState.MapViewTypeReturn = string.Empty;
            sm.InputPageState.MapTileScaleReturn = 0;
            sm.InputPageState.CycleMapTilesReturn = string.Empty;

            // Reset selected map symbols/icons
            if (sm.InputPageState.IconSelectionReturn != null)
            {
                /// Clear the selected icons in the session.
                for (int i = 0; i < sm.InputPageState.IconSelectionReturn.Length; i++)
                {
                    for (int j = 0; j < sm.InputPageState.IconSelectionReturn[i].Length; j++)
                    {
                        sm.InputPageState.IconSelectionReturn[i][j] = false;
                    }
                }
            }
        }
	}
}