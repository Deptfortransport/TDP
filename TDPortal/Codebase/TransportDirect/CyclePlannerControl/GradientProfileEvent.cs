// *********************************************** 
// NAME			: GradientProfileEvent.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 17/01/2009
// DESCRIPTION	: Class which defines a custom event for logging a Gradient profile event
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/GradientProfileEvent.cs-arc  $
//
//   Rev 1.0   Jan 19 2009 11:09:46   mmodi
//Initial revision.
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.Logging;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    [Serializable]
    public class GradientProfileEvent : TDPCustomEvent
    {
        #region Private members

        private DateTime submitted;
        private GradientProfileEventDisplayCategory displayCategory;
        private static GradientProfileEventFileFormatter fileFormatter = new GradientProfileEventFileFormatter();

        #endregion

        #region Constructor
        /// <summary>
		/// Constructor.
        /// This class must be serializable to allow logging to MSMQs.
		/// </summary>
		/// <param name="sessionId">The session id used to perform the cycle planner request.</param>
		/// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
		/// <param name="cyclePlannerRequestId">Identifier used to identify the cycle planner request.</param>
        public GradientProfileEvent(GradientProfileEventDisplayCategory displayCategory,
                                    DateTime submitted,
									bool userLoggedOn,
									string sessionId): base(sessionId, userLoggedOn)
		{
            this.submitted = submitted;
            if (this.submitted.Year < 2000)
            {
                this.submitted = DateTime.Now;
            }

            this.displayCategory = displayCategory;
		}
        #endregion

        #region Public properties
        /// <summary>
        /// Gets the date/time at which the reference transaction was submitted.
        /// </summary>
        public DateTime Submitted
        {
            get { return submitted; }
        }

        /// <summary>
        /// Gets the display category.
        /// </summary>
        public GradientProfileEventDisplayCategory DisplayCategory
        {
            get { return displayCategory; }
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
