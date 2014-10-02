// *********************************************** 
// NAME			: LatestNewsProvider.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 08/04/08
// DESCRIPTION	: Class to provide Latest news information 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LatestNewsService/LatestNewsProvider.cs-arc  $
//
//   Rev 1.2   Oct 10 2012 14:27:28   mmodi
//Updated trace logging level
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.1   Apr 10 2008 15:43:20   mmodi
//Updates for testing
//
//   Rev 1.0   Apr 09 2008 18:20:14   mmodi
//Initial revision.
//

using System;
using System.Collections;
using System.Data.SqlClient;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TD.ThemeInfrastructure;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.LatestNewsService
{
    /// <summary>
    /// Summary description for LatestNewsProvider
    /// </summary>
    public class LatestNewsProvider
    {
        #region Private members

        #region Constants
        private const string DataChangeNotificationGroup = "LatestNewsService";
        private const string SpecialContentStoredProcedure = "GetSpecialContent";
        private const string ThemeNamePlaceholder = "{Theme_Name}";

        // Properties used for the Latest news keys
        private const string PropertyLatestNewsPosting = "LatestNewsProvider.LatestNewsPosting";
        private const string PropertyLatestNewsPlaceHolder = "LatestNewsProvider.LatestNewsPlaceHolder";

        // Properties used for the Special notice board keys
        private const string PropertySpecialNoticeBoardPosting = "LatestNewsProvider.SpecialNoticeBoardPosting";
        private const string PropertySpecialNoticeBoardPlaceHolder = "LatestNewsProvider.SpecialNoticeBoardPlaceHolder";

        private const string PropertyMaxTextLengthCharacters = "LatestNewsProvider.MaxTextLengthCharacters";
        #endregion

        private bool receivingChangeNotifications;

        // Hashtable to hold content
        private Hashtable specialContent = new Hashtable();

        // Used for loading the reference data
        private Hashtable specialContentCurrentLoad = new Hashtable();

        // values used to determine a latest news posting. Defaults, will be overwritten from Properties
        private string latestNewsPosting = "LatestNews";
        private string latestNewsPlaceholder = "TDInformationHtmlPlaceholderDefinition";

        // values used to determine a special notice board posting. Defaults, will be overwritten from Properties
        private string specialNoticeBoardPosting = "SpecialNoticeBoard";
        private string specialNoticeBoardPlaceholder = "SpecialNoticeBoardHtmlPlaceHolder";

        private int maxTextLengthCharacters = 2000000;

        #region Struct
        /// <summary>
        /// Structure used to define hashkey for accessing data cached in latestNews hashtable
        /// </summary>
        private struct SpecialContentKey
        {
            public string pagePosting, placeholder, culture;

            public SpecialContentKey(string pagePosting, string placeholder, string culture)
            {
                this.pagePosting = pagePosting;
                this.placeholder = placeholder;
                this.culture = culture;
            }
        }
        #endregion

        #endregion

        #region Constructor
        /// <summary>
		/// Default constructor, loads data and registers for change notification
		/// </summary>
        public LatestNewsProvider()
		{
            LoadPropertyValues();

            LoadData();
			
            receivingChangeNotifications = RegisterForChangeNotification();
        }
		#endregion

        #region Private methods

        #region Change notification
        /// <summary>
        /// Registers an event handler with the data change notification service
        /// </summary>
        private bool RegisterForChangeNotification()
        {
            IDataChangeNotification notificationService;
            try
            {
                notificationService = (IDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
            }
            catch (TDException e)
            {
                // If the SDInvalidKey TDException is thrown, return false as the notification service
                // hasn't been initialised.
                // Otherwise, rethrow the exception that was received.
                if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising LatestNewsProvider"));
                    return false;
                }
                else
                    throw;
            }
            catch
            {
                // Non-CLS-compliant exception
                throw;
            }

            notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
            return true;
        }
        #endregion

        /// <summary>
        /// Loads the data and performs pre processing
        /// </summary>
        private void LoadData()
        {
            SqlHelper helper = new SqlHelper();

            try
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.TransientPortalDB.ToString() + " to load SpecialContent data"));

                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Verbose, "Loading SpecialContent data started."));

                // Clear out current load
                specialContentCurrentLoad.Clear();

                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                #region Load Special Content, includes Latest news and Special Notice Board
                // Get the latest news data
                SqlDataReader reader = helper.GetReader(SpecialContentStoredProcedure);

                //Assign the column ordinals
                int postingColumnOrdinal = reader.GetOrdinal("Posting");
                int placeholderColumnOrdinal = reader.GetOrdinal("PlaceHolder");
                int cultureColumnOrdinal = reader.GetOrdinal("Culture");
                int htmltextColumnOrdinal = reader.GetOrdinal("Text");

                // Add the latest news items
                while (reader.Read())
                {
                    string posting = reader.GetString(postingColumnOrdinal);
                    string placeholder = reader.GetString(placeholderColumnOrdinal);
                    string culture = reader.GetString(cultureColumnOrdinal);
                    string htmltext = reader.GetString(htmltextColumnOrdinal);

                    AddRecord(posting, placeholder, culture, htmltext);
                }
                reader.Close();
                
                #endregion

                // Assign currentload to public accessable hash
                specialContent = specialContentCurrentLoad;

                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Verbose, "Loading SpecialContent data completed"));
                
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An error occurred whilst attempting to load the SpecialContent data.", e));
            }
            finally
            {
                //close the database connection
                if (helper.ConnIsOpen)
                    helper.ConnClose();

                helper = null;
            }
        }

        /// <summary>
        /// Adds Latest news information to the cache
        /// </summary>
        private void AddRecord(string posting, string placeholder, string culture, string htmltext)
        {
            // Validate text for length - to avoid potentially large items beings added to the cache
            if (htmltext.Length > maxTextLengthCharacters)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                    TDTraceLevel.Error,
                    "Special content text length is larger than permitted character length: " + maxTextLengthCharacters +
                    ". For item Posting: " + posting +
                    " Placeholder: " + placeholder +
                    " Culture: " + culture));
            }
            else
            {
                SpecialContentKey specialContentKey = new SpecialContentKey(posting, placeholder, culture);

                // There shouldn't be a duplicate but for sanity add check
                if (specialContentCurrentLoad.ContainsKey(specialContentKey))
                {
                    specialContentCurrentLoad.Remove(specialContentKey);
                }

                specialContentCurrentLoad.Add(specialContentKey, htmltext);
            }
        }

        /// <summary>
        /// Performs a find and replace for theme specific values in a string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string UpdateForTheme(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string themename = string.Empty;
                try
                {
                    // Try to get themename for current context
                    themename = ThemeProvider.Instance.GetTheme().Name;
                }
                catch
                {
                    // otherwise use default themename
                    themename = ThemeProvider.Instance.GetDefaultTheme().Name;
                }

                text = text.Replace(ThemeNamePlaceholder, themename);
            }
            else
                text = string.Empty;

            return text;
        }

        /// <summary>
        /// Loads values needed from the Property service
        /// </summary>
        private void LoadPropertyValues()
        {
            try
            {
                string propertyLatestNewsPosting = Properties.Current[PropertyLatestNewsPosting];
                string propertyLatestNewsPlaceholder = Properties.Current[PropertyLatestNewsPlaceHolder];
                string propertySpecialNoticeBoardPosting = Properties.Current[PropertySpecialNoticeBoardPosting];
                string propertySpecialNoticeBoardPlaceholider = Properties.Current[PropertySpecialNoticeBoardPlaceHolder];

                if (!string.IsNullOrEmpty(propertyLatestNewsPosting))
                    latestNewsPosting = propertyLatestNewsPosting;

                if (!string.IsNullOrEmpty(propertyLatestNewsPlaceholder))
                    latestNewsPlaceholder = propertyLatestNewsPlaceholder;

                if (!string.IsNullOrEmpty(propertySpecialNoticeBoardPosting))
                    specialNoticeBoardPosting = propertySpecialNoticeBoardPosting;

                if (!string.IsNullOrEmpty(propertySpecialNoticeBoardPlaceholider))
                    specialNoticeBoardPlaceholder = propertySpecialNoticeBoardPlaceholider;

                string maxTextLengthCharactersString = Properties.Current[PropertyMaxTextLengthCharacters];
                if (!string.IsNullOrEmpty(maxTextLengthCharactersString))
                    maxTextLengthCharacters = int.Parse(maxTextLengthCharactersString);

            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An error occurred whilst attempting to read the LatestNewsProvider properties.", e));
            }
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Used by the Data Change Notification service to reload the data if it is changed in the DB
        /// </summary>
        private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
        {
            if (e.GroupId == DataChangeNotificationGroup)
                LoadData();
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Retrieves the Latest news for the specified language
        /// Uses Posting and Placeholder from properties
        /// </summary>
        public string GetLatestNews (string culture)
        {
            SpecialContentKey specialContentKey = new SpecialContentKey(latestNewsPosting, latestNewsPlaceholder, culture);

            if (!specialContent.ContainsKey(specialContentKey))
                return string.Empty;
            else
            {
                // Do any Theme replacements before returning
                return UpdateForTheme((string)specialContent[specialContentKey]);
            }
        }

        /// <summary>
        /// Retrieves the Latest news for the specified language and placeholder.
        /// Uses Posting from properties
        /// </summary>
        public string GetLatestNews(string culture, string placeholder)
        {
            SpecialContentKey specialContentKey = new SpecialContentKey(latestNewsPosting, placeholder, culture);

            if (!specialContent.ContainsKey(specialContentKey))
                return string.Empty;
            else
            {
                // Do any Theme replacements before returning
                return UpdateForTheme((string)specialContent[specialContentKey]);
            }
        }

        /// <summary>
        /// Retrieves the Special notice for the specified language
        /// Uses Posting and Placeholder from properties
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public string GetSpecialNotice(string culture)
        {
            SpecialContentKey specialContentKey = new SpecialContentKey(specialNoticeBoardPosting, specialNoticeBoardPlaceholder, culture);

            if (!specialContent.ContainsKey(specialContentKey))
                return string.Empty;
            else
            {
                // Do any Theme replacements before returning
                return UpdateForTheme((string)specialContent[specialContentKey]);
            }
        }

        /// <summary>
        /// Retrieves the Special notice for the specified language and placeholder
        /// Uses Posting from properties
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public string GetSpecialNotice(string culture, string placeholder)
        {
            SpecialContentKey specialContentKey = new SpecialContentKey(specialNoticeBoardPosting, placeholder, culture);

            if (!specialContent.ContainsKey(specialContentKey))
                return string.Empty;
            else
            {
                // Do any Theme replacements before returning
                return UpdateForTheme((string)specialContent[specialContentKey]);
            }
        }

        #endregion
    }
}
