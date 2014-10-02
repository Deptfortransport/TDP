// *********************************************** 
// NAME                 : MapEventDisplayCategory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 4/09/2003 
// DESCRIPTION  : Defines display categories for map
// events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/MapEventDisplayCategory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:26   mturner
//Initial revision.
//
//   Rev 1.1   Sep 05 2003 10:17:38   ALole
//Updated enum to start at 1
//
//   Rev 1.0   Sep 04 2003 19:23:42   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Enumeration containing display classifiers for <c>MapEvent</c>.
	/// </summary>
	public enum MapEventDisplayCategory : int
	{
		OSStreetView=1,
		ScaleColourRaster50, 
		ScaleColourRaster250,
		MiniScale,
		Strategi
	}
}

