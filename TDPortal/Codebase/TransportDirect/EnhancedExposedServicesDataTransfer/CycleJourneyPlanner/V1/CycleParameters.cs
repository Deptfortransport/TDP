// *********************************************** 
// NAME             : CycleParameters.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: CycleParameters represents the parameters to be used when planning the cycle journey
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleParameters.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:48   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// CycleParameters represents the parameters to be used when planning the cycle journey
    /// </summary>
    [Serializable]
    public class CycleParameters
    {
        #region Private Fields
        private string algorithm;
        private RequestPreference[] requestPreferences;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CycleParameters()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Specifies the penalty function to call (e.g. quickest, quietest)
        /// 
        /// The penalty function will be in the format:
        ///     "Call &lt;DLL&gt;,&lt;algorithm name&gt;"
        /// </summary>
        public string Algorithm
        {
            get { return algorithm; }
            set { algorithm = value; }
        }

        /// <summary>
        /// Specifies the request preferences to influence the journey
        /// (e.g. maximum speed, avoid roads)
        /// 
        /// If the values are not provied the default request preferences will be used
        /// </summary>
        public RequestPreference[] RequestPreferences
        {
            get { return requestPreferences; }
            set { requestPreferences = value; }
        }
        #endregion
    }

}
