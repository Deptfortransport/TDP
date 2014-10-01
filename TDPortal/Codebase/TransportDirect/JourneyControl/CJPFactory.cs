// *********************************************** 
// NAME                 : CjpFactory.cs 
// AUTHOR               : Richard Philpott
// DATE CREATED         : 21/07/2003 
// DESCRIPTION			: ServiceDiscovery Factory 
//							for the CJP.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CJPFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:38   mturner
//Initial revision.
//
//   Rev 1.1   Sep 19 2003 17:44:44   RPhilpott
//Move to correct namespace
//
//   Rev 1.0   Sep 19 2003 15:23:04   RPhilpott
//Initial Revision
//

using System;
using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Factory used by Service Discovery to create CJP instance
	/// </summary>
	public class CjpFactory : IServiceFactory
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public CjpFactory()
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
