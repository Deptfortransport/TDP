// *********************************************** 
// NAME             : AdditionalDataFactory.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2014
// DESCRIPTION  	: AdditionalDataFactory factory class to return an AdditionalData object
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Text;

using TDP.Common.ServiceDiscovery;

namespace TDP.UserPortal.AdditionalDataModule
{
    /// <summary>
    /// AdditionalDataFactory factory class to return an AdditionalData object
    /// </summary>
    public class AdditionalDataFactory : IServiceFactory
    {
        private AdditionalData current;

        /// <summary>
        /// Constructor - create the singleton instance.
        /// </summary>
        public AdditionalDataFactory()
        {
            current = new AdditionalData();
        }

        /// <summary>
        ///  Method used by ServiceDiscovery to get an
        ///  instance of the AdditionalData class.
        /// </summary>
        /// <returns>A new instance of a AdditionalData.</returns>
        public Object Get()
        {
            return current;
        }
    }
}
