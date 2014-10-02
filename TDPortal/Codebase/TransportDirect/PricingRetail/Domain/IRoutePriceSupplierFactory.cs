//********************************************************************************
//NAME         : IRoutePriceSupplierFactory.cs
//AUTHOR       : Russell Wilby
//DATE CREATED :20/10/2005
//DESCRIPTION  : IRoutePriceSupplierFactory interface
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/IRoutePriceSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:48   mturner
//Initial revision.
//
//   Rev 1.0   Oct 20 2005 11:24:10   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for IRoutePriceSupplierFactory.
	/// </summary>
	public interface IRoutePriceSupplierFactory
	{
		
		/// <summary>
		/// Return an appropriate RoutePriceSupplier for the given mode and operatorCode.
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		IRoutePriceSupplier GetSupplier (TicketTravelMode mode, string operatorCode);
	}
}
