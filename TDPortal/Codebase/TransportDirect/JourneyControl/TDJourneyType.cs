// *********************************************** 
// NAME			: TDJourneyType.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TDJourneyType enumeration
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDJourneyType.cs-arc  $
//
//   Rev 1.1   Oct 13 2008 16:45:04   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Jun 20 2008 15:01:38   mmodi
//Updated for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:24:02   mturner
//Initial revision.
//
//   Rev 1.3   Sep 17 2004 15:13:04   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.2   May 10 2004 15:04:28   RHopkins
//Extend Journey.
//Initial version of Itinerary Manager.
//
//   Rev 1.1   Aug 20 2003 17:55:54   AToner
//Work in progress
using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TDJourneyType.
	/// </summary>
	public enum TDJourneyType
	{
		PublicOriginal,
		PublicAmended,
		RoadCongested,
		Itinerary,
        Cycle
	}
}
