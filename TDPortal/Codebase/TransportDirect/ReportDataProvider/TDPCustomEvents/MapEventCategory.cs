// *********************************************** 
// NAME                 : MapEventCategory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 28/08/2003 
// DESCRIPTION  : Defines categories for map
// events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/MapEventCategory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:26   mturner
//Initial revision.
//
//   Rev 1.0   Sep 09 2003 13:45:10   TKarsan
//Initial Revision
//
//   Rev 1.0   Aug 28 2003 17:04:28   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Enumeration containing classifiers for <c>MapEvent</c>.
	/// </summary>
	public enum MapEventCategory : int
	{
		MapRequest=1,
		MapPan,
		MapZoomFull,
		MapZoomToPoint,
		MapZoomToEnvelope,
		MapZoomPrevious,
		MapZoomPTRoute,
		MapZoomRoadRoute
	}
}
