// ***********************************************
// NAME			: IPricedServicesSupplierFactory
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-03-01
// DESCRIPTION	: IPricedServicesSupplierFactory interface
// ***********************************************
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/IPricedServicesSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:48   mturner
//Initial revision.
//
//   Rev 1.0   Mar 01 2005 18:45:34   RPhilpott
//Initial revision.
//

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{

	/// <summary>
	/// Interface that all PricedServicesSupplierFactories must fulfil
	/// </summary>
	public interface IPricedServicesSupplierFactory : IServiceFactory
	{

		/// <summary>
		/// Return an appropriate PricedServicesSupplier for the given mode
		/// </summary>
		/// <param name="mode">Transport mode</param>
		/// <returns>An implementation of IPricedServicesSupplier</returns>
		IPricedServicesSupplier GetPricedServicesSupplier(ModeType mode);
	}
}
