// ************************************************************** 
// NAME			: TestMockJourneyFareFilter.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 28/10/2005 
// DESCRIPTION	: Mock implemention of IRoutePriceSupplier
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestMockJourneyFareFilter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:21:56   mturner
//Initial revision.
//
//   Rev 1.1   Nov 05 2005 16:37:14   RWilby
//Added TestMockJourneyFareFilterFactory class to file
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Nov 02 2005 09:32:56   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;


namespace  TransportDirect.UserPortal.CostSearch
{
	public class TestMockJourneyFareFilterFactory : IJourneyFareFilterFactory,IServiceFactory 
	{
		/// <summary>
		/// Gets the filter.
		/// </summary>
		/// <returns></returns>
		public IJourneyFareFilter GetFilter()
		{
			return new TestMockJourneyFareFilter();
		}

		#region Implementation of IServiceFactory
		/// <summary>
		/// Returns the current TestMockJourneyFareFilter object
		/// </summary>
		/// <returns>TestMockJourneyFareFilter object</returns>
		public object Get()
		{
			return this;
		}
		#endregion
	}
	/// <summary>
	/// Summary description for TestMockJourneyFareFilter.
	/// </summary>
	public class TestMockJourneyFareFilter :IJourneyFareFilter
	{
		/// <summary>
		/// implement the interface
		/// </summary>
		/// <param name="originalJourney"></param>
		/// <param name="tickets"></param>
		/// <returns></returns>
		public TDJourneyResult FilterJourneys(TDJourneyResult originalJourney,CostSearchTicket[] tickets)
		{
			return originalJourney;
		}
	}
}
