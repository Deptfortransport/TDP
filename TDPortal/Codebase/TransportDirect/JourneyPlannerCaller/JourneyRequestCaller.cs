// *********************************************** 
// NAME                 : JourneyRequestCaller.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/04/2010
// DESCRIPTION          : Class to which creates journey requests and calls the journey planner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/JourneyRequestCaller.cs-arc  $ 
//
//   Rev 1.0   Apr 20 2010 16:39:14   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.2   Apr 20 2010 15:40:02   mmodi
//Tidy up, accept arguments from command line, and plan cycle journeys
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.1   Apr 19 2010 16:18:16   mmodi
//Coded to validate the request dates
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 19 2010 15:17:08   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP

using System;
using System.Collections.Generic;
using System.Threading;

using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// Class to which creates journey requests and calls the journey planner
    /// </summary>
    public class JourneyRequestCaller
    {
        #region Private members

        private string[] journeyRequestFilePaths;
        private string[] journeyResultFilePaths;
        private string journeyPlannerTimeout;

        private JourneyPlannerHelper helper;

        #endregion

        #region Private classes

        /// <summary>
        ///  Private class to hold the information for a journey request and result
        /// </summary>
        private class Journey
        {
            public string journeyRequestFilePath;
            public string journeyResultFilePath;

            public JourneyRequest journeyRequest;
            public JourneyResult journeyResult;

            /// <summary>
            ///  Constructor
            /// </summary>
            public Journey()
            {
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyRequestCaller(string[] journeyRequestFilePaths, string[] journeyResultFilePaths, string journeyPlannerTimeout)
        {
            this.journeyRequestFilePaths = journeyRequestFilePaths;
            this.journeyResultFilePaths = journeyResultFilePaths;
            this.journeyPlannerTimeout = journeyPlannerTimeout;

            helper = new JourneyPlannerHelper();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to plan the journey.
        /// Result code 0 - indicates success.
        /// Result code -1 - indicates exception was thrown.
        /// Result code -2 - indicates no requests were created
        /// Result code -3 - indicates 1 or more requests failed to return
        /// Result code other - is the first non-zero/success CJP message code returned in a journey result object
        /// </summary>
        /// <returns></returns>
        public int PlanJourney()
        {
            int resultCode = StatusCodes.JPC_SUCCESS;

            try
            {
                // Create the journey objects to work with
                Journey[] journeys = CreateJourneyObjects(journeyRequestFilePaths, journeyResultFilePaths);

                // Check there are some requests to make
                if ((journeys != null) && (journeys.Length > 0))
                {
                    // Call the Journey Planner
                    PlanCJPJourneys(ref journeys);

                    #region Handle the results

                    // Read the journey results, and set result code
                    foreach (Journey journey in journeys)
                    {
                        if (journey.journeyResult != null)
                        {
                            JourneyResult journeyResult = journey.journeyResult;

                            // Check the messages for an "not ok" message code
                            if ((journeyResult.messages != null) && (journeyResult.messages.Length > 0))
                            {
                                foreach (Message message in journeyResult.messages)
                                {
                                    if (message.code == StatusCodes.CjpOK || message.code == StatusCodes.CjpRoadEngineOK)     // OK - no need to do anything
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        resultCode = journeyResult.messages[0].code;
                                    }
                                }
                            }

                            // Log the results to the file
                            helper.WriteJourneyResult(journey.journeyResult, journey.journeyResultFilePath);

                        }
                        else
                        {
                            // Null journey result object, so its failed
                            resultCode = StatusCodes.JPC_REQUESTFAILURE;
                        }
                    }

                    #endregion
                }
                else
                {
                    // No request to make, this must be an error
                    FileLogger.LogMessage("No journey requests were created.");

                    resultCode = StatusCodes.JPC_NOREQUESTS;
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogMessage(ex.Message);

                resultCode = StatusCodes.JPC_EXCEPTION;
            }

            return resultCode;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to create the journey objects, which associate a request with a result.
        /// </summary>
        /// <returns></returns>
        private Journey[] CreateJourneyObjects(string[] journeyRequestFilePaths, string[] journeyResultFilePaths)
        {
            List<Journey> journeys = new List<Journey>();

            // Check theres the same number of request and result files
            helper.ValidateJourneyRequestResultFilePaths(ref journeyRequestFilePaths, ref journeyResultFilePaths);

            for (int i = 0; i < journeyRequestFilePaths.Length; i++)
            {
                // Create the object 
                Journey journey = new Journey();

                journey.journeyRequestFilePath = journeyRequestFilePaths[i];
                journey.journeyResultFilePath = journeyResultFilePaths[i];
                
                // Get the journey request from file
                journey.journeyRequest = helper.ReadCJPJourneyRequest(journey.journeyRequestFilePath);

                // Store the journey object
                journeys.Add(journey);
            }

            return journeys.ToArray();
        }

        /// <summary>
        /// Method to set up and make calls to the CJP
        /// </summary>
        /// <param name="jr"></param>
        private void PlanCJPJourneys(ref Journey[] journeys)
        {
            #region Set up the CJP calls

            // Variables
            CjpFactory cjpFactory = new CjpFactory();
            CJPCall[] cjpCallList = new CJPCall[journeys.Length];
            bool cjpFailed = false;
            int  cjpFailureCount = 0;
            int cjpTimeOut = helper.ValidateJourneyPlannerTimeout(journeyPlannerTimeout);
            
            // Validate JourneyRequests and create the calls
            for (int i = 0; i < journeys.Length; i++ )
            {
                JourneyRequest jr = journeys[i].journeyRequest;
                jr = helper.ValidateJourneyRequest(jr);

                // Push back into the object
                journeys[i].journeyRequest = jr;

                cjpCallList[i] = new CJPCall(jr);
            }

            #endregion

            #region Invoke CJP calls

            WaitHandle[] wh = new WaitHandle[cjpCallList.Length];

            int callCount = 0;

            foreach (CJPCall cjpCall in cjpCallList)
            {
                FileLogger.LogMessage(string.Format("Calling CJP for RequestId[{0}]", cjpCall.RequestId));
                
                // Plan the journey
                wh[callCount] = cjpCall.InvokeCJP((ICJP)cjpFactory.Get());

                if (wh[callCount] == null)
                {
                    cjpFailed = true;
                }

                callCount++;
            }

            #endregion

            // Wait for parallel CJP calls to finish.
            // This method will return when either:
            //  - all CJP calls have completed; or
            //  - the timeout period is exceeded.
            if (!cjpFailed)
            {
                #region Wait for calls to finish

                //The code uses WaitOne instead of WaitAll for returning threads. 
                //The timeout is adjusted for remaining threads to 
                //ensure the overall timeout (cjpTimeOut) is enforced.

                string message = "CJP Thread has returned. Remaining Timeout is: {0} \n";
                int startTime, endTime;

                // Wait for calls to finish
                foreach (ManualResetEvent mh in wh)
                {
                    startTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    mh.WaitOne(cjpTimeOut, false);
                    endTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    cjpTimeOut = cjpTimeOut - (endTime - startTime);

                    FileLogger.LogMessage(string.Format(message, cjpTimeOut.ToString()));
                }

                #endregion

                #region Process results

                // Process CJP call results
                foreach (CJPCall cjpCall in cjpCallList)
                {
                    JourneyResult journeyResult = cjpCall.CJPResult;

                    if (cjpCall.IsSuccessful)
                    {
                        string journeyResultMessage = helper.GetJourneyResultMessage(journeyResult);

                        FileLogger.LogMessage(string.Format("CJP Call succeeded for RequestId[{0}] Message[{1}] Journeys Public[{2}] Private[{3}] \n",
                            cjpCall.RequestId, journeyResultMessage,
                            (journeyResult.publicJourneys != null) ? journeyResult.publicJourneys.Length : 0, 
                            (journeyResult.privateJourneys != null) ? journeyResult.privateJourneys.Length : 0));

                        // Store the journey result to its journey object
                        AddJourneyResult(ref journeys, journeyResult,cjpCall.RequestId);
                    }
                    else 
                    {
                        // Store an null object to its journey object
                        AddJourneyResult(ref journeys, null, cjpCall.RequestId);

                        cjpFailureCount++;
                    }
                }

                #endregion
            }

            #region Handle cjp call failures

            if (cjpFailed)
            {
                // Failed to Invoke any CJP calls
                FileLogger.LogMessage("Failed to invoke the CJP calls \n");

                // Add null result objects to the journey objects so caller knows the requests that failed
                foreach(CJPCall cjpCall in cjpCallList)
                {
                    AddJourneyResult(ref journeys, null, cjpCall.RequestId);
                }
            }
            else if (cjpFailureCount == cjpCallList.Length)
            {
                // All CJP calls failed
                FileLogger.LogMessage("All CJP calls were unsuccessful \n");
            }
            else if (cjpFailureCount > 0)
            {
                // Some CJP calls failed
                FileLogger.LogMessage("Some CJP calls were unsuccessful \n");
            }

            #endregion
        }

        #region Helpers

        /// <summary>
        /// Method which assigns a JourneyResult back to the correct Journey object
        /// </summary>
        /// <param name="journeys"></param>
        private void AddJourneyResult(ref Journey[] journeys, JourneyResult journeyResult, string journeyRequestId)
        {
            for (int i = 0; i < journeys.Length; i++)
            {
                if (journeys[i].journeyRequest.requestID == journeyRequestId)
                {
                    journeys[i].journeyResult = journeyResult;
                    break;
                }
            }
        }

        #endregion

        #endregion
    }
}
