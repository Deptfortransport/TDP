// ***********************************************
// NAME 		: TDUserType.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 02/07/2004
// DESCRIPTION 	: Enum representing different types of user
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDUserType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:44   mturner
//Initial revision.
//
//   Rev 1.0   Jul 02 2004 13:34:06   jgeorge
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Enum representing different types of user. This value is passed to the CJP
	/// and used by that to determine whether to force verbose logging. It can be
	/// used in the same way in other areas of the system. It is also used to 
	/// determine whether to display additional links to diagnostic pages in the
	/// footer.
	/// </summary>
	public enum TDUserType : int
	{
		Standard = 0, // No special privileges
		CJP = 1       // CJP will force all operational events to be logged
	}
}
