// *********************************************** 
// NAME             : JourneyPlanRunnerCallerFactory.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 29 Mar 2011
// DESCRIPTION  	: Factory class for JourneyPlanRunner class
// ************************************************
                
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.Common.ServiceDiscovery;
using System.Diagnostics;
using TDP.Common.EventLogging;

namespace TDP.UserPortal.JourneyPlanRunner
{
    // <summary>
    /// Factory class for JourneyPlanRunner class
    /// </summary>
    public class JourneyPlanRunnerCallerFactory : IServiceFactory
    {
        #region Constructors
        /// <summary>
        /// Constructor 
        /// </summary>
        public JourneyPlanRunnerCallerFactory()
        {
        }
        #endregion

        #region IServiceFactory Members


        /// <summary>
        ///  This enables JourneyPlanRunnerCaller to 
        ///  be used with Service Discovery, and returns
        ///  a new instance of JourneyPlanRunnerCaller
        /// </summary>
        /// <returns>A new instance of JourneyPlanRunnerCaller</returns>
        public object Get()
        {
            
            return new JourneyPlanRunnerCaller();
            
        }

        #endregion
    }
}
