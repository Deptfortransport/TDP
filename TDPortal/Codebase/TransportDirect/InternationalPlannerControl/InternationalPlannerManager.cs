// *********************************************** 
// NAME			: InternationalPlannerManager.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 02 Feb 2010
// DESCRIPTION	: Class which constructs the International planner calls and waits for the results to be returned
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalPlannerManager.cs-arc  $
//
//   Rev 1.13   Dec 06 2012 09:14:20   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.12   Sep 06 2011 11:20:40   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.11   Feb 24 2010 18:02:54   mmodi
//Don't show couldn't plan PT journeys message is PT modes not requested
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Feb 20 2010 20:25:30   mmodi
//Updated adding error messages
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Feb 19 2010 12:06:42   mmodi
//Add Road Journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 17 2010 15:30:24   mmodi
//Updated to retain public journey duration
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 16 2010 17:48:04   mmodi
//Exposed is permitted journey method
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Feb 16 2010 11:16:16   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Feb 14 2010 22:33:48   apatel
//updated to set the callfailed value to false once the call successful
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Feb 12 2010 11:13:24   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 11 2010 08:53:14   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 09 2010 10:46:20   apatel
//Updated IsPermitted journey method
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 09 2010 10:11:06   apatel
//TD International Planner update
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 09:33:52   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.InternationalPlanner;
using TransportDirect.UserPortal.JourneyControl;

using Logger = System.Diagnostics.Trace;
using JC = TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// Class which orchestrates the various calls to the International planner
    /// </summary>
    public class InternationalPlannerManager : IInternationalPlannerManager
    {
        
        #region IInternationalPlannerManager Members
        /// <summary>
        /// CallInternationalPlanner handles the orchestration of the various calls to the International planner.
        /// This overloaded version handles INITIAL requests.
        /// </summary>
        /// <param name="request">Encapsulates international journey parameters</param>
        /// <param name="sessionID">Used for logging purposes only</param>
        /// <param name="userType">Used to determine level of logging</param>
        /// <param name="referenceTransaction">True is this an SLA-monitoring transaction</param>
        /// <param name="loggedOn">True if the user is logged on (used for logging only)</param>
        /// <param name="language">Two-character ISO id ("en" or "cy") of the current UI language</param>
        /// <returns>The results of the enquiry, including any error messages</returns>
        [MTAThread]
        public ITDJourneyResult CallInternationalPlanner(ITDJourneyRequest request, string sessionId, int userType, 
            bool referenceTransaction, bool loggedOn, string language)
        {
            return CallInternationalPlanner(request,
                sessionId,
                userType,
                false,
                referenceTransaction,
                0,
                0,
                loggedOn,
                language);
        }

        /// <summary>
        /// Validate if the journey permitted between origin country and destination city
        /// </summary>
        /// <param name="originCountry">Origin country code</param>
        /// <param name="destinationCountry">Destination country code</param>
        /// <returns></returns>
        public bool IsPermittedInternationalJourney(string originCountry, string destinationCountry)
        {
            try
            {
                InternationalPlannerHelper helper = new InternationalPlannerHelper();

                return helper.IsPermittedInternationalJourney(originCountry, destinationCountry);
            }
            catch (TDException tdEx)
            {
                #region Log error
                if (!tdEx.Logged)
                {
                    OperationalEvent oe = new OperationalEvent(
                        TDEventCategory.Infrastructure, TDTraceLevel.Error,
                        string.Format("Error attempting to get permitted international journey country combinations. " +
                        "\n TDExceptionIdentifier[{0}] \n Message[{1}]. \n InnerException[{2}]. \n StackTrace[{3}].",
                        tdEx.Identifier,
                        tdEx.Message,
                        tdEx.InnerException,
                        tdEx.StackTrace));

                    Logger.Write(oe);
                }
                #endregion
            }
            
            return false;
        }
        
        #endregion
        
       
        #region Private methods

        /// <summary>
        /// Common private method called by CallInternationalPlanner public method
        /// to do the real work of handling the International planner call.
        /// </summary>
        private ITDJourneyResult CallInternationalPlanner(ITDJourneyRequest request,
            string sessionId,
            int userType,
            bool amendment,
            bool referenceTransaction,
            int referenceNumber,
            int lastSequenceNumber,
            bool loggedOn,
            string language)
        {
           
            IPropertyProvider propertyProvider = Properties.Current;

            string requestId = string.Empty;

            int callCount = 0;
            bool internationalPlannerFailed = false;
            int internationalPlannerFailureCount = 0;
            

            #region Determine if we should log Requests/Responses
            // These switches control operational logging of raw International planner inputs and outputs
            bool logAllRequests = false;
            bool logAllResponses = false;

            // Since valid user types are 0, 1, 2 a high value here disables user-type driven
            // logging (so logging of CJP requests controlled by trace level as normal)
            int minimumLoggingUser = 99;

            try
            {
                minimumLoggingUser = Int32.Parse(propertyProvider[InternationalPlannerConstants.MinLoggingUserType]);
            }
            // nothing to do here - just means property hasn't been set yet
            catch (Exception)
            {
            }

            if (userType < minimumLoggingUser)
            {
                logAllRequests = (TDTraceSwitch.TraceVerbose) && (bool.Parse(propertyProvider[InternationalPlannerConstants.LogAllRequests]));
                logAllResponses = (TDTraceSwitch.TraceVerbose) && (bool.Parse(propertyProvider[InternationalPlannerConstants.LogAllResponses]));
            }
            else
            {
                logAllRequests = true;
                logAllResponses = true;
            }
            #endregion

            #region Create International planner calls

            // if it is an amendment, the previous reference number will have been passed back in 
            // for reuse here, otherwise we need to get hold of a new one.
            if (!amendment)
            {
                referenceNumber = SqlHelper.GetRefNumInt();
                
            }

            // Create the International planner calls
            InternationalPlannerRequestPopulator populator = new InternationalPlannerRequestPopulator(request);

            InternationalPlannerCall[] internationalPlannerCallList = populator.PopulateRequests(
                referenceNumber, lastSequenceNumber, sessionId,
                referenceTransaction, userType, language);

            // Retrieve the formatted reference number from the first call object, there should only be one call
            requestId = ((InternationalPlannerCall)internationalPlannerCallList[0]).RequestId;

            #endregion

            // INVOKE THE INTERNATIONAL PLANNER CALLs
            WaitHandle[] wh = new WaitHandle[internationalPlannerCallList.Length];

            foreach (InternationalPlannerCall ipCall in internationalPlannerCallList)
            {
                #region Log
                LogRequest(ipCall.RequestId, loggedOn, sessionId);

                if (logAllRequests)
                {
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            "InternationalPlanner - InternationalPlannerManager, logging the international planner journey request to submit."));
                    }

                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                        TDTraceLevel.Verbose,
                        ConvertToXML(ipCall.Request, ipCall.RequestId),
                        TDTraceLevelOverride.User));
                }
                #endregion

                wh[callCount] = ipCall.InvokeInternationalPlanner();

                if (wh[callCount] == null)
                {
                    internationalPlannerFailed = true;
                }

                callCount++;
            }

            #region Setup the result object
            // This will force a user to have amend the same route 200 times before overflowing and
            // possibly have remnants of journey items from previous route when plotted on the map.
            TDJourneyResult result;
            result = new TDJourneyResult(referenceNumber, referenceNumber * 200, (request.OutwardDateTime.Length == 0 ? null : request.OutwardDateTime[0]), null, false, false, false);
            
            #endregion
            
            // Timeout value
            int internationalPlannerTimeOut = Int32.Parse(propertyProvider[InternationalPlannerConstants.TimeoutMillisecs]);

            // Wait for parallel International planner calls to finish.
            // This method will return when either:
            //  - all calls have completed; or
            //  - the timeout period is exceeded.
            if (!internationalPlannerFailed)
            {
                #region Wait for International planner calls to return
                //Added code change for multithreaded journey planning. The code uses WaitOne instead
                //of WaitAll for returning threads. The timeout is adjusted for remaining threads to 
                //ensure the overall timeout (internationalPlannerTimeOut) is enforced. 

                // ----- WaitOne code -------- 
                string message = String.Empty;
                int startTime, endTime;

                if (TDTraceSwitch.TraceVerbose)
                {
                    System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                    stringBuilder.Append("InternationalPlanner - InternationalPlannerManager Thread has returned, ref no ");
                    stringBuilder.Append(referenceNumber.ToString());
                    stringBuilder.Append(". Remaining Timeout is: ");
                    message = stringBuilder.ToString();
                }

                foreach (ManualResetEvent mh in wh)
                {
                    startTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    mh.WaitOne(internationalPlannerTimeOut, false);
                    endTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    internationalPlannerTimeOut = internationalPlannerTimeOut - (endTime - startTime);

                    Logger.Write(new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Verbose, message + internationalPlannerTimeOut.ToString()));
                }
                // ----- End WaitOne code -------- 

                               
                #endregion

                foreach (InternationalPlannerCall ipCall in internationalPlannerCallList)
                {
                    bool thisCallFailed = true;

                    // Get the result from the call
                    IInternationalPlannerResult internationalPlannerResult = ipCall.Result;
                    
                    #region Add the journey to the result object
                    if (ipCall.IsSuccessful)
                    {
                        #region Convert result in to JourneyResult
                        try
                        {
                            InternationalJourneyResultConverter ijConverter = new InternationalJourneyResultConverter(internationalPlannerResult);

                            // Add journeys to the result
                            foreach (JC.PublicJourney pJourney in ijConverter.PublicJourneys)
                            {
                                result.AddPublicJourney(pJourney, true, true, true);
                            }

                            foreach (RoadJourney rJourney in ijConverter.RoadJourneys)
                            {
                                result.AddRoadJourney(rJourney, true, true);
                            }

                            thisCallFailed = internationalPlannerResult.MessageID != 0;

                            #region Log result
                            int resultPublicJourneys = result.OutwardPublicJourneyCount;
                            int resultRoadJourneys = result.OutwardRoadJourneyCount;

                            Logger.Write(new OperationalEvent
                                (TDEventCategory.Business, TDTraceLevel.Verbose,
                                "Public Journeys converted from InternationalPlannerEngine is "
                                + ijConverter.PublicJourneys.Count + " and added to result is " + resultPublicJourneys));

                            Logger.Write(new OperationalEvent
                                (TDEventCategory.Business, TDTraceLevel.Verbose,
                                "Private Road Journeys converted from InternationalPlannerEngine is "
                                + ijConverter.RoadJourneys.Count + " and added to result is " + resultRoadJourneys));
                            
                            if (logAllResponses)
                            {
                                if (TDTraceSwitch.TraceVerbose)
                                {
                                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                        "InternationalPlanner - InternationalPlannerManager, logging the international planner journey result received."));
                                }

                                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                    ConvertToXML(internationalPlannerResult, ipCall.RequestId),
                                    TDTraceLevelOverride.User));
                            }

                            // Log any messages contained in the result 
                            if ((internationalPlannerResult != null) && (internationalPlannerResult.MessageDescription != null) && (internationalPlannerResult.MessageID > 0))
                            {
                                // Determine if we need to show any messages  on the Portal results page
                                bool showExternalMessages = (userType >= Int32.Parse(Properties.Current[InternationalPlannerConstants.MinLoggingUserType]));


                                Logger.Write(new OperationalEvent
                                    (TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                     "InternationalPlanner - InternationalPlannerResult included message Code: " + internationalPlannerResult.MessageID.ToString()
                                     + ",  Message: " + internationalPlannerResult.MessageDescription + ". For request: " + ipCall.RequestId));

                                if (showExternalMessages)
                                {
                                    result.AddMessageToArray(internationalPlannerResult.MessageDescription, InternationalPlannerConstants.IPExternalMessage, internationalPlannerResult.MessageID, 0);
                                }


                            }
                            #endregion

                            
                        }
                        catch
                        {
                            #region Log error

                            thisCallFailed = true;

                            // If there is an exception then result returned by the international planner was invalid
                            Logger.Write(new OperationalEvent
                                        (TDEventCategory.CJP, TDTraceLevel.Error,
                                         "InternationalPlanner - InternationalPlannerManager, exception occurred attempting to convert InternationalPlannerResult into a JourneyResult, for request: " + ipCall.RequestId));

                            
                            #endregion
                        }

                        #endregion

                        
                    }

                    

                    if (thisCallFailed)
                    {
                       internationalPlannerFailureCount++;
                    }

                    LogResponse(internationalPlannerResult, requestId, loggedOn, internationalPlannerFailed, sessionId);
                    #endregion
                }

                
            } // All international planner calls processed

            #region Check for errors

            bool publicRequired = false;

            if (request.Modes.Length > 1 || request.Modes[0] != ModeType.Car)
            {
                publicRequired = true;
            }

            if ((result.OutwardPublicJourneyCount == 0) && (result.OutwardRoadJourneyCount == 0))
            {
                // No road or public journeys
                if (result.CJPMessages.Length == 0)
                {
                    result.AddMessageToArray(string.Empty, InternationalPlannerConstants.IPNoResults, 0, 0);
                }

                result.IsValid = false;
            }
            else if ( publicRequired && (result.OutwardPublicJourneyCount == 0) && (result.OutwardRoadJourneyCount >= 1))
            {
                // Only road journeys returned, so indicate a partial result error as we always want public journeys
                if (result.CJPMessages.Length == 0)
                {
                    result.AddMessageToArray(string.Empty, InternationalPlannerConstants.IPPartialReturn, 0, 0);
                }

                result.IsValid = true;
            }
            else
            {
                result.IsValid = true;
            }
            

           

            // if we couldn't invoke a international planner service call
            if (internationalPlannerFailed)
            {
                result.AddMessageToArray(string.Empty, InternationalPlannerConstants.IPInternalError,
                    InternationalPlannerConstants.IPCallError, 0);
            }
            else
            {
                // Check for multiple international planner call fails
                if (internationalPlannerFailureCount == internationalPlannerCallList.Length)
                {
                    if (result.CJPMessages.Length == 0)
                    {
                        result.AddMessageToArray(string.Empty, InternationalPlannerConstants.IPInternalError,
                            InternationalPlannerConstants.IPCallError, 0);
                    }

                    internationalPlannerFailed = true;
                }
                else if (internationalPlannerFailureCount > 0)
                {
                    if (result.CJPMessages.Length == 0)
                    {
                        result.AddMessageToArray(string.Empty, InternationalPlannerConstants.IPPartialReturn,
                            InternationalPlannerConstants.IPCallError, 0);
                    }
                }
            }
            #endregion

           
            return result;
        }

        #region MIS Logging
        /// <summary>
        /// Logs a International Planner request event
        /// </summary>
        private void LogRequest(string requestId, bool isLoggedOn, string sessionId)
        {
            // Log a request event.
            if (CustomEventSwitch.On("InternationalPlannerRequestEvent"))
            {
                InternationalPlannerRequestEvent ipre = new InternationalPlannerRequestEvent(requestId,
                    isLoggedOn,
                    sessionId);
                Logger.Write(ipre);
            }
        }

        /// <summary>
        /// Logs a International Planner result event
        /// </summary>
        private void LogResponse(IInternationalPlannerResult result, string requestId, bool isLoggedOn,
            bool internationalPlannerFailed, string sessionId)
        {
            if (CustomEventSwitch.On("InternationalPlannerResultEvent"))
            {
                JourneyPlanResponseCategory responseCategory;

                if (internationalPlannerFailed)
                {
                    responseCategory = JourneyPlanResponseCategory.Failure;
                }
                else
                {
                    if (result.InternationalJourneys.Length == 0)
                    {
                        responseCategory = JourneyPlanResponseCategory.ZeroResults;
                    }
                    else
                    {
                        responseCategory = JourneyPlanResponseCategory.Results;
                    }
                }

                InternationalPlannerResultEvent ipres = new InternationalPlannerResultEvent(requestId,
                    responseCategory,
                    isLoggedOn,
                    sessionId);
                Logger.Write(ipres);
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
