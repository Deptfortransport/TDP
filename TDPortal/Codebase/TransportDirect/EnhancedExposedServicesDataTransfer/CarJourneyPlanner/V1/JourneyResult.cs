// *********************************************** 
// NAME                 : JourneyResult.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class to hold the outward and return car journey returned by the CarJourneyPlanner exposed service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/JourneyResult.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:16   mmodi
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
    /// Class to hold the outward and return car journey returned by the CarJourneyPlanner exposed service.
    /// </summary>
    [System.Serializable]
    public class JourneyResult
    {
        private int journeyId;
        private Message[] errorMessages;
        private Message[] userWarnings;
        private CarJourney outwardCarJourney;
        private CarJourney returnCarJourney;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyResult()
        {
        }

        /// <summary>
        /// The journey Id provided in the Car Journey Request
        /// </summary>
        public int JourneyId
        {
            get { return journeyId; }
            set { journeyId = value; }
        }

        /// <summary>
        /// Contains any errors that occurred when planning the car journey
        /// e.g. the car journey planner could not plan the journey
        /// </summary>
        public Message[] ErrorMessages
        {
            get { return errorMessages; }
            set { errorMessages = value; }
        }

        /// <summary>
        /// Contains any warnings that the end user of the results should be aware of, 
        /// e.g. journey might start in the past, or the outward and return journey times overlap. 
        /// This text is language sensitive. This array may be populated only if journey planning was successful.
        /// </summary>
        public Message[] UserWarnings
        {
            get { return userWarnings; }
            set { userWarnings = value; }
        }

        /// <summary>
        /// Journey details for the outward journey. 
        /// This is populated only if journey planning was successful.
        /// </summary>
        public CarJourney OutwardCarJourney
        {
            get { return outwardCarJourney; }
            set { outwardCarJourney = value; }
        }

        /// <summary>
        /// Journey details for the return journey, if requested. 
        /// This is populated only if journey planning was successful.
        /// </summary>
        public CarJourney ReturnCarJourney
        {
            get { return returnCarJourney; }
            set { returnCarJourney = value; }
        }
    }
}
