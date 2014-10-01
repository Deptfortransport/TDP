// *********************************************** 
// NAME             : TDPServiceDiscovery.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 14 Feb 2011
// DESCRIPTION  	: The Service discovery will:
//						- Ensure services are initialised correctly depending on environment
//						- Enable switching implementation to test services for selected services
// ************************************************
                
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.ServiceDiscovery
{
    /// <summary>
    /// The Service discovery will:
    ///  - Ensure services are initialised correctly depending on environment
    ///  - Enable switching implementation to test services for selected services
    /// </summary>
    public class TDPServiceDiscovery
    {
        #region Private Static Fields
        private static TDPServiceDiscovery current;
        #endregion

        #region Private Fields
        private Dictionary<string, IServiceFactory> serviceCache = new Dictionary<string, IServiceFactory>();
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public TDPServiceDiscovery()
        {

        }
        #endregion


        /// <summary>
        /// static read-only property that returns the current Service Discovery
        /// </summary>
        public static TDPServiceDiscovery Current
        {
            get
            {
                return current;
            }
        }

        
        /// <summary>
        /// Method that returns the object requested against a key
        /// </summary>
        public T Get<T>(string key)
        {
            if (!current.serviceCache.ContainsKey(key))
            {
                throw new TDPException(
                    string.Format("Exception trying to access the ServiceDiscovery at a given key. Key [{0}] does not exist", key),
                    false, TDPExceptionIdentifier.SDInvalidKey);
            }
            return (T)(current.serviceCache[key]).Get();
        }

        /// <summary>
        /// Static method called at Initialisation of an application to initialise the ServiceDiscovery
        /// </summary>
        /// <param name="initContext"></param>
        public static void Init(IServiceInitialisation initContext)
        {
            if (current == null)
            {
                current = new TDPServiceDiscovery();
                current.Initialise(initContext);
            }
        }

        /// <summary>
        /// Method called to replace a serviceFactory by another one. Used to switch between a test stub and the real system.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="serviceFactory"></param>
        public void SetServiceForTest(string key, IServiceFactory serviceFactory)
        {

            // if a key does not exist add it
            if (serviceCache.ContainsKey(key))
                serviceCache[key] = serviceFactory;
            else
                serviceCache.Add(key, serviceFactory);
        }

        /// <summary>
        /// Method called to completely clear the service discovery. Used to clear down after a test fixture
        /// which uses the ServiceDiscovery.
        /// </summary>
        public static void ResetServiceDiscoveryForTest()
        {
            current = null;
        }

        protected void Initialise(IServiceInitialisation initContext)
        {
            initContext.Populate(serviceCache);

        }
    }
}
