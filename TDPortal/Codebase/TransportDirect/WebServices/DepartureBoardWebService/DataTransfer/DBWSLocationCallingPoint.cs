// *********************************************** 
// NAME             : DBWSLocationCallingPoint.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 29 Nov 2013
// DESCRIPTION  	: DBWSLocationCallingPoint class for a location used in a service detail from a departure board service
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportDirect.WebService.DepartureBoardWebService.DataTransfer
{
    /// <summary>
    /// DBWSLocationCallingPoint class for a location used in a service detail from a departure board service
    /// </summary>
    [Serializable]
    public class DBWSLocationCallingPoint : DBWSLocation
    {
        #region Private variables

        private string timeScheduled;
        private string timeEstimated;
        private string timeActual;

        private List<string> adhocAlerts;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSLocationCallingPoint()
            : base()
		{
            adhocAlerts = new List<string>();
		}

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Scheduled time of the service at this location. 
        /// The time will be either an arrival or departure time, depending on whether it is
        /// in the subsequent or previous calling point list.
        /// </summary>
        public string TimeScheduled
		{
            get { return timeScheduled; }
            set { timeScheduled = value; }
		}

        /// <summary>
        /// Read/Write. Estimated time of the service at this location. 
        /// The time will be either an arrival or departure time, depending on whether it is 
        /// in the subsequent or previous calling point list. 
        /// Will only be present if an actual time is not present
        /// </summary>
        public string TimeEstimated
		{
            get { return timeEstimated; }
            set { timeEstimated = value; }
        }

        /// <summary>
        /// Read/Write. Actual time of the service at this location. 
        /// The time will be either an arrival or departure time, depending on whether it is 
        /// in the subsequent or previous calling point list. 
        /// Will only be present if an estimated time is not present
        /// </summary>
        public string TimeActual
        {
            get { return timeActual; }
            set { timeActual = value; }
        }

        /// <summary>
        /// Read/Write. List of active Adhoc Alert texts for that applies at this location. 
        /// </summary>
        public List<string> AdhocAlerts
        {
            get { return adhocAlerts; }
            set { adhocAlerts = value; }
        }

        #endregion
    }
}