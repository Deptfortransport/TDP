// *********************************************** 
// NAME			: TestInitialisation.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 02/02/2010
// DESCRIPTION	: Test Initialisation class initialises various services
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/Test/TestInitialisation.cs-arc  $
//
//   Rev 1.1   Feb 16 2010 17:48:38   mmodi
//Updated tests
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 02 2010 10:03:20   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.InternationalPlanner;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
   /// <summary>
	/// Initialisation class to be included in the InternationalPlannerControl test harnesses
	/// </summary>
	public class TestInitialisation : IServiceInitialisation
	{
        public TestInitialisation()
		{
		}

        /// <summary>
        /// Populates sevice cache with services needed 
        /// </summary>
		public void Populate(Hashtable serviceCache)
		{
            // Enable PropertyService
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            // Enable logging service.
            ArrayList errors = new ArrayList();
            try
            {
                IEventPublisher[] customPublishers = new IEventPublisher[0];

                Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
            }
            catch (TDException)
            {
                foreach (string error in errors)
                {
                    Console.WriteLine(error);
                }
                throw;
            }

            // Enable Cache object
            serviceCache.Add(ServiceDiscoveryKey.Cache, new TDCache());

            // Enable Location Search
            serviceCache.Add(ServiceDiscoveryKey.GisQuery, new GisQueryFactory());

            // Enable InternationalPlannerFactory
            serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerFactory, new InternationalPlannerFactory());

            // Enable InternationalPlannerDataFactory
            serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerDataFactory, new InternationalPlannerDataFactory());

            // Enable InternationalPlannerManagerFactory
            serviceCache.Add(ServiceDiscoveryKey.InternationalPlannerManager, new InternationalPlannerManagerFactory());
		}
	}
}
