// *********************************************** 
// NAME                 : TestCustomEventSwitch.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : NUnit test class for
// CustomEventSwitch
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestCustomEventSwitch.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:16   mturner
//Initial revision.
//
//   Rev 1.8   Feb 08 2005 13:53:44   bflenk
//Changed Assertion to Assert
//
//   Rev 1.7   Feb 07 2005 09:10:32   RScott
//Assertion changed to Assert
//
//   Rev 1.6   Apr 01 2004 16:05:38   geaton
//Changes resulting from unit testing refactoring exercise.
//
//   Rev 1.5   Aug 26 2003 15:29:42   geaton
//Added cleanup methods so that this test does not intefere with other tests.
//
//   Rev 1.4   Jul 30 2003 11:46:48   geaton
//Added ignore attribute to test that must be run independently of other tests.
//
//   Rev 1.3   Jul 25 2003 14:14:46   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).


using System.Collections;
using System;
using NUnit.Framework;
using System.Diagnostics;

using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.Logging
{
	
	[TestFixture]
	public class TestCustomEventSwitch
	{
		

		[Test]
		public void WhenTDTraceListenerAdded()
		{
			IPropertyProvider goodProperties = new MockPropertiesGood();
			IEventPublisher[] customPublishers = new IEventPublisher[2];
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");																											
			ArrayList errors = new ArrayList();
			
			Trace.Listeners.Add(new TDTraceListener(goodProperties, customPublishers, errors));
	
			Assert.IsTrue(errors.Count == 0);
			
			if (CustomEventSwitch.GlobalOn)
			{
				Console.WriteLine("NUNIT:TestCustomEventSwitch. CustomEventSwitch initialised ok for global.");
			}
			else
			{
				Assert.IsTrue(false);
			}

			if (CustomEventSwitch.On("TDEvent1"))
			{
				Console.WriteLine("NUNIT:TestCustomEventSwitch. CustomEventSwitch initialised ok for TDEvent1.");
			}
			else
			{
				Assert.IsTrue(false);
			}

			if (CustomEventSwitch.Off("TDEvent2"))
			{
				Console.WriteLine("NUNIT:TestCustomEventSwitch. CustomEventSwitch initialised ok for TDEvent2.");
			}
			else
			{
				Assert.IsTrue(false);
			}

		}

		[Test]
		public void WhenTDTraceListenerAddedBadEventTested()
		{
			bool exceptionThrown = false;

			IPropertyProvider goodProperties = new MockPropertiesGood();
			IEventPublisher[] customPublishers = new IEventPublisher[2];
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");																											
			ArrayList errors = new ArrayList();
			
			Trace.Listeners.Add(new TDTraceListener(goodProperties, customPublishers, errors));
	
			Assert.IsTrue(errors.Count == 0);
			
			try
			{
				if (CustomEventSwitch.On("TDEvent3"))
				{
					Assert.IsTrue(false);
				}
			}
			catch (TDException e)
			{
				exceptionThrown = true;
				string message = "NUNIT:TestCustomEventSwitch. Exception thrown correctly with message: " + e.Message;
				Console.WriteLine(message);
			}

			Assert.IsTrue(exceptionThrown);

			
		}

		[SetUp]
		public void SetUp()
		{
			Trace.Listeners.Remove("TDTraceListener");
		}

		[TearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
		}

	}
}
