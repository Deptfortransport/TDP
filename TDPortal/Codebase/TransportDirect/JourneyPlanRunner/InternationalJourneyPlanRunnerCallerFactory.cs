// ****************************************************************
// NAME         : InternationalJourneyPlanRunnerCallerFactory.cs
// AUTHOR       : Amit Patel
// DATE CREATED : 02 Feb 2010
// DESCRIPTION  : Factory class for the International journey plan caller
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/InternationalJourneyPlanRunnerCallerFactory.cs-arc  $
//
//   Rev 1.1   Feb 09 2010 10:45:42   apatel
//Make the classes public
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 09:37:06   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Diagnostics;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// International journey plan runner factory class
    /// </summary>
    public class InternationalJourneyPlanRunnerCallerFactory: IServiceFactory
    {
        #region Constructor

        /// <summary>
		/// Constructor - nothing to do.
		/// </summary>
        public InternationalJourneyPlanRunnerCallerFactory()
		{
		}

        #endregion

        #region IServiceFactory Members

        /// <summary>
        ///  This enables InternationalJourneyPlanRunnerCaller to 
        ///  be used with Service Discovery, and returns
        ///  a new instance of InternationalJourneyPlanRunnerCaller
        /// </summary>
        /// <returns>A new instance of JourneyPlanRunnerCaller</returns>
        public object Get()
        {
            try
            {
                return new InternationalJourneyPlanRunnerCaller();
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
