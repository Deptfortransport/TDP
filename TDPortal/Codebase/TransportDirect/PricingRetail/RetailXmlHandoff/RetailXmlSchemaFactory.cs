// *********************************************** 
// NAME			: RetailXmlSchemaFactory.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 27.11.03
// DESCRIPTION	: Factory that allows ServiceDiscovery to create an instance of the
// RetailXmlSchema class.
// ************************************************

// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RetailXmlHandoff/RetailXmlSchemaFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:14   mturner
//Initial revision.
//
//   Rev 1.0   Nov 28 2003 15:49:08   COwczarek
//Initial revision.
//Resolution for 451: Retail Handoff does not need to read XML schema for each request

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.PricingRetail.RetailXmlHandoff
{
    /// <summary>
    /// Factory class for the Retail Xml component. This factory is used by 
    /// Service Discovery to instantiate a singleton RetailXmlSchema object.
    /// </summary>
    public class RetailXmlSchemaFactory : IServiceFactory
    {
        // Singleton instance of RetailXmlSchema
        RetailXmlSchema current;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RetailXmlSchemaFactory()
        {
            current = new RetailXmlSchema();
        }

        /// <summary>
        /// Returns a singleton instance of RetailXmlSchema
        /// </summary>
        /// <returns>An instance of RetailXmlSchema</returns>
        public object Get()
        {
            return current;
        }
    }
    
}
