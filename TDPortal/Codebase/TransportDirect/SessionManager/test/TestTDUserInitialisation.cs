using System;
using System.Collections;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TestTDUserInitialisation.
	/// </summary>
	public class TestTDUserInitialisation : IServiceInitialisation
	{
		public TestTDUserInitialisation()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public void Populate(Hashtable serviceCache)
		{
			// Add property service
			serviceCache.Add( ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory() );

			// Add cryptographic scheme
			serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );
		}

	}
}
