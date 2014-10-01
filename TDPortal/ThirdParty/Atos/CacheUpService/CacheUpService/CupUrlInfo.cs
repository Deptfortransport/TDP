#region Amendment history
// *********************************************** 
// NAME			: $Workfile:   CupUrlInfo.cs  $
// AUTHOR		: Peter Norell
// DATE CREATED	: 01/11/2007
// REVISION		: $Revision:   1.1  $
// DESCRIPTION	: The Cache Up URL information class
// ************************************************ 
// $Log:   P:\archives\Codebase\WebTIS\CacheUpService\CupUrlInfo.cs-arc  $ 
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
using System.Text;
using WT.Common.Logging;
#endregion

namespace WT.CacheUpService
{
    /// <summary>
    /// The Cache Up URL information class
    /// </summary>
    public class CupUrlInfo
    {
        private string _name;
        /// <summary>
        /// The Name for the URL to be checked (used for logging)
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _urlAddr;
        /// <summary>
        /// The URL to be checked
        /// </summary>
        public string UrlAddress
        {
            get { return _urlAddr; }
            set { _urlAddr = value; }
        }

        private WTTraceLevel _severity = WTTraceLevel.Error;
        /// <summary>
        /// The severity of a failure for retrieving the page
        /// </summary>
        public WTTraceLevel Severity
        {
            get { return _severity; }
            set { _severity = value; }
        }

        private int _timeout = 30;
        /// <summary>
        /// The timeout time in milliseconds
        /// </summary>
        public int TimeoutSeconds
        {
            get { return _timeout; }
            set { _timeout = value; }
        }
        
        private string _acceptedCodes;
        /// <summary>
        /// The accepted status codes from the service
        /// </summary>
        public string AcceptedCodes
        {
            get { return _acceptedCodes; }
            set { _acceptedCodes = value; }
        }

        private string _retryCodes = string.Empty;
        /// <summary>
        /// The retry status codes from the service to resubmit the request
        /// </summary>
        public string RetryCodes
        {
            get { return _retryCodes; }
            set { _retryCodes = value; }
        }

        private bool _retainCookies = false;
        /// <summary>
        /// If cookies should be retained and used from the previous request
        /// </summary>
        public bool RetainCookies
        {
            get { return _retainCookies; }
            set { _retainCookies = value; }
        }

        private bool _keepAlive = false;
        /// <summary>
        /// If keep alive should be turned on or not.
        /// </summary>
        public bool KeepAlive
        {
            get { return _keepAlive; }
            set { _keepAlive = value; }
        }

        private bool _followRedirect = true;
        /// <summary>
        /// If the request should automaticall follow redirects or not
        /// </summary>
        public bool FollowRedirect
        {
            get { return _followRedirect; }
            set { _followRedirect = value; }
        }

        private string _pageScan = null;
        /// <summary>
        /// A suitable regex
        /// </summary>
        public string PageScan
        {
            get { return _pageScan; }
            set { _pageScan = value; }
        }

        private bool _pageScanPositive = true;
        /// <summary>
        /// If the page scan is should be positive or negative
        /// </summary>
        public bool PageScanPositive
        {
            get { return _pageScanPositive; }
            set { _pageScanPositive = value; }
        }

        private string _username = null;
        /// <summary>
        /// Username
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password = null;
        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _domain = null;
        /// <summary>
        /// The authentication domain
        /// </summary>
        public string Domain
        {
            get { return _domain; }
            set { _domain = value; }
        }

        private string _authType = null;
        /// <summary>
        /// The authentication type
        /// </summary>
        public string AuthType
        {
            get { return _authType; }
            set { _authType = value; }
        }

        private ICacheAction _action = null;
        /// <summary>
        /// The action to be executed
        /// </summary>
        public ICacheAction Action
        {
            get { return _action; }
            set { _action = value; }
        }

        private string _actionSettingsId = null;
        /// <summary>
        /// The id of the action settings set to use when executing the Action
        /// </summary>
        public string ActionSettingsId
        {
            get { return _actionSettingsId; }
            set { _actionSettingsId = value; }
        }
    }
}
