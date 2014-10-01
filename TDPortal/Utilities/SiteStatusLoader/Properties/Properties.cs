// *********************************************** 
// NAME                 : Properties.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Properties class, loads properties from a file
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Properties/Properties.cs-arc  $
//
//   Rev 1.1   Apr 09 2009 10:43:16   mmodi
//If properties file is missing, create a template. Added error handling on the properties refresh method
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:30:56   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Timers;
using System.Xml;

using AO.Common;

namespace AO.Properties
{
    public sealed class Properties
    {
        #region Private members

        private string groupID = string.Empty;
        private string applicationID = string.Empty;

        private static readonly object instanceLock = new object();
        private static Properties instance = null;

        private int propertyVersion = 0;
        private Dictionary<string, string> propertyDictionary = null;
        
        private Timer propertyRefreshTimer;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        private Properties()
        {
            propertyVersion = 0;
            
            propertyDictionary = LoadProperties();

            SetupTimer();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the properties from a properties file.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> LoadProperties()
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();

            if (CheckForConfigurationFile())
            {
                // Obtain the AID and GID to ensure correct properties are loaded
                groupID = ConfigurationManager.AppSettings["propertyservice.groupid"];
                applicationID = ConfigurationManager.AppSettings["propertyservice.applicationid"];

                // Obtain the properties file location
                string propertyFile = ConfigurationManager.AppSettings["propertyservice.providers.fileprovider.filepath"];

                // If there is no Property file, then won't be able to set up the properties needed by the application.
                if ((string.IsNullOrEmpty(propertyFile)) || (!File.Exists(propertyFile)))
                {
                    // Create the properties Template file if needed
                    PropertiesTemplate.Instance.CreatePropertiesTemplateFile(propertyFile);

                    throw new SSException(Messages.PSPropertyFileMissing, false, SSExceptionIdentifier.PSMissingPropertyFile);
                }

                #region Load

                // Load properties from file
                if ((propertyVersion == 0) || (IsNewVersion(propertyFile)))
                {
                    try
                    {
                        // open the xml file
                        XmlNodeList xNodelist;
                        XmlDocument xdLookup = new XmlDocument();
                        xdLookup.Load(propertyFile);

                        // step through xml file and load all properties.

                        // first select the properties for this application
                        xNodelist = xdLookup.DocumentElement.SelectNodes("//property[@AID=\"" + applicationID + "\"]");
                        foreach (XmlNode node in xNodelist)
                        {
                            AddProperty(node, ref properties);
                        }

                        // Next select the properties for this GroupId
                        xNodelist = xdLookup.DocumentElement.SelectNodes("//property[@GID=\"" + groupID + "\"]");
                        foreach (XmlNode node in xNodelist)
                        {
                            AddProperty(node, ref properties);
                        }

                        // Next select the global properties
                        xNodelist = xdLookup.DocumentElement.SelectNodes("//property[@GID=\"\"]");
                        foreach (XmlNode node in xNodelist)
                        {
                            AddProperty(node, ref properties);
                        }

                        // Update the latest version
                        propertyVersion = GetVersion(propertyFile);
                    }
                    catch (Exception ex)
                    {
                        string message = string.Format(Messages.PSPropertyLoadFail, propertyFile)
                            + " Exception: " + ex.Message;

                        throw new SSException(message, false, SSExceptionIdentifier.PSLoadPropertyFileFail);
                    }
                }
                else
                {
                    // return the already loaded dictionary
                    return propertyDictionary;
                }

                #endregion
            }

            return properties;
        }

        /// <summary>
        /// Adds the Propery to the supplied properties list
        /// </summary>
        /// <param name="node"></param>
        /// <param name="properties"></param>
        private void AddProperty(XmlNode node, ref Dictionary<string, string> properties)
        {
            string propertyKey = node.Attributes["name"].InnerText;

            string propertyValue = node.InnerText;

            string propertyAID = node.Attributes["AID"].InnerText;
            string propertyGID = node.Attributes["GID"].InnerText;

            // Only Add the property, if 
            // a) its for this AID
            // b) its for this GID and blank AID
            // c) its for a blank GID and blank AID

            if (
                (propertyAID.Equals(applicationID))
                ||
                ((propertyGID.Equals(groupID)) && (string.IsNullOrEmpty(propertyAID)))
                ||
                ((string.IsNullOrEmpty(propertyGID)) && (string.IsNullOrEmpty(propertyAID)))
                )
            {

                if (!properties.ContainsKey(propertyKey))
                {
                    properties.Add(propertyKey, propertyValue);
                }
            }
        }

        #region Version
        /// <summary>
        /// Compares this properties version with the version in the properties file, returns true if they don't match
        /// </summary>
        /// <returns></returns>
        private bool IsNewVersion(string propertyFile)
        {
            int latestVersion = GetVersion(propertyFile);

            if (latestVersion == propertyVersion)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks the properties file for the version number
        /// </summary>
        /// <returns></returns>
        private int GetVersion(string propertyFile)
        {
            int latestVersion = 0;

            try
            {
                XmlNode xnQuery;
                XmlDocument xdLookup = new XmlDocument();
                xdLookup.Load(propertyFile);
                xnQuery = xdLookup.DocumentElement.SelectSingleNode("//property[@name=\"propertyservice.version\"]");
                latestVersion = Convert.ToInt32(xnQuery.InnerXml, CultureInfo.CurrentCulture.NumberFormat);
            }
            catch
            {
                string message = string.Format(Messages.PVPropertyValueMissing, "propertyservice.version");

                throw new SSException(message, false, SSExceptionIdentifier.PSMissingProperty);
            }

            return latestVersion;
        }

        #endregion
        
        #region Timer
        /// <summary>
        /// Sets up a timer to refresh the Properties loaded
        /// </summary>
        private void SetupTimer()
        {
            propertyRefreshTimer = new Timer();
            propertyRefreshTimer.Interval = RetrieveRefreshRate();
            propertyRefreshTimer.AutoReset = true;

            propertyRefreshTimer.Elapsed += new ElapsedEventHandler(propertyRefreshTimer_Elapsed);

            propertyRefreshTimer.Start();
        }

        /// <summary>
        /// Returns a the property refresh interval time
        /// </summary>
        private int RetrieveRefreshRate()
        {
            int refreshRate = 10000; // default

            try
            {
                refreshRate = Convert.ToInt32(this["propertyservice.refreshrate"], CultureInfo.CurrentCulture.NumberFormat);
            }
            catch
            {
                string message = string.Format(Messages.PVPropertyValueMissing, "propertyservice.refreshrate");

                throw new SSException(message, false, SSExceptionIdentifier.PSMissingProperty);
            }

            return refreshRate;
        }
        #endregion

        /// <summary>
        /// Checks if there is a configuration file by examining the number of Keys in the file.
        /// There should be at least one key, if not then there are missing mandatory values
        /// </summary>
        private bool CheckForConfigurationFile()
        {
            try
            {
                if (ConfigurationManager.AppSettings.Count == 0)
                {
                    throw new Exception();
                }
                else
                    return true;
            }
            catch
            {
                string message = string.Format(Messages.PSApplicationConfigFileMissing, Environment.CurrentDirectory);
                
                throw new SSException(message, false, SSExceptionIdentifier.APPMissingApplicationConfigurationFile);
            }
        }
        
        #endregion

        #region Event handlers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertyRefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (typeof(Properties))
            {
                Dictionary<string, string> newProperties = null;
                try
                {
                    newProperties = LoadProperties();
                }
                catch
                {
                    // If an exception is thrown, then the Properties file has been changed since
                    // application was loaded. Can't do anything other than retain the old set of properties
                }

                if (newProperties != null)
                {
                    Dictionary<string, string> oldProperties = propertyDictionary;

                    propertyDictionary = newProperties;

                    oldProperties = null;
                }
            }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the singleton instance of Properties
        /// </summary>
        public static Properties Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new Properties();
                    }

                    return instance;
                }
            }
        }

        /// <summary>
        /// Gets the property for a given property key. Otherwise returns string.empty
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                if (propertyDictionary.ContainsKey(key))
                {
                    return propertyDictionary[key];
                }

                return string.Empty;
            }
        }

        public string ApplicationID
        {
            get { return applicationID; }
        }

        #endregion
    }
}
