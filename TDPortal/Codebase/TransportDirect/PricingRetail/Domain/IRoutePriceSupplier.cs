//********************************************************************************
//NAME         : IRoutePriceSupplier.cs
//AUTHOR       : Russell Wilby
//DATE CREATED : 02/10/2005
//DESCRIPTION  : IRoutePriceSupplier interface
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/IRoutePriceSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:48   mturner
//Initial revision.
//
//   Rev 1.1   Oct 27 2005 17:16:42   RWilby
//Updated interface
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 20 2005 11:23:48   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
using System;
using System.Collections;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for IRouteSupplier.
	/// </summary>
	public interface IRoutePriceSupplier
	{
		
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
		string[] PriceRoute(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts, 
			CJPSessionInfo sessionInfo, string operatorCode, int combinedTickets,int legNumber, QuotaFareList quotaFares);
	}
}
