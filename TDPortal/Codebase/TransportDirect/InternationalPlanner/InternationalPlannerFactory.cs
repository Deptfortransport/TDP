// *********************************************** 
// NAME			: InternationalPlannerFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which provides a single Get method that creates a new instance of 
//                the InternationalPlanner class for each call
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalPlannerFactory.cs-arc  $
//
//   Rev 1.0   Jan 29 2010 12:04:36   mmodi
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
    /// Factory class for the InternationalPlanner
    /// </summary>
    public class InternationalPlannerFactory : IServiceFactory
    {
        #region Constructor
		
        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalPlannerFactory()
		{			
		}

		#endregion

        #region IServiceFactory Members

        /// <summary>
        /// Method used by the ServiceDiscovery to get the instance of the InternationalPlanner.
        /// </summary>
        /// <returns>The a new instance of the InternationalPlanner.</returns>
        public Object Get()
        {
            return new InternationalPlanner();
        }

        #endregion
    }
}
