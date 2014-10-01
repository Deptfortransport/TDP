// *********************************************** 
// NAME			: InternationalJourneyDetail.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which defines an International journey detail - used to represent a leg in an 
//                international journey
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalJourneyDetail.cs-arc  $
//
//   Rev 1.5   Feb 24 2010 14:48:08   mmodi
//Populate the Service Features property of the detail
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Feb 19 2010 10:37:24   rbroddle
//Added Distance property
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 12 2010 09:40:32   mmodi
//Updated to plan train journeys and save journeys to cache
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 09 2010 09:53:16   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 04 2010 10:26:02   mmodi
//Updates as part of development
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:32   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Class which defines an International journey detail - used to represent a leg in an 
    /// international journey
    /// </summary>
    [Serializable()]
    public class InternationalJourneyDetail
    {
        #region Private members

        private InternationalJourneyDetailType detailType;

        private string departureName;
        private string arrivalName;
        private string departureStopNaptan;
        private string arrivalStopNaptan;
        private InternationalStop departureStop;    // Used for a timed journey detail leg
        private InternationalStop arrivalStop;      // Used for a timed journey detail leg
        private InternationalCity departureCity;    // Used for a transfer from journey detail leg
        private InternationalCity arrivalCity;      // Used for a transfer to journey detail leg
        private DateTime departureDateTime;
        private DateTime arrivalDateTime;
        private double durationMinutes;

        private InternationalJourneyCallingPoint[] intermediatesBefore;	// between origin & detail leg start (excl)
        private InternationalJourneyCallingPoint[] intermediatesLeg;    // between detail leg start & detail leg end (excl)
        private InternationalJourneyCallingPoint[] intermediatesAfter;	// between detail leg end & destination (excl)
        
        private string operatorCode;
        private string serviceNumber;   // Train number, Flight number, ...
        private int[] serviceFacilities;

        private string aircraftTypeCode;
        private string terminalNumberFrom;
        private string terminalNumberTo;

        private string transferInfo;    // Used to specify any transfer information if this is a Transfer detail

        private DateTime checkInDateTime;
        private DateTime checkOutDateTime;

        private int distance = -1; //for CO2 calculations

        #endregion

        #region Constructor
		
        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalJourneyDetail()
        {
            // Default values
            departureName = string.Empty;
            arrivalName = string.Empty;
            departureStopNaptan = string.Empty;
            arrivalStopNaptan = string.Empty;

            departureStop = null;
            arrivalStop = null;
            departureCity = null;
            arrivalCity = null;

            departureDateTime = DateTime.MinValue;
            arrivalDateTime = DateTime.MinValue;
            durationMinutes = 0;

            intermediatesBefore = new InternationalJourneyCallingPoint[0];
            intermediatesLeg = new InternationalJourneyCallingPoint[0];
            intermediatesAfter = new InternationalJourneyCallingPoint[0];

            operatorCode = string.Empty;
            serviceNumber = string.Empty;
            serviceFacilities = new int[0];

            aircraftTypeCode = string.Empty;
            terminalNumberFrom = string.Empty;
            terminalNumberTo = string.Empty;

            transferInfo = string.Empty;

            checkInDateTime = DateTime.MinValue;
            checkOutDateTime = DateTime.MinValue;
        }

		#endregion

        #region Public properties

        /// <summary>
        /// Read/Write. The International journey detail type for this journey detail leg
        /// </summary>
        public InternationalJourneyDetailType DetailType
        {
            get { return detailType; }
            set { detailType = value; }
        }

        /// <summary>
        /// Read/Write. The Departure Name for the journey detail leg
        /// </summary>
        public string DepartureName
        {
            get { return departureName; }
            set { departureName = value; }
        }

        /// <summary>
        /// Read/Write. The Arrival Name for the journey detail leg
        /// </summary>
        public string ArrivalName
        {
            get { return arrivalName; }
            set { arrivalName = value; }
        }

        /// <summary>
        /// Read/Write. The Departure stop naptan for the journey detail leg
        /// </summary>
        public string DepartureStopNaptan
        {
            get { return departureStopNaptan.Trim().ToUpper(); }
            set { departureStopNaptan = value; }
        }

        /// <summary>
        /// Read/Write. The Departure international stop for the journey detail leg
        /// Used for a timed journey detail leg. This value can be null
        /// </summary>
        public InternationalStop DepartureStop
        {
            get { return departureStop; }
            set { departureStop = value; }
        }

        /// <summary>
        /// Read/Write. The Arrival stop naptan for the journey detail leg
        /// </summary>
        public string ArrivalStopNaptan
        {
            get { return arrivalStopNaptan.Trim().ToUpper(); }
            set { arrivalStopNaptan = value; }
        }

        /// <summary>
        /// Read/Write. The Arrival international stop for the journey detail leg.
        /// Used for a timed journey detail leg. This value can be null
        /// </summary>
        public InternationalStop ArrivalStop
        {
            get { return arrivalStop; }
            set { arrivalStop = value; }
        }

        /// <summary>
        /// Read/Write. The Departure international city for the journey detail leg.
        /// Used for a transfer from city journey detail leg. This value can be null
        /// </summary>
        public InternationalCity DepartureCity
        {
            get { return departureCity; }
            set { departureCity = value; }
        }

        /// <summary>
        /// Read/Write. The Arrival international city for the journey detail leg.
        /// Used for a transfer to city journey detail leg. This value can be null
        /// </summary>
        public InternationalCity ArrivalCity
        {
            get { return arrivalCity; }
            set { arrivalCity = value; }
        }

        /// <summary>
        /// Read/Write. The Departure date time for the journey detail leg
        /// </summary>
        public DateTime DepartureDateTime
        {
            get { return departureDateTime; }
            set { departureDateTime = value; }
        }

        /// <summary>
        /// Read/Write. The Arrival date time for the journey detail leg
        /// </summary>
        public DateTime ArrivalDateTime
        {
            get { return arrivalDateTime; }
            set { arrivalDateTime = value; }
        }

        /// <summary>
        /// Read/Write. The duration in minutes for the journey, 
        /// excluding check-in/out times
        /// </summary>
        public double DurationMinutes
        {
            get { return durationMinutes; }
            set { durationMinutes = value; }
        }

        /// <summary>
        /// Read/Write. The points service calls at before this detail leg, (will include the service 
        /// Origin calling point).
        /// </summary>
        public InternationalJourneyCallingPoint[] IntermediatesBefore
        {
            get { return intermediatesBefore; }
            set { intermediatesBefore = value; }
        }

        /// <summary>
        /// Read/Write. The points service calls at between the depart and arriva stop within this detail leg.
        /// </summary>
        public InternationalJourneyCallingPoint[] IntermediatesLeg
        {
            get { return intermediatesLeg; }
            set { intermediatesLeg = value; }
        }

        /// <summary>
        /// Read/Write. The points service calls at after this detail leg, (will include the service 
        /// Destination calling point).
        /// </summary>
        public InternationalJourneyCallingPoint[] IntermediatesAfter
        {
            get { return intermediatesAfter; }
            set { intermediatesAfter = value; }
        }

        /// <summary>
        /// Read/Write. The Operator/Carrier code for the journey detail leg
        /// </summary>
        public string OperatorCode
        {
            get { return operatorCode; }
            set { operatorCode = value; }
        }

        /// <summary>
        /// Read/Write. The Service number (e.g. Flight number, Train number) for the journey detail leg
        /// </summary>
        public string ServiceNumber
        {
            get { return serviceNumber; }
            set { serviceNumber = value; }
        }

        /// <summary>
        /// Read/Write. The Service facilities which apply to the service in the journey detail leg
        /// </summary>
        public int[] ServiceFacilities
        {
            get { return serviceFacilities; }
            set { serviceFacilities = value; }
        }

        /// <summary>
        /// Read/Write. The Aircraft Type code for the journey detail leg
        /// </summary>
        public string AircraftTypeCode
        {
            get { return aircraftTypeCode; }
            set { aircraftTypeCode = value; }
        }

        /// <summary>
        /// Read/Write. The Terminal number the journey detail leg departs from
        /// </summary>
        public string TerminalNumberFrom
        {
            get { return terminalNumberFrom; }
            set { terminalNumberFrom = value; }
        }

        /// <summary>
        /// Read/Write. The Terminal number the journey detail leg arrives from
        /// </summary>
        public string TerminalNumberTo
        {
            get { return terminalNumberTo; }
            set { terminalNumberTo = value; }
        }

        /// <summary>
        /// Read/Write. The Transfer information if this detail is of Type Transfer
        /// </summary>
        public string TransferInfo
        {
            get { return transferInfo; }
            set { transferInfo = value; }
        }

        /// <summary>
        /// Read/Write. The Check In date time for the journey detail leg
        /// </summary>
        public DateTime CheckInDateTime
        {
            get { return checkInDateTime; }
            set { checkInDateTime = value; }
        }

        /// <summary>
        /// Read/Write. The Check Out date time for the journey detail leg
        /// </summary>
        public DateTime CheckOutDateTime
        {
            get { return checkOutDateTime; }
            set { checkOutDateTime = value; }
        }

        /// <summary>
        /// Read/Write. The distance (in metres) for the leg.
        /// </summary>
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        #endregion
    }
}
