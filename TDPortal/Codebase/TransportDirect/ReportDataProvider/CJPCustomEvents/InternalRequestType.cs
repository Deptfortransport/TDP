// *********************************************** 
// NAME                 : InternalRequestType.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 30/06/2004
// DESCRIPTION  : Enumeration containing types of internal request
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/CJPCustomEvents/InternalRequestType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:06   mturner
//Initial revision.
//
//   Rev 1.0   Jul 02 2004 13:51:00   jgeorge
//Initial revision.

using System;

namespace TransportDirect.ReportDataProvider.CJPCustomEvents
{
	/// <summary>
	/// Enumeration containing types of internal request
	/// </summary>
	public enum InternalRequestType
	{
		Road,
		RailTTBO,
		AirTTBO
	}
}

