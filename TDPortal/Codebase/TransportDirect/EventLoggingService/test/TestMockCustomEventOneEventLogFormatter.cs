// *********************************************** 
// NAME                 : TestMockCustomEventOneEventLogFormatter.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 08/07/2003 
// DESCRIPTION  : An Event Log formatter for
// CustomEventOne
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestMockCustomEventOneEventLogFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:18   mturner
//Initial revision.
//
//   Rev 1.1   Jul 25 2003 14:14:54   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// An Event Log formatter for CustomEventOne
	/// </summary>
	public class MyCustomEventEventLogFormatter : IEventFormatter
	{
		public MyCustomEventEventLogFormatter()
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
