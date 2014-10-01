// *********************************************** 
// NAME                 : PropertiesTemplate.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: PropertiesTemplate static class generates a file which contains a template 
//                      : of the properties needed by the application.
//                      : This will require updating if any mandatory properties are needed by the application.
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Properties/PropertiesTemplate.cs-arc  $
//
//   Rev 1.1   Aug 24 2011 11:07:46   mmodi
//Added a manual import facility to the SSL UI tool
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.0   Apr 09 2009 10:41:00   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace AO.Properties
{
    public class PropertiesTemplate
    {
        private static PropertiesTemplate instance = null;

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        private PropertiesTemplate()
        {
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Returns the singleton instance of the PropertiesTemplate object
        /// </summary>
        public static PropertiesTemplate Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PropertiesTemplate();
                }

                return instance;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Creates a Properties template file at the path specified
        /// </summary>
        public bool CreatePropertiesTemplateFile(string filepath)
        {
            lock (this)
            {
                try
                {
                    if (!File.Exists(filepath))
                    {
                        return CreateTemplateFile(filepath);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    // Don't send any exceptions back to caller
                    return false;
                }
            }
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Creates a template xml properties file, populated with the necessary values
        /// </summary>
        /// <param name="filepath"></param>
        private bool CreateTemplateFile(string filepath)
        {
            string property = "property";
            string name = "name";
            string GID = "GID";
            string AID = "AID";
            string SiteStatus = "SiteStatus";
            string SiteStatusLoaderService = "SiteStatusLoaderService";
            string SiteStatusLoaderMonitor = "SiteStatusLoaderMonitor";
            string Empty = "";
            //string newline = "\r\n";

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.OmitXmlDeclaration = false;

            // Create and set up the template file
            using (XmlWriter xmlWriter = XmlWriter.Create(filepath, settings))
            {
                xmlWriter.WriteStartDocument();

                xmlWriter.WriteStartElement("lookup");

                #region Common properties

                xmlWriter.WriteComment(" ********************************************************************************************************* ");
                xmlWriter.WriteComment(" ******************************************* Common properties ******************************************* ");
                xmlWriter.WriteComment(" ********************************************************************************************************* ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "propertyservice.version");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "propertyservice.refreshrate");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("10000");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Common properties - Database ");
                
                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "ReportStagingDB");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("DATABASE CONNECTION STRING");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Common properties - Event logging ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.Default");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.EventLog");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("EventLog1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.File");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Internal file used to track status between the LoaderService and the Monitor Application ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.StatusFile.Directory");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("DIRECTORY PATH e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.StatusFile.Name");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("SiteStatusLoaderServiceStatus.xml");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.StatusFile.DateTimeFormat");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("yyyyMMdd HH:mm:ss.fffff");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Site Confidence URLs to obtain data from ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Current.URL");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("REALTIME SITE STATUS DATA URL e.g. http://www.test.com/data.xml");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Current.URL.Username");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Current.URL.Password");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Current.URL.Domain");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Current.URL.Proxy");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.URL.Base");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("HISTORIC SITE STATUS DATA URL e.g. http://www.test.com/");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.URL.Base.PID");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("PAGE IDs TO DOWNLOAD e.g. 1234,5678");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.URL.Base.PIDPrefix");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("PAGE ID URL PREFIX e.g. ?PID=");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.URL.Username");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.URL.Password");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.URL.Domain");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.URL.Proxy");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, Empty);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);
                //xmlWriter.WriteWhitespace(newline);
                //xmlWriter.WriteWhitespace(newline);
                //xmlWriter.WriteWhitespace(newline);

                #endregion

                #region Site Status Loader Service specific properties

                xmlWriter.WriteComment(" ********************************************************************************************************* ");
                xmlWriter.WriteComment(" *************************** Site Status Loader Service specific properties ****************************** ");
                xmlWriter.WriteComment(" ********************************************************************************************************* ");

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceCurrent.Switch");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("true");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceHistoric.Switch");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("true");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Timer properties ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerCurrent.Interval.MilliSeconds");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("60000");
                xmlWriter.WriteEndElement();
                
                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerCurrent.StartTime.Second");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Empty value will default to now ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerCurrent.StartTime.Minute");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Empty value will default to now ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerCurrent.StartTime.Hour");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Empty value will default to now ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerCurrent.Restart.Second");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("00");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Empty value will default to 00 ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerCurrent.Restart.Minute");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("01");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Empty value will default to 00 ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerCurrent.Restart.Hour");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("00");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Empty value will default to 00 ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerCurrent.Schedule.Days");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("Mon,Tue,Wed,Thu,Fri,Sat,Sun");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" e.g. Mon,Tue ");

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerHistoric.Interval.MilliSeconds");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("86400000");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerHistoric.StartTime.Second");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("00");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Empty value will default to now ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerHistoric.StartTime.Minute");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("00");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Empty value will default to now ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerHistoric.StartTime.Hour");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("00");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Empty value will default to now ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.ServiceTimerHistoric.Schedule.Days");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("Mon,Tue,Wed,Thu,Fri,Sat,Sun");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" e.g. Mon,Tue ");

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.AlertLevelCounts.TimePeriod.Seconds");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("7200");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Locations to save the data files to (note: files are prefixed with datetime by the app ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Current.Save");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("true");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Current.Save.Directory");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("DIRECTORY PATH e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Current.Save.Filename");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("RealtimeStatus.xml");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.Save");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("true");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.Save.Directory");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("DIRECTORY PATH e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Historic.Save.Filename");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("HistoricStatus.csv");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Values to read from the xml/csv file ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.XML.Node");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("ELEMENT NODE FOR THE STATUS RECORD");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.XML.Node.Attribute.EventTypeName");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("ELEMENT ATTRIBUTE OF NAME FOR THE STATUS RECORD");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.XML.Node.Attribute.Status");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("ELEMENT ATTRIBUTE OF STATUS CODE FOR THE STATUS RECORD");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.XML.Node.Attribute.Duration");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("ELEMENT ATTRIBUTE OF DURATION FOR THE STATUS RECORD");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.XML.Node.Attribute.TimeSubmitted");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("ELEMENT ATTRIBUTE OF TIME SUBMITTED FOR THE STATUS RECORD");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.XML.DateTimeFormat");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("ddd MMM d HH:mm:ss  yyyy");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" UTC value replaced with \"\" ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.UTCValue");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("UTC");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" UTC value to be removed from datetime string ");

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.CSV.Column.EventTypeName");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("COLUMN NUMBER OF NAME FOR THE STATUS RECORD");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.CSV.Column.Status");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("COLUMN NUMBER OF STATUS CODE FOR THE STATUS RECORD");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.CSV.Column.Duration");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("COLUMN NUMBER OF DURATION FOR THE STATUS RECORD");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.CSV.Column.TimeSubmitted");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("COLUMN NUMBER OF TIME SUBMITTED FOR THE STATUS RECORD");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.CSV.DateTimeFormat");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("dd/MM/yyyy HH:mm:ss");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.CSV.HasHeaderRow");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("true");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Events being monitored and their thresholds ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.EventsToMonitor");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("Event1");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Event.Event1.Success");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("STATUS SUCCESS CODES e.g. 1,2,3");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Event.Event1.ServiceLevelAgreement");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SERVICE LEVEL AGREEMENT FLAG e.g. true");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Event.Event1.AlertThreshold.Amber");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("MILLISECONDS FOR AMBER THRESHOLD e.g. 8000");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Event.Event1.AlertThreshold.Red");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("MILLISECONDS FOR RED THRESHOLD e.g. 10000");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Event.Event1.ReferenceTransactionType");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SLA NAME LOGGED TO REPORTSTAGING DATABASE e.g. SLA01");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Message Prefixes for Events being logged ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Messages.AlertLevel.Amber.Prefix");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SS AMBER ALERT - ");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Messages.AlertLevel.Red.Prefix");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SS RED ALERT - ");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Messages.DownloadError");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SS DOWNLOAD ERROR ");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Messages.DatabaseError.Current");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SS DATABASE (CURRENT) ERROR ");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Messages.DatabaseError.Day");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SS DATABASE (DAILY) ERROR ");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Standard logging ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.File.File1.Directory");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("DIRECTORY PATH e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.File.File1.Rotation");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("10000");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.EventLog.EventLog1.Name");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("Application");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.EventLog.EventLog1.Source");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("Site Status Loader Service");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.EventLog.EventLog1.Machine");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString(".");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.TraceLevel");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("Verbose");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.Verbose.Publishers");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.Info.Publishers");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.Warning.Publishers");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.Error.Publishers");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Custom logging ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.Custom");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SSDB");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.Custom.SSDB.Name");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SSCustomEventPublisher");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Custom");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("REFERENCETRANSACTION");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Custom.Trace");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("On");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Custom.REFERENCETRANSACTION.Assembly");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("ao.eventlogging");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Custom.REFERENCETRANSACTION.Name");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("ReferenceTransactionEvent");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Custom.REFERENCETRANSACTION.Publishers");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("SSDB");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Custom.REFERENCETRANSACTION.Trace");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderService);
                xmlWriter.WriteString("On");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);
                //xmlWriter.WriteWhitespace(newline);
                //xmlWriter.WriteWhitespace(newline);
                //xmlWriter.WriteWhitespace(newline);

                #endregion

                #region Site Status Monitor Application specific properties

                xmlWriter.WriteComment(" ********************************************************************************************************* ");
                xmlWriter.WriteComment(" ************************* Site Status Monitor Application specific properties *************************** ");
                xmlWriter.WriteComment(" ********************************************************************************************************* ");

                xmlWriter.WriteComment(" Service to monitor ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusLoaderService.Name");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("Site Status Loader Service");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteComment(" Must be the same as the Service's .config file ServiceName ");

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Interval time to check the service being monitored ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.ServiceMonitorTimer.Interval.MilliSeconds");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("5000");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" System tray icons ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.SystemTray.DisplayText");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("Site Status Loader Monitor");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.SystemTray.Icon.Disabled");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("DIRECTORY PATH OF ICON e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.SystemTray.Icon.Enabled");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("DIRECTORY PATH OF ICON e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.SystemTray.Icon.Paused");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("DIRECTORY PATH OF ICON e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.SystemTray.Icon.Running");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("DIRECTORY PATH OF ICON e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.SystemTray.Icon.Stopped");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("DIRECTORY PATH OF ICON e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.SystemTray.Icon.RunningAmber");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("DIRECTORY PATH OF ICON e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.SystemTray.Icon.RunningRed");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("DIRECTORY PATH OF ICON e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.SystemTray.Icon.RunningUnknown");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("DIRECTORY PATH OF ICON e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.About.ApplicationName");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("Site Status Loader Monitor Application");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "SiteStatusMonitorApplication.About.Owner");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("Atos, 2011");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Standard logging ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.File.File1.Directory");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("DIRECTORY PATH e.g. C:\\TDPortal");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.File.File1.Rotation");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("10000");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.EventLog.EventLog1.Name");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("Application");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.EventLog.EventLog1.Source");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("Site Status Loader Monitor");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.EventLog.EventLog1.Machine");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString(".");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.TraceLevel");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("Verbose");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.Verbose.Publishers");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.Info.Publishers");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.Warning.Publishers");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Operational.Error.Publishers");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("File1");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteComment(" Custom logging ");

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Publisher.Custom");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement(property);
                xmlWriter.WriteAttributeString(name, "Logging.Event.Custom.Trace");
                xmlWriter.WriteAttributeString(GID, SiteStatus);
                xmlWriter.WriteAttributeString(AID, SiteStatusLoaderMonitor);
                xmlWriter.WriteString("Off");
                xmlWriter.WriteEndElement();

                //xmlWriter.WriteWhitespace(newline);

                #endregion

                
                //xmlWriter.WriteWhitespace(newline);

                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();

                xmlWriter.Close();
            }

            return true;
        }
        
        #endregion
    }
}
