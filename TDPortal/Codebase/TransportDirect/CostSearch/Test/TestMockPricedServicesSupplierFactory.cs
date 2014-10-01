// ************************************************************** 
// NAME			: TestMockPricedServicesSupplierFactory.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 02/03/2005 
// DESCRIPTION	: Mock implemention of IPricedServicesSupplierFactory
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestMockPricedServicesSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:21:56   mturner
//Initial revision.
//
//   Rev 1.0   Oct 28 2005 15:26:56   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for TestMockRouteSupplierFactory.
	/// </summary>
	public class TestMockPricedServicesSupplierFactory : IPricedServicesSupplierFactory ,IServiceFactory
	{
		public TestMockPricedServicesSupplierFactory()
		{
		
		}


		/// <summary>
		/// Return an appropriate PricedServicesSupplier for the given mode
		/// </summary>
		/// <param name="mode">Transport mode</param>
		/// <returns>An implementation of IPricedServicesSupplier</returns>
		public IPricedServicesSupplier GetPricedServicesSupplier(ModeType mode)
		{
		
			return new TestMockPricedServicesSupplier();
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
