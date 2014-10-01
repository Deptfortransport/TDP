// *********************************************** 
// NAME             : CycleJourneyRequest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class to hold details for a CycleJourneyRequest sent to the CycleJourneyPlanner exposed service
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleJourneyRequest.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:46   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold details for a CycleJourneyRequest sent to the CycleJourneyPlanner exposed service
    /// </summary>
    [System.Serializable]
    public class CycleJourneyRequest
    {
        #region Private Fields
        private JourneyRequest journeyRequest;
        private CycleParameters cycleParameters;
        private ResultSettings resultSettings;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public CycleJourneyRequest()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        ///  Specifies the journey to be planned
        /// </summary>
        public JourneyRequest JourneyRequest
        {
            get { return journeyRequest; }
            set { journeyRequest = value; }
        }

        /// <summary>
        /// Contains the parameters to be used for the cycle journey (e.g. algorithm, user preferences)
        /// </summary>
        public CycleParameters CycleParameters 
        {
            get { return cycleParameters; }
            set { cycleParameters = value; } 
        }

        /// <summary>
        /// Specifies values for the result output(e.g. language, coordinate separator, include geometry)
        /// </summary>
        public ResultSettings ResultSettings
        {
            get { return resultSettings; }
            set { resultSettings = value; }
        }

        #endregion


    }
}
