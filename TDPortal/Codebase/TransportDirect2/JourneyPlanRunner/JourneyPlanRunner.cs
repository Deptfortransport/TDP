// *********************************************** 
// NAME             : JourneyPlanRunner.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 29 Mar 2011
// DESCRIPTION  	: Validates user input and initiate journey request
// ************************************************

using System.Diagnostics;
using TDP.Common;
using TDP.Common.EventLogging;
using TDP.UserPortal.JourneyControl;

namespace TDP.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// Validates user input and initiate journey request
    /// </summary>
    public class JourneyPlanRunner : JourneyPlanRunnerBase
    {
        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyPlanRunner()
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
            // Perform location validations
            //
            PerformLocationValidations(journeyRequest);

            //
            // Perform date validations
            //
            PerformDateValidations(journeyRequest);

            #endregion

            if (listErrors.Count == 0)
            {
                if (submitRequest)
                {
                    // All input journey parameters were correctly formed so invoke the CJP Manager
                    InvokeCJPManager(journeyRequest, language);
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
