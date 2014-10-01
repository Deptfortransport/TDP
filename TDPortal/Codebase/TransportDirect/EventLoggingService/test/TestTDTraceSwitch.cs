// *********************************************** 
// NAME                 : TestTDTraceSwitch.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : NUnit test for the
// TDTraceSwitch class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestTDTraceSwitch.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:22   mturner
//Initial revision.
//
//   Rev 1.6   Feb 08 2005 15:15:14   bflenk
//Changed Assertion to Assert
//
//   Rev 1.5   Apr 01 2004 16:05:46   geaton
//Changes resulting from unit testing refactoring exercise.
//
//   Rev 1.4   Jul 30 2003 11:46:46   geaton
//Added ignore attribute to test that must be run independently of other tests.
//
//   Rev 1.3   Jul 25 2003 14:15:04   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;
using NUnit.Framework;
using System.Diagnostics;
using System.Collections;

using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Summary description for TestTDTraceSwitch.
	/// </summary>
	[TestFixture]
	public class TestTDTraceSwitch
	{
		
		[Test]
		public void WhenTDTraceListenerAdded()
		{
			IPropertyProvider goodProperties = new MockPropertiesGood();
			IEventPublisher[] customPublishers = new IEventPublisher[2];
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");																											
			ArrayList errors = new ArrayList();
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties,customPublishers, errors));

				if (TDTraceSwitch.TraceError)
				{
					Console.WriteLine("NUNIT:TestTDTraceSwitch. TDTraceSwitch initialised ok.");
				}
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			Assert.IsTrue(errors.Count == 0);

		}

	}
}
