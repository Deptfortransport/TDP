// *********************************************** 
// NAME             : CycleJourney.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class for the CycleJourney returned in the CycleJourneyPlanner exposed service.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleJourney.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:42   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Class for the CycleJourney returned in the CycleJourneyPlanner exposed service.
    /// </summary>
    [Serializable]
    public class CycleJourney
    {
        #region Private Fields
        private CycleJourneySummary summary;
        private CycleJourneyDetail[] details;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public CycleJourney() { }
        #endregion

        #region Public Properties
        /// <summary>
        /// Summary information for the cycle journey result
        /// </summary>
        public CycleJourneySummary Summary
        {
            get { return summary; }
            set { summary = value; }
        }

        /// <summary>
        /// Detail information for the cycle journey result
        /// </summary>
        public CycleJourneyDetail[] Details
        {
            get { return details; }
            set { details = value; }
        }
        #endregion
    }
}
