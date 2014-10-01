// *********************************************** 
// NAME             : GISQueryEvent.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Defines a custom event for logging GIS Query event data
// ************************************************
// 

using System;
using TDP.Common.EventLogging;
using TDP.Reporting.Events.Formatters;

namespace TDP.Reporting.Events
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