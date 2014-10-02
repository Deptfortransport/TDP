// *********************************************** 
// NAME			: RetailerCatalogueFactory.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 02.10.03
// DESCRIPTION	: Factory that allows ServiceDiscovery to create an instance of the
// RetailerCatalogue class.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/RetailerCatalogueFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:54   mturner
//Initial revision.
//
//   Rev 1.5   Nov 18 2003 16:10:08   COwczarek
//SCR#247 :Add $Log: for PVCS history
 
using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
    /// <summary>
    /// Factory class for the RetailerCatalogue component. This factory is used by 
    /// Service Discovery to instantiate a singleton RetailerCatalogue.
    /// </summary>
    public class RetailerCatalogueFactory : IServiceFactory
    {

        // Singleton instance of RetailerCatalogue
        RetailerCatalogue current;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RetailerCatalogueFactory()
        {
            current = new RetailerCatalogue();
        }

        /// <summary>
        /// Returns a singleton instance of RetailerCatalogue
        /// </summary>
        /// <returns>An instance of RetailerCatalogue</returns>
        public object Get()
        {
            return current;
        }
    }

}
