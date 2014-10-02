// *********************************************** 
// NAME			: TransactionWebServiceInitialisation.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Implementation of the TransactionWebServiceInitialisation class.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/TransactionWebServices/TransactionWebServiceInitialisation.cs-arc  $
//
//   Rev 1.4   Sep 10 2012 14:25:24   rbroddle
//Added initialization of CalorieCalculator key
//Resolution for 5844: TransactionWebService throwing error due to CalorieCalculator
//
//   Rev 1.3   Jul 01 2010 12:47:28   apatel
//Updated for duplicate tiploc provider
//
//   Rev 1.2   Mar 16 2009 12:24:08   build
//Automatically merged from branch for stream5215
//
//   Rev 1.1.1.1   Jan 23 2009 10:51:46   mturner
//Further tech refresh updates
//
//   Rev 1.1.1.0   Jan 16 2009 17:00:34   mturner
//Initial revision
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.1   Oct 14 2008 11:20:14   build
//merge for stream5014
//
//   Rev 1.0.1.0   Sep 10 2008 14:47:46   mturner
//Updated for cycle planner
//
//   Rev 1.0   Nov 08 2007 13:55:30   mturner
//Initial revision.
//
//   Rev 1.24   Jan 16 2006 16:05:16   rgreenwood
//IR 3422: Added service initialisation for PlaceDataProvider
//Resolution for 3422: Del 8 TI issue
//
//   Rev 1.23   Nov 09 2005 12:31:28   build
//Automatically merged from branch for stream2818
//
//   Rev 1.22.1.1   Oct 29 2005 10:57:54   RPhilpott
//TimeBasedFareSupplier instead of TimeBasedPriceSupplierCaller. 
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.22.1.0   Oct 28 2005 18:30:54   RPhilpott
//Include TimeBasedPriceSupplier.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.22   Sep 21 2005 17:31:40   CRees
//ir 2777 trackerlink
//
//   Rev 1.21   Sep 21 2005 16:20:02   CRees
//IR2777 - Vantive 3947660 - Fix for Transaction Injector accessing service discovery keys for ExternalLinksFactory and LocalityTravelineLookup
//
//   Rev 1.20   Apr 09 2005 15:05:00   schand
//Added DepartureBoardService initialisation objects
//
//   Rev 1.19   04 Oct 2004 12:05:54   passuied
//Added AirDataProvider service in ServiceDiscovery
//Resolution for 1677: TransactionWebService Exception: No AirDataProvider added at startup to the ServiceDiscovery
//
//   Rev 1.18   Jan 21 2004 10:44:34   geaton
//Ensure that error messages are flushed to default .NET log file when exceptions are thrown.
//
//   Rev 1.17   Jan 09 2004 12:41:00   PNorell
//Added service for gazetteer.
//
//   Rev 1.16   Nov 13 2003 18:19:24   geaton
//Improved exception message.
//
//   Rev 1.15   Nov 13 2003 17:22:56   geaton
//Improved error message.
//
//   Rev 1.14   Nov 13 2003 17:16:40   geaton
//Improved exception message.
//
//   Rev 1.13   Nov 13 2003 16:52:18   geaton
//Added message to record start of initialisation.
//
//   Rev 1.12   Nov 12 2003 17:34:00   geaton
//Log error if property service fails to start.
//
//   Rev 1.11   Nov 12 2003 16:11:32   geaton
//Added initialisation to support pricing and station info.
//
//   Rev 1.10   Nov 11 2003 15:03:40   geaton
//Added call to add TDMapHandoff to service cache - needed when making CJP calls.
//
//   Rev 1.9   Nov 10 2003 12:09:42   geaton
//Added crypto initialisation.
//
//   Rev 1.8   Nov 06 2003 17:08:20   geaton
//Catch exceptions thrown by remoting configuration.
//
//   Rev 1.7   Nov 05 2003 10:20:34   geaton
//Updated comment.
//
//   Rev 1.6   Nov 05 2003 09:57:58   geaton
//Added initialisation of services that were being performed in global.asax

using System;
using System.Collections;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;
using System.Runtime.Remoting;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.RetailBusinessObjects;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.DepartureBoardService;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager; 
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
//IR 2777 - Vantive 3947660 - Added to fix TI injections for ExternalLinkService - Chris Rees
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.TravelNews;

using TDPProperties = TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.TransactionWebService
{
	/// <summary>
	/// Used by the TDServiceDiscovery class to initialise TD services.
	/// </summary>
	public class TransactionWebServiceInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// Sets up TD Services.
		/// </summary>
		/// <param name="serviceCache">Service cache to add services to.</param>
		/// <exception cref="TDException">
		/// One or more services fail to initialise.
		/// </exception>
		public void Populate( Hashtable serviceCache )
		{			
			// Initialise .NET trace listener to record ONLY errors until TD TraceListener is intialised. 
			TextWriterTraceListener logTextListener = null;
			try
			{	
				string logfilePath = ConfigurationManager.AppSettings["DefaultLogFilepath"];
				Stream logFile = File.Create(logfilePath + "\\" + ConfigurationManager.AppSettings["propertyservice.applicationid"] + ".txt");
				logTextListener = new TextWriterTraceListener(logFile);
				Trace.Listeners.Add(logTextListener);
				Trace.WriteLine(Messages.Init_InitialisationStarted);
			}
			catch (Exception exception) // catch all in this situation.
			{
				throw new TDException(String.Format(Messages.Init_DotNETTraceListenerFailed, exception.Message), false, TDExceptionIdentifier.RDPTransactionServiceDefaultLoggerFailed);
			}

			// Add TD services to service cache that are needed to support TD Trace Listening.
			try
			{	
				serviceCache.Add(ServiceDiscoveryKey.Crypto,  new CryptoFactory());
				serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			}
			catch(Exception exception)
			{
				Trace.WriteLine(String.Format(Messages.Init_TDServiceAddFailed, exception.Message));
				logTextListener.Flush();
				logTextListener.Close();
				throw new TDException(String.Format(Messages.Init_TDServiceAddFailed, exception.Message), true, TDExceptionIdentifier.RDPTransactionServiceTDServiceAddFailed);
			}

			// Create TD Logging service.
			ArrayList errors = new ArrayList();
			IEventPublisher[] customPublishers = new IEventPublisher[0];
			try
			{
                Trace.Listeners.Add(new TDTraceListener(TDPProperties.Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{	
				// Create error message to log to default listener.
				Trace.WriteLine(String.Format(Messages.Init_TDTraceListenerFailed, "Reasons follow."));
				StringBuilder message = new StringBuilder(100);
				foreach (string error in errors)
					message.Append(error + ",");
				Trace.WriteLine(message.ToString());				
				
				throw new TDException(String.Format(Messages.Init_TDTraceListenerFailed, message.ToString()), true, TDExceptionIdentifier.RDPTransactionServiceTDTraceInitFailed);	
			}
			finally
			{
				// Remove other listeners.
				logTextListener.Flush();
				logTextListener.Close();
				Trace.Listeners.Remove( logTextListener );
				logTextListener = null;
				Trace.Listeners.Remove("System.Diagnostics.DefaultTraceListener");
			}

			// Add TD services to service cache that are needed to support the web methods.
			try
			{
				// Following services needed to support web service requests.
				serviceCache.Add(ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
				serviceCache.Add (ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());
				serviceCache.Add (ServiceDiscoveryKey.AirDataProvider, new AirDataProviderFactory());
				serviceCache.Add(ServiceDiscoveryKey.Cjp, new CjpFactory());
				serviceCache.Add(ServiceDiscoveryKey.CjpManager, new CjpManagerFactory());
				serviceCache.Add(ServiceDiscoveryKey.Cache, new TDCache());
				serviceCache.Add(ServiceDiscoveryKey.TDMapHandoff, new TDMapHandoffFactory());
				serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));
				serviceCache.Add (ServiceDiscoveryKey.RetailerCatalogue, new RetailerCatalogueFactory());
				serviceCache.Add( ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory() );

				serviceCache.Add(ServiceDiscoveryKey.TimeBasedFareSupplier,new TimeBasedFareSupplierFactory());

				// adding RTTILookupHandlerFactory
				serviceCache.Add (ServiceDiscoveryKey.RTTILookupHandler, new RTTILookupHandlerFactory());   
				// adding RDHandlerFactory
				serviceCache.Add (ServiceDiscoveryKey.RTTIManager, new RDHandlerFactory());

                // adding DuplicateTiplocProvider
                serviceCache.Add(ServiceDiscoveryKey.DuplicateTiplocProvider, new DuplicateTiplocProviderFactory());

				// adding StopEventManager
				serviceCache.Add(ServiceDiscoveryKey.StopEventManager, new StopEventManager());
				//IR 2777 - Vantive 3947660 - Added to fix TI injections for ExternalLinkService - Chris Rees
				// Enable External Links Service
				serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new ExternalLinksFactory());
				// Enable locality Traveline Lookup
				serviceCache.Add(ServiceDiscoveryKey.LocalityTravelineLookup, new LocalityTravelineLookup());
				// Enable Bay Text Filter
				serviceCache.Add(ServiceDiscoveryKey.BayTextFilter, new BayTextFilter());
				// end IR 2777

				// Enable Place Data Provider - IR3424
				serviceCache.Add(ServiceDiscoveryKey.PlaceDataProvider, new PlaceDataProviderFactory());

                // Enable CyclePlannerFactory
                serviceCache.Add(ServiceDiscoveryKey.CyclePlannerFactory, new CyclePlannerFactory());

                // Enable CyclePlannerManagerFactory
                serviceCache.Add(ServiceDiscoveryKey.CyclePlannerManager, new CyclePlannerManagerFactory());

                // Enable TravelNewsHandlerFactory
                serviceCache.Add(ServiceDiscoveryKey.TravelNews, new TravelNewsHandlerFactory());

                // Enable CalorieCalculator
                serviceCache.Add(ServiceDiscoveryKey.CalorieCalculator, new CalorieCalculatorFactory());

			}
			catch(Exception exception)
			{
				throw new TDException(String.Format(Messages.Init_TDServiceAddFailed, exception.Message), false, TDExceptionIdentifier.RDPTransactionServiceTDServiceAddFailed);
			}

			// Validate Transaction Web Service Properties.
			ArrayList propertyErrors = new ArrayList();
            TransactionWebServicePropertyValidator validator = new TransactionWebServicePropertyValidator(TDPProperties.Properties.Current);	
			validator.ValidateProperty(JourneyControlConstants.NumberOfPublicJourneys, propertyErrors);
			validator.ValidateProperty(JourneyControlConstants.LogAllRequests, propertyErrors);
			validator.ValidateProperty(JourneyControlConstants.LogAllResponses, propertyErrors);
			validator.ValidateProperty(JourneyControlConstants.CJPTimeoutMillisecs, propertyErrors);
			validator.ValidateProperty(Keys.LocationServerName, propertyErrors);
			validator.ValidateProperty(Keys.LocationServiceName, propertyErrors);
			validator.ValidateProperty(Keys.CJPConfig, propertyErrors);
			validator.ValidateProperty(Keys.DefaultDB, propertyErrors);
			validator.ValidateProperty(Keys.EsriDB, propertyErrors);
			if (propertyErrors.Count != 0)
			{
				StringBuilder message = new StringBuilder(100);
				foreach (string error in propertyErrors)
					message.Append(error + ",");
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Init_InvalidPropertyKeys, message.ToString()))); 
		
				throw new TDException(String.Format(Messages.Init_InvalidPropertyKeys, message.ToString()), true, TDExceptionIdentifier.RDPTransactionServiceInvalidProperties);
			}

			// Initialise CJP Remoting configuration.
			try
			{
                RemotingConfiguration.Configure(TDPProperties.Properties.Current[Keys.CJPConfig], false);
			}
			catch (Exception exception) // Catch all since no documentation.
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Init_CJPConfigFailed, exception.Message))); 
				throw new TDException(String.Format(Messages.Init_CJPConfigFailed, exception.Message), true, TDExceptionIdentifier.RDPTransactionServiceCJPConfigFailed);
			}
			
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.Init_Completed));
		}
	}
}
