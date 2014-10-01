using System;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Summary description for TestInitialization.
	/// </summary>
	public class TestInitialization : IServiceInitialisation
	{
		public TestInitialization()
		{}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCache"></param>
        public void Populate(Hashtable serviceCache)
        {
            // Enable PropertyService
            serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            try
            {
                ArrayList errors = new ArrayList();
                IEventPublisher[] customPublishers = new IEventPublisher[0];
                Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
            }
            catch (TDException tdEx)
            {
                OperationalEvent oe =
                    new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Error, (tdEx.Identifier) + tdEx.Message );
                Logger.Write( oe );
                throw tdEx;
            }
        }
	}
}
