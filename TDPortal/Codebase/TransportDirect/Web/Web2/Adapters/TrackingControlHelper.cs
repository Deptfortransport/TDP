// *********************************************** 
// NAME			: TrackingControlHelper.cs
// AUTHOR		: Parvez Ghumra
// DATE CREATED	: 2010-03-03
// DESCRIPTION	: Manages the tracking of various controls in the portal, providing a list of key-value
//				  pairs of parameters to be sent to Intellitracker servers
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/TrackingControlHelper.cs-arc  $
//
//   Rev 1.13   Apr 22 2010 11:54:38   pghumra
//Added some comments and made minor changes to some logic following code review
//Resolution for 5459: Update soft content for Intellitacker tracking URL in tag
//
//   Rev 1.12   Apr 20 2010 14:43:36   apatel
//Updated to check if the Intellitracker tracking key is already added to tracking parameter collection. Change the code to update the value instead of trying to add key with the same name in such case
//Resolution for 5518: Intellitracker exception
//
//   Rev 1.11   Apr 08 2010 13:23:08   apatel
//Updated to resolve html validator issues related to intellitracker
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.10   Mar 05 2010 10:00:34   apatel
//Updated for customised intellitracker tags
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.9   Mar 04 2010 17:03:28   pghumra
//Updated tracking functionality
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.8   Mar 04 2010 15:50:58   apatel
//Updated to add method to clear Tracking parameters
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.7   Mar 04 2010 14:50:16   apatel
//Updated to add customised intellitracker parameters
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.6   Mar 04 2010 12:41:24   pghumra
//Added constants to be used to add parameters
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.5   Mar 03 2010 13:59:48   apatel
//Updated LogError method
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.4   Mar 03 2010 13:33:06   apatel
//Updated LogError method
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.3   Mar 03 2010 12:21:18   apatel
//updated constants
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.2   Mar 03 2010 12:19:22   apatel
//Added constants to use as Tracking Values
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.1   Mar 03 2010 11:30:20   apatel
//Updated to add logging and refactor AddTrackingParameter method
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.0   Mar 03 2010 10:26:12   pghumra
//Initial revision.
//Resolution for 5402: Add Intellitracker tag to all TDP web pages

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.Web.Adapters
{
    [Serializable]
    public class TrackingControlHelper
    {
        #region Private Fields
        bool trackingEnabled = true;
        #endregion

        #region Constants
        // Constatns to use for Tracking values
        public const string CLICK = "c";
        public const string SHOW = "s";
        public const string TRUE = "t";
        public const string FALSE = "f";

        public const string SESSION_KEY = "TrackingParameterList";

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Read only property returns the dictionary object storing customised tracking parameter key value pair
        /// </summary>
        public Dictionary<string, string> TrackingParameters
        {
            get
            {
                object spr = HttpContext.Current.Session[SESSION_KEY];

                if (spr == null)
                {
                    HttpContext.Current.Session[SESSION_KEY] = new Dictionary<string, string>();
                }

                return HttpContext.Current.Session[SESSION_KEY] as Dictionary<string, string>;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TrackingControlHelper()
        {
            string tracking = Properties.Current["TrackingControl.IncludeTag"];

            if (string.IsNullOrEmpty(tracking))
            {
                trackingEnabled = false;
            }
            else
            {
                if (!bool.TryParse(tracking, out trackingEnabled))
                {
                    trackingEnabled = false;
                }
            }

        }
        #endregion

        #region Public Methods
        /// <summary>
       /// Add a new parameter tracking key-value pair to the repository
       /// use Control's PageId and control's id value to get tracking key 
       /// </summary>
       /// <param name="trackingKey">Parameter key</param>
       /// <param name="trackingValue">Parameter value</param>
        public void AddTrackingParameter(Control ctrl, string trackingValue)
        {
            try
            {
                if (trackingEnabled)
                {
                    string trackingKey = GetTrackingKey(ctrl);
                    if (!string.IsNullOrEmpty(trackingKey))
                    {
                        if (TrackingParameters.ContainsKey(trackingKey))
                        {
                            TrackingParameters[trackingKey] = trackingValue;
                        }
                        else
                        {
                            TrackingParameters.Add(trackingKey, trackingValue);
                        }
                    }
                    else
                    {
                        LogError(((TDPage)ctrl.Page).PageId.ToString(), ctrl.ID, null);
                    }
                }
                
            }
            catch(Exception ex)
            {
                LogError(((TDPage)ctrl.Page).PageId.ToString(), ctrl.ID, ex);
            }
        }

        /// <summary>
        /// Add a new parameter tracking key-value pair to the repository
        /// use PageId and custom resource key(i.e. control's client id) to get the tracking key
        /// </summary>
        /// <param name="pageId">Page Id</param>
        /// <param name="resourcekey">Custom resource key used to idnetify tracking key for page</param>
        /// <param name="trackingValue">Tracking value</param>
        public void AddTrackingParameter(string pageId, string resourcekey, string trackingValue)
        {
            try
            {
                if (trackingEnabled)
                {
                    string trackingKey = GetTrackingKey(pageId, resourcekey);
                    if (!string.IsNullOrEmpty(trackingKey))
                    {
                        if (TrackingParameters.ContainsKey(trackingKey))
                        {
                            TrackingParameters[trackingKey] = trackingValue;
                        }
                        else
                        {
                            TrackingParameters.Add(trackingKey, trackingValue);
                        }
                    }
                    else
                    {
                        LogError(pageId, resourcekey, null);
                    }
                }
                
            }
            catch(Exception ex)
            {
                LogError(pageId, resourcekey, ex);
            }
        }

        /// <summary>
        /// Obtain a string containing all parameter key-value pairs in the repository in a format to be used in the URL
        /// to be sent to Intellitracker
        /// </summary>
        /// <returns></returns>
        public string GetTrackingParameterString()
        {
            
            StringBuilder sb = new StringBuilder();
            string sessionKeyTrackingKey = GetTrackingKey("TDPage", "SessionId");

            //Default to SK for session key to ensure this is tracked even if key not found in database
            if (string.IsNullOrEmpty(sessionKeyTrackingKey))
            {
                sessionKeyTrackingKey = "SK";
            }
            sb.Append(HttpUtility.UrlEncode(sessionKeyTrackingKey + "=" + TDSessionManager.Current.Session.SessionID));
            foreach (string s in TrackingParameters.Keys)
            {
                sb.Append(HttpUtility.HtmlEncode("&" + s + "=" + TrackingParameters[s]));
            }
            return sb.ToString();

        }

        /// <summary>
        /// Clears the Tracking parametes key value pair stored in session
        /// </summary>
        public void ClearTrackingParameters()
        {
            HttpContext.Current.Session[SESSION_KEY] = null;
        }

        #endregion

       

        #region Private Methods

        /// <summary>
        /// Obtain tracking value from database based on specified control using control ID value
        /// </summary>
        /// <param name="ctrl">Control containing the object to be tracked</param>
        /// <param name="key">The key used for tracking purposes</param>
        /// <returns></returns>
        private string GetTrackingKey(Control ctrl)
        {
            TDPage page = (TDPage)ctrl.Page;

            return TrackingKeysProvider.Instance[page.PageId.ToString()].GetPropertyValue(page.PageId.ToString(), ctrl.ID);
        }

        /// <summary>
        /// Obtain tracking value from database based on specified page and custome resource key
        /// </summary>
        /// <param name="pageId">Page containing the object to be tracked</param>
        /// <param name="key">The resource key used for tracking purposes</param>
        /// <returns></returns>
        private string GetTrackingKey(string pageId, string key)
        {
            return TrackingKeysProvider.Instance[pageId].GetPropertyValue(pageId, key);
        }

        /// <summary>
        /// Logs error in the log when tracking key not found for given pageId and resourcekey combination.
        /// Also, logs exception if there is an exception while getting the tracking key
        /// </summary>
        /// <param name="pageId">Id of the page</param>
        /// <param name="resourceKey">resource key(this will be either control id or custom value)</param>
        /// <param name="ex">Exception</param>
        private void LogError(string pageId, string resourceKey, Exception ex)
        {
            //log error message
            string message = string.Format("Tracking Control Error : not able to get tracking key for page Id:{0} and resource:{1}.", pageId, resourceKey);

            if (ex != null)
            {
                message = message + string.Format(" Exception: {0}\n{1}", ex.Message, ex.StackTrace);
            }
            OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                TDTraceLevel.Error, message);
            Logger.Write(oe);
        }
        #endregion


    }

}
