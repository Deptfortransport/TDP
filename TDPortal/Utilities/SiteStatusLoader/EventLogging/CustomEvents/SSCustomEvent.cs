// *********************************************** 
// NAME                 : SSCustomEvent.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Base class for all SS Custom Events
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/CustomEvents/SSCustomEvent.cs-arc  $
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
    /// Defines a base class for all SSCustomEvents
    /// </summary>
    [Serializable]
    public abstract class SSCustomEvent : CustomEvent
    {
        private static DefaultFormatter formatter = new DefaultFormatter();

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <remarks>
        /// Instances of this class will never be logged. 
        /// Only subclasses may be loggged.
        /// </remarks>
        protected SSCustomEvent()
            : base()
        {
        }

        /// <summary>
        /// Provides an event formatter for publishing to files.
        /// </summary>
        override public IEventFormatter FileFormatter
        {
            get { return formatter; }
        }


        /// <summary>
        /// Provides an event formatter for publishing to event logs
        /// </summary>
        override public IEventFormatter EventLogFormatter
        {
            get { return formatter; }
        }

        /// <summary>
        /// Provides an event formatter for publishing to console.
        /// </summary>
        override public IEventFormatter ConsoleFormatter
        {
            get { return formatter; }
        }
    }
}
