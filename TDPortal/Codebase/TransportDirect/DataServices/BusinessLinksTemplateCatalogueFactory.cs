// *********************************************** 
// NAME                 :	BusinessLinksTemplateCatalogueFactory.cs
// AUTHOR               :	Tolu Olomolaiye
// DATE CREATED         :	22 Nov 2005 
// DESCRIPTION			:	Implementation of IServiceFactory for BusinessLinks
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/BusinessLinksTemplateCatalogueFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:20:44   mturner
//Initial revision.
//
//   Rev 1.0   Nov 22 2005 11:23:08   tolomolaiye
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Business Links implementation of IserviceFactory
	/// </summary>
	public class BusinessLinksTemplateCatalogueFactory : IServiceFactory
	{
		private BusinessLinksTemplateCatalogue current;

		/// <summary>
		/// Constructor class. Create and store a single instance of BusinessLinksTemplateCatalogue
		/// </summary>
		public BusinessLinksTemplateCatalogueFactory()
		{
			current = new BusinessLinksTemplateCatalogue();
		}

		/// <summary>
		/// Return BusinessLinksTemplateCatalogue
		/// </summary>
		/// <returns>BusinessLinksTemplateCatalogue object</returns>
		public object Get()
		{
			return current;
		}
	}
}
