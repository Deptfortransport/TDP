using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;
using System.Web;
using AO.HttpRequestValidatorCommon;
using Microsoft.Web.Administration;

namespace AO.HttpRequestValidator
{
    /// <summary>
    /// Class to do domain redirects
    /// </summary>
    public class RequestDomainRedirectValidator
    {
        #region Private variables

        private static string[,] domainsToRedirect = {{"", ""}};
        private static bool includeExtensions;
        private static string EVENTLOG_NAME = string.Empty;
        private static string EVENTLOG_SOURCE = string.Empty;
        private static string EVENTLOG_MACHINE = string.Empty;
        private static string EVENTLOG_PREFIX = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Static Constructor
        /// </summary>
        static RequestDomainRedirectValidator()
        {
            // Get the list of redirects and other config items
            ConfigurationSection section = Microsoft.Web.Administration.WebConfigurationManager.GetSection(HttpRequestValidatorKeys.ConfigurationSection);

            #region Get validator config

            string redirects;

            try
            {
                redirects = section.GetChildElement(HttpRequestValidatorKeys.Element_DomainRedirects).GetAttribute(HttpRequestValidatorKeys.DomainRedirects_RedirectList).Value.ToString();
                EVENTLOG_NAME = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Name).Value;
                EVENTLOG_SOURCE = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Source).Value;
                EVENTLOG_MACHINE = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_Machine).Value;
                EVENTLOG_PREFIX = (string)section.GetChildElement(HttpRequestValidatorKeys.Element_EventLog).GetAttribute(HttpRequestValidatorKeys.EventLog_MessagePrefix).Value;
            }
            catch
            {
                throw new Exception(string.Format("Failed to get the RequestDomainRedirectValidator domain redirects values, config[{0}, {1}, {2}, {3} or {4}] is missing or invalid",
                                new object[] {HttpRequestValidatorKeys.DomainRedirects_RedirectList,
                                HttpRequestValidatorKeys.EventLog_Name,
                                HttpRequestValidatorKeys.EventLog_Source,
                                HttpRequestValidatorKeys.EventLog_Machine,
                                HttpRequestValidatorKeys.EventLog_MessagePrefix}));
            }

            string[] redirectsList = redirects.Split(';');

            domainsToRedirect = new string[redirectsList.Length, 2];

            for (int i = 0; i < redirectsList.Length; i++)
            {
                domainsToRedirect[i, 0] = redirectsList[i].Split('!')[0];
                domainsToRedirect[i, 1] = redirectsList[i].Split('!')[1];
            }

            try
            {
                includeExtensions = (bool)(section.GetChildElement(HttpRequestValidatorKeys.Element_DomainRedirects).GetAttribute(HttpRequestValidatorKeys.DomainRedirects_IncludeExtensions).Value);
            }
            catch
            {
                throw new Exception(string.Format("Failed to set RequestDomainRedirectValidator include extensions value, config[{0}] is missing or invalid",
                                HttpRequestValidatorKeys.DomainRedirects_IncludeExtensions));
            }

            #endregion

            #region Setup the Event log

            StringBuilder sb = new StringBuilder("RequestDomainRedirectionValidator initialised");
            sb.AppendLine();
            sb.AppendLine("List of domain redirects:");

            for (int i = 0; i < redirectsList.Length; i++)
            {
                sb.Append(redirectsList[i].Split('!')[0]);
                sb.Append(" to ");
                sb.AppendLine(redirectsList[i].Split('!')[1]);
            }

            EventLogOutput.Instance(EVENTLOG_NAME, EVENTLOG_SOURCE, EVENTLOG_MACHINE, EVENTLOG_PREFIX).WriteEvent(sb.ToString(), EventLogEntryType.Information);

            #endregion
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public RequestDomainRedirectValidator()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method which checks if the request needs redirecting
        /// </summary>
        /// <returns></returns>
        public bool IsValid(HttpContext context)
        {
            if (context != null)
            {
                // check the URL vs the list of domains to redirect
                for (int i = 0; i <= domainsToRedirect.GetUpperBound(0); i++)
                {
                    if (context.Request.Url.AbsoluteUri.StartsWith(domainsToRedirect[i, 0]))
                    {
                        // Redirect required, assemble URL
                        string redirectUrl;

                        if (includeExtensions)
                        {
                            redirectUrl = context.Request.Url.AbsoluteUri.Replace(domainsToRedirect[i, 0], domainsToRedirect[i, 1]);
                        }
                        else
                        {
                            redirectUrl = domainsToRedirect[i, 1];
                        }

                        context.Response.RedirectLocation = redirectUrl;
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

    }
}
