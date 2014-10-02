// ***********************************************
// NAME			: TestStubTimeBasedFareSupplierFactory.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2005-10-29
// DESCRIPTION	: TestStubTimeBasedFareSupplierFactory class implementation.
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestStubTimeBasedFareSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:22   mturner
//Initial revision.
//
//   Rev 1.0   Oct 29 2005 13:54:16   RPhilpott
//Initial revision.
//

using System;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.PricingRetail.RBOGateway;
using TransportDirect.UserPortal.PricingRetail.CoachFares;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for TimeBasedFareSupplierFactory.
	/// </summary>
	public class TestStubTimeBasedFareSupplierFactory : ITimeBasedFareSupplierFactory, IServiceFactory
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestStubTimeBasedFareSupplierFactory()
		{
			
		}

		#region TestStubTimeBasedFareSupplierFactory Members

		/// <summary>
		/// Returns an appropriate TimeBasedFareSupplier for the given mode.
		/// </summary>
		/// <param name="mode"></param>
		/// <returns></returns>
		public ITimeBasedFareSupplier GetSupplier(ModeType mode)
		{
			ITimeBasedFareSupplier supplier = null;

			if (PricingUnitMergePolicy.IsRailMode(mode)) 
			{
				supplier = new TestStubGateway();
			}
			else if (mode == ModeType.Coach)
			{
				// no test stub avaialble for coach ...
				supplier = null;
			}
			
			return supplier;
		}

		#endregion		

		#region IServiceFactory Members

		/// <summary>
		///  Returns the TestStubTimeBasedFareSupplierFactory
		/// </summary>
		/// <returns>The current TestStubTimeBasedFareSupplierFactory.</returns>
		public object Get()
		{
			return this;
		}

		#endregion
	}
}

