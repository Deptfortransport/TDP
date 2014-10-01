// *********************************************** 
// NAME                 : SiteStatusLoaderTest.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class to generate a test realtime Status file. This class is initiated on a new thread by the caller
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderService/SiteStatusLoaderTest.cs-arc  $
//
//   Rev 1.2   Aug 23 2011 11:01:38   mmodi
//Moved EventStatus code into a seperate DLL to allow the monitor tool to be enhanced with a manual import feature
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.1   Aug 23 2011 10:35:30   mmodi
//Updates to log historic multi-step transactions download data
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.0   Apr 01 2009 13:37:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;
using System.Xml;

using AO.Common;
using AO.EventLogging;
using AO.SiteStatusLoaderEvents;

using Logger = System.Diagnostics.Trace;
using PropertyService = AO.Properties.Properties;

namespace AO.SiteStatusLoaderService
{
    public class SiteStatusLoaderTest : IDisposable
    {
        private struct EventTest
        {
            public int eventId;
            public int status;
            public TimeSpan duration;
            public DateTime timeSubmitted;

            public EventTest(int eventId, int status,
                TimeSpan duration, DateTime timeSubmitted)
            {
                this.eventId = eventId;
                this.status = status;
                this.duration = duration;
                this.timeSubmitted = timeSubmitted;
            }
        }

        #region Private members

        static private string staticServiceName;
        static private string threadName = "Test";

        private string serviceName = string.Empty;
        private Timer serviceTimer; //Timer used for generating a test site status data file
        private bool disposed = false;
        private Random randomNumber;

        #endregion

        #region Static methods

        /// <summary>
        /// Method to initiate a new instance of the Site Status Loader Test
        /// </summary>
        public static void Run()
        {
            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                string.Format(Messages.SSLSLoaderServiceStarting, threadName)));

            try
            {
                SiteStatusLoaderTest ssLoader = new SiteStatusLoaderTest(staticServiceName);
                ssLoader.Start();
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                    string.Format(Messages.SSLSLoaderServiceStartFailed, threadName, ex.Message)));
            }

            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                string.Format(Messages.SSLSLoaderServiceStarted, threadName)));
        }

        /// <summary>
        /// Method to set up the static parameters needed when initiating an instance of the Site Status Loader Current using Run()
        /// </summary>
        /// <param name="serviceName"></param>
        public static void InitStaticParameters(string serviceName)
        {
            staticServiceName = serviceName;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SiteStatusLoaderTest(string serviceName)
        {
            this.serviceName = serviceName;
            this.randomNumber = new Random();
        }

        #endregion

        #region Service events - Start, Stop

        /// <summary>
        /// Start checking site status
        /// </summary>	
        public void Start()
        {
            // Start the timer
            SetupServiceTimerTest();
        }
        
        
        /// <summary>
        /// Stop checking site status
        /// </summary>
        public void Stop()
        {
            if (serviceTimer != null)
            {
                if (serviceTimer.Enabled)
                {
                    serviceTimer.Enabled = false;
                    serviceTimer.Stop();
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event called when the timer interval occurs
        /// </summary>
        private void serviceTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                GenerateTestSiteStatusData();
            }
            catch (SSException ssEx)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error, ssEx.Message));
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Disposes of resources. 
        /// Can be called by clients (via Dispose()) or runtime (via destructor).
        /// </summary>
        /// <param name="disposing">
        /// True when called by clients.
        /// False when called by runtime.
        /// </param>
        public void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (serviceTimer != null)
                    {
                        serviceTimer.Dispose();
                    }
                }
            }

            this.disposed = true;
        }

        /// <summary>
        /// Disposes of pool resources.
        /// Allows clients to dispose of resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // take off finalization queue to prevent dispose being called again.
        }


        /// <summary>
        /// Class destructor.
        /// </summary>
        ~SiteStatusLoaderTest()
        {
            Dispose(false);
        }

        #endregion

        #region Private methods

        #region Setup

        /// <summary>
        /// Sets up and enables the timer used for checking the site status every X milliseconds
        /// </summary>
        private void SetupServiceTimerTest()
        {
            #region Set up and start timer

            int timerInterval = 120000; // default - 2 minutes

            serviceTimer = new Timer();

            serviceTimer.BeginInit();

            serviceTimer.Interval = timerInterval;
            serviceTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.serviceTimer_Elapsed);

            serviceTimer.EndInit();

            serviceTimer.Enabled = true;
            serviceTimer.Start();

            #endregion
        }

        #endregion

        
        /// <summary>
        /// Method to get the site status data, process it, and call method to raise alerts
        /// </summary>
        private void GenerateTestSiteStatusData()
        {
            try
            {
                ArrayList events = CreateTestData();

                SaveFileCurrent(events);

                SaveFileHistoric(events);

                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSLSTestStatusEventDataCreated, threadName)));
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                    string.Format(Messages.SSLSLoaderError, threadName, ex.Message)));
            }
        }

        /// <summary>
        /// Creates the site status test data
        /// </summary>
        /// <returns></returns>
        private ArrayList CreateTestData()
        {
            ArrayList events = new ArrayList();

            List<int> eventsToMonitor = EventStatusParser.Instance.GetEventsToMonitor();

            foreach (int eventToMonitor in eventsToMonitor)
            {
                int eventID = eventToMonitor;
                int status = GetRandomStatus();
                TimeSpan duration = GetRandomDuration();
                DateTime timeSubmitted = DateTime.Now;

                EventTest eventTest = new EventTest(eventID, status, duration, timeSubmitted);

                events.Add(eventTest);

                System.Threading.Thread.Sleep(GetRandomMilliseconds());
            }

            return events;
        }

        /// <summary>
        /// Returns a random status of OK, BAD, FAIL
        /// </summary>
        /// <returns></returns>
        private int GetRandomStatus()
        {
            int statusInt = randomNumber.Next(30);

            if (statusInt > 4)
                return 1;
            else
                return statusInt;

        }

        /// <summary>
        /// Returns a random duration between 0 and 10 seconds, in the format "0.0000 s"
        /// </summary>
        /// <returns></returns>
        private TimeSpan GetRandomDuration()
        {
            double duration =  + randomNumber.NextDouble();

            TimeSpan timeSpan = new TimeSpan(0, 0, 0, randomNumber.Next(10), randomNumber.Next(1000));

            return timeSpan;
        }

        /// <summary>
        /// Returns a random number of milliseconds upto 5000
        /// </summary>
        /// <returns></returns>
        private int GetRandomMilliseconds()
        {
            return randomNumber.Next(5001);
        }

        /// <summary>
        /// Saves the file containing Test Site Status Events data to the current status file
        /// </summary>
        private void SaveFileCurrent(ArrayList events)
        {
            string file = PropertyService.Instance["SiteStatusLoaderService.TestMode.File.Current"];

            // XML file values
            string propNode = PropertyService.Instance["SiteStatusLoaderService.XML.Node"];

            string propAttributeEventID = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.EventID"];
            string propAttributeEventTypeName = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.EventTypeName"];
            string propAttributeStatus = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.Status"];
            string propAttributeDuration = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.Duration"];
            string propAttributeTimeSubmitted = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.TimeSubmitted"];

            string dateTimeFormatXML = PropertyService.Instance["SiteStatusLoaderService.XML.DateTimeFormat"];

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.OmitXmlDeclaration = false;

            using (XmlWriter xmlWriter = XmlWriter.Create(file, settings))
            {
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteDocType("Import", null, "./sbgwimport.dtd", null);

                xmlWriter.WriteStartElement("Import");

                foreach (EventTest eventTest in events)
                {
                    string durationString = eventTest.duration.TotalSeconds.ToString() + " s";

                    xmlWriter.WriteStartElement(propNode);

                    xmlWriter.WriteAttributeString(propAttributeEventID, eventTest.eventId.ToString());
                    xmlWriter.WriteAttributeString(propAttributeStatus, eventTest.status.ToString());
                    xmlWriter.WriteAttributeString(propAttributeDuration, durationString);
                    xmlWriter.WriteAttributeString(propAttributeTimeSubmitted, eventTest.timeSubmitted.ToString(dateTimeFormatXML));

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();

                xmlWriter.Close();
            }
        }

        /// <summary>
        /// Saves the file containing Test Site Status Events data to the historic status file
        /// </summary>
        private void SaveFileHistoric(ArrayList events)
        {
            string fileName = PropertyService.Instance["SiteStatusLoaderService.TestMode.File.Historic"];

            string dateTimeFormatCSV = PropertyService.Instance["SiteStatusLoaderService.CSV.DateTimeFormat"];

            string header = "URL,Page Name,Test Server,Start Date Time,DNS Time (ms),Connect Time (ms),First Data Time (ms),Complete Time (ms),Result Code,Total Bytes,Run ID";

            // Create the file and append a header 
            if (!File.Exists(fileName))
            {
                CreateFileAndWriteHeader(fileName, header);
            }

            StreamWriter sw = new StreamWriter(fileName, true);

            StringBuilder csvLine = new StringBuilder();

            string comma = ",";

            // Write each event status as a csv entry
            foreach (EventTest eventTest in events)
            {
                csvLine = new StringBuilder();

                csvLine.Append("testurl");
                csvLine.Append(comma);
                csvLine.Append(eventTest.eventId);
                csvLine.Append(comma);
                csvLine.Append("testserver");
                csvLine.Append(comma);
                csvLine.Append(eventTest.timeSubmitted.ToString(dateTimeFormatCSV));
                csvLine.Append(comma);
                csvLine.Append("testDNStime");
                csvLine.Append(comma);
                csvLine.Append("testconnecttime");
                csvLine.Append(comma);
                csvLine.Append("testfirstdatetime");
                csvLine.Append(comma);
                csvLine.Append(eventTest.duration.TotalMilliseconds.ToString());
                csvLine.Append(comma);
                csvLine.Append(Convert.ToString((int)eventTest.status));
                csvLine.Append(comma);
                csvLine.Append("testbytes");
                csvLine.Append(comma);
                csvLine.Append("testrunid");
                
                sw.WriteLine(csvLine.ToString());
            }
            
            sw.Flush();
            
            sw.Close();
        }
                
        /// <summary>
        /// Creates the csv file and writes a header line
        /// </summary>
        private void CreateFileAndWriteHeader(string fileName, string header)
        {
            StreamWriter sw = new StreamWriter(fileName, false);

            sw.WriteLine(header);

            sw.Flush();

            sw.Close();
        }


        #endregion
    }
}
