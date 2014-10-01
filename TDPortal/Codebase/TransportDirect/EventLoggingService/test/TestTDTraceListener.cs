// *********************************************** 
// NAME                 : TestTDTraceListener.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : NUnit test for the
// TDTraceListner class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestTDTraceListener.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:20   mturner
//Initial revision.
//
//   Rev 1.16   Feb 08 2005 15:12:30   bflenk
//Changed Assertion to Assert
//
//   Rev 1.15   Apr 01 2004 16:05:44   geaton
//Changes resulting from unit testing refactoring exercise.
//
//   Rev 1.14   Nov 10 2003 17:59:50   geaton
//Removed some tests following change to TDTraceListener.OnSuperseded which now utilises Properties.Current. These tests will require re-writing so that tests setup Properties.Current for use.
//
//   Rev 1.13   Oct 09 2003 13:11:40   geaton
//Support for error handling should an unknown event class be logged.
//
//   Rev 1.12   Sep 16 2003 15:05:04   geaton
//Updated expected result following custom event publishers validation bug-fix.
//
//   Rev 1.11   Sep 12 2003 14:13:48   geaton
//Added extra tests for trace level changes
//
//   Rev 1.10   Sep 04 2003 18:50:54   geaton
//Tests added for TDTraceListener prototypes.
//
//   Rev 1.9   Aug 22 2003 14:56:42   geaton
//Clarified comments
//
//   Rev 1.8   Jul 30 2003 18:08:42   geaton
//Changes to OperationalEvent constructors
//
//   Rev 1.7   Jul 30 2003 12:34:34   geaton
//Added to test to check outcome when default publisher fails.
//
//   Rev 1.6   Jul 29 2003 17:32:00   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.5   Jul 28 2003 18:19:34   geaton
//TDTraceListenerParallelUsage test added to perform tests that use the default trace and debug listeners in parallel with a TDTraceListener.
//
//   Rev 1.4   Jul 25 2003 14:15:02   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;
using System.Diagnostics;
using System.Collections;
using NUnit.Framework;
using System.IO;
using System.Messaging;
using System.Drawing;
using System.Drawing.Imaging;

using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.Common.Logging
{

	/// <summary>
	/// Summary description for TestTDTraceListener.
	/// </summary>
	[TestFixture]
	public class TestTDTraceListener
	{

		[Test]
		public void TDTraceListenerUnsupportedPrototypes()
		{
			Cleanup();

			IPropertyProvider MockPropertiesGoodMinimumProperties = new MockPropertiesGoodMinimumProperties();
			IEventPublisher[] customPublishers = new IEventPublisher[0];																							
			ArrayList errors = new ArrayList();
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(MockPropertiesGoodMinimumProperties,customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			Assert.IsTrue(errors.Count == 0);

			// call each unsupported prototype - no exceptions should result
			try
			{
				Exception testObject = new Exception("test object");
				Trace.Write("My call to Write(string)");
				Trace.Write((object)testObject,"My call to Write(object,string)");
				Trace.Write("My message for Write(string,string)", "My category for Write(string, string)");
				Trace.WriteLine("My call to WriteLine(string)");
				Trace.WriteLine((object)testObject,"My call to WriteLine(object,string)");
				Trace.WriteLine("My message for WriteLine(string,string)", "My category for WriteLine(string, string)");
				Trace.WriteLine((object)testObject);
			}
			catch(Exception)
			{
				Assert.IsTrue(false);
			}

		
			DirectoryInfo publisherDir = new DirectoryInfo(MockPropertiesGoodMinimumProperties[String.Format(Keys.FilePublisherDirectory, "File1")] + "\\");
			FileInfo[] fileInfoArray = publisherDir.GetFiles("*.txt");

			Assert.IsTrue(fileInfoArray.Length > 0);

			FileInfo tempFile = fileInfoArray[0];
			FileStream fileStream = tempFile.OpenRead();
			StreamReader streamReader = new StreamReader(fileStream);

			int count = 0;
			while (streamReader.ReadLine() != null)
				count++;

			streamReader.Close();
			fileStream.Close();

			Assert.IsTrue(count==7); // one per prototype call

		}


		[Test]
		public void TDTraceListenerEmptyProperties()
		{
			bool exceptionThrown = false;
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(new MockPropertiesEmpty(), customPublishers, errors));
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}		

			if (!exceptionThrown)
				Assert.IsTrue(false);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestTDTraceListener. " + error;
				Console.WriteLine(message);
			}

			Assert.IsTrue(errors.Count == 6);
		}

		[Test]
		public void TDTraceListenerEmptyValueProperties()
		{
			bool exceptionThrown = false;
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(new MockPropertiesEmptyValues(), customPublishers, errors));
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}		

			if (!exceptionThrown)
				Assert.IsTrue(false);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestTDTraceListener. " + error;
				Console.WriteLine(message);
			}

			Assert.IsTrue(errors.Count == 8);

		}
		
		[Test]
		public void TDTraceListenerMinimumProperties()
		{
			IPropertyProvider MockPropertiesGoodMinimumProperties = new MockPropertiesGoodMinimumProperties();
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(MockPropertiesGoodMinimumProperties,
														customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			Assert.IsTrue(errors.Count == 0);		
		}

		[Test]
		public void TDTraceListenerAllGood()
		{
			// NB. In proper use, an instance of Properties would be
			// retrieved from the Service Discovery (? or other means)
			// and then IPropertyProvider goodProperties = Properties.Current;

			IPropertyProvider goodProperties = new MockPropertiesGood();

			IEventPublisher[] customPublishers = new IEventPublisher[2];
			
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");						
																							
			ArrayList errors = new ArrayList();
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties,
														customPublishers,
														errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			Assert.IsTrue(errors.Count == 0);
		}

		[Test]
		public void TDTraceListenerPublisherNotPassed()
		{
			bool exceptionThrown = false;

			IPropertyProvider goodProperties = new MockPropertiesGood();
			ArrayList errors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[1];

			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			// do not pass a TDPublisher2 even though it is defined in goodProperties

			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties, customPublishers, errors));
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
				Assert.IsTrue(false);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestTDTraceListener. " + error;
				Console.WriteLine(message);
			}

			Assert.IsTrue(errors.Count == 1);
		}

		[Test]
		public void TDTraceListenerUnknownCustomPublisherClassPassed()
		{
			bool exceptionThrown = false;
			ArrayList errors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[3];

			IPropertyProvider goodProperties = new MockPropertiesGood();

			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");
			// pass in a custom publisher that does not appear in good properties
			customPublishers[2] = new TDPublisher3("CustomPublisher3");

			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties, customPublishers, errors));
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
				Assert.IsTrue(false);

			Assert.IsTrue(errors.Count == 1);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestTDTraceListener. " + error;
				Console.WriteLine(message);
			}

		}

		[Test]
		public void TDTraceListenerUnknownCustomPublisherIDPassed()
		{
			bool exceptionThrown = false;
			ArrayList errors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[2];

			IPropertyProvider goodProperties = new MockPropertiesGood();

			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			// intialise with a different ID to that specified in good properties
			customPublishers[1] = new TDPublisher2("DifferentIDToProperties");
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties, customPublishers, errors));
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
				Assert.IsTrue(false);

			Assert.IsTrue(errors.Count == 1);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestTDTraceListener. " + error;
				Console.WriteLine(message);
			}

		}

		
		[Test]
		public void TDTraceListenerUnknownCustomPublisherClassKnownIDPassed()
		{
			bool exceptionThrown = false;
			ArrayList errors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[2];

			IPropertyProvider goodProperties = new MockPropertiesGood();

			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			// provide unknown class with an id that IS specified in good properties
			customPublishers[1] = new TDPublisher3("CustomPublisher2");
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties, customPublishers, errors));
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
				Assert.IsTrue(false);

			Assert.IsTrue(errors.Count == 1);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestTDTraceListener. " + error;
				Console.WriteLine(message);
			}

		}

		[Test]
		public void TDTraceListenerNoCustomPublishersPassed()
		{
			bool exceptionThrown = false;
			ArrayList errors = new ArrayList();
			IPropertyProvider goodProperties = new MockPropertiesGood();
			IEventPublisher[] customPublishers = new IEventPublisher[0];

			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties, customPublishers, errors));
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
				Assert.IsTrue(false);

			Assert.IsTrue(errors.Count == 2);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestTDTraceListener. " + error;
				Console.WriteLine(message);
			}

		}

		[Test]
		public void TDTraceListenerNullArrayCustomPublishersPassed()
		{
			bool exceptionThrown = false;
			ArrayList errors = new ArrayList();
			IPropertyProvider goodProperties = new MockPropertiesGood();

			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties, null, errors));
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
				Assert.IsTrue(false);

			Assert.IsTrue(errors.Count == 1);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestTDTraceListener. " + error;
				Console.WriteLine(message);
			}

		}


		[Test]
		public void TDTraceListenerBadCustomEvents()
		{
			bool exceptionThrown = false;
			ArrayList errors = new ArrayList();
			IPropertyProvider badCustomEvents = new MockPropertiesGoodPublishersBadEvents();
			IEventPublisher[] customPublishers = new IEventPublisher[2];
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");	

			try
			{
				Trace.Listeners.Add(new TDTraceListener(badCustomEvents, customPublishers, errors));
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}

			if (!exceptionThrown)
				Assert.IsTrue(false);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestTDTraceListener. " + error;
				Console.WriteLine(message);
			}

			Assert.IsTrue(errors.Count == 21);
		}


		[Test]
		public void TDTraceListenerEventWriting()
		{
			MockPropertiesGood goodProperties = new MockPropertiesGood();

			IEventPublisher[] customPublishers = new IEventPublisher[2];
			
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");						
																							
			ArrayList errors = new ArrayList();
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties,
					customPublishers,
					errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			OperationalEvent oe1 = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Message",  "11");
			oe1.AuditPublishersOff = false; // so we can perform test below
			Trace.Write(oe1);
			Assert.IsTrue(oe1.PublishedBy == "TransportDirect.Common.Logging.QueuePublisher TransportDirect.Common.Logging.EventLogPublisher ");

			TDEvent1 tdEvent1 = new TDEvent1("MyUser", 111, 111);
			tdEvent1.AuditPublishersOff = false; // so we can perform test below
			Trace.Write(tdEvent1);
			Assert.IsTrue(tdEvent1.PublishedBy == "TransportDirect.Common.Logging.TDPublisher1 TransportDirect.Common.Logging.QueuePublisher ");
			
			// test operational event level switch works -  following event should not be published because level is Error
			OperationalEvent oe2 = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "Message", "11");
			oe2.AuditPublishersOff = false; // so we can perform test below
			Trace.Write(oe2);
			Assert.IsTrue(oe2.PublishedBy == "");
		}

		
		[Test]
		public void TDTraceListenerParallelUsage()
		{
			MockPropertiesGood goodProperties = new MockPropertiesGood();

			IEventPublisher[] customPublishers = new IEventPublisher[2];
			
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");						
																							
			ArrayList errors = new ArrayList();
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties,
									customPublishers,
									errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			bool badCall = false;

			try
			{
				// call Write overload that is not implemented in TDTraceListener
				string test = "TestTDTraceListener.TDTraceListenerParallelUsage. Test message - called using Write overload that is not implemented in TDTraceListener, but is implemented in default listener.\n";
				Trace.Write(test);
			}
			catch (TDException)
			{
				badCall = true;	
			}

			Assert.IsTrue(!badCall);

			badCall = false;

			try
			{
				// call WriteLine - not implemented in TDTraceListener
				string test = "TestTDTraceListener.TDTraceListenerParallelUsage. Test Message - called using WriteLine - this is not implemented in TDTraceListener, but is implemented in default listener.";
				Trace.WriteLine(test);
			}
			catch (TDException)
			{
				badCall = true;	
			}

			Assert.IsTrue(!badCall);

			// ensure that TDTraceListener does not effect any Debug calls
			try
			{
				string test = "TestTDTraceListener.TDTraceListenerParallelUsage. Test message (using Debug, with TDTraceListener registered).";
				Debug.WriteLine(test);
			}
			catch (Exception)
			{
				Assert.IsTrue(true);
			}


			// remove TDTraceListener - above calls should now work without exception being throw
			Trace.Listeners.Remove("TDTraceListener");

			badCall = false;

			try
			{
				// call Write overload that is not implemented in TDTraceListener
				string test = "TestTDTraceListener.TDTraceListenerParallelUsage. Test message (TDTraceListener not registered).";
				Trace.Write(test);
			}
			catch (TDException)
			{
				badCall = true;	
			}

			Assert.IsTrue(!badCall);
		}

		/// <summary>
		/// Test handling when an unknown object is logged.
		/// </summary>
		[Test]
		public void TDTraceListenerUnknownObject()
		{
			// set up tdtracelistener with valid properties
			IPropertyProvider goodProperties = new MockPropertiesGood();
			IEventPublisher[] customPublishers = new IEventPublisher[2];
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");											
			ArrayList errors = new ArrayList();
			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties,
									customPublishers,
									errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			bool badEventFound = false;
			try
			{
				// try logging TDEvent3 - this does not derive from CustomEvent
				Trace.Write(new TDEvent3());
			}
			catch (TDException)
			{
				badEventFound = true;
			}
			
			Assert.IsTrue(badEventFound);
		}

		/// <summary>
		/// Test handling when a custom event is logged, which is unknown to the TDTraceListener
		/// </summary>
		[Test]
		public void TDTraceListenerUnknownCustomEvent()
		{
			// set up tdtracelistener with valid properties
			IPropertyProvider goodProperties = new MockPropertiesGood();
			IEventPublisher[] customPublishers = new IEventPublisher[2];
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");											
			ArrayList errors = new ArrayList();
			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties,
					customPublishers,
					errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			// attempt to log an event that the tdtracelistener knows nothing about:
			bool badEventFound = false;
			try
			{
				// try logging TDEvent4 - this derives from CustomEvent but is
				// not known by TDTraceListener
				Trace.Write(new TDEvent4());
			}
			catch (TDException)
			{
				badEventFound = true;
			}
			
			Assert.IsTrue(badEventFound);
		}

		[SetUp]
		public void SetUp()
		{
			Trace.Listeners.Remove("TDTraceListener");

			// delete file publisher dirs
			IPropertyProvider MockPropertiesGood = new MockPropertiesGood();

			DirectoryInfo di1 = new DirectoryInfo(MockPropertiesGood[String.Format(Keys.FilePublisherDirectory, "File1")]);
			di1.Delete(true);

			DirectoryInfo di2 = new DirectoryInfo(MockPropertiesGood[String.Format(Keys.FilePublisherDirectory, "File2")]);
			di2.Delete(true);
		}


		[TearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");

			// delete file publisher dirs
			IPropertyProvider MockPropertiesGood = new MockPropertiesGood();

			DirectoryInfo di1 = new DirectoryInfo(MockPropertiesGood[String.Format(Keys.FilePublisherDirectory, "File1")]);
			di1.Delete(true);

			DirectoryInfo di2 = new DirectoryInfo(MockPropertiesGood[String.Format(Keys.FilePublisherDirectory, "File2")]);
			di2.Delete(true);

		}

	}
}
