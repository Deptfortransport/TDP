// *********************************************** 
// NAME                 : EventStatusAlertSettings.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class which contains the alert level thresholds for status events being monitored
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderEvents/EventStatusAlertSettings.cs-arc  $
//
//   Rev 1.1   Aug 25 2011 09:30:06   mmodi
//Set child alerts to success unless status code inidicates fail
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.0   Aug 23 2011 11:04:34   mmodi
//Initial revision.
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.2   Aug 23 2011 10:35:20   mmodi
//Updates to log historic multi-step transactions download data
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.1   Jun 16 2009 15:48:50   mmodi
//Added a failed flag
//Resolution for 5298: Site Status Loader - Update to log Red Alerts as successful
//
//   Rev 1.0   Apr 01 2009 13:37:12   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

using Logger = System.Diagnostics.Trace;
using PropertyService = AO.Properties.Properties;
using System.Collections;
using AO.Common;
using AO.EventLogging;

namespace AO.SiteStatusLoaderEvents
{
    public class EventStatusAlertSettings
    {
        #region Struct
        /// <summary>
        /// Struct to hold the values needed to determine the alert status for an EventStatus
        /// </summary>
        public struct EventStatusAlertSetting
        {
            public int eventID;
            public string eventName, referenceTransactionType;
            public ArrayList successValues;
            public bool serviceLevelAgreement, useThresholdAmber, useThresholdRed;
            public TimeSpan thresholdAmber, thresholdRed;
            public List<int> eventIDChildren;
            public Dictionary<int, string> eventIDChildRefTransType;

            public EventStatusAlertSetting(
                int eventID,
                string eventName, string referenceTransactionType,
                ArrayList successValues, bool serviceLevelAgreement, 
                bool useThresholdAmber, TimeSpan thresholdAmber, 
                bool useThresholdRed, TimeSpan thresholdRed,
                List<int> eventIDChildren,
                Dictionary<int, string> eventIDChildRefTransType)
            {
                this.eventID = eventID;
                this.eventName = eventName;
                this.referenceTransactionType = referenceTransactionType;
                this.successValues = successValues;
                this.serviceLevelAgreement = serviceLevelAgreement;
                this.useThresholdAmber = useThresholdAmber;
                this.thresholdAmber = thresholdAmber;
                this.useThresholdRed = useThresholdRed;
                this.thresholdRed = thresholdRed;
                this.eventIDChildren = eventIDChildren;
                this.eventIDChildRefTransType = eventIDChildRefTransType;
            }
        }
        #endregion

        #region Private members

        private static EventStatusAlertSettings instance = null;

        private Dictionary<int, EventStatusAlertSetting> eventAlertSettings;

        #endregion

        #region Private constructor

        /// <summary>
        /// Constructor for the Status parser
        /// </summary>
        private EventStatusAlertSettings()
        {
            LoadAlertSettingsForEvents();
        }

        #endregion

        #region Public Property
        
        /// <summary>
        /// Method for returning static status parser instance
        /// </summary>
        public static EventStatusAlertSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventStatusAlertSettings();
                }

                return instance;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the Alert level of the EventStatus, using Alert Settings read from properties.
        /// If the EventStatus is detected as a "child" event, then Alert level is set to unknown
        /// </summary>
        /// <param name="eventStatusArray"></param>
        /// <returns></returns>
        public EventStatus[] UpdateAlertLevelsForEvents(EventStatus[] eventStatusArray)
        {
            foreach (EventStatus eventStatus in eventStatusArray)
            {
                // If this event is being monitored, its settings will be in the dictionary.
                // And the eventStatusArray will only contain events being monitored as the 
                // EventStatusParser would have removed any not needed
                if (eventAlertSettings.ContainsKey(eventStatus.EventID))
                {
                    EventStatusAlertSetting eventSettings = eventAlertSettings[eventStatus.EventID];

                    bool failed = false; // The overall fail or pass for the event

                    AlertLevel alertLevel = GetAlertLevel(eventStatus, eventSettings, ref failed);

                    eventStatus.UpdateAlertStatus(alertLevel, failed, eventSettings.serviceLevelAgreement, eventSettings.referenceTransactionType);
                }
                // Else if a child event, then update to unknown settings because a child event is 
                // not a "complete transaction" so cannot determine if it failed
                else if (eventAlertSettings.ContainsKey(eventStatus.EventIDParent))
                {
                    EventStatusAlertSetting eventSettings = eventAlertSettings[eventStatus.EventIDParent];

                    // Default to parent-unknown, or use defined value if exists
                    string refTransType = eventSettings.referenceTransactionType + "-UNKNOWN";
                    if (eventSettings.eventIDChildRefTransType.ContainsKey(eventStatus.EventID))
                    {
                        refTransType = eventSettings.eventIDChildRefTransType[eventStatus.EventID];
                    }

                    // Check if status code is ok
                    bool failed = !eventSettings.successValues.Contains(eventStatus.Status);

                    // Assume alert level is green because this is a part transaction so unsure if failed
                    eventStatus.UpdateAlertStatus(AlertLevel.Green, failed, eventSettings.serviceLevelAgreement, refTransType);
                }
            }

            return eventStatusArray;
        }

        /// <summary>
        /// Returns the EventStatusAlertSetting for the specified eventID. 
        /// Else empty struct is returned if not found
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public EventStatusAlertSetting GetEventStatusAlertSetting(int eventID)
        {
            if (eventAlertSettings.ContainsKey(eventID))
            {
                return eventAlertSettings[eventID];
            }
            else
            {
                return new EventStatusAlertSetting();
            };
        }

        /// <summary>
        /// Returns the EventStatusAlertSetting for the specified Child EventID. 
        /// Else empty struct is returned if not found
        /// </summary>
        /// <param name="eventIDChild"></param>
        /// <returns></returns>
        public EventStatusAlertSetting GetEventStatusAlertSettingForChild(int eventIDChild)
        {
            // Loop through all events until child id match found
            foreach (EventStatusAlertSetting esas in eventAlertSettings.Values)
            {
                if (esas.eventIDChildren.Contains(eventIDChild))
                {
                    return esas;
                }
            }

            return new EventStatusAlertSetting();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the event status alert levels from the properties, first validating if properties exist
        /// </summary>
        private void LoadAlertSettingsForEvents()
        {
            eventAlertSettings = new Dictionary<int, EventStatusAlertSetting>();

            // Get the events we're interested in
            List<int> eventsToMonitor = EventStatusParser.Instance.GetEventsToMonitor();

            if (eventsToMonitor != null)
            {
                try
                {
                    EventStatusPropertyValidator propertyValidator = new EventStatusPropertyValidator(PropertyService.Instance);
                    ArrayList errors = new ArrayList();

                    // Validate Properties exist for each Event we're monitoring
                    foreach (int eventToMonitor in eventsToMonitor)
                    {
                        propertyValidator.ValidateProperty(string.Format(Keys.EventID, eventToMonitor), errors);
                        propertyValidator.ValidateProperty(string.Format(Keys.EventName, eventToMonitor), errors);
                        propertyValidator.ValidateProperty(string.Format(Keys.EventSuccess, eventToMonitor), errors);
                        propertyValidator.ValidateProperty(string.Format(Keys.EventReferenceTransactionType, eventToMonitor), errors);
                        propertyValidator.ValidateProperty(string.Format(Keys.EventServiceLevelAgreememt, eventToMonitor), errors);
                        propertyValidator.ValidateProperty(string.Format(Keys.EventAlertThresholdAmber, eventToMonitor), errors);
                        propertyValidator.ValidateProperty(string.Format(Keys.EventAlertThresholdRed, eventToMonitor), errors);

                        // If there are no errors, create the EventStatusAlertSetting and add to the dictionary
                        if (errors.Count == 0)
                        {
                            eventAlertSettings.Add(eventToMonitor, CreateEventStatusAlert(eventToMonitor));
                        }
                        else
                        {
                            LogError(eventToMonitor, errors);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new SSException(Messages.SSLSErrorSettingUpAlertLevelsForEvents 
                        + " Exception: " 
                        + ex.Message, 
                        false, SSExceptionIdentifier.SSLSEventAlertLevelSettings);
                }
            }
        }

        /// <summary>
        /// Creates an EventStatusAlertSetting using values from properties for the supplied event to monitor.
        /// Assumes properties have already been validated.
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        private EventStatusAlertSetting CreateEventStatusAlert(int eventToMonitor)
        {
            // Set up the ID
            int eventID = -1;
            string eventIDValue = PropertyService.Instance[string.Format(Keys.EventID, eventToMonitor)];
            if (!string.IsNullOrEmpty(eventIDValue))
            {
                eventID = Convert.ToInt32(eventIDValue);
            }

            string eventName = PropertyService.Instance[string.Format(Keys.EventName, eventToMonitor)];

            #region Success codes

            // Whats defined as a Success, will be a number of result codes (int)
            string eventSuccessValue = PropertyService.Instance[string.Format(Keys.EventSuccess, eventToMonitor)];

            string[] tempSuccessValues = eventSuccessValue.Split(',');

            ArrayList successValues = new ArrayList();

            foreach (string s in tempSuccessValues)
            {
                successValues.Add(Convert.ToInt32(s));
            }

            #endregion

            // The reference transaction type, used for logging to reporting database
            string eventReferenceTransactionType = PropertyService.Instance[string.Format(Keys.EventReferenceTransactionType, eventToMonitor)];

            // Whether this is an SLA Transaction
            bool eventServiceLevelAgreement = bool.Parse(PropertyService.Instance[string.Format(Keys.EventServiceLevelAgreememt, eventToMonitor)]);

            #region Thresholds

            // Defined Red alert time
            bool useRedAlert = true;
            TimeSpan eventAlertThresholdRed = new TimeSpan();
            string redAlertTime = PropertyService.Instance[string.Format(Keys.EventAlertThresholdRed, eventToMonitor)];
            if (!string.IsNullOrEmpty(redAlertTime))
            {
                eventAlertThresholdRed = TimeSpan.FromMilliseconds(Convert.ToDouble(redAlertTime));
            }
            else
            {   // Prevents validation using the Red alert
                useRedAlert = false;
            }

            // Defind Amber alert time
            bool useAmberAlert = true;
            TimeSpan eventAlertThresholdAmber = new TimeSpan();
            string amberAlertTime = PropertyService.Instance[string.Format(Keys.EventAlertThresholdAmber, eventToMonitor)];
            if (!string.IsNullOrEmpty(amberAlertTime))
            {
                eventAlertThresholdAmber = TimeSpan.FromMilliseconds(Convert.ToDouble(amberAlertTime));
            }
            else
            {
                // Prevents validation using the Amber alert
                useAmberAlert = false;
            }

            #endregion

            #region Child IDs

            // Setup Child page IDs, will be comma seperated list
            List<int> eventIDChildren = new List<int>();
            Dictionary<int, string> eventIDChildRefTransType = new Dictionary<int, string>();

            string eventIDChildValue = PropertyService.Instance[string.Format(Keys.EventIDChild, eventToMonitor)];

            // Only populate if child page IDs defined
            if (!string.IsNullOrEmpty(eventIDChildValue))
            {
                string[] tmpIDChildValues = eventIDChildValue.Split(',');

                foreach (string s in tmpIDChildValues)
                {
                    eventIDChildren.Add(Convert.ToInt32(s));
                }

                // Get Child PID reference transaction type values, used for logging to reporting database
                foreach (int childID in eventIDChildren)
                {
                    eventIDChildRefTransType.Add(childID, PropertyService.Instance[string.Format(Keys.EventIDChildRefTransType, eventToMonitor, childID)]);
                }
            }
            #endregion

            // Create the settings object
            EventStatusAlertSetting eventStatusAlert = new EventStatusAlertSetting(
                eventID,
                eventName, 
                eventReferenceTransactionType,
                successValues, 
                eventServiceLevelAgreement, 
                useAmberAlert, eventAlertThresholdAmber, 
                useRedAlert, eventAlertThresholdRed,
                eventIDChildren,
                eventIDChildRefTransType);

            return eventStatusAlert;
        }

        /// <summary>
        /// Returns the AlertLevel based on the the EventStatus duration time, and the settings defined in the EventStatusAlertSetting
        /// </summary>
        private AlertLevel GetAlertLevel(EventStatus eventStatus, EventStatusAlertSetting alertSetting, ref bool failed)
        {
            AlertLevel alertLevel = AlertLevel.Green;
            failed = false;

            if (!alertSetting.successValues.Contains(eventStatus.Status))
            {
                alertLevel = AlertLevel.Red;
                
                // This event was an actual failure, set flag. This ensures it is logged to the database as 
                // a failed transaction
                failed = true;
            }
            else
            {
                if ((alertSetting.useThresholdRed) && (eventStatus.Duration > alertSetting.thresholdRed))
                {
                    alertLevel = AlertLevel.Red;
                }
                else if ((alertSetting.useThresholdAmber) && (eventStatus.Duration > alertSetting.thresholdAmber))
                {
                    alertLevel = AlertLevel.Amber;
                }
            }

            return alertLevel;
        }

        /// <summary>
        /// Logs an OperationalEvent for the event to monitor supplied using the errors array
        /// </summary>
        /// <param name="errors"></param>
        private void LogError(int eventToMonitor, ArrayList errors)
        {
            StringBuilder message = new StringBuilder();
            message.Append(string.Format(Messages.SSLSMissingPropertyForEventToMonitor, eventToMonitor.ToString()));

            message.Append("\nErrors: ");

            foreach (string error in errors)
            {
                message.Append("\n");
                message.Append(error);
            }

            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error, message.ToString()));
        }

        #endregion
    }
}
