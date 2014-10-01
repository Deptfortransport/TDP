// **************************************************************** 
// NAME			: TestCostSearchInitialisation.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 02/03/2005
// DESCRIPTION	: Initialises services for CostSearch test classes
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestCostSearchInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:21:54   mturner
//Initial revision.
//
//   Rev 1.5   Dec 21 2005 17:41:22   RWilby
//Updated to fix test code
using System;
using System.Diagnostics;
using System.Collections;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Initialisation class to be included in the CostSearch test classes
	/// </summary>
	public class TestCostSearchInitialisation : IServiceInitialisation
	{
		
	
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService					
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			// Enable logging service.
			ArrayList errors = new ArrayList();
			try
			{    
				IEventPublisher[] customPublishers = new IEventPublisher[0];

				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException tdEx)
			{
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw tdEx;
			}	
			
			// Enable mock CJP
			serviceCache.Add(ServiceDiscoveryKey.Cjp, 
				new TransportDirect.UserPortal.JourneyControl.TestMockMatchingCjpFactory( GetCurrentFolderPath() + @"\Properties.xml") );

			//Add CJP Manager to use mock CJP
			serviceCache.Add (ServiceDiscoveryKey.CjpManager, new CjpManagerFactory());

			// Enable GISQuery
			serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TransportDirect.UserPortal.PricingRetail.Domain.TestStubGisQuery());

			// Enable DataServices
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory());

			// Enable Cache object
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TestJourneyControlMockCache () );

			//Add CoachRoutesQuotaFareProvider
			serviceCache.Add(ServiceDiscoveryKey.CoachRoutesQuotaFareProvider,new TestMockCoachRoutesQuotaFaresProvider());

			//Enable RoutePriceSupplierFactory
			serviceCache.Add(ServiceDiscoveryKey.RoutePriceSupplierFactory,new TestMockRoutePriceSupplierFactory());

			//enable PricedServiceSupplier service
			serviceCache.Add(ServiceDiscoveryKey.PricedServiceSupplierFactory, new TestMockPricedServicesSupplierFactory());
			
			//enable JourneyFareFilter service
			serviceCache.Add(ServiceDiscoveryKey.JourneyFareFilterFactory, new TestMockJourneyFareFilterFactory());

			serviceCache.Add( ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory() );

			// Enable Air Data Provider
			serviceCache.Add(ServiceDiscoveryKey.AirDataProvider, new AirDataProviderFactory());

			// Enable Place Data Provider
			serviceCache.Add(ServiceDiscoveryKey.PlaceDataProvider, new PlaceDataProviderFactory());
										
		}

		/// <summary>
		/// Helper function to get the current execution path
		/// </summary>
		/// <returns>execution location path</returns>
		private string GetCurrentFolderPath()
		{   			
			string replaceVal = @"file:\";
			string folderPath = string.Empty;			
			folderPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
			folderPath = folderPath.Replace(replaceVal, string.Empty);			
			return folderPath;
			
		}
	}
}
