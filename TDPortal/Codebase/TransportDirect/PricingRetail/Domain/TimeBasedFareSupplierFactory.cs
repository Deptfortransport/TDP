// ***********************************************
// NAME			: TimeBasedFareSupplierFactory.cs
// AUTHOR		: Murat Guney
// DATE CREATED	: 20/10/2005
// DESCRIPTION	: TimeBasedFareSupplierFactory class implementation.
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/TimeBasedFareSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:00   mturner
//Initial revision.
//
//   Rev 1.2   Oct 26 2005 16:33:56   mguney
//IServiceFactory declaration added.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 20 2005 15:46:00   mguney
//IR associated.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 20 2005 15:44:32   mguney
//Initial revision.

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
	public class TimeBasedFareSupplierFactory : ITimeBasedFareSupplierFactory, IServiceFactory
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public TimeBasedFareSupplierFactory()
		{
			
		}

		#region ITimeBasedFareSupplierFactory Members

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
				supplier = new Gateway();
			}
			else if (mode == ModeType.Coach)
			{
				supplier = new TimeBasedCoachFareSupplier();
			}
			
			return supplier;
		}

		#endregion		

		#region IServiceFactory Members

		/// <summary>
		///  Returns the TimeBasedFareSupplierFactory
		/// </summary>
		/// <returns>The current TimeBasedFareSupplierFactory.</returns>
		public object Get()
		{
			return this;
		}

		#endregion
	}
}
