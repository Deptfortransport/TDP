// *********************************************** 
// NAME             : GazRequestCaller.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 30 Nov 2010
// DESCRIPTION  	: Class providing core functionality to call GAZ web service
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/GazRequestCaller.cs-arc  $
//
//   Rev 1.0   Nov 30 2010 13:33:46   apatel
//Initial revision.
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using JourneyPlannerCaller.Properties;

namespace JourneyPlannerCaller
{
    /// <summary>
    /// GazRequestCaller class
    /// </summary>
    class GazRequestCaller
    {
        #region Private Fields
        private const string LOG_DATETIME_FORMAT = "dd-MM-yyyy HH:mm:ss";
        private string gazWebUrl = string.Empty;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public GazRequestCaller()
        {
            gazWebUrl = Settings.Default.JourneyPlannerCaller_GazWebService_GazService;
        }
        #endregion

        #region Internal Method
        /// <summary>
        /// Method handling calls to GAZ
        /// </summary>
        /// <param name="gazQueryDictionary">Dictionary containing GAZ request calls</param>
        /// <returns></returns>
        internal int PerformGazQueries(Dictionary<string, GazQuery> gazQueryDictionary)
        {
            int gazStatus = StatusCodes.JPC_SUCCESS;

            foreach (string requestId in gazQueryDictionary.Keys)
            {

                int status = CallGaz(gazQueryDictionary[requestId]);

                if (status != StatusCodes.JPC_SUCCESS)
                    gazStatus = status;
            }

            return gazStatus;

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Method calling the GAZ as per the gaz query request
        /// </summary>
        /// <param name="gazQuery"></param>
        /// <returns></returns>
        private int CallGaz(GazQuery gazQuery)
        {
            int gazStatus = StatusCodes.JPC_SUCCESS;

            #region Gaz web request
            try
            {
               
                gazWebUrl = string.Format("{0}?op={1}", gazWebUrl, gazQuery.GazFunction);

                FileLogger.LogMessage(string.Format("Calling GAZ with function {0} at url {1}", gazQuery.GazFunction, gazWebUrl));

                HttpWebResponse resp;


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(gazWebUrl);

                // read input XML from file

                StringBuilder sb = new StringBuilder();

                FileLogger.LogMessage(string.Format("Building Gaz request for function {0}", gazQuery.GazFunction));

                using (StreamReader gazQueryReader = new StreamReader(gazQuery.RequestPath))
                {

                    sb.Append(gazQueryReader.ReadToEnd());

                }

                FileLogger.LogMessage(string.Format("{0}\n{1}", "GAZ Request for function" + gazQuery.GazFunction, gazQuery.RequestPath));

                String postData = sb.ToString();

                byte[] postDataBytes = Encoding.UTF8.GetBytes(postData);

                //request.Credentials = cred;

                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                // if SOAPAction header is required, add it here...
                request.Headers.Add("SOAPAction", gazQuery.SoapAction);

                request.ContentLength = postDataBytes.Length;

                using (Stream requestStream = request.GetRequestStream())
                {

                    requestStream.Write(postDataBytes, 0, postDataBytes.Length);

                    requestStream.Close();
                }



                // get response and write to console

                resp = (HttpWebResponse)request.GetResponse();   

                FileLogger.LogMessage(string.Format("GAZ request succeeded for function {0}", gazQuery.GazFunction));

                using (StreamReader responseReader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                {
                    string gazResponse = responseReader.ReadToEnd();

                    FileLogger.LogMessage(string.Format("{0}\n{1}", "GAZ Response for function" + gazQuery.GazFunction, gazResponse));

                    LogToFile(gazQuery.GazFunction, gazQuery.ResultPath, gazResponse);
                }

                resp.Close();
            }
            catch (WebException we)
            {
                gazStatus = StatusCodes.JPC_REQUESTFAILURE;

                FileLogger.LogMessage(string.Format("{0} failed with web exception: {1}", "GAZ Response for function" + gazQuery.GazFunction, we.StackTrace));
            }
            catch (Exception ex)
            {
                gazStatus = StatusCodes.JPC_EXCEPTION;

                FileLogger.LogMessage(string.Format("{0} failed with exception: {1}", "GAZ Response for function" + gazQuery.GazFunction,ex.StackTrace));
            }

            #endregion

            FileLogger.LogMessage(string.Format("{0}\n{1}", "GAZ Request for function" + gazQuery.GazFunction + " exited with status " + gazStatus, gazQuery.RequestPath));


            return gazStatus;


        }

        /// <summary>
        /// Logs the Gaz result to file
        /// </summary>
        /// <param name="function">GAZ function</param>
        /// <param name="filePath">GAZ result file path</param>
        /// <param name="content">Content to be written to GAZ result file</param>
        private void LogToFile(string function, string filePath, string content)
        {
            StringBuilder resultContent = new StringBuilder();

            
            // Append a datetime stamp to the result file path
            string file_part = filePath.Substring(0, filePath.LastIndexOf('.'));
            string extn_part = filePath.Substring(filePath.LastIndexOf('.'), (filePath.Length - filePath.LastIndexOf('.')));

            string newFilepath = string.Format("{0}_{1}{2}",
                file_part,
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                extn_part);

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);

                string nodeToMatch = function + "Result";
                XmlNodeList resultNodes = doc.GetElementsByTagName(nodeToMatch);

                if (resultNodes != null && resultNodes.Count > 0)
                {
                    
                    foreach (XmlNode resultNode in resultNodes)
                    {
                        resultContent.Append(resultNode.InnerText);
                        resultContent.AppendLine();
                        resultContent.AppendLine();
                    }
                }
                else
                {
                    resultContent.Append(content);
                }
            }
            catch
            {
                resultContent.Append(content);
            }


            using (StreamWriter writer = new StreamWriter(newFilepath))
            {
                writer.Write(resultContent.ToString().Trim());
                writer.Flush();
                writer.Close();
            }
        }

        #endregion
    }
}
