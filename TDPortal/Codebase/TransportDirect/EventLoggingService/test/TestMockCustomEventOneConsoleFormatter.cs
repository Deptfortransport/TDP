// *********************************************** 
// NAME                 : TestMockCustomEventOneConsoleFormatter.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 08/07/2003 
// DESCRIPTION  : A Console formatter for
// CustomEventOne
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestMockCustomEventOneConsoleFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:16   mturner
//Initial revision.
//
//   Rev 1.1   Jul 25 2003 14:14:52   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// A console formatter for CustomEventOne
	/// </summary>
	public class MyCustomEventConsoleFormatter : IEventFormatter
	{
		public MyCustomEventConsoleFormatter()
		{
		}

		public string AsString(LogEvent logEvent)
		{
			string output = String.Empty;

			if(logEvent is CustomEventOne)
			{
				CustomEventOne customEventOne = (CustomEventOne)logEvent;

				output =
					"TD-CustomEventOne" + " " + 
					customEventOne.Time + " " +
					customEventOne.Message + " " +
					customEventOne.Category + " " +
					customEventOne.Level + " " +
					customEventOne.ReferenceNumber;
			}
			return output;
		}
	}
}
