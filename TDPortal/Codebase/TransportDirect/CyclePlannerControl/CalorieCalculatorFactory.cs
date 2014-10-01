// *********************************************** 
// NAME			: CalorieCalculatorFactory.cs
// AUTHOR		: Richard Broddle
// DATE CREATED	: 22/08/2012
// DESCRIPTION	: Implementation of IServiceFactory for the CalorieCalculator
// so that it can be used with TDServiceDiscovery
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CalorieCalculatorFactory.cs-arc  $
//
//   Rev 1.0   Aug 24 2012 16:00:26   RBroddle
//Initial revision.
//Resolution for 5828: CCN - RFC ATO666 CYCLE CALORIE COUNTER

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public class CalorieCalculatorFactory : IServiceFactory
    {
		private CalorieCalculator current;

		#region Implementation of IServiceFactory

		/// <summary>
		/// Standard constructor. Initialises the CalorieCalculator.
		/// </summary>
		public CalorieCalculatorFactory()
		{
			current = new CalorieCalculator();
		}

		/// <summary>
		/// Returns the current CalorieCalculator object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}

		#endregion

    }
}
