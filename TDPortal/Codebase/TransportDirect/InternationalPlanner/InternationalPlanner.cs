// *********************************************** 
// NAME			: InternationalPlanner.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which represents the main entry point in to the International planner.
//              : The class exposes a method which accepts a request object for planning an 
//              : international journey
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalPlanner.cs-arc  $
//
//   Rev 1.3   Feb 22 2010 10:20:46   rbroddle
//Updated to inherit MarshalByRefObject
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 09 2010 09:53:20   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 04 2010 10:26:04   mmodi
//Updates as part of development
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:34   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.Logging;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Class which represents the main entry point in to the International planner
    /// </summary>
    public class InternationalPlanner : MarshalByRefObject, IInternationalPlanner, IDisposable
    {
        #region Constructor
		
        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalPlanner()
		{			
		}

		#endregion

        #region IInternationalPlanner methods

        /// <summary>
        /// Method that returns an International Journey for the supplied request parameters
        /// </summary>
        public IInternationalPlannerResult InternationalJourneyPlan(IInternationalPlannerRequest internationalJourneyRequest)
        {
            // Log started
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format("InternationalPlanner - JourneyPlan call started. RequestID[{0}] SessionID[{1}]", 
                    (internationalJourneyRequest != null) ? internationalJourneyRequest.RequestID : string.Empty,
                    (internationalJourneyRequest != null) ? internationalJourneyRequest.SessionID : string.Empty)));
            }


            // Create the planner engine which does all the work
            InternationalPlannerEngine planner = new InternationalPlannerEngine();

            // Call the plan journeys method
            IInternationalPlannerResult internationalPlannerResult = planner.PlanInternationalJourney(internationalJourneyRequest);


            // Log completed
            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format("InternationalPlanner - JourneyPlan call completed. RequestID[{0}] SessionID[{1}]",
                    (internationalJourneyRequest != null) ? internationalJourneyRequest.RequestID : string.Empty,
                    (internationalJourneyRequest != null) ? internationalJourneyRequest.SessionID : string.Empty)));
            }

            return internationalPlannerResult;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~InternationalPlanner()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
        }

        #endregion
    }
}
