// *********************************************** 
// NAME                 : IEventFormatter.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Interface for an Event Formatter.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/IEventFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:04   mturner
//Initial revision.
//
//   Rev 1.2   Aug 21 2003 17:40:40   geaton
//Clarified comments
//
//   Rev 1.1   Jul 24 2003 18:27:36   geaton
//Added/updated comments

using System;
using System.IO;

namespace TransportDirect.Common.Logging
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
