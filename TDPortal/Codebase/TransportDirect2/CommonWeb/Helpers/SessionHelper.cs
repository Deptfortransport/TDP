// *********************************************** 
// NAME             : SessionHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 13 Apr 2011
// DESCRIPTION  	: SessionHelper class to provide helper methods for updating the session
// ************************************************
// 

using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.SessionManager;
using TDP.Common.DataServices.TimeoutData;
using TDP.Common.ServiceDiscovery;
using TDP.Common.LocationService;

namespace TDP.Common.Web
{
    /// <summary>
    /// SessionHelper class to provide helper methods for updating the session
    /// </summary>
    public class SessionHelper
    {
        #region Private Fields

        private ITDPSessionManager sessionManager;
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public SessionHelper()
        {
            sessionManager = (TDPSessionManager)TDPSessionManager.Current;
        }

        #endregion

        #region Public Methods

        #region Session Timeout/New/Active Methods

        /// <summary>
        /// Detects if the session has timed-out.
        /// </summary>
        /// <param name="context">the http context</param>
        /// <param name="session">the asp session</param>
        /// <param name="request">the http request</param>
        /// <returns>true/false</returns>
        public static bool DetectSessionTimeout(HttpContext context, HttpSessionState session, HttpRequest request)
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
        }

        /// <summary>
        /// Check if this is a new session
        /// </summary>
        /// <param name="context">the http context</param>
        /// <param name="session">the asp session</param>
        /// <param name="request">the http request</param>
        /// <returns>true/false</returns>
        public static bool DetectNewSession(HttpContext context, HttpSessionState session, HttpRequest request)
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
        }

        /// <summary>
        /// Check if this is an active session
        /// </summary>
        /// <param name="context">the http context</param>
        /// <param name="session">the asp session</param>
        /// <param name="request">the http request</param>
        /// <returns>true/false</returns>
        public static bool DetectActiveSession(HttpContext context, HttpSessionState session, HttpRequest request)
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
        }

        /// <summary>
        /// Returns false if the session timeout message should not be displayed for the control id
        /// by checking the configuration for ignore timeout controls/pages.
        /// Default is true
        /// </summary>
        /// <param name="controlID"></param>
        /// <returns></returns>
        public bool DisplayTimeoutMessage(PageId pageId, string controlId)
        {
            // Default to show timeout
            bool displayTimeout = true;

            // Check if timeout message should be displayed
            PageTimeoutData pageTimeoutData = TDPServiceDiscovery.Current.Get<PageTimeoutData>(ServiceDiscoveryKey.PageTimeoutData);

            displayTimeout = pageTimeoutData.ShowTimeoutMessage(pageId, controlId);

            return displayTimeout;
        }

        /// <summary>
        /// Returns true if during session timeout the captured (postback) event should be 
        /// allowed to continue for the control, e.g. plan journey button clicked on input page
        /// by checking the configuration for timeout controls/pages.
        /// Default is false
        /// </summary>
        public bool AllowTimeoutEvent(PageId pageId, string controlId)
        {
            // Default to not allow (postback) event during session timeout
            bool allow = false;

            // Check if (postback) event can be allowed
            PageTimeoutData pageTimeoutData = TDPServiceDiscovery.Current.Get<PageTimeoutData>(ServiceDiscoveryKey.PageTimeoutData);

            allow = pageTimeoutData.AllowTimeoutEvent(pageId, controlId);

            return allow;
        }

        #endregion

        #region Journey Request/Result Methods

        /// <summary>
        /// Retrieves the TDPJourneyRequest from session, using the 
        /// JourneyRequestHash in the current InputPageState. Returns null if not found
        /// </summary>
        /// <returns></returns>
        public ITDPJourneyRequest GetTDPJourneyRequest()
        {
            ITDPJourneyRequest tdpJourneyRequest = null;

            InputPageState pageState = sessionManager.PageState;

            if (!string.IsNullOrEmpty(pageState.JourneyRequestHash))
            {
                string requestHash = pageState.JourneyRequestHash;

                tdpJourneyRequest = sessionManager.RequestManager.GetTDPJourneyRequest(requestHash);
            }

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Retrieves the TDPJourneyRequest from session, using the 
        /// JourneyRequestHash provided. Returns null if not found
        /// </summary>
        /// <returns></returns>
        public ITDPJourneyRequest GetTDPJourneyRequest(string journeyRequestHash)
        {
            ITDPJourneyRequest tdpJourneyRequest = null;

            if (!string.IsNullOrEmpty(journeyRequestHash))
            {
                tdpJourneyRequest = sessionManager.RequestManager.GetTDPJourneyRequest(journeyRequestHash);
            }

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Retrieves the TDPJourneyResult from session, using the 
        /// JourneyRequestHash provided. Returns null if not found
        /// </summary>
        /// <returns></returns>
        public ITDPJourneyResult GetTDPJourneyResult(string journeyRequestHash)
        {
            ITDPJourneyResult tdpJourneyResult = null;

            if (!string.IsNullOrEmpty(journeyRequestHash))
            {
                tdpJourneyResult = sessionManager.ResultManager.GetTDPJourneyResult(journeyRequestHash);
            }

            return tdpJourneyResult;
        }

        /// <summary>
        /// Checks the session if an TDPJourneyResult exists for the supplied journeyRequestHash,
        /// and the result contains journeys
        /// </summary>
        /// <param name="journeyRequestHash"></param>
        /// <returns></returns>
        public bool DoesTDPJourneyResultExist(string journeyRequestHash, bool removeInvalidResult)
        {
            bool resultExists = false;

            if (!string.IsNullOrEmpty(journeyRequestHash))
            {
                // Check for journey result
                resultExists = sessionManager.ResultManager.DoesResultExist(journeyRequestHash);

                // Check journeys exist in the result, otherwise
                if (resultExists)
                {
                    ITDPJourneyRequest journeyRequest = sessionManager.RequestManager.GetTDPJourneyRequest(journeyRequestHash);
                    ITDPJourneyResult journeyResult = sessionManager.ResultManager.GetTDPJourneyResult(journeyRequestHash);

                    // Journeys must exist, or if a message for journey exists, then some part must have failed.
                    // Or if its Venue-to-Venue, then remove because of a bug when "switching locations from/to",
                    // the request equates to the same journey request hash code, which therefore incorrectly states 
                    // the result exists but it could be for the "previous" request (better to be safe so remove!)
                    if ((journeyResult.OutwardJourneys.Count + journeyResult.ReturnJourneys.Count == 0)
                        || (journeyResult.Messages.Count > 0)
                        || (journeyRequest.LocationInputMode == LocationInputMode.VenueToVenue.ToString()))
                    {
                        // No journeys, so it probably failed last time it was planned
                        resultExists = false;

                        // Remove the "bad" result
                        if (removeInvalidResult)
                        {
                            sessionManager.ResultManager.RemoveTDPJourneyResult(journeyRequestHash);
                        }
                    }
                }
            }

            return resultExists;
        }

        /// <summary>
        /// Updates the sesssion with the TDPJourneyRequest, 
        /// and also sets the session PageState to indicate current TDPJourneyRequest to use
        /// </summary>
        /// <param name="tdpJourneyRequest"></param>
        public void UpdateSession(ITDPJourneyRequest tdpJourneyRequest)
        {
            // Add journey request into the session
            sessionManager.RequestManager.AddTDPJourneyRequest(tdpJourneyRequest);

            // Add to page state so current/next page knows which request it "should" be working with.
            // Request identifier is added to URL to also indicate which request to use, so this value 
            // provides a fallback incase it is missing, helps in certain scenarios (e.g. in tabbed browsing)
            sessionManager.PageState.JourneyRequestHash = tdpJourneyRequest.JourneyRequestHash;

            // Reset selected journeys
            sessionManager.PageState.JourneyIdOutward = -1;
            sessionManager.PageState.JourneyIdReturn = -1;

            // Reset leg detail expanded flags
            sessionManager.PageState.JourneyLegDetailExpandedOutward = false;
            sessionManager.PageState.JourneyLegDetailExpandedReturn = false;
        }

        /// <summary>
        /// Updates the sesssion with the journey request hash, 
        /// and also sets the session PageState to indicate current TDPJourneyRequest to use
        /// </summary>
        /// <param name="tdpJourneyRequest"></param>
        public void UpdateSession(string journeyRequestHash)
        {
            // Add to page state so current/next page knows which request it "should" be working with.
            // Request identifier is added to URL to also indicate which request to use, so this value 
            // provides a fallback incase it is missing, helps in certain scenarios (e.g. in tabbed browsing)
            sessionManager.PageState.JourneyRequestHash = journeyRequestHash;
        }

        #endregion

        #region Stop Event Methods

        /// <summary>
        /// Retrieves the Stop Event TDPJourneyRequest from session, using the 
        /// request hash in the current InputPageState. Returns null if not found
        /// </summary>
        /// <returns></returns>
        public ITDPJourneyRequest GetTDPStopEventRequest()
        {
            ITDPJourneyRequest tdpJourneyRequest = null;

            InputPageState pageState = sessionManager.PageState;

            if (!string.IsNullOrEmpty(pageState.StopEventRequestHash))
            {
                string requestHash = pageState.StopEventRequestHash;

                tdpJourneyRequest = sessionManager.StopEventRequestManager.GetTDPJourneyRequest(requestHash);
            }

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Retrieves the Stop Event TDPJourneyRequest from session, using the 
        /// request hash provided. Returns null if not found
        /// </summary>
        /// <returns></returns>
        public ITDPJourneyRequest GetTDPStopEventRequest(string stopEventRequestHash)
        {
            ITDPJourneyRequest tdpJourneyRequest = null;

            if (!string.IsNullOrEmpty(stopEventRequestHash))
            {
                tdpJourneyRequest = sessionManager.StopEventRequestManager.GetTDPJourneyRequest(stopEventRequestHash);
            }

            return tdpJourneyRequest;
        }

        /// <summary>
        /// Retrieves the Stop Event TDPJourneyResult from session, using the 
        /// request hash provided. Returns null if not found
        /// </summary>
        /// <returns></returns>
        public ITDPJourneyResult GetTDPStopEventResult(string stopEventRequestHash)
        {
            ITDPJourneyResult tdpJourneyResult = null;

            if (!string.IsNullOrEmpty(stopEventRequestHash))
            {
                tdpJourneyResult = sessionManager.StopEventResultManager.GetTDPJourneyResult(stopEventRequestHash);
            }

            return tdpJourneyResult;
        }

        /// <summary>
        /// Checks the session if an TDPJourneyResult exists for the supplied request hash,
        /// and the result contains journeys
        /// </summary>
        /// <param name="journeyRequestHash"></param>
        /// <returns></returns>
        public bool DoesTDPStopEventResultExist(string stopEventRequestHash, bool removeInvalidResult)
        {
            bool resultExists = false;

            if (!string.IsNullOrEmpty(stopEventRequestHash))
            {
                // Check for journey result
                resultExists = sessionManager.StopEventResultManager.DoesResultExist(stopEventRequestHash);

                // Check journeys exist in the result, otherwise
                if (resultExists)
                {
                    ITDPJourneyResult departureResult = sessionManager.StopEventResultManager.GetTDPJourneyResult(stopEventRequestHash);

                    if (departureResult.OutwardJourneys.Count + departureResult.ReturnJourneys.Count == 0)
                    {
                        // No journeys, so it probably failed last time it was planned
                        resultExists = false;

                        // Remove the "bad" result
                        if (removeInvalidResult)
                        {
                            sessionManager.StopEventResultManager.RemoveTDPJourneyResult(stopEventRequestHash);
                        }
                    }
                }
            }

            return resultExists;
        }

        /// <summary>
        /// Updates the sesssion with a Stop Event TDPJourneyRequest, 
        /// and also sets the session PageState to indicate current Stop Event TDPJourneyRequest to use
        /// </summary>
        /// <param name="tdpJourneyRequest"></param>
        public void UpdateSessionStopEvent(ITDPJourneyRequest tdpStopEventRequest)
        {
            // Add journey request into the session
            sessionManager.StopEventRequestManager.AddTDPJourneyRequest(tdpStopEventRequest);

            // Add to page state so current/next page knows which request it "should" be working with.
            // Request identifier is added to URL to also indicate which request to use, so this value 
            // provides a fallback incase it is missing, helps in certain scenarios (e.g. in tabbed browsing)
            sessionManager.PageState.StopEventRequestHash = tdpStopEventRequest.JourneyRequestHash;

            // Reset selected journeys
            sessionManager.PageState.StopEventJourneyIdOutward = -1;
            sessionManager.PageState.StopEventJourneyIdReturn = -1;

        }

        #endregion

        #region Earlier/Later Methods

        /// <summary>
        /// Method to reset all the Earlier and Later link flags in session
        /// </summary>
        /// <param name="isRiver"></param>
        public void ResetEarlierLaterLinkFlags(bool isRiver)
        {
            if (isRiver)
            {
                sessionManager.PageState.ShowEarlierLinkOutwardRiver = true;
                sessionManager.PageState.ShowLaterLinkOutwardRiver = true;
                sessionManager.PageState.ShowEarlierLinkReturnRiver = true;
                sessionManager.PageState.ShowLaterLinkReturnRiver = true;
            }
        }

        /// <summary>
        /// Method to update the earlier link flag in session
        /// </summary>
        public void UpdateEarlierLinkFlag(bool isOutward, bool isRiver, bool show)
        {
            if (isRiver)
            {
                if (isOutward)
                {
                    sessionManager.PageState.ShowEarlierLinkOutwardRiver = show;
                }
                else
                {
                    sessionManager.PageState.ShowEarlierLinkReturnRiver = show;
                }
            }
        }

        /// <summary>
        /// Method to update the earlier link flag in session
        /// </summary>
        public void UpdateLaterLinkFlag(bool isOutward, bool isRiver, bool show)
        {
            if (isRiver)
            {
                if (isOutward)
                {
                    sessionManager.PageState.ShowLaterLinkOutwardRiver = show;
                }
                else
                {
                    sessionManager.PageState.ShowLaterLinkReturnRiver = show;
                }
            }
        }

        #endregion

        #region Stop Information Methods

        /// <summary>
        /// Retrieves the Stop Location from session
        /// </summary>
        /// <returns></returns>
        public TDPStopLocation GetStopLocation()
        {
            InputPageState pageState = sessionManager.PageState;

            return pageState.StopLocation;
        }

        /// <summary>
        /// Retrieves the StopInformationMode from session
        /// </summary>
        /// <returns></returns>
        public StopInformationMode GetStopInformationMode()
        {
            InputPageState pageState = sessionManager.PageState;

            string strStopInfomationMode = pageState.StopInformationMode;

            try
            {
                return StopInformationModeHelper.GetStopInformationModeQS(strStopInfomationMode);
            }
            catch (TDPException)
            {
                // Invalid StopInformationMode saved to session, return the default
            }

            return StopInformationMode.StationBoardDeparture;
        }
                
        /// <summary>
        /// Updates the Stop Location to session
        /// </summary>
        /// <returns></returns>
        public void UpdateStopLocation(TDPStopLocation stopLocation)
        {
            InputPageState pageState = sessionManager.PageState;

            pageState.StopLocation = stopLocation;
        }

        /// <summary>
        /// Updates the StopInformationMode to session
        /// </summary>
        /// <returns></returns>
        public void UpdateStopInformationMode(StopInformationMode stopInformationMode)
        {
            InputPageState pageState = sessionManager.PageState;

            pageState.StopInformationMode = StopInformationModeHelper.GetStopInformationModeQS(stopInformationMode);
        }

        #endregion

        #region Messages

        /// <summary>
        /// Adds TDPMessage to session
        /// </summary>
        /// <param name="errorMessages"></param>
        public void AddMessage(TDPMessage errorMessage)
        {
            sessionManager.PageState.AddMessage(errorMessage);
        }

        /// <summary>
        /// Adds TDPMessage to session
        /// </summary>
        /// <param name="errorMessages"></param>
        public void AddMessages(List<TDPMessage> errorMessages)
        {
            sessionManager.PageState.AddMessages(errorMessages);
        }

        /// <summary>
        /// Clears TDPMessage in session
        /// </summary>
        public void ClearMessages()
        {
            sessionManager.PageState.ClearMessages();
        }

        #endregion
        
        #endregion
    }
}