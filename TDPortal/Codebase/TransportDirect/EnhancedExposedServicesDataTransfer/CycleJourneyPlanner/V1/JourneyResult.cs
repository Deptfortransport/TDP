// *********************************************** 
// NAME             : JourneyResult.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class to hold the cycle journey returned by the CycleJourneyPlanner exposed service.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/JourneyResult.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:54   apatel
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
    /// Class to hold the cycle journey returned by the CycleJourneyPlanner exposed service.
    /// </summary>
    [Serializable]
    public class JourneyResult
    {
        #region Private Fields
        private int journeyId;
        private Message[] errorMessages;
        private Message[] userWarnings;
        private CycleJourney outwardCycleJourney;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public JourneyResult()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The id of the journey 
        /// </summary>
        public int JourneyId
        {
            get { return journeyId; }
            set { journeyId = value; }
        }

        /// <summary>
        /// Contains any errors that occured when planning the journey
        /// e.g. the journey planner could not plan the journey
        /// </summary>
        public Message[] ErrorMessages
        {
            get { return errorMessages; }
            set { errorMessages = value; }
        }

        /// <summary>
        /// Contains any warning that the end user of the results should be aware of 
        /// e.g. journey might start in the past.
        /// This text is language sensitive. This array may be populated only if journey 
        /// planning was successful
        /// </summary>
        public Message[] UserWarnings
        {
            get { return userWarnings; }
            set { userWarnings = value; }
        }

        /// <summary>
        /// Journey details for the outward journey. This is populated only if journey
        /// planning was successful
        /// </summary>
        public CycleJourney OutwardCycleJourney
        {
            get { return outwardCycleJourney; }
            set { outwardCycleJourney = value; }
        }
        #endregion
    }

}
