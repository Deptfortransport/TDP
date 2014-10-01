// *********************************************** 
// NAME                 : IEventFormatter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Interface
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/IEventFormatter.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:27:16   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.EventLogging
{
    /// <summary>
    /// Interface for an Event Formatter.
    /// Concrete formatter classes should implement this inteface to 
    /// provide event publishers with a mechanism of retrieving relevant data 
    /// stored with an event for publishing. 
    /// </summary>
    public interface IEventFormatter
    {
        /// <summary>
        /// Returns relevant data that is stored in the <c>LogEvent</c> passed, 
        /// as a formatted string ready for publishing to a data sink.
        /// </summary>
        /// <param name="logEvent"><c>LogEvent</c> to whose data is to be formatted.</param>
        /// <returns>The event data formatted as a string.</returns>
        string AsString(LogEvent logEvent);
    }
}
