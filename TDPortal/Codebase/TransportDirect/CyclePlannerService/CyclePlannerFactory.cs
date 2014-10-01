// ***********************************************
// NAME 		: CyclePlannerFactory.cs
// AUTHOR 		: Mitesh Modi
// DATE CREATED : 10/06/2008
// DESCRIPTION 	: This class provides a single Get method that creates a 
//				  new instance of the CyclePlanner class for each call
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerService/CyclePlannerFactory.cs-arc  $
//
//   Rev 1.0   Jun 20 2008 16:20:42   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CyclePlannerService
{
    /// <summary>
    /// This class provides a single Get method that creates a new instance 
    /// of the CyclePlanner class for each call
    /// </summary>
    public class CyclePlannerFactory : IServiceFactory
    {
        #region constructor
		/// <summary>
		/// Constructor.
		/// </summary>
        public CyclePlannerFactory()
		{			
		}
		#endregion

        #region IServiceFactory Members
        /// <summary>
        /// Method used by the ServiceDiscovery to get the instance of the CyclePlanner.
        /// </summary>
        /// <returns>The current instance of the Journeyplanner.</returns>
        public Object Get()
        {
            return new CyclePlanner();
        }
        #endregion

    }
}
