// *********************************************** 
// NAME                 : TDMapHandoffFactory.cs 
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2003-09-24 
// DESCRIPTION			: Factory that allows 
//						   ServiceDiscovery to create 
//						   instances of TDMapHandoff.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDMapHandoffFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:02   mturner
//Initial revision.
//
//   Rev 1.1   Feb 06 2004 17:52:24   geaton
//Return new instance on every get. Fixes Incident 634.
//
//   Rev 1.0   Sep 25 2003 11:44:20   RPhilpott
//Initial Revision


using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Factory used by Service Discovery to create a TDMapHandoff.
	/// </summary>
	public class TDMapHandoffFactory : IServiceFactory
	{

		//private ITDMapHandoff instance = null;

		/// <summary>
		/// Constructor - instantiates the TDMapHandoff here.
		/// </summary>
		public TDMapHandoffFactory()
		{
			//instance = new TDMapHandoff();
		}

		/// <summary>
		///  Method used by the ServiceDiscovery to get an
		///  instance of the TDMapHandoff class. 
		/// </summary>
		/// <returns>The instance of TDMapHandoff.</returns>
		public Object Get()
		{
			return new TDMapHandoff();
		}
	}
}