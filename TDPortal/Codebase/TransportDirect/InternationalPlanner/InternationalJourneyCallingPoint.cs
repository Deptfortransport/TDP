// *********************************************** 
// NAME			: InternationalJourneyCallingPoint.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/02/2010
// DESCRIPTION	: Class which defines a calling point for an international journey detail leg. 
//              : This is used to identify the stops a train/coach/flight service calls at before/between/after
//                the journey leg origin and destination
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalJourneyCallingPoint.cs-arc  $
//
//   Rev 1.1   Feb 16 2010 17:44:48   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 12 2010 11:03:08   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Class which defines a calling point for an international journey detail leg. 
    /// </summary>
    [Serializable()]
    public class InternationalJourneyCallingPoint
    {
        #region Private members

        private string stopNaptan;
        private InternationalStop stop;
		private DateTime arrivalDateTime;
		private DateTime departureDateTime;
		private CallingPointType type;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
		public InternationalJourneyCallingPoint()
		{
		}

        /// <summary>
        /// Constructor
        /// </summary>
		public InternationalJourneyCallingPoint(string stopNaptan, 
			DateTime arrivalDateTime, 
			DateTime departureDateTime, 
			CallingPointType type)
		{
			this.stopNaptan = stopNaptan;
			this.arrivalDateTime = arrivalDateTime;
			this.departureDateTime = departureDateTime;
			this.type = type;
		}

        #endregion

        #region Public properties

        /// <summary>
        /// Read/write. The stop naptan of the calling point
        /// </summary>
        public string StopNaptan
        {
            get { return stopNaptan; }
            set { stopNaptan = value; }
        }

        /// <summary>
        /// Read/write. The InternationalStop calling point
        /// </summary>
		public InternationalStop Stop
		{
			get { return stop; }
			set { stop = value; }
		}

        /// <summary>
        /// Read/write. The datetime the service arrives at this calling point
        /// </summary>
		public DateTime ArrivalDateTime
		{
			get { return arrivalDateTime; }
			set { arrivalDateTime = value; }
		}

        /// <summary>
        /// Read/write. The datetime the service departs at this calling point
        /// </summary>
		public DateTime DepartureDateTime
		{
			get { return departureDateTime; }
			set { departureDateTime = value; }
		}

        /// <summary>
        /// Read/write. The type of calling point
        /// </summary>
		public CallingPointType Type
		{
			get { return type; }
			set { type = value; }
		}

        #endregion
	}
    
    /// <summary>
    /// Enum identifying the calling point types
    /// </summary>
    [Serializable()]
	public enum CallingPointType
	{
		Origin,
        OriginAndBoard,
        Board,
		Destination,
        DestinationAndAlight,
        Alight,
		CallingPoint
	}
}
