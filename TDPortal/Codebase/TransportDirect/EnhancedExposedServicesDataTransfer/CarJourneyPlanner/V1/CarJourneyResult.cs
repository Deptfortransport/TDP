// *********************************************** 
// NAME                 : CarJourneyResult.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class to hold details for a CarJourneyResult returned by the CarJourneyPlanner exposed service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarJourneyResult.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:12   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold details for a CarJourneyResult returned by the CarJourneyPlanner exposed service.
    /// </summary>
    [System.Serializable]
    public class CarJourneyResult
    {
        private CompletionStatus completionStatus;
        private JourneyResult[] journeyResults;
                
        /// <summary>
        /// Constructor
        /// </summary>
        public CarJourneyResult()
        {
        }

        /// <summary>
        /// The overall completion status of the car journeys planned. 
        /// This will have a status of Success if all car journeys were planned OK.
        /// Any journeys which failed will contain error messages specifiying failure
        /// </summary>
        public CompletionStatus CompletionStatus
        {
            get { return completionStatus; }
            set { completionStatus = value; }
        }

        /// <summary>
        /// The array of car journeys planned
        /// </summary>
        public JourneyResult[] JourneyResults
        {
            get { return journeyResults; }
            set { journeyResults = value; }
        }
    }
}
