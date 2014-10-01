// *********************************************** 
// NAME			: TestCJPMessage.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestCJPMessage class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestCJPMessage.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:14   mturner
//Initial revision.
//
//   Rev 1.3   Feb 07 2005 11:15:22   RScott
//Assertion changed to Assert
//
//   Rev 1.2   Sep 22 2003 19:35:26   RPhilpott
//CJPMessage enhancements following receipt of new interface from Atkins
//
//   Rev 1.1   Sep 10 2003 11:13:56   RPhilpott
//Changes to CJPMessage handling.
//
//   Rev 1.0   Aug 20 2003 17:55:26   AToner
//Initial Revision
using System;
using NUnit.Framework;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestCJPMessage.
	/// </summary>
	[TestFixture]
	public class TestCJPMessage
	{
		public TestCJPMessage()
		{
		}

		[Test]
		public void TestMessage()
		{
			CJPMessage message = new CJPMessage( "Message", "ResourceId", 42, 3);

			Assert.AreEqual("Message", message.MessageText, "MessageText");
			Assert.AreEqual("ResourceId", message.MessageResourceId, "MessageResourceId");
			Assert.AreEqual(42, message.MajorMessageNumber, "MajorMessageNumber");
			Assert.AreEqual(3,  message.MinorMessageNumber, "MinorMessageNumber");
		}

	}
}
