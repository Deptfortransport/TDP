// *********************************************** 
// NAME                 : TimeSpan.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Simple TimeSpan class which can be serialised
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/Common/V1/TimeSpan.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:39:24   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1
{
    /// <summary>
    /// Simple TimeSpan class which can be serialised
    /// </summary>
    [System.Serializable]
    public class TimeSpan
    {
        private int days;
        private int hours;
        private int minutes;
        private int seconds;

        /// <summary>
        /// Constructor
        /// </summary>
        public TimeSpan()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="days"></param>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        public TimeSpan(int days, int hours, int minutes, int seconds)
        {
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }

        /// <summary>
        /// Gets the number of whole Days
        /// </summary>
        public int Days
        {
            get { return days; }
            set { days = value; }
        }

        /// <summary>
        /// Gets the number of whole Hours
        /// </summary>
        public int Hours
        {
            get { return hours; }
            set { hours = value; }
        }

        /// <summary>
        /// Gets the number of whole Minutes
        /// </summary>
        public int Minutes
        {
            get { return minutes; }
            set { minutes = value; }
        }

        /// <summary>
        /// Gets the number of whole Seconds
        /// </summary>
        public int Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }
    }
}
