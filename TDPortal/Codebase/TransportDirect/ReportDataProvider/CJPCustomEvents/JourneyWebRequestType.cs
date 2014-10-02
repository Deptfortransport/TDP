// *********************************************** 
// NAME                 : JourneyWebRequestType.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 20/01/2005
// DESCRIPTION  : Enumeration containing categories 
// of JourneyWeb Request
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/CJPCustomEvents/JourneyWebRequestType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:08   mturner
//Initial revision.
//
//   Rev 1.0   Jan 24 2005 13:59:28   jgeorge
//Initial revision.

using System;

namespace TransportDirect.ReportDataProvider.CJPCustomEvents
{
	/// <summary>
	/// Enumeration containing categories of JourneyWeb Request
	/// </summary>
	public enum JourneyWebRequestType
	{
		Journey=1,
		StopEvent
	}
}

