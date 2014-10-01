// *********************************************** 
// NAME             : JourneyPlannerHelper.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Feb 2012
// DESCRIPTION  	: JourneyPlanner helper class to submit a journey request to be planned
// ************************************************
// 

using TDP.Common.EventLogging;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.JourneyPlanRunner;
using Logger = System.Diagnostics.Trace;

namespace TDP.Common.Web
{
    /// <summary>
    /// JourneyPlanner helper class to submit a journey request to be planned
    /// </summary>
    public class JourneyPlannerHelper
    {
        #region Private members

        private SessionHelper sessionHelper;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyPlannerHelper()
        {
            sessionHelper = new SessionHelper();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Submit a journey request. 
        /// Retrieves the TDPJourneyRequest from session to submit using an IJourneyPlanRunner class.
        /// Any validation or error messages are added to the session
        /// </summary>
        /// <param name="submitRequest">submitRequest flag indicates if the request should 
        /// be validated only or validated and submitted</param>
        /// <returns></returns>
        public bool SubmitRequest(TDPJourneyPlannerMode plannerMode, bool submitRequest)
        {
            bool valid = false;

            try
            {
                IJourneyPlanRunner runner = null;

                if (plannerMode == TDPJourneyPlannerMode.PublicTransport ||
                    plannerMode == TDPJourneyPlannerMode.BlueBadge ||
                    plannerMode == TDPJourneyPlannerMode.ParkAndRide ||
                    plannerMode == TDPJourneyPlannerMode.RiverServices)
                {
                    #region Public transport and Car journey planning

                    runner = new JourneyPlanRunner();

                    #endregion
                }
                else if (plannerMode == TDPJourneyPlannerMode.Cycle)
                {
                    #region Cycle journey planning

                    runner = new CycleJourneyPlanRunner();

                    #endregion
                }
                else
                {
                    // Should never reach here as the above if statements cover all planner modes
                    throw new TDPException("Unexpected planner mode was detected, unable to initiate journey planning", false, TDPExceptionIdentifier.SWUndefinedPlannerMode);
                }

                #region Call Validate and Run

                // Get the journey request to submit
                ITDPJourneyRequest tdpJourneyRequest = sessionHelper.GetTDPJourneyRequest();

                // If the journey result already exists for this request, then save
                // performance by not submitting to journey planners.
                // This is safe because the JourneyRequestHash can be used to check if a request
                // contains identical "user entered parameters".
                bool resultExists = sessionHelper.DoesTDPJourneyResultExist(tdpJourneyRequest.JourneyRequestHash, true);

                if (resultExists)
                {
                    // Journey has already been planned and the result is in the session result manager.
                    valid = true;
                }
                else if (runner != null)
                {
                    // Initiate the journey planning
                    valid = runner.ValidateAndRun(tdpJourneyRequest, CurrentLanguage.Culture, submitRequest);

                    // If failed to initiate journey planning, check for any validation error messages,
                    // add to session for page to display
                    if (!valid)
                    {
                        sessionHelper.AddMessages(runner.Messages);
                    }
                }

                if (valid && submitRequest)
                {
                    // Successfully submitted the request, update request and save. This is done to support
                    // potential repeated identical landing page requests
                    tdpJourneyRequest.JourneyRequestSubmitted = true;
                    sessionHelper.UpdateSession(tdpJourneyRequest);
                }

                #endregion
            }
            catch (TDPException tdpEx)
            {
                if (!tdpEx.Logged)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, tdpEx.Message));
                }

                // Add message to be displayed
                sessionHelper.AddMessage(new TDPMessage("ValidateAndRun.ErrorMessage.General1", TDPMessageType.Error));
                sessionHelper.AddMessage(new TDPMessage("ValidateAndRun.ErrorMessage.General2", TDPMessageType.Error));

                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Submit a stop event request.
        /// Retrieves the StopEvent TDPJourneyRequest from session to submit using an IJourneyPlanRunner class.
        /// Any validation or error messages are added to the session
        /// </summary>
        /// <param name="submitRequest">submitRequest flag indicates if the request should 
        /// be validated only or validated and submitted</param>
        /// <returns></returns>
        public bool SubmitStopEventRequest(bool submitRequest)
        {
            bool valid = false;

            try
            {
                IJourneyPlanRunner runner = new StopEventRunner();

                #region Call Validate and Run

                // Get the stop event request to submit
                ITDPJourneyRequest tdpStopEventRequest = sessionHelper.GetTDPStopEventRequest();

                // If the result already exists for this stop event request, then save
                // performance by not submitting to journey planners.
                // This is safe because the JourneyRequestHash can be used to check if a request
                // contains identical "user entered parameters"
                bool resultExists = sessionHelper.DoesTDPStopEventResultExist(tdpStopEventRequest.JourneyRequestHash, true);

                if (resultExists)
                {
                    // Journey has already been planned and the result is in the session result manager.
                    valid = true;
                }
                else
                {
                    // Initiate the journey planning
                    valid = runner.ValidateAndRun(tdpStopEventRequest, CurrentLanguage.Culture, submitRequest);

                    // If failed to initiate journey planning, check for any validation error messages,
                    // add to session for page to display
                    if (!valid)
                    {
                        sessionHelper.AddMessages(runner.Messages);
                    }
                }

                if (valid && submitRequest)
                {
                    // Successfully submitted the request, update request and save. This is done to support
                    // potential repeated identical landing page requests
                    tdpStopEventRequest.JourneyRequestSubmitted = true;
                    sessionHelper.UpdateSessionStopEvent(tdpStopEventRequest);
                }

                #endregion
            }
            catch (TDPException tdpEx)
            {
                if (!tdpEx.Logged)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Infrastructure, TDPTraceLevel.Error, tdpEx.Message));
                }

                // Add message to be displayed
                sessionHelper.AddMessage(new TDPMessage("ValidateAndRun.ErrorMessage.General1", TDPMessageType.Error));
                sessionHelper.AddMessage(new TDPMessage("ValidateAndRun.ErrorMessage.General2", TDPMessageType.Error));

                valid = false;
            }

            return valid;
        }
        #endregion
    }
}
