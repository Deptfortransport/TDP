// *********************************************** 
// NAME                 : ReferenceTransactionEvent.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Custom event class for Reference Tranaction Events logging
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/CustomEvents/ReferenceTransactionEvent.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:28:16   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Defines the class for capturing Reference Transaction Event data.
    /// </summary>
    [Serializable]
    public class ReferenceTransactionEvent : SSCustomEvent
    {
        #region Private members

        private DateTime timeSubmitted;
        private DateTime timeCompleted;
        private string category;
        private bool serviceLevelAgreement;
        private bool successful;
        private string sessionId;

        private bool logSuccessWriteEvent = false;

        private static ReferenceTransactionEventFileFormatter fileFormatter = new ReferenceTransactionEventFileFormatter();


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for a <c>ReferenceTransactionEvent</c> class. 
        /// A <c>ReferenceTransactionEvent</c> is used
        /// to log reference transaction data using the Event Service.
        /// This class must be serializable to allow logging to MSMQs.
        /// </summary>
        /// <param name="timeSubmitted">Date/Time that reference transaction was submitted, down to the millisecond.</param>
        /// <param name="timeCompleted">Date/Time that reference transaction was completed, down to the millisecond.</param>
        /// <param name="sessionId">The session id used to perform the reference transaction.</param>
        /// <param name="category">The category of reference transaction.</param>
        /// <param name="successful">Indicates whether reference transaction returned the expected results.</param>
        /// <param name="serviceLevelAgreement">Indicates whether reference transaction is used to calculate if SLAs have been met.</param>
        /// <param name="logSuccessWriteEvent">Flag to force a Log entry to be created if the Event is successfully written.
        /// This should only be used for the Historic/Daily data roll up as it gives an indication that the 
        /// Current/Real time monitoring failed to work correctly
        /// </param>
        public ReferenceTransactionEvent(string category,
                                         bool serviceLevelAgreement,
                                         DateTime timeSubmitted,
                                         DateTime timeCompleted,
                                         string sessionId,
                                         bool successful,
                                         bool logSuccessWriteEvent)
            : base()
        {
            this.timeSubmitted = timeSubmitted;
            this.timeCompleted = timeCompleted;
            this.category = category;
            this.serviceLevelAgreement = serviceLevelAgreement;
            this.successful = successful;
            this.sessionId = sessionId;
            this.logSuccessWriteEvent = logSuccessWriteEvent;
        }

        #endregion

        #region Public properties
        /// <summary>
        /// Gets the date/time at which the reference transaction was submitted.
        /// </summary>
        public DateTime TimeSubmitted
        {
            get { return timeSubmitted; }
        }

        /// <summary>
        /// Gets the date/time at which the reference transaction was completed.
        /// </summary>
        public DateTime TimeCompleted
        {
            get { return timeCompleted; }
        }

        /// <summary>
        /// Gets the event category.
        /// </summary>
        public string EventType
        {
            get { return category; }
        }

        /// <summary>
        /// Gets the SLA indicator. True if transaction is used for SLA calculations.
        /// </summary>
        public bool ServiceLevelAgreement
        {
            get { return serviceLevelAgreement; }
        }

        /// <summary>
        /// Gets the success indicator. True if transaction completed successfully.
        /// </summary>
        public bool Successful
        {
            get { return successful; }
        }

        /// <summary>
        /// Gets the session identifier
        /// </summary>
        public string SessionId
        {
            get { return sessionId; }
        }

        /// <summary>
        /// Gets whether a message should be logged when this Event has been successfully written
        /// </summary>
        public bool LogSuccessWriteEvent
        {
            get { return logSuccessWriteEvent; }
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
