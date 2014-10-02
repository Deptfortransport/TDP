// *********************************************** 
// NAME			: TimeBasedPriceSupplier.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 20/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TimeBasedPriceRunner/TimeBasedPriceSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:20   mturner
//Initial revision.
//
//   Rev 1.4   Oct 16 2007 14:00:32   mmodi
//Amended to accept a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.3   Apr 26 2006 12:18:34   RPhilpott
//Don't assume we are necessarily in time-based partition ...
//
//   Rev 1.2   Mar 14 2006 08:41:46   build
//Automatically merged from branch for stream3353
//
//   Rev 1.1.1.1   Mar 10 2006 15:34:02   tmollart
//Updated from code review comments.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1.1.0   Feb 22 2006 11:56:46   RGriffith
//Changes made for multiple asynchronous ticket/costing
//
//   Rev 1.1   Oct 28 2005 18:32:42   RPhilpott
//Work in progress.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 21 2005 18:14:54   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.TimeBasedPriceRunner
{
	/// <summary>
	/// Implementation of ITimeBasedPriceSupplier which retrieves prices via the
	/// TimeBasedPriceSupplierCaller.
	/// </summary>
	public class TimeBasedPriceSupplier : ITimeBasedPriceSupplier
	{

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TimeBasedPriceSupplier()
		{
		}

		/// <summary>
		/// Prices the itinerary for an array element.
		/// </summary>
		/// <param name="pricingArrayCount">Elememt index to price</param>
		/// <returns>Async Call Status Object</returns>
		public AsyncCallStatus PriceItinerary(int pricingArrayCount)
		{
			TimeBasedPriceSupplierCaller caller = new TimeBasedPriceSupplierCaller();
			
			return caller.PriceItinerary(TDSessionManager.Current.GetSessionInformation(), TDSessionManager.Current.Partition, pricingArrayCount);
		}

		/// <summary>
		/// Prices an itinerary element.
		/// </summary>
		/// <param name="itinerary">Itinerary to price</param>
		/// <param name="discounts">Discounts</param>
		/// <returns>Async Call Status Object</returns>
		public AsyncCallStatus PriceItinerary(Itinerary itinerary, Discounts discounts)
		{
			// this is normally a remote object hosted within TDRemotingHost ... 
			TimeBasedPriceSupplierCaller caller = new TimeBasedPriceSupplierCaller();

			return caller.PriceItinerary(TDSessionManager.Current.GetSessionInformation(), TDSessionManager.Current.Partition);
		}

		/// <summary>
		/// Prices an itinerary element.
		/// </summary>
		/// <param name="itinerary">Itinerary to price</param>
		/// <param name="discounts">Discounts</param>
		/// <param name="requestID">Request ID to attach to the fares call if needed</param>
		/// <returns>Async Call Status Object</returns>
		public AsyncCallStatus PriceItinerary(Itinerary itinerary, Discounts discounts, string requestID)
		{
			// this is normally a remote object hosted within TDRemotingHost ... 
			TimeBasedPriceSupplierCaller caller = new TimeBasedPriceSupplierCaller();

			return caller.PriceItinerary(TDSessionManager.Current.GetSessionInformation(), TDSessionManager.Current.Partition, requestID);
		}
	}
}
