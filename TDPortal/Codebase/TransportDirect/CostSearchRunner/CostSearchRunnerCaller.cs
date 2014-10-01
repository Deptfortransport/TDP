// ***********************************************
// NAME 		: CostSearchRunnerCaller.cs
// AUTHOR 		: Russell Wilby
// DATE CREATED : 13/10/2005
// DESCRIPTION 	: A remotable class to implement the ICostSearchRunnerCaller interafce.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/CostSearchRunnerCaller.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:36   mturner
//Initial revision.
//
//   Rev 1.5   Jun 01 2006 17:35:58   rphilpott
//Fix race condtion between this class and Wait Page in Find Cheaper AssembleServices call. 
//Resolution for 4103: Find Cheaper - journeys not always returned.
//
//   Rev 1.4   Jan 16 2006 19:16:22   RPhilpott
//Code review fixes.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.3   Dec 22 2005 17:16:56   RWilby
//Removed unnecessary  Serializable attribute
//
//   Rev 1.2   Oct 28 2005 15:05:44   RWilby
//Updated for new ICostFacade
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 19 2005 15:37:46   RWilby
//Added MTAThread attribute to public methods
//
//   Rev 1.0   Oct 17 2005 13:37:40   RWilby
//Initial revision.
//Resolution for 2818: DEL 7.3 Stream: Search by Price

using System;
using System.Threading;
using System.Collections;
using Logger = System.Diagnostics.Trace;

using TransportDirect.UserPortal.CostSearch;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
	/// CostSearchRunnerCaller. A remotable class to implement the ICostSearchRunnerCaller interafce
	/// </summary>
	public class CostSearchRunnerCaller : MarshalByRefObject, ICostSearchRunnerCaller
	{
		#region delegates to the CallAssembleFares and CallAssembleServices methods

		public delegate void CallAssembleFaresCallback(CJPSessionInfo sessionInfo);
		public delegate void CallAssembleServicesCallback(CJPSessionInfo sessionInfo);

		#endregion

		private ICostSearchFacade costSearchFacade;
		private CostSearchResult costResult;
		private CostSearchRequest costRequest;
		
		/// <summary>
		/// Constructor
		/// </summary>
		public CostSearchRunnerCaller()
		{
		}

		
		#region ICostSearchRunnerCaller implementation

		/// <summary>
		/// Makes the call to the CostSearchFacade to look up services using the selected ticket(s)
		/// </summary>
		[MTAThread]
		public void CallAssembleServices(CJPSessionInfo sessionInfo)
		{
		
			//get current page state 
			TDSessionPartition partition = TDSessionPartition.CostBased;
			TDSessionSerializer deferManager = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);				
			FindCostBasedPageState savedPageState = (FindCostBasedPageState)deferManager.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyFindPageState);			

			//get instance of CostSearchFacade from service discovery 
			costSearchFacade = (ICostSearchFacade)TDServiceDiscovery.Current[ServiceDiscoveryKey.CostSearchFacade];	
			CostSearchTicket outwardTicket = null;
			CostSearchTicket inwardTicket = null;

			try
			{
				//get selected ticket(s) directly from the deferred session
				if (savedPageState.SelectedTravelDate.TravelDate.TicketType == TicketType.Singles) 
				{
					outwardTicket = savedPageState.SelectedOutwardTicket.CostSearchTickets[0];	
					inwardTicket = savedPageState.SelectedInwardTicket.CostSearchTickets[0];
				} 
				else 
				{
					outwardTicket = savedPageState.SelectedSingleOrReturnTicket.CostSearchTickets[0];								
				}
			}			
			catch (NullReferenceException  nRefEx)
			{
				//log error 
				string message = "Error in CostSearchRunnerCaller.CallAssembleServices method when accessing FindCostBasedPageState in TDSessionSerializer. Exception : " + nRefEx.ToString()
					+ Environment.NewLine + nRefEx.StackTrace;

				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message));	

				//return now, this will trigger a time out status
				return;			
			}

			//call assemble services
			try
			{
				if (TDTraceSwitch.TraceVerbose)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"Calling CostSearchFacade.AssembleServices - SessionId = " + sessionInfo.SessionId));
				}
				
				//update the existing result for Singles
				if (inwardTicket != null)
				{
					//update the existing result with service data
					costSearchFacade.AssembleServices(savedPageState.SearchRequest, savedPageState.SearchResult, outwardTicket, inwardTicket);
				}
					//update the existing result for any other Ticket Type 
				else
				{					
					//update the existing result passed in savedPageState.SearchResult with service data
					costSearchFacade.AssembleServices(savedPageState.SearchRequest, savedPageState.SearchResult, outwardTicket);
				}

				//control returns
				if (TDTraceSwitch.TraceVerbose )
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"CostSearchFacade has returned control to CostSearchRunnerCaller.CallAssembleServices - SessionId = " + sessionInfo.SessionId));
				}
			}
			catch (Exception ex) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "Exception in CostSearchRunnerCaller.CallAssembleServices method", ex));					
			}
			
			//update search status after successful call
			try
			{	
				//assign the journey request and result to the session manager, so that it is easily available for the UI
				TDJourneyRequest journeyRequest = null;
				TDJourneyResult journeyResult =null;
				if (inwardTicket != null) 
				{
					journeyResult = (TDJourneyResult)savedPageState.SearchResult.GetJourneyResultsForTicket(outwardTicket,inwardTicket);
					journeyRequest = (TDJourneyRequest)savedPageState.SearchResult.GetJourneyRequestForTicket(outwardTicket,inwardTicket);
				} 
				else 
				{
					journeyResult = (TDJourneyResult)savedPageState.SearchResult.GetJourneyResultsForTicket(outwardTicket);
					journeyRequest = (TDJourneyRequest)savedPageState.SearchResult.GetJourneyRequestForTicket(outwardTicket);
				}

				journeyResult.JourneyReferenceNumber = SqlHelper.GetRefNumInt();
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					string.Format("CostSearchRunnerCaller has assigned a JourneyReferenceNumber of {0} to TDJourneyResult created in CallAssembleServices()", journeyResult.JourneyReferenceNumber)));

				TDJourneyViewState viewState = new TDJourneyViewState();
				viewState.OriginalJourneyRequest = journeyRequest;

				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					"Starting serialisation - SessionId = " + sessionInfo.SessionId));

				deferManager.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyJourneyRequest, journeyRequest);
				deferManager.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyJourneyResult, journeyResult);
				deferManager.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyJourneyViewState, viewState);

				if (TDTraceSwitch.TraceVerbose) 
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"CostSearchRunnerCaller has updated the CostSearchResult in deferred storage  - SessionId = " + sessionInfo.SessionId));
				}

				//save the journey results to the ESRI MasterMap database
				ITDMapHandoff handoff = (ITDMapHandoff) TDServiceDiscovery.Current[ServiceDiscoveryKey.TDMapHandoff];
				handoff.SaveJourneyResult(journeyResult, sessionInfo.SessionId);

				//update search status and save to deferred storage
				AsyncCallState waitState = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);
				waitState.Status = AsyncCallStatus.CompletedOK;

				deferManager.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyFindPageState, savedPageState);
				deferManager.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState, waitState);

				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					"Updated page and async states - SessionId = " + sessionInfo.SessionId));

			}
			catch (Exception ex) 
			{
				//log error 
				string message = "Error in CostSearchRunnerCaller.CallAssembleServices method when updating TDSessionSerializerException : " + ex.ToString()
					+ Environment.NewLine + ex.StackTrace;
				
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message));			
			}
		}


		/// <summary>
		/// Asynchronously makes the call to the CostSearchFacade to look up services using the selected ticket(s)
		/// </summary>
		[MTAThread]
		public void CallAssembleServicesAsync(CJPSessionInfo sessionInfo)
		{
			//Asynchronously invoke services search via the CallAssembleServices method 
			CallAssembleServicesCallback callAssembleServicesCallback = new CallAssembleServicesCallback(CallAssembleServices);
			callAssembleServicesCallback.BeginInvoke(sessionInfo, null,null);
		}


		/// <summary>
		/// Makes the call to the CostSearchFacade to look up fares
		///</summary>
		[MTAThread]
		public void CallAssembleFares(CJPSessionInfo sessionInfo)
		{
			//get current page state 
			TDSessionPartition partition = TDSessionPartition.CostBased;
			TDSessionSerializer deferManager = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);
			FindCostBasedPageState savedPageState = (FindCostBasedPageState)deferManager.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyFindPageState);			
			AsyncCallState savedSearchState = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);			
		
			//the PublicJourneyStore will hold all the journeys separately from the fares information
			ArrayList outwardPublicJourneys = new ArrayList();
			ArrayList returnPublicJourneys = new ArrayList();

			try
			{
				//retrieve the saved cost request
				costRequest = savedPageState.SearchRequest;
			}
			catch (NullReferenceException  nRefEx)
			{
				//log error 
				string message = "Error in CostSearchRunnerCaller.CallAssembleFares method when accessing FindCostBasedPageState in TDSessionSerializer. Exception : " + nRefEx.ToString() 
					+ Environment.NewLine + nRefEx.StackTrace;

				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message));	

				//if no CostSearchRequest exists then there is a problem just return, this will trigger a time out
				return;
			}

			//get instance of CostSearchFacade from service discovery 
			costSearchFacade = (ICostSearchFacade)TDServiceDiscovery.Current[ServiceDiscoveryKey.CostSearchFacade];	
			
			try
			{				
				if (TDTraceSwitch.TraceVerbose)
				{					
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"Calling CostSearchFacade.AssembleFares - SessionId = " + sessionInfo.SessionId));
				}

				//call AssembleFares on the CostSearchFacade
				//this will return a CostSearchResult as well as populating the PublicJourneyStore out parameter
				costResult = (CostSearchResult)costSearchFacade.AssembleFares(costRequest);
				
				//control returns
				if (TDTraceSwitch.TraceVerbose)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"CostSearchFacade has returned control to CostSearchRunnerCaller.CallAssembleFares - SessionId = " + sessionInfo.SessionId));
				}

				//check that the result id is not null
				if (costResult.ResultId == Guid.Empty)  
				{
					if (TDTraceSwitch.TraceVerbose )
					{
						//if the Ids do not match then forget this result and try again
						Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
							"ResultId returned by CostSearchFacade is null. Discarding this result."));
					}

					//if no cost result at this point then return, this will trigger a time out
					return;								
				}
			}
			catch (Exception ex) 
			{
				//log error but do not rethrow - the code will continue and try the AssembleFares call again
				//if error keeps occurring the status will automatically become TimedOut
				string message = "Error in CostSearchRunnerCaller.CallAssembleFares method: ";
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message + ex.ToString() + Environment.NewLine + ex.StackTrace));					
			}				

			//check Guid of result and compare to the RequestID of the saved request			
			if (costResult.ResultId == savedSearchState.RequestID)
			{
				try
				{						
					//add CostSearchResult to the page state
					savedPageState = (FindCostBasedPageState)deferManager.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyFindPageState);
					savedPageState.SearchResult = costResult;
					savedPageState.FareDateTable = null;

					//update search status 
					savedSearchState = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);
					savedSearchState.Status = AsyncCallStatus.CompletedOK;				
				
					//save result and status to deferred storage
					deferManager.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyFindPageState, savedPageState);
					deferManager.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState, savedSearchState);

					//log the update
					if (TDTraceSwitch.TraceVerbose)
					{
						Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
							"CostSearchRunnerCaller has updated FindCostBasedPageState with a CostSearchResult - SessionId = " + sessionInfo.SessionId));
					}
				}							
				catch (Exception ex) 
				{
					//log error 
					string message = "Error in CostSearchRunnerCaller.CallAssembleFares method when updating TDSessionSerializer. Exception : " + ex.ToString();
					
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error, message + Environment.NewLine + ex.StackTrace));			
				}		
			}
				// if the request and result ids do not match, then ignore the result 
				// e.g. the user has used the Back button and then submitted another request
			else 
			{
				try
				{
					// log the problem and do not save the result
					string message = "ResultId returned by CostSearchFacade does not match the Request Id. ResultId = " + costResult.ResultId + 
						" RequestId = " + costRequest.RequestId + " Discarding this result.";
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error, message));	
				}
				catch (NullReferenceException  nRefEx)
				{
					//log error 
					string message = "Error in CostSearchRunnerCaller.CallAssembleFares method when accessing Result and Request IDs. Exception : " + nRefEx.ToString()
						+ Environment.NewLine + nRefEx.StackTrace;
					
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error, message));							
				}
			}			
		}		


		/// <summary>
		/// Asynchronously makes the call to the CostSearchFacade to look up fares
		///</summary>
		[MTAThread]
		public void CallAssembleFaresAsync (CJPSessionInfo sessionInfo)
		{
			//Asynchronously invoke services search via the CallAssembleFares method 
			CallAssembleFaresCallback callAssembleFaresCallback = new CallAssembleFaresCallback(CallAssembleFares);
			callAssembleFaresCallback.BeginInvoke(sessionInfo, null,null);	
		}
		#endregion
	}
}
