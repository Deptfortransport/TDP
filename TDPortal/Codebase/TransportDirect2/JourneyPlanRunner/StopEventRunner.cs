// *********************************************** 
// NAME             : StopEventRunner.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 26 Apr 2011
// DESCRIPTION  	: Validates user input and initiate stop event request
// ************************************************
// 

using System.Diagnostics;
using TDP.Common;
using TDP.Common.EventLogging;
using TDP.UserPortal.JourneyControl;

namespace TDP.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// Validates user input and initiate stop event request
    /// </summary>
    public class StopEventRunner: JourneyPlanRunnerBase
    {
        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        public StopEventRunner()
            : base()
        {
        }

        #endregion
        
        #region Public Methods

        /// <summary>
        /// ValidateAndRun
        /// </summary>
        /// <param name="journeyRequest">ITDPJourneyRequest</param>
        public override bool ValidateAndRun(ITDPJourneyRequest journeyRequest, string language, bool submitRequest)
        {
            if (journeyRequest == null)
            {
                Trace.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, "No journey request provided"));
                throw new TDPException("JourneyRequest object is null", true, TDPExceptionIdentifier.JPRInvalidTDPJourneyRequest);
            }

            #region Validations

            //
            // Perform date validations
            //
            PerformDateValidations(journeyRequest);

            #endregion

            if (listErrors.Count == 0)
            {
                if (submitRequest)
                {
                    // All input journey parameters were correctly formed so invoke the Stop Event Manager
                    InvokeStopEventManager(journeyRequest, language);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        
        #endregion
    }
}
