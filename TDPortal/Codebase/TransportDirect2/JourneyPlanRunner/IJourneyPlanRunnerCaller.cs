// *********************************************** 
// NAME             : IJourneyPlanRunnerCaller.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 29 Mar 2011
// DESCRIPTION  	: Provides interface to invoke CJP manager
// ************************************************
                
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.UserPortal.SessionManager;
using TDP.UserPortal.JourneyControl;

namespace TDP.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// Provides interface to invoke CJP manager
    /// </summary>
    interface IJourneyPlanRunnerCaller
    {
        ///<summary>
        ///Invoke the CJP Manager for a new journey request
        ///</summary>
        /// <param name="journeyRequest">Journey request</param>
        /// <param name="sessionInfo">Current session information</param>
        /// <param name="lang">Two-character ISO id ("en" or "fr") of the current UI language</param>
        void InvokeCJPManager(ITDPJourneyRequest journeyRequest, ITDPSession sessionInfo, string lang);

    }
}
