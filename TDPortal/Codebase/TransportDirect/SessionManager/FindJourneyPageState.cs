// *********************************************** 
// NAME         : FindJourneyPageState.cs
// AUTHOR       : Tim Mollart
// DATE CREATED : 22/12/2004
// DESCRIPTION  : Base class for journey based page states.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindJourneyPageState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:26   mturner
//Initial revision.
//
//   Rev 1.1   Feb 16 2005 10:14:10   rhopkins
//Made Serializable because it is deferred in the Session
//
//   Rev 1.0   Dec 22 2004 15:29:24   tmollart
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for FindJourneyPageState.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class FindJourneyPageState : FindPageState
	{
		public FindJourneyPageState()
		{
			//
			//
		}
	}
}
