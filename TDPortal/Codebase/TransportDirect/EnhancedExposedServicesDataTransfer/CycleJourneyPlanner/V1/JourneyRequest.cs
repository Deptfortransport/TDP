// *********************************************** 
// NAME             : JourneyRequest.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Class to hold details for a Journey request to be used in a CycleJourneyRequest
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/JourneyRequest.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:52   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold details for a Journey request to be used in a CycleJourneyRequest
    /// </summary>
    [Serializable]
    public class JourneyRequest
    {
        #region Private Fields
        private int journeyRequestId;
        private RequestLocation originLocation;
        private RequestLocation destinationLocation;
        private RequestLocation viaLocation;
        private DateTime outwardDateTime;
        private bool outwardArriveBefore;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public JourneyRequest()
        {

        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Specifies the ID for this journey
        /// </summary>
        public int JourneyRequestId
        {
            get { return journeyRequestId; }
            set { journeyRequestId = value; }
        }

        /// <summary>
        /// Specifies the origin location of the journey
        /// </summary>
        public RequestLocation OriginLocation
        {
            get { return originLocation; }
            set { originLocation = value; }
        }

        /// <summary>
        /// Specifies the destination location of the journey
        /// </summary>
        public RequestLocation DestinationLocation
        {
            get { return destinationLocation; }
            set { destinationLocation = value; }
        }

        /// <summary>
        /// Specifies the via location for the journey
        /// </summary>
        public RequestLocation ViaLocation
        {
            get { return viaLocation; }
            set { viaLocation = value; }
        }

        /// <summary>
        /// The time the journey should leave at or arrive by
        /// </summary>
        public DateTime OutwardDateTime
        {
            get { return outwardDateTime; }
            set { outwardDateTime = value; }
        }

        /// <summary>
        /// Specifies whether the outward journey should arrive on or leave on the specified time.
        /// If true, journeys will be planned to arrive on or before the specified time.
        /// If false, journeys will be planned to leave on or after the specified time.
        /// </summary>
        public bool OutwardArriveBefore
        {
            get { return outwardArriveBefore; }
            set { outwardArriveBefore = value; }
        }
        #endregion
    }
}
