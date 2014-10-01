// *********************************************** 
// NAME             : Global.asax.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Global application site class 
// ************************************************
// 

using System;
using System.IO;
using System.Runtime.Remoting;
using System.Web;
using TDP.Common;
using TDP.Common.EventLogging;
using TDP.Common.ResourceManager;
using TDP.Common.ServiceDiscovery;
using TDP.Common.ServiceDiscovery.Initialisation;
using TDP.UserPortal.SessionManager;
using Logger = System.Diagnostics.Trace;

namespace TDP.UserPortal.TDPWeb
{
    /// <summary>
    /// Global web application class
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        #region Private members

        // Resource manager
        private static TDPResourceManager tdpResourceManager = null;

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Read/write. TDPResourceManager to be used throughout application
        /// </summary>
        public static TDPResourceManager TDPResourceManager
        {
            get { return tdpResourceManager; }
            set { tdpResourceManager = value; }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        ///  Method to set up the resource manager, if needed
        /// </summary>
        public static void InstantiateResourceManager()
        {
            if (tdpResourceManager == null)
            {
                // Instantiate a resource manager
                tdpResourceManager = new TDPResourceManager();
            }
        }

        #endregion

        #region Application events

        /// <summary>
        /// Application Start - Only fires once
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(object sender, EventArgs e)
        {
            #if DEBUG
            //System.Diagnostics.Debugger.Break();
            #endif

            // Initialise the service discovery
            TDPServiceDiscovery.Init(new TDPWebInitialisation());

            // Initialise resource manager
            Global.InstantiateResourceManager();

            // Setup remoting configuration
            string applicationPath = Context.Server.MapPath("~");
            string configPath = Path.Combine(applicationPath, "Remoting.config");
            if (File.Exists(configPath))
            {
                RemotingConfiguration.Configure(configPath, false);
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "Loaded remoting configuration file: " + configPath));
            }
            else
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, "Could not find remoting configuration file: " + configPath));	
        
        }

        /// <summary>
        /// Session Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_Start(object sender, EventArgs e)
        {
            // Initialise the intial page entry
            TDPSessionManager.Current.Session[SessionKey.NextPageId] = PageId.Empty;
            TDPSessionManager.Current.Session[SessionKey.Transferred] = false;
        }

        /// <summary>
        /// Application BeginRequest - Fires for every request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetNoStore();

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            HttpContext.Current.Response.Cache.AppendCacheExtension("no-cache=\"set-cookie\"");
        }

        /// <summary>
        /// Application AuthenticateRequest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Application Error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            // Server.GetLastError() normally returns an HttpUnhandledException 
            // object that wraps the exception we actually want to log ...
            Exception ex = Server.GetLastError().InnerException;

            if (ex == null)
            {
                ex = Server.GetLastError();
            }

            string message = "Unhandled Exception on page: " + Request.Path
                + "\n\nMessage:\n " + ex.Message
                + "\n\nStack trace:\n" + ex.StackTrace;

            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, message));
        }

        /// <summary>
        /// Session End
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Application End
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {

        }

        #endregion
    }
}