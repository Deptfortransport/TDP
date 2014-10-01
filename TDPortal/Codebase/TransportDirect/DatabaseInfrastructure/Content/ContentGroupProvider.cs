//// ***********************************************
//// NAME           : ContentGroupProvider.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 07-Jan-2008
//// DESCRIPTION 	: Class to allow content for a particular content group (usually a page) to be stored and managed in isolation.
//// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DatabaseInfrastructure/Content/ContentGroupProvider.cs-arc  $
//
//   Rev 1.3   Aug 04 2009 12:36:20   mmodi
//Overloaded method to accept Language
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
////    Rev Devfactory Jan 07 2008 12:00:00   sbarker
////    CCN 0427 - Class to allow content for a particular content group (usually a page) to be stored and managed in isolation.

using System;
using System.Collections.Generic;
using System.Text;
using TD.ThemeInfrastructure;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Class to allow content for a particular content group (usually a page) 
    /// to be stored and managed in isolation. Many of these will be contained
    /// in the ContentProvider class.
    /// </summary>
    public class ContentGroupProvider
    {
        #region Private Constants

        /// <summary>
        /// The amount of time a group of content is valid for. Perhaps this
        /// should be in a configuration file?
        /// </summary>
        private const int controlPropertyCollectionLifetimeInHours = 1;

        #endregion

        #region Private Fields

        private readonly ControlPropertyCollectionProvider controlPropertyCollectionProvider = new ControlPropertyCollectionProvider();
        private readonly object getControlPropertiesLock = new object();
        private readonly string groupName;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="groupName">The group name to be handled</param>
        internal ContentGroupProvider(string groupName)
        {
            this.groupName = groupName;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the current set of content (property names and values). The class
        /// automatically gets for the correct language and partner, 
        /// and manages the caching  of the values.
        /// </summary>
        /// <returns></returns>
        public ControlPropertyCollection GetControlProperties()
        {
            lock (getControlPropertiesLock)
            {
                //Get the current language
                
                // Place in try catch in case called from anywhere which does not have a HttpContext
                Language currentLanguage = Language.English; // Default is english
                try
                {
                    currentLanguage = CurrentLanguage.Value;
                }
                catch
                {
                    // No need to do anything, language is defaulted to English
                }
                                
                return GetControlProperties(currentLanguage);
            }
        }


        /// <summary>
        /// Gets the current set of content (property names and values). The class
        /// uses the language provided, and manages the caching  of the values.
        /// </summary>
        /// <returns></returns>
        public ControlPropertyCollection GetControlProperties(Language language)
        {
            lock (getControlPropertiesLock)
            {
                //Use the language provided, and get the current theme
                Language currentLanguage = language;

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

                // Make language theme key:
                LanguageThemeName key = new LanguageThemeName(currentLanguage, themeName);

                ControlPropertyCollection controlPropertyCollection;

                //First attempt to get the content dictionary for the
                //specified country:
                bool canGetControlPropertyCollection = controlPropertyCollectionProvider.CanGetControlPropertyCollection(key, out controlPropertyCollection);

                // Creating expiry date from the data refresh time set up in properties table
                DateTime newExpiryDate = DateTime.MinValue;
                DateTime currentExpiryDate;

                if (canGetControlPropertyCollection)
                {
                    currentExpiryDate = controlPropertyCollection.CreationDateTime;
                }
                else
                {
                    currentExpiryDate = DateTime.MinValue;
                }

                try
                {
                    //Get the time from the database when the settings will expire...
                    string dataRefreshTimeText = Properties.Current["DailyDataRefreshTime"];

                    //Convert the time to an int:
                    int dataRefreshTimeNumber = int.Parse(dataRefreshTimeText);
                    int hours = int.Parse(dataRefreshTimeNumber.ToString("0000").Substring(0, 2));
                    int minutes = int.Parse(dataRefreshTimeNumber.ToString("0000").Substring(2, 2));
                    newExpiryDate = new DateTime(currentExpiryDate.Year, currentExpiryDate.Month, currentExpiryDate.Day, hours, minutes, 0);
                }
                catch
                {
                    // set default expiry date to be 4:00 a.m.
                    DateTime now = DateTime.Now;
                    newExpiryDate = new DateTime(now.Year, now.Month, now.Day, 4, 0, 0);
                }

                //Make sure we use the next day:
                newExpiryDate = newExpiryDate.AddDays(1);

                if (!canGetControlPropertyCollection || newExpiryDate < DateTime.Now)
                {
                    //We couldn't get it, so create a new one:
                    controlPropertyCollection = ContentProvider.GetControlPropertyCollection(GroupName, currentLanguage);
                    controlPropertyCollectionProvider.SetControlPropertyCollection(key, controlPropertyCollection);
                }

                return controlPropertyCollection;
            }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets the name of the group managed by the instance.
        /// </summary>
        protected string GroupName
        {
            get
            {
                return groupName;
            }
        }

        #endregion
    }
}
