// *********************************************** 
// NAME             : TrainStopEvent.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: TrainStopEvent class providing Train Stop/Event-related information
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// TrainStopEvent class providing Train Stop/Event-related information
    /// </summary>
    [Serializable]
    public class TrainStopEvent : DBSStopEvent
    {
        private bool isCircularRoute;
        private string sFalseDestination;
        private string sVia;
        private bool isCancelled;
        private string sCancellationReason;
        private string sLateReason;
        private string platform;

        public TrainStopEvent()
            : base()
        {
            isCircularRoute = false;
            sFalseDestination = string.Empty;
            sVia = string.Empty;
            isCancelled = false;
            sCancellationReason = string.Empty;
            sLateReason = string.Empty;
            platform = string.Empty;
        }

        public bool CircularRoute
        {
            get { return isCircularRoute; }
            set { isCircularRoute = value; }
        }

        public string FalseDestination
        {
            get { return sFalseDestination; }
            set { sFalseDestination = value; }
        }

        public string Via
        {
            get { return sVia; }
            set { sVia = value; }
        }

        public bool Cancelled
        {
            get { return isCancelled; }
            set { isCancelled = value; }
        }

        public string CancellationReason
        {
            get { return sCancellationReason; }
            set { sCancellationReason = value; }
        }

        public string LateReason
        {
            get { return sLateReason; }
            set { sLateReason = value; }
        }

        public string Platform
        {
            get { return platform; }
            set { platform = value; }
        }
    }
}