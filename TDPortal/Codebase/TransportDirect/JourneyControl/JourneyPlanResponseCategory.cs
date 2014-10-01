// *********************************************** 
// NAME                 : JourneyResponseCategory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 23/09/2003 
// DESCRIPTION  : Defines response categories for 
// JourneyPlanResultEvents.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyPlanResponseCategory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:50   mturner
//Initial revision.
//
//   Rev 1.0   Sep 24 2003 18:22:00   geaton
//Initial Revision

using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Enumeration containing response classifiers for <c>JourneyPlanResultEvent</c>.
	/// </summary>
	public enum JourneyPlanResponseCategory : int
	{
		Results=1,
		ZeroResults,
		Failure
	}
}

