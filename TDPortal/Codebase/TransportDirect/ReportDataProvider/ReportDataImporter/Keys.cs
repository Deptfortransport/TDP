// *********************************************** 
// NAME                 : Keys.cs
// AUTHOR               : Andy Lole
// DATE CREATED         : 02/10/2003 
// DESCRIPTION			: Property keys.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportDataImporter/Keys.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:58   mturner
//Initial revision.
//
//   Rev 1.3   Nov 26 2003 13:56:10   geaton
//Removed need to use separate connection string (stored as property) for initialising link to Reporting database. - reuse string stored in another property.
//
//   Rev 1.2   Nov 23 2003 14:42:32   geaton
//Added timeout support.
//
//   Rev 1.1   Nov 18 2003 21:27:40   geaton
//Added new key.
//
//   Rev 1.0   Oct 02 2003 17:27:26   ALole
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.ReportDataImporter
{
	/// <summary>
	/// Property keys of properties used by the importer.
	/// </summary>
	public class Keys
	{
		public const string CJPWebRequestWindow = "ReportDataImporter.CJPWebRequestWindow";
		public const string ReportDatabase = "ReportDataDB";
		public const string ImportTimeout = "ReportDataImporter.TimeoutDuration";
	}
}
