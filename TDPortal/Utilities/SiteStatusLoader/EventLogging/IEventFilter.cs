// *********************************************** 
// NAME                 : IEventFilter.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Interface
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/IEventFilter.cs-arc  $
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
    /// Interface for an event filter.
    /// </summary>
    public interface IEventFilter
    {
        /// <summary>
        /// Used to determine whether an event should be published.
        /// </summary>
        /// <param name="logEvent">LogEvent to be published.</param>
        /// <returns><c>true</c> if <c>LogEvent</c> should be published.</returns>
        bool ShouldLog(LogEvent logEvent);
    }
}
