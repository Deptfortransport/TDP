// *********************************************** 
// NAME			: AirDataProviderFactory.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 12/05/2004
// DESCRIPTION	: Implementation of IServiceFactory for the AirDataProvider
// so that it can be used with TDServiceDiscovery
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/AirDataProviderFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:20   mturner
//Initial revision.
//
//   Rev 1.2   Jul 08 2004 14:11:32   jgeorge
//Actioned review comments
//
//   Rev 1.1   May 13 2004 09:28:12   jgeorge
//Modified namespace to TransportDirect.UserPortal.AirDataProvider
//
//   Rev 1.0   May 12 2004 15:59:48   jgeorge
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.AirDataProvider
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
