// ******************************************************* 
// NAME                 : TestCJPCustomEvents.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/09/2003 
// DESCRIPTION  : NUnit tests for CJP Custom Event classes
// ******************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/CJPCustomEvents/TestCJPCustomEvents.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:10   mturner
//Initial revision.
//
//   Rev 1.4   Jan 24 2005 14:00:52   jgeorge
//Del 7 modifications
//
//   Rev 1.3   Jul 02 2004 15:36:56   jgeorge
//Updated tests to add queue publishing test
//
//   Rev 1.2   Jul 02 2004 13:52:28   jgeorge
//Added InternalRequestEvent
//
//   Rev 1.1   Oct 29 2003 20:13:30   geaton
//Updated properties - .dll extension no longer required for assembly properties.
//
//   Rev 1.0   Sep 12 2003 11:33:44   geaton
//Initial Revision

using System;
using NUnit.Framework;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using System.IO;
using System.Diagnostics;

namespace TransportDirect.ReportDataProvider.CJPCustomEvents
{
	[TestFixture]
	public class TestCJPCustomEvents
	{

		#region Setup, Teardown and private methods

		[SetUp]
		public void Init()
		{
			// Remove all listeners
			while (Trace.Listeners.Count > 0)
				Trace.Listeners.RemoveAt(0);
		}

		private void AddListenerWithPropertiesSet(IPropertyProvider provider)
		{
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(provider, customPublishers, errors));
			}
			catch (TDException)
			{
				string errorString = string.Empty;
				foreach (string s in errors)
					errorString = errorString + s + "\n";

				Assert.Fail(errorString);
			}
		}
		
		#endregion

		#region Tests

		/// <summary>
		/// Tests that JourneyWebRequestEvent events are published correctly by using the file publisher.
		/// </summary>
		[Test]
		public void PublishJourneyWebRequestEvent()
		{
			AddListenerWithPropertiesSet(new MockProperties());

			JourneyWebRequestEvent jwr = new JourneyWebRequestEvent("123", "999", DateTime.Now, JourneyWebRequestType.Journey, "region1", true, false);

			jwr.AuditPublishersOff = false; // so we can perform test below
			try
			{
				Trace.Write(jwr);
			}
			catch (TDException)
			{
				Assert.Fail("Unexpected error when trying to write JourneyWebRequestEvent");
			}
			
			Assert.IsTrue(jwr.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");

		}

		/// <summary>
		/// Tests that PublishLocationRequestEvent events are published correctly by using the file publisher.
		/// </summary>
		[Test]
		public void PublishLocationRequestEvent()
		{
			AddListenerWithPropertiesSet(new MockProperties());

			LocationRequestEvent lr = new LocationRequestEvent("432", JourneyPrepositionCategory.From, "admin1", "region2");

			lr.AuditPublishersOff = false; // so we can perform test below
			try
			{
				Trace.Write(lr);
			}
			catch (TDException)
			{
				Assert.Fail("Unexpected error when trying to write LocationRequestEvent to file");
			}
			
			Assert.IsTrue(lr.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
		}

		/// <summary>
		/// Tests that InternalRequestEvent events are published correctly by using the file publisher.
		/// </summary>
		[Test]
		public void PublishInternalRequestEvent()
		{
			AddListenerWithPropertiesSet(new MockProperties());

			InternalRequestEvent ir = new InternalRequestEvent("123", "999", DateTime.Now, InternalRequestType.AirTTBO, "JW", true, false);

			ir.AuditPublishersOff = false; // so we can perform test below
			try
			{
				Trace.Write(ir);
			}
			catch (TDException)
			{
				Assert.Fail("Unexpected error when trying to write InternalRequestEvent to File");
			}
			
			Assert.IsTrue(ir.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
		}

		/// <summary>
		/// Dummy test to flag that manual setup is required.
		/// The Queue referenced in MockProperties2 must be added.
		/// </summary>
		[Test]
		[Ignore("Manual setup required")]
		public void DummyTest()
		{}

		/// <summary>
		/// Tests that JourneyWebRequestEvent events are published correctly by using the queue publisher.
		/// </summary>
		[Test]
		public void PublishJourneyWebRequestEventToQueue()
		{
			AddListenerWithPropertiesSet(new MockProperties2());

			JourneyWebRequestEvent jwr = new JourneyWebRequestEvent("123", "999", DateTime.Now, JourneyWebRequestType.Journey, "region1", true, false);

			jwr.AuditPublishersOff = false; // so we can perform test below
			try
			{
				Trace.Write(jwr);
			}
			catch (TDException)
			{
				Assert.Fail("Unexpected error when trying to write JourneyWebRequestEvent to Queue");
			}
			
			Assert.AreEqual("TransportDirect.Common.Logging.QueuePublisher", jwr.PublishedBy.Trim(), "Not published as expected");
			
		}

		/// <summary>
		/// Tests that PublishLocationRequestEvent events are published correctly by using the queue publisher.
		/// </summary>
		[Test]
		public void PublishLocationRequestEventToQueue()
		{
			AddListenerWithPropertiesSet(new MockProperties2());

			LocationRequestEvent lr = new LocationRequestEvent("432", JourneyPrepositionCategory.From, "admin1", "region2");

			lr.AuditPublishersOff = false; // so we can perform test below
			try
			{
				Trace.Write(lr);
			}
			catch (TDException)
			{
				Assert.Fail("Unexpected error when trying to write LocationRequestEvent to Queue");
			}
			
			Assert.AreEqual("TransportDirect.Common.Logging.QueuePublisher", lr.PublishedBy.Trim(), "Not published as expected");
		}

		/// <summary>
		/// Tests that InternalRequestEvent events are published correctly by using the queue publisher.
		/// </summary>
		[Test]
		public void PublishInternalRequestEventToQueue()
		{
			AddListenerWithPropertiesSet(new MockProperties2());

			InternalRequestEvent ir = new InternalRequestEvent("123", "999", DateTime.Now, InternalRequestType.AirTTBO, "JW", true, false);

			ir.AuditPublishersOff = false; // so we can perform test below
			try
			{
				Trace.Write(ir);
			}
			catch (TDException)
			{
				Assert.Fail("Unexpected error when trying to write InternalRequestEvent to Queue");
			}
			
			Assert.AreEqual("TransportDirect.Common.Logging.QueuePublisher", ir.PublishedBy.Trim(), "Not published as expected");
		}

		/// <summary>
		/// Tests the validation of the function type code when creating an InternalRequestEvent.
		/// The code must be two characters - anything else should throw a specific error
		/// </summary>
		[Test]
		public void CreateInternalRequestEvent()
		{
			// The following are all invalid request codes
			string[] invalidRequestCodes = new string[] { string.Empty,
															"A",
															"ABC"};
															
			string validRequestCode = "JW";																

			// Try the invalid codes
			foreach (string s in invalidRequestCodes)
			{

				try
				{
					InternalRequestEvent ire = new InternalRequestEvent("123", "999", DateTime.Now, InternalRequestType.AirTTBO, s, true, false);
					Assert.Fail(String.Format("No error raised when creating an InternalRequestEvent with functionType = [{0}]", s));
				}
				catch (TDException e)
				{
					if (e.Identifier != TDExceptionIdentifier.ELSInvalidFunctionType)
						Assert.Fail(String.Format("Unexpected TDException with Identifier [{0}] raised when creating an InternalRequestEvent with functionType = [(1}]", e.Identifier.ToString(), s));
				}
				catch (Exception e)
				{
					Assert.Fail(String.Format("Unexpected exception with message [{0}] raised when creating an InternalRequestEvent with functionType = [(1}]", e.Message, s));
				}

			}

			// Try the valid codes
			try
			{
				InternalRequestEvent ire = new InternalRequestEvent("123", "999", DateTime.Now, InternalRequestType.AirTTBO, validRequestCode, true, false);
			}
			catch (TDException e)
			{
				Assert.Fail(String.Format("Unexpected TDException with Identifier [{0}] raised when creating an InternalRequestEvent with functionType = [(1}]", e.Identifier.ToString(), validRequestCode));
			}
			catch (Exception e)
			{
				Assert.Fail(String.Format("Unexpected exception with message [{0}] raised when creating an InternalRequestEvent with functionType = [{1}]", e.Message, validRequestCode));
			}



		}

		#endregion
	}
}

