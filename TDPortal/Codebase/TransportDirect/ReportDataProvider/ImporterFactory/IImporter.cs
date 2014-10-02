// *************************************************** 
// NAME                 : IImporter.cs 
// AUTHOR               : Andy Lole
// DATE CREATED         : 12/09/2003 
// DESCRIPTION  : Interface for a ReportData Importer.
// *************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ImporterFactory/IImporter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:44   mturner
//Initial revision.
//
//   Rev 1.0   Nov 26 2003 11:00:30   geaton
//Initial Revision

using System;
using System.Data.SqlClient;

namespace TransportDirect.ReportDataProvider.ImporterFactory
{
	public interface IImporter
	{
		
		/// <summary>
		/// Performs import of staging data to the reporting database.
		/// Also performs necessary auditing.
		/// </summary>
		/// <param name="toDate">
		/// Date up to which staging data should be imported.
		/// </param>
		/// <exception cref="TDException">
		/// Thrown if error occurs during operation.
		/// </exception>
		void Import(DateTime toDate);

		/// <summary>
		/// Performs archive of the staging data used by the Importer to perform imports.
		/// Only staging data that has been imported will be archived.
		/// Also performs necessary auditing.
		/// </summary>
		/// <exception cref="TDException">
		/// Thrown if error occurs during operation.
		/// </exception>
		void Archive();

		/// <summary>
		/// Gets the date of the latest imported staging data.
		/// </summary>
		DateTime LatestImported {get;}

		/// <summary>
		/// Gets and the Import Enabled flag.
		/// </summary>
		bool ImportEnabled {get; set;}
		
		/// <summary>
		/// Gets and sets the Archive Enabled flag.
		/// </summary>
		bool ArchiveEnabled {get; set;}

		/// <summary>
		/// Gets the staging table/s used by the Importer.
		/// </summary>
		string[] StagingTableName {get;}
		
		/// <summary>
		/// Gets the stored procedure used by the Importer.
		/// </summary>
		string StoredProcedureName {get;}
	}
}
