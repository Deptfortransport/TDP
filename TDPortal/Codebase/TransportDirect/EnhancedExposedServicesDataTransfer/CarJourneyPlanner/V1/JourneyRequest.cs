// *********************************************** 
// NAME                 : JourneyRequest.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class to hold details for a Journey request to be used in a CarJourneyRequest
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/JourneyRequest.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:16   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold details for a Journey request to be used in a CarJourneyRequest
    /// </summary>
    [System.Serializable]
    public class JourneyRequest
    {
        private int journeyRequestId;
        private RequestLocation originLocation;
        private RequestLocation destinationLocation;
        private RequestLocation viaLocation;
        private DateTime outwardDateTime;
        private DateTime returnDateTime;
        private bool outwardArriveBefore;
        private bool returnArriveBefore;
        private bool isReturnRequired;

        // Properties to allow user to optionally include above properties in the request
        private bool viaLocationSpecified;
        private bool returnDateTimeSpecified;
        private bool outwardArriveBeforeSpecified;
        private bool returnArriveBeforeSpecified;

        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyRequest()
        {
        }

        #region Propreties

        /// <summary>
        /// The journey request Id to differentiate between multiple journeys in the Car Journey Request
        /// </summary>
        public int JourneyRequestId
        {
            get { return journeyRequestId; }
            set { journeyRequestId = value; }
        }

        /// <summary>
        /// Specifies the origin of the outward journey.
        /// </summary>
        public RequestLocation OriginLocation
        {
            get { return originLocation; }
            set { originLocation = value; }
        }

        /// <summary>
        /// Specifies the destination of the outward journey.
        /// </summary>
        public RequestLocation DestinationLocation
        {
            get { return destinationLocation; }
            set { destinationLocation = value; }
        }

        /// <summary>
        /// Specifies a via location to use in the journey.
        /// </summary>
        public RequestLocation ViaLocation
        {
            get { return viaLocation; }
            set { viaLocation = value; }
        }

        /// <summary>
        /// The time the outward journey should leave at or arrive by.
        /// </summary>
        public DateTime OutwardDateTime
        {
            get { return outwardDateTime; }
            set { outwardDateTime = value; }
        }

        /// <summary>
        /// The time the return journey should leave at or arrive by. 
        /// Ignored if IsReturnRequired is false.
        /// </summary>
        public DateTime ReturnDateTime
        {
            get { return returnDateTime; }
            set { returnDateTime = value; }
        }

        /// <summary>
        /// Specifies whether the outward journey should arrive on or leave on the specified time. 
        /// If true, journeys will be planned to arrive on or before the specified time. If false, 
        /// journeys will be planned to leave on or after the specified time.
        /// </summary>
        public bool OutwardArriveBefore
        {
            get { return outwardArriveBefore; }
            set { outwardArriveBefore = value; }
        }

        /// <summary>
        /// Specifies whether the return journey should arrive on or leave on the specified time. 
        /// If true, journeys will be planned to arrive on or before the specified time. If false, 
        /// journeys will be planned to leave on or after the specified time.
        /// Ignored if IsReturnRequired is false
        /// </summary>
        public bool ReturnArriveBefore
        {
            get { return returnArriveBefore; }
            set { returnArriveBefore = value; }
        }

        /// <summary>
        /// Specifies whether a return journey should be planned. True if required, false otherwise.
        /// </summary>
        public bool IsReturnRequired
        {
            get { return isReturnRequired; }
            set { isReturnRequired = value; }
        }

        #endregion

        #region Properties Internal (optional)

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ViaLocationSpecified
        {
            get { return viaLocationSpecified; }
            set { viaLocationSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReturnDateTimeSpecified
        {
            get { return returnDateTimeSpecified; }
            set { returnDateTimeSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OutwardArriveBeforeSpecified
        {
            get { return outwardArriveBeforeSpecified; }
            set { outwardArriveBeforeSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReturnArriveBeforeSpecified
        {
            get { return returnArriveBeforeSpecified; }
            set { returnArriveBeforeSpecified = value; }
        }

        #endregion
    }
}
