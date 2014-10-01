// *********************************************** 
// NAME			: CyclePlannerConstants.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Constant strings and property keys
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CyclePlannerConstants.cs-arc  $
//
//   Rev 1.5   Jul 09 2009 14:32:06   mmodi
//Updated Cycle Planner OK code to prevent Error log entry being created even when the result was OK
//
//   Rev 1.4   Sep 02 2008 10:38:58   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 22 2008 10:10:00   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 08 2008 12:05:46   mmodi
//Updated as part of workstream
//
//   Rev 1.1   Jul 18 2008 13:37:32   mmodi
//Updates as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:41:58   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Constant strings and property keys used by CyclePlannerControl components
    /// </summary>
    public class CyclePlannerConstants
    {
        #region Property keys

        public const string MinLoggingUserType = "CyclePlanner.PlannerControl.MinUserTypeLogging";
        public const string LogAllRequests = "CyclePlanner.PlannerControl.LogAllRequests";
        public const string LogAllResponses = "CyclePlanner.PlannerControl.LogAllResponses";
        public const string LogNoJourneyResponses = "CyclePlanner.PlannerControl.LogNoJourneyResponses";
        public const string LogCyclePlannerFailures = "CyclePlanner.PlannerControl.LogCyclePlannerFailures";
        
        public const string TimeoutMillisecs = "CyclePlanner.PlannerControl.TimeoutMillisecs";


        public const string GradientProfilerMinLoggingUserType = "GradientProfiler.PlannerControl.MinUserTypeLogging";
        public const string GradientProfilerLogAllRequests = "GradientProfiler.PlannerControl.LogAllRequests";
        public const string GradientProfilerLogAllResponses = "GradientProfiler.PlannerControl.LogAllResponses";
        public const string GradientProfilerLogFailures = "GradientProfiler.PlannerControl.LogFailures";

        public const string GradientProfilerTimeoutMillisecs = "GradientProfiler.PlannerControl.TimeoutMillisecs";

        #endregion

        #region ResourceIdentifiers

        public const string CPInternalError = "CyclePlanner.Results.CPInternalError";
        public const string CPPartialReturn = "CyclePlanner.Results.CPPartialReturn";
        public const string CPNoResults = "CyclePlanner.Results.CPNoResults";

        /// <summary>
        /// This message must only used to identify messages returned by the Cycle Planner to be
        /// displayed for CJP users on the journey results page 
        /// </summary>
        public const string CPExternalMessage = "CyclePlanner.Results.CPExternalMessage";

        public const string GradientProfilerInternalError = "GradientProfiler.Results.GPInternalError";
        public const string GradientProfilerPartialReturn = "GradientProfiler.Results.GPPartialReturn";
        public const string GradientProfilerNoResults = "GradientProfiler.Results.GPNoResults";

        #endregion

        #region Error codes

        // error code numbers returned by the CyclePlanner service
        public const int CPOK = 100;
        public const int CPUndeterminedError = 1;
        public const int CPSystemException = 2;
        public const int CPOperationNotSupported = 3;
        public const int CPInvalidRequest = 4;
        public const int CPErrorConnectingToDatabase = 5;
        public const int CPNoJourneyCouldBeFound = 6;

        // error code numbers created by the CyclePlannerManager itself ...
        public const int CPCallError = 9999;

        // error code numbers returned by the GradientProfiler service
        public const int GPOK = 0;

        // error code numbers created by the GradientProfilerManager itself ...
        public const int GPCallError = 19999;

        #endregion

        #region Constants
        
        public const int ExpectedCycleJourneyCount = 1;

        #endregion
    }
}
