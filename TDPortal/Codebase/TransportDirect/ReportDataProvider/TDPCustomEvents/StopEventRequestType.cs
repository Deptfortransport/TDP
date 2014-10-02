// *********************************************** 
// NAME                 : StopEventRequestType.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 02/02/2005
// DESCRIPTION  : enumeration containing the types of StopEventRequests
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/StopEventRequestType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:34   mturner
//Initial revision.
//
//   Rev 1.0   Feb 02 2005 18:34:48   passuied
//Initial revision.

using System;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{
	/// <summary>
	/// enumeration containing the types of StopEventRequests
	/// </summary>
	public enum StopEventRequestType
	{
		First = 1,
		Last,
		Time

	}
}
