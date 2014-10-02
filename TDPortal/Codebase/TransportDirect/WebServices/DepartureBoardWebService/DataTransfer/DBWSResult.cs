// *********************************************** 
// NAME             : DBWSResult.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: DBWSResult class
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportDirect.WebService.DepartureBoardWebService.DataTransfer
{
    /// <summary>
    /// DBWSResult class
    /// </summary>
    [Serializable]
    public class DBWSResult
    {
        #region Private variables

        private string requestId;

		private List<DBWSMessage> messages;

        private DateTime generatedAt;
        private DBWSLocation location;

        private List<DBWSMessage> stationBoardMessages;
        private List<DBWSService> stationBoardServices;

        private bool platformAvailable;

        private DBWSServiceDetail serviceDetail;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSResult()
		{
            messages = new List<DBWSMessage>();
            stationBoardServices = new List<DBWSService>();
		}

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. RequestId of the request this result is for
        /// </summary>
        public string RequestId
        {
            get { return requestId; }
            set { requestId = value; }
        }

        /// <summary>
        /// Read/Write. Messages for this result
        /// </summary>
        public List<DBWSMessage> Messages
		{
            get { return messages; }
            set { messages = value; }
        }

        /// <summary>
        /// Read/Write. Time at which the station board was generated for the location
        /// </summary>
        public DateTime GeneratedAt
        {
            get { return generatedAt; }
            set { generatedAt = value; }
        }

        /// <summary>
        /// Read/Write. Location that the station board is for
        /// </summary>
        public DBWSLocation Location
        {
            get { return location; }
            set { location = value; }
        }

        /// <summary>
        /// Read/Write. List of textual messages that should be displayed with the station board. 
        /// The messages are typically used to display important disruption information that applies to the 
        /// location that the station board was for
        /// </summary>
        public List<DBWSMessage> StationBoardMessages
        {
            get { return stationBoardMessages; }
            set { stationBoardMessages = value; }
        }
                
        /// <summary>
        /// Read/Write. Services for the station board. Each service will include its origin and destination,
        /// and the arrival/departure time at/from the station board location (depending if arrival and/or departure
        /// times were requested) 
        /// </summary>
        public List<DBWSService> StationBoardServices
        {
            get { return stationBoardServices; }
            set { stationBoardServices = value; }
        }

        /// <summary>
        /// Read/Write. Indicates if platform information is available, false indicates the "platform" information
        /// should be supressed on the output display
        /// </summary>
        public bool PlatformAvailable 
        {
            get { return platformAvailable; }
            set { platformAvailable = value; }
        }

        /// <summary>
        /// Read/Write. Service detail information for a requested service id from a station board result.
        /// Will only be populated for GetServiceDetail request
        /// </summary>
        public DBWSServiceDetail ServiceDetail
        {
            get { return serviceDetail; }
            set { serviceDetail = value; }
        }

        #endregion
    }
}
