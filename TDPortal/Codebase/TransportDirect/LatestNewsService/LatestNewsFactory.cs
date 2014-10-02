// *********************************************** 
// NAME			: LatestNewsFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 08/04/08
// DESCRIPTION	: Class to provide Latest news information 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LatestNewsService/LatestNewsFactory.cs-arc  $
//
//   Rev 1.0   Apr 09 2008 18:20:12   mmodi
//Initial revision.
//

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LatestNewsService
{
    /// <summary>
    /// Summary description for LatestNewsFactory
    /// </summary>
    public class LatestNewsFactory : IServiceFactory
    {
        private LatestNewsProvider current;

		#region Implementation of IServiceFactory
		/// <summary>
        /// Standard constructor. Initialises the LatestNewsProvider.
		/// </summary>
        public LatestNewsFactory()
		{
			current = new LatestNewsProvider();
		}

		/// <summary>
        /// Returns the current LatestNewsProvider object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}

		#endregion
    }
}
