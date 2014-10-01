// *********************************************** 
// NAME                 : LogEvent.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class for log events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/LogEvent.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:27:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Class for log events.
    /// Has serializable attribute to allow events to be published on MSMQs
    /// </summary>
    [Serializable]
    public abstract class LogEvent
    {
        /// <summary>
        /// Gets the File Formatter.
        /// </summary>
        abstract public IEventFormatter FileFormatter { get;}

        /// <summary>
        /// Gets the Event Log Formatter.
        /// </summary>
        abstract public IEventFormatter EventLogFormatter { get;}

        /// <summary>
        /// Gets the Console Formatter.
        /// </summary>
        abstract public IEventFormatter ConsoleFormatter { get;}

        /// <summary>
        /// Gets the Filter.
        /// </summary>
        abstract public IEventFilter Filter { get;}


        /// <summary>
        /// Gets and sets the string containing the list of publisher class types that
        /// have published the event.
        /// </summary>
        public string PublishedBy
        {
            get { return publishedBy.ToString(); }

            set { publishedBy.Append(value + " "); }
        }

        private readonly DateTime time;

        private StringBuilder publishedBy;

        /// <summary>
        /// Gets the time at which the event was logged.
        /// </summary>
        public DateTime Time
        {
            get { return time; }
        }

        private string className;

        /// <summary>
        /// Gets and sets the class name. The class name does not include the full namespace path.
        /// The class name is used as a mechanism for associating events with publishers in TDTraceListener.
        /// </summary>
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        /// <summary>
        /// Constructor used to create a log event.
        /// </summary>
        protected LogEvent()
        {
            this.time = DateTime.Now;

            this.publishedBy = new StringBuilder();
        }

    }
}
