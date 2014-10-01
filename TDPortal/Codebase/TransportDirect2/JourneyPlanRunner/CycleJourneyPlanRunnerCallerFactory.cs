// *********************************************** 
// NAME             : CycleJourneyPlanRunnerCallerFactory.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 06 Apr 2011
// DESCRIPTION  	: Factory class for CycleJourneyPlanRunnerCaller class
// ************************************************
// 

using System;
using TDP.Common.EventLogging;
using TDP.Common.ServiceDiscovery;
using Logger = System.Diagnostics.Trace;

namespace TDP.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// Factory class for CycleJourneyPlanRunnerCaller class
    /// </summary>
    public class CycleJourneyPlanRunnerCallerFactory : IServiceFactory
    {
        #region Constructors
        
        /// <summary>
        /// Constructor 
        /// </summary>
        public CycleJourneyPlanRunnerCallerFactory()
        {
        }

        #endregion

        #region IServiceFactory Members

        /// <summary>
        ///  This enables CycleJourneyPlanRunnerCaller to 
        ///  be used with Service Discovery, and returns
        ///  a new instance of CycleJourneyPlanRunnerCaller
        /// </summary>
        /// <returns>A new instance of CycleJourneyPlanRunnerCaller</returns>
        public object Get()
        {
            
             return new CycleJourneyPlanRunnerCaller();
            
        }

        #endregion
    }
}
