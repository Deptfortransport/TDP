// ***********************************************
// NAME         : TDSessionManager.cs
// AUTHOR       : Peter Norell
// DATE CREATED : 18/01/2005
// DESCRIPTION  : Enumeration for all partitions available for the session manager.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDSessionPartition.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:44   mturner
//Initial revision.
//
//   Rev 1.1   Jan 26 2005 15:53:12   PNorell
//Support for partitioning the session information.
//
//   Rev 1.0   Jan 19 2005 11:40:36   PNorell
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Enumeration for all partitions available for the session manager.
	/// </summary>
	public enum TDSessionPartition
	{
		TimeBased,
		CostBased
	}
}
