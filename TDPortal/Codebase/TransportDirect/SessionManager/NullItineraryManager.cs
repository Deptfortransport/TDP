// *********************************************************
// NAME			: VisitPlannerItineraryManager.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 22/08/2005
// DESCRIPTION	: Null itinerary manager class
//
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/NullItineraryManager.cs-arc  $
//
//   Rev 1.1   Dec 05 2012 13:59:10   mmodi
//Supress unnecessary warnings during compile
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Nov 08 2007 12:48:32   mturner
//Initial revision.
//
//   Rev 1.5   Mar 22 2006 20:27:36   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 22 2006 16:32:10   rhopkins
//Minor code-review fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 17 2006 16:18:30   tolomolaiye
//Fix for IR 3572. Fixed error that occured when planning a Door-to-Door journey immediately after planning a Vist Planner (Day Trip Planner) journey.
//Resolution for 3572: Del 8.1 - H2 Merge - Problems with JourneySummary, Journey Details, JourneyMaps, and JourneyTickets pages.
//
//   Rev 1.2   Nov 24 2005 16:26:38   tmollart
//Updated initialise search method so that a new JourneyParametersMulti object is created.
//Resolution for 3002: UEE - Find A Flight: Input page not refreshed on selecting new search from results page
//Resolution for 3054: UEE - Selecting New Search for Find a Train results does not show blank input screen
//Resolution for 3061: UEE -  Door to door - new search does not clear input page
//
//   Rev 1.1   Oct 29 2005 14:38:22   tmollart
//Added Obsolete method.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 10:59:00   tmollart
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Null itinerary manager.
	/// </summary>
	[Serializable(), CLSCompliant(false)]
	public class NullItineraryManager : TDItineraryManager
	{
		/// <summary>
		/// Constructor. Sets up max itinerary segments. Zero in this case.
		/// </summary>
		public NullItineraryManager()
		{
			//Set up maximum number of itinerary segments. This is differnet from maxSegementSize
			//above which controls the max size of segments and NOT the maximum number of segments.
			maxItinerarySegments = 0;
		}

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
		/// Read only property - get the ItineraryManagerMode
		/// </summary>
		public override ItineraryManagerMode ItineraryMode
		{
			get {return ItineraryManagerMode.None;}
		}

		/// <summary>
		/// Provides functionality to remove the current itinerary and reset session
		/// manager back to the users original journey before an itinerary was created.
		/// No processing required for NullItineraryManager.
		/// </summary>
		public override void ResetToInitialJourney()
		{
			// Do nothing for NullItineraryManager
		}


		#region Obsolete Overridden Methods

#pragma warning disable 809
        [Obsolete("Method is not available in this subclass")]
		public override void AddExtensionToItinerary()
		{
			throw new NotImplementedException("Method obsolete in implementation");
        }
#pragma warning restore 809

        #endregion
    }
}
