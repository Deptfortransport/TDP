//// ***********************************************
//// NAME           : ThemeProvider.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 15-Feb-2008
//// DESCRIPTION 	: Given a uri object, this class looks up the relevant theme.             
//// ************************************************
////    Rev Devfactory Jan 09 2008 14:00:00   sbarker
////    CCN 0427 - First version
////
////    Rev Devfactory Feb 20 2008 sbarker
////    Change to not chop off the .com/.co.uk/.info part of the 
////    host header when looking up the partner ID.
////
////    Rev Devfactory Feb 27 2008 sbarker
////    Renamed from ThemeNameProvider to ThemeProvider
////    since a Theme object is now being handled. A
////    Theme contains a name and id.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using System.Configuration;

using System.Xml.XPath;
using System.Xml;
using System.Globalization;
using TransportDirect.Common;
using Logger = System.Diagnostics.Trace;

namespace TD.ThemeInfrastructure
{
    /// <summary>
    /// Singleton class that loads in base url to theme name mappings on 
    /// instantiation. Can then return a theme name, given a uri object.
    /// </summary>
    public class ThemeProvider
    {
        #region Private Enums

        /// <summary>
        /// Enum to interact with the data reader containing content. Enum removes the 
        /// need for "magic numbers" in the code, while preventing the performance 
        /// problems associated with accessing fields by string name.
        /// </summary>
        private enum DataReaderFieldIndex
        {
            ThemeId = 0,
            ThemeName = 1,
            UrlBase = 2,            
        }

        #endregion

        #region Private Constants

        private const string defaultThemeName = "TransportDirect";
        private const int defaultThemeId = 1;
        
        #endregion

        #region Private Static Fields

        private static readonly object instanceLock = new object();
        private static ThemeProvider instance = null;
        
        #endregion

        #region Public Static Fields

        /// <summary>
        /// Gets the sinlgeton instance
        /// </summary>
        public static ThemeProvider Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ThemeProvider();
                    }

                    return instance;
                }
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Used to store mappings between base urls and theme names
        /// </summary>
        private readonly Dictionary<string, Theme> themeUrlMappings = null;

        /// <summary>
        /// Used to store the version of the Theme file loaded
        /// </summary>
        private int themeVersion = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Singleton private constructor
        /// </summary>
        private ThemeProvider()
        {
            // Get the value where to load the Themes from, default is from Database
            string providertype = ConfigurationManager.AppSettings
                ["themeinfrastructure.provider.type"];

            // Reset the version, user for when loading from File
            themeVersion = 0;

            // Load the Themes
            themeUrlMappings = GetThemeUrlMappings(providertype);            
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the Themes loaded from Database or File
        /// </summary>
        /// <param name="providertype">"file" to load from file, default from database</param>
        /// <returns></returns>
        private Dictionary<string, Theme> GetThemeUrlMappings(string providertype)
        {
            // Load from File
            if ((!string.IsNullOrEmpty(providertype)) && (providertype.ToLower() == "file"))
            {
                Dictionary<string, Theme> mappings = new Dictionary<string, Theme>();

                #region Get themes from File
                try
                {
                    if ((themeVersion == 0) || (IsNewVersion()))
                    {
                        // New Themes file
                        return LoadThemesFromFile();
                    }
                    else
                    {
                        // Themes haven't changed so return the already loaded themes
                        return themeUrlMappings;
                    }
                }
                catch (TDException e)
                {
                    Logger.Write("Exception trying to manipulate the Themes Xml File."
                        + "\r\n Message: " + e.Message
                        + "\r\n Source: " + e.Source
                        + "\r\n InnerException: " + e.InnerException
                        + "\r\n StackTrace: " + e.StackTrace);
                }

                return mappings;
                #endregion
            }
            else // Load from Database
            {
                #region Get themes from Database
                SqlConnection connection = null;
                SqlCommand command = null;
                SqlDataReader reader = null;

                Dictionary<string, Theme> mappings = new Dictionary<string, Theme>();

                try
                {
                    using (connection = new SqlConnection(ContentDatabaseConnectionStringHelper.Get()))
                    {
                        using (command = new SqlCommand("sprGetThemeDomains", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Connection.Open();

                            using (reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                            {
                                //Note that the reader returns two fields.
                                //These are described  in the enum DataReaderFieldIndex to 
                                //avoid 'magic numbers' in the code.
                                while (reader.Read())
                                {
                                    int themeId = reader.GetInt32((int)DataReaderFieldIndex.ThemeId);
                                    string urlBase = reader.GetString((int)DataReaderFieldIndex.UrlBase);
                                    string themeName = reader.GetString((int)DataReaderFieldIndex.ThemeName);

                                    mappings.Add(urlBase.ToLower(), new Theme(themeId, themeName));
                                }

                                return mappings;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    string message = "Error attempting to access ThemeProvider database, running sprGetThemeDomains";

                    Logger.Write(message
                        + "\r\n Message: " + e.Message
                        + "\r\n Source: " + e.Source
                        + "\r\n InnerException: " + e.InnerException
                        + "\r\n StackTrace: " + e.StackTrace
                        );

                    return null;
                }
                finally
                {
                    connection = null;
                    command = null;
                    reader = null;
                    mappings = null;
                }
                #endregion
            }
        }

        /// <summary>
        /// Returns a theme dictionary loaded from a file
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, Theme> LoadThemesFromFile()
        {
            Dictionary<string, Theme> mappings = new Dictionary<string, Theme>();

            try
            {

                // Get unique provider settings filename
                string providerFile = (string)ConfigurationManager.AppSettings["themeinfrastructure.provider.theme.filepath"];

                // populate themes
                // open the xml file
                XmlNodeList xNodelist;
                XmlDocument xdLookup = new XmlDocument();
                xdLookup.Load(providerFile);

                // step through xml file and load all themes.
                
                xNodelist = xdLookup.DocumentElement.SelectNodes("//theme");
                foreach (XmlNode node in xNodelist)
                {
                    string themeName = node.Attributes["name"].InnerText;
                    
                    if (themeName.Trim() != "version") // Don't add the version value
                    {
                        string themeDomain = node.Attributes["domain"].InnerText;
                        int themeId = int.Parse(node.InnerText);

                        mappings.Add(themeDomain.ToLower(), new Theme(themeId, themeName));
                    }
                }

                // set the version of this file
                themeVersion = GetVersion();
            }
            catch (ArgumentException e)
            {
                throw new TDException(
                    "Exception trying to load the data from Themes Xml File : " + e.Message,
                    true,
                    TDExceptionIdentifier.TIInvalidThemeFile);
            }
            catch (TDException e)
            {
                throw new TDException(
                    "Exception trying to manipulate the Themes Xml File : " + e.Message,
                    true,
                    TDExceptionIdentifier.TIInvalidThemeFile);
            }

            return mappings;
        }

        /// <summary>
        /// Reads the version from the currently saved themes file
        /// </summary>
        /// <returns></returns>
        private int GetVersion()
        {
            int latestVersion = 0;

            try
            {
                // Get unique provider settings ( filename
                string providerFile = (string)ConfigurationManager.AppSettings["themeinfrastructure.provider.theme.filepath"];

                XmlNode xnQuery;
                XmlDocument xdLookup = new XmlDocument();
                xdLookup.Load(providerFile);
                xnQuery = xdLookup.DocumentElement.SelectSingleNode("//theme[@name=\"version\"]");
                latestVersion = Convert.ToInt32(xnQuery.InnerXml, CultureInfo.CurrentCulture.NumberFormat);
            }

            catch (NullReferenceException e)
            {
                throw new TDException(
                    "Exception trying to get the version value from Themes Xml file : " + e.Message,
                    false,
                    TDExceptionIdentifier.TIInvalidVersion);
            }
            catch (XPathException e)
            {
                throw new TDException(
                    "Exception trying to get the version value from Themes Xml file : " + e.Message,
                    false,
                    TDExceptionIdentifier.TIInvalidVersion);
            }
            catch (XmlException e)
            {
                throw new TDException(
                    "Exception trying to get the version value from Themes Xml file : " + e.Message,
                    false,
                    TDExceptionIdentifier.TIInvalidVersion);
            }
            catch (ArgumentException e)
            {
                throw new TDException(
                    "Exception trying to get the version value from Themes Xml file : " + e.Message,
                    false,
                    TDExceptionIdentifier.TIInvalidVersion);
            }

            return latestVersion;

        }

        /// <summary>
        /// Returns if the Themes file is a new version
        /// </summary>
        private bool IsNewVersion()
        {
            try
            {
                int latestVersion = GetVersion();

                if (latestVersion == themeVersion)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (TDException e)
            {
                throw new TDException(
                    "Exception trying to manipulate the Themes Xml File : " + e.Message,
                    true,
                    TDExceptionIdentifier.TIInvalidThemeFile);
            }
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Given a uri, this method returns the associated Theme.
        /// If the uri can't be converted to a theme, then the default
        /// TransportDirect theme (id = 1) is returned.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        private Theme GetThemeFromRequestUri(Uri requestUri)
        {
            try
            {
                return themeUrlMappings[requestUri.Host.ToLower()];
            }
            catch (KeyNotFoundException)
            {
                //If we can't find the host, return the default,
                //which is TransportDirect:
                return new Theme(defaultThemeId, defaultThemeName);
            }
        }

        /// <summary>
        /// This method returns the associated Theme, using the current HttpContext
        /// to deduce the url, and hence the partner. If any errors occur, the default 
        /// TransportDirect theme (id = 1) is returned.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Theme GetTheme()
        {
            HttpContext context = HttpContextHelper.GetCurrent();
            try
            {
                return GetThemeFromRequestUri(context.Request.Url);
            }
            catch
            {
                // Couldn't get theme, so need to revert to default
                return GetDefaultTheme();
            }

        }

        /// <summary>
        /// This method returns the default theme
        /// </summary>
        /// <returns></returns>
        public Theme GetDefaultTheme()
        {
            return new Theme(defaultThemeId, defaultThemeName); 
        }

        #endregion
    }
}
