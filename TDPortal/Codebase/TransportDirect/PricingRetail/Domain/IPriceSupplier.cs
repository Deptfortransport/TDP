// *********************************************** 
// NAME			: IPriceSupplier.cs
// AUTHOR		: Alistair Caunt
// DATE CREATED	: 26/09/03
// DESCRIPTION	: PriceSupplier interface
// ************************************************ 
//
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/IPriceSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:48   mturner
//Initial revision.
//
//   Rev 1.11   Apr 25 2005 10:08:00   jbroome
//Modified PriceRoute definition
//
//   Rev 1.10   Mar 22 2005 16:38:26   jbroome
//Updated PriceRoute() definition
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.9   Mar 10 2005 17:16:42   jbroome
//Updated comments
//
//   Rev 1.8   Mar 01 2005 18:43:14   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//

using System;
using System.Collections;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Interface to be fulfilled by all price suppliers
	/// </summary>
	public interface IPriceSupplier
	{

		/// <summary>
		/// Pass information to the PriceSupplier in advance of requesting the fares information
		/// This is used when we already have fare information, but require the PriceSupplier to organise it
		/// and provide the appropriate information when requested
		/// </summary>
		/// <param name="fareData"></param>
		void PreProcess(UnprocessedFareData fareData);

		/// <summary>
		/// Calculate the fares and use these to update a pricing unit
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <param name="discounts"></param>
		void PricePricingUnit(PricingUnit pricingUnit, Discounts discounts);

		/// <summary>
		/// Calculate the available fares for the dates and route specified
		/// Use these to create CostSearchTickets, which are added to TravelDates
		/// </summary>
		/// <param name="dates"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="dicounts"></param>
		/// <param name="journeyStore"></param></param>
		/// <returns>string array of resource ids for error messages</returns>
		string[] PriceRoute(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts, CJPSessionInfo sessionInfo, out PublicJourneyStore journeyStore);

	}
}
