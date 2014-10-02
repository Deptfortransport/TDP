// *********************************************** 
// NAME                 : GazetteerEventCategory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 28/08/2003 
// DESCRIPTION  : Defines categories for gazeteer
// events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/GazetteerEventCategory.cs-arc  $
//
//   Rev 1.1   Aug 28 2012 10:19:58   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.0   Nov 08 2007 12:39:22   mturner
//Initial revision.
//
//   Rev 1.1   Feb 01 2005 14:51:04   passuied
//added logging for code gazetteer
//
//   Rev 1.0   Sep 02 2003 19:04:38   geaton
//Initial Revision
//
//   Rev 1.0   Aug 28 2003 17:04:26   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Enumeration containing classifiers for <c>GazetteerEvent</c>.
	/// </summary>
	public enum GazetteerEventCategory : int
	{
		GazetteerAddress=1,
		GazetteerPostCode,
		GazetteerPointOfInterest,
		GazetteerMajorStations,
		GazetteerAllStations,
		GazetteerLocality,
		GazetteerCode,
        GazetteerAutoSuggestAirport,
        GazetteerAutoSuggestCoach,
        GazetteerAutoSuggestFerry,
        GazetteerAutoSuggestRail,
        GazetteerAutoSuggestTMU,
        GazetteerAutoSuggestGroup,
        GazetteerAutoSuggestLocality,
        GazetteerAutoSuggestPointOfInterest,
        GazetteerAutoSuggestOther
    }
}
