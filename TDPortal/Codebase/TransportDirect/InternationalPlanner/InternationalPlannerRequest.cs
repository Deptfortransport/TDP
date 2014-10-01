// *********************************************** 
// NAME			: InternationalPlannerRequest.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which contains the properties and values needed for submitting a journey planning 
//                request to the International planner
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalPlannerRequest.cs-arc  $
//
//   Rev 1.2   Feb 09 2010 09:53:04   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 04 2010 10:26:10   mmodi
//Updates as part of development
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:38   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Class which contains the properties and values needed for submitting a journey planning 
    /// request to the International planner
    /// </summary>
    [Serializable()]
    public class InternationalPlannerRequest : IInternationalPlannerRequest
    {
        #region Private members

        private string requestID;
        private string sessionID;
        private int userType;

        // Dates
        private DateTime outwardDateTime;

        // Modes
        private InternationalModeType[] modeType;

        // Locations
        private string originName;
        private string originCityID;
        private string[] originNaptans;
        private string destinationName;
        private string destinationCityID;
        private string[] destinationNaptans;

        #endregion

        #region Constructor
		
        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalPlannerRequest()
		{			
		}

		#endregion

        #region Public properties

        /// <summary>
        /// Read/Write. The ID of this request
        /// </summary>
        public string RequestID
        {
            get { return requestID; }
            set { requestID = value; }
        }

        /// <summary>
        /// Read/Write. The Session ID of this request
        /// </summary>
        public string SessionID
        {
            get { return sessionID; }
            set { sessionID = value; }
        }

        /// <summary>
        /// Read/Write. The UserType for this request
        /// </summary>
        public int UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        /// <summary>
        /// Read/Write. The Outward datetime for this request
        /// </summary>
        public DateTime OutwardDateTime
        {
            get { return outwardDateTime; }
            set { outwardDateTime = value; }
        }

        /// <summary>
        /// Read/Write. The Modes required for the request
        /// </summary>
        public InternationalModeType[] ModeType
        {
            get { return modeType; }
            set { modeType = value; }
        }

        /// <summary>
        /// Read/Write. The Origin location name for the request
        /// </summary>
        public string OriginName
        {
            get { return originName; }
            set { originName = value; }
        }

        /// <summary>
        /// Read/Write. The Origin City ID for where the request. Used to add a city transfer detail leg.
        /// </summary>
        public string OriginCityID
        {
            get { return originCityID; }
            set { originCityID = value; }
        }

        /// <summary>
        /// Read/Write. The Origin location naptans for the request
        /// </summary>
        public string[] OriginNaptans
        {
            get { return originNaptans; }
            set { originNaptans = value; }
        }

        /// <summary>
        /// Read/Write. The Destination location name for the request
        /// </summary>
        public string DestinationName
        {
            get { return destinationName; }
            set { destinationName = value; }
        }

        /// <summary>
        /// Read/Write. The Destination City ID for where the request. Used to add a city transfer detail leg.
        /// </summary>
        public string DestinationCityID
        {
            get { return destinationCityID; }
            set { destinationCityID = value; }
        }

        /// <summary>
        /// Read/Write. The Destination location naptans for the request
        /// </summary>
        public string[] DestinationNaptans
        {
            get { return destinationNaptans; }
            set { destinationNaptans = value; }
        }

        #endregion
    }
}
