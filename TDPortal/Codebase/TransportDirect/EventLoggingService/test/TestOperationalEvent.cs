// *********************************************** 
// NAME                 : TestOperationalEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 29/07/2003 
// DESCRIPTION  : NUnit test class for
// OperationalEvent
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestOperationalEvent.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:20   mturner
//Initial revision.
//
//   Rev 1.3   Feb 08 2005 14:11:32   bflenk
//Changed Assertion to Assert
//
//   Rev 1.2   Jun 30 2004 17:04:44   jgeorge
//Changes for "force logging" functionality
//
//   Rev 1.1   Jul 30 2003 18:08:40   geaton
//Changes to OperationalEvent constructors
//
//   Rev 1.0   Jul 29 2003 17:23:04   geaton
//Initial Revision


using System;
using NUnit.Framework;
using System.Collections;
using System.Diagnostics;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.Logging
{
	[TestFixture]
	public class TestOperationalEvent
	{
		[SetUp]
		public void Init()
		{
			IPropertyProvider goodProperties = new MockPropertiesGood();
			IEventPublisher[] customPublishers = new IEventPublisher[2];
			customPublishers[0] = new TDPublisher1("CustomPublisher1");
			customPublishers[1] = new TDPublisher2("CustomPublisher2");																											
			ArrayList errors = new ArrayList();
			
			try
			{
				Trace.Listeners.Add(new TDTraceListener(goodProperties, customPublishers, errors));
			}
			catch (TDException e)
			{
				Assert.Fail("Initialisation of TDTraceListener failed - " + e.Message);
			}

		}

		[Test]
		public void ConstructorAllParams()
		{
			string sessionId = "11";
			string message = "Message";
			ArrayList target = new ArrayList();
			string targetString = "TargetString";
			target.Add("TargetString");

			OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message, target, sessionId);

			Assert.IsTrue(oe.AssemblyName == "td.common.logging");
			Assert.IsTrue(oe.AuditPublishersOff == true);
			Assert.IsTrue(oe.Category == TDEventCategory.Business);
			Assert.IsTrue(oe.ConsoleFormatter != null);
			Assert.IsTrue(oe.EmailFormatter != null);
			Assert.IsTrue(oe.EventLogFormatter != null);
			Assert.IsTrue(oe.FileFormatter != null);
			Assert.IsTrue(oe.Filter != null);
			Assert.IsTrue(oe.Level == TDTraceLevel.Verbose);
			Assert.IsTrue(oe.MachineName == Environment.MachineName);
			Assert.IsTrue(oe.Message == message);
			Assert.IsTrue(oe.MethodName == "ConstructorAllParams");
			Assert.IsTrue(oe.PublishedBy == String.Empty);
			Assert.IsTrue(oe.SessionId == sessionId);
			ArrayList storedTarget = (ArrayList)oe.Target;
			foreach (string targetElement in storedTarget)
				Assert.IsTrue(targetElement == targetString);
			Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
			Assert.IsTrue(oe.Filter.ShouldLog(oe) == false, "oe.Filter.ShouldLog returned true");

			// Now reinitialise using the User override level. The only difference is that the 
			// Filter.ShouldLog method should return True
			oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message, target, sessionId, TDTraceLevelOverride.User);

			Assert.IsTrue(oe.AssemblyName == "td.common.logging");
			Assert.IsTrue(oe.AuditPublishersOff == true);
			Assert.IsTrue(oe.Category == TDEventCategory.Business);
			Assert.IsTrue(oe.ConsoleFormatter != null);
			Assert.IsTrue(oe.EmailFormatter != null);
			Assert.IsTrue(oe.EventLogFormatter != null);
			Assert.IsTrue(oe.FileFormatter != null);
			Assert.IsTrue(oe.Filter != null);
			Assert.IsTrue(oe.Level == TDTraceLevel.Verbose);
			Assert.IsTrue(oe.MachineName == Environment.MachineName);
			Assert.IsTrue(oe.Message == message);
			Assert.IsTrue(oe.MethodName == "ConstructorAllParams");
			Assert.IsTrue(oe.PublishedBy == String.Empty);
			Assert.IsTrue(oe.SessionId == sessionId);
			storedTarget = (ArrayList)oe.Target;
			foreach (string targetElement in storedTarget)
				Assert.IsTrue(targetElement == targetString);
			Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
			Assert.IsTrue(oe.Filter.ShouldLog(oe), "oe.Filter.ShouldLog returned false");

		}

		[Test]
		public void ConstructorWithoutSessionId()
		{
			string message = "Message";
			ArrayList target = new ArrayList();
			string targetString = "TargetString";
			target.Add("TargetString");

			OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message, target );

			Assert.IsTrue(oe.AssemblyName == "td.common.logging");
			Assert.IsTrue(oe.AuditPublishersOff == true);
			Assert.IsTrue(oe.Category == TDEventCategory.Business);
			Assert.IsTrue(oe.ConsoleFormatter != null);
			Assert.IsTrue(oe.EmailFormatter != null);
			Assert.IsTrue(oe.EventLogFormatter != null);
			Assert.IsTrue(oe.FileFormatter != null);
			Assert.IsTrue(oe.Filter != null);
			Assert.IsTrue(oe.Level == TDTraceLevel.Verbose);
			Assert.IsTrue(oe.MachineName == Environment.MachineName);
			Assert.IsTrue(oe.Message == message);
			Assert.IsTrue(oe.MethodName == "ConstructorWithoutSessionId");
			Assert.IsTrue(oe.PublishedBy == String.Empty);
			Assert.IsTrue(oe.SessionId == OperationalEvent.SessionIdUnassigned);
			ArrayList storedTarget = (ArrayList)oe.Target;
			foreach (string targetElement in storedTarget)
				Assert.IsTrue(targetElement == targetString);
			Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
			Assert.IsTrue(oe.Filter.ShouldLog(oe) == false, "oe.Filter.ShouldLog returned true");

			// Now reinitialise using the User override level. The only difference is that the 
			// Filter.ShouldLog method should return True
			oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message, target, TDTraceLevelOverride.User );

			Assert.IsTrue(oe.AssemblyName == "td.common.logging");
			Assert.IsTrue(oe.AuditPublishersOff == true);
			Assert.IsTrue(oe.Category == TDEventCategory.Business);
			Assert.IsTrue(oe.ConsoleFormatter != null);
			Assert.IsTrue(oe.EmailFormatter != null);
			Assert.IsTrue(oe.EventLogFormatter != null);
			Assert.IsTrue(oe.FileFormatter != null);
			Assert.IsTrue(oe.Filter != null);
			Assert.IsTrue(oe.Level == TDTraceLevel.Verbose);
			Assert.IsTrue(oe.MachineName == Environment.MachineName);
			Assert.IsTrue(oe.Message == message);
			Assert.IsTrue(oe.MethodName == "ConstructorWithoutSessionId");
			Assert.IsTrue(oe.PublishedBy == String.Empty);
			Assert.IsTrue(oe.SessionId == OperationalEvent.SessionIdUnassigned);
			storedTarget = (ArrayList)oe.Target;
			foreach (string targetElement in storedTarget)
				Assert.IsTrue(targetElement == targetString);
			Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
			Assert.IsTrue(oe.Filter.ShouldLog(oe), "oe.Filter.ShouldLog returned false");
		}

		[Test]
		public void ConstructorWithoutTarget()
		{
			string sessionId = "11";
			string message = "Message";
			
			OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, sessionId, TDTraceLevel.Verbose, message);

			Assert.IsTrue(oe.AssemblyName == "td.common.logging");
			Assert.IsTrue(oe.AuditPublishersOff == true);
			Assert.IsTrue(oe.Category == TDEventCategory.Business);
			Assert.IsTrue(oe.ConsoleFormatter != null);
			Assert.IsTrue(oe.EmailFormatter != null);
			Assert.IsTrue(oe.EventLogFormatter != null);
			Assert.IsTrue(oe.FileFormatter != null);
			Assert.IsTrue(oe.Filter != null);
			Assert.IsTrue(oe.Level == TDTraceLevel.Verbose);
			Assert.IsTrue(oe.MachineName == Environment.MachineName);
			Assert.IsTrue(oe.Message == message);
			Assert.IsTrue(oe.MethodName == "ConstructorWithoutTarget");
			Assert.IsTrue(oe.PublishedBy == String.Empty);
			Assert.IsTrue(oe.SessionId == sessionId);
			Assert.IsTrue(oe.Target == null);
			Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
			Assert.IsTrue(oe.Filter.ShouldLog(oe) == false, "oe.Filter.ShouldLog returned true");

			// Now reinitialise using the User override level. The only difference is that the 
			// Filter.ShouldLog method should return True
			oe = new OperationalEvent(TDEventCategory.Business, sessionId, TDTraceLevel.Verbose, message, TDTraceLevelOverride.User );

			Assert.IsTrue(oe.AssemblyName == "td.common.logging");
			Assert.IsTrue(oe.AuditPublishersOff == true);
			Assert.IsTrue(oe.Category == TDEventCategory.Business);
			Assert.IsTrue(oe.ConsoleFormatter != null);
			Assert.IsTrue(oe.EmailFormatter != null);
			Assert.IsTrue(oe.EventLogFormatter != null);
			Assert.IsTrue(oe.FileFormatter != null);
			Assert.IsTrue(oe.Filter != null);
			Assert.IsTrue(oe.Level == TDTraceLevel.Verbose);
			Assert.IsTrue(oe.MachineName == Environment.MachineName);
			Assert.IsTrue(oe.Message == message);
			Assert.IsTrue(oe.MethodName == "ConstructorWithoutTarget");
			Assert.IsTrue(oe.PublishedBy == String.Empty);
			Assert.IsTrue(oe.SessionId == sessionId);
			Assert.IsTrue(oe.Target == null);
			Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
			Assert.IsTrue(oe.Filter.ShouldLog(oe), "oe.Filter.ShouldLog returned false");
		}


		[Test]
		public void ConstructorMinParams()
		{
			string message = "Message";
			
			OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message);

			Assert.IsTrue(oe.AssemblyName == "td.common.logging");
			Assert.IsTrue(oe.AuditPublishersOff == true);
			Assert.IsTrue(oe.Category == TDEventCategory.Business);
			Assert.IsTrue(oe.ConsoleFormatter != null);
			Assert.IsTrue(oe.EmailFormatter != null);
			Assert.IsTrue(oe.EventLogFormatter != null);
			Assert.IsTrue(oe.FileFormatter != null);
			Assert.IsTrue(oe.Filter != null);
			Assert.IsTrue(oe.Level == TDTraceLevel.Verbose);
			Assert.IsTrue(oe.MachineName == Environment.MachineName);
			Assert.IsTrue(oe.Message == message);
			Assert.IsTrue(oe.MethodName == "ConstructorMinParams");
			Assert.IsTrue(oe.PublishedBy == String.Empty);
			Assert.IsTrue(oe.SessionId == OperationalEvent.SessionIdUnassigned);
			Assert.IsTrue(oe.Target == null);
			Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
			Assert.IsTrue(oe.Filter.ShouldLog(oe) == false, "oe.Filter.ShouldLog returned true");

			// Now reinitialise using the User override level. The only difference is that the 
			// Filter.ShouldLog method should return True
			oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, message, TDTraceLevelOverride.User );

			Assert.IsTrue(oe.AssemblyName == "td.common.logging");
			Assert.IsTrue(oe.AuditPublishersOff == true);
			Assert.IsTrue(oe.Category == TDEventCategory.Business);
			Assert.IsTrue(oe.ConsoleFormatter != null);
			Assert.IsTrue(oe.EmailFormatter != null);
			Assert.IsTrue(oe.EventLogFormatter != null);
			Assert.IsTrue(oe.FileFormatter != null);
			Assert.IsTrue(oe.Filter != null);
			Assert.IsTrue(oe.Level == TDTraceLevel.Verbose);
			Assert.IsTrue(oe.MachineName == Environment.MachineName);
			Assert.IsTrue(oe.Message == message);
			Assert.IsTrue(oe.MethodName == "ConstructorMinParams");
			Assert.IsTrue(oe.PublishedBy == String.Empty);
			Assert.IsTrue(oe.SessionId == OperationalEvent.SessionIdUnassigned);
			Assert.IsTrue(oe.Target == null);
			Assert.IsTrue(oe.Time.Year == DateTime.Now.Year);
			Assert.IsTrue(oe.Filter.ShouldLog(oe), "oe.Filter.ShouldLog returned false");

		}

	}
}
