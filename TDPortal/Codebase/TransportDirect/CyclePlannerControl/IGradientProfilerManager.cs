// *********************************************** 
// NAME			: IGradientProfilerManager.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10 Jul 2008
// DESCRIPTION	: Definition of the IGradientProfilerManager interface
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/IGradientProfilerManager.cs-arc  $
//
//   Rev 1.0   Jul 18 2008 14:00:36   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Interface for the GradientProfilerManager class
    /// </summary>
    public interface IGradientProfilerManager
    {
        /// <summary>
        /// CallCyclePlanner handles the orchestration of the various calls to the Cycle planner.
        /// This overloaded version handles INITIAL requests.
        /// </summary>
        /// <param name="request">Encapsulates gradient profiler parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="userType">Used to determine level of logging</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transaction</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        ITDGradientProfileResult CallGradientProfiler
            (ITDGradientProfileRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            bool loggedOn,
            string language);
    }
}
