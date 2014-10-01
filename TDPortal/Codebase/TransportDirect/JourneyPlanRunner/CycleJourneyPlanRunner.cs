// ***********************************************
// NAME 		: CycleJourneyPlanRunner.cs
// AUTHOR 		: Mitesh Modi
// DATE CREATED : 17/09/2003
// DESCRIPTION 	: Journey Plan Runner class. In charge of validating the user input and create a journey request
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/CycleJourneyPlanRunner.cs-arc  $
//
//   Rev 1.11   Sep 01 2011 10:43:30   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.10   May 06 2010 14:34:56   pghumra
//Backed out changes in version 1.8 & 1.9
//Resolution for 5522: CODE FIX - NEW - Del 10.11 - Cycle planner point validation switch
//
//   Rev 1.7   Oct 27 2008 15:52:46   mmodi
//Removed comment
//
//   Rev 1.6   Sep 08 2008 15:48:40   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Aug 22 2008 10:12:26   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Aug 06 2008 14:50:24   mmodi
//Updated as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 04 2008 10:24:28   mmodi
//Updated to check for cycle planner switch
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Jul 28 2008 13:02:14   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 13:59:34   mmodi
//Updates as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:38:58   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;
using System.Web;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    public class CycleJourneyPlanRunner : JourneyPlanRunnerBase
    {
        #region Private members
        /// <summary>
        /// Controls whether date validation should be performed
        /// </summary>
        private bool validateDates = true;

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="resourceManager"></param>
        public CycleJourneyPlanRunner(TDResourceManager resourceManager) : base(resourceManager)
		{
		}

		#endregion

        #region Public methods
        /// <summary>
        /// ValidateAndRun. This overloaded version allows date validation to be ignored for cases where
        /// dates are not set by user input, e.g. for planning journey extensions. 
        /// </summary>
        /// <param name="tdSessionManager">The TD Session Manager</param>
        /// <param name="tdJourneyParameters">The journey parameters</param>
        /// <param name="lang"></param>
        /// <param name="validateDates">True if data validation required, false otherwise</param>
        /// <returns></returns>
        public bool ValidateAndRun(ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters, string lang, bool validateDates)
        {
            this.validateDates = validateDates;
            return ValidateAndRun(tdSessionManager, tdJourneyParameters, lang);
        }

        /// <summary>
		/// ValidateAndRun
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The journey parameters</param>
        /// <param name="lang">Language</param>
        public override bool ValidateAndRun(ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters, string lang)
        {
            TDDateTime outwardDateTime = null;
            TDDateTime returnDateTime = null;

            TDJourneyParametersMulti tdJourneyParametersMulti = tdJourneyParameters as TDJourneyParametersMulti;

            if (tdJourneyParametersMulti == null)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
                throw new TDException("CycleJourneyPlanRunner requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
            }

            tdSessionManager.ValidationError = new ValidationError();

            listErrors = new ArrayList();
            errorMessages = new Hashtable();

            
            // Ensure cycle planner is available, prevents a user navigating to the cycle planner input and submitting
            // a journey, even though we have turned off all navigation links to the page
            try
            {
                bool cyclePlannerAvailable = bool.Parse(Properties.Current["CyclePlanner.Available"]);

                if (!cyclePlannerAvailable)
                    throw new Exception();
            }
            catch
            {
                // cycle planner is not availble. Add error to session and return
                SetValidationError(tdSessionManager, ValidationErrorID.CyclePlannerUnavailable, CYCLE_PLANNER_UNAVAILABLE, true);
                tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
                tdSessionManager.ValidationError.MessageIDs = errorMessages;
                return false;
            }


            //
            // Perform cycle mode validations
            //
            PerformCycleModeValidations(tdSessionManager, tdJourneyParametersMulti);

            //
            // Perform date validations
            //
            PerformDateValidations(tdSessionManager, tdJourneyParametersMulti, ref outwardDateTime, ref returnDateTime, lang, validateDates);

            //
            // Perform location validations
            //
            PerformLocationValidations(tdSessionManager, tdJourneyParametersMulti);
            
            // convert the arraylist in array of ErrorIDs
            tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
            tdSessionManager.ValidationError.MessageIDs = errorMessages;

            // Split validation 1 to reduce time overhead
            if (tdSessionManager.ValidationError.ErrorIDs.Length == 0)
            {
                //
                // Check if locations are too far apart
                //
                CheckLocationsDistance(tdSessionManager, tdJourneyParametersMulti);

                // convert the arraylist in array of ErrorIDs
                tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
                tdSessionManager.ValidationError.MessageIDs = errorMessages;
            }

            // Split validation 2 to reduce time overhead
            if (tdSessionManager.ValidationError.ErrorIDs.Length == 0)
            {
                //
                // Perform cycle location specific validations
                //
                PerformCycleLocationValidations(tdSessionManager, tdJourneyParametersMulti);

                //
                // Perform cycle preference validations
                //
                PerformCyclePreferenceValidations(tdSessionManager, tdJourneyParametersMulti);

                //
                // Check for overlapping naptans
                //
                CheckLocationsForOverlapping(tdSessionManager, tdJourneyParametersMulti, tdJourneyParametersMulti.CycleViaLocation);
                
                //
                // Check for duplication
                //
                CheckLocationsForDuplication(tdSessionManager, tdJourneyParametersMulti);

                // convert the arraylist in array of ErrorIDs
                tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
                tdSessionManager.ValidationError.MessageIDs = errorMessages;

            }

            if (tdSessionManager.ValidationError.ErrorIDs.Length == 0)
            {
                // All input journey parameters were correctly formed so invoke the Cycle planner manager
                InvokeCyclePlannerManager(tdJourneyParametersMulti, outwardDateTime, returnDateTime);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool ValidateAndRun(int referenceNumber, int lastSequenceNumber, TransportDirect.UserPortal.JourneyControl.ITDJourneyRequest journeyRequest, TransportDirect.UserPortal.SessionManager.ITDSessionManager tdSessionManager, string lang)
        {
            //InvokeCJPManager(referenceNumber, lastSequenceNumber, journeyRequest);
            return true;
        }

        /// <summary>
        /// ValidateAndRun method not getting used 
        /// </summary>
        /// <param name="referenceNumber"></param>
        /// <param name="lastSequenceNumber"></param>
        /// <param name="journeyRequest"></param>
        /// <returns></returns>
        public override bool ValidateAndRun(ITDSessionManager tdSessionManager, int referenceNumber, int lastSequenceNumber, TransportDirect.UserPortal.JourneyControl.ITDJourneyRequest journeyRequest)
        {
            return true;
        }

        /// <summary>
        /// Return a TDJourneyParametersMultiConverter.
        /// DO NOT USE IF POPULATING A CYCLEPLANNERREQUEST
        /// </summary>
        public override ITDJourneyParameterConverter GetJourneyParamterConverter()
        {
            return new TDJourneyParametersMultiConverter(TDSessionManager.Current.IsFindAMode);
        }

        #endregion

        #region Validations

        /// <summary>
        /// Perform the cycle mode validation
        /// </summary>
        /// <param name="tdSessionManager">The TD Session Manager</param>
        /// <param name="tdJourneyParameters">The TD Journey Parameters</param>
        private void PerformCycleModeValidations(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
        {
            // If CycleRequired is false
            if (tdJourneyParameters.CycleRequired == false)
            {
                // As user doesnt have an option to select cycle, we'll update the parameter automatically,
                // given that we've called the Cycle validate and run
                if (tdJourneyParameters.PublicModes.Length == 0)
                {
                    tdJourneyParameters.PublicModes = new ModeType[] { ModeType.Cycle };
                }
                else
                {
                    // Check for cycle mode, and add if it is not there.
                    ArrayList modes = new ArrayList(tdJourneyParameters.PublicModes);
                    if (!modes.Contains(ModeType.Cycle))
                        modes.Add(ModeType.Cycle);

                    tdJourneyParameters.PublicModes = (ModeType[])modes.ToArray(typeof(ModeType));
                }

                tdJourneyParameters.CycleRequired = true;
            }
        }

        /// <summary>
        /// Performs specific cycle location validation for properties required
        /// </summary>
        /// <param name="tdSessionManager"></param>
        /// <param name="tdJourneyParameters"></param>
        private void PerformCycleLocationValidations(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
        {
            if (tdJourneyParameters.CycleRequired)
            {
                TDLocation fromLocation = tdJourneyParameters.OriginLocation;
                TDLocation toLocation = tdJourneyParameters.DestinationLocation;
                TDLocation cycleViaLocation = tdJourneyParameters.CycleViaLocation;

                #region Validate Points
                if (fromLocation.Status == TDLocationStatus.Valid)
                {
                    // Check if we have the coordinate Point needed for a cycle journey request
                    if ((fromLocation.Point == null)
                        || (fromLocation.Point.X < 0)
                        || (fromLocation.Point.Y < 0))
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.OriginLocationHasNoPoint, LOCATION_HAS_NO_POINT, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            string logMsg = "CyclePlanner - CycleJourneyPlanRunner. No coordinate Points found for cycle origin location " + tdJourneyParameters.OriginLocation.Description;

                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                        }
                    }
                }

                if (toLocation.Status == TDLocationStatus.Valid)
                {
                    if ((toLocation.Point == null)
                        || (toLocation.Point.X < 0)
                        || (toLocation.Point.Y < 0))
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.DestinationLocationHasNoPoint, LOCATION_HAS_NO_POINT, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            string logMsg = "CyclePlanner - CycleJourneyPlanRunner. No coordinate Points found for cycle destination location " + tdJourneyParameters.DestinationLocation.Description;

                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                        }
                    }
                }

                if ((cycleViaLocation != null) && (cycleViaLocation.Status == TDLocationStatus.Valid))
                {
                    if ((cycleViaLocation.Point == null)
                        || (cycleViaLocation.Point.X < 0)
                        || (cycleViaLocation.Point.Y < 0))
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.CycleViaLocationHasNoPoint, LOCATION_HAS_NO_POINT, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            string logMsg = "CyclePlanner - CycleJourneyPlanRunner. No coordinate Points found for cycle via location " + tdJourneyParameters.CycleViaLocation.Description;

                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                        }
                    }
                }
                #endregion

                // Only spend time checking points if there are no errors to avoid GIS call overhead
                if (listErrors.Count == 0)
                {
                    #region Valiate Points in Cycle Data areas

                    bool validatePoints = bool.Parse(Properties.Current["CyclePlanner.Planner.PointValidation.Switch"]);

                    if (validatePoints)
                    {
                        // Get all the points
                        ArrayList pointsArray = new ArrayList();
                        pointsArray.Add(fromLocation.Point);
                        pointsArray.Add(toLocation.Point);

                        if ((cycleViaLocation != null) && (cycleViaLocation.Status == TDLocationStatus.Valid))
                            pointsArray.Add(cycleViaLocation.Point);

                        Point[] points = (Point[])pointsArray.ToArray(typeof(Point));

                        // First check: make sure the Points are in valid cycle data areas
                        IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                        if (!gisQuery.IsPointsInCycleDataArea(points, false))
                        {
                            SetValidationError(tdSessionManager, ValidationErrorID.LocationInInvalidCycleArea, LOCATION_IN_INVALID_CYCLE_AREA, true);

                            #region Log
                            if (TDTraceSwitch.TraceVerbose)
                            {
                                StringBuilder logMsg = new StringBuilder();
                                logMsg.Append("CyclePlanner - CycleJourneyPlanRunner. Location points are not in a valid cycle data area: ");
                                foreach (Point point in points)
                                {
                                    logMsg.Append(point.X);
                                    logMsg.Append(",");
                                    logMsg.Append(point.Y);
                                    logMsg.Append(" ");
                                }

                                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
                            }
                            #endregion
                        }
                        else
                        {
                            // Second check: if plan in Same area flag
                            bool planInSameCycleArea = bool.Parse(Properties.Current["CyclePlanner.Planner.PointValidation.PlanSameAreaOnly"]);

                            if ((planInSameCycleArea) && (!gisQuery.IsPointsInCycleDataArea(points, true)))
                            {
                                SetValidationError(tdSessionManager, ValidationErrorID.LocationPointsNotInSameCycleArea, LOCATION_POINTS_NOT_IN_SAME_CYCLE_AREA, true);

                                #region Log
                                if (TDTraceSwitch.TraceVerbose)
                                {
                                    StringBuilder logMsg = new StringBuilder();
                                    logMsg.Append("CyclePlanner - CycleJourneyPlanRunner. Location points are not in the same cycle data area: ");
                                    foreach (Point point in points)
                                    {
                                        logMsg.Append(point.X);
                                        logMsg.Append(",");
                                        logMsg.Append(point.Y);
                                        logMsg.Append(" ");
                                    }

                                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion
                }
                 
            }
        }

        /// <summary>
        /// Performs validation of the cycle preferences
        /// </summary>
        /// <param name="tdSessionManager"></param>
        /// <param name="tdJourneyParameters"></param>
        private void PerformCyclePreferenceValidations(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
        {
            string msgResourceID = string.Empty;
            ValidationErrorID validationErrorID;

            if (tdJourneyParameters.CycleSpeedValid == false)
            {
                msgResourceID = CYCLE_SPEED_ENTERED_INVALID;
                validationErrorID = ValidationErrorID.InvalidCycleSpeed;
                SetValidationError(tdSessionManager, validationErrorID, msgResourceID, true);
            }
        }

        /// <summary>
        /// Checks the distance between the locations to a configured value
        /// </summary>
        /// <param name="tdSessionManager"></param>
        /// <param name="tdJourneyParameters"></param>
        private void CheckLocationsDistance(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
        {
            int maxJourneyDistanceMetres = Convert.ToInt32(Properties.Current["CyclePlanner.Planner.MaxJourneyDistance.Metres"]);

            TDLocation fromLocation = tdJourneyParameters.OriginLocation;
            TDLocation toLocation = tdJourneyParameters.DestinationLocation;
            TDLocation cycleViaLocation = tdJourneyParameters.CycleViaLocation;

            if (!HasViaLocation(tdJourneyParameters))
            {
                // Check distance betweeen from and to only
                if (fromLocation.GridReference.DistanceFrom(toLocation.GridReference) > maxJourneyDistanceMetres)
                {
                    SetValidationError(tdSessionManager, ValidationErrorID.DistanceBetweenLocationsTooGreat, DISTANCE_BETWEEN_LOCATIONS_TOO_GREAT, true);

                    #region Log
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        string logMsg = "CyclePlanner - CycleJourneyPlanRunner. Distance between cycle locations "
                            + fromLocation.Description + " and " + toLocation.Description
                            + " is greater than " + maxJourneyDistanceMetres + " metres";

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                    }
                    #endregion
                }
            }
            else
            {
                // Check distance between locations and via location
                if ((cycleViaLocation.GridReference.DistanceFrom(fromLocation.GridReference) > maxJourneyDistanceMetres)
                    ||
                    (cycleViaLocation.GridReference.DistanceFrom(toLocation.GridReference) > maxJourneyDistanceMetres))
                {
                    SetValidationError(tdSessionManager, ValidationErrorID.DistanceBetweenLocationsAndViaTooGreat, DISTANCE_BETWEEN_LOCATIONS_AND_VIA_TOO_GREAT, true);

                    #region Log
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        string logMsg = "CyclePlanner - CycleJourneyPlanRunner. Distance between cycle locations Origin/Destination" 
                            + fromLocation.Description + " / " + toLocation.Description
                            + " and the Via location " + cycleViaLocation.Description
                            + " is greater than " + maxJourneyDistanceMetres + " metres";

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                    }
                    #endregion
                }
            }
             
        }

        /// <summary>
        /// Checks if user has requested a via location
        /// </summary>
        /// <param name="tdJourneyParameters"></param>
        /// <returns></returns>
        private bool HasViaLocation(TDJourneyParametersMulti tdJourneyParameters)
        {
            // Check whether the Public Via Location is valid (and input text has been filled)
            if ((tdJourneyParameters.CycleViaLocation != null)
                && (tdJourneyParameters.CycleViaLocation.Status == TDLocationStatus.Valid)
                && (tdJourneyParameters.CycleVia.InputText.Length != 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Invoke the CyclePlannerManager
        /// <summary>
        /// Invoke the Cycle planner manager (asynchronously!)
        /// </summary>
        /// <param name="tdJourneyParameters">The validated user's Journey Parameters</param>
        /// <param name="outwardDateTime">The Date/Time of the outward journey</param>
        /// <param name="returnDateTime">The Date/Time of the return journey (may be null for one-way journeys)</param>
        private void InvokeCyclePlannerManager(TDJourneyParameters tdJourneyParameters, TDDateTime outwardDateTime, TDDateTime returnDateTime)
        {
            ITDSessionManager sessionManager = TDSessionManager.Current;

            //Set the RequestID and Status properties
            sessionManager.AsyncCallState.RequestID = Guid.NewGuid();
            sessionManager.AsyncCallState.Status = AsyncCallStatus.InProgress;

            //Call the SaveData method of the SessionManager
            sessionManager.SaveData();

            TDJourneyParametersCycleConverter converter = new TDJourneyParametersCycleConverter();

            string lang = Thread.CurrentThread.CurrentUICulture.ToString();

            // The xslt file needed to transform a CycleJourney in to the Map Cycle Journey xml
            // (This has to be passed as a parameter because we are using asynchronous/threading and therefore
            // the CyclePlannerManager will not have a HttpContext)
            string polylinesTransformXslt = HttpContext.Current.Server.MapPath(Properties.Current["CyclePlanner.InteractiveMapping.Map.CycleJourney.Xslt"]);

            //Obtain an instance of CyclePlanRunnerCaller from TDServiceDiscovery
            ICycleJourneyPlanRunnerCaller cycleCaller = (ICycleJourneyPlanRunnerCaller)TDServiceDiscovery.Current[ServiceDiscoveryKey.CycleJourneyPlanRunnerCaller];

            CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();

            //call invoke method.      
            cycleCaller.InvokeCyclePlannerManager(
            	sessionInfo, sessionManager.Partition,
				lang, sessionManager.AsyncCallState.RequestID, tdJourneyParameters,
                converter, outwardDateTime, returnDateTime, polylinesTransformXslt);

        }
        #endregion
    }
}
