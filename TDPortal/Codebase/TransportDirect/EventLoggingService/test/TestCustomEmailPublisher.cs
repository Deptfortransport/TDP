// *********************************************** 
// NAME                 : TestCustomEmailPublisher.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 17/09/2003 
// DESCRIPTION  : NUnit test for the
// CustomEmailPublisher class and indirectly the
// CustomEmailEvent class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestCustomEmailPublisher.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:14   mturner
//Initial revision.
//
//   Rev 1.7   Jul 06 2005 09:20:52   MTillett
//Fix unit test.
//Set credentials on web request to get attachement.
//Ignore the test for the email attachment, as requires manual verification.
//
//   Rev 1.6   May 13 2005 10:56:42   NMoorhouse
//Removed "Test With Empty File Stream" test.
//
//   Rev 1.5   Feb 08 2005 13:51:42   bflenk
//Assertion changed to Assert
//
//   Rev 1.4   Feb 07 2005 09:08:56   RScott
//Assertion changed to Assert
//
//   Rev 1.3   May 26 2004 17:10:52   COwczarek
//Change required due to new overload added to CustomEmailEvent constructor
//Resolution for 726: Server error message when sending email to friend (Maps) (DEL 6.0)
//
//   Rev 1.2   Apr 01 2004 16:05:50   geaton
//Changes resulting from unit testing refactoring exercise.
//
//   Rev 1.1   Sep 22 2003 17:05:58   geaton
//Added extra note re SMTP settings.
//
//   Rev 1.0   Sep 18 2003 11:29:00   geaton
//Initial Revision

using System;
using NUnit.Framework;
using System.Net.Mail;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Configuration;

using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.Logging
{
	[TestFixture]
	public class TestCustomEmailPublisher
	{
		// *** CHANGE FOLLOWING TO VALID DATA BEFORE RUNNING TESTS. (Also ensure that SMTP Server configurations are correct) ***
		private static string singleEmailAddress = ConfigurationManager.AppSettings["singleEmailAddress"];
		private static string multiEmailAddress = ConfigurationManager.AppSettings["multiEmailAddress"];
		private static string attachmentURL =  ConfigurationManager.AppSettings["attachmentURL"];
		// ***********************************************************

		/// <summary>
		/// Tests that a file attachment (specified by this.attachmentURL)
		/// can be sent to the email address (specified by this.singleEmailAddress)
		/// </summary>
		[Test]
		[Ignore("Manual verification that email has been received with attachment required")]
		public void PublishAttachmentWithoutError()
		{
			IPropertyProvider goodProperties = new MockPropertiesCoreCustomPublisher();

			IEventPublisher[] customPublishers = new IEventPublisher[1];

			string workingDirectory = Environment.CurrentDirectory + "\\EmailAttachWorkDir";
			Directory.CreateDirectory(workingDirectory);

			ArrayList errors = new ArrayList();

			customPublishers[0] = 
				new CustomEmailPublisher("EMAIL",
										 "FromSomeone@slb.com",
										 MailPriority.Normal,
										 "localhost",
										 workingDirectory,
										 errors);
							
			Assert.IsTrue(errors.Count == 0);
			
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

			
			string testTo = singleEmailAddress;
			string testUrl = attachmentURL;

			WebResponse result = null;
			Stream attachmentStream = null;

			try
			{
				WebRequest req = WebRequest.Create(testUrl);
				req.Credentials = CredentialCache.DefaultCredentials;
				result = req.GetResponse();
				attachmentStream = result.GetResponseStream();
			}
			catch (Exception)
			{
				string message = "NUNIT:TestCustomEmailPublisher. Error reading test file attachment from url: " + testUrl;
				Console.WriteLine(message);
				Assert.IsTrue(false);
			}


			CustomEmailEvent e1 = new CustomEmailEvent(testTo, "Hello!", "NUNIT_TEST:PublishAttachmentWithoutError", attachmentStream, "map.jpg");
			e1.AuditPublishersOff = false;

			try
			{
				Trace.Write(e1);
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			Assert.IsTrue(e1.PublishedBy == "TransportDirect.Common.Logging.CustomEmailPublisher ");


			// ensure working directory has been tidied up
			string [] dirs = new string [1];
			dirs = Directory.GetDirectories(workingDirectory);
			Assert.IsTrue(dirs.Length == 0);

			// Remove working directory for next test
			Directory.Delete(workingDirectory);

			Console.WriteLine("TestCustomEmailPublisher.PublishAttachmentWithoutError: Manual check required to ensure email arrives containing attachment!!!");
		}

		/// <summary>
		/// Tests error handling capabilities when sending an email with a file attachment.
		/// </summary>
		[Test]
		public void PublishAttachmentWithError()
		{
			IPropertyProvider goodProperties = new MockPropertiesCoreCustomPublisher();

			IEventPublisher[] customPublishers = new IEventPublisher[1];

			string workingDirectory = Environment.CurrentDirectory + "\\EmailAttachWorkDir";
			Directory.CreateDirectory(workingDirectory);


			// *** TEST WITH INVALID CONSTRUCTOR PARAMETERS ***
			ArrayList errors = new ArrayList();
			string badWorkingDirectory = Environment.CurrentDirectory + "\\BADEmailAttachWorkDir";
			bool exceptionThrown = false;

			try
			{
				customPublishers[0] = 
					new CustomEmailPublisher("",
					"FromSomeoneslb.com",
					MailPriority.Normal,
					"",
					badWorkingDirectory,
					errors);
							
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}
			
			Assert.IsTrue(exceptionThrown);
			Assert.IsTrue(errors.Count == 4);
			
			// Now create publisher using valid parameters to enable further tests
			errors.Clear();
			customPublishers[0] = 
				new CustomEmailPublisher("EMAIL",
										 "FromSomeone@slb.com",
										 MailPriority.Normal,
										 "localhost",
										 workingDirectory,
										 errors);
							
			Assert.IsTrue(errors.Count == 0);

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

			string testTo = singleEmailAddress;
			string testUrl = attachmentURL;

			WebResponse result = null;
			Stream attachmentStream = null;

			try
			{
				WebRequest req = WebRequest.Create(testUrl);
				req.Credentials = CredentialCache.DefaultCredentials;
				result = req.GetResponse();
				attachmentStream = result.GetResponseStream();
			}
			catch (Exception)
			{
				string message = "NUNIT:TestCustomEmailPublisher. Error reading test file attachment from url: " + testUrl;
				Console.WriteLine(message);
				Assert.IsTrue(false);
			}

			// *** TEST WITH INVALID FILENAME ***
			CustomEmailEvent e1 = new CustomEmailEvent(testTo, "Hello!", "NUNIT_TEST:PublishAttachmentWithError", attachmentStream, "");
			e1.AuditPublishersOff = false;

			exceptionThrown = false;

			try
			{
				Trace.Write(e1);
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}

			// Since custom email events can never be published by any other publisher other than the custom email publisher
			// then an exception will be thrown
			Assert.IsTrue(exceptionThrown);
			
			// ensure working directory has been tidied up (ie removed)
			bool dirs = true;
			dirs = Directory.Exists(workingDirectory);
			Assert.IsFalse(dirs, "Working Directory not removed");

		}

		/// <summary>
		/// Tests sending an email to a single address (specified by this.singleEmailAddress)
		/// and to multiple email addresses (specified by this.multiEmailAddress)
		/// </summary>
		[Test]
		public void PublishWithoutError()
		{
			IPropertyProvider goodProperties = new MockPropertiesCoreCustomPublisher();

			IEventPublisher[] customPublishers = new IEventPublisher[1];

			string workingDirectory = Environment.CurrentDirectory + "\\EmailAttachWorkDir";
			Directory.CreateDirectory(workingDirectory);

			ArrayList errors = new ArrayList();

			customPublishers[0] = 
				new CustomEmailPublisher("EMAIL",
				"FromSomeone@slb.com",
				MailPriority.Normal,
				"localhost",
				workingDirectory,
				errors);
							
			Assert.IsTrue(errors.Count == 0);
			
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

			// test sending to single email address
			CustomEmailEvent e1 = new CustomEmailEvent(singleEmailAddress, "Hello!", "NUNIT_TEST:PublishWithoutError");
			e1.AuditPublishersOff = false;

			try
			{
				Trace.Write(e1);
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			Assert.IsTrue(e1.PublishedBy == "TransportDirect.Common.Logging.CustomEmailPublisher ");

			// test sending to multiple email addresses
			CustomEmailEvent e2 = new CustomEmailEvent(multiEmailAddress, "Hello to all of you!", "NUNIT_TEST:PublishWithoutError");
			e2.AuditPublishersOff = false;

			try
			{
				Trace.Write(e2);
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			Assert.IsTrue(e2.PublishedBy == "TransportDirect.Common.Logging.CustomEmailPublisher ");

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

		/// <remarks>
		/// Manual verification:
		/// Receive one email from 'FromSomeone@slb.com' with subject NUNIT_TEST:PublishWithoutError with message 'Hello!'.
		/// Receive two emails from 'FromSomeone@slb.com' with subject NUNIT_TEST:PublishWithoutError with message 'Hello to all of you!'.
		/// Receive one email from 'FromSomeone@slb.com'with subject NUNIT_TEST:PublishAttachmentWithoutError with a file attachement and message of 'Hello!'
		/// </remarks>
		[Test]
		[Ignore("Manual verification required")]
		public void ManualVerification()
		{}

		
		/// <remarks>
		/// Manual setup:
		/// Change td.common.logging.dll.config file to use valid email addresses and sample attachment.
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}
	}
}

