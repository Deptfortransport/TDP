﻿// *********************************************** 
// NAME                 : Program.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/04/2010
// DESCRIPTION          : Main entry point to the Journey Planner Caller application
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/Program.cs-arc  $ 
//
//   Rev 1.0   Apr 20 2010 16:39:12   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.1   Apr 20 2010 15:40:04   mmodi
//Tidy up, accept arguments from command line, and plan cycle journeys
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 19 2010 15:17:10   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Remoting;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// Journey Planner Caller application
    /// </summary>
    public class JourneyPlannerCaller
    {
        #region Private constants/members

        // Constants
        private const string REMOTING_CONFIG_FILE = "RemotingConfig.File";
        private const string JOURNEY_REQUESTS = "JourneyRequests";
        private const string JOURNEY_REQUEST_PLANNER = "JourneyRequest.{0}.Planner";
        private const string JOURNEY_REQUEST_FILE = "JourneyRequest.{0}.File";
        private const string JOURNEY_RESULT_FILE = "JourneyResult.{0}.File";
        private const string JOURNEY_PLANNER_TIMEOUT = "JourneyControl.CJPTimeoutMillisecs";
        private const string CYCLE_PLANNER_TIMEOUT = "CyclePlanner.WebService.TimeoutMillisecs";

        private const char REQUEST_IDS_SEPERATOR = ',';
        
        // Variables
        private string remotingConfigPath;

        private string[] journeyRequestFilePaths;
        private string[] journeyResultFilePaths;
        private string journeyPlannerTimeout;

        private string[] cycleRequestFilePaths;
        private string[] cycleResultFilePaths;
        private string cyclePlannerTimeout;

        /// <summary>
        /// Enum defining the Planner type the journey request is for
        /// </summary>
        private enum Planner
        {
            CJP,
            CTP
        }

        #endregion

        #region Main method

        /// <summary>
        /// Program start
        /// </summary>
        /// <param name="args"></param>
        static int Main(string[] args)
        {
            int statusCode = StatusCodes.JPC_SUCCESS;

            try
            {
                JourneyPlannerCaller app = new JourneyPlannerCaller();

                statusCode = app.Run(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception occurred running the application. Message[{0}]", ex.ToString()));
                
                statusCode = StatusCodes.JPC_EXCEPTION;
            }
            
            Console.WriteLine("Exiting with errorcode " + statusCode);
            
            return statusCode;
        }

        #endregion

        #region Private methods

        #region Run

        /// <summary>
        /// Method which performs the work
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private int Run(string[] args)
        {
            // Assume everything works ok
            int returnCode = StatusCodes.JPC_SUCCESS;
            char[] stringSeperator = { ':' };
            
            // Track argruments passed in for validness
            bool invalidParameters = false;

            // Used if any arguments passed are valid and the action results in journey sholdn't be planned 
            bool planJourneys = true;
            
            // Journey Request Ids suplied in the args which should be planned rather than those specified
            // in the config
            string requestIds = string.Empty;

            // Only expect one arg, the RequestIds or help
            if (args.Length > 1)
            {
                invalidParameters = true;
                Console.WriteLine(string.Format("Error. Invalid number of parameters provided [{0}], expected up to 1 parameter", args.Length));
            }
            else
            {
                // Iterate through each of the command line arguments
                foreach (string s in args)
                {
                    #region Process parameters 

                    if (invalidParameters == false)
                    {
                        if (s.StartsWith("/"))
                        {
                            s.ToLower();
                            string[] formattedArg = s.Split(stringSeperator, 2);
                            switch (formattedArg[0])
                            {
                                case "/help":
                                case "/?":
                                    DisplayHelp();

                                    // Display help and dont plan the journeys
                                    planJourneys = false;

                                    break;

                                default:
                                    Console.WriteLine(string.Format("Error. Unrecognised parameter [{0}]", formattedArg[0]));
                                    invalidParameters = true;
                                    break;
                            }
                        }
                        else
                        {
                            if (s.Equals(args[0]))
                            {
                                requestIds = s;
                            }
                            else
                            {
                                Console.WriteLine("Error. Request IDs passed in wrong location. It can be the first parameter only.");
                                invalidParameters = true;
                            }
                        }
                    }

                    #endregion
                }
            } // End processing parameters

            // All parameter args have been validated, so can continue
            if (invalidParameters == false)
            {
                if (planJourneys)
                {
                    returnCode = SetupConfiguration();

                    // Configuration setup ok
                    if (returnCode == StatusCodes.JPC_SUCCESS)
                    {
                        returnCode = SetupRequestResultFiles(requestIds);
                    }

                    // Reading journey request and result paths ok
                    if (returnCode == StatusCodes.JPC_SUCCESS)
                    {
                        // Set up the application remoting 
                        SetupRemoting(remotingConfigPath);

                        #region Plan journeys

                        // Call to plan the journeys
                        int journeyResultCode = StatusCodes.JPC_SUCCESS;
                        int cycleResultCode = StatusCodes.JPC_SUCCESS;

                        // Initialise classes doing the work
                        if (journeyRequestFilePaths.Length > 0)
                        {
                            FileLogger.LogMessage("Calling journey planner");
                            Console.WriteLine("Calling journey planner");

                            JourneyRequestCaller journeyRequestCaller = new JourneyRequestCaller(journeyRequestFilePaths, journeyResultFilePaths, journeyPlannerTimeout);

                            // Call to plan the journeys
                            journeyResultCode = journeyRequestCaller.PlanJourney();
                        }
                        if (cycleRequestFilePaths.Length > 0)
                        {
                            FileLogger.LogMessage("Calling cycle planner");
                            Console.WriteLine("Calling cycle planner");

                            CycleJourneyRequestCaller cycleJourneyRequestCaller = new CycleJourneyRequestCaller(cycleRequestFilePaths, cycleResultFilePaths, cyclePlannerTimeout);

                            // Call to plan the journeys
                            cycleResultCode = cycleJourneyRequestCaller.PlanJourney();
                        }

                        // Log if no requests were found to run
                        if ((journeyRequestFilePaths.Length == 0) && (cycleRequestFilePaths.Length == 0))
                        {
                            FileLogger.LogMessage("No journey requests were found");
                            Console.WriteLine("No journey requests were found");

                            journeyResultCode = StatusCodes.JPC_NOREQUESTS;
                            cycleResultCode = StatusCodes.JPC_NOREQUESTS;
                        }

                        #endregion

                        #region Log

                        // Log success/failure result
                        string resultMessage = string.Empty;
                        if ((journeyResultCode == StatusCodes.JPC_SUCCESS) && (cycleResultCode == StatusCodes.JPC_SUCCESS))
                        {
                            resultMessage = string.Format("\nJourney planning succeeded: result code[{0}]\n", StatusCodes.JPC_SUCCESS);

                        }
                        else
                        {
                            resultMessage = string.Format("\nJourney planning failed: journeyResultCode[{0}] cycleResultCode[{1}]\n", journeyResultCode, cycleResultCode);
                        }

                        FileLogger.LogMessage(resultMessage);
                        Console.WriteLine(resultMessage);

                        #endregion

                        #region Return code

                        // Determine the return code
                        if (journeyResultCode != StatusCodes.JPC_SUCCESS)
                        {
                            returnCode = journeyResultCode;
                        }
                        else if (cycleResultCode != StatusCodes.JPC_SUCCESS)
                        {
                            returnCode = cycleResultCode;
                        }
                        else
                        {
                            returnCode = StatusCodes.JPC_SUCCESS;
                        }

                        #endregion
                    }
                }
            }
            else
            {
                returnCode = StatusCodes.JPC_EXCEPTION;
            }

            return returnCode;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Method which reads and sets the application configuration/property values needed before 
        /// running should continue
        /// </summary>
        private int SetupConfiguration()
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            // Read configuration values
            try
            {
                Console.WriteLine("\nJourney planner caller started");
                FileLogger.LogMessage("Journey planner caller started");
                
                // Read configuration values
                remotingConfigPath = ConfigurationManager.AppSettings[REMOTING_CONFIG_FILE];
                journeyPlannerTimeout = ConfigurationManager.AppSettings[JOURNEY_PLANNER_TIMEOUT];
                cyclePlannerTimeout = ConfigurationManager.AppSettings[CYCLE_PLANNER_TIMEOUT];

            }
            catch (Exception ex)
            {
                string message = string.Format("Error attempting to read application property values. \nMessage[{0}] \n StackTrace[{1}] \n",
                    ex.Message,
                    ex.StackTrace);

                Console.WriteLine(message);

                returnCode = StatusCodes.JPC_EXCEPTION;
            }

            return returnCode;
        }

        /// <summary>
        /// Method which reads the journey request and result files to process from the configuration.
        /// If request ids are provided, then these are run in preference to the ones specified 
        /// in the configuration file
        /// </summary>
        private int SetupRequestResultFiles(string requestIds)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            try
            {
                // Check if any request ids were supplied 
                List<string> requestsToPlan = new List<string>();

                if (!string.IsNullOrEmpty(requestIds))
                {
                    string[] requestIdsArray = requestIds.Split(REQUEST_IDS_SEPERATOR);

                    if (requestIdsArray.Length > 0)
                    {
                        requestsToPlan = new List<string>(requestIdsArray);
                    }
                }
                
                // Get file paths for the CJP requests
                GetJourneyFiles(Planner.CJP, requestsToPlan, ref journeyRequestFilePaths, ref journeyResultFilePaths);
                
                // Get file paths for the CTP Cycle requests
                GetJourneyFiles(Planner.CTP, requestsToPlan, ref cycleRequestFilePaths, ref cycleResultFilePaths);
            }
            catch (Exception ex)
            {
                // Write simple message to console
                string message = "Error attempting to identify journey request filepaths to plan. Please ensure the journey request ids and their configuration file settings are correct";
                Console.WriteLine(message);

                // More detail to the log file
                message = string.Format("{0} \nMessage[{1}] \n StackTrace[{2}] \n",
                    message,
                    ex.Message,
                    ex.StackTrace);
                FileLogger.LogMessage(message);

                returnCode = StatusCodes.JPC_EXCEPTION;
            }

            return returnCode;
        }

        /// <summary>
        /// Method to set up the Application Remoting
        /// </summary>
        /// <param name="remotingConfigPath"></param>
        private void SetupRemoting(string remotingConfigPath)
        {
            //Configure the hosted remoting objects to be a remote object
            if (File.Exists(remotingConfigPath))
            {
                RemotingConfiguration.Configure(remotingConfigPath, false);

                FileLogger.LogMessage(string.Format("Loaded remoting configuration[{0}]", remotingConfigPath));
            }
            else
            {
                FileLogger.LogMessage(string.Format("Could not find remoting configuration[{0}]", remotingConfigPath));
            }

        }

        /// <summary>
        /// Method to return a list of Journey request and result file paths read from the application settings
        /// </summary>
        /// <returns></returns>
        private void GetJourneyFiles(Planner planner, List<string> requestsToPlan, 
            ref string[] journeyRequestFilePaths, ref string[] journeyResultFilePaths)
        {
            List<string> tmpJourneyRequestFiles = new List<string>();
            List<string> tmpJourneyResultFiles = new List<string>();
            
            #region Identify the request ids to get

            List<string> journeyRequestIds = new List<string>();

            if (requestsToPlan.Count > 0)
            {
                // Caller has specified which requests are to be planned
                journeyRequestIds = new List<string>(requestsToPlan);
            }
            else
            {
                // Read which requests to plan from the configuration
                string journeyRequests = ConfigurationManager.AppSettings[JOURNEY_REQUESTS];

                if (!string.IsNullOrEmpty(journeyRequests))
                {
                    journeyRequestIds = new List<string>(journeyRequests.Split(REQUEST_IDS_SEPERATOR));
                }
            }

            #endregion

            // Now get the individual journey file paths
            foreach (string journeyRequestId in journeyRequestIds)
            {
                // Planner type
                string journeyPlannerProperty = string.Format(JOURNEY_REQUEST_PLANNER, journeyRequestId);
                Planner journeyRequestPlanner = (Planner)Enum.Parse(typeof(Planner), ConfigurationManager.AppSettings[journeyPlannerProperty]);

                // Journey request and result filepaths
                string journeyRequestProperty = string.Format(JOURNEY_REQUEST_FILE, journeyRequestId);
                string journeyRequestFilepath = ConfigurationManager.AppSettings[journeyRequestProperty];

                string journeyResultProperty = string.Format(JOURNEY_RESULT_FILE, journeyRequestId);
                string journeyResultFilepath = ConfigurationManager.AppSettings[journeyResultProperty];

                // Check its for the requested Planner
                if (journeyRequestPlanner == planner)
                {
                    // Check a request file path exists
                    if (!string.IsNullOrEmpty(journeyRequestFilepath))
                    {
                        tmpJourneyRequestFiles.Add(journeyRequestFilepath);

                        // Allow an empty result filename to go through, as this would indicate the journey results
                        // do not need to be logged
                        tmpJourneyResultFiles.Add(journeyResultFilepath);
                    }
                }
            }

            // Assign the file paths to be returned
            journeyRequestFilePaths = tmpJourneyRequestFiles.ToArray();
            journeyResultFilePaths = tmpJourneyResultFiles.ToArray();
        }

        /// <summary>
        /// Displays help on the Console
        /// </summary>
        private void DisplayHelp()
        {
            // The text below is displayed to the user showing how a valid call can be constructed
            Console.WriteLine("\n");
            Console.WriteLine("JourneyPlannerCaller [request ids] [/help] \n\n");
            Console.WriteLine("request ids   - Request ids to run from the configuration file.");
            Console.WriteLine("                List should be '" + REQUEST_IDS_SEPERATOR + "' seperated. This is an Optional parameter");
            Console.WriteLine("/help         - Displays this help text and performs no further activity");
            Console.WriteLine("\n");
        }

        #endregion

        #endregion
    }
}