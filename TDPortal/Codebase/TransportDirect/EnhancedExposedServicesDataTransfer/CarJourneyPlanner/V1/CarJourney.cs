// *********************************************** 
// NAME                 : CarJourney.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class for the CarJourney returned in the CarJourneyPlanner exposed service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarJourney.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:10   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Class for the CarJourney returned in the CarJourneyPlanner exposed service.
    /// </summary>
    [System.Serializable]
    public class CarJourney
    {
        private CarJourneySummary carJourneySummary;
        private CarJourneyDetail[] carJourneyDetails;

        /// <summary>
        /// Constructor
        /// </summary>
        public CarJourney()
        {
        }

        /// <summary>
        /// Summary information for the journey
        /// </summary>
        public CarJourneySummary Summary
        {
            get { return carJourneySummary; }
            set { carJourneySummary = value; }
        }

        /// <summary>
        /// Details for the journey
        /// </summary>
        public CarJourneyDetail[] Details
        {
            get { return carJourneyDetails; }
            set { carJourneyDetails = value; }
        }
    }
}
