// ****************************************************************
// NAME         : JourneyPlanRunnerCaller.cs
// AUTHOR       : Andrew Sinclair
// DATE CREATED : 2005-06-07
// DESCRIPTION  : A remotable class to implement the IJourneyPlanRunnerCaller class.
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/JourneyPlanRunnerCaller.cs-arc  $
//
//   Rev 1.12   Sep 21 2011 09:53:22   mmodi
//Corrected to show last car journey planned when no more routes using the replan to avoid incidents
//Resolution for 5739: Real Time In Car - Failed journey Replan does not display last journey
//
//   Rev 1.11   Sep 15 2011 12:11:32   apatel
//Updated to resolve the issue with maps are not updating for real time information in car
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.10   Sep 14 2011 14:30:34   apatel
//Updated to resolve the issues with Page broken when planning the return journey and replanning the outward journey
//Resolution for 5737: CCN 0548 - Real Time Information in Car issues
//
//   Rev 1.9   Sep 06 2011 14:38:06   apatel
//Updated to resolve the issue with public journeys not copied when calling cjp to modify the existing journey result
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.8   Sep 06 2011 12:13:44   apatel
//Updated for Real Time Car following code review
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.7   Sep 06 2011 11:20:36   apatel
//Updated for Real Time Information for Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.6   Sep 02 2011 10:22:04   apatel
//Real time car changes
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.5   Sep 01 2011 10:43:36   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.4   Dec 21 2010 14:05:08   apatel
//Code updated to request services for the day of travel starting from 01:00 on the current day to 01:00 the following day for Find a train, Find a flight and City to City requests
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.3   Oct 12 2009 17:47:14   mmodi
//Corrected EBC logic check when in door to door mode
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 06 2009 14:02:20   mmodi
//Added logic to calculate environmental benefits for a road journey
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Mar 28 2008 16:01:44   pscott
//USD 2206283   if train only and via requires a walk then add underground mode so that travel lines are consulted - otherwise will only use TTBO and will fail journey
//
//   Rev 1.0   Nov 08 2007 12:24:44   mturner
//Initial revision.
//
//   Rev 1.9   Nov 28 2006 15:44:22   pscott
//SCR4275 - RFC ATO437
//
//   Rev 1.8   Apr 12 2006 17:15:58   rwilby
//Changed logic in the CallCJP method to only set the viewState.SelectedOutwardJourneyType and viewState.SelectedReturnJourneyType properties to ‘RoadCongested’ if road journeys exist.
//Resolution for 3886: Point X Map Symbols: Default maps view does not show all transport icons for PT journey
//
//   Rev 1.7   Mar 14 2006 08:41:38   build
//Automatically merged from branch for stream3353
//
//   Rev 1.6.1.0   Mar 10 2006 16:31:42   RGriffith
//Fix to ensure car journeys are automatically highlighted in ResultsSummaryControl
//
//   Rev 1.6   Dec 07 2005 10:48:14   asinclair
//Changed  'Some of these journeys start in the past'  to a warning as it is not an error
//
//   Rev 1.5   Nov 09 2005 12:31:34   build
//Automatically merged from branch for stream2818
//
//   Rev 1.4.1.0   Oct 14 2005 15:10:44   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//
//   Rev 1.4   Aug 31 2005 12:14:46   asinclair
//Made Overlapping Journeys a Warning message
//Resolution for 2740: DN062: Leave destination before Arriving needs to be a Warning, not an Error
//
//   Rev 1.3   Jun 30 2005 16:44:42   asinclair
//Changes made after FXCOP
//
//   Rev 1.1   Jun 17 2005 09:50:44   jgeorge
//Changed from using OneWay attribute to asynchronous call to a private method, as OneWay attribute did not do what was expected.
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.0   Jun 15 2005 14:21:00   asinclair
//Initial revision.

using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.EnvironmentalBenefits;
using EB = TransportDirect.UserPortal.EnvironmentalBenefits;

using Logger = System.Diagnostics.Trace;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    /// <summary>
    /// Summary description for JourneyPlanRunnerCaller.
    /// </summary>
    public class JourneyPlanRunnerCaller : MarshalByRefObject, IJourneyPlanRunnerCaller
    {
        #region Delegates

        private delegate void DelegateInvokeCJPManagerForNew(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang, Guid cjpRequestId, TDJourneyParameters tdJourneyParameters, ITDJourneyParameterConverter converter, TDDateTime outwardDateTime, TDDateTime returnDateTime, bool isExtension, FindAPlannerMode findAMode);
        private delegate void DelegateInvokeCJPManagerForAmend(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, Guid cjpRequestId, int referenceNumber, int lastSequenceNumber, ITDJourneyRequest tdJourneyRequest, bool modifyOriginalRequest);

        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public JourneyPlanRunnerCaller()
        {
        }

        #region IJourneyPlanRunnerCaller Members

        /// <summary>
        /// Invoke the CJP Manager 
        /// </summary>
        /// <param name="sessionInfo">Information on the user's session</param>
        /// <param name="currentPartition">Indicates if cost based or time based search</param>
        /// <param name="lang">The language of the current UI culture</param>
        /// <param name="cjpRequestId">The request ID of the call to the CJP</param>
        /// <param name="tdJourneyParameters">The validated user's Journey Parameters</param>
        /// <param name="converter">Converter class to use</param>
        /// <param name="outwardDateTime">The Date/Time of the outward journey</param>
        /// <param name="returnDateTime">The Date/Time of the return journey (may be null for one-way journeys)</param>
        /// <param name="isExtension">True if the request is for a journey extension, false otherwise</param>
        public void InvokeCJPManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang, Guid cjpRequestId, TDJourneyParameters tdJourneyParameters, ITDJourneyParameterConverter converter, TDDateTime outwardDateTime, TDDateTime returnDateTime, bool isExtension, FindAPlannerMode findAMode)
        {
            DelegateInvokeCJPManagerForNew theDelegate = new DelegateInvokeCJPManagerForNew(InvokeCJPManagerForNew);
            theDelegate.BeginInvoke(sessionInfo, currentPartition, lang, cjpRequestId, tdJourneyParameters, converter, outwardDateTime, returnDateTime, isExtension, findAMode, null, null);
        }

        /// <summary>
        /// Invoke the CJP Manager for Amendments
        /// </summary>
        /// <param name="sessionInfo">Information on the user's session</param>
        /// <param name="cjpRequestId">The request ID of the call to the CJP</param>
        /// <param name="referenceNumber">The reference number for the amendments of the journey parameters</param>
        /// <param name="lastSequenceNumber">The sequence number of the latest ammendment</param>
        /// <param name="tdJourneyRequest">The journey request for submission to the CJP</param>
        public void InvokeCJPManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, Guid cjpRequestId, int referenceNumber, int lastSequenceNumber, ITDJourneyRequest tdJourneyRequest, bool modifyOriginalRequest)
        {
            DelegateInvokeCJPManagerForAmend theDelegate = new DelegateInvokeCJPManagerForAmend(InvokeCJPManagerForAmend);
            theDelegate.BeginInvoke(sessionInfo, currentPartition, cjpRequestId, referenceNumber, lastSequenceNumber, tdJourneyRequest, modifyOriginalRequest, null, null);
        }

        #endregion

        #region Private methods for doing the preparation and CJP call


        /// <summary>
        /// Invoke the CJP Manager 
        /// </summary>
        /// <param name="sessionInfo">Information on the user's session</param>
        /// <param name="currentPartition">Indicates if cost based or time based search</param>
        /// <param name="lang">The language of the current UI culture</param>
        /// <param name="cjpRequestId">The request ID of the call to the CJP</param>
        /// <param name="tdJourneyParameters">The validated user's Journey Parameters</param>
        /// <param name="converter">Converter class to use</param>
        /// <param name="outwardDateTime">The Date/Time of the outward journey</param>
        /// <param name="returnDateTime">The Date/Time of the return journey (may be null for one-way journeys)</param>
        /// <param name="isExtension">True if the request is for a journey extension, false otherwise</param>
        private void InvokeCJPManagerForNew(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang, Guid cjpRequestId, TDJourneyParameters tdJourneyParameters, ITDJourneyParameterConverter converter, TDDateTime outwardDateTime, TDDateTime returnDateTime, bool isExtension, FindAPlannerMode findAMode)
        {
            try
            {
                //Creates a Journey Request using the Convert method
                ITDJourneyRequest request = converter.Convert(tdJourneyParameters, outwardDateTime, returnDateTime);

                request.FindAMode = findAMode;

                //Creates an instance of TDSessionSerializer to access objects in deferred storage
                TDSessionSerializer deferedStorage = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);

                //And now save the request back to the session.
                deferedStorage.SerializeSessionObjectAndSave(sessionInfo.SessionId, currentPartition, TDSessionManager.KeyJourneyRequest, request);

                //Call the CJP Manager
                CallCJP(
                    cjpRequestId,
                    request,
                    sessionInfo.SessionId,
                    currentPartition,
                    sessionInfo.IsLoggedOn,
                    sessionInfo.UserType,
                    lang,
                    isExtension,
                    sessionInfo.OriginAppDomainFriendlyName);
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("Message: [{0}], Stack Trace: [{1}]", e.Message, e.StackTrace), e));
            }
        }


        /// <summary>
        /// Invoke the CJP Manager for Amendments
        /// </summary>
        /// <param name="sessionInfo">Information on the user's session</param>
        /// <param name="cjpRequestId">The request ID of the call to the CJP</param>
        /// <param name="referenceNumber">The reference number for the amendments of the journey parameters</param>
        /// <param name="lastSequenceNumber">The sequence number of the latest ammendment</param>
        /// <param name="tdJourneyRequest">The journey request for submission to the CJP</param>
        private void InvokeCJPManagerForAmend(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, Guid cjpRequestId, int referenceNumber, int lastSequenceNumber, ITDJourneyRequest tdJourneyRequest, bool modifyOriginalResult)
        {
            try
            {
                // The Original result will get modified with the modified journey result 
                // Used to replan the road journeys avoiding specified toids
                if (modifyOriginalResult)
                {
                    CallCJPToModify(referenceNumber, lastSequenceNumber, cjpRequestId, tdJourneyRequest, sessionInfo.SessionId,
                        currentPartition, sessionInfo.IsLoggedOn, sessionInfo.UserType, sessionInfo.Language, sessionInfo.OriginAppDomainFriendlyName);
                }
                else // The result will get amended with the new journey result - Get used in journey replan
                {
                    CallCJPForAmendments(referenceNumber, lastSequenceNumber, cjpRequestId, tdJourneyRequest, sessionInfo.SessionId,
                        currentPartition, sessionInfo.IsLoggedOn, sessionInfo.UserType, sessionInfo.Language, sessionInfo.OriginAppDomainFriendlyName);
                }
            }

            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("Message: [{0}], Stack Trace: [{1}]", e.Message, e.StackTrace), e));
            }


        }

        #endregion

        #region Methods to call the CJP

        /// <summary>
        /// Call the CJP Manager
        /// </summary>
        /// <param name="cjpCallRequestID">The ID of the call to the CJP</param>
        /// <param name="journeyRequest">The journey request for submission to the CJP</param>
        /// <param name="sessionID"></param>
        /// <param name="isExtension">True if the request is for a journey extension, false otherwise</param>
        public void CallCJP(Guid cjpCallRequestID, ITDJourneyRequest journeyRequest, string sessionID, TDSessionPartition part, bool userIsLoggedOn, int userType, string lang, bool isExtension, string clientAppDomainFriendlyName)
        {
            try
            {
                // Get a CJP Manager from the service discovery
                ICJPManager cjpManager = (ICJPManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];

                // Indicate that we are NOT an SLA monitoring transaction
                bool referenceTransation = false;

                // If orig/dest/via is other than rail stations when only rail selected 
                // then add mode underground  so that cjp is forced to consult travelines.
                // Otherwise will just use TTBO which may not be able to plan to or from rail stn.
                bool originIsRail = false;
                bool destIsRail = false;
                bool viaIsRail = false;
                bool viaExists = false;

                for (int i = 0; i < journeyRequest.OriginLocation.NaPTANs.Length; i++)
                {
                    if (i == 0) originIsRail = true;
                    if (journeyRequest.OriginLocation.NaPTANs[i].StationType != StationType.Rail) originIsRail = false;
                }
                for (int i = 0; i < journeyRequest.DestinationLocation.NaPTANs.Length; i++)
                {
                    if (i == 0) destIsRail = true;
                    if (journeyRequest.DestinationLocation.NaPTANs[i].StationType != StationType.Rail) destIsRail = false;
                }
                for (int x = 0; x < journeyRequest.PublicViaLocations.Length; x++)
                {
                    for (int i = 0; i < journeyRequest.PublicViaLocations[x].NaPTANs.Length; i++)
                    {
                        if (x == 0) // we have a via present so set initial flags
                        {
                            viaIsRail = true;
                            viaExists = true;
                        }
                        if (journeyRequest.PublicViaLocations[x].NaPTANs[i].StationType != StationType.Rail) viaIsRail = false;
                    }
                }

                //if any location involved in journey is other than rail then check if PT is Rail mode only
                if (originIsRail == false || destIsRail == false || (viaExists && viaIsRail == false))
                {
                    if (journeyRequest.Modes.Length <= 2 && journeyRequest.Modes[0] == ModeType.Rail)
                    {
                        if (journeyRequest.Modes.Length == 2 && journeyRequest.Modes[1] == ModeType.Car)
                        {
                            journeyRequest.Modes = new ModeType[] { ModeType.Rail, ModeType.Underground, ModeType.Car };
                        }

                        if (journeyRequest.Modes.Length == 1)
                        {
                            journeyRequest.Modes = new ModeType[] { ModeType.Rail, ModeType.Underground };
                        }
                    }
                }

                ITDJourneyResult tdJourneyResult = cjpManager.CallCJP(
                    journeyRequest,
                    sessionID,
                    userType,
                    referenceTransation,
                    userIsLoggedOn,
                    lang,
                    isExtension);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "CJPManager has returned - sessionId = " + sessionID));
                }

                TDSessionSerializer deferManager = new TDSessionSerializer(clientAppDomainFriendlyName);
                // Refresh after call
                AsyncCallState stateData = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyAsyncCallState);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "Retrieved AsyncCallState - sessionId = " + sessionID));
                }

                if (cjpCallRequestID == stateData.RequestID)
                {
                    TDJourneyRequest request = (TDJourneyRequest)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyJourneyRequest);



                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "TDJourneyViewState instantiated - sessionId = " + sessionID));
                    }
                    TDJourneyViewState viewState = new TDJourneyViewState();
                    viewState.OriginalJourneyRequest = request;

                    // Check if any return journey times overlap with outward journey times
                    if (tdJourneyResult.IsValid)
                    {
                        if (tdJourneyResult.CheckForReturnOverlap(request))
                        {
                            tdJourneyResult.AddMessageToArray(string.Empty, "JourneyPlannerOutput.JourneyOverlap", 0, 0, ErrorsType.Warning);
                        }

                        //check if any journeys returned start in the past 
                        if (tdJourneyResult.CheckForJourneyStartInPast(request))
                        {
                            tdJourneyResult.AddMessageToArray(string.Empty, "JourneyPlannerOutput.JourneyTimeInPast", 0, 0, ErrorsType.Warning);
                        }

                        if (tdJourneyResult.OutwardPublicJourneyCount == 0 &&
                            tdJourneyResult.OutwardRoadJourneyCount > 0)
                        {
                            viewState.SelectedOutwardJourneyType = TDJourneyType.RoadCongested;
                        }

                        if (tdJourneyResult.ReturnPublicJourneyCount == 0
                            && tdJourneyResult.ReturnRoadJourneyCount > 0)
                        {
                            viewState.SelectedReturnJourneyType = TDJourneyType.RoadCongested;
                        }

                        #region Calculate Environmental Benefits

                        // Calculate environmental benefits for the road journey if required

                        // Get the object and check cast type (because Door to door does not have a find page state)
                        object objFindPageState = (object)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyFindPageState);
                        if (objFindPageState is FindPageState)
                        {
                            FindPageState findPageState = (FindPageState)objFindPageState;

                            if ((findPageState != null) && (findPageState.Mode == FindAMode.EnvironmentalBenefits))
                            {
                                if (findPageState as FindEBCPageState != null)
                                {
                                    // Cast to the EBCPageState
                                    FindEBCPageState ebcPageState = (FindEBCPageState)findPageState;

                                    // Get the EnvironmentalBenefitsCalculator from the service discovery
                                    EnvironmentalBenefitsCalculator calc = (EnvironmentalBenefitsCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.EnvironmentalBenefitsCalculator];

                                    // Calculate benefits
                                    EB.EnvironmentalBenefits envBenefits = calc.GetEnvironmentalBenefits(tdJourneyResult.OutwardRoadJourney(), sessionID);

                                    // Assign benefits to the page state
                                    ebcPageState.EnvironmentalBenefits = envBenefits;

                                    // Serialise back to deferred storage
                                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyFindPageState, ebcPageState);
                                }
                            }
                        }

                        #endregion
                    }

                    stateData.Status = (tdJourneyResult.IsValid ? AsyncCallStatus.CompletedOK : AsyncCallStatus.NoResults);

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "CJPManager has returned - status is " + stateData.Status.ToString()));
                    }

                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyJourneyResult, tdJourneyResult);
                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyJourneyViewState, viewState);
                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyAsyncCallState, stateData);

                    ITDMapHandoff handoff = (ITDMapHandoff)TDServiceDiscovery.Current[ServiceDiscoveryKey.TDMapHandoff];
                    handoff.SaveJourneyResult(tdJourneyResult, sessionID);

                }
                else if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "CJPManager has returned - discarding unexpected result"));
                }

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "CJP Runner finished saving results"));
                }
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                    "Exception: ", e, sessionID));
            }

        }

        /// <summary>
        /// Call the CJP Manager
        /// </summary>
        /// <param name="referenceNumber">The reference number for the amendments of the journey parameters</param>
        /// <param name="lastSequenceNumber">The sequence number of the latest ammendment</param>
        /// <param name="cjpCallRequestID">The request ID of the call to the CJP</param>
        /// <param name="tdSessionManager">The session manager</param>
        /// <param name="journeyRequest">The journey request for submission to the CJP</param>
        /// <param name="sessionID">The ID of the user's Session</param>
        /// <param name="userIsLoggedOn">True indicates the user is logged on.  False indicates anonymous usage.</param>
        public void CallCJPForAmendments(int referenceNumber, int lastSequenceNumber, Guid cjpCallRequestID, ITDJourneyRequest journeyRequest, string sessionID, TDSessionPartition part, bool userIsLoggedOn, int userType, string lang, string clientAppDomainFriendlyName)
        {
            try
            {
                // Get a CJP Manager from the service discovery
                ICJPManager cjpManager = (ICJPManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];

                // Indicate that we are NOT an SLA monitoring transaction
                bool referenceTransation = false;

                ITDJourneyResult tdJourneyResult = cjpManager.CallCJP(
                    journeyRequest,
                    sessionID,
                    userType,
                    referenceTransation,
                    userIsLoggedOn,
                    lang,
                    false);

                TDSessionSerializer deferManager = new TDSessionSerializer(clientAppDomainFriendlyName);

                // Refresh after call
                AsyncCallState stateData = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyAsyncCallState);

                if (cjpCallRequestID == stateData.RequestID)
                {
                    stateData.Status = (tdJourneyResult.IsValid ? AsyncCallStatus.CompletedOK : AsyncCallStatus.NoResults);

                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyAmendedJourneyResult, tdJourneyResult);
                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyAsyncCallState, stateData);
                }
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                    "Exception: ", e, sessionID));
            }
        }


        /// <summary>
        /// Call the CJP Manager modifying the original journey result with the newly planned journey result
        /// </summary>
        /// <param name="referenceNumber">The reference number for the amendments of the journey parameters</param>
        /// <param name="lastSequenceNumber">The sequence number of the latest ammendment</param>
        /// <param name="cjpCallRequestID">The request ID of the call to the CJP</param>
        /// <param name="tdSessionManager">The session manager</param>
        /// <param name="journeyRequest">The journey request for submission to the CJP</param>
        /// <param name="sessionID">The ID of the user's Session</param>
        /// <param name="userIsLoggedOn">True indicates the user is logged on.  False indicates anonymous usage.</param>
        public void CallCJPToModify(int referenceNumber, int lastSequenceNumber, Guid cjpCallRequestID, ITDJourneyRequest journeyRequest, string sessionID, TDSessionPartition part, bool userIsLoggedOn, int userType, string lang, string clientAppDomainFriendlyName)
        {
            try
            {
                // Get a CJP Manager from the service discovery
                ICJPManager cjpManager = (ICJPManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];

                // Indicate that we are NOT an SLA monitoring transaction
                bool referenceTransation = false;

                // Get the tdjourney result with new journey reference number
                // Later on when modifying update the journey reference number with the original journey result's journey reference number
                ITDJourneyResult tdJourneyResult = cjpManager.CallCJP(
                    journeyRequest,
                    sessionID,
                    userType,
                    referenceTransation,
                    userIsLoggedOn,
                    lang,
                    false);

                // Get the existing journey result from the session
                // The new result will only modify the existing journey result rather than an amendment to it

                TDSessionSerializer deferManager = new TDSessionSerializer(clientAppDomainFriendlyName);
                
                // Refresh after call
                AsyncCallState stateData = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyAsyncCallState);

                // Get the exisiting journey result
                ITDJourneyResult originalTDJourneyResult = (ITDJourneyResult)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyJourneyResult);

                // Set the journey reference no and the journey times from original TD journey result to new TD journey result
                if (tdJourneyResult is TDJourneyResult && originalTDJourneyResult is TDJourneyResult)
                {
                    TDJourneyResult newJourneyResult = (TDJourneyResult)tdJourneyResult;
                    TDJourneyResult origJourneyResult = (TDJourneyResult)originalTDJourneyResult;

                    newJourneyResult.UpdateJourneyReferenceNoAndTime(origJourneyResult.JourneyReferenceNumber, origJourneyResult.TimeOutward, origJourneyResult.TimeReturn,
                        origJourneyResult.ArriveBeforeOutward, origJourneyResult.ArriveBeforeReturn);
                }


                if (tdJourneyResult != null)
                {
                    #region Update road journeys

                    // Copy the outward road journey if an outward was was not replanned and it exists in the original result
                    if (!journeyRequest.IsOutwardRequired && originalTDJourneyResult.OutwardRoadJourneyCount > 0)
                    {
                        tdJourneyResult.AddRoadJourney(originalTDJourneyResult.OutwardRoadJourney(), true, false);
                    }

                    // Copy the return road journey if a return was was not replanned and it exists in the original result
                    if (!journeyRequest.IsReturnRequired && originalTDJourneyResult.ReturnRoadJourneyCount > 0)
                    {
                        tdJourneyResult.AddRoadJourney(originalTDJourneyResult.ReturnRoadJourney(), false, false);
                    }

                    // Error scenarios:
                    // Copy the outward road journey if an outward was replanned but does not exist in the 
                    // new result, and does exists in the original result
                    if (journeyRequest.IsOutwardRequired && tdJourneyResult.OutwardRoadJourneyCount == 0
                        && originalTDJourneyResult.OutwardRoadJourneyCount > 0)
                    {
                        RoadJourney roadJourney = originalTDJourneyResult.OutwardRoadJourney();

                        // Set the allow replan to false as this error scenario means there are no
                        // alternative road journeys
                        roadJourney.AllowReplan = false;

                        tdJourneyResult.AddRoadJourney(roadJourney, true, false);
                    }

                    // Copy the return road journey if a return was replanned but does not exist in the 
                    // new result, and does exists in the original result
                    if (journeyRequest.IsReturnRequired && tdJourneyResult.ReturnRoadJourneyCount == 0
                        && originalTDJourneyResult.ReturnRoadJourneyCount > 0)
                    {
                        RoadJourney roadJourney = originalTDJourneyResult.ReturnRoadJourney();

                        // Set the allow replan to false as this error scenario means there are no
                        // alternative road journeys
                        roadJourney.AllowReplan = false;

                        tdJourneyResult.AddRoadJourney(roadJourney, false, false);
                    }

                    #endregion

                    #region Update public journeys

                    // Coping the outward public journeys if outward journeyes not replanned
                    // Check if the new TDJourneyResult have not got any outward public journeys 
                    if (tdJourneyResult.OutwardPublicJourneyCount == 0 && originalTDJourneyResult.OutwardPublicJourneyCount > 0)
                    {
                        foreach (object pjJourney in originalTDJourneyResult.OutwardPublicJourneys)
                        {
                            if (pjJourney != null)
                            {
                                TransportDirect.UserPortal.JourneyControl.PublicJourney publicJourney = pjJourney as TransportDirect.UserPortal.JourneyControl.PublicJourney;

                                tdJourneyResult.AddPublicJourney(publicJourney, true, true, true);
                            }
                        }
                    }

                    // Coping the return public journeys if outward journeyes not replanned
                    // Check if the new TDJourneyResult have not got any outward public journeys 
                    if (tdJourneyResult.ReturnPublicJourneyCount == 0 && originalTDJourneyResult.ReturnPublicJourneyCount > 0)
                    {
                        foreach (object pjJourney in originalTDJourneyResult.ReturnPublicJourneys)
                        {
                            if (pjJourney != null)
                            {
                                TransportDirect.UserPortal.JourneyControl.PublicJourney publicJourney = pjJourney as TransportDirect.UserPortal.JourneyControl.PublicJourney;

                                tdJourneyResult.AddPublicJourney(publicJourney, false, true, true);
                            }
                        }
                    }

                    #endregion

                    // Update the result valid flag to ensure journeys are displayed
                    tdJourneyResult.IsValid = (tdJourneyResult.OutwardPublicJourneyCount + tdJourneyResult.OutwardRoadJourneyCount
                        + tdJourneyResult.ReturnPublicJourneyCount + tdJourneyResult.ReturnRoadJourneyCount) > 0;
                }
                else
                {
                    // assign the existing original td journey result to new one
                    tdJourneyResult = originalTDJourneyResult;
                }
                                
                
                if (cjpCallRequestID == stateData.RequestID)
                {
                    stateData.Status = (tdJourneyResult.IsValid ? AsyncCallStatus.CompletedOK : AsyncCallStatus.NoResults);

                    // Save new journey result instead of original journey result
                    // This will preserve any CJP messages raised during the new request
                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyJourneyResult, tdJourneyResult);
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