// *********************************************** 
// NAME                 : GisQueryFactory.cs 
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2003-09-20 
// DESCRIPTION			: Factory that allows 
//						   ServiceDiscovery to create 
//						   instances of GisQuery.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/GisQueryFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:06   mturner
//Initial revision.
//
//   Rev 1.2   Sep 22 2003 17:31:18   passuied
//made all objects serializable
//
//   Rev 1.1   Sep 20 2003 16:12:52   RPhilpott
//Fix stupid typo
//
//   Rev 1.0   Sep 20 2003 16:11:12   RPhilpott
//Initial Revision


using System;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for GisQueryFactory.
	/// </summary>
	[Serializable()]
	public class GisQueryFactory : IServiceFactory
	{
		private IGisQuery currentInstance; 

		/// <summary>
		/// Constructor - create the singleton instance.
		/// </summary>
		public GisQueryFactory()
		{
			currentInstance = new GisQuery();
		}

		/// <summary>
		///  Method used by ServiceDiscovery to get an
		///  instance of the GisQuery class.
		/// </summary>
		/// <returns>A new instance of a GisQuery.</returns>
		public Object Get()
		{
			return currentInstance;
		}


	}
}