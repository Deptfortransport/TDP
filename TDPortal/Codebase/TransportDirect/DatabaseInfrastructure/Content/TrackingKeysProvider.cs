// *********************************************** 
// NAME                 : TrackingKeysProvider.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 13/03/2010
// DESCRIPTION          : Tracking Key provider responsible to provide tracking key names 
//                        : for Intellitracker custom parameters
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DatabaseInfrastructure/Content/TrackingKeysProvider.cs-arc  $ 
//
//   Rev 1.3   Mar 16 2010 10:03:26   apatel
//Added header text
//Resolution for 5455: Update TrakingKeyProvider for Intellitracker

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using TD.ThemeInfrastructure;
using System.Collections;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Singleton class to encapsulate interaction with the tracking key content in content database.
    /// </summary>
    public sealed class TrackingKeysProvider
    {

        #region Private Enums

        /// <summary>
        /// Enum to interact with the data reader containing tracking key content.
        /// </summary>
        private enum DataReaderFieldIndex
        {
            ControlId = 0,
            key = 1
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static TrackingKeysProvider instance;
        /// <summary>
        /// Lock to allow thread safe interaction with singleton instance.
        /// </summary>
        private static readonly object instanceLock = new object();

        /// <summary>
        /// Lock to allow thread safe interaction with control properties collection
        /// </summary>
        private readonly object getControlPropertiesLock = new object();

        /// <summary>
        /// Lock to allow thread safe interaction with dictionary 
        /// </summary>
        private readonly object dictionaryLock = new object();

        /// <summary>
        /// Private collection used to stored active ControlPropertyCollections
        /// </summary>
        private Dictionary<string, ControlPropertyCollection> dictionary = new Dictionary<string, ControlPropertyCollection>();
       

        #endregion

        #region Public Static Properties

        /// <summary>
        /// Property to allow singleton instance to be obtained. Thread safe.
        /// </summary>
        public static TrackingKeysProvider Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TrackingKeysProvider();
                    }

                    return instance;
                }
            }
        }

        #endregion

        
        #region Internal Methods

        /// <summary>
        /// Determines whether a control collection for a page exists. If it does, the required 
        /// collection is returned as out parameter.
        /// </summary>
        /// <param name="pageId">Page for which control property collection needed</param>
        /// <param name="controlPropertyCollection">An out parameter that contains the 
        /// required collection if the method return is true.</param>
        /// <returns>Returns true if a collection for the required page exists. 
        /// False otherwise.</returns>
        internal bool CanGetControlPropertyCollection(string pageId, out ControlPropertyCollection controlPropertyCollection)
        {
            lock (dictionaryLock)
            {
                controlPropertyCollection = null;

                if (dictionary.ContainsKey(pageId))
                {
                    controlPropertyCollection = dictionary[pageId];
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Allows a control collection to be added to the internal collection
        /// </summary>
        /// <param name="pageId">The pageId for which the collection being added</param>
        /// <param name="controlPropertyCollection">The collection to add</param>
        internal void SetControlPropertyCollection(string pageId, ControlPropertyCollection controlPropertyCollection)
        {
            lock (dictionaryLock)
            {
                dictionary[pageId] = controlPropertyCollection;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Allows content for a particular page to be obtained.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public ControlPropertyCollection this[string pageId]
        {
            get
            {
                return GetControlProperties(pageId.ToLower());
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Builds hashtable to pass in to database call
        /// </summary>
        /// <param name="pageName"></param>
        /// <returns></returns>
        private Hashtable GetParameters(string pageId)
        {
            Hashtable parameters = new Hashtable();

            parameters.Add("@Page", pageId);

            return parameters;
        }

        /// <summary>
        /// Given a pageId, a collection of relavent 
        /// tracking keys is returned.
        /// </summary>
        /// <param name="pageId">The id of the page to retrieve tracking keys for</param>
        /// <returns></returns>
        private ControlPropertyCollection GetControlPropertyCollection(string pageId)
        {
            //The following field looks overly complex! However, it is required to
            //be this way. See notes in the class ControlPropertyCollection for
            //an explanation:
            Dictionary<string, Dictionary<string, string>> contentDictionary = new Dictionary<string, Dictionary<string, string>>();
            SqlHelper sqlHelper = null;
            SqlDataReader reader = null;

            try
            {
                using (sqlHelper = new SqlHelper())
                {
                    Hashtable parameters = GetParameters(pageId);

                    sqlHelper.ConnOpen(ContentDatabaseConnectionStringHelper.Get());

                    using (reader = sqlHelper.GetReader("GetPageTrackingKeys", parameters))
                    {
                        while (reader.Read())
                        {
                            //Note that we convert keys to lower case:
                            string controlName = reader.GetString((int)DataReaderFieldIndex.ControlId).ToLower();
                            string key = reader.GetString((int)DataReaderFieldIndex.key);

                            //Check to see if information about the current control 
                            //exists. If not, create a new dictionary.
                            if (!contentDictionary.ContainsKey(pageId))
                            {
                                contentDictionary.Add(pageId, new Dictionary<string, string>());
                            }

                            //Add the control property and value.
                            contentDictionary[pageId].Add(controlName, key);
                        }
                    }
                }

                return new ControlPropertyCollection(contentDictionary);
            }
            finally
            {
                sqlHelper = null;
                reader = null;
            }
        }

        /// <summary>
        /// Gets Tracking keys for given Page Id
        /// </summary>
        /// <param name="pageId">Page id for which tracking keys are required</param>
        /// <returns></returns>
        private ControlPropertyCollection GetControlProperties(string pageId)
        {
            
               
            ControlPropertyCollection controlPropertyCollection;

            //First attempt to get the content dictionary for the
            //specified page
            bool canGetControlPropertyCollection = CanGetControlPropertyCollection(pageId, out controlPropertyCollection);

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
                lock (getControlPropertiesLock)
                {
                    //We couldn't get it, so create a new one:
                    controlPropertyCollection = GetControlPropertyCollection(pageId);
                    SetControlPropertyCollection(pageId, controlPropertyCollection);
                }
            }

            return controlPropertyCollection;
            
        }
        #endregion


    }
}
