// *********************************************** 
// NAME             : Initialisation.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 04 Mar 2011
// DESCRIPTION  	: Service Discovery cache initialisation 
// ************************************************
                
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ServiceDiscovery;
using TDP.Common.PropertyManager;
using System.Diagnostics;
using TDP.Common.EventLogging;

namespace TDP.Common.LocationJsGenerator
{
    /// <summary>
    /// Initialisation class for service discovery
    /// Initialises services required fro LocationJsGenerator
    /// </summary>
    class Initialisation : IServiceInitialisation
    {
        #region Public Methods
        /// <summary>
        /// Method to initialise Service Discovery cache with relevant services
        /// </summary>
        /// <param name="serviceCache">Service cache to initialise</param>
        public void Populate(Dictionary<string, IServiceFactory> serviceCache)
        {
            try
            {
                serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
            }
            catch (Exception ex)
            {
                string message = string.Format("Initialisation failed : {0}", ex.StackTrace);

                Trace.Write(
                  new OperationalEvent(
                      TDPEventCategory.Business,
                      TDPTraceLevel.Error,
                      message));

                throw new TDPException(message, true, TDPExceptionIdentifier.LJSGenInitialisationFailed);
            }
        }

        #endregion
    }
}
