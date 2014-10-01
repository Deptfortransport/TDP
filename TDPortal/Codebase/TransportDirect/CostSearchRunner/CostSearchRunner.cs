// ************************************************************** 
// NAME			: CostSearchRunner.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Implementation of the CostSearchRunner class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/CostSearchRunner.cs-arc  $
//
//   Rev 1.2   Feb 02 2009 16:21:40   mmodi
//Include Routing Guide properties
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.1   May 22 2008 16:38:34   mmodi
//Changed check for a Valid location
//Resolution for 5002: Amend Find a train cost does not use the new location
//
//   Rev 1.0   Nov 08 2007 12:19:34   mturner
//Initial revision.
//
//   Rev 1.40   Nov 15 2005 20:53:24   RPhilpott
//Make AssembleFares asynchronous for rail if flexibility (out and/or return) is greater than zero.
//Resolution for 3037: DN040: User responsiveness of SBP fare requests
//
//   Rev 1.39   Nov 10 2005 17:53:34   RPhilpott
//Make Coach AssembleServices call asynchronous.
//
//   Rev 1.38   Nov 09 2005 12:23:50   build
//Automatically merged from branch for stream2818
//
//   Rev 1.37.1.1   Oct 17 2005 14:02:44   RWilby
//Updated for CostSearchRunner Architecture Changes
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.37.1.0   Oct 14 2005 15:08:16   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.37   Jul 26 2005 11:29:28   RBroddle
//SCR2601 - Amendment to invalid date logging code to ensure this runs on production site.
//
//   Rev 1.36   May 26 2005 14:05:08   RPhilpott
//Add stack trace to exception reporting so source of error can be traced.
//Resolution for 2546: PT costing - find-a-fare journeys not returned consistently
//
//   Rev 1.35   May 09 2005 17:04:30   tmollart
//Update to previous fix for conformance to coding standards.
//
//   Rev 1.34   May 09 2005 13:53:54   tmollart
//Added code to log when an error occurs with date valdiation as an operational event.
//Resolution for 2205: DEL 7.1: Invalid date error returned when planning journey
//
//   Rev 1.33   May 05 2005 16:46:58   jbroome
//Set the JourneyReferenceNumber properties of the TDJourneyResult objects that are created during the Cost Based Search process
//Resolution for 2414: Coach Find A fare: Selecting next day then one fare causes out of bound exception
//
//   Rev 1.32   Apr 29 2005 15:08:32   jmorrissey
//Update to ValidateAndRunServices. No need to update the existing result on the session when looking up services.
//Resolution for 2331: PT - Incorrect dates going back from fare selection
//
//   Rev 1.31   Apr 27 2005 16:39:46   jmorrissey
//Removed some unnecessary code in AssembleCoachFares method.
//
//   Rev 1.30   Apr 26 2005 10:57:34   jbroome
//Ensure inward journeys get assigned to correct collection
//
//   Rev 1.29   Apr 26 2005 09:46:28   jbroome
//Ensure inward journeys get assigned to correct collection
//
//   Rev 1.28   Apr 25 2005 17:48:24   jmorrissey
//Added new method AssembleCoachServices, because coach services are now added via this class rather than in the CostSearchFacade class.
//Resolution for 2290: Session data for cost based searching - coach
//
//   Rev 1.27   Apr 25 2005 12:02:08   COwczarek
//ValidateAndRunServices now creates TDJourneyViewState
//object and assigns journey request to it. This is then saved in
//session data.
//Resolution for 2306: Find a Fare - No date displayed on the journey summary page
//
//   Rev 1.26   Apr 21 2005 12:17:58   jmorrissey
//Update to PerformTravelModesValidations and PerformLocationValidations methods.
//Resolution for 2265: PT - Find a Fare - server error when origin is the same as destination
//
//   Rev 1.25   Apr 20 2005 16:16:16   tmollart
//Commented out air code so that it is not included in result of locations validation.
//Resolution for 2265: PT - Find a Fare - server error when origin is the same as destination
//
//   Rev 1.24   Apr 20 2005 13:49:56   jmorrissey
//In ValidateAndRunServices pass through return date time correctly for a return ticket. Also, reset the JourneyResult on the session corrcetly on any call to ValidateAndRunServices .
//
//   Rev 1.23   Apr 13 2005 18:06:00   RPhilpott
//Display location name in location validation error logging.
//
//   Rev 1.22   Apr 08 2005 12:38:08   jmorrissey
//In CallAssembleFares, set FareDateTable to null when a new result is being saved
//
//   Rev 1.21   Apr 06 2005 17:06:34   jmorrissey
//Now checks that the CostSearchResult.ResultId returned by the CostSearchFacade.AssembleFares is not null
//
//   Rev 1.20   Apr 05 2005 18:16:30   jmorrissey
//Ensured that SearchResult is never null on the session manager.
//
//   Rev 1.19   Apr 04 2005 09:09:52   jmorrissey
//Updated after further integration testing
//
//   Rev 1.18   Apr 01 2005 14:26:30   jmorrissey
//Now discards results correctly when the result id does not match the request id
//
//   Rev 1.17   Mar 29 2005 17:04:56   jmorrissey
//Updated after integartion testing
//
//   Rev 1.16   Mar 22 2005 11:01:20   jmorrissey
//Added overload of ValidateAndRunServices method. Changed how SearchResults are accessed on PageState. Updated method signature for CallAssembleServices.
//
//   Rev 1.15   Mar 15 2005 18:28:28   jmorrissey
//Updated to initialise local copies of errors for each call to ValidateAndRunFares
//
//   Rev 1.14   Mar 15 2005 17:33:46   jmorrissey
//Updated with code review actions
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.13   Mar 15 2005 08:34:30   tmollart
//Made changes to use the CostSearchWaitStateData directly on the session manager as opposed to the FindCoseBasedPageState object.
//
//   Rev 1.12   Mar 13 2005 17:43:06   jmorrissey
//Updated error message resource ids. Session manager now longer instantiated outside constructor.
//
//   Rev 1.11   Mar 10 2005 17:09:56   jmorrissey
//Added 'using TransportDirect.UserPortal.JourneyControl' so that it can use the CJPSessionInfo which has moved to this project
//
//   Rev 1.10   Mar 03 2005 11:30:56   jmorrissey
//Fixed bug in AdjustSearchDateRange method. Now handles no return date correctly.
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.9   Mar 01 2005 17:24:16   jmorrissey
//Updated exception identifiers
//
//   Rev 1.8   Mar 01 2005 16:43:48   jmorrissey
//Completed NUnit and Fxcop
//
//   Rev 1.7   Feb 22 2005 16:42:56   jmorrissey
//Added validation and new methods CallAssembleFares and CallAssembleServices
//
//   Rev 1.6   Feb 02 2005 12:15:18   jmorrissey
//More changes to CostSearchParams
//
//   Rev 1.5   Feb 01 2005 11:05:54   jmorrissey
//Updated after changes to CostSearchParams class
//
//   Rev 1.4   Jan 27 2005 12:29:00   jmorrissey
//FindCostBasedPageState now obtained via a cast from FindPageState, rather than a separate property on TDSessionManager
//
//   Rev 1.3   Jan 26 2005 10:35:46   jmorrissey
//Added SetSearchDates method
//
//   Rev 1.2   Jan 14 2005 15:20:08   jmorrissey
//Updated after change of CostSearchParams structure
//
//   Rev 1.1   Jan 12 2005 10:57:16   jmorrissey
//First versions of ValidateAndRunFares and ValidateAndRunServices. 
//
//   Rev 1.0   Dec 22 2004 12:14:14   jmorrissey
//Initial revision.

using System;
using System.Threading;
using System.Collections;
using System.Globalization;
using System.Text;

using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
	/// CostSearchRunner class used up UI code to look up fares and services
	/// </summary>
	public class CostSearchRunner : ICostSearchRunner
	{
		//private instances used by this class
		private ArrayList listErrors = new ArrayList();
		private Hashtable errorMessages = new Hashtable();
		private CostSearchRequest costRequest;
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
	
		/// <summary>
		/// default constructor
		/// </summary>
		public CostSearchRunner()
		{
						
		}

		/// <summary>
		///  validates the searchParams and looks up fares using the CostSearchFacade
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
				
	            // pass through the routing guide flags
                costRequest.RoutingGuideInfluenced = searchParams.RoutingGuideInfluenced;
                costRequest.RoutingGuideCompliantJourneysOnly = searchParams.RoutingGuideCompliantJourneysOnly;

				//adjust search date range according to user's date flexibility
				AdjustSearchDateRange(ref costRequest, searchParams);
				
				//get session info and add it to the request
				sessionInfo = TDSessionManager.Current.GetSessionInformation();
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
			
				//update session manager and explicitly save session data
				((FindCostBasedPageState)tdSessionManager.FindPageState).SearchRequest = costRequest;
				//add empty result at this point
				((FindCostBasedPageState)tdSessionManager.FindPageState).SearchResult = new CostSearchResult();
				tdSessionManager.SaveData();	

				// Need to clear out the session managers cache, 
				//otherwise we will not get the latest CostSearchWaitStateData from the ASPState database.
				tdSessionManager.ClearDeferredData();				
				
				//Obtain an instance of CostSearchRunnerCaller from TDServiceDiscovery
				ICostSearchRunnerCaller costSearchRunnerCaller = (ICostSearchRunnerCaller)
					TDServiceDiscovery.Current[ServiceDiscoveryKey.CostSearchRunnerCaller];

				//if modes include coach, or we searching for more than one day, then call CJP asynchronously
				if (findCoachFares || costRequest.OutwardFlexibilityDays > 0 || costRequest.InwardFlexibilityDays > 0)
				{
					//invoke async fare search
					costSearchRunnerCaller.CallAssembleFaresAsync(sessionInfo);
				}
				else
				{									
					//invoke fare search for single day rail 
					costSearchRunnerCaller.CallAssembleFares(sessionInfo);			
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
		/// looks up services for the selected ticket using the CostSearchFacade 
		/// </summary>
		public AsyncCallStatus ValidateAndRunServices(CostSearchTicket selectedTicket)
		{
			//pass the selected ticket onto overloaded method
			return ValidateAndRunServices(selectedTicket, null);					
		}

		/// <summary>
		/// looks up services for the selected ticket(s) using the CostSearchFacade 
		/// </summary>
		public AsyncCallStatus ValidateAndRunServices(CostSearchTicket outwardTicket, CostSearchTicket inwardTicket)
		{
			//retrieve the current session manager, which will be a cost based partition of the session manager			
			tdSessionManager = TDSessionManager.Current;	

			//get existing request from the session, this is needed to look up services				
			FindCostBasedPageState pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
			CostSearchRequest existingRequest = pageState.SearchRequest;		

			//upodate the CostSearchWaitStateData on the session
			tdSessionManager.AsyncCallState.Status = AsyncCallStatus.InProgress;

			//clear the existing result on the session
			tdSessionManager.JourneyResult = new TDJourneyResult();

			//update session manager 
			tdSessionManager.SaveData();	
			
			// Need to clear out the session managers cache, otherwise we will not get the latest
			// CostSearchWaitStateData from the database.
			tdSessionManager.ClearDeferredData();

			//get session info
			sessionInfo = TDSessionManager.Current.GetSessionInformation();

			//Obtain an instance of CostSearchRunnerCaller from TDServiceDiscovery
			ICostSearchRunnerCaller costSearchRunnerCaller = (ICostSearchRunnerCaller)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.CostSearchRunnerCaller];

			costSearchRunnerCaller.CallAssembleServicesAsync(sessionInfo);	

			//return the current state now i.e InProgress. It is responsibility of 
			//calling code to check the session manager again to see when the status has changed to CompletedOK
			return tdSessionManager.AsyncCallState.Status;	
		}

		/// <summary>
		/// Makes the call to the CostSearchFacade to look up fares 
		/// </summary>


		/// <summary>
		/// Validates the cost search parameters 
		/// </summary>
		/// <param name="searchParams"></param>
		private void PerformTravelModesValidations(ref CostSearchParams searchParams)
		{
			//if no travel modes boxes were selected, then use all travel modes for the search
			if ((searchParams.TravelModesParams == null) || (searchParams.TravelModesParams.Length == 0))
			{	
				searchParams.TravelModesParams = new TicketTravelMode[2];
				searchParams.TravelModesParams[0] = TicketTravelMode.Coach;
				searchParams.TravelModesParams[1] = TicketTravelMode.Rail;
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
		
			catch (Exception ex)
			{

				//Vantive 3577108 - Add log entry to aid investigation
				StringBuilder logRtnMsg = new StringBuilder();
				logRtnMsg.Append(ex.GetType().Name + " " + ex.ToString() + " " + Environment.NewLine + ex.StackTrace + Environment.NewLine);
				logRtnMsg.Append("Invalid Outward Date Time: " + costSearchParams.OutwardDayOfMonth + "/" + costSearchParams.OutwardMonthYear + " " + costSearchParams.OutwardHour + ":" + costSearchParams.OutwardMinute);
				logRtnMsg.Append(", Journey from " + costSearchParams.Origin.SearchType + " " + costSearchParams.Origin.InputText);
				logRtnMsg.Append(" To " + costSearchParams.Destination.SearchType + " " + costSearchParams.Destination.InputText);
				logRtnMsg.Append(", Return Date Time: " + costSearchParams.ReturnDayOfMonth + "/" + costSearchParams.ReturnMonthYear + " " + costSearchParams.ReturnHour + ":" + costSearchParams.ReturnMinute);
				logRtnMsg.Append(", Culture = " + Thread.CurrentThread.CurrentCulture);
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logRtnMsg.ToString()));

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
				catch (Exception ex)
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

					//Vantive 3577108 - Add log entry to aid investigation
					StringBuilder logRtnMsg = new StringBuilder();
					logRtnMsg.Append(ex.GetType().Name + " " + ex.ToString() + " " + Environment.NewLine + ex.StackTrace + Environment.NewLine);
					logRtnMsg.Append("Invalid Return Date Time: " + costSearchParams.ReturnDayOfMonth + "/" + costSearchParams.ReturnMonthYear + " " + costSearchParams.ReturnHour + ":" + costSearchParams.ReturnMinute);
					logRtnMsg.Append(", Journey from " + costSearchParams.Origin.SearchType + " " + costSearchParams.Origin.InputText);
					logRtnMsg.Append(" To " + costSearchParams.Destination.SearchType + " " + costSearchParams.Destination.InputText);
					logRtnMsg.Append(", Outward Date Time: "+ costSearchParams.OutwardDayOfMonth + "/" + costSearchParams.OutwardMonthYear + " " + costSearchParams.OutwardHour + ":" + costSearchParams.OutwardMinute);
					logRtnMsg.Append(", Culture = " + Thread.CurrentThread.CurrentCulture);
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,logRtnMsg.ToString()));
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
			if (fromLocation.Status != TDLocationStatus.Valid)
			{
				if (TDTraceSwitch.TraceVerbose)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"CostSearchRunner.PerformLocationValidations error. No origin location specified."));
				}					

				//add error to session manager
				SetValidationError(ValidationErrorID.OriginLocationInvalid, INVALID_ORIGIN_LOCATION);					
			}

			//check if destination location has been specified
			if ( toLocation.Status != TDLocationStatus.Valid )
			{
				if (TDTraceSwitch.TraceVerbose)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
						"CostSearchRunner.PerformLocationValidations error. No destination location specified."));
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

			//temp array list - this will hold only those travel modes that pass the following validation
			ArrayList costSearchModes = new ArrayList();
			
			//indicates which modes are selected 
			bool railSelected = false;
			bool coachSelected = false;
			
			//populate array list of travel modes with the modes that the user has selected
			for (int i = 0; i < searchParams.TravelModesParams.Length; i++)
			{
				if (searchParams.TravelModesParams[i] == TicketTravelMode.Rail)
				{
					costSearchModes.Add(TicketTravelMode.Rail);
					railSelected = true;
				}
				if (searchParams.TravelModesParams[i] == TicketTravelMode.Coach)
				{
					costSearchModes.Add(TicketTravelMode.Coach);
					coachSelected = true;
				}
			}

			//check if NO naptans were found for the selected locations
			//This should never really happen but check just in case
			bool hasNaptans = false;			

			//check that rail naptans exist for the locations selected
			if (railSelected)
			{
				if (fromLocation.ContainsNaptansForStationType(StationType.Rail) && toLocation.ContainsNaptansForStationType(StationType.Rail))
				{
					hasNaptans = true;
				}												   				
				else
				{
					//remove rail mode from search modes if no rail naptans found for the locations
					costSearchModes.Remove(TicketTravelMode.Rail);	
				}
			}

			//check that coach naptans exist for the locations selected
			if (coachSelected)
			{
				if (fromLocation.ContainsNaptansForStationType(StationType.Coach) && toLocation.ContainsNaptansForStationType(StationType.Coach))
				{
					hasNaptans = true;
				}												   				
				else
				{
					//remove coach mode from search modes if no coach naptans found for the locations
					costSearchModes.Remove(TicketTravelMode.Coach);	
				}							
			}
						
			//validation fails if no naptans found for any of the the selected modes
			if (hasNaptans == false)
			{					
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					"No naptans found for origin and/or destination locations in Find A Fare. Origin location: " + 
					fromLocation.Description + " Destination location: " + toLocation.Description ));

				//add error to session manager
				SetValidationError(ValidationErrorID.NoValidRoutes, NO_VALID_ROUTES);				
				return;
			}

			//bool indicates if selected locations are overlapping, initially set this to TRUE 
			//and then it can be set to false by any mode that has non-overlapping locations
			bool overlappingLocations = true;

			//check that rail naptans exist for the locations selected
			if (railSelected)
			{
				//check origin and destination locations for overlapping rail stations
				if (fromLocation.Intersects(toLocation,StationType.Rail))
				{
					//remove rail mode from array list if the origin and destination rail naptans overlap
					costSearchModes.Remove(TicketTravelMode.Rail);									
				}
				else
				{
					overlappingLocations = false;	
				}		
			}

			//check that coach naptans exist for the locations selected
			if (coachSelected)
			{
				//check origin and destination locations for overlapping coach stations 
				if (fromLocation.Intersects(toLocation,StationType.Coach)) 
				{
					//remove coach mode from array list if the origin and destination coach naptans overlap
					costSearchModes.Remove(TicketTravelMode.Coach);									
				}
				else
				{
					overlappingLocations = false;	
				}					
			}
				
			//validation only fails if ALL selected modes are overlapping			
			if (overlappingLocations)
			{					
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					"Overlapping origin and destination locations in Find A Fare. Origin location: " + 
					searchParams.OriginLocation.Description + " Destination location: " + searchParams.DestinationLocation.Description ));

				//add error to session manager
				SetValidationError(ValidationErrorID.OriginAndDestinationOverlap,  ORIGIN_AND_DESTINATION_OVERLAP);	
				return;
			}
			
			//reset the searchParams to use only travel modes that have not been removed by the previous checks
			searchParams.TravelModesParams = (TicketTravelMode[])costSearchModes.ToArray(typeof(TicketTravelMode));
			
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
