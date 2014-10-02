// *********************************************** 
// NAME                 : JourneyPrepositiontCategory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 3/09/2003 
// DESCRIPTION  : Defines categories of Location 
// Request.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/CJPCustomEvents/JourneyPrepositionCategory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:06   mturner
//Initial revision.
//
//   Rev 1.3   Jan 24 2005 14:00:52   jgeorge
//Del 7 modifications
//
//   Rev 1.2   Sep 05 2003 10:16:12   ALole
//Started the enum from 1
//
//   Rev 1.1   Sep 03 2003 19:23:54   geaton
//Aligned type names with those used in reporting database tables.
//
//   Rev 1.0   Sep 03 2003 19:15:46   geaton
//Initial Revision
//
//   Rev 1.0   Sep 03 2003 16:56:44   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.CJPCustomEvents
{
	/// <summary>
	/// Enumeration containing categories of Location Request
	/// </summary>
	public enum JourneyPrepositionCategory
	{
		From=1,
		To,
		Via,
		StopEvent,
		FirstLastService
	}
}

