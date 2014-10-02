// *********************************************** 
// NAME                 : TestInitialisation.cs
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2006-02-10
// DESCRIPTION			: Implementation of TestInitialisation class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Test/TestInitialisation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:46   mturner
//Initial revision.
//
//   Rev 1.0   Feb 10 2006 18:38:06   RPhilpott
//Initial revision.
//

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.RetailBusinessObjects
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
            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			ArrayList errors = new ArrayList();
            IEventPublisher[]	customPublishers = new IEventPublisher[0];			
            Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
        }
    }
}