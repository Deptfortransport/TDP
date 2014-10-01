// *********************************************** 
// NAME                 : TDTraceLevel.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Enumeration that defines the
// trace levels.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/TDTraceLevel.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:10   mturner
//Initial revision.
//
//   Rev 1.2   Jul 24 2003 18:27:54   geaton
//Added/updated comments


using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Enumeration that defines the levels that may be associated 
	/// with an <c>OperationalEvent</c>.
	/// </summary>
	public enum TDTraceLevel : int
	{
		Undefined,
		Off,
		Error,
		Warning,
		Info,
		Verbose
	}
}
