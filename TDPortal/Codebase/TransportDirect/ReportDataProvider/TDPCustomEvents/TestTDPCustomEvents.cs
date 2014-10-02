// *********************************************** 
// NAME                 : TestTDPCustomEvents.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : NUnit tests for testing
// TDP Custom Event classes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/TestTDPCustomEvents.cs-arc  $
//
//   Rev 1.1   Jun 28 2010 14:07:48   PScott
//SCR 5561 - write MachineName to reference transactions
//
//   Rev 1.0   Nov 08 2007 12:39:36   mturner
//Initial revision.
//
//   Rev 1.17   Feb 23 2006 19:15:48   build
//Automatically merged from branch for stream3129
//
//   Rev 1.16.3.0.1.7   Jan 10 2006 17:35:46   schand
//Passing corrected language parameter
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.16.3.0.1.6   Jan 10 2006 17:31:42   schand
//replaced TestMethod operation with RequestContextData
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.16.3.0.1.5   Jan 10 2006 16:08:38   schand
//Passing sopa action as well
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.16.3.0.1.4   Dec 22 2005 14:58:46   halkatib
//Changes requested by Chris o made
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.16.3.0.1.3   Nov 25 2005 14:43:08   rgreenwood
//TD106 FXCop changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.16.3.0.1.2   Nov 25 2005 14:31:32   rgreenwood
//TD106 FXCop: Extended Namespace for EnhancedExposedServicesCommon
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.16.3.0.1.1   Nov 25 2005 14:27:54   schand
//Added Iinterface method to this test class
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.16.3.0.1.0   Nov 22 2005 16:48:30   rgreenwood
//TD106: Changed using statement and test parameter value.
//
//   Rev 1.16.3.0   Nov 21 2005 13:10:08   rgreenwood
//TD106 Enhanced Exposed WebServ Framework: Added NUnit tests for Start and Finish events
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.16   Mar 08 2005 16:32:32   schand
//Added MpbilePageEvent ;ogging for test
//
//   Rev 1.15   Feb 08 2005 11:47:56   RScott
//Assertion changed to Assert
//
//   Rev 1.14   Jul 22 2004 15:15:02   jmorrissey
//Added test for UserFeedbackEvent in PublishEvents() method
//
//   Rev 1.13   Jul 15 2004 18:04:38   acaunt
//Modified so that GazetteerEvent is now passed submitted time.
//
//   Rev 1.12   Apr 19 2004 20:31:12   geaton
//IR785
//
//   Rev 1.11   Dec 02 2003 20:07:10   geaton
//Updates following addition of Successful column on ReferenceTransactionEvent table.
//
//   Rev 1.10   Nov 10 2003 12:31:14   geaton
//Removed reference to TDTransactionCategory - a string will be used instead to allow new categories to be added at runtime.
//
//   Rev 1.9   Oct 30 2003 12:22:50   geaton
//Added test for ReceivedOperationalEvent.
//
//   Rev 1.8   Oct 29 2003 19:59:38   geaton
//Updated properties following ELS change whereby assembly without extension is needed.
//
//   Rev 1.7   Oct 07 2003 11:57:44   PScott
//workload event now passed datetime instead of ints
//
//   Rev 1.6   Oct 03 2003 12:30:52   geaton
//Removed LoggedOn parameter from DataGatewayEvent constructor.
//
//   Rev 1.5   Oct 02 2003 17:31:34   geaton
//Removed loggedOn parameter from LoginEvent since by definition the user is logged on!
//
//   Rev 1.4   Oct 01 2003 11:45:12   PScott
//Added Datagateway event
//
//   Rev 1.3   Sep 18 2003 16:53:02   geaton
//Added test for UserPreferenceSaveEvent.
//
//   Rev 1.2   Sep 16 2003 16:35:42   ALole
//Updated WorkloadEvent - removed URIStem and BytesSent fields
//
//   Rev 1.1   Sep 15 2003 16:54:20   geaton
//ReferenceTransactionEvent takes DateTime instead of TDDateTime.
//
//   Rev 1.0   Sep 12 2003 11:31:46   geaton
//Initial Revision

using System;
using NUnit.Framework;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using System.IO;
using System.Diagnostics;
using TransportDirect.EnhancedExposedServices.Common;


namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Mock property provider for defining properties used for tests.
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

			current[Keys.OperationalTraceLevel] = "Error";
	
			current[Keys.OperationalEventVerbosePublishers] = "File1";
			current[Keys.OperationalEventInfoPublishers] = "File1";
			current[Keys.OperationalEventWarningPublishers] = "File1";
			current[Keys.OperationalEventErrorPublishers] = "File1";

			current[Keys.CustomEvents] = "RT GE LE ME PE RE WE UP DE ROE UF RTTI MP SE FE";
			current[String.Format(Keys.CustomEventName, "RT")] = "ReferenceTransactionEvent";
			current[String.Format(Keys.CustomEventAssembly, "RT")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "RT")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "RT")] = "On";
			current[String.Format(Keys.CustomEventName, "GE")] = "GazetteerEvent";
			current[String.Format(Keys.CustomEventAssembly, "GE")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "GE")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "GE")] = "On";
			current[String.Format(Keys.CustomEventName, "LE")] = "LoginEvent";
			current[String.Format(Keys.CustomEventAssembly, "LE")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "LE")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "LE")] = "On";
			current[String.Format(Keys.CustomEventName, "ME")] = "MapEvent";
			current[String.Format(Keys.CustomEventAssembly, "ME")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "ME")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "ME")] = "On";
			current[String.Format(Keys.CustomEventName, "PE")] = "PageEntryEvent";
			current[String.Format(Keys.CustomEventAssembly, "PE")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "PE")] = "File1";     			
			current[String.Format(Keys.CustomEventLevel, "PE")] = "On";

			current[String.Format(Keys.CustomEventName, "RE")] = "RetailerHandoffEvent";
			current[String.Format(Keys.CustomEventAssembly, "RE")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "RE")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "RE")] = "On";
			current[String.Format(Keys.CustomEventName, "DE")] = "DataGatewayEvent";
			current[String.Format(Keys.CustomEventAssembly, "DE")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "DE")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "DE")] = "On";
			current[String.Format(Keys.CustomEventName, "WE")] = "WorkloadEvent";
			current[String.Format(Keys.CustomEventAssembly, "WE")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "WE")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "WE")] = "On";
			current[String.Format(Keys.CustomEventName, "UP")] = "UserPreferenceSaveEvent";
			current[String.Format(Keys.CustomEventAssembly, "UP")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "UP")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "UP")] = "On";
			current[String.Format(Keys.CustomEventName, "ROE")] = "ReceivedOperationalEvent";
			current[String.Format(Keys.CustomEventAssembly, "ROE")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "ROE")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "ROE")] = "On";
			current[String.Format(Keys.CustomEventName, "UF")] = "UserFeedbackEvent";
			current[String.Format(Keys.CustomEventAssembly, "UF")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "UF")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "UF")] = "On";
			
			current[String.Format(Keys.CustomEventName, "RTTI")] = "RTTIEvent";
			current[String.Format(Keys.CustomEventAssembly, "RTTI")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "RTTI")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "RTTI")] = "On";
			

			current[String.Format(Keys.CustomEventName, "MP")] = "TDMobilePageEntryEvent";
			current[String.Format(Keys.CustomEventAssembly, "MP")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "MP")] = "File1";
			current[String.Format(Keys.CustomEventLevel, "MP")] = "On";

			current[String.Format(Keys.CustomEventName, "SE")] = "EnhancedExposedServiceStartEvent";
			current[String.Format(Keys.CustomEventAssembly, "SE")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "SE")] = "File1";     			
			current[String.Format(Keys.CustomEventLevel, "SE")] = "On";
			
			current[String.Format(Keys.CustomEventName, "FE")] = "EnhancedExposedServiceFinishEvent";
			current[String.Format(Keys.CustomEventAssembly, "FE")] = "td.reportdataprovider.tdpcustomevents";
			current[String.Format(Keys.CustomEventPublishers, "FE")] = "File1";     			
			current[String.Format(Keys.CustomEventLevel, "FE")] = "On";


			current[Keys.CustomEventsLevel] = "On";

			// create necessary publisher sinks
			DirectoryInfo di = new DirectoryInfo(current[String.Format(Keys.FilePublisherDirectory, "File1")].ToString());
			di.Create();
		
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

	}
	[TestFixture]
	public class TestTDPCustomEvents
	{

		/// <summary>
		/// Tests that all TDP Custom Events can be published successfully.
		/// </summary>
		[Test]
		public void PublishEvents()
		{
			IPropertyProvider mockProperties = new MockProperties();
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(mockProperties, customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}
	
			// create instances of all events
			ReferenceTransactionEvent refTranEvent = new ReferenceTransactionEvent("ComplexJourneyRequest", true, DateTime.Now, "0", true, "INJ01");
			GazetteerEvent ge = new GazetteerEvent(GazetteerEventCategory.GazetteerAddress, DateTime.Now, "123", true);
			LoginEvent le = new LoginEvent("234");
			MapEvent me = new MapEvent(MapEventCommandCategory.MapInitialDisplay,DateTime.Now,MapEventDisplayCategory.OSStreetView,"452", false);
			PageEntryEvent pe = new PageEntryEvent(PageId.InitialPage, "345", true);
			TDMobilePageEntryEvent mp = new TDMobilePageEntryEvent(TDMobilePageId.MobileLogin, "111" , false);   
			RTTIEvent rtti = new RTTIEvent(DateTime.Now, DateTime.Now , true);  
			RetailerHandoffEvent re = new RetailerHandoffEvent("45", "25", true);
			DataGatewayEvent de = new DataGatewayEvent("feed1", "25", "C:\\file.txt",DateTime.Now,DateTime.Now,true,0);

			DateTime testDate = DateTime.Now;
			int testNum = 429;
			WorkloadEvent we = new WorkloadEvent(testDate, testNum);
			UserPreferenceSaveEvent up = new UserPreferenceSaveEvent(UserPreferenceSaveEventCategory.FareOptions, "987");
			UserFeedbackEvent uf = new UserFeedbackEvent("UserFeedbackSiteProblem",DateTime.MinValue,DateTime.MinValue,true,"TestSessionId", false );

			//Create Enhanced Exposed Web Services Start and Finish Events!
			ExposedServiceContext serviceContext = new ExposedServiceContext("1", "TestTranxID", "EN" ,"TransportDirect.EnhancedExposedServices.TestWebService/RequestContextData");			

			EnhancedExposedServiceStartEvent se = new EnhancedExposedServiceStartEvent(true, serviceContext);
			EnhancedExposedServiceFinishEvent fe = new EnhancedExposedServiceFinishEvent(true, serviceContext);

			// turn auditing on to check that event was published
			ge.AuditPublishersOff = false;
			refTranEvent.AuditPublishersOff = false;
			le.AuditPublishersOff = false;
			me.AuditPublishersOff = false;
			pe.AuditPublishersOff = false;
			mp.AuditPublishersOff = false;
			rtti.AuditPublishersOff = false;
			re.AuditPublishersOff = false;
			de.AuditPublishersOff = false;
			we.AuditPublishersOff = false;
			up.AuditPublishersOff = false;
			uf.AuditPublishersOff = false;
			se.AuditPublishersOff = false;
			fe.AuditPublishersOff = false;

			// publish events
			try
			{
				Trace.Write(refTranEvent);
				Trace.Write(ge);
				Trace.Write(le);
				Trace.Write(me);
				Trace.Write(pe);
				Trace.Write(mp);
				Trace.Write(rtti); 
				Trace.Write(re);
				Trace.Write(de);
				Trace.Write(we);
				Trace.Write(up);
				Trace.Write(uf);
				Trace.Write(se);
				Trace.Write(fe);
			}
			catch (TDException e)
			{
				Assert.Fail(e.ToString());
			}

			// check that they were published. NB manual check required for events that are published to file in production
			
			//note that events published to file will be in 
			//C:\TDPortal\DEL5\TransportDirect\ReportDataProvider\TDPCustomEvents\bin\Debug\File1
			Assert.IsTrue(ge.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(refTranEvent.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(le.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(me.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(pe.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(mp.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(re.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(de.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(we.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(up.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(uf.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(se.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");
			Assert.IsTrue(fe.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");

			// Attempt to cast an OperationalEvent to a ReceivedOperationalEvent
			OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, "My message");

			try
			{
				Trace.Write(new ReceivedOperationalEvent(oe));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}
				
			Assert.IsTrue(up.PublishedBy == "TransportDirect.Common.Logging.FilePublisher ");

		}
	}
}

