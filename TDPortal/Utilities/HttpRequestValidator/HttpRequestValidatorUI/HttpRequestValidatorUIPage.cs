// *********************************************** 
// NAME         : HttpRequestValidatorUIPage.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 28/01/2011
// DESCRIPTION  : Class HttpRequestValidatorUIPage
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/HttpRequestValidator/HttpRequestValidatorUI/HttpRequestValidatorUIPage.cs-arc  $
//
//   Rev 1.3   Mar 04 2011 13:32:06   mmodi
//Added page dimensions to ensure scrollbars are shown
//Resolution for 5671: Http Request Validator IIS tool
//
//   Rev 1.2   Mar 04 2011 10:51:04   mmodi
//Update defaults and UI text
//Resolution for 5671: Http Request Validator IIS tool
//
//   Rev 1.1   Feb 08 2011 09:36:54   mmodi
//Updated to remove configuration sections from web.config during an uninstall
//
//   Rev 1.0   Feb 03 2011 10:15:10   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.Management.Client.Win32;
using Microsoft.Web.Administration;
using Microsoft.Web.Management.Client;
using Microsoft.Web.Management.Server;

using AO.HttpRequestValidatorCommon;

namespace AO.HttpRequestValidatorUI
{
    public sealed class HttpRequestValidatorUIPage : ModulePage
    {
        #region Configuration values

        public bool enabledSwitchConnections;
        public bool enabledSwitchSession;
        public bool enabledSwitchDomainRedirect;
        public int durationMinutes;
        public int thresholdHitsMax;
        public int thresholdHitsRepeatOffender;
        public int thresholdTimeMillisecondsMin;
        public string sessionCookieName;
        public string urlExtensions;
        public string errorPageURL;
        public string responseStatus;
        public string eventLogName;
        public string eventLogSource;
        public string eventLogMachine;
        public string eventLogMessagePrefix;
        public int connectionsMonitorPeriodSeconds;
        public Int64 connectionsTriggerThreshold;
        public Int64 connectionsRestoreThreshold;
        public string connectionsStatusPerfCategory;
        public string connectionsStatusPerfCounter;
        public string connectionsStatusPerfInstance;
        public string connectionsStatusPerfSecondInstance;
        public string connectionsStatusFilePath;
        public string connectionsStatusFileName;
        public string[,] domainRedirectList;
        public bool domainRedirectIncludeUrlExtensions;
        public string coolDownFlagFileLocation;
        public int coolDownReportingIntervalSeconds;
        public string coolDownPerfCounterName;
        public string messageRedirectFlagFileLocation;
        public int messageRedirectReportingIntervalSeconds;
        public string messageRedirectPerfCounterName;

        // Original configuration values
        private bool origEnabledSwitchConnections;
        private bool origEnabledSwitchSession;
        private bool origEnabledSwitchDomainRedirect;
        private int origDurationMinutes;
        private int origThresholdHitsMax;
        private int origThresholdHitsRepeatOffender;
        private int origThresholdTimeMillisecondsMin;
        private string origSessionCookieName;
        private string origURLExtensions;
        private string origErrorPageURL;
        private string origResponseStatus;
        private string origEventLogName;
        private string origEventLogSource;
        private string origEventLogMachine;
        private string origEventLogMessagePrefix;
        private int origConnectionsMonitorPeriodSeconds;
        private Int64 origConnectionsTriggerThreshold;
        private Int64 origConnectionsRestoreThreshold;
        public string origConnectionsStatusPerfCategory;
        public string origConnectionsStatusPerfCounter;
        public string origConnectionsStatusPerfInstance;
        public string origConnectionsStatusPerfSecondInstance;
        private string origConnectionsStatusFilePath;
        private string origConnectionsStatusFileName;
        private string[,] origDomainRedirectList;
        private bool origDomainRedirectIncludeUrlExtensions;
        private string origCoolDownFlagFileLocation;
        private int origCoolDownReportingIntervalSeconds;
        private string origCoolDownPerfCounterName;
        private string origMessageRedirectFlagFileLocation;
        private int origMessageRedirectReportingIntervalSeconds;
        private string origMessageRedirectPerfCounterName;

        // Used to keep track of if values have been changed, so if Cancel is clicked it 
        // doesn't do an unnecessary save 
        private bool valuesChanged = false;

        #endregion

        #region Controls 

        Panel pnlContainer = new Panel();
        Panel pnlError = new Panel();
        Panel pnlEnabled = new Panel();
        Panel pnlConnections = new Panel();
        Panel pnlSession = new Panel();
        Panel pnlDomainRedirect = new Panel();
        Panel pnlCoolDown = new Panel();
        Panel pnlMessageRedirect = new Panel();
        Panel pnlResponse = new Panel();
        Panel pnlEventLog = new Panel();
        Panel pnlButtons = new Panel();
        
        Button cancel = new Button();
        Button apply = new Button();
        Button removeRedirect = new Button();
        Button addRedirect = new Button();
        Button initiateCoolDown = new Button();
        Button initiateMessageRedirect = new Button();
        
        CheckBox chkbxEnabledConnections = new CheckBox();
        CheckBox chkbxEnabledSession = new CheckBox();
        CheckBox chkbxEnabledDomainRedirect = new CheckBox();
        CheckBox chkbxDomainRedirectIncludeUrlParameters = new CheckBox();
        CheckBox chkbxCoolDownIncludeUrlExtensions = new CheckBox();
        CheckBox chkbxMessageRedirectIncludeUrlExtensions = new CheckBox();

        TextBox txtbxDurationMinutes = new TextBox();
        TextBox txtbxSessionCookieName = new TextBox();
        TextBox txtbxURLExtensions = new TextBox();
        TextBox txtbxThresholdHitsMax = new TextBox();
        TextBox txtbxThresholdHitsRepeatOffender = new TextBox();
        TextBox txtbxThresholdTimeMilliSeconds = new TextBox();
        TextBox txtbxResponseStatus = new TextBox();
        TextBox txtbxErrorPageURL = new TextBox();
        TextBox txtbxEventLogName = new TextBox();
        TextBox txtbxEventLogSource = new TextBox();
        TextBox txtbxEventLogMachine = new TextBox();
        TextBox txtbxEventLogMessagePrefix = new TextBox();
        TextBox txtbxConnectionsMonitorPeriodSeconds = new TextBox();
        TextBox txtbxConnectionsTriggerThreshold = new TextBox();
        TextBox txtbxConnectionsRestoreThreshold = new TextBox();
        TextBox txtbxConnectionsPerfCategory = new TextBox();
        TextBox txtbxConnectionsPerfCounter = new TextBox();
        TextBox txtbxConnectionsPerfInstance = new TextBox();
        TextBox txtbxConnectionsPerfSecondInstance = new TextBox();
        TextBox txtbxConnectionsStatusFilePath = new TextBox();
        TextBox txtbxConnectionsStatusFileName = new TextBox();
        TextBox txtbxDomainRedirectAddRedirectFrom = new TextBox();
        TextBox txtbxDomainRedirectAddRedirectTo = new TextBox();
        TextBox txtbxCoolDownFlagFileLocation = new TextBox();
        TextBox txtbxCoolDownReportingIntervalSeconds = new TextBox();
        TextBox txtbxCoolDownRedirectDomain = new TextBox();
        TextBox txtbxMessageRedirectFlagFileLocation = new TextBox();
        TextBox txtbxMessageRedirectReportingIntervalSeconds = new TextBox();
        TextBox txtbxMessageRedirectRedirectUrl = new TextBox();

        ListBox lstRedirects = new ListBox();

        Label lblConnectionsPerfCounter = new Label();

        Label lblError = new Label();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public HttpRequestValidatorUIPage()
        {
            this.Initialize(false);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to set up controls shown on the page
        /// </summary>
        private void Initialize(bool showErrorPanel)
        {
            int panelTop = 25;      // Used to control top position for panels
            int panelTopIncrement = 1;  // Used to control top position for panels
            int rowHeight = 25;     // Used to control heights for panels
            int panelWidth = 530;   // Width for all panels
            int labelLeft = 10;     // Default left position for all labels
            int inputLeft = 270;    // Default left position for all inputs

            // Max page dimensions
            this.Width = 530;
            this.Height = 400;
            this.AutoScroll = true;
            
            #region Panels and Controls

            // Panel containing all the other panels 
            pnlContainer.Width = panelWidth;
            pnlContainer.Height = rowHeight * 58; // Expected max height, update if new rows are added

            #region Error

            if (showErrorPanel)
            {
                pnlError.Top = panelTop * (panelTopIncrement);
                pnlError.Height = rowHeight * 2;        // Panel will contain X rows of labels/inputs
                pnlError.Width = panelWidth;
                pnlError.BorderStyle = BorderStyle.None;
                pnlError.AutoScroll = true;
                panelTopIncrement = panelTopIncrement + 2;

                // Error label
                lblError.Text = string.Empty;
                lblError.AutoSize = true;
                lblError.Left = labelLeft;
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Font = new System.Drawing.Font(lblError.Font, System.Drawing.FontStyle.Bold);

                if (!pnlError.Controls.Contains(lblError))
                    pnlError.Controls.Add(lblError);

                if (!pnlContainer.Controls.Contains(pnlError))
                    pnlContainer.Controls.Add(pnlError);
            }
            else
            {
                if (pnlContainer.Controls.Contains(pnlError))
                    pnlContainer.Controls.Remove(pnlError);
            }

            #endregion

            #region Enabled

            pnlEnabled.Top = panelTop * (panelTopIncrement);
            pnlEnabled.Height = rowHeight * 3;      // Panel will contain X rows of labels/inputs
            pnlEnabled.Width = panelWidth;
            pnlEnabled.BorderStyle = BorderStyle.None;
            panelTopIncrement = panelTopIncrement + 3;

            // Enabled switch
            Label lblEnabled = new Label();
            lblEnabled.Text = "Enable:";
            lblEnabled.AutoSize = true;
            lblEnabled.Left = labelLeft;
            lblEnabled.Font = new System.Drawing.Font(lblEnabled.Font, System.Drawing.FontStyle.Bold);

            Label lblEnableConnections = new Label();
            lblEnableConnections.Text = "Active Connections";
            lblEnableConnections.AutoSize = true;
            lblEnableConnections.Left = labelLeft + 95;

            chkbxEnabledConnections.Left = labelLeft + 200;
            chkbxEnabledConnections.AutoSize = true;

            Label lblEnableSession = new Label();
            lblEnableSession.Text = "Session";
            lblEnableSession.AutoSize = true;
            lblEnableSession.Left = labelLeft + 275;

            chkbxEnabledSession.Left = labelLeft + 320;
            chkbxEnabledSession.AutoSize = true;

            Label lblEnableDomainRedirect = new Label();
            lblEnableDomainRedirect.Text = "Domain Redirect";
            lblEnableDomainRedirect.AutoSize = true;
            lblEnableDomainRedirect.Left = labelLeft + 395;

            chkbxEnabledDomainRedirect.Left = labelLeft + 480;
            chkbxEnabledDomainRedirect.AutoSize = true;

            // URL extensions
            Label lblExtensions = new Label();
            lblExtensions.Text = "URL extensions to check (comma seperated list)";
            lblExtensions.AutoSize = true;
            lblExtensions.Left = labelLeft;
            lblExtensions.Top = 35;

            txtbxURLExtensions.Left = inputLeft;
            txtbxURLExtensions.Top = 35;
            txtbxURLExtensions.Width = 250;
            txtbxURLExtensions.MaxLength = 250;

            pnlEnabled.Controls.Add(lblEnabled);
            pnlEnabled.Controls.Add(lblEnableConnections);
            if (!pnlEnabled.Controls.Contains(chkbxEnabledConnections))
                pnlEnabled.Controls.Add(chkbxEnabledConnections);

            pnlEnabled.Controls.Add(lblEnableSession);
            if (!pnlEnabled.Controls.Contains(chkbxEnabledSession))
                pnlEnabled.Controls.Add(chkbxEnabledSession);

            pnlEnabled.Controls.Add(lblEnableDomainRedirect);
            if (!pnlEnabled.Controls.Contains(chkbxEnabledDomainRedirect))
                pnlEnabled.Controls.Add(chkbxEnabledDomainRedirect);

            pnlEnabled.Controls.Add(lblExtensions);
            if (!pnlEnabled.Controls.Contains(txtbxURLExtensions))
                pnlEnabled.Controls.Add(txtbxURLExtensions);

            if (!pnlContainer.Controls.Contains(pnlEnabled))
                pnlContainer.Controls.Add(pnlEnabled);


            #endregion

            #region Connections

            pnlConnections.Top = panelTop * (panelTopIncrement);
            pnlConnections.Height = rowHeight * 10;     // Panel will contain X rows of labels/inputs
            pnlConnections.Width = panelWidth;
            pnlConnections.BorderStyle = BorderStyle.None;
            panelTopIncrement = panelTopIncrement + 10;

            // Connections
            Label lblConnections = new Label();
            lblConnections.Text = "Active Connections";
            lblConnections.AutoSize = true;
            lblConnections.Left = labelLeft;
            lblConnections.Font = new System.Drawing.Font(lblConnections.Font, System.Drawing.FontStyle.Bold);

            Label lblConnectionsPeriod = new Label();
            lblConnectionsPeriod.Text = "Time period (seconds)";
            lblConnectionsPeriod.AutoSize = true;
            lblConnectionsPeriod.Left = labelLeft;
            lblConnectionsPeriod.Top = 20;

            Label lblConnectionsTrigger = new Label();
            lblConnectionsTrigger.Text = "Trigger level (connections in time period)";
            lblConnectionsTrigger.AutoSize = true;
            lblConnectionsTrigger.Left = labelLeft;
            lblConnectionsTrigger.Top = 45;

            Label lblConnectionsRestore = new Label();
            lblConnectionsRestore.Text = "Restore level (connections in time period)";
            lblConnectionsRestore.AutoSize = true;
            lblConnectionsRestore.Left = labelLeft;
            lblConnectionsRestore.Top = 70;

            Label lblConnectionsPerfCategory = new Label();
            lblConnectionsPerfCategory.Text = "Performance monitor category";
            lblConnectionsPerfCategory.AutoSize = true;
            lblConnectionsPerfCategory.Left = labelLeft;
            lblConnectionsPerfCategory.Top = 95;

            Label lblConnectionsPerfInstance = new Label();
            lblConnectionsPerfInstance.Text = "Performance monitor instance";
            lblConnectionsPerfInstance.AutoSize = true;
            lblConnectionsPerfInstance.Left = labelLeft;
            lblConnectionsPerfInstance.Top = 120;

            Label lblConnectionsPerfSecondInstance = new Label();
            lblConnectionsPerfSecondInstance.Text = "Performance monitor second instance to include";
            lblConnectionsPerfSecondInstance.AutoSize = true;
            lblConnectionsPerfSecondInstance.Left = labelLeft;
            lblConnectionsPerfSecondInstance.Top = 145;

            lblConnectionsPerfCounter = new Label();
            lblConnectionsPerfCounter.Text = string.Empty;
            lblConnectionsPerfCounter.AutoSize = true;
            lblConnectionsPerfCounter.Left = labelLeft;
            lblConnectionsPerfCounter.Top = 170;

            Label lblConnectionStatusFilePath = new Label();
            lblConnectionStatusFilePath.Text = "Status file path (for logging connections data)";
            lblConnectionStatusFilePath.AutoSize = true;
            lblConnectionStatusFilePath.Left = labelLeft;
            lblConnectionStatusFilePath.Top = 195;

            Label lblConnectionStatusFileName = new Label();
            lblConnectionStatusFileName.Text = "Status file name (for logging connections data)";
            lblConnectionStatusFileName.AutoSize = true;
            lblConnectionStatusFileName.Left = labelLeft;
            lblConnectionStatusFileName.Top = 220;

            txtbxConnectionsMonitorPeriodSeconds.Left = inputLeft;
            txtbxConnectionsMonitorPeriodSeconds.Top = 20;
            txtbxConnectionsMonitorPeriodSeconds.Width = 35;
            txtbxConnectionsMonitorPeriodSeconds.MaxLength = 250;
            txtbxConnectionsTriggerThreshold.Left = inputLeft;
            txtbxConnectionsTriggerThreshold.Top = 45;
            txtbxConnectionsTriggerThreshold.Width = 60;
            txtbxConnectionsTriggerThreshold.MaxLength = 250;
            txtbxConnectionsRestoreThreshold.Left = inputLeft;
            txtbxConnectionsRestoreThreshold.Top = 70;
            txtbxConnectionsRestoreThreshold.Width = 60;
            txtbxConnectionsRestoreThreshold.MaxLength = 250;

            txtbxConnectionsPerfCategory.Left = inputLeft;
            txtbxConnectionsPerfCategory.Top = 95;
            txtbxConnectionsPerfCategory.Width = 250;
            txtbxConnectionsPerfCategory.MaxLength = 250;
            txtbxConnectionsPerfInstance.Left = inputLeft;
            txtbxConnectionsPerfInstance.Top = 120;
            txtbxConnectionsPerfInstance.Width = 250;
            txtbxConnectionsPerfInstance.MaxLength = 250;
            txtbxConnectionsPerfSecondInstance.Left = inputLeft;
            txtbxConnectionsPerfSecondInstance.Top = 145;
            txtbxConnectionsPerfSecondInstance.Width = 250;
            txtbxConnectionsPerfSecondInstance.MaxLength = 250;
            txtbxConnectionsPerfCounter.Left = inputLeft;
            txtbxConnectionsPerfCounter.Top = 170;
            txtbxConnectionsPerfCounter.Width = 250;
            txtbxConnectionsPerfCounter.MaxLength = 250;

            // Disable and hide counter textbox, shouldnt allow it to be changed
            txtbxConnectionsPerfCounter.Enabled = false;
            txtbxConnectionsPerfCounter.ReadOnly = true;
            txtbxConnectionsPerfCounter.Visible = false;

            txtbxConnectionsStatusFilePath.Left = inputLeft;
            txtbxConnectionsStatusFilePath.Top = 195;
            txtbxConnectionsStatusFilePath.Width = 250;
            txtbxConnectionsStatusFilePath.MaxLength = 250;
            txtbxConnectionsStatusFileName.Left = inputLeft;
            txtbxConnectionsStatusFileName.Top = 220;
            txtbxConnectionsStatusFileName.Width = 250;
            txtbxConnectionsStatusFileName.MaxLength = 250;

            pnlConnections.Controls.Add(lblConnections);
            pnlConnections.Controls.Add(lblConnectionsPeriod);
            if (!pnlConnections.Controls.Contains(txtbxConnectionsMonitorPeriodSeconds))
                pnlConnections.Controls.Add(txtbxConnectionsMonitorPeriodSeconds);
            pnlConnections.Controls.Add(lblConnectionsTrigger);
            if (!pnlConnections.Controls.Contains(txtbxConnectionsTriggerThreshold))
                pnlConnections.Controls.Add(txtbxConnectionsTriggerThreshold);
            pnlConnections.Controls.Add(lblConnectionsRestore);
            if (!pnlConnections.Controls.Contains(txtbxConnectionsRestoreThreshold))
                pnlConnections.Controls.Add(txtbxConnectionsRestoreThreshold);

            pnlConnections.Controls.Add(lblConnectionsPerfCategory);
            if (!pnlConnections.Controls.Contains(txtbxConnectionsPerfCategory))
                pnlConnections.Controls.Add(txtbxConnectionsPerfCategory);
            pnlConnections.Controls.Add(lblConnectionsPerfCounter);
            if (!pnlConnections.Controls.Contains(txtbxConnectionsPerfCounter))
                pnlConnections.Controls.Add(txtbxConnectionsPerfCounter);
            pnlConnections.Controls.Add(lblConnectionsPerfInstance);
            if (!pnlConnections.Controls.Contains(txtbxConnectionsPerfInstance))
                pnlConnections.Controls.Add(txtbxConnectionsPerfInstance);
            pnlConnections.Controls.Add(lblConnectionsPerfSecondInstance);
            if (!pnlConnections.Controls.Contains(txtbxConnectionsPerfSecondInstance))
                pnlConnections.Controls.Add(txtbxConnectionsPerfSecondInstance);

            pnlConnections.Controls.Add(lblConnectionStatusFilePath);
            if (!pnlConnections.Controls.Contains(txtbxConnectionsStatusFilePath))
                pnlConnections.Controls.Add(txtbxConnectionsStatusFilePath);
            pnlConnections.Controls.Add(lblConnectionStatusFileName);
            if (!pnlConnections.Controls.Contains(txtbxConnectionsStatusFileName))
                pnlConnections.Controls.Add(txtbxConnectionsStatusFileName);

            if (!pnlContainer.Controls.Contains(pnlConnections))
                pnlContainer.Controls.Add(pnlConnections);
            
            #endregion

            #region Session

            pnlSession.Top = panelTop * (panelTopIncrement);
            pnlSession.Height = rowHeight * 9;     // Panel will contain X rows of labels/inputs
            pnlSession.Width = panelWidth;
            pnlSession.BorderStyle = BorderStyle.None;
            panelTopIncrement = panelTopIncrement + 9;

            Label lblSession = new Label();
            lblSession.Text = "Session";
            lblSession.AutoSize = true;
            lblSession.Left = labelLeft;
            lblSession.Font = new System.Drawing.Font(lblSession.Font, System.Drawing.FontStyle.Bold);

            pnlSession.Controls.Add(lblSession);

            // Monitor duration minutes
            Label lblDurationMinutes = new Label();
            lblDurationMinutes.Text = "Sampling period (mins)";
            lblDurationMinutes.AutoSize = true;
            lblDurationMinutes.Left = labelLeft;
            lblDurationMinutes.Top = 20;

            txtbxDurationMinutes.Left = inputLeft;
            txtbxDurationMinutes.Top = 20;
            txtbxDurationMinutes.Width = 35;
            txtbxDurationMinutes.MaxLength = 4;

            pnlSession.Controls.Add(lblDurationMinutes);
            if (!pnlSession.Controls.Contains(txtbxDurationMinutes))
                pnlSession.Controls.Add(txtbxDurationMinutes);

            // Session cookie name
            Label lblSessionCookieName = new Label();
            lblSessionCookieName.Text = "Cookie name containing session ID";
            lblSessionCookieName.AutoSize = true;
            lblSessionCookieName.Left = labelLeft;
            lblSessionCookieName.Top = 45;

            txtbxSessionCookieName.Left = inputLeft;
            txtbxSessionCookieName.Top = 45;
            txtbxSessionCookieName.Width = 250;
            txtbxSessionCookieName.MaxLength = 100;

            pnlSession.Controls.Add(lblSessionCookieName);
            if (!pnlSession.Controls.Contains(txtbxSessionCookieName))
                pnlSession.Controls.Add(txtbxSessionCookieName);

            #region Thresholds
            // Thresholds
            Label lblThresholdsTime = new Label();
            lblThresholdsTime.Text = "Inappropriate hit rate containing same session ID (ms)";
            lblThresholdsTime.AutoSize = true;
            lblThresholdsTime.Left = labelLeft;
            lblThresholdsTime.Top = 70;

            txtbxThresholdTimeMilliSeconds.Left = inputLeft + 190;
            txtbxThresholdTimeMilliSeconds.Top = 70;
            txtbxThresholdTimeMilliSeconds.Width = 60;
            txtbxThresholdTimeMilliSeconds.MaxLength = 8;

            Label lblThresholdsHits = new Label();
            lblThresholdsHits.Text = "Lower limit, rolling block. Number of repeated inappropriate hits before temporary response is invoked. "
                + "User recieves normal response when inappropriate hit rate is no longer breeched. "
                + "(Note: If greater than 'Upper limit' value, the user is straight away put on to the 'Black List' and is not allowed in until "
                + "they stop and the 'Sampling period' expires)";
            lblThresholdsHits.AutoSize = false;
            lblThresholdsHits.Left = labelLeft;
            lblThresholdsHits.Top = 95;
            lblThresholdsHits.Width = 430;
            lblThresholdsHits.Height = 70;

            txtbxThresholdHitsMax.Left = inputLeft + 190;
            txtbxThresholdHitsMax.Top = 95;
            txtbxThresholdHitsMax.Width = 60;
            txtbxThresholdHitsMax.MaxLength = 8;

            Label lblThresholdsHitsRepeat = new Label();
            lblThresholdsHitsRepeat.Text = "Upper limit, permanent block. Number of continued inappropriate hits during temporary response "
                + "before being placed on 'Black list' permanently. User is removed from 'Black list' when they stop and sampling period expires. ";
            lblThresholdsHitsRepeat.AutoSize = false;
            lblThresholdsHitsRepeat.Left = labelLeft;
            lblThresholdsHitsRepeat.Top = 165;
            lblThresholdsHitsRepeat.Width = 430;
            lblThresholdsHitsRepeat.Height = 60;

            txtbxThresholdHitsRepeatOffender.Left = inputLeft + 190;
            txtbxThresholdHitsRepeatOffender.Top = 165;
            txtbxThresholdHitsRepeatOffender.Width = 60;
            txtbxThresholdHitsRepeatOffender.MaxLength = 8;

            pnlSession.Controls.Add(lblThresholdsTime);
            if (!pnlSession.Controls.Contains(txtbxThresholdTimeMilliSeconds))
                pnlSession.Controls.Add(txtbxThresholdTimeMilliSeconds);
            pnlSession.Controls.Add(lblThresholdsHits);
            if (!pnlSession.Controls.Contains(txtbxThresholdHitsMax))
                pnlSession.Controls.Add(txtbxThresholdHitsMax);
            pnlSession.Controls.Add(lblThresholdsHitsRepeat);
            if (!pnlSession.Controls.Contains(txtbxThresholdHitsRepeatOffender))
                pnlSession.Controls.Add(txtbxThresholdHitsRepeatOffender);

            #endregion

            if (!pnlContainer.Controls.Contains(pnlSession))
                pnlContainer.Controls.Add(pnlSession);

            #endregion

            #region Domain redirect

            pnlDomainRedirect.Top = panelTop * (panelTopIncrement);
            pnlDomainRedirect.Height = rowHeight * 7;     // Panel will contain X rows of labels/inputs
            pnlDomainRedirect.Width = panelWidth;
            pnlDomainRedirect.BorderStyle = BorderStyle.None;
            panelTopIncrement = panelTopIncrement + 7;

            Label lblDomainRedirect = new Label();
            lblDomainRedirect.Text = "Domain Redirect";
            lblDomainRedirect.AutoSize = true;
            lblDomainRedirect.Left = labelLeft;
            lblDomainRedirect.Font = new System.Drawing.Font(lblDomainRedirect.Font, System.Drawing.FontStyle.Bold);

            pnlDomainRedirect.Controls.Add(lblDomainRedirect);

            // Domain redirect list
            Label lblRedirectList= new Label();
            lblRedirectList.Text = "The list of domains to redirect, these are processed in order (top to bottom)";
            lblRedirectList.Left = labelLeft;
            lblRedirectList.Width = inputLeft - labelLeft - 10;
            lblRedirectList.Top = 20;
            lblRedirectList.Height = 50;

            lstRedirects.Left = inputLeft;
            lstRedirects.Top = 20;
            lstRedirects.Width = 195;
            lstRedirects.Height = 80;

            removeRedirect.Text = "Remove";
            removeRedirect.Left = inputLeft + 200;
            removeRedirect.Top = 65;
            removeRedirect.Width = 55;
            removeRedirect.Height = 20;
            removeRedirect.BackColor = System.Drawing.Color.LightGray;

            pnlDomainRedirect.Controls.Add(lblRedirectList);

            if (!pnlDomainRedirect.Controls.Contains(lstRedirects))
            {
                pnlDomainRedirect.Controls.Add(lstRedirects);
            }

            if (!pnlDomainRedirect.Controls.Contains(removeRedirect))
            {
                removeRedirect.Click += new EventHandler(this.RemoveClick);
                pnlDomainRedirect.Controls.Add(removeRedirect);
            }

            Label lblAddRedirect = new Label();
            lblAddRedirect.Text = "Add a redirect - redirect from then redirect to (include http://)";
            lblAddRedirect.Width = inputLeft - labelLeft - 10;
            lblAddRedirect.Height = 35;
            lblAddRedirect.Left = labelLeft;
            lblAddRedirect.Top = 100;

            txtbxDomainRedirectAddRedirectFrom.Left = inputLeft;
            txtbxDomainRedirectAddRedirectFrom.Top = 100;
            txtbxDomainRedirectAddRedirectFrom.Width = 95;
            txtbxDomainRedirectAddRedirectFrom.MaxLength = 50;

            txtbxDomainRedirectAddRedirectTo.Left = inputLeft + 100;
            txtbxDomainRedirectAddRedirectTo.Top = 100;
            txtbxDomainRedirectAddRedirectTo.Width = 95;
            txtbxDomainRedirectAddRedirectTo.MaxLength = 50;

            addRedirect.Text = "Add";
            addRedirect.Left = inputLeft + 200;
            addRedirect.Top = 100;
            addRedirect.Width = 55;
            addRedirect.Height = 20;
            addRedirect.BackColor = System.Drawing.Color.LightGray;

            pnlDomainRedirect.Controls.Add(lblAddRedirect);

            if (!pnlDomainRedirect.Controls.Contains(txtbxDomainRedirectAddRedirectFrom))
                pnlDomainRedirect.Controls.Add(txtbxDomainRedirectAddRedirectFrom);

            if (!pnlDomainRedirect.Controls.Contains(txtbxDomainRedirectAddRedirectTo))
                pnlDomainRedirect.Controls.Add(txtbxDomainRedirectAddRedirectTo);

            if (!pnlDomainRedirect.Controls.Contains(addRedirect))
            {
                addRedirect.Click += new EventHandler(this.AddClick);
                pnlDomainRedirect.Controls.Add(addRedirect);
            }

            if (!pnlContainer.Controls.Contains(pnlDomainRedirect))
                pnlContainer.Controls.Add(pnlDomainRedirect);

            Label lblIncParams = new Label();
            lblIncParams.Left = labelLeft;
            lblIncParams.Text = "Include URL extensions when redirecting (ie everything after the domain)";
            lblIncParams.Top = 135;
            lblIncParams.Width = inputLeft - labelLeft - 10;
            lblIncParams.Height = 50;

            chkbxDomainRedirectIncludeUrlParameters.Top = 135;
            chkbxDomainRedirectIncludeUrlParameters.Left = inputLeft;

            pnlDomainRedirect.Controls.Add(lblIncParams);

            if (!pnlDomainRedirect.Controls.Contains(chkbxDomainRedirectIncludeUrlParameters))
                pnlDomainRedirect.Controls.Add(chkbxDomainRedirectIncludeUrlParameters);

            #endregion

            #region Cool down

            pnlCoolDown.Top = panelTop * (panelTopIncrement);
            pnlCoolDown.Height = rowHeight * 8;     // Panel will contain X rows of labels/inputs
            pnlCoolDown.Width = panelWidth;
            pnlCoolDown.BorderStyle = BorderStyle.None;
            panelTopIncrement = panelTopIncrement + 8;

            Label lblCoolDown = new Label();
            lblCoolDown.Text = "Cool Down";
            lblCoolDown.AutoSize = true;
            lblCoolDown.Left = labelLeft;
            lblCoolDown.Font = new System.Drawing.Font(lblDomainRedirect.Font, System.Drawing.FontStyle.Bold);

            pnlCoolDown.Controls.Add(lblCoolDown);

            // Add a line saying what perf counter is being used???

            // Flag file
            Label lblFlagFile = new Label();
            lblFlagFile.Text = "The location of the file that indicates cool down is in progress for this site. This MUST be set immediately after installation.";
            lblFlagFile.Left = labelLeft;
            lblFlagFile.Top = 20;
            lblFlagFile.Height = 50;
            lblFlagFile.Width = inputLeft - labelLeft - 10;

            txtbxCoolDownFlagFileLocation.Top = 20;
            txtbxCoolDownFlagFileLocation.Width = 250;
            txtbxCoolDownFlagFileLocation.MaxLength = 250;
            txtbxCoolDownFlagFileLocation.Left = inputLeft;

            pnlCoolDown.Controls.Add(lblFlagFile);

            if (!pnlCoolDown.Controls.Contains(txtbxCoolDownFlagFileLocation))
                pnlCoolDown.Controls.Add(txtbxCoolDownFlagFileLocation);

            // Reporting interval (seconds)
            Label lblReportingInterval = new Label();
            lblReportingInterval.Left = labelLeft;
            lblReportingInterval.Top = 75;
            lblReportingInterval.Text = "Event log reporting interval for active connections (secs)";
            lblReportingInterval.Width = inputLeft - labelLeft - 10;
            lblReportingInterval.Height = 30;

            txtbxCoolDownReportingIntervalSeconds.TabIndex = 75;
            txtbxCoolDownReportingIntervalSeconds.Left = inputLeft;
            txtbxCoolDownReportingIntervalSeconds.MaxLength = 4;
            txtbxCoolDownReportingIntervalSeconds.Width = 40;
            txtbxCoolDownReportingIntervalSeconds.Top = 75;

            pnlCoolDown.Controls.Add(lblReportingInterval);

            if (!pnlCoolDown.Controls.Contains(txtbxCoolDownReportingIntervalSeconds))
                pnlCoolDown.Controls.Add(txtbxCoolDownReportingIntervalSeconds);

            // Redirect domain
            Label lblRedirectToDomain = new Label();
            lblRedirectToDomain.Left = labelLeft;
            lblRedirectToDomain.Top = 110;
            lblRedirectToDomain.Height = 30;
            lblRedirectToDomain.Width = inputLeft - labelLeft - 10;
            lblRedirectToDomain.Text = "The domain to redirect to during cool down (include http://), transient value";

            txtbxCoolDownRedirectDomain.Left = inputLeft;
            txtbxCoolDownRedirectDomain.Top = 110;
            txtbxCoolDownRedirectDomain.Width = 250;
            txtbxCoolDownRedirectDomain.MaxLength = 250;

            pnlCoolDown.Controls.Add(lblRedirectToDomain);

            if (!pnlCoolDown.Controls.Contains(txtbxCoolDownRedirectDomain))
                pnlCoolDown.Controls.Add(txtbxCoolDownRedirectDomain);

            // Include URL extensions
            Label lblUrlExtensions = new Label();
            lblUrlExtensions.Left = labelLeft;
            lblUrlExtensions.Top = 145;
            lblUrlExtensions.Height = 50;
            lblUrlExtensions.Text = "Include URL extensions when redirecting (ie everything after the domain), transient value";
            lblUrlExtensions.Width =  inputLeft - labelLeft - 10;

            chkbxCoolDownIncludeUrlExtensions.Top = 145;
            chkbxCoolDownIncludeUrlExtensions.Left = inputLeft;
            chkbxCoolDownIncludeUrlExtensions.AutoSize = true;

            pnlCoolDown.Controls.Add(lblUrlExtensions);

            if (!pnlCoolDown.Controls.Contains(chkbxCoolDownIncludeUrlExtensions))
                pnlCoolDown.Controls.Add(chkbxCoolDownIncludeUrlExtensions);

            // Initiate cool down button
            initiateCoolDown.Top = 170;
            initiateCoolDown.Left = inputLeft;
            initiateCoolDown.Text = "Initiate Cool Down";
            initiateCoolDown.AutoSize = true;
            initiateCoolDown.BackColor =  System.Drawing.Color.LightGray;

            if (!pnlCoolDown.Controls.Contains(initiateCoolDown))
            {
                initiateCoolDown.Click += new EventHandler(InitiateCoolDownClick);
                pnlCoolDown.Controls.Add(initiateCoolDown);
            }

            if (!pnlContainer.Controls.Contains(pnlCoolDown))
                pnlContainer.Controls.Add(pnlCoolDown);

            #endregion

            #region Message redirector

            pnlMessageRedirect.Top = panelTop * (panelTopIncrement);
            pnlMessageRedirect.Height = rowHeight * 8;     // Panel will contain X rows of labels/inputs
            pnlMessageRedirect.Width = panelWidth;
            pnlMessageRedirect.BorderStyle = BorderStyle.None;
            panelTopIncrement = panelTopIncrement + 8;

            Label lblMessageRedirect = new Label();
            lblMessageRedirect.Text = "Message Redirector";
            lblMessageRedirect.AutoSize = true;
            lblMessageRedirect.Left = labelLeft;
            lblMessageRedirect.Font = new System.Drawing.Font(lblDomainRedirect.Font, System.Drawing.FontStyle.Bold);

            pnlMessageRedirect.Controls.Add(lblMessageRedirect);

            // Add a line saying what perf counter is being used???

            // Flag file
            Label lblMRFlagFile = new Label();
            lblMRFlagFile.Text = "The location of the file that indicates message redirection is in progress for this site. This MUST be set immediately after installation.";
            lblMRFlagFile.Left = labelLeft;
            lblMRFlagFile.Top = 20;
            lblMRFlagFile.Height = 50;
            lblMRFlagFile.Width = inputLeft - labelLeft - 10;

            txtbxMessageRedirectFlagFileLocation.Top = 20;
            txtbxMessageRedirectFlagFileLocation.Width = 250;
            txtbxMessageRedirectFlagFileLocation.MaxLength = 250;
            txtbxMessageRedirectFlagFileLocation.Left = inputLeft;

            pnlMessageRedirect.Controls.Add(lblMRFlagFile);

            if (!pnlMessageRedirect.Controls.Contains(txtbxMessageRedirectFlagFileLocation))
                pnlMessageRedirect.Controls.Add(txtbxMessageRedirectFlagFileLocation);

            // Reporting interval (seconds)
            Label lblMRReportingInterval = new Label();
            lblMRReportingInterval.Left = labelLeft;
            lblMRReportingInterval.Top = 75;
            lblMRReportingInterval.Text = "Event log reporting interval for active connections (secs)";
            lblMRReportingInterval.Width = inputLeft - labelLeft - 10;
            lblMRReportingInterval.Height = 30;

            txtbxMessageRedirectReportingIntervalSeconds.TabIndex = 75;
            txtbxMessageRedirectReportingIntervalSeconds.Left = inputLeft;
            txtbxMessageRedirectReportingIntervalSeconds.MaxLength = 4;
            txtbxMessageRedirectReportingIntervalSeconds.Width = 40;
            txtbxMessageRedirectReportingIntervalSeconds.Top = 75;

            pnlMessageRedirect.Controls.Add(lblMRReportingInterval);

            if (!pnlMessageRedirect.Controls.Contains(txtbxMessageRedirectReportingIntervalSeconds))
                pnlMessageRedirect.Controls.Add(txtbxMessageRedirectReportingIntervalSeconds);

            // Redirect domain
            Label lblRedirectUrl = new Label();
            lblRedirectUrl.Left = labelLeft;
            lblRedirectUrl.Top = 110;
            lblRedirectUrl.Height = 30;
            lblRedirectUrl.Width = inputLeft - labelLeft - 10;
            lblRedirectUrl.Text = "The URL to redirect to during message redirection (include http://), transient value";

            txtbxMessageRedirectRedirectUrl.Left = inputLeft;
            txtbxMessageRedirectRedirectUrl.Top = 110;
            txtbxMessageRedirectRedirectUrl.Width = 250;
            txtbxMessageRedirectRedirectUrl.MaxLength = 250;

            pnlMessageRedirect.Controls.Add(lblRedirectUrl);

            if (!pnlMessageRedirect.Controls.Contains(txtbxMessageRedirectRedirectUrl))
                pnlMessageRedirect.Controls.Add(txtbxMessageRedirectRedirectUrl);

            // Include URL extensions
            Label lblMRUrlExtensions = new Label();
            lblMRUrlExtensions.Left = labelLeft;
            lblMRUrlExtensions.Top = 145;
            lblMRUrlExtensions.Height = 50;
            lblMRUrlExtensions.Text = "Include URL extensions when redirecting (ie everything after the domain), transient value";
            lblMRUrlExtensions.Width = inputLeft - labelLeft - 10;

            chkbxMessageRedirectIncludeUrlExtensions.Top = 145;
            chkbxMessageRedirectIncludeUrlExtensions.Left = inputLeft;
            chkbxMessageRedirectIncludeUrlExtensions.AutoSize = true;

            pnlMessageRedirect.Controls.Add(lblMRUrlExtensions);

            if (!pnlMessageRedirect.Controls.Contains(chkbxMessageRedirectIncludeUrlExtensions))
                pnlMessageRedirect.Controls.Add(chkbxMessageRedirectIncludeUrlExtensions);

            // Initiate cool down button
            initiateMessageRedirect.Top = 170;
            initiateMessageRedirect.Left = inputLeft;
            initiateMessageRedirect.Text = "Initiate Message Redirection";
            initiateMessageRedirect.AutoSize = true;
            initiateMessageRedirect.BackColor = System.Drawing.Color.LightGray;

            if (!pnlMessageRedirect.Controls.Contains(initiateMessageRedirect))
            {
                initiateMessageRedirect.Click += new EventHandler(InitiateMessageRedirectClick);
                pnlMessageRedirect.Controls.Add(initiateMessageRedirect);
            }

            if (!pnlContainer.Controls.Contains(pnlMessageRedirect))
                pnlContainer.Controls.Add(pnlMessageRedirect);

            #endregion

            #region Response

            pnlResponse.Top = panelTop * (panelTopIncrement);
            pnlResponse.Height = rowHeight * 3;     // Panel will contain X rows of labels/inputs
            pnlResponse.Width = panelWidth;
            pnlResponse.BorderStyle = BorderStyle.None;
            panelTopIncrement = panelTopIncrement + 3;

            // Response
            Label lblResponse = new Label();
            lblResponse.Text = "Response";
            lblResponse.AutoSize = true;
            lblResponse.Left = labelLeft;
            lblResponse.Font = new System.Drawing.Font(lblResponse.Font, System.Drawing.FontStyle.Bold);

            Label lblResponseStatus = new Label();
            lblResponseStatus.Text = "Response status for requests triggering threshold";
            lblResponseStatus.AutoSize = true;
            lblResponseStatus.Left = labelLeft;
            lblResponseStatus.Top = 20;

            Label lblResponseURL = new Label();
            lblResponseURL.Text = "Redirect URL for requests triggering threshold";
            lblResponseURL.AutoSize = true;
            lblResponseURL.Left = labelLeft;
            lblResponseURL.Top = 45;

            txtbxResponseStatus.Left = inputLeft;
            txtbxResponseStatus.Top = 20;
            txtbxResponseStatus.Width = 250;
            txtbxResponseStatus.MaxLength = 100;
            txtbxErrorPageURL.Left = inputLeft;
            txtbxErrorPageURL.Top = 45;
            txtbxErrorPageURL.Width = 250;
            txtbxErrorPageURL.MaxLength = 250;

            pnlResponse.Controls.Add(lblResponse);
            pnlResponse.Controls.Add(lblResponseStatus);
            if (!pnlResponse.Controls.Contains(txtbxResponseStatus))
                pnlResponse.Controls.Add(txtbxResponseStatus);
            pnlResponse.Controls.Add(lblResponseURL);
            if (!pnlResponse.Controls.Contains(txtbxErrorPageURL))
                pnlResponse.Controls.Add(txtbxErrorPageURL);

            if (!pnlContainer.Controls.Contains(pnlResponse))
                pnlContainer.Controls.Add(pnlResponse);
            
            #endregion

            #region Event log

            pnlEventLog.Top = panelTop * (panelTopIncrement);
            pnlEventLog.Height = rowHeight * 5;     // Panel will contain X rows of labels/inputs
            pnlEventLog.Width = panelWidth;
            pnlEventLog.BorderStyle = BorderStyle.None;
            panelTopIncrement = panelTopIncrement + 5;

            // Event Log
            Label lblEventLog = new Label();
            lblEventLog.Text = "Event Log";
            lblEventLog.AutoSize = true;
            lblEventLog.Left = labelLeft;
            lblEventLog.Font = new System.Drawing.Font(lblEventLog.Font, System.Drawing.FontStyle.Bold);

            Label lblEventLogName = new Label();
            lblEventLogName.Text = "Name (event log to write to)";
            lblEventLogName.AutoSize = true;
            lblEventLogName.Left = labelLeft;
            lblEventLogName.Top = 20;

            Label lblEventLogMachine = new Label();
            lblEventLogMachine.Text = "Machine (use '.' for local)";
            lblEventLogMachine.AutoSize = true;
            lblEventLogMachine.Left = labelLeft;
            lblEventLogMachine.Top = 45;

            Label lblEventLogSource = new Label();
            lblEventLogSource.Text = "Source (application which logs the event)";
            lblEventLogSource.AutoSize = true;
            lblEventLogSource.Left = labelLeft;
            lblEventLogSource.Top = 70;

            Label lblEventLogMessagePrefix = new Label();
            lblEventLogMessagePrefix.Text = "Message Prefix (string to prefix to all messages)";
            lblEventLogMessagePrefix.AutoSize = true;
            lblEventLogMessagePrefix.Left = labelLeft;
            lblEventLogMessagePrefix.Top = 95;

            txtbxEventLogName.Left = inputLeft;
            txtbxEventLogName.Top = 20;
            txtbxEventLogName.Width = 250;
            txtbxEventLogName.MaxLength = 250;
            txtbxEventLogMachine.Left = inputLeft;
            txtbxEventLogMachine.Top = 45;
            txtbxEventLogMachine.Width = 250;
            txtbxEventLogMachine.MaxLength = 250;
            txtbxEventLogSource.Left = inputLeft;
            txtbxEventLogSource.Top = 70;
            txtbxEventLogSource.Width = 250;
            txtbxEventLogSource.MaxLength = 250;
            txtbxEventLogMessagePrefix.Left = inputLeft;
            txtbxEventLogMessagePrefix.Top = 95;
            txtbxEventLogMessagePrefix.Width = 250;
            txtbxEventLogMessagePrefix.MaxLength = 250;

            pnlEventLog.Controls.Add(lblEventLog);
            pnlEventLog.Controls.Add(lblEventLogName);
            if (!pnlEventLog.Controls.Contains(txtbxEventLogName))
                pnlEventLog.Controls.Add(txtbxEventLogName);
            pnlEventLog.Controls.Add(lblEventLogMachine);
            if (!pnlEventLog.Controls.Contains(txtbxEventLogMachine))
                pnlEventLog.Controls.Add(txtbxEventLogMachine);
            pnlEventLog.Controls.Add(lblEventLogSource);
            if (!pnlEventLog.Controls.Contains(txtbxEventLogSource))
                pnlEventLog.Controls.Add(txtbxEventLogSource);
            pnlEventLog.Controls.Add(lblEventLogMessagePrefix);
            if (!pnlEventLog.Controls.Contains(txtbxEventLogMessagePrefix))
                pnlEventLog.Controls.Add(txtbxEventLogMessagePrefix);

            if (!pnlContainer.Controls.Contains(pnlEventLog))
                pnlContainer.Controls.Add(pnlEventLog);
            
            #endregion

            #region Buttons

            panelTopIncrement = panelTopIncrement + 1; // Allow extra space between panel above
            pnlButtons.Top = panelTop * (panelTopIncrement);
            pnlButtons.Height = rowHeight;
            pnlButtons.Width = panelWidth;
            pnlButtons.BorderStyle = BorderStyle.None;
            panelTopIncrement++;

            string version = string.Empty;
            try
            {
                version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            catch
            {
                // Nothing to do
            }

            Label lblVersion = new Label();
            lblVersion.Text = "v" + version;
            lblVersion.AutoSize = true;
            lblVersion.Left = labelLeft;
            lblVersion.Font = new System.Drawing.Font(lblVersion.Font, System.Drawing.FontStyle.Regular);
            
            apply.Text = "Apply";
            apply.Left = inputLeft;
            apply.AutoSize = true;
            apply.BackColor = System.Drawing.Color.LightGray;
            

            cancel.Text = "Cancel";
            cancel.Left = inputLeft + 100;
            cancel.AutoSize = true;
            cancel.BackColor = System.Drawing.Color.LightGray;

            pnlButtons.Controls.Add(lblVersion);
            if (!pnlButtons.Controls.Contains(apply))
            {
                apply.Click += new EventHandler(this.applyClick);
                pnlButtons.Controls.Add(apply);
            }
            if (!pnlButtons.Controls.Contains(cancel))
            {
                cancel.Click += new EventHandler(this.cancelClick);
                pnlButtons.Controls.Add(cancel);
            }

            if (!pnlContainer.Controls.Contains(pnlButtons))
                pnlContainer.Controls.Add(pnlButtons);
            
            #endregion

            #endregion

            if(!Controls.Contains(pnlContainer))
                Controls.Add(pnlContainer);

        }

        /// <summary>
        /// Method to update controls with configuration variable values
        /// </summary>
        private void UpdateUI()
        {
            chkbxEnabledConnections.Checked = enabledSwitchConnections;
            chkbxEnabledSession.Checked = enabledSwitchSession;
            chkbxEnabledDomainRedirect.Checked = enabledSwitchDomainRedirect;
            txtbxDurationMinutes.Text = durationMinutes.ToString();
            txtbxSessionCookieName.Text = sessionCookieName;
            txtbxURLExtensions.Text = urlExtensions;
            txtbxThresholdTimeMilliSeconds.Text = thresholdTimeMillisecondsMin.ToString();
            txtbxThresholdHitsMax.Text = thresholdHitsMax.ToString();
            txtbxThresholdHitsRepeatOffender.Text = thresholdHitsRepeatOffender.ToString();
            txtbxResponseStatus.Text = responseStatus;
            txtbxErrorPageURL.Text = errorPageURL;
            txtbxEventLogName.Text = eventLogName;
            txtbxEventLogMachine.Text = eventLogMachine;
            txtbxEventLogSource.Text = eventLogSource;
            txtbxEventLogMessagePrefix.Text = eventLogMessagePrefix;
            txtbxConnectionsMonitorPeriodSeconds.Text = connectionsMonitorPeriodSeconds.ToString();
            txtbxConnectionsTriggerThreshold.Text = connectionsTriggerThreshold.ToString();
            txtbxConnectionsRestoreThreshold.Text = connectionsRestoreThreshold.ToString();
            txtbxConnectionsPerfCategory.Text = connectionsStatusPerfCategory;
            txtbxConnectionsPerfCounter.Text = connectionsStatusPerfCounter;
            txtbxConnectionsPerfInstance.Text = connectionsStatusPerfInstance;
            txtbxConnectionsPerfSecondInstance.Text = connectionsStatusPerfSecondInstance;
            txtbxConnectionsStatusFilePath.Text = connectionsStatusFilePath;
            txtbxConnectionsStatusFileName.Text = connectionsStatusFileName;

            lblConnectionsPerfCounter.Text = "Performance monitor counter " + origConnectionsStatusPerfCounter + " will be monitored for the selected category and instance";

            chkbxDomainRedirectIncludeUrlParameters.Checked = domainRedirectIncludeUrlExtensions;

            lstRedirects.Items.Clear();
            for (int i = 0; i <= domainRedirectList.GetUpperBound(0); i++)
            {
                string item = domainRedirectList[i, 0] + " to " + domainRedirectList[i, 1];
                lstRedirects.Items.Add(item);
            }

            txtbxCoolDownFlagFileLocation.Text = coolDownFlagFileLocation;
            txtbxCoolDownReportingIntervalSeconds.Text = coolDownReportingIntervalSeconds.ToString();
            txtbxMessageRedirectFlagFileLocation.Text = messageRedirectFlagFileLocation;
            txtbxMessageRedirectReportingIntervalSeconds.Text = messageRedirectReportingIntervalSeconds.ToString();
        }

        /// <summary>
        /// Method to update the configuration variable values from the control values
        /// </summary>
        public void UpdateVariables(bool useOriginalValues)
        {
            if (!useOriginalValues)
            {
                enabledSwitchConnections = chkbxEnabledConnections.Checked;
                enabledSwitchSession = chkbxEnabledSession.Checked;
                enabledSwitchDomainRedirect = chkbxEnabledDomainRedirect.Checked;
                sessionCookieName = txtbxSessionCookieName.Text;
                urlExtensions = txtbxURLExtensions.Text;
                responseStatus = txtbxResponseStatus.Text;
                errorPageURL = txtbxErrorPageURL.Text;
                eventLogName = txtbxEventLogName.Text;
                eventLogMachine = txtbxEventLogMachine.Text;
                eventLogSource = txtbxEventLogSource.Text;
                eventLogMessagePrefix = txtbxEventLogMessagePrefix.Text;
                connectionsStatusPerfCategory = txtbxConnectionsPerfCategory.Text;
                connectionsStatusPerfCounter = txtbxConnectionsPerfCounter.Text;
                connectionsStatusPerfInstance = txtbxConnectionsPerfInstance.Text;
                connectionsStatusPerfSecondInstance = txtbxConnectionsPerfSecondInstance.Text;
                connectionsStatusFilePath = txtbxConnectionsStatusFilePath.Text;
                connectionsStatusFileName = txtbxConnectionsStatusFileName.Text;
                domainRedirectIncludeUrlExtensions = chkbxDomainRedirectIncludeUrlParameters.Checked;
                coolDownFlagFileLocation = txtbxCoolDownFlagFileLocation.Text;
                coolDownReportingIntervalSeconds = Convert.ToInt32(txtbxCoolDownReportingIntervalSeconds.Text);
                messageRedirectReportingIntervalSeconds = Convert.ToInt32(txtbxMessageRedirectReportingIntervalSeconds.Text);
                messageRedirectFlagFileLocation = txtbxMessageRedirectFlagFileLocation.Text;

                #region Read validated values
                // Validated values
                int parsedValue;
                Int64 parsedValue64;
                bool throwError = false;

                if ((Int32.TryParse(txtbxDurationMinutes.Text, out parsedValue)) && (parsedValue >= 0))
                {
                    durationMinutes = parsedValue;
                }
                else
                {
                    ShowControlError(txtbxDurationMinutes, false);
                    throwError = true;
                }

                if ((Int32.TryParse(txtbxThresholdTimeMilliSeconds.Text, out parsedValue)) && (parsedValue >= 0))
                {
                    thresholdTimeMillisecondsMin = parsedValue;
                }
                else
                {
                    ShowControlError(txtbxThresholdTimeMilliSeconds, false);
                    throwError = true;
                }

                if ((Int32.TryParse(txtbxThresholdHitsMax.Text, out parsedValue)) && (parsedValue >= 0))
                {
                    thresholdHitsMax = parsedValue;
                }
                else
                {
                    ShowControlError(txtbxThresholdHitsMax, false);
                    throwError = true;
                }

                if ((Int32.TryParse(txtbxThresholdHitsRepeatOffender.Text, out parsedValue)) && (parsedValue >= 0))
                {
                    thresholdHitsRepeatOffender = parsedValue;
                }
                else
                {
                    ShowControlError(txtbxThresholdHitsRepeatOffender, false);
                    throwError = true;
                }

                if ((Int32.TryParse(txtbxConnectionsMonitorPeriodSeconds.Text, out parsedValue)) && (parsedValue >= 0))
                {
                    connectionsMonitorPeriodSeconds = parsedValue;
                }
                else
                {
                    ShowControlError(txtbxConnectionsMonitorPeriodSeconds, false);
                    throwError = true;
                }

                if ((Int64.TryParse(txtbxConnectionsTriggerThreshold.Text, out parsedValue64)) && (parsedValue64 >= 0))
                {
                    connectionsTriggerThreshold = parsedValue64;
                }
                else
                {
                    ShowControlError(txtbxConnectionsTriggerThreshold, false);
                    throwError = true;
                }

                if ((Int64.TryParse(txtbxConnectionsRestoreThreshold.Text, out parsedValue64)) && (parsedValue64 >= 0))
                {
                    connectionsRestoreThreshold = parsedValue64;
                }
                else
                {
                    ShowControlError(txtbxConnectionsRestoreThreshold, false);
                    throwError = true;
                }

                if (throwError)
                {
                    throw new Exception("One or more fields contained an invalid value, please check and retry");
                }
                #endregion
            }
            else
            {
                enabledSwitchConnections = origEnabledSwitchConnections;
                enabledSwitchSession = origEnabledSwitchSession;
                enabledSwitchDomainRedirect = origEnabledSwitchDomainRedirect;
                durationMinutes = origDurationMinutes;
                thresholdHitsMax = origThresholdHitsMax;
                thresholdHitsRepeatOffender = origThresholdHitsRepeatOffender;
                thresholdTimeMillisecondsMin = origThresholdTimeMillisecondsMin;
                sessionCookieName = origSessionCookieName;
                urlExtensions = origURLExtensions;
                errorPageURL = origErrorPageURL;
                responseStatus = origResponseStatus;
                eventLogName = origEventLogName;
                eventLogSource = origEventLogSource;
                eventLogMachine = origEventLogMachine;
                eventLogMessagePrefix = origEventLogMessagePrefix;
                connectionsMonitorPeriodSeconds = origConnectionsMonitorPeriodSeconds;
                connectionsTriggerThreshold = origConnectionsTriggerThreshold;
                connectionsRestoreThreshold = origConnectionsRestoreThreshold;
                connectionsStatusPerfCategory = origConnectionsStatusPerfCategory;
                connectionsStatusPerfCounter = origConnectionsStatusPerfCounter;
                connectionsStatusPerfInstance = origConnectionsStatusPerfInstance;
                connectionsStatusPerfSecondInstance = origConnectionsStatusPerfSecondInstance;
                connectionsStatusFilePath = origConnectionsStatusFilePath;
                connectionsStatusFileName = origConnectionsStatusFileName;
                domainRedirectList = origDomainRedirectList;
                domainRedirectIncludeUrlExtensions = origDomainRedirectIncludeUrlExtensions;
                coolDownFlagFileLocation = origCoolDownFlagFileLocation;
                coolDownReportingIntervalSeconds = origCoolDownReportingIntervalSeconds;
                coolDownPerfCounterName = origCoolDownPerfCounterName;
                messageRedirectReportingIntervalSeconds = origMessageRedirectReportingIntervalSeconds;
                messageRedirectFlagFileLocation = origMessageRedirectFlagFileLocation;
                messageRedirectPerfCounterName = origMessageRedirectPerfCounterName;
            }
        }

        /// <summary>
        /// Method to set the original configuration variables from the current values
        /// </summary>
        public void UpdateOriginalValues()
        {
            origEnabledSwitchConnections = enabledSwitchConnections;
            origEnabledSwitchSession = enabledSwitchSession;
            origEnabledSwitchDomainRedirect = enabledSwitchDomainRedirect;
            origDurationMinutes = durationMinutes;
            origThresholdHitsMax = thresholdHitsMax;
            origThresholdHitsRepeatOffender = thresholdHitsRepeatOffender;
            origThresholdTimeMillisecondsMin = thresholdTimeMillisecondsMin;
            origSessionCookieName = sessionCookieName;
            origURLExtensions = urlExtensions;
            origErrorPageURL = errorPageURL;
            origResponseStatus = responseStatus;
            origEventLogName = eventLogName;
            origEventLogSource = eventLogSource;
            origEventLogMachine = eventLogMachine;
            origEventLogMessagePrefix = eventLogMessagePrefix;
            origConnectionsMonitorPeriodSeconds = connectionsMonitorPeriodSeconds;
            origConnectionsTriggerThreshold = connectionsTriggerThreshold;
            origConnectionsRestoreThreshold = connectionsRestoreThreshold;
            origConnectionsStatusPerfCategory = connectionsStatusPerfCategory;
            origConnectionsStatusPerfCounter = connectionsStatusPerfCounter;
            origConnectionsStatusPerfInstance = connectionsStatusPerfInstance;
            origConnectionsStatusPerfSecondInstance = connectionsStatusPerfSecondInstance;
            origConnectionsStatusFilePath = connectionsStatusFilePath;
            origConnectionsStatusFileName = connectionsStatusFileName;
            origDomainRedirectIncludeUrlExtensions = domainRedirectIncludeUrlExtensions;
            origDomainRedirectList = domainRedirectList;
            origCoolDownReportingIntervalSeconds = coolDownReportingIntervalSeconds;
            origCoolDownFlagFileLocation = coolDownFlagFileLocation;
            origCoolDownPerfCounterName = coolDownPerfCounterName;
            origMessageRedirectFlagFileLocation = messageRedirectFlagFileLocation;
            origMessageRedirectReportingIntervalSeconds = messageRedirectReportingIntervalSeconds;
            origMessageRedirectPerfCounterName = messageRedirectPerfCounterName;
        }
        
        /// <summary>
        /// Method to populate the configuration variables from the Config file
        /// </summary>
        public void ReadConfig()
        {
            ClearErrors();

            try
            {
                using (ServerManager mgr = new ServerManager())
                {
                    ConfigurationSection section;

                    Configuration config = mgr.GetWebConfiguration(
                        Connection.ConfigurationPath.SiteName,
                        Connection.ConfigurationPath.ApplicationPath + Connection.ConfigurationPath.FolderPath
                        );


                    section = config.GetSection(HttpRequestValidatorKeys.ConfigurationSection);

                    // Read configuration values
                    enabledSwitchConnections = (bool)section.GetAttribute(HttpRequestValidatorKeys.SwitchConnections).Value;
                    enabledSwitchSession = (bool)section.GetAttribute(HttpRequestValidatorKeys.SwitchSession).Value;
                    enabledSwitchDomainRedirect = (bool)section.GetAttribute(HttpRequestValidatorKeys.SwitchDomainRedirect).Value;

                    durationMinutes = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Monitor).GetAttribute(HttpRequestValidatorKeys.Duration_Minutes).Value;

                    thresholdHitsMax = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Thresholds).GetAttribute(HttpRequestValidatorKeys.Threshold_Hits_Max).Value;
                    thresholdHitsRepeatOffender = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Thresholds).GetAttribute(HttpRequestValidatorKeys.Threshold_Hits_RepeatOffender).Value;
                    thresholdTimeMillisecondsMin = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Thresholds).GetAttribute(HttpRequestValidatorKeys.Threshold_Time_Milliseconds_Min).Value;

                    sessionCookieName = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Session).GetAttribute(HttpRequestValidatorKeys.SessionCookieName).Value;

                    urlExtensions = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Extensions).GetAttribute(HttpRequestValidatorKeys.URL_Extensions).Value;

                    errorPageURL = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Response).GetAttribute(HttpRequestValidatorKeys.ErrorPage_URL).Value;
                    responseStatus = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Response).GetAttribute(HttpRequestValidatorKeys.ResponseStatus).Value;

                    eventLogName = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Name).Value;
                    eventLogSource = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Source).Value;
                    eventLogMachine = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Machine).Value;
                    eventLogMessagePrefix = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_MessagePrefix).Value;

                    connectionsMonitorPeriodSeconds = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_MonitorPeriodSeconds).Value;
                    connectionsTriggerThreshold = Convert.ToInt64((Int64)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_TriggerThreshold).Value);
                    connectionsRestoreThreshold = Convert.ToInt64((Int64)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_RestoreThreshold).Value);
                    connectionsStatusPerfCategory = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_CategoryName).Value;
                    connectionsStatusPerfCounter = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_CounterName).Value;
                    connectionsStatusPerfSecondInstance = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_SecondInstanceName).Value;
                    connectionsStatusPerfInstance = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_InstanceName).Value;
                    connectionsStatusFilePath = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_StatusFilePath).Value;
                    connectionsStatusFileName = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_StatusFileName).Value;

                    domainRedirectIncludeUrlExtensions = Convert.ToBoolean(section.GetChildElement(HttpRequestValidatorKeys.Element_DomainRedirects).GetAttribute(HttpRequestValidatorKeys.DomainRedirects_IncludeExtensions).Value);
                    string[] redirects = section.GetChildElement(HttpRequestValidatorKeys.Element_DomainRedirects).GetAttribute(HttpRequestValidatorKeys.DomainRedirects_RedirectList).Value.ToString().Split(';');

                    if (redirects[0] == string.Empty)
                    {
                        domainRedirectList = new string[0, 0];
                    }
                    else
                    {
                        domainRedirectList = new string[redirects.Length, 2];

                        for (int i = 0; i < redirects.Length; i++)
                        {
                            domainRedirectList[i, 0] = redirects[i].Split('!')[0];
                            domainRedirectList[i, 1] = redirects[i].Split('!')[1];
                        }
                    }

                    coolDownFlagFileLocation = section.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_FileLocation).Value.ToString();
                    coolDownReportingIntervalSeconds = Convert.ToInt32(section.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_ReportInterval).Value);
                    coolDownPerfCounterName = section.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_PerformanceCounterName).Value.ToString();

                    messageRedirectFlagFileLocation = section.GetChildElement(HttpRequestValidatorKeys.Element_MessageRedirector).GetAttribute(HttpRequestValidatorKeys.MessageRedirector_FileLocation).Value.ToString();
                    messageRedirectReportingIntervalSeconds = Convert.ToInt32(section.GetChildElement(HttpRequestValidatorKeys.Element_MessageRedirector).GetAttribute(HttpRequestValidatorKeys.MessageRedirector_ReportInterval).Value);
                    messageRedirectPerfCounterName = section.GetChildElement(HttpRequestValidatorKeys.Element_MessageRedirector).GetAttribute(HttpRequestValidatorKeys.MessageRedirector_PerformanceCounterName).Value.ToString();
                }
            }
            catch (Exception ex)
            {
               ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Method to update the ConfigurationSection with the configuration variables values
        /// </summary>
        /// <param name="section"></param>
        private void UpdateConfigValues(ConfigurationSection section)
        {
            // Update configuration values
            section.GetAttribute(HttpRequestValidatorKeys.SwitchConnections).Value = (object)enabledSwitchConnections;
            section.GetAttribute(HttpRequestValidatorKeys.SwitchSession).Value = (object)enabledSwitchSession;
            section.GetAttribute(HttpRequestValidatorKeys.SwitchDomainRedirect).Value = (object)enabledSwitchDomainRedirect;
            
            section.GetChildElement(HttpRequestValidatorKeys.Element_Monitor).GetAttribute(HttpRequestValidatorKeys.Duration_Minutes).Value = (object)durationMinutes;

            section.GetChildElement(HttpRequestValidatorKeys.Element_Thresholds).GetAttribute(HttpRequestValidatorKeys.Threshold_Hits_Max).Value = (object)thresholdHitsMax;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Thresholds).GetAttribute(HttpRequestValidatorKeys.Threshold_Hits_RepeatOffender).Value = (object)thresholdHitsRepeatOffender;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Thresholds).GetAttribute(HttpRequestValidatorKeys.Threshold_Time_Milliseconds_Min).Value = (object)thresholdTimeMillisecondsMin;

            section.GetChildElement(HttpRequestValidatorKeys.Element_Session).GetAttribute(HttpRequestValidatorKeys.SessionCookieName).Value = (object)sessionCookieName;

            section.GetChildElement(HttpRequestValidatorKeys.Element_Extensions).GetAttribute(HttpRequestValidatorKeys.URL_Extensions).Value = (object)urlExtensions;

            section.GetChildElement(HttpRequestValidatorKeys.Element_Response).GetAttribute(HttpRequestValidatorKeys.ErrorPage_URL).Value = (object)errorPageURL;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Response).GetAttribute(HttpRequestValidatorKeys.ResponseStatus).Value = (object)responseStatus;

            section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Name).Value = (object)eventLogName;
            section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Source).Value = (object)eventLogSource;
            section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Machine).Value = (object)eventLogMachine;
            section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_MessagePrefix).Value = (object)eventLogMessagePrefix;

            section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_MonitorPeriodSeconds).Value = (object)connectionsMonitorPeriodSeconds;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_TriggerThreshold).Value = (object)connectionsTriggerThreshold;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_RestoreThreshold).Value = (object)connectionsRestoreThreshold;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_CategoryName).Value = (object)connectionsStatusPerfCategory;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_CounterName).Value = (object)connectionsStatusPerfCounter;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_InstanceName).Value = (object)connectionsStatusPerfInstance;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_PerformanceCounter_SecondInstanceName).Value = (object)connectionsStatusPerfSecondInstance;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_StatusFilePath).Value = (object)connectionsStatusFilePath;
            section.GetChildElement(HttpRequestValidatorKeys.Element_Connections).GetAttribute(HttpRequestValidatorKeys.Connections_StatusFileName).Value = (object)connectionsStatusFileName;

            section.GetChildElement(HttpRequestValidatorKeys.Element_DomainRedirects).GetAttribute(HttpRequestValidatorKeys.DomainRedirects_IncludeExtensions).Value = (object)domainRedirectIncludeUrlExtensions;
            string redirects = string.Empty;
            for (int i = 0; i <= domainRedirectList.GetUpperBound(0); i++)
            {
                redirects += domainRedirectList[i, 0] + "!" + domainRedirectList[i, 1];
                if (i < domainRedirectList.GetUpperBound(0))
                {
                    redirects += ";";
                }
            }
            section.GetChildElement(HttpRequestValidatorKeys.Element_DomainRedirects).GetAttribute(HttpRequestValidatorKeys.DomainRedirects_RedirectList).Value = (object)redirects;

            section.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_FileLocation).Value = (object)coolDownFlagFileLocation;
            section.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_ReportInterval).Value = (object)coolDownReportingIntervalSeconds;
            section.GetChildElement(HttpRequestValidatorKeys.Element_CoolDown).GetAttribute(HttpRequestValidatorKeys.CoolDown_PerformanceCounterName).Value = (object)coolDownPerfCounterName;

            section.GetChildElement(HttpRequestValidatorKeys.Element_MessageRedirector).GetAttribute(HttpRequestValidatorKeys.MessageRedirector_FileLocation).Value = (object)messageRedirectFlagFileLocation;
            section.GetChildElement(HttpRequestValidatorKeys.Element_MessageRedirector).GetAttribute(HttpRequestValidatorKeys.MessageRedirector_ReportInterval).Value = (object)messageRedirectReportingIntervalSeconds;
            section.GetChildElement(HttpRequestValidatorKeys.Element_MessageRedirector).GetAttribute(HttpRequestValidatorKeys.MessageRedirector_PerformanceCounterName).Value = (object)messageRedirectPerfCounterName;
        }

        /// <summary>
        /// Method to display an error on the page
        /// </summary>
        /// <param name="message"></param>
        private void ShowError(string message)
        {
            if (message != null)
            {
                Initialize(true);
                lblError.Text = message;
                this.pnlContainer.Focus();
            }
            else
            {
                Initialize(false);
                lblError.Text = string.Empty;
            }
        }

        /// <summary>
        /// Method to update a control to show error colours
        /// </summary>
        /// <param name="control"></param>
        private void ShowControlError(Control control, bool reset)
        {
            if (control is TextBox)
            {
                TextBox txtbx = (TextBox)control;

                if (!reset)
                {
                    txtbx.BackColor = System.Drawing.Color.LightPink;
                }
                else
                {
                    txtbx.BackColor = System.Drawing.Color.White;
                }
            }
        }

        /// <summary>
        /// Method to clear all relevant controls from any displayed error messages/styles
        /// </summary>
        private void ClearErrors()
        {
            ShowError(null);
            ShowControlError(txtbxDurationMinutes, true);
            ShowControlError(txtbxThresholdTimeMilliSeconds, true);
            ShowControlError(txtbxThresholdHitsMax, true);
            ShowControlError(txtbxThresholdHitsRepeatOffender, true);
            ShowControlError(txtbxConnectionsMonitorPeriodSeconds, true);
            ShowControlError(txtbxConnectionsTriggerThreshold, true);
            ShowControlError(txtbxConnectionsRestoreThreshold, true);
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// OnActivated method event
        /// </summary>
        /// <param name="initialActivation"></param>
        protected override void OnActivated(bool initialActivation)
        {
            base.OnActivated(initialActivation);
            if (initialActivation)
            {
                ReadConfig();
                UpdateOriginalValues();
                UpdateUI();
            }
        }

        /// <summary>
        /// Apply button click event. Saves configuration variables back to the Config file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyClick(Object sender, EventArgs e)
        {
            valuesChanged = true;

            ClearErrors();

            if(DialogResult.Yes == MessageBox.Show("Clicking apply will write to web.config, which will cause IIS to restart. Do you want to continue?", "Apply", MessageBoxButtons.YesNo))
            {
                // The flag files for cool down and message redirect must be different
                string coolDownFlag = txtbxCoolDownFlagFileLocation.Text;
                string messageRedirectFlag = txtbxMessageRedirectFlagFileLocation.Text;

                if ((coolDownFlag == messageRedirectFlag) & ((coolDownFlag != string.Empty) & (messageRedirectFlag != string.Empty)))
                {
                    ShowError("The flag file locations for cool down and message redirection MUST be different");
                    return;
                }

                try
                {
                    // Read current values
                    UpdateVariables(false);

                    using (ServerManager mgr = new ServerManager())
                    {
                        ConfigurationSection section;

                        Configuration config = mgr.GetWebConfiguration(
                            Connection.ConfigurationPath.SiteName,
                            Connection.ConfigurationPath.ApplicationPath + Connection.ConfigurationPath.FolderPath);

                        section = config.GetSection(HttpRequestValidatorKeys.ConfigurationSection);

                        // Update configuration values
                        UpdateConfigValues(section);

                        mgr.CommitChanges();
                    }

                    UpdateUI();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Cancel button click event. Restores original configuration variables back to the Config file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelClick(Object sender, EventArgs e)
        {
            ClearErrors();
            
            try
            {
                // Reset to original values
                UpdateVariables(true);

                // Only save if values were previously changed and saved
                if (valuesChanged)
                {
                    using (ServerManager mgr = new ServerManager())
                    {
                        ConfigurationSection section;

                        Configuration config = mgr.GetWebConfiguration(
                            Connection.ConfigurationPath.SiteName,
                            Connection.ConfigurationPath.ApplicationPath + Connection.ConfigurationPath.FolderPath);

                        section = config.GetSection(HttpRequestValidatorKeys.ConfigurationSection);

                        UpdateConfigValues(section);

                        mgr.CommitChanges();
                    }
                }

                // Display original values
                UpdateUI();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }

            valuesChanged = false;
        }

        #endregion

        #region Domain redirect buttons

        private void RemoveClick(object sender, EventArgs args)
        {
            // Remove the current selection from the domain redirects list and the underlying array
            string remove = (string)lstRedirects.SelectedItem;
            string removeFrom = remove.Split(' ')[0];
            string removeTo = remove.Split(' ')[2];

            for (int i = 0; i <= domainRedirectList.GetUpperBound(0); i++)
            {
                if ((domainRedirectList[i, 0] == removeFrom) && (domainRedirectList[i, 1] == removeTo))
                {
                    string[,] tempRedirects = domainRedirectList;
                    domainRedirectList = new string[tempRedirects.GetUpperBound(0), 2];

                    for (int j=0; j <= tempRedirects.GetUpperBound(0); j++)
                    {
                        if (j < i)
                        {
                            domainRedirectList[j, 0] = tempRedirects[j, 0];
                            domainRedirectList[j, 1] = tempRedirects[j, 1];
                        }
                        else if (j > i)
                        {
                            domainRedirectList[j - 1, 0] = tempRedirects[j, 0];
                            domainRedirectList[j - 1, 1] = tempRedirects[j, 0];
                        }
                    }
                }
            }

            // Refresh listbox
            lstRedirects.Items.Clear();
            for (int i = 0; i <= domainRedirectList.GetUpperBound(0); i++)
            {
                string item = domainRedirectList[i, 0] + " to " + domainRedirectList[i, 1];
                lstRedirects.Items.Add(item);
            }
        }

        private void AddClick(object sender, EventArgs args)
        {
            // Check that both boxes are not empty and do not contain spaces
            if (txtbxDomainRedirectAddRedirectFrom.Text.Trim() == string.Empty)
            {
                ShowError("The From textbox when adding a domain redirect is empty or just spaces");
                return;
            }
            else if (txtbxDomainRedirectAddRedirectFrom.Text.Contains(" "))
            {
                ShowError("The From textbox when adding a domain redirect contains spaces, this is invalid for a URL");
                return;
            }
            else if (txtbxDomainRedirectAddRedirectTo.Text.Trim() == string.Empty)
            {
                ShowError("The To textbox when adding a domain redirect is empty or just spaces");
                return;
            }
            else if (txtbxDomainRedirectAddRedirectTo.Text.Contains(" "))
            {
                ShowError("The To textbox when adding a domain redirect contains spaces, this is invalid for a URL");
                return;
            }

            // Add it
            string[,] tempRedirects = domainRedirectList;
            domainRedirectList = new string[tempRedirects.GetUpperBound(0) + 2, 2];

            for (int i = 0; i < domainRedirectList.GetUpperBound(0); i++)
            {
                domainRedirectList[i, 0] = tempRedirects[i, 0];
                domainRedirectList[i, 1] = tempRedirects[i, 1];
            }

            domainRedirectList[domainRedirectList.GetUpperBound(0), 0] = txtbxDomainRedirectAddRedirectFrom.Text;
            domainRedirectList[domainRedirectList.GetUpperBound(0), 1] = txtbxDomainRedirectAddRedirectTo.Text;

            txtbxDomainRedirectAddRedirectFrom.Text = string.Empty;
            txtbxDomainRedirectAddRedirectTo.Text = string.Empty;

            // Refresh listbox
            lstRedirects.Items.Clear();
            for (int i = 0; i <= domainRedirectList.GetUpperBound(0); i++)
            {
                string item = domainRedirectList[i, 0] + " to " + domainRedirectList[i, 1];
                lstRedirects.Items.Add(item);
            }
        }

        #endregion

        #region Cool down buttons

        private void InitiateCoolDownClick(object sender, EventArgs args)
        {
            ClearErrors();

            // Check we have enough config for this.
            if (coolDownFlagFileLocation == string.Empty)
            {
                ShowError("The cool down flag file location should have been set previously, cool down cannot commence.");
                return;
            }

            if (coolDownReportingIntervalSeconds == 0)
            {
                ShowError("The cool down reporting interval should have been set to a non zero value previously, cool down cannot commence.");
                return;
            }

            if (txtbxCoolDownRedirectDomain.Text == string.Empty)
            {
                ShowError("The cool down redirect domain must be set prior to initialising cool down. Please enter a value and try again.");
                return;
            }

            // Check the flag file location and event log reporting interval have not 
            // just been changed (as saving new values invalidates the point of cool down)
            if (coolDownFlagFileLocation != txtbxCoolDownFlagFileLocation.Text)
            {
                ShowError("The cool down flag file location cannot be changed immediately prior to initiating cool down. The value will be reverted to the config file value at which point you can retry initialising cool down.");
                txtbxCoolDownFlagFileLocation.Text = coolDownFlagFileLocation;

                if (coolDownReportingIntervalSeconds != int.Parse(txtbxCoolDownReportingIntervalSeconds.Text))
                {
                    ShowError("The cool down reporting interval cannot be changed immediately prior to initiating cool down. The value will be reverted to the config file value at which point you can retry initialising cool down.");
                    txtbxCoolDownReportingIntervalSeconds.Text = coolDownReportingIntervalSeconds.ToString();
                }

                return;
            }
            else if (coolDownReportingIntervalSeconds != int.Parse(txtbxCoolDownReportingIntervalSeconds.Text))
            {
                ShowError("The cool down reporting interval cannot be changed immediately prior to initiating cool down. The value will be reverted to the config file value at which point you can retry initialising cool down.");
                txtbxCoolDownReportingIntervalSeconds.Text = coolDownReportingIntervalSeconds.ToString();
                return;
            }

            // Create the flag file and populate it with first line = domain, second line = inc url extensions setting
            try
            {
                string contents = txtbxCoolDownRedirectDomain.Text + "\r\n" + chkbxCoolDownIncludeUrlExtensions.Checked.ToString();
                File.WriteAllText(coolDownFlagFileLocation, contents);
            }
            catch (Exception ex)
            {
                ShowError(ex, string.Format("Flag file could not be created: {0}.", coolDownFlagFileLocation), false);

                try
                {
                    if (File.Exists(coolDownFlagFileLocation))
                    {
                        File.Delete(coolDownFlagFileLocation);
                    }
                }
                catch { }
            }
        }

        #endregion

        #region Message redirect buttons

        private void InitiateMessageRedirectClick(object sender, EventArgs args)
        {
            ClearErrors();

            // Check we have enough config for this.
            if (messageRedirectFlagFileLocation == string.Empty)
            {
                ShowError("The message redirection flag file location should have been set previously, message redirection cannot commence.");
                return;
            }

            if (messageRedirectReportingIntervalSeconds == 0)
            {
                ShowError("The message redirection reporting interval should have been set to a non zero value previously, message redirection cannot commence.");
                return;
            }

            if (txtbxMessageRedirectRedirectUrl.Text == string.Empty)
            {
                ShowError("The message redirection URL must be set prior to initialising cool down. Please enter a value and try again.");
                return;
            }

            // Check the flag file location and event log reporting interval have not 
            // just been changed (as saving new values invalidates the point of cool down)
            if (messageRedirectFlagFileLocation != txtbxMessageRedirectFlagFileLocation.Text)
            {
                ShowError("The message redirection flag file location cannot be changed immediately prior to initiating message redirection. The value will be reverted to the config file value at which point you can retry initialising message redirection.");
                txtbxMessageRedirectFlagFileLocation.Text = messageRedirectFlagFileLocation;

                if (messageRedirectReportingIntervalSeconds != int.Parse(txtbxMessageRedirectReportingIntervalSeconds.Text))
                {
                    ShowError("The message redirection reporting interval cannot be changed immediately prior to initiating message redirection. The value will be reverted to the config file value at which point you can retry initialising messag redirection.");
                    txtbxMessageRedirectReportingIntervalSeconds.Text = messageRedirectReportingIntervalSeconds.ToString();
                }

                return;
            }
            else if (messageRedirectReportingIntervalSeconds != int.Parse(txtbxMessageRedirectReportingIntervalSeconds.Text))
            {
                ShowError("The message redirection reporting interval cannot be changed immediately prior to initiating message redirection. The value will be reverted to the config file value at which point you can retry initialising messag redirection.");
                txtbxMessageRedirectReportingIntervalSeconds.Text = messageRedirectReportingIntervalSeconds.ToString();
                return;
            }

            // Create the flag file and populate it with first line = domain, second line = inc url extensions setting
            try
            {
                string contents = txtbxMessageRedirectRedirectUrl.Text + "\r\n" + chkbxMessageRedirectIncludeUrlExtensions.Checked.ToString();
                File.WriteAllText(messageRedirectFlagFileLocation, contents);
            }
            catch (Exception ex)
            {
                ShowError(ex, string.Format("Flag file could not be created: {0}.", messageRedirectFlagFileLocation), false);

                try
                {
                    if (File.Exists(coolDownFlagFileLocation))
                    {
                        File.Delete(coolDownFlagFileLocation);
                    }
                }
                catch { }
            }
        }

        #endregion
    }
}
