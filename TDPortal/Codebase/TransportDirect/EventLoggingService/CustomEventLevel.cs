// *********************************************** 
// NAME                 : CustomEventLevel.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Enumeration of custom
// event levels.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/CustomEventLevel.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:00   mturner
//Initial revision.
//
//   Rev 1.1   Jul 24 2003 18:27:28   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Enumeration containing levels that a <c>CustomEvent</c> may take.
	/// </summary>
	public enum CustomEventLevel : int
	{
		Undefined,
		Off,
		On
	}
}
