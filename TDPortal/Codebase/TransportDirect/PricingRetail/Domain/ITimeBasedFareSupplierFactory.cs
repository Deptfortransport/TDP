// ***********************************************
// NAME			: ITimeBasedFareSupplierFactory.cs
// AUTHOR		: Murat Guney
// DATE CREATED	: 20/10/2005
// DESCRIPTION	: ITimeBasedFareSupplierFactory interface
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/ITimeBasedFareSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:50   mturner
//Initial revision.
//
//   Rev 1.1   Oct 20 2005 15:46:08   mguney
//IR associated.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 20 2005 15:44:36   mguney
//Initial revision.

using System;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// ITimeBasedFareSupplierFactory interface.
	/// </summary>
	public interface ITimeBasedFareSupplierFactory : IServiceFactory
	{
		/// <summary>
		/// Returns an appropriate TimeBasedFareSupplier for the given mode.
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		ITimeBasedFareSupplier GetSupplier(ModeType mode);
	}
}
