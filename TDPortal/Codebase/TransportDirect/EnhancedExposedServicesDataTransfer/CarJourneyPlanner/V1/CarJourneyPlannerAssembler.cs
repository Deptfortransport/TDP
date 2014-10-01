// *********************************************** 
// NAME                 : CarJourneyPlannerAssembler.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Assembler class containing methods for converting between Domain and Data Transfer objects
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/CarJourneyPlannerAssembler.cs-arc  $
//
//   Rev 1.1   Mar 14 2011 15:11:50   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.0   Aug 04 2009 14:41:12   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyEmissions;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

using cjpInterface = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Assembler class containing methods for converting between Domain and Data Transfer objects
    /// </summary>
    public sealed class CarJourneyPlannerAssembler
    {
        public static string DEFAULT_LANGUAGE = "en-GB";

        #region Private members

        private static IDataServices populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
        private static CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarCostCalculator];

        #endregion

        #region Public methods

        #region Request 

        /// <summary>
        /// Method uses parameters from a dto request object to create a domain request object
        /// </summary>
        public static ITDJourneyRequest CreateTDJourneyRequest(CarJourneyRequest carJourneyRequest, JourneyRequest journey)
        {
            // At a minimum, the request needs to contain at least one journey to plan
            if ((carJourneyRequest != null) && (journey != null))
            {
                ITDJourneyRequest tdJourneyRequest = new TDJourneyRequest();

                // Set date time values
                tdJourneyRequest.IsReturnRequired = journey.IsReturnRequired;
                tdJourneyRequest.OutwardArriveBefore = journey.OutwardArriveBefore;
                tdJourneyRequest.ReturnArriveBefore = journey.ReturnArriveBefore;

                // Convert datetime parameters into a TDDateTime parameter
                TDDateTime outwardTDDateTime = new TDDateTime(journey.OutwardDateTime);
                tdJourneyRequest.OutwardDateTime = new TDDateTime[] { outwardTDDateTime };
                TDDateTime returnTDDateTime = new TDDateTime(journey.ReturnDateTime);
                tdJourneyRequest.ReturnDateTime = new TDDateTime[] { returnTDDateTime };

                // Set the car parameters
                SetDefaultCarParameters(ref tdJourneyRequest);
                UpdateCarParameters(ref tdJourneyRequest, carJourneyRequest.CarParameters);

                return tdJourneyRequest;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Result

        /// <summary>
        /// Method which creates a ResultJourney dto object containing the car journeys for the JourneyId specified.
        /// If an error message is supplied, this is added to the ErrorMessages list.
        /// </summary>
        public static JourneyResult CreateJourneyResultDT(int journeyId,
            ITDJourneyRequest tdJourneyRequest, ITDJourneyResult tdJourneyResult,
            ResultSettings resultSettings, TDResourceManager rm, 
            bool outwardArriveBefore, bool returnArriveBefore, 
            string errorMessage, TDExceptionIdentifier tdExId)
        {
            // The return dto object
            JourneyResult journeyResultDTO = new JourneyResult();

            ArrayList errorMessageDT = new ArrayList();
            ArrayList warningMessageDT = new ArrayList();

            journeyResultDTO.JourneyId = journeyId;

            // If theres a result object, then create the outward and/or return car journey
            if (tdJourneyResult != null)
            {
                if (tdJourneyResult.IsValid)
                {
                    #region Create car journeys

                    // Create a car cost helper class. This is used to calculate the car cost for both
                    // the outward and return journeys
                    CarCostHelper carCostHelper = new CarCostHelper(true, tdJourneyRequest, tdJourneyResult, rm);
                    
                    // Used to track congestion charge being added across outward and return journey
                    bool congestionChargeAdded = false;
                    ArrayList visitedCongestionCompany = new ArrayList();
                                        
                    if (tdJourneyResult.OutwardRoadJourneyCount > 0)
                    {
                        // There will only be one summary line for the car journey
                        JourneySummaryLine[] jsl = tdJourneyResult.OutwardJourneySummary(outwardArriveBefore);

                        journeyResultDTO.OutwardCarJourney = CreateCarJourneyDT(
                            true,
                            tdJourneyResult.OutwardRoadJourney(),
                            jsl[0],
                            tdJourneyRequest,
                            tdJourneyResult,
                            resultSettings,
                            carCostHelper,
                            rm,
                            ref congestionChargeAdded,
                            ref visitedCongestionCompany);
                    }

                    if (tdJourneyResult.ReturnRoadJourneyCount > 0)
                    {
                        // Change the car cost helper to the return journey mode
                        carCostHelper.Outward = false;

                        // There will only be one summary line for the car journey
                        JourneySummaryLine[] jsl = tdJourneyResult.ReturnJourneySummary(outwardArriveBefore);

                        journeyResultDTO.ReturnCarJourney = CreateCarJourneyDT(
                            false,
                            tdJourneyResult.ReturnRoadJourney(),
                            jsl[0],
                            tdJourneyRequest,
                            tdJourneyResult,
                            resultSettings,
                            carCostHelper,
                            rm,
                            ref congestionChargeAdded,
                            ref visitedCongestionCompany);
                    }

                    #endregion
                }

                #region Add warnings and errors

                // Add any user warnings or error messages
                if (tdJourneyResult.CJPMessages.Length != 0)
                {
                    foreach (CJPMessage cjpMessage in tdJourneyResult.CJPMessages)
                    {
                        if (cjpMessage.Type == ErrorsType.Warning)
                        {
                            warningMessageDT.Add(
                                CommonAssembler.CreateMessageDT(rm.GetString(cjpMessage.MessageResourceId), (int)TDExceptionIdentifier.JPWarningMessage));
                        }
                        else if (cjpMessage.Type == ErrorsType.Error)
                        {
                            errorMessageDT.Add(
                                CommonAssembler.CreateMessageDT(rm.GetString(cjpMessage.MessageResourceId), (int)TDExceptionIdentifier.JPCJPErrorsOccured));
                        }
                    }
                }

                #endregion
            }

            #region Assign warnings and errors

            // If an error message has been supplied, assign that to the error messages list
            if (!string.IsNullOrEmpty(errorMessage))
            {
                #region Add errors

                errorMessageDT.Add(CommonAssembler.CreateMessageDT(errorMessage, (int)tdExId));

                #endregion
            }

            // Convert and assign warning and error messages
            if (warningMessageDT.Count > 0)
            {
                journeyResultDTO.UserWarnings = (Message[])warningMessageDT.ToArray(typeof(Message));
            }

            if (errorMessageDT.Count > 0)
            {
                journeyResultDTO.ErrorMessages = (Message[])errorMessageDT.ToArray(typeof(Message));
            }

            #endregion

            return journeyResultDTO;
        }
                

        /// <summary>
        /// Method which creates a CarJourneyResult dto object for the ResultJourney
        /// </summary>
        public static CarJourneyResult CreateCarJourneyResultDT(JourneyResult[] journeyResults)
        {
            CarJourneyResult carJourneyResultDTO = new CarJourneyResult();

            carJourneyResultDTO.JourneyResults = journeyResults;

            #region Create CompletionStatus object

            if (journeyResults != null)
            {
                bool allJourneysOK = true;

                // Check if any of the result objects have any error messages, if yes, then
                // the CompletionStatus is failed
                foreach (JourneyResult jr in journeyResults)
                {
                    if ((jr.ErrorMessages != null) && (jr.ErrorMessages.Length > 0))
                    {
                        allJourneysOK = false;
                        break;
                    }
                }

                if (allJourneysOK)
                {
                    carJourneyResultDTO.CompletionStatus = CommonAssembler.CreateCompletionStatusDT(true, 0, string.Empty);
                }
                else
                {
                    carJourneyResultDTO.CompletionStatus = CommonAssembler.CreateCompletionStatusDT(false, (int)TDExceptionIdentifier.JPFailedToPlanAllJourneys,
                        "Currently unable to obtain a complete set of journeys using the request parameters supplied. See each journey for error messages.");
                }

            }
            else
            {
                // This error should not be reached, there should always be a result journey, otherwise exception 
                // would have been thrown to user by calling class
                // Added for completeness.
                carJourneyResultDTO.CompletionStatus = CommonAssembler.CreateCompletionStatusDT(false, (int)TDExceptionIdentifier.JPFailedToPlanJourney,
                    "Currently unable to obtain journey options using the request parameters supplied.");
            }

            #endregion

            return carJourneyResultDTO;
        }

        /// <summary>
        /// Method which creates a CarCost dto object for the parameters specified
        /// </summary>
        /// <returns></returns>
        public static CarCost CreateCarCostDT(CarCostType carCostType, double costValue, string description, string companyName, string companyURL)
        {
            CarCost carCost = new CarCost();

            carCost.CostType = carCostType;
            carCost.Cost = costValue;

            if (!string.IsNullOrEmpty(description))
                carCost.Description = description;

            if (!string.IsNullOrEmpty(companyName))
                carCost.CompanyName = companyName;

            if (!string.IsNullOrEmpty(companyURL))
                carCost.CompanyURL = companyURL;

            return carCost;
        }

        #endregion

        #region Result Settings

        /// <summary>
        /// Method to validate the ResultSettings object. If null or invalid, then settings are 
        /// initialised to their default values
        /// </summary>
        public static ResultSettings ValidateResultSettingsDT(ResultSettings resultSettings)
        {
            // Create a default result settings object
            ResultSettings rs = new ResultSettings();

            rs.ResultType = ResultType.Detailed;
            rs.DistanceUnit = DistanceUnit.Miles;
            rs.Language = DEFAULT_LANGUAGE;

            if (resultSettings != null)
            {
                // Update with actual settings in the request
                if (resultSettings.ResultTypeSpecified)
                    rs.ResultType = resultSettings.ResultType;

                if (resultSettings.DistanceUnitSpecified)
                    rs.DistanceUnit = resultSettings.DistanceUnit;

                if (resultSettings.LanguageSpecified)
                    rs.Language = resultSettings.Language;
            }

            return rs;
        }

        #endregion

        #endregion

        #region Private methods

        #region Request

        /// <summary>
        /// Initialises the car parameters to the default values
        /// </summary>
        /// <param name="tdJourneyRequest"></param>
        private static void SetDefaultCarParameters(ref ITDJourneyRequest tdJourneyRequest)
        {
            #region Car parameters

            // Only want a car journey
            tdJourneyRequest.Modes = new cjpInterface.ModeType[] { cjpInterface.ModeType.Car };

            // Read defaults from data services
            try
            {
                tdJourneyRequest.DrivingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop), CultureInfo.CurrentCulture);
                tdJourneyRequest.CarSize = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
                tdJourneyRequest.FuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
            }
            catch
            {
                // If populator hasn't been set up correctly, then manually set defaults (e.g. for nunit tests)
                tdJourneyRequest.DrivingSpeed = 112;
                tdJourneyRequest.CarSize = "medium";
                tdJourneyRequest.FuelType = "petrol";
            }

            // Calculate fuel consumption and cost
            tdJourneyRequest.FuelConsumption = costCalculator.GetFuelConsumption(tdJourneyRequest.CarSize, tdJourneyRequest.FuelType).ToString(CultureInfo.InvariantCulture);
            tdJourneyRequest.FuelPrice = costCalculator.GetFuelCost(tdJourneyRequest.FuelType).ToString(CultureInfo.InvariantCulture);

            // Set journey algorithm type
            tdJourneyRequest.PrivateAlgorithm = cjpInterface.PrivateAlgorithmType.Fastest;
            tdJourneyRequest.VehicleType = cjpInterface.VehicleType.Car;

            // Set avoid flags
            tdJourneyRequest.AvoidMotorways = false;
            tdJourneyRequest.AvoidFerries = false;
            tdJourneyRequest.AvoidTolls = false;
            tdJourneyRequest.DoNotUseMotorways = false;
            tdJourneyRequest.BanUnknownLimitedAccess = false;

            // Set roads to use/avoid
            tdJourneyRequest.AvoidRoads = new string[0];
            tdJourneyRequest.IncludeRoads = new string[0];

            #endregion

            #region Public parameters

            // Set the default Public Transport parameters, only for completeness, the CJP should ignore
            try
            {
                tdJourneyRequest.InterchangeSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.ChangesSpeedDrop), CultureInfo.CurrentCulture);
                tdJourneyRequest.MaxWalkingTime = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.WalkingMaxTimeDrop), CultureInfo.CurrentCulture);
                tdJourneyRequest.WalkingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.WalkingSpeedDrop), CultureInfo.CurrentCulture);
            }
            catch
            {
                // If populator hasn't been set up correctly, then manually set defaults (e.g. for nunit tests)
                tdJourneyRequest.InterchangeSpeed = 0;
                tdJourneyRequest.MaxWalkingTime = 25;
                tdJourneyRequest.WalkingSpeed = 80;
            }

            tdJourneyRequest.PublicAlgorithm = cjpInterface.PublicAlgorithmType.Default;

            tdJourneyRequest.UseOnlySpecifiedOperators = false;
            tdJourneyRequest.RoutingGuideInfluenced = false;
            tdJourneyRequest.RoutingGuideCompliantJourneysOnly = false;
                       
            tdJourneyRequest.SelectedOperators = new string[0];
            tdJourneyRequest.RouteCodes = string.Empty;

            tdJourneyRequest.ExtraCheckinTime = DateTime.MinValue;

            #endregion
        }

        /// <summary>
        /// Populates a TDJourneyRequest with CarParameters
        /// </summary>
        /// <param name="tdJourneyRequest"></param>
        /// <param name="parameters"></param>
        private static void UpdateCarParameters(ref ITDJourneyRequest tdJourneyRequest, CarParameters carParameters)
        {
            // Only populate those carParameters that are specified in the request
            if (carParameters != null)
            {
                if (carParameters.AlgorithmSpecified)
                    tdJourneyRequest.PrivateAlgorithm = (cjpInterface.PrivateAlgorithmType)Enum.Parse(typeof(cjpInterface.PrivateAlgorithmType), carParameters.Algorithm.ToString(), true);

                if (carParameters.AvoidMotorwaySpecified)
                    tdJourneyRequest.AvoidMotorways = carParameters.AvoidMotorway;

                if (carParameters.AvoidFerriesSpecified)
                    tdJourneyRequest.AvoidFerries = carParameters.AvoidFerries;

                if (carParameters.AvoidTollSpecified)
                    tdJourneyRequest.AvoidTolls = carParameters.AvoidToll;

                if (carParameters.BanLimitedAccessSpecified)
                    tdJourneyRequest.BanUnknownLimitedAccess = carParameters.BanLimitedAccess;

                if (carParameters.BanMotorwaySpecified)
                    tdJourneyRequest.DoNotUseMotorways = carParameters.BanMotorway;

                if (carParameters.CarSizeTypeSpecified)
                    tdJourneyRequest.CarSize = carParameters.CarSizeType.ToString().ToLower();

                if (carParameters.FuelTypeSpecified)
                    tdJourneyRequest.FuelType = carParameters.FuelType.ToString().ToLower();

                // Update the max speed if specified
                if ((carParameters.MaxSpeedSpecified) && (carParameters.MaxSpeed > 0))
                {
                    // Speed comes as mph, so convert to kph
                    int speedMPH = carParameters.MaxSpeed;

                    double speedKPH = Math.Round(Convert.ToDouble(MeasurementConversion.Convert(speedMPH, ConversionType.MilesToKilometres)), 0);

                    tdJourneyRequest.DrivingSpeed = Convert.ToInt32(speedKPH);
                }

                // Update the fuel consumption if the user has specified it
                if ((carParameters.FuelConsumptionSpecified) && (carParameters.FuelConsumption > 0))
                {
                    int fuelConsumptionEntered = carParameters.FuelConsumption;

                    // Default to mpg in case user hasn't specified a unit
                    FuelConsumptionUnit unit = FuelConsumptionUnit.MilesPerGallon;

                    if (carParameters.FuelConsumptionUnitSpecified)
                        unit = carParameters.FuelConsumptionUnit;

                    switch (unit)
                    {
                        case FuelConsumptionUnit.LitresPer100Km:
                            // Translate from LitresPer100Km to LitresPerMeter
                            tdJourneyRequest.FuelConsumption = MeasurementConversion.Convert(Convert.ToDouble(fuelConsumptionEntered), ConversionType.LitresPer100KilometersToMetersPerLitre).ToString();
                            break;
                        default:
                            // Translate from GallonsPerMile to LitresPerMeter
                            tdJourneyRequest.FuelConsumption = MeasurementConversion.Convert(Convert.ToDouble(fuelConsumptionEntered), ConversionType.MilesPerGallonToMetersPerLitre).ToString();
                            break;
                    }
                }

                // Update the fuel cost if the user has specified it
                if ((carParameters.FuelCostSpecified) && (carParameters.FuelCost > 0))
                {
                    tdJourneyRequest.FuelPrice = MeasurementConversion.Convert((Convert.ToDouble(carParameters.FuelCost)), ConversionType.TenthOfPencePerLitre).ToString();
                }


                // Update any avoid roads specified
                if ((carParameters.AvoidRoads != null) && (carParameters.AvoidRoads.Length > 0))
                {
                    tdJourneyRequest.AvoidRoads = carParameters.AvoidRoads;
                }

                // Update any use roads specified
                if ((carParameters.UseRoads != null) && (carParameters.UseRoads.Length > 0))
                {
                    tdJourneyRequest.IncludeRoads = carParameters.UseRoads;
                }
            }
        }

        #endregion

        #region Result

        /// <summary>
        /// Method which creates a CarJourney dto object for the supplied domain RoadJourney
        /// </summary>
        private static CarJourney CreateCarJourneyDT(bool outward,
                                                    RoadJourney roadJourney,
                                                    JourneySummaryLine summaryLine,
                                                    ITDJourneyRequest tdJourneyRequest,
                                                    ITDJourneyResult tdJourneyResult,
                                                    ResultSettings resultSettings,
                                                    CarCostHelper cch,
                                                    TDResourceManager rm,
                                                    ref bool congestionChargeAdded,
                                                    ref ArrayList visitedCongestionCompany)
        {
            CarJourney carJourneyDTO = new CarJourney();
            
            // Always include summary in the response
            carJourneyDTO.Summary = CreateCarJourneySummaryDT(roadJourney, summaryLine, tdJourneyRequest, resultSettings, cch);

            // Only create the details if user requested full details result
            if (resultSettings.ResultType == ResultType.Detailed)
            {
                carJourneyDTO.Details = CreateCarJourneyDetailsDT(roadJourney, outward, tdJourneyResult, resultSettings, rm, 
                    ref congestionChargeAdded, ref visitedCongestionCompany);
            }
            
            return carJourneyDTO;
        }

        /// <summary>
        /// Method which creates a CarJourneySummary dto object for the supplied domain RoadJourney and JourneySummaryLine
        /// </summary>
        private static CarJourneySummary CreateCarJourneySummaryDT(RoadJourney roadJourney, 
            JourneySummaryLine summaryLine, ITDJourneyRequest tdJourneyRequest,
            ResultSettings resultSettings, CarCostHelper cch)
        {
            if (summaryLine != null)
            {
                CarJourneySummary carJourneySummaryDTO = new CarJourneySummary();

                carJourneySummaryDTO.OriginLocation = CreateResponseLocationDT(roadJourney.OriginLocation);
                carJourneySummaryDTO.DestinationLocation = CreateResponseLocationDT(roadJourney.DestinationLocation);
                carJourneySummaryDTO.ViaLocation = CreateResponseLocationDT(roadJourney.RequestedViaLocation);

                carJourneySummaryDTO.DepartureDateTime = CalculateDateTime(summaryLine.DepartureDateTime);
                carJourneySummaryDTO.ArrivalDateTime = CalculateDateTime(summaryLine.ArrivalDateTime);
                carJourneySummaryDTO.Duration = CalculateDuration(summaryLine);
                
                carJourneySummaryDTO.Distance = CalculateDistance(roadJourney, resultSettings.DistanceUnit);
                carJourneySummaryDTO.DistanceUnit = resultSettings.DistanceUnit;

                carJourneySummaryDTO.Costs = CalculateCarCosts(cch);

                carJourneySummaryDTO.Emissions = CalculateEmissions(roadJourney, tdJourneyRequest);

                carJourneySummaryDTO.CarParameters = CreateCarParametersDT(tdJourneyRequest);

                carJourneySummaryDTO.SummaryDirections = GetSummaryDirectionsForJourney(roadJourney);

                return carJourneySummaryDTO;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method which creates a ResponseLocation dto object for the domain TDLocation
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private static ResponseLocation CreateResponseLocationDT(TDLocation location)
        {
            if (location != null)
            {
                ResponseLocation responseLocationDTO = new ResponseLocation();
                
                responseLocationDTO.Description = location.Description;
                responseLocationDTO.GridReference = CommonAssembler.CreateOSGridReferenceDT(location.GridReference);
                responseLocationDTO.Type = LocationType.Coordinate;

                return responseLocationDTO;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method which creates a CarParameters dto object to return in the car journey result
        /// </summary>
        /// <param name="tdJourneyRequest"></param>
        /// <returns></returns>
        private static CarParameters CreateCarParametersDT(ITDJourneyRequest tdJourneyRequest)
        {
            CarParameters carParameters = new CarParameters();

            // Populate all parameters used
            carParameters.Algorithm = (CarAlgorithmType)Enum.Parse(typeof(CarAlgorithmType), tdJourneyRequest.PrivateAlgorithm.ToString(), true);
            carParameters.AvoidMotorway = tdJourneyRequest.AvoidMotorways;
            carParameters.AvoidFerries = tdJourneyRequest.AvoidFerries;
            carParameters.AvoidToll = tdJourneyRequest.AvoidTolls;
            carParameters.BanLimitedAccess = tdJourneyRequest.BanUnknownLimitedAccess;
            carParameters.BanMotorway = tdJourneyRequest.DoNotUseMotorways;
            carParameters.CarSizeType = (CarSizeType)Enum.Parse(typeof(CarSizeType), tdJourneyRequest.CarSize.ToString(), true);
            carParameters.FuelType = (FuelType)Enum.Parse(typeof(FuelType), tdJourneyRequest.FuelType.ToString(), true);

            double speedMPH = Math.Round(Convert.ToDouble(MeasurementConversion.Convert(tdJourneyRequest.DrivingSpeed, ConversionType.KilometresToMiles)), 0);
            carParameters.MaxSpeed = Convert.ToInt32(speedMPH);

            double fuelConsumptionMPH = Math.Round(Convert.ToDouble(MeasurementConversion.Convert(Convert.ToDouble(tdJourneyRequest.FuelConsumption), ConversionType.MetersPerLitreToMilesPerGallon)), 0);
            carParameters.FuelConsumption = Convert.ToInt32(fuelConsumptionMPH);
            carParameters.FuelConsumptionUnit = FuelConsumptionUnit.MilesPerGallon;

            carParameters.FuelCost = (Convert.ToDouble(tdJourneyRequest.FuelPrice)) / 10;

            carParameters.AvoidRoads = tdJourneyRequest.AvoidRoads;
            carParameters.UseRoads = tdJourneyRequest.IncludeRoads;

            // Set all the specified flags so they are output in the response
            carParameters.AlgorithmSpecified = true;
            carParameters.AvoidMotorwaySpecified = true;
            carParameters.AvoidFerriesSpecified = true;
            carParameters.AvoidTollSpecified = true;
            carParameters.BanLimitedAccessSpecified = true;
            carParameters.BanMotorwaySpecified = true;
            carParameters.CarSizeTypeSpecified = true;
            carParameters.FuelTypeSpecified = true;
            carParameters.MaxSpeedSpecified = true;
            carParameters.FuelConsumptionSpecified = true;
            carParameters.FuelConsumptionUnitSpecified = true;
            carParameters.FuelCostSpecified = true;
            carParameters.AvoidRoadsSpecified = true;
            carParameters.UseRoadsSpecified = true;

            return carParameters;
        }

        /// <summary>
        /// Method creates an array of CarJourneyDetail dto objects for the supplied RoadJourney
        /// </summary>
        private static CarJourneyDetail[] CreateCarJourneyDetailsDT(RoadJourney roadJourney, bool outward, ITDJourneyResult tdJourneyResult, ResultSettings resultSettings, TDResourceManager rm,
            ref bool congestionChargeAdded, ref ArrayList visitedCongestionCompany)
        {
            // Determine if this is a same day return journey (used is decision logic for congestion charge
            bool sameDayReturn = false;
            bool returnExists = ((tdJourneyResult.ReturnRoadJourneyCount) > 0) && tdJourneyResult.IsValid;
            RoadJourney returnRoadJourney = (outward) ? tdJourneyResult.ReturnRoadJourney() : tdJourneyResult.OutwardRoadJourney();

            if ((returnExists) && (returnRoadJourney != null))
            {
                //Used to check if the Outward and Return Dates are the same
                if (roadJourney.DepartDateTime.GetDifferenceDates(returnRoadJourney.DepartDateTime) == 0)
                {
                    sameDayReturn = true;
                }
            }
            
            // Get the formatted journey directions
            CarJourneyDetailFormatter carJourneyDetailFormatter = new CarJourneyDetailFormatter(roadJourney, outward, sameDayReturn, congestionChargeAdded, visitedCongestionCompany, resultSettings.DistanceUnit, rm);
            IList journeyDetails = carJourneyDetailFormatter.GetJourneyDetails();

            // Keep track of the congestion charge added
            congestionChargeAdded = carJourneyDetailFormatter.CongestionChargeAdded;
            visitedCongestionCompany = carJourneyDetailFormatter.VisitedCongestionCompany;

            ArrayList carJourneyDetailDTO = new ArrayList();

            // details will be in a list of string array objects, convert these into CarJourneyDetail objects
            foreach (object[] journeyDetail in journeyDetails)
            {
                carJourneyDetailDTO.Add(CreateCarJourneyDetailDT(journeyDetail));
            }

            return (CarJourneyDetail[])carJourneyDetailDTO.ToArray(typeof(CarJourneyDetail));
        }

        /// <summary>
        /// Method which creates a CarJourneyDetail dto object
        /// </summary>
        /// <returns></returns>
        private static CarJourneyDetail CreateCarJourneyDetailDT(object[] journeyDetail)
        {
            CarJourneyDetail cjd = new CarJourneyDetail();

            if (!string.IsNullOrEmpty((string)journeyDetail[0]))
            {
                cjd.InstructionNumber = Convert.ToInt32((string)journeyDetail[0]);
            }

            cjd.CumulativeDistance = (string)journeyDetail[1];
            cjd.InstructionText = (string)journeyDetail[3];
            cjd.ArrivalTime = (string)journeyDetail[4];

            if ((!string.IsNullOrEmpty((string)journeyDetail[6])) || (!string.IsNullOrEmpty((string)journeyDetail[7])))
            {
                cjd.Cost = CreateCarCostDT(CarCostType.OtherCost, Convert.ToDouble(journeyDetail[5]), string.Empty, (string)journeyDetail[6], (string)journeyDetail[7]);
            }

            return cjd;
        }

        /// <summary>
        /// Method which creates an Emissions dto object
        /// </summary>
        private static Emissions CreateEmission(VehicleType vehicleType, double emissionsValue)
        {
            Emissions emissions = new Emissions();

            emissions.VehicleType = vehicleType;
            emissions.CO2Emissions = emissionsValue;

            return emissions;
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Returns a DateTime from a TDDateTime
        /// </summary>
        private static DateTime CalculateDateTime(TDDateTime tdDateTime)
        {
            return new DateTime
                (tdDateTime.Year, tdDateTime.Month, tdDateTime.Day, tdDateTime.Hour, tdDateTime.Minute, tdDateTime.Second);
        }

        /// <summary>
        /// Calculates the journey duration.
        /// </summary>
        private static Common.V1.TimeSpan CalculateDuration(JourneySummaryLine summaryLine)
        {
            System.TimeSpan ts = new System.TimeSpan();

            DateTime dateTimeArrivalTime = CalculateDateTime(summaryLine.ArrivalDateTime);
            DateTime dateTimeDepartureTime = CalculateDateTime(summaryLine.DepartureDateTime);

            // find the difference between the two times
            ts = dateTimeArrivalTime.Subtract(dateTimeDepartureTime);

            // Create a simple serialiseable timespan class to return
            Common.V1.TimeSpan duration = new Common.V1.TimeSpan(ts.Days, ts.Hours, ts.Minutes, ts.Seconds);
            
            return duration;
        }

        /// <summary>
        /// Calculates the journey distance in the unit specified
        /// </summary>
        private static double CalculateDistance(RoadJourney roadJourney, DistanceUnit distanceUnit)
        {
            // RoadJourney.TotalDistance is in metres
            if (distanceUnit == DistanceUnit.Miles)
            {
                return Convert.ToDouble(MeasurementConversion.Convert((double)roadJourney.TotalDistance, ConversionType.MetresToMileage));
            }
            else
            {
                // Calculate distance
                double dblDistance = (double)roadJourney.TotalDistance / 1000;

                // Update display format
                string strDistance = dblDistance.ToString("#,###.##", NumberFormatInfo.CurrentInfo);

                return Convert.ToDouble(strDistance);
            }
        }

        /// <summary>
        /// Calculates the costs for the road journey specified
        /// </summary>
        private static CarCost[] CalculateCarCosts(CarCostHelper cch)
        {
            // If theres no helper, unable to calculate costs
            if (cch == null)
            {
                return null;
            }

            // Get the individual costs
            decimal runningCost = cch.CalculateRunningCost();
            decimal fuelCost = cch.CalculateFuelCost();
            decimal totalOtherCosts = cch.CalculateTotalOtherCosts(); // total "other costs" including congestion, car parks...

            // Add up the costs to give a total cost
            decimal totalCost = fuelCost + runningCost + totalOtherCosts;

            // Create the various CarCost dto objects
            ArrayList carCostsArray = new ArrayList();

            carCostsArray.Add(CreateCarCostDT(CarCostType.FuelCost, Convert.ToDouble(fuelCost), "Fuel cost (approx)", null, null));
            carCostsArray.Add(CreateCarCostDT(CarCostType.RunningCost, Convert.ToDouble(runningCost), "Running cost (approx)", null, null));
            carCostsArray.Add(CreateCarCostDT(CarCostType.TotalOtherCosts, Convert.ToDouble(totalOtherCosts), "Total for all congestion, toll, ferry, and other costs", null, null));
            carCostsArray.Add(CreateCarCostDT(CarCostType.TotalCost, Convert.ToDouble(totalCost), "Total cost for journey", null, null));

            // Add the individual Other cost items
            carCostsArray.AddRange(cch.CreateOtherCostItems());

            // Convert into a return object
            CarCost[] carCostsDTO = (CarCost[])carCostsArray.ToArray(typeof(CarCost));

            if ((carCostsDTO == null) || (carCostsDTO.Length == 0))
            {
                return null;
            }
            else
            {
                return carCostsDTO;
            }
        }

        /// <summary>
        /// Calculates the emissions for the road journey
        /// </summary>
        private static Emissions[] CalculateEmissions(RoadJourney roadJourney, ITDJourneyRequest journeyRequest)
        {
            // Set up the fuel parameters needed by the calculator for the planned car journey
            CarFuelParameters carFuelParameters = new CarFuelParameters();

            carFuelParameters.CarSize = journeyRequest.CarSize;
            carFuelParameters.CarFuelType = journeyRequest.FuelType;
            carFuelParameters.FuelConsumptionEntered = journeyRequest.FuelConsumption;
            carFuelParameters.FuelConsumptionUnit = 0;
            carFuelParameters.FuelConsumptionOption = true;
            carFuelParameters.FuelCostEntered = journeyRequest.FuelPrice;
            carFuelParameters.FuelCostOption = true;
            
            // Create instance of calculator
            JourneyEmissionsCalculator emissionsCalculator = new JourneyEmissionsCalculator(carFuelParameters, roadJourney.TotalFuelCost);

            // Get journey distance and emissions
            decimal journeyDistance = emissionsCalculator.GetJourneyDistance(roadJourney);

            decimal emissionsCarValue = emissionsCalculator.GetEmissions();
            decimal emissionsSmallCarValue = emissionsCalculator.GetEmissions("small", "diesel", roadJourney, carFuelParameters);
            decimal emissionsLargeCarValue = emissionsCalculator.GetEmissions("large", "petrol", roadJourney, carFuelParameters);

            decimal emissionsTrainValue = emissionsCalculator.GetEmissions(cjpInterface.ModeType.Rail, journeyDistance);
            decimal emissionsBusValue = emissionsCalculator.GetEmissions(cjpInterface.ModeType.Bus, journeyDistance);
            decimal emissionsCoachValue = emissionsCalculator.GetEmissions(cjpInterface.ModeType.Coach, journeyDistance);
            decimal emissionsPlaneValue = emissionsCalculator.GetEmissions(cjpInterface.ModeType.Air, journeyDistance);

            // Create the emissions dto objects
            ArrayList emissions = new ArrayList();

            emissions.Add(CreateEmission(VehicleType.Car, Convert.ToDouble(emissionsCarValue)));
            emissions.Add(CreateEmission(VehicleType.SmallCar, Convert.ToDouble(emissionsSmallCarValue)));
            emissions.Add(CreateEmission(VehicleType.LargeCar, Convert.ToDouble(emissionsLargeCarValue)));
            emissions.Add(CreateEmission(VehicleType.Train, Convert.ToDouble(emissionsTrainValue)));
            
            // Only add the bus, coach, air dependent on distance (following same rules as per TDP web page output)
            if (JourneyEmissionsCalculator.ShowCoach(journeyDistance))
                emissions.Add(CreateEmission(VehicleType.Coach, Convert.ToDouble(emissionsCoachValue)));
            else
                emissions.Add(CreateEmission(VehicleType.Bus, Convert.ToDouble(emissionsBusValue)));

            if (JourneyEmissionsCalculator.ShowAir(journeyDistance))
                emissions.Add(CreateEmission(VehicleType.Plane, Convert.ToDouble(emissionsPlaneValue)));

            // Return result dto
            return (Emissions[])emissions.ToArray(typeof(Emissions));
        }

        /// <summary>
        /// Creates a string of summary directions for the road journey
        /// </summary>
        /// <param name="roadJourney"></param>
        /// <returns></returns>
        private static string GetSummaryDirectionsForJourney(RoadJourney roadJourney)
        {
            // Used to temporarily hold all the road names so that
            // duplicates can be filtered out.
            ArrayList roadNames = new ArrayList();

            // Get all the roads for the adjusted route
            foreach (RoadJourneyDetail roadJourneyDetail in roadJourney.Details)
            {
                string roadName = (!string.IsNullOrEmpty(roadJourneyDetail.RoadNumber)) ? roadJourneyDetail.RoadNumber : string.Empty;

                // Add the road name only if is not empty and it hasn't
                // already been added.
                if (!string.IsNullOrEmpty(roadName) &&
                    !roadNames.Contains(roadName))
                {
                    roadNames.Add(roadName);
                }
            }

            StringBuilder summaryDirections = new StringBuilder();

            // Construct the adjusted roads label
            for (int i = 0; i < roadNames.Count; i++)
            {
                summaryDirections.Append(roadNames[i]);
                summaryDirections.Append("; ");
            }

            return summaryDirections.ToString();
        }
                
        #endregion

        #endregion
    }
}
