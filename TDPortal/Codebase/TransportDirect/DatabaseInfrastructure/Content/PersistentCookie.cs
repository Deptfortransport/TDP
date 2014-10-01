//// ***********************************************
//// NAME           : PersistentCookie.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 04-Mar-2008
//// DESCRIPTION 	: Represents the persistent cookie that TD will use
//// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DatabaseInfrastructure/Content/PersistentCookie.cs-arc  $
//
//   Rev 1.6   May 23 2008 14:34:00   mmodi
//Update to retreive the cookie for the current theme
//Resolution for 5004: Cookies: Repeat visitor event being logged for every page request
//
//   Rev 1.5   May 14 2008 15:33:50   mmodi
//Added properties for repeat visitor information
//Resolution for 4889: Del 10.1 - Repeat Visitor Cookies
//
//   Rev 1.4   May 01 2008 17:31:12   mmodi
//Updated to include plannermode
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
////
////    Rev Devfactory Mar 04 2008 sbarker
////    Initial version

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using TD.ThemeInfrastructure;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
	/// <summary>
	/// Allows access to the persistent cookie
	/// </summary>
	public static class PersistentCookie
	{
		#region Private Constants

        // Custom datetime pattern based on ISO 8601, to resolution of seconds
        private const string dateTimeFormat = "yyyyMMddTHHmmZ";

		private const string CookieName = "TDP";
		private const string KeyCurrentLanguage = "L";
        private const string KeyPlannerMode = "PM";
		private const string KeyThemeId = "T";
        private const string KeyUniqueId = "UID";
        private const string KeyLastVisitedDateTime = "DT";
        private const string KeyLastPageVisited = "LP";

        private const string KeyDoNotRedirect = "DNR";

		#endregion

		#region Public Static Properties

        /// <summary>
        /// Returns true if the persistent cookie is available in the current request
        /// </summary>
        public static bool IsPersistentCookieAvailable
        {
            get
            {
                try
                {
                    // Check for the cookie in the current httpcontext
                    HttpContext context = HttpContextHelper.GetCurrent();

                    if ((context != null) && (context.Request.Cookies[CookieName] != null))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

		/// <summary>
		/// Gets or sets the Expiry date of the cookie
		/// </summary>
		public static DateTime Expires
		{
            get
			{
				return Cookie.Expires;
			}
            set
            {
                Cookie.Expires = value;
            }
        }

        /// <summary>
        /// Gets or sets the Domain of the cookie
        /// </summary>
        public static string Domain
        {
            get
            {
                return Cookie.Domain;
            }
            set
            {
                Cookie.Domain = value;
            }
        }

        /// <summary>
        /// Gets the Name of the cookie. Returns string.Empty by default
        /// </summary>
        public static string Name
        {
            get
            {
                return Cookie.Name;
            }
        }

        #region Custom properties
        /// <summary>
		/// Gets or sets the current language. Note that English will be returned
		/// by default if nothing can be found.
		/// </summary>
		public static Language CurrentLanguage
		{
			get
			{
				return GetCookieValueLanguage(KeyCurrentLanguage, Language.English);
			}
			set
			{
                SetCookieValueLanguage(KeyCurrentLanguage, value);
			}
		}

		/// <summary>
		/// Gets or sets the theme id. Returns 1 by default
		/// </summary>
		public static int ThemeId
		{
			get
			{
				return GetCookieValueInt(KeyThemeId, 1);
			}
			set
			{
                SetCookieValueInt(KeyThemeId, value);
			}
		}

        /// <summary>
        /// Gets or sets the users current planner mode. Returns "none" by default
        /// </summary>
        public static string PlannerMode
        {
            get
            {
                return GetCookieValueString(KeyPlannerMode, "none");
            }
            set
            {
                SetCookieValueString(KeyPlannerMode, value);
            }
        }

        /// <summary>
        /// Gets or sets the unique id. Returns string.Empty by default
        /// </summary>
        public static string UniqueId
        {
            get
            {
                return GetCookieValueString(KeyUniqueId, string.Empty);
            }
            set
            {
                SetCookieValueString(KeyUniqueId, value);
            }
        }

        /// <summary>
        /// Gets or sets the last visited date time. Returns Now by default
        /// </summary>
        public static DateTime LastVisitedDateTime
        {
            get
            {
                return GetCookieValueDateTime(KeyLastVisitedDateTime, DateTime.Now.ToUniversalTime());
            }
            set
            {
                SetCookieValueDateTime(KeyLastVisitedDateTime, value);
            }
        }

        /// <summary>
        /// Gets or sets the Last page visited. Returns string.Empty by default
        /// </summary>
        public static string LastPageVisited
        {
            get
            {
                return GetCookieValueString(KeyLastPageVisited, string.Empty);
            }
            set
            {
                SetCookieValueString(KeyLastPageVisited, value);
            }
        }

        /// <summary>
        /// Gets or sets the do not redirect to mobile option. Returns false by default
        /// </summary>
        public static bool DoNotRedirect
        {
            get { return GetCookieValueBool(KeyDoNotRedirect, false); }
            set { SetCookieValueBool(KeyDoNotRedirect, value); }
        }

        #endregion

        #endregion

        #region Private Static Methods

        private static string GetCookieValueString(string key, string defaultValue)
		{
			//Persist an instance of this cookie:
			HttpCookie cookie = Cookie;            
			string value = cookie[key];

			if (value == null)
			{
				cookie[key] = defaultValue;
				return defaultValue;
			}
			else
			{
				return value;
			}
		}

		private static void SetCookieValueString(string key, string value)
		{
			//Persist an instance of this cookie:
			HttpCookie cookie = Cookie;

			cookie[key] = value;

			//Make sure the cookie is written back to the browser...
			Cookie = cookie;
		}

		private static Language GetCookieValueLanguage(string key, Language defaultValue)
		{
			string value = GetCookieValueString(key, defaultValue.ToString());

			if (Enum.IsDefined(typeof(Language), value))
			{
				return (Language)Enum.Parse(typeof(Language), value);
			}
			else
			{
				return defaultValue;
			}
		}

		private static void SetCookieValueLanguage(string key, Language value)
		{
			SetCookieValueString(key, value.ToString());
		}

        /// <summary>
        /// Gets a bool value from the cookie, or default if null
        /// </summary>
        private static bool GetCookieValueBool(string key, bool defaultValue)
        {
            string value = GetCookieValueString(key, defaultValue.ToString());

            bool returnValue = false;

            if (Boolean.TryParse(value, out returnValue))
            {
                return returnValue;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Sets a bool value to the cookie
        /// </summary>
        private static void SetCookieValueBool(string key, bool value)
        {
            SetCookieValueString(key, value.ToString());
        }

		private static int GetCookieValueInt(string key, int defaultValue)
		{
			string value = GetCookieValueString(key, defaultValue.ToString());
            
			int returnValue;

			if(int.TryParse(value, out returnValue))
			{
				return returnValue;
			}
			else
			{
				return defaultValue;
			}
		}

		private static void SetCookieValueInt(string key, int value)
		{
			SetCookieValueString(key, value.ToString());
		}

        private static DateTime GetCookieValueDateTime(string key, DateTime defaultValue)
        {
            //Persist an instance of this cookie:
            HttpCookie cookie = Cookie;
            string value = cookie[key];
            
            if (string.IsNullOrEmpty(value))
            {
                cookie[key] = defaultValue.ToString(dateTimeFormat);
                return DateTime.ParseExact(cookie[key], dateTimeFormat, System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                return DateTime.ParseExact(value, dateTimeFormat, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private static void SetCookieValueDateTime(string key, DateTime value)
        {
            //Persist an instance of this cookie:
            HttpCookie cookie = Cookie;

            cookie[key] = value.ToString(dateTimeFormat);
            
            //Make sure the cookie is written back to the browser...
            Cookie = cookie;
        }

		#endregion

		#region Private Static Properties

		private static HttpCookie Cookie
		{
			get
			{
				//Get the current context:
				HttpContext context = HttpContextHelper.GetCurrent();
                Theme currentTheme = ThemeProvider.Instance.GetTheme();
                
                HttpCookie currentCookie = null;
                HttpCookieCollection cookiesCollection = context.Request.Cookies;

                //Make sure we get the correct cookie, as user may have multiple cookies with the same name
                for (int i = 0; i < cookiesCollection.Count; i++)
                {
                    HttpCookie thisCookie = cookiesCollection[i];

                    if ((thisCookie.Name == CookieName) && (thisCookie[KeyThemeId] == currentTheme.Id.ToString()))
                    {
                        currentCookie = thisCookie;
                        break;
                    }
                }

				//Now check for the cookie:
				if (currentCookie == null)
				{
                    HttpCookie newCookie = new HttpCookie(CookieName);
					newCookie.Expires = DateTime.Now.AddMonths(6);
					return newCookie;
				}
				else
				{
                    return currentCookie;
				}
			}
			set
			{            
				//Get the current context:
				HttpContext context = HttpContextHelper.GetCurrent();
				context.Response.AppendCookie(value);
			}
		}

		#endregion
	}
}
