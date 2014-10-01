// *********************************************** 
// NAME             : CycleRequestPreferenceAssembler.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Assembler class containing methods to return cycle request preferences
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleRequestPreferenceAssembler.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:50   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Assembler class containing methods to return cycle request preferences
    /// </summary>
    public class CycleRequestPreferenceAssembler
    {
        #region Private Fields
        private string NUMBER_OF_PREFERENCES = "CyclePlanner.TDUserPreference.NumberOfPreferences";
        private string CYCLEPLANNER_PREFERENCE_PREFIX = "CyclePlanner.TDUserPreference.Preference";
        private string CYCLEPLANNER_PREFERENCE_RESOURCE_PREFIX = "CyclePlanner.TDUserPreference.Preference";
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates and returns CycleRequestPreference dto object
        /// </summary>
        /// <returns></returns>
        public CycleRequestPreference[] GetCycleRequestPreferences()
        {
            // A property that denotes the size of the array of ser preferences expected by the Atkins CTP
            int numOfProperties = System.Convert.ToInt32(Properties.Current[NUMBER_OF_PREFERENCES]);

            List<CycleRequestPreference> cycleRequestPreferenceList = new List<CycleRequestPreference>();

            // Build the actual array of user preferences from permanent portal properties
            // these are used in the request sent to the Atkins CTP.
            for (int i = 0; i < numOfProperties; i++)
            {
                CycleRequestPreference cycleRequestPreference = new CycleRequestPreference();

                RequestPreference requestPreference = new RequestPreference();

                requestPreference.PreferenceId = i;

                requestPreference.PreferenceValue = Properties.Current[CYCLEPLANNER_PREFERENCE_PREFIX + i.ToString()];

                cycleRequestPreference.RequestDescription = GetRequestDescription(i);

                cycleRequestPreference.RequestPreference = requestPreference;

                cycleRequestPreferenceList.Add(cycleRequestPreference);
            }

            return cycleRequestPreferenceList.ToArray();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the description of the cycle request preference
        /// For example, 'Maximum speed - in Kms', 'Avoid ferries - Boolean'
        /// </summary>
        /// <param name="cycleRequestPreferenceId">Cycle request preference ID</param>
        /// <returns>string descript for the cycle request preference</returns>
        private string GetRequestDescription(int cycleRequestPreferenceId)
        {
            TDResourceManager rm = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.JOURNEY_PLANNER_SERVICE_RM);

            string resourceId = string.Format("{0}{1}", CYCLEPLANNER_PREFERENCE_RESOURCE_PREFIX, cycleRequestPreferenceId);

            return rm.GetString(resourceId);
        }
        #endregion
    }
}
