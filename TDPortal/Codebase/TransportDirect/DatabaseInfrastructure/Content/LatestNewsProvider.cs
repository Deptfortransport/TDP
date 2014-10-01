//// ***********************************************
//// NAME           : LatestNewsProvider.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 28-Mar-2008
//// DESCRIPTION 	: Exposes data held in the Latest News table (PermanentPortal.HomePageMessage)
//// ************************************************
////
////    Rev Devfactory Mar 28 2008 sbarker
////    Initial version

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using TD.ThemeInfrastructure;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Singleton class to expose information from the HomePageMessage table of the 
    /// PermanentPortal
    /// </summary>
    public sealed class LatestNewsProvider
    {
        #region Private Constants

        private const string ThemeNamePlaceholder = "{Theme_Name}";

        #endregion

        #region Private Static Fields

        private static LatestNewsProvider instance = null;
        private static readonly object instanceLock = new object();

        #endregion

        #region Public Static Properties
        
        public static LatestNewsProvider Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LatestNewsProvider();
                    }

                    return instance;
                }
            }
        }

        #endregion

        #region Private Constructor

        private LatestNewsProvider()
        {
            //No implementation
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Returns all text from the news table marked with Display = 1, correctly ordered.
        /// </summary>
        /// <returns></returns>
        public string GetGeneralNewsHtml()
        {
            return GetHtml("GetLatestNews");
        }

        /// <summary>
        /// Returns just the Special Notice Board text, regardless of whether Display = 1
        /// </summary>
        /// <returns></returns>
        public string GetSpecialNoticeHtml()
        {
            return GetHtml("GetSpecialNotice");
        }

        /// <summary>
        /// Returns just the Seasonal text, regardless of whether Display = 1
        /// </summary>
        /// <returns></returns>
        public string GetSeasonalNoticeHtml()
        {
            return GetHtml("GetSeasonalNotice");
        }

        #endregion

        #region Private Methods

        private string GetHtml(string storedProcedureName)
        {
            SqlHelper helper = null;
            string html;
            
            try
            {
                using (helper = new SqlHelper())
                {
                    helper.ConnOpen(SqlHelperDatabase.DefaultDB);
                    html =(string)helper.GetScalar(storedProcedureName, GetParameters());                    
                }
            }
            finally
            {
                helper = null;
            }

            //Now do any required find and replaces:
            html = html.Replace(ThemeNamePlaceholder, ThemeProvider.Instance.GetTheme().Name);

            return html;
        }

        private Hashtable GetParameters()
        {
            Hashtable parameters = new Hashtable();

            switch (CurrentLanguage.Value)
            {
                case Language.Welsh:
                    parameters.Add("@Language", "cy-GB");
                    break;
                case Language.English:
                default:
                    parameters.Add("@Language", "en-GB");
                    break;
            }

            return parameters;
        }

        #endregion
    }
}
