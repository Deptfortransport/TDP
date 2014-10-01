// ************************************************************** 
// NAME			: TestMockRouteSupplierFactory.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 02/03/2005 
// DESCRIPTION	: Mock implemention of IRoutePriceSupplierFactory
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestMockRoutePriceSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:21:58   mturner
//Initial revision.
//
//   Rev 1.1   Nov 02 2005 09:31:14   RWilby
//Updated Unit Tests
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 15:26:54   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for TestMockRouteSupplierFactory.
	/// </summary>
	public class TestMockRoutePriceSupplierFactory : IRoutePriceSupplierFactory, IServiceFactory
	{
		public TestMockRoutePriceSupplierFactory()
		{
		
		}
		/// <summary>
		/// Return an appropriate RoutePriceSupplier for the given mode and operatorCode.
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		public IRoutePriceSupplier GetSupplier (TicketTravelMode mode, string operatorCode)
		{
			if(mode == TicketTravelMode.Rail)
				return new TestMockRailRoutePriceSupplier();
			else
				return new TestMockCoachRoutePriceSupplier();
		}

		#region Implementation of IServiceFactory
		/// <summary>
		/// Returns the current BayTextFilter object
		/// </summary>
		/// <returns>BayTextFilter object</returns>
		public object Get()
		{
			return this;
		}

		#endregion
	}
}
