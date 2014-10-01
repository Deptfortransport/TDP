// *********************************************** 
// NAME                 : IEventFilter.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Interface for an event filter.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/IEventFilter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:04   mturner
//Initial revision.
//
//   Rev 1.1   Jul 24 2003 18:27:36   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
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
