// *********************************************** 
// NAME                 : TestQueuePublisher.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 07/07/2003 
// DESCRIPTION  : NUnit test for the
// QueuePublisher class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestQueuePublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:20   mturner
//Initial revision.
//
//   Rev 1.8   Feb 08 2005 15:06:20   bflenk
//Changed Assertion to Assert
//
//   Rev 1.7   Apr 01 2004 16:05:42   geaton
//Changes resulting from unit testing refactoring exercise.
//
//   Rev 1.6   Oct 29 2003 20:55:00   geaton
//Expected results changed followinf changes to formatters of mock custom events
//
//   Rev 1.5   Jul 30 2003 18:08:42   geaton
//Changes to OperationalEvent constructors
//
//   Rev 1.4   Jul 30 2003 10:28:08   geaton
//Added code to create queue so that it does not have to be done manually prior to running test.
//
//   Rev 1.3   Jul 30 2003 08:46:14   geaton
//Removed misleading comments
//
//   Rev 1.2   Jul 29 2003 17:32:00   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.1   Jul 25 2003 14:15:02   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;
using System.Messaging;
using NUnit.Framework;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// NUnit test class for QueuePublisher
	/// </summary>
	[TestFixture]
	public class TestQueuePublisher
	{
		[Test]
		public void TestWriteEvent()
		{	
			// ---------------------------------------------

			// perform initialisation

			
			string queuePath = Environment.MachineName + "\\Private$\\event_test_queue$";

			// Create the (nontransactional) queue if it does not exist.
			if (!MessageQueue.Exists(queuePath))
			{
				MessageQueue newQueue = MessageQueue.Create(queuePath, false);
			}

			// empty the queue in case any existing events exist
			MessageQueue queue = new MessageQueue(queuePath);
			queue.Formatter = new BinaryMessageFormatter();
			queue.Purge();

			// ---------------------------------------------

			// create test data - 16 operational events and 2 custom events

			OperationalEvent[] operationalEvents =
				new OperationalEvent[16];

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

			operationalEvents[15] = new OperationalEvent(
				TDEventCategory.Database,
				TDTraceLevel.Info,
				"Test Error 16: D123 failed",
				"Test Target Number 16");


			// create custom events
			CustomEventOne customEventOne = new CustomEventOne
				(TDEventCategory.Business, TDTraceLevel.Warning,
				"A custom event one message", Environment.UserName, 12345);

			CustomEventTwo customEventTwo = new CustomEventTwo
				(TDEventCategory.ThirdParty, TDTraceLevel.Error,
				"A custom event two message", Environment.UserName, 3343);

			// ---------------------------------------------

			// create a new queue publisher
			QueuePublisher queuePublisher =
				new QueuePublisher("Identifier", MessagePriority.Normal, queuePath, true);

			// call queuePublisher's WriteEvent() for each of the operational events
			foreach(OperationalEvent oe in operationalEvents)
			{
				queuePublisher.WriteEvent(oe);
			}

			// write custom events to queue
			queuePublisher.WriteEvent(customEventOne);
			queuePublisher.WriteEvent(customEventTwo);

			// ---------------------------------------------

			// expect the queue to have 18 messages
			Assert.AreEqual(18, queue.GetAllMessages().Length, "Queue does not have 18 messages");

			// test each message from the queue
			OperationalEvent actualEvent, expectedEvent;
		
			for(int i=0; i<16; i++)
			{
				expectedEvent = operationalEvents[i];
				actualEvent = (OperationalEvent)queue.Receive().Body;

				// Assert that there is one less item in the queue
				Assert.AreEqual((18 - (i+1)), queue.GetAllMessages().Length, "Number of messages in queue is incorrect.");

				// compare the OperationalEvent from the queue with the expected event
				Assert.AreEqual(expectedEvent.Category.ToString(), actualEvent.Category.ToString(),"Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.ConsoleFormatter, actualEvent.ConsoleFormatter, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.EmailFormatter, actualEvent.EmailFormatter, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.EventLogFormatter, actualEvent.EventLogFormatter, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.FileFormatter, actualEvent.FileFormatter, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.Filter, actualEvent.Filter, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.Level, actualEvent.Level, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.Message, actualEvent.Message, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.MethodName, actualEvent.MethodName, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.SessionId, actualEvent.SessionId, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.Target, actualEvent.Target, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.Time, actualEvent.Time, "Message Number:" + (i + 1));
				Assert.AreEqual(expectedEvent.TypeName, actualEvent.TypeName, "");
			}

			// expect the queue to have 2 messages left
			// i.e., the two custom events
			Assert.AreEqual(2, queue.GetAllMessages().Length, "Queue does not have 2 messages");

			// test custom event one

			CustomEventOne actualCustomEventOne =
				(CustomEventOne)queue.Receive().Body;

			Assert.AreEqual
				(customEventOne.Category.ToString(), actualCustomEventOne.Category.ToString(), "Custom Event One");

			Assert.AreEqual
				(customEventOne.ConsoleFormatter, actualCustomEventOne.ConsoleFormatter, "Custom Event One");

			Assert.AreEqual
				(customEventOne.DefaultFormatter, actualCustomEventOne.DefaultFormatter, "Custom Event One");

			Assert.AreEqual
				(customEventOne.EmailFormatter, actualCustomEventOne.EmailFormatter, "Custom Event One");

			Assert.AreEqual
				(customEventOne.EventLogFormatter, actualCustomEventOne.EventLogFormatter, "Custom Event One");

			Assert.AreEqual
				(customEventOne.FileFormatter, actualCustomEventOne.FileFormatter, "Custom Event One");

			Assert.AreEqual
				(customEventOne.Filter, actualCustomEventOne.Filter, "Custom Event One");

			Assert.AreEqual
				(customEventOne.Level, actualCustomEventOne.Level, "Custom Event One");

			Assert.AreEqual
				(customEventOne.Message, actualCustomEventOne.Message, "Custom Event One");

			Assert.AreEqual
				(customEventOne.ReferenceNumber, actualCustomEventOne.ReferenceNumber, "Custom Event One");

			Assert.AreEqual
				(customEventOne.Time, actualCustomEventOne.Time, "Custom Event One");
				
			// test custom event two

			CustomEventTwo actualCustomEventTwo =
				(CustomEventTwo)queue.Receive().Body;

			Assert.AreEqual
				(customEventTwo.Category.ToString(), actualCustomEventTwo.Category.ToString(), "Custom Event Two");

			Assert.AreEqual
				(actualCustomEventTwo.ConsoleFormatter, customEventTwo.ConsoleFormatter, "Custom Event Two");

			Assert.AreEqual
				(customEventTwo.DefaultFormatter, customEventTwo.DefaultFormatter, "Custom Event Two");

			Assert.AreEqual
				(actualCustomEventTwo.EmailFormatter, customEventTwo.EmailFormatter, "Custom Event Two");

			Assert.AreEqual
				(actualCustomEventTwo.EventLogFormatter, customEventTwo.EventLogFormatter, "Custom Event Two");

			Assert.AreEqual
				(actualCustomEventTwo.FileFormatter, customEventTwo.FileFormatter, "Custom Event Two");

			Assert.AreEqual
				(customEventTwo.Filter, actualCustomEventTwo.Filter, "Custom Event Two");

			Assert.AreEqual
				(customEventTwo.Level, actualCustomEventTwo.Level, "Custom Event Two");

			Assert.AreEqual
				(customEventTwo.Message, actualCustomEventTwo.Message, "Custom Event Two");

			Assert.AreEqual
				(customEventTwo.ReferenceNumber, actualCustomEventTwo.ReferenceNumber, "Custom Event Two");

			Assert.AreEqual
				(customEventTwo.Time, actualCustomEventTwo.Time, "Custom Event Two");

			// cleanup
			queue.Purge();
		}

		/// <remarks>
		/// Manual setup:
		/// MSMQ must be installed on test machine.
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}
	}
}
