// *********************************************** 
// NAME			: CarCostCalculatorFactory.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 15/12/2004
// DESCRIPTION	: Implementation of IServiceFactory for the CarCostCalculator
// so that it can be used with TDServiceDiscovery
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CarCostCalculatorFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:38   mturner
//Initial revision.
//
//   Rev 1.0   Jan 04 2005 18:18:56   rhopkins
//Initial revision.
//

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Implementation of IServiceFactory for the CarCostCalculator
	/// </summary>
	public class CarCostCalculatorFactory : IServiceFactory
	{
		private CarCostCalculator current;

		#region Implementation of IServiceFactory

		/// <summary>
		/// Standard constructor. Initialises the CarCostCalculator.
		/// </summary>
		public CarCostCalculatorFactory()
		{
			current = new CarCostCalculator();
		}

		/// <summary>
		/// Returns the current CarCostCalculator object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}

		#endregion

	}
}
