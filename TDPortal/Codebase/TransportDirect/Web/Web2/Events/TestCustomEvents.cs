// *********************************************** 
// NAME                 : TestCustomEvents.cs 
// AUTHOR               : Andrew Windley
// DATE CREATED         : 07/07/2003 
// DESCRIPTION  : NUnit test for the Feedback custom events
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Events/TestCustomEvents.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:19:00   mturner
//Initial revision.
//
//   Rev 1.2   Jul 20 2004 16:12:36   AWindley
//Updated email address to 'tdpdev' account for automated testing.
//
//   Rev 1.1   Aug 12 2003 17:25:40   AWindley
//Amended test event parameter values
//
//   Rev 1.0   Aug 12 2003 13:10:32   AWindley
//Initial Revision

using System;
using System.Net.Mail;
using System.Xml;
using System.Globalization;
using NUnit.Framework;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.Web.Events
{
	/// <summary>
	/// NUnit test class for EmailPublisher.
	/// </summary>
	[TestFixture]
	public class TestCustomEvents
	{
		[Test]
		public void TestWriteEvent()
		{
			// IMPORTANT: The sendTo address is used to facilitate automated testing.
			string sendTo = "tdpdev@yahoo.co.uk";
			string from = "tdpdev@atosorigin.com";

			string infoMessage = "IMPORTANT: This test will email feedback events to: " +
				sendTo + ". This can be changed in the source file TestCustomEvents.cs.";
			Console.WriteLine(infoMessage);
			
			// Sent a test email containing details of an operational event.
			EmailPublisher publisher = new EmailPublisher
				("id", sendTo, from, "[Call Reference] : [FeedbackTypeText]", MailPriority.Normal, "");

			// Create test feedback events
			//
			// Each feedback event has an email formatter defined, therefore
			// email should be sent with the message in the format specified
			// in XxxxxxxxxEventEmailFormatter.cs
			
			// Create FeedbackEvent
			FeedbackEvent fe = new FeedbackEvent("111111",
				"FirstName",
				"LastName",
				"This is a Feedback Event comment.");

			// Create GeneralComplaintEvent
			GeneralComplaintEvent gce = new	GeneralComplaintEvent("222222",
				"FirstName",
				"LastName",
				"This is a General Complaint Event comment.");

			// Create JourneyComplaintEvent
			// Note: the journey plan reference (shown here as 999999999) 
			// is for internal use only and therefore does not appear in the email
			JourneyComplaintEvent jce = new	JourneyComplaintEvent("333333", 
				"FirstName",
				"LastName",
				"This is a Journey Complaint Event comment. Note: Journey Plan reference is not displayed.",
				999999999);
			
			// Write events
			publisher.WriteEvent(fe);
			publisher.WriteEvent(gce);
			publisher.WriteEvent(jce);
		}
	}
}
