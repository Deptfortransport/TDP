// *********************************************** 
// NAME			: InternationalPlannerRequestEvent.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 25/01/2010
// DESCRIPTION	: Class which defines a custom event for logging International planner requests
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalPlannerRequestEvent.cs-arc  $
//
//   Rev 1.0   Jan 25 2010 15:14:18   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    /// <summary>
    /// International planner request event class
    /// </summary>
    [Serializable]
    public class InternationalPlannerRequestEvent : TDPCustomEvent
    {
        #region Private members
        private string internationalPlannerRequestId;

        private static InternationalPlannerRequestEventFileFormatter fileFormatter = new InternationalPlannerRequestEventFileFormatter();
        #endregion

        #region Constructor
        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sessionId">The session id used to perform the international planner request.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="internationalPlannerRequestId">Identifier used to identify the international planner request.</param>
        public InternationalPlannerRequestEvent(string internationalPlannerRequestId,
									   bool userLoggedOn,
									   string sessionId): base(sessionId, userLoggedOn)
		{
            this.internationalPlannerRequestId = internationalPlannerRequestId;
		}
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the international planner request identifier.
        /// </summary>
        public string InternationalPlannerRequestId
        {
            get { return internationalPlannerRequestId; }
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
