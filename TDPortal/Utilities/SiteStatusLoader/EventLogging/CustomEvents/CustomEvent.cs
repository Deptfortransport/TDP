// *********************************************** 
// NAME                 : CustomEvent.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Abstract CustomEvent class. A custom defined event must derive from this class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/CustomEvents/CustomEvent.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:28:14   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Abstract class that is used by clients to define their own event types.
    /// </summary>
    [Serializable]
    public abstract class CustomEvent : LogEvent
    {

        /// <summary>
        /// The default formatter. This is used should clients of this class not provide
        /// a custom formatter. Note that the default formatter will not format
        /// any custom data that clients may include in their custom event classes.
        /// </summary>
        private static IEventFormatter defaultFormatter = new DefaultFormatter();

        /// <summary>
        /// The filter class that is used to determine whether the custom event should
        /// be logged or not.
        /// </summary>
        private static IEventFilter filter = new CustomEventFilter();


        /// <summary>
        /// Gets the default event formatter.
        /// </summary>
        public IEventFormatter DefaultFormatter
        {
            get { return defaultFormatter; }
        }

        /// <summary>
        /// Gets the event filter.
        /// </summary>
        override public IEventFilter Filter
        {
            get { return filter; }
        }

        /// <summary>
        /// Construct a new CustomEvent
        /// </summary>
        protected CustomEvent()
            : base()
        {
            // set class name - used as an id to associate events to publishers, and also in config properties
            this.ClassName = this.GetType().Name;
        }

    }
}
