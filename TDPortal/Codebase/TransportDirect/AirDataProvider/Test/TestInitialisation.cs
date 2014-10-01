// *********************************************** 
// NAME                 : TestInitialisation.cs
// AUTHOR               : Atos Origin
// DATE CREATED         : 15/10/2003
// DESCRIPTION			: Implementation of TestInitialisation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:46   mturner
//Initial revision.
//
//   Rev 1.0   Jun 09 2004 17:44:16   CHosegood
//Initial revision.

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.AirDataProvider
{
    /// <summary>
    /// Initialisation class to be included in test harnesses
    /// </summary>
    public class TestInitialisation : IServiceInitialisation
    {
        public TestInitialisation()
        {
        }

        public void Populate(Hashtable serviceCache)
        {
            // Enable PropertyService
            // Enable PropertyService
            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            // initialise logging service
			ArrayList errors = new ArrayList();
            IEventPublisher[]	customPublishers = new IEventPublisher[0];			
            Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
        }
    }
}