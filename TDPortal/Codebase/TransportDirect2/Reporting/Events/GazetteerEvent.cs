// *********************************************** 
// NAME             : GazetteerEvent.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Defines a custom event for logging gazetteer event data
// ************************************************
// 
                
using System;
using TDP.Common.EventLogging;
using TDP.Reporting.Events.Formatters;

namespace TDP.Reporting.Events
{
    /// <summary>
    /// Defines a custom event for logging gazetteer event data
    /// </summary>
    [Serializable()]
    public class GazetteerEvent : TDPCustomEvent
    {
        #region Private members

        private GazetteerEventCategory eventCategory;

        private DateTime submitted;

        private static GazetteerEventFileFormatter fileFormatter = new GazetteerEventFileFormatter();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for a <c>GazetteerEvent</c> class. 
        /// A <c>GazetteerEvent</c> is used
        /// to log Gazetteer transaction data using the Event Service.
        /// This class must be serializable to allow logging to MSMQs.
        /// </summary>
        /// <param name="sessionId">The session id used to perform map event.</param>
        /// <param name="eventCategory">The event category that uniquely identifies the type of Gazetteer event.</param>
        /// <param name="userLoggedOn">Flag indicating whether a registered user is logged on (true) or not (false).</param>
        public GazetteerEvent(GazetteerEventCategory eventCategory,
                              DateTime submitted,
                              string sessionId,
                              bool userLoggedOn)
            : base(sessionId, userLoggedOn)
        {
            this.eventCategory = eventCategory;
            this.submitted = submitted;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the gazetteer event type classifier
        /// </summary>
        public GazetteerEventCategory EventCategory
        {
            get { return eventCategory; }
        }

        /// <summary>
        /// Returns the time when the event was submitted to the Gazetteer
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