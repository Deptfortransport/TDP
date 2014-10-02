// *********************************************** 
// NAME             : CookieHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Aug 2013
// DESCRIPTION  	: CookieHelper class to detect and process cookies, raising RepeatVisitorEvents
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Web;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.PropertyService.Properties;
using System.Text.RegularExpressions;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.UserPortal.Web.Code
{
    /// <summary>
    /// CookieHelper class to detect and process cookies, raising RepeatVisitorEvents
    /// </summary>
    public class CookieHelper
    {
        #region Private members

        #region Constants

        private const string UserAgentHeaderProperty = "Cookie.UserAgent.HeaderKey";
        
        // Name of the test cookie sent to the browser when checking for Cookie support
        private const string COOKIE_TESTCOOKIE = "TDPTestCookie";
        // Name of the session parameter indicating cookie test is being done.
        // Needed because we assume cookies are supported and can only confirm on a subsequent user request
        private const string COOKIE_TEST = "CookieTest";

        // Name of the session parameter to hold the Cookies support field, used for new/repeatvisitor events
        private const string COOKIE_SUPPORT = "CookieSupport";
        // Name of the session parameter indicating this user should not have cookies sent to 
        // e.g. if is a "robot" user, and therefore don't want raise a new/repeatvisitor event
        private const string COOKIE_OVERRIDE = "CookieOverride";

        #endregion

        private System.Web.SessionState.HttpSessionState session = null;
        private PageId pageId = PageId.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CookieHelper()
        {
            this.session = HttpContext.Current.Session;
                //TDSessionManager.Current.Session;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageId">PageId is used for Visitor event logging and updating the cookie's last page visited value</param>
        public CookieHelper(PageId pageId)
            : this()
        {
            this.pageId = pageId;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// This method checks whether the user has indicated that Cookies are enabled. The default 
        /// setting is false. It places a test Cookie in the response and then checks to see if it is
        /// available in a subsequent request, deleting it if it is. This is then used as an indication 
        /// of the browser's Cookie support. 
        /// IsCookieSettingKnown is stored in the session and cannot be reset.
        /// </summary>
        public void DetectCookies()
        {
            // First page hit only, check if we should send cookie based on the requests User Agent
            if (session[COOKIE_OVERRIDE] == null)
            {
                #region Allowed to add cookie test

                bool cookieOverride = true;

                // Check property to see if we want to detect repeat visitors
                try
                {
                    cookieOverride = bool.Parse(Properties.Current["Cookie.RepeatVisitor.Switch"]);
                }
                catch
                {
                    // By default we want to detect repeat visitors
                }

                if (cookieOverride)
                {
                    // Determie if we should place a cookie for this user
                    cookieOverride = IsPersistentCookieAllowed();

                    if (!cookieOverride)
                    {
                        // Raise a Robot visitor event
                        RepeatVisitorEvent rve = new RepeatVisitorEvent(RepeatVisitorType.VisitorRobot, string.Empty,
                            TDSessionManager.Current.Session.SessionID, DateTime.Now, pageId.ToString(),
                            string.Empty, HttpContext.Current.Request.Headers[Properties.Current[UserAgentHeaderProperty]]);

                        Logger.Write(rve);
                    }
                }

                HttpContext.Current.Session.Add(COOKIE_OVERRIDE, cookieOverride.ToString());
                #endregion
            }

            // Only do cookie test if we're allowed to give this user a cookie
            if ((bool.Parse((string)session[COOKIE_OVERRIDE])))
            {
                #region Cookie detection test
                // Check whether the test cookie has been returned
                if (HttpContext.Current.Request.Cookies[COOKIE_TESTCOOKIE] != null)
                {
                    // Remove the temporary cookie test value as it is no longer needed as we've established user allows cookies
                    session.Remove(COOKIE_TEST);

                    // Remove and then add in case the test cookie has persisted from a previous session
                    session.Remove(COOKIE_SUPPORT);
                    session.Add(COOKIE_SUPPORT, "true");

                    // And delete the test cookie as no longer needed
                    HttpContext.Current.Response.Cookies[COOKIE_TESTCOOKIE].Expires = DateTime.Now.AddDays(-1);
                }
                else if (session[COOKIE_TEST] != null)
                {
                    // As the temporary cookie test value is in the session, and we've come to this logic, 
                    // user does not support cookies
                    session.Remove(COOKIE_TEST);
                    session.Remove(COOKIE_SUPPORT);
                    session.Add(COOKIE_SUPPORT, "false");
                }


                // If session doesnt have the cookie support flag, then must be first time user has accessed Portal.
                // So place the Test cookie to be checked on subsequent page request.
                if (session[COOKIE_SUPPORT] == null)
                {
                    HttpCookie testCookie = new HttpCookie(COOKIE_TESTCOOKIE);
                    HttpContext.Current.Response.AppendCookie(testCookie);

                    // Add temporary value to session so it knows we're performing the cookie test on the next page request
                    session.Add(COOKIE_TEST, "In progress");

                    // Add the flag to session. By default, cookies are supported. 
                    // Needs to be added and true so we can raise new/repeat visitor event on first page hit
                    session.Add(COOKIE_SUPPORT, "true");
                }

                #endregion
            }
        }

        /// <summary>
        /// Method to check for persistent cookie and raise repeat visitor event report logs
        /// </summary>
        public void ProcessPersistentCookie()
        {
            bool cookiesEnabled = ((session[COOKIE_SUPPORT] != null) && (bool.Parse((string)session[COOKIE_SUPPORT])));

            // Do the processing
            if ((cookiesEnabled) && (PersistentCookie.IsPersistentCookieAvailable))
            {
                // If the cookie has the required information, then user has visited the site before (Repeat visitor)
                // otherwise its a new visitor!
                string cookieDomain = HttpContext.Current.Request.Url.Host; // Can't use the cookie value as it'll be null - not sure why it doesnt retain the value
                string cookieUniqueID = PersistentCookie.UniqueId;
                string cookieLastPageVisited = PersistentCookie.LastPageVisited;
                DateTime cookieLastVisitedDateTime = PersistentCookie.LastVisitedDateTime;

                string userAgentHeader = HttpContext.Current.Request.Headers[Properties.Current[UserAgentHeaderProperty]];

                if (string.IsNullOrEmpty(cookieUniqueID))
                {
                    // Raise new visitor event
                    RepeatVisitorEvent rve = new RepeatVisitorEvent(RepeatVisitorType.VisitorNew, string.Empty,
                        TDSessionManager.Current.Session.SessionID,
                        cookieLastVisitedDateTime, cookieLastPageVisited, cookieDomain, userAgentHeader);

                    Logger.Write(rve);
                }
                else if (cookieUniqueID != TDSessionManager.Current.Session.SessionID)
                {
                    // Raise repeat visitor event
                    RepeatVisitorEvent rve = new RepeatVisitorEvent(RepeatVisitorType.VisitorRepeat, cookieUniqueID,
                        TDSessionManager.Current.Session.SessionID,
                        cookieLastVisitedDateTime, cookieLastPageVisited, cookieDomain, userAgentHeader);

                    Logger.Write(rve);
                }
                // No need to raise an event if visitor continues to use the site

                // Update the cookie with latest information
                UpdatePersistentCookie();
            }
        }

        /// <summary>
        /// Updates the do not redirect flag to the cookie
        /// </summary>
        public void UpdateDoNotRedirectToCookie(bool flagValue)
        {
            bool cookiesEnabled = ((session[COOKIE_SUPPORT] != null) && (bool.Parse((string)session[COOKIE_SUPPORT])));

            if (cookiesEnabled)
            {
                PersistentCookie.DoNotRedirect = flagValue;
            }
        }

        /// <summary>
        /// Uses values from the Cookie to return the do not redirect flag value, defaults to false
        /// </summary>
        /// <returns></returns>
        public bool RetrieveDoNotRedirect()
        {
            //bool cookiesEnabled = ((session[SessionKey.CookieSupport] != null) && (bool.Parse((string)session[SessionKey.CookieSupport])));

            //if (cookiesEnabled)
            //{
            //    return PersistentCookie.DoNotRedirect;
            //}
            //else
            //{
            //    return false;
            //}

            // Just try and get the value, if it fails then it's false
            bool dnr = PersistentCookie.DoNotRedirect;
            return dnr;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method to check if the request subbmitted has a User Agent pattern that we want to give a Persistent Cookie.
        /// Gets patterns from properties to compare User Agent header against.
        /// </summary>
        /// <returns>Returns true if User Agent header does not contain any of the patterns</returns>
        private bool IsPersistentCookieAllowed()
        {
            // Get the property values
            string userAgentHeaderKey = Properties.Current[UserAgentHeaderProperty];
            string userAgentPatternsProperty = Properties.Current["Cookie.UserAgent.Robot.Pattern"];
            string userAgentRegExpsProperty = Properties.Current["Cookie.UserAgent.Robot.RegularExpression"];

            if ((!string.IsNullOrEmpty(userAgentHeaderKey)))
            {
                // Get the User Agent request value
                string userAgentHeader = HttpContext.Current.Request.Headers[userAgentHeaderKey];

                if (!string.IsNullOrEmpty(userAgentHeader))
                {
                    #region Check regular expression
                    if (!string.IsNullOrEmpty(userAgentRegExpsProperty))
                    {
                        // Get the patterns to test against
                        string[] userAgentRegExps = userAgentRegExpsProperty.Split(' ');

                        // Compare, if we match any reg exp then not allowed to send cookie to user
                        foreach (string userAgentRegExp in userAgentRegExps)
                        {
                            if (Regex.IsMatch(userAgentHeader, userAgentRegExp))
                                return false;
                        }
                    }
                    #endregion

                    #region Check patterns
                    if (!string.IsNullOrEmpty(userAgentPatternsProperty))
                    {
                        // Get the patterns to test against
                        string[] userAgentPatterns = userAgentPatternsProperty.Split(' ');

                        // Compare, if we match any pattern then not allowed to send cookie to user
                        foreach (string userAgentPattern in userAgentPatterns)
                        {
                            if (userAgentHeader.ToLower().Contains(userAgentPattern.ToLower()))
                                return false;
                        }
                    }
                    #endregion
                }
            }

            // Been through all the patterns and the user agent header does not match any, so ok to send cookie
            return true;
        }
        
        /// <summary>
        /// Method to add values to the Persistent cookie
        /// </summary>
        private void UpdatePersistentCookie()
        {
            PersistentCookie.ThemeId = TD.ThemeInfrastructure.ThemeProvider.Instance.GetTheme().Id;
            PersistentCookie.UniqueId = TDSessionManager.Current.Session.SessionID;
            PersistentCookie.LastVisitedDateTime = DateTime.Now.ToUniversalTime();
            PersistentCookie.LastPageVisited = pageId.ToString();

            #region Set the expiry date
            double expirySeconds = 0;
            int timeoutSeconds = 0;
            try
            {
                expirySeconds = Double.Parse(Properties.Current["Cookie.ExpiryTimeSpan.Seconds"]);
                timeoutSeconds = HttpContext.Current.Session.Timeout * 60;
            }
            catch
            {
                // Log an error
                OperationalEvent oe = new OperationalEvent(
                        TDEventCategory.Infrastructure, TDTraceLevel.Error,
                        "Error setting cookie expiry value: attempting to read property Cookie.ExpiryTimeSpan.Seconds, and Session.Timeout value");

                Logger.Write(oe);

                expirySeconds = (double)15778463; // Default the expiry to 6 months (15778463 seconds)
                timeoutSeconds = 1200; // Default session timeout to 20 minutes
            }

            // Set the expiry to be a property value + the session timeout value
            PersistentCookie.Expires = DateTime.Now.AddSeconds(expirySeconds + timeoutSeconds);

            #endregion
        }

        #endregion
    }
}