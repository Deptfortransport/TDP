// ***********************************************
// NAME			: ITimeBasedFareSupplier.cs
// AUTHOR		: Murat Guney
// DATE CREATED	: 20/10/2005
// DESCRIPTION	: Implementation of the ITimeBasedFareSupplier interface
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/ITimeBasedFareSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:50   mturner
//Initial revision.
//
//   Rev 1.4   Oct 16 2007 13:52:06   mmodi
//Amended to accept a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.3   Nov 02 2005 16:41:22   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Nov 02 2005 09:36:06   RPhilpott
//Change PricePricingUnit return type.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 20 2005 15:46:14   mguney
//IR associated.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 20 2005 15:44:42   mguney
//Initial revision.
using System;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Interface for Time Based Fare Suppliers.
	/// </summary>
	public interface ITimeBasedFareSupplier
	{
		/// <summary>
		/// Calculates the fares associated with the pricing unit using the discount information.
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <param name="discounts"></param>
		/// <returns>The updated PricingUnit</returns>
		PricingUnit PricePricingUnit(PricingUnit pricingUnit, Discounts discounts, CJPSessionInfo sessionInfo, string requestID);
	}
}
