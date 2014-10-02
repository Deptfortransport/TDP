// *********************************************** 
// NAME             : SiteRedirectHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Aug 2013
// DESCRIPTION  	: SiteRedirectHelper class to redirect Main to Mobile if appropriate
// ************************************************
// 

using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.Web.Adapters;
using System.Collections.Specialized;
using TransportDirect.CommonWeb.Helpers;
using System.Globalization;

namespace TransportDirect.UserPortal.Web.Code
{
    /// <summary>
    /// SiteRedirectHelper class to redirect Main to Mobile if appropriate
    /// </summary>
    public class SiteRedirectHelper
    {
        #region local vars

        private static string regexB = string.Empty;
        private static string regexV = string.Empty;
        private static object synchLock = new object();
        private static bool initialised = false;

        /// <summary>
        /// Do not redirect to site, values true or false
        /// </summary>
        private static string QueryStringKey_DoNotRedirect = "dnr";

        /// <summary>
        /// Redirected to site, values "w" (web) or "m" (mobile)
        /// </summary>
        public static string QueryStringKey_RedirectedTo = "r"; 

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Top level method to redirect Main to Mobile if appropriate
        /// </summary>
        /// <param name="request">the http request</param>
        /// <param name="response">the http response</param>
        /// <param name="context">the http context</param>
        /// <param name="session">the asp session</param>
        /// <param name="pageId">the page Id</param>
        public static void PerformSiteRedirect(HttpRequest request, HttpResponse response, HttpContext context, HttpSessionState session, PageId pageId)
        {
            StoreDoNotRedirectParameterInCookie(request);

            if (CheckForMobileDevice(request))
            {
                if (!DoNotRedirect(request))
                {
                    if (IsNewSession(context, session, request))
                    {
                        RedirectToMobile(request, response, pageId);
                    }
                    else if (IsTimedOutSession(context, session, request))
                    {
                        RedirectToMobile(request, response, pageId);
                    }
                }
            }
        }

        #endregion

        #region Private Static Methods
        /// <summary>
        /// Adds the do not redirect querystring parameter to the cookie
        /// </summary>
        /// <param name="request">the http request</param>
        private static void StoreDoNotRedirectParameterInCookie(HttpRequest request)
        {
            if (request.QueryString[QueryStringKey_DoNotRedirect] != null)
            {
                CookieHelper helper = new CookieHelper();
                bool flag = false;
                if (bool.TryParse(request.QueryString[QueryStringKey_DoNotRedirect], out flag))
                {
                    helper.UpdateDoNotRedirectToCookie(flag);
                }
            }
        }

        /// <summary>
        /// Checks to see if a redirect should be blocked
        /// </summary>
        /// <param name="request">the request</param>
        /// <returns>true/false</returns>
        private static bool DoNotRedirect(HttpRequest request)
        {
            if (request.QueryString[QueryStringKey_DoNotRedirect] == null)
            {
                // Check for cookie 
                CookieHelper cookieHelper = new CookieHelper();

                // defaults to false if not present
                return cookieHelper.RetrieveDoNotRedirect();
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks the http user agent for signs of a mobile device
        /// </summary>
        /// <param name="request">the http request</param>
        /// <returns>true/false</returns>
        private static bool CheckForMobileDevice(HttpRequest request)
        {
            Initialise();

            // The rest of the code in this method is covered by the UNLICENSE agreement so is 
            // part of the public domain and not owned by the project or customer (including the regex strings)
            string userAgent = request.ServerVariables["HTTP_USER_AGENT"];

            if (!string.IsNullOrEmpty(userAgent))
            {
                // Regex values come from http://detectmobilebrowsers.com version taken on 3/7/2012
                Regex b = new Regex(regexB, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Regex v = new Regex(regexV, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                if ((b.IsMatch(userAgent) || v.IsMatch(userAgent.Substring(0, 4))))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Perform the transfer to the mobile site, to the appropriate page and with querystring parameters if appropriate
        /// </summary>
        /// <param name="request">the http request</param>
        /// <param name="response">the http response</param>
        /// <param name="pageId">the page Id to redirect from</param>
        private static void RedirectToMobile(HttpRequest request, HttpResponse response, PageId pageId)
        {
            /* Redirection rules:
             * - if coming from main input with no page landing params go to mobile menu page
             * - if coming from main input or results page with page landing params go to mobile input page with
             *      persisted params. default is PT mode or if param indicates cycle then cycle mode
             * - all othe pages / scenarios go to mobile menu page with no persisted params
             * - all redirects will include the RedirectTo param (rt=m)
             */

            // Which mobile page we redirect to depends on the page we're on now
            PageId targetPageId = PageId.MobileDefault;

            // always include the RedirectedTo querystring parameter when redirecting (for google analytics)
            string outgoingQueryString = "?" + QueryStringKey_RedirectedTo + "=m";
            string incomingQueryString = string.Empty;

            if (!string.IsNullOrEmpty(request.ServerVariables["QUERY_STRING"]))
            {
                incomingQueryString = GetLandingPageQueryString(request.QueryString);
            }
            else
            {
                // Check if there are any landing page params in the "POST" request form,
                // and append as the querystring
                incomingQueryString = GetLandingPageQueryString(request.Form);
            }
            
            // for logic see top of method
            switch (pageId)
            {
                case PageId.JourneyPlannerInput:
                    if (incomingQueryString != string.Empty)
                    {
                        targetPageId = PageId.MobileInput;
                        outgoingQueryString += "&" + incomingQueryString;
                    }
                    break;
                case PageId.JPLandingPage:
                    if (incomingQueryString != string.Empty)
                    {
                        targetPageId = PageId.MobileInput;
                        outgoingQueryString += "&" + incomingQueryString;
                    }
                    break;
            }

            // Get the page transfer details
            string url = string.Empty;
            try
            {
                // Get transfer details
                IPageController pageController = (IPageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
                PageTransferDetails transferDetail = pageController.GetPageTransferDetails(targetPageId);
                url = transferDetail.PageUrl;
            }
            catch (Exception ex)
            {
                string message = string.Format("An error occurred retrieving PageTransferDetails for PageId[{0}]. Page has not been declared in the sitemap configuration.", targetPageId);
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message, ex));

                // This is serious as a page/control is attempting to go to requested page and it hasn't been setup
                throw;
            }

            // Include query parameters
            url = url + outgoingQueryString;

            // Log RedirectMobile MIS event
            LogMisEvent();

            // Do the redirect
            response.Redirect(url);
        }

        /// <summary>
        /// Log a page entry event to signify a transfer is happening
        /// </summary>
        private static void LogMisEvent()
        {
            // Log page entry event (to signify a re-direct)
            PageEntryEvent logPage = new PageEntryEvent(PageId.RedirectMobile, TDSessionManager.Current.Session.SessionID, false);
            Logger.Write(logPage);
        }

        /// <summary>
        /// Retrieves the regex patterns
        /// </summary>
        private static void Initialise()
        {
            if (!initialised)
            {
                lock (synchLock)
                {
                    regexB = Properties.Current["SiteRedirector.RegexB"];
                    regexV = Properties.Current["SiteRedirector.RegexV"];

                    initialised = true;
                }
            }
        }

        /// <summary>
        /// Check if this is a new session
        /// </summary>
        /// <param name="context">the http context</param>
        /// <param name="session">the asp session</param>
        /// <param name="request">the http request</param>
        /// <returns>true/false</returns>
        private static bool IsNewSession(HttpContext context, HttpSessionState session, HttpRequest request)
        {
            // The Request and Response appear to both share the 
            // same cookie collection.  If a cookie is set in the Reponse, it is 
            // also immediately visible to the Request collection.  This just means that 
            // since the ASP.Net_SessionID is set in the Session HTTPModule (which 
            // has already run), that we can't use our own code to see if the cookie was 
            // actually sent by the agent with the request using the collection. Check if 
            // the given page supports session or not (this tested as reliable indicator 
            // if EnableSessionState is true), should not care about a page that does 
            // not need session
            if (context.Session != null)
            {
                // The IsNewSession is more advanced then simply checking if 
                // a cookie is present, it does take into account a session timeout
                if (session.IsNewSession)
                {
                    // If it says it is a new session, but an existing cookie exists, then it must 
                    // have timed out (can't use the cookie collection because even on first 
                    // request it already contains the cookie (request and response
                    // seem to share the collection))
                    string cookieHeader = request.Headers["Cookie"];
                    if ((null != cookieHeader) && (cookieHeader.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        // Is a session timeout
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // A new user
                return true;
            }

            //return SessionHelper.DetectNewSession(context, session, request);
        }

        /// <summary>
        /// Check if this is an active session
        /// </summary>
        /// <param name="context">the http context</param>
        /// <param name="session">the asp session</param>
        /// <param name="request">the http request</param>
        /// <returns>true/false</returns>
        private static bool IsActiveSession(HttpContext context, HttpSessionState session, HttpRequest request)
        {
            // The Request and Response appear to both share the 
            // same cookie collection.  If a cookie is set in the Reponse, it is 
            // also immediately visible to the Request collection.  This just means that 
            // since the ASP.Net_SessionID is set in the Session HTTPModule (which 
            // has already run), that we can't use our own code to see if the cookie was 
            // actually sent by the agent with the request using the collection. Check if 
            // the given page supports session or not (this tested as reliable indicator 
            // if EnableSessionState is true), should not care about a page that does 
            // not need session
            if (context.Session != null)
            {
                // The IsNewSession is more advanced then simply checking if 
                // a cookie is present, it does take into account a session timeout
                if (session.IsNewSession)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                // A new user
                return false;
            }

            //return SessionHelper.DetectActiveSession(context, session, request);
        }

        /// <summary>
        /// check if this is a timed-out session
        /// </summary>
        /// <param name="context">the http context</param>
        /// <param name="session">the asp session</param>
        /// <param name="request">the http request</param>
        /// <returns>true/false</returns>
        private static bool IsTimedOutSession(HttpContext context, HttpSessionState session, HttpRequest request)
        {
            // The Request and Response appear to both share the 
            // same cookie collection.  If a cookie is set in the Reponse, it is 
            // also immediately visible to the Request collection.  This just means that 
            // since the ASP.Net_SessionID is set in the Session HTTPModule (which 
            // has already run), that we can't use our own code to see if the cookie was 
            // actually sent by the agent with the request using the collection. Check if 
            // the given page supports session or not (this tested as reliable indicator 
            // if EnableSessionState is true), should not care about a page that does 
            // not need session
            if (context.Session != null)
            {
                // The IsNewSession is more advanced then simply checking if 
                // a cookie is present, it does take into account a session timeout
                if (session.IsNewSession)
                {
                    // If it says it is a new session, but an existing cookie exists, then it must 
                    // have timed out (can't use the cookie collection because even on first 
                    // request it already contains the cookie (request and response
                    // seem to share the collection))
                    string cookieHeader = request.Headers["Cookie"];
                    if ((null != cookieHeader) && (cookieHeader.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        // Is a session timeout
                        return true;
                    }
                }
            }

            // Not a session timeout, it is a new or returning user
            return false;

            //return SessionHelper.DetectSessionTimeout(context, session, request);
        }

        /// <summary>
        /// Retrieves any landing page parameters (updating any values) 
        /// and returns a landing page query string suitable for TDP Mobile
        /// </summary>
        /// <param name="requestForm"></param>
        /// <returns></returns>
        private static string GetLandingPageQueryString(NameValueCollection requestParams)
        {
            NameValueCollection updatedRequestParams = new NameValueCollection();

            if (requestParams != null)
            {
                // Check each value for known landing page keys and update is necessary
                foreach (string key in requestParams.Keys)
                {
                    // Perform any updates where necessary
                    string value = requestParams[key];

                    try
                    {
                        switch (key)
                        {
                            case LandingPageHelperConstants.ParameterOutwardDate:
                            case LandingPageHelperConstants.ParameterReturnDate:
                                //TDP landing page "dt" value is in format "ddMMyyyy" but needs to be "yyyyMMdd" for mobile
                                DateTime date = DateTime.ParseExact(value, "ddMMyyyy", CultureInfo.InvariantCulture);
                                value = date.ToString("yyyyMMdd");
                                break;
                        }
                    }
                    catch
                    {
                        // Ignore exceptions, we're trying to handle known differences between TDP and Mobile,
                        // any values not recognised will be passed on and handled by mobile within its 
                        // landing page handler
                    }

                    updatedRequestParams.Add(key, value);
                }
            }

            LandingPageHelper lph = new LandingPageHelper();

            return lph.BuildLandingPageQueryStringFromParams(updatedRequestParams);
        }


        #endregion
    }
}