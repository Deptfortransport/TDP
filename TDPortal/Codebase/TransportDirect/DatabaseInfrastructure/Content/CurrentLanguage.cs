//// ***********************************************
//// NAME           : CurrentLanguage.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 09-Jan-2008
//// DESCRIPTION 	: The content for the general group
//// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DatabaseInfrastructure/Content/CurrentLanguage.cs-arc  $
//
//   Rev 1.3   Aug 04 2009 12:37:58   mmodi
//Added method to parse a culture string and return a Language 
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
////
////    Rev Devfactory Jan 09 2008 13:00:00   sbarker
////    CCN 0427 - Interacts with the user's cookies to determine the current language for the site.
////
////    Rev DevFactory Mar 04 2008 sbarker
////    Altered to fit in with the new persistent cookie code.
////
////    Rev DevFactory Feb 08 09:44:05 psheldrake
////    added support for new resx / mcms loading logic
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using TD.ThemeInfrastructure;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Interacts with the user's cookies to determine the current language for the site.
    /// </summary>
    public static class CurrentLanguage
    {
        #region Private Constants

        /// <summary>
        /// The name of the session key to look at.
        /// </summary>
        private const string siteLanguageKeyName = "SiteLanguage";

        #endregion 

        #region Private Fields

        private static readonly object valueLock = new object();

        #endregion

        #region Internal Static Properties

        /// <summary>
        /// Gets and sets the value of the current language
        /// </summary>
        public static Language Value
        {
            get
            {
                HttpContext context = HttpContextHelper.GetCurrent();
                return GetValue(context);
            }
            set
            {
                HttpContext context = HttpContextHelper.GetCurrent();
                SetValue(context, value);
            }
        }

        /// <summary>
        /// Gets the current culture
        /// </summary>
        public static string Culture
        {
            get
            {
                switch (Value)
                {
                    case Language.English:
                        return "en-GB";
                    case Language.Welsh:
                        return "cy-GB";
                    default:
                        throw new ArgumentOutOfRangeException("language", Value, "Language not handled");
                }
            }
        }

        /// <summary>
        /// Parses the Culture name and returns a Language value. Throws exception for unrecognised langauge
        /// </summary>
        public static Language ParseCulture(string cultureName)
        {
            switch (cultureName)
            {
                case "en-GB":
                    return Language.English;
                case "cy-GB":
                    return Language.Welsh;
                default:
                    throw new ArgumentOutOfRangeException("Culture", cultureName, "Culture not handled");
            }
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Updates the current language both in a session variable and in a cookie, so
        /// that the value can be read quickly in the same session, and can be recovered
        /// for future sessions, should the user have enabled cookies.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        private static void SetValue(HttpContext context, Language value)
        {
            lock (valueLock)
            {
                //First persist the value in a Cookie:
                PersistentCookie.CurrentLanguage = value;

                //Now persist in the session in case the user is not 
                //allowed to use Cookies:
                SetSessionValue(context, value);
            }
        }

        /// <summary>
        /// Reads the current language from the session if possible. If not, (because 
        /// the user has just started a new session) the value is read from a cookie 
        /// if possible. If this is not possible, english is returned by default.
        /// </summary>
        /// <returns></returns>
        private static Language GetValue(HttpContext context)
        {
            lock (valueLock)
            {
                if (context.Session != null)
                {
                    //Get the value from the session if possible:
                    object sessionLanguage = context.Session[siteLanguageKeyName];

                    if (sessionLanguage != null && sessionLanguage is Language)
                    {
                        return (Language)sessionLanguage;
                    }
                }

                //Could not get from session; now check from the cookie:
                //The cookie code will always return a value, even if
                //there is no cookie:
                Language value = PersistentCookie.CurrentLanguage;

                //Remember to store in the session before continuing...
                SetSessionValue(context, value);

                //Now return:
                return value;
            }
        }

        /// <summary>
        /// Sets the language value in the session
        /// </summary>
        /// <param name="context"></param>
        private static void SetSessionValue(HttpContext context, Language value)
        {
            if (context.Session != null)
            {
                context.Session[siteLanguageKeyName] = value;
            }
        }

        #endregion
    }
}
