// *********************************************** 
// NAME                 : CarJourneyRequest.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class to hold details for a CarJourneyRequest sent to the CarJourneyPlanner exposed service. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarJourneyRequest.cs-arc  $
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
    /// Class to hold details for a CarJourneyRequest sent to the CarJourneyPlanner exposed service
    /// </summary>
    [System.Serializable]
    public class CarJourneyRequest
    {
        private JourneyRequest[] journeyRequests;
        private CarParameters carParameters;
        private ResultSettings resultSettings;

        /// <summary>
        /// Constructor
        /// </summary>
        public CarJourneyRequest()
        {
        }

        /// <summary>
        /// The array of car journeys to plan
        /// </summary>
        public JourneyRequest[] JourneyRequests
        {
            get { return journeyRequests; }
            set { journeyRequests = value; }
        }

        /// <summary>
        /// The car settings to use when planning the car journeys
        /// </summary>
        public CarParameters CarParameters
        {
            get { return carParameters; }
            set { carParameters = value; }
        }

        /// <summary>
        /// The settings to use when formatting the car journey result
        /// </summary>
        public ResultSettings ResultSettings
        {
            get { return resultSettings; }
            set { resultSettings = value; }
        }
    }
}
