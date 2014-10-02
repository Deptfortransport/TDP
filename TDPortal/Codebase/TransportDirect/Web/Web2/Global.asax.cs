//******************************************************************************
//NAME			: Global.asax.cs
//AUTHOR		: Joe Morrissey
//DATE CREATED	: 01/07/2003
//DESCRIPTION	: Also known as the ASP.NET application file, this is a file that 
//contains code for responding to application-level events raised by ASP.NET 
//or by HttpModules
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Global.asax.cs-arc  $
//
//   Rev 1.3   May 14 2008 15:30:24   mmodi
//Added a no cache value for cookies
//Resolution for 4889: Del 10.1 - Repeat Visitor Cookies
//
//   Rev 1.2   Mar 31 2008 13:23:54   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//   Rev 1.0   Nov 08 2007 13:10:52   mturner
//Initial revision.
//
//   Rev 1.29   Nov 14 2006 14:30:18   scraddock
//Added explicit Cache directives for initial connects. Set values to NoStore and Nocache
//Resolution for 4249: Problem with hyperlinks to TDP in WORD
//
//   Rev 1.28   Feb 23 2006 19:15:58   build
//Automatically merged from branch for stream3129
//
//   Rev 1.27.1.2   Jan 30 2006 14:40:04   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.27.1.1   Jan 26 2006 14:54:20   mdambrine
//removed a comment
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.27.1.0   Jan 10 2006 15:53:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.27   Jan 26 2005 15:53:18   PNorell
//Support for partitioning the session information.
//
//   Rev 1.26   Aug 19 2004 12:57:30   COwczarek
//Remove initialisation of FindPageState in session.  Should not
//be initialised to any value to indicate a Find A mode of None.
//Resolution for 1345: Clicking Find A tab should display page for current Find A mode
//
//   Rev 1.25   Jul 22 2004 18:05:54   passuied
//Integration between pages and move of code to location service
//
//   Rev 1.24   Jul 14 2004 13:00:22   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.23   May 26 2004 09:13:06   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.22   May 13 2004 12:40:52   jgeorge
//Added FindFlightPageState initialisation
//
//   Rev 1.21   May 10 2004 17:01:24   passuied
//added init to FindStationPageState
//
//   Rev 1.20   Apr 28 2004 16:19:54   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.19   Mar 17 2004 19:19:40   RPhilpott
//Add error logging (moved from errorpage.aspx.cs and corrected).
//Resolution for 664: Error logging
//
//   Rev 1.18   Mar 12 2004 17:00:58   CHosegood
//Removed JourneyMapState construction in InitialiseSessionVariables
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.17   Mar 01 2004 15:34:12   CHosegood
//Creating JourneyMapState in SessionStart event
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.16   Nov 06 2003 16:26:00   PNorell
//Update and moved the remote configuration before any initialisation is done.
//
//   Rev 1.15   Oct 08 2003 08:53:40   PNorell
//Updated for running CJP.
//
//   Rev 1.14   Sep 26 2003 16:55:02   JMorrissey
//Removed setting of a CS profile ticket from Session_Start. This is not required.
//
//   Rev 1.13   Sep 25 2003 19:21:26   hahad
//no changes
//
//   Rev 1.12   Sep 22 2003 12:49:32   passuied
//change in sessionInitialise
//
//   Rev 1.11   Sep 18 2003 16:31:16   passuied
//corrected to match with latest TDJourneyParameters
//
//   Rev 1.10   Sep 18 2003 16:23:24   passuied
//Added initialisation of more session variables
//
//   Rev 1.9   Sep 18 2003 12:03:14   passuied
//Added initialisation of session variables
//
//   Rev 1.8   Sep 18 2003 09:56:40   jcotton
//Changes for intitial screenflow integration work
//
//   Rev 1.7   Sep 16 2003 16:06:36   passuied
//changed order of initialisation
//
//   Rev 1.6   Sep 16 2003 12:03:42   passuied
//updated call to ASPNetInitialisation constructor to pass the resourceManager to it
//
//   Rev 1.5   Aug 26 2003 10:02:40   passuied
//added ServiceDiscovery Init
//
//   Rev 1.4   Jul 18 2003 16:29:48   JMorrissey
//Changed public property of TDLanguageManager to tdResourceManager
//
//   Rev 1.3   Jul 17 2003 10:35:06   ALole
//Updated to Support internationalizing TDDateTime

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ServiceDiscovery.Initialisation;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;

using System;
using System.IO;
using System.Runtime.Remoting;
using System.Web;

using Logger = System.Diagnostics.Trace;

using TransportDirect.Common.ResourceManager;
using TransportDirect.Web.Support;
using TransportDirect.Common.DatabaseInfrastructure.Content;

namespace TransportDirect.UserPortal.Web
{
    /// <summary>
    /// Summary description for Global.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        #region Private Static Fields

        /// <summary>
        /// This is used to record whether the CustomApplicationStart method has fired.
        /// </summary>
        private static bool customApplicationStartFired = false;
        /// <summary>
        /// A lock to prevent the customApplicationStartFired variable from being accessed by more
        /// than one thread at a time.
        /// </summary>
        private static readonly object customApplicationStartFiredLock = new object();
        
        /// <summary>
        /// private resource manager field  
        /// </summary>
        private static CustomDateTimeInfoManager cm = null;

        /// <summary>
        /// private resource manager field  
        /// </summary>
        private static TDResourceManager rm = null;

        /// <summary>
        /// private language field where the default language is defined.
        /// </summary>  
        private static string lang = "en-GB";

        #endregion

        #region Public Static Properties

        /// <summary>
        /// public property for TDResourceManager rm
        /// </summary>
        public static TDResourceManager tdResourceManager
        {
            get
            {
                //return instance of TDResourceManager, to be used throughout TDP				
                return rm;
            }
            set
            {
                //set value for TDResourceManager
                rm = value;
            }
        }

        /// <summary>
        /// public property for CustomDateTimeInofManager cm
        /// </summary>
        public static CustomDateTimeInfoManager TDDTIManager
        {
            get
            {
                //return instance of CustomeDateTimeInfoManager, to be used throughout TDP				
                return cm;
            }
            set
            {
                //set value for CustomeDateTimeInfoManager
                //Should only be called in Global.Application_Start
                cm = value;
            }
        }

        /// <summary>
        /// public property for string lang
        /// </summary>
        public static string TDLang
        {
            get
            {
                //return the default language to be used throughout TDP				
                return lang;
            }
        }

        #endregion

        #region Constructor

        public Global()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method used to be the Application_Start method. However, it relies on
        /// knowledge of the Request url, so the logic is now all called from 
        /// Session_Start on a once only basis.
        /// </summary>
        private void CustomApplicationStart()
        {
            Global.InstantiateResourceManagerIfRequired();

            // Remember: Always keep 3-rd party initialisations before our initialisations

            // Do remoting configuration before any initialisation is done
            string applicationPath = Context.Server.MapPath(Context.Request.ApplicationPath);
            string configPath = Path.Combine(applicationPath, "cjp.client.config");
            if (File.Exists(configPath))
                RemotingConfiguration.Configure(configPath, false);

            // Initialise the service discovery
            TDServiceDiscovery.Init(new AspNetInitialisation(Global.tdResourceManager));

            //Create an array to hold TDCultureInfo objects for all supported Cultures except en-GB
            TDCultureInfo[] dateTimeLanguages = new TDCultureInfo[1];
            //Populate the array with for all non default Cultures
            dateTimeLanguages[0] = new TDCultureInfo("en-GB", "cy-GB", 1106);
            //Invoke CustomDateTimeInfo Manager to load date related text for non default Cultures
            TDDTIManager = new CustomDateTimeInfoManager();
            TDDTIManager.Init(dateTimeLanguages);
        }

        #endregion

        #region Public Static Methods

        public static void RedirectToGenericPageIfNecessary() 
        {
            string actualPage = HttpContext.Current.Request.FilePath.ToLower();
            //is it an aspx file
            if (actualPage.IndexOf(".aspx") > -1)
            {
                actualPage = actualPage.Replace("/", "_").Replace(".aspx", "");
                string realPage = tdResourceManager.GetContentDatabaseString(actualPage, "Page");
                //do we have details for it... ( it may be a genuine 404 scenario )
                if (realPage != null)
                {
                    string pageToRedirect = realPage + "?id=" + actualPage;
                    HttpContext.Current.Response.Redirect(pageToRedirect);
                }
            }
        }

        public static void InstantiateResourceManagerIfRequired()
        {
            if (tdResourceManager == null)
            {
                // Instantiate the Transport Direct resource manager and set the language culture			
                tdResourceManager = TDResourceManager.GetResourceManagerFromCache("langStrings");
            }
        }

        #endregion

        #region Protected Methods

        protected void Session_Start(Object sender, EventArgs e)
        {
            //default to EnglishLanguage
            string channelLanguage = TDLang;

            //Check if user has selected a non-English channel e.g. through using favourites			
            if (TDPage.SessionChannelName != null)
            {
                //if channel is Welsh set language to Welsh
                if (TDPage.SessionChannelName.IndexOf(TDPage.WelshChannelIndicator) > 0)
                {
                    channelLanguage = "cy-GB";
                }
            }

            //set Current UI Culture
            LanguageHandler.SetThreadLanguageCulture(channelLanguage);

            // Initiliase Initial Entry Page ID
            ITDSessionManager session =
                (ITDSessionManager)TDServiceDiscovery.Current
                [ServiceDiscoveryKey.SessionManager];
            session.Session[SessionKey.NextPageId] = PageId.Empty;
            session.Session[SessionKey.Transferred] = false;
            session.FormShift[SessionKey.TransitionEvent] = TransitionEvent.Default;
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetNoStore();

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            HttpContext.Current.Response.Cache.AppendCacheExtension("no-cache=\"set-cookie\"");
                        
            //Here we run CustomApplicationStart if required.
            //Note that we use locks to ensure that this can
            //only be run by one thread at a time.
            lock (customApplicationStartFiredLock)
            {
                if (!customApplicationStartFired)
                {
                    CustomApplicationStart();
                    customApplicationStartFired = true;
                }
            }

            RedirectToGenericPageIfNecessary();
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_Error(Object sender, EventArgs e)
        {

            // Server.GetLastError() normally returns an HttpUnhandledException 
            //  object that wraps the exception we actually want to log ...

            Exception ex = Server.GetLastError().InnerException;

            if (ex == null)
            {
                ex = Server.GetLastError();
            }

            string message = "Unhandled Exception on page: " + Request.Path
                + "\n\nMessage:\n " + ex.Message
                + "\n\nStack trace:\n" + ex.StackTrace;

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                message));
        }

        protected void Session_End(Object sender, EventArgs e)
        {

        }

        protected void Application_End(Object sender, EventArgs e)
        {

        }

        #endregion

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}

