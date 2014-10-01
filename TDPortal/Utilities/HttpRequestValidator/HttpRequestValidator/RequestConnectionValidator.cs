// *********************************************** 
// NAME         : RequestActionValidator.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 02/06/2011
// DESCRIPTION  : Class to maintain a count of active connections for all requests. If the count exceeds
//              : a threshold, then returns false to allow response to terminate. 
//              : This class is used to help ease the server load when the server is under "excessive" traffic.
// ************************************************ 

using System;
using System.Diagnostics;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.Caching;
using AO.HttpRequestValidatorCommon;
using Microsoft.Web.Administration;
using System.Collections.Generic;

namespace AO.HttpRequestValidator
{
    /// <summary>
    /// Class to check the number of active connections
    /// </summary>
    public class RequestConnectionValidator
    {
        #region Private members

        // Performance counter objects
        private static PerformanceCounter pcRequestsPerSec;
        private static PerformanceCounter pcSecondRequestsPerSec;
        private static List<float> currentRequestsPerSecValues;
        private static int currentIndex;
        private static float currentTotal;
        
        private static int PERIOD_SECONDS;                  // time period in seconds to monitor for
        private static Int64 CONNECTION_TRIGGER_THRESHOLD;    // max number of connections which will trigger the "excessive" traffic check
        private static Int64 CONNECTION_RESTORE_THRESHOLD;    // number of connections where traffic is allowed back in after "excessive" traffic check was triggered
               
        private static string CACHE_KEY_CONNECTIONS = "Connections"; // Connections key used to add to the cache

        private static string EVENTLOG_NAME = string.Empty;
        private static string EVENTLOG_SOURCE = string.Empty;
        private static string EVENTLOG_MACHINE = string.Empty;
        private static string EVENTLOG_PREFIX = string.Empty;
        private static EventLogOutput eventLogInstance;

        /// <summary>
        /// A lock to prevent the objects from being accessed by more
        /// than one thread at a time.
        /// </summary>
        private static readonly object ConnectionsLock = new object();

        #region Connections timer

        /// <summary>
        /// Timer used for updating connections 
        /// </summary>
        private static Timer timerConnections;
        /// <summary>
        /// Used to restart the timer at a specific time
        /// </summary>
        private static DateTime timerConnectionsRestartDateTime;

        #endregion

        #region Logging
                
        /// <summary>
        /// Timer used for logging connection counts
        /// </summary>
        private static Timer timerConnectionsLog;
        /// <summary>
        /// Used to restart the log timer at a specific time
        /// </summary>
        private static DateTime timerConnectionsLogRestartDateTime;

        #endregion
        
        #endregion

        #region Constructor

        /// <summary>
        /// Static Constructor
        /// </summary>
        static RequestConnectionValidator()
        {
            ConfigurationSection section = Microsoft.Web.Administration.WebConfigurationManager.GetSection(HttpRequestValidatorKeys.ConfigurationSection);

            #region Set values used by the validator

            try
            {
                // Time period to monitor for
                PERIOD_SECONDS = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_MonitorPeriodSeconds).Value;

                if (PERIOD_SECONDS <= 0)
                {
                    throw new Exception();
                }

                EVENTLOG_NAME = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Name).Value;
                EVENTLOG_SOURCE = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Source).Value;
                EVENTLOG_MACHINE = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Machine).Value;
                EVENTLOG_PREFIX = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_MessagePrefix).Value;
            }
            catch
            {
                throw new Exception(string.Format("Failed to set RequestActionValidator values, config[{0}, {1}, {2}, {3} or {4}] is missing or invalid",
                                new object[] {HttpRequestValidatorKeys.Connections_MonitorPeriodSeconds,
                                HttpRequestValidatorKeys.EventLog_Name,
                                HttpRequestValidatorKeys.EventLog_Source,
                                HttpRequestValidatorKeys.EventLog_Machine,
                                HttpRequestValidatorKeys.EventLog_MessagePrefix}));
            }

            try
            {
                // Max connections trigger threshold
                CONNECTION_TRIGGER_THRESHOLD = Convert.ToInt64((Int64)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_TriggerThreshold).Value);

                if (CONNECTION_TRIGGER_THRESHOLD <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new Exception(string.Format("Failed to set RequestActionValidator Connection Trigger Threshold value, config[{0}] is missing or invalid",
                                HttpRequestValidatorKeys.Connections_TriggerThreshold));
            }

            try
            {
                // Connections restore threshold
                CONNECTION_RESTORE_THRESHOLD = Convert.ToInt64((Int64)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_RestoreThreshold).Value);

                if (CONNECTION_RESTORE_THRESHOLD <= 0)
                {
                    throw new Exception();
                }
            }
            catch
            {
                throw new Exception(string.Format("Failed to set RequestActionValidator Connection Restore Threshold value, config[{0}] is missing or invalid",
                                HttpRequestValidatorKeys.Connections_RestoreThreshold));
            }
            
            #endregion

            #region Setup the Performance counter

            string categoryName = string.Empty;
            string counterName = string.Empty;
            string instanceName = string.Empty;
            string secondInstanceName = string.Empty;

            try
            {
                // Performance counter
                categoryName = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_CategoryName).Value;
                counterName = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_CounterName).Value;
                instanceName = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_InstanceName).Value;
                secondInstanceName = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_SecondInstanceName).Value;
            }
            catch
            {
                throw new Exception(string.Format("Failed to set RequestActionValidator Performance Monitor values, config[{0}, {1}, {2} or {3}] is missing or invalid",
                                HttpRequestValidatorKeys.Connections_PerformanceCounter_CategoryName,
                                HttpRequestValidatorKeys.Connections_PerformanceCounter_CounterName,
                                HttpRequestValidatorKeys.Connections_PerformanceCounter_InstanceName,
                                HttpRequestValidatorKeys.Connections_PerformanceCounter_SecondInstanceName));
            }

            try
            {
                if ((!string.IsNullOrEmpty(categoryName)) && (!string.IsNullOrEmpty(counterName)))
                {
                    pcRequestsPerSec = new PerformanceCounter(categoryName, counterName, instanceName);

                    if (!string.IsNullOrEmpty(secondInstanceName))
                    {
                        pcSecondRequestsPerSec = new PerformanceCounter(categoryName, counterName, secondInstanceName);

                        if (pcSecondRequestsPerSec == null)
                        {
                            throw new Exception("Second PerformanceCounter is null");
                        }
                    }

                    if (pcRequestsPerSec == null)
                    {
                        throw new Exception("PerformanceCounter is null");
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(secondInstanceName))
                {
                    throw new Exception(string.Format("Failed set up the PerformanceCounter using the values CategoryName[{0}] CounterName[{1}], InstanceName[{2}], Second InstanceName[{3}]. Exception message: {4}",
                                    categoryName,
                                    counterName,
                                    instanceName,
                                    secondInstanceName, 
                                    ex.Message));
                }
                else
                {
                    throw new Exception(string.Format("Failed set up the PerformanceCounter using the values CategoryName[{0}] CounterName[{1}], InstanceName[{2}]. Exception message: {3}",
                                    categoryName,
                                    counterName,
                                    instanceName,
                                    ex.Message));
                }
            }


            #endregion

            #region Setup the File log

            ConnectionStatusFileOutput.Instance.UpdateStatusFile(0, DateTime.Now);

            #endregion

            #region Setup the timer for connections

            currentRequestsPerSecValues = new List<float>();
            currentIndex = 0;
            currentTotal = 0;

            SetupConnectionsTimer();
            SetupConnectionsTimerRestartTime();

            #endregion

            #region Setup the timer for logging

            SetupConnectionsLogTimer();
            SetupConnectionsLogTimerRestartTime();

            #endregion

            #region Setup the Event log

            eventLogInstance = EventLogOutput.Instance(EVENTLOG_NAME, EVENTLOG_SOURCE, EVENTLOG_MACHINE, EVENTLOG_PREFIX);

            StringBuilder sb = new StringBuilder("RequestConnectionValidator initialised");
            sb.AppendLine();
            sb.Append("Time period: ");
            sb.AppendLine(PERIOD_SECONDS.ToString());
            sb.Append("Trigger level: ");
            sb.AppendLine(CONNECTION_TRIGGER_THRESHOLD.ToString());
            sb.Append("Restore level: ");
            sb.AppendLine(CONNECTION_RESTORE_THRESHOLD.ToString());
            sb.Append("Performance monitor category: ");
            sb.AppendLine(categoryName);
            sb.Append("Performance monitor counter name: ");
            sb.AppendLine(counterName);
            sb.Append("Performance monitor instance: ");
            sb.AppendLine(instanceName);
            sb.Append("Performance monitor second instance: ");
            sb.AppendLine(secondInstanceName);
            sb.Append("Status file path: ");
            sb.AppendLine((string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_StatusFilePath).Value);
            sb.Append("Status file name: ");
            sb.AppendLine((string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_StatusFileName).Value);

            eventLogInstance.WriteEvent(sb.ToString(), EventLogEntryType.Information);

            #endregion
        }

        
        /// <summary>
        /// Constructor
        /// </summary>
        public RequestConnectionValidator()
        {
        }

        #endregion

        #region Static methods

        #region Timer methods

        #region Elapsed events

        /// <summary>
        /// TimerConnections elapsed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerConnections_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (pcRequestsPerSec != null)
            {
                // Lock to avoid any thread problems if a timer recycle occurs, which will reset the "current" values
                lock (ConnectionsLock)
                {
                    // Values used to calculate the moving total
                    float oldValue = 0;
                    float newValue = 0;

                    try
                    {
                        newValue = pcRequestsPerSec.NextValue();
                    }
                    catch (Exception ex)
                    {
                        // May fail if the performance counter instance being monitored expires (after it was attached to)
                        // e.g. when the .NET application worker process is removed from memory
                        eventLogInstance.WriteEvent(
                            string.Format("Error attempting to read the Performance Counter value. Exception message[{0}] stack trace[{1}]", 
                                ex.Message, ex.StackTrace),
                            EventLogEntryType.Error);
                    }

                    try
                    {
                        newValue += pcSecondRequestsPerSec.NextValue();
                    }
                    catch (Exception ex)
                    {
                        // May fail if the performance counter instance being monitored expires (after it was attached to)
                        // e.g. when the .NET application worker process is removed from memory
                        eventLogInstance.WriteEvent(
                            string.Format("Error attempting to read the Performance Counter value. Exception message[{0}] stack trace[{1}]",
                                ex.Message, ex.StackTrace),
                            EventLogEntryType.Error);
                    }

                    // Updates the current set of performace monitor values
                    if (currentRequestsPerSecValues.Count >= (currentIndex + 1))
                    {
                        // Grab value being removed so it can be used to calculate the new total
                        oldValue = currentRequestsPerSecValues[currentIndex];

                        // Update the new value
                        currentRequestsPerSecValues[currentIndex] = newValue;
                    }
                    else
                    {
                        // Haven't got to the required number of values, so add
                        currentRequestsPerSecValues.Add(newValue);
                    }

                    // Update the index ready for the next elapsed interval
                    currentIndex++;

                    // Assume period to monitor duration has been validated
                    if (currentIndex >= PERIOD_SECONDS)
                    {
                        currentIndex = 0;
                    }

                    // Update the current total
                    currentTotal = currentTotal - oldValue + newValue;
                }
            }
            
            // Check if we need to reset and restart the timer
            if (DateTime.Now >= timerConnectionsRestartDateTime)
            {
                RecycleConnectionsTimer();
            }
        }
        
        /// <summary>
        /// TimerConnectionsLog elapsed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timerConnectionsLog_Elapsed(object sender, ElapsedEventArgs e)
        {
            ConnectionStatusFileOutput.Instance.UpdateStatusFile(currentTotal, DateTime.Now);
            
            // Check if we need to reset and restart the timer
            if (DateTime.Now >= timerConnectionsLogRestartDateTime)
            {
                RecycleConnectionsLogTimer();
            }
        }

        #endregion

        #region Setup and recycle methods

        /// <summary>
        /// Sets up and enables the timer used for logging the connections every X milliseconds
        /// </summary>
        private static void SetupConnectionsTimer()
        {
            #region Set up and start current/reat time timer

            int timerInterval = 1000; // milliseconds, 1 second

            timerConnections = new Timer();

            timerConnections.BeginInit();

            timerConnections.Interval = timerInterval;
            timerConnections.Elapsed += new ElapsedEventHandler(timerConnections_Elapsed);

            timerConnections.EndInit();

            timerConnections.Enabled = true;
            timerConnections.Start();

            #endregion
        }


        /// <summary>
        /// Sets up and enables the timer used for logging the connections every X milliseconds
        /// </summary>
        private static void SetupConnectionsLogTimer()
        {
            #region Set up and start current/reat time timer
            
            int timerInterval = 60000; // milliseconds, 1 minute

            timerConnectionsLog = new Timer();

            timerConnectionsLog.BeginInit();

            timerConnectionsLog.Interval = timerInterval;
            timerConnectionsLog.Elapsed += new ElapsedEventHandler(timerConnectionsLog_Elapsed);

            timerConnectionsLog.EndInit();

            timerConnectionsLog.Enabled = true;
            timerConnectionsLog.Start();

            #endregion
        }

        /// <summary>
        /// Sets up the time the connections timer is restarted
        /// </summary>
        private static void SetupConnectionsTimerRestartTime()
        {
            DateTime restart = DateTime.Now.AddHours(3);

            timerConnectionsRestartDateTime = restart;
        }

        /// <summary>
        /// Sets up the time the connections log timer is restarted
        /// </summary>
        private static void SetupConnectionsLogTimerRestartTime()
        {
            DateTime restart = DateTime.Now.AddDays(1);

            timerConnectionsLogRestartDateTime = new DateTime(restart.Year, restart.Month, restart.Day, 0, 0, 0);
        }

        /// <summary>
        /// Removes the current connections timer, waits for the configured time, then starts a new timer
        /// </summary>
        private static void RecycleConnectionsTimer()
        {
            // Remove the current timer
            timerConnections.Enabled = false;
            timerConnections.Stop();
            timerConnections.Dispose();

            // Reset the count values
            lock (ConnectionsLock)
            {
                currentRequestsPerSecValues = new List<float>();
                currentIndex = 0;
                currentTotal = 0;
            }

            // And initialise a new timer
            SetupConnectionsTimer();
            SetupConnectionsTimerRestartTime();
        }

        /// <summary>
        /// Removes the current connections log timer, waits for the configured time, then starts a new timer
        /// </summary>
        private static void RecycleConnectionsLogTimer()
        {
            // Remove the current timer
            timerConnectionsLog.Enabled = false;
            timerConnectionsLog.Stop();
            timerConnectionsLog.Dispose();

            // And initialise a new timer
            SetupConnectionsLogTimer();
            SetupConnectionsLogTimerRestartTime();
        }
                
        #endregion

        #endregion

        #endregion

        #region Public methods

        /// <summary>
        /// Method which checks if the number of requests (connections) has exceeded an allowed connections threshold
        /// </summary>
        /// <returns></returns>
        public bool IsValid(HttpContext context)
        {
            if ((context != null) && (context.Cache != null))
            {
                Cache cache = context.Cache;

                // Get connection details from cache
                ConnectionInfo connectionInfo = (ConnectionInfo)(cache[CACHE_KEY_CONNECTIONS] ?? new ConnectionInfo());

                // Add to web cache if new connectionInfo created, specifying an absolute expiration time.
                // This allows the connections info to be recycled to avoid any erroneous coonections to cause 
                // compounded effects over a long period of time
                if (connectionInfo.Connections == -1)
                {
                    // Set to be 0 as its a new ConnectionInfo object
                    connectionInfo.Connections = 0;

                    cache.Add(CACHE_KEY_CONNECTIONS, connectionInfo, null,
                        DateTime.Now.AddHours(1), Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);
                }
                
                #region Validation
                
                // If not in cache, then assume ok
                if (connectionInfo != null)
                {
                    // Perform the validation

                    // Not using cache object locking here because of the performance cost, and also 
                    // when any scenario is triggered there will be enough requests for one to succesfully
                    // update object in cache

                    // All scenarios where Connections trigger is exceeded, fail
                    if (currentTotal > CONNECTION_TRIGGER_THRESHOLD)
                    {
                        // Log if this is the first time threshold exceeded
                        if (!connectionInfo.ConnectionsThresholdExceededLogged)
                        {
                            connectionInfo.ConnectionsThresholdExceededLogged = true;

                            eventLogInstance.WriteEvent(
                                string.Format("Connections trigger threshold exceeded. Number of connections[{0}]", currentTotal),
                                EventLogEntryType.Error);
                        }

                        connectionInfo.ConnectionsThresholdExceeded = true;

                        return false;
                    }
                    // For scenarios where Connections restore is exceeded, only fail where the Connections trigger
                    // was previously exceeded
                    else if ((currentTotal > CONNECTION_RESTORE_THRESHOLD)
                        && (connectionInfo.ConnectionsThresholdExceeded))
                    {
                        return false;
                    }
                    // Else OK, reset Connection threshold exceeded flag
                    else if (connectionInfo.ConnectionsThresholdExceeded)
                    {
                        // Reset flags
                        connectionInfo.ConnectionsThresholdExceeded = false;
                        connectionInfo.ConnectionsThresholdExceededLogged = false;

                        // Log access is restored
                        eventLogInstance.WriteEvent(
                            string.Format("Connections have now dropped to below restore threshold. Number of connections[{0}]", currentTotal),
                            EventLogEntryType.Information);

                    }
                }

                #endregion
            }

            return true;
        }

        #endregion

        #region Private methods
                
        #endregion
    }

    #region Class - ConnectionInfo

    /// <summary>
    /// Class to keep track of connections info
    /// </summary>
    [Serializable]
    public class ConnectionInfo
    {
        private int connectionCount;
        private bool connectionThresholdExceeded;
        private bool connectionThresholdExceededLogged;
        
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionInfo()
        {
            connectionCount = -1;
            connectionThresholdExceeded = false;
            connectionThresholdExceededLogged = false;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Number of connections
        /// </summary>
        public int Connections
        {
            get { return connectionCount; }
            set { connectionCount = value; }
        }

        /// <summary>
        /// Read/Write. Flag used to track if number of connections threshold was exceeded
        /// </summary>
        public bool ConnectionsThresholdExceeded
        {
            get { return connectionThresholdExceeded; }
            set { connectionThresholdExceeded = value; }
        }

        /// <summary>
        /// Read/Write. Flag used to track if connections threshold exceeded message was logged
        /// </summary>
        public bool ConnectionsThresholdExceededLogged
        {
            get { return connectionThresholdExceededLogged; }
            set { connectionThresholdExceededLogged = value; }
        }
                
        #endregion
    }

    #endregion
}
