// *********************************************** 
// NAME             : CycleRequestPreference.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Represents user preference that provide additional weightings to influence
///                 : the cycle journey, e.g. the user's average cycling speed
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleRequestPreference.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:50   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Represents user preference that provide additional weightings to influence
    /// the cycle journey, e.g. the user's average cycling speed
    /// </summary>
    [Serializable]
    public class CycleRequestPreference
    {
        #region Private Fields
        private string requestDescription;
        private RequestPreference requestPreference;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CycleRequestPreference()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Provides a description of this request preference
        /// e.g. "Maximum speed - in Kms", "Avoid ferries - Boolean"
        /// </summary>
        public string RequestDescription
        {
            get { return requestDescription; }
            set { requestDescription = value; }
        }

        /// <summary>
        /// Specifies the request preference, and can be passed in to the 
        /// CycleParameters.RequestPreferences array for a CycleJourneyRequest
        /// </summary>
        public RequestPreference RequestPreference
        {
            get { return requestPreference; }
            set { requestPreference = value; }
        }
        #endregion
    }
}
