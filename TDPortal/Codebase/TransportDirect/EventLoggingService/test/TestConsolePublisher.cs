// *********************************************** 
// NAME                 : TestConsolePublisher.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 08/07/2003 
// DESCRIPTION  : NUnit test for the
// ConsolePublisher class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestConsolePublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:14   mturner
//Initial revision.
//
//   Rev 1.5   Apr 01 2004 16:05:40   geaton
//Changes resulting from unit testing refactoring exercise.
//
//   Rev 1.4   Jul 30 2003 18:08:44   geaton
//Changes to OperationalEvent constructors
//
//   Rev 1.3   Jul 30 2003 12:50:56   geaton
//Change test to pass a complex data object as the 'target' parameter when creating an operational event.
//
//   Rev 1.2   Jul 29 2003 17:32:02   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.1   Jul 25 2003 14:14:44   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;
using NUnit.Framework;
using System.Collections;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// NUnit test class for ConsolePublisher
	/// </summary>
	[TestFixture]
	public class TestConsolePublisher
	{
		[Test]
		public void TestWriteEvent()
		{
			// ---------------------------------------------

			// create test data - 16 operational events

			int numberOfEvents = 16;

			OperationalEvent[] operationalEvents =
				new OperationalEvent[numberOfEvents];

			operationalEvents[0] = new OperationalEvent(
				TDEventCategory.Database,
				TDTraceLevel.Error,
				"Test Error 1: There was a connection error with Database 123XY",
				"Test Target Number 1", "123");

			operationalEvents[1] = new OperationalEvent(
				TDEventCategory.Business,
				TDTraceLevel.Info,
				"Test Error 2: A problem occurred whilst attempting to load G5265",
				"Test Target Number 2", "456");

			operationalEvents[2] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Warning,
				"Test Error 3: Connection XYF failed",
				"Test Target Number 3", "789");

			operationalEvents[3] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Verbose,
				"Test Error 4: Timeout occurred processing H6001.aspx",
				"Test Target Number 4", "012");

			operationalEvents[4] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Error,
				"Test Error 5: Error Number 1234HDJ has occurred",
				"Test Target Number 5", "345");

			operationalEvents[5] = new OperationalEvent(
				TDEventCategory.Business,
				TDTraceLevel.Warning,
				"Test Error 6: Page Index.aspx failed to load",
				"Test Target Number 6", "678");

			operationalEvents[6] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Verbose,
				"Test Error 7: Server SG12323 has failed",
				"Test Target Number 7", "901");

			operationalEvents[7] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Info,
				"Test Error 8: Server SG123 failed to reboot",
				"Test Target Number 8", "234");

			operationalEvents[8] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Info,
				"Test Error 9: Component C123 malfunction",
				"Test Target Number 9", "567");

			operationalEvents[9] = new OperationalEvent(
				TDEventCategory.Business,
				TDTraceLevel.Warning,
				"Test Error 10: Hard disk failure on SG134",
				"Test Target Number 10", "890");

			operationalEvents[10] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Warning,
				"Test Error 11: SG123 has failed to initialise",
				"Test Target Number 11", "123");
			
			operationalEvents[11] = new OperationalEvent(
				TDEventCategory.Database,
				TDTraceLevel.Verbose,
				"Test Error 12: Database connection error on 'Journeys'",
				"Test Target Number 12", "456");

			operationalEvents[12] = new OperationalEvent(
				TDEventCategory.Business,
				TDTraceLevel.Info,
				"Test Error 13: Unknown Error",
				"Test Target Number 13", "789");

			operationalEvents[13] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Info,
				"Test Error 14: Error writing to audit trail",
				"Test Target Number 14", "012");

			operationalEvents[14] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Warning,
				"Test Error 15: Driver D1 failed initialisation routine",
				"Test Target Number 15");

			// for this event add a complex data type as the target parameter
			ArrayList target = new ArrayList();
			target.Add("My target string.");
			operationalEvents[15] = new OperationalEvent(	
				TDEventCategory.Database,
				TDTraceLevel.Info,
				"Test Error 16: D123 failed",
				target	
				);

			// ---------------------------------------------

			// create two console publishers, one where the
			// the streamSetting is set to "Out", and one
			// where the stream setting is set to "Error"

			ConsolePublisher outPublisher = new ConsolePublisher("id", "Out");
			ConsolePublisher errorPublisher = new ConsolePublisher("id2", "Error");

			// write 8 of the operational events to the Out and 8 to Error

			for(int i=0; i<8; i++)
				outPublisher.WriteEvent(operationalEvents[i]);

			for(int i=8; i<16; i++)
				errorPublisher.WriteEvent(operationalEvents[i]);

			//-----------------------------------------------

			// create custom events

			CustomEventOne customEventOne = new CustomEventOne
				(TDEventCategory.Business, TDTraceLevel.Warning,
				"A custom event one message",  Environment.UserName, 12345);

			CustomEventTwo customEventTwo = new CustomEventTwo
				(TDEventCategory.ThirdParty, TDTraceLevel.Error,
				"A custom event two message", Environment.UserName, 3343);

			// CustomEventOne has a console formatter defined, therefore
			// log event should be written in the format specified
			// in CustomEventOneConsoleFormatter
			outPublisher.WriteEvent(customEventOne);
			
			// CustomEventTwo has no consolel formatter defined, therefore
			// log event should be written in the format specified
			// by the DefaultFormatter.
			outPublisher.WriteEvent(customEventTwo);
		}

		/// <remarks>
		/// Manual verification:
		/// Verify that 8 operational events and 2 custom events are published to the Output console.
		/// Verify that 8 operational events are published to the Error console.
		/// </remarks>
		[Test]
		[Ignore("Manual verification required")]
		public void ManualVerification()
		{}
	}
}
