//// ***********************************************
//// NAME           : TipOfTheDayProvider.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 18-Mar-2008
//// DESCRIPTION 	: Singleton class to provide a random "Tip of the Day"
//// ************************************************
////
////    Rev Devfactory Mar 18 2008 sbarker
////    Initial version

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
    /// Singleton class to provide a random "Tip of the Day"
    /// </summary>
    public class TipOfTheDayProvider
    {
        #region Private Static Fields
        
        private static TipOfTheDayProvider instance = null;
        private static readonly object instanceLock = new object();

        #endregion

        #region Public Static Properties

        public static TipOfTheDayProvider Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TipOfTheDayProvider();
                    }

                    return instance;
                }
            }
        }

        #endregion

        #region Private Enums

        private enum DataReaderFieldIndex
        {
            WaitPageTipTextEn = 0,
            WaitPageTipTextCy = 1,
            ThemeId = 2
        }

        #endregion

        #region Private Fields

        private readonly object getTipsLock = new object();
        private Dictionary<int,List<TipOfTheDay>> tips = new Dictionary<int,List<TipOfTheDay>>();
        private DateTime tipsExpiryDate = DateTime.MinValue;
        
        private readonly Random random = null;

        #endregion

        #region Private Constructor

        private TipOfTheDayProvider()
        {
            //Initialise the random number generator using
            //a seed to :
            unchecked
            {
                random = new Random((int)DateTime.Now.Ticks);
            }
        }

        #endregion

        #region Public Methods
        
        public string GetTip()
        {
           
            //Note that we lock in case another process is updating the
            //tips:
            lock (getTipsLock)
            {
                if (DateTime.Now > tipsExpiryDate)
                {
                    //Note that tips are valid for one day:
                    tips = GetTipsFromDatabase();

                    // Creating expiry date from the data refresh time set up in properties table
                    string datarefreshtime = Properties.Current["DailyDataRefreshTime"];
                    try
                    {
                        tipsExpiryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, int.Parse(datarefreshtime.Substring(0, 2)), int.Parse(datarefreshtime.Substring(2, 2)), 0);
                    }
                    catch
                    {
                        // set default expiry date to be 4:00 a.m.
                        tipsExpiryDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 4, 0, 0);
                    }
                }

                //Get the current language:
                Language currentLanguage = CurrentLanguage.Value;                

                int defaulttheme = ThemeProvider.Instance.GetDefaultTheme().Id;

                int theme = ThemeProvider.Instance.GetTheme().Id;

                //Get a random tip number. Note that this gets an int tipIndex, which 
                //is in the range 0 <= tipIndex < tips.Count.
                int tipIndex = 0;

                //Return the value, using the language index and the indexer
                //on the tips class:
                string tipmessage =  null;

                if (tips.ContainsKey(theme))
                {
                    theme = ThemeProvider.Instance.GetTheme().Id;

                    if (tips[theme].Count > 0)
                    {
                        tipIndex = random.Next(0, tips[theme].Count);

                        tipmessage = tips[theme][tipIndex][currentLanguage];
                    }
                   

                    if (string.IsNullOrEmpty(tipmessage))
                    {
                        tipIndex = random.Next(0, tips[defaulttheme].Count);

                        tipmessage = tips[defaulttheme][tipIndex][currentLanguage];
                    }
                }
                else
                {
                    tipIndex = random.Next(0, tips[defaulttheme].Count);

                    tipmessage = tips[defaulttheme][tipIndex][currentLanguage];
                }

                return tipmessage;
            }            
        }

        #endregion

        #region Private Methods

        private Dictionary<int,List<TipOfTheDay>> GetTipsFromDatabase()
        {
            Dictionary<int, List<TipOfTheDay> > tipDictionary = new Dictionary<int,List<TipOfTheDay>>();
            
            SqlHelper sqlHelper = null;            
            SqlDataReader reader = null;

            try
            {
                using (sqlHelper = new SqlHelper())
                {
                    sqlHelper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                   
                    using (reader = sqlHelper.GetReader("GetWaitPageMessageTips", new Hashtable()))
                    {
                        //Note that the reader returns two fields, 
                        //WaitPageTipTextEn and WaitPageTipTextCy. 
                        //These are described in the enum DataReaderFieldIndex to avoid 
                        //'magic numbers' in the code.
                        while (reader.Read())
                        {
                            int themeId = reader.GetInt32((int)DataReaderFieldIndex.ThemeId);

                            if(!tipDictionary.ContainsKey(themeId))
                                tipDictionary.Add(themeId, new List<TipOfTheDay>());

                            string valueEn = reader.GetString((int)DataReaderFieldIndex.WaitPageTipTextEn);
                            string valueCy = reader.GetString((int)DataReaderFieldIndex.WaitPageTipTextCy);

                            TipOfTheDay tip = new TipOfTheDay();

                            tip.AddValue(Language.English, valueEn);
                            tip.AddValue(Language.Welsh, valueCy);

                            tipDictionary[themeId].Add(tip);
                        }
                    }
                }

                return tipDictionary ;
            }
            finally
            {
                sqlHelper = null;
                reader = null;
            }
        }

        #endregion
    }
}
