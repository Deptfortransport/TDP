// *********************************************** 
// NAME                 : DepartureBoardServiceFactory.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 28/02/2005
// DESCRIPTION  : Factory Class for DepartureBoardService
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardServiceFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:06   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 17:21:06   passuied
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;

namespace TransportDirect.UserPortal.DepartureBoardService
{
	/// <summary>
	/// Factory Class for DepartureBoardService
	/// </summary>
	public class DepartureBoardServiceFactory : IServiceFactory
	{
		IDepartureBoardService current = null;
		public DepartureBoardServiceFactory()
		{
			current = new DepartureBoardService();
		}

		/// <summary>
		/// implementation of IServiceFactory class
		/// </summary>
		/// <returns>current instance of
		/// DepartureBoardService.</returns>
		public object Get()
		{
			return current;
		}
	}
}
