// *********************************************** 
// NAME             : IStopEventRunnerCaller.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 26 Apr 2011
// DESCRIPTION  	: Provides interface to invoke Stop Event manager
// ************************************************
// 

using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.SessionManager;

namespace TDP.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// Provides interface to invoke Stop Event manager
    /// </summary>
    public interface IStopEventRunnerCaller
    {
        ///<summary>
        ///Invoke the Stop Event Manager for a new journey request
        ///</summary>
        /// <param name="journeyRequest">Journey request for stop event request</param>
        /// <param name="sessionInfo">Current session information</param>
        /// <param name="lang">Two-character ISO id ("en" or "fr") of the current UI language</param>
        void InvokeStopEventManager(ITDPJourneyRequest journeyRequest, ITDPSession sessionInfo, string lang);
    }
}
