using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.SeasonalNoticeBoardImport
{
	/// <summary>
	/// Summary description for TestInitialiasation.
	/// </summary>
	/// <summary>
	/// Initialisation class to be included in test harnesses
	/// </summary>
	public class TestInitialisation : IServiceInitialisation
	{
		public TestInitialisation()
		{
		}

		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			// Enable PropertyService
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			// initialise logging service
			ArrayList errors = new ArrayList();
			IEventPublisher[]	customPublishers = new IEventPublisher[0];			
			Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
		}


	}
}
