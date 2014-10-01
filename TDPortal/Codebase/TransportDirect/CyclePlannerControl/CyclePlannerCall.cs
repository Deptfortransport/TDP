// *********************************************** 
// NAME			: CyclePlannerCall.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Class which contains the call and invokes the Cycle planner service
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CyclePlannerCall.cs-arc  $
//
//   Rev 1.4   Sep 02 2008 10:40:42   mmodi
//Return a CyclePlannerResult because when casted to a JourneyResult this can fail if the cycle service had a problem. Allows us to capture and log the messages returned by the Cycle manager 
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 08 2008 12:08:06   mmodi
//Updated to log messages returned
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 06 2008 14:49:50   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 04 2008 10:19:44   mmodi
//Updates to work with actual web service
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:41:58   mmodi
//Initial revision.
//

using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Represents a single invocation of the Cycle Planner service by the Portal.	
    /// </summary>
    public class CyclePlannerCall
    {
        #region Private members
        
        private delegate CyclePlannerResult JourneyPlanAsyncDelegate(CyclePlannerRequest request);

        private JourneyRequest request;
        private bool isReturn;
        private bool success;

        private int referenceNumber;
        private string sessionId;

        private JourneyPlanAsyncDelegate cyclePlannerDelegate;
        private IAsyncResult cyclePlannerDelegateASR;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="request">the JourneyRequest to be passed as input to the CJP</param>
		/// <param name="isReturn">true if for return journey, false for outward</param>
		/// <param name="referenceNumber">Used by CJP for logging</param>
		/// <param name="sessionId">Used by CJP for logging</param>
        public CyclePlannerCall(JourneyRequest request, bool isReturn, int referenceNumber, string sessionId)
		{
			this.request  = request;
			this.isReturn = isReturn;
			this.referenceNumber = referenceNumber;
			this.sessionId = sessionId;
		}

        #endregion

        #region Call Cycle Planner service
        /// <summary>
        /// Initiates an asynchronous call to the Cycle Planner service itself.
        /// </summary>
        /// <returns>
        /// A WaitHandle that will be used to determine when the call 
        /// has completed, or null if the call generated an exception.
        /// </returns>
        public WaitHandle InvokeCyclePlanner()
        {
            try
            {
                ICyclePlanner cyclePlanner = (ICyclePlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.CyclePlannerFactory];
                cyclePlannerDelegate = new JourneyPlanAsyncDelegate(cyclePlanner.CycleJourneyPlan);
                cyclePlannerDelegateASR = cyclePlannerDelegate.BeginInvoke(request, null, null);
                return cyclePlannerDelegateASR.AsyncWaitHandle;
            }
            catch (Exception e)
            {
                string message = "CyclePlanner - InvokeCyclePlanner exception after attempting to call web service, for request " + request.requestID;

                OperationalEvent oe = new OperationalEvent
                    (TDEventCategory.CJP, TDTraceLevel.Error, message , e, sessionId);
                Logger.Write(oe);
                return null;
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read-only - the result of the Cycle Planner call (or null if not sucessful)
        /// </summary>
        public CyclePlannerResult Result
        {
            get
            {
                try
                {
                    if (cyclePlannerDelegateASR.IsCompleted)
                    {
                        // Get the result
                        CyclePlannerResult result = cyclePlannerDelegate.EndInvoke(cyclePlannerDelegateASR);

                        success = true;

                        return result;
                    }
                    else
                    {
                        Logger.Write(new OperationalEvent
                            (TDEventCategory.CJP,
                            TDTraceLevel.Error,
                            "CyclePlanner - CyclePlannerCall has timed out for request: " + request.requestID,
                            null,
                            sessionId));

                        success = false;
                        return null;
                    }
                }
                catch (Exception e)
                {
                    string message = "CyclePlanner - CyclePlannerCall exception attempting to get and return CyclePlannerResult for request: " + request.requestID;

                    OperationalEvent oe = new OperationalEvent
                        (TDEventCategory.CJP, TDTraceLevel.Error, message, e, sessionId);
                    Logger.Write(oe);
                    success = false;
                    return null;
                }
            }
        }

        /// <summary>
        /// Read-only - the Request used to call the Cycle Planner service  
        /// </summary>
        public JourneyRequest Request
        {
            get { return request; }
        }

        /// <summary>
        /// Read-only - true if request was for return journey, false if outward 
        /// </summary>
        public bool IsReturnJourney
        {
            get { return isReturn; }
        }

        /// <summary>
        /// Read-only - true if Cycle Planner call was successful
        /// </summary>
        public bool IsSuccessful
        {
            get { return success; }
        }

        /// <summary>
        /// Read-only - the request-id 
        /// Portal query reference, plus 4-digit suffix to make unique per-request
        /// </summary>
        public string RequestId
        {
            get { return request.requestID; }
        }

        #endregion
    }
}
