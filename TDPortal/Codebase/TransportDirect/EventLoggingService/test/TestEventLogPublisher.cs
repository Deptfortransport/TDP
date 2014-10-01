// *********************************************** 
// NAME                 : TestEventLogPublisher.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 04/07/2003 
// DESCRIPTION  : NUnit test for the
// EventLogPublisher class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestEventLogPublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:16   mturner
//Initial revision.
//
//   Rev 1.12   Feb 07 2005 09:17:52   RScott
//Code Error fix
//
//   Rev 1.11   Feb 07 2005 09:13:38   RScott
//Assertion changed to Assert
//
//   Rev 1.10   Nov 07 2003 09:11:26   geaton
//Added test to check Application event log can be published to.
//
//   Rev 1.9   Oct 21 2003 15:14:36   geaton
//Changes resulting from removal of Event Log Entry Type from properties. (This is now derived from TDTraceLevel of event being logged.)
//
//   Rev 1.8   Oct 21 2003 11:34:50   geaton
//Updated tests following changes made to Event Log Publisher. Checks against message body removed since formatters may change.
//
//   Rev 1.7   Aug 22 2003 14:56:18   geaton
//Updated expected output after change to default formatter.
//
//   Rev 1.6   Jul 30 2003 18:08:38   geaton
//Changes to OperationalEvent constructors
//
//   Rev 1.5   Jul 30 2003 10:44:06   geaton
//Changed name of event log so does not clash with other tests, and refactored code that creates the event log.
//
//   Rev 1.4   Jul 29 2003 17:31:48   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.3   Jul 25 2003 14:14:48   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;
using System.Diagnostics;
using NUnit.Framework;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// NUnit test class for EventLogPublisher
	/// </summary
	[TestFixture]
	public class TestEventLogPublisher
	{
		/// <summary>
		/// Test for WriteEvent.
		/// </summary>
		[Test]
		public void TestWriteEvent()
		{							
			string logName = "ELTest";
			string logSource = "ELSource";
			string machineName = "."; // uses current machine

			// intialise event log sink
			if(!EventLog.Exists(logName))
			{
                EventSourceCreationData sourceData = new EventSourceCreationData(logSource, logName);
                EventLog.CreateEventSource(sourceData);
			}
			
			EventLog log = new EventLog(logName, machineName, logSource);
			log.Source = logSource;
			log.Clear();
			
			// instantiate a new instance of the EventLogPublisher
			EventLogPublisher eventLogPublisher =
				new EventLogPublisher("TDIdentifier1", logName, logSource, machineName);
			
			bool logExists = EventLog.Exists(logName);

			Assert.AreEqual(true, logExists, "The log '" + logName + "' " + " was not found on machine '" + machineName + "'");

			// -------------------------------------------------
			
			string errorMessage = "Error connecting to database";
			
			Object target = new Object();
			string sessionId = "12345";

			// Create an operational event and call the
			// WriteEvent object for the Event Log Publisher

			OperationalEvent operationalEvent =
				new OperationalEvent(TransportDirect.Common.Logging.TDEventCategory.Database,
									 TransportDirect.Common.Logging.TDTraceLevel.Error,
									 errorMessage,
									 target,
									 sessionId);

		
			eventLogPublisher.WriteEvent(operationalEvent);
				
			// the number of entries in the log should be equal to 1
			Assert.AreEqual(1, log.Entries.Count, "The number of entries in the log is incorrect.");
				
			
			
				
			// -------------------------------------------------
				
			// create another operational event and write it to the log
				
			string errorMessage2 = "Warning: problem with file XYZ";
			Object target2 = new Object();

			// Create an operational event and call the
			// WriteEvent object for the Event Log Publisher

			OperationalEvent operationalEvent2 =
				new OperationalEvent(TransportDirect.Common.Logging.TDEventCategory.ThirdParty,
									 TransportDirect.Common.Logging.TDTraceLevel.Warning,				    
									 errorMessage2,
									 target2
									);

			
			eventLogPublisher.WriteEvent(operationalEvent2);

			// the number of entries in the log should now be equal to 2
			Assert.AreEqual(2, log.Entries.Count, "The number of entries in the log is incorrect.");


			// -------------------------------------------------

			// create custom events

			CustomEventOne customEventOne = new CustomEventOne
				(TDEventCategory.Business, TDTraceLevel.Warning,
				"A custom event one message", Environment.UserName, 12345);

			// CustomEventOne has an event log formatter defined, therefore
			// log event should be written in the format specified
			// in CustomEventOneEventLogFormatter
			eventLogPublisher.WriteEvent(customEventOne);

			// the number of entries in the log should now be equal to 3
			Assert.AreEqual(3, log.Entries.Count, "The number of entries in the log is incorrect.");


			CustomEventTwo customEventTwo = new CustomEventTwo
				(TDEventCategory.ThirdParty, TDTraceLevel.Error,
				"A custom event two message", Environment.UserName, 3343);

			eventLogPublisher.WriteEvent(customEventTwo);

			// the number of entries in the log should now be equal to 4
			Assert.AreEqual(4, log.Entries.Count, "The number of entries in the log is incorrect.");


			// Test that Application Event Log can be written to :
			EventLogPublisher eventLogPublisherApp =
				new EventLogPublisher("App1", "Application", "TDTestSource", machineName);
			
			
			OperationalEvent oe =
				new OperationalEvent(TransportDirect.Common.Logging.TDEventCategory.Database,
				TransportDirect.Common.Logging.TDTraceLevel.Error,
				"Test error message by TD Event Logging Service");

		
			eventLogPublisherApp.WriteEvent(oe);
			

		}
	}
}
