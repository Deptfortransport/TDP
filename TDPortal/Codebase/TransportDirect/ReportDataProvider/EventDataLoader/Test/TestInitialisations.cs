// *********************************************** 
// NAME                 : TestInitialisations.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/07/2004
// DESCRIPTION  : IServiceInitialisation implementation for testing purposes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventDataLoader/Test/TestInitialisations.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:30   mturner
//Initial revision.
//
//   Rev 1.1   Jul 02 2004 10:06:32   jgeorge
//Added tests
//
//   Rev 1.0   Jul 01 2004 17:16:52   jgeorge
//Initial revision.

using System;
using System.Diagnostics;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.EventDataLoader
{
	/// <summary>
	/// IServiceInitialisation implementation for testing purposes
	/// </summary>
	public class TestInitialisations : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{

			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			// Enable the Event Logging Service
			ArrayList loggingErrors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[0];

			Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, loggingErrors));
			
		}

	}
}
