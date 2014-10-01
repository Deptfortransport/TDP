using System;
using System.Collections.Generic;
using System.Text;
using JourneyPlannerCaller.GisQueryObjects;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace JourneyPlannerCaller
{
    class GisCaller
    {
        #region Private constants/members

        // Constants
        private const string GIS_REQUESTS = "GisRequests";
        private const string GIS_REQUEST_FUNCTION = "GisRequest.{0}.Function";
        private const string GIS_REQUEST_FILE = "GisRequest.{0}.File";
        private const string GIS_RESULT_FILE = "GisResult.{0}.File";
        
        private const char REQUEST_IDS_SEPERATOR = ',';

        // Variables
        private Dictionary<string, IGisQuery> gisQueryDictionary = new Dictionary<string, IGisQuery>();

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

            // Reset the gis query dictionary
            gisQueryDictionary = new Dictionary<string, IGisQuery>();

            // Gis Request Ids suplied in the args which should be planned rather than those specified
            // in the config
            List<string> requestIds = JourneyPlannerCaller.GetRequestIds(args);
            
            // All parameter args have been validated, so can continue

            // Configuration setup ok
            if (returnCode == StatusCodes.JPC_SUCCESS)
            {
                returnCode = SetupGisRequestResultFiles(requestIds);
            }

            // Reading gis request and result paths ok
            if (returnCode == StatusCodes.JPC_SUCCESS)
            {


                #region GIS Requests

                int gisResultCode = StatusCodes.JPC_SUCCESS;

                // Initialise classes doing the work
                if (gisQueryDictionary.Count > 0)
                {
                    FileLogger.LogMessage("Calling GIS Function");
                    Console.WriteLine("Calling GIS Function");

                    GisRequestCaller gisRequestCaller = new GisRequestCaller();

                    // Call to gis query/function
                    gisResultCode = gisRequestCaller.PerformGisQueries(gisQueryDictionary);
                }


                // Log if no requests were found to run
                if ((gisQueryDictionary.Count == 0))
                {
                    FileLogger.LogMessage("No gis requests were found");
                    Console.WriteLine("No gis requests were found");

                    gisResultCode = StatusCodes.JPC_NOREQUESTS;

                }

                #endregion

                #region Log

                // Log success/failure result
                string resultMessage = string.Empty;
                if ((gisResultCode == StatusCodes.JPC_SUCCESS))
                {
                    resultMessage = string.Format("\nGis requests succeeded: result code[{0}]\n", StatusCodes.JPC_SUCCESS);

                }
                else
                {
                    resultMessage = string.Format("\nGis requests failed: resut code[{0}]\n", gisResultCode);
                }

                FileLogger.LogMessage(resultMessage);
                Console.WriteLine(resultMessage);

                #endregion

                returnCode = gisResultCode;

            }
            
            return returnCode;
        }

        #endregion

        #region Helper methods
        /// <summary>
        /// Method which reads the gis request and result files to process from the configuration.
        /// If request ids are provided, then these are run in preference to the ones specified 
        /// in the configuration file
        /// </summary>
        private int SetupGisRequestResultFiles(List<string> requestIds)
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
                        if (reqId.StartsWith("i") && reqId.Trim().Length > 1)
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
                    // Get file paths for the GIS requests
                    returnCode = GetGisFiles(requestsToPlan);
                }


            }
            catch (Exception ex)
            {
                // Write simple message to console
                string message = "Error attempting to identify gis request filepaths to plan. Please ensure the gis request ids and their configuration file settings are correct";
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
        /// Method to return a list of Gis request and result file paths read from the application settings
        /// </summary>
        /// <returns></returns>
        private int GetGisFiles(List<string> requestsToPlan)
        {
            int resultCode = StatusCodes.JPC_SUCCESS;

            #region Identify the request ids to get

            List<string> gisRequestIds = new List<string>();

            try
            {
                if (requestsToPlan.Count > 0)
                {
                    // Caller has specified which requests are to be planned
                    gisRequestIds = new List<string>(requestsToPlan);
                }
                else
                {
                    // Read which requests to plan from the configuration
                    string gisRequests = ConfigurationManager.AppSettings[GIS_REQUESTS];

                    if (!string.IsNullOrEmpty(gisRequests))
                    {
                        string[] requestIdsArray = gisRequests.Split(REQUEST_IDS_SEPERATOR);

                        if (requestIdsArray.Length > 0)
                        {
                            foreach (string reqId in requestIdsArray)
                            {
                                if (reqId.ToLower().StartsWith("i") && reqId.Trim().Length > 1)
                                {
                                    gisRequestIds.Add(reqId.Substring(1));
                                }
                            }
                        }
                    }
                }

            #endregion

                // Now get the individual gis request file paths
                foreach (string gisRequestId in gisRequestIds)
                {
                    // Gis function
                    string gisFunctionProperty = string.Format(GIS_REQUEST_FUNCTION, gisRequestId);
                    GisQueryFunction gisFunction = (GisQueryFunction)Enum.Parse(typeof(GisQueryFunction), ConfigurationManager.AppSettings[gisFunctionProperty]);

                    // Gis request and result filepaths
                    string gisRequestProperty = string.Format(GIS_REQUEST_FILE, gisRequestId);
                    string gisRequestFilepath = ConfigurationManager.AppSettings[gisRequestProperty];

                    string gisResultProperty = string.Format(GIS_RESULT_FILE, gisRequestId);
                    string gisResultFilepath = ConfigurationManager.AppSettings[gisResultProperty];


                    // Check a request file path exists
                    if (!string.IsNullOrEmpty(gisRequestFilepath))
                    {
                        int createGisQuerySuccess = StatusCodes.JPC_SUCCESS;

                        createGisQuerySuccess = CreateGisQueryObjects(gisRequestId, gisFunction, gisRequestFilepath, gisResultFilepath);

                        if (createGisQuerySuccess != StatusCodes.JPC_SUCCESS)
                            resultCode = createGisQuerySuccess;

                    }

                }

                
            }
            catch (Exception ex)
            {
                string message = "Error attempting to get/convert gis requests from request files provided. Please ensure the gis request ids, their configuration file settings and the request files are correct";
                Console.WriteLine(message);

                // More detail to the log file
                message = string.Format("{0} \nMessage[{1}] \n StackTrace[{2}] \n",
                    message,
                    ex.Message,
                    ex.StackTrace);
                FileLogger.LogMessage(message);

                resultCode = StatusCodes.JPC_EXCEPTION;
                
            }

            return resultCode;

        }


        /// <summary>
        /// Method to create the gis query objects, which associate a request with a result.
        /// </summary>
        /// <returns></returns>
        private int CreateGisQueryObjects(string gisRequestId, GisQueryFunction gisFunction, string gisRequestFilePath, string gisResultFilePath)
        {
            int resultCode = StatusCodes.JPC_SUCCESS;

            if (string.IsNullOrEmpty(gisRequestFilePath))
            {
                resultCode = StatusCodes.JPC_NOREQUESTS;
                return resultCode;
            }

            try
            {
                switch (gisFunction)
                {
                    case GisQueryFunction.FindExchangePointsInRadius:
                        {
                            ExchangePointQuery query = (ExchangePointQuery)ReadGisQueryRequest(typeof(ExchangePointQuery), gisRequestFilePath);
                            
                            if (!string.IsNullOrEmpty(gisResultFilePath))
                            {
                                query.GisResultPath = gisResultFilePath;
                            }
                            gisQueryDictionary.Add(gisRequestId, query);
                        }
                        break;

                    case GisQueryFunction.FindNearestITN:
                    case GisQueryFunction.FindNearestITNs:
                    case GisQueryFunction.FindNearestLocality:
                    case GisQueryFunction.FindNearestStops:
                    case GisQueryFunction.FindNearestStopsAndITNs:
                    case GisQueryFunction.FindStopsInfoForStops:
                    case GisQueryFunction.FindStopsInGroupForStops:
                    case GisQueryFunction.FindStopsInRadius:
                    case GisQueryFunction.FindNearestPointOnTOID:
                        {
                            FindNearestQuery query = (FindNearestQuery)ReadGisQueryRequest(typeof(FindNearestQuery), gisRequestFilePath);
                            if (!string.IsNullOrEmpty(gisResultFilePath))
                            {
                                query.GisResultPath = gisResultFilePath;
                            }
                            gisQueryDictionary.Add(gisRequestId, query);
                        }
                        break;

                    case GisQueryFunction.GetStreetsFromPostCode:
                    case GisQueryFunction.IsPointsInCycleDataArea:
                    case GisQueryFunction.IsPointsInWalkDataArea:
                        {
                            GisInfoQuery query = (GisInfoQuery)ReadGisQueryRequest(typeof(GisInfoQuery), gisRequestFilePath);
                            if (!string.IsNullOrEmpty(gisResultFilePath))
                            {
                                query.GisResultPath = gisResultFilePath;
                            }
                            gisQueryDictionary.Add(gisRequestId, query);
                        }
                        break;

                    case GisQueryFunction.FindNearestCarParks:
                        {
                            FindNearestCarParksQuery query = (FindNearestCarParksQuery)ReadGisQueryRequest(typeof(FindNearestCarParksQuery), gisRequestFilePath);
                            if (!string.IsNullOrEmpty(gisResultFilePath))
                            {
                                query.GisResultPath = gisResultFilePath;
                            }
                            gisQueryDictionary.Add(gisRequestId, query);
                        }
                        break;

                    default:
                        resultCode = StatusCodes.JPC_REQUESTFAILURE;
                        break;

                }
            }
            catch (Exception ex)
            {
                string message = "Error attempting to create gis requests objects from request files provided. Please ensure the gis request ids, their configuration file settings and the request files are correct";
                Console.WriteLine(message);

                // More detail to the log file
                message = string.Format("{0} \nMessage[{1}] \n StackTrace[{2}] \n",
                    message,
                    ex.Message,
                    ex.StackTrace);
                FileLogger.LogMessage(message);

                resultCode = StatusCodes.JPC_EXCEPTION;
            }

            return resultCode;
          
        }

        /// <summary>
        /// Generates a IGISQuery object from the supplied xml file path
        /// </summary>
        public Object ReadGisQueryRequest(Type queryType, string gisRequestFilePath)
        {
            Object gisRequest = null;
            TextReader reader = null;
            
            try
            {
                XmlSerializer serializer = new XmlSerializer(queryType);

                if (File.Exists(gisRequestFilePath))
                {
                    // Serialise the xml into the object
                    reader = new StreamReader(gisRequestFilePath);
                    gisRequest = serializer.Deserialize(reader);
                }
                else
                {
                    throw new Exception(string.Format("GIS request file[{0}] does not exist.", gisRequestFilePath));
                }

            }
            catch (Exception ex)
            {
                // Break out if any requests fail to be serialised
                throw new Exception(
                    string.Format("Error occurred parsing GIS request Xml file[{0}] into a GIS Request object. \n Exception Message: {1} \n StackTrace {2}",
                    gisRequestFilePath,
                    ex.Message,
                    ex.StackTrace));
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return gisRequest;
        }



        #endregion

        #endregion
    }
}
