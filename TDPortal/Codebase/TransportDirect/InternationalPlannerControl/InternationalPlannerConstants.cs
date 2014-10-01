// *********************************************** 
// NAME			: InternationalPlannerConstants.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 02 Feb 2010
// DESCRIPTION	: Constant strings and property keys for International Planner
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalPlannerConstants.cs-arc  $
//
//   Rev 1.0   Feb 09 2010 09:33:52   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// Constant strings and property keys used by InternationalPlannerControl components
    /// </summary>
    public class InternationalPlannerConstants
    {
        #region Property keys

        public const string MinLoggingUserType = "InternationalPlanner.PlannerControl.MinUserTypeLogging";
        public const string LogAllRequests = "InternationalPlanner.PlannerControl.LogAllRequests";
        public const string LogAllResponses = "InternationalPlanner.PlannerControl.LogAllResponses";
        public const string LogNoJourneyResponses = "InternationalPlanner.PlannerControl.LogNoJourneyResponses";
        public const string LogInternationalPlannerFailures = "InternationalPlanner.PlannerControl.LogInternationalPlannerFailures";

        public const string TimeoutMillisecs = "InternationalPlanner.PlannerControl.TimeoutMillisecs";

        #endregion

        #region ResourceIdentifiers

        public const string IPInternalError = "InternationalPlanner.Results.IPInternalError";
        public const string IPPartialReturn = "InternationalPlanner.Results.IPPartialReturn";
        public const string IPNoResults = "InternationalPlanner.Results.IPNoResults";

        /// <summary>
        /// This message must only used to identify messages returned by the International Planner to be
        /// displayed for users on the journey results page 
        /// </summary>
        public const string IPExternalMessage = "InternationalPlanner.Results.IPExternalMessage";
        

        #endregion

        #region Error codes

        // error code numbers returned by the InternationalPlanner service
        public const int IPOK = 100;
        public const int IPUndeterminedError = 1;
        public const int IPSystemException = 2;
        public const int IPOperationNotSupported = 3;
        public const int IPInvalidRequest = 4;
        public const int IPErrorConnectingToDatabase = 5;
        public const int IPNoJourneyCouldBeFound = 6;

        // error code numbers created by the InternationalPlannerManager itself ...
        public const int IPCallError = 9999;
      

        #endregion

        #region Constants

        public const int ExpectedInternationalJourneyCount = 1;

        #endregion
    }
}
