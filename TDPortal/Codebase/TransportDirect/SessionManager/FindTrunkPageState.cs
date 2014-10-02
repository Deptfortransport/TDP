// *********************************************** 
// NAME                 : FindTrunkPageState.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 14/07/2004 
// DESCRIPTION  : PageState for FindTrunk pages. Inherit from FindPageState
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindTrunkPageState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:28   mturner
//Initial revision.
//
//   Rev 1.2   Nov 03 2004 12:54:38   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.1   Nov 01 2004 15:29:40   passuied
//Added Property StationMode
//
//   Rev 1.0   Jul 14 2004 12:59:44   passuied
//Initial Revision

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// PageState for FindTrunk pages. Inherit from FindPageState
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class FindTrunkPageState : FindPageState
	{
		
		public FindTrunkPageState()
		{
			findMode = FindAMode.Trunk;			
		}

		
	}


}
