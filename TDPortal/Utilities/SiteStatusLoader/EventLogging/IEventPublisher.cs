// *********************************************** 
// NAME                 : IEventPublisher.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Interface
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/IEventPublisher.cs-arc  $
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
    /// Interface for an Event Publisher.
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        string Identifier { get;}

        /// <summary>
        /// Publishes a <c>LogEvent</c> to a sink.
        /// </summary>
        /// <param name="logEvent"><c>LogEvent</c> to publish.</param>
        /// <exception cref="TransportDirect.Common.TDException"><c>LogEvent</c> was not successfully written to the sink.</exception>
        void WriteEvent(LogEvent logEvent);
    }
}
