// *********************************************** 
// NAME             : DBSEvent.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: DBSEvent class providing an event information
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// DBSEvent class providing an event information
    /// </summary>
    [Serializable]
    public class DBSEvent
    {
        private DBSActivityType atActivityType;
        private DateTime dtDepartTime;
        private DateTime dtArriveTime;
        private DBSRealTime rtRealTime;
        private DBSStop stStop;
        private bool cancelled = false;

        public DBSEvent()
        {
            atActivityType = DBSActivityType.Depart;
            dtDepartTime = DateTime.MinValue;
            dtArriveTime = DateTime.MinValue;
            rtRealTime = null;
            stStop = null;
        }

        public DBSActivityType ActivityType
        {
            get { return atActivityType; }
            set { atActivityType = value; }
        }

        public DateTime DepartTime
        {
            get { return dtDepartTime; }
            set { dtDepartTime = value; }
        }

        public DateTime ArriveTime
        {
            get { return dtArriveTime; }
            set { dtArriveTime = value; }
        }

        public DBSRealTime RealTime
        {
            get { return rtRealTime; }
            set { rtRealTime = value; }
        }

        public DBSStop Stop
        {
            get { return stStop; }
            set { stStop = value; }
        }

        public bool Cancelled
        {
            get { return cancelled; }
            set { cancelled = value; }
        }
    }
}
