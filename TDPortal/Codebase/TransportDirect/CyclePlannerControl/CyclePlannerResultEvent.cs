// *********************************************** 
// NAME			: CyclePlannerResultEvent.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 20/08/2008
// DESCRIPTION	: Class which defines a custom event for logging a Cycle planner result
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CyclePlannerResultEvent.cs-arc  $
//
//   Rev 1.0   Aug 22 2008 10:47:12   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    [Serializable]
    public class CyclePlannerResultEvent : TDPCustomEvent
    {
        #region Private members
        private string cyclePlannerRequestId;
        private JourneyPlanResponseCategory responseCategory;

        private static CyclePlannerResultEventFileFormatter fileFormatter = new CyclePlannerResultEventFileFormatter();
        #endregion

        #region Constructor
        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sessionId">The session id used to perform the cycle planner request.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="cyclePlannerRequestId">Identifier used to identify the cycle planner request.</param>
        public CyclePlannerResultEvent(string cyclePlannerRequestId,
                                       JourneyPlanResponseCategory responseCategory,
									   bool userLoggedOn,
									   string sessionId): base(sessionId, userLoggedOn)
		{
            this.cyclePlannerRequestId = cyclePlannerRequestId;
            this.responseCategory = responseCategory;
		}
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the cycle planner request identifier.
        /// </summary>
        public string CyclePlannerRequestId
        {
            get { return cyclePlannerRequestId; }
        }

        /// <summary>
        /// Gets the response category.
        /// </summary>
        public JourneyPlanResponseCategory ResponseCategory
        {
            get { return responseCategory; }
        }

        /// <summary>
        /// Provides an event formatter for publishing to files.
        /// </summary>
        override public IEventFormatter FileFormatter
        {
            get { return fileFormatter; }
        }
        #endregion
    }
}
