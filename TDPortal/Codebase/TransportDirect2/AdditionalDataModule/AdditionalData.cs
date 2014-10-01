// *********************************************** 
// NAME             : AdditionalData.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2014
// DESCRIPTION  	: AdditionalData class to retrieve date from the AdditionalData database table
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportDirect.AdditionalData.AccessModule;
using TDP.Common.PropertyManager;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.DataServices;
using TDP.Common.ServiceDiscovery;
using TDP.Common;
using TDP.Common.Extenders;
using System.Data;

namespace TDP.UserPortal.AdditionalDataModule
{
    /// <summary>
    /// AdditionalData class to retrieve date from the AdditionalData database table
    /// </summary>
    public class AdditionalData
    {
        #region Private variables

        private AdditionalDataAccessModule dataModule;

        private const string VALUE_COLUMN_NAME = "Value";
        private const string TYPE_COLUMN_NAME = "Type";
        private const string PDSKEY_COLUMN_NAME = "PDS Key";
        private const string COMMON_NAME_TYPE = "CommonName";
        private const string STOP_CATEGORY = "Stop";

        private const string PROP_LOOKUPCACHE_PREFIX = "AdditionalData.LookupCachePrefix";
        private const string PROP_LOOKUPCACHE_TIMEOUT = "AdditionalData.LookupCacheTimeoutSeconds";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public AdditionalData()
		{
            // Initialise the AdditionData access module - this allows acces to the AddtionalData database
            dataModule = new AdditionalDataAccessModule(Properties.Current[SqlHelperDatabase.AdditionalDataDB.ToString()]);
		}

        #endregion

        #region Public methods

        /// <summary>
        /// Implementation of IAdditionalData method
        /// </summary>
        /// <param name="type"></param>
        /// <param name="naptan"></param>
        /// <returns></returns>
        public string LookupFromNaPTAN(LookupType type, String naptan)
        {
            // Avoid nulls or empty strings throwing errors
            if (naptan == null || naptan.Equals(string.Empty))
            {
                return string.Empty;
            }

            string[] results = GetFromCache(type.Type, naptan);

            if (results != null)
            {
                return results[0];
            }

            DataSet data = dataModule.GetByType(pdsEnum.NaPTAN, naptan, type.Category, type.Type, (byte)1);

            if (data.Tables[0].Rows.Count != 0)
            {
                string result = data.Tables[0].Rows[0][VALUE_COLUMN_NAME].ToString();
                AddToCache(type.Type, naptan, new string[] { result });
                return result;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convenience method to get CRS code corresponding to specified NAPTAN
        /// </summary>
        /// <param name="naptan">NAPTAN</param>
        /// <returns>CRS code</returns>
        public string LookupCrsForNaptan(String naptan)
        {
            return LookupFromNaPTAN(LookupType.CRS_Code, naptan);
        }

        /// <summary>
        /// Get station name corresponding to specified NAPTAN
        /// </summary>
        /// <param name="naptan">NAPTAN</param>
        /// <returns>station name</returns>
        public string LookupStationNameForNaptan(String naptan)
        {
            DataSet data = dataModule.GetByType(pdsEnum.NaPTAN, naptan, STOP_CATEGORY, COMMON_NAME_TYPE, (byte)1);

            if (data.Tables[0].Rows.Count != 0)
            {
                return data.Tables[0].Rows[0][VALUE_COLUMN_NAME].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get NAPTAN list for either a CRS or NLC code provided
        /// </summary>
        /// <param name="code">code</param>
        /// <param name="type">type</param> 
        /// <returns>string array of naptans</returns>
        public string[] LookupNaptanForCode(String code, LookupType type)
        {
            string[] results = GetFromCache(type.Type, code);

            if (results == null)
            {
                IPropertyProvider properties = Properties.Current;

                string napString = properties["FindA.NaptanPrefix.Rail"];
                string naptanString = napString + "%";
                results = new string[0];

                // Avoid nulls or empty strings throwing errors
                if (!(code == null || code.Length == 0 || napString == null || napString.Length == 0))
                {
                    //Get the dataset
                    DataSet data = dataModule.GetByValue(pdsEnum.NaPTAN, naptanString, type.Category, type.Type, code, (byte)1);

                    results = new string[data.Tables[0].Rows.Count];

                    //Build String array from dataset returned
                    int i = 0;
                    foreach (DataRow row in data.Tables[0].Rows)
                    {
                        if (!(row[PDSKEY_COLUMN_NAME].ToString() == null || row[PDSKEY_COLUMN_NAME].ToString().Length == 0))
                        {
                            results[i] = row[PDSKEY_COLUMN_NAME].ToString();
                        }
                        i++;
                    }
                }
                AddToCache(type.Type, code, results);
            }

            //will return either empty string array or some naptans
            return results;
        }

        #endregion
        
        #region Private methods

        /// <summary>
        /// Retrieves an item from the cache
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private string[] GetFromCache(string type, string code)
        {
            ICache cache = TDPServiceDiscovery.Current.Get<ICache>(ServiceDiscoveryKey.Cache);

            string prefix = Properties.Current[PROP_LOOKUPCACHE_PREFIX];

            if (string.IsNullOrEmpty(prefix))
            {
                throw new TDPException(string.Format("AdditionalDataModule has a missing property[{0}], please check and add to properties.", PROP_LOOKUPCACHE_PREFIX), false, TDPExceptionIdentifier.PSMissingProperty);
            }

            return (string[])(cache[prefix + type + ":" + code]);
        }

        /// <summary>
        /// Adds an item to the cache
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="results"></param>
        private void AddToCache(string type, string code, string[] results)
        {
            ICache cache = TDPServiceDiscovery.Current.Get<ICache>(ServiceDiscoveryKey.Cache);

            int secs = Properties.Current[PROP_LOOKUPCACHE_TIMEOUT].Parse(1800);
            string prefix = Properties.Current[PROP_LOOKUPCACHE_PREFIX];

            if (string.IsNullOrEmpty(prefix))
            {
                throw new TDPException(string.Format("AdditionalDataModule has a missing property[{0}], please check and add to properties.", PROP_LOOKUPCACHE_PREFIX), false, TDPExceptionIdentifier.PSMissingProperty);
            }

            cache.Add(prefix + type + ":" + code, results, new TimeSpan(0, 0, secs));
        }

        #endregion
    }
}
