#region Amendment history
// *********************************************** 
// NAME			: $Workfile:   CacheUpService.cs  $
// AUTHOR		: Peter Norell
// DATE CREATED	: 01/11/2007
// REVISION		: $Revision:   1.1  $
// DESCRIPTION	: The cache up service processing class
// ************************************************ 
// $Log:   P:\archives\Codebase\WebTIS\CacheUpService\CacheUpService.cs-arc  $ 
//
//   Rev 1.1   Nov 02 2007 16:57:48   p.norell
//Updated for action taking depending on consequences of the testing.
//
//   Rev 1.0   Nov 02 2007 15:13:08   p.norell
//Initial Revision
//
#endregion
#region Imports
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using WT.Common;
using WT.Properties;
using WT.Common.Logging;
using Logger = System.Diagnostics.Trace;
using System.Threading;
using System.Reflection;
#endregion

namespace WT.CacheUpService
{
    /// <summary>
    /// The service base for the service class as well as the entry point for the application when run as 
    /// a command line service
    /// </summary>
    partial class CacheUpService : ServiceBase
    {
        #region Local declarations
        /// <summary>
        /// The runner object
        /// </summary>
        CacheUpRunner cup = null;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        #endregion

        #region Constructors
        /// <summary>
		/// Public constructor
		/// </summary>
		public CacheUpService()
		{
			// Set up service name - this must match the AID used in the properties, and in ProjectInstaller.cs.
			this.ServiceName = "CacheUpService";

			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();
        }
        #endregion

        #region Main method
        /// <summary>
		/// The main entry point for the process
		/// </summary>
		static void Main(string[] args)
		{
            #region Console mode
            if (args.Length > 0 && args[0] == "-c")
            {
                string err = string.Empty;
                if (!WTInitialisation(ref err))
                {
                    // Do nothing with the err
                    Environment.Exit(1);
                    return;
                }
                List<CupUrlInfo> urls = new List<CupUrlInfo>();
                if (!CupInitialisation(urls))
                {
                    Environment.Exit(1);
                    return;
                }
                if (urls.Count == 0)
                {
                    Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                        "No Session",
                        "No urls to parse - service will not start"));
                    // Nothing to monitor - bail out
                    Environment.Exit(1);
                    return;
                }

                // Create runner
                CacheUpRunner cup = new CacheUpRunner(urls);
                
                Environment.Exit(cup.Run());
                return;
            }
            #endregion

            #region Service mode

            System.ServiceProcess.ServiceBase[] ServicesToRun;
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new CacheUpService() };
			System.ServiceProcess.ServiceBase.Run( ServicesToRun );

            #endregion
        }
        #endregion

        #region Cache-up Service initialisation
        /// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{ 
			components = new System.ComponentModel.Container();
        }

        /// <summary>
        /// Initialisation of WT common
        /// </summary>
        /// <returns></returns>
        private static bool WTInitialisation(ref string err)
        {
            #region Initialisation
            try
            {
                ServiceDiscovery.Init(new CupInitialisation());
            }
            catch (Exception exc)
            {
                #region Log servious errors during startup
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error while starting CacheUpService " + exc.Message, exc));
                err = exc.Message;
                return false;
                #endregion
            }
            return true;
            #endregion
        }

        /// <summary>
        /// Initialises the cache up service data
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        private static bool CupInitialisation(List<CupUrlInfo> urls)
        {
            #region Read all urls

            IPropertyService prop = PropertyService.Current;
            string urlIdsStr = prop["CacheUpService.Urls"];

            if (!string.IsNullOrEmpty(urlIdsStr))
            {
                string[] urlIds = urlIdsStr.Split(',');

                foreach (string urlId in urlIds)
                {
                    CupUrlInfo cui = GetCupUrlInfo(urlId);

                    if (cui != null)
                    {
                        urls.Add(cui);
                    }
                }
            }

            return true;
            #endregion
        }


        #endregion

        #region Clean-up
        /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
	
		}

		/// <summary>
		/// C# Destructor.
		/// </summary>
		~CacheUpService()
		{
			Dispose( false );
        }
        #endregion

        #region Service start-up

        /// <summary>
		/// Called when service is started.
		/// </summary>
		/// <param name="args">Arguments passed to service.</param>
		protected override void OnStart(string[] args)
		{
			try
            {
                #region Initialisations
                string err = string.Empty;
                if (!WTInitialisation(ref err))
                {
                    EventLog.WriteEntry(ServiceName, "Failed startup due to errors in the initialisation - "+err, EventLogEntryType.Error);
                }
                AutoLog = true; // Disable default logging (will be using WT Logging Service).
                List<CupUrlInfo> urls = new List<CupUrlInfo>();
                CupInitialisation(urls);
                if (urls.Count == 0)
                {
                    Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                        "No Session",
                        "No urls to parse - service will not start"));
                    // Nothing to monitor - bail out
                    EventLog.WriteEntry(ServiceName, "Failed startup - no URL's to monitor", EventLogEntryType.Error);
                    return;
                }
                #endregion

                #region Start the update
                // Create runner
                cup = new CacheUpRunner(urls);
                // Create thread
                EventLog.WriteEntry(ServiceName, "Started successfully", EventLogEntryType.Information);
                // cup.Start();
                ThreadStart ts = new ThreadStart(cup.Start);
                ts.BeginInvoke(null, null);
                // Start thread
                #endregion
			}
			catch (Exception exc)
            {
                #region Log servious errors during startup
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error while starting CacheUpService "+exc.Message, exc));
                EventLog.WriteEntry(ServiceName,"Failed startup due to exception - "+exc.Message, EventLogEntryType.Error);
                AutoLog = false; // Prevents .NET runtime from making entry to say service has started successfully.
                #endregion
            }
        }
        #endregion

        #region Convinience methods
        /// <summary>
        /// Create Cup Url Info from the property settings
        /// </summary>
        /// <param name="urlId">S</param>
        /// <returns></returns>
        private static CupUrlInfo GetCupUrlInfo(string urlId)
        {
            #region varibles
            IPropertyService prop = PropertyService.Current;
            string prefix = string.Format("CacheUpService.Url.{0}.", urlId);
            #endregion

            #region End of Url list?
            if ( prop[prefix+"UrlAddress"] == null )
            {
                // Not found - no more URLS to process - terminate
                return null;
            }
            #endregion

            #region CupUrlInfo parsing
            CupUrlInfo cui = new CupUrlInfo();

            #region Name - mandatory
            cui.Name = prop[prefix + "Name"];
            if (cui.Name == null)
            {
                // Mandatory property - log and terminate
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "The property [" + prefix + "Name] is missing for url[" + urlId + "] and it " +
                    "is mandatory. Check property setting[" + prefix + "Name]"));
                return null;
            }
            #endregion

            #region UrlAddress - mandatory
            cui.UrlAddress = prop[prefix + "UrlAddress"];
            if (cui.UrlAddress == null)
            {
                // Mandatory property - log and terminate
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "The property [" + prefix + "UrlAddress] is missing for url[" + urlId + "] and it " +
                    "is mandatory. Check property setting[" + prefix + "UrlAddress]"));
                return null;
            }
            #endregion

            #region Accepted codes - mandatory
            cui.AcceptedCodes = prop[prefix + "AcceptedCodes"];
            if (cui.AcceptedCodes == null)
            {
                // Mandatory property - log and terminate
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "The property [" + prefix + "AcceptedCodes] is missing for url["+urlId+"] and it "+
                    "is mandatory. Check property setting[" + prefix + "AcceptedCodes]"));
                return null;
            }
            #endregion

            #region Retry codes - non mandatory
            try
            {
                if (prop[prefix + "RetryCodes"] != null)
                {
                    cui.RetryCodes = prop[prefix + "RetryCodes"];
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error parsing RetryCodes (property[" + prefix + "RetryCodes]) with value " +
                    prop[prefix + "RetryCodes"], exc));
                return null;
            }
            #endregion

            #region Keep alive parsing
            try
            {
                if (prop[prefix + "KeepAlive"] != null)
                {
                    cui.KeepAlive = Boolean.Parse(prop[prefix + "KeepAlive"]);
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error parsing Keep alive (property[" + prefix + "KeepAlive]) with value " +
                    prop[prefix + "KeepAlive"], exc));
                return null;
            }
            #endregion

            #region Follow Redirect parsing
            try
            {
                if (prop[prefix + "FollowRedirect"] != null)
                {
                    cui.FollowRedirect = Boolean.Parse(prop[prefix + "FollowRedirect"]);
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write( new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error parsing Follow Redirect (property["+prefix + "FollowRedirect]) with value "+
                    prop[prefix + "FollowRedirect"],exc) );
                return null;
            }
            #endregion

            #region Retain cookies parsing
            try
            {
                if (prop[prefix + "RetainCookies"] != null)
                {
                    cui.RetainCookies = Boolean.Parse(prop[prefix + "RetainCookies"]);
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error parsing Retain Cookies (property[" + prefix + "RetainCookies]) with value " +
                    prop[prefix + "RetainCookies"], exc));
                return null;
            }
            #endregion

            #region Severity parsing
            try
            {
                if (prop[prefix + "Severity"] != null)
                {

                    cui.Severity = (WTTraceLevel)Enum.Parse(typeof(WTTraceLevel), prop[prefix + "Severity"]);
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error parsing Severity (property[" + prefix + "Severity]) with value " +
                    prop[prefix + "Severity"], exc));
                return null;
            }
            #endregion

            #region Timeout parsing
            try
            {
                if (prop[prefix + "TimeoutSeconds"] != null)
                {
                    cui.TimeoutSeconds = int.Parse(prop[prefix + "TimeoutSeconds"]) * 1000;
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error parsing Timeout (property[" + prefix + "TimeoutSeconds]) with value " +
                    prop[prefix + "TimeoutSeconds"], exc));
                return null;
            }
            #endregion

            #region Page scan - non mandatory
            cui.PageScan = prop[prefix + "PageScan"];
            #endregion

            #region Page scan positive - non mandatory
            try
            {
                if (prop[prefix + "PageScanPositive"] != null)
                {
                    cui.PageScanPositive = bool.Parse(prop[prefix + "PageScanPositive"]);
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error parsing PageScanPositive (property[" + prefix + "PageScanPositive]) with value " +
                    prop[prefix + "PageScanPositive"], exc));
                return null;
            }
            #endregion

            #region Username, password - non mandatory
            try
            {
                if (prop[prefix + "Username"] != null)
                {
                    cui.Username = prop[prefix + "Username"];
                    cui.Password = prop[prefix + "Password"];
                    cui.Domain = prop[prefix + "Domain"];
                    cui.AuthType = prop[prefix + "AuthType"];
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error parsing Username (property[" + prefix + "Username]) with value " +
                    prop[prefix + "Username"], exc));
                return null;
            }
            #endregion

            #region The Action usage and settings number to use
            string strAssembly = prop[prefix + "ActionAssembly"];
            string strClass = prop[prefix + "ActionClass"];
            Assembly assembly = typeof(CacheUpService).Assembly;
            try
            {
                if (!string.IsNullOrEmpty(strClass))
                {
                    if (!string.IsNullOrEmpty(strAssembly))
                    {
                        assembly = Assembly.Load(strAssembly);
                    }
                    Type theClass = assembly.GetType(strClass);
                    cui.Action = (ICacheAction)Activator.CreateInstance(theClass);
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error creating action class (property[" + prefix + "ActionClass]) with value " +
                    prop[prefix + "ActionClass"] +
                    " assembly " + prop[prefix + "ActionAssembly"], exc));
                return null;
            }

            try
            {
                if (prop[prefix + "ActionSettingsId"] != null)
                {
                    cui.ActionSettingsId = prop[prefix + "ActionSettingsId"];
                }
            }
            catch (Exception exc)
            {
                // Error parsing property
                Logger.Write(new OperationalEvent(WTTraceLevel.Error, WTTraceLevel.None,
                    "No Session",
                    "Error parsing ActionSettingsId (property[" + prefix + "ActionSettingsId]) with value " +
                    prop[prefix + "ActionSettingsId"], exc));
                return null;
            }

            #endregion

            #endregion

            return cui;
        }
        #endregion

        #region Service stop
        /// <summary>
		/// Stop the service.
		/// </summary>
		protected override void OnStop()
		{
            if (cup != null)
            {
                cup.Done = true;
                cup.Join();
            }
        }
        #endregion

    }
}
