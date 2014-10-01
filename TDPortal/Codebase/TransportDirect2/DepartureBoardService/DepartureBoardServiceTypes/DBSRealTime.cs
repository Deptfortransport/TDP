// *********************************************** 
// NAME             : DBSRealTime.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: DBSRealTime class providing real time information
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
    /// DBSRealTime class providing real time information
    /// </summary>
    [XmlInclude(typeof(TrainRealTime))] // Define derived types to allow serialisation
    [Serializable]
    public class DBSRealTime
    {
        private DateTime dtDepartTime;
        private DBSRealTimeType rttDepartTimeType;
        private DateTime dtArriveTime;
        private DBSRealTimeType rttArriveTimeType;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DBSRealTime()
        {
            dtDepartTime = DateTime.MinValue;
            dtArriveTime = DateTime.MinValue;
            rttDepartTimeType = DBSRealTimeType.Estimated;
            rttArriveTimeType = DBSRealTimeType.Estimated;
        }

        public DateTime DepartTime
        {
            get { return dtDepartTime; }
            set { dtDepartTime = value; }
        }

        public DBSRealTimeType DepartTimeType
        {
            get { return rttDepartTimeType; }
            set { rttDepartTimeType = value; }
        }

        public DateTime ArriveTime
        {
            get { return dtArriveTime; }
            set { dtArriveTime = value; }
        }

        public DBSRealTimeType ArriveTimeType
        {
            get { return rttArriveTimeType; }
            set { rttArriveTimeType = value; }
        }

    }
}
