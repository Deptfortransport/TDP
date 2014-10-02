// *********************************************** 
// NAME			: CyclePlannerHelper.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 18 Aug 2008
// DESCRIPTION	: Helper class for methods used by cycle planner
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/CyclePlannerHelper.cs-arc  $
//
//   Rev 1.2   Jan 19 2009 11:19:10   mmodi
//Updated to log a Gradient profile event
//Resolution for 5224: Cycle Planner - Gradient profile view reporting events
//
//   Rev 1.1   Sep 17 2008 12:03:30   mturner
//Updated to allow Gradient Profiler errors to be handled by the calling AJAX.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Aug 22 2008 10:48:58   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
    /// Helper class to expose common methods used in cycle journey planning
    /// </summary>
    public class CyclePlannerHelper
    {
        #region Private members

        private ITDSessionManager sessionManager = null;
        private bool logGradientProfileEvent = true;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CyclePlannerHelper(ITDSessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessionManager"></param>
        /// <param name="logGradientProfileEvent">Set to false to not log an event, should only be for Printable pages 
        /// which might display a gradient profile chart chart</param>
        public CyclePlannerHelper(ITDSessionManager sessionManager, bool logGradientProfileEvent)
        {
            this.sessionManager = sessionManager;
            this.logGradientProfileEvent = logGradientProfileEvent;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Helper method which calls the AJAX methods to generate a TDChartData object which
        /// contains the data required for a Gradient Profile graph to be displayed by the browser.
        /// Uses the current outward or return cycle journey in the session, and calls the 
        /// static method in CycleJourneyGraphControl which does the work.
        /// </summary>
        /// <param name="outward">outward journey flag</param>
        /// <returns>TDChartData object containing information needed by the browser</returns>
        public CycleJourneyGraphControl.TDChartData GetChartData(bool outward)
        {
            try
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info,
                        "GradientProfiler - AJAX call started, WebMethod: GetChartData(). SessionId = " + sessionManager.Session.SessionID));

                #region Get CycleJourney from session
                CycleJourney cycleJourney = null;

                if (outward)
                {
                    cycleJourney = sessionManager.CycleResult.OutwardCycleJourney();
                }
                else
                {
                    cycleJourney = sessionManager.CycleResult.ReturnCycleJourney();
                }
                #endregion

                #region Get the Session info for request

                CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();

                #endregion

                #region Call Gradient Profiler

                cycleJourney = CycleJourneyGraphControl.CallGradientProfiler(cycleJourney, sessionInfo);

                // Save the journey back to the session
                if (outward)
                {
                    sessionManager.CycleResult.AddOutwardCycleJourney(cycleJourney);
                }
                else
                {
                    sessionManager.CycleResult.AddReturnCycleJourney(cycleJourney);
                }

                #endregion

                #region Get Chart Data object from the cycle journey

                DateTime gradientProfileStarted = DateTime.Now;

                CycleJourneyGraphControl.TDChartData chartData = CycleJourneyGraphControl.GetChartData(cycleJourney, sessionInfo);

                #endregion

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info,
                        "GradientProfiler - AJAX call finished, WebMethod: GetChartData(). Returning chart data to client. SessionId = " + sessionManager.Session.SessionID));

                // Log an event
                if (this.logGradientProfileEvent)
                {
                    // The AJAX call is only ever made for a chart, so the category is chart
                    CyclePlannerHelper.LogGradientProfileEvent(GradientProfileEventDisplayCategory.Chart,
                                                               gradientProfileStarted,
                                                               sessionManager.Authenticated,
                                                               sessionManager.Session.SessionID);
                }

                return chartData;
            }
            catch (Exception ex)
            {
                // Log the exception 
                string message = "GradientProfiler - AJAX call to WebMethod: GetChartData(), threw an exception attempting to get GradientProfile data."
                    + " Exception: " + ex.Message
                    + " Stacktrace: " + ex.StackTrace;

                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message);
                Logger.Write(oe);

                CycleJourneyGraphControl.TDChartData chartData = new CycleJourneyGraphControl.TDChartData();
                chartData.ErrorOccured = true;
                return chartData;
            }
        }

        /// <summary>
        /// Logs a Gradient Profile Event
        /// </summary>
        /// <param name="category"></param>
        /// <param name="callSubmitted"></param>
        /// <param name="isLoggedOn"></param>
        /// <param name="sessionId"></param>
        public static void LogGradientProfileEvent(GradientProfileEventDisplayCategory category, DateTime callSubmitted, bool isLoggedOn, string sessionId)
        {
            if (CustomEventSwitch.On("GradientProfileEvent"))
            {
                GradientProfileEvent gpe = new GradientProfileEvent(
                    category,
                    callSubmitted,
                    isLoggedOn,
                    sessionId);

                Logger.Write(gpe);
            }
        }

        #endregion
    }
}
