// *********************************************** 
// NAME             : DBWSService.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 29 Nov 2013
// DESCRIPTION  	: DBWSService class describing a departure board service
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace TransportDirect.WebService.DepartureBoardWebService.DataTransfer
{
    /// <summary>
    /// DBWSService class describing a departure board service
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(DBWSServiceDetail))]
    public class DBWSService
    {
        #region Private variables

        private string serviceId;
        private DBWSOperator serviceOperator;

        private List<DBWSLocation> originLocations;
        private List<DBWSLocation> destinationLocations;

        private string timeOfArrivalScheduled;
        private string timeOfArrivalEstimated;

        private string timeOfDepartureScheduled;
        private string timeOfDepartureEstimated;

        private string platform;
        private bool isCircularRoute;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSService()
		{
            originLocations = new List<DBWSLocation>();
            destinationLocations = new List<DBWSLocation>();
		}

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Unique service identifier of this service  relative to the station board on which it is displayed. 
        /// This value can be passed to GetServiceDetails to obtain the full details of the individual service.
        /// </summary>
        public string ServiceId
		{
            get { return serviceId; }
            set { serviceId = value; }
		}

        /// <summary>
        /// Read/Write. Service operator
        /// </summary>
        public DBWSOperator ServiceOperator
		{
            get { return serviceOperator; }
            set { serviceOperator = value; }
        }

        /// <summary>
        /// Read/Write. Origin locations (original or live/current) for the service.
        /// Note that a service may have more than one origin, if the service comprises of multiple trains that 
        /// join at a previous location in the schedule
        /// </summary>
        public List<DBWSLocation> OriginLocations
        {
            get { return originLocations; }
            set { originLocations = value; }
        }

        /// <summary>
        /// Read/Write. Destination locations (original or live/current) for the service.
        /// Note that a service may have more than one destination, if the service comprises of multiple trains that 
        /// divide at a subsequent location in the schedule
        /// </summary>
        public List<DBWSLocation> DestinationLocations
        {
            get { return destinationLocations; }
            set { destinationLocations = value; }
        }

        /// <summary>
        /// Read/Write. Scheduled Time of Arrival of the service at the station board location (optional)
        /// Value can be absolute times formatted as a HH:MM string, 
        /// or a text string such as (but not limited to) "On time", "No report" or "Cancelled"
        /// Time should be output in the user interface exactly as supplied. 
        /// Time value may have an asterisk ("*") appended to indicate that the value is "uncertain".
        /// </summary>
        public string TimeOfArrivalScheduled
        {
            get { return timeOfArrivalScheduled; }
            set { timeOfArrivalScheduled = value; }
        }

        /// <summary>
        /// Read/Write. Estimated Time of Arrival of the service at the station board location (optional)
        /// </summary>
        public string TimeOfArrivalEstimated
        {
            get { return timeOfArrivalEstimated; }
            set { timeOfArrivalEstimated = value; }
        }

        /// <summary>
        /// Read/Write. Scheduled Time of Departure of the service at the station board location (optional)
        /// Value can be absolute times formatted as a HH:MM string, 
        /// or a text string such as (but not limited to) "On time", "No report" or "Cancelled"
        /// Time should be output in the user interface exactly as supplied. 
        /// Time value may have an asterisk ("*") appended to indicate that the value is "uncertain".
        /// </summary>
        public string TimeOfDepartureScheduled
        {
            get { return timeOfDepartureScheduled; }
            set { timeOfDepartureScheduled = value; }
        }

        /// <summary>
        /// Read/Write. Estimated Time of Departure of the service at the station board location (optional)
        /// </summary>
        public string TimeOfDepartureEstimated
        {
            get { return timeOfDepartureEstimated; }
            set { timeOfDepartureEstimated = value; }
        }

        /// <summary>
        /// Read/Write. Platform number for the service at this location (optional)
        /// </summary>
        public string Platform
        {
            get { return platform; }
            set { platform = value; }
        }

        /// <summary>
        /// Read/Write. If "true" then the service is operating on a circular route through the network 
        /// and will call again at this location later on its journey. 
        /// The user interface should indicate this fact to the user, to help them choose the 
        /// correct service from a set of similar alternatives.
        /// </summary>
        public bool IsCircularRoute
        {
            get { return isCircularRoute; }
            set { isCircularRoute = value; }
        }

        #endregion
    }
}