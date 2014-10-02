// *********************************************** 
// NAME                 : RealTimeRoadEvent.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 31/08/2011 
// DESCRIPTION  : Defines a custom event for logging real time car event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RealTimeRoadEvent.cs-arc  $
//
//   Rev 1.1   Sep 12 2011 15:12:52   apatel
//Updated to add Serializable attribute to the class
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.0   Sep 02 2011 10:34:52   apatel
//Initial revision.
//Resolution for 5731: CCN 0548 - Real Time Information in Car

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// Defines a custom event for logging real time car event data.
    /// </summary>
    [Serializable]
    public class RealTimeRoadEvent : TDPCustomEvent
    {
        #region Private Fields
        private static RealTimeRoadEventFileFormatter fileFormatter = new RealTimeRoadEventFileFormatter();

        private RealTimeRoadEventType realTimeRoadEventType = RealTimeRoadEventType.RealTimeRoadJourneyTravelNewsMatching;
        private bool realTimeFound;
        private DateTime submitted;
        private bool success;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the RealTimeRoadEvent to log events
        /// </summary>
        /// <param name="sessionIdOld"></param>
        /// <param name="sessionIdNew"></param>
        /// <param name="lastVisitedDateTime"></param>
        /// <param name="domain"></param>
        /// <param name="userAgent"></param>
        public RealTimeRoadEvent(RealTimeRoadEventType realTimeRoadEventType, DateTime submitted, bool realTimeFound, string sessionId, bool success)
            : base(sessionId, false)
        {
            this.realTimeRoadEventType = realTimeRoadEventType;
            this.submitted = submitted;
            this.realTimeFound = realTimeFound;
            this.success = success;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the RealTimeRoadEventType
        /// </summary>
        public RealTimeRoadEventType RealTimeRoadTypeOfEvent
        {
            get { return realTimeRoadEventType; }
        }

        /// <summary>
        /// Gets the date/time at which the transaction was submitted.
        /// </summary>
        public DateTime Submitted
        {
            get { return submitted; }
        }

        /// <summary>
        /// Indicates if real time incidents were found.
        /// Should only be used with RealTimeRoadEventType of RealTimeRoadJourneyTravelNewsMatching
        /// </summary>
        public bool RealTimeFound
        {
            get { return realTimeFound; }
        }

        /// <summary>
        /// Gets the success flag.
        /// </summary>
        public bool Success
        {
            get { return success; }
        }
         
        /// <summary>
        /// Provides an event formatter for publishing to files.
        /// </summary>
        override public IEventFormatter FileFormatter
        {
            get { return fileFormatter; }
        }

        #endregion
    }
}
