//// ***********************************************
//// NAME           : ContentProvider.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 07-Jan-2008
//// DESCRIPTION 	: Allows interaction with the database containing language/group specific page content
//// ************************************************
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
////    Rev Devfactory Jan 07 2008 12:00:00   sbarker
////    CCN 0427 - Class needed to allow interaction with the database containing 
////    language/group specific page content

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using TransportDirect.Common.DatabaseInfrastructure;
using System.Configuration;
using TD.ThemeInfrastructure;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Singleton class to encapsulate interaction with the content database.
    /// </summary>
    public sealed class ContentProvider
    {
        #region Private Enums

        /// <summary>
        /// Enum to interact with the data reader containing content. Enum removes the 
        /// need for "magic numbers" in the code, while preventing the performance 
        /// problems associated with accessing fields by string name.
        /// </summary>
        private enum DataReaderFieldIndex
        {
            ControlName = 0,
            PropertyName = 1,
            Value = 2,
        }

        #endregion

        #region Private Static Fields

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static ContentProvider instance;
        /// <summary>
        /// Lock to allow thread safe interaction with singleton instance.
        /// </summary>
        private static readonly object instanceLock = new object();

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Property to allow singleton instance to be obtained. Thread safe.
        /// </summary>
        public static ContentProvider Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ContentProvider();
                    }

                    return instance;
                }
            }
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Given a group name, language and request uri, a hashtable of parameter 
        /// details needed to make the correct database call (to retrieve content) 
        /// is returned. While a hashtable isn't the natural choice for a set of 
        /// parameters, this is fit with existing Transport Direct code.
        /// </summary>
        /// <param name="groupName">The group name of the set of content being queried</param>
        /// <param name="language">The language being queried</param>
        /// the partner</param>
        /// <returns>A hashtable of SQL parameter details needed to perform the query</returns>
        private static Hashtable GetParameters(string groupName, Language language)
        {
            Hashtable parameters = new Hashtable();

            string languageParameterValue;

            switch (language)
            {
                case Language.English:
                    languageParameterValue = "en-GB";
                    break;
                case Language.Welsh:
                    languageParameterValue = "cy-GB";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("language", language, "Language not handled");
            }

            parameters.Add("@Language", languageParameterValue);
            parameters.Add("@Group", groupName);

            // Place in try catch in case called from anywhere which does not have a HttpContext
            string themeName = string.Empty;
            try
            {
                themeName = ThemeProvider.Instance.GetTheme().Name;
            }
            catch
            {
                themeName = ThemeProvider.Instance.GetDefaultTheme().Name;
            }

            parameters.Add("@Theme", themeName);

            return parameters;
        }

        #endregion

        #region Internal Static Methods

        /// <summary>
        /// Given a group name, language and request Uri, a collection of relavent 
        /// content (in the form of control names and properties) is returned.
        /// </summary>
        /// <param name="groupName">The name of the group to retrieve content for</param>
        /// <param name="language">The language of the returned content</param>
        /// <returns></returns>
        internal static ControlPropertyCollection GetControlPropertyCollection(string groupName, Language language)
        {
            //The following field looks overly complex! However, it is required to
            //be this way. See notes in the class ControlPropertyCollection for
            //an explanation:
            Dictionary<string, Dictionary<string, string>> contentDictionary = new Dictionary<string, Dictionary<string, string>>();
            SqlHelper sqlHelper = null;            
            SqlDataReader reader = null;

            try
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("ContentProvider - Loading content for Group[{0}] and Language[{1}]", groupName, language.ToString())));

                using (sqlHelper = new SqlHelper())
                {
                    Hashtable parameters = GetParameters(groupName, language);

                    sqlHelper.ConnOpen(ContentDatabaseConnectionStringHelper.Get());

                    using (reader = sqlHelper.GetReader("sprGetContent", parameters))
                    {
                        //Note that the reader returns three fields, 
                        //ControlName, PropertyName and Value. These are described 
                        //in the enum DataReaderFieldIndex to avoid 'magic numbers'
                        //in the code.
                        while (reader.Read())
                        {
                            //Note that we convert keys to lower case:
                            string controlName = reader.GetString((int)DataReaderFieldIndex.ControlName).ToLower();
                            string propertyName = reader.GetString((int)DataReaderFieldIndex.PropertyName).ToLower();
                            string value = reader.GetString((int)DataReaderFieldIndex.Value);

                            //Check to see if information about the current control 
                            //exists. If not, create a new dictionary.
                            if (!contentDictionary.ContainsKey(controlName))
                            {
                                contentDictionary.Add(controlName, new Dictionary<string, string>());
                            }

                            //Add the control property and value.
                            contentDictionary[controlName].Add(propertyName, value);
                        }
                    }
                }

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("ContentProvider - Loading content completed for Group[{0}] and Language[{1}]", groupName, language.ToString())));

                return new ControlPropertyCollection(contentDictionary);
            }
            finally
            {
                sqlHelper = null;
                reader = null;
            }
        }

        #endregion

        #region Private Fields

        //Dictionary to hold all content providers for each group:
        private readonly Dictionary<string, ContentGroupProvider> contentGroupProviders = new Dictionary<string, ContentGroupProvider>();
        
        #endregion

        #region Private Constructor

        /// <summary>
        /// Private constructor required by singleton implementation
        /// </summary>
        private ContentProvider()
        {
            //No implementation
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Allows content for a particular group to be obtained. Throws a 
        /// ContentProviderException if the group name is not recognised.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public ContentGroupProvider this[string groupName]
        {
            get
            {
                //Groups are loaded on demand:
                if (!contentGroupProviders.ContainsKey(groupName))
                {
                    contentGroupProviders.Add(groupName, new ContentGroupProvider(groupName));
                }

                return contentGroupProviders[groupName];
            }
        }

        #endregion
    }
}
