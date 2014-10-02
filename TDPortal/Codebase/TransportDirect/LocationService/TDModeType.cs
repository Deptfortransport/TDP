// *********************************************** 
// NAME                 : TDModeType.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 29/12/2004
// DESCRIPTION  : Enumeration type for mode types known in the service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDModeType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:24   mturner
//Initial revision.
//
//   Rev 1.0   Jan 18 2005 17:38:48   passuied
//Initial revision.
//
//   Rev 1.0   Dec 30 2004 14:34:46   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Enumeration type for mode types known in the service
	/// </summary>
	public enum TDModeType
	{
		Undefined,
		Rail,
		Bus,
		Coach,
		Air,
		Ferry,
		Metro
		
	}
}
