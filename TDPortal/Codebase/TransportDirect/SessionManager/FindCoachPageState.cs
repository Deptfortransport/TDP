// *********************************************** 
// NAME                 : FindCoachPageState.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 14/07/2004 
// DESCRIPTION  : PageState for FindCoach pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindCoachPageState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:24   mturner
//Initial revision.
//
//   Rev 1.2   Jan 31 2005 16:57:38   tmollart
//Changed reinstatejourneyparameters and savejourneyparameters methods to use TDJourneyParams instead of TDJourneyParamsMulti.
//
//   Rev 1.1   Jul 29 2004 11:15:38   COwczarek
//Change to inherit from FindCoachTrainPageState
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.0   Jul 14 2004 12:59:44   passuied
//Initial Revision


using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// PageState for FindCoach pages.
	/// </summary>
	
	[CLSCompliant(false)]
	[Serializable]
	public class FindCoachPageState : FindCoachTrainPageState
	{

        /// <summary>
        /// Constructor. Sets the Mode property to indicate Find A Coach mode.
        /// </summary>
		public FindCoachPageState()
		{
			findMode = FindAMode.Coach;
		}
	}
}
