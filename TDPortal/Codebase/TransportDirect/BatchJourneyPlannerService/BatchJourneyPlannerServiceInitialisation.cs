// *********************************************** 
// NAME                 : BatchJourneyPlannerServiceInitialisation.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Initialises the required services.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/BatchJourneyPlannerServiceInitialisation.cs-arc  $
//
//   Rev 1.5   Mar 22 2013 10:47:28   DLane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.4   Feb 28 2012 15:52:24   DLane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyEmissions;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;

namespace BatchJourneyPlannerService
{
    public class BatchJourneyPlannerServiceInitialisation : IServiceInitialisation
    {
        /// <summary>
        /// Populates the service cache
        /// </summary>
        /// <param name="serviceCache">the service cache</param>
        public void Populate(Hashtable serviceCache)
        {
            ArrayList errors = new ArrayList();

            // Create cryptographic scheme
            try
            {
                serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory());
            }
            catch (Exception exception)
            {
                throw new TDException(String.Format(Messages.Init_CryptographicServiceFailed, exception.Message), false, TDExceptionIdentifier.BJPServiceInitFailed);
            }

            // Create Property Service.
            try
            {
                serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
            }
            catch (Exception exception)
            {
                throw new TDException(String.Format(Messages.Init_PropertyServiceFailed, exception.Message), false, TDExceptionIdentifier.BJPServiceInitFailed);
            }

            // Create Page Controller Service.
            //try
            //{
            //    serviceCache.Add(ServiceDiscoveryKey.PageController, new PageControllerFactory());
            //}
            //catch (Exception exception)
            //{
            //    throw new TDException(String.Format(Messages.Init_PropertyServiceFailed, exception.Message), false, TDExceptionIdentifier.BJPServiceInitFailed);
            //}

            // initialise logging service	
            IEventPublisher[] customPublishers = new IEventPublisher[1];
            customPublishers[0] =
                new CustomEmailPublisher("EMAIL",
                Properties.Current["Logging.Publisher.Custom.EMAIL.Sender"],
                System.Net.Mail.MailPriority.Normal,
                Properties.Current["Logging.Publisher.Custom.EMAIL.SMTPServer"],
                Properties.Current["Logging.Publisher.Custom.EMAIL.WorkingDir"],
                errors);
            Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));

            // Enable DataServices
            serviceCache.Add(ServiceDiscoveryKey.DataServices, new DataServicesFactory(null));

            // Enable DataChangeNotification
            serviceCache.Add(ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());

            // Enable Location Search
            serviceCache.Add(ServiceDiscoveryKey.GisQuery, new GisQueryFactory());
            serviceCache.Add(ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());

            // Enable Cache object
            serviceCache.Add(ServiceDiscoveryKey.Cache, new TDCache());

            //// Add PartnerCatalogueFactory 
            //serviceCache.Add(ServiceDiscoveryKey.PartnerCatalogue, new PartnerCatalogueFactory());

            // Enable CJP 
            serviceCache.Add(ServiceDiscoveryKey.Cjp, new CjpFactory());

            // Enable CarCostCalculator
            serviceCache.Add(ServiceDiscoveryKey.CarCostCalculator, new CarCostCalculatorFactory());

            // Enable JourneyEmissionsFactor
            serviceCache.Add(ServiceDiscoveryKey.JourneyEmissionsFactor, new JourneyEmissionsFactorFactory());

            // Validate Properties which are required by services.
            BatchJourneyPlannerServicePropertyValidator validator = new BatchJourneyPlannerServicePropertyValidator(Properties.Current);
            validator.ValidateProperties(errors);
            if (errors.Count != 0)
            {
                StringBuilder message = new StringBuilder(100);
                foreach (string error in errors)
                    message.Append(error + ",");

                throw new TDException(String.Format(Messages.Init_InvalidPropertyKeys, message.ToString()), true, TDExceptionIdentifier.BJPServiceInitFailed);
            }
        }
    }
}
