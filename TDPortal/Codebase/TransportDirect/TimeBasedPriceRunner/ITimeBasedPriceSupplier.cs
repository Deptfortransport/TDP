// *********************************************** 
// NAME			: ITimeBasedPriceSupplier.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 20/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TimeBasedPriceRunner/ITimeBasedPriceSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:18   mturner
//Initial revision.
//
//   Rev 1.2   Oct 16 2007 13:59:50   mmodi
//Amended to accept a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.1   Mar 14 2006 08:41:46   build
//Automatically merged from branch for stream3353
//
//   Rev 1.0.1.0   Feb 22 2006 11:55:24   RGriffith
//Changes made for multiple asynchronous ticket/costing
//
//   Rev 1.0   Oct 28 2005 14:50:16   RPhilpott
//Initial revision.
//
//   Rev 1.0   Oct 21 2005 18:14:54   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;


namespace TransportDirect.UserPortal.TimeBasedPriceRunner
{
	/// <summary>
	/// Interface for classes which are used to invoke pricing operations for itineraries.
	/// The itinerary is expected to be in PricingRetailOptionsState property of the session
	/// manager, and therefore also accessible by retrieving this object from deferred storage.
	/// </summary>
	public interface ITimeBasedPriceSupplier
	{
		/// <summary>
		/// Calculates and adds fares to a particular array member of the pricing units in the itinerary, applying
		/// the given discounts.
		/// </summary>
		/// <param name="pricingArrayCount">Index value to price the specific itinerary array item</param>
		/// <returns>State of the call.</returns>
		AsyncCallStatus PriceItinerary(int pricingArrayCount);

		/// <summary>
		/// Calculates and adds fares to each of the pricing units in the itinerary, applying
		/// the given discounts.
		/// </summary>
		/// <param name="itinerary">Itinerary to price.</param>
		/// <param name="discounts">Discounts to apply when pricing.</param>
		/// <returns>State of the call.</returns>
		AsyncCallStatus PriceItinerary(Itinerary itinerary, Discounts discounts);

		/// <summary>
		/// Calculates and adds fares to each of the pricing units in the itinerary, applying
		/// the given discounts.
		/// </summary>
		/// <param name="itinerary">Itinerary to price.</param>
		/// <param name="discounts">Discounts to apply when pricing.</param>
		/// <param name="requestID">RequestID to supply with price call.</param>
		/// <returns>State of the call.</returns>
		AsyncCallStatus PriceItinerary(Itinerary itinerary, Discounts discounts, string requestID);
	}
}
