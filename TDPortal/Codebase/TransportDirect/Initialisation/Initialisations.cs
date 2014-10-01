#region History
// *********************************************** 
// NAME                 : Initialisations.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Initialisation class for ASP.NET applications and Console Applications
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Initialisation/Initialisations.cs-arc  $ 
//
//   Rev 1.25   Mar 21 2013 10:12:56   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.24   Aug 28 2012 10:19:52   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.23   Aug 24 2012 15:52:20   rbroddle
//Added CalorieCalculator service initialisation
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER
//
//   Rev 1.22   Nov 18 2010 09:37:14   apatel
//Updated to implement cached route restriction information provider
//Resolution for 5639: Fares page breaks with connection time out errors
//
//   Rev 1.21   Jul 01 2010 12:47:26   apatel
//Updated for duplicate tiploc provider
//
//   Rev 1.20   Jun 16 2010 10:21:10   apatel
//Updated to implement auto-suggest functionality
//
//   Rev 1.19   Feb 11 2010 14:24:20   rbroddle
//Added Initialisation of International Planner Gaz
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.18   Feb 09 2010 10:11:36   apatel
//Updated for International planner control
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.17   Feb 09 2010 09:51:18   mmodi
//Added International Planner Data to the service
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.16   Jan 29 2010 12:07:14   mmodi
//Added InternationalPlanner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.15   Oct 06 2009 14:00:30   mmodi
//Added EnvironmentalBenefits
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.14   Sep 28 2009 10:40:44   PScott
//CCN530 Social BookMarking
//
//   Rev 1.13   Sep 14 2009 10:26:52   apatel
//Departure Board Service Initialisation added
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.12   Aug 04 2009 13:52:18   mmodi
//Updated to load JourneyEmissionsFactor from JourneyEmissions project
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.11   Jun 03 2009 11:17:30   mmodi
//Initialise the Coordinate convertor
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.10   Oct 14 2008 15:11:14   mmodi
//Manual merge for stream5014
//
//   Rev 1.9   Jul 08 2008 09:25:12   apatel
//Accessibility link CCN 458 updates
//
//   Rev 1.8   Jul 03 2008 13:27:24   apatel
//change the namespage zonalaccessibility to zonalservices
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.7   Jun 27 2008 09:40:52   apatel
//CCN - 458 Accessibility Updates - Improved linking
//
//   Rev 1.6.1.2   Oct 10 2008 15:54:18   mmodi
//Updated for cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6.1.1   Jul 18 2008 13:46:36   mmodi
//Added gradient profiler
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6.1.0   Jun 20 2008 15:01:40   mmodi
//Updated for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6   Apr 09 2008 18:23:58   mmodi
//Added LatestNewsService
//Resolution for 4808: Del 10 - Still need to write Latest News updater...
//
//   Rev 1.5   Mar 10 2008 15:17:38   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Feb 13 15:00:00 mmodi
//Added additional logging during initialisation
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//   Rev 1.2   Nov 29 2007 12:30:30   mturner
//Changed E-mail functionality namespace to be System.Net.Mail to remove .Net2 compiler warning
//
//   Rev 1.1   Nov 29 2007 10:37:24   mturner
//Updated for Del 9.8
//
//   Rev 1.58   Oct 25 2007 15:27:28   mmodi
//Added LocationInformationService 
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.57   May 25 2007 16:19:26   build
//Automatically merged from branch for stream4401
//
//   Rev 1.56.1.0   May 09 2007 14:42:26   mmodi
//Added CoachFaresLookup
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.56   Mar 06 2007 12:28:16   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.55.1.0   Feb 27 2007 10:34:14   mmodi
//Added JourneyEmissionsFactor
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.55   Jan 09 2007 15:18:04   dsawe
//added PartnerCatalogueFactory
//Resolution for 4331: iFrames for LastMinute.com
//
//   Rev 1.54   Oct 06 2006 10:42:32   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.53.1.0   Aug 04 2006 13:40:48   esevern
//added carparkcatalogue
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.53   Feb 23 2006 10:43:44   RWilby
//Merged stream3129
//
//   Rev 1.52   Feb 16 2006 15:35:42   halkatib
//Merged stream 0002 into the trunk.
//
//   Rev 1.51   Dec 13 2005 11:30:56   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.50   Nov 28 2005 16:07:38   mguney
//ExceptionalFaresLookup included.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.49   Nov 10 2005 19:47:30   RPhilpott
//Add extra entries to support for running fares providers as local rather than remotable objects.
//
//   Rev 1.48   Nov 09 2005 18:56:38   RPhilpott
//Merge for stream2818 - corrections.
//
//   Rev 1.47   Nov 09 2005 16:54:32   jgeorge
//Manual merge for stream2818 (Search by Price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.46   Oct 31 2005 17:40:30   tolomolaiye
//Merge for stream 2638 (Visit Planner)
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.45   Sep 26 2005 17:00:32   rhopkins
//Merge stream 2596 back into trunk
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.44.2.0   Sep 21 2005 10:40:26   asinclair
//New branch for 2638 with Del 7.1
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.42.1.3   Sep 15 2005 16:52:18   kjosling
//Merged with version 1.44
//
//   Rev 1.42.1.2   Aug 23 2005 13:52:32   kjosling
//Added new DiscoveryKey for SuggestionLinks
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.42.1.1   Aug 12 2005 11:11:36   NMoorhouse
//DN058 Park And Ride, Added Park And Ride
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.42.1.0   Jul 28 2005 09:59:12   NMoorhouse
//Merge Stream 2559 -> Stream 2596
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.42   Jul 05 2005 13:54:20   asinclair
//Merge for stream2557
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.41.2.0   Jun 15 2005 11:40:12   asinclair
//Added JourneyPlanRunnerCallerFactory
//
//   Rev 1.41   Mar 14 2005 15:53:12   COwczarek
//Service discovery can now return search runner and cost
//search facade factories.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.40   Mar 04 2005 09:16:50   jgeorge
//Added DiscountCardCatalogue
//
//   Rev 1.39   Feb 22 2005 13:24:48   esevern
//Car Costing - added CarCostCalculator
//
//   Rev 1.38   Nov 08 2004 10:35:22   Schand
/// Resolved namespace problem with SeasonalNoticeBoardimport
//
//   Rev 1.37   Nov 02 2004 18:03:10   Schand
//Added entry for SeasonalNoticeboardImport
//
//   Rev 1.36   Nov 01 2004 15:53:50   jgeorge
//Added PlaceDataProvider creation
//
//   Rev 1.35   Jul 21 2004 14:49:38   jgeorge
//Removed "using" statements
//
//   Rev 1.34   Jul 21 2004 11:01:46   jgeorge
//Removed database publishers from initialisations
//
//   Rev 1.33   Jul 20 2004 15:21:28   jmorrissey
//Added TDPCustomEventPublisher for user feedback events
//
//   Rev 1.32   Jun 28 2004 13:55:14   rgreenwood
//Added DataChangeNotification
//
//   Rev 1.31   May 13 2004 09:41:24   jgeorge
//AirDataProvider changes
//
//   Rev 1.30   May 12 2004 16:21:14   jbroome
//ScriptRepository changes
//
//   Rev 1.29   May 12 2004 16:01:04   jgeorge
//Added AirDataProvider
//
//   Rev 1.28   Apr 30 2004 13:50:52   jbroome
//DEL 5.4 Merge
//Added ScriptRepository
//
//   Rev 1.27   Nov 28 2003 15:43:04   COwczarek
//Enable RetailXMLSchema service
//Resolution for 451: Retail Handoff does not need to read XML schema for each request
//
//   Rev 1.26   Nov 06 2003 16:26:20   PNorell
//Added Caching
//
//   Rev 1.25   Oct 30 2003 14:52:36   PNorell
//Added new initialisation for crypto.
//
//   Rev 1.24   Oct 24 2003 13:06:06   esevern
//took out the stuff i put in last time which was wrong!
//
//   Rev 1.23   Oct 24 2003 12:19:54   esevern
//amended custom email publisher population to use string const from Common.Logging.Keys class
//
//   Rev 1.22   Oct 22 2003 16:57:40   COwczarek
//Enable PriceSupplierFactory
//
//   Rev 1.21   Oct 21 2003 19:58:38   CHosegood
//Added additional data module
//
//   Rev 1.20   Oct 09 2003 18:30:44   JMorrissey
//Logged full trace listener error message to default listener.
//
//   Rev 1.19   Oct 08 2003 11:00:58   JMorrissey
//Added TravelNewsHandlerFactory to serviceCache
//
//   Rev 1.18   Oct 08 2003 09:29:58   COwczarek
//Add RetailerCatalogueFactory to service cache. Replace obsolete TDExcpetion constructor/method invocations.
//
//   Rev 1.17   Sep 29 2003 16:37:00   geaton
//Included TDTraceListener errors in the exception message.
//
//   Rev 1.16   Sep 29 2003 10:01:18   hahad
//Added CustomEmail for InitialFeedbackPage
//
//   Rev 1.15   Sep 25 2003 15:12:02   PNorell
//Commented out non-working code.
//Added TODO comment.
//
//   Rev 1.14   Sep 25 2003 13:41:50   PNorell
//Updated refrences and using statement to the System.Web.Mail  to ensure it compiles.
//
//   Rev 1.13   Sep 25 2003 13:05:36   hahad
//added CustomEmailPublisher
//
//   Rev 1.12   Sep 25 2003 11:44:34   RPhilpott
//Map handoff and MI logging changes
//
//   Rev 1.11   Sep 20 2003 16:22:46   RPhilpott
//Add Location Service support
//
//   Rev 1.10   Sep 19 2003 17:45:18   RPhilpott
//Add CJP support to AspNetInitialisation
//
//   Rev 1.9   Sep 18 2003 09:57:06   jcotton
//Changes for intitial screenflow integration work
//
//   Rev 1.8   Sep 16 2003 15:38:12   passuied
//latest working version
//
//   Rev 1.7   Sep 16 2003 12:24:50   passuied
//Added constructor with resourceManager as a parameter
//
//   Rev 1.6   Sep 09 2003 16:12:50   PNorell
//Error in variable naming.
//
//   Rev 1.5   Sep 09 2003 14:59:46   PNorell
//Added support for changing the date reference.
//
//   Rev 1.4   Aug 27 2003 17:01:28   PScott
//Update for console apps.
//
//   Rev 1.3   Aug 19 2003 10:54:00   PNorell
//Added support for Session Manager
//
//   Rev 1.2   Jul 23 2003 10:22:48   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.1   Jul 17 2003 14:50:18   passuied
//changed header
#endregion

using System;
using System.Collections;
using System.Globalization;
using System.Diagnostics;
using System.Resources;
using System.Text;
using System.Web;
using System.Net.Mail;

using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Partners;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.CostSearchRunner;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager;
using TransportDirect.UserPortal.EnvironmentalBenefits;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.InternationalPlanner;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyEmissions;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.LatestNewsService;
using TransportDirect.UserPortal.LocationInformationService;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.LocationService.Cache;
using TransportDirect.UserPortal.LocationService.DropDownLocationProvider;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;
using TransportDirect.UserPortal.PricingRetail.CoachFares;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.UserPortal.SeasonalNoticeBoardImport;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.SuggestionLinkService;
using TransportDirect.UserPortal.SocialBookMarkingService;
using TransportDirect.UserPortal.TimeBasedPriceRunner;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.VisitPlanRunner;
using TransportDirect.UserPortal.ZonalServices;
using TransportDirect.UserPortal.DepartureBoardService;

using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.InternationalPlannerControl;

namespace TransportDirect.Common.ServiceDiscovery.Initialisation
{
	/// <summary>
	/// Summary description for Initialisations.
	/// </summary>
	public class AspNetInitialisation : IServiceInitialisation
	{
		private TDResourceManager rm = null;
		public AspNetInitialisation(TDResourceManager rm)
		{
			this.rm = rm;
		}

		public void Populate(Hashtable serviceCache)
		{
			ArrayList errors = new ArrayList();

			// Add cryptographic scheme
			serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

			// Enable PropertyService
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			try
			{
				// create custom email publisher
				IEventPublisher[] customPublishers = new IEventPublisher[1];	
				customPublishers[0] = 
					new CustomEmailPublisher("EMAIL",
					Properties.Current["Logging.Publisher.Custom.EMAIL.Sender"],
					MailPriority.Normal,
					Properties.Current["Logging.Publisher.Custom.EMAIL.SMTPServer"],
					Properties.Current["Logging.Publisher.Custom.EMAIL.WorkingDir"],
					errors);
			
				// create and add TDTraceListener instance to the listener collection	
				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
			}
			catch (TDException tdEx)
			{
				// create message string
				StringBuilder message = new StringBuilder(100);
				message.Append(tdEx.Message); // prepend with existing exception message

				// append all messages returned by TDTraceListener constructor
				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				// log message using .NET default trace listener
				Trace.WriteLine(message.ToString() + "ExceptionID:" + tdEx.Identifier.ToString());			

				// rethrow exception - use the initial exception id as the id
				throw new TDException(message.ToString(), tdEx, false, tdEx.Identifier);
			}

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Started - Populate method"));

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: SessionManager, Cache, DataServices, DataChangeNotification, PageController"));

			// Enable SessionManager
			serviceCache.Add (ServiceDiscoveryKey.SessionManager, new TDSessionManagerFactory());

			// Enable Cache object
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TDCache() );
			
			// Enable DataServices
			serviceCache.Add (ServiceDiscoveryKey.DataServices, new DataServicesFactory(rm));

			// Enable DataChangeNotification
			serviceCache.Add (ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());

			// Enable PageController
			serviceCache.Add (ServiceDiscoveryKey.PageController, new PageControllerFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: GisQuery, GazetteerFactory"));

			// Enable Location Search
			serviceCache.Add (ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
			serviceCache.Add (ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());
            serviceCache.Add(ServiceDiscoveryKey.CodeGazetteer, new CodeGazetteerFactory());
            
            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: LocationServiceCache"));
            
            // Enable Location Service Cache
            serviceCache.Add (ServiceDiscoveryKey.LocationServiceCache, new LocationServiceFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: Cjp, CjpManager, TDMapHandoff"));

			// Enable CJP and CJPManager
			serviceCache.Add (ServiceDiscoveryKey.Cjp, new CjpFactory());
			serviceCache.Add (ServiceDiscoveryKey.CjpManager, new CjpManagerFactory());

			// Enable TDMapHandoff
			serviceCache.Add (ServiceDiscoveryKey.TDMapHandoff, new TDMapHandoffFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: RetailerCatalogue, TravelNews, SeasonalNoticeBoardImport, AdditionalData, RetailXmlSchema"));

			// Enable RetailerCatalogue
			serviceCache.Add (ServiceDiscoveryKey.RetailerCatalogue, new RetailerCatalogueFactory());

			// Enable TravelNews
			serviceCache.Add (ServiceDiscoveryKey.TravelNews, new TravelNewsHandlerFactory());

			// Enable SeasonalNoticeBoardImport
			serviceCache.Add (ServiceDiscoveryKey.SeasonalNoticeBoardImport , new SeasonalNoticeBoardHandlerFactory());
			
			// Enable AdditionalData
			serviceCache.Add( ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory() );

			// Enable RetailXmlSchema
			serviceCache.Add(ServiceDiscoveryKey.RetailXmlSchema, new RetailXmlSchemaFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: ScriptRepository, AirDataProvider, PlaceDataProvider, InternationalPlaceGazetteer, CarCostCalculator, JourneyEmissionsFactor, CalorieCalculator"));

			// Enable ScriptRepository
			string applicationRoot = HttpContext.Current.Request.ApplicationPath;
			string configFile = HttpContext.Current.Server.MapPath(applicationRoot+Properties.Current["TransportDirect.UserPortal.ScriptRepository.ScriptsFile"]);
			serviceCache.Add(ServiceDiscoveryKey.ScriptRepository, new ScriptRepositoryFactory(applicationRoot, configFile));
			
			// Enable Air Data Provider
			serviceCache.Add(ServiceDiscoveryKey.AirDataProvider, new AirDataProviderFactory());

			// Enable Place Data Provider
			serviceCache.Add(ServiceDiscoveryKey.PlaceDataProvider, new PlaceDataProviderFactory());

			// Enable International Gaz
            serviceCache.Add(ServiceDiscoveryKey.InternationalPlaceGazetteer, new InternationalPlaceGazetteerFactory());
    

			// Enable CarCostCalculator
			serviceCache.Add(ServiceDiscoveryKey.CarCostCalculator, new CarCostCalculatorFactory());

			// Enable JourneyEmissionsFactor
			serviceCache.Add(ServiceDiscoveryKey.JourneyEmissionsFactor, new JourneyEmissionsFactorFactory());

            // Enable CalorieCalculator
            serviceCache.Add(ServiceDiscoveryKey.CalorieCalculator, new CalorieCalculatorFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: DiscountCardCatalogue, CostSearchRunner, CostSearchFacade, JourneyPlanRunnerCaller, ExternalLinkService"));

			// Enable DiscountCard catalogue
			serviceCache.Add(ServiceDiscoveryKey.DiscountCardCatalogue, new DiscountCardCatalogue(SqlHelperDatabase.DefaultDB));

			// Enable Cost Search Runner
			serviceCache.Add(ServiceDiscoveryKey.CostSearchRunner, new CostSearchRunnerFactory());

			// Enable Cost Search Facade
			serviceCache.Add(ServiceDiscoveryKey.CostSearchFacade, new CostSearchFacadeFactory());

			//Enable JourneyPlanRunnerCallerFactory
			serviceCache.Add(ServiceDiscoveryKey.JourneyPlanRunnerCaller, new JourneyPlanRunnerCallerFactory());

			// Enable External Links Service
			serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new ExternalLinksFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: CyclePlannerFactory, CyclePlannerManager, CycleJourneyPlanRunnerCaller, GradientProfilerManager, CycleAttributes, CoordinateConvertorFactory"));

            // Enable CyclePlannerFactory
            serviceCache.Add(ServiceDiscoveryKey.CyclePlannerFactory, new CyclePlannerFactory());

            // Enable CyclePlannerManagerFactory
            serviceCache.Add(ServiceDiscoveryKey.CyclePlannerManager, new CyclePlannerManagerFactory());

            // Enable CycleJourneyPlanRunnerCallerFactory
            serviceCache.Add(ServiceDiscoveryKey.CycleJourneyPlanRunnerCaller, new CycleJourneyPlanRunnerCallerFactory());

            // Enable GradientProfilerManagerFactory
            serviceCache.Add(ServiceDiscoveryKey.GradientProfilerManager, new GradientProfilerManagerFactory());

            // Enable CycleAttributesFactory
            serviceCache.Add(ServiceDiscoveryKey.CycleAttributes, new CycleAttributesFactory());

            // Enable CoordinateConvertorFactory
            serviceCache.Add(ServiceDiscoveryKey.CoordinateConvertorFactory, new CoordinateConvertorFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: ZonalServices, ZonalAccessibility, ParkAndRideCatalogue, CarParkCatalogue, NetworkMapLinksService, OperatorsService"));

			// Enable Zonal Services
			serviceCache.Add(ServiceDiscoveryKey.ZonalServices, new ZonalServiceCatalogueFactory());

            // Enable Zonal Accessibility
            serviceCache.Add(ServiceDiscoveryKey.ZonalAccessibility, new ZonalAccessibilityCatalogueFactory());

			// Enable Park And Ride Service
			serviceCache.Add(ServiceDiscoveryKey.ParkAndRideCatalogue, new ParkAndRideCatalogueFactory());
			
			// Enable Car Park Service
			serviceCache.Add(ServiceDiscoveryKey.CarParkCatalogue, new CarParkCatalogueFactory());
			
			// Enable Network Map Links Service
			serviceCache.Add(ServiceDiscoveryKey.NetworkMapLinksService, new NetworkMapLinksFactory());

			// Enable Operator Links Service
			serviceCache.Add(ServiceDiscoveryKey.OperatorsService, new OperatorCatalogueFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: BayTextFilter, JourneyNoteFilter, LocalityTravelineLookup,SocialBookMarkingService,SuggestionLinkService, CostSearchRunnerCaller, TimeBasedPriceSupplier"));

			// Enable Bay Text Filter
			serviceCache.Add(ServiceDiscoveryKey.BayTextFilter, new BayTextFilter());

            // Enable Journey Note Filter
            serviceCache.Add(ServiceDiscoveryKey.JourneyNoteFilter, new JourneyNoteFilterFactory());

			// Enable locality Traveline Lookup
			serviceCache.Add(ServiceDiscoveryKey.LocalityTravelineLookup, new LocalityTravelineLookup());

            // Enable SocialBookMarkingService
            serviceCache.Add(ServiceDiscoveryKey.SocialBookMarkingService, new SocialBookMarkCatalogueFactory());
			
			// Enable SuggestionLinks Service
			serviceCache.Add(ServiceDiscoveryKey.SuggestionLinkService, new SuggestionBoxLinkCatalogueFactory());
			
			//Add CostSearchRunnerFactory
			serviceCache.Add(ServiceDiscoveryKey.CostSearchRunnerCaller, new CostSearchRunnerCallerFactory());			
			
			//Add TimeBasedPriceSupplier
			serviceCache.Add(ServiceDiscoveryKey.TimeBasedPriceSupplier, new TimeBasedPriceSupplierFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: CoachOperatorLookup, VisitPlanRunnerCaller, CoachFaresInterface, TimeBasedFareSupplier, JourneyFareFilterFactory"));

			//Enable Coach Operator Lookup
			serviceCache.Add(ServiceDiscoveryKey.CoachOperatorLookup, new CoachOperatorLookup());

			//Enable VisitPlanRunnerCallerFactory
			serviceCache.Add(ServiceDiscoveryKey.VisitPlanRunnerCaller, new VisitPlanRunnerCallerFactory());

			// Add Factory for Coach Fare Interface.
			serviceCache.Add(ServiceDiscoveryKey.CoachFaresInterface,new CoachFaresInterfaceFactory());

			//Add Factory for TimeBasedFareSupplier
			serviceCache.Add(ServiceDiscoveryKey.TimeBasedFareSupplier,new TimeBasedFareSupplierFactory());

			//Add Factory for JourneyFareFilter
			serviceCache.Add(ServiceDiscoveryKey.JourneyFareFilterFactory,new JourneyFareFilterFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: RoutePriceSupplierFactory, PricedServiceSupplierFactory, CoachRoutesQuotaFareProvider, ExceptionalFaresLookup, CoachFaresLookup"));

			//Add RoutePriceSupplierFactory
			serviceCache.Add(ServiceDiscoveryKey.RoutePriceSupplierFactory,new RoutePriceSupplierFactory());

			//Add PricedServicesSupplierFactory
			serviceCache.Add(ServiceDiscoveryKey.PricedServiceSupplierFactory, new PricedServicesSupplierFactory());

			//Add CoachRoutesQuotaFareProvider
			serviceCache.Add(ServiceDiscoveryKey.CoachRoutesQuotaFareProvider,new CoachRoutesQuotaFareProvider());

			//Enable Excepional Fares Lookup
			serviceCache.Add(ServiceDiscoveryKey.ExceptionalFaresLookup, new ExceptionalFaresLookup());

			//Enable Coach Fares Lookup
			serviceCache.Add(ServiceDiscoveryKey.CoachFaresLookup, new CoachFaresLookup());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: BusinessLinksTemplateCatalogue, PartnerCatalogue, LocationInformation, LatestNewsFactory"));

			//Add Business Links
			serviceCache.Add(ServiceDiscoveryKey.BusinessLinksTemplateCatalogue, new BusinessLinksTemplateCatalogueFactory());
			
			//Add PartnerCatalogueFactory 
			serviceCache.Add (ServiceDiscoveryKey.PartnerCatalogue, new PartnerCatalogueFactory());

			//Add LocationInformationFactory
			serviceCache.Add(ServiceDiscoveryKey.LocationInformation, new LocationInformationFactory());

            //Add LatestNewsFactory
            serviceCache.Add(ServiceDiscoveryKey.LatestNewsFactory, new LatestNewsFactory());

            serviceCache.Add(ServiceDiscoveryKey.StopEventManager, new StopEventManagerFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: RTTILookupHandler, RTTIManager, DepartureBoardService"));
            // For test version only .. 
            //serviceCache.Add(ServiceDiscoveryKey.StopEventManager, new StopEventMockManager());			
            //----------------------------------
            serviceCache.Add(ServiceDiscoveryKey.RTTILookupHandler, new RTTILookupHandlerFactory());

            serviceCache.Add(ServiceDiscoveryKey.RTTIManager, new RDHandlerFactory());

            serviceCache.Add(ServiceDiscoveryKey.DepartureBoardService, new DepartureBoardServiceFactory());

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

            // Enable InternationalJourneyPlanRunnerCallerFactory
            serviceCache.Add(ServiceDiscoveryKey.InternationalJourneyPlanRunnerCaller, new InternationalJourneyPlanRunnerCallerFactory());

			// Time manipulation - to be able to manipulate the base time to be used
			string dateText = Properties.Current["TransportDirect.Common.ServiceDiscovery.Initialisation.ReferenceTime"];
			if( dateText != null )
			{
				try
				{
					DateTime date = System.DateTime.Parse(dateText);
					TDDateTime.FixNow( new TDDateTime(date) );
				}
				catch (Exception ex)
				{
					// If exception happens, rethrow with more detailed message on why
					// Registered exception number is 501
					throw new TDException("Date time reference time property (TransportDirect.Common.ServiceDiscovery.Initialisation.ReferenceTime) is corrupt. It was "+dateText, ex, false, TDExceptionIdentifier.SDBaseDateRefInvalid);
				}
			}

            // Add DropDownGaz
            serviceCache.Add(ServiceDiscoveryKey.DropDownLocationProvider, new DropDownLocationProviderServiceFactory());

            // Add DuplicateTiplocProvider
            serviceCache.Add(ServiceDiscoveryKey.DuplicateTiplocProvider, new DuplicateTiplocProviderFactory());


            // Enable DiscountCard catalogue
            serviceCache.Add(ServiceDiscoveryKey.RouteRestrictionsCatalogue, new RouteRestrictionsCatalogue(SqlHelperDatabase.DefaultDB));


            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Completed - Populate method"));

		}
	}
	public class ConsoleInitialisation : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			// Enable SessionManager
			serviceCache.Add (ServiceDiscoveryKey.SessionManager, new TDSessionManagerFactory());

			// Time manipulation - to be able to manipulate the base time to be used
			string dateText = Properties.Current["TransportDirect.Common.ServiceDiscovery.Initialisation.ReferenceTime"];
			if( dateText != null )
			{
				try
				{
					DateTime date = System.DateTime.Parse(dateText);
					TDDateTime.FixNow( new TDDateTime(date) );
				}
				catch (Exception ex)
				{
					// If exception happens, rethrow with more detailed message on why
					// Registered exception number is 501
					throw new TDException("Date time reference time property (TransportDirect.Common.ServiceDiscovery.Initialisation.ReferenceTime) is corrupt. It was "+dateText, ex, false, TDExceptionIdentifier.SDBaseDateRefInvalid);
				}
			}

		}
	}
}