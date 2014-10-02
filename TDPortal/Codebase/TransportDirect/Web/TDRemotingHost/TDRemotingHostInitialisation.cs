// ***********************************************
// NAME 		: TDRemotingHostInitialisation.cs.cs
// AUTHOR 		: Andrew Sinclair
// DATE CREATED : 15/06/2005
// DESCRIPTION 	: This implements the IService Initialisation
// interface and initialises the services required by the 
// JourneyPlanRunnerCaller.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TDRemotingHost/TDRemotingHostInitialisation.cs-arc  $ 
//
//   Rev 1.12   Aug 24 2012 16:06:40   rbroddle
//Added CalorieCalculator service initialisation.
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER
//
//   Rev 1.11   Sep 29 2010 11:27:54   apatel
//EES Web Services for Cycle code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.10   Feb 09 2010 10:18:04   apatel
//Organised using statements
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Feb 09 2010 10:15:32   apatel
//Added TD International Manager to service
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Feb 09 2010 09:51:32   mmodi
//Added International Planner Data to the service
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Jan 29 2010 12:08:26   mmodi
//Added InternationalPlanner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.6   Oct 06 2009 14:12:04   mmodi
//Added EnvironmentalBenefits
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.5   Aug 04 2009 14:20:22   mmodi
//Added Journey emissions to the service discovery
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.4   Jun 03 2009 11:30:20   mmodi
//Added CoordinateCovertorFactory to the initialisation
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.3   Mar 26 2009 15:57:30   build
//Manually merged in from tech refresh branch - backward compatible change
//
//   Rev 1.2.1.0   Mar 19 2009 18:20:48   build
//Changed to only initialise Business Objects if running in a 32 bit application pool to allow seperate 32 bit and 64 bit versions to be deployed for fares and  journey planning respectively.
//
//   Rev 1.2   Jan 11 2009 18:04:14   mmodi
//Updated to initialise ZPBO or FBO pool
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.1   Dec 04 2008 11:24:18   mturner
//Added support for Cycle Planning
//
//   Rev 1.0   Nov 08 2007 13:58:50   mturner
//Initial revision.
//
//   Rev 1.10   May 25 2007 16:22:16   build
//Automatically merged from branch for stream4401
//
//   Rev 1.9.1.0   May 09 2007 14:46:58   mmodi
//Added CoachFaresLookup
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.9   Nov 28 2005 16:09:26   mguney
//ExceptionalFaresLookup included.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.8   Nov 15 2005 18:32:16   RPhilpott
//Add PlaceDataProvider.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.7   Nov 11 2005 16:57:06   RPhilpott
//Remove unnecessary RetailXMLSchema initialisation.
//Resolution for 3018: DN040: No train or coach fares returned in SITest
//
//   Rev 1.6   Nov 09 2005 12:31:54   build
//Automatically merged from branch for stream2818
//
//   Rev 1.5.1.7   Nov 03 2005 17:52:38   RWilby
//Added PricedServicesSupplierFactory
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5.1.6   Nov 02 2005 18:04:40   RWilby
//Added RoutePriceSupplierFactory to service cache
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5.1.5   Oct 28 2005 16:37:30   mguney
//CoachFareInterfaces, CoachFares included.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5.1.4   Oct 28 2005 15:51:16   mguney
//CoachFaresInterfaceFactory, CoachOperatorLookup, TimeBasedFareSupplierFactory, JourneyFareFilterFactory included.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5.1.3   Oct 26 2005 11:45:44   RWilby
//Updated CoachRoutes namespace
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5.1.2   Oct 18 2005 15:22:02   jgeorge
//Added RetailBusinessObjectsFacade
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.5.1.1   Oct 17 2005 13:33:12   RWilby
//Added CostSearchFacade, RetailXmlSchema, RetailerCatalogue, AdditionalData services to serviceCache
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.5.1.0   Oct 12 2005 15:03:32   RWilby
//Added CoachRoutesQuotaFareProvider to ServiceCache
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.5   Aug 19 2005 18:49:08   asinclair
//Merge for stream2572
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.4   Aug 18 2005 10:25:06   RWilby
//Merge Fix for stream2559
//Added ExternalLinkService to ServiceCache
//
//   Rev 1.3   Jul 15 2005 13:43:22   NMoorhouse
//Changes to support Bookmark Service running from Remote (App) Server
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.2   Jul 04 2005 18:49:08   asinclair
//Added header and removed TODO
using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.DepartureBoardService.MobileBookmark;
using TransportDirect.UserPortal.EnvironmentalBenefits;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.InternationalPlanner;
using TransportDirect.UserPortal.InternationalPlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyEmissions;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;
using TransportDirect.UserPortal.PricingRetail.CoachFares;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.RetailBusinessObjects;
using TransportDirect.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.TDRemotingHost
{
	/// <summary>
	/// Summary description for TDRemotingHostInitialisation.
	/// </summary>
	public class TDRemotingHostInitialisation : IServiceInitialisation	
	{
		private const string DefaultLogFilename = "td.UserPortal.TDRemotingHost.log";

		#region IServiceInitialisation Members

		public void Populate(System.Collections.Hashtable serviceCache)
		{

			TextWriterTraceListener logTextListener = null;
			ArrayList errors = new ArrayList();

			try
			{
				// initialise .NET file trace listener for use prior to TDTraceListener
				string logfilePath = ConfigurationManager.AppSettings["DefaultLogFilepath"];
				Stream logFile = File.Create(logfilePath + "\\" + DefaultLogFilename);
				logTextListener = new System.Diagnostics.TextWriterTraceListener(logFile);
				Trace.Listeners.Add(logTextListener);
				
				// Add cryptographic scheme
				serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

				// initialise properties service
				serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

				// initialise logging service	
				IEventPublisher[]	customPublishers = new IEventPublisher[0];			
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Started - Populate method"));


                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: Cjp, CjpManager, TDMapHandoff"));

				//Add CJP
				serviceCache.Add( ServiceDiscoveryKey.Cjp, new CjpFactory());
				
				//Add CJP Manager
				serviceCache.Add (ServiceDiscoveryKey.CjpManager, new CjpManagerFactory());

				//Add TDMapHandoff
				serviceCache.Add (ServiceDiscoveryKey.TDMapHandoff, new TDMapHandoffFactory());


                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: GisQuery, GazetteerFactory, PlaceDataProvider"));

				//Add Location Search
				serviceCache.Add (ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
				serviceCache.Add (ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());
				serviceCache.Add (ServiceDiscoveryKey.PlaceDataProvider, new PlaceDataProviderFactory());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: Cache, DataChangeNotification, AirDataProvider, DataServices"));

				//Add Cache object
				serviceCache.Add( ServiceDiscoveryKey.Cache, new TDCache() );

				serviceCache.Add( ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory() );

				serviceCache.Add( ServiceDiscoveryKey.AirDataProvider, new AirDataProviderFactory());

				// Attention! here the DataServices component is loaded passing a null ResourceManager.
				// This is because it is used specifically within the WebService which doesn't use resources.
				serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: BookmarkWebService, MobileBookmark, ExternalLinkService"));

				// Adding the instance of BookmarkWebService object... need to be before MobileBookmark!
				serviceCache.Add(ServiceDiscoveryKey.BookmarkWebService, new BookmarkServiceFactory());

				// Adding the instance of ITDmobileBookmark
				serviceCache.Add(ServiceDiscoveryKey.MobileBookmark, new MobileBookmarkFactory());  

				// Enable External Links Service
				serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new ExternalLinksFactory());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: LocalityTravelineLookup, BayTextFilter"));

				// Enable locality Traveline Lookup
				serviceCache.Add(ServiceDiscoveryKey.LocalityTravelineLookup, new LocalityTravelineLookup());

				// Enable Bay Text Filter
				serviceCache.Add(ServiceDiscoveryKey.BayTextFilter, new BayTextFilter());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: CoachRoutesQuotaFareProvider, CostSearchFacade, RetailerCatalogue, AdditionalData"));

				//Add CoachRoutesQuotaFareProvider
				serviceCache.Add(ServiceDiscoveryKey.CoachRoutesQuotaFareProvider,new CoachRoutesQuotaFareProvider());

				// Enable Cost Search Facade
				serviceCache.Add(ServiceDiscoveryKey.CostSearchFacade, new CostSearchFacadeFactory());

				// Enable RetailerCatalogue
				serviceCache.Add (ServiceDiscoveryKey.RetailerCatalogue, new RetailerCatalogueFactory());

				// Enable AdditionalData
				serviceCache.Add( ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory() );

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: CoachFaresInterface, CoachOperatorLookup, TimeBasedFareSupplier, JourneyFareFilterFactory"));

				// Add Factory for Coach Fare Interface.
				serviceCache.Add(ServiceDiscoveryKey.CoachFaresInterface,new CoachFaresInterfaceFactory());

				//Enable Coach Operator Lookup
				serviceCache.Add(ServiceDiscoveryKey.CoachOperatorLookup, new CoachOperatorLookup());

				//Add Factory for TimeBasedFareSupplier
				serviceCache.Add(ServiceDiscoveryKey.TimeBasedFareSupplier,new TimeBasedFareSupplierFactory());

				//Add Factory for JourneyFareFilter
				serviceCache.Add(ServiceDiscoveryKey.JourneyFareFilterFactory,new JourneyFareFilterFactory());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: RoutePriceSupplierFactory, PricedServiceSupplierFactory, ExceptionalFaresLookup, CoachFaresLookup"));

				//Add RoutePriceSupplierFactory
				serviceCache.Add(ServiceDiscoveryKey.RoutePriceSupplierFactory,new RoutePriceSupplierFactory());

				//Add PricedServicesSupplierFactory
				serviceCache.Add(ServiceDiscoveryKey.PricedServiceSupplierFactory, new PricedServicesSupplierFactory());

				//Enable Excepional Fares Lookup
				serviceCache.Add(ServiceDiscoveryKey.ExceptionalFaresLookup, new ExceptionalFaresLookup());

				//Enable Coach Fares Lookup
				serviceCache.Add(ServiceDiscoveryKey.CoachFaresLookup, new CoachFaresLookup());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: CyclePlannerFactory, CyclePlannerManager, CoordinateConvertorFactory"));

                // Enable CyclePlannerFactory
                serviceCache.Add(ServiceDiscoveryKey.CyclePlannerFactory, new CyclePlannerFactory());

                // Enable CyclePlannerManagerFactory
                serviceCache.Add(ServiceDiscoveryKey.CyclePlannerManager, new CyclePlannerManagerFactory());

                // Enable CycleAttributeFactory
                serviceCache.Add(ServiceDiscoveryKey.CycleAttributes, new CycleAttributesFactory());

                // Enable GradientProfileManagerFactory
                serviceCache.Add(ServiceDiscoveryKey.GradientProfilerManager, new GradientProfilerManagerFactory());

                // Enable CoordinateConvertorFactory
                serviceCache.Add(ServiceDiscoveryKey.CoordinateConvertorFactory, new CoordinateConvertorFactory());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: CarCostCalculator, JourneyEmissionsFactor, CalorieCalculator"));

                // Enable CarCostCalculator
                serviceCache.Add(ServiceDiscoveryKey.CarCostCalculator, new CarCostCalculatorFactory());

                // Enable JourneyEmissionsFactor
                serviceCache.Add(ServiceDiscoveryKey.JourneyEmissionsFactor, new JourneyEmissionsFactorFactory());

                // Enable CalorieCalculator
                serviceCache.Add(ServiceDiscoveryKey.CalorieCalculator, new CalorieCalculatorFactory());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: EnvironmentalBenefitsCalculator, InternationalPlanner"));

                // Add Environmental Benefits
                serviceCache.Add(ServiceDiscoveryKey.EnvironmentalBenefitsCalculator, new EnvironmentalBenefitsCalculatorFactory());

                // Add International Planner
                serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerFactory, new InternationalPlannerFactory());

                // Add International Planner Data
                serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerDataFactory, new InternationalPlannerDataFactory());

                // Enable InternationalPlannerManagerFactory
                serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerManager, new InternationalPlannerManagerFactory());
                                
			}

			catch ( TDException tdException)
			{	
				// create message string
				StringBuilder message = new StringBuilder(100);
				message.Append(tdException.Message);

				// append error messages, if any
				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				Trace.WriteLine(message.ToString() + "ExceptionID: " + tdException.Identifier.ToString("D"));		
				throw new TDException(message.ToString(), tdException, false, tdException.Identifier);
			}
			catch (Exception exception)
			{
				Trace.WriteLine(exception.Message);
				throw exception;
			}
			finally
			{
				if( logTextListener != null )
				{
					logTextListener.Flush();
					logTextListener.Close();
					Trace.Listeners.Remove(logTextListener);
				}
			}

            try
			{
                if (IntPtr.Size == 4)
                //Only try to initialise BO pools if we are in 32bit application pool
                //if this TDRemotingHost is running in 64bit mode it will not be using them
                {
                    // Initialise BO pools (by requesting instance of pool - since use Singleton pattern)
                    // This is performed here to check properties are valid and that intialisation succeeds.
                    RBOPool.GetRBOPool();
                    LBOPool.GetLBOPool();
                    RVBOPool.GetRVBOPool();
                    SBOPool.GetSBOPool();

                    // We can only initialise ZPBO if FBO hasn't been initialised, because ZPBO can only 
                    // successfully initialise the FBO dlls if they havent been already.
                    if (ZPBOPool.UseZPBO())
                    {
                        ZPBOPool.GetZPBOPool();
                    }
                    else
                    {
                        FBOPool.GetFBOPool();
                    }
                }
			}
			catch (TDException tdException)
			{
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Failed to create retail business object pool: " + tdException.Message);
				Trace.Write(oe);
				throw new TDException(oe.Message, tdException, true, TDExceptionIdentifier.PRHBOPoolCreationFailed);
			}

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
            "INITIALISATION Completed - Populate method"));

		}

		#endregion
	}
}
