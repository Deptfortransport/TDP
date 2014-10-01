// *********************************************** 
// NAME                 : CjpManagerFactory.cs 
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2003-09-08 
// DESCRIPTION			: Factory that allows 
//						   ServiceDiscovery to create 
//						   instances of CJPManager.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CJPManagerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:40   mturner
//Initial revision.
//
//   Rev 1.0   Sep 08 2003 14:51:44   RPhilpott
//Initial Revision


using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Factory used by Service Discovery to create a CJP Stub.
	/// </summary>
	public class CjpManagerFactory : IServiceFactory
	{
		/// <summary>
		/// Constructor - nothing to do.
		/// </summary>
		public CjpManagerFactory()
		{
		}

		/// <summary>
		///  Method used by the ServiceDiscovery to get an
		///  instance of the CJPManager class. A new object
		///  is instantiated each time because a single shared
		///  instance would not be thread-safe.
		/// </summary>
		/// <returns>A new instance of a CJPManager.</returns>
		public Object Get()
		{
			return new CJPManager();
		}
	}
}
