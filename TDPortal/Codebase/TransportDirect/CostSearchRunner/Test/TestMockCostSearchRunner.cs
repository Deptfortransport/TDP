// ******************************************************************** 
// NAME			: TestMockCostSearchRunner.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 04/01/2005 
// DESCRIPTION	: Implementation of the TestMockCostSearchRunner class
// // This is a mock class available for UI testing purposes.
// ******************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/Test/TestMockCostSearchRunner.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:44   mturner
//Initial revision.
//
//   Rev 1.23   Nov 09 2005 12:23:54   build
//Automatically merged from branch for stream2818
//
//   Rev 1.22.1.1   Oct 28 2005 16:54:28   RWilby
//Updated for CostSearchFacade refactoring
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.22.1.0   Oct 14 2005 15:08:50   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.22   Apr 25 2005 17:45:54   jmorrissey
//Change after design update to CostSearchFacade.AssembleFares method
//Resolution for 2290: Session data for cost based searching - coach
//
//   Rev 1.21   Apr 25 2005 12:02:42   COwczarek
//ValidateAndRunServices now creates TDJourneyViewState
//object and assigns journey request to it. This is then saved in
//session data.
//Resolution for 2306: Find a Fare - No date displayed on the journey summary page
//
//   Rev 1.20   Apr 25 2005 11:48:04   COwczarek
//Assign return date in request for non-singles fares search
//Resolution for 2191: PT - Correct dates not being used to obtain services for selected ticket
//
//   Rev 1.19   Apr 20 2005 10:58:20   RPhilpott
//Change in handling of CostSearchErrors.
//Resolution for 2193: PT - Messages returned by cost search back end will not be displayed
//
//   Rev 1.18   Apr 03 2005 18:30:00   jmorrissey
//Updated CallAssembleServices
//
//   Rev 1.17   Apr 03 2005 14:58:28   COwczarek
//Obtain selected ticket from correct ticket collection
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.16   Apr 01 2005 14:24:18   jmorrissey
//Updated with latest changes made to real CostSearchRunner
//
//   Rev 1.15   Mar 29 2005 09:52:40   COwczarek
//Changes to ensure consistent update of
//FindCostBasedPageState object during
//serialization/deserialization of session
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.14   Mar 22 2005 11:02:02   jmorrissey
//Added overload of ValidateAndRunServices method. Changed how SearchResults are accessed on PageState. Updated method signature for CallAssembleServices.
//
//   Rev 1.13   Mar 21 2005 10:23:12   COwczarek
//Changes to make ValidateAndRunServices update page state
//object graph as a whole and not update cost result seperately (problems occuring after deserialization and objects which were equal with == become duplicated)
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.12   Mar 15 2005 18:28:06   jmorrissey
//Updated to initialise local copies of errors for each call to ValidateAndRunFares
//
//   Rev 1.11   Mar 15 2005 16:11:06   jmorrissey
//Updated after code review
//
//   Rev 1.10   Mar 15 2005 08:34:32   tmollart
//Made changes to use the CostSearchWaitStateData directly on the session manager as opposed to the FindCoseBasedPageState object.
//
//   Rev 1.9   Mar 13 2005 17:43:34   jmorrissey
//Updated error message resource ids. Session manager now longer instantiated outside constructor.
//
//   Rev 1.8   Mar 11 2005 15:24:12   COwczarek
//Revove reference to session manager in constructor to allow
//service discovery to instantiate this object
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.7   Mar 11 2005 15:01:50   rhopkins
//Fix handling of null TravelModeParams
//
//   Rev 1.6   Mar 10 2005 17:10:14   jmorrissey
//Added 'using TransportDirect.UserPortal.JourneyControl' so that it can use the CJPSessionInfo which has moved to this project
//
//   Rev 1.5   Mar 03 2005 11:31:52   jmorrissey
//Fixed bug in AdjustSearchDateRange method. Now handles no return dates correctly.
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.4   Mar 01 2005 17:24:06   jmorrissey
//Updated exception identifiers
//
//   Rev 1.3   Mar 01 2005 16:45:22   jmorrissey
//Completed NUnit and Fxcop
//
//   Rev 1.2   Feb 22 2005 16:41:34   jmorrissey
//Added validation
//
//   Rev 1.1   Feb 02 2005 12:15:26   jmorrissey
//More changes to CostSearchParams
//
//   Rev 1.0   Feb 01 2005 10:59:16   jmorrissey
//Initial revision.

using System;
using System.Threading;
using System.Collections;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.CostSearchRunner
{	
	/// <summary>
	/// TestMockCostSearchRunner is a mock version of TestMockCostSearchRunner that calls a 
	/// mock version of the CostSearchFacade rather than the real CostSearchFacade
	/// </summary>
	public class TestMockCostSearchRunner : ICostSearchRunner
	{
		//private instances used by this class
		private ArrayList listErrors = new ArrayList();
		private Hashtable errorMessages = new Hashtable();
		private ICostSearchFacade costSearchFacade;
		private CostSearchRequest costRequest;
		private CostSearchResult costResult;
		private ITDSessionManager tdSessionManager;	
		private CJPSessionInfo sessionInfo;		

		#region Constants

		protected const string OUTWARD_AND_RETURN_DATE_NOT_VALID = "PerformDateValidations.OutwardAndReturnDateNotValid";
		protected const string OUTWARD_DATE_NOT_VALID = "PerformDateValidations.OutwardDateNotValid";
		protected const string RETURN_DATE_NOT_VALID = "PerformDateValidations.ReturnDateNotValid";
		protected const string OUTWARD_DATE_IS_AFTER_RETURN_DATE = "PerformDateValidations.OutwardDateIsAfterReturnDate";
		protected const string OUTWARD_DATE_IS_IN_THE_PAST = "PerformDateValidations.OutwardDateIsInThePast";
		protected const string RETURN_DATE_IS_IN_THE_PAST = "PerformDateValidations.ReturnDateIsInThePast";
		protected const string OUTWARD_AND_RETURN_DATES_IN_THE_PAST = "PerformDateValidations.OutwardAndReturnDatesInThePast";
		protected const string RETURN_MONTH_MISSING = "PerformDateValidations.ReturnMonthMissing";
		protected const string RETURN_DATE_MISSING = "PerformDateValidations.ReturnDateMissing";
		protected const string ORIGIN_AND_DESTINATION_OVERLAP = "PerformLocationValidations.OriginAndDestinationOverlap";
		protected const string NO_VALID_ROUTES = "PerformLocationValidations.NoValidRoutes";
		protected const string INVALID_ORIGIN_LOCATION = "PerformLocationValidations.InvalidOriginLocation";
		protected const string INVALID_DESTINATION_LOCATION = "PerformLocationValidations.InvalidDestinationLocation";

		#endregion		

		#region delegates

		//delegates to the CallAssembleFares and CallAssembleServices methods
		public delegate void CallAssembleFaresCallback(string sessionId);
		public delegate void CallAssembleServicesCallback(string sessionId);

		#endregion
	
		/// <summary>
		/// default constructor
		/// </summary>
		public TestMockCostSearchRunner()
		{
						
		}
        
		/// <summary>
		///  validates the searchParams and looks up fares using the TestMockCostSearchFacade
		/// </summary>
		public AsyncCallStatus ValidateAndRunFares(CostSearchParams searchParams )
		{
			//retrieve the current session manager, which will be a cost based partition of the session manager
			tdSessionManager = TDSessionManager.Current;	

			//initialise session manager validation errors 
			tdSessionManager.ValidationError = new ValidationError();
			listErrors = new ArrayList();
			errorMessages = new Hashtable();

			//initialise search state  			
			CostBasedFaresSearchState searchState = (CostBasedFaresSearchState)tdSessionManager.AsyncCallState;
			Guid cjpCallRequestID = Guid.NewGuid();
			searchState.RequestID = cjpCallRequestID;
			searchState.Status = AsyncCallStatus.None;
			
			//TDDateTime outward and return object
			TDDateTime outwardDate = null;
			TDDateTime returnDate = null;	

			//check search dates
			PerformDateValidations(searchParams, ref outwardDate, ref returnDate);

			//check travel modes
			PerformTravelModesValidations(ref searchParams);

			//check locations
			PerformLocationValidations(searchParams);

			//convert the arraylist to array of ValidationErrorIDs and save to session manager
			tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
			tdSessionManager.ValidationError.MessageIDs = errorMessages;
			
			//check if searchParams have validated ok 
			if (tdSessionManager.ValidationError.ErrorIDs.Length == 0)
			{
				//if no validation errors then populate a CostSearchRequest 				
				costRequest = new CostSearchRequest();
				costRequest.RequestId = cjpCallRequestID;
				costRequest.CoachDiscountedCard = searchParams.CoachDiscountedCard;
				costRequest.DestinationLocation = searchParams.DestinationLocation;
				costRequest.InwardFlexibilityDays = searchParams.InwardFlexibilityDays;				
				costRequest.OriginLocation = searchParams.OriginLocation;
				costRequest.OutwardDateTime = outwardDate;
				costRequest.OutwardFlexibilityDays = searchParams.OutwardFlexibilityDays;
				costRequest.RailDiscountedCard = searchParams.RailDiscountedCard;
				costRequest.ReturnDateTime = returnDate;
				costRequest.ReturnDestinationLocation = searchParams.OriginLocation;
				costRequest.ReturnOriginLocation = searchParams.DestinationLocation;
				costRequest.TravelModes = searchParams.TravelModesParams;					

				//adjust search date range according to user's date flexibility
				AdjustSearchDateRange(ref costRequest, searchParams);
				
				//get session info and add it to the request
				sessionInfo = new CJPSessionInfo();
				sessionInfo.SessionId = tdSessionManager.Session.SessionID;
				sessionInfo.Language = Thread.CurrentThread.CurrentUICulture.ToString();
				sessionInfo.IsLoggedOn = tdSessionManager.Authenticated;
				sessionInfo.UserType = 	sessionInfo.IsLoggedOn ? (int)tdSessionManager.CurrentUser.UserType : (int)TDUserType.Standard;
				costRequest.SessionInfo = sessionInfo;
				
				//check travel modes to see if request includes coach modes
				bool findCoachFares = false;
				for (int i = 0; i < costRequest.TravelModes.Length; i++)
				{
					if (costRequest.TravelModes[i] == TicketTravelMode.Coach)
					{
						findCoachFares = true;
					}
				}	
	
				//before calling AssembleFares set status of search to InProgress 
				//No need to explicitly set the expiry date time for the searchState 
				//because it is set automatically according to the status
				searchState.Status = AsyncCallStatus.InProgress;	
			
				((FindCostBasedPageState)tdSessionManager.FindPageState).SearchRequest = costRequest;
				tdSessionManager.SaveData();	

				// Need to clear out the session managers cache, 
				//otherwise we will not get the latest CostSearchWaitStateData from the ASPState database.
				tdSessionManager.ClearDeferredData();				
				
				//if modes include coach then call CJP asynchronously
				if (findCoachFares)
				{
					//assign a delegate to the CallAssembleFares method
					CallAssembleFaresCallback callAssembleFaresCallback = new CallAssembleFaresCallback(CallAssembleFares);

					//invoke fare search
					callAssembleFaresCallback.BeginInvoke(costRequest.SessionInfo.SessionId, null,null);								
				}
				//if modes do not include coach then call is synchronous
				else
				{									
					//invoke fare search for non coach modes
					CallAssembleFares(costRequest.SessionInfo.SessionId);						
				}							
			} 
			else 
			{
				//if validation has failed then update session manager status accordingly
				tdSessionManager.AsyncCallState.Status = AsyncCallStatus.ValidationError;
				tdSessionManager.SaveData();
			}

			//return the current state now i.e InProgress or ValidationError. 
			return tdSessionManager.AsyncCallState.Status;
		}

		/// <summary>
		/// looks up services for the selected ticket using the TestMockCostSearchFacade 
		/// </summary>
		public AsyncCallStatus ValidateAndRunServices(CostSearchTicket selectedTicket)
		{
			//pass the selected ticket onto overloaded method
			return ValidateAndRunServices(selectedTicket, null);					
		}
		
		/// <summary>
		/// looks up services for the selected ticket(s) using the TestMockCostSearchFacade 
		/// </summary>
		public AsyncCallStatus ValidateAndRunServices(CostSearchTicket outwardTicket, CostSearchTicket inwardTicket)
		{
			//retrieve the current session manager, which will be a cost based partition of the session manager			
			tdSessionManager = TDSessionManager.Current;	

			//get existing request from the session, this is needed to look up services				
			FindCostBasedPageState pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
			CostSearchRequest existingRequest = pageState.SearchRequest;
					
			//set the mode for based on the outward ticket (if inward ticket exists it will be the same mode)
			for (int i = 0; i < existingRequest.TravelModes.Length; i++)
			{
				existingRequest.TravelModes[i] = outwardTicket.TravelDateForTicket.TravelMode;	
			}				
			
			//because the selected tickets' travel dates can be changed via the UI before searching for services, 
			//the corresponding request properties must be updated 
			if (inwardTicket == null)
			{
				existingRequest.OutwardDateTime = outwardTicket.TravelDateForTicket.OutwardDate;
                existingRequest.ReturnDateTime = outwardTicket.TravelDateForTicket.ReturnDate;			
			}
			else
			{
				existingRequest.OutwardDateTime = outwardTicket.TravelDateForTicket.OutwardDate;	
				existingRequest.ReturnDateTime = inwardTicket.TravelDateForTicket.OutwardDate;	
			}			

			//update session manager 
			((FindCostBasedPageState)tdSessionManager.FindPageState).SearchRequest = existingRequest; 
			tdSessionManager.AsyncCallState.Status = AsyncCallStatus.InProgress;
			tdSessionManager.SaveData();	
			
			// Need to clear out the session managers cache, otherwise we will not get the latest
			// CostSearchWaitStateData from the database.
			tdSessionManager.ClearDeferredData();

			//if selected ticket is for rail mode then attach a delegate to the CallAssembleServices method
			if (outwardTicket.TravelDateForTicket.TravelMode == TicketTravelMode.Rail)
			{			
				//Asynchronously invoke services search via the CallAssembleServices method 
				CallAssembleServicesCallback callAssembleServicesCallback = new CallAssembleServicesCallback(CallAssembleServices);
				callAssembleServicesCallback.BeginInvoke(existingRequest.SessionInfo.SessionId, null,null);
			}
				//if selected ticket is not for rail then call CallAssembleServices synchronously
			else				
			{				
				CallAssembleServices(existingRequest.SessionInfo.SessionId);					
			}

			//return the current state now i.e InProgress. It is responsibility of 
			//calling code to check the session manager again to see when the status has changed to CompletedOK
			return tdSessionManager.AsyncCallState.Status;	
		}	

		/// <summary>
		/// Makes the call to the TestMockCostSearchFacade to look up fares 
		/// </summary>
        private void CallAssembleFares(string sessionId)
        {
			//get current page state 
			TDSessionPartition partition = TDSessionPartition.CostBased;
			TDSessionSerializer deferManager = new TDSessionSerializer();
			FindCostBasedPageState savedPageState = (FindCostBasedPageState)deferManager.RetrieveAndDeserializeSessionObject(sessionId, partition, TDSessionManager.KeyFindPageState);			
			AsyncCallState savedSearchState = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionId, partition, TDSessionManager.KeyAsyncCallState);			

			try
			{
				//retrieve the saved cost request
				costRequest = savedPageState.SearchRequest;
			}
			catch (NullReferenceException  nRefEx)
			{
				//log error 
				string message = "Error in TestMockCostSearchRunner.CallAssembleFares method when accessing FindCostBasedPageState in TDSessionSerializer. Exception : " + nRefEx.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message));	

				//if no CostSearchRequest exists then there is a problem just return, this will trigger a time out
				return;
			}

			//get instance of TestMockCostSearchFacade  
			costSearchFacade = new TestMockCostSearchFacade();
			
			try
			{				
				if (TDTraceSwitch.TraceVerbose)
				{					
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"Calling TestMockCostSearchFacade.AssembleFares - SessionId = " + sessionId));
				}


				costResult = (CostSearchResult)costSearchFacade.AssembleFares(costRequest);
				
				//control returns
				if (TDTraceSwitch.TraceVerbose )
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"TestMockCostSearchFacade has returned control to TestMockCostSearchRunner.CallAssembleFares - SessionId = " + sessionId));
				}

				//check that the result is not null
				if (costResult == null)  
				{
					if (TDTraceSwitch.TraceVerbose )
					{
						//if the Ids do not match then forget this result and try again
						Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
							"ResultId returned by TestMockCostSearchFacade is null. Discarding this result."));
					}

					//if no cost result at this point then return, this will trigger a time out
					return;								
				}
			}
			catch (Exception ex) 
			{
				//log error but do not rethrow - the code will continue and try the AssembleFares call again
				//if error keeps occurring the status will automatically become TimedOut
				string message = "Error in TestMockCostSearchRunner.CallAssembleFares method: ";
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message + ex.Message));					
			}				

			//check Guid of result and compare to the RequestID of the saved search state			
			if (costResult.ResultId == savedSearchState.RequestID)
			{
				try
				{						
					//add CostSearchResult to the page state
					savedPageState = (FindCostBasedPageState)deferManager.RetrieveAndDeserializeSessionObject(sessionId, partition, TDSessionManager.KeyFindPageState);
					savedPageState.SearchResult = costResult;

					//update search status 
					savedSearchState = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionId, partition, TDSessionManager.KeyAsyncCallState);
					savedSearchState.Status = AsyncCallStatus.CompletedOK;				
				
					//save result and status to deferred storage
					deferManager.SerializeSessionObjectAndSave(sessionId, partition, TDSessionManager.KeyFindPageState, savedPageState);
					deferManager.SerializeSessionObjectAndSave(sessionId, partition, TDSessionManager.KeyAsyncCallState, savedSearchState);
				
					//log the update
					if (TDTraceSwitch.TraceVerbose)
					{
						Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
							"TestMockCostSearchRunner has updated FindCostBasedPageState with a CostSearchResult - SessionId = " + sessionId));
					}
				}							
				catch (Exception ex) 
				{
					//log error 
					string message = "Error in TestMockCostSearchRunner.CallAssembleFares method when updating TDSessionSerializer. Exception : " + ex.Message;
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error, message));			
				}		
			}
				// if the request and result ids do not match, then ignore the result 
				// e.g. the user has used the Back button and then submitted another request
			else 
			{
				try
				{
					// log the problem and do not save the result
					string message = "ResultId returned by TestMockCostSearchFacade does not match the Request Id. ResultId = " + costResult.ResultId + 
						" RequestId = " + costRequest.RequestId + " Discarding this result.";
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error, message));	
				}
				catch (NullReferenceException  nRefEx)
				{
					//log error 
					string message = "Error in TestMockCostSearchRunner.CallAssembleFares method when accessing Result and Request IDs. Exception : " + nRefEx.Message;
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error, message));							
				}
			}			
		}		

		/// <summary>
		/// Makes the call to the TestMockCostSearchFacade to look up services using the selected ticket(s)
		/// </summary>
		private void CallAssembleServices(string sessionId)
		{
			//get current page state 
			TDSessionPartition partition = TDSessionPartition.CostBased;
			TDSessionSerializer deferManager = new TDSessionSerializer();				
			FindCostBasedPageState savedPageState = (FindCostBasedPageState)deferManager.RetrieveAndDeserializeSessionObject(sessionId, partition, TDSessionManager.KeyFindPageState);			

            //get instance of TestMockCostSearchFacade 
			costSearchFacade = new TestMockCostSearchFacade();	

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
				string message = "Error in TestMockCostSearchRunner.CallAssembleServices method when accessing FindCostBasedPageState in TDSessionSerializer. Exception : " + nRefEx.Message;
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
						"Calling TestMockCostSearchFacade.AssembleServices - SessionId = " + sessionId));
				}
				
				//update the existing result for Singles
				if (inwardTicket != null)
				{
					//update the existing result passed in savedPageState.SearchResult with service data
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
						"TestMockCostSearchFacade has returned control to TestMockCostSearchRunner.CallAssembleServices - SessionId = " + sessionId));
				}
			}
			catch (Exception ex) 
			{
				//log error 
				string message = "Error in TestMockCostSearchRunner.CallAssembleServices method. Exception : " + ex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message));					
			}
			
			//update search status after successful call
			try
			{				
				//update search status 
				AsyncCallState savedSearchState = (AsyncCallState)deferManager.RetrieveAndDeserializeSessionObject(sessionId, partition, TDSessionManager.KeyAsyncCallState);			
				savedSearchState.Status = AsyncCallStatus.CompletedOK;

				//save to deferred storage
				deferManager.SerializeSessionObjectAndSave(sessionId, partition, TDSessionManager.KeyFindPageState, savedPageState);
				deferManager.SerializeSessionObjectAndSave(sessionId, partition, TDSessionManager.KeyAsyncCallState, savedSearchState);

				//assign the journey request and result to the session manager, so that it is easily available for the UI
				TDJourneyRequest journeyRequest = null;
				TDJourneyResult journeyResult = null;
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

                TDJourneyViewState viewState = new TDJourneyViewState();
                viewState.OriginalJourneyRequest = journeyRequest;

                deferManager.SerializeSessionObjectAndSave(sessionId, partition, TDSessionManager.KeyJourneyRequest, journeyRequest);
				deferManager.SerializeSessionObjectAndSave(sessionId, partition, TDSessionManager.KeyJourneyResult, journeyResult);
                deferManager.SerializeSessionObjectAndSave(sessionId, partition, TDSessionManager.KeyJourneyViewState, viewState);
                
                // Simulate some error cases (as defined in DD/57)

                // Error cases 1,2 for services for fare processing
                if (outwardTicket.Code.StartsWith("Greenline")) 
                {
                    savedPageState.SearchResult.AddError(new CostSearchError("CostSearchError.ServicesInternalError"));
                }

				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					"TestMockCostSearchRunner has updated the CostSearchResult in deferred storage  - SessionId = " + sessionId));

			}
			catch (Exception ex) 
			{
				//log error 
				string message = "Error in TestMockCostSearchRunner.CallAssembleServices method when updating TDSessionSerializerException : " + ex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message));	
			}
		}

		/// <summary>
		/// Validates the cost search parameters 
		/// </summary>
		/// <param name="searchParams"></param>
		private void PerformTravelModesValidations(ref CostSearchParams searchParams)
		{
			//if no travel modes boxes were selected, then use all travel modes for the search
			if ((searchParams.TravelModesParams == null) || (searchParams.TravelModesParams.Length == 0))
			{	
				searchParams.TravelModesParams = new TicketTravelMode[3];
				searchParams.TravelModesParams[0] = TicketTravelMode.Air;
				searchParams.TravelModesParams[1] = TicketTravelMode.Coach;
				searchParams.TravelModesParams[2] = TicketTravelMode.Rail;
			}	
		}	

		/// <summary>
		/// Validates the outward and return dates
		/// </summary>		
		/// <param name="tdJourneyParameters">The CostSearchParams</param>				
		private void PerformDateValidations(CostSearchParams costSearchParams, ref TDDateTime outwardDate, ref TDDateTime returnDate)
		{
			//dates and times used for calculations and adjustments
			TDDateTime dateTimeNow = TDDateTime.Now;			
			TDDateTime dateToday = DateTime.Today;			
			string currentTime = dateTimeNow.Hour + ":" + dateTimeNow.Minute;
			string returnTime = "00:00";

			//validation errors
			string msgResourceID = string.Empty;
			ValidationErrorID validationErrorID;	
						
			//check the outward date 
			try
			{
				//parse the date
				outwardDate = TDDateTime.Parse(costSearchParams.OutwardDayOfMonth + " " + costSearchParams.OutwardMonthYear, Thread.CurrentThread.CurrentCulture);
				
				//if outward date is today, then ensure it is not in the past by adding in the current time
				if (outwardDate.Equals(dateToday))
				{	
					outwardDate = TDDateTime.Parse(costSearchParams.OutwardDayOfMonth + " " + costSearchParams.OutwardMonthYear + " " + currentTime, Thread.CurrentThread.CurrentCulture);
				}
			}			
			catch (FormatException)
			{
				//validation error if the outward date is not a valid date				
				SetValidationError(ValidationErrorID.OutwardDateTimeInvalid, OUTWARD_DATE_NOT_VALID);
			}
			catch (ArgumentNullException)
			{
				//validation error if the outward date is not a valid date				
				SetValidationError(ValidationErrorID.OutwardDateTimeInvalid, OUTWARD_DATE_NOT_VALID);
			}

			//is there a return date?
			if  (costSearchParams.ReturnMonthYear != Enum.GetName(typeof(ReturnType), ReturnType.NoReturn)
				&& costSearchParams.ReturnMonthYear != Enum.GetName(typeof(ReturnType), ReturnType.OpenReturn))
			{	
				try
				{
					//check the return date 					
					returnDate = TDDateTime.Parse(costSearchParams.ReturnDayOfMonth + " " + costSearchParams.ReturnMonthYear + " " + returnTime, Thread.CurrentThread.CurrentCulture );
					//if return date is today, then ensure it is not in the past by adding in the current time
					if (returnDate.Equals(dateToday))
					{							
						returnDate = TDDateTime.Parse(costSearchParams.ReturnDayOfMonth + " " + costSearchParams.ReturnMonthYear + " " + currentTime, Thread.CurrentThread.CurrentCulture);
					}
				}
				catch (FormatException)
				{					
					// month entered, but no day selected
					if(costSearchParams.ReturnDayOfMonth.Length == 0) 
					{
						msgResourceID = RETURN_DATE_MISSING;
						validationErrorID = ValidationErrorID.ReturnDateMissing;
					}
					else 
					{
						//if both the outward AND return dates are invalid...
						if(errorMessages.ContainsValue(OUTWARD_DATE_NOT_VALID))
						{
							//replace the error message to indicate that both dates are not valid
							errorMessages.Remove(ValidationErrorID.OutwardDateTimeInvalid);
							listErrors.Remove(ValidationErrorID.OutwardDateTimeInvalid);
							msgResourceID = OUTWARD_AND_RETURN_DATE_NOT_VALID;
							validationErrorID = ValidationErrorID.OutwardAndReturnDateTimeInvalid;
						}
						else
						{
							//if only the return date is not valid
							msgResourceID = RETURN_DATE_NOT_VALID;
							validationErrorID = ValidationErrorID.ReturnDateTimeInvalid;
						}
					}
					SetValidationError(validationErrorID, msgResourceID);
				}

				// If return date is provided and valid, it must be later than outward
				if (returnDate != null && outwardDate != null)
				{
					if (outwardDate > returnDate)
					{						
						SetValidationError(ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime, 
							OUTWARD_DATE_IS_AFTER_RETURN_DATE);
					}
				}
			}
				// month selection was 'no return' check that date was entered correctly
			else 
			{
				// a day was specified but no month
				if(costSearchParams.ReturnDayOfMonth.Length != 0) 
				{
					SetValidationError(ValidationErrorID.ReturnMonthMissing, RETURN_MONTH_MISSING);					
				}				
			}

			// Check that the outward date is not before today's date
			if  (outwardDate != null)
			{
				if  (outwardDate < dateToday)
				{
					//validation error if the outward date is not before today's date			
					SetValidationError(ValidationErrorID.OutwardDateTimeNotLaterThanNow, 
						OUTWARD_DATE_IS_IN_THE_PAST);

				}				
			}
			// Check that the return date is later than dateTimeNow
			if (returnDate != null)
			{
				// Check that the optional return date is not before today's date
				if  (returnDate < dateToday)
				{
					//change error if both outward and return dates are in the past
					if (errorMessages.ContainsKey(ValidationErrorID.OutwardDateTimeNotLaterThanNow))
					{
						errorMessages.Remove(ValidationErrorID.OutwardDateTimeNotLaterThanNow);
						listErrors.Remove(ValidationErrorID.OutwardDateTimeNotLaterThanNow);
						msgResourceID = OUTWARD_AND_RETURN_DATES_IN_THE_PAST;
						validationErrorID = ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow;
					}
						//only the return date is in the past
					else
					{
						msgResourceID = RETURN_DATE_IS_IN_THE_PAST;
						validationErrorID = ValidationErrorID.ReturnDateTimeNotLaterThanNow;
					}

					SetValidationError(validationErrorID, msgResourceID);
				}
			}			
		}

		/// <summary>
		/// Sets the search dates for the request, according to the dates that the user 
		/// has selected and the flexibility that they have specified.
		/// Note that this method always gets called AFTER the dates have been successfully validated
		/// by the PerformDateValidations method.
		/// </summary>
		/// <param name="?"></param>
		/// <param name="searchParams"></param>
		private void AdjustSearchDateRange(ref CostSearchRequest costRequest, CostSearchParams searchParams)
		{
			//set variable to today's date
			TDDateTime dateTimeNow = TDDateTime.Now;

			//subtract the flexibility days from the OutwardDateTime to get the start date
			//for the outward search 
			TimeSpan outwardTimeSpan = new TimeSpan(searchParams.OutwardFlexibilityDays,0,0,0);
			TDDateTime outwardStart = costRequest.OutwardDateTime.Subtract(outwardTimeSpan);

			//if the outwardStart is in the past then adjust it to today's date
			if  (outwardStart < dateTimeNow)
			{
				outwardStart = dateTimeNow;
			}
			costRequest.SearchOutwardStartDate = outwardStart;

			//add the flexibility days to the OutwardDateTime to get the end date
			//for the outward search 
			TDDateTime outwardEnd = costRequest.OutwardDateTime.AddDays(searchParams.OutwardFlexibilityDays);
			costRequest.SearchOutwardEndDate = outwardEnd;

			//only check return flexibility if there is a return date 
			if (costRequest.ReturnDateTime != null)
			{
				//subtract the flexibility days from the ReturnDateTime to get the start date
				//for the return search 
				TimeSpan returnTimeSpan = new TimeSpan(searchParams.InwardFlexibilityDays,0,0,0);
				TDDateTime returnStart = costRequest.ReturnDateTime.Subtract(returnTimeSpan);
			
				//if the returnStart is in the past then adjust it to today's date
				if  (returnStart < dateTimeNow)
				{
					returnStart = dateTimeNow;
				}
				costRequest.SearchReturnStartDate = returnStart;

				//add the flexibility days to the ReturnDateTime to get the end date
				//for the return search 
				TDDateTime returnEnd = costRequest.ReturnDateTime.AddDays(searchParams.InwardFlexibilityDays);
				costRequest.SearchReturnEndDate = returnEnd;

				//check and adjust overlapping search periods. periods can overlap but 2 scenarios are invalid :
				//1. the SearchOutwardStartDate is greater than the SearchOutwardStartDate
				//2. the SearchOutwardEndDate is greater than the SearchReturnEndDate
				if (costRequest.SearchOutwardStartDate > costRequest.SearchReturnStartDate)
				{
					//reduce the range of the return search
					costRequest.SearchReturnStartDate = costRequest.SearchOutwardStartDate;
				}
				if (costRequest.SearchOutwardEndDate > costRequest.SearchReturnEndDate)
				{
					//adjust the end of the outward search
					costRequest.SearchOutwardEndDate = costRequest.SearchReturnEndDate;
				}
			}
		}

		/// <summary>
		/// validates the OriginLocation and DestinationLocation in the CostSearchParams and  
		/// removes any travel modes if the naptans are overlapping for that mode
		/// </summary>
		private void PerformLocationValidations(CostSearchParams searchParams)
		{
			//origin and destination locations
			TDLocation fromLocation = searchParams.OriginLocation ;
			TDLocation toLocation = searchParams.DestinationLocation;	

			//check if origin location has been specified			
			if (fromLocation.Status == TDLocationStatus.Unspecified )
			{
				if (TDTraceSwitch.TraceVerbose)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"TestMockCostSearchRunner.PerformLocationValidations error. No origin location specified."));
				}					

				//add error to session manager
				SetValidationError(ValidationErrorID.OriginLocationInvalid, INVALID_ORIGIN_LOCATION);					
			}

			//check if destination location has been specified
			if ( toLocation.Status == TDLocationStatus.Unspecified )
			{
				if (TDTraceSwitch.TraceVerbose)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"TestMockCostSearchRunner.PerformLocationValidations error. No destination location specified."));
				}					

				//add error to session manager
				SetValidationError(ValidationErrorID.DestinationLocationInvalid, INVALID_DESTINATION_LOCATION);					
			}
			
			//if either location is invalid then return now				
			if ((errorMessages.ContainsValue(INVALID_ORIGIN_LOCATION)) || 
				(errorMessages.ContainsValue(INVALID_DESTINATION_LOCATION)))
			{
				return;
			}
			
	
			//bools to indicate if no naptans were found			
			bool noRailNaptans = false;
			bool noCoachNaptans = false;
			bool noAirNaptans = false;

			//bools to indicate if overlaps were found			
			bool railOverlaps = false;
			bool coachOverlaps = false;
			bool airOverlaps = false;

			//temp array list - this will hold only those travel modes that we can use
			ArrayList costSearchModes = new ArrayList();
			
			//populate our temp array list of travel modes with the modes that the user has selected
			for (int i = 0; i < searchParams.TravelModesParams.Length; i++)
			{
				costSearchModes.Add(searchParams.TravelModesParams[i]);
			}

			// Remove mode type if there are no naptans found for it
			if (!fromLocation.ContainsNaptansForStationType(StationType.Airport) || !toLocation.ContainsNaptansForStationType(StationType.Airport))
			{
				noAirNaptans = true;
				costSearchModes.Remove(TicketTravelMode.Air);
			}
			if (!fromLocation.ContainsNaptansForStationType(StationType.Rail) || !toLocation.ContainsNaptansForStationType(StationType.Rail))
			{
				noRailNaptans = true;
				costSearchModes.Remove(TicketTravelMode.Rail);
			}
			if (!fromLocation.ContainsNaptansForStationType(StationType.Coach) || !toLocation.ContainsNaptansForStationType(StationType.Coach))
			{
				noCoachNaptans = true;
				costSearchModes.Remove(TicketTravelMode.Coach);
			}

			//validation fails if no naptans found for any modes
			if (noAirNaptans && noRailNaptans && noCoachNaptans)
			{					
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					"No naptans found for origin and destination locations in Find A Fare. Origin location: " + 
					fromLocation + " Destination location: " + toLocation ));

				//add error to session manager
				SetValidationError(ValidationErrorID.NoValidRoutes, NO_VALID_ROUTES);				
				return;
			}

			//check origin and destination locations for overlapping airports 
			if (costSearchModes.Contains(TicketTravelMode.Air) && fromLocation.Intersects(toLocation,StationType.Airport)) 
			{
				//remove airport mode from array list if found
				costSearchModes.Remove(TicketTravelMode.Air);		
				airOverlaps = true;					
			}				

			//check origin and destination locations for overlapping coach stations 
			if (costSearchModes.Contains(TicketTravelMode.Coach) && fromLocation.Intersects(toLocation,StationType.Coach)) 
			{
				//remove coach mode from array list if found
				costSearchModes.Remove(TicketTravelMode.Coach);					
				coachOverlaps = true;					
			}				

			//check origin and destination locations for overlapping rail stations
			if (costSearchModes.Contains(TicketTravelMode.Rail) && fromLocation.Intersects(toLocation,StationType.Rail)) 
			{
				//remove rail mode from array list if found
				costSearchModes.Remove(TicketTravelMode.Rail);					
				railOverlaps = true;					
			}
						
			//validation fails if ALL modes are overlapping
			if (airOverlaps && coachOverlaps && railOverlaps)
			{					
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					"Overlapping origin and destination locations in Find A Fare. Origin location: " + 
					searchParams.OriginLocation + " Destination location: " + searchParams.DestinationLocation ));

				//add error to session manager
				SetValidationError(ValidationErrorID.OriginAndDestinationOverlap,  ORIGIN_AND_DESTINATION_OVERLAP);				
				
			}
			else
			{
				//reset the searchParams to use only those travel modes that we know do not 
				//have overlapping naptans				
				searchParams.TravelModesParams = (TicketTravelMode[])costSearchModes.ToArray(typeof(TicketTravelMode));
			}
		}

		/// <summary>
		/// Adds a validation error to list of errors.
		/// This list gets added to the session manager at the end of the ValidateParams method.
		/// </summary>
		/// <param name="tdSessionManager"></param>
		/// <param name="validationErrorID"></param>
		/// <param name="msgResourceID"></param>
		/// <param name="showErrors"></param>
		private void SetValidationError(ValidationErrorID validationErrorID, string msgResourceID)
		{	
			//add error to array list
			listErrors.Add(validationErrorID);

			//adds error to hashtable
			if	(!errorMessages.ContainsKey(validationErrorID))
			{
				errorMessages.Add(validationErrorID, msgResourceID);
			}	
		}
	}
}
