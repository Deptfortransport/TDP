// *********************************************** 
// NAME			: CyclePlannerManagerFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Factory that allows ServiceDiscovery to create instances of the Cycle planner manager
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CyclePlannerManagerFactory.cs-arc  $
//
//   Rev 1.0   Jun 20 2008 15:42:00   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    /// <summary>
    /// Factory used by Service Discovery to create a Cycle planner manager Stub.
    /// </summary>
    public class CyclePlannerManagerFactory : IServiceFactory
    {
        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
        public CyclePlannerManagerFactory()
		{
        }

        #endregion

        #region IServiceFactory Members

        /// <summary>
		///  Method used by the ServiceDiscovery to get an
        ///  instance of the CyclePlannerManager class. A new object
		///  is instantiated each time because a single shared
		///  instance would not be thread-safe.
		/// </summary>
        /// <returns>A new instance of a CyclePlannerManager</returns>
		public Object Get()
		{
			return new CyclePlannerManager();
		}

        #endregion
    }
}
