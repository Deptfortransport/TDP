// *********************************************** 
// NAME             : DBSResult.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: DepartureBoard result information
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// DepartureBoard result information
    /// </summary>
    [Serializable]
    public class DBSResult
    {
        private DateTime generatedAt;
        private DBSStopEvent[] seStopEvents;
        private DBSMessage[] dbmMessages;

        public DBSResult()
        {
            generatedAt = DateTime.MinValue;
            seStopEvents = new DBSStopEvent[0];
            dbmMessages = new DBSMessage[0];
        }

        public DateTime GeneratedAt
        {
            get { return generatedAt; }
            set { generatedAt = value; }
        }

        public DBSStopEvent[] StopEvents
        {
            get { return seStopEvents; }
            set { seStopEvents = value; }
        }

        public DBSMessage[] Messages
        {
            get { return dbmMessages; }
            set { dbmMessages = value; }
        }
    }
}
