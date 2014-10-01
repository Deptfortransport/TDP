// *********************************************** 
// NAME             : TestInitialisation.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: Initialisation for Test projects
// ************************************************
// 

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TDP.Common;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.EventLogging;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Reporting.EventPublishers;
using TDP.UserPortal.CoordinateConvertorProvider;
using TDP.UserPortal.CyclePlannerService;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.Retail;
using TDP.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;

namespace TDP.TestProject
{
    class TestInitialisation : IServiceInitialisation
    {
        public void Populate(Dictionary<string, IServiceFactory> serviceCache)
        {
            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            #region Enable EventLogging
            // Enable Event Logging
            List<string> errors = new List<string>();
            try
            {
                // No custom publishers
                IEventPublisher[] customPublishers = new IEventPublisher[2];

                // Create custom database publishers which will be used to publish 
                // custom events Note: id passed in constructors
                // must match those defined in the properties.
                customPublishers[0] = new TDPCustomEventPublisher("TDPDB", SqlHelperDatabase.ReportStagingDB);
                customPublishers[1] = new TDPOperationalEventPublisher("OPDB", SqlHelperDatabase.ReportStagingDB);

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

            serviceCache.Add(ServiceDiscoveryKey.DataChangeNotification, new DataChangeNotificationFactory());

            serviceCache.Add(ServiceDiscoveryKey.LocationService, new LocationServiceFactory());

            serviceCache.Add(ServiceDiscoveryKey.CJP, new CJPFactory());

            serviceCache.Add(ServiceDiscoveryKey.CJPManager, new CjpManagerFactory());

            serviceCache.Add(ServiceDiscoveryKey.CTP, new CyclePlannerFactory());

            serviceCache.Add(ServiceDiscoveryKey.CyclePlannerManager, new CyclePlannerManagerFactory());

            serviceCache.Add(ServiceDiscoveryKey.StopEventManager, new StopEventManagerFactory());

            serviceCache.Add(ServiceDiscoveryKey.CoordinateConvertor, new CoordinateConvertorFactory());

            serviceCache.Add(ServiceDiscoveryKey.RetailerHandoffSchema, new RetailHandoffSchemaFactory());

            serviceCache.Add(ServiceDiscoveryKey.SessionManager, new TDPSessionFactory());

            try
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Verbose, "TestInitialisation completed"));
            }
            catch
            {
                // Ignore
            }
        }
    }
}
