// *********************************************** 
// NAME                 : ExternalLinksFactory.cs
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 07/06/2005 
// DESCRIPTION			: Factory class for the ExternalLinksService.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ExternalLinkService/ExternalLinksFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:24   mturner
//Initial revision.
//
//   Rev 1.3   Jun 27 2005 14:29:42   rgeraghty
//Updated documentation comments
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.2   Jun 27 2005 12:33:30   rgeraghty
//Updated with code review comments
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.1   Jun 14 2005 18:58:42   rgeraghty
//Updated to include IR association
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.0   Jun 14 2005 18:56:16   rgeraghty
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.ExternalLinkService
{
	/// <summary>
	/// Factory class for the ExternalLinksService.
	/// </summary>
	public class ExternalLinksFactory : IServiceFactory
	{
		#region private members

		
		/// <summary>Instance of ExternalLinks</summary>
		ExternalLinks currentLinks;

		#endregion

		#region constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public ExternalLinksFactory()
		{					
			currentLinks = new ExternalLinks();
		}

		#endregion
		
		#region methods

		/// <summary>
		/// Returns the current ExternalLinks object
		/// </summary>
		/// <returns>The current instance of ExternalLinks.</returns>
		public object Get()
		{
			return currentLinks;
		}

		#endregion
	}
}
