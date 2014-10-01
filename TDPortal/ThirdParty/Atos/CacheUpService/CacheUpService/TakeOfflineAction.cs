#region Amendment history
// *********************************************** 
// NAME			: $Workfile:   TakeOfflineAction.cs  $
// AUTHOR		: Peter Norell
// DATE CREATED	: 01/11/2007
// REVISION		: $Revision:   1.1  $
// DESCRIPTION	: An ICacheAction class
// ************************************************ 
// $Log:   P:\archives\Codebase\WebTIS\CacheUpService\TakeOfflineAction.cs-arc  $ 
//
//   Rev 1.1   Nov 05 2007 11:41:22   p.norell
//Updated to cater for allowed success/failures for taking servers offline and putting them back online.
//
//   Rev 1.0   Nov 02 2007 16:57:46   p.norell
//Initial Revision
//
#endregion
#region Imports
using System;
using System.Collections.Generic;
using System.Text;
using WT.Properties;
using System.IO;

#endregion

namespace WT.CacheUpService
{
    /// <summary>
    /// Takes a server offline if severity error has occured.
    /// Has ability to put back online again, if the code is not put back online and severity is none or verbose.
    /// </summary>
    public class TakeOfflineAction : ICacheAction
    {
        #region Settings properties

        private string PROP_OnlineFile = "TakeOffline{0}.File.OnlineFile";
        private string PROP_OfflineFile = "TakeOffline{0}.File.OfflineFile";
        private string PROP_AllowedSuccessFailures = "TakeOffline{0}.AllowedSuccessFailures";
        private string PROP_PutOnline = "TakeOffline{0}.PutOnline";
        private string PROP_OnlyIf = "TakeOffline{0}.OnlyIf";

        #endregion

        #region Local parameters

        private bool tookOffline = false;
        private int successes = 0;
        private int failures = 0;

        #endregion

        #region ICacheAction Members

        /// <summary>
        /// Executes on the given level
        /// </summary>
        /// <param name="level">The level for the current running URL</param>
        /// <param name="settingsId">The settings set id to read (empty will use default set)</param>
        public void ExecuteOn(WT.Common.Logging.WTTraceLevel level, string settingsId)
        {
            #region Read settings

            IPropertyService prop = PropertyService.Current;

            #region Check whether to use default settings

            // Determine whether to use the default settings, or the settings for the 
            // set id supplied
            bool useDefault = true;

            if (!string.IsNullOrEmpty(settingsId))
            {
                // Check mandatory properties exist (any missing, then use default)
                useDefault = (string.IsNullOrEmpty(prop[string.Format(PROP_OnlineFile, settingsId)]))
                             ||
                             (string.IsNullOrEmpty(prop[string.Format(PROP_OfflineFile, settingsId)]));
            }

            // No settings id provided, or the settings were missing for the supplied set id
            if (useDefault)
            {   
                // Clear any settings id provided
                settingsId = string.Empty;
            }

            #endregion

            // Read the Online and Offline files for the settings (mandatory)
            string fileOnline = prop[string.Format(PROP_OnlineFile, settingsId)];
            string fileOffline = prop[string.Format(PROP_OfflineFile, settingsId)];

            int allowedSuccessFailures = 1;
            try
            {
                allowedSuccessFailures = int.Parse(prop[string.Format(PROP_AllowedSuccessFailures, settingsId)]);
            }
            catch
            {
                // Ignore if value is illegal
            }

            bool willPutbackOnline = false;
            if (!string.IsNullOrEmpty(prop[string.Format(PROP_PutOnline, settingsId)]))
            {
                willPutbackOnline = bool.Parse(prop[string.Format(PROP_PutOnline, settingsId)]);
            }

            bool onlyIf = true;
            if (!string.IsNullOrEmpty(prop[string.Format(PROP_OnlyIf, settingsId)]))
            {
                onlyIf = bool.Parse(prop[string.Format(PROP_OnlyIf, settingsId)]);
            }

            #endregion

            #region Take offline or put online

            if ((level == WT.Common.Logging.WTTraceLevel.None || 
                level == WT.Common.Logging.WTTraceLevel.Verbose) && 
                willPutbackOnline)
            {
                failures = 0;
                successes++;
                if ( (successes % allowedSuccessFailures) == 0 && ( !onlyIf || onlyIf && tookOffline) )
                {
                    // Take offline
                    if (File.Exists(fileOffline))
                    {
                        // Put online
                        File.Move(fileOffline, fileOnline);
                        tookOffline = false;
                    }
                }
                // Reset counter when the max limit is reached
                successes = successes % allowedSuccessFailures;
            }
            else if (level == WT.Common.Logging.WTTraceLevel.Error)
            {
                successes = 0;
                failures++;
                if ((failures % allowedSuccessFailures) == 0)
                {
                    // Take offline
                    if (File.Exists(fileOnline))
                    {
                        File.Move(fileOnline, fileOffline);
                        tookOffline = true;
                    }
                }
                // Reset counter when the max limit is reached
                failures = failures % allowedSuccessFailures;
            }

            #endregion
        }
        #endregion
    }
}
