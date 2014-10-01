// *********************************************** 
// NAME                 : CycleJourneyRequestCaller.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/04/2010
// DESCRIPTION          : Class to which creates cycle journey requests and calls the cycle journey planner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/CycleJourneyRequestCaller.cs-arc  $ 
//
//   Rev 1.0   Apr 20 2010 16:39:20   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 20 2010 15:41:24   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//

using System;
using System.Collections.Generic;
using System.Threading;

using JourneyPlannerCaller.CyclePlannerWebService;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// Class to which creates cycle journey requests and calls the cycle journey planner
    /// </summary>
    public class CycleJourneyRequestCaller
    {
        #region Private members

        private string[] cycleRequestFilePaths;
        private string[] cycleResultFilePaths;
        private string cyclePlannerTimeout;

        private JourneyPlannerHelper helper;

        #endregion

        #region Private classes

        /// <summary>
        ///  Private class to hold the information for a cycle journey request and result
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
        public CycleJourneyRequestCaller(string[] cycleRequestFilePaths, string[] cycleResultFilePaths, string cyclePlannerTimeout)
        {
            this.cycleRequestFilePaths = cycleRequestFilePaths;
            this.cycleResultFilePaths = cycleResultFilePaths;
            this.cyclePlannerTimeout = cyclePlannerTimeout;

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
                Journey[] journeys = CreateJourneyObjects(cycleRequestFilePaths, cycleResultFilePaths);

                // Check there are some requests to make
                if ((journeys != null) && (journeys.Length > 0))
                {
                    // Call the Cycle Planner
                    PlanCycleJourneys(ref journeys);

                    #region Handle the results

                    // Read the cycle results, and set result code
                    foreach (Journey journey in journeys)
                    {
                        if (journey.journeyResult != null)
                        {
                            CyclePlannerResult cycleResult = journey.journeyResult;

                            // Check the messages for an "not ok" message code
                            if ((cycleResult.messages != null) && (cycleResult.messages.Length > 0))
                            {
                                foreach (Message message in cycleResult.messages)
                                {
                                    if (message.code == StatusCodes.CPOK)     // OK - no need to do anything
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        resultCode = cycleResult.messages[0].code;
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
                    FileLogger.LogMessage("No cycle requests were created.");

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
                journey.journeyRequest = helper.ReadCycleJourneyRequest(journey.journeyRequestFilePath);

                // Store the journey object
                journeys.Add(journey);
            }

            return journeys.ToArray();
        }

        /// <summary>
        /// Method to set up and make calls to the CJP
        /// </summary>
        /// <param name="jr"></param>
        private void PlanCycleJourneys(ref Journey[] journeys)
        {
            #region Set up the Cycle planner calls

            // Variables
            CyclePlannerCall[] cyclePlannerCallList = new CyclePlannerCall[journeys.Length];
            int callCount = 0;
            bool cyclePlannerFailed = false;
            int cyclePlannerFailureCount = 0;
            int cyclePlannerTimeOut = helper.ValidateJourneyPlannerTimeout(cyclePlannerTimeout);

            // Validate JourneyRequests and create the calls
            for (int i = 0; i < journeys.Length; i++ )
            {
                JourneyRequest cpr = journeys[i].journeyRequest;
                cpr = helper.ValidateJourneyRequest(cpr);

                // Push back into the object
                journeys[i].journeyRequest = cpr;

                cyclePlannerCallList[i] = new CyclePlannerCall(cpr);
            }

            #endregion

            #region Invoke Cycle Planner calls

            // Get an instance of the Cycle planner web service
            CyclePlanner cyclePlannerService = new CyclePlanner(cyclePlannerTimeOut);

            WaitHandle[] wh = new WaitHandle[cyclePlannerCallList.Length];

            foreach (CyclePlannerCall cpCall in cyclePlannerCallList)
            {
                FileLogger.LogMessage(string.Format("Calling CyclePlanner for RequestId[{0}]", cpCall.RequestId));
                
                // Plan the journey
                wh[callCount] = cpCall.InvokeCyclePlanner(cyclePlannerService);

                if (wh[callCount] == null)
                {
                    cyclePlannerFailed = true;
                }

                callCount++;
            }

            #endregion

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

                string message = "Cycle Planner Thread has returned. Remaining Timeout is: {0} \n";
                int startTime, endTime;

                // Wait for calls to finish
                foreach (ManualResetEvent mh in wh)
                {
                    startTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    mh.WaitOne(cyclePlannerTimeOut, false);
                    endTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                    cyclePlannerTimeOut = cyclePlannerTimeOut - (endTime - startTime);

                    FileLogger.LogMessage(string.Format(message, cyclePlannerTimeOut.ToString()));
                }

                #endregion

                #region Process results

                // Process CJP call results
                foreach (CyclePlannerCall cpCall in cyclePlannerCallList)
                {
                    // Get the result from the call
                    CyclePlannerResult cyclePlannerResult = cpCall.Result;
                    JourneyResult journeyResult = null;

                    if (cpCall.IsSuccessful)
                    {
                        try
                        {
                            // Convert the result into a journey result so we can get at the cycle journeys
                            journeyResult = (JourneyResult)cyclePlannerResult;

                            string journeyResultMessage = helper.GetJourneyResultMessage(journeyResult);

                            FileLogger.LogMessage(
                                string.Format("Cycle Planner Call succeeded for RequestId[{0}] Message[{1}] Journeys Cycle[{2}] \n",
                                cpCall.RequestId, 
                                journeyResultMessage,
                                (journeyResult.journeys != null) ? journeyResult.journeys.Length : 0));

                            // Store the journey result to its journey object
                            AddJourneyResult(ref journeys, journeyResult, cpCall.RequestId);
                        }
                        catch
                        {
                            #region Log error

                            // If there is an exception then result returned by the cycle planner was invalid
                            FileLogger.LogMessage(
                                "Cycle Planner Call - exception occurred attempting to cast CyclePlannerResult into a JourneyResult, for request: " + cpCall.RequestId);

                            string journeyResultMessage = helper.GetJourneyResultMessage(cyclePlannerResult);


                            FileLogger.LogMessage(
                                string.Format("Cycle Planner Call failed for RequestId[{0}] Message[{1}] \n",
                                cpCall.RequestId, 
                                journeyResultMessage));

                            #endregion

                            // Store an null object to its journey object
                            AddJourneyResult(ref journeys, null, cpCall.RequestId);

                            cyclePlannerFailureCount++;
                        }
                    }
                    else 
                    {
                        // Store an null object to its journey object
                        AddJourneyResult(ref journeys, null, cpCall.RequestId);

                        cyclePlannerFailureCount++;
                    }
                }

                #endregion
            }

            // Dispose of the web service
            cyclePlannerService.Dispose();

            #region Handle cjp call failures

            if (cyclePlannerFailed)
            {
                // Failed to Invoke any Cycle Planner calls
                FileLogger.LogMessage("Failed to invoke the Cycle Planner calls \n");

                // Add null result objects to the journey objects so caller knows the requests that failed
                foreach(CyclePlannerCall cpCall in cyclePlannerCallList)
                {
                    AddJourneyResult(ref journeys, null, cpCall.RequestId);
                }
            }
            else if (cyclePlannerFailureCount == cyclePlannerCallList.Length)
            {
                // All Cycle Planner calls failed
                FileLogger.LogMessage("All Cycle Planner calls were unsuccessful \n");
            }
            else if (cyclePlannerFailureCount > 0)
            {
                // Some Cycle Planner calls failed
                FileLogger.LogMessage("Some Cycle Planner calls were unsuccessful \n");
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
