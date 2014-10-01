// *********************************************** 
// NAME			: TestCJPManager.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestCJPManager class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestCJPManager.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:14   mturner
//Initial revision.
//
//   Rev 1.25   Mar 30 2006 12:17:10   build
//Automatically merged from branch for stream0018
//
//   Rev 1.24.1.0   Feb 27 2006 12:13:58   RPhilpott
//Integrated Air changes
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.24   Feb 09 2006 17:56:46   jmcallister
//Project Newkirk
//

using System;
using System.Collections;
using System.IO;
using System.Diagnostics;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// NUnit tests for CJPmanager class.
	/// </summary>
	[TestFixture]
	public class TestCJPManager
	{
		private TestJourneyRequestData requestData;
		
		public TestCJPManager()
		{
		}

		[TestFixtureSetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestCJPInitialisation());			

			Trace.Listeners.Remove("TDTraceListener");
		
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			DateTime outwardDateTime = DateTime.Now;
			DateTime returnDateTime  = DateTime.Now + new TimeSpan(1, 0, 0);

			requestData = new TestJourneyRequestData(outwardDateTime, returnDateTime);

		}


		[TestFixtureTearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}


		[Test]
		public void CallJourneyManager()
		{
			
			ITDJourneyRequest request = requestData.InitialiseDefaultRequest(true);
			
			CJPManager manager = new CJPManager();
			ITDJourneyResult result =  manager.CallCJP( request, "mysessionid", 0, false, false, "en", false); 

			request = requestData.InitialiseDefaultRequest(false);
			manager = new CJPManager();
			result = manager.CallCJP(request, "returnsessionid", 0, false, 2301, 3, true, "cy"); 

			request = requestData.InitialiseDefaultCityToCityRequest(false);
			manager = new CJPManager();
			result = manager.CallCJP(request, "returnsessionid", 0, false, 2301, 3, true, "cy"); 
		
		}
	}


	public class TestCJPInitialisation : IServiceInitialisation
	{
		private MockCjpFactory factory = new MockCjpFactory();

		public TestCJPInitialisation()
		{
		}

		public void Populate(Hashtable serviceCache)
		{
			// set parameters for CJP stub ...
			factory.FileName = "..\\..\\bin\\debug\\TestJourneyResult3.xml";
			factory.Delay = 1;
			
			serviceCache.Add(ServiceDiscoveryKey.Cjp, factory);
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add(ServiceDiscoveryKey.CjpManager, new MockCjpFactory());
			serviceCache.Add(ServiceDiscoveryKey.Cache, new TestJourneyControlMockCache());
			serviceCache.Add(ServiceDiscoveryKey.AirDataProvider, new TestMockAirDataProvider());
			serviceCache.Add(ServiceDiscoveryKey.TDMapHandoff, new TestStubTDMapHandoff());
			serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TestStubGisQuery());
			serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new TestMockExternalLinks());
		}
	}

}
