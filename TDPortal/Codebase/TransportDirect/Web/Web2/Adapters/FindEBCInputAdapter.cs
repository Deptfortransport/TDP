// *********************************************** 
// NAME			: FindEBCInputAdapter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 16 Sep 2009
// DESCRIPTION	: Responsible for the EBC planner functionality
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindEBCInputAdapter.cs-arc  $
//
//   Rev 1.1   Oct 12 2009 09:11:28   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 12 2009 08:39:58   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Sep 21 2009 15:04:10   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public class FindEBCInputAdapter : FindJourneyInputAdapter
    {
        #region Private members

        IDataServices populator = null;
        CarCostCalculator carCostCalculator = null;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="journeyParams">Journey parameters for EBC journey planning</param>
		/// <param name="pageState">Page state for EBC input pages</param>
		/// <param name="inputPageState">Variables indicating state of page, return stack for navigation etc.</param>
		public FindEBCInputAdapter(
            TDJourneyParametersMulti journeyParams, FindEBCPageState pageState, InputPageState inputPageState) : 
			base(pageState, TDSessionManager.Current, inputPageState, journeyParams)
		{
            populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            carCostCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarCostCalculator];
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a AsyncCallState object for Cycle plans
        /// </summary>
        /// <returns></returns>
        public override void InitialiseAsyncCallState()
        {
            AsyncCallState acs = new JourneyPlanState();

            // Determine refresh interval and resource string for the wait page from parameters passed in
            acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.EnvironmentalBenefitsCalculator"]);
            acs.WaitPageMessageResourceFile = "langStrings";
            acs.WaitPageMessageResourceId = "WaitPageMessage.FindEBC";

            acs.AmbiguityPage = PageId.FindEBCInput;
            acs.DestinationPage = PageId.EBCJourneyDetails;
            acs.ErrorPage = PageId.EBCJourneyDetails;
            acs.Status = AsyncCallStatus.None;
            tdSessionManager.AsyncCallState = acs;
        }

        /// <summary>
        /// Creates new location and search objects for each "to", "from" and "via" locations and 
        /// assigns those to the journey parameters. Then initiates a new search on those objects using
        /// the original user's input values. 
        /// </summary>
        /// <param name="locationsControl"></param>
        public void AmbiguitySearch(FindToFromLocationsControl locationsControl, FindLocationControl viaFindLocationControl)
        {
            base.AmbiguitySearch(locationsControl);

            if (viaFindLocationControl.TheLocation.Status == TDLocationStatus.Unspecified)
            {
                // Only search if text has been entered
                if (!string.IsNullOrEmpty(viaFindLocationControl.TheSearch.InputText))
                {
                    viaFindLocationControl.Search();
                }
            }
        }

        /// <summary>
        /// Initialises journey parameters with default values for EBC journey planning.
        /// Also loads user preferences into journey parameters.
        /// </summary>
        public override void InitJourneyParameters()
        {
            base.InitJourneyParameters();

            // Also Initialise the ValidationError object
            if (TDSessionManager.Current.ValidationError != null)
                TDSessionManager.Current.ValidationError.Initialise();

            journeyParams.Origin.SearchType = SearchType.Locality;
            journeyParams.Destination.SearchType = SearchType.Locality;

            journeyParams.PrivateViaType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
            journeyParams.PrivateVia = new LocationSearch();
            journeyParams.PrivateVia.SearchType = SearchType.Locality;

            // Only want a road journey
            journeyParams.PublicRequired = false;
            journeyParams.PrivateRequired = true;
            journeyParams.CycleRequired = false;
            journeyParams.PublicModes = new ModeType[0];

            journeyParams.AvoidMotorWays = false;
            journeyParams.AvoidFerries = false;
            journeyParams.AvoidTolls = false;
            journeyParams.DoNotUseMotorways = false;

            journeyParams.AvoidRoadsList = new TDRoad[0];
            journeyParams.UseRoadsList = new TDRoad[0];

            journeyParams.DrivingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop), TDCultureInfo.CurrentCulture);
            journeyParams.FuelConsumptionUnit = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.UnitsDrop), TDCultureInfo.CurrentCulture);

            #region Set journey parameters from properties
            // Read and validate journey parameters from Properties (set defaults first
            string propPrivateAlgorithmType = populator.GetDefaultListControlValue(DataServiceType.DrivingFindDrop);
            PrivateAlgorithmType privateAlgorithmType = (PrivateAlgorithmType)Enum.Parse(typeof(PrivateAlgorithmType), propPrivateAlgorithmType);
            string propCarSize = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
            string propCarFuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
            bool propIgnoreCongestion = true;
            int propCongestionValue = -1;
            try
            {
                propPrivateAlgorithmType = Properties.Current["EnvironmentalBenefitsCalculator.JourneyParameters.JourneyAlgorithm"];
                privateAlgorithmType = (PrivateAlgorithmType)Enum.Parse(typeof(PrivateAlgorithmType), propPrivateAlgorithmType, true);

                propCarSize = Properties.Current["EnvironmentalBenefitsCalculator.JourneyParameters.CarSize"];
                propCarFuelType = Properties.Current["EnvironmentalBenefitsCalculator.JourneyParameters.FuelType"];

                propCarSize = propCarSize.ToLower();
                propCarFuelType = propCarFuelType.ToLower();

                propIgnoreCongestion = bool.Parse(Properties.Current["EnvironmentalBenefitsCalculator.JourneyParameters.IgnoreCongestion"]);

                propCongestionValue = int.Parse(Properties.Current["EnvironmentalBenefitsCalculator.JourneyParameters.CongestionValue"]);

            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    "One or more EnvironmentalBenefitsCalculator.JourneyParameters are missing or threw an error when parsing the car parameters: "
                    + ex.Message));
            }

            // Assign property values to the journeyParams
            journeyParams.PrivateAlgorithmType = privateAlgorithmType;
            journeyParams.CarSize = propCarSize;
            journeyParams.CarFuelType = propCarFuelType;
            journeyParams.IgnoreCongestion = propIgnoreCongestion;
            journeyParams.CongestionValue = propCongestionValue;
            
            // Calculate fuel consumption and cost
            int fuelConsumption = carCostCalculator.GetFuelConsumption(journeyParams.CarSize, journeyParams.CarFuelType);
            int fuelCost = carCostCalculator.GetFuelCost(journeyParams.CarFuelType);
            journeyParams.FuelConsumptionEntered = string.Format(fuelConsumption.ToString());
            journeyParams.FuelCostEntered = string.Format(fuelCost.ToString());
            journeyParams.FuelConsumptionOption = true;
            journeyParams.FuelConsumptionValid = true;
            journeyParams.FuelCostOption = true;
            journeyParams.FuelCostValid = true;

            // Log error if fuel consumption and/or cost are 0 because CJP will not plan the road journey
            if (fuelConsumption <= 0)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("EnvironmentalBenefitsCalculator.JourneyParameters for fuel comsumption is 0 (for car size [{0}] and fuel type [{1}]). Journey planning may fail."
                    , journeyParams.CarSize
                    , journeyParams.CarFuelType)));
            }

            if (fuelCost <= 0)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("EnvironmentalBenefitsCalculator.JourneyParameters for fuel cost is 0 (for fuel type [{0}]). Journey planning may fail."
                    , journeyParams.CarFuelType)));
            }
            #endregion

            #region Set journey parameters date time from properties

            int hour = 01;
            int min = 00;
            DayOfWeek dayOfWeek = DayOfWeek.Tuesday;
            bool leaveAfter = true;

            try
            {   
                // Read time and day values from properties
                string propTime = Properties.Current["EnvironmentalBenefitsCalculator.JourneyParameters.OutwardTime"];
                string propDayOfWeek = Properties.Current["EnvironmentalBenefitsCalculator.JourneyParameters.OutwardDayOfWeek"];
                string propLeaveAfter = Properties.Current["EnvironmentalBenefitsCalculator.JourneyParameters.LeaveAt"];

                // Convert the time property in to usable hour and minute
                string propHour = propTime.Substring(0, 2);
                string propMinute = propTime.Substring(2, 2);

                hour = Convert.ToInt32(propHour, CultureInfo.CurrentCulture.NumberFormat);
                min = Convert.ToInt32(propMinute, CultureInfo.CurrentCulture.NumberFormat);

                // Convert the day in to a usable day of week
                dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), propDayOfWeek, true);

                // Convert leave after in to a bool
                leaveAfter = bool.Parse(propLeaveAfter);
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    "One or more EnvironmentalBenefitsCalculator.JourneyParameters are missing or threw an error when parsing the date time parameters: "
                    + ex.Message));

                // Set and use the default time
                journeyParams.InitialiseDefaultOutwardTime();
                hour = Convert.ToInt32(journeyParams.OutwardHour);
                min = Convert.ToInt32(journeyParams.OutwardMinute);
            }

            // Date and time is set from today
            TDDateTime tdDateTime = new TDDateTime(DateTime.Now);
            
            // Time is set to the values read from properties
            TDTimeSpan tdTimeSpan = new TDTimeSpan(hour, min, 0);

            try
            {
                // Actual journey date time is set to be the next occurance of the Day of week specified from properties
                tdDateTime = CalculateActualJourneyTime(tdDateTime, (int)dayOfWeek, tdTimeSpan);

                // Set to journey parameters
                journeyParams.OutwardDayOfMonth = tdDateTime.ToString("dd");
                journeyParams.OutwardMonthYear = tdDateTime.ToString("MM") + "/" + tdDateTime.ToString("yyyy");
                journeyParams.OutwardHour = tdDateTime.Hour.ToString();
                journeyParams.OutwardMinute = tdDateTime.Minute.ToString();
                journeyParams.OutwardArriveBefore = !leaveAfter;
                journeyParams.OutwardAnyTime = false;
            }
            catch (TDException tdEx)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, tdEx.Message));
            }

            #endregion
            
            // Load the user saved preferences
            LoadTravelDetails();
        }

        /// <summary>
        /// Initialises controls used on page
        /// </summary>
        public void InitialiseControls(FindToFromLocationsControl locationsControl, FindLocationControl viaFindLocationControl)
        {
            InitLocationsControl(locationsControl);
            InitViaLocationsControl(viaFindLocationControl);
        }

        
        /// <summary>
        /// Initialises location control for specifying private via locations
        /// </summary>
        public void InitViaLocationsControl(FindLocationControl viaFindLocationControl)
        {
            // Via location
            viaFindLocationControl.LocationControlType = journeyParams.PrivateViaType;
            viaFindLocationControl.LocationType = CurrentLocationType.PrivateVia;
            viaFindLocationControl.TheLocation = journeyParams.PrivateViaLocation;
            viaFindLocationControl.TheSearch = journeyParams.PrivateVia;

            // Hide StationTypes check list for via location
            viaFindLocationControl.StationTypesCheckListVisible = false;
            viaFindLocationControl.DirectionLabelVisible = true;

            // Display via location control 
            viaFindLocationControl.Visible = true;
        }

        /// <summary>
        /// Updates supplied label with message to correct errors. Message depends on errors found in supplied
        /// errors object. The visibility of panelErrorMessage is set to true if the label is set with a message
        /// or false if not.
        /// </summary>
        /// <param name="panelErrorMessage">Panel to set visibility of</param>
        /// <param name="labelErrorMessages">Label to update with message</param>
        /// <param name="errors">errors to search</param>
        public override void UpdateErrorMessages(Panel panelErrorMessage, Label labelErrorMessages, ValidationError errors)
        {
            base.UpdateErrorMessages(panelErrorMessage, labelErrorMessages, errors);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Updates travelPreferences with EBC preferences to save.
        /// </summary>
        /// <param name="travelPreferences">Object to update with preferences that will be saved</param>
        protected override void saveModeSpecificPreferences(TDProfile travelPreferences)
        {
            // No preferences to save
        }

        /// <summary>
        /// Updates journey parameters with values supplied in travelPreferences.
        /// </summary>
        /// <param name="travelPreferences">Object containing loaded user preferences</param>
        protected override void loadModeSpecificPreferences(TDProfile travelPreferences)
        {
            // No preferences to load
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Static read only. Indicates if EBC planner functionality should be made available 
        /// </summary>
        /// <returns></returns>
        public static bool EBCPlannerAvailable
        {
            get
            {
                try
                {
                    return bool.Parse(Properties.Current["EnvironmentalBenefitsCalculator.Available"]);
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Calculates actual journey time based on configuration parameters.
        /// </summary>
        /// <param name="absoluteDateTime">The absolute date and time specified.</param>
        /// <param name="targetDay">The target day</param>
        /// <param name="time">The time of the journey.</param>
        /// <returns>The actual journey datetime.</returns>
        public static TDDateTime CalculateActualJourneyTime(TDDateTime absoluteDateTime, int targetDay, TDTimeSpan time)
        {
            try
            {
                // Add span to current datetime.
                DateTime temp = DateTime.Now;

                bool dayValid = false;
                DayOfWeek dow = (DayOfWeek)targetDay;
                temp = temp.AddDays(1);
                while (!dayValid)
                {
                    if (temp.DayOfWeek != dow)
                    {
                        temp = temp.AddDays(1);
                    }
                    else
                    {
                        dayValid = true;
                    }
                }

                // Logic to avoid planning journey on christmas and new year days
                if (temp.Day == 25 && temp.Month == 12)
                {
                    temp = temp.AddDays(14);
                }
                else
                {
                    if (temp.Day == 26 && temp.Month == 12)
                    {
                        temp = temp.AddDays(7);
                    }
                    else
                    {
                        // Should never be true but here for completeness
                        if (temp.Day == 1 && temp.Month == 1)
                        {
                            temp = temp.AddDays(7);
                        }
                    }
                }

                return new TDDateTime(temp.Year, temp.Month, temp.Day,
                    time.Hours, time.Minutes, time.Seconds);
            }
            catch (Exception exception) // Insufficient documentation so catch all.
            {
                throw new TDException(
                    string.Format("Environmental Benefits Calculator - error occurred attempting to calculate journey datetime using values from Properties: {0}", exception.Message), 
                    false,
                    TDExceptionIdentifier.EBCErrorCalculatingJourneyDateTime);
            }
        }
        #endregion
    }
}
