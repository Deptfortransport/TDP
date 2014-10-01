// *********************************************** 
// NAME			: InternationalPlannerManagerFactory.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 02 Feb 2010
// DESCRIPTION	: Factory that allows ServiceDiscovery to create instances of the International planner manager
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlannerControl/InternationalPlannerManagerFactory.cs-arc  $
//
//   Rev 1.1   Feb 09 2010 10:11:06   apatel
//TD International Planner update
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 09:33:52   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.InternationalPlannerControl
{
    public class InternationalPlannerManagerFactory: IServiceFactory
    {
        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalPlannerManagerFactory()
		{
        }

        #endregion

        #region IServiceFactory Members

        /// <summary>
		///  Method used by the ServiceDiscovery to get an
        ///  instance of the InternationalPlannerManager class. A new object
		///  is instantiated each time because a single shared
		///  instance would not be thread-safe.
		/// </summary>
        /// <returns>A new instance of a InternationalPlannerManager</returns>
		public Object Get()
		{
			return new InternationalPlannerManager();
		}

        #endregion
    }
}
