// *********************************************** 
// NAME                 : SiteStatusLoaderHistoric.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class which performs the historic Status monitoring. This class is initiated on a new thread by the caller
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderService/SiteStatusLoaderHistoric.cs-arc  $
//
//   Rev 1.3   Aug 23 2011 11:01:34   mmodi
//Moved EventStatus code into a seperate DLL to allow the monitor tool to be enhanced with a manual import feature
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.2   Aug 23 2011 10:35:28   mmodi
//Updates to log historic multi-step transactions download data
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.1   Apr 06 2009 16:10:56   mmodi
//Updated to pass in the save file location details to the HttpAccess class
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:37:16   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
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
    public class SiteStatusLoaderHistoric : IDisposable
    {
        #region Private members

        static private string staticServiceName;
        static private string threadName = "Historic";

        private string serviceName = string.Empty;
        private Timer serviceTimerHistoricData; //Timer used for the hisoric/daily site status data
        private bool disposed = false;

        //private Dictionary<string, DateTime> loggedEventStatus; //Used to prevent logging an EventStatus twice

        private Dictionary<DayOfWeek, bool> scheduleHistoricDayOfWeek; // Used to track which days to run on (historic/daily timer)

        private DateTime scheduleHistoricRestartDateTime; // Used to restart the timer at a specific time (historic/daily timer)

        #endregion

        #region Static methods

        /// <summary>
        /// Method to initiate a new instance of the Site Status Loader Historic
        /// </summary>
        public static void Run()
        {
            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose, 
                string.Format(Messages.SSLSLoaderServiceStarting, threadName)));

            try
            {
                SiteStatusLoaderHistoric ssLoader = new SiteStatusLoaderHistoric(staticServiceName);
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
        /// Method to set up the static parameters needed when initiating an instance of the Site Status Loader Historic using Run()
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
        public SiteStatusLoaderHistoric(string serviceName)
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

            // Read the schedule days for the historic/daily timer
            SetupHistoricTimerScheduleDays();

            // Get the restart time for the historic/daily timer
            SetupHistoricTimerRestartTime();

            // Start the timer for the historic/daily timer
            SetupServiceTimerHistoric();

            // And call the process method to immediately process as this is the first hit
            serviceTimerHistoricData_Elapsed(null, null);
        }

        /// <summary>
        /// Pause checking site status
        /// </summary>
        public void Pause()
        {
            if (serviceTimerHistoricData != null)
            {
                if (serviceTimerHistoricData.Enabled)
                {
                    serviceTimerHistoricData.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Continue checking site status
        /// </summary>
        public void Continue()
        {
            if (serviceTimerHistoricData != null)
            {
                if (!serviceTimerHistoricData.Enabled)
                {
                    serviceTimerHistoricData.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Stop checking site status
        /// </summary>
        public void Stop()
        {
            if (serviceTimerHistoricData != null)
            {
                if (serviceTimerHistoricData.Enabled)
                {
                    serviceTimerHistoricData.Enabled = false;
                    serviceTimerHistoricData.Stop();
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event called when the Historic/Daily timer interval occurs
        /// </summary>
        private void serviceTimerHistoricData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {            
            try
            {
                // Only process if this is allowed to run on current day
                DayOfWeek today = DateTime.Now.DayOfWeek;
                
                if (scheduleHistoricDayOfWeek[today])
                {
                    ProcessHistoricSiteStatusData();
                }
            }
            catch (SSException ssEx)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error, ssEx.Message));
            }

            // Check if we need to reset and restart the timer
            if (DateTime.Now >= scheduleHistoricRestartDateTime)
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
                    if (serviceTimerHistoricData != null)
                    {
                        serviceTimerHistoricData.Dispose();
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
        ~SiteStatusLoaderHistoric()
        {
            Dispose(false);
        }

        #endregion

        #region Private methods

        #region Setup

        #region Historic/Daily timer
        /// <summary>
        /// Waits until the configure hour,minute,second before continuing
        /// </summary>
        private void WaitBeforeStarting()
        {
            #region Wait before starting

            try
            {
                // Read in the start time
                string second = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerHistoric.StartTime.Second"];
                string minute = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerHistoric.StartTime.Minute"];
                string hour = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerHistoric.StartTime.Hour"];

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
                    System.Threading.Thread.Sleep(500);

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
        /// Sets up the schedule days the historic/daily is run on
        /// </summary>
        private void SetupHistoricTimerScheduleDays()
        {
            #region Setup days to run the historic/daily service

            // Initialise the scheduleDays to set to run on no days
            scheduleHistoricDayOfWeek = new Dictionary<DayOfWeek, bool>(7);

            scheduleHistoricDayOfWeek.Add(DayOfWeek.Monday, false);
            scheduleHistoricDayOfWeek.Add(DayOfWeek.Tuesday, false);
            scheduleHistoricDayOfWeek.Add(DayOfWeek.Wednesday, false);
            scheduleHistoricDayOfWeek.Add(DayOfWeek.Thursday, false);
            scheduleHistoricDayOfWeek.Add(DayOfWeek.Friday, false);
            scheduleHistoricDayOfWeek.Add(DayOfWeek.Saturday, false);
            scheduleHistoricDayOfWeek.Add(DayOfWeek.Sunday, false);


            // Read in the days to run on
            string[] daysArray = null;

            try
            {
                string daysToRun = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerHistoric.Schedule.Days"];

                daysArray = daysToRun.Split(',');
            }
            catch
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                    string.Format(Messages.SSLSErrorParsingScheduleDays, "SiteStatusLoaderService.ServiceTimerHistoric.Schedule.Days")));

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
                scheduleHistoricDayOfWeek[dayOfWeek] = true;
            }
            #endregion
        }

        /// <summary>
        /// Sets up the time the historic/daily service timer is restarted
        /// </summary>
        private void SetupHistoricTimerRestartTime()
        {
            #region Set up restart time for the historic/daily timer

            DateTime tomorrow = DateTime.Now.AddDays(1);

            try
            {
                string second = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerHistoric.StartTime.Second"];
                string minute = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerHistoric.StartTime.Minute"];
                string hour = PropertyService.Instance["SiteStatusLoaderService.ServiceTimerHistoric.StartTime.Hour"];

                second = (string.IsNullOrEmpty(second)) ? "00" : second;
                minute = (string.IsNullOrEmpty(minute)) ? "00" : minute;
                hour = (string.IsNullOrEmpty(hour)) ? "00" : hour;


                int restartSecond = Convert.ToInt32(second, CultureInfo.InvariantCulture);
                int restartMinute = Convert.ToInt32(minute, CultureInfo.InvariantCulture);
                int restartHour = Convert.ToInt32(hour, CultureInfo.InvariantCulture);

                scheduleHistoricRestartDateTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, restartHour, restartMinute, restartSecond);
            }
            catch
            {
                // If any errors occur here, then log warning and carry on
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning, 
                    string.Format(Messages.SSLSLoaderServiceRestartTimeSetupFailed, threadName)));

                scheduleHistoricRestartDateTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 0, 0, 0);
            }

            #endregion
        }

        /// <summary>
        /// Sets up and enables the timer used for checking the historic site status data, every X hours
        /// </summary>
        private void SetupServiceTimerHistoric()
        {
            #region Set up and start historic/daily timer

            int timerInterval = 86400000; // default - 24 hours

            try
            {
                timerInterval = Convert.ToInt32(PropertyService.Instance["SiteStatusLoaderService.ServiceTimerHistoric.Interval.MilliSeconds"]);
            }
            catch
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                    string.Format(Messages.SSLSServiceTimerIntervalMissing, timerInterval)));
            }

            serviceTimerHistoricData = new Timer();

            serviceTimerHistoricData.BeginInit();

            serviceTimerHistoricData.Interval = timerInterval;
            serviceTimerHistoricData.Elapsed += new System.Timers.ElapsedEventHandler(this.serviceTimerHistoricData_Elapsed);

            serviceTimerHistoricData.EndInit();

            serviceTimerHistoricData.Enabled = true;
            serviceTimerHistoricData.Start();

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
            serviceTimerHistoricData.Enabled = false;
            serviceTimerHistoricData.Stop();
            serviceTimerHistoricData.Dispose();

            // And initialise a new timer
            SetupHistoricTimerRestartTime();

            SetupServiceTimerHistoric();

            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info, 
                string.Format(Messages.SSLSServiceTimerHasRestarted, threadName)));
        }

        #endregion

        #region Process

        /// <summary>
        /// Method to get the site status historic data, process it, and call method to raise alerts
        /// </summary>
        private void ProcessHistoricSiteStatusData()
        {
            ArrayList eventStatusArray = new ArrayList();  // Temp array to hold all of the events retrieved from multiple csv files
            EventStatus[] eventStatusus = null; // Array which will hold the complete set of status events

            #region Set up data location details

            // Read the location details of the data
            string urlBase = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Base"];
            string urlPIDs = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Base.PID"];
            string urlPIDPrefix = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Base.PIDPrefix"];
            
            string username = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Username"];
            string password = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Password"];
            string domain = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Domain"];
            string proxy = PropertyService.Instance["SiteStatusLoaderService.Historic.URL.Proxy"];

            // Determine if the data should be saved
            bool saveFile = false;
            string saveFileDirectory = string.Empty;
            string saveFileName = string.Empty;

            GetSaveStatusFileDetails(ref saveFile, ref saveFileDirectory, ref saveFileName);

            #endregion

            // Try getting the site status data
            try
            {
                string[] urlPIDArray = urlPIDs.Split(',');

                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSLSGettingSiteStatusData, threadName)));

                foreach (string urlPID in urlPIDArray)
                {
                    // Get the download url file name (will come back like "www.website.com/name.php?PID=1234")
                    string urlFile = GenerateHistoricStatusFileName(urlBase, urlPID, urlPIDPrefix);

                    // Update the saveFile name with the PID and datetime stamp
                    string saveFileNameWithPID = DateTime.Now.ToString("yyyyMMddHHmmss-") + 
                        "PID" + urlPID + "-" + saveFileName;

                    
                    
                    // Get the status data
                    CSVReader csvReader = HttpAccess.Instance.GetCSVFromHTTP(
                        urlFile, username, password, domain, proxy);

                    
                    
                    // Save the status data
                    csvReader = HttpAccess.Instance.SaveCSVReader(csvReader, saveFile, saveFileDirectory, saveFileNameWithPID);

                    Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                        string.Format(Messages.SSLSSiteStatusDataSavedToFile, saveFileDirectory + saveFileName)));

                    
                    
                    // Convert into useable EventStatus objects and add to the temp array
                    eventStatusArray.AddRange(EventStatusParser.Instance.ConvertCSVToEventStatus(csvReader, urlPID));

                }

                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSLSUpdatingSiteStatusAlertLevel, threadName)));

                // Update the Alert level status of each EventStatus objects and add to our final array
                eventStatusus = EventStatusAlertSettings.Instance.UpdateAlertLevelsForEvents((EventStatus[])eventStatusArray.ToArray(typeof(EventStatus)));
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

                throw updatedSSEx;
            }

            // Try logging the event
            try
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSLSLoggingSiteStatusEvents, threadName)));

                // Log the EventStatus and any alerts
                LogSiteStatusEvents(eventStatusus);
            }
            catch (SSException ssEx)
            {
                throw ssEx;
            }
        }

        /// <summary>
        /// Method which logs the Site status events. 
        /// Because this is for the Historic/Daily roll up, if they have already been logged, 
        /// then they won't be logged again
        /// </summary>
        private void LogSiteStatusEvents(EventStatus[] eventStatusArray)
        {
            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                        string.Format(Messages.SSLSHistoricSiteStatusDataCount, DateTime.Now.ToString(), eventStatusArray.Length.ToString())
                        ));

            foreach (EventStatus eventStatus in eventStatusArray)
            {
                // Log a ReferenceTransaction event to the database. 
                // These will only get written to the database if it hasn't been already (which should 
                // always be the case if the real time download works perfectly).
                // Set the write flag to true, so that any which are written from here are visible in the logs.
                ReferenceTransactionEvent rte = new ReferenceTransactionEvent(
                        eventStatus.ReferenceTransactionType,
                        eventStatus.SlaTransaction,
                        eventStatus.TimeSubmitted,
                        eventStatus.TimeCompleted,
                        serviceName,
                        eventStatus.Successful,
                        true);

                Logger.Write(rte);

            }
        }

        /// <summary>
        /// Generates the Historic Status File Name
        /// </summary>
        private string GenerateHistoricStatusFileName(string urlBase, string urlPID, string urlPIDPrefix)
        {
            StringBuilder fileName = new StringBuilder();

            #region Validate parameters

            bool throwError = false;

            if (string.IsNullOrEmpty(urlBase))
            {
                urlBase = string.Empty;

                throwError = true;
            }

            if (string.IsNullOrEmpty(urlPID))
            {
                urlPID = string.Empty;
                throwError = true;
            }

            if (string.IsNullOrEmpty(urlPIDPrefix))
            {
                urlPIDPrefix = string.Empty;
                throwError = true;
            }


            #endregion

            if (throwError)
            {
                string message = string.Format(Messages.SSLSErrorGeneratingHistoricSiteStatusURL,
                    threadName,
                    urlBase + urlPIDPrefix + urlPID);

                throw new SSException(message, false, SSExceptionIdentifier.SSLSInvalidURL);
            }
            else
            {
                // Create the filename
                fileName.Append(urlBase);
                fileName.Append(urlPIDPrefix);
                fileName.Append(urlPID);
            }

            return fileName.ToString();
        }


        /// <summary>
        /// Sets up the values required for saving the status data file
        /// </summary>
        private void GetSaveStatusFileDetails(ref bool saveFile, ref string fileDirectory, ref string fileName)
        {
            // Initialise
            saveFile = false;
            fileDirectory = string.Empty;
            fileName = string.Empty;
            
            // Set save file flag
            try
            {
                saveFile = Convert.ToBoolean(PropertyService.Instance["SiteStatusLoaderService.Historic.Save"]);
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                        string.Format(Messages.SSLSErrorParsingSaveToFileFlag, ex.Message)));

                saveFile = false;
            }

            // Get the file directory and name
            fileDirectory = PropertyService.Instance["SiteStatusLoaderService.Historic.Save.Directory"];
            fileName = PropertyService.Instance["SiteStatusLoaderService.Historic.Save.Filename"];
        }

        #endregion

        #endregion
    }
}
