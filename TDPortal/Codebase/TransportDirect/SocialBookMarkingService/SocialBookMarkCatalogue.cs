// *********************************************** 
// NAME                 : SocialBookMarkingCatalogue.cs
// AUTHOR               : Phil Scott
// DATE CREATED         : 31/07/2009 
// DESCRIPTION			: This class contains the reference data that will be 
//                      : used by the Social BookMarking Control. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SocialBookMarkingService/SocialBookMarkCatalogue.cs-arc  $
//
//   Rev 1.1   Oct 06 2009 14:38:24   apatel
//Social bookmarking changes
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.0   Sep 23 2009 12:19:22   PScott
//Initial revision.
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.0   Jul 31 2009 16:44:52   pscott
//Initial revision.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TD.ThemeInfrastructure;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.SocialBookMarkingService
{
    public sealed class SocialBookMarkCatalogue 
	{
		#region Private members

        private Dictionary<int, ArrayList> socialBookMarkDictionary;

        private const string SP_SBM = "GetSocialBookMarkData";
        
        private TransportDirect.Common.DatabaseInfrastructure.SqlHelperDatabase sourceDB;

		#endregion

		#region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SocialBookMarkCatalogue()
        {
            this.sourceDB = SqlHelperDatabase.TransientPortalDB;

            LoadSocialBookMarks(sourceDB);
        }

		#endregion

		#region Public methods

        
		/// <summary>
		/// Obtains the SocialBookMarks object 
		/// </summary>
        /// <returns>A SocialBookMarks info object</returns>
        public SocialBookMark[] GetSocialBookMark(int themeId)
		{
            if (socialBookMarkDictionary != null)
            {
                if (socialBookMarkDictionary.ContainsKey(themeId))
                {
                    // Get all the social bookmarks for this theme id
                    ArrayList socialBookmarksArrayList = socialBookMarkDictionary[themeId];

                    // Convert into the correct type
                    SocialBookMark[] socialBookmarks = (SocialBookMark[])socialBookmarksArrayList.ToArray(typeof(SocialBookMark));

                    return socialBookmarks;
                }
                else
                {
                    // Get all the social bookmarks for the default theme id
                    ArrayList socialBookmarksArrayList = socialBookMarkDictionary[ThemeProvider.Instance.GetDefaultTheme().Id];

                    // Convert into the correct type
                    SocialBookMark[] socialBookmarks = (SocialBookMark[])socialBookmarksArrayList.ToArray(typeof(SocialBookMark));

                    return socialBookmarks;

                }
            }
            else
            {
                return new SocialBookMark[0];
            }
		}
        
		#endregion

		#region Private methods


		/// <summary>
		/// Loads Car Parks into array holding all data.
		/// </summary>
		/// <param name="sourceDB">Source Database</param>
		/// <param name="carParkRef">Car park reference</param>
        /// 
        private void LoadSocialBookMarks(SqlHelperDatabase sourceDB)
		{

			// create new district obj, add to array list and pass back as array.
			ArrayList socialBookMarkList = new ArrayList();
			Hashtable sqlParam = new Hashtable();
			SqlHelper helper = new SqlHelper();
            SqlDataReader reader;

			try
            {
                // Initialise a SqlHelper and connect to the database.
                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Info, "Opening database connection to " + sourceDB.ToString()));
                
                // Open connection and get a DataReader
                helper.ConnOpen(sourceDB);

                // Get data
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Loading Social Bookmarks data started"));
                }

                Dictionary<int, ArrayList> socialBookMarkDictionaryTemp = new Dictionary<int, ArrayList>();

                // Execute the Social Bookmark stored procedure.
                // This returns all the visible social bookmarks data for all themes
                reader = helper.GetReader(SP_SBM, new Hashtable());

                int SBMIdColumnOrdinal = reader.GetOrdinal("SBMId");
                int SBMDescriptionColumnOrdinal = reader.GetOrdinal("SBMDescription");
                int SBMDisplayTextColumnOrdinal = reader.GetOrdinal("SBMDisplayText");
                int SBMDisplayIconPathColumnOrdinal = reader.GetOrdinal("SBMDisplayIconPath");
                int SBMURLTemplateColumnOrdinal = reader.GetOrdinal("SBMURLTemplate");
                int SBMLandingPartnerCodeColumnOrdinal = reader.GetOrdinal("SBMLandingPartnerCode");
                int SBMThemeIdColumnOrdinal = reader.GetOrdinal("SBMThemeId");

                while (reader.Read())
                {
                    int SBMId = reader.GetInt32(SBMIdColumnOrdinal);
                    string SBMDescription = reader.GetString(SBMDescriptionColumnOrdinal);
                    string SBMDisplayText = reader.GetString(SBMDisplayTextColumnOrdinal);
                    string SBMDisplayIconPath = reader.GetString(SBMDisplayIconPathColumnOrdinal);
                    string SBMURLTemplate = reader.GetString(SBMURLTemplateColumnOrdinal);
                    string SBMLandingPartnerCode = reader.GetString(SBMLandingPartnerCodeColumnOrdinal);
                    int SBMThemeId = reader.GetInt32(SBMThemeIdColumnOrdinal);

                    SocialBookMark socialBookmark = new SocialBookMark(SBMId,
							SBMDescription, SBMDisplayText, SBMDisplayIconPath,
                            SBMURLTemplate, SBMLandingPartnerCode, SBMThemeId);

                    AddSocialBookmark(ref socialBookMarkDictionaryTemp, socialBookmark);

                }
                reader.Close();

                // Initialise dictionary if null
                if (socialBookMarkDictionary == null)
                {
                    socialBookMarkDictionary = new Dictionary<int, ArrayList>();
                }

                // Assign the temporary load to the actual dictionary 
                socialBookMarkDictionary = socialBookMarkDictionaryTemp;

                // Record the fact that the data was loaded.
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Loading Social Bookmarks completed"));
                }
            }
            catch (Exception e)
            {
                // Catching the base Exception class because we don't want any possibility
                // of this raising any errors outside of the class in case it causes the
                // application to fall over. As long as the exception doesn't occur in 
                // the final block of code, which copies the new data into the module-level
                // hashtables and arraylists, the object will still be internally consistant,
                // although the data will be inconsistant with that stored in the database.
                // One exception to this: if this is the first time LoadData has been run,
                // the exception should be raised.
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An error occurred whilst attempting to reload the Social Bookmarks data.", e));
                if (((socialBookMarkDictionary == null) || (socialBookMarkDictionary.Count == 0)))
                {
                    throw;
                }

            }
            finally
            {
                //close the database connection
                helper.ConnClose();
            }

		}

        /// <summary>
        /// Adds the supplied SocialBookmark to the correct array (based on theme) in the dictionary
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="socialBookmark"></param>
        private void AddSocialBookmark(ref Dictionary<int, ArrayList> dictionary, SocialBookMark socialBookmark)
        {
            int themeOfSocialBookmark = socialBookmark.SBMThemeId;

            if (dictionary.ContainsKey(themeOfSocialBookmark))
            {
                // Add to existing theme array
                ArrayList socialBookmarks = dictionary[themeOfSocialBookmark];

                socialBookmarks.Add(socialBookmark);

                // Remove existing array of social bookmarks for this theme
                dictionary.Remove(themeOfSocialBookmark);

                // Add the updated array of social bookmarks back in to the dictionary
                dictionary[themeOfSocialBookmark] = socialBookmarks;
            }
            else
            {
                // Create a new array for this theme to add this social bookmark to
                ArrayList socialBookmarks = new ArrayList();

                socialBookmarks.Add(socialBookmark);

                dictionary.Add(themeOfSocialBookmark, socialBookmarks);
            }
        }
    }
        #endregion

}

