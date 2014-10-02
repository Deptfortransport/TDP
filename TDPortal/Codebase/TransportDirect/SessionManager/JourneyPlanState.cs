// *********************************************** 
// NAME         : AsyncCallState.cs
// AUTHOR       : Jonathan George
// DATE CREATED : 10/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/JourneyPlanState.cs-arc  $ 
//
//   Rev 1.3   Nov 11 2010 10:15:06   PScott
//Downgrade AsyncCallState Success nessage to info (was Error)
//
//Resolution for 5634: Downgrade error level of AsyncCallStatus.Completed OK messages
//
//   Rev 1.2   Jan 25 2010 15:51:56   MTurner
//Updated to show error message for cycle journeys that have timed out
//Resolution for 5384: Cycle Planner - No error message shown if journey times out
//
//   Rev 1.1   Nov 04 2009 14:26:26   nrankin
//Fix for issue when user gets no journey returned when feedback clearly shows a journey is returned...
//
//   Rev 1.0   Nov 08 2007 12:48:32   mturner
//Initial revision.
//
//   Rev 1.2   Apr 05 2006 15:42:42   build
//Automatically merged from branch for stream0030
//
//   Rev 1.1.1.0   Mar 29 2006 11:10:28   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.1   Oct 21 2005 18:21:08   jgeorge
//Added extra constructors.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 14 2005 15:13:38   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common.Logging;

using Logger = System.Diagnostics.Trace;
namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Class used to control the asynchronous call for time based journey planning
	/// </summary>
	[Serializable]
	public class JourneyPlanState : AsyncCallState
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public JourneyPlanState() : base( int.Parse(Properties.Current[JourneyControlConstants.WaitPageTimeoutSeconds] ) )
		{ }

		/// <summary>
		/// Implementation of the ProcessState method. Does any processing required for the current state
		/// of the object prior to redirection to the page specified in the return value.
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		public override TransportDirect.Common.PageId ProcessState()
		{
			switch (this.Status)
			{
				case AsyncCallStatus.CompletedOK:
                    // MT - Added code to check for errors or delays in saving journeys to the session DB
                    ITDSessionManager sessionManagerCurr = TDSessionManager.Current;
                    if (sessionManagerCurr != null)
                    {
                        if (sessionManagerCurr.JourneyResult != null)
                        {
                            if (sessionManagerCurr.JourneyResult.OutwardPublicJourneyCount == 0
                                && sessionManagerCurr.JourneyResult.OutwardRoadJourneyCount == 0
                                && sessionManagerCurr.JourneyResult.ReturnPublicJourneyCount == 0
                                && sessionManagerCurr.JourneyResult.ReturnRoadJourneyCount == 0)
                            {
                                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "AsyncCallStatus.CompletedOK Journey count = 0 -  session:" + sessionManagerCurr.Session.SessionID.ToString());
                                Logger.Write(oe);
                                return PageId.WaitPage;
                            }
                            else
                            {
                                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "AsyncCallStatus.CompletedOK Journey count > 0 -  session:" + sessionManagerCurr.Session.SessionID.ToString());
                                Logger.Write(oe);
                            }
                        }
                        else
                        {
                            if (sessionManagerCurr.CycleResult != null)
                            {
                                if (sessionManagerCurr.CycleResult.OutwardCycleJourneyCount == 0
                                    && sessionManagerCurr.CycleResult.ReturnCycleJourneyCount == 0)
                                {
                                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "AsyncCallStatus.CompletedOK Cycle Journey count = 0 -  session:" + sessionManagerCurr.Session.SessionID.ToString());
                                    Logger.Write(oe);
                                    return PageId.WaitPage;
                                }
                                else
                                {
                                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "AsyncCallStatus.CompletedOK Cycle Journey count > 0 -  session:" + sessionManagerCurr.Session.SessionID.ToString());
                                    Logger.Write(oe);
                                }
                            }
                        }
                    }
                    else
                    {
                        OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Checking for journeys being returned could not be done because sessionManagerCurr variable was null");
                        Logger.Write(oe);
                    }
                    // MT - End of added code
				    return DoCompletedOkProcessing();

				case AsyncCallStatus.TimedOut:
					return DoTimeoutProcessing();

				case AsyncCallStatus.NoResults:
					return this.ErrorPage;

				default:
					return PageId.WaitPage;
			}
		}

		/// <summary>
		/// Performs the processing required when the status is AsyncCallStatus.TimedOut
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		private PageId DoTimeoutProcessing()
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
            
            // Update for cycle journey
            if (sessionManager.FindAMode == FindAMode.Cycle)
            {
                if(sessionManager.CycleResult == null)
                    sessionManager.CycleResult = new TDCyclePlannerResult();

                //Update the result to include an error message
                sessionManager.CycleResult.AddMessageToArray(
                    string.Empty,
                    CyclePlannerConstants.CPInternalError,
                    CyclePlannerConstants.CPCallError,
                    0);
            }
            else
            {
                if (sessionManager.JourneyResult == null)
                    sessionManager.JourneyResult = new TDJourneyResult();

                // Update the result to include an error message.
                sessionManager.JourneyResult.AddMessageToArray(
                    string.Empty,
                    JourneyControlConstants.CJPInternalError,
                    JourneyControlConstants.CjpCallError,
                    0);
            }

            // Copy the journey request into viewstate
            sessionManager.JourneyViewState = new TDJourneyViewState();
            sessionManager.JourneyViewState.OriginalJourneyRequest = sessionManager.JourneyRequest;
            
            return this.ErrorPage;
		}

		/// <summary>
		/// Performs the processing required when the status is AsyncCallStatus.CompletedOK
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		private PageId DoCompletedOkProcessing()
		{
			// Set flag to indicate that we are going to the destination page from
			// the wait page.
			TDSessionManager.Current.SetOneUseKey(SessionKey.FirstViewingOfResults, "yes");

			return this.DestinationPage;
		}

	}
}
