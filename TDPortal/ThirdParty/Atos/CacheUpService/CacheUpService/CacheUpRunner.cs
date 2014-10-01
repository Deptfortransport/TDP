#region Amendment history
// *********************************************** 
// NAME			: $Workfile:   CacheUpRunner.cs  $
// AUTHOR		: Peter Norell
// DATE CREATED	: 01/11/2007
// REVISION		: $Revision:   1.3  $
// DESCRIPTION	: The runner class for doing the work for the cache service.
// ************************************************ 
// $Log:   P:\archives\Codebase\WebTIS\CacheUpService\CacheUpRunner.cs-arc  $ 
//
//   Rev 1.3   Jan 15 2010 16:01:54   a.windley
//Updated by TDP (m.modi):
//- The session can now be retained across multiple URL web requests. The existing Retain cookies logic was creating a new session for each URL even when the flag was set to true. Therefore to continue with a new session being created for each URL, the flag .RetainCookies in the Properties file should now be set to false.
//- A new property to specify the UserAgent value in the web request. If property not included in the Properties file, then no value is set as per current behaviour.
//
//   Rev 1.2   Nov 05 2007 11:40:46   p.norell
//Fixed error with credentials handling for when domain are supplied.
//
//   Rev 1.1   Nov 02 2007 16:57:50   p.norell
//Updated for action taking depending on consequences of the testing.
//
//   Rev 1.0   Nov 02 2007 15:13:08   p.norell
//Initial Revision
#endregion
#region Imports
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections;
using WT.Common.Logging;
using System.Web;
using System.Threading;
using Logger = System.Diagnostics.Trace;
using System.IO;
using System.Text.RegularExpressions;
using WT.Properties;
using System.Runtime.CompilerServices;
#endregion


namespace WT.CacheUpService
{
    /// <summary>
    /// The runner class for doing the work for the cache service.
    /// </summary>
    public class CacheUpRunner
    {
        #region Constants
        /// <summary>
        /// Default polling interval is one hour
        /// </summary>
        /// 
        const int DEFAULT_POLLINGINTERVAL = 0;
        /// <summary>
        /// 30 Seconds default timeout
        /// </summary>
        const int DEFAULT_TIMEOUTSECONDS = 30;

        // Exit statuses
        const int SUCCESS = (int)WTTraceLevel.None;

        #endregion

        #region Local declarations
        /// <summary>
        /// The list of urls to check
        /// </summary>
        List<CupUrlInfo> urls = null;

        /// <summary>
        /// The running thread, if such exists, for the program
        /// </summary>
        Thread runningThread = null;

        /// <summary>
        /// If the service is done or not
        /// </summary>
        bool done = false;
        #endregion

        #region Constructors
        /// <summary>
        /// The public constructor
        /// </summary>
        /// <param name="list">The list of URL's that should be monitored</param>
        public CacheUpRunner(List<CupUrlInfo> list)
        {
            urls = list;
        }
        #endregion
        
        #region Public properties
        /// <summary>
        /// If the service should finish up the processing or not
        /// </summary>
        public bool Done
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get { return done; }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set { done = value; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// A start method to use for ThreadStart - this will ignore the exit code returned by the Run method
        /// </summary>
        public void Start()
        {
            Run();
        }

        /// <summary>
        /// Join the running thread with the job thread
        /// </summary>
        public void Join()
        {
            #region Join on the current running thread - or interrupt it if it is sleeping
            if (runningThread != null)
            {
                try
                {
                    if (runningThread.ThreadState == ThreadState.WaitSleepJoin)
                    {
                        runningThread.Interrupt();
                    }
                    else
                    {
                        // Ad-hoc default joining time on the current thread set to 20 seconds
                        // runningThread.Join(20000);
                    }
                }
                catch (Exception exc)
                {
                    #region Fatal exception - log
                    Logger.Write(new OperationalEvent(WTTraceLevel.Verbose, WTTraceLevel.None,
                        "No Session",
                        "Fatal exception", exc));
                    #endregion
                }
            }
            #endregion
        }

        /// <summary>
        /// The method will block until it deems it is finished.  For a service with polling interval, this 
        /// is never finished and loops forever until it is told to wrap up.
        /// 
        /// <see>Done</see>
        /// </summary>
        public int Run()
        {
            #region Locally declared parameters prior to start-up of any polling
            // Remember the current thread - this is required for later handling
            // during shutdown of the process
            runningThread = Thread.CurrentThread;
            
            int returnValue = SUCCESS;

            int pollingInterval = DEFAULT_POLLINGINTERVAL;
            #endregion

            // As long as we are not done - loop here
            while (!done)
            {
                #region Sleep for configured amount of time
                // Recheck polling interval
                try
                {
                    pollingInterval = int.Parse(PropertyService.Current["CacheUpService.PollingIntervalSeconds"]) * 1000;
                }
                catch
                {
                    pollingInterval = DEFAULT_POLLINGINTERVAL;
                    // Log and continue;
                }
                // If polling interval is set to 0, then exit loop at the end of this request
                if (pollingInterval == 0)
                {
                    done = true;
                }
                else
                {
                    // Sleep
                    try
                    {
                        Thread.Sleep(pollingInterval);
                    }
                    catch (ThreadInterruptedException tie)
                    {
                        // Do nothing with tiec
                        if (done)
                        {
                            return returnValue;
                        }
                        Logger.Write(new OperationalEvent(WTTraceLevel.Verbose, WTTraceLevel.None,
                            "No Session",
                            "Application interrupted - this is normally because it is shutting down. Message[" + tie.Message + "]", tie));
                    }
                }
                #endregion

                #region Local parameters
                CookieContainer cookieContainer = new CookieContainer();
                CookieCollection hc = null;
                HttpWebRequest webReq = null;
                HttpWebResponse webRes = null;
                WTTraceLevel currentSeverity = WTTraceLevel.Error;
                string userAgent = PropertyService.Current["CacheUpService.WebRequest.UserAgent"];
                #endregion

                #region Loop through all of the urls
                foreach (CupUrlInfo urlInfo in urls)
                {
                    DateTime start = DateTime.Now;
                    WTTraceLevel latestReturnValue = WTTraceLevel.Verbose;
                    try
                    {                        
                        // Remember severity
                        currentSeverity = urlInfo.Severity;

                        #region Create request
                        webReq = (HttpWebRequest)WebRequest.Create(urlInfo.UrlAddress);
                        webReq.AllowAutoRedirect = urlInfo.FollowRedirect;
                        webReq.KeepAlive = urlInfo.KeepAlive;
                        webReq.UserAgent = userAgent;

                        // Timeout in milliseconds
                        webReq.Timeout = (urlInfo.TimeoutSeconds == 0 ? DEFAULT_TIMEOUTSECONDS : urlInfo.TimeoutSeconds) * 1000;

                        if (!urlInfo.RetainCookies)
                        {
                            // Reset the cookie collection
                            cookieContainer = new CookieContainer();
                        }

                        // Give a reference to the cookie collection, allows retaining of session accross URLs in 
                        // the webrequests (if new instance isn't created above)
                        webReq.CookieContainer = cookieContainer;
                        
                        if (hc != null && urlInfo.RetainCookies)
                        {
                            // System.Net.HttpCookieContainer hcc = new HttpCookieContainer();
                            foreach (Cookie cookie in hc)
                            {
                                if (WTTraceSwitch.TraceVerbose)
                                {
                                    Logger.Write(new OperationalEvent(WTTraceLevel.Verbose, WTTraceLevel.None,
                                        "No Session",
                                        urlInfo.Name +
                                        "Cookie[" + cookie.Name +
                                        "] has been set for url [" + urlInfo.UrlAddress + "]" +
                                        " Cookie Value [" + cookie.Value + "]"
                                        ));
                                }
                            }
                        }

                        // Authentication credentials
                        if (!string.IsNullOrEmpty( urlInfo.Username) )
                        {
                            webReq.PreAuthenticate = true;

                            // Use credential cache if the URL being called includes redirects, to ensure
                            // the credentials are passed through the redirects
                            CredentialCache credCache = new CredentialCache();

                            if (string.IsNullOrEmpty(urlInfo.Domain))
                            {
                                credCache.Add(new Uri(urlInfo.UrlAddress), urlInfo.AuthType, 
                                    new NetworkCredential(urlInfo.Username, urlInfo.Password));
                            }
                            else
                            {
                                credCache.Add(new Uri(urlInfo.UrlAddress), urlInfo.AuthType, 
                                    new NetworkCredential(urlInfo.Username, urlInfo.Password, urlInfo.Domain));
                            }

                            webReq.Credentials = credCache;
                        }
                        #endregion

                        #region Get and process response

                        try
                        {
                            webRes = (HttpWebResponse)webReq.GetResponse();
                        }
                        catch (WebException wex)
                        {
                            #region Resubmit request if required

                            // If a web exception is thrown containing a status code which is in the
                            // retry list, then resubmit the request.
                            // This is to allow a request to an "authenticated" site to succeed, as 
                            // a second submit containing the returned authenticated cookie is needed
                            
                            HttpWebResponse errResponse = (HttpWebResponse)wex.Response;
                            int errStatusCode = (int)errResponse.StatusCode;

                            // Check if retry should be attempted
                            if (urlInfo.RetryCodes.Contains(errStatusCode.ToString()))
                            {
                                #region Rebuild request
                                // Need to create a new request as doesnt allow calling GetResponse
                                // on the same web request object
                                HttpWebRequest webReqRetry = (HttpWebRequest)WebRequest.Create(urlInfo.UrlAddress);
                                webReqRetry.AllowAutoRedirect = urlInfo.FollowRedirect;
                                webReqRetry.KeepAlive = urlInfo.KeepAlive;
                                webReqRetry.UserAgent = userAgent;
                                webReqRetry.Timeout = (urlInfo.TimeoutSeconds == 0 ? DEFAULT_TIMEOUTSECONDS : urlInfo.TimeoutSeconds) * 1000;
                                webReqRetry.CookieContainer = cookieContainer;

                                #region Authentication credentials
                                if (!string.IsNullOrEmpty( urlInfo.Username) )
                                {
                                    webReqRetry.PreAuthenticate = true;
                                    CredentialCache credCache = new CredentialCache();
                                    if (string.IsNullOrEmpty(urlInfo.Domain))
                                    {
                                        credCache.Add(new Uri(urlInfo.UrlAddress), urlInfo.AuthType,
                                            new NetworkCredential(urlInfo.Username, urlInfo.Password));
                                    }
                                    else
                                    {
                                        credCache.Add(new Uri(urlInfo.UrlAddress), urlInfo.AuthType,
                                            new NetworkCredential(urlInfo.Username, urlInfo.Password, urlInfo.Domain));
                                    }

                                    webReq.Credentials = credCache;
                                }
                                #endregion
                                #endregion

                                webRes = (HttpWebResponse)webReqRetry.GetResponse();
                            }
                            else
                            {
                                throw;
                            }

                            #endregion
                        }
                        
                        #region Status code matching
                        int statusCode = (int)webRes.StatusCode;
                        if (!urlInfo.AcceptedCodes.Contains(statusCode.ToString() ))
                        {
                            // Woah! Bad news - raise error here

                            Logger.Write(new OperationalEvent(currentSeverity, WTTraceLevel.None,
                                "No Session",
                                urlInfo.Name + 
                                "The status code[" + statusCode +
                                "] does not match the list of accepted status codes[" + urlInfo.AcceptedCodes +
                                "] for url [" + urlInfo.UrlAddress + "]"
                                ));
                            latestReturnValue = urlInfo.Severity;
                        }
                        #endregion

                        #region Page scan matching
                        else
                        {
                            if (urlInfo.PageScan != null)
                            {
                                Regex reg = new Regex(urlInfo.PageScan);
                                // Scan page for information
                                using (Stream s = webRes.GetResponseStream())
                                {
                                    using (StreamReader sr = new StreamReader(s))
                                    {
                                        string allData = sr.ReadToEnd();
                                        Match m = reg.Match(allData);
                                        if (m.Success != urlInfo.PageScanPositive)
                                        {
                                            // Log
                                            Logger.Write(new OperationalEvent(currentSeverity, WTTraceLevel.None,
                                                "No Session",
                                                urlInfo.Name + 
                                                "The page scan[" + urlInfo.PageScan +
                                                "] failed for url [" + urlInfo.UrlAddress +
                                                "] Expected["+urlInfo.PageScanPositive+
                                                "] Received["+m.Success+"]"
                                                ));
                                            latestReturnValue = urlInfo.Severity;
                                        }
                                    }
                                }
                            }

                        }
                        #endregion

                        #region Process cookies
                        hc = webRes.Cookies;
                        if (WTTraceSwitch.TraceVerbose)
                        {
                            foreach (Cookie cookie in hc)
                            {
                                Logger.Write(new OperationalEvent(WTTraceLevel.Verbose, WTTraceLevel.None,
                                    "No Session",
                                    urlInfo.Name + 
                                    "Received cookie[" + cookie.Name +
                                    "] for url [" + urlInfo.UrlAddress + "]" +
                                    " Cookie Value [" + cookie.Value + "]"
                                    ));
                            }
                        }                        
                        #endregion
                                                
                        #region End time logging
                        TimeSpan totalTime = DateTime.Now - start;
                        // Log time start and time end here for information purposes
                        Logger.Write(new OperationalEvent(WTTraceLevel.Info, WTTraceLevel.None,
                                            "No Session",
                                            urlInfo.Name + 
                                            "Cache Up Service has checked url[" + urlInfo.UrlAddress +
                                            "]. It took [" + ((long)totalTime.TotalMilliseconds) +
                                            "] milliseconds to complete."
                                            ));
                        #endregion

                        #endregion
                    }
                    catch (ThreadInterruptedException tie)
                    {
                        #region Thread interruptions should only occur during shutdown of service
                        // If we are interrupted here with a thread interrupt - then 
                        // the application is shutting down and should just nicely quit
                        // Log message to ensure it is know that shutdown was done
                        Logger.Write(new OperationalEvent( WTTraceLevel.Verbose, WTTraceLevel.None,
                            "No Session",
                            "Application interrupted - this is normally because it is shutting down. Message["+tie.Message+"]",tie));
                        #endregion                        
                    }
                    catch (Exception exc)
                    {
                        #region Fatal exception - log and continue on to the next item
                        // Log this - URL severity
                        Logger.Write(new OperationalEvent(currentSeverity, WTTraceLevel.None,
                            "No Session",
                            urlInfo.Name + 
                            "Fatal exception when accessing page[" + urlInfo.UrlAddress +
                            "]. Message[" + exc.Message +
                            "]", exc));
                        latestReturnValue = urlInfo.Severity;
                        #endregion
                    }
                    finally
                    {
                        #region Close webresponse if such exists
                        if (webRes != null)
                        {

                            try
                            {
                                webRes.Close();
                            }
                            catch
                            {
                            }
                        }
                        #endregion
                    }

                    #region Action handling
                    try
                    {
                        if (urlInfo.Action != null)
                        {
                            urlInfo.Action.ExecuteOn(latestReturnValue, urlInfo.ActionSettingsId);
                        }
                    }
                    catch (Exception exc)
                    {
                        // Log this
                        Logger.Write(new OperationalEvent(currentSeverity, WTTraceLevel.None,
                            "No Session",
                            urlInfo.Name + 
                            "Action failed for page[" + urlInfo.UrlAddress +
                            "]. Message[" + exc.Message +
                            "]", exc));
                    }
                    #endregion

                    #region Return value update
                    returnValue = Math.Min((int)latestReturnValue, returnValue);
                    #endregion 
                }
                #endregion
            }
            if (returnValue == (int)WTTraceLevel.Verbose)
            {
                returnValue = 0;
            }
            return returnValue;
        }
        #endregion
    }
}
