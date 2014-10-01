// *********************************************** 
// NAME                 : CustomEventFilter.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Class used to determine whether a
// custom events should be logged or not.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/CustomEventFilter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:00   mturner
//Initial revision.
//
//   Rev 1.2   Sep 12 2003 13:15:18   geaton
//Removed redundant conditional. (Performance measure.)
//
//   Rev 1.1   Jul 24 2003 18:27:28   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Class used to determine whether CustomEvents should be published.
	/// </summary>
	public class CustomEventFilter : IEventFilter
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public CustomEventFilter()
		{
		}

		/// <summary>
		/// Determines whether the given <c>LogEvent</c> should be published.
		/// </summary>
		/// <param name="logEvent">The <c>LogEvent</c> to test for.</param>
		/// <returns>Returns <c>true</c> if the <c>LogEvent</c> should be logged, otherwise <c>false</c>. Always returns <c>false</c> if the log event passed is not a <c>CustomEvent</c>.</returns>
		public bool ShouldLog(LogEvent logEvent)
		{		
			CustomEvent ce = (CustomEvent)logEvent;

			if (CustomEventSwitch.On(ce.ClassName))
				return true;
			else
				return false;			
		}
	}
	
}
