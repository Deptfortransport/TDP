// ***********************************************
// NAME 		: TDAmendmentType.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 20/08/2003
// DESCRIPTION 	: Type of amendment the journey is
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDAmendmentType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:00   mturner
//Initial revision.
//
//   Rev 1.0   Aug 27 2003 10:50:10   PNorell
//Initial Revision
using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Type of amendment the journey has.
	/// </summary>
	public enum TDAmendmentType
	{
		OutwardJourney,
		ReturnJourney
	}
}
