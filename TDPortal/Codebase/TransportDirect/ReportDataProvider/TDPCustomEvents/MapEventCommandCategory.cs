// *********************************************** 
// NAME                 : MapEventCommandCategory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 04/09/2003 
// DESCRIPTION  : Defines command categories for map
// events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/MapEventCommandCategory.cs-arc  $
//
//   Rev 1.1   Oct 13 2008 16:46:34   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Sep 02 2008 11:42:04   mmodi
//Updated for cycle map tiles
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:39:26   mturner
//Initial revision.
//
//   Rev 1.0   Sep 04 2003 19:23:40   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Enumeration containing command classifiers for <c>MapEvent</c>.
	/// </summary>
	public enum MapEventCommandCategory : int
	{
		MapInitialDisplay=1,
		MapPan,
		MapZoom,
		MapOverlay,
        MapTile
	}
}
