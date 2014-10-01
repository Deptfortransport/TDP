// *********************************************** 
// NAME                 : DefaultFormatter.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 08/07/2003 
// DESCRIPTION  : A default formatter for 
// log events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/DefaultFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:00   mturner
//Initial revision.
//
//   Rev 1.3   Aug 22 2003 11:05:04   geaton
//Improved output string to include warning and class name of event that was formatted.
//
//   Rev 1.2   Jul 29 2003 17:31:22   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.1   Jul 24 2003 18:26:44   geaton
//Added comments and changed prefix from TD-OP to TD-EVENT since can be used by both event types.

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// A class which formats log events for publishing.
	/// Used when a more specific formatter is not available
	/// for a given <c>LogEvent</c> type.
	/// </summary>
	public class DefaultFormatter : IEventFormatter
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public DefaultFormatter()
		{}

		/// <summary>
		/// Formats log event data into a string. The data included in the
		/// format string will exist for all <c>LogEvent</c> types.
		/// </summary>
		/// <param name="logEvent">The <c>LogEvent</c> to format.</param>
		/// <returns>A formatted string containing event data common across all event types.</returns>
		public string AsString(LogEvent logEvent)
		{
			string output = String.Format(Messages.DefaultFormatterOutput, logEvent.Time, logEvent.ClassName);
			return output;
		}
	}
}
