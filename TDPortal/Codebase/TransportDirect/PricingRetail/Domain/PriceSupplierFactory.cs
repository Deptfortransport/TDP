// *********************************************** 
// NAME			: PriceSupplierFactory
// AUTHOR		: Alistair Caunt
// DATE CREATED	: 05/10/03
// DESCRIPTION	: Implementation of the PriceSupplierFactory class

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.PricingRetail.RBOGateway;
using TransportDirect.UserPortal.PricingRetail.NatExFares;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for PriceSupplierFactory.
	/// </summary>
	public class PriceSupplierFactory : IPriceSupplierFactory
	{

		private static IList natExCodes = null;
		private static IList sCLCodes = null;

		/// <summary>
		/// Static constructor to load up the NatEx and SCL operator codes from Data Services.
		/// The synchronization is not as efficient as it could be, but is safe.
		/// </summary>
		public PriceSupplierFactory()
		{
			if (natExCodes == null || sCLCodes == null) 
			{
				lock(this)
				{
					IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
					natExCodes = ds.GetList(DataServiceType.NatExCodes);
					sCLCodes = ds.GetList(DataServiceType.SCLCodes);
				}
			}
		}

		/// <summary>
		/// Return an appropriate PriceSupplier for the given mode and operatorCode.
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		public IPriceSupplier GetPriceSupplier(ModeType mode, string operatorCode)
		{
			IPriceSupplier supplier = null;
            
			if (PricingUnitMergePolicy.IsRailMode(mode)) 
            {
				supplier = new Gateway();
			}
			else if (mode == ModeType.Coach)
			{
				if	(operatorCode == null || operatorCode.Length == 0)
				{
					supplier = new NatExFaresSupplier();
				}
				else if (natExCodes.Contains(operatorCode)) 
				{
					supplier = new NatExFaresSupplier(new NatExTicketNameRule());
				} 
				else if (sCLCodes.Contains(operatorCode)) 
				{
					supplier = new NatExFaresSupplier(new SCLTicketNameRule());
				}
				else
				{
					supplier = new NatExFaresSupplier();
				}
			} 
			if (supplier == null) 
			{
				throw new TDException("Unable to associate a PriceSupplier with this PricingUnit", false, TDExceptionIdentifier.PRHUnabletoPricePricingUnit);
			}
			return supplier;
		}

		/// <summary>
		///  Return the PriceSupplierFactory
		/// </summary>
		/// <returns>The currentPriceSupplierFactory.</returns>
		public Object Get()
		{
			return this;
		}	

}
}
