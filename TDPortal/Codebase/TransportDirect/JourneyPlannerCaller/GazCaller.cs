// *********************************************** 
// NAME             : GazCaller.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 30 Nov 2010
// DESCRIPTION  	: Class provided Gaz request calls setup functionality
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/GazCaller.cs-arc  $
//
//   Rev 1.1   Jan 08 2013 14:29:36   mmodi
//Corrected to run only specified requests, return error code for invalid parameters, and tidied display of exit message
//Resolution for 5882: JourneyPlannerCaller fails to run TTBO journey plan requests
//
//   Rev 1.0   Nov 30 2010 13:33:46   apatel
//Initial revision.
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// Class to set up calls to GAZ
    /// </summary>
    class GazCaller
    {

        #region Private constants/members

        // Constants
        private const string GAZ_REQUESTS = "GazRequests";
        private const string GAZ_REQUEST_FUNCTION = "GazRequest.{0}.Function";
        private const string GAZ_REQUEST_FILE = "GazRequest.{0}.File";
        private const string GAZ_RESULT_FILE = "GazResult.{0}.File";
        private const string GAZ_REQUEST_SOAP_ACTION = "GazResult.{0}.SoapAction";

        private const char REQUEST_IDS_SEPERATOR = ',';

        // Variables
        private Dictionary<string, GazQuery> gazQueryDictionary = new Dictionary<string, GazQuery>();
        
        #endregion

        #region Private methods

        #region Run

        /// <summary>
        /// Method which performs the work
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        internal int Run(string[] args)
        {
            // Assume everything works ok
            int returnCode = StatusCodes.JPC_SUCCESS;
            
            char[] stringSeperator = { ':' };

            // Reset the gaz query dictionary
            gazQueryDictionary = new Dictionary<string, GazQuery>();

            // Gaz Request Ids suplied in the args which should be planned rather than those specified
            // in the config
            List<string> requestIds = JourneyPlannerCaller.GetRequestIds(args);
            
            // All parameter args have been validated, so can continue

            // Configuration setup ok
            if (returnCode == StatusCodes.JPC_SUCCESS)
            {
                returnCode = SetupGazRequestResultFiles(requestIds);
            }

            // Reading gaz request and result paths ok
            if (returnCode == StatusCodes.JPC_SUCCESS)
            {
                

                #region Gaz Requests

                // Call Gaz web service
                int gazResultCode = StatusCodes.JPC_SUCCESS;
                
                // Initialise classes doing the work
                if (gazQueryDictionary.Count > 0)
                {
                    FileLogger.LogMessage("Calling GAZ");
                    Console.WriteLine("Calling GAZ");

                    GazRequestCaller gazRequestCaller = new GazRequestCaller();

                    // Call to gaz query/function
                    gazResultCode = gazRequestCaller.PerformGazQueries(gazQueryDictionary);
                }
                

                // Log if no requests were found to run
                if ((gazQueryDictionary.Count == 0))
                {
                    FileLogger.LogMessage("No gaz requests were found");
                    Console.WriteLine("No gaz requests were found");

                    gazResultCode = StatusCodes.JPC_NOREQUESTS;
                   
                }

                #endregion

                #region Log

                // Log success/failure result
                string resultMessage = string.Empty;
                if ((gazResultCode == StatusCodes.JPC_SUCCESS))
                {
                    resultMessage = string.Format("\nGaz requests succeeded: result code[{0}]\n", StatusCodes.JPC_SUCCESS);

                }
                else
                {
                    resultMessage = string.Format("\nGaz requests failed: resut code[{0}]\n", gazResultCode);
                }

                FileLogger.LogMessage(resultMessage);
                Console.WriteLine(resultMessage);

                #endregion

                returnCode = gazResultCode;
                
            }


            return returnCode;
        }

        #endregion

        #region Helper methods
        /// <summary>
        /// Method which reads the gaz request and result files to process from the configuration.
        /// If request ids are provided, then these are run in preference to the ones specified 
        /// in the configuration file
        /// </summary>
        private int SetupGazRequestResultFiles(List<string> requestIds)
        {
            int returnCode = StatusCodes.JPC_SUCCESS;

            try
            {
                // Check if any request ids were supplied 
                List<string> requestsToPlan = new List<string>();

                if (requestIds != null && requestIds.Count > 1)
                {
                    foreach (string reqId in requestIds)
                    {
                        if (reqId.StartsWith("g") && reqId.Trim().Length > 1)
                        {
                            requestsToPlan.Add(reqId.Substring(1));
                        }
                    }

                    if (requestsToPlan.Count == 0)
                    {
                        returnCode = StatusCodes.JPC_NOREQUESTS;
                    }
                }

                if (returnCode == StatusCodes.JPC_SUCCESS)
                {
                    // Get file paths for the CJP requests
                    GetGazFiles(requestsToPlan);
                }
            }
            catch (Exception ex)
            {
                // Write simple message to console
                string message = "Error attempting to identify gaz request filepaths to plan. Please ensure the gaz request ids and their configuration file settings are correct";
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
        /// Method to return a list of Gaz request and result file paths read from the application settings
        /// </summary>
        /// <returns></returns>
        private void GetGazFiles(List<string> requestsToPlan)
        {
           
            #region Identify the request ids to get

            List<string> gazRequestIds = new List<string>();

            if (requestsToPlan.Count > 0)
            {
                // Caller has specified which requests are to be planned
                gazRequestIds = new List<string>(requestsToPlan);
            }
            else
            {
                // Read which requests to plan from the configuration
                string gazRequests = ConfigurationManager.AppSettings[GAZ_REQUESTS];

                if (!string.IsNullOrEmpty(gazRequests))
                {
                    string[] requestIdsArray = gazRequests.Split(REQUEST_IDS_SEPERATOR);

                    if (requestIdsArray.Length > 0)
                    {
                        foreach (string reqId in requestIdsArray)
                        {
                            if (reqId.ToLower().StartsWith("g") && reqId.Trim().Length > 1)
                            {
                                gazRequestIds.Add(reqId.Substring(1));
                            }
                        }
                    }
                }
            }

            #endregion

            // Now get the individual gaz file paths
            foreach (string gazRequestId in gazRequestIds)
            {
                // Gaz function
                string gazFunctionProperty = string.Format(GAZ_REQUEST_FUNCTION, gazRequestId);
                string gazFunction = ConfigurationManager.AppSettings[gazFunctionProperty];
                
                // Gaz request and result filepaths
                string gazRequestProperty = string.Format(GAZ_REQUEST_FILE, gazRequestId);
                string gazRequestFilepath = ConfigurationManager.AppSettings[gazRequestProperty];

                string gazResultProperty = string.Format(GAZ_RESULT_FILE, gazRequestId);
                string gazResultFilepath = ConfigurationManager.AppSettings[gazResultProperty];

                string soapActionProperty = string.Format(GAZ_REQUEST_SOAP_ACTION, gazRequestId);
                string soapAction = ConfigurationManager.AppSettings[soapActionProperty];
                
                // Check a request file path exists
                if (!string.IsNullOrEmpty(gazRequestFilepath) && !string.IsNullOrEmpty(gazFunction))
                {
                    // Allow an empty result filename to go through, as this would indicate the gaz results
                    // do not need to be logged
                    GazQuery query = new GazQuery(gazFunction,gazRequestFilepath, gazResultFilepath, soapAction);

                    if (!gazQueryDictionary.ContainsKey(gazRequestId))
                    {
                        gazQueryDictionary.Add(gazRequestId, query);
                    }
                    
                }
                
            }

        }

        #endregion

        #endregion
    }
}
