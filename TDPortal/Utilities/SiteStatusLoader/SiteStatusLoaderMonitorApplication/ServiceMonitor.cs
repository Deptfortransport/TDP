// *********************************************** 
// NAME                 : ServiceMonitor.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: ServiceMonitor class. Connects to a window service to monitor, displays status icon in system tray
// and a UI form with status updates
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderMonitorApplication/ServiceMonitor.cs-arc  $
//
//   Rev 1.4   Aug 24 2011 11:07:56   mmodi
//Added a manual import facility to the SSL UI tool
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.3   Jun 16 2009 15:52:22   mmodi
//Updated to parse datetime value using a format
//Resolution for 5298: Site Status Loader - Update to log Red Alerts as successful
//
//   Rev 1.2   Apr 09 2009 10:49:00   mmodi
//Added status label to form and bubble messages on alerts
//Resolution for 5273: Site Status Loader
//
//   Rev 1.1   Apr 06 2009 16:13:48   mmodi
//Removed alert count logic, amended to run a single instance only, added Connect test tools form
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:34:38   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Xml;
using System.Windows.Forms;

using AO.Common;
using AO.DatabaseInfrastructure;
using AO.EventLogging;
using AO.Properties;

using PropertyService = AO.Properties.Properties;
using Logger = System.Diagnostics.Trace;

namespace AO.SiteStatusLoaderMonitorApplication
{
    public partial class ServiceMonitor : Form
    {
        #region Delegates

        public delegate void SimpleDelegate();
        
        #endregion

        #region Private members

        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;

        private MenuItem menuItemExit;
        private MenuItem menuItemStopService;
        private MenuItem menuItemStartService;
        private MenuItem menuItemPauseService;
        private MenuItem menuItemContinueService;
        private MenuItem menuItemStatusForm;

        private string applicationName;
        private string iconDisabled;
        private string iconEnabled;
        private string iconPaused;
        private string iconRunning;
        private string iconStopped;
        private string iconRunningAlertRed;
        private string iconRunningAlertAmber;
        private string iconRunningUnknownStatus;

        private string serviceToMonitor = string.Empty;
        private ServiceController serviceController; // Used to monitor a windows service
        private System.Timers.Timer serviceMonitorTimer; // Timer used to check the service being monitored

        private DataGridView dataGridView1;
        private BindingSource bind = new BindingSource();

        private string dateTimeFormat = "yyyyMMdd HH:mm:ss.fffff"; // Default value, overwritten by properties
        
        private readonly object updatingForm = false;

        private ServiceStatus serviceStatusCurrent = ServiceStatus.Stopped; // Keeps track of service running status 
        private ServiceStatus serviceStatusPrevious = ServiceStatus.Stopped; // Keeps track of service running status 

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor. Sets up all the elements used by the application
        /// </summary>
        public ServiceMonitor()
        {
            // Must come first to establish where to log to
            SetupEventLoggingTraceListener();

            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                Messages.SSMAStarting));
            
            SetupResources();

            InitializeComponent();

            SetupServiceController();
                        
            CreateMenu();

            CreateNotifyIcon();

            SetupDataGrid();
            
            ConfigureForm();

            UpdateServiceStatusIconAndMenu();

            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                Messages.SSMAStarted));
        }

        #endregion
        
        #region Private Methods

        #region Setup

        /// <summary>
        /// Adds the SSTraceListener to allow logging. If an exception occurs, the application is terminated
        /// </summary>
        private void SetupEventLoggingTraceListener()
        {
            ArrayList errors = new ArrayList();

            try
            {
                IEventPublisher[] customPublishers = new IEventPublisher[1];

                // Create custom database publishers which will be used to publish 
                // events received by the eventreceiver. Note: ids passed in constructors
                // must match those defined in the properties.
                customPublishers[0] = new SSCustomEventPublisher("SSDB", SqlHelperDatabase.ReportStagingDB);
                                
                // create and add TraceListener instance to the listener collection	
                Trace.Listeners.Add(new SSTraceListener(PropertyService.Instance, customPublishers, errors));

                // Initialise was ok
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSMAInitialisationCompleted, "Site Status Loader Monitor")));
            }
            catch (SSException ssEx)
            {
                #region Log
                // create message string
                StringBuilder message = new StringBuilder(100);

                message.Append( string.Format(Messages.SSMAInitialisationFailed, ssEx.Message));
                
                message.Append("\n");

                // append all messages returned by TraceListener constructor
                foreach (string error in errors)
                {
                    message.Append(error);
                    message.Append("\n");
                }

                // Log error to default trace listener in case SSTraceListener failed to create.
                EventLog.WriteEntry("Site Status Loader Monitor", message.ToString() , EventLogEntryType.Error, (int)ssEx.Identifier);
                #endregion

                ExitApplication(true);
            }
        }

        /// <summary>
        /// Read values from the properties, set up constant strings
        /// </summary>
        private void SetupResources()
        {
            applicationName = PropertyService.Instance["SiteStatusMonitorApplication.SystemTray.DisplayText"];
            iconDisabled = "SiteStatusMonitorApplication.SystemTray.Icon.Disabled";
            iconEnabled = "SiteStatusMonitorApplication.SystemTray.Icon.Enabled";
            iconPaused = "SiteStatusMonitorApplication.SystemTray.Icon.Paused";
            iconRunning = "SiteStatusMonitorApplication.SystemTray.Icon.Running";
            iconStopped = "SiteStatusMonitorApplication.SystemTray.Icon.Stopped";
            iconRunningAlertAmber = "SiteStatusMonitorApplication.SystemTray.Icon.RunningAmber";
            iconRunningAlertRed = "SiteStatusMonitorApplication.SystemTray.Icon.RunningRed";
            iconRunningUnknownStatus = "SiteStatusMonitorApplication.SystemTray.Icon.RunningUnknown";

            dateTimeFormat = PropertyService.Instance["SiteStatusLoaderService.StatusFile.DateTimeFormat"];
        }

        /// <summary>
        /// Sets up the Menu used by the Notify Icon (in the System Tray)
        /// </summary>
        private void CreateMenu()
        {
            contextMenu = new ContextMenu();

            menuItemStartService = new MenuItem();
            menuItemPauseService = new MenuItem();
            menuItemContinueService = new MenuItem();
            menuItemStopService = new MenuItem();
            menuItemStatusForm = new MenuItem();
            menuItemExit = new MenuItem();

            menuItemStartService.Index = 0;
            menuItemStartService.Text = "&Start";
            menuItemStartService.Enabled = false;
            menuItemStartService.Click += new System.EventHandler(this.menuItemStartService_Click);

            menuItemPauseService.Index = 1;
            menuItemPauseService.Text = "&Pause";
            menuItemStartService.Enabled = false;
            menuItemPauseService.Click += new System.EventHandler(this.menuItemPauseService_Click);

            menuItemContinueService.Index = 2;
            menuItemContinueService.Text = "&Continue";
            menuItemStartService.Enabled = false;
            menuItemContinueService.Click += new System.EventHandler(this.menuItemContinueService_Click);

            menuItemStopService.Index = 3;
            menuItemStopService.Text = "S&top";
            menuItemStartService.Enabled = false;
            menuItemStopService.Click += new System.EventHandler(this.menuItemStopService_Click);

            menuItemStatusForm.Index = 4;
            menuItemStatusForm.Text = "St&atus";
            menuItemStatusForm.Click += new EventHandler(this.menuItemStatusForm_Click);

            menuItemExit.Index = 5;
            menuItemExit.Text = "E&xit";
            menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);

            contextMenu.MenuItems.Add(menuItemStartService);
            contextMenu.MenuItems.Add(menuItemPauseService);
            contextMenu.MenuItems.Add(menuItemContinueService);
            contextMenu.MenuItems.Add(menuItemStopService);
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add(menuItemStatusForm);
            contextMenu.MenuItems.Add("-");
            contextMenu.MenuItems.Add(menuItemExit);

        }

        /// <summary>
        /// Sets up the Notify icon shown in the System Tray
        /// </summary>
        private void CreateNotifyIcon()
        {
            if (notifyIcon == null)
            {
                notifyIcon = new NotifyIcon(this.components);
            }

            notifyIcon.ContextMenu = this.contextMenu;
            notifyIcon.Icon = new Icon(PropertyService.Instance[iconDisabled]);
            notifyIcon.Text = applicationName;
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
        }

        /// <summary>
        /// Sets up the initial state of the form
        /// </summary>
        private void ConfigureForm()
        {
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Visible = false;
            this.Text = applicationName;
        }

        /// <summary>
        /// Sets up the timer used to monitor the service
        /// </summary>
        private void SetupTimer()
        {
            int timerInterval = 2000; // default

            try
            {
                timerInterval = Convert.ToInt32(PropertyService.Instance["SiteStatusMonitorApplication.ServiceMonitorTimer.Interval.MilliSeconds"]);
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                    "Property [SiteStatusMonitorApplication.ServiceMonitorTimer.Interval.MilliSeconds] is missing, defaulting to timer interval " + timerInterval + " milliseconds. Exception: " + ex.Message));
            }

            serviceMonitorTimer = new System.Timers.Timer();
            
            serviceMonitorTimer.AutoReset = true;
            serviceMonitorTimer.Interval = timerInterval;
            serviceMonitorTimer.Start();

            this.serviceMonitorTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.serviceMonitorTimer_Elapsed);

            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                        string.Format(Messages.SSMAServiceTimerRunning, timerInterval.ToString())));

        }

        /// <summary>
        /// Sets up the service to monitor. Any exceptions will terminate this appplication
        /// </summary>
        private void SetupServiceController()
        {
            try
            {
                serviceToMonitor = PropertyService.Instance["SiteStatusLoaderService.Name"];
                
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSMAConnectingToService, serviceToMonitor)));

                serviceController = new ServiceController(serviceToMonitor);

                // Check ther serviceController has been set up, exception will be thrown if we can't get to the status
                if ((serviceController.Status == ServiceControllerStatus.Stopped) ||
                    (serviceController.Status == ServiceControllerStatus.Running) ||
                    (serviceController.Status == ServiceControllerStatus.Paused))
                {
                    Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                        string.Format(Messages.SSMAConnectedToService, serviceToMonitor)));
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                    string.Format(Messages.SSMAErrorConnectingToService, ex.Message)));

                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSMAExiting, applicationName)));

                // Fatal error, unable establish the service to monitor, can't start application so must exit
                ExitApplication(true);
            }
        }

        /// <summary>
        /// Sets up the datagrid
        /// </summary>
        private void SetupDataGrid()
        {
            #region Set-up datagrid

            dataGridView1.AutoGenerateColumns = false;

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.Name = "Event Name";
            column.DataPropertyName = "EventName";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Status";
            column.DataPropertyName = "AlertLevel";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Time Submitted (UTC)";
            column.DataPropertyName = "TimeSubmitted";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.Name = "Duration";
            column.DataPropertyName = "Duration";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(column);

            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
            #endregion
        }
        
        #endregion

        #region Update status form and system tray icon

        /// <summary>
        /// Refreshes the Notify Icon and Context Menu dependent on the status of the service
        /// </summary>
        private void UpdateServiceStatusIconAndMenu()
        {
            serviceController.Refresh();

            // Menu items
            // 0 = Start            
            // 1 = Pause
            // 2 = Continue
            // 3 = Stop
            // 4 = Status
            // 5 = Quit

            bool canPauseAndContinue = serviceController.CanPauseAndContinue;

            switch (serviceController.Status)
            {
                case ServiceControllerStatus.Running:
                    contextMenu.MenuItems[0].Enabled = false;
                    contextMenu.MenuItems[1].Enabled = true && canPauseAndContinue;
                    contextMenu.MenuItems[2].Enabled = false;
                    contextMenu.MenuItems[3].Enabled = true;

                    startToolStripMenuItem.Enabled = false;
                    pauseToolStripMenuItem.Enabled = true && canPauseAndContinue;
                    continueToolStripMenuItem.Enabled = false;
                    stopToolStripMenuItem.Enabled = true;

                    UpdateNotifyIcon(iconRunning);
                    UpdateStatusLabel(Messages.SSMAServiceRunning, iconRunning);

                    // Global service status is set in UpdateStatusForm() to allow bubble messages to be displayed correctly
                    break;
                case ServiceControllerStatus.Paused:
                    contextMenu.MenuItems[0].Enabled = false;
                    contextMenu.MenuItems[1].Enabled = false;
                    contextMenu.MenuItems[2].Enabled = true && canPauseAndContinue;
                    contextMenu.MenuItems[3].Enabled = false;

                    startToolStripMenuItem.Enabled = false;
                    pauseToolStripMenuItem.Enabled = false;
                    continueToolStripMenuItem.Enabled = true && canPauseAndContinue;
                    stopToolStripMenuItem.Enabled = false;

                    UpdateNotifyIcon(iconPaused);
                    UpdateStatusLabel(Messages.SSMAServicePaused, iconPaused);

                    // Set global service status
                    UpdateServiceStatus(ServiceStatus.Paused);
                    break;
                case ServiceControllerStatus.Stopped:
                    contextMenu.MenuItems[0].Enabled = true;
                    contextMenu.MenuItems[1].Enabled = false;
                    contextMenu.MenuItems[2].Enabled = false;
                    contextMenu.MenuItems[3].Enabled = false;
                    
                    startToolStripMenuItem.Enabled = true;
                    pauseToolStripMenuItem.Enabled = false;
                    continueToolStripMenuItem.Enabled = false;
                    stopToolStripMenuItem.Enabled = false;

                    UpdateNotifyIcon(iconStopped);
                    UpdateStatusLabel(Messages.SSMAServiceStopped, iconStopped);

                    // Set global service status
                    UpdateServiceStatus(ServiceStatus.Stopped);
                    break;
                case ServiceControllerStatus.StartPending:
                case ServiceControllerStatus.PausePending:
                case ServiceControllerStatus.ContinuePending:
                case ServiceControllerStatus.StopPending:
                    contextMenu.MenuItems[0].Enabled = false;
                    contextMenu.MenuItems[1].Enabled = false;
                    contextMenu.MenuItems[2].Enabled = false;
                    contextMenu.MenuItems[3].Enabled = false;

                    startToolStripMenuItem.Enabled = false;
                    pauseToolStripMenuItem.Enabled = false;
                    continueToolStripMenuItem.Enabled = false;
                    stopToolStripMenuItem.Enabled = false;

                    UpdateNotifyIcon(iconEnabled);
                    break;
                default:
                    contextMenu.MenuItems[0].Enabled = false;
                    contextMenu.MenuItems[1].Enabled = false;
                    contextMenu.MenuItems[2].Enabled = false;
                    contextMenu.MenuItems[3].Enabled = false;

                    startToolStripMenuItem.Enabled = false;
                    pauseToolStripMenuItem.Enabled = false;
                    continueToolStripMenuItem.Enabled = false;
                    stopToolStripMenuItem.Enabled = false;

                    UpdateNotifyIcon(iconDisabled);
                    UpdateStatusLabel(Messages.SSMAServiceStopped, iconDisabled);
                    break;
            }
        }

        /// <summary>
        /// Disables all Menu items
        /// </summary>
        private void DisableAllServiceMenus()
        {
            contextMenu.MenuItems[0].Enabled = false;
            contextMenu.MenuItems[1].Enabled = false;
            contextMenu.MenuItems[2].Enabled = false;
            contextMenu.MenuItems[3].Enabled = false;

            startToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = false;
            continueToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = false;
        }

        /// <summary>
        /// Updates the current and previous global ServiceStatus values
        /// </summary>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        private void UpdateServiceStatus(ServiceStatus newStatus)
        {
            serviceStatusPrevious = serviceStatusCurrent;
            serviceStatusCurrent = newStatus;
        }

        /// <summary>
        /// Updates the system tray Notify icon with icon image Property Name passed
        /// </summary>
        private void UpdateNotifyIcon(string iconType)
        {
            notifyIcon.Icon = new Icon(PropertyService.Instance[iconType]);
        }

        /// <summary>
        /// Updates the status label in the status bar or the form
        /// </summary>
        private void UpdateStatusLabel(string message, string iconType)
        {
            lblStatus.Text = message;
            lblStatus.Image = new Bitmap(PropertyService.Instance[iconType]);
        }

        /// <summary>
        /// Updates the status of events shown on the main status form
        /// </summary>
        private void UpdateStatusForm()
        {
            lock (updatingForm)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                            Messages.SSMAUpdateSiteStatusForm));

                List<EventStatusRow> eventStatusList = new List<EventStatusRow>();

                bool statusFileOK = true;

                string statusFilePath = GetFilePath();

                if ((!string.IsNullOrEmpty(statusFilePath)) && (File.Exists(statusFilePath)))
                {
                    try
                    {
                        // Status file should be an xml file
                        XmlReaderSettings settings = new XmlReaderSettings();
                        settings.CloseInput = true;

                        XmlReader reader = XmlReader.Create(statusFilePath, settings);

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load(reader);

                        // Release the file
                        reader.Close();

                        #region Get all the status records
                        XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("//Status");

                        Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                            "Parsing status events"));

                        //Loop through the XML nodes creating the status row
                        foreach (XmlNode node in nodeList)
                        {
                            string eventName = node.Attributes["EventName"].InnerText;
                            DateTime timeSubmitted = DateTime.ParseExact(node.Attributes["TimeSubmitted"].InnerText, dateTimeFormat, CultureInfo.InvariantCulture);
                            TimeSpan duration = TimeSpan.FromSeconds(Convert.ToDouble(node.Attributes["Duration"].InnerText));
                            string eventStatus = node.InnerText;

                            AlertLevel alertLevel = AlertLevel.Unknown;

                            try
                            {
                                alertLevel = (AlertLevel)Enum.Parse(typeof(AlertLevel), eventStatus);
                            }
                            catch
                            {
                                // Unable to parse into a correct alert type. This shouldnt have happened
                                // as we control the alert status values
                                alertLevel = AlertLevel.Unknown;
                            }

                            eventStatusList.Add(new EventStatusRow(eventName, alertLevel, timeSubmitted, duration));
                        }

                        #endregion

                        #region Get and set the Last updated date time
                        // Update the last updated date/time (should only be one)
                        nodeList = xmlDoc.DocumentElement.SelectNodes("//LastUpdated");

                        Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                            "Parsing last updated date time"));

                        foreach (XmlNode node in nodeList)
                        {
                            string lastUpdated = node.InnerText;

                            DateTime dateTimeLastUpdated = DateTime.ParseExact(lastUpdated, dateTimeFormat, CultureInfo.InvariantCulture);

                            toolStripStatusLastUpdatedDateTime.Text = dateTimeLastUpdated.ToString();
                        }
                        #endregion

                        #region Get and set the Alert levels count
                        // Update the alert counts last updated date/time (should only be one)
                        nodeList = xmlDoc.DocumentElement.SelectNodes("//AlertCountLastUpdated");

                        Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                            "Updateing alert levels count"));

                        foreach (XmlNode node in nodeList)
                        {
                            string lastUpdated = node.InnerText;

                            DateTime dateTimeAlertCountLastUpdated = DateTime.ParseExact(lastUpdated, dateTimeFormat, CultureInfo.InvariantCulture);

                            lblAlertsSinceDateTime.Text = dateTimeAlertCountLastUpdated.ToString() + "UTC";
                        }

                        // Update the alert counts
                        nodeList = xmlDoc.DocumentElement.SelectNodes("//AlertCount");

                        foreach (XmlNode node in nodeList)
                        {
                            string strAlertLevel = node.Attributes["AlertLevel"].InnerText;
                            string strAlertCount = node.InnerText;

                            AlertLevel alertLevel = (AlertLevel)Enum.Parse(typeof(AlertLevel), strAlertLevel);
                            int alertCount = Convert.ToInt32(strAlertCount);

                            // Update the count labels
                            switch (alertLevel)
                            {
                                case AlertLevel.Green:
                                    lblAlertGreenCount.Text = alertCount.ToString();
                                    break;
                                case AlertLevel.Amber:
                                    lblAlertAmberCount.Text = alertCount.ToString();
                                    break;
                                case AlertLevel.Red:
                                    lblAlertRedCount.Text = alertCount.ToString();
                                    break;
                                default:
                                    //do nothing
                                    break;
                            }
                        }

                        #endregion

                        #region Get the last error message

                        // Update any exceptions returned
                        string errorMessage = string.Empty;
                        int errorId = -1;
                        nodeList = xmlDoc.DocumentElement.SelectNodes("//ErrorId");

                        Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                            "Updating any error messages"));

                        foreach (XmlNode node in nodeList)
                        {
                            errorId = Convert.ToInt32(node.InnerText);

                            errorMessage = "ID: " + node.InnerText;
                        }

                        nodeList = xmlDoc.DocumentElement.SelectNodes("//ErrorMessage");

                        foreach (XmlNode node in nodeList)
                        {
                            errorMessage += " Message: " + node.InnerText;
                        }

                        if (errorId > 0)
                        {
                            lblError.Text = "Last error: " + errorMessage;
                            lblError.Visible = true;
                        }
                        else
                        {
                            lblError.Visible = false;
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                            string.Format(Messages.SSMASiteStatusFileError, statusFilePath, ex.Message)));

                    }
                }
                else
                {
                    eventStatusList.Add(new EventStatusRow("No status file found", AlertLevel.Unknown, DateTime.MinValue, new TimeSpan(0, 0, 0)));
                    statusFileOK = false;

                    Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                            string.Format(Messages.SSMASiteStatusFileMissing, statusFilePath)));
                }

                // Update the data grid with the latest site status details
                bind.DataSource = eventStatusList;
                dataGridView1.DataSource = bind;
                               

                #region Update system tray icon with alert icon

                // Update the system tray icon if needed with an alert style icon
                serviceController.Refresh();
                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    // If we cant see the status file, or an error has occurred
                    if ((!statusFileOK) || (lblError.Visible))
                    {
                        UpdateNotifyIcon(iconRunningUnknownStatus);
                        UpdateStatusLabel(Messages.SSMAServiceRunningUnknown, iconRunningUnknownStatus);
                        BubbleMessage(ToolTipIcon.Warning, Messages.SSMASiteStatusFileMissingShort, Messages.SSMAServiceRunning, ServiceStatus.RunningUnknown);
                        UpdateServiceStatus(ServiceStatus.RunningUnknown);
                    }
                    else
                    {
                        bool alert = false;

                        foreach (EventStatusRow esr in eventStatusList)
                        {
                            if (esr.AlertLevel == AlertLevel.Red)
                            {
                                string message = DateTime.Now.ToString() + "  " + Messages.SSMAServiceRedAlert + " : " + esr.EventName;

                                UpdateNotifyIcon(iconRunningAlertRed);
                                UpdateStatusLabel(Messages.SSMAServiceRunningRed, iconRunningAlertRed);
                                BubbleMessage(ToolTipIcon.Warning, message, Messages.SSMAServiceRunning, ServiceStatus.RunningRed);
                                UpdateServiceStatus(ServiceStatus.RunningRed);

                                alert = true;
                                break;
                            }
                            else if (esr.AlertLevel == AlertLevel.Amber)
                            {
                                string message = DateTime.Now.ToString() + "  " + Messages.SSMAServiceAmberAlert + " : " + esr.EventName;

                                UpdateNotifyIcon(iconRunningAlertAmber);
                                UpdateStatusLabel(Messages.SSMAServiceRunningAmber, iconRunningAlertAmber);
                                BubbleMessage(ToolTipIcon.Warning, message, Messages.SSMAServiceRunning, ServiceStatus.RunningAmber);
                                UpdateServiceStatus(ServiceStatus.RunningAmber);

                                alert = true;
                                continue;
                            }
                        }

                        if (!alert)
                        {
                            UpdateServiceStatus(ServiceStatus.Running);
                        }
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// Displays bubble messages
        /// </summary>
        /// <param name="state">The tooltip icon state</param>
        /// <param name="message">The message to be displayed</param>
        /// <param name="status">The status message to be displayed</param>
        private void BubbleMessage(ToolTipIcon state, string message, string status, ServiceStatus serviceStatus)
        {
            if (serviceStatusCurrent != serviceStatus)
            {
                notifyIcon.BalloonTipIcon = state;
                notifyIcon.BalloonTipText = message;
                notifyIcon.BalloonTipTitle = applicationName + " is " + status;
                notifyIcon.ShowBalloonTip(3000);
            }
        }

        /// <summary>
        /// Returns the Status filename with directory to read from
        /// </summary>
        /// <returns></returns>
        private string GetFilePath()
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
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Warning,
                            string.Format(Messages.SSMASiteStatusFileMissing,
                            "[SiteStatusLoaderService.StatusFile.Directory][SiteStatusLoaderService.StatusFile.Name]", 
                            ex.Message)));

                return string.Empty;
            }
        }

        #endregion

        /// <summary>
        /// Synchronises the window and menues to the current state of the window
        /// </summary>
        private void SynchronizeWindow()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
            }
            else
            {
                ShowInTaskbar = true;
            }

            this.Visible = ShowInTaskbar;
        }

        #region Dispose, Exit

        /// <summary>
        /// Initiates the dispose on all objects
        /// </summary>
        private void DisposeObjects()
        {
            if (serviceMonitorTimer != null)
            {
                serviceMonitorTimer.Dispose();
            }
            if (contextMenu != null)
            {
                contextMenu.Dispose();
            }
            if (notifyIcon != null)
            {
                notifyIcon.Dispose();
            }
            if (serviceController != null)
            {
                serviceController.Dispose();
            }
            if (bind != null)
            {
                bind.Dispose();
            }
            if (dataGridView1 != null)
            {
                dataGridView1.Dispose();
            }
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="fatalExceptionOccurred">If true, then the application will be forceably closed immediately.</param>
        private void ExitApplication(bool fatalExceptionOccurred)
        {
            DisposeObjects();

            Close();

            if (fatalExceptionOccurred)
            {
                Application.Exit();
                System.Environment.Exit(0);
            }
        }

        #endregion

        #endregion

        #region Event handlers

        /// <summary>
        /// Load event of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceMonitor_Load(object sender, System.EventArgs e)
        {
            SetupTimer();

            // Set the version label
            toolStripStatusVersionNumber.Text = "v " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        #region Exit and Close events

        /// <summary>
        /// Context menu click event for Exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                string.Format(Messages.SSMAExiting, applicationName)));

            ExitApplication(true);
        }

        /// <summary>
        /// Menu item Exit handling click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuItemExit_Click(sender, e);
        }

        /// <summary>
        /// Close (x) button handling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServiceMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                SynchronizeWindow();
            }
        }

        /// <summary>
        /// Menu item Close handling click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            SynchronizeWindow();
        }

        #endregion

        #region Service events

        /// <summary>
        /// Context menu click event for Start service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemStartService_Click(object sender, System.EventArgs e)
        {
            try
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                    string.Format(Messages.SSMAServiceStart, serviceToMonitor)));

                serviceController.Refresh();

                if (serviceController.Status == ServiceControllerStatus.Stopped)
                {
                    serviceController.Start();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                    string.Format(Messages.SSMAServiceStartError, serviceToMonitor, ex.Message)));
            }
        }

        /// <summary>
        /// Context menu click event for Pause service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemPauseService_Click(object sender, System.EventArgs e)
        {
            try
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                    string.Format(Messages.SSMAServicePause, serviceToMonitor)));

                serviceController.Refresh();

                if (serviceController.Status != ServiceControllerStatus.Paused)
                {
                    if (serviceController.CanPauseAndContinue)
                    {
                        serviceController.Pause();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                    string.Format(Messages.SSMAServicePauseError, serviceToMonitor, ex.Message)));
            }
        }

        /// <summary>
        /// Context menu click event for Continue service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemContinueService_Click(object sender, System.EventArgs e)
        {
            try
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                    string.Format(Messages.SSMAServiceContinue, serviceToMonitor)));

                serviceController.Refresh();

                if (serviceController.Status == ServiceControllerStatus.Paused)
                {
                    if (serviceController.CanPauseAndContinue)
                    {
                        serviceController.Continue();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                    string.Format(Messages.SSMAServiceContinueError, serviceToMonitor, ex.Message)));
            }
        }

        /// <summary>
        /// Context menu click event for Stop service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemStopService_Click(object sender, System.EventArgs e)
        {
            try
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                    string.Format(Messages.SSMAServiceStop, serviceToMonitor)));

                serviceController.Refresh();

                if (serviceController.Status == ServiceControllerStatus.Running)
                {
                    if (serviceController.CanStop)
                    {
                        serviceController.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                    string.Format(Messages.SSMAServiceStopError, serviceToMonitor, ex.Message)));
            }
        }

        /// <summary>
        /// Menu item Start handling click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableAllServiceMenus();

            menuItemStartService_Click(sender, e);
        }

        /// <summary>
        /// Menu item Pause handling click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableAllServiceMenus();

            menuItemPauseService_Click(sender, e);
        }

        /// <summary>
        /// Menu item Continue handling click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableAllServiceMenus();

            menuItemContinueService_Click(sender, e);
        }

        /// <summary>
        /// Menu item Stop handling click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableAllServiceMenus();

            menuItemStopService_Click(sender, e);
        }

        #endregion

        #region Service Monitor - Status Form

        /// <summary>
        /// Context meny click event for Double click on Notify Icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_DoubleClick(object sender, System.EventArgs e)
        {
            menuItemStatusForm_Click(sender, e);
        }

        /// <summary>
        /// Context menu click event for Status form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemStatusForm_Click(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }

            SynchronizeWindow();
        }

        /// <summary>
        /// Method which is called by the windows processing. Brings the already running (this)
        /// Monitor Application in to focus, raising it from System Tray.
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == WinApi.WM_SHOWFIRSTINSTANCE)
            {
                this.WindowState = FormWindowState.Normal;
                SynchronizeWindow();
                WinApi.ShowToFront(applicationName);
            }

            base.WndProc(ref message);
        }

        #endregion

        #region Timer events

        /// <summary>
        /// Timer elapsed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serviceMonitorTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                    string.Format(Messages.SSMARunning, serviceToMonitor)));

                #region Update the system tray icon

                UpdateServiceStatusIconAndMenu();

                #endregion

                #region Update the status form
                if (this.InvokeRequired || dataGridView1.InvokeRequired)
                {
                    // Merge again
                    this.BeginInvoke(new SimpleDelegate(UpdateStatusForm));
                }
                else
                {
                    UpdateStatusForm();
                }
                #endregion
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                    string.Format(Messages.SSMAUpdateSiteStatusMonitoringError, serviceToMonitor, ex.Message)));
            }
        }

        /// <summary>
        /// Event handler to apply specific cell format for the datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                // If this is the alert column, change the background colour according to the alert level
                if (e.ColumnIndex == 1)
                {
                    // Check this is the AlertLevel type
                    AlertLevel alertLevel = (AlertLevel)e.Value;

                    switch (alertLevel)
                    {
                        case AlertLevel.Green:
                            e.CellStyle.BackColor = Color.Green;
                            e.CellStyle.SelectionBackColor = Color.Green;
                            break;
                        case AlertLevel.Amber:
                            e.CellStyle.BackColor = Color.Yellow;
                            e.CellStyle.SelectionBackColor = Color.Yellow;
                            break;
                        case AlertLevel.Red:
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.SelectionBackColor = Color.Red;
                            break;
                        default:
                            e.CellStyle.BackColor = Color.White;
                            e.CellStyle.SelectionBackColor = Color.White;
                            break;
                    }
                }
            }
            catch
            {
                // No need to do anything
            }
        }

        #endregion

        #region About form

        /// <summary>
        /// Menu item About form click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm(applicationName))
            {
                aboutForm.ShowDialog(this);
            }
        }

        #endregion

        #region Connection tests

        /// <summary>
        /// Menu item Connection tests click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectionTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ToolsForm toolsForm = new ToolsForm(applicationName))
            {
                toolsForm.ShowDialog(this);
            }
        }

        #endregion

        #region Settings form

        /// <summary>
        /// Menu iten Options click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm(applicationName))
            {
                settingsForm.ShowDialog(this);
            }
        }

        #endregion

        #region Import form

        /// <summary>
        /// Menu iten Import click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ImportForm importForm = new ImportForm(applicationName))
            {
                importForm.ShowDialog(this);
            }
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Class to hold details read in from the status file
    /// </summary>
    public class EventStatusRow
    {
        #region Private members

        private string eventName = string.Empty;
        private AlertLevel alertLevel = AlertLevel.Unknown;
        private DateTime timeSubmitted;
        private TimeSpan duration;

        #endregion

        #region Constructor

        public EventStatusRow(string eventName, AlertLevel alertLevel, DateTime timeSubmitted, TimeSpan duration)
        {
            this.eventName = eventName;
            this.alertLevel = alertLevel;
            this.timeSubmitted = timeSubmitted;
            this.duration = duration;
        }

        #endregion

        #region Public properties

        public string EventName
        {
            get { return eventName; }
        }

        public AlertLevel AlertLevel
        {
            get { return alertLevel; }
        }

        public DateTime TimeSubmitted
        {
            get { return timeSubmitted; }
        }

        public string Duration
        {
            get { return duration.TotalSeconds.ToString(); }
        }

        #endregion

    }

    /// <summary>
    /// Enum of available states for service
    /// </summary>
    public enum ServiceStatus
    {
        Stopped,
        Paused,
        Running,
        RunningUnknown,
        RunningRed,
        RunningAmber
    }

}