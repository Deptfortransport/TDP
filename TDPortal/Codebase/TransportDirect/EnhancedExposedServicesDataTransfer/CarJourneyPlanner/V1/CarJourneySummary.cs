// *********************************************** 
// NAME                 : CarJourneySummary.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class for CarJourneySummary returned in the CarJourneyResult
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarJourneySummary.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:12   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Class for CarJourneySummary returned in the CarJourneyResult
    /// </summary>
    [System.Serializable]
    public class CarJourneySummary
    {
        private ResponseLocation originLocation;
        private ResponseLocation destinationLocation;
        private ResponseLocation viaLocation;
        private DateTime departureDateTime;
        private DateTime arrivalDateTime;
        private Common.V1.TimeSpan duration;
        private double distance;
        private DistanceUnit distanceUnit;
        private CarCost[] costs;
        private Emissions[] emissions;
        private CarParameters carParameters;
        private string summaryDirections;
                
        /// <summary>
        /// Constructor
        /// </summary>
        public CarJourneySummary()
        {
        }

        #region Properties

        /// <summary>
        /// Specifies the origin of the journey.
        /// </summary>
        public ResponseLocation OriginLocation
        {
            get { return originLocation; }
            set { originLocation = value; }
        }

        /// <summary>
        /// Specifies the destination of the journey.
        /// </summary>
        public ResponseLocation DestinationLocation
        {
            get { return destinationLocation; }
            set { destinationLocation = value; }
        }

        /// <summary>
        /// Specifies a via location used in the journey.
        /// </summary>
        public ResponseLocation ViaLocation
        {
            get { return viaLocation; }
            set { viaLocation = value; }
        }

        /// <summary>
        /// Journey departure time.
        /// </summary>
        public DateTime DepartureDateTime
        {
            get { return departureDateTime; }
            set { departureDateTime = value; }
        }

        /// <summary>
        /// Journey arrival time.
        /// </summary>
        public DateTime ArrivalDateTime
        {
            get { return arrivalDateTime; }
            set { arrivalDateTime = value; }
        }

        /// <summary>
        /// Journey duration time.
        /// </summary>
        public Common.V1.TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        /// <summary>
        /// Journey distance. Unit will be as detailed in the DistanceUnit property
        /// </summary>
        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        /// <summary>
        /// The distance units the journey is in, e.g. Miles or Kms
        /// </summary>
        public DistanceUnit DistanceUnit
        {
            get { return distanceUnit; }
            set { distanceUnit = value; }
        }

        /// <summary>
        /// The costs associated with the car journey
        /// </summary>
        public CarCost[] Costs
        {
            get { return costs; }
            set { costs = value; }
        }

        /// <summary>
        /// The CO2 emissions for the car journey
        /// </summary>
        public Emissions[] Emissions
        {
            get { return emissions; }
            set { emissions = value; }
        }

        /// <summary>
        /// The car parameters used for the car journey
        /// </summary>
        public CarParameters CarParameters
        {
            get { return carParameters; }
            set { carParameters = value; }
        }

        /// <summary>
        /// The summary of directions for the car journey
        /// </summary>
        public string SummaryDirections
        {
            get { return summaryDirections; }
            set { summaryDirections = value; }
        }
        #endregion
    }
}
