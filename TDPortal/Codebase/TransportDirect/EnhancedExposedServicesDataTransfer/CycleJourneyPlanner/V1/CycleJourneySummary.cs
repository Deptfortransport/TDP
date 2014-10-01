// *********************************************** 
// NAME             : CycleJourneySummary.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class to hold summary information for a cycle journey
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleJourneySummary.cs-arc  $
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
    /// Class to hold summary information for a cycle journey
    /// </summary>
    [Serializable]
    public class CycleJourneySummary
    {
        #region Private Fields
        private ResponseLocation originLocation;
        private ResponseLocation destinationLocation;
        private ResponseLocation viaLocation;
        private DateTime departureDateTime;
        private DateTime arrivalDateTime;
        private Common.V1.TimeSpan duration;
        private double distance;
        private DistanceUnit distanceUnit;
        private CycleCost[] costs;
        private string summaryDirections;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CycleJourneySummary() { }
        #endregion

        #region Public Properties
        /// <summary>
        /// Specifies the origin location of the journey
        /// </summary>
        public ResponseLocation OriginLocation
        {
            get { return originLocation; }
            set { originLocation = value; }
        }

        /// <summary>
        /// Specifies the destination location of the journey
        /// </summary>
        public ResponseLocation DestinationLocation
        {
            get { return destinationLocation; }
            set { destinationLocation = value; }
        }

        /// <summary>
        /// Specifies the via location of the journey
        /// </summary>
        public ResponseLocation ViaLocation
        {
            get { return viaLocation; }
            set { viaLocation = value; }
        }

        /// <summary>
        /// Journey departure time
        /// </summary>
        public DateTime DepartureDateTime
        {
            get { return departureDateTime; }
            set { departureDateTime = value; }
        }

        /// <summary>
        /// Journey arrival time
        /// </summary>
        public DateTime ArrivalDateTime
        {
            get { return arrivalDateTime; }
            set { arrivalDateTime = value; }
        }

        /// <summary>
        /// Duration of the journey as timespan value
        /// </summary>
        public Common.V1.TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        /// <summary>
        /// Journey distance.
        /// Unit will be as detailed in DistanceUnit property
        /// <see>DistanceUnit</see>
        /// </summary>
        public double Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        /// <summary>
        /// The distance units the journey is in e.g. Miles of Kms
        /// </summary>
        public DistanceUnit DistanceUnit
        {
            get { return distanceUnit; }
            set { distanceUnit = value; }
        }

        /// <summary>
        /// The costs associated with the cycle journey, e.g. any toll charges
        /// </summary>
        public CycleCost[] Costs
        {
            get { return costs; }
            set { costs = value; }
        }

        /// <summary>
        /// Specifies the directions as a list or roads used in the journey
        /// e.g. A553; A41; A59;
        /// </summary>
        public string SummaryDirections
        {
            get { return summaryDirections; }
            set { summaryDirections = value; }
        }
        #endregion
    }
}
