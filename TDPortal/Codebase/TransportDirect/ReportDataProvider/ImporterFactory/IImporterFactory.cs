// ************************************************ 
// NAME                 : IImporterFactory.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 25/11/2003 
// DESCRIPTION  : Interface for an Importer Factory.
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ImporterFactory/IImporterFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:46   mturner
//Initial revision.
//
//   Rev 1.2   Nov 26 2003 18:17:24   geaton
//Reverted interface to previous version.
//
//   Rev 1.1   Nov 26 2003 16:19:08   geaton
//Improved interface.
//
//   Rev 1.0   Nov 26 2003 11:00:32   geaton
//Initial Revision

using System;
using System.Collections;

namespace TransportDirect.ReportDataProvider.ImporterFactory
{
	/// <summary>
	/// Factory class for creating a group of Importer instances.
	/// </summary>
	public interface IImporterFactory
	{
		/// <summary>
		/// Creates a group of Importer instances.
		/// </summary>
		/// <param name="requestDate">
		/// Time at which request to use the Importer instances was made.
		/// </param>
		/// <param name="timeout">
		/// Duration in seconds to wait for SLQ commands to complete before timing out.
		/// </param>
		/// <param name="CJPWebRequestWindow">
		/// Duration of CJP web request window - required by some Importers when performing Imports ONLY.
		/// </param>
		/// <returns>Array of importers.</returns>
		/// <exception cref="TDException">
		/// Creation failed.
		/// </exception>
		Importer[] CreateImporters(DateTime requestDate, int timeout, int CJPWebRequestWindow);
	}
}
