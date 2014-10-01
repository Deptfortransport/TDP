// *********************************************** 
// NAME			: CyclePlannerManager.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Class which constructs the Cycle planner calls and waits for the results to be returned
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CyclePlannerManager.cs-arc  $
//
//   Rev 1.7   Sep 29 2010 11:26:12   apatel
//EES WebServices for Cycle Code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.6   Oct 27 2008 14:03:42   mturner
//Changed to not create an ESRI mapping object for injected transactions.
//
//   Rev 1.5   Sep 08 2008 15:45:48   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Sep 02 2008 10:35:32   mmodi
//Updated to log any errors returned by the cycle result before converting and adding to the Result object
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 22 2008 10:10:00   mmodi
//Updated
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
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Class which orchestrates the various calls to the Cycle planner
    /// </summary>
    public class CyclePlannerManager : ICyclePlannerManager
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
        public CyclePlannerManager()
        {
        }

        #endregion

        #region Public methods
        /// <summary>
        /// CallCyclePlanner handles the orchestration of the various calls to the Cycle planner.
        /// This overloaded version handles INITIAL requests.
        /// </summary>
        /// <param name="request">Encapsulates cycle journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="userType">Used to determine level of logging</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transaction</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <param name="polylinesTransformXslt">The path of the xslt file used to transform the Cycle jouney 
        /// xml in to xml passed to Mapping</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        [MTAThread]
        public ITDCyclePlannerResult CallCyclePlanner(ITDCyclePlannerRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            bool loggedOn,
            string language,
            string polylinesTransformXslt)
        {
            return CallCyclePlanner(request,
                sessionId,
                userType,
                false,
                referenceTransaction,
                0,
                0,
                loggedOn,
                language,
                polylinesTransformXslt, false);
        }

        /// <summary>
        /// CallCyclePlanner handles the orchestration of the various calls to the Cycle planner
        /// This overloaded version handles AMENDMENTS to an existing journey.
        /// </summary>
        /// <param name="request">Encapsulates journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transacation</param>
        /// <param name="referenceNumber">Returned by the initial enquiry</param>
        /// <param name="lastSequenceNumber">Incremented by calling code on each amendment request</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <param name="polylinesTransformXslt">The path of the xslt file used to transform the Cycle jouney 
        /// xml in to xml passed to Mapping</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        [MTAThread]
        public ITDCyclePlannerResult CallCyclePlanner(ITDCyclePlannerRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            int referenceNumber,
            int lastSequenceNumber,
            bool loggedOn,
            string language,
            string polylinesTransformXslt)
        {
            return CallCyclePlanner(request,
                sessionId,
                userType,
                true,
                referenceTransaction,
                referenceNumber,
                lastSequenceNumber,
                loggedOn,
                language,
                polylinesTransformXslt, false);
        }

        /// <summary>
        /// CallCyclePlanner handles the orchestration of the various calls to the Cycle planner
        /// This overloaded version handles AMENDMENTS to an existing journey.
        /// </summary>
        /// <param name="request">Encapsulates journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transacation</param>
        /// <param name="referenceNumber">Returned by the initial enquiry</param>
        /// <param name="lastSequenceNumber">Incremented by calling code on each amendment request</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <param name="polylinesTransformXslt">The path of the xslt file used to transform the Cycle jouney 
        /// xml in to xml passed to Mapping</param>
        /// <param name="eesRequest">True if request if from enhanced exposed services</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        [MTAThread]
        public ITDCyclePlannerResult CallCyclePlanner(ITDCyclePlannerRequest request,
            string sessionId,
            int userType,
            bool referenceTransaction,
            bool loggedOn,
            string language,
            string polylinesTransformXslt,
            bool eesRequest)
        {
            return CallCyclePlanner(request,
                sessionId,
                userType,
                true,
                referenceTransaction,
                0,
                0,
                loggedOn,
                language,
                polylinesTransformXslt, eesRequest);
        }


        #endregion

        #region Private methods

        /// <summary>
        /// Common private method called by CallCyclePlanner public method
        /// to do the real work of handling the Cycle planner call.
        /// </summary>
        private ITDCyclePlannerResult CallCyclePlanner(ITDCyclePlannerRequest request,
            string sessionId,
            int userType,
            bool amendment,
            bool referenceTransaction,
            int referenceNumber,
            int lastSequenceNumber,
            bool loggedOn,
            string language,
            string polylinesTransformXslt,
            bool eesRequest)
        {
            #region Set up internal variables
            this.referenceNumber = referenceNumber;
            this.referenceTransaction = referenceTransaction;
            this.sessionId = sessionId;
            this.lastSequenceNumber = lastSequenceNumber;
            this.language = language;
            this.loggedOn = loggedOn;
            this.userType = userType;

            IPropertyProvider propertyProvider = Properties.Current;

            string requestId = string.Empty;

            int callCount = 0;
            bool cyclePlannerFailed = false;
            int cyclePlannerFailureCount = 0;
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
                minimumLoggingUser = Int32.Parse(propertyProvider[CyclePlannerConstants.MinLoggingUserType]);
            }
            // nothing to do here - just means property hasn't been set yet
            catch (Exception)
            {
            }

            if (userType < minimumLoggingUser)
            {
                logAllRequests = (TDTraceSwitch.TraceVerbose) && (bool.Parse(propertyProvider[CyclePlannerConstants.LogAllRequests]));
                logAllResponses = (TDTraceSwitch.TraceVerbose) && (bool.Parse(propertyProvider[CyclePlannerConstants.LogAllResponses]));
            }
            else
            {
                logAllRequests = true;
                logAllResponses = true;
            }
            #endregion

            #region Create cycle planner calls

            // if it is an amendment, the previous reference number will have been passed back in 
            // for reuse here, otherwise we need to get hold of a new one.
            if (!amendment)
            {
                referenceNumber = SqlHelper.GetRefNumInt();
                this.referenceNumber = referenceNumber;
            }

            // Create the Cycle planner calls
            CyclePlannerRequestPopulator populator = new CyclePlannerRequestPopulator(request);

            CyclePlannerCall[] cyclePlannerCallList = populator.PopulateRequests(
                referenceNumber, lastSequenceNumber, sessionId,
                referenceTransaction, userType, language);

            // Retrieve the formatted reference number from the first call object, there should only be one call
            requestId = ((CyclePlannerCall)cyclePlannerCallList[0]).RequestId;

            #endregion

            // INVOKE THE CYCLE PLANNER CALLs
            WaitHandle[] wh = new WaitHandle[cyclePlannerCallList.Length];

            foreach (CyclePlannerCall cpCall in cyclePlannerCallList)
            {
                #region Log
                LogRequest((TDCyclePlannerRequest)request, cpCall.RequestId, loggedOn, sessionId);

                if (logAllRequests)
                {
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose,
                            "CyclePlanner - CyclePlannerManager, logging the cycle journey request to submit."));
                    }

                    Logger.Write(new OperationalEvent(TDEventCategory.CJP,
                        TDTraceLevel.Verbose,
                        ConvertToXML(cpCall.Request, cpCall.RequestId),
                        TDTraceLevelOverride.User));
                }
                #endregion

                wh[callCount] = cpCall.InvokeCyclePlanner();

                if (wh[callCount] == null)
                {
                    cyclePlannerFailed = true;
                }

                callCount++;
            }

            #region Setup the result object
            // This will force a user to have amend the same route 200 times before overflowing and
            // possibly have remnants of journey items from previous route when plotted on the map.
            TDCyclePlannerResult result;
            if (request.IsReturnRequired)
            {
                result = new TDCyclePlannerResult(referenceNumber, referenceNumber * 200, (request.OutwardDateTime.Length == 0 ? null : request.OutwardDateTime[0]), (request.ReturnDateTime.Length == 0 ? null : request.ReturnDateTime[0]), request.OutwardArriveBefore, request.ReturnArriveBefore);
            }
            else
            {
                result = new TDCyclePlannerResult(referenceNumber, referenceNumber * 200, (request.OutwardDateTime.Length == 0 ? null : request.OutwardDateTime[0]), null, request.OutwardArriveBefore, false);
            }
            #endregion

            // Timeout value
            int cyclePlannerTimeOut = Int32.Parse(propertyProvider[CyclePlannerConstants.TimeoutMillisecs]);

            // Wait for parallel Cycle planner calls to finish.
            // This method will return when either:
            //  - all calls have completed; or
            //  - the timeout period is exceeded.
            if (!cyclePlannerFailed)
            {
                #region Wait for Cycle planner calls to return
                //Added code change for multithreaded journey planning. The code uses WaitOne instead
                //of WaitAll for returning threads. The timeout is adjusted for remaining threads to 
                //ensure the overall timeout (cyclePlannerTimeOut) is enforced. 

                // ----- WaitOne code -------- 
                string message = String.Empty;
                int startTime, endTime;

                if (TDTraceSwitch.TraceVerbose)
                {
                    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                    stringBuilder.Append("CyclePlanner - CyclePlannerManager Thread has returned, ref no ");
                    stringBuilder.Append(referenceNumber.ToString());
                    stringBuilder.Append(". Remaining Timeout is: ");
                    message = stringBuilder.ToString();
                }

                foreach (ManualResetEvent mh in wh)
                {
                    startTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    mh.WaitOne(cyclePlannerTimeOut, false);
                    endTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    cyclePlannerTimeOut = cyclePlannerTimeOut - (endTime - startTime);

                    Logger.Write(new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Verbose, message + cyclePlannerTimeOut.ToString()));
                }
                // ----- End WaitOne code -------- 


                // The following code should replace the above WaitOne block if we move away from an App Server 
                // environment. This will not likely happen. See Tracker IR2752 for history on issue.

                // ----- WaitAll code -------- 
                //WaitHandle.WaitAll(wh, cjpTimeOut, false);

                //Logger.Write(new OperationalEvent
                //    (TDEventCategory.Business, TDTraceLevel.Verbose, "CJPManager WaitAll has returned, ref no " + referenceNumber.ToString()));
                // ----- End WaitAll code -------- 

                #endregion

                foreach (CyclePlannerCall cpCall in cyclePlannerCallList)
                {
                    bool thisCallFailed = true;

                    // Get the result from the call
                    CyclePlannerResult cyclePlannerResult = cpCall.Result;
                    JourneyResult journeyResult = null;

                    #region Add the journey to the result object
                    if (cpCall.IsSuccessful)
                    {
                        #region Convert result in to JourneyResult
                        try
                        {
                            // Convert the result into a journey result so we can get at the cycle journeys
                            journeyResult = (JourneyResult)cyclePlannerResult;

                            #region Log result
                            if (logAllResponses)
                            {
                                if (TDTraceSwitch.TraceVerbose)
                                {
                                    Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose,
                                        "CyclePlanner - CyclePlannerManager, logging the cycle journey result received."));
                                }

                                Logger.Write(new OperationalEvent(TDEventCategory.CJP, TDTraceLevel.Verbose,
                                    ConvertToXML(journeyResult, cpCall.RequestId),
                                    TDTraceLevelOverride.User));
                            }
                            #endregion
                        }
                        catch
                        {
                            #region Log error

                            // If there is an exception then result returned by the cycle planner was invalid
                            Logger.Write(new OperationalEvent
                                        (TDEventCategory.CJP, TDTraceLevel.Error,
                                         "CyclePlanner - CyclePlannerManager, exception occurred attempting to cast CyclePlannerResult into a JourneyResult, for request: " + cpCall.RequestId));

                            // Log any messages contained in the result 
                            if ((cyclePlannerResult != null) && (cyclePlannerResult.messages != null) && (cyclePlannerResult.messages.Length > 0))
                            {
                                // Determine if we need to show any messages for CJP users on the Portal results page
                                bool showExternalMessages = (userType >= Int32.Parse(Properties.Current[CyclePlannerConstants.MinLoggingUserType]));

                                foreach (Message msg in cyclePlannerResult.messages)
                                {
                                    Logger.Write(new OperationalEvent
                                        (TDEventCategory.CJP, TDTraceLevel.Verbose,
                                         "CyclePlanner - CyclePlannerResult included message Code: " + msg.code.ToString()
                                         + ",  Message: " + msg.description + ". For request: " + cpCall.RequestId));

                                    if (showExternalMessages)
                                    {
                                        result.AddMessageToArray(msg.description, CyclePlannerConstants.CPExternalMessage, msg.code, 0);
                                    }

                                }
                            }

                            #endregion
                        }

                        #endregion

                        TDLocation cycleViaLocation = null;

                        if (request.CycleViaLocations != null && request.CycleViaLocations.Length > 0)
                        {
                            cycleViaLocation = request.CycleViaLocations[0];
                        }

                        // Add the cycle journey to our result object, noting if it is ok
                        thisCallFailed = !result.AddResult(journeyResult,
                            !(cpCall.IsReturnJourney), cycleViaLocation,
                            request.OriginLocation, request.DestinationLocation, sessionId, userType,
                            polylinesTransformXslt, referenceTransaction,request.ResultSettings,eesRequest);
                    }

                    if (thisCallFailed)
                    {
                        cyclePlannerFailureCount++;
                    }
                    #endregion
                }
            } // All cycle planner calls processed

            #region Check for errors
            if (result.OutwardCycleJourneyCount == 0)
            {
                if (result.CyclePlannerValidError)
                {
                    if (result.CyclePlannerMessages.Length == 0)
                    {
                        result.AddMessageToArray(string.Empty, CyclePlannerConstants.CPNoResults, 0, 0);
                    }
                }
            }

            if (request.IsReturnRequired)
            {
                if (result.ReturnCycleJourneyCount == 0)
                {
                    if (result.CyclePlannerValidError)
                    {
                        if (result.CyclePlannerMessages.Length == 0)
                        {
                            result.AddMessageToArray(string.Empty, CyclePlannerConstants.CPNoResults, 0, 0);
                        }
                    }
                }
            }
            
            // if we couldn't invoke a cycle planner service call
            if (cyclePlannerFailed)
            {
                result.AddMessageToArray(string.Empty, CyclePlannerConstants.CPInternalError,
                    CyclePlannerConstants.CPCallError, 0);
            }
            else
            {
                // Check for multiple cycle planner call fails
                if (cyclePlannerFailureCount == cyclePlannerCallList.Length)
                {
                    if (result.CyclePlannerMessages.Length == 0)
                    {
                        result.AddMessageToArray(string.Empty, CyclePlannerConstants.CPInternalError,
                            CyclePlannerConstants.CPCallError, 0);
                    }

                    cyclePlannerFailed = true;
                }
                else if (cyclePlannerFailureCount > 0)
                {
                    if (result.CyclePlannerMessages.Length == 0)
                    {
                        result.AddMessageToArray(string.Empty, CyclePlannerConstants.CPPartialReturn,
                            CyclePlannerConstants.CPCallError, 0);
                    }
                }
            }
            #endregion

            LogResponse(result, requestId, loggedOn, cyclePlannerFailed, sessionId);

            return result;
        }
        
        #region MIS Logging 
        /// <summary>
        /// Logs a Cycle Planner request event
        /// </summary>
        private void LogRequest(TDCyclePlannerRequest request, string requestId, bool isLoggedOn, string sessionId)
        {
            // Log a request event.
            if (CustomEventSwitch.On("CyclePlannerRequestEvent"))
            {
                CyclePlannerRequestEvent cpre = new CyclePlannerRequestEvent(requestId,
                    isLoggedOn,
                    sessionId);
                Logger.Write(cpre);
            }
        }

        /// <summary>
        /// Logs a Cycle Planner result event
        /// </summary>
        private void LogResponse(TDCyclePlannerResult result, string requestId, bool isLoggedOn,
            bool cyclePlannerFailed, string sessionId)
        {
            if (CustomEventSwitch.On("CyclePlannerResultEvent"))
            {
                JourneyPlanResponseCategory responseCategory;

                if (cyclePlannerFailed)
                {
                    responseCategory = JourneyPlanResponseCategory.Failure;
                }
                else
                {
                    if (result.OutwardCycleJourneyCount == 0
                        && result.ReturnCycleJourneyCount == 0)
                    {
                        responseCategory = JourneyPlanResponseCategory.ZeroResults;
                    }
                    else
                    {
                        responseCategory = JourneyPlanResponseCategory.Results;
                    }
                }

                CyclePlannerResultEvent cpre = new CyclePlannerResultEvent(requestId,
                    responseCategory,
                    isLoggedOn,
                    sessionId);
                Logger.Write(cpre);
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

