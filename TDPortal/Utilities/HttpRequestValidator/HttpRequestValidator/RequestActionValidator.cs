// *********************************************** 
// NAME         : RequestActionValidator.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 24/01/2011
// DESCRIPTION  : Class to maintain a count of specific actions for a request. If the count exceeds
//              : a threshold, then returns false to allow response to terminate. 
//              : This class is used to help prevent a "denial of service" application attack if multiple
//              : requests originate for the same session in a short period of time
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/HttpRequestValidator/HttpRequestValidator/RequestActionValidator.cs-arc  $
//
//   Rev 1.0   Feb 03 2011 10:14:54   mmodi
//Initial revision.
//
//   Rev 1.0   Jan 27 2011 16:24:20   mmodi
//Initial revision.
//

using System;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.Caching;
using AO.HttpRequestValidatorCommon;
using Microsoft.Web.Administration;

namespace AO.HttpRequestValidator
{
    /// <summary>
    /// Class to check for the number of requests from a session
    /// </summary>
    public class RequestActionValidator
    {
        #region Private members

        private static int DURATION;                // time in minutes to monitor for before restarting the check
        private static TimeSpan TIME_THRESHOLD;     // minimum acceptable time threshold between hits
        private static int HIT_THRESHOLD;           // max number of allowed hits if requests are below time threshold
        private static int HIT_REPEAT_THRESHOLD;    // max number hits indicating this is a repeat offender, used to ensure this offender is not allowed in until they stop
        private static string SESSION_COOKIE_NAME;  // ASP's session cookie name
        
        private static string MESSAGE_REQUEST_TERMINATED =
            "HTTP Request has been terminated. \r\n SessionID[{0}] \r\n UserHostAddress[{1}] \r\n UserHostName[{2}] \r\n UserAgent[{3}] \r\n RawUrl[{4}]. \r\n";
        private static string MESSAGE_REPEAT_OFFENDER =
            "HTTP Requests for this sesion have been terminated until requestor stops, repeat offender threshold has been tripped. \r\n First request hit was made at[{0}], Current datetime[{1}]. \r\n  \r\n";

        private static string CACHE_KEY_PREFIX = "Visitor"; // Appended to the key added to the cache
        
        private readonly string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        private static string EVENTLOG_NAME = string.Empty;
        private static string EVENTLOG_SOURCE = string.Empty;
        private static string EVENTLOG_MACHINE = string.Empty;
        private static string EVENTLOG_PREFIX = string.Empty;
        private static EventLogOutput eventLogInstance;

        #endregion

        #region Constructor

        /// <summary>
        /// Static Constructor
        /// </summary>
        static RequestActionValidator()
        {
            ConfigurationSection section = Microsoft.Web.Administration.WebConfigurationManager.GetSection(HttpRequestValidatorKeys.ConfigurationSection);

            #region Set values used by the validator

            try
            {
                // Duration to monitor for
                DURATION = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Monitor).GetAttribute(HttpRequestValidatorKeys.Duration_Minutes).Value;

                // Event log stuff
                EVENTLOG_NAME = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Name).Value;
                EVENTLOG_SOURCE = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Source).Value;
                EVENTLOG_MACHINE = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Machine).Value;
                EVENTLOG_PREFIX = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_MessagePrefix).Value;
            }
            catch
            {
                throw new Exception(string.Format("Failed to set RequestActionValidator Duration value, config[{0}] is missing or invalid",
                                HttpRequestValidatorKeys.Duration_Minutes));
            }

            try
            {
                // Max hit threshold
                HIT_THRESHOLD = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Thresholds).GetAttribute(HttpRequestValidatorKeys.Threshold_Hits_Max).Value;
            }
            catch
            {
                throw new Exception(string.Format("Failed to set RequestActionValidator Threshold Hits value, config[{0}] is missing or invalid",
                                HttpRequestValidatorKeys.Threshold_Hits_Max));
            }

            try
            {
                // Repeat offender hit threshold
                HIT_REPEAT_THRESHOLD = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Thresholds).GetAttribute(HttpRequestValidatorKeys.Threshold_Hits_RepeatOffender).Value;
            }
            catch
            {
                throw new Exception(string.Format("Failed to set RequestActionValidator Threshold Hits RepeatOffender value, config[{0}] is missing or invalid",
                                HttpRequestValidatorKeys.Threshold_Hits_RepeatOffender));
            }

            try
            {
                // Min time allowed between hits
                int timeSpanMilliseconds = (int)section.GetChildElement(HttpRequestValidatorKeys.Element_Thresholds).GetAttribute(HttpRequestValidatorKeys.Threshold_Time_Milliseconds_Min).Value;
                if (timeSpanMilliseconds <= 0)
                {
                    throw new Exception();
                }
                else
                {
                    TIME_THRESHOLD = new TimeSpan(0, 0, 0, 0, timeSpanMilliseconds);
                }
            }
            catch
            {
                throw new Exception(string.Format("Failed to set RequestActionValidator Threshold Time value, config[{0}] is missing or invalid",
                                HttpRequestValidatorKeys.Threshold_Time_Milliseconds_Min));
            }

            // Session cookie name (to retrieve session id)
            SESSION_COOKIE_NAME = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_Session).GetAttribute(HttpRequestValidatorKeys.SessionCookieName).Value;
            if (string.IsNullOrEmpty(SESSION_COOKIE_NAME))
            {
                throw new Exception(string.Format("Failed to set RequestActionValidator SessionCookieName value, config[{0}] is missing or invalid",
                            HttpRequestValidatorKeys.SessionCookieName));
            }

            #endregion

            #region Setup the Event log

            // Get an event log instance
            eventLogInstance = EventLogOutput.Instance(EVENTLOG_NAME, EVENTLOG_SOURCE, EVENTLOG_MACHINE, EVENTLOG_PREFIX);

            // Output the current configuration for this validator
            StringBuilder sb = new StringBuilder("RequestActionValidator initialised");
            sb.AppendLine();
            sb.Append("Sampling period (mins): ");
            sb.AppendLine(DURATION.ToString());
            sb.Append("Cookie name: ");
            sb.AppendLine(SESSION_COOKIE_NAME);
            sb.Append("Inappropriate hit rate (ms): ");
            sb.AppendLine(TIME_THRESHOLD.TotalMilliseconds.ToString());
            sb.Append("Lower limit (rolling block): ");
            sb.AppendLine(HIT_THRESHOLD.ToString());
            sb.Append("Upper limit (permanent block): ");
            sb.AppendLine(HIT_REPEAT_THRESHOLD.ToString());
            eventLogInstance.WriteEvent(sb.ToString(), EventLogEntryType.Information);

            #endregion
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RequestActionValidator()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method which checks if the current session has submitted requests in a period of time 
        /// that has exceeded an allowed threshold
        /// </summary>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public bool IsValid(HttpContext context)
        {

            // Get the session id from the session cookie
            HttpCookie sessionCookie = RetrieveRequestCookie(context.Request, SESSION_COOKIE_NAME);

            // If there's no session, then the request is probably going through the session handshake, 
            // therefore allow it 
            if (sessionCookie != null)
            {
                // Session id must be 24 characters, otherwise may be some kind of tampering with
                // session cookie. In future, could add more robust validation
                if (sessionCookie.Value.Length < 24)
                {
                    return false;
                }

                string sessionID = sessionCookie.Value.Substring(0, 24);
                string key = CACHE_KEY_PREFIX + sessionID;

                Cache cache = context.Cache;

                // Get hit details from cache
                HitInfo hit = (HitInfo)(cache[key] ?? new HitInfo());

                // Add to web cache if first visit, specifying an absolute expiration time.
                // This ensures any potential blocking occurs for a configured time and not indefinitely
                if (hit.Hits == 0)
                {
                    cache.Add(key, hit, null, DateTime.Now.AddMinutes(DURATION), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }

                // Get how long its been since the last made a request
                TimeSpan lastRequestTimeSpan = TimeSinceLastRequest(hit.LastHit);

                #region Validation

                // Perform the validation
                if (hit.ForceTerminate)
                {
                    #region Repeat offender logic

                    // This session hit has exceeded thresholds and therefore is denied entry
                    if (!hit.RepeatOffender)
                    {
                        if (hit.Hits < Int32.MaxValue)
                            hit.Hits++;

                        if (hit.Hits > HIT_REPEAT_THRESHOLD)
                        {
                            // This is a repeat offender, raise another event
                            // and set flag to not log again
                            hit.RepeatOffender = true;

                            // Also update object in cache to have sliding datetime.
                            // This ensures the offender is denied access until they stop
                            cache.Insert(key, hit, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, DURATION, 0), CacheItemPriority.Normal, null);

                            eventLogInstance.WriteEvent(
                                    string.Format(MESSAGE_REPEAT_OFFENDER,
                                        hit.FirstHit.ToString(dateTimeFormat),
                                        hit.LastHit.ToString(dateTimeFormat))
                                        +
                                    string.Format(MESSAGE_REQUEST_TERMINATED,
                                        sessionID,
                                        context.Request.UserHostAddress,
                                        context.Request.UserHostName,
                                        context.Request.UserAgent,
                                        context.Request.RawUrl),
                                    EventLogEntryType.Error);
                        }
                    }

                    #endregion

                    return false;
                }
                // First check: was the previous request within the time threshold, 
                // i.e. this request was submitted too "quickly"
                else if (lastRequestTimeSpan < TIME_THRESHOLD)
                {
                    hit.Hits++;

                    // Second check: have there been too many "quick" requests
                    if (hit.Hits > HIT_THRESHOLD)
                    {
                        // Time and Hits thresholds exceeded.
                        // Prevent any future requests from succeeding. 
                        // Requests for this session will be allowed again when this HitInfo object
                        // has been removed from the cache
                        hit.ForceTerminate = true;

                        // Write an event to let administrator know a request was denied
                        eventLogInstance.WriteEvent(
                                    string.Format(MESSAGE_REQUEST_TERMINATED,
                                        sessionID,
                                        context.Request.UserHostAddress,
                                        context.Request.UserHostName,
                                        context.Request.UserAgent,
                                        context.Request.RawUrl),
                                    EventLogEntryType.Error);

                        return false;
                    }
                }
                else
                {
                    // Reset the hit count as request not within the threshold
                    hit.Hits = 1;
                }

                #endregion
            }
            
            return true;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Retrieves the named cookie from the current Request
        /// </summary>
        /// <param name="currentRequest"></param>
        /// <param name="cookieName"></param>
        /// <returns>Null if not found</returns>
        private HttpCookie RetrieveRequestCookie(HttpRequest currentRequest, string cookieName)
        {
            HttpCookieCollection cookieCollection = currentRequest.Cookies;

            return cookieCollection[cookieName];
        }

        /// <summary>
        /// Returns a TimeSpan value for the difference between datetime now and the last request datatime
        /// </summary>
        /// <param name="lastRequestDateTime"></param>
        /// <returns></returns>
        private TimeSpan TimeSinceLastRequest(DateTime lastRequestDateTime)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(lastRequestDateTime);

            return timeSpan;
        }
        
        #endregion
    }

    #region Class - HitInfo

    /// <summary>
    /// Class to keep track of a hit count
    /// </summary>
    [Serializable]
    public class HitInfo
    {
        private int hitCount;
        private DateTime firstHit;
        private DateTime lastHit;
        private bool forceTerminate = false;
        private bool repeatOffender = false;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public HitInfo()
        {
            hitCount = 0;
            firstHit = DateTime.Now;
            lastHit = DateTime.MinValue;
            forceTerminate = false;
            repeatOffender = false;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Number of hits
        /// </summary>
        public int Hits
        {
            get { return hitCount; }
            set
            {
                hitCount = value;
                lastHit = DateTime.Now;
            }
        }

        /// <summary>
        /// Read. Datetime of the first hit
        /// (Write provided for serializer)
        /// </summary>
        public DateTime FirstHit
        {
            get { return firstHit; }
            set { firstHit = value; }
        }

        /// <summary>
        /// Read. Last datetime the hit was updated
        /// (Write provided for serializer)
        /// </summary>
        public DateTime LastHit
        {
            get { return lastHit; }
            set { lastHit = value; }
        }

        /// <summary>
        /// Read/Write. Flag indicating if the hits should be terminated
        /// </summary>
        public bool ForceTerminate
        {
            get { return forceTerminate; }
            set { forceTerminate = value; }
        }

        /// <summary>
        /// Read/Write. Flag indicating if this is a repeat offender
        /// </summary>
        public bool RepeatOffender
        {
            get { return repeatOffender; }
            set { repeatOffender = value; }
        }

        #endregion
    }

    #endregion
}
