// *********************************************** 
// NAME			: MockProperties.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 19/09/2003 
// DESCRIPTION	: Mock property provider for defining properties used for tests
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestMockProperties.cs-arc  $
//
//   Rev 1.1   Apr 02 2013 11:18:22   mmodi
//Unit test updates
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.0   Nov 08 2007 12:24:18   mturner
//Initial revision.
//
//   Rev 1.6   Mar 30 2006 13:48:36   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.5   Feb 23 2006 19:15:40   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.2.0   Nov 25 2005 18:02:48   schand
//Implemented  IPropertyProvider interface string this [string key, int partnerID] due to the change in IPropertyProvider interface.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.4   Apr 20 2004 16:05:02   CHosegood
//Added PVCS header

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using System.IO;
using System.Diagnostics;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Mock property provider for defining properties used for tests.
	/// </summary>
	public class MockProperties : IPropertyProvider, IServiceFactory	
	{
		private Hashtable current;
		private MockProperties instance;

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
			current[String.Format(Keys.FilePublisherRotation, "File1")] = "10";
			
			current[Keys.EmailPublishersSmtpServer] = "localhost";

			current[Keys.OperationalTraceLevel] = "Verbose";
	
			current[Keys.OperationalEventVerbosePublishers] = "File1";
			current[Keys.OperationalEventInfoPublishers] = "File1";
			current[Keys.OperationalEventWarningPublishers] = "File1";
			current[Keys.OperationalEventErrorPublishers] = "File1";

			current[Keys.CustomEvents] = "REQ REQV RES RESV";

			current[String.Format(Keys.CustomEventName, "REQ")] = "JourneyPlanRequestEvent";
			current[String.Format(Keys.CustomEventAssembly, "REQ")] = "td.userportal.journeycontrol";
			current[String.Format(Keys.CustomEventPublishers, "REQ")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "REQ")] = "On";
			current[String.Format(Keys.CustomEventName, "REQV")] = "JourneyPlanRequestVerboseEvent";
			current[String.Format(Keys.CustomEventAssembly, "REQV")] = "td.userportal.journeycontrol";
			current[String.Format(Keys.CustomEventPublishers, "REQV")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "REQV")] = "On";
			current[String.Format(Keys.CustomEventName, "RES")] = "JourneyPlanResultsEvent";
			current[String.Format(Keys.CustomEventAssembly, "RES")] = "td.userportal.journeycontrol";
			current[String.Format(Keys.CustomEventPublishers, "RES")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "RES")] = "On";
			current[String.Format(Keys.CustomEventName, "RESV")] = "JourneyPlanResultsVerboseEvent";
			current[String.Format(Keys.CustomEventAssembly, "RESV")] = "td.userportal.journeycontrol";
			current[String.Format(Keys.CustomEventPublishers, "RESV")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "RESV")] = "On";

			current[Keys.CustomEventsLevel] = "On";

			// create necessary publisher sinks
			DirectoryInfo di = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File1")].ToString());
			di.Create();
		
            // Location properties
            current[string.Format(LSKeys.NaptanPrefixProperties, StationType.Coach.ToString())] = "9000";
            current[string.Format(LSKeys.NaptanPrefixProperties, StationType.Rail.ToString())] = "9100";
            current[string.Format(LSKeys.NaptanPrefixProperties, StationType.Airport.ToString())] = "9200";
            current[LSKeys.NaptanPrefixAirportVia] = "920F";
            current[LSKeys.FilterGivenNameTag] = "True";
            current[LSKeys.GivenNameTagFilterValues] = "Main Coach Stops|Main Rail / Coach|Main Rail Stations";

		}

		// specifically required for JourneyRequestPopulatorFactory tests ...
		public void SetCombinedAir(string value)
		{
			current[JourneyControlConstants.UseCombinedAir] = value;
		}

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

		public object Get()
		{
			if	(instance == null) 
			{
				instance = new MockProperties();
			}
			return instance;
		}

	}
}
