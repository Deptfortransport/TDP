// ****************************************************************
// NAME         : CycleJourneyPlanRunnerCallerFactory.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 10 Jun 2008
// DESCRIPTION  : Factory class for the Cycle plan caller
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/CycleJourneyPlanRunnerCallerFactory.cs-arc  $
//
//   Rev 1.0   Jun 20 2008 15:39:00   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Text;

using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    public class CycleJourneyPlanRunnerCallerFactory : IServiceFactory
    {
        #region Constructor

        /// <summary>
		/// Constructor - nothing to do.
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
        /// <returns>A new instance of JourneyPlanRunnerCaller</returns>
        public object Get()
        {
            try
            {
                return new CycleJourneyPlanRunnerCaller();
            }
            catch (RemotingException e)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e));
            }
            catch (Exception e)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e));
            }
            return null;
        }

        #endregion
    }
}
