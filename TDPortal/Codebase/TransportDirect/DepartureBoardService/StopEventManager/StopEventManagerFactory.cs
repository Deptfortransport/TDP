// *********************************************** 
// NAME                 : StopEventManagerFactory.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 05/01/2005
// DESCRIPTION  : Factory class for StopEvent manager
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/StopEventManager/StopEventManagerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:46   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:58   passuied
//Initial revision.
//
//   Rev 1.0   Jan 05 2005 16:52:20   passuied
//Initial revision.

using System;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.DepartureBoardService.StopEventManager
{
	/// <summary>
	/// Factory class for StopEvent manager
	/// </summary>
	public class StopEventManagerFactory : IServiceFactory
	{
		public StopEventManagerFactory()
		{
		}

		/// <summary>
		/// Implementation of IServiceFactory. Returns a new StopEventManager every time it's requested.
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return new StopEventManager();
		}
	}
}
