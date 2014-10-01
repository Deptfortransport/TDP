// *********************************************** 
// NAME                 : SiteStatusLoaderCurrent.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class which performs the real time Status monitoring. This class is initiated on a new thread by the caller
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderService/SiteStatusLoaderCurrent.cs-arc  $
//
//   Rev 1.3   Aug 23 2011 11:01:34   mmodi
//Moved EventStatus code into a seperate DLL to allow the monitor tool to be enhanced with a manual import feature
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.2   Aug 23 2011 10:35:26   mmodi
//Updates to log historic multi-step transactions download data
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.1   Apr 06 2009 16:10:08   mmodi
//Updated to keep count of alerts raised, and amended to pass save file parameters to the HttpAccess class
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:37:16   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Timers;
using System.Xml;

using AO.Common;
using AO.EventLogging;
using AO.SiteStatusLoaderEvents;

using PropertyService = AO.Properties.Properties;
using Logger = System.Diagnostics.Trace;

namespace AO.SiteStatusLoaderService
{
    public class SiteStatusLoaderCurrent : IDisposable
    {
        #region Structs

        private struct EventStatusKey
        {
            public DateTime timeSubmitted;
            public string eventName;

            public EventStatusKey(string eventName, DateTime timeSubmitted)
            {
                this.eventName = eventName;
                this.timeSubmitted = timeSubmitted;
            }
        }

        #endregion

        private Dictionary<EventStatusKey, AlertLevel> alertLevelsHistory = new Dictionary<EventStatusKey, AlertLevel>();
        private DateTime alertLevelsHistoryLastUpdated;
        
        #region Private members

        static private string staticServiceName;
        static private string threadName = "Current";

        private string serviceName = string.Empty;
        private Timer serviceTimerCurrentData; //Timer used for the current/real time site status data
        private bool disposed = false;

        private Dictionary<int, DateTime> loggedEventStatus; //Used to prevent logging an EventStatus twice
        
        private Dictionary<DayOfWeek, bool> scheduleCurrentDayOfWeek; // Used to track which days to run on (current/real time timer)

        private DateTime scheduleCurrentRestartDateTime; // Used to restart the timer at a specific time (current/real time timer)

        #endregion

        #region Static methods

        /// <summary>
        /// Method to initiate a new instance of the Site Status Loader Current
        /// </summary>
        public static void Run()
        {
            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                string.Format(Messages.SSLSLoaderServiceStarting, threadName)));

            try
            {
                SiteStatusLoaderCurrent ssLoader = new SiteStatusLoaderCurrent(staticServiceName);
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
        public SiteStatusLoaderCurrent(string serviceName)
		{
			this.serviceName = serviceName;
        }

        #endregion

        #region Service events - Start, Stop, Pause, Continue

        /// <summary>
        /// Start checking site status
        /// </summary>	
        public void Start()
        {
            // Wait until a configured start time before starting
            WaitBeforeStarting();

            // Read the schedule days for the current/real time timer
            SetupCurrentTimerScheduleDays();

            // Get the restart time for the current/real time timer
            SetupCurrentTimerRestartTime();

            // Start the timer for the current/real time data
            SetupServiceTimerCurrent();
        }

        /// <summary>
        /// Pause checking site status
        /// </summary>
        public void Pause()
        {
            if (serviceTimerCurrentData != null)
            {
                if (serviceTimerCurrentData.Enabled)
                {
                    serviceTimerCurrentData.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Continue checking site status
        /// </summary>
        public void Continue()
        {
            if (serviceTimerCurrentData != null)
            {
                if (!serviceTimerCurrentData.Enabled)
                {
                    serviceTimerCurrentData.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Stop checking site status
        /// </summary>
        public void Stop()
        {
            if (serviceTimerCurrentData != null)
            {
                if (serviceTimerCurrentData.Enabled)
                {
                    serviceTimerCurrentData.Enabled = false;
                    serviceTimerCurrentData.Stop();
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event called when the Current/Real time timer interval occurs
        /// </summary>
        private void serviceTimerCurrentData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                // Only process if this is allowed to run on current day
                DayOfWeek today = DateTime.Now.DayOfWeek;
                
                if (scheduleCurrentDayOfWeek[today])
                {
                    ProcessSiteStatusData();
                }

                // Log the service is still running
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                    string.Format(Messages.SSLSServiceRunning, serviceName)));
            }
            catch (SSException ssEx)
            {
                // Log the service is still running, and an error occurred
                string message = string.Format(Messages.SSLSServiceRunning, serviceName);
                message += "Error: " + ssEx.Message;

                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error, message));
            }

            // Check if we need to reset and restart the timer
            if (DateTime.Now >= scheduleCurrentRestartDateTime)
            {
                RecycleServiceTimer();
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
                    if (serviceTimerCurrentData != null)
                    {
                        serviceTimerCurrentData.Dispose();
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
        ~SiteStatusLoaderCurrent()      
		{
			Dispose(false);
        }

        #endregion

        #region Private methods

        #region Setup

        #region Current/Real time timer
        /// <summary>
        /// Waits until the configure hour,minute,second before continuing
        /// </summary>
        private void WaitBeforeStarting()
        {
            #region Wait before starting

            try
            {
                // Read in the start time
                string second = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerCurrent.StartTime.Second"];
                string minute = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerCurrent.StartTime.Minute"];
                string hour = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerCurrent.StartTime.Hour"];

                second = (string.IsNullOrEmpty(second)) ? "-1" : second;
                minute = (string.IsNullOrEmpty(minute)) ? "-1" : minute;
                hour = (string.IsNullOrEmpty(hour)) ? "-1" : hour;

                int startSecond = Convert.ToInt32(second, CultureInfo.InvariantCulture);
                int startMinute = Convert.ToInt32(minute, CultureInfo.InvariantCulture);
                int startHour = Convert.ToInt32(hour, CultureInfo.InvariantCulture);

                bool startTimeLogged = false;

                while (((DateTime.Now.Second != startSecond) && (startSecond != -1))
                        ||
                        ((DateTime.Now.Minute != startMinute) && (startMinute != -1))
                        ||
                        ((DateTime.Now.Hour != startHour) && (startHour != -1))
                      )
                {
                    System.Threading.Thread.Sleep(50);

                    // Log when the service will actually start
                    if (!startTimeLogged)
                    {
                        string s = (startSecond >= 0) ? second : "--";
                        string m = (startMinute >= 0) ? minute : "--";
                        string h = (startHour >= 0) ? hour : "--";

                        Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                            string.Format(Messages.SSLSLoaderServiceStartsAt, threadName, h, m, s)));

                        startTimeLogged = true;
                    }
                }
            }
            catch
            {
                // If any errors occur here, then log warning and carry on
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning, 
                    string.Format(Messages.SSLSLoaderServiceStartsTimeSetupFailed, threadName)));
            }

            #endregion
        }

        /// <summary>
        /// Sets up the Schedule days to run on
        /// </summary>
        private void SetupCurrentTimerScheduleDays()
        {
            #region Setup days to run the current/real time service

            // Initialise the scheduleDays to set to run on no days
            scheduleCurrentDayOfWeek = new Dictionary<DayOfWeek, bool>(7);

            scheduleCurrentDayOfWeek.Add(DayOfWeek.Monday, false);
            scheduleCurrentDayOfWeek.Add(DayOfWeek.Tuesday, false);
            scheduleCurrentDayOfWeek.Add(DayOfWeek.Wednesday, false);
            scheduleCurrentDayOfWeek.Add(DayOfWeek.Thursday, false);
            scheduleCurrentDayOfWeek.Add(DayOfWeek.Friday, false);
            scheduleCurrentDayOfWeek.Add(DayOfWeek.Saturday, false);
            scheduleCurrentDayOfWeek.Add(DayOfWeek.Sunday, false);


            // Read in the days to run on
            string[] daysArray = null;

            try
            {
                string daysToRun = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerCurrent.Schedule.Days"];

                daysArray = daysToRun.Split(',');
            }
            catch
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                    string.Format(Messages.SSLSErrorParsingScheduleDays, "SiteStatusLoaderService.ServiceTimerCurrent.Schedule.Days")));

                daysArray = new string[7] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            }

            // Update the schedule days to run flags
            foreach (string day in daysArray)
            {
                DayOfWeek dayOfWeek = DayOfWeek.Monday;

                switch (day.Trim().ToUpper())
                {
                    case "MON":
                        dayOfWeek = DayOfWeek.Monday;
                        break;
                    case "TUE":
                        dayOfWeek = DayOfWeek.Tuesday;
                        break;
                    case "WED":
                        dayOfWeek = DayOfWeek.Wednesday;
                        break;
                    case "THU":
                        dayOfWeek = DayOfWeek.Thursday;
                        break;
                    case "FRI":
                        dayOfWeek = DayOfWeek.Friday;
                        break;
                    case "SAT":
                        dayOfWeek = DayOfWeek.Saturday;
                        break;
                    case "SUN":
                        dayOfWeek = DayOfWeek.Sunday;
                        break;
                    default:
                        continue;
                }

                // Update schedule
                scheduleCurrentDayOfWeek[dayOfWeek] = true;
            }
            #endregion
        }

        /// <summary>
        /// Sets up the time the service timer current is restarted
        /// </summary>
        private void SetupCurrentTimerRestartTime()
        {
            #region Set up restart time for the current/real time timer

            DateTime tomorrow = DateTime.Now.AddDays(1);

            try
            {
                string second = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerCurrent.Restart.Second"];
                string minute = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerCurrent.Restart.Minute"];
                string hour = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerCurrent.Restart.Hour"];

                second = (string.IsNullOrEmpty(second)) ? "00" : second;
                minute = (string.IsNullOrEmpty(minute)) ? "00" : minute;
                hour = (string.IsNullOrEmpty(hour)) ? "00" : hour;


                int restartSecond = Convert.ToInt32(second, CultureInfo.InvariantCulture);
                int restartMinute = Convert.ToInt32(minute, CultureInfo.InvariantCulture);
                int restartHour = Convert.ToInt32(hour, CultureInfo.InvariantCulture);

                scheduleCurrentRestartDateTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, restartHour, restartMinute, restartSecond);
            }
            catch
            {
                // If any errors occur here, then log warning and carry on
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning, 
                    string.Format(Messages.SSLSLoaderServiceRestartTimeSetupFailed, threadName)));

                scheduleCurrentRestartDateTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 0, 0, 0);
            }

            #endregion
        }
        
        /// <summary>
        /// Sets up and enables the timer used for checking the site status every X milliseconds
        /// </summary>
        private void SetupServiceTimerCurrent()
        {
            #region Set up and start current/reat time timer

            int timerInterval = 60000; // default - 1 minute

            try
            {
                timerInterval = Convert.ToInt32(PropertyService.Instance["SiteStatusLoaderService.ServiceTimerCurrent.Interval.MilliSeconds"]);
            }
            catch
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                    string.Format(Messages.SSLSServiceTimerIntervalMissing, timerInterval)));
            }

            serviceTimerCurrentData = new Timer();

            serviceTimerCurrentData.BeginInit();

            serviceTimerCurrentData.Interval = timerInterval;
            serviceTimerCurrentData.Elapsed += new System.Timers.ElapsedEventHandler(this.serviceTimerCurrentData_Elapsed);

            serviceTimerCurrentData.EndInit();

            serviceTimerCurrentData.Enabled = true;
            serviceTimerCurrentData.Start();

            #endregion
        }

        #endregion

        /// <summary>
        /// Removes the current timer, waits for the configured time, then starts a new timer
        /// </summary>
        private void RecycleServiceTimer()
        {
            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info, 
                string.Format(Messages.SSLSServiceTimerIsBeingRestarted, threadName)));

            // Remove the current timer
            serviceTimerCurrentData.Enabled = false;
            serviceTimerCurrentData.Stop();
            serviceTimerCurrentData.Dispose();

            // And initialise a new timer
            WaitBeforeStarting();

            SetupCurrentTimerRestartTime();

            SetupServiceTimerCurrent();
            
            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info, 
                string.Format(Messages.SSLSServiceTimerHasRestarted, threadName)));
        }

        #endregion

        #region Process

        /// <summary>
        /// Method to get the site status data, process it, and call method to raise alerts
        /// </summary>
        private void ProcessSiteStatusData()
        {
            EventStatus[] eventStatusArray = null;

            // Set up the dictionary to store the last logged EventStatus for each event
            if (loggedEventStatus == null)
            {
                loggedEventStatus = new Dictionary<int, DateTime>();
            }

            #region Set up data location details

            // Read the location details of the data
            string url = PropertyService.Instance["SiteStatusLoaderService.Current.URL"];
            string username = PropertyService.Instance["SiteStatusLoaderService.Current.URL.Username"];
            string password = PropertyService.Instance["SiteStatusLoaderService.Current.URL.Password"];
            string domain = PropertyService.Instance["SiteStatusLoaderService.Current.URL.Domain"];
            string proxy = PropertyService.Instance["SiteStatusLoaderService.Current.URL.Proxy"];

            if (string.IsNullOrEmpty(url))
            {
                throw new SSException(
                    string.Format(Messages.SSLSMissingSiteStatusURL, "SiteStatusLoaderService.Current.URL"),
                    false, SSExceptionIdentifier.SSLSMissingURL);
            }

            // Determine if the data should be saved
            bool saveFile = false;
            bool saveFileAll = false;
            string saveFileDirectory = string.Empty;
            string saveFileName = string.Empty;

            GetSaveStatusFileDetails(ref saveFile, ref saveFileAll, ref saveFileDirectory, ref saveFileName);

            // If save all files switched on, then retain all real time status files by appending a DateTime 
            if (saveFileAll)
            {
                saveFileName = DateTime.Now.ToString("yyyyMMddHHmmss-") + saveFileName;
            }
            
            #endregion

            #region Get status data

            // Try getting the site status data
            try
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose, 
                    string.Format(Messages.SSLSGettingSiteStatusData, threadName)));

                // Get the status data
                XmlDocument siteStatusXmlDoc = HttpAccess.Instance.GetXMLFromHttp(
                    url, username, password, domain, proxy);

                
                
                // Save the status data to a file
                HttpAccess.Instance.SaveXMLDocument(siteStatusXmlDoc, saveFile, saveFileDirectory, saveFileName);

                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                        string.Format(Messages.SSLSSiteStatusDataSavedToFile, saveFileDirectory + saveFileName)));


                
                // Convert into useable EventStatus objects
                eventStatusArray = EventStatusParser.Instance.ConvertXMLToEventStatus(siteStatusXmlDoc);

                
                
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSLSUpdatingSiteStatusAlertLevel, threadName)));

                // Update the Alert level status of each EventStatus objects
                eventStatusArray = EventStatusAlertSettings.Instance.UpdateAlertLevelsForEvents(eventStatusArray);

            }
            catch (SSException ssEx)
            {
                SSException updatedSSEx;
                string message = string.Empty;

                // Prefix the download error if needed
                if (ssEx.Identifier == SSExceptionIdentifier.SSLSErrorRetrievingDataFromURL)
                {
                    string messageDownloadErrorPrefix = PropertyService.Instance["SiteStatusLoaderService.Messages.DownloadError"];

                    message = string.Format(Messages.SSLSDownloadError, messageDownloadErrorPrefix) + ssEx.Message;

                    updatedSSEx = new SSException(message, ssEx.Logged, ssEx.Identifier);
                }
                else
                {
                    updatedSSEx = ssEx;
                }

                // Update the status file with error, and rethrow error
                EventStatusFileOutput.Instance.UpdateStatusFile(updatedSSEx);

                throw updatedSSEx;
            }

            #endregion

            #region Log status data

            // Try logging the event
            try
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSLSLoggingSiteStatusEvents, threadName)));

                // Log the EventStatus and any alerts
                LogSiteStatusEvents(eventStatusArray);
            }
            catch (SSException ssEx)
            {
                // If an error occurs, update the status file, and rethrow error
                EventStatusFileOutput.Instance.UpdateStatusFile(ssEx);

                throw ssEx;
            }

            #endregion

            #region Update the status application file

            UpdateAlertLevelCount(eventStatusArray);

            // Update the status file with the EventStatus states
            EventStatusFileOutput.Instance.UpdateStatusFile(eventStatusArray, GetAlertLevelCounts(), alertLevelsHistoryLastUpdated);

            #endregion
        }

        /// <summary>
        /// Method which logs alerts for the Site status events
        /// </summary>
        private void LogSiteStatusEvents(EventStatus[] eventStatusArray)
        {
            string messageAlertAmberPrefix = PropertyService.Instance["SiteStatusLoaderService.Messages.AlertLevel.Amber.Prefix"];
            string messageAlertRedPrefix = PropertyService.Instance["SiteStatusLoaderService.Messages.AlertLevel.Red.Prefix"];

            foreach (EventStatus eventStatus in eventStatusArray)
            {                
                // Only log this event if is it hasnt been already
                if ((!loggedEventStatus.ContainsKey(eventStatus.EventID)) ||
                    (loggedEventStatus[eventStatus.EventID] < eventStatus.TimeSubmitted))
                {
                    // For tracing purposes, log a low level event indicating logging
                    Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                        string.Format(Messages.SSLSLoggingStatusEvent, eventStatus.ToString())
                        ));

                    #region Raise alert if required
                    EventStatusAlertSettings.EventStatusAlertSetting alertSetting = EventStatusAlertSettings.Instance.GetEventStatusAlertSetting(eventStatus.EventID);

                    StringBuilder message = new StringBuilder();

                    switch (eventStatus.AlertLevel)
                    {
                        case AlertLevel.Red:
                            message.Append(string.Format(Messages.SSLSAlertRed, messageAlertRedPrefix, eventStatus.EventName));
                            message.Append(string.Format(Messages.SSLSAlertMessageStatus, eventStatus.Status));
                            message.Append(string.Format(Messages.SSLSAlertMessageTimeThreshold,
                                Convert.ToString(alertSetting.thresholdRed.TotalMilliseconds),
                                Convert.ToString(eventStatus.Duration.TotalMilliseconds)));
                            message.Append(string.Format(Messages.SSLSEventStatusDetails, eventStatus.ToString()));


                            Logger.Write(new OperationalEvent(SSEventCategory.Business, SSTraceLevel.Error,
                                message.ToString()));
                            break;

                        case AlertLevel.Amber:
                            message.Append(string.Format(Messages.SSLSAlertAmber, messageAlertAmberPrefix, eventStatus.EventName));
                            message.Append(string.Format(Messages.SSLSAlertMessageStatus, eventStatus.Status));
                            message.Append(string.Format(Messages.SSLSAlertMessageTimeThreshold,
                                Convert.ToString(alertSetting.thresholdAmber.TotalMilliseconds),
                                Convert.ToString(eventStatus.Duration.TotalMilliseconds)));
                            message.Append(string.Format(Messages.SSLSEventStatusDetails, eventStatus.ToString()));

                            Logger.Write(new OperationalEvent(SSEventCategory.Business, SSTraceLevel.Error,
                                message.ToString()));

                            break;
                        case AlertLevel.Green:
                            // No need to log any alert
                            break;
                        default:
                            // No action
                            break;
                    }

                    #endregion

                    // Log a ReferenceTransaction event to the database
                    ReferenceTransactionEvent rte = new ReferenceTransactionEvent(
                        eventStatus.ReferenceTransactionType,
                        eventStatus.SlaTransaction,
                        eventStatus.TimeSubmitted,
                        eventStatus.TimeCompleted,
                        serviceName,
                        eventStatus.Successful,
                        false);

                    Logger.Write(rte);

                }
                else
                {
                    // Already logged, for tracing purposes, log a low level event indicating not logging
                    Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                        string.Format(Messages.SSLSNotLoggingStatusEvent, eventStatus.ToString())
                        ));
                }

                #region Keep track of this event

                // Event previously added, so remove and replace with the latest event of this EventName
                if (loggedEventStatus.ContainsKey(eventStatus.EventID))
                {
                    loggedEventStatus.Remove(eventStatus.EventID);

                }

                // Add this event to our logged events dictionary
                if (!loggedEventStatus.ContainsKey(eventStatus.EventID))
                {
                    loggedEventStatus.Add(eventStatus.EventID, eventStatus.TimeSubmitted);
                }

                #endregion
            }
        }

        /// <summary>
        /// Sets up the values required for saving the status data file
        /// </summary>
        private void GetSaveStatusFileDetails(ref bool saveFile, ref bool saveFileAll, ref string fileDirectory, ref string fileName)
        {
            // Initialise
            saveFile = false;
            saveFileAll = false;
            fileDirectory = string.Empty;
            fileName = string.Empty;


            // Set save file flag
            try
            {
                saveFile = Convert.ToBoolean(PropertyService.Instance["SiteStatusLoaderService.Current.Save"]);
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                        string.Format(Messages.SSLSErrorParsingSaveToFileFlag, ex.Message)));
            }

            try
            {
                saveFileAll = Convert.ToBoolean(PropertyService.Instance["SiteStatusLoaderService.Current.Save.All"]);
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                        string.Format(Messages.SSLSErrorParsingSaveToFileFlag, ex.Message)));
            }

            // Get the file directory and name
            fileDirectory = PropertyService.Instance["SiteStatusLoaderService.Current.Save.Directory"];
            fileName = PropertyService.Instance["SiteStatusLoaderService.Current.Save.Filename"];
        }


        #endregion

        #region Alert count methods

        /// <summary>
        ///  Method which calls the update alert level history methods, and updates the alert count labels
        /// </summary>
        /// <param name="eventStatusList"></param>
        private void UpdateAlertLevelCount(EventStatus[] eventStatusArray)
        {
            try
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                                Messages.SSLSUpdateAlertLevelHistory));

                // Add the latest alert levels to the history array
                UpdateAlertLevelHistory(eventStatusArray);

                // Remove alert levels which are passed the period we're interested in
                RemoveAlertLevelHistoryRecords();
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                                string.Format(Messages.SSLSUpdateAlertLevelHistoryError, ex.Message)));
            }
        }

        /// <summary>
        /// Method which keeps track of the Alert levels read over a period of time
        /// </summary>
        /// <param name="eventStatusList">The alert records to update the counts with</param>
        /// <param name="dateTimeLastUpdated"></param>
        private void UpdateAlertLevelHistory(EventStatus[] eventStatusArray)
        {
            // Update the historic count of alerts
            if ((eventStatusArray != null) && (eventStatusArray.Length > 0))
            {
                // Go through each record and update the history array
                foreach (EventStatus es in eventStatusArray)
                {
                    EventStatusKey key = new EventStatusKey(es.EventName, es.TimeSubmitted);

                    if (!alertLevelsHistory.ContainsKey(key))
                    {
                        alertLevelsHistory.Add(key, es.AlertLevel);
                    }
                }
            }
        }

        /// <summary>
        /// Method which removes alert level records when they are older then a configured time period
        /// </summary>
        private void RemoveAlertLevelHistoryRecords()
        {
            // Set up the datetime and period to compare against
            DateTime currentTime = DateTime.UtcNow;

            int showPeriodSeconds = Convert.ToInt32(PropertyService.Instance["SiteStatusLoaderService.AlertLevelCounts.TimePeriod.Seconds"]);

            DateTime compareTime = currentTime.Subtract(TimeSpan.FromSeconds(showPeriodSeconds));

            Dictionary<EventStatusKey, AlertLevel> tempAlertLevelsHistory = new Dictionary<EventStatusKey, AlertLevel>();

            // If the alert is still in the valid period keep, otherwise discard
            foreach (KeyValuePair<EventStatusKey, AlertLevel> kvp in alertLevelsHistory)
            {
                if (kvp.Key.timeSubmitted >= compareTime)
                {
                    tempAlertLevelsHistory.Add(kvp.Key, kvp.Value);
                }
            }

            // Reassign the temp array to the current array
            alertLevelsHistory = tempAlertLevelsHistory;

            // Update the last updated datetime
            alertLevelsHistoryLastUpdated = compareTime;
        }

        /// <summary>
        /// Returns a dictionary with a count of each alert level
        /// </summary>
        /// <param name="alertLevel"></param>
        /// <returns></returns>
        private Dictionary<AlertLevel, int> GetAlertLevelCounts()
        {
            Dictionary<AlertLevel, int> alertLevelCounts = new Dictionary<AlertLevel, int>();

            // Get count for each alert level
            alertLevelCounts.Add(AlertLevel.Green, GetAlertLevelCount(AlertLevel.Green));
            alertLevelCounts.Add(AlertLevel.Amber, GetAlertLevelCount(AlertLevel.Amber));
            alertLevelCounts.Add(AlertLevel.Red, GetAlertLevelCount(AlertLevel.Red));

            return alertLevelCounts;
        }

        /// <summary>
        /// Reads the alert levels history and returns a count for the specified alert level
        /// </summary>
        /// <param name="alertLevel"></param>
        /// <returns></returns>
        private int GetAlertLevelCount(AlertLevel alertLevel)
        {
            int count = 0;

            foreach (KeyValuePair<EventStatusKey, AlertLevel> kvp in alertLevelsHistory)
            {
                if (kvp.Value == alertLevel)
                {
                    count++;
                }
            }

            return count;
        }

        #endregion

        #endregion
    }
}
