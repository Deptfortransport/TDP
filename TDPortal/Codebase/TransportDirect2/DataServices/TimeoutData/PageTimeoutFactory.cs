// *********************************************** 
// NAME             : PageTimeoutFactory.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 22 Aug 2013
// DESCRIPTION  	: Factory class for PageTimeout data
// ************************************************
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ServiceDiscovery;

namespace TDP.Common.DataServices.TimeoutData
{
    /// <summary>
    /// Factory class for PageTimeout data
    /// </summary>
    public class PageTimeoutFactory : IServiceFactory
    {
        private PageTimeoutData current;

        #region Implementation of IServiceFactory

        /// <summary>
        /// Constructor. Initialises the PageTimeoutData.
        /// </summary>
        public PageTimeoutFactory()
        {
            current = new PageTimeoutData();
        }

        /// <summary>
        /// Returns the current PageTimeoutData object
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            return current;
        }

        #endregion
    }
}
