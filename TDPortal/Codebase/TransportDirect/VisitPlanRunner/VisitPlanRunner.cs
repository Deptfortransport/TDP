// *********************************************************
// NAME			: VisitPlanRunner.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 06/09/2005
// DESCRIPTION	: Visit Plan Runner performs validation of
//				  journey paramters and invokes actual visit
//				  plan running.
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/VisitPlanRunner/VisitPlanRunner.cs-arc  $
//
//   Rev 1.1   Jan 20 2013 16:26:28   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.0   Nov 08 2007 12:51:10   mturner
//Initial revision.
//
//   Rev 1.6   Nov 09 2005 18:57:16   RPhilpott
//Merge with stream2818
//
//   Rev 1.5   Nov 09 2005 15:02:04   tmollart
//Changes for code review. Fixed bug which meant that state data was getting overwritten.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Nov 01 2005 13:10:38   tmollart
//Updated with comments from Code Review.
//
//   Rev 1.3   Oct 12 2005 11:28:22   asinclair
//Fixed minor bug and set CJPCallStatus to InProgress
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 10 2005 17:58:28   tmollart
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
using System.Threading;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ServiceDiscovery;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.VisitPlanRunner
{
	/// <summary>
	/// VisitPlannerRunner is responsible for the actual running of an 
	/// actually visit planner journey.
	/// </summary>
	public class VisitPlannerRunner
	{

		#region Declarations and Constants

		/// <summary>
		/// Array list of error messages from validation process
		/// </summary>
		private ArrayList listErrors = new ArrayList();

		/// <summary>
		/// Hash table of error messages from the validation process. Includes
		/// ID of the error message and a resource key to the error message.
		/// </summary>
		private Hashtable errorMessages = new Hashtable();

		/// <summary>
		/// Indicates if a none location validation error has been found.
		/// </summary>
		private bool foundNonLocationValidationError = false;

		// Error message constants
		// NOTE: The error messages defined below are shared with JourneyPlanRunner.
		private const string OUTWARD_DATE_NOT_VALID = "ValidateAndRun.DateNotValid";
		private const string DATE_TIME_IS_IN_THE_PAST = "ValidateAndRun.DateTimeIsInThePast";
		private const string AMBIGUOUS_FROM_TO_VIA_ALTERNATE = "ValidateAndRun.FromToViaAlternativeAmbiguous";
		private const string NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS = "ValidateAndRun.NoMatchForFromToViaAlternativeAndOtherErrors";
		private const string NO_MATCH_FROM_TO_VIA_ALTERNATE = "ValidateAndRun.NoMatchForFromToViaAlternative";

		// NOTE: The following error messages are specific to VistPlanRunner
		private const string VISIT_LOCATIONS_OVERLAP = "VisitPlanRunner.VisitLocationsOverlap";

		#endregion
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public VisitPlannerRunner()
		{
		}


		/// <summary>
		/// Peforms validation of journey parameters and if valid creates and
		/// plans an itinerary. Journey params are accessed through reference
		/// to session manager that is passed in.
		/// </summary>
		/// <param name="SessionManager">Current session manager instance</param>
		/// <returns>Boolean indicating success/failure</returns>
		public bool ValidateAndRunInitialItinerary(ITDSessionManager sessionManager)
		{

			// Get parameters from session manager.
			TDJourneyParametersVisitPlan parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;

			// 1. Check that each visit plan location is valid.
			PerformLocationValidations(parameters);

			// 2. Ensure that locations are not overlapping
			CheckForLocationsOverlapping(parameters);

			// 3. Perform date validations.
			PerformDateValidations(parameters);

			// 4. Check that at least one public transport mode has been specified. If
			// not then all modes are used.
			PeformModeValidations(parameters);

			// 5. Transfer any generated error messages back onto session manager.
			// sessionManager.ValidationError.ErrorIDs = er
			sessionManager.ValidationError = new ValidationError();
			sessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));			
			sessionManager.ValidationError.MessageIDs = errorMessages;

			// 6. Check if there are any errors present. If not continue to plan the journey. If there
			// are then return false from this method.
			if (listErrors.Count == 0)
			{
				//Get new state data object (pre-initialised) and save session data.
				sessionManager.AsyncCallState.Status = AsyncCallStatus.InProgress;
				sessionManager.SaveData();

				//Get an instance of VisitPlanRunnerCaller from Service Discovery.
				IVisitPlanRunnerCaller caller = (IVisitPlanRunnerCaller)TDServiceDiscovery.Current[ServiceDiscoveryKey.VisitPlanRunnerCaller];
				caller.RunInitialItinerary(sessionManager.GetSessionInformation(), sessionManager.Partition);

				return true;
			}
			else
			{
				return false;
			}	
		}


		/// <summary>
		/// Performs planning of earlier/later journeys and adding of those
		/// journeys to an itinerary segment. ExtendJourneyResultEarlier/Later
		/// to generate a TDJourneyResult object.
		/// </summary>
		/// <param name="SessionManager">Current session manager instance</param>
		public void RunAddJourneys(ITDSessionManager sessionManager)
		{
			//Get an instance of VisitPlanRunnerCaller from Service Discovery.
			IVisitPlanRunnerCaller caller = (IVisitPlanRunnerCaller)TDServiceDiscovery.Current[ServiceDiscoveryKey.VisitPlanRunnerCaller];
			caller.RunAddJourneys(sessionManager.GetSessionInformation(), sessionManager.Partition);
		}


		/// <summary>
		/// Performs date validations in the Visit Plan journey parameters.
		/// Ensures that the user has specified a date, the date is valid
		/// and that it is not in the past. Erros are recorded directly
		/// onto the
		/// </summary>
		/// <param name="parameters">Visit Plan Parameters</param>
		internal void PerformDateValidations(TDJourneyParametersVisitPlan parameters)
		{

			// IMPORTANT NOTE:
			// Difference here to other runners is that there is no anytime option
			// for visit planning so there are no checks in regard to this.
			TDDateTime visitDateTime = null;
			StringBuilder tempDateTime = new StringBuilder();

			// If the user has not selcted a time in the drop down box then the
			// contents at this point will be "-". This will be picked up by
			// the try/catch block below. In the mean time if the user has entered
			// an hour but not minutes, preset the minutes to "00".
			if (parameters.OutwardHour.Length != 0 && (parameters.OutwardMinute.Length == 0 || parameters.OutwardMinute.Equals("-")))
			{
				parameters.OutwardMinute = "00";
			}

			try
			{
				//Build up the date and time.
				int day     = int.Parse(parameters.OutwardDayOfMonth);
				int month   = int.Parse(parameters.OutwardMonthYear.Substring(0,2));
				int year    = int.Parse(parameters.OutwardMonthYear.Substring(3,4));
				int hour    = int.Parse(parameters.OutwardHour);
				int minutes = int.Parse(parameters.OutwardMinute);

				// Parse the time to see if there are any errors. This is performed in a try/catch block
				// and an exception is caught to determine any problems.

				visitDateTime = new TDDateTime(year, month, day, hour, minutes, 0, 0);					
			}
			catch (Exception ex)
			{
				// Set class level variable to indicate a non location validation error 
				// has been found. This is basically to control the type of error message
				// that will be displayed.
				foundNonLocationValidationError = true;

				//Update hash table and array list with the error message.
				SetValidationError(ValidationErrorID.OutwardDateTimeInvalid, OUTWARD_DATE_NOT_VALID);

				// Log exception details.
				if (TDTraceSwitch.TraceVerbose)
				{
					StringBuilder msg = new StringBuilder();
					msg.Append("VisitPlanRunner.PerformDateValidations failed to convert date ");
					msg.Append(parameters.OutwardDayOfMonth);
					msg.Append("/");
					msg.Append(parameters.OutwardMonthYear);
					msg.Append(" ");
					msg.Append(parameters.OutwardHour);
					msg.Append(":");
					msg.Append(parameters.OutwardMinute);
					msg.Append(" into a TDDateTimeObject. Exception reported: ");
					msg.Append(ex.Message.ToString());

					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, msg.ToString()));
				}
				return;
			}

			// Operations from this point onwards are on a valid (parsed) date/time

			// Check that the parsed date is not in the past
			if (visitDateTime < TDDateTime.Now)
			{
				foundNonLocationValidationError = true;
				SetValidationError(ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow, DATE_TIME_IS_IN_THE_PAST);
			}
		}



		/// <summary>
		/// Validates the modes of the Visit Plan. If no modes are selected
		/// it is assumed that the user intended to select all modes
		/// </summary>
		/// <param name="parameters"></param>
		internal void PeformModeValidations(TDJourneyParametersVisitPlan parameters)
		{
			//If there are no modes then add them all!
			if (parameters.PublicModes.Length == 0)
			{
				parameters.PublicModes = new ModeType[] { ModeType.Air, ModeType.Bus, ModeType.Coach, ModeType.Ferry, 
                    ModeType.Metro, ModeType.Rail, ModeType.Telecabine, ModeType.Tram, ModeType.Underground};
			}
		}

		
		/// <summary>
		/// Checks to see if there are any overlapping locations. Checks using naptans which will
		/// mean that only stations and all stops will be checked. Non naptan locations do not
		/// need to be checked for overlapping as they use coordinate based searches. We only
		/// provide a generic error message saying some of the locations are overlapping as opposed
		/// to specific error messages when we do the same thing for door to door.
		/// </summary>
		/// <param name="parameters">Visit Plan Parameters</param>
		internal void CheckForLocationsOverlapping(TDJourneyParametersVisitPlan parameters)
		{
			// Location objects
			TDLocation from = parameters.GetLocation(0);
			TDLocation visit1 = parameters.GetLocation(1);
			TDLocation visit2 = parameters.GetLocation(2);

			//Temporary variables
			bool overlapDetected = false;

			// If any of the locations are invalid then exit without doing anything else
			if (from.Status != TDLocationStatus.Valid || visit1.Status != TDLocationStatus.Valid || visit2.Status != TDLocationStatus.Valid)
			{
				return;
			}

			// As we only provide a generic error message that doesnt specify exact overlapping locations
			// we just need to detect an overlap in any of the locations. The only point to note is that
			// if the user has specified to "Return to origin" then we need to check that visit1 and origin dont
			// overlap or if visit2 has been specified that that and origin dont overlap.


			// 1. Check origin location and visit location 1
			if (from.Intersects(visit1, StationType.Undetermined))
			{
				overlapDetected = true;
			}

			// 2. If visit2 is specified then make sure that this doesnt overlap with
			// visit1.
			if (visit2.Status != TDLocationStatus.Unspecified)
			{
				if (visit1.Intersects(visit2, StationType.Undetermined))
				{
					overlapDetected = true;
				}
			}

			// 3. Perform checks if the user is returning to the origin.
			if (parameters.ReturnToOrigin)
			{
				
				if (visit2.Status == TDLocationStatus.Unspecified)
				{
					//If visit2 is unspecified then check that visit1 and origin dont overlap.
					if  (visit1.Intersects(from, StationType.Undetermined))
					{
						overlapDetected = true;
					}
				}
				else
				{
					//Visit2 is specified and the user is returning to the origin location.
					//Check that visit2 and origin are not overlapping.
					if (visit2.Intersects(from, StationType.Undetermined))
					{
						overlapDetected = true;
					}
				}
			}

			// If an ovelap has been detected report generic error message.
			if (overlapDetected)
			{
				//Write error message to the log file.
				StringBuilder error = new StringBuilder();
				error.Append("There are overlapping visit planner locations.");
				error.Append("Locations: ");
				error.Append(from);
				error.Append(" ");
				error.Append(visit1);
				error.Append(" ");
				error.Append(visit2);
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, error.ToString()));

				//Create a validation error.
				SetValidationError(ValidationErrorID.VisitLocationsOverlap, VISIT_LOCATIONS_OVERLAP);
			}
		}


		/// <summary>
		/// Perform validation of locations. This method will check that all locations
		/// are valid. Any errors found will be added to the error message hash table
		/// which can then be reported to the user. Locations are firstly checked for
		/// ambiguity and then to see if they have been specified (where they are
		/// required).
		/// </summary>
		/// <param name="locations">Array of location objects</param>
		internal void PerformLocationValidations(TDJourneyParametersVisitPlan parameters)
		{

			// IMPORTANT NOTE ON NAPTAN VALIDATION
			// Naptan validation is not part of the checks performed for visit planner. This is
			// because we no longer do naptan searches on anything other than "All Stops" and
			// "Station/Airport". These types of locations have naptans as default anyway. Other
			// searches are now performed by grid reference.

			// IMPORTANT NOTE ON DIFFERENCES BETWEEN THIS AND JOURNEY PLAN RUNNER
			// Journey plan runner checks the easting and northing of each location. This was
			// original introduced to check the quality of data and is no longer required and
			// therefore is not implemented in this class.
			
			// 1. Check locations for ambiguity

			// Check whether the Origin Location is ambiguous
			if (parameters.GetLocation(0).Status == TDLocationStatus.Ambiguous)
			{
				SetValidationError(ValidationErrorID.AmbiguousOriginLocation, AMBIGUOUS_FROM_TO_VIA_ALTERNATE);
				foundNonLocationValidationError = true;
			}

			// Check first visit location for ambiguity.
			if (parameters.GetLocation(1).Status == TDLocationStatus.Ambiguous)
			{
				SetValidationError(ValidationErrorID.AmbiguousVisitLocation1, AMBIGUOUS_FROM_TO_VIA_ALTERNATE);
				foundNonLocationValidationError = true;
			}

			// Check second visit location for ambiguity.
			if (parameters.GetLocation(2).Status == TDLocationStatus.Ambiguous)
			{
				SetValidationError(ValidationErrorID.AmbiguousVisitLocation2, AMBIGUOUS_FROM_TO_VIA_ALTERNATE);
				foundNonLocationValidationError = true;
			}


			// 2. Check locations for validity

			// Check if origin location is valid.
			if (parameters.GetLocation(0).Status == TDLocationStatus.Unspecified)
			{
				if (foundNonLocationValidationError)
				{
					SetValidationError(ValidationErrorID.OriginLocationInvalidAndOtherErrors, NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS);
				}
				else
				{
					SetValidationError(ValidationErrorID.OriginLocationInvalid, NO_MATCH_FROM_TO_VIA_ALTERNATE);
				}
			}

			// Check first visit location is valid.
			if (parameters.GetLocation(1).Status == TDLocationStatus.Unspecified)
			{
				if (foundNonLocationValidationError)
				{
					SetValidationError(ValidationErrorID.VisitLocation1InvalidAndOtherErrors, NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS);
				}
				else
				{
					SetValidationError(ValidationErrorID.VisitLocation1Invalid, NO_MATCH_FROM_TO_VIA_ALTERNATE);
				}
			}

			// Check second visit location is valid. 
			if (parameters.GetLocation(2).Status == TDLocationStatus.Unspecified)
			{
				if (foundNonLocationValidationError)
				{
					SetValidationError(ValidationErrorID.VisitLocation2InvalidAndOtherErrors, NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS);
				}
				else
				{
					SetValidationError(ValidationErrorID.VisitLocation2Invalid, NO_MATCH_FROM_TO_VIA_ALTERNATE);
				}
			}
		}


		/// <summary>
		/// Populates internal hash table of errors with specified error.
		/// </summary>
		/// <param name="error">ID of the error</param>
		/// <param name="msgResourceID">Resource ID of the error </param>
		private void SetValidationError(ValidationErrorID error, string msgResourceID)
		{
			//Add error to array list
			listErrors.Add(error);

			//Add error to the hashtable only if the error is not already in there
			if(!errorMessages.ContainsKey(error))
			{
				errorMessages.Add(error, msgResourceID);
			}
		}


		/// <summary>
		/// Read only property. Used for testing. Returns array list of
		/// error messages.
		/// </summary>
		internal ArrayList ErrorMessages
		{
			get {return listErrors;}
		}
	}
}
