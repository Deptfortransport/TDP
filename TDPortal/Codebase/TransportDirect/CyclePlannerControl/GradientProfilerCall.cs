// *********************************************** 
// NAME			: GradientProfilerCall.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/07/2008
// DESCRIPTION	: Class which contains the call and invokes the Gradient Profiler service
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/GradientProfilerCall.cs-arc  $
//
//   Rev 1.2   Jan 19 2009 11:12:42   mmodi
//Added start time property to capture actual start for logging purposes
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//
//   Rev 1.1   Aug 06 2008 14:49:50   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 13:41:02   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.CyclePlannerService.GradientProfilerWebService;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public class GradientProfilerCall
    {
        #region Private members

        private delegate GradientProfileResult JourneyPlanAsyncDelegate(GradientProfileRequest request);

        private GradientProfileRequest request;
        private bool success;
        private DateTime startTime;

        private int referenceNumber;
        private string sessionId;

        private JourneyPlanAsyncDelegate gradientProfilerDelegate;
        private IAsyncResult gradientProfilerDelegateASR;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="request">the GradientProfiler request to be passed as input to the GradientProfiler</param>
		/// <param name="referenceNumber">Used for logging</param>
		/// <param name="sessionId">Used for logging</param>
        public GradientProfilerCall(GradientProfileRequest request, int referenceNumber, string sessionId)
		{
			this.request  = request;
			this.referenceNumber = referenceNumber;
			this.sessionId = sessionId;
		}

        #endregion

         #region Call Gradient Profiler service
        /// <summary>
        /// Initiates an asynchronous call to the Gradient Profiler service itself.
        /// </summary>
        /// <returns>
        /// A WaitHandle that will be used to determine when the call 
        /// has completed, or null if the call generated an exception.
        /// </returns>
        public WaitHandle InvokeGradientProfiler()
        {
            try
            {
                startTime = DateTime.Now;

                ICyclePlanner cyclePlanner = (ICyclePlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.CyclePlannerFactory];
                gradientProfilerDelegate = new JourneyPlanAsyncDelegate(cyclePlanner.GradientProfiler);
                gradientProfilerDelegateASR = gradientProfilerDelegate.BeginInvoke(request, null, null);
                return gradientProfilerDelegateASR.AsyncWaitHandle;
            }
            catch (Exception e)
            {
                OperationalEvent oe = new OperationalEvent
                    (TDEventCategory.CJP, TDTraceLevel.Error, request.requestID + " Exception on Gradient Profiler call", e, sessionId);
                Logger.Write(oe);
                return null;
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read-only - the result of the Gradient Profiler call (or null if not sucessful)
        /// </summary>
        public GradientProfileResult GPResult
        {
            get
            {
                try
                {
                    if (gradientProfilerDelegateASR.IsCompleted)
                    {
                        success = true;

                        return (GradientProfileResult)gradientProfilerDelegate.EndInvoke(gradientProfilerDelegateASR);
                    }
                    else
                    {
                        Logger.Write(new OperationalEvent
                            (TDEventCategory.CJP,
                            TDTraceLevel.Error,
                            "GradientProfiler Call has timed out for request: " + request.requestID,
                            null,
                            sessionId));

                        success = false;
                        return null;
                    }
                }
                catch (Exception e)
                {
                    string message = "GradientProfiler Call exception after attempting to get GradientProfilerResult for request: " + request.requestID;

                    OperationalEvent oe = new OperationalEvent
                        (TDEventCategory.CJP, TDTraceLevel.Error, message, e, sessionId);
                    Logger.Write(oe);
                    success = false;
                    return null;
                }
            }
        }

        /// <summary>
        /// Read-only - the Request used to call the GradientProfiler service  
        /// </summary>
        public GradientProfileRequest GPRequest
        {
            get { return request; }
        }

        /// <summary>
        /// Read-only - true if GradientProfiler call was successful
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

        /// <summary>
        /// Read-only - Returns the start time of when the request was invoked
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
        }
        #endregion
    }
}
