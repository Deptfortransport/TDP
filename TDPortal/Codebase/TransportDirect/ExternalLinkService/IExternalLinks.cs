// *********************************************** 
// NAME                 : IExternalLinks.cs
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 07/06/2005 
// DESCRIPTION			: Definition of the External Links Interface
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ExternalLinkService/IExternalLinks.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:24   mturner
//Initial revision.
//
//   Rev 1.1   Jun 14 2005 18:58:42   rgeraghty
//Updated to include IR association
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.0   Jun 14 2005 18:56:16   rgeraghty
//Initial revision.

using System;


namespace TransportDirect.UserPortal.ExternalLinkService
{
	/// <summary>
	/// Definition of the External Links Interface - IExternalLinks.
	/// </summary>
	public interface IExternalLinks
	{

		/// <summary>
		/// Read only Indexer property which returns an ExternalLinkDetail
		/// </summary>		
		ExternalLinkDetail this[ string key]
		{
			get;
		}

		/// <summary>
		/// Locates a specified URL and returns an ExternalLinkDetail object
		/// </summary>		
		ExternalLinkDetail FindUrl( string url);
		

	}
}

