// *********************************************** 
// NAME             : TDPWebInitialisation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: TDPWebInitialisation class for initialising services needed for the TDPWeb project
// ************************************************
// 

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.DataServices;
using TDP.Common.DataServices.CycleAttributes;
using TDP.Common.DataServices.NPTG;
using TDP.Common.DataServices.StopAccessibilityLinks;
using TDP.Common.EventLogging;
using TDP.Common.LocationService;
using TDP.Common.LocationService.GIS;
using TDP.Common.PropertyManager;
using TDP.UserPortal.CoordinateConvertorProvider;
using TDP.UserPortal.CyclePlannerService;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.JourneyPlanRunner;
using TDP.UserPortal.Retail;
using TDP.UserPortal.ScreenFlow;
using TDP.UserPortal.SessionManager;
using TDP.UserPortal.TravelNews;
using TDP.UserPortal.UndergroundNews;
using Logger = System.Diagnostics.Trace;

namespace TDP.Common.ServiceDiscovery.Initialisation
{
    /// <summary>
    /// Web Initialisation class
    /// </summary>
    public class TDPWebInitialisation : IServiceInitialisation
    {        
        #region Interface members

        /// <summary>
        /// IServiceInitialisation Populate method
        /// </summary>
        /// <param name="serviceCache"></param>
        public void Populate(Dictionary<string, IServiceFactory> serviceCache)
        {
            // Enable PropertyService
            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            #region Enable EventLogging
            // Enable Event Logging
            List<string> errors = new List<string>();
            try
            {
                // Create custom email publisher
                IEventPublisher[] customPublishers = new IEventPublisher[0];

                // Create and add TDPTraceListener instance to the listener collection	
                Trace.Listeners.Add(new TDPTraceListener(Properties.Current, customPublishers, errors));
            }
            catch (TDPException tdpEx)
            {
                #region Log and throw errors
                // Create message string
                StringBuilder message = new StringBuilder(100);
                message.Append(tdpEx.Message); // prepend with existing exception message

                // Append all messages returned by TDTraceListener constructor
                foreach (string error in errors)
                {
                    message.Append(error);
                    message.Append(" ");
                }

                // Log message using .NET default trace listener
                Trace.WriteLine(message.ToString() + "ExceptionID:" + tdpEx.Identifier.ToString());

                // rethrow exception - use the initial exception id as the id
                throw new TDPException(message.ToString(), tdpEx, false, tdpEx.Identifier);

                #endregion
            }
            #endregion

            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Info, "INITIALISATION Started TDPWebInitialisation"));

            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Added to service cache - Properties and Event Logging"));
                        
            // Add other services dependent on Properties and EventLogging

            // Session
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: SessionManager"));
            serviceCache.Add(ServiceDiscoveryKey.SessionManager, new TDPSessionFactory());

            // Enable Cache object
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: Cache"));
            serviceCache.Add(ServiceDiscoveryKey.Cache, new TDPCache());

            // DataChangeNotification
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: DataChangeNotification"));
            serviceCache.Add(ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());

            // LocationService
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: LocationService"));
            serviceCache.Add(ServiceDiscoveryKey.LocationService, new LocationServiceFactory());

            // GisQuery
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: GisQuery"));
            serviceCache.Add(ServiceDiscoveryKey.GisQuery, new GisQueryFactory());

            // PageController
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: PageController"));
            serviceCache.Add(ServiceDiscoveryKey.PageController, new PageControllerFactory());

            // CJP
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: CJP"));
            serviceCache.Add(ServiceDiscoveryKey.CJP, new CJPFactory());

            // CJPManager
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: CJPManger"));
            serviceCache.Add(ServiceDiscoveryKey.CJPManager, new CjpManagerFactory());

            // CTP
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: CTP"));
            serviceCache.Add(ServiceDiscoveryKey.CTP, new CyclePlannerFactory ());
                        
            // CyclePlannerManager
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: CyclePlannerManager"));
            serviceCache.Add(ServiceDiscoveryKey.CyclePlannerManager, new CyclePlannerManagerFactory());

            // StopEventManager
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: StopEventManager"));
            serviceCache.Add(ServiceDiscoveryKey.StopEventManager, new StopEventManagerFactory());

            // CoordinateConvertor
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: CoordinateConvertor"));
            serviceCache.Add(ServiceDiscoveryKey.CoordinateConvertor, new CoordinateConvertorFactory());

            // JourneyPlanRunnerCaller
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: JourneyPlanRunnerCaller"));
            serviceCache.Add(ServiceDiscoveryKey.JourneyPlanRunnerCaller, new JourneyPlanRunnerCallerFactory());

            // CycleJourneyPlanRunnerCaller
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: CycleJourneyPlanRunnerCaller"));
            serviceCache.Add(ServiceDiscoveryKey.CycleJourneyPlanRunnerCaller, new CycleJourneyPlanRunnerCallerFactory());

            // StopEventRunnerCaller
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: StopEventRunnerCaller"));
            serviceCache.Add(ServiceDiscoveryKey.StopEventRunnerCaller, new StopEventRunnerCallerFactory());

            // RetailerCatalogue
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: RetailerCatalogue"));
            serviceCache.Add(ServiceDiscoveryKey.RetailerCatalogue, new RetailerCatalogueFactory());

            // RetailHandoffSchema
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: RetailHandoffSchema"));
            serviceCache.Add(ServiceDiscoveryKey.RetailerHandoffSchema, new RetailHandoffSchemaFactory());

            // TravelcardCatalogue
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: TravelcardCatalogue"));
            serviceCache.Add(ServiceDiscoveryKey.TravelcardCatalogue, new TravelcardCatalogueFactory());

            // DataServices
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: DataServices"));
            serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory());

            // CycleAttributes
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: CycleAttributes"));
            serviceCache.Add(ServiceDiscoveryKey.CycleAttributes, new CycleAttributesFactory());

            // StopAccessibilityLinks
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: StopAccessibilityLinks"));
            serviceCache.Add(ServiceDiscoveryKey.StopAccessibilityLinks, new StopAccessibilityLinksFactory());

            // TravelNews
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: TravelNewsHandler"));
            serviceCache.Add(ServiceDiscoveryKey.TravelNews, new TravelNewsHandlerFactory());

            // UndergroundNews
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: UndergroundNewsHandler"));
            serviceCache.Add(ServiceDiscoveryKey.UndergroundNews, new UndergroundNewsHandlerFactory());

            // NPTGData
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: NPTGData"));
            serviceCache.Add(ServiceDiscoveryKey.NPTGData, new NPTGDataFactory());

            // Enable Journey Note Filter
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: JourneyNoteFilter"));
            serviceCache.Add(ServiceDiscoveryKey.JourneyNoteFilter, new JourneyNoteFilterFactory());

            // Enable Operator Links Service
            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "INITIALISATION Adding to service cache: Operator Catalogue"));
            serviceCache.Add(ServiceDiscoveryKey.OperatorService, new OperatorCatalogueFactory());

            Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Info, "INITIALISATION Completed TDPWebInitialisation"));
        }

        #endregion
    }
}
