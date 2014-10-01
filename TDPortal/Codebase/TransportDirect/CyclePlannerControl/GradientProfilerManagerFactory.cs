// *********************************************** 
// NAME			: GradientProfilerManagerFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Factory that allows ServiceDiscovery to create instances of the GradientProfilerManager
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/GradientProfilerManagerFactory.cs-arc  $
//
//   Rev 1.0   Jul 18 2008 13:41:04   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Factory used by Service Discovery to create a GradientProfilerManager Stub.
    /// </summary>
    public class GradientProfilerManagerFactory : IServiceFactory
    {
        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
        public GradientProfilerManagerFactory()
		{
        }

        #endregion

        #region IServiceFactory Members

        /// <summary>
		///  Method used by the ServiceDiscovery to get an
        ///  instance of the GradientProfilerManager class. A new object
		///  is instantiated each time because a single shared
		///  instance would not be thread-safe.
		/// </summary>
        /// <returns>A new instance of a GradientProfilerManager</returns>
		public Object Get()
		{
            return new GradientProfilerManager();
		}

        #endregion

    }
}
