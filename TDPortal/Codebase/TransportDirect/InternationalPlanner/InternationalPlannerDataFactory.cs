// *********************************************** 
// NAME			: InternationalPlannerDataFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 08/02/10
// DESCRIPTION	: Factory class for International Planner data (cached using private dictionaries rather than the 
//              : web cache)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalPlannerDataFactory.cs-arc  $
//
//   Rev 1.0   Feb 09 2010 09:52:28   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Factory class for International Planner data 
    /// </summary>
    public class InternationalPlannerDataFactory : IServiceFactory
	{
		private InternationalPlannerData current;

		#region Implementation of IServiceFactory

		/// <summary>
        /// Standard constructor. Initialises the InternationalPlannerData
		/// </summary>
        public InternationalPlannerDataFactory()
		{
            current = new InternationalPlannerData();
		}

		/// <summary>
        /// Returns the current InternationalPlannerData object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}

		#endregion
    }
}
