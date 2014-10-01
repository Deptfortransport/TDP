// ***********************************************
// NAME 		: InternationalJourneyPlanRunner.cs
// AUTHOR 		: Amit Patel
// DATE CREATED : 04 Feb 2010
// DESCRIPTION 	: Journey Plan Runner class. In charge of validating the user input and create a journey request for International Journey
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/InternationalJourneyPlanRunner.cs-arc  $
//
//   Rev 1.8   Sep 01 2011 10:43:34   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.7   Mar 05 2010 13:04:22   mmodi
//Updated to validate date is today or later
//Resolution for 5435: TD Extra - Script 001: Journeys displayed for Past dates
//
//   Rev 1.6   Feb 26 2010 11:27:38   mmodi
//Only perform permitted journey check if there are no errors up to that check
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.5   Feb 20 2010 19:29:12   mmodi
//Updated error message
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Feb 16 2010 21:58:30   apatel
//added custom location validation for the International planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Feb 12 2010 11:13:28   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Feb 11 2010 08:53:14   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 10 2010 10:15:56   apatel
//Updated to remove redundunt date validation as there is no return date
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 09:37:04   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using System.Collections;
using TransportDirect.Common.PropertyService.Properties;
using System.Threading;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
    public class InternationalJourneyPlanRunner: JourneyPlanRunnerBase
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
        public InternationalJourneyPlanRunner(TDResourceManager resourceManager)
            : base(resourceManager)
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
                throw new TDException("InternationalJourneyPlanRunner requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
            }

            tdSessionManager.ValidationError = new ValidationError();

            listErrors = new ArrayList();
            errorMessages = new Hashtable();

            
            // Ensure international planner is available, prevents a user navigating to the international planner input and submitting
            // a journey, even though we have turned off all navigation links to the page
            try
            {
                bool internationalPlannerAvailable = bool.Parse(Properties.Current["InternationalPlanner.Available"]);

                if (!internationalPlannerAvailable)
                    throw new Exception();
            }
            catch
            {
                // international planner is not availble. Add error to session and return
                SetValidationError(tdSessionManager, ValidationErrorID.InternationalPlannerUnavailable, INTERNATIONAL_PLANNER_UNAVAILABLE, true);
                tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
                tdSessionManager.ValidationError.MessageIDs = errorMessages;
                return false;
            }


            //
            // Perform international mode validations
            //
            PerformInternationalModeValidations(tdSessionManager, tdJourneyParametersMulti);

            //
            // Perform date validations
            //
            PerformInternationalDateValidations(tdSessionManager, tdJourneyParametersMulti, ref outwardDateTime, lang, validateDates);

            //
            // Perform location validations
            //
            PerformLocationValidations(tdSessionManager, tdJourneyParametersMulti);

            #region Replace specfic error message resource id if added

            if (errorMessages.Contains(ValidationErrorID.OriginLocationHasNoNaptan))
            {
                errorMessages[ValidationErrorID.OriginLocationHasNoNaptan] = INTERNATIONAL_LOCATION_HAS_NO_NAPTAN;
            }
            if (errorMessages.Contains(ValidationErrorID.DestinationLocationHasNoNaptan))
            {
                errorMessages[ValidationErrorID.DestinationLocationHasNoNaptan] = INTERNATIONAL_LOCATION_HAS_NO_NAPTAN;
            }

            #endregion

            //
            // Perform location validation for overlap/duplicate 
            //
            CheckLocationsForOverlapping(tdSessionManager, tdJourneyParametersMulti);

            // Only check if journey pair is permitted if no errors found up to this point,
            // prevents UI showing this error message when it might be something else previously 
            // causing the validation failure
            if (listErrors.Count == 0)
            {
                //
                // Perform International planner journey location pair validation
                //
                PerformPermittedInternationalJourney(tdSessionManager, tdJourneyParametersMulti);
            }
            
            // convert the arraylist in array of ErrorIDs
            tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
            tdSessionManager.ValidationError.MessageIDs = errorMessages;
                        
            if (tdSessionManager.ValidationError.ErrorIDs.Length == 0)
            {
                // All input journey parameters were correctly formed so invoke the International planner manager
                InvokeInternationalPlannerManager(tdJourneyParametersMulti, outwardDateTime, returnDateTime);
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
        /// </summary>
        public override ITDJourneyParameterConverter GetJourneyParamterConverter()
        {
            return new TDJourneyParametersMultiConverter(TDSessionManager.Current.IsFindAMode);
        }

               
        #endregion

        #region Validations

        /// <summary>
        /// Perform Date/Time Validations
        /// </summary>
        /// <param name="tdSessionManager">The TD Session Manager</param>
        /// <param name="tdJourneyParameters">The TD Journey Parameters</param>
        /// <param name="outwardDateTime">The date/time of the outward journey</param>
        /// <param name="returnDateTime">The date/time of the return journey</param>
        /// <param name="lang">Current UI language</param>
        /// <param name="logDateErrors">Whether to log errors found</param>
        protected void PerformInternationalDateValidations(ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters, ref TDDateTime outwardDateTime, string lang, bool logDateErrors)
        {

            // Handle specific International planner scenario for setting the Outward Time only
            string outwardTime = string.Empty;
           
            // Check that the mandatory outward date/time is valid
            try
            {
                // Set up the outward date time
                outwardTime = Properties.Current["InternationalPlanner.Journey.StartTime"]; //"00:00";

                if ((outwardTime.IndexOf(':') < 0) && (outwardTime.Trim().Length == 4))
                {
                    outwardTime = outwardTime.Trim().Substring(0, 2) + ":" + outwardTime.Trim().Substring(2, 2);
                }
                
                outwardDateTime = TDDateTime.Parse(tdJourneyParameters.OutwardDayOfMonth + " " + tdJourneyParameters.OutwardMonthYear + " " + outwardTime, Thread.CurrentThread.CurrentCulture);

                // Set date time now, ignoring the time
                TDDateTime tdDateTimeNow = TDDateTime.Now;
                tdDateTimeNow = new TDDateTime(tdDateTimeNow.Year, tdDateTimeNow.Month, tdDateTimeNow.Day);

                // Check that the mandatory outward date/time is for today or later
                if (outwardDateTime < tdDateTimeNow)
                {
                    // Add a validation error item to indicate that the outward date/time was not later than "now"
                    foundNonLocationValidationError = true;
                    SetValidationError(tdSessionManager, ValidationErrorID.OutwardDateTimeNotLaterThanNow,
                        DATE_TIME_IS_IN_THE_PAST, logDateErrors);
                }

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
                logOutMsg.Append(", Return Date Time: " + tdJourneyParameters.ReturnDayOfMonth + "/" + tdJourneyParameters.ReturnMonthYear);
                logOutMsg.Append(" " + tdJourneyParameters.ReturnHour + ":" + tdJourneyParameters.ReturnMinute);
                logOutMsg.Append(", Culture = " + Thread.CurrentThread.CurrentCulture);
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logOutMsg.ToString()));
                #endregion
                #endregion
            }
           
            // Return not Required
            tdJourneyParameters.IsReturnRequired = false;
            
        }

        /// <summary>
        /// Perform the international mode validation
        /// </summary>
        /// <param name="tdSessionManager">The TD Session Manager</param>
        /// <param name="tdJourneyParameters">The TD Journey Parameters</param>
        private void PerformInternationalModeValidations(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
        {
            if (tdJourneyParameters.PublicRequired)
            {
                foreach (ModeType mode in tdJourneyParameters.PublicModes)
                {
                    if (mode != ModeType.Air && mode != ModeType.Coach && mode != ModeType.Rail)
                    {
                        SetValidationError(tdSessionManager, ValidationErrorID.InternationalPlannerInvalidMode, INTERNATIONAL_PLANNER_MODE_NOT_PERMITTED, true);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            string logMsg = string.Format("InternatioalPlanner - InternationalJourneyPlanRunner.  Journey not permitted with mode {0}",
                                mode);

                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Perform the international location pair validation
        /// </summary>
        /// <param name="tdSessionManager">The TD Session Manager</param>
        /// <param name="tdJourneyParameters">The TD Journey Parameters</param>
        private void PerformPermittedInternationalJourney(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
        {
            //Obtain an instance of InternationalJourneyPlanRunnerCaller from TDServiceDiscovery
            IInternationalJourneyPlanRunnerCaller ipCaller = (IInternationalJourneyPlanRunnerCaller)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalJourneyPlanRunnerCaller];
                        
            //call invoke method.      
            if (!ipCaller.IsPermittedInternationalJourney(tdJourneyParameters))
            {
                SetValidationError(tdSessionManager, ValidationErrorID.InternationalPlannerJourneyNotPermitted, INTERNATIONAL_PLANNER_JOURNEY_NOT_PERMITTED, true);

                if (TDTraceSwitch.TraceVerbose)
                {
                    string logMsg = string.Format("InternatioalPlanner - InternationalJourneyPlanRunner.  Journey not permitted between {0} and {1}", 
                        tdJourneyParameters.OriginLocation.Description, tdJourneyParameters.DestinationLocation.Description);

                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg));
                }
            }
        }

        /// <summary>
        ///validates the from and to locations to ensure that they do not have overlapping naptans 
        ///this prevents journeys such as Paris to Paris passing validation
        /// </summary>
        /// <param name="tdSessionManager">The TD Session Manager</param>
        /// <param name="tdJourneyParameters">The TD Journey Parameters</param>
        private void CheckLocationsForOverlapping(ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters)
        {
            //location objects used for the origin and destination
            TDLocation fromLocation;
            TDLocation toLocation;
            
            //error message objects
            string msgResourceID = string.Empty;
            ValidationErrorID validationErrorID;

            //assign the start and destination locations 
            fromLocation = tdJourneyParameters.OriginLocation;
            toLocation = tdJourneyParameters.DestinationLocation;
           
            //check origin and destination locations for overlapping naptans
            if (LocationIntersects(fromLocation, toLocation))
            {
                //if any overlaps were found output an error message to the user.
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "There are overlapping naptans for the origin and destination locations. Origin location: " +
                    tdJourneyParameters.OriginLocation + " Destination location: "
                    + tdJourneyParameters.DestinationLocation));

                msgResourceID = ORIGIN_AND_DESTINATION_OVERLAP;
                validationErrorID = ValidationErrorID.OriginAndDestinationOverlap;

                SetValidationError(tdSessionManager, validationErrorID, msgResourceID, true);
            }

        }

        /// <summary>
        /// Checks to see if two TDLocations intersect
        /// </summary>
        /// <param name="fromTDLocation">From Location - TDLocation</param>
        /// <param name="toTDLocation">To Location = TDLocation</param>
        /// <returns>true if the NapTANs intersect</returns>
        private bool LocationIntersects(TDLocation fromTDLocation, TDLocation toTDLocation)
        {
            if (fromTDLocation != null && toTDLocation != null)
            {
                foreach (TDNaptan naptanItem in fromTDLocation.NaPTANs)
                {
                    
                    foreach (TDNaptan otherNaptanItem in toTDLocation.NaPTANs)
                    {
                        if (CheckNaPTANsEquals(naptanItem, otherNaptanItem))
                        {
                            return true;
                        }
                    }
                    
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="naptanItem"></param>
        /// <param name="otherNaptanItem"></param>
        /// <returns></returns>
        private bool CheckNaPTANsEquals(TDNaptan fromNaptanItem, TDNaptan toNaptanItem)
        {
            if ((fromNaptanItem.Naptan.Equals(toNaptanItem.Naptan))
                ||(fromNaptanItem.Naptan.Contains(toNaptanItem.Naptan))
                || (toNaptanItem.Naptan.Contains(fromNaptanItem.Naptan)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Invoke the InternationalPlannerManager
        /// <summary>
        /// Invoke the International planner manager (asynchronously!)
        /// </summary>
        /// <param name="tdJourneyParameters">The validated user's Journey Parameters</param>
        /// <param name="outwardDateTime">The Date/Time of the outward journey</param>
        /// <param name="returnDateTime">The Date/Time of the return journey (may be null for one-way journeys)</param>
        private void InvokeInternationalPlannerManager(TDJourneyParameters tdJourneyParameters, TDDateTime outwardDateTime, TDDateTime returnDateTime)
        {
            ITDSessionManager sessionManager = TDSessionManager.Current;

            //Set the RequestID and Status properties
            sessionManager.AsyncCallState.RequestID = Guid.NewGuid();
            sessionManager.AsyncCallState.Status = AsyncCallStatus.InProgress;

            //Call the SaveData method of the SessionManager
            sessionManager.SaveData();


            TDJourneyParametersMultiConverter multiConverter = new TDJourneyParametersMultiConverter(true);

            string lang = Thread.CurrentThread.CurrentUICulture.ToString();

            
            //Obtain an instance of InternationalJourneyPlanRunnerCaller from TDServiceDiscovery
            IInternationalJourneyPlanRunnerCaller ipCaller = (IInternationalJourneyPlanRunnerCaller)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalJourneyPlanRunnerCaller];

            CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();

            //call invoke method.      
            ipCaller.InvokeInternationalPlannerManager(
            	sessionInfo, sessionManager.Partition,
                lang, sessionManager.AsyncCallState.RequestID, tdJourneyParameters, multiConverter, outwardDateTime, returnDateTime);

        }
        #endregion
    }
    
}
