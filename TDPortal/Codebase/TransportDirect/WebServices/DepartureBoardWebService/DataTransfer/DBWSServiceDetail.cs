// *********************************************** 
// NAME             : DBWSServiceDetail.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 29 Nov 2013
// DESCRIPTION  	: /// DBWSServiceDetail class describing the details for a service from a departure board 
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportDirect.WebService.DepartureBoardWebService.DataTransfer
{
    /// <summary>
    /// DBWSServiceDetail class describing the details for a service from a departure board 
    /// </summary>
    [Serializable]
    public class DBWSServiceDetail : DBWSService
    {
        #region Private variables

        private bool isCancelled;
        private string disruptionReason;
        private string overdueMessage;
        private List<string> adhocAlerts;

        private List<List<DBWSLocationCallingPoint>> previousCallingPoints;
        private List<List<DBWSLocationCallingPoint>> subsequentCallingPoints;

        private string timeOfArrivalActual;
        private string timeOfDepartureActual;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSServiceDetail()
            : base()
		{
            previousCallingPoints = new List<List<DBWSLocationCallingPoint>>();
            subsequentCallingPoints = new List<List<DBWSLocationCallingPoint>>();

            adhocAlerts = new List<string>();
		}

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Is Cancelled indicator. 
        /// If "true", the service is cancelled at this location
        /// </summary>
        public bool IsCancelled
        {
            get { return isCancelled; }
            set { isCancelled = value; }
        }

        /// <summary>
        /// Read/Write. Disruption reason for this service. 
        /// If the service is cancelled, this will be a cancellation reason. 
        /// If the service is running late at this location, this will be a late-running reason
        /// </summary>
        public string DisruptionReason
		{
            get { return disruptionReason; }
            set { disruptionReason = value; }
		}

        /// <summary>
        /// Read/Write. Overdue Message for the service.
        /// If an expected movement report has been missed, this will contain a message describing the missed movement
        /// </summary>
        public string OverdueMessage
		{
            get { return overdueMessage; }
            set { overdueMessage = value; }
        }

        /// <summary>
        /// Read/Write. List of active Adhoc Alert texts for that applies at this location. 
        /// </summary>
        public List<string> AdhocAlerts
        {
            get { return adhocAlerts; }
            set { adhocAlerts = value; }
        }

        /// <summary>
        /// Read/Write. Previous Calling Point locations for the service.
        /// A list of lists of location objects representing the previous calling points in the journey. 
        /// A separate calling point list will be present for each origin of the service, 
        /// relative to the current location. The service could have multiple origins if it is
        /// formed from multiple separate trains.
        /// The first list of calling points will be for the "through" train and will 
        /// hold all of the locations from the origin.
        /// The remaining lists will hold the locations of joining trains from their respective 
        /// origins. The point at which the association is made is determined 
        /// by examining the last location in the previousCallingPoints list
        /// </summary>
        public List<List<DBWSLocationCallingPoint>> PreviousCallingPointLocations
        {
            get { return previousCallingPoints; }
            set { previousCallingPoints = value; }
        }

        /// <summary>
        /// Read/Write. Subsequent Calling Point locations for the service.
        /// A list of lists of location objects representing the subsequent calling points in the journey. 
        /// A separate calling point list will be present for each destination of the service, 
        /// relative to the current location. The service could have multiple destinations if it is
        /// split into separate trains.
        /// The first list of calling points will be for the "through" train and will 
        /// hold all of the locations to the destination. 
        /// The remaining lists will hold the locations of splitting trains to their respective 
        /// destinations. The point at which the association is made is determined 
        /// by examining the first location in the subsequentCallingPoints list
        /// </summary>
        public List<List<DBWSLocationCallingPoint>> SubsequentCallingPointLocations
        {
            get { return subsequentCallingPoints; }
            set { subsequentCallingPoints = value; }
        }

        /// <summary>
        /// Read/Write. Actual Time of Arrival of the service at the station board location (optional).
        /// Will only be present if scheduled time is also present and estimate is not present
        /// </summary>
        public string TimeOfArrivalActual
        {
            get { return timeOfArrivalActual; }
            set { timeOfArrivalActual = value; }
        }

        /// <summary>
        /// Read/Write. Actual Time of Departure of the service at the station board location (optional).
        /// Will only be present if scheduled time is also present and estimate is not present
        /// </summary>
        public string TimeOfDepartureActual
        {
            get { return timeOfDepartureActual; }
            set { timeOfDepartureActual = value; }
        }

        #endregion
    }
}