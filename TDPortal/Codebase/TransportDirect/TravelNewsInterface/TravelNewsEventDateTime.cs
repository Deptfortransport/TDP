// *********************************************** 
// NAME                 : TravelNewsEventDateTime.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 31/08/2011
// DESCRIPTION          : Represents Daily Start and End date time for the travel news item calendar day
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNewsInterface/TravelNewsEventDateTime.cs-arc  $ 
//
//   Rev 1.0   Sep 02 2011 11:46:10   apatel
//Initial revision.
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.TravelNewsInterface
{
    /// <summary>
    /// TravelNewsEventDateTime class
    /// Represents Daily Start and End date time for the travel news item
    /// </summary>
    public class TravelNewsEventDateTime
    {
        #region Private Fields
        private DateTime dailyStartDateTime = DateTime.MinValue;
        private DateTime dailyEndDateTime = DateTime.MinValue;
        #endregion

        #region Constructor
        /// <summary>
        /// TravelNewsEventDateTime Constructor
        /// </summary>
        /// <param name="dailyStartDateTime">Daily start datetime</param>
        /// <param name="dailyEndDateTime">Daily end datetime</param>
        public TravelNewsEventDateTime(DateTime dailyStartDateTime, DateTime dailyEndDateTime)
        {
            this.dailyStartDateTime = dailyStartDateTime;
            this.dailyEndDateTime = dailyEndDateTime;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Read only. Returns the daily start date time for the travel news incident
        /// </summary>
        public DateTime DailyStartDateTime
        {
            get { return dailyStartDateTime; }
        }

        /// <summary>
        /// Read only. Returns the daily end date time for the travel news incident
        /// </summary>
        public DateTime DailyEndDateTime
        {
            get { return dailyEndDateTime; }
        }
        #endregion
    }
}
