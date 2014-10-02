// *********************************************** 
// NAME         : CJPCallStatus.cs
// AUTHOR       : Peter Norell
// DATE CREATED : 23/09/2003
// DESCRIPTION  : Enumeration for CJP call status.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/CJPCallStatus.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:18   mturner
//Initial revision.
//
//   Rev 1.3   Nov 01 2005 15:12:14   build
//Automatically merged from branch for stream2638
//
//   Rev 1.2.1.0   Aug 31 2005 15:13:56   jbroome
//Added new state "Failed".
//Resolution for 2638: DEL 8 Stream: Visit Planner

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for CJPCallStatus.
	/// </summary>
	public enum CJPCallStatus 
	{
		None,
		InProgess,
		CompletedOK,
		TimedOut,
		NoJourneysFound,
		Failed
	}
}
