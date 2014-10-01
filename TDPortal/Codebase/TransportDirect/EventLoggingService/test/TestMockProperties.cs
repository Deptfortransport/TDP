// *********************************************** 
// NAME                 : TestMockProperties.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Property Providers used to support
// NUnit tests
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestMockProperties.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:20   mturner
//Initial revision.
//
//   Rev 1.16   Feb 23 2006 19:15:38   build
//Automatically merged from branch for stream3129
//
//   Rev 1.15.2.0   Nov 25 2005 18:02:12   schand
//Implemented  IPropertyProvider interface string this [string key, int partnerID] due to the change in IPropertyProvider interface.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.15   Apr 01 2004 16:05:42   geaton
//Changes resulting from unit testing refactoring exercise.
//
//   Rev 1.14   Oct 21 2003 15:14:32   geaton
//Changes resulting from removal of Event Log Entry Type from properties. (This is now derived from TDTraceLevel of event being logged.)
//
//   Rev 1.13   Oct 08 2003 16:33:52   geaton
//Updated assembly properties so that dll extension not used. (Follows change in PropertyValidator to use Assembly.Load rather than Assembly.LoadFrom)
//
//   Rev 1.12   Sep 18 2003 11:31:36   geaton
//Added support for testing CustomEmailPublisher and CustomEmailEvent classes.
//
//   Rev 1.11   Sep 12 2003 14:13:50   geaton
//Added extra tests for trace level changes
//
//   Rev 1.10   Sep 04 2003 18:47:56   geaton
//Updated property to ensure events are not emailed in tests.
//
//   Rev 1.9   Aug 22 2003 14:57:16   geaton
//Added class for testing emailattachment pub and event
//
//   Rev 1.8   Jul 30 2003 11:21:36   geaton
//Added code to automatically create publisher sinks. This negates the need to manually create them prior to running the tests.
//
//   Rev 1.7   Jul 25 2003 14:21:52   geaton
//Added definition of Supersede() so that compiler warnings are removed.
//
//   Rev 1.6   Jul 25 2003 14:15:00   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.5   Jul 23 2003 14:34:34   geaton
//updated test data dll references following new assembly naming standard for project

using System;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Messaging;
using System.Diagnostics;

using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Summary description for MockPropertiesEmpty.
	/// </summary>
	public class MockPropertiesEmpty : IPropertyProvider
	{
		private Hashtable current;

		public string this[string key]
		{
			get 
			{
				if (current.ContainsKey(key))
				{
					return (string)current[key];
				}
				else
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}


		public MockPropertiesEmpty()
		{
			current = new Hashtable();

		}

		// ---------------------------------------

		// Stuff required from interface - not actually
		// used by the mock

		public event SupersededEventHandler Superseded;

		// following definition gets rid of compiler warning
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}

		public string ApplicationID
		{
			get { return String.Empty; }
		}

		public string GroupID
		{
			get { return String.Empty; }
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		// ---------------------------------------
	}


	/// <summary>
	/// Summary description for MockPropertiesEmptyValues.
	/// </summary>
	public class MockPropertiesEmptyValues : IPropertyProvider
	{
		private Hashtable current;

		public string this[string key]
		{
			get 
			{
				if (current.ContainsKey(key))
				{
					return (string)current[key];
				}
				else
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public MockPropertiesEmptyValues()
		{
			current = new Hashtable();

			current[Keys.QueuePublishers] = "";
			current[Keys.DefaultPublisher] = ""; // bad
			current[Keys.FilePublishers] = "";
			current[Keys.EmailPublishers] = "";
			current[Keys.CustomPublishers] = "";
			current[Keys.EventLogPublishers] = "";
			current[Keys.ConsolePublishers] = "";

			current[Keys.OperationalTraceLevel] = ""; //bad
	
			current[Keys.OperationalEventVerbosePublishers] = "";  // bad
			current[Keys.OperationalEventInfoPublishers] = "";     // bad
			current[Keys.OperationalEventWarningPublishers] = "";  // bad
			current[Keys.OperationalEventErrorPublishers] = "";    // bad

			// current[Keys.CustomEvents] = ""; //bad

			current[Keys.CustomEventsLevel] = "";  // bad

		}
	
		// ---------------------------------------

		// Stuff required from interface - not actually
		// used by the mock

		public event SupersededEventHandler Superseded;

		// following definition gets rid of compiler warning
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}

		public string ApplicationID
		{
			get { return ""; }
		}

		public string GroupID
		{
			get { return ""; }
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		// ---------------------------------------

	}

	/// <summary>
	/// Summary description for MockPropertiesGoodMinimumProperties.
	/// </summary>
	public class MockPropertiesGoodMinimumProperties : IPropertyProvider
	{
		private Hashtable current;

		public string this[string key]
		{
			get 
			{
				if (current.ContainsKey(key))
				{
					return (string)current[key];
				}
				else
				{
					return null;
				}
			}
		}

		
		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}


		public MockPropertiesGoodMinimumProperties()
		{
			current = new Hashtable();

			current[Keys.QueuePublishers] = "";
			current[Keys.DefaultPublisher] = "File1";
			current[Keys.FilePublishers] = "File1";
			current[Keys.EmailPublishers] = "";
			current[Keys.CustomPublishers] = "";
			current[Keys.EventLogPublishers] = "";
			current[Keys.ConsolePublishers] = "";
			
			current[String.Format(Keys.FilePublisherDirectory, "File1")] = @".\File1";
			current[String.Format(Keys.FilePublisherRotation, "File1")] = "10";
			
			current[Keys.EmailPublishersSmtpServer] = "localhost";

			current[Keys.OperationalTraceLevel] = "Warning";
	
			current[Keys.OperationalEventVerbosePublishers] = "File1";
			current[Keys.OperationalEventInfoPublishers] = "File1";
			current[Keys.OperationalEventWarningPublishers] = "File1";
			current[Keys.OperationalEventErrorPublishers] = "File1";

			current[Keys.CustomEvents] = "";

			current[Keys.CustomEventsLevel] = "On";

			// create necessary publisher sinks
			DirectoryInfo di = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File1")].ToString());
			di.Create();
		
		}
	
		// ---------------------------------------

		// Stuff required from interface - not actually
		// used by the mock

		public event SupersededEventHandler Superseded;

		// following definition gets rid of compiler warning
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}

		public string ApplicationID
		{
			get { return ""; }
		}

		public string GroupID
		{
			get { return ""; }
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		// ---------------------------------------

	}

	/// <summary>
	/// Summary description for MockPropertiesGood.
	/// </summary>
	public class MockPropertiesGood : IPropertyProvider
	{
		private Hashtable current;

		public void MockLevelChange1()
		{
			// change operational trace level
			current[Keys.OperationalTraceLevel] = "Off";

			// change global custom event level
			current[Keys.CustomEventsLevel] = "Off";

			if (Superseded != null)
				Superseded(this, null);
		}

		public void MockLevelChange2()
		{
			// change operational trace level
			current[Keys.OperationalTraceLevel] = "Warning";

			// change global custom event level
			current[Keys.CustomEventsLevel] = "On";
			current[String.Format(Keys.CustomEventLevel, "CustomEvent1")] = "Off";

			if (Superseded != null)
				Superseded(this, null);
		}

		public void MockLevelChange3()
		{
			// change operational trace level
			current[Keys.OperationalTraceLevel] = "Info";

			if (Superseded != null)
				Superseded(this, null);
		}

		public void MockValidPropertyChange()
		{
			// change operational trace level
			current[Keys.OperationalTraceLevel] = "Warning";

			// change individual custom events levels to valid values
			current[String.Format(Keys.CustomEventLevel, "CustomEvent1")] = "Off";
			current[String.Format(Keys.CustomEventLevel, "CustomEvent2")] = "On";

			if (Superseded != null)
				Superseded(this, null);
		}

		public void MockInvalidPropertyChange()
		{
			// change operational trace level to invalid value
			current[Keys.OperationalTraceLevel] = "OffOops";

			// change global custom events level to invalid value
			current[Keys.CustomEventsLevel] = "Undefined";

			// change individual custom events levels to invalid values
			current[String.Format(Keys.CustomEventLevel, "CustomEvent1")] = "Undefined";
			current[String.Format(Keys.CustomEventLevel, "CustomEvent2")] = "Undefined";


			if (Superseded != null)
				Superseded(this, null);

		}

		public string this[string key]
		{
			get 
			{
				if (current.ContainsKey(key))
				{
					return (string)current[key];
				}
				else
				{
					return null;
				}
			}
		}

	
		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public MockPropertiesGood()
		{
			current = new Hashtable();

			current[Keys.QueuePublishers] = "Queue1 Queue2";
			current[Keys.DefaultPublisher] = "File1";
			current[Keys.FilePublishers] = "File1 File2";
			current[Keys.EmailPublishers] = "Email1 Email2";
			current[Keys.CustomPublishers] = "CustomPublisher1 CustomPublisher2";
			current[Keys.EventLogPublishers] = "EventLog1 EventLog2";
			current[Keys.ConsolePublishers] = "Console1 Console2";
			
			current[String.Format(Keys.QueuePublisherPath, "Queue1")] = Environment.MachineName + @"\Private$\TestQueue1$";
			current[String.Format(Keys.QueuePublisherDelivery, "Queue1")] = "Express";
			current[String.Format(Keys.QueuePublisherPriority, "Queue1")] = "High";
			current[String.Format(Keys.QueuePublisherPath, "Queue2")] = Environment.MachineName + @"\Private$\TestQueue2$";
			current[String.Format(Keys.QueuePublisherDelivery, "Queue2")] = "Recoverable";
			current[String.Format(Keys.QueuePublisherPriority, "Queue2")] = "Normal";
			
			current[String.Format(Keys.FilePublisherDirectory, "File1")] = @".\File1";
			current[String.Format(Keys.FilePublisherRotation, "File1")] = "10";
			current[String.Format(Keys.FilePublisherDirectory, "File2")] = @".\File2";
			current[String.Format(Keys.FilePublisherRotation, "File2")] = "2000";
			
			current[String.Format(Keys.EmailPublisherTo, "Email1")] = "garypeaton@hotmail.com";
			current[String.Format(Keys.EmailPublisherFrom, "Email1")] = "TDSupport1@slb.com";
			current[String.Format(Keys.EmailPublisherSubject, "Email1")] = "subject1";
			current[String.Format(Keys.EmailPublisherPriority, "Email1")] = "High";
			current[String.Format(Keys.EmailPublisherTo, "Email2")] = "kcheung@slb.com";
			current[String.Format(Keys.EmailPublisherFrom, "Email2")] = "kcheung@slb.com";
			current[String.Format(Keys.EmailPublisherSubject, "Email2")] = "ExactlyThirtyFiveCharactersLong....";
			current[String.Format(Keys.EmailPublisherPriority, "Email2")] = "Low";

			current[String.Format(Keys.ConsolePublisherStream, "Console1")] = "Error";
			current[String.Format(Keys.ConsolePublisherStream, "Console2")] = "Out";

			current[Keys.EmailPublishersSmtpServer] = "localhost";

			current[String.Format(Keys.CustomPublisherName, "CustomPublisher1")] = "TDPublisher1";
			current[String.Format(Keys.CustomPublisherName, "CustomPublisher2")] = "TDPublisher2";
			
			current[String.Format(Keys.EventLogPublisherName, "EventLog1")] = "ELName1";
			current[String.Format(Keys.EventLogPublisherSource, "EventLog1")] = "TDSource1";
			current[String.Format(Keys.EventLogPublisherMachine, "EventLog1")] = Environment.MachineName;
			current[String.Format(Keys.EventLogPublisherName, "EventLog2")] = "ELName2";
			current[String.Format(Keys.EventLogPublisherSource, "EventLog2")] = "TDSource2";
			current[String.Format(Keys.EventLogPublisherMachine, "EventLog2")] = Environment.MachineName;


			current[Keys.OperationalTraceLevel] = "Error";
	
			current[Keys.OperationalEventVerbosePublishers] = "Queue1 Queue2";
			current[Keys.OperationalEventInfoPublishers] = "File2";
			current[Keys.OperationalEventWarningPublishers] = "File2";
			current[Keys.OperationalEventErrorPublishers] = "Queue2 EventLog2";

			current[Keys.CustomEvents] = "CustomEvent1 CustomEvent2";

			current[String.Format(Keys.CustomEventName, "CustomEvent1")] = "TDEvent1";
			current[String.Format(Keys.CustomEventAssembly, "CustomEvent1")] = "td.common.logging";
			current[String.Format(Keys.CustomEventPublishers, "CustomEvent1")] = "CustomPublisher1 Queue1";
			current[String.Format(Keys.CustomEventLevel, "CustomEvent1")] = "On";
			current[String.Format(Keys.CustomEventName, "CustomEvent2")] = "TDEvent2";
			current[String.Format(Keys.CustomEventAssembly, "CustomEvent2")] = "td.common.logging";
			current[String.Format(Keys.CustomEventPublishers, "CustomEvent2")] = "CustomPublisher2";
			current[String.Format(Keys.CustomEventLevel, "CustomEvent2")] = "Off";

			current[Keys.CustomEventsLevel] = "On";

			// create necessary publisher sinks
			DirectoryInfo di1 = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File1")].ToString());
			di1.Create();
			DirectoryInfo di2 = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File2")].ToString());
			di2.Create();
			if (!MessageQueue.Exists(current[String.Format(Keys.QueuePublisherPath, "Queue1")].ToString()))
			{
				MessageQueue newQueue1 = MessageQueue.Create(current[String.Format(Keys.QueuePublisherPath, "Queue1")].ToString(), false);
			}
			if (!MessageQueue.Exists(current[String.Format(Keys.QueuePublisherPath, "Queue2")].ToString()))
			{
				MessageQueue newQueue1 = MessageQueue.Create(current[String.Format(Keys.QueuePublisherPath, "Queue2")].ToString(), false);
			}
			if(!EventLog.Exists(current[String.Format(Keys.EventLogPublisherName, "EventLog1")].ToString()))
			{
                EventSourceCreationData sourceData = new EventSourceCreationData(current[String.Format(Keys.EventLogPublisherSource, "EventLog1")].ToString(),
                                                                                 current[String.Format(Keys.EventLogPublisherName, "EventLog1")].ToString());
                EventLog.CreateEventSource(sourceData);
			}
			if(!EventLog.Exists(current[String.Format(Keys.EventLogPublisherName, "EventLog2")].ToString()))
			{
                EventSourceCreationData sourceData = new EventSourceCreationData(current[String.Format(Keys.EventLogPublisherSource, "EventLog2")].ToString(),
                                                                                 current[String.Format(Keys.EventLogPublisherName, "EventLog2")].ToString());
                EventLog.CreateEventSource(sourceData);
			}
			
		}
	
		// ---------------------------------------

		// Stuff required from interface - not actually
		// used by the mock

		public event SupersededEventHandler Superseded;

		public string ApplicationID
		{
			get { return ""; }
		}

		public string GroupID
		{
			get { return ""; }
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		// ---------------------------------------

	}

	/// <summary>
	/// Summary description for MockProperties.
	/// </summary>
	public class MockPropertiesBadPublishers : IPropertyProvider
	{
		private Hashtable current;

		public string this[string key]
		{
			get 
			{
				if (current.ContainsKey(key))
				{
					return (string)current[key];
				}
				else
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}



		public MockPropertiesBadPublishers()
		{
			current = new Hashtable();
		
			current[Keys.QueuePublishers] = "Queue3 Queue4 Queue5";
			current[Keys.DefaultPublisher] = "File3";
			current[Keys.FilePublishers] = "File3 File4 File5 File6";
			current[Keys.EmailPublishers] = "Email3 Email4 Email5";
			current[Keys.CustomPublishers] = "CustomPublisher3 CustomPublisher4 CustomPublisher5 CustomPublisher6 CustomPublisher7";
			current[Keys.EventLogPublishers] = "EventLog3 EventLog4 EventLog5";
			current[Keys.ConsolePublishers] = "Console3 Console4 Console5";
			
			current[String.Format(Keys.QueuePublisherPath, "Queue3")] = Environment.MachineName + @"\Private$\Missing";
			current[String.Format(Keys.QueuePublisherDelivery, "Queue3")] = "Expresso";
			current[String.Format(Keys.QueuePublisherPriority, "Queue3")] = "Highly";
			current[String.Format(Keys.QueuePublisherPath, "Queue4")] = "";
			current[String.Format(Keys.QueuePublisherDelivery, "Queue4")] = "";
			current[String.Format(Keys.QueuePublisherPriority, "Queue4")] = "";
			
			current[String.Format(Keys.FilePublisherDirectory, "File3")] = @".\Missing";
			current[String.Format(Keys.FilePublisherRotation, "File3")] = "-9";
			current[String.Format(Keys.FilePublisherDirectory, "File4")] = @".\Missing";
			current[String.Format(Keys.FilePublisherRotation, "File4")] = "0";
			current[String.Format(Keys.FilePublisherDirectory, "File5")] = "";
			current[String.Format(Keys.FilePublisherRotation, "File5")] = "";
			
			current[String.Format(Keys.EmailPublisherTo, "Email3")] = "garypeaton@@hotmail.com";
			current[String.Format(Keys.EmailPublisherFrom, "Email3")] = "TDSupport3oops";
			current[String.Format(Keys.EmailPublisherSubject, "Email3")] = "ExactlyThirtySixCharactersLong......";
			current[String.Format(Keys.EmailPublisherPriority, "Email3")] = "Highly";
			current[String.Format(Keys.EmailPublisherTo, "Email4")] = "@hotmail.com";
			current[String.Format(Keys.EmailPublisherFrom, "Email4")] = "TDSupport4";
			current[String.Format(Keys.EmailPublisherSubject, "Email4")] = "reallyreallyreallyreallyreallyreallyreallyreallyreallyreallylongsubjectline";
			current[String.Format(Keys.EmailPublisherPriority, "Email4")] = "";

			current[String.Format(Keys.ConsolePublisherStream, "Console3")] = "Undefined";
			current[String.Format(Keys.ConsolePublisherStream, "Console4")] = "";

			current[Keys.EmailPublishersSmtpServer] = "";

			current[String.Format(Keys.CustomPublisherName, "CustomPublisher3")] = "TDPublisher3";
			current[String.Format(Keys.CustomPublisherName, "CustomPublisher4")] = "BadPublisher";
			current[String.Format(Keys.CustomPublisherName, "CustomPublisher5")] = "BadderPublisher";
			current[String.Format(Keys.CustomPublisherName, "CustomPublisher6")] = "";

			current[String.Format(Keys.EventLogPublisherName, "EventLog3")] = "VeryLongEventLogName";
			current[String.Format(Keys.EventLogPublisherSource, "EventLog3")] = "SourceName";
			current[String.Format(Keys.EventLogPublisherMachine, "EventLog3")] = "UnknownMachine";		
			current[String.Format(Keys.EventLogPublisherName, "EventLog4")] = "Name";
			current[String.Format(Keys.EventLogPublisherSource, "EventLog4")] = "Source";
			current[String.Format(Keys.EventLogPublisherMachine, "EventLog4")] = "Machine";

			
			// NOTE : the last publisher identifier does not have ANY other keys defined for it

			// NB EVENT PROPERTIES ARE NOT REQUIRED FOR THIS MOCK OBJECT

		}	

		// ---------------------------------------

		// Stuff required from interface - not actually
		// used by the mock

		public event SupersededEventHandler Superseded;

		// following definition gets rid of compiler warning
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}

		public string ApplicationID
		{
			get { return ""; }
		}

		public string GroupID
		{
			get { return ""; }
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		// ---------------------------------------

	}

	/// <summary>
	/// Summary description for MockPropertiesGoodPublishersBadEvents.
	/// </summary>
	public class MockPropertiesGoodPublishersBadEvents : IPropertyProvider
	{
		private Hashtable current;

		public string this[string key]
		{
			get 
			{
				if (current.ContainsKey(key))
				{
					return (string)current[key];
				}
				else
				{
					return null;
				}
			}
		}


		
		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public MockPropertiesGoodPublishersBadEvents()
		{
			current = new Hashtable();

			// (GOOD) PUBLISHER PROPERTIES START
			current[Keys.QueuePublishers] = "Queue1 Queue2";
			current[Keys.DefaultPublisher] = "File1";
			current[Keys.FilePublishers] = "File1 File2";
			current[Keys.EmailPublishers] = "Email1 Email2";
			current[Keys.CustomPublishers] = "CustomPublisher1 CustomPublisher2";
			current[Keys.EventLogPublishers] = "EventLog1 EventLog2";
			current[Keys.ConsolePublishers] = "Console1 Console2";
			
			current[String.Format(Keys.QueuePublisherPath, "Queue1")] = Environment.MachineName + @"\Private$\TestQueue1$";
			current[String.Format(Keys.QueuePublisherDelivery, "Queue1")] = "Express";
			current[String.Format(Keys.QueuePublisherPriority, "Queue1")] = "High";
			current[String.Format(Keys.QueuePublisherPath, "Queue2")] = Environment.MachineName + @"\Private$\TestQueue2$";
			current[String.Format(Keys.QueuePublisherDelivery, "Queue2")] = "Recoverable";
			current[String.Format(Keys.QueuePublisherPriority, "Queue2")] = "Normal";
			
			current[String.Format(Keys.FilePublisherDirectory, "File1")] = @".\File1";
			current[String.Format(Keys.FilePublisherRotation, "File1")] = "10";
			current[String.Format(Keys.FilePublisherDirectory, "File2")] = @".\File2";
			current[String.Format(Keys.FilePublisherRotation, "File2")] = "2000";
			
			current[String.Format(Keys.EmailPublisherTo, "Email1")] = "garypeaton@hotmail.com";
			current[String.Format(Keys.EmailPublisherFrom, "Email1")] = "TDSupport1@slb.com";
			current[String.Format(Keys.EmailPublisherSubject, "Email1")] = "subject1";
			current[String.Format(Keys.EmailPublisherPriority, "Email1")] = "High";
			current[String.Format(Keys.EmailPublisherTo, "Email2")] = "garypeaton@hotmail.com";
			current[String.Format(Keys.EmailPublisherFrom, "Email2")] = "TDSupport2@slb.com";
			current[String.Format(Keys.EmailPublisherSubject, "Email2")] = "ExactlyThirtyFiveCharactersLong....";
			current[String.Format(Keys.EmailPublisherPriority, "Email2")] = "Low";

			current[String.Format(Keys.ConsolePublisherStream, "Console1")] = "Error";
			current[String.Format(Keys.ConsolePublisherStream, "Console2")] = "Out";

			current[Keys.EmailPublishersSmtpServer] = "localhost";

			current[String.Format(Keys.CustomPublisherName, "CustomPublisher1")] = "TDPublisher1";
			current[String.Format(Keys.CustomPublisherName, "CustomPublisher2")] = "TDPublisher2";
			
			current[String.Format(Keys.EventLogPublisherName, "EventLog1")] = "ELName1";
			current[String.Format(Keys.EventLogPublisherSource, "EventLog1")] = "TDSource1";
			current[String.Format(Keys.EventLogPublisherMachine, "EventLog1")] = Environment.MachineName;	
			current[String.Format(Keys.EventLogPublisherName, "EventLog2")] = "ELName2";
			current[String.Format(Keys.EventLogPublisherSource, "EventLog2")] = "TDSource2";
			current[String.Format(Keys.EventLogPublisherMachine, "EventLog2")] = Environment.MachineName;

			// (GOOD) PUBLISHER PROPERTIES END

			// (BAD) EVENT PROPERTIES START
			current[Keys.OperationalTraceLevel] = "ErrorOops";
			current[Keys.CustomEventsLevel] = "OnOops";
	
			current[Keys.OperationalEventVerbosePublishers] = "Queue1Oops Queue2Oops";
			current[Keys.OperationalEventInfoPublishers] = "File2Oops";
			current[Keys.OperationalEventWarningPublishers] = "Email1Oops";
			current[Keys.OperationalEventErrorPublishers] = "Email2Oops EventLog1Oops";

			current[Keys.CustomEvents] = "CustomEvent1 CustomEvent2 CustomEvent3 CustomEvent4 CustomEvent5";

			current[String.Format(Keys.CustomEventName, "CustomEvent1")] = "TDEventUnknown";
			current[String.Format(Keys.CustomEventAssembly, "CustomEvent1")] = "td.common.logging";
			current[String.Format(Keys.CustomEventPublishers, "CustomEvent1")] = "UnknownPub";
			current[String.Format(Keys.CustomEventLevel, "CustomEvent1")] = "OnOops";
			current[String.Format(Keys.CustomEventName, "CustomEvent2")] = "TDEvent2";
			current[String.Format(Keys.CustomEventAssembly, "CustomEvent2")] = "Unknown";
			current[String.Format(Keys.CustomEventPublishers, "CustomEvent2")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "CustomEvent2")] = "Off";
			current[String.Format(Keys.CustomEventName, "CustomEvent3")] = "TDEvent3";
			current[String.Format(Keys.CustomEventAssembly, "CustomEvent3")] = "td.common.logging";
			current[String.Format(Keys.CustomEventPublishers, "CustomEvent3")] = "CustomPublisher1";
			current[String.Format(Keys.CustomEventLevel, "CustomEvent3")] = "On";
			current[String.Format(Keys.CustomEventName, "CustomEvent4")] = "";
			current[String.Format(Keys.CustomEventAssembly, "CustomEvent4")] = "";
			current[String.Format(Keys.CustomEventPublishers, "CustomEvent4")] = "";
			current[String.Format(Keys.CustomEventLevel, "CustomEvent4")] = "";
			// (BAD) EVENT PROPERTIES END

			// create necessary publisher sinks
			DirectoryInfo di1 = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File1")].ToString());
			di1.Create();
			DirectoryInfo di2 = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File2")].ToString());
			di2.Create();
			if (!MessageQueue.Exists(current[String.Format(Keys.QueuePublisherPath, "Queue1")].ToString()))
			{
				MessageQueue newQueue1 = MessageQueue.Create(current[String.Format(Keys.QueuePublisherPath, "Queue1")].ToString(), false);
			}
			if (!MessageQueue.Exists(current[String.Format(Keys.QueuePublisherPath, "Queue2")].ToString()))
			{
				MessageQueue newQueue1 = MessageQueue.Create(current[String.Format(Keys.QueuePublisherPath, "Queue2")].ToString(), false);
			}
			if(!EventLog.Exists(current[String.Format(Keys.EventLogPublisherName, "EventLog1")].ToString()))
			{
                EventSourceCreationData sourceData = new EventSourceCreationData(current[String.Format(Keys.EventLogPublisherSource, "EventLog1")].ToString(),
                                                                                 current[String.Format(Keys.EventLogPublisherName, "EventLog1")].ToString());
                EventLog.CreateEventSource(sourceData);
			}
			if(!EventLog.Exists(current[String.Format(Keys.EventLogPublisherName, "EventLog2")].ToString()))
			{
                EventSourceCreationData sourceData = new EventSourceCreationData(current[String.Format(Keys.EventLogPublisherSource, "EventLog2")].ToString(),
                                                                                 current[String.Format(Keys.EventLogPublisherName, "EventLog2")].ToString());
                EventLog.CreateEventSource(sourceData);
			}
		}

		// ---------------------------------------

		// Stuff required from interface - not actually
		// used by the mock

		public event SupersededEventHandler Superseded;

		// following definition gets rid of compiler warning
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}

		public string ApplicationID
		{
			get { return ""; }
		}

		public string GroupID
		{
			get { return ""; }
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		// ---------------------------------------

	}

	/// <summary>
	/// Summary description for MockPropertiesCoreCustomPublisher
	/// </summary>
	public class MockPropertiesCoreCustomPublisher : IPropertyProvider
	{
		private Hashtable current;

		public string this[string key]
		{
			get 
			{
				if (current.ContainsKey(key))
				{
					return (string)current[key];
				}
				else
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}



		public MockPropertiesCoreCustomPublisher()
		{
			current = new Hashtable();

			current[Keys.QueuePublishers] = "";
			current[Keys.DefaultPublisher] = "File1";
			current[Keys.FilePublishers] = "File1";
			current[Keys.EmailPublishers] = "";
			current[Keys.CustomPublishers] = "EMAIL";
			current[Keys.EventLogPublishers] = "";
			current[Keys.ConsolePublishers] = "";
			
			current[String.Format(Keys.FilePublisherDirectory, "File1")] = @".\File1";
			current[String.Format(Keys.FilePublisherRotation, "File1")] = "10";
			
			current[Keys.EmailPublishersSmtpServer] = "localhost";

			current[String.Format(Keys.CustomPublisherName, "EMAIL")] = "CustomEmailPublisher";

			current[Keys.OperationalTraceLevel] = "Warning";
	
			current[Keys.OperationalEventVerbosePublishers] = "File1";
			current[Keys.OperationalEventInfoPublishers] = "File1";
			current[Keys.OperationalEventWarningPublishers] = "File1";
			current[Keys.OperationalEventErrorPublishers] = "File1";

			current[Keys.CustomEvents] = "EMAILEVENT";
			current[String.Format(Keys.CustomEventName, "EMAILEVENT")] = "CustomEmailEvent";
			current[String.Format(Keys.CustomEventAssembly, "EMAILEVENT")] = "td.common.logging";
			current[String.Format(Keys.CustomEventPublishers, "EMAILEVENT")] = "EMAIL";
			current[String.Format(Keys.CustomEventLevel, "EMAILEVENT")] = "On";

			current[Keys.CustomEventsLevel] = "On";

			// create necessary publisher sinks
			DirectoryInfo di = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File1")].ToString());
			di.Create();
		
		}
	
		// ---------------------------------------

		// Stuff required from interface - not actually
		// used by the mock

		public event SupersededEventHandler Superseded;

		// following definition gets rid of compiler warning
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}

		public string ApplicationID
		{
			get { return ""; }
		}

		public string GroupID
		{
			get { return ""; }
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		// ---------------------------------------

	}

}
