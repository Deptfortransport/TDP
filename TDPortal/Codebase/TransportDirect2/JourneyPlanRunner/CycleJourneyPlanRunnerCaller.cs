// *********************************************** 
// NAME             : CycleJourneyPlanRunnerCaller.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: Implementation of ICycleJourneyPlanRunnerCaller class provides 
//                    interface methods to invoke CycleJourneyPlanner
// ************************************************
// 

using System;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common.EventLogging;
using TDP.Common.ServiceDiscovery;
using TDP.UserPortal.JourneyControl;
using TDP.UserPortal.SessionManager;
using TDP.UserPortal.StateServer;
using Logger = System.Diagnostics.Trace;

namespace TDP.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// CycleJourneyPlanRunnerCaller provides interface methods to invoke CycleJourneyPlanner
    /// </summary>
    public class CycleJourneyPlanRunnerCaller : MarshalByRefObject, ICycleJourneyPlanRunnerCaller
    {
        #region Delegates

        private delegate void DelegateInvokeCyclePlannerManager(ITDPJourneyRequest journeyRequest, string sessionID, string lang);
        
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CycleJourneyPlanRunnerCaller()
        {
        }

        #endregion

        #region ICycleJourneyPlanRunnerCaller Members

        /// <summary>
        /// Invoke the Cycle Planner Manager 
        /// </summary>
        /// <param name="journeyRequest">The validated user's Journey Request</param>
        /// <param name="sessionInfo">Information on the user's session</param>
        /// <param name="lang">The language of the current UI culture</param>
        public void InvokeCyclePlannerManager(ITDPJourneyRequest journeyRequest, ITDPSession sessionInfo, string lang)
        {
            DelegateInvokeCyclePlannerManager theDelegate = new DelegateInvokeCyclePlannerManager(InvokeCyclePlanner);
            theDelegate.BeginInvoke(journeyRequest, sessionInfo.SessionID, lang, null, null);
        }

        
        #endregion

        #region Private Methods 

        /// <summary>
        /// Invoke the Cycle Planner Manager 
        /// </summary>
        /// <param name="journeyRequest">The validated user's Journey Request</param>
        /// <param name="sessionID">Information on the user's sessionID</param>
        /// <param name="lang">The language of the current UI culture</param>
        private void InvokeCyclePlanner(ITDPJourneyRequest journeyRequest, string sessionID, string lang)
        {
            try
            {
                //Call the Cycle Planner Manager
                CallCyclePlanner(journeyRequest, sessionID, lang);
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error,
                    string.Format("CycleJourneyPlanRunnerCaller - Exception Message: [{0}], Stack Trace: [{1}]", e.Message, e.StackTrace), e));
            }
        }

        #endregion

        #region Methods to call the Cycle Planner

        /// <summary>
        /// Call the Cycle Planner Manager
        /// </summary>
        /// <param name="journeyRequest">The journey request for submission to the CJP</param>
        /// <param name="sessionID">User's sessionID</param>
        /// <param name="lang">Language of the journey detail expected</param>
        public void CallCyclePlanner(ITDPJourneyRequest journeyRequest, string sessionID, string lang)
        {
            try
            {
                // Get a Cycle Planner Manager from the service discovery
                ICyclePlannerManager cyclePlannerManager = TDPServiceDiscovery.Current.Get<ICyclePlannerManager>(ServiceDiscoveryKey.CyclePlannerManager);

                #region Plan journeys

                // Indicate that we are NOT an SLA monitoring transaction
                bool referenceTransation = false;

                if (TDPTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                        string.Format("CycleJourneyPlanRunnerCaller - Calling CyclePlannerManager to plan journeys for SessionId[{0}] and JourneyRequestHash[{1}]",
                            sessionID,
                            journeyRequest.JourneyRequestHash)));
                }

                ITDPJourneyResult journeyResult = cyclePlannerManager.CallCyclePlanner(
                    journeyRequest,
                    sessionID,
                    referenceTransation,
                    lang);

                if (TDPTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                        string.Format("CycleJourneyPlanRunnerCaller - CyclePlannerManager has returned for SessionId[{0}] and JourneyRequestHash[{1}]",
                            sessionID,
                            journeyRequest.JourneyRequestHash)));
                }

                #endregion

                #region Save journey to session

                if (TDPTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                        string.Format("CycleJourneyPlanRunnerCaller - Saving journey result to the state server for SessionId[{0}], JourneyRequestHash[{1}], JourneyReferenceNumber[{2}]",
                            sessionID,
                            journeyRequest.JourneyRequestHash,
                            SqlHelper.FormatRef(journeyResult.JourneyReferenceNumber))));
                }

                // Get an instance of the StateServer and directly add the journey result.
                
                // IMPORTANT - Relying on only this code in the Solution creating/adding/updating the 
                // TDPResultManager object. Therefore there should never be an issue/race condition 
                // of the UI thread and JourneyPlanning thread overriding the result object in the StateServer.

                // TODO: Mitesh - Possible TDPJourneyResult race condition. If two JourneyPlanning threads detect 
                // there is no result manager object in session, both create a new one, and both then save, 
                // thus losing one journey set.
                using (TDPStateServer stateServer = new TDPStateServer())
                {
                    // Lock the ResultManager - prevents any other process altering the data
                    stateServer.Lock(sessionID, new string[] { SessionManagerKey.KeyResultManager.ID });

                    // TODO: Mitesh - What happens if we can't get a lock on the SessionManagerKey.KeyResultManager? 
                    // How do we handle the jouney result? This could happen is the UI is going through a session/page 
                    // cycle and is checking if the TDPResultManager contains journeys.

                    // Get the TDPResultManager
                    object objResultManager = stateServer.Read(sessionID, SessionManagerKey.KeyResultManager.ID);
                    
                    if (objResultManager == null)
                    {
                        // First time user has planned journey so ok if it didnt exist, create a new instance
                        objResultManager = new TDPResultManager();
                    }

                    TDPResultManager resultManager = (TDPResultManager)objResultManager;

                    // Insert the journey result 
                    resultManager.AddTDPJourneyResult(journeyResult);
                                       
                    // And save, this will release the lock
                    stateServer.Save(sessionID, SessionManagerKey.KeyResultManager.ID, resultManager);
                
                } // StateServer will be disposed, any outstanding locks will be gracefully released during this process
                  // but ensure above code does this to avoid any delays

                if (TDPTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                        string.Format("CycleJourneyPlanRunnerCaller - Finished saving journey result to the state server for SessionId[{0}], JourneyRequestHash[{1}], JourneyReferenceNumber[{2}]",
                            sessionID,
                            journeyRequest.JourneyRequestHash,
                            SqlHelper.FormatRef(journeyResult.JourneyReferenceNumber))));
                }

                #endregion

            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(
                    TDPEventCategory.Business, TDPTraceLevel.Error,
                    string.Format("CycleJourneyPlanRunnerCaller - Exception was thrown planning the journey and/or saving to sesion for SessionId[{0}] and JourneyRequestHash[{1}]. Error Message[{2}], see exception for further details.",
                        sessionID,
                        journeyRequest.JourneyRequestHash,
                        ex.Message), 
                    ex));
            }
        }
        
        #endregion
    }
}
