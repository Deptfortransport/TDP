// ***********************************************
// NAME 		: JourneyStatusType.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 20/08/2003
// DESCRIPTION 	: State-holder for adjustments.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyStatusType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:52   mturner
//Initial revision.
//
//   Rev 1.0   Aug 27 2003 10:50:10   PNorell
//Initial Revision
using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// JourneyStatusTypes is an enum that keeps track of the
	/// current action of the adjust state.
	/// </summary>
	public enum JourneyStatusType 
	{
		Idle,
		RequestReady,
		RequestSent,
		ResultsAvailable
	}
}
