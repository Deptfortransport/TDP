// ***********************************************
// NAME			: JourneyFareFilterFactory.cs
// AUTHOR		: Murat Guney
// DATE CREATED	: 25/10/2005
// DESCRIPTION	: JourneyFareFilterFactory implementation..
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/JourneyFareFilterFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:50   mturner
//Initial revision.
//
//   Rev 1.1   Oct 26 2005 16:43:46   mguney
//Associated IR.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 26 2005 16:42:34   mguney
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.CoachFares;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// JourneyFareFilterFactory.
	/// </summary>
	public class JourneyFareFilterFactory : IJourneyFareFilterFactory, IServiceFactory
	{
		//singleton instance
		private CoachJourneyFareFilter current;

		#region IJourneyFareFilterFactory Members

		/// <summary>
		/// Provides the current CoachJourneyFareFilter.
		/// </summary>
		/// <returns></returns>
		public IJourneyFareFilter GetFilter()
		{
			if (current == null)
				current = new CoachJourneyFareFilter();
			return current;
		}

		#endregion

		#region IServiceFactory Members

		/// <summary>
		/// Gets the current JourneyFareFilterFactory. 
		/// </summary>
		/// <returns></returns>
		public object Get()
		{			
			return this;
		}

		#endregion
	}
}
