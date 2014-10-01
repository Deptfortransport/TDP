// *********************************************** 
// NAME                 : TestMockCustomEventOneEmailFormatter.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 08/07/2003 
// DESCRIPTION  : An email formatter for
// CustomEventOne
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestMockCustomEventOneEmailFormatter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:18   mturner
//Initial revision.
//
//   Rev 1.1   Jul 25 2003 14:14:52   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// An email formatter for CustomEventOne
	/// </summary>
	public class MyCustomEventEmailFormatter : IEventFormatter
	{
		public MyCustomEventEmailFormatter()
		{
		}

		public string AsString(LogEvent logEvent)
		{
			string output = String.Empty;
			
			if(logEvent is CustomEventOne)
			{
				CustomEventOne customEventOne =
					(CustomEventOne)logEvent;

				output =
					"TD-CustomEventOne\n\n" +
					"Time: " + customEventOne.Time + "\n" +
					"Message: " + customEventOne.Message + "\n" +
					"Category: " + customEventOne.Category + "\n" +
					"Level: " + customEventOne.Level + "\n" +
					"Reference: " + customEventOne.ReferenceNumber;

			}

			return output;

		}
	}
}
