// ****************************************************************************** 
// NAME			: TestCostSearchRunnerInitialisation.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 23/02/2005
// DESCRIPTION	: Implementation of the TestCostSearchRunnerInitialisation class
// ****************************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/Test/TestCostSearchRunnerInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:44   mturner
//Initial revision.
//
//   Rev 1.6   Dec 22 2005 16:49:20   RWilby
//Added $Log  tag to file header
using System;
using System.Diagnostics;
using System.Collections;
using System.Text;

using TransportDirect.Common.Logging;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.JourneyControl;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
	/// Initialisation class to be included in the CostSerachRunner test harnesses
	/// </summary>
	public class TestCostSearchRunnerInitialisation : IServiceInitialisation
	{
		
		public void Populate(Hashtable serviceCache)
		{
			// Add cryptographic scheme
			serviceCache.Add(ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

			// Enable PropertyService					
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			// Enable logging service.
			ArrayList errors = new ArrayList();
			try
			{    
				IEventPublisher[] customPublishers = new IEventPublisher[0];

				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw;
			}		

			// Enable DataServices
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory());
			
			// Enable SessionManager
			serviceCache.Add(ServiceDiscoveryKey.SessionManager, new TestDummySessionManager("sessionID"));

			// Enable CostSearchFacade
			serviceCache.Add(ServiceDiscoveryKey.CostSearchFacade, new CostSearchFacadeFactory());

			// Enable Air Data Provider
			serviceCache.Add(ServiceDiscoveryKey.AirDataProvider, new AirDataProviderFactory());

			//use mock CJP
			serviceCache.Add (ServiceDiscoveryKey.Cjp, new MockCjpFactory(@"C:\TDPortal\CodeBase\TransportDirect\CostSearchRunner\Test\TestCoachResultVictoriaToLeedsSingle.xml",5));

			//Add CostSearchRunnerFactory
			serviceCache.Add(ServiceDiscoveryKey.CostSearchRunnerCaller,new CostSearchRunnerCallerFactory());

				
		}
	}
}
