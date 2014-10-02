// *********************************************** 
// NAME                 : FindTrunkPageState.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 14/07/2004 
// DESCRIPTION  : Other Special PageState for FindTrunk and FindStation pages. Inherit from FindPageState
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindTrunkStationPageState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:28   mturner
//Initial revision.
//
//   Rev 1.0   Nov 03 2004 12:53:36   passuied
//Initial Revision
using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Other Special PageState for FindTrunk and FindStation pages.
	/// Inherit from FindPageState
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class FindTrunkStationPageState : FindTrunkPageState
	{
		public FindTrunkStationPageState()
		{
			findMode = FindAMode.TrunkStation;
		}
	}
}
