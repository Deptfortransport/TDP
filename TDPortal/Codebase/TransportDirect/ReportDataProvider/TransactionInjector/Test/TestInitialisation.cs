using System;
using System.Collections;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Summary description for TestInitialisation.
	/// </summary>
	public class TestInitialisation : IServiceInitialisation
	{
		public TestInitialisation()
		{
			
		}

		public void Populate(Hashtable serviceCache)
		{
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
		}
	}
}
