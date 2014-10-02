// *********************************************** 
// NAME                 : RepeatVisitorEvent.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 12/05/2008
// DESCRIPTION          : Defines a custom event for logging repeat visitor event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/RepeatVisitorEvent.cs-arc  $
//
//   Rev 1.0   May 14 2008 15:34:32   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TD.ThemeInfrastructure;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    [Serializable]
    public class RepeatVisitorEvent : TDPCustomEvent
    {
        private static RepeatVisitorEventFileFormatter fileFormatter = new RepeatVisitorEventFileFormatter();

        private RepeatVisitorType repeatVisitorType = RepeatVisitorType.VisitorUnknown;
        private string sessionIdOld = string.Empty;
        private DateTime lastVisitedDateTime = DateTime.MinValue;
        private string lastPageVisited = string.Empty;
        private string domain = string.Empty;
        private string userAgent = string.Empty;
        private int themeId = 0;


        /// <summary>
        /// Constructor for the RepeatVisitorEvent to log events
        /// </summary>
        /// <param name="sessionIdOld"></param>
        /// <param name="sessionIdNew"></param>
        /// <param name="lastVisitedDateTime"></param>
        /// <param name="domain"></param>
        /// <param name="userAgent"></param>
        public RepeatVisitorEvent(RepeatVisitorType repeatVisitorType, string sessionIdOld, string sessionIdNew, DateTime lastVisitedDateTime, 
            string lastPageVisted, string domain, string userAgent)
            : base(sessionIdNew, false)
        {
            this.repeatVisitorType = repeatVisitorType;
            this.sessionIdOld = sessionIdOld;
            this.lastVisitedDateTime = lastVisitedDateTime;
            this.lastPageVisited = lastPageVisted;
            this.domain = domain;
            this.userAgent = userAgent;

            try
            {
                this.themeId = ThemeProvider.Instance.GetTheme().Id;
            }
            catch
            {
                this.themeId = 0;
            }
        }


        /// <summary>
        /// Gets the repeatVisitorType
        /// </summary>
        public RepeatVisitorType RepeatVisitorType
        {
            get { return repeatVisitorType; }
        }
        
        /// <summary>
        /// Gets the sessionIdOld
        /// </summary>
        public string SessionIdOld
        {
            get { return sessionIdOld; }
        }

        /// <summary>
        /// Gets the lastVisitedDateTime
        /// </summary>
        public DateTime LastVisitedDateTime
        {
            get { return lastVisitedDateTime; }
        }

        /// <summary>
        /// Gets the lastPageVisited
        /// </summary>
        public string LastPageVisted
        {
            get { return lastPageVisited; }
        }

        /// <summary>
        /// Gets the domain
        /// </summary>
        public string Domain
        {
            get { return domain; }
        }

        /// <summary>
        /// Gets the userAgent
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
        }

        /// <summary>
        /// Returns stuff
        /// </summary>
        public int ThemeId
        {
            get { return themeId; }
        }

        /// <summary>
        /// Provides an event formatter for publishing to files.
        /// </summary>
        override public IEventFormatter FileFormatter
        {
            get { return fileFormatter; }
        }
    }
}
