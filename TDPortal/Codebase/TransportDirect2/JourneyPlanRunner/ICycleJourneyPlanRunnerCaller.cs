// *********************************************** 
// NAME             : ICycleJourneyPlanRunnerCaller.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Apr 2011
// DESCRIPTION  	: Provides interface to invoke Cycle Planner manager
// ************************************************
// 

using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.SessionManager;

namespace TDP.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// Provides interface to invoke Cycle Planner manager
    /// </summary>
    interface ICycleJourneyPlanRunnerCaller
    {
        ///<summary>
        ///Invoke the Cycle Planner Manager for a new journey request
        ///</summary>
        /// <param name="journeyRequest">Journey request</param>
        /// <param name="sessionInfo">Current session information</param>
        /// <param name="lang">Two-character ISO id ("en" or "fr") of the current UI language</param>
        void InvokeCyclePlannerManager(ITDPJourneyRequest journeyRequest, ITDPSession sessionInfo, string lang);
    }
}
