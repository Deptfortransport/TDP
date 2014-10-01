// ***********************************************
// NAME 		: JourneyPlanRunner.cs
// AUTHOR 		: Callum
// DATE CREATED : 17/09/2003
// DESCRIPTION 	: Journey Plan Runner class. In charge of validating the user input and create a journey request
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/JourneyPlanRunner.cs-arc  $
//
//   Rev 1.5   Jan 20 2013 16:25:50   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.4   Sep 02 2011 12:46:06   apatel
//Real Time Car Unit Test code update
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.3   Sep 01 2011 10:43:34   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.2   Oct 13 2008 16:46:14   build
//Automatically merged from branch for stream5014
//
//   Rev 1.1.1.0   Jun 20 2008 15:01:18   mmodi
//Updated for cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Mar 10 2008 15:18:26   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//   Rev 1.0   Nov 08 2007 12:24:44   mturner
//Initial revision.
//
//   Rev 1.77   Oct 06 2006 10:56:46   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.76.1.0   Oct 03 2006 11:26:18   tmollart
//Modified validation checking to check for car parks that are the same across all possible location inputs.
//Resolution for 4211: Car Parking: Journey can be planned where From and To are same car park
//
//   Rev 1.76   Sep 19 2006 12:26:30   PScott
//SCR = 4158
//vantive 4061289
//Error message for duplicate From/To locations
//
//   Rev 1.75   Mar 30 2006 12:17:12   build
//Automatically merged from branch for stream0018
//
//   Rev 1.74.1.0   Feb 16 2006 16:38:54   mguney
//CheckLocationsForCompleteness method changed in order to prevent execution when CityInterchanges values exist.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.74   Nov 09 2005 12:31:32   build
//Automatically merged from branch for stream2818
//
//   Rev 1.73.1.0   Oct 14 2005 15:10:44   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//
//   Rev 1.73   Jul 05 2005 13:44:38   asinclair
//Merge for stream2557 
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.72.1.2   Jun 30 2005 16:42:52   asinclair
//Changes made after FXCop
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.72.1.1   Jun 21 2005 14:55:08   asinclair
//Added comments
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.72.1.0   Jun 15 2005 14:08:30   asinclair
//Updated for CJP Architecture Changes
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.72   Apr 20 2005 12:14:40   Ralavi
//Changes to ensure blank fuel consumption and fuel cost text box cause an error message to be displayed.
//
//   Rev 1.71   Apr 13 2005 14:18:26   Ralavi
//Added if condition to avoid converting average fuel consumption and fuel cost values in the database table since these values are already in the converted units
//
//   Rev 1.70   Apr 13 2005 13:52:10   Ralavi
//fixed the problem where validation was done for fuel consumption when find a train or coach were used which consequently lead to crash page appearing
//
//   Rev 1.69   Apr 13 2005 12:19:50   Ralavi
//Adding new method to validate fuel cost and fuel consumption
//
//   Rev 1.68   Apr 06 2005 12:08:56   Ralavi
//Converting FuelConsumption before sending it to CJP
//
//   Rev 1.67   Mar 30 2005 19:12:32   RAlavi
//Ensuring that journey is not planned when there is the same road in both avoid road and use road boxes
//
//   Rev 1.66   Mar 30 2005 16:31:36   RAlavi
//Adding code to prevent moving on to journey summary page if avoid road or use roads are not validated.
//
//   Rev 1.65   Mar 24 2005 16:39:12   RAlavi
//Added code for doNotUseMotorway so that the correct value is passed to CJP.
//
//   Rev 1.64   Mar 16 2005 11:16:58   RAlavi
//Checked in after changes for passing fuel consumption and fuel costs
//
//   Rev 1.63   Mar 14 2005 11:32:26   esevern
//These contain a variety of sterilised and sealed items of equipment, such as syringes, needles and suture materials, and may be purchased through a pharmacist, private medical centre, or from a number of other suppliers. They should normally be handed to a doctor or nurse for use in a medical emergency in a country where the safety of such items cannot be assured.
//
//A typical kit should contain:
//Added setting of car size and fuel type in journey request
//
//
//   Rev 1.62   Mar 09 2005 09:40:06   RAlavi
//Modified for car costing
//
//   Rev 1.61   Mar 01 2005 16:54:04   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.60   Feb 24 2005 11:37:34   PNorell
//Updated for favourite details.
//
//   Rev 1.59   Jan 19 2005 14:45:20   RScott
//DEL 7 - PublicViaLocation removed and PublicViaLocations[ ], PublicSoftViaLocations[ ], PublicNotViaLocations[] added.
//
//   Rev 1.58   Nov 25 2004 11:08:30   jgeorge
//Changed to update DirectFlightsOnly property of TDJourneyRequest
//Resolution for 1785: City to City should only plan direct flights
//
//   Rev 1.57   Nov 12 2004 12:00:58   jgeorge
//Bug fix - check for overlapping modes only if locations are valid.
//
//   Rev 1.56   Nov 03 2004 16:16:40   jgeorge
//Modified validation for overlapping locations in FindTrunk mode.
//
//   Rev 1.55   Oct 15 2004 12:33:22   jgeorge
//Switched JourneyPlanControlData to JourneyPlanStateData
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.54   Sep 21 2004 15:16:08   jmorrissey
//Update to CheckLocationsForOverlapping
//Resolution for 1603: Find nearest: Error message is not quite accurate in the action specified.
//
//   Rev 1.53   Sep 16 2004 17:48:08   jmorrissey
//Updated setting of validation errors in CheckLocationsForOverlapping method
//
//   Rev 1.52   Sep 14 2004 17:04:26   jmorrissey
//IR1527 - added CheckLocationsForOverlapping method
//
//   Rev 1.51   Sep 13 2004 16:15:38   RHopkins
//IR1484 Add new attributes to the JourneyRequest for ReturnOriginLocation and ReturnDestinationLocation to allow Extensions to be made to/from Return locations that differ from the corresponding Outward location.
//
//   Rev 1.50   Sep 13 2004 12:17:58   jmorrissey
//IR1527 - added CheckLocationsForOverlapping method
//
//   Rev 1.49   Aug 19 2004 19:48:50   RPhilpott
//Set ExtraCheckinTime to DateTime.MinValue()
//Resolution for 1400: ExtraCheckinTime not being set for all Flight requests
//
//   Rev 1.48   Aug 18 2004 17:17:40   passuied
//Set OutwardAnyTime and ReturnAnyTime in BuildJourneyRequest, since this was never done
//Resolution for 1371: Find A Train. Can't select AnyTime in Results page - Amend Date
//
//   Rev 1.47   Aug 03 2004 16:09:58   RPhilpott
//Use new ITDSessionManager.IsFindAMode to determine if we are handling a trunk request.
//
//   Rev 1.46   Jul 23 2004 18:26:38   RPhilpott
//DEL 6.1 Trunk Journey changes
//
//   Rev 1.45   Jun 25 2004 12:25:08   RPhilpott
//Make support for operator selection accessible to multi-modal journeys as well as flights.
//
//   Rev 1.44   Jun 23 2004 16:35:58   COwczarek
//Add overload for ValidateAndRun method that can ignore date validation
//Resolution for 1044: Add date validation to extend journey functionality
//
//   Rev 1.43   Jun 15 2004 13:26:14   RPhilpott
//Refactored for Find-A-Flight additions. 
//
//   Rev 1.42   May 26 2004 09:03:34   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.41   Apr 15 2004 13:43:24   COwczarek
//Set IsReturnRequired flag in journey parameters if return date/time set. Only default return minutes to "00" if a return hour specified.
//Resolution for 746: Date validation for return journeys behaves strangely
//
//   Rev 1.40   Apr 06 2004 12:22:36   COwczarek
//Fix date validation to correctly default return time minutes to "00" when not supplied by user
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.39   Apr 02 2004 16:45:54   COwczarek
//Changes to date and location validation to support more detailed error reporting on the ambiguity page (DEL 5.2 QA changes)
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.38   Mar 15 2004 19:51:34   geaton
//IR653 - log valid "user logged on" data based on session data.
//
//   Rev 1.37   Mar 12 2004 16:37:14   ESevern
//DEL5.2 corrected check for missing return time
//
//   Rev 1.36   Mar 12 2004 15:07:42   ESevern
//DEL5.2 error message handling for date ambiguity
//
//   Rev 1.35   Feb 11 2004 12:09:14   PNorell
//Uses new improved map handoff for public journeys
//
//   Rev 1.34   Nov 26 2003 11:48:56   passuied
//Use LanguageConverter class to get the Language to pass to CJPManager
//Resolution for 397: Wrong language passed to JW
//
//   Rev 1.33   Nov 26 2003 10:42:10   PNorell
//Updated error handling and logging.
//
//   Rev 1.32   Nov 23 2003 18:16:24   RPhilpott
//Add extra exception handling and diagnostics for IR276.
//Resolution for 276: Run time error
//
//   Rev 1.31   Nov 17 2003 10:12:08   passuied
//fix for ambiguity error message when one or many locations are unspecified and one or many locations are ambiguous.
//Resolution for 231: Ambiguous AND unspecified location errors on page --> bad message
//
//   Rev 1.30   Nov 14 2003 11:25:44   passuied
//minor change
//
//   Rev 1.29   Nov 07 2003 13:52:28   passuied
//changes after fxcop
//
//   Rev 1.28   Nov 05 2003 14:11:20   PNorell
//Corrected logging and open return issues.
//
//   Rev 1.27   Nov 04 2003 10:22:24   RPhilpott
//Typo.
//
//   Rev 1.26   Nov 04 2003 09:52:02   RPhilpott
//Ensure PrivateAlgorithm is set.
//
//   Rev 1.25   Nov 03 2003 18:17:06   passuied
//fixed location validation
//
//   Rev 1.24   Oct 16 2003 14:05:08   passuied
//minor changes
//
//   Rev 1.23   Oct 15 2003 13:30:20   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.22   Oct 13 2003 11:36:40   passuied
//fixed bug so message HasNoNaptan is displayed
//
//   Rev 1.21   Oct 13 2003 11:10:42   RPhilpott
//Only do NAPTAN/Locality/OSGR checking for otherwise valid origin/destination 
//
//   Rev 1.20   Oct 11 2003 19:32:36   RPhilpott
//Correct NAPTAN validation, add similar Locality validation, log missing OSGR's
//
//   Rev 1.19   Oct 09 2003 19:51:50   PNorell
//Removed logging.
//Corrected saving order.
//
//   Rev 1.18   Oct 09 2003 14:03:12   PNorell
//Arranged to find locality for any td location.
//Fixed so amend uses the correct journey request object.
//
//   Rev 1.17   Oct 09 2003 10:55:12   RPhilpott
//Correct driving speed
//
//   Rev 1.16   Oct 08 2003 17:56:04   RPhilpott
//Fixes for CJP input parameters
//
//   Rev 1.15   Sep 26 2003 17:20:42   AToner
//Removed the DataServices populator.
//
//   Rev 1.14   Sep 25 2003 19:59:16   RPhilpott
//Add ESRI map handoff for non-amended journeys.
//
//   Rev 1.13   Sep 25 2003 18:06:44   PNorell
//Ensured everything is linked up together.
//Fixed various small bugs.
//
//   Rev 1.12   Sep 25 2003 11:32:34   PNorell
//Added logging reference.
//Changed to use interface instead of concrete implementation.
//
//   Rev 1.11   Sep 24 2003 15:03:54   passuied
//added instantiation of JourneyRequest
//
//   Rev 1.10   Sep 23 2003 14:06:38   RPhilpott
//Changes to TDJourneyResult (ctor and referenceNumber)
//
//   Rev 1.9   Sep 23 2003 02:06:44   passuied
//corrected datevalidation
//
//   Rev 1.8   Sep 22 2003 17:52:58   passuied
//updated
//
//   Rev 1.7   Sep 20 2003 17:08:46   RPhilpott
//Change in TDLocation.NaPTANs definition
//
//   Rev 1.6   Sep 19 2003 10:32:30   PNorell
//Changed interface to return boolean for run and validate. True is returned when it validates properly.
//Added check for alternative & origin/destination naptans has to exist if it is a public journey.
//
//   Rev 1.5   Sep 18 2003 13:23:46   PNorell
//Fixed culture to use the correct one.
//
//   Rev 1.4   Sep 18 2003 13:03:22   passuied
//Corrected access to ReturnTime and OutwardTime (interface is now separated in Hour and Minute)
//
//   Rev 1.3   Sep 18 2003 11:05:10   PNorell
//Corrected spelling
//Corrected code according to the DD document
//Fixed reference to session manager.
//Fixed possible race-condition

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
using TransportDirect.Presentation.InteractiveMapping;

using Logger = System.Diagnostics.Trace;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for JourneyPlanRunner.
	/// </summary>
	public class JourneyPlanRunner : JourneyPlanRunnerBase
	{
		/// <summary>
		/// Controls whether date validation should be performed
		/// </summary>
		private bool validateDates = true;
		
		#region Constructor
		public JourneyPlanRunner(TDResourceManager resourceManager) : base(resourceManager)
		{
		}
		#endregion


		#region Public Methods

		/// <summary>
		/// ValidateAndRun. This overloaded version allows date validation to be ignored for cases where
		/// dates are not set by user input, e.g. for planning journey extensions. 
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The journey parameters</param>
		/// <param name="lang"></param>
		/// <param name="validateDates">True if data validation required, false otherwise</param>
		/// <returns></returns>
		public bool ValidateAndRun( ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters, string lang, bool validateDates )
		{
			this.validateDates = validateDates;
			return ValidateAndRun(tdSessionManager,tdJourneyParameters,lang);
		}

		/// <summary>
		/// ValidateAndRun
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The journey parameters</param>
		public override bool ValidateAndRun( ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters, string lang)
		{
		
			TDDateTime outwardDateTime = null;
			TDDateTime returnDateTime = null;			

			TDJourneyParametersMulti tdJourneyParametersMulti = tdJourneyParameters as TDJourneyParametersMulti;

			if (tdJourneyParametersMulti == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("JourneyPlanRunner requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

			tdSessionManager.ValidationError = new ValidationError();

			listErrors = new ArrayList();
			errorMessages = new Hashtable();

			//
			// Check if locations need populating with Naptans 
			//
			if	(tdSessionManager.IsFindAMode && tdJourneyParametersMulti.PublicRequired)
			{
				CheckLocationsForCompleteness(ref tdJourneyParametersMulti);
			}

			//
			// Perform public/private mode validations
			//
			PerformPublicPrivateModeValidations(tdSessionManager, tdJourneyParametersMulti);
            
			//
			// Perform date validations
			//
			PerformDateValidations(tdSessionManager, tdJourneyParametersMulti, ref outwardDateTime, ref returnDateTime, lang, validateDates);

			//
			// Perform location validations
			//
			PerformLocationValidations(tdSessionManager, tdJourneyParametersMulti);


			PerformRoadValidations(tdSessionManager, tdJourneyParametersMulti);

			
			PerformFuelDetailValidations(tdSessionManager, tdJourneyParametersMulti);

			
			//IR1527 - check for overlapping naptans			
			CheckLocationsForOverlapping(tdSessionManager, tdJourneyParametersMulti, tdJourneyParametersMulti.PublicViaLocation);
			
			// convert the arraylist in array of ErrorIDs
			tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
			tdSessionManager.ValidationError.MessageIDs = errorMessages;

			// 4061289  check for identical easting/northings if not already errored.
			if (tdSessionManager.ValidationError.ErrorIDs.Length == 0)
			{
				CheckLocationsForDuplication(tdSessionManager, tdJourneyParametersMulti);
				
				// convert the arraylist in array of ErrorIDs
				tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
				tdSessionManager.ValidationError.MessageIDs = errorMessages;

			}

			if (tdSessionManager.ValidationError.ErrorIDs.Length == 0)
			{
				// All input journey parameters were correctly formed so invoke the CJP Manager
				InvokeCJPManager(tdJourneyParametersMulti, outwardDateTime, returnDateTime, false, false);
				return true;
			} 
			else 
			{
				return false;
			}

			
		}

		
		/// <summary>
		/// Return a TDJourneyParametersMultiConverter
		/// </summary>
		public override ITDJourneyParameterConverter GetJourneyParamterConverter()
		{
			return new TDJourneyParametersMultiConverter(TDSessionManager.Current.IsFindAMode);
		}

		/// <summary>
		/// Validate And Run a CJP - in fact no validation will occur as this is invoked for 
		/// amendments and the journey parameters will have already been validated.
		/// </summary>
		/// <param name="journeyRequest"></param>
		public override bool ValidateAndRun(  int referenceNumber, int lastSequenceNumber, ITDJourneyRequest journeyRequest, ITDSessionManager tdSessionManager, string lang )
		{
			InvokeCJPManager(referenceNumber, lastSequenceNumber, journeyRequest, false);
			return true;
		}


        /// <summary>
        /// Validate And Run a CJP - in fact no validation will occur as this is invoked for 
        /// journey result modification and the journey parameters will have already been validated.
        /// </summary>
        /// <param name="journeyRequest"></param>
        public override bool ValidateAndRun(ITDSessionManager tdSessionManager, int referenceNumber, int lastSequenceNumber, ITDJourneyRequest journeyRequest)
        {
            tdSessionManager.ValidationError = new ValidationError();
            
            listErrors = new ArrayList();
            errorMessages = new Hashtable();


            PerformToidsValidations(tdSessionManager, journeyRequest);

            // convert the arraylist in array of ErrorIDs
            tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
            tdSessionManager.ValidationError.MessageIDs = errorMessages;

            if (tdSessionManager.ValidationError.ErrorIDs.Length == 0)
            {
                InvokeCJPManager(referenceNumber, lastSequenceNumber, journeyRequest, true);
                return true;
            }
            else
            {
                return false;
            }
        }
                
                
		#endregion

		#region Validations

		/// <summary>
		/// Check that origin and destination locations have been populated 
		/// with Naptans -- if not, we need to use FindNearestStations to 
		/// populate them with at least one Naptan of the relevant mode(s).
		///  
		/// Only called within this class - public to facilitate NUnit testing 
		/// </summary>
		/// <param name="tdJourneyParameters">The TD Journey Parameters</param>
		public void CheckLocationsForCompleteness(ref TDJourneyParametersMulti tdJourneyParameters)
		{
			//This method should not be called if the origin CityInterchanges array has a length of > 0.
			if ((tdJourneyParameters.OriginLocation.CityInterchanges != null) &&
				(tdJourneyParameters.OriginLocation.CityInterchanges.Length > 0))
				return;
			
			// Note that we can only get here for a public trunk request, 
			//  implying that only possible modes are train, coach, or air 			
			StationType[] stationTypes = new StationType[tdJourneyParameters.PublicModes.Length];
			
			int i = 0;

			foreach (ModeType mode in tdJourneyParameters.PublicModes)
			{
				switch (mode)
				{
					case ModeType.Air:
						stationTypes[i] = StationType.Airport;
						i++;
						break;

					case ModeType.Rail:
						stationTypes[i] = StationType.Rail;
						i++;
						break;

					case ModeType.Coach:
						stationTypes[i] = StationType.Coach;
						i++;
						break;
				}
			}

			if	(tdJourneyParameters.OriginLocation.Status == TDLocationStatus.Valid
				&& tdJourneyParameters.OriginLocation.NaPTANs.Length == 0)
			{
				TDLocation origin = tdJourneyParameters.OriginLocation;
				LocationSearch.FindStations(ref origin, stationTypes);
				tdJourneyParameters.OriginLocation = origin;
			}

			if	(tdJourneyParameters.DestinationLocation.Status == TDLocationStatus.Valid
				&& tdJourneyParameters.DestinationLocation.NaPTANs.Length == 0)
			{
				TDLocation destination = tdJourneyParameters.DestinationLocation;
				LocationSearch.FindStations(ref destination, stationTypes);
				tdJourneyParameters.DestinationLocation = destination;
			}

		}

        /// <summary>
		/// Perform the public/private mode validations
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The TD Journey Parameters</param>
		private void PerformPublicPrivateModeValidations(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
		{
			// If PublicRequired is false AND PrivateRequired is false
			if (tdJourneyParameters.PublicRequired == false && tdJourneyParameters.PrivateRequired == false)
			{
				// Indicate error if no modes selected
				if (tdJourneyParameters.PublicModes.Length == 0)
				{
					foundNonLocationValidationError = true;
					SetValidationError(tdSessionManager, ValidationErrorID.NoModesSelected, CHOOSE_AN_OPTION, true);
				}
				else
				{
					// At least one mode specified, so indicate PublicRequired
					tdJourneyParameters.PublicRequired = true;
				}
			}

			// If PublicRequired and no modes, assume all modes
			if (tdJourneyParameters.PublicRequired == true)
			{
				// Check whether any modes have been selected
				if (tdJourneyParameters.PublicModes.Length == 0)
				{
					// Assume train, tram, bus, ferry, underground, air
					tdJourneyParameters.PublicModes = new ModeType[] { 
                        ModeType.Air, ModeType.Bus, ModeType.Coach, ModeType.Ferry, 
                        ModeType.Metro, ModeType.Rail, ModeType.Telecabine, ModeType.Tram, ModeType.Underground};
				}
			}
		}

		private void PerformRoadValidations(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
		{
			// Check whether the Origin Location is ambiguous

			for (int i = 0; i < tdJourneyParameters.AvoidRoadsList.Length; i++)
			{
										 
				if ( tdJourneyParameters.AvoidRoadsList[i].Status == TDRoadStatus.Ambiguous )
				{
					SetValidationError(tdSessionManager, ValidationErrorID.AmbiguousOriginLocation, AMBIGUOUS_FROM_TO_VIA_ALTERNATE, true);
					foundNonLocationValidationError = true;
				}


			}
			


			for (int i = 0; i < tdJourneyParameters.UseRoadsList.Length; i++)
			{
										 
				if ( tdJourneyParameters.UseRoadsList[i].Status == TDRoadStatus.Ambiguous )
				{
					SetValidationError(tdSessionManager, ValidationErrorID.AmbiguousOriginLocation, AMBIGUOUS_FROM_TO_VIA_ALTERNATE, true);
					foundNonLocationValidationError = true;
				}
			}

			for (int i = 0; i < tdJourneyParameters.AvoidRoadsList.Length; i++)
			{
				string msgResourceID = string.Empty;
				ValidationErrorID validationErrorID;
				int useIndex = 0;

				
				for (int k = 0; k < tdJourneyParameters.AvoidRoadsList.Length; k++)
				{
					for (int j = 0; j < tdJourneyParameters.UseRoadsList.Length; j++)
					{
						if (tdJourneyParameters.AvoidRoadsList[k].RoadName == tdJourneyParameters.UseRoadsList[j].RoadName)
						
						{
							useIndex = -1;
						}
					}
				}
				
				// Check whether the Origin Location is valid
				if (( tdJourneyParameters.AvoidRoadsList[i].Status == TDRoadStatus.Unspecified )||
					( useIndex == -1))
					
				{
					
					if ( foundNonLocationValidationError == true )
					{
						msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS;
						validationErrorID = ValidationErrorID.OriginLocationInvalidAndOtherErrors;
					}
					else
					{
						msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE;
						validationErrorID = ValidationErrorID.PrivateViaLocationInvalid;
					}
					SetValidationError(tdSessionManager, validationErrorID,  msgResourceID, true);
				

				}
				
			}

			for (int i = 0; i < tdJourneyParameters.UseRoadsList.Length; i++)
			{
				string msgResourceID = string.Empty;
				ValidationErrorID validationErrorID;
				
				// Check whether the Origin Location is valid
				
				if ( tdJourneyParameters.UseRoadsList[i].Status == TDRoadStatus.Unspecified ) 	
				{
					
					if ( foundNonLocationValidationError == true )
					{
						msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS;
						validationErrorID = ValidationErrorID.OriginLocationInvalidAndOtherErrors;
					}
					else
					{
						msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE;
						validationErrorID = ValidationErrorID.PrivateViaLocationInvalid;
					}
					SetValidationError(tdSessionManager, validationErrorID,  msgResourceID, true);
				

				}
				
			}


		
		}
	
		private void PerformFuelDetailValidations(ITDSessionManager tdSessionManager, TDJourneyParametersMulti tdJourneyParameters)
		{
			string msgResourceID =string.Empty;
			ValidationErrorID validationErrorID;

		 
			if ( tdJourneyParameters.FuelConsumptionValid == false )
			{
				msgResourceID = FUEL_CONSUMPTION_ENTERED_INVALID;
				validationErrorID = ValidationErrorID.InvalidFuelConsumption;
				SetValidationError(tdSessionManager, validationErrorID,  msgResourceID, true);
			}
				 
			if ( tdJourneyParameters.FuelCostValid == false )
			{
				msgResourceID = FUEL_COST_ENTERED_INVALID;
				validationErrorID = ValidationErrorID.InvalidFuelCost;
				SetValidationError(tdSessionManager, validationErrorID,  msgResourceID, true);
			}
		}

        /// <summary>
        /// Validates the toids at the origin and destination
        /// If any of the origin or destination location have toids in the banned list then they
        /// will not be used in the request to the CJP. If there are no toids left after removing the banned
        /// toids at the origin or destination then validation will fail.
        /// </summary>
        /// <param name="tdSessionManager"></param>
        /// <param name="journeyRequest"></param>
        private void PerformToidsValidations(ITDSessionManager tdSessionManager, ITDJourneyRequest journeyRequest)
        {
            string msgResourceID = string.Empty;
            ValidationErrorID validationErrorID;

            List<string> originToids = new List<string>(journeyRequest.OriginLocation.Toid);
            List<string> destinationToids = new List<string>(journeyRequest.DestinationLocation.Toid);

            if (journeyRequest.IsOutwardRequired)
            {
                
                foreach (string toid in journeyRequest.AvoidToidsOutward)
                {
                    if (originToids.Contains(toid))
                    {
                        originToids.Remove(toid);
                    }

                    if (destinationToids.Contains(toid))
                    {
                        destinationToids.Remove(toid);
                    }
                }

                
            }

            if (journeyRequest.IsReturnRequired)
            {
                foreach (string toid in journeyRequest.AvoidToidsReturn)
                {
                    if (originToids.Contains(toid))
                    {
                        originToids.Remove(toid);
                    }

                    if (destinationToids.Contains(toid))
                    {
                        destinationToids.Remove(toid);
                    }
                }

                
            }

            // if origin and destination location have toids after removing the banned toids
            // update the locations with only toids which are not in the banned list
            if (originToids.Count > 0 && destinationToids.Count > 0)
            {
                journeyRequest.OriginLocation.Toid = originToids.ToArray();
                journeyRequest.DestinationLocation.Toid = destinationToids.ToArray();
            }
            // else return a validation message
            else
            {
                msgResourceID = ROUTE_AFFECTED_BY_CLOSURES_ERRORS;
                validationErrorID = ValidationErrorID.RouteAffectedByClosuresErrors;

                SetValidationError(tdSessionManager, validationErrorID, msgResourceID, true);
            }

            
        }
		#endregion

	}
}