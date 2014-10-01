// *********************************************** 
// NAME                 : CJPFactory.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : ServiceFactory for CJP
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/StopEventManager/CJPFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:42   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:56   passuied
//Initial revision.
//
//   Rev 1.1   Feb 11 2005 11:08:20   passuied
//changes to comply to the new cjp
//
//   Rev 1.0   Dec 30 2004 15:10:14   passuied
//Initial revision.

using System;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.DepartureBoardService.StopEventManager
{
	/// <summary>
	/// ServiceFactory for CJP
	/// </summary>
	public class CJPFactory : IServiceFactory
	{
		public CJPFactory()
		{
		}

		/// <summary>
		///  Method used by the ServiceDiscovery to get an
		///  instance of an implementation of ICJP
		/// </summary>
		/// <returns>A new instance of a CJP.</returns>
		public Object Get()
		{
			return new TransportDirect.JourneyPlanning.CJP.CJP();
		}
	}
}
