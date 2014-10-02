// ***********************************************
// NAME 		: TestSessionManagerInitialisation.cs
// AUTHOR 		: Atos Origin
// DATE CREATED : 05/11/2003
// DESCRIPTION 	: A mock testing object for the Cache service
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestSessionManagerInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:06   mturner
//Initial revision.
//
//   Rev 1.7   Mar 13 2006 18:28:00   rhopkins
//Allow specification of whether to use simpleSession.
//Also added CarCostCalculator
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Nov 09 2005 16:22:50   RPhilpott
//Merge for stream2818
//
//   Rev 1.5   Nov 01 2005 15:12:14   build
//Automatically merged from branch for stream2638
//
//   Rev 1.4.1.0   Sep 27 2005 09:00:54   pcross
//Addition of ExternalLinksService to test session manager
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Mar 21 2005 13:42:40   jgeorge
//Removed error handling to make it easier to find bugs when an unexpected error occurs.
//
//   Rev 1.3   Mar 18 2005 09:19:28   jgeorge
//Changed order of initialisation and added PriceSupplierFactory stub
//
//   Rev 1.2   Feb 04 2005 11:26:24   RScott
//Added DataServes to cache on initialisation
//
//   Rev 1.1   May 10 2004 15:11:28   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.0   Mar 17 2004 17:41:40   CHosegood
//Initial Revision
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.IO;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.ExternalLinkService;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TestSessionManagerInitialisation.
	/// </summary>
	public class TestSessionManagerInitialisation : IServiceInitialisation
	{
		bool hasRealSession;
		private const string defaultLogFilename = "td.UserPortal.SessionManager.log";

		public TestSessionManagerInitialisation()
		{
			hasRealSession = false;
		}

		public TestSessionManagerInitialisation( bool HasRealSession )
		{
			hasRealSession = HasRealSession;
		}

		public void Populate(Hashtable serviceCache)
		{
			// Add cryptographic scheme
			TextWriterTraceListener logTextListener = null;
			ArrayList errors = new ArrayList();

			try
			{
				// initialise .NET file trace listener for use prior to TDTraceListener
				//string logfilePath = ConfigurationSettings.AppSettings[Keys.DefaultLogPath];
				Stream logFile = File.Create( defaultLogFilename );
				logTextListener = new System.Diagnostics.TextWriterTraceListener(logFile);
				Trace.Listeners.Add(logTextListener);

				// Add cryptographic scheme
				serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

				// initialise properties service
				serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

				// initialise logging service	
				IEventPublisher[]	customPublishers = new IEventPublisher[0];			
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));			

				Trace.Listeners.Remove(logTextListener);

				// Enable SessionManager
				serviceCache.Add (ServiceDiscoveryKey.SessionManager, new TestMockSessionManager(false, "ssss", hasRealSession));

				// Enable GISQuery
				serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TransportDirect.UserPortal.JourneyControl.TestStubGisQuery());

				// Enable TDMapHandoff
				serviceCache.Add(ServiceDiscoveryKey.TDMapHandoff, new TestStubTDMapHandoff());

				// Enable caching
				serviceCache.Add( ServiceDiscoveryKey.Cache, new TestJourneyControlMockCache() );

				// Enable Dataservices
				serviceCache.Add( ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory() );

				// Enable TimeBasedFareSupplier
				serviceCache.Add(ServiceDiscoveryKey.TimeBasedFareSupplier, new TestStubTimeBasedFareSupplierFactory());

				// Enable External Links
				serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new DummyExternalLinkService());

				// Enable Car Cost Calculator
				serviceCache.Add(ServiceDiscoveryKey.CarCostCalculator, new CarCostCalculatorFactory());
			}
			finally
			{
				if ( logTextListener != null )
				{
					logTextListener.Flush();
					logTextListener.Close();
					Trace.Listeners.Remove(logTextListener);
				}
			}
		}
	}
}
