// *********************************************** 
// NAME             : DBSStopEvent.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: DBSStopEvent class providing Stop/Event-related information
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// DBSStopEvent class providing Stop/Event-related information
    /// </summary>
    [XmlInclude(typeof(TrainStopEvent))] // Define derived types to allow serialisation
    [Serializable]
    public class DBSStopEvent
    {
        private CallingStopStatus cssCallingStopStatus;
        private DBSEvent dbeDeparture;
        private DBSEvent[] dbePreviousIntermediates;
        private DBSEvent dbeStop;
        private DBSEvent[] dbeOnwardIntermediates;
        private DBSEvent dbeArrival;
        private DBSService svService;
        private DepartureBoardType mode;

        public DBSStopEvent()
        {
            cssCallingStopStatus = CallingStopStatus.Unknown;
            dbeDeparture = null;
            dbePreviousIntermediates = new DBSEvent[0];
            dbeStop = null;
            dbeOnwardIntermediates = new DBSEvent[0];
            dbeArrival = null;
        }

        public CallingStopStatus CallingStopStatus
        {
            get { return cssCallingStopStatus; }
            set { cssCallingStopStatus = value; }
        }

        public DBSEvent Departure
        {
            get { return dbeDeparture; }
            set { dbeDeparture = value; }
        }

        public DBSEvent[] PreviousIntermediates
        {
            get { return dbePreviousIntermediates; }
            set { dbePreviousIntermediates = value; }
        }

        public DBSEvent Stop
        {
            get { return dbeStop; }
            set { dbeStop = value; }
        }

        public DBSEvent[] OnwardIntermediates
        {
            get { return dbeOnwardIntermediates; }
            set { dbeOnwardIntermediates = value; }
        }

        public DBSEvent Arrival
        {
            get { return dbeArrival; }
            set { dbeArrival = value; }
        }

        public DBSService Service
        {
            get { return svService; }
            set { svService = value; }
        }

        public DepartureBoardType Mode
        {
            get { return mode; }
            set { mode = value; }
        }
    }
}
