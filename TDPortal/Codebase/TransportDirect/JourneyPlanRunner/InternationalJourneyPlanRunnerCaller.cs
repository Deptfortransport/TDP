// ****************************************************************
// NAME         : InternationalJourneyPlanRunnerCaller.cs
// AUTHOR       : Amit Patel
// DATE CREATED : 02 Feb 2008
// DESCRIPTION  : A remotable class to implement the IInternationalJourneyPlanRunnerCaller class.
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/InternationalJourneyPlanRunnerCaller.cs-arc  $
//
//   Rev 1.4   Feb 24 2010 17:32:24   mmodi
//Save the request in to the Original Journey Request to allow journey dates to be correctly shown on the details page
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 23 2010 13:32:34   mmodi
//Check for null country object before validating permitted journey
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 16 2010 11:15:32   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 09 2010 10:45:40   apatel
//Make the classes public
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 09:37:06   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.InternationalPlannerControl;
using TransportDirect.UserPortal.JourneyControl;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    public class InternationalJourneyPlanRunnerCaller: MarshalByRefObject, IInternationalJourneyPlanRunnerCaller
    {
        #region Delegates

        private delegate void DelegateInvokeInternationalPlannerManagerForNew
            (CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang, Guid internationalPlannerRequestId,
            TDJourneyParameters tdJourneyParameters, TDJourneyParametersMultiConverter converter, 
            TDDateTime outwardDateTime, TDDateTime returnDateTime);
        
       
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public InternationalJourneyPlanRunnerCaller()
        {
        }

        #endregion

        #region IInternationalJourneyPlanRunnerCaller Members
        /// <summary>
        /// Invakes International Planner for new journey
        /// </summary>
        /// <param name="sessionInfo">Information on the user's session</param>
        /// <param name="currentPartition">Indicates if cost based or time based search</param>
        /// <param name="lang">The language of the current UI culture</param>
        /// <param name="internationalPlannerRequestId">The request ID of the call to the InternationalPlanner</param>
        /// <param name="tdJourneyParameters">The validated user's Journey Parameters</param>
        /// <param name="converter">Converter to use to convert journey parameters into a request object</param>
        /// <param name="outwardDateTime">Outward date time</param>
        /// <param name="returnDateTime">Return date time</param>
        public void InvokeInternationalPlannerManager(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang, Guid internationalPlannerRequestId, TDJourneyParameters tdJourneyParameters, TDJourneyParametersMultiConverter converter, TDDateTime outwardDateTime, TDDateTime returnDateTime)
        {
            DelegateInvokeInternationalPlannerManagerForNew theDelegate = new DelegateInvokeInternationalPlannerManagerForNew(InvokeInternationalPlannerManagerForNew);
            theDelegate.BeginInvoke(sessionInfo, currentPartition, lang, internationalPlannerRequestId, tdJourneyParameters, converter, outwardDateTime, returnDateTime, null, null);
        }

        /// <summary>
        /// Determines if there is an International Journey possible between origin and destination country
        /// </summary>
        /// <param name="tdJourneyParameters"></param>
        /// <returns></returns>
        public bool IsPermittedInternationalJourney(TDJourneyParameters tdJourneyParameters)
        {
            bool permitted = false;

            // Check the country object exists
            if ((tdJourneyParameters.OriginLocation != null) && (tdJourneyParameters.DestinationLocation != null)
                && (tdJourneyParameters.OriginLocation.Country != null) && (tdJourneyParameters.DestinationLocation.Country != null))
            {
                // Get a InternationalPlannerManager from the service discovery
                IInternationalPlannerManager internationalPlannerManager = (IInternationalPlannerManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerManager];

                permitted = internationalPlannerManager.IsPermittedInternationalJourney(tdJourneyParameters.OriginLocation.Country.CountryCode,
                    tdJourneyParameters.DestinationLocation.Country.CountryCode);
            }

            return permitted;
        }

        #endregion

        #region Private methods for doing the preparation and International Planner call

        /// <summary>
        /// Invoke the InternationalPlannerManager 
        /// </summary>
        /// <param name="sessionInfo">Information on the user's session</param>
        /// <param name="currentPartition">Indicates if cost based or time based search</param>
        /// <param name="lang">The language of the current UI culture</param>
        /// <param name="internationalPlannerRequestId">The request ID of the call to the InternationalPlanner</param>
        /// <param name="tdJourneyParameters">The validated user's Journey Parameters</param>
        /// <param name="outwardDateTime">The Date/Time of the outward journey</param>
        /// <param name="returnDateTime">The Date/Time of the return journey (may be null for one-way journeys)</param>
        private void InvokeInternationalPlannerManagerForNew(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition, string lang, Guid internationalPlannerRequestId, TDJourneyParameters tdJourneyParameters, TDJourneyParametersMultiConverter converter, TDDateTime outwardDateTime, TDDateTime returnDateTime)
        {
            try
            {
                ITDJourneyRequest request = converter.Convert(tdJourneyParameters, outwardDateTime, returnDateTime);
               
                
                //Creates an instance of TDSessionSerializer to access objects in deferred storage
                TDSessionSerializer deferedStorage = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);

                //And now save the request back to the session.
                deferedStorage.SerializeSessionObjectAndSave(sessionInfo.SessionId, currentPartition, TDSessionManager.KeyJourneyRequest, request);

                //Call the InternationalPlannerManager
                CallInternationalPlanner(
                    internationalPlannerRequestId,
                    request,
                    sessionInfo.SessionId,
                    currentPartition,
                    sessionInfo.IsLoggedOn,
                    sessionInfo.UserType,
                    lang,
                    sessionInfo.OriginAppDomainFriendlyName);
            }
            catch (Exception e)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("Message: [{0}], Stack Trace: [{1}]", e.Message, e.StackTrace), e));
            }
        }


        #endregion

        #region Methods to call the InternationalPlanner

        /// <summary>
        /// Call the InternationalPlannerManager
        /// </summary>
        /// <param name="internationalPlannerRequestID">The ID of the call to the InternationalPlanner</param>
        /// <param name="tdInternationalPlannerRequest">The request for submission to the InternationalPlanner</param>
        /// <param name="sessionID"></param>
        private void CallInternationalPlanner(Guid internationalPlannerRequestID, ITDJourneyRequest tdInternationalPlannerRequest, string sessionID, TDSessionPartition part, bool userIsLoggedOn, int userType, string lang, string clientAppDomainFriendlyName)
        {
            try
            {
                // Get a InternationalPlannerManager from the service discovery
                IInternationalPlannerManager internationalPlannerManager = (IInternationalPlannerManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlannerManager];

                // Indicate that we are NOT an SLA monitoring transaction
                bool referenceTransation = false;


                ITDJourneyResult tdInternationalPlannerResult = internationalPlannerManager.CallInternationalPlanner(
                    tdInternationalPlannerRequest,
                    sessionID,
                    userType,
                    referenceTransation,
                    userIsLoggedOn,
                    lang);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "InternationalPlanner - InternationalJourneyPlanRunnerCaller. InternationalPlannerManager has returned - sessionId = " + sessionID));
                }

                TDSessionSerializer deferManager = new TDSessionSerializer(clientAppDomainFriendlyName);

                // Refresh after call
                AsyncCallState stateData = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyAsyncCallState);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "Retrieved AsyncCallState - sessionId = " + sessionID));
                }

                if (internationalPlannerRequestID == stateData.RequestID)
                {
                    ITDJourneyRequest request = (ITDJourneyRequest)deferManager.RetrieveAndDeserializeSessionObject(sessionID, part, TDSessionManager.KeyJourneyRequest);

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "TDJourneyViewState instantiated - sessionId = " + sessionID));
                    }

                    TDJourneyViewState viewState = new TDJourneyViewState();
                    viewState.OriginalJourneyRequest = request;

                    stateData.Status = (tdInternationalPlannerResult.IsValid ? AsyncCallStatus.CompletedOK : AsyncCallStatus.NoResults);

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "InternationalPlanner - InternationalJourneyPlanRunnerCaller. InternationalPlannerManager result status is " + stateData.Status.ToString()));
                    }

                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyJourneyResult, tdInternationalPlannerResult);
                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyJourneyViewState, viewState); 
                    deferManager.SerializeSessionObjectAndSave(sessionID, part, TDSessionManager.KeyAsyncCallState, stateData);
                }
                else if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "InternationalPlanner - InternationalJourneyPlanRunnerCaller. InternationalPlannerManager has returned - discarding unexpected result"));
                }

                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "InternationalPlanner - InternationalJourneyPlanRunnerCaller. Runner has finished saving results"));
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
