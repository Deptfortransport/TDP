// *********************************************** 
// NAME             : RequestPreference.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class holds the user preference that influences the cycle journey planning
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/RequestPreference.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:56   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Class holds the user preference that influences the cycle journey planning
    /// </summary>
    [Serializable]
    public class RequestPreference
    {
        #region Private Fields
        private int preferenceId;
        private string preferenceValue;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RequestPreference()
        {

        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The id of the preference
        /// </summary>
        public int PreferenceId
        {
            get { return preferenceId; }
            set { preferenceId = value; }
        }

        /// <summary>
        /// The value of the preference
        /// </summary>
        public string PreferenceValue
        {
            get { return preferenceValue; }
            set { preferenceValue = value; }
        }
        #endregion
    }
}
