// ***********************************************
// NAME 		: TestVisitPlannerInitialisation.cs
// AUTHOR 		: Tim Mollart
// DATE CREATED : 10/10/2005
// DESCRIPTION 	: Initialisation object
// ************************************************
//$ Log: $

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.IO;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.PricingRetail;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.ExternalLinkService;

namespace TransportDirect.UserPortal.VisitPlanRunner
{

	/// <summary>
	/// Available initilisation modes.
	/// </summary>
	public enum VisitPlanTestInitialisationMode { UseMockVisitPlanRunnerCaller, UseRealVisitPlanRunnerCaller };


	/// <summary>
	/// Summary description for TestSessionManagerInitialisation.
	/// </summary>
	public class TestVisitPlanRunnerInitialisation : IServiceInitialisation
	{

		public static string TestSessionID = "upgl0l55hohboh2evw5ew0br00000001";


		/// <summary>
		/// Stores the runner which this initialisation class will use to populate
		/// service discovery. This will be either a mock or a real visit plan runner
		/// caller. 
		/// </summary>
		private IVisitPlanRunnerCaller runner;


		/// <summary>
		/// Default constructor. Initilisation will get a mock
		/// VisitPlanRunner caller object.
		/// </summary>
		public TestVisitPlanRunnerInitialisation()
		{
			// Set runner to be a mock visitplanrunnercaller
			runner = new TestMockVisitPlanRunnerCaller();
		}


		/// <summary>
		/// Specify the initialisation mode.
		/// </summary>
		/// <param name="mode">Select from a real or mock visit plan runner caller.</param>
		public TestVisitPlanRunnerInitialisation(VisitPlanTestInitialisationMode mode)
		{
			// Dependant on the supplied mode set runner to be either a
			// mock or a real visitplanrunnercaller.
			if (mode == VisitPlanTestInitialisationMode.UseMockVisitPlanRunnerCaller)
			{
				runner = new TestMockVisitPlanRunnerCaller();
			}
			else
			{
				runner = new VisitPlanRunnerCaller();
			}
		}


		/// <summary>
		/// Populate mothod.
		/// </summary>
		/// <param name="serviceCache">Service cache to populate.</param>
		public void Populate(Hashtable serviceCache)
		{

            string s = ConfigurationManager.AppSettings["test"];
			// Add cryptographic scheme
			ArrayList errors = new ArrayList();

			// Add cryptographic scheme
			serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

			// initialise properties service
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			// initialise logging service	
			IEventPublisher[]	customPublishers = new IEventPublisher[0];			
			Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));			

			// Enable Dataservices
			serviceCache.Add( ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory() );

			// Add the required VisitPlanRunnerCaller. This will either be a mock
			// one or a real one dependant on how this class was initialised.
			serviceCache.Add ( ServiceDiscoveryKey.VisitPlanRunnerCaller, runner );

			// Mock Session Manager
			serviceCache.Add ( ServiceDiscoveryKey.SessionManager, new TestMockSessionManager(false, TestSessionID, false) );

			// CJP Manager (Real)
			serviceCache.Add ( ServiceDiscoveryKey.CjpManager, new CjpManagerFactory() );

			// Mock CJP 
			string dataFile = Directory.GetCurrentDirectory() + @"\VisitPlanResult1.xml";
			serviceCache.Add ( ServiceDiscoveryKey.Cjp, new MockCjpFactory(dataFile, 0, dataFile, 0) );

			// Mock air data provider
			serviceCache.Add ( ServiceDiscoveryKey.AirDataProvider, new TestMockAirDataProvider() );

			// External link service
			serviceCache.Add ( ServiceDiscoveryKey.ExternalLinkService, new ExternalLinksFactory() );

			// Mock Map Hand Off
			serviceCache.Add ( ServiceDiscoveryKey.TDMapHandoff, new TestStubTDMapHandoff() );
			
			// Mock Test Journey Control Cache
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TestJourneyControlMockCache () );

			// Mock GIS Query
			serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TransportDirect.UserPortal.PricingRetail.Domain.TestStubGisQuery());

		}
	}
}
