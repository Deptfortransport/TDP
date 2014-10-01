// *********************************************** 
// NAME                 : HttpAccess.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Connects to a specified HTTP URL and downloads an XML or CSV file
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Common/HttpAccess.cs-arc  $
//
//   Rev 1.0   Apr 06 2009 16:14:20   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:37:12   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;

namespace AO.Common
{
    public class HttpAccess
    {
        #region Private members

        private static HttpAccess instance = null;

        /// <summary>
        /// Constructor
        /// </summary>
        private HttpAccess()
        {
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Returns the singleton instance of the HttpAccess object
        /// </summary>
        public static HttpAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HttpAccess();
                }

                return instance;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the XML file from an HTTP source
        /// </summary>
        /// <param name="HttpPath">The HTTP Source</param>
        /// <returns>An Xml Document containing the Xml from the Http Source</returns>
        public XmlDocument GetXMLFromHttp(string httpPath, string username, 
            string password, string domain, string proxy)
        {
            lock (this)
            {
                try
                {
                    // Set up the request
                    HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(httpPath);

                    // Only add credentials if passed in
                    if (!string.IsNullOrEmpty(username))
                    {
                        if (string.IsNullOrEmpty(password))
                        {
                            password = string.Empty;
                        }

                        if (string.IsNullOrEmpty(domain))
                        {
                            domain = string.Empty;
                        }

                        webRequest.Credentials = new NetworkCredential(username, password, domain);
                        webRequest.PreAuthenticate = true;
                    }

                    // Only add proxy if passed in
                    if (!string.IsNullOrEmpty(proxy))
                    {
                        WebProxy webProxy = new WebProxy(proxy, true);
                        webRequest.Proxy = webProxy;
                    }
                    
                    // Get the response
                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

                    // Read in the Xml doc returned
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.XmlResolver = null;
                    settings.ValidationType = ValidationType.None;
                    settings.ProhibitDtd = false;

                    XmlReader reader = XmlReader.Create(webResponse.GetResponseStream(), settings);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(reader);

                    // Tidy up
                    reader.Close();
                    
                    return xmlDoc;
                }
                catch (Exception ex)
                {                   
                    string message = string.Format(Messages.SSLSGetSiteStatusFromURLFail, httpPath, ex.Message);

                    throw new SSException(message, true, SSExceptionIdentifier.SSLSErrorRetrievingDataFromURL);
                }
            }
        }

        /// <summary>
        /// Get a CSV file from an HTTP source
        /// </summary>
        /// <returns>A csv file in a CSVReader object</returns>
        public CSVReader GetCSVFromHTTP(string httpPath, string username, 
            string password, string domain, string proxy)
        {
            lock (this)
            {
                try
                {
                    // Set up the request
                    HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(httpPath);

                    // Only add credentials if passed in
                    if (!string.IsNullOrEmpty(username))
                    {
                        if (string.IsNullOrEmpty(password))
                        {
                            password = string.Empty;
                        }

                        if (string.IsNullOrEmpty(domain))
                        {
                            domain = string.Empty;
                        }

                        webRequest.Credentials = new NetworkCredential(username, password, domain);
                        webRequest.PreAuthenticate = true;
                    }

                    // Only add proxy if passed in
                    if (!string.IsNullOrEmpty(proxy))
                    {
                        WebProxy webProxy = new WebProxy(proxy, true);
                        webRequest.Proxy = webProxy;
                    }

                    // Get the response
                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();


                    // Setup the response object
                    CSVReader csvReader = new CSVReader(webResponse.GetResponseStream());

                                        
                    return csvReader;
                }
                catch (CSVReaderException csvrEx)
                {
                    string message = string.Format(Messages.SSLSErrorReadingCSVData, csvrEx.Message);

                    throw new SSException(message, true, SSExceptionIdentifier.SSLSErrorRetrievingDataFromURL);
                }
                catch (Exception ex)
                {
                    string message = string.Format(Messages.SSLSGetSiteStatusFromURLFail, httpPath, ex.Message);

                    throw new SSException(message, true, SSExceptionIdentifier.SSLSErrorRetrievingDataFromURL);
                }
            }
        }


        /// <summary>
        /// Saves an XmlDocument to the specifed location
        /// </summary>
        public void SaveXMLDocument(XmlDocument xmlDoc, bool saveFile, string fileDirectory, string fileName)
        {
            if (saveFile)
            {
                // Save the file to the appropriate place
                SaveXMLFile(xmlDoc, saveFile, fileDirectory, fileName);
            }
        }

        /// <summary>
        /// Saves the CSVReader to a file location.
        /// ****IMPORTANT****
        /// Due to the current implementation of the CSVReader class, this save method if successful returns 
        /// a new instance of the CSVReader object reading in from the file it has saved to.
        /// </summary>
        /// <returns>Returns a new CSVReader from the save location</returns>
        public CSVReader SaveCSVReader(CSVReader csvReader, bool saveFile, string fileDirectory, string fileName)
        {
            // First can save to the file
            if (saveFile)
            {

                // Save the file to the appropriate place
                string savedToFile = SaveCSVFile(csvReader, fileDirectory, fileName);

                // If saved successfully, no exceptions will have been thrown, and there will be a filename
                if (!string.IsNullOrEmpty(savedToFile))
                {
                    // Discard the original csvReader
                    csvReader.Dispose();

                    // Read the data from the file in to the new CSVReader
                    CSVReader newCSVReader = new CSVReader(savedToFile);

                    return newCSVReader;
                }
                else
                {
                    // Filepath details were invalid, so no save was attempted, ok to return the original reader
                    return csvReader;
                }
            }
            else // Not allowed to save, so return the original reader
            {
                return csvReader;
            }

        }

        #endregion

        #region Save file

        /// <summary>
        /// Saves an XML document to a file
        /// </summary>
        /// <param name="xmlDoc"></param>
        private void SaveXMLFile(XmlDocument xmlDoc, bool saveFile, string fileDirectory, string fileName)
        {
            try
            {
                if ((!string.IsNullOrEmpty(fileDirectory)) && (!string.IsNullOrEmpty(fileName)))
                {
                    if (!fileDirectory.EndsWith(@"\"))
                    {
                        fileDirectory = fileDirectory + @"\";
                    }

                    string filepath = fileDirectory + fileName;
                    
                    xmlDoc.Save(filepath);
                }
            }
            catch (Exception ex)
            {
                throw new SSException(
                    string.Format(Messages.SSLSErrorSavingDataToFile, fileDirectory + fileName, ex.Message), 
                    false, SSExceptionIdentifier.SSLSErrorSavingDataToFile);
            }
        }


        /// <summary>
        /// Saves the CSV to a file
        /// </summary>
        /// <param name="cSVReader"></param>
        /// <returns></returns>
        private string SaveCSVFile(CSVReader csvReader, string fileDirectory, string fileName)
        {
            string filePath = GetCSVFileName(fileDirectory, fileName);

            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    csvReader.Save(filePath);
                }

            }
            catch (Exception ex)
            {
                throw new SSException(
                    string.Format(Messages.SSLSErrorSavingDataToFile, filePath, ex.Message),
                    false, SSExceptionIdentifier.SSLSErrorSavingDataToFile);
            }

            return filePath;
        }

        /// <summary>
        /// Returns the CSV filename and path to save to
        /// </summary>
        /// <returns></returns>
        private string GetCSVFileName(string fileDirectory, string fileName)
        {
            string filepath = string.Empty;

            if ((!string.IsNullOrEmpty(fileDirectory)) && (!string.IsNullOrEmpty(fileName)))
            {
                if (!fileDirectory.EndsWith(@"\"))
                {
                    fileDirectory = fileDirectory + @"\";
                }
                                
                filepath = fileDirectory + fileName;
            }

            return filepath;
        }

        #endregion
    }
}
