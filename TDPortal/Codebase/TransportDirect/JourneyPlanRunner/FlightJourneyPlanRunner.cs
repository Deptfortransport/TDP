// ***********************************************
// NAME 		: FlightJourneyPlanRunner.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 24/05/2004
// DESCRIPTION 	: In charge of validating the user input and create a journey request for find a flight
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/FlightJourneyPlanRunner.cs-arc  $
//
//   Rev 1.2   Sep 01 2011 10:43:32   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Mar 10 2008 15:18:26   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for new resx / mcms loading logic
//
//   Rev 1.0   Nov 08 2007 12:24:42   mturner
//Initial revision.
//
//   Rev 1.21   Nov 09 2005 12:31:32   build
//Automatically merged from branch for stream2818
//
//   Rev 1.20.1.0   Oct 14 2005 15:10:46   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//
//   Rev 1.20   Jul 05 2005 13:36:50   asinclair
//Merge for stream2557
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.19.1.0   Jun 15 2005 14:07:10   asinclair
//Updated for CJP Architecture Changes
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.19   Mar 08 2005 14:45:00   rscott
//DEL 7 - Updated code for setting outwardDateTime and ReturnDateTime
//
//   Rev 1.18   Mar 01 2005 16:54:00   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.17   Jan 20 2005 11:00:52   RScott
//DEL 7 Update - PublicViaLocations changes
//
//   Rev 1.16   Nov 25 2004 11:08:26   jgeorge
//Changed to update DirectFlightsOnly property of TDJourneyRequest
//Resolution for 1785: City to City should only plan direct flights
//
//   Rev 1.15   Oct 15 2004 12:33:06   jgeorge
//Switched JourneyPlanControlData to JourneyPlanStateData.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.14   Sep 14 2004 16:38:26   jmorrissey
//IR1507 - added CheckLocationsForOverlapping method
//
//   Rev 1.13   Aug 23 2004 16:05:54   jgeorge
//IR1256
//
//   Rev 1.12   Aug 03 2004 16:09:58   RPhilpott
//Use new ITDSessionManager.IsFindAMode to determine if we are handling a trunk request.
//
//   Rev 1.11   Jul 28 2004 10:51:12   RPhilpott
//Removal of now-redundant FlightPlan parameter from ITDJourneyRequest (replaced by IsTRunkRequest for 6.1)
//
//   Rev 1.10   Jul 23 2004 18:26:36   RPhilpott
//DEL 6.1 Trunk Journey changes
//
//   Rev 1.9   Jul 08 2004 15:40:58   jgeorge
//Actioned review comments
//
//   Rev 1.8   Jun 24 2004 16:03:08   RPhilpott
//Fix Interval/Sequence bug
//
//   Rev 1.7   Jun 18 2004 14:56:32   RPhilpott
//Find-A-Flight validation - interim check-in.
//
//   Rev 1.6   Jun 16 2004 17:59:30   RPhilpott
//Find-A-Flight changes - interim check-in.
//
//   Rev 1.5   Jun 15 2004 13:26:04   RPhilpott
//Refactored for Find-A-Flight additions. 
//
//   Rev 1.4   Jun 10 2004 10:17:12   jgeorge
//Interim check in
//
//   Rev 1.3   Jun 09 2004 17:13:24   jgeorge
//Interim check in
//
//   Rev 1.2   Jun 02 2004 15:46:48   jgeorge
//Interim check in
//
//   Rev 1.1   Jun 02 2004 13:59:24   jgeorge
//Interim check in
//
//   Rev 1.0   May 26 2004 09:07:14   jgeorge
//Initial revision.

using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Resources;
using System.Threading;
using System.Web.SessionState;
using System.Xml.Serialization;
using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for FlightJourneyPlanRunner.
	/// </summary>
	public class FlightJourneyPlanRunner : JourneyPlanRunnerBase
	{

		#region Constructor
		
		public FlightJourneyPlanRunner(TDResourceManager resourceManager) : base(resourceManager)
		{
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// ValidateAndRun
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The journey parameters</param>
		public override bool ValidateAndRun( ITDSessionManager tdSessionManager, TDJourneyParameters tdJourneyParameters, string lang )
		{
			TDDateTime outwardDateTime = null;
			TDDateTime returnDateTime = null;

			TDJourneyParametersFlight tdJourneyParametersFlight = tdJourneyParameters as TDJourneyParametersFlight;

			if (tdJourneyParametersFlight == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersFlight - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("FlightJourneyPlanRunner requires a TDJourneyParametersFlight object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

			tdSessionManager.ValidationError = new ValidationError();

			listErrors = new ArrayList();
			errorMessages = new Hashtable();

			// NB The Find a Flight Input Page is used to report ALL user input errors

			// Perform date validations
			PerformDateValidations(tdSessionManager, tdJourneyParametersFlight, ref outwardDateTime, ref returnDateTime, lang, true);

			// Perform location validations
			PerformLocationValidations(tdSessionManager, tdJourneyParametersFlight);
			
			//IR1527 - check for overlapping naptans			
			CheckLocationsForOverlapping(tdSessionManager, tdJourneyParametersFlight);
			
			if (OriginAndDestinationAreValid())
			{
				// Perform operator and route validations, but only if the locations are valid
				PerformRouteValidations( tdSessionManager, tdJourneyParametersFlight);
			}

			// convert the arraylist in array of ErrorIDs
			tdSessionManager.ValidationError.ErrorIDs = (ValidationErrorID[])listErrors.ToArray(typeof(ValidationErrorID));
			tdSessionManager.ValidationError.MessageIDs = errorMessages;

			if ( tdSessionManager.ValidationError.ErrorIDs.Length == 0 )
			{
				// All input journey parameters were correctly formed so invoke the CJP Manager
				InvokeCJPManager(tdJourneyParametersFlight, outwardDateTime, returnDateTime, false, false);
				return true;
			}

			return false;
		}


		/// <summary>
		/// Validate And Run CJP for amendments - dummy needed to satisfy
		/// interface definition, but not relevant to FindAFlight requests. 
		/// </summary>
		/// <param name="journeyRequest"></param>
		public override bool ValidateAndRun(  int referenceNumber, int lastSequenceNumber, ITDJourneyRequest journeyRequest, ITDSessionManager tdSessionManager, string lang )
		{
			return false;
		}

        /// <summary>
        /// Validate And Run CJP for journey result modification - dummy needed to satisfy
        /// interface definition, but not relevant to FindAFlight requests. 
        /// </summary>
        /// <param name="referenceNumber"></param>
        /// <param name="lastSequenceNumber"></param>
        /// <param name="journeyRequest"></param>
        /// <returns></returns>
        public override bool ValidateAndRun(ITDSessionManager tdSessionManager, int referenceNumber, int lastSequenceNumber, TransportDirect.UserPortal.JourneyControl.ITDJourneyRequest journeyRequest)
        {
            return false;
        }

		public override ITDJourneyParameterConverter GetJourneyParamterConverter()
		{
			return new TDJourneyParametersFlightConverter();
		}

		#endregion

		#region Validations

		/// <summary>
		/// If direct flights, check whether for the supplied origin/destination airports, 
		/// there are valid routes, and that any operators selected fly those routes
		/// </summary>
		/// <param name="tdSessionManager"></param>
		/// <param name="tdJourneyParameters"></param>
		private void PerformRouteValidations(ITDSessionManager tdSessionManager, TDJourneyParametersFlight tdJourneyParameters)
		{

			IAirDataProvider airData = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];

			if (tdJourneyParameters.DirectFlightsOnly)
			{
				// Use the combinations of specified data to get the valid routes
				ArrayList validRoutes = airData.GetValidRoutes(tdJourneyParameters.OriginSelectedAirports(), tdJourneyParameters.DestinationSelectedAirports());

				if (validRoutes.Count == 0)
				{
					SetValidationError(tdSessionManager, ValidationErrorID.NoValidRoutes, NO_VALID_ROUTES, true);
				}
				else if ((tdJourneyParameters.SelectedOperators.Length == 0) && (tdJourneyParameters.OnlyUseSpecifiedOperators))
				{
					SetValidationError(tdSessionManager, ValidationErrorID.InvalidOperatorSelection, INVALID_OPERATOR_SELECTION, true);
				}
				else if (tdJourneyParameters.SelectedOperators.Length != 0)
				{
					// Validate the operator selection made for the route
					ArrayList validOperators = airData.GetRouteOperators((AirRoute[])validRoutes.ToArray(typeof(AirRoute)));
					
					int matches = 0;
					// Compare the array of operators with the selected ones
					for (int index = 0; index < tdJourneyParameters.SelectedOperators.Length; index++)
					{
						if (validOperators.Contains(airData.GetOperator(tdJourneyParameters.SelectedOperators[index])))
						{
							matches++;
						}
					}

					// If the user has said they only want to use the specified operators, we need a match.
					// Otherwise, we need there to not be fewer matches than selected operators

					if ((tdJourneyParameters.OnlyUseSpecifiedOperators && matches == 0)
						||	(!tdJourneyParameters.OnlyUseSpecifiedOperators && (matches >= validOperators.Count)))
					{
						SetValidationError(tdSessionManager, ValidationErrorID.InvalidOperatorSelection, INVALID_OPERATOR_SELECTION, true);
					}
				}
			}
		}

		/// <summary>
		/// Returns true if the origin and destination locations are both valid. Otherwise
		/// returns false.
		/// </summary>
		/// <returns></returns>
		private bool OriginAndDestinationAreValid()
		{
			return !(
				listErrors.Contains(ValidationErrorID.OriginLocationInvalidAndOtherErrors) ||
				listErrors.Contains(ValidationErrorID.OriginLocationInvalid) || 
				listErrors.Contains(ValidationErrorID.DestinationLocationInvalidAndOtherErrors) || 
				listErrors.Contains(ValidationErrorID.DestinationLocationInvalid) ||
				listErrors.Contains(ValidationErrorID.OriginAndDestinationOverlap) ||
				listErrors.Contains(ValidationErrorID.OriginAndViaOverlap) ||
				listErrors.Contains(ValidationErrorID.DestinationAndViaOverlap));
		}


		/// <summary>
		/// Perform the Location Validations
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The TD Journey Parameters</param>
		private void PerformLocationValidations(ITDSessionManager tdSessionManager, TDJourneyParametersFlight tdJourneyParameters)
		{
			// Check whether the Origin Location is valid
			if ( tdJourneyParameters.OriginLocation.Status != TDLocationStatus.Valid )
			{
				string msgResourceID = string.Empty;
				ValidationErrorID validationErrorID;

				if (foundNonLocationValidationError)
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
			if ( tdJourneyParameters.DestinationLocation.Status != TDLocationStatus.Valid )
			{
				string msgResourceID = string.Empty;
				ValidationErrorID validationErrorID;

				if (foundNonLocationValidationError)
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

			// Check whether the Via Location is unspecified (and stopover data has been supplied)
			if ( tdJourneyParameters.ViaLocation.Status == TDLocationStatus.Unspecified
				&& ( tdJourneyParameters.OutwardStopover != 0 || tdJourneyParameters.ReturnStopover != 0 ) )
			{
				string msgResourceID = string.Empty;
				ValidationErrorID validationErrorID;
				
				if (foundNonLocationValidationError)
				{
					msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE_AND_OTHER_ERRORS;
					validationErrorID = ValidationErrorID.PublicViaLocationInvalidAndOtherErrors;
				}
				else
				{
					msgResourceID = NO_MATCH_FROM_TO_VIA_ALTERNATE;
					validationErrorID = ValidationErrorID.PublicViaLocationInvalid;
				}
				SetValidationError(tdSessionManager, validationErrorID,  msgResourceID, true);
			}

			StringBuilder logMsg = new StringBuilder();

			if (tdJourneyParameters.OriginLocation.Status == TDLocationStatus.Valid)
			{
				if (tdJourneyParameters.OriginLocation.NaPTANs.Length == 0)
				{
					SetValidationError(tdSessionManager, ValidationErrorID.OriginLocationHasNoNaptan, LOCATION_HAS_NO_NAPTAN, true);
					
					if  (TDTraceSwitch.TraceVerbose)
					{
						logMsg.Append("No NAPTANs found for origin location " + tdJourneyParameters.OriginLocation.Description + " ");
					}
				}
			}
			
			if (tdJourneyParameters.DestinationLocation.Status == TDLocationStatus.Valid)
			{
				if (tdJourneyParameters.DestinationLocation.NaPTANs.Length == 0)
				{
					SetValidationError(tdSessionManager, ValidationErrorID.DestinationLocationHasNoNaptan, LOCATION_HAS_NO_NAPTAN, true);
					
					if  (TDTraceSwitch.TraceVerbose)
					{
						logMsg.Append("No NAPTANs found for destination location " + tdJourneyParameters.DestinationLocation.Description + " ");
					}
				}
			}

			if	(TDTraceSwitch.TraceVerbose && logMsg.Length > 0) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
			}
		}

		/// <summary>
		///IR1507 - validates the from, to and via locations to ensure that they do not have overlapping naptans 
		///this prevents journeys such as Scotland to Scotland passing validation
		/// </summary>
		/// <param name="tdSessionManager">The TD Session Manager</param>
		/// <param name="tdJourneyParameters">The TD Journey Parameters</param>
		private void CheckLocationsForOverlapping(ITDSessionManager tdSessionManager, TDJourneyParametersFlight tdJourneyParameters)
		{
			//location objects used for the origin and destination
			TDLocation fromLocation;
			TDLocation toLocation;	
			TDLocation viaLocation;

			//error message objects
			string msgResourceID= string.Empty;
			ValidationErrorID validationErrorID;
		
			//assign the start and destination locations 
			fromLocation = tdJourneyParameters.OriginLocation;			
			toLocation = tdJourneyParameters.DestinationLocation;	
			viaLocation =  tdJourneyParameters.ViaLocation;
									
			//check origin and destination locations for overlapping naptans
			if  (fromLocation.Intersects(toLocation,StationType.Airport)) 
			{					
				//if any overlaps were found output an error message to the user.
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"There are overlapping naptans for the origin and destination locations. Origin location: " + 
					tdJourneyParameters.OriginLocation + " Destination location: " 
					+ tdJourneyParameters.DestinationLocation ));

				msgResourceID = FLIGHT_ORIGIN_AND_DESTINATION_OVERLAP;
				validationErrorID = ValidationErrorID.OriginAndDestinationOverlap;
			
				SetValidationError(tdSessionManager, validationErrorID,  msgResourceID, true);
			}

			//check origin and via locations for overlapping naptans
			if  (fromLocation.Intersects(viaLocation,StationType.Airport)) 
			{					
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"There are overlapping naptans for the origin and via locations. Origin location: " + 
					tdJourneyParameters.OriginLocation + " Destination location: " 
					+ tdJourneyParameters.DestinationLocation ));

				msgResourceID = FLIGHT_ORIGIN_AND_VIA_OVERLAP;
				validationErrorID = ValidationErrorID.OriginAndViaOverlap;
			
				SetValidationError(tdSessionManager, validationErrorID,  msgResourceID, true);
			}

			//check destination and via locations for overlapping naptans
			if  (toLocation.Intersects(viaLocation,StationType.Airport)) 
			{					
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"There are overlapping naptans for the destination via locations. Origin location: " + 
					tdJourneyParameters.OriginLocation + " Destination location: " 
					+ tdJourneyParameters.DestinationLocation ));

				msgResourceID = FLIGHT_DESTINATION_AND_VIA_OVERLAP;
				validationErrorID = ValidationErrorID.DestinationAndViaOverlap;
			
				SetValidationError(tdSessionManager, validationErrorID,  msgResourceID, true);
			}							 
			
		}

		#endregion

	}
}