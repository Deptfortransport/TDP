// *********************************************** 
// NAME                 : GISQueryEvent.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 14/01/2013
// DESCRIPTION          : Defines a custom event for logging GIS Query event data
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/GISQueryEvent.cs-arc  $
//
//   Rev 1.0   Jan 14 2013 14:42:32   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TD.ThemeInfrastructure;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// Defines a custom event for logging GIS Query event data
    /// </summary>
    [Serializable]
    public class GISQueryEvent : TDPCustomEvent
    {
        #region Private Fields

        private static GISQueryEventFileFormatter fileFormatter = new GISQueryEventFileFormatter();

        private GISQueryType gisQueryType = GISQueryType.Unknown;
        private DateTime submitted;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for the GISQueryEvent to log events
        /// </summary>
        public GISQueryEvent(GISQueryType gisQueryType, DateTime submitted, string sessionId)
            : base(sessionId, false)
        {
            this.gisQueryType = gisQueryType;
            this.submitted = submitted;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the GISQueryType
        /// </summary>
        public GISQueryType GISQueryType
        {
            get { return gisQueryType; }
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