// *********************************************** 
// NAME                 : EventStatusFileOutput.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class to output the real time Status data to a file 
//                       (to allow the UI Monitor Application to read status and behave accordingly)
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderEvents/EventStatusFileOutput.cs-arc  $
//
//   Rev 1.0   Aug 23 2011 11:04:34   mmodi
//Initial revision.
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.3   Aug 23 2011 10:35:22   mmodi
//Updates to log historic multi-step transactions download data
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.2   Jun 16 2009 15:49:50   mmodi
//Updated to write date using a particular format
//Resolution for 5298: Site Status Loader - Update to log Red Alerts as successful
//
//   Rev 1.1   Apr 06 2009 16:07:50   mmodi
//Added alert count values to the output
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:37:12   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text;

using AO.Common;
using AO.EventLogging;

using PropertyService = AO.Properties.Properties;
using Logger = System.Diagnostics.Trace;
using System.Xml;

namespace AO.SiteStatusLoaderEvents
{
    public class EventStatusFileOutput
    {
        #region Private members

        private static EventStatusFileOutput instance = null;

        private static string dateTimeFormat = "yyyyMMdd HH:mm:ss.fffff";

        private bool fileSetUpOk = false;
        private string statusFile = string.Empty;

        private Dictionary<AlertLevel, int> alertLevelCounts;
        private DateTime alertLevelCountsLastUpdated;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        private EventStatusFileOutput()
        {
            // Initialise
            alertLevelCounts = new Dictionary<AlertLevel, int>();
            alertLevelCountsLastUpdated = DateTime.Now;

            SetupFile();
        }

        #endregion

        #region Public property

        /// <summary>
        /// Returns the singleton instance of the EventStatusFileOutput object
        /// </summary>
        public static EventStatusFileOutput Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventStatusFileOutput();
                    dateTimeFormat = PropertyService.Instance["SiteStatusLoaderService.StatusFile.DateTimeFormat"];
                }

                return instance;
            }
        }

        #endregion

        #region Public method
        
        /// <summary>
        /// Takes an SSException and updates a text file with the error details.
        /// </summary>
        public void UpdateStatusFile (SSException exception)
        {
            lock (this)
            {
                try
                {
                    UpdateFile(null, alertLevelCounts, alertLevelCountsLastUpdated, exception);
                }
                catch 
                {
                    // No exceptions should be thrown back to caller as it doesnt care if the file isn't written correctly
                }
            }
        }

        /// <summary>
        /// Takes an EventStatusArray and AlertLevel counts updates a text file with appropriate status details.
        /// </summary>
        public void UpdateStatusFile(EventStatus[] eventStatusArray, Dictionary<AlertLevel, int> alertLevelCounts, DateTime alertLevelCountsLastUpdated)
        {
            lock (this)
            {
                try
                {
                    // Update the instance member
                    if (alertLevelCounts != null)
                    {
                        this.alertLevelCounts = alertLevelCounts;
                        this.alertLevelCountsLastUpdated = alertLevelCountsLastUpdated;
                    }


                    UpdateFile(eventStatusArray, alertLevelCounts, alertLevelCountsLastUpdated, new SSException());
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
                    GenerateFile(statusFile, null, alertLevelCounts, alertLevelCountsLastUpdated, new SSException());

                    // Set the ok flag to allow future calls to update file to be done.
                    fileSetUpOk = true;
                }
                else
                {
                    throw new Exception("Unable to generate a status file using properties.");
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                    string.Format(Messages.SSLSUnableToSetupMonitorStatusFile, ex.Message)));

                fileSetUpOk = false;
            }
        }

        /// <summary>
        /// Updates the status file with the event status information
        /// </summary>
        private void UpdateFile(EventStatus[] eventStatusArray, Dictionary<AlertLevel, int> alertLevelCounts , DateTime alertLevelCountsLastUpdated, SSException exception)
        {
            try
            {
                if (fileSetUpOk)
                {
                    GenerateFile(statusFile, eventStatusArray, alertLevelCounts, alertLevelCountsLastUpdated, exception);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                    string.Format(Messages.SSLSErrorDuringUpdateMonitorStatusFile, ex.Message)));
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
                string filePath = PropertyService.Instance["SiteStatusLoaderService.StatusFile.Directory"];
                string fileName = PropertyService.Instance["SiteStatusLoaderService.StatusFile.Name"];

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
                return string.Empty;
            }
        }

        /// <summary>
        /// Generates the status file (appending with blanks as required if 
        /// no EventStatus details are passed).
        /// </summary>
        private void GenerateFile(string file, EventStatus[] eventStatusArray, Dictionary<AlertLevel, int> alertLevelCounts , DateTime alertLevelCountsLastUpdated,  SSException exception)
        {
            // Get the events being monitored
            List<int> eventsToMonitor = EventStatusParser.Instance.GetEventsToMonitor();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.OmitXmlDeclaration = false;

            // Create and set up the Status file
            using (XmlWriter xmlWriter = XmlWriter.Create(file, settings))
            {
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("EventStatus");

                xmlWriter.WriteStartElement("Service");
                xmlWriter.WriteString(ConfigurationManager.AppSettings["ServiceName"]);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("LastUpdated");
                xmlWriter.WriteString(DateTime.Now.ToString(dateTimeFormat));
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ErrorId");
                xmlWriter.WriteString(Convert.ToString((int)exception.Identifier));
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("ErrorMessage");
                xmlWriter.WriteString(exception.Message);
                xmlWriter.WriteEndElement();

                // Write the event status and their alert level
                foreach (int eventToMonitor in eventsToMonitor)
                {
                    xmlWriter.WriteStartElement("Status");

                    xmlWriter.WriteAttributeString("EventID", eventToMonitor.ToString());

                    bool found = false;

                    if (eventStatusArray != null)
                    {
                        // Assume theres only one EventStatus for each eventName
                        foreach (EventStatus eventStatus in eventStatusArray)
                        {
                            if (eventStatus.EventID == eventToMonitor)
                            {
                                xmlWriter.WriteAttributeString("EventName", eventStatus.EventName);

                                xmlWriter.WriteAttributeString("TimeSubmitted", eventStatus.TimeSubmitted.ToString(dateTimeFormat));
                                xmlWriter.WriteAttributeString("Duration", eventStatus.Duration.TotalSeconds.ToString() );

                                xmlWriter.WriteString(eventStatus.AlertLevel.ToString());

                                found = true;
                                break;
                            }
                        }
                    }

                    if (!found)
                    {
                        xmlWriter.WriteAttributeString("EventName", "Unknown Event");
                        
                        xmlWriter.WriteAttributeString("TimeSubmitted", new DateTime(1900, 1, 1, 0, 0, 0).ToString(dateTimeFormat));
                        xmlWriter.WriteAttributeString("Duration", "0");

                        xmlWriter.WriteString(AlertLevel.Unknown.ToString());
                    }

                    xmlWriter.WriteEndElement();
                }

                // Write out the alert level counts
                foreach (KeyValuePair<AlertLevel, int> kvp in alertLevelCounts)
                {
                    xmlWriter.WriteStartElement("AlertCount");

                    xmlWriter.WriteAttributeString("AlertLevel", kvp.Key.ToString());

                    xmlWriter.WriteString(kvp.Value.ToString());

                    xmlWriter.WriteEndElement();
                }

                // Write out the last time the alert level counts were updated
                xmlWriter.WriteStartElement("AlertCountLastUpdated");
                xmlWriter.WriteString(alertLevelCountsLastUpdated.ToString(dateTimeFormat));
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();

                xmlWriter.Close();
            }
        }

        #endregion
    }
}
