// *********************************************************
// NAME			: VisitPlanRunnerCaller.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 06/09/2005
// DESCRIPTION	: Visit Plan Runner Caller Interface
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/VisitPlanRunner/VisitPlanRunnerCaller.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:51:10   mturner
//Initial revision.
//
//   Rev 1.10   Nov 24 2005 16:36:16   tmollart
//Updates from code review comments. Ensured that error logging for ItintialItinerary and AddJourneys actually logs the error.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.9   Nov 17 2005 16:52:00   tmollart
//Added additional error handling.
//Resolution for 2946: Visit Planner (CG): duplicate transport from locality
//Resolution for 2950: Visit Planner (CG): Force Coach option is not rejected but should have been
//
//   Rev 1.8   Nov 10 2005 11:49:36   jgeorge
//Changed to inherit from MarshalByRefObject to enable for remoting. Added general error handler to InvokeCJPForInitialItinerary method.
//
//   Rev 1.7   Nov 09 2005 18:57:16   RPhilpott
//Merge with stream2818
//
//   Rev 1.6   Nov 09 2005 15:03:20   tmollart
//Fixed bugs which meant itinerary manager was not getting saved at the correct time.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Nov 03 2005 17:58:24   tmollart
//Fixed problems that occured if no results where returned.
//
//   Rev 1.4   Oct 29 2005 14:16:20   tmollart
//Modified processing order, detection of CJP errors and added a flat to determine if processing should continue if there is an error.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 18 2005 10:36:00   jbroome
//Ensured each itinerary segment has its own Journey State
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 10 2005 17:58:30   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Sep 21 2005 17:22:38   tmollart
//Work in progress.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 13 2005 12:16:06   tmollart
//Initial revision.

using System;
using System.Text;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.VisitPlanRunner
{
	/// <summary>
	/// Summary description for VisitPlanRunnerCaller.
	/// </summary>
	public class VisitPlanRunnerCaller : MarshalByRefObject, IVisitPlanRunnerCaller
	{

		/// <summary>
		/// Delegate to invoke CJP manager on another thread for running the initial itinerary.
		/// </summary>
		private delegate void DelegateInvokeCJPInitialItinerary(CJPSessionInfo sessionInfo, TDSessionPartition partition);

		/// <summary>
		/// Delegate to invoke CJP on another thread for adding journeys to an existing itinerary.
		/// </summary>
		private delegate void DelegateInvokeCJPAddJourneys(CJPSessionInfo sessionInfo, TDSessionPartition partition);


		/// <summary>
		/// Process name to differentiate this from other CJP managers
		/// </summary>
		private string processName = "CJPManager (VisitPlanner)";

		/// <summary>
		/// Default Constructor
		/// </summary>
		public VisitPlanRunnerCaller()
		{
		}

		
		/// <summary>
		/// Runs initial visit plan itinerary by delegating the processing to another
		/// execution thread. This method will return imediately leaving the processing
		/// to run on another thread.
		/// </summary>
		/// <param name="sessionInfo">Users session information object</param>
		/// <param name="currentPartition">Current partition object</param>
		public void RunInitialItinerary(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition)
		{
			// This delegates to another method in this object
			DelegateInvokeCJPInitialItinerary del = new DelegateInvokeCJPInitialItinerary(InvokeCJPForInitialItinerary);
			del.BeginInvoke(sessionInfo, currentPartition, null, null);
		}


		/// <summary>
		/// Add journeys to a visit plan itineerary by delegating the process to
		/// another execution thread.
		/// </summary>
		/// <param name="sessionInfo">Users session information object</param>
		/// <param name="currentPartition">Current partition object</param>
		public void RunAddJourneys(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition)
		{
			DelegateInvokeCJPAddJourneys del = new DelegateInvokeCJPAddJourneys(InvokeCJPForAddJourneys);
			del.BeginInvoke(sessionInfo, currentPartition, null, null);
		}


		/// <summary>
		/// Private method which performs actually planning of the journey using the CJP. This method
		/// will plan the users requested itinerary checking for errors at each stage of the process.
		/// </summary>
		/// <param name="sessionInfo">Users session information object</param>
		/// <param name="partition">Current partition object</param>
		private void InvokeCJPForInitialItinerary(CJPSessionInfo sessionInfo, TDSessionPartition partition)
		{
			TDSessionSerializer ser = null;
			try
			{
				//Write log message indicating method has started.
				WriteCJPLogMessage("InvokeCJPForInitialJourney has started.");

				// Boolean to determine if processing will continue
				// after an error has been detected.
				bool continueProcess;
			
				// General purpose serializer object that will be used for a number of 
				// operations in this method.
				ser = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);

				// Other object declarations
				TDJourneyParametersVisitPlan parameters;
				AsyncCallState originalState;
				IVisitPlannerItineraryManagerRemote itineraryManager;
				ITDJourneyRequest firstRequest;

				// Deserialise required objects from session storage.
				parameters = (TDJourneyParametersVisitPlan)ser.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyJourneyParameters);
				originalState = (AsyncCallState)ser.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);
				itineraryManager = (IVisitPlannerItineraryManagerRemote)ser.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyItineraryManager);

				// JOURNEY 1 - Build first request and then call CJP.
				firstRequest = itineraryManager.BuildNextRequest(parameters);

				ITDJourneyResult result = CallCJP(
					originalState.RequestID, 
					firstRequest, 
					sessionInfo.SessionId, 
					partition, 
					sessionInfo.IsLoggedOn, 
					sessionInfo.UserType, 
					sessionInfo.Language, 
					sessionInfo.OriginAppDomainFriendlyName);

				// Create a new journey state object and set required properties.
				TDJourneyState firstJourneyState = new TDJourneyState();
				firstJourneyState.OriginalJourneyRequest = (ITDJourneyRequest)firstRequest;

				// Call method to add this journey to the itinerary. We already have a deserialised 
				// parameters and result object so these can be passed directly to the method. 
				itineraryManager.AddSegmentToItinerary(parameters, firstRequest, result, firstJourneyState);
				WriteCJPLogMessage("First journey result object added to itinerary manager.");

				// If the result object has CJP messages then a failure has occured. CallCJP will
				// have handled the failure conditions so here we need to stop processing.
				continueProcess = !(result.CJPMessages.GetLength(0) > 0);

				// JOURNEY 2 - Build 2nd request and then call CJP.
				if (continueProcess)
				{
					ITDJourneyRequest secondRequest = itineraryManager.BuildNextRequest(parameters);

					result = CallCJP(
						originalState.RequestID,
						secondRequest,
						sessionInfo.SessionId,
						partition,
						sessionInfo.IsLoggedOn, 
						sessionInfo.UserType, 
						sessionInfo.Language, 
						sessionInfo.OriginAppDomainFriendlyName);

					TDJourneyState secondJourneyState = new TDJourneyState();
					// Add this journey to the itinerary
					itineraryManager.AddSegmentToItinerary(parameters, secondRequest, result, secondJourneyState);
					WriteCJPLogMessage("Second journey result object added to itinerary manager.");

					// Test for failure. 
					continueProcess = !(result.CJPMessages.GetLength(0) > 0);
				}


				// JOURNEY 3 - Build 3rd request then call CJP
				// (Optional journey - only required if returning to origin)
				if (continueProcess)
				{
					if (parameters.ReturnToOrigin)
					{
						ITDJourneyRequest thirdRequest = itineraryManager.BuildNextRequest(parameters);

						result = CallCJP(
							originalState.RequestID,
							thirdRequest,
							sessionInfo.SessionId,
							partition,
							sessionInfo.IsLoggedOn, 
							sessionInfo.UserType, 
							sessionInfo.Language, 
							sessionInfo.OriginAppDomainFriendlyName);
				
						TDJourneyState thirdJourneyState = new TDJourneyState();
						// Add this journey to the itinerary
						itineraryManager.AddSegmentToItinerary(parameters, thirdRequest, result, thirdJourneyState);
						WriteCJPLogMessage("Third journey result object added to itinerary manager.");

						continueProcess = !(result.CJPMessages.GetLength(0) > 0);

					}
				}


				// Compare state data objects to see if the user has started to plan a new 
				// journey. If these are the same the itinerary manager MUST be saved as it
				// contained either jorneys or error information.
				AsyncCallState newState = (AsyncCallState)ser.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);

				if (newState.RequestID == originalState.RequestID)
				{
					// Save the itinerary manager back to deferred storage.
					ser.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyItineraryManager, itineraryManager);
					WriteCJPLogMessage("Itinerary manager saved.");

					// We then examine the continueProcess flag. If this is true then we can update
					// the state data to say that the journey planning was succesful.
					if (continueProcess)
					{
						originalState.Status = AsyncCallStatus.CompletedOK;
						ser.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState, originalState);
						WriteCJPLogMessage("Original State Data object saved.");
					}
				}
			}

			catch (Exception e)
			{
				// A completely unexpected exception has occurred. Log this so it doesn't get completely
				// lost, then try and update the state data.
				StringBuilder message = new StringBuilder();
				bool updatedState;

				// Attempt to update the AsyncCallState.
				updatedState = SetStateToError(ser, sessionInfo, partition);

				// Generate error message for the log file.
				message.Append("Unexpected exception occurred in VisitPlanRunnerCaller.InvokeCJPForInitialItinerary. AsyncCallState update status: ");
				message.Append(updatedState.ToString());
				message.Append("\r\n");
				message.Append("Exception body:\r\n");
				message.Append(e.ToString());
				WriteCJPLogMessage(message.ToString());
			}
		}
		

		/// <summary>
		/// Invokes CJP to add journeys to an existing itinerary.
		/// </summary>
		/// <param name="sessionInfo">Users session information object</param>
		/// <param name="partition">Current partition object</param>
		private void InvokeCJPForAddJourneys(CJPSessionInfo sessionInfo, TDSessionPartition partition)
		{
			//General purpose de/serializer
			TDSessionSerializer ser = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);

			try
			{
				// Boolean to determine if processing will continue
				// after an error has been detected.
				bool continueProcess;

				//Other object declarations
				ITDJourneyRequest request;
				AsyncCallState originalState;
				IVisitPlannerItineraryManagerRemote itineraryManager;

				//Deserialise request object from the defferred storage.
				request = (ITDJourneyRequest)ser.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyJourneyRequest);

				originalState = (AsyncCallState)ser.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);
				itineraryManager = (IVisitPlannerItineraryManagerRemote)ser.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyItineraryManager);

				// Save state data back to database with a status of in progress.
				originalState.Status = AsyncCallStatus.InProgress;
				ser.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState, originalState);

				// Call CJP
				ITDJourneyResult result = CallCJP(
					originalState.RequestID, 
					request, 
					sessionInfo.SessionId, 
					partition, 
					sessionInfo.IsLoggedOn, 
					sessionInfo.UserType, 
					sessionInfo.Language, 
					sessionInfo.OriginAppDomainFriendlyName);
												
				// If the result object has CJP messages then a failure has occured. CallCJP will
				// have handled the failure conditions so here we need to stop processing.
				continueProcess = !(result.CJPMessages.GetLength(0) > 0);

				// Compare state data objects to see if the user has started to plan a new 
				// journey. If these are the same the itinerary manager MUST be saved as it
				// contained either jorneys or error information
				AsyncCallState newState = (AsyncCallState)ser.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);

				if (newState.RequestID == originalState.RequestID)
				{
					// Update the itinerary manager with the result object and save the 
					// itinerary manager back to deferred storage.
					itineraryManager.AddJourneysToSegment(result);
					ser.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyItineraryManager, itineraryManager);
					WriteCJPLogMessage("Itinerary manager saved.");

					// We then examine the continueProcess flag. If this is true then we can update
					// the state data to say that the journey planning was succesful.
					if (continueProcess)
					{
						originalState.Status = AsyncCallStatus.CompletedOK;
						ser.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState, originalState);
						WriteCJPLogMessage("CJP Call Status updated to CompletedOK and deffered.");
					}
				}
			}

			catch (Exception e)
			{
				// A completely unexpected exception has occurred. Log this so it doesn't get completely
				// lost, then try and update the state data.
				StringBuilder message = new StringBuilder();
				bool updatedState;

				// Attempt to update the AsyncCallState.
				updatedState = SetStateToError(ser, sessionInfo, partition);

				// Generate error message for the log file.
				message.Append("Unexpected exception occurred in VisitPlanRunnerCaller.InvokeCJPForInitialItinerary. AsyncCallState update status: ");
				message.Append(updatedState.ToString());
				message.Append("\r\n");
				message.Append("Exception body:\r\n");
				message.Append(e.ToString());
				WriteCJPLogMessage(message.ToString());
			}
		}


		/// <summary>
		/// Private method that handles the call to the CJP for the journey. This will
		/// return either a valid journey result if the call to the CJP is successfull or
		/// a null TDJourneyResult object will be returned. Error messages are written
		/// to the log file during this process if the TDTraceSwitch is verbose. A comparison
		/// of the original/result request ID's to see if the user has started planning
		/// another journey whilst this process has been running.
		/// </summary>
		/// <param name="cjpCallRequestID">Request ID</param>
		/// <param name="journeyRequest">Journey request</param>
		/// <param name="sessionID">Users session ID</param>
		/// <param name="partition">Parition</param>
		/// <param name="userIsLoggedOn">Is the user logged in?</param>
		/// <param name="userType">User type</param>
		/// <param name="lang">Current language</param>
		/// <param name="clientAppDomainFriendlyName">Client App. Domain Friendly Name</param>
		/// <returns>Journey result for planned journey or null if planning failed.</returns>
		private ITDJourneyResult CallCJP( Guid cjpCallRequestID, ITDJourneyRequest journeyRequest, string sessionID, TDSessionPartition partition, bool userIsLoggedOn, int userType, string lang, string clientAppDomainFriendlyName )
		{
			//Stores if a failure condition has occured
			bool failureCondition = false;

			// Indicate that we are NOT an SLA monitoring transaction
			bool referenceTransation = false;
				
			// For Visit Planning journey is not an extension (set for clarity on CallCJP method)
			bool isExtension = false;

			//Defered storage serializer
			TDSessionSerializer ser = new TDSessionSerializer(clientAppDomainFriendlyName);

			//Container for returned journey result. 
			ITDJourneyResult journeyResult = null;

			//Container for state data objeect.
			AsyncCallState oldState = null;
			AsyncCallState newState = null;

			try 
			{
				// Write a log message indicating the call is now starting and including the session ID
				WriteCJPLogMessage("CJP call now starting. Session ID = " + sessionID);

				// Get a CJP Manager from the service discovery
				ICJPManager cjpManager = (ICJPManager) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];

				// We need to do populate a state data object prior to the CJP call so that incase
				// something goes wrong with the actual call we can set the status to failed and this
				// will be reported back to the user.
				oldState = (AsyncCallState)ser.RetrieveAndDeserializeSessionObject(sessionID, partition, TDSessionManager.KeyAsyncCallState);

				WriteCJPLogMessage("Retrieved ORIGINAL AsyncCallState. CJP Call ID = " + oldState.RequestID.ToString()); 

				//Call CJP passing in requireds parameters.
				journeyResult = cjpManager.CallCJP(
					journeyRequest, 
					sessionID, 
					userType, 
					referenceTransation, 
					userIsLoggedOn, 
					lang, 
					isExtension);

				//Write message indicating that the CJP manager has returned reporting
				//the Session ID.
				WriteCJPLogMessage("CJPManager call returned.");

				//Retrieve journey plan state data and write message indicating that 
				//the journey plan state data has been retrieved from deferred storage.
				newState = (AsyncCallState)ser.RetrieveAndDeserializeSessionObject(sessionID, partition, TDSessionManager.KeyAsyncCallState);
				WriteCJPLogMessage("Retrieved NEW AsyncCallState. CJP Call ID = " + oldState.RequestID.ToString()); 
				
				//Check that request id has not changed by the planning of another jounrey.
				//This will mean that we overwrite the results anyway so these can be
				//discarded.
				if ( cjpCallRequestID == newState.RequestID )
				{
					//Set failure condition depedant on IsValid on the journeyResult object.
					failureCondition = !journeyResult.IsValid;

					//Write message indicating what the journey result status is.
					WriteCJPLogMessage("Journey Result Status is " + journeyResult.IsValid.ToString());

					//Add journeys to map hand off database.				
					ITDMapHandoff handoff = (ITDMapHandoff) TDServiceDiscovery.Current[ServiceDiscoveryKey.TDMapHandoff];
					handoff.SaveJourneyResult(journeyResult, sessionID);
				}
				else
				{	
					//The original request Id and the returned one do not match.
					//Report an error. This is not a failure conidition hence we do
					//not want to update the stateData so say a problem has occured
					//as this will overwrite whats happening in the users current
					//request. We just null the journeyResult object so that the
					//calling method knows not to continue.
					WriteCJPLogMessage("Original request and returned request ID's do not match ORIGINAL = " + oldState.RequestID.ToString() + " NEW = " + newState.RequestID.ToString());
				}
			}
			
			catch (Exception e) 
			{
				//Write exception
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Exception: ", e, sessionID));
				failureCondition = true;
			}

			finally
			{
				//If a failure condition has been detected perform relevant actions.
				if (failureCondition)
				{
					//Only if we have a failure condition do we need to defer the plan state
					//object back to storage. This is to record the fact that a failure
					//has occured and we do not wish to continue journey planning.
					oldState.Status = AsyncCallStatus.NoResults;
					ser.SerializeSessionObjectAndSave(sessionID, partition, TDSessionManager.KeyAsyncCallState, oldState);
					WriteCJPLogMessage("Updated ORIGINAL state with CJPRequestID " + oldState.RequestID.ToString() + " to NO RESULTS");

					//In this scenario null the journey result so the calling method
					//knows that a problem has been detected.
					//journeyResult = null;
				}

				//Report process finished.
				WriteCJPLogMessage("Process Completed.");
			}

			//Return journey result object (doenst matter if its null now).
			return journeyResult;	
		}


		/// <summary>
		/// Private method to construct CJP error messages. Only writes message if
		/// TDTraceSwitch set to TraceVerbose
		/// </summary>
		/// <param name="message">String representation of error message.</param>
		private void WriteCJPLogMessage(string message)
		{
			if (TDTraceSwitch.TraceVerbose)
			{
				StringBuilder output = new StringBuilder();
				output.Append(processName);
				output.Append(" ");
				output.Append(message);
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, output.ToString()));
			}
		}


		/// <summary>
		/// Updates the async call state data to reflect an error has occured.
		/// </summary>
		/// <param name="serializer">Serialised to be used.</param>
		/// <param name="info">SessionInfo object</param>
		/// <param name="partition">Session partition to update</param>
		/// <returns>Boolean indicating update as successful or not</returns>
		private bool SetStateToError(TDSessionSerializer serializer, CJPSessionInfo info, TDSessionPartition partition)
		{
			try
			{
				if (serializer == null)
					return false;

				AsyncCallState state = serializer.RetrieveAndDeserializeSessionObject(info.SessionId, partition, TDSessionManager.KeyAsyncCallState) as AsyncCallState;
				if (state == null)
					return false;
			
				state.Status = AsyncCallStatus.NoResults;

				serializer.SerializeSessionObjectAndSave(info.SessionId, partition, TDSessionManager.KeyAsyncCallState, state);
				
				return true;
			}
			catch
			{
				// This code is already being called from an error handler, so don't do anything.
				return false;
			}
		}
	}
}