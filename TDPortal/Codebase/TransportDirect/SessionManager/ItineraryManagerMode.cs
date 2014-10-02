// *********************************************************
// NAME			: ItineraryManagerMode.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 17/08/2005
// DESCRIPTION	: Enumeration of modes for Itinerary Manager
//
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ItineraryManagerMode.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:30   mturner
//Initial revision.
//
//   Rev 1.1   Mar 14 2006 08:41:40   build
//Automatically merged from branch for stream3353
//
//   Rev 1.0.1.0   Feb 07 2006 19:53:20   tmollart
//Added Replan enumeration.
//
//   Rev 1.0   Sep 13 2005 10:58:58   tmollart
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Enumeration specifying available modes for
	/// Itinerary Manager.
	/// </summary>
	public enum ItineraryManagerMode
	{
		None,
		ExtendJourney,
		VisitPlanner,
		RoadAndRide,
		Replan
	};
}