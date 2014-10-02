// *********************************************** 
// NAME             : DBWSRequest.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: DBWSRequest class
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportDirect.WebService.DepartureBoardWebService.DataTransfer
{
    /// <summary>
    /// DBWSRequest class
    /// </summary>
    [Serializable]
    public class DBWSRequest
    {
        #region Private variables

        private string requestId = string.Empty;

        private DBWSLocation location = null;
        
        private DBWSLocation locationFilter = null;
        private DBWSFilterType locationFilterType = DBWSFilterType.ServicesTo;

        private bool showDepartures = true;
        private bool showArrivals = false;

        private int numberOfRows = 0;
        private int durationMins = 0;
        private int timeOffsetMins = 0;
        private DateTime requestedTime = DateTime.MinValue;

        private string serviceId = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSRequest()
        {
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. RequestId
        /// </summary>
        public string RequestId 
        { 
            get { return requestId;}
            set { requestId = value; } 
        }

        /// <summary>
        /// Read/Write. Location to fetch the station board for
        /// </summary>
        public DBWSLocation Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Read/Write. Location to filter the station board by
        /// </summary>
        public DBWSLocation LocationFilter
        {
            get { return locationFilter; }
            set { locationFilter = value; }
        }

        /// <summary>
        /// Read/Write. Filter direction to apply the filter location to the station board
        /// </summary>
        public DBWSFilterType LocationFilterType
        {
            get { return locationFilterType; }
            set { locationFilterType = value; }
        }

        /// <summary>
        /// Read/Write. ShowDepartures - include departure times for station board location
        /// </summary>
        public bool ShowDepartures
        {
            get { return showDepartures; }
            set { showDepartures = value; }
        }

        /// <summary>
        /// Read/Write. ShowArrivals - include arrival times for station board location
        /// </summary>
        public bool ShowArrivals
        {
            get { return showArrivals; }
            set { showArrivals = value; }
        }

        /// <summary>
        /// Read/Write. Maximum number of rows to include in the station board result
        /// </summary>
        public int NumberOfRows
        {
            get { return numberOfRows; }
            set { numberOfRows = value; }
        }

        /// <summary>
        /// Read/Write. Duration (1 to 120 minutes) time window filter (from "now") for the 
        /// services departing/arriving for the station board
        /// </summary>
        public int Duration
        {
            get { return durationMins; }
            set { durationMins = value; }
        }

        /// <summary>
        /// Read/Write. Time Offset from requested time to show 
        /// services departing/arriving for the station board
        /// </summary>
        public int TimeOffset
        {
            get { return timeOffsetMins; }
            set { timeOffsetMins = value; }
        }

        /// <summary>
        /// Read/Write. Date time (default is Now) for which to show 
        /// services departing/arriving for the station board
        /// </summary>
        public DateTime RequestedTime
        {
            get { return requestedTime; }
            set { requestedTime = value; }
        }

        /// <summary>
        /// Read/Write. ServiceId of a service from a station board result.
        /// Service details are only available while the service appears on the station board 
        /// from which it was obtained. This is normally for two minutes after it is expected 
        /// to have departed, or after a terminal arrival. 
        /// If a request is made for a service that is no longer available then the result will include a message
        /// </summary>
        public string ServiceId
        {
            get { return serviceId; }
            set { serviceId = value; }
        }

        #endregion
    }
}