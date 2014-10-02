// *********************************************** 
// NAME			: PriceSupplierFactory
// AUTHOR		: Alistair Caunt
// DATE CREATED	: 05/10/03
// DESCRIPTION	: Implementation of the PriceSupplierFactory class

using System;
using System.Collections;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.PricingRetail.RBOGateway;
using TransportDirect.UserPortal.PricingRetail.NatExFares;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for PriceSupplierFactory.
	/// </summary>
	public class TestStubPriceSupplierFactory : IPriceSupplierFactory
	{

		private static IList natExCodes = null;
		private static IList sCLCodes = null;

		/// <summary>
		/// Static constructor to load up the NatEx and SCL operator codes from Data Services.
		/// The synchronization is not as efficient as it could be, but is safe.
		/// T
		/// </summary>
		public TestStubPriceSupplierFactory()
		{
			if (natExCodes == null || sCLCodes == null) 
			{
				lock(this)
				{
					DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
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
				supplier = new  TestStubGateway();
			}
			else if (mode == ModeType.Coach)
			{
				if (natExCodes.Contains(operatorCode)) 
				{
					supplier = new NatExFaresSupplier(new NatExTicketNameRule());
				} 
				else if (sCLCodes.Contains(operatorCode)) 
				{
					supplier = new NatExFaresSupplier(new SCLTicketNameRule());
				}
			} 
			if (supplier == null) 
			{
				throw new Exception("Price Supplier unavailable to price a PricingUnit of mode "+mode+" and operatorCode "+operatorCode);	
			}
			return supplier;
		}

		/// <summary>
		///  Method used by ServiceDiscovery to get an
		///  instance of the TestStubGisQuery class.
		/// </summary>
		/// <returns>A new instance of a TestStubGisQuery.</returns>
		public Object Get()
		{
			return this;
		}	

	}
}

