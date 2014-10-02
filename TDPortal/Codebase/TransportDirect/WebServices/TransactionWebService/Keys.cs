// *********************************************** 
// NAME			: Keys.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 11/11/2003 
// DESCRIPTION	: Contains keys used by the web service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/TransactionWebServices/Keys.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 13:55:26   mturner
//Initial revision.
//
//   Rev 1.0   Nov 12 2003 16:10:14   geaton
//Initial Revision

using System;

namespace TransportDirect.ReportDataProvider.TransactionWebService
{
	/// <summary>
	/// Container class used to hold TransactionInjector specific key constants
	/// </summary>
	public class Keys
	{
		public const string CJPConfig = "TransactionWebService.CJPConfigFilepath";
		public const string LocationServiceName = "locationservice.servicename";
		public const string LocationServerName = "locationservice.servername";
		public const string DefaultDB = "DefaultDB";
		public const string EsriDB = "EsriDB";

	}
}
