// *********************************************** 
// NAME			: InternationalPlannerEvent.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 29/01/2010
// DESCRIPTION	: Class which defines a custom event for logging International planner events
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalPlannerEvent.cs-arc  $
//
//   Rev 1.1   Feb 18 2010 15:49:22   mmodi
//Corrected file formatter
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 02 2010 10:04:10   apatel
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
    /// International planner event class
    /// </summary>
    [Serializable]
    public class InternationalPlannerEvent : TDPCustomEvent
    {
        #region Private members
        private InternationalPlannerType internationalPlannerType;

        private static InternationalPlannerEventFileFormatter fileFormatter = new InternationalPlannerEventFileFormatter();
        #endregion

        #region Constructor
        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sessionId">The session id used to perform the international planner request.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="internationalPlannerRequestId">Identifier used to identify the international planner request.</param>
        public InternationalPlannerEvent(InternationalPlannerType internationalPlannerType,
									   bool userLoggedOn,
									   string sessionId): base(sessionId, userLoggedOn)
		{
            this.internationalPlannerType = internationalPlannerType;
		}
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the international planner type.
        /// </summary>
        public InternationalPlannerType InternationalPlanner
        {
            get { return internationalPlannerType; }
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
