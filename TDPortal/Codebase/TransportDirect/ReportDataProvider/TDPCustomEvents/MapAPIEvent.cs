// NAME             : MapAPIEvent.cs
// AUTHOR           : Amit Patel
// DATE CREATED     : 12/11/2009 
// DESCRIPTION      : Defines the class for capturing Map API Event data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/MapAPIEvent.cs-arc  $ 
//
//   Rev 1.1   Mar 19 2010 13:00:40   mmodi
//Added file Headers
//
// Added header as original author had failed to add

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
    /// <summary>
    /// Defines the class for capturing Map API Event data.
    /// </summary>
    [Serializable]
    public class MapAPIEvent : TDPCustomEvent
    {
        private DateTime submitted;
        private MapAPIEventCommandCategory commandCategory;
        private static MapAPIEventFileFormatter fileFormatter = new MapAPIEventFileFormatter();

        /// <summary>
        /// Constructor for a <c>MapAPIEvent</c> class. 
        /// </summary>
        /// <param name="submitted">Date and Time that map event was submitted, down to the millisecond.</param>
        /// <param name="sessionId">The session id used to perform map event.</param>
        /// <param name="commandCategory">The command category that uniquely identifies the type of map event.</param>
        public MapAPIEvent(MapAPIEventCommandCategory commandCategory,
                        DateTime submitted,
                        string sessionId)
            : base(sessionId, false)
        {
            this.submitted = submitted;
            if (this.submitted.Year < 2000)
            {
                this.submitted = DateTime.Now;
            }
            this.commandCategory = commandCategory;

        }

        /// <summary>
        /// Gets the date/time at which the reference transaction was submitted.
        /// </summary>
        public DateTime Submitted
        {
            get { return submitted; }
        }

        /// <summary>
        /// Gets the command category.
        /// </summary>
        public MapAPIEventCommandCategory CommandCategory
        {
            get { return commandCategory; }
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
