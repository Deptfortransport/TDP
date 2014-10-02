// *********************************************** 
// NAME			: TimeBasedPriceSupplierFactory.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 20/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TimeBasedPriceRunner/TimeBasedPriceSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:20   mturner
//Initial revision.
//
//   Rev 1.0   Oct 28 2005 14:50:18   RPhilpott
//Initial revision.
//
//   Rev 1.0   Oct 21 2005 18:14:58   jgeorge
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.TimeBasedPriceRunner
{
	/// <summary>
	/// IServiceFactory implementation for obtaining an ITimeBasedPriceSupplier
	/// implementation.
	/// </summary>
	public class TimeBasedPriceSupplierFactory : IServiceFactory
	{
		/// <summary>
		/// Default constructor. Does nothing.
		/// </summary>
		public TimeBasedPriceSupplierFactory()
		{ }

		/// <summary>
		/// Returns a new instance of TimeBasedPriceSupplierFactory
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return new TimeBasedPriceSupplier();
		}

	}
}
