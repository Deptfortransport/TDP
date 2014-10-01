// *********************************************** 
// NAME                 : JourneyPlannerHelper.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/04/2010
// DESCRIPTION          : Helper Class containing methods needed by the Journey Request Caller classes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/JourneyPlannerHelper.cs-arc  $ 
//
//   Rev 1.0   Apr 20 2010 16:39:14   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 20 2010 15:41:22   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using CJP = TransportDirect.JourneyPlanning.CJPInterface;
using CPWS = JourneyPlannerCaller.CyclePlannerWebService;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// Helper Class containing methods needed by the Journey Request Caller classes
    /// </summary>
    public class JourneyPlannerHelper
    {
        private const string LOG_DATETIME_FORMAT = "dd-MM-yyyy HH:mm:ss";

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyPlannerHelper()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method which checks the filepaths are valid, and updates the result filepaths to write to 
        /// with a date time 
        /// </summary>
        public void ValidateJourneyRequestResultFilePaths(ref string[] requestFilePaths, ref string[] resultFilePaths)
        {
            if ((requestFilePaths != null) && (resultFilePaths != null))
            {
                // Request and result file paths must be same length
                if (requestFilePaths.Length != resultFilePaths.Length)
                {
                    throw new Exception("Journey request and result filepaths count were not the same.");
                }
                else
                {
                    #region Append datetime to the result filepaths

                    // If all the results paths are the same, then user wants to write to one result file only.
                    Hashtable filepaths = new Hashtable();

                    for (int i = 0; i < resultFilePaths.Length; i++)
                    {
                        string resultFilepath = resultFilePaths[i];

                        // If theres no file path specified, then do not need to save results to a file
                        if (!string.IsNullOrEmpty(resultFilepath))
                        {
                            if (!filepaths.Contains(resultFilepath))
                            {
                                // Append a datetime stamp to the result file path
                                string file_part = resultFilepath.Substring(0, resultFilepath.LastIndexOf('.'));
                                string extn_part = resultFilepath.Substring(resultFilepath.LastIndexOf('.'), (resultFilepath.Length - resultFilepath.LastIndexOf('.')));

                                string newFilepath = string.Format("{0}_{1}{2}",
                                    file_part,
                                    DateTime.Now.ToString("yyyyMMddHHmmss"),
                                    extn_part);

                                // And update the filepath to write to
                                resultFilePaths[i] = newFilepath;

                                // Track this filepath
                                filepaths.Add(resultFilepath, newFilepath);
                            }
                            else
                            {
                                // Same as a previous file, so use the previously created file name
                                resultFilePaths[i] = (string)filepaths[resultFilepath];
                            }
                        }
                    }

                    #endregion
                }
            }
            else
            {
                throw new Exception("No journey request and/or journey result filepaths were supplied");
            }
        }

        /// <summary>
        /// Returns a valid journey planner timeout, defaulting to 60000
        /// </summary>
        /// <param name="journeyPlannerTimeout"></param>
        /// <returns></returns>
        public int ValidateJourneyPlannerTimeout(string journeyPlannerTimeout)
        {
            int timeoutmilliseconds = 0;

            if (Int32.TryParse(journeyPlannerTimeout, out timeoutmilliseconds))
            {
                return timeoutmilliseconds;
            }
            else
            {
                FileLogger.LogMessage("Invalid journey planner timeout value detected, defaulting to 60 seconds. \n");

                // Default value
                return 60000;
            }
        }

        #region CJP specific

        /// <summary>
        /// Generates a CJP JourneyRequest object from the supplied xml file path
        /// </summary>
        public CJP.JourneyRequest ReadCJPJourneyRequest(string filepath)
        {
            CJP.JourneyRequest journeyRequest = null;
            TextReader reader = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CJP.JourneyRequest));

                if (File.Exists(filepath))
                {
                    // Serialise the xml into the object
                    reader = new StreamReader(filepath);
                    journeyRequest = (CJP.JourneyRequest)serializer.Deserialize(reader);
                }
                else
                {
                    throw new Exception(string.Format("Journey request file[{0}] does not exist.", filepath));
                }

            }
            catch (Exception ex)
            {
                // Break out if any requests fail to be serialised
                throw new Exception(
                    string.Format("Error occurred parsing journey request Xml file[{0}] into a JourneyRequest object. \n Exception Message: {1} \n StackTrace {2}",
                    filepath,
                    ex.Message,
                    ex.StackTrace));
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return journeyRequest;
        }

        /// <summary>
        /// Writes a JourneyResult object to the supplied file path, appending to the end of the file
        /// </summary>
        public void WriteJourneyResult(CJP.JourneyResult jr, string filepath)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                StreamWriter writer = new StreamWriter(filepath, true);

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CJP.JourneyResult));

                    if (jr != null)
                    {
                        serializer.Serialize(writer, jr);

                        // New line for next entry if there is one
                        writer.WriteLine(string.Empty);
                    }
                    else
                    {
                        throw new Exception("JourneyResult object was null, unable to create result XML file");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(
                        string.Format("Error occurred writing JourneyResult to an Xml file[{0}]. \n Exception Message: {1} \n StackTrace {2}",
                        filepath,
                        ex.Message,
                        ex.StackTrace));
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }
        }

        /// <summary>
        /// Validates the journey request parameters
        /// </summary>
        /// <param name="jr"></param>
        /// <returns></returns>
        public CJP.JourneyRequest ValidateJourneyRequest(CJP.JourneyRequest journeyRequest)
        {
            CJP.JourneyRequest jr = journeyRequest;

            #region Validate request id

            // Ensure theres a request id
            if (string.IsNullOrEmpty(jr.requestID))
            {
                jr.requestID = string.Format("JourneyRequestCaller_{0}", DateTime.Now.ToString("yyyyMMddHHmmss.fffff"));
            }

            #endregion

            #region Validate dates

            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            // Check if the supplied date is today or later, if not set it to be tomorrow
            // Added to avoid having to constantly update the date in the xml files

            if (jr.origin.stops != null)
            {
                for (int i = 0; i < jr.origin.stops.Length; i++)
                {
                    CJP.RequestStop stop = jr.origin.stops[i];

                    // Ignore minutes
                    DateTime journeyDateTime = stop.timeDate.Date;

                    // Date is in the past, update it to tomorrow
                    if ((journeyDateTime != DateTime.MinValue) && (journeyDateTime < today))
                    {
                        stop.timeDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day,
                            stop.timeDate.Hour, stop.timeDate.Minute, 0);

                        FileLogger.LogMessage(string.Format("Origin date for journey request id[{0}] stop[{1}] is in the past, date has been updated to be [{2}]",
                            jr.requestID, stop.NaPTANID, stop.timeDate.ToString(LOG_DATETIME_FORMAT)));
                    }
                }
            }

            if (jr.destination.stops != null)
            {
                for (int i = 0; i < jr.destination.stops.Length; i++)
                {
                    CJP.RequestStop stop = jr.origin.stops[i];

                    // Ignore minutes
                    DateTime journeyDateTime = stop.timeDate.Date;

                    // Date is in the past, update it to tomorrow
                    if ((journeyDateTime != DateTime.MinValue) && (journeyDateTime < today))
                    {
                        stop.timeDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day,
                            stop.timeDate.Hour, stop.timeDate.Minute, 0);

                        FileLogger.LogMessage(string.Format("Destination date for journey request id[{0}] stop[{1}] is in the past, date has been updated to be [{2}]",
                            jr.requestID, stop.NaPTANID, stop.timeDate.ToString(LOG_DATETIME_FORMAT)));
                    }
                }
            }

            if (jr.origin.roadPoints != null)
            {
                for (int i = 0; i < jr.origin.roadPoints.Length; i++)
                {
                    CJP.ITN itn = jr.origin.roadPoints[i];

                    // Ignore minutes
                    DateTime journeyDateTime = itn.timeDate.Date;

                    // Date is in the past, update it to tomorrow
                    if ((journeyDateTime != DateTime.MinValue) && (journeyDateTime < today))
                    {
                        itn.timeDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day,
                            itn.timeDate.Hour, itn.timeDate.Minute, 0);

                        FileLogger.LogMessage(string.Format("Origin date for journey request id[{0}] itn[{1}] is in the past, date has been updated to be [{2}]",
                            jr.requestID, itn.TOID, itn.timeDate.ToString(LOG_DATETIME_FORMAT)));
                    }
                }
            }

            if (jr.destination.roadPoints != null)
            {
                for (int i = 0; i < jr.destination.roadPoints.Length; i++)
                {
                    CJP.ITN itn = jr.destination.roadPoints[i];

                    // Ignore minutes
                    DateTime journeyDateTime = itn.timeDate.Date;

                    // Date is in the past, update it to tomorrow
                    if ((journeyDateTime != DateTime.MinValue) && (journeyDateTime < today))
                    {
                        itn.timeDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day,
                            itn.timeDate.Hour, itn.timeDate.Minute, 0);

                        FileLogger.LogMessage(string.Format("Destination date for journey request id[{0}] itn[{1}] is in the past, date has been updated to be [{2}]",
                            jr.requestID, itn.TOID, itn.timeDate.ToString(LOG_DATETIME_FORMAT)));
                    }
                }
            }
            #endregion

            return jr;
        }

        /// <summary>
        /// Returns the Journey Result messages formatted as a single string
        /// </summary>
        /// <returns></returns>
        public string GetJourneyResultMessage(CJP.JourneyResult journeyResult)
        {
            StringBuilder formattedmessage = new StringBuilder();

            if ((journeyResult != null) && (journeyResult.messages != null))
            {
                foreach (CJP.Message message in journeyResult.messages)
                {
                    formattedmessage.Append(message.code);
                    formattedmessage.Append(" - ");
                    formattedmessage.Append(message.description);
                    formattedmessage.Append(".\n");
                }
            }

            return formattedmessage.ToString();
        }


        #endregion

        #region Cycle Planner specific
        
        /// <summary>
        /// Generates a CJP JourneyRequest object from the supplied xml file path
        /// </summary>
        public CPWS.JourneyRequest ReadCycleJourneyRequest(string filepath)
        {
            CPWS.JourneyRequest journeyRequest = null;
            TextReader reader = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CPWS.JourneyRequest));

                if (File.Exists(filepath))
                {
                    // Serialise the xml into the object
                    reader = new StreamReader(filepath);
                    journeyRequest = (CPWS.JourneyRequest)serializer.Deserialize(reader);
                }
                else
                {
                    throw new Exception(string.Format("Journey request file[{0}] does not exist.", filepath));
                }

            }
            catch (Exception ex)
            {
                // Break out if any requests fail to be serialised
                throw new Exception(
                    string.Format("Error occurred parsing journey request Xml file[{0}] into a JourneyRequest object. \n Exception Message: {1} \n StackTrace {2}",
                    filepath,
                    ex.Message,
                    ex.StackTrace));
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return journeyRequest;
        }

        /// <summary>
        /// Writes a JourneyResult object to the supplied file path, appending to the end of the file
        /// </summary>
        public void WriteJourneyResult(CPWS.JourneyResult jr, string filepath)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                StreamWriter writer = new StreamWriter(filepath, true);

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(CPWS.JourneyResult));

                    if (jr != null)
                    {
                        serializer.Serialize(writer, jr);

                        // New line for next entry if there is one
                        writer.WriteLine(string.Empty);
                    }
                    else
                    {
                        throw new Exception("JourneyResult object was null, unable to create result XML file");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(
                        string.Format("Error occurred writing JourneyResult to an Xml file[{0}]. \n Exception Message: {1} \n StackTrace {2}",
                        filepath,
                        ex.Message,
                        ex.StackTrace));
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }
        }

        /// <summary>
        /// Validates the journey request parameters
        /// </summary>
        /// <param name="jr"></param>
        /// <returns></returns>
        public CPWS.JourneyRequest ValidateJourneyRequest(CPWS.JourneyRequest journeyRequest)
        {
            CPWS.JourneyRequest jr = journeyRequest;

            #region Validate request id

            // Ensure theres a request id and its unique
            if (string.IsNullOrEmpty(jr.requestID))
            {
                jr.requestID = string.Format("JourneyRequestCaller_{0}", DateTime.Now.ToString("yyyyMMddHHmmss.fffff"));
            }

            #endregion

            #region Validate dates

            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            // Check if the supplied date is today or later, if not set it to be tomorrow
            // Added to avoid having to constantly update the date in the xml files


            // Ignore minutes
            DateTime journeyDateTime = jr.origin.timeDate.Date;

            // Date is in the past, update it to tomorrow
            if ((journeyDateTime != DateTime.MinValue) && (journeyDateTime < today))
            {
                jr.origin.timeDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day,
                    jr.origin.timeDate.Hour, jr.origin.timeDate.Minute, 0);

                FileLogger.LogMessage(string.Format("Origin date for journey request id[{0}] name[{1}] is in the past, date has been updated to be [{2}]",
                    jr.requestID, jr.origin.givenName, jr.origin.timeDate.ToString(LOG_DATETIME_FORMAT)));
            }


            // Ignore minutes
            journeyDateTime = jr.destination.timeDate.Date;

            // Date is in the past, update it to tomorrow
            if ((journeyDateTime != DateTime.MinValue) && (journeyDateTime < today))
            {
                jr.destination.timeDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day,
                    jr.destination.timeDate.Hour, jr.destination.timeDate.Minute, 0);

                FileLogger.LogMessage(string.Format("Destination date for journey request id[{0}] name[{1}] is in the past, date has been updated to be [{2}]",
                    jr.requestID, jr.destination.givenName, jr.destination.timeDate.ToString(LOG_DATETIME_FORMAT)));
            }

            #endregion

            return jr;
        }

        /// <summary>
        /// Returns the Journey Result messages formatted as a single string
        /// </summary>
        /// <returns></returns>
        public string GetJourneyResultMessage(CPWS.JourneyResult journeyResult)
        {
            StringBuilder formattedmessage = new StringBuilder();

            if ((journeyResult != null) && (journeyResult.messages != null))
            {
                foreach (CPWS.Message message in journeyResult.messages)
                {
                    formattedmessage.Append(message.code);
                    formattedmessage.Append(" - ");
                    formattedmessage.Append(message.description);
                    formattedmessage.Append(".\n");
                }
            }

            return formattedmessage.ToString();
        }

        /// <summary>
        /// Returns the Cycle Planner Result messages formatted as a single string
        /// </summary>
        /// <returns></returns>
        public string GetJourneyResultMessage(CPWS.CyclePlannerResult journeyResult)
        {
            StringBuilder formattedmessage = new StringBuilder();

            if ((journeyResult != null) && (journeyResult.messages != null))
            {
                foreach (CPWS.Message message in journeyResult.messages)
                {
                    formattedmessage.Append(message.code);
                    formattedmessage.Append(" - ");
                    formattedmessage.Append(message.description);
                    formattedmessage.Append(".\n");
                }
            }

            return formattedmessage.ToString();
        }

        #endregion

        #endregion

    }
}
