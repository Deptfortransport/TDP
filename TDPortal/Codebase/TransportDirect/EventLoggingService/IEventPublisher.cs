// *********************************************** 
// NAME                 : IEventPublisher.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Interface for an Event Publisher.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/IEventPublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:04   mturner
//Initial revision.
//
//   Rev 1.1   Jul 24 2003 18:27:38   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Interface for an Event Publisher.
	/// </summary>
	public interface IEventPublisher
	{
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		string Identifier{get;}

		/// <summary>
		/// Publishes a <c>LogEvent</c> to a sink.
		/// </summary>
		/// <param name="logEvent"><c>LogEvent</c> to publish.</param>
		/// <exception cref="TransportDirect.Common.TDException"><c>LogEvent</c> was not successfully written to the sink.</exception>
		void WriteEvent(LogEvent logEvent);	
	}
}
