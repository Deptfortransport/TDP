// *********************************************** 
// NAME                 : TDCodeType.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 29/12/2004
// DESCRIPTION  : Enumeration for all code types to translate in the service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDCodeType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:22   mturner
//Initial revision.
//
//   Rev 1.1   May 03 2007 12:28:32   mturner
//Added new code type of NAPTAN
//
//   Rev 1.0   Jan 18 2005 17:38:42   passuied
//Initial revision.
//
//   Rev 1.0   Dec 30 2004 14:34:22   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Enumeration for all code types to translate in the service
	/// </summary>
	public enum TDCodeType
	{
		CRS,
		SMS,
		IATA,
		Postcode,
		NAPTAN
	}
}
