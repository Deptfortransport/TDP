// *********************************************** 
// NAME                 : TDTraceLevelOverride.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 29/06/2004
// DESCRIPTION  : Enumeration for the override types 
// that can be used to force logging under certain
// circumstances
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/TDTraceLevelOverride.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:10   mturner
//Initial revision.
//
//   Rev 1.0   Jun 30 2004 17:03:48   jgeorge
//Initial revision.

using System;

namespace TransportDirect.Common.Logging
{
	public enum TDTraceLevelOverride : int
	{
		None,
		User
	}


}
