// *********************************************** 
// NAME			: InternationalPlannerResult.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which contains the International journeys planned and any messages to be returned
//                to the caller
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalPlannerResult.cs-arc  $
//
//   Rev 1.1   Feb 09 2010 09:53:02   mmodi
//Updated
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
    /// Class which contains the International journeys planned and any messages to be returned
    /// to the caller
    /// </summary>
    [Serializable()]
    public class InternationalPlannerResult : IInternationalPlannerResult
    {
        #region Private members

        private string requestID;
        private string sessionID;

        private int messageID;
        private string messageDescription;

        private InternationalJourney[] internationalJourneys;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalPlannerResult()
		{			
		}

		#endregion

        #region Public properties

        /// <summary>
        /// Read/Write. The ID of the request
        /// </summary>
        public string RequestID
        {
            get { return requestID; }
            set { requestID = value; }
        }

        /// <summary>
        /// Read/Write. The Session ID of the request
        /// </summary>
        public string SessionID
        {
            get { return sessionID; }
            set { sessionID = value; }
        }

        /// <summary>
        /// Read/Write. The Message ID of the response, indicating success or failure
        /// </summary>
        public int MessageID
        {
            get { return messageID; }
            set { messageID = value; }
        }

        /// <summary>
        /// Read/Write. The Message Description of the response, describing success or failure
        /// </summary>
        public string MessageDescription
        {
            get { return messageDescription; }
            set { messageDescription = value; }
        }

        /// <summary>
        /// Read/Write. The International Journeys planned
        /// </summary>
        public InternationalJourney[] InternationalJourneys
        {
            get { return internationalJourneys; }
            set { internationalJourneys = value; }
        }

        #endregion
    }
}
