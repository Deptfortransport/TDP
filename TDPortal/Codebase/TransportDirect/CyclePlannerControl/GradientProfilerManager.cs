// *********************************************** 
// NAME			: GradientProfilerManager.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Class which constructs the Gradient profiler calls and waits for the results to be returned
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/GradientProfilerManager.cs-arc  $
//
//   Rev 1.3   Jan 19 2009 11:13:26   mmodi
//Log a gradient profile event
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//
//   Rev 1.2   Aug 22 2008 10:10:02   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 06 2008 14:49:52   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jul 18 2008 13:41:04   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;

using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.CyclePlannerService.GradientProfilerWebService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public class GradientProfilerManager : IGradientProfilerManager
    {
        #region Private members

        private int referenceNumber;
        private int lastSequenceNumber;
        private bool referenceTransaction;
        private string sessionId = string.Empty;
        private string language = string.Empty;

        private int userType;
        private bool loggedOn;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public GradientProfilerManager()
        {
        }

        #endregion

        #region Public methods
        /// <summary>
        /// CallGradientProfiler handles the orchestration of the call to the Gradient Profiler.
        /// </summary>
        /// <param name="request">Encapsulates gradient profiler parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="userType">Used to determine level of logging</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transaction</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        [MTAThread]
        public ITDGradientProfileResult CallGradientProfiler(ITDGradientProfileRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            bool loggedOn,
            string language)
        {
            return CallGradientProfiler(request,
                sessionId,
                userType,
                referenceTransaction,
                0,
                0,
                loggedOn,
                language);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Common private method called by CallGradientProfiler public method
        /// to do the real work of handling the Gradient Profiler call.
        /// </summary>
        private ITDGradientProfileResult CallGradientProfiler(ITDGradientProfileRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            int referenceNumber,
            int lastSequenceNumber,
            bool loggedOn,
            string language)
        {
            #region Set up internal variables
            this.referenceNumber = referenceNumber; // Overwritten below if new/amendment
            this.lastSequenceNumber = lastSequenceNumber; // Overwritten below if amendment
            this.referenceTransaction = referenceTransaction;
            this.sessionId = sessionId;
            this.language = language;
            this.loggedOn = loggedOn;
            this.userType = userType;

            IPropertyProvider propertyProvider = Properties.Current;

            string requestId = string.Empty;

            int callCount = 0;
            bool gradientProfilerFailed = false;
            int gradientProfilerFailureCount = 0;
            #endregion

            #region Determine if we should log Requests/Responses
            // These switches control operational logging of raw Cycle planner inputs and outputs
            bool logAllRequests = false;
            bool logAllResponses = false;

            // Since valid user types are 0, 1, 2 a high value here disables user-type driven
            // logging (so logging of CJP requests controlled by trace level as normal)
            int minimumLoggingUser = 99;

            try
            {
                minimumLoggingUser = Int32.Parse(propertyProvider[CyclePlannerConstants.GradientProfilerMinLoggingUserType]);
            }
            // nothing to do here - just means property hasn't been set yet
            catch (Exception)
            {
            }

            if (userType < minimumLoggingUser)
            {
                logAllRequests = (TDTraceSwitch.TraceVerbose) && (bool.Parse(propertyProvider[CyclePlannerConstants.GradientProfilerLogAllRequests]));
                logAllResponses = (TDTraceSwitch.TraceVerbose) && (bool.Parse(propertyProvider[CyclePlannerConstants.GradientProfilerLogAllResponses]));
            }
            else
            {
                logAllRequests = true;
                logAllResponses = true;
            }
            #endregion

            #region Create Gradient Profiler calls

            #region Determine the reference/sequence numbers to use
            if (request.ReferenceNumber > 0)
            {
                referenceNumber = request.ReferenceNumber;
                lastSequenceNumber = request.SequenceNumber;
            }
            else
            {   // its a new request
                referenceNumber = SqlHelper.GetRefNumInt();
                
                // save to the request
                request.ReferenceNumber = referenceNumber;
                request.SequenceNumber = lastSequenceNumber;
            }

            this.referenceNumber = referenceNumber;
            this.lastSequenceNumber = lastSequenceNumber;
            #endregion

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "GradientProfiler - Populating call list for the GradientProfiler webservice. SessionId = " + sessionId));
            }

            // Create the Gradient Profiler calls
            GradientProfilerRequestPopulator populator = new GradientProfilerRequestPopulator(request);

            GradientProfilerCall[] gradientProfilerCallList = populator.PopulateRequests(
                referenceNumber, lastSequenceNumber, sessionId,
                referenceTransaction, userType, language);

            // Retrieve the formatted reference number from the first call object, there should only be one call
            requestId = ((GradientProfilerCall)gradientProfilerCallList[0]).RequestId;

            #endregion

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "GradientProfiler - Making calls to GradientProfiler webservice. SessionId = " + sessionId));
            }

            // INVOKE THE GRADIENT PROFILER CALLs
            WaitHandle[] wh = new WaitHandle[gradientProfilerCallList.Length];

            foreach (GradientProfilerCall gpCall in gradientProfilerCallList)
            {
                #region Log
                if (logAllRequests)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.CJP,
                        TDTraceLevel.Verbose,
                        ConvertToXML(gpCall.GPRequest, gpCall.RequestId),
                        TDTraceLevelOverride.User));
                }
                #endregion

                wh[callCount] = gpCall.InvokeGradientProfiler();

                if (wh[callCount] == null)
                {
                    gradientProfilerFailed = true;
                }

                callCount++;
            }

            #region Setup the result object

            TDGradientProfileResult result = new TDGradientProfileResult(referenceNumber);
            
            #endregion

            // Timeout value
            int gradientProfilerTimeOut = Int32.Parse(propertyProvider[CyclePlannerConstants.GradientProfilerTimeoutMillisecs]);

            // Wait for parallel Gradient profiler calls to finish.
            // This method will return when either:
            //  - all calls have completed; or
            //  - the timeout period is exceeded.
            if (!gradientProfilerFailed)
            {
                #region Wait for Gradient Profiler calls to return
                //Added code change for multithreaded gradient profiler. The code uses WaitOne instead
                //of WaitAll for returning threads. The timeout is adjusted for remaining threads to 
                //ensure the overall timeout (gradientProfilerTimeOut) is enforced. 

                // ----- WaitOne code -------- 
                string message = String.Empty;
                int startTime, endTime;

                if (TDTraceSwitch.TraceVerbose)
                {
                    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                    stringBuilder.Append("GradientProfiler - GradientProfilerManager Thread has returned, ref no ");
                    stringBuilder.Append(referenceNumber.ToString());
                    stringBuilder.Append(". Remaining Timeout is: ");
                    message = stringBuilder.ToString();
                }

                foreach (ManualResetEvent mh in wh)
                {
                    startTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    mh.WaitOne(gradientProfilerTimeOut, false);
                    endTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    gradientProfilerTimeOut = gradientProfilerTimeOut - (endTime - startTime);

                    Logger.Write(new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Verbose, message + gradientProfilerTimeOut.ToString()));
                }
                // ----- End WaitOne code -------- 

                #endregion

                foreach (GradientProfilerCall gpCall in gradientProfilerCallList)
                {
                    bool thisCallFailed = true;

                    GradientProfileResult gradientProfilerResult = gpCall.GPResult;

                    #region Add to the result object
                    if (gpCall.IsSuccessful)
                    {
                        LogResponse(gpCall.StartTime, loggedOn, sessionId);

                        if (logAllResponses)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose,
                                ConvertToXML(gradientProfilerResult, gpCall.RequestId),
                                TDTraceLevelOverride.User));
                        }

                        // Add the gradient profiler result to our result object, noting if it is ok
                        thisCallFailed = !result.AddResult(gradientProfilerResult, sessionId);
                    }

                    if (thisCallFailed)
                    {
                        gradientProfilerFailureCount++;
                    }

                    #endregion
                }
            } // All GradientProfiler calls processed

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "GradientProfiler - All GradientProfiler calls have completed and processed. SessionId = " + sessionId));
            }

            #region Check for errors
            if (result.TDHeightPoints.Count == 0)
            {
                if (result.Messages.Length == 0)
                {
                    result.AddMessageToArray(string.Empty, CyclePlannerConstants.GradientProfilerNoResults, 0, 0);
                }
            }

            // if we couldn't invoke a GradientProfiler service call
            if (gradientProfilerFailed)
            {
                result.AddMessageToArray(string.Empty, CyclePlannerConstants.GradientProfilerInternalError,
                    CyclePlannerConstants.GPCallError, 0);
            }
            else
            {
                // Check for multiple cycle planner call fails
                if (gradientProfilerFailureCount == gradientProfilerCallList.Length)
                {
                    if (result.Messages.Length == 0)
                    {
                        result.AddMessageToArray(string.Empty, CyclePlannerConstants.GradientProfilerInternalError,
                            CyclePlannerConstants.GPCallError, 0);
                    }

                    gradientProfilerFailed = true;
                }
                else if (gradientProfilerFailureCount > 0)
                {
                    if (result.Messages.Length == 0)
                    {
                        result.AddMessageToArray(string.Empty, CyclePlannerConstants.GradientProfilerPartialReturn,
                            CyclePlannerConstants.GPCallError, 0);
                    }
                }
            }
            #endregion

            return result;
        }

        #region Logging

        /// <summary>
        /// Logs a Gradient Profile Event
        /// </summary>
        /// <param name="result"></param>
        /// <param name="requestId"></param>
        /// <param name="isLoggedOn"></param>
        /// <param name="gradientProfilerFailed"></param>
        /// <param name="sessionId"></param>
        private void LogResponse(DateTime callSubmitted, bool isLoggedOn, string sessionId)
        {
            if (CustomEventSwitch.On("GradientProfileEvent"))
            {
                GradientProfileEvent gpe = new GradientProfileEvent(
                    GradientProfileEventDisplayCategory.Data,
                    callSubmitted,
                    isLoggedOn,
                    sessionId);

                Logger.Write(gpe);
            }
        }
        #endregion

        /// <summary>
        /// Create an XML representtaion of the specified object,
        /// with leading whitespace trimmed, for logging purposes.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="requestId"></param>
        /// <returns>XML string, prefixed by request id</returns>
        private string ConvertToXML(object obj, string requestId)
        {
            XmlSerializer xmls = new XmlSerializer(obj.GetType());
            StringWriter sw = new StringWriter();
            xmls.Serialize(sw, obj);
            sw.Close();
            // strip out leading spaces to conserve space in logging ...
            Regex re = new Regex("\\r\\n\\s+");
            return (requestId + " " + re.Replace(sw.ToString(), "\r\n"));
        }

        #endregion
    }
}
