// *********************************************** 
// NAME			: IParkAndRideCatalgue.cs
// AUTHOR		: Neil Moorhouse
// DATE CREATED	: 22/07/2005
// DESCRIPTION	: Interface for Park And Ride lookup and caching classes
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/IParkAndRideCatalogue.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:08   mturner
//Initial revision.
//
//   Rev 1.3   Mar 23 2006 17:58:30   build
//Automatically merged from branch for stream0025
//
//   Rev 1.2.1.0   Mar 08 2006 14:30:06   tolomolaiye
//Changes for Park and Ride Phase II
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.2   Sep 02 2005 15:11:06   NMoorhouse
//Updated following review comments (CR003)
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.1   Aug 12 2005 11:13:34   NMoorhouse
//DN058 Park And Ride, end of CUT
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 03 2005 10:22:20   NMoorhouse
//Initial revision.
//
using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// The Inrterface for ParkAndRideCatalogue.
	/// </summary>
	public interface IParkAndRideCatalogue
	{
		ParkAndRideInfo[] GetRegion(string regionName);
		ParkAndRideInfo[] GetAll();
		ParkAndRideInfo GetScheme(int parkAndRideId);
	}
}