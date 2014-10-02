// *********************************************** 
// NAME                 : EventGeneratorThread.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 25/09/2003 
// DESCRIPTION  : Thread used to generate events
// for link testing the Report Data Provider 
// components.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/LinkTestEventGenerator/EventGeneratorThread.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:52   mturner
//Initial revision.
//
//   Rev 1.11   Jul 15 2004 18:04:40   acaunt
//Modified so that GazetteerEvent is now passed submitted time.
//
//   Rev 1.10   Dec 09 2003 18:53:52   geaton
//Do not pass any parameter to TDJourneyResult.
//
//   Rev 1.9   Dec 09 2003 17:40:26   geaton
//Corrected call to TDJourneyResult that was passing parameter of incorrect type.
//
//   Rev 1.8   Dec 04 2003 15:58:24   geaton
//Updates to make test data more realistic.
//
//   Rev 1.7   Dec 04 2003 10:12:06   geaton
//Corrected submitted times so that they are before the logged times.
//
//   Rev 1.6   Nov 26 2003 18:50:52   geaton
//An event was being logged twice.
//
//   Rev 1.5   Nov 23 2003 15:55:56   geaton
//Prevent event data from being logged where not possible to log by real app.
//
//   Rev 1.4   Nov 23 2003 11:21:42   geaton
//Added exception handling to ensure generator does not fail when running in continous mode.
//
//   Rev 1.3   Nov 22 2003 19:58:48   geaton
//Updated method name to reflect purpose.
//
//   Rev 1.2   Oct 13 2003 11:07:02   PScott
//Code added to prevent processing when no events
//
//   Rev 1.1   Oct 03 2003 12:47:56   geaton
//Added DataGatewayEvent.
//
//   Rev 1.0   Oct 03 2003 09:19:38   geaton
//Initial Revision

using System;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Net;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.ReportDataProvider.CJPCustomEvents;

namespace TransportDirect.ReportDataProvider.LinkTestEventGenerator
{
	/// <summary>
	/// Summary description for EventGeneratorThread.
	/// </summary>
	public class EventGeneratorThread
	{
		
		public EventGeneratorThread()
		{
		}
		
		/// <summary>
		/// Generates N datagateway events
		/// </summary>
		private static void DataGatewayEvents(string sessionId)
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfDataGatewayEvents]);
			
			for (int eventNum=0; eventNum < numOfEvents; eventNum++)
			{		
				Trace.Write(new DataGatewayEvent(Properties.Current[Keys.DataGatwayFeed], sessionId, Properties.Current[Keys.DataGatwayFile],DateTime.Now - TimeSpan.FromSeconds(2), DateTime.Now - TimeSpan.FromSeconds(1),true, 123));
			}
		}

		/// <summary>
		/// Generates location request events, for each preposition category (x3), for two different sets of request id - adminarea - regionid
		/// </summary>
		private static void LocationRequestEvents(string journeyPlanRequestId1, string journeyPlanRequestId2)
		{
			Trace.Write(new LocationRequestEvent(journeyPlanRequestId1, JourneyPrepositionCategory.From, Properties.Current[Keys.AdminArea1], Properties.Current[Keys.RegionCode1]));
			Trace.Write(new LocationRequestEvent(journeyPlanRequestId2, JourneyPrepositionCategory.To, Properties.Current[Keys.AdminArea2], Properties.Current[Keys.RegionCode2]));
		}

		/// <summary>
		/// Generates 2 journey web request events per journey plan request id
		/// </summary>
		private static void JourneyWebRequestEvents(string sessionId, string journeyPlanRequestId1, string journeyPlanRequestId2)
		{
					
			DateTime startWebRequest = DateTime.Now;

			Thread.Sleep(new TimeSpan(0,0,0,0,int.Parse(Properties.Current[Keys.WebRequestDuration])));


			Trace.Write(new JourneyWebRequestEvent(sessionId,
 												   journeyPlanRequestId1 + "1",
												   startWebRequest,
												   Properties.Current[Keys.RegionCode1],
												   true, false));

			Trace.Write(new JourneyWebRequestEvent(sessionId,
												   journeyPlanRequestId2  + "1",
												   DateTime.Now - TimeSpan.FromSeconds(1),
												   Properties.Current[Keys.RegionCode2],
												   true, false));
	
			startWebRequest = DateTime.Now;

			Thread.Sleep(new TimeSpan(0,0,0,0,int.Parse(Properties.Current[Keys.WebRequestDuration])));



			Trace.Write(new JourneyWebRequestEvent(sessionId,
 												   journeyPlanRequestId1 + "2",
												   startWebRequest,
												   Properties.Current[Keys.RegionCode1],
												   true, false));

			Trace.Write(new JourneyWebRequestEvent(sessionId,
												   journeyPlanRequestId2  + "2",
												   DateTime.Now - TimeSpan.FromSeconds(1),
												   Properties.Current[Keys.RegionCode2],
												   true, false));
			

		}

		/// <summary>
		/// Generates N login events
		/// </summary>
		private static void LoginEvents(string sessionId)
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfLoginEvents]);
			
			for (int eventNum=0; eventNum < numOfEvents; eventNum++)
			{		
				Trace.Write(new LoginEvent(sessionId));		
			}

		}

		/// <summary>
		/// Generates N page entry events, for each page id (x?), each for logged on/off 
		/// </summary>
		private static void PageEntryEvents(string sessionId)
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfPageEntryEvents]);
			if ( numOfEvents > 0)
			{
			
				PageId[] ids = (PageId[]) Enum.GetValues(typeof(PageId));

				for (int eventNum=0; eventNum < numOfEvents; eventNum++)
				{
					foreach (PageId id in ids)
					{
						if (id != PageId.Empty) // Not possible to ever log events with this id.
						{
							Trace.Write(new PageEntryEvent(id, sessionId, true));
							Trace.Write(new PageEntryEvent(id, sessionId, false));
						}
					}
				}
			}
		}

		/// <summary>
		/// Generates 2 journey plan result verbose events,  for 2 request ids
		/// </summary>
		private static void JourneyPlanResultVerboseEvents(string sessionId, string journeyPlanRequestId1, string journeyPlanRequestId2)
		{
			TestTDJourneyResult testResult = new TestTDJourneyResult();
			JourneyResult cjpResult = testResult.CreateCJPResult( false );
			TDJourneyResult result = new TDJourneyResult(); //int.Parse(journeyPlanRequestId1));
			result.AddResult(cjpResult, true, null, sessionId);
				
			Trace.Write(new JourneyPlanResultsVerboseEvent(journeyPlanRequestId1, result, true, sessionId));
			Trace.Write(new JourneyPlanResultsVerboseEvent(journeyPlanRequestId2, result, true, sessionId));
		}

		/// <summary>
		/// Generates 2 journey plan result events, for 2 request ids
		/// </summary>
		private static void JourneyPlanResultEvents(string sessionId, string journeyPlanRequestId1, string journeyPlanRequestId2)
		{	
			Trace.Write(new JourneyPlanResultsEvent(journeyPlanRequestId1, JourneyPlanResponseCategory.Results, true, sessionId));
			Trace.Write(new JourneyPlanResultsEvent(journeyPlanRequestId2, JourneyPlanResponseCategory.Results, true, sessionId));	
		}

		/// <summary>
		/// Generates 2 journey plan verbose request events, for 2 request ids, each for logged on/off 
		/// </summary>
		private static void JourneyPlanRequestVerboseEvents(string sessionId, string journeyPlanRequestId1, string journeyPlanRequestId2)
		{
			ITDJourneyRequest request = new TDJourneyRequest();
			DateTime timeNow = DateTime.Now;
			request.IsReturnRequired = true;
			request.OutwardArriveBefore = false;
			request.ReturnArriveBefore = true;
			request.OutwardDateTime = new TDDateTime( timeNow );
			request.ReturnDateTime = new TDDateTime( timeNow.AddMinutes(1) );
			request.InterchangeSpeed = 1;
			request.WalkingSpeed = 2;
			request.MaxWalkingTime = 3;
			request.DrivingSpeed = 4;
			request.AvoidMotorways = false;
			request.OriginLocation = new TDLocation();
			request.DestinationLocation = new TDLocation();
			request.PublicViaLocation = new TDLocation();
			request.PrivateViaLocation = new TDLocation();
			request.AvoidRoads = new string[] {"A1", "A6"};
			request.AlternateLocations = new TDLocation[2];
			request.AlternateLocations[0] = new TDLocation();
			request.AlternateLocations[1] = new TDLocation();
			request.AlternateLocationsFrom = true;
			request.PrivateAlgorithm = PrivateAlgorithmType.MostEconomical;
			request.PublicAlgorithm = PublicAlgorithmType.Fastest;
			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Car };
			
			Trace.Write(new JourneyPlanRequestVerboseEvent(journeyPlanRequestId1, (TDJourneyRequest)request, true, sessionId));
			Trace.Write(new JourneyPlanRequestVerboseEvent(journeyPlanRequestId2, (TDJourneyRequest)request, true, sessionId));			
		}

		/// <summary>
		/// Generates 2 journey plan request events, for 2 request ids, each for logged on/off 
		/// </summary>
		private static void JourneyPlanRequestEvents(string sessionId, string journeyPlanRequestId1, string journeyPlanRequestId2)
		{
			ModeType[] modes = new ModeType[] { ModeType.Rail, ModeType.Car };
	
			Trace.Write(new JourneyPlanRequestEvent(journeyPlanRequestId1, modes, true, sessionId));
			Trace.Write(new JourneyPlanRequestEvent(journeyPlanRequestId2, modes, true, sessionId));

		}

		/// <summary>
		/// Generates N user save preference events for each category (x3)
		/// </summary>
		private static void UserSavePreferenceEvents(string sessionId)
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfUserPreferenceSaveEvents]);
			if ( numOfEvents > 0)
			{

				UserPreferenceSaveEventCategory[] categories = (UserPreferenceSaveEventCategory[]) Enum.GetValues(typeof(UserPreferenceSaveEventCategory));

				for (int eventNum=0; eventNum < numOfEvents; eventNum++)
				{
					foreach (UserPreferenceSaveEventCategory category in categories)
					{
						Trace.Write(new UserPreferenceSaveEvent(category, sessionId)); 
					}
				}
			}
		}

		/// <summary>
		/// Generates N retailer handoff events for for two retailers, for logged on/off
		/// </summary>
		private static void RetailerHandoffEvents(string sessionId)
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfRetailerHandoffEvents]);
			
			for (int eventNum=0; eventNum < numOfEvents; eventNum++)
			{
				Trace.Write(new RetailerHandoffEvent(Properties.Current[Keys.RetailerId1], sessionId, true)); 
				Trace.Write(new RetailerHandoffEvent(Properties.Current[Keys.RetailerId1], sessionId, false)); 
			}

		}

		/// <summary>
		/// Generates N map events for every command category (x4), for every display category (x5), for loggeg on/off (x2)
		/// </summary>
		private static void MapEvents(string sessionId)
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfMapEvents]);
			if ( numOfEvents > 0)
			{
				MapEventCommandCategory[] commands = (MapEventCommandCategory[]) Enum.GetValues(typeof(MapEventCommandCategory));
				MapEventDisplayCategory[] displays = (MapEventDisplayCategory[]) Enum.GetValues(typeof(MapEventDisplayCategory));
		
				for (int eventNum=0; eventNum < numOfEvents; eventNum++)
				{
					foreach (MapEventCommandCategory command in commands)
					{	
						foreach (MapEventDisplayCategory display in displays)
						{	
							Trace.Write(new MapEvent(command, DateTime.Now - TimeSpan.FromSeconds(1), display, sessionId, true));
							Trace.Write(new MapEvent(command, DateTime.Now - TimeSpan.FromSeconds(1), display, sessionId, false));
						}
					}
				}
			}
		}

		/// <summary>
		/// Generates N gazetteer events for every category (x6) and for logged on/off (x2)
		/// </summary>
		private static void GazetteerEvents(string sessionId)
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfGazetteerEvents]);
			if ( numOfEvents > 0)
			{
				GazetteerEventCategory[] categories = (GazetteerEventCategory[]) Enum.GetValues(typeof(GazetteerEventCategory));
		
				for (int eventNum=0; eventNum < numOfEvents; eventNum++)
				{
					foreach (GazetteerEventCategory category in categories)
					{				
						Trace.Write(new GazetteerEvent(category, DateTime.Now, sessionId, false));
						Trace.Write(new GazetteerEvent(category, DateTime.Now, sessionId, true));
					}
				}
			}
		}

		/// <summary>
		/// Generates N entries in the IIS web log of the host machine.
		/// Ensure that web log is empty before running this test and is configured as specified in the Web Log Reader Public interface.
		/// Web Log Reader component will generated WorkloadEvents based on the web log entries made by this method.
		/// </summary>
		private static void WebLogEntries()
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfWebLogEntries]);
			if ( numOfEvents > 0)
			{

				for (int eventNum=0; eventNum < numOfEvents; eventNum++)
				{
					Process myProcess = new Process();
					myProcess.StartInfo.FileName = "C:\\Program Files\\Internet Explorer\\IExplore.exe";
					myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
					myProcess.StartInfo.Arguments = Properties.Current[Keys.WorkloadEventURL];
					myProcess.Start();
				}

				Process[] myProcesses = Process.GetProcessesByName("IExplore");
				foreach(Process myProcess in myProcesses)
				{
					myProcess.CloseMainWindow();
				}
			}			
		}

		/// <summary>
		/// Generates N CustomEmail Events and sends to email address/es specified in properties.
		/// Note that these events will not be processed any further in the link test since they are published to their final destination immediately,
		/// ie custom email events may only be published using the custom email publisher.
		/// </summary>
		private static void CustomEmailEvents()
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfCustomEmailEvents]);

			for (int eventNum=0; eventNum < numOfEvents; eventNum++)
			{
				Trace.Write(new CustomEmailEvent(Properties.Current[Keys.EmailAddresses], "Hello!", "LinkTestEventGenerated!"));
			}
		}


		/// <summary>
		/// Generates N Operational Events for every category (x5) and 'valid' level (x4).
		/// </summary>
		private static void OperationalEvents()
		{
			int numOfEvents = int.Parse(Properties.Current[Keys.NumOfOperationalEvents]);
			if ( numOfEvents > 0)
			{

				TDTraceLevel[] levels = (TDTraceLevel[]) Enum.GetValues(typeof(TDTraceLevel));
				TDEventCategory[] categories = (TDEventCategory[]) Enum.GetValues(typeof(TDEventCategory));

				for (int eventNum=0; eventNum < numOfEvents; eventNum++)
				{	
					foreach (TDTraceLevel level in levels)
					{
						if ((level != TDTraceLevel.Undefined) && (level != TDTraceLevel.Off)) 
						{
							foreach (TDEventCategory category in categories)
							{				
								Trace.Write(new OperationalEvent(category, level, "LinkTestEventGenerated!"));
							}
						}
					}
				
				}
			}
		}

		/// <summary>
		/// Generates events of each type.
		/// Note:
		/// WorkloadEvents are generated by the Web Log Reader based on the web log entries made by this component.
		/// ReferenceTransactionEvents are generated by the Transaction Injector - data is not generated by this component.
		/// </summary>
		public static void Run()
		{
			// Make some properties unique based for each thread.
			string sessionId = Properties.Current[Keys.SessionId] + DateTime.Now.ToString("mmssfff");

			string uniqueId = DateTime.Now.ToString("sfff"); // Seconds part may or may not include leading zero.
			string uniqueIdFourChars = String.Empty;
			
			if (uniqueId.Length == 4)
				uniqueIdFourChars = uniqueId;
			else
				uniqueIdFourChars = uniqueId.Substring(1, 4);

			string journeyPlanRequestId1 = Properties.Current[Keys.JourneyPlanRequestId1] + uniqueIdFourChars + "-0000";
			string journeyPlanRequestId2 = Properties.Current[Keys.JourneyPlanRequestId2] + uniqueIdFourChars + "-0000";

			do
			{
				try
				{
					OperationalEvents();
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [OperationalEvent] Reason: " + exception.Message));
				}

				try
				{
					CustomEmailEvents();
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [CustomEmailEvent] Reason: " + exception.Message));
				}

				try
				{
					WebLogEntries();
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating web log entries. Reason: " + exception.Message));
				}

				try
				{
					GazetteerEvents(sessionId);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [GazetteerEvent] Reason: " + exception.Message));
				}

				try
				{
					MapEvents(sessionId);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [MapEvent] Reason: " + exception.Message));
				}

				try
				{
					RetailerHandoffEvents(sessionId);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [RetailerHandoffEvent] Reason: " + exception.Message));
				}

				try
				{
					UserSavePreferenceEvents(sessionId);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [UserSavePreferenceEvent] Reason: " + exception.Message));
				}

				try
				{
					PageEntryEvents(sessionId);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [PageEntryEvent] Reason: " + exception.Message));
				}
				
				try
				{
					LoginEvents(sessionId);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [LoginEvent] Reason: " + exception.Message));
				}
					
				try
				{
					DataGatewayEvents(sessionId);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [DataGatewayEvent] Reason: " + exception.Message));
				}



				// The remaining events below are related to journey planning and must be submitted in the order below to ensure realistic results are produced	

				try
				{
					JourneyPlanRequestEvents(sessionId, journeyPlanRequestId1, journeyPlanRequestId2); 
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [JourneyPlanRequestEvent] Reason: " + exception.Message));
				}
				
				try
				{
					JourneyPlanRequestVerboseEvents(sessionId, journeyPlanRequestId1, journeyPlanRequestId2);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [JourneyPlanRequestVerboseEvent] Reason: " + exception.Message));
				}	
				
				try
				{
					JourneyWebRequestEvents(sessionId, journeyPlanRequestId1, journeyPlanRequestId2);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [JourneyWebRequestEvent] Reason: " + exception.Message));
				}
					
				try
				{
					LocationRequestEvents(journeyPlanRequestId1, journeyPlanRequestId2);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [LocationRequestEvent] Reason: " + exception.Message));
				}

				try
				{
					JourneyPlanResultEvents(sessionId, journeyPlanRequestId1, journeyPlanRequestId2);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [JourneyPlanResultEvent] Reason: " + exception.Message));
				}		
					
				try
				{
					JourneyPlanResultVerboseEvents(sessionId, journeyPlanRequestId1, journeyPlanRequestId2);
				}
				catch (Exception exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error when generating event of type [JourneyPlanResultVerboseEvent] Reason: " + exception.Message));
				}
									
			}
			while (GeneratorMain.continual);
		}
	}
}
