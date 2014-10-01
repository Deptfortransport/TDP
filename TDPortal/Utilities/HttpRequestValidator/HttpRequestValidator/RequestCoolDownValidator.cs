using AO.HttpRequestValidatorCommon;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Timers;
using System.Web;

namespace AO.HttpRequestValidator
{
    class RequestCoolDownValidator
    {
        #region Private variables

        private static bool inCoolDown = false;
        private static string fileLocation;
        private static int reportInterval;
        private static string redirectDomain;
        private static bool redirectIncludesUrlParams;
        private static Timer timerFileCheck;
        private static Timer timerReportConnections;
        private static PerformanceCounter pcActiveConnections;
        private static string CATEGORY_NAME = string.Empty;
        private static string COUNTER_NAME = string.Empty;
        private static string INSTANCE_NAME = string.Empty;
        private static string EVENTLOG_NAME = string.Empty;
        private static string EVENTLOG_SOURCE = string.Empty;
        private static string EVENTLOG_MACHINE = string.Empty;
        private static string EVENTLOG_PREFIX = string.Empty;
        private static EventLogOutput eventLogInstance;

        #endregion

        #region Constructors

        /// <summary>
        /// Static Constructor
        /// </summary>
        static RequestCoolDownValidator()
        {
            // Retrieve config file settings
            try
            {
                ConfigurationSection section = Microsoft.Web.Administration.WebConfigurationManager.GetSection(HttpRequestValidatorKeys.ConfigurationSection);
                fileLocation = section.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_FileLocation).Value.ToString();
                reportInterval = Convert.ToInt32(section.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_ReportInterval).Value);
                CATEGORY_NAME = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_CategoryName).Value;
                INSTANCE_NAME = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_InstanceName).Value;
                COUNTER_NAME = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_PerformanceCounterName).Value;
                EVENTLOG_NAME = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Name).Value;
                EVENTLOG_SOURCE = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Source).Value;
                EVENTLOG_MACHINE = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Machine).Value;
                EVENTLOG_PREFIX = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_MessagePrefix).Value;
            }
            catch
            {
                throw new Exception(string.Format("Failed to get RequestCoolDownValidator values, config[{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8} or {9}] is missing or invalid",
                                new object[] {HttpRequestValidatorKeys.CoolDown_FileLocation,
                                HttpRequestValidatorKeys.CoolDown_PerformanceCounterName,
                                HttpRequestValidatorKeys.CoolDown_ReportInterval,
                                HttpRequestValidatorKeys.Connections_PerformanceCounter_CategoryName,
                                HttpRequestValidatorKeys.Connections_PerformanceCounter_InstanceName,
                                HttpRequestValidatorKeys.CoolDown_PerformanceCounterName,
                                HttpRequestValidatorKeys.EventLog_Name,
                                HttpRequestValidatorKeys.EventLog_Source,
                                HttpRequestValidatorKeys.EventLog_Machine,
                                HttpRequestValidatorKeys.EventLog_MessagePrefix}));
            }

            // Clear the flag file if it currently exists (flag should be cleared on restart)
            if (fileLocation != string.Empty)
            {
                if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                }

                // Setup the timer
                SetupFileCheckTimer();
            }

            // Get an event log instance
            eventLogInstance = EventLogOutput.Instance(EVENTLOG_NAME, EVENTLOG_SOURCE, EVENTLOG_MACHINE, EVENTLOG_PREFIX);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public RequestCoolDownValidator()
        {
        }

        #endregion

        #region Public methods

        public bool IsValid(HttpContext context)
        {
            if (!inCoolDown)
            {
                return true;
            }
            else
            {
                // Redirect to the redirect location
                string redirectUrl;

                if (redirectIncludesUrlParams)
                {
                    redirectUrl = redirectDomain + context.Request.Url.PathAndQuery;
                }
                else
                {
                    redirectUrl = redirectDomain;
                }

                context.Response.RedirectLocation = redirectUrl;

                return false;
            }
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Sets up and enables the timer used for checking for the flag file
        /// </summary>
        private static void SetupFileCheckTimer()
        {
            #region Set up and start file check timer

            int timerInterval = 1000; // milliseconds, 1 second

            timerFileCheck = new Timer();

            timerFileCheck.BeginInit();

            timerFileCheck.Interval = timerInterval;
            timerFileCheck.Elapsed += new ElapsedEventHandler(timerFileCheck_Elapsed);

            timerFileCheck.EndInit();

            timerFileCheck.Enabled = true;
            timerFileCheck.Start();

            #endregion
        }

        /// <summary>
        /// Sets up and enables the timer used for logging the connections every X milliseconds
        /// </summary>
        private static void SetupReportTimer()
        {
            #region Set up and start reporting timer

            Int64 timerInterval = reportInterval * 1000; // milliseconds

            timerReportConnections = new Timer();

            timerReportConnections.BeginInit();

            timerReportConnections.Interval = timerInterval;
            timerReportConnections.Elapsed += new ElapsedEventHandler(timerReportConnections_Elapsed);

            timerReportConnections.EndInit();

            timerReportConnections.Enabled = true;
            timerReportConnections.Start();

            #endregion
        }

        /// <summary>
        /// Checks for the presence of the file indicating cool down is on
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="args">event args</param>
        private static void timerFileCheck_Elapsed(object sender, ElapsedEventArgs args)
        {
            // Check for existence of the flag file if not already switched on
            if (!inCoolDown)
            {
                try
                {
                    if (File.Exists(fileLocation))
                    {
                        // Get the info from the file
                        StreamReader sr = new StreamReader(File.OpenRead(fileLocation));
                        redirectDomain = sr.ReadLine();
                        redirectIncludesUrlParams = bool.Parse(sr.ReadLine());
                        inCoolDown = true;

                        #region Setup performance counter

                        try
                        {
                            if ((!string.IsNullOrEmpty(CATEGORY_NAME)) && (!string.IsNullOrEmpty(COUNTER_NAME)))
                            {
                                pcActiveConnections = new PerformanceCounter(CATEGORY_NAME, COUNTER_NAME, INSTANCE_NAME);

                                if (pcActiveConnections == null)
                                {
                                    throw new Exception("RequestCoolDownValidator PerformanceCounter is null");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("RequestCoolDownValidator failed set up the PerformanceCounter using the values CategoryName[{0}] CounterName[{1}], InstanceName[{2}]. Exception message: {3}",
                                            CATEGORY_NAME,
                                            COUNTER_NAME,
                                            INSTANCE_NAME,
                                            ex.Message));
                        }

                        #endregion

                        #region Setup the Event log

                        StringBuilder sb = new StringBuilder("RequestCoolDownValidator initialised");
                        sb.AppendLine();
                        sb.Append("Flag file location: ");
                        sb.AppendLine(fileLocation);
                        sb.Append("Reporting interval: ");
                        sb.AppendLine(reportInterval.ToString());
                        sb.Append("Redirect to domain: ");
                        sb.AppendLine(redirectDomain);
                        sb.Append("Redirect includes URL params: ");
                        sb.AppendLine(redirectIncludesUrlParams.ToString());
                        sb.Append("Performance counter category: ");
                        sb.AppendLine(CATEGORY_NAME);
                        sb.Append("Performance counter instance: ");
                        sb.AppendLine(INSTANCE_NAME);
                        sb.Append("Performance counter name: ");
                        sb.AppendLine(COUNTER_NAME);

                        eventLogInstance.WriteEvent(sb.ToString(), EventLogEntryType.Information);

                        #endregion

                        // Start the perf counter timer
                        SetupReportTimer();
                    }
                }
                catch (Exception ex)
                {
                    // Log the error to the event log
                    eventLogInstance.WriteEvent("RequestCoolDownValidator - exception while checking flag file for cool down: " + ex.ToString(), EventLogEntryType.Error);
                }
            }
        }

        /// <summary>
        /// Reports on the number of active connections
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="args">event args</param>
        private static void timerReportConnections_Elapsed(object sender, ElapsedEventArgs args)
        {
            // Write the number of active connections to the event log
            try
            {
                int connections = Convert.ToInt32(pcActiveConnections.NextValue());

                // Log the counter to the event log
                eventLogInstance.WriteEvent("RequestCoolDownValidator - current connection count: " + connections.ToString(), EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                eventLogInstance.WriteEvent("RequestCoolDownValidator - error received while accessing performance counter, exception: " + ex.ToString(), EventLogEntryType.Error);
            }
        }

        #endregion
    }
}
