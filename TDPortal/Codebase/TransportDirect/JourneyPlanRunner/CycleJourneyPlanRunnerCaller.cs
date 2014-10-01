// ****************************************************************
// NAME         : CycleJourneyPlanRunnerCaller.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 10 Jun 2008
// DESCRIPTION  : A remotable class to implement the ICycleJourneyPlanRunnerCaller class.
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/CycleJourneyPlanRunnerCaller.cs-arc  $
//
//   Rev 1.7   Sep 06 2012 13:28:46   mmodi
//Updated to only show one cycle map
//Resolution for 5834: CCN0666 Cycle default to maps view
//
//   Rev 1.6   Aug 29 2012 08:30:48   mmodi
//Updated view state to display cycle maps by default on cycle details page
//Resolution for 5834: CCN0666 Cycle default to maps view
//
//   Rev 1.5   Sep 25 2009 11:34:12   apatel
//Updated for Cycle journey 'arrive by' journeys - USD UK5688851
//Resolution for 5328: CTP - Arrive by journey results show depart after time in summary
//
//   Rev 1.4   Oct 27 2008 15:58:18   mmodi
//Removed comments
//
//   Rev 1.3   Sep 08 2008 15:48:38   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Aug 22 2008 10:12:26   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Aug 06 2008 14:50:26   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:38:58   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    public class CycleJourneyPlanRunnerCaller : MarshalByRefObject, ICycleJourneyPlanRunnerCaller
    {
        #region Delegates

        private delegate void DelegateInvokeCyclePlannerManagerForNew
            (CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang, Guid cyclePlannerRequestId, 
            TDJourneyParameters tdJourneyParameters, TDJourneyParametersCycleConverter converter,
            TDDateTime outwardDateTime, TDDateTime returnDateTime, string polylinesTransformXslt);
        
        private delegate void DelegateInvokeCyclePlannerManagerForAmend(CJPSessionInfo sessionInfo, 
            TDSessionPartition currentPartition, Guid cyclePlannerRequestId, int referenceNumber,
            int lastSequenceNumber, ITDCyclePlannerRequest tdJourneyRequest, string polylinesTransformXslt);

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CycleJourneyPlanRunnerCaller()
        {
        }

        #endregion

        #region ICycleJourneyPlanRunnerCaller Members

        public void InvokeCyclePlannerManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang, Guid cyclePlannerRequestId, TDJourneyParameters tdJourneyParameters, TDJourneyParametersCycleConverter converter, TDDateTime outwardDateTime, TDDateTime returnDateTime, string polylinesTransformXslt)
        {
            DelegateInvokeCyclePlannerManagerForNew theDelegate = new DelegateInvokeCyclePlannerManagerForNew(InvokeCyclePlannerManagerForNew);
            theDelegate.BeginInvoke(sessionInfo, currentPartition, lang, cyclePlannerRequestId, tdJourneyParameters, converter, outwardDateTime, returnDateTime, polylinesTransformXslt, null, null);
        }

        public void InvokeCyclePlannerManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, Guid cyclePlannerRequestId, int referenceNumber, int lastSequenceNumber, ITDCyclePlannerRequest tdCyclePlannerRequest, string polylinesTransformXslt)
        {
            DelegateInvokeCyclePlannerManagerForAmend theDelegate = new DelegateInvokeCyclePlannerManagerForAmend(InvokeCyclePlannerManagerForAmend);
            theDelegate.BeginInvoke(sessionInfo, currentPartition, cyclePlannerRequestId, referenceNumber, lastSequenceNumber, tdCyclePlannerRequest, polylinesTransformXslt, null, null);
        }

        #endregion

        #region Private methods for doing the preparation and Cycle Planner call

        /// <summary>
        /// Invoke the CyclePlannerManager 
        /// </summary>
        /// <param name="sessionInfo">Information on the user's session</param>
        /// <param name="currentPartition">Indicates if cost based or time based search</param>
        /// <param name="lang">The language of the current UI culture</param>
        /// <param name="cyclePlannerRequestId">The request ID of the call to the CyclePlanner</param>
        /// <param name="tdJourneyParameters">The validated user's Journey Parameters</param>
        /// <param name="converter">Converter class to use</param>
        /// <param name="outwardDateTime">The Date/Time of the outward journey</param>
        /// <param name="returnDateTime">The Date/Time of the return journey (may be null for one-way journeys)</param>
        private void InvokeCyclePlannerManagerForNew(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang, Guid cyclePlannerRequestId, TDJourneyParameters tdJourneyParameters, TDJourneyParametersCycleConverter converter, TDDateTime outwardDateTime, TDDateTime returnDateTime, string polylinesTransformXslt)
        {
            try
            {
                //Creates a Cycle Request using the Convert method
                ITDCyclePlannerRequest request = converter.Convert(tdJourneyParameters, outwardDateTime, returnDateTime);

                //Creates an instance of TDSessionSerializer to access objects in deferred storage
                TDSessionSerializer deferedStorage = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);

                //And now save the request back to the session.
                deferedStorage.SerializeSessionObjectAndSave(sessionInfo.SessionId, currentPartition, TDSessionManager.KeyCycleRequest, request);

                //Call the CyclePlannerManager
                CallCyclePlanner(
                    cyclePlannerRequestId,
                    request,
                    sessionInfo.SessionId,
                    currentPartition,
                    sessionInfo.IsLoggedOn,
                    sessionInfo.UserType,
                    lang,
                    sessionInfo.OriginAppDomainFriendlyName,
                    polylinesTransformXslt);
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("Message: [{0}], Stack Trace: [{1}]", e.Message, e.StackTrace), e));
            }
        }


        /// <summary>
        /// Invoke the CyclePlannerManager for Amendments
        /// </summary>
        /// <param name="sessionInfo">Information on the user's session</param>
        /// <param name="cyclePlannerRequestId">The request ID of the call to the Cycle Planner</param>
        /// <param name="referenceNumber">The reference number for the amendments of the journey parameters</param>
        /// <param name="lastSequenceNumber">The sequence number of the latest ammendment</param>
        /// <param name="tdCyclePlannerRequest">The journey request for submission to the CJP</param>
        private void InvokeCyclePlannerManagerForAmend(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, Guid cyclePlannerRequestId, int referenceNumber, int lastSequenceNumber, ITDCyclePlannerRequest tdCyclePlannerRequest, string polylinesTransformXslt)
        {
            try
            {
                CallCyclePlannerForAmendments(referenceNumber, lastSequenceNumber, cyclePlannerRequestId, tdCyclePlannerRequest, sessionInfo.SessionId,
                    currentPartition, sessionInfo.IsLoggedOn, sessionInfo.UserType, sessionInfo.Language, sessionInfo.OriginAppDomainFriendlyName, polylinesTransformXslt);
            }

            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("Message: [{0}], Stack Trace: [{1}]", e.Message, e.StackTrace), e));
            }


        }

        #endregion

        #region Methods to call the CyclePlanner

        /// <summary>
        /// Call the CyclePlannerManager
        /// </summary>
        /// <param name="cyclePlannerRequestID">The ID of the call to the CyclePlanner</param>
        /// <param name="tdCyclePlannerRequest">The request for submission to the CyclePlanner</param>
        /// <param name="sessionID"></param>
        private void CallCyclePlanner(Guid cyclePlannerRequestID, ITDCyclePlannerRequest tdCyclePlannerRequest, string sessionID, TDSessionPartition part, bool userIsLoggedOn, int userType, string lang, string clientAppDomainFriendlyName, string polylinesTransformXslt)
        {
            try
            {
                // Get a CyclePlannerManager from the service discovery
                ICyclePlannerManager cyclePlannerManager = (ICyclePlannerManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CyclePlannerManager];

                // Indicate that we are NOT an SLA monitoring transaction
                bool referenceTransation = false;


                ITDCyclePlannerResult tdCyclePlannerResult = cyclePlannerManager.CallCyclePlanner(
                    tdCyclePlannerRequest,
                    sessionID,
                    userType,
                    referenceTransation,
                    userIsLoggedOn,
                    lang,
                    polylinesTransformXslt);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "CyclePlanner - CycleJourneyPlanRunnerCaller. CyclePlannerManager has returned - sessionId = " + sessionID));
                }

                TDSessionSerializer deferManager = new TDSessionSerializer(clientAppDomainFriendlyName);

                // Refresh after call
                AsyncCallState stateData = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyAsyncCallState);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "Retrieved AsyncCallState - sessionId = " + sessionID));
                }

                if (cyclePlannerRequestID == stateData.RequestID)
                {
                    TDCyclePlannerRequest request = (TDCyclePlannerRequest)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyCycleRequest);

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "TDJourneyViewState instantiated - sessionId = " + sessionID));
                    }

                    TDJourneyViewState viewState = new TDJourneyViewState();
                                        
                    // Check if any return journey times overlap with outward journey times
                    if (tdCyclePlannerResult.IsValid)
                    {
                        if (tdCyclePlannerResult.CheckForReturnOverlap(request))
                        {
                            tdCyclePlannerResult.AddMessageToArray(string.Empty, "JourneyPlannerOutput.JourneyOverlap", 0, 0, TransportDirect.UserPortal.CyclePlannerControl.ErrorsType.Warning);
                        }

                        //check if any journeys returned start in the past 
                        if (tdCyclePlannerResult.CheckForJourneyStartInPast(request))
                        {
                            tdCyclePlannerResult.AddMessageToArray(string.Empty, "JourneyPlannerOutput.JourneyTimeInPast", 0, 0, TransportDirect.UserPortal.CyclePlannerControl.ErrorsType.Warning);
                        }

                        // Save the Cycle journey type to the view state
                        if (tdCyclePlannerResult.OutwardCycleJourneyCount > 0)
                        {
                            viewState.SelectedOutwardJourneyType = TDJourneyType.Cycle;
                            viewState.CycleJourneyLeavingTimeSearchType = request.OutwardArriveBefore;

                            // Ensure when Cycle journey details page is displayed, the maps are shown by default
                            viewState.OutwardShowMap = true;
                        }

                        if (tdCyclePlannerResult.ReturnCycleJourneyCount > 0)
                        {
                            viewState.SelectedReturnJourneyType = TDJourneyType.Cycle;
                            viewState.CycleJourneyReturningTimeSearchType = request.ReturnArriveBefore;

                            // Ensure when Cycle journey details page is displayed, the maps are shown by default
                            // Can only show either outward or return map
                            if (!viewState.OutwardShowMap)
                                viewState.ReturnShowMap = true;
                        }
                    }

                    stateData.Status = (tdCyclePlannerResult.IsValid ? AsyncCallStatus.CompletedOK : AsyncCallStatus.NoResults);

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "CyclePlanner - CycleJourneyPlanRunnerCaller. CyclePlannerManager result status is " + stateData.Status.ToString()));
                    }

                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyCycleResult, tdCyclePlannerResult);
                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyJourneyViewState, viewState); 
                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyAsyncCallState, stateData);
                }
                else if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "CyclePlanner - CycleJourneyPlanRunnerCaller. CyclePlannerManager has returned - discarding unexpected result"));
                }

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "CyclePlanner - CycleJourneyPlanRunnerCaller. Runner has finished saving results"));
                }
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                    "Exception: ", e, sessionID));
            }

        }

        /// <summary>
        /// Call the CyclePlannerManager
        /// </summary>
        /// <param name="referenceNumber">The reference number for the amendments of the journey parameters</param>
        /// <param name="lastSequenceNumber">The sequence number of the latest ammendment</param>
        /// <param name="cyclePlannerRequestID">The request ID of the call to the CyclePlanner</param>
        /// <param name="tdSessionManager">The session manager</param>
        /// <param name="tdCyclePlannerRequest">The journey request for submission to the CyclePlanner</param>
        /// <param name="sessionID">The ID of the user's Session</param>
        /// <param name="userIsLoggedOn">True indicates the user is logged on.  False indicates anonymous usage.</param>
        private void CallCyclePlannerForAmendments(int referenceNumber, int lastSequenceNumber, Guid cyclePlannerRequestID, ITDCyclePlannerRequest tdCyclePlannerRequest, string sessionID, TDSessionPartition part, bool userIsLoggedOn, int userType, string lang, string clientAppDomainFriendlyName, string polylinesTransformXslt)
        {
            try
            {
                // Get a CyclePlannerManager from the service discovery
                ICyclePlannerManager cyclePlannerManager = (ICyclePlannerManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CyclePlannerManager];

                // Indicate that we are NOT an SLA monitoring transaction
                bool referenceTransation = false;

                ITDCyclePlannerResult tdCyclePlannerResult = cyclePlannerManager.CallCyclePlanner(
                    tdCyclePlannerRequest,
                    sessionID,
                    userType,
                    referenceTransation,
                    userIsLoggedOn,
                    lang,
                    polylinesTransformXslt);

                TDSessionSerializer deferManager = new TDSessionSerializer(clientAppDomainFriendlyName);

                // Refresh after call
                AsyncCallState stateData = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyAsyncCallState);

                if (cyclePlannerRequestID == stateData.RequestID)
                {
                    stateData.Status = (tdCyclePlannerResult.IsValid ? AsyncCallStatus.CompletedOK : AsyncCallStatus.NoResults);

                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyCycleResult, tdCyclePlannerResult);
                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyAsyncCallState, stateData);
                }
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                    "Exception: ", e, sessionID));
            }
        }

        #endregion
    }
}
