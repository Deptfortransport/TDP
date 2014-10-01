// *********************************************** 
// NAME                 : StatusCodes.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/04/2010
// DESCRIPTION          : Class defining status/error codes for journey results and application
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/StatusCodes.cs-arc  $ 
//
//   Rev 1.0   Apr 20 2010 16:39:10   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 20 2010 15:41:20   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//

using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// Class defining status/error codes for journey results and application
    /// </summary>
    public class StatusCodes
    {
        #region Error Codes

        // Error code numbers returned by the CJP ...
        public const int CjpOK = 0;
        public const int CjpNoPublicJourney = 18;
        public const int CjpJourneysRejected = 19;
        public const int CjpRoadEngineOK = 100;
        public const int CjpRoadEngineMin = 100;
        public const int CjpRoadEngineMax = 199;

        // Error code numbers created by the CJPManager itself ...
        public const int CjpCallError = 999;

        // Error code numbers returned by the CyclePlanner service
        public const int CPOK = 100;
        public const int CPUndeterminedError = 1;
        public const int CPSystemException = 2;
        public const int CPOperationNotSupported = 3;
        public const int CPInvalidRequest = 4;
        public const int CPErrorConnectingToDatabase = 5;
        public const int CPNoJourneyCouldBeFound = 6;

        // Error code numbers created by the CyclePlannerManager itself ...
        public const int CPCallError = 9999;

        // Error code number returned by this application
        public const int JPC_SUCCESS = 0;
        public const int JPC_EXCEPTION = -1;
        public const int JPC_NOREQUESTS = -2;
        public const int JPC_REQUESTFAILURE = -3;

        #endregion
    }
}
