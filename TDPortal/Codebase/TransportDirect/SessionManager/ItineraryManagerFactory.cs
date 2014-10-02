// *********************************************************
// NAME			: ItineraryManagerFactory.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 17/08/2005
// DESCRIPTION	: Factory for Itinerary Manager
//
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ItineraryManagerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:30   mturner
//Initial revision.
//
//   Rev 1.2   Mar 14 2006 08:41:40   build
//Automatically merged from branch for stream3353
//
//   Rev 1.1.1.0   Feb 07 2006 19:54:12   tmollart
//Now returns a ReplanItineraryManager as well.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Nov 09 2005 18:16:44   RPhilpott
//Get rid of unreachable code warnings.
//
//   Rev 1.0   Sep 13 2005 10:58:58   tmollart
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Itinerary Manager Factory Class
	/// </summary>
	public class ItineraryManagerFactory
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ItineraryManagerFactory()
		{
		}

		/// <summary>
		/// Returns correct Itinerary Manager for requested mode.
		/// </summary>
		/// <param name="mode">Required Mode (from ItineraryManagerMode enum)</param>
		/// <returns>Itinerary Manager</returns>
		public object Get(ItineraryManagerMode requiredMode)
		{
			//Look at supplied mode and return required Itinerary Manager
			switch (requiredMode)
			{
				case ItineraryManagerMode.None:
					return new NullItineraryManager();

				case ItineraryManagerMode.ExtendJourney:
					return new ExtendItineraryManager();

				case ItineraryManagerMode.VisitPlanner:
					return new VisitPlannerItineraryManager();

				case ItineraryManagerMode.RoadAndRide:
					return new RoadAndRideItineraryManager();

				case ItineraryManagerMode.Replan:
					return new ReplanItineraryManager();

				default:
					return new NullItineraryManager();
			}
		}
	}
}
