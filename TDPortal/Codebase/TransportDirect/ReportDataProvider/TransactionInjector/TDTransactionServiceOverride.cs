// ******************************************************** 
// NAME			: TDTransactionServiceOverride.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Provides nethod to override webservice url.
// ********************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TDTransactionServiceOverride.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:04   mturner
//Initial revision.
//
//   Rev 1.3   Nov 06 2003 17:19:56   geaton
//Refactored following handover from JT to GE.

using System;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Class overrides the default generated proxy for the WebService.
	/// This allows the web service URL to be specified at runtime
	/// by calling the override method.
	/// </summary>
	public class TDTransactionServiceOverride : TDTransactionService
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="url">URL to webservice.</param>
		public TDTransactionServiceOverride( string url )
		{
			this.Url = url;
		}
	}
}
