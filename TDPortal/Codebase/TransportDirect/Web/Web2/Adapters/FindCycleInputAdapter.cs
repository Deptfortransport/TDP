// *********************************************** 
// NAME			: FindCycleInputAdapter.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 05 Jun 2008
// DESCRIPTION	: Responsible for the cycle planner functionality
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FindCycleInputAdapter.cs-arc  $
//
//   Rev 1.10   Aug 28 2012 10:20:46   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.9   Nov 12 2008 17:34:54   mturner
//Updates to resolve issues with selection of start and end points. IR5170.
//Resolution for 5170: Cycle Planner - Journeys can start or End on Motorways
//
//   Rev 1.8   Oct 27 2008 14:14:26   mmodi
//Made default cycle speed a public method
//Resolution for 5153: Cycle Planner - Incorrect data as "Average cycling speed is 20mph" is displayed on 'Cycle Journey Details' page
//
//   Rev 1.7   Oct 20 2008 11:09:50   mmodi
//Updated to allow override location coordinates to be used for cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6   Oct 10 2008 16:01:44   mmodi
//Updated to have avoid time based check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5   Sep 09 2008 13:19:24   mmodi
//Updated to load saved preferences
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.4   Aug 22 2008 10:26:16   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Aug 01 2008 16:32:42   mmodi
//Added checks for cycle planner availablity
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Jul 28 2008 13:08:42   mmodi
//Updates
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.1   Jul 18 2008 13:48:04   mmodi
//Updates as part of workstream
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Jun 20 2008 15:29:06   mmodi
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//

using System;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public class FindCycleInputAdapter : FindJourneyInputAdapter
    {
        #region Constructor
        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="journeyParams">Journey parameters for Find Cycle journey planning</param>
		/// <param name="pageState">Page state for Find Cycle input pages</param>
		/// <param name="inputPageState">Variables indicating state of page, return stack for navigation etc.</param>
		public FindCycleInputAdapter(
            TDJourneyParametersMulti journeyParams, FindCyclePageState pageState, InputPageState inputPageState) : 
			base(pageState, TDSessionManager.Current, inputPageState, journeyParams)
		{

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
            acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["CyclePlanner.WaitPageRefreshSeconds.FindACycle"]);
            acs.WaitPageMessageResourceFile = "langStrings";
            acs.WaitPageMessageResourceId = "WaitPageMessage.FindACycle";

            acs.AmbiguityPage = PageId.FindCycleInput;
            acs.DestinationPage = PageId.CycleJourneyDetails;
            acs.ErrorPage = PageId.CycleJourneyDetails;
            acs.Status = AsyncCallStatus.None;
            tdSessionManager.AsyncCallState = acs;
        }

        /// <summary>
		/// Initialises journey parameters with default values for Find cycle journey planning.
		/// Also loads user preferences into journey parameters.
		/// </summary>
        public override void InitJourneyParameters()
        {
            base.InitJourneyParameters();

            // Also Initialise the ValidationError object
            if (TDSessionManager.Current.ValidationError != null)
                TDSessionManager.Current.ValidationError.Initialise();

            journeyParams.Origin.SearchType = SearchType.AddressPostCode;
            journeyParams.Destination.SearchType = SearchType.AddressPostCode;

            journeyParams.CycleViaType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
            journeyParams.CycleVia = new LocationSearch();

            journeyParams.CycleVia.SearchType = SearchType.AddressPostCode;

            journeyParams.CycleSpeedMax = GetDefaultCycleSpeed();

            // Only want a cycle result, although as we use the Cycle runner, we won't get any other type of journey
            journeyParams.PublicRequired = false;
            journeyParams.PrivateRequired = false;

            journeyParams.PublicModes = new ModeType[] { ModeType.Cycle };

            journeyParams.CycleRequired = true;

            // Load the user saved preferences
            LoadTravelDetails();
        }

        /// <summary>
        /// Initialises controls used on page
        /// </summary>
        public void InitialiseControls(LocationControl originLocationControl, LocationControl destinationLocationControl, 
            FindCyclePreferencesControl preferencesControl, FindCycleJourneyTypeControl journeyTypeControl)
        {
            InitLocationsControl(originLocationControl, destinationLocationControl);
            InitPreferencesControl(preferencesControl);
            InitJourneyTypeControl(journeyTypeControl);

            if (pageState.AmbiguityMode)
            {
                UpdatePreferencesDisplayMode(preferencesControl, GenericDisplayMode.ReadOnly);
            }
            else
            {
                UpdatePreferencesDisplayMode(preferencesControl, GenericDisplayMode.Normal);
            }
        }

        /// <summary>
        /// Initialises the locationsControl with journey parameters
        /// </summary>
        /// <param name="locationsControl">The control to initialise</param>
        public void InitLocationsControl(LocationControl originLocationControl, LocationControl destinationLocationControl)
        {
            originLocationControl.Initialise(journeyParams.OriginLocation, journeyParams.Origin, DataServiceType.FindCycleLocationDrop, true, true, false, true, true, true, false, pageState.AmbiguityMode, false);
            destinationLocationControl.Initialise(journeyParams.DestinationLocation, journeyParams.Destination, DataServiceType.FindCycleLocationDrop, true, true, false, true, true, true, false, pageState.AmbiguityMode, false);
        }

        /// <summary>
        /// Initialises preferences control with journey parameters and page state values
        /// </summary>
        public void InitPreferencesControl(FindCyclePreferencesControl preferencesControl)
        {
            // Via location
            preferencesControl.LocationControl.Initialise(journeyParams.CycleViaLocation, journeyParams.CycleVia, DataServiceType.CycleViaLocationDrop, true, true, false, true, true, true, false, pageState.AmbiguityMode, false);

            // Advanced options visibility
            preferencesControl.PreferencesVisible = pageState.TravelDetailsVisible;
        }

        /// <summary>
        /// Initialises journey type control with journey parameters
        /// </summary>
        /// <param name="journeyTypeControl"></param>
        public void InitJourneyTypeControl(FindCycleJourneyTypeControl journeyTypeControl)
        {
            journeyTypeControl.JourneyType = journeyParams.CycleJourneyType;
        }

        /// <summary>
        /// Helper method to set up the specific location values required for Cycle planning.
        /// Calls methods and populates values on the Location object
        /// </summary>
        public void PopulateCycleLocations(LocationControl originLocationControl, LocationControl destinationLocationControl, FindCyclePreferencesControl preferencesControl)
        {
            bool originUseFindPointOnToid = false;
            bool destinationUseFindPointOnToid = false;
            bool viaUseFindPointOnToid = false;

            // Calculate the Origin Point
            //
            // We should only use find NearestPointonToid (useFindPointOnToid = true) when
            // we have a street name to match.
            if (!journeyParams.CycleLocationsIsDefault && journeyParams.CycleLocationOriginOverride.IsValid)
            {
                // Normal logic has been overridden by a type 2 user so set co-ords to the overridden values
                originLocationControl.Location.GridReference = journeyParams.CycleLocationOriginOverride;
                originLocationControl.Location.Description = journeyParams.CycleLocationOriginOverride.Easting.ToString() + "," + journeyParams.CycleLocationOriginOverride.Northing.ToString();
                originLocationControl.Location.Status = TDLocationStatus.Valid;
            }
            else
            {
                // If location has an address to match toids for set flag to use Find Nearest Point logic
                if (originLocationControl.Location.AddressToMatch.Length > 0)
                {
                    originUseFindPointOnToid = true;
                }
            }
            
            // Populate the Origin Point
            if (originLocationControl.Location.Status == TDLocationStatus.Valid)
            {
                originLocationControl.Location.PopulatePoint(originUseFindPointOnToid);
            }
            
            // Calculate the Destination Point
            if (!journeyParams.CycleLocationsIsDefault && journeyParams.CycleLocationDestinationOverride.IsValid)
            {
                // Normal logic has been overridden by a type 2 user so set co-ords to the overridden values
                destinationLocationControl.Location.GridReference = journeyParams.CycleLocationDestinationOverride;
                destinationLocationControl.Location.Description = journeyParams.CycleLocationDestinationOverride.Easting.ToString() + "," + journeyParams.CycleLocationDestinationOverride.Northing.ToString();
                destinationLocationControl.Location.Status = TDLocationStatus.Valid;
            }
            else
            {
                // If location has an address to match toids for set flag to use Find Nearest Point logic
                if (destinationLocationControl.Location.AddressToMatch.Length > 0)
                {
                    destinationUseFindPointOnToid = true;
                }
            }

            //Populate the Destination Point
            if (destinationLocationControl.Location.Status == TDLocationStatus.Valid)
            {
                destinationLocationControl.Location.PopulatePoint(destinationUseFindPointOnToid);
            }


            // If location has an address to match toids for set flag to use Find Nearest Point logic
            if (preferencesControl.LocationControl.Location.AddressToMatch.Length > 0)
            {
                viaUseFindPointOnToid = true;
            }
            // Populate the Via Point
            if (preferencesControl.LocationControl.Location.Status == TDLocationStatus.Valid)
            {
                preferencesControl.LocationControl.Location.PopulatePoint(viaUseFindPointOnToid);
            }
        }

        /// <summary>
        /// Helper method which populates the CyclePreferencesControl with values from the JourneyParameters
        /// </summary>
        /// <param name="preferencesControl"></param>
        /// <param name="journeyParams"></param>
        public void UpdatePreferencesControl(FindCyclePreferencesControl preferencesControl)
        {
            preferencesControl.AvoidSteepClimbs = journeyParams.CycleAvoidSteepClimbs;
            preferencesControl.AvoidUnlitRoads = journeyParams.CycleAvoidUnlitRoads;
            preferencesControl.AvoidWalkingYourBike = journeyParams.CycleAvoidWalkingYourBike;
            preferencesControl.AvoidTimeBased = journeyParams.CycleAvoidTimeBased;
            preferencesControl.SpeedText = journeyParams.CycleSpeedText;
            preferencesControl.SpeedTextValid = journeyParams.CycleSpeedValid;
            preferencesControl.SpeedUnit = journeyParams.CycleSpeedUnit;
            preferencesControl.PenaltyFunctionOverride = journeyParams.CyclePenaltyFunctionOverride;
            preferencesControl.LocationOriginOverride = journeyParams.CycleLocationOriginOverride;
            preferencesControl.LocationDestinationOverride = journeyParams.CycleLocationDestinationOverride;
        }

        /// <summary>
        /// Updates the display mode of all user input fields on FindCyclePreferencesControl
        /// </summary>
        /// <param name="preferencesControl"></param>
        /// <param name="displayMode"></param>
        public void UpdatePreferencesDisplayMode(FindCyclePreferencesControl preferencesControl, GenericDisplayMode displayMode)
        {
            preferencesControl.SpeedDisplayMode = displayMode;
            preferencesControl.AvoidListDisplayMode = displayMode;
            preferencesControl.ViaLocationDisplayMode = displayMode;
            preferencesControl.PenaltyFunctionDisplayMode = displayMode;
            preferencesControl.LocationOverrideDisplayMode = displayMode;
        }

        /// <summary>
        /// Updates the display mode of all user input fields on FindCycleJourneyTypeControl
        /// </summary>
        /// <param name="preferencesControl"></param>
        /// <param name="displayMode"></param>
        public void UpdateJourneyTypeDisplayMode(FindCycleJourneyTypeControl journeyTypeControl, GenericDisplayMode displayMode)
        {
            journeyTypeControl.JourneyTypeDisplayMode = displayMode;
        }

        /// <summary>
        /// Returns the default cycle speed as defined in Properties
        /// </summary>
        /// <returns></returns>
        public string GetDefaultCycleSpeed()
        {            
            string cycleSpeed = "19312"; // Default, incase we can't obtain from properties

            double cycleSpeedDefault = 19312;

            bool cycleDefaultSpeedOK = double.TryParse(Properties.Current["CyclePlanner.Planner.CyclingDefaultSpeed.MetresPerHour"], out cycleSpeedDefault);
            
            // If we obtained the default speed ok, then assign and return
            if (cycleDefaultSpeedOK)
                cycleSpeed = cycleSpeedDefault.ToString();

            return cycleSpeed;
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
        /// Updates travelPreferences with cycle preferences to save.
        /// </summary>
        /// <param name="travelPreferences">Object to update with preferences that will be saved</param>
        protected override void saveModeSpecificPreferences(TDProfile travelPreferences)
        {
            UserPreferencesHelper.SaveCyclePreferences((TDJourneyParametersMulti)journeyParams);
        }

        /// <summary>
        /// Updates journey parameters with values supplied in travelPreferences.
        /// </summary>
        /// <param name="travelPreferences">Object containing loaded user preferences</param>
        protected override void loadModeSpecificPreferences(TDProfile travelPreferences)
        {
            TDJourneyParametersMulti jpm = (TDJourneyParametersMulti)journeyParams;
            UserPreferencesHelper.LoadCyclePreferences(jpm);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Static read only. Indicates if cycle planner functionality should be made available 
        /// </summary>
        /// <returns></returns>
        public static bool CyclePlannerAvailable
        {
            get
            {
                try
                {
                    return bool.Parse(Properties.Current["CyclePlanner.Available"]);
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Static read only. Indicates if gradient profiler functionality should be made available
        /// </summary>
        /// <returns></returns>
        public static bool GradientProfilerAvailable
        {
            get
            {
                try
                {
                    return bool.Parse(Properties.Current["GradientProfiler.Available"]);
                }
                catch
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
