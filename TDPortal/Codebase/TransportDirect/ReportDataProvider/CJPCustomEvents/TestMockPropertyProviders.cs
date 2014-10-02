// ******************************************************* 
// NAME                 : TestMockPropertyProviders.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 02/07/2004
// DESCRIPTION  : Mock property provider classes used for testing
// the custom CJP events
// ******************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/CJPCustomEvents/TestMockPropertyProviders.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:10   mturner
//Initial revision.
//
//   Rev 1.1   Feb 23 2006 19:15:48   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.2.0   Nov 25 2005 18:03:54   schand
//Implemented  IPropertyProvider interface string this [string key, int partnerID] due to the change in IPropertyProvider interface.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Jul 02 2004 15:32:58   jgeorge
//Initial revision.

using System;
using System.IO;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.CJPCustomEvents
{
	/// <summary>
	/// Mock property provider for defining properties used for tests.
	/// Use of a mock property provider simplifies testing since dependency on service discovery etc is removed.
	/// </summary>
	public class MockProperties : IPropertyProvider
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

		/// <summary>
		/// Sets single (file) publisher for all events.
		/// </summary>
		public MockProperties()
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
			current[String.Format(Keys.FilePublisherRotation, "File1")] = "100";
			
			current[Keys.EmailPublishersSmtpServer] = "localhost";

			current[Keys.OperationalTraceLevel] = "Error";
	
			current[Keys.OperationalEventVerbosePublishers] = "File1";
			current[Keys.OperationalEventInfoPublishers] = "File1";
			current[Keys.OperationalEventWarningPublishers] = "File1";
			current[Keys.OperationalEventErrorPublishers] = "File1";

			current[Keys.CustomEvents] = "JWR LR IR";
			current[String.Format(Keys.CustomEventName, "JWR")] = "JourneyWebRequestEvent";
			current[String.Format(Keys.CustomEventAssembly, "JWR")] = "td.reportdataprovider.cjpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "JWR")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "JWR")] = "On";
			current[String.Format(Keys.CustomEventName, "LR")] = "LocationRequestEvent";
			current[String.Format(Keys.CustomEventAssembly, "LR")] = "td.reportdataprovider.cjpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "LR")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "LR")] = "On";
			current[String.Format(Keys.CustomEventName, "IR")] = "InternalRequestEvent";
			current[String.Format(Keys.CustomEventAssembly, "IR")] = "td.reportdataprovider.cjpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "IR")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "IR")] = "On";

			current[Keys.CustomEventsLevel] = "On";

			// create necessary publisher sinks
			DirectoryInfo di = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File1")].ToString());
			di.Create();
		
		}

		// Stuff required from interface - not actually used by the tests

		public event SupersededEventHandler Superseded;

		// following definition gets rid of compiler warning
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}

		public string ApplicationID
		{
			get { return "CJPCustomEvents"; }
		}

		public string GroupID
		{
			get { return "ReportDataProvider"; }
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}
	}


	/// <summary>
	/// Mock property provider for defining properties used for tests.
	/// Use of a mock property provider simplifies testing since dependency on service discovery etc is removed.
	/// </summary>
	public class MockProperties2 : IPropertyProvider
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

		/// <summary>
		/// Sets single (queue) publisher for all events.
		/// </summary>
		public MockProperties2()
		{
			current = new Hashtable();

			current[Keys.QueuePublishers] = "Queue1";
			current[Keys.DefaultPublisher] = "File1";
			current[Keys.FilePublishers] = "File1";
			current[Keys.EmailPublishers] = "";
			current[Keys.CustomPublishers] = "";
			current[Keys.EventLogPublishers] = "";
			current[Keys.ConsolePublishers] = "";
			
			current[String.Format(Keys.FilePublisherDirectory, "File1")] = @".\File1";
			current[String.Format(Keys.FilePublisherRotation, "File1")] = "100";
			
			current[String.Format(Keys.QueuePublisherPath, "Queue1")] = @".\Private$\TDPrimaryQueue";
			current[String.Format(Keys.QueuePublisherDelivery, "Queue1")] = "Express";
			current[String.Format(Keys.QueuePublisherPriority, "Queue1")] = "Normal";

			current[Keys.EmailPublishersSmtpServer] = "localhost";

			current[Keys.OperationalTraceLevel] = "Error";
	
			current[Keys.OperationalEventVerbosePublishers] = "File1";
			current[Keys.OperationalEventInfoPublishers] = "File1";
			current[Keys.OperationalEventWarningPublishers] = "File1";
			current[Keys.OperationalEventErrorPublishers] = "File1";

			current[Keys.CustomEvents] = "JWR LR IR";
			current[String.Format(Keys.CustomEventName, "JWR")] = "JourneyWebRequestEvent";
			current[String.Format(Keys.CustomEventAssembly, "JWR")] = "td.reportdataprovider.cjpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "JWR")] = "Queue1";
			current[String.Format(Keys.CustomEventLevel, "JWR")] = "On";
			current[String.Format(Keys.CustomEventName, "LR")] = "LocationRequestEvent";
			current[String.Format(Keys.CustomEventAssembly, "LR")] = "td.reportdataprovider.cjpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "LR")] = "Queue1";
			current[String.Format(Keys.CustomEventLevel, "LR")] = "On";
			current[String.Format(Keys.CustomEventName, "IR")] = "InternalRequestEvent";
			current[String.Format(Keys.CustomEventAssembly, "IR")] = "td.reportdataprovider.cjpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "IR")] = "Queue1";
			current[String.Format(Keys.CustomEventLevel, "IR")] = "On";

			current[Keys.CustomEventsLevel] = "On";

			// create necessary publisher sinks
			DirectoryInfo di = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File1")].ToString());
			di.Create();


		
		}

		// Stuff required from interface - not actually used by the tests

		public event SupersededEventHandler Superseded;

		// following definition gets rid of compiler warning
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}

		public string ApplicationID
		{
			get { return "CJPCustomEvents"; }
		}

		public string GroupID
		{
			get { return "ReportDataProvider"; }
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}
	}


}
