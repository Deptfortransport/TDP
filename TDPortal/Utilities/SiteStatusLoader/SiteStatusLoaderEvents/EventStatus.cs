// *********************************************** 
// NAME                 : EventStatus.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: EventStatus class used to hold Status details
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderEvents/EventStatus.cs-arc  $
//
//   Rev 1.1   Aug 24 2011 11:07:48   mmodi
//Added a manual import facility to the SSL UI tool
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.0   Aug 23 2011 11:04:34   mmodi
//Initial revision.
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.2   Aug 23 2011 10:35:20   mmodi
//Updates to log historic multi-step transactions download data
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.1   Jun 16 2009 15:48:50   mmodi
//Added a failed flag
//Resolution for 5298: Site Status Loader - Update to log Red Alerts as successful
//
//   Rev 1.0   Apr 01 2009 13:37:10   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

using AO.Common;

namespace AO.SiteStatusLoaderEvents
{
    public class EventStatus
    {
        #region Private members

        private int eventID;
        private string eventName;
        private int eventIDParent;
        private string eventNameParent;
        private string referenceTransactionType;
        private bool slaTransaction; 
        private int status;
        private TimeSpan duration;
        private DateTime timeSubmitted;
        private AlertLevel alertLevel;
        private bool failed;

        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="status"></param>
        /// <param name="duration"></param>
        /// <param name="timeSubmitted"></param>
        public EventStatus(int eventID, string eventName, 
            int eventIDParent, string eventNameParent,
            int status, TimeSpan duration, DateTime timeSubmitted)
        {
            this.eventID = eventID;
            this.eventName = eventName;
            this.eventIDParent = eventIDParent;
            this.eventNameParent = eventNameParent;
            this.status = status;
            this.duration = duration;
            this.timeSubmitted = timeSubmitted;

            this.slaTransaction = false;
            this.alertLevel = AlertLevel.Green;
            this.failed = false;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Event ID of this object
        /// </summary>
        public int EventID
        {
            get { return eventID; }
        }

        /// <summary>
        /// Event name of this object
        /// </summary>
        public string EventName
        {
            get { return eventName; }
        }

        /// <summary>
        /// Event parent ID of this object
        /// </summary>
        public int EventIDParent
        {
            get { return eventIDParent; }
        }

        /// <summary>
        /// Event parent name of this object
        /// </summary>
        public string EventNameParent
        {
            get { return eventNameParent; }
        }

        /// <summary>
        /// Used for logging to reporting database, must match up with the lookup entry in the table of same name
        /// </summary>
        public string ReferenceTransactionType
        {
            get { return referenceTransactionType; }
        }

        /// <summary>
        /// Status value of object
        /// </summary>
        public int Status
        {
            get { return status; }
        }

        /// <summary>
        /// Time taken for the site status event
        /// </summary>
        public TimeSpan Duration
        {
            get { return duration; }
        }

        /// <summary>
        /// Time site status event started
        /// </summary>
        public DateTime TimeSubmitted
        {
            get { return timeSubmitted; }
        }

        /// <summary>
        /// Time site status event completed
        /// </summary>
        public DateTime TimeCompleted
        {
            get { return timeSubmitted.Add(duration); }
        }

        /// <summary>
        /// Alert level for this event status
        /// </summary>
        public AlertLevel AlertLevel
        {
            get { return alertLevel; }
        }

        /// <summary>
        /// Is this an SLA transaction
        /// </summary>
        public bool SlaTransaction
        {
            get { return slaTransaction; }
        }

        /// <summary>
        /// Is this a successful event. True if AlertLevel = Green or Amber or Red.
        /// Only false, if the failed flag is true.
        /// </summary>
        public bool Successful
        {
            get
            {
                bool successful = ((alertLevel == AlertLevel.Green) || (alertLevel == AlertLevel.Amber) || (alertLevel == AlertLevel.Red)) 
                    && !failed;

                return successful ; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the event status alert level
        /// </summary>
        /// <param name="alertLevel"></param>
        /// <param name="slaTransaction"></param>
        /// <param name="referenceTransactionType"></param>
        public void UpdateAlertStatus(AlertLevel alertLevel, bool failed, bool slaTransaction, string referenceTransactionType)
        {
            this.alertLevel = alertLevel;
            this.failed = failed;
            this.slaTransaction = slaTransaction;
            this.referenceTransactionType = referenceTransactionType;
        }

        /// <summary>
        /// Returns a formatted string of this event status
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder eventStatusSB = new StringBuilder();

            eventStatusSB.Append("ID[" + eventID + "] ");
            eventStatusSB.Append("Name[" + eventName + "] ");
            eventStatusSB.Append(string.Format("ParentID[{0}] ",
                (eventIDParent != -1) ? eventIDParent.ToString() : string.Empty));
            eventStatusSB.Append("ParentName[" + eventNameParent + "] ");
            eventStatusSB.Append("Status[" + status.ToString() + "] ");
            eventStatusSB.Append("Alert[" + alertLevel.ToString() + "] ");
            eventStatusSB.Append("Failed[" + failed.ToString() + "] ");
            eventStatusSB.Append("SLA[" + slaTransaction.ToString() + "] ");
            eventStatusSB.Append("Submitted[" + timeSubmitted + "] ");
            eventStatusSB.Append("Duration[" + duration + "] ");

            return eventStatusSB.ToString();
        }

        #endregion
    }
}
