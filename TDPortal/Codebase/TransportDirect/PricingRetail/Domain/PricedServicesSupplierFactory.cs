// ***********************************************
// NAME			: PricedServicesSupplierFactory
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-03-01
// DESCRIPTION	: Creates a PricedServicesSupplier
// ***********************************************
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/PricedServicesSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:50   mturner
//Initial revision.
//
//   Rev 1.0   Mar 01 2005 18:45:34   RPhilpott
//Initial revision.
//

using System;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.PricingRetail.RBOGateway;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for PricedServicesSupplierFactory.
	/// </summary>
	public class PricedServicesSupplierFactory : IPricedServicesSupplierFactory
	{
		public PricedServicesSupplierFactory()
		{
		}

		public IPricedServicesSupplier GetPricedServicesSupplier(ModeType mode)
		{
			if (PricingUnitMergePolicy.IsRailMode(mode)) 
			{
				return new Gateway();
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		///  Return the PricedServicesSupplierFactory
		/// </summary>
		/// <returns>The current PricedServicesSupplierFactory.</returns>
		public Object Get()
		{
			return this;
		}	

	}
}
