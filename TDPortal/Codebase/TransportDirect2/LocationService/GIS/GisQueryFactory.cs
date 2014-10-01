// *********************************************** 
// NAME             : GisQueryFactory.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Factory class for returning GisQuery
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ServiceDiscovery;

namespace TDP.Common.LocationService.GIS
{
    /// <summary>
    /// Factory class for returning GisQuery
    /// </summary>
    public class GisQueryFactory : IServiceFactory
    {
        #region Private members

        private GisQuery current;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public GisQueryFactory()
        {
            current = new GisQuery();
        }

        #endregion

        #region IServiceFactory methods

        /// <summary>
        ///  Method used by the ServiceDiscovery to get the
        ///  instance of the GisQuery.
        /// </summary>
        /// <returns>The current instance of the GisQuery.</returns>
        public Object Get()
        {
            return current;
        }

        #endregion
    }
}
