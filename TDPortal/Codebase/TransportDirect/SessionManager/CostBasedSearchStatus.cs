// *********************************************** 
// NAME         : CostBasedSearchStatus.cs
// AUTHOR       : Tim Mollart
// DATE CREATED : 22/12/2004
// DESCRIPTION  : Enumeration for cost based search status.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/CostBasedSearchStatus.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:20   mturner
//Initial revision.
//
//   Rev 1.1   Jan 26 2005 10:39:18   jmorrissey
//Removed NoResultsFound status
//
//   Rev 1.0   Dec 22 2004 15:29:24   tmollart
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Enumeration for Cost Based Search Status
	/// </summary>
	[CLSCompliant(false)]
	public enum CostBasedSearchStatus
	{
		None,
		InProgress,
		CompletedOK,
		TimedOut,		
		ValidationError
	};
}
