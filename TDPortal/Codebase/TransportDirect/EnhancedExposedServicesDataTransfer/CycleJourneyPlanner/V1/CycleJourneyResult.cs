// *********************************************** 
// NAME             : CycleJourneyResult.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class to hold details for a CycleJourneyResult returned by the CycleJourneyPlanner exposed service
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleJourneyResult.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:46   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold details for a CycleJourneyResult returned by the CycleJourneyPlanner exposed service.
    /// </summary>
    [Serializable]
    public class CycleJourneyResult
    {
        #region Private Fields
        private CompletionStatus completionStatus;
        private JourneyResult journeyResult;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CycleJourneyResult()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The overall completion status of the cycle journey planned.
        /// This will have a status of Success if the cycle journey was planned OK.
        /// A journey which failed will contain error messages specifying failure
        /// </summary>
        public CompletionStatus CompletionStatus
        {
            get { return completionStatus; }
            set { completionStatus = value; }
        }

        /// <summary>
        /// The cycle journey result
        /// </summary>
        public JourneyResult JourneyResult
        {
            get { return journeyResult; }
            set { journeyResult = value; }
        }
        #endregion
    }
}
