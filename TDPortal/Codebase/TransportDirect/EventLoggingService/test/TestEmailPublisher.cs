// *********************************************** 
// NAME                 : TestEmailPublisher.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 07/07/2003 
// DESCRIPTION  : NUnit test for the
// EmailPublisher class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestEmailPublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:16   mturner
//Initial revision.
//
//   Rev 1.10   Apr 01 2004 16:05:48   geaton
//Changes resulting from unit testing refactoring exercise.
//
//   Rev 1.9   Mar 23 2004 13:21:34   ACaunt
//sendTo field modified to send to local machine
//
//   Rev 1.8   Oct 29 2003 20:26:52   geaton
//Removed ignore attribute.
//
//   Rev 1.7   Sep 04 2003 18:53:04   geaton
//Added ignire attribute to tests that email events - since code must be changed prior to running tests. ie a valid email address must be compiled into the code.
//
//   Rev 1.6   Aug 22 2003 14:59:54   geaton
//changed email test address to dummy value
//
//   Rev 1.5   Aug 13 2003 13:48:48   kcheung
//Changed email to point to me.. trying to test something for a web email project
//
//   Rev 1.4   Jul 30 2003 18:08:38   geaton
//Changes to OperationalEvent constructors
//
//   Rev 1.3   Jul 30 2003 11:20:36   geaton
//Added code to output message indicating that email address should be changed prior to running test.
//
//   Rev 1.2   Jul 29 2003 17:31:46   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.1   Jul 25 2003 14:14:46   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;
using System.Web.Mail;
using System.Xml;
using System.Globalization;
using System.Configuration;
using NUnit.Framework;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// NUnit test class for EmailPublisher.
	/// </summary>
	[TestFixture]
	public class TestEmailPublisher
	{
		[Test]
		public void TestWriteEvent()
		{
			string sendTo = ConfigurationManager.AppSettings["singleEmailAddress"];
			string from = "someone@slb.com";

			string infoMessage = "IMPORTANT: The email publisher will publish events to the following email address: " +
				sendTo + " This can be changed in the config file";
			Console.WriteLine(infoMessage);
			
			// Sent a test email containing details of an operational event.

			EmailPublisher publisher = new EmailPublisher
				("id", sendTo, from,Environment.UserName + 
				": Test Email Publisher", System.Net.Mail.MailPriority.Normal, "");

			OperationalEvent oe = new OperationalEvent
				(TDEventCategory.ThirdParty,
				 TDTraceLevel.Error,
				 "This is a first test e-mail to show that the " +
				 "Email Publisher is working.",
				 "DummyTarget",
				 "12345");

			OperationalEvent oe2 = new OperationalEvent
				(TDEventCategory.Database,
				 TDTraceLevel.Info,
				 "This is a second test e-mail to show that the " +
				 "Email Publisher is working.",
				 "DummyTarget"
				);

			OperationalEvent oe3 = new OperationalEvent
				(TDEventCategory.Database,
				 TDTraceLevel.Verbose,
				 "This is the third to show that the " +
				 "Email Publisher is working."
				);

			// publish the events
			publisher.WriteEvent(oe);
			publisher.WriteEvent(oe2);
			publisher.WriteEvent(oe3);

			//-----------------------------------------------

			// create custom events

			CustomEventOne customEventOne = new CustomEventOne
				(TDEventCategory.Business, TDTraceLevel.Warning,
				"A custom event one message", Environment.UserName, 12345);

			CustomEventTwo customEventTwo = new CustomEventTwo
				(TDEventCategory.ThirdParty, TDTraceLevel.Error,
				"A custom event two message", Environment.UserName, 3343);

			// CustomEventOne has an email formatter defined, therefore
			// email should be sent with the message in the format specified
			// in CustomEventOneEmailFormatter
			publisher.WriteEvent(customEventOne);
			
			// CustomEventTwo has no email formatter defined, therefore
			// email should be sent with the message in the format specified
			// by the DefaultFormatter.
			publisher.WriteEvent(customEventTwo);

		}

		/// <remarks>
		/// Manual verification:
		/// Receive three emails from 'FromSomeone@slb.com' with subject [windowslogonuser]: Test Email Publisher  with message containing Operational event data.
		/// Receive two emails from 'FromSomeone@slb.com' with subject [windowslogonuser]: Test Email Publisher with message containing Custom event data.
		/// </remarks>
		[Test]
		[Ignore("Manual verification required")]
		public void ManualVerification()
		{}

		
		/// <remarks>
		/// Manual setup:
		/// Change td.common.logging.dll.config file to use valid email address.
		/// Note that this test will NOT fail even if this email address is invalid
		/// (this is because the logging service will use the default (file) publish to 
		/// publish email events it fails to publish)
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}
	}
}
