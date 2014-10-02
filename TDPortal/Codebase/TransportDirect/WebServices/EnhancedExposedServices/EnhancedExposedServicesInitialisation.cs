// *********************************************** 
// NAME                 : EnhancedExposedServicesInitialisation.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 23/11/2005 
// DESCRIPTION  		: Service Initilisation class for EnhancedExposedServices.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/EnhancedExposedServicesInitialisation.cs-arc  $ 
//
//   Rev 1.5   Mar 28 2013 11:13:24   RBroddle
//Add OperatorCatalogueFactory initialisation so EES can start OK and use translated Service Detail Request Operator Codes Data
//Resolution for 5906: MDV Issue - Stop Events Not Being Displayed in MDV Regions Except London
//
//   Rev 1.4   Sep 29 2010 11:27:56   apatel
//EES Web Services for Cycle code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.3   Jul 01 2010 12:47:28   apatel
//Updated for duplicate tiploc provider
//
//   Rev 1.2   Mar 19 2010 10:52:04   apatel
//Updated initialisation to initialise CJPManager to Make ESS work when AP servers are disabled.
//Resolution for 5470: JourneyPlannerSynchronousService will not run in Direct Mode
//
//   Rev 1.1   Aug 04 2009 14:28:34   mmodi
//Updated for Car journey planner exposed service
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 13:51:44   mturner
//Initial revision.
//
//   Rev 1.8   Feb 24 2006 15:54:12   RWilby
//Fix for stream3129 merge.  Added TDCache to ServiceDiscovery. 
//
//   Rev 1.7   Feb 20 2006 15:36:48   mdambrine
//Changes for access restriction
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.6   Jan 25 2006 14:32:48   mdambrine
//added initialisation of the journeyplanner component
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Jan 19 2006 10:46:58   RWilby
//Added AirDataProvider, GisQuery and GazetteerFactory services to Service Initilisation
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.4   Jan 04 2006 12:37:46   mtillett
//Move Helper classes into the TransportDirect.EnhancedExposedServices.Helpers namespace
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Dec 22 2005 11:29:52   asinclair
//Check in of Sanjeev's Work In Progress code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.2   Nov 30 2005 16:00:14   schand
//Code Review changes
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.1   Nov 29 2005 13:33:36   schand
//Added regions to the code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Nov 25 2005 18:39:06   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

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
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.Partners;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.DepartureBoardService;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager;
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyEmissions;
using TransportDirect.UserPortal.JourneyPlannerService;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.TravelNews;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.EnhancedExposedServices
{
	/// <summary>
	///  Service Initilisation class for EnhancedExposedServices.
	/// </summary>
	public class EnhancedExposedServicesInitialisation   : IServiceInitialisation 
	{
		#region Class Members
		// This filename is not configurable - it is documented in deployment guide
		private const string DefaultLogFilename = "td.enhancedexposedservices.log";
		#endregion


		#region Constructor
		public EnhancedExposedServicesInitialisation()
		{
		}

		#endregion
		
		
		#region Interface Methods
		/// <summary>
		/// Populate implementation of IServiceInitialisation interface
		/// </summary>
		/// <param name="serviceCache">Hasttable</param>
		public void Populate (Hashtable serviceCache)
		{	
			TextWriterTraceListener logTextListener = null;
			ArrayList errors = new ArrayList();

			try
			{
				// initialise .NET file trace listener for use prior to TDTraceListener
				string logfilePath = ConfigurationManager.AppSettings[EnhancedExposedServicesKey.DefaultLogPath];
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
                    "INITIALISATION Adding to service cache: DataChangeNotification, Cache, PartnerCatalogue"));

				serviceCache.Add( ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory() );				

				// Enable Cache object
				serviceCache.Add( ServiceDiscoveryKey.Cache, new TDCache() );

				// Add PartnerCatalogueFactory 
				serviceCache.Add (ServiceDiscoveryKey.PartnerCatalogue, new PartnerCatalogueFactory());

                // Enable CJPManager
                serviceCache.Add(ServiceDiscoveryKey.CjpManager, new CjpManagerFactory());

                // Enable Operator Links Service
                serviceCache.Add(ServiceDiscoveryKey.OperatorsService, new OperatorCatalogueFactory());

                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                    "INITIALISATION Adding to service cache: JourneyPlannerService, JourneyPlannerSynchronousService"));

				//Add JourneyPlannerServiceFactory
				serviceCache.Add(ServiceDiscoveryKey.JourneyPlannerService, new JourneyPlannerFactory());
				
				//Add JourneyPlannerSynchronousFactory
				serviceCache.Add(ServiceDiscoveryKey.JourneyPlannerSynchronousService, new JourneyPlannerSynchronousFactory());

                
			}
			catch (TDException tdException)
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
				string logMessage = "{0} ExceptionID: {1}";
				
				Trace.WriteLine(string.Format(logMessage,  message.ToString(), tdException.Identifier.ToString("D")));		
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

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: DataServices, AdditionalData"));

			// Attention! here the DataServices component is loaded passing a null ResourceManager.
			// This is because it is used specifically within the WebService which doesn't use resources.
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));

			// Enable AdditionalData
			serviceCache.Add( ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory() );

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: TravelNews, CodeGazetteer, CJP, StopEventManager"));

			// initialise TravelNews Handler
			serviceCache.Add(ServiceDiscoveryKey.TravelNews, new TravelNewsHandlerFactory());
			serviceCache.Add(ServiceDiscoveryKey.CodeGazetteer, new CodeGazetteerFactory());

			serviceCache.Add(ServiceDiscoveryKey.Cjp, new CJPFactory());
			serviceCache.Add(ServiceDiscoveryKey.StopEventManager, new StopEventManagerFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: RTTILookupHandler, RTTIManager, DepartureBoardService"));
			// For test version only .. 
			//serviceCache.Add(ServiceDiscoveryKey.StopEventManager, new StopEventMockManager());			
			//----------------------------------
			serviceCache.Add(ServiceDiscoveryKey.RTTILookupHandler, new RTTILookupHandlerFactory());
			serviceCache.Add(ServiceDiscoveryKey.RTTIManager, new RDHandlerFactory());
			serviceCache.Add(ServiceDiscoveryKey.DepartureBoardService, new DepartureBoardServiceFactory());

            // Add DuplicateTiplocProvider
            serviceCache.Add(ServiceDiscoveryKey.DuplicateTiplocProvider, new DuplicateTiplocProviderFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: AirDataProvider, GisQuery, GazetteerFactory"));

			// Enable Air Data Provider
			serviceCache.Add(ServiceDiscoveryKey.AirDataProvider, new AirDataProviderFactory());

			// Enable Location Search
			serviceCache.Add (ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
			serviceCache.Add (ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());

            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Adding to service cache: CarCostCalculator, JourneyEmissionsFactor"));

            // Enable CarCostCalculator
            serviceCache.Add(ServiceDiscoveryKey.CarCostCalculator, new CarCostCalculatorFactory());

            // Enable JourneyEmissionsFactor
            serviceCache.Add(ServiceDiscoveryKey.JourneyEmissionsFactor, new JourneyEmissionsFactorFactory());

            // Enable CycleAttributesFactory
            serviceCache.Add(ServiceDiscoveryKey.CycleAttributes, new CycleAttributesFactory());
            
            // Enable CyclePlannerFactory
            serviceCache.Add(ServiceDiscoveryKey.CyclePlannerFactory, new CyclePlannerFactory());

            // Enable CyclePlannerManagerFactory
            serviceCache.Add(ServiceDiscoveryKey.CyclePlannerManager, new CyclePlannerManagerFactory());
            
            // Enable GradientProfileManagerFactory
            serviceCache.Add(ServiceDiscoveryKey.GradientProfilerManager, new GradientProfilerManagerFactory());

            // Enable CoordinateConvertorFactory
            serviceCache.Add(ServiceDiscoveryKey.CoordinateConvertorFactory, new CoordinateConvertorFactory());


            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info,
                "INITIALISATION Completed - Populate method"));
		}
		#endregion
	}
}
