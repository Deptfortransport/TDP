using System;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// ServiceDiscovery.IServiceInitialisation class for testing the subclasses of AsyncCallState
	/// </summary>
	public class TestAsyncCallStateInitialisation : IServiceInitialisation
	{
		public TestAsyncCallStateInitialisation()
		{
		}

		public void Populate(System.Collections.Hashtable serviceCache)
		{
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add(ServiceDiscoveryKey.SessionManager, new TestMockSimpleSessionManager());
		}

	}
}
