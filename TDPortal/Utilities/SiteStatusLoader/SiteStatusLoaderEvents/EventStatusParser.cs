// *********************************************** 
// NAME                 : EventStatusParser.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class used to parse the Third Party XML or CSV event status record into an EventStatus object
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderEvents/EventStatusParser.cs-arc  $
//
//   Rev 1.0   Aug 23 2011 11:04:36   mmodi
//Initial revision.
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.1   Aug 23 2011 10:35:22   mmodi
//Updates to log historic multi-step transactions download data
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.0   Apr 01 2009 13:37:12   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;

using AO.Common;
using AO.EventLogging;
using ES = AO.SiteStatusLoaderEvents.EventStatusAlertSettings;

using Logger = System.Diagnostics.Trace;
using PropertyService = AO.Properties.Properties;

namespace AO.SiteStatusLoaderEvents
{
    public class EventStatusParser
    {
        #region Private members
        private static EventStatusParser instance = null;

        private string propNode;

        private string propAttributeEventID;
        private string propAttributeEventTypeName;
        private string propAttributeStatus;
        private string propAttributeDuration;
        private string propAttributeTimeSubmitted;

        private string dateTimeFormatXML;
        private string dateTimeFormatCSV;
        private string UTC;

        private bool propCSVHasHeaderLine = false;
        private int propColumnIdEventTypeName = -1;
        private int propColumnIdStatus = -1;
        private int propColumnIdDuration = -1;
        private int propColumnIdTimeSubmitted = -1;

        private List<int> eventsToMonitor;

        #endregion

        #region Private constructor

        /// <summary>
        /// Constructor for the Status parser
        /// </summary>
        private EventStatusParser()
        {
            LoadStrings();

            LoadEventsToMonitor();
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Method for returning static status parser instance
        /// </summary>
        public static EventStatusParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventStatusParser();
                }

                return instance;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Converts an XML document to a EventStatus class.
        /// Uses values obtained from Properties to extract the correct elements/attributes to parse
        /// into an EventStatus class
        /// </summary>
        /// <param name="statusXML">Xml Document containing Event Status information</param>
        /// <returns>Array of Event Status objects containing status details</returns>
        public EventStatus[] ConvertXMLToEventStatus(XmlDocument statusXMLDoc)
        {
            // Get all the records
            XmlNodeList nodeList = statusXMLDoc.DocumentElement.SelectNodes("//" + propNode);

            // Set up the return object
            ArrayList eventStatusObjects = new ArrayList();

            try
            {
                //Loop through the XML nodes creating the status objects
                foreach (XmlNode node in nodeList)
                {
                    EventStatus eventStatus = GetEventStatusObject(node);

                    if (eventStatus != null)
                    {
                        // Only add the event if its one we're monitoring
                        if (eventsToMonitor.Contains(eventStatus.EventID))
                        {
                            eventStatusObjects.Add(eventStatus);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SSException(ex.Message, false, SSExceptionIdentifier.SSLSEventStatusParser);
            }

            return (EventStatus[])eventStatusObjects.ToArray(typeof(EventStatus));
        }

        /// <summary>
        /// Converts an CSVReader (containg a CSV file) to an EventStatus class.
        /// Uses values obtained from Properties to extract the correct elements/attributes to parse
        /// into an EventStatus class
        /// </summary>
        /// <param name="statusCSVReader">CSV file containing Event Status information</param>
        /// <param name="pid">Page ID used to download the CSV file</param>
        /// <returns>Array of Event Status objects containing status details</returns>
        public EventStatus[] ConvertCSVToEventStatus(CSVReader statusCSVReader, string pid)
        {
            // Set up the return object
            ArrayList eventStatusObjects = new ArrayList();

            bool csvColumnIdsValidated = false;

            try
            {
                // Page ID is the Event ID (could be either the Parent event ID or Child event ID
                int eventID = Convert.ToInt32(pid);

                //Loop through each CSV line creating the status objects
                string[] csvFields;

                while ((csvFields = statusCSVReader.GetCSVLine(propCSVHasHeaderLine)) != null)
                {
                    // Validate the column id ordinals on first loop through
                    if (!csvColumnIdsValidated)
                    {
                        csvColumnIdsValidated = ValidateColumnIds(csvFields);
                    }

                    if (csvColumnIdsValidated)
                    {
                        EventStatus eventStatus = GetEventStatusObject(csvFields, eventID);

                        if (eventStatus != null)
                        {
                            // Determine if the event is being monitored and add

                            // The CSV row parsed could be for a parent or child event. 
                            // If not found in the events being monitored, check if parent event 
                            // being monitored and add
                            if ((eventsToMonitor.Contains(eventStatus.EventID))
                                || (eventsToMonitor.Contains(eventStatus.EventIDParent)))
                            {
                                eventStatusObjects.Add(eventStatus);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SSException(ex.Message, false, SSExceptionIdentifier.SSLSEventStatusParser);
            }
            finally
            {
                statusCSVReader.Dispose();
            }

            return (EventStatus[])eventStatusObjects.ToArray(typeof(EventStatus));
        }

        /// <summary>
        /// Returns the events being monitored, in an int array of EventIDs
        /// </summary>
        /// <returns></returns>
        public List<int> GetEventsToMonitor()
        {
            // Assume its already been populated in the Instance constructor
            if (eventsToMonitor == null)
            {
                eventsToMonitor = new List<int>();
            }

            return eventsToMonitor;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Returns an EventStatus object, or null if exception
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private EventStatus GetEventStatusObject(XmlNode node)
        {
            // Values to read in the xml data
            int eventID = -1;
            string eventName = string.Empty;
            int eventParentID = -1; // Real time events never have a parent
            string eventParentName = string.Empty; // Real time events never have a parent
            int status = 1;
            TimeSpan duration = new TimeSpan();
            DateTime timeSubmitted = DateTime.Now;

            bool createdOK = false;

            try
            {
                #region Create EventStatus object

                #region Read XML values

                if (!string.IsNullOrEmpty(propAttributeEventID))
                {
                    eventID = Convert.ToInt32(node.Attributes[propAttributeEventID].InnerText);

                    createdOK = true;
                }

                if (!string.IsNullOrEmpty(propAttributeEventTypeName))
                {
                    eventName = node.Attributes[propAttributeEventTypeName].InnerText;

                    createdOK = true;
                }

                if (!string.IsNullOrEmpty(propAttributeStatus))
                {
                    status = Convert.ToInt32(node.Attributes[propAttributeStatus].InnerText);

                    createdOK = true;
                }

                if (!string.IsNullOrEmpty(propAttributeDuration))
                {
                    string stringDuration = node.Attributes[propAttributeDuration].InnerText;

                    duration = TimeSpanParser.GetValue(stringDuration);

                    createdOK = true;
                }

                if (!string.IsNullOrEmpty(propAttributeTimeSubmitted))
                {
                    string submitted = node.Attributes[propAttributeTimeSubmitted].InnerText;

                    // Assume there is a UTC, take it out and replace with ""
                    submitted = submitted.Replace(UTC, "");

                    timeSubmitted = DateTime.ParseExact(submitted.Trim(), dateTimeFormatXML, CultureInfo.InvariantCulture);

                    createdOK = true;
                }

                #endregion

                if (createdOK)
                {
                    EventStatus eventStatus = new EventStatus(eventID, eventName, eventParentID, eventParentName, status, duration, timeSubmitted);

                    return eventStatus;
                }
                else
                {
                    return null;
                }

                #endregion
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error, ex.Message));

                return null;
            }
        }

        /// <summary>
        /// Returns an EventStatus object, or null if exception
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private EventStatus GetEventStatusObject(string[] csvRow, int eventID)
        {
            // Values to read in the xml data
            string eventName = string.Empty;
            int eventParentID = -1;
            string eventParentName = string.Empty;
            int status = 1;
            TimeSpan duration = new TimeSpan();
            DateTime timeSubmitted = DateTime.Now;

            bool createdOK = false;

            try
            {
                #region Create EventStatus object 

                #region Read CSV values

                if (propColumnIdEventTypeName >= 0)
                {
                    eventName = csvRow[propColumnIdEventTypeName];
                    createdOK = true;
                }

                if (propColumnIdStatus >= 0)
                {
                    status = Convert.ToInt32(csvRow[propColumnIdStatus]);
                    
                    createdOK = true;
                }

                if (propColumnIdDuration >= 0)
                {
                    string stringDuration = csvRow[propColumnIdDuration];

                    duration = TimeSpan.FromMilliseconds(Convert.ToDouble(stringDuration));

                    createdOK = true;
                }

                if (propColumnIdTimeSubmitted >= 0)
                {
                    string submitted = csvRow[propColumnIdTimeSubmitted];
                                        
                    timeSubmitted = DateTime.ParseExact(submitted.Trim(), dateTimeFormatCSV, CultureInfo.InvariantCulture);

                    createdOK = true;
                }

                #endregion

                #region Check for parent

                // Find the parent event alert settings, and add if exists
                ES.EventStatusAlertSetting esas = EventStatusAlertSettings.Instance.GetEventStatusAlertSettingForChild(eventID);

                if (esas.eventID > 0)
                {
                    eventParentName = esas.eventName;
                    eventParentID = esas.eventID;
                }

                #endregion

                if (createdOK)
                {
                    EventStatus eventStatus = new EventStatus(eventID, eventName, eventParentID, eventParentName, status, duration, timeSubmitted);

                    return eventStatus;
                }
                else
                {
                    return null;
                }
                #endregion
            }
            catch (Exception ex)
            {
                StringBuilder csvRowSB = new StringBuilder();

                foreach (string item in csvRow)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        csvRowSB.Append(item);
                    }

                    csvRowSB.Append(",");
                }

                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error, 
                    string.Format(Messages.SSLSErrorParsingHistoricSiteStatusEvent, csvRowSB.ToString(), ex.Message)));

                return null;
            }
        }

        /// <summary>
        /// Validates the column Ids set up, i.e. makes sure the csvRow string[] contains the appropriate number of elements
        /// </summary>
        /// <param name="csvRow"></param>
        /// <returns></returns>
        private bool ValidateColumnIds(string[] csvRow)
        {
            bool columnIdsOK = false;

            if ((csvRow != null) && (csvRow.Length > 0))
            {
                // Only if all of the specified column Ids are smaller than the csvRow string array
                if ((propColumnIdEventTypeName < csvRow.Length)
                    &&
                    (propColumnIdDuration < csvRow.Length)
                    &&
                    (propColumnIdStatus < csvRow.Length)
                    &&
                    (propColumnIdTimeSubmitted < csvRow.Length)
                    )
                {
                    columnIdsOK = true;
                }
            }

            return columnIdsOK;
        }
        
        #region Setup methods

        /// <summary>
        /// Reads in the strings needed by object
        /// </summary>
        private void LoadStrings()
        {
            #region Load strings
            
            // XML file values
            propNode = PropertyService.Instance["SiteStatusLoaderService.XML.Node"];

            propAttributeEventID = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.EventID"];
            propAttributeEventTypeName = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.EventTypeName"];
            propAttributeStatus = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.Status"];
            propAttributeDuration = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.Duration"];
            propAttributeTimeSubmitted = PropertyService.Instance["SiteStatusLoaderService.XML.Node.Attribute.TimeSubmitted"];

            dateTimeFormatXML = PropertyService.Instance["SiteStatusLoaderService.XML.DateTimeFormat"];
            UTC = PropertyService.Instance["SiteStatusLoaderService.UTCValue"];

            // CSV file values
            propColumnIdEventTypeName = Convert.ToInt32(PropertyService.Instance["SiteStatusLoaderService.CSV.Column.EventTypeName"]);
            propColumnIdStatus = Convert.ToInt32(PropertyService.Instance["SiteStatusLoaderService.CSV.Column.Status"]);
            propColumnIdDuration = Convert.ToInt32(PropertyService.Instance["SiteStatusLoaderService.CSV.Column.Duration"]);
            propColumnIdTimeSubmitted = Convert.ToInt32(PropertyService.Instance["SiteStatusLoaderService.CSV.Column.TimeSubmitted"]);

            dateTimeFormatCSV = PropertyService.Instance["SiteStatusLoaderService.CSV.DateTimeFormat"];

            propCSVHasHeaderLine = Convert.ToBoolean(PropertyService.Instance["SiteStatusLoaderService.CSV.HasHeaderRow"]);

            #endregion
        }

        /// <summary>
        /// Loads the events which should be monitored
        /// </summary>
        private void LoadEventsToMonitor()
        {
            eventsToMonitor = new List<int>();

            try
            {            
                string events = PropertyService.Instance["SiteStatusLoaderService.EventsToMonitor"];

                string[] eventsArray = events.Split(',');

                foreach (string s in eventsArray)
                {
                    eventsToMonitor.Add(Convert.ToInt32(s));
                }
            }
            catch
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error, 
                    string.Format(Messages.SSLSErrorSettingUpEventsToMonitor, "SiteStatusLoaderService.EventsToMonitor")));

                eventsToMonitor = new List<int>();
            }
        }

        #endregion

        #endregion
    }
}
