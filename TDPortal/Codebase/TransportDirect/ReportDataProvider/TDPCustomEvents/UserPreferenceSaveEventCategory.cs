// ********************************************************* 
// NAME                 : UserPreferenceSaveEventCategory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/09/2003 
// DESCRIPTION  : Defines categories user preference save
// events.
// ********************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/UserPreferenceSaveEventCategory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:38   mturner
//Initial revision.
//
//   Rev 1.0   Sep 18 2003 16:40:58   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// Enumeration containing classifiers for <c>UserPreferenceSaveEvent</c>.
	/// </summary>
	public enum UserPreferenceSaveEventCategory : int
	{
		JourneyPlanningOptions=1,
		FareOptions,
		News
	}
}

