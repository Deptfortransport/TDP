// *********************************************** 
// NAME                 : FindTrainPageState.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 14/07/2004 
// DESCRIPTION  : PageState for FindTrain pages.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindTrainPageState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:28   mturner
//Initial revision.
//
//   Rev 1.2   Jul 29 2004 11:16:26   COwczarek
//Change to inherit from FindCoachTrainPageState. Move
//members to base class.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.1   Jul 22 2004 16:20:08   COwczarek
//Add methods to save/reinstate journey parameters for
//ambiguity resolution back-out
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.0   Jul 14 2004 12:59:44   passuied
//Initial Revision


using System;
using TransportDirect.UserPortal.LocationService;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// PageState for FindTrain pages.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class FindTrainPageState : FindCoachTrainPageState
	{

        /// <summary>
        /// Constructor. Sets the Mode property to indicate Find A Train mode.
        /// </summary>
        public FindTrainPageState()
		{
			findMode = FindAMode.Train;
		}

	}
}
