// *********************************************** 
// NAME                 : SiteStatusLoaderService.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Main service class which initiates the realtime, historic, and test threads to monitor the Third Party Site Status data files
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderService/SiteStatusLoaderService.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:37:16   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

using AO.Common;
using AO.DatabaseInfrastructure;
using AO.EventLogging;
using AO.Properties;

using PropertyService = AO.Properties.Properties;
using Logger = System.Diagnostics.Trace;


namespace AO.SiteStatusLoaderService
{
    public partial class SiteStatusLoaderService : ServiceBase
    {
        #region Private members

        private SiteStatusLoaderCurrent siteStatusLoaderCurrent;
        private SiteStatusLoaderHistoric siteStatusLoaderHistoric;
        private bool serviceInitialised;
        private bool testMode;
        
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SiteStatusLoaderService(string serviceName)
        {
            // This call is required by the Windows.Forms Component Designer.
            InitializeComponent();
            
            // Service specific values
            this.ServiceName = serviceName;

            // Not allowing pause/continue because of cross threading. This can be included as a future enhancement.
            this.CanPauseAndContinue = false;

            siteStatusLoaderCurrent = null;
            siteStatusLoaderHistoric = null;
        }

        #endregion
        
        #region Private methods

        #region Initialise
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        #endregion

        #region Event handlers

        /// <summary>
        /// Called when service is started.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            siteStatusLoaderCurrent = null;
            siteStatusLoaderHistoric = null;
            serviceInitialised = false;
            testMode = false;
            AutoLog = false; // Prevents .NET runtime from making entry to say service has started successfully.
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

                serviceInitialised = true;

                // Initialise was ok
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Verbose,
                    string.Format(Messages.SSLSInitialisationCompleted, ServiceName)));
            }
            catch (SSException ssEx)
            {
                // Log error to default trace listener in case SSTraceListener failed to create.
                EventLog.WriteEntry(ServiceName, String.Format(Messages.SSLSInitialisationFailed, ssEx.Message),
                    EventLogEntryType.Error, (int)ssEx.Identifier);

                // Force the application to exit
                System.Environment.Exit(0);
            }

            if (serviceInitialised)
            {
                try
                {
                    // Create an instance of the classes that will do the work and start them on new threads

                    #region Test mode

                    testMode = bool.Parse(PropertyService.Instance["SiteStatusLoaderService.TestMode"]);

                    if (testMode)
                    {
                        SiteStatusLoaderTest.InitStaticParameters(this.ServiceName);
                        ThreadStart threadStartTest = new ThreadStart(SiteStatusLoaderTest.Run);
                        Thread ssloaderTestThread = new Thread(threadStartTest);
                        ssloaderTestThread.Start();
                    }

                    #endregion

                    bool startCurrentThread = bool.Parse(PropertyService.Instance["SiteStatusLoaderService.ServiceCurrent.Switch"]);
                    bool startHistoricThread = bool.Parse(PropertyService.Instance["SiteStatusLoaderService.ServiceHistoric.Switch"]);

                    if (startCurrentThread)
                    {
                        SiteStatusLoaderCurrent.InitStaticParameters(this.ServiceName);
                        ThreadStart threadStartCurrent = new ThreadStart(SiteStatusLoaderCurrent.Run);
                        Thread ssloaderCurrentThread = new Thread(threadStartCurrent);
                        ssloaderCurrentThread.Start();
                    }

                    if (startHistoricThread)
                    {
                        SiteStatusLoaderHistoric.InitStaticParameters(this.ServiceName);
                        ThreadStart threadStartHistoric = new ThreadStart(SiteStatusLoaderHistoric.Run);
                        Thread ssloaderHistoricThread = new Thread(threadStartHistoric);
                        ssloaderHistoricThread.Start();
                    }
                                        
                    // Log the service has started
                    Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                        string.Format(Messages.SSLSServiceStarted, ServiceName)));

                    if (!startCurrentThread)
                    {
                        Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                            string.Format(Messages.SSLSLoaderServiceThreadTurnedOff, "Current")));
                    }

                    if (!startHistoricThread)
                    {
                        // Log the service has started
                        Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                            string.Format(Messages.SSLSLoaderServiceThreadTurnedOff, "Historic")));
                    }
                }
                catch (SSException ssEx)
                {
                    Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                        string.Format(Messages.SSLSSerivceStartFailed, ServiceName) + ssEx.Message));
                }
                catch (Exception ex)
                {
                    Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Error,
                        string.Format(Messages.SSLSSerivceStartFailed, ServiceName) + ex.Message));
                }
            }
        }

        /// <summary>
        /// Called when service is paused
        /// </summary>
        protected override void OnPause()
        {
            if (serviceInitialised)
            {
                if (siteStatusLoaderCurrent != null)
                {
                    siteStatusLoaderCurrent.Pause();
                }

                if (siteStatusLoaderHistoric != null)
                {
                    siteStatusLoaderHistoric.Pause();
                }

                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                    string.Format(Messages.SSLSServicePaused, ServiceName)));
            }

            base.OnPause();
        }

        /// <summary>
        /// Called when service is continued
        /// </summary>
        protected override void OnContinue()
        {
            if (serviceInitialised)
            {
                if (siteStatusLoaderCurrent != null)
                {
                    siteStatusLoaderCurrent.Continue();
                }

                if (siteStatusLoaderHistoric != null)
                {
                    siteStatusLoaderHistoric.Continue();
                }

                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                    string.Format(Messages.SSLSServiceContinued, ServiceName)));
            }

            base.OnContinue();
        }


        /// <summary>
        /// Called when service is stopped
        /// </summary>
        protected override void OnStop()
        {
            if (siteStatusLoaderCurrent != null)
            {
                siteStatusLoaderCurrent.Stop();
                siteStatusLoaderCurrent.Dispose(true);
                siteStatusLoaderCurrent = null;
            }

            if (siteStatusLoaderHistoric != null)
            {
                siteStatusLoaderHistoric.Stop();
                siteStatusLoaderHistoric.Dispose(true);
                siteStatusLoaderHistoric = null;
            }

            if (serviceInitialised)
            {
                Logger.Write(new OperationalEvent(SSEventCategory.Infrastructure, SSTraceLevel.Info,
                    string.Format(Messages.SSLSServiceStopped, ServiceName)));
            }
        }

        #endregion

    }
}
