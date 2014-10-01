// *********************************************** 
// NAME			: CyclePlannerCall.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 19/04/2010
// DESCRIPTION	: Class which contains the call and invokes the Cycle planner service
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/CyclePlannerCall.cs-arc  $
//
//   Rev 1.0   Apr 20 2010 16:39:18   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 20 2010 15:41:22   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//

using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text;

using JourneyPlannerCaller.CyclePlannerWebService;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// Represents a single invocation of the Cycle Planner service	
    /// </summary>
    public class CyclePlannerCall
    {
        #region Private members
        
        private delegate CyclePlannerResult JourneyPlanAsyncDelegate(CyclePlannerRequest request);

        private JourneyRequest request;
        private bool success;

        private JourneyPlanAsyncDelegate cyclePlannerDelegate;
        private IAsyncResult cyclePlannerDelegateASR;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="request">the JourneyRequest to be passed as input to the CJP</param>
        public CyclePlannerCall(JourneyRequest request)
		{
			this.request  = request;
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
        public WaitHandle InvokeCyclePlanner(CyclePlanner cyclePlanner)
        {
            try
            {
                cyclePlannerDelegate = new JourneyPlanAsyncDelegate(cyclePlanner.CycleJourneyPlan);
                cyclePlannerDelegateASR = cyclePlannerDelegate.BeginInvoke(request, null, null);

                return cyclePlannerDelegateASR.AsyncWaitHandle;
            }
            catch (Exception ex)
            {
                string message = string.Format(
                    "Exception on CyclePlanner call. RequestId[{0}]. \n Exception Message: {1} \n StackTrace {2}",
                    request.requestID, ex.Message, ex.StackTrace);

                FileLogger.LogMessage(message);
                    
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
                        FileLogger.LogMessage(
                            string.Format("CyclePlannerCall has timed out. RequestId[{0}].",
                            request.requestID));

                        success = false;
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    FileLogger.LogMessage(
                        string.Format("Exception after CyclePlannerCall. RequestId[{0}]. \n Exception Message: {1} \n StackTrace {2}",
                        request.requestID, ex.Message, ex.StackTrace));

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
        /// Read-only - true if Cycle Planner call was successful
        /// </summary>
        public bool IsSuccessful
        {
            get { return success; }
        }

        /// <summary>
        /// Read-only - the request-id 
        /// </summary>
        public string RequestId
        {
            get { return request.requestID; }
        }

        #endregion
    }
}
