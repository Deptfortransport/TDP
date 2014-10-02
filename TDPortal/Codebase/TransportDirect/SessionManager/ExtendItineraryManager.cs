// *********************************************************
// NAME			: ExtendItineraryManager.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 17/08/2005
// DESCRIPTION	: Itinerary Manager for Extend Journey 
//                functionality
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ExtendItineraryManager.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:22   mturner
//Initial revision.
//
//   Rev 1.9   Mar 22 2006 20:27:36   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Mar 15 2006 14:21:10   rhopkins
//Fix merge error  maxItinerarySegments
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Mar 14 2006 17:16:56   tmollart
//Post merge fixes. Stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Mar 14 2006 11:24:54   tmollart
//Manual merge of stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 17 2006 16:18:30   tolomolaiye
//Fix for IR 3572. Fixed error that occured when planning a Door-to-Door journey immediately after planning a Vist Planner (Day Trip Planner) journey.
//Resolution for 3572: Del 8.1 - H2 Merge - Problems with JourneySummary, Journey Details, JourneyMaps, and JourneyTickets pages.
//
//   Rev 1.4   Feb 10 2006 15:04:32   build
//Automatically merged from branch for stream3180
//
//   Rev 1.3.1.1   Dec 22 2005 09:28:00   tmollart
//Removed commented out code.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.3.1.0   Dec 12 2005 17:31:20   tmollart
//Modified to return FindAMode as OldFindAMode no longer in use.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.3   Nov 09 2005 18:57:50   RPhilpott
//Merge with stream2818
//
//   Rev 1.2   Oct 29 2005 14:37:16   tmollart
//Updates to code from code review and also tidy up.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 21 2005 17:11:46   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 10:58:58   tmollart
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TDExtendItineraryManager.
	/// </summary>
	[Serializable(), CLSCompliant(false)]
	public class ExtendItineraryManager : TDItineraryManager
	{

		#region Constructor

		/// <summary>
		/// Constructor. Sets up max number of segments and reqired 
		/// storage array.
		/// </summary>
		public ExtendItineraryManager()
		{
			//Set up maximum number of itinerary segments. This is differnet from maxSegementSize
			//above which controls the max size of segments and NOT the maximum number of segments.
			maxItinerarySegments = 3;

			//Create array of Segment Store objects up to maximum for this mode.
			itineraryArray = new ExtendSegmentStore[maxItinerarySegments];
		}

		#endregion

		#region Public/Private Methods

		/// <summary>
		/// Initialise Search. Calls base class implentation
		/// to reset all required objects for a new search
		/// </summary>
		protected override void InitialiseSearch()
		{
			TDJourneyParameters newJourneyParameters = new TDJourneyParametersMulti();
			TDSessionManager.Current.JourneyParameters = newJourneyParameters;

			base.InitialiseSearch ();
		}


		/// <summary>
		/// Clears the current itinerary and reinitialises the segment
		/// store with new blank segements.
		/// </summary>
		protected override void ClearItinerary()
		{
			//Create array of Segment Store objects up to maximum for this mode.
			itineraryArray = new ExtendSegmentStore[maxItinerarySegments];
			base.ClearItinerary ();
		}

		
		/// <summary>
		/// Returns the Find a Mode used to plan the results or a particular itinerary segment.
		/// Returns FindAMode.None is index out of range.
		/// </summary>
		/// <param name="segmentIndex">Index of segment for interogation.</param>
		/// <returns>Find a Mode</returns>
		public virtual FindAMode SpecificFindAMode(int segmentIndex)
		{
			if ((segmentIndex >= 0) && (segmentIndex < this.Length))
			{
				return ((ExtendSegmentStore)itineraryArray[segmentIndex]).FindAMode;
			}
			else
			{
				return FindAMode.None;
			}
		}


		/// <summary>
		/// Add the current Journey Request/Results to the Itinerary.	
		/// </summary>
		public override void AddExtensionToItinerary()
		{
			if (itineraryLength < maxItinerarySegments)
			{
				if (ExtendEndOfItinerary)
				{

					itineraryArray[itineraryLength] = new ExtendSegmentStore();
					((ExtendSegmentStore)itineraryArray[itineraryLength]).CopyJourneyFromSession();

					latestItinerarySegment = itineraryLength;

					if ((TDSessionManager.Current.JourneyResult.OutwardPublicJourneyCount > 0) || (TDSessionManager.Current.JourneyResult.OutwardRoadJourneyCount > 0))
					{
						outwardLast++;
					}
					if ((TDSessionManager.Current.JourneyResult.ReturnPublicJourneyCount > 0) || (TDSessionManager.Current.JourneyResult.ReturnRoadJourneyCount > 0))
					{
						returnLast++;
					}
				}
				else
				{
					Array.Copy(itineraryArray,0,itineraryArray,1,itineraryLength);

					itineraryArray[0] = new ExtendSegmentStore();
					((ExtendSegmentStore)itineraryArray[0]).CopyJourneyFromSession();

					latestItinerarySegment = 0;

					initialJourney++;

					// If a particular type (Outward/Return) of journey is not present then the start point for that type will have been advanced in the array
					if (this.OutwardLength > 0)
					{
						outwardLast++;
						if ((TDSessionManager.Current.JourneyResult.OutwardPublicJourneyCount == 0) && (TDSessionManager.Current.JourneyResult.OutwardRoadJourneyCount == 0))
						{
							outwardFirst++;
						}
					}
					if (this.ReturnLength > 0)
					{
						returnLast++;
						if ((TDSessionManager.Current.JourneyResult.ReturnPublicJourneyCount == 0) && (TDSessionManager.Current.JourneyResult.ReturnRoadJourneyCount == 0))
						{
							returnFirst++;
						}
					}
				}

				// Increase the length of the itinerary.
				itineraryLength++;

				// Reinitialise the parameters used to plan the journey
				InitialiseSearch();

				// We are no longer performing an extend. We have finished so this needs
				// to be set to false.
				extendInProgress = false;

				// Default to display of "Full" Itinerary
				SelectedItinerarySegment = -1;

				// Serialise the data base to deferred storage.
				SerializeAndSetModechange();
			}
		}

		/// <summary>
		/// Read only property - get the ItineraryManagerMode
		/// </summary>
		public override ItineraryManagerMode ItineraryMode
		{
			get {return ItineraryManagerMode.ExtendJourney;}
		}


		/// <summary>
		/// Restores the Initial Journey into the Working area and clears the Itinerary
		/// </summary>
		public override void ResetToInitialJourney()
		{
			if (this.Length > 0)
			{
				InitialiseSearch();
			
				((ExtendSegmentStore)itineraryArray[initialJourney]).CopyJourneyToSession();

				ClearItinerary();

				SerializeAndSetModechange();
			}
		}


		/// <summary>
		/// Restores the latest Extension to the working area and removes it from the Itinerary.
		/// This allows the User to reselect the journey that they wish to add.
		/// </summary>
		public void ResetLastExtension()
		{
			if ((itineraryLength > 0) && (latestItinerarySegment != -1))
			{
				InitialiseSearch();

				((ExtendSegmentStore)itineraryArray[latestItinerarySegment]).CopyJourneyToSession();

				DeleteSegmentWithoutCleanup(latestItinerarySegment);

				if (itineraryLength > 0)
				{
					extendInProgress = true;
				}

				SerializeAndSetModechange();
			}
		}

		#endregion

	}
}
