// ****************************************************************
// NAME         : JourneyPlanRunner.cs
// AUTHOR       : Richard Philpott
// DATE CREATED : 2004-06-10
// DESCRIPTION  : Abstract base class for JourneyPlanRunner's.
//                Validates user input and creates journey request.
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/JourneyPlanRunnerBase.cs-arc  $
//
//   Rev 1.9   Jan 15 2013 11:13:38   mmodi
//Removed accessible location check for only PT, do always
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.8   Dec 05 2012 14:11:36   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.7   Sep 01 2011 10:43:36   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.6   Dec 21 2010 14:05:06   apatel
//Code updated to request services for the day of travel starting from 01:00 on the current day to 01:00 the following day for Find a train, Find a flight and City to City requests
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.5   Feb 20 2010 19:27:20   mmodi
//International planner error
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Feb 09 2010 09:45:14   apatel
//Updated for TD International planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Oct 13 2008 16:46:14   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.2   Aug 01 2008 16:42:46   mmodi
//Added cycle planner unavailable error
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.1   Jul 28 2008 13:15:06   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Jun 20 2008 15:01:14   mmodi
//Updated for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 10 2008 15:18:28   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//   Rev Devfactory Jan 20 2008 19:00:00 dgath
//   CCN 0382b - City to city enhancements: 
//Changes to PerformDateValidations. If a PrivateJourney is requested and an 'Anytime' value
//is provided, use the car journey times specified in properties table
//
//   Rev 1.0   Nov 08 2007 12:24:44   mturner
//Initial revision.
//
//
//   Rev 1.32   Aug 22 2007 09:45:08   mturner
//Changes to allow journeys in the past if they are for the current day or within a time period specified by the JourneyPlanningOffset property in permenent portal properties.  This is for CCN413 (IR4481)
//
//   Rev 1.31   Jun 15 2006 10:38:04   rbroddle
//Workaround for "Invalid Date" vantive 3577108
//Resolution for 4118: Workaround for "Invalid Date" problem
//
//   Rev 1.30   Apr 12 2006 16:13:32   kjosling
//Checks for default values in ReturnDayOfMonth and validates accordingly
//Resolution for 3772: DN062 Amend Tool: Leaving day field blank generates wrong error message
//
//   Rev 1.29   Apr 06 2006 18:04:28   kjosling
//Added validation of time to prevent fall though error messages
//Resolution for 3772: DN062 Amend Tool: Leaving day field blank generates wrong error message
//
//   Rev 1.28   Feb 10 2006 12:07:46   tolomolaiye
//Merge of stream 3180
//
//   Rev 1.27   Dec 05 2005 11:00:20   RBroddle
//SCR 3260 - Vantive 3577108 - Add further logging code to capture invalid date incidents
//
//   Rev 1.26.1.0   Dec 22 2005 09:09:28   tmollart
//Removed reference to OldJourneyParameters.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.26   Nov 09 2005 12:31:32   build
//Automatically merged from branch for stream2818
//
//   Rev 1.25.1.0   Oct 14 2005 15:10:44   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//
//   Rev 1.25   Jul 26 2005 11:40:00   RBroddle
//SCR2601 - Amendment to invalid date logging code to ensure this runs on production site.
//
//   Rev 1.24   Jul 05 2005 13:45:32   asinclair
//Merge for stream2557
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.23.1.2   Jun 30 2005 16:43:32   asinclair
//Changes made after FXCop
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.23.1.1   Jun 21 2005 14:41:14   asinclair
//Commented code
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.23.1.0   Jun 15 2005 14:09:54   asinclair
//Updated for CJP Architecture Changes
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.23   May 17 2005 14:22:50   PNorell
//Updated for code-review
//Resolution for 1954: Dev Code Review: PT - Session Partitioning
//
//   Rev 1.22   May 09 2005 17:04:34   tmollart
//Update to previous fix for conformance to coding standards.
//
//   Rev 1.21   May 09 2005 13:54:10   tmollart
//Added code to log when an error occurs with date valdiation as an operational event.
//Resolution for 2205: DEL 7.1: Invalid date error returned when planning journey
//
//   Rev 1.20   Apr 13 2005 12:20:52   Ralavi
//Adding new error constants for fuel cost and consumption validation
//
//   Rev 1.19   Mar 22 2005 10:29:30   jbroome
//Removed HandoffToEsriMap() method as added this functionality to the TDMapHandoff class. Updated code accordingly.
//
//   Rev 1.18   Jan 26 2005 15:53:02   PNorell
//Support for partitioning the session information.
//
//   Rev 1.17   Nov 16 2004 16:07:28   SWillcock
//Modified PerformDateValidations method as a fix for vantive #3489985
//Resolution for 1746: Incorrect error message displayed on amending date/time
//
//   Rev 1.16   Oct 15 2004 12:33:24   jgeorge
//Switched JourneyPlanControlData to JourneyPlanStateData
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.15   Sep 23 2004 15:12:38   esevern
//re-added check for open return type in date validation
//
//   Rev 1.14   Sep 22 2004 09:55:44   jmorrissey
//Added new validation constants
//Resolution for 1603: Find nearest: Error message is not quite accurate in the action specified.
//
//   Rev 1.13   Sep 21 2004 17:52:28   COwczarek
//Pass flag to CallCJP method to indicate whether or not
//journey request is for a journey extension.
//Resolution for 1263: Unhelpful user friendly error message for extend results
//
//   Rev 1.12   Sep 19 2004 14:21:28   esevern
//added check for outward minutes selected if hours entered: IR1503
//
//   Rev 1.11   Sep 14 2004 17:00:38   jmorrissey
//IR1507 - added more constants
//
//   Rev 1.10   Sep 13 2004 12:18:38   jmorrissey
//IR1527 - added new constants
//
//   Rev 1.9   Aug 03 2004 16:09:58   RPhilpott
//Use new ITDSessionManager.IsFindAMode to determine if we are handling a trunk request.
//
//   Rev 1.8   Jul 28 2004 16:29:42   RPhilpott
//Unit test corrections to date validation 
//
//   Rev 1.7   Jul 23 2004 18:26:36   RPhilpott
//DEL 6.1 Trunk Journey changes
//
//   Rev 1.6   Jul 06 2004 15:48:30   jgeorge
//userType correction
//
//   Rev 1.5   Jul 02 2004 13:37:32   jgeorge
//Changes for User Type
//
//   Rev 1.4   Jun 29 2004 17:10:22   jmorrissey
//Added CheckForJourneyStartInPast call in CallCJP method
//
//   Rev 1.3   Jun 28 2004 14:12:40   jgeorge
//Added code to remove OldJourneyParameters when performing a new search
//
//   Rev 1.2   Jun 18 2004 14:56:44   RPhilpott
//Find-A-Flight validation - interim check-in.
//
//   Rev 1.1   Jun 15 2004 14:36:26   COwczarek
//Add call to check for overlapping outward and return journey times
//Resolution for 867: Add extend journey functionality to summary and details pages
//
//   Rev 1.0   Jun 15 2004 13:54:22   RPhilpott
//Initial revision.
//

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Resources;
using System.Collections;
using System.Web.SessionState;
using System.Xml.Serialization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.JourneyPlanning.CJPInterface;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Presentation.InteractiveMapping;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for JourneyPlanRunner.
	/// </summary>
	public abstract class JourneyPlanRunnerBase : IJourneyPlanRunner
	{
        
		#region Constants

		protected const string OUTWARD_AND_RETURN_DATE_NOT_VALID = "ValidateAndRun.OutwardAndReturnDateNotValid";
		protected const string OUTWARD_DATE_NOT_VALID = "ValidateAndRun.DateNotValid";
		protected const string RETURN_DATE_NOT_VALID = "ValidateAndRun.DateNotValid";
		protected const string OUTWARD_DATE_IS_AFTER_RETURN_DATE = "ValidateAndRun.OutwardDateIsAfterReturnDate";
		protected const string DATE_TIME_IS_IN_THE_PAST = "ValidateAndRun.DateTimeIsInThePast";
		protected const string DATE_IS_IN_THE_PAST = "ValidateAndRun.DateIsInThePast";
		protected const string OUTWARD_AND_RETURN_DATE_IS_IN_THE_PAST = "ValidateAndRun.OutwardAndReturnDateIsInThePast";
		protected const string CHOOSE_AN_OPTION = "ValidateAndRun.ChooseAnOption";
		protected const string NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS = "ValidateAndRun.NoMatchForFromToViaAlternativeAndOtherErrors";
		protected const string NO_MATCH_FROM_TO_VIA_ALTERNATE = "ValidateAndRun.NoMatchForFromToViaAlternative";
		protected const string AMBIGUOUS_FROM_TO_VIA_ALTERNATE = "ValidateAndRun.FromToViaAlternativeAmbiguous";
		protected const string LOCATION_HAS_NO_NAPTAN = "ValidateAndRun.LocationHasNoNaptan";
        protected const string INTERNATIONAL_LOCATION_HAS_NO_NAPTAN = "ValidateAndRun.InternationalLocationHasNoNaptan";
        protected const string LOCATION_NOT_ACCESSIBLE = "ValidateAndRun.LocationNotAccessible";
		protected const string RETURN_MONTH_MISSING = "ValidateAndRun.ReturnMonthMissing";
		protected const string RETURN_DATE_MISSING = "ValidateAndRun.ReturnDateMissing";
		protected const string RETURN_TIME_MISSING = "ValidateAndRun.ReturnTimeMissing";
		protected const string NO_VALID_ROUTES = "ValidateAndRun.NoValidRoutes";
		protected const string INVALID_OPERATOR_SELECTION = "ValidateAndRun.InvalidOperatorSelection";
		protected const string ORIGIN_AND_DESTINATION_OVERLAP = "ValidateAndRun.OriginAndDestinationOverlap";
		protected const string ORIGIN_AND_VIA_OVERLAP = "ValidateAndRun.OriginAndViaOverlap";
		protected const string DESTINATION_AND_VIA_OVERLAP = "ValidateAndRun.DestinationAndViaOverlap";
		protected const string DOOR_TO_DOOR_ORIGIN_AND_DESTINATION_OVERLAP = "ValidateAndRun.DoorToDoorOriginAndDestinationOverlap";
		protected const string DOOR_TO_DOOR_ORIGIN_AND_VIA_OVERLAP = "ValidateAndRun.DoorToDoorOriginAndViaOverlap";
		protected const string DOOR_TO_DOOR_DESTINATION_AND_VIA_OVERLAP = "ValidateAndRun.DoorToDoorDestinationAndViaOverlap";
		protected const string FLIGHT_ORIGIN_AND_DESTINATION_OVERLAP = "ValidateAndRun.FlightOriginAndDestinationOverlap";
		protected const string FLIGHT_ORIGIN_AND_VIA_OVERLAP = "ValidateAndRun.FlightOriginAndViaOverlap";
		protected const string FLIGHT_DESTINATION_AND_VIA_OVERLAP = "ValidateAndRun.FlightDestinationAndViaOverlap";
		protected const string FIND_NEAREST_ORIGIN_AND_DESTINATION_OVERLAP = "ValidateAndRun.FindNearestOriginAndDestinationOverlap";
		protected const string FIND_NEAREST_ORIGIN_AND_VIA_OVERLAP = "ValidateAndRun.FindNearestOriginAndViaOverlap";
		protected const string FIND_NEAREST_DESTINATION_AND_VIA_OVERLAP = "ValidateAndRun.FindNearestDestinationAndViaOverlap";
		protected const string FUEL_CONSUMPTION_ENTERED_INVALID = "ValidateAndRun.FuelConsumptionErrorKey";
		protected const string FUEL_COST_ENTERED_INVALID = "ValidateAndRun.FuelCostErrorKey";
        protected const string CYCLE_PLANNER_UNAVAILABLE = "ValidateAndRun.CyclePlannerUnavailableKey";
        protected const string CYCLE_SPEED_ENTERED_INVALID = "ValidateAndRun.CycleSpeedErrorKey";
        protected const string LOCATION_HAS_NO_POINT = "ValidateAndRun.LocationHasNoPoint";
        protected const string DISTANCE_BETWEEN_LOCATIONS_TOO_GREAT = "ValidateAndRun.DistanceBetweenLocationsTooGreat";
        protected const string DISTANCE_BETWEEN_LOCATIONS_AND_VIA_TOO_GREAT = "ValidateAndRun.DistanceBetweenLocationsAndViaTooGreat";
        protected const string LOCATION_IN_INVALID_CYCLE_AREA = "ValidateAndRun.LocationInInvalidCycleArea";
        protected const string LOCATION_POINTS_NOT_IN_SAME_CYCLE_AREA = "ValidateAndRun.LocationPointsNotInSameCycleArea";
        protected const string INTERNATIONAL_PLANNER_UNAVAILABLE = "ValidateAndRun.InternationalPlannerUnavailableKey";
        protected const string INTERNATIONAL_PLANNER_JOURNEY_NOT_PERMITTED = "ValidateAndRun.InternationalPlannerJourneyNotPermitted";
        protected const string INTERNATIONAL_PLANNER_MODE_NOT_PERMITTED = "ValidateAndRun.InternationalPlannerModeNotPermitted";
        protected const string ROUTE_AFFECTED_BY_CLOSURES_ERRORS = "ValidateAndRun.RouteAffectedByClosures";
        #endregion


		#region Protected variables

		protected bool foundNonLocationValidationError = false;
		protected TDResourceManager resourceManager= null;
		protected ArrayList listErrors;
		protected Hashtable errorMessages;
        
		#endregion

        
		#region Constructor
		public JourneyPlanRunnerBase(TDResourceManager resourceManager)
		{
			this.resourceManager = resourceManager;
		}
		#endregion


		#region Public Methods
		/// <summary>
		/// ValidateAndRun
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The journey parameters</param>
		public abstract bool ValidateAndRun(ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters, string lang);

		/// <summary>
		/// Validate And Run a CJP - In fact no validation will occur as this is invoked for ammendments and the
		/// journey parameters will have already been validated.
		/// </summary>
		/// <param name="journeyRequest"></param>
		public abstract bool ValidateAndRun(int referenceNumber, int lastSequenceNumber, ITDJourneyRequest journeyRequest, ITDSessionManager tdSessionManager, string lang);
        
        public abstract ITDJourneyParameterConverter GetJourneyParamterConverter();

        /// <summary>
        /// Validate And Run a CJP - In fact no validation will occur as this is invoked for existing journey result modification and the
        /// journey parameters will have already been validated.
        /// </summary>
        /// <param name="referenceNumber"></param>
        /// <param name="lastSequenceNumber"></param>
        /// <param name="journeyRequest"></param>
        /// <returns></returns>
        public abstract bool ValidateAndRun(ITDSessionManager tdSessionManager, int referenceNumber, int lastSequenceNumber, ITDJourneyRequest journeyRequest);

		#endregion


		#region Invoke CJP Manager 
		/// <summary>
		/// Invoke the CJP Manager (asynchronously!)
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The validated user's Journey Parameters</param>
		/// <param name="outwardDateTime">The Date/Time of the outward journey</param>
		/// <param name="returnDateTime">The Date/Time of the return journey (may be null for one-way journeys)</param>
 
		protected void InvokeCJPManager( TDJourneyParameters tdJourneyParameters, TDDateTime outwardDateTime, TDDateTime returnDateTime, bool IsExtension, bool IsTrunkRequest)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			
			//Set the CJPRequestID and Status properties
			sessionManager.AsyncCallState.RequestID = Guid.NewGuid();
			sessionManager.AsyncCallState.Status = AsyncCallStatus.InProgress;
			
			//Call the SaveData method of the SessionManager
			sessionManager.SaveData();
			
			ITDJourneyParameterConverter converter = GetJourneyParamterConverter();
			
			string lang = Thread.CurrentThread.CurrentUICulture.ToString();

			//Obtain an instance of JourneyPlanRunnerCaller from TDServiceDiscovery
			IJourneyPlanRunnerCaller journeyCaller = (IJourneyPlanRunnerCaller)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlanRunnerCaller];
				
			CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();
			
			//call invoke method.      
			journeyCaller.InvokeCJPManager(sessionInfo, sessionManager.Partition,
				lang, sessionManager.AsyncCallState.RequestID, tdJourneyParameters,
                converter, outwardDateTime, returnDateTime, TDItineraryManager.Current.ExtendInProgress, GetPlannerMode());
			
		}

        private FindAPlannerMode GetPlannerMode()
        {
            FindAPlannerMode mode = FindAPlannerMode.None;
            try
            {
                switch (TDSessionManager.Current.FindAMode)
                {
                    case FindAMode.Flight:
                        mode = FindAPlannerMode.Flight;
                        break;

                    case FindAMode.Trunk:
                        mode = FindAPlannerMode.Trunk;
                        break;

                    case FindAMode.TrunkCostBased:
                        mode = FindAPlannerMode.TrunkCostBased;
                        break;

                    case FindAMode.TrunkStation:
                        mode = FindAPlannerMode.TrunkStation;
                        break;

                    case FindAMode.Train:
                        mode = FindAPlannerMode.Train;
                        break;

                    default:
                        mode = FindAPlannerMode.None;
                        break;
                }
            }
            catch
            {
                mode = FindAPlannerMode.None;
            }

            return mode;
        }

         
		/// <summary>
		/// Invoke the CJP Manager (asynchronously!)
		/// </summary>
		/// <param name="referenceNumber"></param>
		/// <param name="lastSequenceNumber"></param>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The validated user's Journey Parameters</param>
		/// <param name="outwardDateTime">The Date/Time of the outward journey</param>
		/// <param name="returnDateTime">The Date/Time of the return journey (may be null for one-way journeys)</param>
		protected void InvokeCJPManager( int referenceNumber, int lastSequenceNumber, ITDJourneyRequest tdJourneyRequest, bool modifyOriginalResult)
		{
            ITDSessionManager sessionManager = TDSessionManager.Current;
			//Set the CJPRequestID and Status properties
			sessionManager.AsyncCallState.RequestID = Guid.NewGuid();
			
			//Set status to InProgress
			sessionManager.AsyncCallState.Status = AsyncCallStatus.InProgress;
			
			CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();
			
			//Call the SaveData method of the SessionManager
			sessionManager.SaveData();
			
			//Obtain an instance of JourneyPlanRunnerCaller from TDServiceDiscovery
			IJourneyPlanRunnerCaller journeyCaller = (IJourneyPlanRunnerCaller)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlanRunnerCaller];
			
			journeyCaller.InvokeCJPManager(sessionInfo, sessionManager.Partition, sessionManager.AsyncCallState.RequestID, referenceNumber, lastSequenceNumber, tdJourneyRequest, modifyOriginalResult);
		}
                

		#endregion

		#region Protected Helper Methods
    
		protected void SetValidationError(ITDSessionManager tdSessionManager, ValidationErrorID validationErrorID, string msgResourceID, bool showErrors)
		{
			if  (showErrors)
			{
				listErrors.Add( validationErrorID);

				if  (!errorMessages.ContainsKey(validationErrorID))
				{
					errorMessages.Add(validationErrorID, msgResourceID);
				}
			}
		}

		#endregion

        
		#region Common Validations
        
		/// <summary>
		/// Perform Date/Time Validations
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The TD Journey Parameters</param>
		/// <param name="outwardDateTime">The date/time of the outward journey</param>
		/// <param name="returnDateTime">The date/time of the return journey</param>
		/// <param name="lang">Current UI language</param>
		/// <param name="logDateErrors">Whether to log errors found</param>
		protected void PerformDateValidations(ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters, ref TDDateTime outwardDateTime, ref TDDateTime returnDateTime, string lang, bool logDateErrors)
		{
			// "Any Time" options are only applicable for Trunk (FindA) requests 
            if (!tdSessionManager.IsFindAMode)
            {
                #region Outward and Return is Anytime
                if (tdJourneyParameters.OutwardAnyTime && tdJourneyParameters.ReturnAnyTime)
                {
                    //Vantive 3577108 - Add log entry to aid investigation
                    StringBuilder logOutRetAnyMsg = new StringBuilder();
                    logOutRetAnyMsg.Append("Invalid 'Any' Outward and Return Date Time");
                    logOutRetAnyMsg.Append(", Outward Date Time: " + tdJourneyParameters.OutwardDayOfMonth + "/" + tdJourneyParameters.OutwardMonthYear + " " + tdJourneyParameters.OutwardHour + ":" + tdJourneyParameters.OutwardMinute);
                    logOutRetAnyMsg.Append(", Return Date Time: " + tdJourneyParameters.ReturnDayOfMonth + "/" + tdJourneyParameters.ReturnMonthYear);
                    logOutRetAnyMsg.Append(" " + tdJourneyParameters.ReturnHour + ":" + tdJourneyParameters.ReturnMinute);
                    logOutRetAnyMsg.Append(", Journey from " + tdJourneyParameters.Origin.SearchType + " " + tdJourneyParameters.Origin.InputText);
                    logOutRetAnyMsg.Append(" To " + tdJourneyParameters.Destination.SearchType + " " + tdJourneyParameters.Destination.InputText);
                    logOutRetAnyMsg.Append(", Culture = " + Thread.CurrentThread.CurrentCulture);
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logOutRetAnyMsg.ToString()));
                    //Vantive 3577108 - Supress error message & reset flags to prevent "Invalid Date" problem
                    tdJourneyParameters.OutwardAnyTime = false;
                    tdJourneyParameters.ReturnAnyTime = false;
                    if (tdJourneyParameters.OutwardHour == "Any")
                    {
                        tdJourneyParameters.OutwardHour = "00";
                        tdJourneyParameters.OutwardMinute = "00";
                    }
                    if (tdJourneyParameters.ReturnHour == "Any")
                    {
                        tdJourneyParameters.ReturnHour = "00";
                        tdJourneyParameters.ReturnMinute = "00";
                    }

                }
                #endregion
                else
                {
                    #region Outward Anytime
                    if (tdJourneyParameters.OutwardAnyTime)
                    {
                        //Vantive 3577108 - Add log entry to aid investigation
                        StringBuilder logOutAnyMsg = new StringBuilder();
                        logOutAnyMsg.Append("Invalid 'Any' Outward Date Time: " + tdJourneyParameters.OutwardDayOfMonth + "/" + tdJourneyParameters.OutwardMonthYear + " " + tdJourneyParameters.OutwardHour + ":" + tdJourneyParameters.OutwardMinute);
                        logOutAnyMsg.Append(", Journey from " + tdJourneyParameters.Origin.SearchType + " " + tdJourneyParameters.Origin.InputText);
                        logOutAnyMsg.Append(" To " + tdJourneyParameters.Destination.SearchType + " " + tdJourneyParameters.Destination.InputText);
                        logOutAnyMsg.Append(", Return Date Time: " + tdJourneyParameters.ReturnDayOfMonth + "/" + tdJourneyParameters.ReturnMonthYear);
                        logOutAnyMsg.Append(" " + tdJourneyParameters.ReturnHour + ":" + tdJourneyParameters.ReturnMinute);
                        logOutAnyMsg.Append(", Culture = " + Thread.CurrentThread.CurrentCulture);
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logOutAnyMsg.ToString()));
                        //Vantive 3577108 - Supress error message & reset flags to prevent "Invalid Date" problem
                        tdJourneyParameters.OutwardAnyTime = false;
                        if (tdJourneyParameters.OutwardHour == "Any")
                        {
                            tdJourneyParameters.OutwardHour = "00";
                            tdJourneyParameters.OutwardMinute = "00";
                        }
                    }
                    #endregion

                    #region Return Anytime
                    if (tdJourneyParameters.ReturnAnyTime)
                    {
                        //Vantive 3577108 - Add log entry to aid investigation
                        StringBuilder logRtnAnyMsg = new StringBuilder();
                        logRtnAnyMsg.Append("Invalid 'Any' Return Date Time: " + tdJourneyParameters.ReturnDayOfMonth + "/" + tdJourneyParameters.ReturnMonthYear + " " + tdJourneyParameters.ReturnHour + ":" + tdJourneyParameters.ReturnMinute);
                        logRtnAnyMsg.Append(", Journey from " + tdJourneyParameters.Origin.SearchType + " " + tdJourneyParameters.Origin.InputText);
                        logRtnAnyMsg.Append(" To " + tdJourneyParameters.Destination.SearchType + " " + tdJourneyParameters.Destination.InputText);
                        logRtnAnyMsg.Append(", Outward Date Time: " + tdJourneyParameters.OutwardDayOfMonth + "/" + tdJourneyParameters.OutwardMonthYear + " " + tdJourneyParameters.OutwardHour + ":" + tdJourneyParameters.OutwardMinute);
                        logRtnAnyMsg.Append(", Culture = " + Thread.CurrentThread.CurrentCulture);
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logRtnAnyMsg.ToString()));
                        //Vantive 3577108 - Supress error message & reset flags to prevent "Invalid Date" problem
                        tdJourneyParameters.ReturnAnyTime = false;
                        if (tdJourneyParameters.ReturnHour == "Any")
                        {
                            tdJourneyParameters.ReturnHour = "00";
                            tdJourneyParameters.ReturnMinute = "00";
                        }
                    }
                    #endregion
                }

                // no point doing any more if we have an error here 
                if (foundNonLocationValidationError)
                {
                    return;
                }
            }
 
            // Handle specific City to City scenario for setting the Outward and Return Time only
            string outwardTime = string.Empty;
            string returnTime = string.Empty;

            #region City to city Anytime
            // For City to city, use a configured time value instead of midnight, where
            // car journeys are required and "Anytime" was selected
            TDJourneyParametersMulti tdJourneyParametersMulti = tdJourneyParameters as TDJourneyParametersMulti;

            if ((tdSessionManager.IsFindAMode) && (tdJourneyParametersMulti != null) && (tdJourneyParametersMulti.PrivateRequired) && (tdJourneyParameters.OutwardAnyTime))
            {
                #region Set outward time
                DateTime dateEntered = DateTime.Parse(tdJourneyParameters.OutwardDayOfMonth + " " + tdJourneyParameters.OutwardMonthYear + " " + "00:00", Thread.CurrentThread.CurrentCulture);
                                
                // Get time value from database
                outwardTime = Properties.Current["CityToCity.CarJourney.OutwardTime"].Substring(0, 2) + ":" + Properties.Current["CityToCity.CarJourney.OutwardTime"].Substring(2, 2);
              
                #endregion

                #region Set return time if required
                if (tdJourneyParameters.ReturnMonthYear != Enum.GetName(typeof(ReturnType), ReturnType.NoReturn)
                && tdJourneyParameters.ReturnMonthYear != Enum.GetName(typeof(ReturnType), ReturnType.OpenReturn))
                {
                    dateEntered = DateTime.Parse(tdJourneyParameters.ReturnDayOfMonth + " " + tdJourneyParameters.ReturnMonthYear + " " + "00:00", Thread.CurrentThread.CurrentCulture);

                    // Get time value from database
                    returnTime = Properties.Current["CityToCity.CarJourney.ReturnTime"].Substring(0, 2) + ":" + Properties.Current["CityToCity.CarJourney.ReturnTime"].Substring(2, 2);
                }
                #endregion
            }
            #endregion

			// Check that the mandatory outward date/time is valid
			try
			{
                #region Set Outward Time
                // Only set the outward time if it has not already been set above
                if ((tdJourneyParameters.OutwardAnyTime) && (string.IsNullOrEmpty(outwardTime)))
				{
					outwardTime = "00:00"; 
				}
				else if (string.IsNullOrEmpty(outwardTime))
				{
                
					// fix for vantive id 3489985
					// if the user hasn't selected a time in the drop down box then
					// the time string at this point will be "-"
					// in this case we need to raise the appropriate error
					// if minutes have not been specified but hour has, default to "00"
					if(tdJourneyParameters.OutwardHour.Length != 0 &&
						(tdJourneyParameters.OutwardMinute.Length == 0
						|| tdJourneyParameters.OutwardMinute.Equals("-")))
					{
						tdJourneyParameters.OutwardMinute = "00";
					}
                    
					outwardTime = tdJourneyParameters.OutwardHour + ":" + tdJourneyParameters.OutwardMinute;
                }
                // else the outwardTime has already been set by the City to city code logic above
                #endregion

                outwardDateTime = TDDateTime.Parse(tdJourneyParameters.OutwardDayOfMonth + " " + tdJourneyParameters.OutwardMonthYear + " " + outwardTime, Thread.CurrentThread.CurrentCulture);
                
			}
			catch (System.Exception ex)
            {
                #region Handle exception and Log error
                // Add a validation error item to indicate that the outward date/time was not a valid date/time
				foundNonLocationValidationError = true;
				SetValidationError(tdSessionManager, ValidationErrorID.OutwardDateTimeInvalid, OUTWARD_DATE_NOT_VALID, logDateErrors);
                #region Log Error
                //Vantive 3577108 - Add log entry to aid investigation
				StringBuilder logOutMsg = new StringBuilder();
				logOutMsg.Append(ex.GetType().Name + " " + ex.Message + " ");
				logOutMsg.Append("Invalid Outward Date Time: " + tdJourneyParameters.OutwardDayOfMonth + "/" + tdJourneyParameters.OutwardMonthYear + " " + tdJourneyParameters.OutwardHour + ":" + tdJourneyParameters.OutwardMinute);
				logOutMsg.Append(", Journey from " + tdJourneyParameters.Origin.SearchType + " " + tdJourneyParameters.Origin.InputText);
				logOutMsg.Append(" To " + tdJourneyParameters.Destination.SearchType + " " + tdJourneyParameters.Destination.InputText);
				logOutMsg.Append(", Return Date Time: "+ tdJourneyParameters.ReturnDayOfMonth + "/" + tdJourneyParameters.ReturnMonthYear);
				logOutMsg.Append(" " + tdJourneyParameters.ReturnHour + ":" + tdJourneyParameters.ReturnMinute);
				logOutMsg.Append(", Culture = " + Thread.CurrentThread.CurrentCulture);
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logOutMsg.ToString()));
                #endregion
                #endregion
            }

			if (!tdJourneyParameters.ReturnAnyTime)
			{
				// if minutes have not been specified but hour has, default to "00"
				if (tdJourneyParameters.ReturnMinute.Length == 0 && tdJourneyParameters.ReturnHour.Length != 0) 
				{
					tdJourneyParameters.ReturnMinute = "00";
				}
			}

			// If optional return date/time is provided, it must be valid
			if  (tdJourneyParameters.ReturnMonthYear != Enum.GetName(typeof(ReturnType), ReturnType.NoReturn)
				&& tdJourneyParameters.ReturnMonthYear != Enum.GetName(typeof(ReturnType), ReturnType.OpenReturn))
			{
				try
                {
                    #region Set Return Time
                    // Only set the return time if it has not already been set above
                    if ((tdJourneyParameters.ReturnAnyTime) && (string.IsNullOrEmpty(returnTime)))
				    {
					    returnTime = "00:00"; 
				    }
				    else if (string.IsNullOrEmpty(returnTime))
					{
						returnTime = tdJourneyParameters.ReturnHour + ":" + tdJourneyParameters.ReturnMinute;
                    }
                    #endregion

                    returnDateTime = TDDateTime.Parse(tdJourneyParameters.ReturnDayOfMonth + " " + tdJourneyParameters.ReturnMonthYear + " " + returnTime, Thread.CurrentThread.CurrentCulture );

				}
				catch (System.Exception ex)
                {
                    #region Handle exception and log error
                    foundNonLocationValidationError = true;
					string msgResourceID = string.Empty;
					ValidationErrorID validationErrorID;
                
					// month and day entered, but no hours selected
					// (note minutes will always be default to "00" if not specified)
					if(!tdJourneyParameters.ReturnAnyTime && tdJourneyParameters.ReturnHour.Length == 0) 
					{
						msgResourceID = RETURN_TIME_MISSING;
						validationErrorID = ValidationErrorID.ReturnTimeMissing;
					}
						// month and time entered, but no day selected
					else if(tdJourneyParameters.ReturnDayOfMonth.Length == 0 ||
							tdJourneyParameters.ReturnDayOfMonth == "-") 
					{
						msgResourceID = RETURN_DATE_MISSING;
						validationErrorID = ValidationErrorID.ReturnDateMissing;
					}
					else if(!ValidateTime(tdJourneyParameters.ReturnHour, tdJourneyParameters.ReturnMinute)) 
					{
						msgResourceID = RETURN_TIME_MISSING;
						validationErrorID = ValidationErrorID.ReturnTimeMissing;
					}
					else 
					{
						// We need a different MsgResourceID depending on whether both the outward AND return dates are invalid
						if(errorMessages.ContainsValue(OUTWARD_DATE_NOT_VALID))
						{
							// removed outward date not valid from errors list
							errorMessages.Remove(ValidationErrorID.OutwardDateTimeInvalid);
							listErrors.Remove(ValidationErrorID.OutwardDateTimeInvalid);
							msgResourceID = OUTWARD_AND_RETURN_DATE_NOT_VALID;
							validationErrorID = ValidationErrorID.OutwardAndReturnDateTimeInvalid;
						}
						else
						{
							msgResourceID = RETURN_DATE_NOT_VALID;
							validationErrorID = ValidationErrorID.ReturnDateTimeInvalid;
                        }

                        #region Log Error
                        //Vantive 3577108 - Add log entry to aid investigation
						StringBuilder logRtnMsg = new StringBuilder();
						logRtnMsg.Append(ex.GetType().Name + " " + ex.Message + " ");
						logRtnMsg.Append("Invalid Return Date Time: " + tdJourneyParameters.ReturnDayOfMonth + "/" + tdJourneyParameters.ReturnMonthYear + " " + tdJourneyParameters.ReturnHour + ":" + tdJourneyParameters.ReturnMinute);
						logRtnMsg.Append(", Journey from " + tdJourneyParameters.Origin.SearchType + " " + tdJourneyParameters.Origin.InputText);
						logRtnMsg.Append(" To " + tdJourneyParameters.Destination.SearchType + " " + tdJourneyParameters.Destination.InputText);
						logRtnMsg.Append(", Outward Date Time: "+ tdJourneyParameters.OutwardDayOfMonth + "/" + tdJourneyParameters.OutwardMonthYear + " " + tdJourneyParameters.OutwardHour + ":" + tdJourneyParameters.OutwardMinute);
						logRtnMsg.Append(", Culture = " + Thread.CurrentThread.CurrentCulture);
						Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,logRtnMsg.ToString()));
                        #endregion
                    }
					SetValidationError(tdSessionManager, validationErrorID, msgResourceID, logDateErrors);
                    #endregion
                }

				// If return date/time is provided and valid, it must be later than outward
				if (returnDateTime != null && outwardDateTime != null)
				{
					if (outwardDateTime > returnDateTime)
					{
						foundNonLocationValidationError = true;
						SetValidationError(tdSessionManager, 
							ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime, 
							OUTWARD_DATE_IS_AFTER_RETURN_DATE,
							logDateErrors);
					}
				}
			}
			else // month selection was 'no return' check for date/time entered
			{
				// a day was specified but no month
				if(tdJourneyParameters.ReturnDayOfMonth.Length != 0) 
				{
					foundNonLocationValidationError = true;
					SetValidationError(tdSessionManager, ValidationErrorID.ReturnMonthMissing, 
						RETURN_MONTH_MISSING,
						logDateErrors);

					// day entered, but no hours selected
					// (note minutes will always be defaulted to "00" if not specified)
					if  (tdJourneyParameters.ReturnHour.Length == 0) 
					{
						SetValidationError(tdSessionManager, ValidationErrorID.ReturnTimeMissing, RETURN_TIME_MISSING, logDateErrors);
					} 
				}

				// a time was specified but no month
				// (note minutes will always be defaulted to "00" if not specified)
				if  (tdJourneyParameters.ReturnHour.Length != 0) 
				{
					foundNonLocationValidationError = true;
					SetValidationError(tdSessionManager, ValidationErrorID.ReturnDateMissing, RETURN_DATE_MISSING, logDateErrors); 
				}
            }

            #region Check OutwardDateTime
            // For valid outward date/time
			if  (outwardDateTime != null)
			{
				// Check that the mandatory outward date/time is later than tdDateTimeNow
				TDDateTime tdDateTimeNow = TDDateTime.Now;

				// If outward time is any, remove the time component
				if (tdJourneyParameters.OutwardAnyTime)
				{
					tdDateTimeNow = new TDDateTime(tdDateTimeNow.Year, tdDateTimeNow.Month, tdDateTimeNow.Day);
				}

				// Create the earliest time for which we will allow users to plan a journey
				TDDateTime adjustedDateTime = outwardDateTime;
				bool differentDate = false;
				
				//Check whether journey is being planned for today
				if((adjustedDateTime.Day != tdDateTimeNow.Day) || (adjustedDateTime.Month != tdDateTimeNow.Month) || (adjustedDateTime.Year != tdDateTimeNow.Year))
				{
					differentDate = true;
				}

				string stringTimeOffset = Properties.Current["JourneyPlanningOffset"];
				
				if(stringTimeOffset != null)
				{
					int intTimeOffset = Int32.Parse(stringTimeOffset);
					adjustedDateTime.AddMinutes(intTimeOffset);
				}

				if  (adjustedDateTime < tdDateTimeNow && differentDate)
				{
					// Add a validation error item to indicate that the outward date/time was not later than "now"
					foundNonLocationValidationError = true;
					SetValidationError(tdSessionManager, ValidationErrorID.OutwardDateTimeNotLaterThanNow, 
						DATE_TIME_IS_IN_THE_PAST, logDateErrors);

				}
				if  (!foundNonLocationValidationError && tdJourneyParameters.OutwardAnyTime)
				{
					// any time & today's date actually means any time after current time ...

					if  (outwardDateTime.Year == tdDateTimeNow.Year 
						&& outwardDateTime.Month == tdDateTimeNow.Month 
						&& outwardDateTime.Day == tdDateTimeNow.Day)
					{
					    //outwardDateTime = TDDateTime.Now;
                        outwardDateTime.Hour = TDDateTime.Now.Hour;
                        outwardDateTime.Minute = TDDateTime.Now.Minute;
                    }
				}
            }
            #endregion

            #region Check ReturnDateTime
            if (returnDateTime != null)
			{
				// Check that the optional return date/time is later than tdDateTimeNow
				TDDateTime tdDateTimeNow = TDDateTime.Now;
				
				// If return time is any, remove time component
				if  (tdJourneyParameters.ReturnAnyTime)
				{
					tdDateTimeNow = new TDDateTime(tdDateTimeNow.Year, tdDateTimeNow.Month, tdDateTimeNow.Day);
				}

				TDDateTime adjustedDateTime = returnDateTime;
				bool differentDate = false;
				
				//Check whether journey is being planned for today
				if((adjustedDateTime.Day != tdDateTimeNow.Day) || (adjustedDateTime.Month != tdDateTimeNow.Month) || (adjustedDateTime.Year != tdDateTimeNow.Year))
				{
					differentDate = true;
				}
								
				// Create the earliest time for which we will allow users to plan a journey
				string stringTimeOffset = Properties.Current["JourneyPlanningOffset"];
				
				if(stringTimeOffset != null)
				{
					int intTimeOffset = Int32.Parse(stringTimeOffset);
					adjustedDateTime.AddMinutes(intTimeOffset);
				}

				if  (adjustedDateTime < tdDateTimeNow && differentDate)
				{
					// Add a validation error item to indicate that the return date/time was not later than "now"
					foundNonLocationValidationError = true;
                    
					string msgResourceID = string.Empty;
                    
					ValidationErrorID validationErrorID;

					if (errorMessages.ContainsKey(ValidationErrorID.OutwardDateTimeNotLaterThanNow))
					{
						errorMessages.Remove(ValidationErrorID.OutwardDateTimeNotLaterThanNow);
						listErrors.Remove(ValidationErrorID.OutwardDateTimeNotLaterThanNow);
						msgResourceID = OUTWARD_AND_RETURN_DATE_IS_IN_THE_PAST;
						validationErrorID = ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow;
					}
					else
					{
						msgResourceID = DATE_TIME_IS_IN_THE_PAST;
						validationErrorID = ValidationErrorID.ReturnDateTimeNotLaterThanNow;
					}

					SetValidationError(tdSessionManager, validationErrorID, msgResourceID, logDateErrors);
				}

				if  (!foundNonLocationValidationError && tdJourneyParameters.ReturnAnyTime)
				{
					// any time & today's date actually means any time after current time ...

					if  (returnDateTime.Year == tdDateTimeNow.Year 
						&& returnDateTime.Month == tdDateTimeNow.Month 
						&& returnDateTime.Day == tdDateTimeNow.Day)
					{
						//returnDateTime = TDDateTime.Now;
                        returnDateTime.Hour = TDDateTime.Now.Hour;
                        returnDateTime.Minute = TDDateTime.Now.Minute;
					}
				}

            }
            #endregion

            #region Update IsReturnRequired flag
            // If a return date/time is present then ensure the IsReturnRequired parameter is
			// set (even if date/time is erroneous). The return date/time may have been
			// "No Return" or "Open Return" when leaving input page but subsequently changed
			// by the ambiguity page if the date/time was incomplete.
            
			if ( returnDateTime != null )
			{
				// It cannot be "Open Return" or "No return"  in this case.
				tdJourneyParameters.IsReturnRequired = true;
            }
            #endregion
        }

		private bool ValidateTime(string hours, string minutes)
		{
			try
			{
				int check = int.Parse(hours);
				check = int.Parse(minutes);
				return true;
			}
			catch(FormatException)
			{
				return false;
			}
		}

        /// <summary>
        /// Perform the Location Validations
        /// </summary>
        /// <param name="tdSessionManager">The TD Session Manager</param>
        /// <param name="tdJourneyParameters">The TD Journey Parameters</param>
        protected void PerformLocationValidations(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
        {
            #region Check locations Ambiguous state

            // Check whether the Origin Location is ambiguous
            if (tdJourneyParameters.OriginLocation.Status == TDLocationStatus.Ambiguous)
            {
                SetValidationError(tdSessionManager, ValidationErrorID.AmbiguousOriginLocation, AMBIGUOUS_FROM_TO_VIA_ALTERNATE, true);
                foundNonLocationValidationError = true;
            }

            // Check whether the Destination Location is ambiguous
            if (tdJourneyParameters.DestinationLocation.Status == TDLocationStatus.Ambiguous)
            {
                SetValidationError(tdSessionManager, ValidationErrorID.AmbiguousDestinationLocation, AMBIGUOUS_FROM_TO_VIA_ALTERNATE, true);
                foundNonLocationValidationError = true;

            }

            // Check whether the Public Via Location is ambiguous
            if (tdJourneyParameters.PublicViaLocation.Status == TDLocationStatus.Ambiguous)
            {
                SetValidationError(tdSessionManager, ValidationErrorID.AmbiguousPublicViaLocation, AMBIGUOUS_FROM_TO_VIA_ALTERNATE, true);
                foundNonLocationValidationError = true;
            }

            // Check whether the Private Via Location is ambiguous
            if (tdJourneyParameters.PrivateViaLocation.Status == TDLocationStatus.Ambiguous)
            {
                SetValidationError(tdSessionManager, ValidationErrorID.AmbiguousPrivateViaLocation, AMBIGUOUS_FROM_TO_VIA_ALTERNATE, true);
                foundNonLocationValidationError = true;
            }

            // Check whether the Cycle Via Location is ambiguous
            if (tdJourneyParameters.CycleViaLocation.Status == TDLocationStatus.Ambiguous)
            {
                SetValidationError(tdSessionManager, ValidationErrorID.AmbiguousCycleViaLocation, AMBIGUOUS_FROM_TO_VIA_ALTERNATE, true);
                foundNonLocationValidationError = true;
            }

            #endregion

            #region Check locations Unspecified state

            // Check whether the Origin Location is valid
            if (tdJourneyParameters.OriginLocation.Status == TDLocationStatus.Unspecified)
            {
                string msgResourceID = string.Empty;
                ValidationErrorID validationErrorID;
                if (foundNonLocationValidationError == true)
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS;
                    validationErrorID = ValidationErrorID.OriginLocationInvalidAndOtherErrors;
                }
                else
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE;
                    validationErrorID = ValidationErrorID.OriginLocationInvalid;
                }
                SetValidationError(tdSessionManager, validationErrorID, msgResourceID, true);
            }

            // Check whether the Destination Location is valid
            if (tdJourneyParameters.DestinationLocation.Status == TDLocationStatus.Unspecified)
            {
                string msgResourceID = string.Empty;
                ValidationErrorID validationErrorID;

                if (foundNonLocationValidationError == true)
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS;
                    validationErrorID = ValidationErrorID.DestinationLocationInvalidAndOtherErrors;
                }
                else
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE;
                    validationErrorID = ValidationErrorID.DestinationLocationInvalid;
                }
                SetValidationError(tdSessionManager, validationErrorID, msgResourceID, true);
            }

            // Check whether the Public Via Location is unspecified (and input text has been filled)
            if (tdJourneyParameters.PublicViaLocation.Status == TDLocationStatus.Unspecified
                && tdJourneyParameters.PublicVia.InputText.Length != 0)
            {
                string msgResourceID = string.Empty;
                ValidationErrorID validationErrorID;
                if (foundNonLocationValidationError == true)
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS;
                    validationErrorID = ValidationErrorID.PublicViaLocationInvalidAndOtherErrors;
                }
                else
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE;
                    validationErrorID = ValidationErrorID.PublicViaLocationInvalid;
                }
                SetValidationError(tdSessionManager, validationErrorID, msgResourceID, true);
            }

            // Check whether the Private Via Location is unspecified (and input text has been filled)
            if (tdJourneyParameters.PrivateViaLocation.Status == TDLocationStatus.Unspecified
                && tdJourneyParameters.PrivateVia.InputText.Length != 0)
            {
                string msgResourceID = string.Empty;
                ValidationErrorID validationErrorID;
                if (foundNonLocationValidationError == true)
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS;
                    validationErrorID = ValidationErrorID.PrivateViaLocationInvalidAndOtherErrors;
                }
                else
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE;
                    validationErrorID = ValidationErrorID.PrivateViaLocationInvalid;
                }
                SetValidationError(tdSessionManager, validationErrorID, msgResourceID, true);
            }

            // Check whether the Cycle Via Location is unspecified (and input text has been filled)
            if (tdJourneyParameters.CycleViaLocation.Status == TDLocationStatus.Unspecified
                && tdJourneyParameters.CycleVia.InputText.Length != 0)
            {
                string msgResourceID = string.Empty;
                ValidationErrorID validationErrorID;
                if (foundNonLocationValidationError == true)
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS;
                    validationErrorID = ValidationErrorID.CycleViaLocationInvalidAndOtherErrors;
                }
                else
                {
                    msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE;
                    validationErrorID = ValidationErrorID.CycleViaLocationInvalid;
                }
                SetValidationError(tdSessionManager, validationErrorID, msgResourceID, true);
            }

            #endregion

            #region Check locations Naptans and Locality

            StringBuilder logMsg = new StringBuilder();

            if (tdJourneyParameters.PublicRequired)
            {
                if (tdJourneyParameters.OriginLocation.Status == TDLocationStatus.Valid)
                {
                    if ((tdJourneyParameters.OriginLocation.NaPTANs.Length == 0)
                        && (tdJourneyParameters.OriginLocation.RequestPlaceType == RequestPlaceType.NaPTAN))
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.OriginLocationHasNoNaptan, LOCATION_HAS_NO_NAPTAN, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            logMsg.Append("No NAPTANs found for origin location " + tdJourneyParameters.OriginLocation.Description + " ");
                        }
                    }

                    if (tdJourneyParameters.OriginLocation.Locality == null
                        || tdJourneyParameters.OriginLocation.Locality.Length == 0
                        || tdJourneyParameters.OriginLocation.Locality.IndexOf('?') > -1) // added, if locality contains '?' (commonly 8 '?'), it is incorrect
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.OriginLocationHasNoNaptan, LOCATION_HAS_NO_NAPTAN, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            logMsg.Append("No locality found for origin location " + tdJourneyParameters.OriginLocation.Description + " ");
                        }
                    }
                }

                if (tdJourneyParameters.DestinationLocation.Status == TDLocationStatus.Valid)
                {
                    if ((tdJourneyParameters.DestinationLocation.NaPTANs.Length == 0)
                        && (tdJourneyParameters.DestinationLocation.RequestPlaceType == RequestPlaceType.NaPTAN))
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.DestinationLocationHasNoNaptan, LOCATION_HAS_NO_NAPTAN, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            logMsg.Append("No NAPTANs found for destination location " + tdJourneyParameters.DestinationLocation.Description + " ");
                        }
                    }

                    if (tdJourneyParameters.DestinationLocation.Locality == null
                        || tdJourneyParameters.DestinationLocation.Locality.Length == 0
                        || tdJourneyParameters.DestinationLocation.Locality.IndexOf('?') > -1) // added, if locality contains '?' (commonly 8 '?'), it is incorrect
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.DestinationLocationHasNoNaptan, LOCATION_HAS_NO_NAPTAN, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            logMsg.Append("No locality found for destination location " + tdJourneyParameters.DestinationLocation.Description);
                        }
                    }
                }
            }

            #endregion

            #region Check locations Coordinates (for debug only!)

            if (TDTraceSwitch.TraceVerbose)
            {
                if (logMsg.Length > 0)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
                }

                if (tdJourneyParameters.OriginLocation.Status == TDLocationStatus.Valid)
                {
                    if (tdJourneyParameters.OriginLocation.GridReference.Easting <= 0
                        || tdJourneyParameters.OriginLocation.GridReference.Northing <= 0)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "Invalid OSGR " + tdJourneyParameters.OriginLocation.GridReference.Easting + ", " + tdJourneyParameters.OriginLocation.GridReference.Northing + " for origin location " + tdJourneyParameters.OriginLocation.Description));
                    }
                }

                if (tdJourneyParameters.DestinationLocation.Status == TDLocationStatus.Valid)
                {
                    if (tdJourneyParameters.DestinationLocation.GridReference.Easting <= 0
                        || tdJourneyParameters.DestinationLocation.GridReference.Northing <= 0)
                    {
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "Invalid OSGR " + tdJourneyParameters.DestinationLocation.GridReference.Easting + ", " + tdJourneyParameters.DestinationLocation.GridReference.Northing + " for destination location " + tdJourneyParameters.DestinationLocation.Description));
                    }
                }
            }

            #endregion

            #region Check locations Accessible state

            // If accessible options selected, check locations are accessible
            if (tdJourneyParameters.RequireStepFreeAccess || tdJourneyParameters.RequireSpecialAssistance)
            {
                if (tdJourneyParameters.OriginLocation.Status == TDLocationStatus.Valid)
                {
                    if (!tdJourneyParameters.OriginLocation.Accessible)
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.OriginLocationNotAccessible, LOCATION_NOT_ACCESSIBLE, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                                string.Format("Origin location [{0}] is not accessible ", tdJourneyParameters.OriginLocation.Description)));
                        }
                    }
                }

                if (tdJourneyParameters.DestinationLocation.Status == TDLocationStatus.Valid)
                {
                    if (!tdJourneyParameters.DestinationLocation.Accessible)
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.DestinationLocationNotAccessible, LOCATION_NOT_ACCESSIBLE, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                                string.Format("Destination location [{0}] is not accessible ", tdJourneyParameters.DestinationLocation.Description)));
                        }
                    }
                }

                if (tdJourneyParameters.PublicViaLocation.Status == TDLocationStatus.Valid)
                {
                    if (!tdJourneyParameters.PublicViaLocation.Accessible)
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.PublicViaLocationNotAccessible, LOCATION_NOT_ACCESSIBLE, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                                string.Format("Public via location [{0}] is not accessible ", tdJourneyParameters.PublicViaLocation.Description)));
                        }
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// validates the from and to location and 
        /// updates the journey parameters to use only the modes where the naptans will not overlap
        /// </summary>
        /// <param name="tdSessionManager"></param>
        /// <param name="tdJourneyParameters"></param>
        protected void CheckLocationsForOverlapping(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters, TDLocation viaLocationParameter)
        {
            //location objects used for the origin and destination
            TDLocation fromLocation;
            TDLocation toLocation;
            TDLocation viaLocation;

            //assign the start and destination locations 
            fromLocation = tdJourneyParameters.OriginLocation;
            toLocation = tdJourneyParameters.DestinationLocation;
            viaLocation = viaLocationParameter; // Use the via location specified

            // Only check if locations are valid
            if ((fromLocation.Status != TDLocationStatus.Valid) || (toLocation.Status != TDLocationStatus.Valid))
                return;

            //temp array list to allow modes to be added and removed
            ArrayList nonOverlappingModeTypes = new ArrayList();

            //bools to indicate if overlaps were found			
            bool railOverlaps = false;
            bool coachOverlaps = false;
            bool airOverlaps = false;


            //for all searches except city-to-city, if an overlap occurs between the from, to
            //or via location, then a message should be displayed to the user and they need to change
            //one of the locations
            if ((tdSessionManager.FindAMode != FindAMode.Trunk) && (tdSessionManager.FindAMode != FindAMode.TrunkStation))
            {
                //check origin and destination locations for overlapping naptans
                if (fromLocation.Intersects(toLocation, StationType.Undetermined))
                {
                    //if any overlaps were found output an error message to the user.
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "There are overlapping naptans for the origin and destination locations. Origin location: " +
                        tdJourneyParameters.OriginLocation + " Destination location: "
                        + tdJourneyParameters.DestinationLocation));

                    //IR1603				
                    bool findNearestUsed = ((fromLocation.SearchType == SearchType.FindNearest) &&
                        (toLocation.SearchType == SearchType.FindNearest));
                    if (findNearestUsed)
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.OriginAndDestinationOverlap, FIND_NEAREST_ORIGIN_AND_DESTINATION_OVERLAP, true);
                    }
                    else
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.OriginAndDestinationOverlap, DOOR_TO_DOOR_ORIGIN_AND_DESTINATION_OVERLAP, true);
                    }

                }

                //check origin and via locations for overlapping naptans
                if (fromLocation.Intersects(viaLocation, StationType.Undetermined))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "There are overlapping naptans for the origin and via locations. Origin location: " +
                        tdJourneyParameters.OriginLocation + " Destination location: "
                        + tdJourneyParameters.DestinationLocation));

                    //IR1603				
                    bool findNearestUsed = ((fromLocation.SearchType == SearchType.FindNearest) &&
                        (viaLocation.SearchType == SearchType.FindNearest));
                    if (findNearestUsed)
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.OriginAndViaOverlap, FIND_NEAREST_ORIGIN_AND_VIA_OVERLAP, true);
                    }
                    else
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.OriginAndViaOverlap, DOOR_TO_DOOR_ORIGIN_AND_VIA_OVERLAP, true);
                    }
                }

                //check destination and via locations for overlapping naptans
                if (toLocation.Intersects(viaLocation, StationType.Undetermined))
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "There are overlapping naptans for the destination via locations. Origin location: " +
                        tdJourneyParameters.OriginLocation + " Destination location: "
                        + tdJourneyParameters.DestinationLocation));

                    //IR1603				
                    bool findNearestUsed = ((toLocation.SearchType == SearchType.FindNearest) &&
                        (viaLocation.SearchType == SearchType.FindNearest));
                    if (findNearestUsed)
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.DestinationAndViaOverlap, FIND_NEAREST_DESTINATION_AND_VIA_OVERLAP, true);
                    }
                    else
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.DestinationAndViaOverlap, DOOR_TO_DOOR_DESTINATION_AND_VIA_OVERLAP, true);
                    }
                }
            }
            //special case for city-to-city
            //check each mode separately for overlaps and remove any overlapping modes from
            //the journey parameters
            else
            {
                //assign mode types used in CJP search to the temp array list
                for (int i = 0; i < tdJourneyParameters.PublicModes.Length; i++)
                {
                    nonOverlappingModeTypes.Add(tdJourneyParameters.PublicModes[i]);
                }

                // Remove any modetypes that aren't used
                if (!fromLocation.ContainsNaptansForStationType(StationType.Airport) || !toLocation.ContainsNaptansForStationType(StationType.Airport))
                {
                    airOverlaps = true;
                    nonOverlappingModeTypes.Remove(ModeType.Air);
                }

                if (!fromLocation.ContainsNaptansForStationType(StationType.Rail) || !toLocation.ContainsNaptansForStationType(StationType.Rail))
                {
                    railOverlaps = true;
                    nonOverlappingModeTypes.Remove(ModeType.Rail);
                }

                if (!fromLocation.ContainsNaptansForStationType(StationType.Coach) || !toLocation.ContainsNaptansForStationType(StationType.Coach))
                {
                    coachOverlaps = true;
                    nonOverlappingModeTypes.Remove(ModeType.Coach);
                }

                //check origin and via locations for overlapping airports, and remove airport mode from array list if found
                if (nonOverlappingModeTypes.Contains(ModeType.Air) && fromLocation.Intersects(toLocation, StationType.Airport))
                {
                    nonOverlappingModeTypes.Remove(ModeType.Air);
                    airOverlaps = true;
                }

                //check origin and destination locations for overlapping coach stations, and remove coach mode from array list if found
                if (nonOverlappingModeTypes.Contains(ModeType.Coach) && fromLocation.Intersects(toLocation, StationType.Coach))
                {
                    nonOverlappingModeTypes.Remove(ModeType.Coach);
                    coachOverlaps = true;
                }

                //check origin and destination locations for overlapping rail stations, and remove rail mode from array list if found
                if (nonOverlappingModeTypes.Contains(ModeType.Rail) && fromLocation.Intersects(toLocation, StationType.Rail))
                {
                    nonOverlappingModeTypes.Remove(ModeType.Rail);
                    railOverlaps = true;
                }

                //check if error message needed for city-to-city, 
                //this is only if ALL modes are overlapping
                if (airOverlaps && coachOverlaps && railOverlaps)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "There are overlapping naptans for the origin and destination locations. Origin location: " +
                        tdJourneyParameters.OriginLocation + " Destination location: "
                        + tdJourneyParameters.DestinationLocation));

                    SetValidationError(tdSessionManager, ValidationErrorID.OriginAndDestinationOverlap, ORIGIN_AND_DESTINATION_OVERLAP, true);

                }
            }
        }

        /// <summary>
        /// validates the from and to location and sets eror if duplicate location
        /// </summary>
        /// <param name="tdSessionManager"></param>
        /// <param name="tdJourneyParameters"></param>
        protected void CheckLocationsForDuplication(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
        {

            // dont check if city-to-city
            if ((tdSessionManager.FindAMode != FindAMode.Trunk) && (tdSessionManager.FindAMode != FindAMode.TrunkStation))
            {

                //location objects used for the origin and destination
                TDLocation fromLocation;
                TDLocation toLocation;
                TDLocation viaLocation;
                TDLocation privateViaLocation; // Car Parking only
                TDLocation cycleViaLocation; // Cycle planner only

                //assign the start and destination locations 
                fromLocation = tdJourneyParameters.OriginLocation;
                toLocation = tdJourneyParameters.DestinationLocation;
                viaLocation = tdJourneyParameters.PublicViaLocation;
                privateViaLocation = tdJourneyParameters.PrivateViaLocation; // Car Parking Only
                cycleViaLocation = tdJourneyParameters.CycleViaLocation; // Cycle planner only

                if ((fromLocation.GridReference.Easting == toLocation.GridReference.Easting &&
                    fromLocation.GridReference.Northing == toLocation.GridReference.Northing))
                {
                    //if any duplicates were found output an error message to the user.
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "The 'From' and 'To' locations are identical.Please amend your choice by clicking 'Back' or 'New Location'"));
                    SetValidationError(tdSessionManager, ValidationErrorID.OriginAndDestinationOverlap, ORIGIN_AND_DESTINATION_OVERLAP, true);
                }

                if ((fromLocation.GridReference.Easting == viaLocation.GridReference.Easting &&
                    fromLocation.GridReference.Northing == viaLocation.GridReference.Northing))
                {
                    //if any duplicates were found output an error message to the user.
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "The 'From' and 'Via' locations are identical.Please amend your choice by clicking 'Back' or 'New Location'"));
                    SetValidationError(tdSessionManager, ValidationErrorID.OriginAndViaOverlap, ORIGIN_AND_VIA_OVERLAP, true);
                }

                if ((toLocation.GridReference.Easting == viaLocation.GridReference.Easting &&
                    toLocation.GridReference.Northing == viaLocation.GridReference.Northing))
                {
                    //if any duplicates were found output an error message to the user.
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                        "The 'To' and 'Via' locations are identical.Please amend your choice by clicking 'Back' or 'New Location'"));
                    SetValidationError(tdSessionManager, ValidationErrorID.DestinationAndViaOverlap, DESTINATION_AND_VIA_OVERLAP, true);
                }

                // --- Car Parks location checking ---

                // Check From and To locations
                if ((fromLocation.CarParking != null) && (toLocation.CarParking != null))
                {
                    if (!errorMessages.ContainsKey(ORIGIN_AND_DESTINATION_OVERLAP))
                    {
                        if (fromLocation.CarParking.CarParkReference == toLocation.CarParking.CarParkReference)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "From/To Car Park has the same reference."));
                            SetValidationError(tdSessionManager, ValidationErrorID.OriginAndDestinationOverlap, ORIGIN_AND_DESTINATION_OVERLAP, true);
                        }
                    }
                }

                // Check From and Private Via locations
                if ((fromLocation.CarParking != null) && (privateViaLocation.CarParking != null))
                {
                    if (!errorMessages.ContainsKey(ORIGIN_AND_VIA_OVERLAP))
                    {
                        if (fromLocation.CarParking.CarParkReference == privateViaLocation.CarParking.CarParkReference)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "From/Via Car Park has the same reference."));
                            SetValidationError(tdSessionManager, ValidationErrorID.OriginAndViaOverlap, ORIGIN_AND_VIA_OVERLAP, true);
                        }
                    }
                }

                // Check To and Private Via locations
                if ((toLocation.CarParking != null) && (privateViaLocation.CarParking != null))
                {
                    if (!errorMessages.ContainsKey(DESTINATION_AND_VIA_OVERLAP))
                    {
                        if (toLocation.CarParking.CarParkReference == privateViaLocation.CarParking.CarParkReference)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "To/Via Car Park has the same reference."));
                            SetValidationError(tdSessionManager, ValidationErrorID.DestinationAndViaOverlap, DESTINATION_AND_VIA_OVERLAP, true);
                        }
                    }
                }


                // Check From and Public Via locations
                if ((fromLocation.CarParking != null) && (viaLocation.CarParking != null))
                {
                    if (!errorMessages.ContainsKey(ORIGIN_AND_VIA_OVERLAP))
                    {
                        if (fromLocation.CarParking.CarParkReference == viaLocation.CarParking.CarParkReference)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "From/Via Car Park has the same reference."));
                            SetValidationError(tdSessionManager, ValidationErrorID.OriginAndViaOverlap, ORIGIN_AND_VIA_OVERLAP, true);
                        }
                    }
                }

                // Check To and Public Via locations
                if ((toLocation.CarParking != null) && (viaLocation.CarParking != null))
                {
                    if (!errorMessages.ContainsKey(DESTINATION_AND_VIA_OVERLAP))
                    {
                        if (toLocation.CarParking.CarParkReference == viaLocation.CarParking.CarParkReference)
                        {
                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "To/Via Car Park has the same reference."));
                            SetValidationError(tdSessionManager, ValidationErrorID.DestinationAndViaOverlap, DESTINATION_AND_VIA_OVERLAP, true);
                        }
                    }
                }

                // --- End of Car Parking location checks ---

                // -- Cycle planner via location checks --

                if (cycleViaLocation != null)
                {
                    if ((fromLocation.GridReference.Easting == cycleViaLocation.GridReference.Easting &&
                        fromLocation.GridReference.Northing == cycleViaLocation.GridReference.Northing))
                    {
                        //if any duplicates were found output an error message to the user.
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "The 'From' and 'Via' locations are identical.Please amend your choice by clicking 'Back' or 'New Location'"));
                        SetValidationError(tdSessionManager, ValidationErrorID.OriginAndViaOverlap, ORIGIN_AND_VIA_OVERLAP, true);
                    }

                    if ((toLocation.GridReference.Easting == cycleViaLocation.GridReference.Easting &&
                        toLocation.GridReference.Northing == cycleViaLocation.GridReference.Northing))
                    {
                        //if any duplicates were found output an error message to the user.
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            "The 'To' and 'Via' locations are identical.Please amend your choice by clicking 'Back' or 'New Location'"));
                        SetValidationError(tdSessionManager, ValidationErrorID.DestinationAndViaOverlap, DESTINATION_AND_VIA_OVERLAP, true);
                    }
                }

                // -- End of Cycle planner via location checks --

            }
        }

		#endregion
	}
}