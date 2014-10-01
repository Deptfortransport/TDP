// *********************************************** 
// NAME             : DepartureBoardServiceFactory.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: Factory used by Service Discovery to create a DepartureBoardService
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ServiceDiscovery;

namespace TDP.UserPortal.DepartureBoardService
{
    /// <summary>
    /// Factory used by Service Discovery to create a DepartureBoardService
    /// </summary>	
    public class DepartureBoardServiceFactory : IServiceFactory
    {
        #region Private members

        private DepartureBoardService current = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DepartureBoardServiceFactory()
        {
            current = new DepartureBoardService();
        }

        #endregion

        #region IServiceFactory members

        /// <summary>
        /// Method used by the ServiceDiscovery to get the instance of the DepartureBoardService.
        /// </summary>
        /// <returns>The current instance of the DepartureBoardService</returns>
        public Object Get()
        {
            return current;
        }

        #endregion
    }
}
