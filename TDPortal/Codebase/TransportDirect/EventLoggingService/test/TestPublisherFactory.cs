// *********************************************** 
// NAME                 : TestPublisherFactory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : NUnit test for the
// PublisherGroup class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestPublisherFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:20   mturner
//Initial revision.
//
//   Rev 1.6   Feb 08 2005 14:36:26   bflenk
//Changed Assertion to Assert
//
//   Rev 1.5   Oct 21 2003 15:14:34   geaton
//Changes resulting from removal of Event Log Entry Type from properties. (This is now derived from TDTraceLevel of event being logged.)
//
//   Rev 1.4   Oct 21 2003 11:35:18   geaton
//Updated expected result following addition of validation of Event Log Source.
//
//   Rev 1.3   Jul 25 2003 14:15:00   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;
using System.Collections;
using NUnit.Framework;
using System.Diagnostics;

using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Summary description for PublisherFactoryTest.
	/// </summary>
	[TestFixture]
	public class TestPublisherFactory
	{
		

		[Test]
		public void GoodProperties()
		{
			IPropertyProvider goodProperties = new MockPropertiesGood();
			ArrayList errors = new ArrayList();

			// event logs must be created programatically on the test machine

			if(!EventLog.Exists("ELName1"))
			{
                EventSourceCreationData sourceData = new EventSourceCreationData("TDSource1", "ELName1");
				EventLog.CreateEventSource(sourceData);
			}

			if(!EventLog.Exists("ELName2"))
			{
                EventSourceCreationData sourceData = new EventSourceCreationData("TDSource2", "ELName2");
                EventLog.CreateEventSource(sourceData);
			}
			
			PublisherGroup standardPublishers = 
				new EventPublisherGroup(goodProperties);
			standardPublishers.CreatePublishers(errors);
			
			// test that no validation errors have been found
			foreach (string error in errors)
				Console.WriteLine(error);
			Assert.IsTrue(errors.Count == 0);
			

			// test that all publishers have been created
			Assert.IsTrue(standardPublishers.Publishers.Count == 10); // 2 each of 5 types

			// test that correct publishers have been created by checking that ids are valid
			foreach (IEventPublisher publisher in standardPublishers.Publishers)
			{
				string id = publisher.Identifier;

				Assert.IsTrue(id == "Email1" ||
								 id == "Email2" ||
								 id == "Queue1" ||
								 id == "Queue2" ||
								 id == "File1" ||
								 id == "File2" ||
								 id == "EventLog1" ||
								 id == "EventLog2" ||
								 id == "Console1" ||
								 id == "Console2"
								 );
			}


		}

		[Test]
		public void BadProperties()
		{
			IPropertyProvider badProperties = new MockPropertiesBadPublishers();
			ArrayList errors = new ArrayList();

			PublisherGroup standardPublishers = new EventPublisherGroup(badProperties);
			standardPublishers.CreatePublishers(errors);
			
			// test that no publishers have been created
			Assert.IsTrue(standardPublishers.Publishers.Count == 0);

			// display the errors for visual comfort factor
			foreach (string error in errors)
			{
				string message = "NUNIT:TestPublisherFactory. " + error;
				Console.WriteLine(message);
			}

			// test for correct number of validation errors
			Assert.IsTrue(errors.Count == 40);
		}

		[SetUp]
		public void Init()
		{
		}

	}
}
