// *********************************************** 
// NAME                 : AuditType.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 25/11/2003 
// DESCRIPTION  : Audit types.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ImporterFactory/AuditType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:44   mturner
//Initial revision.
//
//   Rev 1.0   Nov 26 2003 11:00:28   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.ImporterFactory
{
	/// <summary>
	/// Enumeration containing the types of audit that may be recorded in the 
	/// ReportingStagingDataAuditType table by Importer instances.
	/// The enumeration type 'names' MUST match the values in the RSDATName column
	/// of the ReportingStagingDataAuditType table.
	/// </summary>
	public enum AuditType : int
	{
		LastImport = 1,
		LatestImported,
		LastArchive
	}
}
