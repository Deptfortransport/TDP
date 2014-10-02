// ***********************************************
// NAME			: IPriceSupplierFactory
// AUTHOR		: Alistair Caunt
// DATE CREATED	: 23/10/03
// DESCRIPTION	: Implementation of the IPriceSupplierFactory interface
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/IPriceSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:48   mturner
//Initial revision.
//
//   Rev 1.0   Oct 23 2003 16:04:08   acaunt
//Initial Revision
using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{

	/// <summary>
	/// Interface that all PriceSupplierFactories must fulfil
	/// </summary>
	public interface IPriceSupplierFactory : IServiceFactory
	{

		/// <summary>
		/// Return an appropriate PriceSupplier for the given mode and operatorCode.
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		IPriceSupplier GetPriceSupplier(ModeType mode, string operatorCode);
	}
}
