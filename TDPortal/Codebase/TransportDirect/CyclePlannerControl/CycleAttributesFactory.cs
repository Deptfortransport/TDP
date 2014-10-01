// *********************************************** 
// NAME			: CycleAttributesFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/09/2008
// DESCRIPTION	: Factory class for the CycleAttributes
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CycleAttributesFactory.cs-arc  $
//
//   Rev 1.0   Oct 10 2008 15:31:34   mmodi
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
    /// CycleAttributesFactory class
    /// </summary>
    public class CycleAttributesFactory : IServiceFactory
    {
        private CycleAttributes current;

		#region Implementation of IServiceFactory
		/// <summary>
        /// Standard constructor. Initialises the CycleAttributes.
		/// </summary>
        public CycleAttributesFactory()
		{
            current = new CycleAttributes();
		}

		/// <summary>
        /// Returns the current CycleAttributes object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}

		#endregion
    }
}
