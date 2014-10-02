// *********************************************** 
// NAME                 : AccessibleEvent.cs 
// AUTHOR               : David Lane
// DATE CREATED         : 18/01/2013
// DESCRIPTION          : Defines a custom event for logging Accessible event data
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/AccessibleEvent.cs-arc  $
//
//   Rev 1.0   Jan 24 2013 13:00:56   dlane
//Initial revision.
//Resolution for 5873: CCN:677 - Accessible Journeys Planner

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TD.ThemeInfrastructure;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// Defines a custom event for logging Accessible event data
    /// </summary>
    [Serializable]
    public class AccessibleEvent : TDPCustomEvent
    {
        #region Private Fields

        private static AccessibleEventFileFormatter fileFormatter = new AccessibleEventFileFormatter();

        private AccessibleEventType accessibleEventType = AccessibleEventType.Unknown;
        private DateTime submitted;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for the AccessibleEvent to log events
        /// </summary>
        public AccessibleEvent(AccessibleEventType accessibleEventType, DateTime submitted, string sessionId)
            : base(sessionId, false)
        {
            this.accessibleEventType = accessibleEventType;
            this.submitted = submitted;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the GISQueryType
        /// </summary>
        public AccessibleEventType AccessibleEventType
        {
            get { return accessibleEventType; }
        }

        /// <summary>
        /// Gets the date/time at which the transaction was submitted.
        /// </summary>
        public DateTime Submitted
        {
            get { return submitted; }
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