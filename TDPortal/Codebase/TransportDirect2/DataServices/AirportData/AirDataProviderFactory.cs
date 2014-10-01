// *********************************************** 
// NAME             : AirDataProviderFactory.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 14 Oct 2013
// DESCRIPTION  	: Implementation of IServiceFactory for the AirDataProvider so that it can be used with TDServiceDiscovery
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ServiceDiscovery;

namespace TDP.Common.DataServices.AirportData
{
    /// <summary>
    /// Implementation of IServiceFactory for the AirDataProvider
    /// </summary>
    public class AirDataProviderFactory : IServiceFactory
	{
		private AirDataProvider current;

		#region Implementation of IServiceFactory

		/// <summary>
		/// Standard constructor. Initialises the AirDataProvider.
		/// </summary>
		public AirDataProviderFactory()
		{
			current = new AirDataProvider();
		}

		/// <summary>
		/// Returns the current AirDataProvider object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}

		#endregion

	}
}