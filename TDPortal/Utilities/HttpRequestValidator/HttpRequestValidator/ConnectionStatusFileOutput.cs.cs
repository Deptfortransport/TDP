using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Web.Administration;
using System.Xml;
using AO.HttpRequestValidatorCommon;

namespace AO.HttpRequestValidator
{
    public class ConnectionStatusFileOutput
    {
        #region Private members

        private static ConnectionStatusFileOutput instance = null;

        private static string dateTimeFormat = "yyyyMMdd HH:mm:ss.fffff";

        private bool fileSetUpOk = false;
        private string statusFile = string.Empty;

        private float connectionsForPeriod;
        private DateTime lastUpdated;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        private ConnectionStatusFileOutput()
        {
            // Initialise
            connectionsForPeriod = 0;
            lastUpdated = DateTime.Now;

            SetupFile();
        }

        #endregion

        #region Public property

        /// <summary>
        /// Returns the singleton instance of the ConnectionStatusFileOutput object
        /// </summary>
        public static ConnectionStatusFileOutput Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConnectionStatusFileOutput();
                }

                return instance;
            }
        }

        #endregion

        #region Public method

        /// <summary>
        /// Updates a text file with appropriate status details.
        /// </summary>
        public void UpdateStatusFile(float connectionsForPeriod, DateTime lastUpdated)
        {
            lock (this)
            {
                try
                {
                    // Update the instance member
                    this.connectionsForPeriod = connectionsForPeriod;
                    this.lastUpdated = lastUpdated;

                    UpdateFile(connectionsForPeriod, lastUpdated);
                }
                catch
                {
                    // No exceptions should be thrown back to caller as it doesnt care if the file isn't written correctly
                }
            }
        }

        #endregion

        #region Private method

        /// <summary>
        /// Sets up the file to write the status updates to
        /// </summary>
        private void SetupFile()
        {
            try
            {
                statusFile = GenerateFileName();

                if (!string.IsNullOrEmpty(statusFile))
                {
                    GenerateFile(statusFile, connectionsForPeriod, lastUpdated);

                    // Set the ok flag to allow future calls to update file to be done.
                    fileSetUpOk = true;
                }
                else
                {
                    throw new Exception("Unable to generate a status file.");
                }
            }
            catch
            {
                fileSetUpOk = false;

                // Throw so caller can handle, file log must be correctly setup
                throw;
            }
        }

        /// <summary>
        /// Updates the status file with the event status information
        /// </summary>
        private void UpdateFile(float connectionsForPeriod, DateTime lastUpdated)
        {
            try
            {
                if (fileSetUpOk)
                {
                    GenerateFile(statusFile, connectionsForPeriod, lastUpdated);
                }
            }
            catch
            {
                // File should have been setup correctly so ignore any exceptions here                
            }
        }

        /// <summary>
        /// Returns the filename with directory to write to
        /// </summary>
        /// <returns></returns>
        private string GenerateFileName()
        {
            try
            {
                ConfigurationSection section = Microsoft.Web.Administration.WebConfigurationManager.GetSection(HttpRequestValidatorKeys.ConfigurationSection);

                // Default
                string filePath = "D:\\TDPortal\\";
                string fileName = "RequestConnectionValidatorStatus.xml";

                try
                {
                    // Filepath for saving to
                    filePath = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_StatusFilePath).Value;
                }
                catch
                {
                    throw new Exception(string.Format("Failed to set RequestActionValidator Connection Status FilePath value, config[{0}] is missing or invalid",
                                    HttpRequestValidatorKeys.Connections_RestoreThreshold));
                }

                try
                {
                    // Filename for saving to
                    fileName = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_StatusFileName).Value;
                }
                catch
                {
                    throw new Exception(string.Format("Failed to set RequestActionValidator Connection Status FileName value, config[{0}] is missing or invalid",
                                    HttpRequestValidatorKeys.Connections_StatusFileName));
                }

                string file = filePath;

                if (!filePath.EndsWith("\\"))
                {
                    file += "\\";
                }

                file += fileName;

                return file;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Generates the status file (appending with blanks as required if 
        /// no EventStatus details are passed).
        /// </summary>
        private void GenerateFile(string file, float connectionsForPeriod, DateTime lastUpdated)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.OmitXmlDeclaration = false;

            // Update the status file
            using (XmlWriter xmlWriter = XmlWriter.Create(file, settings))
            {
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("RequestConnectionStatus");

                xmlWriter.WriteStartElement("RequestConnection");
                xmlWriter.WriteString("HttpRequestValidator");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("LastUpdated");
                xmlWriter.WriteString(lastUpdated.ToString(dateTimeFormat));
                xmlWriter.WriteEndElement();
                
                xmlWriter.WriteStartElement("ConnectionsForPeriod");
                xmlWriter.WriteString(connectionsForPeriod.ToString());
                xmlWriter.WriteEndElement();
                                
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();
            }
        }

        #endregion
    }
}
