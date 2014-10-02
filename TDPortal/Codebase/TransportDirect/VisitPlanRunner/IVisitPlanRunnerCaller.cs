// *********************************************************
// NAME			: IVisitPlanRunnerCaller.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 06/09/2005
// DESCRIPTION	: Visit Plan Runner Caller Interface. 
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/VisitPlanRunner/IVisitPlanRunnerCaller.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:51:10   mturner
//Initial revision.
//
//   Rev 1.1   Sep 21 2005 17:22:36   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 12:16:04   tmollart
//Initial revision.

using System;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.VisitPlanRunner
{
	
	public interface IVisitPlanRunnerCaller
	{

		/// <summary>
		/// Peforms validation of journey parameters and if valid creates and
		/// plans an itinerary. Journey params are accessed through reference
		/// to session manager that is passed in.
		/// </summary>
		/// <param name="SessionManager">Current session manager instance</param>
		/// <returns>Boolean indicating success/failure</returns>
		void RunInitialItinerary(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition);


		/// <summary>
		/// Performs planning of earlier/later journeys and adding of those
		/// journeys to an itinerary segment. ExtendJourneyResultEarlier/Later
		/// to generate a TDJourneyResult object.
		/// </summary>
		/// <param name="SessionManager">Current session manager instance</param>
		void RunAddJourneys(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition);
	}
}
