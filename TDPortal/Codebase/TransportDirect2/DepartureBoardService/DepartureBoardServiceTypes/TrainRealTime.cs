// *********************************************** 
// NAME             : TrainRealTime.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: TrainRealTime class providing train real time information
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// TrainRealTime class providing train real time information
    /// </summary>
    [Serializable]
    public class TrainRealTime : DBSRealTime
    {
        private bool isDelayed;
        private bool isUncertain;

        public TrainRealTime()
            : base()
        {
            isDelayed = false;
            isUncertain = false;
        }

        public bool Delayed
        {
            get { return isDelayed; }
            set { isDelayed = value; }
        }

        public bool Uncertain
        {
            get { return isUncertain; }
            set { isUncertain = value; }
        }
    }
}