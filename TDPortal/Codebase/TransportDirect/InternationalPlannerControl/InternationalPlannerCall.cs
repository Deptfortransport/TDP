// *********************************************** 
// NAME			: InternationalPlannerCall.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 02 Feb 2010
// DESCRIPTION	: Class which contains the call and invokes the International planner service
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalPlannerCall.cs-arc  $
//
//   Rev 1.0   Feb 09 2010 09:33:50   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.InternationalPlanner;

using Logger = System.Diagnostics.Trace;
using System.Threading;


namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// Represents a single invocation of the International Planner service by the Portal.	
    /// </summary>
    public class InternationalPlannerCall
    {
        
        #region Private members
        
        private delegate IInternationalPlannerResult JourneyPlanAsyncDelegate(InternationalPlannerRequest request);

        private InternationalPlannerRequest request;
        private bool isReturn;
        private bool success;

        private int referenceNumber;
        private string sessionId;

        private JourneyPlanAsyncDelegate internationalPlannerDelegate;
        private IAsyncResult internationalPlannerDelegateASR;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="request">the JourneyRequest to be passed as input to the International Planner engine</param>
		/// <param name="isReturn">true if for return journey, false for outward</param>
		/// <param name="referenceNumber">Used by International Planner for logging</param>
		/// <param name="sessionId">Used by International Planner for logging</param>
        public InternationalPlannerCall(InternationalPlannerRequest request, bool isReturn, int referenceNumber, string sessionId)
		{
			this.request  = request;
			this.isReturn = isReturn;
			this.referenceNumber = referenceNumber;
			this.sessionId = sessionId;
		}

        #endregion

        #region Call International Planner service
        /// <summary>
        /// Initiates an asynchronous call to the International Planner service itself.
        /// </summary>
        /// <returns>
        /// A WaitHandle that will be used to determine when the call 
        /// has completed, or null if the call generated an exception.
        /// </returns>
        public WaitHandle InvokeInternationalPlanner()
        {
            try
            {
                IInternationalPlanner internationalPlanner = (IInternationalPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerFactory];
                internationalPlannerDelegate = new JourneyPlanAsyncDelegate(internationalPlanner.InternationalJourneyPlan);
                internationalPlannerDelegateASR = internationalPlannerDelegate.BeginInvoke(request, null, null);
                return internationalPlannerDelegateASR.AsyncWaitHandle;
            }
            catch (Exception e)
            {
                string message = "InternationalPlanner - InvokeInternationalPlanner exception after attempting to call web service, for request " + request.RequestID;

                OperationalEvent oe = new OperationalEvent
                    (TDEventCategory.CJP, TDTraceLevel.Error, message , e, sessionId);
                Logger.Write(oe);
                return null;
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read-only - the result of the International Planner call (or null if not sucessful)
        /// </summary>
        public IInternationalPlannerResult Result
        {
            get
            {
                try
                {
                    if (internationalPlannerDelegateASR.IsCompleted)
                    {
                        // Get the result
                        IInternationalPlannerResult result = internationalPlannerDelegate.EndInvoke(internationalPlannerDelegateASR);

                        success = true;

                        return result;
                    }
                    else
                    {
                        Logger.Write(new OperationalEvent
                            (TDEventCategory.Infrastructure,
                            TDTraceLevel.Error,
                            "InternationalPlanner - InternationalPlannerCall has timed out for request: " + request.RequestID,
                            null,
                            sessionId));

                        success = false;
                        return null;
                    }
                }
                catch (Exception e)
                {
                    string message = "InternationPlanner - InternationalPlannerCall exception attempting to get and return InternationalPlannerResult for request: " + request.RequestID;

                    OperationalEvent oe = new OperationalEvent
                        (TDEventCategory.Infrastructure, TDTraceLevel.Error, message, e, sessionId);
                    Logger.Write(oe);
                    success = false;
                    return null;
                }
            }
        }

        /// <summary>
        /// Read-only - the Request used to call the International Planner service  
        /// </summary>
        public InternationalPlannerRequest Request
        {
            get { return request; }
        }

        

        /// <summary>
        /// Read-only - true if International Planner call was successful
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
            get { return request.RequestID; }
        }

        #endregion
    }
}
