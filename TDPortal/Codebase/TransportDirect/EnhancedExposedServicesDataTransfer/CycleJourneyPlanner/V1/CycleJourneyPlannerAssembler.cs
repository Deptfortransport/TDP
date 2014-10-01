// *********************************************** 
// NAME             : CycleJourneyPlannerAssembler.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: Assembler class containing methods for converting between Domain and Data Transfer objects
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleJourneyPlannerAssembler.cs-arc  $
//
//   Rev 1.3   Nov 05 2010 14:35:24   apatel
//Updated to resolve the EES Cycle web service issues
//Resolution for 5632: EES for Cycle issues
//
//   Rev 1.2   Oct 15 2010 10:55:10   apatel
//Updated to accept multiple Cycle algorithm dlls (Doc Ref ATO687)
//Resolution for 5622: Update CTP to accept multiple function dlls (Doc Ref: ATO687)
//
//   Rev 1.1   Oct 12 2010 09:24:48   apatel
//Updated the CreateJourneyResultDT method for error display logic
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.0   Sep 29 2010 10:39:44   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using System.Collections;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;
using CyclePlannerControl = TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.JourneyControl;
using System.Globalization;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;


namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// Assembler class containing methods for converting between Domain and Data Transfer objects
    /// </summary>
    public sealed class CycleJourneyPlannerAssembler
    {
        public static string DEFAULT_LANGUAGE = "en-GB";

        #region Private fields
        //private static string PENALTYFUNCTION_LOCATION = "CyclePlanner.PlannerControl.PenaltyFunction.Location";
        private static string PENALTYFUNCTION_PREFIX = "CyclePlanner.PlannerControl.PenaltyFunction.{0}.Prefix";
        private static string NUMBER_OF_PREFERENCES = "CyclePlanner.TDUserPreference.NumberOfPreferences";
        private static string DEFAULT_CYCLE_JOURNEY_TYPE = "CyclePlanner.PlannerControl.PenaltyFunction.Default";
        private static string PENALTYFUNCTION_FOLDER= "CyclePlanner.PlannerControl.PenaltyFunction.Folder";
        private static string PENALTYFUNCTION_DLL = "CyclePlanner.PlannerControl.PenaltyFunction.{0}.Dll";

        private static string JOURNEYRESULTSETTING_INCLUDEGEOMETRY = "CyclePlanner.PlannerControl.JourneyResultSetting.IncludeGeometry";
        private static string JOURNEYRESULTSETTING_POINTSEPERATOR = "CyclePlanner.PlannerControl.JourneyResultSetting.PointSeperator";
        private static string JOURNEYRESULTSETTING_EASTINGNORTHINGSEPERATOR = "CyclePlanner.PlannerControl.JourneyResultSetting.EastingNorthingSeperator";

        #endregion

        #region Public methods

        #region Result Settings
        /// <summary>
        /// Retuns the ResultSettings object with default settings
        /// </summary>
        /// <returns>ResultSettings object with default settings</returns>
        public static ResultSettings GetDefaultResultSettings()
        {
            ResultSettings resultSettings = new ResultSettings();

            resultSettings.IncludeGeometry = bool.Parse(Properties.Current[JOURNEYRESULTSETTING_INCLUDEGEOMETRY]);
            resultSettings.PointSeparator = Convert.ToChar(Properties.Current[JOURNEYRESULTSETTING_POINTSEPERATOR]);
            resultSettings.EastingNorthingSeparator = Convert.ToChar(Properties.Current[JOURNEYRESULTSETTING_EASTINGNORTHINGSEPERATOR]);

            resultSettings.Language = DEFAULT_LANGUAGE;
            resultSettings.DistanceUnit = DistanceUnit.Miles;

            return resultSettings;

        }
        #endregion

        #region Request

        /// <summary>
        /// Method uses parameters from a dto request object to create a domain request object
        /// </summary>
        public static CyclePlannerControl.ITDCyclePlannerRequest CreateTDCycleJourneyRequest(CycleJourneyRequest cycleJourneyRequest)
        {
            // At a minimum, the request needs to contain at least one journey to plan
            if ((cycleJourneyRequest != null) && (cycleJourneyRequest.JourneyRequest != null))
            {
                JourneyRequest journey = cycleJourneyRequest.JourneyRequest;

                CyclePlannerControl.ITDCyclePlannerRequest tdCycleJourneyRequest = new CyclePlannerControl.TDCyclePlannerRequest();

                // Set date time values
                tdCycleJourneyRequest.IsReturnRequired = false; // Only outward cycle journey are permitted
                tdCycleJourneyRequest.OutwardArriveBefore = journey.OutwardArriveBefore;
                
                // Convert datetime parameters into a TDDateTime parameter
                TDDateTime outwardTDDateTime = new TDDateTime(journey.OutwardDateTime);
                tdCycleJourneyRequest.OutwardDateTime = new TDDateTime[] { outwardTDDateTime };

                #region Set user preferences

                ArrayList userPreferenceArray = new ArrayList();
                Dictionary<int, CyclePlannerControl.TDCycleUserPreference> userPreferences = GetDefaultUserPreferences();

                if (cycleJourneyRequest.CycleParameters != null)
                {
                    

                    if (cycleJourneyRequest.CycleParameters.RequestPreferences != null)
                    {
                        foreach (RequestPreference preference in cycleJourneyRequest.CycleParameters.RequestPreferences)
                        {
                            if (preference != null)
                            {
                                CyclePlannerControl.TDCycleUserPreference userPreference = new CyclePlannerControl.TDCycleUserPreference();
                                userPreference.PreferenceKey = preference.PreferenceId.ToString();
                                userPreference.PreferenceValue = preference.PreferenceValue;

                                // Only update and not add to the userPreferences
                                if (userPreferences.ContainsKey(preference.PreferenceId))
                                {
                                    userPreferences[preference.PreferenceId] = userPreference;
                                }
                            }
                        }
                    }
                }
                
                foreach(CyclePlannerControl.TDCycleUserPreference tdUserPref in userPreferences.Values)
                {
                    userPreferenceArray.Add(tdUserPref);
                }

                tdCycleJourneyRequest.UserPreferences = (CyclePlannerControl.TDCycleUserPreference[])userPreferenceArray.ToArray(typeof(CyclePlannerControl.TDCycleUserPreference));

                #endregion

                #region Set penalty function
                string cycleJourneyType = string.Empty;

                if (cycleJourneyRequest.CycleParameters != null && 
                    !string.IsNullOrEmpty(cycleJourneyRequest.CycleParameters.Algorithm))
                {
                    tdCycleJourneyRequest.PenaltyFunction = GetPenaltyFunction(cycleJourneyRequest.CycleParameters.Algorithm, out cycleJourneyType);
                    tdCycleJourneyRequest.CycleJourneyType = cycleJourneyType;
                }
                else
                {
                    tdCycleJourneyRequest.PenaltyFunction = GetDefaultPenaltyFunction(out cycleJourneyType);
                    tdCycleJourneyRequest.CycleJourneyType = cycleJourneyType;
                }

                
                #endregion
                

                #region Set Result Settings
                CyclePlannerControl.TDCycleJourneyResultSettings resultSettings = new CyclePlannerControl.TDCycleJourneyResultSettings();
                
                if (cycleJourneyRequest.ResultSettings == null)
                {
                    cycleJourneyRequest.ResultSettings = GetDefaultResultSettings();
                }

                resultSettings.IncludeGeometry = cycleJourneyRequest.ResultSettings.IncludeGeometry;
                resultSettings.EastingNorthingSeparator = Convert.ToChar(cycleJourneyRequest.ResultSettings.EastingNorthingSeparator);
                resultSettings.PointSeparator = Convert.ToChar(cycleJourneyRequest.ResultSettings.PointSeparator);
            
                tdCycleJourneyRequest.ResultSettings = resultSettings;

                #endregion


                return tdCycleJourneyRequest;
            }
            else
            {
                return null;
            }
        }

       

        #endregion

        #region Result
        /// <summary>
        /// Method which creates a ResultJourney dto object containing the cycle journeys for the JourneyId specified.
        /// If an error message is supplied, this is added to the ErrorMessages list.
        /// </summary>
        public static JourneyResult CreateJourneyResultDT(int journeyId,
            CyclePlannerControl.ITDCyclePlannerRequest tdCycleJourneyRequest, CyclePlannerControl.ITDCyclePlannerResult tdCycleJourneyResult,
            ResultSettings resultSettings, TDResourceManager rm,
            bool outwardArriveBefore, string errorMessage, TDExceptionIdentifier tdExId)
        {
            // The return dto object
            JourneyResult journeyResultDTO = new JourneyResult();

            ArrayList errorMessageDT = new ArrayList();
            ArrayList warningMessageDT = new ArrayList();

            journeyResultDTO.JourneyId = journeyId;

            if (tdCycleJourneyResult != null)
            {
                // If theres a result object, then create the outward cycle journey
                if (tdCycleJourneyResult.IsValid)
                {
                    #region Create cycle journeys


                    if (tdCycleJourneyResult.OutwardCycleJourneyCount > 0)
                    {
                        // There will only be one summary line for the cycle journey
                        JourneySummaryLine[] jsl = tdCycleJourneyResult.OutwardJourneySummary(tdCycleJourneyRequest.OutwardArriveBefore,
                            new TransportDirect.JourneyPlanning.CJPInterface.ModeType[] { TransportDirect.JourneyPlanning.CJPInterface.ModeType.Cycle });

                        journeyResultDTO.OutwardCycleJourney = CreateCycleJourneyDT(
                            true,
                            tdCycleJourneyResult.OutwardCycleJourney(),
                            jsl[0],
                            tdCycleJourneyRequest,
                            tdCycleJourneyResult,
                            resultSettings,
                            rm);
                    }



                    #endregion
                }

                #region Add warnings and errors

                // Add any user warnings or error messages
                if (tdCycleJourneyResult.CyclePlannerMessages.Length != 0)
                {
                    foreach (CyclePlannerControl.CyclePlannerMessage cyclePlannerMessage in tdCycleJourneyResult.CyclePlannerMessages)
                    {
                        if (cyclePlannerMessage.Type == CyclePlannerControl.ErrorsType.Warning)
                        {
                            warningMessageDT.Add(
                                CommonAssembler.CreateMessageDT(rm.GetString(cyclePlannerMessage.MessageResourceId), (int)TDExceptionIdentifier.JPWarningMessage));
                        }
                        else if (cyclePlannerMessage.Type == CyclePlannerControl.ErrorsType.Error)
                        {
                            errorMessageDT.Add(
                                CommonAssembler.CreateMessageDT(rm.GetString(cyclePlannerMessage.MessageResourceId), (int)TDExceptionIdentifier.JPCJPErrorsOccured));
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
        /// Method which creates a CycleJourneyResult dto object for the ResultJourney
        /// </summary>
        public static CycleJourneyResult CreateCycleJourneyResultDT(JourneyResult journeyResult)
        {
            CycleJourneyResult cycleJourneyResultDTO = new CycleJourneyResult();

            cycleJourneyResultDTO.JourneyResult = journeyResult;

            #region Create CompletionStatus object

            if (journeyResult != null)
            {
                bool allJourneysOK = true;

                if ((journeyResult.ErrorMessages != null) && (journeyResult.ErrorMessages.Length > 0))
                {
                    allJourneysOK = false;
                }
                

                if (allJourneysOK)
                {
                    cycleJourneyResultDTO.CompletionStatus = CommonAssembler.CreateCompletionStatusDT(true, 0, string.Empty);
                }
                else
                {
                    cycleJourneyResultDTO.CompletionStatus = CommonAssembler.CreateCompletionStatusDT(false, (int)TDExceptionIdentifier.JPFailedToPlanAllJourneys,
                        "Currently unable to obtain a complete set of journeys using the request parameters supplied.");
                }

            }
            else
            {
                // This error should not be reached, there should always be a result journey, otherwise exception 
                // would have been thrown to user by calling class
                // Added for completeness.
                cycleJourneyResultDTO.CompletionStatus = CommonAssembler.CreateCompletionStatusDT(false, (int)TDExceptionIdentifier.JPFailedToPlanJourney,
                    "Currently unable to obtain journey options using the request parameters supplied.");
            }

            #endregion

            return cycleJourneyResultDTO;
        }

        
        #endregion

        #endregion

        #region Private methods
        #region Result
        /// <summary>
        /// Method which creates a CycleJourney dto object for the supplied domain RoadJourney
        /// </summary>
        private static CycleJourney CreateCycleJourneyDT(bool outward,
                                                    CyclePlannerControl.CycleJourney cycleJourney,
                                                    JourneySummaryLine summaryLine,
                                                    CyclePlannerControl.ITDCyclePlannerRequest tdCycleJourneyRequest,
                                                    CyclePlannerControl.ITDCyclePlannerResult tdCycleJourneyResult,
                                                    ResultSettings resultSettings,
                                                    TDResourceManager rm)
        {
            CycleJourney cycleJourneyDTO = new CycleJourney();

            CycleJourneySummary summary = CreateCycleJourneySummaryDT(cycleJourney, summaryLine, tdCycleJourneyRequest, resultSettings);

            CycleJourneyDetail[] cycleDetails = CreateCycleJourneyDetailsDT(cycleJourney, resultSettings, rm);
            
            summary.Costs = GetCycleCostSummary(cycleDetails);

            cycleJourneyDTO.Summary = summary;

            cycleJourneyDTO.Details = cycleDetails;

            return cycleJourneyDTO;
        }

        /// <summary>
        /// Calculates and builds cycle cost dto object for cycle journey summary object
        /// </summary>
        /// <param name="cycleDetails">Array of Cycle journey dto objects</param>
        /// <returns>Array of cycle cost dto object</returns>
        private static CycleCost[] GetCycleCostSummary(CycleJourneyDetail[] cycleDetails)
        {
            List<CycleCost> cycleCosts = new List<CycleCost>();

            double totalOtherCost = 0;

            foreach (CycleJourneyDetail cycleDetail in cycleDetails)
            {
                if (cycleDetail.Cost != null)
                {
                    totalOtherCost += cycleDetail.Cost.Cost;
                }
               
            }

            CycleCost cycleCost = new CycleCost();
            cycleCost.Cost = totalOtherCost;
            cycleCost.CostType = CycleCostType.OtherCost;

            cycleCosts.Add(cycleCost);

            return cycleCosts.ToArray();
            
        }

        /// <summary>
        /// Method which creates a CycleJourneySummary dto object for the supplied domain CycleJourney and JourneySummaryLine
        /// </summary>
        private static CycleJourneySummary CreateCycleJourneySummaryDT(CyclePlannerControl.CycleJourney cycleJourney,
            JourneySummaryLine summaryLine, CyclePlannerControl.ITDCyclePlannerRequest tdJourneyRequest,
            ResultSettings resultSettings)
        {
            if (summaryLine != null)
            {
                CycleJourneySummary cycleJourneySummaryDTO = new CycleJourneySummary();

                cycleJourneySummaryDTO.OriginLocation = CreateResponseLocationDT(cycleJourney.OriginLocation);
                cycleJourneySummaryDTO.DestinationLocation = CreateResponseLocationDT(cycleJourney.DestinationLocation);
                cycleJourneySummaryDTO.ViaLocation = CreateResponseLocationDT(cycleJourney.RequestedViaLocation);

                cycleJourneySummaryDTO.DepartureDateTime = CalculateDateTime(summaryLine.DepartureDateTime);
                cycleJourneySummaryDTO.ArrivalDateTime = CalculateDateTime(summaryLine.ArrivalDateTime);
                cycleJourneySummaryDTO.Duration = CalculateDuration(summaryLine);

                cycleJourneySummaryDTO.Distance = CalculateDistance(cycleJourney, resultSettings.DistanceUnit);
                cycleJourneySummaryDTO.DistanceUnit = resultSettings.DistanceUnit;

                cycleJourneySummaryDTO.SummaryDirections = GetSummaryDirectionsForJourney(cycleJourney);

                return cycleJourneySummaryDTO;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Method creates a CycleJourneyDetail dto object for the supplied CycleJourney
        /// </summary>
        private static CycleJourneyDetail[] CreateCycleJourneyDetailsDT(CyclePlannerControl.CycleJourney cycleJourney, ResultSettings resultSettings, TDResourceManager rm)
        {
            
            // Get the formatted journey directions
            CycleJourneyDetailFormatter cycleJourneyDetailFormatter = new CycleJourneyDetailFormatter(cycleJourney, resultSettings, rm);

            List<CycleJourneyDetail> journeyDetails = cycleJourneyDetailFormatter.GetJourneyDetails();

            return journeyDetails.ToArray();
        }

        #endregion

        #region Helper Methods

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
        private static double CalculateDistance(CyclePlannerControl.CycleJourney cycleJourney, DistanceUnit distanceUnit)
        {
            double dblDistance = 0;
            string strDistance = "0";

            // RoadJourney.TotalDistance is in metres
            if (distanceUnit == DistanceUnit.Miles)
            {
                dblDistance = Convert.ToDouble(MeasurementConversion.Convert((double)cycleJourney.TotalDistance, ConversionType.MetresToMileage));
            }
            else
            {
                // Calculate distance
                dblDistance = (double)cycleJourney.TotalDistance / 1000;
               
            }

            // Update display format
            strDistance = dblDistance.ToString("F1", TDCultureInfo.CurrentUICulture.NumberFormat);

            return Convert.ToDouble(strDistance);
        }

        
        /// <summary>
        /// Creates a string of summary directions for the cyle journey
        /// </summary>
        /// <param name="roadJourney"></param>
        /// <returns></returns>
        private static string GetSummaryDirectionsForJourney(CyclePlannerControl.CycleJourney cycleJourney)
        {
            // Used to temporarily hold all the road names so that
            // duplicates can be filtered out.
            ArrayList roadNames = new ArrayList();

            // Get all the roads for the adjusted route
            foreach (CyclePlannerControl.CycleJourneyDetail cycleJourneyDetail in cycleJourney.Details)
            {
                string roadName = (!string.IsNullOrEmpty(cycleJourneyDetail.RoadNumber)) ? cycleJourneyDetail.RoadNumber : string.Empty;

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

        /// <summary>
        /// Gets the Default values of cycle user preferences
        /// </summary>
        /// <returns></returns>
        private static Dictionary<int, CyclePlannerControl.TDCycleUserPreference> GetDefaultUserPreferences()
        {
            Dictionary<int, CyclePlannerControl.TDCycleUserPreference> userPreferences = new Dictionary<int, TransportDirect.UserPortal.CyclePlannerControl.TDCycleUserPreference>();

            CyclePlannerControl.TDCycleUserPreference tdUserPreference = null;

            // A property that denotes the size of the array of ser preferences expected by the Atkins CTP
            int numOfProperties = System.Convert.ToInt32(Properties.Current[NUMBER_OF_PREFERENCES]);

            // Build the actual array of user preferences from permanent portal properties
            // these are used in the request sent to the Atkins CTP.
            for (int i = 0; i < numOfProperties; i++)
            {

                tdUserPreference = new CyclePlannerControl.TDCycleUserPreference(i.ToString(),
                    Properties.Current["CyclePlanner.TDUserPreference.Preference" + i.ToString()]);

                userPreferences.Add(i, tdUserPreference);
            }

            return userPreferences;
        }

        /// <summary>
        /// Gets the default penalty function for the cycle journey
        /// </summary>
        /// <param name="cycleJourneyType"></param>
        /// <returns></returns>
        private static string GetDefaultPenaltyFunction(out string cycleJourneyType)
        {
            StringBuilder penaltyFunction = new StringBuilder();

            string defaultJourneyType = Properties.Current[DEFAULT_CYCLE_JOURNEY_TYPE];

            string penaltyFunctionPath = Properties.Current[PENALTYFUNCTION_FOLDER];

            penaltyFunction.Append("Call ");
            penaltyFunction.Append(penaltyFunctionPath);
            if (!penaltyFunctionPath.EndsWith("/"))
            {
                penaltyFunction.Append("/");
            }
            penaltyFunction.Append(Properties.Current[string.Format(PENALTYFUNCTION_DLL, defaultJourneyType)]);
            penaltyFunction.Append(", ");

            string strPenaltyFunction = Properties.Current[string.Format(PENALTYFUNCTION_PREFIX,defaultJourneyType)]
                + "."
                + defaultJourneyType;

            penaltyFunction.Append(strPenaltyFunction);

            cycleJourneyType = defaultJourneyType;

            return penaltyFunction.ToString();

        }

        /// <summary>
        /// Gets the actual penalty function to be passed in to cycle request.
        /// </summary>
        /// <param name="algorithmCall"></param>
        /// <param name="cycleJourneyType"></param>
        /// <returns></returns>
        private static string GetPenaltyFunction(string algorithmCall, out string cycleJourneyType)
        {
            StringBuilder penaltyFunction = new StringBuilder();

            if (algorithmCall.ToLower().StartsWith("call "))
            {
                string algorithm = algorithmCall.Replace("Call", "").Trim();

                string[] algorithmParts = algorithm.Split(new char[] { ',' });

                string penaltyFunctionPath = Properties.Current[PENALTYFUNCTION_FOLDER];

                penaltyFunction.Append("Call ");
                penaltyFunction.Append(penaltyFunctionPath);
                if (!penaltyFunctionPath.EndsWith("/"))
                {
                    penaltyFunction.Append("/");
                }
                penaltyFunction.Append(algorithmParts[0]); // penalty function dll
                penaltyFunction.Append(", ");

                string strPenaltyFunction = Properties.Current[string.Format(PENALTYFUNCTION_PREFIX, algorithmParts[1])]
                    + "."
                    + algorithmParts[1]; // penalty function name

                penaltyFunction.Append(strPenaltyFunction);

                cycleJourneyType = algorithmParts[1];
            }
            else
            {
                penaltyFunction.Append(algorithmCall);
                cycleJourneyType = string.Empty;
            }

            return penaltyFunction.ToString();

        }

        

        #endregion
        #endregion



        
    }
}
