// *********************************************************
// NAME			: VisitPlannerItineraryManager.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 26/08/2005
// DESCRIPTION	: Visit Planner Itinerary Manager Remote
//				  interface
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/IVisitPlanItineraryManagerRemote.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:30   mturner
//Initial revision.
//
//   Rev 1.4   Nov 24 2005 16:32:02   tmollart
//Updated to reflect methods removed from VisitPlannerItineraryManager.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 29 2005 14:33:22   tmollart
//Updated method signatures.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 10 2005 18:12:50   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 21 2005 17:13:02   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 10:59:00   tmollart
//Initial revision.

using System;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.SessionManager
{
	
	/// <summary>
	/// Allows remote callers to implement the visit planner itinerary manager.
	/// In place as remote systems wont have access to the http context so
	/// interface provides methods which can be safely used outside of the
	/// http context.
	/// </summary>
	[CLSCompliant(false)]
	public interface IVisitPlannerItineraryManagerRemote
	{
		/// <summary>
		/// Adds a segment to the end of the itinerary.
		/// </summary>
		/// <param name="parameters">Journey parameters</param>
		/// <param name="request">Journey request</param>
		/// <param name="result">Journey result</param>
		/// <param name="journeyState">Journey state</param>
		void AddSegmentToItinerary(TDJourneyParameters parameters, ITDJourneyRequest request, ITDJourneyResult result, TDJourneyState journeyState);


		/// <summary>
		/// Builds the visit plan journey request. Looks at current itinerary length
		/// and works out how to build the request. First journey will take date and
		/// time from parameters where as subsequent journeys will base their departure
		/// times on the previous journey.
		/// </summary>
		/// <param name="parameters">Visit Plan Paramters</param>
		/// <returns>Journey request object containing first journey request</returns>
		ITDJourneyRequest BuildNextRequest(TDJourneyParametersVisitPlan parameters);


		/// <summary>
		/// Adds journeys to an existing itinerary segment. Extend...Earlier/Later needs
		/// to be called first and therefore a segment is not required in the call to
		/// this method.
		/// </summary>
		void AddJourneysToSegment(ITDJourneyResult result);


		/// <summary>
		/// Read Only. The number of journeys requested from the CJP
		/// per segment when building initial intinerary.
		/// </summary>
		int InitialRequestSize{get;}


		/// <summary>
		/// Read Only. The number of journeys to discard from the journeys
		/// returned for each segmetn during initial creation of itinerary.
		/// </summary>
		int InitialRequestDiscardSize{get;}
	}
}
